using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetectSystem
{
    public class MyDefPolygon
    {
        public MyDefPoint[] CornerPoint
        {
            set;
            get;
        }

        public MyDefPoint Center
        {
            set;
            get;
        }

        public MyDefPolygon(MyDefPoint[] point,MyDefPoint center)
        {
            CornerPoint = new MyDefPoint[4];
            Center = new MyDefPoint(center.X,Center.Y);
            for (int i = 0; i < 4; i++)
            {
                CornerPoint[i].X = point[i].X;
                CornerPoint[i].Y = point[i].Y;
            }
        }
    }
}
