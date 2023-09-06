using System;
using System.Data;
using System.Web;
using System.Collections.Generic;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {
            public class ILibrary
            {
                #region Private Members

                private int _BOOK_NO;

                private System.Nullable<int> _SESSIONNO;

                private System.Nullable<int> _COURSENO;

                private System.Nullable<int> _UA_NO;

                private string _BOOK_NAME;

                private string _AUTHOR_NAME;

                private string _PUBLISHER_NAME;

                private string _ATTACHMENT;

                private string _FILENAME = string.Empty;

                private string _OLDFILENAME = string.Empty;

                private System.Nullable<System.DateTime> _UPLOAD_DATE;

                private string _WEBSITE_LINK;

                private string _COLLEGE_CODE;


                //FOR IP ADDRESS ENTRY
                private string _computername;

                private string _ipaddress;

                private int _id;


                List<ELibraryAttachment> _attachments = new List<ELibraryAttachment>();

                #endregion

                #region Public Properties

                public int BOOK_NO
                {
                    get
                    {
                        return this._BOOK_NO;
                    }
                    set
                    {
                        if ((this._BOOK_NO != value))
                        {
                            this._BOOK_NO = value;
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

                public string BOOK_NAME
                {
                    get
                    {
                        return this._BOOK_NAME;
                    }
                    set
                    {
                        if ((this._BOOK_NAME != value))
                        {
                            this._BOOK_NAME = value;
                        }
                    }
                }

                public string AUTHOR_NAME
                {
                    get
                    {
                        return this._AUTHOR_NAME;
                    }
                    set
                    {
                        if ((this._AUTHOR_NAME != value))
                        {
                            this._AUTHOR_NAME = value;
                        }
                    }
                }

                public string PUBLISHER_NAME
                {
                    get
                    {
                        return this._PUBLISHER_NAME;
                    }
                    set
                    {
                        if ((this._PUBLISHER_NAME != value))
                        {
                            this._PUBLISHER_NAME = value;
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

                public System.Nullable<System.DateTime> UPLOAD_DATE
                {
                    get
                    {
                        return this._UPLOAD_DATE;
                    }
                    set
                    {
                        if ((this._UPLOAD_DATE != value))
                        {
                            this._UPLOAD_DATE = value;
                        }
                    }
                }

                public string WEBSITE_LINK
                {
                    get
                    {
                        return this._WEBSITE_LINK;
                    }
                    set
                    {
                        if ((this._WEBSITE_LINK != value))
                        {
                            this._WEBSITE_LINK = value;
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


                //FOR IP ADDRESS ENTRY

                public int Id
                {
                    get { return _id; }
                    set { _id = value; }
                }
                public string Ipaddress
                {
                    get { return _ipaddress; }
                    set { _ipaddress = value; }
                }
                public string Computername
                {
                    get { return _computername; }
                    set { _computername = value; }
                }



                public List<ELibraryAttachment> Attachments
                {
                    get { return _attachments; }
                    set { _attachments = value; }
                }

                #endregion
            }



            //Used for multiple file attachments
            public class ELibraryAttachment
            {
                int _attachmentId = 0;
                int _book_no = 0;
                string _fileName = "";
                string _filePath = "";
                int _size = 0;

                public int AttachmentId
                {
                    get { return _attachmentId; }
                    set { _attachmentId = value; }
                }

                public int BOOK_NO
                {
                    get { return _book_no; }
                    set { _book_no = value; }
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

