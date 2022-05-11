using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lec3
{
     static class Parallel
    {
        public static List<_2dpoint> a;
        public static List<_2dpoint> Get(List<_3dpoint> point)
        {
            a = new List<_2dpoint>();
            for (int i = 0; i < point.Count; i++)
            {
                a.Add(new _2dpoint((float)point[i].x, (float)point[i].y));
            }
            return a;
        }
        public static List<_2dpoint> Prespective(ref List<_3dpoint> point)
        {
            a = new List<_2dpoint>();
            for (int i = 0; i < point.Count; i++)
            {
                float yp = (float)(point[i].y * (200 / point[i].z));
                float xp = (float)(point[i].x * (200 / point[i].z));
                a.Add(new _2dpoint(xp, yp));
            }
            return a;
        }
        public static void DoPrespectiveProjection(_3dpoint e, _3dpoint n, float focal)//Calculate the presepctive projection equations
        {
            n.x = focal * e.x / e.z;
            n.y = focal * e.y / e.z;
            n.z = focal;
        }

    }
}
