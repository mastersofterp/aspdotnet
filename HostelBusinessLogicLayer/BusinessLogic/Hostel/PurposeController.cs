using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;
using System.Data;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
            public class PurposeController
            {
                private string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                public int Insert_Update_Purpose(Purpose objPurpose)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Add Block info 
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_PURPOSE_NO", objPurpose.PurposeNo);
                        objParams[1] = new SqlParameter("@P_PURPOSE_NAME", objPurpose.PurposeName);
                        objParams[2] = new SqlParameter("@P_ISACTIVE", objPurpose.IsActive);
                        objParams[3] = new SqlParameter("@P_COLLEGE_CODE", Convert.ToInt32(System.Web.HttpContext.Current.Session["colcode"]));
                        objParams[4] = new SqlParameter("@P_ORGANIZATION", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]));


                        if (objSQLHelper.ExecuteNonQuerySP("PKG_HOSTEL_PURPOSE_INSERT_UPDATE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PurposeController.Insert_Update_Purpose-> " + ex.ToString());
                    }

                    return retStatus;
                }

                public SqlDataReader GetPurpose(int purpose_no)
                {
                    SqlDataReader dr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_PURPOSE_NO", purpose_no);

                        dr = objSQLHelper.ExecuteReaderSP("PKG_HOSTEL_PURPOSE_MASTER_GET_BY_ID", objParams);
                    }
                    catch (Exception ex)
                    {
                        return dr;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PurposeController.GetPurpose-> " + ex.ToString());
                    }
                    return dr;
                }

                public DataSet GetAllPurpose()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_HOSTEL_PURPOSE_GET_ALL", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.HostelPurposeController.GetAllPurpose() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }
            }
        }
    }
}
