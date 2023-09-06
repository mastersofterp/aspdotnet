using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using IITMS.SQLServer.SQLDAL;
using System.Net;
using System.Linq;
using System.IO;
using System.Data.SqlClient;

public partial class ACADEMIC_DemandCancellation : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;


    #region Page Events
    protected void Page_PreInit(object sender, EventArgs e)
    {
        // Set MasterPage
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
                // Check User Session
                if (Session["userno"] == null || Session["username"] == null ||
                    Session["usertype"] == null || Session["userfullname"] == null)
                {
                    Response.Redirect("~/default.aspx");
                }
                else
                {
                    // Check User Authority 
                    this.CheckPageAuthorization();

                    // Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    // Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                    this.objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "CODE", "CODE <> ''", "CODE");
                    this.objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "SHORTNAME", "SHORTNAME <> ''", "SHORTNAME");
                    this.objCommon.FillDropDownList(ddlYear, "ACD_YEAR", "YEAR", "YEARNAME", "", "");
                    this.objCommon.FillDropDownList(ddlSem, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "", "");

                    if (Request.QueryString["id"] != null && Request.QueryString["id"].ToString() != string.Empty)
                    {
                        try
                        {
                            int studId = int.Parse(Request.QueryString["id"].ToString());

                            /// passing demand no as zero to retrieve all demand record  first time.
                            this.DisplayAllDemands(studId, 0);
                        }
                        catch (Exception ex)
                        {
                            throw ex = new Exception("Invalid Student Id has been passed.");
                        }
                    }
                    this.objCommon.FillDropDownList(ddlSearch, "ACD_SEARCH_CRITERIA", "ID", "CRITERIANAME", "ID > 0 ", "srno");
                    ddlSearch.SelectedIndex = 1;
                    //ddlSearch_SelectedIndexChanged(sender, e);
                    
                }
            }
            else
            {
                // Clear message div
                divMsg.InnerHtml = string.Empty;
                if (Request.Params["__EVENTTARGET"] != null &&
                    Request.Params["__EVENTTARGET"].ToString() != string.Empty &&
                    Request.Params["__EVENTTARGET"].ToString() == "btnSearch")
                {
                    this.ShowSearchResults(Request.Params["__EVENTARGUMENT"].ToString());
                }
                if (Page.Request.Params["__EVENTTARGET"].ToString().ToLower().Contains("btnclear"))
                {
                    txtSearch.Text = string.Empty;
                    lvStudent.DataSource = null;
                    lvStudent.DataBind();
                    ddlDegree.ClearSelection();
                    ddlBranch.ClearSelection();
                    ddlYear.ClearSelection();
                    ddlSem.ClearSelection();
                }
                //if (Request.QueryString["id"] != null && Request.QueryString["id"].ToString() != string.Empty)
                //{
                //    try
                //    {
                //        int studId = int.Parse(Request.QueryString["id"].ToString());
                //        /// passing demand no as zero to retrieve all demand record  first time.
                //        this.DisplayAllDemands(studId, 0);
                //    }
                //    catch (Exception ex)
                //    {
                //        throw ex = new Exception("Invalid Student Id has been passed.");
                //    }
                //}

                
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_DemandCancellation.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            // Check user's authrity for Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=DemandCancellation.aspx");
            }
        }
        else
        {
            // Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=DemandCancellation.aspx");
        }
    }

    #endregion

    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtEnrollNo.Text.Trim() != string.Empty)
            {
                FeeCollectionController feeController = new FeeCollectionController();
                int studentId = feeController.GetStudentIdByEnrollmentNo(txtEnrollNo.Text.Trim());

                if (studentId > 0)
                {
                    this.DisplayAllDemands(studentId, 0);
                }
                else
                {
                    ShowMessage("No student found with given enrollment number.");
                    divAllDemands.Visible = false;
                }
            }
            else
                ShowMessage("Please enter enrollment number.");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_DemandCancellation.btnShow_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void lnkId_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnk = sender as LinkButton;
            string url = string.Empty;
            if (Request.Url.ToString().IndexOf("&id=") > 0)
                url = Request.Url.ToString().Remove(Request.Url.ToString().IndexOf("&id="));
            else
                url = Request.Url.ToString();

            Response.Redirect(url + "&id=" + lnk.CommandArgument, false);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_DemandCancellation.ShowSearchResults() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }


    private void ShowSearchResults(string searchParams)
    {
        try
        {
            StudentSearch objSearch = new StudentSearch();

            string[] paramCollection = searchParams.Split(',');
            if (paramCollection.Length > 2)
            {
                for (int i = 0; i < paramCollection.Length; i++)
                {
                    string paramName = paramCollection[i].Substring(0, paramCollection[i].IndexOf('='));
                    string paramValue = paramCollection[i].Substring(paramCollection[i].IndexOf('=') + 1);

                    switch (paramName)
                    {
                        case "Name":
                            objSearch.StudentName = paramValue;
                            break;
                        case "EnrollNo":
                            objSearch.EnrollmentNo = paramValue;
                            break;
                        case "DegreeNo":
                            objSearch.DegreeNo = int.Parse(paramValue);
                            break;
                        case "BranchNo":
                            objSearch.BranchNo = int.Parse(paramValue);
                            break;
                        case "YearNo":
                            objSearch.YearNo = int.Parse(paramValue);
                            break;
                        case "SemNo":
                            objSearch.SemesterNo = int.Parse(paramValue);
                            break;
                        default:
                            break;
                    }
                }
            }
            FeeCollectionController feeController = new FeeCollectionController();
            DataSet ds = feeController.GetStudents(objSearch);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvStudent.DataSource = ds;
                lvStudent.DataBind();
            }
            else
            {
                objCommon.DisplayMessage(updEdit, "Student not found!", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_DemandCancellation.ShowSearchResults() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void DisplayAllDemands(int studentId, int demandNo)
    {
        try
        {
            DemandModificationController dmController = new DemandModificationController();
            DataSet ds = dmController.GetDemandsforCancellation(studentId, demandNo);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                lvAllDemands.DataSource = ds;
                lvAllDemands.DataBind();
                divAllDemands.Visible = true;
                //lvAllDemands.Visible = true;
                //pnlALLDemands.Visible = true;
                ViewState["demand_EXISTS"] = 1;
            }

            else
            {
                ViewState["demand_EXISTS"] = 0;
                ShowMessage("No fee demand found for this student.");
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_DemandCancellation.DisplayAllDemands() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void ShowMessage(string message)
    {
        if (message != string.Empty)
        {
            divMsg.InnerHtml = "<script type='text/javascript' language='javascript'> alert('" + message + "'); </script>";
        }
    }

    protected void btnDeleteDemand_Click(object sender, EventArgs e )
    {
        try
        {
        int Count = 0; 
        foreach (RepeaterItem dataitem in lvAllDemands.Items)
                {
                //TextBox txtreamrk = dataitem.FindControl("txtremark") as TextBox;
                FeeDemand deleteDemand = new FeeDemand();
                TextBox txtremark = dataitem.FindControl("txtremark") as TextBox;
                
                if (txtremark.Text != "")
                  {
                   Count++;
                  
                LinkButton btnDeleteRecord = sender as LinkButton;
                deleteDemand.DemandId = (btnDeleteRecord.CommandArgument != string.Empty ? int.Parse(btnDeleteRecord.CommandArgument) : 0);
                deleteDemand.StudentId = (btnDeleteRecord.CommandName != string.Empty ? int.Parse(btnDeleteRecord.CommandName) : 0);
                deleteDemand.UANO = int.Parse(Session["userno"].ToString());
                deleteDemand.IpAddress = Request.ServerVariables["REMOTE_ADDR"].ToString();

                string Remark= "This Demand has been cancelled by " + Session["userfullname"].ToString() + " on " + DateTime.Now + ". ";
                       Remark += txtremark.Text.Trim();
                DemandModificationController dmController = new DemandModificationController();

                if (DeleteDemand(deleteDemand, Remark))
                    {
                    //this.ShowMessage("Demand Cancelled successfully.");
                    objCommon.DisplayMessage(this.Page, "Demand Cancelled successfully.", this.Page);
                    DisplayAllDemands(deleteDemand.StudentId, 0);
                    }
                else
                    {
                    //this.ShowMessage("Unable to cancel demand.");
                    objCommon.DisplayMessage(this.Page, "Unable to cancel demand.", this.Page);
                    }
                 }

                }
        if (Count==0)
            {
            objCommon.DisplayMessage(this.Page, "Please Enter Remark.", this.Page);
            return;
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_DemandCancellation.btnDeleteDemand_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }


    protected void lvAllDemands_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {

        foreach (RepeaterItem dataitem in lvAllDemands.Items)
        {
            Label lblTxt = dataitem.FindControl("lblTxt") as Label;
            Label lblSem = dataitem.FindControl("lblSem") as Label;
            Label lblRecieptCode = dataitem.FindControl("lblRecieptCode") as Label;
            Label lblPayTypeNo = dataitem.FindControl("lblPayTypeNo") as Label;
            LinkButton btnDel = dataitem.FindControl("btnDelete") as LinkButton;
            int idno = Convert.ToInt32(btnDel.CommandName);
            int sem = Convert.ToInt32(lblSem.ToolTip);
            int PayTypeNo = Convert.ToInt32(lblPayTypeNo.ToolTip);
            string RecieptCode = Convert.ToString(lblRecieptCode.ToolTip);
            string count = objCommon.LookUp("ACD_DCR", "COUNT(1)", "IDNO=" + idno + "AND SEMESTERNO=" + sem + " AND CAN=0 AND RECON=1 AND RECIEPT_CODE='" + RecieptCode + "'");
            int delCount = Convert.ToInt32(objCommon.LookUp("ACD_DEMAND", "COUNT(1)", "IDNO=" + idno + "AND SEMESTERNO=" + sem + " AND CAN=1 AND DELET=1 AND RECIEPT_CODE='" + RecieptCode + "' AND PAYTYPENO = " + PayTypeNo));

            //if (delCount>0)
            //    {
            //    lblTxt.Visible = true;
            //    lblTxt.Text = "Cancel Demand";
            //    lblTxt.ForeColor = Color.Red;
            //    btnDel.Visible = true;
            //    }
            //else{
            //    if(count!="0")
            //        {
            //        lblTxt.Text = "Already Fees Paid";
            //        lblTxt.Visible = true;
            //        lblTxt.ForeColor = Color.Green;
            //        }
                
            //    }
            if (count != "0")
                {
                btnDel.Text = "Already Fees Paid";
                btnDel.Visible = true;
                btnDel.ForeColor = Color.Green;
                }
            else
                {
                if (delCount > 0)
                    {
                    btnDel.Visible = true;
                    btnDel.Text = "Cancel Demand";
                    btnDel.ForeColor = Color.Red;
                    btnDel.Visible = true;
                    }
                else
                    {
                    btnDel.Visible = true;
                    btnDel.Visible = true;
                    btnDel.Text = "Cancel Demand";
                    btnDel.ForeColor = Color.Red;
                    btnDel.Visible = true;
                    }
                }
        }

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        // Reload/refresh complete page. 
        //if (Request.Url.ToString().IndexOf("&id=") > 0)
        //{
        //    Response.Redirect(Request.Url.ToString().Substring(0, Request.Url.ToString().IndexOf("&id=")));
        //}
        //else
        //{
            //Response.Redirect(Request.Url.ToString());
        //}
    divAllDemands.Visible = false;
    lblNoRecords.Visible = false;
    lvStudent.DataSource = null;
    lvStudent.DataBind();
    }

    protected void ddlSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
                divAllDemands.Visible = false;

            //pnlALLDemands.Visible = false;
            lblNoRecords.Visible = false;
            lvStudent.DataSource = null;
            lvStudent.DataBind();
            if (ddlSearch.SelectedIndex > 0)
            {
                DataSet ds = objCommon.GetSearchDropdownDetails(ddlSearch.SelectedItem.Text);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string ddltype = ds.Tables[0].Rows[0]["CRITERIATYPE"].ToString();
                    string tablename = ds.Tables[0].Rows[0]["TABLENAME"].ToString();
                    string column1 = ds.Tables[0].Rows[0]["COLUMN1"].ToString();
                    string column2 = ds.Tables[0].Rows[0]["COLUMN2"].ToString();
                    if (ddltype == "ddl")
                    {
                        pnltextbox.Visible = false;
                        txtSearch.Visible = false;
                        pnlDropdown.Visible = true;
                        rfvDDL.Enabled = true;
                        divtxt.Visible = false;
                        lblDropdown.Text = ddlSearch.SelectedItem.Text;
                        objCommon.FillDropDownList(ddlDropdown, tablename, column1, column2, column1 + ">0", column1);

                    }
                    else
                        {
                        rfvSearchtring.Enabled = true;
                        pnltextbox.Visible = true;
                        divtxt.Visible = true;
                        txtSearch.Visible = true;
                        pnlDropdown.Visible = false;

                    }
                }
            }
            else
            {

                pnltextbox.Visible = false;
                pnlDropdown.Visible = false;

            }
        }
        catch
        {
            throw;
        }
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        // Reload/refresh complete page. 
        if (Request.Url.ToString().IndexOf("&id=") > 0)
        {
            Response.Redirect(Request.Url.ToString().Substring(0, Request.Url.ToString().IndexOf("&id=")));
        }
        else
        {
            Response.Redirect(Request.Url.ToString());
        }
    }


    private void bindlist(string category, string searchtext)
    {

        StudentController objSC = new StudentController();
        DataSet ds = objSC.RetrieveStudentDetailsNew(searchtext, category);

        if (ds.Tables[0].Rows.Count > 0)
        {
            pnlLV.Visible = true;
            lvStudent.Visible = true;
            lvStudent.DataSource = ds;
            lvStudent.DataBind();
            objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvStudent);//Set label - 
            lblNoRecords.Text = "Total Records : " + ds.Tables[0].Rows.Count.ToString();
        }
        else
        {
            lblNoRecords.Text = "Total Records : 0";
            lvStudent.Visible = false;
            lvStudent.DataSource = null;
            lvStudent.DataBind();
        }
    }
    protected void btnSearchStu_Click(object sender, EventArgs e)
    {
        
        divAllDemands.Visible = false;
        //pnlALLDemands.Visible = false;
        //lvAllDemands.Visible = false;
        lblNoRecords.Visible = true;
        string value = string.Empty;
        if (ddlDropdown.SelectedIndex > 0)
        {
            value = ddlDropdown.SelectedValue;
        }
        else
        {
            value = txtSearchStr.Text;
        }

        //ddlSearch.ClearSelection();
        bindlist(ddlSearch.SelectedItem.Text, value);
        ddlDropdown.ClearSelection();
        txtSearchStr.Text = string.Empty;
    }



    public bool DeleteDemand(FeeDemand deleteDemand, string Remark)
        {
        try
            {
            SQLHelper objDataAccess = new SQLHelper(_connectionString);
            SqlParameter[] sqlParams = new SqlParameter[] 
                {                    
                    new SqlParameter("@P_IDNO", deleteDemand.StudentId),
                    new SqlParameter("@P_IPADDRESS", deleteDemand.IpAddress ),
                    new SqlParameter("@P_UA_NO", deleteDemand.UANO),
                    new SqlParameter("@P_REMARK", Remark),
                    new SqlParameter("@P_DM_NO", deleteDemand.DemandId)
                };
            sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;
            deleteDemand.DemandId = (Int32)objDataAccess.ExecuteNonQuerySP("PKG_FEECOLLECT_DELETE_DEMAND", sqlParams, true);

            if (deleteDemand.DemandId == -99)
                return false;
            }
        catch (Exception ex)
            {
            throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.DemandModificationController.UpdateDemand() --> " + ex.Message + " " + ex.StackTrace);
            }
        return true;
        }
    
}