<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="TC_Remarks.aspx.cs" Inherits="ACADEMIC_MASTERS_TC_Remarks" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <link href="<%=Page.ResolveClientUrl("~/plugins/multi-select/bootstrap-multiselect.css") %>" rel="stylesheet" />
    <script src="<%=Page.ResolveClientUrl("~/plugins/multi-select/bootstrap-multiselect.js")%>"></script>

    <style>
        .dataTables_scrollHeadInner
        {
            width: max-content !important;
        }
    </style>
    <style>
        .Tab:focus
        {
            outline: none;
            box-shadow: 0px 0px 5px 2px #61C5FA !important;
        }
    </style>

    <asp:HiddenField ID="hfdActive" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfdStart" runat="server" ClientIDMode="Static" />

    <div>
        <asp:UpdateProgress ID="UPDPROG" runat="server" AssociatedUpdatePanelID="UPDROLE"
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


    <asp:UpdatePanel runat="server" ID="UPDROLE">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <%--<h3 class="box-title">SESSION CREATION</h3>--%>
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Remarks</label>
                                        </div>
                                        <asp:TextBox ID="txtRemarks" runat="server" AutoComplete="off" CssClass="form-control" TabIndex="1" MaxLength="200"
                                            ToolTip="Please Enter Remarks" placeholder="Enter Reason" />
                                        <asp:RequiredFieldValidator ID="rfvReason" runat="server" SetFocusOnError="True"
                                            ErrorMessage="Please Enter remarks" ControlToValidate="txtRemarks"
                                            Display="None" ValidationGroup="Remarks"/>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender45" runat="server" TargetControlID="txtRemarks" FilterMode="InvalidChars"
                                            InvalidChars="~`!@#$%^&*()_-+={[}]|\:;'<,>.?/1234567890">
                                        </ajaxToolKit:FilteredTextBoxExtender>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="row">
                                            <div class="form-group col-6">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Active Status</label>
                                                </div>
                                                <div class="switch form-inline">
                                                    <input type="checkbox" id="rdActive" name="switch" checked />
                                                    <label data-on="Active" data-off="Inactive" for="rdActive"></label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="Remarks"
                                    TabIndex="2" CssClass="btn btn-primary" OnClientClick="return validate();" OnClick="btnSubmit_Click" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel"
                                    TabIndex="3" CssClass="btn btn-warning" OnClick="btnCancel_Click" />

                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="true"
                                    ShowSummary="false" DisplayMode="List" ValidationGroup="Remarks"/>
                               <%-- <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                                    ShowSummary="false" DisplayMode="List" ValidationGroup="submit" />--%>
                            </div>

                            <div class="col-12">
                                <%--<div class="sub-heading">
                                    <h5>Reason for TC</h5>
                                </div>--%>
                                <asp:Panel ID="pnlReason" runat="server" Visible="false">
                                    <asp:ListView ID="lvReason" runat="server">
                                        <LayoutTemplate>
                                            <h5>Remark for TC</h5>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th style="text-align: center;">Action
                                                        </th>
                                                        <th>Remarks
                                                        </th>
                                                        <th>Active Status
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
                                                    <asp:ImageButton ID="btnEdit" class="newAddNew Tab" runat="server" CausesValidation="false" ImageUrl="~/Images/edit.png"
                                                        OnClick="btnEdit_Click" CommandArgument='<%# Eval("ID")%>' AlternateText="Edit Record" ToolTip="Edit Record" TabIndex="4" />
                                                </td>
                                                <td>
                                                    <%# Eval("REMARKS") %>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblIsActive" runat="server" CssClass='<%# Eval("IS_ACTIVE")%>' Text='<%# Eval("IS_ACTIVE")%>'
                                                        ForeColor='<%# Eval("IS_ACTIVE").ToString().Equals("Active")?System.Drawing.Color.Green:System.Drawing.Color.Red %>'></asp:Label>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSubmit" />
            <asp:PostBackTrigger ControlID="btnCancel" />
        </Triggers>
    </asp:UpdatePanel>

    <script>
        function SetStatActive(val) {
            $('#rdActive').prop('checked', val);
        }

        function validate() {

            $('#hfdActive').val($('#rdActive').prop('checked'));
            $('#hfdStart').val($('#rdStart').prop('checked'));

            var idtxtweb = $("[id$=txtReason]").attr("id");
            var txtweb = document.getElementById(idtxtweb);
            if (txtweb.value.length == 0) {
                alert('Please Enter Reason', 'Warning!');
                //$(txtweb).css('border-color', 'red');
                $(txtweb).focus();
                return false;
            }
        }
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnSubmit').click(function () {
                    validate();
                });
            });
        });
    </script>

</asp:Content>
