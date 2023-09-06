using System;
using System.Data;
using System.Web;



namespace IITMS
{
    namespace UAIMS
    {

        namespace BusinessLayer.BusinessEntities
        {
            public class MainGroup
            {
                #region Private Members
                private int _mgrp_no = 0;
                private string _mgrp_name = string.Empty;
                private int _pr_no = 0;
                private bool _child = false;
                private int _fa_no = 0;
                private int _lno = 0;
                private string _sch = string.Empty;
                private string _acc_code = string.Empty;
                private string _college_code = string.Empty;
                private string _payment_type = string.Empty;
                #endregion

                #region Public
                public int Mgrp_No
                {
                    get { return _mgrp_no; }
                    set { _mgrp_no = value; }
                }
                public string Mgrp_Name
                {
                    get { return _mgrp_name; }
                    set { _mgrp_name = value; }
                }
                public int Pr_No
                {
                    get { return _pr_no; }
                    set { _pr_no = value; }
                }
                public bool Child
                {
                    get { return _child; }
                    set { _child = value; }
                }
                public int Fa_No
                {
                    get { return _fa_no; }
                    set { _fa_no = value; }
                }
                public int LNo
                {
                    get { return _lno; }
                    set { _lno = value; }
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
                public string Payment_type
                {
                    get { return _payment_type; }
                    set { _payment_type = value; }
                }
                #endregion
            }

        }//END: BusinessLayer.BusinessEntities

    }//END: UAIMS  

}//END: IITMS