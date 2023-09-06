//======================================================================================
// PROJECT NAME  : NITPRM                                                                
// MODULE NAME   : PAY ROLL
// PAGE NAME     : Pay_Qualification.ascx                                                
// CREATION DATE : 19-June-2009                                                        
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

public partial class Masters_PAY_Qualification : System.Web.UI.UserControl
{
    //CREATING OBJECTS OF CLASS FILES COMMON,UAIMS_COMMON,PAYCONTROLLER
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ServiceBookController objServiceBook = new ServiceBookController();

    public int _idnoEmp; public int _idnoCollege;


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

        DropDownList ddlempidno = (DropDownList)this.Parent.FindControl("ddlEmployee");
        _idnoEmp = Convert.ToInt32(ddlempidno.SelectedValue);
        

        BindListViewQualification();

    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=PAY_Qualification.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=PAY_Qualification.aspx");
        }
    }

    private void FillDropDown()
    {
        try
        {
            //select QUALILEVELNO,QUALILEVELNAME from PAYROLL_QUALILEVEL
            objCommon.FillDropDownList(ddlLevel, "PAYROLL_QUALILEVEL", "QUALILEVELNO", "QUALILEVELNAME", "QUALILEVELNO > 0", "QUALILEVELNAME");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Qualification.FillDropDown-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    private void BindListViewQualification()
    {
        try
        {
            DataSet ds = objServiceBook.GetAllQualificationDetailsOfEmployee(_idnoEmp);
            lvQuali.DataSource = ds;
            lvQuali.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Masters_PAY_Qualification.BindListViewQualification-> " + ex.Message + " " + ex.StackTrace);
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
           
            objSevBook.EXAMNAME = txtExam.Text;
            objSevBook.INST = txtInstituteName.Text;
            objSevBook.UNIVERSITY_NAME = txtUniversity.Text;
            objSevBook.LOCATION = txtLocation.Text;
            objSevBook.PASSYEAR = txtYearOfPassing.Text;
            objSevBook.QUALINO = Convert.ToInt32(ddlQualification.SelectedValue);
            objSevBook.REG_NAME = txtRegName.Text;
            objSevBook.REG_DATE = txtDate.Text.Trim().Equals(string.Empty) ? DateTime.MinValue : Convert.ToDateTime(txtDate.Text.Trim());

            objSevBook.REGNO = txtRegNo.Text;

            objSevBook.LNO = Convert.ToInt32(ddlLevel.SelectedValue);

            objSevBook.SPECI = txtSpecialization.Text;
            //objSevBook.ATTACHMENTS = Convert.ToString(flupld.PostedFile.FileName.ToString());
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

            // objSevBook.ENTRYDATE = Convert.ToDateTime(txtDate.Text.Trim());



            //if (rblCategory.SelectedValue == "1")
            //{
            //    objSevBook.QSTATUS = "UG";
            //}
            //else if (rblCategory.SelectedValue == "2")
            //{
            //    objSevBook.QSTATUS = "PG";
            //}
            //  if (!(Session["colcode"].ToString()==null))objSevBook.COLLEGE_CODE = Session["colcode"].ToString();
            
            objSevBook.COLLEGE_CODE = Session["colcode"].ToString();
            objSevBook.IDCARDNO = txtIDCardNo.Text;

            //Check whether to add or update
            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    //Add New Help
                    CustomStatus cs = (CustomStatus)objServiceBook.AddQualification(objSevBook);

                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        objServiceBook.upload_new_files("QUALIFICATION", _idnoEmp, "QNO", "PAYROLL_SB_QUALI", "QUA_", flupld);
                        this.Clear();
                        this.BindListViewQualification();
                        this.objCommon.DisplayMessage(updpersonaldetails, "Record Saved Successfully", this.Page);
                    }
                }
                else
                {
                    //Edit
                    if (ViewState["qNo"] != null)
                    {                       
                            objSevBook.QNO = Convert.ToInt32(ViewState["qNo"].ToString());
                            int qno = objSevBook.QNO;
                            CustomStatus cs = (CustomStatus)objServiceBook.UpdateQualification(objSevBook);
                            if (cs.Equals(CustomStatus.RecordUpdated))
                            {
                                objServiceBook.update_upload("QUALIFICATION", qno, ViewState["attachment"].ToString(), _idnoEmp, "QUA_", flupld);
                                ViewState["action"] = "add";
                                this.Clear();
                                this.BindListViewQualification();
                                this.objCommon.DisplayMessage(updpersonaldetails, "Record Updated Successfully", this.Page);
                            }                       
                    }
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Masters_PAY_Qualification.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }


    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int qNo = int.Parse(btnEdit.CommandArgument);
            ShowDetails(qNo);
            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Masters_PAY_Qualification.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetails(int qNO)
    {
        DataSet ds = null;
        try
        {
            ds = objServiceBook.GetSingleQualificationDetailsOfEmployee(qNO);
            //To show Qualification Details of the employee
            if (ds.Tables[0].Rows.Count > 0)
            {

                ViewState["qNo"] = qNO.ToString();
                txtExam.Text = ds.Tables[0].Rows[0]["examname"].ToString();
                txtRegNo.Text = ds.Tables[0].Rows[0]["regno"].ToString();
                txtSpecialization.Text = ds.Tables[0].Rows[0]["Speci"].ToString();
                txtInstituteName.Text = ds.Tables[0].Rows[0]["inst"].ToString();
                txtUniversity.Text = ds.Tables[0].Rows[0]["UNIVERSITY_NAME"].ToString();
                txtLocation.Text = ds.Tables[0].Rows[0]["LOCATION"].ToString();

                txtYearOfPassing.Text = ds.Tables[0].Rows[0]["passyear"].ToString();
                ddlLevel.SelectedValue = ds.Tables[0].Rows[0]["LEVEL_NO"].ToString();

                objCommon.FillDropDownList(ddlQualification, "PAYROLL_QUALIFICATION", "QUALINO", "QUALI", "QUALILEVELNO=" + Convert.ToInt32(ddlLevel.SelectedValue) + "", "QUALI");
                ddlQualification.SelectedValue = ds.Tables[0].Rows[0]["QUALINO"].ToString();

                txtIDCardNo.Text = ds.Tables[0].Rows[0]["IDCARDNO"].ToString();
                txtRegName.Text = ds.Tables[0].Rows[0]["REG_COUNCIL_NAME"].ToString();
                txtDate.Text = ds.Tables[0].Rows[0]["REG_DATE"] == null ? string.Empty : ds.Tables[0].Rows[0]["REG_DATE"].ToString();
                ViewState["attachment"] = ds.Tables[0].Rows[0]["ATTACHMENT"].ToString();

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Masters_PAY_Qualification.ShowDetails-> " + ex.Message + " " + ex.StackTrace);
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
            int qNo = int.Parse(btnDel.CommandArgument);
            CustomStatus cs = (CustomStatus)objServiceBook.DeleteQualification(qNo);
            if (cs.Equals(CustomStatus.RecordDeleted))
            {
                BindListViewQualification();
                ViewState["action"] = "add";
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Masters_PAY_Qualification.btnDelete_Click-> " + ex.Message + " " + ex.StackTrace);
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
        txtExam.Text = string.Empty;
        txtRegNo.Text = string.Empty;
        txtSpecialization.Text = string.Empty;
        txtUniversity.Text = string.Empty;
        txtYearOfPassing.Text = string.Empty;
        txtDate.Text = txtLocation.Text = txtInstituteName.Text = string.Empty;
        txtIDCardNo.Text = string.Empty;
        ddlLevel.SelectedIndex = 0;
        //ddlQualification.Items.Clear();
        //ddlQualification.SelectedValue = "0";
        txtRegName.Text = txtDate.Text = string.Empty;
        ViewState["action"] = "add";
    }

    public string GetFileNamePath(object filename, object QNO, object idno)
    {
        string[] extension = filename.ToString().Split('.');
        if (filename != null && filename.ToString() != string.Empty)
            return ("~/ESTABLISHMENT/upload_files/QUALIFICATION/" + idno.ToString() + "/QUA_" + QNO + "." + extension[1].ToString().Trim());
        else
            return "";
    }

    protected void ddlLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        //SELECT  QUALINO,QUALI FROM PAYROLL_QUALIFICATION where QUALILEVELNO=1
        objCommon.FillDropDownList(ddlQualification, "PAYROLL_QUALIFICATION", "QUALINO", "QUALI", "QUALILEVELNO=" + Convert.ToInt32(ddlLevel.SelectedValue) + "", "QUALI");
    }
   
}
