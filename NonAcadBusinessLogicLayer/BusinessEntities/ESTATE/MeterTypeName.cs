using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IITMS.UAIMS.BusinessLayer.BusinessEntities
{

    public class MeterTypeName
    { 
        private int _mtype            = 0;
        private int _meterTypeNo      = 0;
        private string _meterTypeName = string.Empty;

        public int MeterTypeNo
        {
            get { return _meterTypeNo;  }
            set { _meterTypeNo = value; }
        }
        public string MeterTypeName1
        {
            get { return _meterTypeName;  }
            set { _meterTypeName = value; }
        }
        public int Mtype
        {
           get { return _mtype; }
           set { _mtype = value; }
        }

        
    }
}
