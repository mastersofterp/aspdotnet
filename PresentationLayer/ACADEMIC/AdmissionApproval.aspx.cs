using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ACADEMIC_AdmissionApproval : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    StudentController objSC = new StudentController();

    #region Page Event

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
                    this.CheckPageAuthorization();

                    // Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    // Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                    objCommon.SetLabelData("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]));//Set label -  Added By Nikhil L. on 17/01/2021
                    objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));  // Set Page Header  -  Added By Nikhil L. on 17/01/2021
                    this.PopulateDropDownList();

                    //txtToDate.Text = DateTime.Now.ToShortDateString();
                    //Focus on From Date
                    //txtFromDate.Focus();
                }
            }

            //Blank Div
            divMsg.InnerHtml = string.Empty;
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
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=StudentAdmission_Register.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=StudentAdmission_Register.aspx");
        }
    }

    #endregion

    protected void PopulateDropDownList()
    {
        try
        {
            // FILL DROPDOWN DEGREE
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO > 0", "DEGREENO");
            //ddlBranch.Items.Insert(0, "Please Select");

            // FILL DROPDOWN COLLEGE
            if (Session["usertype"].ToString() != "1")
            {
                //objCommon.FillDropDownList(ddlClg, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
                objCommon.FillDropDownList(ddlClg, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "COLLEGE_ID"); //Added by Nikhil L. on 17/01/2022 to get colleges by Organization ID.
                //objCommon.FillDropDownList(ddlClg, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "(COLLEGE_NAME + ' ' + '-' + ' ' + LOCATION) collegeName", "COLLEGE_ID > 0 AND COLLEGE_ID IN (" + Session["college_nos"] + ")", "COLLEGE_ID");
            }
            else
            {
                //objCommon.FillDropDownList(ddlClg, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME", "COLLEGE_ID > 0", "COLLEGE_ID");
                objCommon.FillDropDownList(ddlClg, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME", "COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "COLLEGE_ID"); //Added by Nikhil L. on 17/01/2022 to get colleges by Organization ID.
            }


            // FILL DROPDOWN ADMISSION BATCH
            objCommon.FillDropDownList(ddlAdmbatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>0", "BATCHNO DESC");
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        BindStudentList();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //Refresh Page url
        Response.Redirect(Request.Url.ToString());
    }
    protected void ddlClg_SelectedIndexChanged(object sender, EventArgs e)
    {

        divstudentdetail.Visible = false;
        //ddlDepartment.SelectedIndex = 0;
        ddlDepartment.Items.Clear();
        ddlDegree.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        ddlStatus.SelectedIndex = 0;
        if (ddlClg.SelectedIndex > 0)
        {
            if (Session["usertype"].ToString() != "1")
            {
                //string dec = objCommon.LookUp("USER_ACC", "UA_DEC", "UA_NO=" + Session["userno"].ToString());


                //objCommon.FillDropDownList(ddlDepartment, "ACD_DEPARTMENT D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEPTNO = B.DEPTNO)", "DISTINCT (B.DEPTNO)", "D.DEPTNAME", "B.COLLEGE_ID=" + ddlClg.SelectedValue + " AND B.DEPTNO =" + Session["userdeptno"].ToString(), "B.DEPTNO");
                objCommon.FillListBox(ddlDepartment, "ACD_DEPARTMENT D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEPTNO = B.DEPTNO) ", "DISTINCT (B.DEPTNO)", "D.DEPTNAME ", "B.COLLEGE_ID=" + ddlClg.SelectedValue + " AND B.DEPTNO IN (" + Session["userdeptno"].ToString() + ")", "B.DEPTNO ");

            }
            else
            {
                //objCommon.FillDropDownList(ddlDepartment, "ACD_DEPARTMENT D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEPTNO = B.DEPTNO)", "DISTINCT (B.DEPTNO)", "D.DEPTNAME", "B.COLLEGE_ID=" + ddlClg.SelectedValue, "B.DEPTNO");
                objCommon.FillListBox(ddlDepartment, "ACD_DEPARTMENT D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEPTNO = B.DEPTNO) ", "DISTINCT (B.DEPTNO)", "D.DEPTNAME ", "B.COLLEGE_ID=" + ddlClg.SelectedValue, "B.DEPTNO");

            }
        }
        else
        {
            ddlDepartment.Focus();
            ddlClg.SelectedIndex = 0;
        }



    }
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        divstudentdetail.Visible = false;
        ddlBranch.SelectedIndex = 0;
        ddlStatus.SelectedIndex = 0;
        string a = ddlDegree.SelectedValue;
        if (ddlDegree.SelectedIndex > 0)
        {
            //objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue + " AND B.DEPTNO =" + ddlDepartment.SelectedValue, "A.LONGNAME");
            //if (Session["usertype"].ToString() != "1")
            //    objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue + " AND B.DEPTNO =" + Session["userdeptno"].ToString(), "A.LONGNAME");
            //else
            objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue + "AND B.COLLEGE_ID=" + ddlClg.SelectedValue + " AND B.DEPTNO IN(" + ViewState["dept"].ToString() + ")", "A.LONGNAME");
            ddlBranch.Focus();

        }
        else
        {
            //ddlBranch.Items.Clear();
            ddlDegree.SelectedIndex = 0;
        }
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        divstudentdetail.Visible = false;
        ddlStatus.SelectedIndex = 0;

    }
    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        divstudentdetail.Visible = false;
        ddlBranch.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        ddlStatus.SelectedIndex = 0;
        string dept = string.Empty;
        if (ddlClg.SelectedIndex > 0)
        {
            //if (Session["usertype"].ToString() != "1")
            //{
            foreach (ListItem items in ddlDepartment.Items)
            {
                if (items.Selected == true)
                {
                    dept += items.Value + ',';
                }
            }
            if (!dept.ToString().Equals(string.Empty) || !dept.ToString().Equals(""))
                dept = dept.Remove(dept.Length - 1);
            ViewState["dept"] = dept;
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.DEGREENO>0 AND B.COLLEGE_ID=" + ddlClg.SelectedValue + " AND B.DEPTNO IN(" + dept + ")", "D.DEGREENO");

            //}
            //else
            //{
            //    objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.DEGREENO > 0 AND B.COLLEGE_ID=" + ddlClg.SelectedValue, "D.DEGREENO");
            //}
        }
        else
        {
            ddlDegree.Focus();
            ddlDepartment.SelectedIndex = 0;
        }
    }
    private void BindStudentList()
    {
        try
        {

            string deptno = string.Empty;
            //string deptNos = string.Empty;
            string dept = objCommon.LookUp("ACD_DEPARTMENT D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEPTNO = B.DEPTNO)", "DISTINCT (B.DEPTNO)", "B.COLLEGE_ID=" + ddlClg.SelectedValue + "AND  B.DEPTNO IN(" + Session["userdeptno"].ToString() + ")");
            if (ddlDepartment.SelectedValue == null || ddlDepartment.SelectedValue == "")
            {
                if (dept.ToString().Equals(string.Empty) || dept.ToString().Equals(""))
                {
                    dept = "0";
                }
                deptno = dept;
            }
            else
            {

                foreach (ListItem items in ddlDepartment.Items)
                {
                    if (items.Selected == true)
                    {
                        deptno += items.Value + ',';
                    }
                }
                deptno = deptno.Remove(deptno.Length - 1);
            }

            //DataSet ds = objSC.GetStudentForAdmApprove(Convert.ToInt32(ddlAdmbatch.SelectedValue), Convert.ToInt32(ddlClg.SelectedValue), deptno, Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlStatus.SelectedValue));
            DataSet ds = objSC.GetStudentForAdmApprove_NewMultiSelect(Convert.ToInt32(ddlAdmbatch.SelectedValue), Convert.ToInt32(ddlClg.SelectedValue), deptno, Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlStatus.SelectedValue));

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {

                lvStudentDetail.DataSource = ds;
                lvStudentDetail.DataBind();
                divstudentdetail.Visible = true;
                objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvStudentDetail);//Set label 
            }
            else
            {
                lvStudentDetail.DataSource = null;
                lvStudentDetail.DataBind();
                divstudentdetail.Visible = false;
                objCommon.DisplayMessage(updStudent, "No Record Found.", this);

            }

        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void lvStudentDetail_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        try
        {

            if ((e.Item.ItemType == ListViewItemType.DataItem))
            {
                ListViewDataItem dataItem = (ListViewDataItem)e.Item;
                DataRow dr = ((DataRowView)dataItem.DataItem).Row;

                if (dr["STATUS"].Equals(1))
                {
                    ((Label)e.Item.FindControl("lblStatus")).Text = "Approved";
                    ((Label)e.Item.FindControl("lblStatus")).ForeColor = System.Drawing.Color.Green;
                    ((Label)e.Item.FindControl("lblStatus")).Font.Bold = true;
                }
                else if (dr["STATUS"].Equals(2))
                {
                    ((Label)e.Item.FindControl("lblStatus")).Text = "Rejected";
                    ((Label)e.Item.FindControl("lblStatus")).ForeColor = System.Drawing.Color.Blue;
                    ((Label)e.Item.FindControl("lblStatus")).Font.Bold = true;
                }
                else
                {
                    ((Label)e.Item.FindControl("lblStatus")).Text = "Pending";
                    ((Label)e.Item.FindControl("lblStatus")).ForeColor = System.Drawing.Color.Red;
                    ((Label)e.Item.FindControl("lblStatus")).Font.Bold = true;
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        ShowdStudentData("Document_Status");
    }
    private void ShowdStudentData(string reportName)
    {
        try
        {
            GridView GVDayWiseAtt = new GridView();
            string admbatch = objCommon.LookUp("ACD_ADMBATCH", "BATCHNAME", "BATCHNO=" + ddlAdmbatch.SelectedValue);

            DataSet ds = new DataSet();
            //int deptno = 0;
            string dept = objCommon.LookUp("ACD_DEPARTMENT D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEPTNO = B.DEPTNO)", "DISTINCT (B.DEPTNO)", "B.COLLEGE_ID=" + ddlClg.SelectedValue + "AND  B.DEPTNO IN (" + Session["userdeptno"].ToString() + ")");
            if (ddlDepartment.SelectedIndex == 0)
            {
                if (dept.ToString().Equals(string.Empty) || dept.ToString().Equals(""))
                {
                    dept = "0";
                }
            }
            else
            {
                foreach (ListItem items in ddlDepartment.Items)
                {
                    if (items.Selected == true)
                    {
                        dept += items.Value + ',';
                    }
                }
                if (!dept.ToString().Equals(string.Empty))
                    dept = dept.Remove(dept.Length - 1);
            }

            //ds = objSC.GetNewAdmissionStudentDocUploadStatus(Convert.ToInt32(ddlAdmbatch.SelectedValue), Convert.ToInt32(ddlClg.SelectedValue),Convert.ToInt32(0), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue));
            ds = objSC.GetNewAdmissionStudentDocUploadStatus_NewMultiSelect(Convert.ToInt32(ddlAdmbatch.SelectedValue), Convert.ToInt32(ddlClg.SelectedValue), dept, Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue));


            if (ds.Tables[0].Rows.Count > 0)
            {

                GVDayWiseAtt.DataSource = ds;
                GVDayWiseAtt.DataBind();

                string attachment = "attachment; filename=" + admbatch.Replace(" ", "_") + "_" + reportName + ".xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.MS-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                GVDayWiseAtt.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
            else
            {
                objCommon.DisplayMessage(this.updStudent, "No Data Found for current selection.", this.Page);
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void ddlAdmbatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlClg.SelectedIndex = 0;
            ddlDepartment.Items.Clear();
            ddlDegree.SelectedIndex = 0;
            ddlBranch.SelectedIndex = 0;
            ddlStatus.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}