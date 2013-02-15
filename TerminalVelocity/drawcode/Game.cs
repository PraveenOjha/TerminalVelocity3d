using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Sensors;

namespace TerVel
{


public class Game
    

    {
    //////////////////////////////////Game Class all game related update ,Display done here /////////////////////////
    static bool musci,soundst;
    public static RegionData r1,r2,r3;
    public static ViewManager vm;
    public static GameData gd;
    static long k=0;
    static int tspeed=-300, lk=1,relex_time=0;
    static int speed=-300;
    static float boost=1;
    static float r=.4f,g=.6f,b=.8f;
    static float []sr={.2f,.4f,.4f,.2f,.4f,.5f,.6f,.5f,.3f,.5f,.4f};
    static float []sg={.2f,.3f,.5f,.7f,.2f,.4f,.5f,.6f,.7f,.4f,.5f};
    static float []sb={.5f,.6f,.5f,.3f,.5f,.4f,.2f,.4f,.4f,.2f,.4f};
    static float curtime=0;    
    static int batch=0;

    public  static void Draw(float dt)
                {
                /// if(gd.first)
                /// {relex_time=0;
                ///}

                /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                ////////////////////////////////////////////Base Background Drawn///////////////////////////////////////////////////////////////////////
                /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                //engine.beginBatch((int)GameData.Etex.Tsky);
                engine.spriteDraw(160,240,400,550,0,0,0f,(int)GameData.Ereg.stf);
                //engine.endBatch();
                /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                ////////////////////////////////////////////Enabled Transparency///////////////////////////////////////////////////////////////////////
                /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


                boost=1;
                if(gd.coins>50)//&&gd.pointerstate==1)
                {
                boost=1.5f;gd.coins-=1;
                }


                speed=(int)(tspeed*boost);
                speed=speed-(int)(gd.score/50);
                if(gd.Gamestate==0)
                {gd.score+=.1f*boost-speed/300;
                
                 if (gd.score%3600 < 1&&gd.life<5)
                { gd.life++; 
                }
                relex_time+=(int)(-1*dt*speed);
                }
                lk =3*speed/300;

                /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                ////////////////////////////////////////////sending  the bottom regions to top ///////////////////////////////////////////////////////////////////////
                /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                int a;

                if(r1.regiony<gd.minreg)
                { r1.regiony=gd.maxreg;a=2;
                r1.regionversion=rand()%10;
                genregion(1);
                int sa=(((int)(gd.score))/800)%10;
                r=sr[sa];
                g= sg[sa];
                b=sb[sa];


                }
                if(r2.regiony<gd.minreg)
                {  r2.regiony=gd.maxreg;a=3;
                r1.regionversion=rand()%10;
                }
                if(r3.regiony<gd.minreg)
                { r3.regiony=gd.maxreg;a=1;
                r1.regionversion=rand()%10;
                }
                /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                ////////////////////////////////////////////Drawing the regions///////////////////////////////////////////////////////////////////////
                /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                engine.setcolor(.4f,.5f,.8f,1f);

                drawbg(dt, (int)r3.regionx, (int)r3.regiony, r3.regionversion);
                drawbg(dt, (int)r1.regionx, (int)r1.regiony, r1.regionversion);

                engine.setcolor(r,b,g,1);
                drawbg(dt, (int)r2.regionx, (int)r2.regiony, r2.regionversion);
                engine.setcolor(1,1,1,1);



                //////////////////////////////////////////////////////////////////////////////
                //////////////////////////////////////////////////////////////////////////////
                ////////////////////////////Draw Enemies//////////////////////////////////////
                ////////////type 0 left wall emf, type 1 right wall emf and type 2////////////
                //////////////////////electric shock n type 3 rotating electric shock/////////
                //////////////////////////////////////////////////////////////////////////////
                //engine.beginBatch(gd.Titems);

                int l=0;
                if(gd.Gamestate==0)
                while(l<gd.maxenemey)
                {  switch (gd.enemytype[l])
                {
                /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                ////////////////////////////////////////////Drawing emf and appling their effect on hero that is a suction effect ///////////////////////////////////////////////////////////////////////
                /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



                case 0:  drawitems(dt, r1.regionx, r1.regiony + gd.enemyy[l], 0, 0, 0);
                if(r1.regiony+gd.enemyy[l]>0&&r1.regiony+gd.enemyy[l]<70&&gd.herox<120)
                gd.herox+=lk;break;
                case 1: drawitems(dt, r1.regionx, r1.regiony + gd.enemyy[l], 1, 0, 0);
                if(r1.regiony+gd.enemyy[l]>0&&r1.regiony+gd.enemyy[l]<70&&gd.herox>180)
                gd.herox-=lk;
                break;
                /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                ////////////////////////////////////////////Drawing current fields and appling their effect on hero///////////////////////////////////////////////////////////////////////
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


                case 2:
                case 3: int aq = (int)(gd.enemylength[l] * Math.Cos(gd.enemyangle[l] * 0.017453) / 2); int bq = (int)(gd.enemylength[l] * Math.Sin(gd.enemyangle[l] * 0.017453) / 2);
                drawitems(dt,gd.enemyx[l]-aq,r1.regiony+gd.enemyy[l]-bq,2,0,0);
                drawitems(dt,gd.enemyx[l]+aq,r1.regiony+gd.enemyy[l]+bq,2,0,0);

                if(gd.Gamestate==0&&(     ((pow((gd.herox -(gd.enemyx[l]-aq)),2)+pow((gd.heroy -(r1.regiony+gd.enemyy[l]-bq)),2))<500)||
                ((pow((gd.herox -(gd.enemyx[l]+aq)),2)+pow((gd.heroy -(r1.regiony+gd.enemyy[l]+bq)),2))<500)||
                ((pow((gd.herox -(gd.enemyx[l])),2)+pow((gd.heroy -(r1.regiony+gd.enemyy[l])),2))<800)

                ))
                {

                if(gd.life==0)
                {gd.hit=true;
                }

                /////////////////////////////relexing player to survi some time//////////////////////////
                if(relex_time>400)
                {gd.life--;relex_time=0;
                engine.playsound( 1);
                }
                }

                break;
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                ////////////////////////////////////////////end of single enemy drawing based on its type///////////////////////////////////////////////////////////////////////
                /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


                }
                l++;
                }

                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                //////////////////////Draw Coins and check collision we are using square distance method to detect collison ///////////////////
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                if(gd.hit&&gd.Gamestate==0)
                {gd.Gamestate=4;
                vm.SetState(4);
                engine.write((int)gd.score);
                }

                int  ki=0;
                while(ki<gd.maxcoins)
                {
                int ax=gd.coinarrayx[ki],ay=gd.coinarrayy[ki];

                if((pow((float)(gd.herox -(ax)),2)+pow((float)(gd.heroy -(ay+r1.regiony)),2))<400)
                {gd.coins+=20;engine.playsound((int)GameData.Eso.Scoin);
                gd.coinarrayx[ki]-=500;
                }
                drawitems(dt,ax,ay+r1.regiony,5,0,0);
                ki++;
                }

                ////////////////////////////////////////////////////////////////////////////////
                ////////////////////////////////////////////////////////////////////////////////
                ////////////////////////////////Draw PatternCoins not in this //////////////////
                /////////////////////////////ersion but draw shapes from coins//////////////////
                ////////////////////////////////////////////////////////////////////////////////
                /*
                drawPattern( dt,r2.regionx ,r2.regiony-400,r2.coinsp1,4,0);
                drawPattern( dt,r2.regionx ,r2.regiony,r2.coinsp2,1,0);
                drawPattern( dt,r2.regionx ,r2.regiony+400,r2.coinsp3,6,0);
                drawPattern( dt,r1.regionx ,r1.regiony-400,r1.coinsp1,4,0);
                drawPattern( dt,r1.regionx ,r1.regiony,r1.coinsp2,1,0);
                drawPattern( dt,r1.regionx ,r1.regiony+400,r1.coinsp3,6,0);
                drawPattern( dt,r1.regionx ,r1.regiony-400,r1.coinsp1,4,0);
                drawPattern( dt,r3.regionx ,r3.regiony,r1.coinsp2,1,0);
                drawPattern( dt,r3.regionx ,r3.regiony+400,r1.coinsp3,6,0);*/
                ///////////////////////////////////////////////////////////////////////////////
                //////////////////////////////Draw hero //////////////////////////////////////
                ///////////////////////////////////////////////////////////////////////////////
                engine.setcolor(1,1,1,1);
                if(relex_time<450)
                {engine.setcolor(1f,1f,1f,(relex_time%60==0?1f:.5f));
                }
                drawhero(dt,gd.herox ,gd.heroy,0,3,gd.heroangle);
                //engine.endBatch();






                if(gd.coins>50)//&&gd.pointerstate==1)
                {
                ///////////////////////////////////////////////////////////////////////////////
                ///////////////////////////////////////////////////////////////////////////////
                /////////////////////////////Draw blue  flame///////////////////////////////////
                ///////////////////////////////////////////////////////////////////////////////

                //engine.beginBatch(gd.Thero);
                engine.setcolor(.5f,.8f,1f,.9f);
                drawhero(dt,gd.herox,gd.heroy-10,3,0,gd.heroangle+180);
                // engine.endBatch();
                }
                else {
                ///////////////////////////////////////////////////////////////////////////////
                ///////////////////////////////////////////////////////////////////////////////
                /////////////////////////////Draw red flame///////////////////////////////////
                ///////////////////////////////////////////////////////////////////////////////

                // engine.beginBatch(gd.Thero);
                engine.setcolor(1f,.8f,.8f,.9f);
                drawhero(dt,gd.herox,gd.heroy-10,3,0,gd.heroangle+180);
                // engine.endBatch();

                }

                engine.setcolor(1,1,1,1);










                /////////////////////////////////////////////////////////////////////////////////////////////////////////
                /////////////////////////////Draw current over current fields//////////////////////////////////
                /////////////////////////////////////////////////////////////////////////////////////////////////////////
                if(gd.Gamestate==0)
                {
                // engine.beginBatch(gd.Tcur);
                int i=0;

                while(i<gd.maxenemey)
                { if(gd.enemytype[i]==3||gd.enemytype[i]==2)
                drawcurrent( dt, gd.enemyx[i],gd.enemyy[i]+r1.regiony,.3*gd.enemylength[i]/140,.4*gd.enemylength[i]/140,0,0, gd.enemyangle[i]);
                else
                drawcurrent( dt, gd.enemyx[i]-800,gd.enemyy[i]-800,.2,.3,0,0, gd.enemyangle[i]+90);
                i++;
                }
                // engine.endBatch();
                /////////////////////////////////////////////////////////////////////////////////////////////////////////
                /////////////////////////////////////////////////////////////////////////////////////////////////////////
                /////////////////////////////////////////////////////////////////////////////////////////////////////////
                /////////////////////////////////////////////////////////////////////////////////////////////////////////
                ///////////////////////////////////////////////Draw score and coins//////////////////////////////////////
                /////////////////////////////////////////////////////////////////////////////////////////////////////////




                ////engine.beginBatch( gd.Tfont);
                //engine.textDraw( 10f,460f,2f,1f,1,"Score",1f,1f,.4f,1f," ");
                //drawnumber( (int)gd.score,80,430,15);
                //engine.textDraw( 10f,400f,2f,1f,1,"Life",1f,1f,.4f,1f," ");
                //drawnumber( (int)gd.life,50,380,15);
                //engine.textDraw( 200f,460f,2f,1f,1,"Battery",1f,1f,.3f,1f," ");
                //drawnumber( gd.coins,(gd.score>999?260:280),430,15);
                //engine.setcolor(.8f, .8f, .8f, .8f);
                //engine.textDraw(290f, 400f, 2f, 1f, 0, "II", 1f, 1f, .3f, 1f, " ");
                //engine.setcolor(1f, 1f, 1f, 1f);
                if (gd.pointerstate == 1 && gd.pointx >330 && gd.pointy < 405 && gd.pointx < 385 && gd.pointy > 377)
                {
                    gd.Gamestate = 3;
                }
                    //engine.endBatch(env);
                ///////GamePlaying////////////////
                //////////////////////////
                r1.regiony+=dt*speed;
                r2.regiony+=dt*speed;
                r3.regiony+=dt*speed;
                ///////////////////////////////////////		  break;

                }
                guiDraw( dt);
                //glDisable(GL_BLEND);
                }

