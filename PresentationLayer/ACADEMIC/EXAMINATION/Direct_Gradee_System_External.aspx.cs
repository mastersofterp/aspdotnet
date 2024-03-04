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
using System.IO;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;

public partial class ACADEMIC_EXAMINATION_Direct_Gradee_System : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();

    ExamController objexam = new ExamController();
    decimal max1;
    decimal min1;
    string indicator1;
    decimal max2;
    decimal min2;
    int gradepot;
    string schemeno = string.Empty;
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

                Page.Title = Session["coll_name"].ToString();

            }
            //get College ID
            ViewState["College_ID"] = objCommon.LookUp("User_Acc", "UA_COLLEGE_NOS", "UA_NO=" + Convert.ToInt32(Session["userno"].ToString()));
            //get College and schema
            this.PopulateDropDown();
            ////GetID();
            ViewState["edit"] = null;
            lvCGPA.Visible = false;
            lvGrade.Visible = false;
            //Session["action"] = null;
            



        }
        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Src", "test5();", true);
    }
    private void GetDetails()
    {
        try
        {
            ViewState["SchemeNo"] = string.Empty;
            DataSet ds = null;
            string cheme = string.Empty;
            foreach (ListItem items in lstcollege.Items)
            {
               
                if (items.Selected == true)
                {
                    cheme += items.Value + ",";
                    //cheme = items.Value;
                }
            }
                //= objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(lstcollege.SelectedValue));
            cheme = cheme.TrimEnd(',');
            ds = objCommon.FillDropDown("ACD_COLLEGE_SCHEME_MAPPING", "SCHEMENO", "COLLEGE_ID", "COSCHNO in (" + cheme + ")", "");
            if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    //ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                    //ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                   // ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                    schemeno +=ds.Tables[0].Rows[i]["SCHEMENO"].ToString()+',';
                }
            }
            schemeno = schemeno.TrimEnd(',');
            ViewState["SchemeNo"] = schemeno;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_ExamAssesment.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void LoadGrade()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("ACD_DIRECT_GRADE_SYSTEM G INNER JOIN ACD_GRADE_NEW N ON (N.GRADENO=G.GRADENO)", "GRADENO", "GRADE,MINIRANGE,MAXIRANGE,GRADEPOINT,INDICATOR  ", "GRADENO>0", "GRADENO DESC");
            if (ds != null && ds.Tables.Count > 0)
            {
                lvGrade.DataSource = ds.Tables[0];
                lvGrade.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Masters_Grade.LoadSlot()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void GetID()
    {
        try
        {
            GetDetails();
            ViewState["id"] = string.Empty;
            int LEVEL = Convert.ToInt32(ddlLevel.SelectedValue);
            string SCHEMENO = ViewState["SchemeNo"].ToString();
            int schmeno = Convert.ToInt32(objCommon.LookUp("ACD_COLLEGE_SCHEME_MAPPING", "SCHEMENO", "COSCHNO="+lstcollege.SelectedValue));
            DataSet ds = objexam.GetIDGRADESYSTEM(LEVEL, schmeno);

            if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
            {
                int LEVELID = Convert.ToInt32(ddlLevel.SelectedValue);
                if (LEVELID == 1)
                {

                    lvCGPA.DataSource = ds;
                    lvCGPA.DataBind();
                    lvCGPA.Visible = true;
                    lvGrade.Visible = false;

                }



            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_ExamAssesment.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void GetID2()
    {
        try
        {
            GetDetails();
            ViewState["id"] = string.Empty;
            int LEVEL = Convert.ToInt32(ddlLevel.SelectedValue);
            string SCHEMENO = ViewState["SchemeNo"].ToString();
            int schmeno = Convert.ToInt32(objCommon.LookUp("ACD_COLLEGE_SCHEME_MAPPING", "SCHEMENO", "COSCHNO=" + lstcollege.SelectedValue));
            DataSet ds = objexam.GetIDGRADESYSTEM2(LEVEL, schmeno);

            if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
            {
                int LEVELID = Convert.ToInt32(ddlLevel.SelectedValue);

                if (LEVELID == 2)
                {
                    lvGrade.DataSource = ds;
                    lvGrade.DataBind();

                    lvGrade.Visible = true;
                    lvCGPA.Visible = false;
                }


            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_ExamAssesment.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void PopulateDropDown()
    {
        try
        {
            if (Session["usertype"].ToString().Equals("1"))
            {
                objCommon.FillListBox(lstcollege, "ACD_COLLEGE_SCHEME_MAPPING", "COSCHNO", "COL_SCHEME_NAME", "COLLEGE_ID IN(" + ViewState["College_ID"] + ") AND COSCHNO>0 AND COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "COLLEGE_ID ASC");

               //objCommon.FillDropDownList(ddlCollegeScheme, "ACD_COLLEGE_SCHEME_MAPPING", "COSCHNO", "COL_SCHEME_NAME", "COLLEGE_ID IN(" + ViewState["College_ID"] + ") AND COSCHNO>0 AND COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "COLLEGE_ID ASC");
               // DataSet ds = objCommon.FillDropDown("ACD_COLLEGE_SCHEME_MAPPING", "COSCHNO", "COL_SCHEME_NAME", "COLLEGE_ID IN(" + ViewState["College_ID"] + ") AND COSCHNO>0 AND COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "COLLEGE_ID ASC");
               // if (ds.Tables[0].Rows.Count > 0)
               // {
               //     lstcollege.DataSource = ds;
               //     lstcollege.DataTextField = ds.Tables[0].Columns["COL_SCHEME_NAME"].ToString();
               //     lstcollege.DataValueField = ds.Tables[0].Columns["COSCHNO"].ToString();
               //     lstcollege.DataBind();
               // }
            }
            //else
            //{
            //    objCommon.FillDropDownList(ddlCollegeScheme, "ACD_COLLEGE_SCHEME_MAPPING", "COSCHNO", "COL_SCHEME_NAME", "COLLEGE_ID IN(" + ViewState["College_ID"] + ") AND COSCHNO>0 AND COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "COLLEGE_ID ASC");
            //}

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Intermediate_Grade_Release.PopulateDropDown --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }

    protected void Clear()
    {
        Response.Redirect(Request.Url.ToString());
    }
    protected void ddlLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (lstcollege.SelectedIndex == 0)
        {
            if (ddlLevel.SelectedIndex == 0)
            {
                lvCGPA.Visible = false;
                lvGrade.Visible = false;
            }
            else if (ddlLevel.SelectedIndex == 1)
            {
                GetID();
                lvCGPA.Visible = true;
                lvGrade.Visible = false;
            }
            else
            {
                GetID2();
                lvGrade.Visible = true;
                lvCGPA.Visible = false;
            }
        }
        else
        {

        }
    }
    private void BindcgpaListView()
    {
        try
        {

            DataSet ds = objexam.GetCGPAGrade();

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                GetID();
                lvGrade.DataSource = ds;
                lvGrade.DataBind();
                objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvGrade);//Set label - 
            }
            else
            {

                lvGrade.DataSource = null;
                lvGrade.DataBind();
            }

        }
        catch (Exception ex)
        {
            throw;
        }
    }//clear
    private void BindMARKListView()
    {
        try
        {

            DataSet ds = objexam.GetCGPAGrade();

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                GetID();
                lvCGPA.DataSource = ds;
                lvCGPA.DataBind();
                objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvCGPA);//Set label - 
            }
            else
            {

                lvCGPA.DataSource = null;
                lvCGPA.DataBind();
            }

        }
        catch (Exception ex)
        {
            throw;
        }
    }//clear
    public void cleartext()
    {
        ddlCollegeScheme.SelectedIndex = 0;
        ddlLevel.SelectedIndex = 0;
        //lstcollege.Items.Clear();
        foreach (ListViewDataItem item in lvCGPA.Items)
        {
            TextBox txtMinRange = item.FindControl("txtMinRange") as TextBox;
            TextBox txtMaxRange = item.FindControl("txtMaxRange") as TextBox;
            TextBox txtIndicator = item.FindControl("txtIndicator") as TextBox;
            CheckBox chkStatus1 = item.FindControl("chkStatus1") as CheckBox;
            txtMinRange.Text = "";
            txtMaxRange.Text = "";
            txtIndicator.Text = "";
            chkStatus1.Checked = false;

        }
        foreach (ListViewDataItem item in lvGrade.Items)
        {
            TextBox txtRangeMin = item.FindControl("txtRangeMin") as TextBox;
            TextBox txtRangeMax = item.FindControl("txtRangeMax") as TextBox;
            TextBox txtGraadePoint = item.FindControl("txtGraadePoint") as TextBox;
            CheckBox chkStatus = item.FindControl("chkStatus") as CheckBox;
            txtRangeMin.Text = "";
            txtRangeMax.Text = "";
            txtGraadePoint.Text = "";
            chkStatus.Checked = false;

        }

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            GetDetails();
            int count = 0;
            foreach (ListItem items in lstcollege.Items)
            {

                if (items.Selected == true)
                {
                    count++;
                    //objCommon.DisplayMessage(this.updDirectGrade, "Please Enter the College/Scheme.", this.Page);
                    //cheme = items.Value;
                }
            }

            if (count == 0)
            {
                objCommon.DisplayMessage(this.updDirectGrade, "Please Enter the College/Scheme.", this.Page); 
            }
            else if (ddlLevel.SelectedIndex == 0)
            {

                objCommon.DisplayMessage(this.updDirectGrade, "Please Enter The Level.", this.Page);
            }
            else
            {

                if (ddlLevel.SelectedItem.Text == "CGPA/AGPA")
                {

                    foreach (ListViewDataItem item in lvCGPA.Items)
                    {
                        HiddenField hfdID1 = item.FindControl("hfdID1") as HiddenField;
                        HiddenField hfdValue1 = item.FindControl("hfdValue1") as HiddenField;
                        TextBox txtMinRange = item.FindControl("txtMinRange") as TextBox;
                        TextBox txtMaxRange = item.FindControl("txtMaxRange") as TextBox;
                        TextBox txtIndicator = item.FindControl("txtIndicator") as TextBox;
                        CheckBox chkStatus1 = item.FindControl("chkStatus1") as CheckBox;

                        string schemeno = ViewState["SchemeNo"].ToString();
                        int level = Convert.ToInt32(ddlLevel.SelectedValue);
                        int id = (Convert.ToInt32(hfdValue1.Value));
                        string min = (txtMinRange.Text).ToString();
                        if (min == string.Empty || min == "")
                        {
                            min1 = 0;
                        }
                        else
                        {
                            min1 = Convert.ToDecimal(min);

                        }
                        string max = (txtMaxRange.Text).ToString();
                        if (max == string.Empty || max == "")
                        {
                            max1 = 0;

                        }
                        else
                        {
                            max1 = Convert.ToDecimal(max);

                        }
                        string indi = (txtIndicator.Text).ToString();
                        int gradepoint = Convert.ToInt32(0);
                        int ActiveStatus;
                        if (chkStatus1.Checked == true)
                        {
                            ActiveStatus = 1;
                        }
                        else
                        {
                            ActiveStatus = 0;
                        }
                        if (Convert.ToDecimal(txtMaxRange.Text) >= Convert.ToDecimal(txtMinRange.Text))
                        {
                            CustomStatus cs = (CustomStatus)objexam.Add_DirectGradeSystem(schemeno, level, id, min1, max1, gradepoint, indi, ActiveStatus);
                            if (cs.Equals(CustomStatus.RecordSaved))
                            {

                                objCommon.DisplayMessage(this.updDirectGrade, "Insert Successfully", this.Page);
                            }
                            else
                            {

                            }
                        }
                        else
                        {
                            objCommon.DisplayMessage(this, "Min Mark Not Greater Than Max Mark", this.Page);
                        }
                    }

                    Clear();
                    //listbox();

                }
                else if (ddlLevel.SelectedItem.Text == "Marks Range")
                {

                    foreach (ListViewDataItem item in lvGrade.Items)
                    {

                        HiddenField hfdValue = item.FindControl("hfdValue") as HiddenField;
                        TextBox txtRangeMin = item.FindControl("txtRangeMin") as TextBox;
                        TextBox txtRangeMax = item.FindControl("txtRangeMax") as TextBox;
                        TextBox txtGraadePoint = item.FindControl("txtGraadePoint") as TextBox;
                        CheckBox chkStatus = item.FindControl("chkStatus") as CheckBox;
                        int id = (Convert.ToInt32(hfdValue.Value));
                        string schema = ViewState["SchemeNo"].ToString();
                        int level = Convert.ToInt32(ddlLevel.SelectedValue);
                        string min = (txtRangeMin.Text).ToString();
                        if (min == string.Empty || min == "")
                        {
                            min2 = 0;
                        }
                        else
                        {
                            //min2 = Convert.ToInt16(min2);
                            min2 = Convert.ToDecimal(min2);

                        }
                        string max = (txtRangeMax.Text).ToString();
                        if (max == string.Empty || max == "")
                        {
                            max2 = 0;

                        }
                        else
                        {
                            //max2 = Convert.ToInt16(max);
                            max2 = Convert.ToDecimal(max);
                        }
                        string indi = (txtGraadePoint.Text).ToString();
                        if (indi == string.Empty || indi == "")
                        {
                            gradepot = 0;
                        }
                        else
                        {
                            gradepot = Convert.ToInt32(indi);
                        }
                        string indicator = ("");
                        int ActiveStatus = chkStatus.Checked ? 1 : 0;
                        //if (Convert.ToDecimal(txtRangeMax.Text) >= Convert.ToDecimal(txtRangeMin.Text))
                        if ((max2) >= (min2))
                        {
                            CustomStatus cs = (CustomStatus)objexam.Add_DirectGradeSystem(schema, level, id, min2, max2, gradepot, indicator, ActiveStatus);
                            if (cs.Equals(CustomStatus.RecordSaved))
                            {
                                objCommon.DisplayMessage(this.updDirectGrade, "Insert Successfully", this.Page);
                            }
                            else
                            {

                            }

                        }
                        else
                        {
                            objCommon.DisplayMessage(this, "Min Mark Not Greater Than Max Mark", this.Page);
                        }

                    }
                    cleartext();
                    //listbox();
                }
                else
                {
                    objCommon.DisplayMessage(this.updDirectGrade, "Failed To Save Record ", this.Page);
                }

            }
        }
        catch (Exception ex)
        {

        }
    } 


    protected void txtRangeMin_TextChanged(object sender, EventArgs e)
    {
        foreach (ListViewDataItem item in lvGrade.Items)
        {

            HiddenField hfdValue = item.FindControl("hfdValue") as HiddenField;
            TextBox txtRangeMin = item.FindControl("txtRangeMin") as TextBox;
            TextBox txtRangeMax = item.FindControl("txtRangeMax") as TextBox;
            TextBox txtGraadePoint = item.FindControl("txtGraadePoint") as TextBox;
            if (Convert.ToDecimal(txtRangeMax.Text) > Convert.ToDecimal(txtRangeMin.Text))
            {

            }
            else
            {
                objCommon.DisplayMessage(this.updDirectGrade, "Min Mark Not Greater Than Max Mark", this.Page);

            }


        }

    }
    protected void txtMinRange_TextChanged(object sender, EventArgs e)
    {
        foreach (ListViewDataItem item in lvGrade.Items)
        {
            HiddenField hfdID1 = item.FindControl("hfdID1") as HiddenField;
            HiddenField hfdValue1 = item.FindControl("hfdValue1") as HiddenField;
            TextBox txtMinRange = item.FindControl("txtMinRange") as TextBox;
            TextBox txtMaxRange = item.FindControl("txtMaxRange") as TextBox;
            TextBox txtIndicator = item.FindControl("txtIndicator") as TextBox;
            if (Convert.ToDecimal(txtMaxRange.Text) > Convert.ToDecimal(txtMinRange.Text))
            {

            }
            else
            {
                objCommon.DisplayMessage(this.updDirectGrade, "Min Mark Not Greater Than Max Mark", this.Page);

            }

        }
    }
    protected void lstcollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (lstcollege.SelectedIndex == -1)
        {
            lvGrade.Visible = false;
            lvCGPA.Visible = false;
            ddlLevel.SelectedValue = "0";


        }
        else
        {

        }
    }


}