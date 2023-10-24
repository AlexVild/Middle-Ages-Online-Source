using Intersect.Client.Core;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Interface.Components;

namespace Intersect.Client.Interface.Game.Components
{
    public abstract class GwenComponent : IGwenComponent
    {
        public bool IsHidden => SelfContainer.IsHidden;

        public int Bottom => ParentContainer.Bottom;

        private Base Parent { get; set; }

        /// <summary>
        /// The container containing this component on its parent - will be modifiable only as an image panel in the parent's JSON
        /// </summary>
        public ImagePanel ParentContainer { get; set; }

        protected string ComponentName { get; set; }
        
        /// <summary>
        /// The panel containing THIS component, and to be loaded as an editable JSON file
        /// </summary>
        protected ImagePanel SelfContainer { get; set; }

        public ImagePanel Background => ParentContainer;

        public GwenComponent(
            Base parent,
            string containerName,
            string componentName,
            ComponentList<GwenComponent> referenceList = null)
        {
            Parent = parent;
            ParentContainer = new ImagePanel(parent, containerName);
            ComponentName = componentName;

            if (referenceList != null)
            {
                referenceList.Add(this);
            }
        }

        public virtual void Initialize() 
        {
            SelfContainer.LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
        }

        protected void FitParentToComponent()
        {
            ParentContainer.SetSize(SelfContainer.Width, SelfContainer.Height);
            ParentContainer.ProcessAlignments();
        }

        public void Show()
        {
            SelfContainer.Show();
        }

        public void Hide()
        {
            SelfContainer.Hide();
        }

        public virtual void Dispose()
        {
            ParentContainer.Dispose();
        }

        public virtual void SetPosition(float x, float y)
        {
            ParentContainer.SetPosition(x, y);
        }

        public virtual void ProcessAlignments()
        {
            ParentContainer.ProcessAlignments();
        }
    }
}
