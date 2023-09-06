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
using System.Text;
using JumpyForum;
using IITMS.NITPRM;

public partial class Itle_Itle_NewDiscussionForum : System.Web.UI.Page
{
    private int articleId = 0;
    private int currentCount = 1;
    string file_path = System.Configuration.ConfigurationManager.AppSettings["DirPath"];
    //private int pagesize= 20;
    
    #region Page Load

    protected void Page_Load(object sender, System.EventArgs e)
    {
        // Put user code to initialize the page here

        if (!Page.IsPostBack)
        {
            PageSize = Convert.ToInt32(txtpagesize.SelectedItem.Text);
            //Response.Write ("<h1>" +txtpagesize.SelectedItem.Text + "::" + PageSize +"</h1>");

            //Check CourseNo in Session variable,if null then redirect to SelectCourse page. 
            if (Session["ICourseNo"] == null)
            {
                Response.Redirect("~/Itle/selectCourse.aspx?pageno=1445");
            }


            if (Session["Page"] == null)
            {
                CheckPageAuthorization();
                Session["Page"] = 1;
            }            
        }
        else
        {
            if (Request.QueryString["pagesize"] != null)
            {
                this.PageSize = Convert.ToInt32(Request.QueryString["pagesize"]);
            }
        }
        //Page Authorization
        //CheckPageAuthorization();

        txtpagesize.ClearSelection();
        txtpagesize.Items.FindByText(this.PageSize.ToString()).Selected = true;

        //Response.Write ("<h1>" +PageSize +"</h1>");



        LoadData();
    }

    #endregion