    private static void drawcurrent(float p1, int p2, float p3, double p4, double p5, int p6, int p7, int p8)
    {
        drawcurrent(p1, p2, (int)p3, (float)p4, (float)p5, p6, p7, (float)p8);
    }

    private static void drawitems(float dt, float p1, float p2, int p3, int p4, int p5)
    {
        drawitems((float)dt, (int)p1, (int)p2, (int)p3, (int)p4, (float)p5); 
    }

    private static void drawitems(float dt, int p1, float p2, int p3, int p4, int p5)
    {
        drawitems((float) dt,(int) p1,(int) p2,(int) p3,(int) p4,(float) p5);
    }
    public  static float pow(float p1, int p2)
{
    return  p1*p1;
}
 ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
 ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
 ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
 //////////////////////////////////////////////////Draw back ground ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
 public  static  void drawbg(float dt,int x,int y,int index)
    {
	 if(batch==0){Init();batch=1;}
	 if(index>11)index=11;
     int[] sprite = { (int)GameData.Ereg.bg3, (int)GameData.Ereg.bg4, (int)GameData.Ereg.bg1, (int)GameData.Ereg.bg2, (int)GameData.Ereg.bg4, (int)GameData.Ereg.bg5, (int)GameData.Ereg.bg1, (int)GameData.Ereg.bg5, (int)GameData.Ereg.bg4, (int)GameData.Ereg.bg3, (int)GameData.Ereg.bg1, (int)GameData.Ereg.bg2, (int)GameData.Ereg.bg5, (int)GameData.Ereg.bg5 };
     int[] br ={(int)GameData.Ereg.brick1,(int)GameData.Ereg.brick2,(int)GameData.Ereg.brick3,(int)GameData.Ereg.brick,(int)GameData.Ereg.brick1,(int)GameData.Ereg.brick,(int)GameData.Ereg.brick,(int)GameData.Ereg.brick1,(int)GameData.Ereg.brick1,(int)GameData.Ereg.brick2,(int)GameData.Ereg.brick3,
    		  (int)GameData.Ereg.brick3,(int)GameData.Ereg.brick3,(int)GameData.Ereg.brick2,(int)GameData.Ereg.brick1,(int)GameData.Ereg.brick1,(int)GameData.Ereg.brick3,(int)GameData.Ereg.brick2,(int)GameData.Ereg.brick1,(int)GameData.Ereg.brick2};
     int[] door = { (int)GameData.Ereg.door, (int)GameData.Ereg.door1, (int)GameData.Ereg.door2, (int)GameData.Ereg.door3 };
     int[] doorbl = { (int)GameData.Ereg.bridge, (int)GameData.Ereg.bridge1, (int)GameData.Ereg.bridge3 };

     int[] sprite1 = { (int)GameData.Ereg.bg6, (int)GameData.Ereg.bg7, (int)GameData.Ereg.lift, (int)GameData.Ereg.barbedwire, (int)GameData.Ereg.platformbar };
     int gd1, gd2;//, spritewidth, spriteheight ;
	 gd1=y+480;
	 gd2=y-480;

	 /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
     ////////////////////////                 Draw the Base background                      /////////////////////////////////////
	 /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


     ////////////enable transparency//////////////
      /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
     ///////////////////////                 Draw the Over Art on background                 /////////////////////////////////////
     ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


  //  engine.beginBatch(gd.Tover);
  //  engine.spriteDraw(x,y,gd.regionwidth[door[index/4]]*.7,gd.regionheight[door[index/4]]*.6,0,0,0,door[index/4]);
   // engine.endBatch();

     ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
     /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
      ///////////////////////                 Draw the Lift and its holder   barbed wire etc               //////////////////////////////////////
      ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

	  //engine.beginBatch(gd.Tbg2);
     // engine.spriteDraw(x+15,gd.lift1y,gd.regionwidth[sprite1[2]]*.5,gd.regionheight[sprite1[2]]*.7,0,0,0,sprite1[2]);
     // engine.spriteDraw(x-15,gd.lift2y,gd.regionwidth[sprite1[2]]*.5,gd.regionheight[sprite1[2]]*.7,0,0,0,sprite1[2]);
     engine.spriteDraw(x, y + 155, gd.regionwidth[(int)GameData.Ereg.barbedwire] * 1.2, gd.regionheight[(int)GameData.Ereg.barbedwire] * .7, 0, 0, 0, (int)GameData.Ereg.barbedwire);
     engine.spriteDraw(x, y - 155, gd.regionwidth[(int)GameData.Ereg.barbedwire] * 1.2, gd.regionheight[(int)GameData.Ereg.barbedwire] * .7, 0, 0, 0,  (int)GameData.Ereg.barbedwire);
      engine.spriteDraw(x,y-20,gd.regionwidth[sprite1[4]],gd.regionheight[sprite1[4]]*2.5,0,0,0,sprite1[4]);
      engine.spriteDraw(x,y+gd.regionheight[sprite1[4]],gd.regionwidth[sprite1[4]],gd.regionheight[sprite1[4]]*2.5,0,0,0,sprite1[4]);
      //engine.spriteDraw(x,y-gd.regionheight[sprite1[4]]*2,gd.regionwidth[sprite1[4]],gd.regionheight[sprite1[4]]*2,0,0,0,sprite1[4]);
     // engine.spriteDraw(x,y+gd.regionheight[sprite1[4]]*2,gd.regionwidth[sprite1[4]],gd.regionheight[sprite1[4]]*2,0,0,0,sprite1[4]);
      engine.endBatch();

      /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
      ///////////////////////                 Draw the Over Art on lift foreground                 /////////////////////////////////////
      ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


     //engine.beginBatch(gd.Tover);
     //engine.spriteDraw(x,y-10-gd.regionheight[sprite1[4]]/2,gd.regionwidth[door[index/4]]*.6,gd.regionheight[sprite1[index/4]]*.3,0,0,0,doorbl[index%4]);
     //engine.spriteDraw(x,y+10+gd.regionheight[sprite1[4]]/2,gd.regionwidth[door[index/4]]*.6,gd.regionheight[sprite1[index/4]]*.3,0,0,0,doorbl[index%3]);
    // engine.endBatch();


     /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
     ///////////////////////                 Draw side  wall using bricks                  /////////////////////////////////////
     ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
     //engine.beginBatch(gd.Tbg);
      int k = gd.regionheight[(int)GameData.Ereg.platformbar] / gd.regionheight[(int)GameData.Ereg.brick] + 1;
     int i=0;
     int w = gd.regionheight[(int)GameData.Ereg.brick];
     while(i<4*k)
     {   drawwall(dt,x-15,y-(w*k/2)+i*gd.regionheight[br[index+i%10]]*.59f,0,br[index+i%10],0);
         drawwall(dt,x+15,y-(w*k/2)+i*gd.regionheight[br[index+i%10]]*.59f,1,br[index+i%10],0);
       i++;
     }  i=0;
        while(i<4*k)
          {  drawwall(dt,x-15,y-90-(w*k/2)+i*gd.regionheight[br[index+i%10]]*.59f,2,br[index+(40-i)%10],0);
         	 drawwall(dt,x+15,y-90-(w*k/2)+i*gd.regionheight[br[index+i%10]]*.59f,3,br[index+(40-i)%10],0);
            i++;
          }i=0;


          engine.endBatch();


     //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
     ////////////////////////////////////////////////////////Nothing more to draw for back ground//////////////////////////////////////////
     //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
   /*  {
     int nt=0;
     if(gd.pointy<50)
    	 nt=0;
     else if(gd.pointy<100)
    	 nt=1;
     else if(gd.pointy<150)
         nt=2;
     else if(gd.pointy<200)
         nt=3;
     else if(gd.pointy<250)
         nt=4;
     }*/

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    }

