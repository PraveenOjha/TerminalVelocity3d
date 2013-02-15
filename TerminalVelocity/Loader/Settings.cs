
using System;
using System.IO;
using System.Windows;
using Windows.Devices.Sensors;
namespace TerVel
{

public class Settings {
	
    public static bool soundEnabled ;
    public static bool musicEnabled ;
	public static bool sound;
	public static bool music;
	public static bool vibrate;
	public static float soundvolume=0.8F;
	public static float musicvolume=0.3F;
	public static int musicindex=0;
    
    public  static int[] highscores = new int[] {0,0,0,0,0,0,0,0,0,0};
    public static string file = System.Windows.Forms.Application.UserAppDataPath + ".tervel";
    public static SimpleOrientation orientation = SimpleOrientation.NotRotated;
    public static int or;

    public static void load() 
    {    try
         {
             StreamReader Streamin = new System.IO.StreamReader(file);
             soundEnabled = bool.Parse(Streamin.ReadLine());
             musicEnabled = bool.Parse(Streamin.ReadLine());
       
            for (int i = 0; i < 5; i++)
            {
                highscores[i] = int.Parse(Streamin.ReadLine());
            }
            Streamin.Close();
        }
        catch (Exception )
        { 
        }
    }

    public static void save() 
    {
         try {
             StreamWriter Streamout = new System.IO.StreamWriter(file);
             Streamout.Write(soundEnabled.ToString());
             Streamout.Write("\n");
             Streamout.Write(musicEnabled.ToString());
             Streamout.Write("\n");
              for(int i = 0; i < 5; i++) 
              {
                 Streamout.Write(highscores[i].ToString());
                 Streamout.Write("\n");
            }

              Streamout.Close();
          } 
         catch (IOException ) 
         {
         }
    }

    public static void addScore(int score)
    {
        for(int i=0; i < 10; i++) 
        {
        
            if(highscores[i]< score)
            {
                for(int j= 9; j > i; j--)
                    highscores[j] = highscores[j-1];
                highscores[i] = score;
                break;
            }
       	
        }
    }

    public static void save( int score)
    {
    addScore(score);
    save();
    }



}
}