using Reveal.Sdk.Dom;
using Reveal.Sdk.Dom.Data;
using Reveal.Sdk.Dom.Visualizations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.Factories
{
    public class GoogleSheetDataSourceDashboards
    {
        public static RdashDocument CreateDashboard()
        {
            var googleSheetDS = new GoogleSheetsDataSourceItem("Google Sheet Data Source", "1BxiMVs0XRA5nFMdKvBdBZjgmUUqptlbs74OgvE2upms")
            {
                Title = "Google Sheet Data Source",
                Subtitle = "Google Sheet Data Source Subtitle",
                Sheet = "Class Data",
                Fields = new List<IField>
                {
                    new TextField("Student Name"),
                    new TextField("Gender"),
                    new TextField("Class Level"),
                    new TextField("Home State"),
                    new TextField("Major"),
                    new TextField("Extracurricular Activity")
                }
            };

            var document = new RdashDocument("My Dashboard");

            document.Visualizations.Add(new GridVisualization("Class List", googleSheetDS).SetColumns("Student Name", "Gender", "Major", "Extracurricular Activity"));

            return document;
        }
    }
}
