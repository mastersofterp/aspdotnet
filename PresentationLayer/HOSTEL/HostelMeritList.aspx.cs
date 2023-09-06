using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.ComponentModel.Design;

public partial class Hostel_Merit_List : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    HostelController objhostel = new HostelController(); 
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["mast erpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        // CheckRef();

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
                    //lblHelp.Value = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                pnlSelect.Visible = true;
                PopulateDropDownList();
                ViewState["PreviousRowIndex"] = null;
                ViewState["UPDATE"] = null;
            }
        }
        else
        {
            divMsg.InnerHtml = string.Empty;
        }

    }


    
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=HostelMeritList.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=HostelMeritList.aspx");
        }
    }
    protected void PopulateDropDownList()
    {
        try
        {
           // objCommon.FillDropDownList(ddlSession, "ACD_HOSTEL_SESSION", "HOSTEL_SESSION_NO", "SESSION_NAME", "HOSTEL_SESSION_NO >0", "HOSTEL_SESSION_NO desc");
            objCommon.FillDropDownList(ddlSession, "ACD_HOSTEL_SESSION", "HOSTEL_SESSION_NO", "SESSION_NAME", "HOSTEL_SESSION_NO > 0 AND FLOCK=1", "HOSTEL_SESSION_NO DESC");
            ddlSession.SelectedIndex = 1;
            ddlSession.Enabled = false ;

            objCommon.FillDropDownList(ddlExamSession, "ACD_SESSION_MASTER", "SESSIONNO", "(SESSION_PNAME +' '+ '['+ ltrim(str(SESSIONNO)) +']') As SESSION_PNAME", "SESSIONNO > 0 ", "SESSIONNO desc");
            //CHANGES HBNO TO HOSTEL_NO FOR BIND ADDED BY SHUBHAM B ON 15/04/22
            objCommon.FillDropDownList(ddlHostelNo, "ACD_HOSTEL", "HOSTEL_NO", "HOSTEL_NAME", "HOSTEL_NO > 0", "HOSTEL_NAME");
            objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0", "SEMESTERNO");
            GetHostel();
            GetDegree();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "HOSTEL_HostelMeritList.PopulateDropDownList()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        string strDegreeNo = string.Empty;
        string strHostel = string.Empty;
        int count = 0;
        int chk=0;
        string preparedate=string.Empty;
        try
        {
            foreach (ListItem items in cblstDegree.Items)
            {
                if (items.Selected == true)
                {
                    strDegreeNo += items.Value + ",";
                    count++;
                }
            }
            foreach (ListItem items in cblstHostel.Items)
            {
                if (items.Selected == true)
                {
                    strHostel += items.Value + ",";
                    count++;
                }
            }

            strDegreeNo = strDegreeNo.TrimEnd(',');
            strHostel = strHostel.Trim(',');
            if ( strHostel != string.Empty   )
            {
                if (strDegreeNo != string.Empty)
                {
                    preparedate = objCommon.LookUp("ACD_HOSTEL_CUTTOFF_RANGE A ", "PREPARE_DATE", "HOSTEL_SESSION_NO =" + ddlSession.SelectedValue + " AND EXAM_SESSION_NO =" + ddlExamSession.SelectedValue + " AND DEGREENO IN (" + strDegreeNo + ") AND HOSTELLIST='" + strHostel + "'  AND SEMESTERNO =" + ddlSemester.SelectedValue);

                    if (preparedate == string.Empty || chkoverwrite.Checked)
                    {
                        if (chkoverwrite.Checked)
                        {
                            chk = 1;
                        }
                        CustomStatus cs = (CustomStatus)objhostel.PrepareData(Convert.ToInt16(ddlSession.SelectedValue), Convert.ToInt16(ddlExamSession.SelectedValue), Convert.ToInt16(ddlHostelNo.SelectedValue), Convert.ToInt16(ddlSemester.SelectedValue), strDegreeNo, chk, strHostel);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            objCommon.DisplayMessage(this.updpnlExam, "Data Prepare Successfully!!!", this.Page);
                            chk = 0;
                            Clear();
                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.updpnlExam, "Data Already Prepare !!!", this.Page);
                    }
                }
                else
                {
                    objCommon.DisplayMessage(this.updpnlExam, "Please Select Degree", this.Page);
                }
            }
            else
            {
                objCommon.DisplayMessage(this.updpnlExam, "Please Select Hostel", this.Page);
                
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "HOSTEL_HostelMeritList. btnShow_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
     }
    private void GetDegree()
    {
        DataSet ds = objCommon.FillDropDown("ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO > 0", "DEGREENO");
        if (ds != null && ds.Tables.Count > 0)
        {
            cblstDegree.DataTextField = "DEGREENAME";
            cblstDegree.DataValueField = "DEGREENO";
            cblstDegree.DataSource = ds.Tables[0];
            cblstDegree.DataBind();
        }
    }
    private void GetHostel()
    {
       // DataSet ds = objCommon.FillDropDown("ACD_HOSTEL", "HBNO", "HOSTEL_NAME", "HBNO > 0", "HBNO");
        DataSet ds = objCommon.FillDropDown("ACD_HOSTEL", "HOSTEL_NO", "HOSTEL_NAME", "HOSTEL_NO > 0", "HOSTEL_NO");
        if (ds != null && ds.Tables.Count > 0)
        {
            cblstHostel.DataTextField = "HOSTEL_NAME";
            cblstHostel.DataValueField = "HOSTEL_NO";
            cblstHostel.DataSource = ds.Tables[0];
            cblstHostel.DataBind();
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
       // Response.Redirect(Request.Url.ToString());
        Clear();
    }
    private void Clear()
    {
        ddlSemester.SelectedIndex = 0;
        ddlHostelNo.SelectedIndex = 0;
        //ddlSession.SelectedIndex = 0;
        ddlExamSession.SelectedIndex = 0;
        chkoverwrite.Checked = false;
        cblstDegree.ClearSelection();
        cblstHostel.ClearSelection();
    }
    
    protected void btnCuttofRange_Click(object sender, EventArgs e)
    {
        showReport("Cuttoff Range Report", "rptCuttoffReport.rpt");
    }
   private void showReport(string reportTitle,string rptFileName)
    {
        string strDegreeNo = string.Empty;
        string strHostel = string.Empty;
        int count = 0;

        foreach (ListItem items in cblstDegree.Items)
        {
            if (items.Selected == true)
            {
                strDegreeNo += items.Value + "$";
                count++;
            }
        }
        strDegreeNo = strDegreeNo.TrimEnd('$');

        foreach (ListItem items in cblstHostel.Items)
        {
            if (items.Selected == true)
            {
                strHostel += items.Value + "$";
                count++;
            }
        }
        strHostel = strHostel.Trim('$');
        try
        {
            if (strDegreeNo != string.Empty)
            {
                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("hostel")));
                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=" + reportTitle;
                url += "&path=~,Reports,Hostel," + rptFileName;
                url += "&param=@P_HOSTELSESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_DEGREENO=" + strDegreeNo + ",@P_EXAMSESSIONNO=" + Convert.ToInt32(ddlExamSession.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_HOSTELNO=" + strHostel + ",@P_ORGANIZATION_ID=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]);
                //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
                //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
                //divMsg.InnerHtml += " </script>";

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
                sb.Append(@"window.open('" + url + "','','" + features + "');");

                ScriptManager.RegisterClientScriptBlock(this.updpnlExam, this.updpnlExam.GetType(), "controlJSScript", sb.ToString(), true);

            }
            else
            {
                objCommon.DisplayMessage(this.updpnlExam, "Please Select Degree", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Hostel_Report_HostelVacantRooms.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
        
    }
    protected void btnRankList_Click(object sender, EventArgs e)
    {
        string strDegreeNo = string.Empty;
        string strHostel = string.Empty;
        int count = 0;
        int chk = 0;
        string preparedate = string.Empty;
        ViewState["UPDATE"] = null ;
        try
        {
            foreach (ListItem items in cblstDegree.Items)
            {
                if (items.Selected == true)
                {
                    strDegreeNo += items.Value + ",";
                    count++;
                }
            }
            foreach (ListItem items in cblstHostel.Items)
            {
                if (items.Selected == true)
                {
                    strHostel += items.Value + ",";
                    count++;
                }
            }

            strDegreeNo = strDegreeNo.TrimEnd(',');
            strHostel = strHostel.Trim(',');
            if (strHostel != string.Empty)
            {
                if (strDegreeNo != string.Empty)
                {
                    preparedate = string.Empty;
                    preparedate = objCommon.LookUp("ACD_HOSTEL_RANKLIST", "PREPARE_DATE", "HOSTEL_SESSION_NO =" + ddlSession.SelectedValue + "AND EXAM_SESSION_NO =" + ddlExamSession.SelectedValue + " AND DEGREENO IN (" + strDegreeNo + ") AND HOSTELLIST='" + strHostel + "'  AND SEMESTERNO =" + ddlSemester.SelectedValue);
                    if (preparedate == string.Empty || chkoverwrite.Checked)
                    {
                        if (chkoverwrite.Checked)
                        {
                            chk = 1;
                        }
                        DataSet ds = objhostel.Getstudentrank(Convert.ToInt16(ddlSession.SelectedValue), Convert.ToInt16(ddlExamSession.SelectedValue), Convert.ToInt16(ddlHostelNo.SelectedValue), Convert.ToInt16(ddlSemester.SelectedValue), strDegreeNo, chk, strHostel);
                        if (ds != null && ds.Tables.Count > 0)
                        {
                            GridView8.DataSource = ds.Tables[0];
                            GridView8.DataBind();
                            pnlSelect.Visible = false;
                            trmodify.Visible = true;
                            ViewState["PreviousRowIndex"] = null;
                        }
                    }
                    else
                    {
                        DataSet ds = objCommon.FillDropDown("ACD_HOSTEL_RANKLIST A INNER JOIN ACD_STUDENT B ON A.IDNO=B.IDNO", "RECORDID", "REGNO,STUDNAME,STUD_RANK RANK,A.IDNO,CGPA,SGPA,DBO.FN_DESC('BRANCHSNAME',B.BRANCHNO)BRANCHNAME ", "HOSTEL_SESSION_NO =" + ddlSession.SelectedValue + "AND EXAM_SESSION_NO =" + ddlExamSession.SelectedValue + " AND A.DEGREENO IN (" + strDegreeNo + ") AND HOSTELLIST='" + strHostel + "'  AND A.SEMESTERNO =" + ddlSemester.SelectedValue, "STUD_RANK");
                        if (ds != null && ds.Tables.Count > 0)
                        {
                            GridView8.DataSource = ds.Tables[0];
                            GridView8.DataBind();
                            pnlSelect.Visible = false;
                            trmodify.Visible = true;
                            ViewState["PreviousRowIndex"] = null;
                        }

                    }
                }
                else
                {
                    objCommon.DisplayMessage(this.updpnlExam, "Please Select Degree", this.Page);
                }
            }
            else
            {
                objCommon.DisplayMessage(this.updpnlExam, "Please Select Hostel", this.Page);

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "HOSTEL_HostelMeritList. btnShow_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

        //DataSet ds = objCommon.FillDropDown("ACD_STUDENT", " TOP 10 REGNO", "STUDNAME,idno UserID", "DEGREENO=1 ", "REGNO DESC");
        //if (ds != null && ds.Tables.Count > 0)
        //{
        //    //gvUsers1.DataSource = ds.Tables[0];
        //    //gvUsers1.DataBind();
        //    GridView8.DataSource = ds.Tables[0];
        //    GridView8.DataBind();
        //    pnlSelect.Visible = false;
        //    trmodify.Visible = true;
        //    ViewState["PreviousRowIndex"] = null;
        //}
    }
    protected void GridView8_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //Attach click event to each row in Gridview
            e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.GridView8, "Select$" + e.Row.RowIndex);
        }
    }
    protected void GridView8_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //Check if Viewstate is null or not
        if (ViewState["PreviousRowIndex"] != null)
        {
            //Get the Previously selected rowindex
            var previousRowIndex = (int)ViewState["PreviousRowIndex"];
            //Get the previously selected row
            GridViewRow PreviousRow = GridView8.Rows[previousRowIndex];
            //Assign back color to previously selected row
            PreviousRow.BackColor = System.Drawing.Color.White;
        }
        //Get the Selected RowIndex
        int currentRowIndex = Int32.Parse(e.CommandArgument.ToString());
        //Get the GridViewRow from Current Row Index
        GridViewRow row = GridView8.Rows[currentRowIndex];
        //Assign the Back Color to Blue
        //Change this color as per your need
        row.BackColor = System.Drawing.Color.Aqua;
        //Assign the index as PreviousRowIndex
        ViewState["PreviousRowIndex"] = currentRowIndex;
    }
    protected void imgUpOrderBy_Click(object sender, ImageClickEventArgs e)
    {
        int currentRowIndex = 0;
        //Check if Viewstate is null or not

        if (ViewState["PreviousRowIndex"] != null && Convert.ToInt16(ViewState["PreviousRowIndex"]) > 0)
        {

            //Get the Previously selected rowindex
            var previousRowIndex = (int)ViewState["PreviousRowIndex"];
            //Get the previously selected row
            GridViewRow PreviousRow = GridView8.Rows[previousRowIndex];
            //Assign back color to previously selected row
            PreviousRow.BackColor = System.Drawing.Color.White;

            //Get the Selected RowIndex
            currentRowIndex = Convert.ToInt16(ViewState["PreviousRowIndex"]) - 1;
            //Get the GridViewRow from Current Row Index
            GridViewRow row = GridView8.Rows[currentRowIndex];
            //Assign the Back Color to Blue
            //Change this color as per your need
            row.BackColor = System.Drawing.Color.Aqua;
            //Assign the index as PreviousRowIndex


            int previnIndex = Convert.ToInt16(ViewState["PreviousRowIndex"]);

            //SWAP 1ST CELL
            string A = GridView8.Rows[previnIndex].Cells[0].Text;
            string B = GridView8.Rows[currentRowIndex].Cells[0].Text;
            GridView8.Rows[previnIndex].Cells[0].Text = B;
            GridView8.Rows[currentRowIndex].Cells[0].Text = A;

            //SWAP 2ND CELL
             A  = GridView8.Rows[previnIndex].Cells[1].Text;
            B = GridView8.Rows[currentRowIndex].Cells[1].Text;
            GridView8.Rows[previnIndex].Cells[1].Text = B;
            GridView8.Rows[currentRowIndex].Cells[1].Text = A;


            //SWAP 3RD CELL
            A = GridView8.Rows[previnIndex].Cells[2].Text;
            B = GridView8.Rows[currentRowIndex].Cells[2].Text;
            GridView8.Rows[previnIndex].Cells[2].Text = B;
            GridView8.Rows[currentRowIndex].Cells[2].Text = A;

            //SWAP 4ND CELL
            A = GridView8.Rows[previnIndex].Cells[3].Text;
            B = GridView8.Rows[currentRowIndex].Cells[3].Text;
            GridView8.Rows[previnIndex].Cells[3].Text = B;
            GridView8.Rows[currentRowIndex].Cells[3].Text = A;

            //SWAP 5TH CELL
            A = GridView8.Rows[previnIndex].Cells[4].Text;
            B = GridView8.Rows[currentRowIndex].Cells[4].Text;
            GridView8.Rows[previnIndex].Cells[4].Text = B;
            GridView8.Rows[currentRowIndex].Cells[4].Text = A;

            //SWAP 6TH CELL
            A = GridView8.Rows[previnIndex].Cells[5].Text;
            B = GridView8.Rows[currentRowIndex].Cells[5].Text;
            GridView8.Rows[previnIndex].Cells[5].Text = B;
            GridView8.Rows[currentRowIndex].Cells[5].Text = A;

            //SWAP 7TH CELL
            A = GridView8.Rows[previnIndex].Cells[6].Text;
            B = GridView8.Rows[currentRowIndex].Cells[6].Text;
            GridView8.Rows[previnIndex].Cells[6].Text = B;
            GridView8.Rows[currentRowIndex].Cells[6].Text = A;

            ////SWAP 5TH CELL
            //A = GridView8.Rows[previnIndex].Cells[7].Text;
            //B = GridView8.Rows[currentRowIndex].Cells[7].Text;
            //GridView8.Rows[previnIndex].Cells[7].Text = B;
            //GridView8.Rows[currentRowIndex].Cells[7].Text = A;

            ViewState["PreviousRowIndex"] = currentRowIndex;
        }
        else
        {
            objCommon.DisplayMessage(this.updpnlExam, "You can not move record up !!!", this.Page);
        }
    }
  
    protected void imgDownOrderBy_Click(object sender, ImageClickEventArgs e)
    {
        int currentRowIndex = 0;
        Int32 maxindex = GridView8.Rows.Count - 1;
        //Check if Viewstate is null or not
        if (ViewState["PreviousRowIndex"] != null && Convert.ToInt16(ViewState["PreviousRowIndex"]) != maxindex)
        {
            //Get the Previously selected rowindex
            var previousRowIndex = (int)ViewState["PreviousRowIndex"];
            //Get the previously selected row
            GridViewRow PreviousRow = GridView8.Rows[previousRowIndex];
            //Assign back color to previously selected row
            PreviousRow.BackColor = System.Drawing.Color.White;

            //Get the Selected RowIndex
            currentRowIndex = Convert.ToInt16(ViewState["PreviousRowIndex"]) + 1;
            //Get the GridViewRow from Current Row Index
            GridViewRow row = GridView8.Rows[currentRowIndex];
            //Assign the Back Color to Blue
            //Change this color as per your need
            row.BackColor = System.Drawing.Color.Aqua;
            //Assign the index as PreviousRowIndex
             
             


            int previnIndex = Convert.ToInt16(ViewState["PreviousRowIndex"]);

            //SWAP 1ST CELL
            string A = GridView8.Rows[previnIndex].Cells[0].Text;
            string B = GridView8.Rows[currentRowIndex].Cells[0].Text;
            GridView8.Rows[previnIndex].Cells[0].Text = B;
            GridView8.Rows[currentRowIndex].Cells[0].Text = A;

            //SWAP 2ND CELL
              A = GridView8.Rows[previnIndex].Cells[1].Text;
              B = GridView8.Rows[currentRowIndex].Cells[1].Text;
            GridView8.Rows[previnIndex].Cells[1].Text = B;
            GridView8.Rows[currentRowIndex].Cells[1].Text = A;
           

            //SWAP 3RD CELL
            A = GridView8.Rows[previnIndex].Cells[2].Text;
            B = GridView8.Rows[currentRowIndex].Cells[2].Text;
            GridView8.Rows[previnIndex].Cells[2].Text = B;
            GridView8.Rows[currentRowIndex].Cells[2].Text = A;

            //SWAP 4ND CELL
            A = GridView8.Rows[previnIndex].Cells[3].Text;
            B = GridView8.Rows[currentRowIndex].Cells[3].Text;
            GridView8.Rows[previnIndex].Cells[3].Text = B;
            GridView8.Rows[currentRowIndex].Cells[3].Text = A;

            //SWAP 5TH CELL
            A = GridView8.Rows[previnIndex].Cells[4].Text;
            B = GridView8.Rows[currentRowIndex].Cells[4].Text;
            GridView8.Rows[previnIndex].Cells[4].Text = B;
            GridView8.Rows[currentRowIndex].Cells[4].Text = A;

            //SWAP 6TH CELL
            A = GridView8.Rows[previnIndex].Cells[5].Text;
            B = GridView8.Rows[currentRowIndex].Cells[5].Text;
            GridView8.Rows[previnIndex].Cells[5].Text = B;
            GridView8.Rows[currentRowIndex].Cells[5].Text = A;

            //SWAP 7TH CELL
            A = GridView8.Rows[previnIndex].Cells[6].Text;
            B = GridView8.Rows[currentRowIndex].Cells[6].Text;
            GridView8.Rows[previnIndex].Cells[6].Text = B;
            GridView8.Rows[currentRowIndex].Cells[6].Text = A;

            ////SWAP 8TH CELL
            //A = GridView8.Rows[previnIndex].Cells[7].Text;
            //B = GridView8.Rows[currentRowIndex].Cells[7].Text;
            //GridView8.Rows[previnIndex].Cells[7].Text = B;
            //GridView8.Rows[currentRowIndex].Cells[7].Text = A;

            ViewState["PreviousRowIndex"] = currentRowIndex;
            
          
        }
        else
        {
            objCommon.DisplayMessage(this.updpnlExam, "You can not move record down !!!", this.Page);
        }
    }
    protected void btnBack_Click(object sender, ImageClickEventArgs e)
    {
        pnlSelect.Visible = true;
        trmodify.Visible = false;
        ViewState["PreviousRowIndex"] = null;
    }
    protected void Save_Click(object sender, EventArgs e)
    {
        string strDegreeNo = string.Empty;
        string strHostel = string.Empty;
        string strIdno = string.Empty;
        string strSrno = string.Empty;
        int modifyrank=0;
        int rank=0;
        int idno=0;
        
        int i = 0;
        decimal cgpa=0;
        decimal sgpa=0;
      
        int count = 0;
        int chk = 0;
        string preparedate = string.Empty;
        try
        {
            foreach (ListItem items in cblstDegree.Items)
            {
                if (items.Selected == true)
                {
                    strDegreeNo += items.Value + ",";
                    count++;
                }
            }
            foreach (ListItem items in cblstHostel.Items)
            {
                if (items.Selected == true)
                {
                    strHostel += items.Value + ",";
                    count++;
                }
            }

            strDegreeNo = strDegreeNo.TrimEnd(',');
            strHostel = strHostel.Trim(',');
            //strIdno=strIdno.Trim(',');

            if (strHostel != string.Empty)
            {
                
                if (strDegreeNo != string.Empty)
                {
                    for (i = 0; i < GridView8.Rows.Count; i++)
                    {
                        
                        idno  = Convert.ToInt32(GridView8.Rows[i].Cells[3].Text);
                       
                        modifyrank = Convert.ToInt32(GridView8.Rows[i].Cells[0].Text);
                        rank = i + 1;
                        cgpa = Convert.ToDecimal(GridView8.Rows[i].Cells[6].Text);
                        sgpa  = Convert.ToDecimal(GridView8.Rows[i].Cells[7].Text);
                        CustomStatus cs = (CustomStatus)objhostel.Insertstudentrank(Convert.ToInt16(ddlSession.SelectedValue), Convert.ToInt16(ddlExamSession.SelectedValue), Convert.ToInt16(ddlHostelNo.SelectedValue), Convert.ToInt16(ddlSemester.SelectedValue), strDegreeNo, chk, strHostel, idno, modifyrank, rank, cgpa, sgpa);
                       
                    }
                    objCommon.DisplayMessage(this.updpnlExam, "Save Successfully!!!", this.Page);
                    chkoverwrite.Checked = false;
                    btnRankList_Click(sender, e);
                }
                else
                {
                    objCommon.DisplayMessage(this.updpnlExam, "Please Select Degree", this.Page);
                }
            }
            else
            {
                objCommon.DisplayMessage(this.updpnlExam, "Please Select Hostel", this.Page);

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "HOSTEL_HostelMeritList. btnShow_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnreport_Click(object sender, EventArgs e)
    {
        showReport("Rank List Modification", "rptModifiedHostelRankListReport.rpt");
    }
    protected void btnList_Click(object sender, EventArgs e)
    {
        showReport("Rank List Modification", "rptHostelRankListReport.rpt");
  
    }
}
