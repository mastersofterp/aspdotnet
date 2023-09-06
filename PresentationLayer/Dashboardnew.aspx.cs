//=================================================================================
// PROJECT NAME  : U-AIMS                                                          
// MODULE NAME   : TO CREATE HOME PAGE                                             
// CREATION DATE : 13-April-2009
// CREATED BY    : NIRAJ D. PHALKE & ASHWINI BARBATE                               
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
using System.Collections.Generic;
using System.Linq;
using System.Web;

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


public partial class Dashboardnew : System.Web.UI.Page
{
    Common objCommon = new Common();
    User_AccController objUACC = new User_AccController();

    public string sMarquee = string.Empty;
    public string Notice = string.Empty;
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
            string msg = objCommon.LookUp("REFF", "POPUP_MSG", "POPUP_FLAG=1");
            if (msg != "")
            {
                pmarq.InnerText = msg;
                divmarquee.Visible = true;
                lblpopup.Text = msg;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Hello", "functionConfirm()", true);
            }
            if (Session["username"].ToString() == "MastersoftAccount")
            {
                lblreport.Text = "(Click to view the report)";
                lblreport.Visible = true;
            }
            objCommon.FillDropDownList(ddlbranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "", "BRANCHNO");
            objCommon.FillDropDownList(ddlbranchfees, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "", "BRANCHNO");

            if (Session["usertype"].ToString() == "3")
            {
                // trNews.Visible = false;
                //trTimeTable.Visible = true;
                // this.sessionname();
                CourseTeacherAllotController objCT = new CourseTeacherAllotController();
                //   int sessionNo = Convert.ToInt16(objCommon.LookUp("REFF", "ATT_SESSIONNO", string.Empty));
                //DataSet dsTimeTable = objCT.DisplayTimeTableFaculty(sessionNo, 0, 0, Convert.ToInt32(Session["userno"]));
                //lvTimeTable.DataSource = dsTimeTable.Tables[0];
                //lvTimeTable.DataBind();
                // (lvTimeTable.FindControl("divTitle") as HtmlGenericControl).InnerHtml = "Time Table for " + dsTimeTable.Tables[0].Rows[0]["SESSIONNAME"].ToString();
            }
            else
            {
                //Show scrolling news
                // this.sessionname();
                //  this.Branchresult();
                NewsController objNC = new NewsController();
                sMarquee = objNC.ScrollingNews(Request.ApplicationPath);
                Notice = objNC.NoticeBoard(Request.ApplicationPath);
                //trNews.Visible = true;
                //trTimeTable.Visible = false;
            }

            txtEmpAttendanceDate.Text = DateTime.Now.ToString();
           // DataSet ds = objCommon.FillDropDown("ACD_DASHBOARD_CONFIGURATION", "USERTYPE", "DASHBOARD", "USERTYPE=" + Session["usertype"].ToString(), "USERTYPE");
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

