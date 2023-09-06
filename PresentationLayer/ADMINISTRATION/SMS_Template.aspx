<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="SMS_Template.aspx.cs" Inherits="ADMINISTRATION_SMS_Template" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:HiddenField ID="hfdStat" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfSmsStatus" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfdTemplateType" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfdSmsType" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hidTAB" runat="server" Value="tab_1" />
    <link href='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.css") %>' rel="stylesheet" />
    <script src='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.js") %>'></script>

    <div>
        <asp:UpdateProgress ID="updprogtemp" runat="server" AssociatedUpdatePanelID="updTemplate"
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

    <style>
        .multiselect-container {
            position: absolute;
            transform: translate3d(0px, -46px, 0px);
            top: 0px;
            left: 0px;
            will-change: transform;
            height: 200px;
            overflow: auto;
        }
    </style>
    <script>
        $(document).ready(function () {
            var table = $('#tblSms').DataTable({
                responsive: true,
                lengthChange: true,
                scrollY: 320,
                scrollX: true,
                scrollCollapse: true,
                paging: false,

                dom: 'lBfrtip',
                buttons: [
                    {
                        extend: 'colvis',
                        text: 'Column Visibility',
                        columns: function (idx, data, node) {
                            var arr = [0];
                            if (arr.indexOf(idx) !== -1) {
                                return false;
                            } else {
                                return $('#tblSms').DataTable().column(idx).visible();
                            }
                        }
                    },
                    {
                        extend: 'collection',
                        text: '<i class="glyphicon glyphicon-export icon-share"></i> Export',
                        buttons: [
                                {
                                    extend: 'copyHtml5',
                                    exportOptions: {
                                        columns: function (idx, data, node) {
                                            var arr = [0];
                                            if (arr.indexOf(idx) !== -1) {
                                                return false;
                                            } else {
                                                return $('#tblSms').DataTable().column(idx).visible();
                                            }
                                        }
                                    }
                                },
                                {
                                    extend: 'excelHtml5',
                                    exportOptions: {
                                        columns: function (idx, data, node) {
                                            var arr = [0];
                                            if (arr.indexOf(idx) !== -1) {
                                                return false;
                                            } else {
                                                return $('#tblSms').DataTable().column(idx).visible();
                                            }
                                        }
                                    }
                                },
                        ]
                    }
                ],
                "bDestroy": true,
            });
        });
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                var table = $('#tblSms').DataTable({
                    responsive: true,
                    lengthChange: true,
                    scrollY: 320,
                    scrollX: true,
                    scrollCollapse: true,
                    paging: false,

                    dom: 'lBfrtip',
                    buttons: [
                        {
                            extend: 'colvis',
                            text: 'Column Visibility',
                            columns: function (idx, data, node) {
                                var arr = [0];
                                if (arr.indexOf(idx) !== -1) {
                                    return false;
                                } else {
                                    return $('#tblSms').DataTable().column(idx).visible();
                                }
                            }
                        },
                        {
                            extend: 'collection',
                            text: '<i class="glyphicon glyphicon-export icon-share"></i> Export',
                            buttons: [
                                    {
                                        extend: 'copyHtml5',
                                        exportOptions: {
                                            columns: function (idx, data, node) {
                                                var arr = [0];
                                                if (arr.indexOf(idx) !== -1) {
                                                    return false;
                                                } else {
                                                    return $('#tblSms').DataTable().column(idx).visible();
                                                }
                                            }
                                        }
                                    },
                                    {
                                        extend: 'excelHtml5',
                                        exportOptions: {
                                            columns: function (idx, data, node) {
                                                var arr = [0];
                                                if (arr.indexOf(idx) !== -1) {
                                                    return false;
                                                } else {
                                                    return $('#tblSms').DataTable().column(idx).visible();
                                                }
                                            }
                                        }
                                    },
                            ]
                        }
                    ],
                    "bDestroy": true,
                });
            });
        });

    </script>
    <script>

        $(document).ready(function () {
            $(".multi-select-demo").multiselect({
                includeSelectAllOption: true,
                enableFiltering: true,
                filterPlaceholder: 'Search',
                enableCaseInsensitiveFiltering: true,
                enableHTML: true,
                templates: {
                    filter: '<li class="multiselect-item multiselect-filter"><div class="input-group mb-3"><div class="input-group-prepend"><span class="input-group-text"><i class="fa fa-search"></i></span></div><input class="form-control multiselect-search" type="text" /></div></li>',
                    filterClearBtn: '<span class="input-group-btn"><button class="btn btn-default multiselect-clear-filter" style="height: 33px;" type="button"><i style="margin-right: 4px;" class="fa fa-eraser"></i></button></span>'
                }
            });
        });
        $(document).ready(function () {
            $('.multi-select-demo').multiselect();
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                $('.multi-select-demo').multiselect({
                    includeSelectAllOption: true,
                    enableFiltering: true,
                    filterPlaceholder: 'Search',
                    enableCaseInsensitiveFiltering: true,
                    enableHTML: true,
                    templates: {
                        filter: '<li class="multiselect-item multiselect-filter"><div class="input-group mb-3"><div class="input-group-prepend"><span class="input-group-text"><i class="fa fa-search"></i></span></div><input class="form-control multiselect-search" type="text" /></div></li>',
                        filterClearBtn: '<span class="input-group-btn"><button class="btn btn-default multiselect-clear-filter" style="height: 33px;" type="button"><i style="margin-right: 4px;" class="fa fa-eraser"></i></button></span>'
                    }

                });

            });
        });
    </script>
    <script type="text/javascript">
        $(function () {
            var tabName = $("[id*=Tabid]").val() != "" ? $("[id*=Tabid]").val() : "tab_1";
            $('#Tabs a[href="#' + tabName + '"]').tab('show');
            $("#Tabs a").click(function () {
                $("[id*=Tabid]").val($(this).attr("href").replace("#", ""));

            });
        });

    </script>
    <script>
        function SetStatTemType(val) {
            $('#rdActiveTemType').prop('checked', val);
        }
        function validate() {

            $('#hfdStat').val($('#rdActiveTemType').prop('checked'));
            var idtxtweb = $("[id$=txtTemplateType]").attr("id");
            var txtweb = document.getElementById(idtxtweb);
            if (txtweb.value.length == 0) {
                alert('Please Enter Template Type', 'Warning!');

                $(txtweb).focus();
                return false;
            }


            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                $(function () {
                    $('#btnSave').click(function () {
                        validate();
                    });
                });
            });
        }
    </script>
    <script>
        function SetSmsStatTemType(val) {
            $('#rdActiveSmsTemType').prop('checked', val);
        }


        function smsvalidate() {

            $('#hfSmsStatus').val($('#rdActiveSmsTemType').prop('checked'));

            var idtxtweb = $("[id$=ddlTemplateType]").attr("id");
            var txtweb = document.getElementById(idtxtweb);
            if (txtweb.value == 0) {
                alert('Please Select Template Type', 'Warning!');

                $(txtweb).focus();
                return false;
            }


            var idtxtTemplateName = $("[id$=txtTemplateName]").attr("id");
            var txtTemplateName = document.getElementById(idtxtTemplateName);
            if (txtTemplateName.value.length == 0) {
                alert('Please Enter Template Name', 'Warning!');

                $(txtTemplateName).focus();
                return false;
            }

            var idPageName = $("[id$=lstbxPageName]").attr("id");
            var PageName = document.getElementById(idPageName);
            if (PageName.value == 0) {
                alert('Please Select Page Name', 'Warning!');

                $(PageName).focus();
                return false;
            }

            var idtxtId = $("[id$=txtTemplateId]").attr("id");
            var txtId = document.getElementById(idtxtId);
            if (txtId.value.length == 0) {
                alert('Please Enter Template Id', 'Warning!');

                $(txtId).focus();
                return false;
            }


            var idtxtTemplate = $("[id$=txtTemplate]").attr("id");
            var txtTemplate = document.getElementById(idtxtTemplate);
            if (txtTemplate.value.length == 0) {
                alert('Please Enter Template', 'Warning!');

                $(txtTemplate).focus();
                return false;
            }
        }


        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnSubmit').click(function () {
                    smsvalidate();
                });
            });
        });


    </script>
    <script>
        function getVal() {

            var array = []
            var al_no = '';
            var checkboxes = document.querySelectorAll('input[type=checkbox]:checked')

            for (var i = 1; i < checkboxes.length - 1; i++) {

                if (al_no == undefined) {

                    al_no = checkboxes[i].value + ',';

                }
                else {
                    if (al_no == '') {

                        al_no = checkboxes[i].value + ',';
                    }
                    else {
                        al_no = checkboxes[i].value + ','; checkboxes[i].value;
                    }
                }

                $('#<%= hdnpageno.ClientID %>').val(al_no);


            }


        }
    </script>


    <asp:UpdatePanel ID="updTemplate" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:HiddenField ID="hdnLogo" runat="server" ClientIDMode="Static" />
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">SMS TEMPLATE</h3>
                            <div class="box-body">
                                <div class="nav-tabs-custom" id="Tabs">
                                    <ul class="nav nav-tabs" role="tablist">
                                        <li class="nav-item">
                                            <a class="nav-link active" data-toggle="tab" href="#tab_1" tabindex="1">Template Type</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link" data-toggle="tab" href="#tab_2" tabindex="1">SMS Template</a>
                                        </li>
                                    </ul>
                                    <div class="tab-content" id="my-tab-content">
                                        <div class="tab-pane active" id="tab_1">
                                            <div>
                                                <asp:UpdateProgress ID="UpdprogDepartment" runat="server" AssociatedUpdatePanelID="updtmptyp"
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
                                            <asp:UpdatePanel ID="updtmptyp" runat="server">
                                                <ContentTemplate>
                                                    <div class="box-body">
                                                        <div class="col-12">
                                                            <div class="row">
                                                                <div class="form-group col-lg-3">
                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <label>Template Type</label>
                                                                    </div>
                                                                    <asp:TextBox ID="txtTemplateType" runat="server" CssClass="form-control" TabIndex="2"
                                                                        ToolTip="Please Enter Template Type" AutoComplete="OFF" placeholder="Template Type" ValidationGroup="submit" />
                                                                    <ajaxToolKit:FilteredTextBoxExtender ID="fltname" runat="server" FilterType="UppercaseLetters, LowercaseLetters, Custom" ValidChars="., " TargetControlID="txtTemplateType" />
                                                                </div>


                                                                <div class="form-group col-md-6 col-12">
                                                                    <div class="label-dynamic">

                                                                        <label>Status</label>
                                                                    </div>
                                                                    <div class="switch form-inline">
                                                                        <input type="checkbox" id="rdActiveTemType" name="switch" checked />

                                                                        <label data-on="Active" tabindex="3" class="newAddNew Tab" data-off="Inactive" for="rdActiveTemType"></label>
                                                                    </div>
                                                                </div>

                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-12 btn-footer">

                                                        <asp:Button ID="btnSave" TabIndex="4" ToolTip="Submit" ValidationGroup="Submit" OnClientClick="return validate();"
                                                            CssClass="btn btn-primary" runat="server" Text="Submit" OnClick="btnSave_Click" />
                                                        <asp:Button ID="btnUpdate" TabIndex="5" ToolTip="Update" OnClick="btnUpdate_Click" OnClientClick="return validate();" ValidationGroup="Submit"
                                                            CssClass="btn btn-primary" runat="server" Text="Update" />
                                                        <asp:Button ID="btnCancel" TabIndex="6" ToolTip="Cancel" runat="server" CssClass="btn btn-warning" Text="Cancel" OnClick="btnCancel_Click" />
                                                        <asp:ValidationSummary ID="vlsummary" runat="server" ShowMessageBox="true"
                                                            ShowSummary="false" DisplayMode="List" ValidationGroup="Submit" />

                                                    </div>

                                                    <div class="col-md-12">
                                                        <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto">
                                                            <asp:ListView ID="lvTemplate" runat="server">
                                                                <LayoutTemplate>
                                                                    <div class="sub-heading">
                                                                        <h5>Template Type List</h5>
                                                                    </div>
                                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                                        <thead class="bg-light-blue">
                                                                            <tr>
                                                                                <th style="text-align: center;">Edit
                                                                                </th>
                                                                                <th>Template Type
                                                                                </th>
                                                                                <th>Status
                                                                                </th>
                                                                            </tr>
                                                                        </thead>
                                                                        <tbody>
                                                                            <tr id="itemPlaceholder" runat="server" />
                                                                        </tbody>
                                                                    </table>
                                                                </LayoutTemplate>
                                                                <ItemTemplate>
                                                                    <asp:UpdatePanel runat="server" ID="updTemplate">

                                                                        <ContentTemplate>

                                                                            <tr>
                                                                                <td style="text-align: center;">
                                                                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.png" CommandArgument='<%# Eval("TEMPLATE_ID") %>'
                                                                                        AlternateText="Edit Record" class="newAddNew Tab" ToolTip="Edit Record" OnClick="btnEdit_Click" TabIndex="7" />
                                                                                </td>
                                                                                <td>
                                                                                    <%# Eval("TEMPLATE_TYPE")%>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="Active" Text='<%# Eval("ACTIVE_STATUS")%>' ForeColor='<%# Eval("ACTIVE_STATUS").ToString().Equals("ACTIVE")?System.Drawing.Color.Green:System.Drawing.Color.Red %>' runat="server"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </ItemTemplate>
                                                            </asp:ListView>

                                                        </asp:Panel>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>

                                        <div class="tab-pane" id="tab_2">
                                            <div>
                                                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updsms"
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
                                            <asp:UpdatePanel ID="updsms" runat="server">
                                                <ContentTemplate>
                                                    <div class="box-body">
                                                        <div class="col-12">
                                                            <div class="row">
                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <label>Template Type</label>
                                                                    </div>
                                                                    <asp:DropDownList ID="ddlTemplateType" runat="server" TabIndex="2" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                                                        <asp:ListItem>Please Select</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>

                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <label>Template Name</label>
                                                                    </div>
                                                                    <asp:TextBox ID="txtTemplateName" AutoComplete="off" TabIndex="3" placeholder="Enter Template Name" runat="server" MaxLength="50" CssClass="form-control"
                                                                        ToolTip="Please Enter Template Name" />
                                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="UppercaseLetters, LowercaseLetters, Custom" ValidChars=". ()" TargetControlID="txtTemplateName" />
                                                                </div>

                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <label>Page Name</label>
                                                                    </div>
                                                                    <asp:DropDownList ID="ddlPageName" runat="server" TabIndex="4" multiple="multiple" Visible="false" ToolTip="Please Select Page Name">
                                                                    </asp:DropDownList>
                                                                    <asp:HiddenField ID="hdnpageno" runat="server" />
                                                                    <asp:ListBox ID="lstbxPageName" runat="server" AppendDataBoundItems="true" TabIndex="5"
                                                                        CssClass="form-control multi-select-demo" SelectionMode="multiple" AutoPostBack="false"></asp:ListBox>
                                                                </div>

                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <label>Template Id</label>
                                                                    </div>
                                                                    <asp:TextBox ID="txtTemplateId" AutoComplete="off" TabIndex="5" placeholder="Enter Template Id" runat="server" MaxLength="25" CssClass="form-control"
                                                                        ToolTip="Please Enter Template Id" />
                                                                </div>
                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <label>Template</label>
                                                                    </div>
                                                                    <asp:TextBox ID="txtTemplate" runat="server" TabIndex="6" TextMode="MultiLine" Height="60" Width="240" ToolTip="Please Enter Template"></asp:TextBox>

                                                                </div>
                                                                <div class="form-group col-md-6 col-12">
                                                                    <div class="label-dynamic">

                                                                        <label>Status</label>
                                                                    </div>
                                                                    <div class="switch form-inline">
                                                                        <input type="checkbox" id="rdActiveSmsTemType" name="switch" checked />

                                                                        <label data-on="Active" tabindex="7" class="newAddNew Tab" data-off="Inactive" for="rdActiveSmsTemType"></label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <br />
                                                        <div class="col-12 btn-footer">
                                                            <asp:Button ID="btnSubmit" TabIndex="8" ToolTip="Submit" ValidationGroup="Submit" OnClick="btnSubmit_Click"
                                                                CssClass="btn btn-primary" OnClientClick="return smsvalidate();" runat="server" Text="Submit" />
                                                            <asp:Button ID="btnUpdateSms" TabIndex="9" ToolTip="Update" OnClientClick="return smsvalidate();" ValidationGroup="Submit" OnClick="btnUpdateSms_Click"
                                                                CssClass="btn btn-primary" runat="server" Text="Update" />
                                                            <asp:Button ID="btnCancelSmsTemp" TabIndex="10" ToolTip="Cancel" runat="server" CssClass="btn btn-warning" Text="Cancel" OnClick="btnCancelSmsTemp_Click" />

                                                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="true"
                                                                ShowSummary="false" DisplayMode="List" ValidationGroup="Submit" />
                                                        </div>
                                                        <div class="col-12">
                                                            <asp:Panel ID="pnlSmsTeplate" runat="server" Visible="true">
                                                                <asp:ListView ID="lvSmsTemplate" runat="server">
                                                                    <LayoutTemplate>
                                                                        <div class="sub-heading">
                                                                            <h5>SMS Template List</h5>
                                                                        </div>
                                                                        <table class="table table-striped table-bordered nowrap" id="tblSms" style="width: 100%">
                                                                            <thead class="bg-light-blue">
                                                                                <tr>
                                                                                    <th style="text-align: center;">Edit
                                                                                    </th>
                                                                                    <th>Template Type
                                                                                    </th>
                                                                                    <th>Template Name
                                                                                    </th>
                                                                                    <th>Page Name
                                                                                    </th>
                                                                                    <th>Template id
                                                                                    </th>
                                                                                    <th>Template 
                                                                                    </th>
                                                                                    <th>Status
                                                                                    </th>
                                                                                </tr>
                                                                            </thead>
                                                                            <tbody>
                                                                                <tr id="itemPlaceholder" runat="server" />
                                                                            </tbody>
                                                                        </table>
                                                                    </LayoutTemplate>
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <td style="text-align: center;">
                                                                                <asp:ImageButton ID="btnEditSmsType" class="newAddNew Tab" TabIndex="11" runat="server" ImageUrl="~/images/edit.png" OnClick="btnEditSmsType_Click"
                                                                                    CommandArgument='<%# Eval("SMS_TEMPLATE_ID")%>' AlternateText="Edit Record" ToolTip="Edit Record" />

                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("TEMPLATE_TYPE")%>
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("TEMPLATE_NAME")%>
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("AL_Link")%>
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("TEM_ID")%>
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("TEMPLATE")%>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lblActiveStatus" Text='<%# Eval("ACTIVE_STATUS")%>' ForeColor='<%# Eval("ACTIVE_STATUS").ToString().Equals("ACTIVE")?System.Drawing.Color.Green:System.Drawing.Color.Red %>' runat="server"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:ListView>
                                                            </asp:Panel>
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <asp:HiddenField ID="Tabid" runat="server" />
        </ContentTemplate>
        <Triggers>
            <%--  <asp:PostBackTrigger ControlID="btnSubmit" />
            <asp:PostBackTrigger ControlID="btnCancelSmsTemp" />
            <asp:PostBackTrigger ControlID="btnUpdateSms" />--%>
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>


