<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Quarter_master.aspx.cs" Inherits="ESTATE_Master_Quarter_master" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <asp:UpdatePanel ID="updquater" runat="server">
        <ContentTemplate>

            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">QUARTER NUMBER MASTER</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-md-12">
                                <asp:Panel ID="pnl" runat="server">
                                    <div class="panel panel-info">
                                        <div class="panel-heading">
                                            Add/Edit Quarter Number Master
                                        </div>
                                        <div class="panel-body">
                                            <div class="form-group row">
                                                <div class="col-md-12">
                                                    Note <b>:</b> <span style="color: #FF0000">* Marked Is Mandatory !</span>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <div class="col-md-4">
                                                    <label>Quarter Type<span style="color: red;">*</span>:</label>
                                                    <asp:DropDownList ID="ddlquaterType" runat="server" CssClass="form-control"
                                                        AppendDataBoundItems="true" TabIndex="1">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvddlquatertype" runat="server" ControlToValidate="ddlquaterType" ValidationGroup="quater" InitialValue="0" Display="None" ErrorMessage="Please Select Quater Type.">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                                <div class="col-md-4">
                                                    <label>Block <span style="color: red;">*</span>:</label>
                                                    <asp:DropDownList ID="ddlBlock" runat="server" CssClass="form-control" AppendDataBoundItems="true" TabIndex="2">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvBlock" runat="server" ControlToValidate="ddlBlock" ValidationGroup="quater" InitialValue="0" Display="None" ErrorMessage="Please Select Block.">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                                <div class="col-md-4">
                                                    <label>Quarter No <span style="color: red;">*</span>:</label>
                                                    <asp:TextBox ID="txtquarternumber" runat="server" CssClass="form-control" TabIndex="2" MaxLength="25"></asp:TextBox>
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="ftbeQnumber" runat="server" FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters"
                                                        TargetControlID="txtquarternumber" ValidChars="/\- ">
                                                    </ajaxToolKit:FilteredTextBoxExtender>

                                                    <asp:RequiredFieldValidator ID="rfvquaterNo" runat="server" ControlToValidate="txtquarternumber"
                                                        ErrorMessage="Quater Number Required." Display="None" ValidationGroup="quater" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <div class="col-md-4">
                                                    <label>Energy Consumer Number<span style="color: red;"></span>:</label>
                                                    <asp:TextBox ID="txtquarterName" runat="server" CssClass="form-control" TabIndex="3" MaxLength="25"></asp:TextBox>
                                                    <%--<asp:RequiredFieldValidator ID="rfvquarterName" runat="server" ControlToValidate="txtquarterName"
                                                        ErrorMessage="Quater Name Required." Display="None" ValidationGroup="quater" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="ftbeQName" runat="server" FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters"
                                                        TargetControlID="txtquarterName" ValidChars="/\- ">
                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                </div>
                                                <div class="col-md-4">
                                                    <label>Energy Meter Number<span style="color: red;"></span>:</label>
                                                    <asp:DropDownList ID="ddlmeternumber" runat="server" CssClass="form-control" AppendDataBoundItems="true" TabIndex="4">
                                                    </asp:DropDownList>
                                                    <%--<asp:RequiredFieldValidator ID="rfvddlmeternumber" runat="server" ControlToValidate="ddlmeternumber" ValidationGroup="quater" InitialValue="0" Display="None" ErrorMessage="Please Select Meter Number.">
                                                    </asp:RequiredFieldValidator>--%>
                                                </div>
                                                <div class="col-md-4">
                                                    <label>Connected Load<span style="color: red;"></span>:</label>
                                                    <div class="input-group">
                                                        <asp:TextBox ID="txtConLoad" runat="server" CssClass="form-control" TabIndex="3" MaxLength="2"></asp:TextBox>
                                                        <%--<asp:RequiredFieldValidator ID="rfvConLoad" runat="server" ControlToValidate="txtConLoad"
                                                            ErrorMessage="Connected Load Required." Display="None" ValidationGroup="quater" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="ftbeConLoad" runat="server" FilterType="Numbers"
                                                            TargetControlID="txtConLoad" ValidChars="">
                                                        </ajaxToolKit:FilteredTextBoxExtender>
                                                        <div class="input-group-addon">
                                                            <label>KW</label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group row" style="display: none">
                                                <div class="col-md-4">
                                                    <label>Water Meter Number:</label>
                                                    <asp:DropDownList ID="ddlwatermeter" runat="server" Width="205px" AppendDataBoundItems="true" TabIndex="5">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        <asp:ListItem Value="1">0</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <%--  <asp:RequiredFieldValidator ID="rfvddlcustomer" runat="server" Display="None" ControlToValidate ="ddlwatermeter"  ValidationGroup="quater"
                                                        InitialValue="0" ErrorMessage="Please Select Water Meter Number." ></asp:RequiredFieldValidator>--%>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <div class="col-md-12 text-center">
                                                    <asp:Button ID="btnsubmit" runat="server" CssClass="btn btn-primary" Text="Submit" ValidationGroup="quater" OnClick="btnsubmit_Click" TabIndex="6" />
                                                    <asp:Button ID="btnreset" runat="server" CssClass="btn btn-warning" Text="Reset" OnClick="btnreset_Click" TabIndex="7" />
                                                    <asp:Button ID="btnReport" runat="server" CssClass="btn btn-info" Text="Report" OnClick="btnReport_Click1" TabIndex="8" />
                                                    <asp:ValidationSummary ID="valsum" runat="server" ValidationGroup="quater" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" />
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <div class="col-md-12">
                                                    <asp:Panel ID="fldmetertype" runat="server">
                                                        <div class="table-responsive">
                                                            <asp:Repeater ID="Repeater_metertype" runat="server"
                                                                OnItemCommand="Repeater_metertype_ItemCommand">
                                                                <HeaderTemplate>
                                                                    <table class="table table-bordered table-hover">
                                                                        <thead>
                                                                            <tr class="bg-light-blue">
                                                                                <th>EDIT
                                                                                </th>
                                                                                <th>QUARTER TYPE 
                                                                                </th>
                                                                                <th>BLOCK NAME 
                                                                                </th>
                                                                                <th>QUARTER NUMBER 
                                                                                </th>
                                                                                <%--<th>ENERGY CONSUMER NUMBER
                                                                                </th>
                                                                                <th>METER NUMBER
                                                                                </th>
                                                                                <th>CONNECTED LOAD
                                                                                </th>--%>
                                                                                <%--  <th style="width:30%; text-align: center; display: none;">
                            WATER NUMBER
                          </th>   --%>
                                                                            </tr>
                                                                        </thead>
                                                                        <tbody>
                                                                </HeaderTemplate>

                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:ImageButton ID="ImageButton_Edit" runat="server" CommandArgument=' <%#Eval("IDNO")%>' CommandName="edit" ImageUrl="../../IMAGES/edit.gif" ToolTip="Update this  Investigation" />
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblquartertype" runat="server" Text=' <%#Eval("QUARTER_TYPE")%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="Label2" runat="server" Text=' <%#Eval("BLOCKNAME")%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblquarternumber" runat="server" Text=' <%#Eval("QUARTER_NO")%>'></asp:Label>
                                                                        </td>
                                                                        <%--<td>
                                                                            <asp:Label ID="Label1" runat="server" Text=' <%#Eval("QRTNAME")%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblmeternumber" runat="server" Text=' <%#Eval("energy")%>'></asp:Label>
                                                                        </td>

                                                                        <td>
                                                                            <asp:Label ID="lblConLoad" runat="server" Text=' <%#Eval("CONNECTED_LOAD")%>'></asp:Label>
                                                                        </td>--%>
                                                                        <%-- <td style="width:30%; text-align: center; display: none;">
                    <asp:Label ID="lblwatermeternumber" runat="server" CssClass="label" Text=' <%#Eval("water")%>'></asp:Label>                
 </td>--%>
                                                                    </tr>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    </tbody></table>
                                                                </FooterTemplate>
                                                            </asp:Repeater>
                                                        </div>
                                                    </asp:Panel>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
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

