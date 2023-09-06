//=================================================================================
// PROJECT NAME  : U-AIMS                                                          
// MODULE NAME   : EXAM CREATION                                                   
// CREATION DATE : 22-MAY-2009                                                     
// CREATED BY    : SANJAY RATNAPARKHI                                              
// MODIFIED BY   :                                                                 
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

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

public partial class Academic_createexamname : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    string _uaims_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
    Exam ObjE = new Exam();

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
try
{
        if (!Page.IsPostBack)
        {
            //Check Session
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null || Session["OrgId"] == null)
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
                //Bind the exam pattern with dropdownlist

                objCommon.FillDropDownList(ddlExamPattern, "ACD_EXAM_PATTERN WITH (NOLOCK)", "PATTERNNO", "PATTERN_NAME", "ISNULL(ACTIVESTATUS,0)=1", "PATTERNNO");
                //pnlSeqNum.Visible = true;
                //btnSubmit.Visible = false;
                Divbttn.Visible = false;
            }
        }
}
        catch
        {
          throw;
        }

    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=createexamname.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=createexamname.aspx");
        }
    }

    private void BindListViewList(int patternNo)  
    {
        try
        {
            if (Convert.ToInt32(ddlExamPattern.SelectedValue) != 0)
            {
                //pnlSeqNum.Visible = true;
                ExamController objEC = new ExamController();
                DataSet ds = objEC.GetAllExamName(patternNo);
                if (ds.Tables[0].Rows.Count != 0)
                {
                    Divbttn.Visible = true;
                }
                else
                {
                    Divbttn.Visible = false;
                }
                lvExamHead.DataSource = ds.Tables[0];
                lvExamHead.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "createexamname.BindListViewList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    public void lvExamHead_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        HiddenField hfdIntEx = e.Item.FindControl("hfdIntEx") as HiddenField;
        DropDownList ddlIntEx = e.Item.FindControl("ddlExamType") as DropDownList;
        ddlIntEx.SelectedValue = hfdIntEx.Value;


        try
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {

                CheckBox chk = e.Item.FindControl("Chkstatus") as CheckBox;
                if (chk.ToolTip == "True")
                {
                    chk.Checked = true;
                    //chk.BackColor = Color.Green;
                }
                else
                {
                    chk.Checked = false;
                }

               
            }
        }
        catch { }

    }

    protected void enablelistview(int patternNo)
    {
        if (patternNo != 0)
        {
            BindListViewList(Convert.ToInt32(ddlExamPattern.SelectedValue));
        }
        else
        {
            pnlSeqNum.Visible = false;
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            //if (hftimeslot.Value == "true")
            //{
            //    ObjE.ActiveStatus = true;
            //}
            //else
            //{
            //    ObjE.ActiveStatus = false;
            //}

            ExamController objEC = new ExamController();
            int count = 0;
            foreach (ListViewDataItem lvHead in lvExamHead.Items)
            {
                Label lblFldName = lvHead.FindControl("lblFldName") as Label;
                TextBox txtExamName = lvHead.FindControl("txtExamName") as TextBox;
                DropDownList ddlExamType = lvHead.FindControl("ddlExamType") as DropDownList;
                CheckBox CheckBox1 = lvHead.FindControl("Chkstatus") as CheckBox;
                if (CheckBox1.Checked == true)
                {
                    ObjE.ActiveStatus = true;
                }
                else
                {
                    ObjE.ActiveStatus = false;
                }

                CustomStatus cs = (CustomStatus)objEC.UpdateExamHead((lblFldName.Text), (txtExamName.Text), Convert.ToInt32(ddlExamPattern.SelectedValue), Convert.ToInt32(ddlExamType.SelectedValue), Convert.ToInt32(Session["OrgId"]), ObjE.ActiveStatus);
                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    count = 1;
                }
            }
            if (count == 1)
            {
                lblerror.Text = null;
                //lblmsg.Text = "Record Updated Successfully.";
                objCommon.DisplayMessage(this.updExam, "Pattern Updated Successfully.", this.Page);
            }
            else
            {
                //lblmsg.Text = "Failed To Update Record.";
            }
            BindListViewList(Convert.ToInt32(ddlExamPattern.SelectedValue));
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "createexamname.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlExamPattern_SelectedIndexChanged(object sender, EventArgs e)
     {
        enablelistview(Convert.ToInt32(ddlExamPattern.SelectedValue));

        if (ddlExamPattern.SelectedIndex == 0)
        {
            Divbttn.Visible = false;
            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }
     }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Page.Response.Redirect(Page.Request.Url.ToString(), true);
    }
}

