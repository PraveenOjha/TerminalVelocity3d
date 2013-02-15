
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Permissions;
using System.Threading;
using TerminalVelocity;
using Windows.Devices.Sensors;
using Windows.Foundation;


namespace TerVel
{

    public class Main : GraphicsDeviceControl
    {
        // Fields
        public Accelerometer acel = Accelerometer.GetDefault();
        static public Texture2D camerapre=null;
        static public Texture2D camerapre1 = null;
        static public Texture2D camerapre2 = null;
        private Camera2D camera;
        public ContentManager Content;
        public static Main context;
        private static int d1 = 0;
        private static int d2 = 0;
        public static int evd = -123;
        public static int evd1 = -123;
        public static GameScreen gamescreen;
        private static int lap = -1;
        private static int lastpointer = 0;
        private EventHandler Load;
        public static ButtonState oldbs = ButtonState.Released;
        public static KeyState oldent = KeyState.Up;
        public static KeyState oldks = KeyState.Up;
        private static int omx = 0;
        private static int omy = 0;
        public static bool Onloa = false;
        private static int opx = 0;
        private static int opy = 0;
        public static int pointerstate = -1;
        public static int pointerX;
        public static int pointerY;
        private int screenHeight;
        private int screenWidth;
        public static SpriteBatch spriteBatch;
        public static SpriteBatch spriteBatch1;
        private Stopwatch stopWatch;
        public Thread t;
        private const int TOUCHEVENTF_DOWN = 2;
        private const int TOUCHEVENTF_INRANGE = 8;
        private const int TOUCHEVENTF_MOVE = 1;
        private const int TOUCHEVENTF_NOCOALESCE = 0x20;
        private const int TOUCHEVENTF_PEN = 0x40;
        private const int TOUCHEVENTF_PRIMARY = 0x10;
        private const int TOUCHEVENTF_UP = 4;
        private const int TOUCHINPUTMASKF_CONTACTAREA = 4;
        private const int TOUCHINPUTMASKF_EXTRAINFO = 2;
        private const int TOUCHINPUTMASKF_TIMEFROMSYSTEM = 1;
        private int touchInputSize;
        private bool updated;
        private const int WM_TOUCHDOWN = 0x241;
        private const int WM_TOUCHMOVE = 0x240;
        private const int WM_TOUCHUP = 0x242;
        private ButtonState oms;
        private SimpleOrientationSensor orientationSensor= SimpleOrientationSensor.GetDefault();
        static public   intelperceptual Intelperceptual;
        static public  Microsoft.Xna.Framework.Graphics.GraphicsDevice GrDev;

        // Methods
        [SecurityPermission(SecurityAction.Demand)]
        public Main()
        {
            context = this;
            Assets.color = Color.White;
            this.touchInputSize = Marshal.SizeOf(new TOUCHINPUT());
        }

        private void Aceloro(Accelerometer sender, AccelerometerReadingChangedEventArgs args)
        {
            //gamescreen.batcherBridge.drawText(50f, 50f, 50f, 50f,1, args.Reading.AccelerationX.ToString(), 1f, 1f, 1f, 1f, "ns");
            //if(!Settings.vibrate)
           //  Game.Update1((float)args.Reading.AccelerationX*2, (float)args.Reading.AccelerationY, (float)args.Reading.AccelerationZ); 
            
        }

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32")]
        private static extern void CloseTouchInputHandle(IntPtr lParam);
        private bool DecodeTouch(ref System.Windows.Forms.Message m)
        {
            TOUCHINPUT[] touchinputArray;
            int cInputs = LoWord(m.WParam.ToInt32());
            try
            {
                touchinputArray = new TOUCHINPUT[cInputs];
            }
            catch (Exception exception)
            {
                Debug.Print("ERROR: Could not allocate inputs array");
                Debug.Print(exception.ToString());
                return false;
            }
            if (!GetTouchInputInfo(m.LParam, cInputs, touchinputArray, this.touchInputSize))
            {
                return false;
            }
            bool flag = false;
            for (int i = 0; i < cInputs; i++)
            {
                TOUCHINPUT touchinput = touchinputArray[i];
                EventHandler<EventArgs> handler = null;
                if ((touchinput.dwFlags & 2) != 0)
                {
                    handler = new EventHandler<EventArgs>(this.Touchdown);
                }
                else if ((touchinput.dwFlags & 4) != 0)
                {
                    handler = new EventHandler<EventArgs>(this.Touchup);
                }
                else if ((touchinput.dwFlags & 1) != 0)
                {
                    handler = new EventHandler<EventArgs>(this.TouchMove);
                }
                if (handler != null)
                {
                    EventArgs args;
                    try
                    {
                        args = new EventArgs();
                    }
                    catch (Exception exception2)
                    {
                        Debug.Print("Could not allocate WMTouchEventArgs");
                        Debug.Print(exception2.ToString());
                        continue;
                    }
                    args.ContactY = touchinput.cyContact / 100;
                    args.ContactX = touchinput.cxContact / 100;
                    args.Id = touchinput.dwID;
                    Microsoft.Xna.Framework.Point point = new Microsoft.Xna.Framework.Point(touchinput.x / 100, touchinput.y / 100);
                    args.LocationX = point.X;
                    args.LocationY = point.Y;
                    args.Time = touchinput.dwTime;
                    args.Mask = touchinput.dwMask;
                    args.Flags = touchinput.dwFlags;
                    handler(this, args);
                    flag = true;
                }
            }
            CloseTouchInputHandle(m.LParam);
            return flag;
        }

