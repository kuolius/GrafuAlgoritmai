using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using MonoGame;
using Microsoft.Xna.Framework.Content;
using System;

namespace GrafuAlgoritmai
{
    public class PPLOTmedis
    {
        Screen screen;
        SpriteBatch spriteBatch;
        ContentManager Content;
        MouseState mouseState,previousMouseState;
        KeyboardState keyboardState, previousKeyboardState;
        Rectangle start;
        enum Mode { VA,VR,VM,EA,ER,Idle };
        public enum Clr { B,P,J};
        Mode mode;

        Texture2D VA, VR, VM, EA, ER, Idle;
        SpriteFont mainFont;

        int selectedX = -1;
        int selectedY = -1;
        int selectedQue = 0;

        Random random;

        public int selected=-1;

        public int primeVertix=-1;
        public bool find = false;

       public class Vertix
        {
            public Point coord;
            public Clr color=Clr.B;
            public int d = -1;
            public int p = -1;
            public bool pi = false;

            public Vertix(Point coord)
            {
                this.coord = coord;
            }
            
        }

       public class Edge
        {
            public int x, y;
            public bool pi = false;

            public Edge(int x,int y)
            {
                this.x = x;
                this.y = y;
            }
        }


        public List<Vertix> vertix;
        public List<Edge> edge;

        public PPLOTmedis(Screen screen)
        {
            this.screen = screen;
        }

        public void AddVertix(MouseState mouseState, MouseState previousMouseState)
        {
            if(mouseState.LeftButton==ButtonState.Pressed && previousMouseState.LeftButton==ButtonState.Released)
            {
                foreach(Vertix vx in vertix)
                {
                    if (Math.Abs(vx.coord.X - mouseState.X) <= 10 && Math.Abs(vx.coord.Y - mouseState.Y) <= 10)
                        return;
                }

                vertix.Add(new Vertix(new Point(mouseState.X,mouseState.Y)));
            }

            if(mouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Pressed && selected == -1)
            {
                foreach (Vertix vx in vertix)
                {

                    if (Math.Abs(vx.coord.X - mouseState.X) <= 10 && Math.Abs(vx.coord.Y - mouseState.Y) <= 10)
                    {
                        selected = vertix.IndexOf(vx);
                    }


                }

            }
             if(mouseState.LeftButton == ButtonState.Released && previousMouseState.LeftButton == ButtonState.Pressed && selected!=-1)
            {
                foreach (Vertix vx in vertix)
                {

                    if (Math.Abs(vx.coord.X - mouseState.X) <= 10 && Math.Abs(vx.coord.Y - mouseState.Y) <= 10 && vertix.IndexOf(vx)!=selected)
                    {
                        foreach(Edge eg in edge)
                        {
                            if(eg.x==selected && eg.y== vertix.IndexOf(vx) || eg.y == selected && eg.x == vertix.IndexOf(vx))
                            {
                                edge.Remove(eg);
                                selected = -1;
                                return;
                            }

                        }

                        edge.Add(new Edge(vertix.IndexOf(vx), selected));
                    }


                }
                selected = -1;
            }


            if(mouseState.RightButton==ButtonState.Pressed && previousMouseState.RightButton==ButtonState.Released )
            {
                foreach (Vertix vx in vertix)
                {

                    if (Math.Abs(vx.coord.X - mouseState.X) <= 10 && Math.Abs(vx.coord.Y - mouseState.Y) <= 10)
                    {
                        selected = vertix.IndexOf(vx);
                    }


                }

                if (selected == -1)
                    return;

                List<Edge> remEdge = new List<Edge>();
                foreach (Edge eg in edge)
                {
                    if (eg.x == selected || eg.y == selected)
                        remEdge.Add(eg);
                }

                foreach (Edge rem in remEdge)
                    edge.Remove(rem);


                vertix.RemoveAt(selected);
                foreach (Edge eg in edge)
                {
                    if (eg.x > selected)
                        eg.x -= 1;
                    if (eg.y > selected)
                        eg.y -= 1;
                }
                if (selected == primeVertix)
                    primeVertix = -1;

                selected = -1;
            }
        }
        
        public void MoveVertix(MouseState mouseState, MouseState previousMouseState)
        {

            if (mouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Pressed)
            {

                foreach (Vertix vx in vertix)
                {
                    if (selected == -1)
                    {
                        if (Math.Abs(vx.coord.X - mouseState.X) <= 10 && Math.Abs(vx.coord.Y - mouseState.Y) <= 10)
                        {
                            selected = vertix.IndexOf(vx);
                        }
                    }
                    else if (selected == vertix.IndexOf(vx))
                    {
                        vx.coord.X = mouseState.X;
                        vx.coord.Y = mouseState.Y;
                    }
                }

            }
            else
                selected = -1;
        }

