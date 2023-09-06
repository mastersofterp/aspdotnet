<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="DisposableItemIssue.aspx.cs" Inherits="Health_Transaction_DisposableItemIssue" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--  <script src="../../JAVASCRIPTS/JScriptAdmin_Module.js" type="text/javascript"></script>
    <link href="../../CSS/master.css" rel="stylesheet" />--%>
    <script type="text/javascript">
        ; debugger
        function clientShowing(source, args) {
            source._popupBehavior._element.style.zIndex = 100000;
        }
    </script>

   <%-- <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updActivity"
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
    </div>--%>
    <asp:UpdatePanel ID="updActivity" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">DISPOSABLE ITEM ISSUE</h3>
                        </div>
                        <div class="box-body">
                            <asp:Panel ID="pnlAdd" runat="server">
                                <div class="col-12">
                                  <%--  <div class="panel panel-heading">Disposable Item Issue</div>--%>
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Item Name</label>
                                            </div>
                                            <asp:TextBox ID="txtItemName" runat="server" CssClass="form-control" TabIndex="9" placeholder="Enter Character to Search"
                                                AutoPostBack="true" OnTextChanged="txtItemName_TextChanged"></asp:TextBox>
                                            <asp:HiddenField ID="hfItemName" runat="server" />
                                            <ajaxToolKit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server"
                                                TargetControlID="txtItemName"
                                                MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="100"
                                                ServiceMethod="GetItemName" OnClientShowing="clientShowing" OnClientItemSelected="ItemName">
                                            </ajaxToolKit:AutoCompleteExtender>
                                            <asp:RequiredFieldValidator ID="rfvItemName" runat="server" ControlToValidate="txtItemName"
                                                Display="None" ErrorMessage="Enter Item Name." SetFocusOnError="True"
                                                ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Issue Date</label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon" id="ImgBntCalc">
                                                    <i class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtIssueDate" runat="server" CssClass="form-control" TabIndex="2"></asp:TextBox>
                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" PopupButtonID="ImgBntCalc"
                                                    TargetControlID="txtIssueDate">
                                                </ajaxToolKit:CalendarExtender>
                                                <ajaxToolKit:MaskedEditExtender ID="medt" runat="server"
                                                    AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                                    MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtIssueDate" ClearMaskOnLostFocus="true">
                                                </ajaxToolKit:MaskedEditExtender>
                                                <ajaxToolKit:MaskedEditValidator ID="MEVDate" runat="server" ControlExtender="medt" ControlToValidate="txtIssueDate"
                                                    EmptyValueMessage="Please Enter Issue Date" IsValidEmpty="true" ErrorMessage="Please Enter Valid Date In [dd/MM/yyyy] format"
                                                    InvalidValueMessage="Please Enter Valid Date In [dd/MM/yyyy] format" Display="None" Text="*" ValidationGroup="Submit">                                                        
                                                </ajaxToolKit:MaskedEditValidator>
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Available Qty</label>
                                            </div>
                                            <asp:TextBox ID="txtAvailableQty" runat="server" CssClass="form-control" MaxLength="20" Enabled="false"></asp:TextBox>

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Issue Qty</label>
                                            </div>
                                            <asp:TextBox ID="txtIssueQty" runat="server" CssClass="form-control" MaxLength="10" TabIndex="3"></asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftbeIssueQty" runat="server" FilterType="Numbers" TargetControlID="txtIssueQty"
                                                ValidChars="">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                            <asp:RequiredFieldValidator ID="rfvIssueQty" runat="server" ControlToValidate="txtIssueQty" Display="None"
                                                ErrorMessage="Please enter issue quantity." SetFocusOnError="true"
                                                ValidationGroup="Submit" />
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Remark</label>
                                            </div>
                                            <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control" MaxLength="499" TabIndex="3"
                                                TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" TabIndex="4" CssClass="btn btn-outline-primary" OnClick="btnSubmit_Click"
                                        ValidationGroup="Submit" />
                                    <asp:Button ID="btnRport" runat="server" Text="Report" TabIndex="6" CssClass="btn btn-outline-info" OnClick="btnRport_Click" />
                                    <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false"
                                        ValidationGroup="Submit" />
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" TabIndex="5" CssClass="btn btn-outline-danger"
                                        CausesValidation="false" />
                                </div>

                            </asp:Panel>

                            <asp:Panel ID="pnlList" runat="server">
                                <div class="col-12 mt-3">
                                    <asp:ListView ID="lvDisposableItem" runat="server">
                                        <EmptyDataTemplate>
                                            <p class="text-center text-bold">
                                                <asp:Label ID="lblEmpty" Text="No Records Found" Style="color: Red" runat="server"></asp:Label>
                                            </p>
                                        </EmptyDataTemplate>
                                        <LayoutTemplate>
                                            <div id="lgv1">
                                                <div class="sub-heading">
                                                    <h5>Disposable Item Issue List</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>Item Name
                                                            </th>
                                                            <th>Issue Qty
                                                            </th>
                                                            <th>Issue Date
                                                            </th>
                                                            <th>Remark
                                                            </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </tbody>
                                                </table>
                                            </div>
                                            </div>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <%# Eval("ITEMNAME") %>
                                                </td>
                                                <td>
                                                    <%# Eval("QTY_ISSUE") %>                                                             
                                                </td>
                                                <td>
                                                    <%# Eval("ISSUE_DATE", "{0:dd-MMM-yyyy}")%>
                                                </td>
                                                <td>
                                                    <%# Eval("REMARK") %>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript" language="javascript">
        function validateNumeric(txt) {
            if (isNaN(txt.value)) {
                txt.value = '';
                alert('Only Numeric Characters Allowed!');
                txt.focus();
                return;
            }
        }
        function validateAlphabet(txt) {
            var expAlphabet = /^[A-Za-z]+$/;
            if (txt.value.search(expAlphabet) == -1) {
                txt.value = txt.value.substring(0, (txt.value.length) - 1);
                txt.value = '';
                txt.focus = true;
                alert("Only Alphabets allowed!");
                return false;
            }
            else
                return true;
        }


        function ItemName(source, eventArgs) {
            var idno = eventArgs.get_value().split("*");
            var Name = idno[0].split("-");
            document.getElementById('ctl00_ContentPlaceHolder1_txtItemName').value = idno[1];
            document.getElementById('ctl00_ContentPlaceHolder1_hfItemName').value = Name[0];
        }
    </script>
</asp:Content>

