using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Graphics;
using Intersect.Client.General;
using Intersect.Client.Interface.Game.Toasts;
using Intersect.Client.Localization;
using Intersect.Client.Maps;
using Intersect.Client.MonoGame;
using Intersect.Client.Networking;
using Intersect.Configuration;
using Intersect.Enums;
using Intersect.GameObjects;
using Intersect.GameObjects.Maps;
using Intersect.Utilities;

// ReSharper disable All

namespace Intersect.Client.Core
{

    internal static class Main
    {

        private static long _animTimer;

        private static bool _createdMapTextures;

        private static bool _loadedTilesets;

        private static long GameStartTime;

        internal static void Start(IClientContext context)
        {
            GameStartTime = Timing.Global.MillisecondsUtcUnsynced;
            Graphics.InitPreload();

            //Load Sounds
            Audio.Init();

            //Init Network
            Networking.Network.InitNetwork(context);

            //Make Json.Net Familiar with Our Object Types
            var id = Guid.NewGuid();
            foreach (var val in Enum.GetValues(typeof(GameObjectType)))
            {
                var type = ((GameObjectType) val);
                if (type != GameObjectType.Event && type != GameObjectType.Time)
                {
                    var lookup = type.GetLookup();
                    var item = lookup.AddNew(type.GetObjectType(), id);
                    item.Load(item.JsonData);
                    lookup.Delete(item);
                }
            }
        }

        public static void DestroyGame()
        {
            //Destroy Game
            //TODO - Destroy Graphics and Networking peacefully
            //Network.Close();
            Interface.Interface.DestroyGwen();
            Graphics.Renderer.Close();
        }

        public static bool InitPreload = false;

        public static bool InitLoad = false;

        public static void Update()
        {
            lock (Globals.GameLock)
            {
                Networking.Network.Update();
                Fade.Update();
                Wipe.Update();
                Flash.Update();
                Interface.Interface.ToggleInput(Globals.GameState != GameStates.Intro && Globals.GameState != GameStates.Preloading);

                switch (Globals.GameState)
                {
                    case GameStates.Intro:
                        ProcessIntro();

                        break;

                    case GameStates.Menu:
                        ProcessMenu();

                        break;

                    case GameStates.Loading:
                        if (InitLoad)
                        {
                            ProcessLoading();
                        }
                        InitLoad = true; // Draw a frame
                        break;

                    case GameStates.InGame:
                        ProcessGame();

                        break;

                    case GameStates.Preloading:
                        if (InitPreload)
                        {
                            Graphics.InitGraphics();
                            StartIntro();
                        }
                        InitPreload = true; // Force a frame to be drawn (the loading screen)
                        break;

                    case GameStates.Error:
                        break;

                    default:
                        throw new ArgumentOutOfRangeException(
                            nameof(Globals.GameState), $"Value {Globals.GameState} out of range."
                        );
                }

                Globals.InputManager.Update();
                Audio.Update();

                Globals.OnGameUpdate();
            }
        }

        private static void StartIntro()
        {
            CheckForAnimatedIntro();
            Globals.GameState = GameStates.Intro;
        }

        private static void ProcessIntro()
        {
            if (ClientConfiguration.Instance.IntroImages.Count <= 0 || 
                Globals.IntroIndex >= ClientConfiguration.Instance.IntroImages.Count)
            {
                Globals.GameState = GameStates.Menu;
                Graphics.ResetMenu();
                FadeService.FadeIn();
                return;
            }

            // Forces a delay before showing intro, just to give everything a second.
            if (Timing.Global.MillisecondsUtcUnsynced < Globals.IntroBlackDelay)
            {
                // Prevents our first image from being done displaying immediately.
                Globals.IntroStartTime = Timing.Global.MillisecondsUtcUnsynced;
                return;
            }

            GameTexture imageTex = Globals.ContentManager.GetTexture(
                    GameContentManager.TextureType.Image, ClientConfiguration.Instance.IntroImages[Globals.IntroIndex]
                );

            if (imageTex != null)
            {
                if (Globals.IntroStartTime == -1 && FadeService.DoneFading())
                {
                    FadeService.FadeIn(callback: () =>
                    {
                        Globals.IntroStartTime = Timing.Global.MillisecondsUtcUnsynced;
                    });
                }
                else if(Timing.Global.MillisecondsUtcUnsynced > Globals.IntroStartTime + Globals.IntroDelay && FadeService.DoneFading())
                {
                    //If we have shown an image long enough, fade to black -- keep track that the image is going
                    FadeService.FadeOut(callback: () =>
                    {
                        NextIntro();
                    });
                }
            }
            else
            {
                NextIntro();
            }
        }

