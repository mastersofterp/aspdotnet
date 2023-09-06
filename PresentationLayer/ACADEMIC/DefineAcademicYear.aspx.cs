using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ACADEMIC_DefineAcademicYear : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    static int YearID;
    StudentController objSC = new StudentController();
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

                BindListView();
                AdmBatchBindListView();
                AcademicBatchListView();
            }
            ViewState["action"] = "add";

        }
    }

    private void BindListView()
    {
        try
        {
            StudentController objSC = new StudentController();
            DataSet ds = objSC.GetAllAcedemicYear();

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                pnlAcademicYear.Visible = true;
                lvAcademicYear.DataSource = ds;
                lvAcademicYear.DataBind();
            }
            else
            {
                pnlAcademicYear.Visible = false;
                lvAcademicYear.DataSource = null;
                lvAcademicYear.DataBind();
            }

        }
        catch (Exception ex)
        {
            throw;
        }
    }


    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=DefineAcademicYear.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=DefineAcademicYear.aspx");
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int IsActive = 0;
        int IsCurrFinaYear = 0;
        string StartEndDate = hdnDate.Value;
        string[] dates = new string[] { };
        if ((StartEndDate) == "")//GetDocs()
        {
            objCommon.DisplayMessage(this.updSession, "Please select Start Date End Date !", this.Page);
            return;
        }
        else
        {
            StartEndDate = StartEndDate.Substring(0, StartEndDate.Length - 0);

            dates = StartEndDate.Split('-');
        }
        string StartDate = dates[0];
        string EndDate = dates[1];
        DateTime dtStartDate = DateTime.Parse(StartDate);
        string SDate = dtStartDate.ToString("yyyy/MM/dd");
        DateTime dtEndDate = DateTime.Parse(EndDate);
        string EDate = dtEndDate.ToString("yyyy/MM/dd");

        try
        {

            if (Convert.ToDateTime(EDate) <= Convert.ToDateTime(SDate))
            {
                objCommon.DisplayMessage(this.updSession, "End Date should be greater than Start Date", this.Page);
                return;
            }
            else
            {
                StudentController objSC = new StudentController();
                string academicyearname = txtSLongName.Text.ToString();
                DateTime Session_SDate = Convert.ToDateTime(SDate);
                DateTime Session_EDate = Convert.ToDateTime(EDate);
                if (hfdActive.Value == "true")
                {
                    IsActive = 1;
                }
                else
                {
                    IsActive = 0;

                }
                if (chkIsCurrFinacialYear.Checked)
                {
                    IsCurrFinaYear = 1;
                }
                else
                {
                    IsCurrFinaYear = 0;
                }
                if (ViewState["action"] != null)
                {
                    if (ViewState["action"].ToString().Equals("add"))
                    {
                        //Add Batch
                        CustomStatus cs = (CustomStatus)objSC.AddAcademicYear(academicyearname, Session_SDate, Session_EDate, IsActive, IsCurrFinaYear);

                        if (cs.Equals(CustomStatus.RecordExist))
                        {
                            objCommon.DisplayMessage(this.updSession, "Record Already Exist", this.Page);
                        }
                        else if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            ViewState["action"] = "add";
                            ClearControls();
                            objCommon.DisplayMessage(this.updSession, "Record Saved Successfully!", this.Page);
                        }
                        else
                        {
                            objCommon.DisplayMessage(this.updSession, "Error Adding Slot Name!", this.Page);
                        }
                    }
                    else
                    {
                        //ImageButton btnEdit = sender as ImageButton;
                        //int academicyearId = Convert.ToInt32((btnEdit.CommandArgument));
                        int academicyearId = Convert.ToInt32(ViewState["yearid"]);
                        CustomStatus cs = (CustomStatus)objSC.UpdateAcademicYear(academicyearname, Session_SDate, Session_EDate, IsActive, academicyearId, IsCurrFinaYear);

                        if (cs.Equals(CustomStatus.RecordExist))
                        {
                            objCommon.DisplayMessage(this.updSession, "Record Already Exist", this.Page);
                        }
                        else if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            objCommon.DisplayMessage(this.updSession, "Record Updated Successfully!", this.Page);
                            ClearControls();
                            btnSubmit.Text = "Submit";
                            txtSLongName.Focus();
                        }
                        else
                        {
                            objCommon.DisplayMessage(this.updSession, "Error Adding Slot Name!", this.Page);
                        }

                    }

                    BindListView();
                }
                btnSubmit.Text = "Submit";
            }

        }
        catch (Exception ex)
        {
            throw;
        }

    }

    private void ClearControls()
    {
        txtEndDate.Text = string.Empty;
        txtStartDate.Text = string.Empty;
        txtSLongName.Text = string.Empty;
        btnSubmit.Text = "Submit";
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        this.ClearControls();
        Response.Redirect(Request.Url.ToString());
    }

    private void ShowDetail(int feedbackNo)
    {
        StudentController objSC = new StudentController();
        SqlDataReader dr = objSC.GetyearID(YearID);

        if (dr != null)
        {
            if (dr.Read())
            {
                txtSLongName.Text = dr["ACADEMIC_YEAR_NAME"] == null ? string.Empty : dr["ACADEMIC_YEAR_NAME"].ToString();
                //txtStartDate.Text = dr["ACADEMIC_YEAR_STARTDATE"] == DBNull.Value ? string.Empty : Convert.ToDateTime(dr["ACADEMIC_YEAR_STARTDATE"].ToString()).ToString("dd/MM/yyyy");
                txtStartDate.Text = dr["ACADEMIC_YEAR_STARTDATE"] == DBNull.Value ? string.Empty : Convert.ToDateTime(dr["ACADEMIC_YEAR_STARTDATE"].ToString()).ToString("dd/MM/yyyy");

                //txtEndDate.Text = dr["ACADEMIC_YEAR_ENDDATE"] == DBNull.Value ? string.Empty : Convert.ToDateTime(dr["ACADEMIC_YEAR_ENDDATE"].ToString()).ToString("dd/MM/yyyy");

                txtEndDate.Text = dr["ACADEMIC_YEAR_ENDDATE"] == DBNull.Value ? string.Empty : Convert.ToDateTime(dr["ACADEMIC_YEAR_ENDDATE"].ToString()).ToString("dd/MM/yyyy");

                hdnDate.Value = dr["ACADEMIC_YEAR_STARTDATE"] != DBNull.Value ? Convert.ToDateTime(dr["ACADEMIC_YEAR_STARTDATE"].ToString()).ToString("MMM dd, yyyy") + " - " + Convert.ToDateTime(dr["ACADEMIC_YEAR_ENDDATE"].ToString()).ToString("MMM dd, yyyy") : Convert.ToDateTime(DateTime.Now).ToString("MMM dd, yyyy") + " - " + Convert.ToDateTime(DateTime.Now).ToString("MMM dd, yyyy");

                if (dr["ACTIVE_STATUS"].ToString() == "Active")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatActive(true);", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatActive(false);", true);
                }
                ScriptManager.RegisterClientScriptBlock(updSession, updSession.GetType(), "Src", "Setdate('" + hdnDate.Value + "');", true);

                if (Convert.ToInt32(dr["IS_CURRENT_FY"]) == 1)
                {
                    chkIsCurrFinacialYear.Checked = true;
                }
                else
                {
                    chkIsCurrFinacialYear.Checked = false;
                }


            }
        }
        if (dr != null) dr.Close();
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            YearID = int.Parse(btnEdit.CommandArgument);
            ViewState["yearid"] = YearID;
            ShowDetail(YearID);
            ViewState["action"] = "edit";
            btnSubmit.Text = "Update";
            txtSLongName.Focus();
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    //Added By Tejaswini Dhoble[08-12-22]
    protected void AdmBatchBindListView()
    {
        try
        {
            int id = 0;
            int mode = 1;
            DataSet ds = objSC.GetEditAdmBatchData(id, mode);

            if (ds.Tables[0].Rows.Count > 0)
            {
                PanelAdmBatch.Visible = true;
                lvAdmBatch.DataSource = ds.Tables[0];
                lvAdmBatch.DataBind();
            }
            else
            {
                PanelAdmBatch.Visible = true;
                lvAdmBatch.DataSource = null;
                lvAdmBatch.DataBind();

            }
            //foreach (ListViewDataItem dataitem in lvAdmBatch.Items)
            //{
            //    Label Status = dataitem.FindControl("lblStatus") as Label;

            //    string Statuss = (Status.ToolTip);

            //    if (Statuss == "0")
            //    {
            //        //  Status.CssClass = "badge badge-danger";
            //        Status.Text = "INACTIVE";
            //    }
            //    else
            //    {
            //        //Status.CssClass = "badge badge-success";
            //        Status.Text = "ACTIVE";
            //    }

            //}
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "DefineAcademicYear.AdmBatchBindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    public void ClearData()
    {
        txtAdmBatch.Text = "";

    }
    protected void AdmBatchCancel_Click(object sender, EventArgs e)
    {
        ClearData();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        int isadmissionflag = 0;
        string status;
        string AdmBatch = txtAdmBatch.Text.Trim();
        if (hfdActive.Value == "true")
        {
            status = "1";
        }
        else
        {
            status = "0";
        }
        if (chkIsAdmissionFlag.Checked)
        {
            isadmissionflag = 1;
        }
        else
        {
            isadmissionflag = 0;
        }
        int mode;
        if (ViewState["action"] != null && ViewState["action"].ToString().Equals("edit"))
        {

            int id = Convert.ToInt32(ViewState["id"]);
            mode = 2;
            CustomStatus cs = (CustomStatus)objSC.UpdateAdmBatchData(id, AdmBatch, status, mode, isadmissionflag);
            if (cs.Equals(CustomStatus.RecordUpdated))
            {
                ClearData();
                AdmBatchBindListView();
                objCommon.DisplayMessage(this, "Record Updated sucessfully", this.Page);
                ViewState["action"] = null;
            }
        }
        else
        {
            mode = 1;

            CustomStatus cs = (CustomStatus)objSC.InsertAdmBatchData(0, AdmBatch, status, mode, isadmissionflag);
            if (cs.Equals(CustomStatus.RecordSaved))
            {

                objCommon.DisplayMessage(this, "Record Saved sucessfully", this.Page);
                AdmBatchBindListView();
                ClearData();


            }

            else
            {

                objCommon.DisplayMessage(this, "Record Already Exist", this.Page);
                ClearData();


            }
            AdmBatchBindListView();
        }
    }
    protected void btnEditAdmBatch_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int ID = Convert.ToInt32(btnEdit.CommandArgument);
            ViewState["id"] = Convert.ToInt32(btnEdit.CommandArgument);
            int mode = 2;
            ShowDetailAdmBatch(ID, mode);
            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "PbiConfiguration.btnEditWorkspace_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private void ShowDetailAdmBatch(int ID, int mode)
    {

        DataSet ds = objSC.GetEditAdmBatchData(ID, mode);
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {

            txtAdmBatch.Text = ds.Tables[0].Rows[0]["BATCHNAME"].ToString();
            if (ds.Tables[0].Rows[0]["ACTIVESTATUS"].ToString() == "1")
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetSetSubjecttype(true);", true);

            }
            else
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetSetSubjecttype(false);", true);
            }
            if (Convert.ToInt32(ds.Tables[0].Rows[0]["IS_ADMISSION_FLAG"].ToString()) == 1)
            {
                chkIsAdmissionFlag.Checked = true;
            }
            else
            {
                chkIsAdmissionFlag.Checked = false;
            }
        }
    }

    protected void lvAdmBatch_ItemEditing(object sender, ListViewEditEventArgs e)
    {

    }

    protected void AcademicBatchListView()
    {
        try
        {
            int id = 0;
            int mode = 1;
            DataSet ds = objSC.GetEditAcdemicBatchData(id, mode);

            if (ds.Tables[0].Rows.Count > 0)
            {
                PanelAcdBatch.Visible = true;
                lvAcdBatch.DataSource = ds.Tables[0];
                lvAcdBatch.DataBind();
            }
            else
            {
                PanelAcdBatch.Visible = true;
                lvAcdBatch.DataSource = null;
                lvAcdBatch.DataBind();

            }
            //foreach (ListViewDataItem dataitem in lvAcdBatch.Items)
            //{
            //    Label Status = dataitem.FindControl("lblAcdStatus") as Label;

            //    string Statuss = (Status.ToolTip);

            //    if (Statuss == "0")
            //    {
            //        //  Status.CssClass = "badge badge-danger";
            //        Status.Text = "INACTIVE";
            //    }
            //    else
            //    {
            //        //Status.CssClass = "badge badge-success";
            //        Status.Text = "ACTIVE";
            //    }

            //}
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "DefineAcademicYear.AcademicBatchListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    public void AcdBatchClearData()
    {
        txtAcdBatch.Text = "";

    }
    protected void btnAcdBatchSubmit_Click(object sender, EventArgs e)
    {
        string status;
        string AcdBatch = txtAcdBatch.Text.Trim();
        if (hfdActive.Value == "true")
        {
            status = "1";
        }
        else
        {
            status = "0";
        }
        int mode;
        if (ViewState["Acdaction"] != null && ViewState["Acdaction"].ToString().Equals("Acdedit"))
        {

            int id = Convert.ToInt32(ViewState["Acdid"]);
            mode = 2;
            CustomStatus cs = (CustomStatus)objSC.UpdateAcdemicBatchData(id, AcdBatch, status, mode);
            if (cs.Equals(CustomStatus.RecordUpdated))
            {
                AcdBatchClearData();
                AcademicBatchListView();
                objCommon.DisplayMessage(this, "Record Updated sucessfully", this.Page);
                ViewState["Acdaction"] = null;
            }
        }
        else
        {
            mode = 1;

            CustomStatus cs = (CustomStatus)objSC.InsertAcdemicBatchData(0, AcdBatch, status, mode);
            if (cs.Equals(CustomStatus.RecordSaved))
            {

                objCommon.DisplayMessage(this.UdpAcademicBatch, "Record Saved sucessfully", this.Page);
                AcademicBatchListView();
                AcdBatchClearData();


            }

            else
            {

                objCommon.DisplayMessage(this.UdpAcademicBatch, "Record Already Exist", this.Page);
                AcdBatchClearData();


            }
            AcademicBatchListView();
        }
    }


    protected void btnAcdBatchCancel_Click(object sender, EventArgs e)
    {
        AcdBatchClearData();
    }

    protected void btnAcdBatchEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnAcdBatchEdit = sender as ImageButton;
            int ID = Convert.ToInt32(btnAcdBatchEdit.CommandArgument);
            ViewState["Acdid"] = Convert.ToInt32(btnAcdBatchEdit.CommandArgument);
            int mode = 2;
            ShowDetailAcademicBatch(ID, mode);
            ViewState["Acdaction"] = "Acdedit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "DefineAcademicYear.btnAcdBatchEdit_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private void ShowDetailAcademicBatch(int ID, int mode)
    {

        DataSet ds = objSC.GetEditAcdemicBatchData(ID, mode);
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {

            txtAcdBatch.Text = ds.Tables[0].Rows[0]["ACADEMICBATCH"].ToString();
            if (ds.Tables[0].Rows[0]["ACTIVESTATUS"].ToString() == "1")
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetSetAcademicBatch(true);", true);

            }
            else
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetSetAcademicBatch(false);", true);
            }


        }
    }
    protected void lvAcdBatch_ItemEditing(object sender, ListViewEditEventArgs e)
    {

    }
}