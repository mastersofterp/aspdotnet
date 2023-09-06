using System;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ITLE_User_Controls_FreeTextBox : System.Web.UI.Page
{
    protected string MainPageField;

    protected void Page_Load(object sender, EventArgs e)
    {
        MainPageField = Request.QueryString["hf"];

        if (!IsPostBack)
        {
            // When the page first loads we need to set the freetextbox with 
            // the value from the hiddenfield for databinding etc.
            string setTextScript = string.Empty;

            if (Request.Browser.Browser != "IE")//for firefox we need to reach the freetextbox design editor to place our html
                setTextScript = string.Format("document.getElementById('{0}_designEditor').contentWindow.document.body.innerHTML = window.parent.document.getElementById('{1}').value;\r\n", ftb.ClientID, MainPageField);
            else
                setTextScript = string.Format("document.getElementById('{0}').innerHTML = window.parent.document.getElementById('{1}').value;\r\n", ftb.ClientID, MainPageField);

            Page.ClientScript.RegisterStartupScript(this.GetType(), "setText", setTextScript, true);
            ftb.Text = Session[MainPageField].ToString();
        }

        //Set the width to 99% so that the freetextbox displays completely

        ftb.Width = Unit.Percentage(100);
        // ftb.Height  = Unit.Percentage(99);


        int height = int.Parse(Request.QueryString["h"]);
        int width = int.Parse(Request.QueryString["w"]);


        if (width < 550)
            ftb.Height = Unit.Pixel(height - 130);
        else if (width < 600)
            ftb.Height = Unit.Pixel(height - 120);
        else
            ftb.Height = Unit.Pixel(height - 90);


    }
}
