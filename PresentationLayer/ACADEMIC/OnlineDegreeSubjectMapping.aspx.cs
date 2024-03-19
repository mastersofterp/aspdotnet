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
using IITMS.UAIMS.BusinessLayer;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;
using System.IO;
using System.Threading.Tasks;



public partial class ACADEMIC_OnlineDegreeSubjectMapping : System.Web.UI.Page
{

    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();

    OnlineAdmissionController objAdmC = new OnlineAdmissionController();

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

            }
            //Populate the Drop Down Lists
            PopulateDropDownList();
            BindListView();
            objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));
            objCommon.SetLabelData("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]));
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "HidePopup", "Datatable();", true);
        }
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=OnlineDegreeSubjectMapping.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=OnlineDegreeSubjectMapping.aspx");
        }
    }
    private void PopulateDropDownList()
    {
        try
        {

            objCommon.FillDropDownList(ddlprogramt, "ACD_UA_SECTION", "UA_SECTION", "UA_SECTIONNAME", "", "UA_SECTION");


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_OnlineDegreeSubjectMapping.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlprogramt_SelectedIndexChanged(object sender, EventArgs e)
    {
        chkSpec.Checked = false;

        if (ddlprogramt.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.DEGREENO > 0 AND B.UGPGOT=" + ddlprogramt.SelectedValue, "D.DEGREENAME");
            ddlDegree.Focus();
        }
        else
        {
            ddlprogramt.SelectedIndex = 0;
        }
    }

    //protected void NumberDropDown_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    //DataPager2.SetPageProperties(0, DataPager2.PageSize, true);
    //    //DataPager2.PageSize = Convert.ToInt32(NumberDropDown.SelectedValue);
    //}
    //protected void lvBulkDetail_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
    //{
    //    //DataPager2.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
    //    //BindListView();
    //}

    private void BindListView()
    {
        try
        {
            DataSet ds =  objAdmC.GetAllOnlineMappingdata();
            if (ds.Tables[0].Rows.Count > 0)
            {

                lvonlinemapping.DataSource = ds;
                lvonlinemapping.DataBind();
                lvonlinemapping.Visible = true;
            }
            else
            {
                lvonlinemapping.DataSource = null;
                lvonlinemapping.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_LoanApplicableStudentList() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }


    

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int organizationid = 0;
            int iscompulsory = 0;
            int iscutff = 0;
            int isothers = 0;
            int IsActive = 0; string subName = string.Empty;
            int programtype = Convert.ToInt32(ddlprogramt.SelectedValue);
            int degree = Convert.ToInt32(ddlDegree.SelectedValue);
            int subjectno = Convert.ToInt32(ddlsubject.SelectedValue);
            string subjectname = ddlsubject.SelectedItem.Text;
            int CREATED_BY = Convert.ToInt32(Session["userno"]);
            string ipAddress = Request.ServerVariables["REMOTE_ADDR"];
            int ModifiedBy = Convert.ToInt32(Session["userno"].ToString());
            DateTime ModifiedDate = DateTime.UtcNow.AddHours(5.5);
            string ModifiedIPAddress = Convert.ToString(Session["ipAddress"]);
            organizationid = Convert.ToInt32(Session["OrgId"]);
            subName = txtSubName.Text.ToString().TrimEnd();
            int branchNo = ddlBranch.SelectedIndex > 0 ? Convert.ToInt32(ddlBranch.SelectedValue) : 0;
            int special = chkSpec.Checked ? 1 : 0;
            if (hfdiscompulsory.Value == "true")
            {
                iscompulsory = 1;
            }
            else
            {
                iscompulsory = 0;
            }

            if (hfdiscutoff.Value == "true")
            {
                iscutff = 1;
            }
            else
            {
                iscutff = 0;
            }

            if (hfdisothers.Value == "true")
            {
                isothers = 1;
            }
            else
            {
                isothers = 0;
            }


            if (hfdActive.Value == "true")
            {
                IsActive = 1;
            }
            else
            {
                IsActive = 0;
            }
            //CustomStatus cs = (CustomStatus)objAdmC.AdddegreesubjectMapping(Convert.ToInt32(ViewState["subid"]), degree, subjectno, subjectname, iscompulsory, iscutff, IsActive, isothers, CREATED_BY, ipAddress, ModifiedBy, ModifiedIPAddress, organizationid);
            CustomStatus cs = (CustomStatus)objAdmC.AdddegreesubjectMapping(Convert.ToInt32(ViewState["subid"]), degree, subjectno, subjectname, iscompulsory, iscutff, isothers, IsActive, CREATED_BY, ipAddress, ModifiedBy, ModifiedIPAddress, organizationid, subName, special, branchNo);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                ClearField();
                objCommon.DisplayMessage(this, "Record saved Successfully", this.Page);
                this.BindListView();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "HidePopup", "Datatable();", true);
            }
            if (cs.Equals(CustomStatus.RecordUpdated))
            {
                ClearField();
                objCommon.DisplayMessage(this, "Record Updated Successfully", this.Page);
                this.BindListView();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "HidePopup", "Datatable();", true);
            }
            else if (cs.Equals(CustomStatus.DuplicateRecord))
            {
                objCommon.DisplayMessage(this, "Record Already Exists !!", this.Page);
                ClearField();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "HidePopup", "Datatable();", true);
            }
            else if (cs.Equals(CustomStatus.TransactionFailed))
            {
                objCommon.DisplayMessage(this, "Transaction Failed", this.Page);
                ClearField();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "HidePopup", "Datatable();", true);
            }
        }



        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "AffiliatedFeesCategory.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    protected void ClearField()
    {
        ddlprogramt.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        ddlsubject.SelectedIndex = 0;
        txtSubName.Text = string.Empty;
        chkSpec.Checked = false;
        divBranch.Attributes.Add("style", "display:none");
        ddlBranch.SelectedIndex = 0;
        ddlDegree.Items.Clear();
        ddlBranch.Items.Clear();
        ddlDegree.Items.Add(new ListItem("Please Select", "0"));
        ddlBranch.Items.Add(new ListItem("Please Select", "0"));
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

            ImageButton btnEdit = sender as ImageButton;
            int subid = int.Parse(btnEdit.CommandArgument);
            ViewState["subid"] = int.Parse(btnEdit.CommandArgument);
            this.ShowDetails(subid);
        }
        catch (Exception ex)
        {
            throw;
        }

    }

    private void ShowDetails(Int32 SUB_ID)
    {
        try
        {
            OnlineAdmissionController objAdmC = new OnlineAdmissionController();

            SqlDataReader dr = objAdmC.GetAllOnlineMappingdatabyno(SUB_ID);
            if (dr != null)
            {
                if (dr.Read())
                {
                    ddlprogramt.SelectedValue = dr["UGPGOT"] == DBNull.Value ? "0" : dr["UGPGOT"].ToString();
                    objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.DEGREENO > 0 AND B.UGPGOT=" + ddlprogramt.SelectedValue, "D.DEGREENAME");
                    string degreeno = dr["DEGREENO"] == DBNull.Value ? "0" : dr["DEGREENO"].ToString();
                    string subjectno = dr["SUBJECT_NO"] == DBNull.Value ? "0" : dr["SUBJECT_NO"].ToString();
                    ddlDegree.SelectedValue = dr["DEGREENO"] == DBNull.Value ? "0" : dr["DEGREENO"].ToString();
                    ddlsubject.SelectedValue = dr["SUBJECT_NO"] == DBNull.Value ? "0" : dr["SUBJECT_NO"].ToString();
                    txtSubName.Text = dr["SUB_NAME"] == DBNull.Value ? "" : dr["SUB_NAME"].ToString();
                    if (dr["SPECIALIZE"].ToString() == "0")
                    {
                        chkSpec.Checked = false;
                        divBranch.Attributes.Add("style", "display:none");
                    }
                    else
                    {
                        objCommon.FillDropDownList(ddlBranch, "ACD_COLLEGE_DEGREE_BRANCH A INNER JOIN ACD_BRANCH B ON(A.BRANCHNO=B.BRANCHNO)", "DISTINCT A.BRANCHNO", "B.LONGNAME", "UGPGOT=" + Convert.ToInt32(ddlprogramt.SelectedValue) + " AND DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue), "A.BRANCHNO");
                        ddlBranch.SelectedValue = dr["BRANCHNO"] == DBNull.Value ? "0" : dr["BRANCHNO"].ToString();
                        chkSpec.Checked = true;
                        divBranch.Attributes.Add("style", "display:block");
                    }
                    ViewState["compulsory"] = dr["IS_COMPULSORY"].ToString();
                    if (dr["IS_COMPULSORY"].ToString() == "YES")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Src1", "SetStatcompulsory(true);", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Src2", "SetStatcompulsory(false);", true);
                    }

                    if (dr["IS_CUTOFF"].ToString() == "YES")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Src3", "SetStatcutoff(true);", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Src4", "SetStatcutoff(false);", true);
                    }

                    if (dr["IS_OTHERS"].ToString() == "YES")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Src5", "SetStatothers(true);", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Src6", "SetStatothers(false);", true);
                    }

                    if (dr["ACTIVESTATUS"].ToString() == "Active")
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alertscript9", "SetStatActive(true);", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alertscript10", "SetStatActive(false);", true);
                    }
                }
            }
            if (dr != null) dr.Close();
        }
        catch (Exception ex)
        {
            throw;
        }

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearField();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "HidePopup", "Datatable();", true);
    }
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            chkSpec.Checked = false;
            if (ddlDegree.SelectedIndex > 0)
            {

                objCommon.FillDropDownList(ddlBranch, "ACD_COLLEGE_DEGREE_BRANCH A INNER JOIN ACD_BRANCH B ON(A.BRANCHNO=B.BRANCHNO)", "DISTINCT A.BRANCHNO", "B.LONGNAME", "UGPGOT=" + Convert.ToInt32(ddlprogramt.SelectedValue) + " AND DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue), "A.BRANCHNO");
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
}