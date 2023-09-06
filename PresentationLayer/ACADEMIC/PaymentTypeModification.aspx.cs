//======================================================================================
// PROJECT NAME  : UAIMS
// MODULE NAME   : ACADEMIC
// PAGE NAME     : PAYMENTTYPE_MODIFICATION 
// CREATION DATE : 9-AUG-2019
// CREATED BY    : RITA MUNDE
// MODIFIED DATE :
// MODIFIED DESC :
//======================================================================================

using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using System.IO;

public partial class ACADEMIC_PaymentTypeModification : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentController objSC = new StudentController();

    #region PAGE
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
            ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
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
                    Page.Title = Session["coll_name"].ToString();
                    //Load Page Help

                    //Page Authorization
                    this.CheckPageAuthorization();

                    if (Request.QueryString["pageno"] != null)
                    {
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                    objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER WITH (NOLOCK)", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME", "COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]) + " AND ActiveStatus=1", "COLLEGE_ID");
                    objCommon.FillDropDownList(ddlRecType, "ACD_RECIEPT_TYPE WITH (NOLOCK)", "RECIEPT_CODE", "RECIEPT_TITLE", string.Empty, "RECIEPT_TITLE");
                    //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1", "SESSIONNO DESC");               

                }
            }
            divMsg.InnerHtml = string.Empty;
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    #endregion

    #region PRIVATE METHOD
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=PaymentTypeModification.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=PaymentTypeModification.aspx");
        }
    }
    #endregion

    #region EVENTS
    protected void btnShow_Click(object sender, EventArgs e)
    {
        BindListView();

        int idnocount = 0;
        int logcount = 0;
        int payno = 0;
        foreach (ListViewDataItem lvItem in lvstudList.Items)
        {
            HiddenField hdpay = (HiddenField)lvItem.FindControl("hdpay");
            HiddenField hdnIDNO = (HiddenField)lvItem.FindControl("hdnIDNO");
            idnocount = Convert.ToInt32(hdnIDNO.Value);

        }

        logcount = Convert.ToInt32(objCommon.LookUp("ACAD_PAYMENTTYPE_LOG WITH (NOLOCK)", "ISNULL(COUNT(IDNO),0)COUNT", "IDNO=" + idnocount));

        if (logcount > 0)
        {
            Panel2.Visible = false;
            btnSubmit.Visible = false;
            divUpdatePayType.Visible = false;
            // ShowMessage("Branch change already done for filtered student.");
            lblMsg.ForeColor = System.Drawing.Color.Red;
            lblMsg.Text = "ALERT: Payment Modification already done for filtered student.";
            return;
        }
    }

    public void BindListView()
    {
        try
        {
            int idnocount = 0;
            int logcount = 0;
            int payno = 0;
            // Gtet List Of Student Data..........
            DataSet ds = objCommon.GetListOfStudents(txtAppID.Text.Trim(), Convert.ToInt32(ddlSession.SelectedValue));
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                btnSubmit.Enabled = true;

                lvstudList.DataSource = ds.Tables[0];
                lvstudList.DataBind();
                objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvstudList);//Set label - 
                lvstudList.Visible = true;
                Panel2.Visible = true;
                divUpdatePayType.Visible = true;
                lblMsg.Text = "";

                foreach (ListViewDataItem lvItem in lvstudList.Items)
                {
                    Label lblpay = (Label)lvItem.FindControl("lblPayType");
                    payno = Convert.ToInt32(lblpay.ToolTip);

                }
                //Fill Drop-Down List for Payment type.......
                objCommon.FillDropDownList(ddlPayType, "ACD_PAYMENTTYPE WITH (NOLOCK)", "PAYTYPENO", "PAYTYPENAME", "PAYTYPENO > 0 AND PAYTYPENO <>" + payno + "", "PAYTYPENO ASC");
                //  objCommon.FillDropDownList(ddlPayType, "ACD_PAYMENTTYPE", "PAYTYPENO", "PAYTYPENAME", "PAYTYPENO > 0", "PAYTYPENO DESC");
                objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER WITH (NOLOCK)", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO > 0", "SEMESTERNO ASC");
                // BindddlPaymentType();
            }
            else
            {
                objCommon.DisplayMessage("Record Not Found", this.Page);
                lvstudList.DataSource = null;
                lvstudList.DataBind();
                lvstudList.Visible = false;
                btnSubmit.Enabled = false;
                txtAppID.Text = "";
            }        
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void BindddlPaymentType()
    {
        DataSet ds = objCommon.FillDropDown("ACD_PAYMENTTYPE WITH (NOLOCK)", "PAYTYPENO", "PAYTYPENAME", "PAYTYPENO>0", "PAYTYPENO ASC");
        foreach (ListViewDataItem lvItem in lvstudList.Items)
        {
            DropDownList ddlpaymenttype = (DropDownList)lvItem.FindControl("ddlpaymenttype");
            ddlpaymenttype.Items.Clear();
            ddlpaymenttype.Items.Add("Please Select");
            ddlpaymenttype.SelectedItem.Value = "0";

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlpaymenttype.DataSource = ds.Tables[0];
                ddlpaymenttype.DataTextField = "PAYTYPENAME";
                ddlpaymenttype.DataValueField = "PAYTYPENO";
                ddlpaymenttype.DataBind();
                //ddlpaymenttype.Items.Insert(0, "Please Select");
            }
            ddlpaymenttype.SelectedIndex = 0;

            HiddenField hdpay = (HiddenField)lvItem.FindControl("hdpay");
            if (hdpay.Value != "0")
            {
                ddlpaymenttype.SelectedValue = hdpay.Value.ToString();
            }
        }
    }

    protected void lvstudList_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
      
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
            //if (ddlPayType.SelectedIndex == 0 || ddlSemester.SelectedIndex == 0 || txtRemark.Text == string.Empty || chkOverwrite.Checked == false)
            //{
            //    ShowMessage("Please select Payment Type/Semester/Remark and Please Checked Cancel Existing Demand ");
            //}

            string Name = string.Empty;
            string PaymentType = string.Empty;
            string USERNAME = string.Empty;
            string IDNO = string.Empty;
            int colgId = 0;
             bool Demand_Overwrite = false;
            int Demand_Count = 0;
            CustomStatus cs = new CustomStatus();

            
            foreach (ListViewDataItem lvItem in lvstudList.Items)
            {
                DropDownList ddlpaymenttype = (DropDownList)lvItem.FindControl("ddlpaymenttype");
                HiddenField hdpay = (HiddenField)lvItem.FindControl("hdpay");
                HiddenField hdnIDNO = (HiddenField)lvItem.FindControl("hdnIDNO");
                IDNO = hdnIDNO.Value;
               
            }
            if (IDNO == string.Empty)
            {
                objCommon.DisplayMessage("Please Select Payment Type", Page); return;
            }
            else
            {
               

                //Added by Rita M....................
                DemandModificationController dmController = new DemandModificationController();
                FeeDemand demandCriteria = new FeeDemand();
                DataSet newFees = null;
                foreach (ListViewDataItem lvItem in lvstudList.Items)
                {
                    Label lblbranch = (Label)lvItem.FindControl("lblbranch");
                    Label lbldegree = (Label)lvItem.FindControl("lbldegree");
                    //Colege Id............
                    colgId = Convert.ToInt32(objCommon.LookUp("ACD_COLLEGE_DEGREE_BRANCH WITH (NOLOCK)", "COLLEGE_ID", " BRANCHNO=" + Convert.ToInt32(lblbranch.ToolTip) + " AND DEGREENO=" + Convert.ToInt32(lbldegree.ToolTip) + ""));

                     newFees = objCommon.FillDropDown("ACD_DEMAND WITH (NOLOCK)", "*", string.Empty, "IDNO=" + Convert.ToInt32(IDNO) + " AND BRANCHNO=" + Convert.ToInt32(lblbranch.ToolTip) + " AND DEGREENO=" + Convert.ToInt32(lbldegree.ToolTip) + " AND SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + "", string.Empty);
                    if (newFees != null && newFees.Tables[0].Rows.Count > 0)
                    {
                        demandCriteria.SessionNo = Convert.ToInt32(newFees.Tables[0].Rows[0]["SESSIONNO"]);
                        demandCriteria.ReceiptTypeCode = newFees.Tables[0].Rows[0]["RECIEPT_CODE"].ToString();
                        demandCriteria.BranchNo = Convert.ToInt32(newFees.Tables[0].Rows[0]["BRANCHNO"]);
                        demandCriteria.SemesterNo = Convert.ToInt32(newFees.Tables[0].Rows[0]["SEMESTERNO"]);
                        demandCriteria.PaymentTypeNo = Convert.ToInt32(newFees.Tables[0].Rows[0]["PAYTYPENO"]);
                        demandCriteria.UserNo = Convert.ToInt32(newFees.Tables[0].Rows[0]["UA_NO"]);
                        demandCriteria.CollegeCode = newFees.Tables[0].Rows[0]["COLLEGE_CODE"].ToString();
                        demandCriteria.DegreeNo = Convert.ToInt32(newFees.Tables[0].Rows[0]["DEGREENO"]);
                        demandCriteria.AdmBatchNo = Convert.ToInt32(newFees.Tables[0].Rows[0]["ADMBATCHNO"]);
                    }
                }
                if (newFees == null || newFees.Tables[0].Rows.Count == 0)
                    {
                    objCommon.DisplayMessage("No Demand or standanrd fees found for the given selection!", this.Page);
                    return;
                    }
                //For Demand Overwrite.......
                //Demand_Count = Convert.ToInt32(objCommon.LookUp("ACD_DEMAND", "COUNT(1) DEMAND_COUNT", " SESSIONNO=" + demandCriteria.SessionNo + "  AND RECIEPT_CODE='" + demandCriteria.ReceiptTypeCode + "'  AND BRANCHNO = " + demandCriteria.BranchNo + "    AND DEGREENO = " + demandCriteria.DegreeNo + " AND SEMESTERNO=" + demandCriteria.SemesterNo + " AND PAYTYPENO = " + Convert.ToInt32(ddlPayType.SelectedValue) + " AND UA_NO=" + demandCriteria.UserNo + "  AND IDNO =" + Convert.ToInt32(IDNO)));
                //if (Demand_Count > 0)
                //{
                //    Demand_Overwrite = true;
                //}
                //else
                //{
                //    Demand_Overwrite = false;
                //}
                //---------

                // Create a demand for New Payment type.............Cancel Existing demand......
                string response = dmController.CreateDemandForSelectedStudents_ForPaymentModification(IDNO, demandCriteria, Convert.ToInt32(ddlSemester.SelectedValue), Demand_Overwrite, Convert.ToInt32(colgId),Convert.ToInt32(ddlPayType.SelectedValue));


                if (response != "-99")
                {
                    if (response == "1")
                    {
                        //Update payment type in Student table.....
                        cs = (CustomStatus)objCommon.UpdatePaymentType(ddlPayType.SelectedValue, Convert.ToString(IDNO), txtRemark.Text, Convert.ToInt32(Session["userno"]), ViewState["ipAddress"].ToString());

                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            objCommon.DisplayMessage("Payment Type Modification Done Successfully.Demand sucessfully created For New Payment Type!", this.Page);
                            ddlPayType.SelectedIndex = 0;
                            ddlSemester.SelectedIndex = 0;
                            ddlSession.SelectedIndex = 0;
                            ddlSemester.SelectedIndex = 0;
                            ddlRecType.SelectedIndex = 0;
                            txtAppID.Text = "";
                            txtRemark.Text = "";
                            lblMsg.Text = "";
                            Panel2.Visible = false;
                            divUpdatePayType.Visible = false;
                            btnSubmit.Enabled = false;
                            lvstudList.DataSource = null;
                            lvstudList.DataBind();
                            lvstudList.Visible = false;
                        }
                        else
                        {
                            objCommon.DisplayMessage("Failed To Modify Payment Type!", this.Page);
                            return;
                        }
                    }
                }
            }
            //BindListView();
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    #endregion
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCollege.SelectedIndex > 0)
        {
            this.objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND COLLEGE_ID = " + Convert.ToInt32(ddlCollege.SelectedValue) + " AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "SESSIONNO DESC");
            //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1", "SESSIONNO DESC");
            ddlSession.Focus();
        }
        else
        {
            ddlSession.Items.Clear();
            ddlSession.Items.Add(new ListItem("Please Select", "0"));
        }
    }
}