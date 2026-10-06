[![Reveal.Sdk.Dom Build](https://github.com/RevealBi/Reveal.Sdk.Dom/actions/workflows/build-dom.yml/badge.svg?branch=main)](https://github.com/RevealBi/Reveal.Sdk.Dom/actions/workflows/build-dom.yml)

# Reveal.Sdk.Dom
The Reveal.Sdk.Dom is a Document Object Model (DOM) for the [Reveal](https://www.revealbi.io/) **.rdash** file format. It allows developer using the [Reveal SDK](https://www.revealbi.io/) to load, edit, and create dashboards using .NET.

The Reveal.Sdk.Dom is currently **BETA** so expect things to not work, or to possibly break in a future release.

Check out the [Samples](https://github.com/RevealBi/Reveal.Sdk.Dom/tree/main/e2e/Sandbox/Factories) in the Sandbox application I am using to develop and test the API.

## Quick Start Guide

Install the Reveal.Sdk.Dom NuGet package:

[![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/Reveal.Sdk.Dom)](https://www.nuget.org/packages/Reveal.Sdk.Dom/)

### Load Dashboard

```cs
string filePath = Path.Combine(Environment.CurrentDirectory, "Sales.rdash");
var document = RdashDocument.Load(filePath);
```

### Create Dashboard

```cs
//create the dashboard document
var document = new RdashDocument("My Dashboard");

//create a data source item using a REST service
var jsonDataSourceItem = new RestServiceBuilder("https://excel2json.io/api/share/6e0f06b3-72d3-4fec-7984-08da43f56bb9")
    .SetTitle("JSON Data Source")
    .SetSubtitle("Sales by Category")
    .SetFields(new List<Field>() //must define the fields returned from the data set
    {
        new NumberField("CategoryID"),
        new TextField("CategoryName"),
        new TextField("ProductName"),
        new NumberField("ProductSales"),
    })
    .Build();

//add a Pie Chart to the dashboard using the REST service as the data source
document.Visualizations.Add(new PieChartVisualization("My Pie Chart", jsonDataSourceItem)
    .SetLabel("CategoryName")
    .SetValue("ProductSales"));

//save the document as an .rdash file
document.Save(filePath);

//optionally load the document into a RevealView
var json = document.ToJsonString();
_revealView.Dashboard = await RVDashboard.LoadFromJsonAsync(json);
```

### Date Filters

Create date filters with a `DateFilterRule`, then replace the selection through `Rule` or the fluent `SetRule` method:

```cs
var salesDate = new DashboardDateFilter("Sales Date",
    DateFilterRule.Last(90, PeriodType.Day, includeToday: false));
document.Filters.Add(salesDate);

salesDate.Rule = DateFilterRule.This(PeriodType.Quarter);
salesDate.SetRule(DateFilterRule.Next(7, PeriodType.Day));

var orderDate = new DateTimeFilter(DateFilterRule.Next(7, PeriodType.Day));
visualization.AddDataFilter("OrderDate", orderDate)
    .ConnectDashboardFilter(salesDate, "OrderDate");

var xmlaDate = new XmlaDateFilter(DateFilterRule.ToDate(PeriodType.Year));
var customDate = new DashboardDateFilter(
    DateFilterRule.Custom(new DateTime(2026, 1, 1), new DateTime(2026, 3, 31)));
```

`Last` selects a rolling window; `Previous` selects complete preceding periods. `Next` begins at the start of the next period (tomorrow for days, next month for months). `This` selects the current period in full, and `ToDate` selects its start through today. Weeks start on Monday. `Last` and `ToDate` accept `includeToday`; setting it to `false` evaluates the window as of yesterday. `Custom` accepts inclusive endpoints and allows `null` for an open endpoint. Use `DateFilterRule.AllTime` to remove the date restriction.

Setting `Rule` also activates rule filtering for `DateTimeFilter` and `XmlaDateFilter` and clears any previous selected values. The existing `FilterType` and `SelectedValues` properties still support other field-filter modes.

The legacy date selection API (`DateRuleType`, `RuleType`, `CustomDateRange`, and `IncludeToday`) and constructors without a rule are no longer public. Existing RDASH files retain their original wire format through internal serialization members, including built-in date rules and their `IncludeToday` value. For example, replace a `LastYear` object initializer with `DateFilterRule.Last(1, PeriodType.Year)`, and `TrailingTwelveMonths` with `DateFilterRule.Previous(12, PeriodType.Month)`.

#### Date filter IDs, bindings, and links

Date bindings and visualization imports use the selected filter's `Id`. Replace `new DashboardDateFilterBinding("OrderDate")` with `new DashboardDateFilterBinding(salesDate, "OrderDate")`, or use `visualization.ConnectDashboardFilter(salesDate, "OrderDate")`.

Date links require the source filter and the target filter (or its ID). This supports dashboards with multiple date filters and different IDs in each dashboard:

```cs
// Load dashboards that have already been saved by the current Reveal SDK.
var source = RdashDocument.Load("Orders.rdash");
var target = RdashDocument.Load("Deliveries.rdash");
var sourceDate = source.Filters.OfType<DashboardDateFilter>()
    .Single(f => f.Title == "Order Date");
var targetDate = target.Filters.OfType<DashboardDateFilter>()
    .Single(f => f.Title == "Delivery Date");
var dateLink = new DateLinkFilter(sourceDate, targetDate);
// Alternatively: new DateLinkFilter(sourceDate, targetDate.Id)
```

`DateLinkFilter()` is now internal for legacy JSON loading. New links serialize the actual source ID in `Value` and the target ID in `Namespace`. `_date` remains accepted as an explicit legacy target alias for the first date filter; source lookup requires the source filter's actual ID.

**Format compatibility:** the DOM still creates format-6 documents with the legacy `_date` default and preserves the format version of loaded files. Reveal SDK's pre-version-7 migration rewrites dashboard date-filter IDs, so format-6 documents cannot safely carry arbitrary date-filter IDs or multiple independently identified date filters. Explicit IDs survive format 7+, while formats below 8 still migrate the legacy `_date` ID and its bindings. Use dashboards saved by the current SDK (format 8+) before adding independent date IDs or constructing links that must keep those IDs stable. Reload the saved document before building links so their source IDs match the migrated filters. Simply changing the format number would skip required visualization migrations; this PR does not perform that document-wide upgrade.
