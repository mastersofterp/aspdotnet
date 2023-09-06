using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

public partial class HOSTEL_MessCommMember : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    MessBillController objCom = new MessBillController();
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
        try
        {
            //Check Session
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }

            if (!Page.IsPostBack)
            {
                //Page Authorization
                //CheckPageAuthorization();
                FillDropdownList();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
            }

            ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
            divMsg.InnerHtml = string.Empty;
            lvDetails.DataSource = null;
            lvDetails.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "HOSTEL_MessCommMember.Page_Load-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }
    }

    private void FillDropdownList()
    {
        objCommon.FillDropDownList(ddlSession, "ACD_HOSTEL_SESSION", "HOSTEL_SESSION_NO", "SESSION_NAME", "FLOCK=1", "HOSTEL_SESSION_NO DESC");
        ddlSession.SelectedIndex = 1;
        if (Session["usertype"].ToString() == "1")
            objCommon.FillDropDownList(ddlHostel, "ACD_HOSTEL", "HOSTEL_NO", "HOSTEL_NAME", "HOSTEL_NO>0", "HOSTEL_NO");
        else
            objCommon.FillDropDownList(ddlHostel, "ACD_HOSTEL H INNER JOIN USER_ACC U ON (HOSTEL_NO=UA_EMPDEPTNO)", "HOSTEL_NO", "HOSTEL_NAME", "HOSTEL_NO>0 and UA_NO=" + Convert.ToInt32(Session["userno"].ToString()), "HOSTEL_NO");

        objCommon.FillDropDownList(ddlMess, "ACD_HOSTEL_MESS", "MESS_NO", "MESS_NAME", "MESS_NO>0", "MESS_NO");
    }
    protected void ddlMess_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlMember1, "ACD_HOSTEL_ROOM_ALLOTMENT A INNER JOIN ACD_STUDENT B ON(A.RESIDENT_NO=B.IDNO)", "DISTINCT A.RESIDENT_NO", "DBO.FN_DESC('NAME',A.RESIDENT_NO) AS STUDNAME", "A.HOSTEL_SESSION_NO="+Convert.ToInt32(ddlSession.SelectedValue)+ " AND A.HOSTEL_NO="+Convert.ToInt32(ddlHostel.SelectedValue)+"AND A.MESS_NO ="+Convert.ToInt32(ddlMess.SelectedValue), "RESIDENT_NO");
        objCommon.FillDropDownList(ddlMember2, "ACD_HOSTEL_ROOM_ALLOTMENT A INNER JOIN ACD_STUDENT B ON(A.RESIDENT_NO=B.IDNO)", "DISTINCT A.RESIDENT_NO", "DBO.FN_DESC('NAME',A.RESIDENT_NO) AS STUDNAME", "A.HOSTEL_SESSION_NO="+Convert.ToInt32(ddlSession.SelectedValue)+ " AND A.HOSTEL_NO="+Convert.ToInt32(ddlHostel.SelectedValue)+"AND A.MESS_NO ="+Convert.ToInt32(ddlMess.SelectedValue), "RESIDENT_NO");
        objCommon.FillDropDownList(ddlMember3, "ACD_HOSTEL_ROOM_ALLOTMENT A INNER JOIN ACD_STUDENT B ON(A.RESIDENT_NO=B.IDNO)", "DISTINCT A.RESIDENT_NO", "DBO.FN_DESC('NAME',A.RESIDENT_NO) AS STUDNAME", "A.HOSTEL_SESSION_NO="+Convert.ToInt32(ddlSession.SelectedValue)+ " AND A.HOSTEL_NO="+Convert.ToInt32(ddlHostel.SelectedValue)+"AND A.MESS_NO ="+Convert.ToInt32(ddlMess.SelectedValue), "RESIDENT_NO");
        objCommon.FillDropDownList(ddlMember4, "ACD_HOSTEL_ROOM_ALLOTMENT A INNER JOIN ACD_STUDENT B ON(A.RESIDENT_NO=B.IDNO)", "DISTINCT A.RESIDENT_NO", "DBO.FN_DESC('NAME',A.RESIDENT_NO) AS STUDNAME", "A.HOSTEL_SESSION_NO="+Convert.ToInt32(ddlSession.SelectedValue)+ " AND A.HOSTEL_NO="+Convert.ToInt32(ddlHostel.SelectedValue)+"AND A.MESS_NO ="+Convert.ToInt32(ddlMess.SelectedValue), "RESIDENT_NO");
        objCommon.FillDropDownList(ddlMember5, "ACD_HOSTEL_ROOM_ALLOTMENT A INNER JOIN ACD_STUDENT B ON(A.RESIDENT_NO=B.IDNO)", "DISTINCT A.RESIDENT_NO", "DBO.FN_DESC('NAME',A.RESIDENT_NO) AS STUDNAME", "A.HOSTEL_SESSION_NO="+Convert.ToInt32(ddlSession.SelectedValue)+ " AND A.HOSTEL_NO="+Convert.ToInt32(ddlHostel.SelectedValue)+"AND A.MESS_NO ="+Convert.ToInt32(ddlMess.SelectedValue), "RESIDENT_NO");   
       //DataSet ds =objCommon.FillDropDown(" ACD_HOSTEL_ROOM_ALLOTMENT INNER JOIN ACD_HOSTEL_COMM_MEMBER B ON (A.HOSTEL_SESSION_NO = B.SESSION_NO) INNER JOIN 
        Filllistview();		  
       
    }
    //protected void ddlMember1_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    objCommon.FillDropDownList(ddlMember2, "ACD_HOSTEL_ROOM_ALLOTMENT A INNER JOIN ACD_STUDENT B ON(A.RESIDENT_NO=B.IDNO)", "DISTINCT A.RESIDENT_NO", "DBO.FN_DESC('NAME',A.RESIDENT_NO) AS STUDNAME", "A.HOSTEL_SESSION_NO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND A.HOSTEL_NO=" + Convert.ToInt32(ddlHostel.SelectedValue) + "AND A.MESS_NO =" + Convert.ToInt32(ddlMess.SelectedValue) +" AND A.RESIDENT_NO <> "+Convert.ToInt32(ddlMember1.SelectedValue), "RESIDENT_NO");
    //}
    //protected void ddlMember2_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    objCommon.FillDropDownList(ddlMember3, "ACD_HOSTEL_ROOM_ALLOTMENT A INNER JOIN ACD_STUDENT B ON(A.RESIDENT_NO=B.IDNO)", "DISTINCT A.RESIDENT_NO", "DBO.FN_DESC('NAME',A.RESIDENT_NO) AS STUDNAME", "A.HOSTEL_SESSION_NO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND A.HOSTEL_NO=" + Convert.ToInt32(ddlHostel.SelectedValue) + "AND A.MESS_NO =" + Convert.ToInt32(ddlMess.SelectedValue) + " AND A.RESIDENT_NO <> " + Convert.ToInt32(ddlMember1.SelectedValue) +" AND A.RESIDENT_NO <> " + Convert.ToInt32(ddlMember2.SelectedValue), "RESIDENT_NO");
    //}
    //protected void ddlMember3_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    objCommon.FillDropDownList(ddlMember4, "ACD_HOSTEL_ROOM_ALLOTMENT A INNER JOIN ACD_STUDENT B ON(A.RESIDENT_NO=B.IDNO)", "DISTINCT A.RESIDENT_NO", "DBO.FN_DESC('NAME',A.RESIDENT_NO) AS STUDNAME", "A.HOSTEL_SESSION_NO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND A.HOSTEL_NO=" + Convert.ToInt32(ddlHostel.SelectedValue) + "AND A.MESS_NO =" + Convert.ToInt32(ddlMess.SelectedValue) + " AND A.RESIDENT_NO <> " + Convert.ToInt32(ddlMember1.SelectedValue) + " AND A.RESIDENT_NO <> " + Convert.ToInt32(ddlMember2.SelectedValue) +" AND A.RESIDENT_NO <> "+ Convert.ToInt32(ddlMember3.SelectedValue), "RESIDENT_NO");
    //}  
    //protected void ddlMember4_SelectedIndexChanged1(object sender, EventArgs e)
    //{
    //    objCommon.FillDropDownList(ddlMember5, "ACD_HOSTEL_ROOM_ALLOTMENT A INNER JOIN ACD_STUDENT B ON(A.RESIDENT_NO=B.IDNO)", "DISTINCT A.RESIDENT_NO", "DBO.FN_DESC('NAME',A.RESIDENT_NO) AS STUDNAME", "A.HOSTEL_SESSION_NO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND A.HOSTEL_NO=" + Convert.ToInt32(ddlHostel.SelectedValue) + "AND A.MESS_NO =" + Convert.ToInt32(ddlMess.SelectedValue) + " AND A.RESIDENT_NO <> " + Convert.ToInt32(ddlMember1.SelectedValue) + " AND A.RESIDENT_NO <> " + Convert.ToInt32(ddlMember2.SelectedValue) + " AND A.RESIDENT_NO <> " + Convert.ToInt32(ddlMember3.SelectedValue) + " AND A.RESIDENT_NO <> " + Convert.ToInt32(ddlMember4.SelectedValue), "RESIDENT_NO");
    //}
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int status = -99;
            int Can = 0;
            //DateTime candate
            int a = Convert.ToInt32(Session["userno"].ToString());
            string c = Session["colcode"].ToString();
            string ipadd = ViewState["ipAddress"].ToString();
            status = objCom.AddCommiteeMember(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlHostel.SelectedValue), Convert.ToInt32(ddlMess.SelectedValue), Convert.ToInt32(ddlMember1.SelectedValue), Convert.ToInt32(ddlMember2.SelectedValue), Convert.ToInt32(ddlMember3.SelectedValue), Convert.ToInt32(ddlMember4.SelectedValue), Convert.ToInt32(ddlMember5.SelectedValue), Convert.ToDateTime(DateTime.Now.ToShortDateString()), Convert.ToInt32(Session["userno"].ToString()), ViewState["ipAddress"].ToString(), Can, Session["colcode"].ToString());
        
        if (status == 1)
        {
            objCommon.DisplayMessage("Record Saved Successfully", this.Page);
            Filllistview();
        }
        else
            objCommon.DisplayMessage("Transaction Failed!!", this.Page);
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (ddlMess.SelectedIndex == 0)
        {
            objCommon.DisplayMessage("Please Select Mess", this.Page);
            return;
        }


      //  canstatus = objCom.AddCommiteeMember(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlHostel.SelectedValue), Convert.ToInt32(ddlMess.SelectedValue), Convert.ToInt32(ddlMember1.SelectedValue), Convert.ToInt32(ddlMember2.SelectedValue), Convert.ToInt32(ddlMember3.SelectedValue), Convert.ToInt32(ddlMember4.SelectedValue), Convert.ToInt32(ddlMember5.SelectedValue), Convert.ToDateTime(DateTime.Now.ToShortDateString()), Convert.ToInt32(Session["userno"].ToString()), ViewState["ipAddress"].ToString(), Can, Session["colcode"].ToString());
        ImageButton editButton = sender as ImageButton;
        int COMMNO = Int32.Parse(editButton.CommandArgument);
        DataSet ds = objCommon.FillDropDown("ACD_HOSTEL_COMM_MEMBER", "DISTINCT MEMBER_ID1", "COMM_NO,MESS_NO,MEMBER_ID2,MEMBER_ID3,MEMBER_ID4,MEMBER_ID5", "MESS_NO="+Convert.ToInt32(ddlMess.SelectedValue)+" AND SESSION_NO="+Convert.ToInt32(ddlSession.SelectedValue)+" AND HOSTEL_NO="+Convert.ToInt32(ddlHostel.SelectedValue) +" AND COMM_NO=" + COMMNO, string.Empty);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlMember1.SelectedValue = ds.Tables[0].Rows[0]["MEMBER_ID1"].ToString();
            ddlMember2.SelectedValue = ds.Tables[0].Rows[0]["MEMBER_ID2"].ToString();
            ddlMember3.SelectedValue = ds.Tables[0].Rows[0]["MEMBER_ID3"].ToString();
            ddlMember4.SelectedValue = ds.Tables[0].Rows[0]["MEMBER_ID4"].ToString();
            ddlMember5.SelectedValue = ds.Tables[0].Rows[0]["MEMBER_ID5"].ToString();
           // ViewState["action"] = "edit";
           // ViewState["MESS_NO"] = ds.Tables[0].Rows[0]["MESS_NO"].ToString();
        }
        
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        if (ddlSession.SelectedIndex > 0)
        {
            ShowReport("Hostel Commitee Member Report", "rptHostelCommMember.rpt");
        }
        else
        {
            objCommon.DisplayMessage("Please Select Session  !!", this.Page);
        }
    }
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("hostel")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,HOSTEL," + rptFileName;
            url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_HOSTELNO=" + Convert.ToInt32(ddlHostel.SelectedValue) + ",@P_MESSNO="+Convert.ToInt32(ddlMess.SelectedValue)+",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "HOSTEL_REPORT_HostelFineReport.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void ddlHostel_SelectedIndexChanged(object sender, EventArgs e)
    {
        Filllistview();
 
        ddlMess.SelectedIndex = 0;
        ddlMember1.Items.Clear();
        ddlMember1.Items.Add(new ListItem("Please Select", "0"));
        ddlMember2.Items.Clear();
        ddlMember2.Items.Add(new ListItem("Please Select", "0"));
        ddlMember3.Items.Clear();
        ddlMember3.Items.Add(new ListItem("Please Select", "0"));
        ddlMember4.Items.Clear();
        ddlMember4.Items.Add(new ListItem("Please Select", "0"));
        ddlMember5.Items.Clear();
        ddlMember5.Items.Add(new ListItem("Please Select", "0"));
    }
    private void Filllistview()
    {
        DataSet ds = objCom.GetAllCommiteeMember(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlHostel.SelectedValue), Convert.ToInt32(ddlMess.SelectedValue));
        if (ds.Tables[0].Rows.Count > 0)
        {
            lvDetails.DataSource = ds;
            lvDetails.DataBind();

        }
        else
        {
            lvDetails.DataSource = null;
            lvDetails.DataBind();
        }
    }
}
