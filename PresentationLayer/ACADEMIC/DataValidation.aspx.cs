//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC
// PAGE NAME     : DataValidation                                 
// CREATION DATE : 16-11-2023
// ADDED BY      : Vipul R Tichakule                                               
// ADDED DATE    :
// MODIFIED BY   : 
// MODIFIED DESC :                                                    
//======================================================================================

using IITMS;
using IITMS.UAIMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.IO;
using OfficeOpenXml;
using ClosedXML.Excel;


public partial class ACADEMIC_DataValidation : System.Web.UI.Page
{
    Common objCommon = new Common();
    User_AccController checkdv = new User_AccController();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
            BindListView();
    }


    //protected void rdoPurpose_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    dvSession.Visible = false;
    //    btnShow_Click(sender, e);
    //    //if (Convert.ToInt32(rdoDataValidate.SelectedValue) == 1)
    //    //{
    //    //    Lvdatav.DataSource = null;
    //    //    Lvdatav.DataBind();              
    //    //    Lvdatav.Visible = true;
           
    //    //}
    //    //else if (Convert.ToInt32(rdoDataValidate.SelectedValue) == 2)
    //    //{
    //    //    Lvdatav.Visible = false;
    //    //   // dvSession.Visible = true;
    //    //    this.objCommon.FillDropDownList(ddlSession, "ACD_STUDENT_RESULT S INNER JOIN ACD_SESSION_MASTER SM ON S.SESSIONNO=SM.SESSIONNO INNER JOIN ACD_SESSION SD ON SM.SESSIONID=SD.SESSIONID",
    //    //        "DISTINCT SD.SESSIONID", "SD.SESSION_NAME", "ISNULL(SM.FLOCK,0)=1 AND ISNULL(S.CANCEL,0)=0", "SD.SESSIONID DESC");
    //    //}
    //}

    //protected void btnShow_Click(object sender, EventArgs e)
    //{
    //    //btnCheckHealth.Visible = true;
    //    //if (Convert.ToInt32(rdoDataValidate.SelectedValue) == 1)
    //    BindListView(rdoDataValidate.SelectedValue, 0);
    //}

    //protected void btnCheckHealth_Click(object sender, EventArgs e)
    //{
    //    string value = string.Empty;       
    //    foreach (ListViewDataItem items in Lvdatav.Items)
    //    {
    //        Label checkdate = (Label)items.FindControl("lblCurrentd");
    //        CheckBox checkdataV = (CheckBox)items.FindControl("CheckDV");
    //        HiddenField hdvalue = (HiddenField)items.FindControl("hdid");
    //        if (checkdataV.Checked)
    //        {
    //            //checkdate.Text = Convert.ToString(DateTime.Now);    
    //            value += hdvalue.Value + ",";
    //        }
    //    }
    //    value = value.TrimEnd(',');

    //    int rresult = checkdv.UpdateDataValidation(value);
    //}


    protected void btnDownload_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btndownload = sender as ImageButton;
        int ID = int.Parse(btndownload.CommandArgument);
        //int sessionno = 0;
        //if (Convert.ToInt32(rdoDataValidate.SelectedValue) != 1)
        //    sessionno = Convert.ToInt32(ddlSession.SelectedValue);

        DataSet ds = checkdv.Downloaddatavalidate(ID);
        GridView GV = new GridView();
        if (ds != null && ds.Tables.Count > 0)
        {
            using (XLWorkbook wb = new XLWorkbook())
            {
                foreach (System.Data.DataTable dt in ds.Tables)
                {
                    //Add System.Data.DataTable as Worksheet.
                    if (dt != null && dt.Rows.Count > 0)
                        wb.Worksheets.Add(dt);
                }
                //Export the Excel file.
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename= DataSanityReport_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
        }
        else
        {
            objCommon.DisplayMessage("No Record Found", this.Page);
            return;
        }
    }
    //protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlSession.SelectedIndex > 0)
    //    {
    //        if (Convert.ToInt32(rdoDataValidate.SelectedValue) == 2)
    //            BindListView(rdoDataValidate.SelectedValue, Convert.ToInt16(ddlSession.SelectedIndex));
    //    }
    //}

    private void BindListView()
    {
        try
        {
            DataSet ds = checkdv.GetDataValidation();
            if (ds.Tables[0].Rows.Count > 0)
            {
                Lvdatav.DataSource = null;
                Lvdatav.DataSource = ds;
                Lvdatav.DataBind();

                foreach (ListViewDataItem dataitem in Lvdatav.Items)
                {
                    Label countcheck = (Label)dataitem.FindControl("lblCheck");
                    Label count = (Label)dataitem.FindControl("lblEntryCount");
                    ImageButton btndown = (ImageButton)dataitem.FindControl("btnDownload");
                    int ecount = Convert.ToInt32(count.Text);
                    if (ecount > 0)
                    {
                        countcheck.Text = "Discrepancy";
                        countcheck.ForeColor = System.Drawing.Color.Red;
                        btndown.Visible = true;
                    }
                    else
                    {
                        countcheck.Text = "Ok";
                        countcheck.ForeColor = System.Drawing.Color.Green;
                        btndown.Visible = false;
                    }
                }
            }
            else
            {
                objCommon.DisplayUserMessage(this.Page, "No Record Found", this.Page);
            }
        }
        catch
        {
            throw;
        }
    }
}