 private static void drawwall(float dt, int p1, float p2, int p3, int p4, int p5)
 {
   drawwall(dt, p1, (int)p2, p3, p4, (float)p5); 
 }

 //////////////////////////////////////////////////Draw wall brick by brick////////////////////////////////////////////////////////////////////////////////////////////////////////////
 public static void drawwall(float dt, int x, int y, int _object, int sprite, float angle)
 {
     int spritewidth = gd.regionwidth[sprite];
     int spriteheight = gd.regionheight[sprite];
     ////////////index as per there draw order first to last ///////////////////
     switch (_object)
     {
         case 0: engine.spriteDraw(x - 160 + spritewidth / 2 + .46 * spritewidth, y - 210 + spriteheight * .59, spritewidth, spriteheight, 0, 0, 0, sprite); break;
         case 1: engine.spriteDraw(x + 115 - spritewidth / 2 - .46 * spritewidth, y - 210 + spriteheight * .59, -1 * spritewidth, spriteheight, 0, 0, 0, sprite); break;
         case 2: engine.spriteDraw(x - 160 + spritewidth / 2, y - 210 + spriteheight * .72, spritewidth, spriteheight, 0, 0, 0, sprite); break;
         default: engine.spriteDraw(x + 115 - spritewidth / 2, y - 210 + spriteheight * .72, -1 * spritewidth, spriteheight, 0, 0, 0, sprite); break;

     }
 }
  
//////////////////////////////////////////////////draw Enemy,pickup,ghost they don't have privilege of closing and opening batcher///////////////////////////////////////////


