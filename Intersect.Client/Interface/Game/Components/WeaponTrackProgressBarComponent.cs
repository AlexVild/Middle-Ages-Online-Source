using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Interface.Components;

namespace Intersect.Client.Interface.Game.Components
{
    class WeaponTrackProgressBarComponent : ProgressBarComponent
    {
        public WeaponTrackProgressBarComponent(
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
            ComponentList<IGwenComponent> referenceList = null
        ) : base(parent,
            "WeaponTrackProgressBarComponent",
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
