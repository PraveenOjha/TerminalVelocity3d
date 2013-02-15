using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerVel
{



public class GameData
{
	public  bool musicstate,soundstate,first;
	public  float cur_ax;
	public  float cur_ay;
	public  float cur_az;
	public  float pointx;
	public  float pointy;
	public  float pointx1;
	public  float pointy1;
	public  float heroangle;
	public  int heromaxx,herominx,senstivity;
	public  int life ;
	public  int minreg;
	public  int maxreg;
	public  int herox;
	public  int heroy;
	public  int lift1y;
	public  int lift2y;
    public  float score;
    public  int coins;

    public  int maxcoins;
    public  int maxenemey;
	public  float pointerstate;
	public  float pointerstate1;
	public  int key;
	public  int keystate;
    public  int current_background;
    public  float  bgx;
    public  float bgy;
    public  int liftpos;
    public  int Gamestate;

    public  bool hit;

    public  int []coinarrayy;
    public  int []coinarrayx;
    public  int []enemyx;
    public  int []enemyy;
    public  int []enemytype;
    public  int []enemylength;
   public   int []enemyangle;


  ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
  ////////////////copied thi sdata using watch after asset loader so this need one time to done per game/////////////////////////
 ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	   public   enum Ereg
       
       {bg,bg1, bg2, bg3, bg4, bg5, crane, brick, brick1, brick2, brick3, bg6, bg7, platformbar, barbedwire, lift, boly_tesla10, boly_tesla9,
	      boly_tesla1,boly_tesla8, boly_tesla3, boly_tesla5, boly_tesla2, boly_tesla7, boly_tesla6, boly_tesla4, rocket, hero, heroshadow, wingp2,
	      wingp1,flame1, flame2, flame3, flame4, flame5, flame6, wingp3, ghost_0, ghost_1, ghost_2, ghost_3, ghost_4, ghost_5, ghost_6, ghost_7, ghost_8,
	      ghost_9, ghost_10, ghost_11, ghost_12, ghost_13, ghost_14, ghost_15, ghost_16, ghost_17, ghost_18, ghost_19, ghost_20, ghost_21, ghost_22, ghost_23,
	      ghost_24, ghost_25, oil2, emf, light, regis, ph0, ph1, ph2, regmold, pickupbattery, energycell, oil, pmcharge, price, charg2, charg4, charg1,
	      batterybar,charg3, door1, door2, door3, door, door4, platform, platform1, platform2, platform3, bridge, bridge1, bridge3, doorblock, bolt_strike2,
	      bolt_strike6, bolt_strike5, bolt_strike7, bolt_strike3, bolt_strike4, bolt_strike9, bolt_strike10, font1, font2,
	      stf, sound, soundoff, share1, share3, back, music, musicoff
	      ,start,about,resume,help,gameover,vib,novib  };
      public   enum Eso
	   {Scoin};
    public     enum Etex
		{Tbg, Tbg2,Tcur,Thero,Titems,Tover,Tstrike,Tfont,Tsky,Tgui,Tgameover};

 /////////////////////////////////////////////////////////////////////////////////////////////////////////
 /////////////////////////////////////////Always update this data by appling a  watch/////////////////////
 ///////////////////////////////////  Assets.texturenames,Assets.textures and on Assets.soundname/////////
 /////////////////////////////////////////////////////////////////////////////////////////////////////////

	     public  int screenwidth,screenheight;
	       public  int curtex;
		   public  int texturnum;
		   public  int texturreigonnum;
		   public  int soundnum;
		   public  int [] associatedtextures;
		   //string * sounds;
		   public  int [] regionwidth;
		   public  int [] regionheight;
		   //enum { };
	//viewManager vm;
public GameData(bool musci, bool soundst)
{
 this.cur_ax=0;
 this.cur_ay=0;
 this.cur_az=0;
 this.minreg=-1000;
 this.maxreg=1000;
this.hit=false;
this.first=true;
this.maxcoins=50;
this.maxenemey=25;
this.musicstate=musci;
this.soundstate=soundst;
this.life=3;
}


    
}


public class RegionData
{


public float regionx;
public float regiony;
public int regionspeed;
public  int coinsp1;
public 	int coinsp2;
public 	int coinsp3;
public 	int lift1y;
public  int lift2y;
public  int lift1speed;
public  int lift2speed;
public  int regionversion;
public  int enemyx;
public  int enemyy;
public  int enemylength;
public  int enemytype;
public  float enemyangle;
public  int enemy1x;
public  int enemy1y;
public  int enemy1length;
public  int enemy1type;
public  float enemy1angle;

public  int enemy2x;
public  int enemy2y;
public  int enemy2length;
public  int enemy2type;
public  float enemy2angle;

public  int enemy3x;
public  int enemy3y;
public  int enemy3length;
public  int enemy3type;
public  float enemy3angle;





}





 
    
    
    
    
public class ViewManager
{
public int screenstat;

public ViewManager()
{
this.screenstat=0;

}

~ViewManager()
{

}

public void SetState(int screenstate)
{	 screenstat=screenstate;
}
public int GetState()
{
    return screenstat;
}


}

}
