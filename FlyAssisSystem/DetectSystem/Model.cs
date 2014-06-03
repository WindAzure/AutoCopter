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

        public MyDefPoint PolygonCenter
        {
            set;
            get;
        }

        public MyDefPoint QuadcopterCenter
        {
            set;
            get;
        }

        public MyDefPoint QuadcopterTailCenter
        {
            set;
            get;
        }

        public MyDefPoint[] Polygons
        {
            set;
            get;
        }

        private double GetPolygonsMinX()
        {
            double minX = ConstValue.MIN_LIMMIT;
            for (int i = 0; i < 4; i++)
            {
                if (minX > Polygons[i].X)
                {
                    minX = Polygons[i].X;
                }
            }
            return minX;
        }

        private double GetPolygonsMinY()
        {
            double minY = ConstValue.MIN_LIMMIT;
            for (int i = 0; i < 4; i++)
            {
                if (minY > Polygons[i].Y)
                {
                    minY = Polygons[i].Y;
                }
            }
            return minY;
        }

        private double GetPolygonsMaxX()
        {
            double maxX = ConstValue.MAX_LIMMIT;
            for (int i = 0; i < 4; i++)
            {
                if (maxX < Polygons[i].X)
                {
                    maxX = Polygons[i].X;
                }
            }
            return maxX;
        }

        private double GetPolygonsMaxY()
        {
            double maxY = ConstValue.MAX_LIMMIT;
            for (int i = 0; i < 4; i++)
            {
                if (maxY < Polygons[i].Y)
                {
                    maxY = Polygons[i].Y;
                }
            }
            return maxY;
        }

        public void AddPolygons(double x, double y)
        {
            ++PointPer;
            PointPer %= 4;
            Polygons[PointPer] = new MyDefPoint(x, y);

            if (PointPer == 3)
            {
                PolygonCenter.X = (GetPolygonsMinX() + GetPolygonsMaxX()) / 2.0;
                PolygonCenter.Y = (GetPolygonsMinY() + GetPolygonsMaxY()) / 2.0;
            }
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
            PolygonCenter = new MyDefPoint();
            QuadcopterCenter = new MyDefPoint();
            QuadcopterTailCenter = new MyDefPoint();
        }
    }
}
