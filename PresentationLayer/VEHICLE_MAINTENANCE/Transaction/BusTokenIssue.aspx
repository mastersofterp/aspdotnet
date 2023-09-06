<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="BusTokenIssue.aspx.cs" Inherits="VEHICLE_MAINTENANCE_Transaction_BusTokenIssue" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%-- <script src="../../jquery/jquery-3.2.1.min.js"></script>
    <link href="../../jquery/jquery.multiselect.css" rel="stylesheet" />
    <script src="../../jquery/jquery.multiselect.js"></script>--%>
    <script type="text/javascript">
        function Count(a) {
            if (a == 1) {
                document.getElementById('<%=txtAmount40.ClientID%>').value = 40 * Number(document.getElementById('<%=txtToken40.ClientID%>').value);
            }
            else {
                document.getElementById('<%=txtAmount30.ClientID%>').value = 30 * Number(document.getElementById('<%=txtToken30.ClientID%>').value);
            }
            document.getElementById('<%=txtGrandTotal.ClientID%>').value = Number(Number(document.getElementById('<%=txtAmount40.ClientID%>').value) + Number(document.getElementById('<%=txtAmount30.ClientID%>').value));
        }
    </script>
    <script type="text/javascript">
        function PrasentDate() {
            var today = new Date();
            var dd = today.getDate();
            var mm = today.getMonth() + 1;
            var yyyy = today.getFullYear();
            if (dd < 10) {
                dd = '0' + dd;
            }
            if (mm < 10) {
                mm = '0' + mm;
            }
            document.getElementById('<%=txtcomdate.ClientID%>').value = dd + '/' + mm + '/' + yyyy;
        }
        function Clear() {

            document.getElementById('<%=txtAmount30.ClientID%>').value = '';
            document.getElementById('<%=txtAmount40.ClientID%>').value = '';
            document.getElementById('<%=txtGrandTotal.ClientID%>').value = '';
            document.getElementById('<%=txtToken30.ClientID%>').value = '';
            document.getElementById('<%=txtToken40.ClientID%>').value = '';

            document.getElementById('<%=txtFromDate.ClientID%>').value = '';
            document.getElementById('<%=txtToDate.ClientID%>').value = '';
            var today = new Date();
            var dd = today.getDate();
            var mm = today.getMonth() + 1;
            var yyyy = today.getFullYear();
            if (dd < 10) {
                dd = '0' + dd;
            }
            if (mm < 10) {
                mm = '0' + mm;
            }
            document.getElementById('<%=txtcomdate.ClientID%>').value = dd + '/' + mm + '/' + yyyy;

        }

        function ClearRptCon() {
            document.getElementById('<%=txtFromDate.ClientID%>').value = '';
            document.getElementById('<%=txtToDate.ClientID%>').value = '';
        }

        function check_date(a) {

            if (a == 2) {

                var date = document.getElementById('<%=txtcomdate.ClientID%>').value.split("/");
                var TokenDate = new Date(date[2], date[1] - 1, date[0]);
                var ToDate = new Date();

                if (ToDate < TokenDate) {

                    alert("Future Date is not allowed.");
                    PrasentDate();
                }

            }
            if (a == 0) {
                var Fdate = document.getElementById('<%=txtFromDate.ClientID%>').value.split("/");
                var Tdate = document.getElementById('<%=txtToDate.ClientID%>').value.split("/");
                var FromDate = new Date(Fdate[2], Fdate[1] - 1, Fdate[0]);
                var ToDate = new Date(Tdate[2], Tdate[1] - 1, Tdate[0]);
                if (ToDate < FromDate) {
                    alert("To Date Cannot Be Less Than From Date.");
                    var Tdate = document.getElementById('<%=txtToDate.ClientID%>').value = '';
                }
            } else {
                document.getElementById('<%=txtToDate.ClientID%>').value = '';
            }

        }

    </script>

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updPanel"
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
    <asp:UpdatePanel ID="updPanel" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">Bus Token Issue</h3>
                        </div>
                        <div class="box-body">
                            <div id="DivControls" runat="server" visible="true">
                                <div class="col-12">
                                    <%--<div class=" sub-heading">Bus Token Issue</div>--%>
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Bus Token Issue Date</label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon" id="txtcomdate2">
                                                    <i class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtcomdate" CssClass="form-control" ToolTip="Enter Complaint Date" TabIndex="1"
                                                    runat="server" Style="z-index: 0;" autocomplete="off" onchange="check_date(2);"></asp:TextBox>
                                                <ajaxToolKit:CalendarExtender ID="ceePdDt" runat="server" Enabled="true" EnableViewState="true"
                                                    Format="dd/MM/yyyy" PopupButtonID="txtcomdate2" TargetControlID="txtcomdate">
                                                </ajaxToolKit:CalendarExtender>
                                                <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" Mask="99/99/9999"
                                                    AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="true" MaskType="Date"
                                                    MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtcomdate"
                                                    ClearMaskOnLostFocus="true">
                                                </ajaxToolKit:MaskedEditExtender>
                                                <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator4" runat="server" ControlExtender="MaskedEditExtender3"
                                                    ControlToValidate="txtcomdate" EmptyValueMessage="Please Enter Bus Token Issue Date" IsValidEmpty="false"
                                                    ErrorMessage="Please Enter Valid Date In [dd/MM/yyyy] format" Display="None" Text="*"
                                                    InvalidValueMessage="Please Enter Valid Date In [dd/MM/yyyy] format" ValidationGroup="btnbustoken">                                                            
                                                </ajaxToolKit:MaskedEditValidator>
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Token 40</label>
                                            </div>
                                            <asp:TextBox ID="txtToken40" runat="server" CssClass="form-control" MaxLength="4" onkeyup="Count(1)" TabIndex="2" autocomplete="off"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="reftoken40" runat="server" ControlToValidate="txtToken40" ErrorMessage="Please Enter Token-40" Display="None" ValidationGroup="btnbustoken"></asp:RequiredFieldValidator>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="fte" runat="server" TargetControlID="txtToken40" FilterType="Numbers"></ajaxToolKit:FilteredTextBoxExtender>

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Token 40 Amount</label>
                                            </div>
                                            <asp:TextBox ID="txtAmount40" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Token 30</label>
                                            </div>
                                            <asp:TextBox ID="txtToken30" runat="server" CssClass="form-control" MaxLength="4" onkeyup="Count(2)" TabIndex="3" autocomplete="off"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtToken30" ErrorMessage="Please Enter Token-30" Display="None" ValidationGroup="btnbustoken"></asp:RequiredFieldValidator>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtToken30" FilterType="Numbers"></ajaxToolKit:FilteredTextBoxExtender>

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Token 30 Amount</label>
                                            </div>
                                            <asp:TextBox ID="txtAmount30" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Grand Total</label>
                                            </div>
                                            <asp:TextBox ID="txtGrandTotal" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                            <asp:HiddenField ID="hdnViewSate" runat="server" />

                                        </div>
                                    </div>
                                </div>
                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary" Text="Submit" OnClick="btnSubmit_Click" ValidationGroup="btnbustoken" TabIndex="4" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true" ShowSummary="false" DisplayMode="List" ValidationGroup="btnbustoken" />
                                    <asp:Button ID="btnReport" runat="server" CssClass="btn btn-info" Text="Report" OnClick="btnReport_Click" TabIndex="6" />
                                    <asp:Button ID="btnClear" runat="server" CssClass="btn btn-warning" Text="Cancel" TabIndex="5" OnClick="btnClear_Click" />

                                </div>
                                <div class="col-12 mt-3">
                                    <asp:ListView ID="lvBustokenissue" runat="server">
                                        <LayoutTemplate>
                                            <div>
                                                <div class="sub-heading">
                                                    <h5>Bus Token Issue List.</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>Action</th>
                                                            <th>Date </th>
                                                            <th>Token-40 </th>
                                                            <th>Token-40 Amount</th>
                                                            <th>Token-30</th>
                                                            <th>Token-30 Amount</th>
                                                            <th>Grand Total</th>
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
                                                <td>

                                                    <asp:ImageButton ID="btnEditBtn" runat="server" CausesValidation="false" ImageUrl="~/Images/edit.png"
                                                        CommandArgument='<%#Eval("BTNO")%>' AlternateText="Edit Record"
                                                        OnClick="btnEditBtn_Click" />
                                                <td>

                                                    <%#Eval("BUS_TOKEN_ISSUE_DATE","{0:dd/MM/yyyy}")%>                                                              
                                                </td>

                                                <td>
                                                    <%#Eval("TOKEN_40")%>                                                        
                                                </td>
                                                <td>
                                                    <%#Eval("TOKEN_40_AMOUNT")%>                                                        
                                                </td>
                                                <td>
                                                    <asp:Label ID="lbl" runat="server" Text='<%#Eval("TOKEN_30")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <%#Eval("TOKEN_30_AMOUNT")%>                                                        
                                                </td>
                                                <td>
                                                    <%#Eval("GRAND_TOTAL") %> 
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </div>
                            </div>

                            <div id="DivReport" runat="server" visible="false">
                                <div class="col-12">
                                    <div class=" sub-heading">
                                        <h5>Bus Token Issue Report</h5>
                                    </div>
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>From Date</label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon" id="txtFromDate2">
                                                    <i class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtFromDate" CssClass="form-control" ToolTip="Enter Complaint Date" TabIndex="7"
                                                    runat="server" Style="z-index: 0;" autocomplete="off" onchange="check_date(1);"></asp:TextBox>
                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="true" EnableViewState="true"
                                                    Format="dd/MM/yyyy" PopupButtonID="txtFromDate2" TargetControlID="txtFromDate">
                                                </ajaxToolKit:CalendarExtender>
                                                <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999"
                                                    AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="true" MaskType="Date"
                                                    MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtFromDate"
                                                    ClearMaskOnLostFocus="true">
                                                </ajaxToolKit:MaskedEditExtender>
                                                <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender3"
                                                    ControlToValidate="txtFromDate" EmptyValueMessage="Please Enter From Date" IsValidEmpty="false"
                                                    ErrorMessage="Please Enter Valid From Date In [dd/MM/yyyy] format" Display="None" Text="*"
                                                    InvalidValueMessage="Please Enter Valid From Date In [dd/MM/yyyy] format" ValidationGroup="btnreport">                                                            
                                                </ajaxToolKit:MaskedEditValidator>
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>To Date</label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon" id="txtToDate1">
                                                    <i class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtToDate" CssClass="form-control" ToolTip="Enter Complaint Date" TabIndex="8"
                                                    runat="server" Style="z-index: 0;" autocomplete="off" onchange="check_date(0);"></asp:TextBox>
                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="true" EnableViewState="true"
                                                    Format="dd/MM/yyyy" PopupButtonID="txtToDate1" TargetControlID="txtToDate">
                                                </ajaxToolKit:CalendarExtender>
                                                <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="99/99/9999"
                                                    AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="true" MaskType="Date"
                                                    MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtToDate"
                                                    ClearMaskOnLostFocus="true">
                                                </ajaxToolKit:MaskedEditExtender>
                                                <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="MaskedEditExtender3"
                                                    ControlToValidate="txtToDate" EmptyValueMessage="Please Enter To Date" IsValidEmpty="false"
                                                    ErrorMessage="Please Enter Valid To Date In [dd/MM/yyyy] format" Display="None" Text="*"
                                                    InvalidValueMessage="Please Enter Valid To Date In [dd/MM/yyyy] format" ValidationGroup="btnreport">                                                            
                                                </ajaxToolKit:MaskedEditValidator>

                                            </div>
                                        </div>

                                    </div>
                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="btnShowRpt" runat="server" Text="ShowReport" OnClick="btnShowRpt_Click" CssClass="btn btn-info" ValidationGroup="btnreport" TabIndex="9" />

                                        <asp:Button ID="btnBack" runat="server" CssClass="btn btn-primary" Text="Back" OnClick="btnBack_Click" TabIndex="11" />
                                        <asp:ValidationSummary ID="vsReport" runat="server" ShowMessageBox="true" ShowSummary="false" DisplayMode="List" ValidationGroup="btnreport" />
                                        <asp:Button ID="btnRptClear" runat="server" CssClass="btn btn-warning" Text="Cancel" TabIndex="10" OnClientClick="ClearRptCon();" />

                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>



        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

