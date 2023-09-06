//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : PAY ROLL
// PAGE NAME     : Pay_publication_Details .ascx                                                
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
public partial class PAYROLL_TRANSACTIONS_Pay_publication_Details : System.Web.UI.UserControl
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
        BindListViewPublicationDetails();
        txtAuthor.Text = (objCommon.LookUp("payroll_empmas", "fname+ ' '+mname+' '+lname", "idno=" + _idnoEmp)).ToString();
        txtAuthor.Enabled = false;
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Pay_publication_Details.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Pay_publication_Details.aspx");
        }
    }

    private void BindListViewPublicationDetails()
    {
        try
        {
            DataSet ds = objServiceBook.GetAllPublicationDetails(_idnoEmp);
            lvPublicationDetails.DataSource = ds;
            lvPublicationDetails.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Pay_publication_Details .BindListViewPublicationDetails-> " + ex.Message + " " + ex.StackTrace);
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
            objSevBook.PUBLICATION_TYPE = ddlPublicationType.SelectedValue;
            objSevBook.TITLE = txttitle.Text;
            objSevBook.SUBJECT = txtSubject.Text;
            objSevBook.PUBLICATIONDATE = Convert.ToDateTime(txtPublicationDate.Text);
            objSevBook.DETAILS =txtDetails.Text ;
            objSevBook.AUTHOR2 = txtAuthor2.Text != (string.Empty) ? txtAuthor2.Text : string.Empty;
            objSevBook.AUTHOR3= txtAuthor3.Text != (string.Empty) ? txtAuthor3.Text : string.Empty ;
            objSevBook.CONFERENCE_NAME = txtName.Text != (string.Empty) ? txtName.Text : string.Empty;
            objSevBook.ORGANISOR = txtOrg.Text != (string.Empty) ? txtOrg.Text : string.Empty;
            objSevBook.PAGENO = txtPage.Text != (string.Empty) ? txtPage.Text : string.Empty;
            objSevBook.PUBLICATION = ddlPublication.SelectedValue;
            objSevBook.COLLEGE_CODE = Session["colcode"].ToString();
            string filename = objSevBook.ATTACHMENTS;

            if (flPUB.HasFile)
            {
                objSevBook.ATTACHMENTS = Convert.ToString(flPUB.PostedFile.FileName.ToString());
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
                    CustomStatus cs = (CustomStatus)objServiceBook.AddPublicationDetails(objSevBook);

                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        objServiceBook.upload_new_files("PUBLICATION", _idnoEmp, "PUBTRXNO", "PAYROLL_SB_PUBLICATION_DETAILS", "PUB_", flPUB);
                        this.Clear();
                        this.BindListViewPublicationDetails();
                        this.objCommon.DisplayMessage(updpersonaldetails, "Record Saved Successfully", this.Page);
                    }
                }
                else
                {
                    //Edit
                    if (ViewState["PUBTRXNO"] != null)
                    {
                        objSevBook.PUBTRXNO = Convert.ToInt32(ViewState["PUBTRXNO"].ToString());
                        CustomStatus cs = (CustomStatus)objServiceBook.UpdatePublicationDetails(objSevBook);
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            objServiceBook.update_upload("PUBLICATION", Convert.ToInt32(ViewState["PUBTRXNO"].ToString()), ViewState["attachment"].ToString(), _idnoEmp, "PUB_", flPUB);
                            ViewState["action"] = "add";
                            this.Clear();
                            this.BindListViewPublicationDetails();
                            this.objCommon.DisplayMessage(updpersonaldetails, "Record Updated Successfully", this.Page);
                        }
                    }
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Pay_publication_Details .btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }


    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int PUBTRXNO = int.Parse(btnEdit.CommandArgument);
            ShowDetails(PUBTRXNO);
            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Pay_publication_Details .btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetails(int PUBTRXNO)
    {
        DataSet ds = null;
        try
        {
            ds = objServiceBook.GetSinglePublicationDetails(PUBTRXNO);
            //To show created user details 
            if (ds.Tables[0].Rows.Count > 0)
            {

                ViewState["PUBTRXNO"] = PUBTRXNO.ToString();
                ddlPublicationType.SelectedValue = ds.Tables[0].Rows[0]["PUBLICATION_TYPE"].ToString();
                ddlPublication.SelectedValue = ds.Tables[0].Rows[0]["PUBLICATION"].ToString();
                txtAuthor2.Text = ds.Tables[0].Rows[0]["AUTHOR2"].ToString();
                txtAuthor3.Text = ds.Tables[0].Rows[0]["AUTHOR3"].ToString();
                txtName.Text = ds.Tables[0].Rows[0]["NAME"].ToString();
                txtOrg.Text = ds.Tables[0].Rows[0]["ORGANISOR"].ToString();
                txtPage.Text = ds.Tables[0].Rows[0]["PAGENO"].ToString();
                txttitle.Text = ds.Tables[0].Rows[0]["TITLE"].ToString();
                txtSubject.Text = ds.Tables[0].Rows[0]["SUBJECT"].ToString();
                txtPublicationDate.Text = ds.Tables[0].Rows[0]["PUBLICATIONDATE"].ToString();
                txtDetails.Text = ds.Tables[0].Rows[0]["DETAILS"].ToString();
                ViewState["attachment"] = ds.Tables[0].Rows[0]["ATTACHMENT"].ToString();

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Pay_publication_Details .ShowDetails-> " + ex.Message + " " + ex.StackTrace);
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
            int PUBTRXNO = int.Parse(btnDel.CommandArgument);
            CustomStatus cs = (CustomStatus)objServiceBook.DeletePublicationDetails(PUBTRXNO);
            if (cs.Equals(CustomStatus.RecordDeleted))
            {
                BindListViewPublicationDetails();
                ViewState["action"] = "add";
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Pay_publication_Details .btnDelete_Click-> " + ex.Message + " " + ex.StackTrace);
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
        txtDetails.Text = string.Empty;
        txtPublicationDate.Text = string.Empty;
        txtSubject.Text = string.Empty;
        txttitle.Text = string.Empty;
        ddlPublicationType.SelectedIndex = 0;
        ddlPublication.SelectedIndex = 0;
        txtPage.Text = string.Empty;
        txtOrg.Text = string.Empty;
        txtName.Text = string.Empty;
        txtAuthor2.Text = string.Empty;
        txtAuthor3.Text = string.Empty;
        ViewState["action"] = "add";
    }
    public string GetFileNamePath(object filename, object pubtrxno ,object idno)
    {
        string[] extension = filename.ToString().Split('.');
        if (filename != null && filename.ToString() != string.Empty)
            return ("~/ESTABLISHMENT/upload_files/PUBLICATION/" + idno.ToString() + "/PUB_" + pubtrxno + "." + extension[1].ToString().Trim());
        else
            return "";
    }
}
