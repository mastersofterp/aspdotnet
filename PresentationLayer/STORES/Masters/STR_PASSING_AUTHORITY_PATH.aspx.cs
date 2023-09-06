using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;


public partial class STORES_Masters_STR_PASSING_AUTHORITY_PATH : System.Web.UI.Page
{
    //Creating objects of Class Files Common,UAIMS_COMMON,LeaveController
    Common objCommon = new Common();

    UAIMS_Common objUCommon = new UAIMS_Common();
    StoreMaster objLeaves = new StoreMaster();

    StoreMasterController objPAPath = new StoreMasterController();

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
                Session["usertype"] == null || Session["userfullname"] == null || Session["strdeptname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                CheckPageAuthorization();
                Page.Title = Session["coll_name"].ToString();

                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }

                ViewState["StoreUser"] = null;
                this.CheckMainStoreUser();
                pnlAdd.Visible = false;
                pnlList.Visible = true;
                FillLeave();
                FillDepartment();
                FillPAuthority();
                //  BindListViewPAPath();
                //Set Report Parameters 
                //objCommon.ReportPopUp(btnShowReport, "pagetitle=UAIMS&path=~" + "," + "Reports" + "," + "ESTABLISHMENT" + "," + "LEAVES" + "," + "ESTB_PAPAth.rpt&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@username=" + Session["userfullname"].ToString(), "UAIMS");
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
                Response.Redirect("~/notauthorized.aspx?page=create_user.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=create_user.aspx");
        }
    }

    //Check for Main Store User.
    private bool CheckMainStoreUser()
    {
        string test = Application["strrefmaindept"].ToString();
        string test1 = Session["strdeptcode"].ToString();

        if (Session["strdeptcode"].ToString() == Application["strrefmaindept"].ToString())
        {
            ViewState["StoreUser"] = "MainStoreUser";
            return true;
        }
        else
        {
            this.CheckDeptStoreUser();
            return false;
        }
    }

    //Check for Department Store User.
    private bool CheckDeptStoreUser()
    {
        string test = objCommon.LookUp("STORE_DEPARTMENTUSER", "APLNO", "UA_NO=" + Convert.ToInt32(Session["userno"]));

        // When department user is having approval level as Department Store means "4". It is fixed in Store Reference table.
        string deptStoreUser = objCommon.LookUp("STORE_REFERENCE", "DEPT_STORE_USER", "");

        if (test == deptStoreUser)
        {
            ViewState["StoreUser"] = "DeptStoreUser";
            return true;
        }
        else
        {
            ViewState["StoreUser"] = "NormalUser";
            return false;

        }
    }

    protected void BindListViewPAPath(int MDNO, char PATH_FOR)
    {
        try
        {
            DataSet ds = objPAPath.GetAllPAPath(MDNO, PATH_FOR);
            if (ds.Tables[0].Rows.Count <= 0)
            {

                // dpPager.Visible = false;
            }
            else
            {

                // dpPager.Visible = true;
            }
            lvPAPath.DataSource = ds;
            lvPAPath.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "STORES_Masters_STR_PASSING_AUTHORITY_PATH.BindListViewPAPath ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }
    }

    //protected void dpPager_PreRender(object sender, EventArgs e)
    //{
    //Bind the ListView with Domain  
    // BindListViewPAPath();
    //

    private void Clear()
    {
        //  ddlstage.SelectedIndex = 0;
        if (ViewState["action"] != null)
        {
            if (ViewState["action"].ToString().Equals("edit"))
            {
            }
            else
            {
                ddlDept.SelectedIndex = 0;
                ddlDept.Enabled = true;
            }
        }
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

        lvEmployees.DataSource = null;
        lvEmployees.DataBind();
        pnlEmpList.Visible = false;
        rdbPathFor.SelectedValue = "P";
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Clear();
        ddlDept.SelectedIndex = 0;
        ddlDept.Enabled = true;
        pnlAdd.Visible = true;
        pnlList.Visible = false;
        ddlPA02.Enabled = false;
        ddlPA03.Enabled = false;
        ddlPA04.Enabled = false;
        ddlPA05.Enabled = false;
        ViewState["action"] = "add";
        trEmp.Visible = false;
    }

    private void FillLeave()
    {
        try
        {
            objCommon.FillDropDownList(ddlstage, "STORE_STAGE", "STNO", "SNAME", "", "STNO");
            ddlstage.SelectedValue = "1";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "STORES_Masters_STR_PASSING_AUTHORITY_PATH.FillLeave ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void FillDepartment()
    {
        try
        {

            //objCommon.FillDropDownList(ddlDept, "PAYROLL_SUBDEPT", "SUBDEPTNO", "SUBDEPT", "", "SUBDEPT");
            //main store user
            if (ViewState["StoreUser"].ToString() == "MainStoreUser")
            {
                objCommon.FillDropDownList(ddlDept, "STORE_DEPARTMENT", "MDNO", "MDNAME", "", "MDNAME");  // MDNO=1
                BindListViewPAPath(0, Convert.ToChar(rdbListPathFor.SelectedValue));
            }
            else if (ViewState["StoreUser"].ToString() == "DeptStoreUser")
            {
                // Departmental user                
                objCommon.FillDropDownList(ddlDept, "STORE_DEPARTMENT", "MDNO", "MDNAME", "MDNO=" + Convert.ToInt32(Session["strdeptcode"]), "MDNAME");
                BindListViewPAPath(Convert.ToInt32(Session["strdeptcode"]), Convert.ToChar(rdbListPathFor.SelectedValue));
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "STORES_Masters_STR_PASSING_AUTHORITY_PATH.Fill_Department ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void FillPAuthority()
    {
        try
        {
            objCommon.FillDropDownList(ddlPA01, "STORE_PASSING_AUTHORITY", "PANO", "PANAME", "ISNULL(IS_SPECIAL,0)=0", "PANAME");
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
        Clear();
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Clear();
        pnlAdd.Visible = false;
        pnlList.Visible = true;
    }

    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            // Leaves objLeaves = new Leaves();
            DataTable dtEmpRecord = new DataTable();
            dtEmpRecord.Columns.Add("IDNO");

            objLeaves.LNO = Convert.ToInt32(ddlstage.SelectedValue);
            objLeaves.DEPTNO = Convert.ToInt32(ddlDept.SelectedValue);
            objLeaves.PAN01 = Convert.ToInt32(ddlPA01.SelectedValue);
            objLeaves.PAN02 = Convert.ToInt32(ddlPA02.SelectedValue);
            objLeaves.PAN03 = Convert.ToInt32(ddlPA03.SelectedValue);
            objLeaves.PAN04 = Convert.ToInt32(ddlPA04.SelectedValue);
            objLeaves.PAN05 = Convert.ToInt32(ddlPA05.SelectedValue);
            objLeaves.PAPATH = Convert.ToString(txtPAPath.Text);
            objLeaves.COLLEGE_CODE = Convert.ToString(Session["colcode"]);

            objLeaves.PATH_FOR = Convert.ToChar(rdbPathFor.SelectedValue);
            int count = 0; int count_record = 0;



            // int duplicateCkeck = Convert.ToInt32(objCommon.LookUp("STORE_PASSING_AUTHORITY_PATH", " count(*)", "DEPTNO=" + Convert.ToInt32(ddlDept.SelectedValue)));

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
                            DataSet ds = objCommon.FillDropDown("STORE_PASSING_AUTHORITY_PATH", "PAPNO", "EMP_IDNO", "ISNULL(PATH_FOR,'P')='" + Convert.ToChar(rdbPathFor.SelectedValue) + "' AND EMP_IDNO=" + objLeaves.EMPNO + " ", "");
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
                            DataSet ds = objCommon.FillDropDown("STORE_PASSING_AUTHORITY_PATH", "PAPNO", "EMP_IDNO", "ISNULL(PATH_FOR,'P')='" + Convert.ToChar(rdbPathFor.SelectedValue) + "' AND EMP_IDNO=" + objLeaves.EMPNO + " AND PAPNO!=" + objLeaves.PAPNO + " ", "");
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
                    return;
                }
                else if (count > 0 && count_record == 0)
                {
                    MessageBox("This Employee Already exists.");
                    return;
                }
            }


            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    //if (duplicateCkeck == 0)
                    //{
                    CustomStatus cs = (CustomStatus)objPAPath.AddPAPath(objLeaves, dtEmpRecord);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        objCommon.DisplayMessage(pnlMessage, "Record Saved Successfully", this);
                        if (ViewState["StoreUser"].ToString() == "MainStoreUser")
                        {
                            BindListViewPAPath(0, Convert.ToChar(rdbListPathFor.SelectedValue));
                        }
                        else if (ViewState["StoreUser"].ToString() == "DeptStoreUser")
                        {
                            BindListViewPAPath(Convert.ToInt32(Session["strdeptcode"]), Convert.ToChar(rdbListPathFor.SelectedValue));
                        }
                        pnlAdd.Visible = false;
                        pnlList.Visible = true;
                        ViewState["action"] = null;
                        Clear();
                    }
                    //}
                    //else
                    //{
                    //    objCommon.DisplayMessage(pnlMessage, "Passing Path For This Department Already Exists", this);
                    //    return;
                    //}
                }
                else
                {
                    if (ViewState["PAPNO"] != null)
                    {
                        objLeaves.PAPNO = Convert.ToInt32(ViewState["PAPNO"].ToString());
                        CustomStatus CS = (CustomStatus)objPAPath.UpdatePAPath(objLeaves, dtEmpRecord);
                        if (CS.Equals(CustomStatus.RecordUpdated))
                        {
                            objCommon.DisplayMessage(pnlMessage, "Record Updated Successfully", this);
                            if (ViewState["StoreUser"].ToString() == "MainStoreUser")
                            {
                                BindListViewPAPath(0, Convert.ToChar(rdbListPathFor.SelectedValue));
                            }
                            else if (ViewState["StoreUser"].ToString() == "DeptStoreUser")
                            {
                                BindListViewPAPath(Convert.ToInt32(Session["strdeptcode"]), Convert.ToChar(rdbListPathFor.SelectedValue));
                            }
                            pnlAdd.Visible = false;
                            pnlList.Visible = true;
                            ViewState["action"] = null;
                            Clear();
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_PA_Path.btnSave_Click ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int PAPNO = int.Parse(btnEdit.CommandArgument);
            int  Count = Convert.ToInt32(objCommon.LookUp("STORE_REQ_MAIN", "Count(*)", "UANO IN (SELECT EMP_IDNO FROM STORE_PASSING_AUTHORITY_PATH WHERE PAPNO = " + PAPNO + ")"));
            if (Count > 0)
            {
                MessageBox("Can Not Modify! Passing Path Is Already In Use.");
                return;
            }
            ShowDetails(PAPNO);

            ViewState["action"] = "edit";
            pnlAdd.Visible = true;
            pnlList.Visible = false;
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

            int Count = Convert.ToInt32(objCommon.LookUp("STORE_REQ_MAIN", "Count(*)", "UANO IN (SELECT EMP_IDNO FROM STORE_PASSING_AUTHORITY_PATH WHERE PAPNO = " + PAPNO + ")"));
            if (Count > 0)
            {
                MessageBox("Can Not Delete! Passing Path Is Already In Use.");
                return;
            }

            CustomStatus cs = (CustomStatus)objPAPath.DeletePAPath(PAPNO);
            if (cs.Equals(CustomStatus.RecordDeleted))
            {
                MessageBox("Record Deleted Successfully.");
                if (ViewState["StoreUser"].ToString() == "MainStoreUser")
                {
                    BindListViewPAPath(0, Convert.ToChar(rdbListPathFor.SelectedValue));
                }
                else if (ViewState["StoreUser"].ToString() == "DeptStoreUser")
                {
                    BindListViewPAPath(Convert.ToInt32(Session["strdeptcode"]), Convert.ToChar(rdbListPathFor.SelectedValue));
                }
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

    private void ShowDetails(Int32 PAPNO)
    {
        DataSet ds = null;
        try
        {
            ds = objPAPath.GetSinglePAPath(PAPNO);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["PAPNO"] = PAPNO;
                ddlstage.SelectedValue = ds.Tables[0].Rows[0]["STNO"].ToString();
                ddlDept.SelectedValue = ds.Tables[0].Rows[0]["DEPTNO"].ToString();
                ddlDept.Enabled = false;

                txtPAPath.Text = ds.Tables[0].Rows[0]["PAPATH"].ToString();
                ddlPA01.SelectedValue = ds.Tables[0].Rows[0]["PAN01"].ToString();
                this.EnableDisable(1);
                lblEmpName.Text = ds.Tables[0].Rows[0]["NAME"].ToString();
                ViewState["IDNO"] = ds.Tables[0].Rows[0]["IDNO"].ToString();
                trEmp.Visible = true;
                rdbPathFor.SelectedValue = ds.Tables[0].Rows[0]["PATH_FOR"].ToString();

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
                    string swhere = " ISNULL(IS_SPECIAL,0)=0 AND PANO NOT IN (" + ddlPA01.SelectedValue + ")";
                    objCommon.FillDropDownList(ddlPA02, "STORE_PASSING_AUTHORITY", "PANO", "PANAME", swhere, "PANAME");
                    txtPAPath.Text = ddlPA01.SelectedItem.ToString();
                }
                else
                {

                    ddlPA02.Enabled = true;
                    string swhere = " ISNULL(IS_SPECIAL,0)=0 AND PANO NOT IN (" + ddlPA01.SelectedValue + ")";
                    objCommon.FillDropDownList(ddlPA02, "STORE_PASSING_AUTHORITY", "PANO", "PANAME", swhere, "PANAME");
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
                    string swhere = " ISNULL(IS_SPECIAL,0)=0 AND PANO NOT IN (" + ddlPA01.SelectedValue + "," + ddlPA02.SelectedValue + ")";
                    objCommon.FillDropDownList(ddlPA03, "STORE_PASSING_AUTHORITY", "PANO", "PANAME", swhere, "PANAME");
                    txtPAPath.Text = ddlPA01.SelectedItem.ToString();// +"->" + ddlPA02.SelectedItem.ToString();
                }
                else
                {
                    ddlPA03.Enabled = true;
                    string swhere = " ISNULL(IS_SPECIAL,0)=0 AND PANO NOT IN (" + ddlPA01.SelectedValue + "," + ddlPA02.SelectedValue + ")";
                    objCommon.FillDropDownList(ddlPA03, "STORE_PASSING_AUTHORITY", "PANO", "PANAME", swhere, "PANAME");
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

                    string swhere = " ISNULL(IS_SPECIAL,0)=0 AND PANO NOT IN (" + ddlPA01.SelectedValue + "," + ddlPA02.SelectedValue + "," + ddlPA03.SelectedValue + ")";
                    objCommon.FillDropDownList(ddlPA04, "STORE_PASSING_AUTHORITY", "PANO", "PANAME", swhere, "PANAME");
                    txtPAPath.Text = ddlPA01.SelectedItem.ToString() + "->" + ddlPA02.SelectedItem.ToString();// +"->" + ddlPA03.SelectedItem.ToString();
                }
                else
                {
                    ddlPA04.Enabled = true;
                    string swhere = " ISNULL(IS_SPECIAL,0)=0 AND PANO NOT IN (" + ddlPA01.SelectedValue + "," + ddlPA02.SelectedValue + "," + ddlPA03.SelectedValue + ")";
                    objCommon.FillDropDownList(ddlPA04, "STORE_PASSING_AUTHORITY", "PANO", "PANAME", swhere, "PANAME");
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

                    string swhere = " ISNULL(IS_SPECIAL,0)=0 AND PANO NOT IN (" + ddlPA01.SelectedValue + "," + ddlPA02.SelectedValue + "," + ddlPA03.SelectedValue + "," + ddlPA04.SelectedValue + ")";
                    objCommon.FillDropDownList(ddlPA05, "STORE_PASSING_AUTHORITY", "PANO", "PANAME", swhere, "PANAME");
                    txtPAPath.Text = ddlPA01.SelectedItem.ToString() + "->" + ddlPA02.SelectedItem.ToString() + "->" + ddlPA03.SelectedItem.ToString();// +"->" + ddlPA04.SelectedItem.ToString();
                }
                else
                {
                    ddlPA05.Enabled = true;
                    string swhere = " ISNULL(IS_SPECIAL,0)=0 AND PANO NOT IN (" + ddlPA01.SelectedValue + "," + ddlPA02.SelectedValue + "," + ddlPA03.SelectedValue + "," + ddlPA04.SelectedValue + ")";
                    objCommon.FillDropDownList(ddlPA05, "STORE_PASSING_AUTHORITY", "PANO", "PANAME", swhere, "PANAME");
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


    protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            FillEmployee();
            pnlEmpList.Visible = true;
            lvEmployees.Visible = true;

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "STORES_Masters_STR_PASSING_AUTHORITY_PATH.ddlPA05_SelectedIndexChanged ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void FillEmployee()
    {

        // DataSet dsEmp = objCommon.FillDropDown("PAYROLL_EMPMAS E INNER JOIN PAYROLL_SUBDEPT SD ON (E.SUBDEPTNO = SD.SUBDEPTNO) INNER JOIN STORE_SUBDEPARTMENT SSD ON (SD.SUBDEPTNO = SSD.PAYROLL_SUBDEPTNO)  INNER JOIN STORE_DEPARTMENT D ON (SSD.MDNO = D.MDNO)", "IDNO,ISNULL(PFILENO,'') AS PFILENO", "isnull(FNAME,'')+' '+isnull(MNAME,'')+' '+isnull(LNAME,'') AS NAME", "D.MDNO=" + Convert.ToInt32(ddlDept.SelectedValue), "");

        DataSet dsEmp = objCommon.FillDropDown("STORE_DEPARTMENTUSER DU  INNER JOIN USER_ACC U ON (DU.UA_NO = U.UA_NO) INNER JOIN STORE_APPROVAL_LEVEL AL ON (DU.APLNO = AL.APLNO)", "DU.UA_NO AS IDNO", "U.UA_FULLNAME AS NAME, AL.APLT", "DU.MDNO=" + Convert.ToInt32(ddlDept.SelectedValue), "");
        if (dsEmp.Tables[0].Rows.Count > 0)
        {
            lvEmployees.DataSource = dsEmp;
            lvEmployees.DataBind();
            pnlEmpList.Visible = true;
        }
        else
        {
            lvEmployees.DataSource = null;
            lvEmployees.DataBind();
        }
    }

    protected void rdbListPathFor_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["StoreUser"].ToString() == "MainStoreUser")
            {
                BindListViewPAPath(0, Convert.ToChar(rdbListPathFor.SelectedValue));
            }
            else if (ViewState["StoreUser"].ToString() == "DeptStoreUser")
            {
                BindListViewPAPath(Convert.ToInt32(Session["strdeptcode"]), Convert.ToChar(rdbListPathFor.SelectedValue));
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "STORES_Masters_STR_PASSING_AUTHORITY_PATH.rdbListPathFor_SelectedIndexChanged ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
}
