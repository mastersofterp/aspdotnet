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
public partial class ACADEMIC_BranchStudy_Diploma : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    string SP_Name = string.Empty; string SP_Call = string.Empty; string SP_Value = string.Empty;
    string ipAddress = string.Empty; string mode = string.Empty; int stdNo = 0; int degreeNo = 0; string branchStudy = string.Empty; int activeStatus = 0; int create_Modify = 0;
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
            objCommon.FillDropDownList(ddlDegree, "ACD_COLLEGE_DEGREE_BRANCH DB INNER JOIN ACD_DEGREE D ON(DB.DEGREENO=D.DEGREENO)", "DISTINCT DB.DEGREENO", "DBO.FN_DESC('DEGREENAME',DB.DEGREENO) DEGREENAME", "D.DEGREENO>0 AND D.ACTIVESTATUS=1", "DEGREENAME");
            BindList();
        }
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=BranchStudy_Diploma.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=BranchStudy_Diploma.aspx");
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet dsSubmit = null;
            degreeNo = ddlDegree.SelectedIndex > 0 ? Convert.ToInt32(ddlDegree.SelectedValue) : 0;
            branchStudy = txtBranchStudy.Text.ToString();
            ipAddress = Convert.ToString(Session["ipAddress"]);
            create_Modify = Convert.ToInt32(Session["userno"]);
            if (hfdActive.Value == "true")
            {
                activeStatus = 1;
            }
            else
            {
                activeStatus = 0;
            }
            if (ViewState["MODE"] == null || ViewState["MODE"].ToString().Equals(string.Empty))
            {
                mode = "INSERT";
                
            }
            else if (ViewState["MODE"].ToString().Equals("EDIT"))
            {
                mode = "UPDATE";
                stdNo = Convert.ToInt32(ViewState["STDNO"].ToString());
            }
            SP_Name = "PKG_ADM_CRUDE_BRANCH_STUDY_DIPLOMA";
            SP_Call = "@P_MODE,@P_DEGREENO,@P_BRANCH_STUDY,@P_ACTIVESTATUS,@P_CREATED_MODIFIED_BY,@P_IP_ADDRESS,@P_STD_NO";
            SP_Value = "" + mode + "," + degreeNo + "," + branchStudy + "," + activeStatus + "," + create_Modify + "," + ipAddress + "," + stdNo + "," +"";
            dsSubmit = objCommon.DynamicSPCall_Select(SP_Name, SP_Call, SP_Value);
            if (dsSubmit.Tables[0].Rows.Count > 0)
            {
                if (dsSubmit.Tables[0].Rows[0]["OUTPUT"].ToString().Equals("1"))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert(`Record saved successfully.`)", true);
                    BindList();
                    clearField();
                    return;
                }
                else if (dsSubmit.Tables[0].Rows[0]["OUTPUT"].ToString().Equals("2"))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert(`Record updated successfully.`)", true);
                    BindList();
                    clearField();
                    return;
                }
                else if (dsSubmit.Tables[0].Rows[0]["OUTPUT"].ToString().Equals("99"))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert(`Record already exists.`)", true);
                    BindList();
                    return;
                }
            }
        }
        catch (Exception)
        {
            
            throw;
        }
    }
    protected void clearField()
    {
        try
        {
            ddlDegree.SelectedIndex = 0;
            txtBranchStudy.Text = string.Empty;
            ViewState["MODE"] = null;
            ViewState["STDNO"] = null;
        }
        catch (Exception)
        {
            
            throw;
        }
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            DataSet dsEdit = null;
            ImageButton imgBtn = sender as ImageButton;
            stdNo =Convert.ToInt32(imgBtn.CommandArgument.ToString());
            ViewState["STDNO"] = stdNo;
            mode = "EDIT";
            SP_Name = "PKG_ADM_CRUDE_BRANCH_STUDY_DIPLOMA";
            SP_Call = "@P_MODE,@P_DEGREENO,@P_BRANCH_STUDY,@P_ACTIVESTATUS,@P_CREATED_MODIFIED_BY,@P_IP_ADDRESS,@P_STD_NO";
            SP_Value = "" + mode + "," + degreeNo + "," + branchStudy + "," + activeStatus + "," + create_Modify + "," + ipAddress + "," + stdNo + "," +"";
            dsEdit = objCommon.DynamicSPCall_Select(SP_Name, SP_Call, SP_Value);
            if (dsEdit.Tables[0].Rows.Count > 0)
            {
                ddlDegree.SelectedValue = dsEdit.Tables[0].Rows[0]["DEGREENO"].ToString();
                txtBranchStudy.Text = dsEdit.Tables[0].Rows[0]["NAME_OF_STUDY"].ToString();
                if (dsEdit.Tables[0].Rows[0]["ACTIVESTATUS"].ToString().Equals("False"))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src1", "SetStatActive(false);", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src1", "SetStatActive(true);", true);
                }
                ViewState["MODE"] = "EDIT";
            }
        }
        catch (Exception)
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
            SP_Name = "PKG_ADM_CRUDE_BRANCH_STUDY_DIPLOMA";
            SP_Call = "@P_MODE,@P_DEGREENO,@P_BRANCH_STUDY,@P_ACTIVESTATUS,@P_CREATED_MODIFIED_BY,@P_IP_ADDRESS,@P_STD_NO";
            SP_Value = "" + mode + "," + degreeNo + "," + branchStudy + "," + activeStatus + "," + create_Modify + "," + ipAddress + "," + stdNo + "," + "";
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
}
