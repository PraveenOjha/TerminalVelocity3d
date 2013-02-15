using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TerVel
{
    public class Music
    {

         Song song;
        public Music(string filename)
        {
            song=Main.context.Content.Load<Song>("m1");//"filename);
            
        }
        
        internal void play()
        {
            try
            {
                
                MediaPlayer.Play(song); 
            
            
            }
            catch(Exception e)
            {
                song = Main.context.Content.Load<Song>("m1");//"filename);
            }
        }

        internal void stop()
        {
            MediaPlayer.Stop();
        }

        internal void resume()
        {
            MediaPlayer.Resume();
        }
        internal bool isPlaying()
        {
            if (MediaPlayer.State == MediaState.Playing)
            return true;
            else
            return false;
            
        }

        internal void setVolume(float p)
        {
            MediaPlayer.Volume = p;
        }

        internal void pause()
        {
            MediaPlayer.Pause();
        }

        internal void setLooping(bool p)
        {   MediaPlayer.IsRepeating = p;
        }
    }
}
