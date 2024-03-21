using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Data;
public partial class ACADEMIC_MASTERS_State_District_Mapping : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ConfigController objConfig = new ConfigController();
    string sp_Name = string.Empty; string sp_Parameters = string.Empty; string call = string.Empty; bool activeStatus = false;
    string mode = string.Empty; string district = string.Empty; int districtNo = 0; int stateNo = 0; string college_Code = string.Empty; int output = 0;
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
                CheckPageAuthorization();
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                }
            }
            BindState_District();
            ViewState["add"] = "add";
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
                Response.Redirect("~/notauthorized.aspx?page=State_District_Mapping.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=State_District_Mapping.aspx");
        }
    }
    #endregion Check Authorization
    protected void BindState_District()
    {
        DataSet dsState = null;
        mode = "select";
        districtNo = 0;
        sp_Name = "PKG_ACD_STATE_DISTRICT_MAPPING_MASTER";
        sp_Parameters = "@P_MODE,@P_STATENO,@P_DISTRICTNO";
        call = "" + mode + "," + stateNo + "," + districtNo + "";
        ddlState.Items.Clear();
        ddlState.Items.Add(new ListItem("Please Select", "0"));
        dsState = objCommon.DynamicSPCall_Select(sp_Name, sp_Parameters, call);
        if (dsState.Tables[0].Rows.Count > 0)
        {
            ddlState.DataSource = dsState.Tables[0];
            ddlState.DataTextField = "STATENAME";
            ddlState.DataValueField = "STATENO";
            ddlState.DataBind();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(`Oops something went wrong!`)", true);
            return;
        }
        if (dsState.Tables[1].Rows.Count > 0)
        {
            lvDistrict.DataSource = dsState.Tables[1];
            lvDistrict.DataBind();
            pnlDistrict.Visible = true;
        }
        else
        {
            lvDistrict.DataSource = null;
            lvDistrict.DataBind();
            pnlDistrict.Visible = false;
        }
        ddlState.SelectedValue = stateNo.ToString(); 
    }
    protected void ddlState_TextChanged(object sender, EventArgs e)
    {
        try
        {
            txtDistrict.Text = string.Empty;
            DataSet dsStateWise = null;
            mode = "select";
            districtNo = 0;
            stateNo = ddlState.SelectedIndex > 0 ? Convert.ToInt32(ddlState.SelectedValue) : 0;
            sp_Name = "PKG_ACD_STATE_DISTRICT_MAPPING_MASTER";
            sp_Parameters = "@P_MODE,@P_STATENO,@P_DISTRICTNO";
            call = "" + mode + "," + stateNo + "," + districtNo + "";
            dsStateWise = objCommon.DynamicSPCall_Select(sp_Name, sp_Parameters, call);
            if (dsStateWise.Tables[1].Rows.Count > 0)
            {
                lvDistrict.DataSource = dsStateWise.Tables[1];
                lvDistrict.DataBind();
                pnlDistrict.Visible = true;
            }
            else
            {
                lvDistrict.DataSource = null;
                lvDistrict.DataBind();
                pnlDistrict.Visible = false;
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {

            string DistrictName = objCommon.LookUp("ACD_DISTRICT", "DISTRICTNAME", "UPPER(DISTRICTNAME)='"+txtDistrict.Text.ToUpper().Trim()+"'");
            //if (DistrictName != string.Empty)
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(`District already exists.`)", true);
            //    return;
            //}
            
            if (ViewState["add"].ToString().Equals("add"))
            {
                mode = "insert";
                districtNo = 0;
            }
            else
            {
                mode = "update";
                districtNo = Convert.ToInt32(ViewState["district"].ToString());
            }
            stateNo = ddlState.SelectedIndex > 0 ?Convert.ToInt32(ddlState.SelectedValue): 0;
            district = txtDistrict.Text.ToString().Equals(string.Empty) ? "-" : txtDistrict.Text.ToString().Trim();
            college_Code = Session["colcode"].ToString();
            if (hfdDist.Value == "true")
            {
                activeStatus = true;
            }
            else
            {
                activeStatus = false;
            }
            output=objConfig.State_District_Mapping(mode, districtNo, district, stateNo, college_Code, activeStatus);
            if (output == 1)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(`Record saved successfully.`)", true);
                txtDistrict.Text = string.Empty;
                BindState_District();
            }
            else if (output == 2)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(`Record updated successfully.`)", true);
                txtDistrict.Text = string.Empty;
                BindState_District();
                clearsession();
            }
            else if (output == 2627)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(`Record already exists.`)", true);
                BindState_District();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(`Something went wrong.`)", true);
                txtDistrict.Text = string.Empty;
                BindState_District();
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        BindState_District();
        txtDistrict.Text = string.Empty;
        ViewState["add"] = "add";
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            districtNo = Convert.ToInt32(btnEdit.CommandArgument);
            DataSet dsEdit = null;
            mode = "edit";
            //stateNo = ddlState.SelectedIndex > 0 ? Convert.ToInt32(ddlState.SelectedValue) : 0;
            sp_Name = "PKG_ACD_STATE_DISTRICT_MAPPING_MASTER";
            sp_Parameters = "@P_MODE,@P_STATENO,@P_DISTRICTNO";
            call = "" + mode + "," + stateNo + "," + districtNo + "";
            dsEdit = objCommon.DynamicSPCall_Select(sp_Name, sp_Parameters, call);
            ddlState.SelectedValue = dsEdit.Tables[0].Rows[0]["STATENO"].ToString();
            txtDistrict.Text = dsEdit.Tables[0].Rows[0]["DISTRICTNAME"].ToString();
            if (dsEdit.Tables[0].Rows[0]["ACTIVESTATUS"].ToString() == "Active")
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "Src", "SetActive(true);", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "Src", "SetActive(false);", true);
            }
            ViewState["add"] = "edit";
            ViewState["district"] = districtNo;
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void clearsession()
    {
        ViewState["add"] = "add";
    }
}