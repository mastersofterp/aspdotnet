//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : PAY ROLL
// PAGE NAME     : Pay_FamilyParticulars.ascx                                                
// CREATION DATE : 14-FEB-2022
// CREATED BY    : SONAL BANODE                                                   
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
using System.IO;

public partial class ESTABLISHMENT_ServiceBook_Pay_SB_Avishkar : System.Web.UI.Page
{

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


            objCommon.FillDropDownList(ddlUniversity, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ")", "COLLEGE_NAME");
        }

        // DropDownList ddlempidno = (DropDownList)this.Parent.FindControl("ddlEmployee");       
        // _idnoEmp = Convert.ToInt32(ddlempidno.SelectedValue); 
        if (Session["serviceIdNo"] != null)
        {
            _idnoEmp = Convert.ToInt32(Session["serviceIdNo"].ToString().Trim());
        }
        BindListViewAvishkar();
        GetConfigForEditAndApprove();
    }

    private void BindListViewAvishkar()
    {
        try
        {
            DataSet ds = objServiceBook.GetAllAvishkar(_idnoEmp);
            lvInfo.DataSource = ds;
            lvInfo.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Pay_SB_Avishkar.BindListViewAvishkar-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            ServiceBook objSevBook = new ServiceBook();
            objSevBook.IDNO = _idnoEmp;
            objSevBook.COLLEGE_CODE = Convert.ToString(Session["colcode"]);
            objSevBook.LEVEL = Convert.ToInt32(ddlLevel.SelectedValue);
            objSevBook.PAPERTITLE = txtTitle.Text;
            //objSevBook.DATE = Convert.ToDateTime(txtdatereceived.Text);
            if (txtdatereceived.Text.Trim() == string.Empty)
            {
                objSevBook.AVDATE = null; // DateTime.MinValue;
            }
            else
            {
                objSevBook.AVDATE = Convert.ToDateTime(txtdatereceived.Text);
            }

            objSevBook.VENUE = txtVenue.Text;
            objSevBook.AWARD = Convert.ToInt32(ddlAward.SelectedValue);

            if (!(Session["colcode"].ToString() == null)) objSevBook.COLLEGE_CODE = Session["colcode"].ToString();
            //Check whether to add or update
            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    //Add New Help
                    CustomStatus cs = (CustomStatus)objServiceBook.AddAvishkar(objSevBook);

                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        this.Clear();
                        this.BindListViewAvishkar();
                        MessageBox("Record Saved Successfully");
                    }
                    else if (cs.Equals(CustomStatus.RecordFound))
                    {
                        MessageBox("Record Already Exist ");
                        this.Clear();
                    }
                }
                else
                {
                    //Edit
                    if (ViewState["AVNO"] != null)
                    {
                        objSevBook.AVNO = Convert.ToInt32(ViewState["AVNO"].ToString());
                        CustomStatus cs = (CustomStatus)objServiceBook.UpdateAvishkar(objSevBook);
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            ViewState["action"] = "add";
                            ViewState["AVNO"] = null;

                            this.Clear();
                            this.BindListViewAvishkar();
                            MessageBox("Record Updated Successfully");
                        }
                        else if (cs.Equals(CustomStatus.RecordExist))
                        {
                            MessageBox("Record Already Exist");
                            this.Clear();
                        }

                    }
                }

            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Pay_SB_Avishkar.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }

    public void Clear()
    {
        ddlAward.SelectedIndex = 0;
        ddlLevel.SelectedIndex = 0;
        ddlUniversity.SelectedIndex = 0;
        txtTitle.Text = string.Empty;
        txtVenue.Text = string.Empty;
        txtdatereceived.Text = string.Empty;

        ViewState["action"] = "add";
        ViewState["IsEditable"] = null;
        ViewState["IsApprovalRequire"] = null;
        btnSubmit.Enabled = true;
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

            ImageButton btnEdit = sender as ImageButton;
            int AVNO = int.Parse(btnEdit.CommandArgument);
            ShowDetails(AVNO);
            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Pay_SB_Avishkar.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetails(int AVNO)
    {
        DataSet ds = null;
        try
        {
            ds = objServiceBook.GetSingleFamilyDetailsOfAvishkar(AVNO);

            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["AVNO"] = AVNO.ToString();
                txtdatereceived.Text = ds.Tables[0].Rows[0]["DOR"].ToString();
                txtTitle.Text = ds.Tables[0].Rows[0]["PAPERTITLE"].ToString();
                txtVenue.Text = ds.Tables[0].Rows[0]["VENUE"].ToString();
                ddlAward.SelectedValue = ds.Tables[0].Rows[0]["AWARD"].ToString();
                ddlLevel.SelectedValue = ds.Tables[0].Rows[0]["LEVEL"].ToString();
                ddlUniversity.SelectedValue = ds.Tables[0].Rows[0]["COLLEGE_CODE"].ToString();
                if (Convert.ToBoolean(ViewState["IsApprovalRequire"]) == true)
                {
                    string STATUS = ds.Tables[0].Rows[0]["APPROVE_STATUS"].ToString();
                    if (STATUS == "A")
                    {
                        MessageBox("Your Details Are Approved You Cannot Edit.");
                        btnSubmit.Enabled = false;
                        return;
                    }
                    else
                    {
                        btnSubmit.Enabled = true;
                    }
                    GetConfigForEditAndApprove();
                }
                else
                {
                    btnSubmit.Enabled = true;
                    GetConfigForEditAndApprove();
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Pay_SB_Avishkar.ShowDetails-> " + ex.Message + " " + ex.StackTrace);
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
            int AVNO = int.Parse(btnDel.CommandArgument);
            DataSet ds = new DataSet();
            ds = objCommon.FillDropDown("PAYROLL_SB_AVISHKAR", "LTRIM(RTRIM(ISNULL(APPROVE_STATUS,''))) AS APPROVE_STATUS", "", "AVNO=" + AVNO, "");
            string STATUS = ds.Tables[0].Rows[0]["APPROVE_STATUS"].ToString();
            if (STATUS == "A")
            {
                MessageBox("Your Details are Approved You Cannot Delete.");
                return;
            }
            else if (STATUS == "R")
            {
                MessageBox("Your Details are Rejected You Cannot Delete.");
                return;
            }
            else
            {
                CustomStatus cs = (CustomStatus)objServiceBook.DeleteAvishkar(AVNO);
                if (cs.Equals(CustomStatus.RecordDeleted))
                {
                    BindListViewAvishkar();
                    // lblFamilymsg.Text = "Record Deleted Successfully";
                    MessageBox("Record Deleted Successfully");
                    ViewState["action"] = "add";
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Pay_SB_Avishkar.btnDelete_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
        GetConfigForEditAndApprove();
    }

    #region ServiceBook Config

    private void GetConfigForEditAndApprove()
    {
        DataSet ds = null;
        try
        {
            Boolean IsEditable = false;
            Boolean IsApprovalRequire = false;
            string Command = "Avishkar";
            ds = objServiceBook.GetServiceBookConfigurationForRestrict(Convert.ToInt32(Session["usertype"]), Command);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsEditable = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsEditable"]);
                IsApprovalRequire = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsApprovalRequire"]);
                ViewState["IsEditable"] = IsEditable;
                ViewState["IsApprovalRequire"] = IsApprovalRequire;

                if (Convert.ToBoolean(ViewState["IsEditable"]) == true)
                {
                    btnSubmit.Enabled = false;
                }
                else
                {
                    btnSubmit.Enabled = true;
                }
            }
            else
            {
                ViewState["IsEditable"] = false;
                ViewState["IsApprovalRequire"] = false;
                btnSubmit.Enabled = true;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_PreviousService.GetConfigForEditAndApprove-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
        finally
        {
            ds.Clear();
            ds.Dispose();
        }
    }

    #endregion
}