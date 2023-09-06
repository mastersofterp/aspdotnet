//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : PAY ROLL
// PAGE NAME     : Pay_LoansAndAdvance.ascx                                                
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

public partial class PayRoll_Pay_LoansAndAdvance : System.Web.UI.UserControl
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
        if (user_type != 1)
        {
            btnSubmit.Visible = false; btnCancel.Visible = false;
            btnSubmit.Enabled = false;
            ddlLoanName.Enabled = false;
            txtLoanDate.Enabled = false;
            txtReMarks.Enabled = false;
            txtAmount.Enabled = false;
            txtOrderNo.Enabled = false;
            txtRateOfInterest.Enabled = false;
            txtNoOfInstallMent.Enabled = false;
        }
        DropDownList ddlempidno = (DropDownList)this.Parent.FindControl("ddlEmployee");
        _idnoEmp = Convert.ToInt32(ddlempidno.SelectedValue);
        BindListViewLoansAndAdvance();

    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Pay_LoansAndAdvance.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Pay_LoansAndAdvance.aspx");
        }
    }

    private void BindListViewLoansAndAdvance()
    {
        try
        {
            DataSet ds = objServiceBook.GetAllLoanDetailsOfEmployee(_idnoEmp);
            lvLoanAndAdvance.DataSource = ds;
            lvLoanAndAdvance.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_LoansAndAdvance.BindListViewLoansAndAdvance-> " + ex.Message + " " + ex.StackTrace);
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
            objSevBook.LOANDT = Convert.ToDateTime(txtLoanDate.Text);
            objSevBook.REMARK = txtReMarks.Text;
            objSevBook.LOANNO = Convert.ToInt32(ddlLoanName.Text);
            objSevBook.ORDERNO = txtOrderNo.Text;
            objSevBook.AMOUNT = Convert.ToDecimal(txtAmount.Text);
            objSevBook.INTEREST = Convert.ToDecimal(txtRateOfInterest.Text);
            objSevBook.INSTAL = Convert.ToDecimal(txtNoOfInstallMent.Text);
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
                    CustomStatus cs = (CustomStatus)objServiceBook.AddLoan(objSevBook);

                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        objServiceBook.upload_new_files("LOAN_N_ADVANCE", _idnoEmp, "LNO", "PAYROLL_SB_LOAN", "LNA_", flupld);
                        this.Clear();
                        this.BindListViewLoansAndAdvance();
                        this.objCommon.DisplayMessage(updpersonaldetails, "Record Saved Successfully", this.Page);
                    }
                }
                else
                {
                    //Edit
                    if (ViewState["lNo"] != null)
                    {
                        objSevBook.LNO = Convert.ToInt32(ViewState["lNo"].ToString());
                        CustomStatus cs = (CustomStatus)objServiceBook.UpdateLoan(objSevBook);
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            objServiceBook.update_upload("LOAN_N_ADVANCE", objSevBook.LNO, ViewState["attachment"].ToString(), _idnoEmp, "LNA_", flupld);
                            ViewState["action"] = "add";
                            this.Clear();
                            this.BindListViewLoansAndAdvance();
                            this.objCommon.DisplayMessage(updpersonaldetails, "Record Updated Successfully", this.Page);
                        }
                    }
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_LoansAndAdvance.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }


    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int lNo = int.Parse(btnEdit.CommandArgument);
            ShowDetails(lNo);
            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_LoansAndAdvance.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetails(int lNo)
    {
        DataSet ds = null;
        try
        {
            ds = objServiceBook.GetSingleLoanDetailsOfEmployee(lNo);
            //To show created user details 
            if (ds.Tables[0].Rows.Count > 0)
            {
                //lno,idno,loanno,orderno,amount,interest,instal,remark,loandt
                ViewState["lNo"] = lNo.ToString();
                ddlLoanName.SelectedValue = ds.Tables[0].Rows[0]["loanno"].ToString();
                txtLoanDate.Text = ds.Tables[0].Rows[0]["loandt"].ToString();                
                txtReMarks.Text = ds.Tables[0].Rows[0]["remark"].ToString();
                txtAmount.Text = ds.Tables[0].Rows[0]["amount"].ToString();
                txtOrderNo.Text = ds.Tables[0].Rows[0]["orderno"].ToString();
                txtRateOfInterest.Text = ds.Tables[0].Rows[0]["interest"].ToString();
                txtNoOfInstallMent.Text = ds.Tables[0].Rows[0]["instal"].ToString();
                ViewState["attachment"] = ds.Tables[0].Rows[0]["ATTACHMENT"].ToString();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_LoansAndAdvance.ShowDetails-> " + ex.Message + " " + ex.StackTrace);
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
            int lNo = int.Parse(btnDel.CommandArgument);
            CustomStatus cs = (CustomStatus)objServiceBook.DeleteLoan(lNo);
            if (cs.Equals(CustomStatus.RecordDeleted))
            {
                Clear();
                BindListViewLoansAndAdvance();
                ViewState["action"] = "add";
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_LoansAndAdvance.btnDelete_Click-> " + ex.Message + " " + ex.StackTrace);
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
        ddlLoanName.SelectedValue = "0";
        txtLoanDate.Text = string.Empty;
        txtReMarks.Text = string.Empty;
        txtAmount.Text = string.Empty;
        txtOrderNo.Text = string.Empty;
        txtRateOfInterest.Text = string.Empty;
        txtNoOfInstallMent.Text = string.Empty;
        ViewState["action"] = "add";
    }

    private void FillDropDown()
    {
        try
        {
            objCommon.FillDropDownList(ddlLoanName, "payroll_LoanType", "LOANNO", "LOANNAME", "LOANNO >  0", "LOANNO");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_LoansAndAdvance.FillDropDown-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    
    }

    public string GetFileNamePath(object filename, object LNO, object idno)
    {
        string[] extension = filename.ToString().Split('.');
        if (filename != null && filename.ToString() != string.Empty)
            return ("~/ESTABLISHMENT/upload_files/LOAN_N_ADVANCE/" + idno.ToString() + "/LNA_" + LNO + "." + extension[1].ToString().Trim());
        else
            return "";
    }
}
