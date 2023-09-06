using System;
using System.Web.UI;

[ValidationProperty("Text")]
public partial class FreeTextBoxWrapper : System.Web.UI.UserControl
{

    public string Text
    {

        get
        {

            return hfFtbValue.Value;

        }

        set
        {

            hfFtbValue.Value = value;

        }

    }

    public string Width
    {

        get
        {

            return ifrmTxt.Attributes["width"];

        }

        set
        {

            ifrmTxt.Attributes["width"] = value;

        }

    }

    public string Height
    {

        get
        {

            return ifrmTxt.Attributes["height"];

        }

        set
        {

            ifrmTxt.Attributes["height"] = value;

        }

    }

    protected void Page_Load(object sender, EventArgs e)
    {

        //load the freetextbox page that has no theme and no masterpage set. The background color is the one I chose to blend in with my design 


        ifrmTxt.Attributes["Src"] = ResolveUrl(string.Format("FreeTextBox.aspx?hf={0}&w={1}&h={2}", hfFtbValue.ClientID, Width, Height));

        ifrmTxt.Attributes["Name"] = ifrmTxt.ClientID;

        Session[hfFtbValue.ClientID] = hfFtbValue.Value;

        if (!IsPostBack)
        {

            Session[hfFtbValue.ClientID] = hfFtbValue.Value;

        }

    }



    protected override void OnDataBinding(EventArgs e)
    {

        base.OnDataBinding(e);

        Session[hfFtbValue.ClientID] = hfFtbValue.Value;

    }

}