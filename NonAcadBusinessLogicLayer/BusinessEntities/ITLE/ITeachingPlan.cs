using System;
using System.Data;
using System.Web;

namespace IITMS.UAIMS.BusinessLayer.BusinessEntities
{
    public class ITeachingPlan
    {
        #region Private Members

        private int _PLAN_NO;

        private System.Nullable<int> _SESSIONNO;

        private int _UA_NO = 0;

        private System.Nullable<System.DateTime> _STARTDATE = DateTime.MinValue;

        private System.Nullable<System.DateTime> _ENDDATE = DateTime.MinValue;

        private System.Nullable<int> _COURSENO = 0;

        private string _SUBJECT = string.Empty;

        private string _DESCRIPTION = string.Empty;

        private string _MEDIA = string.Empty;

        private System.Nullable<char> _STATUS = '1';

        private string _COLLEGE_CODE = string.Empty;

        private string _SYLLABUS_NAME = string.Empty;



        private string _UNIT_NAME = string.Empty;



        private string _TOPIC_NAME = string.Empty;

       
        #endregion

        #region Public Properties

        public int PLAN_NO
        {
            get
            {
                return this._PLAN_NO;
            }
            set
            {
                if ((this._PLAN_NO != value))
                {
                    this._PLAN_NO = value;
                }
            }
        }

        public System.Nullable<int> SESSIONNO
        {
            get
            {
                return this._SESSIONNO;
            }
            set
            {
                if ((this._SESSIONNO != value))
                {
                    this._SESSIONNO = value;
                }
            }
        }

        public int UA_NO
        {
            get
            {
                return this._UA_NO;
            }
            set
            {
                if ((this._UA_NO != value))
                {
                    this._UA_NO = value;
                }
            }
        }

        public System.Nullable<System.DateTime> STARTDATE
        {
            get
            {
                return this._STARTDATE;
            }
            set
            {
                if ((this._STARTDATE != value))
                {
                    this._STARTDATE = value;
                }
            }
        }

        public System.Nullable<System.DateTime> ENDDATE
        {
            get
            {
                return this._ENDDATE;
            }
            set
            {
                if ((this._ENDDATE != value))
                {
                    this._ENDDATE = value;
                }
            }
        }

        public System.Nullable<int> COURSENO
        {
            get
            {
                return this._COURSENO;
            }
            set
            {
                if ((this._COURSENO != value))
                {
                    this._COURSENO = value;
                }
            }
        }

        public string SUBJECT
        {
            get
            {
                return this._SUBJECT;
            }
            set
            {
                if ((this._SUBJECT != value))
                {
                    this._SUBJECT = value;
                }
            }
        }

        public string DESCRIPTION
        {
            get
            {
                return this._DESCRIPTION;
            }
            set
            {
                if ((this._DESCRIPTION != value))
                {
                    this._DESCRIPTION = value;
                }
            }
        }

        public string MEDIA
        {
            get
            {
                return this._MEDIA;
            }
            set
            {
                if ((this._MEDIA != value))
                {
                    this._MEDIA = value;
                }
            }
        }

        public System.Nullable<char> STATUS
        {
            get
            {
                return this._STATUS;
            }
            set
            {
                if ((this._STATUS != value))
                {
                    this._STATUS = value;
                }
            }
        }

        public string COLLEGE_CODE
        {
            get
            {
                return this._COLLEGE_CODE;
            }
            set
            {
                if ((this._COLLEGE_CODE != value))
                {
                    this._COLLEGE_CODE = value;
                }
            }
        }


        public string SYLLABUS_NAME
        {
            get { return _SYLLABUS_NAME; }
            set { _SYLLABUS_NAME = value; }
        }

        public string UNIT_NAME
        {
            get { return _UNIT_NAME; }
            set { _UNIT_NAME = value; }
        }

        public string TOPIC_NAME
        {
            get { return _TOPIC_NAME; }
            set { _TOPIC_NAME = value; }
        }

        #endregion
    }
}