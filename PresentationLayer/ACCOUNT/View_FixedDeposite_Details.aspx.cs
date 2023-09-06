//=================================================================================
// PROJECT NAME  : UAIMS                                                           
// MODULE NAME   : RECEIPT PAYMENT REPORT                                                    
// CREATION DATE : 24-MAY-2010                                               
// CREATED BY    : ASHISH THAKRE                                                 
// MODIFIED BY   : 
// MODIFIED DESC : 
//=================================================================================
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
using System.Text.RegularExpressions;
using IITMS;
using IITMS.NITPRM;
using IITMS.NITPRM.BusinessLayer.BusinessEntities;
using IITMS.NITPRM.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS;
using System.Data.SqlClient;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;

public partial class View_FixedDeposite_Details : System.Web.UI.Page
{
    UAIMS_Common objUCommon = new UAIMS_Common();
    Common objCommon = new Common();
    DataSet dschk = new DataSet();

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

        //if (hdnBal.Value != "")
        //{

        //    //lblCurBal.Text = hdnBal.Value;
        //    //lblmode.Text = hdnMode.Value;
        //}

        //Session["WithoutCashBank"] = "N";
        //btnGo.Attributes.Add("onClick", "return CheckFields();");
        if (!Page.IsPostBack)
        {
          //  SetDataColumn();


            //Check Session
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                if (Session["comp_code"] == null)
                {
                    Session["comp_set"] = "NotSelected";

                    objCommon.DisplayMessage("Select company/cash book.", this);

                    Response.Redirect("~/ACCOUNT/selectCompany.aspx");
                }
                else
                {

                    Session["comp_set"] = "";
                    //Page Authorization
                    //CheckPageAuthorization();

                    divCompName.InnerHtml = Session["comp_name"].ToString().ToUpper();
                    Page.Title = Session["coll_name"].ToString();
                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }

                    //--------------------------------------------------------------------------------

                   // DataSet dschk = new DataSet();



                    dschk = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString().Trim() + "_FixedDeposit_Details", "*", "", "Maturity_Date >=Convert(datetime, Convert(int, GetDate()))", "");

                    //objCommon.FillDropDownList(ddlFundingLedger, "ACC_" + Session["comp_code"].ToString() + "_" + "PARTY", "PARTY_NO", "PARTY_NAME", "", "");
                    AccountTransactionController acobj = new AccountTransactionController();
                    DataSet dst = new DataSet();

                    dst = acobj.GetFIXEDDEPOSITE_DETAILS(Session["comp_code"].ToString());

                    GridView1.DataSource = dst.Tables[0];
                    GridView1.DataBind();


                    //for date formate
                    for (int g = 0; g < GridView1.Rows.Count; g++)
                    {
                        Label txtfdate = GridView1.Rows[g].FindControl("lblDeposit_Date") as Label;
                        if (txtfdate != null)
                        {
                            txtfdate.Text = Convert.ToDateTime(dst.Tables[0].Rows[g][5]).ToString("dd-MMM-yyyy");
                        }

                        Label lblTodate = GridView1.Rows[g].FindControl("lblMaturity_Date") as Label;
                        if (lblTodate != null)
                        {
                            lblTodate.Text = Convert.ToDateTime(dst.Tables[0].Rows[g][6]).ToString("dd-MMM-yyyy");
                        }

                    }

                    //Label lblfeehd = GridData.Rows[y].FindControl("lblPayHeadsNo") as Label;
                  //  GridView1.Columns[

                    for(int i=0;i< GridView1.Rows.Count;i++)
                    {
                        DropDownList ddllcash = GridView1.Rows[i].FindControl("ddlParty") as DropDownList;

                            if (ddllcash != null)
                            {

                                 objCommon.FillDropDownList(ddllcash, "ACC_" + Session["comp_code"].ToString() + "_" + "PARTY", "PARTY_NO", "PARTY_NAME", "", "");
                   
                             }

                    }

