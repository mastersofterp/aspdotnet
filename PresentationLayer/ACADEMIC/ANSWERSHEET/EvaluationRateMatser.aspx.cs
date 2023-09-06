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


public partial class ACADEMIC_ANSWERSHEET_EvaluationRateMatser : System.Web.UI.Page
{

    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    AnswerSheetController objAnsSheetController = new AnswerSheetController();

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
                ////Load Page Help
                //if (Request.QueryString["pageno"] != null)
                //{
                //    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                //}
                BindListView();
                this.FillDropdown();
            }
            //Populate the Drop Down Lists
            //BindListView();
        }
        objCommon.SetLabelData("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]));//Set label -
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=EvaluationRateMatser.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=EvaluationRateMatser.aspx");
        }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int SRNO = int.Parse(btnEdit.CommandArgument);
            ViewState["SRNO"] = int.Parse(btnEdit.CommandArgument);
            ViewState["edit"] = "edit";
            this.ShowDetails(SRNO);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_ANSWERSHEET_EvaluationRateMatser.btnEdit_Click-> " + ex.Message + "" + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable");
        }
    }

    private void BindListView()
    {
        try
        {

            DataSet ds = objAnsSheetController.GetAllEvaluationRate();
            lvSession.DataSource = ds;
            lvSession.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_ANSWERSHEET_EvaluationRateMatser.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.DisplayMessage("Server UnAvailable", this.Page);
        }
    }
    private void FillDropdown()
    {
        try
        {
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO > 0", "DEGREENO ");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_ANSWERSHEET_EvaluationRateMatser.FillDropdown --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void ShowDetails(int SR_No)
    {
        try
        {
            //SessionController objSS = new SessionController();
            SqlDataReader dr = objAnsSheetController.GetSinglePaperRate(SR_No);

            if (dr != null)
            {
                if (dr.Read())
                {

                    if (dr["DEGREENO"] == null | dr["DEGREENO"].ToString().Equals(""))
                        ddlDegree.SelectedIndex = 0;
                    else
                        ddlDegree.Text = dr["DEGREENO"].ToString();

                    txtPerUnit.Text = dr["PER_PAPER_RATE"] == null ? string.Empty : dr["PER_PAPER_RATE"].ToString();

                }

            }
            if (dr != null) dr.Close();

            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_ANSWERSHEET_EvaluationRateMatser.ShowDetails_Click-> " + ex.Message + "" + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable");
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToInt32(ddlDegree.SelectedValue) > 0 && txtPerUnit.Text != string.Empty)
            {
                AnswerSheet objAnsSheet = new AnswerSheet();
                objAnsSheet.DegreeNo = Convert.ToInt32(ddlDegree.SelectedValue);
                int rate = objAnsSheet.PerPapeRate = Convert.ToInt32(txtPerUnit.Text);

                //Check for add or edit
                if (ViewState["action"] != null && ViewState["action"].ToString().Equals("edit"))
                {
                    //Edit 
                    int SRNO = Convert.ToInt32(ViewState["SRNO"]);

                    CustomStatus cs = (CustomStatus)objAnsSheetController.UpdatePaperRate(SRNO, rate);
                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        ClearControls();
                        BindListView();
                        objCommon.DisplayMessage(this.updSession, "Record Updated Successfully", this.Page);
                    }
                }
                else
                {
                    string count = objCommon.LookUp("ACD_REMUNERATION", "DEGREENO", "DEGREENO=" + Convert.ToUInt32(ddlDegree.SelectedValue));

                    if (count != "")
                    {
                        objCommon.DisplayMessage(this.updSession, "Degree name already exist", this.Page);
                        return;
                    }


                    CustomStatus cs = (CustomStatus)objAnsSheetController.AddPaperRate(objAnsSheet);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        ClearControls();
                        BindListView();
                        objCommon.DisplayMessage(this.updSession, "Record Added Successfully", this.Page);
                    }
                    else
                    {
                        //msgLbl.Text = "Record already exist";
                        objCommon.DisplayMessage(this.updSession, "Record already exist", this.Page);
                    }
                }

            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_ANSWERSHEET_EvaluationRateMatser.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.DisplayMessage("Server UnAvailable", this.Page);
        }

    }
    private void ClearControls()
    {
        ddlDegree.SelectedIndex = 0;
        txtPerUnit.Text = string.Empty;
        ViewState["action"] = string.Empty;
        ViewState["SRNO"] = string.Empty;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ddlDegree.SelectedIndex = 0;
        txtPerUnit.Text = string.Empty;
        // ClearControls();
    }
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtPerUnit.Text = string.Empty;
    }
}