//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC
// PAGE NAME     : ExternalDoubleMarkEntry                                     
// CREATION DATE : 15-OCT-2011
// CREATED BY    :                                                 
// MODIFIED DATE : 04-Nov-2019
// MODIFIED BY   : Nidhi Gour
// MODIFIED DESC :                                                    
//======================================================================================


using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class ACADEMIC_ExternalDoubleMarkEntry : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common(); 
    MarksEntryController objmec = new MarksEntryController();
    string _uaims_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

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
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
 
                objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE STARTED = 1 AND  SHOW_STATUS =1 AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%')", "");
                    
                //Binddata();
                ViewState["Lock"] = 0;

                Session["currentsession"] = objCommon.LookUp("ACD_SESSION_MASTER", "SESSIONNO", "FLOCK=1"); // ADD BY ROSHAN PANNASE 31-12-2015 FOR COURSE REGISTER IN START SESSION ONELY.

                //CHECK THE STUDENT LOGIN
                string ua_type = objCommon.LookUp("User_Acc", "UA_TYPE", "UA_NO=" + Convert.ToInt32(Session["userno"]) + " and  UA_TYPE =" + Convert.ToInt32(Session["usertype"]) + "");
                ViewState["usertype"] = ua_type;

                if (CheckActivity())
                {
                    //FillDropdown();
                    if (ViewState["usertype"].ToString() == "2")
                    {
                         pnlMain.Visible = false;
                        this.CheckPageAuthorization();
                    }
                    //else if (ViewState["usertype"].ToString() == "1" || ViewState["usertype"].ToString() == "3")
                    //{
                    //    this.CheckPageAuthorization();
                    //}
                    else
                    {
                        this.CheckPageAuthorization();
                        //pnlMain.Visible = false;
                        pnlMain.Visible = true;
                    }
                }
                else
                {

                }
              
                string IPADDRESS = string.Empty;

                
               // ViewState["ipAddress"] = GetUserIPAddress(); //Request.ServerVariables["REMOTE_ADDR"];
                ViewState["ipAddress"] = "14.139.110.183"; //Request.ServerVariables["REMOTE_ADDR"];


            }
        } divMsg.InnerHtml = string.Empty;
    }

    private bool CheckActivity()
    {

        bool ret = true;
        string sessionno = string.Empty;
        //sessionno = objCommon.LookUp("SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (AM.ACTIVITY_NO = SA.ACTIVITY_NO)", "SA.SESSION_NO", "AM.ACTIVITY_CODE = 'EXAMREG' AND SA.STARTED = 1");
        //sessionno = Session["currentsession"].ToString();
        sessionno = objCommon.LookUp("ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE STARTED = 1 AND  SHOW_STATUS =1 AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%')");
        if (String.IsNullOrEmpty(sessionno))
            sessionno = "0";
        ActivityController objActController = new ActivityController();
        DataTableReader dtr = objActController.CheckActivity(Convert.ToInt32(sessionno), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Request.QueryString["pageno"].ToString()));

        if (dtr.Read())
        {
            ViewState["ACTIVITY_NO"] = Convert.ToInt32(dtr["ACTIVITY_NO"]);

            if (dtr["STARTED"].ToString().ToLower().Equals("false"))
            {
                objCommon.DisplayMessage("This Activity has been Stopped. Contact Admin.!!", this.Page);
                pnlMain.Visible = false;
                ret = false;

            }

            //if (dtr["PRE_REQ_ACT"] == DBNull.Value || dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
            if (dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
            {
                objCommon.DisplayMessage("Pre-Requisite Activity for this Page is Not Stopped!!", this.Page);
                pnlMain.Visible = false;
                ret = false;
            }

        }
        else
        {
            objCommon.DisplayMessage("Either this Activity has been Stopped Or You are Not Authorized to View this Page. Contact Admin.", this.Page);
            pnlMain.Visible = false;
            ret = false;
        }
        dtr.Close();
        return ret;
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=teacherallotment.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=teacherallotment.aspx");
        }
    }
    private void Binddata()
    {
        try
        {
            //DataSet ds = objCommon.FillDropDown("ACD_EXAM_MARKENTRY_ALLOCATION a inner join acd_course b on (a.courseno=b.courseno) inner join acd_college_master c on(a.college_id=c.college_id) inner join ACD_Scheme s on (b.schemeno=S.schemeno)", " distinct b.courseno, b.course_name,(case when S.degreeno in(2,5,7) then CONVERT(varchar(10), c.college_id) else s.schemename end) Scheme ,c.college_name ,A.college_id,a.opid,isnull(a.lock,0) as lock", "b.ccode", "a.op_id=" + Convert.ToInt32(Session["userno"]) + "and a.int_ext=2", "b.ccode"); //commented on 19-03-2020 by Vaishali
            DataSet ds = objCommon.FillDropDown("ACD_EXAM_MARKENTRY_ALLOCATION a inner join acd_course b on (a.courseno=b.courseno) inner join acd_college_master c on(a.college_id=c.college_id) inner join ACD_Scheme s on (b.schemeno=S.schemeno)", " distinct b.courseno, b.course_name,s.schemename Scheme ,c.college_name ,A.college_id,a.opid,isnull(a.lock,0) as lock", "b.ccode", "a.op_id=" + Convert.ToInt32(Session["userno"]) + " and a.sessionno = " + Convert.ToInt32(ddlSession.SelectedValue)+ " AND A.IS_MIDS_ENDS = " + Convert.ToInt32(ddlExamName.SelectedValue) + "", "b.ccode"); // modified on 19-03-2020 by Vaishali
            if (ds.Tables[0].Rows.Count > 0)
            {
                //ViewState["opid"] = ds.Tables[0].Rows[0]["opid"].ToString();
                lvCourse.DataSource = ds;//.Tables[0];
                lvCourse.DataBind();
                lvCourse.Visible = true;
               
                foreach (ListViewDataItem item in lvCourse.Items)
                {
                    
                    Label lblcompstatus = item.FindControl("lblcompstatus") as Label;
                    Label lbllockstatus = item.FindControl("lbllockstatus") as Label;
                    HiddenField hdnopid = item.FindControl("hdnopid") as HiddenField;
                    HiddenField hdnlock = item.FindControl("hdnlock") as HiddenField;
                    ImageButton imgbtn = item.FindControl("ImgReport") as ImageButton;
                    Label lblrandomno = item.FindControl("lblrandomno") as Label;

                    HiddenField hdfcourseno = item.FindControl("hdfcourseno") as HiddenField;
                    HiddenField hdncolg = item.FindControl("hdncolg") as HiddenField;
                                         
                    string MINRAND = objCommon.LookUp("ACD_STUDENT_RESULT SR INNER JOIN ACD_STUDENT S on (SR.IDNO=S.IDNO)", "MIN(RANDOM_NO)", " SR.COURSENO=" +hdfcourseno.Value + " AND S.COLLEGE_ID="+hdncolg .Value );

                    string MIXRAND = objCommon.LookUp("ACD_STUDENT_RESULT SR INNER JOIN ACD_STUDENT S on (SR.IDNO=S.IDNO)", "MAX(RANDOM_NO)", " SR.COURSENO=" +hdfcourseno.Value + " AND S.COLLEGE_ID="+hdncolg .Value );

                    lblrandomno.Text = MINRAND + "-" + MIXRAND;
                    lblrandomno.ForeColor = System.Drawing.Color.Green;

                    //int comp = Convert.ToInt32(objCommon.LookUp("ACD_MARKS_ENTRY_OPERTOR A INNER JOIN ACD_EXAM_MARKENTRY_ALLOCATION B ON (A.OPID=B.OPID)", "count(1)", "A.OPID=" + Convert.ToInt32(hdnopid.Value)+" AND B.INT_EXT=2")); // commented on 20-03-2020 by Vaishali
                    int comp = Convert.ToInt32(objCommon.LookUp("ACD_MARKS_ENTRY_OPERTOR A INNER JOIN ACD_EXAM_MARKENTRY_ALLOCATION B ON (A.OPID=B.OPID)", "count(1)", "A.OPID=" + Convert.ToInt32(hdnopid.Value) + "")); // modified on 20-03-2020 by Vaishali

                    if (comp != 0)
                    {
                        lblcompstatus.ForeColor = System.Drawing.Color.Blue;
                        lblcompstatus.Text = "In Process";
                        imgbtn.Visible = true;
                    }
                    else
                    {
                        lblcompstatus.ForeColor = System.Drawing.Color.Red;
                        lblcompstatus.Text = "Incomplete";
                        imgbtn.Visible = false;
                       
                    }
                    if (hdnlock.Value !="False" )
                    {
                        lbllockstatus.ForeColor = System.Drawing.Color.Green;
                        lbllockstatus.Text = "Lock";

                        lblcompstatus.ForeColor = System.Drawing.Color.Green;
                        lblcompstatus.Text = "Completed";
                        
                    }
                    else
                    {
                        lbllockstatus.ForeColor = System.Drawing.Color.Red;
                        lbllockstatus.Text = "Unlock";
                    }

                }
            }
            else
            {
                lvCourse.DataSource = null;
                lvCourse.DataBind();
                pnlDetails.Visible = false;
                pnlStudGrid.Visible = false;   //added on 21-03-2020 by Vaishali
                ddlExamName.SelectedIndex = 0;  //added on 23-03-2020 by Vaishali
                divButtons.Visible = false;
                objCommon.DisplayMessage(this.updSection, "No Courses Found !!!!", this.Page);   //added on 23-03-2020 by Vaishali
                return;                                                                         //added on 23-03-2020 by Vaishali
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Registration_teacherallotment.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
            {
                objUCommon.ShowError(Page, "Server UnAvailable");
            }
        }
    }

   



    protected void btnClear_Click(object sender, EventArgs e)
    {
        //Response.Redirect(Request.Url.ToString());
        pnlDetails.Visible = true;
        pnlStudGrid.Visible = false;
        divButtons.Visible = false;
    }


    protected void gvStudent_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox txtMarks1 = e.Row.FindControl("txtExternmark") as TextBox;

            HiddenField hdnlock = e.Row.FindControl("hdnlock") as HiddenField;
            if (hdnlock.Value == "1")
            {
                txtMarks1.Enabled = false;
            }
            else
            {
                txtMarks1.Enabled = true;
            }
        }
    }
    protected void lnkbtnCourse_Click(object sender, EventArgs e)
    {
        try
        {
            //Show the Student List with Exams that are ON
            //=============================================
            LinkButton lnk = sender as LinkButton;
            title.InnerText = "Enter Marks for following Students of" + "   " + lnk.Text;
            string[] sec_batch = lnk.CommandArgument.ToString().Split('+');
            string s1 = sec_batch[0].ToString();
            ViewState["opid"] = sec_batch[1].ToString();
            //DataSet dsk = objCommon.FillDropDown("ACD_STUDENT_RESULT a inner join acd_student b on(a.idno=b.idno) inner join acd_scheme c on (a.schemeno=c.schemeno) inner join acd_course d on(d.courseno=" + Convert.ToInt32(lnk.ToolTip) + ")", "DISTINCT (Case when (a.RANDOM_NO is null )then  a.regno else  (concat (a.regno, ' / ',a.RANDOM_NO) ) end ) AS RANDOM_NO ,D.MAXMARKS_E,A.ROLL_NO,A.EXTERMARK AS EXTERNMARK,a.idno,*, null  as lock", "b.studname", "a.courseno=" + Convert.ToInt32(lnk.ToolTip) + "AND B.COLLEGE_ID=" + Convert.ToInt32(s1) + " AND a.REGISTERED=1 AND a.EXAM_REGISTERED=1 and ISNULL(A.cancel,0)=0", "A.RANDOM_NO");  //commented on 21-03-2020 by Vaishali
            DataSet dsk = objmec.GetLockMarkEntryForOperator(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(lnk.ToolTip), Convert.ToInt32(Session["userno"]), Convert.ToInt32(ddlExamName.SelectedValue));
            if (dsk.Tables[0].Rows.Count > 0)
            {
                pnlDetails.Visible = false; // added on 27-03-2020 by Vaishali
                pnlStudGrid.Visible = true;
                btnSave.Visible = true;
                btnClear.Visible = true;
                //string patern = objCommon.LookUp("acd_scheme", "PATTERNNO", "schemeno=" + Convert.ToInt32(dsk.Tables[0].Rows[0]["schemeno"].ToString()));  //commented on 21-03-2020 by Vaishali
                //ViewState["pattern"] = patern;   //commented on 21-03-2020 by Vaishali
                //DataSet dspattern = objCommon.FillDropDown("ACD_EXAM_NAME", "examname", "*", "patternno=" + Convert.ToInt32(ViewState["pattern"]), "examno desc");  //commented on 21-03-2020 by Vaishali
                //gvStudent.Columns[4].HeaderText = dspattern.Tables[0].Rows[0]["examname"].ToString() + "[Max : " + dsk.Tables[0].Rows[0]["MAXMARKS_E"].ToString() + "]";// +"[Min : " + dsk.Tables[0].Rows[0]["S1MIN"].ToString() + "]"; //commented on 21-03-2020 by Vaishali
                gvStudent.Columns[4].HeaderText = dsk.Tables[0].Rows[0]["EXAMNAME"].ToString() + "[Max : " + dsk.Tables[0].Rows[0]["MAXMARKS"].ToString() + "]"; // added on 21-03-2020 by Vaishali
                gvStudent.Columns[4].Visible = true;
                gvStudent.DataSource = dsk.Tables[0];
                gvStudent.DataBind();
                btnLock.Visible = true;
                divButtons.Visible = true;

                int chkexist = Convert.ToInt32(objCommon.LookUp("ACD_EXAM_MARKENTRY_ALLOCATION", "Count(1)", "lock=1 and opid=" + Convert.ToInt32(ViewState["opid"])));
                if (chkexist != 0)
                {
                    btnSave.Enabled = false;
                    btnLock.Enabled = false;
                }
                else
                {
                    btnSave.Enabled = true;
                    btnLock.Enabled = true;
                }
                //ShowMark();
            }
            else
            {
                pnlStudGrid.Visible = false;
                btnSave.Visible = false;
                btnClear.Visible = false;
                gvStudent.DataSource = null;
                gvStudent.DataBind();
                btnLock.Visible = false;
                divButtons.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_MarkEntry.lnkbtnCourse_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void ShowMark()
    {
        DataSet dsallo = objCommon.FillDropDown("ACD_MARKS_ENTRY_OPERTOR A INNER JOIN ACD_EXAM_MARKENTRY_ALLOCATION B ON (A.OPID=B.OPID)", "A.*", "ENTRYDATE", "A.OPID=" + Convert.ToInt32(ViewState["opid"])+" AND B.INT_EXT=2", string.Empty);
        //DataSet dsallo = objmec.GetLockMarkEntryForOperator(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(lnkbtnCours));
        if (dsallo.Tables[0].Rows.Count > 0)
        {
            btnLock.Visible = true;
            DataSet dsinfo = objmec.GetStudentExtmarkDetails(Convert.ToInt32(ViewState["opid"]));
            gvStudent.DataSource = dsinfo.Tables[0];
            gvStudent.DataBind();
        }
        else
        {
            btnLock.Visible = false;
        } 
    }
    protected void BindJSlist()
    {
        try
        {
            int comp = Convert.ToInt32(objCommon.LookUp("ACD_MARKS_ENTRY_OPERTOR", "count(1)", "OPID=" + Convert.ToInt32(ViewState["opid"])));

            foreach (ListViewDataItem item in lvCourse.Items)
            {
                Label lblcomp = lvCourse.FindControl("lblcompstatus") as Label;
                Label lbllock = lvCourse.FindControl("lbllockstatus") as Label;
                if (comp != 0)
                {
                    lblcomp.ForeColor = System.Drawing.Color.Red;
                    lblcomp.Text = "Incompleted";
                }
                else
                {
                    lblcomp.Text = "Incompelte";
                    //lbllock.Text = "Unlock";
                }

            }

        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_MarkEntry.btnSubmit_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void BindJS()
    {
        try
        {
            string chklock = objCommon.LookUp("ACD_EXAM_MARKENTRY_ALLOCATION", "Count(1)", "isnull(lock,0)=1 and opid=" + Convert.ToInt32(ViewState["opid"]));
            foreach (GridViewRow gvRow in gvStudent.Rows)
            {
                TextBox txtMarks1 = gvRow.FindControl("txtExternmark") as TextBox;

                if (chklock != string.Empty && chklock != "0")
                {
                    txtMarks1.Enabled = false;
                }
            }

        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_MarkEntry.btnSubmit_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnLock_Click(object sender, EventArgs e)
    {      
        //string chkexist = objCommon.LookUp("ACD_EXAM_MARKENTRY_ALLOCATION", "Count(1)", "lock=1 and opid=" + Convert.ToInt32(ViewState["opid"]));
        //if (chkexist != string.Empty && chkexist != "0")
        //{
        //    objCommon.DisplayMessage(this.updSection,"Marks are already locked !!!!", this.Page);
        //    return;
        //}
        for (int i = 0; i < gvStudent.Rows.Count; i++)
        {
            Label lblidnos = gvStudent.Rows[i].FindControl("lblIDNO") as Label;
            TextBox txts1marks = gvStudent.Rows[i].FindControl("txtExternmark") as TextBox;
            if (txts1marks.Text == string.Empty || txts1marks.Text == "")
            {
                objCommon.DisplayMessage(updSection,"Mark entry could not be locked!! Please enter marks of all students", this.Page);
                return;
            }
           
        }
        ViewState["Lock"] = 1;
        btnSave_Click(sender, e);
        int chklock = objmec.LockEntrybyOperator(Convert.ToInt32(ViewState["opid"]));
        
        if (chklock == 2)
        {
            objCommon.DisplayMessage(updSection,"Marks locked successfully", this.Page);

           for(int i = 0;i<=gvStudent.Rows.Count - 1;i++)
           {
                TextBox txtmarks = (TextBox)gvStudent.Rows[i].FindControl("txtExternmark") as TextBox;  
                txtmarks.Enabled = false;
           }
            
            btnSave.Enabled = false;            // added on 21-03-2020 by Vaishali
            btnLock.Enabled = false;            // added on 21-03-2020 by Vaishali
            this.Binddata();
            ViewState["Lock"] = 0;   // added on 02-04-2020 by Vaishali
            return;
        }
        else
        {
            objCommon.DisplayMessage(updSection,"Error...", this.Page);
            return;
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

        string studids = string.Empty;
        string examname = string.Empty;
        string marks = string.Empty;
        int count = gvStudent.Rows.Count;
        int j = 0;
        for (int i = 0; i < gvStudent.Rows.Count; i++)
        {
            Label lblidnos = gvStudent.Rows[i].FindControl("lblIDNO") as Label;
            TextBox txts1marks = gvStudent.Rows[i].FindControl("txtExternmark") as TextBox;
            if (txts1marks.Text != string.Empty || txts1marks.Text != "")
            {
                marks += txts1marks.Text.ToString() + ',';
            }
            else
            {
                marks += "-100" + ',';
                j++;
            }
         studids += lblidnos.ToolTip.ToString() + ',';
        }
 
        if (marks != "")
        {
            if (j == count)
            {
                objCommon.DisplayMessage(updSection,"Please Enter marks of atleast one student", this.Page);
                return;
            }
            else
            {
                if (marks.Substring(marks.Length - 1) == ",")
                    marks = marks.Substring(0, marks.Length - 1);
            }
        }
           
        if (studids != "")
        {
            if (studids.Substring(studids.Length - 1) == ",")
                studids = studids.Substring(0, studids.Length - 1);
        }

        // added on 21-03-2020 by Vaishali
        if(ddlExamName.SelectedValue == "1")
            examname = "S3MARK";
        else if(ddlExamName.SelectedValue == "2")
            examname = "EXTERMARK";

        for (int i = 0; i < gvStudent.Rows.Count; i++)
        {
            Label lblidnos = gvStudent.Rows[i].FindControl("lblIDNO") as Label;
            TextBox txts1marks = gvStudent.Rows[i].FindControl("txtExternmark") as TextBox;
            if (txts1marks.Text == string.Empty || txts1marks.Text == "")
            {
                objCommon.DisplayMessage(updSection, "Mark entry could not be saved!! Please enter marks of all students", this.Page);
                return;
            }

        }

        int cs = objmec.MarkEntrybyOperator(Convert.ToInt32(ViewState["opid"]), studids.ToString().TrimEnd(','), examname.ToString().TrimEnd(','), marks.ToString().TrimEnd(','));
        if (cs == 1)
        {
            if (ViewState["Lock"].ToString() != "1")
            {
                objCommon.DisplayMessage(updSection, "Marks saved successfully", this.Page);
                Binddata();   // added on 23-03-2020 by Vaishali
            }
            //return;
        }
        else
        {
            objCommon.DisplayMessage(updSection, "Error occured while submitting data.Please contact MIS!!.", this.Page);
            return;
        }
    }

    protected void btnReportS_Click(object sender, EventArgs e)
    {
        ImageButton imgbutton = sender as ImageButton;
        int opid = Convert.ToInt32(imgbutton.AlternateText);
        int courseno = Convert.ToInt32(imgbutton.CommandArgument);
        ShowReport("External_Mark_entry_Report", "rptExternalMarkEntry.rpt", courseno, opid);
    }
    private void ShowReport(string reportTitle, string rptFileName, int Courseno, int opid)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_OPID=" + opid + ",@P_COURSENO=" + Courseno; // commented on 23-03-2020 by Vaishali
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_OPID=" + opid + ",@P_COURSENO=" + Courseno +",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_IS_MIDS_ENDS="+ddlExamName.SelectedValue;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.updSection, this.updSection.GetType(), "controlJSScript", sb.ToString(), true);
            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "StudentReport1.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    //added on 20-03-2020 by Vaishali
    protected void ddlExamName_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnlDetails.Visible = false;
        pnlStudGrid.Visible = false;
        divButtons.Visible = false;
        if (ddlExamName.SelectedIndex > 0)
        {
            pnlDetails.Visible = true;
            Binddata();
            divButtons.Visible = false;
        }
        else
        {
            pnlDetails.Visible = false;
            pnlStudGrid.Visible = false;
            divButtons.Visible = false;
        }
    }
}
