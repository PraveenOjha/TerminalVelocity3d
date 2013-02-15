using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows;
using System.Drawing;

namespace TerVel
{
 

/// <summary>
/// Selected Win API Function Calls
/// </summary>
/// 

      class clientRect
        {
            public System.Drawing.Point location;
            public int width;
            public int height;
        };
        // this should be in the scope your class
           
      
class  FormState
{
    
           static clientRect restore;
             static   bool fullscreen = false;

        /// <summary>
        /// Makes the form either fullscreen, or restores it to it's original size/location
        /// </summary>
         static void Fullscreen(Form f)
        { 
            if (fullscreen == false)
            {
                restore.location = f.Location;
                restore.width = f.Width;
                restore.height =f.Height;
                f.TopMost = true;
                f.Location = new System.Drawing.Point(0,0);
                f.FormBorderStyle = FormBorderStyle.None;
                f.Width = Screen.PrimaryScreen.Bounds.Width;
                f.Height = Screen.PrimaryScreen.Bounds.Height;
            }
            else
            {
                f.TopMost = false;
                f.Location = restore.location;
                f.Width = restore.width;
                f.Height =restore.height;
                                // these are the two variables you may wish to change, depending
                                // on the design of your form (WindowState and FormBorderStyle)
                f.WindowState = FormWindowState.Normal;
                f.FormBorderStyle = FormBorderStyle.Sizable;
            }
        }

        internal static void Maximize(Velocity form1)
         {
             fullscreen = true;
           Fullscreen(form1);
        }

        internal static void Restore(Velocity form1)
        {   fullscreen = false;
            Fullscreen(form1); 
        }

        internal static void Toggle()
        {
            throw new NotImplementedException();
        }
}
}