 public static void drawitems(float dt,int x,int y,int grp,int index,float angle)
     {
         int[] cursig = { (int)GameData.Ereg.charg1, (int)GameData.Ereg.charg2, (int)GameData.Ereg.charg3, (int)GameData.Ereg.charg4 };
         int[] ghost ={ (int)GameData.Ereg.ghost_1, (int)GameData.Ereg.ghost_2, (int)GameData.Ereg.ghost_3, (int)GameData.Ereg.ghost_4, (int)GameData.Ereg.ghost_5, (int)GameData.Ereg.ghost_6, (int)GameData.Ereg.ghost_7, (int)GameData.Ereg.ghost_8,(int)GameData.Ereg.ghost_9, (int)GameData.Ereg.ghost_10,
	    		      (int)GameData.Ereg.ghost_11,(int)GameData.Ereg.ghost_12,(int)GameData.Ereg. ghost_13, (int)GameData.Ereg.ghost_14,(int)GameData.Ereg.ghost_15, (int)GameData.Ereg.ghost_16, (int)GameData.Ereg.ghost_17, (int)GameData.Ereg.ghost_18, (int)GameData.Ereg.ghost_19, (int)GameData.Ereg.ghost_20,
	    		      (int)GameData.Ereg.ghost_21, (int)GameData.Ereg.ghost_22, (int)GameData.Ereg.ghost_23, (int)GameData.Ereg.ghost_24, (int)GameData.Ereg.ghost_25};

		 float spritewidth=gd.regionwidth[ (int)GameData.Ereg.emf]*.4f;
         float spriteheight = gd.regionheight[(int)GameData.Ereg.emf] * .4f;


		 int cin=((int)(curtime*5))%4;
		 curtime+=dt/2;
		 if(curtime>50000000)
		 {curtime=0;
		 }
		 switch (grp)
         {
             case 0:
                engine.spriteDraw(x - 110, y, spritewidth, spriteheight, 0f, 0f, 0f, (int)GameData.Ereg.emf);
		 	    engine.spriteDraw(x-108,y-10,gd.regionwidth[cursig[(cin+1)%4]]*.5,gd.regionheight[cursig[(cin+1)%4]]*.3,0,0,0,cursig[(cin+1)%4]);
		 	    engine.spriteDraw(x-108,y-5,gd.regionwidth[cursig[(cin+1)%4]]*.5,gd.regionheight[cursig[(cin+1)%4]]*.3,0,0,0,cursig[(cin+2)%4]);
		 	    engine.spriteDraw(x-108,y+5,gd.regionwidth[cursig[(cin+1)%4]]*.5,gd.regionheight[cursig[(cin+1)%4]]*.3,0,0,0,cursig[(cin+3)%4]);
		 	    engine.spriteDraw(x-108,y+10,gd.regionwidth[cursig[(cin+1)%4]]*.5,gd.regionheight[cursig[(cin+1)%4]]*.3,0,0,0,cursig[(cin+4)%4]);

		 	    break;
             case 1: engine.spriteDraw(x + 93, y, -1 * spritewidth, spriteheight, 0, 0, 0f, (int)GameData.Ereg.emf);
                   		    	engine.spriteDraw(x+112,y-10,gd.regionwidth[cursig[(cin+1)%4]]*.5,gd.regionheight[cursig[(cin+1)%4]]*.3,0,0,0,cursig[(cin+1)%4]);
		    			 	    engine.spriteDraw(x+112,y-5,gd.regionwidth[cursig[(cin+1)%4]]*.5,gd.regionheight[cursig[(cin+1)%4]]*.3,0,0,0,cursig[(cin+2)%4]);
		    			 	    engine.spriteDraw(x+112,y+5,gd.regionwidth[cursig[(cin+1)%4]]*.5,gd.regionheight[cursig[(cin+1)%4]]*.3,0,0,0,cursig[(cin+3)%4]);
		    			 	    engine.spriteDraw(x+112,y+10,gd.regionwidth[cursig[(cin+1)%4]]*.5,gd.regionheight[cursig[(cin+1)%4]]*.3,0,0,0,cursig[(cin+4)%4]);

            	      break;
             case 2: engine.spriteDraw(x, y, gd.regionwidth[(int)GameData.Ereg.pmcharge], gd.regionheight[(int)GameData.Ereg.pmcharge], 0, 0, 0f, (int)GameData.Ereg.pmcharge); break;
		    	case 3:engine.spriteDraw(x,y,gd.regionwidth[ghost[index]]*.5,gd.regionheight[ghost[index]]*.5,0,0,0,ghost[index]);break;
		    	case 4:engine.spriteDraw(x,y,-1*gd.regionwidth[ghost[index]]*.5,gd.regionheight[ghost[index]]*.5,0,0,0,ghost[index]);break;
                case 5: engine.spriteDraw(x, y, gd.regionwidth[(int)GameData.Ereg.pickupbattery] * .25, gd.regionheight[(int)GameData.Ereg.pickupbattery] * .25, 0, 0, 0, (int)GameData.Ereg.pickupbattery);
		    	  if((cin+1)%4!=0)
                      engine.spriteDraw(x + gd.regionwidth[(int)GameData.Ereg.pickupbattery] * .125f - gd.regionwidth[(int)GameData.Ereg.batterybar] * .125f, y, gd.regionwidth[(int)GameData.Ereg.batterybar] * .25f, gd.regionheight[(int)GameData.Ereg.batterybar] * .25f, 0, 0, 0f, (int)GameData.Ereg.batterybar);
		    	  else
                      engine.spriteDraw(x + gd.regionwidth[(int)GameData.Ereg.pickupbattery] * .125f - gd.regionwidth[(int)GameData.Ereg.batterybar] * .125f, y, gd.regionwidth[(int)GameData.Ereg.batterybar] * .25f, gd.regionheight[(int)GameData.Ereg.batterybar] * .25f, 180, 0, 0f, (int)GameData.Ereg.batterybar);
		    	break;


		    }


     }

static int herotime=0;
static void drawhero(float dt,int x,int y,int grp,int index,float angle)
{
    int[] flame = { (int)GameData.Ereg.flame1, (int)GameData.Ereg.flame2, (int)GameData.Ereg.flame3, (int)GameData.Ereg.flame4, (int)GameData.Ereg.flame5, (int)GameData.Ereg.flame6 };
             int cin=((int)(herotime))%6;
    		 herotime+=(int)(dt*100);
     		 if(herotime>5000000)
    		 {herotime=0;
    		 }
    		switch(grp)
            {
                case 0: engine.spriteDraw(x, y, gd.regionwidth[(int)GameData.Ereg.ph0] * .6f, gd.regionheight[(int)GameData.Ereg.ph0] * .6f, angle, 0, 10, (int)GameData.Ereg.ph2);
                  break;
    		 default:
    			 engine.spriteDraw(x,y-14,gd.regionwidth[flame[(cin+1)%4]]*.6f,gd.regionheight[flame[(cin+1)%4]]*.6f,angle,0,0,flame[(cin)%6]);

    			 break;
    		 }


     }
//////////////////////////////////////////////cur draw/////////////////////////////////////////////////////////////
  static float curcurtime=0;
  static void drawcurrent(float dt,int x,int y,float sx,float sy,int _object,int sprite,float angle)
     {
	   int cin=((int)(curcurtime*5))%9;
	  curcurtime+=dt/1.25f;if(curcurtime>50000000){curcurtime=0;}

      int[] a = { (int)GameData.Ereg.boly_tesla1, (int)GameData.Ereg.boly_tesla2, (int)GameData.Ereg.boly_tesla3, (int)GameData.Ereg.boly_tesla4, (int)GameData.Ereg.boly_tesla5, (int)GameData.Ereg.boly_tesla6, (int)GameData.Ereg.boly_tesla7, (int)GameData.Ereg.boly_tesla8, (int)GameData.Ereg.boly_tesla9, (int)GameData.Ereg.boly_tesla10 };
	     float spritewidth=gd.regionwidth[a[_object]];
	     float spriteheight=gd.regionheight[a[_object]];spritewidth*=sx;spriteheight*=sy;
        ////////////index as per there draw order first to last ///////////////////

    	 engine.spriteDraw(x,y,spritewidth,spriteheight,angle,0,0,a[cin]);

     }
//////////////////////////////////////strike current /////////////////////////////////////////////////
  static void drawstrike(float dt,int x,int y,int _object,int sprite,float angle)
     {
	     int []a={(int)GameData.Ereg.bolt_strike2,(int)GameData.Ereg.bolt_strike3,(int)GameData.Ereg.bolt_strike4,(int)GameData.Ereg.bolt_strike5,(int)GameData.Ereg.bolt_strike6,(int)GameData.Ereg.bolt_strike7,(int)GameData.Ereg.bolt_strike9,(int)GameData.Ereg.bolt_strike10};
	     float spritewidth=gd.regionwidth[a[_object]];
	     float spriteheight=gd.regionheight[a[_object]];
	     spritewidth*=.1f;
	     spriteheight*=.2f;

        ////////////index as per there draw order first to last ///////////////////
	     int cin=((int)(curcurtime*5))%6;
	   	  curcurtime+=dt/1.25f;if(curcurtime>50000000){curcurtime=0;}
         engine.spriteDraw(x,y,spritewidth,spriteheight,angle,0,0,a[cin]);
     }
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////abo DRAW///////////////////////////////////////////////////////////////////////////////////////////// 
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////




