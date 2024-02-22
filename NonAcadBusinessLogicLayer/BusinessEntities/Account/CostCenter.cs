using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.CompilerServices;

[assembly: SuppressIldasmAttribute()]
namespace IITMS
{
    namespace UAIMS
    {

        namespace BusinessLayer.BusinessEntities
        {
            public class CostCenter
            {
                private int _CATID;

                public int CATID
                {
                    get { return _CATID; }
                    set { _CATID = value; }
                }
                private string _CATEGORYNAME;

                public string CATEGORYNAME
                {
                    get { return _CATEGORYNAME; }
                    set { _CATEGORYNAME = value; }
                }
                private int _CC_ID;

                public int CC_ID
                {
                    get { return _CC_ID; }
                    set { _CC_ID = value; }
                }
                private string _CCNAME;

                public string CCNAME
                {
                    get { return _CCNAME; }
                    set { _CCNAME = value; }
                }

                private int _PARTY_NO;

                public int PARTY_NO
                {
                    get { return _PARTY_NO; }
                    set { _PARTY_NO = value; }
                }
                private int _PAYMENTTYPENO;

                public int PAYMENTTYPENO
                {
                    get { return _PAYMENTTYPENO; }
                    set { _PAYMENTTYPENO = value; }
                }
                private string _OPBALANCE;

                public string OPBALANCE
                {
                    get { return _OPBALANCE; }
                    set { _OPBALANCE = value; }
                }
                private string _STATUS;

                public string STATUS
                {
                    get { return _STATUS; }
                    set { _STATUS = value; }
                }
                private string _APPLICABLE;

                public string APPLICABLE
                {
                    get { return _APPLICABLE; }
                    set { _APPLICABLE = value; }
                }
                private int _VCHNO;

                public int VCHNO
                {
                    get { return _VCHNO; }
                    set { _VCHNO = value; }
                }
                private string _VCHTYPE;

                public string VCHTYPE
                {
                    get { return _VCHTYPE; }
                    set { _VCHTYPE = value; }
                }
                private DateTime _CCVHDATE = DateTime.MinValue;
                
                public DateTime CCVHDATE
                {
                    get { return _CCVHDATE; }
                    set { _CCVHDATE = value; }
                }

                private double _AMOUNT;

                public double AMOUNT
                {
                    get { return _AMOUNT; }
                    set { _AMOUNT = value; }
                }
            }
        }
    }
}
