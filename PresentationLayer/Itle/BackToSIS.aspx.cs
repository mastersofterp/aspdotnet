using IITMS;
using IITMS.UAIMS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Itle_BackToSIS : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {


            string FormCode = "~P";

            if (Session["AutoLoginCode"] == null)
            {
                //Response.Redirect("~/Itle/selectCourse.aspx?pageno=1445");
            }
            else
            {

                string AutoLoginCode = Session["AutoLoginCode"].ToString();

                // string TestLink = objCommon.LookUp("Itle_Config", "TestModuleLink", "").ToString();
                //string redirectToUrl = "https://usalmsexamtest.mastersofterp.in/?sessionno=" + sessionno + "&courseno=" + Courseno + "uaname=" + uaname;
                string redirectToUrl = "https://learn.onlinecu.in/studentHome.aspx?type=" + AutoLoginCode + FormCode;

                // string redirectToUrl = TestLink + "?sessionno=" + sessionno + "&courseno=" + Courseno + "&UA1=" + uaname + "&TOA1=" + TOKEN;
                Response.Redirect(redirectToUrl);
            }
        }



        }
    }
