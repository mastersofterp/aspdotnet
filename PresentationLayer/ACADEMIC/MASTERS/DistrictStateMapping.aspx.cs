using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;
using System.Data;
public partial class ACADEMIC_MASTERS_DistrictStateMapping : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    OnlineAdmissionController Admcontroller = new OnlineAdmissionController();
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
                    CheckPageAuthorization();
                    PopulateDropDown();
                    //Set the Page Title
                    //Page.Title = Session["coll_name"].ToString();
                    ////Load Page Help
                    //if (Request.QueryString["pageno"] != null)
                        
                        BindState_Dist();
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void PopulateDropDown()
    {
        objCommon.FillDropDownList(ddlState, "ACD_STATE", "STATENO", "STATENAME", "STATENO > 0 AND ACTIVESTATUS=1", "STATENAME");
        ddlState.Focus();
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=DistrictStateMapping.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=DistrictStateMapping.aspx");
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int status = 0;
            if (hfdStat.Value == "true")
            {
                status = 1;
            }
            else
            {
                status = 0;
            }
            if (btnSubmit.Text.Trim().Equals("Submit"))
            {
                CustomStatus cs =(CustomStatus) Admcontroller.Add_State_District(Convert.ToInt32(ddlState.SelectedValue), txtDistrict.Text.ToString().Trim(), status);
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    objCommon.DisplayMessage(updDist, "Record Saved Successfully.", this.Page);
                    ClearFields();
                    BindState_Dist();
                }
                else if(cs.Equals(CustomStatus.RecordExist))
                {
                    objCommon.DisplayMessage(updDist, "Record Already Exists..", this.Page);
                }
            }
            else if (btnSubmit.Text.Trim().Equals("Update"))
            {
                CustomStatus cs =(CustomStatus) Admcontroller.Update_State_Dsitrict(Convert.ToInt32(ViewState["district"]), Convert.ToInt32(ddlState.SelectedValue), txtDistrict.Text.ToString().Trim(), status);
                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    objCommon.DisplayMessage(updDist, "Record Updated Successfully.", this.Page);
                    ClearFields();
                    BindState_Dist();
                }
            }
            else
            {
                objCommon.DisplayMessage(updDist, "Server Unavailable.", this.Page);
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            ddlState.SelectedIndex = -1;
            txtDistrict.Text = string.Empty;

        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void BindState_Dist()
    {
        DataSet ds = null;
        if (btnSubmit.Text.Equals("Update"))
        {
            ds = Admcontroller.GetState_Dist(Convert.ToInt32(ViewState["district"]), 0);
            ddlState.SelectedValue = ds.Tables[0].Rows[0]["STATENO"].ToString();
            txtDistrict.Text = ds.Tables[0].Rows[0]["DISTRICTNAME"].ToString().Equals(string.Empty) ? "" : ds.Tables[0].Rows[0]["DISTRICTNAME"].ToString();
            if (ds.Tables[0].Rows[0]["ACTIVESTATUS"].ToString().Equals("1"))
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "Src", "SetStat(true);", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "Src", "SetStat(false);", true);
            }
        }
        else
        {
            ds = Admcontroller.GetState_Dist(0,0);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvDistrict.DataSource = ds;
                lvDistrict.DataBind();
                lvDistrict.Visible = true;
            }
            else
            {
                lvDistrict.DataSource = null;
                lvDistrict.DataBind();
                lvDistrict.Visible = false;
            }
        }

    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton imgButton = sender as ImageButton;
            int district = Convert.ToInt32(imgButton.CommandArgument.ToString());
            ViewState["district"] = district;
            btnSubmit.Text = "Update";
            BindState_Dist();
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlState.SelectedIndex > 0)
            {
                DataSet ds = Admcontroller.GetState_Dist(0, Convert.ToInt32(ddlState.SelectedValue));
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        lvDistrict.DataSource = ds;
                        lvDistrict.DataBind();
                        lvDistrict.Visible = true;
                    }
                    else
                    {
                        lvDistrict.DataSource = null;
                        lvDistrict.DataBind();
                        lvDistrict.Visible = false;
                    }
                }
                else
                {
                    lvDistrict.DataSource = null;
                    lvDistrict.DataBind();
                    lvDistrict.Visible = false;
                }
                txtDistrict.Focus();
            }
            else
            {
                objCommon.DisplayMessage(updDist, "Please Select State.", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {
            throw;
        }
        
    }
    protected void ClearFields()
    {
        ddlState.SelectedIndex = -1;
        txtDistrict.Text = string.Empty;
        btnSubmit.Text = "Submit";
    }
}