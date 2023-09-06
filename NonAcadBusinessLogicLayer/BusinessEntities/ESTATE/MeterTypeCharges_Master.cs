using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IITMS;


namespace IITMS.UAIMS.BusinessLogicLayer.BusinessEntities
{
    public class MeterTypeCharges_Master
    {
        #region Private Member
        private int _mCharges = 0;
        private int _mtypeNo = 0;
        private decimal _meterAvgUnit = 0;
        private decimal _meterLockUnit = 0;
        private decimal _ElictricDuty = 0;
        private decimal _FcaCharges = 0;
        private decimal _meterRent = 0;
        private decimal _meterAccCharges = 0;
        private decimal _FixedCharges = 0;
        #endregion




        #region Private Properties filed

        public int MCharges
        {
            get { return _mCharges; }
            set { _mCharges = value; }
        }
        
               
        public int MtypeNo
        {
            get { return _mtypeNo; }
            set { _mtypeNo = value; }
        }


        public decimal FixedCharges
        {
            get { return _FixedCharges; }
            set { _FixedCharges = value; }
        }

        

        public decimal MeterAccCharges
        {
            get { return _meterAccCharges; }
            set { _meterAccCharges = value; }
        }
       

        public decimal MeterRent
        {
            get { return _meterRent; }
            set { _meterRent = value; }
        }
       

        public decimal FcaCharges
        {
            get { return _FcaCharges; }
            set { _FcaCharges = value; }
        }
        

        public decimal ElictricDuty
        {
            get { return _ElictricDuty; }
            set { _ElictricDuty = value; }
        }
    

        public decimal MeterLockUnit
        {
            get { return _meterLockUnit; }
            set { _meterLockUnit = value; }
        }
      

        public decimal MeterAvgUnit
        {
            get { return _meterAvgUnit; }
            set { _meterAvgUnit = value; }
        }

        #endregion
    }
}
