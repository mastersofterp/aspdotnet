<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="HODCarsDetails.aspx.cs" Inherits="VEHICLE_MAINTENANCE_Transaction_HODCarsDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        function CheckDate() {
            var DateString = document.getElementById('<%=txtDate.ClientID%>').value;

            var date = DateString.split("/");
            var day = date[0];
            var Month = date[1] - 1;
            var Year = date[2];
            var Tdate = new Date(Year, Month, day);
            var PresentDate = new Date();
            if (Tdate > PresentDate) {
                alert('Future date is not allowed');
                //var today = new Date();
                document.getElementById('<%=txtDate.ClientID%>').value = '';
                document.getElementById('<%=txtDate.ClientID%>').focus();

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
                            <h3 class="box-title">HOD CAR's Arrival Entry</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Arrival Date</label>
                                        </div>
                                        <div class="input-group date">
                                            <div class="input-group-addon" id="imgFromDate">
                                                <i class="fa fa-calendar text-blue"></i>
                                            </div>
                                            <asp:TextBox ID="txtDate" runat="server" CssClass="form-control" TabIndex="1" AutoPostBack="true" OnTextChanged="txtDate_TextChanged" onchange="CheckDate();"></asp:TextBox>
                                            <ajaxToolKit:CalendarExtender ID="ceFromdate" runat="server" Enabled="true"
                                                EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="imgFromDate" TargetControlID="txtDate">
                                            </ajaxToolKit:CalendarExtender>
                                            <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender2"
                                                runat="server" AcceptNegative="Left"
                                                DisplayMoney="Left"
                                                ErrorTooltipEnabled="true"
                                                Mask="99/99/9999"
                                                MaskType="Date"
                                                MessageValidatorTip="true"
                                                OnInvalidCssClass="errordate"
                                                TargetControlID="txtDate"
                                                ClearMaskOnLostFocus="true">
                                            </ajaxToolKit:MaskedEditExtender>
                                            <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server"
                                                ControlExtender="MaskedEditExtender2"
                                                ControlToValidate="txtDate"
                                                IsValidEmpty="False"
                                                EmptyValueMessage="From Date is required"
                                                InvalidValueMessage="From Date is invalid"
                                                Display="None"
                                                TooltipMessage="Input a date"
                                                EmptyValueBlurredText="*"
                                                InvalidValueBlurredMessage="*"
                                                ValidationGroup="Chk_Date" />
                                            <asp:RequiredFieldValidator runat="server" ID="rfvArrivalDate" ControlToValidate="txtDate" Display="None"
                                                ErrorMessage="Please Enter Arrival Date " ValidationGroup="add" />
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Travels</label>
                                        </div>
                                        <asp:DropDownList ID="ddlTravels" runat="server" AppendDataBoundItems="true" TabIndex="2" CssClass="form-control" data-select2-enable="true"></asp:DropDownList>
                                        <asp:RequiredFieldValidator runat="server" ID="rfvTravels" ControlToValidate="ddlTravels" Display="None" InitialValue="0"
                                            ErrorMessage="Please Select Travels" ValidationGroup="add" />

                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Registration No.</label>
                                        </div>
                                        <asp:TextBox ID="txtRegNo" runat="server" CssClass="form-control" ToolTip="Please Enter Registration No." MaxLength="30"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ID="rfvRegNo" ControlToValidate="txtRegNo" Display="None"
                                            ErrorMessage="Please Enter Registration No." ValidationGroup="add" />
                                        <ajaxToolKit:FilteredTextBoxExtender ID="ftbe" runat="server"
                                            FilterType="Custom, UppercaseLetters, LowercaseLetters, Numbers"
                                            ValidChars="-/" TargetControlID="txtRegNo">
                                        </ajaxToolKit:FilteredTextBoxExtender>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>IN Time</label>
                                        </div>
                                        <asp:TextBox ID="txtINTime" runat="server" CssClass="form-control" ToolTip="Please Enter IN Time"></asp:TextBox>
                                        <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtINTime" Mask="99:99" MaskType="Time"
                                            AcceptAMPM="true" ErrorTooltipEnabled="true" MessageValidatorTip="true" AcceptNegative="Left"
                                            DisplayMoney="Left" OnInvalidCssClass="errordate" />
                                        <%-- <ajaxToolKit:MaskedEditValidator ID="mevEnterTimeTo" runat="server" ControlExtender="MaskedEditExtender1"
                                                            ControlToValidate="txtINTime" Display="None" EmptyValueBlurredText="Empty"
                                                            InvalidValueMessage="IN Time is Invalid (Enter 12 Hour Format)"
                                                            SetFocusOnError="true" TooltipMessage="Please Enter IN Time" IsValidEmpty="false" />--%>
                                        <asp:RequiredFieldValidator runat="server" ID="rfvINTime" ControlToValidate="txtINTime" Display="None"
                                            ErrorMessage="Please Enter IN Time" ValidationGroup="add" />

                                    </div>
                                </div>
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnAdd" runat="server" Text="Add" ValidationGroup="add" OnClick="btnAdd_Click" CssClass="btn btn-primary" />
                                <asp:ValidationSummary ID="VS1" runat="server" ShowMessageBox="true" ShowSummary="false" DisplayMode="List" ValidationGroup="add" />
                                <asp:HiddenField ID="hiddenSrNo" runat="server" />
                            </div>

                            <div class="col-12" id="divAddDetails" runat="server" visible="false">
                                <asp:Panel ID="lvPanel" runat="server">
                                    <asp:ListView ID="lvInTime" runat="server">
                                        <LayoutTemplate>
                                            <div id="lgv1">
                                                <div class="sub-heading">
                                                    <h5>Add Details</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>Delete</th>
                                                            <th>Arrival Date</th>
                                                            <th>Travels</th>
                                                            <th>Registration No.</th>
                                                            <th>IN Time</th>
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
                                                    <asp:ImageButton ID="btnDelete" runat="server" CommandArgument='<%# Eval("SRNO") %>' ImageUrl="~/images/delete.gif" OnClick="btnDelete_Click" ToolTip="Delete Record" />
                                                    <%-- <asp:ImageButton ID="btnEdit" runat="server" CommandArgument='<%# Eval("SRNO") %>' ImageUrl="~/images/edit.gif" OnClick="btnEdit_Click" ToolTip="Edit Record" />  --%>                                                                           
                                                </td>
                                                <td><%# Eval("ARRIVAL_DATE", "{0:dd-MMM-yyyy}") %></td>
                                                <td><%# Eval("TRAVELS_NAME") %>
                                                    <asp:HiddenField ID="hdnTravelId" runat="server" Value='<%# Eval("TRAVELS_ID") %>' />
                                                </td>
                                                <td><%# Eval("REGNO") %></td>
                                                <td><%# Eval("IN_TIME","{0:hh:mm tt}") %></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="btnSubmit_Click" ValidationGroup="submit" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" OnClick="btnCancel_Click" />
                                <asp:Button ID="btnReport" runat="server" Text="Report" CssClass="btn btn-info" OnClick="btnReport_Click" Visible="false" />

                            </div>

                            <div class="col-12" id="divMainList" runat="server" visible="false">
                                <asp:ListView ID="lvMainList" runat="server">
                                    <LayoutTemplate>
                                        <div id="lgv1">
                                            <div class="sub-heading">
                                                <h5>HOD Cars Arrival Entry</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Edit</th>
                                                        <th>Arrival Date</th>
                                                        <th>Travels</th>
                                                        <th>Registration No.</th>
                                                        <th>IN Time</th>
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
                                                <%-- <asp:ImageButton ID="btnDelete" runat="server" CommandArgument='<%# Eval("SRNO") %>' ImageUrl="~/images/delete.gif"  ToolTip="Delete Record" />--%>
                                                <asp:ImageButton ID="btnEdit" runat="server" CommandArgument='<%# Eval("HODCARS_ID") %>' ImageUrl="~/images/edit.png" OnClick="btnEdit_Click" ToolTip="Edit Record" />
                                                <asp:HiddenField ID="hdnSrNo" runat="server" Value='<%# Eval("SRNO") %>' />
                                            </td>
                                            <td><%# Eval("ARRIVAL_DATE", "{0:dd-MMM-yyyy}") %></td>
                                            <td><%# Eval("TRAVELS_NAME") %>
                                                <asp:HiddenField ID="hdnTravelId" runat="server" Value='<%# Eval("TRAVELS_ID") %>' />
                                            </td>
                                            <td><%# Eval("REGNO") %></td>
                                            <td><%# Eval("IN_TIME","{0:hh:mm tt}") %></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>

                        </div>

                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

