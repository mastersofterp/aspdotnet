using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using IITMS;

namespace IITMS.UAIMS.BusinessLayer.BusinessEntities
{
    public class Masterial_Master
    {
        #region Private Member
        private int _materialNo = 0;
        private string _materialName = string.Empty;
    
        #endregion 
        #region Private Properties filed
        public int MaterialNo
        {
            get { return _materialNo; }
            set { _materialNo = value; }
        }

        public string MaterialName
        {
            get { return _materialName; }
            set { _materialName = value; }
        }
        #endregion 
    }
}
