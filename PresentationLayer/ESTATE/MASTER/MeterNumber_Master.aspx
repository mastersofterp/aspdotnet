<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="MeterNumber_Master.aspx.cs" Inherits="ESTATE_Master_MeterNumber_Master" Title="" %>

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

        .style1 {
            width: 10%;
            height: 27px;
        }

        .style2 {
            width: 30%;
            height: 27px;
        }

        .style3 {
            width: 2%;
            height: 27px;
        }

        .style4 {
            width: 38%;
            height: 27px;
        }*/
    </style>

    <div class="row" id="updPnl" runat="server">
        <div class="col-md-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">METER NUMBER MASTER </h3>
                </div>
                <div id="tabs">
                    <div class="nav-tabs-custom">
                        <ul class="nav nav-tabs">
                            <li class="active" id="tabpnlmeternumber_energy" runat="server"><a href="#energy" data-toggle="tab" aria-expanded="true">Energy</a></li>
                            <li class="" id="tabpnlmeternumber_water" runat="server" visible="false"><a href="#water" data-toggle="tab" aria-expanded="true">Water</a></li>
                        </ul>
                        <div class="tab-content" id="tabmeternumber">
                            <div class="tab-pane active" id="energy">
                                <asp:UpdatePanel ID="updEnergy" runat="server">
                                    <ContentTemplate>
                                        <div class="box-body">
                                            <div class="col-md-12">
                                                <div class="box-body">
                                                    <h5>Note <b>:</b> <span style="color: #FF0000">* Marked Is Mandatory !</span></h5>
                                                    <div class="panel panel-info">
                                                        <div class="panel-heading">
                                                            Add/Edit Energy
                                                        </div>
                                                        <div class="panel-body">
                                                            <div class="form-group row">
                                                                <div class="col-md-2">
                                                                    <label>Meter Type <span style="color: red;">*</span>:</label>
                                                                </div>
                                                                <div class="col-md-4">
                                                                    <asp:DropDownList ID="ddlmetertype" runat="server" AppendDataBoundItems="True" CssClass="form-control" TabIndex="1"></asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="rfvmeterType" runat="server" ControlToValidate="ddlmetertype"
                                                                        ErrorMessage="Please Select Meter Type." SetFocusOnError="True" ValidationGroup="energy" InitialValue="0" Display="None"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                            <div class="form-group row">
                                                                <div class="col-md-2">
                                                                    <label>Meter Number <span style="color: red;">*</span>:</label>
                                                                </div>
                                                                <div class="col-md-4">
                                                                    <asp:TextBox ID="txtmeternumber" runat="server" CssClass="form-control" TabIndex="2" MaxLength="20"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="rfvmeterNo" runat="server" ControlToValidate="txtmeternumber"
                                                                        ErrorMessage="Please Enter Meter Number." SetFocusOnError="True" ValidationGroup="energy"
                                                                        Display="None"></asp:RequiredFieldValidator>
                                                                    <ajaxToolKit:FilteredTextBoxExtender ID="ftbemeternumber" runat="server" FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters"
                                                                        TargetControlID="txtmeternumber" ValidChars="/\- ">
                                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                                </div>
                                                            </div>
                                                            <div class="form-group row" id="trErent" runat="server" visible="false">
                                                                <div class="col-md-2">
                                                                    <label>Rent:</label>
                                                                </div>
                                                                <div class="col-md-4">
                                                                    <asp:TextBox ID="txtrent" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="rfvRent" runat="server" ControlToValidate="txtrent"
                                                                        ErrorMessage="Meter Rent Required" SetFocusOnError="True" ValidationGroup="energy"
                                                                        Display="None"></asp:RequiredFieldValidator>
                                                                    <ajaxToolKit:FilteredTextBoxExtender ID="ftbeRent" runat="server" ValidChars="0123456789." TargetControlID="txtrent">
                                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                                </div>
                                                            </div>
                                                            <div class="form-group row" id="trEnergy" runat="server" visible="false">
                                                                <div class="col-md-2">
                                                                    <label>Electric Meter multiple:</label>
                                                                </div>
                                                                <div class="col-md-10">
                                                                    <asp:TextBox ID="txtelectricmetermultiple" runat="server" CssClass="form-control" Visible="false"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="rfvEmulMeter" runat="server" ControlToValidate="txtelectricmetermultiple"
                                                                        ErrorMessage="Multiple Electric Meter Required" SetFocusOnError="True"
                                                                        ValidationGroup="energy" Display="None"></asp:RequiredFieldValidator>
                                                                    <ajaxToolKit:FilteredTextBoxExtender ID="ftxeEmulti" runat="server" FilterType="Numbers" TargetControlID="txtelectricmetermultiple">
                                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                                </div>
                                                            </div>
                                                            <div class="form-group row">
                                                                <div class="col-md-2">
                                                                </div>
                                                                <div class="col-md-10">
                                                                    <asp:ValidationSummary ID="valsumeng" runat="server" ShowMessageBox="True"
                                                                        ShowSummary="False" ValidationGroup="energy" DisplayMode="List" />
                                                                    <asp:Button ID="btnsubmit" runat="server" Text="Submit" TabIndex="3"
                                                                        ValidationGroup="energy" CssClass="btn btn-primary" OnClick="btnsubmit_Click1" />
                                                                    <asp:Button ID="btnreset" runat="server" CssClass="btn btn-warning" Text="Reset" OnClick="btnreset_Click1" TabIndex="4" />
                                                                    <asp:Button ID="btnReport" runat="server" CssClass="btn btn-info" Text="Report" OnClick="btnReport_Click" TabIndex="5" />
                                                                </div>
                                                            </div>

                                                            <div class="form-group row">
                                                                <div class="col-md-12">
                                                                    <asp:Panel ID="fldenergy" runat="server">
                                                                        <asp:Repeater ID="Repeater_energymetercharges" runat="server" OnItemCommand="Repeater_energymetercharges_ItemCommand">
                                                                            <HeaderTemplate>
                                                                                <table class="table table-bordered table-hover">
                                                                                    <thead>
                                                                                        <tr class="bg-light-blue">
                                                                                            <th>EDIT
                                                                                            </th>

                                                                                            <th>METER TYPE
                                                                                            </th>

                                                                                            <th>METER NUMBER
                                                                                            </th>
                                                                                            <%--<th style="width: 25%; text-align: center;">
                         Rent
                        </th>--%>
                                                                                            <%--<th style="width: 30%; text-align: center;">
                         Electric meter Multiple
                        </th>--%>
                                                                                        </tr>
                                                                                    </thead>
                                                                                    <tbody>
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <tr>

                                                                                    <td>
                                                                                        <asp:ImageButton ID="ImageButton_Edit" runat="server" CommandArgument=' <%#Eval("M_ID")%>' CommandName="edit" ImageUrl="../../IMAGES/edit.gif" ToolTip="Update this  Investigation" />
                                                                                    </td>

                                                                                    <td>
                                                                                        <asp:Label ID="lblmetertype" runat="server" Text=' <%#Eval("METER_TYPE")%>'></asp:Label>
                                                                                    </td>
                                                                                    <td >
                                                                                        <asp:Label ID="lblmeternumber" runat="server" Text=' <%#Eval("METER_NO")%>'></asp:Label>
                                                                                    </td>
                                                                                    <%--<td style="width: 15%; text-align: center;">
                    <asp:Label ID="lblrent" runat="server" CssClass="label" Text=' <%#Eval("RENT")%>'></asp:Label>
                </td>--%>
                                                                                    <%--<td style="width: 30%; text-align: center;">
                    <asp:Label ID="lblelectricmultiple" runat="server" CssClass="label" Text=' <%#Eval("EMETER_MULTI")%>'></asp:Label>
                </td>--%>
                                                                                </tr>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                </tbody>
          </table>
                                                                            </FooterTemplate>

                                                                        </asp:Repeater>
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
                                                    <div class="panel panel-info">
                                                        <div class="panel-heading">
                                                            Add/Edit Water
                                                        </div>
                                                        <div class="panel-body">
                                                            <div class="form-group row">
                                                                <div class="col-md-2">
                                                                    <label>Meter Type<span style="color: red;">*</span>:</label>
                                                                </div>
                                                                <div class="col-md-4">
                                                                    <asp:DropDownList ID="ddlwatermetertype" runat="server" AppendDataBoundItems="True" CssClass="form-control">
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlwatermetertype"
                                                                        ErrorMessage="Meter Type Required" SetFocusOnError="True" ValidationGroup="water" InitialValue="0" Display="None"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                            <div class="form-group row">
                                                                <div class="col-md-2">
                                                                    <label>Meter Number<span style="color: red;">*</span>:</label>
                                                                </div>
                                                                <div class="col-md-4">
                                                                    <asp:TextBox ID="txtwatermeternumber" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="rfvwaterNo" runat="server" ControlToValidate="txtwatermeternumber"
                                                                        ErrorMessage="Water Meter Number Required" SetFocusOnError="True"
                                                                        ValidationGroup="water" Display="None"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                            <div class="form-group row">
                                                                <div class="col-md-2">
                                                                    <label>Rent<span style="color: red;">*</span>:</label>
                                                                </div>
                                                                <div class="col-md-4">
                                                                    <asp:TextBox ID="txtwaterrent" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="rfvwaterRent" runat="server" ControlToValidate="txtwaterrent"
                                                                        ErrorMessage="Water Rent Required" SetFocusOnError="True" ValidationGroup="water"
                                                                        Display="None"></asp:RequiredFieldValidator>
                                                                    <ajaxToolKit:FilteredTextBoxExtender ID="ftbeWaterRent" runat="server"
                                                                        ValidChars="0123456789." TargetControlID="txtwaterrent" Enabled="True">
                                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                                </div>
                                                            </div>
                                                            <div class="form-group row">
                                                                <div class="col-md-2">
                                                                </div>
                                                                <div class="col-md-10">
                                                                    <asp:ValidationSummary ID="valsumWater" runat="server" ShowMessageBox="True"
                                                                        ShowSummary="False" ValidationGroup="water" DisplayMode="List" />
                                                                    <asp:Button ID="btnwatersubmit" runat="server" CssClass="btn btn-primary" Text="Submit"
                                                                        ValidationGroup="water" OnClick="btnwatersubmit_Click1" />
                                                                    <asp:Button ID="btnwaterreset" runat="server" CssClass="btn btn-warning" Text="Reset"
                                                                        OnClick="btnwaterreset_Click" />
                                                                    <asp:Button ID="btnwaterReport" runat="server" Text="Report" CssClass="btn btn-info" OnClick="btnwaterReport_Click" />
                                                                </div>
                                                            </div>

                                                            <div class="form-group row">
                                                                <div class="col-md-12">
                                                                    <asp:Panel ID="fldwater" runat="server">
                                                                        <asp:Repeater ID="rpt_watermetercharges" runat="server" OnItemCommand="rpt_watermetercharges_ItemCommand">
                                                                            <HeaderTemplate>
                                                                                <table class="table table-bordered table-hover">
                                                                                    <thead>
                                                                                        <tr class="bg-light-blue">
                                                                                            <th style="width: 2%; text-align: center">Edit
                                                                                            </th>
                                                                                            <th style="width: 49%; text-align: center;">Meter Type
                                                                                            </th>
                                                                                            <th style="width: 49%; text-align: center;">Meter Number
                                                                                            </th>
                                                                                            <%--<th style="width: 35%; text-align: center;">
                         Rent
                        </th>
                                                                                            --%>
                                                                                        </tr>
                                                                                    </thead>
                                                                                    <tbody>
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <tr>

                                                                                    <td>
                                                                                        <asp:ImageButton ID="ImageButton_Edit" runat="server" CommandArgument='  <%#Eval("M_ID")%>' CommandName="edit" ImageUrl="../../IMAGES/edit.gif" ToolTip="Update this  Investigation" />
                                                                                    </td>

                                                                                    <td>
                                                                                        <asp:Label ID="lblwatermetertype" runat="server" CssClass="label" Text=' <%#Eval("METER_TYPE")%>'></asp:Label>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:Label ID="lblwatermeternumber" runat="server" CssClass="label" Text=' <%#Eval("METER_NO")%>'></asp:Label>
                                                                                    </td>
                                                                                    <%--<td style="width:35%; text-align: center;">
                    <asp:Label ID="lblwaterrent" runat="server" CssClass="label" Text=' <%#Eval("RENT")%>'></asp:Label>
                </td>--%>
                                                                                </tr>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                </tbody>
          </table>
                                                                            </FooterTemplate>

                                                                        </asp:Repeater>
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

                <ajaxToolKit:TabContainer ID="uu" Width="100%" CssClass="tab-content" runat="server" ActiveTabIndex="0" TabIndex="0">
                    <ajaxToolKit:TabPanel ID="nn" CssClass="tab-pane active" runat="server" TabIndex="0">
                        <HeaderTemplate>
                        </HeaderTemplate>
                    </ajaxToolKit:TabPanel>
                </ajaxToolKit:TabContainer>

            </div>
        </div>
    </div>
    <div id="divMsg" runat="server">
    </div>
</asp:Content>

