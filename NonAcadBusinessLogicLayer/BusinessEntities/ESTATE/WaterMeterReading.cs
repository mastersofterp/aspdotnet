using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IITMS.UAIMS.BusinessLogicLayer.BusinessEntities
{
    public class WaterMeterReading
    {
        #region Private Member
        private int _checkIDNO = 0;
        private string _IDNO = string.Empty;
        private string _nameId = string.Empty;
        private string _name = string.Empty;
        private string _qtrNo = string.Empty;
        private string _meterNo = string.Empty;
        private string _oldReading = string.Empty;
        private string _currentReading = string.Empty;
        private string _adjUnit = string.Empty;
        private string _total = string.Empty;
        private DateTime _monthreading = DateTime.MinValue;
        private string _conStatus = string.Empty;
        #endregion

        #region Public Member


        public int CheckIDNO
        {
            get { return _checkIDNO; }
            set { _checkIDNO = value; }
        }
        public string IDNO
        {
            get { return _IDNO; }
            set { _IDNO = value; }
        }
        public string NameId
        {
            get { return _nameId; }
            set { _nameId = value; }
        }


        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }


        public string QtrNo
        {
            get { return _qtrNo; }
            set { _qtrNo = value; }
        }


        public string MeterNo
        {
            get { return _meterNo; }
            set { _meterNo = value; }
        }


        public string OldReading
        {
            get { return _oldReading; }
            set { _oldReading = value; }
        }


        public string CurrentReading
        {
            get { return _currentReading; }
            set { _currentReading = value; }
        }


        public string AdjUnit
        {
            get { return _adjUnit; }
            set { _adjUnit = value; }
        }


        public string Total
        {
            get { return _total; }
            set { _total = value; }
        }


        public string ConStatus
        {
            get { return _conStatus; }
            set { _conStatus = value; }
        }


        public DateTime Monthreading
        {
            get { return _monthreading; }
            set { _monthreading = value; }
        }

  
        #endregion 
    }
}
