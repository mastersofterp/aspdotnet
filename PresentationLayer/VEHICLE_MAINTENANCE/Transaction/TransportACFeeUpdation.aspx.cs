//=========================================================================
// PRODUCT NAME  : UAIMS
// MODULE NAME   : VEHICLE MANAGEMENT
// CREATE BY     : GOPAL ANTHATI
// CREATED DATE  : 21-10-2020
// DESCRIPTION   : 
//=========================================================================

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
using System.Net.Mail;
using System.Text;
using System.Net;
using System.IO;

public partial class VEHICLE_MAINTENANCE_Transaction_TransportACFeeUpdation : System.Web.UI.Page
{
    Common objCommon = new Common();
    VM objEnt = new VM();
    VMController objCon = new VMController();

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
                //Studpanel.Enabled = true;
                objCommon.FillDropDownList(ddlAdmBatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>0", "BATCHNAME DESC");
                objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO > 0", "DEGREENAME");
                objCommon.FillDropDownList(ddlsemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0", "SEMESTERNO");
                
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
                Response.Redirect("~/notauthorized.aspx?page=studentinfoentry.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=studentinfoentry.aspx");
        }
    }
    protected void ddlAdmBatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlDegree, "ACD_COLLEGE_DEGREE A INNER JOIN ACD_DEGREE B ON(A.DEGREENO=B.DEGREENO) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD ON (CD.DEGREENO=B.DEGREENO)", "DISTINCT(A.DEGREENO)", "B.DEGREENAME", "", "A.DEGREENO");
    }
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "A.BRANCHNO", "A.LONGNAME", "B.DEGREENO=" + ddlDegree.SelectedValue + " AND A.BRANCHNO>0", "A.BRANCHNO");
    }

    void DisplayMessage(string Message)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + Message + "');", true);

    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = null;
            int Degreeno = Convert.ToInt32(ddlDegree.SelectedValue);
            int AdmBatchno = Convert.ToInt32(ddlAdmBatch.SelectedValue);
            int Branchno = Convert.ToInt32(ddlBranch.SelectedValue);
            int Semesterno = Convert.ToInt32(ddlsemester.SelectedValue);

            ds = objCon.GetStudentDetails(AdmBatchno,Degreeno,Branchno,Semesterno);

            if (ds.Tables[0].Rows.Count > 0)
            {
                lvStudent.DataSource = ds;
                lvStudent.DataBind();
                pnlstud.Visible = true;
                btnSubmit.Visible = true;
            }
            else
            {
                lvStudent.DataSource = null;
                lvStudent.DataBind();
                pnlstud.Visible = false;
                btnSubmit.Visible = false;
                DisplayMessage("Record Not Found.");
            }
            //for (int i = 0; i < lvStudent.Items.Count; i++)
            //{
            //    CheckBox chkSelect = lvStudent.Items[i].FindControl("chkSelect") as CheckBox;                

            //    if (ds.Tables[0].Rows[i]["TRANSPORT_WITH_AC"].ToString() == "1")
            //    {
            //        chkSelect.Checked = true;
            //    }
            //}
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "btnShow_Click.btnShow_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.DisplayMessage("Server UnAvailable", this.Page);
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int Idno;
        int Count = 0;
        foreach (ListViewDataItem lv in lvStudent.Items)
        {
            
            CheckBox chkSelect = lv.FindControl("chkSelect") as CheckBox;
            HiddenField hdnIdno = lv.FindControl("hdnIdno") as HiddenField;
            //Label lblIdno = (Label)lv.FindControl("lblIdno");

            if (chkSelect.Checked)
            {
                Idno = Convert.ToInt32(hdnIdno.Value);
                CustomStatus cs = (CustomStatus)objCon.UpdateTransportAcFesStatus(Idno,1);
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    Count = 1;
                }
            }
            else
            {
                Idno = Convert.ToInt32(hdnIdno.Value);
                CustomStatus cs = (CustomStatus)objCon.UpdateTransportAcFesStatus(Idno,0);
                Count = 1;
            }
        }

        if (Count == 1)
        {
            DisplayMessage("Record Updated Successfully.");
            Clear();
            return;
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {        
        Clear();
    }

    private void Clear()
    {
        ddlAdmBatch.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        ddlsemester.SelectedIndex = 0;
        lvStudent.DataSource = null;
        lvStudent.DataBind();
        pnlstud.Visible = false;
        btnSubmit.Visible = false;
    }
}