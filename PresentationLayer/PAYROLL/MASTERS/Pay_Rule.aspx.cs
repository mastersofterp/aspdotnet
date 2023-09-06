using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class PAYROLL_MASTERS_Pay_Rule : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    PayController objpay = new PayController();

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
                CheckPageAuthorization();
                Page.Title = Session["coll_name"].ToString();

                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
               

                //BindListViewStatus();
                //Set Report Parameters 
                //objCommon.ReportPopUp(btnShowReport, "pagetitle=NITPRM&path=~" + "," + "Reports" + "," + "ESTABLISHMENT" + "," + "LEAVES" + "," + "ESTB_Holidays.rpt&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@username=" + Session["userfullname"].ToString(), "NITPRM");
                objCommon.ReportPopUp(btnShowReport, "pagetitle=NITPRM&path=~" + "," + "Reports" + "," + "ESTABLISHMENT" + "," + "LEAVES" + "," + "ESTB_PassAuthority.rpt&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@username=" + Session["userfullname"].ToString(), "NITPRM");
            }
            ViewState["action"] = "add";
            BindListViewPayrule();
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString()) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Pay_EmpStatus.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Pay_EmpStatus.aspx");
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            Payroll objRule = new Payroll();

            objRule.Payrule = Convert.ToString(txtRule.Text);
            objRule.RULENAME = txtRuleName.Text;
            objRule.IsR7 = cb7cprule.Checked;
            if (ChkIsActive.Checked == true)
            {
                objRule.STATUS = "1";
            }
            else
            {
                objRule.STATUS = "0";
            }

            objRule.COLLEGE_CODE = Convert.ToString(Session["colcode"]);

               if (ViewState["action"].ToString().Equals("add"))
                {
                    var data = objCommon.LookUp("payroll_rule", "RULENO", "PAYRULE='" + txtRule.Text + "'");

                    if (data != string.Empty)
                    {
                        Clear();
                        MessageBox("Record Already Exist");
                        return;
                    }
                    CustomStatus cs = (CustomStatus)objpay.AddPayRule(objRule);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {

                        ViewState["action"] = null;
                        Clear();
                        BindListViewPayrule();
                        MessageBox("Record Saved Successfully");

                    }
                    
                }
                else
                {
                    if (ViewState["RuleNo"] != null)
                    {
                        objRule.RuleNo = Convert.ToInt32(ViewState["RuleNo"].ToString());
                        CustomStatus cs = (CustomStatus)objpay.UpdatePayRule(objRule);
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            ViewState["action"] = null;
                            Clear();
                            BindListViewPayrule();
                            MessageBox("Record Updated Successfully");
                        }
                    }
                }
            

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PAYROLL_MASTERS_Pay_Rule.btnSave_Click ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //lblmsg.Text = null;
            ImageButton btnEdit = sender as ImageButton;
            int Ruleno = int.Parse(btnEdit.CommandArgument);
            ShowDetails(Ruleno);
            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PAYROLL_MASTERS_Pay_Rule.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void BindListViewPayrule()
    {
        try
        {
            DataSet ds = objpay.GetAllPayRule();
            //if (ds.Tables[0].Rows.Count <= 0)
            //{
            //    btnShowReport.Visible = false;
            //    dpPager.Visible = false;
            //}
            //else
            //{
            //    btnShowReport.Visible = true;
            //    dpPager.Visible = true;
            //}
            lvPayRule.DataSource = ds;
            lvPayRule.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_Passing_Authority.BindListViewPAuthority ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetails(int Ruleno)
    {
        try
        {
            DataSet ds = objpay.GetPayRule(Ruleno);
            
            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["RuleNo"] = Ruleno;
                txtRule.Text = ds.Tables[0].Rows[0]["PAYRULE"].ToString();
                txtRuleName.Text = ds.Tables[0].Rows[0]["RULENAME"].ToString();
                cb7cprule.Checked =Convert.ToBoolean(ds.Tables[0].Rows[0]["IsR7"].ToString());
                if (ds.Tables[0].Rows[0]["ACTIVESTATUS"].ToString() == "0")
                {
                    ChkIsActive.Checked = false;
                }
                else
                {
                    ChkIsActive.Checked = true;
                }
            }
           
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PAYROLL_MASTERS_Pay_Rule.ShowDetails-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            this.Clear();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PAYROLL_MASTERS_Pay_Rule.btnCancel_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    protected void Clear()
    {
        try
        {
            txtRule.Text=string.Empty;
            txtRuleName.Text = string.Empty;
            cb7cprule.Checked=false;
            ChkIsActive.Checked = false;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PAYROLL_MASTERS_Pay_Rule.Clear-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }
}