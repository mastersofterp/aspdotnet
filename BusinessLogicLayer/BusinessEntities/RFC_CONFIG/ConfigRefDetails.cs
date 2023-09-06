
//======================================================================================
// PROJECT NAME  : RFC COMMON                                                                
// MODULE NAME   : ConfigRefDetails                 
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
            public class ConfigRefDetails
            {
                //Added By Rishabh on 20/10/2021
                public int ReferenceDetailsId
                {
                    get;
                    set;
                }
                public int OrganizationId
                {
                    get;
                    set;
                }
                public string ProjectName
                {
                    get;
                    set;
                }
                public string ServerName
                {
                    get;
                    set;
                }
                public string UserId
                {
                    get;
                    set;
                }
                public string Password
                {
                    get;
                    set;
                }
                public string DBName
                {
                    get;
                    set;
                }

                public string OrganizationUrl //Added By Rishabh on 04/12/2021
                {
                    get;
                    set;
                }

                public string DefaultPage //Added By Rishabh on 07/12/2021
                {
                    get;
                    set;
                }

            }
        }
    }
}
