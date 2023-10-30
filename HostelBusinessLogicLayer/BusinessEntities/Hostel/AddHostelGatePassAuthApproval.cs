using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {
            public class AddHostelGatePassAuthApproval
            {
                #region Private Members
                private int _APP_NO;
                private int _AUTHORITY;
                private string _AUTHORITY_NAME;
                private int _CLUB_ACTIVITY_NO;
                private string _CLUB_ACTIVITY_TYPE;
                private int _APPROVAL_1;
                private int _APPROVAL_2;
                private int _APPROVAL_3;
                private string _AAPATH;
                private int _CREATED_BY;
                private DateTime _CREATED_DATE;
                private string _IP_ADDRESS;
                #endregion

                #region Public Property Fields
                public int APP_NO
                {
                    get { return this._APP_NO; }
                    set { if ((this._APP_NO != value)) this._APP_NO = value; }
                }

                public int AUTHORITY
                {
                    get { return this._AUTHORITY; }
                    set { if ((this._AUTHORITY != value)) this._AUTHORITY = value; }
                }

                public string AUTHORITY_NAME
                {
                    get { return this._AUTHORITY_NAME; }
                    set { if ((this._AUTHORITY_NAME != value)) this._AUTHORITY_NAME = value; }
                }

                public int APPROVAL_1
                {
                    get { return this._APPROVAL_1; }
                    set { if ((this._APPROVAL_1 != value)) this._APPROVAL_1 = value; }
                }


                public int APPROVAL_2
                {
                    get { return this._APPROVAL_2; }
                    set { if ((this._APPROVAL_2 != value)) this._APPROVAL_2 = value; }
                }

                public int APPROVAL_3
                {
                    get { return this._APPROVAL_3; }
                    set { if ((this._APPROVAL_3 != value)) this._APPROVAL_3 = value; }
                }

                public string AAPATH
                {
                    get { return this._AAPATH; }
                    set { if ((this._AAPATH != value)) this._AAPATH = value; }
                }

                public int CREATED_BY
                {
                    get { return this._CREATED_BY; }
                    set { if ((this._CREATED_BY != value)) this._CREATED_BY = value; }
                }

                public DateTime CREATED_DATE
                {
                    get { return this._CREATED_DATE; }
                    set { if ((this._CREATED_DATE != value)) this._CREATED_DATE = value; }
                }

                public string IP_ADDRESS
                {
                    get { return this._IP_ADDRESS; }
                    set { if ((this._IP_ADDRESS != value)) this._IP_ADDRESS = value; }
                }

                public int CLUB_ACTIVITY_NO
                {
                    get { return this._CLUB_ACTIVITY_NO; }
                    set { if ((this._CLUB_ACTIVITY_NO != value)) this._CLUB_ACTIVITY_NO = value; }
                }

                public string CLUB_ACTIVITY_TYPE
                {
                    get { return this._CLUB_ACTIVITY_TYPE; }
                    set { if ((this._CLUB_ACTIVITY_TYPE != value)) this._CLUB_ACTIVITY_TYPE = value; }
                }
                #endregion
            }
        }
    }
}
