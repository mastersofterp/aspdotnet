<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="CopyStandardFees.aspx.cs" Inherits="ACADEMIC_MASTERS_CopyStandardFees" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="pnlFeeTable"
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
   
    <asp:UpdatePanel ID="pnlFeeTable" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <%--<h3 class="box-title">Copy Standard Fee Definition</h3>--%>
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-9 col-md-12 col-12">
                                        <div class="row">
                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Receipt Type</label>
                                                </div>
                                                <asp:DropDownList ID="ddlReceiptType" runat="server" OnSelectedIndexChanged="ddlReceiptType_SelectedIndexChanged" AppendDataBoundItems="true"
                                                    CssClass="form-control" data-select2-enable="true" TabIndex="1"
                                                    AutoPostBack="true" />
                                                <asp:RequiredFieldValidator ID="valReceiptType" runat="server" ControlToValidate="ddlReceiptType"
                                                    Display="None" ErrorMessage="Please select receipt type." InitialValue="0" SetFocusOnError="true"
                                                    ValidationGroup="show" />
                                            </div>

                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <%--<label>Institute Name</label>--%>
                                                    <asp:Label ID="lblDYddlSchool" runat="server" Font-Bold="true"></asp:Label>
                                                </div>
                                                <asp:DropDownList ID="ddlSchClg" runat="server" OnSelectedIndexChanged="ddlSchClg_SelectedIndexChanged" AppendDataBoundItems="true"
                                                    TabIndex="2" CssClass="form-control" data-select2-enable="true"
                                                    AutoPostBack="true" ToolTip="Please select Institute">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvSchClg" runat="server" ControlToValidate="ddlSchClg"
                                                    Display="None" ErrorMessage="Please select Institute." InitialValue="0" SetFocusOnError="true"
                                                    ValidationGroup="show" />
                                            </div>

                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <%-- <label>Degree</label>--%>
                                                    <asp:Label ID="lblDYddlDegree" runat="server" Font-Bold="true"></asp:Label>
                                                </div>
                                                <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" TabIndex="3"
                                                    CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="valDegree" runat="server" ControlToValidate="ddlDegree"
                                                    Display="None" ErrorMessage="Please select degree." InitialValue="0" SetFocusOnError="true"
                                                    ValidationGroup="show" />
                                            </div>

                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <%--<label>Branch</label>--%>
                                                    <asp:Label ID="lblDYddlBranch" runat="server" Font-Bold="true"></asp:Label>
                                                </div>
                                                <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" TabIndex="4"
                                                    CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" AutoPostBack="True">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlBranch"
                                                    Display="None" ErrorMessage="Please select branch." InitialValue="0" SetFocusOnError="true"
                                                    ValidationGroup="show" />
                                            </div>

                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <%--<label>Batch</label>--%>
                                                    <asp:Label ID="lblDYtxtBatch" runat="server" Font-Bold="true"></asp:Label>
                                                </div>
                                                <asp:DropDownList ID="ddlBatch" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                                    TabIndex="5" OnSelectedIndexChanged="ddlBatch_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="valBatch" runat="server" ControlToValidate="ddlBatch"
                                                    Display="None" ErrorMessage="Please select batch." InitialValue="0" SetFocusOnError="true"
                                                    ValidationGroup="show" />
                                            </div>

                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Payment Type</label>
                                                </div>
                                                <asp:DropDownList ID="ddlPaymentType" runat="server" AppendDataBoundItems="true"
                                                    CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlPaymentType_SelectedIndexChanged" TabIndex="6"
                                                    AutoPostBack="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="valPaymentType" runat="server" ControlToValidate="ddlPaymentType"
                                                    Display="None" ErrorMessage="Please select payment type." InitialValue="0" SetFocusOnError="true"
                                                    ValidationGroup="show" />
                                            </div>

                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Fees Name</label>
                                                </div>
                                                <asp:Label ID="lblFeeName" runat="server" Text="" Font-Size="Small" />
                                            </div>

                                            <div class="col-12 btn-footer">
                                                <asp:Button ID="btnShow" runat="server" Text="Show Fee Definition" ValidationGroup="show"
                                                    TabIndex="7" OnClick="btnShow_Click" CssClass=" btn btn-primary" />
                                                <asp:Button ID="btnCopy" runat="server" OnClick="btnCopy_Click" Text="Copy Standard Fees " ValidationGroup="copy" CssClass="btn btn-primary" />
                                                <asp:ValidationSummary ID="valSummery" runat="server" TabIndex="8" DisplayMode="List" ShowMessageBox="true"
                                                    ShowSummary="false" ValidationGroup="show" CssClass=" btn btn-primary" />
                                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" TabIndex="8" DisplayMode="List" ShowMessageBox="true"
                                                    ShowSummary="false" ValidationGroup="copy" CssClass=" btn btn-primary" />
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-12 col-12">
                                        <div class="row">
                                            <div class="col-12">
                                                <div class="sub-heading">
                                                    <h5>Search Fee Item By Name</h5>
                                                </div>
                                            </div>
                                            <div class="col-lg-12 col-md-12 col-12">
                                                <asp:TextBox ID="txtSearchBox" runat="server" onkeyup="javascript:GetMachingListboxItem(this);"
                                                    CssClass="form-control" TabIndex="6" />
                                            </div>

                                            <div class="form-group col-lg-12 col-md-12 col-12">
                                                <asp:ListBox ID="lstFeesItems" runat="server" AutoPostBack="true" OnSelectedIndexChanged="lstFeesItems_SelectedIndexChanged"
                                                    Style="min-height: 200px; max-height: 200px; overflow: scroll;" TabIndex="7" CssClass="form-control"></asp:ListBox>
                                            </div>
                                        </div>
                                    </div>

                                    <script type="text/javascript" language="javascript">
                                        function GetMachingListboxItem(searchTextbox) {
                                            if (searchTextbox != null) {
                                                var listbox = document.getElementById("ctl00_ContentPlaceHolder1_lstFeesItems");
                                                if (listbox != null) {
                                                    var strTxt = searchTextbox.value;
                                                    if (strTxt != "") {
                                                        for (var index = 0; index < listbox.length; index++) {
                                                            var strLst = listbox.options[index].innerHTML;
                                                            if (strLst.toUpperCase().substring(0, strTxt.length) == strTxt.toUpperCase()) {
                                                                listbox.selectedIndex = index;
                                                                return;
                                                            }
                                                            else {
                                                                listbox.selectedIndex = -1;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                            return false;
                                        }

                                        function IsNumeric(textbox) {
                                            if (textbox != null && textbox.value != "") {
                                                if (isNaN(textbox.value)) {
                                                    document.getElementById(textbox.id).value = 0;
                                                }
                                            }
                                        }
                                    </script>

                                </div>
                            </div>

                            <asp:Panel ID="pnlCopy" runat="server" Visible="false">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Receipt Type</label>
                                            </div>
                                            <asp:DropDownList ID="ddlRecieptCopy" OnSelectedIndexChanged="ddlRecieptCopy_SelectedIndexChanged" runat="server" AppendDataBoundItems="true"
                                                CssClass="form-control" data-select2-enable="true" TabIndex="1"
                                                AutoPostBack="true" />
                                            <asp:RequiredFieldValidator ID="rfv" runat="server" ControlToValidate="ddlRecieptCopy"
                                                Display="None" ErrorMessage="Please select Receipt Type."
                                                InitialValue="0" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Admission Batch From</label>
                                            </div>
                                            <asp:DropDownList ID="ddlColgCopy" OnSelectedIndexChanged="ddlColgCopy_SelectedIndexChanged" runat="server" AppendDataBoundItems="true"
                                                TabIndex="2" CssClass="form-control" data-select2-enable="true"
                                                AutoPostBack="false" ToolTip="Please select Admission Batch From" />
                                            <asp:RequiredFieldValidator ID="rfvddlColgCopy" runat="server" ControlToValidate="ddlColgCopy"
                                                Display="None" ErrorMessage="Please select Admission Batch From." InitialValue="0" SetFocusOnError="true"
                                                ValidationGroup="Submit" Enabled="false" />
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Admission Batch From</label>
                                            </div>
                                            <asp:DropDownList ID="ddlAdmissionBatchFrom" runat="server" AppendDataBoundItems="true"
                                                TabIndex="2" onchange="value(this);" CssClass="form-control" data-select2-enable="true"
                                                AutoPostBack="false" ToolTip="Please select Admission Batch From" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlAdmissionBatchFrom"
                                                Display="None" ErrorMessage="Please select Admission Batch From." InitialValue="0" SetFocusOnError="true"
                                                ValidationGroup="Submit" />
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Degree</label>
                                            </div>
                                            <asp:DropDownList ID="ddlDegreeCopy" runat="server" AppendDataBoundItems="true" TabIndex="3"
                                                CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlDegreeCopy_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvDegreecopy" runat="server" ControlToValidate="ddlDegreeCopy"
                                                Display="None" ErrorMessage="Please select Degree for copy." InitialValue="0" SetFocusOnError="true"
                                                ValidationGroup="Submit" Enabled="false" />
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Branch</label>
                                            </div>
                                            <asp:DropDownList ID="ddlBranchCopy" runat="server" AppendDataBoundItems="true" TabIndex="4"
                                                CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlBranchCopy_SelectedIndexChanged" AutoPostBack="True">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rqBranchCopy" runat="server" ControlToValidate="ddlBranchCopy"
                                                Display="None" ErrorMessage="Please select Branch for copy." InitialValue="0" SetFocusOnError="true"
                                                ValidationGroup="Submit" Enabled="false" />
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Admission Batch To</label>
                                            </div>
                                            <asp:DropDownList ID="ddlBatchCopy" runat="server" OnSelectedIndexChanged="ddlBatchCopy_SelectedIndexChanged" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                                TabIndex="5" AutoPostBack="false">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rqBatchCopy" runat="server" ControlToValidate="ddlBatchCopy"
                                                Display="None" ErrorMessage="Please select Admission Batch To." InitialValue="0" SetFocusOnError="true"
                                                ValidationGroup="Submit" />
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Payment Type</label>
                                            </div>
                                            <asp:DropDownList ID="ddlPaymentCopy" OnSelectedIndexChanged="ddlPaymentCopy_SelectedIndexChanged" runat="server" AppendDataBoundItems="true"
                                                CssClass="form-control" data-select2-enable="true" TabIndex="6"
                                                AutoPostBack="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvPaymentCopy" runat="server" ControlToValidate="ddlPaymentCopy"
                                                Display="None" ErrorMessage="Please select Payment Type for copy." InitialValue="0" SetFocusOnError="true"
                                                ValidationGroup="Submit" Enabled="false" />
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Fees Name</label>
                                            </div>
                                            <asp:Label ID="lblFeeSNameCopy" runat="server" Text="" Font-Size="Small" />
                                        </div>
                                    </div>
                                </div>

                                <asp:ValidationSummary ID="vsSubmit" runat="server" ValidationGroup="Submit" ShowMessageBox="true" DisplayMode="List" ShowSummary="false" CssClass=" btn btn-primary" />
                            </asp:Panel>

                            <div class="col-12">
                                <asp:Panel ID="Panel1" runat="server">
                                    <asp:ListView ID="lv" runat="server">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Standard Fee Definition</h5>
                                            </div>  <%--class="table table-striped table-bordered nowrap"--%>
                                            <table class="table table-striped table-bordered nowrap" style="width: 100%" id="tblFeeEntryGrid">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Fee Items
                                                        </th>
                                                        <th>Currency
                                                        </th>
                                                        <th>Sem-1
                                                        </th>
                                                        <th>Sem-2
                                                        </th>
                                                        <th>Sem-3
                                                        </th>
                                                        <th>Sem-4
                                                        </th>
                                                        <th>Sem-5
                                                        </th>
                                                        <th>Sem-6
                                                        </th>
                                                        <th>Sem-7
                                                        </th>
                                                        <th>Sem-8
                                                        </th>
                                                        <th>Sem-9
                                                        </th>
                                                        <th>Sem-10
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                    </tbody>
                                                <tfoot>
                                                    <tr>
                                                        <td>TOTAL:
                                                        </td>
                                                        <td></td>
                                                        <td>
                                                            <asp:TextBox ID="txtSem1TotalAmt" onkeydown="javascript:return false;" runat="server"
                                                                AutoCompleteType="Disabled" CssClass="form-control" Font-Bold="true" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtSem2TotalAmt" onkeydown="javascript:return false;" runat="server"
                                                                AutoCompleteType="Disabled" CssClass="form-control" Font-Bold="true" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtSem3TotalAmt" onkeydown="javascript:return false;" runat="server"
                                                                AutoCompleteType="Disabled" CssClass="form-control" Font-Bold="true" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtSem4TotalAmt" onkeydown="javascript:return false;" runat="server"
                                                                AutoCompleteType="Disabled" CssClass="form-control" Font-Bold="true" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtSem5TotalAmt" onkeydown="javascript:return false;" runat="server"
                                                                AutoCompleteType="Disabled" CssClass="form-control" Font-Bold="true" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtSem6TotalAmt" onkeydown="javascript:return false;" runat="server"
                                                                AutoCompleteType="Disabled" CssClass="form-control" Font-Bold="true" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtSem7TotalAmt" onkeydown="javascript:return false;" runat="server"
                                                                AutoCompleteType="Disabled" CssClass="form-control" Font-Bold="true" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtSem8TotalAmt" onkeydown="javascript:return false;" runat="server"
                                                                AutoCompleteType="Disabled" CssClass="form-control" Font-Bold="true" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtSem9TotalAmt" onkeydown="javascript:return false;" runat="server"
                                                                AutoCompleteType="Disabled" CssClass="form-control" Font-Bold="true" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtSem10TotalAmt" onkeydown="javascript:return false;" runat="server"
                                                                AutoCompleteType="Disabled" CssClass="form-control" Font-Bold="true" />
                                                        </td>
                                                    </tr>
                                                    </tfoot>
                                            </table>
                                            <asp:ValidationSummary ID="valSummery2" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                ShowSummary="false" ValidationGroup="submit" />

                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblFeeHead" runat="server" Text='<%# Bind("FEE_LONGNAME") %>' />
                                                    <asp:HiddenField ID="hidFeeCatNo" runat="server" Value='<%# Bind("FEE_CAT_NO") %>' />
                                                    <asp:HiddenField ID="hidFeeHead" runat="server" Value='<%# Bind("FEE_HEAD") %>' />
                                                    <asp:HiddenField ID="hidFeeDesc" runat="server" Value='<%# Bind("FEE_DESCRIPTION") %>' />
                                                    <asp:HiddenField ID="hidSrNo" runat="server" Value='<%# Bind("SRNO") %>' />
                                                    <asp:HiddenField ID="hidRecieptCode" runat="server" Value='<%# Bind("RECIEPT_CODE") %>' />
                                                    <asp:HiddenField ID="hidDegreeNo" runat="server" Value='<%# Bind("DEGREENO") %>' />
                                                    <asp:HiddenField ID="hidBranchNo" runat="server" Value='<%# Bind("BRANCHNO") %>' />
                                                    <asp:HiddenField ID="hidBatchNo" runat="server" Value='<%# Bind("BATCHNO") %>' />
                                                    <asp:HiddenField ID="hidPaymentNo" runat="server" Value='<%# Bind("PAYTYPENO") %>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblCurrency" runat="server" Text='<%# Bind("CURRENCY") %>' />
                                                    <%-- <asp:HiddenField ID="hidCurrency" runat="server" Value='<%# Bind("CUR_NO") %>' />--%>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtSem1" runat="server" Text='<%# Bind("SEMESTER1") %>' TabIndex="8"
                                                        AutoCompleteType="Disabled" onblur="UpdateTotalAmounts();" onkeyup="IsNumeric(this);"
                                                        CssClass="form-control" />
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtSem2" runat="server" Text='<%# Bind("SEMESTER2") %>' TabIndex="9"
                                                        AutoCompleteType="Disabled" onblur="UpdateTotalAmounts();" onkeyup="IsNumeric(this);"
                                                        CssClass="form-control" />
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtSem3" runat="server" Text='<%# Bind("SEMESTER3") %>' TabIndex="10"
                                                        AutoCompleteType="Disabled" onblur="UpdateTotalAmounts();" onkeyup="IsNumeric(this);"
                                                        CssClass="form-control" />
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtSem4" runat="server" Text='<%# Bind("SEMESTER4") %>' TabIndex="11"
                                                        AutoCompleteType="Disabled" onblur="UpdateTotalAmounts();" onkeyup="IsNumeric(this);"
                                                        CssClass="form-control" />
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtSem5" runat="server" Text='<%# Bind("SEMESTER5") %>' TabIndex="12"
                                                        AutoCompleteType="Disabled" onblur="UpdateTotalAmounts();" onkeyup="IsNumeric(this);"
                                                        CssClass="form-control" />
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtSem6" runat="server" Text='<%# Bind("SEMESTER6") %>' TabIndex="13"
                                                        AutoCompleteType="Disabled" onblur="UpdateTotalAmounts();" onkeyup="IsNumeric(this);"
                                                        CssClass="form-control" />
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtSem7" runat="server" Text='<%# Bind("SEMESTER7") %>' TabIndex="14"
                                                        AutoCompleteType="Disabled" onblur="UpdateTotalAmounts();" onkeyup="IsNumeric(this);"
                                                        CssClass="form-control" />
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtSem8" runat="server" Text='<%# Bind("SEMESTER8") %>' TabIndex="15"
                                                        AutoCompleteType="Disabled" onblur="UpdateTotalAmounts();" onkeyup="IsNumeric(this);"
                                                        CssClass="form-control" />
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtSem9" runat="server" Text='<%# Bind("SEMESTER9") %>' TabIndex="16"
                                                        AutoCompleteType="Disabled" onblur="UpdateTotalAmounts();" onkeyup="IsNumeric(this);"
                                                        CssClass="form-control" />
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtSem10" runat="server" Text='<%# Bind("SEMESTER10") %>' TabIndex="17"
                                                        AutoCompleteType="Disabled" onblur="UpdateTotalAmounts();" onkeyup="IsNumeric(this);"
                                                        CssClass="form-control" />
                                                </td>
                                            </tr>
                                        </ItemTemplate>

                                    </asp:ListView>
                                </asp:Panel>
                            </div>
                           
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit to Copy Standard Fees" OnClientClick="return Validation();"
                                    TabIndex="7" ValidationGroup="Submit" OnClick="btnSubmit_Click" CssClass="btn btn-primary" />
                                <asp:Button ID="btnReport" runat="server" Text="Report" ValidationGroup="show"
                                    TabIndex="8" OnClick="btnReport_Click" Enabled="true" CssClass="btn btn-info" Visible="false"/>
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                                    TabIndex="9" OnClick="btnCancel_Click" CssClass="btn btn-warning" />
                            </div>
                        </div>
                        <div id="divMsg" runat="server">
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSubmit" />
            <asp:PostBackTrigger ControlID="btnCancel" />
            <asp:PostBackTrigger ControlID="btnReport" />
            <asp:PostBackTrigger ControlID="btnShow" />
        </Triggers>
    </asp:UpdatePanel>
    <script type="text/javascript" language="javascript">

        function UpdateTotalAmounts() {
            try {
                var totalFeeAmt = 0.00;
                var dataRows = null;
                if (document.getElementById('tblFeeEntryGrid') != null)
                    dataRows = document.getElementById('tblFeeEntryGrid').getElementsByTagName('tr');

                if (dataRows != null) {
                    for (sem = 2; sem <= 11; sem++) {

                        totalFeeAmt = 0.00;
                        for (i = 1; i < (dataRows.length - 1) ; i++) {

                            var dataCellCollection = dataRows.item(i).getElementsByTagName('td');

                            var dataCell = dataCellCollection.item(sem);

                            var controls = dataCell.getElementsByTagName('input');

                            var txtAmt = controls.item(0).value;
                            //                              alert(txtAmt)
                            if (txtAmt != '')
                                totalFeeAmt += parseFloat(txtAmt);
                            //                              alert(totalFeeAmt)
                            if ((i + 2) == dataRows.length) {
                                var semcnt = sem - 1;
                                document.getElementById('ctl00_ContentPlaceHolder1_lv_txtSem' + semcnt + 'TotalAmt').value = totalFeeAmt.toString();
                            }
                        }
                    }
                }
            }
            catch (e) {
                alert("Error: " + e.description);
            }
        }

        //$("#column_select").change(function () {
        //    $("#layout_select").children('option').hide();
        //    $("#layout_select").children("option[value^=" + $(this).val() + "]").show()
        //})

        $(function () {
            // When your textbox is changed (i.e. a date of birth is set)
            $("#<%=ddlAdmissionBatchFrom.ClientID%>").change(function () {
                alert('hai');
                //$("#<%--= txtAge.ClientID --%>").val(_calculateAge($(this).val()));
            });
        });

        //$(function () {
        // alert('hai');
        // $("#<%--=ddlAdmissionBatchFrom.ClientID--%>").change(function () {
        //    alert('hai');
        // $("#<%--=ddlBatchCopy.ClientID--%>").children('option').hide();
        // $("#<%--=ddlBatchCopy.ClientID--%>").children("option[value^=" + $(this).val() + "]").show()
        //  });
        //  });

        function value(ddl) {
            var ddlmainValue = ddl.options[ddl.value].value;
            var drop = document.getElementById('<%= ddlBatchCopy.ClientID%>');
            var val = document.getElementById("<%= ddlBatchCopy.ClientID%>").length;
            for (var i = 0 ; i < val ; i++) {
                if (i == ddlmainValue) {
                    drop.options[ddlmainValue].style.display = "none";
                }
                else {
                    drop.options[i].style.display = "block";
                }
            }
        }

        function Validation() {
            var receipt_code = document.getElementById('<%=ddlRecieptCopy.ClientID%>').value;
            var BatchFrom = document.getElementById('<%=ddlAdmissionBatchFrom.ClientID%>').value;
            var BatchTo = document.getElementById('<%=ddlBatchCopy.ClientID%>').value;
            if (receipt_code == "0" && BatchFrom == "0" && BatchTo == "0") {

                alert("Please Select Receipt Type for Copy. \r\n Please Select Admission Batch From. \r\n Please Select Admission Batch To. ");
                return false;
            }
            else if (receipt_code == "0") {
                alert("Please Select Receipt Type for Copy.");
                return false;
            }
            else if (BatchFrom == "0") {
                alert("Please Select Admission Batch From.");
                return false;
            }
            else if (BatchTo == "0") {
                alert("Please Select Admission Batch To.");
                return false;
            }


            else if (confirm("Are you sure ,You want to Copy Standard Fees ?"))
                return true;
            else
                return false
        }
    </script>

</asp:Content>


