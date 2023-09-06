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
using System.Data;

public partial class ACADEMIC_EXAMINATION_MarksCorrection : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    MarksEntryController objMEC = new MarksEntryController();

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
                //CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }

                PopulateDropDownList();
                ddlSession.Focus();
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
                Response.Redirect("~/notauthorized.aspx?page=PublishResult.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=PublishResult.aspx");
        }
    }

    private void PopulateDropDownList()
    {
        try
        {
            objCommon.FillDropDownList(ddlSession, "ACD_EXAM_MARKENTRY_ALLOCATION A INNER JOIN ACD_SESSION_MASTER B ON A.SESSIONNO = B.SESSIONNO", "DISTINCT A.SESSIONNO", "SESSION_PNAME", "A.SESSIONNO = (SELECT MAX(SESSIONNO) FROM ACD_EXAM_MARKENTRY_ALLOCATION) ", "");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_MARKSCORRECTION.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            int idno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "IDNO", "REGNO = '" + txtEnrollmentNo.Text + "'"));
            ViewState["idno"] = idno.ToString();
            DataSet ds = objMEC.GetMarksForCorrection(Convert.ToInt32(ddlSession.SelectedValue), idno, Convert.ToInt32(ddlExamName.SelectedValue));

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                lvStudent.DataSource = ds;
                lvStudent.DataBind();
                btnSave.Visible = true;
                btnLock.Visible = true;
                btnClear.Visible = true;
            }
            else
            {
                lvStudent.DataSource = null;
                lvStudent.DataBind();
                btnSave.Visible = false;
                btnLock.Visible = false;
                btnClear.Visible = false;
                ddlSession.SelectedIndex = 0;
                ddlExamName.SelectedIndex = 0;
                txtEnrollmentNo.Text = string.Empty;
                objCommon.DisplayMessage(this.updUpdate, "No Record Found for Marks Correction !!!!", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_MARKSCORRECTION.btnShow_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string Op1Marks = string.Empty;
            string Op2Marks = string.Empty;
            string courseno = string.Empty;

            foreach (ListViewDataItem lvitem in lvStudent.Items)
            {
                Label lblCourseno = lvitem.FindControl("lblCourse") as Label;
                TextBox txtOp1Marks = lvitem.FindControl("txtOp1Marks") as TextBox;
                TextBox txtOp2Marks = lvitem.FindControl("txtOp2Marks") as TextBox;

                if (String.IsNullOrEmpty(txtOp1Marks.Text) || String.IsNullOrEmpty(txtOp2Marks.Text))
                {
                    objCommon.DisplayMessage(this.updUpdate, "Please Enter marks !!!!", this.Page);
                    return;
                }
                else
                {
                    Op1Marks = Op1Marks + txtOp1Marks.Text + ",";
                    Op2Marks = Op2Marks + txtOp2Marks.Text + ",";
                    courseno = courseno + lblCourseno.ToolTip + ",";
                }
            }
            Op1Marks = Op1Marks.TrimEnd(',');
            Op2Marks = Op2Marks.TrimEnd(',');
            courseno = courseno.TrimEnd(',');

            int n = objMEC.UpdCorrectMarksbyidno(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["idno"]), Convert.ToInt32(ddlExamName.SelectedValue), courseno, Op1Marks, Op2Marks, 0);

            if (n == 2)
            {
                objCommon.DisplayMessage(this.updUpdate, "Marks Saved Successfully !!!!", this.Page);
            }
            else
            {
                objCommon.DisplayMessage(this.updUpdate, "Error in Saving Marks !!!!", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_MARKSCORRECTION.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnLock_Click(object sender, EventArgs e)
    {
        try
        {
            string Op1Marks = string.Empty;
            string Op2Marks = string.Empty;
            string courseno = string.Empty;

            foreach (ListViewDataItem lvitem in lvStudent.Items)
            {
                Label lblCourseno = lvitem.FindControl("lblCourse") as Label;
                TextBox txtOp1Marks = lvitem.FindControl("txtOp1Marks") as TextBox;
                TextBox txtOp2Marks = lvitem.FindControl("txtOp2Marks") as TextBox;

                if (String.IsNullOrEmpty(txtOp1Marks.Text) || String.IsNullOrEmpty(txtOp2Marks.Text))
                {
                    objCommon.DisplayMessage(this.updUpdate, "Please Enter marks !!!!", this.Page);
                    return;
                }
                else
                {
                    Op1Marks = Op1Marks + txtOp1Marks.Text + ",";
                    Op2Marks = Op2Marks + txtOp2Marks.Text + ",";
                    courseno = courseno + lblCourseno.ToolTip + ",";
                }
            }
            Op1Marks = Op1Marks.TrimEnd(',');
            Op2Marks = Op2Marks.TrimEnd(',');
            courseno = courseno.TrimEnd(',');

            //btnSave_Click(sender, e);

            int n = objMEC.UpdCorrectMarksbyidno(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["idno"]), Convert.ToInt32(ddlExamName.SelectedValue), courseno, Op1Marks, Op2Marks, 1);

            if (n == 2)
            {
                objCommon.DisplayMessage(this.updUpdate, "Marks Locked Successfully !!!!", this.Page);
                btnSave.Enabled = false;
                btnLock.Enabled = false;
                foreach (ListViewDataItem lvitem in lvStudent.Items)
                {
                    TextBox txtOp1Marks = lvitem.FindControl("txtOp1Marks") as TextBox;
                    TextBox txtOp2Marks = lvitem.FindControl("txtOp2Marks") as TextBox;

                    txtOp1Marks.Enabled = false;
                    txtOp2Marks.Enabled = false;
                }
            }
            else
            {
                objCommon.DisplayMessage(this.updUpdate, "Error in Locking Marks !!!!", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_MARKSCORRECTION.btnLock_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }


    protected void lvStudent_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        ListViewDataItem dataItem = (ListViewDataItem)e.Item;

        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            TextBox txtOp1Marks = e.Item.FindControl("txtOp1Marks") as TextBox;
            TextBox txtOp2Marks = e.Item.FindControl("txtOp2Marks") as TextBox;

            HiddenField hdnOp1Lock = e.Item.FindControl("hdnOp1Lock") as HiddenField;
            HiddenField hdnOp2Lock = e.Item.FindControl("hdnOp2Lock") as HiddenField;

            if (hdnOp1Lock.Value == "1")
                txtOp1Marks.Enabled = false;
            else
                txtOp1Marks.Enabled = true;

            if (hdnOp2Lock.Value == "1")
                txtOp2Marks.Enabled = false;
            else
                txtOp2Marks.Enabled = true;

        }
        
    }
}