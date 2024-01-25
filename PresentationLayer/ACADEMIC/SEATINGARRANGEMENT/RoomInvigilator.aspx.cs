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

using System.Collections.Generic;

public partial class ACADEMIC_MASTERS_Roominvigilator : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    SeatingArrangementController objExamController = new SeatingArrangementController();

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
    #region pageload

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
                //This checks the authorization of the user.
                //  CheckPageAuthorization();

                Page.Title = Session["coll_name"].ToString();

                if (Request.QueryString["pageno"] != null)
                {
                    // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }

                // Set form mode equals to -1(New Mode).
                ViewState["exdtno"] = "0";
                //SetInitialRow();
                this.PopulateDropDown();
                divMsg.InnerHtml = string.Empty;
                ViewState["roomname"] = string.Empty;
                //this.BindRooms();

            }
            //int collegecode = Convert.ToInt16(objCommon.LookUp("ACD_DEPARTMENT", "DISTINCT COLLEGE_CODE", "DEPTNO = " + Convert.ToInt32(ddlDept.SelectedValue)));
            objCommon.SetLabelData("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]));//Set label -
            //objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));//Header
        }


    }


    private void PopulateDropDown()
    {
        try
        {

            //Degree Name
            objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME AS COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_NAME ASC");

            //For filling the Number of Floors
            //objCommon.FillDropDownList(ddlFloorNo, "ACD_FLOOR", "FLOORNO", "FLOORNAME", "FLOORNO > 0", "FLOORNO DESC");
            ddlCollege.Focus();

            //}
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_MASTERS_ExamDate.PopulateDropDown --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            //if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString()) == false)
            //{
            //    Response.Redirect("~/notauthorized.aspx?page=SessionCreate.aspx");
            //}
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=SessionCreate.aspx");
        }
    }

    #endregion



    #region Submit

    #endregion



    #region BindRoom Show Deatils

    private void BindRooms()
    {
        try
        {
            //string sp_procedure = "PKG_GET_SP_ROOMS_INVIGILATOR";
            //string sp_parameters = "@P_FLOOR_NO,@P_BLOCK_NO";
            //string sp_callValues = "" + Convert.ToInt32(ddlFloorNo.SelectedValue) + "," + Convert.ToInt32(ddlBlockNo.SelectedValue) + "";
            //DataSet ds = objCommon.DynamicSPCall_Select(sp_procedure, sp_parameters, sp_callValues);


            SeatingArrangementController objSC = new SeatingArrangementController();
            DataSet ds = objSC.GetAllRooms(Convert.ToInt32(ddlCollege.SelectedValue) == 0 ? 0 : Convert.ToInt32(ddlCollege.SelectedValue), Convert.ToInt32(ddlDept.SelectedValue) == 0 ? 0 : Convert.ToInt32(ddlDept.SelectedValue), Convert.ToInt32(ddlFloorNo.SelectedValue) == 0 ? 0 : Convert.ToInt32(ddlFloorNo.SelectedValue), Convert.ToInt32(ddlBlockNo.SelectedValue) == 0 ? 0 : Convert.ToInt32(ddlBlockNo.SelectedValue));
            lvRoomMaster.DataSource = ds;
            lvRoomMaster.DataBind();

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {

                    lvRoomMaster.DataSource = ds;
                    lvRoomMaster.DataBind();
                }
                else
                {
                    objCommon.DisplayMessage(this, "Data Not Found..!!", this.Page);
                }
            }
            else
            {
                objCommon.DisplayMessage(this, "Data Not Found..!!", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_AcademicCalenderMaster.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.DisplayMessage(updRoom, "Server UnAvailable", this.Page);
        }
    }


    #endregion


    #region clear

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    private void ClearControls()
    {
        ddlCollege.SelectedIndex = 0;
        ddlDept.SelectedIndex = 0;
        ddlFloorNo.SelectedIndex = 0;
        ddlBlockNo.SelectedIndex = 0;
    }
    #endregion


    #region Dept

    protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDept.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlFloorNo, "ACD_FLOOR ", "FLOORNO", "FLOORNAME", "FLOORNO > 0 AND ACTIVESTATUS=1 ", "FLOORNO ASC");

            ddlFloorNo.Focus();
            int collegecode = Convert.ToInt32(objCommon.LookUp("ACD_DEPARTMENT", "DISTINCT COLLEGE_CODE", "DEPTNO = " + Convert.ToInt32(ddlDept.SelectedValue)));
            ViewState["CollegeCode"] = collegecode;

        }
        else
        {

            ddlDept.SelectedIndex = 0;
            ddlFloorNo.Items.Clear();
            ddlFloorNo.Items.Add(new ListItem("Please Select", "0"));
            ddlBlockNo.Items.Clear();
            ddlBlockNo.Items.Add(new ListItem("Please Select", "0"));
        }

        ddlFloorNo.SelectedIndex = 0;
        ddlBlockNo.SelectedIndex = 0;
        lvRoomMaster.DataSource = null;
        lvRoomMaster.DataBind();


    }
    #endregion

    protected void ddlBlock_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlBlockNo.SelectedIndex > 0)
            {
                this.BindRooms();
            }
            else
            {

            }
        }
        catch (Exception ex)
        {
        }

    }
    protected void ddlFloorNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlFloorNo.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlBlockNo, "ACD_BLOCK ", "BLOCKNO", "BLOCKNAME", "BLOCKNO > 0 AND ACTIVESTATUS=1", "BLOCKNAME ASC");
            ddlBlockNo.Focus();

        }
        else
        {
            ddlFloorNo.SelectedIndex = 0;
            ddlBlockNo.Items.Clear();
            ddlBlockNo.Items.Add(new ListItem("Please Select", "0"));
        }

        ddlBlockNo.SelectedIndex = 0;
        lvRoomMaster.DataSource = null;
        lvRoomMaster.DataBind();

    }
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Branch Name
        if (ddlCollege.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlDept, "ACD_DEPARTMENT D, ACD_COLLEGE_DEPT C ", "D.DEPTNO", "D.DEPTNAME", "D.DEPTNO=C.DEPTNO AND C.DEPTNO >0 AND C.COLLEGE_ID=" + ddlCollege.SelectedValue + "", "DEPTNAME");
            ddlDept.Focus();
        }
        else
        {
            ddlCollege.SelectedIndex = 0;
            ddlDept.Items.Clear();
            ddlDept.Items.Add(new ListItem("Please Select", "0"));
            ddlFloorNo.Items.Clear();
            ddlFloorNo.Items.Add(new ListItem("Please Select", "0"));
            ddlBlockNo.Items.Clear();
            ddlBlockNo.Items.Add(new ListItem("Please Select", "0"));
            
        }
        ddlDept.SelectedIndex = 0;
        ddlFloorNo.SelectedIndex = 0;
        ddlBlockNo.SelectedIndex = 0;

        lvRoomMaster.DataSource = null;
        lvRoomMaster.DataBind();

    }
    protected void ddlBlockNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.BindRooms();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int rowIndex = 0;
            int Count = 0;
            CustomStatus cs = CustomStatus.Error;

            foreach (ListViewDataItem item in lvRoomMaster.Items)
            {
                CheckBox chk = (CheckBox)lvRoomMaster.Items[rowIndex].FindControl("chk") as CheckBox;
                Label lblRoomno = item.FindControl("lblRoomno") as Label;
                Label lblRoomname = item.FindControl("lblRoomname") as Label;
                Label lblRomCpt = item.FindControl("lblRomCpt") as Label;
                TextBox txtRequiredInvigilator = item.FindControl("txtRequiredInvigilator") as TextBox;

                if (chk.Checked)
                {
                    Count++;
                    cs = (CustomStatus)objExamController.InsertReqInvigilator(Convert.ToInt32(ddlCollege.SelectedValue), Convert.ToInt32(ddlFloorNo.SelectedValue), Convert.ToInt32(ddlBlockNo.SelectedValue), Convert.ToInt32(lblRoomno.ToolTip), Convert.ToInt32(txtRequiredInvigilator.Text), Convert.ToInt32(lblRomCpt.ToolTip));
                }
                rowIndex++;
            }

            if (Count == 0)
            {
                objCommon.DisplayMessage(this, "Please Select Room..!!", this.Page);
                return;
            }
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayMessage(this, "Invigilator Done Successfully..!!", this.Page);

            }
            else if (cs.Equals(CustomStatus.RecordNotFound))
            {   
                objCommon.DisplayMessage(this, "Invigilator Already Created..!!", this.Page);
            }
            else
            {
                objCommon.DisplayMessage(this, "Error in Invigilator Creation ..", this.Page);
            }
            BindRooms();

        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_PREEXAMINATION_CreateBundle.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


 
}