        protected override void Draw()
        {
            if (Intelperceptual == null&&intelperceptual.error!=1)
            Intelperceptual = new intelperceptual();
           // base.GraphicsDevice.Clear(Color.CornflowerBlue);
            if (Settings.or != 1)
            {
                Assets.focusStatee(false);
                spriteBatch.Begin();
                spriteBatch.Draw(Assets.wrn.tex, new Rectangle(0, 0, (int)Program.game.Width, (int)Program.game.Height), Color.White);
                spriteBatch.End();
                return;

            }
             
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, (((( this.camera.ViewMatrix * Matrix.CreateScale(1f, -1f, 0f)) * Matrix.CreateTranslation(0f, Assets.screenHeight, 0f)) * Matrix.CreateScale((float)(Assets.screenHeight / 500f))) * Matrix.CreateTranslation(Assets.screenWidth / 3.2f, -Assets.screenHeight / 1.8f, 0f)) * Matrix.CreateScale(((float)Program.game.Width) / Assets.screenWidth, ((float)Program.game.Height) / Assets.screenHeight, 0f));
             gamescreen.present(0.018f);
            spriteBatch.End();
            spriteBatch1.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, this.camera.ViewMatrix * Matrix.CreateScale(((float)Program.game.Width) / Assets.screenWidth, ((float)Program.game.Height) / Assets.screenHeight, 0f));
            spriteBatch1.Draw(Assets.side.tex, new Vector2(-Assets.screenWidth / 6.95f, 0f), Color.White);
            spriteBatch1.Draw(Assets.side.tex, new Vector2(Assets.screenWidth / 1.445f, 0f), Color.White);
            if (Assets.loaderp != 100)
            {
                spriteBatch1.Draw(Assets.splashscreen.tex, new Rectangle(0, 0, (int)Assets.screenWidth, (int)Assets.screenHeight), new Rectangle?(Assets.loadscreen.rectangle), Color.White);
            }
            spriteBatch1.End();
            if (Assets.loaderp == 100)
            {
                
                

                //Matrix m = (((this.camera.ViewMatrix * Matrix.CreateScale(1f, -1f, 0f)) * Matrix.CreateTranslation(0f, Assets.screenHeight, 0f)));
                //spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, m);
                //engine.textDraw(290f, 400f, 2f, 1f, 0, evd.ToString(), 1f, 1f, 0.3f, 1f, " ");
                //engine.textDraw(100f, 400f, 2f, 1f, 0, evd1.ToString(), 1f, 1f, 0.3f, 1f, " ");
                //spriteBatch.End();


                if(Game.gd.Gamestate==0)
               
                {spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, ((((this.camera.ViewMatrix * Matrix.CreateScale(1f, -1f, 0f)) * Matrix.CreateTranslation(0f, Assets.screenHeight, 0f)) * Matrix.CreateScale((float)(Assets.screenHeight / 500f))) * Matrix.CreateTranslation(Assets.screenWidth / 3.2f, -Assets.screenHeight / 1.8f, 0f)) * Matrix.CreateScale(((float)Program.game.Width) / Assets.screenWidth, ((float)Program.game.Height) / Assets.screenHeight, 0f));
                engine.textDraw(10f-90, 460f, 2f, 1f, 1, "Score", 1f, 1f, .4f, 1f, " ");
                Game.drawnumber((int)Game.gd.score, 80-90, 430, 15);
                engine.textDraw(10f - 90, 400f, 2f, 1f, 1, "Life", 1f, 1f, .4f, 1f, " ");
                Game.drawnumber((int)Game.gd.life, 50 - 90, 380, 15);
                engine.textDraw(200f+130, 460f, 2f, 1f, 1, "Battery", 1f, 1f, .3f, 1f, " ");
                Game.drawnumber(Game.gd.coins, (Game.gd.score > 999 ? 390 : 410), 430, 15);
                engine.setcolor(.8f, .8f, .8f, .8f);
                engine.textDraw(290f+50, 400f, 2f, 1f, 0, "II", 1f, 1f, .3f, 1f, " ");
                engine.setcolor(1f, 1f, 1f, 1f);
                spriteBatch.End();
                }




