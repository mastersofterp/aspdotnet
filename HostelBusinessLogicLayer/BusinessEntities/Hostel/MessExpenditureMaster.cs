//======================================================================================
// PROJECT NAME  : UAIMS
// MODULE NAME   : HOSTEL
// PAGE NAME     : MessExpenditure
// CREATION DATE : 
// CREATED BY    : MRUNAL BANSOD
// MODIFIED DATE :
// MODIFIED DESC :
//======================================================================================
using System;
using System.Collections;

namespace IITMS.UAIMS.BusinessLayer.BusinessEntities
{
    public class MessExpenditureMaster
    {
        #region Private
        private int _TRNO = 0;
        private int _MESS_NO = 0;
        private DateTime _MONTH = DateTime.MinValue;
        private double _DIET = 0;
        private double _TOTAL_EXPENDITURE = 0;
        private double _F1 = 0;
        private double _F2 = 0;
        private double _F3 = 0;
        private double _F4 = 0;
        private double _F5 = 0;
        private double _F6 = 0;
        private double _F7 = 0;
        private double _F8 = 0;
        private double _F9 = 0;
        private double _F10 = 0;
        private double _F11 = 0;
        private double _F12 = 0;
        private double _F13 = 0;
        private double _F14 = 0;
        private double _F15 = 0;
        private double _F16 = 0;
        private double _F17 = 0;
        private double _F18 = 0;
        private double _F19 = 0;
        private double _F20 = 0;
        private double _F21 = 0;
        private double _F22 = 0;
        private double _F23 = 0;
        private double _F24 = 0;
        private double _F25 = 0;
        private double _F26 = 0;
        private double _F27 = 0;
        private double _F28 = 0;
        private double _F29 = 0;
        private double _F30 = 0;
        private double _F31 = 0;
        private double _F32 = 0;
        private double _F33 = 0;
        private double _F34 = 0;
        private double _F35 = 0;
        private double _F36 = 0;
        private double _F37 = 0;
        private double _F38 = 0;
        private double _F39 = 0;
        private double _F40 = 0;

        private string _USERID = string.Empty;
        private string _IPADDRESS = string.Empty;
        private string _MACADDRESS = string.Empty;
        private DateTime _AUDITDATE = DateTime.MinValue;
        private string _COLLEGE_CODE = string.Empty;

        //Mess Bill Calculation
        private int _idno = 0;
        private int _mess_no = 0;
        private int _session_no = 0;
        private int _hostel_no = 0;
        private int _month_no = 0;
        private int _attend_days = 0;
        private DateTime _bill_date = DateTime.MinValue;
        private decimal _total_expence = 0.0M;
        private decimal _total_balance = 0.0M;
        private decimal _total_bill = 0.0M;
        private DateTime _mdate = DateTime.MinValue;
        private int _ua_no = 0;
        #endregion

        #region Public
        public int TRNO
        {
            get { return _TRNO; }
            set { _TRNO = value; }
        }
        public int MESSNO
        {
            get { return _MESS_NO; }
            set { _MESS_NO = value; }
        }
        public DateTime MONTH
        {
            get { return _MONTH; }
            set { _MONTH = value; }
        }


        public double DIET
        {
            get { return _DIET; }
            set { _DIET = value; }
        }


        public double TOTALEXPENDITURE
        {
            get { return _TOTAL_EXPENDITURE; }
            set { _TOTAL_EXPENDITURE = value; }
        }


        public double F1
        {
            get { return _F1; }
            set { _F1 = value; }
        }

        public double F2
        {
            get { return _F2; }
            set { _F2 = value; }
        }

        public double F3
        {
            get { return _F3; }
            set { _F3 = value; }
        }


        public double F4
        {
            get { return _F4; }
            set { _F4 = value; }
        }

        public double F5
        {
            get { return _F5; }
            set { _F5 = value; }
        }


        public double F6
        {
            get { return _F6; }
            set { _F6 = value; }
        }


        public double F7
        {
            get { return _F7; }
            set { _F7 = value; }
        }

        public double F8
        {
            get { return _F8; }
            set { _F8 = value; }
        }

        public double F9
        {
            get { return _F9; }
            set { _F9 = value; }
        }


        public double F10
        {
            get { return _F10; }
            set { _F10 = value; }
        }


        public double F11
        {
            get { return _F11; }
            set { _F11 = value; }
        }


