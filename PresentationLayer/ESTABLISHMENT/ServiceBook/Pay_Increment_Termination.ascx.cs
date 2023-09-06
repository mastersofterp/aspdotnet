//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : PAY ROLL
// PAGE NAME     : Pay_Increment_Termination.ascx                                                
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

public partial class PayRoll_Pay_Increment_Termination : System.Web.UI.UserControl
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

            FillDropDown();
        }

        DropDownList ddlempidno = (DropDownList)this.Parent.FindControl("ddlEmployee");
        _idnoEmp = Convert.ToInt32(ddlempidno.SelectedValue);

        BindListViewServiceBook();
        

    }

    private void BindListViewServiceBook()
    {
        try
        {
            DataSet ds = objServiceBook.GetAllServiceBookDetailsOfEmployee(_idnoEmp);
            lvServiceBook.DataSource = ds;
            lvServiceBook.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Increment_Termination.BindListViewServiceBook-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            Panel updpersonaldetails = (Panel)this.Parent.FindControl("upWebUserControl");
            //Trno,ETrno,Idno,TypeTranNo,SubDesigno,SubDeptno,Scaleno,OrderNo,GrNo,Remark,PayAllow,OrderDt,GrDt,TermiDt,OrdEffDt,Seqno
            ServiceBook objSevBook = new ServiceBook();
            objSevBook.IDNO = _idnoEmp;
            objSevBook.TYPETRANNO = Convert.ToInt32(ddlTransactionType.SelectedValue);
            objSevBook.SUBDESIGNO=Convert.ToInt32(ddlDesignation.SelectedValue);
            objSevBook.SUBDEPTNO = Convert.ToInt32(ddlDepartment.SelectedValue);
            if (ddlScale.SelectedIndex > 0)
            {
                objSevBook.SCALENO = Convert.ToInt32(ddlScale.SelectedValue);
            }
            else
            {
                objSevBook.SCALENO = 0;
            }
            objSevBook.ORDERNO=txtOredrNo.Text;
            objSevBook.GRNO=txtGrNo.Text;
            objSevBook.REMARK=txtRemarks.Text;
            if (txtPayAllowance.Text == string.Empty)
            {
                objSevBook.PAYALLOW = 0;
            }
            else
            {
                objSevBook.PAYALLOW = Convert.ToDecimal(txtPayAllowance.Text);
            }
            objSevBook.ORDERDT = txtOrderDate.Text.Trim().Equals(string.Empty) ? DateTime.MinValue : Convert.ToDateTime(txtOrderDate.Text.Trim());
 
          //  objSevBook.GRDT=Convert.ToDateTime(txtGrDate.Text);
            //objSevBook.TERMIDT=Convert.ToDateTime(txtTerRet.Text);
            objSevBook.TERMIDT = txtTerRet.Text.Trim().Equals(string.Empty) ? DateTime.MinValue : Convert.ToDateTime(txtTerRet.Text.Trim());
 
           // objSevBook.ORDEFFDT=Convert.ToDateTime(txtOrderEffectiveDate.Text);
            objSevBook.ORDEFFDT = txtOrderEffectiveDate.Text.Trim().Equals(string.Empty) ? DateTime.MinValue : Convert.ToDateTime(txtOrderEffectiveDate.Text.Trim());
            if (txtSqNo.Text == string.Empty)
            {
                objSevBook.SEQNO = 0;
            }
            else
            {
                objSevBook.SEQNO = Convert.ToInt32(txtSqNo.Text);
            }
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
                    CustomStatus cs = (CustomStatus)objServiceBook.AddServiceBk(objSevBook);

                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        objServiceBook.upload_new_files("INCREMENT_N_TERMINATION", _idnoEmp, "TRNO", "PAYROLL_SB_SERVICEBK", "INT_", flupld);
                        ViewState["action"] = "add";
                        this.Clear();
                        this.BindListViewServiceBook();
                        this.objCommon.DisplayMessage(updpersonaldetails, "Record Saved Successfully", this.Page);
                    }
                }
                else
                {
                    //Edit
                    if (ViewState["Trno"] != null)
                    {
                        objSevBook.TRNO = Convert.ToInt32(ViewState["Trno"].ToString());
                        CustomStatus cs = (CustomStatus)objServiceBook.UpdateServiceBk(objSevBook);
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            objServiceBook.update_upload("INCREMENT_N_TERMINATION",Convert.ToInt32(objSevBook.TRNO), ViewState["attachment"].ToString(), _idnoEmp, "INT_", flupld);
                            ViewState["action"] = "add";
                            this.Clear();
                            this.BindListViewServiceBook();
                            this.objCommon.DisplayMessage(updpersonaldetails, "Record Updated Successfully", this.Page);
                        }
                    }
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Increment_Termination.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }


    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int Trno = int.Parse(btnEdit.CommandArgument);
            ShowDetails(Trno);
            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Increment_Termination.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetails(int Trno)
    {
        DataSet ds = null;
        try
        {
            ds = objServiceBook.GetSingleServiceBookDetailsOfEmployee(Trno);
            //To show created user details 
            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["Trno"] = Trno.ToString();
                ddlTransactionType.SelectedValue = ds.Tables[0].Rows[0]["TypeTranNo"].ToString();
                ddlDesignation.SelectedValue = ds.Tables[0].Rows[0]["SubDesigno"].ToString();
                ddlDepartment.SelectedValue = ds.Tables[0].Rows[0]["SubDeptno"].ToString();
                ddlScale.SelectedValue = ds.Tables[0].Rows[0]["Scaleno"].ToString();
                txtOredrNo.Text = ds.Tables[0].Rows[0]["OrderNo"].ToString();
                //txtGrNo.Text = ds.Tables[0].Rows[0]["GrNo"].ToString(); 
                txtRemarks.Text = ds.Tables[0].Rows[0]["Remark"].ToString();
                txtPayAllowance.Text = ds.Tables[0].Rows[0]["PayAllow"].ToString();
                txtOrderDate.Text = ds.Tables[0].Rows[0]["OrderDt"].ToString();
                //txtGrDate.Text = ds.Tables[0].Rows[0]["GrDt"].ToString(); 
                txtTerRet.Text = ds.Tables[0].Rows[0]["TermiDt"].ToString();
                txtOrderEffectiveDate.Text = ds.Tables[0].Rows[0]["OrdEffDt"].ToString();
                txtSqNo.Text = ds.Tables[0].Rows[0]["Seqno"].ToString();
                ViewState["attachment"] = ds.Tables[0].Rows[0]["ATTACHMENT"].ToString();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Increment_Termination.ShowDetails-> " + ex.Message + " " + ex.StackTrace);
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
            int Trno = int.Parse(btnDel.CommandArgument);
            CustomStatus cs = (CustomStatus)objServiceBook.DeleteServiceBk(Trno);
            if (cs.Equals(CustomStatus.RecordDeleted))
            {
                Clear();
                BindListViewServiceBook();
                ViewState["action"] = "add";
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Increment_Termination.btnDelete_Click-> " + ex.Message + " " + ex.StackTrace);
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
        ddlTransactionType.SelectedValue = "0";
        ddlDesignation.SelectedValue = "0";
        ddlDepartment.SelectedValue = "0";
        ddlScale.SelectedValue = "0";
        txtOredrNo.Text = string.Empty;
        txtGrNo.Text = string.Empty;
        txtRemarks.Text = string.Empty;
        txtPayAllowance.Text = string.Empty;
        txtOrderDate.Text = string.Empty;
        txtGrDate.Text = string.Empty;
        txtTerRet.Text = string.Empty;
        txtOrderEffectiveDate.Text = string.Empty;
        txtSqNo.Text = string.Empty;
        ViewState["action"] = "add";
    }
    
    private void FillDropDown()
    {
        try  
        {
           
            objCommon.FillDropDownList(ddlDepartment, "payroll_SubDept", "subdeptno", "subdept", "subdeptno > 0", "subdeptno");
            objCommon.FillDropDownList(ddlTransactionType, "payroll_TypeTran", "typetranno", "typetran", "typetranno > 0", "typetranno");
            objCommon.FillDropDownList(ddlDesignation, "payroll_SubDesig", "subdesigno", "subdesig", "subdesigno > 0", "subdesigno");
            objCommon.FillDropDownList(ddlScale, "payroll_scale", "scaleno", "scale", "scaleno > 0", "scaleno");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Increment_Termination.FillDropDown-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }



    }

    public string GetFileNamePath(object filename, object TRNO, object idno)
    {
        string[] extension = filename.ToString().Split('.');
        if (filename != null && filename.ToString() != string.Empty)
            return ("~/ESTABLISHMENT/upload_files/INCREMENT_N_TERMINATION/" + idno.ToString() + "/INT_" + TRNO + "." + extension[1].ToString().Trim());
        else
            return "";
    }

}
