//=================================================================================
// PROJECT NAME  : UAIMS                                                           
// MODULE NAME   : CASH/BANK GROUP GENERATIONS
// CREATION DATE : 12-OCTOBER-2009                                                  
// CREATED BY    : JITENDRA CHILATE
// MODIFIED BY   : 
// MODIFIED DESC : 
//=================================================================================

using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.NITPRM;

public partial class CashBankGroup : System.Web.UI.Page
{
    UAIMS_Common objUCommon = new UAIMS_Common();
    Common objCommon = new Common();
    DataSet ds;

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
           // myrow.Style["Display"] = "none";
            ddlHeads.Enabled = false;
          
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
                else { Session["comp_set"] = ""; }
                   
                //Page Authorization
                CheckPageAuthorization();

                divCompName.InnerHtml = Session["comp_name"].ToString().ToUpper();
                                              
            }

            FillddHeads();
        }              
    }

    private void FillddHeads()
    {
        objCommon.FillDropDownList(ddlHeads, "ACC_"+ Session["comp_code"].ToString().Trim() +"_PARENT_COMMON" , "IDNO", "NAME", "1=1", "IDNO");
        ds = objCommon.FillDropDown("ACC_"+Session["comp_code"].ToString().Trim() + "_PARTY", "PARTY_NO", "PARTY_NAME", "PAYMENT_TYPE_NO IN ('1','2')", "PARTY_NO");
        if (ds != null)
        {
            lvGrp.DataSource = ds;
            lvGrp.DataBind();
            if (ds.Tables[0].Rows.Count > 10)
            {
                pnl.ScrollBars = ScrollBars.None;

            }
            else
            {
                pnl.ScrollBars = ScrollBars.Vertical;
            }


        }
        else
        {
            objCommon.DisplayMessage("Record Not Found.", this);
            txtname.Focus();
        
        }

    }

    #region User Defined Methods

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=CashBankGroup.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=CashBankGroup.aspx");
        }
    }




    #endregion


    protected void btnAdd_Click(object sender, EventArgs e)
    {
        string code_year = Session["comp_code"].ToString(); // +"_" + Session["fin_yr"].ToString();
        CashBankGroupController objCsBk = new CashBankGroupController();

        CheckBox ch;
         HiddenField hdn;
       
        string PartyId=string.Empty;
        
        foreach (ListViewDataItem dti in lvGrp.Items)
        {
            ch = dti.FindControl("chkDet") as CheckBox;
            hdn = dti.FindControl("hdnPcd") as HiddenField;
            if (ch.Checked == true)
            {
                if (PartyId == string.Empty)
                {
                    PartyId = hdn.Value;
                }
                else
                {
                    PartyId = PartyId + "," + hdn.Value;
                }
                
            
            }
        }


        if (Convert.ToString(txtname.Text).Trim() != "")
        {
            int status = objCsBk.AddUpdateCashBankGroupDetails(code_year, txtname.Text.ToString().Trim(), PartyId, "Y");
            if (status == 1)
            {

                lblStatus.Text = "Record Saved Successfully.";
                txtname.Focus();
                
            }
            else if (status==-1001)
            {
                lblStatus.Text = "Record Allready Present.";
                txtname.Focus();
            
            }
            else if (status == 0)
            {
                lblStatus.Text = "Record Not Saved.";
                txtname.Focus();

            }
            else if (status == -99)
            {
                lblStatus.Text = "Transaction Failed.";
                txtname.Focus();

            }
            objCommon.FillDropDownList(ddlHeads, "ACC_" + Session["comp_code"].ToString().Trim() +"_PARENT_COMMON", "IDNO", "NAME", "1=1", "IDNO");
        }
        else
        {
            objCommon.DisplayMessage("Please Enter Name.", this);
            txtname.Focus();
            return;
        
        }


        

    }
    protected void btnModify_Click(object sender, EventArgs e)
    {
       // myrow.Style["Display"] = "block";
        ddlHeads.Enabled = true;

        string code_year = Session["comp_code"].ToString();// +"_" + Session["fin_yr"].ToString();
        CashBankGroupController objCsBk = new CashBankGroupController();

        CheckBox ch;
        HiddenField hdn;

        string PartyId = string.Empty;

        foreach (ListViewDataItem dti in lvGrp.Items)
        {
            ch = dti.FindControl("chkDet") as CheckBox;
            hdn = dti.FindControl("hdnPcd") as HiddenField;
            if (ch.Checked == true)
            {
                if (PartyId == string.Empty)
                {
                    PartyId = hdn.Value;
                }
                else
                {
                    PartyId = PartyId + "," + hdn.Value;
                }


            }
        }


        if (Convert.ToString(txtname.Text).Trim() != "")
        {
            if (objCsBk.AddUpdateCashBankGroupDetails(code_year, txtname.Text.ToString().Trim(), PartyId, "N") == 2)
            {
                //objCommon.DisplayMessage("Record Updated Successfully.", this);
                lblStatus.Text = "Record Updated Successfully.";
                txtname.Focus();

            }
            else
            {
                //objCommon.DisplayMessage("Record Not Updated Successfully.", this);
                lblStatus.Text = "Record Not Updated.";
                txtname.Focus();


            }
            objCommon.FillDropDownList(ddlHeads, "ACC_" + Session["comp_code"].ToString().Trim()+ "_PARENT_COMMON", "IDNO", "NAME", "1=1", "IDNO");
        }
        else
        {
            objCommon.DisplayMessage("Please Enter Name.", this);
            txtname.Focus();
            return;

        }


    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //myrow.Style["Display"] = "none";
        ddlHeads.Enabled = false;
        txtname.Text = "";
        lblStatus.Text = "";
        txtname.Focus();
        FillddHeads();
    }
    protected void ddlHeads_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtname.Text = Convert.ToString(ddlHeads.SelectedItem.Text).Trim();


        ds = objCommon.FillDropDown("ACC_"+ Session["comp_code"].ToString().Trim() +"_CHILD_COMMON", "IDNO", "PARTY_NO", "IDNO =" + Convert.ToString(ddlHeads.SelectedValue).Trim() , "IDNO");
        if (ds != null)
        {

            CheckBox ch;
            HiddenField hdn;
            int i=0;
            int j=0;
            j=ds.Tables[0].Rows.Count-1;
            string PartyId = string.Empty;
                                 
            foreach (ListViewDataItem dti in lvGrp.Items)
            {
                ch = dti.FindControl("chkDet") as CheckBox;
                hdn = dti.FindControl("hdnPcd") as HiddenField;
                ch.Checked = false;
                for (i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                {
                   
                    if (Convert.ToString(hdn.Value).Trim() == Convert.ToString(ds.Tables[0].Rows[i]["PARTY_NO"]).Trim())
                    {
                        ch.Checked = true;

                    }
                    
                
                }
                

                
            }


        }
        else
        {
            objCommon.DisplayMessage("Record Not Found.", this);
            txtname.Focus();

        }



    }
}

