<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    Title="Standard Fee Definition" CodeFile="StandardFees.aspx.cs" Inherits="Payments_StandardFeeDefinition" %>

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
                        <div class="box-header with-border">
                            <h3 class="box-title">Standard  Fees</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-6 col-md-12 col-12">
                                        <div class="row">
                                            <div class="form-group col-lg-6 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Session No. </label>
                                                </div>
                                                <asp:DropDownList ID="ddlHostelSessionNo" runat="server" CssClass="form-control" AppendDataBoundItems="True"
                                                    TabIndex="1" data-select2-enable="true">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvHostelSessionNo" runat="server" ErrorMessage="Please Select Hostel Session"
                                                    ControlToValidate="ddlHostelSessionNo" Display="None" InitialValue="0" ValidationGroup="show" />
                                            </div>

                                            <div class="form-group col-lg-6 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Hostel </label>
                                                </div>
                                                <asp:DropDownList ID="ddlHostel" runat="server" CssClass="form-control" AppendDataBoundItems="True"
                                                    TabIndex="2" data-select2-enable="true" OnSelectedIndexChanged="ddlHostel_SelectedIndexChanged" AutoPostBack="true">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please Select Hostel "
                                                    ControlToValidate="ddlHostel" Display="None" InitialValue="0" ValidationGroup="show" />
                                            </div>

                                            <div class="form-group col-lg-6 col-md-6 col-12" id="HostelType" runat="server" visible="true">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Hostel Type </label>
                                                </div>
                                                <asp:DropDownList ID="ddlHosteltype" runat="server" CssClass="form-control" AppendDataBoundItems="True"
                                                    TabIndex="3" data-select2-enable="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    <asp:ListItem Value="1">Boys Hostel</asp:ListItem>
                                                    <asp:ListItem Value="2">Girls Hostel</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>

                                            <div class="col-md-12 col-lg-12 col-12" id="Receipttype" runat="server" visible="true">
                                                <div class="row">
                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Receipt Type </label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlReceiptType" runat="server" AppendDataBoundItems="true"
                                                            CssClass="form-control" TabIndex="4" OnSelectedIndexChanged="ddlReceiptType_SelectedIndexChanged"
                                                            AutoPostBack="true" data-select2-enable="true" />
                                                        <asp:RequiredFieldValidator ID="valReceiptType" runat="server" ControlToValidate="ddlReceiptType"
                                                            Display="None" ErrorMessage="Please select receipt type." InitialValue="0" SetFocusOnError="true"
                                                            ValidationGroup="show" />
                                                    </div>

                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Room Type </label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlRoomType" runat="server" AppendDataBoundItems="true"
                                                            CssClass="form-control" TabIndex="4"
                                                            AutoPostBack="true" data-select2-enable="true" OnSelectedIndexChanged="ddlRoomType_SelectedIndexChanged" />
                                                        <asp:RequiredFieldValidator ID="valddlRoomType" runat="server" ControlToValidate="ddlRoomType"
                                                            Display="None" ErrorMessage="Please select Room type." InitialValue="0" SetFocusOnError="true"
                                                            ValidationGroup="show" />
                                                    </div>

                                                    <div class="form-group col-lg-6 col-md-6 col-12" id="Degree" runat="server" visible="true">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Degree </label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" TabIndex="5" CssClass="form-control"
                                                            OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" AutoPostBack="true" data-select2-enable="true" />
                                                    </div>

                                                    <div class="form-group col-lg-6 col-md-6 col-12" id="RoomCapacity" runat="server" visible="true">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Room Capacity </label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlRoomCapacity" runat="server" AppendDataBoundItems="true" CssClass="form-control"
                                                            TabIndex="6" AutoPostBack="true" OnSelectedIndexChanged="ddlRoomCapacity_SelectedIndexChanged" data-select2-enable="true" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group col-lg-12 col-md-12 col-12">
                                                <ul class="list-group list-group-unbordered">
                                                    <li class="list-group-item"><b>Fee Name :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblFeeName" runat="server" Text="" Font-Bold="True" />
                                                        </a>
                                                    </li>
                                                </ul>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-6 col-md-12 col-12">
                                        <div class="row">
                                            <div class="col-12">
                                                <div class="sub-heading pb-0">
                                                    <h5>Search Fee Item By Name</h5>
                                                </div>
                                            </div>
                                            <div class="col-lg-12 col-md-12 col-12 mb-1">
                                                <asp:TextBox ID="txtSearchBox" runat="server" onkeyup="javascript:GetMachingListboxItem(this);"
                                                    CssClass="form-control" TabIndex="6" />
                                            </div>

                                            <div class="form-group col-lg-12 col-md-12 col-12">
                                                <asp:ListBox ID="lstFeesItems" runat="server" AutoPostBack="true" TabIndex="7" Style="min-height: 200px; max-height: 200px; overflow: scroll;" CssClass="form-control"
                                                    OnSelectedIndexChanged="lstFeesItems_SelectedIndexChanged"></asp:ListBox>
                                            </div>
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

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnShow" runat="server" Text="Show Fee Definition" ValidationGroup="show"
                                    OnClick="btnShow_Click" TabIndex="5" CssClass="btn btn-primary" />
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click"
                                    TabIndex="20" ValidationGroup="submit" CssClass="btn btn-primary" />
                                <asp:Button ID="btnReport" runat="server" Text="Report" OnClick="btnReport_Click"
                                    TabIndex="22" Enabled="false" CssClass="btn btn-info" ValidationGroup="submit" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                                    OnClick="btnCancel_Click" TabIndex="21" CssClass="btn btn-warning" />

                                <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="show" />
                            </div>

                            <%-- Fee Entry Grid --%>
                            <div class="col-12 mt-3">
                                <div class="table-responsive">
                                    <asp:Panel runat="server" ID="pnl">
                                        <asp:ListView ID="lv" runat="server">
                                            <LayoutTemplate>
                                                <table id="tblFeeEntry" class="table table-striped table-bordered nowrap" style="width: 100%">
                                                    <thead class="bg-light-blue">
                                                        <tr>

                                                            <th>Items
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
                                                            <%--<th>
                                    Amount
                                </th>--%>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </tbody>
                                                    <tbody>
                                                        <tr>
                                                            <td>&nbsp;&nbsp;TOTAL  :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtSem1TotalAmt" Width="90px" onkeydown="javascript:return false;" runat="server"
                                                                    AutoCompleteType="Disabled" CssClass="form-control editable_listview_input" Font-Bold="true" />

                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtSem2TotalAmt" Width="90px" onkeydown="javascript:return false;" runat="server"
                                                                    AutoCompleteType="Disabled" CssClass="form-control editable_listview_input" Font-Bold="true" />

                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtSem3TotalAmt" Width="90px" onkeydown="javascript:return false;" runat="server"
                                                                    AutoCompleteType="Disabled" CssClass="form-control editable_listview_input" Font-Bold="true" />

                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtSem4TotalAmt" Width="90px" onkeydown="javascript:return false;" runat="server"
                                                                    AutoCompleteType="Disabled" CssClass="form-control editable_listview_input" Font-Bold="true" />

                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtSem5TotalAmt" Width="90px" onkeydown="javascript:return false;" runat="server"
                                                                    AutoCompleteType="Disabled" CssClass="form-control editable_listview_input" Font-Bold="true" />

                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtSem6TotalAmt" Width="90px" onkeydown="javascript:return false;" runat="server"
                                                                    AutoCompleteType="Disabled" CssClass="form-control editable_listview_input" Font-Bold="true" />

                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtSem7TotalAmt" Width="90px" onkeydown="javascript:return false;" runat="server"
                                                                    AutoCompleteType="Disabled" CssClass="form-control editable_listview_input" Font-Bold="true" />

                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtSem8TotalAmt" Width="90px" onkeydown="javascript:return false;" runat="server"
                                                                    AutoCompleteType="Disabled" CssClass="form-control editable_listview_input" Font-Bold="true" />

                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtSem9TotalAmt" Width="90px" onkeydown="javascript:return false;" runat="server"
                                                                    AutoCompleteType="Disabled" CssClass="form-control editable_listview_input" Font-Bold="true" />

                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtSem10TotalAmt" Width="90px" onkeydown="javascript:return false;" runat="server"
                                                                    AutoCompleteType="Disabled" CssClass="form-control editable_listview_input" Font-Bold="true" />

                                                            </td>
                                                        </tr>
                                                    </tbody>
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
                                                        <asp:HiddenField ID="hidBatchNo" runat="server" Value='<%# Bind("BATCHNO") %>' />
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtSem1" runat="server" Width="90px" Text='<%# Bind("SEMESTER1") %>' TabIndex="17" MaxLength="9"
                                                            AutoCompleteType="Disabled" onblur="UpdateTotalAmounts();" onkeypress="return isNumberKey(event);"
                                                            CssClass="form-control editable_listview_input" />
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" runat="server" FilterType="Numbers, Custom" ValidChars="."
                                                            TargetControlID="txtSem1" />
                                                    </td>
                                                    <%--onkeyup="IsNumeric(this);"--%>
                                                    <td>
                                                        <asp:TextBox ID="txtSem2" runat="server" Width="90px" Text='<%# Bind("SEMESTER2") %>' TabIndex="18" MaxLength="9"
                                                            AutoCompleteType="Disabled" onblur="UpdateTotalAmounts();" onkeypress="return isNumberKey(event);"
                                                            CssClass="form-control editable_listview_input" />
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Numbers, Custom" ValidChars="."
                                                            TargetControlID="txtSem2" />
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtSem3" runat="server" Width="90px" Text='<%# Bind("SEMESTER3") %>' TabIndex="19" MaxLength="9"
                                                            AutoCompleteType="Disabled" onblur="UpdateTotalAmounts();" onkeypress="return isNumberKey(event);"
                                                            CssClass="form-control editable_listview_input" />
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server" FilterType="Numbers, Custom" ValidChars="."
                                                            TargetControlID="txtSem3" />
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtSem4" runat="server" Width="90px" Text='<%# Bind("SEMESTER4") %>' TabIndex="20" MaxLength="9"
                                                            AutoCompleteType="Disabled" onblur="UpdateTotalAmounts();" onkeypress="return isNumberKey(event);"
                                                            CssClass="form-control editable_listview_input" />
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" runat="server" FilterType="Numbers, Custom" ValidChars="."
                                                            TargetControlID="txtSem4" />
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtSem5" runat="server" Width="90px" Text='<%# Bind("SEMESTER5") %>' TabIndex="21" MaxLength="9"
                                                            AutoCompleteType="Disabled" onblur="UpdateTotalAmounts();" onkeypress="return isNumberKey(event);"
                                                            CssClass="form-control editable_listview_input" />
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender14" runat="server" FilterType="Numbers, Custom" ValidChars="."
                                                            TargetControlID="txtSem5" />
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtSem6" runat="server" Width="90px" Text='<%# Bind("SEMESTER6") %>' TabIndex="22" MaxLength="9"
                                                            AutoCompleteType="Disabled" onblur="UpdateTotalAmounts();" onkeypress="return isNumberKey(event);"
                                                            CssClass="form-control editable_listview_input" />
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender15" runat="server" FilterType="Numbers, Custom" ValidChars="."
                                                            TargetControlID="txtSem6" />
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtSem7" runat="server" Width="90px" Text='<%# Bind("SEMESTER7") %>' TabIndex="23" MaxLength="9"
                                                            AutoCompleteType="Disabled" onblur="UpdateTotalAmounts();" onkeypress="return isNumberKey(event);"
                                                            CssClass="form-control editable_listview_input" />
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender16" runat="server" FilterType="Numbers, Custom" ValidChars="."
                                                            TargetControlID="txtSem7" />
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtSem8" runat="server" Width="90px" Text='<%# Bind("SEMESTER8") %>' TabIndex="24" MaxLength="9"
                                                            AutoCompleteType="Disabled" onblur="UpdateTotalAmounts();" onkeypress="return isNumberKey(event);"
                                                            CssClass="form-control editable_listview_input" />
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender17" runat="server" FilterType="Numbers, Custom" ValidChars="."
                                                            TargetControlID="txtSem8" />
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtSem9" runat="server" Width="90px" Text='<%# Bind("SEMESTER9") %>' TabIndex="25" MaxLength="9"
                                                            AutoCompleteType="Disabled" onblur="UpdateTotalAmounts();" onkeypress="return isNumberKey(event);"
                                                            CssClass="form-control editable_listview_input" />
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender18" runat="server" FilterType="Numbers, Custom" ValidChars="."
                                                            TargetControlID="txtSem9" />
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtSem10" runat="server" Width="90px" Text='<%# Bind("SEMESTER10") %>' TabIndex="26" MaxLength="9"
                                                            AutoCompleteType="Disabled" onblur="UpdateTotalAmounts();" onkeypress="return isNumberKey(event);"
                                                            CssClass="form-control editable_listview_input" />
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender19" runat="server" FilterType="Numbers, Custom" ValidChars="."
                                                            TargetControlID="txtSem10" />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <AlternatingItemTemplate>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblFeeHead" runat="server" Text='<%# Bind("FEE_LONGNAME") %>' />
                                                        <asp:HiddenField ID="hidFeeCatNo" runat="server" Value='<%# Bind("FEE_CAT_NO") %>' />
                                                        <asp:HiddenField ID="hidFeeHead" runat="server" Value='<%# Bind("FEE_HEAD") %>' />
                                                        <asp:HiddenField ID="hidFeeDesc" runat="server" Value='<%# Bind("FEE_DESCRIPTION") %>' />
                                                        <asp:HiddenField ID="hidSrNo" runat="server" Value='<%# Bind("SRNO") %>' />
                                                        <asp:HiddenField ID="hidRecieptCode" runat="server" Value='<%# Bind("RECIEPT_CODE") %>' />
                                                        <asp:HiddenField ID="hidDegreeNo" runat="server" Value='<%# Bind("DEGREENO") %>' />
                                                        <asp:HiddenField ID="hidBatchNo" runat="server" Value='<%# Bind("BATCHNO") %>' />
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtSem1" runat="server" Width="90px" Text='<%# Bind("SEMESTER1") %>' TabIndex="17" MaxLength="9"
                                                            AutoCompleteType="Disabled" onblur="UpdateTotalAmounts();" onkeypress="return isNumberKey(event);"
                                                            CssClass="form-control editable_listview_input" />
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender19" runat="server" FilterType="Numbers, Custom" ValidChars="."
                                                            TargetControlID="txtSem1" />
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtSem2" runat="server" Width="90px" Text='<%# Bind("SEMESTER2") %>' TabIndex="18" MaxLength="9"
                                                            AutoCompleteType="Disabled" onblur="UpdateTotalAmounts();" onkeypress="return isNumberKey(event);"
                                                            CssClass="form-control editable_listview_input" />
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender20" runat="server" FilterType="Numbers, Custom" ValidChars="."
                                                            TargetControlID="txtSem2" />
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtSem3" runat="server" Width="90px" Text='<%# Bind("SEMESTER3") %>' TabIndex="19" MaxLength="9"
                                                            AutoCompleteType="Disabled" onblur="UpdateTotalAmounts();" onkeypress="return isNumberKey(event);"
                                                            CssClass="form-control editable_listview_input" />
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender21" runat="server" FilterType="Numbers, Custom" ValidChars="."
                                                            TargetControlID="txtSem3" />
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtSem4" runat="server" Width="90px" Text='<%# Bind("SEMESTER4") %>' TabIndex="20" MaxLength="9"
                                                            AutoCompleteType="Disabled" onblur="UpdateTotalAmounts();" onkeypress="return isNumberKey(event);"
                                                            CssClass="form-control editable_listview_input" />
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender22" runat="server" FilterType="Numbers, Custom" ValidChars="."
                                                            TargetControlID="txtSem4" />
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtSem5" runat="server" Width="90px" Text='<%# Bind("SEMESTER5") %>' TabIndex="21" MaxLength="9"
                                                            AutoCompleteType="Disabled" onblur="UpdateTotalAmounts();" onkeypress="return isNumberKey(event);"
                                                            CssClass="form-control editable_listview_input" />
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender23" runat="server" FilterType="Numbers, Custom" ValidChars="."
                                                            TargetControlID="txtSem5" />
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtSem6" runat="server" Width="90px" Text='<%# Bind("SEMESTER6") %>' TabIndex="22" MaxLength="9"
                                                            AutoCompleteType="Disabled" onblur="UpdateTotalAmounts();" onkeypress="return isNumberKey(event);"
                                                            CssClass="form-control editable_listview_input" />
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender24" runat="server" FilterType="Numbers, Custom" ValidChars="."
                                                            TargetControlID="txtSem6" />
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtSem7" runat="server" Width="90px" Text='<%# Bind("SEMESTER7") %>' TabIndex="23" MaxLength="9"
                                                            AutoCompleteType="Disabled" onblur="UpdateTotalAmounts();" onkeypress="return isNumberKey(event);"
                                                            CssClass="form-control editable_listview_input" />
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender25" runat="server" FilterType="Numbers, Custom" ValidChars="."
                                                            TargetControlID="txtSem7" />
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtSem8" runat="server" Width="90px" Text='<%# Bind("SEMESTER8") %>' TabIndex="24" MaxLength="9"
                                                            AutoCompleteType="Disabled" onblur="UpdateTotalAmounts();" onkeypress="return isNumberKey(event);"
                                                            CssClass="form-control editable_listview_input" />
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender26" runat="server" FilterType="Numbers, Custom" ValidChars="."
                                                            TargetControlID="txtSem8" />
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtSem9" runat="server" Width="90px" Text='<%# Bind("SEMESTER9") %>' TabIndex="25" MaxLength="9"
                                                            AutoCompleteType="Disabled" onblur="UpdateTotalAmounts();" onkeypress="return isNumberKey(event);"
                                                            CssClass="form-control editable_listview_input" />
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender27" runat="server" FilterType="Numbers, Custom" ValidChars="."
                                                            TargetControlID="txtSem9" />
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtSem10" runat="server" Width="90px" Text='<%# Bind("SEMESTER10") %>' TabIndex="26" MaxLength="9"
                                                            AutoCompleteType="Disabled" onblur="UpdateTotalAmounts();" onkeypress="return isNumberKey(event);"
                                                            CssClass="form-control editable_listview_input" />
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender28" runat="server" FilterType="Numbers, Custom" ValidChars="."
                                                            TargetControlID="txtSem10" />
                                                    </td>
                                                </tr>
                                            </AlternatingItemTemplate>
                                        </asp:ListView>
                                    </asp:Panel>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
            <div id="divMsg" runat="server">
            </div>

            <script type="text/javascript">
                function ValidateCheckBoxList(sender, args) {
                    var checkBoxList = document.getElementById("<%=ddlDegree.ClientID %>");
                    var isValid = false;
                    if (checkBoxList != null) {
                        var checkboxes = checkBoxList.getElementsByTagName("input");

                        for (var i = 0; i < checkboxes.length; i++) {
                            if (checkboxes[i].checked) {
                                isValid = true;
                                break;
                            }
                        }
                        args.IsValid = isValid;
                    }
                    else {

                        args.IsValid = isValid;
                    }
                }

                function CheckAll(headchk) {
                    var frm = document.forms[0]
                    for (i = 0; i < document.forms[0].elements.length; i++) {
                        var e = frm.elements[i];
                        if (e.type == 'checkbox') {
                            if (headchk.checked == true)
                                e.checked = true;
                            else
                                e.checked = false;
                        }
                    }
                }
            </script>

            <script type="text/javascript" language="javascript">
                // Move an element directly on top of another element (and optionally
                // make it the same size)
                function Cover(bottom, top, ignoreSize) {
                    var location = Sys.UI.DomElement.getLocation(bottom);
                    top.style.position = 'absolute';
                    top.style.top = location.y + 'px';
                    top.style.left = location.x + 'px';
                    if (!ignoreSize) {
                        top.style.height = bottom.offsetHeight + 'px';
                        top.style.width = bottom.offsetWidth + 'px';
                    }
                }
            </script>

            <script language="javascript" type="text/javascript">

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

                function isNumberKey(evt) {
                    var charCode = (evt.which) ? evt.which : event.keyCode
                    if (charCode > 31 && (charCode < 48 || charCode > 57) && (charCode < 46 || charCode > 48))
                        return false;

                    return true;
                }
            </script>
            <script type="text/javascript" language="javascript">

                function UpdateTotalAmounts() {
                    try {
                        var totalFeeAmt = 0.00;
                        var dataRows = null;

                        if (document.getElementById('tblFeeEntry') != null)
                            dataRows = document.getElementById('tblFeeEntry').getElementsByTagName('tr');

                        if (dataRows != null) {
                            for (sem = 1; sem <= 10; sem++) {
                                totalFeeAmt = 0.00;
                                for (i = 1; i < (dataRows.length - 1) ; i++) {
                                    var dataCellCollection = dataRows.item(i).getElementsByTagName('td');
                                    var dataCell = dataCellCollection.item(sem);
                                    var controls = dataCell.getElementsByTagName('input');
                                    var txtAmt = controls.item(0).value;
                                    if (txtAmt != '')
                                        totalFeeAmt += parseFloat(txtAmt);

                                    if ((i + 2) == dataRows.length)
                                        document.getElementById('ctl00_ContentPlaceHolder1_lv_txtSem' + sem + 'TotalAmt').value = totalFeeAmt.toString();
                                }
                            }
                        }
                    }
                    catch (e) {
                        alert("Error: " + e.description);
                    }
                }
            </script>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
