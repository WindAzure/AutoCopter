using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetectSystem
{
    public class PresentationModel
    {
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

    }
}
