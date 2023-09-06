using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web;
namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {
            public class MeetingScheduleMaster
            {
                #region Meeting Master
                private int _MEETING_NO = 0;
                private string _MEETING_NAME = string.Empty;
                private int _STATUS = 0;
                private int _ua_userno = 0;
                private string _description =string.Empty;
                private int Meetingtimedate_id = 0;
                private int coommiteeid =0;                   
                private int dec_id =0;


                private string _AGENDA_CONTAIN = string.Empty;
                public int MEETING_NO
                {
                    get { return _MEETING_NO; }
                    set { _MEETING_NO = value; }
                }
                public string MEETING_NAME
                {
                    get { return _MEETING_NAME; }
                    set { _MEETING_NAME = value; }
                }
                public int STATUS
                {
                    get { return _STATUS; }
                    set { _STATUS = value; }
                }

                public int UA_Userno
                {
                    get { return _ua_userno; }
                    set { _ua_userno = value; }
                }

               
                public string AGENDA_CONTAIN
                {
                    get { return _AGENDA_CONTAIN; }
                    set { _AGENDA_CONTAIN = value; }
                }

                public int DEC_ID
                {
                    get { return dec_id; }
                    set { dec_id = value; }
                }


                public int MEETINGTIMEDATE_ID
                {
                    get { return Meetingtimedate_id; }
                    set { Meetingtimedate_id = value; }
                }

                public int COMMITEEID
                {
                    get { return coommiteeid; }
                    set { coommiteeid = value; }
                }
                public string DESCRIPTION
                {
                    get { return _description; }
                    set { _description = value; }
                }

           

                #endregion

                #region  COMMITEE Private Members
                /// <summary>
                /// MODIFIED BY   : MRUNAL SINGH
                /// MODIFIED DATE : 16-FEB-2015
                /// DESCRIPTION  : ADD DEPTNO
                /// </summary>
                private int _ID;//primary key of comitee
                private string _Name;
                private string _CODE;
                private int _DEPTNO;
                private int _VENUEID;
                private int _COLLEGE_NO = 0;

                #endregion

                #region Agenda Privatemember

                private int _PK_AGENDA_ID;
                private int _FK_MEETING_ID;
                private DateTime _MEETING_DATE;
                private string _MEETING_TIME;
                private string _MEETING_TO_TIME;
                private string _AGENDA_NO;
                private string _TITLE;
                private string _FILEPATH;
                private string _FILE_NAME;
                private int _USERID;
                private DateTime _AUDIT_DATE;
                private string _VENUE;
                private char _LOCK;
                private char _TABLE_ITEM;

                private string _ADDLINE2 = string.Empty;
                private int _CITY = 0;
                private int _STATE = 0;
                private int _ZIPCODE = 0;
                private int _COUNTRY = 0;
                private DateTime _LAST_DATE;

                private int _DAID;

                #endregion


                #region Commitee Member
                private int _MEMBER_NO;
                private int _COMMITEE_NO;
                private char _ACTIVE;
                private DateTime _STARTDATE;
                private DateTime _ENDDATE;
                private int _DESIGNATION_ID;
                private char _COMMITTEE_TYPE;

                #endregion


                #region  Designation Private Members
                /// <summary>
                /// CREATED BY   : MRUNAL SINGH
                /// CREATED DATE : 29-DEC-2014
                /// DESCRIPTION  : FOR DESIGNATION MASTER
                /// </summary>
                private int _PK_COMMITEEDES;
                private string _DESIGNAME;

                #endregion



                #region AGENDApublic member

                public char LOCK
                {
                    get { return _LOCK; }
                    set { _LOCK = value; }
                }
                public char TABLE_ITEM
                {
                    get { return _TABLE_ITEM; }
                    set { _TABLE_ITEM = value; }
                }
                public int PK_AGENDA_ID
                {
                    get { return _PK_AGENDA_ID; }
                    set { _PK_AGENDA_ID = value; }
                }
                public int FK_MEETING_ID
                {
                    get { return _FK_MEETING_ID; }
                    set { _FK_MEETING_ID = value; }
                }
                public string AGENDA_NO
                {
                    get { return _AGENDA_NO; }
                    set { _AGENDA_NO = value; }
                }
                public int USERID
                {
                    get { return _USERID; }
                    set { _USERID = value; }
                }
                public string MEETING_TIME
                {
                    get { return _MEETING_TIME; }
                    set { _MEETING_TIME = value; }
                }
                public string MEETING_TO_TIME
                {
                    get { return _MEETING_TO_TIME; }
                    set { _MEETING_TO_TIME = value; }
                }
                public string TITLE
                {
                    get { return _TITLE; }
                    set { _TITLE = value; }
                }
                public string FILEPATH
                {
                    get { return _FILEPATH; }
                    set { _FILEPATH = value; }
                }
                public string FILE_NAME
                {
                    get { return _FILE_NAME; }
                    set { _FILE_NAME = value; }
                }
                public DateTime AUDIT_DATE
                {
                    get { return _AUDIT_DATE; }
                    set { _AUDIT_DATE = value; }
                }
                public DateTime MEETING_DATE
                {
                    get { return _MEETING_DATE; }
                    set { _MEETING_DATE = value; }
                }
                public string VENUE
                {
                    get { return _VENUE; }
                    set { _VENUE = value; }
                }
                public string ADDLINE2
                {
                    get { return _ADDLINE2; }
                    set { _ADDLINE2 = value; }
                }
                public int CITY
                {
                    get { return _CITY; }
                    set { _CITY = value; }

                }
                public int STATE
                {
                    get { return _STATE; }
                    set { _STATE = value; }

                }
                public int ZIPCODE
                {
                    get { return _ZIPCODE; }
                    set { _ZIPCODE = value; }

                }
                public int COUNTRY
                {
                    get { return _COUNTRY; }
                    set { _COUNTRY = value; }

                }

                public DateTime LAST_DATE
                {
                    get { return _LAST_DATE; }
                    set { _LAST_DATE = value; }
                }

                public int DAID
                {
                    get { return _DAID; }
                    set { _DAID = value; }
                }

                #endregion

                #region COMMITEE Public Members
                /// <summary>
                /// MODIFIED BY   : MRUNAL SINGH
                /// MODIFIED DATE : 16-FEB-2015
                /// DESCRIPTION  : SET PROPERTY FOR DEPTNO
                /// </summary>
                public int ID
                {
                    get { return _ID; }
                    set { _ID = value; }
                }
                public string CODE
                {
                    get { return _CODE; }
                    set { _CODE = value; }
                }
                public string NAME
                {
                    get { return _Name; }
                    set { _Name = value; }
                }
                public int DEPTNO
                {
                    get { return _DEPTNO; }
                    set { _DEPTNO = value; }
                }

                public int VENUEID
                {
                    get { return _VENUEID; }
                    set { _VENUEID = value; }
                }
                public int COLLEGE_NO
                {
                    get { return _COLLEGE_NO; }
                    set { _COLLEGE_NO = value; }
                }

                public char COMMITTEE_TYPE
                {
                    get { return _COMMITTEE_TYPE; }
                    set { _COMMITTEE_TYPE = value; }
                }


                #endregion

                #region public DesigMaster
                /// <summary>
                /// CREATED BY   : MRUNAL SINGH
                /// CREATED DATE : 29-DEC-2014
                /// DESCRIPTION  : FOR DESIGNATION MASTER
                /// </summary>
                public int PK_COMMITEEDES
                {
                    get { return _PK_COMMITEEDES; }
                    set { _PK_COMMITEEDES = value; }
                }
                public string DESIGNAME
                {
                    get { return _DESIGNAME; }
                    set { _DESIGNAME = value; }
                }
                #endregion

                #region CommiteeMember


                public int DESIGNATION_ID
                {
                    get { return _DESIGNATION_ID; }
                    set { _DESIGNATION_ID = value; }
                }
                public DateTime STARTDATE
                {
                    get { return _STARTDATE; }
                    set { _STARTDATE = value; }
                }
                public DateTime ENDDATE
                {
                    get { return _ENDDATE; }
                    set { _ENDDATE = value; }
                }
                public int MEMBER_NO
                {
                    get { return _MEMBER_NO; }
                    set { _MEMBER_NO = value; }
                }
                public int COMMITEE_NO
                {
                    get { return _COMMITEE_NO; }
                    set { _COMMITEE_NO = value; }
                }
                public char ACTIVE
                {
                    get { return _ACTIVE; }
                    set { _ACTIVE = value; }
                }

                #endregion



                #region Plan & Schedule

                private int _PSNO = 0;



                public int PSNO
                {
                    get { return _PSNO; }
                    set { _PSNO = value; }
                }



                #endregion


                #region Agenda Approval

                private int _APPROVAL_ID = 0;
                private string _REMARK = string.Empty;
                private string _MEETING_CODE = string.Empty;
                private DataTable _REMARK_TBL = null;
                private DataTable _DRAFT_REMARK_TBL = null;

                public int APPROVAL_ID
                {
                    get { return _APPROVAL_ID; }
                    set { _APPROVAL_ID = value; }
                }

                public string REMARK
                {
                    get { return _REMARK; }
                    set { _REMARK = value; }
                }

                public string MEETING_CODE
                {
                    get { return _MEETING_CODE; }
                    set { _MEETING_CODE = value; }
                }

                public DataTable REMARK_TBL
                {
                    get { return _REMARK_TBL; }
                    set { _REMARK_TBL = value; }
                }

                public DataTable DRAFT_REMARK_TBL
                {
                    get { return _DRAFT_REMARK_TBL; }
                    set { _DRAFT_REMARK_TBL = value; }
                }


                #endregion

                #region Agenda Contents
                private int _ACID = 0;
                private int _AGENDA_ID = 0;
                private DataTable _CONTENT_TBL = null;

                public int ACID
                {
                    get { return _ACID; }
                    set { _ACID = value; }
                }

                public int AGENDA_ID
                {
                    get { return _AGENDA_ID; }
                    set { _AGENDA_ID = value; }
                }

                public DataTable CONTENT_TBL
                {
                    get { return _CONTENT_TBL; }
                    set { _CONTENT_TBL = value; }
                }

                #endregion

            }
        }
    }
}