//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : PAY ROLL
// PAGE NAME     : Pay_FamilyParticulars.ascx                                                
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

public partial class PayRoll_Pay_FamilyParticulars : System.Web.UI.UserControl
{
    //CREATING OBJECTS OF CLASS FILES COMMON,UAIMS_COMMON,PAYCONTROLLER
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ServiceBookController objServiceBook = new ServiceBookController();
    public int _idnoEmp;

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
                //CheckPageAuthorization();
            }
            //By default setting ViewState["action"] to add
            ViewState["action"] = "add";
        }

        DropDownList ddlempidno = (DropDownList)this.Parent.FindControl("ddlEmployee");       
        _idnoEmp = Convert.ToInt32(ddlempidno.SelectedValue);        
        BindListViewFamilyInfo();

    }
    
    private void BindListViewFamilyInfo()
    {
        try
        {  
            DataSet ds = objServiceBook.GetAllFamilyDetailsOfEmployee(_idnoEmp);
            lvFamilyInfo.DataSource = ds;
            lvFamilyInfo.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_FamilyParticulars.BindListViewFamilyInfo-> " + ex.Message + " " + ex.StackTrace);
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
            objSevBook.MEMNAME = txtFamilyMemberName.Text;
            objSevBook.ADDRESS = txtAddress.Text;
            objSevBook.RELATION = txtRelationShip.Text;
            objSevBook.AGE = Convert.ToInt32(txtAge.Text);
            objSevBook.DOB = Convert.ToDateTime(txtDateOfBirth.Text);
            objSevBook.OFFICER = txtOfficer.Text;
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
            if(!(Session["colcode"].ToString()==null))objSevBook.COLLEGE_CODE = Session["colcode"].ToString();
            //Check whether to add or update
            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    //Add New Help
                    CustomStatus cs = (CustomStatus)objServiceBook.AddFamilyInfo(objSevBook);

                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        objServiceBook.upload_new_files("FAMILY_INFO", _idnoEmp, "FNNO", "PAYROLL_SB_FAMILYINFO", "FAI_", flupld);
                        //lblFamilymsg.Text = "Record Saved Successfully";
                        this.Clear();
                        this.BindListViewFamilyInfo();
                        this.objCommon.DisplayMessage(updpersonaldetails, "Record Saved Successfully", this.Page);
                    }
                }
                else
                {
                    //Edit
                    if (ViewState["fnNo"] != null)
                    {
                        objSevBook.FNNO = Convert.ToInt32(ViewState["fnNo"].ToString());
                        CustomStatus cs = (CustomStatus)objServiceBook.UpdateFamilyInfo(objSevBook);
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            objServiceBook.update_upload("FAMILY_INFO", objSevBook.FNNO, ViewState["attachment"].ToString(), _idnoEmp, "FAI_", flupld);
                            ViewState["action"] = "add";
                            //lblFamilymsg.Text = "Record Updated Successfully";
                            this.Clear();
                            this.BindListViewFamilyInfo();
                            this.objCommon.DisplayMessage(updpersonaldetails, "Record Updated Successfully", this.Page);
                        }
                    }
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_FamilyParticulars.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }


    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
           // lblFamilymsg.Text = string.Empty;
            ImageButton btnEdit = sender as ImageButton;
            int fnNo = int.Parse(btnEdit.CommandArgument);
            ShowDetails(fnNo);
            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_FamilyParticulars.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetails(int fnNo)
    {
        DataSet ds = null;
        try
        {
            ds = objServiceBook.GetSingleFamilyDetailsOfEmployee(fnNo);
            //To show employee family details 
            if (ds.Tables[0].Rows.Count > 0)
            {
               
                ViewState["fnNo"] = fnNo.ToString();
                txtFamilyMemberName.Text = ds.Tables[0].Rows[0]["memname"].ToString();
                txtRelationShip.Text = ds.Tables[0].Rows[0]["relation"].ToString();
                txtAge.Text = ds.Tables[0].Rows[0]["age"].ToString();
                txtDateOfBirth.Text = ds.Tables[0].Rows[0]["dob"].ToString();
                txtOfficer.Text = ds.Tables[0].Rows[0]["officer"].ToString();
                txtAddress.Text = ds.Tables[0].Rows[0]["Address"].ToString();
                ViewState["attachment"] = ds.Tables[0].Rows[0]["ATTACHMENT"].ToString();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_FamilyParticulars.ShowDetails-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
      

    }

    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //lblFamilymsg.Text = string.Empty;
            ImageButton btnDel = sender as ImageButton;
            int fnNo = int.Parse(btnDel.CommandArgument);
            CustomStatus cs = (CustomStatus)objServiceBook.DeleteFamilyInfo(fnNo);
            if (cs.Equals(CustomStatus.RecordDeleted))
            {
                BindListViewFamilyInfo();
                //lblFamilymsg.Text = "Record Deleted Successfully";
                ViewState["action"] = "add";
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_FamilyParticulars.btnDelete_Click-> " + ex.Message + " " + ex.StackTrace);
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
       // lblFamilymsg.Text = string.Empty;
        txtAddress.Text = string.Empty;
        txtAge.Text = string.Empty;
        txtDateOfBirth.Text = string.Empty;
        txtFamilyMemberName.Text = string.Empty;
        txtOfficer.Text = string.Empty;
        txtRelationShip.Text = string.Empty;
        ViewState["action"] = "add";
    }

    public string GetFileNamePath(object filename, object FNNO, object idno)
    {
        string[] extension = filename.ToString().Split('.');
        if (filename != null && filename.ToString() != string.Empty)
            return ("~/ESTABLISHMENT/upload_files/FAMILY_INFO/" + idno.ToString() + "/FAI_" + FNNO + "." + extension[1].ToString().Trim());
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
