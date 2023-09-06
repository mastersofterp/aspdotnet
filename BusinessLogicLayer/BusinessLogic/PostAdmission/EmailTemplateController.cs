using IITMS;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.BusinessLogic.Academic
{
    public class EmailTemplateController
    {
        private string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        #region Board Grade Scheme Business Logic Methods
        public int InsertUpdate_EmailTemplate(int TEMPLATEID, int TEMPTYPEID, string TEMPLATENAME, string EMAILSUBJECT, string TEMPLATETEXT, bool ACTIVESTATUS)
        {
            int retStatus = 0;

            try
            {
                /*
                @P_TEMPLATEID		INT,
                @P_TEMPTYPEID		INT,    
                @P_TEMPLATENAME	NVARCHAR(100),
                @P_EMAILSUBJECT	NVARCHAR(100),  
                @P_TEMPLATETEXT	NVARCHAR(MAX),  
                @P_ACTIVESTATUS	BIT,
                @P_OUT				INT OUT                          
                */
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = new SqlParameter[]
                        { 

                                new SqlParameter("@P_TEMPLATEID",TEMPLATEID),
                                new SqlParameter("@P_TEMPTYPEID",  TEMPTYPEID ), 
                                new SqlParameter("@P_TEMPLATENAME",TEMPLATENAME),
                                new SqlParameter("@P_EMAILSUBJECT", EMAILSUBJECT ),
                                new SqlParameter("@P_TEMPLATETEXT", TEMPLATETEXT ),
                                new SqlParameter("@P_ACTIVESTATUS",ACTIVESTATUS),
                                new SqlParameter("@P_OUT", SqlDbType.Int),
                         };

                objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INS_UPD_EMAILTEMPLATE", objParams, true);
                //return Convert.ToInt32(ret);
                retStatus = Convert.ToInt32(ret);

            }
            catch (Exception ex)
            {
                retStatus = 0;
                throw new IITMSException("RAJAGIRI.BusinessLayer.BusinessLogic.EmailTemplateController.InsertUpdateEmailTemplate-> " + ex.ToString());
            }
            return retStatus;
        }

        public DataSet GetRetAll_EmailTemplate(int TEMPLATEID)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_TEMPLATEID", TEMPLATEID);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_RET_ALL_EMAILTEMPLATE", objParams);

            }
            catch (Exception ex)
            {
                throw new IITMSException("RAJAGIRI.BusinessLayer.BusinessLogic.EmailTemplateController.GetAll_EmailTemplate-> " + ex.ToString());
            }
            return ds;
        }
        #endregion

    }
}
