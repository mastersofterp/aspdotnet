using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using IITMS.SQLServer.SQLDAL;
using IITMS;
using IITMS;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities.Academic;
using IITMS.UAIMS;
using BusinessLogicLayer.BusinessEntities.Academic;


namespace BusinessLogicLayer.BusinessLogic.Academic
{
    public class SmsTemplateTypeController
    {
        string connectionstring = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        public int InsertSmsTemplateData(SmsTemplateType objSmsTemplate)
        {

            int status = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionstring);

                SqlParameter[] sqlParams = new SqlParameter[8];

                sqlParams[0] = new SqlParameter("@TEMPLATE_TYPE", objSmsTemplate.TEMPLATE_TYPE);
                sqlParams[1] = new SqlParameter("@TEMPLATE_NAME", objSmsTemplate.TEMPLATE_NAME);
                sqlParams[2] = new SqlParameter("@TEM_ID", objSmsTemplate.TEM_ID);
                sqlParams[3] = new SqlParameter("@TEMPLATE", objSmsTemplate.TEMPLATE);
                sqlParams[4] = new SqlParameter("@AL_No", objSmsTemplate.AL_NO);
                sqlParams[5] = new SqlParameter("@ACTIVE_STATUS", objSmsTemplate.ActiveStatus);
                sqlParams[6] = new SqlParameter("@VARIABLE_COUNT", objSmsTemplate.VARIABLE_COUNT);
                sqlParams[7] = new SqlParameter("@OUTPUT", SqlDbType.Int);
                sqlParams[7].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_SMS_TEMPLATE_DATA", sqlParams, true);
                status = Convert.ToInt32(ret);
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Academic.SmsTemplateTypeController.InsertSmsTemplateData() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }


        public DataSet BindListviewSmsTemplateType()
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQL = new SQLHelper(connectionstring);


                SqlParameter[] objParams = new SqlParameter[0];

