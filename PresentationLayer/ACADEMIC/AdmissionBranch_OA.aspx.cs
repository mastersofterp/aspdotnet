using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Data;
public partial class ACADEMIC_AdmissionBranch_OA : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    string SP_Name = string.Empty; string SP_Call = string.Empty; string SP_Value = string.Empty;
    string mode = string.Empty; int branchNo = 0; int activeStatus = 0;
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
                if (Session["userno"] == null || Session["username"] == null ||
                  Session["usertype"] == null || Session["userfullname"] == null)
                {
                    Response.Redirect("~/default.aspx");
                }

                else
                {
                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();
                    CheckPageAuthorization();
                }
                PopulateDropDown();
                BindList();
            }
        }
        catch (Exception ex)
        {
            
            throw;
        }
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=AdmissionBranch_OA.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=AdmissionBranch_OA.aspx");
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            branchNo = ddlBranch.SelectedIndex > 0 ? Convert.ToInt32(ddlBranch.SelectedValue) : 0;
            if (hfdActive.Value == "true")
            {
                activeStatus = 1;
            }
            else
            {
                activeStatus = 0; 
            }
            DataSet dsUpd = null;
            mode = "UPDATE";
            SP_Name = "PKG_ACD_UPDATE_BRANCH_OA_STATUS";
            SP_Call = "@P_BRANCHNO,@P_ACTIVE_STATUS,@P_MODE";
            SP_Value = "" + branchNo + "," + activeStatus + "," + mode + "";
            dsUpd = objCommon.DynamicSPCall_Select(SP_Name, SP_Call, SP_Value);
            if (dsUpd.Tables[0].Rows.Count > 0)
            {
                if (dsUpd.Tables[0].Rows[0]["OUTPUT"].ToString().Equals("2"))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert(`Branch status updated successfully.`)", true);
                    ClearField();
                    BindList();
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            
            throw;
        }
    }
   
    protected void BindList()
    {
        try
        {
            DataSet dsBind = null;
            mode = "BIND";
            SP_Name = "PKG_ACD_UPDATE_BRANCH_OA_STATUS";
            SP_Call = "@P_BRANCHNO,@P_ACTIVE_STATUS,@P_MODE";
            SP_Value = "" + branchNo + "," + activeStatus + "," + mode+"";
            dsBind = objCommon.DynamicSPCall_Select(SP_Name, SP_Call, SP_Value);
            if (dsBind.Tables[0].Rows.Count > 0)
            {
                lvList.DataSource = dsBind;
                lvList.DataBind();
                pnlList.Visible = true;
            }
            else
            {
                lvList.DataSource = null;
                lvList.DataBind();
                pnlList.Visible = true;
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void PopulateDropDown()
    {
        try
        {
            DataSet dsDdl = null;
            mode = "BIND";
            SP_Name = "PKG_ACD_UPDATE_BRANCH_OA_STATUS";
            SP_Call = "@P_BRANCHNO,@P_ACTIVE_STATUS,@P_MODE";
            SP_Value = "" + branchNo + "," + activeStatus + "," + mode + "";
            dsDdl = objCommon.DynamicSPCall_Select(SP_Name, SP_Call, SP_Value);
            if (dsDdl.Tables[0].Rows.Count > 0)
            {
                ddlBranch.Items.Clear();
                ddlBranch.Items.Add(new ListItem("Please Select", "0"));
                ddlBranch.DataSource = dsDdl;
                ddlBranch.DataValueField = "BRANCHNO";
                ddlBranch.DataTextField = "BRANCH";
                ddlBranch.DataBind();
            }
            else
            {
                ddlBranch.Items.Clear();
                ddlBranch.Items.Add(new ListItem("Please Select", "0"));
            }
        }
        catch (Exception ex)
        {
            
            throw;
        }
    }
    protected void ClearField()
    {
        try
        {
            ddlBranch.SelectedIndex = 0;
            PopulateDropDown();
            hfdActive.Value = "false";
            branchNo = 0;
            activeStatus = 0;
            mode = string.Empty;
        }
        catch (Exception ex)
        {
            
            throw;
        }
    }
}