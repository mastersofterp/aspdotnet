using System;
using System.Data;
using System.Web;



namespace IITMS
{
    namespace UAIMS
    {

        namespace BusinessLayer.BusinessEntities
        {
            public class Documentation
            {
                #region Private Members
                private int _IDNO;
                private string _CATEGORY;
                private int _UPLNO;
                private int _UA_NO;
                private int _DNO;
                private string _TITLE;
                private string _DESCRIPTION;
                private string _KEYWORD;
                private char _SHARED;
                private string _CREATED_DATE;
                private string _COLLEGE_CODE;
                private string _FILENAME;
                private int _SIZE;
                private string _FILE_PATH;
                private string _ORIGINAL_FILENAME;
                private int _TYPE;
                private int _SUB_HEAD;
                private string _CAT_NAME;
                private string _DEPARTMENTS;
                private string _ATTACHMENT;
                private string _FILEPATH;
                private DataTable _AttachTable;
                private int _IS_BLOB;


                #endregion

                #region Public Members

                public DataTable AttachTable
                {
                    get { return _AttachTable; }
                    set { _AttachTable = value; }
                }

                public int IS_BLOB
                {
                    get { return _IS_BLOB; }
                    set { _IS_BLOB = value; }
                }
                public string FILEPATH
                {
                    get { return _FILEPATH; }
                    set { _FILEPATH = value; }
                }

                public string ATTACHMENT
                {
                    get { return _ATTACHMENT; }
                    set { _ATTACHMENT = value; }
                }

                public string CAT_NAME
                {
                    get { return _CAT_NAME; }
                    set { _CAT_NAME = value; }
                }


                public int SUB_HEAD
                {
                    get { return _SUB_HEAD; }
                    set { _SUB_HEAD = value; }
                }
                public int TYPE
                {
                    get { return _TYPE; }
                    set { _TYPE = value; }
                }

                public string ORIGINAL_FILENAME
                {
                    get { return _ORIGINAL_FILENAME; }
                    set { _ORIGINAL_FILENAME = value; }
                }
                public string FILE_PATH
                {
                    get { return _FILE_PATH; }
                    set { _FILE_PATH = value; }
                }
                public int SIZE
                {
                    get { return _SIZE; }
                    set { _SIZE = value; }
                }

                public string FILENAME
                {
                    get { return _FILENAME; }
                    set { _FILENAME = value; }
                }
                public char SHARED
                {
                    get { return _SHARED; }
                    set { _SHARED = value; }
                }
                public string COLLEGE_CODE
                {
                    get { return _COLLEGE_CODE; }
                    set { _COLLEGE_CODE = value; }
                }

                public string CREATED_DATE
                {
                    get { return _CREATED_DATE; }
                    set { _CREATED_DATE = value; }
                }


                public string KEYWORD
                {
                    get { return _KEYWORD; }
                    set { _KEYWORD = value; }
                }

                public string DESCRIPTION
                {
                    get { return _DESCRIPTION; }
                    set { _DESCRIPTION = value; }
                }

                public string TITLE
                {
                    get { return _TITLE; }
                    set { _TITLE = value; }
                }

                public int DNO
                {
                    get { return _DNO; }
                    set { _DNO = value; }
                }

                public int UA_NO
                {
                    get { return _UA_NO; }
                    set { _UA_NO = value; }
                }

                public int UPLNO
                {
                    get { return _UPLNO; }
                    set { _UPLNO = value; }
                }

                public string CATEGORY
                {
                    get { return _CATEGORY; }
                    set { _CATEGORY = value; }
                }

                public int IDNO
                {
                    get { return _IDNO; }
                    set { _IDNO = value; }
                }
                public string DEPARTMENTS
                {
                    get { return _DEPARTMENTS; }
                    set { _DEPARTMENTS = value; }
                }
                #endregion
            }
        }
    }
}
