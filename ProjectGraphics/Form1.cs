using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lec3;
namespace ProjectGraphics
{
    public partial class Form1 : Form
    {
        int XB = 0;
        int YB = 0;
        int cx = 400;
        int cy = 400;
        
        
        _3DModel Cube = new _3DModel(0,0,1);
        Camera cam = new Camera();
        Timer t = new Timer();
        Bitmap off;
        //List<_3DModel> Cubes = new List<_3DModel>();
        List<_3DModel> Cubes = new List<_3DModel>();
        public Form1()
        {
            this.WindowState = FormWindowState.Maximized;
            this.Paint += Form1_Paint;
            this.Load += Form1_Load;
            KeyDown += Form1_KeyDown;
            t.Tick += T_Tick;
            t.Start();
        }

        private void T_Tick(object sender, EventArgs e)
        {
            DrawDubble(CreateGraphics());
        }
        int speed = 5;

        void scroll(int dir)
        {
            for (int i = 0; i < Cubes.Count; i++)
            {
                Transform.TranslateOld(Cubes[i].points,0,9.85f*dir,0);
            }
            for (int i = 0; i < Gaps.Count; i++)
            {
                Transform.TranslateOld(Gaps[i].points, 0, 9.85f * dir, 0);
            }
            Transform.TranslateOld(Cube1.points, 0, 9.85f*dir, 0);
        }
        int CurrentCube = 92;
        float Scaler = 0.99f;
        bool Dead = false;
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (Dead == false)
            {
                switch (e.KeyCode)
                {
                    case Keys.Up:
                        if (CurrentCube < 5)
                        {
                            break;
                        }
                        _3dpoint p1 = new _3dpoint(Cubes[CurrentCube].points[Cubes[CurrentCube].Edges[5].e1]);//Create a new 3D point representing one end of a specific edge of the plane
                        _3dpoint p2 = new _3dpoint(Cubes[CurrentCube].points[Cubes[CurrentCube].Edges[5].e2]);//Create a new 3D point representing the other end of a specific edge of the plane
                        for (int i = 0; i < 18; i++)
                        {
                            Transform.RotateArbitraryOld(Cube1.points, p1, p2, -speed);
                            cam.BuildNewSystem();
                            DrawDubble(CreateGraphics());
                        }
                        for (int i = 0; i < 8; i++)
                        {
                            scroll(1);
                            DrawDubble(CreateGraphics());
                        }
                        CurrentCube -= 5;
                        break;
                    case Keys.Down:
                        if (CurrentCube > 94)
                        {
                            break;
                        }
                        p1 = new _3dpoint(Cubes[CurrentCube].points[Cubes[CurrentCube].Edges[1].e1]);//Create a new 3D point representing one end of a specific edge of the plane
                        p2 = new _3dpoint(Cubes[CurrentCube].points[Cubes[CurrentCube].Edges[1].e2]);//Create a new 3D point representing the other end of a specific edge of the plane
                        for (int i = 0; i < 18; i++)
                        {
                            Transform.RotateArbitraryOld(Cube1.points, p1, p2, speed);
                            cam.BuildNewSystem();
                            DrawDubble(CreateGraphics());
                        }
                        for (int i = 0; i < 8; i++)
                        {
                            scroll(-1);
                            DrawDubble(CreateGraphics());
                        }
                        CurrentCube += 5;
                        break;
                    case Keys.Left:
                        if (CurrentCube % 10 == 0 || CurrentCube % 10 == 5)
                        {
                            break;
                        }
                        p1 = new _3dpoint(Cubes[CurrentCube].points[Cubes[CurrentCube].Edges[9].e1]);//Create a new 3D point representing one end of a specific edge of the plane
                        p2 = new _3dpoint(Cubes[CurrentCube].points[Cubes[CurrentCube].Edges[9].e2]);//Create a new 3D point representing the other end of a specific edge of the plane
                        for (int i = 0; i < 18; i++)
                        {
                            Transform.RotateArbitraryOld(Cube1.points, p1, p2, speed);
                            cam.BuildNewSystem();
                            DrawDubble(CreateGraphics());
                        }
                        CurrentCube--;
                        break;
                    case Keys.Right:
                        if (CurrentCube % 10 == 9 || CurrentCube % 10 == 4)
                        {
                            break;
                        }
                        p1 = new _3dpoint(Cubes[CurrentCube].points[Cubes[CurrentCube].Edges[8].e1]);//Create a new 3D point representing one end of a specific edge of the plane
                        p2 = new _3dpoint(Cubes[CurrentCube].points[Cubes[CurrentCube].Edges[8].e2]);//Create a new 3D point representing the other end of a specific edge of the plane
                        for (int i = 0; i < 18; i++)
                        {
                            Transform.RotateArbitraryOld(Cube1.points, p1, p2, -speed);
                            cam.BuildNewSystem();
                            DrawDubble(CreateGraphics());
                        }
                        CurrentCube++;
                        break;
                }

                for (int i = 0; i < GapsPosition.Count; i++)
                {
                    if (GapsPosition[i] == CurrentCube)
                    {
                        Dead = true;
                        for (int j = 0; j < 50; j++)
                        {
                            Scaler = Scaler - 0.0001f;
                            //Transform.Scale(Cube1, Scaler, Scaler, Scaler);
                            Transform.Translate(Cube1,1,1,-15);
                            DrawDubble(CreateGraphics());
                        }

                    }
                }
            }
            cam.BuildNewSystem();
            DrawDubble(CreateGraphics());
        }

