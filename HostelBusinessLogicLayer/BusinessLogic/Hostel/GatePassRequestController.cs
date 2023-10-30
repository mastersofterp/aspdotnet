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
            public class GatePassRequestController
            {
                private string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                public int Insert_Update_GatePassRequest(GatePassRequest objGatePassRequest)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Add Block info 
                        objParams = new SqlParameter[16];
                        objParams[0] = new SqlParameter("@P_IDNO", objGatePassRequest.IDNO);
                        objParams[1] = new SqlParameter("@P_HGP_ID", objGatePassRequest.GatePassNo);
                        objParams[2] = new SqlParameter("@P_OUTDATE", objGatePassRequest.OutDate);
                        objParams[3] = new SqlParameter("@P_OUT_HOUR_FROM", objGatePassRequest.OutHourFrom);
                        objParams[4] = new SqlParameter("@P_OUT_MIN_FROM", objGatePassRequest.OutMinFrom);
                        objParams[5] = new SqlParameter("@P_OUT_AM_PM", objGatePassRequest.OutAMPM);
                        objParams[6] = new SqlParameter("@P_INDATE", objGatePassRequest.InDate);
                        objParams[7] = new SqlParameter("@P_IN_HOUR_FROM", objGatePassRequest.InHourFrom);
                        objParams[8] = new SqlParameter("@P_IN_MIN_FROM", objGatePassRequest.InMinFrom);
                        objParams[9] = new SqlParameter("@P_IN_AM_PM", objGatePassRequest.InAMPM);
                        objParams[10] = new SqlParameter("@P_PURPOSE_ID", objGatePassRequest.PurposeID);
                        objParams[11] = new SqlParameter("@P_PURPOSE_OTHER", objGatePassRequest.PurposeOther);
                        objParams[12] = new SqlParameter("@P_REMARKS", objGatePassRequest.Remarks);
                        objParams[13] = new SqlParameter("@P_COLLEGE_CODE", Convert.ToInt32(System.Web.HttpContext.Current.Session["colcode"]));
                        objParams[14] = new SqlParameter("@P_ORGANIZATION", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]));
                        objParams[15] = new SqlParameter("@P_Session", Convert.ToInt32(System.Web.HttpContext.Current.Session["hostel_session"]));

                          
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_HOSTEL_GATEPASSREQUEST_INSERT_UPDATE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PurposeController.Insert_Update_Purpose-> " + ex.ToString());
                    }

                    return retStatus;
                }

                public DataSet GetAllGatePass()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_IDNO", Convert.ToInt32(System.Web.HttpContext.Current.Session["idno"]));
                        objParams[1] = new SqlParameter("@P_USERTYPE", Convert.ToInt32(System.Web.HttpContext.Current.Session["usertype"]));

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_HOSTEL_GATEPASS_GET_ALL", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.HostelGatePassController.GetAllGatePass() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                public SqlDataReader GetGatePass(int gatepass_no)
                {
                    SqlDataReader dr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_HGP_ID", gatepass_no);

                        dr = objSQLHelper.ExecuteReaderSP("PKG_HOSTEL_GATEPASS_GET_BY_ID", objParams);
                    }
                    catch (Exception ex)
                    {
                        return dr;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PurposeController.GetPurpose-> " + ex.ToString());
                    }
                    return dr;
                }
            }
        }
    }
}
