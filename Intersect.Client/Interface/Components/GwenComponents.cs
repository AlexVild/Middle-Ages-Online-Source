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
    }

    public class ComponentList<T> : List<T> where T : IGwenComponent
    {
        public void InitializeAll()
        {
            ForEach(comp => comp.Initialize());
        }
    }
}
