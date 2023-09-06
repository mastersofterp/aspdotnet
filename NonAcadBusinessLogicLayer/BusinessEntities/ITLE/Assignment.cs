using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Linq;
using System.Text;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {
            public class Assignment
            {
                #region Private Members

                private int _AS_NO;

                private int _UA_NO;

                private int _IDNO;

                private int _SA_NO;

                private System.Nullable<int> _SESSIONNO;

                private int _COURSENO;

                private string _SUBJECT;

                private string _DESCRIPTION;

                private string _ATTACHMENT = string.Empty;

                private string _FILENAME = string.Empty;

                private string _OLDFILENAME = string.Empty;

                private System.Nullable<System.DateTime> _ASSIGNDATE = DateTime.Now;

                private System.Nullable<System.DateTime> _SUBMITDATE = DateTime.Now;

                private System.Nullable<System.DateTime> _REPLY_DATE = DateTime.Now;

                private System.Nullable<System.DateTime> _DUEDATE = DateTime.Now;

                private System.Nullable<System.DateTime> _SUBMITFROMDATE = DateTime.Now;

                private System.Nullable<System.DateTime> _REMINDME_DATE = DateTime.Now;

                private string _File_type = string.Empty;


                private System.Nullable<char> _STATUS;
                private System.Nullable<char> _CHECKED;

                private string _COLLEGE_CODE;
               

                private int _ASSIGNMENT_TOALMARKS = 0;

                List<AssignmentAttachment> _attachments = new List<AssignmentAttachment>();
                
                private int _ASSIGNMENTMARKS_STUDENT_OBTAINED = 0;

                private System.Nullable<char> _DISPLAY_MARKS;
                private int _OrganizationId;

                private int _Doc_Type_Id;

                private int _MAX_NO_OF_FILE;

                private string _FILEPATH;
                private DataTable _ExamMapTbl = null;
                #endregion

                #region Public Properties

                public DataTable ExamMapTbl
                {
                    get
                    {
                        return this._ExamMapTbl;
                    }
                    set
                    {
                        if ((this._ExamMapTbl != value))
                        {
                            this._ExamMapTbl = value;
                        }
                    }
                }
                public string FILEPATH
                {
                    get
                    {
                        return this._FILEPATH;
                    }
                    set
                    {
                        if ((this._FILEPATH != value))
                        {
                            this._FILEPATH = value;
                        }
                    }
                }

                public int AS_NO
                {
                    get
                    {
                        return this._AS_NO;
                    }
                    set
                    {
                        if ((this._AS_NO != value))
                        {
                            this._AS_NO = value;
                        }
                    }
                }

                public int Doc_Type_Id
                {
                    get
                    {
                        return this._Doc_Type_Id;
                    }
                    set
                    {
                        if ((this._Doc_Type_Id != value))
                        {
                            this._Doc_Type_Id = value;
                        }
                    }
                }

                public int MAX_NO_OF_FILE
                {
                    get
                    {
                        return this._MAX_NO_OF_FILE;
                    }
                    set
                    {
                        if ((this._MAX_NO_OF_FILE != value))
                        {
                            this._MAX_NO_OF_FILE = value;
                        }
                    }
                }

                public int ASSIGNMENTMARKS_STUDENT_OBTAINED
                {
                    get { return _ASSIGNMENTMARKS_STUDENT_OBTAINED; }
                    set { _ASSIGNMENTMARKS_STUDENT_OBTAINED = value; }
                }

                public int ASSIGNMENT_TOALMARKS
                {
                    get { return _ASSIGNMENT_TOALMARKS; }
                    set { _ASSIGNMENT_TOALMARKS = value; }
                }
                public int SA_NO
                {
                    get
                    {
                        return this._SA_NO;
                    }
                    set
                    {
                        if ((this._SA_NO != value))
                        {
                            this._SA_NO = value;
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

                 public string File_type
                 {
                     get
                     {
                         return this._File_type;
                     }
                     set
                     {
                         if ((this._File_type != value))
                         {
                             this._File_type = value;
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
                 public System.Nullable<System.DateTime> SUBMITDATE
                 {
                     get
                     {
                         return this._SUBMITDATE;
                     }
                     set
                     {
                         if ((this._SUBMITDATE != value))
                         {
                             this._SUBMITDATE = value;
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

                 public System.Nullable<System.DateTime> SUBMITFROMDATE
                 {
                     get
                     {
                         return this._SUBMITFROMDATE;
                     }
                     set
                     {
                         if ((this._SUBMITFROMDATE != value))
                         {
                             this._SUBMITFROMDATE = value;
                         }
                     }
                 }
                 public System.Nullable<System.DateTime> REMINDME_DATE
                 {
                     get
                     {
                         return this._REMINDME_DATE;
                     }
                     set
                     {
                         if ((this._REMINDME_DATE != value))
                         {
                             this._REMINDME_DATE = value;
                         }
                     }
                 }

                

                 public System.Nullable<System.DateTime> DUEDATE
                 {
                     get
                     {
                         return this._DUEDATE;
                     }
                     set
                     {
                         if ((this._DUEDATE != value))
                         {
                             this._DUEDATE = value;
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

                 public System.Nullable<char> CHECKED
                 {
                     get
                     {
                         return this._CHECKED;
                     }
                     set
                     {
                         if ((this._CHECKED != value))
                         {
                             this._CHECKED = value;
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


                 public List<AssignmentAttachment> Attachments
                 {
                     get { return _attachments; }
                     set { _attachments = value; }
                 }

                 public System.Nullable<char> DISPLAY_MARKS
                 {
                     get { return _DISPLAY_MARKS; }
                     set { _DISPLAY_MARKS = value; }
                 }

                 public int OrganizationId
                 {
                     get { return _OrganizationId; }
                     set { _OrganizationId = value; }
                 }
                #endregion
            }



            public class AssignmentAttachment
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

                public int Assignment_NO
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
}







