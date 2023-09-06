using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VEHICLE_MAINTENANCE_Reports_StudentTransportIdCard : System.Web.UI.Page
{
    Common objCommon = new Common();
    VMController objvmcon = new VMController();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
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
                    //Page Authorization
                    CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                  
                }


                objCommon.FillDropDownList(ddlRoute, "VEHICLE_ROUTEMASTER", "ROUTEID", "ROUTENAME", "", "");

            }
            else
            {
                //objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER A INNER JOIN ACD_STUDENT B ON (A.SEMESTERNO=B.SEMESTERNO)", "DISTINCT A.SEMESTERNO", "A.SEMESTERNAME", "A.SEMESTERNO>0 AND ADMBATCH=" + ddlAdmbatch.SelectedValue, "A.SEMESTERNO");
            }

           // divMsg.InnerHtml = string.Empty;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=StudentTransportIdCard.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=StudentTransportIdCard.aspx");
        }
    }

    //protected void btnbackReport_Click(object sender, EventArgs e)
    //{
    //    string ids ="1449";
    //    ShowReportDefault(ids, "Student_ID_Card_Report", "StudentVehTransportIDCardCresent.rpt");
    //}

    
    protected void btnShow_Click(object sender, EventArgs e)
    {
        BindStudentDetails();
    }

    public void BindStudentDetails()
    {

        try
        {
            
            DataSet ds;
            ds = objvmcon.GetStudentTransportListForIdentityCard(Convert.ToInt32(ddlRoute.SelectedValue));
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                lvStudentTransportRecords.DataSource = ds;
                lvStudentTransportRecords.DataBind();
                //objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvStudentRecords);//Set label -
                //hftot.Value = ds.Tables[0].Rows.Count.ToString();
            }
            else
            {
                lvStudentTransportRecords.DataSource = null;
                lvStudentTransportRecords.DataBind();
                objCommon.DisplayMessage(this.updStudent, "Record Not Found!!", this.Page);
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void btnIdCardReport_Click(object sender, EventArgs e)
    {
        string ids = GetStudentIDs();

        ShowReportDefault(ids, "Student_Transport_ID_Card_Report", "StudentVehTransportIDCardCresent.rpt");
      
    }

    private void ShowReportDefault(string param, string reportTitle, string rptFileName)
    {
        try
        {
           // string url = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("vehicle_maintenance")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,vehicle_maintenance," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO="+param+",@P_OrganizationId=" + Convert.ToInt32(Session["OrgId"]);
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.updStudent, this.updStudent.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private string GetStudentIDs()
    {
        string studentIds = string.Empty;
        try
        {
            foreach (ListViewDataItem item in lvStudentTransportRecords.Items)
            {
                if ((item.FindControl("chkReport") as CheckBox).Checked)
                {
                    if (studentIds.Length > 0)
                        studentIds += "$";
                    studentIds += (item.FindControl("hidIdNo") as HiddenField).Value.Trim();
                    //GenerateQrCode(studentIds);
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
        return studentIds;
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ddlRoute.SelectedValue = "0";
        lvStudentTransportRecords.DataSource = null;
        lvStudentTransportRecords.DataBind();
    }
}