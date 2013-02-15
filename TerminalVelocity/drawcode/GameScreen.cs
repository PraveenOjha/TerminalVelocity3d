using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using TerminalVelocity;

namespace TerVel
{

public class GameScreen 
{
	Camera2D guiCam;
    public  spritebatchextension batcher;
    public  BatcherBridge batcherBridge;
    public  tools_seting_bridge ts_bridge;
    Rectangle nextBounds;
    Vector2 touchPoint;
    Texture helpImage;
    TextureRegion helpRegion;
    private bool first;
    private bool resume=false;
    public GameScreen()
   {
    	
         guiCam = new Camera2D(Assets.screenWidth, Assets.screenHeight);
         batcher = new spritebatchextension(Assets.max_texrg );
         batcherBridge=new BatcherBridge(batcher);
         ts_bridge=new tools_seting_bridge();
    
        
       //Sending a handle of this class to c++
        Bridge.postobject(this, 0);
       //Sending a handle of this class to c++
        Bridge.postobject(batcherBridge, 1);
       //Sending a handle of this class to c++
        Bridge.postobject(guiCam, 2);
       //////////////////////////////////////
        Bridge.postobject(ts_bridge,4);
      /////////////////////////////////  
        
    }
    
    //Resume Call
    //@Override
    public void Resume()
    {  if(!resume)
        NativeFun.preResume();	
    }
    
    
    //Pause Call  
    //@Override
    public void pause() 
    {
     Assets.splashscreen.dispose();
     Assets.dispose();
     resume=true;
     Settings.save();
     NativeFun.prePause();
    }
   //Input Calls
   //  @Override
    public void update(float deltaTime) 
    {
        //List<TouchEvent> touchEvents = game.getInput().getTouchEvents();
       // List<KeyEvent> keyEvents= game.getInput().getKeyEvents();
        
       // touchPoint=new Vector2();
        
        
        
  //      int len = keyEvents.size();
  //      for(int i = 0; i < len; i++)
  //      {  KeyEvent event =  keyEvents.get(i);
  //         Assets.keyhandle=NativeFun.updatekey(event.keyCode,event.type);
  //        // if(event.keyCode==android.view.KeyEvent.KEYCODE_VOLUME_DOWN)
  //        // ((Activity) TerVel.contex).finish();
  //        // Assets.keyhandle=true;
  //        // keyEvents.remove(i);
  //      }
  //      len = touchEvents.size();
        
  //      for(int i = 0; i < len; i++)
  //      {  TouchEvent event = touchEvents.get(i);
  //         touchPoint.set(event.x, event.y);
  //         guiCam.touchToWorld(touchPoint);
  //         Assets.vibrate(Assets.vibra);
  //         NativeFun.updatetou(touchPoint.x,touchPoint.y,event.type,event.pointer);
  //        // touchEvents.remove(i);
  //      }
    
        
     //NativeFun.onSensorChanged(game.getInput().getAccelX(),game.getInput().getAccelY(),game.getInput().getAccelZ());
        //         NativeFun.updatetou(touchPoint.x,touchPoint.y,event.type,event.pointer);
    
    }
    
    
    
  //Draw calls
  //  @Override
  public void present(float deltaTime) 
    {  // GL10 gl = glGraphics.getGL();
        //clrscr(gl);
        //Screen set and now draw calls from native code
        //drawload(deltaTime);
        if(Assets.loaderp!=100)
        {
            //if(GLGame.adView.isShown())
            //    ((Activity) TerVel.contex).runOnUiThread(new Runnable(){public void run() {GLGame.adView.setVisibility(View.GONE);}});
           //drawload(deltaTime);
            if(resume!=true)first=true;
    	
        }
    	
        else
        {
        //if(NativeFun.setgetstate(-1)==4||NativeFun.setgetstate(-1)==8)//||NativeFun.setgetstate(-1)==4)
    	
        //    ((Activity) TerVel.contex).runOnUiThread(new Runnable(){public void run() {GLGame.adView.setVisibility(View.VISIBLE);}}); 
        //else
        //    ((Activity) TerVel.contex).runOnUiThread(new Runnable(){public void run() {GLGame.adView.setVisibility(View.GONE);}});	
    		
        if(first)
        {NativeFun.initEngine();
        first=false;
        }
        NativeFun.draw(deltaTime);
        

        }
    	
    }
 
    static int angle=0;
    static float time=0;
    private void drawload(float deltaTime) 
    {                    
    
    batcher.drawSprite(Assets.screenWidth/2, Assets.screenHeight/2, Assets.screenWidth, Assets.screenHeight, 0, Assets.loadscreen);

    //gl.glEnable(GL10.GL_BLEND);
    //gl.glBlendFunc(GL10.GL_SRC_ALPHA, GL10.GL_ONE_MINUS_SRC_ALPHA);
    //batcher.drawSprite(Mouse.GetState().X,480-Mouse.GetState().Y, 10,50,55/2,Assets.textureregions[80]);
   // batcher.drawSprite(50, 50,0+360*Assets.loaderp/100,60,0,Assets.circ);
    time=500*deltaTime;
    
    if(time>2)
    {angle+=5;
     angle%=360;
    time=0;
    }
    batcher.endBatch();
      
    }

    
    
}
}