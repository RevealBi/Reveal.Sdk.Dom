using Reveal.Sdk.Dom.Core.Extensions;
using Reveal.Sdk.Dom.Data;
using System.IO;
using System.Linq;
using System;
using Xunit;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using Reveal.Sdk.Dom.Visualizations;
using Reveal.Sdk.Dom.Core.Serialization;

namespace Reveal.Sdk.Dom.Tests.Data.DataSources
{
    public class OracleDataSourceFixture
    {
        [Fact]
        public void Constructor_SetsProviderToOracle_WhenConstructed()
        {
            // Act
            var dataSource = new OracleDataSource();

            // Assert
            Assert.Equal(DataSourceProvider.Oracle, dataSource.Provider);
        }

        [Theory]
        [InlineData("TestService")]
        [InlineData(null)]
        public void Service_SetsAndGetsValue_WithDifferentInputs(string service)
        {
            // Arrange
            var dataSource = new OracleDataSource();

            // Act
            dataSource.Service = service;

            // Assert
            Assert.Equal(service, dataSource.Service);
            Assert.Equal(service, dataSource.Properties.GetValue<string>("Service"));
        }

        [Theory]
        [InlineData("TestSID")]
        [InlineData(null)]
        public void SID_SetsAndGetsValue_WithDifferentInputs(string sid)
        {
            // Arrange
            var dataSource = new OracleDataSource();

            // Act
            dataSource.SID = sid;

            // Assert
            Assert.Equal(sid, dataSource.SID);
            Assert.Equal(sid, dataSource.Properties.GetValue<string>("SID"));
        }

        [Fact]
        public void RDashDocument_HasCorrectDataSource_WhenLoadFromFile()
        {
            // Arrange
            var filePath = Path.Combine(Environment.CurrentDirectory, "Dashboards", "TestOracle.rdash");

            // Act
            var document = RdashDocument.Load(filePath);
            var dataSource = document.DataSources.FirstOrDefault();

            // Assert
            Assert.Equal(DataSourceProvider.Oracle, dataSource.Provider);
            Assert.NotNull(dataSource.Properties.GetValue<string>("Host"));
            Assert.NotNull(dataSource.Properties.GetValue<string>("Database"));
            Assert.NotNull(dataSource.Properties.GetValue<string>("SID"));
            Assert.NotNull(dataSource.Properties.GetValue<string>("Port"));
        }

        [Fact]
        public void ToJsonString_CreatesFormattedJson_ForOracleDataSource()
        {
            // Arrange
            var expectedJson =
                """
                {
                  "_type": "DataSourceType",
                  "Id": "oracleSIDId",
                  "Provider": "ORACLE",
                  "Description": "Oracle SID DS",
                  "Subtitle": "Oracle SID Datasource",
                  "Properties": {
                    "Host": "revealdb01.infragistics.local",
                    "Database": "HR",
                    "SID": "orcl",
                    "Port": 1521
                  },
                  "Settings": {}
                }
                """;

            var oracleDataSource = new OracleDataSource
            {
                Id = "oracleSIDId",
                Title = "Oracle SID DS",
                Subtitle = "Oracle SID Datasource",
                Host = "revealdb01.infragistics.local",
                Database = "HR",
                SID = "orcl",
                Port = 1521
            };

            var oracleDataSourceItem = new OracleDataSourceItem("employees report to ID", oracleDataSource)
            {
                Id = "oracleSIDDSItemId",
                Title = "Oracle SID DSItem",
                Database = "HR",
                Table = "EMPLOYEES",
                Fields = new List<IField>
                {
                    new NumberField("MANAGER_ID"),
                    new NumberField("EMPLOYEE_ID"),
                }
            };

            var document = new RdashDocument()
            {
                Title = "Oracle",
                Description = "Example for Oracle",
                UseAutoLayout = false,
            };

            document.Visualizations.Add(new ColumnChartVisualization("Employees report", oracleDataSourceItem)
                .SetLabel("MANAGER_ID")
                .SetValue("EMPLOYEE_ID")
                .SetPosition(20, 11));
            var expectedJObject = JObject.Parse(expectedJson);

            // Act
            RdashSerializer.SerializeObject(document);
            var json = document.ToJsonString();
            var jObject = JObject.Parse(json);
            var actualJObject = jObject["DataSources"].FirstOrDefault();

            // Assert
            Assert.Equal(expectedJObject, actualJObject);
        }
    }
}