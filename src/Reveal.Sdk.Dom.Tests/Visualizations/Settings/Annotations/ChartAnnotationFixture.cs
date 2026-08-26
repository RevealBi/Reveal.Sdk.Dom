using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Reveal.Sdk.Dom.Visualizations;
using Reveal.Sdk.Dom.Visualizations.Settings;
using System;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Visualizations.Settings.Annotations;

public class ChartAnnotationFixture
{
    [Fact]
    public void Constructor_UsesTranslatedApiDefaults_WhenCreated()
    {
        var settings = new LineChartVisualizationSettings();
        var point = new ChartDataPointAnnotation();
        var category = new ChartCategoryAnnotation();
        var range = new ChartCategoryRangeAnnotation();

        Assert.Empty(settings.Annotations);

        Assert.Equal(ChartAnnotationType.DataPoint, point.Type);
        Assert.Equal(0, point.SeriesIndex);
        Assert.Null(point.SeriesField);
        Assert.Equal(0, point.CategoryValue);
        Assert.Equal(0, point.Value);

        Assert.Equal(ChartAnnotationType.Category, category.Type);
        Assert.Equal(0, category.CategoryValue);

        Assert.Equal(ChartAnnotationType.CategoryRange, range.Type);
        Assert.Equal(0, range.StartValue);
        Assert.Equal(0, range.EndValue);

        Assert.Equal(AnnotationDefaults("Point"), JObject.Parse(point.ToJsonString()));
        Assert.Equal(AnnotationDefaults("Slice"), JObject.Parse(category.ToJsonString()));
        Assert.Equal(AnnotationDefaults("Strip"), JObject.Parse(range.ToJsonString()));
        Assert.Equal(new JArray(), JObject.Parse(settings.ToJsonString())["Annotations"]);
    }

    [Fact]
    public void ToJsonString_UsesExactRdashShape_WhenDataPointAnnotationIsSerialized()
    {
        var annotation = new ChartDataPointAnnotation(7.25, 1588598)
        {
            Id = "d1545d80-7012-4fe2-500a-67734c7174af",
            Title = "Large transaction",
            Description = "Review this value",
            SeriesIndex = 2,
            SeriesField = "Sum of amount",
            MarkerColor = "#8961a9"
        };

        var expected = JObject.Parse(
            """
            {
              "_type": "ChartAnnotationType",
              "Identifier": "d1545d80-7012-4fe2-500a-67734c7174af",
              "AnnotationType": "Point",
              "Title": "Large transaction",
              "Description": "Review this value",
              "TargetSeriesIndex": 2,
              "TargetSeriesField": "Sum of amount",
              "XValue": 7.25,
              "YValue": 1588598,
              "StartValue": 0,
              "EndValue": 0,
              "MarkerColor": "#8961a9"
            }
            """);

        Assert.Equal(expected, JObject.Parse(annotation.ToJsonString()));
    }

    [Fact]
    public void ToJsonString_UsesExactRdashShape_WhenCategoryAnnotationIsSerialized()
    {
        var annotation = new ChartCategoryAnnotation(9)
        {
            Id = "category-id",
            Title = "Launch",
            Description = "Product launch"
        };

        var expected = JObject.Parse(
            """
            {
              "_type": "ChartAnnotationType",
              "Identifier": "category-id",
              "AnnotationType": "Slice",
              "Title": "Launch",
              "Description": "Product launch",
              "TargetSeriesIndex": 0,
              "XValue": 9,
              "YValue": 0,
              "StartValue": 0,
              "EndValue": 0
            }
            """);

        Assert.Equal(expected, JObject.Parse(annotation.ToJsonString()));
    }

