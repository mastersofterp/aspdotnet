
//======================================================================================
// PROJECT NAME  : RFC CODE                                                                
// MODULE NAME   : ACADEMIC                                                             
// PAGE NAME     : Maintain Faculty Diary                                         
// CREATION DATE : 29-01-2024                                                         
// CREATED BY    : SAKSHI MAKWANA                                 
// MODIFIED DATE :                                        
// MODIFIED DESC :                                         
//======================================================================================

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ACADEMIC_Maintain_FacultyDiary : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    StudentController objSC = new StudentController();

    #region PageLoad

    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=studentinfoentry.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=studentinfoentry.aspx");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {  
            //Check Session
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                if (Convert.ToInt32(Session["usertype"]) == 3)
                {
                    int Ua_No;
                    Ua_No = Convert.ToInt32(Session["userno"]);
                }
                else
                {
                    objCommon.DisplayMessage(this, "you are not authorized to view this page.!!", this.Page);
                    div11.Visible = false;
                    return;
                }
                //Page Authorization
                //this.CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
            }
            objCommon.FillDropDownList(ddlcollege, "ACD_COLLEGE_MASTER ", "COLLEGE_ID", "COLLEGE_NAME", "", "COLLEGE_ID");

        }
    }

    #endregion PageLoad

    #region  Fill DropDown
    protected void ddlAcdYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlDegree, "ACD_COLLEGE_DEGREE_BRANCH CDB INNER JOIN ACD_DEGREE D ON(CDB.DEGREENO = D.DEGREENO) ", "DISTINCT D.degreeno ", "D.DEGREENAME", "CDB.COLLEGE_ID="+Convert.ToInt32(ddlcollege.SelectedValue), "");
    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlBranch, "ACD_COLLEGE_DEGREE_BRANCH CDB INNER JOIN ACD_BRANCH D ON(CDB.BRANCHNO = D.BRANCHNO) ", "DISTINCT D.BRANCHNO ", "D.LONGNAME", "CDB.COLLEGE_ID=" + Convert.ToInt32(ddlcollege.SelectedValue) + " AND CDB.DEGREENO="+Convert.ToInt32(ddlDegree.SelectedValue)+" ", "");
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlYear, "ACD_YEAR", "YEAR ", "YEARNAME", "YEAR>0 AND ACTIVESTATUS=1", "");
    }

    protected void ddlcollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlAcdYear, "ACD_ACADEMIC_YEAR", "ACADEMIC_YEAR_ID", "ACADEMIC_YEAR_NAME", "ACADEMIC_YEAR_ID>0 AND ACTIVE_STATUS=1", "ACADEMIC_YEAR_ID DESC");
    }

    #endregion  Fill DropDown

    protected void btnShow_Click(object sender, EventArgs e)
    {
        Bind();
    }


    private void Bind()
    {
        try
        {
            if (ddlcollege.SelectedValue == "0")
            {
                objCommon.DisplayMessage(updp, "Please Select College/Institute", this.Page);
                return;
            }
            if (ddlAcdYear.SelectedValue == "0")
            {
                objCommon.DisplayMessage(updp, "Please Select Academic Year", this.Page);
                return;
            }
            if (ddlDegree.SelectedValue == "0")
            {
                objCommon.DisplayMessage(updp, "Please Select Degree", this.Page);
                return;
            }
            if (ddlBranch.SelectedValue == "0")
            {
                objCommon.DisplayMessage(updp, "Please Select Branch", this.Page);
                return;
            }
            if (ddlYear.SelectedValue == "0")
            {
                objCommon.DisplayMessage(updp, "Please Select Year", this.Page);
                return;
            }

            string count = objCommon.LookUp("acd_student s inner join ACD_FACDIARY_VISIT_TRAINING t on(s.idno=t.STUD_IDNO)", "count(*)", " s.ACADEMIC_YEAR_ID=" + Convert.ToInt32(ddlAcdYear.SelectedValue) + " and s.degreeno=" + Convert.ToInt32(ddlDegree.SelectedValue) + " and s.branchno=" + Convert.ToInt32(ddlBranch.SelectedValue) + "  and s.year=" + Convert.ToInt32(ddlYear.SelectedValue) + " and s.college_id=" + Convert.ToInt32(ddlcollege.SelectedValue) + " ");
            pnlSession.Visible = true;
            DataSet ds = objSC.getStudentDeatilsForFacultyDiary(Convert.ToInt32(ddlAcdYear.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlcollege.SelectedValue));

            if (ds.Tables[0].Rows.Count > 0)
            {
                lvphdweightage.DataSource = ds.Tables[0];
                lvphdweightage.DataBind();
                div5.Visible = true;
                divAddDetails.Visible = true;
                div1.Visible = true;
                btnclear1.Visible = true;
                btnSubmitWeight.Visible = true;
                btnReport.Visible = true;
            }
            else
            {
                objCommon.DisplayMessage(updp, "No Data Found", this.Page);
                return;
            }
            if (count == string.Empty || count == "")
            {
                txtLot.Text = "0";
            }
            else
            {
                txtLot.Text = count;
                //for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                //{
                //    string value = ds.Tables[1].Rows[i]["STUD_IDNO"].ToString();
                //    foreach (ListViewDataItem dataitem in lvphdweightage.Items)
                //    {
                //        CheckBox cbRow = dataitem.FindControl("chkSelect") as CheckBox;
                //        string idno = cbRow.ToolTip;
                //        //if (idno == value)
                //        //{
                //        //    cbRow.Checked = true;
                //        //}
                //    }
                //}
                //txtStartDate.Text = ds.Tables[1].Rows[0]["TRAVELLING_DATE"].ToString();
                //txtTravel.Text = ds.Tables[1].Rows[0]["TRAVELLING_DETAIL"].ToString();
                //txtCollegeAdd.Text = ds.Tables[1].Rows[0]["INSTITUDE_COLLEGE_NAME"].ToString();
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void btnSubmitWeight_Click(object sender, EventArgs e)
    {
        int count = 0;
        foreach (ListViewDataItem dataitem in lvphdweightage.Items)
        {
            CheckBox cbRow = dataitem.FindControl("chkSelect") as CheckBox;
            if (cbRow.Checked == true)
            {
                count++;
            }
        }
        if (count == 0)
        {
            objCommon.DisplayMessage(updp, "Please Select atleast One Student!!", this.Page);
            return;
        }
        else
        {
            foreach (ListViewDataItem item in lvphdweightage.Items)
            {
                try
                {
                    CheckBox chek = item.FindControl("chkSelect") as CheckBox;
                    HiddenField lblIdno = item.FindControl("hdnidno") as HiddenField;
                    Label lbldegree = item.FindControl("lbldegree") as Label;
                    Label lblbranch = item.FindControl("lblbranch") as Label;
                    Label lblname = item.FindControl("lblname") as Label;
                    if (chek.Checked)
                    {
                        int result = objSC.addfacultydiary_visit_detail(Convert.ToInt32(ddlcollege.SelectedValue), Convert.ToInt32(ddlAcdYear.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlYear.SelectedValue), lblname.Text, Convert.ToInt32(lblIdno.Value.ToString()), Convert.ToInt32(Session["userno"].ToString()), Session["userfullname"].ToString(), Convert.ToDateTime(txtStartDate.Text), txtTravel.Text, txtCollegeAdd.Text, Convert.ToInt32(Session["OrgId"]));
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            objCommon.DisplayMessage(updp, "Record Inserted Successfully", this.Page);
            Bind();
        }
    }

    protected void btnCancelweight_Click(object sender, EventArgs e)
    {
        ddlcollege.ClearSelection();
        ddlAcdYear.ClearSelection();
        ddlDegree.ClearSelection();
        ddlBranch.ClearSelection();
        ddlYear.ClearSelection();
        txtCollegeAdd.Text = string.Empty;
        txtStartDate.Text = string.Empty;
        txtTravel.Text = string.Empty;
        lvphdweightage.DataSource = null;
        lvphdweightage.DataBind();
        pnlSession.Visible = false;
        txtLot.Text = "0";
        div5.Visible = false;
        divAddDetails.Visible = false;
        div1.Visible = false;
        btnclear1.Visible = false;
        btnSubmitWeight.Visible = false;
        btnReport.Visible = false;
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        if (ddlcollege.SelectedValue == "0")
        {
            objCommon.DisplayMessage(updp, "Please Select College/Institute", this.Page);
            return;
        }
        if (ddlAcdYear.SelectedValue == "0")
        {
            objCommon.DisplayMessage(updp, "Please Select Academic Year", this.Page);
            return;
        }
        if (ddlDegree.SelectedValue == "0")
        {
            objCommon.DisplayMessage(updp, "Please Select Degree", this.Page);
            return;
        }
        if (ddlBranch.SelectedValue == "0")
        {
            objCommon.DisplayMessage(updp, "Please Select Branch", this.Page);
            return;
        }
        if (ddlYear.SelectedValue == "0")
        {
            objCommon.DisplayMessage(updp, "Please Select Year", this.Page);
            return;
        }
        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
        url += "Reports/CommonReport.aspx?";
        url += "pagetitle=" + "Record of Training/Visit";
        url += "&path=~,Reports,Academic," + "rptFacultyDiaryForVisitReportrpt.rpt";
        url += "&param=@P_ACD_YEAR=" + Convert.ToInt32(ddlAcdYear.SelectedValue) + ",@P_DEGREE=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_BRANCH=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_YEAR=" + Convert.ToInt32(ddlYear.SelectedValue) + ",@P_COLLEGE_ID=" + Convert.ToInt32(ddlcollege.SelectedValue) + "";
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
        sb.Append(@"window.open('" + url + "','','" + features + "');");
        ScriptManager.RegisterClientScriptBlock(this.updp, this.updp.GetType(), "controlJSScript", sb.ToString(), true);
    }

}