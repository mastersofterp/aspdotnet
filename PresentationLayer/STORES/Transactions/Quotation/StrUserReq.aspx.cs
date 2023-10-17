//===========================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : STORES
// PAGE NAME     : Str_user_Requisition.aspx                                                  
// CREATION DATE : 11-Sept-2009                                                        
// CREATED BY    : G.V.S. KIRAN                                                         
// MODIFIED DATE :
// MODIFIED DESC :
//============================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.IO;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Web;
using System.Configuration;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using IITMS.SQLServer.SQLDAL;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage;
using IITMS.UAIMS.NonAcadBusinessLogicLayer.BusinessLogic;



public partial class STORES_Transactions_Quotation_StrUserReq : System.Web.UI.Page
{
    //CREATING OBJECTS OF CLASS FILES COMMON,UAIMS_COMMON
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    STR_DEPT_REQ_CONTROLLER objDeptReqController = new STR_DEPT_REQ_CONTROLLER();


    public string Docpath = ConfigurationManager.AppSettings["DirPath"];
    public static string RETPATH = "";
    public string path = string.Empty;
    public string dbPath = string.Empty;
    BlobController objBlob = new BlobController();

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
                Session["usertype"] == null || Session["userfullname"] == null || Session["strdeptname"] == null)
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
                    //  lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }

                //lbluser.Text = Session["userfullname"].ToString();
                //lblDept.Text = Session["strdeptname"].ToString();
                // lblPassingPath.Text = objCommon.LookUp("STORE_PASSING_AUTHORITY_PATH", "PAPATH", "DEPTNO=" + Convert.ToInt32(Session["strdeptcode"].ToString()));

                string Dept_Wise_Item = objCommon.LookUp("STORE_REFERENCE", "ISNULL(DEPT_WISE_ITEM,0) as DEPT_WISE_ITEM", "");
                ViewState["Dept_Wise_Item"] = Dept_Wise_Item;

                PnlReport.Visible = false;
                ViewState["StoreUser"] = null;
                this.CheckMainStoreUser();
                // this.FillItems();
                this.FillDept();
                this.FillBudget();
                //this.FillIndentSlipNo();
                this.FillRequisitionNo();
                this.EnableDisabledIndentSlipNo("ISNOTPOSTBACK");
                ViewState["action"] = "add";
                ViewState["butAction"] = "add";
                txtIndentSlipDate.Text = DateTime.Now.Date.ToShortDateString();
                txtPersonName.Text = Session["userfullname"].ToString();
                CheckBudgetHeadConfiguration();
                BlobDetails();
                //lblPassingPath.Text = objCommon.LookUp("STORE_PASSING_AUTHORITY_PATH", "PAPATH", "ISNULL(PATH_FOR,'P')='P' AND EMP_IDNO=" + Convert.ToInt32(Session["userno"]));
                //if (lblPassingPath.Text == string.Empty)
                //{
                //    Showmessage("Passing path is not defined.");
                //    PnlIndentDetails.Enabled = false;
                //    pnlItemDetails.Enabled = false;
                //    pnlReqFor.Enabled = false;
                //}

                //if (ViewState["StoreUser"].ToString() == "MainStoreUser")
                //{
                //    lblPassingPath.Text = objCommon.LookUp("STORE_PASSING_AUTHORITY_PATH", "PAPATH", "DEPTNO=" + Convert.ToInt32(Application["strrefmaindept"].ToString()));
                //    //lblPassingPath.Text = objCommon.LookUp("STORE_PASSING_AUTHORITY_PATH", "PAPATH", "DEPTNO=" + Convert.ToInt32(Session["strdeptcode"].ToString()));
                //}
                //else if (ViewState["StoreUser"].ToString() == "DeptStoreUser")
                //{
                //    lblPassingPath.Text = objCommon.LookUp("STORE_PASSING_AUTHORITY_PATH", "PAPATH", "DEPTNO=" + Convert.ToInt32(Application["strrefmaindept"].ToString()));
                //}
                //else
                //{
                //    lblPassingPath.Text = objCommon.LookUp("STORE_PASSING_AUTHORITY_PATH", "PAPATH", "DEPTNO=" + Convert.ToInt32(Session["strdeptcode"].ToString()));
                //}
            }
        }

        if (PnlReport.Visible == true)
        {
            //objCommon.ReportPopUp(btnReport, "pagetitle=UAIMS&path=~" + "," + "Reports" + "," + "Store" + "," + "Str_Indent_Slip.rpt&param=@username=" + Session["userfullname"].ToString() + "," + "@P_REQTRNO=" + Convert.ToInt32(ddlReportIndentSlipNo.SelectedValue) + "," + "@P_COLLEGE_CODE=" + Session["colcode"].ToString(), "UAIMS");
            //objCommon.ReportPopUp(btnSpecificationDoc, "pagetitle=UAIMS&pathb7]=~" + "," + "Reports" + "," + "Store" + "," + "Str_Indent_Text_File.rpt&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@username=" + Session["userfullname"].ToString() + "," + "@P_REQTRNO=" + Convert.ToInt32(ddlReportIndentSlipNo.SelectedValue), "UAIMS");
        }

    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=create_user.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=create_user.aspx");
        }
    }

    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }

    private void BlobDetails()
    {
        try
        {
            string Commandtype = "ContainerNamestoresdoctest";
            DataSet ds = objBlob.GetBlobInfo(Convert.ToInt32(Session["OrgId"]), Commandtype);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataSet dsConnection = objBlob.GetConnectionString(Convert.ToInt32(Session["OrgId"]), Commandtype);
                string blob_ConStr = dsConnection.Tables[0].Rows[0]["BlobConnectionString"].ToString();
                string blob_ContainerName = ds.Tables[0].Rows[0]["CONTAINERVALUE"].ToString();
                // Session["blob_ConStr"] = blob_ConStr;
                // Session["blob_ContainerName"] = blob_ContainerName;
                hdnBlobCon.Value = blob_ConStr;
                hdnBlobContainer.Value = blob_ContainerName;
                lblBlobConnectiontring.Text = Convert.ToString(hdnBlobCon.Value);
                lblBlobContainer.Text = Convert.ToString(hdnBlobContainer.Value);
            }
            else
            {
                hdnBlobCon.Value = string.Empty;
                hdnBlobContainer.Value = string.Empty;
                lblBlobConnectiontring.Text = string.Empty;
                lblBlobContainer.Text = string.Empty;
            }

        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void butAddItem_Click(object sender, EventArgs e)
    {
        int reqTrno = 0;
        try
        {
            if (ViewState["butAction"].ToString().Equals("Modify"))
            {
                if (ddlIndentSlipNo.SelectedValue == "0")
                {
                    Showmessage("Please Select Requisition Slip No.");
                    return;
                }
            }
            //Added by gopal anthati on 12-12-2020 To avoid concurrency issue
            int REQ_NO = Convert.ToInt32(objCommon.LookUp("STORE_REQ_MAIN", "COUNT(*)", "REQ_NO = '" + txtIndentSlipNo.Text + "' AND UANO != " + Convert.ToInt32(Session["userno"])));
            if (REQ_NO > 0)
            {
                Showmessage("This Requisition Slip Number Is Already Exist , Please Refresh The Page To Continue.");
                return;
            }
            if (Convert.ToInt32(txtRequiredQty.Text) == 0)
            {
                Showmessage("Required Qty Should Be Greater Than Zero.");
                return;
            }
            // check for Budget amount and each Item amount when adding. Item amount should be less than or equal to budget amount.
            if (txtRate.Text != string.Empty && txtRate.Text != "0.00")
            {
                int qty = Convert.ToInt32(txtRequiredQty.Text);
                decimal rate = Convert.ToDecimal(txtRate.Text);
                decimal amount = Convert.ToDecimal(qty * rate); // amount added for each item
                decimal budgetBalance = lblBudgetBalAmt.Text == "" ? 0 : Convert.ToDecimal(lblBudgetBalAmt.Text); // budget balance amount from accounts module.
                decimal inprocessAmount = lblInprocessAmt.Text == "" ? 0 : Convert.ToDecimal(lblInprocessAmt.Text); // inprocess requisition amount.
                decimal remainingAmount = Convert.ToDecimal(budgetBalance); // - inprocessAmount);
                decimal addedItemAmount = 0;
                decimal modifyItemAmount = 0;
                string REQTRNO = "";
                //current requisition number.
                if (ddlIndentSlipNo.SelectedValue == null || ddlIndentSlipNo.SelectedValue == "0")
                {
                    REQTRNO = objCommon.LookUp("STORE_REQ_MAIN", "REQTRNO", "REQ_NO='" + txtIndentSlipNo.Text + "'");
                }
                else
                {
                    REQTRNO = ddlIndentSlipNo.SelectedValue;
                }
                // item already added in the same requisition that amount
                if (REQTRNO != "")
                {
                    addedItemAmount = Convert.ToDecimal(objCommon.LookUp("STORE_REQ_TRAN", "ISNULL(SUM(REQ_QTY*RATE),0)", "REQTRNO=" + REQTRNO));
                }

                if (ViewState["reqTrno"] != null)
                {
                    modifyItemAmount = Convert.ToDecimal(objCommon.LookUp("STORE_REQ_TRAN", "ISNULL(SUM(REQ_QTY*RATE),0)", "REQ_TNO=" + Convert.ToInt32(ViewState["reqTrno"])));
                    addedItemAmount = Convert.ToDecimal(addedItemAmount - modifyItemAmount);
                }
                //-----------------------------------------------------------------------------------------------------------------
                decimal actualBalanceAmt = Convert.ToDecimal(remainingAmount - addedItemAmount);

                if (objCommon.LookUp("STORE_reference", "isnull(IS_BUDGET_HEAD,0)IS_BUDGET_HEAD", "IS_BUDGET_HEAD=1").Trim() == "1")
                {

                    if (amount > actualBalanceAmt)
                    {
                        Showmessage("Budget balance amount is insufficient.");
                        return;
                    }
                }
                //-----------------------------------------------------------------------------------------------------------------
            }
            // check for Budget amount end


            Str_DeptRequest objdeptRequest = new Str_DeptRequest();
            objdeptRequest.COLLEGE_CODE = Session["colcode"].ToString();




            ///----------------------------------------------------------------------------------------------------------------------
            if (objCommon.LookUp("STORE_reference", "isnull(IS_BUDGET_HEAD,0)IS_BUDGET_HEAD", "IS_BUDGET_HEAD=1").Trim() == "1")
            {

                if (ddlBudgetHead.SelectedValue != "0" || ddlBudgetHead.SelectedValue != null || ddlBudgetHead.SelectedValue != "")
                {
                    objdeptRequest.BHALNO = Convert.ToInt32(ddlBudgetHead.SelectedValue);
                }
                else
                {
                    Showmessage("Please Select Budget Head.");
                    return;
                }
            }
            else
            {
                objdeptRequest.BHALNO = 0;
            
            }
            //if (ddlBudgetHead.SelectedValue != "0" || ddlBudgetHead.SelectedValue != null || ddlBudgetHead.SelectedValue != "")
            //{
            //    objdeptRequest.BHALNO = Convert.ToInt32(ddlBudgetHead.SelectedValue);
            //}
            //else
            //{
            //    objdeptRequest.BHALNO = 0;
            //}
           // -----------------------------------------------------------------------------------------------------------------------------

            objdeptRequest.SDNO = Convert.ToInt32(ddlDepartment.SelectedValue);
            objdeptRequest.REQ_DATE = Convert.ToDateTime(txtIndentSlipDate.Text);
            if (ViewState["butAction"].ToString().Equals("Modify"))
            {
                string[] name = ddlIndentSlipNo.SelectedItem.Text.Split('-');
                //objdeptRequest.REQ_NO = ddlIndentSlipNo.SelectedItem.ToString();
                objdeptRequest.REQ_NO = name[0];
            }
            else
            {
                objdeptRequest.REQ_NO = txtIndentSlipNo.Text;
            }

            objdeptRequest.STATUS = 'N';

            if (ViewState["StoreUser"].ToString() == "MainStoreUser")
            {
                objdeptRequest.STORE_USER_TYPE = 'M';
            }
            else if (ViewState["StoreUser"].ToString() == "DeptStoreUser")
            {
                objdeptRequest.STORE_USER_TYPE = 'D';
            }
            else
            {
                objdeptRequest.STORE_USER_TYPE = 'N';
            }

            objdeptRequest.REMARK = txtIndentRemark.Text;
            objdeptRequest.NAME = txtPersonName.Text;
            if (radTEQIP.Checked)
            {
                objdeptRequest.TEQIP = true;
            }
            else
            {
                objdeptRequest.TEQIP = false;
            }
            objdeptRequest.ITEMNO = Convert.ToInt32(ddlItemName.SelectedValue);

            ////CHECK ITEM APPROVAL
            string itemapproval = Convert.ToString(objCommon.LookUp("STORE_ITEM", "APPROVAL", "Item_no=" + Convert.ToInt32(ddlItemName.SelectedValue)));
            objdeptRequest.STAPPROVAL = "P";


            objdeptRequest.REQ_QTY = Convert.ToInt32(txtRequiredQty.Text);
            if (txtRate.Text == string.Empty)
            {
                objdeptRequest.RATE = Convert.ToDecimal(0);
            }
            else
            {
                objdeptRequest.RATE = Convert.ToDecimal(txtRate.Text);
                //objdeptRequest.TECHSPEC = this.SpecificationDoc();
            }

            objdeptRequest.REMARKTRN = txtRemark.Text;
            objdeptRequest.ITEM_SPECIFICATION = txtItemSpecification.Text;
            //=================== the commented part was used to upload only text file
            //string specdoc = Convert.ToString(objCommon.ReadTextFile(FileUpload1));
            //if (specdoc == "-2" || specdoc == "-3")
            //{
            //    if (Convert.ToString(objCommon.ReadTextFile(FileUpload1)) == "-2")
            //    {
            //        //  DisplayMessage("Please Uplaod Text File only");
            //        Showmessage("Please Uplaod Text File only");
            //        return;
            //    }
            //    else
            //    {
            //        //DisplayMessage("Please Upload Text File less than or equal to " + System.Configuration.ConfigurationManager.AppSettings["TextFileSize-MB-GB"] + " Of Size");
            //        Showmessage("Please Upload Text File less than or equal to " + System.Configuration.ConfigurationManager.AppSettings["TextFileSize-MB-GB"] + " Of Size");
            //        return;
            //    }
            //}
            //else
            //{
            //    if (specdoc == "-1")
            //    {
            //        objdeptRequest.TECHSPEC = null;
            //    }
            //    else
            //    {
            //        objdeptRequest.TECHSPEC = specdoc;
            //    }
            //}
            //============ End Commented part ===================================================

            //============ start file upload =====================================
            //============ Comment Start Gaurav Varma =====================================
            //string file = string.Empty;
            //string filename = string.Empty;
            //if (FileUpload1.HasFile)
            //{
            //    if (FileTypeValid(System.IO.Path.GetExtension(FileUpload1.FileName)))
            //    {
            //        if (ddlIndentSlipNo.SelectedIndex == 0)
            //        {
            //            file = Docpath + "STORES\\REQUISITIONS\\REQ_" + (txtIndentSlipNo.Text).Replace('/', '_').Replace(' ', '_') + "\\ITEM_" + ddlItemName.SelectedItem.Text.Trim();
            //        }
            //        else
            //        {
            //            //file = Docpath + "STORES\\REQUISITIONS\\REQ_" + (ddlIndentSlipNo.SelectedItem.Text).Replace('/', '_').Replace(' ', '_') + "\\ITEM_" + ddlItemName.SelectedItem.Text.Trim();
            //            file = Docpath + "STORES\\REQUISITIONS\\REQ_" + (objdeptRequest.REQ_NO).Replace('/', '_').Replace(' ', '_') + "\\ITEM_" + ddlItemName.SelectedItem.Text.Trim();
            //        }
            //        if (!System.IO.Directory.Exists(file))
            //        {
            //            System.IO.Directory.CreateDirectory(file);
            //        }


            //        ViewState["FILE_PATH"] = file;
            //        dbPath = file;
            //        filename = FileUpload1.FileName;
            //        ViewState["FILE_NAME"] = filename;

            //        path = file + "\\" + FileUpload1.FileName;

            //        //CHECKING FOLDER EXISTS OR NOT file
            //        if (!System.IO.Directory.Exists(path))
            //        {
            //            if (!File.Exists(path))
            //            {
            //                FileUpload1.PostedFile.SaveAs(path);
            //            }
            //            else
            //            {
            //                Showmessage("File Already Exist.");
            //                return;
            //            }
            //        }
            //    }
            //    else
            //    {
            //        objCommon.DisplayUserMessage(this.Page, "Please Upload Valid Files[.pdf,.xls,.doc,.txt]", this.Page);
            //        FileUpload1.Focus();
            //        return;
            //    }
            //}
            //============ Comment End Gaurav Varma =====================================

            //  int idno = _idnoEmp;
            // file upload on Blobe start Gaurav
            string file = string.Empty;
            ServiceBook objSevBook = new ServiceBook();
  
            if (FileUpload1.HasFile)
            {
                if (FileTypeValid(System.IO.Path.GetExtension(FileUpload1.FileName)))
                {
                    if (FileUpload1.HasFile)
                    {
                        if (FileUpload1.FileContent.Length >= 1024 * 10000)
                        {

                            MessageBox("File Size Should Not Be Greater Than 10 Mb");
                            FileUpload1.Dispose();
                            FileUpload1.Focus();
                            return;
                        }
                    }

                    string FileName = FileUpload1.FileName;
                    if (ViewState["FILE1"] != null && ((DataTable)ViewState["FILE1"]) != null)
                    {
                        DataTable dtM = (DataTable)ViewState["FILE1"];
                        for (int i = 0; i < dtM.Rows.Count; i++)
                        {
                            if (dtM.Rows[i]["DisplayFileName"].ToString() == FileName)
                            {
                                MessageBox("File Already Exist!");
                                return;
                            }
                        }
                    }
                    int req_id = 0;
                    int reqid1 = 0;

                    if (ViewState["REQTRNO"] == null)
                    {
                        req_id = Convert.ToInt32(objCommon.LookUp("STORE_REQ_MAIN", "isnull(MAX(REQTRNO)+1,0) REQTRNO", ""));
                        reqid1 = Convert.ToInt32(objCommon.LookUp("STORE_REQ_TRAN", "isnull(MAX(REQ_TNO)+1,0) REQ_TNO", ""));
                    }
                    else
                    {
                        req_id = Convert.ToInt32(ViewState["REQTRNO"]);


                    }
                     file = Docpath + "TEMP_CONDUCTTRAINING_FILES\\APP_0";
                    ViewState["SOURCE_FILE_PATH"] = file;
                    string PATH = Docpath + "TRAINING_CONDUCTED\\";
                    ViewState["DESTINATION_PATH"] = PATH;
                    if (lblBlobConnectiontring.Text == "")
                    {
                        objSevBook.ISBLOB = 0;
                    }
                    else
                    {
                        objSevBook.ISBLOB = 1;
                    }
                    if (objSevBook.ISBLOB == 1)
                    {
                        string filename = string.Empty;
                        string FilePath = string.Empty;
                        // string IdNo = _idnoEmp.ToString();
                        if (FileUpload1.HasFile)
                        {
                            string contentType = contentType = FileUpload1.PostedFile.ContentType;
                            string ext = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName);
                            //HttpPostedFile file = flupld.PostedFile;
                            //filename = objSevBook.IDNO + "_familyinfo" + ext;
                            //string name = DateTime.Now.ToString("ddMMyyyy_hhmmss");
                            string time = DateTime.Now.ToString("MMddyyyyhhmmssfff");
                            filename = req_id + "_REQTRNO_" + time + ext;
                            objSevBook.ATTACHMENTS = filename;

                            if (FileUpload1.FileContent.Length <= 1024 * 10000)
                            {
                                string blob_ConStr = Convert.ToString(lblBlobConnectiontring.Text).Trim();
                                string blob_ContainerName = Convert.ToString(lblBlobContainer.Text).Trim();
                                bool result = objBlob.CheckBlobExists(blob_ConStr, blob_ContainerName);

                                if (result == true)
                                {

                                    int retval = objBlob.Blob_Upload(blob_ConStr, blob_ContainerName, req_id + "_REQTRNO_" + time, FileUpload1);
                                    if (retval == 0)
                                    {
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Unable to upload...Please try again...');", true);
                                        return;
                                    }
                                    //int tano = Addfieldstotbl(filename);
                                    //BindListView_Attachments();
                                }
                            }
                        }
                    }
                    else
                    {
                        string filename = FileUpload1.FileName;
                        if (!System.IO.Directory.Exists(file))
                        {
                            System.IO.Directory.CreateDirectory(file);
                        }

                        if (!System.IO.Directory.Exists(path))
                        {
                            if (!File.Exists(path))
                            {
                                int tano = Addfieldstotbl(filename);
                                path = file + "\\TC_" + tano + System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName);
                                FileUpload1.PostedFile.SaveAs(path);
                            }
                        }
                    }
                }
                else
                {
                    objCommon.DisplayMessage(this.Page, "Please Upload Valid Files[.jpg,.pdf,.xls,.doc,.txt]", this.Page);
                    FileUpload1.Focus();
                }
            }
            //else
            //{
            ////    objCommon.DisplayMessage(this.Page, "Please Select File", this.Page);
               
            //}
            // file upload on Blobe end Gaurav

            if (ddlIndentSlipNo.SelectedValue == null || ddlIndentSlipNo.SelectedValue == "0")
            {
                if (txtIndentSlipNo.Text != null && txtIndentSlipNo.Text != string.Empty && txtIndentSlipNo.Text != "")
                {
                    int count = Convert.ToInt32(objCommon.LookUp("STORE_REQ_MAIN", "count(*)", "REQ_NO='" + txtIndentSlipNo.Text + "'"));
                    if (count > 0)
                    {
                        reqTrno = Convert.ToInt32(objCommon.LookUp("STORE_REQ_MAIN", "REQTRNO", "REQ_NO='" + txtIndentSlipNo.Text + "'"));
                    }
                    else
                    {
                        reqTrno = 0;
                    }
                }
            }
            else
            {
                reqTrno = Convert.ToInt32(ddlIndentSlipNo.SelectedValue);
            }

            objdeptRequest.UANO = Convert.ToInt32(Session["userno"]);
            objdeptRequest.REQ_FOR = Convert.ToChar(rdbReqFor.SelectedValue);
            //Check whether to add or update
            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    int duplicateCkeck = Convert.ToInt32(objCommon.LookUp("STORE_REQ_TRAN", " count(*)", "Item_no=" + Convert.ToInt32(ddlItemName.SelectedValue) + "and reqtrno=" + reqTrno));

                    if (duplicateCkeck == 0)
                    {
                       // objdeptRequest.FILENAME = FileUpload1.FileName;
                        if (objSevBook.ATTACHMENTS != null)
                        {
                            objdeptRequest.FILENAME = objSevBook.ATTACHMENTS;
                        }
                        else
                        {
                            objdeptRequest.FILENAME = string.Empty;
                        }
                        objdeptRequest.FILEPTH = file;
                      
                        CustomStatus cs = (CustomStatus)objDeptReqController.AddDeptRequest(objdeptRequest);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            ViewState["action"] = "add";
                            this.ClearItem();
                            BindListView_ItemDetails();
                            //Showmessage("Item Added Successfully.");
                        }
                            
                    }
                    else
                    {
                        Showmessage("Item Already Exist.");
                    }
                }
                else
                {
                    if (ViewState["reqTrno"] != null)
                    {
                        int duplicateCkeck = Convert.ToInt32(objCommon.LookUp("STORE_REQ_TRAN", " count(1)", "Item_no=" + Convert.ToInt32(ddlItemName.SelectedValue) + "and reqtrno=" + reqTrno + "and req_tno <>" + Convert.ToInt32(ViewState["reqTrno"].ToString())));

                        if (duplicateCkeck == 0)
                        {
                            if (FileUpload1.HasFile)
                            {
                                //objdeptRequest.FILENAME = FileUpload1.FileName;
                                if (objSevBook.ATTACHMENTS != null)
                                {
                                    objdeptRequest.FILENAME = objSevBook.ATTACHMENTS;
                                }
                                else
                                {
                                    objdeptRequest.FILENAME = string.Empty;
                                }
                                objdeptRequest.FILEPTH = file;
                            }
                            else
                            {
                                objdeptRequest.FILENAME = ViewState["FileName"].ToString();
                                objdeptRequest.FILEPTH = ViewState["FilePath"].ToString();
                            }
                            CustomStatus cs = (CustomStatus)objDeptReqController.DeleteItem(Convert.ToInt32(ViewState["reqTrno"].ToString()));
                            if (cs.Equals(CustomStatus.RecordDeleted))
                            {
                                CustomStatus csadd = (CustomStatus)objDeptReqController.AddDeptRequest(objdeptRequest);
                                if (csadd.Equals(CustomStatus.RecordSaved))
                                {
                                    ViewState["action"] = "add";
                                    ClearItem();
                                    BindListView_ItemDetails();
                                   // Showmessage("Item Updated Successfully.");
                                }
                            }
                        }
                        else
                        {
                            Showmessage("Item Already Exist");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Stores_Transactions_Str_user_Requisition.butSubmit_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");

            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Complaints_TRANSACTION_Eapplication.btnAdd_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            ViewState["reqTrno"] = int.Parse(btnEdit.CommandArgument);
            ViewState["action"] = "edit";
            ShowEditDetails(Convert.ToInt32(ViewState["reqTrno"].ToString()));
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Stores_Transactions_Str_user_Requisition.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowEditDetails(int reqTrno)
    {
        DataSet ds = null;
        try
        {
            ds = objDeptReqController.GetItemDetails(reqTrno);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlItemName.SelectedValue = ds.Tables[0].Rows[0]["ITEM_NO"].ToString();
                txtRequiredQty.Text = ds.Tables[0].Rows[0]["REQ_QTY"].ToString();
                txtRate.Text = ds.Tables[0].Rows[0]["RATE"].ToString();
                txtRemark.Text = ds.Tables[0].Rows[0]["REMARK"].ToString();
                txtItemSpecification.Text = ds.Tables[0].Rows[0]["ITEM_SPECIFICATION"].ToString();
                //   divTecSpe.Visible = true;
                txtTechSpe.Text = ds.Tables[0].Rows[0]["TECHSPEC"].ToString();
                lblFileName.Text = "Attachment:" + ds.Tables[0].Rows[0]["FILENAME"].ToString();
                ViewState["FileName"] = ds.Tables[0].Rows[0]["FILENAME"].ToString();
                ViewState["FilePath"] = ds.Tables[0].Rows[0]["FILEPATH"].ToString();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Stores_Transactions_Str_user_Requisition.ShowEditDetails-> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowModifyDetails(int reqTrno)
    {
        DataSet ds = null;
        try
        {
            ds = objDeptReqController.GetDeptRequestByReqNo(reqTrno);
            if (ds.Tables[0].Rows.Count > 0)
            {
                //int instPaidCount = Convert.ToInt32(objCommon.LookUp("PAYROLL_INSTALMENT", "PAIDNO", "INO=" + ino));
                txtIndentSlipDate.Text = Convert.ToString(ds.Tables[0].Rows[0]["REQ_DATE"].ToString());
                txtIndentRemark.Text = ds.Tables[0].Rows[0]["REMARK"].ToString();
                ddlDepartment.SelectedValue = ds.Tables[0].Rows[0]["SDNO"].ToString();
                ddlBudgetHead.SelectedValue = ds.Tables[0].Rows[0]["BHALNO"].ToString();
                txtPersonName.Text = ds.Tables[0].Rows[0]["NAME"].ToString();
                if (Convert.ToBoolean(ds.Tables[0].Rows[0]["TEQIP"].ToString()))
                {
                    radTEQIP.Checked = true;
                    radInstitute.Checked = false;
                }
                else
                {
                    radInstitute.Checked = true;
                    radTEQIP.Checked = false;
                }
                if (Convert.ToChar(ds.Tables[0].Rows[0]["REQ_FOR"]) == 'I')
                {
                    rdbReqFor.SelectedValue = "I";
                }
                else
                {
                    rdbReqFor.SelectedValue = "P";
                }
            }
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Stores_Transactions_Str_user_Requisition.ShowModifyDetails-> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnDel = sender as ImageButton;
            int reqTrno = int.Parse(btnDel.CommandArgument);
            //DataSet ds = objCommon.FillDropDown("STORE_REQ_TRAN A INNER JOIN STORE_ITEM B ON (A.ITEM_NO=B.ITEM_NO)", "A.FILENAME", "B.ITEM_NAME", "REQ_TNO = " + reqTrno, "");
            //string file = string.Empty;
            //if (ddlIndentSlipNo.SelectedIndex == 0)
            //{
            //    file = Docpath + "STORES\\REQUISITIONS\\REQ_" + (txtIndentSlipNo.Text).Replace('/', '_').Replace(' ', '_') + "\\ITEM_" + ds.Tables[0].Rows[0]["ITEM_NAME"].ToString().Trim();
            //}
            //else
            //{
            //    file = Docpath + "STORES\\REQUISITIONS\\REQ_" + (ddlIndentSlipNo.SelectedItem.Text).Replace('/', '_').Replace(' ', '_') + "\\ITEM_" + ds.Tables[0].Rows[0]["ITEM_NAME"].ToString().Trim();
            //}
            //path = file + "/" + ds.Tables[0].Rows[0]["FILENAME"].ToString();
            //if (ds.Tables[0].Rows[0]["FILENAME"].ToString() != "")
            //{
            //    if (System.IO.Directory.Exists(path))
            //    {
            //        System.IO.Directory.Delete(path);
            //    }
            //}
            //objinstall.INO = Convert.ToInt32(ViewState["ino"].ToString());
            CustomStatus cs = (CustomStatus)objDeptReqController.DeleteItem(reqTrno);
            if (cs.Equals(CustomStatus.RecordDeleted))
            {
                ViewState["action"] = "add";
                ClearItem();
                BindListView_ItemDetails();
                Showmessage("Record Deleted Successfully.");
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Stores_Transactions_Str_user_Requisition.btnDelete_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void butCancel_Click(object sender, EventArgs e)
    {
        this.ClearItem();
    }

    private void BindListView_ItemDetails()
    {
        DataSet ds = null;
        try
        {
            int reqTrno;

            if (ddlIndentSlipNo.SelectedValue == null || ddlIndentSlipNo.SelectedValue == "0")
            {
                if (txtIndentSlipNo.Text != null && txtIndentSlipNo.Text != string.Empty && txtIndentSlipNo.Text != "")
                    reqTrno = Convert.ToInt32(objCommon.LookUp("STORE_REQ_MAIN", "REQTRNO", "REQ_NO='" + txtIndentSlipNo.Text + "'"));
                else
                    reqTrno = 0;
            }
            else
            {
                reqTrno = Convert.ToInt32(ddlIndentSlipNo.SelectedValue);
            }

            ds = objDeptReqController.GetTranDetailsByReqNo(reqTrno);
            lvItemDetails.DataSource = ds;
            lvItemDetails.DataBind();
            lvItemDetails.Visible = true;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Stores_Transactions_Str_user_Requisition.BindListView_ItemDetails() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    public void ClearGrid()
    {
        DataSet ds = new DataSet();
        ds = objDeptReqController.GetTranDetailsByReqNo(0);
        lvItemDetails.DataSource = ds;
        lvItemDetails.DataBind();
    }

    private void FillItems()
    {
        int mdno = Convert.ToInt32(Session["strdeptcode"].ToString());
        try
        {
            if (ViewState["Dept_Wise_Item"].ToString() == "1")
            {
                if (rdbReqFor.SelectedValue == "I")
                {
                    objCommon.FillDropDownList(ddlItemName, "STORE_ITEM", "ITEM_NO", "ITEM_NAME", "MDNO=" + mdno, "ITEM_NAME");
                    //objCommon.FillDropDownList(ddlItemName, "STORE_ITEM I INNER JOIN STORE_MAIN_ITEM_GROUP MG ON (I.MIGNO = MG.MIGNO)", "I.ITEM_NO", "I.ITEM_NAME", "I.MDNO=1 AND MG.ITEM_TYPE !='F'", "I.ITEM_NAME");
                }
                else
                {
                    objCommon.FillDropDownList(ddlItemName, "STORE_ITEM", "ITEM_NO", "ITEM_NAME", "MDNO=" + mdno, "ITEM_NAME");
                    //objCommon.FillDropDownList(ddlItemName, "STORE_ITEM I INNER JOIN STORE_MAIN_ITEM_GROUP MG ON (I.MIGNO = MG.MIGNO)", "I.ITEM_NO", "I.ITEM_NAME", "MG.ITEM_TYPE ='F' AND I.MDNO=" + mdno, "I.ITEM_NAME");
                }
            }
            else
            {
                objCommon.FillDropDownList(ddlItemName, "STORE_ITEM", "DISTINCT ITEM_NO", "ITEM_NAME", "", "ITEM_NAME");
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Stores_Transactions_Str_user_Requisition.FillItems() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }


    //Check for Main Store User.
    private bool CheckMainStoreUser()
    {
        string test = Application["strrefmaindept"].ToString();
        string test1 = Session["strdeptcode"].ToString();

        if (Session["strdeptcode"].ToString() == Application["strrefmaindept"].ToString())
        {
            ViewState["StoreUser"] = "MainStoreUser";
            return true;
        }
        else
        {
            this.CheckDeptStoreUser();
            return false;
        }
    }

    //Check for Department Store User.
    private bool CheckDeptStoreUser()
    {
        string test = objCommon.LookUp("STORE_DEPARTMENTUSER", "APLNO", "UA_NO=" + Convert.ToInt32(Session["userno"]));

        // When department user is having approval level as Department Store means "4". It is fixed in Store Reference table.
        string deptStoreUser = objCommon.LookUp("STORE_REFERENCE", "DEPT_STORE_USER", "");

        if (test == deptStoreUser)
        {
            ViewState["StoreUser"] = "DeptStoreUser";
            return true;
        }
        else
        {
            ViewState["StoreUser"] = "NormalUser";
            return false;

        }
    }

    private void FillDept()
    {

        try
        {
            //main store user
            if (ViewState["StoreUser"].ToString() == "MainStoreUser")
            {
                objCommon.FillDropDownList(ddlDepartment, "STORE_SUBDEPARTMENT", "SDNO", "SDNAME", "MDNO='" + Convert.ToInt32(Session["strdeptcode"].ToString()) + "'", "SDNAME");
                ddlDepartment.SelectedValue = Session["strdeptcode"].ToString();
            }
            else if (ViewState["StoreUser"].ToString() == "DeptStoreUser")
            {
                // Departmental user                
                objCommon.FillDropDownList(ddlDepartment, "STORE_SUBDEPARTMENT", "SDNO", "SDNAME", "MDNO='" + Convert.ToInt32(Session["strdeptcode"].ToString()) + "'", "SDNAME");

                //ddlDepartment.SelectedValue = Session["strdeptcode"].ToString();
            }
            else
            {
                // Normal User
                objCommon.FillDropDownList(ddlDepartment, "STORE_SUBDEPARTMENT", "SDNO", "SDNAME", "MDNO='" + Convert.ToInt32(Session["strdeptcode"].ToString()) + "'", "SDNAME");
                //ddlDepartment.SelectedValue = Session["strdeptcode"].ToString();
            }

            //int sdno = Convert.ToInt32(objCommon.LookUp("STORE_SUBDEPARTMENT", "SDNO", "SDNAME="+(Session["strdeptname"].ToString())));

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Stores_Transactions_Str_user_Requisition.FillDept() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }

    }

    private void FillBudget()
    {
        try
        {
            // objCommon.FillDropDownList(ddlBudgetHead, "STORE_BUDGET_HEAD B,STORE_BUDGETHEAD_ALLOCTION A", "A.BHALNO", "B.BHNAME", "A.BHNO=B.BHNO", "BHNAME");
            objCommon.FillDropDownList(ddlBudgetHead, "[DBO].[ACC_BUDGET_HEAD_NEW] BP INNER JOIN  [DBO].[ACC_BUDGET_HEAD_NEW] BC ON (BP.BUDGET_NO = BC.PARENT_ID)", "BC.BUDGET_NO", "BP.BUDGET_HEAD+' - '+BC.BUDGET_HEAD+'('+BC.BUDGET_CODE+')' as BUDGET_HEAD", "", "BP.BUDGET_HEAD");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Stores_Transactions_Str_user_Requisition.FillBudget() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }

    }

    private void FillIndentSlipNo()
    {
        try
        {
            if (ViewState["StoreUser"].ToString() == "MainStoreUser")
            {
                if (rdbReqFor.SelectedValue == "P")
                {
                    objCommon.FillDropDownList(ddlIndentSlipNo, "STORE_REQ_MAIN", "REQTRNO", "REQ_NO+' '+ (CASE STAPPROVAL WHEN 'R' THEN '-- Rejected' ELSE '' END) as REQ_NO", "REQ_FOR='P' AND MDNO='" + Convert.ToInt32(Application["strrefmaindept"]) + "' and (STAPPROVAL<>'A' or STAPPROVAl IS NULL)", "REQTRNO DESC");
                }
                else
                {
                    objCommon.FillDropDownList(ddlIndentSlipNo, "STORE_REQ_MAIN", "REQTRNO", "REQ_NO", "REQ_FOR='I' AND MDNO='" + Convert.ToInt32(Application["strrefmaindept"]) + "' and (STAPPROVAL<>'A' or STAPPROVAl IS NULL)", "REQTRNO DESC");
                }

                objCommon.FillDropDownList(ddlReportIndentSlipNo, "STORE_REQ_MAIN", "REQTRNO", "REQ_NO", "STATUS='S' AND MDNO='" + Convert.ToInt32(Application["strrefmaindept"]) + "'", "REQTRNO DESC");
            }
            else if (ViewState["StoreUser"].ToString() == "DeptStoreUser")
            {
                //objCommon.FillDropDownList(ddlIndentSlipNo, "STORE_REQ_MAIN", "REQTRNO", "REQ_NO", "MDNO=" + Convert.ToInt32(Session["strdeptcode"].ToString()) + " AND (STAPPROVAL<>'A' or STAPPROVAl IS NULL)", "REQTRNO DESC");
                //objCommon.FillDropDownList(ddlReportIndentSlipNo, "STORE_REQ_MAIN", "REQTRNO", "REQ_NO", "MDNO=" + Convert.ToInt32(Session["strdeptcode"].ToString()), "REQTRNO DESC");
                if (rdbReqFor.SelectedValue == "P")
                {
                    objCommon.FillDropDownList(ddlIndentSlipNo, "STORE_REQ_MAIN", "REQTRNO", "REQ_NO", "REQ_FOR='P' AND  NAME='" + Session["userfullname"].ToString() + "' and (STAPPROVAL<>'A' or STAPPROVAl IS NULL)", "REQTRNO DESC");
                }
                else
                {
                    objCommon.FillDropDownList(ddlIndentSlipNo, "STORE_REQ_MAIN", "REQTRNO", "REQ_NO", "REQ_FOR='I' AND  NAME='" + Session["userfullname"].ToString() + "' and (STAPPROVAL<>'A' or STAPPROVAl IS NULL)", "REQTRNO DESC");
                }

                objCommon.FillDropDownList(ddlReportIndentSlipNo, "STORE_REQ_MAIN", "REQTRNO", "REQ_NO", "STATUS='S' AND MDNO=" + Convert.ToInt32(Session["strdeptcode"].ToString()), "REQTRNO DESC");

            }
            else // Normal User
            {
                if (rdbReqFor.SelectedValue == "P")
                {
                    objCommon.FillDropDownList(ddlIndentSlipNo, "STORE_REQ_MAIN", "REQTRNO", "REQ_NO", "REQ_FOR='P' AND  MDNO=" + Convert.ToInt32(Session["strdeptcode"].ToString()) + " AND NAME='" + Session["userfullname"].ToString() + "' and (STAPPROVAL<>'A' or STAPPROVAl IS NULL)", "REQTRNO DESC");
                }
                else
                {
                    objCommon.FillDropDownList(ddlIndentSlipNo, "STORE_REQ_MAIN", "REQTRNO", "REQ_NO", "REQ_FOR='I' AND  MDNO=" + Convert.ToInt32(Session["strdeptcode"].ToString()) + " AND NAME='" + Session["userfullname"].ToString() + "' and (STAPPROVAL<>'A' or STAPPROVAl IS NULL)", "REQTRNO DESC");
                }
                objCommon.FillDropDownList(ddlReportIndentSlipNo, "STORE_REQ_MAIN", "REQTRNO", "REQ_NO", "STATUS='S' AND MDNO=" + Convert.ToInt32(Session["strdeptcode"].ToString()) + "AND NAME='" + Session["userfullname"].ToString() + "'", "REQTRNO DESC");
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Stores_Transactions_Str_user_Requisition.FillBudget() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }

    }
    private void FillRequisitionNo()
    {  //-------------------09/04/2022-------------------------//
        //DataSet ds = new DataSet();
        //int mdno = 0;
        ////if (ViewState["StoreUser"].ToString() == "DeptStoreUser")
        ////{
        ////    mdno = Convert.ToInt32(Application["strrefmaindept"].ToString());
        ////}
        ////else
        ////{
        //mdno = Convert.ToInt32(Session["strdeptcode"].ToString());
        ////} 
        //ds = objDeptReqController.GenrateReq(mdno, Convert.ToInt32(Session["OrgId"]));  //09-03-2022 g
        //if (ds.Tables[0].Rows.Count > 0)
        //{
        //    txtIndentSlipNo.Text = Convert.ToString(ds.Tables[0].Rows[0]["REQNO"].ToString());
        //}
//-------------------------------------09/04/2022---------------------------------//
        DataSet ds = new DataSet();
        int mdno = 0;
        //if (ViewState["StoreUser"].ToString() == "DeptStoreUser")
        //{
        //    mdno = Convert.ToInt32(Application["strrefmaindept"].ToString());
        //}
        //else
        //{
        mdno = Convert.ToInt32(Session["strdeptcode"].ToString());
        //}
        string Reqtype = rdbReqFor.SelectedValue == "I" ? "ISSUE" : "PUR";

        ds = objDeptReqController.GenrateReq(mdno, Convert.ToInt32(Session["OrgId"]), Reqtype);  //09-03-2022 g
        if (ds.Tables[0].Rows.Count > 0)
        {
            txtIndentSlipNo.Text = Convert.ToString(ds.Tables[0].Rows[0]["REQNO"].ToString());
        }




    }

    private string SpecificationDoc()
    {
        TextReader trs = new StreamReader(@FileUpload1.FileContent);
        string specDoc = Convert.ToString(trs.ReadToEnd());
        return specDoc;
    }

    protected void ClearItem()
    {
        ddlItemName.SelectedValue = "0";
        txtRequiredQty.Text = string.Empty;
        txtRate.Text = string.Empty;
        txtRemark.Text = string.Empty;
        txtItemSpecification.Text = string.Empty;
        divTecSpe.Visible = false;
        ViewState["reqTrno"] = null;
        ViewState["action"] = "add";
        lblFileName.Text = string.Empty;
        ViewState["FileName"] = null;
        ViewState["FilePath"] = null;
        lvItemDetails.DataSource = null;
        lvItemDetails.DataBind();
        lvItemDetails.Visible = false;
    }

    protected void ClearIndentNo()
    {
        txtIndentSlipDate.Text = DateTime.Now.Date.ToShortDateString();       
        txtIndentRemark.Text = string.Empty;
        //ddlDepartment.SelectedValue = Session["strdeptcode"].ToString(); 
        ddlBudgetHead.SelectedValue = "0";
        txtPersonName.Text = string.Empty;
        ddlIndentSlipNo.SelectedValue = "0";
        txtIndentSlipNo.Text = string.Empty;
        txtPersonName.Text = Session["userfullname"].ToString();
        this.FillRequisitionNo();
        divTecSpe.Visible = false;
        lblBudgetBalAmt.Text = string.Empty;
        lblInprocessAmt.Text = string.Empty;
        divShowAmt.Visible = false;
        divInprocessAmt.Visible = false;
        ddlDepartment.SelectedIndex = 0;
        lbtUnit.Text = string.Empty;
    }

    //Display Jquery Message Window.
    //void DisplayMessage(string Message)
    //{
    //    string prompt = "<script>$(document).ready(function(){{$.prompt('{0}!');}});</script>";
    //    string message = string.Format(prompt, Message);
    //    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Confirmation", message, false);
    //}

    //For Message Box
    public void Showmessage(string message)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + message + "');", true);
    }

    bool validattioCriteria()
    {
        string Message = "";
        bool IsOK = true;
        if (!ViewState["butAction"].Equals("Modify"))
        {
            if (txtIndentSlipNo.Text == string.Empty)
            {
                IsOK = false;
                Message = "Enter Requisition No";
            }
        }
        if (ddlDepartment.SelectedValue == "0")
        {
            IsOK = false;
            Message = Message + "<br>Select Department";
        }
        if (ddlBudgetHead.SelectedValue == "0")
        {
            IsOK = false;
            Message = Message + "<br>Select Budget";
        }
        if (txtPersonName.Text == string.Empty)
        {
            IsOK = false;
            Message = Message + "<br>Enter Person Name";
        }

        if (IsOK == false)
        {
            // DisplayMessage(Message);
            Showmessage(Message);
        }
        return IsOK;
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("stores")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,STORES," + rptFileName;
            url += "&param=@P_REQTRNO=" + Convert.ToInt32(ddlReportIndentSlipNo.SelectedValue); // +",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_StudentRoolist.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void butAddNew_Click(object sender, EventArgs e)
    {
        ViewState["action"] = "add";
        ViewState["butAction"] = "";
        this.EnableDisabledIndentSlipNo("ADDNEW");
        Response.Redirect(Request.Url.ToString());
        this.FillRequisitionNo();
    }

    protected void butModify_Click(object sender, EventArgs e)
    {
        this.ClearIndentNo();
        this.ClearItem();
        this.FillIndentSlipNo();
        PnlIndentDetails.Visible = true;
        pnlItemDetails.Visible = true;
        ViewState["butAction"] = "Modify";
        this.EnableDisabledIndentSlipNo("Modify");
        // Response.Redirect(Request.Url.ToString());  
        pnlReqFor.Visible = true;

    }

    protected void butCancel1_Click(object sender, EventArgs e)
    {
        ViewState["action"] = "add";
        ViewState["butAction"] = "";
        Response.Redirect(Request.Url.ToString());
    }

    protected void butReport_Click(object sender, EventArgs e)
    {
        int mdno = Convert.ToInt32(Session["strdeptcode"].ToString());
        this.FillIndentSlipNo();
        PnlReport.Visible = true;
        PnlIndentDetails.Visible = false;
        pnlItemDetails.Visible = false;
        butAddNew.Visible = false;
        butModify.Visible = false;
        butReport.Visible = false;
        butCancel1.Visible = false;
        butSubmitReq.Visible = false;
        ViewState["butAction"] = "";
        pnlReqFor.Visible = false;

    }

    private void EnableDisabledIndentSlipNo(string action)
    {
        if (action.Equals("ADDNEW") || action.Equals("ISNOTPOSTBACK"))
        {
            ddlIndentSlipNo.Visible = false;
            txtIndentSlipNo.Visible = true;
        }
        else if (action.Equals("Modify"))
        {
            ddlIndentSlipNo.Visible = true;
            txtIndentSlipNo.Visible = false;
        }
    }

    protected void ddlIndentSlipNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (objCommon.LookUp("STORE_APP_PASS_ENTRY", "STATUS", "TRNO=" + ddlIndentSlipNo.SelectedValue) == "A")
        {
            ClearItem();
            //objCommon.DisplayMessage(updPanel, "This requisition in approval process. You can not modify", this);
            Showmessage("This requisition in approval process. You can not modify");
            //lvItemDetails.Enabled = false;
            pnlItemDetails.Enabled = false;
            ddlItemName.Enabled = false;
            butSubmitReq.Enabled = false;
        }
        else
        {
            pnlItemDetails.Enabled = true;
            //lvItemDetails.Enabled = true;
            ddlItemName.Enabled = true;
            butSubmitReq.Enabled = true;
        }

        this.BindListView_ItemDetails();
        this.ShowModifyDetails(Convert.ToInt32(ddlIndentSlipNo.SelectedValue));
        ddlBudgetHead_SelectedIndexChanged(sender, e);

    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    protected void butSubmitReq_Click(object sender, EventArgs e)
    {
        double Rate = 0.0;
        string IndentSlipNo = string.Empty;
        int reqtno = 0;
        string[] name = ddlIndentSlipNo.SelectedItem.Text.Split('-');
        if (lvItemDetails.Items.Count == 0)
        {
            Showmessage("Add items before submitting the requisition");
            return;
        }

        if (lblBudgetBalAmt.Text != string.Empty)
        {
            if (ViewState["butAction"].Equals("add"))
                Rate = Convert.ToDouble(objCommon.LookUp("STORE_REQ_TRAN T INNER JOIN STORE_REQ_MAIN M ON (T.REQTRNO=M.REQTRNO)", "SUM(RATE)", "REQ_NO='" + txtIndentSlipNo.Text + "'"));
            else
                Rate = Convert.ToDouble(objCommon.LookUp("STORE_REQ_TRAN T INNER JOIN STORE_REQ_MAIN M ON (T.REQTRNO=M.REQTRNO)", "SUM(RATE)", "REQ_NO='" + name[0] + "'"));

            if (Rate > Convert.ToDouble(lblBudgetBalAmt.Text))
            {
                Showmessage("Total Amount should not be greater than Budget Balance Amount.");
                return;
            }
        }

        if (ddlIndentSlipNo.SelectedValue != null && ddlIndentSlipNo.SelectedValue != "" && ddlIndentSlipNo.SelectedValue != "0")
        {
            //IndentSlipNo = ddlIndentSlipNo.SelectedItem.ToString();
            IndentSlipNo = name[0];
            reqtno = Convert.ToInt32(ddlIndentSlipNo.SelectedValue);
        }
        else
        {
            IndentSlipNo = txtIndentSlipNo.Text;
        }
        if (lblBudgetBalAmt.Text == "")
            lblBudgetBalAmt.Text = "0.0";

        if (lblInprocessAmt.Text == "")
            lblInprocessAmt.Text = "0.0";


        if (ViewState["butAction"].Equals("add"))
        {
            int duplicateCkeck = Convert.ToInt32(objCommon.LookUp("STORE_REQ_MAIN", " count(*)", "REQ_NO='" + IndentSlipNo + "' "));

            if (duplicateCkeck > 0)
            {

                CustomStatus cssubmit = (CustomStatus)objDeptReqController.SubmitRequisition(IndentSlipNo, 'S', Convert.ToDouble(lblBudgetBalAmt.Text), Convert.ToDouble(lblInprocessAmt.Text), txtIndentRemark.Text);
                if (cssubmit.Equals(CustomStatus.RecordUpdated))
                {
                    string trno = objCommon.LookUp("STORE_REQ_MAIN", "REQTRNO", "REQ_NO='" + IndentSlipNo + "'");

                    // Session["sanctioning_authority"]='Y' means internally sanctioning authorities which are not in passing path will insert in APP_ENTRY table 
                    // for approval. It is depend upon the Requisition Amount.
                    CustomStatus csStatus = (CustomStatus)objDeptReqController.insertStatus(Convert.ToInt32(trno), Convert.ToInt32(Session["strdeptcode"].ToString()), Convert.ToChar(Session["sanctioning_authority"]), Convert.ToChar(rdbReqFor.SelectedValue));
                    if (csStatus.Equals(CustomStatus.RecordUpdated))
                    {
                        //objCommon.DisplayMessage("Requisition Submitted Successfully", this.Page);
                        Showmessage("Requisition Submitted Successfully");
                        //lblmsg.Text = "Requisition Submited Successfully";

                        // if in store reference table IS_MAIL_SEND=1 means to send email otherwise, it will not send emails.
                        if (Convert.ToInt32(Session["Is_Mail_Send"]) == 1)
                        {
                            SendEmailToAuthority(Convert.ToInt32(trno));
                            SendEmailToRequisitionUser(Convert.ToInt32(trno));
                        }
                        pnlItemDetails.Visible = true;
                        txtIndentSlipNo.Text = string.Empty;
                        ddlDepartment.SelectedValue = "0";
                        this.ClearIndentNo();
                        this.ClearItem();
                        this.ClearGrid();
                    }

                }
            }
            else
            {
                // DisplayMessage("Add items before submitting the requisition");
                Showmessage("Add items before submitting the requisition");

            }
        }
        else
        {
            int duplicateCkeck = Convert.ToInt32(objCommon.LookUp("STORE_REQ_MAIN", " count(*)", "REQ_NO='" + IndentSlipNo + "' and REQTRNO <> " + reqtno));

            if (duplicateCkeck == 0)
            {
                string RejectStatus = objCommon.LookUp("STORE_REQ_MAIN", "STAPPROVAL", "REQTRNO=" + reqtno);
                CustomStatus cssubmit = (CustomStatus)objDeptReqController.UpdateRequisitionStatus(reqtno, "S", "N", Convert.ToDouble(lblBudgetBalAmt.Text), Convert.ToDouble(lblInprocessAmt.Text), txtIndentRemark.Text);
                if (cssubmit.Equals(CustomStatus.RecordUpdated))
                {
                    string trno = objCommon.LookUp("STORE_REQ_MAIN", "REQTRNO", "REQ_NO='" + IndentSlipNo + "'");
                    CustomStatus csStatus = (CustomStatus)objDeptReqController.insertStatus(Convert.ToInt32(trno), Convert.ToInt32(Session["strdeptcode"].ToString()), Convert.ToChar(Session["sanctioning_authority"]), Convert.ToChar(rdbReqFor.SelectedValue));
                    if (csStatus.Equals(CustomStatus.RecordUpdated))
                    {
                        if (RejectStatus == "R")
                        {
                            if (Convert.ToInt32(Session["Is_Mail_Send"]) == 1)
                            {
                                SendEmailToAuthority(Convert.ToInt32(trno));
                                SendEmailToRequisitionUser(Convert.ToInt32(trno));
                            }
                        }
                        // objCommon.DisplayMessage("Requisition Updated Successfully", this.Page);
                        Showmessage("Requisition Updated Successfully");
                        // lblmsg.Text = "Requisition Updated Successfully";
                        //if (Convert.ToInt32(Session["Is_Mail_Send"]) == 1)
                        //{
                        //    SendEmailToAuthority(Convert.ToInt32(trno));
                        //}
                        //pnlItemDetails.Visible = true;
                        txtIndentSlipNo.Text = string.Empty;
                        this.ClearIndentNo();
                        this.ClearItem();
                        this.ClearGrid();
                    }
                }
            }
            else
            {
                // DisplayMessage("Requisition Already exist.");
                Showmessage("Requisition Already exist.");
            }
        }

    }

    #region

    // it is used to send email to Requisition User.
    private void SendEmailToRequisitionUser(int TRNO)
    {
        try
        {
            string fromEmailId = string.Empty;
            string fromEmailPwd = string.Empty;

            string body = string.Empty;

            DataSet ds = objDeptReqController.GetDataForEmailToRequisitionUser(TRNO);
            if (ds.Tables[0].Rows.Count > 0)
            {
                fromEmailId = ds.Tables[1].Rows[0]["EMAILSVCID"].ToString();
                fromEmailPwd = ds.Tables[1].Rows[0]["EMAILSVCPWD"].ToString();

                string receiver = string.Empty;
                string mobilenum = string.Empty;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (receiver == string.Empty)
                    {
                        receiver = ds.Tables[0].Rows[i]["UA_EMAIL"].ToString();
                    }
                    else
                    {
                        receiver = receiver + "," + ds.Tables[0].Rows[i]["UA_EMAIL"].ToString();
                    }
                }
                sendmail(fromEmailId, fromEmailPwd, receiver, "New Requisition", "The above requisition is submitted successfully.", "");

            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }


    // it is used to send email to Approval Authority.
    private void SendEmailToAuthority(int TRNO)
    {
        try
        {
            string fromEmailId = string.Empty;
            string fromEmailPwd = string.Empty;

            string body = string.Empty;

            DataSet ds = objDeptReqController.GetFromDataForSendingEmail(TRNO);
            if (ds.Tables[0].Rows.Count > 0)
            {
                fromEmailId = ds.Tables[1].Rows[0]["EMAILSVCID"].ToString();
                fromEmailPwd = ds.Tables[1].Rows[0]["EMAILSVCPWD"].ToString();

                string receiver = string.Empty;
                string mobilenum = string.Empty;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (receiver == string.Empty)
                    {
                        receiver = ds.Tables[0].Rows[i]["UA_EMAIL"].ToString();
                    }
                    else
                    {
                        receiver = receiver + "," + ds.Tables[0].Rows[i]["UA_EMAIL"].ToString();
                    }
                }
                sendmail(fromEmailId, fromEmailPwd, receiver, "Requisition Approval", "is sending you for approval. It is available at your login.", Session["userfullname"].ToString());

            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }

    //  public void sendmail(string fromEmailId, string fromEmailPwd, string toEmailId, string Sub, string body, string userid)
    public void sendmail(string fromEmailId, string fromEmailPwd, string toEmailId, string Sub, string body, string username)
    {
        try
        {
            string msg = string.Empty;
            System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage();
            mailMessage.IsBodyHtml = true;
            mailMessage.Subject = Sub;
            string ReqSlipNo = string.Empty;
            // DataSet ds = objCommon.FillDropDown("FILE_FILEMASTER F INNER JOIN USER_ACC U ON (F.USERNO = U.UA_NO)", "F.FILE_ID, F.FILE_CODE, F.FILE_NAME, DESCRIPTION", "U.UA_FULLNAME, F.CREATION_DATE", "FILE_ID=" + Convert.ToInt32(ViewState["FILE_ID"]) + "", "");
            if (ddlIndentSlipNo.SelectedIndex > 0)
            {
                string[] name = ddlIndentSlipNo.SelectedItem.Text.Split('-');
                ReqSlipNo = name[0];
                //ReqSlipNo = ddlIndentSlipNo.SelectedItem.ToString();               
            }
            else
            {
                ReqSlipNo = txtIndentSlipNo.Text;
            }
            string MemberEmailId = string.Empty;
            mailMessage.From = new MailAddress(HttpUtility.HtmlEncode(fromEmailId));
            mailMessage.To.Add(toEmailId);

            var MailBody = new StringBuilder();
            MailBody.AppendFormat("Dear Sir, {0}\n", " ");
            MailBody.AppendLine(@"<br />Requisition Slip No. : " + ReqSlipNo);
            //  MailBody.AppendLine(@"<br />is sending you for approval. It is available at your login.");
            MailBody.AppendLine(@"<br />" + body);
            MailBody.AppendLine(@"<br /> ");
            MailBody.AppendLine(@"<br /> ");
            MailBody.AppendLine(@"<br /> ");
            MailBody.AppendLine(@"<br />Thanks And Regards");
            MailBody.AppendLine(@"<br />" + username);


            mailMessage.Body = MailBody.ToString();

            mailMessage.IsBodyHtml = true;
            SmtpClient smt = new SmtpClient("smtp.gmail.com");

            smt.UseDefaultCredentials = false;
            smt.Credentials = new NetworkCredential(HttpUtility.HtmlEncode(fromEmailId), HttpUtility.HtmlEncode(fromEmailPwd));
            smt.Port = 587;
            smt.EnableSsl = true;

            System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate(object s,
            System.Security.Cryptography.X509Certificates.X509Certificate certificate,
            System.Security.Cryptography.X509Certificates.X509Chain chain,
            System.Net.Security.SslPolicyErrors sslPolicyErrors)
            {
                return true;
            };

            smt.Send(mailMessage);

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }

    #endregion



    protected void btnReport_Click(object sender, EventArgs e)
    {
        //ShowReport("Quotation_Report", "Str_Indent_Slip.rpt");
        ShowReport("Quotation_Report", "StrDepartmentPraposal.rpt");
    }
    //To Show Quotation Entry report

    protected void btnSpecificationDoc_Click(object sender, EventArgs e)
    {
        ShowReport("Quotation_SpecialDoc", "Str_Indent_Text_File.rpt");
    }

    protected void ddlItemName_SelectedIndexChanged(object sender, EventArgs e)
    {
        lbtUnit.Text = objCommon.LookUp("STORE_ITEM", " ITEM_UNIT", "ITEM_NO='" + ddlItemName.SelectedValue + "' ");
        if (Convert.ToString(objCommon.LookUp("STORE_reference", "IsAvailableQty", "")) == "Y")
        {
            if (rdbReqFor.SelectedValue == "I")
            {
                Str_JvStockCon objJVCon = new Str_JvStockCon();
                DataSet dsAQty = objJVCon.GetItemAvlQty(Convert.ToInt32(ddlItemName.SelectedValue));
                txtAvailableQty.Text = dsAQty.Tables[0].Rows[0]["AVLQTY"].ToString();
                divAvailableQty.Visible = true; ;
            }
            else
            {
                divAvailableQty.Visible = false;
            }
        }
        else
        {
            divAvailableQty.Visible = false;
        }

    }

    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ViewState["StoreUser"].ToString() == "MainStoreUser")
        //{
        //    // lblPassingPath.Text = objCommon.LookUp("STORE_PASSING_AUTHORITY_PATH", "PAPATH", "DEPTNO=" + ddlDepartment.SelectedValue);      
        //    lblPassingPath.Text = objCommon.LookUp("STORE_PASSING_AUTHORITY_PATH", "PAPATH", "DEPTNO=" + Convert.ToInt32(Application["strrefmaindept"].ToString()));
        //}
        //else if (ViewState["StoreUser"].ToString() == "DeptStoreUser")
        //{
        //    lblPassingPath.Text = objCommon.LookUp("STORE_PASSING_AUTHORITY_PATH", "PAPATH", "DEPTNO=" + Convert.ToInt32(Session["strdeptcode"].ToString()));
        //}
        //else
        //{
        //    lblPassingPath.Text = objCommon.LookUp("STORE_PASSING_AUTHORITY_PATH", "PAPATH", "DEPTNO=" + Convert.ToInt32(Session["strdeptcode"].ToString()));
        //}

    }

    protected void rdbReqFor_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.ClearIndentNo();
            this.ClearItem();
            lvItemDetails.DataSource = null;
            lvItemDetails.Visible = false;
            ddlDepartment.SelectedIndex = 0;
            
            if (rdbReqFor.SelectedValue == "I")
            {
                lblPassingPath.Text = objCommon.LookUp("STORE_PASSING_AUTHORITY_PATH", "PAPATH", "PATH_FOR='I' AND EMP_IDNO=" + Convert.ToInt32(Session["userno"]));
                if (lblPassingPath.Text == string.Empty)
                {
                    Showmessage("Passing path is not defined for Issue.");
                    PnlIndentDetails.Visible = false;
                    pnlItemDetails.Visible = false;
                    //pnlReqFor.Visible = false;
                    // rdbReqFor.Visible = false;
                    return;
                }
                // divInstitute.Visible = false;
                divBudgetHead.Visible = false;
                divShowAmt.Visible = false;
                divInprocessAmt.Visible = false;
                ddlBudgetHead.SelectedIndex = 0;
                lblBudgetBalAmt.Text = string.Empty;
                lblInprocessAmt.Text = string.Empty;
                divAppRate.Visible = false;
                divPurJustification.Visible = false;
                // lblPassingPath.Text = objCommon.LookUp("STORE_PASSING_AUTHORITY_PATH", "PAPATH", "PATH_FOR ='I' AND EMP_IDNO=" + Convert.ToInt32(Session["userno"]));
                PnlIndentDetails.Visible = true;
                pnlItemDetails.Visible = true;
            }
            else
            {
                lblPassingPath.Text = objCommon.LookUp("STORE_PASSING_AUTHORITY_PATH", "PAPATH", "PATH_FOR='P' AND EMP_IDNO=" + Convert.ToInt32(Session["userno"]));
                if (lblPassingPath.Text == string.Empty)
                {
                    Showmessage("Passing path is not defined for Purchase.");
                    PnlIndentDetails.Visible = false;
                    pnlItemDetails.Visible = false;
                    //pnlReqFor.Visible = false;
                    // rdbReqFor.Visible = false;
                    return;
                }
                //divInstitute.Visible = true;
                //divBudgetHead.Visible = true;
                //divShowAmt.Visible = true;
                //divInprocessAmt.Visible = true;
                divAppRate.Visible = true;
                divPurJustification.Visible = false;
                // lblPassingPath.Text = objCommon.LookUp("STORE_PASSING_AUTHORITY_PATH", "PAPATH", "PATH_FOR ='P' AND EMP_IDNO=" + Convert.ToInt32(Session["userno"]));
                PnlIndentDetails.Visible = true;
                pnlItemDetails.Visible = true;
            }

            this.FillItems();
            this.FillIndentSlipNo();
            this.FillRequisitionNo(); //08/04/2022
            //---------------------------Added by shabina 27/01/2023---------available qty is optional--------//
            if (rdbReqFor.SelectedValue == "I")
            {
                divAvailableQty.Visible = true;
            }
            else
            {
                divAvailableQty.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Stores_Transactions_Str_user_Requisition.rdbReqFor_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }


    protected void ddlBudgetHead_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlDepartment.SelectedValue == "0")
            {
                Showmessage("Please Select Department First.");
                return;
            }
            if (txtIndentSlipDate.Text == string.Empty)
            {
                Showmessage("Please Select Requisition Slip Date.");
                ddlBudgetHead.SelectedIndex = 0;
                return;
            }
            else
            {
                DataSet ds = null;
                ds = objDeptReqController.GetBudgetBalance(Convert.ToInt32(ddlDepartment.SelectedValue), Convert.ToInt32(ddlBudgetHead.SelectedValue), Convert.ToDateTime(txtIndentSlipDate.Text), Convert.ToInt32(Session["strdeptcode"].ToString()));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lblBudgetBalAmt.Text = ds.Tables[0].Rows[0]["Balance"].ToString();
                    lblInprocessAmt.Text = ds.Tables[0].Rows[0]["INPROCESS_AMT"].ToString();
                }
                else
                {
                    lblBudgetBalAmt.Text = "0";
                }
                if (rdbReqFor.SelectedValue == "P")
                {
                    divShowAmt.Visible = true;
                    divInprocessAmt.Visible = true;
                }
            }
            if (rdbReqFor.SelectedValue == "P")
            {
                if (Convert.ToDecimal(lblBudgetBalAmt.Text) == 0 || lblBudgetBalAmt.Text == "" || Convert.ToDecimal(lblBudgetBalAmt.Text) < 0)
                {
                    ddlItemName.Enabled = false;
                    Showmessage("Selected budget balance amount is insufficient.");
                    return;
                }
                else
                {
                    ddlItemName.Enabled = true;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Stores_Transactions_Str_user_Requisition.ddlBudgetHead_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnTrackReq_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("~/STORES/Transactions/Quotation/RequisitionTrack.aspx?pageno=2655");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Stores_Transactions_Str_user_Requisition.ddlBudgetHead_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }

    }

    private void CheckBudgetHeadConfiguration()
    {

        if (objCommon.LookUp("STORE_reference", "isnull(IS_BUDGET_HEAD,0)IS_BUDGET_HEAD", "IS_BUDGET_HEAD=1").Trim() == "1")
        {

            divBudgetHead.Visible = true;
        }
        else
        {
            divBudgetHead.Visible = false;

        }
    }

    private void CreateTable()
    {
        DataTable dt = new DataTable();
        DataColumn dc;
        //dc = new DataColumn("SRNO", typeof(int));
        //dt.Columns.Add(dc);

        dc = new DataColumn("DisplayFileName", typeof(string));
        dt.Columns.Add(dc);


        dc = new DataColumn("FILENAME", typeof(string));
        dt.Columns.Add(dc);

        dc = new DataColumn("REQ_TNO", typeof(int));
        dt.Columns.Add(dc);
        ViewState["INV1"] = dt;
    }

    private int Addfieldstotbl(string filename)
    {
        if (ViewState["INV1"] != null && ((DataTable)ViewState["INV1"]) != null)
        {
            DataTable dt = (DataTable)ViewState["INV1"];
            DataRow dr = dt.NewRow();
            //int FUID = Convert.ToInt32(ViewState["FUID"]) + 1;
            //dr["SRNO"] = Convert.ToInt32(ViewState["FUID"]) + 1;
            dr["DisplayFileName"] = FileUpload1.FileName;
            dr["FILENAME"] = filename;
            
            dt.Rows.Add(dr);
            ViewState["FILE1"] = dt;
            this.BindListView_Attachments(dt);
            // ViewState["FUID"] = Convert.ToInt32(ViewState["FUID"]) + 1;
        }
        else
        {
            CreateTable();
            DataTable dt = (DataTable)ViewState["INV1"];
            DataRow dr = dt.NewRow();
            //int FUID = Convert.ToInt32(ViewState["FUID"]) + 1;
            //dr["SRNO"] = Convert.ToInt32(ViewState["FUID"]) + 1;
            dr["DisplayFileName"] = FileUpload1.FileName;
            dr["FILENAME"] = filename;
            // ViewState["FUID"] = Convert.ToInt32(ViewState["FUID"]) + 1;
            dt.Rows.Add(dr);
            ViewState["FILE1"] = dt;
            //pnlAttachmentList.Visible = true;
            this.BindListView_Attachments(dt);
        }
        return Convert.ToInt32(ViewState["FUID"]);
    }
    private void BindListView_Attachments(DataTable dt)
    {
        try
        {
            lvItemDetails.DataSource = dt;
            lvItemDetails.DataBind();
           // pnlAttachmentList.Visible = true;

            if (lblBlobConnectiontring.Text != "")
            {
                Control ctrHeader = lvItemDetails.FindControl("divBlobDownload");
                Control ctrHead1 = lvItemDetails.FindControl("divattachblob");
                Control ctrhead2 = lvItemDetails.FindControl("divattach");
                ctrHeader.Visible = true;
                ctrHead1.Visible = true;
                ctrhead2.Visible = false;

                foreach (ListViewItem lvRow in lvItemDetails.Items)
                {
                    Control ckBox = (Control)lvRow.FindControl("tdBlob");
                    Control ckattach = (Control)lvRow.FindControl("attachfile");
                    Control attachblob = (Control)lvRow.FindControl("attachblob");
                    ckBox.Visible = true;
                    attachblob.Visible = true;
                    ckattach.Visible = false;

                }
            }
            else
            {

                Control ctrHeader = lvItemDetails.FindControl("divDownload");
                ctrHeader.Visible = false;

                foreach (ListViewItem lvRow in lvItemDetails.Items)
                {
                    Control ckBox = (Control)lvRow.FindControl("tdDownloadLink");
                    ckBox.Visible = false;

                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_FeeCollection.BindListView_DemandDraftDetails() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    #region File Upload

    //Check for Valid File 
    public bool FileTypeValid(string FileExtention)
    {
        bool retVal = false;
        string[] Ext = { ".pdf", ".PDF", ".xls", ".XLS", ".doc", ".DOC", ".docx", ".TXT", ".txt" };
        foreach (string ValidExt in Ext)
        {
            if (FileExtention == ValidExt)
            {
                retVal = true;
            }
        }
        return retVal;
    }



    protected void imgdownload_Click(object sender, ImageClickEventArgs e)
    {
       // ImageButton btn = sender as ImageButton;

         string Url = string.Empty;
        string directoryPath = string.Empty;
        try
        {
            string blob_ConStr = Convert.ToString(lblBlobConnectiontring.Text).Trim();
            string blob_ContainerName = Convert.ToString(lblBlobContainer.Text).Trim();

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(blob_ConStr);
            CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();

            CloudBlobContainer blobContainer = cloudBlobClient.GetContainerReference(blob_ContainerName);
            string img = ((System.Web.UI.WebControls.ImageButton)(sender)).ToolTip.ToString();
            var ImageName = img;
            if (img == null || img == "")
            {


            }
            else
            {
                DataTable dtBlobPic = objBlob.Blob_GetById(blob_ConStr, blob_ContainerName, img);
                var blob = blobContainer.GetBlockBlobReference(ImageName);
                string url = dtBlobPic.Rows[0]["Uri"].ToString();
                //dtBlobPic.Tables[0].Rows[0]["course"].ToString();
                string Script = string.Empty;

                //string DocLink = "https://rcpitdocstorage.blob.core.windows.net/" + blob_ContainerName + "/" + blob.Name;
                string DocLink = url;
                //string DocLink = "https://rcpitdocstorage.blob.core.windows.net/" + blob_ContainerName + "/" + blob.Name;
                Script += " window.open('" + DocLink + "','PoP_Up','width=0,height=0,menubar=no,location=no,toolbar=no,scrollbars=1,resizable=yes,fullscreen=1');";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Report", Script, true);
            }
        } catch (Exception ex)
        {
            throw;
        }

        //DownloadFile(btn.AlternateText, btn.CommandName, btn.CommandArgument);
    }

    public void DownloadFile(string fileName, string ItemName, string FilePath)
    {
        try
        {
            if (fileName != "")
            {
                if (ddlIndentSlipNo.SelectedIndex == 0)
                {
                    path = Docpath + "STORES\\REQUISITIONS\\REQ_" + (txtIndentSlipNo.Text).Replace('/', '_').Replace(' ', '_') + "\\ITEM_" + ItemName.Trim();
                    //  path = FilePath + "\\" + fileName;
                }
                else
                {
                    path = Docpath + "STORES\\REQUISITIONS\\REQ_" + (ddlIndentSlipNo.SelectedItem.Text).Replace('/', '_').Replace(' ', '_') + "\\ITEM_" + ItemName.Trim();
                }



                FileStream sourceFile = new FileStream((path + "\\" + fileName), FileMode.Open);
                // FileStream sourceFile = new FileStream((path), FileMode.Open);
                long fileSize = sourceFile.Length;
                byte[] getContent = new byte[(int)fileSize];
                sourceFile.Read(getContent, 0, (int)sourceFile.Length);
                sourceFile.Close();
                sourceFile.Dispose();

                Response.ClearContent();
                Response.Clear();
                Response.BinaryWrite(getContent);
                Response.ContentType = GetResponseType(fileName.Substring(fileName.IndexOf('.')));
                Response.AddHeader("Content-Disposition", "attachment; filename=\"" + fileName + "\"");

                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.SuppressContent = true;
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
            else
            {
                Showmessage("File Not Found.");
                return;
            }

        }
        catch (Exception ex)
        {
            //throw;
            Response.Clear();
            Response.ContentType = "text/html";
            Response.Write("Unable to download the attachment.");
        }
    }

    private string GetResponseType(string fileExtension)
    {
        switch (fileExtension.ToLower())
        {
            case ".doc":
                return "application/vnd.ms-word";
                break;

            case ".docx":
                return "application/vnd.ms-word";
                break;

            case ".xls":
                return "application/ms-excel";
                break;

            case ".xlsx":
                return "application/ms-excel";
                break;

            case ".pdf":
                return "application/pdf";
                break;

            case ".ppt":
                return "application/vnd.ms-powerpoint";
                break;

            case ".txt":
                return "text/plain";
                break;

            case "":
                return "";
                break;

            default:
                return "";
                break;
        }
    }

    #endregion
}