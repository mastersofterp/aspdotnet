
//======================================================================================
// PROJECT NAME  : RFC COMMON                                                                
// MODULE NAME   : Mapping MASTER                             
// CREATION DATE : 08-OCTOMBER-2021                                                         
// CREATED BY    : RISHABH
// ADDED BY      : 
// ADDED DATE    :                                       
// MODIFIED DATE :                                                                      
// MODIFIED DESC :                                                                      
//======================================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IITMS;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLogicLayer.BusinessEntities.RFC_CONFIG
        {
            public class Mapping
            {
                public int OrganizationCollegeId
                {
                    get;
                    set;
                }
                public int OrganizationId
                {
                    get;
                    set;
                }
                public int CollegeId
                {
                    get;
                    set;
                }
                public string OrganizationCollegeName
                {
                    get;
                    set;
                }
                public bool ActiveStatus
                {
                    get;
                    set;
                }
                //Added By Rishabh on 13/10/2021
                public int CollegeDepartmentId
                {
                    get;
                    set;
                }

                public int DepartmentId
                {
                    get;
                    set;
                }
                public string CollegeDepartmentName
                {
                    get;
                    set;
                }

                public int DegreeBranchId
                {
                    get;
                    set;
                }
                public int DegreeDepartmentId
                {
                    get;
                    set;
                }
                public int BranchId
                {
                    get;
                    set;
                }

                public string DegreeBranchName
                {
                    get;
                    set;
                }
                //End
                //Added by Dileep Kare on 27/10/2021
                public int Duration
                {
                    get;
                    set;
                }

                public string ShortName
                {
                    get;
                    set;
                }

                public string Branch_Code
                {
                    get;
                    set;
                }

                public string College_Code
                {
                    get;
                    set;
                }
                public int College_Type
                {
                    get;
                    set;
                }

                public int Programme_Type
                {
                    get;
                    set;
                }
                public int Eng_Status
                {
                    get;
                    set;
                }
                public int Intake_I
                {
                    get;
                    set;
                }
                public int Intake_II
                {
                    get;
                    set;
                }
                public int Intake_III
                {
                    get;
                    set;
                }
                public int Intake_IV
                {
                    get;
                    set;
                }
                public int Intake_V
                {
                    get;
                    set;
                }
                public string College_Name //Added By Rishabh on 24/11/2021
                {
                    get;
                    set;
                }
                public int IsSpecialisation
                {
                    get;
                    set;
                }
                public int CoreBranchNo
                {
                    get;
                    set;
                }
            }
        }
    }
}