                    if (val == 7)
                    {
                        DataSet ds1 = objUACC.GetallDashboard(val, sessionno);
                        if (ds1 != null && ds1.Tables.Count > 0)
                        {
                            if (ds1.Tables[0].Rows.Count > 0)
                            {
                                lblPGTotalFemale.Text = ds1.Tables[0].Rows[0][0].ToString();
                            }
                        }
                        pgfemale.Visible = true;
                    }
                    else if (val == 8)
                    {
                        DataSet ds1 = objUACC.GetallDashboard(val, sessionno);
                        if (ds1 != null && ds1.Tables.Count > 0)
                        {
                            if (ds1.Tables[0].Rows.Count > 0)
                            {
                                lblPGStudentVist.Text = ds1.Tables[0].Rows[0][0].ToString();
                            }
                        }
                        nopaidpg.Visible = true;
                    }
                    else if (val == 9)
                    {
                        DataSet ds1 = objUACC.GetallDashboard(val, sessionno);
                        if (ds1 != null && ds1.Tables.Count > 0)
                        {
                            if (ds1.Tables[0].Rows.Count > 0)
                            {
                                lblUGTotalStudent.Text = ds1.Tables[0].Rows[0][0].ToString();
                            }
                        }
                        totalregisteredug.Visible = true;
                    }
                    else if (val == 10)
                    {
                        DataSet ds1 = objUACC.GetallDashboard(val, sessionno);
                        if (ds1 != null && ds1.Tables.Count > 0)
                        {
                            if (ds1.Tables[0].Rows.Count > 0)
                            {
                                lblUGTotalMale.Text = ds1.Tables[0].Rows[0][0].ToString();
                            }
                        }
                        ugmale.Visible = true;
                    }
                    else if (val == 52)
                    {
                        DataTable dt = new System.Data.DataTable();
                        DataSet ds1 = objUACC.GetallDashboard(val, sessionno);
                        Session["branchwise"] = ds1;
                        dt = ds1.Tables[0];
                  
                        string[] ar1 = new string[dt.Rows.Count];
                        string[] ar2 = new string[dt.Rows.Count];

                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            ar1[j] = dt.Rows[j]["LONGNAME"].ToString();
                            ar2[j] = dt.Rows[j]["TOTAL_STUDENT"].ToString();

                        }
                        DotNet.Highcharts.Highcharts chart = new DotNet.Highcharts.Highcharts("chart223")
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
                    new DotNet.Highcharts.Options.Series {Name="Total", Data = new Data(ar1.Select(q => (object)q).ToArray()) },
                    new DotNet.Highcharts.Options.Series { Name="LongName", Data = new Data(ar2.Select(q => (object)q).ToArray()) },
                   
                   
                }

                );
                        Literal24.Text = chart.ToHtmlString();


                        if (ds1 != null && ds1.Tables.Count > 0)
                        {
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

                                DotNet.Highcharts.Highcharts chart132 = new DotNet.Highcharts.Highcharts("chart132")
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

                                Literal25.Text = chart132.ToHtmlString();
                            }


                        }
                        divbranchcount.Visible = true;
                    }
                    else if (val == 51)
                    {
                        DataTable dt = new System.Data.DataTable();
                        DataSet ds1 = objUACC.GetallDashboard(val, sessionno);
                        Session["categorywise"] = ds1;
                        dt = ds1.Tables[0];

                        string[] ar1 = new string[dt.Rows.Count];
                        string[] ar2 = new string[dt.Rows.Count];

                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            ar1[j] = dt.Rows[j]["CATEGORY"].ToString();
                            ar2[j] = dt.Rows[j]["TOTAL_STUDENT"].ToString();

                        }
                        DotNet.Highcharts.Highcharts chart = new DotNet.Highcharts.Highcharts("chart224")
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
                    new DotNet.Highcharts.Options.Series {Name="Total", Data = new Data(ar1.Select(q => (object)q).ToArray()) },
                    new DotNet.Highcharts.Options.Series { Name="Category", Data = new Data(ar2.Select(q => (object)q).ToArray()) },
                   
                   
                }

                );
                        Literal26.Text = chart.ToHtmlString();


                        if (ds1 != null && ds1.Tables.Count > 0)
                        {
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

                                Literal27.Text = chart133.ToHtmlString();
                            }


                        }
                        divcategory.Visible = true;
                    }
                    else if (val == 53)
                    {
                        int Sessionno = Convert.ToInt32(objCommon.LookUp("ACD_DASHBOARD_MASTER", "SESSIONNO", "ID=53"));
                        DataTable dt = new System.Data.DataTable();
                        DataSet ds1 = objUACC.GetallDashboard(val, Sessionno);
                        Session["bankwise"] = ds1;
                        dt = ds1.Tables[0];

                        string[] ar1 = new string[dt.Rows.Count];
                        string[] ar2 = new string[dt.Rows.Count];
                        string[] ar3 = new string[dt.Rows.Count];
                        string[] ar4 = new string[dt.Rows.Count];
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            ar1[j] = dt.Rows[j]["RECIEPT_DATE"].ToString();
                            ar2[j] = dt.Rows[j]["DD_AMOUNT"].ToString();
                            ar3[j] = dt.Rows[j]["Punjab_National_Bank"].ToString();
                            ar4[j] = dt.Rows[j]["Other_Bank"].ToString();
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
                    new DotNet.Highcharts.Options.Series {Name="Total", Data = new Data(ar2.Select(q => (object)q).ToArray()) },
                    new DotNet.Highcharts.Options.Series { Name="Punjab National Bank", Data = new Data(ar3.Select(q => (object)q).ToArray()) },
                    new DotNet.Highcharts.Options.Series { Name="Other Bank", Data = new Data(ar4.Select(q => (object)q).ToArray()) },
                   
                }

                );
                        Literal28.Text = chart.ToHtmlString();

                        dt = ds1.Tables[0];
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            ar1[j] = dt.Rows[j]["RECIEPT_DATE"].ToString();
                            ar2[j] = dt.Rows[j]["DD_AMOUNT"].ToString();
                            ar3[j] = dt.Rows[j]["Punjab_National_Bank"].ToString();
                            ar4[j] = dt.Rows[j]["Other_Bank"].ToString();
                        }
                        DotNet.Highcharts.Highcharts chart523 = new DotNet.Highcharts.Highcharts("chart523")
                       .InitChart(new DotNet.Highcharts.Options.Chart { DefaultSeriesType = ChartTypes.Line, Height = 380 })
                        .SetTooltip(new Tooltip { Formatter = "function() { return '<b>'+ this.x +'</b>: '+ this.y ; }" })
                .SetTitle(new DotNet.Highcharts.Options.Title { Text = "" })
                .SetLegend(new DotNet.Highcharts.Options.Legend { Enabled = true })
                .SetPlotOptions(new PlotOptions
                {

                    Line = new PlotOptionsLine
                    {

                        AllowPointSelect = true,
                        DataLabels = new PlotOptionsLineDataLabels
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
                    new DotNet.Highcharts.Options.Series {Name="Total", Data = new Data(ar2.Select(q => (object)q).ToArray()) },
                    new DotNet.Highcharts.Options.Series { Name="Punjab National Bank", Data = new Data(ar3.Select(q => (object)q).ToArray()) },
                    new DotNet.Highcharts.Options.Series { Name="Other Bank", Data = new Data(ar4.Select(q => (object)q).ToArray()) },
                   
                }

                );
                        Literal29.Text = chart523.ToHtmlString();


                        divBankwisedd.Visible = true;
                    }

                    else if (val == 13)
                    {
                        DataSet ds1 = objUACC.GetallDashboard(val, sessionno);
                        Session["statewise"] = ds1;
                        if (ds1 != null && ds1.Tables.Count > 0)
                        {
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

                                DotNet.Highcharts.Highcharts chart = new DotNet.Highcharts.Highcharts("chart1")
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

                                ltrChart1.Text = chart.ToHtmlString();



                                StringBuilder str = new StringBuilder();
                                DataTable dt = new DataTable();

                                try
                                {

                                    dt = ds1.Tables[0];

                                    str.Append(@" <script type='text/javascript'>

               google.load('visualization', '1', {'packages': ['geomap']});

               google.setOnLoadCallback(drawRegionsMap);
      
               function drawRegionsMap() {

               var data = google.visualization.arrayToDataTable([

               ['City', 'Count'],");

                                    int count = dt.Rows.Count - 1;

                                    for (int i = 0; i <= count; i++)
                                    {
                                        str.Append("['" + dt.Rows[i]["STATENAME"].ToString() + "',  " + dt.Rows[i]["STUDENTS"].ToString() + "],");

                                        if (i == count)
                                        {
                                            str.Append("['" + dt.Rows[i]["STATENAME"].ToString() + "',  " + dt.Rows[i]["STUDENTS"].ToString() + "]]);");
                                        }
                                    }


                                    str.Append(" var options = {region:'IN',dataMode:'regions',colors: ['pink', 'blue', 'orange','yellow','brown','purple','green','red'],resolution:'provinces',width:500,height:350}; var chart = new google.visualization.GeoChart(document.getElementById('Div_chartug'));");

                                    str.Append("chart.draw(data, options); };");

                                    str.Append("</script>");

                                    Literal1.Text = str.ToString();

                                }

                                catch
                                {

                                }
                                ugstatewise.Visible = true;
                            }
                        }



                    }


                    else if (val == 29)
                    {
                        DataTable dt = new System.Data.DataTable();
                        DataSet ds1 = objUACC.GetallDashboard(val, sessionno);
                        Session["admregistration"] = ds1;
                        dt = ds1.Tables[0];

                        string[] ar1 = new string[dt.Rows.Count];
                        string[] ar2 = new string[dt.Rows.Count];
                        string[] ar3 = new string[dt.Rows.Count];
                        string[] ar4 = new string[dt.Rows.Count];
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            ar1[j] = dt.Rows[j]["ADMISSIONDATE"].ToString();
                            ar2[j] = dt.Rows[j]["TOTALSTUDENT"].ToString();
                            ar3[j] = dt.Rows[j]["MALE"].ToString();
                            ar4[j] = dt.Rows[j]["FEMALE"].ToString();
                        }
                        DotNet.Highcharts.Highcharts chart = new DotNet.Highcharts.Highcharts("chart4")
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
                    new DotNet.Highcharts.Options.Series {Name="Total", Data = new Data(ar2.Select(q => (object)q).ToArray()) },
                    new DotNet.Highcharts.Options.Series { Name="Male", Data = new Data(ar3.Select(q => (object)q).ToArray()) },
                    new DotNet.Highcharts.Options.Series { Name="Female", Data = new Data(ar4.Select(q => (object)q).ToArray()) },
                   
                }

                );
                        Literal2.Text = chart.ToHtmlString();

                        dt = ds1.Tables[0];
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            ar1[j] = dt.Rows[j]["ADMISSIONDATE"].ToString();
                            ar2[j] = dt.Rows[j]["TOTALSTUDENT"].ToString();
                            ar3[j] = dt.Rows[j]["MALE"].ToString();
                            ar4[j] = dt.Rows[j]["FEMALE"].ToString();
                        }
                        DotNet.Highcharts.Highcharts chart1 = new DotNet.Highcharts.Highcharts("chart5")
                       .InitChart(new DotNet.Highcharts.Options.Chart { DefaultSeriesType = ChartTypes.Line, Height = 380 })
                        .SetTooltip(new Tooltip { Formatter = "function() { return '<b>'+ this.x +'</b>: '+ this.y ; }" })
                .SetTitle(new DotNet.Highcharts.Options.Title { Text = "" })
                .SetLegend(new DotNet.Highcharts.Options.Legend { Enabled = true })
                .SetPlotOptions(new PlotOptions
                {

                    Line = new PlotOptionsLine
                    {

                        AllowPointSelect = true,
                        DataLabels = new PlotOptionsLineDataLabels
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
                    new DotNet.Highcharts.Options.Series {Name="Total", Data = new Data(ar2.Select(q => (object)q).ToArray()) },
                    new DotNet.Highcharts.Options.Series { Name="Male", Data = new Data(ar3.Select(q => (object)q).ToArray()) },
                    new DotNet.Highcharts.Options.Series { Name="Female", Data = new Data(ar4.Select(q => (object)q).ToArray()) },
                   
                }

                );
                        Literal3.Text = chart1.ToHtmlString();


                        applicantregistpg.Visible = true;
                    }

                    else if (val == 37)
                    {
                        this.Attendancedetails();
                        this.attendancelinechart();
                        Div2.Visible = true;
                    }
                    else if (val == 38)
                    {

                        Div5.Visible = true;
                        this.Getresult();
                        this.Getresultlinechart();
                    }
                    else if (val == 39)
                    {
                        Div8.Visible = true;
                        this.Admbatch();
                        this.Admbatchbar();
                    }
                    else if (val == 40)
                    {
                        Div14.Visible = true;
                        this.Feecollectionresultline();
                        this.Feecollectionresult();
                    }

                    else if (val == 41)
                    {
                        Div17.Visible = true;
                        this.Feereceipttypebar();
                        this.Receipttypefee();
                    }
                    else if (val == 43)
                    {
                        Div11.Visible = true;
                        this.Bindchart();
                        this.Admbatcbar();
                    }

                    else if (val == 57)
                    {
                        divcommegewiseadmcount.Visible = true;
                        this.Bindchartclgwise();
                        this.Admbatcbarclgwise();
                    }


                    //else if (val == 44)
                    //{
                    //    divmarquee.Visible = true;
                    //}
                    else if (val == 54)
                    {
                        Totaladmcountyear();
                        //Totaladmcountyearline();
                        divgendercontadmbatchwise.Visible = true;
                    }

                    else if (val == 55)
                    {

                        Totalfeeamountyearwise();
                        divfeecollmonthwise.Visible = true;


                    }


                    #region Payroll
                    else if (val == 45)
                    {
                        DataSet ds1 = objUACC.GetAllEmployees(val);
                        if (ds1 != null && ds1.Tables.Count > 0)
                        {
                            if (ds1.Tables[0].Rows.Count > 0)
                            {
                                lblTotalEmployees.Text = ds1.Tables[0].Rows[0][0].ToString();
                            }
                        }
                        paytotalemp.Visible = true;
                    }
                    else if (val == 46)
                    {
                        Div24.Visible = true;
                        this.EmployeeStaffDetails(val);
                        BindEmpoyeeStaffChart(val);
                    }

                    else if (val == 47)
                    {
                        DataSet ds1 = objUACC.GetAllEmployees(val);
                        if (ds1 != null && ds1.Tables.Count > 0)
                        {
                            if (ds1.Tables[0].Rows.Count > 0)
                            {
                                lblTotalEmpFemale.Text = ds1.Tables[0].Rows[0][0].ToString();
                            }
                        }
                        paytotalempFemale.Visible = true;
                    }

                    else if (val == 48)
                    {
                        DataSet ds1 = objUACC.GetAllEmployees(val);
                        if (ds1 != null && ds1.Tables.Count > 0)
                        {
                            if (ds1.Tables[0].Rows.Count > 0)
                            {
                                lblTotalEmpMale.Text = ds1.Tables[0].Rows[0][0].ToString();
                            }
                        }
                        paytotalempMale.Visible = true;
                    }

                    else if (val == 49)
                    {
                        Div25.Visible = true;
                        this.EmployeeJoiningDetails(val);
                        BindEmpoyeeJoiningChart(val);
                    }

                    else if (val == 50)
                    {
                        Div26.Visible = true;
                        this.EmployeeAttendanceDetails(val);
                        BindEmpoyeeAttendanceChart(val);
                    }
                    #endregion
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


    private void Attendancedetails()
    {


        int sessionno = Convert.ToInt32(objCommon.LookUp("ACD_DASHBOARD_MASTER", "SESSIONNO", "ID=37"));
        int idno = Convert.ToInt32(objCommon.LookUp("User_Acc", "isnull(UA_IDNO,0)", "UA_NAME='" + Session["username"].ToString() + "'"));
        DataSet ds2 = objUACC.GetAttendancedetails(idno, sessionno);
        DataTable dt = new DataTable();
        dt = ds2.Tables[0];
        string[] ar1 = new string[dt.Rows.Count];
        string[] ar2 = new string[dt.Rows.Count];

        for (int j = 0; j < dt.Rows.Count; j++)
        {
            ar1[j] = dt.Rows[j][0].ToString();
            ar2[j] = dt.Rows[j][1].ToString();

        }
        DotNet.Highcharts.Highcharts chart = new DotNet.Highcharts.Highcharts("chart16")
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
    Categories = ar2
})
            .SetSeries(new DotNet.Highcharts.Options.Series
            {
                Name = "Total Classes Attended",
                Type = ChartTypes.Column,
                Data = new Data(ar1.Select(q => (object)q).ToArray())

            }


);
        Literal12.Text = chart.ToHtmlString();

    }
    private void Bindchart()
    {
        Random randonGen = new Random();
        DataSet ds = objCommon.FillDropDown("ACD_STUDENT S INNER JOIN ACD_DEGREE D ON (S.DEGREENO=D.DEGREENO)", "D.CODE", "LEFT(ROUND(COUNT(IDNO)* 100.0 / (SELECT COUNT(*) FROM ACD_STUDENT),2),4)   AS PERCENTAGE", "ISNULL(S.ADMCAN, 0) = 0  GROUP BY S.DEGREENO,CODE", "CODE");
        Session["degreewise"] = ds;
        DataTable ChartData = ds.Tables[0];
        var list = new List<object[]>();
        foreach (DataRow dr in ChartData.Rows)
        {
            list.Add(new object[]{ 
                                    dr[0],dr[1]
                                    });
        }

        DotNet.Highcharts.Highcharts chart = new DotNet.Highcharts.Highcharts("chart13")
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
                 Depth = 35,
                 AllowPointSelect = true,
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

        Literal10.Text = chart.ToHtmlString();

    }


    private void Bindchartclgwise()
    {
        Random randonGen = new Random();
        DataSet ds = objCommon.FillDropDown("ACD_STUDENT A INNER JOIN ACD_COLLEGE_MASTER B ON (A.COLLEGE_ID=B.COLLEGE_ID)", "B.COLLEGE_NAME", "COUNT(A.IDNO) AS STUDENTS", "ISNULL(A.ADMCAN, 0) = 0  GROUP BY COLLEGE_NAME", "");
        Session["collegewiseadmcount"] = ds;
        DataTable ChartData = ds.Tables[0];
        var list = new List<object[]>();
        foreach (DataRow dr in ChartData.Rows)
        {
            list.Add(new object[]{ 
                                    dr[0],dr[1]
                                    });
        }

        DotNet.Highcharts.Highcharts chart = new DotNet.Highcharts.Highcharts("chart89")
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
                 Depth = 35,
                 AllowPointSelect = true,
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

        Literal78.Text = chart.ToHtmlString();

    }




    private void attendancelinechart()
    {
        DataTable dt = new System.Data.DataTable();
        int sessionno = Convert.ToInt32(objCommon.LookUp("ACD_DASHBOARD_MASTER", "SESSIONNO", "ID=37 "));
        int idno = Convert.ToInt32(objCommon.LookUp("User_Acc", "isnull(UA_IDNO,0)", "UA_NAME='" + Session["username"].ToString() + "'"));
        DataSet ds2 = objUACC.GetAttendancedetails(idno, sessionno);
        dt = ds2.Tables[0];

        string[] ar1 = new string[dt.Rows.Count];
        string[] ar2 = new string[dt.Rows.Count];

        for (int j = 0; j < dt.Rows.Count; j++)
        {
            ar1[j] = dt.Rows[j][0].ToString();
            ar2[j] = dt.Rows[j][1].ToString();
        }

        DotNet.Highcharts.Highcharts chart = new DotNet.Highcharts.Highcharts("chart17")
       .InitChart(new DotNet.Highcharts.Options.Chart { DefaultSeriesType = ChartTypes.Line, Height = 380 })
        .SetTooltip(new Tooltip { Formatter = "function() { return '<b>'+ this.x +'</b>: '+ this.y ; }" })
.SetTitle(new DotNet.Highcharts.Options.Title { Text = "" })
.SetLegend(new DotNet.Highcharts.Options.Legend { Enabled = true })
.SetPlotOptions(new PlotOptions
{

    Line = new PlotOptionsLine
    {

        AllowPointSelect = true,
        DataLabels = new PlotOptionsLineDataLabels
        {
            Formatter = "function() { return '<b>'+ this.point.name +'</b>: '+ this.y; }"
        }
    }
})

.SetXAxis(new XAxis
{
    Categories = ar2
})
            .SetSeries(new DotNet.Highcharts.Options.Series
            {
                Name = "Total Classes Attended",
                Type = ChartTypes.Line,
                Data = new Data(ar1.Select(q => (object)q).ToArray())

            }


);
        Literal13.Text = chart.ToHtmlString();

    }

    private void Getresult()
    {
        DataTable dt = new DataTable();
        DataSet ds2 = objUACC.Getresultdetails(Session["username"].ToString());
        dt = ds2.Tables[0];
        string[] ar1 = new string[dt.Rows.Count];
        string[] ar2 = new string[dt.Rows.Count];

        for (int j = 0; j < dt.Rows.Count; j++)
        {
            ar1[j] = dt.Rows[j][1].ToString();
            ar2[j] = dt.Rows[j][0].ToString();

        }
        DotNet.Highcharts.Highcharts chart = new DotNet.Highcharts.Highcharts("chart20")
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
    Categories = ar2
})
            .SetSeries(new DotNet.Highcharts.Options.Series
            {
                Name = "SGPA",
                Type = ChartTypes.Column,
                Data = new Data(ar1.Select(q => (object)q).ToArray())

            }


);
        Literal15.Text = chart.ToHtmlString();

    }

    private void Getresultlinechart()
    {
        DataTable dt = new DataTable();
        DataSet ds2 = objUACC.Getresultdetails(Session["username"].ToString());
        dt = ds2.Tables[0];
        string[] ar1 = new string[dt.Rows.Count];
        string[] ar2 = new string[dt.Rows.Count];

        for (int j = 0; j < dt.Rows.Count; j++)
        {
            ar1[j] = dt.Rows[j][1].ToString();
            ar2[j] = dt.Rows[j][0].ToString();

        }
        DotNet.Highcharts.Highcharts chart = new DotNet.Highcharts.Highcharts("chart18")
       .InitChart(new DotNet.Highcharts.Options.Chart { DefaultSeriesType = ChartTypes.Line, Height = 380 })
        .SetTooltip(new Tooltip { Formatter = "function() { return '<b>'+ this.x +'</b>: '+ this.y ; }" })
.SetTitle(new DotNet.Highcharts.Options.Title { Text = "" })
.SetLegend(new DotNet.Highcharts.Options.Legend { Enabled = true })
.SetPlotOptions(new PlotOptions
{

    Line = new PlotOptionsLine
    {

        AllowPointSelect = true,
        DataLabels = new PlotOptionsLineDataLabels
        {
            Formatter = "function() { return '<b>'+ this.point.name +'</b>: '+ this.y; }"
        }
    }
})

.SetXAxis(new XAxis
{
    Categories = ar2
})
            .SetSeries(new DotNet.Highcharts.Options.Series
            {
                Name = "SGPA",
                Type = ChartTypes.Line,
                Data = new Data(ar1.Select(q => (object)q).ToArray())

            }


);
        Literal14.Text = chart.ToHtmlString();

    }

    private void Admbatch()
    {
        DataSet ds = objUACC.Admbatchwisecount();
        DataTable dt = new DataTable();
        dt = ds.Tables[0];
        string[] ar1 = new string[dt.Rows.Count];
        string[] ar2 = new string[dt.Rows.Count];

        for (int j = 0; j < dt.Rows.Count; j++)
        {
            ar1[j] = dt.Rows[j]["TotalStudents"].ToString();
            ar2[j] = dt.Rows[j]["ADMBATCH"].ToString();

        }
        DotNet.Highcharts.Highcharts chart = new DotNet.Highcharts.Highcharts("chart9")
       .InitChart(new DotNet.Highcharts.Options.Chart { DefaultSeriesType = ChartTypes.Line, Height = 380 })
        .SetTooltip(new Tooltip { Formatter = "function() { return '<b>'+ this.x +'</b>: '+ this.y ; }" })
.SetTitle(new DotNet.Highcharts.Options.Title { Text = "" })
.SetLegend(new DotNet.Highcharts.Options.Legend { Enabled = true })
.SetPlotOptions(new PlotOptions
{

    Line = new PlotOptionsLine
    {

        AllowPointSelect = true,
        DataLabels = new PlotOptionsLineDataLabels
        {
            Formatter = "function() { return '<b>'+ this.point.name +'</b>: '+ this.y; }"
        }
    }
})

.SetXAxis(new XAxis
{
    Categories = ar2
})
            .SetSeries(new DotNet.Highcharts.Options.Series
            {
                Name = "Number of Students",
                Type = ChartTypes.Line,
                Data = new Data(ar1.Select(q => (object)q).ToArray())

            }


);
        Literal7.Text = chart.ToHtmlString();

    }
    private void Admbatcbar()
    {

        DataSet ds = objCommon.FillDropDown("ACD_STUDENT S INNER JOIN ACD_DEGREE D ON (S.DEGREENO=D.DEGREENO)", "LEFT(ROUND(COUNT(IDNO)* 100.0 / (SELECT COUNT(*) FROM ACD_STUDENT),2),4)   AS PERCENTAGE", "D.CODE", "ISNULL(S.ADMCAN, 0) = 0  GROUP BY S.DEGREENO,CODE", "CODE");
        DataTable dt = new DataTable();
        dt = ds.Tables[0];
        string[] ar1 = new string[dt.Rows.Count];
        string[] ar2 = new string[dt.Rows.Count];

        for (int j = 0; j < dt.Rows.Count; j++)
        {
            ar1[j] = dt.Rows[j][0].ToString();
            ar2[j] = dt.Rows[j][1].ToString();

        }
        DotNet.Highcharts.Highcharts chart = new DotNet.Highcharts.Highcharts("chart14")
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
    Categories = ar2
})
            .SetSeries(new DotNet.Highcharts.Options.Series
            {
                Name = "Number of Students",
                Type = ChartTypes.Column,
                Data = new Data(ar1.Select(q => (object)q).ToArray())

            }

);
        Literal11.Text = chart.ToHtmlString();

    }





    private void Admbatcbarclgwise()
    {

        DataSet ds = objCommon.FillDropDown("ACD_STUDENT A INNER JOIN ACD_COLLEGE_MASTER B ON (A.COLLEGE_ID=B.COLLEGE_ID)",  "COUNT(A.IDNO) AS STUDENTS","B.COLLEGE_NAME", "ISNULL(A.ADMCAN, 0) = 0  GROUP BY COLLEGE_NAME", "");
        DataTable dt = new DataTable();
        dt = ds.Tables[0];
        string[] ar1 = new string[dt.Rows.Count];
        string[] ar2 = new string[dt.Rows.Count];

        for (int j = 0; j < dt.Rows.Count; j++)
        {
            ar1[j] = dt.Rows[j][0].ToString();
            ar2[j] = dt.Rows[j][1].ToString();

        }
        DotNet.Highcharts.Highcharts chart = new DotNet.Highcharts.Highcharts("chart74")
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
    Categories = ar2
})
            .SetSeries(new DotNet.Highcharts.Options.Series
            {
                Name = "Number of Students",
                Type = ChartTypes.Column,
                Data = new Data(ar1.Select(q => (object)q).ToArray())

            }

);
        Literal79.Text = chart.ToHtmlString();

    }





    private void Admbatchbar()
    {

        DataSet ds = objUACC.Admbatchwisecount();
        DataTable dt = new DataTable();
        dt = ds.Tables[0];
        string[] ar1 = new string[dt.Rows.Count];
        string[] ar2 = new string[dt.Rows.Count];

        for (int j = 0; j < dt.Rows.Count; j++)
        {
            ar1[j] = dt.Rows[j]["TotalStudents"].ToString();
            ar2[j] = dt.Rows[j]["ADMBATCH"].ToString();

        }
        DotNet.Highcharts.Highcharts chart = new DotNet.Highcharts.Highcharts("chart8")
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
    Categories = ar2
})
            .SetSeries(new DotNet.Highcharts.Options.Series
            {
                Name = "Number of Students",
                Type = ChartTypes.Column,
                Data = new Data(ar1.Select(q => (object)q).ToArray())

            }


);
        Literal6.Text = chart.ToHtmlString();

    }



    private void Feecollectionresult()
    {
        int sessionno = Convert.ToInt32(objCommon.LookUp("ACD_DASHBOARD_MASTER", "SESSIONNO", "ID=40"));
        DataSet ds = objUACC.GetFeecollection(sessionno);
        DataTable dt = new DataTable();
        dt = ds.Tables[0];
        string[] ar1 = new string[dt.Rows.Count];
        string[] ar2 = new string[dt.Rows.Count];

        for (int j = 0; j < dt.Rows.Count; j++)
        {
            ar1[j] = dt.Rows[j]["TOTAL"].ToString();
            ar2[j] = dt.Rows[j]["MONTH"].ToString();

        }
        DotNet.Highcharts.Highcharts chart = new DotNet.Highcharts.Highcharts("chart6")
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
    Categories = ar2
})
            .SetSeries(new DotNet.Highcharts.Options.Series
            {
                Name = "Total Amount",
                Type = ChartTypes.Column,
                Data = new Data(ar1.Select(q => (object)q).ToArray())

            }


);
        Literal4.Text = chart.ToHtmlString();

    }
    private void Feecollectionresultline()
    {
        int sessionno = Convert.ToInt32(objCommon.LookUp("ACD_DASHBOARD_MASTER", "SESSIONNO", "ID=40"));
        DataSet ds = objUACC.GetFeecollection(sessionno);
        Session["feecollection"] = ds;
        DataTable dt = new DataTable();
        dt = ds.Tables[0];
        string[] ar1 = new string[dt.Rows.Count];
        string[] ar2 = new string[dt.Rows.Count];

        for (int j = 0; j < dt.Rows.Count; j++)
        {
            ar1[j] = dt.Rows[j]["TOTAL"].ToString();
            ar2[j] = dt.Rows[j]["MONTH"].ToString();

        }
        DotNet.Highcharts.Highcharts chart = new DotNet.Highcharts.Highcharts("chart7")
       .InitChart(new DotNet.Highcharts.Options.Chart { DefaultSeriesType = ChartTypes.Line, Height = 380 })
        .SetTooltip(new Tooltip { Formatter = "function() { return '<b>'+ this.x +'</b>: '+ this.y ; }" })
.SetTitle(new DotNet.Highcharts.Options.Title { Text = "" })
.SetLegend(new DotNet.Highcharts.Options.Legend { Enabled = true })
.SetPlotOptions(new PlotOptions
{

    Line = new PlotOptionsLine
    {

        AllowPointSelect = true,
        DataLabels = new PlotOptionsLineDataLabels
        {
            Formatter = "function() { return '<b>'+ this.point.name +'</b>: '+ this.y; }"
        }
    }
})

.SetXAxis(new XAxis
{
    Categories = ar2
})
            .SetSeries(new DotNet.Highcharts.Options.Series
            {
                Name = "Total Amount",
                Type = ChartTypes.Line,
                Data = new Data(ar1.Select(q => (object)q).ToArray())

            }


);
        Literal5.Text = chart.ToHtmlString();

    }


    private void Receipttypefee()
    {
        int sessionno = Convert.ToInt32(objCommon.LookUp("ACD_DASHBOARD_MASTER", "SESSIONNO", "ID=41"));
        DataSet ds = objUACC.GetFeeReceipttype(sessionno);
        DataTable dt = new DataTable();
        dt = ds.Tables[0];
        string[] ar1 = new string[dt.Rows.Count];
        string[] ar2 = new string[dt.Rows.Count];

        for (int j = 0; j < dt.Rows.Count; j++)
        {
            ar1[j] = dt.Rows[j]["Total"].ToString();
            ar2[j] = dt.Rows[j]["RECIEPT_CODE"].ToString();

        }
        DotNet.Highcharts.Highcharts chart = new DotNet.Highcharts.Highcharts("chart11")
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
    Categories = ar2
})
            .SetSeries(new DotNet.Highcharts.Options.Series
            {
                Name = "Total Amount",
                Type = ChartTypes.Column,
                Data = new Data(ar1.Select(q => (object)q).ToArray())

            }


);
        Literal8.Text = chart.ToHtmlString();

    }
    private void Feereceipttypebar()
    {
        int sessionno = Convert.ToInt32(objCommon.LookUp("ACD_DASHBOARD_MASTER", "SESSIONNO", "ID=41"));
        DataSet ds = objUACC.GetFeeReceipttype(sessionno);
        DataTable dt = new DataTable();
        dt = ds.Tables[0];
        string[] ar1 = new string[dt.Rows.Count];
        string[] ar2 = new string[dt.Rows.Count];

        for (int j = 0; j < dt.Rows.Count; j++)
        {
            ar1[j] = dt.Rows[j]["Total"].ToString();
            ar2[j] = dt.Rows[j]["RECIEPT_CODE"].ToString();

        }
        DotNet.Highcharts.Highcharts chart = new DotNet.Highcharts.Highcharts("chart12")
       .InitChart(new DotNet.Highcharts.Options.Chart { DefaultSeriesType = ChartTypes.Line, Height = 380 })
        .SetTooltip(new Tooltip { Formatter = "function() { return '<b>'+ this.x +'</b>: '+ this.y ; }" })
.SetTitle(new DotNet.Highcharts.Options.Title { Text = "" })
.SetLegend(new DotNet.Highcharts.Options.Legend { Enabled = true })
.SetPlotOptions(new PlotOptions
{

    Line = new PlotOptionsLine
    {

        AllowPointSelect = true,
        DataLabels = new PlotOptionsLineDataLabels
        {
            Formatter = "function() { return '<b>'+ this.point.name +'</b>: '+ this.y; }"
        }
    }
})

.SetXAxis(new XAxis
{
    Categories = ar2
})
            .SetSeries(new DotNet.Highcharts.Options.Series
            {
                Name = "Total Amount",
                Type = ChartTypes.Line,
                Data = new Data(ar1.Select(q => (object)q).ToArray())

            }


);
        Literal9.Text = chart.ToHtmlString();

    }

    #region payrollchart
    private void EmployeeStaffDetails(int val)
    {
        DataSet ds = objUACC.GetEmployeeStaffDetails(val);
        Session["staffdetails"] = ds;
        DataTable dt = new DataTable();
        dt = ds.Tables[0];
        string[] ar1 = new string[dt.Rows.Count];
        string[] ar2 = new string[dt.Rows.Count];

        for (int j = 0; j < dt.Rows.Count; j++)
        {
            ar1[j] = dt.Rows[j]["SHOWCOUNT"].ToString();
            ar2[j] = dt.Rows[j]["SHOWDETAILS"].ToString();

        }
        DotNet.Highcharts.Highcharts chart = new DotNet.Highcharts.Highcharts("chart101")
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
            Categories = ar2
        })
                    .SetSeries(new DotNet.Highcharts.Options.Series
                    {
                        Name = "STAFF DETAILS",
                        Type = ChartTypes.Column,
                        Data = new Data(ar1.Select(q => (object)q).ToArray())

                    }


        );
        Literal17.Text = chart.ToHtmlString();

    }


    private void EmployeeJoiningDetails(int val)
    {
        DataSet ds = objUACC.GetEmployeeStaffDetails(val);
        Session["empjoning"] = ds;
        DataTable dt = new DataTable();
        dt = ds.Tables[0];
        string[] ar1 = new string[dt.Rows.Count];
        string[] ar2 = new string[dt.Rows.Count];

        for (int j = 0; j < dt.Rows.Count; j++)
        {
            ar1[j] = dt.Rows[j]["SHOWCOUNT"].ToString();
            ar2[j] = dt.Rows[j]["SHOWDETAILS"].ToString();

        }
        DotNet.Highcharts.Highcharts chart = new DotNet.Highcharts.Highcharts("chart102")
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
            Categories = ar2
        })
                    .SetSeries(new DotNet.Highcharts.Options.Series
                    {
                        Name = "EMPLOYEE JOINING DETAILS",
                        Type = ChartTypes.Column,
                        Data = new Data(ar1.Select(q => (object)q).ToArray())

                    }


        );
        Literal19.Text = chart.ToHtmlString();
    }


    private void BindEmpoyeeStaffChart(int val)
    {
        Random randonGen = new Random();
        int totEmployee = Convert.ToInt32(objCommon.LookUp("PAYROLL_PAYMAS", "COUNT(IDNO)", "PSTATUS = 'Y'"));
        lblTotalStaff.Text = totEmployee.ToString();
        DataSet ds = objUACC.GetEmployeeStaffDetails(val);
        DataTable ChartData = ds.Tables[0];
        var list = new List<object[]>();
        foreach (DataRow dr in ChartData.Rows)
        {
            list.Add(new object[]{ 
                                    dr[0],dr[1]
                                    });
        }

        DotNet.Highcharts.Highcharts chart = new DotNet.Highcharts.Highcharts("chart103")
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
                 Depth = 35,
                 AllowPointSelect = true,
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

        Literal16.Text = chart.ToHtmlString();

    }


    private void BindEmpoyeeJoiningChart(int val)
    {
        Random randonGen = new Random();
        string FinYear = Convert.ToString(objCommon.LookUp("PAYROLL_PAY_REF", "SUBSTRING(DATENAME(MM, FDATE),1,3)+''+DATENAME(YYYY,  FDATE)+'-'+SUBSTRING(DATENAME(MM, TDATE),1,3)+''+DATENAME(YYYY,  TDATE)", ""));
        lblEmpFY.Text = FinYear;
        DataSet ds = objUACC.GetEmployeeStaffDetails(val);
        DataTable ChartData = ds.Tables[0];
        var list = new List<object[]>();
        foreach (DataRow dr in ChartData.Rows)
        {
            list.Add(new object[]{ 
                                    dr[0],dr[1]
                                    });
        }

        DotNet.Highcharts.Highcharts chart = new DotNet.Highcharts.Highcharts("chart104")
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
                 Depth = 35,
                 AllowPointSelect = true,
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

        Literal18.Text = chart.ToHtmlString();

    }

    private void EmployeeAttendanceDetails(int val)
    {
        int TotalShiftAssignEmployee = Convert.ToInt32(objCommon.LookUp("PAYROLL_EMPMAS E INNER JOIN PAYROLL_PAYMAS P ON(E.IDNO=P.IDNO and P.PSTATUS='Y')", "COUNT(E.IDNO)AS TOT_SHIFT_ASSIGN_EMP", "ISNULL(E.SHIFTNO,0)>0 AND ISNULL(E.RELIEVING_DATE,CONVERT(DATE,GETDATE()))>=CONVERT(DATE,GETDATE())"));

        lblShiftEmp.Text = TotalShiftAssignEmployee.ToString();
        DateTime currentdate;
        if (txtEmpAttendanceDate.Text == string.Empty)
        {
            currentdate = DateTime.Now;
        }
        else
        {
            currentdate = Convert.ToDateTime(txtEmpAttendanceDate.Text);
        }
        DataSet ds = objUACC.GetEmployeeAttendanceDetails(currentdate, val);
        Session["shiftemployee"] = ds;
        DataTable dt = new DataTable();
        dt = ds.Tables[0];
        string[] ar1 = new string[dt.Rows.Count];
        string[] ar2 = new string[dt.Rows.Count];

        for (int j = 0; j < dt.Rows.Count; j++)
        {
            ar1[j] = dt.Rows[j]["TOT_COUNT"].ToString();
            ar2[j] = dt.Rows[j]["HEADING"].ToString();

        }
        DotNet.Highcharts.Highcharts chart = new DotNet.Highcharts.Highcharts("chart105")
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
            Categories = ar2
        })
                    .SetSeries(new DotNet.Highcharts.Options.Series
                    {
                        Name = "EMPLOYEE ATTENDANCE DETAILS",
                        Type = ChartTypes.Column,
                        Data = new Data(ar1.Select(q => (object)q).ToArray())

                    }


        );
        Literal21.Text = chart.ToHtmlString();
    }

    private void BindEmpoyeeAttendanceChart(int val)
    {
        Random randonGen = new Random();
        DateTime currentdate;
        if (txtEmpAttendanceDate.Text == string.Empty)
        {
            currentdate = DateTime.Now;
        }
        else
        {
            currentdate = Convert.ToDateTime(txtEmpAttendanceDate.Text);
        }
        DataSet ds = objUACC.GetEmployeeAttendanceDetails(currentdate, val);
        DataTable ChartData = ds.Tables[0];
        var list = new List<object[]>();
        foreach (DataRow dr in ChartData.Rows)
        {
            list.Add(new object[]{ 
                                    dr[0],dr[1]
                                    });
        }

        DotNet.Highcharts.Highcharts chart = new DotNet.Highcharts.Highcharts("chart106")
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
                 Depth = 35,
                 AllowPointSelect = true,
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

        Literal20.Text = chart.ToHtmlString();

    }

    protected void txtEmpAttendanceDate_TextChanged(object sender, EventArgs e)
    {
        Div26.Visible = true;
        EmployeeAttendanceDetailsText(50);
        BindEmpoyeeAttendanceChartText(50);

    }
    private void EmployeeAttendanceDetailsText(int val)
    {
        int TotalShiftAssignEmployee = Convert.ToInt32(objCommon.LookUp("PAYROLL_EMPMAS E INNER JOIN PAYROLL_PAYMAS P ON(E.IDNO=P.IDNO and P.PSTATUS='Y')", "COUNT(E.IDNO)AS TOT_SHIFT_ASSIGN_EMP", "ISNULL(E.SHIFTNO,0)>0 AND ISNULL(E.RELIEVING_DATE,CONVERT(DATE,GETDATE()))>=CONVERT(DATE,GETDATE())"));

        lblShiftEmp.Text = TotalShiftAssignEmployee.ToString();
        DateTime currentdate;
        if (txtEmpAttendanceDate.Text == string.Empty)
        {
            currentdate = DateTime.Now;
        }
        else
        {
            currentdate = Convert.ToDateTime(txtEmpAttendanceDate.Text);
        }
        DataSet ds = objUACC.GetEmployeeAttendanceDetails(currentdate, val);
        DataTable dt = new DataTable();
        dt = ds.Tables[0];
        string[] ar1 = new string[dt.Rows.Count];
        string[] ar2 = new string[dt.Rows.Count];

        for (int j = 0; j < dt.Rows.Count; j++)
        {
            ar1[j] = dt.Rows[j]["TOT_COUNT"].ToString();
            ar2[j] = dt.Rows[j]["HEADING"].ToString();

        }
        DotNet.Highcharts.Highcharts chart = new DotNet.Highcharts.Highcharts("chart107")
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
            Categories = ar2
        })
                    .SetSeries(new DotNet.Highcharts.Options.Series
                    {
                        Name = "EMPLOYEE ATTENDANCE DETAILS",
                        Type = ChartTypes.Column,
                        Data = new Data(ar1.Select(q => (object)q).ToArray())

                    }


        );
        Literal21.Text = chart.ToHtmlString();
    }

    private void BindEmpoyeeAttendanceChartText(int val)
    {
        Random randonGen = new Random();
        DateTime currentdate;
        if (txtEmpAttendanceDate.Text == string.Empty)
        {
            currentdate = DateTime.Now;
        }
        else
        {
            currentdate = Convert.ToDateTime(txtEmpAttendanceDate.Text);
        }
        DataSet ds = objUACC.GetEmployeeAttendanceDetails(currentdate, val);
        DataTable ChartData = ds.Tables[0];
        var list = new List<object[]>();
        foreach (DataRow dr in ChartData.Rows)
        {
            list.Add(new object[]{ 
                                    dr[0],dr[1]
                                    });
        }

        DotNet.Highcharts.Highcharts chart = new DotNet.Highcharts.Highcharts("chart108")
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
                 Depth = 35,
                 AllowPointSelect = true,
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

        Literal20.Text = chart.ToHtmlString();

    }

    

    #endregion


    private void Totaladmcountyear()
    {
        int sessionno = Convert.ToInt32(objCommon.LookUp("ACD_DASHBOARD_MASTER", "SESSIONNO", "ID=54"));
        int idno = Convert.ToInt32(objCommon.LookUp("User_Acc", "isnull(UA_IDNO,0)", "UA_NAME='" + Session["username"].ToString() + "'"));
        int branchno = Convert.ToInt32(ddlbranch.SelectedValue);
        DataSet ds2 = objUACC.GetTotaladmcountyear(branchno);
        Session["yearwise"] = ds2;
        DataTable dt = new DataTable();
        dt = ds2.Tables[0];

        string[] ar1 = new string[dt.Rows.Count];
        string[] ar2 = new string[dt.Rows.Count];
        string[] ar3 = new string[dt.Rows.Count];
        string[] ar4 = new string[dt.Rows.Count];
        for (int j = 0; j < dt.Rows.Count; j++)
        {
            ar1[j] = dt.Rows[j]["year"].ToString();
            ar2[j] = dt.Rows[j]["TOTAL_COUNT"].ToString();
            ar3[j] = dt.Rows[j]["MALE_COUNT"].ToString();
            ar4[j] = dt.Rows[j]["FEMALE_COUNT"].ToString();
        }
        DotNet.Highcharts.Highcharts admbatchwisegendercount = new DotNet.Highcharts.Highcharts("admbatchwisegendercount")//Name of the chart
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
                    new DotNet.Highcharts.Options.Series {Name="Total Students", Data = new Data(ar2.Select(q => (object)q).ToArray()) },
                    new DotNet.Highcharts.Options.Series { Name="Male", Data = new Data(ar3.Select(q => (object)q).ToArray()) },
                    new DotNet.Highcharts.Options.Series { Name="Female", Data = new Data(ar4.Select(q => (object)q).ToArray()) },
                   
                }

);
        Literal30.Text = admbatchwisegendercount.ToHtmlString();

        DotNet.Highcharts.Highcharts admbatchwisegendercountsecond = new DotNet.Highcharts.Highcharts

