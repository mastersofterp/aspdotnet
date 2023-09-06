//======================================================================================
// PROJECT NAME  : UAIMS
// MODULE NAME   : HOSTEL
// PAGE NAME     : BONAFIDE CERTIFICATE CLASS
// CREATION DATE : 17-AUG-2009
// CREATED BY    : SANJAY RATNAPARKHI
// MODIFIED DATE :
// MODIFIED DESC :
//======================================================================================

using System;
using System.Collections;

namespace IITMS.UAIMS.BusinessLayer.BusinessEntities
{
    public class Bonafide
    {
        #region Private Member
            private int _bcno = 0;
            private int _idno = 0;
            private int _room_No = 0;
            private int _adm_Batch = 0;
            private DateTime _issue_Date = DateTime.MinValue;
            private string _issuer_Name = string.Empty;
            private int _session_No = 0;
            private string _certificate_No = string.Empty;

            
            private string _college_Code = string.Empty;
        #endregion

        #region Private Properties
            public int Bcno
       {
           get { return _bcno; }
           set { _bcno = value; }
       }
            public int Idno
       {
           get { return _idno; }
           set { _idno = value; }
       }
            public int Room_No
       {
           get { return _room_No; }
           set { _room_No = value; }
       }
            public int Adm_Batch
       {
           get { return _adm_Batch; }
           set { _adm_Batch = value; }
       }
            public DateTime Issue_Date
       {
           get { return _issue_Date; }
           set { _issue_Date = value; }
       }
            public string Issuer_Name
       {
           get { return _issuer_Name; }
           set { _issuer_Name = value; }
       }
            public int Session_No
       {
            get { return _session_No; }
            set { _session_No = value; }
       }
            public string Certificate_No
       {
                get { return _certificate_No; }
                set { _certificate_No = value; }
       }
            public string College_Code
       {
              get { return _college_Code; }
              set { _college_Code = value; }
       }

        #endregion
    }
}
