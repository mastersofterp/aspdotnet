using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;
using System.Data;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {

          public  class MM_CONTROLLER
            {
                private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

              

                public int INSERT_MEETINGPRESENTY(MeetingEntity objMD)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[5];

                        objParams[0] = new SqlParameter("@P_PK_MEETINGATT", objMD.PK_MEETINGDETAILS);
                        objParams[1] = new SqlParameter("@P_FK_COMMITEE", objMD.FK_Committe);
                        objParams[2] = new SqlParameter("@P_MEETINGCODE", objMD.METTINGCODE);
                        objParams[3] = new SqlParameter("@P_FK_MEMBER", objMD.FKMEMBER);
                        objParams[4] = new SqlParameter("@P_FK_CommiteeDesig", objMD.DESIGID);
                        

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_MM_INSERTUPDATE_MEETINGPRESENTY", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MR_Controller.AddUpdate_MR_Bill_Details->" + ex.ToString());
                    }
                    return retstatus;
                }
              
              
              public int AddUpdate_Meeting_Details(MeetingEntity objMD)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[12];

                        objParams[0] = new SqlParameter("@P_PK_MEETINGDETAILS", objMD.PK_MEETINGDETAILS);
                        objParams[1] = new SqlParameter("@P_FK_Committe", objMD.FK_Committe);
                        objParams[2] = new SqlParameter("@P_FK_AGENDA", objMD.FK_AGENDA);
                        objParams[3] = new SqlParameter("@P_AGENDADETAILS", objMD.AGENDADETAILS);
                        objParams[4] = new SqlParameter("@P_USERID", objMD.USERID);
                        objParams[5] = new SqlParameter("@P_Filepath", objMD.Filepath);
                        objParams[6] = new SqlParameter("@P_AUDITDATE", DateTime.Now);
                        objParams[7] = new SqlParameter("@P_LOCK_MEET", objMD.LOCK_MEET);
                        objParams[8] = new SqlParameter("@P_METTINGCODE", objMD.METTINGCODE);
                        objParams[9] = new SqlParameter("@P_DisplayFileName", objMD.DisplayFileName);                       
                   
                        objParams[10] = new SqlParameter("@P_QTYPE", objMD.QTYPE);
                      
                        objParams[11] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[11].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_MM_MeetingDetails_INSERUPDATE", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MR_Controller.AddUpdate_MR_Bill_Details->" + ex.ToString());
                    }
                    return retstatus;
                }              
              /// <summary>
              /// MODIFIED BY   : MRUNAL SINGH
              /// MODIFIED DATE : 16-FEB-2015
              /// DESCRIPTION   : ADD DEPTNO
              /// </summary>
              /// <param name="objMD"></param>
              /// <returns></returns>

              public int AddUpdate_MEMBER_Details(MeetingEntity objMD)
              {
                  int retstatus = 0;
                  try
                  {
                      SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                      SqlParameter[] objParams = null;
                      objParams = new SqlParameter[41];

                      objParams[0] = new SqlParameter("@P_PK_CMEMBER", objMD.PKID);
                      objParams[1] = new SqlParameter("@P_TITLE", objMD.TITLE);
                      objParams[2] = new SqlParameter("@P_FNAME", objMD.FNAME);
                      objParams[3] = new SqlParameter("@P_MNAME", objMD.MNAME);
                      objParams[4] = new SqlParameter("@P_LNAME", objMD.LNAME);
                      objParams[5] = new SqlParameter("@P_Representation", objMD.Representation);
                      objParams[6] = new SqlParameter("@P_designation", objMD.designation);
                      objParams[7] = new SqlParameter("@P_CONSTITUENCY", objMD.CONSTITUENCY);
                      objParams[8] = new SqlParameter("@P_P_ADDRESS", objMD.P_ADDRESS);
                      objParams[9] = new SqlParameter("@P_P_CITY", objMD.P_CITY);
                      objParams[10] = new SqlParameter("@P_P_STATE", objMD.P_STATE);
                      objParams[11] = new SqlParameter("@P_P_PHONE", objMD.P_PHONE);
                      objParams[12] = new SqlParameter("@P_P_EMAIL", objMD.P_EMAIL);
                      objParams[13] = new SqlParameter("@P_P_PIN", objMD.P_PIN);
                      objParams[14] = new SqlParameter("@P_T_ADDRESS", objMD.T_ADDRESS);
                      objParams[15] = new SqlParameter("@P_T_CITY", objMD.T_CITY);
                      objParams[16] = new SqlParameter("@P_T_STATE", objMD.T_STATE);
                      objParams[17] = new SqlParameter("@P_T_PHONE", objMD.T_PHONE);
                      objParams[18] = new SqlParameter("@P_T_EMAIL", objMD.T_EMAIL);
                      objParams[19] = new SqlParameter("@P_T_PIN", objMD.T_PIN);
                      objParams[20] = new SqlParameter("@P_USERID", objMD.USERID);
                      objParams[21] = new SqlParameter("@P_AUDITDATE", DateTime.Now);
                      if (objMD.DOB == DateTime.MinValue)
                          objParams[22] = new SqlParameter("@P_DOB", DBNull.Value);
                      else
                          objParams[22] = new SqlParameter("@P_DOB", objMD.DOB);
                      //objParams[22] = new SqlParameter("@P_DOB", objMD.DOB);
                      objParams[23] = new SqlParameter("@P_PROFESSION", objMD.PROFESSION);
                      objParams[24] = new SqlParameter("@P_ACAD_QUALI", objMD.ACAD_QUALI);
                      objParams[25] = new SqlParameter("@P_PAN_NO", objMD.PAN_NO);

                      if (objMD.FROM_DATE == DateTime.MinValue)
                      {
                          objParams[26] = new SqlParameter("@P_FROM_DATE", DBNull.Value);
                      }
                      else
                      {
                          objParams[26] = new SqlParameter("@P_FROM_DATE", objMD.FROM_DATE);
                      }

                      if (objMD.TO_DATE == DateTime.MinValue)
                      {
                          objParams[27] = new SqlParameter("@P_TO_DATE", DBNull.Value);
                      }
                      else
                      {
                          objParams[27] = new SqlParameter("@P_TO_DATE", objMD.TO_DATE);
                      }

                      //objParams[26] = new SqlParameter("@P_FROM_DATE", objMD.FROM_DATE);
                      //objParams[27] = new SqlParameter("@P_TO_DATE", objMD.TO_DATE);

                      objParams[28] = new SqlParameter("@P_P_ADDRESSLINE", objMD.P_ADDLINE);
                      objParams[29] = new SqlParameter("@P_P_COUNTRY", objMD.P_COUNTRY);
                      objParams[30] = new SqlParameter("@P_P_MOBILE", objMD.P_MOBILE);
                      objParams[31] = new SqlParameter("@P_T_ADDRESSLINE", objMD.T_ADDLINE);
                      objParams[32] = new SqlParameter("@P_T_COUNTRY", objMD.T_COUNTRY);
                      objParams[33] = new SqlParameter("@P_T_MOBILE", objMD.T_MOBILE);

                      objParams[34] = new SqlParameter("@P_QTYPE", objMD.QTYPE);
                      objParams[35] = new SqlParameter("@P_IS_SAME_ADDRESS", objMD.IS_SAME_ADDRESS);

                      if (objMD.JOINING_DATE == DateTime.MinValue)
                      {
                          objParams[36] = new SqlParameter("@P_JOINING_DATE", DBNull.Value);
                      }
                      else
                      {
                          objParams[36] = new SqlParameter("@P_JOINING_DATE", objMD.JOINING_DATE);
                      }

                      objParams[37] = new SqlParameter("@P_COLLEGENO", objMD.COLLEGENO);
                      objParams[38] = new SqlParameter("@P_DEPTNO", objMD.DEPTNO);
                      objParams[39] = new SqlParameter("@P_MEMBER_TYPE", objMD.MEMBER_TYPE);
                      objParams[40] = new SqlParameter("@P_OUT", SqlDbType.Int);
                      objParams[40].Direction = ParameterDirection.Output;

                      object ret = objSQLHelper.ExecuteNonQuerySP("PKG_MM_MEMBER_DETAIL_INSERT_UPDATE", objParams, true);
                      if (Convert.ToInt32(ret) == -99)
                          retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                      else
                          retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                  }
                  catch (Exception ex)
                  {
                      retstatus = Convert.ToInt32(CustomStatus.Error);
                      throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MR_Controller.AddUpdate_MR_Bill_Details->" + ex.ToString());
                  }
                  return retstatus;
              }


              // This method is used to get login credentailas for MOM Draft via mail.
              public DataSet GetFromDataForEmail(int ID)
              {
                  DataSet ds = null;
                  try
                  {
                      SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                      SqlParameter[] objParams = new SqlParameter[1];
                      objParams[0] = new SqlParameter("@P_ID", ID);
                      ds = objSQLHelper.ExecuteDataSetSP("PKG_MM_GET_DATA_TO_SEND_MOM_DRAFT", objParams);

                  }
                  catch (Exception ex)
                  {
                      return ds;
                      throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MeetingController.GetFromDataForEmail-> " + ex.ToString());
                  }
                  return ds;
              }

            }
        }
    }
}
