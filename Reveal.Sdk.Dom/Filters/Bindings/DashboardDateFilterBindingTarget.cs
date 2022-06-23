﻿using Reveal.Sdk.Dom.Core.Constants;

namespace Reveal.Sdk.Dom.Filters
{
    public class DashboardDateFilterBindingTarget : BindingTarget
    {
        public string GlobalFilterFieldName { get; set; }

        public DashboardDateFilterBindingTarget()
        {
            SchemaTypeName = SchemaTypeNames.DateGlobalFilterBindingTargetType;
        }
    }
}
