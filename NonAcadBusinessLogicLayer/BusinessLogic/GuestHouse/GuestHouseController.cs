using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;
using System.Net;
using System.Net.NetworkInformation;
using System.Diagnostics;

using System.Runtime.InteropServices;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Collections;
using System.IO;
using System.Data.SqlClient;
using System.Reflection;
using IITMS.SQLServer.SQLDAL;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;


namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
            public class GuestHouseController
            {

                private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
                #region Guest House
                string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
                public long AddUpdateGuestCategory(GuestHouse objMR)
                {
                    long retstatus = 0;
                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_GUEST_CATEGORY_ID", objMR.GUEST_CATEGORY_ID);
                        objParams[1] = new SqlParameter("@P_GUEST_CATEGORY_NAME ", objMR.GUEST_CATEGORY_NAME);
                        objParams[2] = new SqlParameter("@P_CHARGE ", objMR.CHARGE);
                        objParams[3] = new SqlParameter("@P_TAX", objMR.TAX);
                        objParams[4] = new SqlParameter("@P_CHECK_ID", objMR.CHECK_ID);

                        objParams[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[5].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_GUEST_INSERT_UPDATE_CATEGORY", objParams, true);
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

                public long AddUpdateGuestHouseRoom(GuestHouse objMR)
                {
                    long retstatus = 0;
                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_GUEST_ROOM_NAME_ID", objMR.GUEST_ROOM_NAME_ID);
                        objParams[1] = new SqlParameter("@P_GUEST_ROOM_NAME ", objMR.GUEST_ROOM_NAME);
                        objParams[2] = new SqlParameter("@P_GUEST_HOUSE_NAME", objMR.GHID); 
                        objParams[3] = new SqlParameter("@P_GUEST_HOUSE_CATEGORY", objMR.GUEST_CATEGORY_ID); 

                        objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_GUEST_HOUSE_ROOM_INSERT_UPDATE", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GuestHouseController.AddUpdate_MR_Bill_Details->" + ex.ToString());
                    }
                    return retstatus;
                }

                public DataSet FillDropDown(string TableName, string Column1, string Column2, string wherecondition, string orderby)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objsqlhelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_TABLENAME", TableName);
                        objParams[1] = new SqlParameter("@P_COLUMNNAME_1", Column1);
                        objParams[2] = new SqlParameter("@P_COLUMNNAME_2", Column2);
                        if (!wherecondition.Equals(string.Empty))
                            objParams[3] = new SqlParameter("@P_WHERECONDITION", wherecondition);
                        else
                            objParams[3] = new SqlParameter("@P_WHERECONDITION", DBNull.Value);
                        if (!orderby.Equals(string.Empty))
                            objParams[4] = new SqlParameter("@P_ORDERBY", orderby);
                        else
                            objParams[4] = new SqlParameter("@P_ORDERBY", DBNull.Value);

                        ds = objsqlhelper.ExecuteDataSetSP("PKG_UTILS_SP_DROPDOWN", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.Common.FillDropDown-> " + ex.ToString());
                    }
                    return ds;
                }

                public void FillDropDownList(DropDownList ddlList, string TableName, string Column1, string Column2, string wherecondition, string orderby)
                {
                    try
                    {
                        SQLHelper objsqlhelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_TABLENAME", TableName);
                        objParams[1] = new SqlParameter("@P_COLUMNNAME_1", Column1);
                        objParams[2] = new SqlParameter("@P_COLUMNNAME_2", Column2);
                        if (!wherecondition.Equals(string.Empty))
                            objParams[3] = new SqlParameter("@P_WHERECONDITION", wherecondition);
                        else
                            objParams[3] = new SqlParameter("@P_WHERECONDITION", DBNull.Value);
                        if (!orderby.Equals(string.Empty))
                            objParams[4] = new SqlParameter("@P_ORDERBY", orderby);
                        else
                            objParams[4] = new SqlParameter("@P_ORDERBY", DBNull.Value);

                        DataSet ds = null;
                        ds = objsqlhelper.ExecuteDataSetSP("PKG_UTILS_SP_DROPDOWN", objParams);

                        ddlList.Items.Clear();
                        ddlList.Items.Add("Please Select");
                        ddlList.SelectedItem.Value = "0";

                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            ddlList.DataSource = ds;
                            ddlList.DataValueField = ds.Tables[0].Columns[0].ToString();
                            ddlList.DataTextField = ds.Tables[0].Columns[1].ToString();
                            ddlList.DataBind();
                            ddlList.SelectedIndex = 0;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.Common.FillDropDown-> " + ex.ToString());
                    }
                    // return ddlList;
                }

                public long AddUpdateBookingDetails(GuestHouse Objmr)
                {
                    long retstatus = 0;
                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[18];
                        objParams[0] = new SqlParameter("@P_GUEST_BOOKING_DETAILS_ID", Objmr.GUEST_BOOKING_DETAILS_ID);
                        objParams[1] = new SqlParameter("@P_VISITOR_NAME", Objmr.VISITOR_NAME);
                        objParams[2] = new SqlParameter("@P_ARRIVAL_DATE", Objmr.ARRIVAL_DATE);
                        objParams[3] = new SqlParameter("@P_DEPARTURE_DATE", Objmr.DEPARTURE_DATE);
                        objParams[4] = new SqlParameter("@P_GUEST_CATEGORY", Objmr.GUEST_CATEGORY_ID);
                        objParams[5] = new SqlParameter("@P_JUSTIFICATION", Objmr.JUSTIFICATION);
                        objParams[6] = new SqlParameter("@P_NO_OF_ROOM", Objmr.NO_OF_ROOM);
                        objParams[7] = new SqlParameter("@P_VISIT_PURPOSE", Objmr.VISIT_PURPOSE);

                        objParams[8] = new SqlParameter("@P_ADVANCE_PAYMENT", Objmr.ADVANCE_PAYMENT);
                        objParams[9] = new SqlParameter("@P_BOOKING_PERSON_ID", Objmr.BOOKING_PERSON_ID);
                        // objParams[10] = new SqlParameter("@P_DESIGNATION", Objmr.DESIGNATION);
                        objParams[10] = new SqlParameter("@P_MOBILE_NO", Objmr.MOBILE_NO);
                        objParams[11] = new SqlParameter("@P_ARRIVAL_TIME", Objmr.ARRIVAL_TIME);
                        objParams[12] = new SqlParameter("@P_DEPARTMENT_TIME", Objmr.DEPARTMENT_TIME);
                        objParams[13] = new SqlParameter("@P_POSTAL_ADDRESS", Objmr.POSTAL_ADDRESS);
                        objParams[14] = new SqlParameter("@P_NO_OF_VISITORS", Objmr.NO_OF_VISITORS);
                        if (Objmr.ADVANCE_PAYMENT_DATE == DateTime.MinValue)
                        {
                            objParams[15] = new SqlParameter("@P_ADVANCE_PAYMENT_DATE", DBNull.Value);
                        }
                        else
                        {
                            objParams[15] = new SqlParameter("@P_ADVANCE_PAYMENT_DATE", Objmr.ADVANCE_PAYMENT_DATE);
                        }
                        
                        objParams[16] = new SqlParameter("@P_PAPNO", Objmr.PAPNO);

                        objParams[17] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[17].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_GUEST_ADD_PERSONAL_APPROVAL_DETAILS_INSERT", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GuestHouseController.AddUpdateBookingDetails->" + ex.ToString());
                    }
                    return retstatus;
                }

                public int AddPassAuthority(GuestHouse Objmr)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_PANAME", Objmr.PANAME);
                        objParams[1] = new SqlParameter("@P_UA_NO", Objmr.UANO);
                        objParams[2] = new SqlParameter("@P_COLLEGE_CODE", Objmr.COLLEGE_CODE);
                        objParams[3] = new SqlParameter("@P_PANO", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_GUEST_PASSING_AUTHORITY_INSERT", objParams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GuestHouseController.AddPassAuthority->" + ex.ToString());
                    }
                    return retstatus;
                }

                public int UpdatePassAuthority(GuestHouse Objmr)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_PANO", Objmr.PANO);
                        objParams[1] = new SqlParameter("@P_PANAME", Objmr.PANAME);
                        objParams[2] = new SqlParameter("@P_UA_NO", Objmr.UANO);
                        objParams[3] = new SqlParameter("@P_COLLEGE_CODE", Objmr.COLLEGE_CODE);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_GUEST_PASSING_AUTHORITY_UPDATE", objParams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GuestHouseController.UpdatePassAuthority->" + ex.ToString());
                    }
                    return retstatus;
                }

                public DataSet GetAllPassAuthority()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GUEST_PASSING_AUTHORITY", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.GuestHouseController.GetAllPassAuthority->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetPassAuthority(int PANo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null; ;
                        objparams = new SqlParameter[1];
                        objparams[0] = new SqlParameter("@P_PANO", PANo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GUEST_PASSING_AUTHORITY_GET_BY_NO", objparams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GuestHouseController.GetPassAuthority->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }


                public DataSet GetAllPAPath(int collegeno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = new SqlParameter[0];
                        //objparams[0] = new SqlParameter("@P_COLLEGE_NO", collegeno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMN_IQAC_GET_ALL_APPROVAL_AUTHORITY_PATH", objparams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.GuestHouseController.GetAllPAPath->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                //public int AddPAPath(GuestHouse Objmr, DataTable dtEmpRecord)
                //{
                //    int retstatus = 0;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                //        SqlParameter[] objparams = new SqlParameter[12];
                //        objparams[0] = new SqlParameter("@P_PAPNO", Objmr.PAPNO);
                //        objparams[1] = new SqlParameter("@P_PAN01", Objmr.PAN01);
                //        objparams[2] = new SqlParameter("@P_PAN02", Objmr.PAN02);
                //        objparams[3] = new SqlParameter("@P_PAN03", Objmr.PAN03);
                //        objparams[4] = new SqlParameter("@P_PAN04", Objmr.PAN04);
                //        objparams[5] = new SqlParameter("@P_PAN05", Objmr.PAN05);
                //        objparams[6] = new SqlParameter("@P_PAPATH", Objmr.PAPATH);
                //        //objparams[7] = new SqlParameter("@P_COLLEGE_NO", objPAEnt.COLLEGE_NO);
                //        objparams[7] = new SqlParameter("@P_COLLEGE_CODE", Objmr.COLLEGE_CODE);
                //        objparams[8] = new SqlParameter("@P_SUBDESIGNO", Objmr.SUBDESIGNO);
                //        objparams[9] = new SqlParameter("@P_DEPTNO", Objmr.DEPTNO);
                //        objparams[10] = new SqlParameter("@P_IQAC_EMP_TABLE", dtEmpRecord);
                //        objparams[11] = new SqlParameter("@P_OUT", SqlDbType.Int);
                //        objparams[11].Direction = ParameterDirection.Output;

                //        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ADMN_IQAC_APPROVAL_AUTHORITY_PATH_IU", objparams, true);

                //        if (Convert.ToInt32(ret) == 1)
                //        {
                //            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                //        }
                //        else if (Convert.ToInt32(ret) == 2627)
                //        {
                //            retstatus = Convert.ToInt32(CustomStatus.RecordExist);
                //        }
                //        else if (Convert.ToInt32(ret) == 2)
                //        {
                //            retstatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                //        }
                //        else
                //        {
                //            retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                //        }
                //    }
                //    catch (Exception ex)
                //    {
                //        retstatus = Convert.ToInt32(CustomStatus.Error);
                //        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.IqacPassingAuthorityController.AddPAPath->" + ex.ToString());
                //    }
                //    return retstatus;
                //}

                public DataSet GetSinglePAPath(int PAPNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[1];
                        objparams[0] = new SqlParameter("@P_PAPNO", PAPNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMN_IQAC_GET_AUTHORITY_PATH_BY_ID", objparams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.GuestHouseController.GetSinglePAPath->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public int DeletePAPath(int PAPNO)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[1];
                        objparams[0] = new SqlParameter("@PAPNO", PAPNO);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ADMN_IQAC_APPROVAL_AUTHO_PATH_DELETE", objparams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.GuestHouseController.DeletePAPath->" + ex.ToString());
                    }
                    return retstatus;
                }

                public DataSet GetAllPassingPath()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GUEST_PASSING_AUTHORITY_PATH_GET_BYALL", objparams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GuestHouseController.GetAllPassingPath->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public int AddPassingPath(GuestHouse Objmr)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[10];
                        objparams[0] = new SqlParameter("@P_PAN01", Objmr.PAN01);
                        objparams[1] = new SqlParameter("@P_PAN02", Objmr.PAN02);
                        objparams[2] = new SqlParameter("@P_PAN03", Objmr.PAN03);
                        objparams[3] = new SqlParameter("@P_PAN04", Objmr.PAN04);
                        objparams[4] = new SqlParameter("@P_PAN05", Objmr.PAN05);
                        objparams[5] = new SqlParameter("@P_PAPATH", Objmr.PAPATH);
                        objparams[6] = new SqlParameter("@P_GUEST_CATEGORY", Objmr.GUEST_CAT);
                        objparams[7] = new SqlParameter("@P_COLLEGE_CODE", Objmr.COLLEGE_CODE);
                        objparams[8] = new SqlParameter("@P_DEPARTMENT", Objmr.DEPARTMENT);
                        objparams[9] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objparams[9].Direction = ParameterDirection.Output;
                        //if (objSQLHelper.ExecuteNonQuerySP("PKG_GUEST_PASSING_AUTHORITY_PATH_INSERT", objparams, false) != null)
                        object ret = (objSQLHelper.ExecuteNonQuerySP("PKG_GUEST_PASSING_AUTHORITY_PATH_INSERT", objparams, true));
                        if (Convert.ToInt32(ret) == 1)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordExist);
                        }
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.GuestHouseController.AddPassingPath->" + ex.ToString());
                    }
                    return retstatus;
                }

                public int DeleteODPAPath(int PAPNO)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[1];
                        objparams[0] = new SqlParameter("@PAPNO", PAPNO);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_OD_PAY_PASSING_AUTHORITY_PATH_DELETE", objparams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GuestHouseController.DeleteODPAPath->" + ex.ToString());
                    }
                    return retstatus;
                }

                public DataSet GetSinglePassingPath(int PAPNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[1];
                        objparams[0] = new SqlParameter("@P_PAPNO", PAPNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GUEST_PASSING_AUTHORITY_PATH_GET_BY_NO", objparams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GuestHouseController.GetSinglePassingPath->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public int UpdatePassingPath(GuestHouse Objmr)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[10];
                        objparams[0] = new SqlParameter("@P_PAPNO", Objmr.PAPNO);
                        objparams[1] = new SqlParameter("@P_PAN01", Objmr.PAN01);
                        objparams[2] = new SqlParameter("@P_PAN02", Objmr.PAN02);
                        objparams[3] = new SqlParameter("@P_PAN03", Objmr.PAN03);
                        objparams[4] = new SqlParameter("@P_PAN04", Objmr.PAN04);
                        objparams[5] = new SqlParameter("@P_PAN05", Objmr.PAN05);
                        objparams[6] = new SqlParameter("@P_PAPATH", Objmr.PAPATH);
                        objparams[7] = new SqlParameter("@P_GUEST_CATEGORY", Objmr.GUEST_CAT);
                        objparams[8] = new SqlParameter("@P_COLLEGE_CODE", Objmr.COLLEGE_CODE);
                        objparams[9] = new SqlParameter("@P_DEPARTMENT", Objmr.DEPARTMENT);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_GUEST_PASSING_AUTHORITY_PATH_UPDATE", objparams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.GuestHouseController.UpdatePassingPath->" + ex.ToString());
                    }
                    return retstatus;
                }

                #region Guest Approval

                public DataSet GetPendListforApprovalGuest(int UA_No)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_UA_NO", UA_No);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GUEST_GET_PENDINGLIST", objParams);
                    }
                    catch (Exception ex)
                    {

                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GuestHouseController.GetPendListforApprovalGuest->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetBudget(int deptno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_DEPTNO", deptno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_OD_GET_BUDGET", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GuestHouseController.GetBudget->" + ex.ToString());

                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public int UpdateODAppPassEntry(int ODTRNO, int UA_NO, string STATUS, string REMARKS, DateTime APRDT, int p)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[6];
                        objparams[0] = new SqlParameter("@P_ODTRNO", ODTRNO);
                        objparams[1] = new SqlParameter("@P_UA_NO", UA_NO);
                        objparams[2] = new SqlParameter("@P_STATUS", STATUS);
                        objparams[3] = new SqlParameter("@P_REMARKS", REMARKS);
                        objparams[4] = new SqlParameter("@P_APRDT", APRDT);
                        objparams[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objparams[5].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_GUEST_PASS_ENTRY_UPDATE", objparams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                        //if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_PAY_APP_PASS_ENTRY_UPDATE1", objparams, false) != null)
                        //    retstatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GuestHouseController.UpdateLvAplAprStatus->" + ex.ToString());
                    }
                    return retStatus;

                }

                public int UpdateODAppEntry(int ODTRNO, double tadaAmount, double modRegAmt)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[4];
                        objparams[0] = new SqlParameter("@P_ODTRNO", ODTRNO);
                        objparams[1] = new SqlParameter("@P_TADA", tadaAmount);
                        objparams[2] = new SqlParameter("@P_MODI_REG_AMT", modRegAmt);
                        objparams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objparams[3].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_OD_PAY_APP_ENTRY_UPDATE", objparams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                        //if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_PAY_APP_PASS_ENTRY_UPDATE1", objparams, false) != null)
                        //    retstatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GuestHouseController.UpdateODAppEntry->" + ex.ToString());
                    }
                    return retStatus;

                }

                public int UpdateODBudget(int deptno, double budgallot, double budgutil, double budgbal)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[5];
                        objparams[0] = new SqlParameter("@P_DEPTNO", deptno);
                        objparams[1] = new SqlParameter("@P_BUDG_ALLOT", budgallot);
                        objparams[2] = new SqlParameter("@P_BUDG_UTIL", budgutil);
                        objparams[3] = new SqlParameter("@P_BUDG_BAL", budgbal);
                        objparams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objparams[4].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_OD_PAY_UPD_OD BUDGET", objparams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);



                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GuestHouseController.UpdateODBudget->" + ex.ToString());
                    }
                    return retStatus;

                }

                public DataSet GetBookingDetails(int GUEST_BOOKING_DETAILS_ID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_GUEST_BOOKING_DETAILS_ID", GUEST_BOOKING_DETAILS_ID);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GUEST_GET_BOOKING_DETAILS_BY_ID", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GuestHouseController.GetBookingDetails->" + ex.ToString());

                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetPendListforODApprovalStatusALL(int UA_No)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_UA_NO", UA_No);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_OD_PAY_APP_GET_PENDINGLIST_STATUS_ALL", objParams);
                    }
                    catch (Exception ex)
                    {

                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GuestHouseController.GetPendListforODApprovalStatusALL->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetPendListforODApprovalStatusParticular(int UA_No, string frmdt, string todt)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_UA_NO", UA_No);
                        objParams[1] = new SqlParameter("@P_FROM_DATE", frmdt);
                        objParams[2] = new SqlParameter("@P_TODATE", todt);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_OD_PAY_APP_GET_PENDINGLIST_STATUS", objParams);
                    }
                    catch (Exception ex)
                    {

                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GuestHouseController.GetPendListforODApprovalStatusParticular->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                //public DataSet CheckGetAllPassingPath()
                //{
                //    DataSet ds = null;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                //        SqlParameter[] objparams = new SqlParameter[0];
                //        ds = objSQLHelper.ExecuteDataSetSP("PKG_GUEST_CHECK_PASSING_AUTHORITY_PATH", objparams);
                //    }
                //    catch (Exception ex)
                //    {
                //        return ds;
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LeavesController.GetAllPAPath->" + ex.ToString());
                //    }
                //    finally
                //    {
                //        ds.Dispose();
                //    }
                //    return ds;
                //}





                public int UpdateAppAuthorityEntry(GuestHouse ObjGUA, string STATUS)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[5];
                        objparams[0] = new SqlParameter("@P_GUEST_BOOKING_DETAILS_ID", ObjGUA.GUEST_BOOKING_DETAILS_ID);
                        objparams[1] = new SqlParameter("@P_STATUS", STATUS);
                        objparams[2] = new SqlParameter("@P_REMARKS", ObjGUA.Remark);
                        objparams[3] = new SqlParameter("@P_UANO", ObjGUA.UANO);
                        objparams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objparams[4].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_GUEST_UPDATE_APPROVAL_AUTHORITY_DETAILS", objparams, true);
                        if (Convert.ToInt32(ret) == -99)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                        else
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GuestHouseController.UpdateAppAuthorityEntry->" + ex.ToString());
                    }
                    return retStatus;
                }
                #endregion
                #endregion


                #region 
                public DataSet GetGuestHouseDetails()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[0];
                        //objparams[0] = new SqlParameter("@P_UANO", UANO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GUEST_HOUSE_ROOM_MASTER_DETAILS", objparams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GuestHouseController.GetGuestHouseDetails->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
              
                public DataSet CheckRecordExist(string ROOMNAME,int GHID,int GHCAT)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[3];
                        objparams[0] = new SqlParameter("@P_ROOMNAME", ROOMNAME);
                        objparams[1] = new SqlParameter("@P_GHID", GHID);
                        objparams[2] = new SqlParameter("@P_GHCAT", GHCAT);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GUEST_CHECK_RECORD_EXIST", objparams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GuestHouseController.CheckRecordExist->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
                //BELOW CODE TO FETCH BOOKING DETAILS OF APPROVED BOOKING 
                public DataSet ShowBookingD(int BOOKINGID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[1];
                        objparams[0] = new SqlParameter("@P_BOOKINGID", BOOKINGID);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GUEST_GET_ROOM_ALLOTMENT_BOOKING_DETAILS", objparams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.GuestHouseController.ShowBookingD->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetApprovedData()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[0];
                       // objparams[0] = new SqlParameter("@P_BOOKINGID", BOOKINGID);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GUEST_GET_APPROVED_DATA_BOOKING", objparams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.GuestHouseController.GetApprovedData->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet FillBookingPName()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GUEST_GET_BOOKING_PERSON_NAME_ROOMALLOTMENT", objparams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.GuestHouseController.ShowBookingD->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
                #endregion



                #region Room Allotment Page
                public long AddUpdateRoomAllotment(GuestHouse objMR)
                {
                    long retstatus = 0;
                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_GUEST_ROOM_NAME_ID", objMR.GUEST_ROOM_NAME_ID);
                        objParams[1] = new SqlParameter("@P_GUEST_CATEGORY_ID", objMR.GUEST_CATEGORY_ID);
                        objParams[2] = new SqlParameter("@P_GUEST_BOOKING_DETAILS_ID", objMR.GUEST_BOOKING_DETAILS_ID);
                        objParams[3] = new SqlParameter("@P_UANO", objMR.UANO);                        
                        objParams[4] = new SqlParameter("@P_ROOM_ALLOTMENT", objMR.ROOM_ALLOTMENT);                       
                        objParams[5] = new SqlParameter("@P_GUEST_ROOM_NAME", objMR.GUEST_ROOM_NAME);
                        objParams[6] = new SqlParameter("@P_RA_REMARK", objMR.RA_REMARK);

                        objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[7].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_GUEST_HOUSE_ROOM_ALLOTMENT_DETAILS", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GuestHouseController.AddUpdateRoomAllotment->" + ex.ToString());
                    }
                    return retstatus;
                }

                public DataSet ShowGuestHouseRoom(int CATEGORYID, int GUEST_BOOKING_DETAILS_ID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[2];
                        objparams[0] = new SqlParameter("@P_CATEGORYID", CATEGORYID);
                        objparams[1] = new SqlParameter("@P_GUEST_BOOKING_DETAILS_ID", GUEST_BOOKING_DETAILS_ID); 
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GUEST_GET_GUEST_HOUSE_ROOM_LIST", objparams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.GuestHouseController.GetBookingDetails->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet ShowRAllotBookingList()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[0];                       
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GUEST_GET_ALLOTED_BOOKED_ROOM_LIST", objparams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.GuestHouseController.ShowRAllotBookingList->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetRAllotDetails(int RAID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[0];
                        objparams[0] = new SqlParameter("@P_RAID", RAID);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GUEST_GET_UPDATE_ALLOTED_BOOKED_ROOM_LIST", objparams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.GuestHouseController.ShowRAllotBookingList->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetAllotedRoomList(int BOOKINGID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[1];
                        objparams[0] = new SqlParameter("@P_BOOKINGID", BOOKINGID);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GUEST_GET_ALLOTED_ROOM_LIST", objparams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.GuestHouseController.GetAllotedRoomList->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                #endregion


                #region THIS IS USED TO FETCH BOOKING DETAILS ON BOOKING PAGE
                public DataSet GetBookingDetail(int BOOKINGID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_BOOKINGID", BOOKINGID);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GUEST_HOUSE_GETBOOKING_DETAILS", objParams);
                    }
                    catch (Exception ex)
                    {

                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GuestHouseController.GetBookingDetail->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet ValidateDateTimeBooking(string ARRIVAL_DATE, string ARRIVAL_TIME, int GUEST_CATEGORY, int NO_OF_ROOM, string DEPARTURE_DATE, string DEPARTMENT_TIME, int BookingId)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[7];
                        objparams[0] = new SqlParameter("@P_ARRIVALDATE", ARRIVAL_DATE);
                        objparams[1] = new SqlParameter("@P_ARRIVALTIME", ARRIVAL_TIME);
                        objparams[2] = new SqlParameter("@P_GUEST_CATEGORY_ID", GUEST_CATEGORY);
                        objparams[3] = new SqlParameter("@P_NO_ROOM", NO_OF_ROOM);
                        objparams[4] = new SqlParameter("@P_DEPARTURE_DATE", DEPARTURE_DATE);
                        objparams[5] = new SqlParameter("@P_DEPARTURE_TIME", DEPARTMENT_TIME);
                        objparams[6] = new SqlParameter("@P_BOOKING_DETAILS_ID", BookingId);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GUESTS_VALIDATE_ARRIVAL_DATE_TIME", objparams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.GuestHouseController.ValidateDateTimeBooking->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                //public long ValidateDateTimeBooking(GuestHouse Objmr)
                //{
                //    long retstatus = 0;
                //    try
                //    {

                //        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                //        SqlParameter[] objParams = null;

                //        objParams = new SqlParameter[7];
                //        objParams[0] = new SqlParameter("@P_ARRIVALDATE", Objmr.ARRIVALDATE);
                //        objParams[1] = new SqlParameter("@P_ARRIVALTIME", Objmr.ARRIVALTIME);
                //        objParams[2] = new SqlParameter("@P_GUEST_CATEGORY_ID", Objmr.GUEST_CATEGORY_ID);
                //        objParams[3] = new SqlParameter("@P_NO_ROOM", Objmr.NO_OF_ROOM);
                //        objParams[4] = new SqlParameter("@P_DEPARTURE_DATE", Objmr.DEPARTUREDATE);
                //        objParams[5] = new SqlParameter("@P_DEPARTURE_TIME", Objmr.DEPARTMENTTIME);

                //        objParams[6] = new SqlParameter("@P_OUT", SqlDbType.Int);
                //        objParams[6].Direction = ParameterDirection.Output;

                //        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_GUESTS_VALIDATE_ARRIVAL_DATE_TIME", objParams, true);
                //        if (Convert.ToInt32(ret) == 1)
                //            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                //        else
                //            retstatus = Convert.ToInt32(ret);
                //    }
                //    catch (Exception ex)
                //    {
                //        retstatus = Convert.ToInt32(CustomStatus.Error);
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GuestHouseController.ValidateDateTimeBooking->" + ex.ToString());
                //    }
                //    return retstatus;
                //}



                //public DataSet ValidateDateTimeBooking(string ARRIVAL_DATE, string ARRIVAL_TIME, int GUEST_CATEGORY, int NO_OF_ROOM, string DEPARTURE_DATE, string DEPARTMENT_TIME)
                //{
                //    DataSet ds = null;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                //        SqlParameter[] objparams = null;
                //        objparams = new SqlParameter[6];
                //        objparams[0] = new SqlParameter("@P_ARRIVALDATE", ARRIVAL_DATE);
                //        objparams[1] = new SqlParameter("@P_ARRIVALTIME", ARRIVAL_TIME);
                //        objparams[2] = new SqlParameter("@P_GUEST_CATEGORY_ID", GUEST_CATEGORY);
                //        objparams[3] = new SqlParameter("@P_NO_ROOM", NO_OF_ROOM);
                //        objparams[4] = new SqlParameter("@P_DEPARTURE_DATE", DEPARTURE_DATE);
                //        objparams[5] = new SqlParameter("@P_DEPARTURE_TIME", DEPARTMENT_TIME);
                //        ds = objSQLHelper.ExecuteDataSetSP("PKG_GUEST_VALIDATE_ARRIVALDATE_TIME_akshay", objparams);

                //    }
                //    catch (Exception ex)
                //    {
                //        return ds;
                //        throw new IITMSException("IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.GuestHouseController.GetBookingDetails->" + ex.ToString());
                //    }
                //    finally
                //    {
                //        ds.Dispose();
                //    }
                //    return ds;
                //}
                #endregion




                #region Guest House Name

                //To Add And Update Guest House name
                public long AddUpdateGuestHouseName(GuestHouse objMR)
                {
                    long retstatus = 0;
                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_GHID", objMR.GHID);
                        objParams[1] = new SqlParameter("@P_GH_NAME", objMR.GHNAME);
                        objParams[2] = new SqlParameter("@P_UANO", objMR.UANO);
                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_GUEST_HOUSE_NAME_INSERT_UPDATE", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                        else if (Convert.ToInt32(ret) == 2627)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordExist);
                        }
                        else
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GuestHouseController.AddUpdateGuestHouseName->" + ex.ToString());
                    }
                    return retstatus;
                }

                #endregion



                #region Direct booking
                //To Add And Update   Direct booking
                public long AddUpdateDirectBookingDetails(GuestHouse Objmr)
                {
                    long retstatus = 0;
                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[17];
                        objParams[0] = new SqlParameter("@P_GUEST_BOOKING_DETAILS_ID", Objmr.GUEST_BOOKING_DETAILS_ID);
                        objParams[1] = new SqlParameter("@P_VISITOR_NAME ", Objmr.VISITOR_NAME);
                        objParams[2] = new SqlParameter("@P_ARRIVAL_DATE", Objmr.ARRIVAL_DATE);
                        objParams[3] = new SqlParameter("@P_DEPARTURE_DATE ", Objmr.DEPARTURE_DATE);
                        objParams[4] = new SqlParameter("@P_GUEST_CATEGORY", Objmr.GUEST_CATEGORY);
                        objParams[5] = new SqlParameter("@P_JUSTIFICATION", Objmr.JUSTIFICATION);
                        objParams[6] = new SqlParameter("@P_NO_OF_ROOM ", Objmr.NO_OF_ROOM);
                        objParams[7] = new SqlParameter("@P_VISIT_PURPOSE", Objmr.VISIT_PURPOSE);

                        objParams[8] = new SqlParameter("@P_ADVANCE_PAYMENT ", Objmr.ADVANCE_PAYMENT);
                        objParams[9] = new SqlParameter("@P_BOOKING_PERSON_ID ", Objmr.BOOKING_PERSON_ID);
                        // objParams[10] = new SqlParameter("@P_DESIGNATION ", Objmr.DESIGNATION);
                        objParams[10] = new SqlParameter("@P_MOBILE_NO ", Objmr.MOBILE_NO);
                        objParams[11] = new SqlParameter("@P_ARRIVAL_TIME", Objmr.ARRIVAL_TIME);
                        objParams[12] = new SqlParameter("@P_DEPARTMENT_TIME ", Objmr.DEPARTMENT_TIME);
                        objParams[13] = new SqlParameter("@P_POSTAL_ADDRESS ", Objmr.POSTAL_ADDRESS);
                        objParams[14] = new SqlParameter("@P_NO_OF_VISITORS ", Objmr.NO_OF_VISITORS);
                        if (Objmr.ADVANCE_PAYMENT_DATE == DateTime.MinValue)
                        {
                            objParams[15] = new SqlParameter("@P_ADVANCE_PAYMENT_DATE", DBNull.Value);
                        }
                        else
                        {
                            objParams[15] = new SqlParameter("@P_ADVANCE_PAYMENT_DATE", Objmr.ADVANCE_PAYMENT_DATE);
                        }
                        

                        objParams[16] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[16].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_GUEST_ADD_UPD_DIRECT_BOOKING_DETAILS", objParams, true);
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
                #endregion

                public DataSet GetGuestAllotmentDetail()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[0];
                        //objparams[0] = new SqlParameter("@P_VISITOR_NAME", Name);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_GUEST_ALLOTMENTDETAILS", objparams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.GuestHouseController.GetGuestDetailByName->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public long UpdateGuestPaymentDetail(GuestHouse objMR)
                {
                    long retstatus = 0;
                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_GUEST_BOOKING_DETAILS_ID", objMR.GUEST_BOOKING_DETAILS_ID);
                        //objParams[1] = new SqlParameter("@P_REMAINING_PAYMENT", objMR.REMAININGPAYMENT);
                        objParams[1] = new SqlParameter("@P_TOTAL_PAYMENT", objMR.TOTALPAYMENT);
                        objParams[2] = new SqlParameter("@P_TOTAL_DAYS", objMR.TOTALDAYS);
                        objParams[3] = new SqlParameter("@P_CHARGES", objMR.CHARGE);
                        objParams[4] = new SqlParameter("@P_TAX", objMR.TAX);
                        objParams[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[5].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_GUEST_UPDATE_PAYMENT_DETAILS", objParams, true);
                        if (Convert.ToInt32(ret) == 1)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MR_Controller.AddUpdate_MR_Bill_Details->" + ex.ToString());
                    }
                    return retstatus;
                }

                //BELOW CODE TO FETCH BOOKING DETAILS OF APPROVED BOOKING 
                public DataSet ShowGuestBookingD(int BOOKINGID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[1];
                        objparams[0] = new SqlParameter("@P_BOOKINGID", BOOKINGID);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GUEST_GET_BOOKING_DETAILS", objparams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.GuestHouseController.ShowBookingD->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }


                // Added by juned
                public DataSet GetMailInformation(int bokingid)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@GUEST_BOOKING_DETAILS_ID", bokingid);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GUEST_ALLOTMENT_SEND_MAIL_INFORMATION", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LeavesController.GetSMSInformation->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }




                // Added By Shaikh Juned 23-08-2022

                public DataSet GetApprovalDetail()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GUEST_BOOKING_APPROVAL_STATUS_DETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LeavesController.GetSMSInformation->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
            }
        }
    }
}
