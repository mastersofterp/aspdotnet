//================================================================================================
//CREATED BY    : MRUNAL SINGH
//PROJECT NAME  : UAIMS
//MODULE NAME   : SPORTS
//CREATION DATE : 24-APR-2017      
//================================================================================================  

using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {
            public class PlanSchedule
            {
                #region private  Members
                private int _PSID = 0;
                private int _EVENTID = 0;               
                private DateTime _FROMDATE;
                private DateTime _TODATE;
                private int _TEAMID = 0;
                private int _USERID = 0;
                private int _ETID = 0;
                private int _COLLEGE_NO = 0;
                private int _PAPNO = 0;
                private string _COLLEGE_CODE;    
                #endregion

                #region public  Members
                public int PSID
                {
                    get { return _PSID; }
                    set { _PSID = value; }
                }

                public int EVENTID
                {
                    get { return _EVENTID; }
                    set { _EVENTID = value; }
                }                
                public DateTime FROMDATE
                {
                    get { return _FROMDATE; }
                    set { _FROMDATE = value; }
                }
                public DateTime TODATE
                {
                    get { return _TODATE; }
                    set { _TODATE = value; }
                }
                public int TEAMID
                {
                    get { return _TEAMID; }
                    set { _TEAMID = value; }
                }
                public int USERID
                {
                    get { return _USERID; }
                    set { _USERID = value; }
                }

                public int ETID
                {
                    get { return _ETID; }
                    set { _ETID = value; }
                }

                public int COLLEGE_NO
                {
                    get { return _COLLEGE_NO; }
                    set { _COLLEGE_NO = value; }
                }
                public int PAPNO
                {
                    get { return _PAPNO; }
                    set { _PAPNO = value; }
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