        _3DModel Cube1;
        _3DModel pnnGap=new _3DModel(1,1,0);
        List<int> GapsPosition = new List<int>();
        private void Form1_Load(object sender, EventArgs e)
        {
            off = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
            cam.ceneterX = XB + (this.ClientSize.Width / 2); // el camera fel center
            cam.ceneterY = YB + (this.ClientSize.Height / 2);
            cam.lookAt.x = this.ClientSize.Width / 2;
            cam.lookAt.y = this.ClientSize.Height / 2;

            cam.cxScreen = cx;
            cam.cyScreen = cy; 
            cam.BuildNewSystem();
            Transform.Scale(Cube, 0.5f, 0.5f, 0.5f);
            Transform.Translate(Cube,590,400,0);
            Transform.RotateY(Cube,-1);
            Transform.RotatYOld(Cube.points, 65);
            Cube.cam = cam;

            Random rnd = new Random();
            int RandomGap;
            int x = 1500, y = -1900;
            for (int i = 0; i < 100; i++)
            {
                
                _3DModel pnn = new _3DModel(x, y, 0);
                Transform.Scale(pnn, 0.5f, 0.5f, 0.5f);
                pnn.cam = cam;
                Cubes.Add(pnn);

                RandomGap = rnd.Next(20);
                if (RandomGap % 13 == 0)// random equation to get random position of empty spaces
                {
                    pnnGap=new _3DModel(1,1,0);
                    pnnGap.Clear();
                    pnnGap.cam = cam;
                    pnnGap = DesignGap(pnnGap, x, y);
                    Transform.Scale(pnnGap, 0.5f, 0.5f, 0.5f);
                    Gaps.Add(pnnGap);
                    GapsPosition.Add(i);
                }
                
                x += 200;
                if(x>=2500)
                {
                    x = 1500;
                    y += 200;
                }
            }
            Cube1 = new _3DModel(x+400, y-400, 1);
            Transform.Scale(Cube1, 0.5f, 0.5f, 0.5f);
            Cube1.cam = cam;

            Transform.Translate(Cube,350,350,0);
            DrawDubble(CreateGraphics());
        }
        
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            DrawDubble(e.Graphics);
        }
        void DrawScene(Graphics g)
        {
            g.Clear(Color.Black);
            Cube1.Draw(g);
            //Cube1.DrawYourSelf(g, 0, false);
            for (int i = 0; i < Cubes.Count; i++)
            {
                Cubes[i].Draw(g);
                //Cubes[i].DrawYourSelf(g,0, false);
            }
            for (int i = 0; i < Gaps.Count; i++)
            {
                Gaps[i].Draw(g);
                //Gaps[i].DrawYourSelf(g,0,false);
            }
            _3dpoint p1 = new _3dpoint(Cubes[CurrentCube].points[Cubes[CurrentCube].Edges[9].e1]);//Create a new 3D point representing one end of a specific edge of the plane
            _3dpoint p2 = new _3dpoint(Cubes[CurrentCube].points[Cubes[CurrentCube].Edges[9].e2]);//Create a new 3D point representing the other end of a specific edge of the plane
            //g.DrawLine(Pens.Red, (float)p1.x, (float)p1.y,(float)p2.x, (float)p2.y);
            //Cubes[CurrentCube].DrawYourSelf(g, 0, true);
        }
        List<_3DModel> Gaps = new List<_3DModel>();
        _3DModel DesignGap(_3DModel Gap,double xx, double yy)
        {
            int R = 100;
            double x,y, Z = 0;

            int iP = 0;
            for (float th = 0; th < 360; th += 1)
            {
                x = R * Math.Cos(th * Math.PI / 180);
                y = R * Math.Sin(th * Math.PI / 180);

                Gap.points.Add(new _3dpoint((float)x+xx, (float)y+yy, (float)Z));
                if (iP > 0 && th != 0)
                {
                    Gap.Edges.Add(new edge(iP, iP - 1));
                }
                iP++;
            }
            return Gap;
        }

        void DrawDubble(Graphics g)
        {
            Graphics g2 = Graphics.FromImage(off);
            DrawScene(g2);
            g.DrawImage(off, 0, 0);
        }
    }
}
