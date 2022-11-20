using Intersect.Client.Core;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.General;
using Intersect.Client.Interface.Components;

namespace Intersect.Client.Interface.Game.Components
{
    public class HarvestProgressBarComponent : ProgressBarComponent
    {
        public HarvestProgressBarComponent(
            ImagePanel parent,
            string containerName,
            string bgTexture,
            string fgTexture,
            string topLabel = "",
            Color topLabelColor = null,
            string bottomLabel = "",
            Color bottomLabelColor = null,
            string leftLabel = "",
            Color leftLabelColor = null,
            string rightLabel = "",
            Color rightLabelColor = null,
            ComponentList<ProgressBarComponent> referenceList = null
        ) : base(parent,
            "HarvestProgressBarComponent",
            containerName,
            bgTexture,
            fgTexture,
            topLabel,
            topLabelColor,
            bottomLabel,
            bottomLabelColor,
            leftLabel,
            leftLabelColor,
            rightLabel,
            rightLabelColor,
            referenceList)
        {
        }
    }
}
