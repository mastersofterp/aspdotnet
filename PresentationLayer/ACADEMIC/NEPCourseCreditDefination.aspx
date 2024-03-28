<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" ViewStateEncryptionMode="Always" EnableViewStateMac="true"
    CodeFile="NEPCourseCreditDefination.aspx.cs" Inherits="ACADEMIC_NEPCourseCreditDefination" EnableViewState="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link href="<%=Page.ResolveClientUrl("../plugins/multi-select/bootstrap-multiselect.css")%>" rel="stylesheet" />
    <script src="<%=Page.ResolveClientUrl("../plugins/multi-select/bootstrap-multiselect.js")%>"></script>

    <%--<link href="../plugins/multiselect/bootstrap-multiselect.css" rel="stylesheet" />--%>
    <asp:HiddenField ID="hfdActive" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfdShowStatus" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfdPaymentApplicableForSemWise" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfdEligibilityForCrsReg" runat="server" ClientIDMode="Static" />


    <style>
        .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>

    <script>
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }
    </script>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="nav-tabs-custom mt-1">
                    <div class="tab-content">
                        <div class="tab-pane active" id="tabLC">

                            <div class="box-header with-border">
                                <h3 class="box-title">
                                    <label>NEP Course Credit Defination</label>
                                </h3>
                            </div>

                            <div class="box-body">

                                <div>
                                    <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updNEPMapping"
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

                                <asp:UpdatePanel ID="updNEPMapping" runat="server">
                                    <ContentTemplate>

                                        <div class="col-12">
                                            <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Session </label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlSession" runat="server" TabIndex="1" CssClass="form-control" AppendDataBoundItems="true"
                                                        ValidationGroup="submit" ToolTip="Please Select Scheme." data-select2-enable="true" AutoPostBack="true"
                                                        OnSelectedIndexChanged="ddlSession_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvSession" runat="server" ValidationGroup="show" ControlToValidate="ddlSession" Display="None"
                                                        ErrorMessage="Please Select Session." InitialValue="0" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Scheme </label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlScheme" runat="server" TabIndex="2" CssClass="form-control" AppendDataBoundItems="true"
                                                        ValidationGroup="submit" ToolTip="Please Select Scheme." data-select2-enable="true" AutoPostBack="true"
                                                        OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvScheme" runat="server" ValidationGroup="show" ControlToValidate="ddlScheme" Display="None"
                                                        ErrorMessage="Please Select Scheme." InitialValue="0" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-6 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Semester</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlSemester" runat="server" TabIndex="3" CssClass="form-control" AppendDataBoundItems="true"
                                                        ValidationGroup="submit" ToolTip="Please Select Semester." data-select2-enable="true" AutoPostBack="true"
                                                        OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvSemester" runat="server" ValidationGroup="show" ControlToValidate="ddlSemester" Display="None"
                                                        ErrorMessage="Please Select Semester." InitialValue="0" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                </div>


                                            </div>
                                        </div>

                                        <div class="col-12 btn-footer">
                                            <asp:Button ID="btnShow" runat="server" ClientIDMode="Static" Text="Show Data" ValidationGroup="show"
                                                TabIndex="4" CssClass="btn btn-primary" OnClick="btnShow_Click" />
                                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="show"
                                                TabIndex="4" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="5"
                                                CssClass="btn btn-warning" OnClick="btnCancel_Click" />
                                            <%-- <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                ShowSummary="false" ValidationGroup="submit" />--%>
                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                ShowSummary="false" ValidationGroup="show" />
                                        </div>

                                        <div class="col-12">
                                            <asp:Panel ID="Panel1" runat="server" Visible="false">
                                                <asp:ListView ID="lvNEPCategory" runat="server">
                                                    <LayoutTemplate>
                                                        <div class="sub-heading">
                                                            <h5>Schemewise NEP Mapping</h5>
                                                        </div>
                                                        <table class="table table-striped table-bordered nowrap mb-0" style="width: 100%">
                                                            <thead class="bg-light-blue">
                                                                <tr>
                                                                    <th style="text-align: center; width: 5%;">Select </th>
                                                                    <th style="text-align: center; width: 50%;">NEP Category</th>
                                                                    <th style="text-align: center; width: 15%;">Max Credit</th>
                                                                    <th style="text-align: center; width: 15%;">Min Credit</th>
                                                                    <th style="text-align: center; width: 15%;">No. of Subject</th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <tr id="itemPlaceholder" runat="server" />
                                                            </tbody>
                                                        </table>
                                                    </LayoutTemplate>
                                                    <EmptyDataTemplate>
                                                        <div style="text-align: center; font-family: Arial; font-size: medium">
                                                            No Record Found
                                                        </div>
                                                    </EmptyDataTemplate>
                                                    <ItemTemplate>
                                                        <table class="table table-hover table-bordered mb-0">
                                                            <tr id="MAIN" runat="server" class="col-md-12">
                                                                <td>
                                                                    <tr>
                                                                        <td style="text-align: center; width: 5%;">
                                                                            <asp:CheckBox ID="chkSelect" runat="server" ToolTip="Select Record" AutoPostBack="true"
                                                                                OnCheckedChanged="chkSelect_CheckedChanged" />
                                                                        </td>
                                                                        <td style="text-align: left; width: 50%;">
                                                                            <asp:Label ID="lblNEPCategory" Text='<%# Eval("CATEGORYNAME")%>' runat="server" />
                                                                            <asp:HiddenField ID="hfNEPCategory" runat="server" Value='<%# Eval("CATEGORYNO")%>' />
                                                                        </td>
                                                                        <td style="text-align: center; width: 15%;">
                                                                            <asp:TextBox ID="txtMaxCredit" runat="server" Enabled="false" CssClass="form-control" MaxLength="2"
                                                                                Text='<%# Eval("MAX_CREDIT")%>' onkeypress="return isNumberKey(event)" />
                                                                        </td>
                                                                        <td style="text-align: center; width: 15%;">
                                                                            <asp:TextBox ID="txtMinCredit" runat="server" Enabled="false" CssClass="form-control" MaxLength="2"
                                                                                Text='<%# Eval("MIN_CREDIT")%>' onkeypress="return isNumberKey(event)" />
                                                                        </td>
                                                                        <td style="text-align: center; width: 15%;">
                                                                            <asp:TextBox ID="txtSubjects" runat="server" Enabled="false" CssClass="form-control" MaxLength="2"
                                                                                Text='<%# Eval("NO_OF_SUBJECTS")%>' onkeypress="return isNumberKey(event)" />
                                                                        </td>
                                                                    </tr>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </asp:Panel>
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
</asp:Content>
