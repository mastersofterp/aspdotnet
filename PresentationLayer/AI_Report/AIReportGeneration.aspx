<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/SiteMasterPage.master" CodeFile="AIReportGeneration.aspx.cs" Inherits="AI_Report_AIReportGeneration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="Plugins/bootstrap-table/FilterTable/main.css" rel="stylesheet"  />
     <link href="<%=Page.ResolveClientUrl("Plugins/bootstrap-table/FilterTable/main.css")%>" rel="stylesheet" />
     <link href="<%=Page.ResolveClientUrl("Plugins/pivottable/dist/pivot.css")%>" rel="stylesheet" />
     <link href="<%=Page.ResolveClientUrl("~/plugins/Community/css/iziToast.min.css")%>" rel="stylesheet" />
         <script src="<%=Page.ResolveClientUrl("Plugins/bootstrap-table/FilterTable/AI_Generation_CustomFilter.js")%>" ></script>
      <link href="<%=Page.ResolveClientUrl("Plugins/bootstrap-table/dist/bootstrap-table.min.css")%>" rel="stylesheet" />
      <script src="<%=Page.ResolveClientUrl("https://code.jquery.com/ui/1.12.1/jquery-ui.js")%>" ></script>
    <script src="<%=Page.ResolveClientUrl("Plugins/bootstrap-table/dist/bootstrap-table.min.js")%>" ></script>
     <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-table/1.18.3/extensions/export/bootstrap-table-export.min.js" integrity="sha512-cAMZL39BuY4jWHUkLWRS+TlHzd/riowdz6RNNVI6CdKRQw1p1rDn8n34lu6pricfL0i8YXeWQIDF5Xa/HBVLRg==" crossorigin="anonymous" referrerpolicy="no-referrer" ></script>
    <script src="<%=Page.ResolveClientUrl("Plugins/bootstrap-table/dist/extensions/export/libs/FileSaver/FileSaver.min.js")%>" ></script>
    <script src="<%=Page.ResolveClientUrl("Plugins/bootstrap-table/dist/extensions/export/libs/es6-promise/es6-promise.auto.min.js")%>" ></script>
    <script src="<%=Page.ResolveClientUrl("Plugins/bootstrap-table/dist/extensions/export/libs/js-xlsx/xlsx.core.min.js")%>" ></script>
    <script src="<%=Page.ResolveClientUrl("Plugins/bootstrap-table/dist/extensions/export/tableExport.min.js")%>" ></script>

    <script src="<%=Page.ResolveClientUrl("Plugins/pivottable/dist/pivot.js")%>" ></script>
    <script src="<%=Page.ResolveClientUrl("Plugins/jquery.ui.touch-punch.min.js")%>" ></script>
    <script src="<%=Page.ResolveClientUrl("Plugins/poulty-basic.js")%>" ></script>
    <script src="<%=Page.ResolveClientUrl("Plugins/pivottable/dist/plotly_renderers.js")%>" ></script>
    <script src="<%=Page.ResolveClientUrl("Plugins/table2excel/jquery.table2excel.js")%>" ></script>
     
    <script src="<%=Page.ResolveClientUrl("AiReportGeneration.js")%>" ></script>
        <script src="<%=Page.ResolveClientUrl("~/plugins/Community/js/iziToast.min.js")%>"></script>
    

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <%--<h3 class="box-title">SESSION CREATION</h3>--%>
                    <h3 class="box-title">Advanced AI Reporting</h3>
                </div>
                <div class="box-body">
                    <div class="col-12">
                        <div class="row">
                            <div class="col-lg-4 col-md-4 col-12">
                                <div class="form-group">
                                    <label><sup>*</sup>Report Type <code></code></label>
                                    <select id="ddlReportType" class="form-control select2 required-select2-validation" name="ddlReportType" tabindex="1">
                                        <option value="0">Please Select</option>
                                        <option value="1">Student Information</option>
                                        <%--<option value="2">Faculty Information</option>
                                        <option value="3">Fees Information</option>--%>
                                    </select>
                                </div>
                            </div>
                            <div class="col-lg-8 col-md-8 col-12">
                                <div class="form-group">
                                    <div id="report-desc"></div>
                                </div>
                            </div>
                            <div class="col-lg-12 col-md-12 col-12 h">
                                <div class="chat-textarea" style="max-height: 260px; overflow-x: auto;">
                                    <div class="d-flex align-items-center">
                                        <div class="form-group w-100">
                                            <label><sup></sup>Chat With System<code></code></label>
                                            <textarea class="form-control" id="txtChat1" name="txtChat" tabindex="1" spellcheck="true" placeholder="Enter Your Query "></textarea>
                                        </div>
                                        <button type="button" tabindex="1" class="btn btn-primary ms-2" id="btnSend1">Send</button>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-12 col-md-12 col-12 h">
                                <div class="table-responsive mt-3" id="theTable">
                                    <div id="toolbar" class='tableToolbarLeft'>
                                    </div>
                                    <table id="resultTable" class="table table-striped table-bordered display">
                                        <thead>
                                            <tr id="tableColumns">
                                            </tr>
                                        </thead>
                                        <tbody id="tableContent">
                                        </tbody>

                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
