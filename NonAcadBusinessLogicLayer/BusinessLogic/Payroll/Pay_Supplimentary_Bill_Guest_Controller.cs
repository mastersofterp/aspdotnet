//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : BUSINESS LOGIC FILE //[SupplimentaryBill_Controller]                                  
// CREATION DATE : 02-NOV-2009                                                        
// CREATED BY    : KIRAN GVS                                       
// MODIFIED DATE :
// MODIFIED DESC :
//====================================================================================== 
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
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
           public class Pay_Supplimentary_Bill_Guest_Controller
            {
               string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

               //Add Get Parameter from Entity 
               public int AddSupplimentryGuest(Pay_Supplimentary_Bill_Guest objsupbill)
               {
                   int retStatus = Convert.ToInt32(CustomStatus.Others);
                   try
                   {
                       SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                       //AddSupplimentaryBill
                       SqlParameter[] sqlParams = new SqlParameter[]
                        {
                           
                            new SqlParameter("@P_FINYEARID",objsupbill.FINYEARID),
                            new SqlParameter("@P_EMPLOYEENAME",objsupbill.EMPLOYEENAME),
                            new SqlParameter("@P_SUPLHEAD",objsupbill.SUPLHEAD),
                            new SqlParameter("@P_SUPLHNO",objsupbill.SUPLHNO),
                            new SqlParameter("@P_SUBDESIGNO",objsupbill.SUBDESIGNO),
                            new SqlParameter("@P_DEPARTMENTNO",objsupbill.DEPARTMENTNO),
                            new SqlParameter("@P_CODE",objsupbill.CODE),
                            new SqlParameter("@P_ORDNO",objsupbill.ORDNO),
                            new SqlParameter("@P_SBDATE",objsupbill.SBDATE),                    
                            new SqlParameter("@P_PANNO",objsupbill.PANNO),
                            new SqlParameter("@P_BANKNO",objsupbill.BANKNO),
                            new SqlParameter("@P_EMAILID",objsupbill.EMAILID),
                            new SqlParameter("@P_MOBILENO",objsupbill.MOBILENO),
                            new SqlParameter("@P_ACCNO",objsupbill.ACCNO),
                            new SqlParameter("@P_IFSCCODE",objsupbill.IFSCCODE),
                            new SqlParameter("@P_TOTAL_AMOUNT",objsupbill.TOTAL_AMOUNT),
                            new SqlParameter("@P_TDS_PER",objsupbill.TDS_PER),
                            new SqlParameter("@P_TDS_AMOUNT",objsupbill.TDS_AMOUNT),
                            new SqlParameter("@P_NET_AMOUNT",objsupbill.NET_AMOUNT),
                            new SqlParameter("@P_COLLEGE_CODE",objsupbill.COLLEGE_CODE),  
                            new SqlParameter("@P_REMARK",objsupbill.REMARK),    
                            new SqlParameter("@P_TDS_NETAMOUNT",objsupbill.TDS_NETAMOUNT), 
                            new SqlParameter("@P_SECTIONID",objsupbill.SECTIONID), 
                        };

                       if (objSQLHelper.ExecuteNonQuerySP("ACD_ADDSUPPLIMENTARYGUEST_DETAIL", sqlParams, false) != null)
                           retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                   }
                   catch (Exception ex)
                   {
                       retStatus = Convert.ToInt32(CustomStatus.Error);
                       throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Pay_Supplimentary_Bill_Guest_Controller.AddSupplimentryGuest -> " + ex.ToString());
                   }
                   return retStatus;
               }

              //Get Details 
               public DataSet GetAllSupplimentaryGuest(int SUPLGUESTID)
               {
                   DataSet ds = null;
                   try
                   {
                       SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                       SqlParameter[] objparams = null;
                       objparams = new SqlParameter[1];
                       objparams[0] = new SqlParameter("@_SUPLGUESTID", SUPLGUESTID);
                       ds = objSQLHelper.ExecuteDataSetSP("ACD_GET_BY_ID_SUPPLIMENTARYGUEST_DETAIL", objparams);
                   }
                   catch (Exception ex)
                   {
                       throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.Pay_SupplimentaryBill_Controller.GeAllSupplimentaryBill() --> " + ex.Message + " " + ex.StackTrace);
                   }
                   return ds;
               }

               //Get By ID
               public DataSet GetSingleSupplimentaryGuest(Pay_Supplimentary_Bill_Guest objSuppliGuestEntity)
               {
                   DataSet ds = null;
                   try
                   {
                       SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                       SqlParameter[] objparams = null;
                       objparams = new SqlParameter[2];
                       objparams[0] = new SqlParameter("@panNo", objSuppliGuestEntity.PANNO);
                       objparams[1] = new SqlParameter("@finYearId", objSuppliGuestEntity.FINYEARID);

                       ds = objSQLHelper.ExecuteDataSetSP("ACD_GETSUPPLIMENTARYGUEST_DETAIL", objparams);
                   }
                   catch (Exception ex)
                   {
                       throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.Pay_Supplimentary_Bill_Guest_Controller.GetSingleSupplimentaryGuest() --> " + ex.Message + " " + ex.StackTrace);
                   }
                   return ds;
               }
              
               //Update Get Parameter from Entity 
               public int UpdateSupplimentryGuest(Pay_Supplimentary_Bill_Guest objsupbill)
               {
                   int retStatus = Convert.ToInt32(CustomStatus.Others);
                   try
                   {
                       SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                       //AddSupplimentaryBill
                       SqlParameter[] sqlParams = new SqlParameter[]
                        {
                            new SqlParameter("@P_SUPLGUESTID",objsupbill.SUPLGUESTID),
                            new SqlParameter("@P_FINYEARID",objsupbill.FINYEARID),
                            new SqlParameter("@P_EMPLOYEENAME",objsupbill.EMPLOYEENAME),
                            new SqlParameter("@P_SUPLHEAD",objsupbill.SUPLHEAD),
                            new SqlParameter("@P_SUPLHNO",objsupbill.SUPLHNO),
                            new SqlParameter("@P_SUBDESIGNO",objsupbill.SUBDESIGNO),
                            new SqlParameter("@P_DEPARTMENTNO",objsupbill.DEPARTMENTNO),
                            new SqlParameter("@P_CODE",objsupbill.CODE),
                            new SqlParameter("@P_ORDNO",objsupbill.ORDNO),
                            new SqlParameter("@P_SBDATE",objsupbill.SBDATE),                    
                            new SqlParameter("@P_PANNO",objsupbill.PANNO),
                            new SqlParameter("@P_BANKNO",objsupbill.BANKNO),
                            new SqlParameter("@P_EMAILID",objsupbill.EMAILID),
                            new SqlParameter("@P_MOBILENO",objsupbill.MOBILENO),
                            new SqlParameter("@P_ACCNO",objsupbill.ACCNO),
                            new SqlParameter("@P_IFSCCODE",objsupbill.IFSCCODE),
                            new SqlParameter("@P_TOTAL_AMOUNT",objsupbill.TOTAL_AMOUNT),
                            new SqlParameter("@P_TDS_PER",objsupbill.TDS_PER),
                            new SqlParameter("@P_TDS_AMOUNT",objsupbill.TDS_AMOUNT),
                            new SqlParameter("@P_NET_AMOUNT",objsupbill.NET_AMOUNT),
                            new SqlParameter("@P_COLLEGE_CODE",objsupbill.COLLEGE_CODE),  
                            new SqlParameter("@P_REMARK",objsupbill.REMARK), 
                             new SqlParameter("@P_TDS_NETAMOUNT",objsupbill.TDS_NETAMOUNT), 
                               new SqlParameter("@P_SECTIONID",objsupbill.SECTIONID), 
                        };

                       if (objSQLHelper.ExecuteNonQuerySP("ACD_UPDATE_SUPPLIMENTARYGUEST_DETAIL", sqlParams, false) != null)
                           retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                   }
                   catch (Exception ex)
                   {
                       retStatus = Convert.ToInt32(CustomStatus.Error);
                       throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Pay_Supplimentary_Bill_Guest_Controller.UpdateSupplimentryGuest -> " + ex.ToString());
                   }
                   return retStatus;
               }
            }
        }
    }
}
