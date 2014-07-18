using AR.Drone.Client;
using AR.Drone.Client.Command;
using AR.Drone.Data;
using AR.Drone.Data.Navigation;
using Emgu.CV;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DetectSystem
{
    public class Model
    {
        private Thread _planeMotionThread = null;
        private readonly DroneClient _droneClient;

        public bool ControlFlag
        {
            set;
            get;
        }

        public Contour<Point> Quadcopter
        {
            set;
            get;
        }
        
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
            if (PointPer < 3)
            {
                return 0;
            }
            return GetTriangleValue(Polygons[0], Polygons[1], Polygons[2]) + GetTriangleValue(Polygons[2], Polygons[3], Polygons[0]);
        }

        public void DisposeDroneClient()
        {
            _droneClient.Stop();
            _droneClient.Dispose();
        }

        private bool IsOverlapping(Rectangle A, Rectangle B)
        {
            if (A.Left <= B.Left && B.Left <= A.Right && A.Top <= B.Top && B.Top <= A.Bottom)
            {
                return true;
            }
            else if (A.Left <= B.Left && B.Left <= A.Right && B.Top <= A.Top && A.Top <= B.Bottom)
            {
                return true;
            }
            else if (B.Left <= A.Left && A.Left <= B.Right && A.Top <= B.Top && B.Top <= A.Bottom)
            {
                return true;
            }
            else if (B.Left <= A.Left && A.Left <= B.Right && B.Top <= A.Top && A.Top <= B.Bottom)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool IsAreaSame(Contour<Point> quadcopter)
        {
            return Math.Abs(quadcopter.Area - CalcaluateArea()) <= ConstValue.OBJECT_TOLERANCE_AREA;
        }

        private bool IsAreaCover(Contour<Point> quadcopter)
        {
            Rectangle rect = new Rectangle(Convert.ToInt32(GetPolygonsMinX()), Convert.ToInt32(GetPolygonsMinY()), Convert.ToInt32(GetPolygonsMaxX() - GetPolygonsMinX()), Convert.ToInt32(GetPolygonsMaxY() - GetPolygonsMinY()));
            return IsOverlapping(rect, quadcopter.BoundingRectangle) || IsOverlapping(quadcopter.BoundingRectangle, rect);
        }

        public int GetDirection()
        {
            int direct;
            if (QuadcopterCenter.X == QuadcopterTailCenter.X)
            {
                if (PolygonCenter.X > QuadcopterCenter.X)
                {
                    direct = 1;
                }
                else if (PolygonCenter.X < QuadcopterCenter.X)
                {
                    direct = -1;
                }
                else
                {
                    direct = 0;
                }
            }
            else
            {
                double value = -PolygonCenter.Y + ((QuadcopterTailCenter.Y - QuadcopterCenter.Y) / (QuadcopterTailCenter.X - QuadcopterCenter.X) * PolygonCenter.X) + ((QuadcopterCenter.X * QuadcopterTailCenter.Y - QuadcopterCenter.Y * QuadcopterTailCenter.X) / (QuadcopterCenter.X - QuadcopterTailCenter.X));
                if (value > 0.0)
                {
                    direct = 1;
                }
                else if (value < 0.0)
                {
                    direct = -1;
                }
                else
                {
                    direct = 0;
                }
            }
            return direct;
        }

        public void PlaneMotion()
        {
            while (true)
            {
                if (Quadcopter != null && ControlFlag)
                {
                    double minX = Math.Min(QuadcopterCenter.X, QuadcopterTailCenter.X);
                    double maxX = Math.Max(QuadcopterCenter.X, QuadcopterTailCenter.X);

                    if (((minX - ConstValue.OBJECT_TOLERANCE_ANGLE) <= PolygonCenter.X) && (PolygonCenter.X <= (maxX + ConstValue.OBJECT_TOLERANCE_ANGLE)))
                    {
                        if (IsAreaCover(Quadcopter) || ((QuadcopterCenter.Y+100)<PolygonCenter.Y))
                        {
                            if (_droneClient.NavigationData.Altitude > 0.30)
                            {
                                _droneClient.Progress(FlightMode.Progressive, gaz: -0.25f);
                            }
                            else
                            {
                                _droneClient.Land();
                                break;
                            }
                            Debug.WriteLine("0");
                            Thread.Sleep(100);
                        }
                        else
                        {
                            _droneClient.Progress(FlightMode.Progressive, pitch: -0.1f);
                            Debug.WriteLine("+0");
                            Thread.Sleep(100);
                        }
                    }
                    else
                    {
                        if (PolygonCenter.X < QuadcopterCenter.X)
                        {
                            _droneClient.Progress(FlightMode.Progressive, roll: -0.05f);
                            Debug.WriteLine("-1");
                        }
                        else
                        {
                            _droneClient.Progress(FlightMode.Progressive, roll: 0.05f);
                            Debug.WriteLine("1");
                        }
                        Thread.Sleep(100);
                    }
                }
                else
                    Thread.Sleep(10);
            }
        }

        public void TakeOffDrone()
        {
            _droneClient.Takeoff();
            _droneClient.Hover();
        }

        public void LandDrone()
        {
            _droneClient.Land();
        }

        public void EmergencyLand()
        {
            _droneClient.Emergency();
        }

        public void RotateRight()
        {
            _droneClient.Progress(FlightMode.Progressive, yaw: -0.25f);
        }

        public Model()
        {
            PointPer = -1;
            ControlFlag = false;
            Polygons = new MyDefPoint[4];
            PolygonCenter = new MyDefPoint();
            Quadcopter = null;
            QuadcopterCenter = new MyDefPoint();
            QuadcopterTailCenter = new MyDefPoint();
            _droneClient = new DroneClient("192.168.1.1");
            _droneClient.Start();

            ThreadStart start3 = new ThreadStart(PlaneMotion);
            _planeMotionThread = new Thread(start3);
            _planeMotionThread.Start();
        }

        public void DisposeThread()
        {
            if (_planeMotionThread != null)
            {
                _planeMotionThread.Abort();
            }
        }
    }
}
