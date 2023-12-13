//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC                                                        
// PAGE NAME     : RECIEPT TYPE DEFINITION (USER CONTROL)                                                     
// CREATION DATE : 14-MAY-2009                                                        
// CREATED BY    : AMIT YADAV
// MODIFIED DATE :
// MODIFIED DESC :
//======================================================================================

using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class Fees_RecieptTypeDefinition : System.Web.UI.UserControl
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();

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
                this.CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                ////Load Page Help
                //if (Request.QueryString["pageno"] != null)
                //{
                //    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                //}

                // Set form action as add
                ViewState["action"] = "add";
                BindListView();
                PopulateDropDown();
                //this.objCommon.FillDropDownList(ddlCompany, "", "", "", "", "");
            }
        }
        txtCode.Focus();
        divMsg.InnerText = string.Empty;
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=RecieptTypeDefinition.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=RecieptTypeDefinition.aspx");
        }
    }



    private void PopulateDropDown()
    {
        try
        {
            objCommon.FillDropDownList(ddlReceiptCode, "ACD_RECIEPT_CODE", "RECIEPT_CODE", "RC_NAME", "RCNO>0", " RCNO");


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Administration_BulkStudentLogin.PopulateDropDown-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }



    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            RecieptType recieptType = new RecieptType();
            recieptType.RecieptCode = ddlReceiptCode.SelectedValue;
            recieptType.RecieptTitle = txtTitle.Text;
            recieptType.BelongsTo = ddlBelongsTo.SelectedValue[0];
            recieptType.AccountNumber = txtAccountNo.Text;
            recieptType.CompanyName = ddlCompany.SelectedValue;
            recieptType.IsLinked = rdoYes.Checked ? true : false;
            recieptType.CollegeCode = Session["colcode"].ToString();
            recieptType.isadmission = 0;
            if (chkStatus.Checked)
            {
                recieptType.isadmission = 1;
            }
            else
            {
                recieptType.isadmission = 0;
            }

            recieptType.IsLateFine = (chkLateFineStatus.Checked) ? 1 : 0;


            //Check whether to add or update
            if (ViewState["action"] != null)
            {
                RecieptTypeController controller = new RecieptTypeController();
                CustomStatus cs = new CustomStatus();
                string Rcode = objCommon.LookUp("ACD_RECIEPT_TYPE", "RECIEPT_CODE", "RECIEPT_CODE='" + ddlReceiptCode.SelectedValue + "'");
                string isadmmisionStatutsAtleatonetimeTrue = objCommon.LookUp("ACD_RECIEPT_TYPE", "ISNULL(ISADDMISSION,0)", "ISADDMISSION = 1");
                if (ViewState["action"].ToString().Equals("add"))
                {
                    //Add Branch Type
                    // isadmmisionStatutsAtleatonetimeTrue = if 1 its true and if 0 its false
                    if ((isadmmisionStatutsAtleatonetimeTrue) == "1" && chkStatus.Checked)
                    {
                        objCommon.DisplayMessage("Is Tution/Admission/Institute Fees Submit only for one Receipt Code ", Page);
                        return;
                    }
                    else
                    {
                        if (Rcode == (ddlReceiptCode.Text))
                        {
                            objCommon.DisplayMessage("Receipt Code Already Exists!", Page);
                            ddlReceiptCode.Focus();
                        }
                        else
                        {
                            cs = (CustomStatus)controller.AddRecieptType(recieptType);
                            objCommon.DisplayMessage("Receipt Type Saved!", Page);
                        }
                        BindListView();
                    }
                }
                else
                {
                    if (ViewState["action"].ToString().Equals("EDIT"))
                    {
                        objCommon.DisplayMessage("Receipt Code Already Exists!", Page);
                        ddlReceiptCode.Focus();
                    }
                    else
                    {

                        recieptType.RecieptTypeId = Convert.ToInt32(ViewState["RecieptTypeId"].ToString());
                        cs = (CustomStatus)controller.UpdateRecieptType(recieptType);
                        objCommon.DisplayMessage("Receipt Type Updated!", Page);
                    }
                    BindListView();
                }

                if (cs.Equals(CustomStatus.TransactionFailed))
                {
                    throw new IITMSException("Addition or updation of reciept type failed. Please try again.");
                }
            }
            this.ClearControlContents();
        }



        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        this.ClearControlContents();
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        ShowReport("ReceiptType", "rptReceiptType.rpt");
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton editButton = sender as ImageButton;
            int recieptTypeId = Int32.Parse(editButton.CommandArgument);

            RecieptTypeController controller = new RecieptTypeController();
            DataTable dt = controller.GetRecieptTypeById(recieptTypeId).Tables[0];
            ddlReceiptCode.SelectedValue = dt.Rows[0]["RECIEPT_CODE"].ToString();
            //txtCode.Text = dt.Rows[0]["RECIEPT_CODE"].ToString();
            txtTitle.Text = dt.Rows[0]["RECIEPT_TITLE"].ToString();
            ddlBelongsTo.SelectedValue = dt.Rows[0]["BELONGS_TO"].ToString();
            txtAccountNo.Text = dt.Rows[0]["ACCNO"].ToString();
            //chkStatus.Text = dt.Rows[0]["ISADDMISSION"].ToString();

            //
            if (dt.Rows[0]["ISADDMISSION"].ToString() == "1")
            {
                chkStatus.Checked = true;
            }
            else
            {
                chkStatus.Checked = false;
            }
            //

            chkLateFineStatus.Checked = (dt.Rows[0]["IS_LATE_FINE_APPLICABLE"].ToString() == "1") ? true : false;

            // ddlCompany.SelectedValue = dt.Rows[0]["CNAME"].ToString();
            if (dt.Rows[0]["LINKED"].ToString() == "True")
                rdoYes.Checked = true;
            else
                rdoNo.Checked = true;

            ViewState["RecieptTypeId"] = recieptTypeId.ToString();
            ViewState["action"] = "edit";

        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void dpRecieptType_PreRender(object sender, EventArgs e)
    {
        //BindListView();
    }

    private void BindListView()
    {
        try
        {
            RecieptTypeController recieptTypeController = new RecieptTypeController();
            DataSet ds = recieptTypeController.GetRecieptTypes();
            lvRecieptType.DataSource = ds;
            lvRecieptType.DataBind();
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void ClearControlContents()
    {
        ddlReceiptCode.SelectedIndex = 0;
        txtTitle.Text = string.Empty;
        ddlBelongsTo.SelectedIndex = 0;
        txtAccountNo.Text = string.Empty;
        chkStatus.Checked = false;
        chkLateFineStatus.Checked = false;
        //ddlCompany.SelectedValue = "Please Select";

        ViewState["action"] = "add";
    }
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@UserName=" + Session["username"].ToString();

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}