    #region Private Method

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Itle_NewDiscussionForum.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Itle_NewDiscussionForum.aspx");
        }
    }

    private void LoadData()
    {
        DateTime lastVisit = DateTime.Now;
        StringBuilder sb = new StringBuilder();
        //string myQuery ="";
        //string url = "/ITLE/upload_files/announcement/";
        string url = file_path + "ITLE\\upload_files\\DiscussionForum\\";

        if (Request.QueryString["id"] != null)
            articleId = Convert.ToInt32(Request.QueryString["id"]);
        else
            articleId = 1;
        //Response.Write ("<hr>" + myQuery + "<hr>");
        //if (Session["usertype"].ToString().Equals("3") || Session["usertype"].ToString().Equals("1"))
        //{
        //    lblnewmessage.Visible = true;
        //}

        if (Convert.ToInt32(Session["usertype"]) == 3 || Convert.ToInt32(Session["usertype"]) == 1)
        {
            lblnewmessage.Visible = true;
        }
        lblnewmessage.Text = "<A title='Add a new message to the Article " + articleId + "' href='newmessage.aspx?id=" + articleId + "'><b><FONT face='Arial' size='2'>Add Question</FONT></b></A><A title='Add a TEST message to the Article " + articleId + "' href='newmessage.aspx?id=" + articleId + "&amp;Test=true'><b><FONT face='Arial' size='2'></FONT></b></A>";

        clsDataAccess myclass = new clsDataAccess();
        myclass.openConnection();



        SqlDataReader myReader = myclass.getForumData(articleId, Convert.ToInt32(Session["ICourseNo"]), Convert.ToInt32(Session["SessionNo"]));

        int mycount = 1;



        while (myReader.Read())
        {

            DateTime dt1 = DateTime.Now;
            DateTime dt2 = Convert.ToDateTime(myReader["DateAdded"].ToString());
            if (mycount == 1)
                lastVisit = Convert.ToDateTime(myReader["DateAdded"].ToString());
            else
            {
                if (DateTime.Compare(lastVisit, dt2) < 0)
                    lastVisit = dt2;
            }


            TimeSpan ts = dt1.Subtract(dt2);

            string mytimeago = "";
            if (Convert.ToInt32(ts.TotalDays) != 0)
                mytimeago = "" + Math.Abs(Convert.ToInt32(ts.TotalDays)) + " Days ago";
            else
            {
                if ((Convert.ToInt32(ts.TotalMinutes) < 10) && (Convert.ToInt32(ts.TotalHours) == 0))
                {
                    mytimeago = "Just Posted";
                }
                else if ((Convert.ToInt32(ts.TotalMinutes) > 5) && (Convert.ToInt32(ts.TotalHours) == 0))
                {
                    mytimeago = Convert.ToInt32(ts.TotalMinutes) % 60 + " Mins ago";
                }
                else if (Convert.ToInt32(ts.TotalHours) != 0)
                {
                    mytimeago = "" + Convert.ToInt32(ts.TotalHours) + " Hours " + Convert.ToInt32(ts.TotalMinutes) % 60 + " Mins ago";
                }
                else
                {
                    mytimeago = Convert.ToInt32(ts.TotalMinutes) % 60 + " Mins ago";
                }


            }

            string newimg = "";
            if (String.Compare(mytimeago, "Just Posted") == 0)
                newimg = "<img src='../IMAGES/New.gif' border='0' alt=''>";
            else
                //  newimg = "<img src='../IMAGES/New.gif' border='0' alt=''>";

                //if(mycount==1)
                //sb.Append("<tr bgcolor='#b7dfd5' id='K1745932k" + mycount + "kOFF'>");
                //else

                if (Request.QueryString["current"] != null)
                    currentCount = Convert.ToInt32(Request.QueryString["current"]);
                else
                    currentCount = 1;

            int myMaxCount = currentCount + Convert.ToInt32(this.PageSize);
            int myStartCount = currentCount;

            if (currentCount == -1)
            {
                myStartCount = 0;
                myMaxCount = 999;
            }



            if (mycount < myMaxCount && mycount >= myStartCount)
            {
                sb.Append("<tr bgcolor='#EDF8F4' id='K1745932k" + mycount + "kOFF'>");

                sb.Append("<td width='100%' colspan='1'>");
                sb.Append("<table border='0' cellspacing='0' cellpadding='0' width='100%'>");
                sb.Append("<tr>");

                int myindent = 4;
                if (Convert.ToInt32(myReader["Indent"]) <= 4)
                    myindent = 16 * Convert.ToInt32(myReader["Indent"]);
                else if (Convert.ToInt32(myReader["Indent"]) <= 8)
                    myindent = 15 * Convert.ToInt32(myReader["Indent"]);
                else if (Convert.ToInt32(myReader["Indent"]) <= 16)
                    myindent = 14 * Convert.ToInt32(myReader["Indent"]);
                else if (Convert.ToInt32(myReader["Indent"]) <= 20)
                    myindent = Convert.ToInt32(13.5 * Convert.ToDouble(myReader["Indent"]));
                else if (Convert.ToInt32(myReader["Indent"]) <= 24)
                    myindent = 13 * Convert.ToInt32(myReader["Indent"]);
                else if (Convert.ToInt32(myReader["Indent"]) <= 28)
                    myindent = Convert.ToInt32(12.7 * Convert.ToDouble(myReader["Indent"]));
                else if (Convert.ToInt32(myReader["Indent"]) <= 32)
                    myindent = Convert.ToInt32(12.4 * Convert.ToDouble(myReader["Indent"]));

                sb.Append("<td  width='60%'><a name='xxK1745932k" + mycount + "kxx'></a><img height='1' width='" + myindent + "' src='../images/general.gif' alt=''>");

                //bgcolor='white'
                if (Convert.ToInt32(myReader["CommentType"].ToString()) == 1)
                    sb.Append("<img align='middle' src='../images/general.gif' alt=''>");//</td>
                if (Convert.ToInt32(myReader["CommentType"].ToString()) == 2)
                    sb.Append("<img align='middle' src='../images/info.gif' alt=''>&nbsp;");//</td>
                if (Convert.ToInt32(myReader["CommentType"].ToString()) == 3)
                    sb.Append("<img align='middle' src='../images/answer.gif' alt=''>&nbsp;");//</td>
                if (Convert.ToInt32(myReader["CommentType"].ToString()) == 4)
                    sb.Append("<img align='middle' src='../images/question.gif' alt=''>&nbsp;");//</td>
                if (Convert.ToInt32(myReader["CommentType"].ToString()) == 5)
                    sb.Append("<img align='middle' src='../images/game.gif' alt=''>&nbsp;");//</td>


                // sb. Append("<td bgcolor='white'><a name='xxK1745932k" + mycount + "kxx'></a><img height='1' width='" + myindent + "' src='../images/general.gif' alt=''>");

                //NITIN 18 68% <td width='58%'>
                sb.Append("<a  id='LinkTrigger" + mycount + "' name='K1745932k" + mycount + "k' href='K1745932#xxK1745932k" + mycount + "kxx'>");

                if (Convert.ToInt32(myReader["Indent"]) == 0)
                    sb.Append("<b><FONT face='Arial' size='2'>&nbsp;" + myReader["Title"].ToString() + "</FONT></b></a>" + newimg + "</td>");
                else
                    //sb. Append("<FONT face='Arial' size='2'>&nbsp;" + myReader["Title"]. ToString() + "<!-- : " + myindent + "::" + Convert. ToInt32(myReader["Indent"]) + "--></FONT></a>" + newimg + "</td>");
                    sb.Append("<FONT face='Arial' size='2'>&nbsp;" + myReader["Title"].ToString() + "<!--: " + myindent + "::" + Convert.ToInt32(myReader["Indent"]) + "--></FONT></a>" + newimg + "</td>");

                //nitin comments
                //DateTime dt = DateTime.Now.CompareTo(Convert.ToDateTime(myReader["DateAdded"].ToString()));


                sb.Append("<td width='2%' valign='middle' nowrap ><a href='" + myReader["UserProfile"].ToString() + "'> <img src='../Images/userinfo.gif'  alt='' title='Click for User Profile' border='0' width='14' height='15'></a>&nbsp;</td>");
                sb.Append("<td align='left' width='18%' nowrap><font ><b><FONT face='Arial' size='2'>" + myReader["UserName"].ToString() + "</FONT></b>&nbsp;</font></td>");
                sb.Append("<td  align='right' width='20%' nowrap><font ><b><FONT face='Arial' size='2'>" + mytimeago);
                sb.Append("</FONT></b>&nbsp;</font></td>");
                sb.Append("</tr>");
                sb.Append("</table>");
                sb.Append("</td>");
                sb.Append("</tr>");


                sb.Append("<tr id='K1745932k" + mycount + "kON' style='DISPLAY:none'>");

                sb.Append("<td colspan='1' width='100%'>");
                sb.Append("<table border='0' cellspacing='0' cellpadding='0' width='100%'>");
                sb.Append("<tr>");
                sb.Append("<td><img height='1' width='" + myindent + "' src='../Images/ind.gif' alt=''><img align='middle' src='../Images/blank.gif' height='30' width='28' alt=''>&nbsp;</td>");
                sb.Append("<td bgcolor='#EDF8F4' width='100%'><table border='0' cellspacing='5' cellpadding='0' width='100%'>");
                sb.Append("<tr>");
                sb.Append("<td>");
                sb.Append("<table border='0' cellspacing='0' cellpadding='0' width='100%'>");
                sb.Append("<tr>");
                sb.Append("<td colspan='2'>");
                sb.Append("<font face = 'arial' size='2'>" + myReader["Description"].ToString() + "</font>");//" Time Now:" + dt1 + " DBTime:" +  dt2 +"
                sb.Append("<br>");
                sb.Append("&nbsp;</td>");
                sb.Append("</tr>");
                sb.Append("<tr valign='top'>");
                sb.Append("<td >[<a href='Reply.aspx?id=" + articleId + "&amp;CID=" + myReader["ID"].ToString() + "' title='Reply to this current thread'><font face = arial size=2><span style ='color:red;font-weight:bold;'>Reply</span></font></a>]");
                //sb. Append("[<a href='Reply.aspx?Test=true&amp;id=" + articleId + "&amp;CID=" + myReader["ID"]. ToString() + "' title='Test Reply for this current thread'><font face = arial size=2></font></a>]");
                sb.Append("</td>");


                if (Session["usertype"].ToString().Equals("3") || Session["usertype"].ToString().Equals("1"))
                {
                    sb.Append("<td align='right' >[<a href='Delete.aspx?id=" + articleId + "&amp;CID=" + myReader["ID"].ToString() + "' ");
                    sb.Append("title='Delete this current thread'><font face = arial size=2>Delete</font></a>]");
                }

                sb.Append("</td>");
                sb.Append("</tr>");

                //for attachment link
                sb.Append("<tr>");
                //sb.Append("<td>");
                //sb.Append("<a Target=_blank href=" +"file:///"+ url + myReader["ATTACHMENT"].ToString() + ">" + myReader["ATTACHMENT"].ToString() + "</a>");
                sb.Append("<td align='left' >[<a Target=_blank href='DownloadAttachment.aspx?file=" + url + myReader["ATTACHMENT"].ToString() + "&filename=" + myReader["ATTACHMENT"].ToString() + "' ");
                sb.Append("title=''>" + myReader["ATTACHMENT"].ToString() + "</a>]"); sb.Append("</td>");
                //sb.Append("</td>");

                sb.Append("</tr>");

                sb.Append("</table>");
                sb.Append("</td>");
                sb.Append("</tr>");
                sb.Append("</table>");
                sb.Append("</td>");
                sb.Append("</tr>");
                sb.Append("</table>");
                sb.Append("</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td colspan='1'><img src='../images/t.gif' border='0' width='1' height='6' alt=''></td>");
                sb.Append("</tr>");
            }
            mycount++;
        }
        myReader.Close();
        myclass.closeConnection();

        if (currentCount == -1)
        {
            lblPaging.Text = "<a title ='First " + this.PageSize + "' href='Itle_NewDiscussionForum.aspx?id=" + articleId + "&amp;current=" + 1 + "&amp;pagesize=" + this.PageSize + "'>First</a>&nbsp;&nbsp;Prev&nbsp;&nbsp;<a title='Show All' href='Itle_NewDiscussionForum.aspx?id=" + articleId + "&amp;current=-1" + "&amp;pagesize=" + this.PageSize + "'><b>" + (mycount - 1) + "</b> records</a>&nbsp;&nbsp;Next&nbsp;&nbsp;<a href='Itle_NewDiscussionForum.aspx?id=" + articleId + "&amp;current=" + (mycount - Convert.ToInt32(this.PageSize)) + "&amp;pagesize=" + this.PageSize + "' title ='Last " + this.PageSize + "' >Last</a>&nbsp;&nbsp;";
        }
        else if (currentCount == 1)
        {
            lblPaging.Text = "First&nbsp;&nbsp;Prev&nbsp;&nbsp;<a title='Show All' href='Itle_NewDiscussionForum.aspx?id=" + articleId + "&amp;current=-1&amp;pagesize=" + this.PageSize + "'><b>" + (mycount - 1) + "</b> records</a>&nbsp;&nbsp;<a href='Itle_NewDiscussionForum.aspx?id=" + articleId + "&amp;current=" + (Convert.ToInt32(this.PageSize) + 1) + "&amp;pagesize=" + this.PageSize + "' title ='Next " + this.PageSize + "'>Next</a>&nbsp;&nbsp;<a href='Itle_NewDiscussionForum.aspx?id=" + articleId + "&amp;current=" + (mycount - Convert.ToInt32(this.PageSize)) + "&amp;pagesize=" + this.PageSize + "' title ='Last " + this.PageSize + "'>Last</a>&nbsp;&nbsp;";
        }
        else if (currentCount == (mycount - Convert.ToInt32(this.PageSize)))
        {
            lblPaging.Text = "<a href='Itle_NewDiscussionForum.aspx?id=" + articleId + "&amp;current=" + 1 + "&amp;pagesize=" + this.PageSize + "'>First</a>&nbsp;&nbsp;<a href='Itle_NewDiscussionForum.aspx?&amp;pagesize=" + this.PageSize + "'  title ='Previous " + this.PageSize + "'>Prev</a>&nbsp;&nbsp;<a title='Show All' href='Itle_NewDiscussionForum.aspx?id=" + articleId + "&amp;current=-1&amp;pagesize=" + this.PageSize + "'><b>" + (mycount - 1) + "</b> records</a>&nbsp;&nbsp;Next&nbsp;&nbsp;Last&nbsp;&nbsp;";
        }
        else
        {
            if (mycount > (Convert.ToInt32(this.PageSize) + currentCount))
            {
                if (currentCount - Convert.ToInt32(this.PageSize) < 0)
                {
                    lblPaging.Text = "<a  title ='First " + this.PageSize + "' href='Itle_NewDiscussionForum.aspx?id=" + articleId + "&amp;current=" + 1 + "&amp;pagesize=" + this.PageSize + "'>First</a>&nbsp;&nbsp;Prev&nbsp;&nbsp;<a title='Show All' href='Itle_NewDiscussionForum.aspx?id=" + articleId + "&amp;current=-1&amp;pagesize=" + this.PageSize + "'><b>" + (mycount - 1) + "</b> records</a>&nbsp;&nbsp;<a href='Itle_NewDiscussionForum.aspx?id=" + articleId + "&amp;current=" + (Convert.ToInt32(this.PageSize) + currentCount) + "&amp;pagesize=" + this.PageSize + "' title ='Next " + this.PageSize + "'>Next</a>&nbsp;&nbsp;<a href='Itle_NewDiscussionForum.aspx?id=" + articleId + "&amp;current=" + (mycount - Convert.ToInt32(this.PageSize)) + "&amp;pagesize=" + this.PageSize + "' title ='Last " + this.PageSize + "'>Last</a>&nbsp;&nbsp;";
                }
                else
                {
                    lblPaging.Text = "<a  title ='First " + this.PageSize + "' href='Itle_NewDiscussionForum.aspx?id=" + articleId + "&amp;current=" + 1 + "&amp;pagesize=" + this.PageSize + "'>First</a>&nbsp;&nbsp;<a href='Itle_NewDiscussionForum.aspx?id=" + articleId + "&amp;pagesize=" + this.PageSize + "&amp;current=" + (currentCount - Convert.ToInt32(this.PageSize)) + "' title ='Previous " + this.PageSize + "'>Prev</a>&nbsp;&nbsp;<a title='Show All' href='Itle_NewDiscussionForum.aspx?id=" + articleId + "&amp;current=-1&amp;pagesize=" + this.PageSize + "'><b>" + (mycount - 1) + "</b> records</a>&nbsp;&nbsp;<a href='Itle_NewDiscussionForum.aspx?id=" + articleId + "&amp;current=" + (Convert.ToInt32(this.PageSize) + currentCount) + "&amp;pagesize=" + this.PageSize + "'  title ='Next " + this.PageSize + "'>Next</a>&nbsp;&nbsp;<a href='Itle_NewDiscussionForum.aspx?id=" + articleId + "&amp;current=" + (mycount - Convert.ToInt32(this.PageSize)) + "&amp;pagesize=" + this.PageSize + "' title ='Last " + this.PageSize + "'>Last</a>&nbsp;&nbsp;";
                }
            }
            else
            {
                if (currentCount - Convert.ToInt32(this.PageSize) < 0)
                {
                    lblPaging.Text = "<a  title ='First " + this.PageSize + "' href='Itle_NewDiscussionForum.aspx?id=" + articleId + "&amp;current=" + 1 + "&amp;pagesize=" + this.PageSize + "'>First</a>&nbsp;&nbsp;Prev&nbsp;&nbsp;<a title='Show All' href='Itle_NewDiscussionForum.aspx?id=" + articleId + "&amp;current=-1&amp;pagesize=" + this.PageSize + "'><b>" + (mycount - 1) + "</b> records</a>&nbsp;&nbsp;Next&nbsp;&nbsp;<a href='Itle_NewDiscussionForum.aspx?id=" + articleId + "&amp;current=" + (mycount - Convert.ToInt32(this.PageSize)) + "&amp;pagesize=" + this.PageSize + "' title ='Last " + this.PageSize + "'>Last</a>&nbsp;&nbsp;";
                }
                else
                {
                    lblPaging.Text = "<a  title ='First " + this.PageSize + "' href='Itle_NewDiscussionForum.aspx?id=" + articleId + "&amp;current=" + 1 + "&amp;pagesize=" + this.PageSize + "'>First</a>&nbsp;&nbsp;<a href='Itle_NewDiscussionForum.aspx?id=" + articleId + "&amp;pagesize=" + this.PageSize + "&amp;current=" + (currentCount - Convert.ToInt32(this.PageSize)) + "' title ='Previous " + this.PageSize + "'>Prev</a>&nbsp;&nbsp;<a title='Show All' href='Itle_NewDiscussionForum.aspx?id=" + articleId + "&amp;current=-1&amp;pagesize=" + this.PageSize + "'><b>" + (mycount - 1) + "</b> records</a>&nbsp;&nbsp;Next&nbsp;&nbsp;<a href='Itle_NewDiscussionForum.aspx?id=" + articleId + "&amp;current=" + (mycount - Convert.ToInt32(this.PageSize)) + "&amp;pagesize=" + this.PageSize + "' title ='Last " + this.PageSize + "'>Last</a>&nbsp;&nbsp;";
                }

            }

        }


        ltlPost.Text = sb.ToString();

        lbldate.Text = "Last Visit: " + lastVisit.ToLongTimeString() + ",  " + lastVisit.ToLongDateString();


    }

    public int PageSize
    {
        get
        {
            object o = ViewState["PageSize"];
            if ((o == null))
            {
                return 20;
            }
            return int.Parse(o.ToString());
        }
        set
        {
            ViewState["PageSize"] = value;
        }
    }

    #endregion

    #region Web Form Designer generated code

    override protected void OnInit(EventArgs e)
    {
        //
        // CODEGEN: This call is required by the ASP.NET Web Form Designer.
        //
        InitializeComponent();
        base.OnInit(e);
    }

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {

    }
    #endregion

    #region Page Events

    protected void ltlPost_Init(object sender, System.EventArgs e)
    {

    }

    protected void btnsetpaging_Click(object sender, System.EventArgs e)
    {

    }

    protected void txtpageSize_SelectedIndexChanged(object sender, System.EventArgs e)
    {

    }

    #endregion
}
