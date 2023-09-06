//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC
// PAGE NAME     : PHD THESIS APPROVAL ENTRY                                                     
// CREATION DATE : 01-Mar-2023                                                          
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

public partial class ACADEMIC_PHD_Phd_Thesis_Tracking_Status : System.Web.UI.Page
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
                //CheckPageAuthorization();

                CheckPageAuthorization();
                if (Session["usertype"].ToString() == "2")
                {
                    Response.Redirect("~/notauthorized.aspx?page=Phd_Thesis_Tracking_Status.aspx");
                }
                FillDropDown();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                if (Session["usertype"].ToString() == "3")
                {
                    divAdmBatch.Visible = true;
                    ddlAdmBatch.Visible = true;
                    //pnlHistory.Visible = false;
                    updMaindiv.Visible = false;
                }
                else
                {
                    divAdmBatch.Visible = false;
                    ddlAdmBatch.Visible = false;
                    updMaindiv.Visible = false;
                    BindListView();
                }
                updMaindiv.Visible = false;
            }
        }
    }
    private void FillDropDown()
    {
        try
        {
            this.objCommon.FillDropDownList(ddlAdmBatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "ACTIVESTATUS = 1", "BATCHNO");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Phd_Thesis_Tracking_Status.FillDropDown-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Phd_Thesis_Tracking_Status.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Phd_Thesis_Tracking_Status.aspx");
        }
    }

    private void BindListView()
    {

        try
        {
            DataSet ds;
          
            if (Session["usertype"].ToString() == "3")
            {
                int uano = Convert.ToInt32(Session["userno"].ToString());
                ds = objPhd.GetAllStudentApprovalThesisList_UANO(uano, Convert.ToInt32(ddlAdmBatch.SelectedValue));
            }
            else
            {
                 ds = objPhd.GetAllStudentApprovalThesisList(0);
            }

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                divStudentlist.Visible = true;
                btnExcel.Visible = true;
                lvStudentlist.DataSource = ds;
                lvStudentlist.DataBind();
            }
            else
            {
                objCommon.DisplayUserMessage(this.updSession, "Record not found.", this.Page);             
                divStudentlist.Visible = false;
                btnExcel.Visible = false;
                updMaindiv.Visible = false;
                lvStudentlist.DataSource = null;
                lvStudentlist.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw;
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
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        DataSet ds;
        if (Session["usertype"].ToString() == "3")
        {
            int uano = Convert.ToInt32(Session["userno"].ToString());
            ds = objPhd.GetStudentDataExcel_UANO(0,uano, Convert.ToInt32(ddlAdmBatch.SelectedValue));
        }
        else
        {
            ds = objPhd.GetStudentDataExcel(0);
        }
        
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
    protected void ddlAdmBatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlAdmBatch.SelectedIndex > 0)
        {
            BindListView();
        }
        else
        {
        }
    }
}