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

                public int Insert_Update_GatePassRequest(GatePassRequest objGatePassRequest, int hostel)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Add Block info 
                        objParams = new SqlParameter[20];
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
                        objParams[15] = new SqlParameter("@P_SESSION", Convert.ToInt32(System.Web.HttpContext.Current.Session["hostel_session"]));
                        objParams[16] = new SqlParameter("@P_HOSTEL", hostel);
                        objParams[17] = new SqlParameter("@P_STUDTYPE", objGatePassRequest.StudType);
                        //objParams[18] = new SqlParameter("@P_PATH", objGatePassRequest.ApprPath);
                        objParams[18] = new SqlParameter("@P_ADMIN_UANO", objGatePassRequest.Admin_UANO);
                        objParams[19]=new SqlParameter("@P_OUT",SqlDbType.Int);
                        objParams[19].Direction= ParameterDirection.Output;

                        retStatus=Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_HOSTEL_GATEPASSREQUEST_INSERT_UPDATE", objParams, true));
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PurposeController.Insert_Update_Purpose-> " + ex.ToString());
                    }

                    return retStatus;
                }

                public DataSet GetAllGatePass(string Applydate,int Purpose,string Todate,string Fromdate,string Status) //parameters added by Himanshu tamrakar 05042024
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_IDNO", Convert.ToInt32(System.Web.HttpContext.Current.Session["idno"]));
                        objParams[1] = new SqlParameter("@P_USERTYPE", Convert.ToInt32(System.Web.HttpContext.Current.Session["usertype"]));

                        //below code added by Himanshu tamrakar 05042024
                        objParams[2] = new SqlParameter("@P_TODATE", Todate);
                        objParams[3] = new SqlParameter("@P_FROMDATE", Fromdate);
                        objParams[4] = new SqlParameter("@P_PURPOSE",Purpose);
                        objParams[5] = new SqlParameter("@P_APPLYDATE",Applydate);
                        objParams[6] = new SqlParameter("@P_STATUS", Status);
                        

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_HOSTEL_GATEPASS_GET_ALL", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.HostelGatePassController.GetAllGatePass() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                public DataSet GetGatePass(int GatePassRequestNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_HGP_NO", GatePassRequestNo);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_HOSTEL_GATEPASS_REQUESTDETAILS_BY_ID", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GatePassRequestController.GetGatePass-> " + ex.ToString());
                    }
                    return ds;
                }
            }
        }
    }
}
