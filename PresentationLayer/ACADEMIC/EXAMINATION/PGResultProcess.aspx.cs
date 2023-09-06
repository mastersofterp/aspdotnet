using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class ACADEMIC_EXAMINATION_PGResultProcess : System.Web.UI.Page
{
    MarksEntryController objMark = new MarksEntryController();
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();

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
                ViewState["ipAddress"] = GetUserIPAddress();
                PopulateDropDownList();
            }
        }
        divMsg.InnerHtml = string.Empty;
    }
    private string GetUserIPAddress()
    {
        string User_IPAddress = string.Empty;
        string User_IPAddressRange = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (string.IsNullOrEmpty(User_IPAddressRange))//without Proxy detection
        {
            User_IPAddress = Request.ServerVariables["REMOTE_ADDR"];

        }
        //else////with Proxy detection
        //{
        //    string[] splitter = { "," };
        //    string[] IP_Array = User_IPAddressRange.Split(splitter,
        //                                                  System.StringSplitOptions.None);

        //    int LatestItem = IP_Array.Length - 1;
        //    User_IPAddress = IP_Array[LatestItem - 1];

        //}
        return User_IPAddress;

    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=ResultProcess.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=ResultProcess.aspx");
        }
    }
    private void FillStudentList()
    {
        try
        {
            if (ddlScheme.SelectedIndex > 0)
            {
                if (ddlSem.SelectedIndex > 0)
                {
                  objCommon.FillDropDownList(ddlStudent, "ACD_STUDENT_RESULT SR INNER JOIN ACD_STUDENT S ON SR.IDNO=S.IDNO", "DISTINCT S.IDNO", "('['+SR.ROLL_NO+'] '+S.REGNO+' - '+S.STUDNAME) REGNO", "SR.EXAM_REGISTERED=1 AND SR.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SR.ACCEPTED=1 AND SR.SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + " AND SR.SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue), "REGNO");
                   
                  ddlStudent.Focus();
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_RESULTPROCESSING.FillStudentList() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private void PopulateDropDownList()
    {
        try
        {
            objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0", "SESSIONNO DESC");
           // objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO > 0", "DEGREENO");
            objCommon.FillDropDownList(ddlColg, "ACD_COLLEGE_MASTER C INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD ON (CD.COLLEGE_ID=C.COLLEGE_ID)", "DISTINCT (C.COLLEGE_ID)", "C.COLLEGE_NAME", "C.COLLEGE_ID > 0 AND CD.UGPGOT IN (" + Session["ua_section"] + ") AND CD.UGPGOT = 2", "C.COLLEGE_ID");
            ddlSession.Focus();

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_RESULTPROCESSING.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    #region Fill DropDownList

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlDegree.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue + " and B.College_id=" + ddlColg.SelectedValue, "A.LONGNAME");

            }
            else
            {
                ddlBranch.Items.Clear();
                ddlDegree.SelectedIndex = 0;
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_MarkEntryComparision.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
            {
                objUCommon.ShowError(Page, "Server UnAvailable");
            }
        }

    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBranch.SelectedIndex > 0)
        {
            ddlScheme.Items.Clear();
            objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME A INNER JOIN  ACD_COLLEGE_DEGREE_BRANCH B ON (A.DEGREENO=B.DEGREENO AND A.BRANCHNO=B.BRANCHNO)", "a.SCHEMENO", "a.SCHEMENAME", "B.UGPGOT=2 and a.BRANCHNO = " + ddlBranch.SelectedValue, "SCHEMENO");
            ddlScheme.Focus();
            ddlSem.Items.Clear();
            ddlSem.Items.Add(new ListItem("Please Select", "0"));
            ddlStudent.Items.Clear();
            ddlStudent.Items.Add(new ListItem("Please Select", "0"));
           
        }
        else
        {
            ddlScheme.Items.Clear();
            ddlScheme.Items.Add(new ListItem("Please Select", "0"));
            ddlSem.Items.Clear();
            ddlSem.Items.Add(new ListItem("Please Select", "0"));
            ddlStudent.Items.Clear();
            ddlStudent.Items.Add(new ListItem("Please Select", "0"));
            
        }
    }

    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlScheme.SelectedIndex > 0)
        {
            ddlSem.Items.Clear();
            objCommon.FillDropDownList(ddlSem, "ACD_STUDENT_RESULT", "DISTINCT SEMESTERNO", "DBO.FN_DESC('SEMESTER',SEMESTERNO) AS SEMESTER", "SCHEMENO = " + Convert.ToInt32(ddlScheme.SelectedValue) + " AND SESSIONNO = " + Convert.ToInt32(ddlSession.SelectedValue), "SEMESTERNO");
            ddlSem.Focus();
            ddlStudent.Items.Clear();
            ddlStudent.Items.Add(new ListItem("Please Select", "0"));

        }
        else
        {
            ddlSem.Items.Clear();
            ddlSem.Items.Add(new ListItem("Please Select", "0"));
            ddlStudent.Items.Clear();
            ddlStudent.Items.Add(new ListItem("Please Select", "0"));
           
        }
    }
    #endregion

    private void ClearControls()
    {
        ddlBranch.Items.Clear();
        ddlBranch.Items.Add("Please Select");
        ddlScheme.Items.Clear();
        ddlScheme.Items.Add("Please Select");
        ddlSem.Items.Clear();
        ddlSem.Items.Add("Please Select");
    }



    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    protected void ddlSem_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSem.SelectedIndex > 0)
        {
            this.FillStudentList(); 
        }
        else
        {
            ddlStudent.Items.Clear();
            ddlStudent.Items.Add(new ListItem("Please Select", "0"));
          
        }
    }

    private string GetIDNO()
    {
        string retIDNO = string.Empty;
        foreach (ListViewDataItem item in lvStudent.Items)
        {
            CheckBox chk = item.FindControl("chkStudent") as CheckBox;
            Label lblStudname = item.FindControl("lblStudname") as Label;
            if (chk.Checked && ddlExamType.SelectedValue == "0")
            {
                if (retIDNO.Length == 0) retIDNO = lblStudname.ToolTip.ToString();
                else
                    retIDNO += "," + lblStudname.ToolTip.ToString();
            }
            else if (chk.Checked && ddlExamType.SelectedValue == "1")
            {
                if (retIDNO.Length == 0) retIDNO = lblStudname.ToolTip.ToString();
                else
                    retIDNO += "," + lblStudname.ToolTip.ToString();
            }          
            else if (chk.Checked)
            {
                if (retIDNO.Length == 0) retIDNO = lblStudname.ToolTip.ToString();
                else
                    retIDNO += "," + lblStudname.ToolTip.ToString();
            }
        }
        if (retIDNO != "")
        {
            if (retIDNO.Substring(retIDNO.Length - 1) == ",")
                retIDNO = retIDNO.Substring(0, retIDNO.Length - 1);
        }
        if (retIDNO.Equals("")) 
        {
            ShowMessage("Please select at least one student.");
            return "";
        }  
        else return retIDNO;
    }
    protected void btnProcessResult_Click(object sender, EventArgs e)
    {
        //LOGIC
        try
        {
            MarksEntryController objProcessResult = new MarksEntryController();
            string idno = GetIDNO();
            if (idno == "")
                return;

            int Check = Convert.ToInt32(objCommon.LookUp("ACD_TRRESULT", "COUNT(LOCK)", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + "AND SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + " AND IDNO IN (" + idno + ") AND LOCK = 1"));
            if (Check == 1)
            {
                ShowMessage("Already locked You can not reprocess the result of selected student.");
                return;
            }

            int Status = objProcessResult.CheckMarksLocked(Convert.ToInt32(ddlSession.SelectedValue), idno, Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSem.SelectedValue));
            if (Status == 0)
            {
                ShowMessage("Mark entry not completed.");
            }
            else
            {
                CustomStatus cs = (CustomStatus)objProcessResult.PGProcessResultAll(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSem.SelectedValue), 0, idno, ViewState["ipAddress"].ToString(), Convert.ToInt32(ddlColg.SelectedValue));
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    ShowMessage("Result Processed Successfully!!!");
                

                }
                else
                    ShowMessage("Error in Processing Result!");
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_RESULTPROCESSING.btnProcessResult_Click() -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    private void ShowMessage(string message)
    {
        if (message != string.Empty)
        {
            divMsg.InnerHtml += "<script type='text/javascript' language='javascript'> alert('" + message + "'); </script>";
        }
    }

    //protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlSession.SelectedIndex > 0)
    //        ddlDegree.Focus();
            
    //    else
    //        ddlSession.Focus();
    //}

    protected void btnShow_Click(object sender, EventArgs e)
    {
        DataSet ds = null;
                //DataSet ds = objMark.GetresultprocessStudentData(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSem.SelectedValue),Convert.ToInt32(ddlColg.SelectedValue));
                if (ds.Tables[0].Rows.Count > 0)
                {

                    lvStudent.DataSource = ds;
                    lvStudent.DataBind();
                    btnProcessResult.Enabled = true;
                    btnLock.Enabled = true;
                }
                else
                {
                    lvStudent.DataSource = null;
                    lvStudent.DataBind();
                    btnLock.Enabled = false;
                }
        
                //objCommon.DisplayMessage("Mark Entry and Lock entry is completed", this.Page);
               
    }
    protected void ddlColg_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD ON (D.DEGREENO = CD.DEGREENO)", "DISTINCT (D.DEGREENO)", " D.DEGREENAME", "CD.COLLEGE_ID=" + ddlColg.SelectedValue + "AND CD.UGPGOT IN (" + Session["ua_section"] + ") AND CD.UGPGOT=2", "D.DEGREENO");
            ddlDegree.Focus();

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_MarkEntryComparision.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
            {
                objUCommon.ShowError(Page, "Server UnAvailable");
            }
        }
    }
    protected void btnLock_Click(object sender, EventArgs e)
    {
        
       //  string CHKPROCESS = objCommon.LookUp("ACD_TRRESULT", "COUNT(1)", "IDNO IN (" + idno + ")");
        foreach (ListViewDataItem item in lvStudent.Items)
        {
            int i = 0;
            string idno = string.Empty;
            CheckBox chk = item.FindControl("chkStudent") as CheckBox;
            Label lblStudname = item.FindControl("lblStudname") as Label;
            if (chk.Checked == true)
            {
                idno = lblStudname.ToolTip.ToString();
                string CHKPROCESS = objCommon.LookUp("ACD_TRRESULT", "COUNT(1)", "IDNO IN (" + idno + ")");
                if (CHKPROCESS == "0")
                {
                    objCommon.DisplayMessage(updPgresult, lblStudname.Text + " student result is not processed,so cant not lock it.", this.Page);
                    return;
                }
                else
                {
                    int cs = 0;
                    //int cs = objMark.UpdateResultlockStatus(idno, Convert.ToInt32(ddlSem.SelectedValue));
                    if (cs == 2)
                    {
                        objCommon.DisplayMessage(updPgresult, "Result locked succesfully", this.Page);
                        btnShow_Click(sender, e);
                        return;
                    }
                }
            }
            else
            {
                i++;
            }
            if (i > 0)
            {
                objCommon.DisplayMessage(updPgresult,"Please select at least one student.", this.Page);
                return;
            }
        }
    }
}
