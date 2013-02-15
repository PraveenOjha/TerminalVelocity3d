using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TerVel
{
    public class TextureRegion
    {
        public Texture texhold;
        public Rectangle rectangle;
        public TextureRegion(Texture splashscreen, int p1, int p2, int p3, int p4)
        {
            // TODO: Complete member initialization
            this.texhold = splashscreen;
            rectangle = new Rectangle(p1, p2, p3, p4);
            
        }
    }
}
