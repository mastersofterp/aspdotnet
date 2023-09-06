
//============================================================
// Project Name   : IITMS   
// Created By     : Sumit Karangale
// Page Name      : TP Dashboard                                                                 
// Description    : Show all t&p information on dashboard    
// Creation Date  : 28-april-2020                              
// Modifying Date :                                                                                  
//============================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS;
using InfoSoftGlobal;
using System.Web.UI.DataVisualization.Charting;
using System.Drawing;
using System.Text;

using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;
using System.Web.Services;
using System.Web.Script.Serialization;
using System.IO;

public partial class TPDashboard : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    TPController objTP = new TPController();
    User_AccController objUACC = new User_AccController();

    public string sMarquee = string.Empty;
    public string Notice = string.Empty;


    protected void Page_Load(object sender, EventArgs e)
    {
        //Check Session
        if (Session["userno"] == null || Session["username"] == null ||
            Session["usertype"] == null || Session["userfullname"] == null || Session["coll_name"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        // string no = Session["username"].ToString();
        //int sessionno = Convert.ToInt32(objCommon.LookUp("ACD_DASHBOARD_MASTER", "SESSIONNO", "status=1"));
        string sess = objCommon.LookUp("ACD_DASHBOARD_MASTER", "ISNULL(SESSIONNO,0)SESSIONNO", "status=1");
        int sessionno = Convert.ToInt32(sess == "" ? "0" : sess);
        string sessionname = objCommon.LookUp("ACD_SESSION_MASTER", "SESSION_NAME", "SESSIONNO=" + Convert.ToInt32(sessionno));

        if (!Page.IsPostBack)
        {

            if (Session["username"].ToString() == "MastersoftAccount")
            {
                //lblreport.Text = "(Click to view the report)";
                //lblreport.Visible = true;
            }
            //objCommon.FillDropDownList(ddlbranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "", "BRANCHNO");
            //objCommon.FillDropDownList(ddlbranchfees, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "", "BRANCHNO");

            //if (Session["usertype"].ToString() == "3")
            //{
            //    // trNews.Visible = false;
            //    //trTimeTable.Visible = true;
            //    // this.sessionname();
            //    CourseTeacherAllotController objCT = new CourseTeacherAllotController();
            //    //   int sessionNo = Convert.ToInt16(objCommon.LookUp("REFF", "ATT_SESSIONNO", string.Empty));
            //    //DataSet dsTimeTable = objCT.DisplayTimeTableFaculty(sessionNo, 0, 0, Convert.ToInt32(Session["userno"]));
            //    //lvTimeTable.DataSource = dsTimeTable.Tables[0];
            //    //lvTimeTable.DataBind();
            //    // (lvTimeTable.FindControl("divTitle") as HtmlGenericControl).InnerHtml = "Time Table for " + dsTimeTable.Tables[0].Rows[0]["SESSIONNAME"].ToString();
            //}
            //else
            //{

            NewsController objNC = new NewsController();
            sMarquee = objNC.ScrollingNews(Request.ApplicationPath);
            Notice = objNC.NoticeBoard(Request.ApplicationPath);

            // }



            DataSet ds = objCommon.FillDropDown("ACD_DASHBOARD_CONFIGURATION CROSS APPLY (SELECT VALUE FROM [DBO].[SPLIT] (DASHBOARD,','))A INNER JOIN ACD_DASHBOARD_MASTER DM ON(A.VALUE=DM.ID)", "USERTYPE", "A.VALUE AS DASHBOARD", "USERTYPE=" + Session["usertype"].ToString() + "AND STATUS=1 ", "USERTYPE");
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int cnt = 0; cnt < ds.Tables[0].Rows.Count; cnt++)
                {
                    int val = 0;
                    string value = ds.Tables[0].Rows[cnt]["DASHBOARD"].ToString();

                    if (value != "")
                    {
                        val = Convert.ToInt32(value);
                    }


                    if (val == 65) //66
                    {
                        DataTable dt = new System.Data.DataTable();
                        DataSet ds1 = objUACC.GetTpAppliedRejectOnTabulerform(val, sessionno);
                        Session["ApproveRejectStatus"] = ds1;
                        dt = ds1.Tables[0];

                        string[] ar1 = new string[dt.Rows.Count];
                        string[] ar2 = new string[dt.Rows.Count];
                        //string[] ar3 = new string[dt.Rows.Count];
                        //string[] ar4 = new string[dt.Rows.Count];
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            ar1[j] = dt.Rows[j]["Approval_Student"].ToString();
                            ar2[j] = dt.Rows[j]["Rejected_Student"].ToString();
                            //ar3[j] = dt.Rows[j]["MALE"].ToString();
                            //ar4[j] = dt.Rows[j]["FEMALE"].ToString();
                        }
                        DotNet.Highcharts.Highcharts chart = new DotNet.Highcharts.Highcharts("chart523")
                       .InitChart(new DotNet.Highcharts.Options.Chart { DefaultSeriesType = ChartTypes.Column, Height = 380 })
                        .SetTooltip(new Tooltip { Formatter = "function() { return '<b>'+ this.x +'</b>: '+ this.y ; }" })
                .SetTitle(new DotNet.Highcharts.Options.Title { Text = "" })
                .SetLegend(new DotNet.Highcharts.Options.Legend { Enabled = true })
                .SetPlotOptions(new PlotOptions
                {

                    Column = new PlotOptionsColumn
                    {

                        AllowPointSelect = true,
                        DataLabels = new PlotOptionsColumnDataLabels
                        {
                            Formatter = "function() { return '<b>'+ this.point.name +'</b>: '+ this.y; }"
                        }
                    }
                })

                .SetXAxis(new XAxis
                {
                    Categories = ar1

                })

                .SetSeries(new[]
                {
                    new DotNet.Highcharts.Options.Series {Name="T&P Approval", Data = new Data(ar1.Select(q => (object)q).ToArray()) },
                    new DotNet.Highcharts.Options.Series { Name="T&P Reject", Data = new Data(ar2.Select(q => (object)q).ToArray()) },
                    //new DotNet.Highcharts.Options.Series { Name="Female", Data = new Data(ar4.Select(q => (object)q).ToArray()) },
                   
                }

                );
                        Literal1.Text = chart.ToHtmlString();


                        DataSet ds2 = objUACC.GetallDashboard(val, sessionno);
                        if (ds2 != null && ds2.Tables.Count > 0)
                        {
                            if (ds2.Tables[0].Rows.Count > 0)
                            {

                                DataTable dt2 = new System.Data.DataTable();
                                dt2 = ds2.Tables[0];

                                var list = new List<object[]>();
                                foreach (DataRow dr in dt2.Rows)
                                {
                                    list.Add(new object[]{ 
                                    dr[0],dr[1]
                                    });
                                }

                                DotNet.Highcharts.Highcharts chart133 = new DotNet.Highcharts.Highcharts("chart133")
                                .InitChart(new DotNet.Highcharts.Options.Chart
                                {
                                    Height = 380,
                                    Options3d = new ChartOptions3d
                                    {
                                        Enabled = true,
                                        Alpha = 45,
                                        Beta = 0

                                    }

                                })

                                .SetTooltip(new Tooltip { Formatter = "function() { return '<b>'+ this.point.name +'</b>: '+ this.y ; }" })
                                 .SetPlotOptions(new PlotOptions
                                 {

                                     Pie = new PlotOptionsPie
                                     {
                                         AllowPointSelect = true,
                                         Depth = 35,

                                         DataLabels = new PlotOptionsPieDataLabels
                                         {
                                             Formatter = "function() { return '<b>'+ this.point.name +'</b>: '+ this.y; }"
                                         }
                                     }
                                 })

                                 .SetTitle(new DotNet.Highcharts.Options.Title { Text = "" })
                                 .SetSeries(new DotNet.Highcharts.Options.Series


                                 {
                                     Type = ChartTypes.Pie,
                                     Name = "Browser share",




                                     Data = new Data(list.Select(b => new { Name = (object)b.GetValue(0), y = (object)b.GetValue(1) }).ToArray())
                                 });

                                Literal2.Text = chart133.ToHtmlString();
                            }


                        }


                        ApproveandReject.Visible = true;
                    }



                    if (val == 62) //67
                    {
                        DataTable dt = new System.Data.DataTable();
                        DataSet ds1 = objUACC.GetTpAppliedRejectOnTabulerform(val, sessionno);
                        Session["DrivePlacedStatus"] = ds1;
                        dt = ds1.Tables[0];

                        string[] ar1 = new string[dt.Rows.Count];
                        string[] ar2 = new string[dt.Rows.Count];
                        //string[] ar3 = new string[dt.Rows.Count];
                        //string[] ar4 = new string[dt.Rows.Count];
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            ar1[j] = dt.Rows[j]["DRIVE_APPLIED_STUDENT"].ToString();
                            ar2[j] = dt.Rows[j]["PLACED_STUDENT"].ToString();
                            //ar3[j] = dt.Rows[j]["MALE"].ToString();
                            //ar4[j] = dt.Rows[j]["FEMALE"].ToString();
                        }
                        DotNet.Highcharts.Highcharts chart = new DotNet.Highcharts.Highcharts("chart467")
                       .InitChart(new DotNet.Highcharts.Options.Chart { DefaultSeriesType = ChartTypes.Column, Height = 380 })
                        .SetTooltip(new Tooltip { Formatter = "function() { return '<b>'+ this.x +'</b>: '+ this.y ; }" })
                .SetTitle(new DotNet.Highcharts.Options.Title { Text = "" })
                .SetLegend(new DotNet.Highcharts.Options.Legend { Enabled = true })
                .SetPlotOptions(new PlotOptions
                {

                    Column = new PlotOptionsColumn
                    {

                        AllowPointSelect = true,
                        DataLabels = new PlotOptionsColumnDataLabels
                        {
                            Formatter = "function() { return '<b>'+ this.point.name +'</b>: '+ this.y; }"
                        }
                    }
                })

                .SetXAxis(new XAxis
                {
                    Categories = ar1

                })

                .SetSeries(new[]
                {
                    new DotNet.Highcharts.Options.Series {Name="T&P Drive Applied", Data = new Data(ar1.Select(q => (object)q).ToArray()) },
                    new DotNet.Highcharts.Options.Series {Name="T&P Placed Student", Data = new Data(ar2.Select(q => (object)q).ToArray()) },
                    //new DotNet.Highcharts.Options.Series { Name="Female", Data = new Data(ar4.Select(q => (object)q).ToArray()) },
                   
                }

                );
                        Literal3.Text = chart.ToHtmlString();

                        DataSet ds2 = objUACC.GetallDashboard(val, sessionno);
                        if (ds2 != null && ds2.Tables.Count > 0)
                        {
                            if (ds2.Tables[0].Rows.Count > 0)
                            {

                                DataTable dt2 = new System.Data.DataTable();
                                dt2 = ds2.Tables[0];

                                var list = new List<object[]>();
                                foreach (DataRow dr in dt2.Rows)
                                {
                                    list.Add(new object[]{ 
                                    dr[0],dr[1]
                                    });
                                }

                                DotNet.Highcharts.Highcharts chart134 = new DotNet.Highcharts.Highcharts("chart134")
                                .InitChart(new DotNet.Highcharts.Options.Chart
                                {
                                    Height = 380,
                                    Options3d = new ChartOptions3d
                                    {
                                        Enabled = true,
                                        Alpha = 45,
                                        Beta = 0

                                    }

                                })

                                .SetTooltip(new Tooltip { Formatter = "function() { return '<b>'+ this.point.name +'</b>: '+ this.y ; }" })
                                 .SetPlotOptions(new PlotOptions
                                 {

                                     Pie = new PlotOptionsPie
                                     {
                                         AllowPointSelect = true,
                                         Depth = 35,

                                         DataLabels = new PlotOptionsPieDataLabels
                                         {
                                             Formatter = "function() { return '<b>'+ this.point.name +'</b>: '+ this.y; }"
                                         }
                                     }
                                 })

                                 .SetTitle(new DotNet.Highcharts.Options.Title { Text = "" })
                                 .SetSeries(new DotNet.Highcharts.Options.Series


                                 {
                                     Type = ChartTypes.Pie,
                                     Name = "Browser share",




                                     Data = new Data(list.Select(b => new { Name = (object)b.GetValue(0), y = (object)b.GetValue(1) }).ToArray())
                                 });

                                Literal4.Text = chart134.ToHtmlString();
                            }


                        }


                        DriveappliedandPlaced.Visible = true;
                    }

                    if (val == 59)  //68
                    {
                        DataTable dt = new System.Data.DataTable();
                        DataSet ds1 = objUACC.GetallDashboard(val, sessionno);
                        Session["SelectedStudent"] = ds1;
                        dt = ds1.Tables[0];

                        string[] ar1 = new string[dt.Rows.Count];
                        string[] ar2 = new string[dt.Rows.Count];
                        //string[] ar3 = new string[dt.Rows.Count];
                        //string[] ar4 = new string[dt.Rows.Count];
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            ar1[j] = dt.Rows[j]["COMPANY_NAME"].ToString();
                            ar2[j] = dt.Rows[j]["SELECTED_STUDENT"].ToString();
                            //ar3[j] = dt.Rows[j]["MALE"].ToString();
                            //ar4[j] = dt.Rows[j]["FEMALE"].ToString();
                        }
                        DotNet.Highcharts.Highcharts chart = new DotNet.Highcharts.Highcharts("chart524")
                       .InitChart(new DotNet.Highcharts.Options.Chart { DefaultSeriesType = ChartTypes.Column, Height = 380 })
                        .SetTooltip(new Tooltip { Formatter = "function() { return '<b>'+ this.x +'</b>: '+ this.y ; }" })
                .SetTitle(new DotNet.Highcharts.Options.Title { Text = "" })
                .SetLegend(new DotNet.Highcharts.Options.Legend { Enabled = true })
                .SetPlotOptions(new PlotOptions
                {

                    Column = new PlotOptionsColumn
                    {

                        AllowPointSelect = true,
                        DataLabels = new PlotOptionsColumnDataLabels
                        {
                            Formatter = "function() { return '<b>'+ this.point.name +'</b>: '+ this.y; }"
                        }
                    }
                })

                .SetXAxis(new XAxis
                {
                    Categories = ar1

                })

                .SetSeries(new[]
                {
                   // new DotNet.Highcharts.Options.Series {Name="Company Name", Data = new Data(ar1.Select(q => (object)q).ToArray()) },
                    new DotNet.Highcharts.Options.Series { Name="Selected Student", Data = new Data(ar2.Select(q => (object)q).ToArray()) },
                    //new DotNet.Highcharts.Options.Series { Name="Female", Data = new Data(ar4.Select(q => (object)q).ToArray()) },
                   
                }

                );
                        Literal5.Text = chart.ToHtmlString();


                        //DataSet ds2 = objUACC.GetallDashboard(val, sessionno);
                        //if (ds2 != null && ds2.Tables.Count > 0)
                        //{
                        if (ds1.Tables[0].Rows.Count > 0)
                        {

                            DataTable dt2 = new System.Data.DataTable();
                            dt2 = ds1.Tables[0];

                            var list = new List<object[]>();
                            foreach (DataRow dr in dt2.Rows)
                            {
                                list.Add(new object[]{ 
                                    dr[0],dr[1]
                                    });
                            }

                            DotNet.Highcharts.Highcharts chart134 = new DotNet.Highcharts.Highcharts("chart135")
                            .InitChart(new DotNet.Highcharts.Options.Chart
                            {
                                Height = 380,
                                Options3d = new ChartOptions3d
                                {
                                    Enabled = true,
                                    Alpha = 45,
                                    Beta = 0

                                }

                            })

                            .SetTooltip(new Tooltip { Formatter = "function() { return '<b>'+ this.point.name +'</b>: '+ this.y ; }" })
                             .SetPlotOptions(new PlotOptions
                             {

                                 Pie = new PlotOptionsPie
                                 {
                                     AllowPointSelect = true,
                                     Depth = 35,

                                     DataLabels = new PlotOptionsPieDataLabels
                                     {
                                         Formatter = "function() { return '<b>'+ this.point.name +'</b>: '+ this.y; }"
                                     }
                                 }
                             })

                             .SetTitle(new DotNet.Highcharts.Options.Title { Text = "" })
                             .SetSeries(new DotNet.Highcharts.Options.Series


                             {
                                 Type = ChartTypes.Pie,
                                 Name = "Browser share",




                                 Data = new Data(list.Select(b => new { Name = (object)b.GetValue(0), y = (object)b.GetValue(1) }).ToArray())
                             });

                            Literal6.Text = chart134.ToHtmlString();
                        }


                        // }


                        StudentPlacedCompanyWise.Visible = true;
                    }


                    if (val == 60) //69
                    {
                        DataTable dt = new System.Data.DataTable();
                        DataSet ds1 = objUACC.GetallDashboard(val, sessionno);
                        Session["BranchwiseCompany"] = ds1;
                        dt = ds1.Tables[0];

                        string[] ar1 = new string[dt.Rows.Count];
                        string[] ar2 = new string[dt.Rows.Count];
                        //string[] ar3 = new string[dt.Rows.Count];
                        //string[] ar4 = new string[dt.Rows.Count];
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            ar1[j] = dt.Rows[j]["BRANCH_NAME"].ToString();
                            ar2[j] = dt.Rows[j]["NUMBER_OF_COMPANY"].ToString();
                            //ar3[j] = dt.Rows[j]["MALE"].ToString();
                            //ar4[j] = dt.Rows[j]["FEMALE"].ToString();
                        }
                        DotNet.Highcharts.Highcharts chart = new DotNet.Highcharts.Highcharts("chart525")
                       .InitChart(new DotNet.Highcharts.Options.Chart { DefaultSeriesType = ChartTypes.Column, Height = 380 })
                        .SetTooltip(new Tooltip { Formatter = "function() { return '<b>'+ this.x +'</b>: '+ this.y ; }" })
                .SetTitle(new DotNet.Highcharts.Options.Title { Text = "" })
                .SetLegend(new DotNet.Highcharts.Options.Legend { Enabled = true })
                .SetPlotOptions(new PlotOptions
                {

                    Column = new PlotOptionsColumn
                    {

                        AllowPointSelect = true,
                        DataLabels = new PlotOptionsColumnDataLabels
                        {
                            Formatter = "function() { return '<b>'+ this.point.name +'</b>: '+ this.y; }"
                        }
                    }
                })

                .SetXAxis(new XAxis
                {
                    Categories = ar1

                })

                .SetSeries(new[]
                {
                   // new DotNet.Highcharts.Options.Series {Name="Branch", Data = new Data(ar1.Select(q => (object)q).ToArray()) },
                    new DotNet.Highcharts.Options.Series { Name="Number of Company", Data = new Data(ar2.Select(q => (object)q).ToArray()) },
                    //new DotNet.Highcharts.Options.Series { Name="Female", Data = new Data(ar4.Select(q => (object)q).ToArray()) },
                   
                }

                );
                        Literal7.Text = chart.ToHtmlString();


                        //DataSet ds2 = objUACC.GetallDashboard(val, sessionno);
                        //if (ds2 != null && ds2.Tables.Count > 0)
                        //{
                        if (ds1.Tables[0].Rows.Count > 0)
                        {

                            DataTable dt2 = new System.Data.DataTable();
                            dt2 = ds1.Tables[0];

                            var list = new List<object[]>();
                            foreach (DataRow dr in dt2.Rows)
                            {
                                list.Add(new object[]{ 
                                    dr[0],dr[1]
                                    });
                            }

                            DotNet.Highcharts.Highcharts chart135 = new DotNet.Highcharts.Highcharts("chart136")
                            .InitChart(new DotNet.Highcharts.Options.Chart
                            {
                                Height = 380,
                                Options3d = new ChartOptions3d
                                {
                                    Enabled = true,
                                    Alpha = 45,
                                    Beta = 0

                                }

                            })

                            .SetTooltip(new Tooltip { Formatter = "function() { return '<b>'+ this.point.name +'</b>: '+ this.y ; }" })
                             .SetPlotOptions(new PlotOptions
                             {

                                 Pie = new PlotOptionsPie
                                 {
                                     AllowPointSelect = true,
                                     Depth = 35,

                                     DataLabels = new PlotOptionsPieDataLabels
                                     {
                                         Formatter = "function() { return '<b>'+ this.point.name +'</b>: '+ this.y; }"
                                     }
                                 }
                             })

                             .SetTitle(new DotNet.Highcharts.Options.Title { Text = "" })
                             .SetSeries(new DotNet.Highcharts.Options.Series


                             {
                                 Type = ChartTypes.Pie,
                                 Name = "Browser share",




                                 Data = new Data(list.Select(b => new { Name = (object)b.GetValue(0), y = (object)b.GetValue(1) }).ToArray())
                             });

                            Literal8.Text = chart135.ToHtmlString();
                        }


                        // }


                        Scheduleasperbranchwise.Visible = true;
                    }


                    else if (val == 66) //70
                    {
                        DataSet ds1 = objUACC.GetallDashboard(val, sessionno);
                        if (ds1 != null && ds1.Tables.Count > 0)
                        {
                            if (ds1.Tables[0].Rows.Count > 0)
                            {
                                lblRegiteredStudent.Text = ds1.Tables[0].Rows[0][0].ToString();
                            }
                        }
                        RegisteredStudent.Visible = true;
                    }

                    else if (val == 64) //71
                    {
                        DataSet ds1 = objUACC.GetallDashboard(val, sessionno);
                        if (ds1 != null && ds1.Tables.Count > 0)
                        {
                            if (ds1.Tables[0].Rows.Count > 0)
                            {
                                lblMaleStudent.Text = ds1.Tables[0].Rows[0][0].ToString();
                            }
                        }
                        MaleStudent.Visible = true;
                    }

                    else if (val == 63) //72
                    {
                        DataSet ds1 = objUACC.GetallDashboard(val, sessionno);
                        if (ds1 != null && ds1.Tables.Count > 0)
                        {
                            if (ds1.Tables[0].Rows.Count > 0)
                            {
                                lblFemaleStudent.Text = ds1.Tables[0].Rows[0][0].ToString();
                            }
                        }
                        FemaleStudent.Visible = true;
                    }


                //    if (val == 73)
                //    {
                //        DataTable dt = new System.Data.DataTable();
                //        DataSet ds1 = objUACC.GetTpAppliedRejectOnTabulerform(val, sessionno);
                //        Session["SchedulePlacedunplacedBothstatusUG"] = ds1;
                //        dt = ds1.Tables[0];

                //        string[] ar1 = new string[dt.Rows.Count];
                //        string[] ar2 = new string[dt.Rows.Count];
                //        string[] ar3 = new string[dt.Rows.Count];
                //        //string[] ar4 = new string[dt.Rows.Count];
                //        for (int j = 0; j < dt.Rows.Count; j++)
                //        {
                //            ar1[j] = dt.Rows[j]["PLACED_STUDENT_STATUS"].ToString();
                //            ar2[j] = dt.Rows[j]["UNPLACED_STUDENT_STATUS"].ToString();
                //            ar3[j] = dt.Rows[j]["BOTH_STUDENT_STATUS"].ToString();
                //            //ar4[j] = dt.Rows[j]["FEMALE"].ToString();
                //        }
                //        DotNet.Highcharts.Highcharts chart = new DotNet.Highcharts.Highcharts("chart526")
                //       .InitChart(new DotNet.Highcharts.Options.Chart { DefaultSeriesType = ChartTypes.Column, Height = 380 })
                //        .SetTooltip(new Tooltip { Formatter = "function() { return '<b>'+ this.x +'</b>: '+ this.y ; }" })
                //.SetTitle(new DotNet.Highcharts.Options.Title { Text = "" })
                //.SetLegend(new DotNet.Highcharts.Options.Legend { Enabled = true })
                //.SetPlotOptions(new PlotOptions
                //{

                //    Column = new PlotOptionsColumn
                //    {

                //        AllowPointSelect = true,
                //        DataLabels = new PlotOptionsColumnDataLabels
                //        {
                //            Formatter = "function() { return '<b>'+ this.point.name +'</b>: '+ this.y; }"
                //        }
                //    }
                //})

                //.SetXAxis(new XAxis
                //{
                //    Categories = ar1

                //})

                //.SetSeries(new[]
                //{
                //    new DotNet.Highcharts.Options.Series {Name="Schedule Announced For Placed Status", Data = new Data(ar1.Select(q => (object)q).ToArray()) },
                //    new DotNet.Highcharts.Options.Series { Name="Schedule Announced For UnPlaced Status", Data = new Data(ar2.Select(q => (object)q).ToArray()) },
                //    new DotNet.Highcharts.Options.Series { Name="Schedule Announced For Both Status", Data = new Data(ar3.Select(q => (object)q).ToArray()) },
                   
                //}

                //);
                //        Literal9.Text = chart.ToHtmlString();


                //        DataSet ds2 = objUACC.GetallDashboard(val, sessionno);
                //        if (ds2 != null && ds2.Tables.Count > 0)
                //        {
                //            if (ds2.Tables[0].Rows.Count > 0)
                //            {

                //                DataTable dt2 = new System.Data.DataTable();
                //                dt2 = ds2.Tables[0];

                //                var list = new List<object[]>();
                //                foreach (DataRow dr in dt2.Rows)
                //                {
                //                    list.Add(new object[]{ 
                //                    dr[0],dr[1]
                //                    });
                //                }

                //                DotNet.Highcharts.Highcharts chart136 = new DotNet.Highcharts.Highcharts("chart137")
                //                .InitChart(new DotNet.Highcharts.Options.Chart
                //                {
                //                    Height = 380,
                //                    Options3d = new ChartOptions3d
                //                    {
                //                        Enabled = true,
                //                        Alpha = 45,
                //                        Beta = 0

                //                    }

                //                })

                //                .SetTooltip(new Tooltip { Formatter = "function() { return '<b>'+ this.point.name +'</b>: '+ this.y ; }" })
                //                 .SetPlotOptions(new PlotOptions
                //                 {

                //                     Pie = new PlotOptionsPie
                //                     {
                //                         AllowPointSelect = true,
                //                         Depth = 35,

                //                         DataLabels = new PlotOptionsPieDataLabels
                //                         {
                //                             Formatter = "function() { return '<b>'+ this.point.name +'</b>: '+ this.y; }"
                //                         }
                //                     }
                //                 })

                //                 .SetTitle(new DotNet.Highcharts.Options.Title { Text = "" })
                //                 .SetSeries(new DotNet.Highcharts.Options.Series


                //                 {
                //                     Type = ChartTypes.Pie,
                //                     Name = "Browser share",




                //                     Data = new Data(list.Select(b => new { Name = (object)b.GetValue(0), y = (object)b.GetValue(1) }).ToArray())
                //                 });

                //                Literal10.Text = chart136.ToHtmlString();
                //            }


                //        }


                //        SchedulePlacedunplacedbothstatusforUG.Visible = true;
                //    }

                //    if (val == 74)
                //    {
                //        DataTable dt = new System.Data.DataTable();
                //        DataSet ds1 = objUACC.GetTpAppliedRejectOnTabulerform(val, sessionno);
                //        Session["SchedulePlacedunplacedBothstatusPG"] = ds1;
                //        dt = ds1.Tables[0];

                //        string[] ar1 = new string[dt.Rows.Count];
                //        string[] ar2 = new string[dt.Rows.Count];
                //        string[] ar3 = new string[dt.Rows.Count];
                //        //string[] ar4 = new string[dt.Rows.Count];
                //        for (int j = 0; j < dt.Rows.Count; j++)
                //        {
                //            ar1[j] = dt.Rows[j]["PLACED_STUDENT_STATUS"].ToString();
                //            ar2[j] = dt.Rows[j]["UNPLACED_STUDENT_STATUS"].ToString();
                //            ar3[j] = dt.Rows[j]["BOTH_STUDENT_STATUS"].ToString();
                //            //ar4[j] = dt.Rows[j]["FEMALE"].ToString();
                //        }
                //        DotNet.Highcharts.Highcharts chart = new DotNet.Highcharts.Highcharts("chart527")
                //       .InitChart(new DotNet.Highcharts.Options.Chart { DefaultSeriesType = ChartTypes.Column, Height = 380 })
                //        .SetTooltip(new Tooltip { Formatter = "function() { return '<b>'+ this.x +'</b>: '+ this.y ; }" })
                //.SetTitle(new DotNet.Highcharts.Options.Title { Text = "" })
                //.SetLegend(new DotNet.Highcharts.Options.Legend { Enabled = true })
                //.SetPlotOptions(new PlotOptions
                //{

                //    Column = new PlotOptionsColumn
                //    {

                //        AllowPointSelect = true,
                //        DataLabels = new PlotOptionsColumnDataLabels
                //        {
                //            Formatter = "function() { return '<b>'+ this.point.name +'</b>: '+ this.y; }"
                //        }
                //    }
                //})

                //.SetXAxis(new XAxis
                //{
                //    Categories = ar1

                //})

                //.SetSeries(new[]
                //{
                //    new DotNet.Highcharts.Options.Series {Name="Schedule Announced For Placed Status", Data = new Data(ar1.Select(q => (object)q).ToArray()) },
                //    new DotNet.Highcharts.Options.Series { Name="Schedule Announced For UnPlaced Status", Data = new Data(ar2.Select(q => (object)q).ToArray()) },
                //    new DotNet.Highcharts.Options.Series { Name="Schedule Announced For Both Status", Data = new Data(ar3.Select(q => (object)q).ToArray()) },
                   
                //}

                //);
                //        Literal11.Text = chart.ToHtmlString();


                //        DataSet ds2 = objUACC.GetallDashboard(val, sessionno);
                //        if (ds2 != null && ds2.Tables.Count > 0)
                //        {
                //            if (ds2.Tables[0].Rows.Count > 0)
                //            {

                //                DataTable dt2 = new System.Data.DataTable();
                //                dt2 = ds2.Tables[0];

                //                var list = new List<object[]>();
                //                foreach (DataRow dr in dt2.Rows)
                //                {
                //                    list.Add(new object[]{ 
                //                    dr[0],dr[1]
                //                    });
                //                }

                //                DotNet.Highcharts.Highcharts chart137 = new DotNet.Highcharts.Highcharts("chart138")
                //                .InitChart(new DotNet.Highcharts.Options.Chart
                //                {
                //                    Height = 380,
                //                    Options3d = new ChartOptions3d
                //                    {
                //                        Enabled = true,
                //                        Alpha = 45,
                //                        Beta = 0

                //                    }

                //                })

                //                .SetTooltip(new Tooltip { Formatter = "function() { return '<b>'+ this.point.name +'</b>: '+ this.y ; }" })
                //                 .SetPlotOptions(new PlotOptions
                //                 {

                //                     Pie = new PlotOptionsPie
                //                     {
                //                         AllowPointSelect = true,
                //                         Depth = 35,

                //                         DataLabels = new PlotOptionsPieDataLabels
                //                         {
                //                             Formatter = "function() { return '<b>'+ this.point.name +'</b>: '+ this.y; }"
                //                         }
                //                     }
                //                 })

                //                 .SetTitle(new DotNet.Highcharts.Options.Title { Text = "" })
                //                 .SetSeries(new DotNet.Highcharts.Options.Series


                //                 {
                //                     Type = ChartTypes.Pie,
                //                     Name = "Browser share",




                //                     Data = new Data(list.Select(b => new { Name = (object)b.GetValue(0), y = (object)b.GetValue(1) }).ToArray())
                //                 });

                //                Literal12.Text = chart137.ToHtmlString();
                //            }


                //        }


                //        SchedulePlacedunplacedbothstatusforPG.Visible = true;
                //    }

                    if (val == 61) //73
                    {
                        DataTable dt = new System.Data.DataTable();
                        DataSet ds1 = objUACC.GetTpAppliedRejectOnTabulerform(val, sessionno);
                        Session["CommpanyActiveinactivestatus"] = ds1;
                        dt = ds1.Tables[0];

                        string[] ar1 = new string[dt.Rows.Count];
                        string[] ar2 = new string[dt.Rows.Count];
                        string[] ar3 = new string[dt.Rows.Count];
                        //string[] ar4 = new string[dt.Rows.Count];
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            ar1[j] = dt.Rows[j]["TOTAL_COMPANY"].ToString();
                            ar2[j] = dt.Rows[j]["ACTIVE_COMPANY"].ToString();
                            ar3[j] = dt.Rows[j]["INACTIVE_COMPANY"].ToString();
                            //ar4[j] = dt.Rows[j]["FEMALE"].ToString();
                        }
                        DotNet.Highcharts.Highcharts chart = new DotNet.Highcharts.Highcharts("chart528")
                       .InitChart(new DotNet.Highcharts.Options.Chart { DefaultSeriesType = ChartTypes.Column, Height = 380 })
                        .SetTooltip(new Tooltip { Formatter = "function() { return '<b>'+ this.x +'</b>: '+ this.y ; }" })
                .SetTitle(new DotNet.Highcharts.Options.Title { Text = "" })
                .SetLegend(new DotNet.Highcharts.Options.Legend { Enabled = true })
                .SetPlotOptions(new PlotOptions
                {

                    Column = new PlotOptionsColumn
                    {

                        AllowPointSelect = true,
                        DataLabels = new PlotOptionsColumnDataLabels
                        {
                            Formatter = "function() { return '<b>'+ this.point.name +'</b>: '+ this.y; }"
                        }
                    }
                })

                .SetXAxis(new XAxis
                {
                    Categories = ar1

                })

                .SetSeries(new[]
                {
                    new DotNet.Highcharts.Options.Series {Name="Total Company", Data = new Data(ar1.Select(q => (object)q).ToArray()) },
                    new DotNet.Highcharts.Options.Series { Name="Approved Company", Data = new Data(ar2.Select(q => (object)q).ToArray()) },
                    new DotNet.Highcharts.Options.Series { Name="Pending Company", Data = new Data(ar3.Select(q => (object)q).ToArray()) },
                   
                }

                );
                        Literal13.Text = chart.ToHtmlString();


                        DataSet ds2 = objUACC.GetallDashboard(val, sessionno);
                        if (ds2 != null && ds2.Tables.Count > 0)
                        {
                            if (ds1.Tables[0].Rows.Count > 0)
                            {

                                DataTable dt2 = new System.Data.DataTable();
                                dt2 = ds2.Tables[0];

                                var list = new List<object[]>();
                                foreach (DataRow dr in dt2.Rows)
                                {
                                    list.Add(new object[]{ 
                                    dr[0],dr[1]
                                    });
                                }

                                DotNet.Highcharts.Highcharts chart138 = new DotNet.Highcharts.Highcharts("chart139")
                                .InitChart(new DotNet.Highcharts.Options.Chart
                                {
                                    Height = 380,
                                    Options3d = new ChartOptions3d
                                    {
                                        Enabled = true,
                                        Alpha = 45,
                                        Beta = 0

                                    }

                                })

                                .SetTooltip(new Tooltip { Formatter = "function() { return '<b>'+ this.point.name +'</b>: '+ this.y ; }" })
                                 .SetPlotOptions(new PlotOptions
                                 {

                                     Pie = new PlotOptionsPie
                                     {
                                         AllowPointSelect = true,
                                         Depth = 35,

                                         DataLabels = new PlotOptionsPieDataLabels
                                         {
                                             Formatter = "function() { return '<b>'+ this.point.name +'</b>: '+ this.y; }"
                                         }
                                     }
                                 })

                                 .SetTitle(new DotNet.Highcharts.Options.Title { Text = "" })
                                 .SetSeries(new DotNet.Highcharts.Options.Series


                                 {
                                     Type = ChartTypes.Pie,
                                     Name = "Browser share",




                                     Data = new Data(list.Select(b => new { Name = (object)b.GetValue(0), y = (object)b.GetValue(1) }).ToArray())
                                 });

                                Literal14.Text = chart138.ToHtmlString();
                            }


                        }


                        Companywiseactiveinactivestatus.Visible = true;
                    }

                }
            }
        }
        else
        {

            /// Check if postback is caused by btnSearch then call search method.
            /// 
            if (Session["username"].ToString() == "MastersoftAccount")
            {
                if (Request.Params["__EVENTTARGET"] != null && Request.Params["__EVENTTARGET"].ToString() != string.Empty)
                {
                    if (Request.Params["__EVENTTARGET"].ToString() == "loadReport")
                    {
                        try
                        {
                            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("dashboardnew")));
                            url += "Reports/CommonReport.aspx?";
                            url += "pagetitle=" + "userreport";
                            url += "&path=~,Reports,Academic," + "rptuserdetails.rpt";
                            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@UserName=" + Session["username"].ToString();

                            divScript.InnerHtml += " <script type='text/javascript' language='javascript'> try{";
                            divScript.InnerHtml += " window.open('" + url + "','" + "rptuserdetails.rpt" + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
                            //To open new window from Updatepanel
                            System.Text.StringBuilder sb = new System.Text.StringBuilder();
                            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
                            sb.Append(@"window.open('" + url + "','','" + features + "');");

                            // ScriptManager.RegisterClientScriptBlock(this.updpayrule, this.updpayrule.GetType(), "controlJSScript", sb.ToString(), true);
                            divScript.InnerHtml += " }catch(e){ alert('Error: ' + e.description); } ";
                            divScript.InnerHtml += " window.close();";
                            divScript.InnerHtml += " </script>";
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }
            }

        }
    }
    protected void btnregistrationcount_Click(object sender, ImageClickEventArgs e)
    {
        DataSet ds = new DataSet();
        ds = (DataSet)Session["ApproveRejectStatus"];
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            ExportExcel(ds, "Approved_Reject_Student_Count");
        }
    }
    protected void btnTpStudentdetails_Click(object sender, ImageClickEventArgs e)
    {
        DataSet ds = new DataSet();
        ds = (DataSet)Session["DrivePlacedStatus"];
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            ExportExcel(ds, "Drive_Placed_Student_Count");
        }
    }
    protected void btnStudentPlacedCompanyWise_Click(object sender, ImageClickEventArgs e)
    {
        DataSet ds = new DataSet();
        ds = (DataSet)Session["SelectedStudent"];
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            ExportExcel(ds, "Selected_Student_Company_wise");
        }
    }
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        DataSet ds = new DataSet();
        ds = (DataSet)Session["BranchwiseCompany"];
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            ExportExcel(ds, "Branch_Wise_Company_Count");
        }
    }
    protected void btnCompanystatus_Click(object sender, ImageClickEventArgs e)
    {
        DataSet ds = new DataSet();
        ds = (DataSet)Session["CommpanyActiveinactivestatus"];
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            ExportExcel(ds, "Company_Active/Inactive_Status_Count");
        }
    }

    private void ExportExcel(DataSet ds, string filename)
    {
        //ADDED BY: M. REHBAR SHEIKH 
        string attachment = "attachment; filename=" + filename + ".xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/" + "ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);

        DataSet dsfee = ds;
        DataGrid dg = new DataGrid();
        if (dsfee.Tables.Count > 0)
        {
            dg.DataSource = dsfee.Tables[0];
            dg.DataBind();
        }
        dg.HeaderStyle.Font.Bold = true;
        dg.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();
    }
}