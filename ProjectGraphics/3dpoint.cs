using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lec3
{
    class _3dpoint
    {
        public double x, y, z;
        public _3dpoint(double x1, double y1, double z1)
        {
            x = x1;
            y = y1;
            z = z1;
        }

        public _3dpoint(_3dpoint p)
        {
            x = p.x;
            y = p.y;
            z = p.z;
        }
    }
}
