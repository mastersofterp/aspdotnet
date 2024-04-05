/*                                                    
---------------------------------------------------------------------------------------------------------------------------                                                            
Created By : Kajal Jaiswal                                                        
Created On : 22-03-2024                               
Purpose    : Event Calendar                                 
Version    : 1.0.0                                                    
---------------------------------------------------------------------------------------------------------------------------                                                              
Version   Modified On   Modified By      Purpose                                                              
---------------------------------------------------------------------------------------------------------------------------                                                              
           
*/

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Data;
    using System.Data.SqlClient;
    using IITMS.SQLServer.SQLDAL;
    using IITMS.UAIMS;
    using IITMS;

    namespace BusinessLogicLayer.BusinessLogic
    {
       public class EventCalendarController
        {
               private string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
               public DataSet GetAllEventList(int UANO )
               {
                   DataSet ds = null;
                   try
                   {
                       SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                       SqlParameter[] sqlParams = new SqlParameter[1];
                       sqlParams[0] = new SqlParameter("@P_UA_NO", UANO);
                       ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_CALENDAR_SCHEDULE_EVENT", sqlParams);
                   }
                   catch (Exception ex)
                   {

                       throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GetAllInterviewTestList-> " + ex.ToString());
                   }
                   return ds;
               }

      
        }
    }
