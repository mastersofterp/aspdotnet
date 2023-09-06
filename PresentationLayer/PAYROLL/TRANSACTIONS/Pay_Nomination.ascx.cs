//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : PAY ROLL
// PAGE NAME     : Pay_Nomination.ascx                                                
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

public partial class PayRoll_Pay_Nomination : System.Web.UI.UserControl
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
        BindListViewNominee();

    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Pay_Nomination.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Pay_Nomination.aspx");
        }
    }
    private void BindListViewNominee()
    {
        try
        {  
            DataSet ds = objServiceBook.GetAllNominiDetailsOfEmployee(_idnoEmp);
            lvNomination.DataSource = ds;
            lvNomination.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Nomination.BindListViewNominee-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {   //nfno,idno,ntno,name,relation,per,remark,srno,dob,last,Address,Conting,Age
            Panel updpersonaldetails = (Panel)this.Parent.FindControl("upWebUserControl");
            ServiceBook objSevBook = new ServiceBook();
            objSevBook.IDNO = _idnoEmp;
            objSevBook.NTNO = Convert.ToInt32(ddlNominationFor.SelectedValue);
            objSevBook.NAME = txtNameOfNominee.Text;
            objSevBook.RELATION = txtRelation.Text;
            objSevBook.PER = Convert.ToDecimal(txtPercentage.Text);
            objSevBook.REMARK = txtRemarks.Text;            
            objSevBook.DOB = Convert.ToDateTime(txtDateOfBirth.Text);
            bool lastNomini;             
            if(chkLastNominee.Checked)
                lastNomini=true;
            else
                lastNomini=false;

            objSevBook.LAST = lastNomini;
            objSevBook.ADDRESS = txtAddress.Text;
            objSevBook.CONTING = txtContingencies.Text;
            objSevBook.AGE = Convert.ToDecimal(txtAge.Text);
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
                    CustomStatus cs = (CustomStatus)objServiceBook.AddNominiFor(objSevBook);

                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        objServiceBook.upload_new_files("NOMINATION", _idnoEmp, "NFNO", "PAYROLL_SB_NOMINIFOR", "NOM_", flupld);
                        this.Clear();
                        this.BindListViewNominee();
                        this.objCommon.DisplayMessage(updpersonaldetails, "Record Saved Successfully", this.Page);
                        
                    }
                }
                else
                {
                    //Edit
                    if (ViewState["nfNo"] != null)
                    {
                        objSevBook.NFNO = Convert.ToInt32(ViewState["nfNo"].ToString());
                        CustomStatus cs = (CustomStatus)objServiceBook.UpdateNominiFor(objSevBook);
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            objServiceBook.update_upload("NOMINATION", objSevBook.NFNO, ViewState["attachment"].ToString(), _idnoEmp, "NOM_", flupld);
                            ViewState["action"] = "add";
                            this.Clear();
                            this.BindListViewNominee();
                            this.objCommon.DisplayMessage(updpersonaldetails,"Record Updated Successfully", this.Page);
                        }
                    }
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Nomination.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }


    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int nfNo = int.Parse(btnEdit.CommandArgument);
            ShowDetails(nfNo);
            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Nomination.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetails(int nfNo)
    {
        DataSet ds = null;
        try
        {
            ds = objServiceBook.GetSingleNominiDetailsOfEmployee(nfNo);
            //To show created user details 
            if (ds.Tables[0].Rows.Count > 0)
            {
                //nfno,idno,ntno,name,relation,per,remark,srno,dob,last,Address,Conting,Age
                ViewState["nfNo"] = nfNo.ToString();
                ddlNominationFor.SelectedValue = ds.Tables[0].Rows[0]["ntno"].ToString();
                txtAddress.Text = ds.Tables[0].Rows[0]["Address"].ToString();
                txtAge.Text = ds.Tables[0].Rows[0]["Age"].ToString();
                txtContingencies.Text = ds.Tables[0].Rows[0]["Conting"].ToString();
                txtDateOfBirth.Text = ds.Tables[0].Rows[0]["dob"].ToString();
                txtNameOfNominee.Text = ds.Tables[0].Rows[0]["name"].ToString();
                txtPercentage.Text = ds.Tables[0].Rows[0]["per"].ToString();
                txtRelation.Text = ds.Tables[0].Rows[0]["relation"].ToString();
                txtRemarks.Text = ds.Tables[0].Rows[0]["remark"].ToString();
                chkLastNominee.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["last"].ToString());
                ViewState["attachment"] = ds.Tables[0].Rows[0]["ATTACHMENT"].ToString();
                //if(Convert.ToBoolean(ds.Tables[0].Rows[0]["last"].ToString()))
                  //  chkLastNominee.Checked = true;
                //else
                  //  chkLastNominee.Checked = false;

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Nomination.ShowDetails-> " + ex.Message + " " + ex.StackTrace);
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
            int nfNo = int.Parse(btnDel.CommandArgument);
            CustomStatus cs = (CustomStatus)objServiceBook.DeleteNominifor(nfNo);
            if (cs.Equals(CustomStatus.RecordDeleted))
            {
                Clear();
                BindListViewNominee();
                ViewState["action"] = "add";
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Nomination.btnDelete_Click-> " + ex.Message + " " + ex.StackTrace);
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
        ddlNominationFor.SelectedValue = "0";
        txtAddress.Text = string.Empty;
        txtAge.Text = string.Empty;
        txtContingencies.Text = string.Empty;
        txtDateOfBirth.Text = string.Empty;
        txtNameOfNominee.Text = string.Empty;
        txtPercentage.Text = string.Empty;
        txtRelation.Text = string.Empty;
        txtRemarks.Text = string.Empty;
        chkLastNominee.Checked = false;
        ViewState["action"] = "add";
    }
    
    private void FillDropDown()
    {
        try
        {
            objCommon.FillDropDownList(ddlNominationFor, "Payroll_NominiType", "Ntno", "Nominitype", "Ntno > 0", "Ntno");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Nomination.FillDropDown-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    public string GetFileNamePath(object filename, object NFNO, object idno)
    {
        string[] extension = filename.ToString().Split('.');
        if (filename != null && filename.ToString() != string.Empty)
            return ("~/ESTABLISHMENT/upload_files/NOMINATION/" + idno.ToString() + "/NOM_" + NFNO + "." + extension[1].ToString().Trim());
        else
            return "";
    }
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }
    protected void txtDateOfBirth_TextChanged(object sender, EventArgs e)
    {
        if (Convert.ToDateTime(txtDateOfBirth.Text) > System.DateTime.Now)
        {
            MessageBox("Date Of Birth Should Not be greater Than Current Date");
            return;
        }
        var today = DateTime.Today;
        DateTime dateOfBirth;
        dateOfBirth = Convert.ToDateTime(txtDateOfBirth.Text);
        var a = (today.Year * 100 + today.Month) * 100 + today.Day;
        var b = (dateOfBirth.Year * 100 + dateOfBirth.Month) * 100 + dateOfBirth.Day;
        txtAge.Text = Convert.ToString((a - b) / 10000);
    }
}
