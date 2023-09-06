
//======================================================================================
// PROJECT NAME  : RFC COMMON                                                                
// MODULE NAME   : BRANCH MASTER                             
// CREATION DATE : 08-OCTOMBER-2021                                                         
// CREATED BY    : S.Patil
// ADDED BY      : 
// ADDED DATE    :                                       
// MODIFIED DATE :                                                                      
// MODIFIED DESC :                                                                      
//======================================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLogicLayer.BusinessEntities.RFC_CONFIG
        {
            public class ConfigAffilationType
            {
                public int AffilationTypeId
                {
                    get;
                    set;
                }
                public string AffilationName
                {
                    get;
                    set;
                }
                public bool ActiveStatus
                {
                    get;
                    set;
                }
                public int CreatedBy
                {
                    get;
                    set;
                }
                public DateTime CreatedDate
                {
                    get;
                    set;
                }
                public int ModifiedBy
                {
                    get;
                    set;
                }
                public DateTime ModifiedDate
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
            }
        }
    }
}