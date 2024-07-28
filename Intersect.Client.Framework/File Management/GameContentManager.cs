using Intersect.Client.Framework.Audio;
using Intersect.Client.Framework.Content;
using Intersect.Client.Framework.Graphics;
using Intersect.Logging;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Intersect.Plugins;
using Intersect.Compression;

namespace Intersect.Client.Framework.File_Management
{

    public abstract class GameContentManager : IContentManager
    {

        public enum TextureType
        {

            Tileset = 0,

            Item,

            Entity,

            Spell,

            Animation,

            Face,

            Image,

            Fog,

            Resource,

            Paperdoll,

            Gui,

            Misc,

            Decor,

            Challenge,
            
            Loading,

        }

        public enum UI
        {

            Menu,

            InGame

        }

        public static GameContentManager Current;

        protected Dictionary<TextureType, Dictionary<string, IAsset>> mTextureAssets = new Dictionary<TextureType, Dictionary<string, IAsset>>();

        protected List<GameFont> mFontDict = new List<GameFont>();

        protected Dictionary<string, GameAudioSource> mMusicDict = new Dictionary<string, GameAudioSource>();

        protected Dictionary<string, GameShader> mShaderDict = new Dictionary<string, GameShader>();
        
        protected Dictionary<string, GameAudioSource> mSoundDict = new Dictionary<string, GameAudioSource>();

        protected Dictionary<KeyValuePair<UI, string>, string> mUiDict = new Dictionary<KeyValuePair<UI, string>, string>();

        /// <summary>
        /// Contains all indexed files and their caches from sound pack files.
        /// </summary>
        public AssetPacker SoundPacks { get; set; }

        /// <summary>
        /// Contains all indexed files and their caches from music pack files.
        /// </summary>
        public AssetPacker MusicPacks { get; set; }

        protected Dictionary<string, IAsset> mSpellDict = new Dictionary<string, IAsset>();

        protected Dictionary<string, IAsset> mTexturePackDict = new Dictionary<string, IAsset>();

        //Game Content

        public bool TilesetsLoaded = false;

        public void Init(GameContentManager manager)
        {
            Current = manager;
        }

        //Content Loading
        public void LoadAll()
        {
            LoadTexturePacks();
            LoadEntities();
            LoadItems();
            LoadAnimations();
            LoadSpells();
            LoadFaces();
            LoadImages();
            LoadFogs();
            LoadResources();
            LoadPaperdolls();
            LoadMisc();
            LoadGui();
            LoadFonts();
            LoadShaders();
            LoadDecor();
            LoadChallenges();
        }

        public abstract void LoadTexturePacks();

        public abstract void LoadTilesets(string[] tilesetnames);

        public abstract void LoadItems();

        public abstract void LoadEntities();

        public abstract void LoadSpells();

        public abstract void LoadAnimations();

        public abstract void LoadFaces();

        public abstract void LoadImages();

        public abstract void LoadFogs();

        public abstract void LoadResources();

        public abstract void LoadPaperdolls();

        public abstract void LoadGui();

        public abstract void LoadMisc();

        public abstract void LoadFonts();

        public abstract void LoadShaders();

        public abstract void LoadDecor();

        public abstract void LoadChallenges();

        //Audio Loading
        public void LoadAudio()
        {
            LoadSounds();
            LoadMusic();
        }

        public abstract void LoadSounds();

        public abstract void LoadMusic();

        public static string RemoveExtension(string fileName)
        {
            var fileExtPos = fileName.LastIndexOf(".");
            if (fileExtPos >= 0)
            {
                fileName = fileName.Substring(0, fileExtPos);
            }

            return fileName;
        }

        public string[] GetTextureNames(TextureType type)
        {
            if (mTextureAssets.TryGetValue(type, out var txtDict))
            {
                return null;
            }

            return txtDict.Keys.ToArray();
        }

        //Content Getters
        public virtual GameTexture GetTexture(TextureType type, string name)
        {
            if (string.IsNullOrEmpty(name) || !mTextureAssets.TryGetValue(type, out var textureDict))
            {
                return null;
            }

            return textureDict.TryGetValue(name.ToLower(), out var asset) ? asset as GameTexture : default;
        }

        public virtual GameShader GetShader(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return null;
            }

            if (mShaderDict == null)
            {
                return null;
            }

            return mShaderDict.TryGetValue(name.ToLower(), out var effect) ? effect : null;
        }

        public virtual GameFont GetFont(string name, int size)
        {
            if (name == null)
            {
                return null;
            }

            return mFontDict.Where(t => t != null)
                .Where(t => t.GetName().ToLower().Trim() == name.ToLower().Trim())
                .FirstOrDefault(t => t.GetSize() == size);
        }

        public virtual GameAudioSource GetMusic(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return null;
            }

            if (mMusicDict == null)
            {
                return null;
            }

