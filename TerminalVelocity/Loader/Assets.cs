using TerminalVelocity;
using System.Collections.Generic;
using System.Threading;
using System;

namespace TerVel
{


    public class Assets
    {
        public static int max_tex = 20;
        public static bool isMusicPlaying = false;
        public static int max_texrg = 1000;
        public static float screenHeight = 480;
        public static float screenWidth = 320;
        public static Texture splashscreen,side,wrn;
        public static TextureRegion loadscreen, circ, loading;
        public static Texture[] textures;
        public static TextureRegion[] textureregions;
        //public static Animation [] animations;
        public static FontExtension[] fonts;
        public static List<string> TextureNames = new List<string>(60);
        public static TexturRegionName TextureRegionNames = new TexturRegionName();
        public static List<string> SoundNames = new List<string>();
        public static List<string> FontNames = new List<string>();
        public static List<string> MusicNames = new List<string>();
        public static Music[] music;
        public static Sound[] sounds;
        public static int texcount = 0;
        public static int texregcount = 0;
        public static int fontcount = 0;
        public static int soundcount = 0;
        public static int MusicCount = 0;
        public static int loaderp;
        public static bool keyhandle;
        public static int fps;
        public static int vibra = -1;
        public static int performtask;
        public static bool quit;
        public static int CurrentScore;
        public static Random rand;
        public static void load(GLGame game)
        {

            //Load Textures and define texture regions LoadFonts///////////
            Assets.game = game;
            splashscreen = new Texture("splash.png");
            wrn = new Texture("wrg.png");
            side = new Texture("side.png");
            loadscreen = new TextureRegion(splashscreen, 1, 205, 800, 600);
            circ = new TextureRegion(side, 0, 0,600 ,1000);
            loading = new TextureRegion(splashscreen, 803, 705, 156, 55);
            Settings.load();
            Game.gd = new GameData(Settings.musicEnabled,Settings.soundEnabled);
            Game.vm = new ViewManager();
            Game.r1 = new RegionData();
            Game.r2 = new RegionData();
            Game.r3 = new RegionData();
            new Thread(delegate()
           {
               bgload();
           }).Start();






        }






        public static void bgload()
        {



            texcount = 0;
            texregcount = 0;
            fontcount = 0;
            loaderparser.loadindex("asset.items");
            Settings.soundvolume = .7f;
            Settings.musicvolume = .3f;


            //////////////////////////////////////////////////////////////

            textures = new Texture[TextureNames.Count];
            textureregions = new TextureRegion[TextureRegionNames.size()];
            sounds = new Sound[SoundNames.Count];
            music = new Music[MusicNames.Count];
            fonts = new FontExtension[2];
            music[Settings.musicindex] = new Music(MusicNames[Settings.musicindex]);
            music[Settings.musicindex].setLooping(true);
            music[Settings.musicindex].setVolume(Settings.musicvolume);

            if (Settings.musicEnabled)
            { music[Settings.musicindex].play();
            isMusicPlaying = true;
            }


            adjustvolume(Settings.musicvolume, 1);
            adjustvolume(Settings.soundvolume, 0);


            //load all textures reported by pack files/////////////
            int i = 0;
            while (i < TextureNames.Count)
            {
                textures[i] = new Texture(TextureNames[i]);
                i++;
            }
            Assets.loaderp = 60;
            i = 0;
            ///////////////////////////////Load fonts//////////////////////////////////
            while (i < 2)
            {
                string[] s = { "font1", "font2" };
                int[] w = { 50, 50, 50, 50 };
                int x = TexturRegionName.x[(TexturRegionName.texturegionname.IndexOf(s[i]))] + 9;
                int y = TexturRegionName.y[(TexturRegionName.texturegionname.IndexOf(s[i]))] + 11;
                fonts[i]=new FontExtension(textures[TextureNames.IndexOf(FontNames[0])],x,y,16,w[i],w[i+2]);
                i++;
            }
            i = 0;
            Assets.loaderp = 65;
            //////////////load all textures regions reported by pack files////////////////
            while (i < TexturRegionName.texturename.Count)
            {
                int x, y, w, h;
                x = TexturRegionName.x[i];
                y = TexturRegionName.y[i];
                w = TexturRegionName.sizex[i];
                h = TexturRegionName.sizey[i];
                textureregions[i] = new TextureRegion(gettexture(TexturRegionName.texturename[i]), x, y, w, h);
                i++;
                Assets.loaderp = 65 + 30 * (i * 100 / TexturRegionName.texturename.Count) / 100;
            } i = 0;
            Assets.loaderp = 95;
            ///////////////load all sounds reported by pack files/////////////////////////
            sounds[0] = new Sound(SoundNames[0]);
            sounds[1] = new Sound(SoundNames[2]);
            sounds[2] = new Sound(SoundNames[1]);

            /*/
             while(i<SoundNames.size())
    	 
           { sounds[i] = new Sound(SoundNames(i));
           i++;
           }
           //*/
            Assets.loaderp = 100;
            //////////////////////////////////////////Load all Patterns/////////////////////////////

        }

