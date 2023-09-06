//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC
// PAGE NAME     : PHD ANNEXURE                                                     
// CREATION DATE : 30-JAN-2012                                                          
// CREATED BY    : ASHISH DHAKATE                             
// MODIFIED DATE :                 
// ADDED BY      :   Dipali Nanore                                
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


public partial class Academic_PhdWithdraw : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    GridView GV = new GridView();
    static int supstatus = 0; static int jntsupstatus = 0; static int secjntsupstatus = 0;
    static int instfacstatus = 0; static int drcstatus = 0; static int drcchairmanstatus = 0; static int deanstatus = 0;
    static int hodstatus = 0; static int hodNo = 0; static int AnnexureSatus = 0;

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
        //********************************
        string Sessionexam = string.Empty;
        //********************************

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

                //**************************************
                string ua_type = objCommon.LookUp("User_Acc", "UA_TYPE", "UA_NO=" + Convert.ToInt32(Session["userno"]));
                ViewState["usertype"] = ua_type;
                this.objCommon.FillDropDownList(ddlSearch, "ACD_SEARCH_CRITERIA", "ID", "CRITERIANAME", "ID > 0 AND IS_FEE_RELATED = 0", "SRNO");
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }

                ViewState["usertype"] = ua_type;
                if (ViewState["usertype"].ToString() == "2")
                {
                    dividno.Visible = false;
                    pnlId.Visible = false;
                    updEdit.Visible = false;
                    txtremark.Enabled = true;
                    ShowStudentDetails();
                    dgcremark.Visible = false;
                    divmain.Visible=true;
                }
                else
                {
                    //pnlId.Visible = true;
                    //updEdit.Visible = true;
                    divmain.Visible = false;
                    dividno.Visible = false;
                    updEdit.Visible = true;
                    pnlId.Visible = false;
                    txtremark.Enabled = false;
                    dgcremark.Visible = true;

                    if (Request.QueryString["id"] != null)
                    {
                        ViewState["action"] = "edit";
                        ShowStudentDetails();
                    }
                }

                if (ViewState["usertype"].ToString() == "4" || ViewState["usertype"].ToString() == "1")
                {
                    dgcremark.Visible = false;
                    btnexcel.Visible = true;
                }
                else if (ViewState["usertype"].ToString() != "2")
                { 
                btnexcel.Visible = false;
                dgcremark.Visible = true;
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
        divMsg.InnerHtml = string.Empty;
        
    }

    //  ---  show student details  -- 
    private void ShowStudentDetails()
    {
        PhdController objSC = new PhdController();
        DataTableReader dtr = null;
        if (ViewState["usertype"].ToString() == "2")
        {
            dtr = objSC.GetPhdWithdrawStudentDetails(Convert.ToInt32(Session["idno"]));
            btnSubmit.Text = "Submit";
            dgcremark.Visible = false;
        }
        else
        {
        if (Session["idno"] != null)
            {
            dtr = objSC.GetPhdWithdrawStudentDetails(Convert.ToInt32(Session["idno"].ToString()));
            }
            txtremark.Enabled = false;
            btnSubmit.Text = "Approval";
            dgcremark.Visible = true;
        }
        if (dtr != null)
        {
            if (dtr.Read())
            {
                txtIDNo.Text = dtr["IDNO"].ToString();
                lblEnrollNo.Text = dtr["ENROLLNO"].ToString();
                lblEnrollNo.ToolTip = dtr["IDNO"].ToString();
                lblstudname.Text = dtr["STUDNAME"].ToString();
                lbldepartment.Text = dtr["BRANCHNAME"].ToString();
                lbladmdate.Text = dtr["ADMDATE"].ToString();
               // lblcredits.Text = Convert.ToDecimal(dtr["CREDITS"]).ToString();
                lblstatus.Text = dtr["FULLPART"].ToString();
                lblsuperrole.Text = dtr["SUPERROLE"].ToString();
                lblreasearch.Text = dtr["RESEARCH"].ToString();
                lblsup.Text = dtr["SUPERVISORNAME"].ToString();
                lbljntsup.Text = dtr["JOINTSUPERVISORNAME"].ToString();
                lblinst.Text = dtr["INSTITUTEFACUTYNAME"].ToString();
                lbldrc.Text = dtr["DRCNAME"].ToString();
                lbldrcchairman.Text = dtr["DRCCHAIRNAME"].ToString();

                lblstatus.ToolTip = dtr["PHDSTATUS"].ToString();
                lblsuperrole.ToolTip = dtr["ROLEE"].ToString();
                lblsup.ToolTip = dtr["SUPERVISORNO"].ToString();
                lbljntsup.ToolTip = dtr["JOINTSUPERVISORNO"].ToString();
                lblinst.ToolTip = dtr["INSTITUTEFACULTYNO"].ToString();
                lbldrc.ToolTip = dtr["DRCNO"].ToString();
                lbldrcchairman.ToolTip = dtr["DRCCHAIRMANNO"].ToString();

                txtremark.Text = dtr["WITHDRAWREMARK"].ToString();
                supstatus = Convert.ToInt32(dtr["SUPERVISORSTATUS"].ToString());
                jntsupstatus = Convert.ToInt32(dtr["JOINTSUPERVISORSTATUS"].ToString());
                instfacstatus = Convert.ToInt32(dtr["INSTITUTEFACULTYSTATUS"].ToString());
                drcstatus = Convert.ToInt32(dtr["DRCSTATUS"].ToString());
                drcchairmanstatus = Convert.ToInt32(dtr["DRCCHAIRMANSTATUS"].ToString());
                deanstatus = Convert.ToInt32(dtr["DEAN_STATUS"].ToString());
                //------- add hod status condition --------//
               // hodNo = Convert.ToInt32(dtr["HODNO"].ToString());
                hodstatus = Convert.ToInt32(dtr["HODSTATUS"].ToString());
                AnnexureSatus = Convert.ToInt32(dtr["ANNEXURESTATUS"].ToString());
                divmain.Visible = true;
                updEdit.Visible = false;
                // if student register annexure a then withdraw the annexurestatus = 1 else 0
                // 
                if (txtremark.Text != string.Empty || txtremark.Text != "")
                {
                    txtremark.Enabled = false;
                    if (ViewState["usertype"].ToString() == "2")
                    {
                        objCommon.DisplayMessage("Your Registration for Withdraw is Already Done", this.Page);
                        btnSubmit.Enabled = false;
                    }
                }

                if (AnnexureSatus == 1)
                {
                    if (hodstatus == 1 && supstatus == 1 && jntsupstatus == 1 && instfacstatus == 1 && drcstatus == 1 && drcchairmanstatus == 1 && deanstatus == 1)
                    {
                        btnReport.Visible = true;
                    }
                    else
                    {
                        btnReport.Visible = false;
                    }
                }
                else
                {
                    if (hodstatus == 1 && drcchairmanstatus == 1 && deanstatus == 1)
                    {
                        btnReport.Visible = true;
                    }
                    else
                    {
                        btnReport.Visible = false;
                    }

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
                Response.Redirect("~/notauthorized.aspx?page=PhdAnnexure.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=PhdAnnexure.aspx");
        }
    }

    private void ClearControl()
    {
        txtIDNo.Text = string.Empty;
    }

    //  --- submit methode --
    private void SubmitData(string Action)
    {
        PhdController objSC = new PhdController();
        Student objS = new Student();
        try
        {
            objS.IdNo = Convert.ToInt32(lblEnrollNo.ToolTip.ToString());
            if (ViewState["usertype"].ToString() == "2")
            {
                objS.Remark = txtremark.Text.ToString();
            }
            else
            {
                objS.Remark = txtdgcremark.Text.ToString();
            }

            objS.SuperRole = Action;
            int status = Convert.ToInt32(objSC.AddPhdWithdrawDetails(objS));
            if (status == 1)
            {
                if (Action == "STU") { objCommon.DisplayMessage("Student Register For PhD Withdraw Successfully!", this.Page); }
                if (Action == "HOD") { objCommon.DisplayMessage("HOD Approve Successfully!", this.Page); }
                if (Action == "SUP") { objCommon.DisplayMessage("Supervisior Approve Successfully!", this.Page); }
                if (Action == "JSP") { objCommon.DisplayMessage("Joint Supervisior Approve Successfully!", this.Page); }
                if (Action == "INS") { objCommon.DisplayMessage("Institute Faculty Approve Successfully!", this.Page); }
                if (Action == "DR")  { objCommon.DisplayMessage("Drc Nominee Approve Successfully!", this.Page); }
                if (Action == "DCH") { objCommon.DisplayMessage("Drc Chairman Approve Successfully!", this.Page); }
                if (Action == "DN") { objCommon.DisplayMessage("Dean Approve Successfully!", this.Page); }
                ShowStudentDetails();
            }
            else
            {
                objCommon.DisplayMessage("Something Get Wrong!!", this.Page);
            }
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentInfoEntry.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

            this.ClearControl();
        }
    }

    protected void lnkId_Click(object sender, EventArgs e)
    {
        LinkButton lnk = sender as LinkButton;
        string url = string.Empty;
        if (Request.Url.ToString().IndexOf("&id=") > 0)
        {
            url = Request.Url.ToString().Remove(Request.Url.ToString().IndexOf("&id="));
        }
        else
        {
            url = Request.Url.ToString();
        }
        
        //Response.Redirect(url + "&id=" + lnk.CommandArgument);
        Session["idno"] = Convert.ToInt32(lnk.CommandArgument);
        ShowStudentDetails();
        //Response.Redirect(url + "&id="+Session["IDNO"]);
    }
    
    private void bindlist(string category, string searchtext)
    {
        string branchno = "0";

        StudentController objSC = new StudentController();
        DataSet ds = objSC.RetrieveStudentDetailsPHD(searchtext, category, branchno);

        if (ds.Tables[0].Rows.Count > 0)
        {
            lvStudent.DataSource = ds;
            lvStudent.DataBind();
            lblNoRecords.Text = "Total Records : " + ds.Tables[0].Rows.Count.ToString();
        }
        else
            lblNoRecords.Text = "Total Records : 0";

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearControl();
        Response.Redirect(Request.Url.ToString());
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (ViewState["usertype"].ToString() == "2")
        {
            SubmitData("STU");
        }

        else if (ViewState["usertype"].ToString() == "4")
        {
            if (AnnexureSatus == 1)
            {
                if (hodstatus == 1 && supstatus == 1 && jntsupstatus == 1 && instfacstatus == 1 && drcstatus == 1 && drcchairmanstatus == 1)
                {
                    SubmitData("DN");
                }
                else
                {
                    objCommon.DisplayMessage("DGC and DRC Member Approval is Pending", this.Page);
                }
            }
            else
            {
                if (hodstatus == 1 && drcchairmanstatus == 1)
                {
                    SubmitData("DN");
                }
                else
                {
                    objCommon.DisplayMessage("HOD OR DRC Member Approval is Pending", this.Page);
                }
            }
        }
        else
        {
            int Count = Convert.ToInt32(objCommon.LookUp("ACD_PHD_WITHDRAW", "Count(idno)IDNo", "IDNO=" + Convert.ToInt32(lblEnrollNo.ToolTip.ToString())));

            if (Count > 0)
            {
                if (txtdgcremark.Text != string.Empty)
                {
                    if (AnnexureSatus == 1)
                    {
                        if (hodstatus == 0 && hodNo == Convert.ToInt32(Session["userno"].ToString()))
                        {
                            if (txtdgcremark.Text != string.Empty)
                            { SubmitData("HOD"); }
                            else
                            { objCommon.DisplayMessage("Please Enter Remark!", this.Page); }

                        }
                        else if (hodstatus == 1 && supstatus == 0 && lblsup.ToolTip.ToString() == Session["userno"].ToString())
                        {
                            if (txtdgcremark.Text != string.Empty)
                            { SubmitData("SUP"); }
                            else
                            { objCommon.DisplayMessage("Please Enter Remark!", this.Page); }
                        }
                        else if (hodstatus == 1)
                        {
                            if (supstatus == 1)
                            {
                                if (jntsupstatus == 0 && lbljntsup.ToolTip.ToString() == Session["userno"].ToString())
                                {
                                    if (txtdgcremark.Text != string.Empty)
                                    { SubmitData("JSP"); }
                                    else { objCommon.DisplayMessage("Please Enter Remark!", this.Page); }
                                }
                                if (instfacstatus == 0 && lblinst.ToolTip.ToString() == Session["userno"].ToString())
                                {
                                    if (txtdgcremark.Text != string.Empty) { SubmitData("INS"); }
                                    else { objCommon.DisplayMessage("Please Enter Remark!", this.Page); }
                                }
                                if (drcstatus == 0 && lbldrc.ToolTip.ToString() == Session["userno"].ToString())
                                {
                                    if (txtdgcremark.Text != string.Empty) { SubmitData("DR"); }
                                    else { objCommon.DisplayMessage("Please Enter Remark!", this.Page); }
                                }
                                if (supstatus == 1 && jntsupstatus == 1 && instfacstatus == 1 && drcstatus == 1)
                                {
                                    if (drcchairmanstatus == 0 && lbldrcchairman.ToolTip.ToString() == Session["userno"].ToString())
                                    {
                                        if (txtdgcremark.Text != string.Empty) { SubmitData("DCH"); }
                                        else { objCommon.DisplayMessage("Please Enter Remark!", this.Page); }
                                    }
                                }
                                else
                                {
                                    objCommon.DisplayMessage("DGC Member Approval is Pending", this.Page);
                                }
                            }
                            else
                            {
                                objCommon.DisplayMessage("Supervisior Approval is Pending", this.Page);
                            }
                        }
                        else
                        {
                            objCommon.DisplayMessage("HOD Approval is Pending", this.Page);
                        }
                    }
                    else
                    {
                        if (hodstatus == 0 && hodNo == Convert.ToInt32(Session["userno"].ToString()))
                        {
                            if (txtdgcremark.Text != string.Empty)
                            {
                                SubmitData("HOD");
                            }
                            else { objCommon.DisplayMessage("Please Enter Remark!", this.Page); }
                        }
                        else if (hodstatus == 1 && drcchairmanstatus == 0 && lbldrcchairman.ToolTip.ToString() == Session["userno"].ToString())
                        {
                            if (txtdgcremark.Text != string.Empty) { SubmitData("DCH"); }
                            else { objCommon.DisplayMessage("Please Enter Remark!", this.Page); }
                        }
                        else
                        {
                            objCommon.DisplayMessage("HOD OR DRC Chairman Approval is Pending", this.Page);
                        }
                    }
                }
                else { objCommon.DisplayMessage("Please Enter Remark!", this.Page); }
            }
            else
            {
                objCommon.DisplayMessage("Student Not Register For Withdraw.", this.Page);
            }
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

    private DataRow GetEditableDataRow(DataTable dt, string value)
    {
        DataRow dataRow = null;
        try
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["QUALIFYNO"].ToString() == value)
                {
                    dataRow = dr;
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic.GetEditableDataRow() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return dataRow;
    }


    protected void btnReport_Click(object sender, EventArgs e)
    {
        ShowReport("PHDWithdraw", "rptPhdWithdraw.rpt");
    }

    protected void btnApply_Click(object sender, EventArgs e)
    {
        ShowReportApplystudent("PHDApplystudent", "Phd_WithDraw_Stud_Apply.rpt");
    }
    private void ShowReportApplystudent(string reportTitle, string rptFileName)
    {
        try
        {
            string ua_type = objCommon.LookUp("User_Acc", "UA_TYPE", "UA_NO=" + Convert.ToInt32(Session["userno"]));
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            //  url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString();
            if (ua_type == "2")
            {
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + Convert.ToInt32(txtIDNo.Text);
            }
            else
            {
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO= 0";
            }

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

    protected void btnexcel_Click(object sender, EventArgs e)
    {
        PhdController objSC = new PhdController();
        DataSet ds = null;
        ds = objSC.RetrievePhdWithdrawExcel();
        if (ds.Tables[0].Rows.Count > 0)
        {
            GV.DataSource = ds;
            GV.DataBind();
            this.CallExcel();
        }
        else
        {
            objCommon.DisplayMessage("Record Not Found!!", this.Page);
            return;
        }

    }

    // -- add excel report on 23032018 ---// 
    private void CallExcel()
    {
        string attachment = "attachment; filename=Phd_WithDraw_Details " + DateTime.Today.ToString() + ".xls";

        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/vnd.MS-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        GV.HeaderRow.Style.Add("background-color", "#e3ac9a");
        GV.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();
    }

    protected void ddlSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
        try
            {
            //Panel3.Visible = false;
            lblNoRecords.Visible = false;
            lvStudent.DataSource = null;
            lvStudent.DataBind();
            //ddlSearch.SelectedIndex = 2;
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

    protected void btnClose_Click(object sender, EventArgs e)
        {
        Response.Redirect(Request.Url.ToString());

        }
}



