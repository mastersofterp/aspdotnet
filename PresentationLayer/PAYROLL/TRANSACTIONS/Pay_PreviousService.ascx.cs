//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : PAY ROLL
// PAGE NAME     : Pay_PreviousService.ascx                                                
// CREATION DATE : 23-June-2009                                                        
// CREATED BY    : G.V.S. KIRAN                                                         
// MODIFIED DATE :
// MODIFIED DESC :
//=======================================================================================
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

public partial class PayRoll_Pay_PreviousService : System.Web.UI.UserControl
{
    //CREATING OBJECTS OF CLASS FILES COMMON,UAIMS_COMMON,PAYCONTROLLER
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ServiceBookController objServiceBook = new ServiceBookController();

    public int _idnoEmp;
    
    protected void Page_Load(object sender, EventArgs e)
    {

        //string empno = ViewState["idno"].ToString();

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
               // CheckPageAuthorization();
            }
            //By default setting ViewState["action"] to add
            ViewState["action"] = "add";
        }

        DropDownList ddlempidno = (DropDownList)this.Parent.FindControl("ddlEmployee");
        _idnoEmp = Convert.ToInt32(ddlempidno.SelectedValue);
        BindListViewPreService();
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Pay_PreviousService.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Pay_PreviousService.aspx");
        }
    }

    private void BindListViewPreService()
    {
        try
        {  
            DataSet ds = objServiceBook.GetAllPreServiceDetailsOfEmployee(_idnoEmp);
            lvPrvService.DataSource = ds;
            lvPrvService.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_PreviousService.BindListViewPreService-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            Panel updpersonaldetails = (Panel)this.Parent.FindControl("upWebUserControl");
            ServiceBook objSevBook = new ServiceBook();
            objSevBook.IDNO = _idnoEmp;
            objSevBook.FDT = Convert.ToDateTime(txtFromDate.Text);
            objSevBook.TDT = Convert.ToDateTime(txtToDate.Text);
            objSevBook.REMARK = txtReMarks.Text;
            objSevBook.INST = txtInstitute.Text;
            objSevBook.POSTNAME = txtPostName.Text;
            objSevBook.EXPERIENCE = txtExperience.Text;
            objSevBook.TERMINATION = txtReasonForTerminationOfService.Text;
            objSevBook.OFFICER = txtDesignationOfAttestingOfficer.Text;
            objSevBook.COLLEGE_CODE = Session["colcode"].ToString();
            if (flupld.HasFile)
            {
                objSevBook.ATTACHMENTS = Convert.ToString(flupld.PostedFile.FileName.ToString());
            }
            else
            {
                if (ViewState["attachment"] != null)
                {
                    objSevBook.ATTACHMENTS = ViewState["attachment"].ToString();
                }
                else
                {
                    objSevBook.ATTACHMENTS = string.Empty;
                }

            }
            //Check whether to add or update
            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    //Add New Help
                    CustomStatus cs = (CustomStatus)objServiceBook.AddPreService(objSevBook);

                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        objServiceBook.upload_new_files("PREVIOUS_SERVICE", _idnoEmp, "PSNO", "PAYROLL_SB_PRESERVICE", "PRE_", flupld);
                        this.Clear();
                        this.BindListViewPreService();
                        this.objCommon.DisplayMessage(updpersonaldetails, "Record Saved Successfully", this.Page);
                    }
                }
                else
                {
                    //Edit
                    if (ViewState["psNo"] != null)
                    {
                        objSevBook.PSNO = Convert.ToInt32(ViewState["psNo"].ToString());
                        CustomStatus cs = (CustomStatus)objServiceBook.UpdatePreService(objSevBook);
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            objServiceBook.update_upload("PREVIOUS_SERVICE", objSevBook.PSNO, ViewState["attachment"].ToString(), _idnoEmp, "PRE_", flupld);
                            ViewState["action"] = "add";
                            this.Clear();
                            this.BindListViewPreService();
                            this.objCommon.DisplayMessage(updpersonaldetails, "Record Updated Successfully", this.Page);
                        }
                    }
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_PreviousService.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }


    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int psNo = int.Parse(btnEdit.CommandArgument);
            ShowDetails(psNo);
            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_PreviousService.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void txtToDate_TextChanged(object sender, EventArgs e)
    {
        if (txtToDate.Text != string.Empty && txtToDate.Text != "__/__/____" && txtFromDate.Text != string.Empty && txtFromDate.Text != "__/__/____")
        {
            DateTime DtFrom = Convert.ToDateTime(txtFromDate.Text);
            DateTime DtTo = Convert.ToDateTime(txtToDate.Text);
            DataSet ds = objServiceBook.GetTotExperience(DtFrom, DtTo);
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtExperience.Text = ds.Tables[0].Rows[0]["Total_experience"].ToString();

            }
        }
    }
    protected void txtFromDate_TextChanged(object sender, EventArgs e)
    {
        if (txtToDate.Text != string.Empty && txtToDate.Text != "__/__/____" && txtFromDate.Text != string.Empty && txtFromDate.Text != "__/__/____")
        {
            DateTime DtFrom = Convert.ToDateTime(txtFromDate.Text);
            DateTime DtTo = Convert.ToDateTime(txtToDate.Text);
            DataSet ds = objServiceBook.GetTotExperience(DtFrom, DtTo);
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtExperience.Text = ds.Tables[0].Rows[0]["Total_experience"].ToString();

            }
        }
    }
    private void ShowDetails(int psNo)
    {
        DataSet ds = null;
        try
        {
            ds = objServiceBook.GetSinglePreServiceDetailsOfEmployee(psNo);
            //To show created user details 
            if (ds.Tables[0].Rows.Count > 0)
            {

                ViewState["psNo"] = psNo.ToString();
                txtFromDate.Text = ds.Tables[0].Rows[0]["fdt"].ToString();
                txtToDate.Text = ds.Tables[0].Rows[0]["tdt"].ToString();
                txtReMarks.Text = ds.Tables[0].Rows[0]["remark"].ToString();
                txtDesignationOfAttestingOfficer.Text = ds.Tables[0].Rows[0]["officer"].ToString();
                txtInstitute.Text = ds.Tables[0].Rows[0]["inst"].ToString();
                txtPostName.Text = ds.Tables[0].Rows[0]["POST"].ToString();
                txtReasonForTerminationOfService.Text = ds.Tables[0].Rows[0]["termination"].ToString();
                txtExperience.Text = ds.Tables[0].Rows[0]["EXPERIENCE"].ToString();

                ViewState["attachment"] = ds.Tables[0].Rows[0]["ATTACHMENT"].ToString();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_PreviousService.ShowDetails-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
        finally
        {
            ds.Clear();
            ds.Dispose();
        }

    }

    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnDel = sender as ImageButton;
            int psNo = int.Parse(btnDel.CommandArgument);
            CustomStatus cs = (CustomStatus)objServiceBook.DeletePreService(psNo);
            if (cs.Equals(CustomStatus.RecordDeleted))
            {
                Clear();
                BindListViewPreService();
                ViewState["action"] = "add";
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_PreviousService.btnDelete_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }

    private void Clear()
    {
        txtReMarks.Text = string.Empty;               
        txtToDate.Text = string.Empty;
        txtFromDate.Text = string.Empty;
        txtDesignationOfAttestingOfficer.Text = string.Empty;
        txtInstitute.Text = string.Empty;
        txtReasonForTerminationOfService.Text = string.Empty;
        ViewState["action"] = "add";
    }

    public string GetFileNamePath(object filename, object PSNO, object idno)
    {
        string[] extension = filename.ToString().Split('.');
        if (filename != null && filename.ToString() != string.Empty)
            return ("~/ESTABLISHMENT/upload_files/PREVIOUS_SERVICE/" + idno.ToString() + "/PRE_" + PSNO + "." + extension[1].ToString().Trim());
        else
            return "";
    }

}
