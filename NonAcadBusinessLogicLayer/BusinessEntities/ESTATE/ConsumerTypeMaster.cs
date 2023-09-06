using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using IITMS;


namespace IITMS.UAIMS.BusinessLogicLayer.BusinessEntities
{
    public class ConsumerTypeMaster
    {
        #region Private Member
        private int _consumerTypeNo = 0;
        private string _consumerType = string.Empty;

        #endregion
        #region Private Properties filed
        public int ConsumerTypeNo
        {
            get { return _consumerTypeNo; }
            set { _consumerTypeNo = value; }
        }

        public string ConsumerType
        {
            get { return _consumerType; }
            set { _consumerType = value; }
        }
        #endregion 
    }
}
