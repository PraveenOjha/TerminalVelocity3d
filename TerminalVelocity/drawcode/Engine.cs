using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerVel
{
    public class engine
    {




        public static void setcolor(float r, float g, float b, float a)
        {
            Assets.color = new Microsoft.Xna.Framework.Color(r, g, b, a);

        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////Draw Call functions////////////////////////////////////////////


        public static void beginBatch(int i)//const char * path)
        {
            Main.gamescreen.batcherBridge.beignBatch(i);

        }

        public static void endBatch()
        {
            Main.gamescreen.batcherBridge.endBatch();
            return;
        }

        public static void spriteDraw(float x, float y, float width, float height, float angle, float pinx, float piny, int index)
        {
            Main.gamescreen.batcherBridge.drawSprite(x, y, width, height, angle, pinx, piny, index);
            return;
        }


        public static void scoreDraw(float x, float y, float width, float height, float angle, float pinx, float piny, int index)
        {
            Main.gamescreen.batcherBridge.scoreDraw(x, y, width, height, angle, pinx, piny, index);
            return;
        }




        public static void textDraw(float x, float y, float width, float height, int font, string text, float sx, float sy, float tx, float ty, string path)
        {
            Main.gamescreen.batcherBridge.drawText(x, y, width, height, font, text, sx, sy, tx, ty, path);
            return;
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////


        /////////////////////////////////Audio functions binding///////////////////////////////////////


        public static void playsound(string path)
        {

            /////	jmethodID  mid= g_env->GetMethodID(audio,"playsound","(Ljava/lang/string;)I");
            ///// 	jstring name = g_env->NewStringUTF(path);
            //// 	int l=g_env->CallIntMethod(Aud, mid,name);
            return;
        }

        public static void playsound(int i)
        {
            Main.gamescreen.batcherBridge.playsound(i);
            return;

        }
        //////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////off or on tells to off on play music 1/0///////////////////////////////
        public static void music(int offoron, string path)
        {
            Main.gamescreen.batcherBridge.music(offoron, path);
            return;

        }
        ///////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////Channel tells about adjusting  volume of sound effect or music////////////
        public static void adjustVolume(float vol, int channel, string name)
        {
            Main.gamescreen.batcherBridge.adjustvolume(vol, channel, name);
            return;
        }

        public static void vibration(int t)
        {
            Main.gamescreen.batcherBridge.vibration(t);
            return;

        }

        ///////////////////////////////////////////////////////////////////////////////////////
        //////////////perform android tasks like finishing app ,calling any context ,etc///////
        ///////////////////////////////////////////////////////////////////////////////////////
        public static void performtask(int t)
        {
            Main.gamescreen.batcherBridge.performtask(t);
            return;

        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////File function bindings/////////////////////////////////////////////

        public static void write(string path)
        {
            Main.gamescreen.ts_bridge.write(path);
            return;

        }
        public static void write(int score)
        {
            Main.gamescreen.batcherBridge.write(score);
            return;

        }
        public static string read(string name)
        {
            return Main.gamescreen.ts_bridge.read(name);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public static int assetsize(int i)
        {
            return  Assets.assetsize(i);

        }

        public static int assetdatasize(int i, int j)
        {
            return  Assets.assetdatasize(i, j);
        }

        public static string assetdataname(int i, int j)
        {

            return null;

        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////


        internal static void spriteDraw(double p1, double p2, int spritewidth, int spriteheight, int p3, int p4, int p5, int sprite)
        {
            spriteDraw((float)p1,(float)p2,(float)spritewidth,(float)spriteheight,(float)p3,(float)p4,(float)p5,(int)sprite);
        }

        internal static void spriteDraw(int p1, int p2, double p3, double p4, int p5, int p6, int p7, int p8)
        {
            spriteDraw((float)p1, (float)p2, (float)p3, (float)p4, (float)p5, (float)p6, (float)p7, (int)p8);
    
        }

        
    }

}
