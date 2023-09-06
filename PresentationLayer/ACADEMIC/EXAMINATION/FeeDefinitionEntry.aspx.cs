using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

public partial class ACADEMIC_EXAMINATION_FeeDefinitionEntry : System.Web.UI.Page
{
    Common objCommon = new Common();
    ExamController Exm = new ExamController();
    UAIMS_Common objUCommon = new UAIMS_Common();
    LateFeeController objLFC = new LateFeeController();
    string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

    #region Page Events
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
            }

            //objCommon.FillDropDownList(ddlSubType, "ACD_SUBJECTTYPE", "SUBID", "SUBNAME", "SUBID>0", "SUBID");
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO>0", "DEGREENO");
          // lvinfo.Visible = true;
            //BindDataTemp();
        } 

        ViewState["IPADDRESS"] = Request.ServerVariables["REMOTE_ADDR"];
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=FeeDefinitionEntry.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=FeeDefinitionEntry.aspx");
        }
    }

    #endregion

    //protected void btnSubmit_Click(object sender, EventArgs e)
    //{
    //    int count = 1; string sem1 = "", sem2 = "", sem3 = "", sem4 = "", sem5 = "", sem6 = "", sem7 = "",sem8 = "", sem9 = "", sem10 = ""; //sem8 = string.Empty;
         
    //    foreach (RepeaterItem item in lvinfo.Items)
    //    {
    //        TextBox txtBacklogFeeAmtSem1 = item.FindControl("txtFeeAmtSem1") as TextBox;
    //        Label lblFAmtSem1 = item.FindControl("lblFAmtSem1") as Label;
    //        if (count == 1)
    //        { 
    //            sem1 = txtBacklogFeeAmtSem1.Text;
    //            if (String.IsNullOrEmpty(sem1))
    //            {
    //                objCommon.DisplayMessage(this.updScheme, "Please Enter Sem 1  Fee Amount !", this.Page);
    //                 return;
    //            }
    //        }
    //        if (count == 2) { sem2 = txtBacklogFeeAmtSem1.Text;
    //        if (String.IsNullOrEmpty(sem2))
    //        {
    //            objCommon.DisplayMessage(this.updScheme, "Please Enter Sem 2  Fee Amount !", this.Page);
    //            return;
    //        }
    //        }
    //        if (count == 3) 
    //        { 
    //            sem3 = txtBacklogFeeAmtSem1.Text;
    //            if (String.IsNullOrEmpty(sem3))
    //            {
    //                objCommon.DisplayMessage(this.updScheme, "Please Enter Sem 3  Fee Amount !", this.Page);
    //                return;
    //            }
    //        }
    //        if (count == 4) 
    //        { 
    //            sem4 = txtBacklogFeeAmtSem1.Text;
    //            if (String.IsNullOrEmpty(sem4))
    //            {
    //                objCommon.DisplayMessage(this.updScheme, "Please Enter Sem 4 backlo Fee Amount !", this.Page);
    //                return;
    //            }
    //        }
    //        if (count == 5) 
    //        { 
    //            sem5 = txtBacklogFeeAmtSem1.Text;
    //            if (String.IsNullOrEmpty(sem5))
    //            {
    //                objCommon.DisplayMessage(this.updScheme, "Please Enter Sem 5  Fee Amount !", this.Page);
    //                return;
    //            }
    //        }
    //        if (count == 6) 
    //        {
    //            sem6 = txtBacklogFeeAmtSem1.Text;
    //            if (String.IsNullOrEmpty(sem6))
    //            {
    //                objCommon.DisplayMessage(this.updScheme, "Please Enter Sem 6  Fee Amount !", this.Page);
    //                return;
    //            }
    //        }
    //        if (count == 7) 
    //        { 
    //            sem7 = txtBacklogFeeAmtSem1.Text;
    //            if (String.IsNullOrEmpty(sem7))
    //            {
    //                objCommon.DisplayMessage(this.updScheme, "Please Enter Sem 7  Fee Amount !", this.Page);
    //                return;
    //            }
    //        }
    //        if (count == 8) 
    //        { 
    //            sem8 = txtBacklogFeeAmtSem1.Text;
    //            if (String.IsNullOrEmpty(sem8))
    //            {
    //                objCommon.DisplayMessage(this.updScheme, "Please Enter Sem 8  Fee Amount !", this.Page);
    //                return;
    //            }
    //        }

    //        if (count == 9)
    //        {
    //            sem8 = txtBacklogFeeAmtSem1.Text;
    //            if (String.IsNullOrEmpty(sem9))
    //            {
    //                objCommon.DisplayMessage(this.updScheme, "Please Enter Sem 9  Fee Amount !", this.Page);
    //                return;
    //            }
    //        }
    //        if (count == 10)
    //        {
    //            sem8 = txtBacklogFeeAmtSem1.Text;
    //            if (String.IsNullOrEmpty(sem10))
    //            {
    //                objCommon.DisplayMessage(this.updScheme, "Please Enter Sem 10  Fee Amount !", this.Page);
    //                return;
    //            }
    //        }
    //        count++;
    //    }

        
    //    int result = objLFC.Insert_Reg_Backlog_Fees_Details(Convert.ToDouble(txtRegFeeAmt.Text), Convert.ToDouble(sem1), Convert.ToDouble(sem2), Convert.ToDouble(sem3), Convert.ToDouble(sem4), Convert.ToDouble(sem5), Convert.ToDouble(sem6), Convert.ToDouble(sem7), Convert.ToDouble(sem8),ViewState["IPADDRESS"].ToString(),Convert.ToInt32(Session["userno"].ToString()));

    //    if (result > 0)
    //    {
    //        objCommon.DisplayMessage(this.updScheme, "Record Saved Successfully !", this.Page);
    //        //txtRegFeeAmt.Text = string.Empty;
    //        BindDataTemp();

    //        foreach (RepeaterItem item in lvinfo.Items)
    //        {
    //            TextBox txtBacklogFeeAmtSem1 = item.FindControl("txtBacklogFeeAmtSem1") as TextBox;
    //           // txtBacklogFeeAmtSem1.Text = string.Empty;
    //        }
    //    }

    //        //if (txtBacklogSem1.Text == string.Empty || txtBacklogSem2.Text == string.Empty || txtBacklogSem3.Text == string.Empty || txtBacklogSem4.Text == string.Empty || txtBacklogSem5.Text == string.Empty || txtBacklogSem6.Text == string.Empty || txtBacklogSem7.Text == string.Empty || txtBacklogSem8.Text == string.Empty)
    //        //{
    //        //    objCommon.DisplayMessage(this.updScheme, "Please Enter all Sem backlog Fee Amount !", this.Page);
    //        //    return;
    //        //}
    //        //sem1 = txtBacklogSem1.Text;
    //        //sem2 = txtBacklogSem2.Text;
    //        //sem3 = txtBacklogSem3.Text;
    //        //sem4 = txtBacklogSem4.Text;
    //        //sem5 = txtBacklogSem5.Text;
    //        //sem6 = txtBacklogSem6.Text;
    //        //sem7 = txtBacklogSem7.Text;
    //        //sem8 = txtBacklogSem8.Text;

    //        //int result = objLFC.Insert_Reg_Backlog_Fees_Details(Convert.ToDouble(txtRegFeeAmt.Text), Convert.ToDouble(sem1), Convert.ToDouble(sem2), Convert.ToDouble(sem3), Convert.ToDouble(sem4), Convert.ToDouble(sem5), Convert.ToDouble(sem6), Convert.ToDouble(sem7), Convert.ToDouble(sem8));
    //        //if (result > 0)
    //        //{
    //        //    objCommon.DisplayMessage(this.updScheme, "Record Saved Successfully !", this.Page);
    //        //    txtRegFeeAmt.Text = string.Empty;

    //        //    txtBacklogSem1.Text = string.Empty;
    //        //    txtBacklogSem2.Text = string.Empty;
    //        //    txtBacklogSem3.Text = string.Empty;
    //        //    txtBacklogSem4.Text = string.Empty;
    //        //    txtBacklogSem5.Text = string.Empty;
    //        //    txtBacklogSem6.Text = string.Empty;
    //        //    txtBacklogSem7.Text = string.Empty;
    //        //    txtBacklogSem8.Text = string.Empty;
    //        //}
        
      
    //}

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString();
            
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updFee, this.updFee.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_REPORTS_RollListForScrutiny.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        int count = Convert.ToInt32(objCommon.LookUp("ACD_EXAM_FEE_CONFIG", "count(1)", string.Empty));

        if (count != 0)
            ShowReport("FeeDefList", "rptFeeDefEntry.rpt");
        else
        {
            objCommon.DisplayMessage(this.updFee, "No Record Found !", this.Page);
            return;
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    void BindDataTemp()
    {

        //if (rdoRegular.Checked == true) { }
        //else if (rdoBacklog.Checked == true) { }
        //else { }
        DataSet dsFees = objCommon.FillDropDown("ACD_EXAM_FEE_CONFIG", "ISNULL(REG_FAMT,0)REG_FAMT", "ISNULL(BACKLOG_FAMTSEM1,0)BACKLOG_FAMTSEM1,ISNULL(BACKLOG_FAMTSEM2,0)BACKLOG_FAMTSEM2,ISNULL(BACKLOG_FAMTSEM3,0)BACKLOG_FAMTSEM3,ISNULL(BACKLOG_FAMTSEM4,0)BACKLOG_FAMTSEM4,ISNULL(BACKLOG_FAMTSEM5,0)BACKLOG_FAMTSEM5,ISNULL(BACKLOG_FAMTSEM6,0)BACKLOG_FAMTSEM6,ISNULL(BACKLOG_FAMTSEM7,0)BACKLOG_FAMTSEM7,ISNULL(BACKLOG_FAMTSEM8,0)BACKLOG_FAMTSEM8", "FID>0", "");
        // Create a new DataTable.    
        DataTable custTable = new DataTable("Customers");
        DataColumn dtColumn;
        DataRow myDataRow;
        txtRegFeeAmt.Text = dsFees.Tables[0].Rows.Count > 0 ? dsFees.Tables[0].Rows[0]["REG_FAMT"].ToString() : "0.00"; 
        // Create id column  
        dtColumn = new DataColumn();
         
        dtColumn.ColumnName = "Semester";
        // Add column to the DataColumnCollection.  
        custTable.Columns.Add(dtColumn);

        // Create id column  
        dtColumn = new DataColumn();
        
        dtColumn.ColumnName = "Fees";
        // Add column to the DataColumnCollection.  
        custTable.Columns.Add(dtColumn);

        myDataRow = custTable.NewRow();
        myDataRow["Semester"] = 1;
        myDataRow["Fees"] = dsFees.Tables[0].Rows.Count > 0 ? dsFees.Tables[0].Rows[0]["BACKLOG_FAMTSEM1"] : "0.00";         
        custTable.Rows.Add(myDataRow);

        myDataRow = custTable.NewRow();
        myDataRow["Semester"] = 2;
        myDataRow["Fees"] = dsFees.Tables[0].Rows.Count > 0 ? dsFees.Tables[0].Rows[0]["BACKLOG_FAMTSEM2"] : "0.00"; 
        custTable.Rows.Add(myDataRow);

        myDataRow = custTable.NewRow();
        myDataRow["Semester"] = 3;
        myDataRow["Fees"] = dsFees.Tables[0].Rows.Count > 0 ? dsFees.Tables[0].Rows[0]["BACKLOG_FAMTSEM3"] : "0.00";  
        custTable.Rows.Add(myDataRow);

        myDataRow = custTable.NewRow();
        myDataRow["Semester"] = 4;
        myDataRow["Fees"] = dsFees.Tables[0].Rows.Count > 0 ? dsFees.Tables[0].Rows[0]["BACKLOG_FAMTSEM4"] : "0.00";  
        custTable.Rows.Add(myDataRow);

        myDataRow = custTable.NewRow();
        myDataRow["Semester"] = 5;
        myDataRow["Fees"] = dsFees.Tables[0].Rows.Count > 0 ? dsFees.Tables[0].Rows[0]["BACKLOG_FAMTSEM5"] : "0.00";  
        custTable.Rows.Add(myDataRow);

        myDataRow = custTable.NewRow();
        myDataRow["Semester"] = 6;
        myDataRow["Fees"] = dsFees.Tables[0].Rows.Count > 0 ? dsFees.Tables[0].Rows[0]["BACKLOG_FAMTSEM6"] : "0.00";  
        custTable.Rows.Add(myDataRow);

        myDataRow = custTable.NewRow();
        myDataRow["Semester"] = 7;
        myDataRow["Fees"] = dsFees.Tables[0].Rows.Count > 0 ? dsFees.Tables[0].Rows[0]["BACKLOG_FAMTSEM7"] : "0.00";  
        custTable.Rows.Add(myDataRow);

        myDataRow = custTable.NewRow();
        myDataRow["Semester"] = 8;
        myDataRow["Fees"] = dsFees.Tables[0].Rows.Count > 0 ? dsFees.Tables[0].Rows[0]["BACKLOG_FAMTSEM8"] : "0.00";  
        custTable.Rows.Add(myDataRow);


        myDataRow = custTable.NewRow();
        myDataRow["Semester"] = 9;
       myDataRow["Fees"] = dsFees.Tables[0].Rows.Count > 0 ? dsFees.Tables[0].Rows[0]["BACKLOG_FAMTSEM8"] : "0.00";
        custTable.Rows.Add(myDataRow);



        myDataRow = custTable.NewRow();
        myDataRow["Semester"] = 10;
        myDataRow["Fees"] = dsFees.Tables[0].Rows.Count > 0 ? dsFees.Tables[0].Rows[0]["BACKLOG_FAMTSEM8"] : "0.00";
        custTable.Rows.Add(myDataRow);

       // lvinfo.DataSource = custTable;
        //lvinfo.DataBind();
    }



    protected void btnShow_Click(object sender, EventArgs e)
    {
        if (rdlist.SelectedValue == string.Empty)
        {

            objCommon.DisplayMessage(this.updFee,"Please Select At least One Exam Fee Type", this.Page);
            return;
        
        }

        BindListView();

    }

    private void BindListView()
    {
        try
        {

             btnSubmit.Visible = true;
             int rdl = Convert.ToInt32(rdlist.SelectedValue);
             int degreeno = Convert.ToInt32(ddlDegree.SelectedValue);
             bool ActiveStatus = chkCourse.Checked ? true : false;
             if (ActiveStatus == true)
             {
                 DataSet ds = Exm.GetFeeStructureCourse(rdl, degreeno);
                 lvFee.DataSource = ds;
                 lvFee.DataBind();
                 lvFee.Visible = true;
                 lvfee2.Visible = false;
             }
             else
             {
                 DataSet ds = Exm.GetFeeStructureSemester(rdl, degreeno);
                 lvfee2.DataSource = ds;
                 lvfee2.DataBind();
                 lvFee.Visible = false;
                 lvfee2.Visible=true;
             
             }



            //GradeEntryController objGEC = new GradeEntryController();
            //DataSet ds = objGEC.GetAllGradeEntry(Convert.ToInt32(ddlGradeType.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(ddlSubType.SelectedValue), Convert.ToInt32(ddlcollege.SelectedValue));
            //lvGrade.DataSource = ds;
            //lvGrade.DataBind();
            //this.LoadGrade();
            //lvGrade.Visible = true;



        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_FeeDefinationEntry.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }




    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        try
        {

            if (rdlist.SelectedValue == String.Empty)
            {

                //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>alert(\"Please select your option for radio button list.\");</script>", false);
                objCommon.DisplayMessage(this.updFee, "Please select at least one Exam Fee Type", this.Page);
                return;
            }

        

            int rdl = Convert.ToInt32(rdlist.SelectedValue);
            int degreeno = Convert.ToInt32(ddlDegree.SelectedValue);
            bool ActiveStatus = chkCourse.Checked ? true : false;


            if (ActiveStatus == true)
            {
                foreach (ListViewDataItem item in lvFee.Items)
                {

                    Label Sub = item.FindControl("lblSubType") as Label;
                    TextBox Fee = item.FindControl("txtFee") as TextBox;
                    String SubjectName = Sub.Text.Trim();
                    int SUBID = Convert.ToInt32(Sub.ToolTip);
                    String FeeAmt = Fee.Text.Trim();

                    if (decimal.Parse(Fee.Text) > 0)
                    {


                        CustomStatus cs = (CustomStatus)Exm.FeeStructureCourse(rdl, degreeno, SUBID, SubjectName, FeeAmt);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {

                            objCommon.DisplayMessage(this.updFee, "Record Saved Successfully!", this.Page);
                            BindListView();
                            lvFee.Visible = true;
                            lvfee2.Visible = false;

                        }
                        else
                        {
                            objCommon.DisplayMessage(this.updFee, "Record  Successfully Saved!", this.Page);
                            BindListView();
                            lvFee.Visible = true;
                            lvfee2.Visible = false;
                        }


                    }

                }


            }
            else
            {


                foreach (ListViewDataItem item in lvfee2.Items)
                {

                    Label Sub = item.FindControl("lblSubType") as Label;
                    TextBox Fee = item.FindControl("txtFee") as TextBox;
                    //String SubjectName = Sub.Text.Trim();
                   // int SUBID = Convert.ToInt32(Sub.ToolTip);
                    String FeeAmt = Fee.Text.Trim();
                    Label Sem = item.FindControl("lblSem") as Label;
                    int Semesterno =Convert.ToInt32( Sem.Text);


                    if (decimal.Parse(Fee.Text) > 0)
                    {


                        CustomStatus cs = (CustomStatus)Exm.FeeStructureSemester(rdl, degreeno, Semesterno, FeeAmt);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {

                            objCommon.DisplayMessage(this.updFee, "Record Saved Successfully!", this.Page);
                            BindListView();
                           lvFee.Visible = false;
                            lvfee2.Visible = true;

                        }
                        else
                        {
                            objCommon.DisplayMessage(this.updFee, "Record  Successfully Saved!", this.Page);
                            BindListView();
                            lvFee.Visible = false;
                            lvfee2.Visible = true;
                        }


                    }

                }






            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Masters_Grade.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }






    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvFee.Visible = false;
        lvfee2.Visible = false;
        btnSubmit.Visible = false;

    }
    protected void rdlist_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvFee.Visible = false;
        lvfee2.Visible = false;
        btnSubmit.Visible = false;
    }
    protected void chkCourse_CheckedChanged(object sender, EventArgs e)
    {
        lvFee.Visible = false;
        lvfee2.Visible = false;
        btnSubmit.Visible = false;
    }
   
}




        