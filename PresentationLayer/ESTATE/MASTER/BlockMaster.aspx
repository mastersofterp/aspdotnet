<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="BlockMaster.aspx.cs" Inherits="ESTATE_Master_BlockMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="updBlockmaster" runat="server">
        <ContentTemplate>

            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">BLOCK MASTER</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-md-12">
                                <asp:Panel ID="fldBlock" runat="server">
                                    <div class="panel panel-info">
                                        <div class="panel-heading">
                                            Add/Edit Block Master
                                        </div>
                                        <div class="panel-body">
                                            <div class="form-group row">
                                                <div class="col-md-12">
                                                    Note <b>:</b> <span style="color: #FF0000">* Marked Is Mandatory !</span>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <div class="col-md-2">
                                                    <label>Block Name<span style="color: red;">*</span>:</label>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txtBlock" runat="server" CssClass="form-control" TabIndex="1" MaxLength="30"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvBlock" runat="server" ControlToValidate="txtBlock"
                                                        ErrorMessage="Please Enter Block Name." ValidationGroup="block" Display="None"></asp:RequiredFieldValidator>
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="ftbeType" runat="server" TargetControlID="txtBlock" FilterType="Custom, LowercaseLetters,UppercaseLetters"
                                                        ValidChars=" ">
                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <div class="col-md-2">
                                                </div>
                                                <div class="col-md-10">
                                                    <asp:Button ID="btnsubmit" runat="server" Text="Submit" OnClick="btnsubmit_Click" ValidationGroup="material" CssClass="btn btn-primary" TabIndex="2" />
                                                    <asp:Button ID="btnreset" runat="server" Text="Reset" OnClick="btnreset_Click" CssClass="btn btn-warning" TabIndex="3" />
                                                    <asp:Button ID="btnreport" runat="server" Text="Report" CssClass="btn btn-info" OnClick="btnreport_Click1" TabIndex="4" />
                                                    <asp:ValidationSummary ID="valsumEng" runat="server" DisplayMode="List" ValidationGroup="block" ShowMessageBox="true" ShowSummary="false" />
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <div class="col-md-12">
                                                    <asp:Panel runat="server" ID="fldmetertype">
                                                        <div class="table-responsive">
                                                            <asp:Repeater ID="Repeater_block" runat="server" OnItemCommand="Repeater_block_ItemCommand">
                                                                <HeaderTemplate>
                                                                    <table class="table table-bordered table-hover">
                                                                        <thead>
                                                                            <tr class="bg-light-blue">
                                                                                <th>EDIT
                                                                                </th>
                                                                                <th>BLOCK NAME
                                                                                </th>
                                                                            </tr>
                                                                        </thead>
                                                                        <tbody>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:ImageButton ID="ImageButton_Edit" runat="server" CommandArgument='<%#Eval("BLOCKID")%>' CommandName="edit" ImageUrl="../../IMAGES/edit.gif" ToolTip="Update block name." />
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblmaterialtype" runat="server" Text=' <%#Eval("BLOCKNAME")%>' Style="text-align: left;"></asp:Label>
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

