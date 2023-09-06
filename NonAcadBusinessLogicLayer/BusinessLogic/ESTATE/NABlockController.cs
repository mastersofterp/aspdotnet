using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;

namespace IITMS.UAIMS.BusinessLayer.BusinessEntities
{
    public class NABlockController
    {

        string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
        public int AddUpdateBlock(BlockMaster objBlock)
        {
            int status = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_BLOCK_NAME",objBlock.BLOCK_NAME),
                    new SqlParameter("@P_OUTPUT", objBlock.BLOCKNO)
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ESTATE_BLOCK_MASTER_INSERT_UPDATE", sqlParams, true);

                if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                {
                    status = Convert.ToInt32(CustomStatus.RecordSaved);
                }
                else if (obj.ToString() == "-1001")
                {
                    status = Convert.ToInt32(CustomStatus.DuplicateRecord);
                }
                else
                {
                    status = Convert.ToInt32(CustomStatus.Error);
                }
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BlockController.AddUpdateBlock() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }
    }
}
