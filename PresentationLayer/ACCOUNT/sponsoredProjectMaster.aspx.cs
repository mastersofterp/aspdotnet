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

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Xml;
using System.Web.Services;
using System.Collections.Generic;
using IITMS.NITPRM;
using System.IO;
using System.Data.Linq;

using System.Data.SqlClient;

public partial class ACCOUNT_sponsoredProjectMaster : System.Web.UI.Page
{
    UAIMS_Common objUCommon = new UAIMS_Common();
    Common objCommon = new Common();
    SponserProject objpro = new SponserProject();
    SponsorProjectController objProController = new SponsorProjectController();
    protected void Page_PreInit(object sender, EventArgs e)
    {
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
                // CheckPageAuthorization();
                Page.Title = Session["coll_name"].ToString();
                if (Session["comp_code"] == null)
                {
                    Response.Redirect("~/Account/selectCompany.aspx");
                }
                divCompName.InnerHtml = Session["comp_name"].ToString().ToUpper();
                FillDropDown();
                FillListView();
            }
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["obj"] != null)
        {
            if (Request.QueryString["obj"].ToString().Trim() != "config")
            {
                if (Request.QueryString["pageno"] != null)
                {
                    //Check for Authorization of Page
                    if (Common.CheckPage(int.Parse(Session["userno"].ToString()), objCommon.LookUp("ACCESS_LINK", "AL_No", "AL_URL='Account/selectCompany.aspx'"), int.Parse(Session["loginid"].ToString()), 0) == false)
                    {
                        Response.Redirect("~/notauthorized.aspx?page=ledgerhead.aspx");
                    }
                }
                else
                {
                    //Even if PageNo is Null then, don't show the page
                    Response.Redirect("~/notauthorized.aspx?page=ledgerhead.aspx");
                }

            }

        }
        else
        {

            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=ledgerhead.aspx");
            }

        }
    }

    private void FillDropDown()
    {
        ddlProjectName.Items.Clear();
        ddlProjectSubHead.Items.Clear();
        objCommon.FillDropDownList(ddlProjectName, "Acc_" + Session["comp_code"].ToString() + "_Project", "ProjectId", "ProjectName", "ProjectId > 0", "");
        objCommon.FillDropDownList(ddlProjectSubHead, "Acc_" + Session["comp_code"].ToString() + "_ProjectSubHead", "ProjectSubId", " ProjectSubHeadName+'('+ProjectSubHeadShort+')' as ProjectSubHeadName", "", "");
        objCommon.FillDropDownList(ddlDepartment, "PAYROLL_SUBDEPT", "SUBDEPTNO", "SUBDEPT", "", "");

        objCommon.FillDropDownList(ddlProject, "Acc_" + Session["comp_code"].ToString() + "_Project", "ProjectId", "ProjectName", "ProjectId > 0", "ProjectId");
        objCommon.FillDropDownList(ddlResType, "PROJECT_HEADTYPE", "ID", "PROJECTHEADTYPE", "", "");

        //Added by Pawan Nikhare
        objCommon.FillDropDownList(ddlFundingAgency, "ACC_FUNDING_AGENCY", "AGENCY_ID", "FUNDING_AGENCY", "", "");

    }

    private void FillListView()
    {
        DataSet dsProject = objCommon.FillDropDown("Acc_" + Session["comp_code"].ToString() + "_Project a left join payroll_subdept b on (a.department=b.subdeptno) LEFT JOIN ACC_" + Session["comp_code"].ToString() + "_PARTY AP ON(AP.PARTY_NO = A.PARTY_NO)", "a.ProjectId,a.ProjectShortName,subdept,a.sanctionby,a.SanctionLetter", "a.ProjectName,a.scheme,a.coordinator,a.value,AP.PARTY_NAME", "", "");
        DataSet dsSubProject = objCommon.FillDropDown("Acc_" + Session["comp_code"].ToString() + "_ProjectSubHead A left join Acc_" + Session["comp_code"].ToString() + "_Project B on(A.ProjectId=B.ProjectId) LEFT JOIN ACC_" + Session["comp_code"].ToString() + "_PARTY AP ON(AP.PARTY_NO = A.Party_No) LEFT JOIN PROJECT_HEADTYPE C on(C.ID=A.HeadType_id)", "A.ProjectSubId,A.ProjectSubHeadShort,A.Res_Type", "A.ProjectSubHeadName,B.ProjectName,C.PROJECTHEADTYPE,AP.PARTY_NAME", "", "");
        ListView1.DataSource = dsProject;
        ListView1.DataBind();
        lstSubProj.DataSource = dsSubProject;
        lstSubProj.DataBind();

        DataSet dsProjectAllocation = objCommon.FillDropDown("Acc_" + Session["comp_code"].ToString() + "_ProjectAllocation a inner join Acc_" + Session["comp_code"].ToString() + "_Project b on (a.ProjectId=b.ProjectId)", "distinct ProjectName", "a.ProjectId", "", "");
        rptsubhead.DataSource = dsProjectAllocation;
        rptsubhead.DataBind();
    }

    protected void btnSubmitProj_Click(object sender, EventArgs e)
    {
        string ProName = txtProjName.Text;
        int look = 0;
        if (ViewState["ProjectId"] == null)
        {
            look = Convert.ToInt32(objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_PROJECT", "count(isnull(ProjectId,0))", "ProjectName='" + ProName + "'"));
        }
        else
        {
            look = Convert.ToInt32(objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_PROJECT", "count(isnull(ProjectId,0))", "ProjectName='" + ProName + "' AND ProjectId !=" + Convert.ToInt32(ViewState["ProjectId"].ToString()) + ""));
        }

        if (look > 0)
        {
            objCommon.DisplayUserMessage(updBank, "Project Name Already Exist.!", this);
            btnSubmit.Text = "Submit";
            ViewState["action"] = "add";
            return;
        }

        try
        {
            int ret = 0;
            objpro.ProjectName = txtProjName.Text;
            objpro.ProjectShortName = txtSponShortName.Text;

            objpro.Department = Convert.ToInt32(ddlDepartment.SelectedValue);
            objpro.SanctionBy = txtSanctionBy.Text;
            objpro.Scheme = txtScheme.Text;
            objpro.Coordinator = txtCoordinator.Text;
            objpro.Value = txtValue.Text == "" ? 0 : Convert.ToDouble(txtValue.Text);
            objpro._SanctionLetter = txtSanctionLetter.Text.Trim();
            objpro.Date = Convert.ToDateTime(txtDate.Text); 
            objpro.Party_No = Convert.ToInt32(HdnAcc.Value);
            objpro.FundingAgency = Convert.ToInt32(ddlFundingAgency.SelectedValue);
            if (txtProjectDuration.Text == "")
            {
                objpro.ProjectDuration = 0;
            }
            else
            {
                objpro.ProjectDuration = Convert.ToInt32(txtProjectDuration.Text);
            }
            objpro.ProjectStartDate =Convert.ToDateTime(txtStartDate.Text);
            objpro.ProjectEndDate = Convert.ToDateTime(txtEndDate.Text);
            
           if (txtAmtRecurring.Text == "")
            {
                objpro.AmountReceivedRecurring =0  ;
            }
            else
            {
                objpro.AmountReceivedRecurring =Convert.ToDecimal(txtAmtRecurring.Text);
            }

            if (txtAmtNonRecurring.Text == "")
            {
                objpro.AmountReceivedNonRecurring =0 ;
            }
            else
            {
                objpro.AmountReceivedNonRecurring = Convert.ToDecimal(txtAmtNonRecurring.Text);
            }
           
              
           objpro.SanctionDate = Convert.ToDateTime(txtSanctionDate.Text);
            

                if (ViewState["ProjectId"] == null)
            {
                objpro.ProjectId = 0;
                ret = objProController.AddUpdateProjectName(objpro, Session["comp_code"].ToString());
            }
            else
            {
                objpro.ProjectId = Convert.ToInt32(ViewState["ProjectId"].ToString());
                ret = objProController.AddUpdateProjectName(objpro, Session["comp_code"].ToString());
            }

            switch (ret)
            {
                case 1:
                    {
                        objCommon.DisplayUserMessage(updBank, "Record Saved Successfully", this.Page);
                        ClearProj();
                        FillListView();
                        break;
                    }
                case 2:
                    {
                        objCommon.DisplayUserMessage(updBank, "Record Update Successfully", this.Page);
                        ClearProj();
                        FillListView();
                        break;
                    }
                default:
                    {
                        objCommon.DisplayUserMessage(updBank, "Error Occurred", this.Page);

                        break;
                    }
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void btnCancelProj_Click(object sender, EventArgs e)
    {
        ClearProj();
    }

    private void ClearProj()
    {
        txtSponShortName.Text = "";
        txtProjName.Text = "";
        ddlDepartment.SelectedIndex = 0;
        txtSanctionBy.Text = "";
        txtScheme.Text = "";
        txtCoordinator.Text = "";
        txtValue.Text = "";
        ViewState["ProjectId"] = null;
        txtSanctionLetter.Text = "";
        txtDate.Text = "";
        HdnAcc.Value = "0";
        txtAgainstAcc.Text = "";
        txtProjectDuration.Text = string.Empty;
        txtStartDate.Text = string.Empty;
        txtEndDate.Text = string.Empty;
        txtAmtRecurring.Text = string.Empty;
        txtAmtNonRecurring.Text = string.Empty;
        txtSanctionDate.Text = string.Empty;
        ddlFundingAgency.SelectedIndex = 0;
        FillDropDown();
    }

    protected void btnSubmitSubProj_Click(object sender, EventArgs e)
    {
        string ProSubHeadName = txtSponsorProj.Text;
        string ProjectSubHeadShort = txtProjShortName.Text;
        string look = string.Empty;
        var Count = 0;
        var Ledger = 0;
        if (ViewState["ProjectSubId"] == null)
        {
            look = (objCommon.LookUp("Acc_" + Session["comp_code"].ToString() + "_ProjectSubHead", "ProjectSubHeadShort", "ProjectSubHeadShort='" + ProjectSubHeadShort + "'AND ProjectSubHeadName='" + ProSubHeadName + "' AND ProjectId='" + ddlProject.SelectedValue + "' AND HeadType_id='" + ddlResType.SelectedValue + "'"));
            Count = Convert.ToInt32(objCommon.LookUp("Acc_" + Session["comp_code"].ToString() + "_ProjectSubHead", "count(*)", "(Party_No=" + Convert.ToInt32(hdnOpartyManual.Value) + " OR Party_No !=" + Convert.ToInt32(hdnOpartyManual.Value) + ") AND ProjectSubHeadName='" + ProSubHeadName + "' AND ProjectId='" + ddlProject.SelectedValue + "' AND HeadType_id='" + ddlResType.SelectedValue + "'"));
            Ledger = Convert.ToInt32(objCommon.LookUp("Acc_" + Session["comp_code"].ToString() + "_ProjectSubHead", "count(*)", "Party_No=" + Convert.ToInt32(hdnOpartyManual.Value) + " AND ProjectId ='" + ddlProject.SelectedValue + "'"));

        }
        else
        {
            look = (objCommon.LookUp("Acc_" + Session["comp_code"].ToString() + "_ProjectSubHead", "ProjectSubHeadShort", "ProjectSubHeadShort='" + ProjectSubHeadShort + "' AND ProjectSubHeadName='" + ProSubHeadName + "' AND ProjectId='" + ddlProject.SelectedValue + "' AND HeadType_id='" + ddlResType.SelectedValue + "' AND ProjectSubId !='" + ViewState["ProjectSubId"].ToString() + "'"));
            Count = Convert.ToInt32(objCommon.LookUp("Acc_" + Session["comp_code"].ToString() + "_ProjectSubHead", "count(*)", "(Party_No=" + Convert.ToInt32(hdnOpartyManual.Value) + " OR Party_No !=" + Convert.ToInt32(hdnOpartyManual.Value) + ") AND ProjectSubHeadName='" + ProSubHeadName + "' AND ProjectId='" + ddlProject.SelectedValue + "' AND HeadType_id='" + ddlResType.SelectedValue + "' AND ProjectSubId !='" + ViewState["ProjectSubId"].ToString() + "'"));
            Ledger = Convert.ToInt32(objCommon.LookUp("Acc_" + Session["comp_code"].ToString() + "_ProjectSubHead", "count(*)", "Party_No=" + Convert.ToInt32(hdnOpartyManual.Value) + " AND ProjectId='" + ddlProject.SelectedValue + "' AND ProjectSubId !='" + ViewState["ProjectSubId"].ToString() + "'"));

        }

        if (look == ProjectSubHeadShort)
        {
            objCommon.DisplayUserMessage(updBank, "Project SubHead Name Already Exist.!", this);
            btnSubmit.Text = "Submit";
            //ViewState["action"] = "add";
            return;
        }
        else if (Count > 0)
        {
            objCommon.DisplayUserMessage(updBank, "Expense Head Already Exist selected Project.!", this);
            btnSubmit.Text = "Submit";
            //ViewState["action"] = "add";
            return;
        }
        else if (Ledger > 0)
        {
            objCommon.DisplayUserMessage(updBank, "Ledger Name Already Exist selected Project.!", this);
            btnSubmit.Text = "Submit";
            //ViewState["action"] = "add";
            return;
        }

        try
        {
            int ret = 0;
            int srno = 0;
            string SrNo = string.Empty;
            objpro.ProjectSubHeadShort = txtProjShortName.Text;
            objpro.ProjectSubHeadName = txtSponsorProj.Text;
            if (ViewState["ProjectSubId"] == null)
            {
                //srno = Convert.ToInt32(objCommon.LookUp("Acc_" + Session["comp_code"].ToString() + "_ProjectSubHead", "ISNULL(SUM(SrNo),0)", "ProjectId='" + ddlProject.SelectedValue + "' AND HeadType_id='" + ddlResType.SelectedValue + "'"));
                SrNo = objCommon.LookUp("Acc_" + Session["comp_code"].ToString() + "_ProjectSubHead", "top 1 SrNo", "ProjectId='" + ddlProject.SelectedValue + "' AND HeadType_id='" + ddlResType.SelectedValue + "' ORDER BY ProjectSubId DESC");
            }
            else
            {

                //srno = Convert.ToInt32(objCommon.LookUp("Acc_" + Session["comp_code"].ToString() + "_ProjectSubHead", "ISNULL(SUM(SrNo),0)", "ProjectId='" + ddlProject.SelectedValue + "' AND HeadType_id='" + ddlResType.SelectedValue + "' AND ProjectSubId !='" + ViewState["ProjectSubId"].ToString() + "'"));
                SrNo = objCommon.LookUp("Acc_" + Session["comp_code"].ToString() + "_ProjectSubHead", "top 1 SrNo", "ProjectId='" + ddlProject.SelectedValue + "' AND HeadType_id='" + ddlResType.SelectedValue + "' AND ProjectSubId !='" + ViewState["ProjectSubId"].ToString() + "' ORDER BY ProjectSubId DESC");
            }
            if (SrNo != string.Empty)
            {
                srno = Convert.ToInt32(SrNo) + 1;
            }
            else
            {
                srno = 1;
            }
            objpro.SRNO = srno;
            objpro.ProjectId = Convert.ToInt32(ddlProject.SelectedValue);
            objpro.ExpHeadType = Convert.ToInt32(ddlResType.SelectedValue);
            objpro.Party_No = Convert.ToInt32(hdnOpartyManual.Value);

            //if (ddlResType.SelectedValue =="1")
            //{
            //    objpro.ResType = "RR";
            //}
            //else if (ddlResType.SelectedValue=="2")
            //{
            //    objpro.ResType = "NR";
            //}
            if (ViewState["ProjectSubId"] == null)
            {
                objpro.ProjectSubId = 0;
                ret = objProController.AddUpdateProjectSubHeadName(objpro, Session["comp_code"].ToString());
            }
            else
            {
                objpro.ProjectSubId = Convert.ToInt32(ViewState["ProjectSubId"].ToString());
                ret = objProController.AddUpdateProjectSubHeadName(objpro, Session["comp_code"].ToString());
            }

            switch (ret)
            {
                case 1:
                    {
                        objCommon.DisplayUserMessage(updBank, "Record Saved Successfully", this.Page);
                        ClearSubProj();
                        FillListView();
                        break;
                    }
                case 2:
                    {
                        objCommon.DisplayUserMessage(updBank, "Record Update Successfully", this.Page);
                        ClearSubProj();
                        FillListView();
                        break;
                    }
                default:
                    {
                        objCommon.DisplayUserMessage(updBank, "Error Occurred", this.Page);

                        break;
                    }
            }
        }
        catch (Exception ex)
        {

        }
    }

    private void ClearSubProj()
    {
        ViewState["ProjectSubId"] = null;
        txtProjShortName.Text = "";
        txtSponsorProj.Text = "";
        ddlResType.SelectedIndex = 0;
        ddlProject.SelectedIndex = 0;
        hdnOpartyManual.Value = "0";
        txtAcc.Text = "";
        ddlProject.Enabled = true;
        ddlResType.Enabled = true;
        //FillDropDown();
    }

    protected void btnCancelSubProj_Click(object sender, EventArgs e)
    {
        ClearSubProj();

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int ret = 0;
            objpro.ProjectId = Convert.ToInt32(ddlProjectName.SelectedValue);
            objpro.ProjectSubId = Convert.ToInt32(ddlProjectSubHead.SelectedValue);
            objpro.TotAmtReceived = txtReceived.Text == "" ? 0 : Convert.ToDouble(txtReceived.Text);
            objpro.TotAmtSpent = txttotSpent.Text == "" ? 0 : Convert.ToDouble(txttotSpent.Text);
            objpro.TotAmtRemain = txtReceivedAmt.Text == "" ? 0 : Convert.ToDouble(txtReceivedAmt.Text);


            if (ViewState["ProjectSubHeadAllocationId"] == null)
            {
                //int Count = Convert.ToInt32(objCommon.LookUp("Acc_" + Session["comp_code"].ToString() + "_ProjectAllocation", "COUNT(*)", "ProjectId =" + ddlProjectName.SelectedValue + " AND ProjectSubId =" + ddlProjectSubHead.SelectedValue));
                //if (Count > 0)
                //{
                //    objCommon.DisplayUserMessage(updBank, "Record Already Exist", this.Page);
                //    return;
                //}
                objpro.ProjectSubHeadAllocationId = 0;
                ret = objProController.AddUpdateProjectSubHeadAllocation(objpro, Session["comp_code"].ToString());
            }
            else
            {
                //int Count = Convert.ToInt32(objCommon.LookUp("Acc_" + Session["comp_code"].ToString() + "_ProjectAllocation", "COUNT(*)", "ProjectId =" + ddlProjectName.SelectedValue + " AND ProjectSubId =" + ddlProjectSubHead.SelectedValue + " AND ProjectSubHeadAllocationId <> " + Convert.ToInt32(ViewState["ProjectSubHeadAllocationId"].ToString())));
                //if (Count > 0)
                //{
                //    objCommon.DisplayUserMessage(updBank, "Record Already Exist", this.Page);
                //    return;
                //}
                objpro.ProjectSubHeadAllocationId = Convert.ToInt32(ViewState["ProjectSubHeadAllocationId"].ToString());
                ret = objProController.AddUpdateProjectSubHeadAllocation(objpro, Session["comp_code"].ToString());
            }

            switch (ret)
            {
                case 1:
                    {
                        objCommon.DisplayUserMessage(updBank, "Record Saved Successfully", this.Page);
                        Clear();
                        FillListView();
                        break;
                    }
                case 2:
                    {
                        objCommon.DisplayUserMessage(updBank, "Record Update Successfully", this.Page);
                        Clear();
                        FillListView();
                        break;
                    }
                default:
                    {
                        objCommon.DisplayUserMessage(updBank, "Error Occurred", this.Page);
                        break;
                    }
            }
        }
        catch (Exception ex)
        {

        }
    }

    private void Clear()
    {
        ViewState["ProjectSubHeadAllocationId"] = null;
        ddlProjectName.SelectedValue = "0";
        ddlProjectSubHead.SelectedValue = "0";
        txtReceived.Text = "";
        txttotSpent.Text = "";
        txtReceivedAmt.Text = "";
        FillDropDown();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }

    protected void rptsubhead_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            HiddenField hdnProjectId = e.Item.FindControl("hdnProjectId") as HiddenField;
            DataSet ds = objCommon.FillDropDown("Acc_" + Session["comp_code"].ToString() + "_ProjectAllocation a inner join Acc_" + Session["comp_code"].ToString() + "_ProjectSubHead b on (a.ProjectSubId=b.ProjectSubId) left join Acc_" + Session["comp_code"].ToString() + "_Trans T on(T.ProjectId = a.ProjectId and T.ProjectSubId = a.ProjectSubId)", "a.ProjectSubHeadAllocationId,b.ProjectSubHeadName", "TotAmtReceived,ISNULL(SUM((CASE WHEN [TRAN] = 'Dr' THEN isnULL(AMOUNT,0) WHEN [TRAN] = 'Cr' THEN -isnull(AMOUNT,0) END)),0) TotAmtSpent,TotAmtReceived- ISNULL(SUM((CASE WHEN [TRAN] = 'Dr' THEN isnULL(AMOUNT,0) WHEN [TRAN] = 'Cr' THEN -isnull(AMOUNT,0) END)),0) TotAmtRemain", "a.ProjectId=" + hdnProjectId.Value + "group by a.ProjectSubHeadAllocationId,b.ProjectSubHeadName,TotAmtReceived", "");
            ListView lvSubProjDetail = e.Item.FindControl("lvSubProjDetail") as ListView;
            lvSubProjDetail.DataSource = ds;
            lvSubProjDetail.DataBind();
        }
    }

    protected void ListView1_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        DataSet ds = objCommon.FillDropDown("Acc_" + Session["comp_code"].ToString() + "_Project A LEFT JOIN Acc_" + Session["comp_code"].ToString() + "_PARTY B ON(A.PARTY_NO = B.PARTY_NO)", "A.ProjectShortName,A.department,A.sanctionby", "A.ProjectName,A.scheme,A.coordinator,A.value,A.SANCTIONLETTER,CONVERT(VARCHAR,A.P_DATE,103)DATE,isnull(A.Party_No,0)Party_No,Isnull(B.PARTY_NAME,'')+'*'+ Isnull(B.ACC_CODE,'')PARTY_NAME,A.PROJECTDURATION,A.PROJECTSTARTDATE,A.PROJECTENDDATE,A.AMOUNTRECEIVEDRECURRING,A.AMOUNTRECEIVEDNONRECURRING,A.SANCTIONDATE,A.FUNDINGAGENCY", "ProjectId=" + e.CommandArgument.ToString(), "");
        txtSponShortName.Text = ds.Tables[0].Rows[0]["ProjectShortName"].ToString();
        txtProjName.Text = ds.Tables[0].Rows[0]["ProjectName"].ToString();
        objCommon.FillDropDownList(ddlDepartment, "PAYROLL_SUBDEPT", "SUBDEPTNO", "SUBDEPT", "", "");
        ddlDepartment.SelectedValue = ds.Tables[0].Rows[0]["department"].ToString();
        txtSanctionBy.Text = ds.Tables[0].Rows[0]["sanctionby"].ToString();
        txtScheme.Text = ds.Tables[0].Rows[0]["scheme"].ToString();
        txtCoordinator.Text = ds.Tables[0].Rows[0]["coordinator"].ToString();
        txtValue.Text = ds.Tables[0].Rows[0]["value"].ToString();   
        txtSanctionLetter.Text = ds.Tables[0].Rows[0]["SANCTIONLETTER"].ToString();
        txtDate.Text = ds.Tables[0].Rows[0]["DATE"].ToString();
        txtProjectDuration.Text = ds.Tables[0].Rows[0]["PROJECTDURATION"].ToString();
        txtStartDate.Text = ds.Tables[0].Rows[0]["PROJECTSTARTDATE"].ToString();
        txtEndDate.Text = ds.Tables[0].Rows[0]["PROJECTENDDATE"].ToString();
        txtAmtRecurring.Text = ds.Tables[0].Rows[0]["AMOUNTRECEIVEDRECURRING"].ToString();
        txtAmtNonRecurring.Text = ds.Tables[0].Rows[0]["AMOUNTRECEIVEDNONRECURRING"].ToString();
        //txtSanctionDate.Text = ds.Tables[0].Rows[0]["SANCTIONDATE"].ToString();
        DateTime sanctiondt = Convert.ToDateTime(ds.Tables[0].Rows[0]["SANCTIONDATE"].ToString());
           if(sanctiondt == DateTime.MinValue)
           {
               txtSanctionDate.Text = string.Empty;
           }
           else
           {
               txtSanctionDate.Text = ds.Tables[0].Rows[0]["SANCTIONDATE"].ToString(); 
           }
        ddlFundingAgency.SelectedValue = ds.Tables[0].Rows[0]["FUNDINGAGENCY"].ToString();
        
        HdnAcc.Value = ds.Tables[0].Rows[0]["PARTY_NO"].ToString();
        if (ds.Tables[0].Rows[0]["PARTY_NAME"].ToString() != string.Empty && ds.Tables[0].Rows[0]["PARTY_NAME"].ToString() != "*")
        {
            txtAgainstAcc.Text = ds.Tables[0].Rows[0]["PARTY_NAME"].ToString();
        }

        ViewState["ProjectId"] = e.CommandArgument.ToString();
    }

    protected void lstSubProj_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        DataSet ds = objCommon.FillDropDown("Acc_" + Session["comp_code"].ToString() + "_ProjectSubHead A LEFT JOIN Acc_" + Session["comp_code"].ToString() + "_PARTY B ON(A.Party_No = B.PARTY_NO)", "ProjectSubHeadShort", "ProjectSubHeadName,Res_Type,isnull(ProjectId,0)ProjectId,isnull(HeadType_id,0)HeadType_id,isnull(A.Party_No,0)Party_No,Isnull(B.PARTY_NAME,'')+'*'+ Isnull(B.ACC_CODE,'')PARTY_NAME", "A.ProjectSubId=" + e.CommandArgument.ToString(), "");
        txtProjShortName.Text = ds.Tables[0].Rows[0]["ProjectSubHeadShort"].ToString();
        txtSponsorProj.Text = ds.Tables[0].Rows[0]["ProjectSubHeadName"].ToString();
        ddlProject.SelectedValue = ds.Tables[0].Rows[0]["ProjectId"].ToString();
        ddlResType.SelectedValue = ds.Tables[0].Rows[0]["HeadType_id"].ToString();
        hdnOpartyManual.Value = ds.Tables[0].Rows[0]["Party_No"].ToString();
        if (ds.Tables[0].Rows[0]["PARTY_NAME"].ToString() != string.Empty && ds.Tables[0].Rows[0]["PARTY_NAME"].ToString() != "*")
        {
            txtAcc.Text = ds.Tables[0].Rows[0]["PARTY_NAME"].ToString();
        }

        if (ddlProject.SelectedIndex > 0)
        {
            ddlProject.Enabled = false;
        }
        else
        {
            ddlProject.Enabled = true;
        }

        if (ddlResType.SelectedIndex > 0)
        {
            ddlResType.Enabled = false;
        }
        else
        {
            ddlResType.Enabled = true;
        }

        ViewState["ProjectSubId"] = e.CommandArgument;


    }

    protected void lvSubProjDetail_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        DataSet ds = objCommon.FillDropDown("Acc_" + Session["comp_code"].ToString() + "_ProjectAllocation", "ProjectId,ProjectSubId,TotAmtReceived", "TotAmtSpent,TotAmtRemain", "ProjectSubHeadAllocationId=" + e.CommandArgument.ToString(), "");
        ddlProjectName.SelectedValue = ds.Tables[0].Rows[0]["ProjectId"].ToString();
        ddlProjectSubHead.SelectedValue = ds.Tables[0].Rows[0]["ProjectSubId"].ToString();
        // txtReceived.Text = ds.Tables[0].Rows[0]["TotAmtReceived"].ToString();
        // txttotSpent.Text = ds.Tables[0].Rows[0]["TotAmtSpent"].ToString();
        // txtReceivedAmt.Text = ds.Tables[0].Rows[0]["TotAmtRemain"].ToString();
        ViewState["ProjectSubHeadAllocationId"] = e.CommandArgument;

        txttotSpent.Text = objCommon.LookUp("Acc_" + Session["comp_code"].ToString() + "_Trans", "SUM((CASE WHEN [TRAN] = 'Dr' THEN isnULL(AMOUNT,0) WHEN [TRAN] = 'Cr' THEN -isnull(AMOUNT,0) END)) AS AMOUNT", "ProjectId = " + ddlProjectName.SelectedValue + " AND ProjectSubId = " + ddlProjectSubHead.SelectedValue);

        //DataSet ds = objCommon.FillDropDown("Acc_" + Session["comp_code"].ToString() + "_ProjectAllocation", "ProjectId,ProjectSubId,TotAmtReceived", "TotAmtSpent,TotAmtRemain", "ProjectId = " + ddlProjectName.SelectedValue + " AND ProjectSubId = " + ddlProjectSubHead.SelectedValue, "");

        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            txtReceived.Text = ds.Tables[0].Rows[0]["TotAmtReceived"].ToString();
            ViewState["ProjectSubId"] = ds.Tables[0].Rows[0]["ProjectSubId"].ToString();
            //ViewState["ProjectSubHeadAllocationId"] = ds.Tables[0].Rows[0]["ProjectSubId"].ToString();
            if (txttotSpent.Text != "")
            {
                txtReceivedAmt.Text = (Convert.ToDouble(ds.Tables[0].Rows[0]["TotAmtReceived"].ToString()) - Convert.ToDouble(txttotSpent.Text)).ToString();
            }
            else
            {
                txtReceivedAmt.Text = ds.Tables[0].Rows[0]["TotAmtReceived"].ToString();
                txttotSpent.Text = "0.00";
            }
        }

    }

    protected void ddlProjectName_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlProjectSubHead.SelectedIndex != 0)
        {
            txttotSpent.Text = objCommon.LookUp("Acc_" + Session["comp_code"].ToString() + "_Trans", "SUM((CASE WHEN [TRAN] = 'Dr' THEN isnULL(AMOUNT,0) WHEN [TRAN] = 'Cr' THEN -isnull(AMOUNT,0) END)) AS AMOUNT", "ProjectId = " + ddlProjectName.SelectedValue + " AND ProjectSubId = " + ddlProjectSubHead.SelectedValue);
            DataSet ds = objCommon.FillDropDown("Acc_" + Session["comp_code"].ToString() + "_ProjectAllocation", "ProjectId,ProjectSubId,TotAmtReceived", "TotAmtSpent,TotAmtRemain", "ProjectId = " + ddlProjectName.SelectedValue + " AND ProjectSubId = " + ddlProjectSubHead.SelectedValue, "");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                txtReceived.Text = ds.Tables[0].Rows[0]["TotAmtReceived"].ToString();

            }
        }
    }

    protected void ddlProjectSubHead_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlProjectName.SelectedIndex != 0)
        {
            txttotSpent.Text = objCommon.LookUp("Acc_" + Session["comp_code"].ToString() + "_Trans", "SUM((CASE WHEN [TRAN] = 'Dr' THEN isnULL(AMOUNT,0) WHEN [TRAN] = 'Cr' THEN -isnull(AMOUNT,0) END)) AS AMOUNT", "ProjectId = " + ddlProjectName.SelectedValue + " AND ProjectSubId = " + ddlProjectSubHead.SelectedValue);

            DataSet ds = objCommon.FillDropDown("Acc_" + Session["comp_code"].ToString() + "_ProjectAllocation", "ProjectSubHeadAllocationId,ProjectId,ProjectSubId,TotAmtReceived", "TotAmtSpent,TotAmtRemain", "ProjectId = " + ddlProjectName.SelectedValue + " AND ProjectSubId = " + ddlProjectSubHead.SelectedValue, "");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                txtReceived.Text = ds.Tables[0].Rows[0]["TotAmtReceived"].ToString();
                ViewState["ProjectSubHeadAllocationId"] = ds.Tables[0].Rows[0]["ProjectSubHeadAllocationId"].ToString();
                if (txttotSpent.Text != "")
                {
                    txtReceivedAmt.Text = (Convert.ToDouble(ds.Tables[0].Rows[0]["TotAmtReceived"].ToString()) - Convert.ToDouble(txttotSpent.Text)).ToString();
                }
                else
                {
                    txtReceivedAmt.Text = ds.Tables[0].Rows[0]["TotAmtReceived"].ToString();
                    txttotSpent.Text = "0.00";
                }
            }
        }
    }

    protected void txtAcc_TextChanged(object sender, EventArgs e)
    {
        string[] Party = txtAcc.Text.Trim().Split('*');
        if (Party.Length > 1)
        {
            hdnOpartyManual.Value = objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_PARTY", "PARTY_NO", "ACC_CODE='" + Party[1] + "'");
        }
    }

    //Fill AutoComplete Against Account Textbox
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetAccount(string prefixText)
    {
        List<string> Ledger = new List<string>();
        DataSet ds = new DataSet();
        try
        {
            prefixText = prefixText.ToUpper();
            AutoCompleteController objAutocomplete = new AutoCompleteController();
            ds = objAutocomplete.GetAccountEntryCashBank(prefixText);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Ledger.Add(ds.Tables[0].Rows[i]["PARTY_NAME"].ToString());
            }
        }
        catch (Exception ex)
        {
            ds.Dispose();
        }
        return Ledger;
    }

    protected void ddlProject_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlProject.SelectedIndex > 0 && ddlResType.SelectedIndex > 0)
        {
            string ShortName = objCommon.LookUp("Acc_" + Session["comp_code"].ToString() + "_Project", "ProjectShortName", "ProjectId='" + ddlProject.SelectedValue + "'");
            string HeadSrno = objCommon.LookUp("PROJECT_HEADTYPE", "SRNO", "ID='" + ddlResType.SelectedValue + "'");
            if (ShortName != string.Empty && HeadSrno != string.Empty)
            {
                string count = objCommon.LookUp("Acc_" + Session["comp_code"].ToString() + "_ProjectSubHead", "top 1 SrNo", "ProjectId='" + ddlProject.SelectedValue + "' AND HeadType_id='" + ddlResType.SelectedValue + "' ORDER BY ProjectSubId DESC");
                if (count != string.Empty)
                {
                    txtProjShortName.Text = ShortName + "." + HeadSrno + "." + (Convert.ToInt32(count) + 1);
                    //count = 1;
                    //txtProjShortName.Text = ShortName + "." + HeadSrno + "." + Convert.ToString(count);
                }
                else
                {
                    //txtProjShortName.Text = ShortName + "." + HeadSrno + "." + Convert.ToString(count+1);
                    txtProjShortName.Text = ShortName + "." + HeadSrno + "." + 1;
                }
            }

        }
        else
        {
            txtProjShortName.Text = string.Empty;

        }
    }

    protected void ddlResType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlProject.SelectedIndex > 0 && ddlResType.SelectedIndex > 0)
        {
            string ShortName = objCommon.LookUp("Acc_" + Session["comp_code"].ToString() + "_Project", "ProjectShortName", "ProjectId='" + ddlProject.SelectedValue + "'");
            string HeadSrno = objCommon.LookUp("PROJECT_HEADTYPE", "SRNO", "ID='" + ddlResType.SelectedValue + "'");
            if (ShortName != string.Empty && HeadSrno != string.Empty)
            {
                string count = objCommon.LookUp("Acc_" + Session["comp_code"].ToString() + "_ProjectSubHead", "top 1 SrNo", "ProjectId='" + ddlProject.SelectedValue + "' AND HeadType_id='" + ddlResType.SelectedValue + "' ORDER BY ProjectSubId DESC");
                if (count != string.Empty)
                {
                    txtProjShortName.Text = ShortName + "." + HeadSrno + "." + (Convert.ToInt32(count) + 1);
                    //count = 1;
                    //txtProjShortName.Text = ShortName + "." + HeadSrno + "." + Convert.ToString(count);
                }
                else
                {
                    //txtProjShortName.Text = ShortName + "." + HeadSrno + "." + Convert.ToString(count+1);
                    txtProjShortName.Text = ShortName + "." + HeadSrno + "." + 1;
                }
            }
        }
        else
        {
            txtProjShortName.Text = string.Empty;

        }
    }
    protected void txtAgainstAcc_TextChanged(object sender, EventArgs e)
    {
        HdnAcc.Value = txtAgainstAcc.Text.ToString().Split('*')[1].ToString();    //[0]
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetAgainstAcc(string prefixText)
    {
        List<string> Ledger = new List<string>();
        DataSet ds = new DataSet();
        try
        {
            prefixText = prefixText.ToUpper();
            AutoCompleteController objAutocomplete = new AutoCompleteController();
            ds = objAutocomplete.GetAgainstAccLedger(prefixText);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Ledger.Add(ds.Tables[0].Rows[i]["PARTY_NAME"].ToString());
            }
        }
        catch (Exception ex)
        {
            ds.Dispose();
        }
        return Ledger;
    }

    protected void txtStartDate_TextChanged(object sender, EventArgs e)
    {
        int val = Convert.ToInt32(txtProjectDuration.Text);
        DateTime StartDate = Convert.ToDateTime(txtStartDate.Text);
        DateTime endDate = StartDate.AddMonths(val).AddDays(-1);
        txtEndDate.Text = endDate.ToString("dd/MM/yyyy");

    }
}