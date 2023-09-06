//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC
// PAGE NAME     : STUDENT INFORMATION                                                     
// CREATION DATE : 27-SEPT-2010                                                          
// CREATED BY    : NIRAJ D. PHALKE                               
// MODIFIED DATE : 30-11-2013                
// ADDED BY      : ASHISH MOTGHARE                                      
// MODIFIED DESC :                                                                      
//======================================================================================
using System;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Linq;

public partial class Academic_StudentInfoEntry : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    FeeCollectionController feeController = new FeeCollectionController();
    #region digilocker cred
    string ClientSecret = "7e3d07e964cf19918a1b";
    string ClientId = "E006C74F";
    //string ClientSecret = "a3adea63d589e2d8b120";
    //string ClientId = "98FB0BEC";
    string RedirectUrl = "";

    #endregion digilocker cred
    protected void Page_Load(object sender, EventArgs e)
    {

        RedirectUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/ACADEMIC/StudentInfoEntry.aspx?pageno=74";
        string Code = Request.QueryString["code"];
        string State = Request.QueryString["state"];
        string Error = Request.QueryString["error"];
        if (Code != null)
        {
            Response.Redirect("~/academic/UploadDocument.aspx?ResponseCode=" + Code + "&ResponseState=" + State + "&ResponseError=" + Error);
        }
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
                if (ViewState["action"] == null)
                    ViewState["action"] = "add";
                ViewState["usertype"] = Session["usertype"];

                if (ViewState["usertype"].ToString() == "2")
                {

                    divadmissiondetails.Visible = false;
                    divtabs.Visible = true;
                    int FinalSubmit = 0 ;
                    if (objCommon.LookUp("ACD_ADM_STUD_INFO_SUBMIT_LOG", "FINAL_SUBMIT", "IDNO=" + Convert.ToInt32(Session["idno"])) != String.Empty)
                    {
                         FinalSubmit = Convert.ToInt32(objCommon.LookUp("ACD_ADM_STUD_INFO_SUBMIT_LOG", "FINAL_SUBMIT", "IDNO=" + Convert.ToInt32(Session["idno"])));
                    }

                    if (FinalSubmit == 1)
                    { divPrintReport.Visible = true; }
                    else
                    { divPrintReport.Visible = false; }

                    //divPrintReport.Visible = true;
                    //DivCovid.Visible = true;
                    Response.Redirect("~/academic/PersonalDetails.aspx");
                    // Server.Transfer("~/academic/PersonalDetails.aspx", false);

                }
                else
                {
                    if (!string.IsNullOrEmpty(Request.QueryString["id"] as string))
                    {
                        Session["stuinfoidno"] = Request.QueryString["id"].ToString();
                        //Response.Redirect("~/academic/PersonalDetails.aspx");
                        Server.Transfer("~/academic/PersonalDetails.aspx", false);
                    }
                }
            }
            //Search Pannel Dropdown Added by Swapnil
            this.objCommon.FillDropDownList(ddlSearch, "ACD_SEARCH_CRITERIA", "ID", "CRITERIANAME, ISNULL(IS_FEE_RELATED,0) IS_FEE_RELATED", "ID > 0 AND ISNULL(IS_FEE_RELATED,0)=0", "SRNO    ");
            ddlSearch.SelectedIndex = 1;
            ddlSearch_SelectedIndexChanged(sender, e);
            //ddlSearch.SelectedIndex = 0;
            //End Search Pannel Dropdown
        }
        else
        {
            if (Page.Request.Params["__EVENTTARGET"] != null)
            {
                if (Page.Request.Params["__EVENTTARGET"].ToString().ToLower().Contains("btnsearch"))
                {
                    string[] arg = Page.Request.Params["__EVENTARGUMENT"].ToString().Split(',');
                }
                if (Page.Request.Params["__EVENTTARGET"].ToString().ToLower().Contains("btncancelmodal"))
                {

                }
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
                Response.Redirect("~/notauthorized.aspx?page=StudentInfoEntryNew.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=StudentInfoEntryNew.aspx");
        }
    }

    protected void ProfileComplition()
    {
        try
        {

        }
        catch (Exception Ex)
        {
            throw;
        }
    }
    protected void lnkPersonalDetail_Click(object sender, EventArgs e)
    {
        if (ViewState["usertype"].ToString() == "2")
        {
            Server.Transfer("~/academic/PersonalDetails.aspx", false);
        }
        else
        {
            if (Session["stuinfoidno"] != null)
            {
                Server.Transfer("~/academic/PersonalDetails.aspx", false);
            }
            else
            {
                objCommon.DisplayMessage("Please Select Search Criteria!!", this.Page);
                ddlSearch.SelectedIndex = 0;
                txtSearch.Text = string.Empty;
            }
        }
        // HttpContext.Current.RewritePath("PersonalDetails.aspx");
    }
    protected void lnkAddressDetail_Click(object sender, EventArgs e)
    {
        if (ViewState["usertype"].ToString() == "2")
        {
            Server.Transfer("~/academic/AddressDetails.aspx", false);
        }
        else
        {
            if (Session["stuinfoidno"] != null)
            {
                Server.Transfer("~/academic/AddressDetails.aspx", false);
            }
            else
            {
                objCommon.DisplayMessage("Please Select Search Criteria!!", this.Page);
                ddlSearch.SelectedIndex = 0;
                txtSearch.Text = string.Empty;
            }
        }

    }
    protected void lnkAdmissionDetail_Click(object sender, EventArgs e)
    {

        if (ViewState["usertype"].ToString() == "2")
        {
            Server.Transfer("~/academic/AdmissionDetails.aspx", false);
        }
        else
        {
            if (Session["stuinfoidno"] != null)
            {
                Server.Transfer("~/academic/AdmissionDetails.aspx", false);
            }
            else
            {
                objCommon.DisplayMessage("Please Select Search Criteria!!", this.Page);
                ddlSearch.SelectedIndex = 0;
                txtSearch.Text = string.Empty;
            }
        }
    }

    protected void lnkUploadDocument_Click(object sender, EventArgs e)
    {
        if (ViewState["usertype"].ToString() == "2")
        {
            Server.Transfer("~/academic/UploadDocument.aspx", false);
        }
        else
        {
            if (Session["stuinfoidno"] != null)
            {
                Server.Transfer("~/academic/UploadDocument.aspx", false);
            }
            else
            {
                objCommon.DisplayMessage("Please Select Search Criteria!!", this.Page);
                ddlSearch.SelectedIndex = 0;
                txtSearch.Text = string.Empty;
            }
        }
    }

    protected void lnkQualificationDetail_Click(object sender, EventArgs e)
    {
        if (ViewState["usertype"].ToString() == "2")
        {
            Server.Transfer("~/academic/QualificationDetails.aspx", false);
        }
        else
        {
            if (Session["stuinfoidno"] != null)
            {
                Server.Transfer("~/academic/QualificationDetails.aspx", false);
            }
            else
            {
                objCommon.DisplayMessage("Please Select Search Criteria!!", this.Page);
                ddlSearch.SelectedIndex = 0;
                txtSearch.Text = string.Empty;
            }
        }

    }
    protected void lnkotherinfo_Click(object sender, EventArgs e)
    {

        if (ViewState["usertype"].ToString() == "2")
        {
            Server.Transfer("~/academic/OtherInformation.aspx", false);
        }
        else
        {
            if (Session["stuinfoidno"] != null)
            {
                Server.Transfer("~/academic/OtherInformation.aspx", false);
            }
            else
            {
                objCommon.DisplayMessage("Please Select Search Criteria!!", this.Page);
                ddlSearch.SelectedIndex = 0;
                txtSearch.Text = string.Empty;
            }
        }


    }
    protected void lnkApproveAdm_Click(object sender, EventArgs e)
    {
        if (ViewState["usertype"].ToString() == "2")
        {
            Server.Transfer("~/academic/ApproveAdmission.aspx", false);
        }
        else
        {
            if (Session["stuinfoidno"] != null)
            {
                Server.Transfer("~/academic/ApproveAdmission.aspx", false);
            }
            else
            {
                objCommon.DisplayMessage("Please Select Search Criteria!!", this.Page);
                ddlSearch.SelectedIndex = 0;
                txtSearch.Text = string.Empty;
            }
        }

    }
    protected void lnkprintapp_Click(object sender, EventArgs e)
    {
        GEC_Student objGecStud = new GEC_Student();
        if (ViewState["usertype"].ToString() == "2")
        {
            objGecStud.RegNo = Session["idno"].ToString();
            string output = objGecStud.RegNo;
            ShowReport("Admission_Form_Report_M.TECH", "Admission_Slip_Confirm_PHD_General.rpt", output);
        }
        else
        {
            if (Session["stuinfoidno"] != null)
            {
                objGecStud.RegNo = Session["stuinfoidno"].ToString();
                string output = objGecStud.RegNo;
                ShowReport("Admission_Form_Report_M.TECH", "Admission_Slip_Confirm_PHD_General.rpt", output);
            }
            else
            {
                objCommon.DisplayMessage("Please Select Search Criteria!!", this.Page);
            }
        }
    }

    private void ShowReport(string reportTitle, string rptFileName, string regno)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            //url += "pagetitle=" + reportTitle;
            //url += "pagetitle=Admission Form Report " + txtRegNo.Text;
            url += "&path=~,Reports,Academic," + rptFileName;
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + regno + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_ADMBATCH=" + Convert.ToInt32(ddlBatch.SelectedValue) + ",@PTYPE=" + ((rbDDPayment.Checked) ? Convert.ToInt32("0") : Convert.ToInt32("1")) + ",@Year=" + ddlYear.SelectedValue; 
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + Convert.ToInt16(txtIDNo.Text.Trim().ToString()) + "";
            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";


            //To open new window from Updatepanel
            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            //sb.Append(@"window.open('" + url + "','','" + features + "');");
            //ScriptManager.RegisterClientScriptBlock(this., this.upEditQualExm.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void lnkCovid_Click(object sender, EventArgs e)
    {
        if (ViewState["usertype"].ToString() == "2")
        {
            Server.Transfer("~/academic/CovidVaccinationDetails.aspx", false);
        }
        else
        {
            if (Session["stuinfoidno"] != null)
            {
                Server.Transfer("~/academic/CovidVaccinationDetails.aspx", false);
            }
            else
            {
                objCommon.DisplayMessage("Please Select Search Criteria!!", this.Page);
                ddlSearch.SelectedIndex = 0;
                txtSearch.Text = string.Empty;
            }
        }
    }

    #region SearchPannel

    protected void ddlSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            pnlLV.Visible = false;
            lblNoRecords.Visible = false;
            lvStudent.DataSource = null;
            lvStudent.DataBind();
            if (ddlSearch.SelectedIndex > 0)
            {
                DataSet ds = objCommon.GetSearchDropdownDetails(ddlSearch.SelectedItem.Text);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string ddltype = ds.Tables[0].Rows[0]["CRITERIATYPE"].ToString();
                    string tablename = ds.Tables[0].Rows[0]["TABLENAME"].ToString();
                    string column1 = ds.Tables[0].Rows[0]["COLUMN1"].ToString();
                    string column2 = ds.Tables[0].Rows[0]["COLUMN2"].ToString();
                    if (ddltype == "ddl")
                    {
                        pnltextbox.Visible = false;
                        txtSearch.Visible = false;
                        pnlDropdown.Visible = true;

                        divtxt.Visible = false;
                        lblDropdown.Text = ddlSearch.SelectedItem.Text;
                        objCommon.FillDropDownList(ddlDropdown, tablename, column1, column2, column1 + ">0", column1);
                        ddlDropdown.Focus();
                    }
                    else
                    {
                        pnltextbox.Visible = true;
                        divtxt.Visible = true;
                        txtSearch.Visible = true;
                        pnlDropdown.Visible = false;
                        txtSearch.Focus();
                    }
                }
            }
            else
            {
                pnltextbox.Visible = false;
                pnlDropdown.Visible = false;
            }
        }
        catch
        {
            throw;
        }
    }
    private void bindlist(string category, string searchtext, int uano)
    {
        StudentController objSC = new StudentController();
        DataSet ds = objSC.RetrieveStudentDetailsAdmCancel_StudInfo(searchtext, category, uano);

        if (ds.Tables[0].Rows.Count > 0)
        {
            pnlLV.Visible = true;
            lvStudent.Visible = true;
            lvStudent.DataSource = ds;
            lvStudent.DataBind();
            lblNoRecords.Text = "Total Records : " + ds.Tables[0].Rows.Count.ToString();
            objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvStudent);//Set label - 
        }
        else
        {
            lblNoRecords.Text = "Total Records : 0";
            lvStudent.Visible = false;
            lvStudent.DataSource = null;
            lvStudent.DataBind();
        }
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        //Response.Redirect(Request.Url.ToString());
        ddlSearch.SelectedIndex = 0;
        pnlDropdown.Visible = false;
        pnltextbox.Visible = false;
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        lblNoRecords.Visible = true;
        string value = string.Empty;
        int uano = Convert.ToInt32(Session["userno"].ToString());

        if (ddlDropdown.SelectedIndex > 0)
        {
            value = ddlDropdown.SelectedValue;
        }
        else
        {
            value = txtSearch.Text;
        }

        //ddlSearch.ClearSelection();

        //bindlist(ddlSearch.SelectedItem.Text, value); //commented by ruchika on 01.09.2022
        bindlist(ddlSearch.SelectedItem.Text, value, uano);

        ddlDropdown.ClearSelection();
        txtSearch.Text = string.Empty;
        //if (value == "BRANCH")
        //{
        //    divbranch.Attributes.Add("style", "display:block");

        //}
        //else if (value == "SEM")
        //{
        //    divSemester.Attributes.Add("style", "display:block");
        //}
        //else
        //{
        //    divtxt.Attributes.Add("style", "display:block");
        //}

        //ShowDetails();

    }
    protected void lnkId_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnk = sender as LinkButton;
            string url = string.Empty;
            if (Request.Url.ToString().IndexOf("&id=") > 0)
                url = Request.Url.ToString().Remove(Request.Url.ToString().IndexOf("&id="));
            else
                url = Request.Url.ToString();

            Label lblenrollno = lnk.Parent.FindControl("lblstuenrollno") as Label;

            Session["stuinfoenrollno"] = lblenrollno.Text.Trim();
            Session["stuinfofullname"] = lnk.Text.Trim();

            Session["stuinfoidno"] = Convert.ToInt32(lnk.CommandArgument);
            //txtIDNo.Text = Session["stuinfoidno"].ToString();
            Response.Redirect("~/academic/PersonalDetails.aspx");
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    #endregion
}
