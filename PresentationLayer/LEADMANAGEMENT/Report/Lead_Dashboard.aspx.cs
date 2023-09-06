
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
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;

public partial class Lead_Dashboard : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    LeadDashboardController objJD = new LeadDashboardController();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //Check Session
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null || Session["coll_name"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            if (!Page.IsPostBack)
            {
                //Page Authorization
                CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                PopulateDropDownList();
                BindChart(Convert.ToInt32(ddlAdmissionBatch.SelectedValue));
            }
        }
        catch (Exception Ex)
        {
            throw new Exception(Ex.Message);
        }
    }

    protected void ddlAdmissionBatch_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        BindChart(Convert.ToInt32(ddlAdmissionBatch.SelectedValue));
    }

    #region Excel Report

    protected void btnSourseWiseEnquiryCount_Click(object sender, ImageClickEventArgs e)
    {
        DataTable ds = new DataTable();
        ds = (DataTable)Session["SourseWiseEnquiry"];
        if (ds.Rows.Count > 0)
        {
            ExportExcel(ds, "Sourse_WiseEnquiry_Count");
        }
    }

    protected void btnEnquiryStatusDoneCount_Click(object sender, EventArgs e)
    {
        DataTable ds = new DataTable();
        ds = (DataTable)Session["EnquiryStatusDoneCount"];
        if (ds.Rows.Count > 0)
        {
            ExportExcel(ds, "Enquiry_Status_Done_Count");
        }
    }

    protected void btnDegreeWiseDoneStatusCount_Click(object sender, EventArgs e)
    {
        DataTable ds = new DataTable();
        ds = (DataTable)Session["DegreeWiseEnquiryDoneStatus"];
        if (ds.Rows.Count > 0)
        {
            ExportExcel(ds, "Degree_Wise_Enquiry_Done_Status_Count");
        }
    }

    protected void btnBranchWiseDoneStatusCount_Click(object sender, EventArgs e)
    {
        DataTable ds = new DataTable();
        ds = (DataTable)Session["BranchWiseEnquiryDoneStatus"];
        if (ds.Rows.Count > 0)
        {
            ExportExcel(ds, "Branch_Wise_Enquiry_Done_Status_Count");
        }
    }

    protected void btnDaillyFollowUpCount_Click(object sender, EventArgs e)
    {
        DataTable ds = new DataTable();
        ds = (DataTable)Session["UserwiseDaillyFollowUp"];
        if (ds.Rows.Count > 0)
        {
            ExportExcel(ds, "Userwise_Dailly_FollowUp_Count");
        }
    }

    protected void btnUserStatuswiseDaillyFollowCount_Click(object sender, EventArgs e)
    {

    }

    #endregion Excel Report


    #region User Define Function

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Lead_Dashboard.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Lead_Dashboard.aspx");
        }
    }

    protected void PopulateDropDownList()
    {
        try
        {
            objCommon.FillDropDownList(ddlAdmissionBatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO > 0", "BATCHNO DESC");
            if (ddlAdmissionBatch.Items.Count > 0)
            {
                ddlAdmissionBatch.SelectedIndex = 1;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Lead_Dashboard.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void BindChart(int AdmissionBatch)
    {
        DataSet ds = objJD.GetLeadDashboardDetail(AdmissionBatch);

        divTotalEnquiry.Visible = false;
        divMale.Visible = false;
        divFemale.Visible = false;
        divOther.Visible = false;

        divSourseWiseEnquiryCount.Visible = false;
        divEnquiryStatusDoneCount.Visible = false;
        divDegreeWiseDoneStatusCount.Visible = false;
        divBranchWiseDoneStatusCount.Visible = false;
        divDaillyFollowUpCount.Visible = false;
        divUserStatuswiseDaillyFollowCount.Visible = false;

        if (ds.Tables[0].Rows.Count > 0)
        {
            lblTotalEnquiry.Text = ds.Tables[0].Rows[0]["TOTAL_ENQUIRY"].ToString();
            lblTotalMale.Text = ds.Tables[0].Rows[0]["MALE_COUNT"].ToString();
            lblTotalFemale.Text = ds.Tables[0].Rows[0]["FEMALE_COUNT"].ToString();
            lblTotalOther.Text = ds.Tables[0].Rows[0]["OTHER_COUNT"].ToString();
            divTotalEnquiry.Visible = true;
            divMale.Visible = true;
            divFemale.Visible = true;
            divOther.Visible = true;
        }

        #region Sourse Wise Enquiry Graph

        if (ds.Tables[1].Rows.Count > 0)
        {
            Session["SourseWiseEnquiry"] = ds.Tables[1];

            DataTable dt2 = new System.Data.DataTable();
            dt2 = ds.Tables[1];

            var list = new List<object[]>();
            foreach (DataRow dr in dt2.Rows)
            {
                list.Add(new object[]{ 
                                    dr[1],dr[0]
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


            //StringBuilder str = new StringBuilder();
            //DataTable dt = new DataTable();

            DataTable dt = new System.Data.DataTable();
            dt = ds.Tables[1];

            string[] ar1 = new string[dt.Rows.Count];
            string[] ar2 = new string[dt.Rows.Count];
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                ar1[j] = dt.Rows[j][1].ToString();
                ar2[j] = dt.Rows[j][0].ToString();
            }

            DotNet.Highcharts.Highcharts chart2 = new DotNet.Highcharts.Highcharts("chart2")
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
                    new DotNet.Highcharts.Options.Series {Name="Sourse Name", Data = new Data(ar1.Select(q => (object)q).ToArray()) },
                    new DotNet.Highcharts.Options.Series { Name="Total Count", Data = new Data(ar2.Select(q => (object)q).ToArray()) },
                }
            );

            Literal1.Text = chart2.ToHtmlString();

            divSourseWiseEnquiryCount.Visible = true;
        }

        #endregion Sourse Wise Enquiry Graph

        #region Enquiry Done with Users

        if (ds.Tables[2].Rows.Count > 0)
        {
            Session["EnquiryStatusDoneCount"] = ds.Tables[2];
            DataTable ChartData = ds.Tables[2];
            var list = new List<object[]>();
            foreach (DataRow dr in ChartData.Rows)
            {
                list.Add(new object[]{ 
                                    dr[1],dr[0]
                                    });
            }

            DotNet.Highcharts.Highcharts chart3 = new DotNet.Highcharts.Highcharts("chart3")
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

            Literal2.Text = chart3.ToHtmlString();


            DataTable dt = new System.Data.DataTable();
            dt = ds.Tables[2];

            string[] ar1 = new string[dt.Rows.Count];
            string[] ar2 = new string[dt.Rows.Count];
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                ar1[j] = dt.Rows[j][1].ToString();
                ar2[j] = dt.Rows[j][0].ToString();
            }

            DotNet.Highcharts.Highcharts chart4 = new DotNet.Highcharts.Highcharts("chart4")
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
                    new DotNet.Highcharts.Options.Series {Name="Enquiry Status Done User", Data = new Data(ar1.Select(q => (object)q).ToArray()) },
                    new DotNet.Highcharts.Options.Series { Name="Total Count", Data = new Data(ar2.Select(q => (object)q).ToArray()) },
                }
            );

            Literal3.Text = chart4.ToHtmlString();
            divEnquiryStatusDoneCount.Visible = true;
        }

        #endregion Enquiry Done with Users

        #region Degree Wise Enquiry Done Status

        if (ds.Tables[3].Rows.Count > 0)
        {
            Session["DegreeWiseEnquiryDoneStatus"] = ds.Tables[3];
            DataTable ChartData = ds.Tables[3];
            var list = new List<object[]>();
            foreach (DataRow dr in ChartData.Rows)
            {
                list.Add(new object[]{ 
                                    dr[1],dr[0]
                                    });
            }

            DotNet.Highcharts.Highcharts chart5 = new DotNet.Highcharts.Highcharts("chart5")
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

            Literal4.Text = chart5.ToHtmlString();


            DataTable dt = new System.Data.DataTable();
            dt = ds.Tables[3];

            string[] ar1 = new string[dt.Rows.Count];
            string[] ar2 = new string[dt.Rows.Count];
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                ar1[j] = dt.Rows[j][1].ToString();
                ar2[j] = dt.Rows[j][0].ToString();
            }

            DotNet.Highcharts.Highcharts chart6 = new DotNet.Highcharts.Highcharts("chart6")
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
                    new DotNet.Highcharts.Options.Series {Name="Degree Name", Data = new Data(ar1.Select(q => (object)q).ToArray()) },
                    new DotNet.Highcharts.Options.Series { Name="Total Count", Data = new Data(ar2.Select(q => (object)q).ToArray()) },
                }
            );

            Literal5.Text = chart6.ToHtmlString();

            dt = ds.Tables[3];

            string[] arr1 = new string[dt.Rows.Count];
            string[] arr2 = new string[dt.Rows.Count];
            //string[] ar3 = new string[dt.Rows.Count];
            //string[] ar4 = new string[dt.Rows.Count];
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                arr1[j] = dt.Rows[j][1].ToString();
                arr2[j] = dt.Rows[j][0].ToString();
            }


            DotNet.Highcharts.Highcharts chart7 = new DotNet.Highcharts.Highcharts("chart7")
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
                            new DotNet.Highcharts.Options.Series {Name="Total", Data = new Data(arr2.Select(q => (object)q).ToArray()) },
                            //new DotNet.Highcharts.Options.Series { Name="Male", Data = new Data(ar3.Select(q => (object)q).ToArray()) },
                            //new DotNet.Highcharts.Options.Series { Name="Female", Data = new Data(ar4.Select(q => (object)q).ToArray()) },
                           
                        }

            );
            Literal6.Text = chart7.ToHtmlString();
            divDegreeWiseDoneStatusCount.Visible = true;
        }



        #endregion Degree Wise Enquiry Done Status

        #region Branch Wise Enquiry Done Status

        if (ds.Tables[4].Rows.Count > 0)
        {
            Session["BranchWiseEnquiryDoneStatus"] = ds.Tables[4];
            DataTable ChartData = ds.Tables[4];
            var list = new List<object[]>();
            foreach (DataRow dr in ChartData.Rows)
            {
                list.Add(new object[]{ 
                                    dr[1],dr[0]
                                    });
            }

            DotNet.Highcharts.Highcharts chart8 = new DotNet.Highcharts.Highcharts("chart8")
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

            Literal7.Text = chart8.ToHtmlString();


            DataTable dt = new System.Data.DataTable();
            dt = ds.Tables[4];

            string[] ar1 = new string[dt.Rows.Count];
            string[] ar2 = new string[dt.Rows.Count];
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                ar1[j] = dt.Rows[j][1].ToString();
                ar2[j] = dt.Rows[j][0].ToString();
            }

            DotNet.Highcharts.Highcharts chart9 = new DotNet.Highcharts.Highcharts("chart9")
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
                    new DotNet.Highcharts.Options.Series {Name="Branch Name", Data = new Data(ar1.Select(q => (object)q).ToArray()) },
                    new DotNet.Highcharts.Options.Series { Name="Total Count", Data = new Data(ar2.Select(q => (object)q).ToArray()) },
                }
            );

            Literal8.Text = chart9.ToHtmlString();

            dt = ds.Tables[4];

            string[] arr1 = new string[dt.Rows.Count];
            string[] arr2 = new string[dt.Rows.Count];
            //string[] ar3 = new string[dt.Rows.Count];
            //string[] ar4 = new string[dt.Rows.Count];
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                arr1[j] = dt.Rows[j][1].ToString();
                arr2[j] = dt.Rows[j][0].ToString();
            }


            DotNet.Highcharts.Highcharts chart10 = new DotNet.Highcharts.Highcharts("chart10")
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
                            new DotNet.Highcharts.Options.Series {Name="Total", Data = new Data(arr2.Select(q => (object)q).ToArray()) },
                            
                        }

            );
            Literal9.Text = chart10.ToHtmlString();
            divBranchWiseDoneStatusCount.Visible = true;
        }
        #endregion Branch Wise Enquiry Done Status

        #region User Wise Dailly Follow UP

        if (ds.Tables[5].Rows.Count > 0)
        {
            Session["UserwiseDaillyFollowUp"] = ds.Tables[5];
            DataTable ChartData = ds.Tables[5];
            var list = new List<object[]>();
            foreach (DataRow dr in ChartData.Rows)
            {
                list.Add(new object[]{ 
                                    dr[1],dr[0]
                                    });
            }

            DotNet.Highcharts.Highcharts chart11 = new DotNet.Highcharts.Highcharts("chart11")
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

            Literal10.Text = chart11.ToHtmlString();


            DataTable dt = new System.Data.DataTable();
            dt = ds.Tables[5];

            string[] ar1 = new string[dt.Rows.Count];
            string[] ar2 = new string[dt.Rows.Count];
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                ar1[j] = dt.Rows[j][1].ToString();
                ar2[j] = dt.Rows[j][0].ToString();
            }

            DotNet.Highcharts.Highcharts chart12 = new DotNet.Highcharts.Highcharts("chart12")
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
                    new DotNet.Highcharts.Options.Series {Name="User Name", Data = new Data(ar1.Select(q => (object)q).ToArray()) },
                    new DotNet.Highcharts.Options.Series { Name="Equiry Count", Data = new Data(ar2.Select(q => (object)q).ToArray()) },
                }
            );

            Literal11.Text = chart12.ToHtmlString();

            dt = ds.Tables[5];

            string[] arr1 = new string[dt.Rows.Count];
            string[] arr2 = new string[dt.Rows.Count];
            //string[] ar3 = new string[dt.Rows.Count];
            //string[] ar4 = new string[dt.Rows.Count];
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                arr1[j] = dt.Rows[j][1].ToString();
                arr2[j] = dt.Rows[j][0].ToString();
            }


            DotNet.Highcharts.Highcharts chart13 = new DotNet.Highcharts.Highcharts("chart13")
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
                            new DotNet.Highcharts.Options.Series {Name="Total", Data = new Data(arr2.Select(q => (object)q).ToArray()) },
                            
                        }

            );
            Literal12.Text = chart13.ToHtmlString();
            divDaillyFollowUpCount.Visible = true;
        }
        #endregion User Wise Dailly Follow UP

        #region User Status Wise Dailly Follow UP

        if (ds.Tables[6].Rows.Count > 0)
        {
            Session["UserStatuswiseDaillyFollowUp"] = ds.Tables[6];
            DataTable ChartData = ds.Tables[6];
            var list = new List<object[]>();
            foreach (DataRow dr in ChartData.Rows)
            {
                list.Add(new object[]{ 
                                    dr[1],dr[0]
                                    });
            }

            DotNet.Highcharts.Highcharts chart14 = new DotNet.Highcharts.Highcharts("chart14")
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

            Literal13.Text = chart14.ToHtmlString();


            DataTable dt = new System.Data.DataTable();
            dt = ds.Tables[6];

            string[] ar1 = new string[dt.Rows.Count];
            string[] ar2 = new string[dt.Rows.Count];
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                ar1[j] = dt.Rows[j][1].ToString();
                ar2[j] = dt.Rows[j][0].ToString();
            }

            DotNet.Highcharts.Highcharts chart15 = new DotNet.Highcharts.Highcharts("chart15")
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
                    new DotNet.Highcharts.Options.Series {Name="User & Status Name", Data = new Data(ar1.Select(q => (object)q).ToArray()) },
                    new DotNet.Highcharts.Options.Series { Name="Equiry Count", Data = new Data(ar2.Select(q => (object)q).ToArray()) },
                }
            );

            Literal14.Text = chart15.ToHtmlString();

            dt = ds.Tables[6];

            string[] arr1 = new string[dt.Rows.Count];
            string[] arr2 = new string[dt.Rows.Count];
            //string[] ar3 = new string[dt.Rows.Count];
            //string[] ar4 = new string[dt.Rows.Count];
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                arr1[j] = dt.Rows[j][1].ToString();
                arr2[j] = dt.Rows[j][0].ToString();
            }


            DotNet.Highcharts.Highcharts chart16= new DotNet.Highcharts.Highcharts("chart16")
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
                            new DotNet.Highcharts.Options.Series {Name="Total", Data = new Data(arr2.Select(q => (object)q).ToArray()) },
                            
                        }

            );
            Literal15.Text = chart16.ToHtmlString();
            divUserStatuswiseDaillyFollowCount.Visible = true;
        }
        #endregion User StatusWise Dailly Follow UP

    }

    private void ExportExcel(DataTable ds, string filename)
    {
        string attachment = "attachment; filename=" + filename + ".xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/" + "ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);

        DataTable dsfee = ds;
        DataGrid dg = new DataGrid();
        if (dsfee.Rows.Count > 0)
        {
            dg.DataSource = dsfee;
            dg.DataBind();
        }
        dg.HeaderStyle.Font.Bold = true;
        dg.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();
    }

    #endregion User Define Function
}