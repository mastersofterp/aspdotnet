using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;

public partial class ITLE_ITLE_TEST_Preview : System.Web.UI.Page
{

    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ITLETestController objAC = new ITLETestController();
    CourseControlleritle objCourse = new CourseControlleritle();
    ITestMaster objTM = new ITestMaster();
    string PageId;



    #region Page Load

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
        //CheckBrowser();
        if (!Page.IsPostBack)
        {

            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                if (Convert.ToInt32(Session["usertype"]) == 2)
                {
                    objCommon.DisplayMessage(UpdatePanel1, "You are not authorized for the Test Please contact Administrator!", this.Page);
                    return;
                }
                //Page Authorization
                //CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                lblAutoPreview.Visible = false;
                btnStopPreview.Visible = false;
                PageId = Request.QueryString["pageno"];
                FillDropdown();
                dvPreview.Visible = false;

                //BindListView();


            }

        }

    }

    #endregion



    #region Public Method


    public string GetIP()
    {
        string Str = "";
        Str = System.Net.Dns.GetHostName();
        IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(Str);
        IPAddress[] addr = ipEntry.AddressList;
        return addr[addr.Length - 1].ToString();

    }

    public static string GetRemoteIP()
    {
        string strValue = "";
        //Gets a comma-delimited list of IP Addresses
        string ipList = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

        //If any are available - use the first one
        if (!string.IsNullOrEmpty(ipList))
        {
            strValue = ipList.Split(',')[0];
        }
        else
        {
            strValue = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        }
        return strValue;
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
                Response.Redirect("~/notauthorized.aspx?page=ITLE_Exam_TestPreview.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=ITLE_Exam_TestPreview.aspx");
        }
    }

    private void Bindchart()
    {

        DataSet ds = objAC.GetIETestStatusForChart(Convert.ToInt32(ddlTest.SelectedValue));


        DataTable ChartData = ds.Tables[0];

        string[] XPointMember = new string[3];
        int[] YPointMember = new int[3];


        int pending = 0;
        int attend = 0;
        int absent = 0;
        if (!String.IsNullOrEmpty(ChartData.Rows[0]["PENDING"].ToString()))
        {
            pending = Convert.ToInt32(ChartData.Rows[0]["PENDING"]);
        }
        if (!String.IsNullOrEmpty(ChartData.Rows[0]["ATTEND"].ToString()))
        {
            attend = Convert.ToInt32(ChartData.Rows[0]["ATTEND"].ToString());
        }
        if (!String.IsNullOrEmpty(ChartData.Rows[0]["ABSENT"].ToString()))
        {
            absent = Convert.ToInt32(ChartData.Rows[0]["ABSENT"]);
        }
        int complete = attend - pending;
        if (complete != 0)
        {
            //storing Values for X axis  
            XPointMember[0] = "Completed";
            //storing values for Y Axis  
            YPointMember[0] = complete;
        }

        if (pending != 0)
        {
            //storing Values for X axis  
            XPointMember[1] = "Pending";
            //storing values for Y Axis  
            YPointMember[1] = pending;

        }
        //storing Values for X axis  
        XPointMember[2] = "Absent";
        //storing values for Y Axis  
        YPointMember[2] = absent;

        //}
        //binding chart control  
        Chart1.Series[0].Points.DataBindXY(XPointMember, YPointMember);

        //Setting width of line  
        Chart1.Series[0].BorderWidth = 10;
        //setting Chart type   
        Chart1.Series[0].ChartType = SeriesChartType.Pie;


        foreach (Series charts in Chart1.Series)
        {
            foreach (DataPoint point in charts.Points)
            {
                switch (point.AxisLabel)
                {
                    case "Absent": point.Color = Color.IndianRed; break;
                    case "Completed": point.Color = Color.YellowGreen; break;
                    case "Pending": point.Color = Color.LightBlue; break;
                }
                point.Label = string.Format("{0:0} - {1}", point.YValues[0], point.AxisLabel);

            }
        }


        //Enabled 3D  
        Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
        Chart1.Legends[0].Enabled = true;


    }

    private void FillDropdown()
    {
        try
        {

            objCommon.FillDropDownList(ddlTest, "ACD_ITESTMaster", "TESTNO", "TESTNAME", "UA_NO=" + Convert.ToInt32(Session["userno"]) + "AND COURSENO=" + Convert.ToInt32(Session["ICourseNo"]) + " AND SESSIONNO=" + Convert.ToInt32(Session["SessionNo"]) + "AND TEST_TYPE='O'", "TESTNO");


        }
        catch (Exception ex)
        {
            objUCommon.ShowError(Page, "ITLE_Exam_TestPreview.FillDropdown-> " + ex.Message + " " + ex.StackTrace);

        }
    }

    private void BindListView()
    {
        try
        {
            BindTestAppearing();
            BindTestAbsent();

        }
        catch (Exception ex)
        {
            objCommon.DisplayMessage(UpdatePanel1, "ITLE_StudTest.aspx.BindListView->  " + ex.Message + ex.StackTrace, this.Page);
        }
    }

    private void BindTestAppearing()
    {

        DataSet ds = null;


        if (Convert.ToInt32(Session["usertype"]) != 2)
        {
            ds = objAC.GetIETestPreview(Convert.ToInt32(ddlTest.SelectedValue));
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvTest.DataSource = ds;
                lvTest.DataBind();
                dvPreview.Visible = true;
                dvSelectTest.Visible = false;
            }
            else
            {
                lvTest.DataSource = null;
                lvTest.DataBind();
                dvPreview.Visible = true;
                dvSelectTest.Visible = false;
            }
        }
    }

    private void BindTestAbsent()
    {

        DataSet ds = objAC.GetIETestAbsentList(Convert.ToInt32(ddlTest.SelectedValue));
        lblAbsentCount.Text = ds.Tables[0].Rows.Count.ToString();
        lvAbsent.DataSource = ds;
        lvAbsent.DataBind();
    }

    #endregion



    #region Page Events

    protected void btnBack_Click(object sender, EventArgs e)
    {
        dvPreview.Visible = false;
        dvSelectTest.Visible = true;
        ddlTestPrevType.SelectedValue = "0";
    }





    protected void ddlTestPrevType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlTestPrevType.SelectedValue == "0")
        {
            BindListView();
            //dvChart.Visible = false;
            dvDetailsPrev.Visible = true;
        }
        else
        {
            Bindchart();
            dvChart.Visible = true;
            dvDetailsPrev.Visible = false;
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        lblAutoPreview.Visible = false;
        btnStopPreview.Visible = false;
        btnStartPreview.Visible = true;
        if (ddlTestPrevType.SelectedValue == "0")
        {
            BindListView();
            dvChart.Visible = false;
            dvDetailsPrev.Visible = true;
        }
        else
        {
            Bindchart();
            dvChart.Visible = true;
            dvDetailsPrev.Visible = false;
        }
    }

    protected void btnStartPreview_Click(object sender, EventArgs e)
    {
        if (ddlTestPrevType.SelectedValue == "0")
        {
            BindListView();
            dvChart.Visible = false;
            dvDetailsPrev.Visible = true;
        }
        else
        {
            Bindchart();
            dvChart.Visible = true;
            dvDetailsPrev.Visible = false;
        }
        lblAutoPreview.Visible = true;
        btnStartPreview.Visible = false;
        btnStopPreview.Visible = true;
    }
    #endregion


    protected void btnStopPreview_Click(object sender, EventArgs e)
    {
        if (ddlTestPrevType.SelectedValue == "0")
        {
            BindListView();
            dvChart.Visible = false;
            dvDetailsPrev.Visible = true;
        }
        else
        {
            Bindchart();
            dvChart.Visible = true;
            dvDetailsPrev.Visible = false;
        }
        lblAutoPreview.Visible = false;
        btnStartPreview.Visible = true;
        btnStopPreview.Visible = false;
    }
    protected void btnBack_Click1(object sender, EventArgs e)
    {
        dvPreview.Visible = false;
        dvSelectTest.Visible = true;
        ddlTestPrevType.SelectedValue = "0";
    }
    protected void btnAbsentStud_Click(object sender, EventArgs e)
    {
        LinkButton btnAbsentStud = sender as LinkButton;
        int idno = Convert.ToInt32(int.Parse(btnAbsentStud.CommandArgument));
        DisplayStudentInformation(idno);
        dvIntermediate.Visible = true;
        dvShowStudent.Visible = true;
    }

    protected void DisplayStudentInformation(int idno)
    {
        DataSet ds = objCommon.FillDropDown("ACD_STUDENT S INNER JOIN ACD_DEGREE D ON (S.DEGREENO=D.DEGREENO) INNER JOIN ACD_BRANCH B ON (S.BRANCHNO=B.BRANCHNO) INNER JOIN ACD_SEMESTER SM ON (S.SEMESTERNO=SM.SEMESTERNO)", "S.IDNO,S.STUDNAME,S.REGNO,SM.SEMESTERNAME,B.LONGNAME", "D.DEGREENAME,S.STUDENTMOBILE,S.EMAILID", "S.IDNO=" + idno, "S.REGNO");
        lblPopStudName.Text = String.IsNullOrEmpty(ds.Tables[0].Rows[0]["STUDNAME"].ToString()) ? "-" : ds.Tables[0].Rows[0]["STUDNAME"].ToString();
        lblPopDegree.Text = String.IsNullOrEmpty(ds.Tables[0].Rows[0]["DEGREENAME"].ToString()) ? "-" : ds.Tables[0].Rows[0]["DEGREENAME"].ToString();
        lblPopBranch.Text = String.IsNullOrEmpty(ds.Tables[0].Rows[0]["LONGNAME"].ToString()) ? "-" : ds.Tables[0].Rows[0]["LONGNAME"].ToString();
        lblPopRegno.Text = String.IsNullOrEmpty(ds.Tables[0].Rows[0]["REGNO"].ToString()) ? "-" : ds.Tables[0].Rows[0]["REGNO"].ToString();
        lblPopSem.Text = String.IsNullOrEmpty(ds.Tables[0].Rows[0]["SEMESTERNAME"].ToString()) ? "-" : ds.Tables[0].Rows[0]["SEMESTERNAME"].ToString();
        lblPopPhone.Text = String.IsNullOrEmpty(ds.Tables[0].Rows[0]["STUDENTMOBILE"].ToString()) ? "-" : ds.Tables[0].Rows[0]["STUDENTMOBILE"].ToString();
        lblPopMail.Text = String.IsNullOrEmpty(ds.Tables[0].Rows[0]["EMAILID"].ToString()) ? "-" : ds.Tables[0].Rows[0]["EMAILID"].ToString();
        imgPhoto.ImageUrl = "~/showimage.aspx?id=" + ds.Tables[0].Rows[0]["IDNO"].ToString() + "&type=student";
    }
    protected void btnCloseModal_Click(object sender, EventArgs e)
    {
        dvIntermediate.Visible = false;
        dvShowStudent.Visible = false;
    }
    protected void btnPrev_Click(object sender, EventArgs e)
    {
        if (ddlTestPrevType.SelectedValue == "0")
        {
            BindListView();
            dvChart.Visible = false;
            dvDetailsPrev.Visible = true;
        }
        else
        {
            Bindchart();
            dvChart.Visible = true;
            dvDetailsPrev.Visible = false;
        }
    }
    protected void lvTest_ItemDataBound(object sender, ListViewItemEventArgs e)
    {

        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            // Display the email address in italics.
            Label lblStudName = (Label)e.Item.FindControl("lblStudName");
            Label lblRoll = (Label)e.Item.FindControl("Label1");
            ListView lvPrev = (ListView)e.Item.FindControl("lvPreview");

            string tdno = lblStudName.ToolTip.ToString();
            int idno = Convert.ToInt32(lblRoll.ToolTip.ToString());
            DataSet ds = objCommon.FillDropDown("ACD_ITLE_RESULTCOPY", "ROW_NUMBER() OVER(ORDER BY TESTNO) AS SRNO", "QUESTIONNO,SELECTED,CASE WHEN ANS_STAT = 'S' THEN 'btn btn-success' WHEN ANS_STAT = 'R' THEN 'btn btn-outline-primary' WHEN ANS_STAT = 'N' THEN 'btn btn-warning' ELSE 'btn btn-outline-danger' END AS ANS_STAT", "TESTNO=" + tdno + "AND IDNO=" + idno, "");
            lvPrev.DataSource = ds;
            lvPrev.DataBind();

            //System.Data.DataRowView rowView = e.Item.DataItem as System.Data.DataRowView;
            //string currentEmailAddress = rowView["EmailAddress"].ToString();
            //if (currentEmailAddress == "orlando0@adventure-works.com")
            //{
            //    EmailAddressLabel.Font.Bold = true;
            //}
        }
    }
}