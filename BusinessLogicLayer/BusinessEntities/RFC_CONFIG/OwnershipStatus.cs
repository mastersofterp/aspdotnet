
//======================================================================================
// PROJECT NAME  : RFC COMMON                                                                
// MODULE NAME   : OwnershipStatus MASTER                             
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
namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLogicLayer.BusinessEntities.RFC_CONFIG
        {
            public class OwnershipStatus
            {
                public int OwnershipStatusId
                {
                    get;
                    set;
                }
                public string OwnershipStatusName
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
                public int OrganizationId
                {
                    get;
                    set;
                }
                public bool IsActive
                {
                    get;
                    set;
                }
            }
        }
    }
}
