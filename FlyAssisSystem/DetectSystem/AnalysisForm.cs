using AR.Drone.Client;
using AR.Drone.Client.Command;
using AR.Drone.Data;
using AR.Drone.Data.Navigation;
using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZXing;

namespace DetectSystem
{
    public partial class AnalysisForm : Form
    {
        private Contour<Point> _red = null;
        private Contour<Point> _green = null;
        private Capture _capture = null;
        private Image<Bgr, Byte> _input = null;
        private Image<Gray, Byte> _output = null;
        private Thread _fectchImageThread = null;
        private Thread _processImageThread = null;
        public delegate void InvokeShowImageData();
        public delegate void InvokeShowTextData(String data);

        private PresentationModel _presentationModel = new PresentationModel();

        private IBarcodeReader reader = new BarcodeReader();

        void ShowInputImage()
        {
            if (_inputPictureBox.InvokeRequired)
            {
                InvokeShowImageData showData = new InvokeShowImageData(ShowInputImage);
                this.Invoke(showData, new Object[] { });
            }
            else
            {
                _inputPictureBox.Image = _input.ToBitmap();
                Bitmap map = _inputPictureBox.Image as Bitmap;
                Graphics graphic = Graphics.FromImage(map);
                graphic.DrawLine(new Pen(Brushes.Red, 5), Convert.ToInt32(_presentationModel.TempShape.StartPoint.X), Convert.ToInt32(_presentationModel.TempShape.StartPoint.Y), Convert.ToInt32(_presentationModel.TempShape.EndPoint.X), Convert.ToInt32(_presentationModel.TempShape.EndPoint.Y));

                for (int i = 1; i <= _presentationModel.DataModel.PointPer; i++)
                {
                    graphic.DrawLine(new Pen(Brushes.Red, 5), Convert.ToInt32(_presentationModel.DataModel.Polygons[i - 1].X), Convert.ToInt32(_presentationModel.DataModel.Polygons[i - 1].Y), Convert.ToInt32(_presentationModel.DataModel.Polygons[i].X), Convert.ToInt32(_presentationModel.DataModel.Polygons[i].Y));
                }
                if (_presentationModel.DataModel.PointPer == 3)
                {
                   // graphic.DrawEllipse(new Pen(Brushes.Red, 5), Convert.ToInt32(_presentationModel.DataModel.PolygonCenter.X), Convert.ToInt32(_presentationModel.DataModel.PolygonCenter.Y), 30, 30);
                    graphic.DrawLine(new Pen(Brushes.Red, 5), Convert.ToInt32(_presentationModel.DataModel.Polygons[3].X), Convert.ToInt32(_presentationModel.DataModel.Polygons[3].Y), Convert.ToInt32(_presentationModel.DataModel.Polygons[0].X), Convert.ToInt32(_presentationModel.DataModel.Polygons[0].Y));
                }
            }
        }

        void ShowOutputImage()
        {
            if (_outputPictureBox.InvokeRequired)
            {
                InvokeShowImageData showData = new InvokeShowImageData(ShowOutputImage);
                this.Invoke(showData, new Object[] { });
            }
            else
            {
                _outputPictureBox.Image = _output.ToBitmap();
            }
        }

        void ShowData(String data)
        {
            if (_qrTextLabel.InvokeRequired)
            {
                InvokeShowTextData showData = new InvokeShowTextData(ShowData);
                this.Invoke(showData, new Object[] { data });
            }
            else
            {
                _qrTextLabel.Text = data;
            }
        }

        public void showImage(Image<Bgr, Byte> input, Image<Gray, Byte> output)
        {
            Adapter.Invoke(new SendOrPostCallback(o =>
            {
                _inputPictureBox.Image = input.ToBitmap();
                _outputPictureBox.Image = output.ToBitmap();
            }), null);
        }

        public void ReadImage()
        {
            while (true)
            {
                _input = _capture.QueryFrame();
            }
        }

