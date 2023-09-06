//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : PAY ROLL
// PAGE NAME     : Pay_Admin_Responsibilities.ascx                                                
// CREATION DATE : 23-FEB-2010                                                     
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

public partial class PAYROLL_TRANSACTIONS_Pay_Admin_Responsibilities : System.Web.UI.UserControl
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
                //CheckPageAuthorization();
            }
            //By default setting ViewState["action"] to add
            ViewState["action"] = "add";
        }
        DropDownList ddlempidno = (DropDownList)this.Parent.FindControl("ddlEmployee");
        _idnoEmp = Convert.ToInt32(ddlempidno.SelectedValue);
        BindListViewAdminResponsiblities();

    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Pay_Admin_Responsibilities.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Pay_Admin_Responsibilities.aspx");
        }
    }

    private void BindListViewAdminResponsiblities()
    {
        try
        {
            DataSet ds = objServiceBook.GetAllAdminResponsibilities(_idnoEmp);
            lvAdminResponsibilities.DataSource = ds;
            lvAdminResponsibilities.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Pay_Admin_Responsibilities.BindListViewAdminResponsiblities-> " + ex.Message + " " + ex.StackTrace);
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
            objSevBook.RESPONSIBILITY = txtResponsibility.Text;
            objSevBook.ORGANIZATION = txtOrganization.Text;
            objSevBook.FROMDATE = Convert.ToDateTime(txtFromDate.Text);
            objSevBook.TODATE= Convert.ToDateTime(txtToDate.Text);
            objSevBook.REMARK = txtReMarks.Text;
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
                    CustomStatus cs = (CustomStatus)objServiceBook.AddAdminResponsibilities(objSevBook);

                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        objServiceBook.upload_new_files("ADMIN_RESPONSIBLITY", _idnoEmp, "ADMINTRXNO", "PAYROLL_SB_ADMIN_RESPONSIBILITIES", "ADM_", flupld);
                        this.Clear();
                        this.BindListViewAdminResponsiblities();
                        this.objCommon.DisplayMessage(updpersonaldetails, "Record Saved Successfully", this.Page);
                    }
                }
                else
                {
                    //Edit
                    if (ViewState["ADMINTRXNO"] != null)
                    {
                        objSevBook.ADMINTRXNO = Convert.ToInt32(ViewState["ADMINTRXNO"].ToString());
                        CustomStatus cs = (CustomStatus)objServiceBook.UpdateAdminResponsibilities(objSevBook);
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            objServiceBook.update_upload("ADMIN_RESPONSIBLITY",Convert.ToInt32(objSevBook.ADMINTRXNO), ViewState["attachment"].ToString(), _idnoEmp, "ADM_", flupld);
                            ViewState["action"] = "add";
                            this.Clear();
                            this.BindListViewAdminResponsiblities();
                            this.objCommon.DisplayMessage(updpersonaldetails, "Record Updated Successfully", this.Page);
                        }
                    }
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Pay_Admin_Responsibilities.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }


    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int ADMINTRXNO = int.Parse(btnEdit.CommandArgument);
            ShowDetails(ADMINTRXNO);
            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Pay_Admin_Responsibilities.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetails(int ADMINTRXNO)
    {
        DataSet ds = null;
        try
        {
            ds = objServiceBook.GetSingleAdminResponsibilities(ADMINTRXNO);
            //To show created user details 
            if (ds.Tables[0].Rows.Count > 0)
            {

                ViewState["ADMINTRXNO"] = ADMINTRXNO.ToString();
                txtResponsibility.Text = ds.Tables[0].Rows[0]["Responsibility"].ToString();
                txtOrganization.Text = ds.Tables[0].Rows[0]["ORGANIZATION"].ToString();
                txtFromDate.Text = ds.Tables[0].Rows[0]["FROMDATE"].ToString();
                txtToDate.Text = ds.Tables[0].Rows[0]["TODATE"].ToString();
                txtReMarks.Text = ds.Tables[0].Rows[0]["remark"].ToString();
                ViewState["attachment"] = ds.Tables[0].Rows[0]["ATTACHMENT"].ToString();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Pay_Admin_Responsibilities.ShowDetails-> " + ex.Message + " " + ex.StackTrace);
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
            int ADMINTRXNO = int.Parse(btnDel.CommandArgument);
            CustomStatus cs = (CustomStatus)objServiceBook.DeleteAdminResponsibilities(ADMINTRXNO);
            if (cs.Equals(CustomStatus.RecordDeleted))
            {
                BindListViewAdminResponsiblities();
                ViewState["action"] = "add";
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Pay_Admin_Responsibilities.btnDelete_Click-> " + ex.Message + " " + ex.StackTrace);
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
        txtResponsibility.Text = string.Empty;
        txtOrganization.Text = string.Empty;
        txtReMarks.Text = string.Empty;
        txtToDate.Text = string.Empty;
        txtFromDate.Text = string.Empty;
        ViewState["action"] = "add";
    }

    public string GetFileNamePath(object filename, object ADMINTRXNO, object idno)
    {
        string[] extension = filename.ToString().Split('.');
        if (filename != null && filename.ToString() != string.Empty)
            return ("~/ESTABLISHMENT/upload_files/ADMIN_RESPONSIBLITY/" + idno.ToString() + "/ADM_" + ADMINTRXNO + "." + extension[1].ToString().Trim());
        else
            return "";
    }
}
