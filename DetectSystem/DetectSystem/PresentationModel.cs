using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetectSystem
{
    public class PresentationModel
    {
        private bool _isPress = false;
        public delegate void PresentationModelChanged();
        public event PresentationModelChanged _presentationModelChanged = null;

        public bool Table1StateChangeButtonEnable
        {
            set;
            get;
        }

        public bool Table2StateChangeButtonEnable
        {
            set;
            get;
        }

        public bool Table3StateChangeButtonEnable
        {
            set;
            get;
        }

        public bool Table4StateChangeButtonEnable
        {
            set;
            get;
        }

        public bool IsTable1StateChanged
        {
            set;
            get;
        }

        public bool IsTable2StateChanged
        {
            set;
            get;
        }

        public bool IsTable3StateChanged
        {
            set;
            get;
        }

        public bool IsTable4StateChanged
        {
            set;
            get;
        }

        public int Index
        {
            set;
            get;
        }

        public Shape TempShape
        {
            set;
            get;
        }

        public int InputPictureBoxWidth
        {
            set;
            get;
        }

        public int InputPictureBoxHeight
        {
            set;
            get;
        }

        public int InputPictureBoxImageWidth
        {
            set;
            get;
        }

        public int InputPictureBoxImageHeight
        {
            set;
            get;
        }

        public void InitializeStateButton()
        {
            Table1StateChangeButtonEnable = true;
            Table2StateChangeButtonEnable = true;
            Table3StateChangeButtonEnable = true;
            Table4StateChangeButtonEnable = true;
            NotifyStateButtonChanged();
        }

        public PresentationModel()
        {
            IsTable1StateChanged = false;
            IsTable2StateChanged = false;
            IsTable3StateChanged = false;
            IsTable4StateChanged = false;
            Index = -1;
        }

        private void NotifyStateButtonChanged()
        {
            if (_presentationModelChanged != null)
            {
                _presentationModelChanged();
            }
        }


        public Point ConvertRatioOfPictureBoxAndImage(Point point)
        {
            double ratioX = Convert.ToDouble(InputPictureBoxImageWidth) / Convert.ToDouble(InputPictureBoxWidth);
            double ratioY = Convert.ToDouble(InputPictureBoxImageHeight) / Convert.ToDouble(InputPictureBoxHeight);
            return new Point(Convert.ToInt32(point.X * ratioX), Convert.ToInt32(point.Y * ratioY));
        }

        public void MouseDownInputBox(Point movePoint)
        {
            _isPress = true;
            Point point = ConvertRatioOfPictureBoxAndImage(movePoint);
            TempShape.SetStartPoint(point.X, point.Y);
            TempShape.SetEndPoint(point.X, point.Y);
        }

        public void MouseMoveInputBox(Point movePoint)
        {
            if (_isPress)
            {
                Point point = ConvertRatioOfPictureBoxAndImage(movePoint);
                TempShape.SetEndPoint(point.X, point.Y);
            }
        }

        public void MouseUpInputBox(Point movePoint)
        {
            if (movePoint.X < 0 || movePoint.X > InputPictureBoxWidth || movePoint.Y < 0 || movePoint.Y > InputPictureBoxHeight)
            {
                _isPress = false;
                TempShape = new MyDefLine();
            }

            if (_isPress)
            {
                Point point = ConvertRatioOfPictureBoxAndImage(movePoint);
                TempShape.SetEndPoint(point.X, point.Y);
                _isPress = false;
            }
        }
    }
}