        private static void NextIntro()
        {
            Globals.IntroStartTime = -1;
            Globals.IntroIndex++;
            // Start the menu music if we've made it past the Grimhaus logo
            if (Globals.IntroIndex > 0)
            {
                Globals.IntroDelay = Globals.DefaultIntroDelay;
                StartMenuMusic();
            }
            CheckForAnimatedIntro();
        }

        private static void CheckForAnimatedIntro()
        {
            Globals.AnimatedIntro = false;
            var introImages = ClientConfiguration.Instance.IntroImages;
            if (introImages.Count <= Globals.IntroIndex)
            {
                return;
            }

            var nextImage = introImages[Globals.IntroIndex];
            // If the intro image contains something like "strip_44", the last number is the amount of frames
            var frameNameSplit = nextImage.Split('_');
            if (frameNameSplit.Length <= 0 || !frameNameSplit.Contains("strip"))
            {
                return;
            }
            try
            {
                // Split on HFRAMEsxVFRAMES.ext
                var frameData = frameNameSplit.Last().Split('.').First();
                Globals.IntroHFrames = int.Parse(frameData.Split('x').First());
                Globals.IntroVFrames = int.Parse(frameData.Split('x').Last());
                InitializeAnimatedIntro();
            }
            catch (Exception e)
            {
                return; // just swallow, we won't animate
            }

            Globals.IntroDelay = Globals.IntroFps * (Globals.IntroHFrames * Globals.IntroVFrames) * 10;
            Globals.IntroStartTime = Timing.Global.MillisecondsUtcUnsynced + Globals.IntroFps;
        }

        private static void InitializeAnimatedIntro()
        {
            Graphics.IntroHFrame = 0;
            Graphics.IntroVFrame = 0;
            Globals.AnimatedIntro = true;
            Graphics.IntroUpdateTime = Timing.Global.MillisecondsUtcUnsynced + (Globals.IntroFps * 10);
        }

        private static void ProcessMenu()
        {
            StartMenuMusic();
            if (!Globals.JoiningGame)
                return;

            //Check if maps are loaded and ready
            Globals.GameState = GameStates.Loading;
            Interface.Interface.DestroyGwen();
        }

        private static void StartMenuMusic()
        {
            if (!Globals.StartMenuMusic)
            {
                Audio.PlayMusic(ClientConfiguration.Instance.MenuMusic, 6f, 10f, true);
                Globals.StartMenuMusic = true;
            }
        }

        private static void ProcessLoading()
        {
            if (Globals.Me == null || Globals.Me.MapInstance == null)
                return;

            if (!_loadedTilesets && Globals.HasGameData)
            {
                Globals.ContentManager.LoadTilesets(TilesetBase.GetNameList());
                _loadedTilesets = true;
            }

            Audio.PlayMusic(MapInstance.Get(Globals.Me.CurrentMap).Music, 6f, 10f, true);
            Globals.GameState = GameStates.InGame;
            FadeService.FadeIn();
        }

