using Intersect.Client.Framework.Content;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.GenericClasses;
using Intersect.Client.Framework.Graphics;
using Intersect.Client.Localization;
using Intersect.Client.MonoGame.Audio;
using Intersect.Client.MonoGame.Graphics;
using Intersect.Compression;
using Intersect.Logging;

using Newtonsoft.Json.Linq;

namespace Intersect.Client.MonoGame.File_Management
{

    public class MonoContentManager : GameContentManager
    {

        public MonoContentManager()
        {
            Init(this);
            if (!Directory.Exists("resources"))
            {
                Log.Error(Strings.Errors.resourcesnotfound);

                Environment.Exit(1);
            }
        }

        //Graphic Loading
        public override void LoadTilesets(string[] tilesetnames)
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
            IEnumerable<string> tilesetFiles = Array.Empty<string>();

            var dir = Path.Combine("resources", "tilesets");
            if (!Directory.Exists(dir))
            {
                if (!Directory.Exists(Path.Combine("resources", "packs")))
                {
                    Directory.CreateDirectory(dir);
                }
            }
            else
            {
                tilesetFiles = Directory.GetFiles(dir).Select(f => Path.GetFileName(f));
            }

            foreach (var t in tilesetnames)
            {
                var realFilename = tilesetFiles.FirstOrDefault(file => t.Equals(file, StringComparison.InvariantCultureIgnoreCase)) ?? string.Empty;
                if (!string.IsNullOrWhiteSpace(t) &&
                    (!string.IsNullOrWhiteSpace(realFilename) ||
                     GameTexturePacks.GetFrame(Path.Combine("resources", "tilesets", t.ToLower())) != null) &&
                    !tilesetDict.ContainsKey(t.ToLower()))
                {
                    tilesetDict.Add(
                        t.ToLower(), Core.Graphics.Renderer.LoadTexture(Path.Combine("resources", "tilesets", t), Path.Combine("resources", "tilesets", realFilename))
                    );
                }
            }

            TilesetsLoaded = true;
        }

        public override void LoadTexturePacks()
        {
            mTexturePackDict.Clear();
            var dir = Path.Combine("resources", "packs");
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            var items = Directory.GetFiles(dir, "*.meta");
            for (var i = 0; i < items.Length; i++)
            {
                var json = GzipCompression.ReadDecompressedString(items[i]);
                var obj = JObject.Parse(json);
                var frames = (JArray) obj["frames"];
                var img = obj["meta"]["image"].ToString();
                if (File.Exists(Path.Combine("resources", "packs", img)))
                {
                    var platformText = Core.Graphics.Renderer.LoadTexture(Path.Combine("resources", "packs", img), Path.Combine("resources", "packs", img));
                    if (platformText != null)
                    {
                        foreach (var frame in frames)
                        {
                            var filename = frame["filename"].ToString();
                            var sourceRect = new Rectangle(
                                int.Parse(frame["frame"]["x"].ToString()), int.Parse(frame["frame"]["y"].ToString()),
                                int.Parse(frame["frame"]["w"].ToString()), int.Parse(frame["frame"]["h"].ToString())
                            );

                            var rotated = bool.Parse(frame["rotated"].ToString());
                            var sourceSize = new Rectangle(
                                int.Parse(frame["spriteSourceSize"]["x"].ToString()),
                                int.Parse(frame["spriteSourceSize"]["y"].ToString()),
                                int.Parse(frame["spriteSourceSize"]["w"].ToString()),
                                int.Parse(frame["spriteSourceSize"]["h"].ToString())
                            );

                            GameTexturePacks.AddFrame(
                                new GameTexturePackFrame(filename, sourceRect, rotated, sourceSize, platformText)
                            );
                        }
                    }
                }
            }
        }