  static int butonpush = -1;

  static void guiDraw(float dt)
  {


      ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
      ////////////////////////////////////////////Draw Gui  Screen Based on Game State and pause game and hit detection if game///////////////////////////////////////
      /////////////////////////////////////////////////////////state is not playing////////////////////////////////////////////////////////////////////////////////////
      /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


      int[] screena = { 70, 150, 240, 330, 800 };

      
      int ti = 0;
      switch (gd.Gamestate)
      {


          ////////////Game Paused///////////
          case 1:

          case 2:
          /////////////Game Resumed/////////
          case 3:
              if (vm.GetState() == 0)
                  vm.SetState(3);
              goto case 4;
          //else
          //vm.screenstate=0;
          /* no break */

          ////////////////Game new not resumed/////////
          case 4:
              //vm.SetState(0);
              //gd.Gamestate = 0;
              switch (vm.GetState())
              {
                  case 0:
                      engine.setcolor(.2f, .2f, .7f, .4f);
                      //engine.beginBatch(gd.Tbg2);
                      engine.spriteDraw(160, 200, 300, 460, 0, 0, 0f, (int)GameData.Ereg.bg6);
                      if (gd.pointy > 0 && gd.pointy < 400)
                          engine.spriteDraw(160, screena[(int)gd.pointy / 90], 300, 50, 0, 0, 0f, (int)GameData.Ereg.bg7);

                      engine.endBatch();

                      ti = 0;
                      while (ti < 5)
                      {
                          if ((gd.pointerstate == 1) && gd.pointx > 80 && gd.pointx < 280 && gd.pointy > screena[4 - ti] - 25 && gd.pointy < screena[4 - ti] + 25)
                          {
                              butonpush = ti - 1;
                          } ti++;
                      }

                      if (gd.pointerstate == 1 && gd.pointx < 80 && gd.pointy < 220 && gd.pointx > 0 && gd.pointy > 80)
                      {
                          butonpush = 6;
                      }

                      if (gd.pointerstate == 1 && gd.pointx > 240 && gd.pointy < 220 && gd.pointx < 320 && gd.pointy > 80)
                      {
                          butonpush = 7;
                      }

                      switch (butonpush)
                      {
                          case 0: gd.Gamestate = 0; gd.first = true; gd.life = 5; gd.hit = false; gd.score = 0; gd.coins = 0; engine.playsound(2);
                              //gd.life = 123;
                              butonpush = -1; Init();
                              break;
                          case 1: vm.SetState(2); engine.playsound(2); butonpush = -1;
                              break;
                          case 2: vm.SetState(1); engine.playsound(2); butonpush = -1;
                              break;
                          case 3: vm.SetState(8); engine.playsound(2); butonpush = -1;
                              break;
                          case 4: vm.SetState(8); engine.playsound(2); butonpush = -1; quit();//engine.playsound(1);
                              break;

                          /////////////////////////////////disable enable music //////////////////////
                          case 6:

                              if (gd.pointerstate == 1)
                              {
                                  if (gd.musicstate) gd.musicstate = false; else gd.musicstate = true;
                                  engine.music((gd.musicstate ? 1 : 0), "");//break;
                                  engine.playsound(2);
                                  engine.write(-40);
                                  butonpush = -1;
                                  gd.pointerstate = 0;
                              } break;
                          /////////////////////////////////disable enable sound //////////////////////
                          case 7:
                              if (gd.pointerstate == 1)
                              {
                                  if (gd.soundstate) gd.soundstate = false; else gd.soundstate = true;
                                  engine.music((gd.soundstate ? 3 : 2), "");// break;
                                  engine.playsound(2);
                                  engine.write(-40);
                                  butonpush = -1;
                                  gd.pointerstate = 0;
                              }
                              break;

                      }

                      //engine.endBatch();
                      engine.setcolor(1, 1, 1, 1);
                      //   engine.beginBatch(gd.Tgui);
                      engine.spriteDraw(160, 240, 300, 460, 0, 0, 0f, (int)GameData.Ereg.start);
                      //  engine.endBatch();
                      // engine.beginBatch(gd.Tsky);
                      if (gd.musicstate == false)
                          engine.spriteDraw(30, 130, 70, 70, 0, 0, 0f, (int)GameData.Ereg.musicoff);
                      else
                          engine.spriteDraw(30, 130, 70, 70, 0, 0, 0f, (int)GameData.Ereg.music);

                      if (Settings.vibrate == false)
                          engine.spriteDraw(30, 60, 70, 40, 0, 0, 0f, (int)GameData.Ereg.novib);
                      else
                          engine.spriteDraw(30, 60, 70, 40, 0, 0, 0f, (int)GameData.Ereg.vib);


                             if (gd.pointerstate == 1 && gd.pointx < 80 && gd.pointy < 90 && gd.pointx > 0 && gd.pointy >30) 
                              {
                                  if (Settings.vibrate) Settings.vibrate = false; else Settings.vibrate = true;
                                  //engine.music(6, "");//break;
                                  engine.playsound(2);
                                  engine.write(-40);
                                  butonpush = -1;
                                  gd.pointerstate = 0;
                                 
                              }  
                      
                      

                      if (gd.soundstate == false)
                          engine.spriteDraw(290, 130, 70, 70, 0, 0, 0f, (int)GameData.Ereg.soundoff);
                      else
                          engine.spriteDraw(290, 130, 70, 70, 0, 0, 0f, (int)GameData.Ereg.sound);
                      engine.endBatch();

                      engine.spriteDraw(310, 10, 30, 60, 90, 0, 0f, (int)GameData.Ereg.back);
                      engine.endBatch();
                      if (gd.pointerstate == 1 && gd.pointx < 330 && gd.pointy < 50 && gd.pointx > 290 && gd.pointy > 0)
                      {
                          hook.togglehook(); Main.context.Exit();
                      }

                      //if (gd.pointerstate == 0)
                      //   __android_log_print(1, "tag", "this is pointer 2 y=%d and x =%d", gd.pointy, gd.pointx);

                      break;


                  ////////////////////////about////////////////////

                  case 1:

                      //engine.beginBatch(gd.Tgui);
                      engine.spriteDraw(160, 225, 300, 460, 0, 0, 0f, (int)GameData.Ereg.about);
                      engine.endBatch();
                      //engine.beginBatch(gd.Tsky);
                      engine.spriteDraw(30, 50, 70, 70, 0, 0, 0f, (int)GameData.Ereg.back);
                      engine.endBatch();
                      if (gd.pointerstate == 1 && gd.pointx < 90 && gd.pointy < 85 && gd.pointx > 0 && gd.pointy > 0)
                      {
                          vm.SetState(0); engine.playsound(2);
                      }
                      break;

                  ////////////////////////help////////////////////

                  case 2:
                      //engine.beginBatch(gd.Tgui);
                      engine.spriteDraw(160, 225, 300, 460, 0, 0, 0f, (int)GameData.Ereg.help);
                      //engine.endBatch();
                      //engine.beginBatch(gd.Tsky);
                      engine.spriteDraw(30, 50, 70, 70, 0, 0, 0f, (int)GameData.Ereg.back);
                      engine.endBatch();
                      if (gd.pointerstate == 1 && gd.pointx < 90 && gd.pointy < 85 && gd.pointx > 0 && gd.pointy > 0)
                      {
                          vm.SetState(0); engine.playsound(2);
                      }
                      break;

                  ////////////////////////pause////////////////////
                  case 3:
                      //engine.beginBatch(gd.Tgui);
                      engine.spriteDraw(160, 240, 300, 460, 0, 0, 0f, (int)GameData.Ereg.resume);
                      engine.endBatch();
                      //engine.beginBatch(gd.Tsky);
                      engine.spriteDraw(30, 50, 70, 70, 0, 0, 0f, (int)GameData.Ereg.back);
                      engine.endBatch();
                      gd.Gamestate = 4;
                      if (gd.pointerstate == 1 && gd.pointx < 90 && gd.pointy < 100 && gd.pointx > 0 && gd.pointy > 0)
                      {
                          vm.SetState(0);
                          //engine.playsound(2);
                      }
                      //////////////////resume//////////////

                      else if (gd.pointerstate == 1 && gd.pointx > 90 && gd.pointy < 290 && gd.pointx < 270 && gd.pointy > 190)
                      {
                          gd.Gamestate = 0;
                      }
                      break;


                  ///////////////////////Game Over////////////////////


                  case 4:

                      //engine.beginBatch(gd.Tgameover);
                      engine.spriteDraw(160, 360, 210, 100, 0, 0, 0f,(int)GameData.Ereg.gameover);
                      engine.endBatch();

                      //engine.beginBatch(gd.Tfont);
                      engine.textDraw(80, 220, 1.2f, 1.2f, 1, "YourScore", 1, 1, .5f, 1, " ");
                      engine.scoreDraw(80, 223, 200, 200, 0, 0, 0, 1);
                      //engine.beginBatch(gd.Tfont);
                      //engine.textDraw(10,300,1.4,1.5,1," Game Over ",2,2,.7,1," ");
                      engine.endBatch();


                      //engine.beginBatch(gd.Tsky);
                      engine.spriteDraw(30, 50, 70, 70, 0, 0, 0f, (int)GameData.Ereg.back);
                      engine.endBatch();
                      if (gd.pointerstate == 1 && gd.pointx < 90 && gd.pointy < 120 && gd.pointx > 0 && gd.pointy > 0)
                      {
                          vm.SetState(0); gd.Gamestate = 4;
                          engine.playsound(2);
                          gd.pointerstate = 0; 
                      }
                      break;
                  ///////////////////////////////////////////High Score////////////////////////////////////////////////////////////
                  case 8:
                      //engine.beginBatch(gd.Tfont);
                      engine.textDraw(80, 420, 1.2f, 1.2f, 1, "HighScores", 1, 1, .5f, 1, " ");
                      engine.scoreDraw(80, 340, 200, 200, 0, 0, 0, 1);
                      engine.endBatch();
                      //engine.beginBatch(gd.Tsky);
                      engine.spriteDraw(30, 50, 70, 70, 0, 0, 0f, (int)GameData.Ereg.back);
                      engine.endBatch();
                      if (gd.pointerstate == 1 && gd.pointx < 90 && gd.pointy < 120 && gd.pointx > 0 && gd.pointy > 0)
                      {
                          vm.SetState(0); gd.Gamestate = 4;
                          engine.playsound(2);
                          gd.pointerstate = 0; 
                      }
                      break;




              } break;

      }




  }























////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////









