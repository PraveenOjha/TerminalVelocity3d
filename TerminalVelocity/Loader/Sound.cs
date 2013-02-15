
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
namespace TerVel
{
    public class Sound 
    {
        SoundEffect soundEffect;

        public Sound(string filename)
        {
           soundEffect=Main.context.Content.Load<SoundEffect>("S"+filename.Substring(0,filename.IndexOf(".")));
            
        }
        public void play()
        {
            soundEffect.Play();
        }


        internal void play(float p)
        {
            play();

        }
    }
}
