using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NonAcadBusinessLogicLayer.BusinessEntities.Account
{
    public class BillDetails
    {

        #region Private Members

        //Bill Details
        private int _BillDetailsId = 0;
        private int _Unit = 0;
        private int _Bill_Type = 0;
        private string _Bill_No = string.Empty;
        private string _Bill_Date = string.Empty;
        private string _Bill_Received_Date = string.Empty;
        private string _Month = string.Empty;
        private int _Creditor_Id = 0;
        private string _PAN_NO = string.Empty;
        private string _GST_NO = string.Empty;
        private string _Exp_Head = string.Empty;
        private int _Nature_Expense = 0;
        private string _Other_Nature_Expense = string.Empty;
        private double _Basic_Amt = 0;
        private string _Quantity = string.Empty;
        private double _Rate = 0;
        private double _Discount = 0;
        private double _Freight_Charge = 0;
        private int _GST_Type = 0;
        private double _SGST_PERCENT = 0;
        private double _CGST_PERCENT = 0;
        private double _IGST_PERCENT = 0;
        private double _GST_Amt = 0;
        private double _Total_Bill_Amt = 0;
        private double _TDS_Percent = 0;
        private double _TDS_Amt = 0;
        private double _Final_Bill_Amt = 0;
        private int _Party_No = 0;
        private int _Entered_Tally = 0;
        private string _Detail_Narration = string.Empty;
        private string _Tally_Narration = string.Empty;
        private int _Company_No = 0;
        private string _Company_Code = string.Empty;
        private int _User_Id = 0;

        //Payment Details
        private int _Payment_Id = 0;
        private int _Payment_Status = 0;
        private string _Payment_Date = string.Empty;
        private int _Payment_Mode = 0;
        private string _Instrument_No = string.Empty;
        private string _Instrument_Date = string.Empty;
        private double _Payment_Amt = 0;
        private int _College_Code = 0;

        #endregion

        #region Public Memebers

        //Bill Details
        public int BillDetailsId
        {
            get { return _BillDetailsId; }
            set { _BillDetailsId = value; }
        }
        public int Unit
        {
            get { return _Unit; }
            set { _Unit = value; }
        }
        public int Bill_Type
        {
            get { return _Bill_Type; }
            set { _Bill_Type = value; }
        }
        public string Bill_No
        {
            get { return _Bill_No; }
            set { _Bill_No = value; }
        }
        public string Bill_Date
        {
            get { return _Bill_Date; }
            set { _Bill_Date = value; }
        }
        public string Bill_Received_Date
        {
            get { return _Bill_Received_Date; }
            set { _Bill_Received_Date = value; }
        }
        public string Month
        {
            get { return _Month; }
            set { _Month = value; }
        }
        public int Creditor_Id
        {
            get { return _Creditor_Id; }
            set { _Creditor_Id = value; }
        }
        public string PAN_NO
        {
            get { return _PAN_NO; }
            set { _PAN_NO = value; }
        }
        public string GST_NO
        {
            get { return _GST_NO; }
            set { _GST_NO = value; }
        }
        public string Exp_Head
        {
            get { return _Exp_Head; }
            set { _Exp_Head = value; }
        }
        public int Nature_Expense
        {
            get { return _Nature_Expense; }
            set { _Nature_Expense = value; }
        }
        public string Other_Nature_Expense
        {
            get { return _Other_Nature_Expense; }
            set { _Other_Nature_Expense = value; }
        }
        public double Basic_Amt
        {
            get { return _Basic_Amt; }
            set { _Basic_Amt = value; }
        }
        public string Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }
        public double Rate
        {
            get { return _Rate; }
            set { _Rate = value; }
        }
        public double Discount
        {
            get { return _Discount; }
            set { _Discount = value; }
        }
        public double Freight_Charge
        {
            get { return _Freight_Charge; }
            set { _Freight_Charge = value; }
        }
        public int GST_Type
        {
            get { return _GST_Type; }
            set { _GST_Type = value; }
        }
        public double SGST_PERCENT
        {
            get { return _SGST_PERCENT; }
            set { _SGST_PERCENT = value; }
        }
        public double CGST_PERCENT
        {
            get { return _CGST_PERCENT; }
            set { _CGST_PERCENT = value; }
        }
        public double IGST_PERCENT
        {
            get { return _IGST_PERCENT; }
            set { _IGST_PERCENT = value; }
        }
        public double GST_Amt
        {
            get { return _GST_Amt; }
            set { _GST_Amt = value; }
        }
        public double Total_Bill_Amt
        {
            get { return _Total_Bill_Amt; }
            set { _Total_Bill_Amt = value; }
        }
        public double TDS_Percent
        {
            get { return _TDS_Percent; }
            set { _TDS_Percent = value; }
        }
        public double TDS_Amt
        {
            get { return _TDS_Amt; }
            set { _TDS_Amt = value; }
        }
        public double Final_Bill_Amt
        {
            get { return _Final_Bill_Amt; }
            set { _Final_Bill_Amt = value; }
        }
        public int Party_No
        {
            get { return _Party_No; }
            set { _Party_No = value; }
        }
        public int Entered_Tally
        {
            get { return _Entered_Tally; }
            set { _Entered_Tally = value; }
        }
        public string Detail_Narration
        {
            get { return _Detail_Narration; }
            set { _Detail_Narration = value; }
        }
        public string Tally_Narration
        {
            get { return _Tally_Narration; }
            set { _Tally_Narration = value; }
        }
        public int Company_No
        {
            get { return _Company_No; }
            set { _Company_No = value; }
        }
        public string Company_Code
        {
            get { return _Company_Code; }
            set { _Company_Code = value; }
        }
        public int User_Id
        {
            get { return _User_Id; }
            set { _User_Id = value; }
        }

        //Payment Details
        public int Payment_Id
        {
            get { return _Payment_Id; }
            set { _Payment_Id = value; }
        }
        public int Payment_Status
        {
            get { return _Payment_Status; }
            set { _Payment_Status = value; }
        }
        public string Payment_Date
        {
            get { return _Payment_Date; }
            set { _Payment_Date = value; }
        }
        public int Payment_Mode
        {
            get { return _Payment_Mode; }
            set { _Payment_Mode = value; }
        }
        public string Instrument_No
        {
            get { return _Instrument_No; }
            set { _Instrument_No = value; }
        }
        public string Instrument_Date
        {
            get { return _Instrument_Date; }
            set { _Instrument_Date = value; }
        }
        public double Payment_Amt
        {
            get { return _Payment_Amt; }
            set { _Payment_Amt = value; }
        }
        public int College_Code
        {
            get { return _College_Code; }
            set { _College_Code = value; }
        }

        #endregion
    }
}
