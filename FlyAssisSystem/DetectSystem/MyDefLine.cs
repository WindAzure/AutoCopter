using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetectSystem
{
    public class MyDefLine : Shape
    {
        //設定 EndPoint，並更新 Width 和 Height
        public override void SetEndPoint(double x, double y)
        {
            EndPoint.X = x;
            EndPoint.Y = y;
            Width = Math.Abs(EndPoint.X - StartPoint.X);
            Height = Math.Abs(EndPoint.Y - StartPoint.Y);
        }

        //根據參數初始化線
        public MyDefLine(double x1, double y1, double x2, double y2)
        {
            base.SetStartPoint(x1, y1);
            SetEndPoint(x2, y2);
        }

        //內建初始化線
        public MyDefLine()
            : base()
        {
        }
    }
}
