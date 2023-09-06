using System;
using System.Collections.Generic;
using System.Linq;

using System.Data.SqlClient;
using System.Data;
using IITMS;
using IITMS.UAIMS;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;

namespace IITMS.UAIMS.BusinessLogicLayer.BusinessLogic
{
    public class ConsumerMasterController
    {
         string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
          
            //public int AddconsumerEntry(ConsumerMasterEntity objConMas)
            //{
            //    int status = 0;
            //    try
            //    {
            //        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
            //        SqlParameter[] sqlParams = new SqlParameter[]
            //         {
            //              new SqlParameter("@P_CONSUMERTYPE",objConMas.Consumertype),
            //              new SqlParameter("@P_TITLE",objConMas.Title),
            //              new SqlParameter("@P_FNAME" ,objConMas.Firstname),
            //              new SqlParameter("@P_MNAME",objConMas.Middlename),
            //              new SqlParameter("@P_LNAME", objConMas.Lastname),
            //              new SqlParameter("@P_SEX", objConMas.Gender),

            //              new SqlParameter("@P_DOB", objConMas.Dateofbirth),
            //              new SqlParameter("@P_DOJ", objConMas.Dateofjoining),

                          //new SqlParameter("@P_RESADD1",objConMas.Loccaladdress),
                          //new SqlParameter("@P_TOWNADD1",objConMas.PermanentAddress),
                          //new SqlParameter("@P_PHONENO",objConMas.Phonenumber),
                          //new SqlParameter("@P_EMAILID",objConMas.Emailaddress),
                          //new SqlParameter("@P_PAN_NO",objConMas.PANnumber),
                          //new SqlParameter("@P_COLLEGE_CODE",objConMas.COLLEGECODE),
                          //new SqlParameter("@P_CONSUMERFULLNAME",objConMas.Consumerfullname),
                          //new SqlParameter("@P_MartialStatus",objConMas.Martial),
                          //new SqlParameter("@P_SUBDEPTNO",objConMas.Department),
                          //new SqlParameter("@P_SUBDESIGNO",objConMas.Dessignation),
                          //new SqlParameter("@P_ChkStatus",objConMas.Checkstatus),
                          //new SqlParameter("@p_OUTPUT",objConMas.ConsumerIDNO)

            //      };
            //        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

            //        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_EST_INSERTINTO_CONMSUMERMASTER", sqlParams, true);

            //        if (obj != null && obj.ToString() != "-99")
            //        {
            //            status = Convert.ToInt32(CustomStatus.RecordSaved);
            //        }
            //        else
            //            status = Convert.ToInt32(CustomStatus.Error);
            //    }
            //    catch (Exception ex)
            //    {
            //        status = Convert.ToInt32(CustomStatus.Error);
            //        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Disciplinary_ActionController.AddEvent() --> " + ex.Message + " " + ex.StackTrace);
            //    }
            //    return status;
            //}