        public double F12
        {
            get { return _F12; }
            set { _F12 = value; }
        }


        public double F13
        {
            get { return _F13; }
            set { _F13 = value; }
        }


        public double F14
        {
            get { return _F14; }
            set { _F14 = value; }
        }


        public double F15
        {
            get { return _F15; }
            set { _F15 = value; }
        }


        public double F16
        {
            get { return _F16; }
            set { _F16 = value; }
        }


        public double F17
        {
            get { return _F17; }
            set { _F17 = value; }
        }


        public double F18
        {
            get { return _F18; }
            set { _F18 = value; }
        }


        public double F19
        {
            get { return _F19; }
            set { _F19 = value; }
        }


        public double F20
        {
            get { return _F20; }
            set { _F20 = value; }
        }


        public double F21
        {
            get { return _F21; }
            set { _F21 = value; }
        }


        public double F22
        {
            get { return _F22; }
            set { _F22 = value; }
        }


        public double F23
        {
            get { return _F23; }
            set { _F23 = value; }
        }


        public double F24
        {
            get { return _F24; }
            set { _F24 = value; }
        }


        public double F25
        {
            get { return _F25; }
            set { _F25 = value; }
        }


        public double F26
        {
            get { return _F26; }
            set { _F26 = value; }
        }

        public double F27
        {
            get { return _F27; }
            set { _F27 = value; }
        }


        public double F28
        {
            get { return _F28; }
            set { _F28 = value; }
        }


        public double F29
        {
            get { return _F29; }
            set { _F29 = value; }
        }


        public double F30
        {
            get { return _F30; }
            set { _F30 = value; }
        }



        public double F31
        {
            get { return _F31; }
            set { _F31 = value; }
        }

        public double F32
        {
            get { return _F32; }
            set { _F32 = value; }
        }


        public double F33
        {
            get { return _F33; }
            set { _F33 = value; }
        }


        public double F34
        {
            get { return _F34; }
            set { _F34 = value; }
        }

        public double F35
        {
            get { return _F35; }
            set { _F35 = value; }
        }


        public double F36
        {
            get { return _F36; }
            set { _F36 = value; }
        }


        public double F37
        {
            get { return _F37; }
            set { _F37 = value; }
        }


        public double F38
        {
            get { return _F38; }
            set { _F38 = value; }
        }


        public double F39
        {
            get { return _F39; }
            set { _F39 = value; }
        }


        public double F40
        {
            get { return _F40; }
            set { _F40 = value; }
        }

        public string USERID
        {
            get { return _USERID; }
            set { _USERID = value; }
        }

        public string IPADDRESS
        {
            get { return _IPADDRESS; }
            set { _IPADDRESS = value; }
        }

        public string MACADDRESS
        {
            get { return _MACADDRESS; }
            set { _MACADDRESS = value; }
        }


        public DateTime AUDITDATE
        {
            get { return _AUDITDATE; }
            set { _AUDITDATE = value; }
        }


        public string COLLEGE_CODE
        {
            get { return _COLLEGE_CODE; }
            set { _COLLEGE_CODE = value; }
        }

        //Mess Bill Calculation
        public int Idno
        {
            get { return _idno; }
            set { _idno = value; }
        }
        public int Mess_no
        {
            get { return _mess_no; }
            set { _mess_no = value; }
        }
        public int Session_no
        {
            get { return _session_no; }
            set { _session_no = value; }
        }
        public int Hostel_no
        {
            get { return _hostel_no; }
            set { _hostel_no = value; }
        }
        public int Month_no
        {
            get { return _month_no; }
            set { _month_no = value; }
        }
        public int Attend_days
        {
            get { return _attend_days; }
            set { _attend_days = value; }
        }
        public DateTime Bill_date
        {
            get { return _bill_date; }
            set { _bill_date = value; }
        }
        public decimal Total_expence
        {
            get { return _total_expence; }
            set { _total_expence = value; }
        }
        public decimal Total_balance
        {
            get { return _total_balance; }
            set { _total_balance = value; }
        }
        public decimal Total_bill
        {
            get { return _total_bill; }
            set { _total_bill = value; }
        }
        public DateTime Mdate
        {
            get { return _mdate; }
            set { _mdate = value; }
        }
        public int Ua_no
        {
            get { return _ua_no; }
            set { _ua_no = value; }
        }
        #endregion
    }
}