    [Fact]
    public void ToJsonString_UsesExactRdashShape_WhenCategoryRangeAnnotationIsSerialized()
    {
        var annotation = new ChartCategoryRangeAnnotation(9, 15)
        {
            Id = "range-id",
            Title = "Campaign",
            Description = "Campaign period",
            MarkerColor = "#e051a9"
        };

        var expected = JObject.Parse(
            """
            {
              "_type": "ChartAnnotationType",
              "Identifier": "range-id",
              "AnnotationType": "Strip",
              "Title": "Campaign",
              "Description": "Campaign period",
              "TargetSeriesIndex": 0,
              "XValue": 0,
              "YValue": 0,
              "StartValue": 9,
              "EndValue": 15,
              "MarkerColor": "#e051a9"
            }
            """);

        Assert.Equal(expected, JObject.Parse(annotation.ToJsonString()));
    }

    [Fact]
    public void DeserializeObject_CreatesEveryPublicAnnotationType_AndRoundTrips()
    {
        var annotationsJson = JArray.Parse(
            """
            [
              {
                "_type": "ChartAnnotationType",
                "Identifier": "point-id",
                "AnnotationType": "Point",
                "Title": "Point",
                "Description": "Point description",
                "TargetSeriesIndex": 3,
                "TargetSeriesField": "Revenue",
                "XValue": 2,
                "YValue": 125,
                "StartValue": 0,
                "EndValue": 0,
                "MarkerColor": "#123456"
              },
              {
                "_type": "ChartAnnotationType",
                "Identifier": "category-id",
                "AnnotationType": "Slice",
                "Title": "Category",
                "Description": "Category description",
                "TargetSeriesIndex": 0,
                "XValue": 4,
                "YValue": 0,
                "StartValue": 0,
                "EndValue": 0
              },
              {
                "_type": "ChartAnnotationType",
                "Identifier": "range-id",
                "AnnotationType": "Strip",
                "Title": "Range",
                "Description": "Range description",
                "TargetSeriesIndex": 0,
                "XValue": 0,
                "YValue": 0,
                "StartValue": 5,
                "EndValue": 8,
                "MarkerColor": "#abcdef"
              }
            ]
            """);

        var settings = JsonConvert.DeserializeObject<LineChartVisualizationSettings>(
            new JObject { ["Annotations"] = annotationsJson }.ToString());

        var point = Assert.IsType<ChartDataPointAnnotation>(settings.Annotations[0]);
        Assert.Equal(ChartAnnotationType.DataPoint, point.Type);
        Assert.Equal("point-id", point.Id);
        Assert.Equal(3, point.SeriesIndex);
        Assert.Equal("Revenue", point.SeriesField);
        Assert.Equal(2, point.CategoryValue);
        Assert.Equal(125, point.Value);
        Assert.Equal("#123456", point.MarkerColor);

        var category = Assert.IsType<ChartCategoryAnnotation>(settings.Annotations[1]);
        Assert.Equal(ChartAnnotationType.Category, category.Type);
        Assert.Equal(4, category.CategoryValue);

        var range = Assert.IsType<ChartCategoryRangeAnnotation>(settings.Annotations[2]);
        Assert.Equal(ChartAnnotationType.CategoryRange, range.Type);
        Assert.Equal(5, range.StartValue);
        Assert.Equal(8, range.EndValue);

        var roundTrip = JObject.Parse(settings.ToJsonString());
        Assert.Equal(annotationsJson, roundTrip["Annotations"]);
    }

    [Fact]
    public void DeserializeObject_DefaultsMissingOrUnknownType_ToDataPointAnnotation()
    {
        const string json =
            """
            {
              "Annotations": [
                { "_type": "ChartAnnotationType" },
                { "_type": "ChartAnnotationType", "AnnotationType": "Unknown" }
              ]
            }
            """;

        var settings = JsonConvert.DeserializeObject<LineChartVisualizationSettings>(json);

        Assert.All(settings.Annotations, annotation =>
        {
            Assert.IsType<ChartDataPointAnnotation>(annotation);
            Assert.Equal(ChartAnnotationType.DataPoint, annotation.Type);
        });
    }

