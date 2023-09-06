using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS;
using IITMS;
using IITMS.NITPRM;

public partial class ACCOUNT_BudegtAllocation : System.Web.UI.Page
{
    UAIMS_Common objUCommon = new UAIMS_Common();
    Common objCommon = new Common();
    budgetHeadController objBudgetController = new budgetHeadController();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            ViewState["action"] = "add";

            //Check Session
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                if (Session["comp_code"] == null || Session["fin_yr"] == null)
                {
                    Session["comp_set"] = "NotSelected";
                    Response.Redirect("~/ACCOUNT/selectCompany.aspx");

                }
                else
                {
                    Session["comp_set"] = "";
                }


                //Page Authorization
                CheckPageAuthorization();

                divCompName.InnerHtml = Session["comp_name"].ToString().ToUpper();
                Page.Title = Session["coll_name"].ToString();
                //Load Page Help
               
                //DataTable dtPRNO = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString() + "_BUDGET_HEAD", "BUDG_NAME", "BUDG_NO", "BUDG_PRNO=0", "").Tables[0];
                DataTable dtPRNO = objBudgetController.FetchBudgetHeadAmount(Session["comp_code"].ToString(), 0).Tables[0];
                populatetreeview(dtPRNO, 0, null);
                populateBudgetHead();
            }
        }
    }


    private void CheckPageAuthorization()
    {
        if (Request.QueryString["obj"] != null)
        {
            if (Request.QueryString["obj"].ToString().Trim() != "config")
            {

                //Check for Authorization of Page
                if (Common.CheckPage(int.Parse(Session["userno"].ToString()), objCommon.LookUp("ACCESS_LINK", "AL_No", "AL_URL='Account/maingroup.aspx'"), int.Parse(Session["loginid"].ToString()), 0) == false)
                {
                    Response.Redirect("~/notauthorized.aspx?page=Acc_BudgetHeadCreation.aspx");
                }

            }

        }
        else
        {
            if (Request.QueryString["pageno"] != null)
            {
                //Check for Authorization of Page
                if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
                {
                    Response.Redirect("~/notauthorized.aspx?page=Acc_BudgetHeadCreation.aspx");
                }
            }
            else
            {
                //Even if PageNo is Null then, don't show the page
                Response.Redirect("~/notauthorized.aspx?page=Acc_BudgetHeadCreation.aspx");
            }
        }
    }

    private void populatetreeview(DataTable dt, int ParentID, TreeNode TreeNode)
    {
        foreach (DataRow row in dt.Rows)
        {
            //DataTable dtPRNO = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString() + "_BUDGET_HEAD", "BUDG_NAME", "BUDG_NO", "BUDG_PRNO=0", "").Tables[0];
            TreeNode treeChild = new TreeNode()
            {
                Text = row["BUDTOT_AMT"].ToString(),
                Value = row["BUDG_NO"].ToString()
            };
            if (ParentID == 0)
            {
                tvHierarchy.Nodes.Add(treeChild);
                //DataTable dtChild = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString() + "_BUDGET_HEAD", "BUDG_NAME", "BUDG_NO", "BUDG_PRNO=" + treeChild.Value, "").Tables[0];
                DataTable dtChild = objBudgetController.FetchBudgetHeadAmount(Session["comp_code"].ToString(), Convert.ToInt32(treeChild.Value)).Tables[0];
                populatetreeview(dtChild, Convert.ToInt32(treeChild.Value), treeChild);
            }
            else
            {
                TreeNode.ChildNodes.Add(treeChild);
                //DataTable dtChild = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString() + "_BUDGET_HEAD", "BUDG_NAME", "BUDG_NO", "BUDG_PRNO=" + treeChild.Value, "").Tables[0];
                DataTable dtChild = objBudgetController.FetchBudgetHeadAmount(Session["comp_code"].ToString(), Convert.ToInt32(treeChild.Value)).Tables[0];
                populatetreeview(dtChild, Convert.ToInt32(treeChild.Value), treeChild);
            }
        }
    }

    private void populateBudgetHead()
    {
        // DataSet ds = objBudgetController.FetchBudgetHead(Session["comp_code"].ToString());
        DataSet ds = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString() + "_BUDGET_HEAD a Left Join ACC_" + Session["comp_code"].ToString() + "_TRANS T ON (a.BUDG_NO = T.BudgetNo)", "budg_no, BUDG_CODE, BUDG_NAME", "BUDG_PRNO,isnull(BUD_AMT,0) BUD_AMT,(ISNULL(a.BUD_AMT,0) - ISNULL(SUM(Case when T.[TRAN] = 'Dr' Then T.AMOUNT When T.[TRAN] = 'Cr' Then -(T.AMOUNT) End),0)) As BUD_BAL_AMT", "not exists (select BUDG_PRNO from ACC_" + Session["comp_code"].ToString() + "_BUDGET_HEAD b where a.budg_no=b.BUDG_PRNO) Group by budg_no,BUDG_CODE, BUDG_NAME,BUDG_PRNO,BUD_AMT", "");
        lvbudgethead.DataSource = ds;
        lvbudgethead.DataBind();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        DataTable dtBudgetHead = new DataTable();
        if (!dtBudgetHead.Columns.Contains("BUDG_NO"))
            dtBudgetHead.Columns.Add("BUDG_NO");
        if (!dtBudgetHead.Columns.Contains("BUDAMT"))
            dtBudgetHead.Columns.Add("BUDAMT");

        foreach (RepeaterItem items in lvbudgethead.Items)
        {
            HiddenField hdnBudNo = items.FindControl("hdnBudNo") as HiddenField;
            TextBox txtAmt = items.FindControl("txtAmt") as TextBox;

            if ((txtAmt.Text == string.Empty ? 0 : Convert.ToDouble(txtAmt.Text)) >= 0)
            {
                DataRow dr = dtBudgetHead.NewRow();
                dr["BUDG_NO"] = hdnBudNo.Value;
                dr["BUDAMT"] = txtAmt.Text == string.Empty ? "0" : txtAmt.Text;
                dtBudgetHead.Rows.Add(dr);
            }
        }

        CustomStatus cs = (CustomStatus)objBudgetController.BudgetAllocation(dtBudgetHead, Session["comp_code"].ToString());
        if (cs.Equals(CustomStatus.RecordSaved))
        {
            populateBudgetHead();
            tvHierarchy.Nodes.Clear();
            //DataTable dtPRNO = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString() + "_BUDGET_HEAD", "BUDG_NAME", "BUDG_NO", "BUDG_PRNO=0", "").Tables[0];
            DataTable dtPRNO = objBudgetController.FetchBudgetHeadAmount(Session["comp_code"].ToString(), 0).Tables[0];
            populatetreeview(dtPRNO, 0, null);
            tvHierarchy.ExpandAll();
            objCommon.DisplayUserMessage(UPDMainGroup, "Record Saved Successfully", this.Page);
            //lblStatus.Text = "Record Saved Successfully!!!";
        }
        else
            lblStatus.Text = "Server Error!!!";
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        populateBudgetHead();
    }
}
