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

   public class IFAQMaster
    {


        #region Private Properties

        private int _QUES_NO;

        private string _QUESTION = string.Empty;


        private int _UA_NO;

        private int _IDNO;

        private DateTime _CREATED_DATE;

        private string _ANSWER_REPLY = string.Empty;



        private System.Nullable<int> _SESSIONNO;

        private int _COURSENO;

        private string _SUBJECT;

        private string _ATTACHMENT = string.Empty;

        private string _FILENAME = string.Empty;

        private string _OLDFILENAME = string.Empty;

        private System.Nullable<System.DateTime> _ASSIGNDATE = DateTime.Now;

        private System.Nullable<System.DateTime> _REPLY_DATE = DateTime.Now;

        private System.Nullable<char> _STATUS;
        private System.Nullable<char> _REPLY;

        private string _COLLEGE_CODE;

        #endregion


        #region Public Properties


        public int QUES_NO
        {
            get { return _QUES_NO; }
            set { _QUES_NO = value; }
        }

        public DateTime CREATED_DATE
        {
            get { return _CREATED_DATE; }
            set { _CREATED_DATE = value; }
        }

        public string ANSWER_REPLY
        {
            get { return _ANSWER_REPLY; }
            set { _ANSWER_REPLY = value; }
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

        public int IDNO
        {
            get
            {
                return this._IDNO;
            }
            set
            {
                if ((this._IDNO != value))
                {
                    this._IDNO = value;
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

        public int COURSENO
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

        public string QUESTION
        {
            get { return _QUESTION; }
            set { _QUESTION = value; }
        }

        public string ATTACHMENT
        {
            get
            {
                return this._ATTACHMENT;
            }
            set
            {
                if ((this._ATTACHMENT != value))
                {
                    this._ATTACHMENT = value;
                }
            }
        }
        public string FILENAME
        {
            get
            {
                return this._FILENAME;
            }
            set
            {
                this._FILENAME = value;
            }
        }
        public string OLDFILENAME
        {
            get
            {
                return this._OLDFILENAME;
            }
            set
            {
                this._OLDFILENAME = value;
            }
        }
        public System.Nullable<System.DateTime> ASSIGNDATE
        {
            get
            {
                return this._ASSIGNDATE;
            }
            set
            {
                if ((this._ASSIGNDATE != value))
                {
                    this._ASSIGNDATE = value;
                }
            }
        }

        public System.Nullable<System.DateTime> REPLY_DATE
        {
            get
            {
                return this._REPLY_DATE;
            }
            set
            {
                if ((this._REPLY_DATE != value))
                {
                    this._REPLY_DATE = value;
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

        public System.Nullable<char> REPLY
        {
            get
            {
                return this._REPLY;
            }
            set
            {
                if ((this._REPLY != value))
                {
                    this._REPLY = value;
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

        #endregion

    }
}
    }
}

   

