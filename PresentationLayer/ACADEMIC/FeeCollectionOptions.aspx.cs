//======================================================================================
// PROJECT NAME  : UAIMS
// MODULE NAME   : ACADEMIC
// PAGE NAME     : Fee COLLECTION OPTIONS
// CREATION DATE : 29-MAY-2009
// CREATED BY    : AMIT YADAV
// MODIFIED DATE :
// MODIFIED DESC :
//======================================================================================

using System;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class Academic_FeeCollectionOptions : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    FeeCollectionController feeController = new FeeCollectionController();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        // Set MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                // Check User Session
                if (Session["userno"] == null || Session["username"] == null ||
                    Session["usertype"] == null || Session["userfullname"] == null)
                {
                    Response.Redirect("~/default.aspx");
                }
                else
                {
                    // Check User Authority 
                    //  this.CheckPageAuthorization();

                    // Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    // Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                    //this.objCommon.FillDropDownList(ddlSem, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO > 0", "SEMESTERNO");
                    //this.objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO > 0", "DEGREENO");
                    //this.objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "BRANCHNO >0", "BRANCHNO");
                    //this.objCommon.FillDropDownList(ddlYear, "ACD_YEAR", "YEAR", "YEARNAME", "YEAR>0 AND YEAR<6", "");
                    this.objCommon.FillDropDownList(ddlReceiptType, "ACD_RECIEPT_TYPE RT INNER JOIN [dbo].[ACD_COUNTER_REF] CR ON (RT.RECIEPT_CODE = CR.RECEIPT_PERMISSION)", " DISTINCT RT.RECIEPT_CODE", "RT.RECIEPT_TITLE", "RT.RCPTTYPENO>0 AND CR.UA_NO =" + Convert.ToInt32(Session["userno"]), "RT.RECIEPT_TITLE");
                    //this.objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNAME <> '-' AND SEMESTERNO > 0", "SEMESTERNO");//*******
                    //Search Pannel Dropdown Added by Swapnil
                    this.objCommon.FillDropDownList(ddlSearch, "ACD_SEARCH_CRITERIA", "ID", "CRITERIANAME", "ID > 0 ", "SRNO");
                    ddlSearch.SelectedIndex = 0;
                    //End Search Pannel Dropdown
                }
               
            }
            else
            {
                // Clear message div
                divMsg.InnerHtml = string.Empty;

                /// if postback has been done implicitly
                /// then call correspinding methods.
                if (Request.Params["__EVENTTARGET"] != null &&
                    Request.Params["__EVENTTARGET"].ToString() != string.Empty)
                {
                    if (Request.Params["__EVENTTARGET"].ToString() == "btnSearch")
                        this.ShowSearchResults(Request.Params["__EVENTARGUMENT"].ToString());
                }

                if (Page.Request.Params["__EVENTTARGET"].ToString().ToLower().Contains("btnclear"))
                {
                    txtSearch.Text = string.Empty;
                    lvStudent.DataSource = null;
                    lvStudent.DataBind();
                    //ddlDegree.ClearSelection();
                    //ddlBranch.ClearSelection();
                    //ddlYear.ClearSelection();
                    //ddlSem.ClearSelection();
                }

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_FeeCollectionOptions.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private void ShowSearchResults(string searchParams)
    {
        try
        {
            StudentSearch objSearch = new StudentSearch();

            string[] paramCollection = searchParams.Split(',');
            if (paramCollection.Length > 2)
            {
                for (int i = 0; i < paramCollection.Length; i++)
                {
                    string paramName = paramCollection[i].Substring(0, paramCollection[i].IndexOf('='));
                    string paramValue = paramCollection[i].Substring(paramCollection[i].IndexOf('=') + 1);

                    switch (paramName)
                    {
                    case "Name":
                    objSearch.StudentName = paramValue;
                    break;
                    case "EnrollNo":
                    objSearch.EnrollmentNo = paramValue;
                    break;
                    case "IdNo":
                    objSearch.IdNo = paramValue;
                    break;
                    case "SRNO":
                    objSearch.Srno = paramValue;
                    break;
                    case "DegreeNo":
                    objSearch.DegreeNo = int.Parse(paramValue);
                    break;
                    case "BranchNo":
                    objSearch.BranchNo = int.Parse(paramValue);
                    break;
                    case "YearNo":
                    objSearch.YearNo = int.Parse(paramValue);
                    break;
                    case "SemNo":
                    objSearch.SemesterNo = int.Parse(paramValue);
                    break;
                    default:
                    break;
                    }
                }
            }
            DataSet ds = feeController.GetStudents(objSearch);
            lvStudent.DataSource = ds;
            lvStudent.DataBind();
            objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvStudent);//Set label - 
       
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_FeeCollection.ShowSearchResults() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void GetStudDetails()
    {
        StudentFeedBackController SFB = new StudentFeedBackController();
        //if (txtSearch.Text.Trim() != string.Empty)
        //{
            if (ddlSemester.SelectedIndex > 0)
            {
                int ISCounter = Convert.ToInt32(objCommon.LookUp("ACD_COUNTER_REF", "COUNT(*)", "RECEIPT_PERMISSION IN('" + ddlReceiptType.SelectedValue + "')  AND UA_NO=" + Session["userno"]));//AND (REC1<>0 OR REC2<>0 OR REC3<>0 OR REC4<>0 OR REC5<>0)
                if (ISCounter != 0)
                {
                    //int studentId = feeController.GetStudentIdByEnrollmentNo(txtSearch.Text.Trim());
                    int studentId = Convert.ToInt32(hdnID.Value);
                    //hdnID.Value = studentId.ToString();
                    int semester = Convert.ToInt16(objCommon.LookUp("ACD_STUDENT", "SEMESTERNO", "IDNO=" + studentId + ""));

                    if (studentId > 0)
                    {

                    }
                    else
                        objCommon.DisplayUserMessage(updEdit, "No student found with given enrollment number.", this.Page);
                    return;
                }
                else
                {
                    objCommon.DisplayUserMessage(updEdit, "Counter is Not Assign To Generate Receipt No. Please Assign Counter For User := " + Session["userfullname"], this.Page);
                    return;
                }
            }
            else
                objCommon.DisplayUserMessage(updEdit, "Please select semester.", this.Page);
            return;
        //}
        //else
        //    objCommon.DisplayUserMessage(updEdit, "Please enter enrollment number.", this.Page);
        return;
    }
    private void LoadFeeCollectionOptions()
    {
        try
        {
            DataSet ds = feeController.GetFeeCollectionModes();
            if (ds.Tables[0].Rows.Count > 0)
            {
                GetStudDetails();
                lvFeeCollectionModes.DataSource = ds;
                lvFeeCollectionModes.DataBind();

            }
            else
            {
                //  objCommon.DisplayUserMessage(updEdit,"No Data Found.", this.Page);
                string str = "@alert('No Data Found.');";
                //ScriptManager.RegisterStartupScript(this, this.GetType(), str, "alert", true);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", str, true);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_FeeCollectionOptions.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            // Check user's authrity for Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=FeeCollection.aspx");
            }
        }
        else
        {
            // Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=FeeCollection.aspx");
        }
    }

    protected void ddlReceiptType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //if ((txtSearch.Text != "") || (txtSearch.Text != string.Empty))
            //{
                if (ddlSemester.SelectedIndex > 0)
                {
                    this.LoadFeeCollectionOptions();
                    this.lvFeeCollectionModes.Visible = true;
                }
                else
                {
                    lvFeeCollectionModes.Visible = false;
                    ddlReceiptType.SelectedIndex = 0;
                    objCommon.DisplayUserMessage(updEdit, "Please Select Semester", this.Page);
                }
            //}
            //else
            //{
            //    lvFeeCollectionModes.Visible = false;
            //    ddlSemester.SelectedIndex = 0;
            //    ddlReceiptType.SelectedIndex = 0;

            //    pnltextbox.Visible = true;
            //    pnlDropdown.Visible = false;
            //    divpanel.Attributes.Add("style", "display:block");

            //    objCommon.DisplayUserMessage(updEdit, "Please Enter Search String.", this.Page);
            //}
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_FeeCollectionOptions.ddlReceiptType_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlReceiptType.Focus();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_FeeCollectionOptions.ddlReceiptType_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    //protected void lnkId_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        LinkButton lnk = sender as LinkButton;
    //        //hdnID.Value = lnk.CommandArgument;
    //        ddlSemester.SelectedValue = lnk.ToolTip;
    //        txtSearch.Text = lnk.CommandName;
    //        //ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "Close Modal Popup", "Closepopup();", true);
    //        //"document.getElementById('id01').style.display='none'" 
    //        //string myScriptValue = "function callMe() {window.onclick = function(event) {if (event.target == modal) {modal.style.display = 'none';}} }";
    //        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "myScriptName", myScriptValue, true);
    //        ScriptManager.RegisterClientScriptBlock(this, GetType(), "none", "<script>myFunction();</script>", false);
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUaimsCommon.ShowError(Page, "Academic_FeeCollection.ShowSearchResults() --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUaimsCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //}
    protected void Clear()
    {
        ddlReceiptType.SelectedIndex = 0;
        txtSearch.Text = "";
        ddlDropdown.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;
        lvFeeCollectionModes.Visible = false;
        lvStudent.DataSource = null;
        lvStudent.DataBind();
        ddlSearch.SelectedIndex = 0;
        pnltextbox.Visible = false;
        pnlDropdown.Visible = false;
        divpanel.Attributes.Add("style", "display:none");
        divfooter.Visible = false;
        
    }
    protected void ClearSelection()
    {
        ddlReceiptType.SelectedIndex = 0;
        txtSearch.Text = "";
        ddlDropdown.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;
        lvFeeCollectionModes.Visible = false;
        lvStudent.DataSource = null;
        lvStudent.DataBind();

        //pnltextbox.Visible = false;
        ////txtSearch.Visible = false;
        //pnlDropdown.Visible = false;


    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
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
                        divpanel.Attributes.Add("style", "display:block");
                        divtxt.Visible = false;
                        lblDropdown.Text = ddlSearch.SelectedItem.Text;
                        objCommon.FillDropDownList(ddlDropdown, tablename, column1, column2, column1 + ">0", column1);

                    }
                    else
                    {
                        pnltextbox.Visible = true;
                        divtxt.Visible = true;
                        txtSearch.Visible = true;
                        pnlDropdown.Visible = false;
                        divpanel.Attributes.Add("style", "display:block");
                    }
                }
            }
            else
            {

                pnltextbox.Visible = false;
                pnlDropdown.Visible = false;
                divpanel.Attributes.Add("style", "display:none");
               
            }
            ClearSelection();
        }
        catch
        {
            throw;
        }
    }
    private void bindlist(string category, string searchtext)
    {

        StudentController objSC = new StudentController();
        DataSet ds = objSC.RetrieveStudentDetailsNew(searchtext, category);

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
            ddlSearch.ClearSelection();
            lblNoRecords.Text = "Total Records : 0";
            lvStudent.Visible = false;
            lvStudent.DataSource = null;
            lvStudent.DataBind();
        }
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        lblNoRecords.Visible = true;
        string value = string.Empty;
        if (ddlDropdown.SelectedIndex > 0)
        {
            value = ddlDropdown.SelectedValue;
        }
        else
        {
            value = txtSearch.Text;
        }
        bindlist(ddlSearch.SelectedItem.Text, value);
        ddlDropdown.ClearSelection();
        txtSearch.Text = string.Empty;
        pnltextbox.Visible = false;
        pnlDropdown.Visible = false;
        divpanel.Attributes.Add("style", "display:none");
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
            Label lblSem = lnk.Parent.FindControl("lblstudsemester") as Label;
            Session["stuinfoenrollno"] = lblenrollno.Text.Trim();
            Session["stuinfofullname"] = lnk.Text.Trim();
            int idno = Convert.ToInt32(lnk.CommandArgument);
            hdnID.Value = idno.ToString();
            txtSearch.Text = lblenrollno.Text;

            this.objCommon.FillDropDownList(ddlSemester, "ACD_DEMAND A INNER JOIN ACD_SEMESTER S  ON(A.SEMESTERNO=S.SEMESTERNO)", "DISTINCT A.SEMESTERNO", "SEMESTERNAME", "SEMESTERNAME <> '-' AND A.SEMESTERNO > 0 AND CAN=0 AND DELET=0 AND A.IDNO=" + idno, "SEMESTERNO");//

            //ddlSemester.SelectedValue = lblSem.Text;

            pnltextbox.Visible = true;
            pnlDropdown.Visible = false;
            divpanel.Attributes.Add("style", "display:block");
            pnlLV.Visible = false;
            lblNoRecords.Text = "";
            divfooter.Visible = true;
            ddlSemester.Focus();
            
            //if ((txtSearch.Text != "") || (txtSearch.Text != string.Empty))
            //{
            //    if (ddlSemester.SelectedIndex > 0)
            //    {
            //        this.LoadFeeCollectionOptions();
            //        this.lvFeeCollectionModes.Visible = true;
            //        pnlLV.Visible = false;
            //    }
            //    else
            //    {
            //        lvFeeCollectionModes.Visible = false;
            //        ddlReceiptType.SelectedIndex = 0;
            //        objCommon.DisplayUserMessage(updEdit, "Please Select Semester", this.Page);
            //    }
            //}
            //else
            //{
            //    lvFeeCollectionModes.Visible = false;
            //    ddlSemester.SelectedIndex = 0;
            //    ddlReceiptType.SelectedIndex = 0;
            //    objCommon.DisplayUserMessage(updEdit, "Please Enter Enrollment No.", this.Page);
            //}
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_FeeCollectionOptions.ddlReceiptType_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    #endregion
}