        public static void drawnumber( int n, int x,int y,int stepx)
        {   if(n==0){engine.textDraw( x,y,2f,1f,1,"0",1f,1f,.4f,1f," "); return;}
        while(n > 0)
        {switch(n%10)
        {case 0:engine.textDraw( x,y,2f,1f,1,"0",1f,1f,.4f,1f," ");break;
        case 1:engine.textDraw( x,y,2f,1f,1,"1",1f,1f,.4f,1f," ");break;
        case 2:engine.textDraw( x,y,2f,1f,1,"2",1f,1f,.4f,1f," ");break;
        case 3:engine.textDraw( x,y,2f,1f,1,"3",1f,1f,.4f,1f," ");break;
        case 4:engine.textDraw( x,y,2f,1f,1,"4",1f,1f,.4f,1f," ");break;
        case 5:engine.textDraw( x,y,2f,1f,1,"5",1f,1f,.4f,1f," ");break;
        case 6:engine.textDraw( x,y,2f,1f,1,"6",1f,1f,.4f,1f," ");break;
        case 7:engine.textDraw( x,y,2f,1f,1,"7",1f,1f,.4f,1f," ");break;
        case 8:engine.textDraw( x,y,2f,1f,1,"8",1f,1f,.4f,1f," ");break;
        case 9:engine.textDraw( x,y,2f,1f,1,"9",1f,1f,.4f,1f," ");break;
        }
        n = n / 10;
        x-=stepx;
        }

        }

