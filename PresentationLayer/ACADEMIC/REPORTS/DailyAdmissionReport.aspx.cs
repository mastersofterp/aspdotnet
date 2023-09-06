using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ACADEMIC_DailyAdmissionReport : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    StudentController objSC = new StudentController();
    Student objStudent = new Student();
    DataGrid dg = new DataGrid();
    DataSet ds = new DataSet();

    #region Page Events

    protected void Page_PreInit(object sender, EventArgs e)
    {
        // Set MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }

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
                //Page Authorization
                //CheckPageAuthorization();

                //lblPreSession.Text = Session["currentsession"].ToString();

                //DataTableReader dtr = objCommon.FillDropDown("ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO = " + Session["currentsession"].ToString(), string.Empty).CreateDataReader();
                //if (dtr.Read())
                //{
                //    lblPreSession.Text = dtr["SESSION_NAME"].ToString();
                //    lblPreSession.ToolTip = dtr["SESSIONNO"].ToString();
                //}
                //dtr.Close();

                //PopulateDropDown();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];

            }
            objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));  // Set Page Header  -  Added By Rishabh on 03/01/2022
            divMsg.InnerHtml = string.Empty;
        }
        //Blank Div
        //divMsg.InnerHtml = string.Empty;
    }



    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=DailyAdmissionReport.aspx.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=DailyAdmissionReport.aspx.aspx");
        }
    }

    #endregion

    private void ShowMessage(string msg)
    {
        this.divMsg.InnerHtml = "<script type='text/javascript' language='javascript'> alert('" + msg + "'); </script>";
    }

    //Report
    //protected void btnReport_Click(object sender, EventArgs e)
    //{
    //    if (txtToDate.Text == string.Empty)
    //    {
    //        txtToDate.Text = DateTime.Now.ToString();
    //    }
    //    DateTime repdatefrom = Convert.ToDateTime(txtFromDate.Text.Trim());
    //    DateTime repdateto = Convert.ToDateTime(txtToDate.Text.Trim());
    //    if (repdatefrom > DateTime.Now || repdateto > DateTime.Now)
    //    {
    //        ShowMessage("selected date is greater than current date..");
    //        return;
    //    }

    //    try
    //    {
    //        ShowReport("Admission_Register", "Stud_Admission_Register.rpt");
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objCommon.ShowError(Page, "ACADEMIC_StudentHorizontalReport.btnReport_Click()-> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objCommon.ShowError(Page, "Server UnAvailable");
    //    }


    //}



    protected void btnCancel_Click(object sender, EventArgs e)
    {
        // Reload/refresh complete page. 
        Response.Redirect(Request.Url.ToString());
    }

    //Excel Report
    protected void btnExcelReport_Click(object sender, EventArgs e)
    {

        try
        {
            if (rdoAdmissionReport.Checked == true)
            {
                GridView GVDayWiseAtt = new GridView();
                string ContentType = string.Empty;

                string fDate = txtFromDate.Text;
                string tDate = txtToDate.Text;
                if (tDate == "")
                {
                    tDate = txtFromDate.Text;
                }
                else
                {
                    tDate = txtToDate.Text;
                }


                DataSet ds = objSC.GetStudentBasicData(Convert.ToDateTime(fDate), Convert.ToDateTime(tDate));

                if (ds.Tables[0].Rows.Count > 0)
                {

                    GVDayWiseAtt.DataSource = ds;
                    GVDayWiseAtt.DataBind();

                    string attachment = "attachment;filename=StudentAdmissionData" + fDate + "-To-" + tDate + ".xls";
                    Response.ClearContent();
                    Response.AddHeader("content-disposition", attachment);
                    Response.ContentType = "application/vnd.MS-excel";
                    StringWriter sw = new StringWriter();
                    HtmlTextWriter htw = new HtmlTextWriter(sw);
                    GVDayWiseAtt.RenderControl(htw);
                    Response.Write(sw.ToString());
                    Response.End();
                }
                else
                {
                    objCommon.DisplayMessage("No Data Found for current selection.", this.Page);
                }
            }
            else if (rdoProspectusReport.Checked == true)
            {
                GridView GV = new GridView();
                string ContentType = string.Empty;

                string fDate = txtFromDate.Text;
                string tDate = txtToDate.Text;
                if (tDate == "")
                {
                    tDate = txtFromDate.Text;
                }
                else
                {
                    tDate = txtToDate.Text;
                }

                DataSet ds = objSC.GetProspectusData(Convert.ToDateTime(fDate), Convert.ToDateTime(tDate));

                if (ds.Tables[0].Rows.Count > 0)
                {

                    GV.DataSource = ds;
                    GV.DataBind();

                    string attachment = "attachment;filename=ProspectusReport" + fDate + "To" + tDate + ".xls";
                    Response.ClearContent();
                    Response.AddHeader("content-disposition", attachment);
                    Response.ContentType = "application/vnd.MS-excel";
                    StringWriter sw = new StringWriter();
                    HtmlTextWriter htw = new HtmlTextWriter(sw);
                    GV.RenderControl(htw);
                    Response.Write(sw.ToString());
                    Response.End();
                }
                else
                {
                    objCommon.DisplayMessage("No Data Found for current selection.", this.Page);
                }
            }
            else if (rdoStudNotConfirm.Checked == true)
            {
                GridView GVDayWiseAtt = new GridView();
                string ContentType = string.Empty;

                string fDate = txtFromDate.Text;
                string tDate = txtToDate.Text;
                if (tDate == "")
                {
                    tDate = txtFromDate.Text;
                }
                else
                {
                    tDate = txtToDate.Text;
                }


                DataSet ds = objSC.GetStudentBasicDataNOTConfirm(Convert.ToDateTime(fDate), Convert.ToDateTime(tDate));

                if (ds.Tables[0].Rows.Count > 0)
                {

                    GVDayWiseAtt.DataSource = ds;
                    GVDayWiseAtt.DataBind();

                    string attachment = "attachment;filename=StudentAdmissionNotConfirmData" + fDate + "-To-" + tDate + ".xls";
                    Response.ClearContent();
                    Response.AddHeader("content-disposition", attachment);
                    Response.ContentType = "application/vnd.MS-excel";
                    StringWriter sw = new StringWriter();
                    HtmlTextWriter htw = new HtmlTextWriter(sw);
                    GVDayWiseAtt.RenderControl(htw);
                    Response.Write(sw.ToString());
                    Response.End();
                }
                else
                {
                    objCommon.DisplayMessage("No Data Found for current selection.", this.Page);
                }
            }
            else
            {

            }
        }
        catch (Exception ex)
        {
            throw;
        }

    }

    protected void rdoProspectusReport_CheckedChanged(object sender, EventArgs e)
    {

    }

    protected void rdoBankwiseDDReport_CheckedChanged(object sender, EventArgs e)
    {

    }
}