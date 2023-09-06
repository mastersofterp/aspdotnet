using System;
using System.Collections;
using System.Configuration;
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
using IITMS.NITPRM;
public partial class ITLE_Result : System.Web.UI.Page
{
    
    int ctr = 0;
    long timerStartValue = 1800;
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

    int t = 0;
    int count = 0;
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
                //Check CourseNo in Session variable,if null then redirect to SelectCourse page. 

                lblUrname.Text = Convert.ToString(Session["userfullname"].ToString());

                //Page Authorization
                // CheckPageAuthorization();
                //Set the Page Title
            
                Page.Title = Session["coll_name"].ToString();
                lblSession.Text = Session["SESSION_NAME"].ToString();
                lblSession.ToolTip = Session["SessionNo"].ToString();
                lblCourse.Text = Session["ICourseName"].ToString();


                lblQueNo.Text = "1" + "/" + Convert.ToInt32(Session["NOQ"].ToString());
                if (Session["TOTSCORE"] == null || Session["TOTSCORE"] =="")
                {
                    Session["TOTSCORE"] = 0;
                }
                else
                {
                    lblScore.Text = Session["TOTSCORE"].ToString() + "/" + Session["TOTQUES"].ToString();
                }
                 
                lblTotQue.Text = Session["TOTQUES"].ToString();
                if (Session["TOTANSQUE"] == null || Session["TOTANSQUE"] == "")
                {
                    Session["TOTANSQUE"] = 0;
                    lblAnsQue.Text = Session["TOTANSQUE"].ToString();
                }
                else
                {
                    lblAnsQue.Text = Session["TOTANSQUE"].ToString();
                }

                if (Session["TOTUNANSQUESTION"] == null || Session["TOTUNANSQUESTION"] == "")
                {
                    lblUnAnsQue.Text = Session["TOTQUES"].ToString();
                }
                else
                {
                    lblUnAnsQue.Text = Session["TOTUNANSQUESTION"].ToString();
                }
                if (Session["NOCORRANS"] == null || Session["NOCORRANS"] == "")
                {
                    Session["NOCORRANS"] = "0";
                    lblWriAns.Text = Session["NOCORRANS"].ToString();
                }
                else
                {
                    lblWriAns.Text = Session["NOCORRANS"].ToString();
                }
                if (Session["TOTUNANSQUESTION"] == null || Session["TOTUNANSQUESTION"] == "")
                {
                    lblWrongAns.Text = "0";
                }
                else
                {
                    //lblWrongAns.Text = (int.Parse(lblAnsQue.Text) - int.Parse(lblWriAns.Text)).ToString();
                    lblWrongAns.Text = (Convert.ToInt32(lblAnsQue.Text.Trim()) - Convert.ToInt32(lblWriAns.Text.Trim())).ToString();
                }

                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                     //cancelButton.Attributes.Add("onclick", "window.close();");
        
            }
         
        }
        divCollegename.InnerText = objCommon.LookUp("REFF", "COLLEGENAME", "");

    }
        //DataTable dt = (DataTable)Session["Answered"];
        //decimal TOTALMARKS = 0.0m;

        //foreach (DataRow X in dt.Rows)
        //{
        //    if (Convert.ToInt32(X["SELECTED"].ToString()) + 1 == Convert.ToInt32(X["CORRECTANS"].ToString()))
        //    {
        //        TOTALMARKS += Convert.ToDecimal(X["CORRECT_MARKS"].ToString());

        //    }
        //}
        ////Label2.Text = TOTALMARKS.ToString();
    
    protected void btnOK_Click(object sender, EventArgs e)
    {
        //Response.Redirect("ITLE_StudTest.aspx");
        Session["TOTSCORE"] = 0;
        Session["TOTALMARKS"] = 0;
        Session["NOCORRANS"] = 0;
        Session["TOTANSQUE"] = 0;
        Response.Redirect("~/Itle/StudTest.aspx?pageno=1476");
        //ClientScript.RegisterStartupScript(typeof(Page), "closePage", "<script type='text/JavaScript'>window.close();</script>");
        //this.Page.Response.Close();
        //Response.Write("<script language='javascript'> { window.close();}</script>");
        //Response.Redirect("Thanks.aspx");
    }


    protected void Page_Unload(object sender, EventArgs e)
    {
        Session["TOTSCORE"] = 0;
        Session["TOTALMARKS"] = 0;
        Session["NOCORRANS"] = 0;
        Session["TOTANSQUE"] = 0;
    }
   
}
