using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;
namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
            public class PayDesignationController
            {
                private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
                
                public int AddUpdateNUDesig (PayDesignationMas objDes)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        //Add New Employee
                        objParams = new SqlParameter[6];

                        objParams[0] = new SqlParameter("@P_NUDESIGNO", objDes.DESIGNO);
                        objParams[1] = new SqlParameter("@P_DESIG", objDes.DESIGNATION);
                        objParams[2] = new SqlParameter("@P_DESIGSHORT", objDes.DESIGSHORT);
                        objParams[3] = new SqlParameter("@P_COLLEGECODE", objDes.COLLEGECODE);
                        objParams[4] = new SqlParameter("@P_STAFFNO", objDes.STAFFNO);
                        objParams[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[5].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_INS_UPD_NUDESIG", objParams, true);
                        if (Convert.ToInt32(ret) == 1)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PayDesignationController.AddUpdateNUDesig-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetNUDesigList(int staffNo)
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_STAFFNO", staffNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_RET_NUDESIG", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PayDesignationController.GetNUDesig-> " + ex.ToString());
                    }
                    return ds;
                }

                public int AddUpdateDesig(PayDesignationMas objDes)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        //Add New Employee
                        objParams = new SqlParameter[6];

                        objParams[0] = new SqlParameter("@P_SUBDESIGNO", objDes.DESIGNO);
                        objParams[1] = new SqlParameter("@P_DESIG", objDes.DESIGNATION);
                        objParams[2] = new SqlParameter("@P_DESIGSHORT", objDes.DESIGSHORT);
                        objParams[3] = new SqlParameter("@P_COLLEGECODE", objDes.COLLEGECODE);
                        objParams[4] = new SqlParameter("@P_STAFFNO", objDes.STAFFNO);
                        objParams[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[5].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_INS_UPD_SUBDESIG", objParams, true);
                        if (Convert.ToInt32(ret) == 1)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PayDesignationController.AddUpdateNUDesig-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetDesigList(int staffNo)
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_STAFFNO", staffNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_RET_SUBDESIG", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PayDesignationController.GetNUDesig-> " + ex.ToString());
                    }
                    return ds;
                }
            }
        }
    }
}
