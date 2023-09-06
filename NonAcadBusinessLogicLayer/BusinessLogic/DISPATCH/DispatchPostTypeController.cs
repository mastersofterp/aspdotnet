using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
            public  class DispatchPostTypeController
    {

        private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        public int AddUpdatePostType(DispatchPostType objposttype, IOTRAN objIO)
        {
            int retstatus = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[6];
                objParams[0] = new SqlParameter("@P_POSTTYPENO", objposttype.PostTypeNo);
                objParams[1] = new SqlParameter("@P_POSTTYPE", objposttype.PostType);
                objParams[2] = new SqlParameter("@P_COLLEGE_CODE", objIO.COLLEGE_CODE);
                objParams[3] = new SqlParameter("@P_CREATOR", objIO.CREATOR);
                objParams[4] = new SqlParameter("@P_CREATED_DATE", objIO.CREATED_DATE);
                objParams[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[5].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ADMN_IO_SP_POST_TYPE_MASTER_INSERT_UPDATE", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retstatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MR_Controller.AddUpdate_MR_Bill_Details->" + ex.ToString());
            }
            return retstatus;
        }



    }
        }
    }
}

