//=================================================================================
// PROJECT NAME  : UAIMS                                                           
// MODULE NAME   : TO CREATE NEW COMPANY/CASH BOOK                                 
// CREATION DATE : 25-AUGUST-2009                                                  
// CREATED BY    : NIRAJ D. PHALKE                                                 
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

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

using System.Data.SqlClient;
using IITMS.NITPRM;

public partial class Account_createCompany : System.Web.UI.Page
{
    UAIMS_Common objUCommon = new UAIMS_Common();
    Common objCommon = new Common();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //btndelete.Attributes.Add("onClick", "return AskConfirmation();");

        if (!Page.IsPostBack)
        {
            //Check Session
            txtCode.Focus();
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                //Page Authorization
                CheckPageAuthorization();
                Page.Title = Session["coll_name"].ToString();

                //txtBWDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtBWDate.Text = "01/04/" + ((DateTime.Now.Month < 4) ? (DateTime.Now.Year - 1).ToString() : (DateTime.Now.Year).ToString());
                txtFromDate.Text = "01/04/" + ((DateTime.Now.Month < 4) ? (DateTime.Now.Year - 1).ToString() : (DateTime.Now.Year).ToString());
                txtToDate.Text = "31/03/" + ((DateTime.Now.Month < 4) ? (DateTime.Now.Year).ToString() : (DateTime.Now.Year + 1).ToString());
            }
            btndelete.Attributes.Add("onClick", "return AskAskToDelete();");
        }
        //txtBWDate.Text = "01/04/" + ((DateTime.Now.Month < 4) ? (DateTime.Now.Year - 1).ToString() : (DateTime.Now.Year).ToString());
        PopulateCompanyList();
        divMsg.InnerHtml = string.Empty;
    }


    private void PopulateCompanyList()
    {
        try
        {
            DataSet dsCompany = objCommon.FillDropDown("ACC_COMPANY", "COMPANY_NO", "(COMPANY_NAME + ' - ' + CAST(YEAR(COMPANY_FINDATE_FROM) AS NVARCHAR(4)) + '-' + CAST(YEAR(COMPANY_FINDATE_TO) AS NVARCHAR(4))) AS COMPANY_NAME", "DROP_FLAG='N'", "COMPANY_NAME");
            if (dsCompany.Tables.Count > 0)
            {
                if (dsCompany.Tables[0].Rows.Count > 0)
                {
                   
                    lstCompany.DataTextField = "COMPANY_NAME";
                    lstCompany.DataValueField = "COMPANY_NO";
                    lstCompany.DataSource = dsCompany.Tables[0];
                    lstCompany.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Account_createCompany.PopulateCompanyList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {

            if (DateTime.Compare(Convert.ToDateTime(txtToDate.Text), Convert.ToDateTime(DateTime.Now)) == -1)
            {
                objCommon.DisplayMessage(updpan,"Financial Year To Date Should Not Be Less Than Current Date.", this.Page);
                txtToDate.Focus();
                return;
            }
            if (DateTime.Compare(Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtToDate.Text)) == 1)
            {
                objCommon.DisplayUserMessage(updpan, "Financial Year From Date Should Not Be Greater Than To Date.", this.Page);
                txtFromDate.Focus();
                return;
            }

            if (Convert.ToDateTime(txtBWDate.Text) > Convert.ToDateTime(txtToDate.Text))
            {
                objCommon.DisplayUserMessage(updpan, "Book Writing Date Should Be Less Than Or Equal To Financial Year To.", this.Page);
                txtBWDate.Focus();
                return;
            }

            if (DateTime.Compare(Convert.ToDateTime(txtBWDate.Text), Convert.ToDateTime(txtFromDate.Text)) == -1)
            {
                objCommon.DisplayUserMessage(updpan, "Book Writing Date Should Be Greater Than Or Equal To Financial Year From.", this);
                txtBWDate.Focus();
                return;
            }

            DateTime frmdt = Convert.ToDateTime(txtFromDate.Text);
            DateTime todt = Convert.ToDateTime(txtToDate.Text);
            DateTime dtcounter = DateTime.Now;
            Int32 i = 1;
            while (DateTime.Compare(dtcounter, todt) != 0)
            {
                dtcounter = frmdt.AddDays(i);
                i = i + 1;
            }
            if (i > 366)
            {
                objCommon.DisplayMessage("Financial Year Is Not Having Valid Range.", this);
                return;
            }


            #region Apply Transaction Scope

            //TransactionOptions transactionOptions = new TransactionOptions();
            //transactionOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            //transactionOptions.Timeout = TimeSpan.FromDays(1);
            //TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Suppress, transactionOptions, EnterpriseServicesInteropOption.Full);

            #endregion

            //using (transactionScope)
            {
                //FinanceCashBookController objCBC = new FinanceCashBookController(Session["UserName"].ToString().Trim(), Session["Password"].ToString().Trim(), Session["DataBase"].ToString().Trim());
                FinanceCashBookController objCBC = new FinanceCashBookController();
                FinCashBook objCashBook = new FinCashBook();

                objCashBook.Company_Code = txtCode.Text.Trim();
                objCashBook.Company_Name = txtName.Text.Trim().ToUpper();
                objCashBook.Company_FindDate_From = Convert.ToDateTime(txtFromDate.Text.Trim());
                objCashBook.Company_FindDate_To = Convert.ToDateTime(txtToDate.Text.Trim());
                objCashBook.BookWriteDate = Convert.ToDateTime(txtBWDate.Text.Trim());
                objCashBook.College_Code = Session["colcode"].ToString();

                if (chkIsCompanyLock.Checked == true)   // Akshay Dixit on 11-04-2022
                {
                    objCashBook.IsCompanyLock = 1;
                }
                else
                {
                    objCashBook.IsCompanyLock = 0;
                }

         
                //Financial Year
                int yearfrm = Convert.ToDateTime(txtFromDate.Text.Trim()).Year;  // DateTime.Now.Year;
                int monfrm = Convert.ToDateTime(txtFromDate.Text.Trim()).Month;   //DateTime.Now.Month;

                int year = Convert.ToDateTime(txtToDate.Text.Trim()).Year;  // DateTime.Now.Year;
                int mon = Convert.ToDateTime(txtToDate.Text.Trim()).Month;   //DateTime.Now.Month;
                //if (mon < 4)
                objCashBook.Year = yearfrm.ToString().Substring(2) + year.ToString().Substring(2);//(year - 1).ToString().Substring(2) + year.ToString().Substring(2);
                //else
                //    objCashBook.Year = year.ToString().Substring(2) + (year + 1).ToString().Substring(2);

                #region For Creating Tables At run time
                FinCashBook objCashBookTable = new FinCashBook();
                objCashBookTable.Company_Code = txtCode.Text.Trim();
                objCashBookTable.Year = yearfrm.ToString().Substring(2) + year.ToString().Substring(2);
                objCashBookTable.College_Code = Session["colcode"].ToString();

                #endregion


                if (hdnCompNo.Value.ToString().Trim() != "")
                {
                    if (hdnOldFinYr.Value.ToString().Trim() != "")
                    {
                        #region ashish updated
                        if (objCBC.UpdateComp_CashBook(Convert.ToDateTime(txtFromDate.Text.Trim()).ToString("dd-MMM-yyyy").Trim(), Convert.ToDateTime(txtToDate.Text.Trim()).ToString("dd-MMM-yyyy").Trim(), Convert.ToDateTime(txtBWDate.Text.Trim()).ToString("dd-MMM-yyyy").Trim(), objCashBook.Company_Code, objCashBook.Company_Name, Convert.ToInt16(hdnCompNo.Value.ToString().Trim()), hdnOldFinYr.Value.ToString().Trim(), objCashBook.Year.ToString().Trim(), txtName.Text.ToString().Trim().ToUpper(), objCashBook.IsCompanyLock) == 1)
                        {
                            //#region Fill Cashe

                            //HttpRuntime.Cache.Remove("ACC_COMPANY" + Session["DataBase"].ToString().Trim());
                            //DataSet dscache = objCommon.FillDropDown("ACC_COMPANY", "COMPANY_NO", "(COMPANY_NAME + ' - ' + CAST(YEAR(COMPANY_FINDATE_FROM) AS NVARCHAR(4)) + '-' + CAST(YEAR(COMPANY_FINDATE_TO) AS NVARCHAR(4))) AS COMPANY_NAME", "DROP_FLAG='N'", "COMPANY_NAME");
                            //if (dscache != null && dscache.Tables[0].Rows.Count > 0)
                            //{
                            //    HttpRuntime.Cache.Insert("ACC_COMPANY" + Session["DataBase"].ToString().Trim(), dscache, null, DateTime.Now.AddMinutes(120), Cache.NoSlidingExpiration, CacheItemPriority.Default, null);

                            //}

                            //#endregion

                            ///Change
                            //objCommon.ShowErrorMessage(Panel_Confirm, Label_ConfirmMessage, Common.Message.Saved, Common.MessageType.Success);

                            hdnCompNo.Value = "";
                            objCommon.DisplayUserMessage(updpan, "Company Details Updated Successfully.", this.Page);
                            txtCode.Enabled = true;
                            txtFromDate.Enabled = true;
                            txtToDate.Enabled = true;
                            txtBWDate.Enabled = true;
                            Session["comp_code"] = null;
                            Session["fin_yr"] = null;
                            txtCode.Focus();
                            Clear();
                            PopulateCompanyList();
                            //   transactionScope.Complete();
                            return;

                        }
                        #endregion
                    }
                }

                CustomStatus cs = (CustomStatus)objCBC.AddComp_CashBook(objCashBook);
                if (cs.Equals(CustomStatus.DuplicateRecord))
                    objCommon.DisplayMessage(updpan, "The company code already exist !", this);
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    CustomStatus ct = (CustomStatus)objCBC.Create_Tables(objCashBookTable);
                    //CustomStatus sy = (CustomStatus)objCBC.Create_Synonym_For_Tables(objCashBookTable);
                    //if (sy.Equals(CustomStatus.RecordSaved))
                    //{

                    //}
                    //else
                    //{
                    //    objCommon.DisplayMessage(updpan, "synonym For TABLES NOT CREATED", this);
                    //    return;
                    //}
                    if (ct.Equals(CustomStatus.RecordSaved))
                    {
                        Clear();
                        // ShowMessage("Company/Cash Book Created");
                        objCommon.DisplayMessage(updpan, "Company/Cash Book Created Successfully", this.Page);
                        hdnCompNo.Value = "";

                        //#region Fill Cashe

                        //HttpRuntime.Cache.Remove("ACC_COMPANY" + Session["DataBase"].ToString().Trim());
                        //DataSet dscache = objCommon.FillDropDown("ACC_COMPANY", "COMPANY_NO", "(COMPANY_NAME + ' - ' + CAST(YEAR(COMPANY_FINDATE_FROM) AS NVARCHAR(4)) + '-' + CAST(YEAR(COMPANY_FINDATE_TO) AS NVARCHAR(4))) AS COMPANY_NAME", "DROP_FLAG='N'", "COMPANY_NAME");
                        //if (dscache != null && dscache.Tables[0].Rows.Count > 0)
                        //{
                        //    HttpRuntime.Cache.Insert("ACC_COMPANY" + Session["DataBase"].ToString().Trim(), dscache, null, DateTime.Now.AddMinutes(120), Cache.NoSlidingExpiration, CacheItemPriority.Default, null);

                        //}

                        //#endregion

                    }
                    else
                    {
                        objCommon.DisplayMessage(updpan, "Company/Cash Book Creation Error", this);
                        hdnCompNo.Value = "";
                        return;
                    }


                    // transactionScope.Complete();
                }
                else if (cs.Equals(CustomStatus.DuplicateRecord))
                // ShowMessage("Code Already Exists. Enter Another Code");
                {
                    objCommon.DisplayMessage(updpan, "Code Already Exists. Enter Another Code", this);
                    //  txtCode.Focus();
                    hdnCompNo.Value = "";
                }
                else
                {
                    // ShowMessage("Server Error!!!");
                    objCommon.DisplayMessage(updpan, "Error Occured!", this);
                    //objCommon.ShowErrorMessage(Panel_Error, Label_ErrorMessage, Common.Message.ExceptionOccured, Common.MessageType.Error);
                }
                txtCode.Enabled = true;
                txtFromDate.Enabled = true;
                txtToDate.Enabled = true;
                txtBWDate.Enabled = true;
                PopulateCompanyList();
                Session["comp_code"] = null;
                Session["fin_yr"] = null;

                txtCode.Focus();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Account_createCompany.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    #region User Defined Methods
    private void ShowMessage(string message)
    {
        if (message != string.Empty)
        {
            divMsg.InnerHtml += "<script type='text/javascript' language='javascript'> alert('" + message + "'); </script>";
        }
    }

    private void Clear()
    {
        txtCode.Text = string.Empty;
        txtName.Text = string.Empty;
        txtCode.Enabled = true;
        txtBWDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        txtFromDate.Text = "01/04/" + ((DateTime.Now.Month < 4) ? (DateTime.Now.Year - 1).ToString() : (DateTime.Now.Year).ToString());
        txtToDate.Text = "31/03/" + ((DateTime.Now.Month < 4) ? (DateTime.Now.Year).ToString() : (DateTime.Now.Year + 1).ToString());
        txtCode.Focus();
        hdnCompNo.Value = "";
        chkIsCompanyLock.Checked = false;
        //txtFromDate.Text = string.Empty;
        //txtToDate.Text = string.Empty;
        //txtBWDate.Text = string.Empty;
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=createCompany.aspx");
            }
            Common objCommon = new Common();
            objCommon.RecordActivity(int.Parse(Session["loginid"].ToString()), int.Parse(Request.QueryString["pageno"].ToString()), 0);
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=createCompany.aspx");
        }
    }

    #endregion



    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }

    protected void lstCompany_SelectedIndexChanged(object sender, EventArgs e)
    {

        FinCashBookController objCBC = new FinCashBookController();
        string id = Request.Form[lstCompany.UniqueID].ToString();
        DataTableReader dtr = objCBC.GetCashBookByCompanyNo(id);
        if (dtr.Read())
        {
            hdnCompNo.Value = dtr["COMPANY_NO"].ToString().Trim();
            txtCode.Text = dtr["COMPANY_CODE"].ToString().Trim();
            Session["fin_yr"] = Convert.ToDateTime(dtr["COMPANY_FINDATE_FROM"]).Year.ToString().Substring(2) + Convert.ToDateTime(dtr["COMPANY_FINDATE_TO"].ToString()).Year.ToString().Substring(2);
            hdnOldFinYr.Value = Convert.ToDateTime(dtr["COMPANY_FINDATE_FROM"]).Year.ToString().Substring(2) + Convert.ToDateTime(dtr["COMPANY_FINDATE_TO"].ToString()).Year.ToString().Substring(2);
            txtName.Text = dtr["COMPANY_NAME"].ToString().Trim();
            txtFromDate.Text = Convert.ToDateTime(dtr["COMPANY_FINDATE_FROM"]).ToString("dd/MM/yyyy").Trim();
            txtToDate.Text = Convert.ToDateTime(dtr["COMPANY_FINDATE_TO"]).ToString("dd/MM/yyyy").Trim();
            txtBWDate.Text = Convert.ToDateTime(dtr["BOOKWRTDATE"]).ToString("dd/MM/yyyy").Trim();

            if (Convert.ToBoolean(dtr["Lock_Status"]))  // Akshay Dixit on 11-04-2022
            {
                chkIsCompanyLock.Checked = true;
            }
            else
            {
                chkIsCompanyLock.Checked = false;
            }

            txtCode.Enabled = false;
            txtName.Focus();

        }
        dtr.Close();

    }
    protected void btndelete_Click(object sender, EventArgs e)
    {
        if (hdnCompNo.Value.ToString().Trim() != "")
        {
            if (hdnConfirm.Value.ToString().Trim() == "1")
            {
                FinCashBookController objCBC = new FinCashBookController();
                if (Session["fin_yr"].ToString().Trim() != "")
                {
                    if (objCBC.DeleteComp_CashBook(txtCode.Text.ToString().Trim()) == 1)
                    {
                        txtCode.Focus();
                        objCommon.DisplayUserMessage(updpan, "Company/Cash Book Dropped Successfully.", this);
                        PopulateCompanyList();
                        Clear();
                        Session["comp_code"] = null;
                        Session["fin_yr"] = null;
                        return;
                    }
                }
            }

        }
        else
        {
            objCommon.DisplayUserMessage(updpan, "Please Select a company for dropping.", this);
            txtCode.Focus();
            return;

        }

    }
}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               