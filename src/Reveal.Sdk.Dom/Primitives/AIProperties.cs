using Reveal.Sdk.Dom.Core;

namespace Reveal.Sdk.Dom
{
    // This interface and class is solely meant as a temporary implementation so Jason can set the AI descripions in his samples.
    // These are not used in Reveal, and only used in Slingshot.
    // We need a proper metadata extension point
    // This will be removed in the future when we have a properly designed implementation of AI.

    public interface IAIMetadata
    {
        /// <summary>
        /// Gets or sets the AI metadata properties.
        /// </summary>
        AIProperties AIProperties { get; set; }
    }

    public class AIProperties : SchemaType
    {
        public AIProperties()
        {
            SchemaTypeName = "VisualizationAIType";
        }

        /// <summary>
        /// Gets or sets the AI description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets whether the AI description is manually set.
        /// </summary>
        public bool Manual { get; set; } = true;

        /// <summary>
        /// Gets or sets whether the AI description is disabled.
        /// </summary>
        public bool Disabled { get; set; }

        /// <summary>
        /// Gets or sets the AI status. (Pending, Genrated, Error)
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets whether the descirption is pending for review by an admin
        /// </summary>
        public string PendingDescription { get; set; }

        /// <summary>
        /// Gets or sets the AI resolution.
        /// </summary>
        public string Resolution { get; set; }
    }
}
