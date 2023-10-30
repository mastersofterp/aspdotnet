using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class PAYROLL_MASTERS_DA_HEAD_CALCULATION : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    EmpCreateController objStatus = new EmpCreateController();
    PayMaster objPay = new PayMaster();
    string UsrStatus = string.Empty;
    int OrganizationId;
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
                ViewState["action"] = "Add";
                BindListView();
                HRADataTable();
                OrganizationId = Convert.ToInt32(Session["OrgId"]);
                objCommon.FillDropDownList(ddlDAHRA, "DA_HEAD", "DA_HEADID", "DA_HEAD_DESCRIPTION", "", "");
                divHracal.Visible = false;
            }
        }
    }


    private void HRADataTable()
    {
        try
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("FROM_YEAR");
            dt.Columns.Add("TO_YEAR");
            dt.Columns.Add("HEAD_PER");


            ViewState["HRACal"] = dt;
        }
        catch (Exception ex)
        {
            //objCommon.ShowErrorMessage(Panel_Error, Label_ErrorMessage, CLOUD_COMMON.Message.ExceptionOccured, CLOUD_COMMON.MessageType.Alert);
            //return ex;
        }
    }


    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = new DataTable();

            ImageButton btnDelete = sender as ImageButton;
            int srno = Convert.ToInt32(btnDelete.CommandArgument);
            if (ViewState["HRACal"] != null)
            {
                dt = (DataTable)ViewState["HRACal"];
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (srno - 1 == i)
                {
                    dt.Rows.RemoveAt(i);
                    break;
                }

            }

            lstHraList.DataSource = dt;
            lstHraList.DataBind();
            ViewState["HRACal"] = dt;

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Scale.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
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
                Response.Redirect("~/notauthorized.aspx?page=Pay_DA_HEAD.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Pay_DA_HEAD.aspx");
        }
    }
    private void BindListView()
    {
        try
        {
            DataSet ds = objStatus.GETDAHEADCalculation(0);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvheaddescription.DataSource = ds;
                lvheaddescription.DataBind();

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Scale.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void DataPager1_PreRender(object sender, EventArgs e)
    {
        BindListView();
    }


    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["action"].ToString() != "edit")
            {
                //Select DA_HEADID from DA_HEAD_CALCULATION where DA_HEADID=4
                int Daid = Convert.ToInt32(objCommon.LookUp("DA_HEAD_CALCULATION", "isnull(DA_HEADID,0)", "DA_HEADID=" + ddlDAHRA.SelectedValue + ""));

                if (Daid > 0)
                {
                    objCommon.DisplayMessage(updpanel, "Selected DA head Already Exists!", this);
                    return;
                }
            }


            int DAHeadId = 0;
            if (ViewState["action"].ToString() == "edit")
            {
                if (ViewState["DAID"].ToString() != "")
                {
                    DAHeadId = Convert.ToInt32(ViewState["DAID"].ToString());
                }
            }
            else
            {
                DAHeadId = Convert.ToInt32(ddlDAHRA.SelectedValue);
            }
            int HeadCalPer = Convert.ToInt32(txtDAper.Text);

            bool Flag = false;

            string Fdate = (String.Format("{0:u}", Convert.ToDateTime(txtEffectDate.Text)));
            Fdate = Fdate.Substring(0, 10);

            DataTable dtResult = new DataTable();


            DataTable DEHEADTbl = new DataTable("DEHEADTbl");
            DEHEADTbl.Columns.Add("FROMYEAR", typeof(decimal));
            DEHEADTbl.Columns.Add("TOYEAR", typeof(decimal));
            DEHEADTbl.Columns.Add("HEADPER", typeof(decimal));


            DataRow dr = null;


            foreach (ListViewItem i in lstHraList.Items)
            {
                TextBox txtFromYear = (TextBox)i.FindControl("txtFromYear");
                TextBox txtToYear = (TextBox)i.FindControl("txtToYear");
                TextBox txtHRAPerr = (TextBox)i.FindControl("txtHRAPer");

                dr = DEHEADTbl.NewRow();

                dr["FROMYEAR"] = Convert.ToDecimal(txtFromYear.Text);
                dr["TOYEAR"] = Convert.ToDecimal(txtToYear.Text);
                dr["HEADPER"] = Convert.ToDecimal(txtHRAPerr.Text);


                DEHEADTbl.Rows.Add(dr);
            }

            if (chkYrHRACal.Checked == true)
            {
                Flag = true;
            }
            else
            {
                Flag = false;
            }



            // PreviousDate = Convert.ToDateTime(objCommon.LookUp("DA_HEAD_CALCULATION", "isnull(DA_HEAD_CALCULATION_DATE,'')", "DA_HEADID =" + DAHeadId));
            int cs = objStatus.UpdateDAHeadCalculationDetails(DAHeadId, Convert.ToInt32(txtHRAPer.Text), Fdate, 0, Convert.ToInt32(Session["OrgId"]), Convert.ToInt32(Session["userno"]), HeadCalPer, DEHEADTbl, Flag);
           
            if (cs == 2)
            {
                if (ViewState["action"].ToString() == "edit")
                {
                    objCommon.DisplayMessage(updpanel, "Record Updated Successfully", this);
                    //  Response.Redirect(Request.Url.ToString());
                    ClearControl();
                }
                else
                {
                    objCommon.DisplayMessage(updpanel, "Record inserted Successfully", this);
                    // Response.Redirect(Request.Url.ToString());
                    ClearControl();
                }
            }




            //foreach (ListViewDataItem lvitem in lvheaddescription.Items)
            //{
            //    CheckBox chkbox = lvitem.FindControl("chkID") as CheckBox;
            //    if (chkbox.Checked == true)
            //    {
            //        HiddenField hdnDAHEADiD = lvitem.FindControl("hdnDaHeadId") as HiddenField;
            //        DAHeadId = Convert.ToInt32(hdnDAHEADiD.Value);
            //        TextBox txtCalPer = lvitem.FindControl("txtDAHEADPER") as TextBox;
            //        HeadCalPer = Convert.ToInt32(txtCalPer.Text.Trim());
            //        TextBox txtHeadCalDate = lvitem.FindControl("txtdaheaddate") as TextBox;
            //        HeadCalDate = Convert.ToDateTime(txtHeadCalDate.Text.Trim());
            //        if (chkbox.Checked == true)
            //        {
            //           // PreviousDate = Convert.ToDateTime(objCommon.LookUp("DA_HEAD_CALCULATION", "isnull(DA_HEAD_CALCULATION_DATE,'')", "DA_HEADID =" + DAHeadId));
            //            int cs = objStatus.UpdateDAHeadCalculationDetails(DAHeadId, HeadCalPer, HeadCalDate, CollegeNo, Convert.ToInt32(Session["OrgId"]), Convert.ToInt32(Session["userno"]));
            //            if (cs == 2)
            //            {
            //                count = 1;
            //            }
            //        }
            //    }
            //}
            //if (count == 1)
            //{
            //    //lblerror.Text = null;
            //    //lblmsg.Text = "Record Updated Successfully";
            //    objCommon.DisplayMessage(updpanel, "Record Updated Successfully", this);
            //}
        }
        catch (Exception ex)
        {

        }
    }

    private void ClearControl()
    {
        try
        {
            txtFrYr.Text = string.Empty;
            txttoyr.Text = string.Empty;
            txtHraPernew.Text = string.Empty;
            ddlDAHRA.SelectedValue = "0";
            txtDAper.Text = string.Empty;
            txtHRAPer.Text = string.Empty;
            txtEffectDate.Text = string.Empty;
            chkYrHRACal.Checked = false;
            divHracal.Visible = false;

            lstHraList.DataSource = null;
            lstHraList.DataBind();

            ViewState["HRACal"] = null;
            ViewState["action"] = "Add";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Masters_quarterMas.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btncancel_Click(object sender, EventArgs e)
    {
        ClearControl();
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtFrYr.Text.Trim() == string.Empty) { txtFrYr.Text = "0"; }
            if (txttoyr.Text.Trim() == string.Empty) { txttoyr.Text = "0"; }
            if (txtHraPernew.Text.Trim() == string.Empty) { txtHraPernew.Text = "0"; }

            if (ViewState["HRACal"] == null)
            {
                HRADataTable();
            }

            DataTable dt = (DataTable)ViewState["HRACal"];

            DataRow dr = dt.NewRow();

            dr["FROM_YEAR"] = Convert.ToDecimal(txtFrYr.Text);
            dr["TO_YEAR"] = Convert.ToDecimal(txttoyr.Text);
            dr["HEAD_PER"] = Convert.ToDecimal(txtHraPernew.Text);

            dt.Rows.Add(dr);

            lstHraList.DataSource = dt;
            lstHraList.DataBind();

            txtFrYr.Text = "";
            txttoyr.Text = "";
            txtHraPernew.Text = "";


        }
        catch (Exception)
        {

            throw;
        }

    }
    protected void chkYrHRACal_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (chkYrHRACal.Checked == true)
            {
                divHracal.Visible = true;
            }
            else
            {
                divHracal.Visible = false;

                lstHraList.DataSource = null;
                lstHraList.DataBind();


                ViewState["HRACal"] = null;
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            //int qtrno = int.Parse(btnEdit.CommandArgument);
            ViewState["DAID"] = int.Parse(btnEdit.CommandArgument);

            ListViewDataItem lst = btnEdit.NamingContainer as ListViewDataItem;
            //Label lblQtrTypeNo = lst.FindControl("lblQtrTypeNo") as Label;

            HiddenField hdnDAHEADiD = lst.FindControl("hdnDaHeadId") as HiddenField;
            TextBox txtCalPer = lst.FindControl("txtDAHEADPER") as TextBox;
            TextBox txtHRAPerr = lst.FindControl("txtHRAPer") as TextBox;
            HiddenField hdnDetail = lst.FindControl("hdnDetail") as HiddenField;
            TextBox txtHeadCalDate = lst.FindControl("txtdaheaddate") as TextBox;

            ddlDAHRA.SelectedValue = hdnDAHEADiD.Value;
            txtDAper.Text = txtCalPer.Text.Trim();
            txtHRAPer.Text = txtHRAPerr.Text.Trim();

            if (hdnDetail.Value == "False")
            {
                chkYrHRACal.Checked = false;
            }
            else
            {
                chkYrHRACal.Checked = true;
            }
            txtEffectDate.Text = txtHeadCalDate.Text.Trim();



            if (chkYrHRACal.Checked == true)
            {
                divHracal.Visible = true;

                DataSet ds = objStatus.GETDAHEADCalculation(Convert.ToInt32(hdnDAHEADiD.Value));
                if (ds.Tables[1].Rows.Count > 0)
                {
                    lstHraList.DataSource = ds.Tables[1];
                    lstHraList.DataBind();

                }

                ViewState["HRACal"] = ds.Tables[1];
            }
            else
            {
                divHracal.Visible = false;

                lstHraList.DataSource = null;
                lstHraList.DataBind();


                ViewState["HRACal"] = null;
            }





            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Masters_quarterMas.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
}