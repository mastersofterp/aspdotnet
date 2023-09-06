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
using System.Collections.Generic;

using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer;
using BusinessLogicLayer.BusinessLogic.Academic;

public partial class ACADEMIC_PHD_Phd_Thesis_Status_Master : System.Web.UI.Page
{
    #region Page Events
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    PhdController objAC = new PhdController();
    static int StatusNo;

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
                this.CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                }

            }

            this.objCommon.FillDropDownList(ddlAcademicBatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "ACTIVESTATUS = 1", "BATCHNO");
            
            BindListView();
            ViewState["action"] = "add";
        }
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Phd_Thesis_Status_Master.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Phd_Thesis_Status_Master.aspx");
        }
    }
    #endregion
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            int Active_Status = 0;

            if (hdnActive.Value == "true")
            {
                Active_Status = 1;
            }
            else
            {
                Active_Status = 0;
            }
            //Check whether to add or update
            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    //Add Batch

                    CustomStatus cs = (CustomStatus)objAC.AddStatusMaster(txtStatus.Text, Active_Status, Convert.ToInt32(ddlAcademicBatch.SelectedValue), Convert.ToInt32(txtSeq.Text));
                    if (cs.Equals(CustomStatus.DuplicateRecord))
                    {
                        objCommon.DisplayMessage(updStatus, "Record Already Exist", this.Page);
                    }
                    else if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        ViewState["action"] = "add";
                        objCommon.DisplayMessage(updStatus, "Record Saved Successfully!", this.Page);
                        Clear();
                    }
                    else
                    {
                        objCommon.DisplayMessage(updStatus, "Error Adding Status Name!", this.Page);
                    }
                }
                else
                {
                    CustomStatus cs = (CustomStatus)objAC.UpdateStatusMaster(StatusNo, txtStatus.Text, Active_Status, Convert.ToInt32(ddlAcademicBatch.SelectedValue), Convert.ToInt32(txtSeq.Text));

                    if (cs.Equals(CustomStatus.DuplicateRecord))
                    {
                        objCommon.DisplayMessage(updStatus, "Record Already Exist", this.Page);
                    }
                    else if (cs.Equals(CustomStatus.RecordUpdated))
                    {

                        objCommon.DisplayMessage(this.updStatus, "Record Updated Successfully!", this.Page);

                        Clear();
                        //ViewState["action"] = null;
                        btnSave.Text = "Submit";
                        //btnSave.CssClass = "btn btn-success";
                        ddlAcademicBatch.Focus();

                        //Response.Redirect(Request.Url.ToString());
                    }
                    else
                    {
                        objCommon.DisplayMessage(updStatus, "Error Adding Status Name!", this.Page);
                    }

                }

                BindListView();

            }

        }
        catch (Exception ex)
        {
            throw;
        }

    }
    protected void Clear()
    {
        txtStatus.Text = string.Empty;
        ddlAcademicBatch.SelectedValue = "0";
        txtSeq.Text = string.Empty;
        //Label1.Text = string.Empty;
        ViewState["action"] = "add";
    }
    private void BindListView()
    {
        try
        {
            DataSet ds = objAC.GetAllStatusNo(0);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                divstatuslist.Visible = true;
                lvStatus.DataSource = ds;
                lvStatus.DataBind();
            }
            else
            {
                divstatuslist.Visible = false;
                lvStatus.DataSource = null;
                lvStatus.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
        ViewState["action"] = null;
        btnSave.Text = "Submit";
        txtStatus.Focus();
    }
    private void ShowDetail(int StatusNo)
    {
        DataSet ds = new DataSet();
        ds = objAC.GetAllStatusNo(StatusNo);

        if (ds.Tables[0].Rows.Count > 0)
        {
            if (ds.Tables[0].Rows[0]["ACTIVESTATUS"].ToString() == "1")
                ScriptManager.RegisterStartupScript(this, GetType(), "act", "$('[id*=chkActive]').prop('checked', true);", true);
            else
                ScriptManager.RegisterStartupScript(this, GetType(), "act1", "$('[id*=chkActive]').prop('checked', false);", true);

            txtStatus.Text = ds.Tables[0].Rows[0]["STATUSNAME"].ToString();
            txtSeq.Text = ds.Tables[0].Rows[0]["SEQUENCE"].ToString();
            ddlAcademicBatch.SelectedValue = ds.Tables[0].Rows[0]["ADMBATCH"].ToString();

        }
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        //string MyScript = "$(document).ready(function () { var table = $('#example').DataTable();table.button(0).disable();});";
        //ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey",MyScript, true);
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            StatusNo = int.Parse(btnEdit.CommandArgument);
            //Label1.Text = string.Empty;

            ShowDetail(StatusNo);
            ViewState["action"] = "edit";

            btnSave.Text = "Update";
            ddlAcademicBatch.Focus();
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}

