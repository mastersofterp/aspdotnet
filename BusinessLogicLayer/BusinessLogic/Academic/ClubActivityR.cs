//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC                                                             
// PAGE NAME     : ClubActivityR                               
// CREATION DATE : 25-10-2023                                                   
// CREATED BY    : Vipul Tichakule                                                        
// MODIFIED DATE : 
// MODIFIED BY                                                     
// MODIFIED DESC :                                                                  
//======================================================================================

using IITMS;
using IITMS.SQLServer.SQLDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BusinessLogicLayer.BusinessLogic.Academic
{
    public class ClubActivityR
    {

        string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        public DataSet GetClubActivityReport( string clubno, int session)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {                     
                    //new SqlParameter("@P_SESSIONNO", sessionNo),
                    new SqlParameter("@P_CLUB_ACTIVITY_NO",clubno),
                    new SqlParameter("@P_SESSION_ID",session),
  
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_PDA_CLUB_ACTIVITY_REPORT", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ClubActivityR.GetClubActivityReport() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }
    }
}
