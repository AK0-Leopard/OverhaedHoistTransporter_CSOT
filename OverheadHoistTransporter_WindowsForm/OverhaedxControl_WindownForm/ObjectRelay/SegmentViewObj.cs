using com.mirle.ibg3k0.bcf.Common;
using com.mirle.ibg3k0.sc;
using com.mirle.ibg3k0.sc.ProtocolFormat.OHTMessage;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.mirle.ibg3k0.ohxc.winform.ObjectRelay
{
    public class SegmentViewObj : INotifyPropertyChanged
    {
        ASEGMENT segment = null;
        public SegmentViewObj(ASEGMENT myDatabaseObject)
        {
            this.segment = myDatabaseObject;
        }

        public string CVID
        {
            get
            {
                string cv_id = "";
                if (!string.IsNullOrWhiteSpace(segment.CVID))
                {
                    string[] cv_id_info = segment.CVID.Split('_');
                    if (cv_id_info.Count() > 0)
                    {
                        cv_id = cv_id_info[0];
                    }
                }
                return cv_id;
            }
        }
        public bool DISABLE_FLAG_SYSTEM
        {
            get { return segment.DISABLE_FLAG_SYSTEM; }
        }
        public bool DISABLE_FLAG_HID
        {
            get { return segment.DISABLE_FLAG_HID; }
        }
        public bool DISABLE_FLAG_SAFETY
        {
            get { return segment.DISABLE_FLAG_SAFETY; }
        }
        public bool DISABLE_FLAG_USER
        {
            get { return segment.DISABLE_FLAG_USER; }
        }
        public DateTime? DISABLE_TIME
        {
            get { return segment.DISABLE_TIME; }
        }
        public DateTime? PRE_DISABLE_TIME
        {
            get { return segment.PRE_DISABLE_TIME; }
        }
        public bool PRE_DISABLE_FLAG
        {
            get { return segment.PRE_DISABLE_FLAG; }
        }
        public E_RAIL_DIR DIR
        {
            get { return segment.DIR; }
        }
        public string NOTE
        {
            get { return segment.NOTE; }
        }

        public string RESERVE_FIELD
        {
            get { return segment.RESERVE_FIELD; }
        }
        public int SEG_TYPE
        {
            get { return segment.SEG_TYPE; }
        }
        public int? SPECIAL_MARK
        {
            get { return segment.SPECIAL_MARK; }
        }
        public string SEG_NUM
        {
            get { return segment.SEG_NUM; }
        }
        public E_SEG_STATUS STATUS
        {
            get { return segment.STATUS; }
        }

        //This is to notify the changes made to the object directly and not from the control. This refreshes the datagridview.
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(String propertyName)
        {
            try
            {
                if (PropertyChanged != null)
                {
                    Adapter.Invoke((obj) =>
                    {
                        {
                            PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
                        }
                    }, null);

                }
            }
            catch
            {

            }
        }
    }
}
