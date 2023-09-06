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
    public class QueryController
    {
        string connectionstring = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        public int InsertQueryCategoryData(int QCategoryId, string QueryCategory, int COLLEGE_CODE, int Status)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionstring);
                SqlParameter[] sqlParams = new SqlParameter[]
                        { 
                            
                            new SqlParameter("@P_CATEGORYNO",QCategoryId ),
                            new SqlParameter("@P_QUERY_CATEGORY",QueryCategory ),
                            new SqlParameter("@P_COLLEGE_CODE",COLLEGE_CODE),
                            new SqlParameter("@P_STATUS",Status),
                            new SqlParameter("@P_OUTPUT", SqlDbType.Int),
                         };

                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;
                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INS_UPD_QUERY_CATEGORY", sqlParams, true);
                if (ret != null && ret.ToString() == "1")
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                else if (ret != null && ret.ToString() == "2")
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                else if (ret != null && ret.ToString() == "3")
                    retStatus = Convert.ToInt32(CustomStatus.DuplicateRecord);
                else if (ret != null && ret.ToString() == "4")
                    retStatus = Convert.ToInt32(CustomStatus.RecordFound);
                else
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.InsertQueryCategoryData-> " + ex.ToString());
            }
            return retStatus;
        }

        public int InsertUserAllocationData(int QMUSERALLOCATIONID, int QCategoryId, int INCHARGEID, string MEMBERID, int USERNO)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionstring);
                SqlParameter[] sqlParams = new SqlParameter[]
                        { 
                            new SqlParameter("@P_QMUSERALLOCATIONID",QMUSERALLOCATIONID ),
                            new SqlParameter("@P_QCATEGORYID",QCategoryId ),
                            new SqlParameter("@P_INCHARGEID",INCHARGEID ),
                            new SqlParameter("@P_MEMBERID",MEMBERID ),
                            new SqlParameter("@P_USERNO",USERNO),
                            new SqlParameter("@P_OUTPUT", SqlDbType.Int),
                         };

                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;
                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INS_UPD_USER_ALLOCATION", sqlParams, true);
                if (ret != null && ret.ToString() == "1")
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                else if (ret != null && ret.ToString() == "2")
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                else if (ret != null && ret.ToString() == "-1001")
                    retStatus = Convert.ToInt32(CustomStatus.DuplicateRecord);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.InsertUserAllocationData-> " + ex.ToString());
            }
            return retStatus;
        }

        public DataSet GetUserAllocationList()
        {
            DataSet dspanelcreation = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionstring);
                SqlParameter[] sqlParams = new SqlParameter[0];
                dspanelcreation = objSQLHelper.ExecuteDataSetSP("PKG__GET_USER_ALLOCATION", sqlParams);

            }
            catch (Exception ex)
            {
                return dspanelcreation;
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GetUserAllocationList-> " + ex.ToString());
            }
            return dspanelcreation;
        }
        
    }
}
