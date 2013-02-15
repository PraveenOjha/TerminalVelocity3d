using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
namespace TerVel

{

public class spritebatchextension 
{        
    

	
    public spritebatchextension(int maxSprites)
    {         
    }       
    
    public void beginBatch(Texture texture)
    {
    }
    
    public void endBatch()
    {
    }
    
    public void drawSprite(float x, float y, float width, float height, TextureRegion region) 
    {
    	drawSprite(x, y,width,  height,  0F,0,0, region);
        
    }
    
    public void drawSprite(float x, float y, float width, float height, float angle, TextureRegion region) 
    {
        drawSprite(x, y, width, height, angle, 0, 0, region);
        
    }
	
	 public void drawSprite(float x, float y, float width, float height, float angle,float px,float py, TextureRegion region) 
	 {
         float sx = width /( region.rectangle.Width);
         float sy= -height /( region.rectangle.Height);
         //y =- y;
         //py = -py;
        // height = -height;
         //TerVel.Main.spriteBatch.Draw(region.texhold.tex, new Vector2(x,y), region.rectangle, Color.White, angle * -0.0174533F, new Vector2(width/2+px,-height/2+py), new Vector2(sx, sy), SpriteEffects.None, 0);
          //Vector2 scale= new Vector2(sx,sy);
         if (sx < 0)
         {
             sx = sx * -1;
             Vector2 scale = new Vector2(sx, sy);
             Vector2 origin = new Vector2((width / 2 + px) / sx, -(height / 2 + py) / (sy));
             Vector2 position = new Vector2(x, y);
             TerVel.Main.spriteBatch.Draw(region.texhold.tex, position, region.rectangle, Assets.color, angle * 0.0174533F, origin, scale, SpriteEffects.FlipHorizontally, 0);
         }
         else
         {
             Vector2 scale = new Vector2(sx, sy);
             Vector2 origin = new Vector2((width / 2 + px) / sx, -(height / 2 + py) / (sy));
             Vector2 position = new Vector2(x, y);

             TerVel.Main.spriteBatch.Draw(region.texhold.tex, position, region.rectangle, Assets.color, angle * 0.0174533F, origin, scale, SpriteEffects.None, 0);
         }
          
 

     }

}

	
}