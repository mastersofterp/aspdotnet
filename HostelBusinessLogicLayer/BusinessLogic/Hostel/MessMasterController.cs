//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : HOSTEL                                                               
// PAGE NAME     : HOSTEL fee head CONTROLLER                                            
// CREATION DATE : 20-Dec-2012                                                         
// CREATED BY    : MRUNAL BANSOD                                               
// MODIFIED DATE :                                                                      
// MODIFIED DESC :                                                                      
//======================================================================================
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;

namespace IITMS.UAIMS.BusinessLayer.BusinessLogic
{
    public class MessMasterController
    {
        string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        #region methods

        /// </summary>
        /// <param name="objMM">Type MessMaster</param>
        /// <param name="MessMasterage">ref string</param>
        /// <returns>long value</returns>
        //This method is used to add or modify the MessMaster Master 

        public long AddMessMaster(MessMaster objMM)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[4];

                objParams[0] = new SqlParameter("@P_Mess_NO", objMM.MESSNO );
                objParams[1] = new SqlParameter("@P_Mess_NAME", objMM.MESSNAME);
                objParams[2] = new SqlParameter("@P_COLLEGE_CODE", objMM.COLLLEGECODE);
                //objParams[3] = new SqlParameter("@P_USERID", objMM.USERID);
                //objParams[4] = new SqlParameter("@P_IPADDRESS", objMM.IPADDRESS);
                //objParams[5] = new SqlParameter("@P_MACADDRESS", objMM.MACADDRESS);
                //objParams[6] = new SqlParameter("@P_AUDITDATE", objMM.AUDITDATE);
                objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[3].Direction = ParameterDirection.Output;


                //object ret = objSQLHelper.ExecuteNonQuerySP("PKG_HOSTEL_MessMaster_INSERT_UPDATE", objParams, true);
                if (objSQLHelper.ExecuteNonQuerySP("PKG_HOSTEL_MESS_MASTER_INSERT", objParams, false) != null)
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                

            }
            catch (Exception ee)
            {
                throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.MessMasterMasterController.AddUpdateMessMasterMaster->" + ee.ToString());
            }
            return retStatus;
        }

        public int UpdateMessMaster(MessMaster  objmess)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                //update Block info
                objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_MESS_NO", objmess.MESSNO);
                objParams[1] = new SqlParameter("@P_MESS_NAME", objmess.MESSNAME);

                if (objSQLHelper.ExecuteNonQuerySP("PKG_HOSTEL_MESS_UPDATE", objParams, false) != null)
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BlockController.UpdateBlock-> " + ex.ToString());
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
                objParams[0] = new SqlParameter("@P_MESS_NO", mess_no);

                dr = objSQLHelper.ExecuteReaderSP("KG_HOSTEL_MESS_MASTER_GET_BY_ID", objParams);
            }
            catch (Exception ex)
            {
                return dr;
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BlockController.GetBlock-> " + ex.ToString());
            }
            return dr;
        }

        public DataSet GetAllMess()
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = new SqlParameter[0];

                ds = objSQLHelper.ExecuteDataSetSP("PKG_HOSTEL_MESS_MASTER_GET_ALL", objParams);

            }
            catch (Exception ex)
            {
                return ds;
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BlockController.GetAllBlock-> " + ex.ToString());
            }
            return ds;
        }

        public int UpdateMessHead(int MESS_HEAD_NO, string MESS_SHORTNAME, string MESS_LONGNAME)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_MESS_HEAD_NO", MESS_HEAD_NO);
                objParams[1] = new SqlParameter("@P_MESS_SHORTNAME", MESS_SHORTNAME);
                objParams[2] = new SqlParameter("@P_MESS_LONGNAME", MESS_LONGNAME);


                if (objSQLHelper.ExecuteNonQuerySP("PKG_HOSTEL_MESS_HEAD_UPDATE", objParams, false) != null)
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MessMasterController.UpdateMessHead-> " + ex.ToString());
            }

            return retStatus;
        }
        public int UpdateMessBillHead(int MESS_BILL_HEAD_NO, string MESS_BILL_SHORTNAME, string MESS_BILL_LONGNAME)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_MESS_BILL_NO", MESS_BILL_HEAD_NO);
                objParams[1] = new SqlParameter("@P_MESS_BILL_SHORTNAME", MESS_BILL_SHORTNAME);
                objParams[2] = new SqlParameter("@P_MESS_BILL_LONGNAME", MESS_BILL_LONGNAME);


                if (objSQLHelper.ExecuteNonQuerySP("PKG_HOSTEL_MESS_BILL_HEAD_UPDATE", objParams, false) != null)
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MessMasterController.UpdateMessBillHead-> " + ex.ToString());
            }

            return retStatus;
        }
        #endregion
    }
}
