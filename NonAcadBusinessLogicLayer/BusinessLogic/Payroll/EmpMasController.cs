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
            public class EmpMasController
            {
                private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                /// <summary>
                /// AddEmployee method is used to add new Employee.
                /// </summary>
                /// <param name="objEM">objEM is the object of EmpMaster class</param>
                /// <returns>Integer CustomStatus Record Added or Error</returns>
                public int AddEmployee(EmpMaster objEM)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        //Add New Employee
                        objParams = new SqlParameter[28];
                        objParams[0] = new SqlParameter("@P_IDNO", objEM.IDNO);
                        objParams[1] = new SqlParameter("@P_SEQ_NO", objEM.SEQ_NO);
                        objParams[2] = new SqlParameter("@P_TITLE", objEM.TITLE);
                        objParams[3] = new SqlParameter("@P_FNAME",objEM.FNAME);
                        objParams[4] = new SqlParameter("@P_MNAME", objEM.MNAME);
                        objParams[5] = new SqlParameter("@P_LNAME", objEM.LNAME);
                        objParams[6] = new SqlParameter("@P_SEX", objEM.SEX);
                        objParams[7] = new SqlParameter("@P_FATHERNAME", objEM.FATHERNAME);
                        objParams[8] = new SqlParameter("@P_DOB", objEM.DOB);
                        objParams[9] = new SqlParameter("@P_DOJ", objEM.DOJ);
                        objParams[10] = new SqlParameter("@P_DOI", objEM.DOI);
                        objParams[11] = new SqlParameter("@P_RDT", objEM.RDT);
                        objParams[12] = new SqlParameter("@P_HP", objEM.HP);
                        objParams[13] = new SqlParameter("@P_SUBDEPTNO", objEM.SUBDEPTNO);
                        objParams[14] = new SqlParameter("@P_SUBDESIGNO", objEM.SUBDESIGNO);
                        objParams[15] = new SqlParameter("@P_STNO", objEM.STNO);
                        objParams[16] = new SqlParameter("@P_STAFFNO", objEM.STAFFNO);
                        objParams[17] = new SqlParameter("@P_BANKACC_NO", objEM.BANKACC_NO);
                        objParams[18] = new SqlParameter("@P_GPF_NO", objEM.GPF_NO);
                        objParams[19] = new SqlParameter("@P_PPF_NO ", objEM.PPF_NO);
                        objParams[20] = new SqlParameter("@P_PAN_NO", objEM.PAN_NO);
                        objParams[21] = new SqlParameter("@P_BANKNO", objEM.BANKNO);
                        objParams[22] = new SqlParameter("@P_QUARTER", objEM.QUARTER);
                        objParams[23] = new SqlParameter("@P_QTRNO", objEM.QTRNO);
                        objParams[24] = new SqlParameter("@P_REMARK", objEM.REMARK);
                        objParams[25] = new SqlParameter("@P_ANFN", objEM.ANFN);
                        objParams[26] = new SqlParameter("@P_QRENT_YN", objEM.QRENT_YN);
                        objParams[27] = new SqlParameter("@P_COLLEGE_CODE", objEM.COLLEGE_CODE);
                        
                        if (objSQLHelper.ExecuteNonQuerySPTrans("PKG_PAY_INS_EMPMAS", objParams, false,1) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                       
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpMasController.AddEmployee -> " + ex.ToString());
                    }
                    return retStatus;
                }

                /// <summary>
                /// to Update status of Employee in pay_Empmas table
                /// </summary>
                /// <param name="idno"></param>
                /// <param name="status"></param>
                /// <returns></returns>
                public int UpdateStatus(int idno,string status)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        //Add New Employee
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_ACTIVE", status);


                        if (objSQLHelper.ExecuteNonQuerySP("PAYROLL_UPD_ACTIVE_STATUS", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpMasController.UpdateStatus -> " + ex.ToString());
                    }
                    return retStatus;
                }

                /// <summary>
                /// Show Active status of employee
                /// </summary>
                /// <param name="idno"></param>
                /// <returns></returns>
                public DataTableReader ShowEmpStatus(int idno)
                {
                    DataTableReader dtr = null;
                    //SqlDataReader dr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_Idno", idno);
                        dtr = objSQLHelper.ExecuteDataSetSP("PKG_EMP_SP_RET_EMP_ACTIVE_STATUS", objParams).Tables[0].CreateDataReader();
                        // dtr = objSQLHelper.ExecuteReaderSP("PKG_EMP_SP_RET_EMPLOYEE_BYID", objParams)as SqlDataReader ;

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpMasController.ShowEmpStatus->" + ex.ToString());
                    }
                    return dtr;
                }
            }
        }
    }
}