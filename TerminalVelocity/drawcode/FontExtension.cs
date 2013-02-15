
using TerVel;

public class FontExtension 
{

    public Texture texture;
    public int glyphWidth;
    public int glyphHeight;
    public TextureRegion[] glyphs = new TextureRegion[96];
    private TerVel.Texture texture1;
    private int x;
    private int y;
    private int p1;
    private int p2;
    private int p3;

    public FontExtension(Texture texture, int offsetX, int offsetY, int glyphsPerRow, int glyphWidth, int glyphHeight)
    {
        //super(texture, offsetX, offsetY, glyphsPerRow, glyphWidth, glyphHeight);

        this.texture = texture;
        this.glyphWidth = glyphWidth;
        this.glyphHeight = glyphHeight;
        int x = offsetX;
        int y = offsetY;
        for (int i = 0; i < 96; i++)
        {
            glyphs[i] = new TextureRegion(texture, x, y, glyphWidth, glyphHeight);
            x += glyphWidth;
            if (x == offsetX + glyphsPerRow * 50)
            {
                x = offsetX;
                y += 50;
            }
        }
    }

    
    public void drawText(spritebatchextension batcher, string text, float x, float y)
    {
      drawText(batcher,  text, x, y,1,1,1,1);
    }
    public void drawText(spritebatchextension batcher, string text, float x, float y, float sx, float sy, float tx, float ty)
    {
        int len = text.Length;
        for (int i = 0; i < len; i++)
        {
            int c = text[i] - ' ';
            
            if (c < 0 || c > glyphs.Length - 1)
            continue;

            TextureRegion glyph = glyphs[c];
            batcher.drawSprite(x, y, sx * glyphWidth, sy * glyphHeight, glyph);
            x += glyphWidth * (tx);
        }
    }
}
