//================================================
//MODIFIED BY   : MRUNAL SINGH
//MODIFIED DATE : 19-12-2014
//DESCRIPTION   : ALLOW WEIGHT TO GET MODIFY
//================================================
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
//using IITMS.SQLServer.SQLDAL;

public partial class Dispatch_Masters_Weight : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    WeightController objWeightc = new WeightController();
    double WeightFrom, WeightTo;
    string Unit;
    int posttypeno;
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
                    Session["usertype"] == null || Session["userfullname"] == null)
                {
                    Response.Redirect("~/default.aspx");
                }
                else
                {
                    //Page Authorization
                    this.CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                    PopulateDropdown();
                    ViewState["action"] = "add";
                    BindListViewWeight();
                 
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DISPATCH_Masters_Weight.Page_Load --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Weight.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Weight.aspx");
        }
    }
    private void PopulateDropdown()
    {
        try
        {
            objCommon.FillDropDownList(ddlPostType, "ADMN_IO_POST_TYPE", "POSTTYPENO", "POSTTYPENAME", "POSTTYPENO > 0", "POSTTYPENAME");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DISPATCH_Masters_Weight.PopulateDropdown --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect(Request.Url.ToString());
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DISPATCH_Masters_Weight.btnCancel_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void BindListViewWeight()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("ADMN_IO_WEIGHT W LEFT OUTER JOIN ADMN_IO_POST_TYPE P ON W.POSTTYPENO=P.POSTTYPENO ", "W.WEIGHTNO,W.WEIGHTFROM,W.WEIGHTTO,W.COST,W.POSTTYPENO,W.CREATED_DATE,W.CREATOR,W.COLLEGE_CODE,CASE W.UNIT WHEN 0 THEN 'GM' ELSE 'KG' END UNIT", "P.POSTTYPENAME", "W.STATUS=0", "WEIGHTNO");
            //DataSet ds = objWeightc.GetAllWeight();

            if (ds.Tables[0].Rows.Count > 0)
            {
                lvWeight.DataSource = ds;
                lvWeight.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DISPATCH_Masters_Weight.BindListViewWeight -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int WEIGHTNO = int.Parse(btnEdit.CommandArgument);
            ViewState["WEIGHTNO"] = int.Parse(btnEdit.CommandArgument);
            ViewState["action"] = "edit";
            this.ShowDetails(WEIGHTNO);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DISPATCH_Masters_Weight.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void ShowDetails(int WeightNo)
    {
        try
        {
            //    DataSet ds = objWeightc.GetWeightByWeightNo(WeightNo);
            DataSet ds = objCommon.FillDropDown("ADMN_IO_WEIGHT weight LEFT OUTER JOIN ADMN_IO_POST_TYPE post ON weight.POSTTYPENO=post.POSTTYPENO ", "weight.*", "post.*", "WEIGHTNO=" + WeightNo, "WEIGHTNO");
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtFromWeight.Text = ds.Tables[0].Rows[0]["WEIGHTFROM"].ToString();
                txtToWeight.Text = ds.Tables[0].Rows[0]["WEIGHTTO"].ToString();
                ddlUnit.SelectedValue = ds.Tables[0].Rows[0]["UNIT"].ToString();
                txtCost.Text = ds.Tables[0].Rows[0]["COST"].ToString();
                ddlPostType.SelectedValue = ds.Tables[0].Rows[0]["POSTTYPENO"].ToString();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DISPATCH_Masters_Weight.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
       
        weight objWeight = new weight();
        try
        {
            if (!txtFromWeight.Text.Equals(string.Empty))                                                         //gayatri rode 19/011/2021
            {
                if (Convert.ToDouble(txtToWeight.Text) < Convert.ToDouble(txtFromWeight.Text))
                {
                    objCommon.DisplayMessage(this.Page, "From Weight Can Not Be Greater Than To Weight .", this.Page);
                    txtFromWeight.Focus();
                    return;
                }
            }

            objWeight.WeightFrom = Convert.ToDouble(txtFromWeight.Text);
            objWeight.WeightTo = Convert.ToDouble(txtToWeight.Text);
            objWeight.PostType = Convert.ToInt32(ddlPostType.SelectedValue);
            objWeight.Unit = Convert.ToInt32(ddlUnit.SelectedValue);
            objWeight.Cost = Convert.ToDouble(txtCost.Text);
            objWeight.College_code = Convert.ToString(Session["colcode"]);
            objWeight.Creator = (Session["userno"].ToString());
            objWeight.Created_Date = System.DateTime.Now;
            

            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    double WeightFrom = Convert.ToDouble(txtFromWeight.Text);
                    double WeightTo = Convert.ToDouble(txtToWeight.Text);
                    int Unit = Convert.ToInt32(ddlUnit.SelectedValue);
                    int posttypeno = Convert.ToInt16(ddlPostType.SelectedValue);
                    DataSet ds = objWeightc.GetWeightByWeightRange(WeightFrom, WeightTo, Unit, posttypeno);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        objCommon.DisplayUserMessage(updActivity, "Record Already Exist.", this);
                    }

                    

                    else
                    {
                       
                        CustomStatus cs = (CustomStatus)objWeightc.AddWeight(objWeight);
                        if (Convert.ToInt32(cs) != -99)
                        {
                            //objCommon.DisplayMessage(this.updActivity, "Record Saved", this.Page);
                            BindListViewWeight();
                            ViewState["action"] = "add";
                            objCommon.DisplayMessage(this.updActivity, "Record Saved successfully.", this.Page);
                            //Response.Redirect(Request.Url.ToString());
                            clear();
                            return;
                        }
                    }
                }
                else
                {
                    if (ViewState["WEIGHTNO"] != null)
                    {
                        int weightno = Convert.ToInt16(ViewState["WEIGHTNO"]);
                        double WeightFrom = Convert.ToDouble(txtFromWeight.Text);
                        double WeightTo = Convert.ToDouble(txtToWeight.Text);
                        int Unit = Convert.ToInt32(ddlUnit.SelectedValue);
                        int posttypeno = Convert.ToInt16(ddlPostType.SelectedValue);
                        //-------------------
                        DataSet ds = objWeightc.GetWeightByWeightNoAndRange(WeightFrom, WeightTo, Unit, posttypeno, weightno);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            objCommon.DisplayUserMessage(updActivity, "Record Already Exist.", this);
                        }
                        else
                        {
                            objWeight.WeightNo = Convert.ToInt32(ViewState["WEIGHTNO"].ToString());
                            CustomStatus cs = (CustomStatus)objWeightc.UpdateWeight(objWeight);
                            if (cs.Equals(CustomStatus.RecordUpdated))
                            {
                                BindListViewWeight();
                                ViewState["action"] = "add";
                                //objCommon.DisplayMessage("Record Updated successfully", this);
                                objCommon.DisplayMessage(this.updActivity, "Record Updated successfully.", this.Page);
                                clear();
                                return;
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DISPATCH_Masters_Weight.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void clear()
    {
        txtCost.Text = string.Empty;
        txtFromWeight.Text = string.Empty;
        txtToWeight.Text = string.Empty;
        ddlUnit.SelectedIndex = 0;
        ddlPostType.SelectedIndex = 0;
        ViewState["WEIGHTNO"] = null;
        ViewState["action"] = "add";
    }

    protected void txtToWeight_TextChanged(object sender, EventArgs e)
    {
        double WeightFrom = Convert.ToDouble(txtFromWeight.Text);
        double WeightTo = Convert.ToDouble(txtToWeight.Text);
        if (WeightFrom > WeightTo)
            objCommon.DisplayUserMessage(updActivity, "From Weight Can Not Be Greater Than To Weight.", this);

        //ddlUnit.Focus();                //gayatri rode 13-01-2021


       
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnDelete = sender as ImageButton;
            int WeightNo = int.Parse(btnDelete.CommandArgument);
            ViewState["WeightNo"] = int.Parse(btnDelete.CommandArgument);

            CustomStatus cs = (CustomStatus)objWeightc.DeleteWeight(WeightNo, Convert.ToInt32(Session["idno"]));
            if (cs.Equals(CustomStatus.RecordDeleted))
            {
                clear();
                BindListViewWeight();
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Deleted Successfully.');", true);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["errror"]) == true)
                objCommon.ShowError(Page, "DISPATCH_Masters_Weight.btnDelete_Click -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
}