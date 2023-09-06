using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Text;
using System.Linq;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {
            public class ILecture
            {
                #region Private Members

                private int _NOTE_NO = 0;

                private System.Nullable<int> _SUB_NO = 0;

                private System.Nullable<int> _SESSIONNO = 0;

                private System.Nullable<int> _COURSENO = 0;

                private System.Nullable<int> _UA_NO = 0;

                private string _TOPIC_NAME = string.Empty;

                private string _DESCRIPTION = string.Empty;

                private string _ATTACHMENT = string.Empty;
                private string _FILENAME = string.Empty;

                private string _OLDFILENAME = string.Empty;

                private System.Nullable<System.DateTime> _CREATED_DATE = DateTime.MinValue;

                List<LectureNotesAttachment> _attachments = new List<LectureNotesAttachment>();

                private string _COLLEGE_CODE = string.Empty;

                #endregion

                #region Public Properties

                public int NOTE_NO
                {
                    get
                    {
                        return this._NOTE_NO;
                    }
                    set
                    {
                        if ((this._NOTE_NO != value))
                        {
                            this._NOTE_NO = value;
                        }
                    }
                }

                public System.Nullable<int> SUB_NO
                {
                    get
                    {
                        return this._SUB_NO;
                    }
                    set
                    {
                        if ((this._SUB_NO != value))
                        {
                            this._SUB_NO = value;
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

                public System.Nullable<int> UA_NO
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

                public string TOPIC_NAME
                {
                    get
                    {
                        return this._TOPIC_NAME;
                    }
                    set
                    {
                        if ((this._TOPIC_NAME != value))
                        {
                            this._TOPIC_NAME = value;
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

                public string OLDFILENAME
                {
                    get
                    {
                        return this._OLDFILENAME;
                    }
                    set
                    {
                        if ((this._OLDFILENAME != value))
                        {
                            this._OLDFILENAME = value;
                        }
                    }
                }

                public System.Nullable<System.DateTime> CREATED_DATE
                {
                    get
                    {
                        return this._CREATED_DATE;
                    }
                    set
                    {
                        if ((this._CREATED_DATE != value))
                        {
                            this._CREATED_DATE = value;
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

                public List<LectureNotesAttachment> Attachments
                {
                    get { return _attachments; }
                    set { _attachments = value; }
                }


                #endregion



              
            }
        }


        //Used for multiple file attachments
        public class LectureNotesAttachment
        {
            int _attachmentId = 0;
            int _as_no = 0;
            string _fileName = "";
            string _filePath = "";
            int _size = 0;

            public int AttachmentId
            {
                get { return _attachmentId; }
                set { _attachmentId = value; }
            }

            public int NOTE_NO
            {
                get { return _as_no; }
                set { _as_no = value; }
            }

            public string FileName
            {
                get { return _fileName; }
                set { _fileName = value; }
            }

            public string FilePath
            {
                get { return _filePath; }
                set { _filePath = value; }
            }

            public int Size
            {
                get { return _size; }
                set { _size = value; }
            }


        }
    }
}