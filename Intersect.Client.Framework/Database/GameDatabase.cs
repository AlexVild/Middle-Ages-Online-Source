using System;

namespace Intersect.Client.Framework.Database
{

    public abstract class GameDatabase
    {

        public bool FullScreen;

        public bool HideOthersOnWindowOpen;

        public bool TapToTurn;

        public bool FaceOnLock;

        public bool CombatFlash;

        public bool CombatShake;

        public bool TargetAccountDirection;

        //Preferences
        public int MusicVolume;

        public int SoundVolume;

        public int TargetFps;

        public int TargetResolution;

        public bool StickyTarget;

        public bool LeftClickTarget;

        //Saving password, other stuff we don't want in the games directory
        public abstract void SavePreference(string key, object value);

        public abstract string LoadPreference(string key);

        public T LoadPreference<T>(string key, T defaultValue)
        {
            var value = LoadPreference(key);
            if (string.IsNullOrEmpty(value))
            {
                return defaultValue;
            }

            return (T) Convert.ChangeType(value, typeof(T));
        }

        //Load all preferences when the game starts
        public virtual void LoadPreferences()
        {
            MusicVolume = LoadPreference("MusicVolume", 25);
            SoundVolume = LoadPreference("SoundVolume", 25);
            TargetResolution = LoadPreference("Resolution", 0);
            TargetFps = LoadPreference("Fps", 0);
            FullScreen = LoadPreference("Fullscreen", false);
            HideOthersOnWindowOpen = LoadPreference("HideOthersOnWindowOpen", true);
            TapToTurn = LoadPreference("TapToTurn", false);
            FaceOnLock = LoadPreference("FaceOnLock", true);
            LeftClickTarget = LoadPreference("LeftClickTarget", false);
            TargetAccountDirection = LoadPreference("TargetAccountDirection", false);
            StickyTarget = LoadPreference("StickyTarget", true);
        }

        public virtual void SavePreferences()
        {
            SavePreference("MusicVolume", MusicVolume.ToString());
            SavePreference("SoundVolume", SoundVolume.ToString());
            SavePreference("Fullscreen", FullScreen.ToString());
            SavePreference("Resolution", TargetResolution.ToString());
            SavePreference("Fps", TargetFps.ToString());
            SavePreference("HideOthersOnWindowOpen", HideOthersOnWindowOpen.ToString());
            SavePreference("TargetAccountDirection", TargetAccountDirection.ToString());
            SavePreference("StickyTarget", StickyTarget.ToString());
            SavePreference("TapToTurn", TapToTurn.ToString());
            SavePreference("FaceOnLock", FaceOnLock.ToString());
            SavePreference("LeftClickTarget", LeftClickTarget.ToString());
        }

        public abstract bool LoadConfig();

    }

}
