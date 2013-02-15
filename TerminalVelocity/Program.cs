using System;
using System.Globalization;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace TerVel
{
//#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static Form game;
        
        [STAThread]
        static void Main(string[] args)
        {
            if (System.Diagnostics.Process.GetProcessesByName(System.IO.Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetEntryAssembly().Location)).Length > 1)
            {
                MessageBox.Show("Game  already running");
                return;
            }
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(game= new Velocity());
            

            Application.ApplicationExit+=new EventHandler(exit);
            
   }

private static void exit(object sender, EventArgs e)
{
 	
}

            
            //using (game = new Main())
            //{

             //   game.Run();
            //}
public static int i = 1;

internal static void swap()
{
        Assets.focusStatee(false);
        game.WindowState = FormWindowState.Minimized;

}

internal static void toggle()
{


    ////return;
    //if (i == 1)
    //{
    //    Assets.focusStatee(false);
    //    game.WindowState = FormWindowState.Minimized;
     
    //}//.Maximized;
    return;
    
    
    i*=(-1);
    if(i==1)
    {
        game.FormBorderStyle = FormBorderStyle.None;
        game.WindowState = FormWindowState.Maximized;
        game.TopMost = true;
        
        game.Bounds = Screen.PrimaryScreen.Bounds;
        
        
    }
    else
    {
        game.WindowState = FormWindowState.Normal;
        game.FormBorderStyle = FormBorderStyle.Fixed3D;
        game.TopMost = true;
        game.Height = 768;
        game.Width = 1366; 
    
    }

    
}
    }
    }
//#endif

