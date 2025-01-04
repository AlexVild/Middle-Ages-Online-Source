using Intersect.Client.Framework.Audio;
using Intersect.Client.Framework.Content;
using Intersect.Client.Framework.Graphics;
using Intersect.Logging;

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;

using Intersect.Client.Framework.GenericClasses;
using Intersect.Plugins;
using Intersect.Compression;

using Newtonsoft.Json.Linq;

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

        protected Dictionary<TextureType, ConcurrentDictionary<string, IAsset>> mTextureAssets = new Dictionary<TextureType, ConcurrentDictionary<string, IAsset>>();

        protected ConcurrentDictionary<string, GameFont> mFontDict = new ConcurrentDictionary<string, GameFont>();

        protected ConcurrentDictionary<string, GameAudioSource> mMusicDict = new ConcurrentDictionary<string, GameAudioSource>();

        protected ConcurrentDictionary<string, GameShader> mShaderDict = new ConcurrentDictionary<string, GameShader>();

        protected ConcurrentDictionary<string, GameAudioSource> mSoundDict = new ConcurrentDictionary<string, GameAudioSource>();

        protected ConcurrentDictionary<KeyValuePair<UI, string>, string> mUiDict = new ConcurrentDictionary<KeyValuePair<UI, string>, string>();

        /// <summary>
        /// Contains all indexed files and their caches from sound pack files.
        /// </summary>
        public AssetPacker SoundPacks { get; set; }

        /// <summary>
        /// Contains all indexed files and their caches from music pack files.
        /// </summary>
        public AssetPacker MusicPacks { get; set; }

        protected Dictionary<string, IAsset> mSpellDict = new Dictionary<string, IAsset>();

        protected ConcurrentDictionary<string, IAsset> mTexturePackDict = new ConcurrentDictionary<string, IAsset>();

        //Game Content

        public bool TilesetsLoaded = false;

        public void Init(GameContentManager manager)
        {
            Current = manager;
        }

        //Content Loading
        public void LoadAll(Action onCompleted)
        {
            var packLoader = new ThreadedContentLoader();

            var startTexturePacks = DateTime.UtcNow;
            packLoader.EnqueueWork(IndexTexturePacks());
            var endTexturePacks = DateTime.UtcNow;

            var timeSpanTexturePacks = endTexturePacks - startTexturePacks;

            Log.Info($"[Content] Indexing TexturePacks took {timeSpanTexturePacks.TotalMilliseconds}ms");

            packLoader.Completed += () =>
            {
                var contentLoader = new ThreadedContentLoader();
                contentLoader.Completed += onCompleted;

                Log.Info("[Content] Starting content indexing...");

                var startIndexAll = DateTime.UtcNow;

                var startEntities = DateTime.UtcNow;
                contentLoader.EnqueueWork(IndexEntities());
                var endEntities = DateTime.UtcNow;

                var startItems = DateTime.UtcNow;
                contentLoader.EnqueueWork(IndexItems());
                var endItems = DateTime.UtcNow;

                var startAnimations = DateTime.UtcNow;
                contentLoader.EnqueueWork(IndexAnimations());
                var endAnimations = DateTime.UtcNow;

                var startSpells = DateTime.UtcNow;
                contentLoader.EnqueueWork(IndexSpells());
                var endSpells = DateTime.UtcNow;

                var startFaces = DateTime.UtcNow;
                contentLoader.EnqueueWork(IndexFaces());
                var endFaces = DateTime.UtcNow;

                var startImages = DateTime.UtcNow;
                contentLoader.EnqueueWork(IndexImages());
                var endImages = DateTime.UtcNow;

                var startFogs = DateTime.UtcNow;
                contentLoader.EnqueueWork(IndexFogs());
                var endFogs = DateTime.UtcNow;

                var startResources = DateTime.UtcNow;
                contentLoader.EnqueueWork(IndexResources());
                var endResources = DateTime.UtcNow;

                var startPaperdolls = DateTime.UtcNow;
                contentLoader.EnqueueWork(IndexPaperdolls());
                var endPaperdolls = DateTime.UtcNow;

                var startMisc = DateTime.UtcNow;
                contentLoader.EnqueueWork(IndexMisc());
                var endMisc = DateTime.UtcNow;

                var startGui = DateTime.UtcNow;
                contentLoader.EnqueueWork(IndexGui());
                var endGui = DateTime.UtcNow;

                var startFonts = DateTime.UtcNow;
                contentLoader.EnqueueWork(IndexFonts());
                var endFonts = DateTime.UtcNow;

                var startShaders = DateTime.UtcNow;
                contentLoader.EnqueueWork(IndexShaders());
                var endShaders = DateTime.UtcNow;

                var startDecor = DateTime.UtcNow;
                contentLoader.EnqueueWork(IndexDecor());
                var endDecor = DateTime.UtcNow;

                var startChallenges = DateTime.UtcNow;
                contentLoader.EnqueueWork(IndexChallenges());
                var endChallenges = DateTime.UtcNow;

                var endAll = DateTime.UtcNow;
                var timeSpanIndexAll = endAll - startIndexAll;

                var timeSpanEntities = endEntities - startEntities;
                var timeSpanItems = endItems - startItems;
                var timeSpanAnimations = endAnimations - startAnimations;
                var timeSpanSpells = endSpells - startSpells;
                var timeSpanFaces = endFaces - startFaces;
                var timeSpanImages = endImages - startImages;
                var timeSpanFogs = endFogs - startFogs;
                var timeSpanResources = endResources - startResources;
                var timeSpanPaperdolls = endPaperdolls - startPaperdolls;
                var timeSpanMisc = endMisc - startMisc;
                var timeSpanGui = endGui - startGui;
                var timeSpanFonts = endFonts - startFonts;
                var timeSpanShaders = endShaders - startShaders;
                var timeSpanDecor = endDecor - startDecor;
                var timeSpanChallenges = endChallenges - startChallenges;

                Log.Info($"[Content] Indexing Entities took {timeSpanEntities.TotalMilliseconds}ms");
                Log.Info($"[Content] Indexing Items took {timeSpanItems.TotalMilliseconds}ms");
                Log.Info($"[Content] Indexing Animations took {timeSpanAnimations.TotalMilliseconds}ms");
                Log.Info($"[Content] Indexing Spells took {timeSpanSpells.TotalMilliseconds}ms");
                Log.Info($"[Content] Indexing Faces took {timeSpanFaces.TotalMilliseconds}ms");
                Log.Info($"[Content] Indexing Images took {timeSpanImages.TotalMilliseconds}ms");
                Log.Info($"[Content] Indexing Fogs took {timeSpanFogs.TotalMilliseconds}ms");
                Log.Info($"[Content] Indexing Resources took {timeSpanResources.TotalMilliseconds}ms");
                Log.Info($"[Content] Indexing Paperdolls took {timeSpanPaperdolls.TotalMilliseconds}ms");
                Log.Info($"[Content] Indexing Misc took {timeSpanMisc.TotalMilliseconds}ms");
                Log.Info($"[Content] Indexing Gui took {timeSpanGui.TotalMilliseconds}ms");
                Log.Info($"[Content] Indexing Fonts took {timeSpanFonts.TotalMilliseconds}ms");
                Log.Info($"[Content] Indexing Shaders took {timeSpanShaders.TotalMilliseconds}ms");
                Log.Info($"[Content] Indexing Decor took {timeSpanDecor.TotalMilliseconds}ms");
                Log.Info($"[Content] Indexing Challenges took {timeSpanChallenges.TotalMilliseconds}ms");
                Log.Info($"[Content] Indexing all content took {timeSpanIndexAll.TotalMilliseconds}ms");

                contentLoader.Start();
            };

            packLoader.Start();
        }

        public Action[] IndexTextureGroup(string textureGroup, ConcurrentDictionary<string, IAsset> textureLookup)
        {
            textureLookup.Clear();

            var textureDirectoryPath = Path.Combine("resources", textureGroup);
            if (!Directory.Exists(textureDirectoryPath))
            {
                if (!Directory.Exists(Path.Combine("resources", "packs")))
                {
                    Directory.CreateDirectory(textureDirectoryPath);
                }

                return Array.Empty<Action>();
            }

            GameTexturePacks.TryGetFolderFrames(textureGroup, out var groupFrames);

            var workItems = new List<Action>();

            var looseFiles = Directory.GetFiles(textureDirectoryPath, "*.png");
            var looseLoadItems = looseFiles
                .Where(
                    looseFilePath =>
                    {
                        if (groupFrames == default &&
                            !GameTexturePacks.TryGetFolderFrames(textureGroup, out groupFrames))
                        {
                            return true;
                        }

                        var fileName = looseFilePath.Replace(textureDirectoryPath, string.Empty)
                            .TrimStart(Path.DirectorySeparatorChar);

                        var textureName = fileName.ToLowerInvariant();
                        if (!groupFrames.TryGetValue(textureName, out var matchingPackFrame))
                        {
                            return true;
                        }

                        Log.Debug(
                            $"Texture '{textureName}' already found in texture pack '{matchingPackFrame.PackTexture.Name}'"
                        );

                        return false;

                    }
                )
                .Select<string, Action>(
                    looseFilePath => () =>
                    {
                        var fileName = looseFilePath.Replace(textureDirectoryPath, string.Empty)
                            .TrimStart(Path.DirectorySeparatorChar);

                        var textureName = fileName.ToLowerInvariant();

                        var realFilePath = Path.Combine(textureDirectoryPath, fileName);

                        if (textureLookup.ContainsKey(textureName))
                        {
                            Log.Warn($"Duplicate texture with name '{textureName}' ({realFilePath})");

                            return;
                        }

                        var texture = LoadTexture(
                            realFilePath,
                            realFilePath
                        );

                        if (!textureLookup.TryAdd(textureName, texture))
                        {
                            Log.Error(
                                $"Failed to add loaded texture for {looseFilePath} to the texture lookup because another texture shares the name '{textureName}'"
                            );
                        }
                    }
                )
                .ToArray();
            workItems.AddRange(looseLoadItems);

            if (groupFrames != default)
            {
                workItems.AddRange(
                    groupFrames.Where(kvp => !textureLookup.ContainsKey(kvp.Key))
                        .Select<KeyValuePair<string, GameTexturePackFrame>, Action>(
                            framePair => () =>
                            {
                                var frameKey = framePair.Key;
                                var frame = framePair.Value;

                                if (textureLookup.ContainsKey(frameKey))
                                {
                                    return;
                                }

                                var asset = LoadTexture(
                                    frameKey,
                                    frame.Filename,
                                    packFrame: frame
                                );

                                textureLookup.TryAdd(frameKey, asset);
                            }
                        )
                );
            }

            return workItems.ToArray();
        }

        protected abstract void InitializeTextureAsset(TextureType textureType);

        public abstract GameTexture LoadTexture(
            string filename,
            string realFilename,
            bool isTexturePack = false,
            GameTexturePackFrame packFrame = null
        );

        public Action[] IndexTexturePacks()
        {
            mTexturePackDict.Clear();

            var dir = Path.Combine("resources", "packs");
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);

                return Array.Empty<Action>();
            }

            var packMetadataNames = Directory.GetFiles(dir, "*.meta");

            return packMetadataNames.Select<string, Action>(
                    packMetadataName => () =>
                    {
                        var json = GzipCompression.ReadDecompressedString(packMetadataName);
                        var obj = JObject.Parse(json);
                        if (!obj.TryGetValue("frames", out var framesToken) || !(framesToken is JArray frameMetaTokens))
                        {
                            return;
                        }

                        if (!obj.TryGetValue("meta", out var metaToken) || !(metaToken is JObject meta))
                        {
                            return;
                        }

                        if (!meta.TryGetValue("image", out var imageToken))
                        {
                            return;
                        }

                        var packTextureFileName = imageToken?.ToString();
                        if (string.IsNullOrWhiteSpace(packTextureFileName))
                        {
                            return;
                        }

                        var packFilePath = Path.Combine("resources", "packs", packTextureFileName);
                        if (!File.Exists(packFilePath))
                        {
                            return;
                        }

                        var platformText = LoadTexture(
                            packFilePath,
                            packFilePath,
                            isTexturePack: true
                        );

                        if (platformText == null)
                        {
                            return;
                        }

                        foreach (var frameMetaToken in frameMetaTokens)
                        {
                            if (!(frameMetaToken is JObject frameMeta))
                            {
                                continue;
                            }

                            if (!frameMeta.TryGetValue("frame", out var frameToken) || !(frameToken is JObject frame))
                            {
                                continue;
                            }

                            if (!frameMeta.TryGetValue("spriteSourceSize", out var spriteSourceSizeToken) ||
                                !(spriteSourceSizeToken is JObject spriteSourceSize))
                            {
                                continue;
                            }

                            var filename = frameMeta["filename"]?.ToString();
                            var rotated = bool.Parse(frameMeta["rotated"].ToString());

                            var sourceRect = new Rectangle(
                                int.Parse(frame["x"].ToString()),
                                int.Parse(frame["y"].ToString()),
                                int.Parse(frame["w"].ToString()),
                                int.Parse(frame["h"].ToString())
                            );

                            var sourceBounds = new Rectangle(
                                int.Parse(spriteSourceSize["x"].ToString()),
                                int.Parse(spriteSourceSize["y"].ToString()),
                                int.Parse(spriteSourceSize["w"].ToString()),
                                int.Parse(spriteSourceSize["h"].ToString())
                            );

                            GameTexturePacks.AddFrame(
                                new GameTexturePackFrame(
                                    filename,
                                    sourceRect,
                                    rotated,
                                    sourceBounds,
                                    platformText
                                )
                            );
                        }
                    }
                )
                .ToArray();
        }

        public void LoadTilesets(string[] tilesetNames)
        {
            if (mTextureAssets.TryGetValue(TextureType.Tileset, out var tilesetDict))
            {
                tilesetDict.Clear();
            }
            else
            {
                InitializeTextureAsset(TextureType.Tileset);
                tilesetDict = mTextureAssets[TextureType.Tileset];
            }

            var tilesetDirectoryPath = Path.Combine("resources", "tilesets");
            if (!Directory.Exists(tilesetDirectoryPath))
            {
                if (!Directory.Exists(Path.Combine("resources", "packs")))
                {
                    Directory.CreateDirectory(tilesetDirectoryPath);
                }

                return;
            }

            var tilesetFiles = Directory.GetFiles(tilesetDirectoryPath)
                .Select(f => Path.GetFileName(f))
                .ToDictionary(f => f.ToLowerInvariant(), f => f);

            foreach (var tilesetName in tilesetNames)
            {
                if (string.IsNullOrWhiteSpace(tilesetName))
                {
                    continue;
                }

                var searchName = tilesetName.ToLowerInvariant();
                if (!tilesetFiles.TryGetValue(searchName, out var realFilename))
                {
                    continue;
                }

                if (string.IsNullOrWhiteSpace(realFilename))
                {
                    var packFrame = GameTexturePacks.GetFrame(
                        Path.Combine("resources", "tilesets", tilesetName),
                        Path.Combine("resources", "tilesets", searchName)
                    );

                    if (packFrame == default && !tilesetDict.ContainsKey(searchName))
                    {
                        continue;
                    }
                }

                var asset = LoadTexture(
                    Path.Combine("resources", "tilesets", tilesetName),
                    Path.Combine("resources", "tilesets", realFilename)
                );

                tilesetDict.TryAdd(searchName, asset);
            }

            TilesetsLoaded = true;
        }

        public Action[] IndexItems()
        {
            InitializeTextureAsset(TextureType.Item);
            return IndexTextureGroup("items", mTextureAssets[TextureType.Item]);
        }

        public Action[] IndexEntities()
        {
            InitializeTextureAsset(TextureType.Entity);
            return IndexTextureGroup("entities", mTextureAssets[TextureType.Entity]);
        }

        public Action[] IndexSpells()
        {
            InitializeTextureAsset(TextureType.Spell);
            return IndexTextureGroup("spells", mTextureAssets[TextureType.Spell]);
        }

        public Action[] IndexAnimations()
        {
            InitializeTextureAsset(TextureType.Animation);
            return IndexTextureGroup("animations", mTextureAssets[TextureType.Animation]);
        }

        public Action[] IndexFaces()
        {
            InitializeTextureAsset(TextureType.Face);
            return IndexTextureGroup("faces", mTextureAssets[TextureType.Face]);
        }

        public Action[] IndexImages()
        {
            InitializeTextureAsset(TextureType.Image);
            return IndexTextureGroup("images", mTextureAssets[TextureType.Image]);
        }

        public Action[] IndexFogs()
        {
            InitializeTextureAsset(TextureType.Fog);
            return IndexTextureGroup("fogs", mTextureAssets[TextureType.Fog]);
        }

        public Action[] IndexResources()
        {
            InitializeTextureAsset(TextureType.Resource);
            return IndexTextureGroup("resources", mTextureAssets[TextureType.Resource]);
        }

        public Action[] IndexPaperdolls()
        {
            InitializeTextureAsset(TextureType.Paperdoll);
            return IndexTextureGroup("paperdolls", mTextureAssets[TextureType.Paperdoll]);
        }

        public Action[] IndexGui()
        {
            InitializeTextureAsset(TextureType.Gui);
            return IndexTextureGroup("gui", mTextureAssets[TextureType.Gui]);
        }

        public Action[] IndexDecor()
        {
            InitializeTextureAsset(TextureType.Decor);
            return IndexTextureGroup("decor", mTextureAssets[TextureType.Decor]);
        }

        public Action[] IndexChallenges()
        {
            InitializeTextureAsset(TextureType.Challenge);
            return IndexTextureGroup("challenges", mTextureAssets[TextureType.Challenge]);
        }

        public void LoadLoading()
        {
            InitializeTextureAsset(TextureType.Loading);
            foreach (var workItem in IndexTextureGroup("loading", mTextureAssets[TextureType.Loading]))
            {
                workItem();
            }
        }

        public Action[] IndexMisc()
        {
            InitializeTextureAsset(TextureType.Misc);
            return IndexTextureGroup("misc", mTextureAssets[TextureType.Misc]);
        }

        public Action[] IndexFonts()
        {
            mFontDict.Clear();
            var dir = Path.Combine("resources", "fonts");
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            var filePaths = Directory.GetFiles(dir, "*.xnb");

            return filePaths.Select<string, Action>(
                    filePath => () =>
                    {
                        var fileName = filePath.Replace(dir, "").TrimStart(Path.DirectorySeparatorChar).ToLower();
                        var font = LoadFont(Path.Combine(dir, fileName));
                        var fontSearchName = $"{font.GetName().Trim().ToLowerInvariant()},{font.GetSize()}";
                        mFontDict.TryAdd(fontSearchName, font);
                    }
                )
                .ToArray();
        }

        public abstract GameFont LoadFont(string filePath);

        public Action[] IndexShaders()
        {
            mShaderDict.Clear();

            const string shaderPrefix = "Intersect.Client.Resources.Shaders.";
            var availableShaders = GetType().Assembly
                .GetManifestResourceNames()
                .Where(resourceName =>
                    resourceName.StartsWith(shaderPrefix)
                    && resourceName.EndsWith(".xnb")
                ).ToArray();

            return availableShaders.Select<string, Action>(
                    resourceFullName => () =>
                    {
                        var shaderNameWithoutExtension = resourceFullName.Substring(0, resourceFullName.Length - 4);
                        var shader = LoadShader(resourceFullName);
                        var shaderName = shaderNameWithoutExtension.Substring(shaderPrefix.Length).ToLowerInvariant();
                        mShaderDict.TryAdd(shaderName, shader);
                    }
                )
                .ToArray();
        }

        public abstract GameShader LoadShader(string shaderName);

        //Audio Loading
        public void LoadAudio()
        {
            LoadSounds();
            LoadMusic();
        }

        public void LoadSounds()
        {
            mSoundDict.Clear();
            var dir = Path.Combine("resources", "sounds");
            if (!Directory.Exists(dir))
            {
                if (!Directory.Exists(Path.Combine("resources", "packs")))
                {
                    Directory.CreateDirectory(dir);
                }
            }
            else
            {
                var soundFilePaths = Directory.GetFiles(dir, "*.wav");
                foreach (var soundFilePath in soundFilePaths)
                {
                    var filename = soundFilePath.Replace(dir, string.Empty).TrimStart(Path.DirectorySeparatorChar).ToLowerInvariant();
                    mSoundDict.TryAdd(
                        RemoveExtension(filename),
                        LoadSoundSource(
                            Path.Combine(dir, filename),
                            Path.Combine(dir, soundFilePath.Replace(dir, "").TrimStart(Path.DirectorySeparatorChar))
                        )
                    );
                }
            }

            // If we have a sound index file, load from it!
            if (File.Exists(Path.Combine("resources", "packs", "sound.index")))
            {
                SoundPacks = new AssetPacker(Path.Combine("resources", "packs", "sound.index"), Path.Combine("resources", "packs"));
                foreach(var soundFileName in SoundPacks.FileList)
                {
                    var soundName = RemoveExtension(soundFileName).ToLowerInvariant();
                    if (!mSoundDict.TryAdd(soundName, LoadSoundSource(soundFileName, soundFileName)))
                    {
                        Log.Error($"Found two sounds with the normalized name '{soundName}'");
                    }
                }
            }

        }

        public abstract GameAudioSource LoadSoundSource(string path, string realPath);

        public void LoadMusic()
        {
            mMusicDict.Clear();
            var dir = Path.Combine("resources", "music");
            if (!Directory.Exists(dir))
            {
                if (!Directory.Exists(Path.Combine("resources", "packs")))
                {
                    Directory.CreateDirectory(dir);
                }
            }
            else
            {
                var musicFilePaths = Directory.GetFiles(dir, "*.ogg");
                foreach (var musicFilePath in musicFilePaths)
                {
                    var musicFileName = musicFilePath.Replace(dir, string.Empty).TrimStart(Path.DirectorySeparatorChar).ToLowerInvariant();
                    mMusicDict.TryAdd(
                        RemoveExtension(musicFileName),
                        LoadMusicSource(
                            Path.Combine(dir, musicFileName),
                            Path.Combine(dir, musicFilePath.Replace(dir, string.Empty).TrimStart(Path.DirectorySeparatorChar))
                        )
                    );
                }
            }

            // If we have a music index file, load from it!
            if (File.Exists(Path.Combine("resources", "packs", "music.index")))
            {
                MusicPacks = new AssetPacker(Path.Combine("resources", "packs", "music.index"), Path.Combine("resources", "packs"));
                foreach (var musicFileName in MusicPacks.FileList)
                {
                    var musicName = RemoveExtension(musicFileName).ToLowerInvariant();
                    if (!mSoundDict.TryAdd(musicName, LoadMusicSource(musicFileName, musicFileName)))
                    {
                        Log.Error($"Found two music tracks with the normalized name '{musicName}'");
                    }
                }
            }
        }

        public abstract GameAudioSource LoadMusicSource(string path, string realPath);

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
            if (!mTextureAssets.TryGetValue(type, out var txtDict))
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

            var searchName = $"{name.ToLower().Trim()},{size}";

            return mFontDict.TryGetValue(searchName, out var font) ? font : default;
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
                        mUiDict.TryAdd(key, json);
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

        protected ConcurrentDictionary<string, IAsset> GetAssetLookup(ContentTypes contentType)
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
            ConcurrentDictionary<string, IAsset> lookup,
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
