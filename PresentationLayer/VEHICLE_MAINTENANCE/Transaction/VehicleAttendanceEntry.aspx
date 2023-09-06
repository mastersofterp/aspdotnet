<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="VehicleAttendanceEntry.aspx.cs" Inherits="VEHICLE_MAINTENANCE_Transaction_VehicleAttendanceEntry"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--  <script src="../../JAVASCRIPTS/JScriptAdmin_Module.js" type="text/javascript"></script>--%>

    <script type="text/javascript">
        function CalculateTotal(vall) {

            var st = vall.id.split("lvStudent_ctrl");
            var i = st[1].split("_txtBeta");
            var index = i[0];

            var BetaValue = document.getElementById("ctl00_ContentPlaceHolder1_lvStudent_ctrl" + index + "_txtBeta").value;
            var OverTimeValue = document.getElementById("ctl00_ContentPlaceHolder1_lvStudent_ctrl" + index + "_txtamount").value;

            var total = Number(BetaValue) + Number(OverTimeValue);

            document.getElementById("ctl00_ContentPlaceHolder1_lvStudent_ctrl" + index + "_txtTotalAmt").value = total.toFixed(2);
        }


        function CalculateAmount(vall) {

            var st = vall.id.split("lvStudent_ctrl");
            var i = st[1].split("_txtamount");
            var index = i[0];

            var BetaValue = document.getElementById("ctl00_ContentPlaceHolder1_lvStudent_ctrl" + index + "_txtBeta").value;
            var OverTimeValue = document.getElementById("ctl00_ContentPlaceHolder1_lvStudent_ctrl" + index + "_txtamount").value;

            var total = Number(BetaValue) + Number(OverTimeValue);

            document.getElementById("ctl00_ContentPlaceHolder1_lvStudent_ctrl" + index + "_txtTotalAmt").value = total.toFixed(2);
            document.getElementById("ctl00_ContentPlaceHolder1_lvStudent_ctrl" + index + "_hdTotalAmt").value = total.toFixed(2);
        }
    </script>
    <asp:UpdatePanel ID="updActivity" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">VEHICLE DAILY ATTENDANCE ENTRY</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class=" sub-heading">
                                    <h5>Select Date and Supplier</h5>
                                </div>
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Vehicle Travelling Date</label>
                                        </div>
                                        <div class="input-group date">
                                            <div class="input-group-addon" id="imgtravellingDate">
                                                <i class="fa fa-calendar text-blue"></i>
                                            </div>
                                            <asp:TextBox ID="txttravellingDate" runat="server" TabIndex="1" Style="z-index: 0;"
                                                ToolTip="Enter Event Start Date" CssClass="form-control" AutoPostBack="false"></asp:TextBox>
                                            <ajaxToolKit:CalendarExtender ID="cetravellingDate" runat="server" Enabled="true" EnableViewState="true"
                                                Format="dd/MM/yyyy" PopupButtonID="imgtravellingDate" TargetControlID="txttravellingDate" />
                                            <ajaxToolKit:MaskedEditExtender ID="meetravellingDate" runat="server" Mask="99/99/9999" MaskType="Date"
                                                OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate" TargetControlID="txttravellingDate" />
                                            <ajaxToolKit:MaskedEditValidator ID="mevtravellingDate" runat="server"
                                                ControlExtender="meetravellingDate" ControlToValidate="txttravellingDate" IsValidEmpty="true"
                                                InvalidValueMessage="From Date is invalid" Display="None" TooltipMessage="Input a date"
                                                ErrorMessage="Please Select Date" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                                ValidationGroup="submit" SetFocusOnError="true" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                ErrorMessage="Please Enter From Date" ControlToValidate="txttravellingDate"
                                                Display="None" SetFocusOnError="True" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                            <asp:HiddenField ID="hdnDate" runat="server" />
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Supplier</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSuppiler" CssClass="form-control" data-select2-enable="true" ToolTip="Select Supplier" runat="server"
                                            AppendDataBoundItems="true" OnSelectedIndexChanged="ddlSuppiler_SelectedIndexChanged"
                                            AutoPostBack="true" TabIndex="2">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddl" runat="server"
                                            ErrorMessage="Please Select Suppiler" ControlToValidate="ddlSuppiler" InitialValue="0"
                                            Display="None" ValidationGroup="Submit">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>


                            <div class="col-12" id="trvehicledetails" runat="server" visible="false">
                                <div class="form-group col-md-12">
                                    <label><span style="color: #FF0000; font-weight: bold">*</span>Vehicle Details </label>
                                </div>

                                <asp:Panel ID="pnlList" runat="server" ScrollBars="Auto" TabIndex="3">
                                    <%--<asp:Panel ID="pnlList" runat="server" ScrollBars="Vertical" Height="170px" BorderColor="#CDCDCD"--%>
                                    <%-- BorderStyle="Solid" BorderWidth="1px" Width="700px" TabIndex="1">--%>
                                    <asp:ListView ID="lvStudent" runat="server">
                                        <LayoutTemplate>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Vehicle</th>
                                                        <th>From</th>
                                                        <th>To</th>
                                                        <th>Driver/ Conductor Name</th>
                                                        <th>Attendance Mark</th>
                                                         <th style="width: 10%">Beta</th>
                                                        <th>Over Time</th>
                                                        <th>Total Amount</th>
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
                                                    <%#Eval("VEHICLE_NAME")%>                                                        
                                                </td>
                                                <td>
                                                    <%#Eval("FROM_LOCATION")%>                                                        
                                                </td>
                                                <td>
                                                    <%#Eval("TO_LOCATION")%>                                                        
                                                </td>
                                                <td id="Td3" runat="server">
                                                    <asp:DropDownList ID="ddlDriver" CssClass="form-control" ToolTip="Select Name" TabIndex="4"
                                                        AppendDataBoundItems="true" runat="server">
                                                    </asp:DropDownList>
                                                </td>

                                                <td id="Td1" runat="server">
                                                    <asp:CheckBox ID="chkattendance" runat="server" ToolTip='<%# Eval("VEHICLE_ID") %>'
                                                        OnCheckedChanged="chkattendance_CheckedChanged" AutoPostBack="true" TabIndex="5" />
                                                    <%-- ToolTip='<%# Eval("VEHICLE_ID") %>'--%>
                                                </td>
                                                <td id="Td2" runat="server" >
                                                    <asp:CheckBox ID="chkDriver" runat="server" Visible="false" ToolTip="Driver" TabIndex="6" OnCheckedChanged="chkDriver_CheckedChanged" AutoPostBack="true" />
                                                    <asp:TextBox ID="txtBeta" CssClass="form-control" runat="server" MaxLength="10" TabIndex="7"
                                                        onkeypress="return CheckNumeric(event,this); " onblur="CalculateTotal(this);"></asp:TextBox>

                                                    <ajaxToolKit:FilteredTextBoxExtender ID="ftbtxtContNo" runat="server" ValidChars="0123456789."
                                                                            FilterType="Custom" FilterMode="ValidChars" TargetControlID="txtBeta">
                                                                        </ajaxToolKit:FilteredTextBoxExtender>
                                                </td>

                                                <td id="tddate" runat="server">
                                                    <asp:TextBox ID="txtamount" CssClass="form-control" runat="server" MaxLength="10" TabIndex="7"
                                                        onkeypress="return CheckNumeric(event,this); " onblur="CalculateAmount(this);"></asp:TextBox>
                                                       <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" ValidChars="0123456789."
                                                                            FilterType="Custom" FilterMode="ValidChars" TargetControlID="txtamount">
                                                                        </ajaxToolKit:FilteredTextBoxExtender>
                                                </td>
                                                <td id="tdTotal" runat="server">
                                                    <asp:TextBox ID="txtTotalAmt" CssClass="form-control" runat="server" ReadOnly="true" MaxLength="10" TabIndex="7"
                                                        onkeypress="return CheckNumeric(event,this);"></asp:TextBox>
                                                    <asp:HiddenField ID="hdTotalAmt" Value="" runat="server"  />
                                                </td>
                                                <%--     <td id="td2" runat="server" style="text-align: left"  width="20%" >
                                                                                <asp:TextBox ID="txthikeprice" Width="70%" runat="server" onkeypress="return CheckNumeric(event,this);" MaxLength="10"></asp:TextBox>
                                                                            </td>
                                                --%>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>

                            </div>
                            <div class="col-12 mt-4">
                                <asp:Panel ID="pnlAttendance" runat="server">
                                    <asp:ListView ID="lvBillEntryList" runat="server">
                                        <LayoutTemplate>
                                            <div id="lgv1">
                                                <div>
                                                    <div class="sub-heading">
                                                        <h5>Vehicle Daily Attendance Entry List</h5>
                                                    </div>
                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th>EDIT
                                                                </th>
                                                                <th>SUPPLIER NAME
                                                                </th>
                                                                <th>TRAVELLING DATE  
                                                                </th>
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
                                                    <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" ImageUrl="~/Images/edit.png"
                                                        CommandArgument='<%# Eval("TRAVELLING_DATE") %>' ToolTip='<%# Eval("SUPPILER_ID") %>'
                                                        OnClick="btnEdit_Click" TabIndex="8" />
                                                </td>
                                                <td>
                                                    <%# Eval("SUPPILER_NAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("TRAVELLING_DATE","{0:dd-MMM-yyyy}")%>
                            
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="Submit" TabIndex="9"
                                    OnClick="btnSubmit_Click" CssClass="btn btn-primary" ToolTip="Click here to Submit" CausesValidation="true" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" TabIndex="10"
                                    CssClass="btn btn-warning" ToolTip="Click here to Reset" CausesValidation="false" />
                                <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="Submit" />
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>