        public Contour<Point> FindContours(Contour<Point> contours)
        {
            for (; contours != null; contours = contours.HNext)
            {
                if (contours.Area >= ConstValue.OBJECT_MIN_AREA)
                {
                    return contours;
                }
            }
            return null;
        }

        public void ProcessImage()
        {
            while (true)
            {
                if (_input != null)
                {
                    _output = new Image<Gray, byte>(_presentationModel.InputPictureBoxImageWidth, _presentationModel.InputPictureBoxImageHeight, new Gray(0));
                    Image<Gray, Byte> greenTemp = _input.Convert<Hsv, Byte>().InRange(new Hsv(35, 50, 50), new Hsv(80, 255, 255));
                    Image<Gray, Byte> redTemp = _input.Convert<Ycc, Byte>().InRange(new Ycc(0, 170, 0), new Ycc(110, 255, 150)); //ycrcb red
                    //Image<Gray, Byte> greenTemp = _input.Convert<Ycc, Byte>().InRange(new Ycc(0, 0, 0), new Ycc(255, 110, 130)); //ycrcb green

                    Contour<Point> redContours = redTemp.FindContours(Emgu.CV.CvEnum.CHAIN_APPROX_METHOD.CV_CHAIN_APPROX_NONE, Emgu.CV.CvEnum.RETR_TYPE.CV_RETR_EXTERNAL);
                    Contour<Point> greenContours = greenTemp.FindContours(Emgu.CV.CvEnum.CHAIN_APPROX_METHOD.CV_CHAIN_APPROX_NONE, Emgu.CV.CvEnum.RETR_TYPE.CV_RETR_EXTERNAL);
                    Contour<Point> red = FindContours(redContours);
                    Contour<Point> green = FindContours(greenContours);

                    if (red != null)
                    {
                        _red = red;
                        _output.Draw(red.BoundingRectangle, new Gray(255), 1);
                    }
                    if (green != null)
                    {
                        _green = green;
                        _output.Draw(green.BoundingRectangle, new Gray(255), 1);
                    }

                    if (_red != null && _green != null)
                    {
                        double redCenterX = (2.0 * _red.BoundingRectangle.X + _red.BoundingRectangle.Width) / 2.0;
                        double redCenterY = (2.0 * _red.BoundingRectangle.Y + _red.BoundingRectangle.Height) / 2.0;
                        double greenCenterX = (2.0 * _green.BoundingRectangle.X + _green.BoundingRectangle.Width) / 2.0;
                        double greenCenterY = (2.0 * _green.BoundingRectangle.Y + _green.BoundingRectangle.Height) / 2.0;
                        _presentationModel.SetQuadcopterCenter(greenCenterX, greenCenterY);
                        _presentationModel.SetQuadcopterTailCenter(redCenterX, redCenterY);
                        _presentationModel.DataModel.Quadcopter = _green;

                        Debug.WriteLine("QuadcopterCenter=" + greenCenterX.ToString() + "," + greenCenterY.ToString());
                        Debug.WriteLine("QuadcopterTailCenter=" + redCenterX.ToString() + "," + redCenterY.ToString());
                        Debug.WriteLine("PolygonCenter=" + _presentationModel.DataModel.PolygonCenter.X.ToString() + "," + _presentationModel.DataModel.PolygonCenter.Y.ToString());
                    }
                    /*   var result = reader.Decode(_input.ToBitmap());
                       ShowData("");
                       if (result != null)
                       {
                           ShowData(result.Text);
                       }*/

                    ShowInputImage();
                    ShowOutputImage();
                }
            }
        }

        private void ReadQR()
        {
            while (true)
            {
                if (_input != null)
                {
                    var result = reader.Decode(_input.ToBitmap());
                    ShowData("");
                    if (result != null)
                    {
                        ShowData(result.Text);
                    }
                }
            }
        }

