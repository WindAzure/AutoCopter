using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetectSystem
{
    public class Model
    {
        public int PolygonPer
        {
            set;
            get;
        }

        public Shape[] Polygons
        {
            set;
            get;
        }

        public Model()
        {
            PolygonPer = -1;
        }

        public void AddPolygons(Shape polygon)
        {
            Shape shape = new MyDefLine();
            shape.SetStartPoint(polygon.StartPoint.X, polygon.StartPoint.Y);
            shape.SetEndPoint(polygon.EndPoint.X, polygon.EndPoint.Y);
            Polygons[++PolygonPer] = shape;
        }

        public void ClearPolygons()
        {
            PolygonPer = -1;
        }
    }
}
