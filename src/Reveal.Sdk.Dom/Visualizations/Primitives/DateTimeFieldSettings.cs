using Reveal.Sdk.Dom.Core.Constants;

namespace Reveal.Sdk.Dom.Visualizations
{
    public sealed class DateTimeFieldSettings : FieldSettings
    {
        public int DateFiscalYearStartMonth { get; set; }

        public DateTimeFieldSettings()
        {
            SchemaTypeName = SchemaTypeNames.DateTimeFieldSettingsType;
        }
    }
}
