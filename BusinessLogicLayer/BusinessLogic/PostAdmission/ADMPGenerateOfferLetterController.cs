using IITMS;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace IITMS.UAIMS.BusinessLayer.BusinessLogic
{
    public class ADMPGenerateOfferLetterController
    {
        private string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        public DataSet GetOfferLetterList(int ADMBATCH, int DEGREE, int ENTERANCE, int BRANCH, int CATEGORY, int ROUND, int OFFERTYPE)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[7];
                objParams[0] = new SqlParameter("@P_ADMBATCH", ADMBATCH);
                objParams[1] = new SqlParameter("@P_DEGREENO", DEGREE);
                objParams[2] = new SqlParameter("@P_EXAMNO", ENTERANCE);
                objParams[3] = new SqlParameter("@P_BRANCHNO", BRANCH);
                objParams[4] = new SqlParameter("@P_CATEGORYNO", CATEGORY);
                objParams[5] = new SqlParameter("@P_ROUND", ROUND);
                objParams[6] = new SqlParameter("@P_OFFERTYPE", OFFERTYPE);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMP_GET_OFFERLETTERLIST", objParams);

            }
            catch (Exception ex)
            {

                throw new IITMSException("IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.PostAdmission.ADMPGenerateOfferLetterController.GetOfferLetterList-> " + ex.ToString());
            }
            return ds;
        }

        public DataSet GetOfferLetterTemplate(int userno, string applicationids, int LetterTemplateId, int Degree, int Branch, int Batch, DateTime FromDate,
        DateTime ToDate, int LetterTypeId)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[9];
                objParams[0] = new SqlParameter("@P_APPLICATIONID", applicationids);
                objParams[1] = new SqlParameter("@P_LETTER_TYPE_ID", LetterTypeId);
                objParams[2] = new SqlParameter("@P_LETTER_TEMPLATE_ID", LetterTemplateId);
                objParams[3] = new SqlParameter("@P_USERNO", userno);
                objParams[4] = new SqlParameter("@P_DEGREENO", Degree);
                objParams[5] = new SqlParameter("@P_BRANCHNO", Branch);
                objParams[6] = new SqlParameter("@P_FROMDATE", FromDate);
                objParams[7] = new SqlParameter("@P_TODATE ", ToDate);
                objParams[8] = new SqlParameter("@P_BATCHNO", Batch);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_ADMP_LETTER_TEMPLATE_NEW", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.PostAdmission.ADMPGenerateOfferLetterController.GetOfferLetterTemplate-> " + ex.ToString());
            }
            return ds;
        }

        public string SaveOfferLetter(int GeneratedBy, string XMLDATA)
        {
            string retStatus = string.Empty;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[3];

                objParams[0] = new SqlParameter("@P_UANO", GeneratedBy);
                objParams[1] = new SqlParameter("@P_XML", XMLDATA);
                objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);

                objParams[2].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ADMP_INS_OFFER_LETTER_GENERATION", objParams, true);
                retStatus = ret.ToString();

                return retStatus;
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.PostAdmission.ADMPGenerateOfferLetterController.SaveOfferLetter-> " + ex.ToString());
            }
        }

        public string SubmitOfferLetter(int UA_NO, string XMLDATA)
        {
            string retStatus = string.Empty;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[3];

                objParams[0] = new SqlParameter("@P_UANO", UA_NO);
                objParams[1] = new SqlParameter("@P_XML", XMLDATA);
                objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);

                objParams[2].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ADMP_UPD_OFFER_LETTER_GENERATION", objParams, true);
                retStatus = ret.ToString();

                return retStatus;
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.PostAdmission.ADMPGenerateOfferLetterController.SubmitOfferLetter-> " + ex.ToString());
            }
        }

        public string UpdateOfferLetterSendStatus(int UA_NO, int userno, string Application_NO, int RoundNo)
        {
            string retStatus = string.Empty;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[4];

                objParams[0] = new SqlParameter("@P_UA_NO", UA_NO);
                objParams[1] = new SqlParameter("@P_USERNO", userno);
                objParams[2] = new SqlParameter("@P_APPLICATION_NO", Application_NO);
                objParams[3] = new SqlParameter("@P_ROUNDNO", RoundNo);

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ADMP_UPD_OFFER_LETTER_SEND_EMAIL_STATUS", objParams, true);
                retStatus = ret.ToString();

                return retStatus;
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.PostAdmission.ADMPGenerateOfferLetterController.UpdateOfferLetterSendStatus-> " + ex.ToString());
            }
        }
    }
}
