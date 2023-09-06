<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Metertarrif_master.aspx.cs" Inherits="ESTATE_Master_Metertarrif_master" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        function checkRange() {
            var ddlenergy = document.getElementById("<%=ddlenergymetertype.ClientID%>");
            var Text = ddlenergy.options[ddlenergy.selectedIndex].text;
            var Value = ddlenergy.options[ddlenergy.selectedIndex].value;

            var txtenergyfrom = document.getElementById('<%=txtenergyfrom.ClientID%>').value;

            var table = document.getElementById('tbitems');

            for (var r = 1; r < table.rows.length; r++) {

                var lbleneergytype = document.getElementById('ctl00_ContentPlaceHolder1_tabmetercharges_tabpnlmetercharges_energy_Repeater_energymetercharges_ctl0' + r.toString() + '_lblenergymetertype').innerHTML;


                var lblto = document.getElementById('ctl00_ContentPlaceHolder1_tabmetercharges_tabpnlmetercharges_energy_Repeater_energymetercharges_ctl0' + r.toString() + '_lblenergyTo').innerHTML;

                if (lbleneergytype == Text) {
                    if (parseInt(txtenergyfrom) <= parseInt(lblto)) {

                        document.getElementById('<%=txtenergyfrom.ClientID%>').value = '';
                        //                     document.getElementById('<%=txtenergyto.ClientID %>').value = '';
                        //                     document.getElementById('<%=txtenergyrate.ClientID %>').value = '';
                        alert("Value Already Exists!");
                        return;
                    }
                }

            }
        }
    </script>

    <script type="text/javascript">
        function CompareValueEnergy() {
            var Mat1 = document.getElementById('<%=txtenergyfrom.ClientID%>').value;
            var Mat2 = document.getElementById('<%=txtenergyto.ClientID %>').value;
            if (parseInt(Mat2) < parseInt(Mat1)) {

                alert("From reading Can not be greater then To reading.")
                document.getElementById('<%=txtenergyto.ClientID %>').value = '';
                return false;
            }
            else {
            }
        }
    </script>

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
        }*/
    </style>

    <asp:UpdatePanel ID="updpnltariif" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">METER TARIFF MASTER</h3>
                        </div>
                        <div id="tabs">
                            <div class="nav-tabs-custom">
                                <ul class="nav nav-tabs">
                                    <li class="active" id="tabpnlmetercharges_energy" runat="server"><a href="#energy" data-toggle="tab" aria-expanded="true">Energy</a></li>
                                    <li class="" id="tabpnlmetercharges_water" runat="server" visible="false"><a href="#water" data-toggle="tab" aria-expanded="true">Water</a></li>
                                </ul>
                                <div class="tab-content" id="tabmetercharges" runat="server">
                                    <div class="tab-pane active" id="energy">
                                        <asp:UpdatePanel ID="updpnlcharges" runat="server">
                                            <ContentTemplate>
                                                <div class="box-body">
                                                    <div class="col-md-12">
                                                        <div class="box-body">
                                                            <h5>Note <b>:</b> <span style="color: #FF0000">* Marked Is Mandatory !</span></h5>
                                                            <div class="panel panel-info">
                                                                <div class="panel-heading">
                                                                    Add/Edit Meter Tariff Master
                                                                </div>
                                                                <div class="panel-body">
                                                                    <div class="form-group row">
                                                                        <div class="col-md-4">
                                                                            <label>Meter Type<span style="color: red;">*</span>:</label>
                                                                            <asp:DropDownList ID="ddlenergymetertype" runat="server" AppendDataBoundItems="True"
                                                                                CssClass="form-control" TabIndex="1">
                                                                            </asp:DropDownList>
                                                                            <asp:RequiredFieldValidator ID="rfvenergymetrType" runat="server" ControlToValidate="ddlenergymetertype"
                                                                                ErrorMessage="Please select Energy Meter Type" ValidationGroup="Esate" SetFocusOnError="true" InitialValue="0" Display="None"></asp:RequiredFieldValidator>
                                                                        </div>

                                                                        <div class="col-md-4">
                                                                            <label>From<span style="color: red;">*</span>:</label>
                                                                            <div class="input-group">
                                                                                <asp:TextBox ID="txtenergyfrom" runat="server" CssClass="form-control"
                                                                                    onblur="checkRange();" onclick="CompareValueEnergy();" TabIndex="2" MaxLength="10"></asp:TextBox>
                                                                                <ajaxToolKit:FilteredTextBoxExtender ID="ftbeeneryFrom" runat="server" FilterType="Numbers" TargetControlID="txtenergyfrom">
                                                                                </ajaxToolKit:FilteredTextBoxExtender>
                                                                                <asp:RequiredFieldValidator ID="rfvenergyFrom" runat="server" ControlToValidate="txtenergyfrom"
                                                                                    ErrorMessage="From Value of Unit is Required." ValidationGroup="Esate"
                                                                                    SetFocusOnError="True" Display="None"></asp:RequiredFieldValidator>
                                                                                <div class="input-group-addon">
                                                                                    <label>Units.</label>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="col-md-4">
                                                                            <label>To<span style="color: red;">*</span>:</label>
                                                                            <div class="input-group">
                                                                                <asp:TextBox ID="txtenergyto" runat="server" CssClass="form-control" onblur="CompareValueEnergy();" TabIndex="3" MaxLength="10"></asp:TextBox>
                                                                                <ajaxToolKit:FilteredTextBoxExtender ID="ftbeEnergyTo" runat="server" FilterType="Numbers" TargetControlID="txtenergyto">
                                                                                </ajaxToolKit:FilteredTextBoxExtender>
                                                                                <asp:RequiredFieldValidator ID="rfvenergyTo" runat="server" ControlToValidate="txtenergyto"
                                                                                    ErrorMessage="To Value Of Energy is Required" ValidationGroup="Esate"
                                                                                    SetFocusOnError="True" Display="None"></asp:RequiredFieldValidator>
                                                                                <div class="input-group-addon">
                                                                                    <label>Units.</label>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                    <div class="form-group row">
                                                                        <div class="col-md-4">
                                                                            <label>Rate/Unit<span style="color: red;">*</span>:</label>
                                                                            <div class="input-group">
                                                                                <asp:TextBox ID="txtenergyrate" runat="server" CssClass="form-control" TabIndex="4" MaxLength="10"></asp:TextBox>
                                                                                <asp:RequiredFieldValidator ID="rfvenergyRate" runat="server" ControlToValidate="txtenergyrate"
                                                                                    ErrorMessage="Rate Per Units Required" ValidationGroup="Esate"
                                                                                    SetFocusOnError="True" Display="None"></asp:RequiredFieldValidator>
                                                                                <ajaxToolKit:FilteredTextBoxExtender ID="ftbeEnergyrate" runat="server" TargetControlID="txtenergyrate"
                                                                                    ValidChars="0123456789." Enabled="True">
                                                                                </ajaxToolKit:FilteredTextBoxExtender>
                                                                                <div class="input-group-addon">
                                                                                    <label>Rs.</label>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                    <div class="form-group row">
                                                                        <div class="col-md-12">
                                                                            <div class="text-center">
                                                                                <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="True"
                                                                                    ShowSummary="False" ValidationGroup="Esate" />
                                                                                <asp:Button ID="btnenergysubmit" runat="server" CssClass="btn btn-primary" Text="Submit" TabIndex="5"
                                                                                    OnClick="btnenergysubmit_Click" ValidationGroup="Esate" OnKeypress="checkRange();" OnKeyUp="CompareValueEnergy();" />
                                                                                <asp:Button ID="btnenergyreset" runat="server" CssClass="btn btn-warning" Text="Reset" TabIndex="6"
                                                                                    OnClick="btnenergyreset_Click" />
                                                                                <asp:Button ID="btnReport" runat="server" CssClass="btn btn-info" Text="Report" TabIndex="7"
                                                                                    OnClick="btnReport_Click" />
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                    <div class="form-group row">
                                                                        <div class="col-md-12">
                                                                            <asp:Panel ID="fldtariff" runat="server">
                                                                                <div class="table-responsive">
                                                                                    <asp:Repeater ID="Repeater_energymetercharges" runat="server" OnItemCommand="Repeater_energymetercharges_ItemCommand">
                                                                                        <HeaderTemplate>
                                                                                            <table id="tbitems" class="table table-bordered table-hover">
                                                                                                <thead>
                                                                                                    <tr class="bg-light-blue">
                                                                                                        <th>EDIT
                                                                                                        </th>
                                                                                                        <th>METER TYPE
                                                                                                        </th>
                                                                                                        <th>FROM
                                                                                                        </th>
                                                                                                        <th>TO
                                                                                                        </th>
                                                                                                        <th>RATE/UNIT
                                                                                                        </th>
                                                                                                    </tr>
                                                                                                </thead>
                                                                                                <tbody>
                                                                                        </HeaderTemplate>
                                                                                        <ItemTemplate>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:ImageButton ID="ImageButton_Edit" runat="server" CommandArgument='<%#Eval("ID")%>' CommandName="edit" ImageUrl="../../IMAGES/edit.gif" ToolTip="Update this  Investigation" />
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lblenergymetertype" runat="server" Text=' <%#Eval("METER_TYPE")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lblenergyfrom" runat="server" Text=' <%#Eval("FROM")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lblenergyTo" runat="server" Text=' <%#Eval("TO")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lblenergyrateunit" runat="server" Text=' <%#Eval("RATE/UNIT")%>'></asp:Label>
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
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <div class="box-body">
                                                    <div class="col-md-12">
                                                        <div class="box-body">
                                                            <h5>Note <b>:</b> <span style="color: #FF0000">* Marked Is Mandatory !</span></h5>
                                                            <div class="panel panel-info">
                                                                <div class="panel-heading">
                                                                    Add/Edit Meter Tariff Master
                                                                </div>
                                                                <div class="panel-body">
                                                                    <div class="form-group row">
                                                                        <div class="col-md-4">
                                                                            <label>Meter Type:</label>
                                                                            <asp:DropDownList ID="ddlwatermetertype" runat="server" AppendDataBoundItems="True" CssClass="form-control">
                                                                            </asp:DropDownList>
                                                                            <asp:RequiredFieldValidator ID="rfvwaterMeterType" runat="server" ControlToValidate="ddlwatermetertype"
                                                                                ErrorMessage="Please select Water Meter Type" ValidationGroup="Est" SetFocusOnError="true" InitialValue="0" Display="None"></asp:RequiredFieldValidator>
                                                                        </div>

                                                                        <div class="col-md-4">
                                                                            <label>From:</label>
                                                                            <div class="input-group">
                                                                                <asp:TextBox ID="txtwaterfrom" runat="server" CssClass="form-control"
                                                                                    onblur="checkrangeWater();" onclick="CompareValueWater();"></asp:TextBox>
                                                                                <asp:RequiredFieldValidator ID="rfvWatwrFrom" runat="server" ControlToValidate="txtwaterfrom"
                                                                                    ErrorMessage="Units Of Water From Required" ValidationGroup="Est" SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>
                                                                                <ajaxToolKit:FilteredTextBoxExtender ID="ftbeWaterFrom" runat="server"
                                                                                    FilterType="Numbers" TargetControlID="txtwaterfrom" Enabled="True">
                                                                                </ajaxToolKit:FilteredTextBoxExtender>
                                                                                <div class="input-group-addon">
                                                                                    <label>Units.</label>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="col-md-4">
                                                                            <label>To:</label>
                                                                            <div class="input-group">
                                                                                <asp:TextBox ID="txtwaterto" runat="server" CssClass="form-control" onblur="CompareValueWater();"></asp:TextBox>
                                                                                <asp:RequiredFieldValidator ID="rfvWaterTo" runat="server" ControlToValidate="txtwaterto"
                                                                                    ErrorMessage="Units Of Water To Required" ValidationGroup="Est" SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>
                                                                                <ajaxToolKit:FilteredTextBoxExtender ID="ftbewaterTo" runat="server"
                                                                                    FilterType="Numbers" TargetControlID="txtwaterto" Enabled="True">
                                                                                </ajaxToolKit:FilteredTextBoxExtender>
                                                                                <div class="input-group-addon">
                                                                                    <label>Units.</label>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                    <div class="form-group row">
                                                                        <div class="col-md-4">
                                                                            <label>Rate/Unit:</label>
                                                                            <div class="input-group">
                                                                                <asp:TextBox ID="txtwaterrate" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                <asp:RequiredFieldValidator ID="rfvWaterRate" runat="server" ControlToValidate="txtwaterrate"
                                                                                    ErrorMessage="Rate Per Units Required" ValidationGroup="Est" SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>
                                                                                <ajaxToolKit:FilteredTextBoxExtender ID="ftbeWaterRate" runat="server" TargetControlID="txtwaterrate"
                                                                                    ValidChars="0123456789." Enabled="True">
                                                                                </ajaxToolKit:FilteredTextBoxExtender>
                                                                                <div class="input-group-addon">
                                                                                    <label>Rs.</label>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                    <div class="form-group row">
                                                                        <div class="col-md-12">
                                                                            <div class="text-center">
                                                                                <asp:ValidationSummary ID="valsum" runat="server" ValidationGroup="Est" DisplayMode="List"
                                                                                    ShowMessageBox="true" ShowSummary="False" />
                                                                                <asp:Button ID="btnwatersubmit" runat="server" Text="Submit" CssClass="btn btn-primary"
                                                                                    OnClick="btnwatersubmit_Click" ValidationGroup="Est" OnKeypress="checkrangeWater();" />
                                                                                <asp:Button ID="btnwaterreset" runat="server" Text="Reset" CssClass="btn btn-warning"
                                                                                    OnClick="btnwaterreset_Click" />
                                                                                <asp:Button ID="btnWaterReport" runat="server" Text="Report"
                                                                                    CssClass="btn btn-info" OnClick="btnWaterReport_Click" />
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                    <div class="form-group row">
                                                                        <div class="col-md-12">
                                                                            <asp:Panel ID="fldwater" runat="server">
                                                                                <div class="table-responsive">
                                                                                    <asp:Repeater ID="rpt_watermetercharges" runat="server" OnItemCommand="rpt_watermetercharges_ItemCommand">
                                                                                        <HeaderTemplate>
                                                                                            <table class="table table-bordered table-hover" id="tbitemswater">
                                                                                                <thead>
                                                                                                    <tr class="bg-light-blue">
                                                                                                        <th>Edit
                                                                                                        </th>
                                                                                                        <th>Meter Type
                                                                                                        </th>
                                                                                                        <th>From
                                                                                                        </th>
                                                                                                        <th>To
                                                                                                        </th>
                                                                                                        <th>Rate/Unit
                                                                                                        </th>
                                                                                                    </tr>
                                                                                                </thead>
                                                                                                <tbody>
                                                                                        </HeaderTemplate>
                                                                                        <ItemTemplate>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:ImageButton ID="ImageButton_Edit" runat="server" CommandArgument='<%#Eval("ID")%>' CommandName="edit" ImageUrl="../../IMAGES/edit.gif" ToolTip="Update this  Investigation" />
                                                                                                </td>

                                                                                                <td>
                                                                                                    <asp:Label ID="lblwatermetertype" runat="server" Text=' <%#Eval("METER_TYPE")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lblwaterfrom" runat="server" Text=' <%#Eval("FROM")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lblwaterto" runat="server" Text=' <%#Eval("TO")%>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lblwaterrateunit" runat="server" Text=' <%#Eval("RATE/UNIT")%>'></asp:Label>
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