        public void LoadTextureGroup(string directory, Dictionary<string, IAsset> dict)
        {
            dict.Clear();
            var dir = Path.Combine("resources", directory);
            if (!Directory.Exists(dir))
            {
                if (!Directory.Exists(Path.Combine("resources", "packs")))
                {
                    Directory.CreateDirectory(dir);
                }
            }
            else
            {
                var items = Directory.GetFiles(dir, "*.png");
                for (var i = 0; i < items.Length; i++)
                {
                    var filename = items[i].Replace(dir, "").TrimStart(Path.DirectorySeparatorChar).ToLower();
                    dict.Add(filename, Core.Graphics.Renderer.LoadTexture(Path.Combine(dir, filename), Path.Combine(dir, items[i].Replace(dir, "").TrimStart(Path.DirectorySeparatorChar))));
                }
            }

            var packItems = GameTexturePacks.GetFolderFrames(directory);
            if (packItems != null)
            {
                foreach (var itm in packItems)
                {
                    var filename = Path.GetFileName(itm.Filename.ToLower().Replace("\\", "/"));
                    if (!dict.ContainsKey(filename))
                    {
                        dict.Add(filename, Core.Graphics.Renderer.LoadTexture(Path.Combine(dir, filename), Path.Combine(dir, Path.Combine(dir, filename))));
                    }
                }
            }
        }

        private void InitializeTextureAsset(TextureType type)
        {
            if (mTextureAssets.ContainsKey(type))
            {
                mTextureAssets[type] = new Dictionary<string, IAsset>();
                return;
            }

            mTextureAssets.Add(type, new Dictionary<string, IAsset>());
        }

        public override void LoadItems()
        {
            InitializeTextureAsset(TextureType.Item);
            LoadTextureGroup("items", mTextureAssets[TextureType.Item]);
        }

        public override void LoadEntities()
        {
            InitializeTextureAsset(TextureType.Entity);
            LoadTextureGroup("entities", mTextureAssets[TextureType.Entity]);
        }

        public override void LoadSpells()
        {
            InitializeTextureAsset(TextureType.Spell);
            LoadTextureGroup("spells", mTextureAssets[TextureType.Spell]);
        }

        public override void LoadAnimations()
        {
            InitializeTextureAsset(TextureType.Animation);
            LoadTextureGroup("animations", mTextureAssets[TextureType.Animation]);
        }

        public override void LoadFaces()
        {
            InitializeTextureAsset(TextureType.Face);
            LoadTextureGroup("faces", mTextureAssets[TextureType.Face]);
        }

        public override void LoadImages()
        {
            InitializeTextureAsset(TextureType.Image);
            LoadTextureGroup("images", mTextureAssets[TextureType.Image]);
        }

        public override void LoadFogs()
        {
            InitializeTextureAsset(TextureType.Fog);
            LoadTextureGroup("fogs", mTextureAssets[TextureType.Fog]);
        }

        public override void LoadResources()
        {
            InitializeTextureAsset(TextureType.Resource);
            LoadTextureGroup("resources", mTextureAssets[TextureType.Resource]);
        }

        public override void LoadPaperdolls()
        {
            InitializeTextureAsset(TextureType.Paperdoll);
            LoadTextureGroup("paperdolls", mTextureAssets[TextureType.Paperdoll]);
        }

        public override void LoadGui()
        {
            InitializeTextureAsset(TextureType.Gui);
            LoadTextureGroup("gui", mTextureAssets[TextureType.Gui]);
        }

        public override void LoadDecor()
        {
            InitializeTextureAsset(TextureType.Decor);
            LoadTextureGroup("decor", mTextureAssets[TextureType.Decor]);
        }

        public override void LoadChallenges()
        {
            InitializeTextureAsset(TextureType.Challenge);
            LoadTextureGroup("challenges", mTextureAssets[TextureType.Challenge]);
        }

        public override void LoadMisc()
        {
            InitializeTextureAsset(TextureType.Misc);
            LoadTextureGroup("misc", mTextureAssets[TextureType.Misc]);
        }

        public override void LoadFonts()
        {
            mFontDict.Clear();
            var dir = Path.Combine("resources", "fonts");
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            var items = Directory.GetFiles(dir, "*.xnb");
            for (var i = 0; i < items.Length; i++)
            {
                var filename = items[i].Replace(dir, "").TrimStart(Path.DirectorySeparatorChar).ToLower();
                var font = Core.Graphics.Renderer.LoadFont(Path.Combine(dir, filename));
                if (mFontDict.IndexOf(font) == -1)
                {
                    mFontDict.Add(font);
                }
            }
        }

