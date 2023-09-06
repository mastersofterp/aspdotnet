<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="VehicleSchedule.aspx.cs" Inherits="VEHICLE_MAINTENANCE_Transaction_VehicleSchedule" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--  <script src="../../JAVASCRIPTS/JScriptAdmin_Module.js" type="text/javascript"></script>--%>
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div2" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">VEHICLE SCHEDULE </h3>
                </div>

                <div class="box-body">
                    <asp:Panel ID="Panel1" runat="server">
                        <div class="col-12">
                            <%--  <div class="panel panel-heading">Add/Edit Vehicle Schedule</div>--%>
                            <div class="row">
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Select Date</label>
                                    </div>
                                    <div class="input-group date">
                                        <div class="input-group-addon" id="img3">
                                            <i class="fa fa-calendar text-blue"></i>
                                        </div>
                                        <asp:TextBox ID="txtDate" runat="server" CssClass="form-control" ToolTip="Select Date"
                                            ValidationGroup="Submit" TabIndex="1" Style="z-index: 0;"></asp:TextBox>
                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="true"
                                            EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="img3" TargetControlID="txtDate">
                                        </ajaxToolKit:CalendarExtender>
                                        <ajaxToolKit:MaskedEditExtender ID="medt" runat="server"
                                            AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                            MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtDate" ClearMaskOnLostFocus="true">
                                        </ajaxToolKit:MaskedEditExtender>
                                        <ajaxToolKit:MaskedEditValidator ID="MEVDate" runat="server" ControlExtender="medt"
                                            ControlToValidate="txtDate" IsValidEmpty="false"
                                            ErrorMessage="Please Enter Valid Date In [dd/MM/yyyy] format"
                                            InvalidValueMessage="Please Enter Valid Date In [dd/MM/yyyy] format"
                                            Display="None" Text="*" ValidationGroup="Submit"></ajaxToolKit:MaskedEditValidator>
                                        <asp:RequiredFieldValidator ID="rfvDate" runat="server" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select Date."
                                            ValidationGroup="Submit" ControlToValidate="txtDate"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Morning Trip</label>
                                    </div>
                                    <asp:TextBox ID="txtMorTrip" runat="server" MaxLength="1023" CssClass="form-control"
                                        TabIndex="2" ToolTip="Enter Morning Trip" TextMode="MultiLine">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvMorTrip" runat="server" SetFocusOnError="true"
                                        Display="None" ErrorMessage="Please Enter Morning Trip"
                                        ValidationGroup="Submit" ControlToValidate="txtMorTrip"></asp:RequiredFieldValidator>
                                    <ajaxToolKit:FilteredTextBoxExtender ID="ftbeMorTrip" runat="server" FilterType="Custom,UppercaseLetters,LowercaseLetters,Numbers"
                                        TargetControlID="txtMorTrip" ValidChars=",/ ">
                                    </ajaxToolKit:FilteredTextBoxExtender>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label>Special Trip</label>
                                    </div>
                                    <asp:TextBox ID="txtSpeTrip" runat="server" MaxLength="512" CssClass="form-control" TabIndex="3"
                                        ToolTip="Enter Special Trip" TextMode="MultiLine"></asp:TextBox>
                                    <ajaxToolKit:FilteredTextBoxExtender ID="ftbeSpeTrip" runat="server" FilterType="Custom,UppercaseLetters,LowercaseLetters,Numbers"
                                        TargetControlID="txtSpeTrip" ValidChars=",/ ">
                                    </ajaxToolKit:FilteredTextBoxExtender>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Evening Trip </label>
                                    </div>
                                    <asp:TextBox ID="txtEveTrip" runat="server" MaxLength="1023" CssClass="form-control" TabIndex="4"
                                        ToolTip="Enter Evening Trip" TextMode="MultiLine"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvEveTrip" runat="server" SetFocusOnError="true" Display="None"
                                        ErrorMessage="Please Enter Evening Trip."
                                        ValidationGroup="Submit" ControlToValidate="txtEveTrip"></asp:RequiredFieldValidator>
                                    <ajaxToolKit:FilteredTextBoxExtender ID="ftbeEveTrip" runat="server" FilterType="Custom,UppercaseLetters,LowercaseLetters,Numbers"
                                        TargetControlID="txtEveTrip" ValidChars=",/ ">
                                    </ajaxToolKit:FilteredTextBoxExtender>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label>Late Trip </label>
                                    </div>
                                    <asp:TextBox ID="txtLateTrip" runat="server" MaxLength="512" CssClass="form-control"
                                        TabIndex="6" ToolTip="Enter Late Trip"></asp:TextBox>
                                    <ajaxToolKit:FilteredTextBoxExtender ID="ftbeLateTrip" runat="server" FilterType="Custom,UppercaseLetters,LowercaseLetters,Numbers"
                                        TargetControlID="txtLateTrip" ValidChars=",/ ">
                                    </ajaxToolKit:FilteredTextBoxExtender>
                                </div>
                            </div>

                        </div>

                    </asp:Panel>
                    <%--</ContentTemplate>
                                </asp:UpdatePanel>--%>


                    <div class="col-12 btn-footer">
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="Submit" TabIndex="12"
                            OnClick="btnSubmit_Click" CssClass="btn btn-primary" ToolTip="Click here to Submit" CausesValidation="true" />
                        <asp:Button ID="btnReport" runat="server" Text="Report" OnClick="btnReport_Click" TabIndex="14"
                            CssClass="btn btn-info" ToolTip="You can also select Hire Type." />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" TabIndex="13"
                            CssClass="btn btn-warning" ToolTip="Click here to Reset" CausesValidation="false" />

                        <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Submit" />
                    </div>

                    <div class="col-12 mt-3">
                        <asp:ListView ID="lvSchedule" runat="server">
                            <LayoutTemplate>
                                <div id="lgv1">
                                    <div class="sub-heading">
                                        <h5>Vehicle Schedule Entry List</h5>
                                    </div>
                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                        <thead class="bg-light-blue">
                                            <tr>
                                                <th>Edit
                                                </th>
                                                <th>Date
                                                </th>
                                                <th>Morning Trip
                                                </th>
                                                <th>Special Trip
                                                </th>
                                                <th>Evening Trip (03:30pm)
                                                </th>
                                                <th>Late Trip (04:30pm)
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
                                            CommandArgument='<%# Eval("SCHEDULEID") %>' AlternateText="Edit Record" ToolTip="Edit Record"
                                            OnClick="btnEdit_Click" />
                                    </td>
                                    <td>
                                        <%# Eval("SCHEDULE_DATE","{0:dd-MMM-yyyy}")%>
                                    </td>
                                    <td>
                                        <%# Eval("MORNING_TRIP")%>
                                    </td>
                                    <td>
                                        <%# Eval("SPECIAL_TRIP")%>
                                    </td>
                                    <td>
                                        <%# Eval("EVENING_TRIP")%>
                                    </td>
                                    <td>
                                        <%# Eval("LATE_TRIP")%>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:ListView>
                    </div>

                </div>
            </div>
        </div>
    </div>



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
    </script>
</asp:Content>


