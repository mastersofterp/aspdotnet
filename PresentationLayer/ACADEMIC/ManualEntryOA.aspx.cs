/*
 Created By         : Nikhil Lambe
 Created Date     : 09-02-2023
 Description        : To get student for manual fee entry for admission portal.
*/
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
public partial class ACADEMIC_ManualEntryOA : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    OnlineAdmissionController Admcontroller = new OnlineAdmissionController();
    string spName = string.Empty; string spParameters = string.Empty; string spValue = string.Empty;
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
                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();
                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                    }
                }

            }
            else
            {
                ViewState["DONE"] = 0;
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
                Response.Redirect("~/notauthorized.aspx?page=ManualEntryOA.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=ManualEntryOA.aspx");
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            lblName.Text = string.Empty;
            lblEmail.Text = string.Empty;
            lblMobile.Text = string.Empty;
            lblDegree.Text = string.Empty;
            lblFees.Text = string.Empty;
            lblPayStatus.Text = string.Empty;
            if (!txtAppId.Text.ToString().Equals(string.Empty))
            {
                string appId = string.Empty;
                appId = txtAppId.Text.ToString().TrimEnd();
                spName = "PKG_ACD_OA_GET_SEARCH_USER_FOR_MANUAL_ENTRY";
                spParameters = "@P_APPID";
                spValue = "" + appId + "";
                DataSet dsGet = null;
                dsGet = objCommon.DynamicSPCall_Select(spName, spParameters, spValue);
                if (dsGet.Tables[0].Rows.Count > 0)
                {
                    divDetails.Visible = true;
                    lblName.Text = dsGet.Tables[0].Rows[0]["FIRSTNAME"].ToString().Equals(string.Empty) ? "-" : dsGet.Tables[0].Rows[0]["FIRSTNAME"].ToString();
                    lblEmail.Text = dsGet.Tables[0].Rows[0]["EMAILID"].ToString().Equals(string.Empty) ? "-" : dsGet.Tables[0].Rows[0]["EMAILID"].ToString();
                    lblMobile.Text = dsGet.Tables[0].Rows[0]["MOBILENO"].ToString().Equals(string.Empty) ? "-" : dsGet.Tables[0].Rows[0]["MOBILENO"].ToString();
                    lblDegree.Text = dsGet.Tables[0].Rows[0]["DEGREE"].ToString().Equals(string.Empty) ? "-" : dsGet.Tables[0].Rows[0]["DEGREE"].ToString();
                    lblFees.Text = dsGet.Tables[0].Rows[0]["FEES"].ToString().Equals(string.Empty) ? "-" : dsGet.Tables[0].Rows[0]["FEES"].ToString();
                    lblPayStatus.Text = dsGet.Tables[0].Rows[0]["PAY_STATUS"].ToString().Equals(string.Empty) ? "-" : dsGet.Tables[0].Rows[0]["PAY_STATUS"].ToString();
                    ViewState["AMOUNT"] = dsGet.Tables[0].Rows[0]["FEES"].ToString();
                    ViewState["USERNO"] = dsGet.Tables[0].Rows[0]["USERNO"].ToString();
                    if (lblPayStatus.Text.ToString().Equals("Done"))
                    {
                        if(ViewState["DONE"].ToString()=="0")
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert(`Payment already done for these user.`)", true);
                        lblPayStatus.ForeColor = System.Drawing.Color.Green;
                        btnSubmit.Visible = false;
                    }
                    else
                    {                        
                        lblPayStatus.ForeColor = System.Drawing.Color.Red;
                        btnSubmit.Visible = true;
                    }
                    btnCancel.Visible = true;
                }
                else
                {
                    divDetails.Visible = false;
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert(`No record found.`)", true);
                    btnCancel.Visible = false;
                    return;
                }
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
            int userNo =Convert.ToInt32(ViewState["USERNO"].ToString());
            decimal amount=Convert.ToDecimal(ViewState["AMOUNT"].ToString());
            spName = "PKG_ACD_OA_UPDATE_MANUAL_ENTRY";
            spParameters = "@P_USERNO,@P_AMOUNT";
            spValue = "" + userNo + "," + amount + "";
            DataSet dsPay = null;
            dsPay = objCommon.DynamicSPCall_Select(spName, spParameters, spValue);
            if (dsPay.Tables[0].Rows.Count > 0)
            {
                if (dsPay.Tables[0].Rows[0]["OUTPUT"].ToString().Equals("1"))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert(`Manual payment done successfully.`)", true);
                    dsPay = null;
                    userNo =0;
                    amount = 0;
                    spName = "";
                    spParameters = "";
                    spValue = "";
                    ViewState["DONE"] = 1;
                    btnSearch_Click(sender, e);
                    return;
                }
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
            lblName.Text = string.Empty;
            lblEmail.Text = string.Empty;
            lblMobile.Text = string.Empty;
            lblDegree.Text = string.Empty;
            lblFees.Text = string.Empty;
            lblPayStatus.Text = string.Empty;
            txtAppId.Text = string.Empty;
            btnSubmit.Visible = false;
            divDetails.Visible = false;
            btnCancel.Visible = false;
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}