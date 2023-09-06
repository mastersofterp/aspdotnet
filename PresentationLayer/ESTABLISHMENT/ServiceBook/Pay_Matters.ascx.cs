//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : PAY ROLL
// PAGE NAME     : Pay_Matters.ascx                                                
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

public partial class PayRoll_Pay_Matters : System.Web.UI.UserControl
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

        BindListViewMatters();

    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Pay_Matters.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Pay_Matters.aspx");
        }
    }

    private void BindListViewMatters()
    {
        try
        {   
            DataSet ds = objServiceBook.GetAllMatterDetailsOfEmployee(_idnoEmp);
            lvMatters.DataSource = ds;
            lvMatters.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Matters.BindListViewMatters-> " + ex.Message + " " + ex.StackTrace);
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
            objSevBook.HEADING = txtHeadingTopic.Text;
            objSevBook.EDT = Convert.ToDateTime(txtDate.Text);
            objSevBook.MATTER = txtMatter.Text;
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
                    CustomStatus cs = (CustomStatus)objServiceBook.AddMatter(objSevBook);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        objServiceBook.upload_new_files("MATTER", _idnoEmp, "MNO", "PAYROLL_SB_MATTER", "MAT_", flupld);
                        this.Clear();
                        this.BindListViewMatters();
                        this.objCommon.DisplayMessage(updpersonaldetails, "Record Saved Successfully", this.Page);
                    }
                }
                else
                {
                    //Edit
                    if (ViewState["mNo"] != null)
                    {
                        objSevBook.MNO = Convert.ToInt32(ViewState["mNo"].ToString());
                        CustomStatus cs = (CustomStatus)objServiceBook.UpdateMatter(objSevBook);
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            objServiceBook.update_upload("MATTER", objSevBook.MNO, ViewState["attachment"].ToString(), _idnoEmp, "MAT_", flupld);
                            ViewState["action"] = "add";
                            this.Clear();
                            this.BindListViewMatters();
                            this.objCommon.DisplayMessage(updpersonaldetails, "Record Updated Successfully", this.Page);
                        }
                    }
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Matters.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }


    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int mNo = int.Parse(btnEdit.CommandArgument);
            ShowDetails(mNo);
            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Matters.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetails(int mNo)
    {
        DataSet ds = null;
        try
        {
            ds = objServiceBook.GetSingleMatterDetailsOfEmployee(mNo);
            //To show created user details 
            if (ds.Tables[0].Rows.Count > 0)
            {

                ViewState["mNo"] = mNo.ToString();
                txtDate.Text = ds.Tables[0].Rows[0]["edt"].ToString();
                txtHeadingTopic.Text = ds.Tables[0].Rows[0]["heading"].ToString();
                txtMatter.Text = ds.Tables[0].Rows[0]["matter"].ToString();
                ViewState["attachment"] = ds.Tables[0].Rows[0]["ATTACHMENT"].ToString();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Matters.ShowDetails-> " + ex.Message + " " + ex.StackTrace);
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
            int mNo = int.Parse(btnDel.CommandArgument);
            CustomStatus cs = (CustomStatus)objServiceBook.DeleteMatter(mNo);
            if (cs.Equals(CustomStatus.RecordDeleted))
            {
                Clear();
                BindListViewMatters();
                ViewState["action"] = "add";
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Matters.btnDelete_Click-> " + ex.Message + " " + ex.StackTrace);
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
        txtDate.Text = string.Empty;
        txtHeadingTopic.Text = string.Empty;
        txtMatter.Text = string.Empty;
        ViewState["action"] = "add";
    }

    public string GetFileNamePath(object filename, object MNO, object idno)
    {
        string[] extension = filename.ToString().Split('.');
        if (filename != null && filename.ToString() != string.Empty)
            return ("~/ESTABLISHMENT/upload_files/MATTER/" + idno.ToString() + "/MAT_" + MNO + "." + extension[1].ToString().Trim());
        else
            return "";
    }
}
