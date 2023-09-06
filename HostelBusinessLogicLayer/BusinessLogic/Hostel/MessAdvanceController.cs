//======================================================================================
// PROJECT NAME  : UAIMS (GPM)                                                               
// MODULE NAME   : BUSINESS LOGIC FILE [BLOCK INFO]                                  
// CREATION DATE : 03-MARCH-2013                                                       
// CREATED BY    : OVES KHAN                                       
// MODIFIED DATE :
// MODIFIED DESC :
//======================================================================================  
using System;
using System.Data;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;


namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
            public class MessAdvanceController
            {
                private string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                public int AddMessInfo(MessAdvance objmess)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Add Block info 
                        objParams = new SqlParameter[8];
                        
                        objParams[0] = new SqlParameter("@P_MESS_NO", objmess.MESS_NO);
                        objParams[1] = new SqlParameter("@P_COLLEGE_CODE", objmess.CollegeCode);
                        objParams[2] = new SqlParameter("@P_ADV_DATE", objmess.ADV_DATE);
                        objParams[3] = new SqlParameter("@P_ADV_REMARK",objmess.ADV_REMARK);                        
                        objParams[4] = new SqlParameter("@P_ADV_AMOUNT", objmess.ADV_AMOUNT);
                        objParams[5] = new SqlParameter("@P_COMM_MEMBER", objmess.Comm_Member);
                        objParams[6] = new SqlParameter("@P_SESSION_NO", objmess.Session_No);

                        objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[7].Direction = ParameterDirection.Output;
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_MESS_ADV_INSERT", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MessAdvanceController.AddMessInfo-> " + ex.ToString());
                    }

                    return retStatus;
                }
                public int UpdateMessInfo(MessAdvance objmess)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Add Block info 
                        objParams = new SqlParameter[9];
                        objParams[0] = new SqlParameter("@P_MESSSRNO", objmess.MESSRNO);
                        objParams[1] = new SqlParameter("@P_MESS_NO", objmess.MESS_NO);
                        objParams[2] = new SqlParameter("@P_COLLEGE_CODE", objmess.CollegeCode);
                        objParams[3] = new SqlParameter("@P_ADV_DATE", objmess.ADV_DATE);
                        objParams[4] = new SqlParameter("@P_ADV_REMARK", objmess.ADV_REMARK);
                        objParams[5] = new SqlParameter("@P_ADV_AMOUNT", objmess.ADV_AMOUNT);
                        objParams[6] = new SqlParameter("@P_COMM_MEMBER", objmess.Comm_Member);
                        objParams[7] = new SqlParameter("@P_SESSION_NO", objmess.Session_No);

                        objParams[8] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[8].Direction = ParameterDirection.Output;
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_MESS_ADV_UPDATE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MessAdvanceController.UpdateMessInfo-> " + ex.ToString());
                    }
                    return retStatus;


                }

              

                public DataSet GetMessInfo()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_MESS_ADV", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MessAdvanceController.GetMessInfo-> " + ex.ToString());
                    }
                    return ds;
                }

                public int DeleteMess(MessAdvance objmess)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_MESSSRNO", objmess.MESS_NO);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_DELETE_MESS_ADV", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                       
                    }
                    catch (Exception ex)
                    {
                        //return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MessAdvanceController.DeleteMess-> " + ex.ToString());
                    }
                    return retStatus;
                }


               
                
                public SqlDataReader GetMess(int mess_no)
                {
                    SqlDataReader dr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_MESSSRNO",mess_no);

                        dr = objSQLHelper.ExecuteReaderSP("PKG_GET_MESS_ADV_BY_MESSNO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return dr;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MessAdvanceController.GetMess-> " + ex.ToString());
                    }
                    return dr;
                }
            }
        }
    }
}