                spriteBatch.Begin();
                spriteBatch.Draw(Assets.textureregions[68].texhold.tex, new Vector2(Game.gd.pointx1, Game.gd.pointy1), Assets.textureregions[68].rectangle, Color.White, 90 * 80, new Vector2(50, 50), .4f, SpriteEffects.None, 0);
               // gamescreen.batcher.drawSprite(Game.gd.pointx, Game.gd.pointy, 20f, 20f, 30f, Assets.textureregions[0x44]);
                if(camerapre!=null)
                spriteBatch.Draw(camerapre,new Rectangle(80,360,320,240),Color.White);

                if (camerapre1 != null)
                spriteBatch.Draw(camerapre1,new Rectangle(80,600,320,240), Color.White);

                //if (camerapre2 != null)
               // spriteBatch.Draw(camerapre2, new Vector2(400, 100), Color.White);
                if (intelperceptual.device_lost)
                    Velocity.DebugBox.Text = "Camera disconnected";
                 ////   engine.textDraw(10f , 460f, 2f, 1f, 1, "Camera disconnected", 1f, 1f, .4f, 1f, " ");
                else
                    Velocity.DebugBox.Text = "" + Game.gd.pointx1 + "   " + Game.gd.pointy1;
                if(camerapre==null&&intelperceptual.error!=1)
                    Velocity.DebugBox.Text = "Wait Camera intialising";

