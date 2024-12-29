using Intersect.Client.Framework.Content;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Intersect.Client.Framework.Audio;
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

        /// <inheritdoc />
        public override GameTexture LoadTexture(string filename, string realFilename) =>
            Core.Graphics.Renderer.LoadTexture(filename, realFilename);

        protected override void InitializeTextureAsset(TextureType type)
        {
            mTextureAssets[type] = new ConcurrentDictionary<string, IAsset>();
        }

        /// <inheritdoc />
        public override GameFont LoadFont(string filePath) => Core.Graphics.Renderer.LoadFont(filePath);

        /// <inheritdoc />
        public override GameShader LoadShader(string shaderName) => Core.Graphics.Renderer.LoadShader(shaderName);

        /// <inheritdoc />
        public override GameAudioSource LoadSoundSource(string path, string realPath) =>
            new MonoSoundSource(path, realPath);

        /// <inheritdoc />
        public override GameAudioSource LoadMusicSource(string path, string realPath) =>
            new MonoMusicSource(path, realPath);

        /// <inheritdoc />
        protected override TAsset Load<TAsset>(
            ConcurrentDictionary<string, IAsset> lookup,
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
