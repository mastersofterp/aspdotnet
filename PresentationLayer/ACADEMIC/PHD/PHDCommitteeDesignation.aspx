<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="PHDCommitteeDesignation.aspx.cs" Inherits="ACADEMIC_PHD_PHDCommitteeDesignation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="<%=Page.ResolveClientUrl("~/plugins/multi-select/bootstrap-multiselect.css") %>" rel="stylesheet" />
    <script src="<%=Page.ResolveClientUrl("~/plugins/multi-select/bootstrap-multiselect.js")%>"></script>
    <style>
        .Tab:focus
        {
            outline: none;
            box-shadow: 0px 0px 5px 2px #61C5FA !important;
        }
    </style>
    <asp:HiddenField ID="hfdActive" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfdStart" runat="server" ClientIDMode="Static" />
    <asp:UpdatePanel runat="server" ID="udpphd" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <%--<h3 class="box-title">Course Creation </h3>--%>
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>
                        <div class="box-body">
                            <div class="nav-tabs-custom">
                                <ul class="nav nav-tabs" role="tablist">
                                    <li class="nav-item">
                                        <a class="nav-link active" data-toggle="tab" href="#tab_1" tabindex="1" id="tab1">PhD Committee</a>
                                    </li>
                                    <li class="nav-item">
                                        <a class="nav-link" data-toggle="tab" href="#tab_2" tabindex="2" id="tab2">PhD Committee Designation</a>
                                    </li>
                                    <li class="nav-item">
                                        <a class="nav-link" data-toggle="tab" href="#tab_3" tabindex="3" id="tab3">Committee Mapping</a>
                                    </li>
                                </ul>
                                <div class="tab-content" id="my-tab-content">
                                    <div class="tab-pane active" id="tab_1">
                                        <div>
                                            <asp:UpdateProgress ID="updCommittee" runat="server" AssociatedUpdatePanelID="UdpphdCommittee"
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

                                        <asp:UpdatePanel ID="UdpphdCommittee" runat="server">
                                            <ContentTemplate>
                                                <div class="box-body">
                                                    <div class="col-12">
                                                        <div class="row">
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Committee Name</label>
                                                                </div>
                                                                <asp:TextBox ID="txtCommitteeName" runat="server" MaxLength="80" AutoComplete="off" CssClass="form-control" TabIndex="4" ToolTip="Please Enter Committee Name" />
                                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                                    FilterMode="InvalidChars" FilterType="Custom" InvalidChars="~`!@#$%^*()_+=,/:;<>?'{}[]\|.:-&&quot;'" TargetControlID="txtCommitteeName" />
                                                                <asp:RequiredFieldValidator ID="rfvtxtCommitteeName" runat="server" ControlToValidate="txtCommitteeName"
                                                                    Display="None" ErrorMessage="Please Enter Committee Name" SetFocusOnError="True"
                                                                    ValidationGroup="Committee"></asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <label></label>
                                                                </div>
                                                                <asp:CheckBox ID="chkMaincom" runat="server" TabIndex="5"/>
                                                                <label>Is Main Committee</label>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <label>Status</label>
                                                                </div>
                                                                <div class="switch form-inline">
                                                                    <input type="checkbox" id="chkrdActive" name="switch" checked />
                                                                    <label data-on="Active" tabindex="6" class="newAddNew Tab" data-off="Inactive" for="chkrdActive"></label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-12 btn-footer">
                                                        <asp:Button ID="btnCommitteeSubmit" runat="server" Text="Submit" ToolTip="Submit"
                                                            CssClass="btn btn-primary" ValidationGroup="Committee" TabIndex="7" OnClientClick="return validation();" OnClick="btnCommitteeSubmit_Click" />

                                                        <asp:Button ID="btnCommitteeCancel" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false"
                                                            CssClass="btn btn-warning" TabIndex="8" OnClick="btnCommitteeCancel_Click" />

                                                    </div>
                                                    <asp:ValidationSummary ID="ValidationSummary3" runat="server" ValidationGroup="Committee"
                                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                                    <div class="col-12 mt-3">

                                                        <div class="table-responsive">
                                                            <asp:Panel ID="PnCommittee" runat="server" Visible="false">
                                                                <div class="sub-heading">
                                                                    <h5>PhD Committee List</h5>
                                                                </div>
                                                                <asp:ListView ID="lvCommittee" runat="server" ItemPlaceholderID="itemPlaceholder">
                                                                    <LayoutTemplate>
                                                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="idlvCommittee">
                                                                            <thead class="bg-light-blue">
                                                                                <tr>
                                                                                    <th>Edit</th>
                                                                                    <th>Committee Name
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
                                                                                    <td>

                                                                                        <asp:ImageButton ID="btnCommitteeEdit" runat="server" ImageUrl="~/Images/edit.png"
                                                                                            CommandArgument='<%#Eval("COMMITTEE_ID")%>' TabIndex="9" OnClick="btnCommitteeEdit_Click" CausesValidation="false" />
                                                                                    </td>

                                                                                    <td>
                                                                                        <%# Eval("COMMITTEE_NAME")%>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:Label ID="lblAcdStatus" runat="server" Text='<%# Eval("ACTIVESTATUS") %>' ForeColor='<%# Eval("ACTIVESTATUS").ToString().Equals("Active")?System.Drawing.Color.Green:System.Drawing.Color.Red %>'></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                            </ContentTemplate>

                                                                        </asp:UpdatePanel>
                                                                    </ItemTemplate>
                                                                </asp:ListView>
                                                            </asp:Panel>
                                                        </div>
                                                    </div>

                                                </div>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="lvCommittee" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                    <div class="tab-pane" id="tab_2">
                                        <div>
                                            <asp:UpdateProgress ID="upDesig" runat="server" AssociatedUpdatePanelID="updDesignation">
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
                                        <asp:UpdatePanel ID="updDesignation" runat="server">
                                            <ContentTemplate>
                                                <div class="box-body">
                                                    <div class="col-12">
                                                        <div class="row">
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Designation</label>
                                                                </div>
                                                                <asp:TextBox ID="txtDesignation" runat="server" MaxLength="80" AutoComplete="off" CssClass="form-control" TabIndex="1" ToolTip="Please Enter Designation" ValidationGroup="Designation" />
                                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                                                    FilterMode="InvalidChars" FilterType="Custom" InvalidChars="~`!@#$%^*()_+=,/:;<>?'{}[]\|.:-&&quot;0123456789'" TargetControlID="txtDesignation" />
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDesignation"
                                                                    Display="None" ErrorMessage="Please Enter Designation" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <label></label>
                                                                </div>
                                                                <asp:CheckBox ID="chkExternal" runat="server" />
                                                                <label>Is External</label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-12 btn-footer">
                                                        <asp:Button ID="btnDesignationSubmit" runat="server" Text="Submit" ToolTip="Submit"
                                                            CssClass="btn btn-primary" TabIndex="3" OnClick="btnDesignationSubmit_Click" ValidationGroup="Designation" OnClientClick="return validationDesig();" />

                                                        <asp:Button ID="btnDesignationCancel" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false"
                                                            CssClass="btn btn-warning" TabIndex="4" OnClick="btnDesignationCancel_Click" />

                                                    </div>
                                                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="Designation"
                                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                                    <div class="col-12 mt-3">

                                                        <div class="table-responsive">
                                                            <asp:Panel ID="PnDesignation" runat="server" Visible="false">
                                                                <div class="sub-heading">
                                                                    <h5>PhD Committee Designation List</h5>
                                                                </div>
                                                                <asp:ListView ID="lvDesignation" runat="server">
                                                                    <LayoutTemplate>
                                                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="idlvDesignation">
                                                                            <thead class="bg-light-blue">
                                                                                <tr>
                                                                                    <th>Edit</th>
                                                                                    <th>Designation
                                                                                    </th>
                                                                                    <th>External Status
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
                                                                            <td>
                                                                                <asp:ImageButton ID="btnDesignationEdit" runat="server" ImageUrl="~/Images/edit.png" CausesValidation="false"
                                                                                    AlternateText="Edit Record" ToolTip="Edit Record" CommandArgument='<%# Eval("DESIG_ID")%>' TabIndex="5" OnClick="btnDesignationEdit_Click" />
                                                                            </td>

                                                                            <td>
                                                                                <%# Eval("DESIGNATION")%>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lblExternalStatus" runat="server" Text='<%# Eval("EXTERNALSTATUS") %>' ForeColor='<%# Eval("EXTERNALSTATUS").ToString().Equals("Yes")?System.Drawing.Color.Green:System.Drawing.Color.Red %>'></asp:Label>

                                                                            </td>
                                                                        </tr>

                                                                    </ItemTemplate>
                                                                </asp:ListView>
                                                            </asp:Panel>

                                                        </div>
                                                    </div>

                                                </div>
                                            </ContentTemplate>

                                        </asp:UpdatePanel>


                                    </div>

                                    <div class="tab-pane" id="tab_3">
                                        <div>
                                            <asp:UpdateProgress ID="UdpMapping" runat="server" AssociatedUpdatePanelID="updCMapping"
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

                                        <asp:UpdatePanel ID="updCMapping" runat="server">
                                            <ContentTemplate>
                                                <div class="box-body">
                                                    <div class="col-12">
                                                        <div class="row">
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Committee</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlCommittee" runat="server" AppendDataBoundItems="true" SetFocusOnError="true" ValidationGroup="SubmitMapping"
                                                                    CssClass="form-control" data-select2-enable="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>

                                                                <asp:RequiredFieldValidator ID="rfvCommittee" runat="server" ControlToValidate="ddlCommittee"
                                                                    Display="None" ErrorMessage="Please Select Committee" InitialValue="0"></asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12" id="DivMultipleSelect" runat="server">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Designation</label>
                                                                </div>

                                                                <asp:ListBox ID="lboDesignation" runat="server" SelectionMode="Multiple" CssClass="form-control multi-select-demo" AppendDataBoundItems="true"></asp:ListBox>

                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <label>Status</label>
                                                                </div>
                                                                <div class="switch form-inline">
                                                                    <input type="checkbox" id="chkMapping" name="switch" checked />
                                                                    <label data-on="Active" tabindex="2" class="newAddNew Tab" data-off="Inactive" for="chkMapping"></label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-12 btn-footer">
                                                        <asp:Button ID="btnMappingSubmit" runat="server" Text="Submit" ToolTip="Submit"
                                                            CssClass="btn btn-primary" ValidationGroup="SubmitMapping" TabIndex="3" OnClientClick=" return validationMapping();" OnClick="btnMappingSubmit_Click" />

                                                        <asp:Button ID="btnMappingCancel" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false"
                                                            CssClass="btn btn-warning" TabIndex="4" OnClick="btnMappingCancel_Click" />

                                                    </div>
                                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="SubmitMapping"
                                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                                    <div class="col-12 mt-3">

                                                        <div class="table-responsive">
                                                            <asp:Panel ID="PnMapping" runat="server" Visible="false">
                                                                <div class="sub-heading">
                                                                    <h5>PhD Committee Mapping List</h5>
                                                                </div>
                                                                <asp:ListView ID="lvMapping" runat="server">
                                                                    <LayoutTemplate>
                                                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="lvMapping">
                                                                            <thead class="bg-light-blue">
                                                                                <tr>
                                                                                    <th>Edit</th>
                                                                                    <th>Committee
                                                                                    </th>
                                                                                    <th>Designation
                                                                                    </th>
                                                                                    <th>Staus
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
                                                                            <td>
                                                                                <asp:ImageButton ID="btnMappingEdit" runat="server" ImageUrl="~/Images/edit.png" CausesValidation="false"
                                                                                    AlternateText="Edit Record" ToolTip="Edit Record" CommandArgument='<%# Eval("COMMITTEE_ID")%>' TabIndex="5" OnClick="btnMappingEdit_Click" />
                                                                            </td>

                                                                            <td>
                                                                                <%# Eval("COMMITTEE_NAME")%>
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("DESIGNATION")%>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lblExternalStatus" runat="server" Text='<%# Eval("ACTIVESTATUS") %>' ForeColor='<%# Eval("ACTIVESTATUS").ToString().Equals("Active")?System.Drawing.Color.Green:System.Drawing.Color.Red %>'></asp:Label>

                                                                            </td>
                                                                        </tr>

                                                                    </ItemTemplate>
                                                                </asp:ListView>
                                                            </asp:Panel>

                                                        </div>
                                                    </div>

                                                </div>
                                            </ContentTemplate>
                                            <%--<Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="btnMappingSubmit" />
                                            </Triggers>--%>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
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
    <script>
        function validationDesig() {
            var alertMsg = "";
            var committee = document.getElementById('<%=txtDesignation.ClientID%>').value;
            if (committee == 0) {
                if (committee == "") {
                    alertMsg = alertMsg + 'Please Enter Designation\n';
                }
                alert(alertMsg);
                return false;
            }
        }
    </script>
    <script>
        function SetCommittee(val) {

            $('#chkrdActive').prop('checked', val);
        }
        function validation() {
            var alertMsg = "";
            var committee = document.getElementById('<%=txtCommitteeName.ClientID%>').value;
            if (committee == 0) {
                if (committee == "") {
                    alertMsg = alertMsg + 'Please Enter Committee Name\n';
                }
                alert(alertMsg);
                return false;
            }
            else {

                $('#hfdActive').val($('#chkrdActive').prop('checked'));
            }
        }
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnCommitteeSubmit').click(function () {
                    validation();
                });
            });
        });

    </script>
    <script>
        function SetMapping(val) {

            $('#chkMapping').prop('checked', val);
        }
        function validationMapping() {
            debugger;

            var lboDesignation = $("[id$=lboDesignation]").attr("id");
            var committee = document.getElementById('<%=ddlCommittee.ClientID%>').value;
            //alert(lboDesignation);
            var lboDesignation = document.getElementById(lboDesignation);
            if (committee == "0") {
                alert('Please Select Committee', 'Warning!');
                return false;
            } else
                if (lboDesignation.value == 0) {
                    alert('Please Select Atleast One Designation', 'Warning!');
                    $(lboDesignation).focus();
                    return false;
                }
                else {

                    $('#hfdStart').val($('#chkMapping').prop('checked'));
                }
        }
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnMappingSubmit').click(function () {
                    validationMapping();
                });
            });
        });

    </script>
</asp:Content>

