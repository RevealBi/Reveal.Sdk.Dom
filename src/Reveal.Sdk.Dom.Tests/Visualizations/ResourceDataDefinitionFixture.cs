using Newtonsoft.Json.Linq;
using Reveal.Sdk.Dom.Visualizations;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Visualizations
{
    public class ResourceDataDefinitionFixture
    {
        // Based on Reveal's Tests/SDKCPTests/NetCoreSdkServer/Dashboards/Madagascar.rdash: an image of a web resource.
        private const string DashboardJson =
            """
            {
              "Title": "Madagascar",
              "ThemeName": "rvDashboardMountainTheme",
              "FormatVersion": 6,
              "UseAutoLayout": true,
              "Tags": "newdash",
              "Widgets": [ {
                "Id": "a4f929fc-12ef-4256-ad1a-49613b039af5",
                "RowSpan": 30,
                "ColumnSpan": 30,
                "IsTitleVisible": true,
                "Title": "Madagascar_Penguins",
                "DataSpec": {
                  "_type": "ResourceDataSpecType",
                  "DataSourceItem": {
                    "_type": "DataSourceItemType",
                    "Id": "e97e10ef_6ce35a55-4829-41d3-96d9-f9ca943262af_Madagascar_Penguins_-_1280x797_%281%29.png",
                    "Title": "Madagascar_Penguins",
                    "DataSourceId": "__ASSET",
                    "HasTabularData": false,
                    "HasAsset": true,
                    "Properties": { "IsImage": true, "IsAsset": true, "FileType": "Image" },
                    "Parameters": {},
                    "ResourceItem": {
                      "_type": "DataSourceItemType",
                      "Id": "https://static.wikia.nocookie.net/thomas-and-twilight-sparkles-adventures/images/f/f7/Madagascar_Penguins_-_1280x797_(1).png_item",
                      "Title": "Madagascar_Penguins_-_1280x797_%281%29.png",
                      "DataSourceId": "e97e10ef_6ce35a55-4829-41d3-96d9-f9ca943262af_resource",
                      "HasTabularData": false,
                      "HasAsset": false,
                      "Properties": { "Url": "https://static.wikia.nocookie.net/thomas-and-twilight-sparkles-adventures/images/f/f7/Madagascar_Penguins_-_1280x797_(1).png" },
                      "Parameters": {}
                    }
                  },
                  "Expiration": 120,
                  "Bindings": { "Bindings": [] }
                },
                "VisualizationSettings": { "_type": "AssetVisualizationSettingsType", "VisualizationType": "IMAGE", "ContentMode": "AspectFit", "ZoomEnabled": true },
                "VisualizationDataSpec": { "_type": "AssetVisualizationDataSpecType" }
              } ],
              "DataSources": [
                { "_type": "DataSourceType", "Id": "__ASSET", "Provider": "ASSET_PROVIDER", "Description": "Madagascar_Penguins", "Properties": { "IsAsset": true, "IsImage": true, "FileType": "Image" } },
                { "_type": "DataSourceType", "Id": "e97e10ef_6ce35a55-4829-41d3-96d9-f9ca943262af_resource", "Provider": "WEBSERVICE", "Properties": { "_rpUseAnonymousAuthentication": true, "Url": "https://static.wikia.nocookie.net/thomas-and-twilight-sparkles-adventures/images/f/f7/Madagascar_Penguins_-_1280x797_(1).png" } }
              ],
              "GlobalFilters": [],
              "GlobalVariables": []
            }
            """;

        [Fact]
        public void Constructor_SetsSchemaTypeAndDefaults_WhenInstanceIsCreated()
        {
            // Act
            var dataDefinition = new ResourceDataDefinition();

            // Assert
            Assert.Equal("ResourceDataSpecType", dataDefinition.SchemaTypeName);
            Assert.Null(dataDefinition.DataSourceItem);
            Assert.Equal(1440, dataDefinition.CacheExpiration);
        }

        [Fact]
        public void LoadFromJson_KeepsDataSpec_WhenVisualizationUsesWebResourceItem()
        {
            // Arrange
            var expected = JObject.Parse(DashboardJson)["Widgets"][0]["DataSpec"];

            // Act
            var document = RdashDocument.LoadFromJson(DashboardJson);
            var saved = JObject.Parse(RdashDocument.LoadFromJson(document.ToJsonString()).ToJsonString());

            // Assert
            var visualization = Assert.IsType<ImageVisualization>(Assert.Single(document.Visualizations));
            var dataDefinition = Assert.IsType<ResourceDataDefinition>(visualization.DataDefinition);
            Assert.Equal(120, dataDefinition.CacheExpiration);
            Assert.Equal("e97e10ef_6ce35a55-4829-41d3-96d9-f9ca943262af_resource", dataDefinition.DataSourceItem.ResourceItem.DataSourceId);
            Assert.True(JToken.DeepEquals(expected, saved["Widgets"][0]["DataSpec"]), saved["Widgets"][0]["DataSpec"].ToString());
        }
    }
}
