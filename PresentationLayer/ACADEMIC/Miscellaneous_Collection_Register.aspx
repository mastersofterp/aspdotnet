<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Miscellaneous_Collection_Register.aspx.cs" Inherits="ACADEMIC_Miscellaneous_Collection_Register"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updpnlMain"
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

    <asp:UpdatePanel ID="updpnlMain" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <%--<h3 class="box-title">MISCELLANEOUS COLLECTION REGISTER</h3>--%>
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-12 col-md-12 col-12">
                                        <asp:RadioButtonList ID="rdbReports" runat="server" RepeatDirection="Horizontal" RepeatColumns="4"
                                            OnSelectedIndexChanged="rdbReports_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Selected="True" Value="1">&nbsp;Collection Register&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                            <%-- <asp:ListItem Value="2">&nbsp;Summary Register&nbsp;&nbsp;&nbsp;</asp:ListItem>--%>
                                            <asp:ListItem Value="3">&nbsp;HeadWise Collection&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                            <%-- <asp:ListItem Value="4">&nbsp;BankWise Collection&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                            <asp:ListItem Value="6">&nbsp;Transfer Fees Collection&nbsp;&nbsp;&nbsp;</asp:ListItem>--%>
                                            <asp:ListItem Value="5">&nbsp;Cancel/Reprint&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                            <asp:ListItem Value="6">&nbsp;Bulk Miscellaneous Fees Report</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Cash Book</label>
                                        </div>
                                        <asp:DropDownList ID="ddlCashbook" runat="server"
                                            AppendDataBoundItems="true" OnSelectedIndexChanged="ddlCashbook_SelectedIndexChanged" AutoPostBack="true" TabIndex="1" CssClass="form-control" data-select2-enable="true">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCashbook" ValidationGroup="Show"
                                            SetFocusOnError="true" ErrorMessage="Please Select Cash Book" Display="None" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <asp:Panel ID="pnlPaymenttype" runat="server">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Transaction Type</label>
                                            </div>
                                            <asp:DropDownList ID="ddlPaytype" runat="server" AutoPostBack="true" AppendDataBoundItems="true" TabIndex="2" CssClass="form-control" data-select2-enable="true"
                                                OnSelectedIndexChanged="ddlPaytype_SelectedIndexChanged">
                                                <asp:ListItem Value="">Please Select</asp:ListItem>
                                                <asp:ListItem Value="P">Payment</asp:ListItem>
                                                <asp:ListItem Value="R">Receipt</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlPaytype"
                                                ErrorMessage="Please Select Transaction Type" Display="None" InitialValue="0" ValidationGroup="Show"></asp:RequiredFieldValidator>
                                        </asp:Panel>
                                    </div>

                                    <div class="form-group col-lg-6 col-md-12 col-12">
                                        <asp:Panel ID="pnldate" runat="server">
                                            <div class="row">
                                                <div class="form-group col-lg-6 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>From Date</label>
                                                    </div>
                                                    <div class="input-group">
                                                        <div class="input-group-addon" id="imgCalDDDate">
                                                            <i class="fa fa-calendar text-blue"></i>
                                                        </div>
                                                        <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control" TabIndex="3" />

                                                        <ajaxToolKit:CalendarExtender ID="ceDDDate" runat="server" Format="dd/MM/yyyy" PopupButtonID="imgCalDDDate"
                                                            TargetControlID="txtFromDate" />
                                                        <ajaxToolKit:MaskedEditExtender ID="meeDDDate" runat="server" ErrorTooltipEnabled="true"
                                                            Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true" OnInvalidCssClass="errordate"
                                                            TargetControlID="txtFromDate" />
                                                        <%-- <ajaxToolKit:MaskedEditValidator ID="mevDDDate" runat="server" ControlExtender="meeDDDate"
                                                            ControlToValidate="txtFromDate" Display="Dynamic" EmptyValueBlurredText="*" EmptyValueMessage="Demand draft date is required"
                                                            InvalidValueBlurredMessage="*" InvalidValueMessage="Demand draft date is invalid"
                                                            IsValidEmpty="False" ValidationGroup="dd_info" />--%>
                                                    </div>
                                                </div>
                                                <div class="form-group col-lg-6 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>To Date</label>
                                                    </div>
                                                    <div class="input-group">
                                                        <div class="input-group-addon" id="imgCalDDDate1">
                                                            <i class="fa fa-calendar text-blue"></i>
                                                        </div>
                                                        <asp:TextBox ID="txtTodate" runat="server" CssClass="form-control" TabIndex="4" />
                                                        <%--<asp:Image ID="Image1" runat="server" src="../images/calendar.png" Style="cursor: hand" />--%>
                                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                                            PopupButtonID="imgCalDDDate1" TargetControlID="txtTodate" />
                                                        <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" ErrorTooltipEnabled="true"
                                                            Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true" OnInvalidCssClass="errordate"
                                                            TargetControlID="txtTodate" />
                                                        <%--  <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="meeDDDate"
                                                            ControlToValidate="txtTodate" Display="Dynamic"  EmptyValueMessage="Demand draft date is required"
                                                            InvalidValueBlurredMessage="*" InvalidValueMessage="Demand draft date is invalid"
                                                            IsValidEmpty="False" ValidationGroup="dd_info" />--%>
                                                    </div>
                                                </div>
                                            </div>

                                        </asp:Panel>




                                        <div class="form-group col-lg-6 col-md-6 col-12">
                                            <asp:Panel ID="pnlpaytype" runat="server" Visible="false">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Pay Type (C/D/T)</label>
                                                </div>
                                                <asp:TextBox ID="txtPayType" runat="server" MaxLength="1" TabIndex="11" onkeyup="IsPayType(this);" ToolTip="Please enter only 'C' for Cash payment OR 'D' for payment through Demand Drafts OR 'T' for Transfer Payment to Online Transfer"
                                                    CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="valPayType" runat="server" ControlToValidate="txtPayType"
                                                    Display="None" ErrorMessage="Please enter only 'C' for Cash payment OR 'D' for payment through Demand Drafts OR 'T' for Transfer Payment to Online Transfer"
                                                    SetFocusOnError="true" ValidationGroup="Show" />
                                            </asp:Panel>
                                        </div>
                                        <asp:Panel ID="pnlbutton" runat="server">
                                            <div class="col-12" style="display: none">
                                                <div class="row">
                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>
                                                                <asp:Label ID="lblCounter" runat="server" Text="Receipt No"></asp:Label></label></label>
                                                        </div>
                                                        <asp:TextBox ID="txtCounter" runat="server" CssClass="validate[required]" placeholder="Receipt No"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtCounter" ValidationGroup="Show"
                                                            ErrorMessage="Please Enter Receipt No " Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                    </div>
                                    <asp:Panel ID="Panelrdocancel" runat="server">
                                        <div class="form-group col-lg-12 col-md-12 col-12">
                                            <asp:RadioButtonList ID="rdocancel" runat="server" RepeatDirection="Horizontal"
                                                AutoPostBack="true">
                                                <%--<asp:ListItem  Value="">&nbsp;Cancel&nbsp;&nbsp;&nbsp;</asp:ListItem>--%>
                                                <asp:ListItem Value="1">&nbsp;Cancel&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                                <asp:ListItem Value="2">&nbsp;Without Cancel&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                            </asp:RadioButtonList>

                                        </div>
                                    </asp:Panel>
                                </div>
                            </div>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <div class="row" id="usnno" runat="server" visible="false">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Search Criteria</label>
                                            </div>
                                            <%--onchange=" return ddlSearch_change();"--%>
                                            <asp:DropDownList runat="server" class="form-control" ID="ddlSearchPanel" AutoPostBack="true" OnSelectedIndexChanged="ddlSearchPanel_SelectedIndexChanged"
                                                AppendDataBoundItems="true" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlSearchPanel" ValidationGroup="SEARCH"
                                                ErrorMessage="Please Select Search Criteria " Display="None"></asp:RequiredFieldValidator>
                                            <asp:HiddenField ID="hdfidno" runat="server" />
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divpanel">
                                            <asp:Panel ID="pnltextbox" runat="server">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Search String</label>
                                                </div>
                                                <div id="divtxt" runat="server" style="display: block">
                                                    <asp:TextBox ID="txtSearchPanel" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtSearchPanel" ValidationGroup="SEARCH"
                                                        ErrorMessage="Please Enter Search String " Display="None"></asp:RequiredFieldValidator>
                                                    <asp:HiddenField ID="HiddenField1" runat="server" />
                                                </div>
                                            </asp:Panel>

                                            <asp:Panel ID="pnlDropdown" runat="server">
                                                <div id="divDropDown" runat="server" style="display: none">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <asp:Label ID="lblDropdown" Style="font-weight: bold" runat="server"></asp:Label>
                                                    </div>
                                                    <asp:DropDownList runat="server" class="form-control" ID="ddlDropdown" AppendDataBoundItems="true" data-select2-enable="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </asp:Panel>
                                        </div>
                                        <div class="form-group col-lg-3 col-12">
                                            <div class="label-dynamic">
                                                <label></label>
                                            </div>
                                            <asp:Button ID="btnSearchPanel" runat="server" Text="Search" ValidationGroup="SEARCH" CausesValidation="false" CssClass="btn btn-primary" OnClick="btnSearchPanel_Click" />
                                            <asp:Button ID="btnClosePanel" runat="server" Text="Clear" CausesValidation="false" CssClass="btn btn-warning" OnClick="btnClosePanel_Click" />

                                            <asp:ValidationSummary ID="ValidationSummary1" DisplayMode="List" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="SEARCH" />
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>

                            <div class="col-lg-3 col-md-6 col-12">
                                <asp:Panel ID="pnlhead" runat="server" Visible="false">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label id="lblHead" runat="server">Head</label>
                                    </div>
                                    <asp:DropDownList ID="ddlHead" runat="server" AppendDataBoundItems="true" data-select2-enable="true">
                                        <asp:ListItem>Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                </asp:Panel>
                            </div>

                            <%--    <div class="form-group col-lg-6 col-md-6 col-12">
                                          <asp:Panel ID="Panelsortby" runat="server" Visible="false" >
                                                <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Sort By</label>
                                        </div>
                                        <asp:RadioButton ID="rbDate" runat="server" GroupName="sort" Text="Date Wise" Checked="True"  AutoPostBack="true" OnCheckedChanged="rbDate_CheckedChanged" />
                                        <asp:RadioButton ID="rbpayment" runat="server" GroupName="sort" Text=" Payment type"  AutoPostBack="true" OnCheckedChanged="rbpayment_CheckedChanged" />
                                        <asp:RadioButton ID="rbreceiptnumber" runat="server" GroupName="sort" Text="Receipt number"  AutoPostBack="true" OnCheckedChanged="rbreceiptnumber_CheckedChanged" />
                                    </asp:Panel>
                              </div>--%>

                            <div class="col-12 btn-footer">
                                <asp:Panel ID="pnlReport" runat="server">
                                    <asp:Button ID="ImageButton1" runat="server" CssClass="btn btn-primary" ValidationGroup="Show" Visible="false"
                                        OnClick="imgbutExporttoexcel_Click" ToolTip="Export to excel" Text="Export to excel" />
                                    <asp:Button ID="ImageButton2" runat="server" CssClass="btn btn-primary"
                                        OnClick="imgbutExporttoWord_Click" Visible="false" ToolTip="Export to word" />

                                    <asp:Button ID="ImageButton3" runat="server" CssClass="btn btn-primary" ValidationGroup="Show"
                                        OnClick="imgbutExporttoPdf_Click" ToolTip="Export to PDF" Text="Export to PDF" Visible="false" />
                                    <asp:Button ID="btnExcelRcpit" runat="server" Text="Miscellaneous Fees Excel Report" CssClass="btn btn-primary" ValidationGroup="Show"
                                        Visible="false" OnClick="btnExcelRcpit_Click" />
                                    <asp:Button ID="btnDCRreport" runat="server" Text="Miscellaneous Fees DCR Report" CssClass="btn btn-primary" ValidationGroup="Show" Visible="false"
                                        ToolTip="Miscellaneous Fees DCR Excel Report" OnClick="btnDCRreport_Click" />
                                    <asp:ImageButton ID="ImageButton4" Visible="false" runat="server"
                                        OnClick="btnBack_Click" ToolTip="Back" />
                                    <asp:Button ID="btnBulkReport" Visible="false" runat="server" OnClick="btnBulkReport_Click1" CssClass="btn btn-primary" Text="Bulk Miscellaneous Fees Report" ValidationGroup="Show" />
                                    <asp:Button ID="btnSummaryReport" runat="server" CssClass="btn btn-primary"
                                        OnClick="btnSummaryReport_Click" Visible="false" Text="Fee Collection Summary Report" ValidationGroup="Show" />
                                    <asp:ValidationSummary ID="validationsummary2" runat="server" ValidationGroup="Show" ShowMessageBox="true"
                                        ShowSummary="false" />
                                </asp:Panel>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Panel ID="Pnlreportbtn" runat="server" Visible="false">
                                    <asp:Button ID="btnReprint" runat="server" Text="Receipt Reprint" OnClick="btnReprint_Click" CssClass="btn btn-info" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel Receipt" OnClientClick="displayRadioValue();" OnClick="btnCancel_Click" CssClass="btn btn-warning" />
                                    <asp:Button ID="btnClear" runat="server" Text="Cancel" OnClick="btnClear_Click" CausesValidation="false" CssClass="btn btn-warning" />
                                </asp:Panel>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:HiddenField ID="hdnCount" Value="0" runat="server" />
                            </div>

                            <div class="col-12">
                                <asp:Panel ID="Panel1" runat="server">
                                    <asp:ListView ID="lvPaidReceipts" runat="server">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Search Results</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="tblSearchResults">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Select
                                                        </th>
                                                        <th>Receipt No
                                                        </th>
                                                        <th>Receipt Date
                                                        </th>
                                                        <th>Transaction Type
                                                        </th>
                                                        <th>Name
                                                        </th>
                                                        <th>Status
                                                        </th>
                                                        <th>Amount
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
                                                    <input id="rdoSelectRecord" value='<%# Eval("MISCDCRSRNO") %>' name="Receipts" type="radio"
                                                        onclick="ShowRemark(this);" />
                                                    <asp:HiddenField ID="hidRemark" runat="server" Value='<%# Eval("RECNO") %>' />
                                                    <asp:HiddenField ID="hidDcrNo" runat="server" Value='<%# Eval("MISCDCRSRNO") %>' />

                                                </td>
                                                <td>
                                                    <%# Eval("RECNO") %>
                                                </td>
                                                <td>
                                                    <%# (Eval("AUDITDATE").ToString() != string.Empty) ? ((DateTime)Eval("AUDITDATE")).ToShortDateString() : Eval("AUDITDATE")%>
                                                </td>
                                                <td>
                                                    <%# Eval("PAY_TYPE") %>
                                                </td>
                                                <td>
                                                    <%# Eval("NAME") %>
                                                </td>
                                                <td>
                                                    <%# Eval("STATUS")%>
                                                </td>
                                                <td>
                                                    <%# Eval("CHDDAMT")%>
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
            <asp:PostBackTrigger ControlID="btnDCRreport" />
        </Triggers>
    </asp:UpdatePanel>

    <div id="divMsg" runat="server">
    </div>

    <script type="text/javascript">
        function receiptno(textbox) {

            textbox.value = ''
            alert("Please Enter Receipt Number");
            textbox.focus();

            return;
        }
        function IsPayType(txt) {
            var ValidChars = "CDTcdt";
            var num = true;
            var mChar;
            cnt = 0

            for (i = 0; i < txt.value.length && num == true; i++) {
                mChar = txt.value.charAt(i);

                if (ValidChars.indexOf(mChar) == -1) {
                    num = false;
                    txt.value = '';
                    alert("Please enter only 'C' for Cash payment OR 'D' for payment through Demand Drafts OR 'T' for Transfer Payment to Online Transfer")
                    txt.select();
                    txt.focus();
                }
            }
            return num;
        }

        function displayRadioValue() {
            var ele = document.getElementsByName('rdoSelectRecord');

            for (i = 0; i < ele.length; i++) {
                if (ele[i].checked) {

                }
                else {
                    alert("Please select a receipt to cancel.");
                }
            }
        }

        //function CancelReceipt()
        //{
        //    try {
        //        if (ValidateRecordSelection())
        //        {             
        //            if (confirm("Do you really want to cancel this receipt.") && confirm("If you cancel this receipt, it will not be considered as paid."))
        //            {
        //                __doPostBack("CancelReceipt", "");
        //            }
        //            else
        //                alert('Please enter reason of cancelling receipt.');
        //        }
        //        else
        //            alert("Please select a receipt to cancel.");
        //    }
        //    catch (e) {
        //        alert("Error: " + e.description);
        //    }
        //}
        //THIS SCRIPT IS USED TO SHOW SELECT DATA OF RADIOBUTTON
        function ShowRemark(rdoSelect) {
            document.getElementById('ctl00_ContentPlaceHolder1_txtCounter').value = rdoSelect.value;

            document.getElementById('<%=hdnCount.ClientID %>').value = rdoSelect.value;
        }

        function ValidateRecordSelection(headchk) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.type == 'radio') {
                    if (headchk.checked == true) {
                        e.checked = true;
                        alert('if part')
                    }
                    else {
                        alert('else part');
                        e.checked = false;
                    }
                }
            }

        }
    </script>

</asp:Content>
