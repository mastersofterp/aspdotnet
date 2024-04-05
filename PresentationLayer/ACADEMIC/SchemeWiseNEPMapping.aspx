<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" ViewStateEncryptionMode="Always" EnableViewStateMac="true"
    CodeFile="SchemeWiseNEPMapping.aspx.cs" Inherits="ACADEMIC_SchemeWiseNEPMapping" EnableViewState="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link href="<%=Page.ResolveClientUrl("../plugins/multi-select/bootstrap-multiselect.css")%>" rel="stylesheet" />
    <script src="<%=Page.ResolveClientUrl("../plugins/multi-select/bootstrap-multiselect.js")%>"></script>

    <%--<link href="../plugins/multiselect/bootstrap-multiselect.css" rel="stylesheet" />--%>
    <asp:HiddenField ID="hfdActive" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfdShowStatus" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfdPaymentApplicableForSemWise" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfdEligibilityForCrsReg" runat="server" ClientIDMode="Static" />

    <script type="text/javascript" language="javascript">
        function SelectAllNEPCategory() {
            var CHK = document.getElementById("<%=chkNEPCategoryList.ClientID%>");
            var checkbox = CHK.getElementsByTagName("input");

            var chkUser = document.getElementById('ctl00_ContentPlaceHolder1_chkNEPCategory');

            for (var i = 0; i < checkbox.length; i++) {
                var chk = document.getElementById('ctl00_ContentPlaceHolder1_chkNEPCategoryList_' + i);
                if (chkUser.checked == true) {
                    chk.checked = true;
                }
                else {
                    chk.checked = false;
                }
            }
        }
    </script>

    <style>
        .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="nav-tabs-custom mt-1">
                    <div class="tab-content">
                        <div class="tab-pane active" id="tabLC">

                            <div class="box-header with-border">
                                <h3 class="box-title">
                                    <label>Scheme Wise NEP Category Mapping</label>
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
                                                        <label>Scheme </label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlScheme" runat="server" TabIndex="1" CssClass="form-control" AppendDataBoundItems="true"
                                                        ValidationGroup="submit" ToolTip="Please Select Scheme." data-select2-enable="true" AutoPostBack="true"
                                                        OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvScheme" runat="server" ValidationGroup="submit" ControlToValidate="ddlScheme" Display="None"
                                                        ErrorMessage="Please Select Scheme." InitialValue="0" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-6 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>College</label>
                                                    </div>

                                                    <asp:ListBox ID="ddlCollege" runat="server" SelectionMode="Multiple" CssClass="form-control multi-select-demo"
                                                        AppendDataBoundItems="true" TabIndex="2" AutoPostBack="true"></asp:ListBox>
                                                    <asp:RequiredFieldValidator ID="rfvCollege" runat="server" ControlToValidate="ddlCollege" SetFocusOnError="true"
                                                        Display="None" ErrorMessage="Please Select College/Institute." ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="form-group col-lg-5 col-md-6 col-12 checkbox-list-column">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>NEP Category</label>
                                                </div>
                                                <div class="checkbox-list-box" style="height: auto;">
                                                    <asp:CheckBox ID="chkNEPCategory" runat="server" Text="All NEP Category" onclick="SelectAllNEPCategory()" CssClass="select-all-checkbox" TabIndex="3" />
                                                    <asp:CheckBoxList ID="chkNEPCategoryList" runat="server" RepeatColumns="2" RepeatDirection="Horizontal" Width="100%" CssClass="checkbox-list-style" />

                                                    <asp:CustomValidator ID="cvNEPCategory" runat="server" ClientValidationFunction="validateNEPCategory" SetFocusOnError="true" Display="None"
                                                        ErrorMessage="Please Select NEP Category." ValidationGroup="submit"></asp:CustomValidator>
                                                </div>
                                            </div>

                                        </div>

                                        <div class="col-12 btn-footer">
                                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="submit"
                                                TabIndex="4" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="5"
                                                CssClass="btn btn-warning" OnClick="btnCancel_Click" />
                                            <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                ShowSummary="false" ValidationGroup="submit" />
                                        </div>

                                        <div class="col-12">
                                            <asp:Panel ID="Panel1" runat="server">
                                                <asp:ListView ID="lvSchemeNEPMapping" runat="server" OnItemDataBound="lvSchemeNEPMapping_ItemDataBound">
                                                    <LayoutTemplate>
                                                        <div class="sub-heading">
                                                            <h5>Schemewise NEP Mapping</h5>
                                                        </div>
                                                        <table class="table table-striped table-bordered nowrap mb-0" style="width: 100%">
                                                            <thead class="bg-light-blue">
                                                                <tr>
                                                                    <th style="text-align: center; width: 5%;">Edit </th>
                                                                    <th style="text-align: center; width: 5%;">Show</th>
                                                                    <th style="text-align: center; width: 10%;">Unique Group No</th>
                                                                    <th style="text-align: center; width: 80%;">Scheme</th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <tr id="itemPlaceholder" runat="server" />
                                                            </tbody>
                                                        </table>
                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <table class="table table-hover table-bordered mb-0">
                                                            <tr id="MAIN" runat="server" class="col-md-12">
                                                                <td>
                                                                    <tr>
                                                                        <td style="text-align: center; width: 5%;">
                                                                            <asp:ImageButton ID="btnEdit" runat="server" AlternateText="Edit Record" CausesValidation="false"
                                                                                CommandArgument='<%# Eval("GROUPID") %>' OnClick="btnEdit_Click"
                                                                                ImageUrl="~/Images/edit.png" ToolTip="Edit Record" />
                                                                        </td>
                                                                        <td style="text-align: center; width: 5%;">
                                                                            <asp:Panel ID="pnlDetails" runat="server" Style="cursor: pointer; vertical-align: top; margin: auto;">
                                                                                <asp:Image ID="imgExp" runat="server" ImageUrl="~/Images/action_down.png" />
                                                                            </asp:Panel>
                                                                        </td>
                                                                        <td style="text-align: center; width: 10%;">
                                                                            <asp:Label ID="Label1" Text='<%# Eval("GROUPID")%>' runat="server" />
                                                                        </td>
                                                                        <td style="text-align: left; width: 80%;">
                                                                            <asp:Label ID="lbllvScheme" Text='<%# Eval("SCHEMENAME")%>' runat="server" />
                                                                        </td>

                                                                        <ajaxToolKit:CollapsiblePanelExtender ID="cpeCourt2" runat="server" CollapseControlID="pnlDetails"
                                                                            Collapsed="true" CollapsedImage="~/Images/action_down.png" ExpandControlID="pnlDetails"
                                                                            ExpandedImage="~/Images/action_up.png" ImageControlID="imgExp" TargetControlID="pnlShowCDetails">
                                                                        </ajaxToolKit:CollapsiblePanelExtender>
                                                                    </tr>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <div class="col-12">
                                                            <asp:Panel ID="pnlShowCDetails" runat="server" CssClass="collapsePanel">

                                                                <asp:ListView ID="lvDetails" runat="server">
                                                                    <LayoutTemplate>
                                                                        <div class="table-responsive" style="width: 100%; overflow: scroll;">
                                                                            <table class="table table-striped table-bordered nowrap">
                                                                                <thead class="bg-light-blue">
                                                                                    <tr>
                                                                                        <th style="text-align: center">NEP Category
                                                                                        </th>
                                                                                        <th style="text-align: center">NEP Scheme Name
                                                                                        </th>
                                                                                        <th style="text-align: center">College
                                                                                        </th>
                                                                                    </tr>
                                                                                </thead>
                                                                                <tbody>
                                                                                    <tr id="itemPlaceholder" runat="server">
                                                                                    </tr>
                                                                                </tbody>
                                                                            </table>
                                                                        </div>
                                                                    </LayoutTemplate>
                                                                    <EmptyDataTemplate>
                                                                        <div style="text-align: center; font-family: Arial; font-size: medium">
                                                                            No Record Found
                                                                        </div>
                                                                    </EmptyDataTemplate>
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <td>
                                                                                <%# Eval("CATEGORYNAME")%>
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("NEP_SCHEME_NAME")%>
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("COLLEGE_NAME")%>
                                                                            </td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:ListView>

                                                            </asp:Panel>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </asp:Panel>
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btnSubmit" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        function validateNEPCategory(sender, args) {
            var chkList = document.getElementById('<%= chkNEPCategoryList.ClientID %>');
            var checkboxes = chkList.getElementsByTagName('input');
            var isChecked = false;

            // Check if any checkbox in the list is checked
            for (var i = 0; i < checkboxes.length; i++) {
                if (checkboxes[i].type === 'checkbox' && checkboxes[i].checked) {
                    isChecked = true;
                    break; // Exit loop if any checkbox is checked
                }
            }

            // If none of the checkboxes are checked, trigger validation
            if (!isChecked) {
                args.IsValid = false;
            } else {
                args.IsValid = true;
            }
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
