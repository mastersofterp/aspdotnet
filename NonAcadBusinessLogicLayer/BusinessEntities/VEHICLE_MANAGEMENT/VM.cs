using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {
            public class VM
            {


                #region Vehicle Master

                private int _VIDNO;
                private string _VNAME;


                private string _MAKE;
                private string _MODEL;
                private DateTime _PURCHASEDT;
                private string _VENDORNAME;
                private string _VENDORADD;
                private DateTime _REGDT;
                private string _RCBOOKNO;
                private string _ENGINENO;
                private string _CHASISNO;
                private string _REGNO;
                private string _YROFMAKE;
                private string _COKOR;
                private string _STATPERMIT;
                private string _ALLINDIAPERMIT;
                private decimal _KM_RATE;
                private decimal _DRIVE_TA;
                private decimal _PERKM;
                private decimal _WAITCHARGE;
                private int _LOGNO;
                private int _RCVALIDITY;    // BY MRUNAL
                private DateTime _RCVALIDITYDT;
                private DateTime _PUCDT;
                private string _VEHICLE_NUMBER = string.Empty;

                private int _Status = 0;


                #region Public Members

                public int Status
                {
                    get { return _Status; }
                    set { _Status = value; }
                }
                public int RCVALIDITY         // BY MRUNAL             
                {
                    get { return _RCVALIDITY; }
                    set { _RCVALIDITY = value; }
                }
                public DateTime RCVALIDITYDT
                {
                    get { return _RCVALIDITYDT; }
                    set { _RCVALIDITYDT = value; }
                }
                public DateTime PUCDT
                {
                    get { return _PUCDT; }
                    set { _PUCDT = value; }
                }
                public int LOGNO
                {
                    get { return _LOGNO; }
                    set { _LOGNO = value; }
                }
                public string VNAME
                {
                    get { return _VNAME; }
                    set { _VNAME = value; }
                }
                public decimal WAITCHARGE
                {
                    get { return _WAITCHARGE; }
                    set { _WAITCHARGE = value; }
                }

                public decimal PERKM
                {
                    get { return _PERKM; }
                    set { _PERKM = value; }
                }

                public decimal DRIVE_TA
                {
                    get { return _DRIVE_TA; }
                    set { _DRIVE_TA = value; }
                }


                public decimal KM_RATE
                {
                    get { return _KM_RATE; }
                    set { _KM_RATE = value; }
                }

                public string ALLINDIAPERMIT
                {
                    get { return _ALLINDIAPERMIT; }
                    set { _ALLINDIAPERMIT = value; }
                }

                public string STATPERMIT
                {
                    get { return _STATPERMIT; }
                    set { _STATPERMIT = value; }
                }

                public string COKOR
                {
                    get { return _COKOR; }
                    set { _COKOR = value; }
                }

                public string YROFMAKE
                {
                    get { return _YROFMAKE; }
                    set { _YROFMAKE = value; }
                }

                public string REGNO
                {
                    get { return _REGNO; }
                    set { _REGNO = value; }
                }

                public string CHASISNO
                {
                    get { return _CHASISNO; }
                    set { _CHASISNO = value; }
                }

                public string ENGINENO
                {
                    get { return _ENGINENO; }
                    set { _ENGINENO = value; }
                }

                public string RCBOOKNO
                {
                    get { return _RCBOOKNO; }
                    set { _RCBOOKNO = value; }
                }

                public DateTime REGDT
                {
                    get { return _REGDT; }
                    set { _REGDT = value; }
                }

                public string VENDORADD
                {
                    get { return _VENDORADD; }
                    set { _VENDORADD = value; }
                }

                public string VENDORNAME
                {
                    get { return _VENDORNAME; }
                    set { _VENDORNAME = value; }
                }

                public DateTime PURCHASEDT
                {
                    get { return _PURCHASEDT; }
                    set { _PURCHASEDT = value; }
                }


                public string MODEL
                {
                    get { return _MODEL; }
                    set { _MODEL = value; }
                }




                public string MAKE
                {
                    get { return _MAKE; }
                    set { _MAKE = value; }
                }

                public int VIDNO
                {
                    get { return _VIDNO; }
                    set { _VIDNO = value; }
                }

                public string VEHICLE_NUMBER
                {
                    get { return _VEHICLE_NUMBER; }
                    set { _VEHICLE_NUMBER = value; }
                }

                #endregion Public Members

                #endregion

                #region Suppiler Master
                private int _SUPPILER_ID = 0;
                private string _SUPPILER_NAME = string.Empty;
                private string _CONTACT_ADDRESS = string.Empty;
                private string _CONTACT_NUMBER = string.Empty;
                private string _CONTACT_PERSON = string.Empty;
                private Boolean _IS_ACTIVE;

                #region public member
                public int SUPPILER_ID
                {
                    get { return _SUPPILER_ID; }
                    set { _SUPPILER_ID = value; }
                }

                public string SUPPILER_NAME
                {
                    get { return _SUPPILER_NAME; }
                    set { _SUPPILER_NAME = value; }
                }

                public string CONTACT_ADDRESS
                {
                    get { return _CONTACT_ADDRESS; }
                    set { _CONTACT_ADDRESS = value; }
                }

                public string CONTACT_NUMBER
                {
                    get { return _CONTACT_NUMBER; }
                    set { _CONTACT_NUMBER = value; }
                }
                public string CONTACT_PERSON
                {
                    get { return _CONTACT_PERSON; }
                    set { _CONTACT_PERSON = value; }
                }
                public Boolean IS_ACTIVE
                {
                    get { return _IS_ACTIVE; }
                    set { _IS_ACTIVE = value; }
                }
                #endregion

                #endregion

                #region Vehicle Hire Master
                private int _VEHICLE_ID = 0;
                private string _VEHICLE_NAME = string.Empty;
                private string _FROM_LOCATION = string.Empty;
                private string _TO_LOCATION = string.Empty;
                private int _HIRE_TYPE = 0;
                private decimal _HIRE_RATE = 0;

                #region public member
                public int HIRE_TYPE
                {
                    get { return _HIRE_TYPE; }
                    set { _HIRE_TYPE = value; }
                }
                public decimal HIRE_RATE
                {
                    get { return _HIRE_RATE; }
                    set { _HIRE_RATE = value; }
                }
                public int VEHICLE_ID
                {
                    get { return _VEHICLE_ID; }
                    set { _VEHICLE_ID = value; }
                }
                public string VEHICLE_NAME
                {
                    get { return _VEHICLE_NAME; }
                    set { _VEHICLE_NAME = value; }
                }

                public string FROM_LOCATION
                {
                    get { return _FROM_LOCATION; }
                    set { _FROM_LOCATION = value; }
                }

                public string TO_LOCATION
                {
                    get { return _TO_LOCATION; }
                    set { _TO_LOCATION = value; }
                }

                #endregion

                #endregion

                #region Vehicle Bill Entry
                private int _BILL_ID = 0;
                private Double _BILL_AMOUNT = Double.MinValue;
                private Double _TOUR_TOTAL_AMOUNT = Double.MinValue;
                private DateTime _BILL_FROM_DATE = DateTime.MinValue;
                private DateTime _BILL_TO_DATE = DateTime.MinValue;
                private Double _HIKE_PRICE = Double.MinValue;
                private string _HIRE_FOR = String.Empty;
                private string _FROM_TIME = String.Empty;
                private string _TO_TIME = String.Empty;
                private string _HIRED_BY = String.Empty;
                private string _TOUR_PURPOSE = String.Empty;
                private string _VISIT_PLACE = String.Empty;
                private string _ROUTE_FROM = String.Empty;
                private string _ROUTE_TO = String.Empty;
                private Double _EXTRA_AMOUNT = Double.MinValue;
                private Double _EXTRA_DISTANCE_AMOUNT = Double.MinValue;
                private Double _EXTRA_TIME_AMOUNT = Double.MinValue;
                private Double _PAID_AMOUNT = Double.MinValue;
                private Double _BALANCE_AMOUNT = Double.MinValue;
                private int _TRIP_TYPE = 0;
                private DateTime _AUDIT_DATE = DateTime.MinValue;

                #region public member
                public int BILL_ID
                {
                    get { return _BILL_ID; }
                    set { _BILL_ID = value; }
                }
                public Double BILL_AMOUNT
                {
                    get { return _BILL_AMOUNT; }
                    set { _BILL_AMOUNT = value; }
                }
                public Double TOUR_TOTAL_AMOUNT
                {
                    get { return _TOUR_TOTAL_AMOUNT; }
                    set { _TOUR_TOTAL_AMOUNT = value; }
                }
                public DateTime BILL_FROM_DATE
                {
                    get { return _BILL_FROM_DATE; }
                    set { _BILL_FROM_DATE = value; }
                }
                public DateTime BILL_TO_DATE
                {
                    get { return _BILL_TO_DATE; }
                    set { _BILL_TO_DATE = value; }

                }
                public Double HIKE_PRICE
                {
                    get { return _HIKE_PRICE; }
                    set { _HIKE_PRICE = value; }
                }
                public string HIRE_FOR
                {
                    get { return _HIRE_FOR; }
                    set { _HIRE_FOR = value; }
                }
                public string FROM_TIME
                {
                    get { return _FROM_TIME; }
                    set { _FROM_TIME = value; }
                }
                public string TO_TIME
                {
                    get { return _TO_TIME; }
                    set { _TO_TIME = value; }
                }
                public string HIRED_BY
                {
                    get { return _HIRED_BY; }
                    set { _HIRED_BY = value; }
                }
                public string TOUR_PURPOSE
                {
                    get { return _TOUR_PURPOSE; }
                    set { _TOUR_PURPOSE = value; }

                }
                public string VISIT_PLACE
                {
                    get { return _VISIT_PLACE; }
                    set { _VISIT_PLACE = value; }

                }
                public Double EXTRA_AMOUNT
                {
                    get { return _EXTRA_AMOUNT; }
                    set { _EXTRA_AMOUNT = value; }
                }

                public Double EXTRA_DISTANCE_AMOUNT
                {
                    get { return _EXTRA_DISTANCE_AMOUNT; }
                    set { _EXTRA_DISTANCE_AMOUNT = value; }
                }
                public Double EXTRA_TIME_AMOUNT
                {
                    get { return _EXTRA_TIME_AMOUNT; }
                    set { _EXTRA_TIME_AMOUNT = value; }
                }
                public Double PAID_AMOUNT
                {
                    get { return _PAID_AMOUNT; }
                    set { _PAID_AMOUNT = value; }
                }
                public Double BALANCE_AMOUNT
                {
                    get { return _BALANCE_AMOUNT; }
                    set { _BALANCE_AMOUNT = value; }
                }
                public int TRIP_TYPE
                {
                    get { return _TRIP_TYPE; }
                    set { _TRIP_TYPE = value; }
                }


                public DateTime AUDIT_DATE
                {
                    get { return _AUDIT_DATE; }
                    set { _AUDIT_DATE = value; }
                }
                public string ROUTE_FROM
                {
                    get { return _ROUTE_FROM; }
                    set { _ROUTE_FROM = value; }
                }
                public string ROUTE_TO
                {
                    get { return _ROUTE_TO; }
                    set { _ROUTE_TO = value; }
                }

                #endregion

                #endregion

                #region Vehicle Daily Attendance Entry
                private int _ATTENDANCE_ID = 0;
                private DateTime _TRAVELLING_DATE = DateTime.MinValue;
                private Boolean _ATTENDANCE_MARK;
                private int _DRIVER_ID = 0;
                private Boolean _DRIVER_TA_APPLY;
                private Double _DRIVER_TA_AMOUNT = Double.MinValue;
                private Double _BETA = Double.MinValue;
                private Double _TOTAL_AMOUNT = Double.MinValue;
                private DataTable _VEHICLE_ATTENDANCE_DT;

                #region public member

                public DataTable VEHICLE_ATTENDANCE_DT
                {
                    get { return _VEHICLE_ATTENDANCE_DT; }
                    set { _VEHICLE_ATTENDANCE_DT = value; }
                }

                public int ATTENDANCE_ID
                {
                    get { return _ATTENDANCE_ID; }
                    set { _ATTENDANCE_ID = value; }
                }
                public DateTime TRAVELLING_DATE
                {
                    get { return _TRAVELLING_DATE; }
                    set { _TRAVELLING_DATE = value; }
                }
                public Boolean ATTENDANCE_MARK
                {
                    get { return _ATTENDANCE_MARK; }
                    set { _ATTENDANCE_MARK = value; }
                }
                public int DRIVER_ID
                {
                    get { return _DRIVER_ID; }
                    set { _DRIVER_ID = value; }
                }



                public Boolean DRIVER_TA_APPLY
                {
                    get { return _DRIVER_TA_APPLY; }
                    set { _DRIVER_TA_APPLY = value; }
                }


                public Double DRIVER_TA_AMOUNT
                {
                    get { return _DRIVER_TA_AMOUNT; }
                    set { _DRIVER_TA_AMOUNT = value; }
                }

                public Double BETA
                {
                    get { return _BETA; }
                    set { _BETA = value; }
                }

                public Double TOTAL_AMOUNT
                {
                    get { return _TOTAL_AMOUNT; }
                    set { _TOTAL_AMOUNT = value; }
                }



                #endregion

                #endregion

                #region  Driver Master
                private int _DNO = 0;
                private string _DNAME = string.Empty;
                private string _DPHONE = string.Empty;
                private string _DADD1 = string.Empty;
                private string _DADD2 = string.Empty;
                private string _D_DRIVING_LICENCE_TYPE = string.Empty;
                private string _D_DRIVING_LICENCE_NO = string.Empty;
                private DateTime _D_DRIVING_LICENCE_FROM_DATE = DateTime.MinValue;
                private DateTime _D_DRIVING_LICENCE_EXPIRY_DATE = DateTime.MinValue;
                private int _D_CATEGORY = 0;
                private int _DRI_CON_TYPE = 0;

                #region public member
                public int D_CATEGORY
                {
                    get { return _D_CATEGORY; }
                    set { _D_CATEGORY = value; }
                }
                public int DRI_CON_TYPE
                {
                    get { return _DRI_CON_TYPE; }
                    set { _DRI_CON_TYPE = value; }
                }

                public string DADD2
                {
                    get { return _DADD2; }
                    set { _DADD2 = value; }
                }

                public string DADD1
                {
                    get { return _DADD1; }
                    set { _DADD1 = value; }
                }

                public string DPHONE
                {
                    get { return _DPHONE; }
                    set { _DPHONE = value; }
                }

                public string DNAME
                {
                    get { return _DNAME; }
                    set { _DNAME = value; }
                }

                public int DNO
                {
                    get { return _DNO; }
                    set { _DNO = value; }
                }

                public string D_DRIVING_LICENCE_TYPE
                {
                    get { return _D_DRIVING_LICENCE_TYPE; }
                    set { _D_DRIVING_LICENCE_TYPE = value; }
                }

                public string D_DRIVING_LICENCE_NO
                {
                    get { return _D_DRIVING_LICENCE_NO; }
                    set { _D_DRIVING_LICENCE_NO = value; }
                }

                public DateTime D_DRIVING_LICENCE_FROM_DATE
                {
                    get { return _D_DRIVING_LICENCE_FROM_DATE; }
                    set { _D_DRIVING_LICENCE_FROM_DATE = value; }
                }

                public DateTime D_DRIVING_LICENCE_EXPIRY_DATE
                {
                    get { return _D_DRIVING_LICENCE_EXPIRY_DATE; }
                    set { _D_DRIVING_LICENCE_EXPIRY_DATE = value; }
                }
                #endregion

                #endregion

                #region Fitness
                private int _FID;
                private string _FITNO;
                private DateTime _FENTRYDT;
                private DateTime _FDATE;
                private DateTime _TDATE;

                #region public
                public DateTime TDATE
                {
                    get { return _TDATE; }
                    set { _TDATE = value; }
                }

                public DateTime FDATE
                {
                    get { return _FDATE; }
                    set { _FDATE = value; }
                }

                public DateTime FENTRYDT
                {
                    get { return _FENTRYDT; }
                    set { _FENTRYDT = value; }
                }

                public string FITNO
                {
                    get { return _FITNO; }
                    set { _FITNO = value; }
                }

                public int FID
                {
                    get { return _FID; }
                    set { _FID = value; }
                }
                #endregion
                #endregion

                #region Insurance
                private int _INSIDNO;
                private string _INSCOMPANY;
                private string _POLICYNO;
                private string _VEHICLECAT; //BY MRUNAL
                private string _AGENTSECPHONE; // BY MRUNAL

                private DateTime _INSDT;
                private DateTime _INSENDDT;
                private decimal _PREMIUM;
                private string _AGENTNAME;
                private int _AGENTNO;


                private string _AGENTPHONE;
                private decimal _NCB;
                private decimal _CLAIMAMT;




                private DateTime _CLAIMDT;


                #region PUBLIC
                public string AGENTSECPHONE // MRUNAL
                {
                    get { return _AGENTSECPHONE; }
                    set { _AGENTSECPHONE = value; }
                }
                public string VEHICLECAT // BY MRUNAL
                {
                    get { return _VEHICLECAT; }
                    set { _VEHICLECAT = value; }
                }
                public DateTime CLAIMDT
                {
                    get { return _CLAIMDT; }
                    set { _CLAIMDT = value; }
                }
                public int AGENTNO
                {
                    get { return _AGENTNO; }
                    set { _AGENTNO = value; }
                }

                public decimal CLAIMAMT
                {
                    get { return _CLAIMAMT; }
                    set { _CLAIMAMT = value; }
                }

                public decimal NCB
                {
                    get { return _NCB; }
                    set { _NCB = value; }
                }

                public string AGENTPHONE
                {
                    get { return _AGENTPHONE; }
                    set { _AGENTPHONE = value; }
                }

                public string AGENTNAME
                {
                    get { return _AGENTNAME; }
                    set { _AGENTNAME = value; }
                }

                public decimal PREMIUM
                {
                    get { return _PREMIUM; }
                    set { _PREMIUM = value; }
                }
                public string POLICYNO
                {
                    get { return _POLICYNO; }
                    set { _POLICYNO = value; }
                }



                public DateTime INSENDDT
                {
                    get { return _INSENDDT; }
                    set { _INSENDDT = value; }
                }


                public DateTime INSDT
                {
                    get { return _INSDT; }
                    set { _INSDT = value; }
                }



                public string INSCOMPANY
                {
                    get { return _INSCOMPANY; }
                    set { _INSCOMPANY = value; }
                }

                public int INSIDNO
                {
                    get { return _INSIDNO; }
                    set { _INSIDNO = value; }
                }
                #endregion
                #endregion

                #region Road Tax
                private int _RTAXID;
                private string _RYR;
                private DateTime _RFDATE;
                private DateTime _RTDATE;
                private decimal _RAMT;
                private string _RECNO;
                private DateTime _RPAIDDT;

                #region PUBLIC

                public DateTime RFDATE
                {
                    get { return _RFDATE; }
                    set { _RFDATE = value; }
                }

                public DateTime RTDATE
                {
                    get { return _RTDATE; }
                    set { _RTDATE = value; }
                }

                public DateTime RPAIDDT
                {
                    get { return _RPAIDDT; }
                    set { _RPAIDDT = value; }
                }

                public string RECNO
                {
                    get { return _RECNO; }
                    set { _RECNO = value; }
                }

                public decimal RAMT
                {
                    get { return _RAMT; }
                    set { _RAMT = value; }
                }



                public string RYR
                {
                    get { return _RYR; }
                    set { _RYR = value; }
                }

                public int RTAXID
                {
                    get { return _RTAXID; }
                    set { _RTAXID = value; }
                }
                #endregion
                #endregion


                #region Service Master
                private int _SIDNO;
                private string _SBILLNO;
                private decimal _SBILLAMT;
                private char _SPAIDBY;


                private DateTime? _SPAIDDT = null;


                private string _SCHQNO;
                private string _SCHQBANK;
                private DateTime? _SCHQDT = null;
                private DateTime? _WSINDT = null;
                private DateTime? _WSOUTDT = null;
                private DateTime? _WSINTIME;
                private DateTime? _WSOUTTIME;
                private string _REMARK;
                private DateTime? _BILLDT = null;
                private DateTime? _NEXTDT = null;
                private string _ORDNO;

                private string _TRANSCATIONNO;
                private DateTime? _TRANSFERDT = null;

                #region

                public string ORDNO
                {
                    get { return _ORDNO; }
                    set { _ORDNO = value; }
                }

                public DateTime? NEXTDT
                {
                    get { return _NEXTDT; }
                    set { _NEXTDT = value; }
                }

                public DateTime? BILLDT
                {
                    get { return _BILLDT; }
                    set { _BILLDT = value; }
                }

                public string REMARK
                {
                    get { return _REMARK; }
                    set { _REMARK = value; }
                }

                public DateTime? WSOUTTIME
                {
                    get { return _WSOUTTIME; }
                    set { _WSOUTTIME = value; }
                }
                public DateTime? WSINTIME
                {
                    get { return _WSINTIME; }
                    set { _WSINTIME = value; }
                }

                public DateTime? WSOUTDT
                {
                    get { return _WSOUTDT; }
                    set { _WSOUTDT = value; }
                }


                public DateTime? WSINDT
                {
                    get { return _WSINDT; }
                    set { _WSINDT = value; }
                }
                public char SPAIDBY
                {
                    get { return _SPAIDBY; }
                    set { _SPAIDBY = value; }
                }

                public DateTime? SPAIDDT
                {
                    get { return _SPAIDDT; }
                    set { _SPAIDDT = value; }
                }
                public DateTime? SCHQDT
                {
                    get { return _SCHQDT; }
                    set { _SCHQDT = value; }
                }

                public string SCHQBANK
                {
                    get { return _SCHQBANK; }
                    set { _SCHQBANK = value; }
                }

                public string SCHQNO
                {
                    get { return _SCHQNO; }
                    set { _SCHQNO = value; }
                }

                public decimal SBILLAMT
                {
                    get { return _SBILLAMT; }
                    set { _SBILLAMT = value; }
                }

                public string SBILLNO
                {
                    get { return _SBILLNO; }
                    set { _SBILLNO = value; }
                }


                public int SIDNO
                {
                    get { return _SIDNO; }
                    set { _SIDNO = value; }
                }
                public string TRANSCATIONNO
                {
                    get { return _TRANSCATIONNO; }
                    set { _TRANSCATIONNO = value; }
                }

                public DateTime? TRANSFERDT
                {
                    get { return _TRANSFERDT; }
                    set { _TRANSFERDT = value; }
                }

                #endregion
                #endregion

                #region Service Tran
                private int _STIDNO;
                private string _ITEM;
                private int _QTY;
                private decimal _STAMT;
                private char _STATUS;

                #region

                public char STATUS
                {
                    get { return _STATUS; }
                    set { _STATUS = value; }
                }

                public decimal STAMT
                {
                    get { return _STAMT; }
                    set { _STAMT = value; }
                }

                public int QTY
                {
                    get { return _QTY; }
                    set { _QTY = value; }
                }

                public string ITEM
                {
                    get { return _ITEM; }
                    set { _ITEM = value; }
                }

                public int STIDNO
                {
                    get { return _STIDNO; }
                    set { _STIDNO = value; }
                }

                #endregion
                #endregion

                #region Work Shop
                private int _WSNO;
                private string _WORKSHOP;
                private string _WADD1;
                private string _WADD2 = string.Empty;
                private string _WPHONE;
                private string _WPERSONNAME;

                #region
                public string WADD2
                {
                    get { return _WADD2; }
                    set { _WADD2 = value; }
                }
                public string WPHONE
                {
                    get { return _WPHONE; }
                    set { _WPHONE = value; }
                }
                public string WADD1
                {
                    get { return _WADD1; }
                    set { _WADD1 = value; }
                }
                public string WPERSONNAME
                {
                    get { return _WPERSONNAME; }
                    set { _WPERSONNAME = value; }
                }

                public string WORKSHOP
                {
                    get { return _WORKSHOP; }
                    set { _WORKSHOP = value; }
                }

                public int WSNO
                {
                    get { return _WSNO; }
                    set { _WSNO = value; }
                }

                #endregion
                #endregion


                #region Trip_Type

                private int _TTID;
                private string _TRIPTYPENAME;
                private Boolean _CHARGEABLE;
                private Boolean _ACTIVE;
                private int _COLLEGE_CODE;

                #region Public Members
                public int TTID
                {
                    get { return _TTID; }
                    set { _TTID = value; }
                }

                public string TRIPTYPENAME
                {
                    get { return _TRIPTYPENAME; }
                    set { _TRIPTYPENAME = value; }
                }
                public Boolean CHARGEABLE
                {
                    get { return _CHARGEABLE; }
                    set { _CHARGEABLE = value; }
                }
                public Boolean ACTIVE
                {
                    get { return _ACTIVE; }
                    set { _ACTIVE = value; }
                }
                public int COLLEGE_CODE
                {
                    get { return _COLLEGE_CODE; }
                    set { _COLLEGE_CODE = value; }
                }
                #endregion

                #endregion



                #region Log Book
                private int _LLOGBOOKID;
                private int _LVIDNO;
                private int _LTTID;
                private DateTime _LTOURDATE;
                private DateTime _LDTIMEL;
                private string _LDTIME;
                private DateTime _LATIME;
                private string _LFROMLOCATION;
                private string _LTOLOCATION;
                private string _LREMARK;

                private double _LSTARTMETERREADING;
                private double _LENDMETERREADING;
                private double _LTotalKm;

                private double _LWAITINGCHARGES;
                private double _LHIRERATEPERKM;
                private double _LDRIVERTA;
                private double _LTOTALAMT;
                private string _LTOURDETAILS;
                private int _LDNO;
                private string _PASSENGERNAME;

                #region Public Members

                public int LLOGBOOKID
                {
                    get { return _LLOGBOOKID; }
                    set { _LLOGBOOKID = value; }
                }
                public int LVIDNO
                {
                    get { return _LVIDNO; }
                    set { _LVIDNO = value; }
                }
                public int LTTID
                {
                    get { return _LTTID; }
                    set { _LTTID = value; }
                }

                public DateTime LTOURDATE
                {
                    get { return _LTOURDATE; }
                    set { _LTOURDATE = value; }
                }

                public DateTime LDTIMEL
                {
                    get { return _LDTIMEL; }
                    set { _LDTIMEL = value; }
                }
                public string LDTIME
                {
                    get { return _LDTIME; }
                    set { _LDTIME = value; }
                }

                public DateTime LATIME
                {
                    get { return _LATIME; }
                    set { _LATIME = value; }
                }

                public string LFROMLOCATION
                {
                    get { return _LFROMLOCATION; }
                    set { _LFROMLOCATION = value; }
                }
                public string LTOLOCATION
                {
                    get { return _LTOLOCATION; }
                    set { _LTOLOCATION = value; }
                }

                public string LREMARK
                {
                    get { return _REMARK; }
                    set { _REMARK = value; }
                }

                public double LSTARTMETERREADING
                {
                    get { return _LSTARTMETERREADING; }
                    set { _LSTARTMETERREADING = value; }
                }

                public double LENDMETERREADING
                {
                    get { return _LENDMETERREADING; }
                    set { _LENDMETERREADING = value; }
                }


                public double LTOTALKM
                {
                    get { return _LTotalKm; }
                    set { _LTotalKm = value; }
                }

                public double LWAITINGCHARGES
                {
                    get { return _LWAITINGCHARGES; }
                    set { _LWAITINGCHARGES = value; }
                }
                public double LHIRERATEPERKM
                {
                    get { return _LHIRERATEPERKM; }
                    set { _LHIRERATEPERKM = value; }
                }
                public double LDRIVERTA
                {
                    get { return _LDRIVERTA; }
                    set { _LDRIVERTA = value; }
                }
                public double LTOTALAMT
                {
                    get { return _LTOTALAMT; }
                    set { _LTOTALAMT = value; }
                }

                public string LTOURDETAILS
                {
                    get { return _LTOURDETAILS; }
                    set { _LTOURDETAILS = value; }
                }


                public int LDNO
                {
                    get { return _LDNO; }
                    set { _LDNO = value; }
                }

                public string PASSENGERNAME
                {
                    get { return _PASSENGERNAME; }
                    set { _PASSENGERNAME = value; }
                }









                #endregion
                #endregion

                #region Fuel Entry

                private int _FFEID;
                private int _FVIDNO;
                private int _FDNO;
                private DateTime _FFDATE;
                private string _FBILLNO;
                private DateTime _FBILLDATE;
                private string _FFUELTYPE;
                private double _FQTY;
                private double _FAMOUNT;
                private string _FREMARK;
                private string _FLOGNO;
                private double _FCMREADING;
                private double _FLMREADING;
                private int _FCOLLEGECODE;
               // private string _METER_READING;  //-----29-03-2023
                private double _METER_READING;  //-----29-03-2023
                private double _END_METER_READING;
                private double _NO_OF_KMS;
                private double _MILEGE;
                private double _MILEGE_AMOUNT;

                private string _VEHICLETYPES;

                public decimal _ORIGINAL_RATE;

                public int _ISSUE_TYPE;  //----29-03-2023
                public decimal _AVELABLE_QTY; //----29-03-2023  ---08-05-2023
                public string _COUPON_NO; //----29-03-2023
                public decimal _TOTAL_AMOUNT1; //----29-03-2023

                public int _DEPARTMENT; //----29-03-2023
                public DateTime _DATE_OF_WITHDRAWAL; //----29-03-2023
                public int _DIESEL_REQUESTER; //----29-03-2023
                public int _APPROVER; //----29-03-2023
                public string _PURPOSE_OF_WITHDRAWAL; //----29-03-2023
                public string _UploadRequestLetter = string.Empty;

                

                #region public Members
                public string VEHICLETYPES     // BY TANU
                {
                    get { return _VEHICLETYPES; }
                    set { _VEHICLETYPES = value; }
                }
                public int FFEID
                {
                    get { return _FFEID; }
                    set { _FFEID = value; }
                }
                public int FVIDNO
                {
                    get { return _FVIDNO; }
                    set { _FVIDNO = value; }
                }
                public int FDNO
                {
                    get { return _FDNO; }
                    set { _FDNO = value; }
                }
                public DateTime FFDATE
                {
                    get { return _FFDATE; }
                    set { _FFDATE = value; }
                }
                public string FBILLNO
                {
                    get { return _FBILLNO; }
                    set { _FBILLNO = value; }
                }
                public DateTime FBILLDATE
                {
                    get { return _FBILLDATE; }
                    set { _FBILLDATE = value; }
                }
                public string FFUELTYPE
                {
                    get { return _FFUELTYPE; }
                    set { _FFUELTYPE = value; }
                }
                public double FQTY
                {
                    get { return _FQTY; }
                    set { _FQTY = value; }
                }
                public double FAMOUNT
                {
                    get { return _FAMOUNT; }
                    set { _FAMOUNT = value; }
                }
                public string FREMARK
                {
                    get { return _FREMARK; }
                    set { _FREMARK = value; }
                }
                public string FLOGNO
                {
                    get { return _FLOGNO; }
                    set { _FLOGNO = value; }
                }
                public double FCMREADING
                {
                    get { return _FCMREADING; }
                    set { _FCMREADING = value; }
                }
                public double FLMREADING
                {
                    get { return _FLMREADING; }
                    set { _FLMREADING = value; }
                }
                public int FCOLLEGECODE
                {
                    get { return _FCOLLEGECODE; }
                    set { _FCOLLEGECODE = value; }
                }
                //public string METER_READING
                //{
                //    get { return _METER_READING; }
                //    set { _METER_READING = value; }
                //}

                public double METER_READING   //----29-03-2023
                {
                    get { return _METER_READING; }
                    set { _METER_READING = value; }
                }
                public double END_METER_READING
                {
                    get { return _END_METER_READING; }
                    set { _END_METER_READING = value; }
                }
                public double NO_OF_KMS
                {
                    get { return _NO_OF_KMS; }
                    set { _NO_OF_KMS = value; }
                }
                public double MILEGE
                {
                    get { return _MILEGE; }
                    set { _MILEGE = value; }
                }
                public double MILEGE_AMOUNT
                {
                    get { return _MILEGE_AMOUNT; }
                    set { _MILEGE_AMOUNT = value; }
                }
                public decimal ORIGINAL_RATE
                {
                    get { return _ORIGINAL_RATE; }
                    set { _ORIGINAL_RATE = value; }
                }
                //---------START------29-03-2023
                public int ISSUE_TYPE
                {
                    get { return _ISSUE_TYPE; }
                    set { _ISSUE_TYPE = value; }
                }
                public decimal AVELABLE_QTY
                {
                    get { return _AVELABLE_QTY; }
                    set { _AVELABLE_QTY = value; }
                }
                public string COUPON_NO
                {
                    get { return _COUPON_NO; }
                    set { _COUPON_NO = value; }
                }
                public decimal TOTAL_AMOUNT1
                {
                    get { return _TOTAL_AMOUNT1; }
                    set { _TOTAL_AMOUNT1 = value; }
                }

                public int DEPARTMENT
                {
                    get { return _DEPARTMENT; }
                    set { _DEPARTMENT = value; }
                }
                public DateTime DATE_OF_WITHDRAWAL
                {
                    get { return _DATE_OF_WITHDRAWAL; }
                    set { _DATE_OF_WITHDRAWAL = value; }
                }
                public int DIESEL_REQUESTER
                {
                    get { return _DIESEL_REQUESTER; }
                    set { _DIESEL_REQUESTER = value; }
                }
                public int APPROVER
                {
                    get { return _APPROVER; }
                    set { _APPROVER = value; }
                }
                public string PURPOSE_OF_WITHDRAWAL
                {
                    get { return _PURPOSE_OF_WITHDRAWAL; }
                    set { _PURPOSE_OF_WITHDRAWAL = value; }
                }
                public string UploadRequestLetter
                {
                    get { return _UploadRequestLetter; }
                    set { _UploadRequestLetter = value; }
                }
                //---------END------29-03-2023

                #endregion




                #endregion



                #region  Stop Master
                private int _STOPNO = 0;
                private string _STOPNAME = string.Empty;
                private decimal _SEQNO = 0;
                private string _IPADDRESS = string.Empty;
                private string _MACADDRESS = string.Empty;
                private DateTime _STARTING_TIME;
                private double _STUDENT_FEE;
                private double _EMPLOYEE_FEE;

                public DateTime STARTING_TIME
                {
                    get { return _STARTING_TIME; }
                    set { _STARTING_TIME = value; }
                }

                public double STUDENT_FEE
                {
                    get { return _STUDENT_FEE; }
                    set { _STUDENT_FEE = value; }
                }
                public double EMPLOYEE_FEE
                {
                    get { return _EMPLOYEE_FEE; }
                    set { _EMPLOYEE_FEE = value; }
                }

                #region public member
                public int STOPNO
                {
                    get { return _STOPNO; }
                    set { _STOPNO = value; }
                }
                public string STOPNAME
                {
                    get { return _STOPNAME; }
                    set { _STOPNAME = value; }
                }
                public decimal SEQNO
                {
                    get { return _SEQNO; }
                    set { _SEQNO = value; }
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







                #endregion

                #endregion

                #region  RouteMaster
                private string _ROUTENAME = string.Empty;
                private string _ROUTEPATH = string.Empty;
                private string _ROUTECODE = string.Empty;
                private string _KM = string.Empty;
                private string _ROUTENUMBER = string.Empty;
                private int _VEHICLETYPE = 0; //Added by Vijay Andoju on 29-02-2020 for new requirement
                private string _ROUTEFEES = string.Empty;//Added by Shaikh Juned on 02-02-2023 for new requirement
                #region public member


                public string ROUTEFEES //Added by Shaikh Juned on 02-02-2023 for new requirement
                {
                    get { return _ROUTEFEES; }
                    set { _ROUTEFEES = value; }
                }
                public int VEHICLETYPE //Added by Vijay Andoju on 29-02-2020 for new requirement
                {
                    get { return _VEHICLETYPE; }
                    set { _VEHICLETYPE = value; }
                }

                public string ROUTENAME
                {
                    get { return _ROUTENAME; }
                    set { _ROUTENAME = value; }
                }
                public string ROUTEPATH
                {
                    get { return _ROUTEPATH; }
                    set { _ROUTEPATH = value; }
                }
                public string ROUTECODE
                {
                    get { return _ROUTECODE; }
                    set { _ROUTECODE = value; }
                }
                public string KM
                {
                    get { return _KM; }
                    set { _KM = value; }
                }

                public string ROUTENUMBER
                {
                    get { return _ROUTENUMBER; }
                    set { _ROUTENUMBER = value; }
                }
                #endregion

                #endregion





                #region VehicleRouteAllotment
                private int _RANO = 0;
                private int _ROUTENO = 0;



                #region public member
                public int RANO
                {
                    get { return _RANO; }
                    set { _RANO = value; }
                }

                public int ROUTENO
                {
                    get { return _ROUTENO; }
                    set { _ROUTENO = value; }

                }

                #endregion

                #endregion


                #region  BoardingPass
                private DateTime _EXPIRY_DATE;
                private DateTime _ALLOT_DATE;
                private int _USERNO = 0;
                private int _BORID = 0;

                #region public member

                public DateTime EXPIRY_DATE
                {
                    get { return _EXPIRY_DATE; }
                    set { _EXPIRY_DATE = value; }
                }
                public DateTime ALLOT_DATE
                {
                    get { return _ALLOT_DATE; }
                    set { _ALLOT_DATE = value; }
                }
                public int USERNO
                {
                    get { return _USERNO; }
                    set { _USERNO = value; }
                }
                public int BORID
                {
                    get { return _BORID; }
                    set { _BORID = value; }
                }

                #endregion

                #endregion

                #region Item Master
                // Private Members
                private int _ITEM_ID = 0;
                private string _ITEM_NAME = string.Empty;
                private int _UNIT = 0;
                private decimal _RATE = 0;
              //  private int _QUANTITY = 0;   //09-05-2023
                private decimal _QUANTITY = 0;
                private int _ITEM_TYPE = 0;

                // Public Members
                public int ITEM_ID
                {
                    get { return _ITEM_ID; }
                    set { _ITEM_ID = value; }
                }
                public string ITEM_NAME
                {
                    get { return _ITEM_NAME; }
                    set { _ITEM_NAME = value; }
                }
                public int UNIT
                {
                    get { return _UNIT; }
                    set { _UNIT = value; }
                }
                public decimal RATE
                {
                    get { return _RATE; }
                    set { _RATE = value; }
                }
                //public int QUANTITY
                //{
                //    get { return _QUANTITY; }
                //    set { _QUANTITY = value; }
                //}

                public decimal QUANTITY
                {
                    get { return _QUANTITY; }
                    set { _QUANTITY = value; }
                }
                public int ITEM_TYPE
                {
                    get { return _ITEM_TYPE; }
                    set { _ITEM_TYPE = value; }
                }
                #endregion

                #region Item Purchase
                // Private members
                private int _PURCHASE_ID = 0;
                private DateTime _PURCHASE_DATE;
                private decimal _TOTAL_AMT ;
                private string _CRN = string.Empty;
                private int _PURCHASE_FOR = 0;
                private string _PURCHASE_COUPON_NUMBER = string.Empty;

                // Public members 
                public int PURCHASE_ID
                {
                    get { return _PURCHASE_ID; }
                    set { _PURCHASE_ID = value; }
                }
                public DateTime PURCHASE_DATE
                {
                    get { return _PURCHASE_DATE; }
                    set { _PURCHASE_DATE = value; }
                }
                public decimal TOTAL_AMT
                {
                    get { return _TOTAL_AMT; }
                    set { _TOTAL_AMT = value; }
                }

                public string CRN
                {
                    get { return _CRN; }
                    set { _CRN = value; }
                }
                public int PURCHASE_FOR
                {
                    get { return _PURCHASE_FOR; }
                    set { _PURCHASE_FOR = value; }
                }
                public string PURCHASE_COUPON_NUMBER
                {
                    get { return _PURCHASE_COUPON_NUMBER; }
                    set { _PURCHASE_COUPON_NUMBER = value; }
                }

                #endregion

                #region  Vehicle Type Master

                private int _VTID = 0;
                private string _VTNAME = string.Empty;
                private int _ROUTE_TYPE_NO = 0;
                private int _VTAC = 0;

                #region public member
                public int VTID
                {
                    get { return _VTID; }
                    set { _VTID = value; }
                }
                public string VTNAME
                {
                    get { return _VTNAME; }
                    set { _VTNAME = value; }
                }
                public int ROUTE_TYPE_NO
                {
                    get { return _ROUTE_TYPE_NO; }
                    set { _ROUTE_TYPE_NO = value; }
                }
                public int VTAC
                {
                    get { return _VTAC; }
                    set { _VTAC = value; }
                }
                #endregion

                #endregion



                #region UserRouteAllotment
                private int _URID = 0;
                private int _IDNO = 0;
                private int _ROUTEID = 0;
                private char _USER_TYPE;
                private DataTable _ALLOTMENT_TABLE;
                private string _CANCEL_REMARK = string.Empty;
                private int _YEAR = 0;


                public int YEAR
                {
                    get { return _YEAR; }
                    set { _YEAR = value; }
                }

                public DataTable ALLOTMENT_TABLE
                {
                    get { return _ALLOTMENT_TABLE; }
                    set { _ALLOTMENT_TABLE = value; }
                }

                public int URID
                {
                    get { return _URID; }
                    set { _URID = value; }
                }
                public int IDNO
                {
                    get { return _IDNO; }
                    set { _IDNO = value; }
                }
                public int ROUTEID
                {
                    get { return _ROUTEID; }
                    set { _ROUTEID = value; }
                }

                public char USER_TYPE
                {
                    get { return _USER_TYPE; }
                    set { _USER_TYPE = value; }
                }

                public string CANCEL_REMARK
                {
                    get { return _CANCEL_REMARK; }
                    set { _CANCEL_REMARK = value; }
                }


                #endregion


                #region Vehicle Schedule

                private int _SCHEDULEID = 0;
                private DateTime _SCHEDULE_DATE;
                private string _MORNING_TRIP = string.Empty;
                private string _SPECIAL_TRIP = string.Empty;
                private string _EVENING_TRIP = string.Empty;
                private string _LATE_TRIP = string.Empty;

                public int SCHEDULEID
                {
                    get { return _SCHEDULEID; }
                    set { _SCHEDULEID = value; }
                }

                public DateTime SCHEDULE_DATE
                {
                    get { return _SCHEDULE_DATE; }
                    set { _SCHEDULE_DATE = value; }
                }
                public string MORNING_TRIP
                {
                    get { return _MORNING_TRIP; }
                    set { _MORNING_TRIP = value; }
                }
                public string SPECIAL_TRIP
                {
                    get { return _SPECIAL_TRIP; }
                    set { _SPECIAL_TRIP = value; }
                }

                public string EVENING_TRIP
                {
                    get { return _EVENING_TRIP; }
                    set { _EVENING_TRIP = value; }
                }
                public string LATE_TRIP
                {
                    get { return _LATE_TRIP; }
                    set { _LATE_TRIP = value; }
                }


                #endregion

                #region ArrivalTimeEntry

                private DateTime _ArrivalDate;
                private DateTime _ArrivalTime;
                private DataTable _ArrivalTimeEntryDataTable;
                private int _AC_BUS_38_SEAT = 0;
                private int _AC_BUS_55_SEAT = 0;
                private int _DEDICATED_BUSES = 0;
                private int _SVCE_BUSES = 0;

                public int AC_BUS_38_SEAT
                {
                    get { return _AC_BUS_38_SEAT; }
                    set { _AC_BUS_38_SEAT = value; }
                }
                public int AC_BUS_55_SEAT
                {
                    get { return _AC_BUS_55_SEAT; }
                    set { _AC_BUS_55_SEAT = value; }
                }
                public int DEDICATED_BUSES
                {
                    get { return _DEDICATED_BUSES; }
                    set { _DEDICATED_BUSES = value; }
                }
                public int SVCE_BUSES
                {
                    get { return _SVCE_BUSES; }
                    set { _SVCE_BUSES = value; }
                }


                public DateTime ArrivalTime
                {
                    get { return _ArrivalTime; }
                    set { _ArrivalTime = value; }
                }
                public DataTable ArrivalTimeEntryDataTable
                {
                    get { return _ArrivalTimeEntryDataTable; }
                    set { _ArrivalTimeEntryDataTable = value; }

                }

                public DateTime ArrivalDate
                {
                    get { return _ArrivalDate; }
                    set { _ArrivalDate = value; }
                }
                #endregion


                #region ComplaintRegister   Added by Vijay Andoju on 21-02-2020 for Vehicle Compalint Register


                private System.Nullable<System.DateTime> _ActionTakenDate = DateTime.MinValue;
                private DateTime _ComplaintDate;
                private int _Sno = 0;
                private string _Route_no = string.Empty;
                private string _NatureofComplaint = string.Empty;
                private string _ComplaintRegister = string.Empty;
                private string _ComplaintRecivedThrough = string.Empty;
                private string _ActionTaken = string.Empty;


                //public DateTime ActionTakenDate
                //{
                //    get { return _ActionTakenDate; }
                //    set { _ActionTakenDate = value; }
                //}
                public System.Nullable<System.DateTime> ActionTakenDate
                {
                    get
                    { return this._ActionTakenDate; }
                    set
                    {
                        if ((this._ActionTakenDate != value))
                        {
                            this._ActionTakenDate = value;
                        }
                    }
                }

                public DateTime ComplaintDate
                {
                    get { return _ComplaintDate; }
                    set { _ComplaintDate = value; }
                }

                public string Route_no
                {
                    get { return _Route_no; }
                    set { _Route_no = value; }
                }

                public string NatureofComplaint
                {
                    get { return _NatureofComplaint; }
                    set { _NatureofComplaint = value; }
                }

                public string ComplaintRegister
                {
                    get { return _ComplaintRegister; }
                    set { _ComplaintRegister = value; }
                }

                public string ComplaintRecivedThrough
                {
                    get { return _ComplaintRecivedThrough; }
                    set { _ComplaintRecivedThrough = value; }
                }

                public string ActionTaken
                {
                    get { return _ActionTaken; }
                    set { _ActionTaken = value; }
                }

                public int Sno
                {
                    get { return _Sno; }
                    set { _Sno = value; }
                }

                #endregion


                #region Incident Report Added by Vijay Andoju 22-02-2020

                private string _NATUREOFINCIDENT = string.Empty;
                private string _INCIDENTPLACE = string.Empty;
                private string _FOLLOWUP = string.Empty;

                public string NATUREOFINCIDENT
                {
                    get { return _NATUREOFINCIDENT; }
                    set { _NATUREOFINCIDENT = value; }
                }

                public string INCIDENTPLACE
                {
                    get { return _INCIDENTPLACE; }
                    set { _INCIDENTPLACE = value; }
                }

                public string FOLLOWUP
                {
                    get { return _FOLLOWUP; }
                    set { _FOLLOWUP = value; }
                }

                #endregion


                #region StudentTransportCancellation //Added by vijay on 01-03-2020

                private int _S_SRNO = 0;
                private int _S_DEPT_NO = 0;
                private int _S_YEAR = 0;
                private int _S_SEMESTER = 0;
                private int _S_DEGREENO = 0;
                private int _S_IDNO = 0;
                private int _S_BRANCH = 0;
                private int _S_TRANSPORT = 0;
                private int _S_CREATED_BY = 0;

                public int S_SRNO
                {
                    get { return _S_SRNO; }
                    set { _S_SRNO = value; }
                }
                public int S_DEPT_NO
                {
                    get { return _S_DEPT_NO; }
                    set { _S_DEPT_NO = value; }
                }
                public int S_YEAR
                {
                    get { return _S_YEAR; }
                    set { _S_YEAR = value; }
                }
                public int S_SEMESTER
                {
                    get { return _S_SEMESTER; }
                    set { _S_SEMESTER = value; }
                }
                public int S_DEGREENO
                {
                    get { return _S_DEGREENO; }
                    set { _S_DEGREENO = value; }
                }
                public int S_IDNO
                {
                    get { return _S_IDNO; }
                    set { _S_IDNO = value; }
                }
                public int S_BRANCH
                {
                    get { return _S_BRANCH; }
                    set { _S_BRANCH = value; }
                }
                public int S_TRANSPORT
                {
                    get { return _S_TRANSPORT; }
                    set { _S_TRANSPORT = value; }
                }
                public int S_CREATED_BY
                {
                    get { return _S_CREATED_BY; }
                    set { _S_CREATED_BY = value; }
                }


                #endregion

                #region StudentTransportStatus added by vijay 2020-mar-16

                private string _S_CANCELLED_STATUS = string.Empty;
                private int _S_STATUS = 0;
                public int S_STATUS
                {
                    get { return _S_STATUS; }
                    set { _S_STATUS = value; }
                }
                public string S_CANCELLED_STATUS
                {
                    get { return _S_CANCELLED_STATUS; }
                    set { _S_CANCELLED_STATUS = value; }
                }


                #endregion

                #region HOD Cars Details

                private int _HODCARS_ID = 0;
                private DataTable _HODCarsTable = null;
                private int _CREATED_BY = 0;
                private DateTime _ARRIVAL_DATE;
                private int _TRAVELS_ID = 0;

                private DateTime _IN_TIME;


                public int HODCARS_ID
                {
                    get { return _HODCARS_ID; }
                    set { _HODCARS_ID = value; }
                }

                public DataTable HODCarsTable
                {
                    get { return _HODCarsTable; }
                    set { _HODCarsTable = value; }
                }

                public int CREATED_BY
                {
                    get { return _CREATED_BY; }
                    set { _CREATED_BY = value; }
                }



                public DateTime ARRIVAL_DATE
                {
                    get { return _ARRIVAL_DATE; }
                    set { _ARRIVAL_DATE = value; }
                }


                public int TRAVELS_ID
                {
                    get { return _TRAVELS_ID; }
                    set { _TRAVELS_ID = value; }
                }



                public DateTime IN_TIME
                {
                    get { return _IN_TIME; }
                    set { _IN_TIME = value; }
                }




                #endregion

                #region BUS_TOKEN_ISSUE

                private int _BTNO = 0;
                private DateTime _BUS_TOKEN_ISSUE_DATE;
                private int _TOKEN_40 = 0;
                private int _TOKEN_30 = 0;
                private double _TOKEN_40_AMOUNT = 0.0;
                private double _TOKEN_30_AMOUNT = 0.0;
                private double _GRAND_TOTAL = 0.0;

                public int BTNO
                {
                    get { return _BTNO; }
                    set { _BTNO = value; }
                }
                public DateTime BUS_TOKEN_ISSUE_DATE
                {
                    get { return _BUS_TOKEN_ISSUE_DATE; }
                    set { _BUS_TOKEN_ISSUE_DATE = value; }
                }
                public int TOKEN_40
                {
                    get { return _TOKEN_40; }
                    set { _TOKEN_40 = value; }
                }
                public int TOKEN_30
                {
                    get { return _TOKEN_30; }
                    set { _TOKEN_30 = value; }
                }
                public double TOKEN_40_AMOUNT
                {
                    get { return _TOKEN_40_AMOUNT; }
                    set { _TOKEN_40_AMOUNT = value; }
                }
                public double TOKEN_30_AMOUNT
                {
                    get { return _TOKEN_30_AMOUNT; }
                    set { _TOKEN_30_AMOUNT = value; }
                }
                public double GRAND_TOTAL
                {
                    get { return _GRAND_TOTAL; }
                    set { _GRAND_TOTAL = value; }
                }


#endregion

                #region Fuel_Supplier_Master

                private int _FUEL_SUPPILER_ID = 0;
                private string _FUEL_SUPPILER_NAME = string.Empty;
                private string _FUEL_CONTACT_ADDRESS = string.Empty;
                private string _FUEL_CONTACT_NUMBER = string.Empty;
                private string _FUEL_CONTACT_PERSON = string.Empty;
                private DateTime _FUEL_SUPPLIED_DATE;
                private Boolean _FUEL_IS_ACTIVE;


                public int FUEL_SUPPILER_ID
                {
                    get { return _FUEL_SUPPILER_ID; }
                    set { _FUEL_SUPPILER_ID = value; }
                }

                public string FUEL_SUPPILER_NAME
                {
                    get { return _FUEL_SUPPILER_NAME; }
                    set { _FUEL_SUPPILER_NAME = value; }
                }

                public string FUEL_CONTACT_ADDRESS
                {
                    get { return _FUEL_CONTACT_ADDRESS; }
                    set { _FUEL_CONTACT_ADDRESS = value; }
                }

                public string FUEL_CONTACT_NUMBER
                {
                    get { return _FUEL_CONTACT_NUMBER; }
                    set { _FUEL_CONTACT_NUMBER = value; }
                }
                public string FUEL_CONTACT_PERSON
                {
                    get { return _FUEL_CONTACT_PERSON; }
                    set { _FUEL_CONTACT_PERSON = value; }
                }
                public DateTime FUEL_SUPPLIED_DATE
                {
                    get { return _FUEL_SUPPLIED_DATE; }
                    set { _FUEL_SUPPLIED_DATE = value; }
                }
                public Boolean FUEL_IS_ACTIVE
                {
                    get { return _FUEL_IS_ACTIVE; }
                    set { _FUEL_IS_ACTIVE = value; }
                }




                #endregion

                #region MONTHLY_FUEL_EXPENSE_ENTRY
                private DateTime _Fule_FromDate;
                private DateTime _Fule_ToDate;

                private double _Fuel_Amount=0.0;
                private int _MFE_NO = 0;


                public DateTime Fule_FromDate
                {
                    get { return _Fule_FromDate; }
                    set { _Fule_FromDate = value; }
                }
                public DateTime Fule_ToDate
                {
                    get { return _Fule_ToDate; }
                    set { _Fule_ToDate = value; }
                }

                public double Fuel_Amount
                {
                    get { return _Fuel_Amount; }
                    set { _Fuel_Amount = value; }
                }
                public int MFE_NO
                {
                    get { return _MFE_NO; }
                    set { _MFE_NO = value; }
                }

                #endregion

                #region Bus Incharge

                private int _BUS_INC_ID;

                public int BUS_INC_ID
                {
                    get { return _BUS_INC_ID; }
                    set { _BUS_INC_ID = value; }
                }
                #endregion

                #region Vehicle Requisition

                private int _VEH_REQ_ID;
                private int _COLLEGE_ID;
                private DateTime _DATE_OF_JOURNEY;
                private int _ONE_WAY;
                private int _UANO;
                private DataTable _VEHICLE_REQ_EMP_TBL;
                private DataTable _VEHICLE_REQ_VEH_TBL;

                public int VEH_REQ_ID
                {
                    get { return _VEH_REQ_ID; }
                    set { _VEH_REQ_ID = value; }
                }
                public int COLLEGE_ID
                {
                    get { return _COLLEGE_ID; }
                    set { _COLLEGE_ID = value; }
                }
                public DateTime DATE_OF_JOURNEY
                {
                    get { return _DATE_OF_JOURNEY; }
                    set { _DATE_OF_JOURNEY = value; }
                }
                public int ONE_WAY
                {
                    get { return _ONE_WAY; }
                    set { _ONE_WAY = value; }
                }
                public int UANO
                {
                    get { return _UANO; }
                    set { _UANO = value; }
                }
                public DataTable VEHICLE_REQ_EMP_TBL
                {
                    get { return _VEHICLE_REQ_EMP_TBL; }
                    set { _VEHICLE_REQ_EMP_TBL = value; }
                }
                public DataTable VEHICLE_REQ_VEH_TBL
                {
                    get { return _VEHICLE_REQ_VEH_TBL; }
                    set { _VEHICLE_REQ_VEH_TBL = value; }
                }

                #endregion

                #region VehiclE Allotment By TC

                private int _VATC_ID;
                private string _VEHICLE_NO;
                private DateTime _ARRIVAL_TIME;
                private DateTime _DEPARTURE_TIME;
                private int _TOT_KM_TRAVEL;
                private int _TOT_HRS_TRAVEL;
                private double _AMOUNT_PAY;

                public int VATC_ID
                {
                    get { return _VATC_ID; }
                    set { _VATC_ID = value; }
                }
                public string VEHICLE_NO
                {
                    get { return _VEHICLE_NO; }
                    set { _VEHICLE_NO = value; }
                }
                public DateTime ARRIVAL_TIME
                {
                    get { return _ARRIVAL_TIME; }
                    set { _ARRIVAL_TIME = value; }
                }
                public DateTime DEPARTURE_TIME
                {
                    get { return _DEPARTURE_TIME; }
                    set { _DEPARTURE_TIME = value; }
                }
                public int TOT_KM_TRAVEL
                {
                    get { return _TOT_KM_TRAVEL; }
                    set { _TOT_KM_TRAVEL = value; }
                }
                public int TOT_HRS_TRAVEL
                {
                    get { return _TOT_HRS_TRAVEL; }
                    set { _TOT_HRS_TRAVEL = value; }
                }
                public double AMOUNT_PAY
                {
                    get { return _AMOUNT_PAY; }
                    set { _AMOUNT_PAY = value; }
                }

                #endregion

                #region Tour/Event Entry

                private int _TEID;
                private DateTime _TOUREVENTDATE;
                private DateTime _OUTTIME;
                private double _OUTKM;
                private string _PURPOSE;
                private DateTime _INTIME;
                private double _INKM;
                private int _MALE_COUNT;
                private int _FEMALE_COUNT;
                private int _CHILDREN_COUNT;
                private int _INFANT_COUNT;
                private int _TOTAL_NO_PATIENT;
                private double _TOTAL_KM;
                private string _PLACE;



                public int TEID
                {
                    get { return _TEID; }
                    set { _TEID = value; }
                }
                public DateTime TOUREVENTDATE
                {
                    get { return _TOUREVENTDATE; }
                    set { _TOUREVENTDATE = value; }
                }
                public DateTime OUTTIME
                {
                    get { return _OUTTIME; }
                    set { _OUTTIME = value; }
                }

                public double OUTKM
                {
                    get { return _OUTKM; }
                    set { _OUTKM = value; }
                }
                public string PURPOSE
                {
                    get { return _PURPOSE; }
                    set { _PURPOSE = value; }
                }
                public DateTime INTIME
                {
                    get { return _INTIME; }
                    set { _INTIME = value; }
                }
                public double INKM
                {
                    get { return _INKM; }
                    set { _INKM = value; }
                }

                public int MALE_COUNT
                {
                    get { return _MALE_COUNT; }
                    set { _MALE_COUNT = value; }
                }
                public int FEMALE_COUNT
                {
                    get { return _FEMALE_COUNT; }
                    set { _FEMALE_COUNT = value; }
                }
                public int CHILDREN_COUNT
                {
                    get { return _CHILDREN_COUNT; }
                    set { _CHILDREN_COUNT = value; }
                }
                public int INFANT_COUNT
                {
                    get { return _INFANT_COUNT; }
                    set { _INFANT_COUNT = value; }
                }
                public int TOTAL_NO_PATIENT
                {
                    get { return _TOTAL_NO_PATIENT; }
                    set { _TOTAL_NO_PATIENT = value; }
                }
                public double TOTAL_KM
                {
                    get { return _TOTAL_KM; }
                    set { _TOTAL_KM = value; }
                }
                public string PLACE
                {
                    get { return _PLACE; }
                    set { _PLACE = value; }
                }
                #endregion

                #region Late Coming Vehicle Panalty

                private DateTime _ACTUAL_ARRIVAL_TIME;
                private int _LATE_HOURS;
                private double _FINE_AMOUNT;
                private int _PENALTY_ID;

                public int PENALTY_ID
                {
                    get {return _PENALTY_ID;}
                    set { _PENALTY_ID = value; }
                }

                public DateTime ACTUAL_ARRIVAL_TIME
                {
                    get {return _ACTUAL_ARRIVAL_TIME;}
                    set{_ACTUAL_ARRIVAL_TIME = value;}
                }

                public int LATE_HOURS
                {
                    get {return _LATE_HOURS;}
                    set{_LATE_HOURS = value;}
                }
                public double FINE_AMOUNT
                {
                    get {return _FINE_AMOUNT;}
                    set{_FINE_AMOUNT = value;}
                }

                #endregion

                #region Ambulance Maintenance
                private int _AM_ID;
                private DateTime _DATE;
                private string _BILLNO;
                private string _SUPPLIERS_NAME;
                private string _DISCRIPTION_NAME;
                private double _AMOUNT;                
                private double _CGST;
                private double _SGST;
                private string _INCHARGE;


                public int AM_ID
                {
                    get { return _AM_ID; }
                    set { _AM_ID = value; }
                }
                public DateTime DATE
                {
                    get { return _DATE; }
                    set { _DATE = value; }
                }
                public string BILLNO
                {
                    get { return _BILLNO; }
                    set { _BILLNO = value; }
                }
                public string SUPPLIERS_NAME
                {
                    get { return _SUPPLIERS_NAME; }
                    set { _SUPPLIERS_NAME = value; }
                }
                public string DISCRIPTION_NAME
                {
                    get { return _DISCRIPTION_NAME; }
                    set { _DISCRIPTION_NAME = value; }
                }
                public double AMOUNT
                {
                    get { return _AMOUNT; }
                    set { _AMOUNT = value; }
                }
                public double CGST
                {
                    get { return _CGST; }
                    set { _CGST = value; }
                }
                public double SGST
                {
                    get { return _SGST; }
                    set { _SGST = value; }
                }
                public string INCHARGE
                {
                    get { return _INCHARGE; }
                    set { _INCHARGE = value; }
                }
                
                #endregion

                #region  Vehicle Category Master

                private int _VCID = 0;
                private string _CATEGORYNAME = string.Empty;
                private DateTime _ModifiedDate;
                private int _IsActive = 0;
                private int _CollegeNo = 0;
                private int _VTRAID = 0;
                private DateTime _APP_DATE;
                private string _PERIOD_FROM;
                private string _PERIOD_TO;

                #region public member
                public int VCID
                {
                    get { return _VCID; }
                    set { _VCID = value; }
                }
                public string CATEGORYNAME
                {
                    get { return _CATEGORYNAME; }
                    set { _CATEGORYNAME = value; }
                }
                public DateTime ModifiedDate
                {
                    get { return _ModifiedDate; }
                    set { _ModifiedDate = value; }
                }
                public int IsActive
                {
                    get { return _IsActive; }
                    set { _IsActive = value; }
                }
                public int CollegeNo
                {
                    get { return _CollegeNo; }
                    set { _CollegeNo = value; }
                }
                public int VTRAID
                {
                    get { return _VTRAID; }
                    set { _VTRAID = value; }
                }
                public DateTime APP_DATE
                {
                    get { return _APP_DATE; }
                    set { _APP_DATE = value; }
                }
                #endregion

                #endregion


                private DataTable _APPROVAL_TRAN = null;
                private int _SESSIONNO = 0;

                public DataTable APPROVAL_TRAN
                {
                    get { return _APPROVAL_TRAN; }
                    set { _APPROVAL_TRAN = value; }
                }

                public int SESSIONNO
                {
                    get { return _SESSIONNO; }
                    set { _SESSIONNO = value; }
                }

                public string PERIOD_FROM
                {
                    get { return _PERIOD_FROM; }
                    set { _PERIOD_FROM = value; }
                }


                public string PERIOD_TO
                {
                    get { return _PERIOD_TO; }
                    set { _PERIOD_TO = value; }
                }


                #region Bus Structure Form
                string _UploadBusStructure = string.Empty;
                int _IsBlob = 0;
                private DataTable _Structure_Count_TBL = null;
                int _Route = 0;
                int _BusStructure = 0;
                private DataTable _Bus_Structure_Data_TBL = null;
                string _UploadBlobName = string.Empty;


                public int IsBlob
                {
                    get { return _IsBlob; }
                    set { _IsBlob = value; }
                }

                public string UploadBusStructure
                {
                    get { return _UploadBusStructure; }
                    set { _UploadBusStructure = value; }
                }

                public DataTable Structure_Count_TBL
                {
                    get
                    {
                        return this._Structure_Count_TBL;
                    }
                    set
                    {
                        if ((this._Structure_Count_TBL != value))
                        {
                            this._Structure_Count_TBL = value;
                        }
                    }
                }

                public int Route
                {
                    get { return _Route; }
                    set { _Route = value; }
                }
                public int BusStructure
                {
                    get { return _BusStructure; }
                    set { _BusStructure = value; }
                }

                public DataTable Bus_Structure_Data_TBL
                {
                    get
                    {
                        return this._Bus_Structure_Data_TBL;
                    }
                    set
                    {
                        if ((this._Bus_Structure_Data_TBL != value))
                        {
                            this._Bus_Structure_Data_TBL = value;
                        }
                    }
                }

                public string UploadBlobName
                {
                    get { return _UploadBlobName; }
                    set { _UploadBlobName = value; }
                }
                #endregion
                #region
                int _SESSIONN = 0;
                int _SCHEMENO = 0;
                int _SEMESTERNOS = 0;
                int _COURSENOS = 0;
                string _IPADDRESS1 = string.Empty;
                int _IDNO1 = 0;
                string _REGNO1 = string.Empty;
                int _UA_NO = 0;
                int _COLLEGE_CODE1 = 0;

                public int SESSIONN
                {
                    get { return _SESSIONN; }
                    set { _SESSIONN = value; }
                }
                public int SCHEMENO
                {
                    get { return _SCHEMENO; }
                    set { _SCHEMENO = value; }
                }

                public int SEMESTERNOS
                {
                    get { return _SEMESTERNOS; }
                    set { _SEMESTERNOS = value; }
                }
                public int COURSENOS
                {
                    get { return _COURSENOS; }
                    set { _COURSENOS = value; }
                }
                public string IPADDRESS1
                {
                    get { return _IPADDRESS1; }
                    set { _IPADDRESS1 = value; }
                }
                public int IDNO1
                {
                    get { return _IDNO1; }
                    set { _IDNO1 = value; }
                }

                public string REGNO1
                {
                    get { return _REGNO1; }
                    set { _REGNO1 = value; }
                }

                public int UA_NO
                {
                    get { return _UA_NO; }
                    set { _UA_NO = value; }
                }
                public int COLLEGE_CODE1
                {
                    get { return _COLLEGE_CODE1; }
                    set { _COLLEGE_CODE1 = value; }
                }

                #endregion
            }
        }
    }
}
