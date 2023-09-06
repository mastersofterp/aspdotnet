
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {
            public class Complaint
            {
                #region complaint Registration             

                private int _Admin_UA_no = 0;
                private int _complaintid = 0;
                private string _complaintno = string.Empty;
                private DateTime _complaintdate = DateTime.Today;
                private DateTime _complaintenddate = DateTime.Today;
                private string _complaint = string.Empty;
                private string _complaintee_name = string.Empty;
                private string _complaintee_address = string.Empty;
                private string _complaintee_phoneno = string.Empty;
                private char _allotmentstatus = ' ';
                private char _complaintstatus = ' ';
                private int _deptid = 0;
                private int _ua_no = 0;
                private string _AllotedTo = string.Empty;
                private int _CompNatureId = 0;
                private int _AreaId = 0;
                private string _Complaintee_OtherPhoneNo = string.Empty;
                private DateTime _PreferableDate ;
                private DateTime _PreferableTime;
                private DateTime _PreferableTimeTo;
               
                private char _USER_CATEGORY ;
                private string _ALLOT_TO_NAME = string.Empty;

              
                //Complaint
                public int ComplaintId
                {
                    get { return _complaintid; }
                    set { _complaintid = value; }
                }

                public int Deptid
                {
                    get { return _deptid; }
                    set { _deptid = value; }
                }

                public int Ua_No
                {
                    get { return _ua_no; }
                    set { _ua_no = value; }
                }

                public DateTime ComplaintDate
                {
                    get { return _complaintdate; }
                    set { _complaintdate = value; }
                }

                public DateTime ComplaintEndDate
                {
                    get { return _complaintenddate; }
                    set { _complaintenddate = value; }
                }

                public string ComplaintNo
                {
                    get { return _complaintno; }
                    set { _complaintno = value; }
                }

                public char AllotmentStatus
                {
                    get { return _allotmentstatus; }
                    set { _allotmentstatus = value; }
                }

                public char ComplaintStatus
                {
                    get { return _complaintstatus; }
                    set { _complaintstatus = value; }
                }

                public string complaint
                {
                    get { return _complaint; }
                    set { _complaint = value; }
                }
                public string remark
                {
                    get { return _remark; }
                    set { _remark = value; }
                }
                public int CELLALLOTMENTID
                {
                    get { return _CELLALLOTMENTID; }
                    set { _CELLALLOTMENTID = value; }
                }

                public string Complaintee_Name
                {
                    get { return _complaintee_name; }
                    set { _complaintee_name = value; }
                }

                public string Complaintee_Address
                {
                    get { return _complaintee_address; }
                    set { _complaintee_address = value; }
                }

                public string Complaintee_PhoneNo
                {
                    get { return _complaintee_phoneno; }
                    set { _complaintee_phoneno = value; }
                }

                public string Complaintee_OtherPhoneNo
                {
                    get { return _Complaintee_OtherPhoneNo; }
                    set { _Complaintee_OtherPhoneNo = value; }
                }
                public int AreaId
                {
                    get { return _AreaId; }
                    set { _AreaId = value; }
                }

                public DateTime PreferableDate
                {
                    get { return _PreferableDate; }
                    set { _PreferableDate = value; }
                }

                public DateTime PreferableTime
                {
                    get { return _PreferableTime; }
                    set { _PreferableTime = value; }
                }
                public DateTime PreferableTimeTo
                {
                    get { return _PreferableTimeTo; }
                    set { _PreferableTimeTo = value; }
                }
               
                public char USER_CATEGORY
                {
                    get { return _USER_CATEGORY; }
                    set { _USER_CATEGORY = value; }
                }
                public string ALLOT_TO_NAME
                {
                    get { return _ALLOT_TO_NAME; }
                    set { _ALLOT_TO_NAME = value; }
                }


                #endregion


                #region  complaint_type

                private string _type_name = string.Empty;
                private string _type_code = string.Empty;
                private int _typeid = 0;

                //Complaint_Type
                public string Type_Name
                {
                    get { return _type_name; }
                    set { _type_name = value; }
                }
                public string Type_Code
                {
                    get { return _type_code; }
                    set { _type_code = value; }
                }
                public int TypeId
                {
                    get { return _typeid; }
                    set { _typeid = value; }
                }
                #endregion


                #region complaint Item

                private int _itemid = 0;
                private string _itemcode = string.Empty;
                private string _itemname = string.Empty;
                private string _itemunit = string.Empty;
                private int _maxstock = 0;
                private int _minstock = 0;
                private int _currstock = 0;
                private DataTable _ITEMLIST_DT = null;

                //Complaint Item
                public DataTable ITEMLIST_DT
                {
                    get { return _ITEMLIST_DT; }
                    set { _ITEMLIST_DT = value; }
                }
                public int ItemId
                {
                    get { return _itemid; }
                    set { _itemid = value; }
                }
                public string ItemCode
                {
                    get { return _itemcode; }
                    set { _itemcode = value; }
                }
                public string ItemName
                {
                    get { return _itemname; }
                    set { _itemname = value; }
                }
                public string ItemUnit
                {
                    get { return _itemunit; }
                    set { _itemunit = value; }
                }
                public int MaxStock
                {
                    get { return _maxstock; }
                    set { _maxstock = value; }
                }
                public int MinStock
                {
                    get { return _minstock; }
                    set { _minstock = value; }
                }
                public int CurrStock
                {
                    get { return _currstock; }
                    set { _currstock = value; }
                }
                #endregion


                #region complaint itemorder

                private int _orderid = 0;
                private decimal _qtyorder = 0.0M;
                private DateTime _orderdate = DateTime.Today;
                private int _Flag = 0;

                //complaint_itemorder
                public int  Flag
                {
                    get { return _Flag; }
                    set { _Flag = value; }
                }
                public int OrderId
                {
                    get { return _orderid; }
                    set { _orderid = value; }
                }
                public decimal QtyOrder
                {
                    get { return _qtyorder; }
                    set { _qtyorder = value; }
                }
                public DateTime OrderDate
                {
                    get { return _orderdate; }
                    set { _orderdate = value; }
                }

                public int Dept_Id
                {
                    get { return _dept_Id; }
                    set { _dept_Id = value; }
                }

                public string Department_name
                {
                    get { return _department_name; }
                    set { _department_name = value; }
                }

                public string Department_code
                {
                    get { return _department_code; }
                    set { _department_code = value; }
                }

                #endregion


                #region complaint User

                private string _c_status = string.Empty;                
                private int _c_no = 0;
                private int _C_ACTIVE_STATUS = 0;
                public int C_ACTIVE_STATUS
                {
                    get { return _C_ACTIVE_STATUS; }
                    set { _C_ACTIVE_STATUS = value; }
                }

                //Complaint User
                public int Admin_UA_no
                {
                    get { return _Admin_UA_no; }
                    set { _Admin_UA_no = value; }
                }
                public int C_No
                {
                    get { return _c_no; }
                    set { _c_no = value; }
                }

                public string C_Status
                {
                    get { return _c_status; }
                    set { _c_status = value; }
                }
                #endregion


                #region complaint Allotment

                private int _compallotmentid = 0;
                private int _EMP_DEPT = 0;
                private int _IDNO = 0;
                private int _USERNO = 0;


                // Complaint_allotment
                public int CompAllotmentId
                {
                    get { return _compallotmentid; }
                    set { _compallotmentid = value; }
                }  
                //Complaint Committee User
                public int EMP_DEPT
                {
                    get { return _EMP_DEPT; }
                    set { _EMP_DEPT = value; }
                }
                public int IDNO
                {
                    get { return _IDNO; }
                    set { _IDNO = value; }
                }
                public int USERNO
                {
                    get { return _USERNO; }
                    set { _USERNO = value; }
                }
                #endregion


                #region complaint workout

                private int _cwid = 0;
                private DateTime _workdate = DateTime.Today;
                private string _workout = string.Empty;
                private int _empid = 0;
                private int _qtyissued = 0;
                private int _dept_Id = 0;
                private string _department_name = string.Empty;
                private string _department_code = string.Empty; 
                private string _remark = string.Empty;
                private int _CELLALLOTMENTID = 0;

                //complaint_workout
                public int CwId
                {
                    get { return _cwid; }
                    set { _cwid = value; }
                }
                public DateTime WorkDate
                {
                    get { return _workdate; }
                    set { _workdate = value; }
                }
                public string WorkOut
                {
                    get { return _workout; }
                    set { _workout = value; }
                }
                public int EmpId
                {
                    get { return _empid; }
                    set { _empid = value; }
                }

                public int QtyIssued
                {
                    get { return _qtyissued; }
                    set { _qtyissued = value; }
                }

                public string AllotedTo
                {
                    get { return _AllotedTo; }
                    set { _AllotedTo = value; }
                }

                public int CompNatureId
                {
                    get { return _CompNatureId; }
                    set { _CompNatureId = value; }
                }           
                #endregion


                #region Priority Work
                private int _PWID = 0;
                private string _PWNAME = string.Empty;

                public int PWID
                {
                    get { return _PWID; }
                    set { _PWID = value; }
                }

                public string PWNAME
                {
                    get { return _PWNAME; }
                    set { _PWNAME = value; }
                }
                #endregion


                #region Staff Master

                private int _STAFFID = 0;
                private int _DEPTNO = 0;
                private int _COMPLAINT_NATURE_ID = 0;
                private string _STAFF_NAME = string.Empty;
                private string _MOBILENO = string.Empty;
                private string _EMAIL_ID = string.Empty;

                public int STAFFID
                {
                    get { return _STAFFID; }
                    set { _STAFFID = value; }
                }
                public int DEPTNO
                {
                    get { return _DEPTNO; }
                    set { _DEPTNO = value; }
                }
                public int COMPLAINT_NATURE_ID
                {
                    get { return _COMPLAINT_NATURE_ID; }
                    set { _COMPLAINT_NATURE_ID = value; }
                }

                public string STAFF_NAME
                {
                    get { return _STAFF_NAME; }
                    set { _STAFF_NAME = value; }
                }
                public string MOBILENO
                {
                    get { return _MOBILENO; }
                    set { _MOBILENO = value; }
                }
                public string EMAIL_ID
                {
                    get { return _EMAIL_ID; }
                    set { _EMAIL_ID = value; }
                }

                #endregion


                #region Area Name

                private int _AREAID = 0;
                private string _AREANAME = string.Empty;

                public int AREAID
                {
                    get { return _AREAID; }
                    set { _AREAID = value; }
                }

                public string AREANAME
                {
                    get { return _AREANAME; }
                    set { _AREANAME = value; }
                }
                #endregion

                private int _subid = 0;
                private int _ctid = 0;
                private int _sessionno = 0;
                private int _idno = 0;
                private int _courseno = 0;
                private int _electiveno = 0;
                private int _uano = 0;
                private string _OPENREMARK = string.Empty;
                private string _REWORK = string.Empty;
                private string _SUGGESTION = string.Empty;


                public int SubId
                {
                    get { return _subid; }
                    set { _subid = value; }
                }

                public int CTID
                {
                    get { return _ctid; }
                    set { _ctid = value; }
                }

                public int SessionNo
                {
                    get { return _sessionno; }
                    set { _sessionno = value; }
                }
                public int Idno
                {
                    get { return _idno; }
                    set { _idno = value; }
                }

                public int CourseNo
                {
                    get { return _courseno; }
                    set { _courseno = value; }
                }

                public int ElectiveNo
                {
                    get { return _electiveno; }
                    set { _electiveno = value; }
                }

                public int UA_NO
                {
                    get { return _uano; }
                    set { _uano = value; }
                }
                public string OPENREMARK
                {
                    get { return _OPENREMARK; }
                    set { _OPENREMARK = value; }
                }

                public string REWORK
                {
                    get { return _REWORK; }
                    set { _REWORK = value; }
                }

                public string SUGGESTION
                {
                    get { return _SUGGESTION; }
                    set { _SUGGESTION = value; }
                }

            }
        }//END: BusinessLayer.BusinessEntities

    }//END: NITPRM  

}//END: IITMS
