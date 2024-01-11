using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using Microsoft.Win32;
using Microsoft.WindowsAzure.Storage.Blob;
//using Microsoft.WindowsAzure.StorageClient;
using Microsoft.WindowsAzure.Storage.Auth;

using Microsoft.WindowsAzure.Storage;

using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.IO.MemoryMappedFiles;
using System.Threading.Tasks;
using System.Configuration;
using ICSharpCode.SharpZipLib.Zip;
using System.Drawing.Imaging;
public partial class ACADEMIC_EXAMINATION_StudExamTransDetails : System.Web.UI.Page
{

    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    Exam ObjE = new Exam();
    ExamController objExamController = new ExamController();
    int degreeno = 0;
    int branchno = 0;
    int semesterno = 0;
    int collegeid = 0;

    string blob_ConStr = System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"].ToString();
    string blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerNameEXAM"].ToString();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                //Check Session
                if (Session["userno"] == null || Session["username"] == null ||
                    Session["usertype"] == null || Session["userfullname"] == null || Session["OrgId"] == null)
                {
                    Response.Redirect("~/default.aspx");
                }
                else
                {
                    //Page Authorization
                    CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                    }

                    //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_NAME", "SESSIONNO > 0", "SESSIONNO DESC");
                  
                    //if (CheckActivity())
                    //{
                        ViewState["action"] = "add";
                        ViewState["idno"] = "0";
                        if (Session["usertype"].ToString().Equals("2"))     //Student 
                        {
                            if (CheckActivity())
                            {
                                Divpersonaldetails.Visible = true;
                                Transdetails.Visible = true;
                                DivAdminapproval.Visible = false;
                                DivSubmit.Visible = true;
                                BindStudentDetails(Convert.ToInt32(Session["idno"].ToString()));
                                DivNote.Visible = true;
                               
                            }
                            else
                            {
                                Divpersonaldetails.Visible = false;
                                Transdetails.Visible = false;
                                DivAdminapproval.Visible = false;
                                DivSubmit.Visible = false;
                                DivNote.Visible = false;
                                DivRegCourse.Visible = false;
                               // txttransdate.Text = DateTime.Now.ToString();
                               // txttransdate.Enabled = false;
                            }
                        }
                        else   //Admin 
                        {
                            if (Session["IDNONEW"].ToString() != null)
                            {
                                objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND SESSIONNO=" + Convert.ToInt32(Session["Sessionno"].ToString()), "SESSIONNO DESC");
                                ddlSession.SelectedIndex = 1;
                                ddlSession.Focus();
                                Divpersonaldetails.Visible = true;
                                Transdetails.Visible = true;
                                DivAdminapproval.Visible = true;
                                DivSubmit.Visible = true;
                                BindStudentDetails(Convert.ToInt32(Session["IDNONEW"].ToString()));
                                DivNote.Visible = false;
                               // txttransdate.Text = DateTime.Now.ToString();
                               // txttransdate.Enabled = false;
                            }
                            else
                            {
                                Divpersonaldetails.Visible = false;
                                Transdetails.Visible = false;
                                DivAdminapproval.Visible = false;
                                DivSubmit.Visible = false;
                                DivNote.Visible = false;
                                txttransdate.Text = DateTime.Now.ToString();
                                txttransdate.Enabled = false;
                            } 
                        }
                    //}
                    //else
                    //{
                    //    Divpersonaldetails.Visible = false;
                    //    Transdetails.Visible = false;
                    //    DivAdminapproval.Visible = false;
                    //    DivSubmit.Visible = false;
                    //    DivNote.Visible = false;
                    //    DivRegCourse.Visible = false;
                    //}
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_MASTERS_TimeTableSlot.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private bool CheckActivity()
    {
        DataSet dssession = objCommon.FillDropDown("SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO)", "SESSION_NO", "", "STARTED = 1 AND  SHOW_STATUS =1 AND SA.USERTYPE LIKE '%" + Session["usertype"].ToString() + "%' and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%'", "SESSION_NO DESC");

        if (dssession.Tables[0].Rows.Count > 0)
        {
            objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_NAME", "SESSIONNO > 0", "SESSIONNO DESC");
            ddlSession.SelectedValue = dssession.Tables[0].Rows[0]["SESSION_NO"].ToString();
            ddlSession.Enabled = false;
        }
        DataSet DS = null;
        if (Session["usertype"].ToString().Equals("2"))     //Student 
        {
            DS = objCommon.FillDropDown("ACD_STUDENT ", "SEMESTERNO,DEGREENO", "BRANCHNO,COLLEGE_ID", "IDNO=" + Convert.ToInt32(Session["idno"].ToString()), "");
        }
        else
        {

            if (Session["IDNONEW"] == null)
            {
                Response.Redirect("~/notauthorized.aspx?page=StudExamTransDetails.aspx");
            }
            else
            {
                DS = objCommon.FillDropDown("ACD_STUDENT ", "SEMESTERNO,DEGREENO", "BRANCHNO,COLLEGE_ID", "IDNO=" + Convert.ToInt32(Session["IDNONEW"].ToString()), "");

            }          
        }
        if (DS != null && DS.Tables.Count > 0)
        {
            degreeno = Convert.ToInt32(DS.Tables[0].Rows[0]["DEGREENO"].ToString());
            branchno = Convert.ToInt32(DS.Tables[0].Rows[0]["BRANCHNO"].ToString());
            semesterno = Convert.ToInt32(DS.Tables[0].Rows[0]["SEMESTERNO"].ToString());
            collegeid = Convert.ToInt32(DS.Tables[0].Rows[0]["COLLEGE_ID"].ToString());
        }
        bool ret = true;
        ActivityController objActController = new ActivityController();
        DataTableReader dtr = objActController.CheckActivity(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Request.QueryString["pageno"].ToString()), Convert.ToString(degreeno), Convert.ToString(branchno), Convert.ToString(semesterno));

