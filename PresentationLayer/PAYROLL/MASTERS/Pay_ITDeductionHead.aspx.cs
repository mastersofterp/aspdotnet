  //======================================================================================
// PROJECT NAME  : CCMS                                                                
// MODULE NAME   : PAY ROLL
// PAGE NAME     : Pay_ITRules.ASPX                                                    
// CREATION DATE : 13-Apr-2011
// CREATED BY    : Ankit Agrawal                                          
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

public partial class Pay_ITRebate : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ITMasController objITMas = new ITMasController();
    string UsrStatus = string.Empty;

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

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Pay_ITDeductionHead.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Pay_ITDeductionHead.aspx");
        }
    }

    //Save Rule
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
       Update();
    }
    protected void Update()
    {
        try
        {
            //Add/Update
            //string uRight = GetUserRight();
            //string TwoCharAdd = uRight.Substring(0, 2).ToString();
            //string TwoCharMod = uRight.Substring(2, 2).ToString();

            if (ViewState["lblFNO"] != null)
            {

                CustomStatus cs = (CustomStatus)objITMas.UpdateITDedutionHead(Convert.ToInt32(ViewState["lblFNO"].ToString().Trim()), txtHead.Text.ToString().Trim().ToUpper(), txtField.Text.ToString().Trim().ToUpper());
                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    BindListView();
                    Clear();
                    objCommon.DisplayMessage(this.updpanel, "Record Updated Successfully!", this.Page);
                }
                else
                    objCommon.DisplayMessage(this.updpanel, "Exception Occured!", this.Page);
            }
            else
            {
                int FNO = 0;
                CustomStatus cs = (CustomStatus)objITMas.InsertITDeductionHead(FNO, txtHead.Text.ToString().Trim().ToUpper(), txtField.Text.ToString().Trim().ToUpper(),"N", Session["colcode"].ToString(), Convert.ToInt32(Session["userno"]));
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    BindListView();
                    Clear();
                    objCommon.DisplayMessage(this.updpanel, "Record Saved Successfully!", this.Page);
                }
                else
                {
                    objCommon.DisplayMessage(this.updpanel, "Exception Occured!", this.Page);

                }
            
            }
            //else
            //{
            //    CustomStatus cs=(CustomStatus)objITMas.UpdateITDedutionHead
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
    //Get Details of Rules To be Edited on textbox
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            ViewState["lblFNO"] = int.Parse(btnEdit.CommandArgument);
            ListViewDataItem lst = btnEdit.NamingContainer as ListViewDataItem;
            Label lblPayhead = lst.FindControl("lblPayHead") as Label;
            Label lblDeduHead = lst.FindControl("lblDedHead") as Label;
            txtHead.Text = lblPayhead.Text.Trim();
            txtField.Text = lblDeduHead.Text.Trim();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Scale.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    //Check user rights and generate the report
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        //string uRight = GetUserRight();
        //string TwoCharReport = uRight.Substring(4, 2).ToString();
        //if (TwoCharReport == "YR")
        //{
        ShowReport("ITDEDUCTIONHEADS", "Pay_ITDeductionHead.rpt");
        //}
        //else
        //{
        //    objCommon.DisplayMessage(updpanel, Common.Message.NoReport, this);
        //    return;
        //}



    }

    //Fetch Already Defined Rules From The Database
    private void BindListView()
    {
        try
        {
            DataSet ds = objITMas.GetITDeductionHeads();

            if (ds.Tables[0].Rows.Count > 0)
            {
                lvITDedHead.DataSource = ds;
                lvITDedHead.DataBind();
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

  

    //Function to generate Report
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string Script = string.Empty;

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("PAYROLL")));

            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,PAYROLL," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_UserId=" + Session["userfullname"].ToString() + ",@P_ReportName=" + reportTitle ;
            //",@P_Session=" + Session["Session"].ToString() + ",@P_Ip=" + Session["IPADDR"].ToString() ++ ",@P_WorkingDate=" + Session["WorkingDate"].ToString().Trim()


            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

           
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

    private void Clear()
    {
        txtField.Text = "";
        txtHead.Text = "";
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Clear();
    }
}
