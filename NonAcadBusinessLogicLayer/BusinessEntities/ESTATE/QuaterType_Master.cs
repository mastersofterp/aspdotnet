using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using IITMS;

namespace IITMS.UAIMS.BusinessLayer.BusinessEntities
{
    public class QuaterType_Master
    {
        #region Private Member

      
        private int _qrtTypeNo =0;
        private string _qrtType =string.Empty;
        private int _rent=0;
        private string _qArea = string.Empty;
        private int _eStaff = 0;
        private double _fixedCharge = 0.0;

       
        #endregion 

        #region Private Properties filed

        public int eStaff
        {
            get { return _eStaff; }
            set { _eStaff = value; }
        }
       
       
        public int QrtTypeNo
        {
            get { return _qrtTypeNo; }
            set { _qrtTypeNo = value; }
        }

        public string QrtType
        {
            get { return _qrtType; }
            set { _qrtType = value; }
        }

        public int Rent
        {
            get { return _rent; }
            set { _rent = value; }
        }

        public string QArea
        {
            get { return _qArea; }
            set { _qArea = value; }
        }

        public double fixedCharge
        {
            get { return _fixedCharge; }
            set { _fixedCharge = value; }
        }
       
        #endregion 
    }
}
