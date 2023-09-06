
//======================================================================================
// PROJECT NAME  : RFC COMMON                                                                
// MODULE NAME   : Department MASTER                             
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
using System.Web;
namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLogicLayer.BusinessEntities.RFC_CONFIG
        {
            public class Department
            {
                public int DepartmentId
                {
                    get;
                    set;
                }
                public string DepartmentName
                {
                    get;
                    set;
                }
                public string DepartmentShortName
                {
                    get;
                    set;
                }
                public int CreatedBy
                {
                    get;
                    set;
                }
                public int ModifiedBy
                {
                    get;
                    set;
                }
                public string IPAddress
                {
                    get;
                    set;
                }
                public int OrganizationId
                {
                    get;
                    set;
                }
                public bool ActiveStatus
                {
                    get;
                    set;
                }
            }
        }
    }
}
