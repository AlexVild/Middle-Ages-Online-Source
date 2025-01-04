using System.Collections.Generic;
using System.IO;
using System.Linq;

using Intersect.Client.Framework.GenericClasses;

namespace Intersect.Client.Framework.Graphics
{

    public class GameTexturePacks
    {
        private static readonly char[] DirectorySeparatorChars =
            new[] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar }.Distinct().ToArray();

        private static List<GameTexturePackFrame> mFrames = new List<GameTexturePackFrame>();

        private static Dictionary<string, Dictionary<string, GameTexturePackFrame>> _texturePackFramesByFolder =
            new Dictionary<string, Dictionary<string, GameTexturePackFrame>>();

        public static void AddFrame(GameTexturePackFrame frame)
        {
            lock (mFrames)
            {
                mFrames.Add(frame);

                var fileNameParts = frame.Filename.ToLowerInvariant().Split(DirectorySeparatorChars);

                var folderName = fileNameParts[1];
                if (!_texturePackFramesByFolder.TryGetValue(folderName, out var texturePackFramesForFolder))
                {
                    texturePackFramesForFolder = new Dictionary<string, GameTexturePackFrame>();
                    _texturePackFramesByFolder[folderName] = texturePackFramesForFolder;
                }

                var frameKey = fileNameParts[2];
                if (!texturePackFramesForFolder.ContainsKey(frameKey))
                {
                    texturePackFramesForFolder.Add(frameKey, frame);
                }
            }
        }

        public static bool TryGetFolderFrames(
            string folder,
            out Dictionary<string, GameTexturePackFrame> texturePackFramesForFolder
        ) => _texturePackFramesByFolder.TryGetValue(folder.ToLowerInvariant(), out texturePackFramesForFolder);

        public static GameTexturePackFrame GetFrame(params string[] filenames)
        {
            foreach (var filename in filenames)
            {
                var searchName = filename.Replace("\\", "/");

                return mFrames.Where(p => p.Filename.ToLower() == searchName).FirstOrDefault();
            }

            return default;
        }

    }

    public class GameTexturePackFrame
    {

        public GameTexturePackFrame(
            string filename,
            Rectangle rect,
            bool rotated,
            Rectangle sourceSpriteRect,
            GameTexture packTexture
        )
        {
            Filename = filename.Replace('\\', '/');
            Rect = rect;
            Rotated = rotated;
            SourceRect = sourceSpriteRect;
            PackTexture = packTexture;
        }

        public string Filename { get; set; }

        public Rectangle Rect { get; set; }

        public bool Rotated { get; set; }

        public Rectangle SourceRect { get; set; }

        public GameTexture PackTexture { get; set; }

    }

}