("admbatchwisegendercountsecond")
      .InitChart(new DotNet.Highcharts.Options.Chart { DefaultSeriesType = ChartTypes.Line, Height = 380 })
       .SetTooltip(new Tooltip { Formatter = "function() { return '<b>'+ this.x +'</b>: '+ this.y ; }" })
.SetTitle(new DotNet.Highcharts.Options.Title { Text = "" })
.SetLegend(new DotNet.Highcharts.Options.Legend { Enabled = true })
.SetPlotOptions(new PlotOptions
{

    Line = new PlotOptionsLine
    {

        AllowPointSelect = true,
        DataLabels = new PlotOptionsLineDataLabels
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
                    new DotNet.Highcharts.Options.Series {Name="Total Students", Data = new Data(ar2.Select(q => (object)q).ToArray()) },
                    new DotNet.Highcharts.Options.Series { Name="Male", Data = new Data(ar3.Select(q => (object)q).ToArray()) },
                    new DotNet.Highcharts.Options.Series { Name="Female", Data = new Data(ar4.Select(q => (object)q).ToArray()) },
                   
                }

);
        Literal31.Text = admbatchwisegendercountsecond.ToHtmlString();
    }


    protected void ddlbranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        Totaladmcountyear();
    }

    private void Totalfeeamountyearwise()
    {
        DataTable dt = new System.Data.DataTable();
        int branchno = Convert.ToInt32(ddlbranchfees.SelectedValue);
        DataSet ds1 = objUACC.GetTotalfeeamountyearwise(branchno);
        Session["feecollection"] = ds1;
        dt = ds1.Tables[0];

        string[] ar1 = new string[dt.Rows.Count];
        string[] ar2 = new string[dt.Rows.Count];
        string[] ar3 = new string[dt.Rows.Count];
        string[] ar4 = new string[dt.Rows.Count];

        for (int j = 0; j < dt.Rows.Count; j++)
        {
            ar1[j] = dt.Rows[j]["year"].ToString();
            ar2[j] = dt.Rows[j]["TOTAL_AMT_D"].ToString();
            ar3[j] = dt.Rows[j]["TOTAL_AMT"].ToString();
            ar4[j] = dt.Rows[j]["BAL_AMT"].ToString();

        }
        DotNet.Highcharts.Highcharts divmonthwisefeecollection = new DotNet.Highcharts.Highcharts("divmonthwisefeecollection")
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
                    new DotNet.Highcharts.Options.Series {Name="Total Demand", Data = new Data(ar2.Select(q => (object)q).ToArray()) },
                    new DotNet.Highcharts.Options.Series { Name="Total Paid", Data = new Data(ar3.Select(q => (object)q).ToArray()) },
                    new DotNet.Highcharts.Options.Series { Name="Total Balance", Data = new Data(ar4.Select(q => (object)q).ToArray()) },
                }

);
        Literal32.Text = divmonthwisefeecollection.ToHtmlString();
        dt = ds1.Tables[0];
        for (int j = 0; j < dt.Rows.Count; j++)
        {
            ar1[j] = dt.Rows[j]["year"].ToString();
            ar2[j] = dt.Rows[j]["TOTAL_AMT_D"].ToString();
            ar3[j] = dt.Rows[j]["TOTAL_AMT"].ToString();
            ar4[j] = dt.Rows[j]["BAL_AMT"].ToString();

        }
        DotNet.Highcharts.Highcharts divmonthwisefeecollectionsecond = new DotNet.Highcharts.Highcharts("divmonthwisefeecollectionsecond")
       .InitChart(new DotNet.Highcharts.Options.Chart { DefaultSeriesType = ChartTypes.Line, Height = 380 })
        .SetTooltip(new Tooltip { Formatter = "function() { return '<b>'+ this.x +'</b>: '+ this.y ; }" })