        private static void ProcessGame()
        {
            if (Globals.ConnectionLost)
            {
                Main.ForceLogout(false);
                Interface.Interface.MsgboxErrors.Add(
                    new KeyValuePair<string, string>("", Strings.Errors.lostconnection)
                );

                Globals.ConnectionLost = false;

                return;
            }

            //If we are waiting on maps, lets see if we have them
            var currentMap = MapInstance.Get(Globals.Me.CurrentMap);

            if (Globals.NeedsMaps)
            {
                bool canShowWorld = true;
                if (currentMap != null)
                {
                    var gridX = currentMap.MapGridX;
                    var gridY = currentMap.MapGridY;
                    for (int x = gridX - 1; x <= gridX + 1; x++)
                    {
                        for (int y = gridY - 1; y <= gridY + 1; y++)
                        {
                            if (!Graphics.MapAtCoord(x, y))
                            {
                                continue;
                            }

                            var map = MapInstance.Get(Globals.MapGrid[x, y]);
                            canShowWorld = map != null && map.MapLoaded;
                        }
                    }
                }
                else
                {
                    canShowWorld = false;
                }

                canShowWorld = true;
                if (canShowWorld)
                {
                    Globals.NeedsMaps = false;
                    //Send ping to server, so it will resync time if needed as we load in
                    PacketSender.SendPing();
                }
            }
            else if (currentMap != null)
            {
                var gridX = currentMap.MapGridX;
                var gridY = currentMap.MapGridY;
                for (int x = gridX - 1; x <= gridX + 1; x++)
                {
                    for (int y = gridY - 1; y <= gridY + 1; y++)
                    {
                        if (!Graphics.MapAtCoord(x, y))
                        {
                            continue;
                        }

                        PacketSender.SendNeedMap(Globals.MapGrid[x, y]);
                    }
                }
            }

            if (!Globals.NeedsMaps)
            {
                //Update All Entities
                foreach (var en in Globals.Entities)
                {
                    if (en.Value == null)
                        continue;

                    en.Value.Update();
                }

                for (int i = 0; i < Globals.EntitiesToDispose.Count; i++)
                {
                    if (Globals.Entities.ContainsKey(Globals.EntitiesToDispose[i]))
                    {
                        if (Globals.EntitiesToDispose[i] == Globals.Me.Id)
                            continue;

                        Globals.Entities[Globals.EntitiesToDispose[i]].Dispose();
                        Globals.Entities.Remove(Globals.EntitiesToDispose[i]);
                    }
                }

                Globals.EntitiesToDispose.Clear();

                //Update Maps
                var maps = MapInstance.Lookup.Values.ToArray();
                foreach (MapInstance map in maps)
                {
                    if (map == null)
                        continue;

                    map.Update(map.InView());
                }
            }

            //Update Game Animations
            if (_animTimer < Timing.Global.Milliseconds)
            {
                Globals.AnimFrame++;
                if (Globals.AnimFrame == 3)
                {
                    Globals.AnimFrame = 0;
                }

                _animTimer = Timing.Global.Milliseconds + 500;
            }

            //Remove Event Holds If Invalid
            var removeHolds = new List<Guid>();
            foreach (var hold in Globals.EventHolds)
            {
                //If hold.value is empty its a common event, ignore. Otherwise make sure we have the map else the hold doesnt matter
                if (hold.Value != Guid.Empty && MapInstance.Get(hold.Value) == null)
                {
                    removeHolds.Add(hold.Key);
                }
            }

            foreach (var hold in removeHolds)
            {
                Globals.EventHolds.Remove(hold);
            }

            Graphics.UpdatePlayerLight();
            Time.Update();
        }

        public static void JoinGame()
        {
            Globals.LoggedIn = true;
            Globals.LastDialogClosed = Timing.Global.Milliseconds;
            Audio.StopMusic(6f);
        }

        public static void Logout(bool characterSelect)
        {
            Globals.StartMenuMusic = false;
            Globals.LastLevelJinglePlayed = 0L;
            ToastService.EmptyToastQueue();
            InitLoad = false;
            FadeService.FadeOut(callback: () =>
            {
                ForceLogout(characterSelect);
            });
        }

        public static void ForceLogout(bool characterSelect)
        {
            Globals.StartMenuMusic = false;
            PacketSender.SendLogout(characterSelect);
            Globals.LoggedIn = false;
            Globals.WaitingOnServer = false;
            Globals.WaitingOnServerDispose = characterSelect;
            Graphics.CurrentShake = 0f;
            Globals.GameState = GameStates.Menu;
            Globals.JoiningGame = false;
            Globals.NeedsMaps = true;
            Globals.Picture = null;
            Interface.Interface.HideUi = false;

            //Dump Game Objects
            Globals.Me = null;
            Globals.HasGameData = false;
            foreach (var map in MapInstance.Lookup)
            {
                var mp = (MapInstance)map.Value;
                mp.Dispose(false, true);
            }

            foreach (var en in Globals.Entities.ToArray())
            {
                en.Value.Dispose();
            }

            MapBase.Lookup.Clear();
            MapInstance.Lookup.Clear();

            Globals.Entities.Clear();
            Globals.MapGrid = null;
            Globals.GridMaps.Clear();
            Globals.EventDialogs.Clear();
            Globals.EventHolds.Clear();
            Globals.PendingEvents.Clear();

            Timers.ActiveTimers.Clear();

            Interface.Interface.InitGwen();
            FadeService.FadeIn();
        }

    }

}
