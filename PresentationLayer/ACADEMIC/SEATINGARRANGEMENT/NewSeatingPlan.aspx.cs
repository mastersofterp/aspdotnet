//=================================================================================
// PROJECT NAME  : U-AIMS                                                          
// MODULE NAME   : SEATING PLAN 
// CREATION DATE : 20-APR-2012                                                     
// CREATED BY    : PRIYANKA KABADE               
// MODIFIED DATE :               
// MODIFIED DESC :                                  
//=================================================================================

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
using System.Linq;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;


public partial class ACADEMIC_NewSeatingPlan : System.Web.UI.Page
{

    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    SeatingController objSC = new SeatingController();
    Seating objSeating = new Seating();

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
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }

                PopulateDropDownList();
                //FillDropDownList();
                //ViewState["TYPE"] = "1";
                ViewState["action"] = "done";
               
            }
            Session["examTbl"] = null;
        }
        divMsg.InnerHtml = string.Empty;
    }
    #endregion

    #region  Seating Plan
    # region Click Events
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    //SHOWS ALL THE EXAMS FOR SELECED SESSION & DEGREE
    protected void btnProcess_Click(object sender, EventArgs e)
    {
        //SHOWS ALL THE EXAM DATES FOR SELECTED DEGREE AND SESSION
        ShowExamDate();
    }

    //Show the Shift  Details when button Clicked
    protected void btnShiftDetails_Click(object sender, EventArgs e)
    {
        try
        {
            ClearControls();    //CLEAR ALL GRIDVIEWS
            BindExamInfo();     //BIND THE SELECTED EXAM DATE'S ALL PAPERS IN A VIEW
            BindRoomInfo();     //BIND ALL THE ROOM INFORMATION REGARDING ROOM PREFERENCES AND CAPACITY IN A ROOM GRIDVIEW
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_NewSeatingPlan.btnShiftDetails_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }

    }
 
    // Clear All feilds on Page
    protected void btnClear_Click(object sender, EventArgs e)
    {
        gvOdd.DataSource = null;
        gvOdd.DataBind();
        gvEven.DataSource = null;
        gvEven.DataBind();
        Response.Redirect(Request.Url.ToString());
    }

    // Clear All feilds on Page
    protected void btnClearall_Click(object sender, EventArgs e)
    {
        gvShift.DataSource = null;
        gvShift.DataBind();
        txtOdd.Text = string.Empty;
        txtEven.Text = string.Empty;
        txtTotal.Text = string.Empty;
        txtSelected.Text = string.Empty;
        gvRoom.DataSource = null;
        gvRoom.DataBind();
        //SHOWS ALL THE EXAM DATES FOR SELECTED DEGREE AND SESSION
     
        ShowExamDate();

    }

    //TO SET THE PREFERENCES UPATED OR NOT OF ALL THE ROOM
    protected void btnSet_Click(object sender, EventArgs e)
    {
        try
        {
            string roomnos = string.Empty;
            string preferences = string.Empty;
            for (int i = 0; i < gvRoom.Rows.Count; i++)
            {
                roomnos += (gvRoom.Rows[i].FindControl("txtPreference") as TextBox).ToolTip.Trim() + ',';   //CONCAT ALL THE ROOMNO IN STRING
                if ((gvRoom.Rows[i].FindControl("txtPreference") as TextBox).Text.Trim() == "")
                {
                    objCommon.DisplayMessage("Error!Room Preference cannot be kept Blank!!", this.Page);
                    return;
                }
                else
                    preferences += (gvRoom.Rows[i].FindControl("txtPreference") as TextBox).Text.Trim() + ',';  //CONCAT CORRESPONDING PREFERENCES OF ROOM
            }
            int retstatus = objSC.UpdateRoomsPreferences(roomnos, preferences);     //UPDATE ALL THE ROOM'S PREFERENCE'S
            if (retstatus == 2)
                objCommon.DisplayMessage("Preferences Updated!", this.Page);
            else
                if (retstatus == 1)
                    objCommon.DisplayMessage("Preferences Saved!", this.Page);
            else
                objCommon.DisplayMessage("Error in Updating Preferences!", this.Page);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic.btnSet_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    //GET BACK TO DEFAULT PREFERENCES STORED IN DATABASE
    protected void btnRevert_Click(object sender, EventArgs e)
    {
        //SET TO DEFAULT
        BindRoomInfo();
    }   
    
    //GENERATES SEATING PLAN FOR THE ALL THE BRANCHES ONE BY ONE
    protected void btnSeating_Click(object sender, EventArgs e)
    {
        DataSet ds = (DataSet)Session["examTbl"];
        //SET ALL THE SELECTED VALUES OF THE BRANCHES IN A STRING
        string[] bench1 = { ddlBench1.SelectedValue, ddlBench2.SelectedValue, ddlBench3.SelectedValue.ToString(), ddlBench4.SelectedValue.ToString() };
        int sessionno = Convert.ToInt16(ddlSession.SelectedValue);
        int return_status = 0;
        int count = 0;

        //Session["examTbl"] STORES THE LAST 'EXAM INFO' HIT TABLE 
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            //FOR ALL THE EXAMNO IN TABLE FOR A DAY & SLOT
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                //IF SELECTED VALUE MATCHES THE CURRENT ROW 
                if (bench1.Contains(ds.Tables[0].Rows[i]["exdtno"].ToString()))
                {
                    //GET THE POSITION OF VALUE THAT MATCHED 
                    for (int j = 0; j < bench1.Length; j++)
                    {
                        //MATCH THE POSTION VALUE AND DATASET CURRENT ROW EXAMNO 
                        if (bench1[j] == ds.Tables[0].Rows[i]["exdtno"].ToString())
                        {
                            int dayno = Convert.ToInt16(ds.Tables[0].Rows[i]["DAYNO"].ToString());          //GET THE DAY FROM CURRENT ROW
                            int slotno = Convert.ToInt16(ds.Tables[0].Rows[i]["SLOTNO"].ToString());        //GET THE SLOT FROM CURRENT ROW
                            int courseno = Convert.ToInt16(ds.Tables[0].Rows[i]["COURSENO"].ToString());    //GET THE COURSEHNO
                            int branchno = Convert.ToInt16(ds.Tables[0].Rows[i]["BRANCHNO"].ToString());

                            int oddeven = j == 0 || j == 3 ? 1 : 2;                                         //GET THE jTH VALUE TO GET ODD_EVEN COLUMN FROM SELECTION
                            int position = j == 0 || j == 1 ? 1 : 2;                                        //GET THE jTH VALUE TO GET POSTION ROW FROM  SELECTION
                            count++;                                                                        //ATLEAST ONE SELECTION IS NEEDED AMONGST 4 BRANCH DROPDOWNS
                            
                            //SAVE THE BRANCH ROW/COLUMN SELECTION FOR THE BRANCH ONE BY ONE
                            return_status = objSC.InsertSeatingArrangement(sessionno, Convert.ToInt16(bench1[j]), dayno, slotno, courseno, oddeven, branchno, position);
                           // if(!(return_status.Equals(CustomStatus.RecordSaved) || return_status.Equals(CustomStatus.RecordUpdated)))
                            if (return_status.Equals(CustomStatus.Error) || return_status.Equals(CustomStatus.Others) || return_status.Equals(CustomStatus.TransactionFailed) || return_status.Equals(CustomStatus.Others))
                            {
                                objCommon.DisplayMessage("Error while saving Seating Arrangement!!", this.Page);
                                return;
                            }
                            break;
                        }
                    }
                }
            }
            if (count == 0)
                objCommon.DisplayMessage("Please select atleast 1 branch!!", this.Page);
            else
                objCommon.DisplayMessage("Seating Arrangement Done!!", this.Page);
            BindExamInfo();
            BindRoomInfo();
        }
        else
            objCommon.DisplayMessage("No Record Found!!", this.Page);
    }

    //REPORT SEATING PLAN ROOMWISE
    protected void btnRoomwise_Click(object sender, EventArgs e)
    {
        string dayno = string.Empty;
        string slotno = string.Empty;
        //IF EXAM SELECTION IS DONE ELSE 0
        if (Session["examTbl"] != null)
        {
            dayno = ((DataSet)Session["examTbl"]).Tables[0].Rows[0]["DAYNO"].ToString();
            slotno = ((DataSet)Session["examTbl"]).Tables[0].Rows[0]["SLOTNO"].ToString();
        }
        ShowReport("Seating_Arrangement_Roomwise", "rptSeatingPlanRoomwise.rpt", dayno.Equals(string.Empty) ? "0" : dayno, slotno.Equals(string.Empty) ? "0" : slotno);
    }
    
    //REPORT SEATING PLAN STASTICS FOR THE SEATS OF ROOMS
    protected void btnStastical_Click(object sender, EventArgs e)
    {
        string dayno = string.Empty;
        string slotno = string.Empty;
        //IF EXAM SELECTION IS DONE THEN THAT PARTICULAR RECORD ELSE 0 (FOR ALL EXAMS)
        if (Session["examTbl"] != null)
        {
            dayno = ((DataSet)Session["examTbl"]).Tables[0].Rows[0]["DAYNO"].ToString();
            slotno = ((DataSet)Session["examTbl"]).Tables[0].Rows[0]["SLOTNO"].ToString();
        }
        ShowReport("Seating_Arrangement_Stastical", "rptSeatingPlanStastical.rpt", dayno.Equals(string.Empty) ? "0" : dayno, slotno.Equals(string.Empty) ? "0" : slotno);
    }
    
    //ACTUAL SEATING ARRANGEMENT
    protected void btnRoomSeats_Click(object sender, EventArgs e)
    {
        string dayno = string.Empty;
        string slotno = string.Empty;
        //IF EXAM SELECTION IS DONE THEN THAT PARTICULAR RECORD ELSE 0 (FOR ALL EXAMS)
        if (Session["examTbl"] != null)
        {
            dayno = ((DataSet)Session["examTbl"]).Tables[0].Rows[0]["DAYNO"].ToString();
            slotno = ((DataSet)Session["examTbl"]).Tables[0].Rows[0]["SLOTNO"].ToString();
        }
        ShowReportRoomSeats("Seating_Plan_RoomSeats", "rptSeatingPlanRoomSeats.rpt", dayno.Equals(string.Empty) ? "0" : dayno, slotno.Equals(string.Empty) ? "0" : slotno, "0");
    }

    #region Omitted Part
    //Show the Odd Gridview Details on button click
    protected void btnOdd_Click(object sender, EventArgs e)
    {
        try
        {
            char ch = ('-');
            int remain = 0;
            int totalodd = 0;
            int i = 0;
            int odd = 0;
            DataTable dt = new DataTable();
            DataRow dr;
            if (Session["oddTbl"] != null && ((DataTable)Session["oddTbl"]) != null)
            {
                dt = ((DataTable)Session["oddTbl"]);
            }
            else
            {
                //CustomStatus cs = new CustomStatus();
                //Create Columns
                dt.Columns.Add(new DataColumn("SEMESTERNAME"));
                dt.Columns.Add(new DataColumn("BRANCHNAME"));
                dt.Columns.Add(new DataColumn("COURSENAME"));
                dt.Columns.Add(new DataColumn("COURSENO"));
                dt.Columns.Add(new DataColumn("REG"));
                dt.Columns.Add(new DataColumn("EX"));
                dt.Columns.Add(new DataColumn("TOT"));
            }
            foreach (ListViewDataItem item in lvDate.Items)
            {
                RadioButton rdochk = item.FindControl("rdoSelect") as RadioButton;
                if (rdochk.Checked == true)
                {
                    Label lblexamdate = item.FindControl("lblExamDate") as Label;
                    Label lblSlotname = item.FindControl("lblSlotName") as Label;

                    foreach (GridViewRow d1 in gvShift.Rows)
                    {
                        CheckBox chk = (CheckBox)d1.FindControl("cbshift");
                        if (chk.Checked == true && chk.Enabled == true)
                        {
                            Label lblccode = d1.FindControl("lblCourse") as Label;
                            string[] ccode = lblccode.Text.Split(ch);

                            int exdtno = int.Parse(((d1.FindControl("lblexDtno") as Label).ToolTip));
                            int status = int.Parse(((d1.FindControl("lblStatus") as Label).ToolTip));

                            if (status == 0)
                            {
                                DataSet oddcol = objSC.GetOddColumnInfoById(lblexamdate.Text, Convert.ToInt32(lblSlotname.ToolTip), ccode[0].ToString(), Convert.ToInt32(ViewState["TYPE"]));

                                if (oddcol.Tables.Count > 0)
                                {

                                    status = 1;

                                    if (oddcol != null)
                                    {
                                        dr = dt.NewRow();
                                        dr["SEMESTERNAME"] = ((Label)d1.Cells[0].FindControl("lblSem")).Text;
                                        dr["BRANCHNAME"] = ((Label)d1.Cells[1].FindControl("lblBranch")).Text;
                                        dr["COURSENAME"] = ((Label)d1.Cells[2].FindControl("lblCourse")).Text;
                                        dr["COURSENO"] = ((Label)d1.Cells[2].FindControl("lblCourse")).ToolTip;
                                        dr["REG"] = ((Label)d1.Cells[3].FindControl("lblReg")).Text;
                                        dr["EX"] = ((Label)d1.Cells[4].FindControl("lblEx")).Text;
                                        dr["TOT"] = ((Label)d1.Cells[5].FindControl("lblTot")).Text;
                                        dt.Rows.Add(dr);

                                        remain += Convert.ToInt32(dt.Rows[i]["TOT"].ToString());

                                        CustomStatus cs = (CustomStatus)objSC.UpdateShiftStatus(exdtno, status);

                                        if (cs.Equals(CustomStatus.RecordUpdated))
                                        {
                                            chk.Enabled = false;
                                        }
                                        else
                                        {
                                            chk.Enabled = true;
                                            objCommon.DisplayMessage("Server Error", this.Page);
                                        }
                                    }

                                    gvOdd.Visible = true;
                                    gvOdd.DataSource = dt;
                                    gvOdd.DataBind();
                                }
                                odd = Convert.ToInt32(txtOdd.Text);
                                totalodd = Convert.ToInt32(odd - remain);
                                // txtRemainingOdd.Text = totalodd.ToString();
                                Session["oddTbl"] = dt;

                                gvOdd.Visible = true;
                                gvOdd.DataSource = dt;
                                gvOdd.DataBind();

                            }
                            i++;
                        }
                    }
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_NewSeatingPlan.btnOdd_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    //Show the Even Gridview Details on button click
    protected void btnEven_Click(object sender, EventArgs e)
    {
        try
        {
            char ch = ('-');
            int remain = 0;
            int totaleven = 0;
            int i = 0;
            int even = 0;
            DataTable dt = new DataTable();
            DataRow dr;

            if (Session["evenTbl"] != null && ((DataTable)Session["evenTbl"]) != null)
            {
                dt = ((DataTable)Session["evenTbl"]);
            }
            else
            {
                //CustomStatus cs = new CustomStatus();
                //Create Columns
                dt.Columns.Add(new DataColumn("SEMESTERNAME"));
                dt.Columns.Add(new DataColumn("BRANCHNAME"));
                dt.Columns.Add(new DataColumn("COURSENAME"));
                dt.Columns.Add(new DataColumn("COURSENO"));
                dt.Columns.Add(new DataColumn("REG"));
                dt.Columns.Add(new DataColumn("EX"));
                dt.Columns.Add(new DataColumn("TOT"));
            }
            foreach (ListViewDataItem item in lvDate.Items)
            {
                RadioButton rdochk = item.FindControl("rdoSelect") as RadioButton;
                if (rdochk.Checked == true)
                {
                    Label lblexamdate = item.FindControl("lblExamDate") as Label;
                    Label lblSlotname = item.FindControl("lblSlotName") as Label;

                    foreach (GridViewRow d1 in gvShift.Rows)
                    {
                        CheckBox chk = (CheckBox)d1.FindControl("cbshift");
                        if (chk.Checked == true && chk.Enabled == true)
                        {
                            Label lblccode = d1.FindControl("lblCourse") as Label;
                            string[] ccode = lblccode.Text.Split(ch);

                            int exdtno = int.Parse(((d1.FindControl("lblexDtno") as Label).ToolTip));
                            int status = int.Parse(((d1.FindControl("lblStatus") as Label).ToolTip));

                            if (status == 0)
                            {
                                DataSet evencol = objSC.GetOddColumnInfoById(lblexamdate.Text, Convert.ToInt32(lblSlotname.ToolTip), ccode[0].ToString(), Convert.ToInt32(ViewState["TYPE"]));

                                if (evencol.Tables.Count > 0)
                                {
                                    status = 1;

                                    if (evencol != null)
                                    {
                                        dr = dt.NewRow();
                                        dr["SEMESTERNAME"] = ((Label)d1.Cells[0].FindControl("lblSem")).Text;
                                        dr["BRANCHNAME"] = ((Label)d1.Cells[1].FindControl("lblBranch")).Text;
                                        dr["COURSENAME"] = ((Label)d1.Cells[2].FindControl("lblCourse")).Text;
                                        dr["COURSENO"] = ((Label)d1.Cells[2].FindControl("lblCourse")).ToolTip;
                                        dr["REG"] = ((Label)d1.Cells[3].FindControl("lblReg")).Text;
                                        dr["EX"] = ((Label)d1.Cells[4].FindControl("lblEx")).Text;
                                        dr["TOT"] = ((Label)d1.Cells[5].FindControl("lblTot")).Text;
                                        dt.Rows.Add(dr);

                                        remain += Convert.ToInt32(dt.Rows[i]["TOT"].ToString());

                                        CustomStatus cs = (CustomStatus)objSC.UpdateShiftStatus(exdtno, status);

                                        if (cs.Equals(CustomStatus.RecordUpdated))
                                        {
                                            chk.Enabled = false;
                                        }
                                        else
                                        {
                                            chk.Enabled = true;
                                            objCommon.DisplayMessage("Server Error", this.Page);
                                        }
                                    }

                                    gvEven.Visible = true;
                                    gvEven.DataSource = dt;
                                    gvEven.DataBind();
                                }
                                even = Convert.ToInt32(txtEven.Text);
                                totaleven = Convert.ToInt32(even - remain);
                                Session["evenTbl"] = dt;

                                gvEven.Visible = true;
                                gvEven.DataSource = dt;
                                gvEven.DataBind();

                            }
                            i++;
                        }

                    }
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_NewSeatingPlan.btnEven_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    //DELETE THE RECORD IN ODD DETAILS GRIDVIEW IN ROWWISE MANNER
    protected void btnDeleteOddDetail_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnDeleteOddDetail = sender as ImageButton;
            DataTable dt;

            if (Session["oddTbl"] != null && ((DataTable)Session["oddTbl"]) != null)
            {
                dt = ((DataTable)Session["oddTbl"]);
                dt.Rows.Remove(this.GetEditableDataRow(dt, btnDeleteOddDetail.CommandArgument));
                foreach (GridViewRow gvr in gvOdd.Rows)
                {
                    int totalodd = 0;
                    int remain = 0;
                    int odd = 0;

                    gvOdd.Visible = true;
                    gvOdd.DataSource = dt;
                    gvOdd.DataBind();

                    for (int i = 0; dt.Rows.Count > i; i++)
                    {
                        totalodd = totalodd + Convert.ToInt32(dt.Rows[i]["TOT"].ToString());
                        remain += Convert.ToInt32(dt.Rows[i]["TOT"].ToString());
                        odd = Convert.ToInt32(txtOdd.Text);
                    }
                    Session["oddTbl"] = dt;
                }

                foreach (GridViewRow shift in gvShift.Rows)
                {

                    CheckBox chk = (CheckBox)shift.FindControl("cbshift");
                    int exdtno = int.Parse(((shift.FindControl("lblexDtno") as Label).ToolTip));
                    int status = int.Parse(((shift.FindControl("lblStatus") as Label).ToolTip));
                    int courseno = int.Parse(((shift.FindControl("lblCourse") as Label).ToolTip));


                    if (Session["oddTbl"] != null)
                    {
                        status = 0;// For Checkbox Checked Status changed 
                        CustomStatus cs = (CustomStatus)objSC.UpdateShiftStatus(exdtno, status);

                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            if (chk.Checked == true && Convert.ToInt32(btnDeleteOddDetail.CommandArgument) == courseno)
                            {
                                chk.Enabled = true;
                            }
                        }
                        else
                        {
                            objCommon.DisplayMessage("Error", this.Page);
                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage("Error", this.Page);
                    }
                }
            }
            else
            {
                ViewState["SelectedRecord"] = null;
                Session["oddTbl"] = null;
                gvOdd.Visible = false;
                gvOdd.DataSource = null;
                gvOdd.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic.btnDeleteOddDetail_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    //Delete Even Gridview Details
    protected void btnDeleteEvenDetail_Click(object sender, EventArgs e)
    {
        try
        {

            ImageButton btnDeleteEvenDetail = sender as ImageButton;
            DataTable dt;

            if (Session["evenTbl"] != null && ((DataTable)Session["evenTbl"]) != null)
            {
                dt = ((DataTable)Session["evenTbl"]);
                dt.Rows.Remove(this.GetEditableDataRow(dt, btnDeleteEvenDetail.CommandArgument));
                //txtRemainEven.Text = Convert.ToString(Convert.ToInt32(btnDeleteEvenDetail.ToolTip) + Convert.ToInt32(txtRemainEven.Text));
                foreach (GridViewRow gvr in gvEven.Rows)
                {
                    int totaleven = 0;
                    int remain = 0;
                    int even = 0;

                    gvEven.Visible = true;
                    gvEven.DataSource = dt;
                    gvEven.DataBind();

                    for (int i = 0; dt.Rows.Count > i; i++)
                    {
                        totaleven = totaleven + Convert.ToInt32(dt.Rows[i]["TOT"].ToString());
                        remain += Convert.ToInt32(dt.Rows[i]["TOT"].ToString());
                        even = Convert.ToInt32(txtEven.Text);
                    }
                    Session["evenTbl"] = dt;
                }

                foreach (GridViewRow shift in gvShift.Rows)
                {

                    CheckBox chk = (CheckBox)shift.FindControl("cbshift");
                    int exdtno = int.Parse(((shift.FindControl("lblexDtno") as Label).ToolTip));
                    int status = int.Parse(((shift.FindControl("lblStatus") as Label).ToolTip));
                    int courseno = int.Parse(((shift.FindControl("lblCourse") as Label).ToolTip));

                    if (Session["evenTbl"] != null)
                    {
                        status = 0;// For Checkbox Checked Status changed 
                        CustomStatus cs = (CustomStatus)objSC.UpdateShiftStatus(exdtno, status);

                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            if (chk.Checked == true && Convert.ToInt32(btnDeleteEvenDetail.CommandArgument) == courseno)
                            {
                                chk.Enabled = true;
                            }
                        }
                    }
                }
            }
            else
            {
                ViewState["Record"] = null;
                Session["evenTbl"] = null;
                gvEven.Visible = false;
                gvEven.DataSource = null;
                gvEven.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic.btnDeleteEvenDetail_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    #endregion

    # endregion

    #region OTHER EVENTS
    
    //SELECTED INDEX CHANGES OF ANY OF BRANCHES
    protected void ddlBench_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlSelected = sender as DropDownList;  //GET THE DROPDOWN WHOSE SELECTED INDEX CHANGED
        DropDownList ddlbranch = new DropDownList();        // NEW BRANCH TO KEEP TRACK OF THE SELECTIONS OF ALL THE  BRANCHES

        //FILL THE SELECTED ITEMS OF 4 DROPDOWNS IN A NEW DROPDOWN
        ddlbranch.Items.Add(new ListItem("Please Select", "0"));
        ddlbranch.Items.Add(ddlBench1.SelectedItem);
        ddlbranch.Items.Add(ddlBench2.SelectedItem);
        ddlbranch.Items.Add(ddlBench3.SelectedItem);
        ddlbranch.Items.Add(ddlBench4.SelectedItem);

        //RE-FILL ALL BRANCHES
        FillBranchDropDowns(ddlBench1);
        FillBranchDropDowns(ddlBench2);
        FillBranchDropDowns(ddlBench3);
        FillBranchDropDowns(ddlBench4);

        //RESET THE TOTAL FIELD
        //txtRemainEven.Text = txtEven.Text;
        //txtRemainingOdd.Text = txtRemainingOdd.Text;

        //NOW REMOVE ITEMS WHICH WERE SELECTED AT FIRST TO GET UNIQUE EXAMNO(PAPER) IN EACH DROPDOWNS
        for (int i = 1; i < ddlbranch.Items.Count; i++)
        {
            ddlBench1.Items.Remove(ddlbranch.Items[i]);
            ddlBench2.Items.Remove(ddlbranch.Items[i]);
            ddlBench3.Items.Remove(ddlbranch.Items[i]);
            ddlBench4.Items.Remove(ddlbranch.Items[i]);
        }

        //ADD AND REGAIN THE FOCUS ON PREVIOUS SELECTED ITEM FOREACH DROPDOWNS INDIVIDUALLY
        ddlBench1.Items.Add(ddlbranch.Items[1]);
        ddlBench1.SelectedValue = ddlbranch.Items[1].Value;

        ddlBench2.Items.Add(ddlbranch.Items[2]);
        ddlBench2.SelectedValue = ddlbranch.Items[2].Value;

        ddlBench3.Items.Add(ddlbranch.Items[3]);
        ddlBench3.SelectedValue = ddlbranch.Items[3].Value;

        ddlBench4.Items.Add(ddlbranch.Items[4]);
        ddlBench4.SelectedValue = ddlbranch.Items[4].Value;

        //CHECK IF 'PLEASE SELECT' IS ALSO REMOVED IF YES THEN ADD IT AGAIN
        if (!ddlBench1.Items.Contains(ddlbranch.Items[0]))
        {
            ddlBench1.Items.Add(ddlbranch.Items[0]);
        }
        if (!ddlBench2.Items.Contains(ddlbranch.Items[0]))
        {
            ddlBench2.Items.Add(ddlbranch.Items[0]);
        }
        if (!ddlBench3.Items.Contains(ddlbranch.Items[0]))
        {
            ddlBench3.Items.Add(ddlbranch.Items[0]);
        }
        if (!ddlBench4.Items.Contains(ddlbranch.Items[0]))
        {
            ddlBench4.Items.Add(ddlbranch.Items[0]);
        }
    }
#endregion

    #region User Methods

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=NewSeatingPlan.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=NewSeatingPlan.aspx");
        }
    }

    //FILL DEGREE AND SESSION
    private void PopulateDropDownList()
    {
        try
        {
            objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0 AND FLOCK = 1 ", "SESSIONNO DESC");
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO > 0", "DEGREENO");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_NewSeatingPlan.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    //SHOW EXAM DATE DETAILS & ALL THE SHIFTS IN A DAY
    private void ShowExamDate()
    {
        try
        {
            ClearControls();
            lvDate.DataSource = null;
            lvDate.DataBind();
            if (ddlSession.SelectedIndex > 0 && ddlDegree.SelectedIndex > 0)//&& ddlSem.SelectedIndex > 0)
            {
                DataSet dsshowdata;
                //GET THE EXAMS FOR SELECTED SESSION AND DEGREE
                dsshowdata = objSC.GetStudentsExamDate(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue));
                
                if (dsshowdata != null && dsshowdata.Tables.Count > 0)
                {
                    if (dsshowdata.Tables[0].Rows.Count > 0)
                    {
                        lvDate.Visible = true;
                        lvDate.DataSource = dsshowdata;
                        lvDate.DataBind();
                    }
                    else
                    {
                        lvDate.Visible = false;
                        lvDate.DataSource = null;
                        lvDate.DataBind();
                    }
                }
                else
                {
                    objCommon.DisplayMessage("Error in Saving Marks!", this.Page);
                }
            }
            else
            {
                lvDate.Visible = false;
                lvDate.DataSource = null;
                lvDate.DataBind();
                objCommon.DisplayMessage("Already Record Shown!", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_NewSeatingPlan_ShowExamDate() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    //Get The Editable Data Row for Edit Records on it
    private DataRow GetEditableDataRow(DataTable dt, string value)
    {
        DataRow dataRow = null;
        try
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["COURSENO"].ToString() == value)
                {
                    dataRow = dr;
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic.GetEditableDataRow() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return dataRow;
    }

    //Bind the Gridview For Even Column Details
    private void BindGridView_EvenColumnDetails(DataTable dt)
    {
        try
        {
            gvEven.DataSource = dt;
            gvEven.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic.BindListView_EvenColumnDetails() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    //Bind Exam Info in gridview
    private void BindExamInfo()
    {
        Session["examTbl"] = null;
        ClearControls();
        foreach (ListViewDataItem item in lvDate.Items)
        {
            RadioButton rdochk = item.FindControl("rdoSelect") as RadioButton;
            if (rdochk.Checked == true && rdochk != null)
            {
                Label lblexamdate = item.FindControl("lblExamDate") as Label;
                Label lblSlotname = item.FindControl("lblSlotName") as Label;
                string[] str = lblexamdate.Text.Split('-');

                //BIND ALL THE PAPERS FOR SELECTED UNIQUE EXAMNO
                DataSet ds = objSC.GetExamDateInfoById(str[0], Convert.ToInt32(lblSlotname.ToolTip), Convert.ToInt32(rdoSelection.SelectedValue));
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        gvShift.Visible = true;
                        gvShift.DataSource = ds;
                        gvShift.DataBind();
                        Session["examTbl"] = ds;
                        foreach (GridViewRow shift in gvShift.Rows)
                        {
                            TextBox txtRemainigStudent = shift.FindControl("txtRemainigStudent") as TextBox;
                            Label lblStatus = shift.FindControl("lblStatus") as Label;
                            int status = Convert.ToInt16(lblStatus.ToolTip.ToString());
                            if (status == 1)
                            {
                                lblStatus.Enabled = false;
                            }
                        }
                        //GET ALL THE PAPERS IN DROPDOWNS
                        FillBranchDropDowns(ddlBench1);
                        FillBranchDropDowns(ddlBench2);
                        FillBranchDropDowns(ddlBench3);
                        FillBranchDropDowns(ddlBench4);
                        int totalstudent = 0;

                        for (int i = 0; ds.Tables[0].Rows.Count > i; i++)
                        {
                            totalstudent = totalstudent + Convert.ToInt32(ds.Tables[0].Rows[i]["TOT"].ToString());
                        }
                        txtTotal.Text = totalstudent.ToString();    // DISPLAY THE TOTAL STUDENTS ATTENDING EXAM ON SELECTED DAY & SLOT
                    }
                    else
                    {
                        gvShift.Visible = false;
                        gvShift.DataSource = null;
                        gvShift.DataBind();
                        objCommon.DisplayMessage("No Exam Record Found!", this.Page);
                    }
                }
                else
                {
                    gvShift.Visible = false;
                    gvShift.DataSource = null;
                    gvShift.DataBind();
                    objCommon.DisplayMessage("No Exam Record Found!", this.Page);
                }
                break;
            }
        }
    }
    
    //BIND ROOM INFO
    private void BindRoomInfo()
    {
        DataSet showRoom = objSC.GetRoomPreference();
        string preference = string.Empty;
        if (showRoom != null && showRoom.Tables.Count > 0)
        {
            if (showRoom.Tables[0].Rows.Count > 0)
            {
                gvRoom.Visible = true;
                gvRoom.DataSource = showRoom;
                gvRoom.DataBind();
                int totalcapacity = 0;
                int oddcapacity = 0;
                int evencapacity = 0;

                for (int i = 0; showRoom.Tables[0].Rows.Count > i; i++)
                {
                    totalcapacity = totalcapacity + Convert.ToInt32(showRoom.Tables[0].Rows[i]["ACTUALCAPACITY"].ToString()) * 2;
                    int total = Convert.ToInt32(totalcapacity.ToString());
                    evencapacity = Convert.ToInt32(total) / 2;
                    oddcapacity = Convert.ToInt32(total - evencapacity);
                    preference +=',' +showRoom.Tables[0].Rows[i]["PREFERENCE"].ToString(); // CONCAT THE PREFERENCES IN A STRING
                }

                txtSelected.Text = totalcapacity.ToString();
                txtEven.Text = evencapacity.ToString();
                txtOdd.Text = oddcapacity.ToString();
                
                //STORE THESE UNIQUE PREFERENCES IN A HIDDEN FIELD TO CHECK WHEN USER INPUTS/CHANGES THE PREFERENCES
                //SO THAT UNIQUE PREFERENCES ARE SET
                //THE PREFERENCE LIST EDITED BY JAVASCRIPT FUNCTION IF PREFERENCES REMOVED/CHANGED/ADDED
                hdfPreferenceList.Value = preference+",";   
                hdfTotRoom.Value = showRoom.Tables[0].Rows.Count.ToString();
            }
            else
            {
                gvRoom.Visible = false;
                gvRoom.DataSource = null;
                gvRoom.DataBind();
                objCommon.DisplayMessage("No Rooms Found!", this.Page);
            }

        }
    }

    //CLEAR ALL GRIDS
    private void ClearControls()
    {
        gvRoom.DataSource = null;
        gvRoom.DataBind();
        gvShift.DataSource = null;
        gvShift.DataBind();
        ddlBench1.Items.Clear();
        ddlBench1.Items.Add(new ListItem("Please Select","0"));
        ddlBench2.Items.Clear();
        ddlBench2.Items.Add(new ListItem( "Please Select","0"));
        ddlBench3.Items.Clear();
        ddlBench3.Items.Add(new ListItem("Please Select","0"));
        ddlBench4.Items.Clear();
        ddlBench4.Items.Add(new ListItem("Please Select","0"));
    }

    //SHOW REPORT 'ROOMWISE' & 'ROOM STASTICAL'
    private void ShowReport(string reportTitle, string rptFileName, string dayno, string slotno)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("ACADEMIC")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;

            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DAYNO= " + dayno + ",@P_SLOTNO= " + slotno;
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_CertificateMaster.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    //SHOW REPORT SEATING ARRNAGEMENT(SEATING POSITIONS) ROOM WISE
    private void ShowReportRoomSeats(string reportTitle, string rptFileName, string dayno, string slotno, string roomno)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("ACADEMIC")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;

            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue +  ",@P_DAYNO= " + dayno + ",@P_SLOTNO= " + slotno + ",@P_ROOMNO= " + roomno;
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_CertificateMaster.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    //FILL THE BRANCH DROPDOWN (ALL THE FOUR ONE BY ONE)
    private void FillBranchDropDowns(DropDownList ddlBranch)
    {
        try
        {
            DataSet ds = (DataSet)Session["examTbl"];
            ddlBranch.Items.Clear();
            ddlBranch.Items.Add(new ListItem("Please Select", "0"));
            //SHOW ONLY THOSE EXAMS IN BRANCH DROP DOWNS WHOSE SEATING ARRANGEMENT IS NOT DONE (STATUS = 0)
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (ds.Tables[0].Rows[i]["STATUS"].ToString() != "1")
                        ddlBranch.Items.Add(new ListItem(ds.Tables[0].Rows[i]["EXAM_COURSE"].ToString(), ds.Tables[0].Rows[i]["EXDTNO"].ToString()));
                }
            }
        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_NewSeatingPlan.FillBranchDropDowns() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    #endregion


    #endregion
    #region COMMENTED/UNUSED CODE
    #region OMITTED UNUSED PART
    protected void txtTotal_TextChanged(object sender, EventArgs e)
    {
        this.txtTotal.Text = txtTotal.Text;
    }

    //protected void btnRmvEven_Click(object sender, EventArgs e)
    //{
    //    char ch = ('-');
    //    ArrayList productsToDelete = new ArrayList();

    //    foreach (GridViewRow row in gvEven.Rows)
    //    {
    //        if (row.RowType == DataControlRowType.DataRow)
    //        {
    //            CheckBox chkDelete = (CheckBox)row.Cells[0].FindControl("cbeven");
    //            Label lblccode = row.FindControl("lblCourse") as Label;
    //            string[] ccode = lblccode.Text.Split(ch);

    //            if (chkDelete != null)
    //            {
    //                if (chkDelete.Checked)
    //                {
    //                    string productId = row.Cells[1].Text;
    //                    productsToDelete.Add(ccode);
    //                }
    //            }
    //        }
    //    }
    //}





    //For Each Row can be checked when delete button click

    protected void gvOdd_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //    try
        //    {
        //    if (e.CommandName == "DELETE")
        //    {
        //        ImageButton btnDeleteOddDetail = gvOdd.FindControl("btnDeleteOddDetail")as ImageButton ;
        //        int dtno = Convert.ToInt32(btnDeleteOddDetail.ToolTip);

        //        foreach (GridViewRow shift in gvShift.Rows)
        //        {
        //            CheckBox chk = (CheckBox)shift.FindControl("cbshift");
        //            int exdtno = int.Parse(((shift.FindControl("lblexDtno") as Label).ToolTip));
        //            //int status = int.Parse(((shift.FindControl("lblStatus") as Label).ToolTip));
        //            if (dtno == exdtno)
        //            {
        //                chk.Enabled = true;
        //                chk.Checked = false;
        //            }
        //            else
        //            {
        //                chk.Enabled = false;
        //                chk.Checked = true; 
        //            }
        //        }  

        //    }
        //    }
        //    catch (Exception ex)
        //    {
        //         if (Convert.ToBoolean(Session["error"]) == true)
        //            objCommon.ShowError(Page, "Academic.gvOdd_RowCommand() --> " + ex.Message + " " + ex.StackTrace);
        //        else
        //            objCommon.ShowError(Page, "Server Unavailable.");
        //    }
    }
    #endregion

    #endregion
    
}

