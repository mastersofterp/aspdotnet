using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using IITMS.SQLServer.SQLDAL;
using IITMS.NITPRM.BusinessLayer.BusinessEntities;

namespace IITMS.NITPRM.BusinessLayer.BusinessLogic
{
    public class CommanController
    {
        string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
 

        public DataSet FillDropDown(string TableName, string Column1, string Column2, string wherecondition, string orderby)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[5];
                objParams[0] = new SqlParameter("@P_TABLENAME", TableName);
                objParams[1] = new SqlParameter("@P_COLUMNNAME_1", Column1);
                objParams[2] = new SqlParameter("@P_COLUMNNAME_2", Column2);
                if (!wherecondition.Equals(string.Empty))
                    objParams[3] = new SqlParameter("@P_WHERECONDITION", wherecondition);
                else
                    objParams[3] = new SqlParameter("@P_WHERECONDITION", DBNull.Value);
                if (!orderby.Equals(string.Empty))
                    objParams[4] = new SqlParameter("@P_ORDERBY", orderby);
                else
                    objParams[4] = new SqlParameter("@P_ORDERBY", DBNull.Value);
                ds = objSQLHelper.ExecuteDataSetSP("spcommonfilldropdown_select", objParams);
            }
            catch (Exception ex)
            {
                throw;
            }
            return ds;
        }


        public List<CommonModel> FillDropDownOnly(string TableName, string Column1, string Column2, string wherecondition, string orderby)
        {
            List<CommonModel> fill = new List<CommonModel>();
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[5];
                objParams[0] = new SqlParameter("@P_TABLENAME", TableName);
                objParams[1] = new SqlParameter("@P_COLUMNNAME_1", Column1);
                objParams[2] = new SqlParameter("@P_COLUMNNAME_2", Column2);
                if (!wherecondition.Equals(string.Empty))
                    objParams[3] = new SqlParameter("@P_WHERECONDITION", wherecondition);
                else
                    objParams[3] = new SqlParameter("@P_WHERECONDITION", DBNull.Value);
                if (!orderby.Equals(string.Empty))
                    objParams[4] = new SqlParameter("@P_ORDERBY", orderby);
                else
                    objParams[4] = new SqlParameter("@P_ORDERBY", DBNull.Value);
                DataSet ds = null;
                ds = objSQLHelper.ExecuteDataSetSP("spcommonfilldropdown_select_new", objParams);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        fill.Add(new CommonModel()
                        {
                            Value = int.Parse(dr["value"].ToString()),
                            Text = dr["text"].ToString(),
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return fill;
        }
    }
}
