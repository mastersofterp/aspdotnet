//==============================================
//MODIFIED BY: Swati Ghate
//MODIFIED DATE:10-11-2014
//PURPOSE: TO UPDATE THE DESIGN & DISPLAY PATH
//==============================================
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;  


public partial class ESTABLISHMENT_LEAVES_Master_OD_Passing_Path : System.Web.UI.Page
{
    //Creating objects of Class Files Common,UAIMS_COMMON,LeaveController
    Common objCommon = new Common();

    UAIMS_Common objUCommon = new UAIMS_Common();

    LeavesController objPAPath = new LeavesController();

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

                pnlAdd.Visible = false; pnlEmpList.Visible = false;
                pnlList.Visible = true;
                FillCollege();
                FillDepartment();
                FillPAuthority();
                BindListViewPAPath();

                btnAdd.Visible = true;
                btnShowReport.Visible = true;
                btnSave.Visible = false;
                btnCancel.Visible = false;
                btnBack.Visible = false;
                //Set Report Parameters 
               // objCommon.ReportPopUp(btnShowReport, "pagetitle=UAIMS&path=~" + "," + "Reports" + "," + "ESTABLISHMENT" + "," + "LEAVES" + "," + "ESTB_PAPAth.rpt&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@username=" + Session["userfullname"].ToString(), "UAIMS");
            }
        }
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=createdomain.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=createdomain.aspx");
        }
    }
    protected void BindListViewPAPath()
    {
        try
        {
            //DataSet ds = objPAPath.GetAllPAPath(Convert.ToInt32(ddlCollege.SelectedValue));
            DataSet ds = objPAPath.GetAllODPAPath(Convert.ToInt32(ddlCollege.SelectedValue));
            if (ds.Tables[0].Rows.Count <= 0)
            {
                btnShowReport.Visible = false;               
            }
            else
            {
                btnShowReport.Visible = true;                
            }
            lvPAPath.DataSource = ds;
            lvPAPath.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_PA_Path.BindListViewPAPath ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }
    }

    protected void dpPager_PreRender(object sender, EventArgs e)
    {
        //Bind the ListView with Domain  
        BindListViewPAPath();
    }

    private void Clear()
    {
        ddlDept.SelectedIndex = ddlCollege.SelectedIndex= 0;
        ddlPA01.SelectedIndex = 0;
        ddlPA02.SelectedIndex = 0;
        ddlPA03.SelectedIndex = 0;
        ddlPA04.SelectedIndex = 0;
        ddlPA05.SelectedIndex = 0;
        ddlPA02.Enabled = false;
        ddlPA03.Enabled = false;
        ddlPA04.Enabled = false;
        ddlPA05.Enabled = false;
        txtPAPath.Text = string.Empty;
        
    }

    private void clearnew()
    {
        ddlCollege.SelectedIndex = 0;
        ddlDept.SelectedIndex = 0;
        ddlPA01.SelectedIndex = 0;
        ddlPA02.SelectedIndex = 0;
        ddlPA03.SelectedIndex = 0;
        ddlPA04.SelectedIndex = 0;
        ddlPA05.SelectedIndex = 0;
        ddlPA02.Enabled = false;
        ddlPA03.Enabled = false;
        ddlPA04.Enabled = false;
        ddlPA05.Enabled = false;
        txtPAPath.Text = string.Empty;
        lblEmpName.Text = string.Empty;
        ViewState["IDNO"] = null;
        
        pnlEmpList.Visible = false;
        trEmp.Visible = false;
       

        ddlCollege.Enabled = true;
        ddlDept.Enabled = true;


    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Clear();
        pnlAdd.Visible = true;
        pnlbtn.Visible = true;
        pnlList.Visible = false;
        pnlPAPaList.Visible = false;
        ddlPA02.Enabled = false;
        ddlPA03.Enabled = false;
        ddlPA04.Enabled = false;
        ddlPA05.Enabled = false;
        ViewState["action"] = "add";

        btnAdd.Visible = false;
        btnShowReport.Visible = false;
        btnSave.Visible = true;
        btnCancel.Visible = true;
        btnBack.Visible = true;
    }
    private void FillCollege()
    {
        //objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_NO", "COLLEGE_NAME", "COLLEGE_NO IN(" + Session["college_nos"] + ")", "COLLEGE_NO ASC");
        objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ")", "COLLEGE_ID ASC"); //Modified By Saket Singh on 09-12-2016
        if (Session["usertype"].ToString() != "1")
        {
            ListItem removeItem = ddlCollege.Items.FindByValue("0");
            ddlCollege.Items.Remove(removeItem);
        }


    }
    private void FillDepartment()
    {
        try
        {
            objCommon.FillDropDownList(ddlDept, "PAYROLL_SUBDEPT", "SUBDEPTNO", "SUBDEPT", "", "SUBDEPT");
            //objCommon.FillDropDownList(ddlDept, "PAYROLL_EMPMAS E INNER JOIN PAYROLL_SUBDEPT D ON(E.SUBDEPTNO=D.SUBDEPTNO)", "DISTINCT E.SUBDEPTNO", "D.SUBDEPT", "E.COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + "", "D.SUBDEPT");
           
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_PA_Path.Fill_Department ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void FillPAuthority()
    {
        try
        {
            objCommon.FillDropDownList(ddlPA01, "PAYROLL_LEAVE_PASSING_AUTHORITY", "PANO", "PANAME", "COLLEGE_NO > 0", "PANAME");
            //if (ddlCollege.SelectedValue != "0")
            //{
            //    objCommon.FillDropDownList(ddlPA01, "PAYROLL_LEAVE_PASSING_AUTHORITY", "PANO", "PANAME", "COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + "", "PANAME");
            //}
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_PA_Path.FillPAuthority ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //Clear();
        clearnew();
        ViewState["action"] = "add";
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        //Clear();
        clearnew();
        pnlAdd.Visible = false; pnlEmpList.Visible = false;
        pnlList.Visible = true;

        btnAdd.Visible = true;
        btnShowReport.Visible = true;
        btnSave.Visible = false;
        btnCancel.Visible = false;
        btnBack.Visible = false;
        pnlPAPaList.Visible = true;
    }

   
     
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            pnlbtn.Visible = false;
            Leaves objLeaves = new Leaves();
            DataTable dtEmpRecord = new DataTable();
            dtEmpRecord.Columns.Add("IDNO");
            objLeaves.LNO = 0;
            objLeaves.DEPTNO = Convert.ToInt32(ddlDept.SelectedValue);
            //objLeaves.PAN01 = Convert.ToInt32(ddlPA01.SelectedValue);
            //objLeaves.PAN02 = Convert.ToInt32(ddlPA02.SelectedValue);
            //objLeaves.PAN03 = Convert.ToInt32(ddlPA03.SelectedValue);
            //objLeaves.PAN04 = Convert.ToInt32(ddlPA04.SelectedValue);
            //objLeaves.PAN05 = Convert.ToInt32(ddlPA05.SelectedValue);
            if (ddlPA01.SelectedValue != string.Empty)
            {
                objLeaves.PAN01 = Convert.ToInt32(ddlPA01.SelectedValue);
            }
            else
            {
                objLeaves.PAN01 = 0;
            }

            //  objLeaves.PAN03 = Convert.ToInt32(ddlPA03.SelectedValue);
            if (ddlPA02.SelectedValue != string.Empty)
            {
                objLeaves.PAN02 = Convert.ToInt32(ddlPA02.SelectedValue);
            }
            else
            {
                objLeaves.PAN02 = 0;
            }

            if (ddlPA03.SelectedValue != string.Empty)
            {
                objLeaves.PAN03 = Convert.ToInt32(ddlPA03.SelectedValue);
            }
            else
            {
                objLeaves.PAN03 = 0;
            }
            if (ddlPA04.SelectedValue != string.Empty)
            {
                objLeaves.PAN04 = Convert.ToInt32(ddlPA04.SelectedValue);
            }
            else
            {
                objLeaves.PAN04 = 0;
            }
            if (ddlPA05.SelectedValue != string.Empty)
            {
                objLeaves.PAN05 = Convert.ToInt32(ddlPA05.SelectedValue);
            }
            else
            {
                objLeaves.PAN05 = 0;
            }
            objLeaves.PAPATH = Convert.ToString(txtPAPath.Text);
            objLeaves.COLLEGE_CODE = Convert.ToString(Session["colcode"]);
            objLeaves.COLLEGE_NO = Convert.ToInt32(ddlCollege.SelectedValue);
            int count = 0; int count_record = 0;
            if (trEmp.Visible == true)
            {
                DataRow dr = dtEmpRecord.NewRow();
                int idno = Convert.ToInt32(ViewState["IDNO"]);
                dr["IDNO"] = idno;
                dtEmpRecord.Rows.Add(dr);
                dtEmpRecord.AcceptChanges();
                objLeaves.PAPNO = Convert.ToInt32(ViewState["PAPNO"].ToString());

            }
            else
            {
                foreach (ListViewItem lvitem in lvEmployees.Items)
                {
                    CheckBox chkIdNo = lvitem.FindControl("chkIdNo") as CheckBox;
                    if (chkIdNo.Checked == true)
                    {
                        count = count + 1;
                        objLeaves.EMPNO = Convert.ToInt32(chkIdNo.ToolTip);
                        if (ViewState["action"].ToString().Equals("add"))
                        {
                            DataSet ds = objCommon.FillDropDown("payroll_OD_passing_authority_path", "PAPNO", "IDNO", "IDNO=" + objLeaves.EMPNO + " ", "");
                            if (ds.Tables[0].Rows.Count <= 0)
                            {
                                count_record = count_record + 1;
                                DataRow dr = dtEmpRecord.NewRow();
                                dr["IDNO"] = objLeaves.EMPNO;
                                dtEmpRecord.Rows.Add(dr);
                                dtEmpRecord.AcceptChanges();
                            }

                        }
                        else
                        {

                            objLeaves.PAPNO = Convert.ToInt32(ViewState["PAPNO"].ToString());
                            DataSet ds = objCommon.FillDropDown("payroll_OD_passing_authority_path", "PAPNO", "IDNO", "IDNO=" + objLeaves.EMPNO + " AND PAPNO!=" + objLeaves.PAPNO + " ", "");
                            if (ds.Tables[0].Rows.Count <= 0)
                            {
                                count_record = count_record + 1;
                                DataRow dr = dtEmpRecord.NewRow();
                                dr["IDNO"] = objLeaves.EMPNO;
                                dtEmpRecord.Rows.Add(dr);
                                dtEmpRecord.AcceptChanges();
                            }
                        }

                    }
                }
                if (count == 0)
                {
                    MessageBox("Please Select Atleast one employee");
                    pnlbtn.Visible = true;
                    return;
                }
                else if (count > 0 && count_record == 0)
                {
                    MessageBox("Sorry! Record Already exists");
                    pnlbtn.Visible = true;
                    return;
                }
            }
            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    // CustomStatus cs=(CustomStatus) objPAPath.AddPAPath(objLeaves);
                    CustomStatus cs = (CustomStatus)objPAPath.AddODPAPath(objLeaves, dtEmpRecord);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        MessageBox("Record Saved Successfully");
                        pnlAdd.Visible = false; pnlEmpList.Visible = false;
                        pnlList.Visible = true;
                        btnAdd.Visible = true;
                        pnlPAPaList.Visible = true;
                        ViewState["action"] = null;
                        Clear();
                    }
                }
                else
                {
                    if (ViewState["PAPNO"] != null)
                    {
                        objLeaves.PAPNO = Convert.ToInt32(ViewState["PAPNO"].ToString());
                        // CustomStatus CS = (CustomStatus)objPAPath.UpdatePAPath(objLeaves);
                        CustomStatus CS = (CustomStatus)objPAPath.UpdateODPAPath(objLeaves, dtEmpRecord);
                        if (CS.Equals(CustomStatus.RecordUpdated))
                        {
                            MessageBox("Record Updated Successfully");
                            pnlAdd.Visible = false; pnlEmpList.Visible = false;
                            pnlList.Visible = true;
                            btnAdd.Visible = true;
                            pnlPAPaList.Visible = true;
                            ViewState["action"] = null;
                            Clear();
                        }
                    }
                }
            }
            BindListViewPAPath();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_PA_Path.btnSave_Click ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    //protected void btnSaveOLD_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        OD objLeaves = new OD();
    //        objLeaves.DEPTNO = Convert.ToInt32(ddlDept.SelectedValue);
    //        objLeaves.PAN01 = Convert.ToInt32(ddlPA01.SelectedValue);
    //        objLeaves.PAN02 = Convert.ToInt32(ddlPA02.SelectedValue);
    //        objLeaves.PAN03 = Convert.ToInt32(ddlPA03.SelectedValue);
    //        objLeaves.PAN04 = Convert.ToInt32(ddlPA04.SelectedValue);
    //        objLeaves.PAN05 = Convert.ToInt32(ddlPA05.SelectedValue);
    //        objLeaves.PAPATH = Convert.ToString(txtPAPath.Text);
    //        objLeaves.COLLEGE_NO = Convert.ToInt32(ddlCollege.SelectedValue);
    //       // objLeaves.COLLEGE_CODE = Convert.ToString(Session["colcode"]);
    //        if (ViewState["action"] != null)
    //        {
    //            if (ViewState["action"].ToString().Equals("add"))
    //            {
    //                CustomStatus cs = (CustomStatus)objPAPath.AddODPAPath(objLeaves);
    //                if (cs.Equals(CustomStatus.RecordSaved))
    //                {
    //                    MessageBox("Record Saved Successfully");
    //                    pnlAdd.Visible = false; pnlEmpList.Visible = false;
    //                    pnlList.Visible = true;
    //                    ViewState["action"] = null;
    //                    Clear();

    //                    btnAdd.Visible = true;
    //                    btnShowReport.Visible = true;
    //                    btnSave.Visible = false;
    //                    btnCancel.Visible = false;
    //                    btnBack.Visible = false;
    //                }
    //            }
    //            else
    //            {
    //                if (ViewState["PAPNO"] != null)
    //                {
    //                    objLeaves.PAPNO = Convert.ToInt32(ViewState["PAPNO"].ToString());
    //                    CustomStatus CS = (CustomStatus)objPAPath.UpdateODPAPath(objLeaves);
    //                    if (CS.Equals(CustomStatus.RecordUpdated))
    //                    {
    //                        MessageBox("Record Updated Successfully");
    //                        pnlAdd.Visible = false; pnlEmpList.Visible = false;
    //                        pnlList.Visible = true;
    //                        ViewState["action"] = null;
    //                        Clear();

    //                        btnAdd.Visible = true;
    //                        btnShowReport.Visible = true;
    //                        btnSave.Visible = false;
    //                        btnCancel.Visible = false;
    //                        btnBack.Visible = false;
    //                    }
    //                }
    //            }
    //        }
    //        BindListViewPAPath();
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_PA_Path.btnSave_Click ->" + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server UnAvailable");
    //    }
    //}

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int PAPNO = int.Parse(btnEdit.CommandArgument);
            ShowDetails(PAPNO);

            ViewState["action"] = "edit";
            pnlAdd.Visible = true;
            pnlList.Visible = false;

            btnAdd.Visible = false;
            btnShowReport.Visible = false;
            btnSave.Visible = true;
            btnCancel.Visible = true;
            btnBack.Visible = true;
            pnlPAPaList.Visible = false;

            pnlbtn.Visible = true;
            btnSave.Visible = true;
            btnCancel.Visible = true;
            btnBack.Visible = true;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_PA_Path.btnEdit_Click ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnDelete = sender as ImageButton;
            int PAPNO = int.Parse(btnDelete.CommandArgument);
            CustomStatus cs = (CustomStatus)objPAPath.DeleteODPAPath(PAPNO);
            if (cs.Equals(CustomStatus.RecordDeleted))
            {
                MessageBox("Record Deleted Successfully");
                ViewState["action"] = null;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_PA_Path.btnDelete_Click ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);

    }
    private void ShowDetails(Int32 PAPNO)
    {
        DataSet ds = null;
        try
        {
            ds = objPAPath.GetSingleODPAPath(PAPNO);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["PAPNO"] = PAPNO;
                //ddlLeavename.SelectedValue = ds.Tables[0].Rows[0]["LNO"].ToString();

                ddlCollege.SelectedValue = ds.Tables[0].Rows[0]["COLLEGE_NO"].ToString();
                FillDepartment();
                ddlDept.SelectedValue = ds.Tables[0].Rows[0]["DEPTNO"].ToString();

                //===========
                FillPAuthority();
                FillEmployee();



                //foreach (ListViewItem lvitem in lvEmployees.Items)
                //{
                //    CheckBox chkIdNo = lvitem.FindControl("chkIdNo") as CheckBox;

                //    if (Convert.ToInt32(chkIdNo.ToolTip) == Convert.ToInt32(ds.Tables[0].Rows[0]["IDNO"].ToString()))
                //    {
                //        chkIdNo.Checked = true;
                //        break;
                //    }

                //}
                // ddlEmployee.SelectedValue = ds.Tables[0].Rows[0]["IDNO"].ToString();
                lblEmpName.Text = ds.Tables[0].Rows[0]["NAME"].ToString();
                ViewState["IDNO"] = ds.Tables[0].Rows[0]["IDNO"].ToString();
                trEmp.Visible = true;
                //===============



                txtPAPath.Text = ds.Tables[0].Rows[0]["PAPATH"].ToString();
                ddlPA01.SelectedValue = ds.Tables[0].Rows[0]["PAN01"].ToString();
                this.EnableDisable(1);

                if (!(ds.Tables[0].Rows[0]["PAN02"].ToString().Trim().Equals("0")))
                {
                    ddlPA02.SelectedValue = ds.Tables[0].Rows[0]["PAN02"].ToString();
                    this.EnableDisable(2);
                    ddlPA02.Enabled = true;


                }
                if (!(ds.Tables[0].Rows[0]["PAN03"].ToString().Trim().Equals("0")))
                {
                    ddlPA03.SelectedValue = ds.Tables[0].Rows[0]["PAN03"].ToString();
                    this.EnableDisable(3);
                    ddlPA03.Enabled = true;


                }
                if (!(ds.Tables[0].Rows[0]["PAN04"].ToString().Trim().Equals("0")))
                {
                    ddlPA04.SelectedValue = ds.Tables[0].Rows[0]["PAN04"].ToString();
                    this.EnableDisable(4);
                    ddlPA04.Enabled = true;

                }
                if (!(ds.Tables[0].Rows[0]["PAN05"].ToString().Trim().Equals("0")))
                {
                    ddlPA05.SelectedValue = ds.Tables[0].Rows[0]["PAN05"].ToString();
                    this.EnableDisable(5);
                    ddlPA05.Enabled = true;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_PA_Path.ShowDetails ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void ShowDetailsold(Int32 PAPNO)
    {
        DataSet ds = null;
        try
        {
            ds = objPAPath.GetSingleODPAPath(PAPNO);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["PAPNO"] = PAPNO;
                ddlCollege.SelectedValue = ds.Tables[0].Rows[0]["COLLEGE_NO"].ToString();
                FillDepartment();
                ddlDept.SelectedValue = ds.Tables[0].Rows[0]["DEPTNO"].ToString();
                txtPAPath.Text = ds.Tables[0].Rows[0]["PAPATH"].ToString();
                FillPAuthority(); //Added on 09-12-2016 By Saket Singh
                ddlPA01.SelectedValue = ds.Tables[0].Rows[0]["PAN01"].ToString();
                this.EnableDisable(1);

                if (!(ds.Tables[0].Rows[0]["PAN02"].ToString().Trim().Equals("0")))
                {
                    ddlPA02.SelectedValue = ds.Tables[0].Rows[0]["PAN02"].ToString();
                    this.EnableDisable(2);
                    ddlPA02.Enabled = true;


                }
                if (!(ds.Tables[0].Rows[0]["PAN03"].ToString().Trim().Equals("0")))
                {
                    ddlPA03.SelectedValue = ds.Tables[0].Rows[0]["PAN03"].ToString();
                    this.EnableDisable(3);
                    ddlPA03.Enabled = true;


                }
                if (!(ds.Tables[0].Rows[0]["PAN04"].ToString().Trim().Equals("0")))
                {
                    ddlPA04.SelectedValue = ds.Tables[0].Rows[0]["PAN04"].ToString();
                    this.EnableDisable(4);
                    ddlPA04.Enabled = true;

                }
                if (!(ds.Tables[0].Rows[0]["PAN05"].ToString().Trim().Equals("0")))
                {
                    ddlPA05.SelectedValue = ds.Tables[0].Rows[0]["PAN05"].ToString();
                    this.EnableDisable(5);
                    ddlPA05.Enabled = true;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_PA_Path.ShowDetails ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        //FillDepartment();
        FillPAuthority();
    }
    protected void ddlPA01_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.EnableDisable(1);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_PA_Path.ddlPA01_click ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }


    }

    protected void ddlPA02_SelectedIndexChanged1(object sender, EventArgs e)
    {
        try
        {
            this.EnableDisable(2);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_PA_Path.ddlPA02_SelectedIndexChanged ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlPA03_SelectedIndexChanged1(object sender, EventArgs e)
    {
        try
        {
            this.EnableDisable(3);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_PA_Path.ddlPA03_SelectedIndexChanged ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    protected void ddlPA04_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.EnableDisable(4);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_PA_Path.ddlPA04_SelectedIndexChanged ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    private void EnableDisable(int index)
    {

        switch (index)
        {
            case 1:
                if (ddlPA01.SelectedIndex == 0)
                {
                    ddlPA02.SelectedIndex = 0;
                    ddlPA02.Enabled = false;
                    ddlPA03.SelectedIndex = 0;
                    ddlPA03.Enabled = false;
                    ddlPA04.SelectedIndex = 0;
                    ddlPA04.Enabled = false;
                    ddlPA05.SelectedIndex = 0;
                    ddlPA05.Enabled = false;
                    //string swhere = " COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + " and  PANO NOT IN (" + ddlPA01.SelectedValue + ")";
                    string swhere ="PANO NOT IN (" + ddlPA01.SelectedValue + ")";
                    objCommon.FillDropDownList(ddlPA02, "PAYROLL_LEAVE_PASSING_AUTHORITY", "PANO", "PANAME", swhere, "PANAME");

                  
                }
                else
                {

                    ddlPA02.Enabled = true;
                   
                    //string swhere = "COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + " and PANO NOT IN (" + ddlPA01.SelectedValue + ")";
                    string swhere ="PANO NOT IN (" + ddlPA01.SelectedValue + ")";
                    objCommon.FillDropDownList(ddlPA02, "PAYROLL_LEAVE_PASSING_AUTHORITY", "PANO", "PANAME", swhere, "PANAME");
                    ddlPA03.SelectedIndex = 0;
                    ddlPA03.Enabled = false;
                    ddlPA04.SelectedIndex = 0;
                    ddlPA04.Enabled = false;
                    ddlPA05.SelectedIndex = 0;
                    ddlPA05.Enabled = false;
                    txtPAPath.Text = ddlPA01.SelectedItem.ToString();
                }

                break;

            case 2:
                if (ddlPA02.SelectedIndex == 0)
                {
                    ddlPA03.SelectedIndex = 0;
                    ddlPA03.Enabled = false;
                    ddlPA04.SelectedIndex = 0;
                    ddlPA04.Enabled = false;
                    ddlPA05.SelectedIndex = 0;
                    ddlPA05.Enabled = false;
                    //string swhere = " COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + " and  PANO NOT IN (" + ddlPA01.SelectedValue + "," + ddlPA02.SelectedValue + ")";
                    string swhere ="PANO NOT IN (" + ddlPA01.SelectedValue + "," + ddlPA02.SelectedValue + ")";
                    objCommon.FillDropDownList(ddlPA03, "PAYROLL_LEAVE_PASSING_AUTHORITY", "PANO", "PANAME", swhere, "PANAME");

                }
                else
                {
                    ddlPA03.Enabled = true;
                    //string swhere = " COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + " and PANO NOT IN (" + ddlPA01.SelectedValue + "," + ddlPA02.SelectedValue + ")";
                    string swhere ="PANO NOT IN (" + ddlPA01.SelectedValue + "," + ddlPA02.SelectedValue + ")";
                    objCommon.FillDropDownList(ddlPA03, "PAYROLL_LEAVE_PASSING_AUTHORITY", "PANO", "PANAME", swhere, "PANAME");
                    ddlPA04.SelectedIndex = 0;
                    ddlPA04.Enabled = false;
                    ddlPA05.SelectedIndex = 0;
                    ddlPA05.Enabled = false;

                    txtPAPath.Text = ddlPA01.SelectedItem.ToString() + "->" + ddlPA02.SelectedItem.ToString();
                }
                break;
            case 3:


                if (ddlPA03.SelectedIndex == 0)
                {
                    ddlPA04.SelectedIndex = 0;
                    ddlPA04.Enabled = false;
                    ddlPA05.SelectedIndex = 0;
                    ddlPA05.Enabled = false;

                    //string swhere = " COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + " and PANO NOT IN (" + ddlPA01.SelectedValue + "," + ddlPA02.SelectedValue + "," + ddlPA03.SelectedValue + ")";
                    string swhere ="PANO NOT IN (" + ddlPA01.SelectedValue + "," + ddlPA02.SelectedValue + "," + ddlPA03.SelectedValue + ")";
                    objCommon.FillDropDownList(ddlPA04, "PAYROLL_LEAVE_PASSING_AUTHORITY", "PANO", "PANAME", swhere, "PANAME");
                }
                else
                {
                    ddlPA04.Enabled = true;
                    //string swhere = " PANO NOT IN (" + ddlPA01.SelectedValue + "," + ddlPA02.SelectedValue + "," + ddlPA03.SelectedValue + ")";
                    string swhere = "PANO NOT IN (" + ddlPA01.SelectedValue + "," + ddlPA02.SelectedValue + "," + ddlPA03.SelectedValue + ")";
                    objCommon.FillDropDownList(ddlPA04, "PAYROLL_LEAVE_PASSING_AUTHORITY", "PANO", "PANAME", swhere, "PANAME");
                    ddlPA05.SelectedIndex = 0;
                    ddlPA05.Enabled = false;
                    txtPAPath.Text = ddlPA01.SelectedItem.ToString() + "->" + ddlPA02.SelectedItem.ToString() + "->" + ddlPA03.SelectedItem.ToString();
                }

                break;
            case 4:

                if (ddlPA04.SelectedIndex == 0)
                {
                    ddlPA05.SelectedIndex = 0;
                    ddlPA05.Enabled = false;

                    //string swhere = " COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + " and  PANO NOT IN (" + ddlPA01.SelectedValue + "," + ddlPA02.SelectedValue + "," + ddlPA03.SelectedValue + "," + ddlPA04.SelectedValue + ")";
                    string swhere ="PANO NOT IN (" + ddlPA01.SelectedValue + "," + ddlPA02.SelectedValue + "," + ddlPA03.SelectedValue + "," + ddlPA04.SelectedValue + ")";
                    objCommon.FillDropDownList(ddlPA05, "PAYROLL_LEAVE_PASSING_AUTHORITY", "PANO", "PANAME", swhere, "PANAME");
                }
                else
                {
                    ddlPA05.Enabled = true;
                    //string swhere = " COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + " and PANO NOT IN (" + ddlPA01.SelectedValue + "," + ddlPA02.SelectedValue + "," + ddlPA03.SelectedValue + "," + ddlPA04.SelectedValue + ")";
                    string swhere ="PANO NOT IN (" + ddlPA01.SelectedValue + "," + ddlPA02.SelectedValue + "," + ddlPA03.SelectedValue + "," + ddlPA04.SelectedValue + ")";
                    objCommon.FillDropDownList(ddlPA05, "PAYROLL_LEAVE_PASSING_AUTHORITY", "PANO", "PANAME", swhere, "PANAME");
                    txtPAPath.Text = ddlPA01.SelectedItem.ToString() + "->" + ddlPA02.SelectedItem.ToString() + "->" + ddlPA03.SelectedItem.ToString() + "->" + ddlPA04.SelectedItem.ToString();
                }

                break;
            case 5:
                if (!(ddlPA04.SelectedIndex == 0))
                {
                    txtPAPath.Text = ddlPA01.SelectedItem.ToString() + "->" + ddlPA02.SelectedItem.ToString() + "->" + ddlPA03.SelectedItem.ToString() + "->" + ddlPA04.SelectedItem.ToString() + "->" + ddlPA05.SelectedItem.ToString();
                }
                break;

        }

    }


    protected void ddlPA05_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.EnableDisable(5);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_PA_Path.ddlPA05_SelectedIndexChanged ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    
    protected void btnShowReport_Click(object sender, EventArgs e)
    {

        ShowReport("LeaveType", "ESTB_ODPAPAth.rpt");

    }

    //added function to display the report 18 nov 2014
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            //GetStudentIDs();
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("ESTABLISHMENT")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ESTABLISHMENT,LEAVES," + rptFileName;

            //            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@P_ORDTRNO=" + Convert.ToInt32(ddlOrder.SelectedValue) + "," + "@username=" + Session["userfullname"].ToString();
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString();
           // url += "&param=@username=" + Session["username"].ToString() + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString()+",@P_COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) ;
            string collegeno = Session["college_nos"].ToString();
            string[] values = collegeno.Split(',');
            if (values.Length > 1)
            {
                url += "&param=@username=" + Session["username"].ToString() + ",@P_COLLEGE_CODE=0,@P_COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue);
            }
            else
            {
                url += "&param=@username=" + Session["username"].ToString() + ",@P_COLLEGE_CODE=" + Session["college_nos"].ToString() + ",@P_COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue);
            }
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@P_COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + "," + "@username=" + Session["username"].ToString();
         
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Comparative.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillPAuthority();
        FillEmployee();
        //  objCommon.FillDropDownList(ddlEmployee, "PAYROLL_EMPMAS", "IDNO", "FNAME+' '+MNAME+' '+LNAME AS NAME", "SUBDEPTNO=" + Convert.ToInt32(ddlDept.SelectedValue) + "", "");
        pnlEmpList.Visible = true;
        lvEmployees.Visible = true;
        trEmp.Visible = false;
        txtPAPath.Text = string.Empty;
        pnlEmpList.Visible = true;
    }
    private void FillEmployee()
    {
        DataSet dsEmp = objCommon.FillDropDown("PAYROLL_EMPMAS E INNER JOIN PAYROLL_PAYMAS P ON(E.IDNO = P.IDNO)", "E.IDNO", "isnull(TITLE,'')+' '+isnull(FNAME,'')+' '+isnull(MNAME,'')+' '+isnull(LNAME,'') AS NAME", "E.COLLEGE_NO =" + Convert.ToInt32(ddlCollege.SelectedValue) + " and E.SUBDEPTNO=" + Convert.ToInt32(ddlDept.SelectedValue) + "  AND P.PSTATUS='Y'", "NAME");
        if (dsEmp.Tables[0].Rows.Count > 0)
        {
            lvEmployees.DataSource = dsEmp;
            lvEmployees.DataBind();
            //pnlEmpList.Visible = true;
            //lvEmployees.Visible = true;
        }
        else
        {
            lvEmployees.DataSource = null;
            lvEmployees.DataBind();
        }

        //  objCommon.FillDropDownList(ddlEmployee, "PAYROLL_EMPMAS", "IDNO", "FNAME+' '+MNAME+' '+LNAME AS NAME", "SUBDEPTNO=" + Convert.ToInt32(ddlDept.SelectedValue) + " AND SCHOOL_NO=" + Convert.ToInt32(ddlSchool.SelectedValue) + " ", "");

    }
}
