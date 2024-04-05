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
using System.Collections.Generic;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer;
using BusinessLogicLayer.BusinessLogic.Academic;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities.Academic;

public partial class ACADEMIC_MASTERS_Grade_CC : System.Web.UI.Page
{
    #region Page Events
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    Exam ObjE = new Exam();
    GradeEntryController objGEC = new GradeEntryController();
    GradeEntry objGradeEntry = new GradeEntry();

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
                //this.CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
            }
            ViewState["College_ID"] = objCommon.LookUp("User_Acc", "UA_COLLEGE_NOS", "UA_NO=" + Convert.ToInt32(Session["userno"].ToString()));
            // objCommon.FillDropDownList(ddlSubjectType, "ACD_SUBJECTTYPE", "SUBID", "SUBNAME", "SUBID>0", "SUBID");
            //objCommon.FillDropDownList(ddlcollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID > 0 AND COLLEGE_ID IN (" + Session["college_nos"].ToString() + ") AND OrganizationId=" + Convert.ToInt32(Session["OrgId"].ToString()) + "", "COLLEGE_ID");
            objCommon.FillDropDownList(ddlGradeType, "ACD_GRADE_TYPE", "GRADE_TYPE", "GRADE_TYPE_NAME", "GRADE_TYPE>0 AND ISNULL(ACTIVESTATUS,0)=1", "GRADE_TYPE");
            // objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO>0", "DEGREENO");
            // objCommon.FillDropDownList(ddlSection, "ACD_UA_SECTION", "UA_SECTION", "UA_SECTIONNAME", "UA_SECTION>0 AND UA_SECTION IN(1,2)", "UA_SECTION");
            objCommon.FillDropDownList(ddlSection, "ACD_UA_SECTION", "UA_SECTION", "UA_SECTIONNAME", "UA_SECTION>0 AND ISNULL(ACTIVESTATUS,0)=1", "UA_SECTION");
            // MultipleCollegeBind();
            objCommon.FillListBox(ddlSubType, "ACD_SUBJECTTYPE ", "SUBID", "SUBNAME", "SUBID > 0 AND ISNULL(ACTIVESTATUS,0)=1  ", "SUBID ");
            objCommon.FillListBox(ddlALType, "ACD_UA_SECTION", "UA_SECTION", "UA_SECTIONNAME", "UA_SECTION>0 AND UA_SECTION IN(1,2)", "UA_SECTION");
            //BindListView();
            // ViewState["action"] = "add";
            int userno = Convert.ToInt32(Session["userno"]);
            //lvGrade.FindControl("tdEdit").Visible = false;
            objCommon.SetLabelData("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]));//Set label -
            //objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));  // Set Page Header  
        }
        //BindListView();
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Grade.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Grade.aspx");
        }
    }
    #endregion

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ddlcollege.SelectedIndex == -1)
        {
            objCommon.DisplayMessage(this.updGradeEntry, "Please Select Scheme!", this.Page);
            return;
        }

        int Count = 0;
        foreach (ListItem items in ddlcollege.Items)
        {
            if (items.Selected == true)
            {
                Count++;
            }
        }
        if (Count > 1)
        {
            objCommon.DisplayMessage(this.updGradeEntry, "Please Select Only One Scheme!", this.Page);
            btnLock.Visible = false;
            btnUnlock.Visible = false;
            btnSubmit.Visible = false;
            return;
        }
        //added by Pallavi M on 11/03/2024
        if (ddlSubType.SelectedIndex == -1)
        {
            objCommon.DisplayMessage(this.updGradeEntry, "Please Select Subject Type!", this.Page);
            return;

        }
        int count = 0;
        foreach (ListItem items in ddlSubType.Items)
        {
            if (items.Selected == true)
            {
                count++;
            }
        }
        if (count > 1)
        {
            objCommon.DisplayMessage(this.updGradeEntry, "Please Select Only One Subject Type!", this.Page);
            btnLock.Visible = false;
            btnUnlock.Visible = false;
            btnSubmit.Visible = false;
            return;
        }
        lvGrade.Visible = true;
        //btnSubmit.Visible = true;
        BindListView();
        #region
        //foreach (ListViewDataItem dataitem in lvGrade.Items)
        //{
        //    Exam ObjE = new Exam();
        //    objGradeEntry.GradeType = Convert.ToInt32(ddlGradeType.SelectedValue);
        //    objGradeEntry.UGPGOTNO = Convert.ToInt32(ddlSection.SelectedValue);
        //    Label Grade = dataitem.FindControl("lblGrade") as Label;
        //    TextBox GradePoint = dataitem.FindControl("txtGradePoint") as TextBox;
        //    TextBox MaxMark = dataitem.FindControl("txtMaxMark") as TextBox;
        //    TextBox MinMark = dataitem.FindControl("txtMinMark") as TextBox;
        //    TextBox GradeDesc = dataitem.FindControl("txtGradeDesc") as TextBox;
        //    DropDownList Result = dataitem.FindControl("ddlResult") as DropDownList;
        //    CheckBox Status = dataitem.FindControl("chkStatus") as CheckBox;
        //    objGradeEntry.Grade = Grade.Text.Trim();
        //    objGradeEntry.GradePoint = GradePoint.Text.Trim();
        //    objGradeEntry.MaxMark = Convert.ToInt32(MaxMark.Text.Trim());
        //    objGradeEntry.MinMark = Convert.ToInt32(MinMark.Text.Trim());
        //    objGradeEntry.GradeDesc = (GradeDesc.Text.Trim());
        //    objGradeEntry.Result = Convert.ToInt32(Result.SelectedValue);
        //    ObjE.ActiveStatus = Status.Checked ? true : false;




        //    if (Convert.ToInt16(txtMaxMark.Text) > Convert.ToInt16(txtMinMark.Text))
        //    {
        //        if (ViewState["action"] != null)
        //        {
        //            if (ViewState["action"].ToString().Equals("add"))
        //            {
        //                //Add Batch
        //                CustomStatus cs = (CustomStatus)objGEC.AddGradeEntry(objGradeEntry, Convert.ToInt32(Session["OrgId"]), ObjE.ActiveStatus);
        //                if (cs.Equals(CustomStatus.RecordSaved))
        //                {
        //                    ViewState["action"] = "add";
        //                    // Clear();
        //                    objCommon.DisplayMessage(this.updGradeEntry, "Record Saved Successfully!", this.Page);
        //                    // this.LoadGrade();
        //                    this.Clear();
        //                }
        //                else
        //                {
        //                    //objCommon.DisplayMessage(this.updBatch, "Existing Record", this.Page);
        //                    Label1.Text = "Record already exist";
        //                }
        //            }
        //            else
        //            {
        //                objGradeEntry.GradeNo = Convert.ToInt32(ViewState["Gradeno"]);
        //                CustomStatus cs = (CustomStatus)objGEC.UpdateGradeEntry(objGradeEntry, Convert.ToInt32(Session["OrgId"]), ObjE.ActiveStatus);
        //                if (cs.Equals(CustomStatus.RecordUpdated))
        //                {
        //                    ViewState["action"] = "add";
        //                    //Clear();
        //                    objCommon.DisplayMessage(this.updGradeEntry, "Record Updated Successfully!", this.Page);
        //                    // this.LoadGrade();
        //                    this.Clear();

        //                }
        //            }
        //        }
        //    }



        //    BindListView();



        //}








        //try
        //{
        //    GradeEntryController objGEC = new GradeEntryController();
        //    GradeEntry objGradeEntry = new GradeEntry();
        //    Exam ObjE = new Exam();

        //    objGradeEntry.Grade = txtGrade.Text.Trim();
        //    objGradeEntry.GradePoint = txtGradePoint.Text.Trim();
        //   // objGradeEntry.Subid = Convert.ToInt32(ddlSubjectType.SelectedValue);
        //    objGradeEntry.MaxMark = Convert.ToInt32(txtMaxMark.Text.Trim());
        //    objGradeEntry.MinMark = Convert.ToInt32(txtMinMark.Text.Trim());
        //    objGradeEntry.GradeType = Convert.ToInt32(ddlGradeType.SelectedValue);
        //    objGradeEntry.GradeDesc = (txtGradeDesc.Text.Trim());
        //   // objGradeEntry.DegreeNo = Convert.ToInt32(ddlDegree.SelectedValue);
        //    objGradeEntry.CollegeCode = Session["colcode"].ToString();
        //    objGradeEntry.UGPGOTNO = Convert.ToInt32(ddlSection.SelectedValue);
        //    objGradeEntry.Result =Convert.ToInt32(rdoPassFailType.SelectedValue);
        //   // string Result = rdoPass.Checked ? "Pass" : "Fail" ;



        //    if (hfGradenew.Value == "true")
        //    {
        //        ObjE.ActiveStatus = true;
        //    }
        //    else
        //    {
        //        ObjE.ActiveStatus = false;
        //    }

        //    string chkexist = objCommon.LookUp("ACD_GRADE G INNER JOIN ACD_UA_SECTION S ON (S.UA_SECTION=G.UGPGOT)", "count(1)", "GRADE='" + txtGrade.Text.Trim() + "' AND GRADEPOINT ='" + txtGradePoint.Text.Trim() + "' AND MAXMARK ='" + Convert.ToInt32(txtMaxMark.Text.Trim()) + "' AND MINMARK ='" + Convert.ToInt32(txtMinMark.Text.Trim()) + "' AND GRADE_TYPE ='" + Convert.ToInt32(ddlGradeType.SelectedValue) + "' AND DESC_GRADE ='" + (txtGradeDesc.Text.Trim()) + "' AND  UGPGOT='" + Convert.ToInt32(ddlSection.SelectedValue) + "' AND  RESULT='" + rdoPassFailType.SelectedValue + "'");

        //    if ((chkexist != null || chkexist != string.Empty) && chkexist != "0")
        //    {
        //        objCommon.DisplayMessage(this.updGradeEntry, "Record already exist", this.Page);
        //        this.Clear();
        //        return;

        //    }
        //    //Check whether to add or update

        //    if (Convert.ToInt16(txtMaxMark.Text) > Convert.ToInt16(txtMinMark.Text))
        //    {
        //        if (ViewState["action"] != null)
        //        {
        //            if (ViewState["action"].ToString().Equals("add"))
        //            {
        //                //Add Batch
        //                CustomStatus cs = (CustomStatus)objGEC.AddGradeEntry(objGradeEntry, Convert.ToInt32(Session["OrgId"]), ObjE.ActiveStatus);
        //                if (cs.Equals(CustomStatus.RecordSaved))
        //                {
        //                    ViewState["action"] = "add";
        //                   // Clear();
        //                    objCommon.DisplayMessage(this.updGradeEntry, "Record Saved Successfully!", this.Page);
        //                   // this.LoadGrade();
        //                    this.Clear();
        //                }
        //                else
        //                {
        //                    //objCommon.DisplayMessage(this.updBatch, "Existing Record", this.Page);
        //                    Label1.Text = "Record already exist";
        //                }
        //            }
        //            else
        //            {
        //                objGradeEntry.GradeNo = Convert.ToInt32(ViewState["Gradeno"]);
        //                CustomStatus cs = (CustomStatus)objGEC.UpdateGradeEntry(objGradeEntry, Convert.ToInt32(Session["OrgId"]), ObjE.ActiveStatus);
        //                if (cs.Equals(CustomStatus.RecordUpdated))
        //                {
        //                    ViewState["action"] = "add";
        //                    //Clear();
        //                    objCommon.DisplayMessage(this.updGradeEntry, "Record Updated Successfully!", this.Page);
        //                   // this.LoadGrade();
        //                    this.Clear();

        //                }
        //            }
        //        }
        //    }
        //    else
        //    {
        //        Label1.Text = "Min Mark Not Greater Than Max Mark";
        //    }





        //}
        //catch (Exception ex)
        //{
        //    if (Convert.ToBoolean(Session["error"]) == true)
        //        objUCommon.ShowError(Page, "Academic_Masters_Grade.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
        //    else
        //        objUCommon.ShowError(Page, "Server UnAvailable");
        //}
        #endregion
    }

    private void LoadGrade()
    {
        try
        {
            //DataSet ds = objCommon.FillDropDown("ACD_GRADE G INNER JOIN ACD_UA_SECTION S ON (S.UA_SECTION=G.UGPGOT)", "GRADENO", "GRADE,GRADEPOINT,MAXMARK,MINMARK,UA_SECTIONNAME AS UA_SECTIONNAME,DESC_GRADE,case when isnull(G.ACTIVESTATUS,0)=0 then 'Inactive' else 'Active' end as ACTIVESTATUS,case when isnull(G.RESULT,0)=0 then 'Fail' else 'Pass' end as RESULT", "GRADENO>0", "GRADENO DESC");
            //DataSet ds = objCommon.FillDropDown("ACD_GRADE G INNER JOIN ACD_GRADE_NEW N ON (N.GRADENO=G.GRADENO_NEW)", "GRADENO", "GRADE,GRADEPOINT,MAXMARK,MINMARK,ACTIVESTATUS, RESULT  ", "GRADENO>0", "GRADENO DESC");
            DataSet ds = objCommon.FillDropDown("ACD_GRADE G INNER JOIN ACD_GRADE_NEW N ON (N.GRADENO=G.GRADENO_NEW)", "G.GRADENO", "G.GRADE,GRADEPOINT,MAXMARK,IsLock,MINMARK,DESC_GRADE,CASE when isnull(G.ACTIVESTATUS,0)=0 then 'Inactive' else 'Active' end as ACTIVESTATUS, RESULT  ", "G.GRADENO>0", "G.GRADENO DESC");
            if (ds != null && ds.Tables.Count > 0)
            {
                lvGrade.DataSource = ds.Tables[0];
                lvGrade.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Masters_Grade.LoadSlot()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void Clear()
    {
        
        Label1.Text = string.Empty;
        ddlSection.SelectedIndex = 0;
        ddlGradeType.SelectedIndex = 0;
        lvGrade.Visible = false;
        btnSave.Visible = true;
        btnSubmit.Visible = false;
        btnLock.Visible = false;
        btnUnlock.Visible = false;
        //rdoPassFailType.SelectedValue = "-1";
    }

    private void BindListView()
    {
        try
        {
            //int UGPGOTNO = 0;

            GradeEntryController objGEC = new GradeEntryController();
            // DataSet ds = objGEC.GetAllGradeEntry(Convert.ToInt32(ddlGradeType.SelectedValue), Convert.ToInt32(1),Convert.ToInt32(ddlSubType.SelectedValue),Convert.ToInt32(ddlcollege.SelectedValue));

            // UGPGOTNO = Convert.ToInt32(objCommon.LookUp("ACD_DEGREE", "DEGREETYPEID", "DEGREENO=" + Convert.ToInt16(ViewState["degreeno"])));//added gaurav

            // DataSet ds = objGEC.GetAllGradeEntry(Convert.ToInt32(ddlGradeType.SelectedValue), Convert.ToInt32(UGPGOTNO), Convert.ToInt32(ddlSubType.SelectedValue), Convert.ToInt32(ViewState["college_id"]));
            // DataSet ds = objGEC.GetAllGradeEntry(Convert.ToInt32(ddlGradeType.SelectedValue), Convert.ToInt32(UGPGOTNO), Convert.ToInt32(ddlSubType.SelectedValue), Convert.ToInt32(ViewState["college_id"]), Convert.ToInt32(ViewState["schemeno"]));
            // DataSet ds = objGEC.GetAllGradeEntry(Convert.ToInt32(ddlGradeType.SelectedValue), Convert.ToInt32(UGPGOTNO), Convert.ToInt32(ddlSubType.SelectedValue), Convert.ToInt32(ViewState["college_id"]), 0);
            DataSet ds = objGEC.GetAllGradeEntry(Convert.ToInt32(ddlGradeType.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(ddlSubType.SelectedValue), Convert.ToInt32(ViewState["college_id"]), Convert.ToInt32(ViewState["schemeno"]));
            lvGrade.DataSource = ds;
            lvGrade.DataBind();
            // this.LoadGrade();
            lvGrade.Visible = true;
            btnSubmit.Visible = true;
            btnLock.Visible = true;
            btnUnlock.Visible = true;
            btnSave.Visible = true;
            //btnSubmit.Visible = true;
            // btnLock.Visible = true;
            //foreach (ListViewDataItem item in lvGrade.Items)
            //{
            //    objGradeEntry.GradeType = Convert.ToInt32(ddlGradeType.SelectedValue);
            //    objGradeEntry.UGPGOTNO = Convert.ToInt32(ddlSection.SelectedValue);
            //    int SubType=Convert.ToInt32(ddlSubType.SelectedValue);
            //    Label Grade = item.FindControl("lblGrade") as Label;
            //    int Gradeno_New = Convert.ToInt32(Grade.ToolTip);
            //    int CollegeId = Convert.ToInt32(ddlcollege.SelectedValue);

            //    string Chk = objCommon.LookUp("ACD_GRADE", "COUNT(1)", "GRADE_TYPE ='" + objGradeEntry.GradeType + "' AND UGPGOT = '" + objGradeEntry.UGPGOTNO + "' AND GRADENO_NEW = '" + Gradeno_New + "' AND SUBID = '" + SubType + "' AND COLLEGE_ID = '" + CollegeId + "' AND IsLock = '" + 1 + "'");

            //    if ((Chk != null || Chk != string.Empty) && Chk != "0")
            //    {
            //        lvGrade.Visible = true;
            //        btnSave.Visible = false;
            //        btnLock.Visible = false;
            //        btnSubmit.Visible = false;
            //        btnUnlock.Visible = true;
            //    }
            //    else
            //    {
            //        lvGrade.Visible = true;
            //        btnSave.Visible = true;
            //        btnSubmit.Visible = true;
            //        btnLock.Visible = true;
            //        btnUnlock.Visible = false;
            //    }
            //}
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Masters_Grade.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    //private bool CheckDuplicateRoom()
    //{
    //    //Check Room Name is duplicate entry or not
    //    if (ViewState["roomname"].ToString() == txtRoomName.Text)
    //    {
    //        //Room name not changed while editing
    //        return false;
    //    }

    //    int cnt = Convert.ToInt16(objCommon.LookUp("ACD_ROOM", "COUNT(*)", "ROOMNAME LIKE '" + txtRoomName.Text.Trim() + "'"));
    //    if (cnt > 0)
    //        return true;
    //    else
    //        return false;
    //}
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnEdit = sender as ImageButton;
        ViewState["Gradeno"] = int.Parse(btnEdit.CommandArgument);
        int gradeno = Convert.ToInt32(ViewState["Gradeno"]);
        ViewState["action"] = "edit";
        ShowDetails(gradeno);
    }
    private void ShowDetails(int gradeno)
    {
        SqlDataReader dr = null;
        dr = objGEC.GetGradebyGradeno(gradeno);
        if (dr != null)
        {
            if (dr.Read())
            {
                //txtGrade.Text = dr["GRADE"].ToString();
                // txtGradePoint.Text = dr["GRADEPOINT"].ToString();
                //txtGradeDesc.Text = dr["DESC_GRADE"].ToString();
                //txtMaxMark.Text = Convert.ToInt32(dr["MAXMARK"]).ToString();
                // txtMinMark.Text = Convert.ToInt32(dr["MINMARK"]).ToString();
                // ddlGradeType.SelectedValue = dr["GRADE_TYPE"].ToString();
                // ddlSection.SelectedValue = dr["UGPGOT"].ToString();
                // rdoPassFailType.SelectedValue = dr["RESULT"].ToString();
                //if(Convert.ToInt32(rdoPassFailType.SelectedValue = dr["RESULT"].ToString()) == 0)
                //{
                //}  
                if (dr["ACTIVESTATUS"].ToString() == "Active")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "settimeslotgradenew(true);", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "settimeslotgradenew(false);", true);
                }

                //if (ds.Tables[0].Rows[0]["ACTIVESTATUS"].ToString() == "Active")
                //{
                //    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "settimeslot(true);", true);
                //}
                //else
                //{
                //    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "settimeslot(false);", true);
                //}
            }
        }
    }
    protected void txtMaxMark_TextChanged(object sender, EventArgs e)
    {
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int DegreeNo = 0; int Branchno = 0; int College_id = 0; int Schemeno = 0;
        int subids = 0;
        try
        {
            foreach (ListItem items in ddlcollege.Items)
            {
                if (items.Selected == true)
                {
                    //DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlcollege.SelectedValue));
                    DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(items.Value));
                    if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
                    {
                        DegreeNo = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]);
                        Branchno = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]);
                        College_id = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]);
                        Schemeno = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]);

                        //Added by Pallavi M on  11/03/2024
                        DataSet dst = objCommon.FillDropDown("ACD_SUBJECTTYPE", "SUBID", "SUBNAME", "SUBID>0 AND ISNULL(ACTIVESTATUS,0)=1 ", "SUBID");

                        if (dst.Tables[0].Rows.Count > 0 && dst.Tables[0] != null)
                        {
                            foreach (ListItem itms in ddlSubType.Items)
                            {
                                if (itms.Selected == true)
                                {
                                    subids = Convert.ToInt32(itms.Value);
                                   
                                    foreach (ListViewDataItem item in lvGrade.Items)
                                    {
                                        Exam ObjE = new Exam();
                                        objGradeEntry.GradeType = Convert.ToInt32(ddlGradeType.SelectedValue);
                                        // objGradeEntry.UGPGOTNO = Convert.ToInt32(ddlSection.SelectedValue);
                                        objGradeEntry.UGPGOTNO = Convert.ToInt32(objCommon.LookUp("ACD_DEGREE", "DEGREETYPEID", "DEGREENO=" + DegreeNo));//added gaurav
                                        Label Grade = item.FindControl("lblGrade") as Label;
                                        TextBox GradePoint = item.FindControl("txtGradePoint") as TextBox;
                                        TextBox MaxMark = item.FindControl("txtMaxMark") as TextBox;
                                        TextBox MinMark = item.FindControl("txtMinMark") as TextBox;
                                        TextBox GradeDesc = item.FindControl("txtGradeDesc") as TextBox;
                                        DropDownList Result = item.FindControl("ddlResult") as DropDownList;
                                        CheckBox Status = item.FindControl("chkStatus") as CheckBox;
                                        objGradeEntry.Grade = Grade.Text.Trim();
                                        objGradeEntry.GradePoint = GradePoint.Text.Trim();
                                        string MaxMarks = MaxMark.Text.Trim();
                                        string MinMarks = MinMark.Text.Trim();
                                        // string maxMark =(objGradeEntry.MaxMark).ToString();
                                        //objGradeEntry.MinMark = Convert.ToInt32(MinMark.Text.Trim());
                                        objGradeEntry.GradeDesc = GradeDesc.Text.Trim();
                                        objGradeEntry.Result = Convert.ToInt32((Result.SelectedValue));
                                        ObjE.ActiveStatus = Status.Checked ? true : false;
                                        objGradeEntry.CollegeCode = Session["colcode"].ToString();
                                        int OrgID = Convert.ToInt32(Session["OrgId"]);
                                        int Gradeno_New = Convert.ToInt32(Grade.ToolTip);
                                       // int SubType = Convert.ToInt32(ddlSubType.SelectedValue);
                                        int CollegeId = College_id;//added gaurav

                                        if (Convert.ToInt32(Session["OrgId"]) == 18)
                                        {

                                            CustomStatus cs = (CustomStatus)objGEC.AddGradeEntry(objGradeEntry, OrgID, ObjE.ActiveStatus, Gradeno_New, subids, CollegeId, MaxMarks, MinMarks, DegreeNo, Schemeno);
                                            if (cs.Equals(CustomStatus.RecordSaved))
                                            {
                                                //ViewState["action"] = "add";
                                                objCommon.DisplayMessage(this.updGradeEntry, "Record Saved Successfully!", this.Page);
                                                //this.LoadGrade();
                                                this.BindListView();
                                            }
                                            else if (cs.Equals(CustomStatus.RecordUpdated))
                                            {
                                                objCommon.DisplayMessage(this.updGradeEntry, "Record Update successfully!", this.Page);
                                                this.BindListView();
                                            }
                                        }
                                        else
                                        {
                                            if ((decimal.Parse(MaxMark.Text) >= decimal.Parse(MinMark.Text)) && decimal.Parse(MaxMark.Text) > 0)
                                            {
                                                //if (decimal.Parse(MaxMark.Text) > decimal.Parse(MinMark.Text))
                                                //{
                                                //  CustomStatus cs=(CustomStatus)objGEC.AddGradeEntry(objGradeEntry,OrgID, 
                                                //}
                                                //if (ViewState["action"] != null)
                                                //{
                                                //    if (ViewState["action"].ToString().Equals("add"))
                                                //    {
                                                CustomStatus cs = (CustomStatus)objGEC.AddGradeEntry(objGradeEntry, OrgID, ObjE.ActiveStatus, Gradeno_New, subids, CollegeId, MaxMarks, MinMarks, DegreeNo, Schemeno);
                                                if (cs.Equals(CustomStatus.RecordSaved))
                                                {
                                                    //ViewState["action"] = "add";

                                                    objCommon.DisplayMessage(this.updGradeEntry, "Record Saved Successfully!", this.Page);
                                                    //this.LoadGrade();
                                                    this.BindListView();

                                                }
                                                else if (cs.Equals(CustomStatus.RecordUpdated))
                                                {
                                                    objCommon.DisplayMessage(this.updGradeEntry, "Record Update successfully!", this.Page);
                                                    this.BindListView();
                                                }
                                            }
                                            else if (decimal.Parse(MaxMark.Text) == 0 && decimal.Parse(MinMark.Text) == 0)
                                            {
                                                // objCommon.DisplayMessage(this, "Min Mark Not Greater Than Max Mark", this.Page);
                                            }

                                            else if (decimal.Parse(MaxMark.Text) == 0 && decimal.Parse(MinMark.Text) == 0)
                                            {
                                                // objCommon.DisplayMessage(this, "Min Mark Not Greater Than Max Mark", this.Page);
                                            }
                                            else
                                            {
                                                objCommon.DisplayMessage(this, "Min Mark Not Greater Than Max Mark", this.Page);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            lvGrade.Visible = false;
            btnSubmit.Visible = false;
            btnSave.Visible = true;
            BindListView();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Masters_Grade.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlGradeType_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnSubmit.Visible = false;
        btnSave.Visible = true;
        lvGrade.Visible = false;
        btnLock.Visible = false;
        btnUnlock.Visible = false;
        ddlSubType.SelectedIndex = -1;
    }
    protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        MultipleCollegeBind();
        btnSubmit.Visible = false;
        btnSave.Visible = true;
        lvGrade.Visible = false;
        btnLock.Visible = false;
        btnUnlock.Visible = false;
        ddlSubType.SelectedIndex = -1;
    }
    protected void btnLock_Click(object sender, EventArgs e)
    {
        int DegreeNo = 0; int Branchno = 0; int College_id = 0; int Schemeno = 0;
        try
        {
            foreach (ListItem items in ddlcollege.Items)
            {
                if (items.Selected == true)
                {
                    //DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlcollege.SelectedValue));
                    DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(items.Value));
                    if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
                    {
                        DegreeNo = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]);
                        Branchno = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]);
                        College_id = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]);
                        Schemeno = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]);
                       
                        foreach (ListViewDataItem item in lvGrade.Items)
                        {
                            objGradeEntry.GradeType = Convert.ToInt32(ddlGradeType.SelectedValue);
                            //objGradeEntry.UGPGOTNO = Convert.ToInt32(ddlSection.SelectedValue);
                            objGradeEntry.UGPGOTNO = Convert.ToInt32(objCommon.LookUp("ACD_DEGREE", "DEGREETYPEID", "DEGREENO=" + DegreeNo));//added gaurav
                            int SubType = Convert.ToInt32(ddlSubType.SelectedValue);
                            Label Grade = item.FindControl("lblGrade") as Label;
                            objGradeEntry.Grade = Grade.Text.Trim();
                            int OrgID = Convert.ToInt32(Session["OrgId"]);
                            int Gradeno_New = Convert.ToInt32(Grade.ToolTip);
                            // int CollegeId = Convert.ToInt32(ddlcollege.SelectedValue);
                            int CollegeId = College_id;
                            //CustomStatus cs = (CustomStatus)objGEC.LockGradeEntry(objGradeEntry, OrgID, Gradeno_New, SubType, CollegeId);
                            CustomStatus cs = (CustomStatus)objGEC.LockGradeEntryNew(objGradeEntry, OrgID, SubType, CollegeId, Schemeno, DegreeNo);
                            if (cs.Equals(CustomStatus.RecordSaved))
                            {
                                objCommon.DisplayMessage(this.updGradeEntry, "Record Locked Successfully!", this.Page);
                                BindListView();
                                //this.LoadGrade();
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Masters_Grade.btnLock_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnUnlock_Click(object sender, EventArgs e)
    {
        try
        {
            int DegreeNo = 0; int Branchno = 0; int CollegeId = 0; int Schemeno = 0;
            foreach (ListItem items in ddlcollege.Items)
            {
                if (items.Selected == true)
                {
                    //DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlcollege.SelectedValue));
                    DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(items.Value));
                    if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
                    {
                        DegreeNo = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]);
                        Branchno = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]);
                        CollegeId = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]);
                        Schemeno = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]);

                        foreach (ListViewDataItem item in lvGrade.Items)
                        {
                            objGradeEntry.GradeType = Convert.ToInt32(ddlGradeType.SelectedValue);
                            // objGradeEntry.UGPGOTNO = Convert.ToInt32(ddlSection.SelectedValue);
                            objGradeEntry.UGPGOTNO = Convert.ToInt32(objCommon.LookUp("ACD_DEGREE", "DEGREETYPEID", "DEGREENO=" + DegreeNo));//added gaurav
                            int SubType = Convert.ToInt32(ddlSubType.SelectedValue);
                            Label Grade = item.FindControl("lblGrade") as Label;
                            objGradeEntry.Grade = Grade.Text.Trim();
                            int OrgID = Convert.ToInt32(Session["OrgId"]);
                            //int Gradeno_New = Convert.ToInt32(Grade.ToolTip);
                            // int CollegeId = Convert.ToInt32(ddlcollege.SelectedValue);
                            //CustomStatus cs = (CustomStatus)objGEC.UnLockGradeEntry(objGradeEntry, OrgID, Gradeno_New, SubType,CollegeId);
                            CustomStatus cs = (CustomStatus)objGEC.UnLockGradeEntryNew(objGradeEntry, OrgID, SubType, CollegeId, Schemeno, DegreeNo);

                            if (cs.Equals(CustomStatus.RecordSaved))
                            {
                                objCommon.DisplayMessage(this.updGradeEntry, "Record UnLocked Successfully!", this.Page);
                                BindListView();
                                // this.LoadGrade();
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Masters_Grade.btnUnLock_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlSubType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToString(ViewState["EDIT_HIT"]) == "1")
            {
            }
            else
            {
                string subno = string.Empty;

                string subnos = string.Empty;

                foreach (ListItem items in ddlSubType.Items)
                {
                    if (items.Selected == true)
                    {
                        subnos += (items.Value).Split('-')[0] + ',';
                    }
                }
                if (subnos.Length > 1)
                {
                    subnos = subnos.Remove(subnos.Length - 1);
                }

                if (subnos.Length > 0)
                {
                    DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlSubType.SelectedValue));

                    if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
                    {
                        //ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                        //ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                        //ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                       // ViewState["subtype"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SUBID"]).ToString();
                    }
                }
                else
                {
                }
            }
        }
        catch (Exception)
        {
            
            throw;
        }
        
    }
    protected void ddlcollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnSubmit.Visible = false;
        btnSave.Visible = true;
        lvGrade.Visible = false;
        btnLock.Visible = false;
        btnUnlock.Visible = false;
        ddlGradeType.SelectedIndex = 0;
        ddlSubType.SelectedIndex = -1;
        ddlSection.SelectedIndex = 0;
    }
    private void MultipleCollegeBind()
    {
        try
        {
            //objCommon.FillListBox(ddlcollege, "ACD_COLLEGE_SCHEME_MAPPING  M INNER JOIN   ACD_DEGREE D ON (D.DEGREENO=M.DEGREENO) ", "COSCHNO", "COL_SCHEME_NAME", "COLLEGE_ID IN(" + ViewState["College_ID"].ToString() + ") AND COSCHNO>0 AND M.COLLEGE_ID > 0 AND M.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) + "AND DEGREETYPEID=" + ddlSection.SelectedValue, "COLLEGE_ID");
            objCommon.FillListBox(ddlcollege, "ACD_COLLEGE_SCHEME_MAPPING  M INNER JOIN   ACD_DEGREE D ON (D.DEGREENO=M.DEGREENO) ", "COSCHNO", "COL_SCHEME_NAME", "COSCHNO>0 AND M.COLLEGE_ID > 0 AND M.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) + "AND DEGREETYPEID=" + ddlSection.SelectedValue, "COLLEGE_ID");
            //objCommon.FillListBox(ddlcollege, "ACD_COLLEGE_MASTER WITH (NOLOCK)", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME", "COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "COLLEGE_ID");
        }
        catch
        {
            throw;
        }
    }
    protected void ddlCollege_SelectedIndexChanged1(object sender, EventArgs e)
    {
        if (Convert.ToString(ViewState["EDIT_HIT"]) == "1")
        {
        }
        else
        {
            string collegenose = string.Empty;
            string collegenos = string.Empty;
            foreach (ListItem items in ddlcollege.Items)
            {
                if (items.Selected == true)
                {
                    collegenos += (items.Value).Split('-')[0] + ','; //Add by maithili [08-09-2022]
                    //collegenos += items.Value + ','; 
                    //collegenames += items.Text + ',';
                }
            }
            if (collegenos.Length > 1)
            {
                collegenos = collegenos.Remove(collegenos.Length - 1);
            }

            if (collegenos.Length > 0)
            {
                // MultipleSessionBind(collegenos);
                // ddlSession.Focus();
                //PopulateDegreeList();
                // BindDegreeWithMultipleCollege(collegenos);
                //if (ddlcollege.SelectedIndex > 0)
                //{
                DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlcollege.SelectedValue));

                if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
                {
                    ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                    ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                    ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                    ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();
                    // FILL DROPDOWN  ddlSession_SelectedIndexChanged
                }
            }
            else
            {
            }
        }
    }
}