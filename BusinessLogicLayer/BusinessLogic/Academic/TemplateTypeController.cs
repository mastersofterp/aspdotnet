using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using IITMS.SQLServer.SQLDAL;
using IITMS;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities.Academic;
using IITMS.UAIMS;
using BusinessLogicLayer.BusinessEntities.Academic;



namespace BusinessLogicLayer.BusinessLogic.Academic
{
    public class TemplateTypeController
    {
        string connectionstring = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;


        public int InsertTemplateType(TemplateType objTemplateType)
        {
            int status = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionstring);

                SqlParameter[] sqlParams = new SqlParameter[3];

                sqlParams[0] = new SqlParameter("@TEMPLATE_TYPE", objTemplateType.TEMPLATE_TYPE);
                sqlParams[1] = new SqlParameter("@ACTIVE_STATUS", objTemplateType.ActiveStatus);
                sqlParams[2] = new SqlParameter("@OUTPUT", SqlDbType.Int);
                sqlParams[2].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_TEMPLATE_TYPE", sqlParams, true);
                status = Convert.ToInt32(ret);
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Academic.TemplateController.InsertTemplateType() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }


        public DataSet BindListview()
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQL = new SQLHelper(connectionstring);


                SqlParameter[] objParams = new SqlParameter[0];

                ds = objSQL.ExecuteDataSetSP("PKG_GET_TEMPLATE_TYPE", objParams);
            }
            catch (Exception ex)
            {

                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Academic.TemplateController.BindListview-> " + ex.ToString());
            }
            return ds;
        }

        

        public DataSet GetTemplateTypeInfo(int TEMPLATE_ID)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionstring);
                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@TEMPLATE_ID", TEMPLATE_ID);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_TEMPLATETYPE_DATA", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TemplateTypeController.GetTemplateTypeInfo() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet GetTemplateWhatsappTypeInfo(int TEMPLATE_ID)
            {
            DataSet ds = null;
            try
                {
                SQLHelper objSQLHelper = new SQLHelper(connectionstring);
                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@TEMPLATE_ID", TEMPLATE_ID);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_WHATSAPP_TEMPLAT_ETYPE_DATA", objParams);
                }
            catch (Exception ex)
                {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TemplateTypeController.GetTemplateTypeInfo() --> " + ex.Message + " " + ex.StackTrace);
                }
            return ds;
            }


        public int UpdateTemplateType(TemplateType objTemplateType)
        {
            int status = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionstring);

                SqlParameter[] sqlParams = new SqlParameter[3];
                sqlParams[0] = new SqlParameter("@TEMPLATE_ID", objTemplateType.TEMPLATE_ID);
                sqlParams[1] = new SqlParameter("@TEMPLATE_TYPE", objTemplateType.TEMPLATE_TYPE);
                sqlParams[2] = new SqlParameter("@ACTIVE_STATUS", objTemplateType.ActiveStatus);




                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_UPDATE_TEMPLATE_TYPE", sqlParams, false);
                status = Convert.ToInt32(ret);
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Academic.TemplateController.InsertTemplateType() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }


        public DataSet GetWhatsappTemplateTypeInfo(int TEMPLATE_ID)
            {
            DataSet ds = null;
            try
                {
                SQLHelper objSQLHelper = new SQLHelper(connectionstring);
                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@TEMPLATE_ID", TEMPLATE_ID);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_WHATSAPP_TEMPLATETYPE_DATA", objParams);
                }
            catch (Exception ex)
                {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TemplateTypeController.GetTemplateTypeInfo() --> " + ex.Message + " " + ex.StackTrace);
                }
            return ds;
            }
        public DataSet GetWhatsappTemplateTypeInfoDetails(int TEMPLATE_ID)
            {
            DataSet ds = null;
            try
                {
                SQLHelper objSQLHelper = new SQLHelper(connectionstring);
                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@TEMPLATE_ID", TEMPLATE_ID);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_WHATSAPP_TEMPLATETYPE_DATA", objParams);
                }
            catch (Exception ex)
                {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TemplateTypeController.GetTemplateTypeInfo() --> " + ex.Message + " " + ex.StackTrace);
                }
            return ds;
            }



        public int UpdateWhatsappTemplateType(TemplateType objTemplateType)
            {
            int status = Convert.ToInt32(CustomStatus.Others);
            try
                {
                SQLHelper objSQLHelper = new SQLHelper(connectionstring);

                SqlParameter[] sqlParams = new SqlParameter[3];
                sqlParams[0] = new SqlParameter("@TEMPLATE_ID", objTemplateType.TEMPLATE_ID);
                sqlParams[1] = new SqlParameter("@TEMPLATE_TYPE", objTemplateType.TEMPLATE_TYPE);
                sqlParams[2] = new SqlParameter("@ACTIVE_STATUS", objTemplateType.ActiveStatus);




                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_UPDATE_WHATSAPP_TEMPLATE_TYPE", sqlParams, false);
                status = Convert.ToInt32(ret);
                }
            catch (Exception ex)
                {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Academic.TemplateController.InsertTemplateType() --> " + ex.Message + " " + ex.StackTrace);
                }
            return status;
            }

        public DataSet BindListWhatsappttypeview()
            {
            DataSet ds = null;
            try
                {
                SQLHelper objSQL = new SQLHelper(connectionstring);


                SqlParameter[] objParams = new SqlParameter[0];

                ds = objSQL.ExecuteDataSetSP("PKG_GET_WHATSAPP_TEMPLATE_TYPE_DETAILS", objParams);
                }
            catch (Exception ex)
                {

                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Academic.TemplateController.BindListview-> " + ex.ToString());
                }
            return ds;
            }

        public int InsertWhatsappTemplateType(TemplateType objTemplateType)
            {
            int status = 0;
            try
                {
                SQLHelper objSQLHelper = new SQLHelper(connectionstring);

                SqlParameter[] sqlParams = new SqlParameter[3];

                sqlParams[0] = new SqlParameter("@TEMPLATE_TYPE", objTemplateType.TEMPLATE_TYPE);
                sqlParams[1] = new SqlParameter("@ACTIVE_STATUS", objTemplateType.ActiveStatus);
                sqlParams[2] = new SqlParameter("@OUTPUT", SqlDbType.Int);
                sqlParams[2].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_WHATSAPP_TEMPLATE_TYPE", sqlParams, true);
                status = Convert.ToInt32(ret);
                }
            catch (Exception ex)
                {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Academic.TemplateController.InsertTemplateType() --> " + ex.Message + " " + ex.StackTrace);
                }
            return status;
            }



    }
}

        