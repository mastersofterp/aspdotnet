﻿using System;
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
            }
        }
    }
}