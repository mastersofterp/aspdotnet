<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="LeaveChart.aspx.cs" Inherits="ESTABLISHMENT_LEAVES_Transactions_LeaveChart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.3/font/bootstrap-icons.min.css">

    
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">CURRENT SESSION LEAVE DASHBOARD</h3>
                </div>
                <div class="box-body">
                    <div class="row">
                        <%--<div class="form-group col-lg-3 col-md-6 col-12">
                            <div class="label-dynamic">

                                <label>Dashboard</label>
                            </div>
                            <select id="ddlDashborad" class="form-control" tabindex="1">
                                <option value="0">Employee info DashBoard</option>
                                <option value="1">Employee Salary Dashboard</option>
                            </select>

                        </div>--%>
                    </div>

                    <div class="row" id="card-area"></div>
                    <div class="row">
                        <div class="col-xl-2 col-lg-2 col-12 in-left a3" id="filter-area">
                            <%--     area1--%>
                        </div>

                        <div class="col-xl-10 col-lg-10 col-12 in-right a3">
                            <div class="row" id="graph-plot">
                                <%--    area2--%>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>




    <script src="<%=Page.ResolveClientUrl("~/plugins/dynamic-graph/alasql/dist/alasql.min.js")%>"></script>

    <!-- For Chart JS -->
    <script src="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/3.8.0/chart.min.js" integrity="sha512-sW/w8s4RWTdFFSduOTGtk4isV1+190E/GghVffMA9XczdJ2MDzSzLEubKAs5h0wzgSJOQTRYyaz73L3d6RtJSg==" crossorigin="anonymous" referrerpolicy="no-referrer"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/chartjs-plugin-datalabels/2.0.0/chartjs-plugin-datalabels.min.js" integrity="sha512-R/QOHLpV1Ggq22vfDAWYOaMd5RopHrJNMxi8/lJu8Oihwi4Ho4BRFeiMiCefn9rasajKjnx9/fTQ/xkWnkDACg==" crossorigin="anonymous" referrerpolicy="no-referrer"></script>

    <script src="<%=Page.ResolveClientUrl("~/plugins/custom/js/ESTABLISHMENT/TRANSACTIONS/LeaveCharts.js")%>"></script>

    <script src="<%=Page.ResolveClientUrl("~/plugins/dynamic-graph/graph-library.js")%>"></script>

   

</asp:Content>


