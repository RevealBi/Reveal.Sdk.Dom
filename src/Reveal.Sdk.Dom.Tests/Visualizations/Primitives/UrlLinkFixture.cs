using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Reveal.Sdk.Dom.Visualizations;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Visualizations.Primitives
{
    public class UrlLinkFixture
    {
        [Fact]
        public void Constructor_SetDefaultValues_WithoutParams()
        {
            // Act
            var instance = new UrlLink();

            // Assert
            Assert.Null(instance.Title);
            Assert.Null(instance.Url);
            Assert.Equal(UrlLinkTarget.NewTab, instance.Target);
            Assert.Equal(LinkType.OpenUrl, instance.Type);
        }

        [Fact]
        public void Constructor_SetDefaultValues_WithParams()
        {
            // Act
            var instance = new UrlLink("Title", "http://url");

            // Assert
            Assert.Equal("Title", instance.Title);
            Assert.Equal("http://url", instance.Url);
            Assert.Equal(UrlLinkTarget.NewTab, instance.Target);
            Assert.Equal(LinkType.OpenUrl, instance.Type);
        }

        [Fact]
        public void Constructor_SetsTarget_WhenProvided()
        {
            // Act
            var instance = new UrlLink("Title", "http://url", UrlLinkTarget.SameTab);

            // Assert
            Assert.Equal(UrlLinkTarget.SameTab, instance.Target);
        }

        [Fact]
        public void Target_UsesRevealJsonValues_WhenRoundTripped()
        {
            // Arrange
            var instance = new UrlLink("Title", "http://url", UrlLinkTarget.SameTab);

            // Act
            var json = instance.ToJsonString();
            var deserialized = JsonConvert.DeserializeObject<UrlLink>(
                """
                {
                  "Title": "Title",
                  "Url": "http://url",
                  "Target": "Blank",
                  "Type": "OpenUrl"
                }
                """);

            // Assert
            Assert.Equal("Self", JObject.Parse(json)["Target"]?.Value<string>());
            Assert.Equal(UrlLinkTarget.NewTab, deserialized.Target);
        }

        [Fact]
        public void ToJsonString_CreateCorrectJsonString_WithoutCondition()
        {
            // Arrange
            var expectedJson = """
            {
              "Url": "Dashboard",
              "Parameters": [],
              "Title": "My Dashboard",
              "Type": "OpenDashboard"
            }
            """;

            var instance = new DashboardLink("My Dashboard", "Dashboard");

            // Act
            var actualJson = instance.ToJsonString();
            var expectedJObject = JObject.Parse(expectedJson);
            var actualJObject = JObject.Parse(actualJson);

            // Assert
            Assert.Equal(expectedJObject, actualJObject);
        }
    }
}
