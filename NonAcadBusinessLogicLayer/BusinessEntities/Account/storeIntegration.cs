using System;
using System.Data;
using System.Web;

namespace IITMS
{
    namespace UAIMS
    {

        namespace BusinessLayer.BusinessEntities
        {
            public class storeIntegration
            {
                private int _PNO;

                public int PNO
                {
                    get { return _PNO; }
                    set { _PNO = value; }
                }
                private string _PNAME;

                public string PNAME
                {
                    get { return _PNAME; }
                    set { _PNAME = value; }
                }
                private int _PARTY_NO;

                public int PARTY_NO
                {
                    get { return _PARTY_NO; }
                    set { _PARTY_NO = value; }
                }
                private int _PARTY_NO_EXPE;

                public int PARTY_NO_EXPE
                {
                    get { return _PARTY_NO_EXPE; }
                    set { _PARTY_NO_EXPE = value; }
                }

                private string fromDate;

                public string FromDate
                {
                    get { return fromDate; }
                    set { fromDate = value; }
                }
                private string toDate;

                public string ToDate
                {
                    get { return toDate; }
                    set { toDate = value; }
                }

                private string _college_code;

                public string College_code
                {
                    get { return _college_code; }
                    set { _college_code = value; }
                }
                private string _Net_Amount;

                public string Net_Amount
                {
                    get { return _Net_Amount; }
                    set { _Net_Amount = value; }
                }
                private int _UA_NO;

                public int UA_NO
                {
                    get { return _UA_NO; }
                    set { _UA_NO = value; }
                }

                private int _INVTRNO;

                public int INVTRNO
                {
                    get { return _INVTRNO; }
                    set { _INVTRNO = value; }
                }
                private string _DATABASE;

                public string DATABASE
                {
                    get { return _DATABASE; }
                    set { _DATABASE = value; }
                }
            }
        }
    }
}
