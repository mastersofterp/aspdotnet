//======================================================================================
// PROJECT NAME  : CCMS                                                                
// MODULE NAME   : PAY ROLL
// PAGE NAME     : Pay_ITHead.ASPX                                                    
// CREATION DATE : 14-Apr-2011
// CREATED BY    : Mrunal Bansod                                          
// MODIFIED DATE :
// MODIFIED DESC :
//=======================================================================================

using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class Pay_ITHead : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ITMasController objITMas = new ITMasController();

    string UsrStatus = string.Empty;

    #region PageEvents

    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }


    //Checking logon Status and redirection to Login Page(Default.aspx) if user is not logged in
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

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }

                BindListView();
            }
        }

    }

    #endregion

    #region Actions

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Pay_ITHead.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Pay_ITHead.aspx");
        }
    }

    //Get Details of Heads To be Edited on textbox
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            ViewState["lblCNO"] = int.Parse(btnEdit.CommandArgument);
            ListViewDataItem lst = btnEdit.NamingContainer as ListViewDataItem;
            Label lblHead = lst.FindControl("lblHead") as Label;
            Label lblSection = lst.FindControl("lblSection") as Label;
            Label lblPayHead = lst.FindControl("lblPayHead") as Label;
            Label lblfullhead = lst.FindControl("Label1") as Label;
            Label lblLimit = lst.FindControl("lblLimit") as Label;
            Label lblIsPercentage = lst.FindControl("lblIsPercentage") as Label;
            txtheadfullname.Text = lblfullhead.Text.Trim();
            txtHead.Text = lblHead.Text.Trim();
            txtSection.Text = lblSection.Text.Trim();
            txtPayHead.Text = lblPayHead.Text.Trim();
            txtLimitAmt.Text = lblLimit.Text.Trim();
            if (lblIsPercentage.Text == "False")
                chkIsPercentage.Checked = false;
            else
                chkIsPercentage.Checked = true;
            
            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Scale.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    //To Add and Update Heads
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            //Update
           
            //if (TwoCharAdd == "YA")
            //{
            //if (ViewState["action"] != null)
            //{
            //    if (ViewState["action"].ToString().Equals("add"))
            //    {
            //        CustomStatus cs = (CustomStatus)objITMas.AddITHeads(txtHead.Text.Trim(), txtSection.Text.Trim(), txtPayHead.Text.Trim());
            //        if (cs.Equals(CustomStatus.RecordSaved))
            //        {
            //            BindListView();
            //            Clear();

            //            objCommon.ShowErrorMessage(Panel_Confirm, Label_ConfirmMessage, Common.Message.Saved, Common.MessageType.Success);
            //        }
            //        else
            //            objCommon.ShowErrorMessage(Panel_Error, Label_ErrorMessage, Common.Message.ExceptionOccured, Common.MessageType.Error);

            //    }
            //}
            //}
            //else
            //{


            if (ViewState["lblCNO"] != null)
            {
                if (chkIsPercentage.Checked == true && Convert.ToDouble(txtLimitAmt.Text) > 100.00)
                {
                    objCommon.DisplayMessage(this.updpanel, "Please enter percentage less than 100", this.Page);
                    return;
                }

                CustomStatus cs = (CustomStatus)objITMas.UpdateITHead(Convert.ToInt32(ViewState["lblCNO"].ToString().Trim()), txtHead.Text.ToString().Trim().ToUpper(), txtSection.Text.ToString().Trim().ToUpper(), txtPayHead.Text.ToString().Trim().ToUpper(), txtheadfullname.Text.ToString().Trim().ToUpper(), Convert.ToDecimal(txtLimitAmt.Text.ToString().Trim()), chkIsPercentage.Checked);
                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    BindListView();
                    objCommon.DisplayMessage(this.updpanel, "Record Updated Successfully!", this.Page);
                    ViewState["lblCNO"] = null;
                    Clear();
                }
                else
                    objCommon.DisplayMessage(this.updpanel, "Exception Occured", this.Page);

            }
            else
            {
                int CNO = 0;
                CustomStatus cs = (CustomStatus)objITMas.AddITHeadCHV(CNO,txtHead.Text.ToString().Trim().ToUpper(), txtSection.Text.ToString().Trim().ToUpper(),Convert.ToInt32(Session ["colcode"]),txtPayHead.Text.ToString().Trim().ToUpper(),txtheadfullname.Text.Trim().ToUpper(),Convert.ToDecimal(txtLimitAmt.Text), chkIsPercentage.Checked);
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    BindListView();
                    Clear();
                    objCommon.DisplayMessage(this.updpanel, "Record Added Successfully!", this.Page);
                }

            }
            //}

        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Scale.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    //Call Clear() Function to Clear the Controls on button Click
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Clear();
    }

    //Generate the Report on button click
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        ShowReport("ITHeads", "Pay_ITHead.rpt");
    }

    protected void dpPager_PreRender(object sender, EventArgs e)
    {
        BindListView();
    }

    #endregion

    #region Methods
    
 
    //Fetch Already Defined Heads From The Database
    private void BindListView()
    {
        try
        {
            DataSet ds = objITMas.GetITHeads();

            if (ds.Tables[0].Rows.Count > 0)
            {
                lvITHead.DataSource = ds;
                lvITHead.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Scale.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

     //To clear Controls
    protected void Clear()
    {
        txtHead.Text = string.Empty;
        txtSection.Text = string.Empty;
        txtPayHead.Text = string.Empty;
        txtheadfullname.Text = string.Empty;
        txtLimitAmt.Text = string.Empty;
        chkIsPercentage.Checked = false;
    }

    //To Generate the Report
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            //string session = Session["Session"].ToString();
            //string ip = Session["IPADDR"].ToString();
            //string workingdate = Session["WorkingDate"].ToString();
            string Script = string.Empty;

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("PAYROLL")));

            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,PAYROLL," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_UserId=" + Session["userfullname"].ToString() + ",@P_ReportName=" + reportTitle;
            //+",@P_Session=" + Session["Session"].ToString() + ",@P_Ip=" + Session["IPADDR"].ToString() + ",@P_WorkingDate=" + Session["WorkingDate"].ToString().Trim()

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Report", Script, true);
            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updpanel, this.updpanel.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Scale.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

   

  
    #endregion
}
