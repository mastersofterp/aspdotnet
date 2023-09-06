using System;
using System.Data;
using System.Web;



namespace IITMS
{
    namespace UAIMS
    {

        namespace BusinessLayer.BusinessEntities
        {
            public class ChequePrintMaster
            {
                #region Private Members
                private string _PARTYNAME = string.Empty;
                private double _AMOUNT = 0;
                private string _CHECKDT = string.Empty;
                private string _CHECKNO = "0";
                private int _BANKNO = 0;
                private int _CTRNO = 0;
                private string _REMARK = string.Empty;
                private string _ACCNO = string.Empty;
                private string _BILLNO = string.Empty;
                private string _BILLDT = string.Empty;
                private int _CANCEL = 0;
                private string _USERNAME = string.Empty;
                private string _VNO = string.Empty;
                private string _VDT = string.Empty;
                private string _ADDRESS = string.Empty;
                private string _COPY1 = string.Empty;
                private string _COPY2 = string.Empty;
                private string _COPY3 = string.Empty;
                private string _REASON1 = string.Empty;
                private string _REASON2 = string.Empty;
                private string _REASON3 = string.Empty;
                private int _PRINTSTATUS = 0;
                private int _DEDSTATUS = 0;
                private double _DEDAMT = 0;
                private string _DEPT = string.Empty;
                private string _ACCNAME = string.Empty;
                private string _STAMP = string.Empty;
                private int _TRNO = 0;
                private string _PARTYACCOUNTNO = string.Empty;
                private int _PARTYNO = 0;
                private string _COMPANY_CODE = string.Empty;
                #endregion

                #region Public
                public string PARTYNAME
                {
                    get { return _PARTYNAME; }
                    set { _PARTYNAME = value; }
                }
                public double AMOUNT
                {
                    get { return _AMOUNT; }
                    set { _AMOUNT = value; }
                }
                public string CHECKDT
                {
                    get { return _CHECKDT; }
                    set { _CHECKDT = value; }
                }
                public string CHECKNO
                {
                    get { return _CHECKNO; }
                    set { _CHECKNO = value; }
                }
                public int BANKNO
                {
                    get { return _BANKNO; }
                    set { _BANKNO = value; }
                }
                public int CTRNO
                {
                    get { return _CTRNO; }
                    set { _CTRNO = value; }
                }
                public string REMARK
                {
                    get { return _REMARK; }
                    set { _REMARK = value; }
                }
                public string ACCNO
                {
                    get { return _ACCNO; }
                    set { _ACCNO = value; }
                }
                public string BILLNO
                {
                    get { return _BILLNO; }
                    set { _BILLNO = value; }
                }
                public string BILLDT
                {
                    get { return _BILLDT; }
                    set { _BILLDT = value; }
                }
                public int CANCEL
                {
                    get { return _CANCEL; }
                    set { _CANCEL = value; }
                }
                public string USERNAME
                {
                    get { return _USERNAME; }
                    set { _USERNAME = value; }
                }
                public string VNO
                {
                    get { return _VNO; }
                    set { _VNO = value; }
                }
                public string VDT
                {
                    get { return _VDT; }
                    set { _VDT = value; }
                }
                public string ADDRESS
                {
                    get { return _ADDRESS; }
                    set { _ADDRESS = value; }
                }
                public string COPY1
                {
                    get { return _COPY1; }
                    set { _COPY1 = value; }
                }
                public string COPY2
                {
                    get { return _COPY2; }
                    set { _COPY2 = value; }
                }
                public string COPY3
                {
                    get { return _COPY3; }
                    set { _COPY3 = value; }
                }
                public string REASON1
                {
                    get { return _REASON1; }
                    set { _REASON1 = value; }
                }
                public string REASON2
                {
                    get { return _REASON2; }
                    set { _REASON2 = value; }
                }
                public string REASON3
                {
                    get { return _REASON3; }
                    set { _REASON3 = value; }
                }
                public int PRINTSTATUS
                {
                    get { return _PRINTSTATUS; }
                    set { _PRINTSTATUS = value; }
                }
                public int DEDSTATUS
                {
                    get { return _DEDSTATUS; }
                    set { _DEDSTATUS = value; }
                }
                public double DEDAMT
                {
                    get { return _DEDAMT; }
                    set { _DEDAMT = value; }
                }
                public string DEPT
                {
                    get { return _DEPT; }
                    set { _DEPT = value; }
                }
                public string ACCNAME
                {
                    get { return _ACCNAME; }
                    set { _ACCNAME = value; }
                }
                public string STAMP
                {
                    get { return _STAMP; }
                    set { _STAMP = value; }
                }
                public int TRNO
                {
                    get { return _TRNO; }
                    set { _TRNO = value; }
                }
                public string PARTYACCOUNTNO
                {
                    get { return _PARTYACCOUNTNO; }
                    set { _PARTYACCOUNTNO = value; }
                }
                public int PARTYNO
                {
                    get { return _PARTYNO; }
                    set { _PARTYNO = value; }
                }
                public string COMPANY_CODE
                {
                    get { return _COMPANY_CODE; }
                    set { _COMPANY_CODE = value; }
                }
                #endregion
            }

        }//END: BusinessLayer.BusinessEntities

    }//END: UAIMS  

}//END: IITMS