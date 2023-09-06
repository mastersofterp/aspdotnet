<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="CollegeVehicleAttendanceEntry.aspx.cs" Inherits="VEHICLE_MAINTENANCE_Transaction_CollegeVehicleAttendanceEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--  <script src="../../JAVASCRIPTS/JScriptAdmin_Module.js" type="text/javascript"></script>--%>

    <script type="text/javascript">
        function CalculateTotal(vall) {

            var BetaValue = document.getElementById('<%=txtBeta.ClientID%>').value;
            var OverTimeValue = document.getElementById('<%=txtamount.ClientID%>').value;
            var total = Number(BetaValue) + Number(OverTimeValue);
            document.getElementById('<%=txtTotalAmt.ClientID%>').value = total.toFixed(2);
        }


        function CalculateAmount(vall) {


            var BetaValue = document.getElementById('<%=txtBeta.ClientID%>').value;
            var OverTimeValue = document.getElementById('<%=txtamount.ClientID%>').value;
            var total = Number(BetaValue) + Number(OverTimeValue);
            document.getElementById('<%=txtTotalAmt.ClientID%>').value = total.toFixed(2);
        }





        function isFutureDate(idate) {
            var today = new Date().getTime(),
                idate = idate.split("/");

            idate = new Date(idate[2], idate[1] - 1, idate[0]).getTime();
            return (today - idate) < 0 ? true : false;
        }


        // your function
        function checkDate() {
            var idate = document.getElementById('<%=txttravellingDate.ClientID%>').value.split("/");
            var GDate = new Date(idate[2], idate[1] - 1, idate[0]);
            var prasentdate = new Date()
            if (prasentdate < GDate) {
                alert("Future Date Is Not Allowed");
                document.getElementById('<%=txttravellingDate.ClientID%>').value = '';

            }
        }
    </script>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div2" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">COLLEGE VEHICLE DAILY ATTENDANCE</h3>
                </div>
                <div class="box-body">

                    <div>
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
                    </div>
                    <asp:UpdatePanel ID="updActivity" runat="server">
                        <ContentTemplate>
                            <%--<div class="col-12">
                                    <div class="panel-heading">College Vehicle Daily Attendance Details</div>
                                    <div class="panel panel-body">--%>

                            <div class="col-12">
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
                                                ToolTip="Enter Event Start Date" CssClass="form-control" onchange="checkDate()"></asp:TextBox>
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
                                            <label>Vehicle</label>
                                        </div>
                                        <asp:DropDownList ID="ddlVehicle" CssClass="form-control" data-select2-enable="true" ToolTip="Select Vehicle" runat="server"
                                            AppendDataBoundItems="true" TabIndex="2">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddl" runat="server"
                                            ErrorMessage="Please Select Vehicle" ControlToValidate="ddlVehicle" InitialValue="0"
                                            Display="None" ValidationGroup="Add">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Driver/ Conductor Name</label>
                                        </div>
                                        <asp:DropDownList ID="ddlDriverCond" CssClass="form-control" data-select2-enable="true" ToolTip="Select Driver/ Conductor Name" runat="server"
                                            AppendDataBoundItems="true" TabIndex="2">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                            ErrorMessage="Please Select Driver/ Conductor Name" ControlToValidate="ddlDriverCond" InitialValue="0"
                                            Display="None" ValidationGroup="Add">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Attendance Mark</label>
                                        </div>
                                        <asp:CheckBox ID="chkAttendance" runat="server" ToolTip="Driver" AutoPostBack="true" OnCheckedChanged="chkAttendance_CheckedChanged" TabIndex="6" />

                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Beta</label>
                                        </div>
                                        <asp:TextBox ID="txtBeta" CssClass="form-control" Enabled="false" runat="server" MaxLength="7" TabIndex="7"
                                            onkeypress="return CheckNumeric(event,this); " onblur="CalculateTotal(this);"></asp:TextBox>
                                        
