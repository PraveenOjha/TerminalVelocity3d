using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Windows;
using System.Security.Permissions;
using System.Windows.Forms;
namespace TerVel
{
    class hook
    {

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr GetModuleHandle(string name);
        public delegate int HookProc(int nCode, IntPtr wParam, IntPtr lParam);
        //Declare the hook handle as an int.
        static int hHook = 0;

        //Declare the mouse hook constant.
        //For other hook types, you can obtain these values from Winuser.h in the Microsoft SDK.
        public const int WH_MOUSE = 7;
        public const int WM_POINTERUPDATE = 0x0245;
        public const int WM_POINTERDOWN = 0x0246;
        public const int WM_POINTERUP = 0x0247;
        public const int WM_POINTERENTER = 0x0249;
        public const int WM_POINTERLEAVE = 0x024A;

        static private System.Windows.Forms.Button button1;

        //Declare MouseHookProcedure as a HookProc type.
        static HookProc MouseHookProcedure;

        //Declare the wrapper managed POINT class.

        //Declare the wrapper managed MouseHookStruct class.

        public class MouseHookStruct
        {
            public POINT pt;
            public int hwnd;
            public int wHitTestCode;
            public int dwExtraInfo;
        }
        //This is the Import for the SetWindowsHookEx function.
        //Use this function to install a thread-specific hook.
        [DllImport("user32.dll", CharSet = CharSet.Auto,
         CallingConvention = CallingConvention.StdCall)]
        public static extern int SetWindowsHookEx(int idHook, HookProc lpfn,
        IntPtr hInstance, int threadId);

        //This is the Import for the UnhookWindowsHookEx function.
        //Call this function to uninstall the hook.
        [DllImport("user32.dll", CharSet = CharSet.Auto,
         CallingConvention = CallingConvention.StdCall)]
        public static extern bool UnhookWindowsHookEx(int idHook);

        //This is the Import for the CallNextHookEx function.
        //Use this function to pass the hook information to the next hook procedure in chain.
        [DllImport("user32.dll", CharSet = CharSet.Auto,
         CallingConvention = CallingConvention.StdCall)]
        public static extern int CallNextHookEx(int idHook, int nCode, IntPtr wParam, IntPtr lParam);





        public static void togglehook()
        {
            //if (hHook == 0)
            //{
            //    // Create an instance of HookProc.

            //    MouseHookProcedure = new HookProc(MouseHookProc);
            //    //bool a = RegisterTouchWindow(Program.game.Window.Handle, 0);
            //    hHook = SetWindowsHookEx(3, MouseHookProcedure, (IntPtr)0, AppDomain.GetCurrentThreadId());
            //    //If the SetWindowsHookEx function fails.
            //    if (hHook == 0)
            //    {
            //        System.Windows.Forms.MessageBox.Show("SetWindowsHookEx Failed");
            //        return;
            //    }
            //    //button1.Text = "UnHook Windows Hook";
            //}
            //else
            //{
            //    bool ret = UnhookWindowsHookEx(hHook);
            //    //If the UnhookWindowsHookEx function fails.
            //    if (ret == false)
            //    {
            //        System.Windows.Forms.MessageBox.Show("UnhookWindowsHookEx Failed");
            //        return;
            //    }
            //    hHook = 0;
            //    //button1.Text = "Set Windows Hook";

            //}
        }



        private static int MouseHookPro(int nCode, IntPtr wParam, IntPtr lParam)
        {
            MouseHookStruct MyMouseHookStruct = (MouseHookStruct)Marshal.PtrToStructure(lParam, typeof(MouseHookStruct));

            if (nCode < 0)
            {
                return CallNextHookEx(hHook, nCode, wParam, lParam);
            }
            else
            {
                //Create a string variable that shows the current mouse coordinates.
                //int state = 0;
                //if ((int)Mouse.GetState().LeftButton == 1)
                //{
                //   state = 1;
                // }
                //Game.Update((MyMouseHookStruct.pt.x - 690) * (500) / Assets.screenHeight, (Assets.screenHeight - MyMouseHookStruct.pt.y+150) * (500) / Assets.screenHeight ,state, 0);
                return CallNextHookEx(hHook, nCode, wParam, lParam);
            }
        }


        private delegate IntPtr LowLevelMouseProc(int nCode, IntPtr wParam, IntPtr lParam);
        public static int state = 0;
        private static int MouseHookProc(int nCode, IntPtr wParam, IntPtr lParam)
        {
            state = 0;
            if (nCode >= 0)
            {


                Main.evd = (int)wParam;
                Main.evd1 = (int)lParam;
                bool handled;

                handled = DecodeTouch(wParam);

                if (MouseMessages.WM_LBUTTONDOWN == (MouseMessages)wParam)
                    state = 1;
                MSLLHOOKSTRUCT hookStruct = (MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(MSLLHOOKSTRUCT));
                // Game.Update((hookStruct.pt.x - 690) * (500) / Assets.screenHeight, (Assets.screenHeight - hookStruct.pt.y + 150) * (500) / Assets.screenHeight, state, 0);
                //Console.WriteLine(hookStruct.pt.x + ", " + hookStruct.pt.y);
            }
            return CallNextHookEx(hHook, nCode, wParam, lParam);
        }



        private const int WH_MOUSE_LL = 14;

        private enum MouseMessages
        {
            WM_LBUTTONDOWN = 0x0201,
            WM_LBUTTONUP = 0x0202,
            WM_MOUSEMOVE = 0x0200,
            WM_MOUSEWHEEL = 0x020A,
            WM_RBUTTONDOWN = 0x0204,
            WM_RBUTTONUP = 0x0205
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int x;
            public int y;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct MSLLHOOKSTRUCT
        {
            public POINT pt;
            public uint mouseData;
            public uint flags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        //[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        //private static extern IntPtr SetWindowsHookEx(int idHook,LowLevelMouseProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        //[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        //private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
        //    IntPtr wParam, IntPtr lParam);

        //[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        //private static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("user32")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool RegisterTouchWindow(IntPtr hWnd, uint ulFlags);







        private static bool DecodeTouch(IntPtr WParam)
        {
            // More than one touchinput may be associated with a touch message,
            // so an array is needed to get all event information.
            int inputCount;

            if(WParam!=null)
            inputCount = WParam.ToInt32(); // Number of touch inputs, actual per-contact messages


            return false;
        }



















    }





}






     