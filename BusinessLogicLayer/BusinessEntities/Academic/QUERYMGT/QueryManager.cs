using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Linq;
using System.Text;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {
            public class QueryManager
            {
                #region Private Members

                private int _QMDepartmentID;

                private string _QMDepartmentName;

                private string _QMDepartmentShortname;

                private bool _IsActive;

                private int _QMRequestTypeID;

                private string _QMRequestTypeName;

                private string _QMRequestCategoryName;


                private int _QMRequestCategoryID;

                private string _QMRequestSubCategoryName;
	
                private string _GeneralInstruction;

                private bool _IsPaidService;
                private bool _IsEmergencyService;

                private double _PaidServiceAmount;
                private double _EmergencyServiceAmount;
                private int _EmergencyServiceHours;

                private int _QMRequestSubCategoryID;
                private int _QMUserAllocationID;
                private string _MemberID ;
                private int _InchargeID;
                private string _RequestType;

                private string _filePath;
                
                #endregion

                #region Public Properties

                public int QMDepartmentID
                {
                    get
                    {
                        return this._QMDepartmentID;
                    }
                    set
                    {
                        if ((this._QMDepartmentID != value))
                        {
                            this._QMDepartmentID = value;
                        }
                    }
                }

                public string QMDepartmentName
                {
                    get
                    {
                        return this._QMDepartmentName;
                    }
                    set
                    {
                        if ((this._QMDepartmentName != value))
                        {
                            this._QMDepartmentName = value;
                        }
                    }
                }

                public string QMDepartmentShortname
                {
                    get
                    {
                        return this._QMDepartmentShortname;
                    }
                    set
                    {
                        if ((this._QMDepartmentShortname != value))
                        {
                            this._QMDepartmentShortname = value;
                        }
                    }
                }

                public bool IsActive
                {
                    get
                    {
                        return this._IsActive;
                    }
                    set
                    {
                        if ((this._IsActive != value))
                        {
                            this._IsActive = value;
                        }
                    }
                }

                public int QMRequestTypeID
                {
                    get
                    {
                        return this._QMRequestTypeID;
                    }
                    set
                    {
                        if ((this._QMRequestTypeID != value))
                        {
                            this._QMRequestTypeID = value;
                        }
                    }
                }
                public int QMRequestCategoryID
                {
                    get
                    {
                        return this._QMRequestCategoryID;
                    }
                    set
                    {
                        if ((this._QMRequestCategoryID != value))
                        {
                            this._QMRequestCategoryID = value;
                        }
                    }
                }

                public string QMRequestTypeName
                {
                    get
                    {
                        return this._QMRequestTypeName;
                    }
                    set
                    {
                        if ((this._QMRequestTypeName != value))
                        {
                            this._QMRequestTypeName = value;
                        }
                    }
                }
                public string QMRequestCategoryName
                {
                    get
                    {
                        return this._QMRequestCategoryName;
                    }
                    set
                    {
                        if ((this._QMRequestCategoryName != value))
                        {
                            this._QMRequestCategoryName = value;
                        }
                    }
                }
                public string QMRequestSubCategoryName
                {
                    get
                    {
                        return this._QMRequestSubCategoryName;
                    }
                    set
                    {
                        if ((this._QMRequestSubCategoryName != value))
                        {
                            this._QMRequestSubCategoryName = value;
                        }
                    }
                }

                public string GeneralInstruction
                {
                    get
                    {
                        return this._GeneralInstruction;
                    }
                    set
                    {
                        if ((this._GeneralInstruction != value))
                        {
                            this._GeneralInstruction = value;
                        }
                    }
                }
                public bool IsEmergencyService
                {
                    get
                    {
                        return this._IsEmergencyService;
                    }
                    set
                    {
                        if ((this._IsEmergencyService != value))
                        {
                            this._IsEmergencyService = value;
                        }
                    }
                }

                public double EmergencyServiceAmount
                {
                    get
                    {
                        return this._EmergencyServiceAmount;
                    }
                    set
                    {
                        if ((this._EmergencyServiceAmount != value))
                        {
                            this._EmergencyServiceAmount = value;
                        }
                    }
                }
                public bool IsPaidService
                {
                    get
                    {
                        return this._IsPaidService;
                    }
                    set
                    {
                        if ((this._IsPaidService != value))
                        {
                            this._IsPaidService = value;
                        }
                    }
                }

                public double PaidServiceAmount
                {
                    get
                    {
                        return this._PaidServiceAmount;
                    }
                    set
                    {
                        if ((this._PaidServiceAmount != value))
                        {
                            this._PaidServiceAmount = value;
                        }
                    }
                }

                public int EmergencyServiceHours
                {
                    get
                    {
                        return this._EmergencyServiceHours;
                    }
                    set
                    {
                        if ((this._EmergencyServiceHours != value))
                        {
                            this._EmergencyServiceHours = value;
                        }
                    }
                }


                public int QMRequestSubCategoryID
                {
                    get
                    {
                        return this._QMRequestSubCategoryID;
                    }
                    set
                    {
                        if ((this._QMRequestSubCategoryID != value))
                        {
                            this._QMRequestSubCategoryID = value;
                        }
                    }
                }

                public int QMUserAllocationID
                {
                    get
                    {
                        return this._QMUserAllocationID;
                    }
                    set
                    {
                        if ((this._QMUserAllocationID != value))
                        {
                            this._QMUserAllocationID = value;
                        }
                    }
                }

                public int InchargeID
                {
                    get
                    {
                        return this._InchargeID;
                    }
                    set
                    {
                        if ((this._InchargeID != value))
                        {
                            this._InchargeID = value;
                        }
                    }
                }
                public string MemberID
                {
                    get
                    {
                        return this._MemberID;
                    }
                    set
                    {
                        if ((this._MemberID != value))
                        {
                            this._MemberID = value;
                        }
                    }
                }

                public string RequestType
                {
                    get
                    {
                        return this._RequestType;
                    }
                    set
                    {
                        if ((this._RequestType != value))
                        {
                            this._RequestType = value;
                        }
                    }
                }


              

                #endregion
            }
        }
    }
}