    [Theory]
    [InlineData(typeof(AreaChartVisualizationSettings))]
    [InlineData(typeof(BarChartVisualizationSettings))]
    [InlineData(typeof(ColumnChartVisualizationSettings))]
    [InlineData(typeof(LineChartVisualizationSettings))]
    [InlineData(typeof(SplineAreaChartVisualizationSettings))]
    [InlineData(typeof(SplineChartVisualizationSettings))]
    [InlineData(typeof(StepAreaChartVisualizationSettings))]
    [InlineData(typeof(StepLineChartVisualizationSettings))]
    [InlineData(typeof(TimeSeriesVisualizationSettings))]
    public void Annotations_AreAvailable_OnEverySupportedCategoryChart(Type settingsType)
    {
        var settings = Assert.IsAssignableFrom<CategoryChartVisualizationSettings>(Activator.CreateInstance(settingsType));
        settings.Annotations.Add(new ChartCategoryAnnotation(1));

        var expected = AnnotationDefaults("Slice");
        expected["XValue"] = 1;

        Assert.Single(settings.Annotations);
        Assert.Equal(new JArray(expected), JObject.Parse(settings.ToJsonString())["Annotations"]);
    }

    [Theory]
    [InlineData(typeof(AssetVisualizationSettings))]
    [InlineData(typeof(BulletGraphVisualizationSettings))]
    [InlineData(typeof(ChoroplethVisualizationSettings))]
    [InlineData(typeof(CircularGaugeVisualizationSettings))]
    [InlineData(typeof(CustomVisualizationSettings))]
    [InlineData(typeof(GaugeVisualizationSettings))]
    [InlineData(typeof(KpiTargetVisualizationSettings))]
    [InlineData(typeof(KpiTimeVisualizationSettings))]
    [InlineData(typeof(LinearGaugeVisualizationSettings))]
    [InlineData(typeof(PivotVisualizationSettings))]
    [InlineData(typeof(ScatterMapVisualizationSettings))]
    [InlineData(typeof(SingleRowVisualizationSettings))]
    [InlineData(typeof(TextBoxVisualizationSettings))]
    [InlineData(typeof(TextVisualizationSettings))]
    [InlineData(typeof(StackedAreaChartVisualizationSettings))]
    [InlineData(typeof(StackedBarChartVisualizationSettings))]
    [InlineData(typeof(StackedColumnChartVisualizationSettings))]
    [InlineData(typeof(BubbleVisualizationSettings))]
    [InlineData(typeof(CandleStickVisualizationSettings))]
    [InlineData(typeof(ComboChartVisualizationSettings))]
    [InlineData(typeof(DoughnutChartVisualizationSettings))]
    [InlineData(typeof(FunnelChartVisualizationSettings))]
    [InlineData(typeof(OHLCVisualizationSettings))]
    [InlineData(typeof(PieChartVisualizationSettings))]
    [InlineData(typeof(RadialVisualizationSettings))]
    [InlineData(typeof(ScatterVisualizationSettings))]
    [InlineData(typeof(GridVisualizationSettings))]
    [InlineData(typeof(SparklineVisualizationSettings))]
    [InlineData(typeof(TreeMapVisualizationSettings))]
    public void Annotations_AreAbsent_FromUnrelatedVisualizationSettings(Type settingsType)
    {
        var settings = Activator.CreateInstance(settingsType);

        Assert.Null(settingsType.GetProperty(nameof(CategoryChartVisualizationSettings.Annotations)));
        Assert.Null(JObject.Parse(settings.ToJsonString()).Property("Annotations"));
    }

    private static JObject AnnotationDefaults(string annotationType)
    {
        return new JObject
        {
            ["_type"] = "ChartAnnotationType",
            ["AnnotationType"] = annotationType,
            ["TargetSeriesIndex"] = 0,
            ["XValue"] = 0,
            ["YValue"] = 0,
            ["StartValue"] = 0,
            ["EndValue"] = 0
        };
    }
}
