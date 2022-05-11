using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lec3
{
    class Transform
    {
        public Transform()
        {
        }
        public static void Scale(_3DModel a, float sx, float sy, float sz)//Resize the object, either bigger or smaller
        {
            for (int i = 0; i < a.points.Count; i++)
            {
                a.points[i].x *=sx;
                a.points[i].y *= sy;
                a.points[i].z *= sz;

            }
        }
        public static void Translate(_3DModel a,double xr, double yr, double zr)
        {
            for (int i = 0; i < a.points.Count; i++)
            {
                a.points[i].x += xr;
                a.points[i].y += yr;
                a.points[i].z += zr;

            }
        }
        public static void TranslateOld(List<_3dpoint> L_Pts, float xr, float yr, float zr)//Used to translate all 
        {
            for (int i = 0; i < L_Pts.Count; i++)
            {
                L_Pts[i].x += xr;
                L_Pts[i].y += yr;
                L_Pts[i].x += zr;

            }
        }
        public static void RotateX(_3DModel a,double theta)
        {
            //theta = (float)(theta * Math.PI / 180.0);
            for (int i = 0; i < a.points.Count; i++)
            {
                double y1=((a.points[i].y)*Math.Cos(theta));
                double z1=((a.points[i].z)* Math.Sin(theta));

                double z2 = ((a.points[i].z) * Math.Cos(theta));
                double y2 = ((a.points[i].y) * Math.Sin(theta));
                a.points[i].y = (float)(y1 - z1);
                a.points[i].z = (float)(y2 + z2);
            }
        }

        public static void RotateXOld(List<_3dpoint> L_Pts, float theta)//Rotate a list of 3D point using angle theta on the X axis
        {

            float th = (float)(Math.PI * theta / 180);//Simila to Graphics1 the correct theta is calculated using the given theta
            for (int i = 0; i < L_Pts.Count; i++)
            {
                _3dpoint p = L_Pts[i];

                
                float y_ = (float)(p.y * Math.Cos(th) - p.z * Math.Sin(th));//Y changes based on its mathematical equation
                float z_ = (float)(p.y * Math.Sin(th) + p.z * Math.Cos(th));//Z changes based on its mathematical equation

                //The new values are placed in the 3D list
                p.y = y_;
                p.z = z_;
            }
        }
        public static void RotatYOld(List<_3dpoint> L_Pts, float theta)
        {

            float th = (float)(Math.PI * theta / 180);
            for (int i = 0; i < L_Pts.Count; i++)
            {
                _3dpoint p = L_Pts[i];


                float x_ = (float)(p.z * Math.Sin(th) + p.x * Math.Cos(th));
                float z_ = (float)(p.z * Math.Cos(th) - p.x * Math.Sin(th));


                p.x = x_;
                p.z = z_;
            }
        }

        public static void RotatZOld(List<_3dpoint> L_Pts, float theta)
        {

            float th = (float)(Math.PI * theta / 180);
            for (int i = 0; i < L_Pts.Count; i++)
            {
                _3dpoint p = L_Pts[i];


                float x_ = (float)(p.x * Math.Cos(th) - p.y * Math.Sin(th));
                float y_ = (float)(p.x * Math.Sin(th) + p.y * Math.Cos(th));

                p.x = x_;
                p.y = y_;
            }
        }
        public static void RotateY(_3DModel a, double theta)
        {
            //theta = (float)(theta * Math.PI / 180.0);
            for (int i = 0; i < a.points.Count; i++)
            {
                double z1 = ((a.points[i].z) * Math.Cos(theta));
                double x1 = ((a.points[i].x) * Math.Sin(theta));

                double x2 = ((a.points[i].x) * Math.Cos(theta));
                double z2 = ((a.points[i].z) * Math.Sin(theta));
                a.points[i].z = (float)(z1 - x1);
                a.points[i].x = (float)(x2 + z2);
            }
        }
        public static void RotateZ(_3DModel a, double theta)
        {
            //theta = (float)(theta * Math.PI / 180.0);

            for (int i = 0; i < a.points.Count; i++)
            {
                double x1 = ((a.points[i].x) * Math.Cos(theta));
                double y1 = ((a.points[i].y) * Math.Sin(theta));

                double y2 = ((a.points[i].y) * Math.Cos(theta));
                double x2 = ((a.points[i].x) * Math.Sin(theta));
                a.points[i].x = (float)(x1 - y1);
                a.points[i].y = (float)(x2 + y2);
            }
        }
        public static void RotateArbitrary(_3DModel L_Pts,
                                           _3dpoint v1,
                                           _3dpoint v2,
                                           float ang)//The list of 3D points will rotate around 2 specific 3D point v1 and v2 using a given angle
        {
            Transform.RotateX(L_Pts, v1.x * -1);//The X values of the 3D list is translated to have the first 3D point start at 0,0,0
            Transform.RotateY(L_Pts, v1.y * -1);//The Y values of the 3D list is translated to have the first 3D point start at 0,0,0
            Transform.RotateZ(L_Pts, v1.z * -1);//The Z values of the 3D list is translated to have the first 3D point start at 0,0,0

            double dx = v2.x - v1.x;//The difference between the X values of the second 3D point and the translated X of the first 3D point
            double dy = v2.y - v1.y;//The difference between the Y values of the second 3D point and the translated X of the first 3D point
            double dz = v2.z - v1.z;//The difference between the Z values of the second 3D point and the translated X of the first 3D point

            float theta = (float)Math.Atan2(dy, dx);//Theta is calculated based on the proof in the project files
            float phi = (float)Math.Atan2(Math.Sqrt(dx * dx + dy * dy), dz);//phi is calculated based on the proof in the project files

            theta = (float)(theta * 180 / Math.PI);//Theta is then converted to radian value as per graphics 1
            phi = (float)(phi * 180 / Math.PI);//Phi is also converted to radian value

            //To rotate the given 3d list around V1 and V2, the following code lines are used based on the proof in the project file
            Transform.RotateZ(L_Pts, theta * -1);//Start inverse rotation around Z axis using theta
            Transform.RotateY(L_Pts, phi * -1);//Start inverse rotation around Y axis using phi

            Transform.RotateZ(L_Pts, ang);//Start rotation around Z axis using ang

            Transform.RotateY(L_Pts, phi * 1);//Start rotation around Y axis using phi
            Transform.RotateZ(L_Pts, theta * 1);//Start rotation around Z axis using theta


            Transform.Translate(L_Pts,0,0, v1.z * 1);//Returns the Z values of v1 to original place
            Transform.Translate(L_Pts,0, v1.y * 1,0);//Returns the Y values of v1 to original place
            Transform.Translate(L_Pts, v1.x * 1,0,0);//Returns the X values of v1 to original place
        }

        public static void TranslateX(List<_3dpoint> L_Pts, float tx)//The X values of the given list are translted (moved), usually to start its plane from x,y,z of 0,0,0
        {
            for (int i = 0; i < L_Pts.Count; i++)
            {
                _3dpoint p = L_Pts[i];
                p.x += tx;
            }
        }

        public static void TranslateY(List<_3dpoint> L_Pts, float ty)
        {
            for (int i = 0; i < L_Pts.Count; i++)
            {
                _3dpoint p = L_Pts[i];
                p.y += ty;
            }
        }

        public static void TranslateZ(List<_3dpoint> L_Pts, float tz)
        {
            for (int i = 0; i < L_Pts.Count; i++)
            {
                _3dpoint p = L_Pts[i];
                p.z += tz;
            }
        }
        public static void RotateArbitraryOld(List<_3dpoint> L_Pts,
                                           _3dpoint v1,
                                           _3dpoint v2,
                                           float ang)//The list of 3D points will rotate around 2 specific 3D point v1 and v2 using a given angle
        {
            Transform.TranslateX(L_Pts, (float)v1.x * -1);//The X values of the 3D list is translated to have the first 3D point start at 0,0,0
            Transform.TranslateY(L_Pts, (float)v1.y * -1);//The Y values of the 3D list is translated to have the first 3D point start at 0,0,0
            Transform.TranslateZ(L_Pts, (float)v1.z * -1);//The Z values of the 3D list is translated to have the first 3D point start at 0,0,0

            double dx = v2.x - v1.x;//The difference between the X values of the second 3D point and the translated X of the first 3D point
            double dy = v2.y - v1.y;//The difference between the Y values of the second 3D point and the translated X of the first 3D point
            double dz = v2.z - v1.z;//The difference between the Z values of the second 3D point and the translated X of the first 3D point

            float theta = (float)Math.Atan2(dy, dx);//Theta is calculated based on the proof in the project files
            float phi = (float)Math.Atan2(Math.Sqrt(dx * dx + dy * dy), dz);//phi is calculated based on the proof in the project files

            theta = (float)(theta * 180 / Math.PI);//Theta is then converted to radian value as per graphics 1
            phi = (float)(phi * 180 / Math.PI);//Phi is also converted to radian value

            //To rotate the given 3d list around V1 and V2, the following code lines are used based on the proof in the project file
            Transform.RotatZOld(L_Pts, theta * -1);//Start inverse rotation around Z axis using theta
            Transform.RotatYOld(L_Pts, phi * -1);//Start inverse rotation around Y axis using phi

            Transform.RotatZOld(L_Pts, ang);//Start rotation around Z axis using ang

            Transform.RotatYOld(L_Pts, phi * 1);//Start rotation around Y axis using phi
            Transform.RotatZOld(L_Pts, theta * 1);//Start rotation around Z axis using theta


            Transform.TranslateZ(L_Pts, (float)v1.z * 1);//Returns the Z values of v1 to original place
            Transform.TranslateY(L_Pts, (float)v1.y * 1);//Returns the Y values of v1 to original place
            Transform.TranslateX(L_Pts, (float)v1.x * 1);//Returns the X values of v1 to original place
        }
    }
}
