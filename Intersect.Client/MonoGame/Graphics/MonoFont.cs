using System;
using System.Diagnostics;

using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Graphics;
using Intersect.Logging;

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Intersect.Client.MonoGame.Graphics
{

    public class MonoFont : GameFont
    {

        private SpriteFont mFont;

        public MonoFont(string fontName, string fileName, int fontSize, ContentManager contentManager) : base(
            fontName, fontSize
        )
        {
            Debug.Assert(contentManager != null, nameof(contentManager) + " != null");
            try
            {
                fileName = GameContentManager.RemoveExtension(fileName);
                lock (contentManager)
                {
                    mFont = contentManager.Load<SpriteFont>(fileName);
                }
            }
            catch (Exception ex)
            {
                Log.Trace(ex);
            }
        }

        public override object GetFont()
        {
            return mFont;
        }

    }

}
