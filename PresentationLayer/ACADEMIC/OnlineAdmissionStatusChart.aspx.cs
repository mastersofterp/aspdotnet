using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ACADEMIC_OnlineAdmissionStatusChart : System.Web.UI.Page
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

                string batch = objCommon.LookUp("ACD_DASHBOARD_MASTER_ONLINE_ADM", "BATCHNO", "status=1");
                int batchno = Convert.ToInt32(batch == "" ? "0" : batch);
                string batchname = objCommon.LookUp("ACD_ADMBATCH", "BATCHNAME", "BATCHNO=" + batchno);
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

                    else if (val == 5)
                    {
                        DataSet ds1 = objUACC.GetallDashboard_onlineadm(val, batchno);
                        if (ds1 != null && ds1.Tables.Count > 0)
                        {
                            if (ds1.Tables[0].Rows.Count > 0)
                            {
                                lbladmbatch10.Text = batchname;
                                lblTotalFeeNotpaid.Text = ds1.Tables[0].Rows[0][0].ToString();
                            }
                        }
                        TotalAppFeeNotPaid.Visible = true;
                    }

                    else if (val == 7)
                    {
                        DataSet ds1 = objUACC.GetallDashboard_onlineadm(val, batchno);
                        if (ds1 != null && ds1.Tables.Count > 0)
                        {
                            if (ds1.Tables[0].Rows.Count > 0)
                            {
                                lbladmbatch5.Text = batchname;
                                lblTotalFeepaid.Text = ds1.Tables[0].Rows[0][0].ToString();
                            }
                        }
                        TotalAppFeePaid.Visible = true;
                    }
                    else if (val == 11)
                    {
                        DataSet ds1 = objUACC.GetallDashboard_onlineadm(val, batchno);
                        if (ds1 != null && ds1.Tables.Count > 0)
                        {
                            if (ds1.Tables[0].Rows.Count > 0)
                            {
                                lbladmbatch13.Text = batchname;
                                lbltotalProvFeePaid.Text = ds1.Tables[0].Rows[0][0].ToString();
                            }
                        }
                        totProvFeePaid.Visible = true;
                    }
                    else if (val == 12)
                    {
                        DataSet ds1 = objUACC.GetallDashboard_onlineadm(val, batchno);
                        if (ds1 != null && ds1.Tables.Count > 0)
                        {
                            if (ds1.Tables[0].Rows.Count > 0)
                            {
                                lbladmbatch12.Text = batchname;
                                lbltotalDetailsNotCompleted.Text = ds1.Tables[0].Rows[0][0].ToString();
                            }
                        }
                        totDetailsNotCompleted.Visible = true;
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
              Options3d = new ChartOptions3d
              {
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
            ar2[j] = dt.Rows[j][1].ToString();
        }

    }

    private void Bindchart()
    {
        Random randonGen = new Random();
        DataSet ds = objCommon.FillDropDown("ACD_USER_REGISTRATION R INNER JOIN ACD_STUDENT_ONLINE_ADM NR ON NR.IDNO=R.USERNO inner join ACD_USER_PROFILE_STATUS C on C.USERNO = NR.IDNO INNER JOIN ACD_USER_BRANCH_PREF BP ON BP.USERNO = NR.IDNO INNER JOIN ACD_DEGREE ADG ON ADG.DEGREENO = BP.DEGREENO", "ADG.CODE", " COUNT(R.USERNO)  AS PERCENTAGE", "isnull(CONFIRM_STATUS,0)=1 AND R.ADMBATCH =" + ViewState["batchno"] + " GROUP BY ADG.DEGREENO,CODE", "CODE");
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

    protected void txtEmpAttendanceDate_TextChanged(object sender, EventArgs e)
    {

    }
    protected void btnimg_Click(object sender, ImageClickEventArgs e)
    {
        DataSet ds = new DataSet();
        ds = (DataSet)Session["statewise"];
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            ExportExcel(ds, "State_Wise_Count");
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
    protected void btnregistrationcount_Click(object sender, ImageClickEventArgs e)
    {
        DataSet ds = new DataSet();
        ds = (DataSet)Session["DateWiseadmregistration"];
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            ExportExcel(ds, "Date_Wise_Count");
        }
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
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
}