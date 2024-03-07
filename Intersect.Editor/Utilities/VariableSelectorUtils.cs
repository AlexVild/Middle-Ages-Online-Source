using Intersect.Editor.Forms;
using Intersect.Editor.Localization;
using Intersect.Enums;
using Intersect.Extensions;
using System;

namespace Intersect.Editor.Utilities
{
    public static class VariableSelectorUtils
    {
        public static void OpenVariableSelector(Action<VariableSelection> onSelectionComplete,
            Guid selectedVariableId,
            VariableTypes selectedVariableType,
            VariableDataTypes dataTypeFilter = 0)
        {
            var variableSelection = new FrmVariableSelector(selectedVariableType, selectedVariableId, dataTypeFilter);
            variableSelection.ShowDialog();

            if (variableSelection.GetResult())
            {
                var selection = variableSelection.GetSelection();

                onSelectionComplete.Invoke(selection);
            }
        }

        public static string GetSelectedVarText(VariableTypes variableType, Guid selectedVariableId)
        {
            Strings.VariableSelector.VariableTypes.TryGetValue((int)variableType, out var type);
            var varName = variableType.GetVariableTable().GetLookup().Get(selectedVariableId)?.Name;

            if (varName == default || selectedVariableId == Guid.Empty)
            {
                return Strings.VariableSelector.ValueNoneSelected.ToString();
            }

            return Strings.VariableSelector.ValueCurrentSelection.ToString(varName, type);
        }
    }
}
