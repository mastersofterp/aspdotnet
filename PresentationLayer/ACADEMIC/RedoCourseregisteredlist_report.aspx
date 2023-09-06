<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="RedoCourseregisteredlist_report.aspx.cs" Inherits="ACADEMIC_RedoCourseregisteredlist_report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../../../plugins/multiselect/bootstrap-multiselect.css" rel="stylesheet" />
    <script src="../../../plugins/multiselect/bootstrap-multiselect.js"></script>
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updpnl_details"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div id="preloader">
                    <div id="loader-img">
                        <div id="loader">
                        </div>
                        <p class="saving">Loading<span>.</span><span>.</span><span>.</span></p>
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <asp:UpdatePanel ID="updpnl_details" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <label>REDO COURSE REGISTRATION LIST REPORT</label></h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="col-12">
                                    <div class="row">

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>College/Session</label>
                                            </div>
                                            <asp:ListBox ID="ddlCollege" runat="server" AppendDataBoundItems="true" ValidationGroup="Excel" TabIndex="1"
                                                CssClass="form-control multi-select-demo" SelectionMode="multiple" AutoPostBack="false"></asp:ListBox>

                                            <%--  <asp:DropDownList ID="ddlCollege" runat="server" AppendDataBoundItems="true" AutoPostBack="True" CssClass="form-control" TabIndex="1"
                                                data-select2-enable="true" ValidationGroup="Excel">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>--%>
                                        </div>
                                        <div class="col-12 btn-footer">
                                            <asp:Button ID="btnExcel" runat="Server" ToolTip="Excel" Text="Excel Report" TabIndex="3" ValidationGroup="Excel" OnClick="btnExcel_Click" OnClientClick="return Validate()"
                                                CssClass="btn btn-info" />
                                            <asp:Button ID="btnCancel" runat="server" ToolTip="Cancel" Text="Cancel" TabIndex="4" CssClass="btn btn-warning" OnClick="btnCancel_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExcel" />
        </Triggers>
    </asp:UpdatePanel>
    <div id="divMsg" runat="server">
    </div>
    <script type="text/javascript">
        function Validate() {
            var ddlcollege = document.getElementById("<%=ddlCollege.ClientID %>");
         if (ddlcollege.value == "") {
             //If the "Please Select" option is selected display error.
             alert("Please Select Session!");
             return false;
         }
         return true;
     }
    </script>

    <script type="text/javascript">
        $(document).ready(function () {
            $('.multi-select-demo').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200,
                enableFiltering: true,
                filterPlaceholder: 'Search',
                enableCaseInsensitiveFiltering: true,
            });
        });
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                $('.multi-select-demo').multiselect({
                    includeSelectAllOption: true,
                    maxHeight: 200,
                    enableFiltering: true,
                    filterPlaceholder: 'Search',
                    enableCaseInsensitiveFiltering: true,
                });
            });
        });
    </script>
</asp:Content>

