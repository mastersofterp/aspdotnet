using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using System.Data;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class ACADEMIC_EXAMINATION_BackMarkEntry : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ExamController objEC = new ExamController();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0", "");
            objCommon.FillDropDownList(ddlsession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO>0", "");
          
            pnlSelection.Visible = true;

        }
        
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
   
    protected void btnShow_Click(object sender, EventArgs e)
 {
         if (txtRollNo.Text == string.Empty)
        {
            objCommon.DisplayMessage("Please Enter Reg. No...!!", this.Page);
            return;
        }
         if (ddlSemester.SelectedValue == "0")
         {
             objCommon.DisplayMessage("Please select semester", this.Page);
             return;
         }
        string idno = objCommon.LookUp("ACD_STUDENT", "IDNO", "REGNO = '" + txtRollNo.Text.Trim() + "'");
        ViewState["idno"] = idno;
        if (idno == "")
        {
            //objCommon.DisplayMessage("Student Not Found having Roll. No.'" + txtRollNo.Text.Trim() + "'", this.Page);
            objCommon.DisplayMessage("Student Not Found for This Roll. No...!!", this.Page);
            return;
           
        }

       
        ShowDetails();
        
        
     }
    private void chklock()
    {
        string chkhist = string.Empty;
        string idno = objCommon.LookUp("ACD_STUDENT", "IDNO", "REGNO = '" + txtRollNo.Text.Trim() + "'");
        string chktr = objCommon.LookUp("acd_trresult", "count(1)", "idno=" + Convert.ToInt32(idno) + "and semesterno=" + Convert.ToInt32(ddlSemester.SelectedValue));
        string chklocktr = objCommon.LookUp("acd_trresult", "count(1)", "idno=" + Convert.ToInt32(idno) + "and semesterno=" + Convert.ToInt32(ddlSemester.SelectedValue) +"and lock=1");
        ViewState["chklock"] = chklocktr;
        string cnt = objCommon.LookUp("acd_student_result_hist", "count(1)", " idno=" + Convert.ToInt32(idno) + "and semesterno=" + Convert.ToInt32(ddlSemester.SelectedValue)+"and( isnull(s1mark,0)<>0 or isnull(s2mark,0)<>0 or isnull(s3mark,0)<>0 or isnull(extermark,0)<>0 )");
        
        if (Convert.ToInt32(cnt) > 0)
        {
           // if (Convert.ToInt32(chktr) > 0)
           // {
               
                    btnlock.Enabled = true; //btnSubmit.Enabled = false;
                    
           }
                else
                {
                    btnlock.Enabled = false; btnSubmit.Enabled = true;
                   
                }

        if (Convert.ToInt32(chklocktr) > 0)
        {
            txtregcredit.Enabled = false;
            txtearncredit.Enabled = false;
            txtsgpa.Enabled = false;
            txtCgpa.Enabled = false;
            txttotobtmrk.Enabled = false;
            txtOutOfmark.Enabled = false;
            ddlResult.Enabled = false;
        }
        else
        {
            txtregcredit.Enabled = true;
            txtearncredit.Enabled = true;
            txtsgpa.Enabled = true;
            txtCgpa.Enabled = true;
            txttotobtmrk.Enabled = true;
            txtOutOfmark.Enabled = true;
            ddlResult.Enabled = true;
        }
    }
    private void ShowDetails()
    {
        try
        {
           
            string idno = objCommon.LookUp("ACD_STUDENT", "IDNO", "REGNO = '" + txtRollNo.Text.Trim() + "'");
            ViewState["idno"] = idno;
            DataSet dsinfo = objCommon.FillDropDown("ACD_STUDENT", "studname,dbo.fn_desc('degreename',degreeno)degreename,dbo.fn_desc('BRANCHLNAME',branchno)branchname,dbo.fn_desc('SECTIONNAME',sectionno)section,degreeno", "dbo.fn_desc('ADMBATCH',admbatch)admbatch", "idno=" + Convert.ToInt32(idno), string.Empty);
            if (dsinfo != null && dsinfo.Tables[0].Rows.Count > 0)
            {
                trinfo.Visible = true;
                lblStudname.Text = dsinfo.Tables[0].Rows[0]["studname"].ToString();
                lblDegree.Text = dsinfo.Tables[0].Rows[0]["degreename"].ToString();
                lblBranch.Text = dsinfo.Tables[0].Rows[0]["branchname"].ToString();
                lbladmbatch.Text = dsinfo.Tables[0].Rows[0]["admbatch"].ToString();
                lblsection.Text = dsinfo.Tables[0].Rows[0]["section"].ToString();
                ViewState["degree"] = dsinfo.Tables[0].Rows[0]["degreeno"].ToString();

            }
            else
            {
                trinfo.Visible = false;
            }
            DataSet dstr = objCommon.FillDropDown("ACD_TRRESULT", "RESULT,CAST(SGPA AS DECIMAL(18,2))SSGPA,CAST(CGPA AS DECIMAL(18,2))CCGPA", "*", "IDNO=" + Convert.ToInt32(idno) + "AND SEMESTERNO=" + ddlSemester.SelectedValue, string.Empty);
            if (dstr != null && dstr.Tables[0].Rows.Count > 0)
            {
                txtregcredit.Text = dstr.Tables[0].Rows[0]["REGD_CREDITS"].ToString();
                txtearncredit.Text = dstr.Tables[0].Rows[0]["EARN_CREDITS"].ToString();
                txtsgpa.Text = dstr.Tables[0].Rows[0]["SSGPA"].ToString();
                txtCgpa.Text = dstr.Tables[0].Rows[0]["CCGPA"].ToString();
                txttotobtmrk.Text = dstr.Tables[0].Rows[0]["TOTAL_OBTD_MARKS"].ToString();
                ddlResult.SelectedValue = dstr.Tables[0].Rows[0]["RESULT"].ToString();
                txtOutOfmark.Text = dstr.Tables[0].Rows[0]["OUTOFMARKS"].ToString();
                ViewState["lock"] = dstr.Tables[0].Rows[0]["lock"].ToString();
            }
            else
            {
                txtregcredit.Text = string.Empty;
                txtearncredit.Text = string.Empty;
                txtsgpa.Text = string.Empty;
                txtCgpa.Text = string.Empty;
                txttotobtmrk.Text = string.Empty;
                txtOutOfmark.Text = string.Empty;
                ddlResult.SelectedValue = ddlResult.SelectedValue.ToString();
                txtregcredit.Enabled = true;
                txtearncredit.Enabled = true;
                txtsgpa.Enabled = true;
                txtCgpa.Enabled = true;
                txttotobtmrk.Enabled = true;
                ddlResult.Enabled = true; txtOutOfmark.Enabled = true;
            }
            DataSet dsCurrCourses = objCommon.FillDropDown("ACD_STUDENT_RESULT_hist a inner join acd_student b on(a.idno=b.idno) inner join acd_scheme c on (a.schemeno=c.schemeno) inner join acd_course d on(d.courseno=a.courseno)", "distinct a.idno,a.regno,isnull(D.s2max,0)s2max,isnull(D.s3max,0)s3max,isnull(a.s1mark,0)s1mark,isnull(a.s2mark,0)s2mark,isnull(a.s3mark,0)s3mark,*", "b.studname,'" + ViewState["lock"] + "' as lock", "b.IDNO = " + idno + "and a.semesterno = " + ddlSemester.SelectedValue +"and a.sessionno="+Convert.ToInt32(ddlsession.SelectedValue), "a.courseno");
           
           // DataSet dsCurrCourses = objCommon.FillDropDown("ACD_STUDENT_RESULT_hist a inner join acd_scheme b on(a.schemeno=b.schemeno)", "a.REGNO,a.COURSENAME,a.MARKTOT,a.schemeno,b.PATTERNNO", "a.S1MARK,a.S2MARK,a.LOCKS1,a.S3MARK,a.LOCKS2,a.EXTERMARK,a.LOCKE,a.S4MARK,a.LOCKS4,a.locks3,* ", "a.IDNO = " + idno + "and a.semesterno = " + ddlSemester.SelectedValue, "a.COURSENAME");
            if (dsCurrCourses != null && dsCurrCourses.Tables.Count > 0)
            {
                if (dsCurrCourses.Tables[0].Rows.Count > 0)
                {
                    ViewState["schemeno"] = dsCurrCourses.Tables[0].Rows[0]["schemeno"].ToString();
                    pnlCourse.Visible = true;
                    trresult.Visible = true;
                    trevent.Visible = true;
                    //gvStudent.DataSource = dsCurrCourses.Tables[0];
                    //gvStudent.DataBind();
                   // string patern = objCommon.LookUp("acd_scheme", "PATTERNNO", "schemeno=" + Convert.ToInt32(dsCurrCourses.Tables[0].Rows[0]["schemeno"].ToString()));
                    int jj = 2;
                    string patern = dsCurrCourses.Tables[0].Rows[0]["PATTERNNO"].ToString();
                   //ViewState["pattern"] = patern;
                   //DataSet dspattern = objCommon.FillDropDown("ACD_EXAM_NAME", "ROW_NUMBER() OVER(ORDER BY EXAMNO) SRNO,*,examname", "fldname", "patternno=" + Convert.ToInt32(ViewState["pattern"]) + "  AND EXAMNAME!='' AND FLDNAME!='EXTERMARK' ", "examno");
                   //DataTableReader dtrExams = dspattern.Tables[0].CreateDataReader();
                   //for (jj = 3; jj < gvStudent.Columns.Count   ; jj++)
                   //{
                   //    while (dtrExams.Read())
                   //    {
                   //        string s = dtrExams["FLDNAME"].ToString();
                   //        int examrow = (Convert.ToInt32(dtrExams["SRNO"].ToString()) - 1);
                          
                   //        if (Convert.ToInt32(dsCurrCourses.Tables[0].Rows[0][s + "MAX"]) > 0 )
                   //        {

                   //            gvStudent.Columns[jj].HeaderText = dspattern.Tables[0].Rows[examrow]["examname"].ToString();// +"[Max : " + dsCurrCourses.Tables[0].Rows[0][s + "MAX"].ToString() + "]";
                   //            gvStudent.Columns[jj].FooterText = s;
                   //            gvStudent.Columns[jj].Visible = true;
                   //            ViewState["STRING"] += s +"$";
                   //            break;
                   //        }
                   //    } 
                   //}
                   //gvStudent.DataSource = dsCurrCourses.Tables[0];
                   //gvStudent.DataBind();



                   DataSet dspattern = objCommon.FillDropDown("ACD_EXAM_NAME", "ROW_NUMBER() OVER(ORDER BY EXAMNO) SRNO", "EXAMNO, EXAMNAME, FLDNAME", "patternno=" + patern + "  AND EXAMNAME!='' AND FLDNAME!='EXTERMARK'", "examno");
                  // DataTableReader dtrExams = dspattern.Tables[0].CreateDataReader();
                   for (int j = 1; j < dsCurrCourses.Tables[0].Rows.Count; j++)
                   {
                      DataTableReader dtrExams = dspattern.Tables[0].CreateDataReader();
                  
                       
                       while (dtrExams.Read())
                       {
                           string s = dtrExams["FLDNAME"].ToString();
                           int examrow = (Convert.ToInt32(dtrExams["SRNO"].ToString()) - 1);
                           int col = Convert.ToInt16(s.Substring(1, 1));
                           if (Convert.ToInt32(dsCurrCourses.Tables[0].Rows[j][s + "MAX"]) > 0)
                           {
                               gvStudent.Columns[2 + col].HeaderText = dspattern.Tables[0].Rows[examrow]["examname"].ToString(); 
                               gvStudent.Columns[2 + col].FooterText = s;
                               gvStudent.Columns[2 + col].Visible = true;
                               gvStudent.Columns[2 + col].ControlStyle.BorderColor = System.Drawing.Color.Black;
                               ViewState["STRING"] += s + "$";
                               //break;
                           }
                           else
                           {
                               //gvStudent.Columns[2 + col].HeaderText = dspattern.Tables[0].Rows[examrow]["examname"].ToString() + "[Max : " + dsCurrCourses.Tables[0].Rows[0][s + "MAX"].ToString() + "]";
                               //gvStudent.Columns[2 + col].FooterText = s;

                               //gvStudent.Columns[2 + col].ControlStyle.BorderColor = System.Drawing.Color.Red;
                           }
                       }
                   }
                   DataSet dsextpattern = objCommon.FillDropDown("ACD_EXAM_NAME", "ROW_NUMBER() OVER(ORDER BY EXAMNO) SRNO", "EXAMNO, EXAMNAME, FLDNAME", "patternno=" + patern + "  AND EXAMNAME!='' AND FLDNAME='EXTERMARK'", "examno");
                   if (dsextpattern.Tables[0].Rows.Count > 0)
                   {
                       gvStudent.Columns[13].HeaderText = dsextpattern.Tables[0].Rows[0]["examname"].ToString();

                   }

                              
                   gvStudent.DataSource = dsCurrCourses.Tables[0];
                   gvStudent.DataBind();

                    //btnSubmit.Visible = true;
                    // divCourses.Visible = true;
                  btnSubmit.Enabled = true;
                  for (int j = 2; j < gvStudent.Columns.Count; j++)    //columns
                  {
                      for (int i = 0; i < gvStudent.Rows.Count; i++)   //rows 
                      {

                          if (dsCurrCourses.Tables[0].Rows[i]["marktot"].ToString() != "")
                          {
                              TextBox txtmarktot = gvStudent.Rows[i].Cells[j].FindControl("txtTotMarksAll") as TextBox;
                              //  txtmarktot.Enabled = false;
                              if (Convert.ToDouble(dsCurrCourses.Tables[0].Rows[i]["marktot"].ToString()) == 0)
                              {

                                 // txtmarktot.Text = string.Empty;
                                  txtmarktot.Enabled = true;
                              }
                              if (dsCurrCourses.Tables[0].Rows[i]["locki"].ToString() == "True")
                              {
                                  txtmarktot.Enabled = false;
                              }
                          }
                          if (dsCurrCourses.Tables[0].Rows[i]["marktot"].ToString() != "")
                          {
                              TextBox txts1 = gvStudent.Rows[i].Cells[j].FindControl("txtTotMarks") as TextBox;
                              // txts1.Enabled = false;
                              if (Convert.ToDouble(dsCurrCourses.Tables[0].Rows[i]["s1mark"].ToString()) == 0)
                              {

                                  //txts1.Text = string.Empty;
                                  txts1.Enabled = true;
                              }
                              if (dsCurrCourses.Tables[0].Rows[i]["locks1"].ToString() == "True")
                              {
                                  txts1.Enabled = false;
                              }
                          }
                          if (dsCurrCourses.Tables[0].Rows[i]["marktot"].ToString() != "")
                          {
                              TextBox txts2 = gvStudent.Rows[i].Cells[j].FindControl("txtTAMarks") as TextBox;
                              //  txts2.Enabled = false;
                              if (Convert.ToDouble(dsCurrCourses.Tables[0].Rows[i]["s2mark"].ToString()) == 0)
                              {

                                 // txts2.Text = string.Empty;
                                  txts2.Enabled = true;
                              }
                              if (dsCurrCourses.Tables[0].Rows[i]["locks2"].ToString() == "True")
                              {
                                  txts2.Enabled = false;
                              }
                          }

                          if (dsCurrCourses.Tables[0].Rows[i]["marktot"].ToString() != "")
                          {
                              TextBox txts3 = gvStudent.Rows[i].Cells[j].FindControl("txtESPRMarks") as TextBox;
                              // txts3.Enabled = false;
                              if (Convert.ToDouble(dsCurrCourses.Tables[0].Rows[i]["s3mark"].ToString()) == 0)
                              {

                                 // txts3.Text = string.Empty;
                                  txts3.Enabled = true;
                              } if (dsCurrCourses.Tables[0].Rows[i]["locks3"].ToString() == "True")
                              {
                                  txts3.Enabled = false;
                              }
                          }
                          if (dsCurrCourses.Tables[0].Rows[i]["EXTERMARK"].ToString() != "")
                          {
                              TextBox txtext = gvStudent.Rows[i].Cells[j].FindControl("txtESMarks") as TextBox;
                              // txtext.Enabled = false;
                              if (Convert.ToDouble(dsCurrCourses.Tables[0].Rows[i]["extermark"].ToString()) == 0)
                              {

                                 // txtext.Text = string.Empty;
                                  txtext.Enabled = true;
                              } if (dsCurrCourses.Tables[0].Rows[i]["locke"].ToString() == "True")
                              {
                                  txtext.Enabled = false;
                              }
                          }
                      }
                  }
                   
                  chklock();
                }
                else
                {
                    objCommon.DisplayMessage("Student with Roll No." + txtRollNo.Text.Trim() + ",  Course Registration Not Exists!", this.Page);
                    pnlCourse.Visible = false;
                    trresult.Visible = false;
                    trevent.Visible = false;
                    return;
                }

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "BackMarkEntry.ddlSession_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void internalsubmit()
    {

        string studids = string.Empty; 
        string studcoursenos = string.Empty;
        string examname = string.Empty;
        string marks = string.Empty;

        int k = 0;
        int columns = 0;
        for (int i = 3; i < gvStudent.Columns.Count - 2; i++)
        {
            if (gvStudent.Columns[i].Visible == true)
            {
                columns++;
            }
        }
        int count = columns * (gvStudent.Rows.Count);
        for (int i = 0; i < gvStudent.Rows.Count; i++)
        {
            Label lblidnos = gvStudent.Rows[i].FindControl("lblIDNO") as Label;
            Label lblCourseName = gvStudent.Rows[i].FindControl("lblCourseName") as Label;
            for (int j = 3; j < gvStudent.Columns.Count - 2; j++)
            {
                if (gvStudent.Columns[j].Visible == true)
                {
                    TableCell tc = gvStudent.Rows[i].Cells[j];
                    TextBox myControl = null;
                    foreach (Control c in tc.Controls)
                    {
                        if (c is TextBox)
                        {
                            myControl = (TextBox)c;
                            break;
                        }
                    }
                    string value = myControl.Text.Trim();
                    if (value != "" || value != string.Empty)
                    {
                        marks += value + ',';
                    }
                    else
                    {
                        marks += "-100" + ',';
                        k++;
                    }
                }
            }
            studids = lblidnos.ToolTip.ToString() + ',';
            studcoursenos += lblCourseName.ToolTip.ToString() + ',';
        }
        for (int j = 3; j < gvStudent.Columns.Count - 2; j++)
        {
            if (gvStudent.Columns[j].Visible == true)
            {
                examname += gvStudent.Columns[j].FooterText + "MARK" + ',';
            }
        }
        if (marks != "")
        {
            if (k == count)
            {
                objCommon.DisplayMessage("Please Enter marks", this.Page);
                return;
            }
            else
            {
                if (marks.Substring(marks.Length - 1) == ",")
                    marks = marks.Substring(0, marks.Length - 1);
            }
        }
        if (studcoursenos != "")
        {
            if (studcoursenos.Substring(studcoursenos.Length - 1) == ",")
                studcoursenos = studcoursenos.Substring(0, studcoursenos.Length - 1);
        }
        if (examname != "")
        {
            if (examname.Substring(examname.Length - 1) == ",")
                examname = examname.Substring(0, examname.Length - 1);
        }
        int cs = objEC.MarkUpdate(Convert.ToInt32(ViewState["idno"]), studcoursenos, Convert.ToInt32(ddlSemester.SelectedValue), marks,examname,Convert.ToInt32(ddlsession.SelectedValue));

        if (cs == 2)
        
        {
            objCommon.DisplayMessage("Record submitted successfully", this.Page);
            chklock();
            return;
        }

    }
    private void externalsubmit()
{
    string studids = string.Empty;
    string examname = string.Empty; string studcoursenos = string.Empty;
                string marks = string.Empty;

                int count = gvStudent.Rows.Count;
                int j = 0;
                for (int i = 0; i < gvStudent.Rows.Count; i++)
                {
                    Label lblidnos = gvStudent.Rows[i].FindControl("lblIDNO") as Label;
                    Label lblcourseno = gvStudent.Rows[i].FindControl("lblCoursename") as Label;

                    TextBox txts1marks = gvStudent.Rows[i].FindControl("txtESMarks") as TextBox;
                   
                    if (txts1marks.Text != string.Empty || txts1marks.Text != "")
                    {
                        marks += txts1marks.Text.ToString() + ',';
                    }
                    else
                    {
                        marks += "-100" + ',';
                        j++;
                    }
                    studids = lblidnos.ToolTip.ToString() ;
                    studcoursenos += lblcourseno.ToolTip.ToString() + ',';
                }

                if (marks != "")
                {
                    if (j == count)
                    {
                        objCommon.DisplayMessage("Please Enter marks ", this.Page);
                        return;
                    }
                    else
                    {
                        if (marks.Substring(marks.Length - 1) == ",")
                            marks = marks.Substring(0, marks.Length - 1);
                    }
                }
                if (studcoursenos != "")
                {
                    if (studcoursenos.Substring(studcoursenos.Length - 1) == ",")
                        studcoursenos = studcoursenos.Substring(0, studcoursenos.Length - 1);
                }
                if (studids != "")
                {
                    if (studids.Substring(studids.Length - 1) == ",")
                        studids = studids.Substring(0, studids.Length - 1);
                }

                examname = "EXTERMARK";
                int cs = objEC.MarkUpdate(Convert.ToInt32(ViewState["idno"]), studcoursenos, Convert.ToInt32(ddlSemester.SelectedValue), marks, examname, Convert.ToInt32(ddlsession.SelectedValue));

                if (cs == 2)
                {
                    objCommon.DisplayMessage("Record submitted successfully", this.Page);
                    chklock();
                    return;
                }       
}
protected void btnSubmit_Click(object sender, EventArgs e)
{
    try
    {
        if (Convert.ToInt32(ViewState["chklock"]) == 1)
        {
            objCommon.DisplayMessage("Record already saved & locked", this.Page); return;
        }
        internalsubmit();
        externalsubmit();
        //int c = objEC.MarkUpdate(lblRollno.Text.ToString(), lblCourseName.Text.ToString(), Convert.ToDouble(txts1mark.Text), Convert.ToDouble(txts2mark.Text), Convert.ToDouble(txts3mark.Text), Convert.ToDouble(txtextmark.Text), Convert.ToDouble(txttotmark.Text));

        string idno = objCommon.LookUp("ACD_STUDENT", "IDNO", "REGNO = '" + txtRollNo.Text.Trim() + "'");
        if ((Convert.ToInt32(ViewState["degree"]) != 2))
        {
            if (Convert.ToInt32(ViewState["degree"]) != 5)
            {
                if (Convert.ToInt32(ViewState["degree"]) != 7)
                {
                    if (txtCgpa.Text == string.Empty || txtsgpa.Text == string.Empty || txtearncredit.Text == string.Empty || txtregcredit.Text == string.Empty)
                    {

                        objCommon.DisplayMessage("Please enter all details properly.", this.Page);
                        return;
                    }
                }
            }
        }
        else
        {
            txtCgpa.Text = "0";
            txtsgpa.Text = "0"; txtearncredit.Text = "0"; txtregcredit.Text = "0";
        }
        int CHKTR = objEC.TrMarkUpdate(txtRollNo.Text.ToString(), Convert.ToInt32(txtregcredit.Text), Convert.ToInt32(txtearncredit.Text), ddlResult.SelectedItem.ToString(), Convert.ToDouble(txtsgpa.Text), Convert.ToDouble(txtCgpa.Text), Convert.ToInt32(idno), Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToDouble(txttotobtmrk.Text), Convert.ToInt32(txtOutOfmark.Text),Convert.ToInt32(ddlsession.SelectedValue));
        if (CHKTR == 2)
        {
            objCommon.DisplayMessage("Record submitted successfully", this.Page);
            chklock();
            return;
        }    
       
    }
    
    catch (Exception ex)
    {
        if (Convert.ToBoolean(Session["error"]) == true)
            objUCommon.ShowError(Page, "ExamName.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
        else
            objUCommon.ShowError(Page, "Server UnAvailable");
    }
}



protected void gvStudent_RowDataBound(object sender, GridViewRowEventArgs e)
{
    if (e.Row.RowType == DataControlRowType.DataRow)
    {
        TextBox txtMarks1 = e.Row.FindControl("txtTotMarks") as TextBox;
        TextBox txtMarks2 = e.Row.FindControl("txtTAMarks") as TextBox;
        TextBox txtMarks3 = e.Row.FindControl("txtESPRMarks") as TextBox;
        TextBox txtMarks4 = e.Row.FindControl("txts4mark") as TextBox;
        TextBox txtMarks5 = e.Row.FindControl("txts5mark") as TextBox;

        TextBox txtMarks6 = e.Row.FindControl("txts6mark") as TextBox;
        TextBox txtMarks7 = e.Row.FindControl("txts7mark") as TextBox;
        TextBox txtMarks8 = e.Row.FindControl("txts8mark") as TextBox;
        TextBox txtMarks9 = e.Row.FindControl("txts9mark") as TextBox;
        TextBox txtMarks10 = e.Row.FindControl("txts10mark") as TextBox;
       
        // string ii=  txtMarks1.Text.ToString();
        HiddenField hdnlock = e.Row.FindControl("hdnlock") as HiddenField;
        if (hdnlock.Value == "1")
        {
            txtMarks1.Enabled = false;
            txtMarks2.Enabled = false;
            txtMarks3.Enabled = false;
            txtMarks4.Enabled = false;
            txtMarks5.Enabled = false;

            txtMarks6.Enabled = false;
            txtMarks7.Enabled = false;
            txtMarks8.Enabled = false; txtMarks9.Enabled = false;
            txtMarks10.Enabled = false;
            
        }
        else
        {
            txtMarks1.Enabled = true;

            txtMarks2.Enabled = true;

            txtMarks3.Enabled = true;

            txtMarks4.Enabled = true;

            txtMarks5.Enabled = true;
            txtMarks6.Enabled = true;
            txtMarks7.Enabled = true;
            txtMarks8.Enabled = true; txtMarks9.Enabled = true;
            txtMarks10.Enabled = true;

        }
    }
}
protected void btnlock_Click(object sender, EventArgs e)
{
    if (Convert.ToInt32(ViewState["chklock"]) == 1)
    {
        objCommon.DisplayMessage("Record already locked", this.Page); return;
    }
    int CS=objEC.LOCKUpdate(Convert.ToInt32( ViewState["idno"]),Convert.ToInt32(ddlSemester.SelectedValue),ViewState["STRING"].ToString(), Convert.ToInt32(ddlsession.SelectedValue));
    if (CS == 2)
    {
        objCommon.DisplayMessage("Record Locked Successfully", this.Page);
        ShowDetails();
        return;

    }
    else
    {
        objCommon.DisplayMessage("Error...", this.Page);

    }

    
}
}
