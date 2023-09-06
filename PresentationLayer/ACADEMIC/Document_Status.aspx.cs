using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Net;
using System.IO;
using System.Collections;
using System.Text;
using System.Web.UI.HtmlControls;
using Newtonsoft.Json;
using System.Text;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography;
using System.Collections.Specialized;
using Microsoft.Win32;
using Newtonsoft.Json.Linq;

public partial class ACADEMIC_Default : System.Web.UI.Page
{
    Common objCommon = new Common();
    StudentController objstud = new StudentController();
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
                    //   this.CheckPageAuthorization();

                    // Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    // Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                        this.objCommon.FillDropDownList(ddlSearch, "ACD_SEARCH_CRITERIA", "ID", "CRITERIANAME", "ID > 0 AND IS_FEE_RELATED = 0", "SRNO");
                        ddlSearch.SelectedIndex = 1;
                        ddlSearch_SelectedIndexChanged(sender, e);
                    }
                }
            }
            else
            {
                // Clear message div
                divMsg.InnerHtml = string.Empty;
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            // Check user's authrity for Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Document_Status.aspx");
            }
        }
        else
        {
            // Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Document_Status.aspx");
        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {

        //if (txtEnrollment.Text.Trim() != string.Empty)
        //{
        //    string IDNO = objCommon.LookUp("ACD_STUDENT", "IDNO", "(ENROLLNO='" + txtEnrollment.Text.Trim() + "' OR APPLICATIONID='" + txtEnrollment.Text.Trim() + "')");

        //    if (IDNO == "0" || IDNO == string.Empty || IDNO == "")
        //    {
        //        objCommon.DisplayMessage(this.Page, "No Student not found!!", this.Page);
        //        return;
        //    }
        //    else
        //    {
        //        ViewState["idno"] = IDNO;
        //        bindStudentInfo();
        //        BindListView();
        //    }
        //}
        //else
        //{
        //    objCommon.DisplayMessage(this.Page, "No Student not found!!", this.Page);
        //    return;
        //}
        ////bindStudentInfo();
        //BindListView();
        btnshownew();
    }

    protected void btnshownew()
    {
       //string Enrollment =Session["stuinfoidno"].ToString();
       
       // if (txtEnrollment.Text.Trim() != string.Empty)
       // {
       //     string IDNO = objCommon.LookUp("ACD_STUDENT", "IDNO", "(ENROLLNO='" + txtEnrollment.Text.Trim() + "' OR APPLICATIONID='" + txtEnrollment.Text.Trim() + "')");

       //     if (IDNO == "0" || IDNO == string.Empty || IDNO == "")
       //     {
       //         objCommon.DisplayMessage(this.Page, "No Student not found!!", this.Page);
       //         return;
       //     }
       //     else
       //     {
                string IDNO = Session["stuinfoidno"].ToString();

                ViewState["idno"] = IDNO;
                bindStudentInfo();
                BindListView();
            //}
        //}
        //else
        //{
        //    objCommon.DisplayMessage(this.Page, "No Student not found!!", this.Page);
        //    return;
        //}
        //bindStudentInfo();
       BindListView();
    
    }

    protected void BindListView()
    {
        
        //string Enrollment = txtEnrollment.Text.Trim();
        //string idno = objCommon.LookUp("ACD_STUDENT", "IDNO", "(ENROLLNO='" + Enrollment + "' OR APPLICATIONID='" + txtEnrollment.Text.Trim() + "')");
        DataSet ds = null;
        string idno = Session["stuinfoidno"].ToString();
        ds = objstud.GetDocument(Convert.ToString(idno));
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
           
            lvBinddata.DataSource = ds;
            lvBinddata.DataBind();
            lvBinddata.Visible = true;

        }
        else
        {
            lvBinddata.Visible = false;
            lvBinddata.DataSource = null;
            lvBinddata.DataBind();


        }
        //foreach (ListViewDataItem lvitem in lvBinddata.Items)
        //{
        //    //TextBox remark = lvitem.FindControl("txtDoc") as TextBox;
        //   // remark.Enabled = true;
        //   // remark.Enabled = false;
        //}

    }
    

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int documentstatus=0;
        int status = 0;
        int count = 0;
        if (rdoSubmit.Checked == true)
        {
            foreach (ListViewDataItem lvitem in lvBinddata.Items)
            {
                CheckBox chkBox = lvitem.FindControl("chkDocsingle") as CheckBox;
                string hdnf = (lvitem.FindControl("HiddenField1")as HiddenField).Value;
                string Remark = (lvitem.FindControl("txtDoc") as TextBox).Text;
                string docname = (lvitem.FindControl("lblDocument") as Label).Text;

                string docno = hdnf;
                string date = (lvitem.FindControl("txtDate") as TextBox).Text;
               
                if(rdoSubmit.Checked)
                {
                       status=1;
                       documentstatus = 1;
                       divstuddetails.Visible = true;
                       lvBinddata.Visible = true;
                }

                if (chkBox.Checked == true)
                {
                int retval = objstud.UpdateAcd_Document(Convert.ToString(Remark), Convert.ToString(docno), Convert.ToInt32(status), Convert.ToDateTime(date), Convert.ToInt32(Session["stuinfoidno"].ToString()),documentstatus);
                    if (retval == 0)
                    {
                        objCommon.DisplayMessage(this, "Data Not Saved!!", this.Page);                     
                    }
                    else
                    {
                        //objCommon.DisplayMessage(this, "Data save Successfully...!!", this.Page);
                        count++;
                    }

                }
                //else if (chkBox.Checked == false)
                //{

                //    objCommon.DisplayMessage(this, "Please Select Atleast One Document...!!", this.Page);
                //    return;
                //}
            }
            if (count > 0)
            {
                objCommon.DisplayMessage(this, "Data save Successfully...!!", this.Page);
                BindListView();
                divstuddetails.Visible = true;
                lvBinddata.Visible = true;
                pnlBind.Visible = true;
                divDetails.Visible = true;      
                //return;
               
            }
            //else if (count < 0)
            //{
            //    objCommon.DisplayMessage(this, "Please Select Atleast One Document...!!", this.Page);
            //    return;
            //}
            BindListView();
            //divstuddetails.Visible = true;
            //lvBinddata.Visible = true;                
           
        }
        else
        {
            foreach (ListViewDataItem lvitem in lvBinddata.Items)
            {
                CheckBox chkBox = lvitem.FindControl("chkDocsingle") as CheckBox;
                string hdnf = (lvitem.FindControl("HiddenField1") as HiddenField).Value;
                string Remark = (lvitem.FindControl("txtDoc") as TextBox).Text;
                string docname = (lvitem.FindControl("lblDocument") as Label).Text;
                string docno = hdnf;
 
                string date = (lvitem.FindControl("txtDate") as TextBox).Text;

                if (rdoReturn.Checked)
                {
                    status = 0;
                    documentstatus = 2;
                    divstuddetails.Visible = true;
                    lvBinddata.Visible = true;
                }
               

                if (chkBox.Checked == true)
                {

                int retval = objstud.UpdateAcd_Document(Convert.ToString(Remark), Convert.ToString(docno), Convert.ToInt32(status), Convert.ToDateTime(date), Convert.ToInt32(Session["stuinfoidno"].ToString()), documentstatus);

                    // if (retval == 0)
                    //{
                    //    objCommon.DisplayMessage(this.Page, "Data Not Saved!!", this.Page);
                    //    return;
                    //}
                    //else
                    //{
                objCommon.DisplayMessage(this.Page, "Data save Successfully...!!", this.Page);


                
                //lvBinddata.Visible = true;
               // pnlBind.Visible = true;
               // divDetails.Visible = true;
                //divstuddetails.Visible = true;
               // lvBinddata.Visible = true;
                    //    return;
                    //}

                }

            }





            BindListView();
            divDetails.Visible = true;
            pnlBind.Visible = true;
            upnllvBinddata.Visible = true;
            lvBinddata.Visible = true;
            pnlSearch.Visible = true;
            pnlSearch.Visible = true;
            pnlrdb.Visible = true;
            pnlbtn.Visible = true;
            // BindListView();
            divstuddetails.Visible = true;
            //BindListView();
            //divstuddetails.Visible = true;
            //lvBinddata.Visible = true;
            //pnlBind.Visible = true;
            //divDetails.Visible = true;
            //divstuddetails.Visible = true;
            //lvBinddata.Visible = true;
          
        }

        //divstuddetails.Visible = false;
        //lvBinddata.Visible = false;
        //divDetails.Visible = false;
        //pnlrdb.Visible = false;
        //divstuddetails.Visible = false;
        //pnlbtn.Visible = false;
        //divpanel.Visible = false;
        //divstuddetails.Visible = true;
        //lvBinddata.Visible = true;
       // ddlSearch.SelectedIndex = 0;
       // upnllvBinddata.Visible = false;
    }

    private void bindStudentInfo()
    {
        try
        {
            //string searchText = txtSearch.Text.Trim();
                //txtEnrollment.Text.Trim();
            //string idno1 = Convert.ToString((txtEnrollment.Text)).ToString();
            //string studid = objCommon.LookUp("ACD_STUDENT", "IDNO", "(ENROLLNO='" + idno1 + "' OR APPLICATIONID='" + txtEnrollment.Text.Trim() + "')");  
            
            string studid = Session["stuinfoidno"].ToString();
            ShowStudents(studid);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void ShowStudents( string searchText)
    {
        //bind the student search in text box......
        DataSet ds = objstud.SearchStudentsByEnroll(searchText);

        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                //Show Student Details..
                lblDegree.Text = ds.Tables[0].Rows[0]["CODE"].ToString();
                lblName.Text = ds.Tables[0].Rows[0]["NAME"].ToString();  
                lblBranch.Text = ds.Tables[0].Rows[0]["LONGNAME"].ToString();
                
                lblSemester.Text = ds.Tables[0].Rows[0]["SEM_NAME"].ToString();
                divstuddetails.Visible = true;
                
               
               
               
            }
        }
        else
        {
            lvBinddata.DataSource = null;
            //lvDocumentList.DataSource = null;
            lvBinddata.DataBind();
            divstuddetails.Visible = false;
            lvBinddata.Visible = false;
            objCommon.DisplayMessage(this.Page, "No Student Found ", this.Page);
        }
    }
    //protected void chkDocsingle_CheckedChanged(object sender, EventArgs e)
    //{
    //    //foreach (ListViewDataItem lvitem in lvBinddata.Items)
    //    //{
    //    //    CheckBox chbk = lvitem.FindControl("chkDocsingle") as CheckBox;
    //    //    TextBox remark = lvitem.FindControl("txtDoc") as TextBox;
    //    //    //remark.Enabled = true;

    //    //    if (chbk.Checked == true)
    //    //    {
    //    //        remark.Enabled = true;
    //    //    }
    //    //    else
    //    //    {
    //    //        remark.Enabled = false;
    //    //    }
    //    //}
    //}
    //protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    //{
    //    //foreach (ListViewDataItem lvitem in lvBinddata.Items)
    //    //{
    //    //    CheckBox chbk = lvitem.FindControl("chkDocsingle") as CheckBox;
    //    //    CheckBox chkbox = lvitem.FindControl("CheckBox1") as CheckBox;
    //    //    TextBox remark = lvitem.FindControl("txtDoc") as TextBox;
    //    //    //remark.Enabled = true;

    //    //    if (chbk.Checked == true)
    //    //    {
    //    //        remark.Enabled = true;
    //    //    }
    //    //    else
    //    //    {
    //    //        remark.Enabled = false;
    //    //        chkbox.Checked = false;
    //    //    }
    //    //}
    //}
    protected void lvBinddata_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        //foreach (ListViewDataItem lvitem in lvBinddata.Items)
        //{
        //    TextBox date = lvitem.FindControl("txtDate") as TextBox;
        //    DateTime dt = DateTime.Now;
        //    date.Text = dt.ToString();
        //   //Convert.ToDateTime(date.Text);



        ListViewDataItem dataItem = (ListViewDataItem)e.Item;
        TextBox date = (TextBox)e.Item.FindControl("txtDate");
        DateTime dt = DateTime.Now.Date;
        date.TextMode = TextBoxMode.SingleLine;
        date.Text = string.Format("{0:dd/MM/yyyy}", dt);

    }


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //txtEnrollment.Text = string.Empty;
        //txtEnrollment.Focus();
        lvBinddata.DataSource = null;
        lvBinddata.DataBind();
        divstuddetails.Visible = false;
        lvBinddata.Visible = false;
        divDetails.Visible = false;
        pnlrdb.Visible = false;
        lvBinddata.Visible = false;
        divstuddetails.Visible = false;
        pnlbtn.Visible = false;
        ddlSearch.SelectedIndex = 0;
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReport("Document_Status", "rptStudDocumentStatus_New.rpt");

            divstuddetails.Visible = false;
            lvBinddata.Visible = false;
            divDetails.Visible = false;
            pnlrdb.Visible = false;
            divstuddetails.Visible = false;
            pnlbtn.Visible = false;
            ddlSearch.SelectedIndex = 0;
            upnllvBinddata.Visible = false;

            divstuddetails.Visible = true;
            pnlrdb.Visible = true;
            pnlbtn.Visible = true;
            divDetails.Visible = true;
            lvBinddata.Visible = true;
            pnlBind.Visible = true;
            upnllvBinddata.Visible = true;
            pnlsearchnew.Visible = true;
            //rdoSubmit.Checked = false;
            //rdoReturn.Checked = false;
        }
        catch (Exception ex)
        {
            throw;
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
            url += "&param=@P_COLLEGE_CODE=" + Session["ColCode"].ToString() + ",Username=" + Session["username"].ToString() + ",@P_IDNO=" + ViewState["idno"];

            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void ddlSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Panel3.Visible = false;
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
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {

        //Panel1.Visible = true;
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
        //div_Studentdetail.Visible = false;
        //divMSG.Visible = false;
        //btnPayment.Visible = false;
        //btnReciept.Visible = false;
        //divPreviousReceipts.Visible = false;
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
        pnlSearch.Visible = false;
        divDetails.Visible = false;
        pnlrdb.Visible = false;
        divstuddetails.Visible = false;
        //pnlbtn.Visible = false;
       // ddlSearch.SelectedIndex = 0;

        lvBinddata.Visible = false;
        divDetails.Visible = false;
       // lvStudent.Visible = false;
        divstuddetails.Visible = false;
        pnlbtn.Visible = false;
        pnlBind.Visible = false;
        upnllvBinddata.Visible = false;
     
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        //Response.Redirect(Request.Url.ToString());
        lvBinddata.Visible = false;
        lvStudent.Visible = false;
        divstuddetails.Visible = false;
        pnlrdb.Visible = false;
        divDetails.Visible = false;
         pnlSearch.Visible = false;
        divDetails.Visible = false;
        pnlrdb.Visible = false;
        lvBinddata.Visible = false;
        divstuddetails.Visible = false;
        pnlbtn.Visible = false;
        ddlSearch.SelectedIndex = 0;
    }
    private void bindlist(string category, string searchtext)
    {
        StudentController objSC = new StudentController();
        DataSet ds = objSC.RetrieveStudentDetailsNew(searchtext, category);

        if (ds.Tables[0].Rows.Count > 0)
        {
            Panel3.Visible = true;
            //divReceiptType.Visible = false;
            //divStudSemester.Visible = false;
            lvStudent.Visible = true;
            lvStudent.DataSource = ds;
            lvStudent.DataBind();
            objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvStudent);//Set label - 
            lblNoRecords.Text = "Total Records : " + ds.Tables[0].Rows.Count.ToString();
        }
        else
        {
            lblNoRecords.Text = "Total Records : 0";
            lvStudent.Visible = false;
            lvStudent.DataSource = null;
            lvStudent.DataBind();
            objCommon.DisplayMessage(this.Page, "Selected Student Admission is Not Confirmed so that not able to Submit Document Status.", this.Page);
        }
    }
    protected void lnkId_Click(object sender, EventArgs e)
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

        //if (lnk.CommandArgument == null)
        //{
        //    string number = Session["StudId"].ToString();
        //    Session["stuinfoidno"] = Convert.ToInt32 (number);
        //}
        //else
        //{
        Session["stuinfoidno"] = Convert.ToInt32(lnk.CommandArgument);
        //}
        ViewState["idno"] = Session["stuinfoidno"].ToString();
        string idno = Session["stuinfoidno"].ToString();

        // DisplayStudentInfo(Convert.ToInt32(Session["stuinfoidno"]));

        //Server.Transfer("PersonalDetails.aspx", false);
        //DisplayInformation(Convert.ToInt32(Session["stuinfoidno"]));


        btnshownew();

        // bindStudentInfo();
        //string IDNO = Session["stuinfoidno"].ToString();

        //ViewState["idno"] = IDNO;
        //ShowStudents(txtSearch.Text.Trim());
        //bindStudentInfo();
        //updEdit.Visible = false;

        //lvStudent.DataSource = null;
        //lblNoRecords.Visible = false;
        //divstuddetails.Visible = true;
        pnlbtn.Visible = true;
        pnlsearchnew.Visible = false;
        lvBinddata.Visible = true;
        pnlSearch.Visible = true;
        divDetails.Visible = true;
        pnlrdb.Visible = true;
        lvBinddata.Visible = true;
        lvStudent.Visible = false;
        lvStudent.DataSource = null;
        lblNoRecords.Visible = false;
        Panel3.Visible = false;
        pnlsearchnew.Visible = false;
        pnlsearchnew.Visible = true;
        pnlBind.Visible = true;
        upnllvBinddata.Visible = true;
    }
}