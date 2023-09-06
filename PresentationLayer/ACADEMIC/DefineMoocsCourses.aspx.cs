using System;
using System.Configuration;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Net;
using System.IO;

public partial class ACADEMIC_DefineMoocsCourses : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    FeeCollectionController feeController = new FeeCollectionController();
    UAIMS_Common objUCommon = new UAIMS_Common();
    CourseController objCC = new CourseController();

    #region Page Events

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
                    //this.CheckPageAuthorization();

                    // Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    // Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                   
                    PopulateDropDownList();
                    //string host = Dns.GetHostName();
                    //IPHostEntry ip = Dns.GetHostEntry(host);
                    //string IPADDRESS = string.Empty;
                    //IPADDRESS = ip.AddressList[0].ToString();
                    //ViewState["ipAddress"] = IPADDRESS;
                    objCommon.SetLabelData("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]));//Set label -  Added By Dileep K. on 25/01/2022
                    objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));  // Set Page Header  -  Added By Dileep K. on 25/01/2022
                }
            }
        }
        catch 
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
                Response.Redirect("~/notauthorized.aspx?page=DefineMoocsCourses.aspx");
            }
        }
        else
        {
            // Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=DefineMoocsCourses.aspx");
        }
    }
    private void PopulateDropDownList()
    {
       // objCommon.FillDropDownList(ddlSession, "ACD_STUDENT_RESULT SR INNER JOIN ACD_SESSION_MASTER SM ON (SR.SESSIONNO=SM.SESSIONNO)", "distinct SR.SESSIONNO", "SM.SESSION_PNAME", "SM.SESSIONNO>0", "SR.SESSIONNO DESC");
      //  objCommon.FillDropDownList(ddlScheme, "ACD_COURSE C INNER JOIN ACD_SCHEME S ON(C.SCHEMENO=S.SCHEMENO)", "DISTINCT C.SCHEMENO", "S.SCHEMENAME", "C.SCHEMENO>0", "S.SCHEMENAME ASC");
        objCommon.FillDropDownList(ddlScheme, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID) INNER JOIN ACD_SCHEME SC ON(SC.SCHEMENO=SM.SCHEMENO) INNER JOIN ACD_COURSE C ON C.SCHEMENO=SC.SCHEMENO", "DISTINCT COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) + " AND (SC.DEPTNO IN(" + Session["userdeptno"].ToString() + "))", "COSCHNO");
    }
    #endregion

    protected void btnShow_Click(object sender, EventArgs e)
    {
        BindCourseList();
    }

    private void BindCourseList()
    {
        try
        {

            int schemeno = int.Parse(ViewState["schemeno"].ToString());

            DataSet dscourse = null;

            dscourse = objCC.GetCourseForMoocs(schemeno, Convert.ToInt32(ddlSession.SelectedValue),Convert.ToInt32(Session["OrgId"]));
            if (dscourse != null && dscourse.Tables.Count > 0 && dscourse.Tables[0].Rows.Count > 0)
            {
                lvCourse.DataSource = dscourse;
                lvCourse.DataBind();
                objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvCourse);//Set label -
                pnlCourse.Visible = true;
                btnSubmit.Enabled = true;
              
            }
            else
            {
                lvCourse.DataSource = null;
                lvCourse.DataBind();
                btnSubmit.Enabled = false; ;
                pnlCourse.Visible = false; ;
                objCommon.DisplayMessage(this.updpnl, "Record Not Found!", this.Page);
            }
        }
        catch 
        {
            throw;
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //cancel();
        Response.Redirect(Request.Url.ToString());
        // ddlSession.SelectedIndex = 0;
        // ddlScheme.SelectedIndex = 0;
        //// lvCourse.Visible = false;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string offcourse = string.Empty;
        string sem = string.Empty;
        int count = 0;
        foreach (ListViewDataItem dataitem in lvCourse.Items)
        {

            CheckBox cbRow = dataitem.FindControl("chkoffered") as CheckBox;
            if (cbRow.Checked == true)
            {
                count++;
            }
        }

        if (count == 0)
        {
            objCommon.DisplayMessage(updpnl, "Please Select atleast One Course!!", this.Page);
            return;
        }
        try
        {
           

            int SchemeNo = Convert.ToInt32(ViewState["schemeno"]);
            int ua_no = Convert.ToInt32(Session["userno"]);
            int Sessionno = Convert.ToInt32(ddlSession.SelectedValue);
            string IpAddress = Request.ServerVariables["REMOTE_ADDR"].ToString();
            int OrgId = Convert.ToInt32(Session["OrgId"]);
            foreach (ListViewDataItem dataitem in lvCourse.Items)
            {
                CheckBox chkBox = dataitem.FindControl("chkoffered") as CheckBox;
                Label lblsem = dataitem.FindControl("LblSemNo") as Label;

                if (chkBox.Checked == true)
                {
                    //objCourse.Offered. += chkBox.ToolTip + ",";
                    
                    offcourse += chkBox.ToolTip + ",";
                    sem += lblsem.Text + ",";
                   
                }
            }

            CustomStatus cs = (CustomStatus)objCC.EnterMoocsCourse(Sessionno, offcourse, SchemeNo, sem, ua_no, IpAddress, OrgId);

            if (cs.Equals(CustomStatus.RecordUpdated))
            {
                //this.BindListView();
                objCommon.DisplayMessage(this.updpnl, "MOOCs Courses Saved Successfully!!", this.Page);
                ClearControls();
            }
            else
                objCommon.DisplayMessage(this.updpnl, "Error in Saving!!", this.Page);
        
        }
        catch 
        {
            throw;
        }
    }

    private void ClearControls()
    {
        ddlSession.SelectedIndex = 0;
        ddlScheme.SelectedIndex = 0;
        pnlCourse.Visible = false;
        btnSubmit.Enabled = false;
    }

    protected void lvCourse_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        ListViewDataItem dataitem = (ListViewDataItem)e.Item;
        CheckBox ChkOffer = dataitem.FindControl("chkoffered") as CheckBox;
        Label lblSem = dataitem.FindControl("LblSemNo") as Label;
        ChkOffer.Checked = lblSem.ToolTip.Equals("1") ? true : false;
    }
    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ddlScheme.SelectedIndex = 0;
        lvCourse.DataSource = null;
        lvCourse.DataBind();
        pnlCourse.Visible = false;
        btnSubmit.Enabled = false;
    }
    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlSession.SelectedIndex = 0;
        DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlScheme.SelectedValue));
        //ViewState["degreeno"]
        if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
        {
            ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
            ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
            ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
            ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();
        }

        objCommon.FillDropDownList(ddlSession, "ACD_STUDENT_RESULT SR INNER JOIN ACD_SESSION_MASTER SM ON (SR.SESSIONNO=SM.SESSIONNO)", "distinct SR.SESSIONNO", "SM.SESSION_PNAME", "SM.SESSIONNO>0 AND SM.COLLEGE_ID="+ViewState["college_id"], "SR.SESSIONNO DESC");

        lvCourse.DataSource = null;
        lvCourse.DataBind();
        pnlCourse.Visible = false;
        btnSubmit.Enabled = false;
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        ShowdDATA();
    }

    private void ShowdDATA()
    {
        try
        {
            GridView GVMoocs = new GridView();

            string session = objCommon.LookUp("ACD_SESSION_MASTER", "SESSION_NAME", "SESSIONNO=" + ddlSession.SelectedValue);
            string scheme = objCommon.LookUp("ACD_SCHEME", "SCHEMENAME", "SCHEMENO=" + ViewState["schemeno"]);
            //string sem = ddlSem.SelectedItem.Text;

            int SessionNo = Convert.ToInt32(ddlSession.SelectedValue);
            int SchemeNo = Convert.ToInt32(ViewState["schemeno"]);
           // int SemesterNo = Convert.ToInt32(ddlSem.SelectedValue);
            
            DataSet ds = new DataSet();

            ds = objCC.GetMoocsCoursesData(SessionNo, SchemeNo);


            if (ds.Tables[0].Rows.Count > 0)
            {
                GVMoocs.DataSource = ds;
                GVMoocs.DataBind();

               // string attachment = "attachment; filename=" + session.Replace(" ", "_") + "_" + scheme.Replace(" ", "_") + "MOOCS_Courses" + ".xls";
                string attachment = "attachment; filename=" + "MOOCS_Courses" + ".xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.MS-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                GVMoocs.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
            else
            {
                objCommon.DisplayMessage(this.updpnl, "No Data Found for current selection.", this.Page);
            }
        }
        catch 
        {
            throw;
        }
    }
}