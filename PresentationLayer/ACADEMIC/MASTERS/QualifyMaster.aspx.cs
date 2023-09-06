//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : QUALIFY AND ENTRANCE EXAM MASTER                             
// CREATION DATE : 21-MARCH-2012                                                         
// CREATED BY    : ASHISH DHAKATE   
// ADDED BY      : 
// ADDED DATE    :                                       
// MODIFIED DATE :                                                                      
// MODIFIED DESC :                                                                      
//======================================================================================

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

using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;


public partial class ACADEMIC_MASTERS_QualifyMaster : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();

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
                //objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO>0", "DEGREENO");
                //objCommon.FillDropDownList(ddlQualification, "ACD_QUALILEVEL", "QUALILEVELNO", "QUALILEVELNAME", "QUALILEVELNO>0", "QUALILEVELNO");
            }
            objCommon.FillDropDownList(ddlProgrammeType, "ACD_UA_SECTION", "UA_SECTION", "UA_SECTIONNAME", "UA_SECTION>0", "UA_SECTION");
            BindListView();
            ViewState["action"] = "add";
            //trQualLevel.Visible = false;
            //lblQuaLevel.Visible = false;
            //ddlQualification.Visible = false;
            trQualLevel.Style.Add("display", "none");
        }
        divMsg.InnerHtml = string.Empty;
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=QualifyMaster.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=QualifyMaster.aspx");
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            QualifyController objBC = new QualifyController();
            StudentQualExm objExam = new StudentQualExm();
            //BatchController objBC = new BatchController();
            //StudentQualExm objExam = new StudentQualExm();
            objExam.QualexmId = Convert.ToInt32(ddlStatusType.SelectedValue);
            if (objExam.QualexmId == 1)
            {
                objExam.QexmStatus = "Q";
            }
            else if (objExam.QualexmId == 2)
            {
                objExam.QexmStatus = "E";
            }
            objExam.QualexmName = txtExamName.Text.Trim();
            //objExam.DegreeNo =Convert.ToInt32(ddlDegree.SelectedValue);
            int Qualilevelno = 0;
            //Convert.ToInt32(ddlQualification.SelectedValue);
            objExam.CollegeCode = Session["colcode"].ToString();

            //Check whether to add or update
            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {

                    int Exists = Convert.ToInt32(objCommon.LookUp("ACD_QUALEXM", "COUNT(1)", "QUALIEXMNAME='" + txtExamName.Text.Trim() + "'AND QEXAMID=" + Convert.ToInt32(ddlStatusType.SelectedValue) + ""));
                    if (Exists > 0)
                    {
                        objCommon.DisplayMessage(this.updBatch, "Record Already Exists!", this.Page);
                    }
                    else
                    {
                        //Add Batch
                        CustomStatus cs = (CustomStatus)objBC.AddQualifyExam(objExam, Qualilevelno,Convert.ToInt32(ddlProgrammeType.SelectedValue));
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            ViewState["action"] = "add";
                            Clear();
                            objCommon.DisplayMessage(this.updBatch, "Record Saved Successfully!", this.Page);
                            BindListView();
                        }
                        else
                        {
                            //objCommon.DisplayMessage(this.updBatch, "Existing Record", this.Page);
                            Label1.Text = "Record already exist";
                        }
                    }
                }
                else
                {
                    //Edit
                    if (ViewState["qualifyexamno"] != null)
                    {
                        objExam.QUALIFYNO = Convert.ToInt32(ViewState["qualifyexamno"].ToString());

                        CustomStatus cs = (CustomStatus)objBC.UpdateQualifyExam(objExam, Qualilevelno,Convert.ToInt32(ddlProgrammeType.SelectedValue));
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            ViewState["action"] = null;
                            Clear();
                            objCommon.DisplayMessage(this.updBatch, "Record Updated Successfully!", this.Page);
                            BindListView();
                        }
                        else
                        {
                            //objCommon.DisplayMessage(this.updBatch, "Existing Record", this.Page);
                            Label1.Text = "Record already exist";
                        }
                    }
                }
                BindListView();
            }
        }

        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
        ViewState["action"] = null;
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int qualifyexamno = int.Parse(btnEdit.CommandArgument);

            ShowDetail(qualifyexamno);
            string status = objCommon.LookUp("ACD_QUALEXM", "QEXAMSTATUS", "QUALIFYNO=" + qualifyexamno);
            if (status == "E")
            {
                //trDegree.Visible = true;
                //trQualLevel.Visible = false;
                //lblQuaLevel.Visible = false;
                //ddlQualification.Visible = false;
                //     trQualLevel.Style.Add("display", "none");
            }
            else
            {
                //trQualLevel.Visible = true;
                //lblQuaLevel.Visible = true;
                //ddlQualification.Visible = true;
                //  trQualLevel.Style.Add("display", "block");
                //trDegree.Visible = false;
            }
            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void ShowDetail(int qualifyexamno)
    {


        QualifyController objExam = new QualifyController();
        SqlDataReader dr = objExam.GetQualifyExamNo(qualifyexamno);

        if (dr != null)
        {
            if (dr.Read())
            {
                ViewState["qualifyexamno"] = qualifyexamno.ToString();
                ddlStatusType.Text = dr["QEXAMID"] == null ? string.Empty : dr["QEXAMID"].ToString();
                txtExamName.Text = dr["QUALIEXMNAME"] == null ? string.Empty : dr["QUALIEXMNAME"].ToString();
                //ddlQualification.SelectedValue = dr["QUALILEVELNO"] == null ? string.Empty : dr["QUALILEVELNO"].ToString();
                ddlProgrammeType.SelectedValue = dr["PROGRAMME_TYPE"] == null ? string.Empty : dr["PROGRAMME_TYPE"].ToString();
            }
        }
        if (dr != null) dr.Close();
    }

    private void Clear()
    {
        ddlStatusType.SelectedIndex = -1;
        txtExamName.Text = string.Empty;
        //ddlDegree.SelectedIndex = 0;
        Label1.Text = string.Empty;
        ddlQualification.SelectedIndex = -1;
        //trQualLevel.Visible = false;
        //lblQuaLevel.Visible = false;
        //ddlQualification.Visible = false;
        ddlProgrammeType.SelectedIndex = 0;
        trQualLevel.Style.Add("display", "none");
        //trDegree.Visible = false;
    }

    private void BindListView()
    {
        try
        {
            QualifyController objBC = new QualifyController();
            DataSet ds = objBC.GetAllQualifyExam();
            lvQualifyExamName.DataSource = ds;
            lvQualifyExamName.DataBind();

        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnShowReport_Click(object sender, EventArgs e)
    {
        ShowReport("QualifyExamMaster", "rptQualifyExam.rpt");
    }
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@UserName=" + Session["username"].ToString();
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updBatch, this.updBatch.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void ddlStatusType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlStatusType.SelectedIndex > 0)
        {
            if (ddlStatusType.SelectedValue == "1")
            {
                //trQualLevel.Visible = true;
                //lblQuaLevel.Visible = true;
                //ddlQualification.Visible = true;
                trQualLevel.Style.Add("display", "block");
                //trDegree.Visible = false;
            }
            else
            {
                //trQualLevel.Visible = false;
                //lblQuaLevel.Visible = false;
                //ddlQualification.Visible = false;
                trQualLevel.Style.Add("display", "none");
                //trDegree.Visible = true;
            }
        }
        else
        {
            //trDegree.Visible = false;
            //trQualLevel.Visible = false;
            //lblQuaLevel.Visible = false;
            //ddlQualification.Visible = false;
            trQualLevel.Style.Add("display", "none");
        }
    }
    //protected void ddlProgrammeType_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        lvQualifyExamName.DataSource = null;
    //        lvQualifyExamName.DataBind();
    //        lvQualifyExamName.Visible = false;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw;
    //    }
    //}
}
