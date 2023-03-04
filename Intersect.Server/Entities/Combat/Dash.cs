using Intersect.Enums;
using Intersect.GameObjects;
using Intersect.Server.General;
using Intersect.Server.Maps;
using Intersect.Server.Networking;
using Intersect.Utilities;

namespace Intersect.Server.Entities.Combat
{

    public partial class Dash
    {

        public byte Direction;

        public int DistanceTraveled;

        public byte Facing;

        public int Range;

        public long TransmissionTimer;

        public SpellBase Spell;

        public Dash(
            Entity en,
            int range,
            byte direction,
            bool blockPass = false,
            bool activeResourcePass = false,
            bool deadResourcePass = false,
            bool zdimensionPass = false,
            bool entityPass = false,
            SpellBase spell = null,
            long stunMs = 0
        )
        {
            DistanceTraveled = 0;
            Direction = direction;
            Facing = (byte) en.Dir;
            Spell = spell;

            CalculateRange(en, range, blockPass, activeResourcePass, deadResourcePass, zdimensionPass, entityPass);
            if (Range <= 0)
            {
                return;
            } //Remove dash instance if no where to dash

            TransmissionTimer = Timing.Global.Milliseconds + (long) ((float) Options.MaxDashSpeed / (float) Range);
            en.DashTransmissionTimer = TransmissionTimer + stunMs;
            PacketSender.SendEntityDash(
                en, en.MapId, (byte) en.X, (byte) en.Y, (int) (Options.MaxDashSpeed * (Range / 10f)),
                Direction == Facing ? (sbyte) Direction : (sbyte) -1
            );

            en.MoveTimer = Timing.Global.Milliseconds + Options.MaxDashSpeed;
        }

        public void CalculateRange(
            Entity en,
            int range,
            bool blockPass = false,
            bool activeResourcePass = false,
            bool deadResourcePass = false,
            bool zdimensionPass = false,
            bool entityPass = false
        )
        {
            var n = 0;
            en.MoveTimer = 0;
            Range = 0;
            for (var i = 1; i <= range; i++)
            {
                n = en.CanMove(Direction);
                if (n == -5) //Check for out of bounds
                {
                    return;
                } //Check for blocks

                if (n == -2 && blockPass == false)
                {
                    return;
                } //Check for ZDimensionTiles

                if (n == -3 && zdimensionPass == false)
                {
                    return;
                } //Check for active resources

                if (n == (int) EntityTypes.Resource && activeResourcePass == false)
                {
                    return;
                } //Check for dead resources

                if (n == (int) EntityTypes.Resource && deadResourcePass == false)
                {
                    return;
                } //Check for players and solid events

                if (n == (int) EntityTypes.Player && (entityPass == false || i == range))
                {
                    var xOffset = 0;
                    var yOffset = 0;

                    var tile = new TileHelper(en.MapId, en.X, en.Y);
                    switch (Direction)
                    {
                        case 0: //Up
                            yOffset--;

                            break;
                        case 1: //Down
                            yOffset++;

                            break;
                        case 2: //Left
                            xOffset--;

                            break;
                        case 3: //Right
                            xOffset++;

                            break;
                        case 4: //NW
                            yOffset--;
                            xOffset--;

                            break;
                        case 5: //NE
                            yOffset--;
                            xOffset++;

                            break;
                        case 6: //SW
                            yOffset++;
                            xOffset--;

                            break;
                        case 7: //SE
                            yOffset++;
                            xOffset++;

                            break;
                    }

                    if (tile.Translate(xOffset, yOffset))
                    {
                        var tileX = tile.GetX();
                        var tileY = tile.GetY();
                        var entities = en.GetEntitiesOnTile(tileX, tileY);
                        foreach (var collidedEntity in entities)
                        {
                            if (en.CanAttack(collidedEntity, Spell) && en is AttackingEntity attacker)
                            {
                                attacker.UseSpell(Spell, -1, attacker.Target, true, instantCast: true);
                            }
                        }
                    }
                    return;
                }
                // Proc dash spell if an enemy
                else if (n == (int) EntityTypes.Player && Spell != null)
                {
                    var xOffset = 0;
                    var yOffset = 0;

                    var tile = new TileHelper(en.MapId, en.X, en.Y);
                    switch (Direction)
                    {
                        case 0: //Up
                            yOffset--;

                            break;
                        case 1: //Down
                            yOffset++;

                            break;
                        case 2: //Left
                            xOffset--;

                            break;
                        case 3: //Right
                            xOffset++;

                            break;
                        case 4: //NW
                            yOffset--;
                            xOffset--;

                            break;
                        case 5: //NE
                            yOffset--;
                            xOffset++;

                            break;
                        case 6: //SW
                            yOffset++;
                            xOffset--;

                            break;
                        case 7: //SE
                            yOffset++;
                            xOffset++;

                            break;
                    }

                    MapController mapController = null;
                    int tileX = 0;
                    int tileY = 0;

                    if (tile.Translate(xOffset, yOffset))
                    {
                        mapController = MapController.Get(tile.GetMapId());
                        tileX = tile.GetX();
                        tileY = tile.GetY();

                        var entities = en.GetEntitiesOnTile(tileX, tileY);
                        foreach(var collidedEntity in entities)
                        {
                            if (en.CanAttack(collidedEntity, Spell) && en is AttackingEntity attacker)
                            {
                                attacker.UseSpell(Spell, -1, attacker.Target, true, instantCast: true);
                            }
                        }
                    }
                }

                if (n == (int) EntityTypes.Event)
                {
                    return;
                }

                en.Move(Direction, null, true);
                en.Dir = Facing;

                Range = i;
            }
        }

    }

}
