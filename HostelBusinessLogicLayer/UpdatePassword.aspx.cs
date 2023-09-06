//=================================================================================
// PROJECT NAME  : UAIMS                                                           
// PAGE NAME     : TO CHANGE PASSWORD                                              
// CREATION DATE : 19-AUG-2009                                                     
// CREATED BY    : NIRAJ D. PHALKE                                                 
// MODIFIED BY   : 
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
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;


public partial class UpdatePassword : System.Web.UI.Page
{
    Common objCommon = new Common();

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
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    lblHelp.Text = "In this Page, the user can change his existing password";
                }

                //First time login
                if (Request.QueryString["status"] != null)
                {
                    if (Request.QueryString["status"].ToString().Equals("firstlog"))
                    {
                        lblStatus.Text = "You have logged in for the first time. Please Change your Password.";
                        //update the firstlog status
                        User_AccController objUA = new User_AccController();
                        objUA.UpdateFirstLogin(Session["username"].ToString());
                    }
                }
            }
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtNewPassword.Text.Trim() == string.Empty || txtOldPassword.Text.Trim() == string.Empty || txtConfirmPassword.Text.Trim() == string.Empty)
            {
                lblMessage.Text = "Blank password is not allowed";
            }
            else
            {
                User_AccController objUC = new User_AccController();
                UserAcc objUA = new UserAcc();
                objUA.UA_Name = Session["username"].ToString();
                objUA.UA_No = Convert.ToInt32(Session["userno"].ToString());
                objUA.UA_Pwd = Common.EncryptPassword(txtNewPassword.Text.Trim());
                objUA.UA_OldPwd = Common.EncryptPassword(txtOldPassword.Text.Trim());

                CustomStatus cs = (CustomStatus)objUC.ChangePassword(objUA);

                if (cs.Equals(CustomStatus.InvalidUserNamePassword))
                    lblMessage.Text = "Invalid Old Password";
                else
                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        lblMessage.Text = "Password Modified Successfully";
                        txtConfirmPassword.Text = string.Empty;
                        txtNewPassword.Text = string.Empty;
                        txtOldPassword.Text = string.Empty;

                        objUC.UpdateFirstLogin(Session["username"].ToString());

                        Response.Redirect("~/home.aspx");
                    }
            }
        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                lblMessage.Text = "Invalid Old Password";
            else
                lblMessage.Text = "Server UnAvailable";
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/default.aspx");
    }
}
