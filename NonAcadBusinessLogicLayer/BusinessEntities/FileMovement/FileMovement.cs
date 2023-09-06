//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : FILE MOVEMENT TRACKING                              
// CREATION DATE : 24-SEP-2015                                                        
// CREATED BY    : MRUNAL SINGH                                      
// MODIFIED DATE :
// MODIFIED DESC :
//====================================================================================== 

using System;
using System.Data;
using System.Web;


namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLogicLayer.BusinessEntities.FileMovement
        {
           public class FileMovement
           {
               #region Section Master   
            
               // private members
               private int _SECTIONNO = 0;
               private string _SECTIONNAME = string.Empty;
               private int _RECEIVER_ID = 0;
               private int _RECEIVER_HEAD_ID = 0;
               private string _USER_ROLES = string.Empty;

               // public members
               public int SECTIONNO
               {
                   get { return _SECTIONNO; }
                   set { _SECTIONNO = value; }
               }
               public string SECTIONNAME
               {
                   get { return _SECTIONNAME; }
                   set { _SECTIONNAME = value; }
               }
               public int RECEIVER_ID
               {
                   get { return _RECEIVER_ID; }
                   set { _RECEIVER_ID = value; }
               }
               public int RECEIVER_HEAD_ID
               {
                   get { return _RECEIVER_HEAD_ID; }
                   set { _RECEIVER_HEAD_ID = value; }
               }
               public string USER_ROLES
               {
                   get { return _USER_ROLES; }
                   set { _USER_ROLES = value; }
               }

               #endregion

               #region File Master
               // Private members
               private int _FILE_ID = 0;
               private string _FILE_CODE = string.Empty;
               private string _FILE_NAME = string.Empty;
               private string _DESCRIPTION = string.Empty;
               private DateTime _CREATION_DATE;
               private int _DOC_TYPE = 0;
               private int _STATUS = 0;
               private int _EMPDEPTNO = 0;
               private int _SECTION_TO_SEND = 0;
               private int _UA_NO = 0;
               private int _DNO = 0;
               private int _UPLNO = 0;
               private DataTable _FILE_TABLE = null;
               private string _SOURCEPATH = string.Empty;
               private int _LINK_STATUS = 0;
               private string _FOLDER_PATH = string.Empty;
               private string _FILE_KEYWORDS = string.Empty;
               private int _SELF_ROLE = 0;
               private int _RECEIVER_ROLE = 0;
               // Public Members            


               public string FOLDER_PATH
               {
                   get { return _FOLDER_PATH; }
                   set { _FOLDER_PATH = value; }
               }
               public int LINK_STATUS
               {
                   get { return _LINK_STATUS; }
                   set { _LINK_STATUS = value; }
               }
               public int SECTION_TO_SEND
               {
                   get { return _SECTION_TO_SEND; }
                   set { _SECTION_TO_SEND = value; }
               }
               public int FILE_ID
               {
                   get { return _FILE_ID; }
                   set { _FILE_ID = value; }
               }
               public string FILE_CODE
               {
                   get { return _FILE_CODE; }
                   set { _FILE_CODE = value; }
               }
               public string FILE_NAME
               {
                   get { return _FILE_NAME; }
                   set { _FILE_NAME = value; }
               }
               public string DESCRIPTION
               {
                   get { return _DESCRIPTION; }
                   set { _DESCRIPTION = value; }
               }
               public DateTime CREATION_DATE
               {
                   get { return _CREATION_DATE; }
                   set { _CREATION_DATE = value; }
               }
               public int DOC_TYPE
               {
                   get { return _DOC_TYPE; }
                   set { _DOC_TYPE = value; }
               }
               public int STATUS
               {
                   get { return _STATUS; }
                   set { _STATUS = value; }
               }
               public int EMPDEPTNO
               {
                   get { return _EMPDEPTNO; }
                   set { _EMPDEPTNO = value; }
               }

               public int UA_NO
               {
                   get { return _UA_NO; }
                   set { _UA_NO = value; }
               }
               public int DNO
               {
                   get { return _DNO; }
                   set { _DNO = value; }
               }
               public int UPLNO
               {
                   get { return _UPLNO; }
                   set { _UPLNO = value; }
               }
               public DataTable FILE_TABLE
               {
                   get { return _FILE_TABLE; }
                   set { _FILE_TABLE = value; }
               }

               public string SOURCEPATH
               {
                   get { return _SOURCEPATH; }
                   set { _SOURCEPATH = value; }
               }
               public string FILE_KEYWORDS
               {
                   get { return _FILE_KEYWORDS; }
                   set { _FILE_KEYWORDS = value; }
               }
               public int SELF_ROLE
               {
                   get { return _SELF_ROLE; }
                   set { _SELF_ROLE = value; }
               }

               public int RECEIVER_ROLE
               {
                   get { return _RECEIVER_ROLE; }
                   set { _RECEIVER_ROLE = value; }
               }
               #endregion

               #region File Movement
               // Private members
               private string _FILEPATH = string.Empty;
               private string _SECTIONPATH = string.Empty;
               private int _FILE_MOVID = 0;
               private int _USERNO = 0;
               private string _REMARK = string.Empty;
               //Public member
               public string FILEPATH
               {
                   get { return _FILEPATH; }
                   set { _FILEPATH = value; }
               }
               public string SECTIONPATH
               {
                   get { return _SECTIONPATH; }
                   set { _SECTIONPATH = value; }
               }
               public int FILE_MOVID
               {
                   get { return _FILE_MOVID; }
                   set { _FILE_MOVID = value; }
               }
               public int USERNO
               {
                   get { return _USERNO; }
                   set { _USERNO = value; }
               }
               public string REMARK
               {
                   get { return _REMARK; }
                   set { _REMARK = value; }
               }
               #endregion

               #region File Receive
               // private members
               private int _FMTRAN_ID = 0;
               private string _FSTATUS = string.Empty;
               // public members
               public int FMTRAN_ID
               {
                   get { return _FMTRAN_ID; }
                   set { FMTRAN_ID = value; }
               }

               public string FSTATUS
               {
                   get { return _FSTATUS; }
                   set { _FSTATUS = value; }
               }
               #endregion

               #region Document Type


               private int _DOCUMENT_TYPE_ID = 0;
               private string _DOCUMENT_TYPE = string.Empty;


               public int DOCUMENT_TYPE_ID
               {
                   get { return _DOCUMENT_TYPE_ID; }
                   set { _DOCUMENT_TYPE_ID = value; }
               }
               public string DOCUMENT_TYPE
               {
                   get { return _DOCUMENT_TYPE; }
                   set { _DOCUMENT_TYPE = value; }
               }

               #endregion



               #region Role Master

               private int _ROLE_ID = 0;
               private string _ROLENAME = string.Empty;

               public int ROLE_ID
               {
                   get { return _ROLE_ID; }
                   set { _ROLE_ID = value; }
               }
               public string ROLENAME
               {
                   get { return _ROLENAME; }
                   set { _ROLENAME = value; }
               }

               #endregion


           }
        }
    }
}