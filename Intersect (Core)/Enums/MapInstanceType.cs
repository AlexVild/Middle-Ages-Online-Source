using System;

namespace Intersect.Enums
{
    /// <summary>
    /// Used to determine warping logic across the different types of map instance a developer may request the creation of
    /// </summary>
    public enum MapInstanceType
    {
        /// <summary>
        /// The overworld instance - the instance that is commonly shared amongst players, and where players are placed in default situations
        /// </summary>
        Overworld = 0,

        /// <summary>
        /// The type used whenever you want a player to be alone on their own map instance
        /// </summary>
        Personal,

        /// <summary>
        /// An instance that is shared amongst members of a guild
        /// </summary>
        Guild,

        /// <summary>
        /// The instance that is shared amongst members of a party and used for dungeoneering
        /// </summary>
        Shared,

        /// <summary>
        /// Same as personal, but allows players to warp to the party leader's instance
        /// </summary>
        Party,

        /// <summary>
        /// A personal instance where everything is aggro
        /// </summary>
        PersonalDungeon,

        /// <summary>
        /// Same as personalDungeon, but allows party members to join
        /// </summary>
        PartyDungeon,

        /// <summary>
        /// An instance for the Clan Wars minigame, using the idea of the current clan war
        /// </summary>
        ClanWar,

        /// <summary>
        /// ALWAYS PLACE AT END OF ENUM. This value is used for logical reference and should never appear to the user in the editor.
        /// </summary>
        NoChange,
    }
}
