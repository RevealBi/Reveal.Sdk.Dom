using Reveal.Sdk.Dom.Data;
using System.IO;
using System.Linq;
using System;
using Xunit;
using Reveal.Sdk.Dom.Core.Extensions;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using Reveal.Sdk.Dom.Visualizations;
using Reveal.Sdk.Dom.Core.Serialization;

namespace Reveal.Sdk.Dom.Tests.Data.DataSourceItems
{
    public class OracleDataSourceItemFixture
    {
        [Theory]
        [InlineData("Test Item")]
        [InlineData(null)]
        public void Constructor_SetsTitleAndDataSource_WhenCalled(string title)
        {
            // Arrange
            var dataSource = new OracleDataSource();

            // Act
            var item = new OracleDataSourceItem(title, dataSource);

            // Assert
            Assert.Equal(title, item.Title);
            Assert.Equal(dataSource, item.DataSource);
        }

        [Fact]
        public void RDashDocument_HasCorrectDataSourceItem_WhenLoadFromFile()
        {
            // Arrange
            var filePath = Path.Combine(Environment.CurrentDirectory, "Dashboards", "TestOracle.rdash");

            // Act
            var document = RdashDocument.Load(filePath);
            var dataSource = document.DataSources.FirstOrDefault();
            var dataSourceItem = document.Visualizations.FirstOrDefault().DataDefinition.DataSourceItem;

            // Assert
            Assert.Equal(dataSource.Id, dataSourceItem.DataSourceId);
            Assert.Equal(DataSourceProvider.Oracle, dataSource.Provider);
            Assert.NotNull(dataSourceItem.Properties.GetValue<string>("Table"));
            Assert.NotNull(dataSourceItem.Properties.GetValue<string>("Database"));
        }

        [Fact]
        public void ToJsonString_CreatesFormattedJson_ForOracleDataSource()
        {
            // Arrange
            var expectedJson =
                """
                {
                  "_type": "DataSourceItemType",
                  "Id": "oracleSIDDSItemId",
                  "Title": "Oracle SID DSItem",
                  "DataSourceId": "oracleSIDId",
                  "HasTabularData": true,
                  "HasAsset": false,
                  "Properties": {
                    "Database": "HR",
                    "Table": "EMPLOYEES"
                  },
                  "Parameters": {}
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
            var actualJObject = jObject["Widgets"][0]["DataSpec"]["DataSourceItem"];

            // Assert
            Assert.Equal(expectedJObject, actualJObject);
        }
    }
}