/*
 Added by Nikhil L.
 Created Date : 12/12/2022 
 Used in PhD Module.
 Feature : Use to add or update the external member for PHD.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Data;
public partial class ACADEMIC_PHD_PhD_Add_External_Member : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    PhdController objPhDController = new PhdController();
    string sp_Name = string.Empty; string sp_Parameters = string.Empty; string call = string.Empty;
    string name = string.Empty; string inst_Name = string.Empty; string mobile = string.Empty; string emailId = string.Empty;
    string designation = string.Empty; int createdBy = 0; string ipAddress = string.Empty; int desig = 0; int modified = 0;
    string mode = string.Empty; int outPut = 0;
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
                //Page Authorization
                CheckPageAuthorization();
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                }
            }
            GetExtMembers();
            ViewState["mode"] = string.Empty;
        }

    }
    #region Check Authorization
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=PhD_Add_External_Member.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=PhD_Add_External_Member.aspx");
        }
    }
    #endregion Check Authorization
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet dsMember = null;
            name = txtName.Text.ToString();
            inst_Name = txtInstituteName.Text.ToString();
            mobile = txtMobile.Text.ToString();
            emailId = txtEmail.Text.ToString();
            designation = txtDesig.Text.ToString();
            createdBy=Convert.ToInt32(Session["userno"].ToString());
            ipAddress = Request.ServerVariables["REMOTE_ADDR"];
            desig = 0;
            if (ViewState["mode"].ToString().Equals(string.Empty))
            {
                mode = "INSERT";
                desig = 0;
            }
            else
            {
                mode = ViewState["mode"].ToString();
                desig =Convert.ToInt32(ViewState["desigNo"].ToString());
            }
            sp_Name = "PKG_ACD_INS_UPD_GET_PHD_EXT_MEMBER";
            sp_Parameters = "@P_NAME,@P_INST_NAME,@P_MOBILE_NO,@P_EMAILID,@P_DESIGN,@P_UA_NO,@P_IP_ADDRESS,@P_DESIG_NO,@P_MODE,@P_OUTPUT";
            call = "" + name + "," + inst_Name + "," + mobile + "," + emailId + "," + designation + "," + createdBy + "," + ipAddress + "," + desig + "," + mode + "," + outPut + "";
            dsMember = objCommon.DynamicSPCall_Select(sp_Name, sp_Parameters, call);
            if (dsMember.Tables[0].Rows.Count > 0)
            {
                if (dsMember.Tables[0].Rows[0]["OUTPUT"].ToString().Equals("0"))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert(`External member already exists.`)", true);
                }
                else if (dsMember.Tables[0].Rows[0]["OUTPUT"].ToString().Equals("1"))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert(`External member added successfully.`)", true);
                    clearField();
                    GetExtMembers();
                }
                else if (dsMember.Tables[0].Rows[0]["OUTPUT"].ToString().Equals("2"))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert(`External member updated successfully.`)", true);
                    clearField();
                    GetExtMembers();
                }
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
            ImageButton imgEdit = sender as ImageButton;
            desig =Convert.ToInt32(imgEdit.CommandArgument);
            DataSet dsEdit = null;
            mode = "EDIT";
            sp_Name = "PKG_ACD_INS_UPD_GET_PHD_EXT_MEMBER";
            sp_Parameters = "@P_NAME,@P_INST_NAME,@P_MOBILE_NO,@P_EMAILID,@P_DESIGN,@P_UA_NO,@P_IP_ADDRESS,@P_DESIG_NO,@P_MODE,@P_OUTPUT";
            call = "" + name + "," + inst_Name + "," + mobile + "," + emailId + "," + designation + "," + createdBy + "," + ipAddress + "," + desig + "," + mode + "," + outPut + "";
            dsEdit = objCommon.DynamicSPCall_Select(sp_Name, sp_Parameters, call);
            if (dsEdit.Tables[0].Rows.Count > 0)
            {
                txtName.Text = dsEdit.Tables[0].Rows[0]["NAME"].ToString();
                txtInstituteName.Text = dsEdit.Tables[0].Rows[0]["INSTITIUTE_NAME"].ToString();
                txtMobile.Text = dsEdit.Tables[0].Rows[0]["MOBILE_NO"].ToString();
                txtEmail.Text = dsEdit.Tables[0].Rows[0]["EMAIL_ID"].ToString();
                txtDesig.Text = dsEdit.Tables[0].Rows[0]["DESIGNATION"].ToString();
                ViewState["mode"] = "Update";
                ViewState["desigNo"] = desig;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert(`Record does not exists.`)", true);
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void GetExtMembers()
    {
        try
        {
            DataSet dsGet= null;
            desig = 0;
            mode = "GET";
            sp_Name = "PKG_ACD_INS_UPD_GET_PHD_EXT_MEMBER";
            sp_Parameters = "@P_NAME,@P_INST_NAME,@P_MOBILE_NO,@P_EMAILID,@P_DESIGN,@P_UA_NO,@P_IP_ADDRESS,@P_DESIG_NO,@P_MODE,@P_OUTPUT";
            call = "" + name + "," + inst_Name + "," + mobile + "," + emailId + "," + designation + "," + createdBy + "," + ipAddress + "," + desig + "," + mode + "," + outPut + "";
            dsGet = objCommon.DynamicSPCall_Select(sp_Name, sp_Parameters, call);
            if (dsGet.Tables[0].Rows.Count > 0)
            {
                lvMember.DataSource = dsGet;
                lvMember.DataBind();
                pnlMember.Visible = true;
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void clearField()
    {
        txtName.Text = string.Empty;
        txtInstituteName.Text = string.Empty;
        txtMobile.Text = string.Empty;
        txtEmail.Text = string.Empty;
        txtDesig.Text = string.Empty;
        ViewState["mode"] = string.Empty;
        ViewState["desigNo"] = string.Empty;
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            clearField();
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}