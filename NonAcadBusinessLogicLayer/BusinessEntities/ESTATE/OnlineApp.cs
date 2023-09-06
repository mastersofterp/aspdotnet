using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using IITMS;

namespace IITMS.UAIMS.BusinessLayer.BusinessEntities
{
   public class OnlineApp
    {

        #region Private Member
        private int _APPID = 0;
        private int _EMPID = 0;
        private int _TOTAL_MEMBERS = 0;
        private string _REMARK = string.Empty;
        private int _STATUS = 0;
        private int _QRT_TYPE = 0;
        private DateTime _APPLICATION_DATE; 

        #endregion

        #region Private Properties filed
        public int APPID
        {
            get { return _APPID; }
            set { _APPID = value; }
        }       
        public int EMPID
        {
            get { return _EMPID; }
            set { _EMPID = value; }
        }

        public int TOTAL_MEMBERS
        {
            get { return _TOTAL_MEMBERS; }
            set { _TOTAL_MEMBERS = value; }
        }
        public string REMARK
        {
            get { return _REMARK; }
            set { _REMARK = value; }
        }
        public int STATUS
        {
            get { return _STATUS; }
            set { _STATUS = value; }
        }
        public int QRT_TYPE
        {
            get { return _QRT_TYPE; }
            set { _QRT_TYPE = value; }
        }
        public DateTime APPLICATION_DATE
        {
            get { return _APPLICATION_DATE; }
            set { _APPLICATION_DATE = value; }
        }
       
        #endregion 
    }
}