        public static void State(int state)
        {    gd.Gamestate=state;
        }

        static void quit()
        {
        engine.performtask( -34);
        //intelperceptual.dispose();
        }



        public static void postaudiodata(bool a, bool b)
        {
        gd.musicstate = a;
        gd.soundstate = b;
        }
 

        //////////////////////////////////////////////////Initialize the game data call it when ever you need to reset game data it took a while  //////////////////////////////////////////
        public static void Init()
        {   r1.regiony=200;
        r2.regiony=1200;
        r3.regiony=2200;
        r1.regionx=160;
        r2.regionx=160;
        r3.regionx=160;
        gd.coins=0;
        gd.texturnum=engine.assetsize(1);
        gd.texturreigonnum=engine.assetsize(2);
        gd.soundnum=engine.assetsize(3);
        gd.coinarrayx= new int[gd.maxcoins];
        gd.coinarrayy= new int[gd.maxcoins];
        gd.enemyx =new int[gd.maxenemey];
        gd.enemyy= new int[gd.maxenemey];
        gd.enemytype=new int[gd.maxenemey];
        gd.enemylength=new int[gd.maxenemey];
        gd.enemyangle=new int[gd.maxenemey];
        gd.regionheight=new int[gd.texturreigonnum];
        gd.regionwidth=new int[gd.texturreigonnum];
        gd.associatedtextures=new int[gd.texturreigonnum];
        gd.screenheight=engine.assetsize(4);
        gd.screenwidth=engine.assetsize(5);
        gd.bgx=160;
        gd.bgy=210;
        int i=0;
        while(i<gd.texturreigonnum)
        { gd.regionwidth[i]= engine.assetdatasize(1,i);
        gd.regionheight[i]=engine.assetdatasize(2,i);
        gd.associatedtextures[i]=engine.assetdatasize(3,i);
        i++;
        }
        i=0;
        gd.herox=160;
        gd.heroy=50;
        int k=50;
        gd.herominx=0+k;
        gd.heromaxx=360-k-34;
        gd.senstivity=(int)(Math.Min(6+(float)(gd.score/500),16));
        gd.heroangle=0;
        genregion(1);
        
        }
        ///////////////////////////////////////////////////////////////Genrate the region data //////////////////////////////////////////

