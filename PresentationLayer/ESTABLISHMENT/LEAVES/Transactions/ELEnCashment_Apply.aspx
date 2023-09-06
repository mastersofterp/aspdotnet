<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="ELEnCashment_Apply.aspx.cs" Inherits="ESTABLISHMENT_LEAVES_Transactions_ELEnCashment_Apply"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="updAll" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">EL ENCASHMENT APPLY</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="col-12">
                                        <div class="sub-heading">
                                            <h5>EL Encashment Entry</h5>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12">
                                <div class="row" id="pnlAdd" runat="server">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>EL Balance</label>
                                        </div>
                                        <asp:TextBox ID="txtELBalance" Enabled="false" runat="server" CssClass="form-control" MaxLength="5" onkeyup="return check(this);"></asp:TextBox>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Apply Date</label>
                                        </div>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i id="dvcal2" runat="server" class="fa fa-calendar text-blue"></i>
                                            </div>
                                            <asp:TextBox ID="txtApplyDt" runat="server" CssClass="form-control" SelectedDate="<%# DateTime.Today %>"
                                                MaxLength="10" ToolTip="Enter Apply Date" Style="z-index: 0;"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvApplyDt" runat="server" ControlToValidate="txtApplyDt"
                                                Display="None" ErrorMessage="Please Enter Apply Date" ValidationGroup="submit"
                                                SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            <ajaxToolKit:CalendarExtender ID="ceApplyDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtApplyDt"
                                                Enabled="true" EnableViewState="true" PopupButtonID="dvcal2">
                                            </ajaxToolKit:CalendarExtender>
                                            <ajaxToolKit:MaskedEditExtender ID="meeApplyDt" runat="server" TargetControlID="txtApplyDt"
                                                Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                AcceptNegative="Left" ErrorTooltipEnabled="true" />
                                            <ajaxToolKit:MaskedEditValidator ID="mevApplyDt" runat="server" ControlExtender="meeApplyDt"
                                                ControlToValidate="txtApplyDt" EmptyValueMessage="Please Enter Apply Date" InvalidValueMessage="From Date is Invalid (Enter dd/MM/yyyy Format)"
                                                Display="None" TooltipMessage="Please Enter From Date" EmptyValueBlurredText="Empty"
                                                InvalidValueBlurredMessage="Invalid Date" ValidationGroup="submit" SetFocusOnError="true">
                                            </ajaxToolKit:MaskedEditValidator>
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Encashment Days</label>
                                        </div>
                                        <asp:TextBox ID="txtEnCashDays" runat="server" CssClass="form-control" MaxLength="5"
                                            onkeyup="return check(this);"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvEnCashDays" runat="server" ControlToValidate="txtEnCashDays"
                                            Display="None" ErrorMessage="Please Enter Encashment Days" ValidationGroup="submit"
                                            SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>

                                    <%--<div class="form-group col-md-4" id="divPending" runat="server" visible="true">
                                                        <label>Pending EL Leaves </label>
                                                        <asp:TextBox ID="txtPending" runat="server" CssClass="form-control" MaxLength="5" Enabled="false" ></asp:TextBox>                                                    
                                                    </div>--%>
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divTotDays" runat="server" visible="true">
                                        <div class="label-dynamic">
                                            <label>Total Encashment Days Approved in</label>&nbsp;<asp:Label ID="lblYear" runat="server"></asp:Label>
                                        </div>                                        
                                        <asp:TextBox ID="txtTotDays" runat="server" CssClass="form-control" MaxLength="5" Enabled="false"></asp:TextBox>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="div3" runat="server" visible="true">
                                        <div class="label-dynamic">
                                            <label>Total Encashment Days Pending in</label>&nbsp;<asp:Label ID="lblYearPending" runat="server"></asp:Label>
                                        </div>                                        
                                        <asp:TextBox ID="txtPendingEncashment" runat="server" CssClass="form-control" MaxLength="5" Enabled="false"></asp:TextBox>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="div4" runat="server" visible="true">
                                        <div class="label-dynamic">
                                            <label>Active</label>
                                        </div>
                                        <asp:CheckBox ID="chkIsActive" runat="server" Checked="true" onclick="checkVal(this);" />
                                        <%--onclick="boxdisable(appTimes);"--%>
                                    </div>

                                </div>
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="submit"
                                    CssClass="btn btn-primary" ToolTip="Click here to Submit" OnClick="btnSubmit_Click" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                                    CssClass="btn btn-warning" ToolTip="Click here to Reset" OnClick="btnCancel_Click" />
                                <asp:ValidationSummary ID="VsEnCashment" runat="server" ValidationGroup="submit"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                            </div>
                            <div class="col-12">
                                    <asp:Panel ID="PanelList" runat="server">
                                        <asp:Repeater ID="lvEnCashment" runat="server">
                                            <HeaderTemplate>
                                                <div class="sub-heading">
	                                                <h5>EnCashment List</h5>
                                                </div>
                                                <table id="table2" class="table table-striped table-bordered nowrap display" style="width:100%">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>Action
                                                            </th>
                                                            <th>Apply Date
                                                            </th>
                                                            <th>Encashment Days
                                                            </th>
                                                            <th>Status
                                                            </th>
                                                            <th>Active
                                                            </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("SRNO") %>'
                                                            AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />
                                                        <%--<asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.gif" CommandArgument='<%# Eval("SRNO") %>'
                                                        AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                                        OnClientClick="showConfirmDel(this); return false;" />--%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("APPDT")%>
                                                    </td>
                                                    <td>
                                                        <%#Eval("TOTAL_DAYS")%>
                                                    </td>
                                                    <td>
                                                        <%#Eval("STATUS")%>
                                                    </td>
                                                    <td>
                                                        <%#Eval("IsActive")%>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </tbody></table>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                        <div id="DivNote" runat="server">
                                            <div class="form-group col-sm-12">
                                                <%--<div class="text-center">
                                                <p style="color: Red; font-weight: bold">
                                                    No Record Found..!!                                                                
                                                </p>
                                            </div>--%>
                                            </div>
                                        </div>
                                    </asp:Panel>

                            </div>
                        </div>
                    </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">
        //  keeps track of the delete button for the row
        //  that is going to be removed
        var _source;
        // keep track of the popup div
        var _popup;

        function showConfirmDel(source) {
            this._source = source;
            this._popup = $find('mdlPopupDel');

            //  find the confirm ModalPopup and show it    
            this._popup.show();
        }

        function okDelClick() {
            //  find the confirm ModalPopup and hide it    
            this._popup.hide();
            //  use the cached button as the postback source
            __doPostBack(this._source.name, '');
        }

        function cancelDelClick() {
            //  find the confirm ModalPopup and hide it 
            this._popup.hide();
            //  clear the event source
            this._source = null;
            this._popup = null;
        }
    </script>
    <script type="text/javascript" language="javascript">
        //checkVal
        function checkVal(me) {
            ; debugger

            var limit = 15;
            var Bal = document.getElementById('ctl00_ContentPlaceHolder1_txtELBalance').value;
            var EnCashDays = document.getElementById('ctl00_ContentPlaceHolder1_txtEnCashDays').value;
            // var Pending = document.getElementById('ctl00_ContentPlaceHolder1_txtPending').value;
            if (Bal <= 0) {
                alert("Sorry ! Insufficient EL Balance");
                document.getElementById('ctl00_ContentPlaceHolder1_txtEnCashDays').value = '';
                return;
            }
            //var BalNew = (Bal - Pending) - EnCashDays;//8-9
            if (Bal < 0) {
                alert("Sorry ! Encashment Days Should Be Less Or Equals To Balance Leaves");
                document.getElementById('ctl00_ContentPlaceHolder1_txtEnCashDays').value = '';
                return;
            }
            if (Math.abs(EnCashDays) > Math.abs(Bal)) {
                alert("Sorry ! Encashment Days Should Not Greater Than Balance ");
                return;
            }
            var TotDaysSubmitted = document.getElementById('ctl00_ContentPlaceHolder1_txtTotDays').value;//10
            var PendingEncashment = document.getElementById('ctl00_ContentPlaceHolder1_txtPendingEncashment').value;//4
            var TotDaysSubmittedNew = Math.abs(TotDaysSubmitted) + Math.abs(PendingEncashment) + Math.abs(EnCashDays);//10+9
            if (TotDaysSubmittedNew > limit) {
                alert("Total Encashment Days Should Not Greater Than 15 Leaves");
                document.getElementById('ctl00_ContentPlaceHolder1_txtEnCashDays').value = '';
                return;
            }




        }
        function check(me) {
            ; debugger
            if (ValidateNumeric(me) == true) {
                var limit = 15;
                var Bal = document.getElementById('ctl00_ContentPlaceHolder1_txtELBalance').value;
                var EnCashDays = document.getElementById('ctl00_ContentPlaceHolder1_txtEnCashDays').value;
                // var Pending = document.getElementById('ctl00_ContentPlaceHolder1_txtPending').value;
                if (Bal <= 0) {
                    alert("Sorry ! Insufficient EL Balance");
                    document.getElementById('ctl00_ContentPlaceHolder1_txtEnCashDays').value = '';
                    return;
                }
                //var BalNew = (Bal - Pending) - EnCashDays;//8-9
                if (Bal < 0) {
                    alert("Sorry ! Encashment Days Should Be Less Or Equals To Balance Leaves");
                    document.getElementById('ctl00_ContentPlaceHolder1_txtEnCashDays').value = '';
                    return;
                }
                if (Math.abs(EnCashDays) > Math.abs(Bal)) {
                    alert("Sorry ! Encashment Days Should Not Greater Than Balance ");
                    return;
                }
                var TotDaysSubmitted = document.getElementById('ctl00_ContentPlaceHolder1_txtTotDays').value;//10
                var PendingEncashment = document.getElementById('ctl00_ContentPlaceHolder1_txtPendingEncashment').value;//4
                var TotDaysSubmittedNew = Math.abs(TotDaysSubmitted) + Math.abs(PendingEncashment) + Math.abs(EnCashDays);//10+9
                if (TotDaysSubmittedNew > limit) {
                    alert("Total Encashment Days Should Not Greater Than 15 Leaves");
                    document.getElementById('ctl00_ContentPlaceHolder1_txtEnCashDays').value = '';
                    return;
                }



            }
        }

        function ValidateNumeric(txt) {


            if (isNaN(txt.value)) {
                txt.value = txt.value.substring(0, (txt.value.length) - 1);
                txt.value = "";
                txt.focus();
                alert("Only Numeric Characters allowed");
                return false;
            }
            else {
                return true;
            }
        }

    </script>
</asp:Content>
