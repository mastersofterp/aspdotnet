using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IITMS;

namespace IITMS.UAIMS.BusinessLogicLayer.BusinessEntities
{
    public class Quater_Master
    {

        #region Private Member
        private int _qId = 0;
        private string _quater = string.Empty;
        private int _qrtType = 0;
        private string _qrtName = string.Empty;
        //private int _collegeCode = 0;
        private string  _qrtNo = string.Empty;
        private int _meterNumber = 0;
        private int _waterNumber = 0;
        private int _block = 0;
        private int _ConnectedLoad = 0;

        #endregion

        #region public Members
        public int ConnectedLoad
        {
            get { return _ConnectedLoad; }
            set { _ConnectedLoad = value; }
        }
        public int block
        {
            get { return _block; }
            set { _block = value; }
        }

        public int QId
        {
            get { return _qId; }
            set { _qId = value; }
        }

        public string Quater
        {
            get { return _quater; }
            set { _quater = value; }
        }
        

        public int QrtType
        {
            get { return _qrtType; }
            set { _qrtType = value; }
        }
        //public int CollegeCode
        //{
        //    get { return _collegeCode; }
        //    set { _collegeCode = value; }
        //}

        public string QrtName
        {
            get { return _qrtName; }
            set { _qrtName= value;}

        }
        public string  QrtNo
        {
            get { return _qrtNo; }
            set { _qrtNo = value; }
        }
        

        public int MeterNumber
        {
            get { return _meterNumber; }
            set { _meterNumber = value; }
        }


        public int WaterNumber
        {
            get { return _waterNumber; }
            set { _waterNumber = value; }
        }

        #endregion


    }
}
