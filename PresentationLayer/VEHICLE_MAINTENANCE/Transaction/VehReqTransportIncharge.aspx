<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="VehReqTransportIncharge.aspx.cs" Inherits="VEHICLE_MAINTENANCE_Transaction_VehReqTransportIncharge" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--  <script src="../../jquery/jquery-3.2.1.min.js"></script>
    <link href="../../jquery/jquery.multiselect.css" rel="stylesheet" />
    <script src="../../jquery/jquery.multiselect.js"></script>--%>
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updPanel"
            DynamicLayout="true" DisplayAfter="0">
            <%--<ProgressTemplate>
                <div id="preloader">
                    <div id="loader-img">
                        <div id="loader">
                        </div>
                        <p class="saving">Loading<span>.</span><span>.</span><span>.</span></p>
                    </div>
                </div>
            </ProgressTemplate>--%>
        </asp:UpdateProgress>
    </div>
    <asp:UpdatePanel ID="updPanel" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">Vehicle Arrangement By Transport Incharge</h3>
                        </div>
                        <div class="box-body">
                            <div id="divApprReqList" runat="server">
                                <div class="col-12">
                                    <asp:ListView ID="lvVehicleReq" runat="server">
                                        <LayoutTemplate>
                                            <div>
                                                <div class="sub-heading">
                                                    <h5>Approved Vehicle Requisition List</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>Name Of The Institution</th>
                                                            <th>Date Of Journey</th>
                                                            <th>One-Way/Two-Way</th>
                                                            <th>Action</th>
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
                                                    <%#Eval("COLLEGE_NAME")%>                                                              
                                                </td>
                                                <td>
                                                    <%#Eval("DATE_OF_JOURNEY","{0:dd/MM/yyyy}")%>                                                        
                                                </td>
                                                <td>
                                                    <%#Eval("ONE_WAY")%>                                                        
                                                </td>
                                                <td>

                                                    <asp:Button ID="btnSelect" runat="server" CommandArgument='<%#Eval("VEH_REQ_ID")%>' Text="Select" CssClass="btn btn-primary" OnClick="btnSelect_Click" />

                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </div>
                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnReport" runat="server" CssClass="btn btn-info" Text="Report" OnClick="btnReport_Click" />
                                </div>
                            </div>

                            <div class="col-12 mt-3" id="divReport" runat="server" visible="false">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Name Of The Institution</label>
                                        </div>
                                        <asp:DropDownList ID="ddlRInstitute" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator7" ControlToValidate="ddlRInstitute" Display="None"
                                            ErrorMessage="Please Select Name Of The Institution" ValidationGroup="Report" InitialValue="0" />

                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Date Of Journey</label>
                                        </div>
                                        <div class="input-group date">
                                            <div class="input-group-addon" id="imgFromDate">
                                                <i class="fa fa-calendar text-blue"></i>
                                            </div>
                                            <asp:TextBox ID="txtDOJ" runat="server" CssClass="form-control" TabIndex="2"></asp:TextBox>
                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="true"
                                                EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="imgFromDate" TargetControlID="txtDOJ">
                                            </ajaxToolKit:CalendarExtender>
                                            <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender3"
                                                runat="server" AcceptNegative="Left"
                                                DisplayMoney="Left"
                                                ErrorTooltipEnabled="true"
                                                Mask="99/99/9999"
                                                MaskType="Date"
                                                MessageValidatorTip="true"
                                                OnInvalidCssClass="errordate"
                                                TargetControlID="txtDOJ"
                                                ClearMaskOnLostFocus="true">
                                            </ajaxToolKit:MaskedEditExtender>
                                            <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator2" runat="server"
                                                ControlExtender="MaskedEditExtender3"
                                                ControlToValidate="txtDOJ"
                                                IsValidEmpty="False"
                                                EmptyValueMessage="Please Select Date Of Journey"
                                                InvalidValueMessage="Date Of Journey is Invalid (Enter In dd/MM/yyyy Format)"
                                                Display="None"
                                                TooltipMessage="Input a date"
                                                EmptyValueBlurredText="*"
                                                InvalidValueBlurredMessage="*"
                                                ValidationGroup="Report" />

                                        </div>
                                    </div>

                                </div>
                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnRpt" runat="server" Text="Show Report" CssClass="btn btn-info" TabIndex="11" ValidationGroup="Report" OnClick="btnRpt_Click" />
                                    <asp:Button ID="btnRBack" runat="server" Text="Back" CssClass="btn btn-primary" TabIndex="11" OnClick="btnRBack_Click" />
                                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Report" />
                                </div>

                            </div>
                            <div class="col-12 mt-3" id="divVehArrange" runat="server" visible="false">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Contractor Name</label>
                                        </div>
                                        <asp:DropDownList ID="ddlContractor" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator8" ControlToValidate="ddlContractor" Display="None"
                                            ErrorMessage="Please Select Contractor Name" InitialValue="0" ValidationGroup="Submit" />

                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Vehicle No.</label>
                                        </div>
                                        <asp:TextBox ID="txtVehicleNo" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="5"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="txtVehicleNo" Display="None"
                                            ErrorMessage="Please Enter Vehicle No." ValidationGroup="Submit" />

                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Arrival Time </label>
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
                                            IsValidEmpty="false" ValidationGroup="Submit" />
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txtArrivalTime" Display="None"
                                            ErrorMessage="Please Enter Arrival Time" ValidationGroup="Submit" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Departure Time</label>
                                        </div>
                                        <asp:TextBox ID="txtDepartureTime" runat="server" CssClass="form-control" TabIndex="3"></asp:TextBox>
                                        <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txtDepartureTime"
                                            Mask="99:99" MaskType="Time"
                                            AcceptAMPM="true" ErrorTooltipEnabled="true" MessageValidatorTip="true" AcceptNegative="Left"
                                            DisplayMoney="Left" OnInvalidCssClass="errordate" />
                                        <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender2"
                                            ControlToValidate="txtDepartureTime" Display="None" EmptyValueBlurredText="Empty"
                                            InvalidValueMessage="Departure Time is Invalid (Enter 12 Hour Format)"
                                            SetFocusOnError="true" TooltipMessage="Please Enter Departure Time"
                                            IsValidEmpty="false" ValidationGroup="Submit" />
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="txtDepartureTime" Display="None"
                                            ErrorMessage="Please Enter Departure Time" ValidationGroup="Submit" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Total Kms Travelled</label>
                                        </div>
                                        <asp:TextBox ID="txtTotKm" runat="server" MaxLength="5" CssClass="form-control" TabIndex="5" onkeyup="validateNumeric(this);"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ControlToValidate="txtTotKm" Display="None"
                                            ErrorMessage="Please Enter Total Kms Travelled" ValidationGroup="Submit" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Total Hrs. Travelled</label>
                                        </div>
                                        <asp:TextBox ID="txtTotHrs" runat="server" MaxLength="5" CssClass="form-control" TabIndex="3" onkeyup="validateNumeric(this);"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtTotHrs" Display="None"
                                            ErrorMessage="Please Enter Total Hrs. Travelled" ValidationGroup="Submit" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Amount Payable </label>
                                        </div>
                                        <asp:TextBox ID="txtAmountPay" runat="server" CssClass="form-control" MaxLength="9" onkeyup="validateNumeric(this);" TabIndex="5"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator6" ControlToValidate="txtAmountPay" Display="None"
                                            ErrorMessage="Please Enter Amount Payable" ValidationGroup="Submit" />
                                    </div>


                                </div>


                                <div class="col-12 mt-3">
                                    <asp:ListView ID="lvVehicle" runat="server">
                                        <LayoutTemplate>
                                            <div>
                                                <div class="sub-heading">
                                                    <h5>Vehicle Arranged</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>Vehicle Name</th>
                                                            <th>Vehicle ype</th>
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
                                                    <%#Eval("VEHICLE_NAME")%>                                                              
                                                </td>
                                                <td>
                                                    <%#Eval("VEHICLE_AC_NONAC")%>                                                        
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </div>

                                <div class="col-12 btn-footer mt-3">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" TabIndex="11" ValidationGroup="Submit" OnClick="btnSubmit_Click" />
                                    <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btn btn-primary" TabIndex="11" OnClick="btnBack_Click" />
                                    <asp:ValidationSummary ID="vsbuss" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Submit" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>


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

