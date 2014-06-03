using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetectSystem
{
    public class Model
    {
        public int PointPer
        {
            set;
            get;
        }

        public Rectangle MinRectangle
        {
            set;
            get;
        }

        public MyDefPoint[] Polygons
        {
            set;
            get;
        }



        public void AddPolygons(double x,double y)
        {
            if (PointPer == 3)
            {
                PointPer = -1;
                MinRectangle = new Rectangle(GetMinX(Polygons), GetMinY(Polygons), GetMaxX(Polygons), GetMaxY(Polygons));
            }
            Polygons[++PointPer] = new MyDefPoint(x, y);
        }

        private double GetTriangleValue(MyDefPoint p1, MyDefPoint p2, MyDefPoint p3)
        {
            MyDefPoint s1 = new MyDefPoint(p1.X - p2.X, p1.Y - p2.Y);
            MyDefPoint s2 = new MyDefPoint(p1.X - p3.X, p1.Y - p3.Y);
            return Math.Abs(0.5 * (s1.X * s2.Y - s1.Y * s2.X));
        }

        public double CalcaluateArea()
        {
            return GetTriangleValue(Polygons[0], Polygons[1], Polygons[2]) + GetTriangleValue(Polygons[2], Polygons[3], Polygons[0]);
        }

        public Model()
        {
            PointPer = -1;
            Polygons = new MyDefPoint[4];
        }
    }
}
