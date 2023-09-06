using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class ACADEMIC_ReceiptCode : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    FeeCollectionController objFCC = new FeeCollectionController();
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
                     if (Request.QueryString["pageno"] != null)
                {
                   
                }
            }
            BindListView();
            
        }
    }

    protected void BindListView()
    {
        try
        {

            DataSet ds = objFCC.GetReceiptCodeList();

            if (ds.Tables[0].Rows.Count > 0)
            {
                PanelReceiptCode.Visible = true;
                lvReceiptCode.DataSource = ds.Tables[0];
                lvReceiptCode.DataBind();
            }
            else
            {
                PanelReceiptCode.Visible = true;
                lvReceiptCode.DataSource = null;
                lvReceiptCode.DataBind();
                objCommon.DisplayMessage(this.Page, "Record Not Found", Page);
            }
            foreach (ListViewDataItem dataitem in lvReceiptCode.Items)
            {
                Label Status = dataitem.FindControl("lblStatus") as Label;

                string Statuss = (Status.ToolTip);

                if (Statuss == "0")
                {
                    
                    Status.Text = "INACTIVE";
                }
                else
                {
                    Status.Text = "ACTIVE";
                }

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Faculty_DisciplineMaster.Bind-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    public void ClearData()
    {
        txtReceiptName.Text = string.Empty;
        txtReceiptCode.Text = string.Empty;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string status;
             string RC_NAME = txtReceiptName.Text.Trim();
        string RECIEPT_CODE = txtReceiptCode.Text.Trim();

        if (hfdActive.Value == "true")
        {
            status = "1";
        }
        else
        {
            status = "0";
        }

         if (ViewState["action"] != null && ViewState["action"].ToString().Equals("edit"))
         {

             int id = Convert.ToInt32(ViewState["id"]);

             CustomStatus cs = (CustomStatus)objFCC.UpdateReceiptCodeData(id, RC_NAME, RECIEPT_CODE, status);
             if (cs.Equals(CustomStatus.RecordUpdated))
             {
                 ClearData();
                 BindListView();
                 objCommon.DisplayMessage(this, "Record Updated sucessfully", this.Page);
                 ViewState["action"] = null;
             }
         }
         else
         {
             //Edit 
             CustomStatus cs = (CustomStatus)objFCC.InsertReceiptCodeData(0,RC_NAME, RECIEPT_CODE, status);
             if (cs.Equals(CustomStatus.RecordSaved))
             {
                  
                 objCommon.DisplayMessage(this, "Record Saved sucessfully", this.Page);
                 BindListView();
                 ClearData();


             }

             else
             {

                 objCommon.DisplayMessage(this, "Record Already Exist", this.Page);
                 ClearData();


             }
             BindListView();
         }
        }
        catch (Exception ex)
        {
            
           if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Receipt_Code.Bind-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int ID = Convert.ToInt32(btnEdit.CommandArgument);
            ViewState["id"] = Convert.ToInt32(btnEdit.CommandArgument);
            ShowDetail(ID);
            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "BranchWiseIntake.btnEdit_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void ShowDetail(int ID)
    {

        DataSet ds = objFCC.EditReceiptCodeData(ID);
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {


            txtReceiptName.Text = ds.Tables[0].Rows[0]["RC_NAME"].ToString();
            txtReceiptCode.Text = ds.Tables[0].Rows[0]["RECIEPT_CODE"].ToString();
            if (ds.Tables[0].Rows[0]["ACTIVESTATUS"].ToString() == "1")
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetSetSubjecttype(true);", true);

            }
            else
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetSetSubjecttype(false);", true);
            }
        }
    }
}