using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLogicLayer.BusinessEntities.Academic
        {
            public class ModuleConfig
            {
                private string _fieldName = string.Empty;
                private int _configid = 0; // 
                private string _OUTSTANDING_MESSAGE = string.Empty;
                private int _Fee_Receipt_Copies = 0;

                public bool RegnoGenFeeCollection
                {
                    get;
                    set;
                }
                public bool NewStudEmailSend
                {
                    get;
                    set;
                }

                public bool FacultyAdvisorApp
                {
                    get;
                    set;
                }

                public bool NewStudUserCreation
                {
                    get;
                    set;
                }
                public bool TPDocverificationAllow
                {
                    get;
                    set;
                }
                public bool TPPaymentLinkSendMail
                {
                    get;
                    set;
                }

                public bool FeeCollUserCreation
                {
                    get;
                    set;
                }

                public int Configid
                {
                    get
                    {
                        return _configid;
                    }
                    set
                    {
                        _configid = value;
                    }
                }
                public string FieldName
                {
                    get
                    {
                        return _fieldName;
                    }
                    set
                    {
                        _fieldName = value;
                    }
                }
                public bool AllowRegno
                {
                    get;
                    set;
                }
                public bool AllowRollno
                {
                    get;
                    set;
                }
                public bool AllowEnrollno
                {
                    get;
                    set;
                }

                public bool CourseExmRegSame
                {
                    get;
                    set;
                }



                public bool StudInfoMandate
                {
                    get;
                    set;
                }


                public bool OnlinebtnStudadm
                {
                    get;
                    set;
                }

                public int EmailType
                {
                    get;
                    set;
                }

                public bool SemAdmWithPayment
                {
                    get;
                    set;
                }

                public bool OUTSTANDING_FEECOLLECTION
                {
                    get;
                    set;
                }
                public string OUTSTANDING_MESSAGE
                {
                    get
                    {
                        return _OUTSTANDING_MESSAGE;
                    }
                    set
                    {
                        _OUTSTANDING_MESSAGE = value;
                    }
                }

                // Added by Gopal M 01112023
                public bool FEE_HEAD_GROUP
                {
                    get;
                    set;
                }
                public int FEE_RECEIPT_COPIES
                    {
                    get
                        {
                        return _Fee_Receipt_Copies;
                        }
                    set
                        {
                        _Fee_Receipt_Copies = value;
                        }
                    }

                public bool TOSHOW_FEEREC_STUDLOGIN
                    {
                    get;
                    set;
                    }

            }
        }
    }
}