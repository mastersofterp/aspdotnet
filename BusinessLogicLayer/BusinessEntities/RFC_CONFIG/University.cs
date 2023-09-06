
//======================================================================================
// PROJECT NAME  : RFC COMMON                                                                
// MODULE NAME   : University MASTER                             
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
            public class University
            {
                public int  Universityid
                {
                    get;
                    set;
                }
                public string UniversityName
                {
                    get;
                    set;
                }
               public int Stateid
               {
                   get;
                   set;
               }
                public bool Status
                {
                    get;
                    set;
                }
                public int  CreatedBy
                {
                    get;
                    set;
                }
                public DateTime CreatedDate
                {
                    get;
                    set;
                }
                public int Modifiedby
                {
                    get;
                    set;
                }
                public DateTime ModifiedDate
                {
                    get;
                    set;
                }
                public string Ipaddress
                {
                    get;
                    set;
                }
                public string MacAddress
                {
                    get;
                    set;
                }
            }
        }
    }
}

