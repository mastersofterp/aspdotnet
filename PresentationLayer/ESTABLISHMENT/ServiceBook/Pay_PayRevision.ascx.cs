//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : PAY ROLL
// PAGE NAME     : PayRevision.ascx                                                
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

public partial class PayRoll_Pay_PayRevision : System.Web.UI.UserControl
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

            FillDropDown();
        }
        int user_type = 0;
        user_type = Convert.ToInt32(Session["usertype"].ToString());
        if (user_type != 1 )
        {
            btnSubmit.Visible = false; btnCancel.Visible = false;
            btnSubmit.Enabled = false;
            ddlDesignation.Enabled = false;
            ddlScale.Enabled = false;
            txtToDate.Enabled = false;
            txtFromDate.Enabled = false;
            txtRemarks.Enabled = false;
            rdoPayrevision.Enabled = false;
        }
        else
        {
            btnSubmit.Visible = true; btnCancel.Visible = true;
        }
        
        DropDownList ddlempidno = (DropDownList)this.Parent.FindControl("ddlEmployee");
        _idnoEmp = Convert.ToInt32(ddlempidno.SelectedValue);

        BindListViewPayRevision();
        

    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Pay_PayRevision.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Pay_PayRevision.aspx");
        }
    }

    private void BindListViewPayRevision()
    {
        try
        {   
            DataSet ds = objServiceBook.GetAllPayRevisionOfEmployee(_idnoEmp);
            lvPayRevision.DataSource = ds;
            lvPayRevision.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_PayRevision.BindListViewPayRevision-> " + ex.Message + " " + ex.StackTrace);
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
            objSevBook.SUBDESIGNO = Convert.ToInt32(ddlDesignation.SelectedValue);
            objSevBook.SCALENO = Convert.ToInt32(ddlScale.SelectedValue);
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
            objSevBook.COLLEGE_CODE = Session["colcode"].ToString();
            if (rdoPromotion.Checked)
               objSevBook.TYPE = "PA";
            else
               objSevBook.TYPE = "PR";

            if (txtAmount.Text != string.Empty)
            {
                objSevBook.AMOUNT = Convert.ToDecimal(txtAmount.Text);
            }
            else
            {
                objSevBook.AMOUNT = 0;
            }
            //Check whether to add or update
            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    //Add New Help
                    CustomStatus cs = (CustomStatus)objServiceBook.AddPayRevision(objSevBook);

                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        objServiceBook.upload_new_files("PAY_REVISION", _idnoEmp, "PRNO", "PAYROLL_SB_PAYREV", "PAY_", flupld);
                        this.Clear();
                        this.BindListViewPayRevision();
                        this.objCommon.DisplayMessage(updpersonaldetails, "Record Saved Successfully", this.Page);
                    }
                }
                else
                {
                    //Edit
                    if (ViewState["prNo"] != null)
                    {
                        objSevBook.PRNO = Convert.ToInt32(ViewState["prNo"].ToString());
                        CustomStatus cs = (CustomStatus)objServiceBook.UpdatePayRevision(objSevBook);
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            objServiceBook.update_upload("PAY_REVISION", objSevBook.PRNO, ViewState["attachment"].ToString(), _idnoEmp, "PAY_", flupld);
                            ViewState["action"] = "add";
                            this.Clear();
                            this.BindListViewPayRevision();
                            this.objCommon.DisplayMessage(updpersonaldetails, "Record Updated Successfully", this.Page);
                        }
                    }
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_PayRevision.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }


    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int prNo = int.Parse(btnEdit.CommandArgument);
            ShowDetails(prNo);
            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_PayRevision.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetails(int prNo)
    {
        DataSet ds = null;
        try
        {
            ds = objServiceBook.GetSinglePayRevisionOfEmployee(prNo);
            //To show created user details 
            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["prNo"] = prNo.ToString();
                txtFromDate.Text = ds.Tables[0].Rows[0]["fdt"].ToString();
                txtToDate.Text = ds.Tables[0].Rows[0]["tdt"].ToString();
                txtRemarks.Text = ds.Tables[0].Rows[0]["remark"].ToString();
                ddlScale.SelectedValue = ds.Tables[0].Rows[0]["Scaleno"].ToString();
                ddlDesignation.SelectedValue = ds.Tables[0].Rows[0]["SubDesigno"].ToString();
                if (ds.Tables[0].Rows[0]["Type"].ToString() == "PA")
                {
                    rdoPromotion.Checked = true;
                    rdoPayrevision.Checked = false;
                }
                else if (ds.Tables[0].Rows[0]["Type"].ToString() == "PR")
                {
                    rdoPromotion.Checked = false;
                    rdoPayrevision.Checked = true;
                }
                ViewState["attachment"] = ds.Tables[0].Rows[0]["ATTACHMENT"].ToString();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_PayRevision.ShowDetails-> " + ex.Message + " " + ex.StackTrace);
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
            int prNo = int.Parse(btnDel.CommandArgument);
            CustomStatus cs = (CustomStatus)objServiceBook.DeletePayRevision(prNo);
            if (cs.Equals(CustomStatus.RecordDeleted))
            {
                Clear();
                BindListViewPayRevision();
                ViewState["action"] = "add";
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_PayRevision.btnDelete_Click-> " + ex.Message + " " + ex.StackTrace);
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
        ddlDesignation.SelectedValue = "0";
        ddlScale.SelectedValue = "0";        
        txtToDate.Text = string.Empty;
        txtFromDate.Text = string.Empty;
        txtRemarks.Text = string.Empty;
        rdoPayrevision.Checked = true;
        ViewState["action"] = "add";
    }

    private void FillDropDown()
    {
        try
        {
            objCommon.FillDropDownList(ddlDesignation, "payroll_SubDesig", "subdesigno", "subdesig", "subdesigno > 0 ", "subdesigno");
            objCommon.FillDropDownList(ddlScale, "payroll_scale", "scaleno", "scale", "scaleno > 0", "scaleno");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_PayRevision.FillDropDown-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    public string GetFileNamePath(object filename, object PRNO, object idno)
    {
        string[] extension = filename.ToString().Split('.');
        if (filename != null && filename.ToString() != string.Empty)
            return ("~/ESTABLISHMENT/upload_files/PAY_REVISION/" + idno.ToString() + "/PAY_" + PRNO + "." + extension[1].ToString().Trim());
        else
            return "";
    }
}