        public void SelectVertix(MouseState mouseState, MouseState previousMouseState)
        {

            if (mouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Pressed)
            {

                foreach (Vertix vx in vertix)
                {
                    if (selected == -1)
                    {
                        if (Math.Abs(vx.coord.X - mouseState.X) <= 10 && Math.Abs(vx.coord.Y - mouseState.Y) <= 10)
                        {
                            selected = vertix.IndexOf(vx);
                        }
                    }
                    else if (selected == vertix.IndexOf(vx))
                    {
                        vx.coord.X = mouseState.X;
                        vx.coord.Y = mouseState.Y;
                    }
                }

            }
            else
                selected = -1;
            /* if (mouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released)
             {

                 foreach (Vertix vx in vertix)
                 {

                     if (Math.Abs(vx.coord.X - mouseState.X) <= 10 && Math.Abs(vx.coord.Y - mouseState.Y) <= 10)
                     {
                         if (selectedX == -1 && selectedY == -1)
                             selectedX = vertix.IndexOf(vx);
                         else if (selectedX != -1 && selectedY == -1 && vertix.IndexOf(vx) != selectedX)
                             selectedY = vertix.IndexOf(vx);
                         else if (selectedX != -1 && selectedY == -1 && vertix.IndexOf(vx) == selectedX)
                             selectedX = -1;
                         else if (selectedX == -1 && selectedY != -1 && vertix.IndexOf(vx) != selectedY)
                             selectedX = vertix.IndexOf(vx);
                         else if (selectedX == -1 && selectedY != -1 && vertix.IndexOf(vx) == selectedY)
                             selectedY = -1;
                         else if (selectedX != -1 && selectedY != -1 && vertix.IndexOf(vx) != selectedY && vertix.IndexOf(vx) != selectedX)
                         {
                             if (selectedQue == 0)
                             {
                                 selectedX = vertix.IndexOf(vx);
                                 selectedQue = 1;
                             }
                             else
                             {
                                 selectedY = vertix.IndexOf(vx);
                                 selectedQue = 0;
                             }
                         }
                         else if (selectedX != -1 && selectedY != -1 && vertix.IndexOf(vx) != selectedY && vertix.IndexOf(vx) == selectedX)
                             selectedX = -1;
                         else if (selectedX != -1 && selectedY != -1 && vertix.IndexOf(vx) == selectedY && vertix.IndexOf(vx) != selectedX)
                             selectedY = -1;

                     }

                 }

             }
             */
            if (mouseState.RightButton == ButtonState.Pressed && previousMouseState.RightButton == ButtonState.Released)
            {

                foreach (Vertix vx in vertix)
                {

                    if (Math.Abs(vx.coord.X - mouseState.X) <= 10 && Math.Abs(vx.coord.Y - mouseState.Y) <= 10)
                    {
                        if (primeVertix == vertix.IndexOf(vx))
                            primeVertix = -1;
                        else
                            primeVertix = vertix.IndexOf(vx);

                    }


                }

            }

        }

        public void AddEdge()
        {
            foreach (Edge eg in edge)
            {
                if (eg.x == selectedX && eg.y == selectedY || eg.x == selectedY && eg.y == selectedX)
                {
                    mode = Mode.Idle;
                    return;
                }
                
            }

            edge.Add(new Edge(selectedX, selectedY));
            
            mode = Mode.Idle;
        }

        public void RemoveEdge()
        {
            foreach (Edge eg in edge)
            {
                if (eg.x == selectedX && eg.y == selectedY || eg.x == selectedY && eg.y == selectedX)
                {
                    edge.Remove(eg);
                    mode = Mode.Idle;
                    return;
                }

            }
            mode = Mode.Idle;

        }

        public void RemoveVertix()
        {
            List<Edge> remEdge = new List<Edge>();
            foreach (Edge eg in edge)
            {
                if (selectedX != -1 && (eg.x == selectedX || eg.y == selectedX) || selectedY != -1 && (eg.x == selectedY || eg.y == selectedY))
                    remEdge.Add(eg);
            }

            foreach (Edge rem in remEdge)
                edge.Remove(rem);


            vertix.RemoveAt(selectedX + selectedY + 1);
            foreach(Edge eg in edge)
            {
                if (eg.x > selectedX + selectedY + 1)
                    eg.x -= 1;
                if (eg.y > selectedX + selectedY + 1)
                    eg.y -= 1;
            }
            if (selectedX + selectedY + 1 == primeVertix)
                primeVertix = -1;

            /*
            if (selectedX != -1)
                vertix.RemoveAt(selectedX);
            else if (selectedY != -1)
                vertix.RemoveAt(selectedY);
            if (selectedX != -1 && selectedX == primeVertix || selectedY != -1 && selectedY == primeVertix)
                primeVertix = -1;

            */    
            selectedX = -1;
            selectedY = -1;
            mode = Mode.Idle;

        }

