using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClosedXML.Excel;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class ACADEMIC_NewStudentEntryReport : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentController objSC = new StudentController();
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["masterpage"] != null)
        {
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        }
        else
        {
            objCommon.SetMasterPage(Page, "");
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                CheckPageAuthorization();
                Page.Title = Session["coll_name"].ToString();
            }
            objCommon.SetLabelData("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]));//Set label 
            objCommon.FillDropDownList(ddlAdmbatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>0 AND ACTIVESTATUS=1", "BATCHNO DESC");
        }
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=NewStudentEntryReport.aspx");
            }
        }
        else
        {
            Response.Redirect("~/notauthorized.aspx?page=NewStudentEntryReport.aspx");
        }
    }
    protected void butShow_Click(object sender, EventArgs e)
    {
        DataSet ds = null;
        ds = objSC.GetNewStudentEntryData(Convert.ToInt32(ddlAdmbatch.SelectedValue), Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTdate.Text),Convert.ToInt32(Session["userno"]));
        if (ds.Tables[0].Rows.Count > 0)
        {
            lvStudent.DataSource = ds;
            lvStudent.DataBind();
            objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvStudent);//Set label -
        }
        else
        {
            objCommon.DisplayMessage(updPanel, "No Record Found.", this.Page);
        }
    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {

        {
            try
            {
                GridView GVDayWiseAtt = new GridView();

                DataSet ds = null;
                ds = objSC.GetNewStudentEntryData(Convert.ToInt32(ddlAdmbatch.SelectedValue), Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTdate.Text),Convert.ToInt32(Session["userno"]));
                if (ds.Tables[0].Rows.Count > 0)


                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        GVDayWiseAtt.DataSource = ds;
                        GVDayWiseAtt.DataBind();

                        string attachment = "attachment; filename=NewStudentEntryReport.xls";
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
                        objCommon.DisplayMessage(this.updPanel, "No Data Found for current selection.", this.Page);
                    }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }


}