        public AnalysisForm()
        {
            InitializeComponent();
            _presentationModel.InputPictureBoxWidth = _inputPictureBox.Width;
            _presentationModel.InputPictureBoxHeight = _inputPictureBox.Height;
        }

        private void ClickStartButton(object sender, EventArgs e)
        {
            try
            {
                Adapter.Initialize();
                _presentationModel.TempShape = new MyDefLine();
                // _capture = new Capture(0);
                _capture = new Capture("rtsp://192.168.0.250/h264");
                _presentationModel.InputPictureBoxImageWidth = _capture.Width;
                _presentationModel.InputPictureBoxImageHeight = _capture.Height;

                ThreadStart start1 = new ThreadStart(ReadImage);
                _fectchImageThread = new Thread(start1);
                _fectchImageThread.Start();

                ThreadStart start2 = new ThreadStart(ProcessImage);
                _processImageThread = new Thread(start2);
                _processImageThread.Start();
                _startButton.Enabled = false;
            }
            catch (Exception)
            {
                MessageBox.Show("連結錯誤，請確認攝影機是否已連結");
            }
        }

        private void ClickEmergencyLandButton(object sender, EventArgs e)
        {
            _presentationModel.DataModel.EmergencyLand();
        }

        private void ClickTakeOffButton(object sender, EventArgs e)
        {
            _presentationModel.DataModel.TakeOffDrone();
        }

        private void ClickAutoPilotButton(object sender, EventArgs e)
        {
            _presentationModel.DataModel.ControlFlag = !_presentationModel.DataModel.ControlFlag;
            if (_presentationModel.DataModel.ControlFlag == false)
            {
                _presentationModel.DataModel.LandDrone();
                _autoPilotButton.Text = "Auto Pilot";
            }
            else
            {
                _autoPilotButton.Text = "Disable Auto Pilot";
            }
        }

        private void ClosingAnalysisForm(object sender, FormClosingEventArgs e)
        {
            if (_fectchImageThread != null)
            {
                _fectchImageThread.Abort();
            }

            if (_processImageThread != null)
            {
                _processImageThread.Abort();
            }
            _presentationModel.DataModel.DisposeThread();
            _presentationModel.DataModel.DisposeDroneClient();
        }

        private void MouseDownInputBox(object sender, MouseEventArgs e)
        {
            if (_inputPictureBox.Image != null)
            {
                _presentationModel.MouseDownInputBox(e.Location);
                _table1AreaLabel.Text = _presentationModel.CalSelectRangeArea();
            }
        }

        private void MouseMoveInputBox(object sender, MouseEventArgs e)
        {
            if (_inputPictureBox.Image != null)
            {
                _presentationModel.MouseMoveInputBox(e.Location);
            }
        }

        private void MouseUpInputBox(object sender, MouseEventArgs e)
        {
            if (_inputPictureBox.Image != null)
            {
                _presentationModel.MouseUpInputBox(e.Location);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //_droneClient.Takeoff();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            //  _droneClient.Start();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            //   _droneClient.Stop();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            //   _droneClient.Land();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //  _droneClient.Progress(FlightMode.Progressive, pitch: -0.05f);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //  _droneClient.Progress(FlightMode.Progressive, pitch: 0.05f);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //  _droneClient.Progress(FlightMode.Progressive, roll: -0.05f);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //   _droneClient.Progress(FlightMode.Progressive, roll: 0.05f);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            //    _droneClient.Progress(FlightMode.Progressive, yaw: 0.25f);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            //    _droneClient.Progress(FlightMode.Progressive, yaw: -0.25f);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            //     _droneClient.Emergency();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //    _droneClient.Progress(FlightMode.Progressive, gaz: 0.25f);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //   _droneClient.Progress(FlightMode.Progressive, gaz: -0.25f);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //    _droneClient.Hover();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            //     _droneClient.ResetEmergency();
        }

        private void button10_Click_1(object sender, EventArgs e)
        {
            _presentationModel.DataModel.RotateRight();
        }
    }
}