                if (intelperceptual.error == 1)
                {
                    Velocity.DebugBox.Size = new System.Drawing.Size(350, 50);
                    Velocity.DebugBox.Text = "Sorry Error in  Camera intialising press esc thrice to quit";
                }
                evd = (int)Game.gd.pointx;
                evd1 = (int)Game.gd.pointy;
                spriteBatch.End();






            }
            this.inputUpdate();


            this.Invalidate();
        }

        internal void Exit()
        {
            this.t.Abort();
            System.Windows.Forms.Application.Exit();
        }

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32")]
        private static extern bool GetTouchInputInfo(IntPtr hTouchInput, int cInputs, [In, Out] TOUCHINPUT[] pInputs, int cbSize);
        protected override void Initialize()
        {
            
                

            Assets.screenWidth = 1366f;
            Assets.screenHeight = 768f;
            Assets.rand = new Random(0x53cac);
            this.Content = new ContentManager(base.Services, "");
            Assets.load(new GLGame());
            gamescreen = new GameScreen();
            Game.gd.Gamestate = 4;
            
            this.LoadContent();
            this.t = new Thread(new ThreadStart(this.RunInThread));
            this.t.IsBackground = true;
            this.t.Start();
            this.stopWatch = Stopwatch.StartNew();
            this.stopWatch.Start();
            Onloa = false;
        }

        protected void inputUpdate()
        {
            
            if ((Keyboard.GetState().IsKeyDown(Keys.Tab) && (Keyboard.GetState().IsKeyDown(Keys.LeftAlt) || Keyboard.GetState().IsKeyDown(Keys.RightAlt))) )
            {
                Program.swap();
            }

            if ((Keyboard.GetState().IsKeyDown(Keys.Enter) && (Keyboard.GetState().IsKeyDown(Keys.LeftAlt) || Keyboard.GetState().IsKeyDown(Keys.RightAlt))) && (oldent == KeyState.Up))
            {
                Program.toggle();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Enter) )
              oldent = KeyState.Down;
            else
            oldent = KeyState.Up;

            Vector2 left = GamePad.GetState(PlayerIndex.One).ThumbSticks.Left;
            gamescreen.update(150f);
            Game.Update(GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X, 0f, 0f);
            int posX = 0;
            int posY = 0;
            int state = 0;
            d1 = Math.Abs((int)(omx - Mouse.GetState().X)) + Math.Abs((int)(omy - Mouse.GetState().Y));
            d2 = Math.Abs((int)(opx - pointerX)) + Math.Abs((int)(opy - pointerY));
            if ((d1 == d2) && (lap != 2))
            {
                d1 = d2 + 2;
            }
            if (d1 > d2)
            {
                //posX = (Mouse.GetState().X - 420) - Program.game.Location.X;
                //posY = (Program.game.Height - Mouse.GetState().Y) + Program.game.Location.Y;

                posX = (Mouse.GetState().X) - Program.game.Location.X;
                posY = (Mouse.GetState().Y)- Program.game.Location.Y;
                if (Mouse.GetState().LeftButton == ButtonState.Released && oms==ButtonState.Pressed)
                {
                    state = 1;
                }
                lap = 1;
            }
            else
            {
              //  posX = (pointerX - 420) - Program.game.Location.X;
              //  posY = (Program.game.Height - pointerY) + Program.game.Location.Y;
                posX = (pointerX) - Program.game.Location.X;
                posY =(pointerY - Program.game.Location.Y);
                if ((lastpointer == 1) && (pointerstate == 0)&&d2<80)
                {
                    state = 1;
                }
                lap = 2;
            }
            lastpointer = pointerstate;
            omx = Mouse.GetState().X;
            omy = Mouse.GetState().Y;
            oms = Mouse.GetState().LeftButton;
            opx = pointerX;
            opy = pointerY;
            //posX = (int)((posX * Assets.screenWidth) / ((float)Program.game.Width));
            //posY = (int)((posY * Assets.screenHeight) / ((float)Program.game.Height));
         ///   Game.Update(posX,posY, state, 0);
            if (((oldbs == ButtonState.Pressed) && (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Released)) || ((oldks == KeyState.Down) && Keyboard.GetState().IsKeyUp(Keys.Escape)))
            {
                Game.Update(50, 1);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                Game.Update(20, 1);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                Game.Update(0x15, 1);
            }
            oldbs = GamePad.GetState(PlayerIndex.One).Buttons.Back;
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                oldks = KeyState.Down;
            }
            else
            {
                oldks = KeyState.Up;
            }
            base.Update();
            
        }

        protected void LoadContent()
        {
            GrDev = base.GraphicsDevice;
            spriteBatch = new SpriteBatch(base.GraphicsDevice);
            spriteBatch1 = new SpriteBatch(base.GraphicsDevice);
            this.camera = new Camera2D(new Viewport(0, 0, (int)Assets.screenWidth, (int)Assets.screenHeight));
            if (this.acel != null)
            {
                acel.ReportInterval = acel.MinimumReportInterval;
                acel.ReadingChanged += Aceloro;
                
            }

            if (this.orientationSensor != null)
            {
                Settings.orientation = this.orientationSensor.GetCurrentOrientation();
                switch (Settings.orientation)
                {
                    case SimpleOrientation.NotRotated: Settings.or = 1; break;
                    case SimpleOrientation.Faceup: Settings.or = 1; break;
                    case SimpleOrientation.Rotated180DegreesCounterclockwise: Settings.or = 2; break;
                    case SimpleOrientation.Rotated270DegreesCounterclockwise:Settings.or = 3; break;
                    case SimpleOrientation.Rotated90DegreesCounterclockwise:Settings.or = 4; break;
                }
                this.orientationSensor.OrientationChanged += OrientationChange;

            }

         
   
        }

        private void OrientationChange(SimpleOrientationSensor sender, SimpleOrientationSensorOrientationChangedEventArgs args)
        {

            Settings.orientation = this.orientationSensor.GetCurrentOrientation();
            switch (Settings.orientation)
            {
                case SimpleOrientation.NotRotated: Settings.or = 1; break;
                //case SimpleOrientation.Faceup: Settings.or = 0; break;
                case SimpleOrientation.Rotated180DegreesCounterclockwise: Settings.or = 2; break;
                case SimpleOrientation.Rotated270DegreesCounterclockwise: Settings.or = 3; break;
                case SimpleOrientation.Rotated90DegreesCounterclockwise: Settings.or = 4; break;
            } 
            
        }

        private static int LoWord(int number)
        {
            return (number & 0xffff);
        }

        internal void onload(IntPtr intPtr)
        {
            ulong ulFlags = 0L;
            try
            {
               // if (!RegisterTouchWindow(intPtr, ulFlags))
                {
                    Debug.Print("ERROR: Could not register window for touch");
                }
            }
            catch (Exception exception)
            {
                Debug.Print("ERROR: RegisterTouchWindow API not available");
                Debug.Print(exception.ToString());
            }
        }

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32")]
        private static extern bool RegisterTouchWindow(IntPtr hWnd, ulong ulFlags);
        private void RunInThread()
        {
            while (true)
            {
                this.Tick();
            }
        }

        private void Tick()
        {
            this.updated = false;
            if (this.stopWatch.ElapsedMilliseconds > 20L)
            {
                this.stopWatch.Restart();
                this.updated = true;
            }
            if (this.updated)
            {
                //base.Invalidate();
                FrameworkDispatcher.Update();
                if (!Onloa)
                {
                    Onloa = true;
                }
            }
        }

        private void Touchdown(object sender, EventArgs e)
        {
            if (e.IsPrimaryContact)
            {
                pointerstate = 0;
                pointerX = e.LocationX;
                pointerY = e.LocationY;
            }
        }

        private void TouchMove(object sender, EventArgs e)
        {
            if (e.IsPrimaryContact)
            {
                pointerX = e.LocationX;
                pointerY = e.LocationY;
            }
        }

        private void Touchup(object sender, EventArgs e)
        {
            if (e.IsPrimaryContact)
            {
                pointerstate = 1;
                pointerX = e.LocationX;
                pointerY = e.LocationY;
            }
        }

        protected void UnloadContent()
        {
        }

        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            bool flag;
            switch (m.Msg)
            {
                case 0x240:
                case 0x241:
                case 0x242:
                    flag = this.DecodeTouch(ref m);
                    break;

                default:
                    flag = false;
                    break;
            }
            try
            {
                if (flag)
                {
                    m.Result = new IntPtr(1);
                }
            }
            catch (Exception exception)
            {
                Debug.Print("ERROR: Could not allocate result ptr");
                Debug.Print(exception.ToString());
            }
            base.WndProc(ref m);
        }

        // Nested Types
        public class EventArgs : System.EventArgs
        {
            // Fields
            private int contactX;
            private int contactY;
            private int flags;
            private int id;
            private int mask;
            private int time;
            private int x;
            private int y;

            // Properties
            public int ContactX
            {
                get
                {
                    return this.contactX;
                }
                set
                {
                    this.contactX = value;
                }
            }

            public int ContactY
            {
                get
                {
                    return this.contactY;
                }
                set
                {
                    this.contactY = value;
                }
            }

            public int Flags
            {
                get
                {
                    return this.flags;
                }
                set
                {
                    this.flags = value;
                }
            }

            public int Id
            {
                get
                {
                    return this.id;
                }
                set
                {
                    this.id = value;
                }
            }

            public bool IsPrimaryContact
            {
                get
                {
                    return ((this.flags & 0x10) != 0);
                }
            }

            public int LocationX
            {
                get
                {
                    return this.x;
                }
                set
                {
                    this.x = value;
                }
            }

            public int LocationY
            {
                get
                {
                    return this.y;
                }
                set
                {
                    this.y = value;
                }
            }

            public int Mask
            {
                get
                {
                    return this.mask;
                }
                set
                {
                    this.mask = value;
                }
            }

            public int Time
            {
                get
                {
                    return this.time;
                }
                set
                {
                    this.time = value;
                }
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct POINTS
        {
            public short x;
            public short y;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct TOUCHINPUT
        {
            public int x;
            public int y;
            public IntPtr hSource;
            public int dwID;
            public int dwFlags;
            public int dwMask;
            public int dwTime;
            public IntPtr dwExtraInfo;
            public int cxContact;
            public int cyContact;
        }
    }



}