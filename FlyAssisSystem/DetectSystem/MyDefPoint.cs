using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetectSystem
{
    public class MyDefPoint
    {
        public double X
        {
            set;
            get;
        }

        public double Y
        {
            set;
            get;
        }

        //根據參數初始化點
        public MyDefPoint(double x, double y)
        {
            X = x;
            Y = y;
        }

        //內建初始化點
        public MyDefPoint()
        {
            X = 0;
            Y = 0;
        }
    }
}
