using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;


namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
            public class GuestHouse
            {
                #region Guest Category Details
                private int _GUEST_CATEGORY_ID;

                private string _GUEST_CATEGORY_NAME=string.Empty;
                private string _CHARGE = string.Empty;
                private string _TAX = string.Empty;
                private int _CHECK_ID = 0;
                private int _GHID = 0;

                public int GHID
                {
                    get { return _GHID; }
                    set { _GHID = value; }
                }

                public int GUEST_CATEGORY_ID
                {
                    get { return _GUEST_CATEGORY_ID; }
                    set { _GUEST_CATEGORY_ID = value; }
                }

                public string GUEST_CATEGORY_NAME
                {
                    get { return _GUEST_CATEGORY_NAME; }
                    set { _GUEST_CATEGORY_NAME = value; }
                }
                public string CHARGE
                {
                    get { return _CHARGE; }
                    set { _CHARGE = value; }
                }
                public string TAX
                {
                    get { return _TAX; }
                    set { _TAX = value; }
                }
                public int CHECK_ID
                {
                    get { return _CHECK_ID; }
                    set { _CHECK_ID = value; }
                }

                #region this is for Master Guest House Room 
                private string _GUEST_ROOM_NAME = string.Empty;
                private int _GUEST_ROOM_NAME_ID=0;
                private int _GUEST_HOUSE_NAME = 0;
                private int _GUEST_HOUSE_CATEGORY = 0;

                public int  GUEST_HOUSE_NAME
                {
                    get { return _GUEST_HOUSE_NAME; }
                    set { _GUEST_HOUSE_NAME = value; }
                }
                public int  GUEST_HOUSE_CATEGORY
                {
                    get { return _GUEST_HOUSE_CATEGORY; }
                    set { _GUEST_HOUSE_CATEGORY = value; }
                }
                public string  GUEST_ROOM_NAME
                {
                    get { return _GUEST_ROOM_NAME; }
                    set { _GUEST_ROOM_NAME = value; }
                }
                public int GUEST_ROOM_NAME_ID
                {
                    get { return _GUEST_ROOM_NAME_ID; }
                    set { _GUEST_ROOM_NAME_ID = value; }
                }
                #endregion


                #region
                private string _VISITOR_NAME = string.Empty;
                private DateTime _ARRIVAL_DATE;
                private DateTime _DEPARTURE_DATE ;
                private string _GUEST_CATEGORY = string.Empty;
                private string _JUSTIFICATION = string.Empty;
                private int _NO_OF_ROOM = 0;
                private string _VISIT_PURPOSE = string.Empty;
                private decimal _ADVANCE_PAYMENT=0;
                private string _BOOKING_PERSON_NAME = string.Empty;
                private string _DESIGNATION = string.Empty;
                private string _DEPARMENT = string.Empty;
                private Int64 _MOBILE_NO = 0;
                private int _BOOKING_DETAIL_NO = 0;
                private DateTime _ARRIVAL_TIME;
                private DateTime _DEPARTMENT_TIME;
                private string _POSTAL_ADDRESS = string.Empty;
                private int _NO_OF_VISITORS= 0;
                private DateTime _ADVANCE_PAYMENT_DATE;
                private int _GUEST_BOOKING_DETAILS_ID = 0;
                private int _BOOKING_PERSON_ID = 0;
                private int _LNO;
                private int _PAPN;

                private string _Remark = string.Empty;
                public string Remark
                {
                    get { return _Remark; }
                    set { _Remark = value; }
                }


                public int PAPN
                {
                    get { return _PAPN; }
                    set { _PAPN = value; }
                }
                public int  BOOKING_PERSON_ID
                {
                    get { return _BOOKING_PERSON_ID; }
                    set { _BOOKING_PERSON_ID = value; }
                }
                public int  GUEST_BOOKING_DETAILS_ID
                {
                    get { return _GUEST_BOOKING_DETAILS_ID; }
                    set { _GUEST_BOOKING_DETAILS_ID = value; }
                }
                public DateTime  ADVANCE_PAYMENT_DATE
                {
                    get { return _ADVANCE_PAYMENT_DATE; }
                    set { _ADVANCE_PAYMENT_DATE = value; }
                }
                public string  POSTAL_ADDRESS
                {
                    get { return _POSTAL_ADDRESS; }
                    set { _POSTAL_ADDRESS = value; }
                }
                public int NO_OF_VISITORS
                {
                    get { return _NO_OF_VISITORS; }
                    set { _NO_OF_VISITORS = value; }

                }
                public string  VISITOR_NAME
                {
                    get { return _VISITOR_NAME; }
                    set { _VISITOR_NAME = value; }
                }
                public DateTime  ARRIVAL_DATE
                {
                    get { return _ARRIVAL_DATE; }
                    set { _ARRIVAL_DATE = value; }
                }
                public DateTime  DEPARTURE_DATE
                {
                    get { return _DEPARTURE_DATE; }
                    set { _DEPARTURE_DATE = value; }
                }
                public string  GUEST_CATEGORY
                {
                    get { return _GUEST_CATEGORY; }
                    set { _GUEST_CATEGORY = value; }
                }
                public string  JUSTIFICATION
                {
                    get { return _JUSTIFICATION; }
                    set { _JUSTIFICATION = value; }
                }
                public int  NO_OF_ROOM
                {
                    get { return _NO_OF_ROOM; }
                    set { _NO_OF_ROOM = value; }
                }
                public string  VISIT_PURPOSE
                {
                    get { return _VISIT_PURPOSE; }
                    set { _VISIT_PURPOSE = value; }
                }
                public decimal  ADVANCE_PAYMENT
                {
                    get { return _ADVANCE_PAYMENT; }
                    set { _ADVANCE_PAYMENT = value; }
                }
                public string  BOOKING_PERSON_NAME
                {
                    get { return _BOOKING_PERSON_NAME; }
                    set { _BOOKING_PERSON_NAME = value; }
                }
                public string  DESIGNATION
                {
                    get { return _DESIGNATION; }
                    set { _DESIGNATION = value; }
                }
                public string  DEPARMENT
                {
                    get { return _DEPARMENT; }
                    set { _DEPARMENT = value; }
                }
                public Int64  MOBILE_NO
                {
                    get { return _MOBILE_NO; }
                    set { _MOBILE_NO = value; }
                }
                public int  BOOKING_DETAIL_NO
                {
                    get { return _BOOKING_DETAIL_NO; }
                    set { _BOOKING_DETAIL_NO = value; }
                }
                public DateTime ARRIVAL_TIME
                {
                    get { return _ARRIVAL_TIME; }
                    set {_ARRIVAL_TIME = value; }
                }
                public DateTime DEPARTMENT_TIME
                {
                    get { return _DEPARTMENT_TIME; }
                    set { _DEPARTMENT_TIME = value; }
                }

         #endregion

                #region Approval Authority

                private int _PANO;
                private string _PANAME;
                private int _UANO;
                private string _COLLEGE_CODE;
                private int _SUBDESIGNO;

                public int PANO
                {
                    get
                    { return this._PANO; }
                    set
                    {
                        if ((this._PANO != value))
                        {
                            this._PANO = value;
                        }
                    }
                }

                public string PANAME
                {
                    get
                    {
                        return this._PANAME;
                    }
                    set
                    {
                        if ((this._PANAME != value))
                        {
                            this._PANAME = value;
                        }
                    }

                }

                public int UANO
                {
                    get
                    {
                        return this._UANO;
                    }
                    set
                    {
                        if ((this._UANO != value))
                        {
                            this._UANO = value;
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

                #endregion


                #region Approval Passing Path


                private int _PAPNO;
                private int _PAN01;
                private int _PAN02;
                private int _PAN03;
                private int _PAN04;
                private int _PAN05;
                private string _PAPATH;
                private int _GUEST_CAT;
                private int _EVENTNO;
                private int _EMPNO;
                private int _DEPTARTMENT;
                private int _DEPARTMENT;


                public int PAPNO
                {
                    get
                    {
                        return this._PAPNO;
                    }
                    set
                    {
                        if ((this._PAPNO != value))
                        {
                            this._PAPNO = value;
                        }
                    }
                }

                public int PAN01
                {
                    get
                    {
                        return this._PAN01;
                    }
                    set
                    {
                        if ((this._PAN01 != value))
                        {
                            this._PAN01 = value;
                        }
                    }
                }
                public int PAN02
                {
                    get
                    {
                        return this._PAN02;
                    }
                    set
                    {
                        if ((this._PAN02 != value))
                        {
                            this._PAN02 = value;
                        }
                    }
                }
                public int PAN03
                {
                    get
                    {
                        return this._PAN03;
                    }
                    set
                    {
                        if ((this._PAN03 != value))
                        {
                            this._PAN03 = value;
                        }
                    }
                }

                public int PAN04
                {
                    get
                    {
                        return this._PAN04;
                    }
                    set
                    {
                        if ((this._PAN04 != value))
                        {
                            this._PAN04 = value;
                        }
                    }
                }

                public int PAN05
                {
                    get
                    {
                        return this._PAN05;
                    }
                    set
                    {
                        if ((this._PAN05 != value))
                        {
                            this._PAN05 = value;
                        }
                    }
                }
                public string PAPATH
                {
                    get
                    {
                        return this._PAPATH;
                    }
                    set
                    {
                        if ((this._PAPATH != value))
                        {
                            this._PAPATH = value;
                        }

                    }
                }
                public int DEPTARTMENT
                {
                    get
                    {
                        return this._DEPTARTMENT;
                    }
                    set
                    {
                        if ((this._DEPTARTMENT != value))
                        {
                            this._DEPTARTMENT = value;
                        }
                    }
                }
               
                public int GUEST_CAT
                {
                    get
                    {
                        return this._GUEST_CAT;
                    }
                    set
                    {
                        if ((this._GUEST_CAT != value))
                        {
                            this._GUEST_CAT = value;
                        }
                    }
                }
                

                public int DEPARTMENT
                {
                    get
                    {
                        return this._DEPARTMENT;
                    }
                    set
                    {
                        if ((this._DEPARTMENT != value))
                        {
                            this._DEPARTMENT = value;
                        }
                    }
                }

                public int SUBDESIGNO
                {
                    get
                    {
                        return this._SUBDESIGNO;
                    }
                    set
                    {
                        if ((this._SUBDESIGNO != value))
                        {
                            this._SUBDESIGNO = value;
                        }
                    }
                }

                public int EMPNO
                {
                    get
                    {
                        return this._EMPNO;
                    }
                    set
                    {
                        if ((this._EMPNO != value))
                        {
                            this._EMPNO = value;
                        }
                    }

                }
                
                
                public int LNO
                {
                    get
                    {
                        return this._LNO;
                    }
                    set
                    {
                        if ((this._LNO != value))
                        {
                            this._LNO = value;
                        }
                    }
                }

                #endregion

                #endregion

                #region Room Allotment Page
                //private int _ROOMID = 0;
                //private int _GHCATEGORYID = 0;
                //private int _BOOKINGID=0;
                private int _ROOM_ALLOTMENT = 0;
                private string _GHNAME = string.Empty;
                private string _GHROOMNAME = string.Empty;
                private string _RA_REMARK = string.Empty;

                public string RA_REMARK
                {
                    get { return _RA_REMARK; }
                    set { _RA_REMARK = value; }
                }
                public string GHNAME
                {
                    get { return _GHNAME; }
                    set { _GHNAME = value; }
                }
                public string  GHROOMNAME
                {
                    get { return _GHROOMNAME; }
                    set { _GHROOMNAME = value; }
                }
                //public int  BOOKINGID
                //{
                //    get { return _BOOKINGID; }
                //    set { _BOOKINGID = value; }
                //}
                public int ROOM_ALLOTMENT
                {
                    get { return _ROOM_ALLOTMENT; }
                    set { _ROOM_ALLOTMENT = value; }
                }
                //public int  USERNO
                //{
                //    get { return _USERNO; }
                //    set { _USERNO = value; }
                //}
                #endregion


                #region Booking Detail Page

                private string _ARRIVALDATE = string.Empty;
                private string _ARRIVALTIME = string.Empty;
                private string _DEPARTUREDATE = string.Empty;
                private string _DEPARTMENTTIME = string.Empty;
                

                public string  ARRIVALDATE
                {
                    get { return _ARRIVALDATE; }
                    set { _ARRIVALDATE = value; }
                }
                public string ARRIVALTIME
                {
                    get { return _ARRIVALTIME; }
                    set { _ARRIVALTIME = value; }
                }
                public string DEPARTUREDATE
                {
                    get { return _DEPARTUREDATE; }
                    set { _DEPARTUREDATE = value; }
                }
                public string  DEPARTMENTTIME
                {
                    get { return _DEPARTMENTTIME; }
                    set { _DEPARTMENTTIME = value; }
                }
                #endregion

                #region Payment detail
                private decimal _REMAINING_PAYMENT = 0;
                private decimal _TOTAL_PAYMENT = 0;
                private int _TOTALDAYS = 0;
                public decimal REMAININGPAYMENT
                {
                    get { return _REMAINING_PAYMENT; }
                    set { _REMAINING_PAYMENT = value; }
                }

                public decimal TOTALPAYMENT
                {
                    get { return _TOTAL_PAYMENT; }
                    set { _TOTAL_PAYMENT = value; }
                }

                public int TOTALDAYS
                {
                    get { return _TOTALDAYS; }
                    set { _TOTALDAYS = value; }
                }
                 #endregion
            }
        }
    }
}