            public int AddconsumerEntry(ConsumerMasterEntity objConMas)
            {
                int retStatus = 0;
                try
                {
                    SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                    SqlParameter[] objParams = null;
                    objParams = new SqlParameter[21];
                    objParams[0] = new SqlParameter("@P_IDNO", objConMas.ConsumerIDNO); 
                    objParams[1] = new SqlParameter("@P_CONSUMERTYPE",objConMas.Consumertype);
                    objParams[2] = new SqlParameter("@P_TITLE", objConMas.Title);
                    objParams[3] = new SqlParameter("@P_FNAME", objConMas.Firstname);
                    objParams[4] = new SqlParameter("@P_MNAME", objConMas.Middlename);
                    objParams[5] = new SqlParameter("@P_LNAME", objConMas.Lastname);
                    objParams[6] = new SqlParameter("@P_SEX", objConMas.Gender);

                    if (objConMas.Dateofbirth == DateTime.MinValue)
                    {
                        objParams[7] = new SqlParameter("@P_DOB", DBNull.Value);
                    }
                    else
                    {
                        objParams[7] = new SqlParameter("@P_DOB", objConMas.Dateofbirth);
                    }
                    if (objConMas.Dateofjoining == DateTime.MinValue)
                    {
                        objParams[8] = new SqlParameter("@P_DOJ", DBNull.Value);
                    }
                    else
                    {
                        objParams[8] = new SqlParameter("@P_DOJ", objConMas.Dateofjoining);
                    }
                        objParams[9] = new SqlParameter("@P_RESADD1",objConMas.Loccaladdress);
                        objParams[10] = new SqlParameter("@P_TOWNADD1",objConMas.PermanentAddress);
                        objParams[11] = new SqlParameter("@P_PHONENO",objConMas.Phonenumber);
                        objParams[12] = new SqlParameter("@P_EMAILID",objConMas.Emailaddress);
                        objParams[13] = new SqlParameter("@P_PAN_NO",objConMas.PANnumber);
                        objParams[14] = new SqlParameter("@P_COLLEGE_CODE",objConMas.COLLEGECODE);
                        objParams[15] = new SqlParameter("@P_CONSUMERFULLNAME",objConMas.Consumerfullname);
                        objParams[16] = new SqlParameter("@P_MartialStatus",objConMas.Martial);
                        objParams[17] = new SqlParameter("@P_SUBDEPTNO",objConMas.Department);
                        objParams[18] = new SqlParameter("@P_SUBDESIGNO",objConMas.Dessignation);
                        objParams[19] = new SqlParameter("@P_ChkStatus", objConMas.Checkstatus);
                        objParams[20] = new SqlParameter("@P_OUTPUT",SqlDbType.Int);
                        objParams[20].Direction = ParameterDirection.Output;

                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_EST_INSERTINTO_CONMSUMERMASTER", objParams, true);

                    if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                        retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    else
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                }
                catch (Exception ex)
                {
                    retStatus = Convert.ToInt32(CustomStatus.Error);
                    throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ConsumerMasterController.AddconsumerEntry() --> " + ex.Message + " " + ex.StackTrace);
                }
                return retStatus;
            }


          
        
             public DataTableReader GetemployeeInformation(int id)
             {
                DataTableReader dtr = null;

                try
                {
                    SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                    SqlParameter[] objParams = new SqlParameter[1];
                    objParams[0] = new SqlParameter("@p_IDNO", id);

                    dtr = objSQLHelper.ExecuteDataSetSP("PKG_ESTATE_GETCONSUMERDATA", objParams).Tables[0].CreateDataReader();
                }
                catch (Exception ex)
                {
                    throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.User_AccController.GetemployeeInformation-> " + ex.ToString());
                }
                return dtr;
            }



             public SqlDataReader AutoCompleteEmpInfo(string preFix)
             {
                 SqlDataReader dr = null;
                 try
                 {
                     SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                     SqlParameter[] objParams = new SqlParameter[]
                            { 
                              new SqlParameter("@P_PREFIX",preFix)  
                            };
                     dr = objSQLHelper.ExecuteReaderSP("PKG_ESTATE_AUTOCOMPLETE_EMPINFO", objParams);
                 }
                 catch (Exception ex)
                 {
                     throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessEntities.LibAutoComplete.AutoCompleteAccNo() --> " + ex.Message + " " + ex.StackTrace);
                 }
                 return dr;
             }



             public int ImportDataFromPayRoll()
             {
                 int status = 0;
                 try
                 {
                     SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                     SqlParameter[] objParams = new SqlParameter[0];

                    // status = (int)objSQLHelper.ExecuteNonQuerySP("PKG_ESTATE_INSERT_PAYROLLDATA",objParams,true);

                     status = Convert.ToInt16(objSQLHelper.ExecuteNonQuerySP("PKG_ESTATE_INSERT_PAYROLLDATA",objParams, false));


               
                 
                 }
                 catch (Exception ex)
                 {
                     status = Convert.ToInt32(CustomStatus.Error);
                     throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Disciplinary_ActionController.AddEvent() --> " + ex.Message + " " + ex.StackTrace);
                 }
                 return status;
             }

          


    }
 }

   