        ///////////////////////////////////////////Reload Textures.///////////////////////////////
        public static void reload()
        {
            Assets.splashscreen.reload();
            //final Runnable bg = new Runnable() 
            //{@Override
            //public void run() 
            //{
            bgreload();
            //}
            //};
            //GLGame.glView.queueEvent(bg);  

        }






        protected static void bgreload()
        {
            int i = 0;
            Assets.loaderp = 0;
            while (i < texcount)
            {
                textures[i++].reload();
                Assets.loaderp = 100 * i / texcount;
            }
            if (Settings.musicEnabled)
                music[Settings.musicindex].play();
            Assets.loaderp = 100;// TODO Auto-generated method stub

        }

        public static void playSound(Sound sound)
        {
            if (Settings.soundEnabled)
                sound.play(Settings.soundvolume);
            //sound.play(0.5f);
        }

        //////////////////////////////////////////////////Dispose Textures.////////////////////////////
        public static void dispose()
        {  //Dispose textures



            int i = 0;
            while (i < texcount)
            {
                textures[i++].dispose();
            }
            Assets.loaderp = 0;
        }

        public static Texture gettexture(string name)
        {
            return textures[TextureNames.IndexOf(name)];
        }


        public static TextureRegion gettextureregion(string name)
        {
            return textureregions[TexturRegionName.texturegionname.IndexOf(name)];

        }




        public static Sound getsound(string name)
        {
            return sounds[SoundNames.IndexOf(name)];
        }




        public static void playMusic()
        {
            // TODO Auto-generated method stub
            if (!(music[Settings.musicindex].isPlaying()))
            {
                music[Settings.musicindex].play();
            }


        }

        public static void stopMusic()
        {
            Settings.musicEnabled = false;
            music[Settings.musicindex].stop();

        }

        public static void adjustvolume(float v, int m)
        {
            if (m == 1)
            { Settings.soundvolume = v; }
            else
            {
                Settings.musicvolume = v;
                music[Settings.musicindex].setVolume(Settings.musicvolume);
            }
        }

        public static void vibrate(int patternindex)
        {
            if (patternindex == -1)
                return;

            //Vibrator v = (Vibrator)TerVel.contex.getSystemService(Context.VIBRATOR_SERVICE);
            //long[] pattern = { 0,10,10,20};
            //v.vibrate(pattern, -1);

        }


        public static int assetsize(int i)
        {
            switch (i)
            {
                case 1: return Assets.TextureNames.Count;
                case 2: return TexturRegionName.texturegionname.Count;
                case 3: return Assets.SoundNames.Count;
                case 4: return (int)Assets.screenHeight;
                case 5: return (int)Assets.screenWidth;

            }
            return -1;
        }
        public static string assetnames(int i, int index)
        {
            if (i == 1)
                return Assets.TextureNames[index];
            if (i == 2)
                return TexturRegionName.texturename[index];
            if (i == 3)
                return TexturRegionName.texturegionname[index];
            if (i == 4)
                return Assets.SoundNames[index];
            else
                return null;
        }

        public static int assetdatasize(int i, int index)
        {//send texture regions width
            if (i == 1)
                return TexturRegionName.sizex[index];
            //send texture region height
            if (i == 2)
                return TexturRegionName.sizey[index];
            if (i == 3)
                return TextureNames.IndexOf(TexturRegionName.texturename[index]);

            //Send sound list
            else
                return -1;
        }

        public static void focusStatee(bool hasFocus)
        {

            if (Assets.music == null)
             return;
            
            if (Assets.music[0] == null)
            {
                return; 
            }
            
            if (!hasFocus && isMusicPlaying == true)
            {
                Assets.music[0].pause(); 
                isMusicPlaying = false;
            }
            else if (hasFocus && isMusicPlaying == false&&Settings.musicEnabled==true)
            {   Assets.music[0].resume(); 
                isMusicPlaying = true;
            }

            if (!hasFocus)
            {
                if(Game.gd.Gamestate==0)
                NativeFun.poststate(3);
                if(Main.Intelperceptual!=null)
                Main.Intelperceptual.dispose();
                // TODO Auto-generated method stub
               return;
            }


        }



        public static GLGame game { get; set; }

        public static Microsoft.Xna.Framework.Color color { get; set; }


    }
}