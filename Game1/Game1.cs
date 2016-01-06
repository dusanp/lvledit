using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Windows.Forms;

namespace Game1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        public static SpriteBatch spriteBatch;
        static Tile[,] tilelist;
        static SelectionButton[] selectionMenu;
        Texture2D SimpleTexture, currentTexture, yellowexampleTexture, greenexampleTexture, blueexampleTexture, purpleexampleTexture;
        public int xdraw, ydraw, tilesize;
        bool drag;
        int xstart;
        int ystart;
        int xdrawstart;
        bool firstQ,firstE;
        int ydrawstart;
        public static SpriteFont Arial;
        public static void inittiles(int width, int height)
        {
            tilelist = new Tile[width, height];
        }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
            tilesize = 25;
            selectionMenu = new SelectionButton[4];
            yellowexampleTexture = new Texture2D(GraphicsDevice, 1, 1);
            blueexampleTexture = new Texture2D(GraphicsDevice, 1, 1);
            greenexampleTexture = new Texture2D(GraphicsDevice, 1, 1);
            purpleexampleTexture = new Texture2D(GraphicsDevice, 1, 1);
            yellowexampleTexture.SetData<Color>(new Color[] { Color.Yellow });
            blueexampleTexture.SetData<Color>(new Color[] { Color.Blue });
            greenexampleTexture.SetData<Color>(new Color[] { Color.Green });
            purpleexampleTexture.SetData<Color>(new Color[] { Color.Purple });
            
            for (int i = 0; i<selectionMenu.Length; i++)
            {
                selectionMenu[i] = new SelectionButton(100 + (50 * i));
            }
            selectionMenu[0].tileskin = yellowexampleTexture;
            selectionMenu[1].tileskin = blueexampleTexture;
            selectionMenu[2].tileskin = greenexampleTexture;
            selectionMenu[3].tileskin = purpleexampleTexture;
            currentTexture = selectionMenu[0].tileskin;

        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            SimpleTexture = new Texture2D(GraphicsDevice, 1, 1);
            SimpleTexture.SetData<Color>(new Color[] { Color.White });
            Arial = Content.Load<SpriteFont>("NewSpriteFont");


            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == Microsoft.Xna.Framework.Input.ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Escape))
                Exit();
            if (Mouse.GetState().X < 700)
            {
                if (Mouse.GetState().LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
                {


                    int x = ((Mouse.GetState().X - xdraw) / tilesize);
                    int y = ((Mouse.GetState().Y - ydraw) / tilesize);
                    if (x > -1 && y > -1 && x < tilelist.GetLength(0) && y < tilelist.GetLength(1))
                    {
                        tilelist[x, y] = new Tile();
                        tilelist[x, y].tileskin = currentTexture;
                    }

                }
                else if ((Mouse.GetState().RightButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed))
                {

                    if (!drag)
                    {
                        xstart = Mouse.GetState().X;
                        ystart = Mouse.GetState().Y;
                        xdrawstart = xdraw;
                        ydrawstart = ydraw;
                        drag = true;
                    }
                    else
                    {
                        try
                        {
                            xdraw = xdrawstart + (Mouse.GetState().X - xstart);
                            ydraw = ydrawstart + (Mouse.GetState().Y - ystart);
                        }
                        catch { }
                    }

                }
                else
                {
                    drag = false;
                }
                if (Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Q))
                {

                    if (!firstQ)
                    {
                        firstQ = true;
                        if (tilesize > 10)
                        {
                            tilesize=tilesize-10;
                            xdraw = xdraw+(((Mouse.GetState().X- xdraw)/(tilesize+10))*10);
                            ydraw = ydraw + (((Mouse.GetState().Y - ydraw) / (tilesize + 10)) * 10);
                        }
                    }
                }
                else { firstQ = false; }
                if (Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.E))
                {

                    if (!firstE)
                    {
                        firstE = true;
                        if (tilesize < 51)
                        {
                            tilesize=tilesize+10;
                            xdraw = xdraw - (((Mouse.GetState().X - xdraw) / (tilesize - 10)) * 10);
                            ydraw = ydraw - (((Mouse.GetState().Y - ydraw) / (tilesize - 10)) * 10);
                        }
                    }
                }
                else { firstE = false; }
            }
            else if(Mouse.GetState().LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
            {
                foreach (SelectionButton s in selectionMenu)
                {
                    if ((Mouse.GetState().X>s.x && Mouse.GetState().X < (s.x+s.size)) &&((Mouse.GetState().Y > s.y && Mouse.GetState().Y < (s.y + s.size))))
                    {
                        currentTexture = s.tileskin;
                    }
                }
            }


            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            spriteBatch.Begin();
            // TODO: Add your drawing code here
            if (tilelist != null)
            {
                for (int a = tilelist.GetLength(0); a > 0; a--)
                {
                    for (int b = tilelist.GetLength(1); b > 0; b--)
                    {

                        if (tilelist[a-1, b-1] != null)
                        {
                            
                            spriteBatch.Draw(tilelist[a-1, b-1].tileskin, new Rectangle(xdraw +((a-1)*tilesize), ydraw+((b-1)* tilesize), tilesize, tilesize),Color.White);
                        }
                    }

                }
                for (int a = tilelist.GetLength(1); a > -1; a--)
                {
                    
                    spriteBatch.Draw(SimpleTexture, new Rectangle(xdraw, ydraw + (a * tilesize), tilesize* tilelist.GetLength(0), 1), Color.Gray);
                }
                for (int a = tilelist.GetLength(0); a > -1; a--)
                {

                    spriteBatch.Draw(SimpleTexture, new Rectangle(xdraw + (a * tilesize), ydraw, 1, tilesize * tilelist.GetLength(1)), Color.Gray);
                }

                
                


            }
            foreach (SelectionButton s in selectionMenu)
            {
                s.Draw();
            }
            spriteBatch.Draw(SimpleTexture, new Rectangle(700, 0, 1, 500), Color.Blue);
            spriteBatch.Draw(SimpleTexture, new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, 3, 3), Color.Red);
            spriteBatch.DrawString(Arial, "LMB to select, RMB to move, Q/E to zoom", new Vector2(), Color.Black);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
