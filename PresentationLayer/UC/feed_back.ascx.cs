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

using IITMS;
using IITMS.UAIMS;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;

public partial class UC_feed_back : System.Web.UI.UserControl
{
    UAIMS_Common objCommon = new UAIMS_Common();
    //ConnectionStrings
    string uaims_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

    protected void btnOk_Click(object sender, EventArgs e)
    {
        try
        {
            SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
            SqlParameter[] objParams = null;
                   
            //Add Feedback
            objParams = new SqlParameter[6];
            objParams[0] = new SqlParameter("@P_FNAME", txtName.Text.Trim());
            objParams[1] = new SqlParameter("@P_FADD", txtAddress.Text.Trim());
            objParams[2] = new SqlParameter("@P_FCONTACT", txtContactNo.Text.Trim());
            objParams[3] = new SqlParameter("@P_FEMAIL", txtEmail.Text.Trim());
            objParams[4] = new SqlParameter("@P_FSUGGESTION", txtComments.Text.Trim());
            objParams[5] = new SqlParameter("@P_FDATE", DateTime.Now.ToString());
            
            Object ret = objSQLHelper.ExecuteNonQuerySP("PKG_FEEDBACK_SP_INS_FEEDBACK", objParams, false);
            if (ret != null)
            {
                txtName.Text = string.Empty;
                txtAddress.Text = string.Empty;
                txtContactNo.Text = string.Empty;
                txtEmail.Text = string.Empty;
                txtComments.Text = string.Empty;
                //lblStatus.Text = "Thanks for your Feedback";

                Response.Redirect(Request.Url.ToString());
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "UC_feed_back.btnOk_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }   

}
