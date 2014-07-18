using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetectSystem
{
    public class Shape
    {
        private MyDefPoint _tempStartPoint = new MyDefPoint();

        public double Width
        {
            set;
            get;
        }

        public double Height
        {
            set;
            get;
        }

        public MyDefPoint StartPoint
        {
            set;
            get;
        }

        public MyDefPoint EndPoint
        {
            set;
            get;
        }

        //內建初始化 Shape
        public Shape()
        {
            StartPoint = new MyDefPoint();
            EndPoint = new MyDefPoint();
            Width = 0;
            Height = 0;
        }

        //根據參數初始化 Shape
        public Shape(double x, double y, double width, double height)
        {
            StartPoint = new MyDefPoint();
            EndPoint = new MyDefPoint();
            StartPoint.X = x;
            StartPoint.Y = y;
            _tempStartPoint.X = x;
            _tempStartPoint.Y = y;
            EndPoint.X = x + width;
            EndPoint.Y = y + height;
            Width = width;
            Height = height;
        }

        //設定 StartPoint
        public virtual void SetStartPoint(double x, double y)
        {
            StartPoint.X = x;
            StartPoint.Y = y;
            _tempStartPoint.X = x;
            _tempStartPoint.Y = y;
        }

        //設定 EndPoint
        public virtual void SetEndPoint(double x, double y)
        {
            if (x < _tempStartPoint.X)
            {
                StartPoint.X = x;
                x = _tempStartPoint.X;
            }
            else
            {
                StartPoint.X = _tempStartPoint.X;
            }
            if (y < _tempStartPoint.Y)
            {
                StartPoint.Y = y;
                y = _tempStartPoint.Y;
            }
            else
            {
                StartPoint.Y = _tempStartPoint.Y;
            }
            EndPoint.X = x;
            EndPoint.Y = y;
            Width = EndPoint.X - StartPoint.X;
            Height = EndPoint.Y - StartPoint.Y;
        }
    }
}
