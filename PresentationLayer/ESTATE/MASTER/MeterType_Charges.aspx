<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="MeterType_Charges.aspx.cs" Inherits="ESTATE_Master_MeterType_Charges" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <style type="text/css">
        /*.ajax__myTab .ajax__tab_header {
            font-family: verdana,tahoma,helvetica;
            font-size: 11px;
            border-bottom: solid 1px #999999;
        }

        .ajax__myTab .ajax__tab_outer {
            padding-right: 4px;
            height: 21px;
            background-color: #C0C0C0;
            margin-right: 2px;
            border-right: solid 1px #666666;
            border-top: solid 1px #aaaaaa;
        }

        .ajax__myTab .ajax__tab_inner {
            padding-left: 3px;
            background-color: #C0C0C0;
        }

        .ajax__myTab .ajax__tab_tab {
            height: 13px;
            padding: 4px;
            margin: 0;
        }

        .ajax__myTab .ajax__tab_hover .ajax__tab_outer {
            background-color: #cccccc;
        }

        .ajax__myTab .ajax__tab_hover .ajax__tab_inner {
            background-color: #cccccc;
        }

        .ajax__myTab .ajax__tab_hover .ajax__tab_tab {
        }

        .ajax__myTab .ajax__tab_active .ajax__tab_outer {
            background-color: #fff;
            border-left: solid 1px #999999;
        }

        .ajax__myTab .ajax__tab_active .ajax__tab_inner {
            background-color: #fff;
        }

        .ajax__myTab .ajax__tab_active .ajax__tab_tab {
        }

        .ajax__myTab .ajax__tab_body {
            font-family: verdana,tahoma,helvetica;
            font-size: 10pt;
            border: 1px solid #999999;
            border-top: 0;
            padding: 8px;
            background-color: #ffffff;
        }

        .style9 {
            width: 10%;
            height: 31px;
        }

        .style10 {
            width: 30%;
            height: 31px;
        }

        .style11 {
            width: 2%;
            height: 31px;
        }

        .style12 {
            width: 38%;
            height: 31px;
        }*/
    </style>

    <%-- <asp:UpdatePanel ID="" runat="server">
        <ContentTemplate>--%>


    <div class="row" id="updpnlcharges" runat="server">
        <div class="col-md-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">METER TYPE CHARGES MASTER</h3>
                </div>
                <div id="tabs">
                    <div class="nav-tabs-custom">
                        <ul class="nav nav-tabs">
                            <li class="active" id="tabpnlmeternumber_energy" runat="server"><a href="#energy" data-toggle="tab" aria-expanded="true">Energy</a></li>
                            <li class="" id="tabpnlmeternumber_water" runat="server" visible="false"><a href="#water" data-toggle="tab" aria-expanded="true">Water</a></li>
                        </ul>
                        <div class="tab-content" id="tabmetercharges">
                            <div class="tab-pane active" id="energy">
                                <asp:UpdatePanel ID="updEnergy" runat="server">
                                    <ContentTemplate>
                                        <div class="box-body">
                                            <div class="col-md-12">
                                                <div class="box-body">
                                                    <h5>Note <b>:</b> <span style="color: #FF0000">* Marked Is Mandatory !</span></h5>
                                                    <div class="panel panel-info">
                                                        <div class="panel-heading">
                                                            Add/Edit Meter Type Charges Master
                                                        </div>
                                                        <div class="panel-body">
                                                            <div class="form-group row">
                                                                <div class="col-md-3">
                                                                    <label>Meter Type<span style="color: red;">*</span>:</label>
                                                                    <asp:DropDownList ID="ddlmetertype" runat="server" AppendDataBoundItems="True" TabIndex="1"
                                                                        CssClass="form-control">
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="rfvmeterType" runat="server" ControlToValidate="ddlmetertype"
                                                                        ErrorMessage="Please select Energy Meter Type" InitialValue="0" SetFocusOnError="true" ValidationGroup="energy" Display="None"></asp:RequiredFieldValidator>
                                                                </div>
                                                                <div class="col-md-3">
                                                                    <label>Fixed Charge<span style="color: red;">*</span>:</label>
                                                                    <asp:TextBox ID="txtfixedcharge" runat="server" CssClass="form-control" TabIndex="2" MaxLength="4"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="rfvfixedCharge" runat="server" ControlToValidate="txtfixedcharge"
                                                                        ErrorMessage="Fixed Charge Required" SetFocusOnError="true" ValidationGroup="energy" Display="None"></asp:RequiredFieldValidator>
                                                                    <ajaxToolKit:FilteredTextBoxExtender ID="ftbefenergyFixedCharge" runat="server" TargetControlID="txtfixedcharge"
                                                                        ValidChars="0123456789." Enabled="True">
                                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                                </div>
                                                                <div class="col-md-3">
                                                                    <label>Fca Charge<span style="color: red;">*</span>:</label>
                                                                    <asp:TextBox ID="txtfcacharge" runat="server" CssClass="form-control" TabIndex="3" MaxLength="4"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="rfvfcaCharge" runat="server" ControlToValidate="txtfcacharge"
                                                                        ErrorMessage="FCA Charge Required" SetFocusOnError="true" ValidationGroup="energy" Display="None"></asp:RequiredFieldValidator>
                                                                    <ajaxToolKit:FilteredTextBoxExtender ID="ftbenergyfcaCharge" runat="server" TargetControlID="txtfcacharge"
                                                                        ValidChars="0123456789." Enabled="True">
                                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                                </div>
                                                                <div class="col-md-3">
                                                                    <label>Electricity Duty<span style="color: red;">*</span>:</label>
                                                                    <asp:TextBox ID="txtelectricityduty" runat="server" CssClass="form-control" TabIndex="4" MaxLength="4"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="rfvElecDuty" runat="server" ControlToValidate="txtelectricityduty"
                                                                        ErrorMessage="Electricity Duty Required" SetFocusOnError="true" ValidationGroup="energy" Display="None"></asp:RequiredFieldValidator>
                                                                    <ajaxToolKit:FilteredTextBoxExtender ID="ftbeenergyElectricDuty" runat="server" TargetControlID="txtelectricityduty"
                                                                        ValidChars="0123456789." Enabled="True">
                                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                                </div>
                                                            </div>
                                                            <div class="form-group row">
                                                                <div class="col-md-3">
                                                                    <label>Lock Unit<span style="color: red;">*</span>:</label>
                                                                    <asp:TextBox ID="txtlocunit" runat="server" CssClass="form-control" TabIndex="5" MaxLength="4"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="rfvlocunit" runat="server" ControlToValidate="txtlocunit"
                                                                        ErrorMessage="Lock Unit Required" SetFocusOnError="true" ValidationGroup="energy" Display="None"></asp:RequiredFieldValidator>
                                                                    <ajaxToolKit:FilteredTextBoxExtender ID="ftbeEnergylocUnit" runat="server" TargetControlID="txtlocunit"
                                                                        ValidChars="0123456789." Enabled="True">
                                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                                </div>
                                                                <div class="col-md-3">
                                                                    <label>Avg. Unit<span style="color: red;">*</span>:</label>
                                                                    <asp:TextBox ID="txtenergyavgunit" runat="server" CssClass="form-control" TabIndex="6" MaxLength="4"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="rfveAvgUnit" runat="server" ControlToValidate="txtenergyavgunit"
                                                                        ErrorMessage=" AVG.Unit Required" SetFocusOnError="true" ValidationGroup="energy" Display="None"></asp:RequiredFieldValidator>
                                                                    <ajaxToolKit:FilteredTextBoxExtender ID="ftbeenergyAvgUnit" runat="server" TargetControlID="txtenergyavgunit"
                                                                        ValidChars="0123456789." Enabled="True">
                                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                                </div>
                                                                <div class="col-md-3">
                                                                    <label>Meter Rent<span style="color: red;">*</span>:</label>
                                                                    <asp:TextBox ID="txtmeterrent" runat="server" CssClass="form-control" TabIndex="7" MaxLength="4"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="rfvrent" runat="server" ControlToValidate="txtmeterrent"
                                                                        ErrorMessage=" Meter Rent Required" SetFocusOnError="true" ValidationGroup="energy" Display="None"></asp:RequiredFieldValidator>
                                                                    <ajaxToolKit:FilteredTextBoxExtender ID="ftbeEnergymeterRent" runat="server" TargetControlID="txtmeterrent"
                                                                        ValidChars="0123456789." Enabled="True">
                                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                                </div>
                                                            </div>
                                                            <div class="form-group row">
                                                                <div class="col-md-12 text-center">
                                                                    <asp:ValidationSummary ID="valsumEng" runat="server"
                                                                        DisplayMode="List" ValidationGroup="energy" ShowMessageBox="true" ShowSummary="false" />
                                                                    <asp:Button ID="btnsubmit" runat="server" CssClass="btn btn-primary" Text="Submit" TabIndex="8"
                                                                        OnClick="btnsubmit_Click" ValidationGroup="energy" />
                                                                    <asp:Button ID="btnreset" runat="server" CssClass="btn btn-warning" Text="Reset" TabIndex="9"
                                                                        OnClick="btnreset_Click" />
                                                                    <asp:Button ID="btnReport" runat="server" CssClass="btn btn-info" Text="Report" TabIndex="10"
                                                                        Width="61px" OnClick="btnReport_Click" />
                                                                </div>
                                                            </div>

                                                            <div class="form-group row">
                                                                <div class="col-md-12">
                                                                    <asp:Panel ID="fldenergy" runat="server">
                                                                        <div class="table-responsive">
                                                                            <asp:Repeater ID="Repeater_energymetercharges" runat="server" OnItemCommand="Repeater_energymetercharges_ItemCommand">
                                                                                <HeaderTemplate>
                                                                                    <table class="table table-responsive table-hover">
                                                                                        <thead>
                                                                                            <tr class="bg-light-blue">
                                                                                                <th>EDIT
                                                                                                </th>
                                                                                                <th>METER TYPE
                                                                                                </th>
                                                                                                <th>FIXED CHARGE
                                                                                                </th>
                                                                                                <th>FCA CHARGE
                                                                                                </th>
                                                                                                <th>ELECTRICITY DUTY
                                                                                                </th>
                                                                                                <th>LOCK UNIT
                                                                                                </th>
                                                                                                <th>AVG.UNIT
                                                                                                </th>
                                                                                                <th>METER RENT
                                                                                                </th>
                                                                                            </tr>
                                                                                        </thead>
                                                                                        <tbody>
                                                                                </HeaderTemplate>

                                                                                <ItemTemplate>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:ImageButton ID="ImageButton_Edit" runat="server" CommandArgument='<%#Eval("SRNO")%>' CommandName="edit" ImageUrl="../../IMAGES/edit.gif" ToolTip="Update this  Investigation" />
                                                                                        </td>

                                                                                        <td>
                                                                                            <asp:Label ID="lblmetertype" runat="server" Text=' <%#Eval("METER_TYPE")%>'></asp:Label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:Label ID="lblfixedcharge" runat="server" Text=' <%#Eval("FIX_CHRG")%>'></asp:Label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:Label ID="lblfcacharges" runat="server" Text=' <%#Eval("FCA_CHRG")%>'></asp:Label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:Label ID="lblelectriccityduty" runat="server" Text=' <%#Eval("ELECT_DUTY")%>'></asp:Label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:Label ID="lbllockunit" runat="server" Text=' <%#Eval("LOCK_UN")%>'></asp:Label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:Label ID="lblavgunit" runat="server" Text=' <%#Eval("AVG_UN")%>'></asp:Label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:Label ID="lblmeterrent" runat="server" Text=' <%#Eval("METER_RENT")%>'></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                    </tbody>
          </table>
                                                                                </FooterTemplate>

                                                                            </asp:Repeater>
                                                                        </div>
                                                                    </asp:Panel>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="tab-pane" id="water">
                                <asp:UpdatePanel ID="updWater" runat="server">
                                    <ContentTemplate>
                                        <div class="box-body">
                                            <div class="col-md-12">
                                                <div class="box-body">
                                                    <h5>Note <b>:</b> <span style="color: #FF0000">* Marked Is Mandatory !</span></h5>
                                                    </h5>
                                                    <div class="panel panel-info">
                                                        <div class="panel-heading">
                                                            Add/Edit Meter Type Charges Master
                                                        </div>
                                                        <div class="panel-body">
                                                            <div class="form-group row">
                                                                <div class="col-md-4">
                                                                    <label>Meter Type<span style="color: red;">*</span>:</label>
                                                                    <asp:DropDownList ID="ddlwatermetertype" runat="server" AppendDataBoundItems="True"
                                                                        CssClass="form-control">
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="rfvWaterMetreType" runat="server" ControlToValidate="ddlwatermetertype"
                                                                        ErrorMessage="Please select Water Meter Type" InitialValue="0" SetFocusOnError="true" ValidationGroup="water" Display="None"></asp:RequiredFieldValidator>
                                                                </div>
                                                                <div class="col-md-4">
                                                                    <label>Water Acc.charge<span style="color: red;">*</span>:</label>
                                                                    <asp:TextBox ID="txtwaterACCcharges" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="rfvAcc" runat="server" ControlToValidate="txtwaterACCcharges"
                                                                        ErrorMessage=" Water ACC.charge Required" Display="None" ValidationGroup="water" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                                    <ajaxToolKit:FilteredTextBoxExtender ID="ftbeWaterAccCharges" runat="server" TargetControlID="txtwaterACCcharges"
                                                                        ValidChars="0123456789." Enabled="True">
                                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                                </div>
                                                                <div class="col-md-4">
                                                                    <label>Avg.Unit<span style="color: red;">*</span>:</label>
                                                                    <asp:TextBox ID="txtavgunit" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="rfvavgUnit" runat="server" ControlToValidate="txtavgunit"
                                                                        ErrorMessage=" Water Lock Unit Required" Display="None" ValidationGroup="water" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                                    <ajaxToolKit:FilteredTextBoxExtender ID="ftbeWaterAvgUnit" runat="server" TargetControlID="txtavgunit"
                                                                        ValidChars="0123456789." Enabled="True">
                                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                                </div>
                                                            </div>
                                                            <div class="form-group row">
                                                                <div class="col-md-4">
                                                                    <label>Lock Unit<span style="color: red;">*</span>:</label>
                                                                    <asp:TextBox ID="txtwaterlockunit" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="rfvlockunit" runat="server" ControlToValidate="txtwaterlockunit"
                                                                        ErrorMessage=" Water Lock Unit Required" Display="None" ValidationGroup="water" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                                    <ajaxToolKit:FilteredTextBoxExtender ID="ftbeWaterLockUnit" runat="server" TargetControlID="txtwaterlockunit"
                                                                        ValidChars="0123456789." Enabled="True">
                                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                                </div>
                                                                <div class="col-md-4">
                                                                    <label>Meter Rent<span style="color: red;">*</span>:</label>
                                                                    <asp:TextBox ID="txtwatermeterunit" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="rfvwmeterUnit" runat="server" ControlToValidate="txtwatermeterunit"
                                                                        ErrorMessage=" Meter Rent Required" Display="None" ValidationGroup="water" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                                    <ajaxToolKit:FilteredTextBoxExtender ID="ftbeWaterMeterUnit" runat="server" TargetControlID="txtwatermeterunit"
                                                                        ValidChars="0123456789." Enabled="True">
                                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                                </div>
                                                            </div>
                                                            <div class="form-group row">
                                                                <div class="col-md-12 text-center">
                                                                    <asp:ValidationSummary ID="valsumWater" runat="server"
                                                                        DisplayMode="List" ValidationGroup="water" ShowMessageBox="true" ShowSummary="false" />
                                                                    <asp:Button ID="btnwatersubmit" CssClass="btn btn-primary" runat="server" Text="Submit"
                                                                        OnClick="btnwatersubmit_Click" ValidationGroup="water" />
                                                                    <asp:Button ID="btnwaterreset" CssClass="btn btn-warning" runat="server" Text="Reset" OnClick="btnwaterreset_Click" />
                                                                    <asp:Button ID="btnWaterReport" CssClass="btn btn-info" runat="server" Text="Report"
                                                                        OnClick="btnWaterReport_Click" />
                                                                </div>
                                                            </div>

                                                            <div class="form-group row">
                                                                <div class="col-md-12">
                                                                    <asp:Panel ID="fldwater" runat="server">
                                                                        <div class="table-responsive">
                                                                            <asp:Repeater ID="rpt_watermetercharges" runat="server" OnItemCommand="rpt_watermetercharges_ItemCommand">
                                                                                <HeaderTemplate>
                                                                                    <table class="table table-bordered table-hover">
                                                                                        <thead>
                                                                                            <tr class="bg-light-blue">
                                                                                                <th>Edit
                                                                                                </th>
                                                                                                <th>Meter Type
                                                                                                </th>
                                                                                                <th>Acc Charge
                                                                                                </th>
                                                                                                <th>Avg. Unit
                                                                                                </th>
                                                                                                <th>Lock Unit
                                                                                                </th>
                                                                                                <th>Meter Rent
                                                                                                </th>
                                                                                            </tr>
                                                                                        </thead>
                                                                                        <tbody>
                                                                                </HeaderTemplate>

                                                                                <ItemTemplate>
                                                                                    <tr>

                                                                                        <td>
                                                                                            <asp:ImageButton ID="ImageButton_Edit" runat="server" CommandArgument='<%#Eval("SRNO")%>' CommandName="edit" ImageUrl="../../IMAGES/edit.gif" ToolTip="Update this  Investigation" />
                                                                                        </td>

                                                                                        <td>
                                                                                            <asp:Label ID="lblwatermetertype" runat="server" Text=' <%#Eval("METER_TYPE")%>'></asp:Label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:Label ID="lblwaterAcccharge" runat="server" Text=' <%#Eval("ACC_CHRG")%>'></asp:Label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:Label ID="lblwateravgunit" runat="server" Text=' <%#Eval("AVG_UN")%>'></asp:Label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:Label ID="lblwaterlockunit" runat="server" Text=' <%#Eval("LOCK_UN")%>'></asp:Label>
                                                                                        </td>

                                                                                        <td>
                                                                                            <asp:Label ID="lblwatermeterrent" runat="server" Text=' <%#Eval("METER_RENT")%>'></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                    </tbody>
          </table>
                                                                                </FooterTemplate>
                                                                            </asp:Repeater>
                                                                        </div>
                                                                    </asp:Panel>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="divMsg" runat="server">
    </div>
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>

