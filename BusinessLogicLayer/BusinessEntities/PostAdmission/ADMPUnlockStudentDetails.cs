using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IITMS.UAIMS.BusinessLayer.BusinessEntities
{
    public class ADMPUnlockStudentDetails
    {
        #region Private Member
        private string userno = string.Empty;
        private string allowprocess = string.Empty;
        private string applicationid = string.Empty;
        private string firstname = string.Empty;
        private DateTime startdate;
        private DateTime enddate;  
        private int degreeno = 0;
        private int branchno = 0;
        private int batchno = 0;
        private int ugpgot = 0;
        private int uano = 0;
        private string starttime = string.Empty;
        private string endtime = string.Empty;
        private char allowprocessfrom;
        private int college_id = 0;
        #endregion

        #region Public Member

        public string Userno
        {
            get { return userno; }
            set { userno = value; }
        }
        public string AllowProcess
        {
            get { return allowprocess; }
            set { allowprocess = value; }
        }
        public string ApplicationId
        {
            get { return applicationid; }
            set { applicationid = value; }
        }
        public string FirstName
        {
            get { return firstname; }
            set { firstname = value; }
        }
        public DateTime StartDate
        {
            get { return startdate; }
            set { startdate = value; }
        }
        public DateTime EndDate
        {
            get { return enddate; }
            set { enddate = value; }
        }
        public int BatchNo
        {
            get { return batchno; }
            set { batchno = value; }
        }
        public int DegreeNo
        {
            get { return degreeno; }
            set { degreeno = value; }
        }
        public int BranchNo
        {
            get { return branchno; }
            set { branchno = value; }
        }
        public int UGPGOT
        {
            get { return ugpgot; }
            set { ugpgot = value; }
        }
        public int UaNo
        {
            get { return uano; }
            set { uano = value; }
        }
        public string StartTime
        {
            get { return starttime; }
            set { starttime = value; }
        }
        public string EndTime
        {
            get { return endtime; }
            set { endtime = value; }
        }

        public char AllowProcessFrom
        {
            get { return allowprocessfrom; }
            set { allowprocessfrom = value; }
        }
        public int CollegeId
        {
            get { return college_id; }
            set { college_id = value; }
        }


        #endregion
    }
}
