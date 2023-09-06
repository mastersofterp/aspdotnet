using System;
using System.Collections;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.IO;
using System.Data.SqlClient;

using System.Data;
using System.Data.OleDb;
using System.Data.Common;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;


public partial class ACADEMIC_StudentFileUpload : System.Web.UI.Page
{
    UAIMS_Common objUCommon = new UAIMS_Common();
    Common objCommon = new Common();
    StudentController studcon = new StudentController();
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

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                    PopulateDropDownList();
                }
                lblTotalMsg.Text = string.Empty;
            }
            divMsg.InnerHtml = string.Empty;
            lblTotalMsg.Text = string.Empty;
        }
        catch (Exception ex)
        {
            throw;
        }

    }

    private void PopulateDropDownList()
    {
        objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "ISNULL(ACTIVESTATUS,0)=1", "DEGREENO");
        objCommon.FillDropDownList(ddlAdmBatch, "ACD_ADMBATCH", "top 1 BATCHNO", "BATCHNAME", "BATCHNO <> 0 AND ISNULL(ACTIVESTATUS,0)=1", "BATCHNO DESC");
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        try
        {
            string path = MapPath("~/ExcelData/");
            if (FileUpload1.HasFile)
            {
                if (FileUpload1.FileName.ToString().Contains(".xls"))
                {
                    if (!(Directory.Exists(MapPath("~/PresentationLayer/ExcelData"))))
                        Directory.CreateDirectory(path);

                    string[] array1 = Directory.GetFiles(path);

                    foreach (string str in array1)
                    {
                        if ((path + FileUpload1.FileName.ToString()).Equals(str))
                        {
                            objCommon.DisplayMessage(UpdatePanel1, "File with similar name already exists!", this);
                            return;
                        }
                    }
                    ViewState["FileName"] = FileUpload1.FileName.ToString();
                    //Upload file to server
                    FileUpload1.SaveAs(MapPath("~/ExcelData/" + FileUpload1.FileName));

                    //Import Data
                    if (!CheckFormatAndImport())
                    {
                        objCommon.DisplayMessage(UpdatePanel1, "File is not in correct format!!Please check if the data is saved in sheet1 or the column names do not match!", this.Page);
                        if (File.Exists(MapPath("~/ExcelData/") + ViewState["FileName"].ToString()))
                            File.Delete(MapPath("~/ExcelData/") + ViewState["FileName"].ToString());
                        lblTotalMsg.Text = "Error!!";
                    }
                }
                else
                {
                    objCommon.DisplayMessage(UpdatePanel1, "Only Excel Sheet is Allowed!", this);
                    return;
                }
            }
            else
            {
                objCommon.DisplayMessage(UpdatePanel1, "Select File to Upload!", this);
                return;
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    private bool CheckFormatAndImport()
    {
        string filename = ViewState["FileName"].ToString();

        String sConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;" +
       "Data Source=" + MapPath("~/ExcelData/") + ViewState["FileName"].ToString() + ";" +
        "Extended Properties=Excel 8.0;";


        string Message = string.Empty;
        long ret = 0;
        int count = 0;
        OleDbConnection objCon = new OleDbConnection(sConnectionString);

        try
        {
            objCon.Open();
            OleDbCommand objcmd = new OleDbCommand("Select * from [SHEET1$] ", objCon);

            OleDbDataAdapter objda = new OleDbDataAdapter(objcmd);
            objda.SelectCommand = objcmd;
            DataSet ds = new DataSet();

            objda.Fill(ds);

            int i = ds.Tables[0].Rows.Count;
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                for (i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    ImportDataMaster objIDM = new ImportDataMaster();
                    ImportDataController objIDC = new ImportDataController();


                    objIDM.NO = ds.Tables[0].Rows[i]["No#"].ToString().Trim();
                    objIDM.CANDIDATENAME = ds.Tables[0].Rows[i]["Name"].ToString().Trim();
                    objIDM.Meritno = ds.Tables[0].Rows[i]["Merit No"].ToString().Trim();
                    objIDM.GATESCORE = ds.Tables[0].Rows[i]["Score"].ToString().Trim();
                    objIDM.ApplicationId = ds.Tables[0].Rows[i]["ApplicationID"].ToString().Trim();
                    objIDM.GENDER = ds.Tables[0].Rows[i]["GENDER"].ToString().Trim();
                    objIDM.APPLICANTCATEGORY = ds.Tables[0].Rows[i]["CATEGORY"].ToString().Trim();
                    objIDM.BATCHNO = Convert.ToInt16(ddlAdmBatch.SelectedValue);
                    objIDM.DEGREENO = Convert.ToInt32(ddlDegree.SelectedValue);

                    ret = objIDC.InsertToSqlDB(objIDM);

                    if (ret == 1)
                        count++;
                    else
                    {
                        count--;
                        return false;
                    }
                }
                lblTotalMsg.Text = "Total Records Saved:" + count.ToString();
                if (count < i)
                {
                    objCommon.DisplayMessage(UpdatePanel1, "Please check the format of file & upload again!", this.Page);
                    return false;
                }
                else
                {
                    objCommon.DisplayMessage(UpdatePanel1, "Data Saved Successfully!", this.Page);
                    return true;
                }
            }
            return false;
        }
        catch (Exception ex)
        {
            throw;
        }
        finally
        {
            objCon.Close();
            objCon.Dispose();
        }

    }

    private void StudentTempExcel()
    {
        DataSet ds = studcon.GetTempStudData(Convert.ToInt32(ddlAdmBatch.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue));
        if (ds.Tables[0].Rows.Count > 0)
        {
            DataGrid dg = new DataGrid();
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                string attachment = "attachment; filename=" + "ExcelUploadedDataSheet.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/" + "ms-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);

                dg.DataSource = ds.Tables[0];
                dg.DataBind();
                dg.HeaderStyle.Font.Bold = true;
                dg.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();

            }
            else
            {
                objCommon.DisplayMessage(this, "No Data Found for this Selection", this.Page);
                return;
            }

        }
        else
        {
            objCommon.DisplayMessage(this, "No Data Found for this Selection", this.Page);
            return;
        }
    }

    protected void btnsheet_Click(object sender, EventArgs e)
    {
        try
        {
            StudentTempExcel();
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}