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
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.IO;
using ClosedXML.Excel;


public partial class ACADEMIC_Prospectus_Entry : System.Web.UI.Page
{
    #region Page Events

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
                // this.CheckPageAuthorization();
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
            }
            PopulateDropDownList();
            // txtAdmBatch.Text = objCommon.LookUp("ACD_ADMBATCH", "TOP (1) BATCHNAME", "BATCHNO=(SELECT MAX(BATCHNO) FROM ACD_ADMBATCH)");
            objCommon.FillDropDownList(ddlAdmBatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "ACTIVESTATUS=1 AND ISNULL(IS_ADMSSION,0)=1", "");
            ddlAdmBatch.SelectedIndex = 1;
            //ViewState["action"] = "add";
        }
        objCommon.SetLabelData("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]));//Set label -  
        objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));  // Set Page Header  -  
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Prospectus_Entry.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Prospectus_Entry.aspx");
        }
    }

    private void PopulateDropDownList()
    {
        objCommon.FillDropDownList(ddlSchool, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0 and ISNULL(ActiveStatus,0)=1", "COLLEGE_ID");
    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDegree.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlBranch, "ACD_COLLEGE_DEGREE_BRANCH CD INNER JOIN ACD_BRANCH B ON (B.BRANCHNO = CD.BRANCHNO)", "DISTINCT CD.BRANCHNO", "B.LONGNAME", "CD.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND CD.BRANCHNO > 0 AND ISNULL(B.ISCORE,0)=0 AND CD.COLLEGE_ID=" + Convert.ToInt32(ddlSchool.SelectedValue), "B.LONGNAME");

            //objCommon.FillDropDownList(ddlBranch, "ACD_COLLEGE_DEGREE_BRANCH CD INNER JOIN ACD_BRANCH B ON (B.BRANCHNO = CD.BRANCHNO)", "DISTINCT CD.BRANCHNO", "B.LONGNAME", "CD.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND CD.BRANCHNO > 0 AND ISNULL(B.ACTIVESTATUS,0)=1", "B.LONGNAME");
            ddlBranch.Focus();
        }
        else
        {
            ddlBranch.Items.Clear();
            ddlBranch.Items.Add(new ListItem("Please Select", "0"));
        }
    }

    #endregion


    protected void btnSave_Click(object sender, EventArgs e)
    {
        StudentController objSC = new StudentController();
        Student objS = new Student();

        int count = Convert.ToInt32(objCommon.LookUp("ACD_PROSPECTUS", "count(PROSPECTUSNO)", "PROSPECTUSNO='" + txtProspectusno.Text + "'"));

        if (count > 0)
        {
            objCommon.DisplayMessage(this.updBatch, "Prospectus No. already exist!", this.Page);
            txtProspectusno.Text = "";
        }
        else
        {
            string admbatch = objCommon.LookUp("ACD_ADMBATCH", "TOP (1) BATCHNAME", "BATCHNO=(SELECT MAX(BATCHNO) FROM ACD_ADMBATCH)");

            objS.StudName = txtStudName.Text.Trim();
            objS.StudentMobile = txtMobile.Text.Trim();
            objS.EmailID = txtEmail.Text.Trim();
            objS.Collegeid = Convert.ToInt32(ddlSchool.SelectedValue);
            objS.DegreeNo = Convert.ToInt32(ddlDegree.SelectedValue);

            if (ddlSpecialisation.SelectedIndex > 0)
            {
                objS.BranchNo = Convert.ToInt32(ddlSpecialisation.SelectedValue);
            }
            else
            {
                objS.BranchNo = Convert.ToInt32(ddlBranch.SelectedValue);
            }
            //objS.AdmBatch = Convert.ToInt32(objCommon.LookUp("ACD_ADMBATCH", "BATCHNO", "BATCHNAME='" + txtAdmBatch.Text + "'"));
            objS.AdmBatch = Convert.ToInt32(ddlAdmBatch.SelectedValue);
            objS.ProspectusNo = txtProspectusno.Text.Trim();
            objS.EntryDate = DateTime.Now;
            objS.Uano = Convert.ToInt32(Session["userno"].ToString());
            objS.IPADDRESS = Session["ipAddress"].ToString();
            objS.CollegeCode = Session["colcode"].ToString();
            objS.ReceiptNo = txtReceiptNo.Text.Trim();
            objS.TotalAmt = Convert.ToInt32(txtTotalAmount.Text.Trim());
            //objS.Remarks = txtrem.Text.Trim();          
            objS.Remarks = ddlRemark.SelectedItem.Text;

            CustomStatus cs = (CustomStatus)objSC.InsertProspectusInfo(objS);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                ViewState["action"] = "add";
                objCommon.DisplayMessage(this.updBatch, "Prospectus Entry Done Successfully!. Now you can print receipt to click on Prospectus Receipt button.", this.Page);
                btnShowReport.Enabled = true;
                ClearAll();
            }
            else
            {
                objCommon.DisplayMessage(this.updBatch, "Record Not Saved!", this.Page);
            }
        }

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearAll();
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Convert.ToInt32(ViewState["college_id"]);
            
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updBatch, this.updBatch.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnShowReport_Click(object sender, EventArgs e)
    {
        int organizationid = Convert.ToInt32(Session["OrgId"].ToString());
        if (organizationid == 3 || organizationid == 4)
        {
            ShowReport("Prospectus", "ProspectusReceipt_cpukota.rpt");
        }
        else
        {
            ShowReport("Prospectus", "ProspectusReceipt.rpt");
        }
    }

    private void ClearAll()
    {
        txtStudName.Text = string.Empty;
        ddlDegree.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        ddlSchool.SelectedIndex = 0;
        txtMobile.Text = string.Empty;
        txtEmail.Text = string.Empty;
        txtProspectusno.Text = string.Empty;
        txtTotalAmount.Text = string.Empty;
        //txtrem.Text = string.Empty;
        ddlRemark.SelectedIndex = 0;
    }


    protected void ddlSchool_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlBranch.Items.Clear();
        ddlBranch.Items.Add(new ListItem("Please Select", "0"));
        if (ddlSchool.SelectedIndex > 0)
        {
            ViewState["college_id"] = Convert.ToInt32(ddlSchool.SelectedIndex).ToString();
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "ISNULL(D.ACTIVESTATUS,0)= 1 AND D.DEGREENO > 0 AND B.COLLEGE_ID=" + ddlSchool.SelectedValue, "D.DEGREENO");
            ddlDegree.Focus();
        }
        else
        {
            ddlBranch.Items.Clear();
            ddlBranch.Items.Add(new ListItem("Please Select", "0"));
            ddlDegree.Items.Clear();
            ddlDegree.Items.Add(new ListItem("Please Select", "0"));
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {

        showstudent();

        string proschk = Session["invalid"].ToString();
        if (proschk == string.Empty)
        {
            divdet.Visible = false;
        }
        //if (proschk != string.Empty) 
        else
        {
            divdet.Visible = true;
            objCommon.SetLabelData("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]));//Set label -  
        }
    }

    public void showstudent()
    {
        StudentController objSC = new StudentController();
        DataSet ds = new DataSet();

        string prospectno = txtprosno.Text.Trim();
        string prosno = Convert.ToString(objCommon.LookUp("ACD_PROSPECTUS", "PROSNO", "PROSPECTUSNO='" + prospectno + "'"));
        Session["invalid"] = prosno;
        if (prosno == string.Empty)
        {
            objCommon.DisplayMessage(this.Page, "Prospectus No. " + prospectno + " is Invalid.", this.Page);
        }
        else
        {

            Session["prosno"] = prosno;
            ds = objSC.GetStuddetprosprint(Convert.ToInt32(prosno));

            if (ds.Tables[0].Rows.Count > 0)
            {
                lblprosno.Text = ds.Tables[0].Rows[0]["PROSPECTUSNO"].ToString();
                lblname.Text = ds.Tables[0].Rows[0]["STUDENT_NAME"] == null ? string.Empty : ds.Tables[0].Rows[0]["STUDENT_NAME"].ToString();
                lblname.Text = lblname.Text.ToUpper();
                lblmail.Text = ds.Tables[0].Rows[0]["EMAIL"] == null ? string.Empty : ds.Tables[0].Rows[0]["EMAIL"].ToString();
                lblmob.Text = ds.Tables[0].Rows[0]["MOBILE"] == null ? string.Empty : ds.Tables[0].Rows[0]["MOBILE"].ToString();
                lblschool.Text = ds.Tables[0].Rows[0]["COLLEGE_NAME"] == null ? string.Empty : ds.Tables[0].Rows[0]["COLLEGE_NAME"].ToString();
                lbldegree.Text = ds.Tables[0].Rows[0]["DEGREENAME"] == null ? string.Empty : ds.Tables[0].Rows[0]["DEGREENAME"].ToString();
                lblbranch.Text = ds.Tables[0].Rows[0]["LONGNAME"] == null ? string.Empty : ds.Tables[0].Rows[0]["LONGNAME"].ToString();
                lblbatch.Text = ds.Tables[0].Rows[0]["BATCHNAME"] == null ? string.Empty : ds.Tables[0].Rows[0]["BATCHNAME"].ToString();
                lblamt.Text = ds.Tables[0].Rows[0]["TOTAL_AMT"] == null ? string.Empty : ds.Tables[0].Rows[0]["TOTAL_AMT"].ToString();
            }
            else
            {
                objCommon.DisplayMessage(this.updBatch, "Please enter valid Prospectus No.", this.Page);
            }
        }
    }

    protected void btnreprint_Click(object sender, EventArgs e)
    {
        try
        {
            int organizationid = Convert.ToInt32(Session["OrgId"].ToString());

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + "Prospectus";
            if (organizationid == 3 || organizationid == 4)
            {
                url += "&path=~,Reports,Academic," + "ProspectusReprintRec_cpukota.rpt";
            }
            else
            {
                url += "&path=~,Reports,Academic," + "ProspectusReprintRec.rpt";
            }
            url += "&param=@P_PROSNO=" + Session["prosno"].ToString();
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + "Prospectus" + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updReprint, this.updReprint.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            throw;
        }

        txtprosno.Text = string.Empty;
        divdet.Visible = false;

        //Server.Transfer(Request.Url.AbsolutePath);
        //Response.Redirect(Request.RawUrl);
    }


    //protected void txtProspectusno_TextChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        //int count = Convert.ToInt32(objCommon.LookUp("ACD_PROSPECTUS", "count(PROSPECTUSNO)", "PROSPECTUSNO='" + Convert.ToInt32(txtProspectusno.Text) + "'"));

    //        //if (count > 0)
    //        //{
    //        //    objCommon.DisplayMessage(this.updBatch, "Prospectus No. already exist!", this.Page);
    //        //    txtProspectusno.Text = "";
    //        //}
    //        //else
    //        //{

    //        //}
    //    }
    //    catch (Exception ex)
    //    {
    //    }
    //}
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBranch.SelectedIndex > 0)
        {
            int count = Convert.ToInt32(objCommon.LookUp("ACD_COLLEGE_DEGREE_BRANCH", "count(1)", "CORE_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + " AND DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND COLLEGE_ID=" + Convert.ToInt32(ddlSchool.SelectedValue)));
            if (count > 0)
            {
                divSpecialisation.Visible = true;
                objCommon.FillDropDownList(ddlSpecialisation, "ACD_COLLEGE_DEGREE_BRANCH CD INNER JOIN ACD_BRANCH B ON (B.BRANCHNO = CD.BRANCHNO)", "DISTINCT CD.BRANCHNO", "B.LONGNAME", "CD.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND ISNULL(B.ISCORE,0)=1 AND ISNULL(ISSPECIALISATION,0) = 1 AND CD.COLLEGE_ID=" + Convert.ToInt32(ddlSchool.SelectedValue) + " AND CD.CORE_BRANCHNO =" + Convert.ToInt32(ddlBranch.SelectedValue), "B.LONGNAME");

            }
            else
            {
                divSpecialisation.Visible = false;
            }
        }
        else
        {
            divSpecialisation.Visible = false;

        }

    }

    protected void btnExcelReport_Click(object sender, EventArgs e)
    {
        ExportinExcel();
    }

    private void ExportinExcel()
    {
        StudentController objSC = new StudentController();
        string attachment = "attachment; filename=" + "ProspectusEntryExcelReport_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") +".xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/" + "ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);

        DataSet dsfee = objSC.Get_ProspectusEntryExcelReport(Convert.ToInt32(ddlAdmBatch.SelectedValue));
        DataGrid dg = new DataGrid();
        if (dsfee.Tables.Count > 0)
        {
            dg.DataSource = dsfee.Tables[0];
            dg.DataBind();
        }
        dg.HeaderStyle.Font.Bold = true;
        dg.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();

    }
}