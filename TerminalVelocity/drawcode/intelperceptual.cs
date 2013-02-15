using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TerVel
{
    public class intelperceptual  : UtilMPipeline
    {


        static public PXCMSession session;
        static public PXCMBase gesture_t;
        static public int error;
        static public pxcmStatus sts;
        static public PXCMGesture gesture;
        static public UtilMCapture capture;
        static public bool device_lost = false;
        static public bool loop;
        private System.Drawing.Bitmap bmp;
  
       public  intelperceptual():base()
       {init();
       }
        public  void init()
        {

            EnableGesture();
            EnableImage(PXCMImage.ColorFormat.COLOR_FORMAT_RGB24);
            EnableImage(PXCMImage.ColorFormat.COLOR_FORMAT_DEPTH);
            device_lost = false;
            loop = true;
            Thread t=new Thread(loopframes);
            t.IsBackground = true;
            t.Start();
	    }

        private void loopframes()
        {
            error = 0;
            if (!LoopFrames())
            {
                Console.WriteLine("Unable to intialise pipline");
                error = 1;
            }
             try
            {
                if(Main.Intelperceptual!=null&&!device_lost)
                Dispose();
            }catch(Exception E){ }
            Main.Intelperceptual = null;
        }
	    
        public void dispose()
        { loop=false;
         }

        public override void OnGesture(ref PXCMGesture.Gesture data) 
        {
            if (data.label == PXCMGesture.Gesture.Label.LABEL_POSE_THUMB_DOWN)
            {
                if (Game.gd.Gamestate == 0)
                    Game.gd.Gamestate = 1;
            }
		    if (data.active) Console.WriteLine("OnGesture("+data.label+")");
	    }
        
        public override bool OnDisconnect()
        {
            if (!device_lost) Console.WriteLine("Device disconnected");
            device_lost = true;
            return base.OnDisconnect();
        }
        
        public override void OnReconnect()
        {
            Console.WriteLine("Device reconnected");
            device_lost = false;
        }
	    
        public override bool OnNewFrame()
        {
            float x, y;
            PXCMGesture gesture = QueryGesture();
            PXCMGesture.GeoNode ndata;
            pxcmStatus sts = gesture.QueryNodeData(0, PXCMGesture.GeoNode.Label.LABEL_BODY_HAND_PRIMARY, out ndata);
            if (sts >= pxcmStatus.PXCM_STATUS_NO_ERROR)
            {
                Console.WriteLine("node HAND_MIDDLE ({0},{1},{2},{3},{4})", ndata.positionImage.x, ndata.positionImage.y, ndata.confidence, ndata.openness, ndata.opennessState);
                x = ndata.positionImage.x;
                if (x > 260) x = 260; if (x < 40) x = 40;
                x = x - 40;
               //////////////////////////////////////////////////
                x = 220 - x;
                if (ndata.opennessState == PXCMGesture.GeoNode.Openness.LABEL_CLOSE)
                Game.Update1((float)(x-110)/110, 1, 1);
           
            //
                /*
              }
            sts = gesture.QueryNodeData(0, PXCMGesture.GeoNode.Label.LABEL_BODY_HAND_PRIMARY, out ndata);
            if (sts >= pxcmStatus.PXCM_STATUS_NO_ERROR)
            {
                Console.WriteLine("node HAND_MIDDLE ({0},{1},{2},{3},{4})", ndata.positionImage.x, ndata.positionImage.y, ndata.confidence, ndata.openness, ndata.opennessState);
                //*/
                int state;
                if (ndata.opennessState == PXCMGesture.GeoNode.Openness.LABEL_OPEN)
                    state = 0;
                else
                    state = 1;

              //  Game.Update((float)Program.game.Width * (160 - ndata.positionImage.x) / 160, (float)Program.game.Height * (ndata.positionImage.y - 100) / 100, state, 0);
                
                    x = ndata.positionImage.x;
                    y = ndata.positionImage.y;
                    ////////////////////////////////////////////////
                    ///////////////Smoothing of coordinate nad removing data outside less presice rang//////////////
                    if (x > 260) x = 260; if (x < 40) x = 40;
                    if (y > 200) y = 200; if (y < 20) y = 20;

                    x = x - 40;
                    y = y - 20;
                    //////////////////////////////////////////////////
                    x = 220 - x;
                if(Game.gd.Gamestate!=0)
                  Game.Update(Program.game.Width*x/220,Program.game.Height*y/180, state, 0);
                
            
            }
             PXCMImage img = QueryImage(PXCMImage.ImageType.IMAGE_TYPE_COLOR);
             if(img!=null)
             img.QueryBitmap(QuerySession(),out bmp);
             if(bmp!=null) 
             Main.camerapre = GetTexture(Main.GrDev,bmp);
             bmp = null; 
             
             img = QueryImage(PXCMImage.ImageType.IMAGE_TYPE_DEPTH);
             if (img != null)
                 img.QueryBitmap(QuerySession(), out bmp);
             if (bmp != null)
                 Main.camerapre1 = GetTexture(Main.GrDev, bmp);

             bmp = null;

             img = QueryImage(PXCMImage.ImageType.IMAGE_TYPE_MASK);
             if (img != null)
                 img.QueryBitmap(QuerySession(), out bmp);
             if (bmp != null)
                 Main.camerapre2 = GetTexture(Main.GrDev, bmp);


            
            
            
            return (loop);


	    }

        private Texture2D GetTexture(GraphicsDevice dev, System.Drawing.Bitmap bmp)
        {
            try
            {
                int[] imgData = new int[bmp.Width * bmp.Height];
                Texture2D texture = new Texture2D(dev, bmp.Width, bmp.Height);

                unsafe
                {
                    // lock bitmap
                    System.Drawing.Imaging.BitmapData origdata =
                        bmp.LockBits(new System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height), System.Drawing.Imaging.ImageLockMode.ReadOnly, bmp.PixelFormat);

                    uint* byteData = (uint*)origdata.Scan0;

                    // Switch bgra -> rgba
                    for (int i = 0; i < imgData.Length; i++)
                    {
                        byteData[i] = (byteData[i] & 0x000000ff) << 16 | (byteData[i] & 0x0000FF00) | (byteData[i] & 0x00FF0000) >> 16 | (byteData[i] & 0xFF000000);
                    }

                    // copy data
                    System.Runtime.InteropServices.Marshal.Copy(origdata.Scan0, imgData, 0, bmp.Width * bmp.Height);

                    byteData = null;

                    // unlock bitmap
                    bmp.UnlockBits(origdata);
                }

                texture.SetData(imgData);

                return texture;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        
    /*


         
         static public PXCMSession session;
         static public PXCMBase gesture_t;
         static public int error;
         static public pxcmStatus sts;
         static public PXCMGesture gesture;
         static public UtilMCapture capture;
         static public bool device_lost = false;
         static public bool loop;
         static public void init()
         {
              sts = PXCMSession.CreateInstance(out session);

              error = 0;
             if (sts < pxcmStatus.PXCM_STATUS_NO_ERROR)
             {
                 error = 1;
                 Console.WriteLine("Failed to create the SDK session");
                 return;
             }

             // Gesture Module

             sts = session.CreateImpl(PXCMGesture.CUID, out gesture_t);
             if (sts < pxcmStatus.PXCM_STATUS_NO_ERROR)
             {
                 error = 2;
                 Console.WriteLine("Failed to load any gesture recognition module");
                 session.Dispose();
                 return;
             }
              gesture = (PXCMGesture)gesture_t;

             PXCMGesture.ProfileInfo pinfo;
             sts = gesture.QueryProfile(0, out pinfo);
             capture = new UtilMCapture(session);
             sts = capture.LocateStreams(ref pinfo.inputs);
             if (sts < pxcmStatus.PXCM_STATUS_NO_ERROR)
             {
                 error = 3;
                 Console.WriteLine("Failed to locate a capture module");
                 gesture.Dispose();
                 capture.Dispose();
                 session.Dispose();
                 return;
             }
             sts = gesture.SetProfile(ref pinfo);
             sts = gesture.SubscribeGesture(100, OnGesure);
            
             device_lost = false;
             loop = true;
             Thread t = new Thread(onimage);
             t.IsBackground = true;
            
             t.Start();
             
             
         }


         static public void onimage()
           {
             PXCMImage[] images = new PXCMImage[PXCMCapture.VideoStream.STREAM_LIMIT];
             PXCMScheduler.SyncPoint[] sps = new PXCMScheduler.SyncPoint[2];
             
             for (;loop ; )
             {
                 sts = capture.ReadStreamAsync(images, out sps[0]);
                 if (sts < pxcmStatus.PXCM_STATUS_NO_ERROR)
                 {
                     if (sts == pxcmStatus.PXCM_STATUS_DEVICE_LOST)
                     {
                         if (!device_lost) Console.WriteLine("Device disconnected");
                         device_lost = true; 
                         continue;
                     }
                     Console.WriteLine("Device failed\n");
                     break;
                 }
                 if (device_lost)
                 {
                     Console.WriteLine("Device reconnected");
                     device_lost = false;
                 }

                 sts = gesture.ProcessImageAsync(images, out sps[1]);
                 if (sts < pxcmStatus.PXCM_STATUS_NO_ERROR) break;

                 PXCMScheduler.SyncPoint.SynchronizeEx(sps);
                 if (sps[0].Synchronize(0) >= pxcmStatus.PXCM_STATUS_NO_ERROR)
                 {int state=0;
                     PXCMGesture.GeoNode data;
                     sts = gesture.QueryNodeData(0, PXCMGesture.GeoNode.Label.LABEL_BODY_HAND_PRIMARY | PXCMGesture.GeoNode.Label.LABEL_HAND_MIDDLE, out data);
                     if (sts >= pxcmStatus.PXCM_STATUS_NO_ERROR)
                     Console.WriteLine("[node] {0}, {1}, {2}", data.positionImage.x, data.positionImage.y, data.timeStamp);
                     Game.Update1((float)(160-data.positionImage.x)/ 160, 1, 1);
                    if(data.openness>50)
                         state=1;
                     else
                        state=0;

                 // Game.Update((float)Program.game.Width * (160-data.positionImage.x ) / 160, (float)Program.game.Height* (data.positionImage.y - 120) / 120, state, 0);
                 }

                 foreach (PXCMScheduler.SyncPoint s in sps) if (s != null) s.Dispose();
                 foreach (PXCMImage i in images) if (i != null) i.Dispose();
             }
             gesture.Dispose();
             capture.Dispose();
             session.Dispose();

          
         }
         static public void dispose()
         {
             loop = false;
             
         }
         static void OnGesure(ref PXCMGesture.Gesture gesture)
         {
              
         }
    */
    }
}
