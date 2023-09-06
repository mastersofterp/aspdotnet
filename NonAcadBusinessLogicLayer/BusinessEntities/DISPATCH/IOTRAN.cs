using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Web;
namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {
            public class IOTRAN
    {
        #region Private Members

        private int _IOTRANNO = 0;                                    // Transaction No
        private char _IOTYPE = ' ';                             // Transaction Type I=Inward O=Outward
        private DateTime _DEPTRECSENTDT = DateTime.MinValue;        // Department Recevied Date
        private DateTime _CENTRALRECSENTDT = DateTime.MinValue;     // Dispatch Received Date
        private DateTime _DEPTENTRYDT = DateTime.MinValue;          //Department Entry Date
        private DateTime _CENTRALENTRYDT = DateTime.MinValue;       //Dispatch Entry Date
        private string _OUTREFERENCENO = string.Empty;            //Outward Reference No. 
        private string _DEPTREFERENCENO = string.Empty;           // Department Reference No.
        private string _CENTRALREFERENCENO = string.Empty;          //Dispatch Reference No.
        private string _IOFROM = string.Empty;                    //Inward From
        private string _IOTO = string.Empty;                      //Outward To
        private int _FROMDEPT = 0;                             //outward from Department
        private int _TODEPT = 0;                               //Inward To Department
        private string _TOUSER = string.Empty;
        private string _ADDRESS = string.Empty;                   //Address of sender or Receiver
        private int _CITYNO = 0;                              //City 
        private string _PINCODE = string.Empty;                    //Pincode
        private int _LETTERTYPENO = 0;                           //Letter Type
        private string _SUBJECT = string.Empty;                   //Subject of Letter
        private double _WEIGHT = 0.00;                            //Weight of Letter
        private string _CHQDDNO = string.Empty;                   //Cheque No./DD NO
        private double _CHEQAMT = 0.00;                           // cheque/DD Amount 
        private DateTime _CHEQDT = DateTime.MinValue;               // Cheque/DD Date
        private string _BANKNAME = string.Empty;                  // Cheque/DD Bank
        private string _DEPTIONO = string.Empty;                  //Department Inward/Outward No
        private int _CENTRALIONO = 0;               //Dispatch Inward/Outward No
        private double _COST = 0.00;                              //Cost of Postage
        private double _EXTRACOST = 0.00;                         //Extra Cost Of Postage
        private int _POSTTYPENO = 0;                           //Post Type
        private string _SIGNBY = string.Empty;                    //Signed By
        private string _REMARK = string.Empty;                      //CC REMARK
        private int _SRNO = 0;                                      //PRIMARY KEY OF IO_CC_TRAN
        private string _COLLEGE_CODE = string.Empty;              //College Code 
        private string _CREATOR = string.Empty;                   //Creator 
        private DateTime _CREATED_DATE = DateTime.MinValue;         //Created Date 
        private string _DEPTREMARKS = string.Empty;                 //Department Remarks 
        private DateTime _MOV_DATE = DateTime.MinValue;
        private int _IDNO = 0;

        private string _ADDLINE = string.Empty;                   //Address line 2
        private int _STATENO = 0;                              //State
        private int _COUNTRYNO = 0;                              //Country  
        private int _DESIGNO = 0;    // designation no.
        private int _IN_TO_USER = 0; // user idno
        private string _RFID = string.Empty;
        private string _PEON = string.Empty;
        private string _TRACKING_NO = string.Empty;
        private int _CHEUQE_ID = 0;

        private int _NO_OF_PERSONS = 0;
        private double _TOTAL_COST = 0.00;
        private int _UNIT = 0;
        private DataTable _CHEQUE_DETAILS_TABLE;


        private int _USERTYPE = 0;
        private char _USERFLAG  ;


        

        #endregion


        #region Public Members
        public int USERTYPE
        {
            get { return _USERTYPE; }
            set { _USERTYPE = value; }

        }

        public char USERFLAG
        {
            get { return _USERFLAG; }
            set { _USERFLAG = value; }

        }

        public int UNIT
        {
            get { return _UNIT; }
            set { _UNIT = value; }
        }
        public double TOTAL_COST
        {
            get { return _TOTAL_COST; }
            set { _TOTAL_COST = value; }
        }
        public int NO_OF_PERSONS
        {
            get { return _NO_OF_PERSONS; }
            set { _NO_OF_PERSONS = value; }
        }
        public int CHEUQE_ID
        {
            get { return _CHEUQE_ID; }
            set { _CHEUQE_ID = value; }
        }
        public string TRACKING_NO
        {
            get { return _TRACKING_NO; }
            set { _TRACKING_NO = value; }
        }
        public int IOTRANNO
        {
            get { return _IOTRANNO; }
            set { _IOTRANNO = value; }
        }
        public char IOTYPE
        {
            get { return _IOTYPE; }
            set { _IOTYPE = value; }
        }
        public DateTime DEPTRECSENTDT
        {
            get { return _DEPTRECSENTDT; }
            set { _DEPTRECSENTDT = value; }
        }
        public DateTime CENTRALRECSENTDT
        {
            get { return _CENTRALRECSENTDT; }
            set { _CENTRALRECSENTDT = value; }
        }
        public DateTime DEPTENTRYDT
        {
            get { return _DEPTENTRYDT; }
            set { _DEPTENTRYDT = value; }
        }
        public DateTime CENTRALENTRYDT
        {
            get { return _CENTRALENTRYDT; }
            set { _CENTRALENTRYDT = value; }
        }
        public string OUTREFERENCENO
        {
            get { return _OUTREFERENCENO; }
            set { _OUTREFERENCENO = value; }
        }
        public string DEPTREFERENCENO
        {
            get { return _DEPTREFERENCENO; }
            set { _DEPTREFERENCENO = value; }
        }
        public string CENTRALREFERENCENO
        {
            get { return _CENTRALREFERENCENO; }
            set { _CENTRALREFERENCENO = value; }
        }
        public string IOFROM
        {
            get { return _IOFROM; }
            set { _IOFROM = value; }
        }
        public string IOTO
        {
            get { return _IOTO; }
            set { _IOTO = value; }
        }
        public int FROMDEPT
        {
            get { return _FROMDEPT; }
            set { _FROMDEPT = value; }
        }
        public int TODEPT
        {
            get { return _TODEPT; }
            set { _TODEPT = value; }
        }

        public string TOUSER
        {
            get { return _TOUSER; }
            set { _TOUSER = value; }
        }

        public string ADDRESS
        {
            get { return _ADDRESS; }
            set { _ADDRESS = value; }
        }
        public int CITYNO
        {
            get { return _CITYNO; }
            set { _CITYNO = value; }
        }
        public string PINCODE
        {
            get { return _PINCODE; }
            set { _PINCODE = value; }
        }
        public int LETTERTYPENO
        {
            get { return _LETTERTYPENO; }
            set { _LETTERTYPENO = value; }
        }
        public string SUBJECT
        {
            get { return _SUBJECT; }
            set { _SUBJECT = value; }
        }
        public double WEIGHT
        {
            get { return _WEIGHT; }
            set { _WEIGHT = value; }
        }
        public string CHQDDNO
        {
            get { return _CHQDDNO; }
            set { _CHQDDNO = value; }
        }
        public double CHEQAMT
        {
            get { return _CHEQAMT; }
            set { _CHEQAMT = value; }
        }
        public DateTime CHEQDT
        {
            get { return _CHEQDT; }
            set { _CHEQDT = value; }
        }
        public string BANKNAME
        {
            get { return _BANKNAME; }
            set { _BANKNAME = value; }
        }
        public string DEPTIONO
        {
            get { return _DEPTIONO; }
            set { _DEPTIONO = value; }
        }
        public int CENTRALIONO
        {
            get { return _CENTRALIONO; }
            set { _CENTRALIONO = value; }
        }
        public double COST
        {
            get { return _COST; }
            set { _COST = value; }
        }
        public double EXTRACOST
        {
            get { return _EXTRACOST; }
            set { _EXTRACOST = value; }
        }
        public int POSTTYPENO
        {
            get { return _POSTTYPENO; }
            set { _POSTTYPENO = value; }
        }

        public string SIGNBY
        {
            get { return _SIGNBY; }
            set { _SIGNBY = value; }
        }
        public int SRNO
        {
            get { return _SRNO; }
            set { _SRNO = value; }
        }

        public string REMARK
        {
            get { return _REMARK; }
            set { _REMARK = value; }
        }
        public string COLLEGE_CODE
        {
            get { return _COLLEGE_CODE; }
            set { _COLLEGE_CODE = value; }
        }
        public string CREATOR
        {
            get { return _CREATOR; }
            set { _CREATOR = value; }
        }
        public DateTime CREATED_DATE
        {
            get { return _CREATED_DATE; }
            set { _CREATED_DATE = value; }
        }
        public string DEPTREMARKS
        {
            get { return _DEPTREMARKS; }
            set { _DEPTREMARKS = value; }
        }

        public DateTime MOV_DATE
        {
            get { return _MOV_DATE; }
            set { _MOV_DATE = value; }
        }

        public int IDNO
        {
            get { return _IDNO; }
            set { _IDNO = value; }
        }

        public string ADDLINE
        {
            get { return _ADDLINE; }
            set { _ADDLINE = value; }
        }
        public int STATENO
        {
            get { return _STATENO; }
            set { _STATENO = value; }
        }
        public int COUNTRYNO
        {
            get { return _COUNTRYNO; }
            set { _COUNTRYNO = value; }

        }
        public int DESIGNO
        {
            get { return _DESIGNO; }
            set { _DESIGNO = value; }
        }
        public int IN_TO_USER
        {
            get { return _IN_TO_USER; }
            set { _IN_TO_USER = value; }
        }
        public string RFID
        {
            get { return _RFID; }
            set { _RFID = value; }
        }

        public string PEON
        {
            get { return _PEON; }
            set { _PEON = value; }
        }

        public DataTable CHEQUE_DETAILS_TABLE
        {
            get {return _CHEQUE_DETAILS_TABLE; }
            set { _CHEQUE_DETAILS_TABLE = value; }
        }
        #endregion

    }
        }
    }
}