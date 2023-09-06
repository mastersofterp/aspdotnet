//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC
// PAGE NAME     : PHD ANNEXURE-B                                                  
// CREATION DATE : 25-APRIL-2013                                                          
// CREATED BY    : ASHISH DHAKATE                             
// MODIFIED DATE :                 
// ADDED BY      :                                  
// MODIFIED DESC :                                                                      
//======================================================================================
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data.Common;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;


public partial class Academic_StudentInfoEntry : System.Web.UI.Page
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
               // CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }

                //Populate all the DropDownLists
                //FillDropDown();

                this.objCommon.FillDropDownList(ddlSearch, "ACD_SEARCH_CRITERIA", "ID", "CRITERIANAME", "ID > 0 AND IS_FEE_RELATED = 0", "SRNO");
                if (ViewState["action"] == null)
                    ViewState["action"] = "add";

                string ua_type = objCommon.LookUp("User_Acc", "UA_TYPE", "UA_NO=" + Convert.ToInt32(Session["userno"]));  //--- change ua_idno by  ua_no 


                ViewState["usertype"] = ua_type;
                if (ViewState["usertype"].ToString() == "2")
                {
                    pnlId.Visible = false;
                    divmain.Visible = true;
                    ShowStudentDetails();
                    ViewState["action"] = "edit";

                }
                else
                {

                    string ua_type_fac = objCommon.LookUp("User_Acc", "UA_TYPE", "UA_NO=" + Convert.ToInt32(Session["userno"]));
                    if (ua_type_fac == "3")
                    {
                        //pnlDoc.Enabled = false;
                        pnlId.Enabled = false;
                        updEdit.Visible = true;
                        divmain.Visible = false;
                        divfooter.Visible = false;

                    }

                    //pnlId.Visible = true;
                    lblRegNo.Enabled = true;


                    if (Request.QueryString["id"] != null)
                    {

                        ViewState["action"] = "edit";
                        ShowStudentDetails();
                        //ShowSignDetails();

                    }
                }

            }
        }
        else
        {
            if (Page.Request.Params["__EVENTTARGET"] != null)
            {
                if (Page.Request.Params["__EVENTTARGET"].ToString().ToLower().Contains("btnsearch"))
                {
                    string[] arg = Page.Request.Params["__EVENTARGUMENT"].ToString().Split(',');
                    bindlist(arg[0], arg[1]);
                }

                if (Page.Request.Params["__EVENTTARGET"].ToString().ToLower().Contains("btncancelmodal"))
                {
                    txtSearch.Text = string.Empty;
                    lvStudent.DataSource = null;
                    lvStudent.DataBind();
                    lblNoRecords.Text = string.Empty;
                }
            }
        }
    }

    private void bindlist(string category, string searchtext)
    {

        //int dept = Convert.ToInt32(objCommon.LookUp("USER_ACC", "UA_DEPTNO", "UA_NO=" + Convert.ToInt32(Session["userno"])));

        //if (dept > 0)
        //{
        //    string branchno = objCommon.LookUp("ACD_BRANCH", "BRANCHNO", "DEGREENO=6 AND DEPTNO=" + dept);


        //    StudentController objSC = new StudentController();
        //    DataSet ds = objSC.RetrieveStudentDetailsPHD(searchtext, category, branchno);

        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        lvStudent.DataSource = ds;
        //        lvStudent.DataBind();
        //        lblNoRecords.Text = "Total Records : " + ds.Tables[0].Rows.Count.ToString();
        //    }
        //    else
        //        lblNoRecords.Text = "Total Records : 0";
        //}
        //else
        //{
        string branchno = "0";

        PhdController objSC = new PhdController();
        DataSet ds = objSC.RetrieveStudentDetailsPHD(searchtext, category, branchno);

        if (ds.Tables[0].Rows.Count > 0)
        {
            lvStudent.DataSource = ds;
            lvStudent.DataBind();
            lblNoRecords.Text = "Total Records : " + ds.Tables[0].Rows.Count.ToString();
        }
        else
            lblNoRecords.Text = "Total Records : 0";
        //}
    }

    protected void lnkId_Click(object sender, EventArgs e)
    {
        LinkButton lnk = sender as LinkButton;
        string url = string.Empty;
        if (Request.Url.ToString().IndexOf("&id=") > 0)
        {
            url = Request.Url.ToString().Remove(Request.Url.ToString().IndexOf("&id="));
        }
        //else
        //{
        //    url = Request.Url.ToString();
        //}

        //Response.Redirect(url + "&id=" + lnk.CommandArgument);
        int idno = Convert.ToInt32(lnk.CommandArgument);
        Session["stuinfoidno"] = idno;
        ShowStudentDetails();
        updEdit.Visible = false;
        divmain.Visible = true;
        divfooter.Visible = true;
    }

    private void ChangeControlStatus(bool status)
    {

        foreach (Control c in Page.Controls)
            foreach (Control ctrl in c.Controls)

                if (ctrl is TextBox)

                    ((TextBox)ctrl).Enabled = status;

                else if (ctrl is Button)

                    ((Button)ctrl).Enabled = status;

                else if (ctrl is RadioButton)

                    ((RadioButton)ctrl).Enabled = status;

                else if (ctrl is ImageButton)

                    ((ImageButton)ctrl).Enabled = status;

                else if (ctrl is CheckBox)

                    ((CheckBox)ctrl).Enabled = status;

                else if (ctrl is DropDownList)

                    ((DropDownList)ctrl).Enabled = status;

                else if (ctrl is HyperLink)

                    ((HyperLink)ctrl).Enabled = status;

    }

    private void ShowStudentDetails()
    {
        string count = objCommon.LookUp("ACD_PHD_STUD_ANNEXUREB", "COUNT(IDNO)", "IDNO=" + Convert.ToInt32(Session["idno"]) + " AND COURSECOMP=1" + " AND STATUS=1");
        StudentController objSC = new StudentController();
        DataTableReader dtr = null;
        if (ViewState["usertype"].ToString() == "2")
        {
            if (Convert.ToInt32(count) > 0)
            {
                dtr = objSC.GetStudentPHDDetails(Convert.ToInt32(Session["idno"]));
            }
            else
            {
                objCommon.DisplayMessage("PhD Annexure B is Incomplete or Not Approved !!", this.Page);
                btnReport.Enabled = false; // Added on 05/12/2015
            }
        }
        else
        {
        dtr = objSC.GetStudentPHDDetails(Convert.ToInt32(Session["stuinfoidno"].ToString()));
            pnDisplay.Enabled = true;
        }
        if (dtr != null)
        {
            if (dtr.Read())
            {
                txtIDNo.Text = dtr["IDNO"].ToString();
                //txtIDNo.ToolTip = dtr["REGNO"].ToString();
                lblRegNo.ToolTip = dtr["IDNO"].ToString();
                lblEnrollNo.ToolTip = dtr["ENROLLNO"].ToString();
                lblEnrollNo.Text = dtr["ENROLLNO"].ToString();
                lblRegNo.Text = dtr["IDNO"].ToString();
                //txtRegNo.Enabled = false;
                lblStudName.Text = dtr["STUDNAME"] == null ? string.Empty : dtr["STUDNAME"].ToString();
                lblFatherName.Text = dtr["FATHERNAME"] == null ? string.Empty : dtr["FATHERNAME"].ToString().ToUpper();
                lblDateOfJoining.Text = dtr["ADMDATE"] == DBNull.Value ? "" : Convert.ToDateTime(dtr["ADMDATE"]).ToString("dd/MM/yyyy");
                lblBranch.Text = dtr["BRANCHNAME"] == null ? string.Empty : dtr["BRANCHNAME"].ToString();
                lblBranch.ToolTip = dtr["BRANCHNO"] == null ? string.Empty : dtr["BRANCHNO"].ToString();
                lblStatus.Text = dtr["PHDSTATUS"] == null ? string.Empty : dtr["PHDSTATUS"].ToString();
                if (lblStatus.Text == "2")
                {
                    lblStatus.Text = "PART TIME";
                }
                else
                {
                    lblStatus.Text = "FULL TIME";
                }

                lblSupervisor.Text = dtr["PHDSUPERVISORNAME"] == null ? string.Empty : dtr["PHDSUPERVISORNAME"].ToString();
                lblSupervisor.ToolTip = dtr["PHDSUPERVISORNO"] == null ? string.Empty : dtr["PHDSUPERVISORNO"].ToString();
                btnReport.Enabled = true; // Added on 05/12/2015
            }
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString()) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=studentinfoentry.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=studentinfoentry.aspx");
        }
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + Convert.ToInt32(txtIDNo.Text);
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            //ScriptManager.RegisterClientScriptBlock(this.updBatch, this.updBatch.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_PhdAnnexure.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnReport_Click1(object sender, EventArgs e)
    {
        ShowReport("PhdAnnexureCReport", "rptAnnexureC.rpt");
    }

    protected void btnSearch_Click(object sender, EventArgs e)
        {
        //// Panel1.Visible = true;
        // lblNoRecords.Visible = true;
        // //divbranch.Attributes.Add("style", "display:none");
        // //divSemester.Attributes.Add("style", "display:none");
        // //divtxt.Attributes.Add("style", "display:none");
        // string value = string.Empty;
        // if (ddlDropdown.SelectedIndex > 0)
        // {
        //     value = ddlDropdown.SelectedValue;
        // }
        // else
        // {
        //     value = txtSearch.Text;
        // }

        // //ddlSearch.ClearSelection();

        // bindlist(ddlSearch.SelectedItem.Text, value);
        // ddlDropdown.ClearSelection();
        // txtSearch.Text = string.Empty;
        //// div_Studentdetail.Visible = false;
        // //divMSG.Visible = false;
        // //btnPayment.Visible = false;
        //// btnReciept.Visible = false;
        //// divPreviousReceipts.Visible = false;
        // //if (value == "BRANCH")
        // //{
        // //    divbranch.Attributes.Add("style", "display:block");

        // //}
        // //else if (value == "SEM")
        // //{
        // //    divSemester.Attributes.Add("style", "display:block");
        // //}
        // //else
        // //{
        // //    divtxt.Attributes.Add("style", "display:block");
        // //}

        // //ShowDetails();
        // Panel3.Visible = true;

        Panellistview.Visible = true;

        lblNoRecords.Visible = true;
        //divbranch.Attributes.Add("style", "display:none");
        //divSemester.Attributes.Add("style", "display:none");
        //divtxt.Attributes.Add("style", "display:none");
        string value = string.Empty;
        if (ddlDropdown.SelectedIndex > 0)
            {
            value = ddlDropdown.SelectedValue;
            }
        else
            {
            value = txtSearch.Text;
            }

        //ddlSearch.ClearSelection();

        bindlist(ddlSearch.SelectedItem.Text, value);
        ddlDropdown.ClearSelection();
        txtSearch.Text = string.Empty;

        }
    protected void ddlSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
        try
            {
            //Panel3.Visible = false;
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


                        //if(ddlSearch.SelectedItem.Text.Equals("BRANCH"))
                        //{
                        //    objCommon.FillDropDownList(ddlDropdown, "ACD_COLLEGE_DEGREE_BRANCH CDB INNER JOIN ACD_BRANCH B ON(CDB.BRANCHNO = B.BRANCHNO)", "DISTINCT B.BRANCHNO", "B.LONGNAME", "B.BRANCHNO>0 AND CDB.OrganizationId =" + Convert.ToInt32(Session["OrgId"]), "B.BRANCHNO");
                        //}
                        //else if(ddlSearch.SelectedItem.Text.Equals("SEMESTER"))
                        //{
                        //    objCommon.FillDropDownList(ddlDropdown, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0", "SEMESTERNO");
                        //}
                        }
                    else
                        {
                        pnltextbox.Visible = true;
                        divtxt.Visible = true;
                        txtSearch.Visible = true;
                        pnlDropdown.Visible = false;

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
        txtSearch.Text = string.Empty;
        }

    protected void btnClose_Click(object sender, EventArgs e)
        {
        Response.Redirect(Request.Url.ToString());

        }
}



