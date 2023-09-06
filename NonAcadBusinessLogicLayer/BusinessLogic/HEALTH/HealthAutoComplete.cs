//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : HEALTH                              
// CREATION DATE : 13-FEB-2016                                                        
// CREATED BY    : MRUNAL SINGH                                      
// MODIFIED DATE :
// MODIFIED DESC :
//====================================================================================== 
using System;
using System.Data;
using System.Web;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities.Health;


namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLogicLayer.BusinessLogic //.Health
        {
            public class HealthAutoComplete
            {             

                private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                public SqlDataReader AutoCompleteItemName(string preFix)
                {
                    SqlDataReader sdr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[]
                            { 
                              new SqlParameter("@P_PREFIX",preFix)  
                            };
                        sdr = objSQLHelper.ExecuteReaderSP("PKG_HEALTH_AUTOCOMPLETE_ITEMNAME", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.HealthAutoComplete.AutoCompleteItemName() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return sdr;
                }

                public SqlDataReader AutoCompletePatientName(string preFix)
                {
                    SqlDataReader dr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[]
                            { 
                              new SqlParameter("@P_PREFIX",preFix)  
                            };
                        dr = objSQLHelper.ExecuteReaderSP("PKG_AUTOCOMPLETE_PATIENTNAME", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessEntities.HealthAutoComplete.AutoCompletePatientName() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return dr;
                }

                public SqlDataReader AutoCompleteDoctorName(string preFix)
                {
                    SqlDataReader dr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[]
                            { 
                              new SqlParameter("@P_PREFIX",preFix)  
                            };
                        dr = objSQLHelper.ExecuteReaderSP("PKG_AUTOCOMPLETE_DOCTORNAME", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessEntities.HealthAutoComplete.AutoCompleteDoctorName() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return dr;
                }

                public SqlDataReader AutoCompleteStudName(string preFix)
                {
                    SqlDataReader dr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[]
                            { 
                              new SqlParameter("@P_PREFIX",preFix)  
                            };
                        dr = objSQLHelper.ExecuteReaderSP("PKG_AUTOCOMPLETE_STUDNAME", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessEntities.HealthAutoComplete.AutoCompleteStudName() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return dr;
                }

                public SqlDataReader AutoCompleteEmpName(string preFix)
                {
                    SqlDataReader dr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[]
                            { 
                              new SqlParameter("@P_PREFIX",preFix)  
                            };
                        dr = objSQLHelper.ExecuteReaderSP("PKG_AUTOCOMPLETE_EMPNAME", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessEntities.HealthAutoComplete.AutoCompleteEmpName() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return dr;
                }

            }
        }
    }
}
