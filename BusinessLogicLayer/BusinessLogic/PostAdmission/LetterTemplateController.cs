using IITMS;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using BusinessLogicLayer.BusinessEntities.PostAdmission;

namespace IITMS.UAIMS.BusinessLayer.BusinessLogic
{
    public class LetterTemplateController
    {
        private string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        public int InsUpdLetterTemplate(LetterTemplate objLT)
        {
            int retstatus = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[8];
                objParams[0] = new SqlParameter("@P_LETTER_TEMPLATE_ID", objLT.LetterTemplateId);
                objParams[1] = new SqlParameter("@P_LETTER_TYPE_ID", objLT.LetterTypeId);
                objParams[2] = new SqlParameter("@P_LETTER_TEMPLATE_NAME", objLT.LetterTemplateName);
                objParams[3] = new SqlParameter("@P_SHORT_DESC", objLT.ShortDesc);
                objParams[4] = new SqlParameter("@P_LETTER_TEXT", objLT.LetterText);
                objParams[5] = new SqlParameter("@P_ACTIVE_STATUS", objLT.ActiveStatus);
                objParams[6] = new SqlParameter("@P_UA_NO", objLT.UaNo);
                objParams[7] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[7].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INS_UPD_LETTER_TEMPLATE", objParams, true);

                if (Convert.ToInt32(ret) == -99)
                    retstatus = Convert.ToInt32(ret);  //Convert.ToInt32(CustomStatus.Error);                  
                else
                    retstatus = Convert.ToInt32(ret);  //Convert.ToInt32(CustomStatus.RecordSaved);

            }
            catch (Exception ex)
            {
                throw new IITMSException("BusinessLogicLayer.BusinessLogic.LetterTemplatecontroller.InsUpdLetterTemplate-> " + ex.ToString());
            }
            return retstatus;
        }


        public int InsUpdLetterFieldTemplateData(LetterTemplateFieldData fieldobj)
        {
            int retstatus = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[6];
                objParams[0] = new SqlParameter("@LETTER_DATAFIELD_CONFIGID", fieldobj.LetterTemplateId);
                objParams[1] = new SqlParameter("@P_LETTER_TYPE_ID", fieldobj.LetterTypeId);                
                objParams[2] = new SqlParameter("@DISPLAY_FIELD_NAME", fieldobj.DisplayDataField);
                objParams[3] = new SqlParameter("@DATA_FIELD", fieldobj.DataField);               
                objParams[4] = new SqlParameter("@P_UA_NO", fieldobj.UaNo);
                objParams[5] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[5].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INS_UPD_LETTER_FIELD_TEMPLATE_DATA", objParams, true);

                if (Convert.ToInt32(ret) == -99)
                    retstatus = Convert.ToInt32(ret);  //Convert.ToInt32(CustomStatus.Error);                  
                else
                    retstatus = Convert.ToInt32(ret);  //Convert.ToInt32(CustomStatus.RecordSaved);

            }
            catch (Exception ex)
            {
                throw new IITMSException("BusinessLogicLayer.BusinessLogic.LetterTemplatecontroller.InsUpdLetterTemplate-> " + ex.ToString());
            }
            return retstatus;
        }

        public DataSet GetLetterTemplate_DATA(LetterTemplateFieldData fieldobj)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_LETTER_TEMPLATE_ID", fieldobj.LetterTemplateId);
                ds = objSQLHelper.ExecuteDataSetSP("GET_DATAFIELDEMAIL_LETTER_TEMPLATE", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("BusinessLogicLayer.BusinessLogic.LetterTemplatecontroller.GetLetterTemplate-> " + ex.ToString());
            }
            return ds;
        }

        public DataSet GetLetterTemplate_BYTEMPID(LetterTemplateFieldData fieldobj)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_LETTER_TEMPLATE_ID", fieldobj.LetterTemplateId);
                ds = objSQLHelper.ExecuteDataSetSP("GET_DATAFIELDEMAIL_LETTER_TEMPLATEBYID", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("BusinessLogicLayer.BusinessLogic.LetterTemplatecontroller.GetLetterTemplate-> " + ex.ToString());
            }
            return ds;
        }



        public DataSet GetLetterTemplate(LetterTemplate objLT)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_LETTER_TEMPLATE_ID", objLT.LetterTemplateId);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_LETTER_TEMPLATE", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("BusinessLogicLayer.BusinessLogic.LetterTemplatecontroller.GetLetterTemplate-> " + ex.ToString());
            }
            return ds;
        }


    }
}
