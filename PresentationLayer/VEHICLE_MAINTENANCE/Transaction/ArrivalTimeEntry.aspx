<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ArrivalTimeEntry.aspx.cs" Inherits="VEHICLE_MAINTENANCE_Transaction_ArrivalTimeEntry" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../../jquery/jquery-3.2.1.min.js"></script>
    <link href="../../jquery/jquery.multiselect.css" rel="stylesheet" />
    <script src="../../jquery/jquery.multiselect.js"></script>
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
                            <h3 class="box-title">Arrival Working Day Entry</h3>
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
                                            <%--    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtFrmDate" Display="None"
                                            ErrorMessage="Please Enter From Date " ValidationGroup="Chk_Date" />--%>
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Route No.</label>
                                        </div>
                                        <asp:DropDownList ID="ddlRouteNo" runat="server" CssClass="form-control" data-select2-enable="true" AutoPostBack="false" TabIndex="2" AppendDataBoundItems="true">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="ddlRouteNo" Display="None"
                                            ErrorMessage="Please Select Route " ValidationGroup="Chk_Date" InitialValue="0" />

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
                                            InvalidValueMessage="Departure Time is Invalid (Enter 12 Hour Format)"
                                            SetFocusOnError="true" TooltipMessage="Please Enter Arrival Time"
                                            IsValidEmpty="true" />
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtArrivalTime" Display="None"
                                            ErrorMessage="Please Enter Arrival Time " ValidationGroup="Chk_Date" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Reason</label>
                                        </div>
                                        <asp:DropDownList ID="ddlReason" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" TabIndex="4" AutoPostBack="false"></asp:DropDownList>

                                    </div>
                                </div>
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="btn btn-primary" OnClick="btnAdd_Click" TabIndex="5" ValidationGroup="Chk_Date" />
                                <asp:ValidationSummary ID="vsdate" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Chk_Date" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" TabIndex="6" OnClick="btnCancel_Click" OnClientClick="Clear();" />
                                <%--<asp:Button ID="btnReport" runat="server" Text="Report" CssClass="btn btn-info" TabIndex="7" OnClick="btnReport_Click" />--%>
                            </div>


                            <div class="col-12 mt-3">
                                <asp:ListView ID="lvArrival" runat="server" Visible="false">
                                    <LayoutTemplate>
                                        <div>
                                            <div class="sub-heading">
                                                <h5>Route List.</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Action</th>
                                                        <th>Route Name</th>
                                                        <th>Date </th>
                                                        <th>Arrival Time</th>
                                                        <th>Reason for Delay</th>
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

                                                <asp:ImageButton ID="btnImgDelete" runat="server" CausesValidation="false" ImageUrl="~/Images/delete.png"
                                                    CommandArgument='<%#Eval("ATID")%>' AlternateText="Edit Record"
                                                    OnClick="btnImgDelete_Click" />
                                            <td>

                                                <%#Eval("ROUTENAME")%>                                                              
                                            </td>

                                            <td>
                                                <%#Eval("ARRIVAL_DATE_TIME","{0:dd/MM/yyyy}")%>                                                        
                                            </td>

                                            <td>
                                                <%#Eval("ARRIVAL_DATE_TIME","{0:hh:mm tt}")%>          
                                            </td>

                                            <td>
                                                <%#Eval("REASON") %> 
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>


                            <div class="col-12">
                                <div class="sub-heading">
                                    <h5>Bus Count Entry</h5>
                                </div>
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>A/C Buses-38 Seat</label>
                                        </div>
                                        <asp:TextBox ID="txtAcbus38" runat="server" CssClass="form-control" TabIndex="7" MaxLength="3"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvAc38" runat="server" ControlToValidate="txtAcbus38" Display="None" ValidationGroup="Buses" ErrorMessage="Please Enter A/C Buses-38 Seat"></asp:RequiredFieldValidator>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtAcbus38" FilterType="Numbers" InvalidChars="0"></ajaxToolKit:FilteredTextBoxExtender>

                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>A/C Buses-55 Seat</label>
                                        </div>
                                        <asp:TextBox ID="txtAcbus55" runat="server" CssClass="form-control" TabIndex="8" MaxLength="3"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvac55" runat="server" ControlToValidate="txtAcbus55" Display="None" ValidationGroup="Buses" ErrorMessage="Please Enter A/C Buses-55 Seat"></asp:RequiredFieldValidator>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtAcbus55" FilterType="Numbers" InvalidChars="0"></ajaxToolKit:FilteredTextBoxExtender>

                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Dedicated Buses</label>
                                        </div>
                                        <asp:TextBox ID="txtDedicatedbus" runat="server" CssClass="form-control" TabIndex="9" MaxLength="3"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvDedicatedbus" runat="server" ControlToValidate="txtDedicatedbus" Display="None" ValidationGroup="Buses" ErrorMessage="Please Enter Dedicated Buses"></asp:RequiredFieldValidator>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtDedicatedbus" FilterType="Numbers" InvalidChars="0"></ajaxToolKit:FilteredTextBoxExtender>

                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>RCPIT Buses</label>
                                        </div>
                                        <asp:TextBox ID="txtSvcebus" runat="server" CssClass="form-control" TabIndex="10" MaxLength="3"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvsvcebus" runat="server" ControlToValidate="txtSvcebus" Display="None" ValidationGroup="Buses" ErrorMessage="Please Enter Svce Buses"></asp:RequiredFieldValidator>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="ftbe" runat="server" TargetControlID="txtSvcebus" FilterType="Numbers" InvalidChars="0"></ajaxToolKit:FilteredTextBoxExtender>

                                    </div>

                                </div>
                            </div>

                            <div class="col-12 btn-footer ">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" TabIndex="11" OnClick="btnSubmit_Click" ValidationGroup="Buses" />
                                <asp:ValidationSummary ID="vsbuss" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Buses" />
                                <asp:Button ID="btnReport" runat="server" CssClass="btn btn-info" Text="Report" OnClick="btnReport_Click1" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>



</asp:Content>

