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
                        lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
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
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_StudentFileUpload.Page_Load-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    private void PopulateDropDownList()
    {
        objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO IN (1,5,3)", "DEGREENO");
        objCommon.FillDropDownList(ddlAdmBatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO <> 0", "BATCHNO DESC");
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
                        if(File.Exists(MapPath("~/ExcelData/")+ ViewState["FileName"].ToString()))
                            File.Delete(MapPath("~/ExcelData/")+ ViewState["FileName"].ToString());
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
              if (Convert.ToBoolean(Session["error"]) == true)
                  objUCommon.ShowError(this.Page, " ACADEMIC_StudentFileUpload->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
              lblTotalMsg.Text = "ERROR!!";
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    private bool CheckFormatAndImport()
    {
        string filename =  ViewState["FileName"].ToString();

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
                    //MTECH
                    if (ddlDegree.SelectedValue == "5")
                    {

                        objIDM.NO = ds.Tables[0].Rows[i]["No#"].ToString().Trim();
                        objIDM.CANDIDATENAME = ds.Tables[0].Rows[i]["Name"].ToString().Trim();
                        objIDM.PRN = ds.Tables[0].Rows[i]["PRN"].ToString().Trim();
                        objIDM.GATEYEAR = ds.Tables[0].Rows[i]["GATE Year"].ToString().Trim();
                        objIDM.GATEREG = ds.Tables[0].Rows[i]["GATE Reg"].ToString().Trim();
                        objIDM.GATESCORE = ds.Tables[0].Rows[i]["GATE Score"].ToString().Trim();
                        objIDM.GATEPAPER = ds.Tables[0].Rows[i]["GATE Paper"].ToString().Trim();
                        objIDM.DOB =  Convert.ToDateTime(ds.Tables[0].Rows[i]["DOB"].ToString());
                        objIDM.GENDER = ds.Tables[0].Rows[i]["Gender"].ToString().Trim();
                        objIDM.MOBILE = ds.Tables[0].Rows[i]["Mobile"].ToString().Trim();
                        objIDM.APPLICANTCATEGORY = ds.Tables[0].Rows[i]["Applicant Category"].ToString().Trim();
                        objIDM.PROGRAMME = ds.Tables[0].Rows[i]["Programme"].ToString().Trim();
                        objIDM.INSTITUTE = ds.Tables[0].Rows[i]["Institute"].ToString().Trim();
                        objIDM.ALLOTTEDCATEGORY = ds.Tables[0].Rows[i]["Allotted Category"].ToString().Trim();
                        objIDM.GROUP = ds.Tables[0].Rows[i]["Group"].ToString().Trim();
                        objIDM.BATCHNO = Convert.ToInt16(ddlAdmBatch.SelectedValue);
                        ret = objIDC.InsertToSqlDB(objIDM,2);

                        if (ret == 1)
                            count++;
                        else
                        {
                            count--;
                            return false;
                        }
                    }
                    else
                    {
                    //BTECH
                        if (ddlDegree.SelectedValue == "3" || ddlDegree.SelectedValue == "1")
                        {

                            objIDM.NO = ds.Tables[0].Rows[i][0].ToString().Trim();
                            objIDM.ROLLNO = ds.Tables[0].Rows[i][1].ToString().Trim();
                            objIDM.AIROVERALL = ds.Tables[0].Rows[i][2].ToString().Trim();
                            objIDM.CANDIDATENAME = ds.Tables[0].Rows[i][3].ToString().Trim();
                            objIDM.BRANCHNAME = ds.Tables[0].Rows[i][4].ToString().Trim();//.Replace(" & "," and ");
                            objIDM.ALLOTTEDCATEGORY = ds.Tables[0].Rows[i][5].ToString().Trim();
                            objIDM.APPLICANTCATEGORY = ds.Tables[0].Rows[i][6].ToString().Trim();
                            //objIDM.PH = ds.Tables[0].Rows[i][7].ToString().Trim();
                            objIDM.HOMESTATE = ds.Tables[0].Rows[i][7].ToString().Trim();
                            objIDM.QUOTA = ds.Tables[0].Rows[i][10].ToString().Trim();
                            //objIDM.ROUNDNO = ds.Tables[0].Rows[i][10].ToString().Trim();
                            objIDM.BATCHNO = Convert.ToInt16(ddlAdmBatch.SelectedValue);

                            //objIDM.NO = ds.Tables[0].Rows[i]["S# No"].ToString().Trim();
                            //objIDM.ROLLNO = ds.Tables[0].Rows[i]["Roll No"].ToString().Trim();
                            //objIDM.AIROVERALL = ds.Tables[0].Rows[i]["AIR Overall"].ToString().Trim();
                            //objIDM.CANDIDATENAME = ds.Tables[0].Rows[i]["Candidate Name"].ToString().Trim();
                            //objIDM.BRANCHNAME = ds.Tables[0].Rows[i]["Branch Name"].ToString().Trim();//.Replace(" & "," and ");
                            //objIDM.ALLOTTEDCATEGORY = ds.Tables[0].Rows[i]["Allotted Category"].ToString().Trim();
                            //objIDM.APPLICANTCATEGORY = ds.Tables[0].Rows[i]["Candidate Category"].ToString().Trim();
                            //objIDM.PH = ds.Tables[0].Rows[i]["PH"].ToString().Trim();
                            //objIDM.HOMESTATE = ds.Tables[0].Rows[i]["Home State"].ToString().Trim();
                            //objIDM.QUOTA = ds.Tables[0].Rows[i]["Quota"].ToString().Trim();
                            //objIDM.ROUNDNO = ds.Tables[0].Rows[i]["Round No#"].ToString().Trim();


                            ret = objIDC.InsertToSqlDB(objIDM,1);
                            if (ret == 1)
                                count++;
                            else
                            {
                                count--;
                                return false;
                            }
                        }
                    }
                }
                lblTotalMsg.Text = "Totla Records Saved:" + count.ToString();
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
              if (Convert.ToBoolean(Session["error"]) == true)
                  objUCommon.ShowError(Page, "Please Check if the data is saved in sheet1 of the file you are uploading or the file is in correct format!! ACADEMIC_StudentFileUpload->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(this.Page, "Server UnAvailable");
              return false;
        }
        finally
        {
            objCon.Close();
            objCon.Dispose();
        }

    }
}