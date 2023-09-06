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
using IITMS.NITPRM;
using IITMS.NITPRM.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class PAYROLL_MASTERS_Pay_BankManagment : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();

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
                //CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                     lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
               
                //Bind the ListView with Qualification
                BindListViewQualification();

                ViewState["action"] = "add";

                
            }
        }
    }
    private void BindListViewQualification()
    {
        try
        {
            EmpCreateController objMasters = new EmpCreateController();
            int BANKNO = 0;

            DataSet dsQualification = objMasters.GetBANKID(BANKNO);

            if (dsQualification.Tables[0].Rows.Count > 0)
            {
                lvQualification.DataSource = dsQualification;
                lvQualification.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Masters_qualificationMas.BindListViewQualification-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void ShowEditDetailsSubBlog(int BANKNO)
    {
       
        DataSet ds = null;
        EmpCreateController objMasters = new EmpCreateController();
        try
        {

            ds = objMasters.GetBANKID(BANKNO);
            if (ds.Tables[0].Rows.Count > 0)
            {

                txtbname.Text = ds.Tables[0].Rows[0]["BANKNAME"].ToString();
                txtbcode.Text = ds.Tables[0].Rows[0]["BANKCODE"].ToString();
                txtaddress.Text = ds.Tables[0].Rows[0]["BANKADDR"].ToString();
                txtbranch.Text = ds.Tables[0].Rows[0]["BRANCHNAME"].ToString();
               
            }
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Stores_Masters_Str_Dept_Master.ShowEditDetailsSubDepartment-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }
        finally
        {
            ds.Clear();
            ds.Dispose();
        }

    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            ViewState["BANKNO"] = int.Parse(btnEdit.CommandArgument);
            ViewState["action"] = "edit";
            ShowEditDetailsSubBlog(Convert.ToInt32(ViewState["BANKNO"].ToString()));

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Masters_qualificationMas.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }
    private void Clear()
    {

        txtbcode.Text = string.Empty;
        txtbname.Text = string.Empty;
        txtbranch.Text = string.Empty;
        txtaddress.Text = string.Empty;
        ViewState["action"] = "add";
        lblStatus.Text = string.Empty;
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            //Add/Update
            if (ViewState["action"] != null)
            {
                EmpCreateController objMasters = new EmpCreateController();

                if (ViewState["action"].ToString().Equals("add"))
                {
                    CustomStatus cs = (CustomStatus)objMasters.AddBankManagment(txtbname.Text, txtbcode.Text, txtaddress.Text, txtbranch.Text, Session["colcode"].ToString());
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        BindListViewQualification();
                        Clear();
                        MessageBox("Record Saved Successfully");
                       
                    }
                    else if (cs.Equals(CustomStatus.RecordExist))
                    {
                        BindListViewQualification();
                        Clear();
                        MessageBox("Record Already Exist");
                    }
                    else
                    {
                        MessageBox("Error!!!!!");
                    }
                }
                else
                {
                    if (ViewState["BANKNO"] != null)
                    {
                        CustomStatus cs = (CustomStatus)objMasters.UpdateBank(txtbname.Text, txtbcode.Text, txtaddress.Text, txtbranch.Text, Convert.ToInt32(ViewState["BANKNO"]));
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            BindListViewQualification();
                            Clear();
                            MessageBox("Record Successfully Updated");
                        }
                        else
                        {
                            objCommon.DisplayMessage(pnlList, "Record Already Exist", this);
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Masters_qualificationMas.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString()) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Pay_BankManagment.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Pay_BankManagment.aspx");
        }
    }

    protected void btnShowReport_Click(object sender, EventArgs e)
    {
        ShowReportBankDetails("Bank Details Report","PayBankDetailsReport.rpt");

    }
    public void ShowReportBankDetails(string reportTitle, string rptFileName)
    {
        string Script = string.Empty;
        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("payroll")));
        url += "Reports/commonreport.aspx?";
        url += "pagetitle=" + reportTitle;
        url += "&path=~,Reports,Payroll," + rptFileName;
        url += "&param=@P_BANKNO=0"+",@P_COLLEGE_CODE=" + Session["colcode"].ToString();

        divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
        divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
        divMsg.InnerHtml += " </script>";

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
        sb.Append(@"window.open('" + url + "','','" + features + "');");

        ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.UpdatePanel1.GetType(), "controlJSScript", sb.ToString(), true);
    }
}