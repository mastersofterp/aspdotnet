//======================================================================================
// PROJECT NAME  : UAIMS
// MODULE NAME   : PAYROLL
// PAGE NAME     : PayITAutocomplete.cs
// CREATION DATE : 2/04/2012
// CREATED BY    : Mrunal Bansod
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

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
            public class PayITAutoComplete
            {
                private string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;


                public SqlDataReader AutoCompleteEmp(string preFix)
                {
                    SqlDataReader dr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[]
                            { 
                              new SqlParameter("@P_PREFIX",preFix)  
                            };
                        dr = objSQLHelper.ExecuteReaderSP("PKG_AUTOCOMPLETE_EMPLOYEE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.LibAutoComplete.AutoCompleteEmp() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return dr;
                }
            }
        }
    }
}
