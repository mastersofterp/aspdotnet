using System;
using System.Data;
using System.Web;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;

using System.Data.SqlClient;
using System.Data.SqlTypes;


namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLogicLayer.BusinessLogic
        {

            public class Str_Calibration
            {

                private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                public DataSet GetTablesForCalibration()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_GET_TABLES_FOR_CALIBRATION", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.DepartmentController.GetAllDepartMent-> " + ex.ToString());
                    }
                    return ds;
                }


                public int Ins_Update_Calibration(Str_CalibartionEnt ObjEnt)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New BUDGETHEAD
                        objParams = new SqlParameter[19];
                        objParams[0] = new SqlParameter("@P_ITEMNO",ObjEnt.ITEMNO);
                        objParams[1] = new SqlParameter("@P_CALIBRATED_BY",ObjEnt.CALIBRATED_BY);
                        objParams[2] = new SqlParameter("@P_COST",ObjEnt.COST);
                        objParams[3] = new SqlParameter("@P_DATE_OF_CALIBRATION",ObjEnt.DATE_OF_CALIBRATION);
                        objParams[4] = new SqlParameter("@P_UANO",ObjEnt.UANO);
                        objParams[5] = new SqlParameter("@P_NEXT_DUE_DATE",ObjEnt.NEXT_DUE_DATE);
                        objParams[6] = new SqlParameter("@P_MDNO",ObjEnt.MDNO);
                        objParams[7] = new SqlParameter("@P_MAKE",ObjEnt.MAKE);
                        objParams[8] = new SqlParameter("@P_SPECIFICATION",ObjEnt.SPECIFICATION);
                        objParams[9] = new SqlParameter("@P_YEAR_OF_MANUFACTURING",ObjEnt.YEAR_OF_MANUFACTURING);
                        objParams[10] = new SqlParameter("@P_DATE_OF_PURCHASE",ObjEnt.DATE_OF_PURCHASE);
                        objParams[11] = new SqlParameter("@P_VALUE",ObjEnt.VALUE);
                        objParams[12] = new SqlParameter("@P_MAINTAINED_BY",ObjEnt.MAINTAINED_BY );
                        objParams[13] = new SqlParameter("@P_GUARANTEE_FROM",ObjEnt.GUARANTEE_FROM);
                        objParams[14] = new SqlParameter("@P_GUARANTEE_TO",ObjEnt.GUARANTEE_TO);
                        objParams[15] = new SqlParameter("@P_CALIBRATION_FREQUENCY",ObjEnt.CALIBRATION_FREQUENCY);
                        objParams[16] = new SqlParameter("@P_STORE_CALIBRATION_TBL", ObjEnt.CalibrationTable);
                        objParams[17] = new SqlParameter("@P_CID", ObjEnt.CID);
                        objParams[18] = new SqlParameter("@P_OUT", SqlDbType.Int);
                       objParams[18].Direction = ParameterDirection.Output;
                       object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STR_INS_UPD_CALIBRATION_ENTRY", objParams, true);
                       if (ret != null)
                       {
                           if (ret.ToString().Equals("1"))
                               retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                           if (ret.ToString().Equals("2"))
                               retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                       }
                       else
                           retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                    }
                    catch (Exception ex)
                    {
                        return retStatus;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.DepartmentController.GetAllDepartMent-> " + ex.ToString());
                    }
                    return retStatus;
                }



                public DataSet GetTablesForEquipment()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_GET_TABLES_FOR_EQUIPMENT", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.DepartmentController.GetAllDepartMent-> " + ex.ToString());
                    }
                    return ds;
                }

                public int Ins_Update_Equipment(Str_CalibartionEnt ObjEnt)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                       
                        objParams = new SqlParameter[10];
                        objParams[0] = new SqlParameter("@P_ITEMNO",ObjEnt.ITEMNO );
                        objParams[1] = new SqlParameter("@P_SERVICED_ON",ObjEnt.SERVICED_ON );
                        objParams[2] = new SqlParameter("@P_NATURE_OF_COMPLAINT",ObjEnt.NATURE_OF_COMPLAINT );                    
                        objParams[3] = new SqlParameter("@P_COST",ObjEnt.COST);
                        objParams[4] = new SqlParameter("@P_SERVICED_BY",ObjEnt.SERVICED_BY  );
                        objParams[5] = new SqlParameter("@P_INITIALS_IF_INCHARGE",ObjEnt.INITIALS_IF_INCHARGE );                      
                        objParams[6] = new SqlParameter("@P_CREATED_BY",ObjEnt.UANO );
                        objParams[7] = new SqlParameter("@P_STORE_ITEM_SERVCING_TBL", ObjEnt.ServicingTable);
                        objParams[8] = new SqlParameter("@P_ISNO", ObjEnt.ISNO);
                        objParams[9] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[9].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INS_UPD_ITEM_SERVICE", objParams, true);
                       if (ret != null)
                       {
                           if (ret.ToString().Equals("1"))
                               retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                           if (ret.ToString().Equals("2"))
                               retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                       }
                       else
                           retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                    }
                    catch (Exception ex)
                    {
                        return retStatus;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.DepartmentController.GetAllDepartMent-> " + ex.ToString());
                    }
                    return retStatus;
                }
            }
        }
    }
}
