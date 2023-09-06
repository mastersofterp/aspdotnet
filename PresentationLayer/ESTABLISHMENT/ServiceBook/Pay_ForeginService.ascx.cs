//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : PAY ROLL
// PAGE NAME     : Pay_ForeginService.ascx                                                
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

public partial class PayRoll_Pay_ForeginService : System.Web.UI.UserControl
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

        BindListViewForeginService();

    }
    
    private void BindListViewForeginService()
    {
        try
        {
            DataSet ds = objServiceBook.GetAllForServiceDetailsOfEmployee(_idnoEmp);
            lvForeginService.DataSource = ds;
            lvForeginService.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_ForeginService.BindListViewForeginService-> " + ex.Message + " " + ex.StackTrace);
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
            objSevBook.REMARK = txtRemarks.Text;
            objSevBook.POSTNAME = txtPostAndNameOfTheEmployer.Text;
            objSevBook.LSC = txtPensionContributionPayable.Text;
            objSevBook.LSCR = txtPensionContributionPayableCreditparticulars.Text;
            objSevBook.COLLEGE_CODE = Session["colcode"].ToString();
            objSevBook.ATTACHMENTS = Convert.ToString(flupld.PostedFile.FileName.ToString());
            //Check whether to add or update
            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    //Add New Help
                    CustomStatus cs = (CustomStatus)objServiceBook.AddForService(objSevBook);

                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        objServiceBook.upload_new_files("FOREIGN_SERVICE", _idnoEmp, "FSNO", "PAYROLL_SB_FORSERVICE", "FOR_", flupld);
                        this.Clear();
                        this.BindListViewForeginService();
                        this.objCommon.DisplayMessage(updpersonaldetails,"Record Saved Successfully", this.Page);
                    }
                }
                else
                {
                    //Edit
                    if (ViewState["fsNo"] != null)
                    {
                        objSevBook.FSNO = Convert.ToInt32(ViewState["fsNo"].ToString());
                        CustomStatus cs = (CustomStatus)objServiceBook.UpdateForService(objSevBook);
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            objServiceBook.update_upload("FOREIGN_SERVICE", objSevBook.FSNO, ViewState["attachment"].ToString(), _idnoEmp, "FOR_", flupld);
                            ViewState["action"] = "add";
                            this.Clear();
                            this.BindListViewForeginService();
                            this.objCommon.DisplayMessage(updpersonaldetails, "Record Updated Successfully", this.Page);
                        }
                    }
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_ForeginService.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }


    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int fsNo = int.Parse(btnEdit.CommandArgument);
            ShowDetails(fsNo);
            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_ForeginService.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetails(int fsNo)
    {
        DataSet ds = null;
        try
        {
            ds = objServiceBook.GetSingleForServiceDetailsOfEmployee(fsNo);
            //To show created user details 
            if (ds.Tables[0].Rows.Count > 0)
            {
                //Insert into sb_forservice (fsno,idno,fdt,tdt,postname,lsc,lscr,REAMRK)
                ViewState["fsNo"] = fsNo.ToString();
                txtFromDate.Text = ds.Tables[0].Rows[0]["fdt"].ToString();
                txtToDate.Text = ds.Tables[0].Rows[0]["tdt"].ToString();
                txtRemarks.Text = ds.Tables[0].Rows[0]["remark"].ToString();
                txtPostAndNameOfTheEmployer.Text = ds.Tables[0].Rows[0]["postname"].ToString();
                txtPensionContributionPayable.Text = ds.Tables[0].Rows[0]["lsc"].ToString();
                txtPensionContributionPayableCreditparticulars.Text= ds.Tables[0].Rows[0]["lscr"].ToString();
                ViewState["attachment"] = ds.Tables[0].Rows[0]["ATTACHMENTS"].ToString();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_ForeginService.ShowDetails-> " + ex.Message + " " + ex.StackTrace);
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
            int fsNo = int.Parse(btnDel.CommandArgument);
            CustomStatus cs = (CustomStatus)objServiceBook.DeleteForService(fsNo);
            if (cs.Equals(CustomStatus.RecordDeleted))
            {
                Clear();
                BindListViewForeginService();
                ViewState["action"] = "add";
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_ForeginService.btnDelete_Click-> " + ex.Message + " " + ex.StackTrace);
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
        txtFromDate.Text = string.Empty;
        txtToDate.Text = string.Empty;
        txtRemarks.Text = string.Empty;
        txtPostAndNameOfTheEmployer.Text = string.Empty;
        txtPensionContributionPayable.Text = string.Empty;
        txtPensionContributionPayableCreditparticulars.Text = string.Empty;
        ViewState["action"] = "add";
    }

    public string GetFileNamePath(object filename, object FSNO, object idno)
    {
        string[] extension = filename.ToString().Split('.');
        if (filename != null && filename.ToString() != string.Empty)
            return ("~/ESTABLISHMENT/upload_files/FOREIGN_SERVICE/" + idno.ToString() + "/FOR_" + FSNO + "." + extension[1].ToString().Trim());
        else
            return "";
    }
}
