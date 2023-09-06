using System;
using System.Data;
using System.Linq;
using System.Xml.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
//using System.Transactions;
using System.IO;
using System.Net;
using System.Text;
using Excel;
using System.Collections;
using System.Configuration;
using System.Web.Caching;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using IITMS.SQLServer.SQLDAL;
//using CrystalDecisions.CrystalReports.Engine;
using IITMS.NITPRM;


public partial class Itle_ITLE_Import_Data : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    IQuestionbankController objIQBC = new IQuestionbankController();
    IQuestionbank objQuest = new IQuestionbank();
    DataTable dt;

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
                if (Session["ICourseNo"] == null)
                    //Response.Redirect("selectCourse.aspx");
                    Response.Redirect("~/Itle/selectCourse.aspx?pageno=1445");


                //Page Authorization
                CheckPageAuthorization();
                //Set the Page Title
                lblSession.Text = Session["SESSION_NAME"].ToString();
                lblSession.ToolTip = Session["SessionNo"].ToString();
                lblSession.ForeColor = System.Drawing.Color.Green;
                lblCorseName.ForeColor = System.Drawing.Color.Green;
                lblCorseName.Text = Session["ICourseName"].ToString();
                Page.Title = Session["coll_name"].ToString();
               
                if (ViewState["action"] == null)
                    ViewState["action"] = "add";
                TotalQuestions();
                // temprary provision for current session using session variable [by defaullt value set 1 in db]
                objCommon.FillDropDownList(ddlfileformate, "ACD_ITLE_ATTACHMENT_FILE_FORMAT", "FORMAT_ID", "FORMATE", "IS_ACTIVE=1", "FORMAT_ID");

                //GetStatusOnPageLoad();
            }
        }
    }

    #endregion

    #region Private Method

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(),int.Parse(Session["loginid"].ToString()),0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=ITLE_Import_Data.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=ITLE_Import_Data.aspx");
        }
    }
   
    // This Function Defines the Cart whch is used to carry the books in a temprary table until 
    // they are not saved finally to the database.    
    private DataTable GetDataTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn("UNIQUE", typeof(string)));
        dt.Columns.Add(new DataColumn("QUESTIONNO", typeof(int)));
        dt.Columns.Add(new DataColumn("SDSRNO", typeof(int)));
        dt.Columns.Add(new DataColumn("TYPE", typeof(string)));
        dt.Columns.Add(new DataColumn("QUESTIONTEXT", typeof(string)));
        dt.Columns.Add(new DataColumn("QUESTIONIMAGE", typeof(string)));
        return dt;
    }

    //USED FOR DISPLAYING TOTAL NUMBER OR QUESTIONS IN THAT SUBJECT
    private void TotalQuestions()
    {
        try
        {

            objQuest.COURSENO = Convert.ToInt32(Session["ICourseNo"]);
            objQuest.UA_NO = Convert.ToInt32(Session["userno"]);
            int count = Convert.ToInt32(objCommon.LookUp("ACD_IQUESTIONBANK", "COUNT(QUESTIONNO) AS TOTAL_QUESTION", "COURSENO=" + objQuest.COURSENO));
            //objIQBC.TotalQuestions(objQuest);

            lblTotalQues.Text = count.ToString();
        }
        catch (Exception ex)
        {

        }
    }

    #endregion

    #region Page Events

    protected void dpPager_PreRender(object sender, EventArgs e)
    {
        DataTable dt;
        dt = ((DataTable)Session["Carta"]);
        //this.BindListView_Details(dt);
    }

    protected void dpPager2_PreRender(object sender, EventArgs e)
    {
        //this.BindLostBooks();
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlfileformate.SelectedIndex == 0)
            {
                objCommon.DisplayMessage("Please Select file format", this.Page);
            }
            DataSet result = new DataSet();

            if (fuRFIDFILE.HasFile)
            {
                btnSubmitToDatabase.Visible = true;

                dt = new DataTable();
                //DataColumn dc = new DataColumn("EPC", typeof(string));
                //DataColumn dc = new DataColumn("SERIESCODE", typeof(string));
                dt.Columns.Add(new DataColumn("QUESTIONNO", typeof(int)));
                dt.Columns.Add(new DataColumn("SDSRNO", typeof(int)));
                dt.Columns.Add(new DataColumn("TYPE", typeof(string)));
                //dt.Columns.Add(dc);

                string uploadPath = HttpContext.Current.Server.MapPath("~/ITLE/upload_files/QuestionBank/");
                string Fname = hfRF.Value;
                Fname = Server.MapPath(fuRFIDFILE.FileName);

                fuRFIDFILE.PostedFile.SaveAs(uploadPath + fuRFIDFILE.FileName);
                FileStream stream = File.Open(uploadPath + fuRFIDFILE.FileName, FileMode.Open, FileAccess.Read);

                string extension = Path.GetExtension(Fname);
                if (extension == ".txt")
                {

                    StreamReader objReader = new StreamReader(stream);
                    string sLine = "";
                    ArrayList arrText = new ArrayList();



                    result = new DataSet();
                    DataTable dt1 = new DataTable();
                    result.Tables.Add(dt1);

                    result.Tables[0].Columns.Add("QUESTIONTEXT", System.Type.GetType("System.String"));
                    result.Tables[0].Columns.Add("Option1", System.Type.GetType("System.String"));
                    result.Tables[0].Columns.Add("Option2", System.Type.GetType("System.String"));
                    result.Tables[0].Columns.Add("Option3", System.Type.GetType("System.String"));
                    result.Tables[0].Columns.Add("Option4", System.Type.GetType("System.String"));
                    result.Tables[0].Columns.Add("Option5", System.Type.GetType("System.String"));
                    result.Tables[0].Columns.Add("Option6", System.Type.GetType("System.String"));
                    result.Tables[0].Columns.Add("Answer", System.Type.GetType("System.String"));

                    int i = 0;
                    DataRow row = null;
                    while (sLine != null)
                    {
                        sLine = objReader.ReadLine();

                        if (sLine != string.Empty && sLine != null && sLine != " ")
                        {
                            if (i == 0)
                            {
                                row = result.Tables[0].NewRow();
                            }
                            if (sLine.Contains("ANSWER"))
                            {
                                row[7] = sLine.Trim().Replace("ANSWER:", "");
                            }
                            else
                            {
                                row[i] = sLine;
                            }
                            i++;
                        }
                        else
                        {
                            if (sLine != null)
                            {
                                result.Tables[0].Rows.Add(row);
                            }
                            else
                            {
                                break;
                            }
                            //DataRow row = result.Tables[0].NewRow();
                            i = 0;
                            if (sLine == null)
                            {
                                break;
                            }
                        }

                        //if (sLine != null)
                        //    arrText.Add(sLine);
                    }

                    //foreach (string str in arrText)
                    //{
                    //    if (str != string.Empty)
                    //    {
                    //        DataRow row = result.Tables[0].NewRow();
                    //        row[0] = str;
                    //        result.Tables[0].Rows.Add(row);

                    //    }
                    //}
                }
                else if (extension == ".xls")
                {
                    IExcelDataReader excelReader = null;
                    //1. Reading from a binary Excel file ('97-2003 format; *.xls)
                    excelReader = ExcelReaderFactory.CreateBinaryReader(stream);

                    //2. Reading from a OpenXml Excel file (2007 format; *.xlsx)
                    //IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);

                    //3. DataSet - The result of each spreadsheet will be created in the result.Tables
                    result = excelReader.AsDataSet();

                    //4. DataSet - Create column names from first row.
                    excelReader.IsFirstRowAsColumnNames = true;
                    result = excelReader.AsDataSet();
                }
                else
                {
                    objCommon.DisplayMessage("File format not supported", this.Page);

                }



                if (result.Tables[0].Rows.Count > 0)
                {
                    lvBooksInLib.DataSource = result.Tables[0];
                    lvBooksInLib.DataBind();
                    lvBooksInLib.Visible = true;
                    pnlTotalQuestions.Visible = true;
                    Session["Carta"] = result.Tables[0];
                }
                //excelReader.GetInt32(0);
                // }

                //6. Free resources (IExcelDataReader is IDisposable)
                // excelReader.Close();
            }

            else
            {
                btnSubmitToDatabase.Visible = false;
                objCommon.DisplayMessage("Please Upload File First", this.Page);
            }
        }
        catch (Exception ex)
        {
            btnSubmitToDatabase.Visible = false;
            objCommon.DisplayMessage("Uploaded File not in proper format", this.Page);
        }
    }

    //public string ExtractNumber(string original)
    //{
    //    return new string(original.Where(c => Char.IsLetter(c)).ToArray());
    //}

    protected void btnReset_Click(object sender, EventArgs e)
    {
        //int Check = objLibTransaction.CreateStockTables();
        //if (Check == 1)
        //    objCommon.DisplayMessage(updStockVerification,"Tables are ready for Stock Verification", this);
    }

    protected void btnSubmitToDatabase_Click(object sender, EventArgs e)
    {

        if (ddlfileformate.SelectedItem.Text == "Aiken")
        {
            if (txtTopicName.Text == "")
            {
                objCommon.DisplayMessage("Please Enter The Topic Name", this.Page);
                return;
            }
        }

        DataTable dt;
        int cs = 0;
        if (Session["Carta"] != null && ((DataTable)Session["Carta"]) != null)
        {
            if (ddlfileformate.SelectedItem.Text == "Aiken")
            {
               

                dt = ((DataTable)Session["Carta"]);

                int totalColumns = dt.Columns.Count;

                if (totalColumns == 8)
                {

                    objQuest.COURSENO = Convert.ToInt32(Session["ICourseNo"]);
                    objQuest.REMARKS = "Imported";
                    objQuest.AUTHOR = Session["userfullname"].ToString();
                    objQuest.TYPE = 'T';
                    objQuest.OBJECTIVE_DESCRIPTIVE = 'O';
                    objQuest.COLLEGE_CODE = Session["colcode"].ToString();
                    objQuest.UA_NO = Convert.ToInt32(Session["userno"].ToString());

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i][0].ToString() != "" & dt.Rows[i][0].ToString() != null)
                        {
                            objQuest.QUESTIONTEXT = dt.Rows[i][0].ToString();
                            //objQuest.ANS1TEXT = dt.Rows[i][1].ToString().Trim().Replace("A.", "").Replace("A)", "").Replace("1.", "").Replace("1)", "");
                            //objQuest.ANS2TEXT = dt.Rows[i][2].ToString().Trim().Replace("B.", "").Replace("B)", "").Replace("2.", "").Replace("2)", "");
                            //objQuest.ANS3TEXT = dt.Rows[i][3].ToString().Trim().Replace("C.", "").Replace("C)", "").Replace("3.", "").Replace("3)", "");
                            //objQuest.ANS4TEXT = dt.Rows[i][4].ToString().Trim().Replace("D.", "").Replace("D)", "").Replace("4.", "").Replace("4)", "");
                            //objQuest.ANS5TEXT = dt.Rows[i][5].ToString().Trim().Replace("E.", "").Replace("E)", "").Replace("5.", "").Replace("5)", "");
                            //objQuest.ANS6TEXT = dt.Rows[i][6].ToString().Trim().Replace("F.", "").Replace("F)", "").Replace("6.", "").Replace("6)", "");


                            if (dt.Rows[i][1].ToString() != "")
                            {
                                objQuest.ANS1TEXT = dt.Rows[i][1].ToString().Substring(2);
                            }
                            else
                            {
                                objQuest.ANS1TEXT = "";
                            }
                            if (dt.Rows[i][2].ToString() != "")
                            {
                                objQuest.ANS2TEXT = dt.Rows[i][2].ToString().Substring(2);
                            }
                            else
                            {
                                objQuest.ANS2TEXT = "";
                            }

                            if (dt.Rows[i][3].ToString() != "")
                            {
                                objQuest.ANS3TEXT = dt.Rows[i][3].ToString().Substring(2);
                            }
                            else
                            {
                                objQuest.ANS3TEXT = "";
                            }
                            if (dt.Rows[i][4].ToString() != "")
                            {
                                objQuest.ANS4TEXT = dt.Rows[i][4].ToString().Substring(2);
                            }
                            else
                            {
                                objQuest.ANS4TEXT = "";
                            }
                            if (dt.Rows[i][5].ToString() != "")
                            {
                                objQuest.ANS5TEXT = dt.Rows[i][5].ToString().Substring(2);
                            }
                            else
                            {
                                objQuest.ANS5TEXT = "";
                            }
                            if (dt.Rows[i][6].ToString() != "")
                            {
                                objQuest.ANS6TEXT = dt.Rows[i][6].ToString().Substring(2);
                            }
                            else
                            {
                                objQuest.ANS6TEXT ="";
                            }
                            

                            if (dt.Rows[i][7].ToString().Trim() == "A")
                            {
                                objQuest.CORRECTANS = "1";
                            }
                            else if (dt.Rows[i][7].ToString().Trim() == "B")
                            {
                                objQuest.CORRECTANS = "2";
                            }
                            else if (dt.Rows[i][7].ToString().Trim() == "C")
                            {
                                objQuest.CORRECTANS = "3";
                            }
                            else if (dt.Rows[i][7].ToString().Trim() == "D")
                            {
                                objQuest.CORRECTANS = "4";
                            }
                            else if (dt.Rows[i][7].ToString().Trim() == "E")
                            {
                                objQuest.CORRECTANS = "5";
                            }
                            else if (dt.Rows[i][7].ToString().Trim() == "F")
                            {
                                objQuest.CORRECTANS = "6";
                            }
                           else if (dt.Rows[i][7].ToString().Trim() == "1")
                            {
                                objQuest.CORRECTANS = "1";
                            }
                            else if (dt.Rows[i][7].ToString().Trim() == "2")
                            {
                                objQuest.CORRECTANS = "2";
                            }
                            else if (dt.Rows[i][7].ToString().Trim() == "3")
                            {
                                objQuest.CORRECTANS = "3";
                            }
                            else if (dt.Rows[i][7].ToString().Trim() == "4")
                            {
                                objQuest.CORRECTANS = "4";
                            }
                            else if (dt.Rows[i][7].ToString().Trim() == "5")
                            {
                                objQuest.CORRECTANS = "5";
                            }
                            else if (dt.Rows[i][7].ToString().Trim() == "6")
                            {
                                objQuest.CORRECTANS = "6";
                            }

                            objQuest.TOPIC = txtTopicName.Text.Trim().Replace(",", "");//dt.Rows[i][8].ToString().Trim().Replace(",", "");
                            //DateTime CreateDate = Convert.ToDateTime( dt.Rows[i][10]);
                            cs = Convert.ToInt32(objIQBC.AddIQuestionBank(objQuest, Convert.ToInt32(Session["OrgId"])));
                        }
                        //objLibTransaction.InsertStock(ddlYear.SelectedItem.ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString());
                    }



                    //CustomStatus cs = (CustomStatus)objIQBC.AddIQuestionBank(objQuest);
                    if (cs != -99)
                    {
                        //objCommon.DisplayUserMessage(UpdatePanel1, "Record Saved Sucessfully... ", this.Page);
                        objCommon.DisplayMessage("Record Saved Sucessfully", this.Page);
                        ddlfileformate.SelectedIndex = 0;
                        btnSubmitToDatabase.Visible = false;
                        txtTopicName.Text = string.Empty;
                        lvBooksInLib.DataSource = null;
                        lvBooksInLib.DataBind();
                        Session["Carta"] = null;
                    }

                }
                else
                {
                    objCommon.DisplayMessage("Please download Sample file and enter questions according to that format", this.Page);
                    lvBooksInLib.DataSource = null;
                    lvBooksInLib.DataBind();
                    Session["Carta"] = null;
                }
            }
            else if (ddlfileformate.SelectedItem.Text == "Excel")
            {
                dt = ((DataTable)Session["Carta"]);

                int totalColumns = dt.Columns.Count;

                if (totalColumns == 9 && dt.Rows[0][0].ToString() == "QUESTIONTEXT")
                {

                    objQuest.COURSENO = Convert.ToInt32(Session["ICourseNo"]);
                    objQuest.REMARKS = "Imported";
                    objQuest.AUTHOR = Session["userfullname"].ToString();
                    objQuest.TYPE = 'T';
                    objQuest.OBJECTIVE_DESCRIPTIVE = 'O';
                    objQuest.COLLEGE_CODE = Session["colcode"].ToString();
                    objQuest.UA_NO = Convert.ToInt32(Session["userno"].ToString());

                    for (int i = 1; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i][0].ToString() != "" & dt.Rows[i][0].ToString() != null)
                        {
                            objQuest.QUESTIONTEXT = dt.Rows[i][0].ToString();
                            objQuest.ANS1TEXT = dt.Rows[i][1].ToString();
                            objQuest.ANS2TEXT = dt.Rows[i][2].ToString();
                            objQuest.ANS3TEXT = dt.Rows[i][3].ToString();
                            objQuest.ANS4TEXT = dt.Rows[i][4].ToString();
                            objQuest.ANS5TEXT = dt.Rows[i][5].ToString();
                            objQuest.ANS6TEXT = dt.Rows[i][6].ToString();
                            objQuest.CORRECTANS = dt.Rows[i][7].ToString();
                            objQuest.TOPIC = dt.Rows[i][8].ToString().Trim().Replace(",", "");
                            //DateTime CreateDate = Convert.ToDateTime( dt.Rows[i][10]);
                            cs = Convert.ToInt32(objIQBC.AddIQuestionBank(objQuest, Convert.ToInt32(Session["OrgId"])));
                        }
                        //objLibTransaction.InsertStock(ddlYear.SelectedItem.ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString());
                    }



                    //CustomStatus cs = (CustomStatus)objIQBC.AddIQuestionBank(objQuest);
                    if (cs != -99)
                    {
                        //objCommon.DisplayUserMessage(UpdatePanel1, "Record Saved Sucessfully... ", this.Page);
                        objCommon.DisplayMessage("Record Saved Sucessfully", this.Page);
                        lvBooksInLib.DataSource = null;
                        lvBooksInLib.DataBind();
                        Session["Carta"] = null;
                    }

                }
                else
                {
                    objCommon.DisplayMessage("Please download Sample file and enter questions according to that format", this.Page);
                    lvBooksInLib.DataSource = null;
                    lvBooksInLib.DataBind();
                    Session["Carta"] = null;
                }
            }

        }

        else
        {
            objCommon.DisplayMessage("Please Upload File First", this.Page);
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    protected void ddlfileformate_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlfileformate.SelectedItem.Text == "Aiken")
        {
            
            divtopic.Visible = true;
            hlnkAikenDwonload.Visible = true;
            hlnkDownload.Visible = false;
        }
        else
        {
            divtopic.Visible = false;
            hlnkAikenDwonload.Visible = false;
            hlnkDownload.Visible = true;
        }
        if (ddlfileformate.SelectedIndex > 0)
        {
            divdownloadfile.Visible = true;
        }
        else
        {
            divdownloadfile.Visible = false;
        }
    } 
  
    #endregion

    #region Commented Code

    //private void BindListView_Details(DataTable dt)
    //{
    //    try
    //    {
    //        if (dt != null && dt.Rows.Count > 0)
    //        {
    //            lvBooksInLib.DataSource = dt;
    //            lvBooksInLib.DataBind();
    //        }
    //        else
    //        {
    //            lvBooksInLib.DataSource = null;
    //            lvBooksInLib.DataBind();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        //if (Convert.ToBoolean(Session["error"]) == true)
    //        //    objUaimsCommon.ShowError(Page, "LIBRARY_Transactions_Lib_Stock_Verification.BindListView_Details() --> " + ex.Message + " " + ex.StackTrace);
    //        //else
    //        //    objUaimsCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //}

    //For Downloading Sample of Excel File

    #endregion
}

