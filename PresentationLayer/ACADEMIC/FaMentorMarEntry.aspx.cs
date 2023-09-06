using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities;
//using IITMS.UAIMS.BusinessLayer.BusinessLogicLayer;
using IITMS.UAIMS.BusinessLayer;
using BusinessLogicLayer.BusinessLogic.Academic;
using System.Data;

public partial class ACADEMIC_FaMentorMarEntry : System.Web.UI.Page
{

    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ActivityController objACT = new ActivityController();
    static int id;
    static int actno;
    DataSet ds1 = null;

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
                if (Session["usertype"].ToString() == "3")
                {

                    // this.PopulateDropDownList();
                    this.CheckPageAuthorization();


                }
                else
                {
                    Response.Redirect("~/notauthorized.aspx?page=FaMentorMarEntry.aspx");
                }
                Page.Title = Session["coll_name"].ToString();
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                }
               
            }

           objCommon.FillDropDownList(ddlAcdYear, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO > 0 ", "BATCHNO DESC");
            //  objCommon.FillDropDownList(ddlInstitute, "acd_college_master", "COLLEGE_ID", "(COLLEGE_NAME + ' ' + '-' + ' ' + LOCATION) collegeName", "COLLEGE_ID > 0 AND COLLEGE_ID IN (" + Session["college_nos"] + ")", "COLLEGE_ID");
            objCommon.FillDropDownList(ddlInstitute, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID > 0 AND COLLEGE_ID IN (" + Session["college_nos"] + ")", "COLLEGE_ID");
          
            ddlAcdYear.SelectedIndex = 1;

            btnReport.Visible = true;

            divMsg.InnerHtml = string.Empty;
            
          
            // BindListView();
            //ViewState["action"] = "add";
        }

    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=FaMentorMarEntry.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=FaMentorMarEntry.aspx");
        }
    }

    protected void ddlAcdYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlInstitute.Items.Clear();
        ddlInstitute.Items.Add(new ListItem("Please Select", "0"));


        ddlDegree.Items.Clear();
        ddlDegree.Items.Add(new ListItem("Please Select", "0"));


        ddlBranch.Items.Clear();
        ddlBranch.Items.Add(new ListItem("Please Select", "0"));


        ddlactvity.Items.Clear();
        ddlactvity.Items.Add(new ListItem("Please Select", "0"));

        ddlInstitute.SelectedIndex = 0;
        ddlactvity.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;

        //lvStudent.DataSource = null;
       // lvStudent.DataBind();

        lvMarActivity.DataSource = null;
        lvMarActivity.DataBind();
        pnlMarEntry.Visible = false;
        pnlActivity.Visible = false;
        pnlActivityNew.Visible = false;



        if (ddlAcdYear.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlInstitute, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID > 0 AND COLLEGE_ID IN (" + Session["college_nos"] + ")", "COLLEGE_ID");
            ddlAcdYear.Focus();
        }

    }



    protected void ddlInstitute_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlDegree.Items.Clear();
        ddlDegree.Items.Add(new ListItem("Please Select", "0"));


        ddlBranch.Items.Clear();
        ddlBranch.Items.Add(new ListItem("Please Select", "0"));


        ddlactvity.Items.Clear();
        ddlactvity.Items.Add(new ListItem("Please Select", "0"));


        pnlActivity.Visible = false;
        pnlActivityNew.Visible = false;
        ddlBranch.SelectedIndex = 0;
        ddlactvity.SelectedIndex = 0;
      //  lvStudent.DataSource = null;
      //  lvStudent.DataBind();
        ddlDegree.SelectedIndex = 0;
        lvMarActivity.DataSource = null;
        lvMarActivity.DataBind();
        pnlMarEntry.Visible = false;

        if (ddlInstitute.SelectedIndex > 0)
        {
            // objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0", "A.LONGNAME");

            objCommon.FillDropDownList(ddlDegree, "ACD_COLLEGE_DEGREE_BRANCH CD INNER JOIN ACD_STUDENT ST ON ST.BRANCHNO = CD.BRANCHNO AND ST.COLLEGE_ID = CD.COLLEGE_ID AND ST.DEGREENO = CD.DEGREENO INNER JOIN  ACD_DEGREE AD  ON AD.DEGREENO  = CD.DEGREENO INNER JOIN ACD_BRANCH BR ON BR.BRANCHNO  = CD.BRANCHNO", "DISTINCT AD.DEGREENO", "DEGREENAME", "FAC_ADVISOR=" + Session["userno"] + "AND CD.COLLEGE_ID=" + Session["college_nos"], "DEGREENAME asc");
            ddlInstitute.Focus();


        }

    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {


        ddlactvity.Items.Clear();
        ddlactvity.Items.Add(new ListItem("Please Select", "0"));


        ddlactvity.SelectedIndex = 0;

       // lvStudent.DataSource = null;
       // lvStudent.DataBind();

        lvMarActivity.DataSource = null;
        lvMarActivity.DataBind();
        pnlMarEntry.Visible = false;
        pnlActivity.Visible = false;
        pnlActivityNew.Visible = false;

        if (ddlBranch.SelectedIndex > 0)
        {

          //  objCommon.FillDropDownList(ddlactvity, "ACD_MAR_ACTIVITY_MASTER", "ACTIVITYNO", "ACTIVITY_NAME", "ACADEMIC_YEAR=" + Convert.ToInt32(ddlAcdYear.SelectedValue), "ACTIVITYNO ASC");
            ddlBranch.Focus();
        }
    }
    protected void ddlactvity_SelectedIndexChanged(object sender, EventArgs e)
    {
       // lvStudent.DataSource = null;
       // lvStudent.DataBind();
        pnlActivity.Visible = false;
        pnlActivityNew.Visible = false;
        lvMarActivity.DataSource = null;
        lvMarActivity.DataBind();
        pnlMarEntry.Visible = false;
        ddlactvity.Focus();
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
      //  BindListView();
       // lvActivity

        divMsg.InnerHtml = string.Empty;
        BindActivityListView();
        DataSet ds = objACT.GetStudentDetailsNew(Convert.ToInt32(ddlAcdYear.SelectedValue), Convert.ToInt32(ddlInstitute.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(Session["userno"]));
        lvStudentNew.DataSource = ds;
        lvStudentNew.DataBind();
        pnlActivityNew.Visible = true;
        lvStudentNew.Visible = true;
        btnReport.Visible = true;
    }


    private void BindActivityListView()
    {
        try {


            int version=0;

            version=Convert.ToInt32(objCommon.LookUp("ACD_MAR_ACTIVITY_MASTER","VERSON","ACTIVITYNO > 0"));


            DataSet ds = objACT.GetAllActivity(version);

            if (ds.Tables[0].Rows.Count > 0)
            {
                lvActivity.DataSource = ds;
                lvActivity.DataBind();
                pnlActivity.Visible = true;
                pnlActivityNew.Visible = true;
              //  pnlStudent.Visible = false;
                pnlMarEntry.Visible = false;

            }
            else
            {

                lvActivity.DataSource = null;
                lvActivity.DataBind();
                objCommon.DisplayMessage(updActivity, "Activity is not found", this.Page);

            }


        
        
        }


        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Mar_Activity_Master.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

















    private void BindListView(int actonos)
    {
        try
        {

            DataSet ds = objACT.GetStudentDetails(Convert.ToInt32(ddlAcdYear.SelectedValue), Convert.ToInt32(ddlInstitute.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(Session["userno"]), actonos);

            if (ds.Tables[0].Rows.Count > 0)
            {
                pnlMarEntry.Visible = false;
               // pnlStudent.Visible = true;
               // lvStudent.DataSource = ds;
               // lvStudent.DataBind();
               // btnReport.Visible = true;
               
            }
            else
            {
                //lvStudent.DataSource = null;
                //lvStudent.DataBind();
                objCommon.DisplayMessage(updActivity, "Student not found", this.Page);

               // lvStudent.DataSource = null;
               // lvStudent.DataBind();

                lvMarActivity.DataSource = null;
                lvMarActivity.DataBind();
                pnlMarEntry.Visible = false;

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Mar_Activity_Master.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }




    private void BindListView2(int actno)
    {

        //

        //ListViewDataItem lvActivity = (ListViewDataItem)lvActivity;
        //lvActivity.FindControl("lblActno");
         


        //Label lblActno = dataItem.FindControl("lblActno") as Label;
        //int act = Convert.ToInt32(lblActno.ToolTip);
        //ViewState["actno"] = act;


        //ListView lv = dataItem.FindControl("lvStudent") as ListView;
        //

        
        
        


        try
        {
            DataSet ds = objACT.GetAllSubActivityForMarEntry(actno);
            if (ds.Tables[0].Rows.Count > 0)
            {
               // pnlStudent.Visible = false;
                pnlMarEntry.Visible = true;
                
                lvMarActivity.DataSource = ds;
                lvMarActivity.DataBind();
                btnReport.Visible = false;
                pnlActivity.Visible = false;
                pnlActivityNew.Visible = false;

                DataSet getSubActivity = objACT.GetStudentDataSubActivityWise(actno, Convert.ToInt32(ViewState["IDNO"]));

                if (getSubActivity.Tables[0].Rows.Count > 0)
                {
                    int rn = 0;
                    foreach (ListViewItem itemRow in lvMarActivity.Items)
                    {
                        HiddenField hdnfldSubActivityNo = (HiddenField)itemRow.FindControl("hdnfldSubActivityNo");
                        actno = Convert.ToInt32(hdnfldSubActivityNo.Value);

                        DropDownList ddlSubPoints = (DropDownList)itemRow.FindControl("ddlSubPoints");
                        TextBox txtAcqPoint = (TextBox)itemRow.FindControl("txtAcqPoint");
                        HiddenField hdfPoints = (HiddenField)itemRow.FindControl("hdnPoints");
                        objCommon.FillDropDownList(ddlSubPoints, "ACD_MAR_PARTICIPANT", "MARPARTICIPANTNO", "MARPARTICIPANTNO", "", "");


                        if (hdnfldSubActivityNo.Value == getSubActivity.Tables[0].Rows[rn]["SUBACTIVITYNO"].ToString())
                        {
                            ddlSubPoints.SelectedValue = getSubActivity.Tables[0].Rows[rn]["APPERED_COUNT"].ToString();
                            txtAcqPoint.Text = getSubActivity.Tables[0].Rows[rn]["POINT_AQUIRED"].ToString();
                        }
                        rn++;
                    }
                }
                else
                {
                    foreach (ListViewItem itemRow in lvMarActivity.Items)
                    {
                        DropDownList ddlSubPoints = (DropDownList)itemRow.FindControl("ddlSubPoints");
                        objCommon.FillDropDownList(ddlSubPoints, "ACD_MAR_PARTICIPANT", "MARPARTICIPANTNO", "MARPARTICIPANTNO", "", "");
                        HiddenField hidActivityNo = (HiddenField)itemRow.FindControl("hdnfldActivityNo");
                        hidActivityNo.Value = actno.ToString();
                    }
                }

                //   btnSubmit.Enabled = false;
            }
            else
            {
                //pnlMarEntry.Visible = false;
                //lvMarActivity.DataSource = null;
                //lvMarActivity.DataBind();
                objCommon.DisplayMessage(updActivity, "Sub Activity is not found", this.Page);
                pnlActivity.Visible = true;
                pnlActivityNew.Visible = true;



            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sub_Activity_Creation.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnMar_Click(object sender, EventArgs e)
    {
        int ActivityNo = 0;
        //Button btn = new Button();

        //ddlSubPoints

        //id=int.Parse(btn.CommandArgument);

       // ListViewDataItem dataitem=(ListViewDat
       // string ActivityName = string.Empty;
        


        Button btn = sender as Button;
        int iactno =  Convert.ToInt32(btn.ToolTip);
        lblactivity2.Text = objCommon.LookUp("ACD_MAR_ACTIVITY_MASTER", "ACTIVITY_NAME", "ACTIVITYNO=" + iactno);
        int id = int.Parse(btn.CommandArgument);
        ViewState["IDNO"] = id;
        ViewState["iactno"] = iactno;
        DataSet ds = objCommon.FillDropDown("ACD_STUDENT S INNER JOIN ACD_BRANCH B ON (B.BRANCHNO=S.BRANCHNO) INNER JOIN ACD_DEGREE D ON (S.DEGREENO=D.DEGREENO)", "S.IDNO,B.LONGNAME,S.STUDNAME,D.DEGREENAME", "S.REGNO", "S.IDNO= " + id, "");
        if (ds.Tables[0].Rows.Count > 0)
        {
            lblRegno2.Text = ds.Tables[0].Rows[0]["REGNO"].ToString();
            lblbranch2.Text = ddlBranch.SelectedItem.Text;
            lblCollege2.Text = ddlInstitute.SelectedItem.Text;
          //  lblactivity2.Text = ActivityName.ToString();
            lblName2.Text = ds.Tables[0].Rows[0]["STUDNAME"].ToString();
            lblDegree2.Text = ddlDegree.SelectedItem.Text;
            ViewState["BATCHNO"] = Convert.ToInt32(ddlAcdYear.SelectedValue);
            // ViewState["SUB_ACTIVITY_NO"] = ds.Tables[0].Rows[0]["SUB_ACTIVITY_NO"].ToString();
            // ViewState["CURRENT_ACQ_POINTS"] = ds.Tables[0].Rows[0]["CURRENT_ACQ_POINTS"].ToString();
            // ViewState["CURRENT_PER_POINTS"] = ds.Tables[0].Rows[0]["CURRENT_PER_POINTS"].ToString();
            pnlActivity.Visible = false;
            pnlActivityNew.Visible = false;
        }

        divMsg.InnerHtml = string.Empty;
        BindListView2(iactno);
    }

    //private void ShowDetail(int actnos)
    //{
    //    SqlDataReader dr = objACT.GetSubActivityDetails(actno);

    //    if (dr != null)
    //    {
    //        if (dr.Read())
    //        {
    //            // ViewState["actno"] = feedbackNo.ToString();


    //           // ddlActName.SelectedValue = dr["ACTIVITYNO"] == null ? string.Empty : dr["ACTIVITYNO"].ToString();
    //          //  txtSrNo.Text = dr["SR_NO"] == null ? string.Empty : dr["SR_NO"].ToString();
    //           // txtActName.Text = dr["SUB_ACTIVITY_NAME"] == null ? string.Empty : dr["SUB_ACTIVITY_NAME"].ToString();


    //        }
    //    }
    //    if (dr != null) dr.Close();
    //}

    protected void btnSubmit_Click(object sender, EventArgs e)
    {


       // objCommon.DisplayMessage(this.updActivity, divMsg.InnerHtml, this.Page);
        divMsg.InnerHtml = string.Empty;
        try
        {

            DataTable table = new DataTable();

            table.Columns.Add("ACTIVITYNO", typeof(Int32));
            table.Columns.Add("SUBACTIVITYNO", typeof(Int32));
            table.Columns.Add("IDNO", typeof(Int32));
            table.Columns.Add("APPERED_COUNT", typeof(Int32));
            table.Columns.Add("POINT_AQUIRED", typeof(Int32));
            table.Columns.Add("UA_NO", typeof(Int32));
            if (ViewState["iactno"] != null)
            {
                actno = Convert.ToInt32(ViewState["iactno"]);
            }
            
            foreach (ListViewItem itemRow in lvMarActivity.Items)
            {
                HiddenField hdnfldSubActivityNo = (HiddenField)itemRow.FindControl("hdnfldSubActivityNo");
                DropDownList ddlSubPoints = (DropDownList)itemRow.FindControl("ddlSubPoints");
                TextBox txtAcqPoint = (TextBox)itemRow.FindControl("txtAcqPoint");
                HiddenField hdfPoints = (HiddenField)itemRow.FindControl("hdnPoints");
              

                //if (ddlSubPoints.SelectedIndex > 0)
                //{
                    //objCommon.DisplayMessage(updActivity, "Please Select No. Of Participated For All Sub Activity", this.Page);
                    //return;
               

                table.Rows.Add(actno, hdnfldSubActivityNo.Value, Convert.ToInt32(ViewState["IDNO"]), Convert.ToInt32(ddlSubPoints.SelectedValue), Convert.ToInt32(txtAcqPoint.Text), Convert.ToInt32(Session["userno"]));
                //}


            }
            CustomStatus cs = (CustomStatus)objACT.AddMarActivityPoints(Convert.ToString(lblRegno2.Text), Convert.ToInt32(ViewState["IDNO"]),
            Convert.ToString(lblName2.Text), Convert.ToInt32(hidSumAquirPoint.Value),
            Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlInstitute.SelectedValue), Convert.ToInt32(Session["userno"]),
            actno, Convert.ToInt32(ddlAcdYear.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), table);
            //Convert.ToInt32(ViewState["CURRENT_PER_POINTS"] ,Convert.ToInt32(ViewState["SUB_ACTIVITY_NO"])
            if (cs.Equals(CustomStatus.RecordUpdated))
            {
                objCommon.DisplayMessage(updActivity, "MAR Entry Update Successfully", this.Page);
                BindActivityListView();
              //  pnlMarEntry.Visible = false;
                //pnlActivity.Visible = true;
               // ListView lv =  (ListView) FindControl("lvStudent");
               // lv.Style.Add("display", "Block");
                //pnlMarEntry.Visible = false;

                foreach (ListViewItem item in lvActivity.Items)
                {
                    int actnos = Convert.ToInt32(ViewState["iactno"]);
                    Label lbl = item.FindControl("lblActno") as Label;
                    int aa = Convert.ToInt32(lbl.ToolTip);
                    //btn = item.FindControl("btnMar") as Button;
                    //int aa = Convert.ToInt32(btn.ToolTip);

                    //Label lblact = item.FindControl("lblAct") as Label;
                    //int actnosss = Convert.ToInt32(lblact.ToolTip);
                    //ViewState["actnos"] = actnosss;
                
                    if (actnos == aa)
                    {
                        AjaxControlToolkit.CollapsiblePanelExtender a = (AjaxControlToolkit.CollapsiblePanelExtender)item.FindControl("cpeCourt2");
                        a.CollapseControlID = "pnlDetails";
                        a.TargetControlID = "pnlStudent";
                        a.Collapsed = false;                                               
                    }                                       
                }
               // ViewState["iactno"] = null;


               btnReport.Visible = true;

             
            }
            else if (cs.Equals(CustomStatus.RecordSaved))
            {


               objCommon.DisplayMessage(updActivity, "MAR Entry Done Successfully!", this.Page);
                BindActivityListView();
               // pnlMarEntry.Visible = false;
              //  pnlActivity.Visible = true;

                foreach (ListViewItem item in lvActivity.Items)
                {
                    int actnos = Convert.ToInt32(ViewState["iactno"]);
                    Label lbl = item.FindControl("lblActno") as Label;
                    int aa = Convert.ToInt32(lbl.ToolTip);
                    //btn = item.FindControl("btnMar") as Button;
                    //int aa = Convert.ToInt32(btn.ToolTip);

                    //Label lblact = item.FindControl("lblAct") as Label;
                    //int actnosss = Convert.ToInt32(lblact.ToolTip);
                    //ViewState["actnos"] = actnosss;

                    if (actnos == aa)
                    {
                        AjaxControlToolkit.CollapsiblePanelExtender a = (AjaxControlToolkit.CollapsiblePanelExtender)item.FindControl("cpeCourt2");
                        a.CollapseControlID = "pnlDetails";
                        a.TargetControlID = "pnlStudent";
                        a.Collapsed = false;
                    }
                }
                //ViewState["iactno"] = null;

                btnReport.Visible = true;
               
            }
            else
            {
                objCommon.DisplayMessage(updActivity, "Error in MAR Entry !", this.Page);
            }
        }
        catch (Exception)
        {

            objCommon.DisplayMessage(this.updActivity, "MAR Entry is Not Done", this.Page);
           // btnReport.Visible = true;
        }


    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
        divMsg.InnerHtml = string.Empty;
       
    }
    //protected void btnCan_Click(object sender, EventArgs e)
    //{
    //   // Response.Redirect(Request.Url.ToString());
    //    lvMarActivity.DataSource = null;
    //    lvMarActivity.DataBind();
    //    pnlMarEntry.Visible = false;
    //    BindListView();
    //}

    protected void ddlSubPoints_SelectedIndexChanged(object sender, EventArgs e)
    {
        int sum = 0;
        int sumAquirPoint = 0;
        foreach (ListViewItem itemRow in lvMarActivity.Items)
        {
            //DropDownList ddl = sender as DropDownList;
            //ddl.FindControl("ddlSubPoints");
            //TextBox txtbox = new TextBox();
            //(txtbox.FindControl("txtAcqPoint") as TextBox).ToolTip;
            DropDownList ddl = (DropDownList)itemRow.FindControl("ddlSubPoints");
            TextBox tex = (TextBox)itemRow.FindControl("txtAcqPoint");
            HiddenField hdfPoints = (HiddenField)itemRow.FindControl("hdnPoints");

            // TextBox  tex2 =((itemRow.FindControl("txtAcqPoint") as TextBox).ToolTip);
            //TextBox tex2 = (e.FindControl("txtAcqPoint") as TextBox);
            int aa = Convert.ToInt32(ddl.SelectedValue);
            if (ddl.SelectedIndex > 0)
            {
                sum = Convert.ToInt32(hdfPoints.Value) * aa;
                btnSubmit.Enabled = true;
            }
            else
            {
                sum = 0;
            }
            tex.Text = sum.ToString();
            sumAquirPoint = sumAquirPoint + sum;
            //string ff = ddl.SelectedValue;
            //DropDownList ddl= new DropDownList();
            //ddl.FindControl("ddlSubPoints");
            //int selection = Convert.ToInt32(ddl.SelectedValue);


        }
        hidSumAquirPoint.Value = sumAquirPoint.ToString();
        sumAquirPoint = 0;
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {

        divMsg.InnerHtml = string.Empty;
        lvMarActivity.DataSource = null;
        lvMarActivity.DataBind();
        pnlMarEntry.Visible = false;
        pnlActivity.Visible = true;
        btnReport.Visible = true;
        pnlActivityNew.Visible = true;
       // BindActivityListView();
        //BindListView(actno);
    }
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlBranch.Items.Clear();
        ddlBranch.Items.Add(new ListItem("Please Select", "0"));


        ddlactvity.Items.Clear();
        ddlactvity.Items.Add(new ListItem("Please Select", "0"));

        ddlBranch.SelectedIndex = 0;
        ddlactvity.SelectedIndex = 0;
        //lvStudent.DataSource = null;
        //lvStudent.DataBind();

        lvMarActivity.DataSource = null;
        lvMarActivity.DataBind();
        pnlMarEntry.Visible = false;
        pnlActivity.Visible = false;
        pnlActivityNew.Visible = false;

        if (ddlDegree.SelectedIndex > 0)
        {

            objCommon.FillDropDownList(ddlBranch, "ACD_COLLEGE_DEGREE_BRANCH CD INNER JOIN ACD_STUDENT ST ON ST.BRANCHNO = CD.BRANCHNO AND ST.COLLEGE_ID = CD.COLLEGE_ID AND ST.DEGREENO = CD.DEGREENO INNER JOIN ACD_BRANCH BR ON BR.BRANCHNO  = CD.BRANCHNO", "DISTINCT BR.BRANCHNO", "LONGNAME", "FAC_ADVISOR=" + Session["userno"] + "AND CD.COLLEGE_ID=" + Session["college_nos"], "");
            ddlDegree.Focus();

        }


    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        ShowReport("MarActivityStudentReport", "rptMarActivityStudentReport.rpt");
    }


    private void ShowReport(string reportTitle, string rptFileName)
    {


        try
        {


            DataSet ds = objACT.GetMarEntryStudentReport(Convert.ToInt32(ddlAcdYear.SelectedValue), Convert.ToInt32(ddlInstitute.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(Session["userno"]));

            if (ds.Tables[0].Rows.Count > 0)
            {
              //  objCommon.DisplayMessage(this.updActivity, divMsg.InnerHtml, this.Page);
             
                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=" + reportTitle;
                url += "&path=~,Reports,Academic," + rptFileName;
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_ACADEMIC_YEAR="+ddlAcdYear.SelectedValue + ",@P_COLLEGE_ID=" + ddlInstitute.SelectedValue + ",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_BRANCHNO=" + ddlBranch.SelectedValue + ",@P_FAC_ADVISOR=" + Convert.ToInt32(Session["userno"]);
                divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
                divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
                divMsg.InnerHtml += " </script>";
                //objCommon.DisplayMessage(this.updActivity, divMsg.InnerHtml, this.Page);
            }

            else
            {

                objCommon.DisplayMessage(this.updActivity, "Report Not Found", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "FaMentorMarEntry.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }



    //protected void lnkBtn_Click(object sender, EventArgs e)
    //{

    //    LinkButton lnk = sender as LinkButton;

    //    actno = int.Parse(lnk.CommandArgument);
    //    BindListView(actno);

    //}
    protected void lvActivity_SelectedIndexChanged(object sender, EventArgs e)
    {

        //foreach (ListViewItem item in lvActivity.Items)
        //{
        //    int actnos = Convert.ToInt32(ViewState["iactno"]);
        //    Label lbl = item.FindControl("lblActno") as Label;
        //    int aa = Convert.ToInt32(lbl.ToolTip);
        //    //btn = item.FindControl("btnMar") as Button;
        //    //int aa = Convert.ToInt32(btn.ToolTip);

        //    //Label lblact = item.FindControl("lblAct") as Label;
        //    //int actnosss = Convert.ToInt32(lblact.ToolTip);
        //    //ViewState["actnos"] = actnosss;

        //    if (actnos == aa)
        //    {
        //        AjaxControlToolkit.CollapsiblePanelExtender a = (AjaxControlToolkit.CollapsiblePanelExtender)item.FindControl("cpeCourt2");
        //        a.CollapseControlID = "pnlDetails";
        //        a.TargetControlID = "pnlStudent";
        //        a.Collapsed = false;
             
        //    }
        //    else {

        //        AjaxControlToolkit.CollapsiblePanelExtender a = (AjaxControlToolkit.CollapsiblePanelExtender)item.FindControl("cpeCourt2");
        //        a.CollapseControlID = "pnlDetails";
        //        a.TargetControlID = "pnlStudent";
        //        a.Collapsed = true;
            
            
        //    }
        //}






    }
    protected void lvActivity_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        if (e == null)
        {
            //lvActivity
        }
        if (e != null)
        {
            ListViewDataItem dataItem = (ListViewDataItem)e.Item;


            //LinkButton lnk = sender as LinkButton;

            //actno = int.Parse(lnk.CommandArgument);


            Label lblActno = dataItem.FindControl("lblActno") as Label;
            int act = Convert.ToInt32(lblActno.ToolTip);
            ViewState["actno"] = act;

            //for displaying specific panel
            //Panel pnlStudent = dataItem.FindControl("pnlStudent") as Panel;
            //Panel pnlDetails = dataItem.FindControl("pnlDetails") as Panel;

            //if (pnlStudent.Visible = true)
            //{

            //    pnlDetails.Visible = false;


            //}
            //else if(pnlDetails.Visible = true)
            //{

            //    pnlStudent.Visible = false;
            
            
            //}



          //  CollapsiblePanelExtender.
         //   pnlStudent.Collapsed = true;
             
             
            //

            ListView lv = dataItem.FindControl("lvStudent") as ListView;

            try
            {
                Button btn = lv.FindControl("btnMar") as Button;
              

                ds1 = objACT.GetStudentDetails(Convert.ToInt32(ddlAcdYear.SelectedValue), Convert.ToInt32(ddlInstitute.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(Session["userno"]), act);

              
                    lv.DataSource = ds1;
                    lv.DataBind();
                    //lv.Visible = true;
               

              

            }
            catch (Exception ex)
            {
                if (Convert.ToBoolean(Session["error"]) == true)
                    objUCommon.ShowError(Page, "Academic_Reports_Comprehensive_Stud_Report.lvCollege_ItemDatabound() --> " + ex.Message + " " + ex.StackTrace);
                else
                    objUCommon.ShowError(Page, "Server Unavailable.");
            }
        }
    }
    protected void cpeCourt2_DataBinding(object sender, EventArgs e)
    {
      //  Panel details=new Panel();
        //details=FindControl("pnlDetails");
        


        //if (pnlStudent.Visible = true)
        //{

        //    pnlDetails.Visible = false;


        //}
        //else if(pnlDetails.Visible = true)
        //{

        //    pnlStudent.Visible = false;


        //}
        
    }




    protected void lvActivityNew_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void lvActivityNew_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        
    }
    protected void lvStudentNew_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void lvStudentNew_ItemDataBound(object sender, ListViewItemEventArgs e)
    {

    }
    protected void lvStudentNew_ItemDataBound1(object sender, ListViewItemEventArgs e)
    {
        ListViewDataItem dataitem = (ListViewDataItem)e.Item;
        ListView lv = dataitem.FindControl("lvAcitivityNew") as ListView;
        DataSet ds = null;
        try
        {
            int version = 0;
            Label lblIdno = dataitem.FindControl("lblRegNo") as Label;
            int idno = Convert.ToInt32(lblIdno.ToolTip);
            version = Convert.ToInt32(objCommon.LookUp("ACD_MAR_ACTIVITY_MASTER", "VERSON", "ACTIVITYNO > 0"));
            ds = objACT.GetAllActivityNew(version,idno);
            lv.DataSource = ds;
            lv.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "FaMentorMarEntry.lvStudentNew_ItemDataBound1()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    protected void btnMarNew_Click1(object sender, EventArgs e)
    {
        try
        {

            int ActivityNo = 0;
            //Button btn = new Button();

            //ddlSubPoints

            //id=int.Parse(btn.CommandArgument);

            // ListViewDataItem dataitem=(ListViewDat
            // string ActivityName = string.Empty;



            Button btn = sender as Button;
            int iactno = Convert.ToInt32(btn.ToolTip);
            lblactivity2.Text = objCommon.LookUp("ACD_MAR_ACTIVITY_MASTER", "ACTIVITY_NAME", "ACTIVITYNO=" + iactno);
            int id = int.Parse(btn.CommandArgument);
            ViewState["IDNO"] = id;
            ViewState["iactno"] = iactno;
            DataSet ds = objCommon.FillDropDown("ACD_STUDENT S INNER JOIN ACD_BRANCH B ON (B.BRANCHNO=S.BRANCHNO) INNER JOIN ACD_DEGREE D ON (S.DEGREENO=D.DEGREENO)", "S.IDNO,B.LONGNAME,S.STUDNAME,D.DEGREENAME", "S.REGNO", "S.IDNO= " + id, "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                lblRegno2.Text = ds.Tables[0].Rows[0]["REGNO"].ToString();
                lblbranch2.Text = ddlBranch.SelectedItem.Text;
                lblCollege2.Text = ddlInstitute.SelectedItem.Text;
                //  lblactivity2.Text = ActivityName.ToString();
                lblName2.Text = ds.Tables[0].Rows[0]["STUDNAME"].ToString();
                lblDegree2.Text = ddlDegree.SelectedItem.Text;
                ViewState["BATCHNO"] = Convert.ToInt32(ddlAcdYear.SelectedValue);
                // ViewState["SUB_ACTIVITY_NO"] = ds.Tables[0].Rows[0]["SUB_ACTIVITY_NO"].ToString();
                // ViewState["CURRENT_ACQ_POINTS"] = ds.Tables[0].Rows[0]["CURRENT_ACQ_POINTS"].ToString();
                // ViewState["CURRENT_PER_POINTS"] = ds.Tables[0].Rows[0]["CURRENT_PER_POINTS"].ToString();
               // pnlActivity.Visible = false;
                pnlActivityNew.Visible = false;
            }

            divMsg.InnerHtml = string.Empty;
            BindListView2(iactno);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "FaMentorMarEntry.btnMarNew_Click1()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
}