
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


public partial class OnlineAdmissionStatusChart : System.Web.UI.Page
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
        string batch = objCommon.LookUp("ACD_DASHBOARD_MASTER_ONLINE_ADM", "BATCHNO", "status=1");
        int batchno = Convert.ToInt32(batch == "" ? "0" : batch);
        string batchname = objCommon.LookUp("ACD_ADMBATCH", "BATCHNAME", "BATCHNO=" + Convert.ToInt32(batchno));
        ViewState["batchno"] = batchno;
        ViewState["batchname"] = batchname;

        if (!Page.IsPostBack)
        {


            txtEmpAttendanceDate.Text = DateTime.Now.ToString();
            //DataSet ds = objCommon.FillDropDown("ACD_DASHBOARD_CONFIGURATION_ONLINE_ADM", "USERTYPE", "DASHBOARD", "USERTYPE=" + Session["usertype"].ToString(), "USERTYPE");
            DataSet ds = objCommon.FillDropDown("ACD_DASHBOARD_CONFIGURATION_ONLINE_ADM CROSS APPLY (SELECT VALUE FROM [DBO].[SPLIT] (DASHBOARD,','))A INNER JOIN ACD_DASHBOARD_MASTER_ONLINE_ADM DM ON(A.VALUE=DM.ID)", "USERTYPE", "A.VALUE AS DASHBOARD", "STATUS=1 AND USERTYPE=" + Session["usertype"].ToString(), "USERTYPE");
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int cnt = 0; cnt < ds.Tables[0].Rows.Count; cnt++)
                {

                    int val = 0;
                    string value = ds.Tables[0].Rows[cnt]["DASHBOARD"].ToString();
                 //   int val = 0;
                    if (value != "")
                    {
                        val = Convert.ToInt32(value);
                    }

                    if (val == 4)
                    {
                        DataSet ds1 = objUACC.GetallDashboard_onlineadm(val, batchno);
                        if (ds1 != null && ds1.Tables.Count > 0)
                        {
                            if (ds1.Tables[0].Rows.Count > 0)
                            {
                                lbladmbatch1.Text = batchname;
                                lblUGTotalStudent.Text = ds1.Tables[0].Rows[0][0].ToString();
                            }
                        }
                        totalregisteredug.Visible = true;
                    }
                    else if (val == 3)
                    {
                        DataSet ds1 = objUACC.GetallDashboard_onlineadm(val, batchno);
                        if (ds1 != null && ds1.Tables.Count > 0)
                        {
                            if (ds1.Tables[0].Rows.Count > 0)
                            {
                                lbladmbatch2.Text = batchname;
                                lblotalSubmitted.Text = ds1.Tables[0].Rows[0][0].ToString();
                            }
                        }
                        totalSubmitted.Visible = true;
                    }
                    else if (val == 1)
                    {
                        DataSet ds1 = objUACC.GetallDashboard_onlineadm(val, batchno);
                        if (ds1 != null && ds1.Tables.Count > 0)
                        {
                            if (ds1.Tables[0].Rows.Count > 0)

                            {
                                lbladmbatch3.Text = batchname;
                                lbltotfemale.Text = ds1.Tables[0].Rows[0][0].ToString();
                            }
                        }
                        totfemale.Visible = true;
                    }
                    else if (val == 2)
                    {
                        DataSet ds1 = objUACC.GetallDashboard_onlineadm(val, batchno);
                        if (ds1 != null && ds1.Tables.Count > 0)
                        {
                            if (ds1.Tables[0].Rows.Count > 0)
                            {
                                lbladmbatch4.Text = batchname;
                                lblotalmale.Text = ds1.Tables[0].Rows[0][0].ToString();
                            }
                        }
                        totmale.Visible = true;
                    }

                    //else if (val == 5)
                    //{
                    //    DataSet ds1 = objUACC.GetallDashboard_onlineadm(val, batchno);
                    //    if (ds1 != null && ds1.Tables.Count > 0)
                    //    {
                    //        if (ds1.Tables[0].Rows.Count > 0)
                    //        {
                    //            lbladmbatch5.Text = batchname;
                    //            lbltotfeepaid.Text = ds1.Tables[0].Rows[0][0].ToString();
                    //        }
                    //    }
                    //    totfeepaid.Visible = true;
                    //}

                    else if (val == 7)
                    {
                        DataSet ds1 = objUACC.GetallDashboard_onlineadm(val, batchno);
                        if (ds1 != null && ds1.Tables.Count > 0)
                        {
                            if (ds1.Tables[0].Rows.Count > 0)
                            {
                                lbladmbatch6.Text = batchname;
                                lbltotfeepending.Text = ds1.Tables[0].Rows[0][0].ToString();
                            }
                        }
                        totfeepending.Visible = true;
                    }
                    else if (val == 6)
                    {
                        DataSet ds1 = objUACC.GetallDashboard_onlineadm(val, batchno);
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
                                lbladmbatch7.Text=ViewState["batchname"].ToString();
                            }
                        }
                    }

                    else if (val == 9)
                    {
                        Div11.Visible = true;
                        lbladmbatch9.Text = ViewState["batchname"].ToString();
                        this.Bindchart();
                        //this.Admbatcbar();
                    }
                    else if (val == 10)
                    {
                        Div50.Visible = true;
                        this.Bindchartclgwise(val, batchno);
                        //this.Admbatcbarclgwise(val, batchno);
                    }



                    else if (val == 8)
                    {
                        DataTable dt = new System.Data.DataTable();
                        DataSet ds1 = objUACC.GetallDashboard_onlineadm(val, batchno);
                        Session["DateWiseadmregistration"] = ds1;
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
                        lbladmbatch8.Text = ViewState["batchname"].ToString();
                    }
                }
            }
        }
    }


    private void Admbatcbar()
    {

        DataSet ds = objCommon.FillDropDown("ACD_USER_REGISTRATION R INNER JOIN ACD_NEWUSER_REGISTRATION NR ON NR.USERNO=R.USERNO inner join ACD_USER_PROFILE_STATUS C on C.USERNO = NR.USERNO INNER JOIN ACD_USER_BRANCH_PREF BP ON BP.USERNO = NR.USERNO INNER JOIN ACD_DEGREE ADG ON ADG.DEGREENO = BP.DEGREENO", "ADG.CODE", " COUNT(R.USERNO)  AS PERCENTAGE", "isnull(CONFIRM_STATUS,0)=1 AND ADMBATCH =" + ViewState["batchno"] + " GROUP BY ADG.DEGREENO,CODE", "CODE");
        //DataSet ds = objCommon.FillDropDown("ACD_STUDENT S INNER JOIN ACD_DEGREE D ON (S.DEGREENO=D.DEGREENO)", "LEFT(ROUND(COUNT(IDNO)* 100.0 / (SELECT COUNT(*) FROM ACD_STUDENT),2),4)   AS PERCENTAGE", "D.CODE", "ISNULL(S.ADMCAN, 0) = 0  GROUP BY S.DEGREENO,CODE", "CODE");
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



    private void Bindchartclgwise(int val, int batchno)
    {
        Random randonGen = new Random();
        DataSet ds = objUACC.GetallDashboard_onlineadm(val, batchno);
        Session["ApplicationStages"] = ds;
        DataTable ChartData = ds.Tables[0];
        var list = new List<object[]>();
        foreach (DataRow dr in ChartData.Rows)
        {
            list.Add(new object[]{ 
                                    dr[0],dr[1]
                                    });
        }
        DotNet.Highcharts.Highcharts chart = new DotNet.Highcharts.Highcharts("chart90")
            //.InitChart(new DotNet.Highcharts.Options.Chart
            //{
            //    DefaultSeriesType = ChartTypes.Column,
            //    Height = 380,
            //    Options3d = new ChartOptions3d
            //    {
            //        Enabled = true,
            //        Alpha = 45,
            //        Beta = 0

        //    }


        //})
          .InitChart(new DotNet.Highcharts.Options.Chart
          {
              DefaultSeriesType = ChartTypes.Column,
              Height = 380,
              Options3d = new ChartOptions3d {
                  Enabled = true,
                  Alpha = 45,
                  Beta = 0
              }
          })
         .SetTooltip(new Tooltip { Formatter = "function() { return '<b>'+ this.point.name +'</b>: '+ this.y ; }" })
         .SetLegend(new DotNet.Highcharts.Options.Legend { Enabled = true })
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

        Literal84.Text = chart.ToHtmlString();
        lbladmbatch11.Text = ViewState["batchname"].ToString(); ;

        DataTable dt = new System.Data.DataTable();
        DataSet ds1 = objUACC.GetallDashboard_onlineadm(val, batchno);
        dt = ds1.Tables[0];

        string[] ar1 = new string[dt.Rows.Count];
        string[] ar2 = new string[dt.Rows.Count];
        for (int j = 0; j < dt.Rows.Count; j++)
        {
            ar1[j] = dt.Rows[j][0].ToString();
            ar2[j] = dt.Rows[j][1].ToString();
        }

        DotNet.Highcharts.Highcharts chart1 = new DotNet.Highcharts.Highcharts("chart91")
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
                    new DotNet.Highcharts.Options.Series {Name="Application Stages", Data = new Data(ar1.Select(q => (object)q).ToArray()) },
                    new DotNet.Highcharts.Options.Series { Name="Total Count", Data = new Data(ar2.Select(q => (object)q).ToArray()) },
                }

        );

        Literal89.Text = chart1.ToHtmlString();
        dt = ds1.Tables[0];
        for (int j = 0; j < dt.Rows.Count; j++)
        {
            ar1[j] = dt.Rows[j][0].ToString();
            ar2[j] =dt.Rows[j][1].ToString();
        }

    }


    private void Bindchart()
    {
        Random randonGen = new Random();
        DataSet ds = objCommon.FillDropDown("ACD_USER_REGISTRATION R INNER JOIN ACD_NEWUSER_REGISTRATION NR ON NR.USERNO=R.USERNO inner join ACD_USER_PROFILE_STATUS C on C.USERNO = NR.USERNO INNER JOIN ACD_USER_BRANCH_PREF BP ON BP.USERNO = NR.USERNO INNER JOIN ACD_DEGREE ADG ON ADG.DEGREENO = BP.DEGREENO", "ADG.CODE", " COUNT(R.USERNO)  AS PERCENTAGE", "isnull(CONFIRM_STATUS,0)=1 AND ADMBATCH =" + ViewState["batchno"] + " GROUP BY ADG.DEGREENO,CODE", "CODE");
        //DataSet ds = objCommon.FillDropDown("ACD_STUDENT S INNER JOIN ACD_DEGREE D ON (S.DEGREENO=D.DEGREENO)", "LEFT(ROUND(COUNT(IDNO)* 100.0 / (SELECT COUNT(*) FROM ACD_STUDENT),2),4)   AS PERCENTAGE", "D.CODE", "ISNULL(S.ADMCAN, 0) = 0  GROUP BY S.DEGREENO,CODE", "CODE");
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


        DataTable dt = new System.Data.DataTable();
        dt = ds.Tables[0];

        string[] ar1 = new string[dt.Rows.Count];
        string[] ar2 = new string[dt.Rows.Count];
        for (int j = 0; j < dt.Rows.Count; j++)
        {
            ar1[j] = dt.Rows[j][0].ToString();
            ar2[j] = dt.Rows[j][1].ToString();
        }

        DotNet.Highcharts.Highcharts chart1 = new DotNet.Highcharts.Highcharts("chart14")
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
                    new DotNet.Highcharts.Options.Series {Name="Degree", Data = new Data(ar1.Select(q => (object)q).ToArray()) },
                    new DotNet.Highcharts.Options.Series { Name="Total Count", Data = new Data(ar2.Select(q => (object)q).ToArray()) },
                }
        );

        Literal11.Text = chart1.ToHtmlString();
        //dt = ds1.Tables[0];
        //for (int j = 0; j < dt.Rows.Count; j++)
        //{
        //    ar1[j] = dt.Rows[j][0].ToString();
        //    ar2[j] = dt.Rows[j][1].ToString();
        //}

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

    protected void btnStateWise_Click(object sender, ImageClickEventArgs e)
    {
        DataSet ds = new DataSet();
        ds = (DataSet)Session["statewise"];
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            ExportExcel(ds, "State_Wise_Count");
        }
    }

    protected void btnDateWise_Click(object sender, ImageClickEventArgs e)
    {
        DataSet ds = new DataSet();
        ds = (DataSet)Session["DateWiseadmregistration"];
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            ExportExcel(ds, "Date_Wise_Count");
        }
    }

    protected void btnApplicationStagesWise_Click(object sender, ImageClickEventArgs e)
    {
        DataSet ds = new DataSet();
        ds = (DataSet)Session["ApplicationStages"];
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            ExportExcel(ds, "Application_Stages_Wise_Count");
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


//    private void Attendancedetails()
//    {


//        int sessionno = Convert.ToInt32(objCommon.LookUp("ACD_DASHBOARD_MASTER", "SESSIONNO", "status=1"));
//        int idno = Convert.ToInt32(objCommon.LookUp("User_Acc", "isnull(UA_IDNO,0)", "UA_NAME='" + Session["username"].ToString() + "'"));
//        DataSet ds2 = objUACC.GetAttendancedetails(idno, sessionno);
//        DataTable dt = new DataTable();
//        dt = ds2.Tables[0];
//        string[] ar1 = new string[dt.Rows.Count];
//        string[] ar2 = new string[dt.Rows.Count];

//        for (int j = 0; j < dt.Rows.Count; j++)
//        {
//            ar1[j] = dt.Rows[j][0].ToString();
//            ar2[j] = dt.Rows[j][1].ToString();

//        }
//        DotNet.Highcharts.Highcharts chart = new DotNet.Highcharts.Highcharts("chart16")
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
//    Categories = ar2
//})
//            .SetSeries(new DotNet.Highcharts.Options.Series
//            {
//                Name = "Total Classes Attended",
//                Type = ChartTypes.Column,
//                Data = new Data(ar1.Select(q => (object)q).ToArray())

//            }


//);
//        Literal12.Text = chart.ToHtmlString();

//    }
//    private void Bindchart()
//    {
//        Random randonGen = new Random();
//        DataSet ds = objCommon.FillDropDown("ACD_STUDENT S INNER JOIN ACD_DEGREE D ON (S.DEGREENO=D.DEGREENO)", "D.CODE", "LEFT(ROUND(COUNT(IDNO)* 100.0 / (SELECT COUNT(*) FROM ACD_STUDENT),2),4)   AS PERCENTAGE", "ISNULL(S.ADMCAN, 0) = 0  GROUP BY S.DEGREENO,CODE", "CODE");
//        Session["degreewise"] = ds;
//        DataTable ChartData = ds.Tables[0];
//        var list = new List<object[]>();
//        foreach (DataRow dr in ChartData.Rows)
//        {
//            list.Add(new object[]{ 
//                                    dr[0],dr[1]
//                                    });
//        }

//        DotNet.Highcharts.Highcharts chart = new DotNet.Highcharts.Highcharts("chart13")
//        .InitChart(new DotNet.Highcharts.Options.Chart
//        {
//            Height = 380,
//            Options3d = new ChartOptions3d
//            {
//                Enabled = true,
//                Alpha = 45,
//                Beta = 0

//            }


//        })

//        .SetTooltip(new Tooltip { Formatter = "function() { return '<b>'+ this.point.name +'</b>: '+ this.y ; }" })
//         .SetPlotOptions(new PlotOptions
//         {

//             Pie = new PlotOptionsPie
//             {
//                 Depth = 35,
//                 AllowPointSelect = true,
//                 DataLabels = new PlotOptionsPieDataLabels
//                 {
//                     Formatter = "function() { return '<b>'+ this.point.name +'</b>: '+ this.y; }"
//                 }
//             }
//         })

//         .SetTitle(new DotNet.Highcharts.Options.Title { Text = "" })
//         .SetSeries(new DotNet.Highcharts.Options.Series
//         {
//             Type = ChartTypes.Pie,
//             Name = "Browser share",

//             Data = new Data(list.Select(b => new { Name = (object)b.GetValue(0), y = (object)b.GetValue(1) }).ToArray())
//         });

//        Literal10.Text = chart.ToHtmlString();

//    }

//    private void attendancelinechart()
//    {
//        DataTable dt = new System.Data.DataTable();
//        int sessionno = Convert.ToInt32(objCommon.LookUp("ACD_DASHBOARD_MASTER", "SESSIONNO", "status=1"));
//        int idno = Convert.ToInt32(objCommon.LookUp("User_Acc", "isnull(UA_IDNO,0)", "UA_NAME='" + Session["username"].ToString() + "'"));
//        DataSet ds2 = objUACC.GetAttendancedetails(idno, sessionno);
//        dt = ds2.Tables[0];

//        string[] ar1 = new string[dt.Rows.Count];
//        string[] ar2 = new string[dt.Rows.Count];

//        for (int j = 0; j < dt.Rows.Count; j++)
//        {
//            ar1[j] = dt.Rows[j][0].ToString();
//            ar2[j] = dt.Rows[j][1].ToString();
//        }

//        DotNet.Highcharts.Highcharts chart = new DotNet.Highcharts.Highcharts("chart17")
//       .InitChart(new DotNet.Highcharts.Options.Chart { DefaultSeriesType = ChartTypes.Line, Height = 380 })
//        .SetTooltip(new Tooltip { Formatter = "function() { return '<b>'+ this.x +'</b>: '+ this.y ; }" })
//.SetTitle(new DotNet.Highcharts.Options.Title { Text = "" })
//.SetLegend(new DotNet.Highcharts.Options.Legend { Enabled = true })
//.SetPlotOptions(new PlotOptions
//{

//    Line = new PlotOptionsLine
//    {

//        AllowPointSelect = true,
//        DataLabels = new PlotOptionsLineDataLabels
//        {
//            Formatter = "function() { return '<b>'+ this.point.name +'</b>: '+ this.y; }"
//        }
//    }
//})

//.SetXAxis(new XAxis
//{
//    Categories = ar2
//})
//            .SetSeries(new DotNet.Highcharts.Options.Series
//            {
//                Name = "Total Classes Attended",
//                Type = ChartTypes.Line,
//                Data = new Data(ar1.Select(q => (object)q).ToArray())

//            }


//);
//        Literal13.Text = chart.ToHtmlString();

//    }

//    private void Getresult()
//    {
//        DataTable dt = new DataTable();
//        DataSet ds2 = objUACC.Getresultdetails(Session["username"].ToString());
//        dt = ds2.Tables[0];
//        string[] ar1 = new string[dt.Rows.Count];
//        string[] ar2 = new string[dt.Rows.Count];

//        for (int j = 0; j < dt.Rows.Count; j++)
//        {
//            ar1[j] = dt.Rows[j][1].ToString();
//            ar2[j] = dt.Rows[j][0].ToString();

//        }
//        DotNet.Highcharts.Highcharts chart = new DotNet.Highcharts.Highcharts("chart20")
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
//    Categories = ar2
//})
//            .SetSeries(new DotNet.Highcharts.Options.Series
//            {
//                Name = "SGPA",
//                Type = ChartTypes.Column,
//                Data = new Data(ar1.Select(q => (object)q).ToArray())

//            }


//);
//        Literal15.Text = chart.ToHtmlString();

//    }

//    private void Getresultlinechart()
//    {
//        DataTable dt = new DataTable();
//        DataSet ds2 = objUACC.Getresultdetails(Session["username"].ToString());
//        dt = ds2.Tables[0];
//        string[] ar1 = new string[dt.Rows.Count];
//        string[] ar2 = new string[dt.Rows.Count];

//        for (int j = 0; j < dt.Rows.Count; j++)
//        {
//            ar1[j] = dt.Rows[j][1].ToString();
//            ar2[j] = dt.Rows[j][0].ToString();

//        }
//        DotNet.Highcharts.Highcharts chart = new DotNet.Highcharts.Highcharts("chart18")
//       .InitChart(new DotNet.Highcharts.Options.Chart { DefaultSeriesType = ChartTypes.Line, Height = 380 })
//        .SetTooltip(new Tooltip { Formatter = "function() { return '<b>'+ this.x +'</b>: '+ this.y ; }" })
//.SetTitle(new DotNet.Highcharts.Options.Title { Text = "" })
//.SetLegend(new DotNet.Highcharts.Options.Legend { Enabled = true })
//.SetPlotOptions(new PlotOptions
//{

//    Line = new PlotOptionsLine
//    {

//        AllowPointSelect = true,
//        DataLabels = new PlotOptionsLineDataLabels
//        {
//            Formatter = "function() { return '<b>'+ this.point.name +'</b>: '+ this.y; }"
//        }
//    }
//})

//.SetXAxis(new XAxis
//{
//    Categories = ar2
//})
//            .SetSeries(new DotNet.Highcharts.Options.Series
//            {
//                Name = "SGPA",
//                Type = ChartTypes.Line,
//                Data = new Data(ar1.Select(q => (object)q).ToArray())

//            }


//);
//        Literal14.Text = chart.ToHtmlString();

//    }

//    private void Admbatch()
//    {
//        DataSet ds = objUACC.Admbatchwisecount();
//        DataTable dt = new DataTable();
//        dt = ds.Tables[0];
//        string[] ar1 = new string[dt.Rows.Count];
//        string[] ar2 = new string[dt.Rows.Count];

//        for (int j = 0; j < dt.Rows.Count; j++)
//        {
//            ar1[j] = dt.Rows[j]["TotalStudents"].ToString();
//            ar2[j] = dt.Rows[j]["ADMBATCH"].ToString();

//        }
//        DotNet.Highcharts.Highcharts chart = new DotNet.Highcharts.Highcharts("chart9")
//       .InitChart(new DotNet.Highcharts.Options.Chart { DefaultSeriesType = ChartTypes.Line, Height = 380 })
//        .SetTooltip(new Tooltip { Formatter = "function() { return '<b>'+ this.x +'</b>: '+ this.y ; }" })
//.SetTitle(new DotNet.Highcharts.Options.Title { Text = "" })
//.SetLegend(new DotNet.Highcharts.Options.Legend { Enabled = true })
//.SetPlotOptions(new PlotOptions
//{

//    Line = new PlotOptionsLine
//    {

//        AllowPointSelect = true,
//        DataLabels = new PlotOptionsLineDataLabels
//        {
//            Formatter = "function() { return '<b>'+ this.point.name +'</b>: '+ this.y; }"
//        }
//    }
//})

//.SetXAxis(new XAxis
//{
//    Categories = ar2
//})
//            .SetSeries(new DotNet.Highcharts.Options.Series
//            {
//                Name = "Number of Students",
//                Type = ChartTypes.Line,
//                Data = new Data(ar1.Select(q => (object)q).ToArray())

//            }


//);
//        Literal7.Text = chart.ToHtmlString();

//    }
//    private void Admbatcbar()
//    {

//        DataSet ds = objCommon.FillDropDown("ACD_STUDENT S INNER JOIN ACD_DEGREE D ON (S.DEGREENO=D.DEGREENO)", "LEFT(ROUND(COUNT(IDNO)* 100.0 / (SELECT COUNT(*) FROM ACD_STUDENT),2),4)   AS PERCENTAGE", "D.CODE", "ISNULL(S.ADMCAN, 0) = 0  GROUP BY S.DEGREENO,CODE", "CODE");
//        DataTable dt = new DataTable();
//        dt = ds.Tables[0];
//        string[] ar1 = new string[dt.Rows.Count];
//        string[] ar2 = new string[dt.Rows.Count];

//        for (int j = 0; j < dt.Rows.Count; j++)
//        {
//            ar1[j] = dt.Rows[j][0].ToString();
//            ar2[j] = dt.Rows[j][1].ToString();

//        }
//        DotNet.Highcharts.Highcharts chart = new DotNet.Highcharts.Highcharts("chart14")
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
//    Categories = ar2
//})
//            .SetSeries(new DotNet.Highcharts.Options.Series
//            {
//                Name = "Number of Students",
//                Type = ChartTypes.Column,
//                Data = new Data(ar1.Select(q => (object)q).ToArray())

//            }

//);
//        Literal11.Text = chart.ToHtmlString();

//    }
//    private void Admbatchbar()
//    {

//        DataSet ds = objUACC.Admbatchwisecount();
//        DataTable dt = new DataTable();
//        dt = ds.Tables[0];
//        string[] ar1 = new string[dt.Rows.Count];
//        string[] ar2 = new string[dt.Rows.Count];

//        for (int j = 0; j < dt.Rows.Count; j++)
//        {
//            ar1[j] = dt.Rows[j]["TotalStudents"].ToString();
//            ar2[j] = dt.Rows[j]["ADMBATCH"].ToString();

//        }
//        DotNet.Highcharts.Highcharts chart = new DotNet.Highcharts.Highcharts("chart8")
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
//    Categories = ar2
//})
//            .SetSeries(new DotNet.Highcharts.Options.Series
//            {
//                Name = "Number of Students",
//                Type = ChartTypes.Column,
//                Data = new Data(ar1.Select(q => (object)q).ToArray())

//            }


//);
//        Literal6.Text = chart.ToHtmlString();

//    }



//    private void Feecollectionresult()
//    {
//        int sessionno = Convert.ToInt32(objCommon.LookUp("ACD_DASHBOARD_MASTER", "SESSIONNO", "status=1"));
//        DataSet ds = objUACC.GetFeecollection(sessionno);
//        DataTable dt = new DataTable();
//        dt = ds.Tables[0];
//        string[] ar1 = new string[dt.Rows.Count];
//        string[] ar2 = new string[dt.Rows.Count];

//        for (int j = 0; j < dt.Rows.Count; j++)
//        {
//            ar1[j] = dt.Rows[j]["TOTAL"].ToString();
//            ar2[j] = dt.Rows[j]["MONTH"].ToString();

//        }
//        DotNet.Highcharts.Highcharts chart = new DotNet.Highcharts.Highcharts("chart6")
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
//    Categories = ar2
//})
//            .SetSeries(new DotNet.Highcharts.Options.Series
//            {
//                Name = "Total Amount",
//                Type = ChartTypes.Column,
//                Data = new Data(ar1.Select(q => (object)q).ToArray())

//            }


//);
//        Literal4.Text = chart.ToHtmlString();

//    }
//    private void Feecollectionresultline()
//    {
//        int sessionno = Convert.ToInt32(objCommon.LookUp("ACD_DASHBOARD_MASTER", "SESSIONNO", "status=1"));
//        DataSet ds = objUACC.GetFeecollection(sessionno);
//        Session["feecollection"] = ds;
//        DataTable dt = new DataTable();
//        dt = ds.Tables[0];
//        string[] ar1 = new string[dt.Rows.Count];
//        string[] ar2 = new string[dt.Rows.Count];

//        for (int j = 0; j < dt.Rows.Count; j++)
//        {
//            ar1[j] = dt.Rows[j]["TOTAL"].ToString();
//            ar2[j] = dt.Rows[j]["MONTH"].ToString();

//        }
//        DotNet.Highcharts.Highcharts chart = new DotNet.Highcharts.Highcharts("chart7")
//       .InitChart(new DotNet.Highcharts.Options.Chart { DefaultSeriesType = ChartTypes.Line, Height = 380 })
//        .SetTooltip(new Tooltip { Formatter = "function() { return '<b>'+ this.x +'</b>: '+ this.y ; }" })
//.SetTitle(new DotNet.Highcharts.Options.Title { Text = "" })
//.SetLegend(new DotNet.Highcharts.Options.Legend { Enabled = true })
//.SetPlotOptions(new PlotOptions
//{

//    Line = new PlotOptionsLine
//    {

//        AllowPointSelect = true,
//        DataLabels = new PlotOptionsLineDataLabels
//        {
//            Formatter = "function() { return '<b>'+ this.point.name +'</b>: '+ this.y; }"
//        }
//    }
//})

//.SetXAxis(new XAxis
//{
//    Categories = ar2
//})
//            .SetSeries(new DotNet.Highcharts.Options.Series
//            {
//                Name = "Total Amount",
//                Type = ChartTypes.Line,
//                Data = new Data(ar1.Select(q => (object)q).ToArray())

//            }


//);
//        Literal5.Text = chart.ToHtmlString();

//    }


//    private void Receipttypefee()
//    {
//        int sessionno = Convert.ToInt32(objCommon.LookUp("ACD_DASHBOARD_MASTER", "SESSIONNO", "status=1"));
//        DataSet ds = objUACC.GetFeeReceipttype(sessionno);
//        DataTable dt = new DataTable();
//        dt = ds.Tables[0];
//        string[] ar1 = new string[dt.Rows.Count];
//        string[] ar2 = new string[dt.Rows.Count];

//        for (int j = 0; j < dt.Rows.Count; j++)
//        {
//            ar1[j] = dt.Rows[j]["Total"].ToString();
//            ar2[j] = dt.Rows[j]["RECIEPT_CODE"].ToString();

//        }
//        DotNet.Highcharts.Highcharts chart = new DotNet.Highcharts.Highcharts("chart11")
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
//    Categories = ar2
//})
//            .SetSeries(new DotNet.Highcharts.Options.Series
//            {
//                Name = "Total Amount",
//                Type = ChartTypes.Column,
//                Data = new Data(ar1.Select(q => (object)q).ToArray())

//            }


//);
//        Literal8.Text = chart.ToHtmlString();

//    }
//    private void Feereceipttypebar()
//    {
//        int sessionno = Convert.ToInt32(objCommon.LookUp("ACD_DASHBOARD_MASTER", "SESSIONNO", "status=1"));
//        DataSet ds = objUACC.GetFeeReceipttype(sessionno);
//        DataTable dt = new DataTable();
//        dt = ds.Tables[0];
//        string[] ar1 = new string[dt.Rows.Count];
//        string[] ar2 = new string[dt.Rows.Count];

//        for (int j = 0; j < dt.Rows.Count; j++)
//        {
//            ar1[j] = dt.Rows[j]["Total"].ToString();
//            ar2[j] = dt.Rows[j]["RECIEPT_CODE"].ToString();

//        }
//        DotNet.Highcharts.Highcharts chart = new DotNet.Highcharts.Highcharts("chart12")
//       .InitChart(new DotNet.Highcharts.Options.Chart { DefaultSeriesType = ChartTypes.Line, Height = 380 })
//        .SetTooltip(new Tooltip { Formatter = "function() { return '<b>'+ this.x +'</b>: '+ this.y ; }" })
//.SetTitle(new DotNet.Highcharts.Options.Title { Text = "" })
//.SetLegend(new DotNet.Highcharts.Options.Legend { Enabled = true })
//.SetPlotOptions(new PlotOptions
//{

//    Line = new PlotOptionsLine
//    {

//        AllowPointSelect = true,
//        DataLabels = new PlotOptionsLineDataLabels
//        {
//            Formatter = "function() { return '<b>'+ this.point.name +'</b>: '+ this.y; }"
//        }
//    }
//})

//.SetXAxis(new XAxis
//{
//    Categories = ar2
//})
//            .SetSeries(new DotNet.Highcharts.Options.Series
//            {
//                Name = "Total Amount",
//                Type = ChartTypes.Line,
//                Data = new Data(ar1.Select(q => (object)q).ToArray())

//            }


//);
//        Literal9.Text = chart.ToHtmlString();

//    }

//    #region payrollchart
//    private void EmployeeStaffDetails(int val)
//    {
//        DataSet ds = objUACC.GetEmployeeStaffDetails(val);
//        Session["staffdetails"] = ds;
//        DataTable dt = new DataTable();
//        dt = ds.Tables[0];
//        string[] ar1 = new string[dt.Rows.Count];
//        string[] ar2 = new string[dt.Rows.Count];

//        for (int j = 0; j < dt.Rows.Count; j++)
//        {
//            ar1[j] = dt.Rows[j]["SHOWCOUNT"].ToString();
//            ar2[j] = dt.Rows[j]["SHOWDETAILS"].ToString();

//        }
//        DotNet.Highcharts.Highcharts chart = new DotNet.Highcharts.Highcharts("chart101")
//       .InitChart(new DotNet.Highcharts.Options.Chart { DefaultSeriesType = ChartTypes.Column, Height = 380 })
//        .SetTooltip(new Tooltip { Formatter = "function() { return '<b>'+ this.x +'</b>: '+ this.y ; }" })
//        .SetTitle(new DotNet.Highcharts.Options.Title { Text = "" })
//        .SetLegend(new DotNet.Highcharts.Options.Legend { Enabled = true })
//        .SetPlotOptions(new PlotOptions
//        {

//            Column = new PlotOptionsColumn
//            {

//                AllowPointSelect = true,
//                DataLabels = new PlotOptionsColumnDataLabels
//                {
//                    Formatter = "function() { return '<b>'+ this.point.name +'</b>: '+ this.y; }"
//                }
//            }
//        })

//        .SetXAxis(new XAxis
//        {
//            Categories = ar2
//        })
//                    .SetSeries(new DotNet.Highcharts.Options.Series
//                    {
//                        Name = "STAFF DETAILS",
//                        Type = ChartTypes.Column,
//                        Data = new Data(ar1.Select(q => (object)q).ToArray())

//                    }


//        );
//        Literal17.Text = chart.ToHtmlString();

//    }


//    private void EmployeeJoiningDetails(int val)
//    {
//        DataSet ds = objUACC.GetEmployeeStaffDetails(val);
//        Session["empjoning"] = ds;
//        DataTable dt = new DataTable();
//        dt = ds.Tables[0];
//        string[] ar1 = new string[dt.Rows.Count];
//        string[] ar2 = new string[dt.Rows.Count];

//        for (int j = 0; j < dt.Rows.Count; j++)
//        {
//            ar1[j] = dt.Rows[j]["SHOWCOUNT"].ToString();
//            ar2[j] = dt.Rows[j]["SHOWDETAILS"].ToString();

//        }
//        DotNet.Highcharts.Highcharts chart = new DotNet.Highcharts.Highcharts("chart102")
//       .InitChart(new DotNet.Highcharts.Options.Chart { DefaultSeriesType = ChartTypes.Column, Height = 380 })
//        .SetTooltip(new Tooltip { Formatter = "function() { return '<b>'+ this.x +'</b>: '+ this.y ; }" })
//        .SetTitle(new DotNet.Highcharts.Options.Title { Text = "" })
//        .SetLegend(new DotNet.Highcharts.Options.Legend { Enabled = true })
//        .SetPlotOptions(new PlotOptions
//        {

//            Column = new PlotOptionsColumn
//            {

//                AllowPointSelect = true,
//                DataLabels = new PlotOptionsColumnDataLabels
//                {
//                    Formatter = "function() { return '<b>'+ this.point.name +'</b>: '+ this.y; }"
//                }
//            }
//        })

//        .SetXAxis(new XAxis
//        {
//            Categories = ar2
//        })
//                    .SetSeries(new DotNet.Highcharts.Options.Series
//                    {
//                        Name = "EMPLOYEE JOINING DETAILS",
//                        Type = ChartTypes.Column,
//                        Data = new Data(ar1.Select(q => (object)q).ToArray())

//                    }


//        );
//        Literal19.Text = chart.ToHtmlString();
//    }


//    private void BindEmpoyeeStaffChart(int val)
//    {
//        Random randonGen = new Random();
//        int totEmployee = Convert.ToInt32(objCommon.LookUp("PAYROLL_PAYMAS", "COUNT(IDNO)", "PSTATUS = 'Y'"));
//        lblTotalStaff.Text = totEmployee.ToString();
//        DataSet ds = objUACC.GetEmployeeStaffDetails(val);
//        DataTable ChartData = ds.Tables[0];
//        var list = new List<object[]>();
//        foreach (DataRow dr in ChartData.Rows)
//        {
//            list.Add(new object[]{ 
//                                    dr[0],dr[1]
//                                    });
//        }

//        DotNet.Highcharts.Highcharts chart = new DotNet.Highcharts.Highcharts("chart103")
//        .InitChart(new DotNet.Highcharts.Options.Chart
//        {
//            Height = 380,
//            Options3d = new ChartOptions3d
//            {
//                Enabled = true,
//                Alpha = 45,
//                Beta = 0

//            }


//        })

//        .SetTooltip(new Tooltip { Formatter = "function() { return '<b>'+ this.point.name +'</b>: '+ this.y ; }" })
//         .SetPlotOptions(new PlotOptions
//         {

//             Pie = new PlotOptionsPie
//             {
//                 Depth = 35,
//                 AllowPointSelect = true,
//                 DataLabels = new PlotOptionsPieDataLabels
//                 {
//                     Formatter = "function() { return '<b>'+ this.point.name +'</b>: '+ this.y; }"
//                 }
//             }
//         })

//         .SetTitle(new DotNet.Highcharts.Options.Title { Text = "" })
//         .SetSeries(new DotNet.Highcharts.Options.Series
//         {
//             Type = ChartTypes.Pie,
//             Name = "Browser share",

//             Data = new Data(list.Select(b => new { Name = (object)b.GetValue(0), y = (object)b.GetValue(1) }).ToArray())
//         });

//        Literal16.Text = chart.ToHtmlString();

//    }


//    private void BindEmpoyeeJoiningChart(int val)
//    {
//        Random randonGen = new Random();
//        string FinYear = Convert.ToString(objCommon.LookUp("PAYROLL_PAY_REF", "SUBSTRING(DATENAME(MM, FDATE),1,3)+''+DATENAME(YYYY,  FDATE)+'-'+SUBSTRING(DATENAME(MM, TDATE),1,3)+''+DATENAME(YYYY,  TDATE)", ""));
//        lblEmpFY.Text = FinYear;
//        DataSet ds = objUACC.GetEmployeeStaffDetails(val);
//        DataTable ChartData = ds.Tables[0];
//        var list = new List<object[]>();
//        foreach (DataRow dr in ChartData.Rows)
//        {
//            list.Add(new object[]{ 
//                                    dr[0],dr[1]
//                                    });
//        }

//        DotNet.Highcharts.Highcharts chart = new DotNet.Highcharts.Highcharts("chart104")
//        .InitChart(new DotNet.Highcharts.Options.Chart
//        {
//            Height = 380,
//            Options3d = new ChartOptions3d
//            {
//                Enabled = true,
//                Alpha = 45,
//                Beta = 0

//            }


//        })

//        .SetTooltip(new Tooltip { Formatter = "function() { return '<b>'+ this.point.name +'</b>: '+ this.y ; }" })
//         .SetPlotOptions(new PlotOptions
//         {

//             Pie = new PlotOptionsPie
//             {
//                 Depth = 35,
//                 AllowPointSelect = true,
//                 DataLabels = new PlotOptionsPieDataLabels
//                 {
//                     Formatter = "function() { return '<b>'+ this.point.name +'</b>: '+ this.y; }"
//                 }
//             }
//         })

//         .SetTitle(new DotNet.Highcharts.Options.Title { Text = "" })
//         .SetSeries(new DotNet.Highcharts.Options.Series
//         {
//             Type = ChartTypes.Pie,
//             Name = "Browser share",

//             Data = new Data(list.Select(b => new { Name = (object)b.GetValue(0), y = (object)b.GetValue(1) }).ToArray())
//         });

//        Literal18.Text = chart.ToHtmlString();

//    }

//    private void EmployeeAttendanceDetails(int val)
//    {
//        int TotalShiftAssignEmployee = Convert.ToInt32(objCommon.LookUp("PAYROLL_EMPMAS E INNER JOIN PAYROLL_PAYMAS P ON(E.IDNO=P.IDNO and P.PSTATUS='Y')", "COUNT(E.IDNO)AS TOT_SHIFT_ASSIGN_EMP", "ISNULL(E.SHIFTNO,0)>0 AND ISNULL(E.RELIEVING_DATE,CONVERT(DATE,GETDATE()))>=CONVERT(DATE,GETDATE())"));

//        lblShiftEmp.Text = TotalShiftAssignEmployee.ToString();
//        DateTime currentdate;
//        if (txtEmpAttendanceDate.Text == string.Empty)
//        {
//            currentdate = DateTime.Now;
//        }
//        else
//        {
//            currentdate = Convert.ToDateTime(txtEmpAttendanceDate.Text);
//        }
//        DataSet ds = objUACC.GetEmployeeAttendanceDetails(currentdate, val);
//        Session["shiftemployee"] = ds;
//        DataTable dt = new DataTable();
//        dt = ds.Tables[0];
//        string[] ar1 = new string[dt.Rows.Count];
//        string[] ar2 = new string[dt.Rows.Count];

//        for (int j = 0; j < dt.Rows.Count; j++)
//        {
//            ar1[j] = dt.Rows[j]["TOT_COUNT"].ToString();
//            ar2[j] = dt.Rows[j]["HEADING"].ToString();

//        }
//        DotNet.Highcharts.Highcharts chart = new DotNet.Highcharts.Highcharts("chart105")
//       .InitChart(new DotNet.Highcharts.Options.Chart { DefaultSeriesType = ChartTypes.Column, Height = 380 })
//        .SetTooltip(new Tooltip { Formatter = "function() { return '<b>'+ this.x +'</b>: '+ this.y ; }" })
//        .SetTitle(new DotNet.Highcharts.Options.Title { Text = "" })
//        .SetLegend(new DotNet.Highcharts.Options.Legend { Enabled = true })
//        .SetPlotOptions(new PlotOptions
//        {

//            Column = new PlotOptionsColumn
//            {

//                AllowPointSelect = true,
//                DataLabels = new PlotOptionsColumnDataLabels
//                {
//                    Formatter = "function() { return '<b>'+ this.point.name +'</b>: '+ this.y; }"
//                }
//            }
//        })

//        .SetXAxis(new XAxis
//        {
//            Categories = ar2
//        })
//                    .SetSeries(new DotNet.Highcharts.Options.Series
//                    {
//                        Name = "EMPLOYEE ATTENDANCE DETAILS",
//                        Type = ChartTypes.Column,
//                        Data = new Data(ar1.Select(q => (object)q).ToArray())

//                    }


//        );
//        Literal21.Text = chart.ToHtmlString();
//    }

//    private void BindEmpoyeeAttendanceChart(int val)
//    {
//        Random randonGen = new Random();
//        DateTime currentdate;
//        if (txtEmpAttendanceDate.Text == string.Empty)
//        {
//            currentdate = DateTime.Now;
//        }
//        else
//        {
//            currentdate = Convert.ToDateTime(txtEmpAttendanceDate.Text);
//        }
//        DataSet ds = objUACC.GetEmployeeAttendanceDetails(currentdate, val);
//        DataTable ChartData = ds.Tables[0];
//        var list = new List<object[]>();
//        foreach (DataRow dr in ChartData.Rows)
//        {
//            list.Add(new object[]{ 
//                                    dr[0],dr[1]
//                                    });
//        }

//        DotNet.Highcharts.Highcharts chart = new DotNet.Highcharts.Highcharts("chart106")
//        .InitChart(new DotNet.Highcharts.Options.Chart
//        {
//            Height = 380,
//            Options3d = new ChartOptions3d
//            {
//                Enabled = true,
//                Alpha = 45,
//                Beta = 0

//            }


//        })

//        .SetTooltip(new Tooltip { Formatter = "function() { return '<b>'+ this.point.name +'</b>: '+ this.y ; }" })
//         .SetPlotOptions(new PlotOptions
//         {

//             Pie = new PlotOptionsPie
//             {
//                 Depth = 35,
//                 AllowPointSelect = true,
//                 DataLabels = new PlotOptionsPieDataLabels
//                 {
//                     Formatter = "function() { return '<b>'+ this.point.name +'</b>: '+ this.y; }"
//                 }
//             }
//         })

//         .SetTitle(new DotNet.Highcharts.Options.Title { Text = "" })
//         .SetSeries(new DotNet.Highcharts.Options.Series
//         {
//             Type = ChartTypes.Pie,
//             Name = "Browser share",

//             Data = new Data(list.Select(b => new { Name = (object)b.GetValue(0), y = (object)b.GetValue(1) }).ToArray())
//         });

//        Literal20.Text = chart.ToHtmlString();

//    }

//    protected void txtEmpAttendanceDate_TextChanged(object sender, EventArgs e)
//    {
//        Div26.Visible = true;
//        EmployeeAttendanceDetailsText(50);
//        BindEmpoyeeAttendanceChartText(50);

//    }
//    private void EmployeeAttendanceDetailsText(int val)
//    {
//        int TotalShiftAssignEmployee = Convert.ToInt32(objCommon.LookUp("PAYROLL_EMPMAS E INNER JOIN PAYROLL_PAYMAS P ON(E.IDNO=P.IDNO and P.PSTATUS='Y')", "COUNT(E.IDNO)AS TOT_SHIFT_ASSIGN_EMP", "ISNULL(E.SHIFTNO,0)>0 AND ISNULL(E.RELIEVING_DATE,CONVERT(DATE,GETDATE()))>=CONVERT(DATE,GETDATE())"));

//        lblShiftEmp.Text = TotalShiftAssignEmployee.ToString();
//        DateTime currentdate;
//        if (txtEmpAttendanceDate.Text == string.Empty)
//        {
//            currentdate = DateTime.Now;
//        }
//        else
//        {
//            currentdate = Convert.ToDateTime(txtEmpAttendanceDate.Text);
//        }
//        DataSet ds = objUACC.GetEmployeeAttendanceDetails(currentdate, val);
//        DataTable dt = new DataTable();
//        dt = ds.Tables[0];
//        string[] ar1 = new string[dt.Rows.Count];
//        string[] ar2 = new string[dt.Rows.Count];

//        for (int j = 0; j < dt.Rows.Count; j++)
//        {
//            ar1[j] = dt.Rows[j]["TOT_COUNT"].ToString();
//            ar2[j] = dt.Rows[j]["HEADING"].ToString();

//        }
//        DotNet.Highcharts.Highcharts chart = new DotNet.Highcharts.Highcharts("chart107")
//       .InitChart(new DotNet.Highcharts.Options.Chart { DefaultSeriesType = ChartTypes.Column, Height = 380 })
//        .SetTooltip(new Tooltip { Formatter = "function() { return '<b>'+ this.x +'</b>: '+ this.y ; }" })
//        .SetTitle(new DotNet.Highcharts.Options.Title { Text = "" })
//        .SetLegend(new DotNet.Highcharts.Options.Legend { Enabled = true })
//        .SetPlotOptions(new PlotOptions
//        {

//            Column = new PlotOptionsColumn
//            {

//                AllowPointSelect = true,
//                DataLabels = new PlotOptionsColumnDataLabels
//                {
//                    Formatter = "function() { return '<b>'+ this.point.name +'</b>: '+ this.y; }"
//                }
//            }
//        })

//        .SetXAxis(new XAxis
//        {
//            Categories = ar2
//        })
//                    .SetSeries(new DotNet.Highcharts.Options.Series
//                    {
//                        Name = "EMPLOYEE ATTENDANCE DETAILS",
//                        Type = ChartTypes.Column,
//                        Data = new Data(ar1.Select(q => (object)q).ToArray())

//                    }


//        );
//        Literal21.Text = chart.ToHtmlString();
//    }

//    private void BindEmpoyeeAttendanceChartText(int val)
//    {
//        Random randonGen = new Random();
//        DateTime currentdate;
//        if (txtEmpAttendanceDate.Text == string.Empty)
//        {
//            currentdate = DateTime.Now;
//        }
//        else
//        {
//            currentdate = Convert.ToDateTime(txtEmpAttendanceDate.Text);
//        }
//        DataSet ds = objUACC.GetEmployeeAttendanceDetails(currentdate, val);
//        DataTable ChartData = ds.Tables[0];
//        var list = new List<object[]>();
//        foreach (DataRow dr in ChartData.Rows)
//        {
//            list.Add(new object[]{ 
//                                    dr[0],dr[1]
//                                    });
//        }

//        DotNet.Highcharts.Highcharts chart = new DotNet.Highcharts.Highcharts("chart108")
//        .InitChart(new DotNet.Highcharts.Options.Chart
//        {
//            Height = 380,
//            Options3d = new ChartOptions3d
//            {
//                Enabled = true,
//                Alpha = 45,
//                Beta = 0

//            }


//        })

//        .SetTooltip(new Tooltip { Formatter = "function() { return '<b>'+ this.point.name +'</b>: '+ this.y ; }" })
//         .SetPlotOptions(new PlotOptions
//         {

//             Pie = new PlotOptionsPie
//             {
//                 Depth = 35,
//                 AllowPointSelect = true,
//                 DataLabels = new PlotOptionsPieDataLabels
//                 {
//                     Formatter = "function() { return '<b>'+ this.point.name +'</b>: '+ this.y; }"
//                 }
//             }
//         })

//         .SetTitle(new DotNet.Highcharts.Options.Title { Text = "" })
//         .SetSeries(new DotNet.Highcharts.Options.Series
//         {
//             Type = ChartTypes.Pie,
//             Name = "Browser share",

//             Data = new Data(list.Select(b => new { Name = (object)b.GetValue(0), y = (object)b.GetValue(1) }).ToArray())
//         });

//        Literal20.Text = chart.ToHtmlString();

//    }



//    #endregion


//    private void Totaladmcountyear()
//    {
//        int sessionno = Convert.ToInt32(objCommon.LookUp("ACD_DASHBOARD_MASTER", "SESSIONNO", "status=1"));
//        int idno = Convert.ToInt32(objCommon.LookUp("User_Acc", "isnull(UA_IDNO,0)", "UA_NAME='" + Session["username"].ToString() + "'"));
//        int branchno = Convert.ToInt32(ddlbranch.SelectedValue);
//        DataSet ds2 = objUACC.GetTotaladmcountyear(branchno);
//        Session["yearwise"] = ds2;
//        DataTable dt = new DataTable();
//        dt = ds2.Tables[0];

//        string[] ar1 = new string[dt.Rows.Count];
//        string[] ar2 = new string[dt.Rows.Count];
//        string[] ar3 = new string[dt.Rows.Count];
//        string[] ar4 = new string[dt.Rows.Count];
//        for (int j = 0; j < dt.Rows.Count; j++)
//        {
//            ar1[j] = dt.Rows[j]["year"].ToString();
//            ar2[j] = dt.Rows[j]["TOTAL_COUNT"].ToString();
//            ar3[j] = dt.Rows[j]["MALE_COUNT"].ToString();
//            ar4[j] = dt.Rows[j]["FEMALE_COUNT"].ToString();
//        }
//        DotNet.Highcharts.Highcharts admbatchwisegendercount = new DotNet.Highcharts.Highcharts("admbatchwisegendercount")//Name of the chart
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
//                {
//                    new DotNet.Highcharts.Options.Series {Name="Total Students", Data = new Data(ar2.Select(q => (object)q).ToArray()) },
//                    new DotNet.Highcharts.Options.Series { Name="Male", Data = new Data(ar3.Select(q => (object)q).ToArray()) },
//                    new DotNet.Highcharts.Options.Series { Name="Female", Data = new Data(ar4.Select(q => (object)q).ToArray()) },
                   
//                }

//);
//        Literal30.Text = admbatchwisegendercount.ToHtmlString();

//        DotNet.Highcharts.Highcharts admbatchwisegendercountsecond = new DotNet.Highcharts.Highcharts

//("admbatchwisegendercountsecond")
//      .InitChart(new DotNet.Highcharts.Options.Chart { DefaultSeriesType = ChartTypes.Line, Height = 380 })
//       .SetTooltip(new Tooltip { Formatter = "function() { return '<b>'+ this.x +'</b>: '+ this.y ; }" })
//.SetTitle(new DotNet.Highcharts.Options.Title { Text = "" })
//.SetLegend(new DotNet.Highcharts.Options.Legend { Enabled = true })
//.SetPlotOptions(new PlotOptions
//{

//    Line = new PlotOptionsLine
//    {

//        AllowPointSelect = true,
//        DataLabels = new PlotOptionsLineDataLabels
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
//                {
//                    new DotNet.Highcharts.Options.Series {Name="Total Students", Data = new Data(ar2.Select(q => (object)q).ToArray()) },
//                    new DotNet.Highcharts.Options.Series { Name="Male", Data = new Data(ar3.Select(q => (object)q).ToArray()) },
//                    new DotNet.Highcharts.Options.Series { Name="Female", Data = new Data(ar4.Select(q => (object)q).ToArray()) },
                   
//                }

//);
//        Literal31.Text = admbatchwisegendercountsecond.ToHtmlString();
//    }


//    protected void ddlbranch_SelectedIndexChanged(object sender, EventArgs e)
//    {
//        Totaladmcountyear();
//    }

//    private void Totalfeeamountyearwise()
//    {
//        DataTable dt = new System.Data.DataTable();
//        int branchno = Convert.ToInt32(ddlbranchfees.SelectedValue);
//        DataSet ds1 = objUACC.GetTotalfeeamountyearwise(branchno);
//        Session["feecollection"] = ds1;
//        dt = ds1.Tables[0];

//        string[] ar1 = new string[dt.Rows.Count];
//        string[] ar2 = new string[dt.Rows.Count];
//        string[] ar3 = new string[dt.Rows.Count];
//        string[] ar4 = new string[dt.Rows.Count];

//        for (int j = 0; j < dt.Rows.Count; j++)
//        {
//            ar1[j] = dt.Rows[j]["year"].ToString();
//            ar2[j] = dt.Rows[j]["TOTAL_AMT_D"].ToString();
//            ar3[j] = dt.Rows[j]["TOTAL_AMT"].ToString();
//            ar4[j] = dt.Rows[j]["BAL_AMT"].ToString();

//        }
//        DotNet.Highcharts.Highcharts divmonthwisefeecollection = new DotNet.Highcharts.Highcharts("divmonthwisefeecollection")
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
//            .SetSeries(new[]
//                {
//                    new DotNet.Highcharts.Options.Series {Name="Total Demand", Data = new Data(ar2.Select(q => (object)q).ToArray()) },
//                    new DotNet.Highcharts.Options.Series { Name="Total Paid", Data = new Data(ar3.Select(q => (object)q).ToArray()) },
//                    new DotNet.Highcharts.Options.Series { Name="Total Balance", Data = new Data(ar4.Select(q => (object)q).ToArray()) },
//                }

//);
//        Literal32.Text = divmonthwisefeecollection.ToHtmlString();
//        dt = ds1.Tables[0];
//        for (int j = 0; j < dt.Rows.Count; j++)
//        {
//            ar1[j] = dt.Rows[j]["year"].ToString();
//            ar2[j] = dt.Rows[j]["TOTAL_AMT_D"].ToString();
//            ar3[j] = dt.Rows[j]["TOTAL_AMT"].ToString();
//            ar4[j] = dt.Rows[j]["BAL_AMT"].ToString();

//        }
//        DotNet.Highcharts.Highcharts divmonthwisefeecollectionsecond = new DotNet.Highcharts.Highcharts("divmonthwisefeecollectionsecond")
//       .InitChart(new DotNet.Highcharts.Options.Chart { DefaultSeriesType = ChartTypes.Line, Height = 380 })
//        .SetTooltip(new Tooltip { Formatter = "function() { return '<b>'+ this.x +'</b>: '+ this.y ; }" })
//.SetTitle(new DotNet.Highcharts.Options.Title { Text = "" })
//.SetLegend(new DotNet.Highcharts.Options.Legend { Enabled = true })
//.SetPlotOptions(new PlotOptions
//{

//    Line = new PlotOptionsLine
//    {

//        AllowPointSelect = true,
//        DataLabels = new PlotOptionsLineDataLabels
//        {
//            Formatter = "function() { return '<b>'+ this.point.name +'</b>: '+ this.y; }"
//        }
//    }
//})


//.SetXAxis(new XAxis
//{
//    Categories = ar1
//})
//            .SetSeries(new[]
//                {
//                    new DotNet.Highcharts.Options.Series {Name="Total Demand", Data = new Data(ar2.Select(q => (object)q).ToArray()) },
//                    new DotNet.Highcharts.Options.Series { Name="Total Paid", Data = new Data(ar3.Select(q => (object)q).ToArray()) },
//                    new DotNet.Highcharts.Options.Series { Name="Total Balance", Data = new Data(ar4.Select(q => (object)q).ToArray()) },
                   
//                }

//);
//        Literal33.Text = divmonthwisefeecollectionsecond.ToHtmlString();

//    }


//    protected void ddlbranchfees_SelectedIndexChanged(object sender, EventArgs e)
//    {
//        Totalfeeamountyearwise();
//    }
//    protected void btnimg_Click(object sender, ImageClickEventArgs e)
//    {
//        DataSet ds = new DataSet();
//        ds = (DataSet)Session["statewise"];
//        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
//        {
//            ExportExcel(ds, "UG_Admission_State_Wise_Count");
//        }
//    }
//    private void ExportExcel(DataSet ds, string filename)
//    {
//        //ADDED BY: M. REHBAR SHEIKH 
//        string attachment = "attachment; filename=" + filename + ".xls";
//        Response.ClearContent();
//        Response.AddHeader("content-disposition", attachment);
//        Response.ContentType = "application/" + "ms-excel";
//        StringWriter sw = new StringWriter();
//        HtmlTextWriter htw = new HtmlTextWriter(sw);

//        DataSet dsfee = ds;
//        DataGrid dg = new DataGrid();
//        if (dsfee.Tables.Count > 0)
//        {
//            dg.DataSource = dsfee.Tables[0];
//            dg.DataBind();
//        }
//        dg.HeaderStyle.Font.Bold = true;
//        dg.RenderControl(htw);
//        Response.Write(sw.ToString());
//        Response.End();
//    }
//    protected void btnbranchwise_Click(object sender, ImageClickEventArgs e)
//    {
//        DataSet ds = new DataSet();
//        ds = (DataSet)Session["branchwise"];
//        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
//        {
//            ExportExcel(ds, "Branch_Wise_Count");
//        }
//    }
//    protected void btnFeeCollection_Click(object sender, ImageClickEventArgs e)
//    {
//        DataSet ds = new DataSet();
//        ds = (DataSet)Session["feecollection"];
//        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
//        {
//            ExportExcel(ds, "FeeCollection_Total");
//        }

//    }
//    protected void btnyearwise_Click(object sender, ImageClickEventArgs e)
//    {
//        DataSet ds = new DataSet();
//        ds = (DataSet)Session["yearwise"];
//        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
//        {
//            ExportExcel(ds, "Year_Wise_Count");
//        }
//    }
//    protected void btndegreewise_Click(object sender, ImageClickEventArgs e)
//    {
//        DataSet ds = new DataSet();
//        ds = (DataSet)Session["degreewise"];
//        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
//        {
//            ExportExcel(ds, "Degree_Wise_Count");
//        }
//    }
//    protected void btnEmpjoining_Click(object sender, ImageClickEventArgs e)
//    {
//        DataSet ds = new DataSet();
//        ds = (DataSet)Session["empjoning"];
//        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
//            ExportExcel(ds, "Empolyee_Joning_Details_Count");
//    }
//    protected void btnregistrationcount_Click(object sender, ImageClickEventArgs e)
//    {
//        DataSet ds = new DataSet();
//        ds = (DataSet)Session["admregistration"];
//        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
//        {
//            ExportExcel(ds, "Admission_Registration_Count");
//        }
//    }
//    protected void btncategorywise_Click(object sender, ImageClickEventArgs e)
//    {
//        DataSet ds = new DataSet();
//        ds = (DataSet)Session["categorywise"];
//        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
//        {
//            ExportExcel(ds, "Category_Wise_Registration_Count");
//        }
//    }
//    protected void btnbankwise_Click(object sender, ImageClickEventArgs e)
//    {
//        DataSet ds = new DataSet();
//        ds = (DataSet)Session["bankwise"];
//        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
//        {
//            ExportExcel(ds, "Bank_Wise_DD_Collection");
//        }
//    }
//    protected void feecollection_Click(object sender, ImageClickEventArgs e)
//    {
//        DataSet ds = new DataSet();
//        ds = (DataSet)Session["feecollection"];
//        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
//        {
//            ExportExcel(ds, "Month_Wise_Fee_Collection");
//        }
//    }
//    protected void btnstaffdetails_Click(object sender, ImageClickEventArgs e)
//    {
//        DataSet ds = new DataSet();
//        ds = (DataSet)Session["staffdetails"];
//        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
//        {
//            ExportExcel(ds, "Staff_Detalis");
//        }
//    }
//    protected void btnshiftemployees_Click(object sender, ImageClickEventArgs e)
//    {
//        DataSet ds = new DataSet();
//        ds = (DataSet)Session["shiftemployee"];
//        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
//        {
//            ExportExcel(ds, "Shift_Employees");
//        }
//    }
}
