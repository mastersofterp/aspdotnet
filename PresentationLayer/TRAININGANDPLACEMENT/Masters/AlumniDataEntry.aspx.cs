//======================================================================================
// PROJECT NAME  : UAIMS
// MODULE NAME   : T&P
// PAGE NAME     : ALUMNI DATA ENTRY
// CREATION DATE : 10-MAR-2013
// CREATED BY    : KAPIL BUDHLANI
// MODIFIED DATE :
// MODIFIED DESC :
//======================================================================================

using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;

public partial class TRAININGANDPLACEMENT_MASTERS_AlumniDataEntry : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    TPController objTpcont = new TPController();
    

    #region Page Events
    protected void Page_PreInit(object sender, EventArgs e)
    {
        // Set MasterPage
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
                // Check User Session
                if (Session["userno"] == null || Session["username"] == null ||
                    Session["usertype"] == null || Session["userfullname"] == null)
                {
                    Response.Redirect("~/default.aspx");
                }
                else
                {
                    // Check User Authority 
                    this.CheckPageAuthorization();

                    // Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    // Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                }
                PopulateDropDownList();
                BindListView();
                ViewState["action"] = "add";
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "TRAININGANDPLACEMENT_MASTERS_AlumniDataEntry.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }   
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            // Check user's authrity for Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Block.aspx");
            }
        }
        else
        {
            // Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Block.aspx");
        }
    }
    #endregion Page Events

    #region Action
    //private bool CheckDuplicateEntry()
    //{
    //    bool flag = false;
    //    try
    //    {
    //        string blno = objCommon.LookUp("ACD_HOSTEL_BLOCK_MASTER", "BL_NO", "HOSTEL_NO=" + ddlHostel.SelectedValue + " AND BLOCK_NAME='" + txtBlockName.Text + "'");
    //        if (blno != null && blno != string.Empty)
    //        {
    //            flag = true;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUaimsCommon.ShowError(Page, "Block.CheckDuplicateEntry() --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUaimsCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //    return flag;
    //}

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            TPStudent objTp = new TPStudent();

            objTp.CompId = Convert.ToInt32(ddlComp.SelectedValue);
            objTp.StudName = txtStuName.Text.Trim();
            objTp.EnrollmentNo = Convert.ToInt32( txtEnrollNo .Text.Trim());
            objTp.ContactNo  = txtContact.Text.Trim();
            objTp.CompContact = txtCompCont.Text.Trim();
            objTp.BranchNo = Convert.ToInt32(ddlBranch.SelectedValue); 
            objTp.Desig = txtDesig.Text.Trim();
            objTp.EmailID  = txtEmail .Text.Trim();
            objTp.LAddress = txtAdd.Text.Trim();
            objTp.PassYear = Convert.ToInt32(txtPassYear.Text.Trim());
            objTp.CollegeCode  = Convert.ToString(Session["colcode"]);

            /// check form action whether add or update
            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    //if (CheckDuplicateEntry() == true)
                    //{
                    //    objCommon.DisplayMessage("Entry for this Selection Already Done!", this.Page);
                    //    return;
                    //}
                    //Add Block
                    CustomStatus cs = (CustomStatus)objTpcont.AddAlumniData(objTp);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        objCommon.DisplayMessage("Record Saved Successfully!!!", this.Page);
                        ViewState["action"] = "add";
                        Clear();
                    }
                }
                else
                {
                    //Edit
                    if (ViewState["aluno"] != null)
                    {
                        objTp.AluNo = Convert.ToInt32(ViewState["aluno"].ToString());

                        CustomStatus cs = (CustomStatus)objTpcont.UpdateAlumniData(objTp);
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            objCommon.DisplayMessage("Record Updated Successfully!!!", this.Page);
                            ViewState["action"] = "add";
                            Clear();
                        }
                    }
                }
                BindListView();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "TRAININGANDPLACEMENT_MASTERS_AlumniDataEntry.btnSubmit_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable");
        }
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int aluNo = int.Parse(btnEdit.CommandArgument);

            ShowDetail(aluNo);
            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "TRAININGANDPLACEMENT_MASTERS_AlumniDataEntry.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
        //Clear();
        ViewState["action"] = null;
    }

    private void Clear()
    {
        ddlComp.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        txtStuName .Text = string.Empty;
        txtEnrollNo.Text = string.Empty;
        txtContact.Text = string.Empty;
        txtCompCont.Text = string.Empty;
        txtDesig.Text = string.Empty;
        txtEmail .Text = string.Empty;
        txtAdd.Text = string.Empty;
        txtPassYear.Text = string.Empty;
    }

    private void PopulateDropDownList()
    {
        try
        {
            objCommon.FillDropDownList(ddlComp, "ACD_TP_COMPANY", "COMPID", "COMPNAME", "COMPID > 0 ", "COMPNAME");
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO > 0 ", "");
            objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "BRANCHNO > 0 ", "");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "TRAININGANDPLACEMENT_MASTERS_AlumniDataEntry.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void BindListView()
    {
        try
        {
            DataSet ds = new DataSet();
            ds = objCommon.FillDropDown("ACD_TP_ALUMNI_DATA A INNER JOIN ACD_TP_COMPANY C ON (A.COMPANYID=C.COMPID)", "ALUNO,STUDNAME,ENROLLMENTNO,COMPANYID,CONTACTNO,COMP_CONTACTNO,DESIGNATION,A.EMAILID", "ADDRESS,COMPNAME", "", "");
            lvAlumni.DataSource = ds;
            lvAlumni.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "TRAININGANDPLACEMENT_MASTERS_AlumniDataEntry.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetail(int aluNo)
    {
        try
        {
            DataSet ds = new DataSet();
            ds = objCommon.FillDropDown("ACD_TP_ALUMNI_DATA A INNER JOIN ACD_TP_COMPANY C ON (A.COMPANYID=C.COMPID)", "ALUNO,STUDNAME,ENROLLMENTNO,COMPANYID,CONTACTNO,COMP_CONTACTNO,ISNULL(A.BRANCHNO,0)BRANCHNO,DESIGNATION,A.EMAILID", "ADDRESS,COMPNAME,PASSYEAR", "ALUNO=" + aluNo, "");
                        
            //Show Detail
            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["aluno"] = aluNo.ToString();
                //txtSchduleDt.Text = ds.Tables[0].Rows[0]["SCHEDULEDATE"].ToString();
                txtStuName.Text = ds.Tables[0].Rows[0]["STUDNAME"].ToString();
                ddlComp.SelectedValue = ds.Tables[0].Rows[0]["COMPANYID"].ToString();
                txtEnrollNo.Text = ds.Tables[0].Rows[0]["ENROLLMENTNO"].ToString();
                txtContact.Text = ds.Tables[0].Rows[0]["CONTACTNO"].ToString();
                txtCompCont.Text = ds.Tables[0].Rows[0]["COMP_CONTACTNO"].ToString();
                ddlBranch.SelectedValue = ds.Tables[0].Rows[0]["BRANCHNO"].ToString();
                txtDesig.Text = ds.Tables[0].Rows[0]["DESIGNATION"].ToString();
                txtEmail.Text = ds.Tables[0].Rows[0]["EMAILID"].ToString();
                txtAdd.Text = ds.Tables[0].Rows[0]["ADDRESS"].ToString();
                txtPassYear.Text = ds.Tables[0].Rows[0]["PASSYEAR"].ToString();

               ddlDegree.SelectedValue = objCommon.LookUp("ACD_BRANCH","DEGREENO","BRANCHNO="+Convert.ToInt16 (ds.Tables[0].Rows[0]["BRANCHNO"].ToString()));
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "TRAININGANDPLACEMENT_MASTERS_AlumniDataEntry.ShowDetail-> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
        
    }

    protected void dpBlock_PreRender(object sender, EventArgs e)
    {
        BindListView();
    }
    #endregion Action
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(ddlDegree.SelectedIndex>0)
            objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "DEGREENO = " + Convert.ToInt16(ddlDegree.SelectedValue), "LONGNAME");

    }
}
