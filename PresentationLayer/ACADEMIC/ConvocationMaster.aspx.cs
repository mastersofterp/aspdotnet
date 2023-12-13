//=================================================================================
// PROJECT NAME  : RFCC                                                          
// MODULE NAME   : ACADEMIC -CONVOCATION QUESTION                                       
// CREATION DATE : 15/11/2023                                               
// CREATED BY    : SHUBHAM BARKE                                                 
// MODIFIED BY   :                                                     
// MODIFIED DESC : 
//=================================================================================

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
using System.Data.SqlClient;
using System.Collections.Specialized;
using System.Collections.Generic;

public partial class ACADEMIC_EXAMINATION_ConvocationMaster : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    QrCodeController objQrC = new QrCodeController();
    StudentFeedBack SFB = new StudentFeedBack();
    StudentFeedBackController objSBC = new StudentFeedBackController();
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
                //Page Authorization
                CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }

                objCommon.FillDropDownList(ddlConvocation, "ACD_CONVOCATION_MASTER WITH (NOLOCK)", "CONV_NO", "CONVOCATION_NAME", "CONV_NO>0", "CONV_NO DESC");
                ddlConvocation.Focus();
                ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
                SetInitialRow();
                Questionlist();
                bool answerstatus = CheckQuesCount();
                if (answerstatus == true)
                {
                    objCommon.DisplayUserMessage(this.Page, "Question Limit is Over!", this.Page);
                    Questionlist();
                    return;
                }

            }
        }
        // divMsg.InnerHtml = string.Empty;
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=MarksEntryNotDone.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=MarksEntryNotDone.aspx");
        }
    }

    //to set initial row in grid
    private void SetInitialRow()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;
        dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
        dt.Columns.Add(new DataColumn("Column1", typeof(string)));
        dt.Columns.Add(new DataColumn("Column2", typeof(string)));
        dr = dt.NewRow();
        dr["RowNumber"] = 1;
        dr["Column1"] = string.Empty;
        dr["Column2"] = string.Empty;
        dt.Rows.Add(dr);

        //Store the DataTable in ViewState
        ViewState["CurrentTable"] = dt;

        gvAnswers.DataSource = dt;
        gvAnswers.DataBind();
    }

    private void Questionlist()
    {
        try
        {
            string SP_Name = "PKG_ACD_STUDENT_GET_ALL_CONVOCATION_FEEDBACK_QUESTION";
            string SP_Parameters = "@P_OUTPUT";
            string Call_Values = "0";
            DataSet ds = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values);

            if (ds != null)
            {
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    lvQuestion.DataSource = null;
                    lvQuestion.DataBind();
                    lvQuestion.Items.Clear();
                    lvQuestion.DataSource = ds;
                    lvQuestion.DataBind();

                    foreach (ListViewDataItem dataitem in lvQuestion.Items)
                    {
                        RadioButtonList rblOptions = dataitem.FindControl("rblOptions") as RadioButtonList;
                        HiddenField hdnOptions = dataitem.FindControl("hdnOptions") as HiddenField;

                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            if (Convert.ToInt32(hdnOptions.Value) == Convert.ToInt32(ds.Tables[0].Rows[i]["QUESTIONID"]))
                            {
                                string ansOptions = ds.Tables[0].Rows[i]["ANS_OPTIONS"].ToString();
                                string ansValue = ds.Tables[0].Rows[i]["ANS_VALUE"].ToString();

                                if (ansOptions.Contains(","))
                                {
                                    string[] opt;
                                    string[] val;

                                    opt = ansOptions.Split(new[] { "," }, StringSplitOptions.None);
                                    val = ansValue.Split(new[] { "," }, StringSplitOptions.None);

                                    int itemindex = 0;
                                    for (int j = 0; j < opt.Length; j++)
                                    {
                                        for (int k = 0; k < val.Length; k++)
                                        {
                                            if (j == k)
                                            {
                                                RadioButtonList lst;
                                                lst = new RadioButtonList();

                                                rblOptions.Items.Add(opt[j]);
                                                rblOptions.SelectedIndex = itemindex;
                                                rblOptions.SelectedItem.Value = val[j];

                                                itemindex++;
                                                break;
                                            }
                                        }
                                    }
                                }
                                rblOptions.SelectedIndex = -1;
                                break;
                            }
                        }
                    }
                }
                else
                {
                    lvQuestion.DataSource = null;
                    lvQuestion.DataBind();
                }
                ds.Dispose();
            }
            else
            {
                lvQuestion.DataSource = null;
                lvQuestion.DataBind();
            }

        }
        catch (Exception ex)
        {

        }
    }

    CustomStatus cs;
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int rowIndex = 0;
        string ansOptions = string.Empty;
        string ansValue = string.Empty;
        bool status = CheckQuesCount();
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            if (dtCurrentTable.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                {

                    if (dtCurrentTable.Rows.Count > 1)
                    {
                        //extract the TextBox values
                        TextBox box1 = (TextBox)gvAnswers.Rows[rowIndex].Cells[1].FindControl("txtAnsOption");
                        TextBox box2 = (TextBox)gvAnswers.Rows[rowIndex].Cells[2].FindControl("txtValue");
                        if (box1.Text.Trim() != string.Empty && box2.Text.Trim() != string.Empty)
                        {
                            //get the values from the TextBoxes
                            ansOptions += box1.Text.Trim() + ",";
                            ansValue += box2.Text.Trim() + ",";
                            rowIndex++;
                        }
                        else
                        {
                            objCommon.DisplayUserMessage(this.Page, "Please Enter Answer Options in Data Row " + i, this.Page);
                            Questionlist();
                            return;
                        }
                    }
                    else
                    {
                        objCommon.DisplayUserMessage(this.Page, "Please Enter More Answer Options in Data Row ", this.Page);
                        Questionlist();
                        return;
                    }


                }
            }

        }
        if (!txtConvocationQuestion.Text.Trim().Equals(string.Empty))
        {
            SFB.QuestionName = txtConvocationQuestion.Text.Trim();
        }
        SFB.CollegeCode = Session["colcode"].ToString();
        SFB.AnsOptions = ansOptions.TrimEnd(',');
        SFB.Value = ansValue.TrimEnd(',');

        if (ChkCmt.Checked == true)
        { SFB.ActiveStatus = 1; }
        else { SFB.ActiveStatus = 0; }

        string[] str1 = SFB.Value.ToString().Split(',');
        string[] str2 = SFB.AnsOptions.ToString().Split(',');
        bool valuestatus = HasDuplicateValues(str1);
        bool answerstatus = HasDuplicateAnswers(str2);
        if (valuestatus == true)
        {
            objCommon.DisplayUserMessage(this.Page, "Please Enter Unique Answer Values", this.Page);
            Questionlist();
            return;
        }
        else if (answerstatus == true)
        {
            objCommon.DisplayUserMessage(this.Page, "Please Enter Unique Options in Answers", this.Page);
            Questionlist();
            return;
        }
        else
        {
            if (ViewState["action"] != null && ViewState["action"].ToString().Equals("edit"))
            {

                //Edit 
                SFB.QuestionId = Convert.ToInt32(ViewState["QuestionId"]);
                cs = (CustomStatus)objSBC.UpdateConvocationQuestion(SFB);
            }
            else
            {
                if (status == true)
                {
                    objCommon.DisplayUserMessage(this.Page, "Question Limit is Over Not Able To Add More Questions!", this.Page);
                    Questionlist();
                    return;
                }
                //save
                cs = (CustomStatus)objSBC.AddConvocationQuestion(SFB);
            }
        }

        if (ViewState["action"] != null && ViewState["action"].ToString().Equals("edit"))
        {
            //Edit 
            if (cs.Equals(CustomStatus.RecordUpdated))
            {
                objCommon.DisplayUserMessage(this.Page, "Question Updated sucessfully", this.Page);
                ClearControl();
                Questionlist();

            }
            else if (cs.Equals(CustomStatus.DuplicateRecord))
            {
                objCommon.DisplayUserMessage(this.Page, "Question Already Added !!!!", this.Page);
                Questionlist();
            }
            else if (cs.Equals(CustomStatus.TransactionFailed))
            {
                objCommon.DisplayUserMessage(this.Page, "Transaction Failed", this.Page);
                //FillQuestion();
            }
        }
        else
        {
            //Add New
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayUserMessage(this.Page, "Feedback Question Saved Successfully", this.Page);
                ClearControl();
                Questionlist();

            }
            else if (cs.Equals(CustomStatus.DuplicateRecord))
            {
                objCommon.DisplayUserMessage(this.Page, "Question Already Added !!!!", this.Page);
                Questionlist();
            }
            else if (cs.Equals(CustomStatus.TransactionFailed))
            {
                objCommon.DisplayUserMessage(this.Page, "Transaction Failed", this.Page);
                //FillQuestion();
            }
        }
    }

    protected void lnkRemove_Click(object sender, ImageClickEventArgs e)
    {
        //LinkButton lb = (LinkButton)sender;
        ImageButton lb = (ImageButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        //int rowID = gvRow.RowIndex;
        int rowID = gvRow.RowIndex + 1;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count > 1)
            {
                //if (gvRow.RowIndex < dt.Rows.Count )
                //if (gvRow.RowIndex < dt.Rows.Count - 1)
                if (gvRow.RowIndex < dt.Rows.Count)
                {
                    //Remove the Selected Row data
                    dt.Rows.Remove(dt.Rows[rowID]);
                }
            }
            //Store the current data in ViewState for future reference
            ViewState["CurrentTable"] = dt;
            //Re bind the GridView for the updated data
            gvAnswers.DataSource = dt;
            gvAnswers.DataBind();
        }

        //Set Previous Data on Postbacks
        SetPreviousData();
    }

    //to set previous details in grid row
    private void SetPreviousData()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 1; i < dt.Rows.Count; i++)
                {
                    TextBox box1 = (TextBox)gvAnswers.Rows[rowIndex].Cells[1].FindControl("txtAnsOption");
                    TextBox box2 = (TextBox)gvAnswers.Rows[rowIndex].Cells[2].FindControl("txtValue");

                    box1.Text = dt.Rows[i]["Column1"].ToString();
                    box2.Text = dt.Rows[i]["Column2"].ToString();

                    rowIndex++;
                }
            }

        }
    }

    //add new row to grid
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        AddNewRowToGrid();
    }


    private void AddNewRowToGrid()
    {
        int rowIndex = 0;

        if (ViewState["CurrentTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];

            DataRow drCurrentRow = null;
            if (dtCurrentTable.Rows.Count > 0 && dtCurrentTable.Rows.Count < 5)
            {
                DataTable dtNewTable = new DataTable();
                dtNewTable.Columns.Add(new DataColumn("RowNumber", typeof(string)));
                dtNewTable.Columns.Add(new DataColumn("Column1", typeof(string)));
                dtNewTable.Columns.Add(new DataColumn("Column2", typeof(string)));
                drCurrentRow = dtNewTable.NewRow();

                drCurrentRow["RowNumber"] = 1;
                drCurrentRow["Column1"] = string.Empty;
                drCurrentRow["Column2"] = string.Empty;

                dtNewTable.Rows.Add(drCurrentRow);

                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                {
                    //extract the TextBox values
                    TextBox box1 = (TextBox)gvAnswers.Rows[rowIndex].Cells[1].FindControl("txtAnsOption");
                    TextBox box2 = (TextBox)gvAnswers.Rows[rowIndex].Cells[2].FindControl("txtValue");

                    if (box1.Text.Trim() != string.Empty && box2.Text.Trim() != string.Empty)
                    {
                        drCurrentRow = dtNewTable.NewRow();

                        drCurrentRow["RowNumber"] = i + 1;
                        drCurrentRow["Column1"] = box1.Text;
                        drCurrentRow["Column2"] = box2.Text;

                        rowIndex++;
                        dtNewTable.Rows.Add(drCurrentRow);
                    }
                    else
                    {
                        objCommon.DisplayUserMessage(this.Page, "Please Enter Answer Options in Sr.No. " + i, this.Page);
                        return;
                    }
                }


                ViewState["CurrentTable"] = dtNewTable;
                gvAnswers.DataSource = dtNewTable;
                gvAnswers.DataBind();

                SetPreviousData();
            }
            else
            {
                objCommon.DisplayUserMessage(this.Page, "Maximum Options Limit Reached", this.Page);
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }


    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            SFB.QuestionId = int.Parse(btnEdit.CommandArgument);
            ViewState["QuestionId"] = int.Parse(btnEdit.CommandArgument);
            ViewState["action"] = "edit";
            btnSubmit.Text = "Update";
            this.ShowDetails();
            divansoption.Visible = true;
            divbtns.Visible = true;
            btnShow.Visible = false;
            btnCancel.Visible = true;
            btnSubmit.Visible = true;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_FeedBack_Question.btnEdit_Click-> " + ex.Message + "" + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }
    }

    //function to show question details 
    private void ShowDetails()
    {
        try
        {
            int SFB = Convert.ToInt32(ViewState["QuestionId"]);
            string SP_Name = "PKG_CONVOCATION_QUESTION_EDIT";
            string SP_Parameters = "@P_QUESTIONID";
            string Call_Values = "" + SFB + "";
            DataSet ds = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values);
            // DataSet ds = objSBC.GetEditFeedBack(SFB);
            if (ds != null)
            {
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {

                    txtConvocationQuestion.Text = ds.Tables[0].Rows[0]["QUESTIONNAME"] == null ? string.Empty : ds.Tables[0].Rows[0]["QUESTIONNAME"].ToString();

                    if (Convert.ToInt32(ds.Tables[0].Rows[0]["ISCOMMAND"]) == 1)
                    {
                        ChkCmt.Checked = true;
                    }
                    else
                    {
                        ChkCmt.Checked = false;
                    }
                    DataTable dtCurrentTable = new DataTable();

                    DataRow drCurrentRow = null;
                    dtCurrentTable.Columns.Add(new DataColumn("RowNumber", typeof(string)));
                    dtCurrentTable.Columns.Add(new DataColumn("Column1", typeof(string)));
                    dtCurrentTable.Columns.Add(new DataColumn("Column2", typeof(string)));

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        drCurrentRow = dtCurrentTable.NewRow();

                        drCurrentRow["RowNumber"] = Convert.ToInt32(ds.Tables[0].Rows[i]["ROWNUMBER"].ToString());
                        drCurrentRow["Column1"] = ds.Tables[0].Rows[i]["ANS_OPTIONS"];
                        drCurrentRow["Column2"] = ds.Tables[0].Rows[i]["ANS_VALUE"];

                        dtCurrentTable.Rows.Add(drCurrentRow);
                    }

                    ViewState["CurrentTable"] = dtCurrentTable;

                    gvAnswers.DataSource = dtCurrentTable;
                    gvAnswers.DataBind();

                }
            }
            if (ds != null) ds.Dispose();

            //Set Previous Data on Postbacks
            BindDataonEdit();
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_FeedBack_Question.ShowDetails_Click-> " + ex.Message + "" + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }
    }

    private void BindDataonEdit()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    TextBox box1 = (TextBox)gvAnswers.Rows[rowIndex].Cells[1].FindControl("txtAnsOption");
                    TextBox box2 = (TextBox)gvAnswers.Rows[rowIndex].Cells[2].FindControl("txtValue");

                    box1.Text = dt.Rows[i]["Column1"].ToString();
                    box2.Text = dt.Rows[i]["Column2"].ToString();

                    rowIndex++;
                }
            }

        }
    }

    //funtion to check duplicate values in string
    private bool HasDuplicateValues(string[] arrayList)
    {
        List<string> vals = new List<string>();
        bool returnValue = false;
        foreach (string s in arrayList)
        {
            if (vals.Contains(s))
            {
                returnValue = true;
                break;
            }
            vals.Add(s);
        }
        return returnValue;
    }

    //funtion to check duplicate values in string
    private bool HasDuplicateAnswers(string[] arrayList)
    {
        List<string> vals = new List<string>();
        bool returnValue = false;
        foreach (string s in arrayList)
        {
            if (vals.Contains(s))
            {
                returnValue = true;
                break;
            }
            vals.Add(s);
        }
        return returnValue;
    }


    //to clear controls
    private void ClearControl()
    {
        ddlConvocation.SelectedIndex = 0;
        txtConvocationQuestion.Text = string.Empty;
        ChkCmt.Checked = false;
        btnSubmit.Text = "Submit";
        ViewState["action"] = "add";
        btnShow.Visible = true;
        divansoption.Visible = false;
        btnSubmit.Visible = false;
        btnCancel.Visible = false;
        SetInitialRow();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearControl();
        Response.Redirect(Request.Url.ToString());
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            divansoption.Visible = true;
            divbtns.Visible = true;
            btnShow.Visible = false;
            btnSubmit.Visible = true;
            btnCancel.Visible = true;
        }
        catch (Exception ex)
        {

        }
    }

    // Hide the Remove Button at the last row of the GridView
    protected void gvAnswers_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            // LinkButton lb = (LinkButton)e.Row.FindControl("lnkRemove");
            ImageButton lb = (ImageButton)e.Row.FindControl("lnkRemove");


            if (lb != null)
            {
                if (dt.Rows.Count > 1)
                {
                    if (e.Row.RowIndex == dt.Rows.Count - 1)
                    {
                        lb.Visible = false;
                    }
                }
                else
                {
                    lb.Visible = false;
                }
            }
        }
    }

    private bool CheckQuesCount()
    {

        bool returnValue = false;
        int count = Convert.ToInt32(objCommon.LookUp("ACD_CONVOCATION_FEEDBACK_QUESTION_DEMO", "COUNT(*)", "QUESTIONID > 0"));

        if (count >= 12)
        {
            returnValue = true;
        }
        else
        {
            return returnValue;
        }

        return returnValue;

    }


}