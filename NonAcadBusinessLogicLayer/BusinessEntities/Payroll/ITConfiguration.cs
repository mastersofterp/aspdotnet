using System;
using System.Data;
using System.Web;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {
            public class ITConfiguration
            {
                #region Private Members

                private string _COLLEGENAME;

                private string _PANNO;

                private string _TANNO;

                private string _SECTION;

                private string _SIGNFNAME;

                private string _SIGNNAME;

                private string _SONOF;

                private string _DESIGNATION;

                private string _EMPBLOCKNO;

                private string _EMPBUILDINGNAME;

                private string _EMPROAD;

                private string _EMPAREA;

                private string _EMPCITY;

                private string _EMPSTATE;

                private string _EMPPINCODE;

                private string _EMPTELEPHONENO;

                private string _EMPEMAIL;

                private string _PERSONBLOCKNO;

                private string _PERSONBUILDINGNAME;

                private string _PERSONROAD;

                private string _PERSONAREA;

                private string _PERSONCITY;

                private string _PERSONSTATE;

                private string _PERSONPINCODE;

                private string _PERSONTELEPHONENO;

                private string _PERSONEMAIL;

                private string _BANKNAME;

                private string _BRANCHNAME;

                private string _BANKPLACE;

                private System.Nullable<System.DateTime> _PRINTDATE = DateTime.MinValue;

                private decimal _LIMIT;

                private decimal _BONDLIMIT;

                private decimal _FEMALELIMIT;

                private decimal _STDDEDMALE;

                private decimal _STDDEDFEMALE;

                private decimal _MEDICALEXEMPTION;

                private decimal _SURCHARGE;

                private decimal _MORETHAN;

                private decimal _TAXINCLIMIT;

                private decimal _EDUCESS;

                private decimal _TAXINCUPLIMIT;

                private decimal _ADDITIONALPT;

                private string _BSRCODE;

                private System.Nullable<System.DateTime> _FROMDATE = DateTime.MinValue;

                private System.Nullable<System.DateTime> _TODATE = DateTime.MinValue;

                private System.Nullable<decimal> _SHOWFORMNO16;

                private System.Nullable<decimal> _PROPOSEDSALARY;

                private System.Nullable<decimal> _PREVIOUSMONTHSALARY;

                private string _COLLEGE_CODE;

                private string _Q1;

                private string _Q2;

                private string _Q3;

                private string _Q4;

                private string _Q5;

                private string _ACK1;

                private string _ACK2;

                private string _ACK3;

                private string _ACK4;

                private string _ACK5;

                private decimal _HIGHEDUCESS;
                private int _ADDNPSGROSSFORIT;
                private decimal _TALIMIT;

                private decimal _TAXREBATE;

                public decimal TAXREBATE
                {
                    get { return _TAXREBATE; }
                    set { _TAXREBATE = value; }
                }
                private decimal _HOUSEAMTLIMIT;

                public decimal HOUSEAMTLIMIT
                {
                    get { return _HOUSEAMTLIMIT; }
                    set { _HOUSEAMTLIMIT = value; }
                }

                private decimal _CCDNPS80;

                public decimal CCDNPS80
                {
                    get { return _CCDNPS80; }
                    set { _CCDNPS80 = value; }
                }
                private decimal _RGESS_CCG;

                public decimal RGESS_CCG
                {
                    get { return _RGESS_CCG; }
                    set { _RGESS_CCG = value; }
                }

                private int _COLLEGENO;

                public int COLLEGENO
                {
                    get { return _COLLEGENO; }
                    set { _COLLEGENO = value; }
                }

                #endregion

                #region Public Properties

                public string COLLEGENAME
                {
                    get
                    {
                        return this._COLLEGENAME;
                    }
                    set
                    {
                        if ((this._COLLEGENAME != value))
                        {
                            this._COLLEGENAME = value;
                        }
                    }
                }

                public string PANNO
                {
                    get
                    {
                        return this._PANNO;
                    }
                    set
                    {
                        if ((this._PANNO != value))
                        {
                            this._PANNO = value;
                        }
                    }
                }

                public string TANNO
                {
                    get
                    {
                        return this._TANNO;
                    }
                    set
                    {
                        if ((this._TANNO != value))
                        {
                            this._TANNO = value;
                        }
                    }
                }

                public string SECTION
                {
                    get
                    {
                        return this._SECTION;
                    }
                    set
                    {
                        if ((this._SECTION != value))
                        {
                            this._SECTION = value;
                        }
                    }
                }

                public string SIGNNAME
                {
                    get
                    {
                        return this._SIGNNAME;
                    }
                    set
                    {
                        if ((this._SIGNNAME != value))
                        {
                            this._SIGNNAME = value;
                        }
                    }
                }

                public string SIGNFNAME
                {
                    get
                    {
                        return this._SIGNFNAME;
                    }
                    set
                    {
                        if ((this._SIGNFNAME != value))
                        {
                            this._SIGNFNAME = value;
                        }
                    }
                }

                public string SONOF
                {
                    get
                    {
                        return this._SONOF;
                    }
                    set
                    {
                        if ((this._SONOF != value))
                        {
                            this._SONOF = value;
                        }
                    }
                }

                public string DESIGNATION
                {
                    get
                    {
                        return this._DESIGNATION;
                    }
                    set
                    {
                        if ((this._DESIGNATION != value))
                        {
                            this._DESIGNATION = value;
                        }
                    }
                }

                public string EMPBLOCKNO
                {
                    get
                    {
                        return this._EMPBLOCKNO;
                    }
                    set
                    {
                        if ((this._EMPBLOCKNO != value))
                        {
                            this._EMPBLOCKNO = value;
                        }
                    }
                }

                public string EMPBUILDINGNAME
                {
                    get
                    {
                        return this._EMPBUILDINGNAME;
                    }
                    set
                    {
                        if ((this._EMPBUILDINGNAME != value))
                        {
                            this._EMPBUILDINGNAME = value;
                        }
                    }
                }

                public string EMPROAD
                {
                    get
                    {
                        return this._EMPROAD;
                    }
                    set
                    {
                        if ((this._EMPROAD != value))
                        {
                            this._EMPROAD = value;
                        }
                    }
                }

                public string EMPAREA
                {
                    get
                    {
                        return this._EMPAREA;
                    }
                    set
                    {
                        if ((this._EMPAREA != value))
                        {
                            this._EMPAREA = value;
                        }
                    }
                }

                public string EMPCITY
                {
                    get
                    {
                        return this._EMPCITY;
                    }
                    set
                    {
                        if ((this._EMPCITY != value))
                        {
                            this._EMPCITY = value;
                        }
                    }
                }

                public string EMPSTATE
                {
                    get
                    {
                        return this._EMPSTATE;
                    }
                    set
                    {
                        if ((this._EMPSTATE != value))
                        {
                            this._EMPSTATE = value;
                        }
                    }
                }

                public string EMPPINCODE
                {
                    get
                    {
                        return this._EMPPINCODE;
                    }
                    set
                    {
                        if ((this._EMPPINCODE != value))
                        {
                            this._EMPPINCODE = value;
                        }
                    }
                }

                public string EMPPHONENO
                {
                    get
                    {
                        return this._EMPTELEPHONENO;
                    }
                    set
                    {
                        if ((this._EMPTELEPHONENO != value))
                        {
                            this._EMPTELEPHONENO = value;
                        }
                    }
                }

                public string EMPEMAIL
                {
                    get
                    {
                        return this._EMPEMAIL;
                    }
                    set
                    {
                        if ((this._EMPEMAIL != value))
                        {
                            this._EMPEMAIL = value;
                        }
                    }
                }

                public string PERSONBLOCKNO
                {
                    get
                    {
                        return this._PERSONBLOCKNO;
                    }
                    set
                    {
                        if ((this._PERSONBLOCKNO != value))
                        {
                            this._PERSONBLOCKNO = value;
                        }
                    }
                }

                public string PERSONBUILDINGNAME
                {
                    get
                    {
                        return this._PERSONBUILDINGNAME;
                    }
                    set
                    {
                        if ((this._PERSONBUILDINGNAME != value))
                        {
                            this._PERSONBUILDINGNAME = value;
                        }
                    }
                }

                public string PERSONROAD
                {
                    get
                    {
                        return this._PERSONROAD;
                    }
                    set
                    {
                        if ((this._PERSONROAD != value))
                        {
                            this._PERSONROAD = value;
                        }
                    }
                }

                public string PERSONAREA
                {
                    get
                    {
                        return this._PERSONAREA;
                    }
                    set
                    {
                        if ((this._PERSONAREA != value))
                        {
                            this._PERSONAREA = value;
                        }
                    }
                }

                public string PERSONCITY
                {
                    get
                    {
                        return this._PERSONCITY;
                    }
                    set
                    {
                        if ((this._PERSONCITY != value))
                        {
                            this._PERSONCITY = value;
                        }
                    }
                }

                public string PERSONSTATE
                {
                    get
                    {
                        return this._PERSONSTATE;
                    }
                    set
                    {
                        if ((this._PERSONSTATE != value))
                        {
                            this._PERSONSTATE = value;
                        }
                    }
                }

                public string PERSONPINCODE
                {
                    get
                    {
                        return this._PERSONPINCODE;
                    }
                    set
                    {
                        if ((this._PERSONPINCODE != value))
                        {
                            this._PERSONPINCODE = value;
                        }
                    }
                }

                public string PERSONPHONENO
                {
                    get
                    {
                        return this._PERSONTELEPHONENO;
                    }
                    set
                    {
                        if ((this._PERSONTELEPHONENO != value))
                        {
                            this._PERSONTELEPHONENO = value;
                        }
                    }
                }

                public string PERSONEMAIL
                {
                    get
                    {
                        return this._PERSONEMAIL;
                    }
                    set
                    {
                        if ((this._PERSONEMAIL != value))
                        {
                            this._PERSONEMAIL = value;
                        }
                    }
                }

                public string BANKNAME
                {
                    get
                    {
                        return this._BANKNAME;
                    }
                    set
                    {
                        if ((this._BANKNAME != value))
                        {
                            this._BANKNAME = value;
                        }
                    }
                }

                public string BRANCHNAME
                {
                    get
                    {
                        return this._BRANCHNAME;
                    }
                    set
                    {
                        if ((this._BRANCHNAME != value))
                        {
                            this._BRANCHNAME = value;
                        }
                    }
                }

                public string BANKPLACE
                {
                    get
                    {
                        return this._BANKPLACE;
                    }
                    set
                    {
                        if ((this._BANKPLACE != value))
                        {
                            this._BANKPLACE = value;
                        }
                    }
                }

                public System.Nullable<System.DateTime> PRINDATE
                {
                    get
                    {
                        return this._PRINTDATE;
                    }
                    set
                    {
                        if ((this._PRINTDATE != value))
                        {
                            this._PRINTDATE = value;
                        }
                    }

                }

                public decimal LIMIT
                {
                    get
                    {
                        return this._LIMIT;
                    }
                    set
                    {
                        if ((this._LIMIT != value))
                        {
                            this._LIMIT = value;
                        }
                    }
                }

                public decimal BONDLIMIT
                {
                    get
                    {
                        return this._BONDLIMIT;
                    }
                    set
                    {
                        if ((this._BONDLIMIT != value))
                        {
                            this._BONDLIMIT = value;
                        }
                    }
                }

                public decimal FEMALELIMIT
                {
                    get
                    {
                        return this._FEMALELIMIT;
                    }
                    set
                    {
                        if ((this._FEMALELIMIT != value))
                        {
                            this._FEMALELIMIT = value;
                        }
                    }
                }

                public decimal STDDEDMALE
                {
                    get
                    {
                        return this._STDDEDMALE;
                    }
                    set
                    {
                        if ((this._STDDEDMALE != value))
                        {
                            this._STDDEDMALE = value;
                        }
                    }
                }

                public decimal STDDEDFEMALE
                {
                    get
                    {
                        return this._STDDEDFEMALE;
                    }
                    set
                    {
                        if ((this._STDDEDFEMALE != value))
                        {
                            this._STDDEDFEMALE = value;
                        }
                    }
                }

                public decimal MEDICALEXEMPTION
                {
                    get
                    {
                        return this._MEDICALEXEMPTION;
                    }
                    set
                    {
                        if ((this._MEDICALEXEMPTION != value))
                        {
                            this._MEDICALEXEMPTION = value;
                        }
                    }
                }

                public decimal SURCHARGE
                {
                    get
                    {
                        return this._SURCHARGE;
                    }
                    set
                    {
                        if ((this._SURCHARGE != value))
                        {
                            this._SURCHARGE = value;
                        }
                    }
                }

                public decimal MORETHAN
                {
                    get
                    {
                        return this._MORETHAN;
                    }
                    set
                    {
                        if ((this._MORETHAN != value))
                        {
                            this._MORETHAN = value;
                        }
                    }
                }

                public decimal TAXINC_LIMIT
                {
                    get
                    {
                        return this._TAXINCLIMIT;
                    }
                    set
                    {
                        if ((this._TAXINCLIMIT != value))
                        {
                            this._TAXINCLIMIT = value;
                        }
                    }
                }

                public decimal EDUCESS
                {
                    get
                    {
                        return this._EDUCESS;
                    }
                    set
                    {
                        if ((this._EDUCESS != value))
                        {
                            this._EDUCESS = value;
                        }
                    }
                }

                public decimal TAXINCUP_LIMIT
                {
                    get
                    {
                        return this._TAXINCUPLIMIT;
                    }
                    set
                    {
                        if ((this._TAXINCUPLIMIT != value))
                        {
                            this._TAXINCUPLIMIT = value;
                        }
                    }
                }

                public decimal ADDITIONALPT
                {
                    get
                    {
                        return this._ADDITIONALPT;
                    }
                    set
                    {
                        if ((this._ADDITIONALPT != value))
                        {
                            this._ADDITIONALPT = value;
                        }
                    }
                }

                public string BSRCODE
                {
                    get
                    {
                        return this._BSRCODE;
                    }
                    set
                    {
                        if ((this._BSRCODE != value))
                        {
                            this._BSRCODE = value;
                        }
                    }
                }

                public System.Nullable<System.DateTime> FROMDATE
                {
                    get
                    {
                        return this._FROMDATE;
                    }
                    set
                    {
                        if ((this._FROMDATE != value))
                        {
                            this._FROMDATE = value;
                        }
                    }

                }

                public System.Nullable<System.DateTime> TODATE
                {
                    get
                    {
                        return this._TODATE;
                    }
                    set
                    {
                        if ((this._TODATE != value))
                        {
                            this._TODATE = value;
                        }
                    }

                }

                public System.Nullable<decimal> SHOWFORMNO16
                {
                    get
                    {
                        return this._SHOWFORMNO16;
                    }
                    set
                    {
                        if ((this._SHOWFORMNO16 != value))
                        {
                            this._SHOWFORMNO16 = value;
                        }
                    }

                }

                public System.Nullable<decimal> PROPOSEDSALARY
                {
                    get
                    {
                        return this._PROPOSEDSALARY;
                    }
                    set
                    {
                        if ((this._PROPOSEDSALARY != value))
                        {
                            this._PROPOSEDSALARY = value;
                        }
                    }

                }

                public System.Nullable<decimal> PREVIOUSSALARY
                {
                    get
                    {
                        return this._PREVIOUSMONTHSALARY;
                    }
                    set
                    {
                        if ((this._PREVIOUSMONTHSALARY != value))
                        {
                            this._PREVIOUSMONTHSALARY = value;
                        }
                    }

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

                public string Q1
                {
                    get
                    {
                        return this._Q1;
                    }
                    set
                    {
                        if ((this._Q1 != value))
                        {
                            this._Q1 = value;
                        }
                    }
                }

                public string Q2
                {
                    get
                    {
                        return this._Q2;
                    }
                    set
                    {
                        if ((this._Q2 != value))
                        {
                            this._Q2 = value;
                        }
                    }
                }

                public string Q3
                {
                    get
                    {
                        return this._Q3;
                    }
                    set
                    {
                        if ((this._Q3 != value))
                        {
                            this._Q3 = value;
                        }
                    }
                }

                public string Q4
                {
                    get
                    {
                        return this._Q4;
                    }
                    set
                    {
                        if ((this._Q4 != value))
                        {
                            this._Q4 = value;
                        }
                    }
                }

                public string Q5
                {
                    get
                    {
                        return this._Q5;
                    }
                    set
                    {
                        if ((this._Q5 != value))
                        {
                            this._Q5 = value;
                        }
                    }
                }

                public string ACK1
                {
                    get
                    {
                        return this._ACK1;
                    }
                    set
                    {
                        if ((this._ACK1 != value))
                        {
                            this._ACK1 = value;
                        }
                    }
                }

                public string ACK2
                {
                    get
                    {
                        return this._ACK2;
                    }
                    set
                    {
                        if ((this._ACK2 != value))
                        {
                            this._ACK2 = value;
                        }
                    }
                }

                public string ACK3
                {
                    get
                    {
                        return this._ACK3;
                    }
                    set
                    {
                        if ((this._ACK3 != value))
                        {
                            this._ACK3 = value;
                        }
                    }
                }

                public string ACK4
                {
                    get
                    {
                        return this._ACK4;
                    }
                    set
                    {
                        if ((this._ACK4 != value))
                        {
                            this._ACK4 = value;
                        }
                    }
                }

                public string ACK5
                {
                    get
                    {
                        return this._ACK5;
                    }
                    set
                    {
                        if ((this._ACK5 != value))
                        {
                            this._ACK5 = value;
                        }
                    }
                }

                public decimal HIGHEDUCESS
                {
                    get
                    {
                        return this._HIGHEDUCESS;
                    }
                    set
                    {
                        if ((this._HIGHEDUCESS != value))
                        {
                            this._HIGHEDUCESS = value;
                        }
                    }
                }

                //_ADDNPSGROSSFORIT
                public int ADDNPSGROSSFORIT
                {
                    get
                    {
                        return this._ADDNPSGROSSFORIT;
                    }
                    set
                    {
                        if ((this._ADDNPSGROSSFORIT != value))
                        {
                            this._ADDNPSGROSSFORIT = value;
                        }
                    }
                }

                public decimal TALIMIT
                {
                    get
                    {
                        return this._TALIMIT;
                    }
                    set
                    {
                        if ((this._TALIMIT != value))
                        {
                            this._TALIMIT = value;
                        }
                    }
                }






                #endregion
            }
        }
    }
}
