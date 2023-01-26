using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Interface.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Client.Interface.Game.Components
{
    public class ChallengeProgressBarComponent : ProgressBarComponent
    {
        public ChallengeProgressBarComponent(
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
            "ChallengeProgressBarComponent",
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
