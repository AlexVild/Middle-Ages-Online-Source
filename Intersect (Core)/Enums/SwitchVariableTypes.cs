using Intersect.Attributes;

namespace Intersect.Enums
{

    public enum VariableDataTypes : byte
    {

        Boolean = 1,

        Integer,

        String,

        Number

    }

    public enum VariableTypes
    {
        [EnhancedRelatedTable(GameObjectType.PlayerVariable)]
        PlayerVariable = 0,

        [EnhancedRelatedTable(GameObjectType.ServerVariable)]
        ServerVariable,

        [EnhancedRelatedTable(GameObjectType.InstanceVariable)]
        InstanceVariable,

        [EnhancedRelatedTable(GameObjectType.GuildVariable)]
        GuildVariable

    }

    //Should properly seperate static value, player & global vars into a seperate enum.
    //But technical debt :/
    public enum VariableMods
    {

        Set = 0,

        Add,

        Subtract,

        Random,

        SystemTime,

        DupPlayerVar,

        DupGlobalVar,

        AddPlayerVar,

        AddGlobalVar,

        SubtractPlayerVar,

        SubtractGlobalVar,

        Replace,

        Multiply,

        MultiplyPlayerVar,

        MultiplyGlobalVar,

        Divide,

        DividePlayerVar,

        DivideGlobalVar,

        LeftShift,

        LeftShiftPlayerVar,

        LeftShiftGlobalVar,

        RightShift,

        RightShiftPlayerVar,

        RightShiftGlobalVar,

        DupInstanceVar,

        AddInstanceVar,

        SubtractInstanceVar,
        
        MultiplyInstanceVar,

        DivideInstanceVar,

        LeftShiftInstanceVar,

        RightShiftInstanceVar,

        PlayerX,

        PlayerY,

        PlayerLevel,

        EventX,

        EventY,

        SpawnGroup,

        OpenSlots
    }

    public enum VariableComparators
    {

        Equal = 0,

        GreaterOrEqual,

        LesserOrEqual,

        Greater,

        Less,

        NotEqual

    }

    public enum StringVariableComparators
    {

        Equal = 0,

        Contains,

    }

}
