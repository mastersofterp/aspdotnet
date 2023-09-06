using System;
using System.Data;
using System.Web;



namespace IITMS
{
    namespace UAIMS
    {

        namespace BusinessLayer.BusinessEntities
        {
            public class ReceiptPayment
            {
                #region Private Members
                private int _rp_no = 0;
                private string _rp_name = string.Empty;
                private int _rph_no = 0;
                private int _rpprno = 0;
                private int _lno = 0;
                private bool _modified = false;
                private string _sch = string.Empty;
                private string _acc_code = string.Empty;
                private string _college_code = string.Empty;

                //Receipt Payment Group Report
                private int _level_no = 0;
                private int _party_no = 0;
                private string _party_name = string.Empty;
                private string _oType = string.Empty;
                private string _clType = string.Empty;
                private double _oBalance = 0.0;
                private double _clBalance = 0.0;
                private double _debit = 0;
                private double _credit = 0;
                private double _prvBalance = 0.0;
                private string _prvType = string.Empty;
                private int _gr_no = 0;
                private string _gr_name = string.Empty;
                private int _fGroup = 0;

                #endregion

                #region Public
                public int Rp_No
                {
                    get { return _rp_no; }
                    set { _rp_no = value; }
                }
                public string Rp_Name
                {
                    get { return _rp_name; }
                    set { _rp_name = value; }
                }
                public int Rph_No
                {
                    get { return _rph_no; }
                    set { _rph_no = value; }
                }
                public int RpprNo
                {
                    get { return _rpprno; }
                    set { _rpprno = value; }
                }
                public int LNo
                {
                    get { return _lno; }
                    set { _lno = value; }
                }
                public bool Modified
                {
                    get { return _modified; }
                    set { _modified = value; }
                }
                public string Sch
                {
                    get { return _sch; }
                    set { _sch = value; }
                }
                public string Acc_Code
                {
                    get { return _acc_code; }
                    set { _acc_code = value; }
                }
                public string College_Code
                {
                    get { return _college_code; }
                    set { _college_code = value; }
                }

                //Receipt Payment Group
                public int Level_No
                {
                    get { return _level_no ; }
                    set { _level_no  = value; }
                }
                public int Party_No
                {
                    get { return _party_no ; }
                    set { _party_no  = value; }
                }
                public string Party_Name
                {
                    get { return _party_name ; }
                    set { _party_name  = value; }
                }
                public string OType
                {
                    get { return _oType ; }
                    set { _oType  = value; }
                }
                public string ClType
                {
                    get { return _clType; }
                    set { _clType = value; }
                }
                public double  OBalanace
                {
                    get { return _oBalance ; }
                    set { _oBalance = value; }
                }
                public double CLBalanace
                {
                    get { return _clBalance; }
                    set { _clBalance = value; }
                }
                public double Debit
                {
                    get { return _debit; }
                    set { _debit = value; }
                }
                public double Credit
                {
                    get { return _credit ; }
                    set { _credit  = value; }
                }
                public double PrvBalanace
                {
                    get { return _prvBalance ; }
                    set { _prvBalance = value; }
                }
                public string PrvType
                {
                    get { return _prvType; }
                    set { _prvType   = value; }
                }
                public int Gr_No
                {
                    get { return _gr_no; }
                    set { _gr_no = value; }
                }
                public string GrName
                {
                    get { return _gr_name; }
                    set { _gr_name = value; }
                }
                public int FGroup
                {
                    get { return _fGroup; }
                    set { _fGroup = value; }
                }
                #endregion
            }

        }//END: BusinessLayer.BusinessEntities

    }//END: UAIMS  

}//END: IITMS