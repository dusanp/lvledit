using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game1
{
    class SelectionButton
    {
        public Texture2D tileskin;
        public int x, y, size;
        public void Draw()
        {
            Game1.spriteBatch.Draw(tileskin, new Rectangle(x, y, size, size), Color.White);
        }
        public SelectionButton(int yz)
        {
            y = yz;
            x = 725;
            size = 25;
        }
    }
}
