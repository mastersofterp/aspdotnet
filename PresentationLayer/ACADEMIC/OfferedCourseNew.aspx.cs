using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ACADEMIC_OfferedCourseNew : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    CourseController objCourse = new CourseController();
    Course objc = new Course();
    //ConnectionStrings
    string _uaims_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

    #region Page Events
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
        try
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

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                    }
                    //this.CheckPageAuthorization();

                    FilldropDown();
                    string host = Dns.GetHostName();
                    IPHostEntry ip = Dns.GetHostEntry(host);
                    string IPADDRESS = string.Empty;
                    IPADDRESS = ip.AddressList[0].ToString();
                    ViewState["ipAddress"] = IPADDRESS;


                }
               
                objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"])); //for header

                Page.Form.Attributes.Add("enctype", "multipart/form-data");
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACDEMIC_COLLEGE_MASTER.Page_Load -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    #endregion

    private void FilldropDown()
    {
        objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "distinct SESSIONNO", "SESSION_PNAME", "ISNULL(IS_ACTIVE,0)=1", "SESSIONNO DESC");
        objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "ISNULL(ACTIVESTATUS,0)=1  AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "COLLEGE_NAME");
        objCommon.FillDropDownList(ddlDept, "ACD_DEPARTMENT", "DEPTNO", "DEPTNAME", "", "DEPTNAME");

    }


    protected void btnSave_Click(object sender, EventArgs e)
    {

        try
        {
            CourseController objCC = new CourseController();

            int SchemeNo = Convert.ToInt32(ddlScheme.SelectedValue);
            int collegeid = Convert.ToInt32(ddlCollege.SelectedValue);
            int ua_no = Convert.ToInt32(Session["userno"]);
            CustomStatus cs = 0;
            foreach (ListViewDataItem dataitem in lvCourse.Items)
            {
                CheckBox chkBox = dataitem.FindControl("chkoffered") as CheckBox;
                if (chkBox.Checked == false)
                    continue;

                string offcourse = string.Empty;
                string sem = string.Empty;
                string elect = string.Empty;
                string intern = string.Empty;
                string externn = string.Empty;
                string capcity = string.Empty;
                string group = string.Empty;
                string TOTAL = string.Empty;
                string capacity = string.Empty;
                string ca = string.Empty;
                string final = string.Empty;
                string overall = string.Empty;
                string modulelic = string.Empty;
                decimal credits = 0.0m;


                ListBox ddlsem = dataitem.FindControl("ddlsem") as ListBox;
                DropDownList ddlgroup = dataitem.FindControl("ddlcoregroup") as DropDownList;
                DropDownList ddlelect = dataitem.FindControl("ddlcore") as DropDownList;
                TextBox txtinternn = dataitem.FindControl("txtIntern") as TextBox;
                TextBox txtextern = dataitem.FindControl("txtExtern") as TextBox;
                TextBox txtcapacity = dataitem.FindControl("txtcapacity") as TextBox;
                DropDownList ddlmodulelic = dataitem.FindControl("ddlmodulelic") as DropDownList;
                TextBox txtca = dataitem.FindControl("txtca") as TextBox;
                TextBox txtfinal = dataitem.FindControl("txtfinal") as TextBox;
                TextBox txtoverall = dataitem.FindControl("txtoverall") as TextBox;
                TextBox txtcredits = dataitem.FindControl("txtcredits") as TextBox;
                //if (chkBox.Checked == true)
                //{
                if (txtinternn.Text == "" && txtinternn.Text == "")
                {
                    objCommon.DisplayMessage(this.updpnl, "Please enter details", this.Page);
                    goto noresult;
                }
                string semno = "";

                foreach (ListItem item in ddlsem.Items)
                {
                    if (item.Selected == true)
                    {
                        semno += item.Value + ',';
                    }

                }

                if (!string.IsNullOrEmpty(semno))
                {
                    semno = semno.Substring(0, semno.Length - 1);
                }
                else
                {
                    semno = "0";
                }

                offcourse = chkBox.ToolTip;
                sem = semno;
                elect = ddlelect.SelectedValue;
                group = ddlgroup.SelectedValue;
                intern = txtinternn.Text;
                externn = txtextern.Text;
                externn = txtextern.Text;
                externn = txtextern.Text;
                capacity = txtcapacity.Text;
                ca = txtca.Text;
                final = txtfinal.Text;
                overall = txtoverall.Text;
                modulelic = Convert.ToString(ddlmodulelic.SelectedValue);
                credits = Convert.ToDecimal(txtcredits.Text);
                TOTAL = Convert.ToString(Convert.ToInt32(txtinternn.Text) + Convert.ToInt32(txtextern.Text));

                //cs = (CustomStatus)objCC.InsertOfferedCourse(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), capacity, credits, sem, elect, group, Convert.ToInt32(ddlCollege.SelectedValue), offcourse, intern, externn, TOTAL, ua_no, ViewState["ipAddress"].ToString(), ca, final, overall, modulelic);
                // }

            }

            if (cs.Equals(CustomStatus.RecordSaved))
            {
                BindListview();

                objCommon.DisplayMessage(this.updpnl, "Module offered saved successfully.", this.Page);
            }
            else
            {
                objCommon.DisplayMessage(this.updpnl, "Error in Saving", this.Page);
            }

        noresult:
            { }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Offered_Course.btnAd_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clear();
    }

    private void clear()
    {
        ddlSession.SelectedIndex = 0;
        ddlCollege.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        ddlScheme.SelectedIndex = 0;
        lvCourse.DataSource = null;
        lvCourse.DataBind();
        Response.Redirect(Request.Url.ToString());
    }

    private void BindListview()
    {
        DataSet dsfaculty = null;
        DataSet dsCE = null;
        DataSet dsSem = null;
        DataSet dsUser = null;
        DataSet dsElectG = null;

        try
        {
            string SEMNO = string.Empty;
            int schemeno = int.Parse(ddlScheme.SelectedValue);
            int sessionno = int.Parse(ddlSession.SelectedValue);
            int college_id = int.Parse(ddlCollege.SelectedValue);
            int degree = int.Parse(ddlDegree.SelectedValue);
            int dept = int.Parse(ddlDept.SelectedValue);

            //dsfaculty = objCourse.GetCourseOfferedCreation(schemeno, sessionno, degree, college_id, dept);
            //if (dsfaculty != null && dsfaculty.Tables.Count > 0 && dsfaculty.Tables[0].Rows.Count > 0)
            //{
            //    lvCourse.DataSource = dsfaculty;
            //    lvCourse.DataBind();
            //    btnSave.Visible = true;
            //    btnCancel.Visible = true;
            //}
            //else
            //{
            //    lvCourse.DataSource = null;
            //    lvCourse.DataBind();
            //    btnSave.Visible = false;
            //    btnCancel.Visible = false;
            //    objCommon.DisplayMessage(this.updpnl, "Record Not Found!", this.Page);
            //}

            dsCE = objCommon.FillDropDown("ACD_CORE_ELECTIVE", "GROUPNO", "ELECTIVENAME", "GROUPNO>0", "GROUPNO");
            dsSem = objCommon.FillDropDown("ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0", "SEMESTERNO");
            dsUser = objCommon.FillDropDown("USER_ACC", "UA_NO", "UA_FULLNAME", "UA_TYPE=3", "UA_FULLNAME");
            dsElectG = objCommon.FillDropDown("ACD_ELECTGROUP", "GROUPNO", "GROUPNAME", "GROUPNO > 0", "GROUPNO");

            foreach (ListViewDataItem dataitem in lvCourse.Items)
            {
                DropDownList ddlcore = dataitem.FindControl("ddlcore") as DropDownList;
                ListBox ddlsem = dataitem.FindControl("ddlsem") as ListBox;
                DropDownList ddlcoregroup = dataitem.FindControl("ddlcoregroup") as DropDownList;
                DropDownList ddlmodulelic = dataitem.FindControl("ddlmodulelic") as DropDownList;
                CheckBox ChkOffer = dataitem.FindControl("chkoffered") as CheckBox;
                TextBox txtintern = dataitem.FindControl("txtIntern") as TextBox;
                TextBox txtextern = dataitem.FindControl("txtExtern") as TextBox;
                Label lblSem = dataitem.FindControl("LblSemNo") as Label;
                Label LBLCORE = dataitem.FindControl("lblcore") as Label;
                Label LBLELECTIVE = dataitem.FindControl("lblelective") as Label;
                Label lbllic = dataitem.FindControl("lbllic") as Label;
                BindDropDown(ddlcore, dsCE);
                BindListBox(ddlsem, dsSem);
                BindDropDown(ddlmodulelic, dsUser);

                BindDropDown(ddlcoregroup, dsElectG);
                
                ddlcore.SelectedValue = LBLCORE.Text;
                ddlcoregroup.SelectedValue = LBLELECTIVE.Text;
                (ddlmodulelic.SelectedValue) = lbllic.Text;
                ChkOffer.Checked = lblSem.ToolTip.Equals("1") ? true : false;
                ddlsem.SelectedValue = lblSem.Text;
            }
            string courseNo = "";
            foreach (ListViewDataItem lvitem in lvCourse.Items)
            {
                CheckBox chkBox = lvitem.FindControl("chkoffered") as CheckBox;
                ListBox ddlsem = lvitem.FindControl("ddlsem") as ListBox;
                if (dsfaculty.Tables[0].Rows.Count > 0 && dsfaculty.Tables[0] != null)
                {
                    for (int k = 0; k < dsfaculty.Tables[0].Rows.Count; k++)
                    {
                        courseNo = dsfaculty.Tables[0].Rows[k]["COURSENO"].ToString();
                        SEMNO = dsfaculty.Tables[0].Rows[k]["SEMESTERNO"].ToString();
                        string[] SEM = SEMNO.Split(',');
                        foreach (ListItem item in ddlsem.Items)
                        {
                            if (Convert.ToString(chkBox.ToolTip) == courseNo.ToString())
                            {
                                for (int j = 0; j < SEM.Length; j++)
                                {
                                    if (SEM[j].ToString() == item.Value)
                                    {
                                        item.Selected = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            #region Dispose Dataset Object

            dsfaculty = null;
           // dsCE = null;
            dsSem = null;
            dsUser = null;
            dsElectG = null;

            #endregion Dispose Dataset Object
        }

        catch (Exception ex)
        {
            #region Dispose Dataset Object

            dsfaculty = null;
            //dsCE = null;
            dsSem = null;
            dsUser = null;
            dsElectG = null;

            #endregion Dispose Dataset Object

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Offered_Course.BindListView -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlScheme_SelectedIndexChanged1(object sender, EventArgs e)
    {
        BindListview();
    }

    protected void ddlcore_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl = sender as DropDownList;
        string Value = ddl.ToolTip.ToString();
        foreach (ListViewDataItem dataitem in lvCourse.Items)
        {
            DropDownList ddlcore = dataitem.FindControl("ddlcore") as DropDownList;

            DropDownList ddlcoregroup = dataitem.FindControl("ddlcoregroup") as DropDownList;
            HiddenField hdfValue = (HiddenField)dataitem.FindControl("hdfValue") as HiddenField;
            HiddenField hdfValue1 = (HiddenField)dataitem.FindControl("hdfValue1") as HiddenField;

            if (Value == hdfValue1.Value && ddlcore.SelectedItem.Text == "ELECTIVE")
            {
                ddlcoregroup.Enabled = true;
            }
            else
            {
                ddlcoregroup.Enabled = false;
            }
        }
    }

    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH C ON D.DEGREENO=C.DEGREENO", "DISTINCT D.DEGREENO", "D.DEGREENAME", "ISNULL(C.ACTIVESTATUS,0)=1 AND COLLEGE_ID=" + ddlCollege.SelectedValue, "D.DEGREENAME");
    }

    protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindListview();
    }

    private void BindDropDown(DropDownList ddlList, DataSet ds)
    {
        ddlList.Items.Clear();
        ddlList.Items.Add("Please Select");
        ddlList.SelectedItem.Value = "0";

        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlList.DataSource = ds;
            ddlList.DataValueField = ds.Tables[0].Columns[0].ToString();
            ddlList.DataTextField = ds.Tables[0].Columns[1].ToString();
            ddlList.DataBind();
            ddlList.SelectedIndex = 0;
        }
        ds = null;
    }

    private void BindListBox(ListBox lstbox, DataSet ds)
    {
        lstbox.Items.Clear();

        if (ds.Tables[0].Rows.Count > 0)
        {
            lstbox.DataSource = ds;
            lstbox.DataValueField = ds.Tables[0].Columns[0].ToString();
            lstbox.DataTextField = ds.Tables[0].Columns[1].ToString();
            lstbox.DataBind();
        }

        ds = null;
    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME S INNER JOIN ACD_DEGREE D ON (S.DEGREENO=D.DEGREENO) ", "S.SCHEMENO", "S.SCHEMENAME", "S.SCHEMENO>0 AND S.DEGREENO='" + ddlDegree.SelectedValue + "'", "S.SCHEMENAME");
    }
}