<ajaxToolKit:FilteredTextBoxExtender ID="ftbtxtContNo" runat="server" ValidChars="0123456789."
                                                                            FilterType="Custom" FilterMode="ValidChars" TargetControlID="txtBeta">
                                                                        </ajaxToolKit:FilteredTextBoxExtender>
                                        <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                                                    ErrorMessage="Please Enter Beta" ControlToValidate="txtBeta"
                                                                    Display="None" ValidationGroup="Add">
                                                                </asp:RequiredFieldValidator>--%>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Over Time</label>
                                        </div>
                                        <asp:TextBox ID="txtamount" CssClass="form-control" Enabled="false" runat="server" MaxLength="7" TabIndex="7"
                                            onkeypress="return CheckNumeric(event,this); " onblur="CalculateAmount(this);"></asp:TextBox>
                                      
                                          <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" ValidChars="0123456789."
                                                                            FilterType="Custom" FilterMode="ValidChars" TargetControlID="txtamount">
                                                                        </ajaxToolKit:FilteredTextBoxExtender>

                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Total Amount </label>
                                        </div>
                                        <asp:TextBox ID="txtTotalAmt" CssClass="form-control" runat="server" Enabled="false" MaxLength="10" TabIndex="7"
                                            ReadOnly="true"></asp:TextBox>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                                                    ErrorMessage="Please Enter Total Amount" ControlToValidate="txtTotalAmt"
                                                                    Display="None" ValidationGroup="Add">
                                                                </asp:RequiredFieldValidator>--%>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label></label>
                                        </div>
                                        <asp:Button ID="btnAdd" runat="server" Text="Add" ValidationGroup="Add" TabIndex="9"
                                            OnClick="btnAdd_Click" CssClass="btn btn-primary" ToolTip="Click here to Add" CausesValidation="true" />
                                        <%--&nbsp;<asp:Button ID="Button2" runat="server" Text="Cancel" OnClick="btnCancel_Click" TabIndex="10"
                                                            CssClass="btn btn-warning" ToolTip="Click here to Reset" CausesValidation="false" />--%>
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="true"
                                            ShowSummary="false" ValidationGroup="Add" />
                                    </div>

                                </div>
                            </div>
                            <div class="col-12 mt-3">
                                <asp:Panel ID="PnlAdd" runat="server">
                                    <asp:ListView ID="lvVehDetails" runat="server">
                                        <LayoutTemplate>
                                            <div id="lgv1">
                                                <%--<h4 class="box-title">College Vehicle Daily Attendance List
                                                                    </h4>--%>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>EDIT
                                                            </th>
                                                            <th>VEHICLE NAME 
                                                            </th>
                                                            <th>DRIVER NAME
                                                            </th>
                                                            <th>ATTENDANCE MARK 
                                                            </th>
                                                            <th>BETA 
                                                            </th>
                                                            <th>OVER TIME 
                                                            </th>
                                                            <th>TOTAL AMOUNT 
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
                                                    <asp:ImageButton ID="btnEditVeh" runat="server" CausesValidation="false" ImageUrl="~/Images/edit.png"
                                                        CommandArgument='<%# Eval("SRNO") %>'
                                                        OnClick="btnEditVeh_Click" TabIndex="8" />
                                                </td>

                                                <td>
                                                    <%# Eval("VEHICLE_NAME")%>
                            
                                                </td>
                                                <td>
                                                    <%# Eval("DRIVER_NAME")%>                            
                                                </td>
                                                <td>
                                                    <%#Eval("ATTENDANCE_STATUS")%>
                                                </td>
                                                <td>
                                                    <%# Eval("BETA")%>
                            
                                                </td>
                                                <td>
                                                    <%# Eval("DRIVER_TA_AMOUNT")%>                            
                                                </td>
                                                <td>
                                                    <%#Eval("TOTAL_AMOUNT")%>
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

                            <div class="col-12">
                                <asp:Panel ID="pnlAttendance" runat="server">
                                    <asp:ListView ID="lvBillEntryList" runat="server">
                                        <LayoutTemplate>
                                            <div id="lgv1">
                                                <div class="sub-heading">
                                                    <h5>College Vehicle Daily Attendance List</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>EDIT
                                                            </th>

                                                            <th>TRAVELLING DATE  
                                                            </th>
                                                            <%-- <th>VEHICLE NAME 
                                                                                </th>--%>
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
                                                        CommandArgument='<%# Eval("TRAVELLING_DATE") %>'
                                                        OnClick="btnEdit_Click" TabIndex="8" />
                                                </td>

                                                <td>
                                                    <%# Eval("TRAVELLING_DATE","{0:dd-MMM-yyyy}")%>
                            
                                                </td>
                                                <%--<td>
                                                                        <%# Eval("NAME")%>                            
                                                                    </td>--%>
                                            </tr>
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


</asp:Content>


