
using IITMS.UAIMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using mastersofterp_MAKAUAT;


public partial class ACADEMIC_REPORTS_Applicant_Username_Password_Dump : System.Web.UI.Page
{
    Common objCommon = new Common();
    FetchDataController objFetchData = new FetchDataController();

    protected void Page_Load(object sender, EventArgs e)
    {
        CheckUserAccess();
    }

    protected void CheckUserAccess()
    {
        int ua_type = Convert.ToInt32(Session["usertype"]);

        if (ua_type != 1)
        {
            objCommon.DisplayMessage(updApplication, "You Are Not Authorized to View this Page!", this.Page);
            Response.Redirect("~/notauthorized.aspx?page= this");
            return; 
        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        if (txtApplicationNumber.Text == string.Empty)
        {
            objCommon.DisplayMessage("Please Enter Application Number", this.Page);
            return;
        }
        else
        {
            string userName = txtApplicationNumber.Text;
            int uaNo = Convert.ToInt32(Session["userno"].ToString());

            DataSet ds = objFetchData.GetApplicantUserNamePassword(userName, uaNo);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                string user_pwd = ds.Tables[0].Rows[0]["USER_PASSWORD"].ToString();
                string pass = DecryptPassword_Adm(user_pwd);

                pnlApplicantUserName.Visible = true;
                lvApplicantUserName.Visible = true;
                lvApplicantUserName.DataSource = ds;
                lvApplicantUserName.DataBind();
            }
            else
            {
                objCommon.DisplayMessage(updApplication, "No Record Found!", this.Page);
                txtApplicationNumber.Text = string.Empty;
                pnlApplicantUserName.Visible = false;
                lvApplicantUserName.Visible = false;
                lvApplicantUserName.DataSource = null;
            }


        }

    }

    protected void lvApplicantUserName_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        if (e.Item.ItemType == ListViewItemType.DataItem)
        {         
            Label lblDecryptedPassword = (Label)e.Item.FindControl("lblDecryptedPassword");

            if (lblDecryptedPassword != null)
            {
                string decryptedPassword = DecryptPassword_Adm(DataBinder.Eval(e.Item.DataItem, "USER_PASSWORD").ToString());
                lblDecryptedPassword.Text = decryptedPassword;
            }
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtApplicationNumber.Text = string.Empty;
        pnlApplicantUserName.Visible = false;
        lvApplicantUserName.Visible = false;
        lvApplicantUserName.DataSource = null;
    }

    protected void btnConnect_Click(object sender, EventArgs e)
    {
        DataSet ds = objCommon.FillDropDown("reff", "DEV_PASS", "", "", "");
        string pass = ds.Tables[0].Rows[0]["DEV_PASS"].ToString();
        string db_pwd = clsTripleLvlEncyrpt.DecryptPassword(pass);
        if (txtPass.Text.Trim() == db_pwd)
        {
            popup.Visible = false;
            CheckUserAccess();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "window", "javascript:window.close();", true);
        }
        else
            objCommon.DisplayMessage("Password does not match!", this.Page);
    }

    protected void btnCancel1_Click(object sender, EventArgs e)
    {
        if (Session["usertype"].ToString() == "1")
        {
            Response.Redirect("~/principalHome.aspx", false);
        }
        else if (Session["usertype"].ToString() == "2" || Session["usertype"].ToString() == "14")
        {
            Response.Redirect("~/studeHome.aspx", false);
        }
        else if (Session["usertype"].ToString() == "3")
        {
            Response.Redirect("~/homeFaculty.aspx", false);
        }
        else if (Session["usertype"].ToString() == "5")
        {
            Response.Redirect("~/homeNonFaculty.aspx", false);
        }
        else
        {
            Response.Redirect("~/home.aspx", false);
        }
    }


    public string DecryptPassword_Adm(string cipherText)
    {
        //if (button1WasClicked == true)
        //{
        string EncryptionKey = "0123456789abcdefghijklmnopqrstuvwxyz";
        cipherText = cipherText.Replace(" ", "+");
        byte[] cipherBytes = Convert.FromBase64String(cipherText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {
            0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76});
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(cipherBytes, 0, cipherBytes.Length);
                    cs.Close();
                }
                cipherText = Encoding.Unicode.GetString(ms.ToArray());
            }
        }
        return cipherText;
    }
}