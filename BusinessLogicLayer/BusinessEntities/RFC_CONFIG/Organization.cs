
//======================================================================================
// PROJECT NAME  : RFC COMMON                                                                
// MODULE NAME   : Organization MASTER                             
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
            public class Organization
            {
                public int OrganizationId
                {
                    get;
                    set;
                }
                public string Name
                {
                    get;
                    set;
                }
                public string NameInLocalLanguage
                {
                    get;
                    set;
                }
                public string Address
                {
                    get;
                    set;
                }
                public string AddressInLocalLanguage
                {
                    get;
                    set;
                }
                public string Email
                {
                    get;
                    set;
                }
                public string Website
                {
                    get;
                    set;
                }
                public string ContactName
                {
                    get;
                    set;
                }
                public string ContactNo
                {
                    get;
                    set;
                }
                public string ContactDesignation
                {
                    get;
                    set;
                }
                public string ContactEmail
                {
                    get;
                    set;
                }
                //public DateTime EstabishmentDate { get; set; }
                public string EstabishmentDate
                {
                    get;
                    set;
                }
                public int InstitutionTypeId
                {
                    get;
                    set;
                }
                public int OwnershipStatusId
                {
                    get;
                    set;
                }
                //public DateTime MISOrderDate { get; set; }
                public string MISOrderDate
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
                public string MACAddress
                {
                    get;
                    set;
                }
                public byte[] Logo
                {
                    get;
                    set;
                }
                public bool LogoFlag //Modified by Rishabh 11-10-2021
                {
                    get;
                    set;
                }
                public HttpPostedFileBase image1
                {
                    get;
                    set;
                }
            }
        }
    }
}