        public void Execute()
        {
            foreach(Vertix vx in vertix)
            {
                vx.color = Clr.B;
                vx.d = -1;
                vx.p = -1;
                vx.pi = false;
            }

            foreach(Edge eg in edge)
            {
                eg.pi = false;
            }

            vertix[primeVertix].color = Clr.P;
            vertix[primeVertix].d = 0;
            vertix[primeVertix].p = -1;

            List<int> Q = new List<int> { primeVertix };

            while(Q.Count>0)
            {
                int u = Q[0];
                List<int> Adj = new List<int>();
                foreach(Edge eg in edge)
                {
                    if (eg.x == u)
                        Adj.Add(eg.y);
                    else if (eg.y == u)
                        Adj.Add(eg.x);
                }

                foreach(int adj in Adj)
                {
                    if(vertix[adj].color==Clr.B)
                    {
                        vertix[adj].color = Clr.P;
                        vertix[adj].d = vertix[u].d + 1;
                        vertix[adj].p = u;
                        Q.Add(adj);
                    }
                }
                Q.RemoveAt(0);
                vertix[u].color = Clr.J;
            }

            foreach(Vertix vx in vertix)
            {
                if (vx.p != -1 || vertix.IndexOf(vx) == primeVertix)
                    vx.pi = true;
                else
                    vx.pi = false;
            }

            foreach(Edge eg in edge)
            {
                if (eg.x == vertix[eg.y].p || eg.y == vertix[eg.x].p)
                    eg.pi = true;
                else
                    eg.pi = false;
            }
            

        }

        public void Clear()
        {
            edge.Clear();
            vertix.Clear();
            primeVertix = -1;
            selectedX = -1;
            selectedY = -1;
        }

        public bool CheckIfClose(int x, int y)
        {
            foreach (Vertix vx in vertix)
                if (Math.Sqrt(Math.Pow(vx.coord.X - x, 2) + Math.Pow(vx.coord.Y - y, 2)) <= 100)
                    return true;

            return false;

        }

        

        public bool CheckIfExist(int x, int y,List<Point> list)
        {
            foreach (Point eg in list)
                if (eg.X == x && eg.Y == y || eg.X == y && eg.Y == x)
                    return true;
            return false;
        }


        public void Randomize()
        {
            Clear();
            int n = random.Next(5, 10);
            int x, y;

            for(int i=0;i<n;i++)
            {
                x = random.Next(100, 600);
                y = random.Next(100, 400);
                while (CheckIfClose(x, y))
                {
                x = random.Next(100, 600);
                y = random.Next(100, 400);
                }
                vertix.Add(new Vertix(new Point(x, y)));
            }
            primeVertix = random.Next(0, vertix.Count - 1);

            n = random.Next(5, n * (n - 1) / 2);
            List<Point> edgeList = new List<Point>();
            foreach (Vertix vx in vertix)
                foreach (Vertix vz in vertix)
                    if (vx.coord.X != vz.coord.X && vx.coord.Y != vz.coord.Y && !CheckIfExist(vertix.IndexOf(vx), vertix.IndexOf(vz),edgeList))
                        edgeList.Add(new Point(vertix.IndexOf(vx), vertix.IndexOf(vz)));


            for(int i=0;i<n;i++)
            {
                int index = random.Next(0, edgeList.Count - 1);
                x = edgeList[index].X;
                y = edgeList[index].Y;
                edgeList.RemoveAt(index);
                edge.Add(new Edge(x, y));
            }


            
        }

        public void Initialize(SpriteBatch spriteBatch, GraphicsDevice graphics, ContentManager Content)
        {
            this.spriteBatch = spriteBatch;
            this.Content = Content;
            mouseState = new MouseState();
            keyboardState = new KeyboardState();
            previousMouseState = mouseState;
            previousKeyboardState = keyboardState;
            vertix = new List<Vertix>();
            edge = new List<Edge>();
            mode = Mode.Idle;

            VA = Content.Load<Texture2D>("VA");
            VR = Content.Load<Texture2D>("VR");
            VM = Content.Load<Texture2D>("VM");
            EA = Content.Load<Texture2D>("EA");
            ER = Content.Load<Texture2D>("ER");
            Idle = Content.Load<Texture2D>("IDLE");

            mainFont = Content.Load<SpriteFont>("main");

            start = new Rectangle(new Point(0, 0), new Point(100, 50));
            random = new Random();
        }

