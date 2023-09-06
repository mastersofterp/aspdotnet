using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ACADEMIC_RuntimeErrorHandling_ErrPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(Session["userno"] as string))
        {
            Response.Redirect("~/ACADEMIC/ErrorHandling/ErrPageForDefault.aspx");
        }
    }
    protected void lbtnBackToPage_Click(object sender, EventArgs e)
    {
        Response.Redirect(Session["BackToPageUrl"].ToString());
    }
}