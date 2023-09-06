<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="MaterialMaster.aspx.cs" Inherits="ESTATE_Master_MaterialMaster" Title=" " %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="updmasterialmaster" runat="server">
        <ContentTemplate>

            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">ASSETS MASTER</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-md-12">
                                <asp:Panel ID="pnl" runat="server">
                                    <div class="panel panel-info">
                                        <div class="panel-heading">
                                            Add/Edit Asset Master
                                        </div>
                                        <div class="panel-body">
                                            <div class="form-group row">
                                                <div class="col-md-12">
                                                    Note <b>:</b> <span style="color: #FF0000">* Marked Is Mandatory !</span>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <div class="col-md-2">
                                                    <label>Asset Name<span style="color: red;">*</span>:</label>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txtmaterialtype" runat="server" CssClass="form-control" TabIndex="1" MaxLength="30"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvmaterial" runat="server" ControlToValidate="txtmaterialtype"
                                                        ErrorMessage="Please Enter Asset Name." ValidationGroup="material" Display="None"></asp:RequiredFieldValidator>
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="ftbeType" runat="server" TargetControlID="txtmaterialtype" FilterType="Custom, Numbers, LowercaseLetters,UppercaseLetters"
                                                        ValidChars=" ">
                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                    <asp:ValidationSummary ID="valsumEng" runat="server"
                                                        DisplayMode="List" ValidationGroup="material" ShowMessageBox="true" ShowSummary="false" />
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <div class="col-md-2">
                                                </div>
                                                <div class="col-md-10">
                                                    <asp:Button ID="btnsubmit" runat="server" Text="Submit" OnClick="btnsubmit_Click" CssClass="btn btn-primary"
                                                        ValidationGroup="material" Width="70px" TabIndex="2" />
                                                    <asp:Button ID="btnreset" runat="server" Text="Reset" OnClick="btnreset_Click" CssClass="btn btn-warning"
                                                        Width="70px" TabIndex="3" />
                                                    <asp:Button ID="btnreport" runat="server" Text="Report" CssClass="btn btn-info"
                                                        Width="70px" OnClick="btnreport_Click1" TabIndex="4" />
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <div class="col-md-12">
                                                    <asp:Panel ID="fldmetertype" runat="server">
                                                        <asp:Repeater ID="Repeater_meterialtype" runat="server" OnItemCommand="Repeater_meterialtype_ItemCommand">
                                                            <HeaderTemplate>
                                                                <table class="table table-bordered table-hover">
                                                                    <thead>
                                                                        <tr class="bg-light-blue">
                                                                            <th>EDIT
                                                                            </th>
                                                                            <th>ASSET NAME
                                                                            </th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td>
                                                                        <asp:ImageButton ID="ImageButton_Edit" runat="server" CommandArgument='<%#Eval("MNO")%>' CommandName="edit" ImageUrl="../../IMAGES/edit.gif" ToolTip="Update this Material Type" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblmaterialtype" runat="server" Text=' <%#Eval("MNAME")%>' Style="text-align: left;"></asp:Label>
                                                                    </td>
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

