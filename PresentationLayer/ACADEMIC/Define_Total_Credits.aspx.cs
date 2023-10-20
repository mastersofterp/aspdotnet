using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Data;
using System.Configuration;
using System.Net;
using System.Web.UI.HtmlControls;
using System.IO;
using Mastersoft.Security;
using Mastersoft.Security.IITMS;



public partial class ACADEMIC_Define_Total_Credits : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    DefineTotalCreditController objDefineTotalCredit = new DefineTotalCreditController();
    DefineCreditLimit dcl = new DefineCreditLimit();
    public string ele_Name = string.Empty;
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
                //this.CheckPageAuthorization();

                //Check for Activity On/Off
                //CheckActivity();
                ViewState["edit"] = "submit";
                ViewState["recordNo"] = null;
                btnSubmit.Text = "Submit";

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                this.PopulateDropDownList();
                //txtElectiveChoiseFor.Text = objCommon.LookUp("ACD_ELECTGROUP", "CHOICEFOR", "ACTIVESTATUS=1 AND GROUPNO=" + Convert.ToInt32(ddlElective.SelectedValue)); // Check Rahul Moraskar
                string host = Dns.GetHostName();
                IPHostEntry ip = Dns.GetHostEntry(host);
                string IPADDRESS = string.Empty;

                IPADDRESS = ip.AddressList[1].ToString();
                //ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
                ViewState["ipAddress"] = IPADDRESS;
                //check activity for course registration.
                string coursestatus = "";
                //= objCommon.LookUp("REFF", "STATUS", "");


                if (coursestatus == "1")
                {

                    Divmaxschem.Visible = true;
                    Div8.Visible = true;
                    Div1.Visible = true;
                    mincredregular.Visible = true;
                }
                else
                {
                    Divmaxschem.Visible = false;
                    Div8.Visible = false;
                    Div1.Visible = false;
                    mincredregular.Visible = false;
                }
                showDetails();
            }
        }
        divMsg.InnerHtml = string.Empty;
        string coursestatusnew = "";
        //objCommon.LookUp("REFF", "STATUS", "");
        foreach (ListViewItem lvi in lvCredit.Items)
        {
            if (lvi.ItemType == ListViewItemType.DataItem)
            {
                HtmlTableCell addtd = (HtmlTableCell)lvi.FindControl("addtd");

                HtmlTableCell minschemtd = (HtmlTableCell)lvi.FindControl("minschemtd");
                HtmlTableCell maxschemtd = (HtmlTableCell)lvi.FindControl("maxschemtd");
                HtmlTableCell minregcredtd = (HtmlTableCell)lvi.FindControl("minregcredtd");
                if (coursestatusnew == "1")
                {
                    //addtd.Visible = true;
                    //maxschemtd.Visible = true;
                    //minregcredtd.Visible = true;
                }
                else
                {
                    //addtd.Visible = false;
                    //maxschemtd.Visible = false;
                    //minregcredtd.Visible = false;
                }
            }
        }




    }

    private void PopulateDropDownList()
    {
        objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME+'('+SHORT_NAME +'-'+ CODE +')' as COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");

        //objCommon.FillDropDownList(ddlSession, "ACD_SCHEME", "SCHEMENO", "SCHEMENAME", "SCHEMENO>0", "SCHEMENO"); // Check Rahul Moraskar COLLEGE_ID in (select UA_COLLEGE_NOS from User_Acc where UA_NO=9473)

        //objCommon.FillListBox(lstbxSession, "ACD_SCHEME A INNER JOIN ACD_COLLEGE_SCHEME_MAPPING M ON A.SCHEMENO=M.SCHEMENO ", "DISTINCT A.SCHEMENO", "A.SCHEMENAME", "A.SCHEMENO>0 AND M.COLLEGE_ID=" + Convert.ToInt16(ddlCollege.SelectedValue), "A.SCHEMENO"); // Added by Rahul Moraskar
        // objCommon.FillDropDownList(lstbxSession, "ACD_COLLEGE_SCHEME_MAPPING", "COSCHNO", "COL_SCHEME_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "COLLEGE_ID");

        //objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO >0", "SEMESTERNO");
        objCommon.FillListBox(lstbxSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO >0", "SEMESTERNO");
        //objCommon.FillDropDownList(ddlElective, "ACD_ELECTGROUP ", "GROUPNO", "GROUPNAME", "GROUPNO>0 AND ACTIVESTATUS=1", "GROUPNO"); // Check Rahul Moraskar

        objCommon.FillListBox(lstbxElective, "ACD_ELECTGROUP ", "GROUPNO", "GROUPNAME", "GROUPNO>0 AND ACTIVESTATUS=1", "GROUPNO");
        DataSet dsElectiveGrp = objCommon.FillDropDown("ACD_ELECTGROUP ", "GROUPNO", "GROUPNAME", "GROUPNO>0 AND ACTIVESTATUS=1", "GROUPNO");
        if (dsElectiveGrp != null && dsElectiveGrp.Tables[0].Rows.Count > 0)
        {
            lvElectiveGrpChoice.DataSource = dsElectiveGrp.Tables[0];
            lvElectiveGrpChoice.DataBind();
        }
    }

    //public void fillChecboxlistOfterm()
    //{

    //    DataSet dsSemester = objCommon.FillDropDown("ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO >0", "SEMESTERNO");
    //    if (dsSemester != null && dsSemester.Tables.Count > 0)
    //    {
    //        if (dsSemester.Tables[0].Rows.Count > 0)
    //        {
    //            chkListTerm.DataTextField = "SEMESTERNAME";
    //            chkListTerm.DataValueField = "SEMESTERNO";
    //            chkListTerm.DataSource = dsSemester.Tables[0];
    //            chkListTerm.DataBind();
    //            chkListTerm.Visible = true;


    //        }
    //        else
    //        {
    //            chkListTerm.DataSource = null;
    //            chkListTerm.DataBind();
    //            chkListTerm.Visible = false;
    //        }
    //    }

    //}

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string result = "Record Saved Successfully !!";

            #region commented code
            /*bool checkLstList = false;
            for (int i = 0; i < lstbxSession.Items.Count; i++)
            {
                if (lstbxSession.Items[i].Selected == true) checkLstList = true;
            }
            if (checkLstList == false)
            {
                objCommon.DisplayMessage("Please Select Scheme", this.Page);
                return;
            }
            checkLstList = false;
            for (int i = 0; i < lstbxSemester.Items.Count; i++)
            {
                if (lstbxSemester.Items[i].Selected == true) checkLstList = true;

            }
            if (checkLstList == false)
            {
                objCommon.DisplayMessage("Please Select Semester", this.Page);
                return;
            }
            checkLstList = false;
            for (int i = 0; i < lstbxElective.Items.Count; i++)
                if (lstbxElective.Items[i].Selected == true) checkLstList = true;

            if (checkLstList == false)
            {
                objCommon.DisplayMessage("Please Select Elective", this.Page);
                return;
            }

            if (chkMaximumSchemeLimit.Checked == false)
            {
                if (txtFromCredit.Text == string.Empty)
                {
                    objCommon.DisplayMessage("Enter Minimum Credit Limit", this.Page);
                    return;
                }

                if (txtToCredits.Text == string.Empty)
                {
                    objCommon.DisplayMessage("Enter Maximum Credit Limit", this.Page);
                    return;
                }
                if (Convert.ToInt32(txtFromCredit.Text) > Convert.ToInt32(txtToCredits.Text))
                {
                    objCommon.DisplayMessage("Minimum Credits Limit should not be Greater than Maximum Credits Limit", this.Page);
                    return;
                }
            }
            int addtionalCourse = 0;
            int minimumSchemeLimit = 0;
            int maximumSchemeLimit = 0;

            if (chkMinimumSchemeLimit.Checked)
            {
                minimumSchemeLimit = 1;
                txtFromCredit.Text = "0";
            }

            if (chkMaximumSchemeLimit.Checked)
            {
                maximumSchemeLimit = 1;
                txtToCredits.Text = "0";
            }

            if (chkAddionalCourse.Checked)
                addtionalCourse = 1;
            */

            #endregion

            DataSet dsSemester = objCommon.FillDropDown("ACD_COLLEGE_DEGREE_BRANCH CDB INNER JOIN ACD_SCHEME S ON S.DEGREENO=CDB.DEGREENO AND S.BRANCHNO=CDB.BRANCHNO", "DURATION", "SCHEMENO", "", "");
            DataTable dtSemester = dsSemester.Tables[0];
            CustomStatus cs = CustomStatus.Error;

            //int oldgroupid = Convert.ToInt32(objCommon.LookUp("ACD_DEFINE_TOTAL_CREDIT", "ISNULL(MAX(GROUPID),0)GROUPID", ""));
            //int newgroupid = oldgroupid + 1;

            string schemenos = string.Empty;
            string terms = string.Empty;
            string electivechoice = string.Empty;

            foreach (ListItem item in lstbxSession.Items)
            {
                if (item.Selected == true)
                {
                    schemenos += item.Value + ',';
                }
            }
            if (schemenos != string.Empty)
            {
                schemenos = schemenos.Substring(0, schemenos.Length - 1);
            }

            foreach (ListItem item in lstbxSemester.Items)
            {
                if (item.Selected == true)
                {
                    terms += item.Value + ',';
                }
            }
            if (terms != string.Empty)
            {
                terms = terms.Substring(0, terms.Length - 1);
            }


            //foreach (ListItem item in lstbxElective.Items)
            //{
            //    if (item.Selected == true)
            //    {
            //        electivechoice += item.Value + ',';
            //    }
            //}
            //if (terms != string.Empty)
            //{
            //    electivechoice = electivechoice.Substring(0, electivechoice.Length - 1);
            //}

            foreach (ListViewDataItem itm in lvElectiveGrpChoice.Items)
            {
                HiddenField hdfElectiveGroupNo = itm.FindControl("hdfElectiveGroupNo") as HiddenField;
                TextBox txtElectiveChoiseFor1 = itm.FindControl("txtElectiveChoiseFor1") as TextBox;

                if (!string.IsNullOrEmpty(txtElectiveChoiseFor1.Text))
                {
                    if (!SecurityThreads.CheckSecurityInput(txtElectiveChoiseFor1.Text))
                        electivechoice += hdfElectiveGroupNo.Value + '-' + txtElectiveChoiseFor1.Text.Trim() + ',';
                    else
                    {
                        objCommon.DisplayMessage(updpnl, "Input String was not in correct Format!", this.Page);
                        return;
                    }
                }
            }

            dcl.TERM = terms;
            dcl.Schemenos = schemenos;
            dcl.Electivegroupnos = electivechoice.TrimEnd(',');

            dcl.DEGREENO = 0; // Convert.ToInt32(ddlDegree.SelectedValue);
            dcl.STUDENT_TYPE = Convert.ToInt32(ddlStudentType.SelectedValue);

            txtFromRange.Text = "";
            txtToRange.Text = "";
            dcl.Core_credit = Convert.ToDouble(txtCoreCredits.Text);
            dcl.Elective_credit = Convert.ToDouble(txtElectiveCredits.Text);
            dcl.Global_credit = Convert.ToDouble(txtGlobalCredits.Text);

            if (txtOverloadCreditLimit.Text == "")
            {
                dcl.Overload_credit = 0;
            }
            else
            {
                dcl.Overload_credit = Convert.ToDouble(txtOverloadCreditLimit.Text); ;
            }

            dcl.TO_CREDIT = Convert.ToDouble(dcl.Core_credit + dcl.Elective_credit + dcl.Global_credit + dcl.Overload_credit);
            dcl.FROM_CREDIT = Convert.ToDouble(txtFromCredit.Text); //txtFromCredit.Text == null ? "0" : txtFromCredit.Text.ToString(); 
            if (dcl.TO_CREDIT == 0)
            {
                objCommon.DisplayMessage(updpnl, "Maximum Credit Limit Must Be greater than Zero (0)!", this.Page);
                return;
            }
            if (dcl.FROM_CREDIT > dcl.TO_CREDIT)
            {
                objCommon.DisplayMessage(updpnl, "Minimum Credit Limit Must Be Same or Less Than Maximum All Credit Limit!", this.Page);
                return;
            }
            dcl.ADM_TYPE = Convert.ToInt32(ddlAdmissionType.SelectedValue);
            dcl.ADDITIONAL_COURSE = 0; // addtionalCourse;
            dcl.DEGREE_TYPE = Convert.ToInt32(ddlAdditionalCourseDegree.SelectedValue);
            dcl.MIN_SCHEMELIMIT = 0;// minimumSchemeLimit;
            dcl.MAX_SCHEMELIMIT = 0; // maximumSchemeLimit;
            dcl.MIN_REG_CREDIT_LIMIT = string.IsNullOrEmpty(txtMinRegCredit.Text) ? 0 : Convert.ToDouble(txtMinRegCredit.Text);
            dcl.ELECTIVE_CHOISEFOR = !string.IsNullOrEmpty(txtElectiveChoiseFor.Text) ? Convert.ToInt32(txtElectiveChoiseFor.Text) : 0;
            int collegeID = Convert.ToInt16(ddlCollege.SelectedValue);

            if (ViewState["edit"] != null && ViewState["edit"].ToString().Equals("edit"))
            {
                cs = (CustomStatus)objDefineTotalCredit.AddCreditModified(dcl, Convert.ToInt32(ViewState["groupid"]),collegeID);
                result = "Record Updated Successfully !!";
            }
            else
            {
                cs = (CustomStatus)objDefineTotalCredit.AddCreditModified(dcl, 0, collegeID);
                result = "Record Saved Successfully !!";
            }

            //for (int i = 0; i < lstbxSession.Items.Count; i++)
            //{
            //    if (lstbxSession.Items[i].Selected == true)
            //    {
            //        for (int j = 0; j < lstbxSemester.Items.Count; j++)
            //        {
            //            if (lstbxSemester.Items[j].Selected == true)
            //            {
            //                string Criteria = "SCHEMENO=" + lstbxSession.Items[i].Value;
            //                if ((GetDataFromDataTable(dtSemester, Criteria, "DURATION") * 2) > j)
            //                {
            //                    for (int k = 0; k < lstbxElective.Items.Count; k++)
            //                    {
            //                        if (lstbxElective.Items[k].Selected == true)
            //                        {
            //                            dcl.SCHEMENO = Convert.ToInt32(lstbxSession.Items[i].Value); // Check Rahul Moraskar
            //                            dcl.DEGREENO = 0; // Convert.ToInt32(ddlDegree.SelectedValue);
            //                            dcl.SCHEMENAME = lstbxSession.Items[i].Text;  // Check Rahul Moraskar
            //                            dcl.TERM = (lstbxSemester.Items[j].Value); // Check Rahul Moraskar
            //                            dcl.TERM_TEXT = lstbxSemester.Items[j].Text; // Check Rahul Moraskar
            //                            dcl.STUDENT_TYPE = Convert.ToInt32(ddlStudentType.SelectedValue);

            //                            txtFromRange.Text = "";
            //                            txtToRange.Text = "";
            //                            dcl.Core_credit = Convert.ToInt32(txtCoreCredits.Text);
            //                            dcl.Elective_credit = Convert.ToInt32(txtElectiveCredits.Text);
            //                            dcl.Global_credit = Convert.ToInt32(txtGlobalCredits.Text);

            //                            dcl.TO_CREDIT = Convert.ToInt32(dcl.Core_credit + dcl.Elective_credit + dcl.Global_credit);
            //                            dcl.FROM_CREDIT = Convert.ToInt32(txtFromCredit.Text); //txtFromCredit.Text == null ? "0" : txtFromCredit.Text.ToString(); 
            //                            if (dcl.FROM_CREDIT > dcl.TO_CREDIT)
            //                            {
            //                                objCommon.DisplayMessage(updpnl, "Minimum Credit Limit Must Be Same or Less Than Maximum All Credit Limit!", this.Page);
            //                                return;
            //                            }
            //                            dcl.ADM_TYPE = Convert.ToInt32(ddlAdmissionType.SelectedValue);
            //                            dcl.ADDITIONAL_COURSE = 0; // addtionalCourse;
            //                            dcl.DEGREE_TYPE = Convert.ToInt32(ddlAdditionalCourseDegree.SelectedValue);
            //                            dcl.MIN_SCHEMELIMIT = 0;// minimumSchemeLimit;
            //                            dcl.MAX_SCHEMELIMIT = 0; // maximumSchemeLimit;
            //                            dcl.MIN_REG_CREDIT_LIMIT = string.IsNullOrEmpty(txtMinRegCredit.Text) ? 0 : Convert.ToInt32(txtMinRegCredit.Text);
            //                            dcl.ELECTIVE_GROUPNO = Convert.ToInt32(lstbxElective.Items[k].Value) > 0 ? Convert.ToInt32(lstbxElective.Items[k].Value) : 0;
            //                            dcl.ELECTIVE_CHOISEFOR = !string.IsNullOrEmpty(txtElectiveChoiseFor.Text) ? Convert.ToInt32(txtElectiveChoiseFor.Text) : 0;

            //                            DataTable dt = (DataTable)ViewState["credits"];


            //                            //string IsAvail = "SCHEMENO=" + lstbxSession.Items[i].Value + " AND TERM=" + lstbxSemester.Items[j].Value + " AND FROM_CREDIT=" + dcl.FROM_CREDIT + " AND TO_CREDIT=" + dcl.TO_CREDIT;

            //                            if (lstbxSemester.Items.Count == 1)
            //                            {
            //                                string IsAvail = "SCHEMENO=" + lstbxSession.Items[i].Value + " AND TERM=" + lstbxSemester.Items[j].Value;
            //                                if (dt != null && dt.Select(IsAvail).Length > 0)
            //                                {
            //                                    string IsRecordAvail = "SCHEMENO=" + lstbxSession.Items[i].Value + " AND TERM=" + lstbxSemester.Items[j].Value + " AND FROM_CREDIT=" + dcl.FROM_CREDIT + " AND TO_CREDIT=" + dcl.TO_CREDIT;
            //                                    if (dt.Select(IsRecordAvail).Length == 0)
            //                                    {
            //                                        objCommon.DisplayMessage(updpnl, "OOPS, Credit Limit must be same for same Scheme and Semester!", this.Page);
            //                                        return;
            //                                    }
            //                                }
            //                            }
            //                            else
            //                            {
            //                                string IsAvail = "SCHEMENO=" + lstbxSession.Items[i].Value + " AND TERM=" + lstbxSemester.Items[j].Value;
            //                                //DataRow[] dr = dt.Select(IsAvail);
            //                                //if (dr != null && dr.Count() > 0)
            //                                //{
            //                                //    int minCredit = Convert.ToInt32(dr[0]["FROM_CREDIT"].ToString());
            //                                //    int maxCredit = Convert.ToInt32(dr[0]["TO_CREDIT"].ToString());

            //                                //    if (minCredit != dcl.FROM_CREDIT || maxCredit != dcl.TO_CREDIT)
            //                                //    {
            //                                //        dcl.FROM_CREDIT = minCredit;
            //                                //        dcl.TO_CREDIT = maxCredit;
            //                                //    }
            //                                //}
            //                            }

            //                            string expression = "SCHEMENO=" + lstbxSession.Items[i].Value + " AND TERM=" + lstbxSemester.Items[j].Value + " AND ELECTIVE_GROUPNO=" + Convert.ToInt32(lstbxElective.Items[k].Value);  // Check Rahul Moraskar

            //                            if (ViewState["edit"] != null && ViewState["edit"].ToString().Equals("edit"))
            //                            {

            //                                if (dt.Select(expression).Length == 0)
            //                                {
            //                                    dcl.RECORDNO = Convert.ToInt32(ViewState["groupid"].ToString());
            //                                    cs = (CustomStatus)objDefineTotalCredit.DeleteCredit(dcl);
            //                                }

            //                                result = "Record Updated Successfully !!";


            //                            }


            //                            if (dt.Select(expression).Length == 0)
            //                            {
            //                                int groupflag = 0;
            //                                cs = (CustomStatus)objDefineTotalCredit.AddCredit(dcl, newgroupid, groupflag);
            //                            }
            //                            else
            //                            {
            //                                int groupflag = 1;
            //                                DataRow[] dr = dt.Select(expression);
            //                                dcl.RECORDNO = Convert.ToInt32(dr[0]["groupid"].ToString());
            //                                cs = (CustomStatus)objDefineTotalCredit.AddCredit(dcl, Convert.ToInt32(ViewState["groupid"]), groupflag);
            //                            }

            //                        }
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}

            objCommon.DisplayMessage(updpnl, result, this.Page);
            showDetails();
            btnCancel_Click(btnCancel, e);

            #region commented code
            ////dcl.SESSION = Convert.ToInt32(ddlSession.SelectedValue); // Check Rahul Moraskar
            //dcl.DEGREENO = Convert.ToInt32(ddlDegree.SelectedValue);
            ////dcl.SESSIONNAME = ddlSession.SelectedItem.Text;  // Check Rahul Moraskar
            ////dcl.TERM = (ddlSemester.SelectedValue); // Check Rahul Moraskar
            ////dcl.TERM_TEXT = ddlSemester.SelectedItem.Text; // Check Rahul Moraskar
            //dcl.STUDENT_TYPE = Convert.ToInt32(ddlStudentType.SelectedValue);

            //txtFromRange.Text = "";
            //txtToRange.Text = "";
            //dcl.TO_CREDIT = Convert.ToInt32(txtToCredits.Text);
            //dcl.FROM_CREDIT = 0; //txtFromCredit.Text == null ? "0" : txtFromCredit.Text.ToString(); 
            //dcl.ADM_TYPE = Convert.ToInt32(ddlAdmissionType.SelectedValue);
            //dcl.ADDITIONAL_COURSE = addtionalCourse;
            //dcl.DEGREE_TYPE = Convert.ToInt32(ddlAdditionalCourseDegree.SelectedValue);
            //dcl.MIN_SCHEMELIMIT = minimumSchemeLimit;
            //dcl.MAX_SCHEMELIMIT = maximumSchemeLimit;

            //if (txtMinRegCredit.Text == string.Empty)
            //{
            //    dcl.MIN_REG_CREDIT_LIMIT = 0;

            //}
            //else
            //{
            //    dcl.MIN_REG_CREDIT_LIMIT = Convert.ToInt32(txtMinRegCredit.Text);
            //}

            ////string whereCond = "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND ELECTIVE_GROUPNO=" + Convert.ToInt32(ddlElective.SelectedValue) + " AND TERM=" + Convert.ToInt32(ddlSemester.SelectedValue);
            ////string IsAvail = objCommon.LookUp("ACD_DEFINE_TOTAL_CREDIT", "TOP 1 ISNULL(ELECTIVE_GROUPNO,0) as ELECT_GROUPNO", whereCond);

            ////if (IsAvail == "0" || IsAvail == "")
            ////{
            ////dcl.ELECTIVE_GROUPNO = ddlElective.SelectedIndex > 0 ? Convert.ToInt32(ddlElective.SelectedValue) : 0; // Check Rahul Moraskar
            //dcl.ELECTIVE_CHOISEFOR = !string.IsNullOrEmpty(txtElectiveChoiseFor.Text) ? Convert.ToInt32(txtElectiveChoiseFor.Text) : 0;
            ////}
            ////else
            ////{
            ////    objCommon.DisplayMessage("Same Entry Already Available.", this.Page);
            ////    ddlElective.Focus();
            ////    return;
            ////}
            //if (ViewState["edit"] != null && ViewState["edit"].ToString().Equals("edit"))
            //{

            //    dcl.RECORDNO = Convert.ToInt32(ViewState["recordNo"].ToString());
            //    CustomStatus cs = (CustomStatus)objDefineTotalCredit.UpdateCredit(dcl);
            //    if (cs.Equals(CustomStatus.RecordSaved))
            //    {
            //        dcl.RECORDNO = Convert.ToInt32(ViewState["recordNo"]);
            //        objCommon.DisplayMessage("Updated Successfully !!", this.Page);
            //        showDetails();
            //        //ddlSession.Enabled = true;  // Check Rahul Moraskar
            //        //ddlSemester.Enabled = true; // Check Rahul Moraskar
            //        btnCancel_Click(btnCancel, e);
            //    }
            //}
            //else
            //{
            //    //Add New
            //    DataTable dt = (DataTable)ViewState["credits"];

            //    //string expression = "SESSIONNO=" + ddlSession.SelectedValue + " AND TERM=" + ddlSemester.SelectedValue + " AND ELECTIVE_GROUPNO=" + Convert.ToInt32(ddlElective.SelectedValue);  // Check Rahul Moraskar
            //    string expression = string.Empty;  // Check Rahul Moraskar
            //    if (dt.Select(expression).Length > 0)
            //    {
            //        objCommon.DisplayMessage("Course credits already defined for the below selection,but you can able modify it. !!", this.Page);
            //        btnCancel_Click(btnCancel, e);
            //        return;
            //    }
            //    CustomStatus cs = (CustomStatus)objDefineTotalCredit.AddCredit(dcl);
            //    if (cs.Equals(CustomStatus.RecordSaved))
            //    {

            //        objCommon.DisplayMessage("Saved Successfully !!", this.Page);
            //        showDetails();
            //        btnCancel_Click(btnCancel, e);

            //    }
            //    else
            //    {
            //        objCommon.DisplayMessage("Course credits already defined for the below selection,but you can able modify it. !!", this.Page);
            //        btnCancel_Click(btnCancel, e);
            //    }

            //}

            #endregion
        }
        catch (Exception ex)
        {
            objCommon.DisplayMessage(this.Page, "OOPS, something went wrong..!", this.Page);
        }
    }

    void showDetails()
    {
        string col3 = "DISTINCT ISNULL(C.GROUPID,0)GROUPID,CDB.COLLEGE_ID,CM.COLLEGE_NAME,ISNULL(FROM_CREDIT,0)FROM_CREDIT,ISNULL(TO_CREDIT,0)TO_CREDIT,ISNULL(CORE_CREDIT,0)CORE_CREDIT,ISNULL(ELECTIVE_CREDIT,0)ELECTIVE_CREDIT,ISNULL(GLOBAL_CREDIT,0)GLOBAL_CREDIT,ISNULL(OVERLOAD_CREDIT,0)OVERLOAD_CREDIT,0 as SCHEMENO,0 as TERM,0 as ELECTIVE_GROUPNO";
        string col4 = @"(CASE  WHEN ISNULL(LOCK,0)=1 THEN 'YES' ELSE 'NO' END)AS LOCK";
        string tblName2 = "ACD_DEFINE_TOTAL_CREDIT C LEFT OUTER JOIN ACD_ELECTGROUP E ON C.ELECTIVE_GROUPNO=E.GROUPNO	INNER JOIN ACD_SCHEME S ON (C.SCHEMENO = S.SCHEMENO) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CDB ON (S.DEGREENO = CDB.DEGREENO AND S.BRANCHNO = CDB.BRANCHNO) INNER JOIN ACD_COLLEGE_MASTER CM ON(CDB.COLLEGE_ID=CM.COLLEGE_ID)";

        DataSet dsCredit2 = objCommon.FillDropDown(tblName2, col3, col4, "", "");
        lvCredit.DataSource = dsCredit2.Tables[0];
        DataTable dt2 = dsCredit2.Tables[0];
        ViewState["credits"] = dt2;
        lvCredit.DataBind();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //ddlSessionType.SelectedIndex = 0;
        //ddlSession.Items.Clear();
        //ddlSession.Items.Add(new ListItem("Please Select", "0"));
        // objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO>0", "SESSIONNO DESC");

        //objCommon.FillDropDownList(ddlSession, "ACD_SCHEME", "SCHEMENO", "SCHEMENAME", "SCHEMENO>0", "SCHEMENO");  // Check Rahul Moraskar

        // objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO>0", "DEGREENO");
        ddlStudentType.SelectedIndex = 0;
        txtToRange.Text = string.Empty;
        txtFromRange.Text = string.Empty;
        chkAddionalCourse.Checked = false;
        chkMinimumSchemeLimit.Checked = false;
        chkMaximumSchemeLimit.Checked = false;
        txtToCredits.Enabled = true;
        txtFromCredit.Enabled = true;
        txtToCredits.Text = string.Empty;
        txtCoreCredits.Text = string.Empty;
        txtElectiveCredits.Text = string.Empty;
        txtGlobalCredits.Text = string.Empty;
        txtFromCredit.Text = string.Empty;
        txtOverloadCreditLimit.Text = string.Empty;

        ViewState["edit"] = "submit";
        btnSubmit.Text = "Submit";
        ViewState["recordNo"] = null;
        //this.lstbxSession.Attributes.Remove("disabled");
        //this.lstbxSemester.Attributes.Remove("disabled");


        //ddlSession.Enabled = true;  // Check Rahul Moraskar
        //ddlSemester.Enabled = true; // Check Rahul Moraskar
        ddlAdmissionType.SelectedIndex = 0;
        ddlAdditionalCourseDegree.SelectedIndex = 0;
        trAdditionalCourses.Visible = false;

        for (int i = 0; i < lstbxSession.Items.Count; i++)
        {
            lstbxSession.Items[i].Selected = false;
            lstbxSession.Items[i].Enabled = true;
        }

        for (int i = 0; i < lstbxSemester.Items.Count; i++)
        {
            lstbxSemester.Items[i].Selected = false;
            lstbxSemester.Items[i].Enabled = true;
        }

        for (int i = 0; i < lstbxElective.Items.Count; i++)
        {
            lstbxElective.Items[i].Selected = false;
        }

        //ddlSemester.SelectedIndex = 0;  // Check Rahul Moraskar
        //ddlElective.SelectedIndex = 0; // Check Rahul Moraskar
        txtElectiveChoiseFor.Text = "";// objCommon.LookUp("ACD_ELECTGROUP", "CHOICEFOR", "ACTIVESTATUS=1 AND GROUPNO=" + Convert.ToInt32(ddlElective.SelectedValue));
        //Response.Redirect(Request.Url.ToString());
        ddlCollege.SelectedIndex = 0;

        foreach (ListViewDataItem itm in lvElectiveGrpChoice.Items)
        {
            TextBox txtElectiveChoiseFor1 = itm.FindControl("txtElectiveChoiseFor1") as TextBox;
            txtElectiveChoiseFor1.Text = string.Empty;
        }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
    }
    protected void chkAddionalCourse_CheckedChanged(object sender, EventArgs e)
    {
        if (chkAddionalCourse.Checked)
        {
            trAdditionalCourses.Visible = true;
        }
        else
        {
            trAdditionalCourses.Visible = false;
            ddlAdditionalCourseDegree.SelectedIndex = 0;
        }
    }
    protected void chkMinimumSchemeLimit_CheckedChanged(object sender, EventArgs e)
    {
        if (chkMinimumSchemeLimit.Checked)
        {
            txtFromCredit.Text = "0";
            txtFromCredit.Enabled = false;
        }
        else
        {
            txtFromCredit.Text = "0";
            txtFromCredit.Enabled = true;
        }
    }
    protected void chkMaximumSchemeLimit_CheckedChanged(object sender, EventArgs e)
    {
        if (chkMaximumSchemeLimit.Checked)
        {
            txtToCredits.Text = "0";
            txtToCredits.Enabled = false;

        }
        else
        {
            txtToCredits.Text = "0";
            txtToCredits.Enabled = true;
        }
    }
    protected void btnLock_Click(object sender, EventArgs e)
    {
        try
        {
            int ret = objDefineTotalCredit.LockCreditDefination();

            if (ret > 0)
            {

                objCommon.DisplayMessage("Locked Successfully !!", this.Page);
                showDetails();
                btnCancel_Click(btnCancel, e);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_DefineTotalCredit.btnSubmit_CLick --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");

        }
    }
    protected void OnLayoutCreated(object sender, EventArgs e)
    {
        string coursestatus = "";
        //objCommon.LookUp("REFF", "STATUS", "");
        if (coursestatus == "1")
        {
            this.lvCredit.FindControl("addth").Visible = true;
            this.lvCredit.FindControl("minschemth").Visible = true;
            this.lvCredit.FindControl("maxschemth").Visible = true;
            this.lvCredit.FindControl("minregcredth").Visible = true;
        }
        else
        {
            this.lvCredit.FindControl("addth").Visible = false;
            this.lvCredit.FindControl("minschemth").Visible = false;
            this.lvCredit.FindControl("maxschemth").Visible = false;
            this.lvCredit.FindControl("minregcredth").Visible = false;

        }
    }


    protected void lstbxSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ViewState["edit"].ToString() == "edit")
        {
            return;
        }
        string LstItems = string.Empty;
        bool Lstcheck = false;
        for (int i = 0; i < lstbxSession.Items.Count; i++)
        {

            if (lstbxSession.Items[i].Selected == true)
            {
                if (LstItems == string.Empty)
                {
                    LstItems = lstbxSession.Items[i].Value.ToString();
                    Lstcheck = true;

                }
                else
                    Lstcheck = false;
            }
        }
        if (Lstcheck == false)
        {
            objCommon.FillListBox(lstbxSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO >0", "SEMESTERNO");
        }
        else
        {
            string duration = objCommon.LookUp("ACD_COLLEGE_DEGREE_BRANCH CDB INNER JOIN ACD_SCHEME S ON S.DEGREENO=CDB.DEGREENO AND S.BRANCHNO=CDB.BRANCHNO", "DURATION", "SCHEMENO =  " + LstItems + " "); // Check Rahul Moraskar    
            objCommon.FillListBox(lstbxSemester, "ACD_SEMESTER ", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO <=" + Convert.ToInt32(duration) * 2 + " AND SEMESTERNO>0", "SEMESTERNO");

        }
    }

    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        //string duration = objCommon.LookUp("ACD_COLLEGE_DEGREE_BRANCH CDB INNER JOIN ACD_SCHEME S ON S.DEGREENO=CDB.DEGREENO AND S.BRANCHNO=CDB.BRANCHNO", "DURATION", "SCHEMENO=" + ddlSession.SelectedValue); // Check Rahul Moraskar
        string duration = objCommon.LookUp("ACD_COLLEGE_DEGREE_BRANCH CDB INNER JOIN ACD_SCHEME S ON S.DEGREENO=CDB.DEGREENO AND S.BRANCHNO=CDB.BRANCHNO", "DURATION", "SCHEMENO=" + lstbxSession.SelectedValue); // Check Rahul Moraskar
        //objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER ", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO <=" + Convert.ToInt32(duration) * 2 + " AND SEMESTERNO>0", "SEMESTERNO");  // Check Rahul Moraskar
    }
    protected void ddlElective_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddlElective.SelectedIndex > 0)
        //    txtElectiveChoiseFor.Text = objCommon.LookUp("ACD_ELECTGROUP", "CHOICEFOR", "ACTIVESTATUS=1 AND GROUPNO=" + Convert.ToInt32(ddlElective.SelectedValue));
        //else
        //    txtElectiveChoiseFor.Text = "";
    }
    protected void lstbxElective_SelectedIndexChanged(object sender, EventArgs e)
    {
        string LstItems = string.Empty;
        for (int i = 0; i < lstbxElective.Items.Count; i++)
        {

            if (lstbxElective.Items[i].Selected == true)
            {
                string choicefor = objCommon.LookUp("ACD_ELECTGROUP", "CHOICEFOR", "ACTIVESTATUS=1 AND GROUPNO=" + Convert.ToInt32(lstbxElective.Items[i].Value.ToString()));
                if (LstItems == string.Empty)
                    LstItems = choicefor;
            }
        }

        if (LstItems == string.Empty)
            txtElectiveChoiseFor.Text = "";
        else
            txtElectiveChoiseFor.Text = LstItems;

        //string LstItems = string.Empty;
        //for (int i = 0; i < lstbxElective.Items.Count; i++)
        //{

        //    if (lstbxElective.Items[i].Selected == true)
        //    {
        //        string choicefor = objCommon.LookUp("ACD_ELECTGROUP", "CHOICEFOR", "ACTIVESTATUS=1 AND GROUPNO=" + Convert.ToInt32(lstbxElective.Items[i].Value.ToString()));
        //        if (LstItems == string.Empty)
        //            LstItems = choicefor;
        //        else
        //        {
        //            if (!LstItems.Contains(choicefor))
        //                LstItems = LstItems + "," + choicefor;
        //        }
        //    }
        //}
        //if (LstItems == string.Empty)
        //    txtElectiveChoiseFor.Text = "";
        //else
        //    txtElectiveChoiseFor.Text = LstItems;

    }

    private int GetDataFromDataTable(DataTable dt, string Criteria, string Column)
    {
        foreach (DataRow item in dt.Select(Criteria))
        {
            return (int)item[Column];
        }
        return 0;
    }

    //added by nehal on 15052023
    protected void lvCredit_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        ListViewDataItem dataitem = (ListViewDataItem)e.Item;
        ImageButton btnEdit = dataitem.FindControl("btnEdit") as ImageButton;
        int gropuid = Convert.ToInt32(btnEdit.CommandArgument);
        ListView lv = dataitem.FindControl("lvDetails") as ListView;
        try
        {
            string col1 = "ISNULL(C.GROUPID,0)GROUPID,CDB.COLLEGE_NAME,C.SCHEMENO,C.SCHEMENAME,FROM_CREDIT,TO_CREDIT,CORE_CREDIT,ELECTIVE_CREDIT,GLOBAL_CREDIT,SESSIONTYPE,STUDENT_TYPE,TERM,FROM_RANGE,TO_RANGE,ADDITIONAL_COURSE";
            string col2 = @"(CASE  WHEN STUDENT_TYPE=1 THEN 'All Pass' ELSE 'Backlog' END)AS STUDENT_TYPE_NAME,
                                    (CASE  WHEN ADDITIONAL_COURSE=1 THEN 'YES' ELSE 'NO' END)AS ADDITIONAL_COURSE_NAME,
                                    (CASE  WHEN MIN_SCHEME_LIMIT=1 THEN 'YES' ELSE 'NO' END)AS MIN_SCHEME_LIMIT,
                                    (CASE  WHEN MAX_SCHEME_LIMIT=1 THEN 'YES' ELSE 'NO' END)AS MAX_SCHEME_LIMIT,
                                    (CASE  WHEN C.ADM_TYPE=1 THEN 'Regular' ELSE 'Direct Second Year' END)AS ADM_TYPE,
                                    (CASE  WHEN DEGREE_TYPE=1 THEN 'UG'  WHEN DEGREE_TYPE=2 then 'UG + PG'  else '' END)AS DEGREE_TYPE,
                                    TERM_TEXT,
                                    (CASE  WHEN isnull(LOCK,0)=1 THEN 'Yes' else 'No' END)AS LOCK,
                                    MIN_REG_CREDIT_LIMIT,E.GROUPNAME,ISNULL(c.ELECTIVE_GROUPNO,0)as ELECTIVE_GROUPNO,
                                    ISNULL(c.ELECTIVE_CHOISEFOR,0)as ELECTIVE_CHOISEFOR";
            string tblName = "ACD_DEFINE_TOTAL_CREDIT C LEFT OUTER JOIN ACD_ELECTGROUP E ON C.ELECTIVE_GROUPNO=E.GROUPNO	INNER JOIN ACD_SCHEME S ON (C.SCHEMENO = S.SCHEMENO) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CDB ON (S.DEGREENO = CDB.DEGREENO AND S.BRANCHNO = CDB.BRANCHNO)";

            DataSet dsCredit = objCommon.FillDropDown(tblName, col1, col2, "GROUPID =" + gropuid, "GROUPID DESC");
            lv.DataSource = dsCredit.Tables[0];
            DataTable dt = dsCredit.Tables[0];
            ViewState["credits"] = dt;
            lv.DataBind();

        }
        catch { }
    }
    protected void btnEdit_Click1(object sender, ImageClickEventArgs e)
    {
        try
        {
            //PopulateDropDownList();
            btnCancel_Click(btnCancel, e);
            ImageButton editButton = sender as ImageButton;
            int groupid = Int32.Parse(editButton.CommandArgument);
            //int groupid = Int32.Parse(editButton.ToolTip);
            ViewState["groupid"] = groupid;
            ViewState["edit"] = "edit";
            btnSubmit.Text = "Update";
            ViewState["recordNo"] = groupid;

            DataSet ds = objDefineTotalCredit.GetCreditDataDetailsEdit(groupid);
            //DataSet ds = objCommon.FillDropDown("ACD_DEFINE_TOTAL_CREDIT", "*", "", "GROUPID='" + groupid + "'", "GROUPID DESC");
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlCollege.SelectedValue = !string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["COLLEGE_ID"])) ? Convert.ToString(ds.Tables[0].Rows[0]["COLLEGE_ID"]) : "0";
                if (ds.Tables[0].Rows[0]["LOCK"].ToString() == "1")
                {
                    objCommon.DisplayMessage("Credit Defination is Locked ,Please Contact Administrator.", this.Page);
                    showDetails();
                    btnCancel_Click(btnCancel, e);
                    return;
                }

                //int rowcount = ds.Tables[0].Rows.Count;

                //for (int i = 0; i < lstbxSession.Items.Count; i++)
                //{
                //    if (i < rowcount)
                //    {
                //        lstbxSession.Items[i].Selected = (lstbxSession.Items[i].Value.ToString() == ds.Tables[0].Rows[i]["SCHEMENO"].ToString()) ? true : false;
                //    }
                //}

                //if (ddlCollege.SelectedIndex > 0)
                    ddlCollege_SelectedIndexChanged(sender, e);

                ViewState["SCHEMENO"] = Convert.ToString(ds.Tables[0].Rows[0]["SCHEMENO"]);
                if (ViewState["SCHEMENO"] != null && ViewState["SCHEMENO"] != "")
                {
                    string sechemeno = ViewState["SCHEMENO"].ToString();
                    string[] subs = sechemeno.Split(',');

                    //foreach (ListItem lstbxSession1 in lstbxSession.Items)
                    //{
                    //    for (int i = 0; i < subs.Count(); i++)
                    //    {
                    //        if (subs[i].Contains(lstbxSession1.Value))
                    //        {
                    //            lstbxSession1.Selected = true;
                    //        }
                    //    }
                    //}

                    foreach (ListItem sessionsitems in lstbxSession.Items)
                    {
                        for (int i = 0; i < subs.Count(); i++)
                        {
                            if (subs[i].ToString().Trim() == sessionsitems.Value)
                            {
                                sessionsitems.Selected = true;
                            }
                        }
                    }
                }
                ViewState["TERM"] = Convert.ToString(ds.Tables[0].Rows[0]["TERM"]);
                if (ViewState["TERM"] != null && ViewState["TERM"] != "")
                {
                    string termno = ViewState["TERM"].ToString();
                    string[] subs2 = termno.Split(',');

                    foreach (ListItem lstbxSemester1 in lstbxSemester.Items)
                    {
                        for (int i = 0; i < subs2.Count(); i++)
                        {
                            if (subs2[i].ToString().Trim() == lstbxSemester1.Value)
                            {
                                lstbxSemester1.Selected = true;
                            }
                        }
                    }
                }

                // below code added by Shailendra K. on dated 30.09.2023 as per T-48611

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    int groupNo = Convert.ToInt16(ds.Tables[0].Rows[i]["ELECTIVE_GROUPNO"]);
                    string choiceFor = Convert.ToString(ds.Tables[0].Rows[i]["ELECTIVE_CHOISEFOR"]);
                    foreach (ListViewDataItem itm in lvElectiveGrpChoice.Items)
                    {
                        HiddenField hdfElectiveGroupNo = itm.FindControl("hdfElectiveGroupNo") as HiddenField;
                        TextBox txtElectiveChoiseFor1 = itm.FindControl("txtElectiveChoiseFor1") as TextBox;

                        if (Convert.ToInt16(hdfElectiveGroupNo.Value) == groupNo)
                            txtElectiveChoiseFor1.Text = choiceFor;
                    }
                }

              

                // Above code added by Shailendra K. on dated 30.09.2023 as per T-48611

                // below code commented by Shailendra K. on dated 30.09.2023 as per T-48611

                //ViewState["ELECTIVE_GROUPNO"] = Convert.ToString(ds.Tables[0].Rows[0]["ELECTIVE_GROUPNO"]);
                //if (ViewState["ELECTIVE_GROUPNO"] != null && ViewState["ELECTIVE_GROUPNO"] != "")
                //{
                //    string electiveno = ViewState["ELECTIVE_GROUPNO"].ToString();
                //    string[] subs3 = electiveno.Split(',');

                //    foreach (ListItem lstbxElective1 in lstbxElective.Items)
                //    {
                //        for (int i = 0; i < subs3.Count(); i++)
                //        {
                //            if (subs3[i].ToString().Trim() == lstbxElective1.Value)
                //            {
                //                lstbxElective1.Selected = true;
                //            }
                //        }
                //    }
                //}



                //for (int i = 0; i < lstbxSemester.Items.Count; i++)
                //{
                //    if (i < rowcount)
                //    {
                //        lstbxSemester.Items[i].Selected = (lstbxSemester.Items[i].Value.ToString() == ds.Tables[0].Rows[i]["TERM"].ToString()) ? true : false;
                //    }
                //}
                //for (int i = 0; i < lstbxElective.Items.Count; i++)
                //{
                //    if (i < rowcount)
                //    {
                //        lstbxElective.Items[i].Selected = (lstbxElective.Items[i].Value.ToString() == ds.Tables[0].Rows[0]["ELECTIVE_GROUPNO"].ToString()) ? true : false;
                //    }
                //}
                //  txtElectiveChoiseFor.Text = !string.IsNullOrEmpty(ds.Tables[0].Rows[0]["ELECTIVE_CHOISEFOR"].ToString()) ? ds.Tables[0].Rows[0]["ELECTIVE_CHOISEFOR"].ToString() : "1";
                ddlDegree.SelectedValue = ds.Tables[0].Rows[0]["DEGREENO"].ToString();
                txtFromCredit.Text = ds.Tables[0].Rows[0]["FROM_CREDIT"].ToString();
                txtToCredits.Text = ds.Tables[0].Rows[0]["TO_CREDIT"].ToString();
                ddlStudentType.SelectedValue = ds.Tables[0].Rows[0]["STUDENT_TYPE"].ToString();
                txtFromRange.Text = ds.Tables[0].Rows[0]["FROM_RANGE"].ToString();
                txtToRange.Text = ds.Tables[0].Rows[0]["TO_RANGE"].ToString();
                ddlAdmissionType.SelectedValue = ds.Tables[0].Rows[0]["ADM_TYPE"].ToString();
                ddlAdditionalCourseDegree.SelectedValue = ds.Tables[0].Rows[0]["DEGREE_TYPE"].ToString();
                txtMinRegCredit.Text = ds.Tables[0].Rows[0]["MIN_REG_CREDIT_LIMIT"].ToString();
                txtCoreCredits.Text = ds.Tables[0].Rows[0]["CORE_CREDIT"].ToString();
                txtElectiveCredits.Text = ds.Tables[0].Rows[0]["ELECTIVE_CREDIT"].ToString();
                txtGlobalCredits.Text = ds.Tables[0].Rows[0]["GLOBAL_CREDIT"].ToString();
                txtOverloadCreditLimit.Text = ds.Tables[0].Rows[0]["OVERLOAD_CREDIT"].ToString();
                lstbxSession.Focus();

                #region commented code
                //ddlSession.SelectedValue = ds.Tables[0].Rows[0]["SESSIONNO"].ToString(); // Check Rahul Moraskar
                //ddlSemester.SelectedValue = ds.Tables[0].Rows[0]["TERM"].ToString(); // Check Rahul Moraskar
                //ddlSemester.SelectedValue = ds.Tables[0].Rows[0]["TERM"].ToString(); // Check Rahul Moraskar
                //ddlTerm.SelectedValue = ds.Tables[0].Rows[0]["TERM"].ToString();
                ////if (ds.Tables[0].Rows[0]["ADDITIONAL_COURSE"].ToString() == "1")
                ////{
                ////    chkAddionalCourse.Checked = true;
                ////    trAdditionalCourses.Visible = true;
                ////}
                ////else
                ////{
                ////    chkAddionalCourse.Checked = false;
                ////    trAdditionalCourses.Visible = false;
                ////    ddlAdditionalCourseDegree.SelectedIndex = 0;
                ////}

                ////if (ds.Tables[0].Rows[0]["MIN_SCHEME_LIMIT"].ToString() == "1")
                ////{
                ////    chkMinimumSchemeLimit.Checked = true;
                ////    txtFromCredit.Enabled = false;
                ////}
                ////else
                ////{
                ////    chkMinimumSchemeLimit.Checked = false;
                ////    txtFromCredit.Enabled = true;
                ////}


                ////if (ds.Tables[0].Rows[0]["MAX_SCHEME_LIMIT"].ToString() == "1")
                ////{
                ////    chkMaximumSchemeLimit.Checked = true;
                ////    txtToCredits.Enabled = false;
                ////}
                ////else
                ////{
                ////    chkMaximumSchemeLimit.Checked = false;
                ////    txtToCredits.Enabled = true;
                ////}

                //this.lstbxSession.Attributes.Add("disabled", "");
                //this.lstbxSemester.Attributes.Add("disabled", "");



                //ddlSemester.Enabled = false; // Check Rahul Moraskar
                //ddlSession.Enabled = false; // Check Rahul Moraskar
                //ddlElective.SelectedValue = !string.IsNullOrEmpty(ds.Tables[0].Rows[0]["ELECTIVE_GROUPNO"].ToString()) ? ds.Tables[0].Rows[0]["ELECTIVE_GROUPNO"].ToString() : "1"; // Check Rahul Moraskar

                // string[] strings = ds.Tables[0].Rows[0]["TERM"].ToString().Split(',');

                //for (int i = 0; i < strings.Count(); i++)
                //{
                //    foreach (ListItem item in chkListTerm.Items)
                //    {
                //        item.Selected = false;
                //    }
                //}

                //for (int i = 0; i < strings.Count(); i++)
                //{
                //    foreach (ListItem item in chkListTerm.Items)
                //    {
                //        if (item.Value == strings[i])
                //        {
                //            item.Selected = true;
                //        }

                //    }

                #endregion
            }
        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "DefineCounter.btnEdit_Click1() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable");
        }
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        GridView GVCreditDef = new GridView();
        DataSet ds = objDefineTotalCredit.GetCreditDefinitionDetails();

        if (ds != null && ds.Tables.Count > 0)
        {
            GVCreditDef.DataSource = ds;
            GVCreditDef.DataBind();

            string attachment = "attachment; filename=" + "Credit Definition Details Report.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.MS-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            GVCreditDef.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
        else
        {
            objCommon.DisplayMessage(this.Page, "Record Not Found!!!", this.Page);
        }
    }
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddlCollege.SelectedIndex > 0)
        //{
        objCommon.FillListBox(lstbxSession, "ACD_SCHEME A INNER JOIN ACD_COLLEGE_SCHEME_MAPPING M ON A.SCHEMENO=M.SCHEMENO ", "DISTINCT A.SCHEMENO", "A.SCHEMENAME", "A.SCHEMENO>0 AND M.COLLEGE_ID=" + Convert.ToInt16(ddlCollege.SelectedValue), "A.SCHEMENO"); // Added by Rahul Moraskar
        //}
        //else
        //{
        //    objCommon.DisplayMessage(this.Page, "Please Select College!!!", this.Page);
        //    return;
        //}
    }
}