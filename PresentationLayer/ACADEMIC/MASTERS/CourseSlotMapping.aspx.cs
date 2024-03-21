//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : Examination                                                             
// PAGE NAME     : Course Slot Mapping                                                        
// CREATION DATE : 10-March-2024                                                         
// CREATED BY    : INJAMAM ANSARI        
// MODIFIED BY   :                                              
// MODIFIED DATE :                                                                      
// MODIFIED DESC :                                              
//======================================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class ACADEMIC_MASTERS_CourseSlotMapping : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ExamController objExamController = new ExamController();
    AcademinDashboardController objADEController = new AcademinDashboardController();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                Page.Title = Session["coll_name"].ToString();

                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                this.PopulateDropDown();
                divMsg.InnerHtml = string.Empty;
                ViewState["ipadress"] = Request.ServerVariables["REMOTE_ADDR"];
            }
        }
    }

    protected void PopulateDropDown()
    {
        try
        {
            DataSet ds1 = objADEController.Get_College_Session(5, Session["college_nos"].ToString());
            if (ds1.Tables[0].Rows.Count > 0)
            {
                ddlSession.DataSource = ds1;
                ddlSession.DataValueField = ds1.Tables[0].Columns[0].ToString();
                ddlSession.DataTextField = ds1.Tables[0].Columns[1].ToString();
                ddlSession.DataBind();
            }
            objCommon.FillDropDownList(ddlcourseslot, "ACD_EXAM_TT_SLOT", "DISTINCT SLOTNO", "SLOTNAME", "ISNULL(COURSE_SLOT,0)=1", "SLOTNO");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_MASTERS_CourseSlotMapping.PopulateDropDown --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ClearDropDown(ddlScheme);
            ClearDropDown(ddlSemester);
            ClearDropDown(ddlSubjecttype);
            ddlcourseslot.SelectedIndex = 0;
            pnlcommonapply.Visible = false;
            txtrowFrom.Text = string.Empty;
            txtrowTo.Text = string.Empty;
            lvcourse.DataSource = null;
            lvcourse.DataBind();
            if (ddlSession.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME S INNER JOIN ACD_OFFERED_COURSE OC ON (OC.SCHEMENO=S.SCHEMENO) INNER JOIN ACD_SESSION_MASTER SM ON (SM.SESSIONNO=OC.SESSIONNO)", "DISTINCT S.SCHEMENO", "S.SCHEMENAME", "SM.SESSIONID=" + Convert.ToInt32(ddlSession.SelectedValue), "S.SCHEMENO");
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_MASTERS_CourseSlotMapping.ddlSession_SelectedIndexChanged --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ClearDropDown(ddlSemester);
            ClearDropDown(ddlSubjecttype);
            ddlcourseslot.SelectedIndex = 0;
            pnlcommonapply.Visible = false;
            txtrowFrom.Text = string.Empty;
            txtrowTo.Text = string.Empty;
            lvcourse.DataSource = null;
            lvcourse.DataBind();
            if (ddlScheme.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER S INNER JOIN ACD_OFFERED_COURSE OC ON (OC.SEMESTERNO=S.SEMESTERNO) INNER JOIN ACD_SESSION_MASTER SM ON (SM.SESSIONNO=OC.SESSIONNO)", "DISTINCT S.SEMESTERNO", "S.SEMESTERNAME", "SM.SESSIONID=" + Convert.ToInt32(ddlSession.SelectedValue) + "AND OC.SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue), "S.SEMESTERNO");
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_MASTERS_CourseSlotMapping.ddlScheme_SelectedIndexChanged --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ClearDropDown(ddlSubjecttype);
            ddlcourseslot.SelectedIndex = 0;
            pnlcommonapply.Visible = false;
            txtrowFrom.Text = string.Empty;
            txtrowTo.Text = string.Empty;
            lvcourse.DataSource = null;
            lvcourse.DataBind();
            if (ddlSemester.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlSubjecttype, "ACD_SUBJECTTYPE S INNER JOIN ACD_COURSE C ON (S.SUBID=C.SUBID) INNER JOIN ACD_OFFERED_COURSE OC ON (C.COURSENO=OC.COURSENO) INNER JOIN ACD_SESSION_MASTER SM ON (SM.SESSIONNO=OC.SESSIONNO)", "DISTINCT S.SUBID", "S.SUBNAME", "SM.SESSIONID=" + Convert.ToInt32(ddlSession.SelectedValue) + "AND OC.SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + "AND OC.SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue), "S.SUBID");
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_MASTERS_CourseSlotMapping.ddlSemester_SelectedIndexChanged --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void ddlSubjecttype_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            txtrowFrom.Text = string.Empty;
            txtrowTo.Text = string.Empty;
            ddlcourseslot.SelectedIndex = 0;
            pnlcommonapply.Visible = false;
            lvcourse.DataSource = null;
            lvcourse.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_MASTERS_CourseSlotMapping.ddlSubjecttype_SelectedIndexChanged --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnshow_Click(object sender, EventArgs e)
    {
        try
        {
            pnlcommonapply.Visible = false;
            txtrowFrom.Text = string.Empty;
            txtrowTo.Text = string.Empty;
            ddlcourseslot.SelectedIndex = 0;
            getCourses();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_MASTERS_CourseSlotMapping.btnshow_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void getCourses()
    {
        try
        {
            DataSet ds = new DataSet();
            string pro_ = "PKG_GET_COURSE_FOR_COURSESLOT_MAPPING";
            string para = "@P_SESSIONID,@P_SCHEMENO,@P_SEMESTERNO,@P_SUBID";
            string value = "" + Convert.ToInt32(ddlSession.SelectedValue) + "," + Convert.ToInt32(ddlScheme.SelectedValue) + "," + Convert.ToInt32(ddlSemester.SelectedValue) + "," + Convert.ToInt32(ddlSubjecttype.SelectedValue);
            ds = objCommon.DynamicSPCall_Select(pro_, para, value);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvcourse.DataSource = ds;
                lvcourse.DataBind();
                pnlcommonapply.Visible = true;
                foreach (ListViewDataItem item in lvcourse.Items)
                {
                    DropDownList ddlCourseSlot = item.FindControl("ddlCourseSlot") as DropDownList;
                    HiddenField hdf_slotno = item.FindControl("hdf_slotno") as HiddenField;
                    objCommon.FillDropDownList(ddlCourseSlot, "ACD_EXAM_TT_SLOT", "DISTINCT SLOTNO", "SLOTNAME", "ISNULL(COURSE_SLOT,0)=1", "SLOTNO");
                    if (!string.IsNullOrEmpty(hdf_slotno.Value))
                    {
                        ddlCourseSlot.SelectedValue = hdf_slotno.Value;
                    }
                }
            }
            else
            {
                objCommon.DisplayMessage(updcourseslot, "No Record Found", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_MASTERS_CourseSlotMapping.getCourses() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btncancel_Click(object sender, EventArgs e)
    {
        ddlSession.SelectedIndex = 0;
        ClearDropDown(ddlScheme);
        ClearDropDown(ddlSemester);
        ClearDropDown(ddlSubjecttype);
        lvcourse.DataSource = null;
        lvcourse.DataBind();
        pnlcommonapply.Visible = false;
        txtrowFrom.Text = string.Empty;
        txtrowTo.Text = string.Empty;
        ddlcourseslot.SelectedIndex = 0;
    }

    protected void ClearDropDown(DropDownList ddl)
    {
        ddl.Items.Clear();
        ddl.Items.Add(new ListItem("Please Select", "0"));
    }

    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (CheckCourse() == false)
            {
                objCommon.DisplayMessage(updcourseslot, "Please Select Course", this.Page);
                checkboxcheck();
                return;
            }
            if (CheckCourseSlot() == false)
            {
                objCommon.DisplayMessage(updcourseslot, "Please Select Slot for all Check Courses", this.Page);
                checkboxcheck();
                return;
            }
            int sessionid = Convert.ToInt32(ddlSession.SelectedValue);
            int schemeno = Convert.ToInt32(ddlScheme.SelectedValue);
            int semesterno = Convert.ToInt32(ddlSemester.SelectedValue);
            int subid = Convert.ToInt32(ddlSubjecttype.SelectedValue);
            CustomStatus cs = CustomStatus.Others;
            foreach (ListViewDataItem itm in lvcourse.Items)
            {
                CheckBox chk = itm.FindControl("chkAccept") as CheckBox;
                if (chk.Checked)
                {
                    DropDownList ddlslot = itm.FindControl("ddlCourseSlot") as DropDownList;
                    Label lblccode = itm.FindControl("lblccode") as Label;
                    string ccode = lblccode.Text;
                    cs = (CustomStatus)objExamController.InsertUpdateCourseSlotMapping(sessionid, ccode, Convert.ToInt32(ddlslot.SelectedValue), ViewState["ipadress"].ToString(), Convert.ToInt32(Session["userno"].ToString()));
                }
            }
            if (cs.Equals(CustomStatus.RecordUpdated))
            {
                objCommon.DisplayMessage(updcourseslot, "Record Saved", this.Page);
            }
            else
            {
                objCommon.DisplayMessage(updcourseslot, "Faild To Save ,Try Again", this.Page);
            }
            getCourses();

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_MASTERS_CourseSlotMapping.btnsubmit_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected bool CheckCourse()
    {
        bool stat = true;
        int checkcout = 0;
        foreach (ListViewDataItem itm in lvcourse.Items)
        {
            CheckBox chk = itm.FindControl("chkAccept") as CheckBox;
            if (chk.Checked)
            {
                checkcout++;
                break;
            }
        }
        if (checkcout == 0)
        {
            stat = false;
        }
        return stat;
    }

    protected bool CheckCourseSlot()
    {
        bool stat = true;
        int checkcout = 0;
        int ddlselectedcount = 0;
        foreach (ListViewDataItem itm in lvcourse.Items)
        {
            CheckBox chk = itm.FindControl("chkAccept") as CheckBox;
            if (chk.Checked)
            {
                DropDownList ddlCourseSlot = itm.FindControl("ddlCourseSlot") as DropDownList;
                checkcout++;
                if (ddlCourseSlot.SelectedIndex > 0)
                {
                    ddlselectedcount++;
                }
            }
        }
        if (checkcout != ddlselectedcount)
        {
            stat = false;
        }
        return stat;
    }

    protected void checkboxcheck()
    {
        foreach (ListViewDataItem lst in lvcourse.Items)
        {
            CheckBox chk = lst.FindControl("chkAccept") as CheckBox;
            DropDownList ddlcourseslot = lst.FindControl("ddlCourseSlot") as DropDownList;
            if (chk.Checked)
            {
                ddlcourseslot.Enabled = true;
            }
        }
    }

    protected void btnreport_Click(object sender, EventArgs e)
    {
        string filename = ddlSession.SelectedItem.Text + "_" + "Course Slot Data" + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls";
        DataSet ds = new DataSet();
        string pro_ = "PKG_GET_COURSESLOT_MAPPING_EXCEL_REPORT";
        string para = "@P_SESSIONID,@P_SCHEMENO,@P_SEMESTERNO,@P_SUBID";
        string value = "" + Convert.ToInt32(ddlSession.SelectedValue) + "," + Convert.ToInt32(ddlScheme.SelectedValue) + "," + Convert.ToInt32(ddlSemester.SelectedValue) + "," + Convert.ToInt32(ddlSubjecttype.SelectedValue);
        ds = objCommon.DynamicSPCall_Select(pro_, para, value);
        GridView gv = new GridView();
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            gv.DataSource = ds;
            gv.DataBind();
            string attachment = "attachment ; filename=" + filename;
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            gv.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.Flush();
            Response.End();

        }
        else
        {
            gv.DataSource = null;
            gv.DataBind();
            objCommon.DisplayMessage(updcourseslot, "No Records Found", this.Page);
        }

    }
    protected void btnapply_Click(object sender, EventArgs e)
    {
        if (ddlcourseslot.SelectedIndex > 0)
        {
            int from = 0;
            if (!string.IsNullOrEmpty(txtrowFrom.Text.ToString()))
            {
                from = (Convert.ToInt32(txtrowFrom.Text.ToString()) == 0) ? 1 : (Convert.ToInt32(txtrowFrom.Text.ToString()));
            }
            int to = 0;
            if (!string.IsNullOrEmpty(txtrowTo.Text.ToString()))
            {
                to = (Convert.ToInt32(txtrowTo.Text.ToString()) >= lvcourse.Items.Count) ? lvcourse.Items.Count : Convert.ToInt32(txtrowTo.Text.ToString());
            }
            if (from <= to && to <= lvcourse.Items.Count)
            {
                for (int i = 0; i < lvcourse.Items.Count; i++)
                {
                    CheckBox chk = (CheckBox)lvcourse.Items[i].FindControl("chkAccept");
                    DropDownList ddlCourseSlot = (DropDownList)lvcourse.Items[i].FindControl("ddlCourseSlot");
                    if (i >= from - 1 && i < to)
                    {
                        chk.Checked = true;
                        ddlCourseSlot.SelectedValue = ddlcourseslot.SelectedValue;
                        ddlCourseSlot.Enabled = true;
                    }
                    else
                    {
                        chk.Checked = false;
                        ddlCourseSlot.SelectedIndex = 0;
                        ddlCourseSlot.Enabled = false;
                    }
                }
            }
            else
            {
                objCommon.DisplayMessage(updcourseslot, "Please Define Range Properly", this.Page);
            }
        }
        else
        {
            objCommon.DisplayMessage(updcourseslot, "Please Select CourseSlot to Apply", this.Page);
        }
    }
}