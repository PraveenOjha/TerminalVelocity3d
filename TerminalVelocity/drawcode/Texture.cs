using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TerVel{
    public  class Texture
    {
        public Texture2D  tex;

        public Texture(string filename)
        {
             tex = Main.context.Content.Load<Texture2D>(filename.Substring(0,filename.IndexOf(".")));
            
        }

        internal void reload()
        {
          //tex.r
        }

        internal void dispose()
        {
            tex.Dispose();
        }
    }
}
