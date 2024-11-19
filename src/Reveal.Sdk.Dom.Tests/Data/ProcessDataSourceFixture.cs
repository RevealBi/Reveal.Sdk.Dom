using Moq;
using Reveal.Sdk.Dom.Core.Extensions;
using Reveal.Sdk.Dom.Data;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Data
{
    public class ProcessDataSourceFixture
    {
        [Fact]
        public void Constructor_CreateProcessDataSource_WithoutParameters()
        {
            // Arrange
            var expectedProcessDataOnServerDefaultValue = true;
            var expectedProcessDataOnServerReadOnly = false;

            // Act
            var mock = new Mock<ProcessDataSource>();
            var processDataSource = mock.Object;

            // Assert
            Assert.Equal(expectedProcessDataOnServerDefaultValue, processDataSource.ProcessDataOnServerDefaultValue);
            Assert.Equal(expectedProcessDataOnServerReadOnly, processDataSource.ProcessDataOnServerReadOnly);
        }

        [Fact]
        public void GetProcessDataOnServerDefaultValue_ReturnSameValue_WithSetValue()
        {
            // Arrange
            var expectedProcessDataOnServerDefaultValue = false;
            var mock = new Mock<ProcessDataSource>();
            var processDataSource = mock.Object;

            // Act
            processDataSource.ProcessDataOnServerDefaultValue = expectedProcessDataOnServerDefaultValue;

            // Assert
            Assert.Equal(expectedProcessDataOnServerDefaultValue, processDataSource.ProcessDataOnServerDefaultValue);
            Assert.Equal(expectedProcessDataOnServerDefaultValue, processDataSource.Properties.GetValue<bool>("ServerAggregationDefault"));
        }

        [Fact]
        public void GetProcessDataOnServerReadOnly_ReturnSameValue_WithSetValue()
        {
            // Arrange
            var expectedProcessDataOnServerReadOnly = false;
            var mock = new Mock<ProcessDataSource>();
            var processDataSource = mock.Object;

            // Act
            processDataSource.ProcessDataOnServerReadOnly = expectedProcessDataOnServerReadOnly;

            // Assert
            Assert.Equal(expectedProcessDataOnServerReadOnly, processDataSource.ProcessDataOnServerReadOnly);
            Assert.Equal(expectedProcessDataOnServerReadOnly, processDataSource.Properties.GetValue<bool>("ServerAggregationReadOnly"));
        }
    }
}
