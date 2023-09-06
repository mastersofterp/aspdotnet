
//=================================================================================
// PROJECT NAME  : UAIMS                                                           
// MODULE NAME   : PAYMENT GROUP ENTRY                                                    
// CREATION DATE : 16-August-2015                                               
// CREATED BY    : NAKUL CHAWRE                                                
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
using System.Text.RegularExpressions;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

using System.Data.SqlClient;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;
using IITMS.NITPRM;

public partial class Acc_PaymentType : System.Web.UI.Page
{
    AccountPaymentTypeController objAccPaymentTypeController = new AccountPaymentTypeController();
    AccountPaymentType objAccPayment = new AccountPaymentType();
    Common objCommon = new Common();
    CustomStatus CS = new CustomStatus();
    static int GroupID = 0;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["CollegeId"] != null || Session["UserName"] != null || Session["Password"] != null || Session["DataBase"] != null)
        {
            objCommon = new Common();
        }
        else
        {
            Response.Redirect("~/Default.aspx");
        }
        objCommon = new Common();
        Page.Title = Session["coll_name"].ToString();
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
                if (Session["comp_code"] == null)
                {
                    Session["comp_set"] = "NotSelected";
                    //objCommon.DisplayUserMessage(updBank, "Select company/cash book.", this);
                    Response.Redirect("~/Account/selectCompany.aspx");
                }
                else
                {
                    Session["comp_set"] = "";
                    //Page Authorization
                    //CheckPageAuthorization();
                    divCompName.InnerHtml = Session["comp_name"].ToString().ToUpper();
                    
                }
            }
            ViewState["action"] = "add";
            fillPartyList();
            FillPartyDetails();
        }

    }

    public void DisplayMessage(Control UpdatePanelId, string msg, Page pg)
    {
        string prompt = "<script>$(document).ready(function(){{$.prompt('{0}!');}});</script>";
        string message = string.Format(prompt, msg);

        ScriptManager.RegisterClientScriptBlock(UpdatePanelId, UpdatePanelId.GetType(), "Message", message, false);
    }

    #region Private Methods

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["obj"] != null)
        {
            if (Request.QueryString["obj"].ToString().Trim() != "config")
            {
                if (Request.QueryString["pageno"] != null)
                {
                    //Check for Authorization of Page
                    if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
                    {
                        Response.Redirect("~/notauthorized.aspx?page=Acc_PaymentType.aspx");
                    }
                }
                else
                {
                    //Even if PageNo is Null then, don't show the page
                    Response.Redirect("~/notauthorized.aspx?page=Acc_PaymentType.aspx");
                }
            }
        }
        else
        {
            if (Request.QueryString["pageno"] != null)
            {
                //Check for Authorization of Page
                if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
                {
                    Response.Redirect("~/notauthorized.aspx?page=Acc_PaymentType.aspx");
                }
            }
            else
            {
                //Even if PageNo is Null then, don't show the page
                Response.Redirect("~/notauthorized.aspx?page=Acc_PaymentType.aspx");
            }
        }
    }

    //Fill Payment Party Listview with Data
    private void fillPartyList()
    {

        DataSet ds = objAccPaymentTypeController.FillPartyList();
        if (ds != null)
        {
            lvPaymentParty.DataSource = ds;
            lvPaymentParty.DataBind();
        }
        else
        {
            lvPaymentParty.DataSource = null;
            lvPaymentParty.DataBind();
        }
    }

    //Fill Payment Party Details after Submitting
    private void FillPartyDetails()
    {

        DataSet ds = objAccPaymentTypeController.FillPartyDetails();
        if (ds != null)
        {
            lvPartyDetails.DataSource = ds;
            lvPartyDetails.DataBind();
        }
        else
        {
            lvPartyDetails.DataSource = null;
            lvPartyDetails.DataBind();
        }
    }

    //Clear Control Values
    private void ClearControl()
    {
        txtGrpName.Text = string.Empty;
        btnSubmit.Text = "Submit";
        foreach (ListViewDataItem lvItem in lvPaymentParty.Items)
        {
            CheckBox chkBox = lvItem.FindControl("ChkPayment") as CheckBox;
            chkBox.Checked = false;
        }
        //CheckBox chkBox = lvPaymentParty.FindControl("ChkPayment") as CheckBox;
        //chkBox.Checked = false;
    }

    #endregion Private Methods

    #region Page Events

    //Fetch Data for Updating
    protected void ImageButtonEdit_Click(object sender, EventArgs e)
    {
        try
        {
            btnSubmit.Text = "Update Account";
            //btnSubmit.Style.Add(HtmlTextWriterStyle.Color, "red");
            ImageButton btnEdit = sender as ImageButton;
            GroupID = int.Parse(btnEdit.CommandArgument);
            ViewState["action"] = GroupID;
            DataSet ds = null;
            ds = objAccPaymentTypeController.GetPartyDetails(GroupID);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtGrpName.Text = ds.Tables[0].Rows[0]["GROUPNAME"].ToString();
                    string PartyNo = ds.Tables[0].Rows[0]["PAYTYPENO"].ToString();
                    string[] Party = PartyNo.Split(',');

                    foreach (string item in Party)
                    {
                        foreach (ListViewDataItem lvItem in lvPaymentParty.Items)
                        {
                            CheckBox chkBox = lvItem.FindControl("ChkPayment") as CheckBox;
                            if (chkBox.ToolTip == item)
                            {
                                chkBox.Checked = true;
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "AbstractBillAccounts.ImageButtonEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    //Insert or Update Party Payment Data
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int ret = 0;
            string GroupName = txtGrpName.Text;
            int USERID = Convert.ToInt32(Session["userno"].ToString());
            //int COLLEGEID = Convert.ToInt32(Session["CollegeId"].ToString());
            int COLLEGEID = Convert.ToInt32(Session["comp_code"].ToString());
            String PartyName = string.Empty;

            foreach (ListViewDataItem lvItem in lvPaymentParty.Items)
            {
                CheckBox chkBox = lvItem.FindControl("ChkPayment") as CheckBox;
                if (chkBox.Checked == true)
                {
                    if (PartyName == string.Empty)
                        PartyName = chkBox.ToolTip;
                    else
                        PartyName = PartyName + "," + chkBox.ToolTip;
                }
            }

            if (PartyName == string.Empty)
            {
                objCommon.DisplayUserMessage(updBank, "Please select at least one payment type", this.Page);
                return;
            }
            //int ret = 0;
            //objAccPayment.PGROUP_NO = Convert.ToInt32(ViewState["REQTRNO"].ToString());
            //objAccPayment.GROUPNAME = txtGrpName.Text;
            //objAccPayment.PAYTYPENO = PartyName;
            //objAccPayment.USERID = Convert.ToInt32(Session["userno"].ToString());
            //objAccPayment.COLLEGE_ID = Convert.ToInt32(Session["USERID"].ToString());

            int look = Convert.ToInt32(objCommon.LookUp("acc_" + Session["comp_code"].ToString() + "_PAYMENT_GROUP", "count(1)", "GROUPNAME='" + GroupName + "'"));
            if (look == 1)
            {
                if (Convert.ToInt32(ViewState["action"]) != Convert.ToInt32(objCommon.LookUp("acc_" + Session["comp_code"].ToString() + "_PAYMENT_GROUP", "PGROUP_NO", "GROUPNAME='" + GroupName + "'")))
                {
                    //txtBankName.Text = string.Empty;
                    objCommon.DisplayUserMessage(updBank, "Account Already Exist...!", this);
                    ViewState["action"] = "add";
                    ClearControl();
                    return;
                }
                else
                    look = 0;


            }
            if (look == 0)
            {
                if (ViewState["action"].ToString() != null && ViewState["action"].ToString() != "add")
                {
                    ret = objAccPaymentTypeController.UpdatePaymentData(Convert.ToInt32(ViewState["action"]), GroupName, PartyName, USERID, COLLEGEID, Session["comp_code"].ToString());
                }
                else if (ViewState["action"].ToString() != null && ViewState["action"].ToString() == "add")
                {
                    ret = objAccPaymentTypeController.AddPaymentData(GroupName, PartyName, USERID, COLLEGEID, Session["comp_code"].ToString());
                }

                if (ret == 1)
                {
                    objCommon.DisplayUserMessage(updBank, "Account Added Successfully", this);
                    ViewState["action"] = "add";
                    ClearControl();
                }
                if (ret == 2)
                {
                    objCommon.DisplayUserMessage(updBank, "Account Updated Successfully", this);
                    ViewState["action"] = "add";
                    ClearControl();
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "AbstractBillAccounts.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
            //objCommon.ShowErrorMessage(Panel_Error, Label_ErrorMessage, Common.Message.ExceptionOccured, Common.MessageType.Error);
        }
        GroupID = 0;
        FillPartyDetails();

    }

    //Clear Control Values
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearControl();
    }

    #endregion Page Events
}