        public static void genregion(int i)
        {
        int k=0;
        while(k<gd.maxcoins)
        {
        gd.coinarrayx[k]= (k%7)*40+30;
        gd.coinarrayy[k]=(k/7)*45;
        int a= rand()%10;
        if(gd.coinarrayx[k]>310||gd.coinarrayx[k]<50||a<7)
        { gd.coinarrayx[k]-=600;
        }
        //gd.coinarrayy[k]=(k/8-2)*90;
        k++;

        }k=0;


        while(k<gd.maxenemey)
        {
        gd.enemyx[k]=(k*134)%210;
        gd.enemyx[k]+=55;
        int a= (rand()+rand()+rand()+rand())%100;
        if(gd.enemyx[k]>310||gd.enemyx[k]<50||a<15)
        {   gd.enemyx[k]=2500;
        }


        int [] ja={45,-60,90,-45,30,45,-64,30,-23};
        gd.enemyangle[k]=ja[rand()%9];
        gd.enemytype[k]=rand()%4;

        /////////////////////////////////////rechance to get type 2 enemey//////////////////////////////////////////
        if(gd.enemytype[k]==0||gd.enemytype[k]==1)
        gd.enemytype[k]=rand()%4;
        int []b={85,60,90,100,80,118};
        gd.enemylength[k]=b[rand()%6];

        if(gd.enemytype[k]==0||gd.enemytype[k]==1)
        gd.enemyy[k]=(k-10)*80;
        else
        gd.enemyy[k]=(k-10)*150;

        if(gd.enemyy[k]+r1.regiony>-100&&gd.enemyy[k]+r1.regiony<250)
        {gd.enemyy[k]+=gd.herox+450-(int)r1.regiony;
        }
        k++;
        }




        }

        private static int rand()
        {
        return Assets.rand.Next(32541065);

        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

   
    
    
    
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////input////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        static int bp=0;
    
        public static void Update(float ax,float ay,float az)
        {gd.cur_ax=ax;
        gd.cur_ay=ay;
        gd.cur_az=az;
        if(gd.herox>=gd.herominx&&gd.herox<=gd.heromaxx&&gd.Gamestate==0)
        {gd.herox=(int)(gd.herox+ax*gd.senstivity);
        }
        if(gd.herox<gd.herominx)
        gd.herox=gd.herominx;
        if(gd.herox>gd.heromaxx)
        gd.herox=gd.heromaxx;

        }
        public static void Update()
        {

        }
        public static bool Update(int key,int state)
        {
        gd.key=key;
        gd.keystate=state;

        if(gd.key==50 &&gd.keystate==1)
        {if(gd.Gamestate==0)
         gd.Gamestate=3;
         else  if(vm.GetState()!=0&&gd.Gamestate!=3)
         {vm.SetState(0);
         gd.Gamestate=4;
          bp=0;
         }
        if(gd.Gamestate!=4)
        {
        bp++;
        }
        if(gd.Gamestate==4&&vm.GetState()==0)
        {
        if(bp==2)
        {bp=0;
        Main.context.Exit();
        }
        else
        {bp++;
        }
        }
        return true;
       }



        /////////////////////////////////////hero on emulator via left and right/////////////////
        if(gd.herox>=gd.herominx&&gd.herox<=gd.heromaxx&&gd.Gamestate==0)
        {
        if(key==20&&state==1)
        gd.herox=gd.herox-gd.senstivity;
        else if(key==21 &&state==1)
        gd.herox=gd.herox+gd.senstivity;


        if(gd.herox<gd.herominx)
        gd.herox=gd.herominx;
        if(gd.herox>gd.heromaxx)
        gd.herox=gd.heromaxx;

        return false;
        }
        return false;
        }
        public static void Update(float tx,float ty,int state,int index)
        {
        if(index==0)
        {gd.pointx1=tx;
        gd.pointy1=ty;
        gd.pointerstate=state;
        }

        gd.pointx = 825 * (gd.pointx1 - (Program.game.Width / 3.5f)) / Program.game.Width;
        gd.pointy =480- 480 *(gd.pointy1-Program.game.Height/50) / Program.game.Height;

        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////input////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        internal static void Update1(float ax, float ay, float az)
        {


            

            gd.cur_ax = ax;
            gd.cur_ay = ay;
            gd.cur_az = az;

           

             if (gd.herox >= gd.herominx && gd.herox <= gd.heromaxx && gd.Gamestate == 0)
            {
                gd.herox = (int)(gd.herox + gd.cur_ax * gd.senstivity*10);
            }
            if (gd.herox < gd.herominx)
                gd.herox = gd.herominx;
            if (gd.herox > gd.heromaxx)
                gd.herox = gd.heromaxx;

            
        }
    }
}
 