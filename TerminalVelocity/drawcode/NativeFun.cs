using System;


//Class responsible for calling all native functions from java. 
//Each java class calls this class.

namespace TerVel
{

public class NativeFun
{
	// static Context contex;
    public static void createEngine()
    {Game.gd.Gamestate=4;
     Game.vm.SetState(0);
    }
    public static void postaudiostate(bool a, bool b)
    {Game.postaudiodata(a,b);
    }
    public static void draw(float dt)
    {Game.Draw(dt);
    }
    public static void update()
    {Game.Update();
    }
    public static int updateacc(float accelX, float accelY, float accelZ)
    {Game.Update(accelX,accelY,accelZ);
    return 1; 
    }
    public static void updatetou(float x, float y, int type, int pointer)
    { Game.Update( x, y, type, pointer);
    }
    public static bool updatekey(int keyCode, int type) 
    { return Game.Update(keyCode, type);
    }
    public static int postdata(Object[] a, int i, int j)
    { return -1; 
    }
    public static int poststate(int i) 
    { Game.State(i); return -1; 
    }
    public static int setgetstate(int index) 
    {
        if(index==-1)
	    return Game.vm.GetState();
	    else
	    Game.vm.SetState(index);
	    return -1;

    }
	
	public static void onSensorChanged(float accelX, float accelY, float accelZ) 
	{
		
		//switch(TerVel.orientation)
		//{case 0:updateacc(-1*accelX,-1*accelY,accelZ);break;
	    //case 1:	updateacc(1*accelY,-1*accelX,accelZ);break;
		//case 2:	updateacc(1*accelX,1*accelY,accelZ);break;
		//case 3:	updateacc(-1*accelY,1*accelX,accelZ);break;
		//}
		

	}
	 
 
 

	public static void preResume()
	{
		poststate(1);
	}

	public static void postResume() 
	{
		 
		 
		
	}

	public static void prePause()
	{	 poststate(3);
		
	}

	public static void postPause()
	{ 
		 
		 
	}
	public static void initEngine()
	{
		
		postaudiostate(Settings.musicEnabled,Settings.soundEnabled);
		createEngine();
	}




}

}