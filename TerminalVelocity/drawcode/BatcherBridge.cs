

using System;
namespace TerVel
{
 
    public class BatcherBridge
    {
        spritebatchextension batcher;
        public BatcherBridge(spritebatchextension batcher)
        {
            this.batcher = batcher;
        }
        public int beignBatch(int index)
        {
            if (checkerror() != 1)
                return -1;
            else
                batcher.beginBatch(Assets.textures[index]);

            return 1;

        }
        public int endBatch()
        {
            if (checkerror() != 1)
                return -1;
            else
                batcher.endBatch();
            //Assets.playSound(Assets.getsound("coin.ogg"));
            return 1;
        }

        public int drawSprite(float x, float y, float width, float height, float angle, float px, float py, int i)
        {
            //if ((x + Math.Abs(width) < -100 && (y + Math.Abs(height) < -100)) || (x - Math.Abs(width) > 400 && (y - Math.Abs(height) > 500)))
            //    return 0;
            if (checkerror() != 1)
                return -1;
            else
            {
                batcher.drawSprite(x, y, width, height, angle, px, py, Assets.textureregions[i]);
            }
            return 0;
        }











        public int scoreDraw(float x, float y, float width, float height, float angle, float px, float py, int i)
        {
            if (checkerror() != 1)
                return -1;
            else
            {
                if (y == 223)
                {
                    if (Assets.CurrentScore < 9999)
                        x = x - 20;
                    else if (Assets.CurrentScore < 999)
                        x = x - 0;
                    else if (Assets.CurrentScore < 99)
                        x = x + 20;
                    Assets.fonts[1].drawText(batcher, "   " + Assets.CurrentScore, x - 20, 20 + y + 50, 1f, 1f, .6f, 1f);
                    return 0;
                }
                else
                    for (i = 0; i < 7; i++)
                    {

                        Assets.fonts[1].drawText(batcher, "" + (i + 1) + ":" + Settings.highscores[i], x - 20, 20 + y - i * 40, 1f, 1f, .6f, 1f);

                    }

            }
            return 0;
        }










        public int drawText(float x, float y, float width, float height, int font, string text, float sx, float sy, float tx, float ty, string name)
        {
            if (checkerror() != 1)
                return -1;
            else
            {
                Assets.fonts[font].drawText(batcher, text, x, y, sx, sy, tx, ty);
                return 1;
            }

        }


        int checkerror()
        {
            if (batcher == null)
                return -1;
            else
                return 1;
        }
        public int playsound(int i)
        {

            Assets.playSound(Assets.sounds[i]);
            return 1;

        }

        public int music(int a, string name)
        {
            if (a == 1)
            {
                Settings.musicEnabled = true;
                Assets.playMusic();
            }
            if (a == 0)
            {
                Assets.stopMusic();
                Settings.musicEnabled = false;
            }
            if (a == 3)
            {
                Settings.soundEnabled = true;
            }
            if (a == 2)
            {
                Settings.soundEnabled = false;
            }

            return 2;

        }
        public int adjustvolume(float v, int m, string name)
        {
            Assets.adjustvolume(v, m);
            return 3;
        }

        public int vibration(int patternindex)
        {
            Assets.vibra = patternindex;
            //Assets.vibrate(patternindex);
            return 4;

        }


        public int performtask(int index)
        {
            switch (index)
            {
                case 1:
                    Assets.performtask = index; break;
                case -34:
                    Assets.quit = true;
                    break;

            }
            return 0;
        }

        public int write(int score)
        {
            Assets.CurrentScore = score;
            Settings.save(score);
            return 0;

        }

    }
}