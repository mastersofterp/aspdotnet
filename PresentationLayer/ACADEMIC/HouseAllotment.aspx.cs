using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ACADEMIC_HouseAllotment : System.Web.UI.Page
{
    Common objCommon = new Common();
    CollegeController objColgCon = new CollegeController();
    College objcolg = new College();

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
            }
            BindListView();
            ViewState["Action"] = "add";
        }
    }


    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=HouseAllotment.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=HouseAllotment.aspx");
        }
    }


    #region Methods
    private void BindListView()
    {
        try
        {
            DataSet ds = objColgCon.GetHouseAllotedData(0);
            lvHouseAllotment.DataSource = ds;
            lvHouseAllotment.DataBind();
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void ShowDetailDept(int ha_idno)
    {
        DataSet ds = null;
        ds = objColgCon.GetHouseAllotedData(ha_idno);
        if (ds.Tables != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["HA_ID"] = ha_idno.ToString();
                txtHouseId.Text = ds.Tables[0].Rows[0]["HOUSE_ID"] == null ? string.Empty : ds.Tables[0].Rows[0]["HOUSE_ID"].ToString();
                txtEitherValue.Text = ds.Tables[0].Rows[0]["EITHER_VALUE"] == null ? string.Empty : ds.Tables[0].Rows[0]["EITHER_VALUE"].ToString();

                txtOrValue.Text = ds.Tables[0].Rows[0]["OR_VALUE"] == null ? string.Empty : ds.Tables[0].Rows[0]["OR_VALUE"].ToString();
                txtHouseName.Text = ds.Tables[0].Rows[0]["HOUSE_NAME"] == null ? string.Empty : ds.Tables[0].Rows[0]["HOUSE_NAME"].ToString();
                txtHouseCode.Text = ds.Tables[0].Rows[0]["HOUSE_CODE"] == null ? string.Empty : ds.Tables[0].Rows[0]["HOUSE_CODE"].ToString();

                txtColour.Text = ds.Tables[0].Rows[0]["COLOUR"] == null ? string.Empty : ds.Tables[0].Rows[0]["COLOUR"].ToString();



                if (ds.Tables[0].Rows[0]["ACTIVE_STATUS"].ToString() == "ACTIVE")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStat(true);", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStat(false);", true);
                }
            }
        }
    }

    private void ClearAll()
    {
        txtHouseId.Text = string.Empty;
        txtEitherValue.Text = string.Empty;
        txtOrValue.Text = string.Empty;
        txtHouseName.Text = string.Empty;
        txtHouseCode.Text = string.Empty;
        txtColour.Text = string.Empty;
        ViewState["HA_ID"] = null;
    }
    #endregion Methods

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int activeStatus = 0;
            objcolg.House_Id = Convert.ToInt32(txtHouseId.Text);
            objcolg.Either_Val = Convert.ToInt32(txtEitherValue.Text);
            objcolg.Or_Val = Convert.ToInt32(txtOrValue.Text);
            objcolg.HouseName = txtHouseName.Text;
            objcolg.HouseCode = txtHouseCode.Text;
            objcolg.Colour = txtColour.Text;

            if (hfdActive.Value == "true")
            {
                objcolg.Active_Status = 1;
            }
            else
            {
                objcolg.Active_Status = 0;
            }

            if (ViewState["HA_ID"] != null)
            {
                objcolg.Ha_Id = Convert.ToInt32(ViewState["HA_ID"]);
            }
            CustomStatus cs = (CustomStatus)objColgCon.SaveHouseAllotment(objcolg);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayUserMessage(this.updHouse, "Record Saved Successfully!", this.Page);
                BindListView();
                ClearAll();
            }
            else if (cs.Equals(CustomStatus.RecordUpdated))
            {
                objCommon.DisplayUserMessage(this.updHouse, "Record Updated Successfully!", this.Page);
                BindListView();
                ClearAll();
            }
            else
            {
                objCommon.DisplayUserMessage(this.updHouse, "Record already exist!", this.Page);
            }
        }
        catch
        {
            throw;
        }

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            ClearAll();
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
            ImageButton btnEdit = sender as ImageButton;
            int ha_idno = int.Parse(btnEdit.CommandArgument);
            ShowDetailDept(ha_idno);
            ViewState["Action"] = "Edit";
        }
        catch (Exception ex)
        {
            throw;
        }
    }

}