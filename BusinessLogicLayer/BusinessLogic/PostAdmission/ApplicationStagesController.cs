using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;


namespace IITMS.UAIMS.BusinessLayer.BusinessLogic
{
    public class ApplicationStagesController
    {
        string connectionstring = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        public int InsertApplicationStageData(int Stage_Id, string Stage_Name, string Description,int USERNO)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionstring);
                SqlParameter[] sqlParams = new SqlParameter[]
                        { 
                            
                            new SqlParameter("@STAGEID",Stage_Id ),
                            new SqlParameter("@STAGENAME",Stage_Name ),
                            new SqlParameter("@DESCRIPTION",Description),
                            new SqlParameter("@USERNO",USERNO),
                            new SqlParameter("@P_OUTPUT", SqlDbType.Int),
                         };

                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;
                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INS_UPD_APPLICATIONSTAGES", sqlParams, true);
                if (ret != null && ret.ToString() == "1")
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                else if (ret != null && ret.ToString() == "2")
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                else
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.InsertBasicDetailsData-> " + ex.ToString());
            }
            return retStatus;
        }
      
    }
}