.SetTitle(new DotNet.Highcharts.Options.Title { Text = "" })
.SetLegend(new DotNet.Highcharts.Options.Legend { Enabled = true })
.SetPlotOptions(new PlotOptions
{

    Line = new PlotOptionsLine
    {

        AllowPointSelect = true,
        DataLabels = new PlotOptionsLineDataLabels
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
                    new DotNet.Highcharts.Options.Series {Name="Total Demand", Data = new Data(ar2.Select(q => (object)q).ToArray()) },
                    new DotNet.Highcharts.Options.Series { Name="Total Paid", Data = new Data(ar3.Select(q => (object)q).ToArray()) },
                    new DotNet.Highcharts.Options.Series { Name="Total Balance", Data = new Data(ar4.Select(q => (object)q).ToArray()) },
                   
                }

);
        Literal33.Text = divmonthwisefeecollectionsecond.ToHtmlString();

    }


    protected void ddlbranchfees_SelectedIndexChanged(object sender, EventArgs e)
    {
        Totalfeeamountyearwise();
    }
    protected void btnimg_Click(object sender, ImageClickEventArgs e)
    {
           DataSet ds=new DataSet();
          ds = (DataSet)Session["statewise"];
          if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
       {
          ExportExcel(ds, "UG_Admission_State_Wise_Count");
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
    protected void btnbranchwise_Click(object sender, ImageClickEventArgs e)
    {
        DataSet ds = new DataSet();
        ds = (DataSet)Session["branchwise"];
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            ExportExcel(ds, "Branch_Wise_Count");
        }
    }
    protected void btnFeeCollection_Click(object sender, ImageClickEventArgs e)
    { 
         DataSet ds = new DataSet();
         ds = (DataSet)Session["feecollection"];
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            ExportExcel(ds, "FeeCollection_Total");
        }
        
    }
    protected void btnyearwise_Click(object sender, ImageClickEventArgs e)
    {
        DataSet ds = new DataSet();
        ds = (DataSet)Session["yearwise"];
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            ExportExcel(ds, "Year_Wise_Count");
        }
    }
    protected void btndegreewise_Click(object sender, ImageClickEventArgs e)
    {
        DataSet ds = new DataSet();
        ds = (DataSet)Session["degreewise"];
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            ExportExcel(ds, "Degree_Wise_Count");
        }
    }
    protected void btnEmpjoining_Click(object sender, ImageClickEventArgs e)
    {
        DataSet ds = new DataSet();
        ds = (DataSet)Session["empjoning"];
        if(ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        ExportExcel(ds,"Empolyee_Joning_Details_Count");
    }
    protected void btnregistrationcount_Click(object sender, ImageClickEventArgs e)
    {
        DataSet ds = new DataSet();
        ds = (DataSet)Session["admregistration"];
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            ExportExcel(ds, "Admission_Registration_Count");
        }
    }
    protected void btncategorywise_Click(object sender, ImageClickEventArgs e)
    {
        DataSet ds = new DataSet();
        ds = (DataSet)Session["categorywise"];
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            ExportExcel(ds, "Category_Wise_Registration_Count");
        }
    }
    protected void btnbankwise_Click(object sender, ImageClickEventArgs e)
    {
        DataSet ds = new DataSet();
        ds = (DataSet)Session["bankwise"];
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            ExportExcel(ds, "Bank_Wise_DD_Collection");
        }
    }
    protected void feecollection_Click(object sender, ImageClickEventArgs e)
    {
        DataSet ds = new DataSet();
        ds = (DataSet)Session["feecollection"];
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            ExportExcel(ds, "Month_Wise_Fee_Collection");
        }
    }
    protected void btnstaffdetails_Click(object sender, ImageClickEventArgs e)
    {
        DataSet ds = new DataSet();
        ds = (DataSet)Session["staffdetails"];
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            ExportExcel(ds, "Staff_Detalis");
        }
    }
    protected void btnshiftemployees_Click(object sender, ImageClickEventArgs e)
    {
        DataSet ds = new DataSet();
        ds = (DataSet)Session["shiftemployee"];
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            ExportExcel(ds, "Shift_Employees");
        }
    }
}
