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
using System.Web.Services;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using DynamicAL_v2;

public partial class CreateTemplate : System.Web.UI.Page
{
    Common objCommon = new Common();
    //UAIMS_Common objUCommon = new UAIMS_Common();
    ExamController objExamController = new ExamController();
    DynamicControllerAL AL = new DynamicControllerAL();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["masterpage"] != null)
        {
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        }
        else
        {
            objCommon.SetMasterPage(Page, "");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                //This checks the authorization of the user.
            //    CheckPageAuthorization();

                Page.Title = Session["coll_name"].ToString();

                //if (Request.QueryString["pageno"] != null)
                //{
                //    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                //}

                // Set form mode equals to -1(New Mode).
                ViewState["exdtno"] = "0";

                
                divMsg.InnerHtml = string.Empty;
                ViewState["roomname"] = string.Empty;
            }

            LoadDropDowns();
            GetTemplate();
        }
    }

    void LoadDropDowns()
    {
        objCommon.FillDropDownList(ddlCategoty, "ACD_MAIL_CATEGORIES", "ID", "CATEGORY_NAME", "", "CATEGORY_NAME");
    }

    private void GetTemplate()
    {
        string SP = "PKG_CRUD_MAIL_DATA_CATEGORY";
        string PR = "@P_ID, @P_CATEGORY_ID, @P_NAME, @P_SP_NAME, @P_OPERATION";
        string VL = "0,0,0,0,2";

        DataSet ds = AL.DynamicSPCall_Select(SP, PR, VL);
        rptTemplate.DataSource = ds;
        rptTemplate.DataBind();
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=SessionCreate.aspx");
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlCategoty.SelectedValue == "0")
            {
                ScriptManager.RegisterStartupScript(updTemplate, updTemplate.GetType(), "Script", "alert('Select Template Name.');", true);
                ddlCategoty.Focus();
                return;
            }
            else if (txtCategoryName.Text==string.Empty)
            {
                ScriptManager.RegisterStartupScript(updTemplate, updTemplate.GetType(), "Script", "alert('Enter Data Type Name.');", true);
                txtCategoryName.Focus();
                return;
            }
            else if (txtSPName.Text==string.Empty)
            {
                ScriptManager.RegisterStartupScript(updTemplate, updTemplate.GetType(), "Script", "alert('Enter SP Name.');", true);
                txtSPName.Focus();
                return;
            }

            string Id="0";
            int Action = 1;
            if (ViewState["ProcessID"] != null)
            {
                Id = Convert.ToString(ViewState["ProcessID"]);
                Action = Convert.ToInt32(ViewState["Action"]);
            }

            string SP = "PKG_CRUD_MAIL_DATA_CATEGORY";
            string PR = "@P_ID, @P_CATEGORY_ID, @P_NAME, @P_SP_NAME, @P_OPERATION";
            string VL = "" + Id + "," + Convert.ToInt32(ddlCategoty.SelectedValue) + "," + Convert.ToString(txtCategoryName.Text) + "," + Convert.ToString(txtSPName.Text) + "," + Action + "";

            string retVal = AL.DynamicSPCall_IUD(SP, PR, VL, true, 2);

            if (retVal == "-1")
            {
                ScriptManager.RegisterStartupScript(updTemplate, updTemplate.GetType(), "Script", "alert('Data Type with same name is already exists. Try another Data Type Name.');", true);
            }
            else if (retVal == "1")
            {
                ScriptManager.RegisterStartupScript(updTemplate, updTemplate.GetType(), "Script", "alert('Template Data Category created successfully.');", true);
                ClearControls();
                GetTemplate();
            }
            else if (retVal == "3")
            {
                ScriptManager.RegisterStartupScript(updTemplate, updTemplate.GetType(), "Script", "alert('Template Data Category Updated successfully.');", true);
                ClearControls();
                GetTemplate();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_MASTERS_RoomMaster.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.DisplayMessage("Server UnAvailable", this.Page);
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearControls();
    }

    private void ClearControls()
    {
        txtCategoryName.Text = txtSPName.Text = string.Empty;
        ddlCategoty.SelectedIndex = 0;
        btnSubmit.Text = "Submit";
        ViewState["Action"] = 1;
        ViewState["ProcessID"] = 0;
    }
    
    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton Ibtn = sender as ImageButton;
        int Id = Convert.ToInt32(Convert.ToString(Ibtn.CommandArgument).Split('$')[0]);

        string SP = "PKG_CRUD_MAIL_DATA_CATEGORY";
        string PR = "@P_ID, @P_CATEGORY_ID, @P_NAME, @P_SP_NAME, @P_OPERATION";
        string VL = "" + Id + ",0,0,0,4";

        string retVal = AL.DynamicSPCall_IUD(SP, PR, VL, true, 2);

        if (retVal == "4")
        {
            ScriptManager.RegisterStartupScript(updTemplate, updTemplate.GetType(), "Script", "alert('Data Type Deleted Successfully.');", true);
        }

        ClearControls();
        GetTemplate();
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton Ibtn = sender as ImageButton;
        int Id = Convert.ToInt32(Convert.ToString(Ibtn.CommandArgument).Split('$')[0]);
        txtCategoryName.Text = Convert.ToString(Ibtn.CommandArgument).Split('$')[1];
        txtSPName.Text = Convert.ToString(Ibtn.CommandArgument).Split('$')[2];
        ddlCategoty.SelectedValue = Convert.ToString(Ibtn.CommandArgument).Split('$')[3];
        btnSubmit.Text = "Update";
        ViewState["Action"] = 3;
        ViewState["ProcessID"] = Id;
    }
}
