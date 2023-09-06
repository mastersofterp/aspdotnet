//=================================================================================
// PROJECT NAME  : PERSONNEL REQUIREMENT MANAGEMENT                                
// MODULE NAME   : UNLOCK USER APPLICATION                                      
// CREATION DATE : 
// CREATED BY    : MANISH CHAWADE
// MODIFIED BY   : 
// MODIFIED DESC : 
//=================================================================================
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
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;
public partial class ACADEMIC_UnlockUserApplication : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    applicationReceivedController objnuc = new applicationReceivedController();
    OnlineAdmissionController objOA = new OnlineAdmissionController();
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
            if (Session["userno"] == null && Session["username"] == null &&
                Session["usertype"] == null && Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }

            //Page Authorization
            CheckPageAuthorization();
            //Set the Page Title
            Page.Title = Session["coll_name"].ToString();

        }

    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=UnlockUserApplication.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=UnlockUserApplication.aspx");
        }
    }

    private void clear()
    {
        txtUserName.Text = string.Empty;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        this.clear();
        pnlDetails.Visible = false;
    }

    protected void btnUnlock_Click(object sender, EventArgs e)
    {
        int USERNO = Convert.ToInt32(objCommon.LookUp("ACD_USER_REGISTRATION", "USERNO", "USERNAME='" + txtUserName.Text.Trim() + "'"));

        int Check = Convert.ToInt32(objCommon.LookUp("ACD_USER_PROFILE_STATUS", "COUNT(USERNO)", "CONFIRM_STATUS = 1 AND USERNO= " + USERNO));

        if (Check == 0)
        {
            objCommon.DisplayMessage(this.updAppliid, "Application already Unlocked.", this.Page);
            return;
        }

        CustomStatus cs = (CustomStatus)objnuc.UnlockStudentApplication(USERNO);

        if (cs.Equals(CustomStatus.RecordUpdated))
        {
            objCommon.DisplayMessage(this.updAppliid, "Application Unlocked Successfully.", this.Page);
            ClearFields();
            pnlDetails.Visible = false;
        }
        else
            objCommon.DisplayMessage(this.updAppliid, "Application Failed to Unlock.", this.Page);
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        this.ShowInfo();
    }
    protected void ClearFields()
    {
        txtUserName.Text = string.Empty;
    }
    private void ShowInfo()
    {
        try
        {
            if (String.IsNullOrEmpty(txtUserName.Text.Trim()))
            {
                objCommon.DisplayMessage(this.updAppliid, "Please Enter Correct Application ID.", this.Page);
                return;
            }
            DataSet dsCheck = objOA.GetConfirmStatus_FromUsername(txtUserName.Text.ToString().Trim());
            if (dsCheck.Tables[0].Rows.Count > 0)
            {
                if (dsCheck.Tables[0].Rows[0]["CONFIRM_STATUS"].ToString().Equals("1"))
                {
                    int USERNO = Convert.ToInt32(objCommon.LookUp("ACD_USER_REGISTRATION", "USERNO", "USERNAME='" + txtUserName.Text.Trim() + "'"));
                    ViewState["userno"] = USERNO;
                    DataSet ds = null;
                    ds = objnuc.GetExstStudentDetailsByApplicationID(txtUserName.Text.Trim());
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        lblStudentname.Text = ds.Tables[0].Rows[0]["STUDENTNAME"].ToString();
                        lblDateOfBirth.Text = ds.Tables[0].Rows[0]["DOB"].ToString();
                        lblAddress.Text = ds.Tables[0].Rows[0]["ADDRESS"].ToString();
                        lblEmail.Text = ds.Tables[0].Rows[0]["EMAILID"].ToString();
                        lblMobile.Text = ds.Tables[0].Rows[0]["MOBILENO"].ToString();
                        lblPinCode.Text = ds.Tables[0].Rows[0]["PINCODE"].ToString();
                        lblStatus.Text = ds.Tables[0].Rows[0]["STATUS"].ToString();
                        lblDegree.Text = ds.Tables[0].Rows[0]["DEGREE"].ToString();
                        if (lblStatus.Text.ToString().Equals("LOCKED"))
                        {
                            lblStatus.ForeColor = System.Drawing.Color.Red;
                        }
                        else
                        {
                            lblStatus.ForeColor = System.Drawing.Color.Green;
                        }
                        pnlDetails.Visible = true;
                        btnUnlock.Enabled = true;
                    }
                }
                else
                {
                    objCommon.DisplayMessage(this.Page, "User " + txtUserName.Text.Trim() + " is Already Unlocked.", this.Page);
                    btnUnlock.Enabled = false;
                    pnlDetails.Visible = false;
                    return;
                }
            }


            else
            {
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

}