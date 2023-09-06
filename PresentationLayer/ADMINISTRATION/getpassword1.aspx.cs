//=================================================================================
// PROJECT NAME  : PERSONNEL REQUIREMENT MANAGEMENT                                
// MODULE NAME   : GET THE DECRYPTED PASSWORD                                      
// CREATION DATE : 
// CREATED BY    : SHEETAL RAUT 
// MODIFIED BY   : ASHISH DHAKATE
// MODIFIED DESC : 
//=================================================================================

using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using System.Data.SqlClient;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;

public partial class getPassword : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    string Userpassword;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //Check Session
            if (Session["userno"] == null && Session["username"] == null &&
                Session["usertype"] == null && Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }

            //Page Authorization
            // CheckPageAuthorization();


            //Set the Page Title
            Page.Title = Session["coll_name"].ToString();

            //Load Page Help
            if (Request.QueryString["pageno"] != null)
            {
                //  lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
            }
        }
    }

    protected void btnGetPassword_Click(object sender, EventArgs e)
    {
        try
        {

            GetDetails();

            //////string retPwd = objPc.GetPassword(ua_name);

            ////////Show Password
            //////if (!retPwd.Equals(string.Empty))
            //////{
            //////    lblpassword.Text = Common.DecryptPassword(retPwd);
            //////    lblStatus.Text = string.Empty;
            //////}
            //////else
            //////{
            //////    lblpassword.Text = string.Empty;
            //////    lblStatus.Text = "Username Invalid";
            //////}

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Administration_getpassword.btnGetPassword_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void GetDetails()
    {
        //show password 

        PasswordController objPc = new PasswordController();
        string ua_name = txtUserName.Text.Replace("'", "").Trim();

        DataSet ds = objPc.GetPassword(ua_name);
        if (ds != null && ds.Tables[0].Rows.Count == 0)
        {
            objCommon.DisplayMessage(this.updPassword, "Username Invalid", this.Page);
            this.clear();
        }
        else
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                //trPwd.Visible = false;
                lvGetStud.DataSource = ds;
                lvGetStud.DataBind();
                ds.Tables[0].Columns[0].ToString();

                foreach (ListViewDataItem item in lvGetStud.Items)
                {
                    Label lblUserpass = item.FindControl("lblUserpass") as Label;
                    lblUserpass.Text = Common.DecryptPassword(lblUserpass.Text.ToString());
                }
                lvGetStud.Visible = true;
            }

        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        this.clear();
    }

    private void clear()
    {
        txtUserName.Text = string.Empty;
        lblpassword.Text = string.Empty;
        lblStatus.Text = string.Empty;
        lvGetStud.Visible = false;
        //trPwd.Visible = false;
        trResetPwd.Visible = false;
        btnReSetPassword.Visible = false;
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=getpassword.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=createhelp.aspx");
        }
    }

    //Reset the user password
    protected void btnReSetPassword_Click(object sender, EventArgs e)
    {
        try
        {
            PasswordController objPc = new PasswordController();
            string username = txtUserName.Text.Trim().Replace("'", "");
            string password = hfUano.Value;
            //objPc.GetPassword(username);

            int userno = -1;

            User_AccController objUC = new User_AccController();

            if (txtReSetPassword.Text.Trim() != string.Empty)
            {

                if (objUC.ValidateLogin(username, password, out userno) == Convert.ToInt32((CustomStatus.ValidUser)))
                {
                    UserAcc objUA = new UserAcc();
                    objUA.UA_Name = txtUserName.Text.Trim();
                    objUA.UA_No = userno;
                    objUA.UA_Pwd = Common.EncryptPassword(txtReSetPassword.Text.Trim());
                    objUA.UA_OldPwd = password;

                    CustomStatus cs = (CustomStatus)objUC.ChangePasswordByadmin(objUA);

                    if (cs.Equals(CustomStatus.InvalidUserNamePassword))
                    {
                        //lblStatus.Text = "Invalid Old Password";
                        objCommon.DisplayMessage(this.updPassword, "Invalid Old Password", this.Page);
                    }
                    else
                    {
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            objCommon.DisplayMessage(this.updPassword, "Password Reset Successfully !!", this.Page);
                            txtReSetPassword.Text = string.Empty;
                            lblpassword.Text = string.Empty;
                            trResetPwd.Visible = false;
                            btnReSetPassword.Visible = false;
                            GetDetails();

                        }
                    }
                }
            }
            else
            {
                objCommon.DisplayMessage(this.updPassword, "Password reset can not be blank", this.Page);
            }
        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                lblStatus.Text = "Invalid Old Password";
            else
                lblStatus.Text = "Server UnAvailable";
        }
    }


    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int ua_no = int.Parse(btnEdit.CommandArgument);
            ShowUserDetails(ua_no);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Administration_getpassword.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowUserDetails(int idno)
    {
        try
        {
            User_AccController objACC = new User_AccController();
            DataTableReader dtr;

            dtr = objACC.GetUserByUANo(idno);

            if (dtr != null)
            {
                if (dtr.Read())
                {
                    trResetPwd.Visible = true;
                    btnReSetPassword.Visible = true;

                    txtUserName.Text = dtr["UA_NAME"] == DBNull.Value ? string.Empty : dtr["UA_NAME"].ToString();
                    hfUano.Value = dtr["UA_PWD"] == DBNull.Value ? string.Empty : dtr["UA_PWD"].ToString();
                }
            }


            dtr.Close();

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Administration_getpassword.ShowUserDetails-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


}
