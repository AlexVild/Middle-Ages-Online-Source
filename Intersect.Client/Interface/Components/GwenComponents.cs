using System.Collections.Generic;

namespace Intersect.Client.Interface.Components
{
    /// <summary>
    /// A gwen components should be:
    /// - Instantiated BEFORE loading the Json UI file for its parent
    /// - Initialized AFTER loading the Json UI file for its parent
    /// </summary>
    public interface IGwenComponent
    {
        /// <summary>
        /// Call this, generally, AFTER loading the parent's UI from JSON
        /// </summary>
        void Initialize();

        void Dispose();

        void Hide();

        void Show();
    }

    public class ComponentList<T> : List<T> where T : IGwenComponent
    {
        public void DisposeAll()
        {
            ForEach(comp => comp.Dispose());
            Clear();
        }

        public void InitializeAll()
        {
            ForEach(comp => comp.Initialize());
        }

        public void ShowAll()
        {
            ForEach(comp => comp.Show());
        }

        public void HideAll()
        {
            ForEach(comp => comp.Hide());
        }
    }
}
