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
            public class FacilityEntity
            {
                #region Private
                private int _MinFacilityNo;
                private string _MinFacilityName;
                private string _MinFacilityDetail;
	            private Boolean _IsActive;
	            private int _CreatedBy;
	            private int _ModifiedBy;
                private DateTime _CreatedDate;
	            private DateTime _ModifiedDate;
                private string _IPAddress;
                private string _MacAddress;
                private int _CollegeCode;

                /// <summary>
                /// Centralize facility Detail
                /// </summary>
                private int _CenFacilityNo;
                private string _CenFacilityName;
                private string _CenFacilityDetail;
                private string _Remark;

                /// <summary>
                /// Application Entry
                /// </summary>

                private int _ApplicationNo;                
                private DateTime _ApplicationDate;
                private DateTime _FromDate;
                private DateTime _ToDate;
                private char _PriorityLevel;
                private char _STATUS;
                
                private int _IDNO;

                private int _UANO;
                private Boolean _IsCancel;
                private string _Reason;

                private string _NoticeTitle;
                private string _NoticeDetailMessage;

                #endregion



                #region Public

                public string MessageTitle
                {
                    get { return _NoticeTitle; }
                    set { _NoticeTitle = value; }
                }


                public string Message
                {
                    get { return _NoticeDetailMessage; }
                    set { _NoticeDetailMessage = value; }
                }
                public int MinFacilityNo
                {
                    get { return _MinFacilityNo; }
                    set { _MinFacilityNo = value; }
                }
                public string MinFacilityName
                {
                    get { return _MinFacilityName; }
                    set { _MinFacilityName = value; }
                }

                public string MinFacilityDetail
                {
                    get { return _MinFacilityDetail; }
                    set { _MinFacilityDetail = value; }
                }


                public Boolean IsActive
                {
                    get { return _IsActive; }
                    set { _IsActive = value; }
                }
                public int CreatedBy
                {
                    get { return _CreatedBy; }
                    set { _CreatedBy = value; }
                }
                public int ModifiedBy
                {
                    get { return _ModifiedBy; }
                    set { _ModifiedBy = value; }
                }

                public DateTime CreatedDate
                {
                    get { return _CreatedDate; }
                    set { _CreatedDate = value; }
                }
                public DateTime ModifiedDate
                {
                    get { return _ModifiedDate; }
                    set { _ModifiedDate = value; }
                }

                public int CollegeCode
                {
                    get { return _CollegeCode; }
                    set { _CollegeCode = value; }
                }
                public string IPAddress
                {
                    get { return _IPAddress; }
                    set { _IPAddress = value; }
                }

                public string MacAddress
                {
                    get { return _MacAddress; }
                    set { _MacAddress = value; }
                }

                /// <summary>
                /// Centralize facility Detail
                /// </summary>
                public int CenFacilityNo
                {
                    get { return _CenFacilityNo; }
                    set { _CenFacilityNo = value; }
                }
                public string CenFacilityName
                {
                    get { return _CenFacilityName; }
                    set { _CenFacilityName = value; }
                }

                public string CenFacilityDetail
                {
                    get { return _CenFacilityDetail; }
                    set { _CenFacilityDetail = value; }
                }
                public string Remark
                {
                    get { return _Remark; }
                    set { _Remark = value; }
                }
            
                
                public char STATUS
                {
                    get { return _STATUS; }
                    set { _STATUS = value; }
                }
                public int UANO
                {
                    get { return _UANO; }
                    set { _UANO = value; }
                }
                public int IDNO
                {
                    get { return _IDNO; }
                    set { _IDNO = value; }
                }
                public int ApplicationNo
                {
                    get { return _ApplicationNo; }
                    set { _ApplicationNo = value; }
                }

                public DateTime ApplicationDate
                {
                    get { return _ApplicationDate; }
                    set { _ApplicationDate = value; }
                }
                public DateTime FromDate
                {
                    get { return _FromDate; }
                    set { _FromDate = value; }
                }
                public DateTime ToDate
                {
                    get { return _ToDate; }
                    set { _ToDate = value; }
                }
                public char PriorityLevel
                {
                    get { return _PriorityLevel; }
                    set { _PriorityLevel = value; }
                }
                public Boolean IsCancel
                {
                    get { return _IsCancel; }
                    set { _IsCancel = value; }
                }
                public string Reason
                {
                    get { return _Reason; }
                    set { _Reason = value; }
                }
                #endregion
            }
        }
    }
}

