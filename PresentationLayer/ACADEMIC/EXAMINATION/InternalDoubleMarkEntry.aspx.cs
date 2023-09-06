//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC
// PAGE NAME     : InternalDoubleMarkEntry                                     
// CREATION DATE : 15-OCT-2011
// CREATED BY    :                                                 
// MODIFIED DATE : 07/11/2019
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

public partial class ACADEMIC_InternalDoubleMarkEntry : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common(); 
    MarksEntryController objmec = new MarksEntryController();
    string _uaims_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

    //int i = 0;
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
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                Binddata();
                ViewState["Lock"] = 0;


                Session["currentsession"] = objCommon.LookUp("ACD_SESSION_MASTER", "SESSIONNO", "FLOCK=1"); // ADD BY ROSHAN PANNASE 31-12-2015 FOR COURSE REGISTER IN START SESSION ONELY.

                //Set the Page Title
                // Page.Title = Session["coll_name"].ToString();

                //File Dropdown Box


                //CHECK THE STUDENT LOGIN
                string ua_type = objCommon.LookUp("User_Acc", "UA_TYPE", "UA_NO=" + Convert.ToInt32(Session["userno"]) + " and  UA_TYPE =" + Convert.ToInt32(Session["usertype"]) + "");
                ViewState["usertype"] = ua_type;

                if (CheckActivity())
                {
                    //FillDropdown();
                    if (ViewState["usertype"].ToString() == "2")
                    {
                        //CHECK ACTIVITY FOR EXAM REGISTRATION
                        //CheckActivity();
                        //divCourses.Visible = true;
                        pnlMain.Visible = false;
                        //this.ShowDetails();
                        // BindStudentDetails();
                        this.CheckPageAuthorization();
                    }
                    else if (ViewState["usertype"].ToString() == "1" || ViewState["usertype"].ToString() == "3")
                    {
                        //divNote.Visible = false;
                        // pnlSearch.Visible = true;
                        this.CheckPageAuthorization();
                    }
                    else
                    {
                        pnlMain.Visible = false;
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
        sessionno = Session["currentsession"].ToString();
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
            //string chkintext=objCommon.LookUp("ACD_EXAM_MARKENTRY_A

            DataSet ds = objCommon.FillDropDown("ACD_EXAM_MARKENTRY_ALLOCATION a inner join acd_course b on (a.courseno=b.courseno) inner join ACD_STUDENT_RESULT SR ON (SR.COURSENO=A.COURSENO AND SR.SESSIONNO=A.SESSIONNO) inner join acd_college_master c on(a.college_id=c.college_id) inner join ACD_SCHEME S on (s.Schemeno=b.schemeno)", " distinct b.courseno, b.course_name,(case when S.degreeno in(2,5,7) then c.college_name else s.schemename end) college_name, A.college_id,a.opid,isnull(a.lock,0) as lock", "b.ccode", "a.op_id=" + Convert.ToInt32(Session["userno"]) + "and a.int_ext=1 AND ISNULL(SR.CANCEL,0) = 0", "b.ccode");
            if (ds.Tables[0].Rows.Count > 0)
            {
                //ViewState["opid"] = ds.Tables[0].Rows[0]["opid"].ToString();
                //ViewState["lock"] = ds.Tables[0].Rows[0]["lock"].ToString();
                lvCourse.DataSource = ds;//.Tables[0];
                lvCourse.DataBind();
               
                foreach (ListViewDataItem item in lvCourse.Items)
                {
                 
                    Label lblcompstatus = item.FindControl("lblcompstatus") as Label;
                    Label lbllockstatus = item.FindControl("lbllockstatus") as Label;
                    HiddenField hdnopid = item.FindControl("hdnopid") as HiddenField;
                    HiddenField hdnlock = item.FindControl("hdnlock") as HiddenField;
                    ImageButton imgbutton = item.FindControl("ImgReport") as ImageButton;
                    int comp = Convert.ToInt32(objCommon.LookUp("ACD_MARKS_ENTRY_OPERTOR A INNER JOIN ACD_EXAM_MARKENTRY_ALLOCATION B ON (A.OPID=B.OPID)", "count(1)", "A.OPID=" + Convert.ToInt32(hdnopid.Value)+" AND B.INT_EXT=1"));

                    if (comp != 0)
                    {
                        lblcompstatus.ForeColor = System.Drawing.Color.Blue;
                        lblcompstatus.Text = "In Process";
                        imgbutton.Visible = true;
                    }
                    else
                    {
                        lblcompstatus.ForeColor = System.Drawing.Color.Red;
                        lblcompstatus.Text = "Pending";
                        imgbutton.Visible = false;
                       
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

    
    protected void btnReportS_Click(object sender, EventArgs e)
    {
        ImageButton imgbutton = sender as ImageButton;
        int opid = Convert.ToInt32(imgbutton.AlternateText);
        int courseno = Convert.ToInt32(imgbutton.CommandArgument);
        ShowReport("Internal_Mark_entry_Report", "rptInternalMarkEntry.rpt",courseno,opid);
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
           Response.Redirect(Request.Url.ToString());
          //divcourses.Visible = true;
          //Binddata();
          //btnClear.Visible = false;

    }

    protected void gvStudent_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox txtMarks1 = e.Row.FindControl("txtTotMarks") as TextBox;
            TextBox txtMarks2 = e.Row.FindControl("txtTAMarks") as TextBox;
            TextBox txtMarks3 = e.Row.FindControl("txtESPRMarks") as TextBox;
            TextBox txtMarks4 = e.Row.FindControl("txts4mark") as TextBox;
            TextBox txtMarks5 = e.Row.FindControl("txts5mark") as TextBox;
            TextBox txtMarks6 = e.Row.FindControl("txts6mark") as TextBox; 
            TextBox txtMarks7 = e.Row.FindControl("txts7mark") as TextBox;
            TextBox txtMarks8 = e.Row.FindControl("txts8mark") as TextBox;
            //TextBox txtMarks9 = e.Row.FindControl("txts9mark") as TextBox;
            TextBox txtMarks10 = e.Row.FindControl("txts10mark") as TextBox;
         // string ii=  txtMarks1.Text.ToString();
            HiddenField hdnlock = e.Row.FindControl("hdnlock") as HiddenField;
            if (hdnlock.Value == "1")
            {
                txtMarks1.Enabled = false;
                txtMarks2.Enabled = false;
                txtMarks3.Enabled = false;
                txtMarks4.Enabled = false;
                txtMarks5.Enabled = false;

                     txtMarks6.Enabled = false;
                txtMarks7.Enabled = false;
                txtMarks8.Enabled = false;
                //txtMarks9.Enabled = false;
                txtMarks10.Enabled = false;
            }
            else
            {
                txtMarks1.Enabled = true;
                txtMarks2.Enabled = true;
                txtMarks3.Enabled = true;
                txtMarks4.Enabled = true;
                txtMarks5.Enabled = true;

                txtMarks6.Enabled = true;
                txtMarks7.Enabled = true;
                txtMarks8.Enabled = true;
                //txtMarks9.Enabled = false;
                txtMarks10.Enabled = true;
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

            //ViewState["courseno"] = lnk.ToolTip;
            //ViewState["colg"] = s1;
            ViewState["opid"] = sec_batch[1].ToString();
            int j=2;

            ////Commented and Added by Mr.Manish Walde on date 14-Jan-2016
            //DataSet dsk = objCommon.FillDropDown("ACD_STUDENT_RESULT a inner join acd_student b on(a.idno=b.idno) inner join acd_scheme c on (a.schemeno=c.schemeno) inner join acd_course d on(d.courseno=a.courseno)", "distinct a.IDNO, b.REGNO, ROLL_NO, RANDOM_NO, a.SECTIONNO, a.SEMESTERNO, SESSIONNO, a.SCHEMENO", "a.COURSENO, COURSENAME, d.SUBID, d.CCODE, d.SRNO, EXTERMARK, S1MARK, S2MARK, S3MARK, S4MARK, S5MARK, S6MARK, S7MARK, S8MARK, S9MARK, S10MARK, LOCKE, LOCKS1, LOCKS2, LOCKS3, LOCKS4, LOCKS5, LOCKS6, LOCKS7, LOCKS8, LOCKS9, LOCKS10, d.CORE_SUBID, PATTERNNO, MAXMARKS_E, S1MAX, S2MAX, S3MAX, S4MAX, S5MAX, S6MAX, S7MAX, S8MAX, S9MAX, S10MAX, '" + ViewState["lock"] + "' as lock", "a.courseno=" + Convert.ToInt32(lnk.ToolTip) + "AND B.COLLEGE_ID=" + Convert.ToInt32(s1), "a.idno");
            DataSet dsk = objCommon.FillDropDown("ACD_STUDENT_RESULT a inner  join acd_student b on(a.idno=b.idno) inner join ACD_COURSE C on(a.courseno=c.courseno ) INNER JOIN ACD_SCHEME S ON (C.SCHEMENO = S.SCHEMENO)", "distinct  '" + ViewState["lock"] + "' as lock,A.REGNO,B.STUDNAME,A.IDNO, C.COURSENO, C.CCODE", "C.COURSE_NAME, C.SEMESTERNO, C.SCHEMENO, C.SUBID, C.SRNO, C.CORE_SUBID,S.PATTERNNO, C.MAXMARKS_E,S1MARK, S2MARK, S3MARK, S4MARK, S5MARK, S6MARK, S7MARK, S8MARK, S9MARK, S10MARK, LOCKE, LOCKS1, LOCKS2, LOCKS3, LOCKS4, LOCKS5, LOCKS6, LOCKS7, LOCKS8, LOCKS9, LOCKS10, C.S1MAX, C.S2MAX, C.S3MAX, C.S4MAX, C.S5MAX, C.S6MAX, C.S7MAX, C.S8MAX, C.S9MAX, C.S10MAX", "C.COURSENO=" + Convert.ToInt32(lnk.ToolTip) + " AND B.COLLEGE_ID=" + Convert.ToInt32(s1) + " AND a.REGISTERED=1 and ISNULL(A.cancel,0)=0", "A.REGNO"); 
            
            if (dsk.Tables[0].Rows.Count > 0)
            {
                pnlStudGrid.Visible = true;
                btnSave.Visible = true;
                btnClear.Visible = true;
                string patern = dsk.Tables[0].Rows[0]["PATTERNNO"].ToString();
                //string patern = objCommon.LookUp("acd_scheme", "PATTERNNO", "schemeno=" + Convert.ToInt32(dsk.Tables[0].Rows[0]["schemeno"].ToString()));
                //ViewState["pattern"] = patern;
                DataSet dspattern = objCommon.FillDropDown("ACD_EXAM_NAME", "ROW_NUMBER() OVER(ORDER BY EXAMNO) SRNO", "EXAMNO, EXAMNAME, FLDNAME", "patternno=" + patern + "  AND EXAMNAME!='' AND FLDNAME!='EXTERMARK'", "examno");
                DataTableReader dtrExams = dspattern.Tables[0].CreateDataReader();
                 //for (j = 3; j < gvStudent.Columns.Count; j++)
                 //{
                    while (dtrExams.Read())
                    {
                        string s = dtrExams["FLDNAME"].ToString();
                        int examrow = (Convert.ToInt32(dtrExams["SRNO"].ToString())-1);
                        int col = Convert.ToInt16(s.Substring(1, 1));
                        if(Convert.ToInt32(dsk.Tables[0].Rows[0][s+"MAX"]) > 0)
                        {
                            gvStudent.Columns[2+col].HeaderText = dspattern.Tables[0].Rows[examrow]["examname"].ToString() + "[Max : " + dsk.Tables[0].Rows[0][s + "MAX"].ToString() + "]";
                            gvStudent.Columns[2+col].FooterText = s;
                            gvStudent.Columns[2+col].Visible = true;  
                            //break;
                        }
                    }
                 //}
                    gvStudent.DataSource = dsk.Tables[0];
                    gvStudent.DataBind();
                // divcourses.Visible = false;

                ShowMark();
                //this.BindJS();
            }
            else
            {
                pnlStudGrid.Visible = false;
                btnSave.Visible = false;
                btnClear.Visible = false;
                //divcourses.Visible = true ;
                gvStudent.DataSource = null;
                gvStudent.DataBind();
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
        DataSet dsallo = objCommon.FillDropDown("ACD_MARKS_ENTRY_OPERTOR A INNER JOIN ACD_EXAM_MARKENTRY_ALLOCATION B ON (A.OPID=B.OPID)", "A.*", "ENTRYDATE", "A.OPID=" + Convert.ToInt32(ViewState["opid"]) + " AND B.INT_EXT=1", string.Empty);

        if (dsallo.Tables[0].Rows.Count > 0)
        {
            btnLock.Visible = true;
           // btnReport.Visible = true;
            DataSet dsinfo = objmec.GetStudentmarkDetails(Convert.ToInt32(ViewState["opid"]));
            gvStudent.DataSource = dsinfo.Tables[0];
            gvStudent.DataBind();
        }
        else
        {
            btnLock.Visible = false; 
            //gvStudent.DataSource = null;
            //gvStudent.DataBind();
            //Added by Mr.Manish Walde on date 14-Jan-2016
            //pnlStudGrid.Visible = false;
            //btnSave.Visible = false;
            //btnClear.Visible = false;
           // btnReport.Visible = false;
        } 
    }

    #region Commented By Mr.Manish Walde on date 14-Jan-2016 Because this methods are not in use on form
    //Commented By Manish Because this methods are not in use on form
    //protected void BindJS()
    //{
    //    try
    //    {
    //        //int subid = Convert.ToInt32(objCommon.LookUp("ACD_COURSE", "SUBID", "COURSENO =" + ddlCourse.SelectedValue));
    //        string chklock = objCommon.LookUp("ACD_EXAM_MARKENTRY_ALLOCATION", "Count(1)", "isnull(lock,0)=1 and opid=" + Convert.ToInt32(ViewState["opid"]));
    //        //DataSet chkunlockstud = objCommon.FillDropDown("ACD_MARKS_ENTRY_OPERTOR", "distinct idno","examname", "isnull(lock,0)=0 and opid=" + Convert.ToInt32(ViewState["opid"]),"idno");
    //        DataSet ds = objmec.GetMarkComparewithlock(Convert.ToInt32(ViewState["courseno"]), Convert.ToInt32(ViewState["colg"]), Convert.ToInt32(1));
              
    //        //foreach (GridViewRow gvRow in gvStudent.Rows)
    //        //{
    //        //    TextBox txtMarks1 = gvRow.FindControl("txtTotMarks") as TextBox;
    //        //    TextBox txtMarks2 = gvRow.FindControl("txtTAMarks") as TextBox;
    //        //    TextBox txtMarks3 = gvRow.FindControl("txtESPRMarks") as TextBox;
    //        //    TextBox txtMarks4 = gvRow.FindControl("txts4mark") as TextBox;
    //        //    TextBox txtMarks5 = gvRow.FindControl("txts5mark") as TextBox;
    //        //    HiddenField hdnlock = gvRow.FindControl("hdnlock") as HiddenField;

    //        //    if (hdnlock.Value.Equals(0))
    //        //    {
    //        //        txtMarks1.Enabled = false;
    //        //        txtMarks2.Enabled = false;
    //        //        txtMarks3.Enabled = false;
    //        //        txtMarks4.Enabled = false;
    //        //        txtMarks5.Enabled = false;
    //        //    }
    //        //    else
    //        //    {
    //        //        txtMarks1.Enabled = true;
    //        //        txtMarks2.Enabled = true;
    //        //        txtMarks3.Enabled = true;
    //        //        txtMarks4.Enabled = true;
    //        //        txtMarks5.Enabled = true;
    //        //    }
    //            //if (chklock != string.Empty && chklock != "0")
    //            //{
    //            //    if (ds.Tables[0].Rows.Count > 0)
    //            //    {
    //            //        for (int i=0 ;i<=ds.Tables[0].Rows.Count;i++)
    //            //        {
    //            //           // ds.Tables[0].Rows[i]["idno"].ToString()=
    //            //        }
    //            //    } 
    //            //    txtMarks1.Enabled = false;
    //            //    txtMarks2.Enabled = false;
    //            //    txtMarks3.Enabled = false;
    //            //    txtMarks4.Enabled = false;
    //            //    txtMarks5.Enabled = false;
    //            //}
    //        //}
    //    }

    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "Academic_MarkEntry.btnSubmit_Click --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //}

    //protected void BindJSlist()
    //{
    //    try
    //    {
    //        int comp = Convert.ToInt32(objCommon.LookUp("ACD_MARKS_ENTRY_OPERTOR", "count(1)", "OPID=" + Convert.ToInt32(ViewState["opid"])));

    //        foreach (ListViewDataItem item in lvCourse.Items)
    //        {
    //            Label lblcomp = lvCourse.FindControl("lblcompstatus") as Label;
    //            Label lbllock = lvCourse.FindControl("lbllockstatus") as Label;
    //            if (comp != 0)
    //            {
    //                lblcomp.ForeColor = System.Drawing.Color.Red;
    //                lblcomp.Text = "Incompleted";
    //            }
    //            else
    //            {
    //                lblcomp.Text = "Incompelte";
    //                //lbllock.Text = "Unlock";
    //            }

    //        }

    //    }

    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "Academic_MarkEntry.btnSubmit_Click --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //}
    #endregion Commented By Mr.Manish Walde on date 14-Jan-2016 Because this methods are not in use on form

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string studids = string.Empty;
            string examname = string.Empty;
            string marks = string.Empty;
            string chkexist = objCommon.LookUp("ACD_EXAM_MARKENTRY_ALLOCATION", "Count(1)", "lock=1 and opid=" + Convert.ToInt32(ViewState["opid"]));
            if (chkexist != string.Empty && chkexist != "0")
            {
                objCommon.DisplayMessage(updSection, "Record already Saved and Locked...", this.Page);
                return;
            }
            int k = 0;
            int columns=0;
            for (int i = 3; i < gvStudent.Columns.Count; i++)
            {
                if (gvStudent.Columns[i].Visible == true)
                {
                    columns++;
                }    
            }

            int count = columns * (gvStudent.Rows.Count);
            for (int i = 0; i < gvStudent.Rows.Count; i++)
            {
                Label lblidnos = gvStudent.Rows[i].FindControl("lblIDNO") as Label;
                for (int j = 3; j < gvStudent.Columns.Count; j++)
                {
                    if (gvStudent.Columns[j].Visible == true)
                    {
                        TableCell tc = gvStudent.Rows[i].Cells[j];
                        TextBox myControl = null;
                        foreach (Control c in tc.Controls)
                        {
                            if (c is TextBox)
                            {
                                myControl = (TextBox)c;
                                break;
                            }
                        }
                        string value = myControl.Text.Trim();
                        if (value != "" || value != string.Empty)
                        {
                            marks += value + ',';
                        }
                        else
                        {
                            marks += "-100" + ',';
                            k++;
                        }
                        
                        if (i == 0)
                        {
                            examname += gvStudent.Columns[j].FooterText + "MARK" + ',';
                        }
                    }
                }
                studids += lblidnos.ToolTip.ToString() + ',';
            }

            ////Commented and Added by Mr.Manish Walde on date 14-Jan-2016 this logic is added in upper for loop
            //for (int j = 3; j < gvStudent.Columns.Count; j++)
            //{
            //    if (gvStudent.Columns[j].Visible == true)
            //    {
            //        examname += gvStudent.Columns[j].FooterText + "MARK" + ',';
            //    }
            //}
  
            if (marks != "") 
            {
                if (k == count)
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

            if (examname != "")
            {
                if (examname.Substring(examname.Length - 1) == ",")
                    examname = examname.Substring(0, examname.Length - 1);
            }

            int cs = objmec.MarkEntrybyOperator(Convert.ToInt32(ViewState["opid"]), studids.ToString().TrimEnd(','), examname.ToString().TrimEnd(','), marks.ToString().TrimEnd(','));
            if (cs == 1)
            {
                if (ViewState["Lock"].ToString() != "1")
                {
                    objCommon.DisplayMessage(updSection,"Record saved successfully", this.Page);
                }
                ShowMark();
                return;
            }
            else
            {
                objCommon.DisplayMessage(updSection, "Error occured while submitting data.Please contact MIS!!.", this.Page);
                return;
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
    protected void btnLock_Click(object sender, EventArgs e)
    {
       try
       {
        string chkexist = objCommon.LookUp("ACD_EXAM_MARKENTRY_ALLOCATION", "Count(1)", "lock=1 and opid=" + Convert.ToInt32(ViewState["opid"]));
        if (chkexist != string.Empty && chkexist !="0")
        {
            objCommon.DisplayMessage(updSection, "Already locked...", this.Page);
            return;
        }
        for (int i = 0; i < gvStudent.Rows.Count; i++)
        {
            for (int j = 3; j < gvStudent.Columns.Count; j++)
            {
                if (gvStudent.Columns[j].Visible == true)
                {
                    TableCell tc = gvStudent.Rows[i].Cells[j];
                    TextBox myControl = null;
                    foreach (Control c in tc.Controls)
                    {
                        if (c is TextBox)
                        {
                            myControl = (TextBox)c;
                            break;
                        }
                    }
                    string value = myControl.Text.Trim();
                    if (value == "" || value == string.Empty)
                    {
                        objCommon.DisplayMessage(updSection, "Mark entry could not be locked...!! Please enter marks of all students", this.Page);
                        return;
                    }
                    
                }
            }
        }
        ViewState["Lock"] = 1;
        btnSave_Click(sender, e);
        int chklock = objmec.LockEntrybyOperator(Convert.ToInt32(ViewState["opid"]));
        if (chklock == 2)
        {
            objCommon.DisplayMessage(updSection, "Record locked successfully", this.Page);
            this.ShowMark();
            this.Binddata();
            return;
        }
        else
        {
            objCommon.DisplayMessage(updSection,"Error...", this.Page);
            return;
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
    private void ShowReport( string reportTitle, string rptFileName,int Courseno,int opid)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_opid=" + opid+ ",@P_COURSENO=" + Courseno;
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
    //protected void btnReport_Click(object sender, EventArgs e)
    //{
    //    //ShowReport( "Internal_Mark_entry_Report", "rptInternalMarkEntry.rpt");
    //}
}
