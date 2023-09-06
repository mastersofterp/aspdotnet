//======================================================================================
// PROJECT NAME  : UAIMS
// MODULE NAME   : ITLE
// PAGE NAME     : 
// CREATION DATE : 01/02/2014
// CREATED BY    : ZUBAIR AHMAD
// MODIFIED DATE :
// MODIFIED DESC :
//======================================================================================

using System;
using System.Data;
using System.Web;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

using System.Data.SqlClient;


namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
           public class ItleAutoComplete
            {
                string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                // USED IN QUESTION BANK FORM
                
                public SqlDataReader AutoCompleteTopic(string preFix)
                {
                    SqlDataReader dr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[]
                            { 
                              new SqlParameter("@P_PREFIX",preFix)  
                            };
                        dr = objSQLHelper.ExecuteReaderSP("PKG_ITLE_AUTOCOMPLETE_TOPIC", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessEntities.ItleAutoComplete.AutoCompleteTopic() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return dr;
                }
               //END

               //USED IN STUDENT TEST RESULT FORM

                public SqlDataReader AutoComplete_Stud_ID(string preFix)
                {
                    SqlDataReader dr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[]
                            { 
                              new SqlParameter("@P_PREFIX",preFix)  
                            };
                        dr = objSQLHelper.ExecuteReaderSP("PKG_ITLE_AUTOCOMPLETE_STUDENT_ID", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessEntities.ItleAutoComplete.AutoCompleteTopic() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return dr;
                }


                public SqlDataReader AutoComplete_Stud_Name(string preFix)
                {
                    SqlDataReader dr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[]
                            { 
                              new SqlParameter("@P_PREFIX",preFix)  
                            };
                        dr = objSQLHelper.ExecuteReaderSP("PKG_ITLE_AUTOCOMPLETE_STUDENT_NAME", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessEntities.ItleAutoComplete.AutoCompleteTopic() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return dr;
                }


                // USED IN MAIL MESSAGE PAGE

                public SqlDataReader SearchInboxMail(string preFix)
                {
                    SqlDataReader dr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[]
                            { 
                              new SqlParameter("@P_PREFIX",preFix),
                              
  
                            };
                        dr = objSQLHelper.ExecuteReaderSP("PKG_ITLE_AUTOCOMPLETE_SEARCH_MAIL", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessEntities.ItleAutoComplete.AutoCompleteTopic() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return dr;
                }
                //END

               
            }
        }
    }
}
