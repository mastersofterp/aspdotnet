//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : HEALTH (Laboratory Test)                        
// CREATION DATE : 13-APR-2016                                                        
// CREATED BY    : MRUNAL SINGH 
//====================================================================================== 

using System;
using System.Data;
using System.Web;

namespace IITMS
{
    namespace UAIMS
    {

        namespace BusinessLogicLayer.BusinessEntities.Health
        {
            public class LabMaster
            {
                #region Test Title

                private int _TITLENO = 0;
                private string _TITLE = string.Empty;
                private string _COLLEGE_CODE = string.Empty;

                public int TITLENO
                {
                    get { return _TITLENO; }
                    set { _TITLENO = value; }
                }
                public string TITLE
                {
                    get { return _TITLE; }
                    set { _TITLE = value; }
                }
                public string COLLEGE_CODE
                {
                    get { return _COLLEGE_CODE; }
                    set { _COLLEGE_CODE = value; }
                }


                #endregion

                #region Test Content
                private int _CONTENTNO = 0;
                private string _GROUP_NAME = string.Empty;
                private string _CONTENT_NAME = string.Empty;
                private string _UNIT = string.Empty;
                private string _NORMAL_RANGE = string.Empty;
                private string _INSERT_SRNO = string.Empty;
                private string _UPDATE_SRNO = string.Empty;

                private DataTable _TESTCONTENT = null;

                public DataTable TESTCONTENT
                {
                    get { return _TESTCONTENT; }
                    set { _TESTCONTENT = value; }
                }
                
                public int CONTENTNO
                {
                    get { return _CONTENTNO; }
                    set { _CONTENTNO = value; }
                }
                public string GROUP_NAME
                {
                    get { return _GROUP_NAME; }
                    set { _GROUP_NAME = value; }
                }
                public string CONTENT_NAME
                {
                    get { return _CONTENT_NAME; }
                    set { _CONTENT_NAME = value; }
                }
                public string UNIT
                {
                    get { return _UNIT; }
                    set { _UNIT = value; }
                }
                public string NORMAL_RANGE
                {
                    get { return _NORMAL_RANGE; }
                    set { _NORMAL_RANGE = value; }
                }

                public string INSERT_SRNO
                {
                    get { return _INSERT_SRNO; }
                    set { _INSERT_SRNO = value; }
                }
                public string UPDATE_SRNO
                {
                    get { return _UPDATE_SRNO; }
                    set { _UPDATE_SRNO = value; }
                }
                #endregion

                #region Observation Entry
                private int _OBSERNO = 0;        // main table of observation
                private int _OBTRNO = 0;         // transaction table of observation
                private int _PATIENT_ID = 0;
                private int _OPDID = 0;               
                private DateTime  _TEST_SAMPLE_DT ; 
                private DateTime _TEST_DUE_DT ;
                private string _COMMON_REMARK = string.Empty;
                private string _PATIENT_VALUES = string.Empty;
                private string _REMARKS = string.Empty;

                private DataTable _OBSERVATION_TRAN = null;


                public int OBSERNO
                {
                    get { return _OBSERNO; }
                    set { _OBSERNO = value; }
                }
                public int OBTRNO
                {
                    get { return _OBTRNO; }
                    set { _OBTRNO = value; }
                } 
                public DataTable OBSERVATION_TRAN
                {
                    get { return _OBSERVATION_TRAN; }
                    set { _OBSERVATION_TRAN = value; }
                }
                public int PATIENT_ID
                {
                    get { return _PATIENT_ID; }
                    set { _PATIENT_ID = value; }
                }
                public int OPDID
                {
                    get { return _OPDID; }
                    set { _OPDID = value; }
                }               
                public DateTime TEST_SAMPLE_DT
                {
                    get { return _TEST_SAMPLE_DT; }
                    set { _TEST_SAMPLE_DT = value; }
                }
                public DateTime TEST_DUE_DT
                {
                    get { return _TEST_DUE_DT; }
                    set { _TEST_DUE_DT = value; }
                }
                public string COMMON_REMARK
                {
                    get { return _COMMON_REMARK; }
                    set { _COMMON_REMARK = value; }
                }
                public string PATIENT_VALUES
                {
                    get { return _PATIENT_VALUES; }
                    set { _PATIENT_VALUES = value; }
                }
                public string REMARKS
                {
                    get { return _REMARKS; }
                    set { _REMARKS = value; }
                }
                #endregion

            }
        }
    }
}
