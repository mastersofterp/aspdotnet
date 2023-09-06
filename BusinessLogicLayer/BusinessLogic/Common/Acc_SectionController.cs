using System;
using System.Data;
using System.Web;

using IITMS;
using IITMS.UAIMS.BusinessLayer;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
            /// <summary>
            /// This Acc_SectionController is used to control Acc_Section table.
            /// </summary>
            public class Acc_SectionController
            {
                /// <summary>
                /// ConnectionStrings
                /// </summary>
                private string _uaims_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                /// <summary>
                /// AddAcc_Section method is used to add new Acc_Section.
                /// </summary>
                /// <param name="objAS">objAS is the object of Acc_Section class</param>
                /// <returns>Integer CustomStatus Record Added or Error</returns>
                public int AddAcc_Section(Acc_Section objAS, bool status)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;

                        //Add New Acc_Section
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_AS_TITLE", objAS.As_Title);
                        objParams[1] = new SqlParameter("@P_AS_SRNO", objAS.As_SrNo);
                        objParams[2] = new SqlParameter("@P_IS_ACTIVE", status);
                        objParams[3] = new SqlParameter("@P_AS_NO", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_DOMAINS_SP_INS_DOMAIN", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Acc_SectionController.AddAcc_Section-> " + ex.ToString());
                    }
                    return retStatus;
                }

                /// <summary>
                /// UpdateAcc_Section method is used to update existing Acc_Section.
                /// </summary>
                /// <param name="objAS">objAS is the object of Acc_Section class</param>
                /// <returns>Integer CustomStatus Record Updated or Error</returns>
                public int UpdateAcc_Section(Acc_Section objAS, bool status)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;

                        //UpdateFaculty Acc_Section
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_AS_NO", objAS.As_No);
                        objParams[1] = new SqlParameter("@P_AS_TITLE", objAS.As_Title);
                        objParams[2] = new SqlParameter("@P_IS_ACTIVE", status);
                        objParams[3] = new SqlParameter("@P_AS_SRNO", objAS.As_SrNo);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_DOMAINS_SP_UPD_DOMAIN", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Acc_SectionController.UpdateAcc_Section-> " + ex.ToString());
                    }
                    return retStatus;
                }
                /////// <summary>
                /////// DeleteAcc_Section method is used to delete existing Acc_Section.
                /////// </summary>
                /////// <param name="as_no">Delete Acc_Section as per this as_no</param>
                /////// <returns>Integer CustomStatus Record Deleted or Error</returns>
                ////public int DeleteAcc_Section(int as_no)
                ////{
                ////    int retStatus = Convert.ToInt32(CustomStatus.Others);
                ////    try
                ////    {
                ////        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                ////        SqlParameter[] objParams = new SqlParameter[1];
                ////        objParams[0] = new SqlParameter("@P_AS_NO", as_no);

                ////        objSQLHelper.ExecuteNonQuerySP("Pkg_Domains_SP_DEL_DOMAIN", objParams, false);
                ////        retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                ////    }
                ////    catch (Exception ex)
                ////    {
                ////        retStatus = Convert.ToInt32(CustomStatus.Error);
                ////        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.NewsController.GetSingleNews-> " + ex.ToString());
                ////    }

                ////    return Convert.ToInt32(retStatus);
                ////}

                /// <summary>
                /// GetAllAcc_Sections method is used to retrieve all existing Acc_Sections.
                /// </summary>
                /// <returns>Dataset/returns>
                public DataSet GetAllAcc_Sections()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_DOMAINS_SP_ALL_DOMAIN", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Acc_SectionController.GetAllAcc_Sections-> " + ex.ToString());
                    }
                    return ds;
                }


            }

        }//END: BusinessLayer.BusinessLogic

    }//END: UAIMS  

}//END: IITMS