        if (dtr.Read())
        {
            if (dtr["STARTED"].ToString().ToLower().Equals("false"))
            {
                objCommon.DisplayMessage(this, "Either this Activity has been Stopped Or You are Not Authorized to View this Page. Contact Admin.", this.Page);
                ret = false;
            }

            //if (dtr["PRE_REQ_ACT"] == DBNull.Value || dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
            if (dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
            {
                objCommon.DisplayMessage(this, "Pre-Requisite Activity for this Page is Not Stopped!!", this.Page);
                ret = false;
            }
        }
        else
        {
            objCommon.DisplayMessage(this, "Either this Activity has been Stopped Or You are Not Authorized to View this Page. Contact Admin.", this.Page);
            ret = false;
        }
        dtr.Close();
        return ret;
        //}
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=StudExamTransDetails.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=StudExamTransDetails.aspx");
        }
    }

    //private void BindStudentDetails(int idno)
    //{
    //    try
    //    {
    //        SqlDataReader dr = null;

    //        dr = objExamController.GetStudDetailsForExamTransaction(idno, Convert.ToInt32(Session["OrgId"]));
    //        if (dr != null)
    //        {
    //            if (dr.Read())
    //            {
    //                Session["IDNONEW"] = null;
    //                lblstudname.Text = dr["STUDNAME"] == null ? string.Empty : dr["STUDNAME"].ToString();
    //                hfidno.Value = dr["IDNO"] == null ? string.Empty : dr["IDNO"].ToString();
    //                lblenrollmentnos.Text = dr["ENROLLNO"] == null ? string.Empty : dr["ENROLLNO"].ToString();
    //                lblenrollmentnos.ToolTip = dr["ENROLLNO"] == null ? string.Empty : dr["ENROLLNO"].ToString();
    //                lblregistrationnos.Text = dr["REGNO"] == null ? string.Empty : dr["REGNO"].ToString();
    //                lblregistrationnos.ToolTip = dr["REGNO"] == null ? string.Empty : dr["REGNO"].ToString();
    //                lbldegrees.Text = dr["DEGREENAME"] == null ? string.Empty : dr["DEGREENAME"].ToString();
    //                lbldegrees.ToolTip = dr["DEGREENO"] == null ? string.Empty : dr["DEGREENO"].ToString();
    //                lblbranchs.Text = dr["LONGNAME"] == null ? string.Empty : dr["LONGNAME"].ToString();
    //                lblbranchs.ToolTip = dr["BRANCHNO"] == null ? string.Empty : dr["BRANCHNO"].ToString();
    //                hfbranchnos.Value = dr["BRANCHNO"] == null ? string.Empty : dr["BRANCHNO"].ToString();
    //                lblsemester.Text = dr["SEMESTERNAME"] == null ? string.Empty : dr["SEMESTERNAME"].ToString();
    //                lblsemester.ToolTip = dr["SEMESTERNO"] == null ? string.Empty : dr["SEMESTERNO"].ToString();
    //                hfsemno.Value = dr["SEMESTERNO"] == null ? string.Empty : dr["SEMESTERNO"].ToString();
    //                lblMobile.Text = dr["STUDENTMOBILE"] == null ? string.Empty : dr["STUDENTMOBILE"].ToString();
    //                lblemail.Text = dr["EMAILID"] == null ? string.Empty : dr["EMAILID"].ToString();

    //                txttranid.Text = dr["TRANSACTION_NO"] == null ? string.Empty : dr["TRANSACTION_NO"].ToString();
    //                txttransamount.Text = dr["TRANSACTION_AMT"] == null ? string.Empty : dr["TRANSACTION_AMT"].ToString().TrimEnd('.');
    //                txttransdate.Text = dr["TRANS_DATE"] == null ? string.Empty : dr["TRANS_DATE"].ToString();
    //                ddlstatus.SelectedValue = dr["APPROVAL_STATUS"].ToString();
    //                txtremark.Text = dr["REMARK"].ToString();
    //                if (dr["DOC_PATH"].ToString() == "Blob Storage")
    //                {
    //                    DivDocument.Visible = true;
    //                    lnkTransDoc.ToolTip = dr["DOC_NAME"] == null ? string.Empty : dr["DOC_NAME"].ToString();
    //                    btnDownloadFile.Text = dr["DOC_NAME"] == null ? string.Empty : dr["DOC_NAME"].ToString();
    //                    btnDownloadFile.ToolTip = dr["DOC_NAME"] == null ? string.Empty : dr["DOC_NAME"].ToString();
    //                }
    //                else
    //                {
    //                    DivDocument.Visible = false;
    //                }


    //                if (Session["usertype"].ToString().Equals("2"))     //Student 
    //                {
    //                    if (dr["APPROVAL_STATUS"].ToString() == "1")
    //                    {
    //                        objCommon.DisplayMessage(this, "Your Exam Transaction Details Approved by admin.", this.Page);
    //                        txttranid.Enabled = false;
    //                        txttransamount.Enabled = false;
    //                        txttransdate.Enabled = false;
    //                        fuUpload.Enabled = false;
    //                        btnSave.Enabled = false;
    //                        DivAdminapproval.Visible = true;
    //                        txtremark.Enabled = false;
    //                        ddlstatus.Enabled = false;
    //                        Divstatus.Visible = true;
    //                    }
    //                    else if (dr["APPROVAL_STATUS"].ToString() == "2")
    //                    {
    //                        //objCommon.DisplayMessage(this, "Your Exam Transaction Details Rejected by admin. Please make correction.", this.Page);
    //                        objCommon.DisplayMessage(this, "You need to make correction in Exam Transaction Details and resumbit the details.", this.Page);
    //                        txttranid.Enabled = true;
    //                        txttransamount.Enabled = true;
    //                        txttransdate.Enabled = true;
    //                        fuUpload.Enabled = true;
    //                        btnSave.Enabled = true;
    //                        DivAdminapproval.Visible = true;
    //                        txtremark.Enabled = false;
    //                        ddlstatus.Enabled = false;
    //                        ddlstatus.Visible = false;
    //                        Divstatus.Visible = false;
    //                    }
    //                    else
    //                    {
    //                        txttranid.Enabled = true;
    //                        txttransamount.Enabled = true;
    //                        txttransdate.Enabled = true;
    //                        fuUpload.Enabled = true;
    //                        btnSave.Enabled = true;
    //                        DivAdminapproval.Visible = false;
    //                    }
    //                }
    //                else
    //                {
    //                    if (dr["APPROVAL_STATUS"].ToString() == "1")
    //                    {
    //                        txttranid.Enabled = false;
    //                        txttransamount.Enabled = false;
    //                        txttransdate.Enabled = false;
    //                        fuUpload.Enabled = false;
    //                        ddlstatus.Enabled = false;
    //                        txtremark.Enabled = false;
    //                        btnSave.Enabled = false;
    //                        ddlstatus.SelectedValue = dr["APPROVAL_STATUS"].ToString();
    //                        txtremark.Text = dr["REMARK"].ToString();
    //                    }
    //                    else
    //                    {
    //                        txttranid.Enabled = false;
    //                        txttransamount.Enabled = false;
    //                        txttransdate.Enabled = false;
    //                        fuUpload.Enabled = false;
    //                        ddlstatus.Enabled = true;
    //                        txtremark.Enabled = true;
    //                        btnSave.Enabled = true;
    //                        ddlstatus.SelectedValue = dr["APPROVAL_STATUS"].ToString();
    //                        txtremark.Text = dr["REMARK"].ToString();
    //                    }
    //                }
    //            }
    //        }
    //        if (dr != null)
    //            dr.Close();
    //    }
    //    catch (Exception ex)
    //    {
    //    }
    //}

    //private void BindStudentdetailsaftersubmit(int idno)
    //{
    //    try
    //    {
    //        SqlDataReader dr = null;

    //        dr = objExamController.GetStudDetailsForExamTransaction(idno, Convert.ToInt32(Session["OrgId"]));
    //        if (dr != null)
    //        {
    //            if (dr.Read())
    //            {
    //                Session["IDNONEW"] = null;
    //                lblstudname.Text = dr["STUDNAME"] == null ? string.Empty : dr["STUDNAME"].ToString();
    //                hfidno.Value = dr["IDNO"] == null ? string.Empty : dr["IDNO"].ToString();
    //                lblenrollmentnos.Text = dr["ENROLLNO"] == null ? string.Empty : dr["ENROLLNO"].ToString();
    //                lblenrollmentnos.ToolTip = dr["ENROLLNO"] == null ? string.Empty : dr["ENROLLNO"].ToString();
    //                lblregistrationnos.Text = dr["REGNO"] == null ? string.Empty : dr["REGNO"].ToString();
    //                lblregistrationnos.ToolTip = dr["REGNO"] == null ? string.Empty : dr["REGNO"].ToString();
    //                lbldegrees.Text = dr["DEGREENAME"] == null ? string.Empty : dr["DEGREENAME"].ToString();
    //                lbldegrees.ToolTip = dr["DEGREENO"] == null ? string.Empty : dr["DEGREENO"].ToString();
    //                lblbranchs.Text = dr["LONGNAME"] == null ? string.Empty : dr["LONGNAME"].ToString();
    //                lblbranchs.ToolTip = dr["BRANCHNO"] == null ? string.Empty : dr["BRANCHNO"].ToString();
    //                hfbranchnos.Value = dr["BRANCHNO"] == null ? string.Empty : dr["BRANCHNO"].ToString();
    //                lblsemester.Text = dr["SEMESTERNAME"] == null ? string.Empty : dr["SEMESTERNAME"].ToString();
    //                lblsemester.ToolTip = dr["SEMESTERNO"] == null ? string.Empty : dr["SEMESTERNO"].ToString();
    //                hfsemno.Value = dr["SEMESTERNO"] == null ? string.Empty : dr["SEMESTERNO"].ToString();
    //                lblMobile.Text = dr["STUDENTMOBILE"] == null ? string.Empty : dr["STUDENTMOBILE"].ToString();
    //                lblemail.Text = dr["EMAILID"] == null ? string.Empty : dr["EMAILID"].ToString();

    //                txttranid.Text = dr["TRANSACTION_NO"] == null ? string.Empty : dr["TRANSACTION_NO"].ToString();
    //                txttransamount.Text = dr["TRANSACTION_AMT"] == null ? string.Empty : dr["TRANSACTION_AMT"].ToString().TrimEnd('.');
    //                txttransdate.Text = dr["TRANS_DATE"] == null ? string.Empty : dr["TRANS_DATE"].ToString();
    //                ddlstatus.SelectedValue = dr["APPROVAL_STATUS"].ToString();
    //                txtremark.Text = dr["REMARK"].ToString();
    //                if (dr["DOC_PATH"].ToString() == "Blob Storage")
    //                {
    //                    DivDocument.Visible = true;
    //                    lnkTransDoc.ToolTip = dr["DOC_NAME"] == null ? string.Empty : dr["DOC_NAME"].ToString();
    //                    btnDownloadFile.Text = dr["DOC_NAME"] == null ? string.Empty : dr["DOC_NAME"].ToString();
    //                    btnDownloadFile.ToolTip = dr["DOC_NAME"] == null ? string.Empty : dr["DOC_NAME"].ToString();
    //                }
    //                else
    //                {
    //                    DivDocument.Visible = false;
    //                }


    //                if (Session["usertype"].ToString().Equals("2"))     //Student 
    //                {
    //                    if (dr["APPROVAL_STATUS"].ToString() == "1")
    //                    {
    //                        //objCommon.DisplayMessage(this, "Your Exam Transaction Details Approved by admin.", this.Page);
    //                        txttranid.Enabled = false;
    //                        txttransamount.Enabled = false;
    //                        txttransdate.Enabled = false;
    //                        fuUpload.Enabled = false;
    //                        btnSave.Enabled = false;
    //                        DivAdminapproval.Visible = true;
    //                        txtremark.Enabled = false;
    //                        ddlstatus.Enabled = false;
    //                        Divstatus.Visible = true;
    //                    }
    //                    else if (dr["APPROVAL_STATUS"].ToString() == "2")
    //                    {
    //                       // objCommon.DisplayMessage(this, "Your Exam Transaction Details Rejected by admin. Please make correction.", this.Page);
    //                        txttranid.Enabled = true;
    //                        txttransamount.Enabled = true;
    //                        txttransdate.Enabled = true;
    //                        fuUpload.Enabled = true;
    //                        btnSave.Enabled = true;
    //                        DivAdminapproval.Visible = true;
    //                        txtremark.Enabled = false;
    //                        ddlstatus.Enabled = false;
    //                        ddlstatus.Visible = false;
    //                        Divstatus.Visible = false;
    //                    }
    //                    else
    //                    {
    //                        txttranid.Enabled = true;
    //                        txttransamount.Enabled = true;
    //                        txttransdate.Enabled = true;
    //                        fuUpload.Enabled = true;
    //                        btnSave.Enabled = true;
    //                        DivAdminapproval.Visible = false;
    //                    }
    //                }
    //                else
    //                {
    //                    if (dr["APPROVAL_STATUS"].ToString() == "1")
    //                    {
    //                        txttranid.Enabled = false;
    //                        txttransamount.Enabled = false;
    //                        txttransdate.Enabled = false;
    //                        fuUpload.Enabled = false;
    //                        ddlstatus.Enabled = false;
    //                        txtremark.Enabled = false;
    //                        btnSave.Enabled = false;
    //                        ddlstatus.SelectedValue = dr["APPROVAL_STATUS"].ToString();
    //                        txtremark.Text = dr["REMARK"].ToString();
    //                    }
    //                    else
    //                    {
    //                        txttranid.Enabled = false;
    //                        txttransamount.Enabled = false;
    //                        txttransdate.Enabled = false;
    //                        fuUpload.Enabled = false;
    //                        ddlstatus.Enabled = true;
    //                        txtremark.Enabled = true;
    //                        btnSave.Enabled = true;
    //                        ddlstatus.SelectedValue = dr["APPROVAL_STATUS"].ToString();
    //                        txtremark.Text = dr["REMARK"].ToString();
    //                    }
    //                }
    //            }
    //        }
    //        if (dr != null)
    //            dr.Close();
    //    }
    //    catch (Exception ex)
    //    {
    //    }
    //}



    //private void BindStudentDetails(int idno)
    //{
    //    try
    //    {
    //        SqlDataReader dr = null;

    //        dr = objExamController.GetStudDetailsForExamTransaction(idno, Convert.ToInt32(Session["OrgId"]));
    //        if (dr != null)
    //        {
    //            if (dr.Read())
    //            {
    //                Session["IDNONEW"] = null;
    //                lblstudname.Text = dr["STUDNAME"] == null ? string.Empty : dr["STUDNAME"].ToString();
    //                hfidno.Value = dr["IDNO"] == null ? string.Empty : dr["IDNO"].ToString();
    //                lblenrollmentnos.Text = dr["ENROLLNO"] == null ? string.Empty : dr["ENROLLNO"].ToString();
    //                lblenrollmentnos.ToolTip = dr["ENROLLNO"] == null ? string.Empty : dr["ENROLLNO"].ToString();
    //                lblregistrationnos.Text = dr["REGNO"] == null ? string.Empty : dr["REGNO"].ToString();
    //                lblregistrationnos.ToolTip = dr["REGNO"] == null ? string.Empty : dr["REGNO"].ToString();
    //                lbldegrees.Text = dr["DEGREENAME"] == null ? string.Empty : dr["DEGREENAME"].ToString();
    //                lbldegrees.ToolTip = dr["DEGREENO"] == null ? string.Empty : dr["DEGREENO"].ToString();
    //                lblbranchs.Text = dr["LONGNAME"] == null ? string.Empty : dr["LONGNAME"].ToString();
    //                lblbranchs.ToolTip = dr["BRANCHNO"] == null ? string.Empty : dr["BRANCHNO"].ToString();
    //                hfbranchnos.Value = dr["BRANCHNO"] == null ? string.Empty : dr["BRANCHNO"].ToString();
    //                lblsemester.Text = dr["SEMESTERNAME"] == null ? string.Empty : dr["SEMESTERNAME"].ToString();
    //                lblsemester.ToolTip = dr["SEMESTERNO"] == null ? string.Empty : dr["SEMESTERNO"].ToString();
    //                hfsemno.Value = dr["SEMESTERNO"] == null ? string.Empty : dr["SEMESTERNO"].ToString();
    //                lblMobile.Text = dr["STUDENTMOBILE"] == null ? string.Empty : dr["STUDENTMOBILE"].ToString();
    //                lblemail.Text = dr["EMAILID"] == null ? string.Empty : dr["EMAILID"].ToString();

    //                txttranid.Text = dr["TRANSACTION_NO"] == null ? string.Empty : dr["TRANSACTION_NO"].ToString();
    //                txttransamount.Text = dr["TRANSACTION_AMT"] == null ? string.Empty : dr["TRANSACTION_AMT"].ToString().TrimEnd('.');
    //                txttransdate.Text = dr["TRANS_DATE"] == null ? string.Empty : dr["TRANS_DATE"].ToString();
    //                ddlstatus.SelectedValue = dr["APPROVAL_STATUS"].ToString();
    //                txtremark.Text = dr["REMARK"].ToString();
    //                if (dr["DOC_PATH"].ToString() == "Blob Storage")
    //                {
    //                    DivDocument.Visible = true;
    //                    lnkTransDoc.ToolTip = dr["DOC_NAME"] == null ? string.Empty : dr["DOC_NAME"].ToString();
    //                    btnDownloadFile.Text = dr["DOC_NAME"] == null ? string.Empty : dr["DOC_NAME"].ToString();
    //                    btnDownloadFile.ToolTip = dr["DOC_NAME"] == null ? string.Empty : dr["DOC_NAME"].ToString();
    //                }
    //                else
    //                {
    //                    DivDocument.Visible = false;
    //                }


    //                if (Session["usertype"].ToString().Equals("2"))     //Student 
    //                {
    //                    if (dr["APPROVAL_STATUS"].ToString() == "1")
    //                    {
    //                        objCommon.DisplayMessage(this, "Your Exam Transaction Details Approved by admin.", this.Page);
    //                        txttranid.Enabled = false;
    //                        txttransamount.Enabled = false;
    //                        txttransdate.Enabled = false;
    //                        fuUpload.Enabled = false;
    //                        btnSave.Enabled = false;
    //                        btnSave.Visible = false;
    //                        btnCancel.Visible = false;
    //                        DivAdminapproval.Visible = true;
    //                        txtremark.Enabled = false;
    //                        ddlstatus.Enabled = false;
    //                        Divstatus.Visible = true;
    //                    }
    //                    else if (dr["APPROVAL_STATUS"].ToString() == "2")
    //                    {
    //                        //objCommon.DisplayMessage(this, "Your Exam Transaction Details Rejected by admin. Please make correction.", this.Page);
    //                        objCommon.DisplayMessage(this, "You need to make correction in Exam Transaction Details and resumbit the details.", this.Page);
    //                        txttranid.Enabled = true;
    //                        txttransamount.Enabled = true;
    //                        txttransdate.Enabled = true;
    //                        fuUpload.Enabled = true;
    //                        btnSave.Enabled = true;
    //                        btnSave.Visible = true;
    //                        btnCancel.Visible = true;
    //                        DivAdminapproval.Visible = true;
    //                        txtremark.Enabled = false;
    //                        ddlstatus.Enabled = false;
    //                        ddlstatus.Visible = false;
    //                        Divstatus.Visible = false;
    //                    }
    //                    else
    //                    {
    //                        txttranid.Enabled = true;
    //                        txttransamount.Enabled = true;
    //                        txttransdate.Enabled = true;
    //                        fuUpload.Enabled = true;
    //                        btnSave.Enabled = true;
    //                        btnSave.Visible = true;
    //                        btnCancel.Visible = true;
    //                        DivAdminapproval.Visible = false;
    //                    }
    //                }
    //                else
    //                {
    //                    if (dr["APPROVAL_STATUS"].ToString() == "1")
    //                    {
    //                        txttranid.Enabled = false;
    //                        txttransamount.Enabled = false;
    //                        txttransdate.Enabled = false;
    //                        fuUpload.Enabled = false;
    //                        ddlstatus.Enabled = false;
    //                        txtremark.Enabled = false;
    //                        btnSave.Enabled = false;
    //                        btnSave.Visible = false;
    //                        btnCancel.Visible = false;
    //                        ddlstatus.SelectedValue = dr["APPROVAL_STATUS"].ToString();
    //                        txtremark.Text = dr["REMARK"].ToString();
    //                    }
    //                    else
    //                    {
    //                        txttranid.Enabled = false;
    //                        txttransamount.Enabled = false;
    //                        txttransdate.Enabled = false;
    //                        fuUpload.Enabled = false;
    //                        ddlstatus.Enabled = true;
    //                        txtremark.Enabled = true;
    //                        btnSave.Enabled = true;
    //                        btnSave.Visible = true;
    //                        btnCancel.Visible = true;
    //                        ddlstatus.SelectedValue = dr["APPROVAL_STATUS"].ToString();
    //                        txtremark.Text = dr["REMARK"].ToString();
    //                    }
    //                }
    //            }
    //            BindCourses();
    //        }
    //        if (dr != null)
    //            dr.Close();
    //    }
    //    catch (Exception ex)
    //    {
    //    }
    //}

    //private void BindStudentdetailsaftersubmit(int idno)
    //{
    //    try
    //    {
    //        SqlDataReader dr = null;

    //        dr = objExamController.GetStudDetailsForExamTransaction(idno, Convert.ToInt32(Session["OrgId"]));
    //        if (dr != null)
    //        {
    //            if (dr.Read())
    //            {
    //                Session["IDNONEW"] = null;
    //                lblstudname.Text = dr["STUDNAME"] == null ? string.Empty : dr["STUDNAME"].ToString();
    //                hfidno.Value = dr["IDNO"] == null ? string.Empty : dr["IDNO"].ToString();
    //                lblenrollmentnos.Text = dr["ENROLLNO"] == null ? string.Empty : dr["ENROLLNO"].ToString();
    //                lblenrollmentnos.ToolTip = dr["ENROLLNO"] == null ? string.Empty : dr["ENROLLNO"].ToString();
    //                lblregistrationnos.Text = dr["REGNO"] == null ? string.Empty : dr["REGNO"].ToString();
    //                lblregistrationnos.ToolTip = dr["REGNO"] == null ? string.Empty : dr["REGNO"].ToString();
    //                lbldegrees.Text = dr["DEGREENAME"] == null ? string.Empty : dr["DEGREENAME"].ToString();
    //                lbldegrees.ToolTip = dr["DEGREENO"] == null ? string.Empty : dr["DEGREENO"].ToString();
    //                lblbranchs.Text = dr["LONGNAME"] == null ? string.Empty : dr["LONGNAME"].ToString();
    //                lblbranchs.ToolTip = dr["BRANCHNO"] == null ? string.Empty : dr["BRANCHNO"].ToString();
    //                hfbranchnos.Value = dr["BRANCHNO"] == null ? string.Empty : dr["BRANCHNO"].ToString();
    //                lblsemester.Text = dr["SEMESTERNAME"] == null ? string.Empty : dr["SEMESTERNAME"].ToString();
    //                lblsemester.ToolTip = dr["SEMESTERNO"] == null ? string.Empty : dr["SEMESTERNO"].ToString();
    //                hfsemno.Value = dr["SEMESTERNO"] == null ? string.Empty : dr["SEMESTERNO"].ToString();
    //                lblMobile.Text = dr["STUDENTMOBILE"] == null ? string.Empty : dr["STUDENTMOBILE"].ToString();
    //                lblemail.Text = dr["EMAILID"] == null ? string.Empty : dr["EMAILID"].ToString();

    //                txttranid.Text = dr["TRANSACTION_NO"] == null ? string.Empty : dr["TRANSACTION_NO"].ToString();
    //                txttransamount.Text = dr["TRANSACTION_AMT"] == null ? string.Empty : dr["TRANSACTION_AMT"].ToString().TrimEnd('.');
    //                txttransdate.Text = dr["TRANS_DATE"] == null ? string.Empty : dr["TRANS_DATE"].ToString();
    //                ddlstatus.SelectedValue = dr["APPROVAL_STATUS"].ToString();
    //                txtremark.Text = dr["REMARK"].ToString();
    //                if (dr["DOC_PATH"].ToString() == "Blob Storage")
    //                {
    //                    DivDocument.Visible = true;
    //                    lnkTransDoc.ToolTip = dr["DOC_NAME"] == null ? string.Empty : dr["DOC_NAME"].ToString();
    //                    btnDownloadFile.Text = dr["DOC_NAME"] == null ? string.Empty : dr["DOC_NAME"].ToString();
    //                    btnDownloadFile.ToolTip = dr["DOC_NAME"] == null ? string.Empty : dr["DOC_NAME"].ToString();
    //                }
    //                else
    //                {
    //                    DivDocument.Visible = false;
    //                }


    //                if (Session["usertype"].ToString().Equals("2"))     //Student 
    //                {
    //                    if (dr["APPROVAL_STATUS"].ToString() == "1")
    //                    {
    //                        //objCommon.DisplayMessage(this, "Your Exam Transaction Details Approved by admin.", this.Page);
    //                        txttranid.Enabled = false;
    //                        txttransamount.Enabled = false;
    //                        txttransdate.Enabled = false;
    //                        fuUpload.Enabled = false;
    //                        btnSave.Enabled = false;
    //                        btnSave.Visible = false;
    //                        btnCancel.Visible = false;
    //                        DivAdminapproval.Visible = true;
    //                        txtremark.Enabled = false;
    //                        ddlstatus.Enabled = false;
    //                        Divstatus.Visible = true;
    //                    }
    //                    else if (dr["APPROVAL_STATUS"].ToString() == "2")
    //                    {
    //                        // objCommon.DisplayMessage(this, "Your Exam Transaction Details Rejected by admin. Please make correction.", this.Page);
    //                        txttranid.Enabled = true;
    //                        txttransamount.Enabled = true;
    //                        txttransdate.Enabled = true;
    //                        fuUpload.Enabled = true;
    //                        btnSave.Enabled = true;
    //                        btnSave.Visible = true;
    //                        btnCancel.Visible = true;
    //                        DivAdminapproval.Visible = true;
    //                        txtremark.Enabled = false;
    //                        ddlstatus.Enabled = false;
    //                        ddlstatus.Visible = false;
    //                        Divstatus.Visible = false;
    //                    }
    //                    else
    //                    {
    //                        txttranid.Enabled = true;
    //                        txttransamount.Enabled = true;
    //                        txttransdate.Enabled = true;
    //                        fuUpload.Enabled = true;
    //                        btnSave.Enabled = true;
    //                        btnSave.Visible = true;
    //                        btnCancel.Visible = true;
    //                        DivAdminapproval.Visible = false;
    //                    }
    //                }
    //                else
    //                {
    //                    if (dr["APPROVAL_STATUS"].ToString() == "1")
    //                    {
    //                        txttranid.Enabled = false;
    //                        txttransamount.Enabled = false;
    //                        txttransdate.Enabled = false;
    //                        fuUpload.Enabled = false;
    //                        ddlstatus.Enabled = false;
    //                        txtremark.Enabled = false;
    //                        btnSave.Enabled = false;
    //                        btnSave.Visible = false;
    //                        btnCancel.Visible = false;
    //                        ddlstatus.SelectedValue = dr["APPROVAL_STATUS"].ToString();
    //                        txtremark.Text = dr["REMARK"].ToString();
    //                    }
    //                    else
    //                    {
    //                        txttranid.Enabled = false;
    //                        txttransamount.Enabled = false;
    //                        txttransdate.Enabled = false;
    //                        fuUpload.Enabled = false;
    //                        ddlstatus.Enabled = true;
    //                        txtremark.Enabled = true;
    //                        btnSave.Enabled = true;
    //                        btnSave.Visible = true;
    //                        btnCancel.Visible = true;
    //                        ddlstatus.SelectedValue = dr["APPROVAL_STATUS"].ToString();
    //                        txtremark.Text = dr["REMARK"].ToString();
    //                    }
    //                }
    //            }
    //            BindCourses();
    //        }
    //        if (dr != null)
    //            dr.Close();
    //    }
    //    catch (Exception ex)
    //    {
    //    }
    //}


    private void BindStudentDetails(int idno)
    {
        try
        {
            //SqlDataReader dr = null;
            DataSet ds = objCommon.DynamicSPCall_Select("PKG_ACD_GET_EXAM_TRANSACTION_DETAILS_NEW", "@P_IDNO,@P_ORGID,@P_SESSIONNO", "" + Convert.ToInt32(idno) + "," + Convert.ToInt32(Session["OrgId"]) + "," + Convert.ToInt32(ddlSession.SelectedValue) + "");

            // dr = objExamController.GetStudDetailsForExamTransaction(idno, Convert.ToInt32(Session["OrgId"]));
            //if (dr != null)
            //{
            //    if (dr.Read())
            //    {
            if (ds.Tables[0].Rows.Count > 0)
            {
                Session["IDNONEW"] = null;
                lblstudname.Text = ds.Tables[0].Rows[0]["STUDNAME"] == null ? string.Empty : ds.Tables[0].Rows[0]["STUDNAME"].ToString();
                hfidno.Value = ds.Tables[0].Rows[0]["IDNO"] == null ? string.Empty : ds.Tables[0].Rows[0]["IDNO"].ToString();
                lblenrollmentnos.Text = ds.Tables[0].Rows[0]["ENROLLNO"] == null ? string.Empty : ds.Tables[0].Rows[0]["ENROLLNO"].ToString();
                lblenrollmentnos.ToolTip = ds.Tables[0].Rows[0]["ENROLLNO"] == null ? string.Empty : ds.Tables[0].Rows[0]["ENROLLNO"].ToString();
                lblregistrationnos.Text = ds.Tables[0].Rows[0]["REGNO"] == null ? string.Empty : ds.Tables[0].Rows[0]["REGNO"].ToString();
                lblregistrationnos.ToolTip = ds.Tables[0].Rows[0]["REGNO"] == null ? string.Empty : ds.Tables[0].Rows[0]["REGNO"].ToString();
                lbldegrees.Text = ds.Tables[0].Rows[0]["DEGREENAME"] == null ? string.Empty : ds.Tables[0].Rows[0]["DEGREENAME"].ToString();
                lbldegrees.ToolTip = ds.Tables[0].Rows[0]["DEGREENO"] == null ? string.Empty : ds.Tables[0].Rows[0]["DEGREENO"].ToString();
                lblbranchs.Text = ds.Tables[0].Rows[0]["LONGNAME"] == null ? string.Empty : ds.Tables[0].Rows[0]["LONGNAME"].ToString();
                lblbranchs.ToolTip = ds.Tables[0].Rows[0]["BRANCHNO"] == null ? string.Empty : ds.Tables[0].Rows[0]["BRANCHNO"].ToString();
                hfbranchnos.Value = ds.Tables[0].Rows[0]["BRANCHNO"] == null ? string.Empty : ds.Tables[0].Rows[0]["BRANCHNO"].ToString();
                lblsemester.Text = ds.Tables[0].Rows[0]["SEMESTERNAME"] == null ? string.Empty : ds.Tables[0].Rows[0]["SEMESTERNAME"].ToString();
                lblsemester.ToolTip = ds.Tables[0].Rows[0]["SEMESTERNO"] == null ? string.Empty : ds.Tables[0].Rows[0]["SEMESTERNO"].ToString();
                hfsemno.Value = ds.Tables[0].Rows[0]["SEMESTERNO"] == null ? string.Empty : ds.Tables[0].Rows[0]["SEMESTERNO"].ToString();
                lblMobile.Text = ds.Tables[0].Rows[0]["STUDENTMOBILE"] == null ? string.Empty : ds.Tables[0].Rows[0]["STUDENTMOBILE"].ToString();
                lblemail.Text = ds.Tables[0].Rows[0]["EMAILID"] == null ? string.Empty : ds.Tables[0].Rows[0]["EMAILID"].ToString();

                txttranid.Text = ds.Tables[0].Rows[0]["TRANSACTION_NO"] == null ? string.Empty : ds.Tables[0].Rows[0]["TRANSACTION_NO"].ToString();
                txttransamount.Text = ds.Tables[0].Rows[0]["TRANSACTION_AMT"] == null ? string.Empty : ds.Tables[0].Rows[0]["TRANSACTION_AMT"].ToString().TrimEnd('.');
                txttransdate.Text = ds.Tables[0].Rows[0]["TRANS_DATE"] == null ? string.Empty : ds.Tables[0].Rows[0]["TRANS_DATE"].ToString();
                ddlstatus.SelectedValue = ds.Tables[0].Rows[0]["APPROVAL_STATUS"].ToString();
                txtremark.Text = ds.Tables[0].Rows[0]["REMARK"].ToString();
                if (ds.Tables[0].Rows[0]["DOC_PATH"].ToString() == "Blob Storage")
                {
                    DivDocument.Visible = true;
                    lnkTransDoc.ToolTip = ds.Tables[0].Rows[0]["DOC_NAME"] == null ? string.Empty : ds.Tables[0].Rows[0]["DOC_NAME"].ToString();
                    btnDownloadFile.Text = ds.Tables[0].Rows[0]["DOC_NAME"] == null ? string.Empty : ds.Tables[0].Rows[0]["DOC_NAME"].ToString();
                    btnDownloadFile.ToolTip = ds.Tables[0].Rows[0]["DOC_NAME"] == null ? string.Empty : ds.Tables[0].Rows[0]["DOC_NAME"].ToString();
                }
                else
                {
                    DivDocument.Visible = false;
                }


                if (Session["usertype"].ToString().Equals("2"))     //Student 
                {
                    if (ds.Tables[0].Rows[0]["APPROVAL_STATUS"].ToString() == "1")
                    {
                        objCommon.DisplayMessage(this, "Your Exam Transaction Details Approved by admin.", this.Page);
                        txttranid.Enabled = false;
                        txttransamount.Enabled = false;
                        txttransdate.Enabled = false;
                        fuUpload.Enabled = false;
                        btnSave.Enabled = false;
                        btnSave.Visible = false;
                        btnCancel.Visible = false;
                        DivAdminapproval.Visible = true;
                        txtremark.Enabled = false;
                        ddlstatus.Enabled = false;
                        Divstatus.Visible = true;
                    }
                    else if (ds.Tables[0].Rows[0]["APPROVAL_STATUS"].ToString() == "2")
                    {
                        //objCommon.DisplayMessage(this, "Your Exam Transaction Details Rejected by admin. Please make correction.", this.Page);
                        objCommon.DisplayMessage(this, "You need to make correction in Exam Transaction Details and resumbit the details.", this.Page);
                        txttranid.Enabled = true;
                        txttransamount.Enabled = true;
                        txttransdate.Enabled = true;
                        fuUpload.Enabled = true;
                        btnSave.Enabled = true;
                        btnSave.Visible = true;
                        btnCancel.Visible = true;
                        DivAdminapproval.Visible = true;
                        txtremark.Enabled = false;
                        ddlstatus.Enabled = false;
                        ddlstatus.Visible = false;
                        Divstatus.Visible = false;
                        txttransdate.Text = DateTime.Now.ToShortDateString();
                        txttransdate.Enabled = false;
                    }
                    else
                    {
                        txttranid.Enabled = true;
                        txttransamount.Enabled = true;
                        txttransdate.Enabled = true;
                        fuUpload.Enabled = true;
                        btnSave.Enabled = true;
                        btnSave.Visible = true;
                        btnCancel.Visible = true;
                        DivAdminapproval.Visible = false;
                        txttransdate.Text = DateTime.Now.ToShortDateString();
                        txttransdate.Enabled = false;
                    }
                }
                else
                {
                    if (ds.Tables[0].Rows[0]["APPROVAL_STATUS"].ToString() == "1")
                    {
                        txttranid.Enabled = false;
                        txttransamount.Enabled = false;
                        txttransdate.Enabled = false;
                        fuUpload.Enabled = false;
                        ddlstatus.Enabled = false;
                        txtremark.Enabled = false;
                        btnSave.Enabled = false;
                        btnSave.Visible = false;
                        btnCancel.Visible = false;
                        ddlstatus.SelectedValue = ds.Tables[0].Rows[0]["APPROVAL_STATUS"].ToString();
                        txtremark.Text = ds.Tables[0].Rows[0]["REMARK"].ToString();
                    }
                    else
                    {
                        txttranid.Enabled = false;
                        txttransamount.Enabled = false;
                        txttransdate.Enabled = false;
                        fuUpload.Enabled = false;
                        ddlstatus.Enabled = true;
                        txtremark.Enabled = true;
                        btnSave.Enabled = true;
                        btnSave.Visible = true;
                        btnCancel.Visible = true;
                        ddlstatus.SelectedValue = ds.Tables[0].Rows[0]["APPROVAL_STATUS"].ToString();
                        txtremark.Text = ds.Tables[0].Rows[0]["REMARK"].ToString();
                    }
                }
            }
            // }
            BindCourses();
            //}
            //if (dr != null)
            //    dr.Close();
        }
        catch (Exception ex)
        {
        }
    }

    private void BindStudentdetailsaftersubmit(int idno)
    {
        try
        {
            //SqlDataReader dr = null;

            //dr = objExamController.GetStudDetailsForExamTransaction(idno, Convert.ToInt32(Session["OrgId"]));
            //if (dr != null)
            //{
            //    if (dr.Read())
            //    {
            DataSet ds = objCommon.DynamicSPCall_Select("PKG_ACD_GET_EXAM_TRANSACTION_DETAILS_NEW", "@P_IDNO,@P_ORGID,@P_SESSIONNO", "" + Convert.ToInt32(idno) + "," + Convert.ToInt32(Session["OrgId"]) + "," + Convert.ToInt32(ddlSession.SelectedValue) + "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                Session["IDNONEW"] = null;
                lblstudname.Text = ds.Tables[0].Rows[0]["STUDNAME"] == null ? string.Empty : ds.Tables[0].Rows[0]["STUDNAME"].ToString();
                hfidno.Value = ds.Tables[0].Rows[0]["IDNO"] == null ? string.Empty : ds.Tables[0].Rows[0]["IDNO"].ToString();
                lblenrollmentnos.Text = ds.Tables[0].Rows[0]["ENROLLNO"] == null ? string.Empty : ds.Tables[0].Rows[0]["ENROLLNO"].ToString();
                lblenrollmentnos.ToolTip = ds.Tables[0].Rows[0]["ENROLLNO"] == null ? string.Empty : ds.Tables[0].Rows[0]["ENROLLNO"].ToString();
                lblregistrationnos.Text = ds.Tables[0].Rows[0]["REGNO"] == null ? string.Empty : ds.Tables[0].Rows[0]["REGNO"].ToString();
                lblregistrationnos.ToolTip = ds.Tables[0].Rows[0]["REGNO"] == null ? string.Empty : ds.Tables[0].Rows[0]["REGNO"].ToString();
                lbldegrees.Text = ds.Tables[0].Rows[0]["DEGREENAME"] == null ? string.Empty : ds.Tables[0].Rows[0]["DEGREENAME"].ToString();
                lbldegrees.ToolTip = ds.Tables[0].Rows[0]["DEGREENO"] == null ? string.Empty : ds.Tables[0].Rows[0]["DEGREENO"].ToString();
                lblbranchs.Text = ds.Tables[0].Rows[0]["LONGNAME"] == null ? string.Empty : ds.Tables[0].Rows[0]["LONGNAME"].ToString();
                lblbranchs.ToolTip = ds.Tables[0].Rows[0]["BRANCHNO"] == null ? string.Empty : ds.Tables[0].Rows[0]["BRANCHNO"].ToString();
                hfbranchnos.Value = ds.Tables[0].Rows[0]["BRANCHNO"] == null ? string.Empty : ds.Tables[0].Rows[0]["BRANCHNO"].ToString();
                lblsemester.Text = ds.Tables[0].Rows[0]["SEMESTERNAME"] == null ? string.Empty : ds.Tables[0].Rows[0]["SEMESTERNAME"].ToString();
                lblsemester.ToolTip = ds.Tables[0].Rows[0]["SEMESTERNO"] == null ? string.Empty : ds.Tables[0].Rows[0]["SEMESTERNO"].ToString();
                hfsemno.Value = ds.Tables[0].Rows[0]["SEMESTERNO"] == null ? string.Empty : ds.Tables[0].Rows[0]["SEMESTERNO"].ToString();
                lblMobile.Text = ds.Tables[0].Rows[0]["STUDENTMOBILE"] == null ? string.Empty : ds.Tables[0].Rows[0]["STUDENTMOBILE"].ToString();
                lblemail.Text = ds.Tables[0].Rows[0]["EMAILID"] == null ? string.Empty : ds.Tables[0].Rows[0]["EMAILID"].ToString();

                txttranid.Text = ds.Tables[0].Rows[0]["TRANSACTION_NO"] == null ? string.Empty : ds.Tables[0].Rows[0]["TRANSACTION_NO"].ToString();
                txttransamount.Text = ds.Tables[0].Rows[0]["TRANSACTION_AMT"] == null ? string.Empty : ds.Tables[0].Rows[0]["TRANSACTION_AMT"].ToString().TrimEnd('.');
                txttransdate.Text = ds.Tables[0].Rows[0]["TRANS_DATE"] == null ? string.Empty : ds.Tables[0].Rows[0]["TRANS_DATE"].ToString();
                ddlstatus.SelectedValue = ds.Tables[0].Rows[0]["APPROVAL_STATUS"].ToString();
                txtremark.Text = ds.Tables[0].Rows[0]["REMARK"].ToString();
                if (ds.Tables[0].Rows[0]["DOC_PATH"].ToString() == "Blob Storage")
                {
                    DivDocument.Visible = true;
                    lnkTransDoc.ToolTip = ds.Tables[0].Rows[0]["DOC_NAME"] == null ? string.Empty : ds.Tables[0].Rows[0]["DOC_NAME"].ToString();
                    btnDownloadFile.Text = ds.Tables[0].Rows[0]["DOC_NAME"] == null ? string.Empty : ds.Tables[0].Rows[0]["DOC_NAME"].ToString();
                    btnDownloadFile.ToolTip = ds.Tables[0].Rows[0]["DOC_NAME"] == null ? string.Empty : ds.Tables[0].Rows[0]["DOC_NAME"].ToString();
                }
                else
                {
                    DivDocument.Visible = false;
                }


                if (Session["usertype"].ToString().Equals("2"))     //Student 
                {
                    if (ds.Tables[0].Rows[0]["APPROVAL_STATUS"].ToString() == "1")
                    {
                        //objCommon.DisplayMessage(this, "Your Exam Transaction Details Approved by admin.", this.Page);
                        txttranid.Enabled = false;
                        txttransamount.Enabled = false;
                        txttransdate.Enabled = false;
                        fuUpload.Enabled = false;
                        btnSave.Enabled = false;
                        btnSave.Visible = false;
                        btnCancel.Visible = false;
                        DivAdminapproval.Visible = true;
                        txtremark.Enabled = false;
                        ddlstatus.Enabled = false;
                        Divstatus.Visible = true;
                    }
                    else if (ds.Tables[0].Rows[0]["APPROVAL_STATUS"].ToString() == "2")
                    {
                        // objCommon.DisplayMessage(this, "Your Exam Transaction Details Rejected by admin. Please make correction.", this.Page);
                        txttranid.Enabled = true;
                        txttransamount.Enabled = true;
                        txttransdate.Enabled = true;
                        fuUpload.Enabled = true;
                        btnSave.Enabled = true;
                        btnSave.Visible = true;
                        btnCancel.Visible = true;
                        DivAdminapproval.Visible = true;
                        txtremark.Enabled = false;
                        ddlstatus.Enabled = false;
                        ddlstatus.Visible = false;
                        Divstatus.Visible = false;
                    }
                    else
                    {
                        txttranid.Enabled = true;
                        txttransamount.Enabled = true;
                        txttransdate.Enabled = true;
                        fuUpload.Enabled = true;
                        btnSave.Enabled = true;
                        btnSave.Visible = true;
                        btnCancel.Visible = true;
                        DivAdminapproval.Visible = false;
                    }
                }
                else
                {
                    if (ds.Tables[0].Rows[0]["APPROVAL_STATUS"].ToString() == "1")
                    {
                        txttranid.Enabled = false;
                        txttransamount.Enabled = false;
                        txttransdate.Enabled = false;
                        fuUpload.Enabled = false;
                        ddlstatus.Enabled = false;
                        txtremark.Enabled = false;
                        btnSave.Enabled = false;
                        btnSave.Visible = false;
                        btnCancel.Visible = false;
                        ddlstatus.SelectedValue = ds.Tables[0].Rows[0]["APPROVAL_STATUS"].ToString();
                        txtremark.Text = ds.Tables[0].Rows[0]["REMARK"].ToString();
                    }
                    else
                    {
                        txttranid.Enabled = false;
                        txttransamount.Enabled = false;
                        txttransdate.Enabled = false;
                        fuUpload.Enabled = false;
                        ddlstatus.Enabled = true;
                        txtremark.Enabled = true;
                        btnSave.Enabled = true;
                        btnSave.Visible = true;
                        btnCancel.Visible = true;
                        ddlstatus.SelectedValue = ds.Tables[0].Rows[0]["APPROVAL_STATUS"].ToString();
                        txtremark.Text = ds.Tables[0].Rows[0]["REMARK"].ToString();
                    }
                }
            }
            BindCourses();
            //}
            //if (dr != null)
            //    dr.Close();
        }
        catch (Exception ex)
        {
        }
    }

    private void BindCourses()
    {
        DataSet dsCERT = objCommon.DynamicSPCall_Select("PKG_ACD_GET_REGISTERED_COURSE_TRANSACTION", "@P_IDNO,@P_SESSIONNO, @P_SEMESTERNO", "" + Convert.ToInt32(hfidno.Value) + "," + Convert.ToInt32(ddlSession.SelectedValue) + "," + Convert.ToInt32(lblsemester.ToolTip) + "");
        // DataSet dsCERT = objExamController.GetTransactionEntryList(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue));
        if (dsCERT.Tables[0].Rows.Count > 0 && dsCERT != null)
        {
            DivRegCourse.Visible = true;
            lvCurrentSubjects.DataSource = dsCERT;
            lvCurrentSubjects.DataBind();
        }
        else
        {
            //objCommon.DisplayMessage(this, "No Record Found", this.Page);
            DivRegCourse.Visible = false;
            lvCurrentSubjects.DataSource = null;
            lvCurrentSubjects.DataBind();

        }
    }

    protected void lnkTransDoc_Click(object sender, EventArgs e)
    {
        string Url = string.Empty;
        string directoryPath = string.Empty;
        try
        {
            string blob_ConStr = System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"].ToString();
            string blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerNameEXAM"].ToString();

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(blob_ConStr);
            CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();
            string directoryName = "~/DownloadImg" + "/";
            directoryPath = Server.MapPath(directoryName);

            if (!Directory.Exists(directoryPath.ToString()))
            {

                Directory.CreateDirectory(directoryPath.ToString());
            }
            CloudBlobContainer blobContainer = cloudBlobClient.GetContainerReference(blob_ContainerName);
            string img = ((System.Web.UI.WebControls.Button)(sender)).ToolTip.ToString();
            var ImageName = img;
            if (img == null || img == "")
            {

                // objCommon.DisplayMessage(this, "Image not Found...", this);
                string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"600px\" height=\"400px\">";
                embed += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
                embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
                embed += "</object>";
                ltEmbed.Text = "Image Not Found....!";

            }
            else
            {

                DataTable dtBlobPic = Blob_GetById(blob_ConStr, blob_ContainerName, img);

                var blob = blobContainer.GetBlockBlobReference(ImageName);

                string filePath = directoryPath + "\\" + ImageName;


                if ((System.IO.File.Exists(filePath)))
                {
                    System.IO.File.Delete(filePath);
                }

                blob.DownloadToFile(filePath, System.IO.FileMode.CreateNew);


                string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"600px\" height=\"500px\">";
                embed += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
                embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
                embed += "</object>";
                ltEmbed.Text = string.Format(embed, ResolveUrl("~/DownloadImg/" + ImageName));
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        //string trans_no = txttranid.Text;
        //int count = Convert.ToInt32(objCommon.LookUp("ACD_EXAM_TRANSACTION_DETAILS", "COUNT(1)", "TRANSACTION_NO='" + trans_no + "'"));
        //if (count == 0)
        //{
            try
            {
                string filename = string.Empty;
                string FilePath = string.Empty;
                int ret = 0;

                if (ddlSession.SelectedIndex > 0)
                {
                    ObjE.Idno = Convert.ToInt32(hfidno.Value);
                    ObjE.Sessionno = Convert.ToInt32(ddlSession.SelectedValue);
                    ObjE.Transaction_no = txttranid.Text.Trim();
                    ObjE.trans_amt = Convert.ToDecimal(txttransamount.Text.Trim());
                    if (!txttransdate.Text.Trim().Equals(string.Empty)) ObjE.Transaction_date = Convert.ToDateTime(txttransdate.Text.Trim());
                    ObjE.OrgId = Convert.ToInt32(Session["OrgId"]);
                    ObjE.Approvedby = Convert.ToInt32(Session["userno"].ToString());
                    ObjE.Approvedstatus = Convert.ToInt32(ddlstatus.SelectedValue);
                    ObjE.Remark = txtremark.Text.Trim();

                    if (Session["usertype"].ToString() == "2")
                    {
                        int Count = Convert.ToInt32(objCommon.LookUp("ACD_EXAM_TRANSACTION_DETAILS", "COUNT(*)", "IDNO=" + Convert.ToInt32(hfidno.Value)));
                        if (Count > 0)
                        {
                            if (fuUpload.HasFile)
                            {
                                string contentType = contentType = fuUpload.PostedFile.ContentType;
                                string ext = System.IO.Path.GetExtension(fuUpload.PostedFile.FileName);
                                HttpPostedFile file = fuUpload.PostedFile;
                                filename = ObjE.Idno + "_ExamTransactionDoc_" + ddlSession.SelectedValue + "_" + lblregistrationnos.Text.Trim() + ext;
                                ObjE.file_name = filename;
                                ObjE.file_path = "Blob Storage";

                                if (ext == ".pdf" || ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".PNG" || ext == ".PDF")
                                {

                                    if (file.ContentLength <= 524288)// 31457280 before size 524288 40960  //For Allowing 512 Kb Size Files only 
                                    {
                                        int retval = Blob_Upload(blob_ConStr, blob_ContainerName, ObjE.Idno + "_ExamTransactionDoc_" + ddlSession.SelectedValue + "_" + lblregistrationnos.Text.Trim() + "", fuUpload);
                                        if (retval == 0)
                                        {
                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Unable to upload...Please try again...');", true);
                                            return;
                                        }

                                        ret = Convert.ToInt32(objExamController.AddExamTransactionDetails(ObjE, Convert.ToInt32(Session["usertype"])));
                                        if (ret > 0)
                                        {
                                            BindStudentdetailsaftersubmit(ObjE.Idno);
                                            //objCommon.DisplayMessage(this, "Exam Transaction Details Save Sucessfully please go for exam registration. !", this);
                                            objCommon.DisplayMessage(this, "Thank you for submitting Exam Transaction Registration Form.", this);
                                            return;
                                        }
                                        else
                                        {
                                            objCommon.DisplayMessage(this, "Something went wrong ..Please try again !", this);
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        objCommon.DisplayMessage(this, "Please Upload file Below or Equal to 512 Kb only !", this);
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
                                        return;
                                    }
                                }
                                else
                                {
                                    objCommon.DisplayMessage(this, "Upload the Documents only with following formats: .jpg, .jpeg, .pdf!", this);
                                    return;
                                }
                            }
                            else
                            {

                                ObjE.file_name = btnDownloadFile.ToolTip;
                                ObjE.file_path = "Blob Storage";
                                ret = Convert.ToInt32(objExamController.AddExamTransactionDetails(ObjE, Convert.ToInt32(Session["usertype"])));
                                if (ret > 0)
                                {
                                    BindStudentdetailsaftersubmit(ObjE.Idno);
                                    //objCommon.DisplayMessage(this, "Exam Transaction Details Updated Sucessfully Please go for exam registration. !", this);
                                    objCommon.DisplayMessage(this, "Thank you for submitting Exam Transaction Registration Form.", this);
                                    return;
                                }
                                else
                                {
                                    objCommon.DisplayMessage(this, "Something went wrong ..Please try again !", this);
                                    return;
                                }
                            }
                        }
                        else
                        {
                            if (fuUpload.HasFile)
                            {
                                string contentType = contentType = fuUpload.PostedFile.ContentType;
                                string ext = System.IO.Path.GetExtension(fuUpload.PostedFile.FileName);
                                HttpPostedFile file = fuUpload.PostedFile;
                                filename = ObjE.Idno + "_ExamTransactionDoc_" + ddlSession.SelectedValue + "_" + lblregistrationnos.Text.Trim() + ext;
                                ObjE.file_name = filename;
                                ObjE.file_path = "Blob Storage";

                                if (ext == ".pdf" || ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".PNG" || ext == ".PDF")
                                {

                                    if (file.ContentLength <= 524288)// 31457280 before size 524288 40960  //For Allowing 512 Kb Size Files only 
                                    {
                                        int retval = Blob_Upload(blob_ConStr, blob_ContainerName, ObjE.Idno + "_ExamTransactionDoc_" + ddlSession.SelectedValue + "_" + lblregistrationnos.Text.Trim() + "", fuUpload);
                                        if (retval == 0)
                                        {
                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Unable to upload...Please try again...');", true);
                                            return;
                                        }

                                        ret = Convert.ToInt32(objExamController.AddExamTransactionDetails(ObjE, Convert.ToInt32(Session["usertype"])));
                                        if (ret > 0)
                                        {
                                            BindStudentdetailsaftersubmit(ObjE.Idno);
                                            //objCommon.DisplayMessage(this, "Exam Transaction Details Save Sucessfully please go for exam registration. !", this);
                                            objCommon.DisplayMessage(this, "Thank you for submitting Exam Transaction Registration Form.", this);
                                            return;
                                        }
                                        else
                                        {
                                            objCommon.DisplayMessage(this, "Something went wrong ..Please try again !", this);
                                            return;
                                        }

                                    }
                                    else
                                    {
                                        objCommon.DisplayMessage(this, "Please Upload file Below or Equal to 512 Kb only !", this);
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
                                        return;
                                    }
                                }
                                else
                                {
                                    objCommon.DisplayMessage(this, "Upload the Documents only with following formats: .jpg, .jpeg, .pdf!", this);
                                    return;
                                }
                            }
                            else
                            {
                                objCommon.DisplayMessage(this, "Please Upload the Documents.", this.Page);
                                return;
                            }
                        }
                    }
                    else
                    {
                        //objCommon.DisplayMessage(this, "Either this Activity has been Stopped Or You are Not Authorized to View this Page. Contact Admin.", this.Page);
                        //return;
                        ObjE.file_name = btnDownloadFile.ToolTip;
                        ObjE.file_path = "Blob Storage";
                        ret = Convert.ToInt32(objExamController.AddExamTransactionDetails(ObjE, Convert.ToInt32(Session["usertype"])));
                        if (ret > 0)
                        {
                            BindStudentDetails(ObjE.Idno);
                            objCommon.DisplayMessage(this, "Student Exam Transaction " + ddlstatus.SelectedItem.Text + " Sucessfully.", this);
                            return;
                        }
                        else
                        {
                            objCommon.DisplayMessage(this, "Something went wrong ..Please try again !", this);
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        //}
        //else
        //{
        //    objCommon.DisplayMessage("Transaction_Id is Already Exists..", this.Page);
        //    return;

        //}
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl.ToString());
    }

    private CloudBlobContainer Blob_Connection(string ConStr, string ContainerName)
    {
        CloudStorageAccount account = CloudStorageAccount.Parse(ConStr);
        CloudBlobClient client = account.CreateCloudBlobClient();
        CloudBlobContainer container = client.GetContainerReference(ContainerName);
        return container;
    }

    public int Blob_Upload(string ConStr, string ContainerName, string DocName, FileUpload FU)
    {
        CloudBlobContainer container = Blob_Connection(ConStr, ContainerName);
        int retval = 1;
        string Ext = Path.GetExtension(FU.FileName);
        string FileName = DocName + Ext;
        try
        {
            DeleteIFExits(FileName);
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
           

            CloudBlockBlob cblob = container.GetBlockBlobReference(FileName);
            cblob.UploadFromStream(FU.PostedFile.InputStream);
        }
        catch
        {
            retval = 0;
            return retval;
        }
        return retval;
    }

    public void DeleteIFExits(string FileName)
    {
        CloudBlobContainer container = Blob_Connection(blob_ConStr, blob_ContainerName);
        string FN = Path.GetFileNameWithoutExtension(FileName);
        try
        {
            Parallel.ForEach(container.ListBlobs(FN, true), y =>
            {
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
                ((CloudBlockBlob)y).DeleteIfExists();
            });
        }
        catch (Exception) { }
    }

    public DataTable Blob_GetById(string ConStr, string ContainerName, string Id)
    {
        CloudBlobContainer container = Blob_Connection(ConStr, ContainerName);
        System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
        var permission = container.GetPermissions();
        permission.PublicAccess = BlobContainerPublicAccessType.Container;
        container.SetPermissions(permission);

        DataTable dt = new DataTable();
        dt.TableName = "FilteredBolb";
        dt.Columns.Add("Name");
        dt.Columns.Add("Uri");

        //var blobList = container.ListBlobs(useFlatBlobListing: true);
        var blobList = container.ListBlobs(Id, true);
        foreach (var blob in blobList)
        {
            string x = (blob.Uri.ToString().Split('/')[blob.Uri.ToString().Split('/').Length - 1]);
            string y = x.Split('_')[0];
            dt.Rows.Add(x, blob.Uri);
        }
        return dt;
    }

    protected void btnDownloadFile_Click(object sender, EventArgs e)
    {
        LinkButton lnkbtn = sender as LinkButton;
        string filename = ((System.Web.UI.WebControls.LinkButton)(sender)).ToolTip.ToString();

        string accountname = System.Configuration.ConfigurationManager.AppSettings["Blob_AccountName"].ToString();
        string accesskey = System.Configuration.ConfigurationManager.AppSettings["Blob_AccessKey"].ToString();
        string containerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerNameEXAM"].ToString();

        StorageCredentials creden = new StorageCredentials(accountname, accesskey);
        CloudStorageAccount acc = new CloudStorageAccount(creden, useHttps: true);
        CloudBlobClient client = acc.CreateCloudBlobClient();
        CloudBlobContainer container = client.GetContainerReference(containerName);
        CloudBlob blob = container.GetBlobReference(filename);
        MemoryStream ms = new MemoryStream();
        System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
        blob.DownloadToStream(ms);
        Response.ContentType = blob.Properties.ContentType;
        Response.AddHeader("Content-Disposition", "Attachment; filename=" + filename.ToString());
        Response.AddHeader("Content-Length", blob.Properties.Length.ToString());
        Response.BinaryWrite(ms.ToArray());
    }

    protected void txttransdate_TextChanged(object sender, EventArgs e)
    {
        if (Convert.ToDateTime(txttransdate.Text) > DateTime.Now)
        {
            objCommon.DisplayMessage(this,"Please Select valid Date.", this.Page);
            txttransdate.Text = string.Empty;
            txttransdate.Focus();
            return;
        }
    }
    protected void txttranid_TextChanged(object sender, EventArgs e)
    {
         string trans_no = txttranid.Text;
        int count = Convert.ToInt32(objCommon.LookUp("ACD_EXAM_TRANSACTION_DETAILS", "COUNT(1)", "TRANSACTION_NO='" + trans_no + "'"));
        if (count>0)
        {
            objCommon.DisplayMessage("Transcation_id already Exists....", this.Page);
            txttranid.Text = string.Empty;
            return;
        }
    }
}