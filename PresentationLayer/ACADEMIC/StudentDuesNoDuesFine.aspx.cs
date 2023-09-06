
//======================================================================================
// PROJECT NAME  : UAIMS [Sarala Birla University, Ranchi]                                                          
// MODULE NAME   : ACADEMIC/EXAMINATION                                                             
// PAGE NAME     : NO DUES/DUES FEES                                    
// CREATION DATE : 05-APRIL-2019
// CREATED BY    : MD. REHBAR SHEIKH                                     
// MODIFIED DATE :                                                              
// MODIFIED DESC :                                                                  
//======================================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class ACADEMIC_StudentDuesNoDuesFine : System.Web.UI.Page
{
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
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                ViewState["ipAddress"] = GetUserIPAddress();
                PopulateDropDownList();
            }
        }
        divMsg.InnerHtml = string.Empty;
    }

    private void PopulateDropDownList()
    {
        try
        {
            objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0  AND ISNULL(IS_ACTIVE,0)=1", "SESSIONNO DESC");
            //objCommon.FillDropDownList(ddlColg, "ACD_COLLEGE_MASTER C INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD ON (CD.COLLEGE_ID=C.COLLEGE_ID)", "DISTINCT (C.COLLEGE_ID)", "C.COLLEGE_NAME", "C.COLLEGE_ID > 0", "C.COLLEGE_ID");  //  AND CD.UGPGOT IN (" + Session["ua_section"] + ") AND CD.UGPGOT = 1
            objCommon.FillDropDownList(ddlColg, "ACD_COLLEGE_MASTER C WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD WITH (NOLOCK) ON (CD.COLLEGE_ID=C.COLLEGE_ID)", "DISTINCT (C.COLLEGE_ID)", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME", "C.COLLEGE_ID IN(" + Session["college_nos"] + ") AND C.COLLEGE_ID > 0", "C.COLLEGE_ID");
            // objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO > 0", "DEGREENO");         
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

    private string GetUserIPAddress()
    {
        string User_IPAddress = string.Empty;
        string User_IPAddressRange = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (string.IsNullOrEmpty(User_IPAddressRange))//without Proxy detection
        {
            User_IPAddress = Request.ServerVariables["REMOTE_ADDR"];

        }

        return User_IPAddress;

    }


    protected void ddlColg_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ////objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE CD ON (D.DEGREENO = CD.DEGREENO) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CDB ON (CDB.DEGREENO=D.DEGREENO)", "DISTINCT D.DEGREENO", " D.DEGREENAME", " CD.COLLEGE_ID=" + ddlColg.SelectedValue, "D.DEGREENO");//+ "AND CDB.UGPGOT IN (" + Session["ua_section"] + ") AND CDB.UGPGOT = 1"
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.DEGREENO > 0 AND B.COLLEGE_ID=" + ddlColg.SelectedValue, "D.DEGREENO");
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

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlDegree.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue + " and B.College_id=" + ddlColg.SelectedValue, "A.LONGNAME");

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
            objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME WITH (NOLOCK)", "SCHEMENO", "SCHEMENAME", "BRANCHNO = " + ddlBranch.SelectedValue + " AND DEGREENO=" + ddlDegree.SelectedValue, "SCHEMENO");
            ddlScheme.Focus();
            ddlSem.Items.Clear();
            ddlSem.Items.Add(new ListItem("Please Select", "0"));

        }
        else
        {
            ddlScheme.Items.Clear();
            ddlScheme.Items.Add(new ListItem("Please Select", "0"));
            ddlSem.Items.Clear();
            ddlSem.Items.Add(new ListItem("Please Select", "0"));
        }
    }

    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlScheme.SelectedIndex > 0)
        {
            ddlSem.Items.Clear();
            objCommon.FillDropDownList(ddlSem, "ACD_STUDENT_RESULT WITH (NOLOCK)", "DISTINCT SEMESTERNO", "DBO.FN_DESC('SEMESTER',SEMESTERNO) AS SEMESTER", "SCHEMENO = " + Convert.ToInt32(ddlScheme.SelectedValue) + " AND SESSIONNO = " + Convert.ToInt32(ddlSession.SelectedValue), "SEMESTERNO");
            ddlSem.Focus();
        }
        else
        {
            ddlSem.Items.Clear();
            ddlSem.Items.Add(new ListItem("Please Select", "0"));
        }
    }

    public void show()
    {
        if (ddlStudentType.SelectedIndex > 0)
        {
            MarksEntryController objMark = new MarksEntryController();
            DataSet ds = objMark.GetNoDuesStudentData(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSem.SelectedValue), Convert.ToInt32(ddlColg.SelectedValue), Convert.ToInt32(ddlStudentType.SelectedValue), Convert.ToInt32(Session["userno"]), Convert.ToInt32(Session["usertype"]));
            if (ds.Tables[0].Rows.Count > 0)
            {
                pnlLv.Visible = true;
                lvStudent.DataSource = ds;
                lvStudent.DataBind();
                btnSubmit.Enabled = true;
            }
            else
            {
                pnlLv.Visible = false;
                lvStudent.DataSource = null;
                lvStudent.DataBind();

                ShowMessage("No Student Found.");
                return;
            }
        }
        else
        {
            ShowMessage("Please Select Student Type.");
            ddlStudentType.Focus();
            return;
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        show();
    }


    private void ShowMessage(string message)
    {
        if (message != string.Empty)
        {
            divMsg.InnerHtml += "<script type='text/javascript' language='javascript'> alert('" + message + "'); </script>";
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlSession.SelectedIndex == 0)
            {
                objCommon.DisplayMessage(updresult, "Please Select Session.", this.Page);
                return;
            }

            if (ddlColg.SelectedIndex == 0)
            {
                objCommon.DisplayMessage(updresult, "Please Select College.", this.Page);
                return;
            }

            if (ddlDegree.SelectedIndex == 0)
            {
                objCommon.DisplayMessage(updresult, "Please Select Degree.", this.Page);
                return;
            }

            if (ddlBranch.SelectedIndex == 0)
            {
                objCommon.DisplayMessage(updresult, "Please Select Branch.", this.Page);
                return;
            }

            if (ddlScheme.SelectedIndex == 0)
            {
                objCommon.DisplayMessage(updresult, "Please Select Scheme.", this.Page);
                return;
            }

            if (ddlSem.SelectedIndex == 0)
            {
                objCommon.DisplayMessage(updresult, "Please Select Semester.", this.Page);
                return;
            }

            if (ddlStudentType.SelectedIndex == 0)
            {
                objCommon.DisplayMessage(updresult, "Please Select Student Type.", this.Page);
                return;
            }

            DataTable dt = new DataTable();
            FeeCollectionController ObjFee = new FeeCollectionController();
            string notSelected_studName = string.Empty;
            string notSelected_studName_remark = string.Empty;
            bool atleastOneSelected = false;
            dt.Columns.AddRange(new DataColumn[12] 
                        { 
                            new DataColumn("IDNO", typeof(int)),
                            new DataColumn("SESSIONNO", typeof(int)),
                            new DataColumn("COLLEGE_ID",typeof(int)),
                            new DataColumn("DEGREENO",typeof(int)),
                            new DataColumn("BRANCHNO",typeof(int)),
                            new DataColumn("SCHEMENO",typeof(int)),
                            new DataColumn("SEMESTERNO",typeof(int)),
                            new DataColumn("STUD_TYPE",typeof(int)),
                            new DataColumn("AMOUNT",typeof(decimal)),
                            new DataColumn("REMARKS",typeof(string)),
                            new DataColumn("UA_NO",typeof(int)),
                            new DataColumn("UA_TYPE",typeof(int)) 
                        });



            foreach (ListViewDataItem item in lvStudent.Items)
            {
                if ((item.FindControl("chkStudent") as CheckBox).Checked)
                {
                    atleastOneSelected = true;
                    if ((item.FindControl("txtAmount") as TextBox).Text.Trim() != string.Empty)
                    {
                        int IDNO = Convert.ToInt32((item.FindControl("lblStudname") as Label).ToolTip);
                        int SESSIONNO = Convert.ToInt32(ddlSession.SelectedValue);
                        int COLLEGE_ID = Convert.ToInt32(ddlColg.SelectedValue);
                        int DEGREENO = Convert.ToInt32(ddlDegree.SelectedValue);
                        int BRANCHNO = Convert.ToInt32(ddlBranch.SelectedValue);
                        int SCHEMENO = Convert.ToInt32(ddlScheme.SelectedValue);
                        int SEMESTERNO = Convert.ToInt32(ddlSem.SelectedValue);
                        int STUD_TYPE = Convert.ToInt32(ddlStudentType.SelectedValue);
                        decimal AMOUNT = Convert.ToDecimal((item.FindControl("txtAmount") as TextBox).Text.Trim());
                        string REMARKS = (item.FindControl("txtRemarks") as TextBox).Text;
                        int UA_NO = Convert.ToInt32(Session["userno"]);
                        int UA_TYPE = Convert.ToInt32(Session["usertype"]);

                        dt.Rows.Add(IDNO, SESSIONNO, COLLEGE_ID, DEGREENO, BRANCHNO, SCHEMENO, SEMESTERNO, STUD_TYPE, AMOUNT, REMARKS, UA_NO, UA_TYPE);
                    }
                    else
                    {
                        Label lblStudname = item.FindControl("lblStudname") as Label;
                        notSelected_studName += lblStudname.Text + ", ";
                    }

                    if ((item.FindControl("txtRemarks") as TextBox).Text.Trim() == string.Empty)
                    {
                        Label lblStudname = item.FindControl("lblStudname") as Label;
                        notSelected_studName_remark += lblStudname.Text + ", ";
                    }
                }
            }

            if (!atleastOneSelected)
            {
                objCommon.DisplayMessage(updresult, "Please select student before proceeding.", this.Page);
                return;
            }

            string notSelected = notSelected_studName.TrimEnd().TrimEnd(',');
            if (notSelected != string.Empty)
            {
                objCommon.DisplayMessage(updresult, "Please enter Amount for Student(s): " + notSelected + "", this.Page);
                return;
            }

            string notSelectedRemark = notSelected_studName_remark.TrimEnd().TrimEnd(',');
            if (notSelectedRemark != string.Empty)
            {
                objCommon.DisplayMessage(updresult, "Please enter Remark for Student(s): " + notSelectedRemark + "", this.Page);
                return;
            }

            int count = 0;
            if (dt.Rows.Count > 0)
            {
                btnSubmit.Enabled = false;
                CustomStatus cs = new CustomStatus();
                foreach (DataRow dr in dt.Rows)
                {
                    //INSERT QUERY
                    cs = (CustomStatus)ObjFee.NoDuesInsert(Convert.ToInt32(dr["IDNO"]), Convert.ToInt32(dr["SESSIONNO"]), Convert.ToInt32(dr["COLLEGE_ID"]),
                                                                        Convert.ToInt32(dr["DEGREENO"]), Convert.ToInt32(dr["BRANCHNO"]), Convert.ToInt32(dr["SCHEMENO"]),
                                                                        Convert.ToInt32(dr["SEMESTERNO"]), Convert.ToInt32(dr["STUD_TYPE"]), Convert.ToDecimal(dr["AMOUNT"]),
                                                                        dr["REMARKS"].ToString(), Convert.ToInt32(dr["UA_NO"]), Convert.ToInt32(dr["UA_TYPE"]));

                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        count++;
                    }
                }

                if (count == dt.Rows.Count)
                {
                    objCommon.DisplayMessage(updresult, "Record Submited Successfully.", this.Page);
                    show();
                    //clear();
                    return;
                }
                else
                {
                    objCommon.DisplayMessage(updresult, "Error in Processing Result!", this.Page);
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_StudentDuesNoDuesFine.btnSubmit_Click() -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    public void clear()
    {
        ddlSession.SelectedIndex = 0;
        ddlColg.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        ddlScheme.SelectedIndex = 0;
        ddlSem.SelectedIndex = 0;
        ddlStudentType.SelectedIndex = 0;
        lvStudent.DataSource = null;
        lvStudent.DataBind();
        pnlLv.Visible = false;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    protected void lvStudent_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        try
        {
            if ((e.Item.ItemType == ListViewItemType.DataItem))
            {
                ListViewDataItem dataItem = (ListViewDataItem)e.Item;
                DataRow dr = ((DataRowView)dataItem.DataItem).Row;

                if (dr["STATUS"].Equals("PAY"))
                {
                    ((CheckBox)e.Item.FindControl("chkStudent")).Enabled = false;
                    ((TextBox)e.Item.FindControl("txtAmount")).Enabled = false;
                    ((TextBox)e.Item.FindControl("txtRemarks")).Enabled = false;
                    ((Label)e.Item.FindControl("lblStatus")).Text = "Done";
                    ((TextBox)e.Item.FindControl("txtAmount")).BorderColor = System.Drawing.Color.Green;
                    ((TextBox)e.Item.FindControl("txtAmount")).ForeColor = System.Drawing.Color.Black;
                    ((TextBox)e.Item.FindControl("txtRemarks")).BorderColor = System.Drawing.Color.Green;
                    ((TextBox)e.Item.FindControl("txtRemarks")).ForeColor = System.Drawing.Color.Black;
                    ((Label)e.Item.FindControl("lblStatus")).ForeColor = System.Drawing.Color.Green;
                    ((Label)e.Item.FindControl("lblStatus")).Font.Bold = true;

                }
                else if (dr["STATUS"].Equals("NOTPAY"))
                {
                    ((CheckBox)e.Item.FindControl("chkStudent")).Enabled = true;
                    ((TextBox)e.Item.FindControl("txtAmount")).Enabled = true;
                    ((TextBox)e.Item.FindControl("txtRemarks")).Enabled = true;
                    ((Label)e.Item.FindControl("lblStatus")).Text = "Pending";
                    ((TextBox)e.Item.FindControl("txtAmount")).BorderColor = System.Drawing.Color.Red;
                    ((TextBox)e.Item.FindControl("txtAmount")).ForeColor = System.Drawing.Color.Black;
                    ((TextBox)e.Item.FindControl("txtRemarks")).BorderColor = System.Drawing.Color.Red;
                    ((TextBox)e.Item.FindControl("txtRemarks")).ForeColor = System.Drawing.Color.Black;
                    ((Label)e.Item.FindControl("lblStatus")).ForeColor = System.Drawing.Color.Red;
                    ((Label)e.Item.FindControl("lblStatus")).Font.Bold = true;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_StudentDuesNoDuesFine.lvStudent_ItemDataBound() -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
}