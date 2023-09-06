using System;
using System.Data;
using System.Web;

namespace IITMS
{
    namespace UAIMS
    {

        namespace BusinessLayer.BusinessEntities
        {
            public class Depreciation
            {
                private int _Party_No;

                public int Party_No
                {
                    get { return _Party_No; }
                    set { _Party_No = value; }
                }
                private int _Rate;

                public int Rate
                {
                    get { return _Rate; }
                    set { _Rate = value; }
                }
                private int _opBal;

                public int OpBal
                {
                    get { return _opBal; }
                    set { _opBal = value; }
                }
                private DateTime _FromDate;

                public DateTime FromDate
                {
                    get { return _FromDate; }
                    set { _FromDate = value; }
                }
                private DateTime _ToDate;

                public DateTime ToDate
                {
                    get { return _ToDate; }
                    set { _ToDate = value; }
                }
                private DateTime _SecondDate;

                public DateTime SecondDate
                {
                    get { return _SecondDate; }
                    set { _SecondDate = value; }
                }
            }
        }
    }
}
