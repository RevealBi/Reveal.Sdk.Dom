﻿using Reveal.Sdk.Dom.Core.Constants;

namespace Reveal.Sdk.Dom.Visualizations.Settings
{
    public sealed class TextBoxVisualizationSettings : VisualizationSettings
    {
        public TextBoxVisualizationSettings()
        {
            SchemaTypeName = SchemaTypeNames.TextBoxVisualizationSettingsType;
            VisualizationType = VisualizationTypes.TEXT_BOX;
        }
    }
}
