using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
namespace Lec3
{
     class _3DModel
    {
        public List<_3dpoint> points;
        public List<_2dpoint> points2d;
        public List<edge> Edges;
        public int XB = 0;
        public int YB = 0;

        public List<_3dpoint> L_3D_Pts = new List<_3dpoint>();//List of 3D points
        public List<edge> L_Edges = new List<edge>();//List of edges connecting the 3D points


        public Camera cam;

        public void Clear()
        {
            L_Edges.Clear();
            Edges.Clear();
            points.Clear();
            points2d.Clear();
        }
        public _3DModel(int x,int y,int z)
        {
            points = new List<_3dpoint>();
            points2d = new List<_2dpoint>();
            Edges = new List<edge>();
            //OpenFileDialog dlg = new OpenFileDialog();
            //dlg.ShowDialog();
            StreamReader sr = new StreamReader("Cube.txt");
            string strPt;
            while ((strPt = sr.ReadLine()) != null)
            {
                if (strPt[0] == 'L')
                    break;

                string[] s = strPt.Split(',');
                float[] v = new float[3];
                for (int i = 0; i < 3; i++)
                { v[i] = int.Parse(s[i]); }
                points.Add(new _3dpoint(v[0]+x, v[1]+y, v[2]*z));

            }
            while ((strPt = sr.ReadLine()) != null)
            {
                string[] s1 = strPt.Split(',');
                int[] indx = new int[2];
                indx[0] = int.Parse(s1[0]);
                indx[1] = int.Parse(s1[1]);
                Edges.Add(new edge(indx[0], indx[1]));
            }
            sr.Close();
            points2d=Parallel.Get(points);

        }
        public void Draw(Graphics g)
        {
            Pen PP = new Pen(Color.White, 2);
            for (int i = 0; i < Edges.Count; i++)
            {
                edge ptrv = (edge)Edges[i];

                PointF s = cam.TransformToOrigin_And_Rotate_And_Project((_3dpoint)points[ptrv.e1]);
                PointF e = cam.TransformToOrigin_And_Rotate_And_Project((_3dpoint)points[ptrv.e2]);

                g.DrawLine(PP, s.X + XB, s.Y + YB, e.X + XB, e.Y + YB);
                //g.DrawString(ptrv.e1.ToString(), new Font("Times New Roman", 10), Brushes.Blue, s.X + XB, s.Y + YB);
            }

        }
        public void DrawYourSelf(Graphics g, int ex,bool Colored)
        {
            for (int k = 0; k < Edges.Count; k++)//Drawing edges between specific points in the 3D point list
            {
                int i = Edges[k].e1;
                int j = Edges[k].e2;

                _3dpoint pi = points[i];
                _3dpoint pj = points[j];

                Pen Pn;
                
                if(Colored==true)
                {
                    Pn = new Pen(Color.Red, 2);
                    g.DrawLine(Pn, (int)pi.x + ex, (int)pi.y + ex, (int)pj.x + ex, (int)pj.y + ex);
                }
                else
                {
                    Pn = new Pen(Color.White, 2);
                    g.DrawLine(Pn, (int)pi.x + ex, (int)pi.y + ex, (int)pj.x + ex, (int)pj.y + ex);
                }

            }
        }


        public void CreateCube()//Create Cube from text file in Debug folder
        {


            StreamReader sr = new StreamReader("Cube.txt");


            string strPt;
            while ((strPt = sr.ReadLine()) != null)//While the file has not ended
            {
                if (strPt[0] == 'L')//Break when the letter 'L' appears indicating the end of 3D points
                    break;

                string[] s = strPt.Split(',');//Split at every ','
                float[] v = new float[3];//create a new array of floats for the 3D points
                for (int i = 0; i < 3; i++)
                { v[i] = float.Parse(s[i]); }//Parse each string into a float
                L_3D_Pts.Add(new _3dpoint(v[0], v[1], v[2]));//Add the 3D point into the 3D point list

            }
            int cl_i = 0;
            while ((strPt = sr.ReadLine()) != null)//Continue reading the file until it ends
            {
                string[] s1 = strPt.Split(',');
                int[] indx = new int[2];
                indx[0] = int.Parse(s1[0]);//Parse each edge index into an int
                indx[1] = int.Parse(s1[1]);
                Color[] cl = { Color.Red, Color.Yellow, Color.Black, Color.Blue };//Choose a colour
                edge pnn = new edge(indx[0], indx[1]);//Create a new edge with these indicies
                
                L_Edges.Add(pnn);//Add the new edge into the Edges list
                cl_i++;
                if (cl_i == 4)
                {
                    cl_i = 0;
                }

            }
            sr.Close();//Close the streamreader


        }

    }
}
