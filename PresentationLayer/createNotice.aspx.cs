//=================================================================================
// PROJECT NAME  : UAIMS                                                           
// MODULE NAME   :                                                                 
// PAGE NAME     : TO CREATE NOTICE                                                
// CREATION DATE : 13-April-2009                                                   
// CREATED BY    : NIRAJ D. PHALKE & ASHWINI BARBATE                               
// MODIFIED BY   : 
// MODIFIED DESC : 
//=================================================================================

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
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

public partial class notice : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();

    string blob_ConStr = System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"].ToString();
    string blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerName"].ToString();

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
                else
                {
                    //lblHelp.Text = "No Help Added";   
                }
                //Bind the ListView with Notice
                BindListViewNotice();

                pnlAdd.Visible = false;
                pnlList.Visible = true;
                objCommon.FillDropDownList(ddlUserType, "USER_RIGHTS", "USERTYPEID", "USERDESC", "USERTYPEID IN(2,3,8)", "USERTYPEID");
                objCommon.FillDropDownList(ddlDept, "ACD_DEPARTMENT", "DEPTNO", "DEPTNAME", "DEPTNO > 0", "DEPTNO");
                objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID > 0", "COLLEGE_ID");
                ChekListBind();
                BindDepartments();
                ViewState["action"] = "add";
               
            }
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));  // Set Page Header  -  Added By Rishabh on 28/12/2021
            
        }
    }

    private void BindListViewNotice()
    {
        try
        {
            lvNotice.DataSource = null;
            lvNotice.DataBind();
            string a = Page.Request.QueryString["pageno"].ToString();
            DataSet dsNotice = null;
            NewsController objNc = new NewsController();
            if (a == "1096")
            {
                dsNotice = objNc.GetAllNews("PKG_NEWS_SP_ALL_NOTICE_TP");

            }
            else
            {
                dsNotice = objNc.GetAllNews("PKG_NEWS_SP_ALL_NOTICE");
            }

            if (dsNotice.Tables[0].Rows.Count > 0)
            {
                lvNotice.DataSource = dsNotice;
                lvNotice.DataBind();
                Panel1.Visible = true;
                pnlList.Visible = true;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "notice.BindListViewNews-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

        clear();
    }

    private void clear()
    {
       
        divStudent.Visible = false;
        btnParentList.Visible = false;
        btnShowFaculty.Visible = false;
        lvFacultyList.Visible = false;
        divdepartment.Visible = false;
        txtTitle.Enabled = true;
        lblfile.Text = string.Empty;
        ViewState["newsid"] = null;
        lstbxdept.Items.Clear();

    }

    private void ChekListBind()
    {
        DataSet ds = objCommon.FillDropDown("USER_RIGHTS", "USERTYPEID", "USERDESC", "USERTYPEID IN(1,2,3,5,8,14)", "USERTYPEID");
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            DataTable dt = new DataTable();
            dt = ds.Tables[0];
            //for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            //{
            chklUserType.DataSource = dt;
            chklUserType.DataTextField = "USERDESC"; //ds.Tables[0].Rows[i]["USERDESC"].ToString();
            chklUserType.DataValueField = "USERTYPEID";//ds.Tables[0].Rows[i]["USERTYPEID"].ToString();
            chklUserType.DataBind();
            //}
        }
    }
    private void BindDepartments()
    {
        DataSet dsDept = objCommon.FillDropDown("ACD_DEPARTMENT", "DEPTNO", "DEPTNAME", "DEPTNO > 0", "DEPTNO");
        if (dsDept.Tables.Count > 0 && dsDept.Tables[0].Rows.Count > 0)
        {
            DataTable dtDept = new DataTable();
            dtDept = dsDept.Tables[0];
            chklDepartment.DataSource = dtDept;
            chklDepartment.DataTextField = "DEPTNAME";
            chklDepartment.DataValueField = "DEPTNO";
            chklDepartment.DataBind();
            hdfDepartment.Value = dsDept.Tables[0].Rows.Count.ToString();
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        saveNotice();
       
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //Clear all textboxes
        //txtTitle.Text = "";
        //txtLinkName.Text  = "";
        //txtExpiryDate.Text = "";
        //ftbDesc.Text = "";
        //pnlAdd.Visible = false;
        //pnlList.Visible = true;
        Response.Redirect(Request.Url.ToString());
    }

    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnDel = sender as ImageButton;
            int newsid = int.Parse(btnDel.CommandArgument);

            NewsController objNC = new NewsController();

            CustomStatus cs = (CustomStatus)objNC.Delete(newsid);
            if (cs.Equals(CustomStatus.RecordDeleted))
            {
                objCommon.DisplayMessage("Record Deleted Successfully", this.Page);
                pnlAdd.Visible = false;
                pnlList.Visible = true;
                BindListViewNotice();
            }
            else
            {
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "notice.btnDelete_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int newsid = int.Parse(btnEdit.CommandArgument);
            ShowDetail(newsid);
            ViewState["action"] = "edit";
            pnlAdd.Visible = true;
            pnlList.Visible = false;
            txtTitle.Enabled = false;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "notice.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetail(int newsid)
    {
        NewsController objNC = new NewsController();
        SqlDataReader dr = objNC.GetSingleNews(newsid);
        string user_type = string.Empty;
        string depatments = "0";
        if (dr != null)
        {
            if (dr.Read())
            {
                ViewState["newsid"] = int.Parse(dr["NewsID"].ToString());
                int NewsID = Convert.ToInt32(ViewState["newsid"].ToString());
                txtTitle.Text = dr["NewsTitle"] == null ? "" : dr["NewsTitle"].ToString();
                txtLinkName.Text = dr["Link"] == null ? "" : dr["Link"].ToString();
                txtExpiryDate.Text = dr["EXPIRY_DATE"] == null ? "" : Convert.ToDateTime(dr["EXPIRY_DATE"].ToString()).ToString("dd/MM/yyyy");
                lblfile.Text = dr["Filename"] == null ? "" : dr["Filename"].ToString();
                ftbDesc.Text = dr["NewsDesc"] == null ? "" : dr["NewsDesc"].ToString();
                user_type = dr["UA_TYPE"] == null ? "" : dr["UA_TYPE"].ToString();
                depatments = dr["DEPARTMENT"] != null ? dr["DEPARTMENT"].ToString() : "";
                // ddlUserType.SelectedValue = dr["UA_TYPE"] == null ? "" : dr["UA_TYPE"].ToString();
                string[] User_types = dr["UA_TYPE"].ToString().Split(',');
                for (int i = 0; i < User_types.Length; i++)
                {
                    for (int j = 0; j < chklUserType.Items.Count; j++)
                    {
                        if (chklUserType.Items[j].Value == User_types[i])
                            chklUserType.Items[j].Selected = true;
                    }
                }

                ddlCollege.SelectedValue = dr["COLLEGE_ID"] == null ? "" : dr["COLLEGE_ID"].ToString();
                int COLLEGE_ID = Convert.ToInt32(ddlCollege.SelectedValue);
                objCommon.FillDropDownList(ddlDegree, "ACD_COLLEGE_DEGREE A INNER JOIN ACD_DEGREE B ON(A.DEGREENO=B.DEGREENO) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD ON (CD.DEGREENO=B.DEGREENO)", "DISTINCT(A.DEGREENO)", "B.DEGREENAME", "A.COLLEGE_ID=" + Convert.ToInt32(ddlCollege.SelectedValue), "A.DEGREENO");
                ddlDegree.SelectedValue = dr["DEGREE"] == null ? "" : dr["DEGREE"].ToString();
                int DEGREENO = Convert.ToInt32(ddlDegree.SelectedValue);
                objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH B INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CB ON B.BRANCHNO=CB.BRANCHNO", "DISTINCT B.BRANCHNO", " B.LONGNAME", " CB.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND CB.COLLEGE_ID=" + Convert.ToInt32(ddlCollege.SelectedValue), "LONGNAME");
                ddlBranch.SelectedValue = dr["BRANCH"] == null ? "" : dr["BRANCH"].ToString();
                int BRANCHNO = Convert.ToInt32(ddlBranch.SelectedValue);
                objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER A INNER JOIN ACD_STUDENT B ON (A.SEMESTERNO=B.SEMESTERNO)", "DISTINCT A.SEMESTERNO", "A.SEMESTERNAME", "A.SEMESTERNO>0 AND DEGREENO=" + ddlDegree.SelectedValue, "A.SEMESTERNO");
                ddlSemester.SelectedValue = dr["SEMESTER"] == null ? "" : dr["SEMESTER"].ToString();
                int SEMESTERNO = Convert.ToInt32(ddlSemester.SelectedValue);
                string all_users = dr["ALL_USERS"] == null ? "" : dr["ALL_USERS"].ToString();
                string select = dr["SELECT_ALL"] == null ? "" : dr["SELECT_ALL"].ToString();
                string studcount = objCommon.LookUp("SELECTED_STUDENT_NEWS", "COUNT(IDNO)", "NEWSID=" + ViewState["newsid"].ToString());
                string faccount = objCommon.LookUp("ACD_SELECTED_FACULTY_NEWS", "COUNT(UA_NO)", "NEWSID=" + ViewState["newsid"].ToString());
                if (Convert.ToInt32(studcount) > 0)
                {
                    if (select.Equals("1"))
                    {
                        divStudent.Visible = true;
                        chkAll.Visible = true;
                        chkAll.Checked = true;
                        ddlCollege.Enabled = false;
                        ddlDegree.Enabled = false;
                        ddlBranch.Enabled = false;
                        ddlSemester.Enabled = false;
                        rfvCollege.Visible = !chkAll.Checked;
                        rfvDegree.Visible = !chkAll.Checked;
                        rfvBranch.Visible = !chkAll.Checked;
                        rfvSemester.Visible = !chkAll.Checked;

                    }
                    else
                    {


                        DataSet ds = objNC.GetNews(COLLEGE_ID, DEGREENO, BRANCHNO, SEMESTERNO, NewsID);

                        //DataSet ds = objCommon.FillDropDown("ACD_STUDENT S LEFT JOIN SELECTED_STUDENT_NEWS N ON (S.IDNO=N.IDNO) LEFT JOIN NEWS NE ON(NE.NEWSID=N.NEWSID)", "DISTINCT S.IDNO", "N.IDNO NEWS_IDNO,NE.NewsID,REGNO,STUDNAME,S.COLLEGE_ID,S.DEGREENO,S.BRANCHNO,S.SEMESTERNO,ISNULL(N.STATUS,0)STATUS,ISNULL(N.SPECIAL_STUDENT,0) AS SPECIAL_STUD", "S.COLLEGE_ID =" + Convert.ToInt32(ddlCollege.SelectedValue) + " AND S.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND S.BRANCHNO= " + Convert.ToInt32(ddlBranch.SelectedValue) + "AND S.SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + "AND NE.NEWSID=" + int.Parse(dr["NewsID"].ToString()), "");
                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            lvStudent.DataSource = ds;
                            lvStudent.DataBind();
                            pnlStudent.Visible = true;
                            lvStudent.Visible = true;
                            rblFilter.SelectedValue = "2";
                        }

                    }
                    divStudent.Visible = true;
                    ddlCollege.Enabled = true;
                    ddlDegree.Enabled = true;
                    ddlBranch.Enabled = true;
                    ddlSemester.Enabled = true;

                }


                else if (Convert.ToInt32(faccount) > 0)
                {

                    btnShowFaculty.Visible = true;
                       //DataSet ds = objCommon.FillDropDown("USER_ACC UA  LEFT OUTER JOIN ACD_SELECTED_FACULTY_NEWS AF ON UA.UA_NO=AF.UA_NO AND NEWSID=" + ViewState["newsid"].ToString() + "", "DISTINCT UA.UA_NO,UA_FULLNAME,UA.UA_TYPE", "UA.UA_NAME,ISNULL(STATUS,0)STATUS", "UA.UA_TYPE IN(" + user_type + ")", "UA.UA_NO");
                    divdepartment.Visible = true;
                    int User_Types = Convert.ToInt32(dr["UA_TYPE"] == null ? "" : dr["UA_TYPE"].ToString());
                    DataSet dsdept = objNC.GetDepartment(User_Types);
                    lstbxdept.DataSource = dsdept;
                    lstbxdept.DataValueField = "DEPTNO";
                    lstbxdept.DataTextField = "DEPTNAME";
                    lstbxdept.DataBind();
                    char delimiterChars = ',';
                    string Dept = dr["DEPARTMENT"] == null ? "" : dr["DEPARTMENT"].ToString();
                    string[] stu = Dept.Split(delimiterChars);
                    for (int j = 0; j < stu.Length; j++)
                    {
                        for (int i = 0; i < lstbxdept.Items.Count; i++)
                        {
                            if (stu[j].Trim() == lstbxdept.Items[i].Value.Trim())
                            {
                                lstbxdept.Items[i].Selected = true;
                            }
                        }
                    }
                    string Departments = string.Empty;
                  
                    rblFilter.SelectedValue = (dr["UA_TYPE"] == null ? "" : dr["UA_TYPE"].ToString());
                    foreach (ListItem item in lstbxdept.Items)
                    {
                        if (item.Selected)
                        {
                            if (Departments == string.Empty)
                                Departments = item.Value;
                            else
                                Departments += ',' + item.Value;
                        }
                    }
                    DataSet ds = objNC.GetSelectedFaculty(Departments, User_Types);
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        lvFacultyList.DataSource = ds;
                        lvFacultyList.DataBind();
                        pnlFaculty.Visible = true;
                        lvFacultyList.Visible = true;
                        hdffacultycount.Value = ds.Tables[0].Rows.Count.ToString();
                    }
                }
                    else
                    {
                        ddlUserType.Enabled = false;
                        rfvUsertype.Visible = false;
                        divDept.Visible = false;
                        ddlDept.Enabled = false;
                        lvFacultyList.DataSource = null;
                        lvFacultyList.DataBind();
                        lvFacultyList.Visible = false;
                        pnlFaculty.Visible = false;
                        chkAll.Visible = false;
                        chkAll.Checked = false;
                    }
                    //if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    //    {
                    //        chkAll.Visible = true;
                    //        chkAll.Checked = true;
                    //        divdepartment.Visible = true;
                    //        lvFacultyList.DataSource = ds;
                    //        lvFacultyList.DataBind();
                    //        lvFacultyList.Visible = true;
                    //        pnlFaculty.Visible = true;
                    //        hdffacultycount.Value = ds.Tables[0].Rows.Count.ToString();
                    //        rblFilter.SelectedValue = ds.Tables[0].Rows[0]["UA_TYPE"].ToString();
                    //    }
                    //    else
                    //    {
                    //        ddlUserType.Enabled = false;
                    //        rfvUsertype.Visible = false;
                    //        divDept.Visible = false;
                    //        ddlDept.Enabled = false;
                    //        lvFacultyList.DataSource = null;
                    //        lvFacultyList.DataBind();
                    //        lvFacultyList.Visible = false;
                    //        pnlFaculty.Visible = false;
                    //        rblFilter.SelectedIndex = 0;
                    //        btnShowFaculty.Visible = false;
                    //    }
                    //}
                    //else
                    //{
                    //    ddlUserType.Enabled = false;
                    //    rfvUsertype.Visible = false;
                    //    divDept.Visible = false;
                    //    ddlDept.Enabled = false;
                    //    lvFacultyList.DataSource = null;
                    //    lvFacultyList.DataBind();
                    //    lvFacultyList.Visible = false;
                    //    pnlFaculty.Visible = false;
                    //    chkAll.Visible = false;
                    //    chkAll.Checked = false;

                    }


          

            }
            if (dr != null) dr.Close();
        }

    
    public string GetStatus(object status)
    {
        if (status.ToString().ToLower().Equals("expired"))
            return "<span style='color:Red'>Expired</span>";
        else
            return "<span style='color:Green'>Active</span>";
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        ViewState["action"] = "add";
        txtTitle.Text = "";
        txtLinkName.Text = "";
        txtExpiryDate.Text = "";
        ftbDesc.Text = "";
        pnlAdd.Visible = true;
        pnlList.Visible = false;
        divStudent.Visible = false;
        divdepartment.Visible = false;
        chklDepartment.SelectedIndex = -1;
        chklUserType.SelectedIndex = -1;
        rblFilter.SelectedIndex = -1;
    }

    protected void dpPager_PreRender(object sender, EventArgs e)
    {
        BindListViewNotice();
    }


    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=createnotice.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=createnotice.aspx");
        }
    }

    protected void ddlUserType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlUserType.SelectedIndex > 0)
        {
            if (ddlUserType.SelectedValue == "2")
            {
                divStudent.Visible = true;
                chkAll.Visible = true;
                divDept.Visible = false;
                ddlCollege.Focus();
                chkAll.Checked = false;
                divNote.Visible = true;
                btnShow.Visible = true;
                btnShow.Enabled = true;
                EnableDropDown();
            }
            else
            {
                divStudent.Visible = false;
                //chkAll.Visible = false;
                divDept.Visible = true;
                ddlDept.Focus();
                chkAll.Visible = true;
                chkAll.Checked = false;
                divNote.Visible = false;
                btnShow.Visible = false;
                EnableDropDown();
            }

        }
        else
        {
            divStudent.Visible = false;
            chkAll.Visible = false;
            divDept.Visible = false;
        }
    }
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCollege.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlDegree, "ACD_COLLEGE_DEGREE A INNER JOIN ACD_DEGREE B ON(A.DEGREENO=B.DEGREENO) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD ON (CD.DEGREENO=B.DEGREENO)", "DISTINCT(A.DEGREENO)", "B.DEGREENAME", "A.COLLEGE_ID=" + Convert.ToInt32(ddlCollege.SelectedValue), "A.DEGREENO");
        }
        ddlDegree.Focus();

    }
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDegree.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH B INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CB ON B.BRANCHNO=CB.BRANCHNO", "DISTINCT B.BRANCHNO", " B.LONGNAME", " CB.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND CB.COLLEGE_ID=" + Convert.ToInt32(ddlCollege.SelectedValue), "LONGNAME");
        }
        ddlBranch.Focus();

    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBranch.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER A INNER JOIN ACD_STUDENT B ON (A.SEMESTERNO=B.SEMESTERNO)", "DISTINCT A.SEMESTERNO", "A.SEMESTERNAME", "A.SEMESTERNO>0 AND DEGREENO=" + ddlDegree.SelectedValue, "A.SEMESTERNO");
        }
        ddlSemester.Focus();
    }
    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void chkAll_CheckedChanged(object sender, EventArgs e)
    {
        if (chkAll.Checked)
        {
            if (ddlUserType.SelectedValue == "2")
            {
                ddlCollege.Enabled = false;
                ddlDegree.Enabled = false;
                ddlDegree.Enabled = false;
                ddlSemester.Enabled = false;
                ddlBranch.Enabled = false;
                btnShow.Enabled = false;
                divNote.Visible = true;
                pnlStudent.Visible = false;
                lvStudent.Visible = false;
                lvStudent.DataSource = null;
                lvStudent.DataBind();

            }
            else if (ddlUserType.SelectedValue == "3" || ddlUserType.SelectedValue == "8")
            {
                ddlDept.Enabled = false;
                lvFacultyList.DataSource = null;
                lvFacultyList.DataBind();
                lvFacultyList.Visible = false;
                pnlFaculty.Visible = false;
            }
        }
        else
        {
            ddlCollege.Enabled = true;
            ddlDegree.Enabled = true;
            ddlDegree.Enabled = true;
            ddlSemester.Enabled = true;
            ddlBranch.Enabled = true;
            ddlDept.Enabled = true;
            btnShow.Enabled = true;
        }
        rfvCollege.Visible = !chkAll.Checked;
        rfvDegree.Visible = !chkAll.Checked;
        rfvBranch.Visible = !chkAll.Checked;
        rfvSemester.Visible = !chkAll.Checked;
        rfvDepartment.Visible = !chkAll.Checked;
    }
    protected void EnableDropDown()
    {
        ddlCollege.Enabled = true;
        ddlDegree.Enabled = true;
        ddlDegree.Enabled = true;
        ddlSemester.Enabled = true;
        ddlBranch.Enabled = true;
        ddlDept.Enabled = true;
    }
    //protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
    //{
    //    args.IsValid = chkAll.Checked;
    //}
    protected void Clear()
    {
        txtTitle.Text = string.Empty;
        txtLinkName.Text = string.Empty;
        txtExpiryDate.Text = string.Empty;
        ftbDesc.Text = string.Empty;
        ddlUserType.SelectedIndex = 0;
        ddlDept.SelectedIndex = 0;
        ddlCollege.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;
        lvStudent.DataSource = null;
        lvStudent.DataBind();
        pnlStudent.Visible = false;
        lvStudent.Visible = false;
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {

            //Show News Detail

            DataSet dsStudent = objCommon.FillDropDown("ACD_STUDENT S LEFT JOIN SELECTED_STUDENT_NEWS N ON (S.IDNO=N.IDNO) LEFT JOIN NEWS NE ON(NE.NEWSID=N.NEWSID)", "DISTINCT S.IDNO", "REGNO,STUDNAME,S.COLLEGE_ID,S.DEGREENO,S.BRANCHNO,S.SEMESTERNO,0 AS SPECIAL_STUD,2 AS Flag", "S.COLLEGE_ID =" + Convert.ToInt32(ddlCollege.SelectedValue) + " AND S.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND S.BRANCHNO= " + Convert.ToInt32(ddlBranch.SelectedValue) + " AND S.SEMESTERNO= " + Convert.ToInt32(ddlSemester.SelectedValue), "");

            //DataSet dsStudent = objCommon.FillDropDown("ACD_STUDENT S LEFT JOIN SELECTED_STUDENT_NEWS N ON (S.IDNO=N.IDNO) LEFT JOIN NEWS NE ON(NE.NEWSID=N.NEWSID)", "DISTINCT S.IDNO", "N.IDNO NEWS_IDNO,REGNO,STUDNAME,S.COLLEGE_ID,S.DEGREENO,S.BRANCHNO,S.SEMESTERNO,ISNULL(N.STATUS,0)STATUS", "S.COLLEGE_ID =" + Convert.ToInt32(ddlCollege.SelectedValue) + " AND S.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND S.BRANCHNO= " + Convert.ToInt32(ddlBranch.SelectedValue) + " AND S.SEMESTERNO= " + Convert.ToInt32(ddlSemester.SelectedValue), "");
            if (dsStudent.Tables[0].Rows.Count > 0)
            {
                dsStudent.Tables[0].Rows[0]["REGNO"].ToString();
                lvStudent.DataSource = dsStudent;
                lvStudent.DataBind();
                pnlStudent.Visible = true;
                lvStudent.Visible = true;
                hdnCount.Value = lvStudent.Items.Count.ToString();


            }
            else
            {
                lvStudent.DataSource = null;
                lvStudent.DataBind();
                pnlStudent.Visible = false;
                lvStudent.Visible = false;
                objCommon.DisplayMessage(this.Page, "Record Not Found For Current Selection.", this.Page);
                return;
            }
        }


        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "createNotice.aspx.btnShow_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnShowFaculty_Click(object sender, EventArgs e)
    {
        NewsController objNC = new NewsController();
        string Departments = string.Empty;
        int User_Types = Convert.ToInt32(rblFilter.SelectedValue); ;

        foreach (ListItem item in lstbxdept.Items)
        {
            if (item.Selected)
            {
                if (Departments == string.Empty)
                    Departments = item.Value;
                else
                    Departments += ',' + item.Value;
            }
        }
     
        DataSet ds = objNC.GetFaculty(Departments, User_Types);
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            lvFacultyList.DataSource = ds;
            lvFacultyList.DataBind();
            pnlFaculty.Visible = true;
            lvFacultyList.Visible = true;
            hdffacultycount.Value = ds.Tables[0].Rows.Count.ToString();
        }
        else
        {
            lvFacultyList.DataSource = null;
            lvFacultyList.DataBind();
            pnlFaculty.Visible = false;
            objCommon.DisplayMessage(this.Page, "No Data Found.", this.Page);
        }
    }
    protected void chkAllUsers_CheckedChanged(object sender, EventArgs e)
    {


        if (chkAllUsers.Checked)
        {
            ddlCollege.Enabled = false;
            ddlDegree.Enabled = false;
            ddlDegree.Enabled = false;
            ddlSemester.Enabled = false;
            ddlBranch.Enabled = false;
            btnShow.Enabled = false;
            divNote.Visible = false;
            ddlUserType.Enabled = false;
            pnlStudent.Visible = false;
            lvFacultyList.DataSource = null;
            lvFacultyList.DataBind();
            lvStudent.Visible = false;
            lvStudent.DataSource = null;
            lvStudent.DataBind();

            ddlUserType.SelectedIndex = 0;
            ddlBranch.SelectedIndex = 0;
            ddlDegree.SelectedIndex = 0;
            ddlDept.Visible = false;
            rfvCollege.Visible = !chkAllUsers.Checked;
            rfvDegree.Visible = !chkAllUsers.Checked;
            rfvBranch.Visible = !chkAllUsers.Checked;
            rfvSemester.Visible = !chkAllUsers.Checked;
            rfvDepartment.Visible = !chkAllUsers.Checked;
            rfvUsertype.Visible = !chkAllUsers.Checked;
            chkAll.Enabled = false;
        }
        else
        {
            ddlUserType.Enabled = true;
            rfvUsertype.Visible = true;
            chkAll.Enabled = true;
        }
    }

    private void saveNotice()
    {
        try
        {
            string filetext = string.Empty;
            string AL_ASNo = "";
            string Category = "";
            int userType = 0; int collegeId = 0; int degree = 0;
            int branch = 0; int semester = 0;
            int department = 0; int select = 0;
            int OrgID = 0;
            string a = Page.Request.QueryString["pageno"].ToString();
            DataSet ds = objCommon.FillDropDown("ACCESS_LINK", "*", "", "AL_No=" + Convert.ToInt32(a) + "", "");
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    AL_ASNo = ds.Tables[0].Rows[0]["AL_ASNo"].ToString();
                }
            }

            string alno = (objCommon.LookUp("ACCESS_LINK", "DISTINCT AL_ASNo", "AL_URL like 'TRAININGANDPLACEMENT%'"));
            int asno = Convert.ToInt32(alno);

            if (Convert.ToInt32(AL_ASNo) == asno)
            {
                Category = "NC_TP";
            }
            else
            {
                Category = "N/C";
            }

            string User_Types = string.Empty;
            string Departments = string.Empty;

            //User Type Values are Binding
            foreach (ListItem item in chklUserType.Items)
            {
                if (item.Selected)
                {
                    if (User_Types == string.Empty)
                        User_Types = item.Value;
                    else
                        User_Types += ',' + item.Value;
                }
            }
            
            //Department Value are Binding
            foreach (ListItem item in lstbxdept.Items)
            {
                if (item.Selected)
                {
                    if (Departments == string.Empty)
                        Departments = item.Value;
                    else
                        Departments += ',' + item.Value;
                }
            }

           
            objCommon.FillDropDown("ACCESS_LINK", "*", "", "AL_No=" + Convert.ToInt32(a) + "", "");
            if (ViewState["newsid"] == null)
            {
                
                int newid = Convert.ToInt32(objCommon.LookUp("NEWS","ISNULL(MAX(NEWSID),0)",""));
                ViewState["newsid"] = newid + 1;
            }
            byte[] imgData;
            if (fuFile.HasFile)
            {
                if (!fuFile.PostedFile.ContentLength.Equals(string.Empty) || fuFile.PostedFile.ContentLength != null)
                {
                    int fileSize = fuFile.PostedFile.ContentLength;
                    string ext = System.IO.Path.GetExtension(fuFile.FileName).ToLower();
                    int KB = fileSize / 1024;


                    if (ext == ".pdf" || ext == ".jpeg" || ext == ".jpg")
                    {
                        if (KB >= 500 && ext != ".pdf")
                        {
                            objCommon.DisplayMessage(this.Page, "Uploaded File size should be less than or equal to 500 kb.", this.Page);
                            return;
                        }
                        else if (KB >= 10240 && ext == ".pdf")
                        {
                            objCommon.DisplayMessage(this.Page, "Uploaded File size should be less than or equal to 10 mb.", this.Page);
                            return;
                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage("Please Upload PDF/JPEG/JPG file only", this.Page);
                        return;
                    }
                    

                    if (fuFile.FileName.ToString().Length > 50)
                    {
                        objCommon.DisplayMessage("Upload File Name is too long", this.Page);
                        return;
                    }




                    imgData = objCommon.GetImageData(fuFile);
                    filetext = imgData.ToString();
                    string filename_Certificate = Path.GetFileName(fuFile.PostedFile.FileName);
                    //string Ext = Path.GetExtension(fuFile.FileName);

                    filetext = (ViewState["newsid"]) + "_News_Create_Notice_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ext;
                    int retval = Blob_UploadDepositSlip(blob_ConStr, blob_ContainerName, (ViewState["newsid"]) + "_News_Create_Notice_" + DateTime.Now.ToString("yyyyMMddHHmmss"), fuFile, imgData);
                    if (retval == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Unable to upload...Please try again...');", true);

                        return;
                    }

                }
            }
            else if (!fuFile.HasFile && lblfile.Text != string.Empty)
            {
                filetext = lblfile.Text;
            }
            
            //if (fuFile.HasFile)
            //{
            //    string path = Server.MapPath("~//upload_files//notice_document//");
            //    // string path = Server.MapPath("~//ExcelData//");
            //    string ext = System.IO.Path.GetExtension(fuFile.FileName).ToLower();
            //    if (ext == ".pdf" || ext == ".doc" || ext == ".docx")
            //    {

            //    }
            //    else
            //    {
            //        objCommon.DisplayMessage("Please Upload only Pdf and Word files", this.Page);
            //        return;
            //    }

            //    if (fuFile.FileName.ToString().Length > 50)
            //    {
            //        objCommon.DisplayMessage("Upload File Name is too long", this.Page);
            //        return;
            //    }
            //}
            //else
            //{

            //    filetext = lblfile.Text;
            //}


            NewsController objNC = new NewsController();
            News objNews = new News();
            objNews.NewsTitle = txtTitle.Text.Trim();
            objNews.NewsDesc = ftbDesc.Text.Trim();
            objNews.Link = txtLinkName.Text.Trim();
            objNews.Category = Category;
           // objNews.Filename = fuFile.FileName.Replace(" ", "_");
            objNews.ExpiryDate = Convert.ToDateTime(txtExpiryDate.Text);
            int countCheck = 0;
            int recordCount = 0;
            int updCount = 0;

            DateTime dt1 = Convert.ToDateTime(txtExpiryDate.Text);
            if (dt1 < DateTime.Now)
                objNews.Status = 0;
            else
                objNews.Status = 1;

            if (rblFilter.SelectedValue == "2" || rblFilter.SelectedValue == "14")
            {
                
                foreach (ListViewDataItem lv in lvStudent.Items)
                {
                    CheckBox chkSelect = lv.FindControl("chkSelect") as CheckBox;
                    // added by kajal jaiswal 
                    int special_stud = 0;
                    if (chkSelect.Checked)
                    {
                        special_stud = 1;
                        countCheck++;
                    }
                    else
                    {
                        special_stud = 0;
                    }

                    // commented by kajal jaiswal
                    //if (chkSelect.Checked)
                    //{
                   
                    HiddenField hdnId = lv.FindControl("hdnIdno") as HiddenField;
                    //CheckBox hdnId = lv.FindControl("chkSelect") as CheckBox;
                    if (!ddlUserType.SelectedValue.Equals(string.Empty)) userType = Convert.ToInt32(User_Types);
                    if (!ddlCollege.SelectedValue.Equals(string.Empty)) collegeId = Convert.ToInt32(ddlCollege.SelectedValue);
                    if (!ddlDegree.SelectedValue.Equals(string.Empty)) degree = Convert.ToInt32(ddlDegree.SelectedValue);
                    if (!ddlBranch.SelectedValue.Equals(string.Empty)) branch = Convert.ToInt32(ddlBranch.SelectedValue);
                    if (!ddlSemester.SelectedValue.Equals(string.Empty)) semester = Convert.ToInt32(ddlSemester.SelectedValue);
                    if (!ddlDept.SelectedValue.Equals(string.Empty)) department = Convert.ToInt32(ddlDept.SelectedValue);
                    int idno = int.Parse(hdnId.Value);
                    // int idno = hdnId;
                    if (ViewState["action"] != null)
                    {
                        if (ViewState["action"].ToString().Equals("add"))
                        {
                            CustomStatus cs = (CustomStatus)objNC.AddNoticeForSelectedStudents(objNews, userType, collegeId, degree, branch, semester, select, idno, department, OrgID, filetext, special_stud);
                            if (cs.Equals(CustomStatus.RecordSaved))
                            {
                                recordCount++;
                            }
                            else if (cs.Equals(CustomStatus.RecordUpdated))
                            {
                                updCount++;
                            }
                        }
                        else if (ViewState["action"].ToString().Equals("edit"))
                        {
                            CustomStatus cs = (CustomStatus)objNC.AddNoticeForSelectedStudents(objNews,userType, collegeId, degree, branch, semester, select, idno, department, OrgID, filetext, special_stud);
                            if (cs.Equals(CustomStatus.RecordSaved))
                            {
                                recordCount++;
                            }
                            else if (cs.Equals(CustomStatus.RecordUpdated))
                            {
                                updCount++;
                            }
                        }
                    }
                    // }
                    //}
                    //else
                    //{


                    //}
                }
                if (ViewState["action"] != null)
                {
                    if (countCheck == 0 && !chkAll.Checked)
                    {
                        objCommon.DisplayMessage(this.Page, "Please Select Atleast One Student From List.", this.Page);
                        return;
                    }
                    else if (ViewState["action"].ToString().Equals("add"))
                    {
                        objCommon.DisplayMessage(this.Page, "Record Saved Successfully.", this.Page);
                        Clear();
                        pnlAdd.Visible = false;
                        pnlList.Visible = true;
                        BindListViewNotice();
                        return;

                    }
                    else if (ViewState["action"].ToString().Equals("edit"))
                    {
                        objCommon.DisplayMessage(this.Page, "Record Updated Successfully.", this.Page);
                        Clear();
                        pnlAdd.Visible = false;
                        pnlList.Visible = true;
                        BindListViewNotice();
                        return;
                    }
                    
                }
            
              
            }
            else if (rblFilter.SelectedValue == "3" || rblFilter.SelectedValue == "5" || rblFilter.SelectedValue == "8")
            {
                 //foreach (ListItem item in lstbxdept.Items)
                 //  {
                 //     if (item.Selected)
                 //     {
                   
                 //     }
                 //     objCommon.DisplayMessage(updIntake, "Please Select Department.", Page);
                 //       divdepartment.Visible = true;
                 //       lvNotice.Visible = false;
                 //       lstbxdept.Visible = true;
                 //       return;
                 //  }
               
                foreach (ListViewDataItem lv in lvFacultyList.Items)
                {
                    CheckBox chkSelect = lv.FindControl("chkSelect") as CheckBox;
                    // added by kajal jaiswal 
                    int status = 0;
                    if (chkSelect.Checked)
                    {
                        countCheck++;
                        status = 1;
                    }
                    else
                    {
                        status = 0;
                    }

                    HiddenField hdnuano = lv.FindControl("hdnuano") as HiddenField;
                    if (!ddlCollege.SelectedValue.Equals(string.Empty)) collegeId = Convert.ToInt32(ddlCollege.SelectedValue);
                    if (!ddlDegree.SelectedValue.Equals(string.Empty)) degree = Convert.ToInt32(ddlDegree.SelectedValue);
                    if (!ddlBranch.SelectedValue.Equals(string.Empty)) branch = Convert.ToInt32(ddlBranch.SelectedValue);
                    if (!ddlSemester.SelectedValue.Equals(string.Empty)) semester = Convert.ToInt32(ddlSemester.SelectedValue);
                    int ua_no = int.Parse(hdnuano.Value);
                    int All_Users = chkAllUsers.Checked ? 1 : 0;
                    string ipaddr = Request.ServerVariables["REMOTE_ADDR"].ToString();
                    int Created_By = Convert.ToInt32(Session["userno"].ToString());
                    // int idno = hdnId;
                    if (ViewState["action"] != null)
                    {
                        if (ViewState["action"].ToString().Equals("add"))
                        {
                            CustomStatus cs = (CustomStatus)objNC.AddNoticeForSelectedFaculty(objNews, filetext, status,ua_no, User_Types, collegeId, degree, branch, semester, Departments, select, Created_By, ipaddr, All_Users, OrgID);
                            if (cs.Equals(CustomStatus.RecordSaved))
                            {
                                recordCount++;
                            }
                            else if (cs.Equals(CustomStatus.RecordUpdated))
                            {
                                updCount++;
                            }
                        }
                        else if (ViewState["action"].ToString().Equals("edit"))
                        {
                            CustomStatus cs = (CustomStatus)objNC.AddNoticeForSelectedFaculty(objNews, filetext, status,ua_no, User_Types, collegeId, degree, branch, semester, Departments, select, Created_By, ipaddr, All_Users, OrgID);
                            if (cs.Equals(CustomStatus.RecordSaved))
                            {
                                recordCount++;
                            }
                            else if (cs.Equals(CustomStatus.RecordUpdated))
                            {
                                updCount++;
                            }
                        }
                    }
                }
                if (ViewState["action"] != null)
                {
                    if (countCheck == 0 && !chkAll.Checked)
                    {
                        objCommon.DisplayMessage(this.Page, "Please Select Atleast One user From List.", this.Page);
                        return;
                    }
                    else  if (ViewState["action"].ToString().Equals("add"))
                    {
                        objCommon.DisplayMessage(this.Page, "Record Saved Successfully.", this.Page);
                        Clear();
                        pnlAdd.Visible = false;
                        pnlList.Visible = true;
                        BindListViewNotice();
                        return;

                    }
                    else if (ViewState["action"].ToString().Equals("edit"))
                    {
                        objCommon.DisplayMessage(this.Page, "Record Updated Successfully.", this.Page);
                        Clear();
                        pnlAdd.Visible = false;
                        pnlList.Visible = true;
                        BindListViewNotice();
                        return;
                    }
                    
                }

               
            }
            else
            {

                if (!ddlCollege.SelectedValue.Equals(string.Empty)) collegeId = Convert.ToInt32(ddlCollege.SelectedValue);
                if (!ddlDegree.SelectedValue.Equals(string.Empty)) degree = Convert.ToInt32(ddlDegree.SelectedValue);
                if (!ddlBranch.SelectedValue.Equals(string.Empty)) branch = Convert.ToInt32(ddlBranch.SelectedValue);
                if (!ddlSemester.SelectedValue.Equals(string.Empty)) semester = Convert.ToInt32(ddlSemester.SelectedValue);
                int All_Users = chkAllUsers.Checked ? 1 : 0;
                string ipaddr = Request.ServerVariables["REMOTE_ADDR"].ToString();
                int Created_By = Convert.ToInt32(Session["userno"].ToString());
                //}
                //else if (ddlUserType.SelectedValue == "3" || ddlUserType.SelectedValue == "8")
                //{
                //if (!ddlDept.SelectedValue.Equals(string.Empty)) department = Convert.ToInt32(ddlDept.SelectedValue);

                //    }
                //}
                //string ua_nos = string.Empty;
                // if (chkAllUsers.Checked == false)
                //{

                //foreach (ListViewDataItem item in lvFacultyList.Items)
                //{
                //    CheckBox chkselectfaculty = item.FindControl("chkSelect") as CheckBox;
                //    HiddenField hdnuano = item.FindControl("hdnuano") as HiddenField;
                //    if (chkselectfaculty.Checked && chkselectfaculty.Enabled)
                //    {
                //        ua_nos += hdnuano.Value + ",";
                //    }

                //}
                // }
                //else
                //{
                //  ua_nos = string.Empty;
                // }
                //Check whether to add or update
                if (ViewState["action"] != null)
                {
                    //Add New Record
                    if (ViewState["action"].ToString().Equals("add"))
                    {

                        CustomStatus cs = (CustomStatus)objNC.Add_News_UserWise(objNews,filetext, User_Types, collegeId, degree, branch, semester, Departments, select,  Created_By, ipaddr, All_Users, OrgID);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            objCommon.DisplayMessage(this.Page, "Record Saved Successfully", this.Page);
                            Clear();
                            pnlAdd.Visible = false;
                            pnlList.Visible = true;
                            BindListViewNotice();
                        }
                        else if (cs.Equals(CustomStatus.DuplicateRecord))
                        {
                            objCommon.DisplayMessage("Record Already Exist", this.Page);
                            pnlAdd.Visible = false;
                            pnlList.Visible = true;
                            
                        }
                        else
                            if (cs.Equals(CustomStatus.FileExists))
                                objCommon.DisplayMessage("File already exists. Please upload another file or rename and upload.", this.Page);
                        //lblStatus.Text = "File already exists. Please upload another file or rename and upload.";
                    }
                    else
                    {
                        //Update Record
                        if (ViewState["newsid"] != null)
                        {
                            //set additional properties
                            objNews.NewsID = Convert.ToInt32(ViewState["newsid"].ToString());
                            objNews.OldFilename = lblfile.Text;

                            //set status
                            DateTime dt = Convert.ToDateTime(txtExpiryDate.Text);
                            if (dt < DateTime.Now)
                                objNews.Status = 0;
                            else
                                objNews.Status = 1;

                            CustomStatus cs = (CustomStatus)objNC.Update_News_UserWise(objNews, filetext , User_Types, collegeId, degree, branch, semester, Departments, select, Created_By, ipaddr, 0, OrgID);
                            if (cs.Equals(CustomStatus.RecordUpdated))
                            {
                                objCommon.DisplayMessage(this.Page, "Record Updated Successfully", this.Page);
                                Clear();
                                pnlAdd.Visible = false;
                                pnlList.Visible = true;
                                BindListViewNotice();
                            }
                            else
                                if (cs.Equals(CustomStatus.FileExists))
                                    objCommon.DisplayMessage(this.Page, "File already exists. Please upload another file or rename and upload.", this.Page);
                        }
                    }
                }
            }


        }
        catch (Exception ex)
        {

        }
        clear();
       
    }
    //protected void btnSubmit_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        //Page.Form.Attributes.Add("enctype", "multipart/form-data");
    //        string AL_ASNo = "";
    //        string Category = "";
    //        int userType = 0; int collegeId = 0; int degree = 0; int branch = 0; int semester = 0; int department = 0; int select = 0;
    //        string a = Page.Request.QueryString["pageno"].ToString();
    //        DataSet ds = objCommon.FillDropDown("ACCESS_LINK", "*", "", "AL_No=" + Convert.ToInt32(a) + "", "");
    //        if (ds.Tables.Count > 0)
    //        {
    //            if (ds.Tables[0].Rows.Count > 0)
    //            {
    //                AL_ASNo = ds.Tables[0].Rows[0]["AL_ASNo"].ToString();
    //            }
    //        }

    //        string alno = (objCommon.LookUp("ACCESS_LINK", "DISTINCT AL_ASNo", "AL_URL like 'TRAININGANDPLACEMENT%'"));
    //        int asno = Convert.ToInt32(alno);

    //        if (Convert.ToInt32(AL_ASNo) == asno)
    //        {
    //            Category = "NC_TP";
    //        }
    //        else
    //        {
    //            Category = "N/C";
    //        }
    //        if (fuFile.HasFile)
    //        {
    //            //string path=Server.MapPath("~//UPLOAD_FILES//NOTICE_DOCUMENT//");
    //            string ext = System.IO.Path.GetExtension(fuFile.FileName).ToLower();
    //            if (ext == ".pdf" || ext == ".doc" || ext == ".docx")
    //            {

    //            }
    //            else
    //            {
    //                objCommon.DisplayMessage("Please Upload only Pdf and Word files", this.Page);
    //                return;
    //            }

    //            if (fuFile.FileName.ToString().Length > 50)
    //            {
    //                objCommon.DisplayMessage("Upload File Name is too long", this.Page);
    //                return;
    //            }
    //            //if (System.IO.File.Exists(path + fuFile.FileName))
    //            //{
    //            //    //lblStatus.Text = "File already exists. Please upload another file or rename and upload.";                            
    //            //     Convert.ToInt32(CustomStatus.FileExists);
    //            //     objCommon.DisplayMessage("File already exists. Please upload another file or rename and upload.", this.Page);
    //            //     return;
    //            //}
    //            //else
    //            //{
    //            //    string uploadFile = System.IO.Path.GetFileName(fuFile.PostedFile.FileName);
    //            //    fuFile.PostedFile.SaveAs(path + uploadFile);
    //            //    //flag = true;
    //            //}
    //        }

    //        NewsController objNC = new NewsController();
    //        News objNews = new News();
    //        objNews.NewsTitle = txtTitle.Text.Trim();
    //        objNews.NewsDesc = ftbDesc.Text.Trim();
    //        objNews.Link = txtLinkName.Text.Trim();
    //        objNews.Category = Category;
    //        objNews.Filename = fuFile.FileName;
    //        objNews.ExpiryDate = Convert.ToDateTime(txtExpiryDate.Text);
    //        int countCheck = 0;
    //        int recordCount = 0;
    //        int updCount = 0;
    //        if (chkAll.Checked)
    //        {
    //            select = 1;
    //        }
    //        if (ddlUserType.SelectedValue == "2")
    //        {
    //            if (!chkAll.Checked)
    //            {
    //                foreach (ListViewDataItem lv in lvStudent.Items)
    //                {
    //                    CheckBox chkSelect = lv.FindControl("chkSelect") as CheckBox;
    //                    if (chkSelect.Checked)
    //                    {
    //                        countCheck++;
    //                        HiddenField hdnId = lv.FindControl("hdnIdno") as HiddenField;
    //                        if (!ddlUserType.SelectedValue.Equals(string.Empty)) userType = Convert.ToInt32(ddlUserType.SelectedValue);
    //                        if (!ddlCollege.SelectedValue.Equals(string.Empty)) collegeId = Convert.ToInt32(ddlCollege.SelectedValue);
    //                        if (!ddlDegree.SelectedValue.Equals(string.Empty)) degree = Convert.ToInt32(ddlDegree.SelectedValue);
    //                        if (!ddlBranch.SelectedValue.Equals(string.Empty)) branch = Convert.ToInt32(ddlBranch.SelectedValue);
    //                        if (!ddlSemester.SelectedValue.Equals(string.Empty)) semester = Convert.ToInt32(ddlSemester.SelectedValue);
    //                        if (!ddlDept.SelectedValue.Equals(string.Empty)) department = Convert.ToInt32(ddlDept.SelectedValue);
    //                        int idno = int.Parse(hdnId.Value);
    //                        //if (ViewState["action"] != null)
    //                        //{
    //                        //if (ViewState["action"].ToString().Equals("add"))
    //                        //{
    //                        CustomStatus cs = (CustomStatus)objNC.AddNoticeForSelectedStudents(objNews, fuFile, userType, collegeId, degree, branch, semester, select, idno, department);
    //                        if (cs.Equals(CustomStatus.RecordSaved))
    //                        {
    //                            recordCount++;
    //                        }
    //                        else if (cs.Equals(CustomStatus.RecordUpdated))
    //                        {
    //                            updCount++;
    //                        }
    //                        //}
    //                        //}
    //                    }
    //                }
    //                if (countCheck == recordCount && countCheck > 0)
    //                {
    //                    objCommon.DisplayMessage(this.Page, "Record Saved Successfully.", this.Page);
    //                    Clear();
    //                    pnlAdd.Visible = false;
    //                    pnlList.Visible = true;
    //                    return;
    //                }
    //                else if (countCheck == updCount && countCheck > 0)
    //                {
    //                    objCommon.DisplayMessage(this.Page, "Record Updated Successfully.", this.Page);
    //                    Clear();
    //                    pnlAdd.Visible = false;
    //                    pnlList.Visible = true;
    //                    return;
    //                }
    //            }
    //            if (countCheck == 0 && !chkAll.Checked)
    //            {
    //                objCommon.DisplayMessage(this.Page, "Please Select Atleast One Student From List.", this.Page);
    //                return;
    //            }
    //        }


    //        if (!ddlUserType.SelectedValue.Equals(string.Empty)) userType = Convert.ToInt32(ddlUserType.SelectedValue);
    //        if (!ddlCollege.SelectedValue.Equals(string.Empty)) collegeId = Convert.ToInt32(ddlCollege.SelectedValue);
    //        if (!ddlDegree.SelectedValue.Equals(string.Empty)) degree = Convert.ToInt32(ddlDegree.SelectedValue);
    //        if (!ddlBranch.SelectedValue.Equals(string.Empty)) branch = Convert.ToInt32(ddlBranch.SelectedValue);
    //        if (!ddlSemester.SelectedValue.Equals(string.Empty)) semester = Convert.ToInt32(ddlSemester.SelectedValue);
    //        int All_Users = chkAllUsers.Checked ? 1 : 0;
    //        string ipaddr = Request.ServerVariables["REMOTE_ADDR"].ToString();
    //        int Created_By = Convert.ToInt32(Session["userno"].ToString());
    //        //}
    //        //else if (ddlUserType.SelectedValue == "3" || ddlUserType.SelectedValue == "8")
    //        //{
    //        if (!ddlDept.SelectedValue.Equals(string.Empty)) department = Convert.ToInt32(ddlDept.SelectedValue);

    //        //    }
    //        //}
    //        string ua_nos = string.Empty;
    //        if (chkAllUsers.Checked == false)
    //        {

    //            foreach (ListViewDataItem item in lvFacultyList.Items)
    //            {
    //                CheckBox chkselectfaculty = item.FindControl("chkSelect") as CheckBox;
    //                HiddenField hdnuano = item.FindControl("hdnuano") as HiddenField;
    //                if (chkselectfaculty.Checked)
    //                {
    //                    ua_nos += hdnuano.Value + ",";
    //                }

    //            }
    //        }
    //        else
    //        {
    //            ua_nos = string.Empty;
    //        }





    //        //Check whether to add or update
    //        if (ViewState["action"] != null)
    //        {
    //            //Add New Record
    //            if (ViewState["action"].ToString().Equals("add"))
    //            {

    //                CustomStatus cs = (CustomStatus)objNC.Add_News_UserWise(objNews, fuFile, userType, collegeId, degree, branch, semester, department, select, ua_nos, Created_By, ipaddr, All_Users);
    //                if (cs.Equals(CustomStatus.RecordSaved))
    //                {
    //                    objCommon.DisplayMessage(this.Page, "Record Saved Successfully", this.Page);
    //                    Clear();
    //                    pnlAdd.Visible = false;
    //                    pnlList.Visible = true;
    //                }
    //                else if (cs.Equals(CustomStatus.DuplicateRecord))
    //                {
    //                    objCommon.DisplayMessage("Record Already Exist", this.Page);
    //                    pnlAdd.Visible = false;
    //                    pnlList.Visible = true;
    //                }
    //                else
    //                    if (cs.Equals(CustomStatus.FileExists))
    //                        objCommon.DisplayMessage("File already exists. Please upload another file or rename and upload.", this.Page);
    //                //lblStatus.Text = "File already exists. Please upload another file or rename and upload.";
    //            }
    //            else
    //            {
    //                //Update Record
    //                if (ViewState["newsid"] != null)
    //                {
    //                    //set additional properties
    //                    objNews.NewsID = Convert.ToInt32(ViewState["newsid"].ToString());
    //                    objNews.OldFilename = hdnFile.Value;

    //                    //set status
    //                    DateTime dt = Convert.ToDateTime(txtExpiryDate.Text);
    //                    if (dt < DateTime.Now)
    //                        objNews.Status = 0;
    //                    else
    //                        objNews.Status = 1;

    //                    CustomStatus cs = (CustomStatus)objNC.Update_News_UserWise(objNews, fuFile, userType, collegeId, degree, branch, semester, department, select, ua_nos, Created_By, ipaddr, All_Users);
    //                    if (cs.Equals(CustomStatus.RecordUpdated))
    //                    {
    //                        objCommon.DisplayMessage(this.Page, "Record Updated Successfully", this.Page);
    //                        Clear();
    //                        pnlAdd.Visible = false;
    //                        pnlList.Visible = true;
    //                    }
    //                    else
    //                        if (cs.Equals(CustomStatus.FileExists))
    //                            objCommon.DisplayMessage(this.Page, "File already exists. Please upload another file or rename and upload.", this.Page);
    //                }
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objCommon.ShowError(Page, "notice.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objCommon.ShowError(Page, "Server UnAvailable");
    //    }
    //}
    protected void rblFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        lstbxdept.Items.Clear();
        lstbxdept.DataSource = null;
        lstbxdept.DataBind();
        NewsController objNC = new NewsController();
        int UA_TYPE =Convert.ToInt32(rblFilter.SelectedValue);
        DataSet ds = objNC.GetDepartment(UA_TYPE);
        lstbxdept.DataSource = ds;
        lstbxdept.DataValueField = "DEPTNO";
        lstbxdept.DataTextField = "DEPTNAME";
        lstbxdept.DataBind();
        if (rblFilter.SelectedValue == "2")
        {
            chklUserType.SelectedIndex = 0;
            chklUserType.SelectedValue = "2";
            divStudent.Visible = true;
            btnShow.Visible = true;
            lvStudent.DataSource = null;
            lvStudent.DataBind();
            btnParentList.Visible = false;
            btnShowFaculty.Visible = false;
            lvFacultyList.Visible = false;
            divdepartment.Visible = false;

            foreach (ListItem item in chklUserType.Items)
            {
                if (item.Value.ToString().Equals("2"))
                {
                    item.Enabled = true;
                }
                else
                {
                    item.Enabled = false;
                }
            }

        }
        else if (rblFilter.SelectedValue == "3")
        {
            divdepartment.Visible = true;
            chklUserType.SelectedIndex = 0;
            chklUserType.SelectedValue = "3";
            btnShowFaculty.Visible = true;
            divStudent.Visible = false;
            btnShow.Visible = false;
            lvFacultyList.DataSource = null;
            lvFacultyList.DataBind();
            btnParentList.Visible = false;
            btnShowFaculty.Text = "Show Faculty List";

            foreach (ListItem item in chklUserType.Items)
            {
                if (item.Value.ToString().Equals("3"))
                {
                    item.Enabled = true;
                }
                else
                {
                    item.Enabled = false;
                }
            }

        }
        else if (rblFilter.SelectedValue == "8")
        {
            divdepartment.Visible = true;
            chklUserType.SelectedIndex = 0;
            chklUserType.SelectedValue = "8";
            btnShowFaculty.Visible = true;
            btnShow.Visible = false;
            lvFacultyList.DataSource = null;
            lvFacultyList.DataBind();
            lvStudent.Visible = false;
            btnParentList.Visible = false;
            divStudent.Visible = false;
            btnShowFaculty.Text = "Show HOD List";

            foreach (ListItem item in chklUserType.Items)
            {
                if (item.Value.ToString().Equals("8"))
                {
                    item.Enabled = true;
                }
                else
                {
                    item.Enabled = false;
                }
            }
        }
        else if (rblFilter.SelectedValue == "5")
        {
          
            divdepartment.Visible = true;
            chklUserType.SelectedIndex = 0;
            chklUserType.SelectedValue = "5";
            btnShowFaculty.Visible = true;
            divStudent.Visible = false;
            btnShow.Visible = false;
            lvFacultyList.DataSource = null;
            lvFacultyList.DataBind();
            btnShowFaculty.Text = "Show Non-Teaching List";

            foreach (ListItem item in chklUserType.Items)
            {
                if (item.Value.ToString().Equals("5"))
                {
                    item.Enabled = true;
                }
                else
                {
                    item.Enabled = false;
                }
            }
        }
        else if (rblFilter.SelectedValue == "5")
        {
            chklUserType.SelectedIndex = 0;
            chklUserType.SelectedValue = "5";
            btnShowFaculty.Visible = true;
            divStudent.Visible = false;
            btnShow.Visible = false;
            lvFacultyList.DataSource = null;
            lvFacultyList.DataBind();
            lvStudent.DataSource = null;
            lvStudent.DataBind();
            btnShowFaculty.Text = "Show Affiliated College Users List";

            foreach (ListItem item in chklUserType.Items)
            {
                if (item.Value.ToString().Equals("5"))
                {
                    item.Enabled = true;
                }
                else
                {
                    item.Enabled = false;
                }
            }
        }
        else if (rblFilter.SelectedValue == "14")
        {
            chklUserType.SelectedIndex = 0;
            chklUserType.SelectedValue = "14";
            divStudent.Visible = true;
            btnShow.Visible = false;
            lvStudent.DataSource = null;
            lvStudent.DataBind();
            btnParentList.Visible = true;
            btnShowFaculty.Visible = false;
            lvFacultyList.Visible = false;
            divdepartment.Visible = false;
            foreach (ListItem item in chklUserType.Items)
            {
                if (item.Value.ToString().Equals("14"))
                {
                    item.Enabled = true;
                }
                else
                {
                    item.Enabled = false;
                }
            }

        }
    }

    //Added By Ruchika Dhakate
    protected void btnParentList_Click(object sender, EventArgs e)
    {

        if (rblFilter.SelectedValue == "14")
        {

            DataSet dsStudent = objCommon.FillDropDown("ACD_STUDENT S INNER JOIN USER_ACC UA ON (S.IDNO=UA.UA_IDNO)", "S.IDNO", "UA.UA_TYPE,0 AS SPECIAL_STUD ,UA.UA_NAME AS REGNO,UA.UA_FULLNAME AS STUDNAME,S.COLLEGE_ID,S.DEGREENO,S.BRANCHNO,S.SEMESTERNO,2 as flag ", "UA.UA_TYPE=14 AND S.COLLEGE_ID =" + Convert.ToInt32(ddlCollege.SelectedValue) + " AND S.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND S.BRANCHNO= " + Convert.ToInt32(ddlBranch.SelectedValue) + " AND S.SEMESTERNO= " + Convert.ToInt32(ddlSemester.SelectedValue), "");

            if (dsStudent.Tables[0].Rows.Count > 0)
            {
                dsStudent.Tables[0].Rows[0]["REGNO"].ToString();
                lvStudent.DataSource = dsStudent;
                lvStudent.DataBind();
                pnlStudent.Visible = true;
                lvStudent.Visible = true;
                hdnCount.Value = lvStudent.Items.Count.ToString();
                //foreach (ListViewDataItem lv in lvStudent.Items)
                //{
                //    Label lblStudName = (Label)lv.FindControl("Label1");
                //    Label lblstudent = (Label)lv.FindControl("lblstudent");
                //    lblstudent.Text = "Parent List";
                //    lblStudName.Text = "Parent Name";
                //}

            }
            else
            {
                lvStudent.DataSource = null;
                lvStudent.DataBind();
                pnlStudent.Visible = false;
                lvStudent.Visible = false;
                objCommon.DisplayMessage(this.Page, "Record Not Found For Current Selection.", this.Page);
                return;
            }
        }
    }

    #region BlogStorage
    public int Blob_UploadDepositSlip(string ConStr, string ContainerName, string DocName, FileUpload FU, byte[] ChallanCopy)
    {
        CloudBlobContainer container = Blob_Connection(ConStr, ContainerName);
        int retval = 1;

        string Ext = Path.GetExtension(FU.FileName);
        string FileName = DocName + Ext;
        try
        {
            DeleteIFExits(FileName);
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            container.CreateIfNotExists();
            container.SetPermissions(new BlobContainerPermissions
            {
                PublicAccess = BlobContainerPublicAccessType.Blob
            });

            CloudBlockBlob cblob = container.GetBlockBlobReference(FileName);
            cblob.Properties.ContentType = System.Net.Mime.MediaTypeNames.Application.Pdf;
            if (!cblob.Exists())
            {
                using (Stream stream = new MemoryStream(ChallanCopy))
                {
                    cblob.UploadFromStream(stream);
                }
            }
            //cblob.UploadFromStream(FU.PostedFile.InputStream);
        }
        catch
        {
            retval = 0;
            return retval;
        }
        return retval;
    }

    public DataTable Blob_GetById(string ConStr, string ContainerName, string Id)
    {
        CloudBlobContainer container = Blob_Connection(ConStr, ContainerName);
        System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
        var permission = container.GetPermissions();
        permission.PublicAccess = BlobContainerPublicAccessType.Container;
        container.SetPermissions(permission);

        DataTable dt = new DataTable();
        dt.TableName = "FilteredBolb";
        dt.Columns.Add("Name");
        dt.Columns.Add("Uri");

        //var blobList = container.ListBlobs(useFlatBlobListing: true);
        var blobList = container.ListBlobs(Id, true);
        foreach (var blob in blobList)
        {
            string x = (blob.Uri.ToString().Split('/')[blob.Uri.ToString().Split('/').Length - 1]);
            string y = x.Split('_')[0];
            dt.Rows.Add(x, blob.Uri);
        }
        return dt;
    }

    private CloudBlobContainer Blob_Connection(string ConStr, string ContainerName)
    {
        CloudStorageAccount account = CloudStorageAccount.Parse(ConStr);
        CloudBlobClient client = account.CreateCloudBlobClient();
        CloudBlobContainer container = client.GetContainerReference(ContainerName);
        return container;
    }
    public void DeleteIFExits(string FileName)
    {
        CloudBlobContainer container = Blob_Connection(blob_ConStr, blob_ContainerName);
        string FN = Path.GetFileNameWithoutExtension(FileName);
        try
        {
            Parallel.ForEach(container.ListBlobs(FN, true), y =>
            {
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
                ((CloudBlockBlob)y).DeleteIfExists();
            });
        }
        catch (Exception) { }
    }
    #endregion BlogStorage

    protected void imgbtnPreview_Click(object sender, ImageClickEventArgs e)
    {
        ////Added By Prafull
        string Url = string.Empty;
        string directoryPath = string.Empty;
        try
        {
            string blob_ConStr = System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"].ToString();
            string blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerName"].ToString();

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(blob_ConStr);
            CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();
            string directoryName = "~/DownloadImg" + "/";
            directoryPath = Server.MapPath(directoryName);

            if (!Directory.Exists(directoryPath.ToString()))
            {

                Directory.CreateDirectory(directoryPath.ToString());
            }
            CloudBlobContainer blobContainer = cloudBlobClient.GetContainerReference(blob_ContainerName);
            string img = ((System.Web.UI.WebControls.ImageButton)(sender)).ToolTip.ToString();
            var ImageName = img;
            if (img != null || img != "")
            {
                DataTable dtBlobPic = Blob_GetById(blob_ConStr, blob_ContainerName, img);
                var blob = blobContainer.GetBlockBlobReference(ImageName);
                string filePath = directoryPath + "\\" + ImageName;
               
                if ((System.IO.File.Exists(filePath)))
                {
                    System.IO.File.Delete(filePath);
                }
               
                blob.DownloadToFile(filePath, System.IO.FileMode.CreateNew);
                string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"500px\" height=\"400px\">";
                embed += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
                embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
                embed += "</object>";

                ltEmbed.Text = string.Format(embed, ResolveUrl("~/DownloadImg/" + ImageName));
                BindListViewNotice();

              
            }

        }
        catch (Exception ex)
        {
            
        }

         
    }


    //public string GetFileName(object FILENAME)
    //{
    //    if (FILENAME != null && FILENAME.ToString() != "")
    //        return FILENAME.ToString();
    //    else
    //        return "None";
    //}

    //public string GetFileNamePath(object FILENAME)
    //{
    //    if (filename != null && filename.ToString() != "")
    //        return "~/upload_files/notice_document/" + filename.ToString();
    //    else
    //        return "";
    //}


  
}
