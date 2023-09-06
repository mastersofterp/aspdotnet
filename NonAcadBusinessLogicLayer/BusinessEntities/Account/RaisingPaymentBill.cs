using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IITMS
{
    namespace UAIMS
    {

        namespace BusinessLayer.BusinessEntities
        {
            public class RaisingPaymentBill
            {
                #region Private Members
                private int _RAISE_PAY_NO;
                private int _SERIAL_NO;
                private string _ACCOUNT;
                private int _DEPT_ID;
                private string _APPROVAL_NO;
                private DateTime _APPROVAL_DATE;
                private string _APPROVED_BY;
                private string _SUPPLIER_NAME;
                private string _PAYEE_NAME_ADDRESS;
                private string _NATURE_SERVICE;
                private string _GSTIN_NO;
                private double _BILL_AMT;
                private int _ISTDS;
                private int _SECTION_NO;
                private double _TDS_PERCENT;
                private double _TDS_AMT;
                private double _TDS_ON_AMT;
                private double _GST_AMT;
                private double _TOTAL_BILL_AMT;
                private double _TOTAL_TDS_AMT;
                private string _COMPANY_CODE;
                private string _STATUS;
                private string _PAN_NO;
                private string _REMARK;
                private int _COMPANY_NO;
                private string _AUTHORITY_PATH;
                private int _BUDGET_NO;
                private int _LEDGER_NO;
                private int _BILL_TYPE;
                private double _NET_AMT;
                private string _INVOICEDATE;
                private string _INVOICENO;

                private string _TransDate;
                private int _TDSLedgerId;


                private int _BankLedgerId;
                private string _Narration;

                //Added by vijay andoju 
                private int _ISIGST;
                private int _IGST_SECTION;
                private double _IGST_PER;
                private double _IGST_AMT;

                private int _ISGST;

                private int _CGST_SECTION;
                private double _CGST_PER;
                private double _CGST_AMT;

                private int _SGST_SECTION;
                private double _SGST_PER;
                private double _SGST_AMT;

                //Added by Gopal Anthati
                private char _PAYMENT_MODE;

                private int _TDSonGSTLedgerId;
                private double _TDS_ON_GST_AMT;
                private double _TDSGST_ON_AMT;
                private int _ISTDSONGST;
                private int _TDSGST_SECTION_NO;
                private double _TDSGST_PERCENT;

                private int _TDSonCGSTLedgerId;
                private int _TDSonSGSTLedgerId;
                private int _SecurityLedgerId;
                private double _TDS_ON_CGST_AMT;
                private double _TDS_ON_SGST_AMT;
                private double _TDSCGST_ON_AMT;
                private double _TDSSGST_ON_AMT;
                private int _ISTDSONCGSTSGST;


                private int _TDSCGST_SECTION_NO;
                private int _TDSSGST_SECTION_NO;
                private double _TDSCGST_PERCENT;
                private double _TDSSGST_PERCENT;

                private int _ISSECURITY;
                private double _SECURITY_AMT;
                private double _SECURITY_PER;
                private string _FILEPATH = string.Empty;  //Added by Vidisha on 13-05-2021 for multiple bill upload

                private int _ProjectId;
                private int _ProjectSubId;
                private int _EXPENSE_LEDGER_NO;



                //Added by TANU on 15-12-2021 for TAG USER
                private int _SUPPLIER_ID;
                private int _PROVIDER_TYPE;
                private int _PAYEE_NATURE;

                private int _BILL_ID;

                #endregion

                #region Public Member

                public int SUPPLIER_ID
                {
                    get { return _SUPPLIER_ID; }
                    set { _SUPPLIER_ID = value; }
                }
                public int PROVIDER_TYPE
                {
                    get { return _PROVIDER_TYPE; }
                    set { _PROVIDER_TYPE = value; }
                }
                public int PAYEE_NATURE
                {
                    get { return _PAYEE_NATURE; }
                    set { _PAYEE_NATURE = value; }
                }





                public int EXPENSE_LEDGER_NO
                {
                    get { return _EXPENSE_LEDGER_NO; }
                    set { _EXPENSE_LEDGER_NO = value; }
                }

                public int ProjectId
                {
                    get { return _ProjectId; }
                    set { _ProjectId = value; }
                }

                public int ProjectSubId
                {
                    get { return _ProjectSubId; }
                    set { _ProjectSubId = value; }
                }

                public int ISSECURITY
                {
                    get { return _ISSECURITY; }
                    set { _ISSECURITY = value; }
                }

                public double SECURITY_AMT
                {
                    get { return _SECURITY_AMT; }
                    set { _SECURITY_AMT = value; }
                }

                public double SECURITY_PER
                {
                    get { return _SECURITY_PER; }
                    set { _SECURITY_PER = value; }
                }

                public double TDS_ON_GST_AMT
                {
                    get { return _TDS_ON_GST_AMT; }
                    set { _TDS_ON_GST_AMT = value; }
                }
                public double TDS_ON_CGST_AMT
                {
                    get { return _TDS_ON_CGST_AMT; }
                    set { _TDS_ON_CGST_AMT = value; }
                }
                public double TDS_ON_SGST_AMT
                {
                    get { return _TDS_ON_SGST_AMT; }
                    set { _TDS_ON_SGST_AMT = value; }
                }
                public double TDSGST_ON_AMT
                {
                    get { return _TDSGST_ON_AMT; }
                    set { _TDSGST_ON_AMT = value; }
                }
                public double TDSCGST_ON_AMT
                {
                    get { return _TDSCGST_ON_AMT; }
                    set { _TDSCGST_ON_AMT = value; }
                }
                public double TDSSGST_ON_AMT
                {
                    get { return _TDSSGST_ON_AMT; }
                    set { _TDSSGST_ON_AMT = value; }
                }
                public int ISTDSONGST
                {
                    get { return _ISTDSONGST; }
                    set { _ISTDSONGST = value; }
                }
                public int ISTDSONCGSTSGST
                {
                    get { return _ISTDSONCGSTSGST; }
                    set { _ISTDSONCGSTSGST = value; }
                }

                public int TDSGST_SECTION_NO
                {
                    get { return _TDSGST_SECTION_NO; }
                    set { _TDSGST_SECTION_NO = value; }
                }
                public int TDSCGST_SECTION_NO
                {
                    get { return _TDSCGST_SECTION_NO; }
                    set { _TDSCGST_SECTION_NO = value; }
                }
                public int TDSSGST_SECTION_NO
                {
                    get { return _TDSSGST_SECTION_NO; }
                    set { _TDSSGST_SECTION_NO = value; }
                }
                public double TDSGST_PERCENT
                {
                    get { return _TDSGST_PERCENT; }
                    set { _TDSGST_PERCENT = value; }
                }
                public double TDSCGST_PERCENT
                {
                    get { return _TDSCGST_PERCENT; }
                    set { _TDSCGST_PERCENT = value; }
                }
                public double TDSSGST_PERCENT
                {
                    get { return _TDSSGST_PERCENT; }
                    set { _TDSSGST_PERCENT = value; }
                }
                public char PAYMENT_MODE
                {
                    get { return _PAYMENT_MODE; }
                    set { _PAYMENT_MODE = value; }
                }
                public int TDSonGSTLedgerId
                {
                    get { return _TDSonGSTLedgerId; }
                    set { _TDSonGSTLedgerId = value; }
                }
                public int TDSonCGSTLedgerId
                {
                    get { return _TDSonCGSTLedgerId; }
                    set { _TDSonCGSTLedgerId = value; }
                }
                public int TDSonSGSTLedgerId
                {
                    get { return _TDSonSGSTLedgerId; }
                    set { _TDSonSGSTLedgerId = value; }
                }
                public int SecurityLedgerId
                {
                    get { return _SecurityLedgerId; }
                    set { _SecurityLedgerId = value; }
                }

                public int ISGST
                {
                    get { return _ISGST; }
                    set { _ISGST = value; }
                }



                public int ISIGST
                {
                    get { return _ISIGST; }
                    set { _ISIGST = value; }
                }
                public int IGST_SECTION
                {
                    get { return _IGST_SECTION; }
                    set { _IGST_SECTION = value; }
                }
                public int SGST_SECTION
                {
                    get { return _SGST_SECTION; }
                    set { _SGST_SECTION = value; }
                }
                public int CGST_SECTION
                {
                    get { return _CGST_SECTION; }
                    set { _CGST_SECTION = value; }
                }

                public double SGST_PER
                {
                    get { return _SGST_PER; }
                    set { _SGST_PER = value; }
                }
                public double CGST_PER
                {
                    get { return _CGST_PER; }
                    set { _CGST_PER = value; }
                }
                public double IGST_PER
                {
                    get { return _IGST_PER; }
                    set { _IGST_PER = value; }
                }
                public double SGST_AMT
                {
                    get { return _SGST_AMT; }
                    set { _SGST_AMT = value; }
                }
                public double CGST_AMT
                {
                    get { return _CGST_AMT; }
                    set { _CGST_AMT = value; }
                }

                public double IGST_AMT
                {
                    get { return _IGST_AMT; }
                    set { _IGST_AMT = value; }
                }





                public int RAISE_PAY_NO
                {
                    get { return _RAISE_PAY_NO; }
                    set { _RAISE_PAY_NO = value; }
                }
                public int SERIAL_NO
                {
                    get { return _SERIAL_NO; }
                    set { _SERIAL_NO = value; }
                }
                public string ACCOUNT
                {
                    get { return _ACCOUNT; }
                    set { _ACCOUNT = value; }
                }
                public int DEPT_ID
                {
                    get { return _DEPT_ID; }
                    set { _DEPT_ID = value; }
                }
                public string APPROVAL_NO
                {
                    get { return _APPROVAL_NO; }
                    set { _APPROVAL_NO = value; }
                }
                public DateTime APPROVAL_DATE
                {
                    get { return _APPROVAL_DATE; }
                    set { _APPROVAL_DATE = value; }
                }
                public string APPROVED_BY
                {
                    get { return _APPROVED_BY; }
                    set { _APPROVED_BY = value; }
                }
                public string SUPPLIER_NAME
                {
                    get { return _SUPPLIER_NAME; }
                    set { _SUPPLIER_NAME = value; }
                }
                public string PAYEE_NAME_ADDRESS
                {
                    get { return _PAYEE_NAME_ADDRESS; }
                    set { _PAYEE_NAME_ADDRESS = value; }
                }
                public string NATURE_SERVICE
                {
                    get { return _NATURE_SERVICE; }
                    set { _NATURE_SERVICE = value; }
                }
                public string GSTIN_NO
                {
                    get { return _GSTIN_NO; }
                    set { _GSTIN_NO = value; }
                }
                public double BILL_AMT
                {
                    get { return _BILL_AMT; }
                    set { _BILL_AMT = value; }
                }
                public int ISTDS
                {
                    get { return _ISTDS; }
                    set { _ISTDS = value; }
                }
                public int SECTION_NO
                {
                    get { return _SECTION_NO; }
                    set { _SECTION_NO = value; }
                }
                public double TDS_PERCENT
                {
                    get { return _TDS_PERCENT; }
                    set { _TDS_PERCENT = value; }
                }
                public double TDS_AMT
                {
                    get { return _TDS_AMT; }
                    set { _TDS_AMT = value; }
                }
                public double TDS_ON_AMT
                {
                    get { return _TDS_ON_AMT; }
                    set { _TDS_ON_AMT = value; }
                }
                public double GST_AMT
                {
                    get { return _GST_AMT; }
                    set { _GST_AMT = value; }
                }
                public double TOTAL_BILL_AMT
                {
                    get { return _TOTAL_BILL_AMT; }
                    set { _TOTAL_BILL_AMT = value; }
                }
                public double TOTAL_TDS_AMT
                {
                    get { return _TOTAL_TDS_AMT; }
                    set { _TOTAL_TDS_AMT = value; }
                }
                public string COMPANY_CODE
                {
                    get { return _COMPANY_CODE; }
                    set { _COMPANY_CODE = value; }
                }
                public string STATUS
                {
                    get { return STATUS; }
                    set { _STATUS = value; }
                }
                public string PAN_NO
                {
                    get { return _PAN_NO; }
                    set { _PAN_NO = value; }
                }
                public string REMARK
                {
                    get { return _REMARK; }
                    set { _REMARK = value; }
                }
                public int COMPANY_NO
                {
                    get { return _COMPANY_NO; }
                    set { _COMPANY_NO = value; }
                }
                public string AUTHORITY_PATH
                {
                    get { return _AUTHORITY_PATH; }
                    set { _AUTHORITY_PATH = value; }
                }
                public int BUDGET_NO
                {
                    get { return _BUDGET_NO; }
                    set { _BUDGET_NO = value; }
                }
                public int LEDGER_NO
                {
                    get { return _LEDGER_NO; }
                    set { _LEDGER_NO = value; }
                }
                public int BILL_TYPE
                {
                    get { return _BILL_TYPE; }
                    set { _BILL_TYPE = value; }
                }
                public double NET_AMT
                {
                    get { return _NET_AMT; }
                    set { _NET_AMT = value; }
                }
                public string INVOICEDATE
                {
                    get { return _INVOICEDATE; }
                    set { _INVOICEDATE = value; }
                }
                public string INVOICENO
                {
                    get { return _INVOICENO; }
                    set { _INVOICENO = value; }
                }

                public string TransDate
                {
                    get { return _TransDate; }
                    set { _TransDate = value; }
                }
                public int TDSLedgerId
                {
                    get { return _TDSLedgerId; }
                    set { _TDSLedgerId = value; }
                }


                public int BankLedgerId
                {
                    get { return _BankLedgerId; }
                    set { _BankLedgerId = value; }
                }

                public string Narration
                {
                    get { return _Narration; }
                    set { _Narration = value; }
                }
                public string FILEPATH
                {
                    get { return this._FILEPATH; }
                    set { _FILEPATH = value; }
                }

                public int BILL_ID
                {
                    get { return _BILL_ID; }
                    set { _BILL_ID = value; }
                }
                #endregion

            }
        }
    }
}
