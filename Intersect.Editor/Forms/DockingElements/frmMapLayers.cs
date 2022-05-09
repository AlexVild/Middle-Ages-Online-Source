using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Intersect.Config;
using Intersect.Editor.Content;
using Intersect.Editor.General;
using Intersect.Editor.Localization;
using Intersect.Enums;
using Intersect.GameObjects;
using Intersect.GameObjects.Events;
using Intersect.GameObjects.Maps;
using Intersect.GameObjects.Maps.MapList;
using Intersect.Localization;
using Intersect.Utilities;

using Microsoft.Xna.Framework.Graphics;

using WeifenLuo.WinFormsUI.Docking;

namespace Intersect.Editor.Forms.DockingElements
{

    public enum LayerTabs
    {

        Tiles = 0,

        Attributes,

        Lights,

        Events,

        Npcs

    }

    public partial class FrmMapLayers : DockContent
    {

        public LayerTabs CurrentTab = LayerTabs.Tiles;

        public Dictionary<string, bool> LayerVisibility = new Dictionary<string,bool>();

        //MonoGame Swap Chain
        private SwapChainRenderTarget mChain;

        private string mLastTileLayer;

        private List<PictureBox> mMapLayers = new List<PictureBox>();

        private bool mTMouseDown;

        private bool mLoading = false;

        public FrmMapLayers()
        {
            InitializeComponent();
            mMapLayers.Add(picLayer1);
            mMapLayers.Add(picLayer2);
            mMapLayers.Add(picLayer3);
            mMapLayers.Add(picLayer4);
            mMapLayers.Add(picLayer5);

            this.Icon = Properties.Resources.Icon;
        }

        public void Init()
        {
            cmbAutotile.SelectedIndex = 0;

            //See if we can use the old style icons instead of a combobox
            if (Options.Instance.MapOpts.Layers.All.Count <= mMapLayers.Count)
            {
                //Hide combobox...
                cmbMapLayer.Hide();
                for (int i = 0; i < mMapLayers.Count; i++)
                {
                    if (i < Options.Instance.MapOpts.Layers.All.Count)
                    {
                        Strings.Tiles.maplayers.TryGetValue(Options.Instance.MapOpts.Layers.All[i].ToLower(), out LocalizedString layerName);
                        if (layerName == null) layerName = Options.Instance.MapOpts.Layers.All[i];
                        mMapLayers[i].Text = layerName;
                        mMapLayers[i].Show();
                    }
                    else
                    {
                        mMapLayers[i].Hide();
                    }
                }
            }
            else
            {
                foreach(var layer in mMapLayers)
                {
                    layer.Hide();
                }
                //Show Combobox
                cmbMapLayer.Show();
                cmbMapLayer.Items.AddRange(Options.Instance.MapOpts.Layers.All.ToArray());
                cmbMapLayer.SelectedIndex = 0;
            }

            foreach (var layer in Options.Instance.MapOpts.Layers.All)
            {
                LayerVisibility.Add(layer, true);
            }

            SetLayer(Options.Instance.MapOpts.Layers.All[0]);
            if (cmbTilesets.Items.Count > 0)
            {
                SetTileset(cmbTilesets.Items[0].ToString());
            }

            rbZDimension.Visible = Options.ZDimensionVisible;
            grpZResource.Visible = Options.ZDimensionVisible;
            grpInstanceSettings.Visible = chkChangeInstance.Checked;

            cmbInstanceType.Items.Clear();
            // We do not want to iterate over the "NoChange" enum - so we subtract 1 from the iterating maximum
            for (var i = 0; i < Enum.GetNames(typeof(MapInstanceType)).Length - 1; i++)
            {
                cmbInstanceType.Items.Add(Enum.GetName(typeof(MapInstanceType), i));
            }

            InitializeConditionVariables();
        }

        //Tiles Tab
        private void cmbTilesets_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetTileset(cmbTilesets.Text);
        }