                    DataSet dsfd = new DataSet();
                    dsfd = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString().Trim() + "_FixedDeposit_Details", "*", "", "Maturity_Date >=Convert(datetime, Convert(int, GetDate()))", "Maturity_Date");
                    for (int y = 0; y < dsfd.Tables[0].Rows.Count; y++)
                    {
                        DropDownList ddlledger = GridView1.Rows[y].FindControl("ddlParty") as DropDownList;
                        if (ddlledger != null)
                        {
                            ddlledger.SelectedValue = dsfd.Tables[0].Rows[y]["Party_No"].ToString().Trim();
                        }
                    }






                    //===================================FOR DELETE GROD ENTRY

                    DataSet dsMatScheme = new DataSet();
                    dsMatScheme = acobj.GetFIXEDDEPOSITE_MATURED_DETAILS(Session["comp_code"].ToString().Trim());

                    GridView2.DataSource = dsMatScheme.Tables[0];
                    GridView2.DataBind();


                    //for date formate
                    for (int g = 0; g < GridView2.Rows.Count; g++)
                    {
                        Label txtfdate = GridView2.Rows[g].FindControl("lblDeposit_Date") as Label;
                        if (txtfdate != null)
                        {
                            txtfdate.Text = Convert.ToDateTime(dsMatScheme.Tables[0].Rows[g][5]).ToString("dd-MMM-yyyy");
                        }

                        Label lblTodate = GridView2.Rows[g].FindControl("lblMaturity_Date") as Label;
                        if (lblTodate != null)
                        {
                            lblTodate.Text = Convert.ToDateTime(dsMatScheme.Tables[0].Rows[g][6]).ToString("dd-MMM-yyyy");
                        }

                    }

                    //Label lblfeehd = GridData.Rows[y].FindControl("lblPayHeadsNo") as Label;
                    //  GridView1.Columns[

                    for (int i = 0; i < GridView2.Rows.Count; i++)
                    {
                        DropDownList ddllcash = GridView2.Rows[i].FindControl("ddlParty") as DropDownList;

                        if (ddllcash != null)
                        {

                            objCommon.FillDropDownList(ddllcash, "ACC_" + Session["comp_code"].ToString() + "_" + "PARTY", "PARTY_NO", "PARTY_NAME", "", "");

                        }

                    }

                    DataSet dsfd1 = new DataSet();
                    dsfd1 = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString().Trim() + "_FixedDeposit_Details", "*", "", "Maturity_Date < Convert(datetime, Convert(int, GetDate()))", "Maturity_Date");
                    for (int y = 0; y < dsfd1.Tables[0].Rows.Count; y++)
                    {
                        DropDownList ddlledger = GridView2.Rows[y].FindControl("ddlParty") as DropDownList;
                        if (ddlledger != null)
                        {
                            ddlledger.SelectedValue = dsfd1.Tables[0].Rows[y]["Party_No"].ToString().Trim();
                        }
                    }



                    

                }
               

               

                



               

            }


          //  SetFinancialYear();

        }



        divMsg.InnerHtml = string.Empty;
    }
   
  
   
  
    
 
    
    

   
    
    
   
   
  
    
    



    protected void txtFrmDate_TextChanged(object sender, EventArgs e)
    {
        //if (txtAcc.Text.ToString().Trim() != "")
        //{
        //    btnGo_Click(sender, e);
        //}
        //txtUptoDate.Focus();
    }
    
    
  
    









   
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            GridView1.EditIndex = e.NewEditIndex;
            AccountTransactionController acobj = new AccountTransactionController();
            DataSet dst = new DataSet();

            dst = acobj.GetFIXEDDEPOSITE_DETAILS(Session["comp_code"].ToString());

            GridView1.DataSource = dst.Tables[0];
            GridView1.DataBind();
            //for date formate
            for (int g = 0; g < GridView1.Rows.Count; g++)
            {
                Label txtfdate = GridView1.Rows[g].FindControl("lblDeposit_Date") as Label;
                if (txtfdate != null)
                {
                    txtfdate.Text = Convert.ToDateTime(dst.Tables[0].Rows[g][5]).ToString("dd-MMM-yyyy");
                }

                Label lblTodate = GridView1.Rows[g].FindControl("lblMaturity_Date") as Label;
                if (lblTodate != null)
                {
                    lblTodate.Text = Convert.ToDateTime(dst.Tables[0].Rows[g][6]).ToString("dd-MMM-yyyy");
                }
            }

            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                DropDownList ddllcash = GridView1.Rows[i].FindControl("ddlParty") as DropDownList;

                if (ddllcash != null)
                {

                    objCommon.FillDropDownList(ddllcash, "ACC_" + Session["comp_code"].ToString() + "_" + "PARTY", "PARTY_NO", "PARTY_NAME", "", "");

                }

            }

            DataSet dsfd = new DataSet();
            dsfd = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString().Trim() + "_FixedDeposit_Details", "*", "", "Maturity_Date >=Convert(datetime, Convert(int, GetDate()))", "Maturity_Date");
            for (int y = 0; y < dsfd.Tables[0].Rows.Count; y++)
            {
                DropDownList ddlledger = GridView1.Rows[y].FindControl("ddlParty") as DropDownList;
                if (ddlledger != null)
                {
                    ddlledger.SelectedValue = dsfd.Tables[0].Rows[y]["Party_No"].ToString().Trim();
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "GridView1_RowEditing -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }


    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            GridView1.EditIndex = -1;
            AccountTransactionController acobj = new AccountTransactionController();
            DataSet dst = new DataSet();

            dst = acobj.GetFIXEDDEPOSITE_DETAILS(Session["comp_code"].ToString());

            GridView1.DataSource = dst.Tables[0];
            GridView1.DataBind();


            for (int g = 0; g < GridView1.Rows.Count; g++)
            {
                Label txtfdate = GridView1.Rows[g].FindControl("lblDeposit_Date") as Label;
                if (txtfdate != null)
                {
                    txtfdate.Text = Convert.ToDateTime(dst.Tables[0].Rows[g][5]).ToString("dd-MMM-yyyy");
                }

                Label lblTodate = GridView1.Rows[g].FindControl("lblMaturity_Date") as Label;
                if (lblTodate != null)
                {
                    lblTodate.Text = Convert.ToDateTime(dst.Tables[0].Rows[g][6]).ToString("dd-MMM-yyyy");
                }
            }



            

            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                DropDownList ddllcash = GridView1.Rows[i].FindControl("ddlParty") as DropDownList;

                if (ddllcash != null)
                {

                    objCommon.FillDropDownList(ddllcash, "ACC_" + Session["comp_code"].ToString() + "_" + "PARTY", "PARTY_NO", "PARTY_NAME", "", "");

                }

            }

            DataSet dsfd = new DataSet();
            dsfd = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString().Trim() + "_FixedDeposit_Details", "*", "", "Maturity_Date >=Convert(datetime, Convert(int, GetDate()))", "Maturity_Date");
            for (int y = 0; y < dsfd.Tables[0].Rows.Count; y++)
            {
                DropDownList ddlledger = GridView1.Rows[y].FindControl("ddlParty") as DropDownList;
                if (ddlledger != null)
                {
                    ddlledger.SelectedValue = dsfd.Tables[0].Rows[y]["Party_No"].ToString().Trim();
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "GridView1_RowCancelingEdit -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }
        

    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            FixedDepositeClass fdObj = new FixedDepositeClass();
            string idNo = GridView1.DataKeys[e.RowIndex].Value.ToString();

            DropDownList ddlparty = GridView1.Rows[e.RowIndex].FindControl("ddlParty") as DropDownList;
            int pno = Convert.ToInt32(ddlparty.SelectedValue.ToString());
            fdObj.PARTY_NO = ddlparty.SelectedValue.ToString();


            TextBox txtsc = GridView1.Rows[e.RowIndex].FindControl("txtFDScheme") as TextBox;
            string strScheme = txtsc.Text;
            fdObj.SCHEME = strScheme;

            //
            TextBox txtFdrNo1 = GridView1.Rows[e.RowIndex].FindControl("txtFDR_No") as TextBox;
            string FDR_No = txtFdrNo1.Text;
            fdObj.FDR_NO = FDR_No.ToString();


            //txtROI
            TextBox txtROI1 = GridView1.Rows[e.RowIndex].FindControl("txtROI") as TextBox;
            string roi = txtROI1.Text;
            fdObj.RateOfIntrest = Convert.ToDouble(roi);


            
            //txtDeposit_Date
            TextBox txtDeposit_Date1 = GridView1.Rows[e.RowIndex].FindControl("txtDeposit_Date") as TextBox;
            string Deposit_Date = txtDeposit_Date1.Text;
            fdObj.Deposite_Date = Convert.ToDateTime(txtDeposit_Date1.Text).ToString("dd-MMM-yyyy");

            //txtMaturity_Date
            TextBox txtMaturity_Date1 = GridView1.Rows[e.RowIndex].FindControl("txtMaturity_Date") as TextBox;
            string Maturity_Date = txtMaturity_Date1.Text;
            fdObj.Maturity_Date = Convert.ToDateTime(txtMaturity_Date1.Text).ToString("dd-MMM-yyyy");


            //--------------Checking Dates--------



            if (DateTime.Compare(Convert.ToDateTime(txtDeposit_Date1.Text), Convert.ToDateTime(txtMaturity_Date1.Text)) == 1)
            {
                objCommon.DisplayMessage(UPDLedger, "Date Of Deposit Can Not Be Greater Than Date Of Maturity. ", this);
                txtDeposit_Date1.Focus();
                return;
            }




            
            TextBox txtamt1 = GridView1.Rows[e.RowIndex].FindControl("txtamt") as TextBox;
            string amt = txtamt1.Text;
            fdObj.Amount = Convert.ToDouble(amt);

            AccountTransactionController acobj = new AccountTransactionController();
            int y = 0;
            y=acobj.DeleteFixedDepositeDetails(idNo, Session["comp_code"].ToString());
            int i = 0;

            i = acobj.InsertFixedDepositeDetails(fdObj, Session["comp_code"].ToString());

            //i = acobj.UpdateFixedDepositeDetails(fdObj, Session["comp_code"].ToString());
            if (i != 0)
            {
                //GridView1_RowCancelingEdit(sender, e);
                objCommon.DisplayMessage("Recorde Updated.........", this);
                GridViewCancelEditEventArgs E = new GridViewCancelEditEventArgs(e.RowIndex);
                GridView1_RowCancelingEdit(sender, E);
                //idView1.EditIndex = -1;
                
                return;
            }
        }


        //FixedDepositeClass fdObj = new FixedDepositeClass();





        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "GridView1_RowUpdatingt -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }
    }
    protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        AccountTransactionController acobj = new AccountTransactionController();
        int y = 0;
        string idNo = GridView2.DataKeys[e.RowIndex].Value.ToString();
        y = acobj.DeleteFixedDepositeDetails(idNo, Session["comp_code"].ToString());
        if (y != 0)
        {
            //GridView1_RowCancelingEdit(sender, e);
            objCommon.DisplayMessage("Recorde Updated.........", this);
            return;
        }

    }
}



