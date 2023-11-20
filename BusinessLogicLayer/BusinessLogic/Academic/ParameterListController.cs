using System;
using System.Data;
using System.Web;
using IITMS;
using IITMS.UAIMS.BusinessLayer;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;
using System.Data.SqlTypes;
/*                            
---------------------------------------------------------------------------------------------------------------------------                            
Created By : RUTUAJA DAWLE                     
Created On :  25-10-2023                   
Purpose :                         
Version :                         
---------------------------------------------------------------------------------------------------------------------------                            
Version  Modified On   Modified By      Purpose                            
---------------------------------------------------------------------------------------------------------------------------                             
--------------------------------------------------------------------------------------------------------------------------                                               
*/       
namespace BusinessLogicLayer.BusinessLogic.Administration
{
   public  class ParameterListController
    {
       string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        public int AddParams(int UserNo, string XmlData)
        {
           int retStatus = 0;

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[4];
                
                objParams[0] = new SqlParameter("@P_UANO", UserNo);
                objParams[1] = new SqlParameter("@P_XML", XmlData);
                objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[2].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ADMI_PARAMETERLIST_UPDATE", objParams, true);
                retStatus = Convert.ToInt16(ret);

                return retStatus;
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLogicLayer.BusinessEntities.Administration.ParameterListController.AddParams() --> " + ex.ToString());
            }
        }

      
    }
}
   