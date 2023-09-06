
//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : Grievance Redressal                           
// CREATION DATE : 27-july-2019                                                        
// CREATED BY    : NANCY SHARMA                                   
// MODIFIED DATE :
// MODIFIED DESC :
//====================================================================================== 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLogicLayer.BusinessEntities
        {
            public class GrievanceEntity
            {
                #region Grievance Type

                //private  member
                private string _GRIEVANCE_TYPE = string.Empty;
                private int _GRIEVANCE_TYPE_ID = 0;
                private int _UANO = 0;
                private string _GRIEVANCE_TYPE_CODE = string.Empty;
                private int _SUBUANO = 0;               
                private System.Nullable<char> _ISCOMMITEETYPE;

                //Public member
                public int GRIEVANCE_TYPE_ID
                {
                    get { return _GRIEVANCE_TYPE_ID; }
                    set { _GRIEVANCE_TYPE_ID = value; }
                }
                public string GRIEVANCE_TYPE
                {
                    get { return _GRIEVANCE_TYPE; }
                    set { _GRIEVANCE_TYPE = value; }
                }
                public int UANO
                {
                    get { return _UANO; }
                    set { _UANO = value; }
                }
                public string GRIEVANCE_TYPE_CODE
                {
                    get { return _GRIEVANCE_TYPE_CODE; }
                    set { _GRIEVANCE_TYPE_CODE = value; }
                }

                public int SUBUANO
                {
                    get { return _SUBUANO; }
                    set { _SUBUANO = value; }
                }

                public System.Nullable<char> ISCOMMITEETYPE
                {
                    get
                    {
                        return this._ISCOMMITEETYPE;
                    }
                    set
                    {
                        if ((this._ISCOMMITEETYPE != value))
                        {
                            this._ISCOMMITEETYPE = value;
                        }
                    }
                }

                #endregion

                #region Grievance  SubType

                private int _GRIV_SUB_ID = 0;
                private string _GRIV_SUB_TYPE = string.Empty;

                public int GRIV_SUB_ID
                {
                    get { return _GRIV_SUB_ID; }
                    set { _GRIV_SUB_ID = value; }
                }

                public string GRIV_SUB_TYPE
                {
                    get { return _GRIV_SUB_TYPE; }
                    set { _GRIV_SUB_TYPE = value; }
                }

                #endregion

                #region Grievance Redressal Committee


                //private  member
                private string _COMMITTEE_TYPE = string.Empty;
                private int _COMMITTEE_TYPE_ID = 0;
                
                private int _DEPT_FLAG = 0;

                //Public member
                public int DEPT_FLAG
                {
                    get { return _DEPT_FLAG; }
                    set { _DEPT_FLAG = value; }
                }
                public int COMMITTEE_TYPE_ID
                {
                    get { return _COMMITTEE_TYPE_ID; }
                    set { _COMMITTEE_TYPE_ID = value; }
                }
                public string COMMITTEE_TYPE
                {
                    get { return _COMMITTEE_TYPE; }
                    set { _COMMITTEE_TYPE = value; }
                }
               
                private DataTable _dtTableRCell = null;
                public object dtRTableCell;
                public DataTable dtTableRCell
                {
                    get { return _dtTableRCell; }
                    set { _dtTableRCell = value; }
                }
                #endregion

                #region Grievance Redressal Cell

                private int _GRC_ID = 0;
                private int _DEPARTMENT_ID = 0;
                private DataTable _GRCELL_TABLE = null;
                private DataTable _GRSUB_TABLE = null;
                private int _SUB_GR_ID = 0;

                public int GRC_ID
                {
                    get { return _GRC_ID; }
                    set { _GRC_ID = value; }
                }
                public int DEPARTMENT_ID
                {
                    get { return _DEPARTMENT_ID; }
                    set { _DEPARTMENT_ID = value; }
                }
                public DataTable GRCELL_TABLE
                {
                    get { return _GRCELL_TABLE; }
                    set { _GRCELL_TABLE = value; }
                }

                public DataTable GRSUB_TABLE
                {
                    get { return _GRSUB_TABLE; }
                    set { _GRSUB_TABLE = value; }
                }

                public int SUB_GR_ID
                {
                    get { return _SUB_GR_ID; }
                    set { _SUB_GR_ID = value; }
                }

                #endregion

                #region Grievance Application

                private string _MOBILE_NO = string.Empty;
                private string _EMAIL_ID = string.Empty;
                private int _GRCOMMITTEE_TYPE = 0;
                private string _GRIEVANCE = string.Empty;
                private DateTime _GR_APPLICATION_DATE;
                private int _GR_GAID = 0;
                //private int _DEPT_ID=0;
                private int _STATUS = 0;
                private string _GRIV_CODE = string.Empty;
                private int _GRIV_ID = 0;
                private string _GRIV_ATTACHMENT = string.Empty;
                public string _STUDEPTID = string.Empty;
                

                public int  GRIV_ID
                {
                    get { return _GRIV_ID; }
                    set { _GRIV_ID = value; }
                }
                public int  STATUS
                {
                    get { return _STATUS; }
                    set { _STATUS = value; }
                }
                public string MOBILE_NO
                { 
                    get { return _MOBILE_NO; }
                    set { _MOBILE_NO = value; }
                }
                public string  EMAIL_ID
                {
                    get { return _EMAIL_ID; }
                    set { _EMAIL_ID = value; }
                }
                public int GRCOMMITTEE_TYPE
                {
                    get { return _GRCOMMITTEE_TYPE; }
                    set { _GRCOMMITTEE_TYPE = value; }
                }
                public string GRIEVANCE
                {
                    get { return _GRIEVANCE; }
                    set { _GRIEVANCE = value; }
                }
                public DateTime GR_APPLICATION_DATE
                {
                    get { return _GR_APPLICATION_DATE; }
                    set { _GR_APPLICATION_DATE = value; }
                }
                public int GR_GAID
                {
                    get { return _GR_GAID; }
                    set { _GR_GAID = value; }
                }
                //public int  DEPT_ID
                //{
                //    get { return _DEPT_ID; }
                //    set { _DEPT_ID = value; }
                //}
                public string  GRIV_CODE
                {
                    get { return _GRIV_CODE; }
                    set { _GRIV_CODE = value; }
                }

                public string GRIV_ATTACHMENT
                {
                    get { return _GRIV_ATTACHMENT; }
                    set { _GRIV_ATTACHMENT = value; }
                }

                public string STUDEPTID
                {
                    get { return _STUDEPTID; }
                    set { _STUDEPTID = value; }
                }


                #endregion

                #region 
                private int _REPLY_ID = 0;              
                private int _REPLY_UANO = 0;
                private string _REPLY =string.Empty;
                private char _GR_STATUS ;
                private string _GR_REMARKS = string.Empty;
                private int _GAID = 0;
                private int _GAT_ID = 0;
                private int _GRCTID = 0;
                private string _REPLY_ATTACHMENT = string.Empty;

                public int  GRCTID
                {
                    get { return _GRCTID; }
                    set { _GRCTID = value; }
                }
                public int  GAT_ID
                {
                    get { return _GAT_ID; }
                    set { _GAT_ID = value; }
                }
                public int REPLY_ID
                {
                    get { return _REPLY_ID; }
                    set { _REPLY_ID = value; }
                }
                public int REPLY_UANO
                {
                    get { return _REPLY_UANO; }
                    set { _REPLY_UANO = value; }
                }
                public string REPLY
                {
                    get { return _REPLY; }
                    set { _REPLY = value; }
                }
                public char  GR_STATUS
                {
                    get { return _GR_STATUS; }
                    set { _GR_STATUS = value; }
                }
                public  string GR_REMARKS
                {
                    get { return _GR_REMARKS; }
                    set { _GR_REMARKS = value; }
                }
                public int GAID
                {
                    get { return _GAID; }
                    set { _GAID = value; }
                }

                public string REPLY_ATTACHMENT
                {
                    get { return _REPLY_ATTACHMENT; }
                    set { _REPLY_ATTACHMENT = value; }
                }

                
                #endregion



                private DateTime _FROM_DATE;
                private DateTime _TO_DATE;

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

               

            }
        }
    }
}