        public void Update(out Screen screen, Screen screenIn)
        {
            mouseState = Mouse.GetState();
            keyboardState = Keyboard.GetState();
            this.screen = screenIn;
            
            if (previousKeyboardState.IsKeyDown(Keys.Back) && keyboardState.IsKeyUp(Keys.Back))
                this.screen = Screen.Main;

            //Program Start

            switch(mode)
            {
                case Mode.Idle:
                    SelectVertix(mouseState, previousMouseState);
                    break;
                case Mode.VA:
                    AddVertix(mouseState,previousMouseState);
                    break;
                case Mode.VM:
                    MoveVertix(mouseState, previousMouseState);
                    break;
                case Mode.EA:
                    AddEdge();
                    break;
                case Mode.ER:
                    RemoveEdge();
                    break;
                case Mode.VR:
                    RemoveVertix();
                    break;
            }


            //End

            if (previousKeyboardState.IsKeyDown(Keys.A) && keyboardState.IsKeyUp(Keys.A) && (selectedX == -1 || selectedY == -1))
                mode = Mode.VA;
            /*else if (previousKeyboardState.IsKeyDown(Keys.A) && keyboardState.IsKeyUp(Keys.A) && selectedX != -1 && selectedY != -1)
                mode = Mode.EA;
                */
            /*if (previousKeyboardState.IsKeyDown(Keys.R) && keyboardState.IsKeyUp(Keys.R) && (selectedX == -1 && selectedY != -1 || selectedX != -1 && selectedY == -1))
                mode = Mode.VR;
            else if (previousKeyboardState.IsKeyDown(Keys.R) && keyboardState.IsKeyUp(Keys.R) && selectedX != -1 && selectedY != -1)
                mode = Mode.ER;
                */

            if (previousKeyboardState.IsKeyDown(Keys.I) && keyboardState.IsKeyUp(Keys.I))
                mode = Mode.Idle;
            if (previousKeyboardState.IsKeyDown(Keys.R) && keyboardState.IsKeyUp(Keys.R))
                Randomize();
            if (previousKeyboardState.IsKeyDown(Keys.C) && keyboardState.IsKeyUp(Keys.C))
                Clear();

            /* if (previousKeyboardState.IsKeyDown(Keys.M) && keyboardState.IsKeyUp(Keys.M))
                 mode = Mode.VM;
             */

            if (previousKeyboardState.IsKeyDown(Keys.F) && keyboardState.IsKeyUp(Keys.F))
            {
                if (find)
                    find = false;
                else
                    find = true;
            }

            if(find && primeVertix!=-1)
            {
                Execute();
                
            }


           
            screen = this.screen;
            previousMouseState = mouseState;
            previousKeyboardState = keyboardState;
        }

        public void Draw()
        {

            spriteBatch.Begin();

            switch(mode)
            {
                case Mode.Idle:
                    spriteBatch.Draw(Idle, start, Color.White);
                    break;
                case Mode.VA:
                    spriteBatch.Draw(VA, start, Color.White);
                    break;
                case Mode.VR:
                    spriteBatch.Draw(VR, start, Color.White);
                    break;
                case Mode.VM:
                    spriteBatch.Draw(VM, start, Color.White);
                    break;
                case Mode.EA:
                    spriteBatch.Draw(EA, start, Color.White);
                    break;
                case Mode.ER:
                    spriteBatch.Draw(ER, start, Color.White);
                    break;

            }

            foreach (Edge eg in edge)
            {
                if (eg.pi)
                    spriteBatch.DrawLine(new Vector2(vertix[eg.x].coord.X, vertix[eg.x].coord.Y), new Vector2(vertix[eg.y].coord.X, vertix[eg.y].coord.Y), Color.Red, 4);
                else
                    spriteBatch.DrawLine(new Vector2(vertix[eg.x].coord.X, vertix[eg.x].coord.Y), new Vector2(vertix[eg.y].coord.X, vertix[eg.y].coord.Y), Color.Gray, 4);
            }

            foreach (Vertix vx in vertix)
            {
                if (vertix.IndexOf(vx) == primeVertix)
                    spriteBatch.DrawCircle(vx.coord.X, vx.coord.Y, 18, 10, Color.Purple, 2);

                if (vertix.IndexOf(vx)==selectedX || vertix.IndexOf(vx) == selectedY)
                    spriteBatch.DrawCircle(vx.coord.X, vx.coord.Y,14, 10, Color.Blue,2);

                if (vx.pi)
                    spriteBatch.DrawCircle(vx.coord.X, vx.coord.Y, 10, 10, Color.Red, 10);
                else
                    spriteBatch.DrawCircle(vx.coord.X, vx.coord.Y, 10, 10, Color.Black, 10);

                if (vx.d != -1)
                    spriteBatch.DrawString(mainFont, vx.d.ToString(), new Vector2(vx.coord.X + 15, vx.coord.Y-10), Color.Black);
            }


            if (mode == Mode.VA && selected != -1)
                spriteBatch.DrawLine(new Vector2(vertix[selected].coord.X, vertix[selected].coord.Y), new Vector2(mouseState.X, mouseState.Y), Color.Gray, 4);
            
            
            spriteBatch.End();

        }
    }
}
