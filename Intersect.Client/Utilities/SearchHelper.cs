using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Client.Utilities
{
    public static class SearchHelper
    {
        /// <summary>
        /// Determines whether or not a text-field search should return some item if the text field
        /// is being used as a search function
        /// </summary>
        /// <param name="name">The item name we're searching for</param>
        /// <param name="searchTerm">The search term entered</param>
        /// <returns></returns>
        public static bool IsSearchable(string name, string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return true;
            }
            if (string.IsNullOrEmpty(name))
            {
                return false;
            }

            return name.StartsWith(searchTerm, StringComparison.CurrentCultureIgnoreCase)
                || name.EndsWith(searchTerm, StringComparison.CurrentCultureIgnoreCase)
                || (searchTerm.Length > 3 && name.ToLower().Contains(searchTerm.ToLower()));
        }
    }
}
