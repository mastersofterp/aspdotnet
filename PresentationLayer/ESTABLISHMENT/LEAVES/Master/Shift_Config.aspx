<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Shift_Config.aspx.cs"
    Inherits="ESTABLISHMENT_LEAVES_Master_Shift_Config" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">SHIFT MASTER</h3>
                </div>

                <div class="box-body">
                    <div class="col-12">
                        <div class="row">
                            <div class="col-lg-3 col-md-5 col-12">
                                <asp:Panel ID="pnlSft" runat="server">
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="sub-heading">
                                                <h5>Add Shift Name</h5>
                                            </div>
                                        </div>

                                        <div class="form-group col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>College Name</label>
                                            </div>
                                            <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control" AppendDataBoundItems="true"
                                                TabIndex="1" AutoPostBack="true" ToolTip="Select College Name" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddlcollege" runat="server" ControlToValidate="ddlCollege"
                                                Display="None" ErrorMessage="Please Select College Name" ValidationGroup="shift"
                                                SetFocusOnError="true" InitialValue="0"></asp:RequiredFieldValidator>
                                        </div>


                                        <div class="form-group col-12" runat="server" id="divshifttext">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Shift As</label>
                                            </div>
                                            <asp:TextBox ID="txtShift" runat="server" CssClass="form-control" ToolTip="Enter Shift Name" TabIndex="1"
                                                onkeypress="return CheckAlphabet(event,this);" />
                                            <asp:RequiredFieldValidator ID="rfvShift" runat="server" ValidationGroup="shift"
                                                ControlToValidate="txtShift" ErrorMessage="Please Enter Shift Name" Display="None" />
                                            <asp:DropDownList ID="ddlShift" runat="server" AppendDataBoundItems="true" AutoPostBack="true" TabIndex="3"
                                                CssClass="form-control" Visible="False" OnSelectedIndexChanged="ddlShift_SelectedIndexChanged">
                                                <asp:ListItem Enabled="true" Selected="True" Text="Please Select" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>

                                    </div>
                                </asp:Panel>
                            </div>

                            <div class="col-lg-6 col-md-7 col-12">
                                <asp:Panel ID="pnlShift" runat="server">
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="sub-heading">
                                                <h5>Add Shift Details</h5>
                                            </div>
                                        </div>

                                        <div class="col-12">
                                            <table class="table table-bordered">
                                                <thead>
                                                    <tr>
                                                        <th>Check</th>
                                                        <th>Days</th>
                                                        <th>In-Time</th>
                                                        <th>Out-Time</th>
                                                        <th>Working Hours</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr>
                                                        <td>
                                                            <asp:CheckBox ID="chkSun" runat="server" ToolTip="Check for Sunday" TabIndex="2" /></td>
                                                        <td>Sunday</td>
                                                        <td>
                                                            <asp:TextBox ID="txtSunIn" runat="server" CssClass="form-control" ToolTip="Enter Sunday In-Time" TabIndex="3" />
                                                            <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="shift"
                                                                ControlToValidate="txtSunIn" ErrorMessage="Please Enter the Time" Display="None" />--%>
                                                            <ajaxToolKit:MaskedEditExtender ID="meeSunIn" runat="server" TargetControlID="txtSunIn"
                                                                ClearMaskOnLostFocus="false" MaskType="Time" Mask="99:99:99" />
                                                            <%--<ajaxToolKit:MaskedEditValidator ID="mevSunIn" ControlToValidate="txtSunIn" ControlExtender="meeSunIn"
                                                                SetFocusOnError="true" ValidationGroup="shift" MaximumValue="23:59:59" runat="server"
                                                                ErrorMessage="Invalid Time" MinimumValue="00:00:00" InvalidValueBlurredMessage="Invalid Time" IsValidEmpty="false" InitialValue="__/__/____"/>--%>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtSunOut" runat="server" CssClass="form-control" ToolTip="Enter Sunday Out-Time" TabIndex="4" AutoPostBack="true" OnTextChanged="txtSunOut_TextChanged" />
                                                            <ajaxToolKit:MaskedEditExtender ID="meeSunOut" runat="server" TargetControlID="txtSunOut"
                                                                ClearMaskOnLostFocus="false" MaskType="Time" Mask="99:99:99" />
                                                            <%--<ajaxToolKit:MaskedEditValidator ID="mevSunOut" ControlToValidate="txtSunOut" ControlExtender="meeSunOut"
                                                                SetFocusOnError="true" ValidationGroup="shift" MaximumValue="23:59:59" runat="server"
                                                                ErrorMessage="Invalid Time" MinimumValue="00:00:00" InvalidValueBlurredMessage="Invalid Time"  IsValidEmpty="false" InitialValue="__/__/____"/>--%>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtSunHours" runat="server" CssClass="form-control" ToolTip="Enter Sunday Working Hours" TabIndex="5" ReadOnly="true" />
                                                            <ajaxToolKit:MaskedEditExtender ID="meeSunHours" runat="server" TargetControlID="txtSunHours"
                                                                ClearMaskOnLostFocus="false" MaskType="Time" Mask="99:99:99" />
                                                            <%-- <ajaxToolKit:MaskedEditValidator ID="mevSunHours" ControlToValidate="txtSunHours" ControlExtender="meeSunHours"
                                                                SetFocusOnError="true" ValidationGroup="shift" MaximumValue="23:59:59" runat="server"
                                                                ErrorMessage="Invalid Time" MinimumValue="00:00:00" InvalidValueBlurredMessage="Invalid Time" IsValidEmpty="false" InitialValue="__/__/____" />--%>

                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:CheckBox ID="chkMon" runat="server" ToolTip="Check for Monday" TabIndex="6" /></td>
                                                        <td>Monday</td>
                                                        <td>
                                                            <asp:TextBox ID="txtMonIn" runat="server" CssClass="form-control" ToolTip="Enter Monday In-Time" TabIndex="7" />
                                                            <asp:RequiredFieldValidator ID="rfvMonIn" runat="server" ValidationGroup="shift"
                                                                ControlToValidate="txtMonIn" ErrorMessage="Please Enter the Time" Display="None" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                            <ajaxToolKit:MaskedEditExtender ID="meeMonIn" runat="server" TargetControlID="txtMonIn"
                                                                ClearMaskOnLostFocus="false" MaskType="Time" Mask="99:99:99" />
                                                            <ajaxToolKit:MaskedEditValidator ID="mevMonIn" ControlToValidate="txtMonIn" ControlExtender="meeMonIn"
                                                                SetFocusOnError="true" ValidationGroup="shift" MaximumValue="23:59:59" runat="server"
                                                                ErrorMessage="Invalid Time" MinimumValue="00:00:00" IsValidEmpty="false" InitialValue="99:99:99"
                                                                InvalidValueBlurredMessage="Invalid Time" InvalidValueMessage="Please Enter Monday InTime" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtMonOut" runat="server" CssClass="form-control" ToolTip="Enter Monday Out-Time" TabIndex="8" AutoPostBack="true" OnTextChanged="txtMonOut_TextChanged" />
                                                            <asp:RequiredFieldValidator ID="rfvMonOut" runat="server" ValidationGroup="shift"
                                                                ControlToValidate="txtMonOut" ErrorMessage="Please Enter the Time" Display="None"></asp:RequiredFieldValidator>
                                                            <ajaxToolKit:MaskedEditExtender ID="meeMonOut" runat="server" TargetControlID="txtMonOut"
                                                                ClearMaskOnLostFocus="false" MaskType="Time" Mask="99:99:99" />
                                                            <ajaxToolKit:MaskedEditValidator ID="mevMonOut" ControlToValidate="txtMonOut" ControlExtender="meeMonOut"
                                                                SetFocusOnError="true" ValidationGroup="shift" MaximumValue="23:59:59" runat="server"
                                                                ErrorMessage="Invalid Time" MinimumValue="00:00:00" InvalidValueBlurredMessage="Invalid Time" IsValidEmpty="false"
                                                                InitialValue="99:99:99" InvalidValueMessage="Please Enter Monday OutTime" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtMonHours" runat="server" CssClass="form-control" ToolTip="Enter Monday Working Hours" TabIndex="9"  ReadOnly="true"/>
                                                            <%--<ajaxToolKit:MaskedEditExtender ID="meeMonHours" runat="server" TargetControlID="txtMonHours"
                                                                ClearMaskOnLostFocus="false" MaskType="Time" Mask="99:99:99" />
                                                            <ajaxToolKit:MaskedEditValidator ID="meaMonHours" ControlToValidate="txtMonHours" ControlExtender="meeMonHours"
                                                                SetFocusOnError="true" ValidationGroup="shift" MaximumValue="23:59:59" runat="server"
                                                                ErrorMessage="Invalid Time" MinimumValue="00:00:00" InvalidValueBlurredMessage="Invalid Time" />--%>

                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:CheckBox ID="chkTue" runat="server" ToolTip="Check for Tuesday" TabIndex="10" /></td>
                                                        <td>Tuesday</td>
                                                        <td>
                                                            <asp:TextBox ID="txtTueIn" runat="server" CssClass="form-control" ToolTip="Enter Tuesday In-Time" TabIndex="11" />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="shift"
                                                                ControlToValidate="txtMonIn" ErrorMessage="Please Enter the Time" Display="None" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                            <ajaxToolKit:MaskedEditExtender ID="meeTueIn" runat="server" TargetControlID="txtTueIn"
                                                                ClearMaskOnLostFocus="false" MaskType="Time" Mask="99:99:99" />
                                                            <ajaxToolKit:MaskedEditValidator ID="mevTueIn" ControlToValidate="txtTueIn" ControlExtender="meeTueIn"
                                                                SetFocusOnError="true" ValidationGroup="shift" MaximumValue="23:59:59" runat="server"
                                                                ErrorMessage="Invalid Time" MinimumValue="00:00:00" InvalidValueBlurredMessage="Invalid Time" IsValidEmpty="false"
                                                                InitialValue="99:99:99" InvalidValueMessage="Please Enter Tuesday InTime" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtTueOut" runat="server" CssClass="form-control" ToolTip="Enter Tuesday Out-Time" TabIndex="12" AutoPostBack="true" OnTextChanged="txtTueOut_TextChanged" />
                                                             <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="shift"
                                                                ControlToValidate="txtMonIn" ErrorMessage="Please Enter the Time" Display="None" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                            <ajaxToolKit:MaskedEditExtender ID="meeTueOut" runat="server" TargetControlID="txtTueOut"
                                                                ClearMaskOnLostFocus="false" MaskType="Time" Mask="99:99:99" />
                                                            <ajaxToolKit:MaskedEditValidator ID="mevTueOut" ControlToValidate="txtTueOut" ControlExtender="meeTueOut"
                                                                SetFocusOnError="true" ValidationGroup="shift" MaximumValue="23:59:59" runat="server"
                                                                ErrorMessage="Invalid Time" MinimumValue="00:00:00" InvalidValueBlurredMessage="Invalid Time" IsValidEmpty="false" 
                                                                InitialValue="99:99:99"  InvalidValueMessage="Please Enter Tuesday OutTime"/>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtTueHours" runat="server" CssClass="form-control" ToolTip="Enter Tuesday Working Hours" TabIndex="13" ReadOnly="true" />
                                                            <%--<ajaxToolKit:MaskedEditExtender ID="meeTueHours" runat="server" TargetControlID="txtTueHours"
                                                                ClearMaskOnLostFocus="false" MaskType="Time" Mask="99:99:99" />
                                                            <ajaxToolKit:MaskedEditValidator ID="mevTueHours" ControlToValidate="txtTueHours" ControlExtender="meeTueHours"
                                                                SetFocusOnError="true" ValidationGroup="shift" MaximumValue="23:59:59" runat="server"
                                                                ErrorMessage="Invalid Time" MinimumValue="00:00:00" InvalidValueBlurredMessage="Invalid Time" />--%>

                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:CheckBox ID="chkWed" runat="server" ToolTip="Check for Wednesday" TabIndex="14" /></td>
                                                        <td>Wednesday</td>
                                                        <td>
                                                            <asp:TextBox ID="txtWedIn" runat="server" CssClass="form-control" ToolTip="Enter Wednesday In-Time" TabIndex="15" />
                                                             <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="shift"
                                                                ControlToValidate="txtMonIn" ErrorMessage="Please Enter the Time" Display="None" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                            <ajaxToolKit:MaskedEditExtender ID="meeWedIn" runat="server" TargetControlID="txtWedIn"
                                                                ClearMaskOnLostFocus="false" MaskType="Time" Mask="99:99:99" />
                                                            <ajaxToolKit:MaskedEditValidator ID="mevWedIn" ControlToValidate="txtWedIn" ControlExtender="meeWedIn"
                                                                SetFocusOnError="true" ValidationGroup="shift" MaximumValue="23:59:59" runat="server"
                                                                ErrorMessage="Invalid Time" MinimumValue="00:00:00" InvalidValueBlurredMessage="Invalid Time" IsValidEmpty="false" 
                                                                InitialValue="99:99:99" InvalidValueMessage="Please Enter Wednesday InTime" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtWedOut" runat="server" CssClass="form-control" ToolTip="Enter Wednesday Out-Time" TabIndex="16" AutoPostBack="true" OnTextChanged="txtWedOut_TextChanged" />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ValidationGroup="shift"
                                                                ControlToValidate="txtMonIn" ErrorMessage="Please Enter the Time" Display="None" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                            <ajaxToolKit:MaskedEditExtender ID="meeWedOut" runat="server" TargetControlID="txtWedOut"
                                                                ClearMaskOnLostFocus="false" MaskType="Time" Mask="99:99:99" />
                                                            <ajaxToolKit:MaskedEditValidator ID="mevWedOut" ControlToValidate="txtWedOut" ControlExtender="meeWedOut"
                                                                SetFocusOnError="true" ValidationGroup="shift" MaximumValue="23:59:59" runat="server"
                                                                ErrorMessage="Invalid Time" MinimumValue="00:00:00" InvalidValueBlurredMessage="Invalid Time" IsValidEmpty="false" 
                                                                InitialValue="99:99:99" InvalidValueMessage="Please Enter Wednesday OutTime"/>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtWedHours" runat="server" CssClass="form-control" ToolTip="Enter Wednesday Working Hours" TabIndex="17" ReadOnly="true" />
                                                            <%--<ajaxToolKit:MaskedEditExtender ID="meeWedHours" runat="server" TargetControlID="txtWedHours"
                                                                ClearMaskOnLostFocus="false" MaskType="Time" Mask="99:99:99" />
                                                            <ajaxToolKit:MaskedEditValidator ID="mevWedHours" ControlToValidate="txtWedHours" ControlExtender="meeWedHours"
                                                                SetFocusOnError="true" ValidationGroup="shift" MaximumValue="23:59:59" runat="server"
                                                                ErrorMessage="Invalid Time" MinimumValue="00:00:00" InvalidValueBlurredMessage="Invalid Time" />--%>

                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:CheckBox ID="chkThu" runat="server" ToolTip="Check for Thursday" TabIndex="18" /></td>
                                                        <td>Thursday</td>
                                                        <td>
                                                            <asp:TextBox ID="txtThuIn" runat="server" CssClass="form-control" ToolTip="Enter Thursday In-Time" TabIndex="19" />
                                                           <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ValidationGroup="shift"
                                                                ControlToValidate="txtMonIn" ErrorMessage="Please Enter the Time" Display="None" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                            <ajaxToolKit:MaskedEditExtender ID="meeThuIn" runat="server" TargetControlID="txtThuIn"
                                                                ClearMaskOnLostFocus="false" MaskType="Time" Mask="99:99:99" />
                                                            <ajaxToolKit:MaskedEditValidator ID="mevThuIn" ControlToValidate="txtThuIn" ControlExtender="meeThuIn"
                                                                SetFocusOnError="true" ValidationGroup="shift" MaximumValue="23:59:59" runat="server"
                                                                ErrorMessage="Invalid Time" MinimumValue="00:00:00" InvalidValueBlurredMessage="Invalid Time" IsValidEmpty="false" 
                                                                InitialValue="99:99:99" InvalidValueMessage="Please Enter Thursday InTime"/>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtThuOut" runat="server" CssClass="form-control" ToolTip="Enter Thursday Out-Time" TabIndex="20" AutoPostBack="true" OnTextChanged="txtThuOut_TextChanged" />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ValidationGroup="shift"
                                                                ControlToValidate="txtMonIn" ErrorMessage="Please Enter the Time" Display="None" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                            <ajaxToolKit:MaskedEditExtender ID="meeThuOut" runat="server" TargetControlID="txtThuOut"
                                                                ClearMaskOnLostFocus="false" MaskType="Time" Mask="99:99:99" />
                                                            <ajaxToolKit:MaskedEditValidator ID="mevThuOut" ControlToValidate="txtThuOut" ControlExtender="meeThuOut"
                                                                SetFocusOnError="true" ValidationGroup="shift" MaximumValue="23:59:59" runat="server"
                                                                ErrorMessage="Invalid Time" MinimumValue="00:00:00" InvalidValueBlurredMessage="Invalid Time" IsValidEmpty="false" 
                                                                InitialValue="99:99:99" InvalidValueMessage="Please Enter Thursday OutTime"/>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtthuHours" runat="server" CssClass="form-control" ToolTip="Enter Thursday Working Hours" TabIndex="21" ReadOnly="true" />
                                                            <%--<ajaxToolKit:MaskedEditExtender ID="meeThuHours" runat="server" TargetControlID="txtthuHours"
                                                                ClearMaskOnLostFocus="false" MaskType="Time" Mask="99:99:99" />
                                                            <ajaxToolKit:MaskedEditValidator ID="mevThuHours" ControlToValidate="txtthuHours" ControlExtender="meeThuHours"
                                                                SetFocusOnError="true" ValidationGroup="shift" MaximumValue="23:59:59" runat="server"
                                                                ErrorMessage="Invalid Time" MinimumValue="00:00:00" InvalidValueBlurredMessage="Invalid Time" />--%>

                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:CheckBox ID="chkFri" runat="server" ToolTip="Check for Friday" TabIndex="22" /></td>
                                                        <td>Friday</td>
                                                        <td>
                                                            <asp:TextBox ID="txtFriIn" runat="server" CssClass="form-control" ToolTip="Enter Friday In-Time" TabIndex="23" />
                                                             <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ValidationGroup="shift"
                                                                ControlToValidate="txtMonIn" ErrorMessage="Please Enter the Time" Display="None" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                            <ajaxToolKit:MaskedEditExtender ID="meeFriIn" runat="server" TargetControlID="txtFriIn"
                                                                ClearMaskOnLostFocus="false" MaskType="Time" Mask="99:99:99" />
                                                            <ajaxToolKit:MaskedEditValidator ID="mevFriIn" ControlToValidate="txtFriIn" ControlExtender="meeFriIn"
                                                                SetFocusOnError="true" ValidationGroup="shift" MaximumValue="23:59:59" runat="server"
                                                                ErrorMessage="Invalid Time" MinimumValue="00:00:00" InvalidValueBlurredMessage="Invalid Time" IsValidEmpty="false" 
                                                                InitialValue="99:99:99" InvalidValueMessage="Please Enter Friday InTime"/>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtFriOut" runat="server" CssClass="form-control" ToolTip="Enter Friday Out-Time" TabIndex="24" AutoPostBack="true" OnTextChanged="txtFriOut_TextChanged" />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ValidationGroup="shift"
                                                                ControlToValidate="txtMonIn" ErrorMessage="Please Enter the Time" Display="None" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                            <ajaxToolKit:MaskedEditExtender ID="meeFriOut" runat="server" TargetControlID="txtFriOut"
                                                                ClearMaskOnLostFocus="false" MaskType="Time" Mask="99:99:99" />
                                                            <ajaxToolKit:MaskedEditValidator ID="mevFriOut" ControlToValidate="txtFriOut" ControlExtender="meeFriOut"
                                                                SetFocusOnError="true" ValidationGroup="shift" MaximumValue="23:59:59" runat="server"
                                                                ErrorMessage="Invalid Time" MinimumValue="00:00:00" InvalidValueBlurredMessage="Invalid Time" IsValidEmpty="false"
                                                                 InitialValue="99:99:99" InvalidValueMessage="Please Enter Friday OutTime"/>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtFriHours" runat="server" CssClass="form-control" ToolTip="Enter Friday Working Hours" TabIndex="25" ReadOnly="true"/>
                                                            <%--<ajaxToolKit:MaskedEditExtender ID="meeFriHours" runat="server" TargetControlID="txtFriHours"
                                                                ClearMaskOnLostFocus="false" MaskType="Time" Mask="99:99:99" />
                                                            <ajaxToolKit:MaskedEditValidator ID="mevFriHours" ControlToValidate="txtFriHours" ControlExtender="meeFriHours"
                                                                SetFocusOnError="true" ValidationGroup="shift" MaximumValue="23:59:59" runat="server"
                                                                ErrorMessage="Invalid Time" MinimumValue="00:00:00" InvalidValueBlurredMessage="Invalid Time" />--%>

                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:CheckBox ID="chkSat" runat="server" ToolTip="Check for Saturday" TabIndex="26" /></td>
                                                        <td>Saturday</td>
                                                        <td>
                                                            <asp:TextBox ID="txtSatIn" runat="server" CssClass="form-control" ToolTip="Enter Saturday In-Time" TabIndex="27" />
                                                             <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ValidationGroup="shift"
                                                                ControlToValidate="txtMonIn" ErrorMessage="Please Enter the Time" Display="None" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                             <ajaxToolKit:MaskedEditExtender ID="meeSatIn" runat="server" TargetControlID="txtSatIn"
                                                                ClearMaskOnLostFocus="false" MaskType="Time" Mask="99:99:99" />
                                                            <ajaxToolKit:MaskedEditValidator ID="mevSatIn" ControlToValidate="txtSatIn" ControlExtender="meeSatIn"
                                                                SetFocusOnError="true" ValidationGroup="shift" MaximumValue="23:59:59" runat="server"
                                                                ErrorMessage="Invalid Time" MinimumValue="00:00:00" InvalidValueBlurredMessage="Invalid Time" IsValidEmpty="false" 
                                                                InitialValue="99:99:99" InvalidValueMessage="Please Enter Saturday InTime"/>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtSatOut" runat="server" CssClass="form-control" ToolTip="Enter Saturday Out-Time" TabIndex="28" AutoPostBack="true" OnTextChanged="txtSatOut_TextChanged" />
                                                             <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ValidationGroup="shift"
                                                                ControlToValidate="txtMonIn" ErrorMessage="Please Enter the Time" Display="None" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                            <ajaxToolKit:MaskedEditExtender ID="meeSatOut" runat="server" TargetControlID="txtSatOut"
                                                                ClearMaskOnLostFocus="false" MaskType="Time" Mask="99:99:99" />
                                                            <ajaxToolKit:MaskedEditValidator ID="mevSatOut" ControlToValidate="txtSatOut" ControlExtender="meeSatOut"
                                                                SetFocusOnError="true" ValidationGroup="shift" MaximumValue="23:59:59" runat="server"
                                                                ErrorMessage="Invalid Time" MinimumValue="00:00:00" InvalidValueBlurredMessage="Invalid Time" IsValidEmpty="false"
                                                                 InitialValue="99:99:99" InvalidValueMessage="Please Enter Saturday OutTime"/>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtSatHours" runat="server" CssClass="form-control" ToolTip="Enter Saturday Working Hours" TabIndex="29" ReadOnly="true" />
                                                           <%-- <ajaxToolKit:MaskedEditExtender ID="meeSatHours" runat="server" TargetControlID="txtSatHours"
                                                                ClearMaskOnLostFocus="false" MaskType="Time" Mask="99:99:99" />
                                                            <ajaxToolKit:MaskedEditValidator ID="mevSatHours" ControlToValidate="txtSatHours" ControlExtender="meeSatHours"
                                                                SetFocusOnError="true" ValidationGroup="shift" MaximumValue="23:59:59" runat="server"
                                                                ErrorMessage="Invalid Time" MinimumValue="00:00:00" InvalidValueBlurredMessage="Invalid Time" />--%>

                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>

                                        <div class="col-12 btn-footer">
                                            <asp:Button ID="btnSaveSft" runat="server" Text="Save" OnClick="btnSaveSft_Click" TabIndex="30"
                                                ValidationGroup="shift" CssClass="btn btn-primary" ToolTip="Click here to Submit" />
                                            <asp:Button ID="btnUpdatesft" runat="server" Text="Update" TabIndex="31" OnClick="btnUpdatesft_Click"
                                                ValidationGroup="shift" CssClass="btn btn-primary" ToolTip="Click here to Update" />
                                            <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" TabIndex="32"
                                                CssClass="btn btn-primary" ToolTip="Click here to Go Back" />
                                            <asp:Button ID="btnCancelSft" runat="server" Text="Cancel" CausesValidation="false" TabIndex="33"
                                                OnClick="btnCancelSft_Click" CssClass="btn btn-warning" ToolTip="Click here to Reset" />
                                            <asp:ValidationSummary ID="valShift" runat="server" ValidationGroup="shift" DisplayMode="List"
                                                ShowSummary="false" ShowMessageBox="true" />
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>

                    <div class="col-12 btn-footer">
                        <asp:LinkButton ID="btnAdd" runat="server" SkinID="LinkAddNew" OnClick="btnAdd_Click" Text="Add New Shift" TabIndex="26"
                            CssClass="btn btn-primary" ToolTip="Click here to Add New Shift"></asp:LinkButton>
                        <asp:LinkButton ID="btnModifySft" runat="server" SkinID="LinkAddNew" OnClick="btnModifySft_Click" Text="Modify Shift" TabIndex="27"
                            CssClass="btn btn-primary" ToolTip="Click here to Modify Shift"></asp:LinkButton>
                        <asp:Button ID="btnShowReport" runat="server" Text="Show Report" CssClass="btn btn-info" ToolTip="Click here to Show Report"
                            Visible="false" OnClick="btnShowReport_Click" TabIndex="27" />
                        <asp:Button ID="butBack" runat="server" OnClick="butBack_Click" CssClass="btn btn-primary" Text="Back"
                            ToolTip="Click here to Go on Attendance Configuration Page" TabIndex="28" />
                    </div>

                    <div class="col-12 pb-2">
                        <asp:Panel ID="pnlLvShift" runat="server">
                            <asp:ListView ID="lvShift" runat="server">
                                <EmptyDataTemplate>
                                    <p class="text-center text-bold">
                                        <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Shifts Available" />
                                    </p>
                                </EmptyDataTemplate>
                                <LayoutTemplate>
                                    <div id="lgv1">
                                        <div class="sub-heading">
                                            <h5>Shift List</h5>
                                        </div>
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>Sr.No
                                                    </th>
                                                    <th>College Name
                                                    </th>
                                                    <th>SHIFT NAME
                                                    </th>
                                                    <th>No. Of Working Days
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
                                            <%#Container.DataItemIndex+1 %>
                                        </td>
                                        <td>
                                            <%# Eval("COLLEGE_NAME") %>
                                        </td>
                                        <td>
                                            <%# Eval("SHIFTNAME") %>
                                        </td>
                                        <td>
                                            <%# Eval("NO_OF_DAYS") %>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                            <%--<div class="vista-grid_datapager">
                                <div class="text-center">
                                    <asp:DataPager ID="dpPager" runat="server" PagedControlID="lvShift" PageSize="10" OnPreRender="dpPager_PreRender">
                                        <Fields>
                                            <asp:NextPreviousPagerField FirstPageText="<<" PreviousPageText="<" ButtonType="Link"
                                                RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="true" ShowPreviousPageButton="true"
                                                ShowLastPageButton="false" ShowNextPageButton="false" />
                                            <asp:NumericPagerField ButtonType="Link" ButtonCount="7" CurrentPageLabelCssClass="Current" />
                                            <asp:NextPreviousPagerField LastPageText=">>" NextPageText=">" ButtonType="Link"
                                                RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="false" ShowPreviousPageButton="false"
                                                ShowLastPageButton="true" ShowNextPageButton="true" />

                                        </Fields>
                                    </asp:DataPager>
                                    .
                                </div>
                            </div>--%>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <asp:Panel ID="Panel1" runat="server" Width="100%">
    </asp:Panel>

    <script type="text/javascript" language="javascript">

        function CheckNumeric(event, obj) {
            var k = (window.event) ? event.keyCode : event.which;
            //alert(k);
            if (k == 8 || k == 9 || k == 43 || k == 95 || k == 0) {
                obj.style.backgroundColor = "White";
                return true;
            }
            if (k > 45 && k < 58) {
                obj.style.backgroundColor = "White";
                return true;

            }
            else {
                alert('Please Enter numeric Value');
                obj.focus();
            }
            return false;
        }
        function showConfirmDel(source) {
            this._source = source;
            this._popup = $find('mdlPopupDel');

            //  find the confirm ModalPopup and show it    
            this._popup.show();
        }
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
            //alert("validation");
        }

        //function validateAlphabet(txt) {
        //    var expAlphabet = /^[A-Za-z]+$/;
        //    if (txt.value.search(expAlphabet) == -1) {
        //        txt.value = txt.value.substring(0, (txt.value.length) - 1);
        //        txt.value = '';
        //        txt.focus = true;
        //        alert("Only Alphabets allowed!");
        //        return false;
        //    }
        //    else
        //        return true;
        onkeypress = "return CheckAlphabet(event,this);"
        function CheckAlphabet(event, obj) {

            var k = (window.event) ? event.keyCode : event.which;
            if (k == 8 || k == 9 || k == 43 || k == 95 || k == 0 || k == 32 || k == 46 || k == 13) {
                obj.style.backgroundColor = "White";
                return true;

            }
            if (k >= 65 && k <= 90 || k >= 97 && k <= 122) {
                obj.style.backgroundColor = "White";
                return true;

            }
            else {
                alert('Please Enter Alphabets Only!');
                obj.focus();
            }
            return false;
        }
    </script>
</asp:Content>

