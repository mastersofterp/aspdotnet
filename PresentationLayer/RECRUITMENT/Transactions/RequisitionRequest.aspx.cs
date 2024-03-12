using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;
using System.Web.UI.HtmlControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class RECRUITMENT_Transactions_RequisitionRequest : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    RequisitionController objReq = new RequisitionController();
    Requisition objRequisition = new Requisition();

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
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                Page.Title = Session["coll_name"].ToString();
                //CheckPageAuthorization();

                pnlAdd.Visible = false;
                pnlbtn.Visible = false;
                pnlList.Visible = true;
                FillDepartment();
                BindLVRequisitionReqStatus();
            }
            txtDescription.Attributes.Add("maxlength", txtDescription.MaxLength.ToString());
            txtReqAppLvl.Attributes.Add("maxlength", txtReqAppLvl.MaxLength.ToString());
        }
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=RequisitionUser.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=RequisitionUser.aspx");
        }
    }
    private void FillDepartment()
    {
        try
        {
            objCommon.FillDropDownList(ddlDepartment, "PAYROLL_EMPMAS E INNER JOIN PAYROLL_SUBDEPT DEPT ON(DEPT.SUBDEPTNO=E.SUBDEPTNO)", "DISTINCT E.SUBDEPTNO", "DEPT.SUBDEPT", "E.SUBDEPTNO <> 0 AND E.COLLEGE_CODE =" + Convert.ToInt32(Session["colcode"]) + " ", "DEPT.SUBDEPT");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "RECRUITMENT_Master_RequisitionUser.FillDepartment ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void FillPost()
    {
        try
        {
            int deptNo = Convert.ToInt32(ddlDepartment.SelectedValue);
            DataSet ds = objReq.GetPosts(deptNo);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlPost.DataSource = ds;
                ddlPost.DataTextField = "sPostCode";
                ddlPost.DataValueField = "nId";
                ddlPost.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "RECRUITMENT_Master_RequisitionUser.FillPost ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        btnSave.Enabled = true;
        pnlAdd.Visible = true;
        pnlList.Visible = false;
        pnlbtn.Visible = true;
        ViewState["action"] = "add";
        FillRequisitionNo();
        pnlRequiReqList.Visible = false;
        GetPAPath1();
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Clear();
        pnlAdd.Visible = false;
        pnlList.Visible = true;
        pnlbtn.Visible = false;
        pnlRequiReqList.Visible = true;
    }
    private void FillRequisitionNo()
    {
        try
        {
            int RequNo = Convert.ToInt32(objCommon.LookUp("REC_REQUISITION_REQUEST", "ISNULL(MAX(REQ_ID),0) AS REQ_ID", ""));
            string collegeshort;
            string collegeno = Session["college_nos"].ToString();
            string[] values = collegeno.Split(',');
            if (values.Length > 1)
            {
                collegeshort = objCommon.LookUp("reff", "ISNULL(CODE_STANDARD,'') AS CODE_STANDARD", "");
            }
            else
            {
                collegeshort = objCommon.LookUp("acd_college_master", "ISNULL(SHORT_NAME,'') AS SHORT_NAME", "COLLEGE_ID=" + Session["college_nos"].ToString() + " ");
            }
            string year = DateTime.Now.Year.ToString();
            RequNo++;
            string ReqNo = RequNo.ToString().PadLeft(4, '0');
            txtReqNo.Text = collegeshort + "/" + year + "/" + ReqNo;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "RECRUITMENT_Transactions_RequisitionRequest.FillRequisitionNo ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");

        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string requno = objCommon.LookUp("REC_REQUISITION_USER", "ISNULL(REQUNO,'') AS REQUNO ", "UA_NO=" + Convert.ToInt32(Session["userno"].ToString()));
            if (requno == string.Empty)
            {
                objCommon.DisplayMessage(this.updPanel, "Sorry!! you are not authorised user", this.Page);
                return;
            }
            objRequisition.REQUISITION_NO = txtReqNo.Text.Trim();
            objRequisition.DEPTNO = Convert.ToInt32(ddlDepartment.SelectedValue);
            objRequisition.POSTCATNO = Convert.ToInt32(ddlpostType.SelectedValue);
            objRequisition.POSTNO = ddlPost.SelectedValue;
            objRequisition.POST = ddlPost.SelectedItem.Text;
            //if (requno != 0)
            //{
            //    objRequisition.REQUNO = requno;
            //}
            objRequisition.REQUNO = Convert.ToInt32(Session["userno"].ToString());
            objRequisition.NO_OF_POSITION = Convert.ToInt32(txtNoofPosition.Text.Trim());
            objRequisition.DESCRIPTION = txtDescription.Text.Trim();

            if (txtReqAppLvl.Text.Equals(string.Empty))
            {
                objRequisition.REQ_PANO = 0;
            }
            else
            {
                objRequisition.REQ_PANO = Convert.ToInt32(ViewState["REQ_PANO"]);
            }

            objRequisition.REQUEST_DATE = Convert.ToDateTime(DateTime.Now.Date);

            objRequisition.CREATEDBY = Convert.ToInt32(Session["userno"].ToString());

            objRequisition.COLLEGE_CODE = Session["colcode"].ToString();

            string collegeno = Session["college_nos"].ToString();
            string[] values = collegeno.Split(',');
            if (values.Length > 1)
            {
                objRequisition.COLLEGE_NO = 0;
            }
            else
            {
                objRequisition.COLLEGE_NO = Convert.ToInt32(Session["college_nos"].ToString());
            }
            objRequisition.APPROVAL_STATUS = "P";
            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    bool result = CheckRequisitionRequest();

                    if (result == true)
                    {
                        objCommon.DisplayMessage(this.updPanel, "Record Already Exist", this.Page);
                        Clear();
                        return;
                    }
                    else
                    {
                        CustomStatus cs = (CustomStatus)Convert.ToInt32(objReq.AddRequisitionRequest(objRequisition));

                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            pnlRequiReqList.Visible = true;
                            BindLVRequisitionReqStatus();
                            objCommon.DisplayMessage(this.updPanel, "Record Saved Successfully!", this.Page);
                        }
                        else
                            objCommon.DisplayMessage(this.updPanel, "Exception Occured", this.Page);

                        pnlAdd.Visible = false;
                        pnlList.Visible = true;
                        pnlbtn.Visible = false;
                        Clear();
                    }
                }
                else if (ViewState["action"].ToString().Equals("edit"))
                {

                    objRequisition.REQ_ID = Convert.ToInt32(ViewState["REQ_ID"]);

                    DataSet ds = objCommon.FillDropDown("REC_REQUISITION_REQUEST", "*", "", "DEPT_NO= '" + ddlDepartment.SelectedValue + "' and POSTTYPE_NO='" + ddlpostType.SelectedValue + "'and POST_NO ='" + ddlPost.SelectedValue + "' and  APPROVAL_STATUS = 'P' and REQUISITION_NO != '"+txtReqNo.Text+"'", "");
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        objCommon.DisplayMessage(this.updPanel, "Record Already Exist", this.Page);
                        return;
                    }

                    CustomStatus cs = (CustomStatus)objReq.UpdateRequisitionRequest(objRequisition);
                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        pnlRequiReqList.Visible = true;
                        BindLVRequisitionReqStatus();
                        objCommon.DisplayMessage(this.updPanel, "Record Updated Successfully", this.Page);
                    }
                    else
                    objCommon.DisplayMessage(this.updPanel, "Exception Occured", this.Page);
                    pnlAdd.Visible = false;
                    pnlList.Visible = true;
                    pnlbtn.Visible = false;
                    ViewState["action"] = null;
                    Clear();
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "RECRUITMENT_Transactions_RequisitionRequest.btnSave_Click ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");

        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }
    private void Clear()
    {
        //txtReqNo.Text = string.Empty;
        ddlDepartment.SelectedIndex = 0;
        ddlpostType.SelectedIndex = 0;
        ddlPost.SelectedIndex = 0;
        ddlPost.Items.Clear();
        ddlPost.Items.Add("Please Select");
        txtNoofPosition.Text = string.Empty;
        txtDescription.Text = string.Empty;
        //txtReqAppLvl.Text = string.Empty;

       // ViewState["action"] = "add";
    }

    public bool CheckRequisitionRequest()
    {
        bool result = false;
        DataSet ds = new DataSet();

        ds = objCommon.FillDropDown("REC_REQUISITION_REQUEST", "*", "", "REQUISITION_NO= '" + txtReqNo.Text + "' OR APPROVAL_STATUS IN ('P','F') AND DEPT_NO= '" + ddlDepartment.SelectedValue + "' and POSTTYPE_NO='" + ddlpostType.SelectedValue + "'and POST_NO ='" + ddlPost.SelectedValue + "'", "");
        if (ds.Tables[0].Rows.Count > 0)
        {
            result = true;

        }

        return result;
    }

    protected void GetPAPath1()
    {
        try
        {

            string path = string.Empty;
            string userno = Session["userno"].ToString();
            //int useridno = Convert.ToInt32(Session["idno"]);
            //string userno = "319";
            //int useridno = 3773;

            DataSet dspath = new DataSet();

            dspath = null;
            dspath = objCommon.FillDropDown("REC_REQUISITION_APPROVAL_LEVELS", "*", "", "UA_NO=" + userno + " ", "");

            if (dspath.Tables[0].Rows.Count > 0)
            {
                ViewState["REQ_PANO"] = dspath.Tables[0].Rows[0]["REQ_PANO"].ToString();


                string pano1 = dspath.Tables[0].Rows[0]["PAN01"].ToString();
                string pano2 = dspath.Tables[0].Rows[0]["PAN02"].ToString();
                string pano3 = dspath.Tables[0].Rows[0]["PAN03"].ToString();
                string pano4 = dspath.Tables[0].Rows[0]["PAN04"].ToString();
                string pano5 = dspath.Tables[0].Rows[0]["PAN05"].ToString();

                string paname1 = string.Empty;
                string paname2 = string.Empty;
                string paname3 = string.Empty;
                string paname4 = string.Empty;
                string paname5 = string.Empty;

                DataSet dsauth1 = objCommon.FillDropDown("USER_ACC", "UA_NO", "UA_FULLNAME", "UA_NO=" + pano1, "");
                if (dsauth1.Tables[0].Rows.Count > 0)
                {
                    paname1 = dsauth1.Tables[0].Rows[0]["UA_FULLNAME"].ToString();
                }

                DataSet dsauth2 = objCommon.FillDropDown("USER_ACC", "UA_NO", "UA_FULLNAME", "UA_NO=" + pano2, "");
                if (dsauth2.Tables[0].Rows.Count > 0)
                {
                    paname2 = dsauth2.Tables[0].Rows[0]["UA_FULLNAME"].ToString();
                }

                DataSet dsauth3 = objCommon.FillDropDown("USER_ACC", "UA_NO", "UA_FULLNAME", "UA_NO=" + pano3, "");
                if (dsauth3.Tables[0].Rows.Count > 0)
                {
                    paname3 = dsauth3.Tables[0].Rows[0]["UA_FULLNAME"].ToString();
                }

                DataSet dsauth4 = objCommon.FillDropDown("USER_ACC", "UA_NO", "UA_FULLNAME", "UA_NO=" + pano4, "");
                if (dsauth4.Tables[0].Rows.Count > 0)
                {
                    paname4 = dsauth4.Tables[0].Rows[0]["UA_FULLNAME"].ToString();
                }

                DataSet dsauth5 = objCommon.FillDropDown("USER_ACC", "UA_NO", "UA_FULLNAME", "UA_NO=" + pano5, "");
                if (dsauth5.Tables[0].Rows.Count > 0)
                {
                    paname5 = dsauth5.Tables[0].Rows[0]["UA_FULLNAME"].ToString();
                }


                ////-------------------------------------------///

                if (userno == pano1)
                {
                    path = paname2 + "->" + paname3 + "->" + paname4 + "->" + paname5;
                }
                else if (userno == pano2)
                {
                    path = paname3 + "->" + paname4 + "->" + paname5;
                }
                else if (userno == pano3)
                {
                    path = paname4 + "->" + paname5;
                }
                else if (userno == pano4)
                {
                    path = paname5;
                }
                else
                {
                    path = paname1 + "->" + paname2 + "->" + paname3 + "->" + paname4 + "->" + paname5;
                }
                txtReqAppLvl.Text = path;

            }
            else
            {
                //MessageBox("Sorry! Authority Not found");
                objCommon.DisplayMessage(this.updPanel, "Sorry! Requisition Approval Levels Not found!", this.Page);
                txtReqAppLvl.Text = string.Empty;
                btnSave.Enabled = false;
                return;
            }



        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "RECRUITMENT_Transactions_RequisitionRequest.GetPAPath ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");

        }

    }

    protected void BindLVRequisitionReqStatus()
    {
        try
        {
            string userno = Session["userno"].ToString();

            DataSet ds = objReq.GetRequisitionReqStatus(Convert.ToInt32(userno));
            lvRequiReq.DataSource = ds.Tables[0];
            lvRequiReq.DataBind();
            lvRequiReq.Visible = true;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "RECRUITMENT_Transactions_RequisitionRequest.BindLVRequisitionReqStatus ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }
    }


    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            int userno = Convert.ToInt32(Session["userno"].ToString());
            ImageButton btnEdit = sender as ImageButton;
            Int32 REQ_ID = int.Parse(btnEdit.CommandArgument);

            ViewState["REQ_ID"] = REQ_ID;

            DataSet ds = new DataSet();
            string status;
            ds = objCommon.FillDropDown("REC_REQUISITION_REQUEST_PASS", "*", "", "REQ_ID=" + REQ_ID, "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                status = ds.Tables[0].Rows[0]["STATUS"].ToString();

                if (status == "A")
                {
                    btnSave.Enabled = false;
                    objCommon.DisplayMessage(this.updPanel, "Requisition Request is Already Approved by authority, You cannot modify", this.Page);
                    return;
                }
                else
                {
                    btnSave.Enabled = true;
                }
            }
            ds = objCommon.FillDropDown("REC_REQUISITION_REQUEST", "*", "", "REQ_ID=" + REQ_ID, "");
            string statusnew = ds.Tables[0].Rows[0]["APPROVAL_STATUS"].ToString();
            if (statusnew == "A" || statusnew == "T")
            {
                btnSave.Enabled = false;
                objCommon.DisplayMessage(this.updPanel, "Requisition Request Is Already Approved By Authority, You Cannot Modify", this.Page);
                return;
            }
            else if (statusnew == "R")
            {
                btnSave.Enabled = false;
                objCommon.DisplayMessage(this.updPanel, "Requisition Request Is Already Rejected By Authority, You Cannot Modify", this.Page);
                return;

            }
            else
            {
                btnSave.Enabled = true;
            }
            ds = objCommon.FillDropDown("REC_REQUISITION_REQUEST_PASS", "STATUS", "REQ_PANO", "REQ_ID=" + REQ_ID + " ", "REQ_PANO");
            int total = ds.Tables[0].Rows.Count;
            for (int i = 0; i < total; i++)
            {
                //Code to avoid modification of record if 1st authority has approved leave (in case of more than 1 authority)
                status = ds.Tables[0].Rows[i]["STATUS"].ToString();
                if (status == "F")
                {
                    objCommon.DisplayMessage(this.updPanel, "Approval In Progress, Not Allow To Modify", this.Page);
                    return;
                }
            }

            ShowDetails(userno, REQ_ID);

            GetPAPath1();

            ViewState["action"] = "edit";
            pnlAdd.Visible = true;
            pnlList.Visible = false;
            pnlbtn.Visible = true;
            pnlRequiReqList.Visible = false;

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "RECRUITMENT_Transactions_RequisitionRequest.btnEdit_Click ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");

        }
    }

    protected void ShowDetails(int userno, int ActivityId)
    {
        DataSet ds = null;
        try
        {
            ds = objReq.GetRequisitionReqStatusById(userno, ActivityId);
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtReqNo.Text = ds.Tables[0].Rows[0]["REQUISITION_NO"].ToString();
                ddlDepartment.SelectedValue = ds.Tables[0].Rows[0]["DEPT_NO"].ToString();
                ddlpostType.SelectedValue = ds.Tables[0].Rows[0]["POSTTYPE_NO"].ToString();
                FillPost();
                ddlPost.SelectedValue = ds.Tables[0].Rows[0]["POST_NO"].ToString();
                txtNoofPosition.Text = ds.Tables[0].Rows[0]["NO_OF_POSITION"].ToString();
                txtDescription.Text = ds.Tables[0].Rows[0]["DESCRIPTION"].ToString();
            }
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "RECRUITMENT_Transactions_RequisitionRequest.ShowDetails ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");

        }
    }
    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnDelete = sender as ImageButton;
            Int32 REQ_ID = int.Parse(btnDelete.CommandArgument);
            DataSet ds = new DataSet();
            string status;
            ds = objCommon.FillDropDown("REC_REQUISITION_REQUEST_PASS", "*", "", "REQ_ID=" + REQ_ID, "");


            if (ds.Tables[0].Rows.Count > 0)
            {
                status = ds.Tables[0].Rows[0]["STATUS"].ToString();
                if (status == "A")
                {
                    btnSave.Enabled = false;
                    objCommon.DisplayMessage(this.updPanel, "Requisition Request Is Already Approved By Authority, You Cannot Delete", this.Page);
                    return;
                }
                else
                {
                    btnSave.Enabled = true;
                }
            }
            ds = objCommon.FillDropDown("REC_REQUISITION_REQUEST", "*", "", "REQ_ID=" + REQ_ID, "");
            string statusnew = ds.Tables[0].Rows[0]["APPROVAL_STATUS"].ToString();
            if (statusnew == "A" || statusnew == "T")
            {
                btnSave.Enabled = false;
                objCommon.DisplayMessage(this.updPanel, "Requisition Request Is Already Approved By Authority, You Cannot Delete", this.Page);
                return;
            }
            else if (statusnew == "R")
            {
                btnSave.Enabled = false;
                objCommon.DisplayMessage(this.updPanel, "Requisition Request Is Already Approved By Authority, You Cannot Delete", this.Page);
                return;

            }
            else
            {
                btnSave.Enabled = true;
            }

            ds = objCommon.FillDropDown("REC_REQUISITION_REQUEST_PASS", "STATUS", "REQ_PANO", "REQ_ID=" + REQ_ID + " ", "REQ_PANO");
            int total = ds.Tables[0].Rows.Count;
            for (int i = 0; i < total; i++)
            {
                //Code to avoid modification of record if 1st authority has approved leave (in case of more than 1 authority)
                status = ds.Tables[0].Rows[i]["STATUS"].ToString();
                if (status == "F")
                {
                    objCommon.DisplayMessage(this.updPanel, "Approval In Progress, Not Allow To Delete", this.Page);
                    return;
                }
            }

            CustomStatus cs = (CustomStatus)objReq.DeleteRequisitionRequest(REQ_ID);
            if (cs.Equals(CustomStatus.RecordDeleted))
            {
                ViewState["action"] = "add";
                objCommon.DisplayMessage(this.updPanel, "Record Deleted Successfully!", this.Page);
                pnlRequiReqList.Visible = true;
                BindLVRequisitionReqStatus();
            }
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "RECRUITMENT_Transactions_RequisitionRequest.btnDelete_Click ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");

        }
    }
    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillPost();
    }
}