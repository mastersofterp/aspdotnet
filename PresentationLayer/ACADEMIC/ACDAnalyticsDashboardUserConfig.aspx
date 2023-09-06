<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/SiteMasterPage.master" CodeFile="ACDAnalyticsDashboardUserConfig.aspx.cs" Inherits="ACADEMIC_ACDAnalyticsDashboardUserConfig" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link href="<%=Page.ResolveClientUrl("~/plugins/multiselect/bootstrap-multiselect.css") %>" rel="stylesheet" />
    <script src="<%=Page.ResolveClientUrl("~/plugins/multiselect/bootstrap-multiselect.js")%>"></script>

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
        function checkBulkAllCheckbox(headchk) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                var s = e.name.split("ctl00$ContentPlaceHolder1$lvDashboard$ctrl");
                var b = 'ctl00$ContentPlaceHolder1$lvDashboard$ctrl';
                var g = b + s[1];
                if (e.name == g) {
                    if (headchk.checked == true)
                        e.checked = true;
                    else
                        e.checked = false;
                }
            }
        }
    </script>

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updpnlUser"
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
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div2" runat="server"></div>
                <div class="box-header with-border">
                    <%--<h3 class="box-title">USER LINK ASSIGN</h3>--%>
                    <h3 class="box-title">
                        <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                </div>
                <div class="box-body">
                    <asp:UpdatePanel ID="updpnlUser" runat="server">
                        <ContentTemplate>
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-4 col-md-5 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>User Type</label>
                                        </div>
                                        <asp:DropDownList ID="ddlUserType" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlUserType_SelectedIndexChanged" TabIndex="1">
                                            <%--OnSelectedIndexChanged="ddlUserType_SelectedIndexChanged"--%>
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="req_usertype" runat="server" ControlToValidate="ddlUserType"
                                            ErrorMessage="User Type Required" Display="None" InitialValue="0" ValidationGroup="Dashboard">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-4 col-md-5 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Module</label>
                                        </div>
                                        <asp:ListBox runat="server" ID="lstModule" SelectionMode="Multiple" CssClass="form-control multi-select-demo " AutoPostBack="true" OnSelectedIndexChanged="lstModule_SelectedIndexChanged"></asp:ListBox>
                                        <%--OnSelectedIndexChanged="lstSubjectName_SelectedIndexChanged"--%>
                                        <asp:RequiredFieldValidator ID="rvflstModule" runat="server" ControlToValidate="lstModule"
                                            Display="None" ValidationGroup="Dashboard" InitialValue=""
                                            ErrorMessage="Please Select Module."></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-4 col-md-5 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Dashboard</label>
                                        </div>
                                        <asp:ListBox runat="server" ID="lstDashboard" SelectionMode="Multiple" CssClass="form-control multi-select-demo "></asp:ListBox>
                                        <asp:RequiredFieldValidator ID="rfvlstDashboard" runat="server" ControlToValidate="lstDashboard"
                                            Display="None" ValidationGroup="Dashboard" InitialValue=""
                                            ErrorMessage="Please Select Dashboard."></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-4 col-md-5 col-12" runat="server" style="display: none;" id="divUserName">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>User Name</label>
                                        </div>
                                        <asp:TextBox runat="server" ID="txtUserName" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="Dashboard" TabIndex="16" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
                                    <%--OnClick="btnSubmit_Click"--%>
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="1" CssClass="btn btn-warning" OnClick="btnCancel_Click" />
                                    <%--OnClick="btnCancel_Click"--%>
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False" DisplayMode="List" ValidationGroup="Dashboard" />
                                </div>
                                <div class="col-12">
                                    <asp:Panel ID="pnlDashboard" runat="server" Visible="false">
                                        <asp:ListView ID="lvDashboard" runat="server">
                                            <LayoutTemplate>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%;" id="divDashboardlist">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th id="thHead">
                                                                <asp:CheckBox ID="cbHead" runat="server" Text="Select All" TabIndex="9" OnClick="checkBulkAllCheckbox(this)" />
                                                            </th>
                                                            <th>Edit</th>
                                                            <th>Name</th>
                                                            <th>Login Name</th>
                                                            <th>Type</th>
                                                            <th>Module</th>
                                                            <th>Dashboard</th>
                                                            <th>Status</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </tbody>
                                                </table>
                                                </div>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td style="text-align: center">
                                                        <asp:HiddenField ID="hfdUaNo" runat="server" Value='<%# Eval("UA_NO") %>' />
                                                        <asp:CheckBox ID="chkIsActive" runat="server" CssClass="chkbox_addsubject" />
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="btnEdit" class="newAddNew Tab" runat="server" CausesValidation="false" ImageUrl="~/Images/edit.png"
                                                            CommandArgument='<%# Eval("UA_NO")%>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                            OnClick="btnEdit_Click" TabIndex="6" />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblUser" runat="server" Text='<%# Eval("UA_FULLNAME")%>' ToolTip='<%# Eval("UA_NO")%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <%# Eval("UA_NAME")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("USERDESC")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("MODULE")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("DASHBOARD")%>
                                                    <td>
                                                        <asp:Label ID="lblUStatus" Style="font-size: 9pt;" runat="server" Text='<%# Eval("UA_STATUS")%>'></asp:Label>
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
</asp:Content>
