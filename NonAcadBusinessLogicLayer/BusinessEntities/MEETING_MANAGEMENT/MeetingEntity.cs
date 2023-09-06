using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {
            public class MeetingEntity
            {
                #region private
                private int _PKID;
                private int _QTYPE;
                private string _TITLE;
                private string _FNAME;
                private string _MNAME;
                private string _LNAME;
                private string _Representation;



                private DateTime _DOB;
                private string _PROFESSION;
                private string _ACAD_QUALI;
                private string _PAN_NO;
                private DateTime _FROM_DATE;
                private DateTime _TO_DATE;
                private DateTime _JOINING_DATE;
                private int _COLLEGENO = 0;

                private string _designation;
                private string _CONSTITUENCY;
                private string _P_ADDRESS;
                private string _P_CITY;
                private string _P_STATE;
                private string _P_PHONE;
                private string _P_EMAIL;
                private string _P_PIN;
                private string _P_ADDLINE;
                private string _P_MOBILE;
                private int _P_COUNTRY;
                private string _T_ADDRESS;
                private string _T_ADDLINE;
                private string _T_CITY;
                private string _T_STATE;
                private string _T_PIN;
                private int _T_COUNTRY;
                private string _T_PHONE;
                private string _T_MOBILE;
                private string _T_EMAIL;
                private int _USERID;
                private int _DEPTNO;         // define by Mrunal
                private DateTime _AUDITDATE;


                private int _PK_MEETINGDETAILS;
                private int _FK_Committe;
                private int _FK_AGENDA;
                private string _AGENDADETAILS;
                private string _Filepath;
                private char _LOCK_MEET;
                private string _METTINGCODE;
                private string _DisplayFileName;
                private int _MEETINGPRESENTY;
                private int _FKMEMBER;
                private int _DESIGID;
                private Boolean _IS_SAME_ADDRESS;

                private int _MEMBER_TYPE;



                #endregion

                #region Public Members
                public int MEMBER_TYPE
                {
                    get { return _MEMBER_TYPE; }
                    set { _MEMBER_TYPE = value; }
                }

                public int DEPTNO
                {
                    get { return _DEPTNO; }
                    set { _DEPTNO = value; }
                }

                public Boolean IS_SAME_ADDRESS
                {
                    get { return _IS_SAME_ADDRESS; }
                    set { _IS_SAME_ADDRESS = value; }
                }
                public int DESIGID
                {
                    get { return _DESIGID; }
                    set { _DESIGID = value; }
                }
                public int FKMEMBER
                {
                    get { return _FKMEMBER; }
                    set { _FKMEMBER = value; }
                }
                public int MEETINGPRESENTY
                {
                    get { return _MEETINGPRESENTY; }
                    set { _MEETINGPRESENTY = value; }
                }

                public int PK_MEETINGDETAILS
                {
                    get { return _PK_MEETINGDETAILS; }
                    set { _PK_MEETINGDETAILS = value; }
                }
                public int FK_Committe
                {
                    get { return _FK_Committe; }
                    set { _FK_Committe = value; }
                }

                public int FK_AGENDA
                {
                    get { return _FK_AGENDA; }
                    set { _FK_AGENDA = value; }
                }
                public string AGENDADETAILS
                {
                    get { return _AGENDADETAILS; }
                    set { _AGENDADETAILS = value; }
                }
                public string Filepath
                {
                    get { return _Filepath; }
                    set { _Filepath = value; }
                }
                public string METTINGCODE
                {
                    get { return _METTINGCODE; }
                    set { _METTINGCODE = value; }
                }
                public string DisplayFileName
                {
                    get { return _DisplayFileName; }
                    set { _DisplayFileName = value; }
                }
                public char LOCK_MEET
                {
                    get { return _LOCK_MEET; }
                    set { _LOCK_MEET = value; }
                }


                public int QTYPE
                {
                    get { return _QTYPE; }
                    set { _QTYPE = value; }
                }
                public int PKID
                {
                    get { return _PKID; }
                    set { _PKID = value; }
                }
                public string TITLE
                {
                    get { return _TITLE; }
                    set { _TITLE = value; }
                }

                public string FNAME
                {
                    get { return _FNAME; }
                    set { _FNAME = value; }
                }
                public string MNAME
                {
                    get { return _MNAME; }
                    set { _MNAME = value; }
                }

                public string LNAME
                {
                    get { return _LNAME; }
                    set { _LNAME = value; }

                }

                public string Representation
                {
                    get { return _Representation; }
                    set { _Representation = value; }
                }
                public string designation
                {
                    get { return _designation; }
                    set { _designation = value; }

                }
                public string CONSTITUENCY
                {
                    get { return _CONSTITUENCY; }
                    set { _CONSTITUENCY = value; }
                }
                public string P_ADDRESS
                {
                    get { return _P_ADDRESS; }
                    set { _P_ADDRESS = value; }
                }
                public string P_CITY
                {
                    get { return _P_CITY; }
                    set { _P_CITY = value; }
                }
                public string P_STATE
                {
                    get { return _P_STATE; }
                    set { _P_STATE = value; }
                }
                public string P_PHONE
                {
                    get { return _P_PHONE; }
                    set { _P_PHONE = value; }
                }
                public string P_EMAIL
                {
                    get { return _P_EMAIL; }
                    set { _P_EMAIL = value; }
                }
                public string P_PIN
                {
                    get { return _P_PIN; }
                    set { _P_PIN = value; }
                }

                public string T_CITY
                {
                    get { return _T_CITY; }
                    set { _T_CITY = value; }
                }
                public string T_ADDRESS
                {
                    get { return _T_ADDRESS; }
                    set { _T_ADDRESS = value; }
                }
                public string T_STATE
                {
                    get { return _T_STATE; }
                    set { _T_STATE = value; }

                }
                public string T_EMAIL
                {
                    get { return _T_EMAIL; }
                    set { _T_EMAIL = value; }

                }
                public string T_PHONE
                {
                    get { return _T_PHONE; }
                    set { _T_PHONE = value; }

                }
                public string T_PIN
                {
                    get { return _T_PIN; }
                    set { _T_PIN = value; }

                }
                public int USERID
                {
                    get { return _USERID; }
                    set { _USERID = value; }

                }
                public DateTime DOB
                {
                    get { return _DOB; }
                    set { _DOB = value; }
                }

                public DateTime FROM_DATE
                {
                    get { return _FROM_DATE; }
                    set { _FROM_DATE = value; }
                }
                public DateTime TO_DATE
                {
                    get { return _TO_DATE; }
                    set { _TO_DATE = value; }
                }
                public int T_COUNTRY
                {
                    get { return _T_COUNTRY; }
                    set { _T_COUNTRY = value; }
                }
                public int P_COUNTRY
                {
                    get { return _P_COUNTRY; }
                    set { _P_COUNTRY = value; }
                }
                public string P_MOBILE
                {
                    get { return _P_MOBILE; }
                    set { _P_MOBILE = value; }
                }
                public string P_ADDLINE
                {
                    get { return _P_ADDLINE; }
                    set { _P_ADDLINE = value; }
                }

                public string PAN_NO
                {
                    get { return _PAN_NO; }
                    set { _PAN_NO = value; }
                }

                public string ACAD_QUALI
                {
                    get { return _ACAD_QUALI; }
                    set { _ACAD_QUALI = value; }
                }

                public string PROFESSION
                {
                    get { return _PROFESSION; }
                    set { _PROFESSION = value; }
                }
                public string T_ADDLINE
                {
                    get { return _T_ADDLINE; }
                    set { _T_ADDLINE = value; }
                }
                public string T_MOBILE
                {
                    get { return _T_MOBILE; }
                    set { _T_MOBILE = value; }
                }

                public DateTime JOINING_DATE
                {
                    get { return _JOINING_DATE; }
                    set { _JOINING_DATE = value; }
                }

                public int COLLEGENO
                {
                    get { return _COLLEGENO; }
                    set { _COLLEGENO = value; }
                }
                #endregion
            }
        }
    }
}
