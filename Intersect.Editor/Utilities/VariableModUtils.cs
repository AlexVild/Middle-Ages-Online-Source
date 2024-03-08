using Intersect.Enums;
using System.Collections.Generic;

namespace Intersect.Editor.Utilities
{
    public static class VariableModUtils
    {

        public static readonly List<VariableMods> SetMods = new List<VariableMods>() { 
            VariableMods.Set,
            VariableMods.DupPlayerVar,
            VariableMods.DupGlobalVar,
            VariableMods.DupGuildVar
        };

        public static readonly List<VariableMods> AddMods = new List<VariableMods>() {
            VariableMods.Add,
            VariableMods.AddPlayerVar,
            VariableMods.AddGlobalVar,
            VariableMods.AddGuildVar,
        };

        public static readonly List<VariableMods> SubMods = new List<VariableMods>() {
            VariableMods.Subtract,
            VariableMods.SubtractPlayerVar,
            VariableMods.SubtractGlobalVar,
            VariableMods.SubtractGuildVar,
        };

        public static readonly List<VariableMods> MultMods = new List<VariableMods>() {
            VariableMods.Multiply,
            VariableMods.MultiplyPlayerVar,
            VariableMods.MultiplyGlobalVar,
            VariableMods.MultiplyGuildVar,
        };

        public static readonly List<VariableMods> DivideMods = new List<VariableMods>() {
            VariableMods.Divide,
            VariableMods.DividePlayerVar,
            VariableMods.DivideGlobalVar,
            VariableMods.DivideGuildVar,
        };

        public static readonly List<VariableMods> LShiftMods = new List<VariableMods>() {
            VariableMods.LeftShift,
            VariableMods.LeftShiftPlayerVar,
            VariableMods.LeftShiftGlobalVar,
            VariableMods.LeftShiftGuildVar,
        };

        public static readonly List<VariableMods> RShiftMods = new List<VariableMods>() {
            VariableMods.RightShift,
            VariableMods.RightShiftPlayerVar,
            VariableMods.RightShiftGlobalVar,
            VariableMods.RightShiftGuildVar,
        };

    }
}
