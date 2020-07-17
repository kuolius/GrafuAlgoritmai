using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GrafuAlgoritmai
{
    public class Main
    {
        Rectangle PPLOTButton;
        Rectangle PPLOTOutlines;
        Rectangle KMJMButton;
        Rectangle KMJMOutlines;
        Rectangle PMJMButton;
        Rectangle PMJMOutlines;

        SpriteBatch spriteBatch;
        GraphicsDevice graphics;
        ContentManager Content;

        Texture2D PPLOTButtonTexture;
        Texture2D PPLOTOutlinesTexture;
        Texture2D KMJMButtonTexture;
        Texture2D KMJMOutlinesTexture;
        Texture2D PMJMButtonTexture;
        Texture2D PMJMOutlinesTexture;

        Texture2D PPLOTString;
        Rectangle PPLOTStringRectangle;
        Texture2D KMJMString;
        Rectangle KMJMStringRectangle;
        Texture2D PMJMString;
        Rectangle PMJMStringRectangle;

        Screen screen;
        MouseState mouseState;
        GameWindow Window;
        

        public Main(Screen screen,GameWindow Window)
        {
            this.Window = Window;
            this.screen = screen;
        
        }


        public void Initialize(SpriteBatch spriteBatch,GraphicsDevice graphics,ContentManager Content)
        {
            this.spriteBatch = spriteBatch;
            this.graphics = graphics;
            this.Content = Content;
            mouseState = new MouseState();
            Point middle = new Point(graphics.Viewport.Bounds.Width / 2, graphics.Viewport.Bounds.Height / 2);

            //PPLOT
            PPLOTButton = new Rectangle(new Point(middle.X-75, middle.Y-70), new Point(150, 40));
            PPLOTOutlines = new Rectangle(new Point(middle.X - 76, middle.Y - 71), new Point(152, 42));

            PPLOTButtonTexture = new Texture2D(graphics,1,1,false,SurfaceFormat.Color);
            PPLOTButtonTexture.SetData<Color>(new Color[] { new Color(66, 244, 128) });

            PPLOTOutlinesTexture = new Texture2D(graphics, 1, 1, false, SurfaceFormat.Color);
            PPLOTOutlinesTexture.SetData<Color>(new Color[] { new Color(1, 137, 21) });

            PPLOTString = Content.Load<Texture2D>("PPLOT");
            PPLOTStringRectangle = new Rectangle(new Point(middle.X-PPLOTString.Width/2,middle.Y-50-PPLOTString.Height/2), new Point(PPLOTString.Width,PPLOTString.Height));

            //KMJM
            KMJMButton = new Rectangle(new Point(middle.X - 75, middle.Y - 20), new Point(150, 40));
            KMJMOutlines = new Rectangle(new Point(middle.X - 76, middle.Y - 21), new Point(152, 42));

            KMJMButtonTexture = new Texture2D(graphics, 1, 1, false, SurfaceFormat.Color);
            KMJMButtonTexture.SetData<Color>(new Color[] { new Color(66, 244, 128) });

            KMJMOutlinesTexture = new Texture2D(graphics, 1, 1, false, SurfaceFormat.Color);
            KMJMOutlinesTexture.SetData<Color>(new Color[] { new Color(1, 137, 21) });

            KMJMString = Content.Load<Texture2D>("KMJM");
            KMJMStringRectangle = new Rectangle(new Point(middle.X - KMJMString.Width / 2, middle.Y - KMJMString.Height / 2), new Point(KMJMString.Width, KMJMString.Height));


            //PMJM
            PMJMButton = new Rectangle(new Point(middle.X - 75, middle.Y + 30), new Point(150, 40));
            PMJMOutlines = new Rectangle(new Point(middle.X - 76, middle.Y + 29), new Point(152, 42));

            PMJMButtonTexture = new Texture2D(graphics, 1, 1, false, SurfaceFormat.Color);
            PMJMButtonTexture.SetData<Color>(new Color[] { new Color(66, 244, 128) });

            PMJMOutlinesTexture = new Texture2D(graphics, 1, 1, false, SurfaceFormat.Color);
            PMJMOutlinesTexture.SetData<Color>(new Color[] { new Color(1, 137, 21) });

            PMJMString = Content.Load<Texture2D>("PMJM");
            PMJMStringRectangle = new Rectangle(new Point(middle.X - PMJMString.Width / 2, middle.Y + 50 - PMJMString.Height / 2), new Point(PMJMString.Width, PMJMString.Height));


        }

        public void Update(out Screen screen,Screen screenIn)
        {
            mouseState = Mouse.GetState();
            this.screen = screenIn;
            
            Point middle = new Point(Window.ClientBounds.Width/2, Window.ClientBounds.Height/2);
            if (mouseState.LeftButton == ButtonState.Pressed && mouseState.Position.X <= middle.X  + 75 && mouseState.Position.X >= middle.X  - 75)
            {
                if ( mouseState.Position.Y >= middle.Y  - 70 && mouseState.Position.Y <= middle.Y  - 30)
                {
                    this.screen = Screen.PPLOT;
                }

                else if ( mouseState.Position.Y >= middle.Y  - 20 && mouseState.Position.Y <= middle.Y  +20)
                {
                    this.screen = Screen.KMJM;
                }

                else if (mouseState.Position.Y >= middle.Y + 30 && mouseState.Position.Y <= middle.Y + 70)
                {
                    this.screen = Screen.PMJM;
                }

            }
            screen = this.screen;
        }

        public void Draw()
        {
            
            spriteBatch.Begin();
            //PPLOT
            spriteBatch.Draw(PPLOTOutlinesTexture, PPLOTOutlines, Color.White);
            spriteBatch.Draw(PPLOTButtonTexture, PPLOTButton, Color.White);
            spriteBatch.Draw(PPLOTString, PPLOTStringRectangle, Color.White);
            //KMJM
            spriteBatch.Draw(KMJMOutlinesTexture, KMJMOutlines, Color.White);
            spriteBatch.Draw(KMJMButtonTexture, KMJMButton, Color.White);
            spriteBatch.Draw(KMJMString, KMJMStringRectangle, Color.White);
            //PMJM
            spriteBatch.Draw(PMJMOutlinesTexture, PMJMOutlines, Color.White);
            spriteBatch.Draw(PMJMButtonTexture, PMJMButton, Color.White);
            spriteBatch.Draw(PMJMString, PMJMStringRectangle, Color.White);
            spriteBatch.End();

        }
    }
}
