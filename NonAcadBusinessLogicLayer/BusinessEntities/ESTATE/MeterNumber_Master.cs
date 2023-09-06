using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IITMS.UAIMS.BusinessLogicLayer.BusinessEntities
{
    public class MeterNumber_Master
    {
        #region Private Member
        private string _MeterType;
        private int _mType;
        private int _MeterTypeNo;
        private string _MeterNo;
        private int _Rent;
        private int _EmeterMultiple;
        #endregion

        #region Private Properties filed



        public int MType
        {
            get { return _mType; }
            set { _mType = value; }
        }
        public int MeterTypeNo
        {
            get { return _MeterTypeNo; }
            set { _MeterTypeNo = value; }
        }

        public string MeterType
        {
            get { return _MeterType; }
            set { _MeterType = value; }
        }

        public string MeterNo
        {
            get { return _MeterNo; }
            set { _MeterNo = value; }
        }

        public int Rent
        {
            get { return _Rent; }
            set { _Rent = value; }
        }

        public int EmeterMultiple
        {
            get { return _EmeterMultiple; }
            set { _EmeterMultiple = value; }
        }
        #endregion
    }
}
