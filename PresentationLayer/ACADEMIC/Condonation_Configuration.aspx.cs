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
using IITMS.SQLServer.SQLDAL;
public partial class ACADEMIC_Condonation_Configuration : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    CourseController objCC = new CourseController();
    ConfigController Confi = new ConfigController();
    string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
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
                if (Convert.ToInt32(Session["usertype"]) == 1)
                {
                    CheckPageAuthorization();
                }
                else
                {
                }
                string deptnos = (Session["userdeptno"].ToString() == "" || Session["userdeptno"].ToString() == string.Empty) ? "0" : Session["userdeptno"].ToString();
                if (Session["usertype"].ToString() != "1")
                {
                    objCommon.FillDropDownList(ddlschool, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID)", "COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) + " AND (DB.DEPTNO IN (" + deptnos + ") OR ('" + deptnos + "')='0')", "");
                }
                else
                {
                    objCommon.FillDropDownList(ddlschool, "ACD_COLLEGE_SCHEME_MAPPING", "COSCHNO", "COL_SCHEME_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "COLLEGE_ID");
                }
                string deptnoss = (Session["userdeptno"].ToString() == "" || Session["userdeptno"].ToString() == string.Empty) ? "0" : Session["userdeptno"].ToString();
                if (Session["usertype"].ToString() != "1")
                {
                    objCommon.FillDropDownList(ddlschollOvera, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID)", "COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) + " AND (DB.DEPTNO IN (" + deptnos + ") OR ('" + deptnos + "')='0')", "");
                }
                else
                {
                    objCommon.FillDropDownList(ddlschollOvera, "ACD_COLLEGE_SCHEME_MAPPING", "COSCHNO", "COL_SCHEME_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "COLLEGE_ID");
                }
              
                ViewState["action"] = "add";
                ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
                objCommon.FillDropDownList(ddlsemester, "ACD_SEMESTER WITH (NOLOCK)", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO > 0", "SEMESTERNO");
                objCommon.FillDropDownList(ddlsemoverall, "ACD_SEMESTER WITH (NOLOCK)", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO > 0", "SEMESTERNO");
              
            }
            
        }
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=StudentDocumentList.aspx");
            }
        }
        else
        {
            Response.Redirect("~/notauthorized.aspx?page=StudentDocumentList.aspx");
        }
    }
   
    protected void ddlschool_SelectedIndexChanged(object sender, EventArgs e)
    {
        divCourseDetail.Visible = false;
        lvCourse.DataSource = null;
        lvCourse.DataBind();
        Panel1.Visible = false;
        LvAllDetails.DataSource = null;
        LvAllDetails.DataBind();
        if (ddlschool.SelectedIndex > 0)
        {
            DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlschool.SelectedValue));
          
            if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
            {
                ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();

                objCommon.FillDropDownList(ddlsession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND COLLEGE_ID = " + Convert.ToInt32(ViewState["college_id"]) + " AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "SESSIONNO DESC");
                ddlsession.Focus();
            }
        }
        else
        {
            ddlsession.SelectedIndex = 0;
            ddlsemester.SelectedIndex = 0;
            //ddlsession.Items.Clear();
            //ddlsemester.Items.Clear();
            //ddlsession.Items.Add(new ListItem("Please Select", "0"));
            ddlschool.Focus();
        }
    } 
    private void BindListView()
    {
        try
        {
            divCourseDetail.Visible = false;
            int schemeno = int.Parse(ViewState["schemeno"].ToString());
            DataSet dsfaculty = null;
            dsfaculty = Confi.GetCourseConConf(schemeno, Convert.ToInt32(ddlsession.SelectedValue), ddlsemester.SelectedValue);
            if (dsfaculty != null && dsfaculty.Tables.Count > 0 && dsfaculty.Tables[0].Rows.Count > 0)
            {
                lvCourse.DataSource = dsfaculty;
                lvCourse.DataBind();
                objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvCourse);//Set label - 
                pnlCourse.Visible = true;
                divCourseDetail.Visible = true;
            }
            else
            {
                lvCourse.DataSource = null;
                lvCourse.DataBind();
                objCommon.DisplayMessage(this.UpdCondolance, "Record Not Found!", this.Page);             
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void BindOverAllList()
    {
        string SP_Name2 = "PKG_ACD_GET_CONDOLANCE_CONFIGURATION_DATA_OVERALL";
        string SP_Parameters2 = "@P_OUTPUT";
        string Call_Values2 = "" + Convert.ToInt32(Session["idno"]) + "";
        DataSet dsStudList = objCommon.DynamicSPCall_Select(SP_Name2, SP_Parameters2, Call_Values2);
        if (dsStudList.Tables[0].Rows.Count > 0)
        {
            Panel2.Visible = true;
            LvOverall.DataSource = dsStudList;
            LvOverall.DataBind();
        }
        else
        {
            Panel2.Visible = false;
            LvOverall.DataSource = null;
            LvOverall.DataBind();
        }
    }
    private void BINDlISTVIEW()
    {
        string SP_Name2 = "PKG_ACD_GET_CONDOLANCE_CONFIGURATION_DATA";
        string SP_Parameters2 = "@P_SESSIONNO,@P_SEMESTERNO,@P_COLLEGE_ID";
        string Call_Values2 = "" + Convert.ToInt32(ddlsession.SelectedValue) + "," + Convert.ToInt32(ddlsemester.SelectedValue) + "," + Convert.ToInt32(ViewState["college_id"]) + "";
        DataSet dsStudList = objCommon.DynamicSPCall_Select(SP_Name2, SP_Parameters2, Call_Values2);
        if (dsStudList.Tables[0].Rows.Count > 0)
        {
            Panel1.Visible = true;
            LvAllDetails.DataSource = dsStudList;
            LvAllDetails.DataBind();
        }
        else
        {
            Panel1.Visible = false;
            LvAllDetails.DataSource = null;
            LvAllDetails.DataBind();
        }
    }
    protected void rdoSelect_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdoSelect.SelectedValue == "1")
        {
            cousewiese.Visible=true;
            overall.Visible = false;
            ddlschollOvera.SelectedIndex=0;
            lstSession.ClearSelection();
            ddlsemoverall.SelectedIndex=0;
            txtAmount.Text="";
            txtFrom.Text = "";
            TxtTo.Text = "";
            Panel1.Visible = false;
            LvAllDetails.DataSource = null;
            LvAllDetails.DataBind();
           
        }
        else if (rdoSelect.SelectedValue == "2")
        {
            overall.Visible = true;
            cousewiese.Visible = false;
            divCourseDetail.Visible = false;
            lvCourse.DataSource = null;
            lvCourse.DataBind();
            ddlschool.SelectedIndex=0;
            ddlsession.SelectedIndex=0;
            ddlsemester.SelectedIndex = 0;
            BindOverAllList();
        }
    }

    protected void ddlsemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindListView();
        BINDlISTVIEW();
    }
    protected void chkallowfee_CheckedChanged(object sender, EventArgs e)
    {
        if (chkallowfee.Checked == true)
        {
            chekfess.Visible = true;
        }
        else
        {
            chekfess.Visible = false;
        }
    }
    protected void ddlschollOvera_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlschollOvera.SelectedIndex > 0)
        {
            DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlschollOvera.SelectedValue));

            if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
            {
                ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                ViewState["college_idOVER"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();

                objCommon.FillListBox(lstSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND COLLEGE_ID = " + Convert.ToInt32(ViewState["college_idOVER"]) + " AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "SESSIONNO DESC");
              
            }
        }
        else
        {
            lstSession.Items.Clear();
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string session = "";
            int semesterno = 0, condolance_id = 0, cheked = 0,fees_type=0;
            int collegeid = 0; int count = 0; int colS = 0;
            int ua_no = Convert.ToInt32(Session["userno"]);
            CustomStatus cs = 0; string courseno = string.Empty;
            string coursename = string.Empty; string fees = string.Empty,col_id="";

            condolance_id = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_CONDOLANCE_CONFIGURATION", "isnull(max(CONDOLANCE_NO),0)CONDOLANCE_NO", ""));
            condolance_id = condolance_id + 1;
            if (chkallowfee.Checked == true)
            {
                cheked = 1;
                if (rdoSelect.SelectedValue == "")
                {
                    objCommon.DisplayMessage(this.UpdCondolance, "Please Select Option!", this.Page);
                    return;
                }
                if (rdoSelect.SelectedValue == "1")
                {

                    collegeid = Convert.ToInt32(ViewState["college_id"]);
                    semesterno = Convert.ToInt32(ddlsemester.SelectedValue);
                    session = ddlsession.SelectedValue;
                    colS = Convert.ToInt32(ddlschool.SelectedValue);
                }
                else
                {
                    foreach (ListItem item in lstSession.Items)
                    {
                        if (item.Selected == true)
                        {
                            session += item.Value + ',';
                        }
                    }
                    if (!string.IsNullOrEmpty(session))
                    {
                        session = session.Substring(0, session.Length - 1);
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.UpdCondolance, "Please Select Session!", this.Page);
                        return;
                    }
                    collegeid = Convert.ToInt32(ViewState["college_idOVER"]);
                    semesterno = Convert.ToInt32(ddlsemoverall.SelectedValue);
                    fees = txtAmount.Text;
                    colS = Convert.ToInt32(ddlschollOvera.SelectedValue);
                }
                foreach (ListViewDataItem dataitem in lvCourse.Items)
                {
                    CheckBox chkBox = dataitem.FindControl("chkoffered") as CheckBox;
                    TextBox TxtAmt = dataitem.FindControl("TxtAMount") as TextBox;
                    Label lblcoursename = dataitem.FindControl("lblcoursename") as Label;
                    HiddenField hdfcoursno = dataitem.FindControl("hdfcoursno") as HiddenField;
                    HiddenField hdfcondlanceid = dataitem.FindControl("hdfcondlanceid") as HiddenField;
                    if (TxtAmt.Text != "")
                    {                        
                        count++;
                        courseno += hdfcoursno.Value + ',';
                        coursename += lblcoursename.Text.ToString() + ',';
                        fees += TxtAmt.Text.ToString() + ',';
                        col_id = hdfcondlanceid.Value ;
                    }
                }
                courseno = courseno.TrimEnd(',');
                coursename = coursename.TrimEnd(',');
                fees = fees.TrimEnd(',');

                //if (rdoSelect.SelectedValue == "1")
                //{
                //    if (count == 0)
                //    {
                //        objCommon.DisplayMessage(this.UpdCondolance, "Please Select at Least One Checkbox", this.Page);
                //        return;
                //    }
                //}
            }
            else
            {
                cheked = 0;
                collegeid = 0;
                semesterno = 0;
                session = "";
                courseno= "";
                coursename= "";
                fees = "";
                if (fees == "")
                {
                    fees = "99";
                }
            }
            if (rdoSelect.SelectedValue == "")
            { 
                fees_type=0;
            }
            else if(rdoSelect.SelectedValue == "1")
            {
                fees_type=1;
            }
            else if (rdoSelect.SelectedValue == "2")
            {
                fees_type = 2;
            }
            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    cs = (CustomStatus)Confi.AddCondolanceConfi(condolance_id, Convert.ToString(ddlOperator.SelectedValue), Convert.ToString(ddlopratorto.SelectedValue), Convert.ToInt32(txtFrom.Text), Convert.ToInt32(TxtTo.Text), Convert.ToInt32(cheked), Convert.ToInt32(fees_type), Convert.ToInt32(collegeid), Convert.ToString(session), semesterno, Convert.ToString(fees), Convert.ToInt32(Session["OrgId"]), Convert.ToInt32(ua_no), Convert.ToString(ViewState["ipAddress"]), Convert.ToString(courseno), Convert.ToString(coursename), Convert.ToInt32(colS), Convert.ToInt32(ViewState["condolanceid"]));
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        objCommon.DisplayMessage(this.UpdCondolance, "Record Saved Successfully", this.Page);
                        BINDlISTVIEW();
                        Clear();
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.UpdCondolance, "Error in Saving", this.Page);
                    }
                }
                else if (ViewState["action"].ToString().Equals("edit"))
                {
                    cs = (CustomStatus)Confi.updateCondolanceConfi(Convert.ToInt32(ViewState["condolanceno"]), Convert.ToString(ddlOperator.SelectedValue), Convert.ToString(ddlopratorto.SelectedValue), Convert.ToInt32(txtFrom.Text), Convert.ToInt32(TxtTo.Text), Convert.ToInt32(collegeid), Convert.ToString(session), semesterno, Convert.ToString(fees), Convert.ToInt32(Session["OrgId"]),  Convert.ToInt32(ddlschollOvera.SelectedValue));
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        objCommon.DisplayMessage(this.UpdCondolance, "Record Updated Successfully", this.Page);
                        ViewState["action"] = "add";
                        btnSubmit.Text = "Submit";
                        BINDlISTVIEW();
                        Clear();
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.UpdCondolance, "Error in Saving", this.Page);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Offered_Course.btnAd_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }       
    }
    private void Clear()
    {
        txtFrom.Text = "";
        TxtTo.Text = "";
        ddlschool.SelectedIndex = 0;
        ddlsession.SelectedIndex = 0;
        ddlsemester.SelectedIndex = 0;
        divCourseDetail.Visible = false;
        lvCourse.DataSource = null;
        lvCourse.DataBind();
        ddlschollOvera.SelectedIndex = 0;
        lstSession.ClearSelection();
        ddlsemoverall.SelectedIndex = 0;
        txtAmount.Text = "";
        chkallowfee.Checked = false;
        chekfess.Visible = false;
        rdoSelect.ClearSelection();
        cousewiese.Visible = false;
        overall.Visible = false;
        btnSubmit.Text = "Submit";
        ViewState["action"] = "add";
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            ViewState["action"] = "edit";
            int condolanceno = int.Parse(btnEdit.CommandArgument);
            ViewState["condolanceno"] = condolanceno;
            btnSubmit.Text = "Update";
            ShowDetailsCon(condolanceno);
        }
        catch (Exception ex)
        {
        }
    }
    private void ShowDetailsCon(int condolanceno)
    {

        try
        {
            lstSession.ClearSelection();
            DataSet ds = objCommon.FillDropDown("ACD_STUDENT_CONDOLANCE_CONFIGURATION", "*", "", "CONDOLANCE_ID=" + condolanceno, "CONDOLANCE_ID");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlschollOvera.SelectedValue = ds.Tables[0].Rows[0]["COSCHNO"].ToString();
                ddlsemoverall.SelectedValue = ds.Tables[0].Rows[0]["SEMESTERNO"].ToString();
                txtAmount.Text = ds.Tables[0].Rows[0]["FEES_DEFINE"].ToString();
                txtFrom.Text = ds.Tables[0].Rows[0]["RANGE_FROM"].ToString();
                TxtTo.Text = ds.Tables[0].Rows[0]["RANGE_TO"].ToString();
                ddlOperator.SelectedValue = ds.Tables[0].Rows[0]["OPERATOR_FROM"].ToString();
                ddlopratorto.SelectedValue = ds.Tables[0].Rows[0]["OPERATOR_TO"].ToString();
                string program = (ds.Tables[0].Rows[0]["SESSIONNO"].ToString());
                ddlschollOvera_SelectedIndexChanged(new object(), new EventArgs());  
                string[] pgm = program.Split(',');
                for (int j = 0; j < pgm.Length; j++)
                {
                    for (int i = 0; i < lstSession.Items.Count; i++)
                    {
                        if (pgm[j] == lstSession.Items[i].Value)
                        {
                            lstSession.Items[i].Selected = true;
                        }
                    }
                }  
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_CourseCreation.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnEditCou_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            ViewState["action"] = "add";
            int condolanceno = int.Parse(btnEdit.CommandArgument);
            ViewState["condolanceid"] = condolanceno;
            ShowDetailsCourseWise(condolanceno);
        }
        catch (Exception ex)
        {
        }
    }
    private void ShowDetailsCourseWise(int condolanceno)
    {

        try
        {
            lstSession.ClearSelection();
            DataSet ds = objCommon.FillDropDown("ACD_STUDENT_CONDOLANCE_CONFIGURATION", "*", "", "CONDOLANCE_ID=" + condolanceno, "CONDOLANCE_ID");
            if (ds.Tables[0].Rows.Count > 0)
            {
               
                txtFrom.Text = ds.Tables[0].Rows[0]["RANGE_FROM"].ToString();
                TxtTo.Text = ds.Tables[0].Rows[0]["RANGE_TO"].ToString();
                ddlOperator.SelectedValue = ds.Tables[0].Rows[0]["OPERATOR_FROM"].ToString();
                ddlopratorto.SelectedValue = ds.Tables[0].Rows[0]["OPERATOR_TO"].ToString();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_CourseCreation.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
}