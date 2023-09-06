using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Web;



namespace IITMS.UAIMS.BusinessLayer.BusinessEntities
{
   public class Disciplinary_Action
    {
        #region private
        private int _EID = 0;
        private System.Nullable<System.DateTime> _EDATE;
        private string _ETITLE = string.Empty.Trim();
        private int _ECAT = 0;
        private string _EDESC= string.Empty;
        private int _STUDID = 0;
        private int _STUDNAME = 0;
        private int _BRANCHNO = 0;
        private string _PUNISH = string.Empty.Trim();
        private string _AUTHO = string.Empty.Trim();
        private int _UNO = 0;
        private System.Nullable<System.DateTime> _ENTRYDATE;
        private int _CCODE = 0;
        private int _SEMNO = 0;
        #endregion

        #region public
        public int EID
        {
            get { return _EID; }
            set { _EID = value; }
        }
        public System.Nullable<System.DateTime> EDATE
        {
            get { return _EDATE; }
            set { _EDATE = value; }
        }

        public string ETITLE
        {
            get { return _ETITLE; }
            set { _ETITLE = value; }
        }
        public int ECAT
        {
            get { return _ECAT; }
            set { _ECAT = value; }
        }
        public string EDESC
        {
            get { return _EDESC; }
            set { _EDESC = value; }
        }
        public int STUDID
        {
            get { return _STUDID; }
            set { _STUDID = value; }
        }

        public int STUDNAME
        {
            get { return _STUDNAME; }
            set { _STUDNAME = value; }
        }
        public int BRANCHNO
        {
            get { return _BRANCHNO; }
            set { _BRANCHNO = value; }
        }
        public string PUNISH
        {
            get { return _PUNISH; }
            set { _PUNISH = value; }
        }
        public string AUTHO
        {
            get { return _AUTHO; }
            set { _AUTHO = value; }
        }
        public int UNO
        {
            get { return _UNO; }
            set { _UNO = value; }
        }
        public System.Nullable<System.DateTime> ENTRYDATE
        {
            get { return _ENTRYDATE; }
            set { _ENTRYDATE = value; }
        }
        public int CCODE
        {
            get { return _CCODE; }
            set { _CCODE = value; }
        }
        public int SEMNO
        {
            get { return _SEMNO; }
            set { _SEMNO = value; }
        }
        #endregion

    }
}
        
