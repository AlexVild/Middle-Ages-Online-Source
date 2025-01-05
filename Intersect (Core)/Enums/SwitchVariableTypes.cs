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

        [RelatedVariableType(VariableTypes.PlayerVariable)]
        DupPlayerVar,

        [RelatedVariableType(VariableTypes.ServerVariable)]
        DupGlobalVar,

        [RelatedVariableType(VariableTypes.PlayerVariable)]
        AddPlayerVar,

        [RelatedVariableType(VariableTypes.ServerVariable)]
        AddGlobalVar,

        [RelatedVariableType(VariableTypes.PlayerVariable)]
        SubtractPlayerVar,

        [RelatedVariableType(VariableTypes.ServerVariable)]
        SubtractGlobalVar,

        Replace,

        Multiply,

        [RelatedVariableType(VariableTypes.PlayerVariable)]
        MultiplyPlayerVar,

        [RelatedVariableType(VariableTypes.ServerVariable)]
        MultiplyGlobalVar,

        Divide,

        [RelatedVariableType(VariableTypes.PlayerVariable)]
        DividePlayerVar,

        [RelatedVariableType(VariableTypes.ServerVariable)]
        DivideGlobalVar,

        LeftShift,

        [RelatedVariableType(VariableTypes.PlayerVariable)]
        LeftShiftPlayerVar,

        [RelatedVariableType(VariableTypes.ServerVariable)]
        LeftShiftGlobalVar,

        RightShift,

        [RelatedVariableType(VariableTypes.PlayerVariable)]
        RightShiftPlayerVar,

        [RelatedVariableType(VariableTypes.ServerVariable)]
        RightShiftGlobalVar,

        [RelatedVariableType(VariableTypes.InstanceVariable)]
        DupInstanceVar,

        [RelatedVariableType(VariableTypes.InstanceVariable)]
        AddInstanceVar,

        [RelatedVariableType(VariableTypes.InstanceVariable)]
        SubtractInstanceVar,

        [RelatedVariableType(VariableTypes.InstanceVariable)]
        MultiplyInstanceVar,

        [RelatedVariableType(VariableTypes.InstanceVariable)]
        DivideInstanceVar,

        [RelatedVariableType(VariableTypes.InstanceVariable)]
        LeftShiftInstanceVar,

        [RelatedVariableType(VariableTypes.InstanceVariable)]
        RightShiftInstanceVar,

        PlayerX,

        PlayerY,

        PlayerLevel,

        EventX,

        EventY,

        SpawnGroup,

        OpenSlots,

        [RelatedVariableType(VariableTypes.GuildVariable)]
        DupGuildVar,

        [RelatedVariableType(VariableTypes.GuildVariable)]
        AddGuildVar,

        [RelatedVariableType(VariableTypes.GuildVariable)]
        SubtractGuildVar,

        [RelatedVariableType(VariableTypes.GuildVariable)]
        MultiplyGuildVar,

        [RelatedVariableType(VariableTypes.GuildVariable)]
        DivideGuildVar,

        [RelatedVariableType(VariableTypes.GuildVariable)]
        LeftShiftGuildVar,

        [RelatedVariableType(VariableTypes.GuildVariable)]
        RightShiftGuildVar,

        [RelatedVariableType(VariableTypes.PlayerVariable)]
        ModPlayerVar,

        [RelatedVariableType(VariableTypes.ServerVariable)]
        ModServerVar,

        [RelatedVariableType(VariableTypes.InstanceVariable)]
        ModInstanceVar,

        [RelatedVariableType(VariableTypes.GuildVariable)]
        ModGuildVar,

        Mod,
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
