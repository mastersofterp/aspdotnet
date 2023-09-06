<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="meterType_Master.aspx.cs" Inherits="ESTATE_Master_meterType_Master" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="updpnlmetertype" runat="server">
        <ContentTemplate>

            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">METER TYPE MASTER</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-md-12">
                                <asp:Panel ID="pnl" runat="server">
                                    <div class="panel panel-info">
                                        <div class="panel-heading">
                                            Add/Edit Meter Type Master
                                        </div>
                                        <div class="panel-body">
                                            <div class="form-group row">
                                                <div class="col-md-12">
                                                    Note <b>:</b> <span style="color: #FF0000">* Marked Is Mandatory !</span>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <div class="col-md-2">
                                                    <label>Meter Type Name<span style="color: red;">*</span>:</label>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txtmetertypeName" runat="server" CssClass="form-control" TabIndex="1" MaxLength="25"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvmetername" runat="server" ControlToValidate="txtmetertypeName"
                                                        ErrorMessage="Please Enter Meter Type Name." ValidationGroup="metertype" SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="ftbeMeterType" runat="server" TargetControlID="txtmetertypeName"
                                                        FilterType="Custom,LowercaseLetters,  Numbers, UppercaseLetters" ValidChars="-/\ ">
                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <div class="col-md-2">
                                                    <label>Type:</label>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:DropDownList ID="ddltype" runat="server" AppendDataBoundItems="true" CssClass="form-control" TabIndex="2">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvddldtype" runat="server" Display="None" SetFocusOnError="true" ControlToValidate="ddltype" InitialValue="0" ValidationGroup="metertype" ErrorMessage="Please Select Type."></asp:RequiredFieldValidator>
                                                    <asp:ValidationSummary ID="vsummeterType" runat="server" ValidationGroup="metertype" ShowMessageBox="true" ShowSummary="false" />
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <div class="col-md-2">
                                                </div>
                                                <div class="col-md-10">
                                                    <asp:Button ID="btnsubmit" runat="server" Text="Submit" OnClick="btnsubmit_Click" ValidationGroup="metertype"
                                                        CssClass="btn btn-primary" TabIndex="3" />
                                                    <asp:Button ID="btnreset" runat="server" Text="Reset" OnClick="btnreset_Click"
                                                        CssClass="btn btn-warning" TabIndex="4" />
                                                    <asp:Button ID="btnreport" runat="server" Text="Report" OnClick="btnreport_Click" CssClass="btn btn-info" TabIndex="5" />
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <div class="col-md-12">
                                                    <asp:Panel runat="server" ID="fldmetertype">
                                                        <asp:Repeater ID="Repeater_metertype" runat="server" OnItemCommand="Repeater_metertype_ItemCommand">
                                                            <HeaderTemplate>
                                                                <table class="table table-bordered table-hover">
                                                                    <thead>
                                                                        <tr class="bg-light-blue">
                                                                            <th>EDIT
                                                                            </th>
                                                                            <th>TYPE
                                                                            </th>
                                                                            <th>METER TYPE NAME
                                                                            </th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td>
                                                                        <asp:ImageButton ID="ImageButton_Edit" runat="server" CommandArgument='<%#Eval("MTYPE_NO")%>' CommandName="edit" ImageUrl="../../IMAGES/edit.gif" ToolTip="Update this  Investigation" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lbltype" runat="server" Text=' <%#Eval("METER_HEAD")%>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblmetertype" runat="server" Text=' <%#Eval("METER_TYPE")%>'></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                </tbody></table>
                                                            </FooterTemplate>
                                                        </asp:Repeater>
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