            return mMusicDict.TryGetValue(name.ToLower(), out var music) ? music : null;
        }

        public virtual GameAudioSource GetSound(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return null;
            }

            if (mSoundDict == null)
            {
                return null;
            }

            return mSoundDict.TryGetValue(name.ToLower(), out var sound) ? sound : null;
        }

        public virtual string GetUIJson(UI stage, string name, string resolution, out bool loadedCachedJson)
        {
            var key = new KeyValuePair<UI, string>(stage, $"{name}.{resolution}.json");
            if (mUiDict.TryGetValue(key, out string uiJson))
            {
                loadedCachedJson = true;
                return uiJson;
            }

            var resolutionWidth = int.Parse(resolution.Split('x')[0]);
            var resolutionSizeKey = GetUiSize(resolutionWidth);

            var alternateKey = new KeyValuePair<UI, string>(stage, $"{name}.{resolutionSizeKey}.json");
            if (mUiDict.TryGetValue(alternateKey, out string uiJsonAlt))
            {
                loadedCachedJson = true;
                return uiJsonAlt;
            }

            loadedCachedJson = false;

            var layouts = Path.Combine("resources", "gui", "layouts");
            if (!Directory.Exists(layouts))
            {
                Directory.CreateDirectory(layouts);
            }

            var dir = Path.Combine(layouts, stage == UI.Menu ? "menu" : "game");
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            var path = "";
            if (resolution != null)
            {
                path = Path.Combine(dir, name + "." + resolution + ".json");
                if (File.Exists(path))
                {
                    return File.ReadAllText(path);
                }
                path = Path.Combine(dir, name + "." + resolutionSizeKey + ".json");
                if (File.Exists(path))
                {
                    return File.ReadAllText(path);
                }
            }

            path = Path.Combine(dir, name + ".json");
            if (File.Exists(path))
            {
                var i = 0;
                while (true)
                {
                    try
                    {
                        var json = File.ReadAllText(path);
                        mUiDict.Add(key, json);
                        return json;
                    }
                    catch (Exception ex)
                    {
                        i++;
                        if (i > 10)
                        {
                            throw;
                        }

                        System.Threading.Thread.Sleep(50);
                    }
                }
            }

            return "";
        }

        public static string GetUiSize(int width)
        {
            if (width > 1366)
            {
                return "lg";
            }
            else
            {
                return "sm";
            }
        }

        public virtual void SaveUIJson(UI stage, string name, string json, string resolution)
        {
            var layouts = Path.Combine("resources", "gui", "layouts");
            if (!Directory.Exists(layouts))
            {
                Directory.CreateDirectory(layouts);
            }

            var dir = Path.Combine(layouts, stage == UI.Menu ? "menu" : "game");
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            var path = "";
            if (resolution != null)
            {
                path = Path.Combine(dir, name + "." + resolution + ".json");
                if (File.Exists(path))
                {
                    try
                    {
                        File.WriteAllText(path, json);
                    }
                    catch (Exception exception)
                    {
                        Log.Debug(exception);
                    }

                    return;
                }
            }

            path = Path.Combine(dir, name + ".json");
            try
            {
                File.WriteAllText(path, json);
            }
            catch (Exception exception)
            {
                Log.Debug(exception);
            }
        }

        protected Dictionary<string, IAsset> GetAssetLookup(ContentTypes contentType)
        {
            switch (contentType)
            {
                case ContentTypes.Animation:
                    return mTextureAssets[TextureType.Animation];

                case ContentTypes.Entity:
                    return mTextureAssets[TextureType.Entity];

                case ContentTypes.Face:
                    return mTextureAssets[TextureType.Face];

                case ContentTypes.Fog:
                    return mTextureAssets[TextureType.Fog];

                case ContentTypes.Image:
                    return mTextureAssets[TextureType.Image];

                case ContentTypes.Interface:
                    return mTextureAssets[TextureType.Gui];

                case ContentTypes.Item:
                    return mTextureAssets[TextureType.Item];

                case ContentTypes.Miscellaneous:
                    return mTextureAssets[TextureType.Misc];

                case ContentTypes.Paperdoll:
                    return mTextureAssets[TextureType.Paperdoll];

                case ContentTypes.Resource:
                    return mTextureAssets[TextureType.Resource];

                case ContentTypes.Spell:
                    return mTextureAssets[TextureType.Spell];

                case ContentTypes.TexturePack:
                    return mTexturePackDict;

                case ContentTypes.TileSet:
                    return mTextureAssets[TextureType.Tileset];

                case ContentTypes.Decor:
                    return mTextureAssets[TextureType.Decor];

                case ContentTypes.Challenges:
                    return mTextureAssets[TextureType.Challenge];

                case ContentTypes.Loading:
                    return mTextureAssets[TextureType.Loading];

                case ContentTypes.Font:
                    throw new NotImplementedException();

                case ContentTypes.Shader:
                    throw new NotImplementedException();

                case ContentTypes.Music:
                case ContentTypes.Sound:
                    throw new NotImplementedException();

                default:
                    throw new ArgumentOutOfRangeException(nameof(contentType), contentType, null);
            }
        }

        protected abstract TAsset Load<TAsset>(
            Dictionary<string, IAsset> lookup,
            ContentTypes contentType,
            string assetName,
            Func<Stream> createStream
        ) where TAsset : class, IAsset;

        /// <inheritdoc />
        public TAsset Load<TAsset>(ContentTypes contentType, string assetPath) where TAsset : class, IAsset
        {
            if (!File.Exists(assetPath))
            {
                throw new FileNotFoundException($@"Asset does not exist at '{assetPath}'.");
            }

            return Load<TAsset>(contentType, assetPath, () => File.OpenRead(assetPath));
        }

        /// <inheritdoc />
        public TAsset Load<TAsset>(ContentTypes contentType, string assetName, Func<Stream> createStream)
            where TAsset : class, IAsset
        {
            var assetLookup = GetAssetLookup(contentType);

            if (assetLookup.TryGetValue(assetName, out var asset))
            {
                return asset as TAsset;
            }

            return Load<TAsset>(assetLookup, contentType, assetName, createStream);
        }

        /// <inheritdoc />
        public TAsset LoadEmbedded<TAsset>(IPluginContext context, ContentTypes contentType, string assetName)
            where TAsset : class, IAsset
        {
            var manifestResourceName = context.EmbeddedResources.Resolve(assetName);
            return Load<TAsset>(contentType, assetName, () => context.EmbeddedResources.Read(manifestResourceName));
        }

    }

}
