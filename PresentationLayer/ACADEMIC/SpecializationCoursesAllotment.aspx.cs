using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;


using IITMS;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;


public partial class ACADEMIC_SpecializationCoursesAllotment : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentController objSC = new StudentController();

    string PageId;
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
            if (Session["userno"] == null && Session["username"] == null &&
                Session["usertype"] == null && Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                //CheckPageAuthorization();
                if (Session["usertype"].Equals(1))
                {
                }
                else
                {
                    Response.Redirect("~/notauthorized.aspx?page=AffiliatedFeesCategory.aspx");
                }

            }
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            PopulateDropDown();
           // GroupDropDown();
            //BindValueAddedEntries();

        }
    }

    protected void PopulateDropDown()
    {
        try
        {
          
          
            objCommon.FillDropDownList(ddlCollegeScheme, "ACD_VALUEADDED_COURSE AC INNER JOIN ACD_COLLEGE_SCHEME_MAPPING SM ON (AC.COSCHNO=SM.COSCHNO) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID) INNER JOIN ACD_SCHEME SC ON(SC.SCHEMENO=SM.SCHEMENO)", "DISTINCT AC.COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND AC.COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"])+" ", "AC.COSCHNO");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DefineIntake.PopulateDropDown()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    
    protected void ddlCollegeScheme_SelectedIndexChanged(object sender, EventArgs e)
    {


        if (ddlCollegeScheme.SelectedIndex > 0)
        {

            DataSet dt = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlCollegeScheme.SelectedValue));
            
            if (dt.Tables[0].Rows.Count > 0 && dt.Tables[0] != null)
            {
                ViewState["degreeno"] = Convert.ToInt32(dt.Tables[0].Rows[0]["DEGREENO"]).ToString();
                ViewState["branchno"] = Convert.ToInt32(dt.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                ViewState["college_id"] = Convert.ToInt32(dt.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                ViewState["schemeno"] = Convert.ToInt32(dt.Tables[0].Rows[0]["SCHEMENO"]).ToString();
            }

            objCommon.FillDropDownList(ddlsemester, "ACD_STUDENT S WITH (NOLOCK) INNER JOIN ACD_SEMESTER SM WITH (NOLOCK) ON(S.SEMESTERNO=SM.SEMESTERNO)", "DISTINCT SM.SEMESTERNO", "SM.SEMESTERNAME ", "S.COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]), "SM.SEMESTERNAME asc");
            DataSet ds = objSC.GetViewGroupDatabycoschno(Convert.ToInt32(ddlCollegeScheme.SelectedValue));
            // if (ds.Tables[0].Rows.Count > 0)

            ddlgroup.Items.Clear();
            //ddlgroup.Items.Add(new ListItem("", "0"));

            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlgroup.DataSource = ds;
                ddlgroup.DataValueField = ds.Tables[0].Columns[1].ToString();
                ddlgroup.DataTextField = ds.Tables[0].Columns[0].ToString();
                ddlgroup.DataBind();


            }
        }
          
    }

    private void BindListView()
    {     
          
            DataSet ds = objSC.Getshowstudentlist(Convert.ToInt32(ViewState["college_id"]));
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {

                lvCourseallotment.DataSource = ds;
                lvCourseallotment.DataBind();

                
                foreach (ListViewDataItem item in lvCourseallotment.Items)
                {

                    //CheckBox status = item.FindControl("chkapprove") as CheckBox;
                    Label lblstatus = item.FindControl("lbstatus") as Label;

                    string approve = lblstatus.ToolTip;

                    if (approve == "1")
                    {
                        lblstatus.Text = "Alloted";
                        //status.Checked = true;
                        //status.Enabled = false;

                    }
                    if (approve == "0")
                    {
                        lblstatus.Text = "NA";
                        //status.Checked = false;

                    }

                }

            }
            else
            {
                lvCourseallotment.DataSource = null;
                lvCourseallotment.DataBind();
                objCommon.DisplayMessage(this.Page, "No Record Found for current selection", this);

            }

        }
    

    protected void btnShow_Click(object sender, EventArgs e)
    {
        BindListView();
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int count = 0;
            int id = 0;
            string StudId = string.Empty;           
            string group = string.Empty;            
            string regno = string.Empty;

            if (!CheckGroupSelected())
            {
                objCommon.DisplayUserMessage(this.Page, "Please Select Group!", this.Page);
                return;
            }

            //if (ddlgroup.SelectedIndex <= 0)
            //{
            //    objCommon.DisplayMessage(this.Page, "Please Select Group.", this.Page);
            //    return;
            //}

            StudentController objSC = new StudentController();


            int collegeid = Convert.ToInt32(ddlCollegeScheme.SelectedValue);
           
            int semesterno = Convert.ToInt32(ddlsemester.SelectedValue);

           // string CollegeIds = GetSelectedCollegeIds();

            foreach (ListItem items in ddlgroup.Items)
            {
                if (items.Selected == true)
                {

                    group += items.Value + ',';
                }
            }
            if (!group.Equals(string.Empty))
            {
                group = group.Substring(0, group.Length - 1);
            }

            foreach (ListViewDataItem lvItem in lvCourseallotment.Items)
            {
                CheckBox chkBox = lvItem.FindControl("cbRow") as CheckBox;
                
                Label lblregno = lvItem.FindControl("lbregno") as Label;
                regno = lblregno.Text;
                id = Convert.ToInt32(chkBox.ToolTip.ToString());
                if (chkBox.Checked == true)
                {
                    
                    CustomStatus cs = (CustomStatus)objSC.Add_Course_allotment(id, regno, collegeid, group, semesterno);

                    if (cs.Equals(CustomStatus.RecordSaved))
                    {

                        count++;
                    }
                }
            }
            if (count < 1)
            {
                objCommon.DisplayMessage(this.Page, "Please Select Atleast One Student..!!", this.Page);
            }
            else if (count > 0)
            {
                objCommon.DisplayMessage(this.Page, "Group Alloted Sucessfully..", this.Page);
                this.BindListView();
                return;
            }

            //this.BindListView();
        }
        catch
        {
            throw;
        }

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());

    }
    protected void btnReport_Click(object sender, EventArgs e)
    {

        try
        {
            
            GridView gv = new GridView();
            int COLLEGEID = Convert.ToInt32(ddlCollegeScheme.SelectedValue);

            DataSet ds = objSC.GetGroupAllotmentReport(COLLEGEID);
            if (ds.Tables[0].Rows.Count > 0)
            {
                gv.DataSource = ds;
                gv.DataBind();

                string Attachment = "Attachment; filename=" + "GroupAllotmentReport.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", Attachment);
                Response.ContentType = "application/" + "ms-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                gv.HeaderStyle.Font.Bold = true;
                gv.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "No Data Found for current selection.", this.Page);
            }
        }
           
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_RefundReport.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private Boolean CheckGroupSelected()
    {
        Boolean IsSessionSelected = false;

        string Groupno = string.Empty;
        
        foreach (ListItem items in ddlgroup.Items)
        {
            if (items.Selected == true)
            {
                Groupno += items.Value + ',';
               
            }
        }
        if (Groupno.Length < 1)
        {
            IsSessionSelected = false;
        }
        else if (Groupno.Length > 0)
        {
            IsSessionSelected = true;
        }
        return IsSessionSelected;
    }
    //private string GetSelectedCollegeIds()
    //{
    //    string CollegeIds = string.Empty;
    //    foreach (ListItem items in ddlgroup.Items)
    //    {
    //        if (items.Selected == true)
    //        {
    //            CollegeIds += items.Value + ',';
    //            //CollegeIds += (items.Value).Split('-')[0] + ','; // Add by maithili [08-09-2022]
    //        }
    //    }
    //    if (CollegeIds.Length > 1)
    //    {
    //        CollegeIds = CollegeIds.Remove(CollegeIds.Length - 1);
    //    }
    //    return CollegeIds;
    //}

}

