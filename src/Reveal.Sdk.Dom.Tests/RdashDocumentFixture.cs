﻿using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Reveal.Sdk.Dom.Core.Constants;
using System;
using System.IO;
using Xunit;

namespace Reveal.Sdk.Dom.Tests
{
    public class RdashDocumentFixture
    {
        [Fact]
        public void NewDashboard_SetsTitle()
        {
            // Arrange
            string title = "TestTitle";

            // Act
            RdashDocument document = new RdashDocument(title);

            //Assert
            Assert.Equal(title, document.Title);
        }

        [Fact]
        public void NewDashboard_SetsDefaultValues()
        {
            var dashboard = new RdashDocument();

            Assert.Equal("New Dashboard", dashboard.Title);
            Assert.Equal(GlobalConstants.RdashDocument.CreatedWith, dashboard.CreatedWith);
            Assert.Equal(string.Empty, dashboard.SavedWith);
            Assert.Equal(Theme.Mountain, dashboard.Theme);
            Assert.Null(dashboard.Tags);
            Assert.Equal(6, dashboard.FormatVersion);
            Assert.True(dashboard.UseAutoLayout);

            Assert.NotNull(dashboard.DataSources);
            Assert.Empty(dashboard.DataSources);

            Assert.NotNull(dashboard.Filters);
            Assert.Empty(dashboard.Filters);

            Assert.NotNull(dashboard.Visualizations);
            Assert.Empty(dashboard.Visualizations);
        }

        [Fact]
        public void Load_SetsProperties()
        {
            var filePath = Path.Combine(Environment.CurrentDirectory, "Dashboards", "Sales.rdash");
            var document = RdashDocument.Load(filePath);

            Assert.NotNull(document);

            Assert.NotNull(document.Title);
            Assert.NotEqual(GlobalConstants.RdashDocument.CreatedWith, document.CreatedWith);
            Assert.NotEqual(string.Empty, document.SavedWith);
            Assert.Equal(Theme.Mountain, document.Theme);
            Assert.NotNull(document.Tags);
            Assert.Equal(6, document.FormatVersion);
            Assert.False(document.UseAutoLayout);
            Assert.NotEmpty(document.DataSources);
            Assert.NotEmpty(document.Filters);
            Assert.NotEmpty(document.Visualizations);
        }

        [Fact]
        public void ToJsonString_IsValidSchema()
        {
            var schemaJson = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "Schemas", "RdashDocument.json"));
            var schema = JSchema.Parse(schemaJson);

            var dashboard = new RdashDocument()
            {
                Title = "New Dashboard",
                Description = "This is a test dashboard",
                Theme = Theme.Aurora,
                Tags = "tag1,tag2,tag3"
            };
            var json = dashboard.ToJsonString();

            var jsonDocument = JObject.Parse(json);

            bool isValid = jsonDocument.IsValid(schema);

            Assert.True(isValid);
        }

        [Fact]
        public void Save_SavesFile()
        {
            var filePath = Path.Combine(Path.GetTempPath(), $"{Path.GetTempFileName()}.rdash");

            try
            {
                var dashboard = new RdashDocument();

                if (File.Exists(filePath))
                    File.Delete(filePath);

                dashboard.Save(filePath);

                Assert.Equal("Reveal.Sdk.Dom", dashboard.SavedWith);
                Assert.True(File.Exists(filePath));
            }
            catch
            {
                throw;
            }
            finally
            {
                if (File.Exists(filePath))
                    File.Delete(filePath);
            }
        }

        [Fact]
        public void SettingDescription_SetsDescriptionValue()
        {
            // Arrange
            var dashboard = new RdashDocument();
            var description = "This is a test description";

            // Act
            dashboard.Description = description;

            // Assert
            Assert.Equal(description, dashboard.Description);
        }

        [Fact]
        public void SettingSavedWith_SetsSavedWithValue()
        {
            // Arrange
            var dashboard = new RdashDocument();
            var savedWith = "Custom Test Version 1.0";

            // Act
            dashboard.SavedWith = savedWith;

            // Assert
            Assert.Equal(savedWith, dashboard.SavedWith);
        }

        [Fact]
        public void SettingTheme_SetsThemeValue()
        {
            // Arrange
            var dashboard = new RdashDocument();
            var theme = Theme.Ocean;

            // Act
            dashboard.Theme = theme;

            // Assert
            Assert.Equal(theme, dashboard.Theme);
        }

        [Fact]
        public void SettingTags_SetsTagsValue()
        {
            // Arrange
            var dashboard = new RdashDocument();
            var tags = "tag1,tag2,tag3,tag4";

            // Act
            dashboard.Tags = tags;

            // Assert
            Assert.Equal(tags, dashboard.Tags);
        }

        [Fact]
        public void SettingFormatVersion_SetsFormatVersionValue()
        {
            // Arrange
            var dashboard = new RdashDocument();
            var formatVersion = 7;

            // Act
            dashboard.FormatVersion = formatVersion;

            // Assert
            Assert.Equal(formatVersion, dashboard.FormatVersion);
        }

        [Fact]
        public void SettingUseAutoLayout_SetsUseAutoLayoutValue()
        {
            // Arrange
            var dashboard = new RdashDocument();
            var useAutoLayout = false;

            // Act
            dashboard.UseAutoLayout = useAutoLayout;

            // Assert
            Assert.Equal(useAutoLayout, dashboard.UseAutoLayout);
        }
    }
}
