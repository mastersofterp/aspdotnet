//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC
// PAGE NAME     : PHD THESIS APPROVAL ENTRY                                                     
// CREATION DATE : 27-FEB-2023                                                          
// CREATED BY    : NEHAL                                                                     
//======================================================================================

using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.IO;

public partial class ACADEMIC_PHD_Phd_Thesis_Approval_Entry : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    PhdController objPhd = new PhdController();
    Phd obju = new Phd();

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
        if (!Page.IsPostBack)
        {
            //Check Session
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                //Page Authorization
                //if (Session["usertype"] == "1")
                //{
                CheckPageAuthorization();
                if (Session["usertype"].ToString() == "1" || Session["usertype"].ToString() == "8")
                {
                    //objCommon.FillDropDownList(ddlAcdYear, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO > 0", "BATCHNO desc");
                }
                else
                {
                    Response.Redirect("~/notauthorized.aspx?page=Phd_Thesis_Approval_Entry.aspx");
                }
                //}

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                ////Load Page Help
                //if (Request.QueryString["pageno"] != null)
                //{
                //    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                //}
                this.objCommon.FillDropDownList(ddlStatus, "ACD_PHD_THESIS_STATUS_MASTER", "STATUSNO", "STATUSNAME", "ACTIVESTATUS = 1 AND SEQUENCE IS NOT NULL", "SEQUENCE");
                BindListView();
                updMaindiv.Visible = false;
            }
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Phd_Thesis_Approval_Entry.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Phd_Thesis_Approval_Entry.aspx");
        }
    }

    private void BindListView()
    {

        try
        {
            DataSet ds = objPhd.GetAllStudentApprovalThesisList(0);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                divStudentlist.Visible = true;
                lvStudentlist.DataSource = ds;
                lvStudentlist.DataBind();
            }
            else
            {
                divStudentlist.Visible = false;
                lvStudentlist.DataSource = null;
                lvStudentlist.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            divEntry.Visible = true;
            ImageButton idno = sender as ImageButton;
            DataSet ds = new DataSet();
            ds = objPhd.GetAllStudentApprovalThesisList(Convert.ToInt32(idno.CommandArgument));
            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["IDNO"] = ds.Tables[0].Rows[0]["IDNO"].ToString();
                txtTitleThesis.Text = ds.Tables[0].Rows[0]["THESIS_TITLE"].ToString();
                txtThesisdate.Text = ds.Tables[0].Rows[0]["THESIS_SUBMIT_DATE"].ToString();
                txtSynopsisDate.Text = ds.Tables[0].Rows[0]["SYNOPSIS_SUBMIT_DATE"].ToString();
                txtRemark.Text = ds.Tables[0].Rows[0]["FLAG_REMARK"].ToString();
                ddlExaminerType.SelectedValue = ds.Tables[0].Rows[0]["EXAMINER_TYPE_NO"].ToString();
                ddlStatus.SelectedValue = ds.Tables[0].Rows[0]["FLAG"].ToString();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Phd_Thesis_Approval_Entry.btnEdit_Click" + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnView_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            updMaindiv.Visible = true;
            MpHistory.Show();
            ImageButton idno = sender as ImageButton;
            DataSet ds = new DataSet();
            ds = objPhd.GetAllStudStatusDetails(Convert.ToInt32(idno.CommandArgument));
            if (ds.Tables[7].Rows.Count > 0)
            {
                lvstustslist.DataSource = ds.Tables[7];
                lvstustslist.DataBind(); 
            }
            else
            {
                objCommon.DisplayMessage(this.updSession, "Approval pending...!!!", this.Page);
                updMaindiv.Visible = false;
                return;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Phd_Thesis_Approval_Entry.btnEdit_Click" + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int idno = Convert.ToInt32(ViewState["IDNO"].ToString());
            obju.EXAMINER_TYPE = ddlExaminerType.SelectedItem.Text;
            obju.EXAMINER_TYPE_NO = Convert.ToInt32(ddlExaminerType.SelectedValue.ToString());
            obju.FLAG_NAME = ddlStatus.SelectedItem.Text;
            obju.FLAG_REMARK = txtRemark.Text;
            obju.FLAG = Convert.ToInt32(ddlStatus.SelectedValue.ToString());
            obju.SUBMISSION_STATUS_NO = Convert.ToInt32(ddlStatus.SelectedValue.ToString());
            //obju.SUBMISSION_STATUS = ddlStatus.SelectedItem.Text;
            obju.SUBMISSION_UANO = Convert.ToInt32(Session["userno"].ToString());
            obju.SUBMISSION_IP = Session["ipAddress"].ToString();
            //obju.SUBMISSION_REMARK = txtRemark.Text;
            obju.SUBMISSION_CREATE_DATE = Convert.ToDateTime(DateTime.Now.ToString());

            CustomStatus cs = (CustomStatus)objPhd.InsertThesisApprovalEntry(idno, obju);
            if (cs.Equals(CustomStatus.RecordUpdated))
            {
                ClearControls();
                divEntry.Visible = false;
                objCommon.DisplayMessage(this.updSession, "Record Updated Successfully", this.Page);
            }
            BindListView();
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearControls();
        Response.Redirect(Request.Url.ToString());
    }
    private void ClearControls()
    {
        txtThesisdate.Text = string.Empty;
        txtSynopsisDate.Text = string.Empty;
        txtTitleThesis.Text = string.Empty;
        ddlExaminerType.SelectedValue = "0";
        ddlStatus.SelectedValue = "0";
        txtRemark.Text = string.Empty;
    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        DataSet ds = objPhd.GetStudentDataExcel(0);
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            GridView gvStudData = new GridView();
            gvStudData.DataSource = ds;
            gvStudData.DataBind();
            string FinalHead = @"<style>.FinalHead { font-weight:bold; }</style>";
            string attachment = "attachment; filename=PHD_Thesis_Tracking_Status.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.MS-excel";
            StringWriter sw = new StringWriter();

            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Response.Write(FinalHead);

            gvStudData.RenderControl(htw);
            //string a = sw.ToString().Replace("_", " ");
            Response.Write(sw.ToString());
            Response.End();
        }
        else
        {
            objCommon.DisplayMessage(updSession, "No Record Found", this);
            lvStudentlist.DataSource = null;
            lvStudentlist.DataBind();
            return;

        }
    }
    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet ds = objPhd.GetAllStudentThesisList(Convert.ToInt32(ViewState["IDNO"].ToString()));

        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            if (ddlStatus.SelectedValue == "1")
            {
                txtRemark.Text = ds.Tables[0].Rows[0]["SUBMISSION_REMARK"].ToString();
            }
            else if (ddlStatus.SelectedValue == "2")
            {
                txtRemark.Text = ds.Tables[0].Rows[0]["DISPATCHEDTOREVIEWER_REMARK"].ToString();
            }
            else if (ddlStatus.SelectedValue == "3")
            {
                txtRemark.Text = ds.Tables[0].Rows[0]["REVIEWERREPORTRECEIVED_REMARK"].ToString();
            }
            else if (ddlStatus.SelectedValue == "4")
            {
                txtRemark.Text = ds.Tables[0].Rows[0]["OPENDEFENCEVIVASCHEDULE_REMARK"].ToString();
            }
            else if (ddlStatus.SelectedValue == "5")
            {
                txtRemark.Text = ds.Tables[0].Rows[0]["AWARDED_REMARK"].ToString();
            }

            else if (ddlStatus.SelectedValue == "6")
            {
                txtRemark.Text = ds.Tables[0].Rows[0]["FOREIGNEXAMINERREPORT_REMARK"].ToString();
            }
        }
        else
        {
            txtRemark.Text = string.Empty;
        }
    }
}