        public override void LoadShaders()
        {
            mShaderDict.Clear();

            const string shaderPrefix = "Intersect.Client.Resources.Shaders.";
            var availableShaders = typeof(MonoContentManager).Assembly
                .GetManifestResourceNames()
                .Where(resourceName =>
                    resourceName.StartsWith(shaderPrefix)
                    && resourceName.EndsWith(".xnb")
                ).ToArray();

            for (var i = 0; i < availableShaders.Length; i++)
            {
                var resourceFullName = availableShaders[i];
                var shaderNameWithoutExtension = resourceFullName.Substring(0, resourceFullName.Length - 4);
                var shaderName = shaderNameWithoutExtension.Substring(shaderPrefix.Length);
                mShaderDict.Add(shaderName, Core.Graphics.Renderer.LoadShader(resourceFullName));
            }
        }

        public override void LoadSounds()
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
                var items = Directory.GetFiles(dir, "*.wav");
                for (var i = 0; i < items.Length; i++)
                {
                    var filename = items[i].Replace(dir, "").TrimStart(Path.DirectorySeparatorChar).ToLower();
                    mSoundDict.Add(
                        RemoveExtension(filename),
                        new MonoSoundSource(
                            Path.Combine(dir, filename), Path.Combine(dir, items[i].Replace(dir, "").TrimStart(Path.DirectorySeparatorChar))
                        )
                    );
                }
            }

            // If we have a sound index file, load from it!
            if (File.Exists(Path.Combine("resources", "packs", "sound.index")))
            {
                SoundPacks = new AssetPacker(Path.Combine("resources", "packs", "sound.index"), Path.Combine("resources", "packs"));
                foreach(var item in SoundPacks.FileList)
                {
                    if (!mSoundDict.ContainsKey(RemoveExtension(item).ToLower()))
                    {
                        mSoundDict.Add(
                            RemoveExtension(item).ToLower(),
                            new MonoSoundSource(item, item)
                        );
                    }
                }
            }
            
        }

        public override void LoadMusic()
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
                var items = Directory.GetFiles(dir, "*.ogg");
                for (var i = 0; i < items.Length; i++)
                {
                    var filename = items[i].Replace(dir, "").TrimStart(Path.DirectorySeparatorChar).ToLower();
                    mMusicDict.Add(RemoveExtension(filename), new MonoMusicSource(Path.Combine(dir, filename), Path.Combine(dir, items[i].Replace(dir, "").TrimStart(Path.DirectorySeparatorChar))));
                }
            }

            // If we have a music index file, load from it!
            if (File.Exists(Path.Combine("resources", "packs", "music.index")))
            {
                MusicPacks = new AssetPacker(Path.Combine("resources", "packs", "music.index"), Path.Combine("resources", "packs"));
                foreach (var item in MusicPacks.FileList)
                {
                    if (!mMusicDict.ContainsKey(RemoveExtension(item).ToLower()))
                    {
                        mMusicDict.Add(
                            RemoveExtension(item).ToLower(),
                            new MonoMusicSource(item, item)
                        );
                    }
                }
            }
        }

        /// <inheritdoc />
        protected override TAsset Load<TAsset>(
            Dictionary<string, IAsset> lookup,
            ContentTypes contentType,
            string name,
            Func<Stream> createStream
        )
        {
            switch (contentType)
            {
                case ContentTypes.Animation:
                case ContentTypes.Entity:
                case ContentTypes.Face:
                case ContentTypes.Fog:
                case ContentTypes.Image:
                case ContentTypes.Interface:
                case ContentTypes.Item:
                case ContentTypes.Miscellaneous:
                case ContentTypes.Paperdoll:
                case ContentTypes.Resource:
                case ContentTypes.Spell:
                case ContentTypes.TexturePack:
                case ContentTypes.TileSet:
                    return Core.Graphics.Renderer.LoadTexture(name, createStream) as TAsset;

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

    }

}
