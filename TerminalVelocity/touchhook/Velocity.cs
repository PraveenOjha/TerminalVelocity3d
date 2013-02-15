
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Security.Permissions;

namespace TerVel
{
    // Main app, WMToouchForm-derived, multi-touch aware form.
//Exercise2-Task1-Step6 
    public partial class Velocity
    {
        // Constructor
        
        public static IntPtr mainHandle;
        public Velocity()
        {   
            
            InitializeComponent();
            this.main = new Main();
            this.main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.main.Location = new System.Drawing.Point(0, 0);
            this.main.Name = "Game";
            this.main.Size = new System.Drawing.Size(1366, 768);
                   
            //this.VisibleChanged += new EventHandler(pause);
            
            this.main.TabIndex = 0;
            this.main.Text = "game";

            this.main.HandleCreated += new EventHandler(onhand);
            Cursor.Hide();
            this.main.Click+=Velocity_GotFocus;
            DebugBox.LostFocus+=Velocity_LostFocus;

            
            this.Controls.Add(main);
            
            try
            {
               //this.WindowState = FormWindowState.Maximized;
                //togglescreen();
            }
            catch(Exception){}
            
            
        }

        private void Velocity_LostFocus(object sender, EventArgs e)
        {
            Program.swap();
        }

        private void Velocity_GotFocus(object sender, EventArgs e)
        {
            Assets.focusStatee(true);
        }

        
        private void onhand(object sender, EventArgs e)
        {

            this.main.onload(this.main.Handle);
        }

       
              

          

        public Main main ;

      //public touch touchj { get; set; }

        public object handle { get; set; }
    }
         



    
}