        private void picTileset_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.X > picTileset.Width || e.Y > picTileset.Height)
            {
                return;
            }

            mTMouseDown = true;
            Globals.CurSelX = (int) Math.Floor((double) e.X / Options.TileWidth);
            Globals.CurSelY = (int) Math.Floor((double) e.Y / Options.TileHeight);
            Globals.CurSelW = 0;
            Globals.CurSelH = 0;
            if (Globals.CurSelX < 0)
            {
                Globals.CurSelX = 0;
            }

            if (Globals.CurSelY < 0)
            {
                Globals.CurSelY = 0;
            }

            switch (Globals.Autotilemode)
            {
                case 1:
                case 5:
                    Globals.CurSelW = 1;
                    Globals.CurSelH = 2;

                    break;
                case 2:
                    Globals.CurSelW = 0;
                    Globals.CurSelH = 0;

                    break;
                case 3:
                    Globals.CurSelW = 5;
                    Globals.CurSelH = 2;

                    break;
                case 4:
                    Globals.CurSelW = 1;
                    Globals.CurSelH = 1;

                    break;
                case 6:
                    Globals.CurSelW = 2;
                    Globals.CurSelH = 3;

                    break;
                case 7:
                    Globals.CurSelW = 8;
                    Globals.CurSelH = 3;

                    break;
            }
        }

        private void picTileset_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.X > picTileset.Width || e.Y > picTileset.Height)
            {
                return;
            }

            if (mTMouseDown && Globals.Autotilemode == 0)
            {
                var tmpX = (int) Math.Floor((double) e.X / Options.TileWidth);
                var tmpY = (int) Math.Floor((double) e.Y / Options.TileHeight);
                Globals.CurSelW = tmpX - Globals.CurSelX;
                Globals.CurSelH = tmpY - Globals.CurSelY;
            }
        }

        private void picTileset_MouseUp(object sender, MouseEventArgs e)
        {
            var selX = Globals.CurSelX;
            var selY = Globals.CurSelY;
            var selW = Globals.CurSelW;
            var selH = Globals.CurSelH;
            if (selW < 0)
            {
                selX -= Math.Abs(selW);
                selW = Math.Abs(selW);
            }

            if (selH < 0)
            {
                selY -= Math.Abs(selH);
                selH = Math.Abs(selH);
            }

            Globals.CurSelX = selX;
            Globals.CurSelY = selY;
            Globals.CurSelW = selW;
            Globals.CurSelH = selH;
            mTMouseDown = false;
            Globals.MapEditorWindow.DockPanel.Focus();
        }

        private void cmbAutotile_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetAutoTile(cmbAutotile.SelectedIndex);
        }

        public void SetAutoTile(int index)
        {
            Globals.Autotilemode = index;
            cmbAutotile.SelectedIndex = index;
            switch (Globals.Autotilemode)
            {
                case 1:
                case 5:
                    Globals.CurSelW = 1;
                    Globals.CurSelH = 2;

                    break;
                case 2:
                    Globals.CurSelW = 0;
                    Globals.CurSelH = 0;

                    break;
                case 3:
                    Globals.CurSelW = 5;
                    Globals.CurSelH = 2;

                    break;
                case 4:
                    Globals.CurSelW = 1;
                    Globals.CurSelH = 1;

                    break;
                case 6:
                    Globals.CurSelW = 2;
                    Globals.CurSelH = 3;

                    break;
                case 7:
                    Globals.CurSelW = 8;
                    Globals.CurSelH = 3;

                    break;
            }
        }

        public void InitTilesets()
        {
            Globals.MapLayersWindow.cmbTilesets.Items.Clear();
            var tilesetList = new List<string>();
            tilesetList.AddRange(TilesetBase.Names);
            tilesetList.Sort(new AlphanumComparatorFast());
            foreach (var filename in tilesetList)
            {
                if (File.Exists("resources/tilesets/" + filename))
                {
                    Globals.MapLayersWindow.cmbTilesets.Items.Add(filename);
                }
                else
                {
                }
            }

            if (TilesetBase.Lookup.Count > 0)
            {
                if (Globals.MapLayersWindow.cmbTilesets.Items.Count > 0)
                {
                    Globals.MapLayersWindow.cmbTilesets.SelectedIndex = 0;
                }

                Globals.CurrentTileset = (TilesetBase) TilesetBase.Lookup.Values.ToArray()[0];
            }
        }

        public void SetTileset(string name)
        {
            TilesetBase tSet = null;
            var tilesets = TilesetBase.Lookup;
            var id = Guid.Empty;
            foreach (var tileset in tilesets.Pairs)
            {
                if (tileset.Value.Name.ToLower() == name.ToLower())
                {
                    id = tileset.Key;

                    break;
                }
            }

            if (id != Guid.Empty)
            {
                tSet = TilesetBase.Get(id);
            }

            if (tSet != null)
            {
                if (File.Exists("resources/tilesets/" + tSet.Name))
                {
                    picTileset.Show();
                    Globals.CurrentTileset = tSet;
                    Globals.CurSelX = 0;
                    Globals.CurSelY = 0;
                    var tilesetTex = GameContentManager.GetTexture(GameContentManager.TextureType.Tileset, tSet.Name);
                    if (tilesetTex != null)
                    {
                        picTileset.Width = tilesetTex.Width;
                        picTileset.Height = tilesetTex.Height;
                    }

                    cmbTilesets.SelectedItem = name;
                    CreateSwapChain();
                }
            }
        }

        public void SetLayer(string name)
        {
            Globals.CurrentLayer = name;

            var index = Options.Instance.MapOpts.Layers.All.IndexOf(name);

            if (!cmbMapLayer.Visible)
            {
                for (var i = 0; i < mMapLayers.Count; i++)
                {
                    if (mMapLayers[i].BackgroundImage != null)
                    {
                        mMapLayers[i].BackgroundImage.Dispose();
                        mMapLayers[i].BackgroundImage = null;
                    }
                    mMapLayers[i].BackgroundImage = DrawLayerImage(i, i == index, !LayerVisibility[Options.Instance.MapOpts.Layers.All[i]]);
                }
            }
            else
            {
                if (cmbMapLayer.Items.IndexOf(name) > -1)
                {
                    cmbMapLayer.SelectedIndex = cmbMapLayer.Items.IndexOf(name);
                }
            }

            mLastTileLayer = name;

            Core.Graphics.TilePreviewUpdated = true;
        }

        private Bitmap DrawLayerImage(int layerIndex, bool selected, bool hidden)
        {
            var img = new Bitmap(32, 32);
            img.MakeTransparent(img.GetPixel(0, 0));

            var g = Graphics.FromImage(img);

            var layer = (Bitmap)Properties.Resources.ResourceManager.GetObject("layer");
            var layerSel = (Bitmap)Properties.Resources.ResourceManager.GetObject("layer_sel");
            var face = (Bitmap)Properties.Resources.ResourceManager.GetObject("layer_face");
            var faceSel = (Bitmap)Properties.Resources.ResourceManager.GetObject("layer_face_sel");
            var hiddenIcon = (Bitmap)Properties.Resources.ResourceManager.GetObject("layer_hidden");
            var drawFace = selected ? faceSel : face;

            var drawIndex = 0;

            //Draw Lower & Middle Layers
            foreach (var l in Options.Instance.MapOpts.Layers.LowerLayers)
            {
                var drawImg = layer;
                if (drawIndex == layerIndex)
                {
                    drawImg = layerSel;
                }
                g.DrawImage(drawImg, new PointF(3, 23 - ((drawIndex) * (layer.Height - 4))));
                drawIndex++;
            }


            //If this image for is an upper layer, render the face below the next layers
            if (!Options.Instance.MapOpts.Layers.LowerLayers.Contains(Options.Instance.MapOpts.Layers.All[layerIndex]))
            {
                g.DrawImage(drawFace, new PointF(13, 13));
            }


            //Draw Upper Layers
            var middleUpperLayers = Options.Instance.MapOpts.Layers.LowerLayers.ToList();
            middleUpperLayers.AddRange(Options.Instance.MapOpts.Layers.MiddleLayers);
            foreach (var l in middleUpperLayers)
            {
                var drawImg = layer;
                if (drawIndex == layerIndex)
                {
                    drawImg = layerSel;
                }
                g.DrawImage(drawImg, new PointF(3, 23 - ((drawIndex) * (layer.Height - 4))));
                drawIndex++;
            }

            //If this image for is a lower layer, render the face above everything
            if (Options.Instance.MapOpts.Layers.LowerLayers.Contains(Options.Instance.MapOpts.Layers.All[layerIndex]))
            {
                g.DrawImage(drawFace, new PointF(13, 13));
            }


            //Draw Hidden Icon
            if (hidden)
            {
                g.DrawImage(hiddenIcon, new PointF(32 - hiddenIcon.Width, 0));
            }

            g.Dispose();
            return img;
        }

        //Mapping Attribute Functions
        /// <summary>
        ///     A method that hides all of the extra group boxes for tile data related to the map attributes.
        /// </summary>
        private void HideAttributeMenus()
        {
            grpBlock.Visible = false;
            grpItem.Visible = false;
            grpZDimension.Visible = false;
            grpWarp.Visible = false;
            grpSound.Visible = false;
            grpResource.Visible = false;
            grpAnimation.Visible = false;
            grpSlide.Visible = false;
            grpCritter.Visible = false;
        }

        private void rbItem_CheckedChanged(object sender, EventArgs e)
        {
            HideAttributeMenus();
            grpItem.Visible = true;
            cmbItemAttribute.Items.Clear();
            cmbItemAttribute.Items.AddRange(ItemBase.Names);
            if (cmbItemAttribute.Items.Count > 0)
            {
                cmbItemAttribute.SelectedIndex = 0;
            }
        }

        private void rbBlocked_CheckedChanged(object sender, EventArgs e)
        {
            HideAttributeMenus();
            grpBlock.Visible = true;
            chkGroundLevel.Checked = false;
        }

        private void rbNPCAvoid_CheckedChanged(object sender, EventArgs e)
        {
            HideAttributeMenus();
        }

        private void rbZDimension_CheckedChanged(object sender, EventArgs e)
        {
            HideAttributeMenus();
            grpZDimension.Visible = true;
        }

        private void rbWarp_CheckedChanged(object sender, EventArgs e)
        {
            nudWarpX.Maximum = Options.MapWidth;
            nudWarpY.Maximum = Options.MapHeight;
            cmbWarpMap.Items.Clear();
            for (var i = 0; i < MapList.OrderedMaps.Count; i++)
            {
                cmbWarpMap.Items.Add(MapList.OrderedMaps[i].Name);
            }

            cmbWarpMap.SelectedIndex = 0;
            cmbDirection.SelectedIndex = 0;
            if (!rbWarp.Checked)
            {
                return;
            }

            HideAttributeMenus();
            grpWarp.Visible = true;
        }

        private void rbSound_CheckedChanged(object sender, EventArgs e)
        {
            HideAttributeMenus();
            grpSound.Visible = true;
            cmbMapAttributeSound.Items.Clear();
            cmbMapAttributeSound.Items.Add(Strings.General.none);
            cmbMapAttributeSound.Items.AddRange(GameContentManager.SmartSortedSoundNames);
            cmbMapAttributeSound.SelectedIndex = 0;
        }

        private void rbResource_CheckedChanged(object sender, EventArgs e)
        {
            cmbResourceAttribute.Items.Clear();
            cmbResourceAttribute.Items.AddRange(ResourceBase.Names);
            if (cmbResourceAttribute.Items.Count > 0)
            {
                cmbResourceAttribute.SelectedIndex = 0;
            }

            if (!rbResource.Checked)
            {
                return;
            }

            HideAttributeMenus();
            grpResource.Visible = true;
        }

        // Used for returning an integer value depending on which radio button is selected on the forms. This is merely used to make PlaceAtrribute less messy.
        private byte GetEditorDimensionGateway()
        {
            if (rbGateway1.Checked == true)
            {
                return 1;
            }
            else if (rbGateway2.Checked == true)
            {
                return 2;
            }

            return 0;
        }

        private byte GetEditorDimensionBlock()
        {
            if (rbBlock1.Checked == true)
            {
                return 1;
            }
            else if (rbBlock2.Checked == true)
            {
                return 2;
            }

            return 0;
        }

        public int GetAttributeFromEditor()
        {
            if (rbBlocked.Checked == true)
            {
                return (int) MapAttributes.Blocked;
            }
            else if (rbItem.Checked == true)
            {
                return (int) MapAttributes.Item;
            }
            else if (rbZDimension.Checked == true)
            {
                return (int) MapAttributes.ZDimension;
            }
            else if (rbNPCAvoid.Checked == true)
            {
                return (int) MapAttributes.NpcAvoid;
            }
            else if (rbWarp.Checked == true)
            {
                return (int) MapAttributes.Warp;
            }
            else if (rbSound.Checked == true)
            {
                return (int) MapAttributes.Sound;
            }
            else if (rbResource.Checked == true)
            {
                return (int) MapAttributes.Resource;
            }
            else if (rbAnimation.Checked == true)
            {
                return (int) MapAttributes.Animation;
            }
            else if (rbGrappleStone.Checked == true)
            {
                return (int) MapAttributes.GrappleStone;
            }
            else if (rbSlide.Checked == true)
            {
                return (int) MapAttributes.Slide;
            }
            else if (rbCritter.Checked == true)
            {
                return (int) MapAttributes.Critter;
            }

            return (int) MapAttributes.Walkable;
        }

        private MapAttributes SelectedMapAttributeType
        {
            get
            {
                if (rbBlocked.Checked)
                {
                    return MapAttributes.Blocked;
                }
                
                if (rbItem.Checked)
                {
                    return MapAttributes.Item;
                }

                if (rbZDimension.Checked)
                {
                    return MapAttributes.ZDimension;
                }

                if (rbNPCAvoid.Checked)
                {
                    return MapAttributes.NpcAvoid;
                }

                if (rbWarp.Checked)
                {
                    return MapAttributes.Warp;
                }

                if (rbSound.Checked)
                {
                    return MapAttributes.Sound;
                }

                if (rbResource.Checked)
                {
                    return MapAttributes.Resource;
                }

                if (rbAnimation.Checked)
                {
                    return MapAttributes.Animation;
                }

                if (rbGrappleStone.Checked)
                {
                    return MapAttributes.GrappleStone;
                }

                if (rbSlide.Checked)
                {
                    return MapAttributes.Slide;
                }

                if (rbCritter.Checked)
                {
                    return MapAttributes.Critter;
                }

                return (MapAttributes) byte.MaxValue;
            }
        }

        [Obsolete("The entire switch statement should be implemented as a parameterized CreateAttribute().")]
        public MapAttribute CreateAttribute()
        {
            var attributeType = SelectedMapAttributeType;
            var attribute = MapAttribute.CreateAttribute(attributeType);
            switch (SelectedMapAttributeType)
            {
                case MapAttributes.Walkable:
                case MapAttributes.GrappleStone:
                case MapAttributes.NpcAvoid:
                    break;
                case MapAttributes.Blocked:
                    var blockAttribute = attribute as MapBlockedAttribute;
                    blockAttribute.GroundLevel = chkGroundLevel.Checked;
                    break;

                case MapAttributes.Item:
                    var itemAttribute = attribute as MapItemAttribute;
                    itemAttribute.ItemId = ItemBase.IdFromList(cmbItemAttribute.SelectedIndex);
                    itemAttribute.Quantity = (int)nudItemQuantity.Value;
                    break;

                case MapAttributes.ZDimension:
                    var zDimensionAttribute = attribute as MapZDimensionAttribute;
                    zDimensionAttribute.GatewayTo = GetEditorDimensionGateway();
                    zDimensionAttribute.BlockedLevel = GetEditorDimensionBlock();
                    break;

                case MapAttributes.Warp:
                    var warpAttribute = attribute as MapWarpAttribute;
                    warpAttribute.MapId = MapList.OrderedMaps[cmbWarpMap.SelectedIndex].MapId;
                    warpAttribute.X = (byte)nudWarpX.Value;
                    warpAttribute.Y = (byte)nudWarpY.Value;
                    warpAttribute.Direction = (WarpDirection)cmbDirection.SelectedIndex;
                    warpAttribute.FadeOnWarp = chkMapFade.Checked;
                    warpAttribute.ChangeInstance = chkChangeInstance.Checked;
                    warpAttribute.InstanceType = (MapInstanceType)cmbInstanceType.SelectedIndex;
                    break;

                case MapAttributes.Sound:
                    var soundAttribute = attribute as MapSoundAttribute;
                    soundAttribute.Distance = (byte)nudSoundDistance.Value;
                    soundAttribute.File = TextUtils.SanitizeNone(cmbMapAttributeSound.Text);
                    soundAttribute.LoopInterval = (int)nudSoundLoopInterval.Value;
                    break;

                case MapAttributes.Resource:
                    var resourceAttribute = attribute as MapResourceAttribute;
                    resourceAttribute.ResourceId = ResourceBase.IdFromList(cmbResourceAttribute.SelectedIndex);
                    resourceAttribute.SpawnLevel = (byte)(rbLevel1.Checked ? 0 : 1);
                    break;

                case MapAttributes.Animation:
                    var animationAttribute = attribute as MapAnimationAttribute;
                    animationAttribute.AnimationId = AnimationBase.IdFromList(cmbAnimationAttribute.SelectedIndex);
                    animationAttribute.IsBlock = chkAnimationBlock.Checked;
                    break;

                case MapAttributes.Slide:
                    var slideAttribute = attribute as MapSlideAttribute;
                    slideAttribute.Direction = (byte)cmbSlideDir.SelectedIndex;
                    break;

                case MapAttributes.Critter:
                    var critterAttribute = attribute as MapCritterAttribute;
                    critterAttribute.Sprite = cmbCritterSprite.Text;
                    critterAttribute.AnimationId = AnimationBase.IdFromList(cmbCritterAnimation.SelectedIndex - 1);
                    critterAttribute.Movement = (byte)cmbCritterMovement.SelectedIndex;
                    critterAttribute.Layer = (byte)cmbCritterLayer.SelectedIndex;
                    critterAttribute.Speed = (int)nudCritterMoveSpeed.Value;
                    critterAttribute.Frequency = (int)nudCritterMoveFrequency.Value;
                    critterAttribute.IgnoreNpcAvoids = chkCritterIgnoreNpcAvoids.Checked;
                    critterAttribute.BlockPlayers = chkCritterBlockPlayers.Checked;
                    critterAttribute.Direction = (byte)cmbCritterDirection.SelectedIndex;
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(SelectedMapAttributeType), @"The currently selected attribute type has not been fully implemented.");
            }

            return attribute;
        }

        public MapAttribute PlaceAttribute(MapBase mapDescriptor, int x, int y, MapAttribute attribute = null)
        {
            if (attribute == null)
            {
                attribute = CreateAttribute();
            }

            mapDescriptor.Attributes[x, y] = attribute;

            return attribute;
        }

        public bool RemoveAttribute(MapBase tmpMap, int x, int y)
        {
            if (tmpMap.Attributes[x, y] != null && tmpMap.Attributes[x, y].Type != MapAttributes.Walkable)
            {
                tmpMap.Attributes[x, y] = null;

                return true;
            }

            return false;
        }

        public void RefreshNpcList()
        {
            // Update the list incase npcs have been modified since form load.
            cmbNpc.Items.Clear();
            cmbNpc.Items.AddRange(NpcBase.Names);
            InitializeConditionVariables();
            grpVariable.Hide();

            // Add the map NPCs
            lstMapNpcs.Items.Clear();
            for (var i = 0; i < Globals.CurrentMap.Spawns.Count; i++)
            {
                lstMapNpcs.Items.Add(NpcBase.GetName(Globals.CurrentMap.Spawns[i].NpcId));
            }

            // Don't select if there are no NPCs, to avoid crashes.
            if (cmbNpc.Items.Count > 0)
            {
                cmbNpc.SelectedIndex = 0;
            }

            cmbDir.SelectedIndex = 0;
            rbRandom.Checked = true;
            if (lstMapNpcs.Items.Count > 0)
            {
                lstMapNpcs.SelectedIndex = 0;
                if (lstMapNpcs.SelectedIndex < Globals.CurrentMap.Spawns.Count)
                {
                    var selectedSpawn = Globals.CurrentMap.Spawns[lstMapNpcs.SelectedIndex];
                    cmbDir.SelectedIndex = (int) selectedSpawn.Direction;
                    cmbNpc.SelectedIndex = NpcBase.ListIndex(selectedSpawn.NpcId);
                    if (selectedSpawn.X >= 0)
                    {
                        rbDeclared.Checked = true;
                    }
                    RefreshSpawnConditions();
                }
            }
        }

        #region NPC Spawn Variable Editor

        private void InitializeConditionVariables()
        {
            cmbCompareGlobalVar.Items.Clear();
            cmbCompareGlobalVar.Items.AddRange(ServerVariableBase.Names);
            cmbCompareInstanceVar.Items.Clear();
            cmbCompareInstanceVar.Items.AddRange(InstanceVariableBase.Names);

            cmbBooleanGlobalVariable.Items.Clear();
            cmbBooleanGlobalVariable.Items.AddRange(ServerVariableBase.Names);
            cmbBooleanInstanceVariable.Items.Clear();
            cmbBooleanInstanceVariable.Items.AddRange(InstanceVariableBase.Names);
        }

        private void SetupSpawnConditionFormValues()
        {
            if (lstMapNpcs.Items.Count <= 0 || lstMapNpcs.SelectedIndex < 0)
            {
                return;
            }

            var selectedSpawn = Globals.CurrentMap.Spawns[lstMapNpcs.SelectedIndex];
            if (selectedSpawn.SpawnVariableType == VariableTypes.ServerVariable)
            {
                rdoGlobalVariable.Checked = true;
            }
            else if (selectedSpawn.SpawnVariableType == VariableTypes.InstanceVariable)
            {
                rdoInstanceVariable.Checked = true;
            }

            InitVariableElements(selectedSpawn.SpawnVariableId);

            UpdateVariableElements();
        }

        private void UpdateVariableElements()
        {
            var selectedSpawn = Globals.CurrentMap.Spawns[lstMapNpcs.SelectedIndex];
            //Hide editor windows until we have a variable selected to work with
            grpNumericVariable.Hide();
            grpBooleanVariable.Hide();
            grpStringVariable.Hide();

            var varType = 0;
            if (cmbVariable.SelectedIndex > -1)
            {
                //Determine Variable Type
                if (rdoGlobalVariable.Checked)
                {
                    var serverVar = ServerVariableBase.FromList(cmbVariable.SelectedIndex);
                    if (serverVar != null)
                    {
                        varType = (byte)serverVar.Type;
                    }
                }
                else if (rdoInstanceVariable.Checked)
                {
                    var serverVar = InstanceVariableBase.FromList(cmbVariable.SelectedIndex);
                    if (serverVar != null)
                    {
                        varType = (byte)serverVar.Type;
                    }
                }
            }

            //Load the correct editor
            if (varType > 0)
            {
                switch ((VariableDataTypes)varType)
                {
                    case VariableDataTypes.Boolean:
                        grpBooleanVariable.Show();
                        TryLoadVariableBooleanComparison(selectedSpawn.BooleanSpawnComparison);

                        break;

                    case VariableDataTypes.Integer:
                        grpNumericVariable.Show();
                        TryLoadVariableIntegerComparison(selectedSpawn.IntegerSpawnComparison);
                        UpdateNumericVariableElements();

                        break;

                    case VariableDataTypes.Number:
                        break;

                    case VariableDataTypes.String:
                        grpStringVariable.Show();
                        TryLoadVariableStringComparison(selectedSpawn.StringSpawnComparison);

                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private void UpdateNumericVariableElements()
        {
            nudVariableValue.Enabled = rdoVarCompareStaticValue.Checked;
            cmbCompareGlobalVar.Enabled = rdoVarCompareGlobalVar.Checked;
            cmbCompareInstanceVar.Enabled = rdoVarCompareInstanceVar.Checked;
        }

        private void InitVariableElements(Guid variableId)
        {
            mLoading = true;
            cmbVariable.Items.Clear();
            if (rdoGlobalVariable.Checked)
            {
                cmbVariable.Items.AddRange(ServerVariableBase.Names);
                cmbVariable.SelectedIndex = ServerVariableBase.ListIndex(variableId);
            }
            else if (rdoInstanceVariable.Checked)
            {
                cmbVariable.Items.AddRange(InstanceVariableBase.Names);
                cmbVariable.SelectedIndex = InstanceVariableBase.ListIndex(variableId);
            }

            mLoading = false;
        }

        private void TryLoadVariableBooleanComparison(VariableCompaison comp)
        {
            if (comp != null && comp.GetType() == typeof(BooleanVariableComparison))
            {
                var com = (BooleanVariableComparison)comp;

                cmbBooleanComparator.SelectedIndex = Convert.ToInt32(!com.ComparingEqual);

                if (cmbBooleanComparator.SelectedIndex < 0)
                {
                    cmbBooleanComparator.SelectedIndex = 0;
                }

                optBooleanTrue.Checked = com.Value;
                optBooleanFalse.Checked = !com.Value;

                if (com.CompareVariableId != Guid.Empty)
                {
                    if (com.CompareVariableType == VariableTypes.ServerVariable)
                    {
                        optBooleanGlobalVariable.Checked = true;
                        cmbBooleanGlobalVariable.SelectedIndex = ServerVariableBase.ListIndex(com.CompareVariableId);
                    }
                    else if (com.CompareVariableType == VariableTypes.InstanceVariable)
                    {
                        optBooleanInstanceVariable.Checked = true;
                        cmbBooleanInstanceVariable.SelectedIndex = InstanceVariableBase.ListIndex(com.CompareVariableId);
                    }
                }
            }
        }

        private void TryLoadVariableIntegerComparison(VariableCompaison comp)
        {
            if (comp != null && comp.GetType() == typeof(IntegerVariableComparison))
            {
                var com = (IntegerVariableComparison)comp;

                cmbNumericComparitor.SelectedIndex = (int)com.Comparator;

                if (cmbNumericComparitor.SelectedIndex < 0)
                {
                    cmbNumericComparitor.SelectedIndex = 0;
                }

                if (com.CompareVariableId != Guid.Empty)
                {
                    if (com.CompareVariableType == VariableTypes.ServerVariable)
                    {
                        rdoVarCompareGlobalVar.Checked = true;
                        cmbCompareGlobalVar.SelectedIndex = ServerVariableBase.ListIndex(com.CompareVariableId);
                    }
                    else if (com.CompareVariableType == VariableTypes.InstanceVariable)
                    {
                        rdoVarCompareInstanceVar.Checked = true;
                        cmbCompareInstanceVar.SelectedIndex = InstanceVariableBase.ListIndex(com.CompareVariableId);
                    }
                }
                else
                {
                    rdoVarCompareStaticValue.Checked = true;
                    nudVariableValue.Value = com.Value;
                }

                UpdateNumericVariableElements();
            }
        }

        private void TryLoadVariableStringComparison(VariableCompaison comp)
        {
            if (comp != null && comp.GetType() == typeof(StringVariableComparison))
            {
                var com = (StringVariableComparison)comp;

                cmbStringComparitor.SelectedIndex = Convert.ToInt32(com.Comparator);

                if (cmbStringComparitor.SelectedIndex < 0)
                {
                    cmbStringComparitor.SelectedIndex = 0;
                }

                txtStringValue.Text = com.Value;
            }
        }

        private BooleanVariableComparison GetBooleanVariableComparison()
        {
            var comp = new BooleanVariableComparison();

            if (cmbBooleanComparator.SelectedIndex < 0)
            {
                cmbBooleanComparator.SelectedIndex = 0;
            }

            comp.ComparingEqual = !Convert.ToBoolean(cmbBooleanComparator.SelectedIndex);

            comp.Value = optBooleanTrue.Checked;

            if (optBooleanGlobalVariable.Checked)
            {
                comp.CompareVariableType = VariableTypes.ServerVariable;
                comp.CompareVariableId = ServerVariableBase.IdFromList(cmbBooleanGlobalVariable.SelectedIndex);
            }
            else if (optBooleanInstanceVariable.Checked)
            {
                comp.CompareVariableType = VariableTypes.InstanceVariable;
                comp.CompareVariableId = InstanceVariableBase.IdFromList(cmbBooleanInstanceVariable.SelectedIndex);
            }

            return comp;
        }

        private IntegerVariableComparison GetNumericVariableComparison()
        {
            var comp = new IntegerVariableComparison();

            if (cmbNumericComparitor.SelectedIndex < 0)
            {
                cmbNumericComparitor.SelectedIndex = 0;
            }

            comp.Comparator = (VariableComparators)cmbNumericComparitor.SelectedIndex;

            comp.CompareVariableId = Guid.Empty;

            if (rdoVarCompareStaticValue.Checked)
            {
                comp.Value = (long)nudVariableValue.Value;
            }
            else if (rdoVarCompareGlobalVar.Checked)
            {
                comp.CompareVariableType = VariableTypes.ServerVariable;
                comp.CompareVariableId = ServerVariableBase.IdFromList(cmbCompareGlobalVar.SelectedIndex);
            }
            else if (rdoVarCompareInstanceVar.Checked)
            {
                comp.CompareVariableType = VariableTypes.InstanceVariable;
                comp.CompareVariableId = InstanceVariableBase.IdFromList(cmbCompareInstanceVar.SelectedIndex);
            }

            return comp;
        }

        private StringVariableComparison GetStringVariableComparison()
        {
            var comp = new StringVariableComparison();

            if (cmbStringComparitor.SelectedIndex < 0)
            {
                cmbStringComparitor.SelectedIndex = 0;
            }

            comp.Comparator = (StringVariableComparators)cmbStringComparitor.SelectedIndex;

            comp.Value = txtStringValue.Text;

            return comp;
        }

        private void DisplaySpawnConditions()
        {
            grpVariable.Show();
            SetupSpawnConditionFormValues();
        }

        #endregion

        private void frmMapLayers_DockStateChanged(object sender, EventArgs e)
        {
            CreateSwapChain();
        }

        //Map NPC List
        private void btnAddMapNpc_Click(object sender, EventArgs e)
        {
            var n = new NpcSpawn();

            //Don't add nothing
            if (cmbNpc.SelectedIndex > -1)
            {
                n.NpcId = NpcBase.IdFromList(cmbNpc.SelectedIndex);
                n.X = -1;
                n.Y = -1;
                n.Direction = NpcSpawnDirection.Random;
                n.RequiredPlayersToSpawn = (int)nudInstanceSpawnLimit.Value;
                n.PreventRespawn = chkDoNotRespawn.Checked;

                Globals.CurrentMap.Spawns.Add(n);
                lstMapNpcs.Items.Add(NpcBase.GetName(n.NpcId));
                lstMapNpcs.SelectedIndex = lstMapNpcs.Items.Count - 1;
                Globals.SelectedMapNpc = lstMapNpcs.SelectedIndex;
            }

            RefreshSpawnConditions();
        }

        private void btnRemoveMapNpc_Click(object sender, EventArgs e)
        {
            if (lstMapNpcs.SelectedIndex > -1)
            {
                Globals.CurrentMap.Spawns.RemoveAt(lstMapNpcs.SelectedIndex);
                lstMapNpcs.Items.RemoveAt(lstMapNpcs.SelectedIndex);

                // Refresh List
                lstMapNpcs.Items.Clear();
                for (var i = 0; i < Globals.CurrentMap.Spawns.Count; i++)
                {
                    lstMapNpcs.Items.Add(NpcBase.GetName(Globals.CurrentMap.Spawns[i].NpcId));
                }

                if (lstMapNpcs.Items.Count > 0)
                {
                    lstMapNpcs.SelectedIndex = 0;
                    NpcSpawn spawn = Globals.CurrentMap.Spawns[lstMapNpcs.SelectedIndex];
                    cmbNpc.SelectedIndex = NpcBase.ListIndex(spawn.NpcId);
                    cmbDir.SelectedIndex = (int)spawn.Direction;
                    nudInstanceSpawnLimit.Value = spawn.RequiredPlayersToSpawn;
                    chkDoNotRespawn.Checked = spawn.PreventRespawn;
                    if (Globals.CurrentMap.Spawns[lstMapNpcs.SelectedIndex].X >= 0)
                    {
                        rbDeclared.Checked = true;
                    }
                    else
                    {
                        rbRandom.Checked = true;
                    }
                }
                Globals.SelectedMapNpc = lstMapNpcs.SelectedIndex;
            }

            RefreshSpawnConditions();
        }

        private void lstMapNpcs_Click(object sender, EventArgs e)
        {
            Globals.SelectedMapNpc = lstMapNpcs.SelectedIndex;
            if (lstMapNpcs.Items.Count > 0 && lstMapNpcs.SelectedIndex > -1)
            {
                NpcSpawn spawn = Globals.CurrentMap.Spawns[lstMapNpcs.SelectedIndex];
                cmbNpc.SelectedIndex = NpcBase.ListIndex(spawn.NpcId);
                cmbDir.SelectedIndex = (int)spawn.Direction;
                nudInstanceSpawnLimit.Value = spawn.RequiredPlayersToSpawn;
                chkDoNotRespawn.Checked = spawn.PreventRespawn;
                if (Globals.CurrentMap.Spawns[lstMapNpcs.SelectedIndex].X >= 0)
                {
                    rbDeclared.Checked = true;
                }
                else
                {
                    rbRandom.Checked = true;
                }
            }
        }

        private void rbRandom_Click(object sender, EventArgs e)
        {
            if (lstMapNpcs.SelectedIndex > -1)
            {
                Globals.CurrentMap.Spawns[lstMapNpcs.SelectedIndex].X = -1;
                Globals.CurrentMap.Spawns[lstMapNpcs.SelectedIndex].Y = -1;
                Globals.CurrentMap.Spawns[lstMapNpcs.SelectedIndex].Direction = NpcSpawnDirection.Random;
            }
        }

        private void cmbDir_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstMapNpcs.SelectedIndex >= 0)
            {
                Globals.CurrentMap.Spawns[lstMapNpcs.SelectedIndex].Direction =
                    (NpcSpawnDirection) cmbDir.SelectedIndex;
            }
        }

        private void cmbNpc_SelectedIndexChanged(object sender, EventArgs e)
        {
            var n = 0;

            if (lstMapNpcs.SelectedIndex >= 0)
            {
                Globals.CurrentMap.Spawns[lstMapNpcs.SelectedIndex].NpcId = NpcBase.IdFromList(cmbNpc.SelectedIndex);

                // Refresh List
                n = lstMapNpcs.SelectedIndex;
                lstMapNpcs.Items.Clear();
                for (var i = 0; i < Globals.CurrentMap.Spawns.Count; i++)
                {
                    lstMapNpcs.Items.Add(NpcBase.GetName(Globals.CurrentMap.Spawns[i].NpcId));
                }

                lstMapNpcs.SelectedIndex = n;
            }
        }

        private void lightEditor_Load(object sender, EventArgs e)
        {
        }

        private void btnVisualMapSelector_Click(object sender, EventArgs e)
        {
            var frmWarpSelection = new FrmWarpSelection();
            frmWarpSelection.SelectTile(
                MapList.OrderedMaps[cmbWarpMap.SelectedIndex].MapId, (int) nudWarpX.Value, (int) nudWarpY.Value
            );

            frmWarpSelection.ShowDialog();
            if (frmWarpSelection.GetResult())
            {
                cmbWarpMap.Items.Clear();
                for (var i = 0; i < MapList.OrderedMaps.Count; i++)
                {
                    cmbWarpMap.Items.Add(MapList.OrderedMaps[i].Name);
                }

                for (var i = 0; i < MapList.OrderedMaps.Count; i++)
                {
                    if (MapList.OrderedMaps[i].MapId == frmWarpSelection.GetMap())
                    {
                        cmbWarpMap.SelectedIndex = i;

                        break;
                    }
                }

                nudWarpX.Value = frmWarpSelection.GetX();
                nudWarpY.Value = frmWarpSelection.GetY();
            }
        }

        private void rbAnimation_CheckedChanged(object sender, EventArgs e)
        {
            cmbAnimationAttribute.Items.Clear();
            cmbAnimationAttribute.Items.AddRange(AnimationBase.Names);
            if (cmbAnimationAttribute.Items.Count > 0)
            {
                cmbAnimationAttribute.SelectedIndex = 0;
            }

            if (!rbAnimation.Checked)
            {
                return;
            }

            HideAttributeMenus();
            grpAnimation.Visible = true;
        }

        private void rbCritter_CheckedChanged(object sender, EventArgs e)
        {
            cmbCritterAnimation.Items.Clear();
            cmbCritterAnimation.Items.Add(Strings.General.none);
            cmbCritterAnimation.Items.AddRange(AnimationBase.Names);
            cmbCritterAnimation.SelectedIndex = 0;

            cmbCritterSprite.Items.Clear();
            cmbCritterSprite.Items.Add(Strings.General.none);
            cmbCritterSprite.Items.AddRange(GameContentManager.GetSmartSortedTextureNames(GameContentManager.TextureType.Entity));
            cmbCritterSprite.SelectedIndex = 0;

            if (nudCritterMoveFrequency.Value == 0)
            {
                nudCritterMoveFrequency.Value = 1000;
            }

            if (nudCritterMoveSpeed.Value == 0)
            {
                nudCritterMoveSpeed.Value = 400;
            }

            if (!rbCritter.Checked)
            {
                return;
            }

            HideAttributeMenus();
            grpCritter.Visible = true;
        }

        private void frmMapLayers_Load(object sender, EventArgs e)
        {
            CreateSwapChain();
            picTileset.MouseDown += pnlTilesetContainer.AutoDragPanel_MouseDown;
            picTileset.MouseUp += pnlTilesetContainer.AutoDragPanel_MouseUp;
            pnlTiles.BringToFront();
            InitLocalization();
        }

        private void InitLocalization()
        {
            Text = Strings.MapLayers.title;
            btnTileHeader.Text = Strings.MapLayers.tiles;
            btnAttributeHeader.Text = Strings.MapLayers.attributes;
            btnEventsHeader.Text = Strings.MapLayers.events;
            btnLightsHeader.Text = Strings.MapLayers.lights;
            btnNpcsHeader.Text = Strings.MapLayers.npcs;

            //Tiles Panel
            lblLayer.Text = Strings.Tiles.layer;
            lblTileset.Text = Strings.Tiles.tileset;
            lblTileType.Text = Strings.Tiles.tiletype;
            cmbAutotile.Items.Clear();
            cmbAutotile.Items.Add(Strings.Tiles.normal);
            cmbAutotile.Items.Add(Strings.Tiles.autotile);
            cmbAutotile.Items.Add(Strings.Tiles.fake);
            cmbAutotile.Items.Add(Strings.Tiles.animated);
            cmbAutotile.Items.Add(Strings.Tiles.cliff);
            cmbAutotile.Items.Add(Strings.Tiles.waterfall);
            cmbAutotile.Items.Add(Strings.Tiles.autotilexp);
            cmbAutotile.Items.Add(Strings.Tiles.animatedxp);

            //Attributes Panel
            rbBlocked.Text = Strings.Attributes.blocked;
            rbZDimension.Text = Strings.Attributes.zdimension;
            rbNPCAvoid.Text = Strings.Attributes.npcavoid;
            rbWarp.Text = Strings.Attributes.warp;
            rbItem.Text = Strings.Attributes.itemspawn;
            rbSound.Text = Strings.Attributes.mapsound;
            rbResource.Text = Strings.Attributes.resourcespawn;
            rbAnimation.Text = Strings.Attributes.mapanimation;
            rbGrappleStone.Text = Strings.Attributes.grapple;
            rbSlide.Text = Strings.Attributes.slide;
            rbCritter.Text = Strings.Attributes.critter;

            //Block Groupbox
            grpBlock.Text = Strings.Attributes.blocked;
            chkGroundLevel.Text = Strings.Attributes.groundlevel;

            //Map Animation Groupbox
            grpAnimation.Text = Strings.Attributes.mapanimation;
            lblAnimation.Text = Strings.Attributes.mapanimation;
            chkAnimationBlock.Text = Strings.Attributes.mapanimationblock;

            //Slide Groupbox
            grpSlide.Text = Strings.Attributes.slide;
            lblSlideDir.Text = Strings.Attributes.dir;
            cmbSlideDir.Items.Clear();
            for (var i = -1; i < 4; i++)
            {
                cmbSlideDir.Items.Add(Strings.Directions.dir[i]);
            }

            //Map Sound
            grpSound.Text = Strings.Attributes.mapsound;
            lblMapSound.Text = Strings.Attributes.sound;
            lblSoundDistance.Text = Strings.Attributes.distance;

            //Map Item
            grpItem.Text = Strings.Attributes.itemspawn;
            lblMapItem.Text = Strings.Attributes.item;
            lblMaxItemAmount.Text = Strings.Attributes.quantity;

            //Z-Dimension
            grpZDimension.Text = Strings.Attributes.zdimension;
            grpGateway.Text = Strings.Attributes.zgateway;
            grpDimBlock.Text = Strings.Attributes.zblock;
            rbGatewayNone.Text = Strings.Attributes.znone;
            rbGateway1.Text = Strings.Attributes.zlevel1;
            rbGateway2.Text = Strings.Attributes.zlevel2;
            rbBlockNone.Text = Strings.Attributes.znone;
            rbBlock1.Text = Strings.Attributes.zlevel1;
            rbBlock2.Text = Strings.Attributes.zlevel2;

            //Warp
            grpWarp.Text = Strings.Attributes.warp;
            lblMap.Text = Strings.Warping.map.ToString("");
            lblX.Text = Strings.Warping.x.ToString("");
            lblY.Text = Strings.Warping.y.ToString("");
            lblWarpDir.Text = Strings.Warping.direction.ToString("");
            cmbDirection.Items.Clear();
            for (var i = -1; i < 4; i++)
            {
                cmbDirection.Items.Add(Strings.Directions.dir[i]);
            }
            lblInstance.Text = Strings.Warping.instanceType;
            chkChangeInstance.Text = Strings.Warping.changeInstance;
            grpInstanceSettings.Text = Strings.Warping.mapInstancingGroup;

            btnVisualMapSelector.Text = Strings.Warping.visual;

            //Resource
            grpResource.Text = Strings.Attributes.resourcespawn;
            lblResource.Text = Strings.Attributes.resource;
            grpZResource.Text = Strings.Attributes.zdimension;
            rbLevel1.Text = Strings.Attributes.zlevel1;
            rbLevel2.Text = Strings.Attributes.zlevel2;

            //Critter
            grpCritter.Text = Strings.Attributes.critter;
            lblCritterSprite.Text = Strings.Attributes.crittersprite;
            lblCritterAnimation.Text = Strings.Attributes.critteranimation;
            lblCritterMovement.Text = Strings.Attributes.crittermovement;
            lblCritterLayer.Text = Strings.Attributes.critterlayer;
            lblCritterMoveSpeed.Text = Strings.Attributes.critterspeed;
            lblCritterMoveFrequency.Text = Strings.Attributes.critterfrequency;
            chkCritterIgnoreNpcAvoids.Text = Strings.Attributes.critterignorenpcavoids;
            chkCritterBlockPlayers.Text = Strings.Attributes.critterblockplayers;
            lblCritterDirection.Text = Strings.Attributes.critterdirection;

            cmbCritterDirection.Items.Clear();
            cmbCritterDirection.Items.Add(Strings.NpcSpawns.randomdirection);
            for (var i = 0; i < 4; i++)
            {
                cmbCritterDirection.Items.Add(Strings.Directions.dir[i]);
            }
            cmbCritterDirection.SelectedIndex = 0;

            cmbCritterMovement.Items.Clear();
            for (var i = 0; i < Strings.Attributes.crittermovements.Count; i++)
            {
                cmbCritterMovement.Items.Add(Strings.Attributes.crittermovements[i]);
            }
            cmbCritterMovement.SelectedIndex = 0;

            cmbCritterLayer.Items.Clear();
            for (var i = 0; i < Strings.Attributes.critterlayers.Count; i++)
            {
                cmbCritterLayer.Items.Add(Strings.Attributes.critterlayers[i]);
            }
            cmbCritterLayer.SelectedIndex = 1;

            //NPCS Tab
            grpSpawnLoc.Text = rbDeclared.Checked ? Strings.NpcSpawns.spawndeclared : Strings.NpcSpawns.spawnrandom;
            rbDeclared.Text = Strings.NpcSpawns.declaredlocation;
            rbRandom.Text = Strings.NpcSpawns.randomlocation;
            lblDir.Text = Strings.NpcSpawns.direction;
            cmbDir.Items.Clear();
            cmbDir.Items.Add(Strings.NpcSpawns.randomdirection);
            for (var i = 0; i < 4; i++)
            {
                cmbDir.Items.Add(Strings.Directions.dir[i]);
            }

            grpNpcList.Text = Strings.NpcSpawns.addremove;
            btnAddMapNpc.Text = Strings.NpcSpawns.add;
            btnRemoveMapNpc.Text = Strings.NpcSpawns.remove;
            lblInstanceLimit.Text = Strings.NpcSpawns.minimumininstance;
            grpSpawnSettings.Text = Strings.NpcSpawns.SpawnSettings;
            chkConditionalSpawning.Text = Strings.NpcSpawns.ConditionalSpawning;

            lblEventInstructions.Text = Strings.MapLayers.eventinstructions;
            lblLightInstructions.Text = Strings.MapLayers.lightinstructions;
        }

        public void InitMapLayers()
        {
            CreateSwapChain();
        }

        private void CreateSwapChain()
        {
            if (!Globals.ClosingEditor)
            {
                if (mChain != null)
                {
                    mChain.Dispose();
                }

                if (Core.Graphics.GetGraphicsDevice() != null)
                {
                    mChain = new SwapChainRenderTarget(
                        Core.Graphics.GetGraphicsDevice(), picTileset.Handle, picTileset.Width, picTileset.Height,
                        false, SurfaceFormat.Color, DepthFormat.Depth24, 0, RenderTargetUsage.DiscardContents,
                        PresentInterval.Immediate
                    );

                    Core.Graphics.SetTilesetChain(mChain);
                }
            }
        }

        private void rbGrappleStone_CheckedChanged(object sender, EventArgs e)
        {
            HideAttributeMenus();
        }

        private void rbSlide_CheckedChanged(object sender, EventArgs e)
        {
            HideAttributeMenus();
            grpSlide.Visible = true;
            cmbSlideDir.SelectedIndex = 0;
        }

        private void lstMapNpcs_MouseDown(object sender, MouseEventArgs e)
        {
            lstMapNpcs.SelectedIndex = lstMapNpcs.IndexFromPoint(e.Location);
            Globals.SelectedMapNpc = lstMapNpcs.SelectedIndex;
            RefreshSpawnConditions();
        }

        public void RefreshSpawnConditions()
        {
            if (lstMapNpcs.Items.Count > 0 && lstMapNpcs.SelectedIndex >= 0)
            {
                chkConditionalSpawning.Checked = Globals.CurrentMap.Spawns[lstMapNpcs.SelectedIndex].ConditionalSpawning;
                if (Globals.CurrentMap.Spawns[lstMapNpcs.SelectedIndex].ConditionalSpawning)
                {
                    DisplaySpawnConditions();
                }
                else
                {
                    grpVariable.Hide();
                }
            }
        }

        private void ChangeTab()
        {
            btnTileHeader.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
            btnAttributeHeader.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
            btnLightsHeader.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
            btnEventsHeader.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
            btnNpcsHeader.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
            pnlTiles.Hide();
            pnlAttributes.Hide();
            pnlLights.Hide();
            pnlEvents.Hide();
            pnlNpcs.Hide();
            Globals.SelectedMapNpc = -1;

            //Force Game Object Lists to Refresh
            rbAnimation_CheckedChanged(null, null);
            rbWarp_CheckedChanged(null, null);
            rbResource_CheckedChanged(null, null);

            if (Globals.EditingLight != null)
            {
                Globals.MapLayersWindow.lightEditor.Cancel();
            }
        }

        private void btnTileHeader_Click(object sender, EventArgs e)
        {
            Globals.CurrentTool = Globals.SavedTool;
            ChangeTab();
            SetLayer(mLastTileLayer);
            Core.Graphics.TilePreviewUpdated = true;
            btnTileHeader.BackColor = System.Drawing.Color.FromArgb(90, 90, 90);
            CurrentTab = LayerTabs.Tiles;
            pnlTiles.Show();
        }

        private void btnAttributeHeader_Click(object sender, EventArgs e)
        {
            Globals.CurrentTool = Globals.SavedTool;
            ChangeTab();
            Globals.CurrentLayer = LayerOptions.Attributes;
            Core.Graphics.TilePreviewUpdated = true;
            btnAttributeHeader.BackColor = System.Drawing.Color.FromArgb(90, 90, 90);
            CurrentTab = LayerTabs.Attributes;
            pnlAttributes.Show();
        }

        public void btnLightsHeader_Click(object sender, EventArgs e)
        {
            if (Globals.CurrentLayer != LayerOptions.Lights && Globals.CurrentLayer != LayerOptions.Events && Globals.CurrentLayer != LayerOptions.Npcs)
            {
                Globals.SavedTool = Globals.CurrentTool;
            }

            ChangeTab();
            Globals.CurrentLayer = LayerOptions.Lights;
            Core.Graphics.TilePreviewUpdated = true;
            btnLightsHeader.BackColor = System.Drawing.Color.FromArgb(90, 90, 90);
            CurrentTab = LayerTabs.Lights;
            pnlLights.Show();
        }

        private void btnEventsHeader_Click(object sender, EventArgs e)
        {
            if (Globals.CurrentLayer != LayerOptions.Lights && Globals.CurrentLayer != LayerOptions.Events && Globals.CurrentLayer != LayerOptions.Npcs)
            {
                Globals.SavedTool = Globals.CurrentTool;
            }

            ChangeTab();
            Globals.CurrentLayer = LayerOptions.Events;
            Core.Graphics.TilePreviewUpdated = true;
            btnEventsHeader.BackColor = System.Drawing.Color.FromArgb(90, 90, 90);
            CurrentTab = LayerTabs.Events;
            pnlEvents.Show();
        }

        private void btnNpcsHeader_Click(object sender, EventArgs e)
        {
            if (Globals.CurrentLayer != LayerOptions.Lights && Globals.CurrentLayer != LayerOptions.Events && Globals.CurrentLayer != LayerOptions.Npcs)
            {
                Globals.SavedTool = Globals.CurrentTool;
            }

            ChangeTab();
            Globals.CurrentLayer = LayerOptions.Npcs;
            Core.Graphics.TilePreviewUpdated = true;
            RefreshNpcList();
            btnNpcsHeader.BackColor = System.Drawing.Color.FromArgb(90, 90, 90);
            CurrentTab = LayerTabs.Npcs;
            pnlNpcs.Show();
            Globals.SelectedMapNpc = lstMapNpcs.SelectedIndex;
        }

        private void picMapLayer_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var index = mMapLayers.IndexOf((PictureBox)sender);
                if (index > -1 && index < Options.Instance.MapOpts.Layers.All.Count)
                {
                    SetLayer(Options.Instance.MapOpts.Layers.All[index]);
                }
            }
            else
            {
                ToggleLayerVisibility(mMapLayers.IndexOf((PictureBox) sender));
            }
        }

        private void ToggleLayerVisibility(int index)
        {
            if (index > -1 && index < Options.Instance.MapOpts.Layers.All.Count)
            {
                LayerVisibility[Options.Instance.MapOpts.Layers.All[index]] = !LayerVisibility[Options.Instance.MapOpts.Layers.All[index]];
                SetLayer(Globals.CurrentLayer);
            }
            
        }

        private void picMapLayer_MouseHover(object sender, EventArgs e)
        {
            var tt = new ToolTip();
            tt.SetToolTip((PictureBox) sender, Options.Instance.MapOpts.Layers.All[mMapLayers.IndexOf((PictureBox)sender)]);
        }

        private void cmbTilesets_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && Globals.CurrentTileset != null)
            {
                Clipboard.SetText(Globals.CurrentTileset.Id.ToString());
            }
        }

        private void NudItemQuantity_ValueChanged(object sender, System.EventArgs e)
        {
            nudItemQuantity.Value = Math.Max(1, nudItemQuantity.Value);
        }

        private void cmbMapLayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbMapLayer.SelectedIndex > -1)
            {
                SetLayer(Options.Instance.MapOpts.Layers.All[cmbMapLayer.SelectedIndex]);
            }
        }

        private void chkChangeInstance_CheckedChanged(object sender, EventArgs e)
        {
            grpInstanceSettings.Visible = chkChangeInstance.Checked;
        }

        private void nudInstanceSpawnLimit_ValueChanged(object sender, EventArgs e)
        {
            if (lstMapNpcs.SelectedIndex >= 0)
            {
                Globals.CurrentMap.Spawns[lstMapNpcs.SelectedIndex].RequiredPlayersToSpawn = (int) nudInstanceSpawnLimit.Value;
            }
        }

        private void chkDoNotRespawn_CheckedChanged(object sender, EventArgs e)
        {
            if (lstMapNpcs.SelectedIndex >= 0)
            {
                Globals.CurrentMap.Spawns[lstMapNpcs.SelectedIndex].PreventRespawn = chkDoNotRespawn.Checked;
            }
        }

        private void chkConditionalSpawning_CheckedChanged(object sender, EventArgs e)
        {
            if (lstMapNpcs.SelectedIndex >= 0)
            {
                Globals.CurrentMap.Spawns[lstMapNpcs.SelectedIndex].ConditionalSpawning = chkConditionalSpawning.Checked;
                if (chkConditionalSpawning.Checked)
                {
                    DisplaySpawnConditions();
                }
                else
                {
                    grpVariable.Hide();
                }
            }
        }

        private void InitializeAndSelectDefaultVariableElement()
        {
            InitVariableElements(Guid.Empty);
            if (!mLoading && cmbVariable.Items.Count > 0)
            {
                cmbVariable.SelectedIndex = 0;
            }
        }

        private void rdoGlobalVariable_CheckedChanged(object sender, EventArgs e)
        {
            InitializeAndSelectDefaultVariableElement();
        }

        private void rdoInstanceVariable_CheckedChanged(object sender, EventArgs e)
        {
            InitializeAndSelectDefaultVariableElement();
        }

        private void rdoVarCompareGlobalVar_CheckedChanged(object sender, EventArgs e)
        {
            UpdateNumericVariableElements();
        }

        private void rdoVarCompareInstanceVar_CheckedChanged(object sender, EventArgs e)
        {
            UpdateNumericVariableElements();
        }

        private void SaveSpawnCondition()
        {
            if (lstMapNpcs.Items.Count <= 0 || lstMapNpcs.SelectedIndex < 0)
            {
                btnApplySpawnConditions.Enabled = false;
                return;
            }
           
            btnApplySpawnConditions.Enabled = true;

            var selectedSpawn = Globals.CurrentMap.Spawns[lstMapNpcs.SelectedIndex];
            if (rdoGlobalVariable.Checked)
            {
                selectedSpawn.SpawnVariableType = VariableTypes.ServerVariable;
                selectedSpawn.SpawnVariableId = ServerVariableBase.IdFromList(cmbVariable.SelectedIndex);
            }
            else if (rdoInstanceVariable.Checked)
            {
                selectedSpawn.SpawnVariableType = VariableTypes.InstanceVariable;
                selectedSpawn.SpawnVariableId = InstanceVariableBase.IdFromList(cmbVariable.SelectedIndex);
            }

            if (grpBooleanVariable.Visible)
            {
                selectedSpawn.BooleanSpawnComparison = GetBooleanVariableComparison();
                selectedSpawn.SpawnConditionType = NpcSpawn.SpawnComparisonType.Boolean;
            }
            else if (grpNumericVariable.Visible)
            {
                selectedSpawn.IntegerSpawnComparison = GetNumericVariableComparison();
                selectedSpawn.SpawnConditionType = NpcSpawn.SpawnComparisonType.Integer;
            }
            else if (grpStringVariable.Visible)
            {
                selectedSpawn.StringSpawnComparison = GetStringVariableComparison();
                selectedSpawn.SpawnConditionType = NpcSpawn.SpawnComparisonType.String;
            }
        }

        private void btnApplySpawnConditions_Click(object sender, EventArgs e)
        {
            SaveSpawnCondition();
        }

        private void cmbVariable_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mLoading)
            {
                return;
            }

            if (rdoGlobalVariable.Checked)
            {
                InitVariableElements(ServerVariableBase.IdFromList(cmbVariable.SelectedIndex));
            }
            else if (rdoInstanceVariable.Checked)
            {
                InitVariableElements(InstanceVariableBase.IdFromList(cmbVariable.SelectedIndex));
            }

            UpdateVariableElements();
        }
    }

}
