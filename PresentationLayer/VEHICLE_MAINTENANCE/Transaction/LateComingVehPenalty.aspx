<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="LateComingVehPenalty.aspx.cs" Inherits="VEHICLE_MAINTENANCE_Transaction_LateComingVehPenalty" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%-- <script src="../../JAVASCRIPTS/JScriptAdmin_Module.js" type="text/javascript"></script>--%>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div2" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">Penalty For Late Coming Hired Vehicle</h3>
                </div>
                <div class="box-body">
                    <asp:Panel ID="pnlInsurance" runat="server">
                        <div class="col-12">
                            <div class="row">
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>College</label>
                                    </div>
                                    <asp:DropDownList ID="ddlCollege" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" TabIndex="2"
                                        ToolTip="Select College">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" Display="None" runat="server"
                                        ControlToValidate="ddlCollege" ErrorMessage="Please Select College" SetFocusOnError="true"
                                        ValidationGroup="submit" InitialValue="0"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Vehicle</label>
                                    </div>
                                    <asp:DropDownList ID="ddlVehicle" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" TabIndex="2"
                                        ToolTip="Select Vehicle">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="None" runat="server"
                                        ControlToValidate="ddlVehicle" ErrorMessage="Please Select Vehicle" SetFocusOnError="true"
                                        ValidationGroup="submit" InitialValue="0"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Arrival Date </label>
                                    </div>
                                    <div class="input-group date">
                                        <div class="input-group-addon" id="imgtravellingDate">
                                            <i class="fa fa-calendar text-blue"></i>
                                        </div>
                                        <asp:TextBox ID="txtArrivalDate" runat="server" TabIndex="1" Style="z-index: 0;"
                                            ToolTip="Enter Arrival Date" CssClass="form-control"></asp:TextBox>
                                        <ajaxToolKit:CalendarExtender ID="cetravellingDate" runat="server" Enabled="true" EnableViewState="true"
                                            Format="dd/MM/yyyy" PopupButtonID="imgtravellingDate" TargetControlID="txtArrivalDate" />
                                        <ajaxToolKit:MaskedEditExtender ID="meetravellingDate" runat="server" Mask="99/99/9999" MaskType="Date"
                                            OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate" TargetControlID="txtArrivalDate" />
                                        <ajaxToolKit:MaskedEditValidator ID="mevtravellingDate" runat="server"
                                            ControlExtender="meetravellingDate" ControlToValidate="txtArrivalDate" IsValidEmpty="false"
                                            InvalidValueMessage="From Date is invalid" Display="None" TooltipMessage="Input a date"
                                            ErrorMessage="Please Select Date" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                            ValidationGroup="submit" SetFocusOnError="true" EmptyValueMessage="Please Select Arrival Date" />

                                    </div>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Arrival Time</label>
                                    </div>
                                    <asp:TextBox ID="txtArrivalTime" runat="server" CssClass="form-control" TabIndex="3"></asp:TextBox>
                                    <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtArrivalTime"
                                        Mask="99:99" MaskType="Time"
                                        AcceptAMPM="true" ErrorTooltipEnabled="true" MessageValidatorTip="true" AcceptNegative="Left"
                                        DisplayMoney="Left" OnInvalidCssClass="errordate" />
                                    <ajaxToolKit:MaskedEditValidator ID="mevEnterTimeTo" runat="server" ControlExtender="MaskedEditExtender1"
                                        ControlToValidate="txtArrivalTime" Display="None" EmptyValueBlurredText="Empty"
                                        InvalidValueMessage="Arrival Time is Invalid (Enter 12 Hour Format)"
                                        SetFocusOnError="true" TooltipMessage="Please Enter Arrival Time"
                                        IsValidEmpty="false" ValidationGroup="submit" />
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txtArrivalTime" Display="None"
                                        ErrorMessage="Please Enter Arrival Time" ValidationGroup="submit" />
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Actuall Arrival Time</label>
                                    </div>
                                    <asp:TextBox ID="txtAcualArrivalTime" runat="server" CssClass="form-control" TabIndex="3"></asp:TextBox>
                                    <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txtAcualArrivalTime"
                                        Mask="99:99" MaskType="Time"
                                        AcceptAMPM="true" ErrorTooltipEnabled="true" MessageValidatorTip="true" AcceptNegative="Left"
                                        DisplayMoney="Left" OnInvalidCssClass="errordate" />
                                    <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender2"
                                        ControlToValidate="txtAcualArrivalTime" Display="None" EmptyValueBlurredText="Empty"
                                        InvalidValueMessage="Actuall Arrival Time is Invalid (Enter 12 Hour Format)"
                                        SetFocusOnError="true" TooltipMessage="Please Enter Actuall Arrival Time"
                                        IsValidEmpty="false" ValidationGroup="submit" />
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="txtAcualArrivalTime" Display="None"
                                        ErrorMessage="Please Enter Actuall Arrival Time" ValidationGroup="submit" />
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Late By (Hrs)</label>
                                    </div>
                                    <asp:TextBox ID="txtLateHours" runat="server" CssClass="form-control" TabIndex="3"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ControlToValidate="txtLateHours" Display="None"
                                        ErrorMessage="Please Enter Late By (Hrs)" ValidationGroup="submit" />
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Fine Amount </label>
                                    </div>
                                    <asp:TextBox ID="txtFineAmount" runat="server" CssClass="form-control" MaxLength="8" TabIndex="3" onkeyup="validateNumeric(this);"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator6" ControlToValidate="txtFineAmount" Display="None"
                                        ErrorMessage="Please Enter Fine Amount" ValidationGroup="submit" />
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                    <div class="col-12 btn-footer">
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" ValidationGroup="submit"
                            ToolTip="Click here to Submit" TabIndex="15" OnClick="btnSubmit_Click" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" TabIndex="16"
                            ToolTip="Click here to reset" OnClick="btnCancel_Click" />
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                            ShowSummary="false" DisplayMode="List" ValidationGroup="submit" />
                    </div>
                    <div class="col-12 mt-3">
                        <asp:Panel ID="pnlList" runat="server">
                            <asp:ListView ID="lvPenaltyList" runat="server">
                                <LayoutTemplate>
                                    <div id="lgv1">
                                        <div class="sub-heading">
                                            <h5>Penalty For Late Coming Hired Vehicle List</h5>
                                        </div>
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>EDIT
                                                    </th>
                                                    <th>COLLEGE NAME
                                                    </th>
                                                    <th>VEHICLE NAME
                                                    </th>
                                                    <th>ARRIVAL DATE
                                                    </th>
                                                    <th>LATE HOURS
                                                    </th>
                                                    <th>FINE AMOUNT
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
                                            <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("PENALTY_ID") %>'
                                                ToolTip="Edit Record" AlternateText="Edit Record" OnClick="btnEdit_Click" />&nbsp;                                         
                                        </td>
                                        <td>
                                            <%# Eval("COLLEGE_NAME")%>
                                        </td>
                                        <td>
                                            <%# Eval("VEHICLE_NAME")%>
                                        </td>
                                        <td>
                                            <%# Eval("ARRIVAL_DATE","{0:dd/MM/yyyy}")%>
                                        </td>
                                        <td>
                                            <%# Eval("LATE_HOURS")%>
                                        </td>
                                        <td>
                                            <%# Eval("FINE_AMOUNT")%>
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


    <script type="text/javascript">

        function validateNumeric(txt) {
            if (isNaN(txt.value)) {
                txt.value = txt.value.substring(0, (txt.value.length) - 1);
                txt.value = '';
                txt.focus = true;
                alert("Only Numeric Characters allowed !");
                return false;
            }
            else
                return true;
        }
    </script>
</asp:Content>