                ds = objSQL.ExecuteDataSetSP("PKG_GET_SMS_TEMPLATE_TYPE", objParams);
            }
            catch (Exception ex)
            {

                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Academic.SmsTemplateTypeController.BindListviewSmsTemplateType-> " + ex.ToString());
            }
            return ds;
        }

        public DataSet GetSmsTemplateTypeInfo(int SMS_TEMPLATE_ID)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionstring);
                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@SMS_TEMPLATE_ID",SMS_TEMPLATE_ID);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_SMS_TEMPLATETYPE_DATA", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SmsTemplateTypeController.GetSmsTemplateTypeInfo() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

    


      public int UpdateSmsTemplateType(SmsTemplateType objSmsTemplate)
        {
            int status = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionstring);

                SqlParameter[] sqlParams = new SqlParameter[7];
                sqlParams[0] = new SqlParameter("@SMS_TEMPLATE_ID", objSmsTemplate.SMS_TEMPLATE_ID);
                sqlParams[1] = new SqlParameter("@TEMPLATE_TYPE", objSmsTemplate.TEMPLATE_TYPE);
                sqlParams[2] = new SqlParameter("@TEMPLATE_NAME", objSmsTemplate.TEMPLATE_NAME);
                sqlParams[3] = new SqlParameter("@AL_No", objSmsTemplate.AL_NO);
                sqlParams[4] = new SqlParameter("@TEM_ID", objSmsTemplate.TEM_ID);
                sqlParams[5] = new SqlParameter("@TEMPLATE", objSmsTemplate.TEMPLATE);
                sqlParams[6] = new SqlParameter("@ACTIVE_STATUS", objSmsTemplate.ActiveStatus);




                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_UPDATE_SMS_TEMPLATE_TYPE", sqlParams, false);
                status = Convert.ToInt32(ret);
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Academic.SmsTemplateController.UpdateTemplateType() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }
      public int UpdateWhatsappTemplateType(SmsTemplateType objSmsTemplate)
          {
          int status = Convert.ToInt32(CustomStatus.Others);
          try
              {
              SQLHelper objSQLHelper = new SQLHelper(connectionstring);

              SqlParameter[] sqlParams = new SqlParameter[7];
              sqlParams[0] = new SqlParameter("@WHATSAPP_TEMPLATE_ID", objSmsTemplate.SMS_TEMPLATE_ID);
              sqlParams[1] = new SqlParameter("@TEMPLATE_TYPE", objSmsTemplate.TEMPLATE_TYPE);
              sqlParams[2] = new SqlParameter("@TEMPLATE_NAME", objSmsTemplate.TEMPLATE_NAME);
              sqlParams[3] = new SqlParameter("@AL_No", objSmsTemplate.AL_NO);
              sqlParams[4] = new SqlParameter("@TEM_ID", objSmsTemplate.TEM_ID);
              sqlParams[5] = new SqlParameter("@TEMPLATE", objSmsTemplate.TEMPLATE);
              sqlParams[6] = new SqlParameter("@ACTIVE_STATUS", objSmsTemplate.ActiveStatus);




              object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_UPDATE_WHATSAPP_TEMPLATE_TYPE_DATA", sqlParams, false);
              status = Convert.ToInt32(ret);
              }
          catch (Exception ex)
              {
              status = Convert.ToInt32(CustomStatus.Error);
              throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Academic.SmsTemplateController.UpdateTemplateType() --> " + ex.Message + " " + ex.StackTrace);
              }
          return status;
          }


      public DataSet GetWhatsappTemplateTypeInfo(int SMS_TEMPLATE_ID)
          {
          DataSet ds = null;
          try
              {
              SQLHelper objSQLHelper = new SQLHelper(connectionstring);
              SqlParameter[] objParams = new SqlParameter[1];
              objParams[0] = new SqlParameter("@WHATSAPP_TEMPLATE_ID", SMS_TEMPLATE_ID);
              ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_WHATSAPP_TEMPLATETYPE_DATA", objParams);
              }
          catch (Exception ex)
              {
              throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SmsTemplateTypeController.GetSmsTemplateTypeInfo() --> " + ex.Message + " " + ex.StackTrace);
              }
          return ds;
          }

      public DataSet BindListviewwhastappTemplateType()
          {
          DataSet ds = null;
          try
              {
              SQLHelper objSQL = new SQLHelper(connectionstring);


              SqlParameter[] objParams = new SqlParameter[0];

              ds = objSQL.ExecuteDataSetSP("PKG_GET_WHATSAPP_TEMPLATE_TYPE", objParams);
              }
          catch (Exception ex)
              {

              throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Academic.SmsTemplateTypeController.BindListviewSmsTemplateType-> " + ex.ToString());
              }
          return ds;
          }

      public int InsertWhatsappTemplateData(SmsTemplateType objSmsTemplate)
          {

          int status = 0;
          try
              {
              SQLHelper objSQLHelper = new SQLHelper(connectionstring);

              SqlParameter[] sqlParams = new SqlParameter[8];

              sqlParams[0] = new SqlParameter("@TEMPLATE_TYPE", objSmsTemplate.TEMPLATE_TYPE);
              sqlParams[1] = new SqlParameter("@TEMPLATE_NAME", objSmsTemplate.TEMPLATE_NAME);
              sqlParams[2] = new SqlParameter("@TEM_ID", objSmsTemplate.TEM_ID);
              sqlParams[3] = new SqlParameter("@TEMPLATE", objSmsTemplate.TEMPLATE);
              sqlParams[4] = new SqlParameter("@AL_No", objSmsTemplate.AL_NO);
              sqlParams[5] = new SqlParameter("@ACTIVE_STATUS", objSmsTemplate.ActiveStatus);
              sqlParams[6] = new SqlParameter("@VARIABLE_COUNT", objSmsTemplate.VARIABLE_COUNT);
              sqlParams[7] = new SqlParameter("@OUTPUT", SqlDbType.Int);
              sqlParams[7].Direction = ParameterDirection.Output;

              object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_WHATSAPP_TEMPLATE_DATA", sqlParams, true);
              status = Convert.ToInt32(ret);
              }
          catch (Exception ex)
              {
              status = Convert.ToInt32(CustomStatus.Error);
              throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Academic.SmsTemplateTypeController.InsertSmsTemplateData() --> " + ex.Message + " " + ex.StackTrace);
              }
          return status;
          }
      public DataSet BindListviewwhastappTemplateType_New()
      {
          DataSet ds = null;
          try
          {
              SQLHelper objSQL = new SQLHelper(connectionstring);
              SqlParameter[] objParams = new SqlParameter[0];
              ds = objSQL.ExecuteDataSetSP("PKG_GET_WHATSAPP_TEMPLATE_TYPE_NEW", objParams);
          }
          catch (Exception ex)
          {

              throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Academic.SmsTemplateTypeController.BindListviewwhastappTemplateType_New-> " + ex.ToString());
          }
          return ds;
      }


    }
}