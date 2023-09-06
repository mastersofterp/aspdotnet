//================================================================================================
//CREATED BY    : MRUNAL SINGH
//PROJECT NAME  : UAIMS
//MODULE NAME   : SPORTS
//CREATION DATE : 01-MAY-2017      
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
            public class EventApprovalEnt
            {
                #region Event Approval

                private int _PSID = 0;
                private int _EVENTID = 0;
                private int _UA_NO = 0;
                private string _Status = string.Empty;
                private string _Remarks = string.Empty;
                private DateTime _FROMDT = DateTime.MinValue;
                private DateTime _TODT = DateTime.MinValue;


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

                public int UA_NO
                {
                    get { return _UA_NO; }
                    set { _UA_NO = value; }
                }

                public string Status
                {
                    get { return _Status; }
                    set { _Status = value; }
                }

                public string Remarks
                {
                    get { return _Remarks; }
                    set { _Remarks = value; }
                }

                public DateTime FROMDT
                {
                    get { return _FROMDT; }
                    set { _FROMDT = value; }
                }

                public DateTime TODT
                {
                    get { return _TODT; }
                    set { _TODT = value; }
                }




                #endregion

                #region Event Team Allotment

                private int _ETID = 0;
                private int _ETALLOTID = 0;
                private int _TEAMID = 0;
                private int _VENUEID = 0;
                private int _USERID = 0;

                public int ETID
                {
                    get { return _ETID; }
                    set { _ETID = value; }
                }
                public int ETALLOTID
                {
                    get { return _ETALLOTID; }
                    set { _ETALLOTID = value; }
                } 

                public int TEAMID
                {
                    get { return _TEAMID; }
                    set { _TEAMID = value; }
                }

                public int VENUEID
                {
                    get { return _VENUEID; }
                    set { _VENUEID = value; }
                }

                public int USERID
                {
                    get { return _USERID; }
                    set { _USERID = value; }
                }

                #endregion
            }
        }
    }
}
