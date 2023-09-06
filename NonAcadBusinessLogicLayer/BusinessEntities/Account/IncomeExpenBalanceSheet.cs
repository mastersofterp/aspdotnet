using System;
using System.Data;
using System.Web;



namespace IITMS
{
    namespace UAIMS
    {

        namespace BusinessLayer.BusinessEntities
        {
           public class IncomeExpenBalanceSheet
            {
                private int _Sch_id;

                public int Sch_id
                {
                    get { return _Sch_id; }
                    set { _Sch_id = value; }
                }


                private string _SchduleName;

                public string SchduleName
                {
                    get { return _SchduleName; }
                    set { _SchduleName = value; }
                }
                private string _Schtype;

                public string Schtype
                {
                    get { return _Schtype; }
                    set { _Schtype = value; }
                }
                private string _BS_IE;

                public string BS_IE
                {
                    get { return _BS_IE; }
                    set { _BS_IE = value; }
                }
                private int _Position;

                public int Position
                {
                    get { return _Position; }
                    set { _Position = value; }
                }
                private string _LedgerName;

                public string LedgerName
                {
                    get { return _LedgerName; }
                    set { _LedgerName = value; }
                }
                private double _Amount_Cr;

                public double Amount_Cr
                {
                    get { return _Amount_Cr; }
                    set { _Amount_Cr = value; }
                }
                private double _amount_Dr;

                public double Amount_Dr
                {
                    get { return _amount_Dr; }
                    set { _amount_Dr = value; }
                }
                private string _Status;

                public string Status
                {
                    get { return _Status; }
                    set { _Status = value; }
                }
                private DateTime _AuditDate;

                public DateTime AuditDate
                {
                    get { return _AuditDate; }
                    set { _AuditDate = value; }
                }
                private string _User_Id;

                public string User_Id
                {
                    get { return _User_Id; }
                    set { _User_Id = value; }
                }
                private string _FinancialYr;

                public string FinancialYr
                {
                    get { return _FinancialYr; }
                    set { _FinancialYr = value; }
                }
                private int _LedgerId;

                public int LedgerId
                {
                    get { return _LedgerId; }
                    set { _LedgerId = value; }
                }

                private int _Party_NO;

                public int Party_NO
                {
                    get { return _Party_NO; }
                    set { _Party_NO = value; }
                }

                private string _College_Code;

                public string College_Code
                {
                    get { return _College_Code; }
                    set { _College_Code = value; }
                }

            }
        }
    }
}
