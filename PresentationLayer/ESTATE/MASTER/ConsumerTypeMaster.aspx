<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ConsumerTypeMaster.aspx.cs" Inherits="ESTATE_Master_ConsumerTypeMaster" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="updConsumerTypeMaster" runat="server">
        <ContentTemplate>

            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">RESIDENT TYPE MASTER</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-md-12">
                                <asp:Panel ID="pnlAdd" runat="server">
                                    <div class="panel panel-info">
                                        <div class="panel-heading">
                                            Add/Edit Resident Type Master 
                                        </div>
                                        <div class="panel-body">
                                            <div class="form-group row">
                                                <div class="col-md-12">
                                                    Note <b>:</b> <span style="color: #FF0000">* Marked Is Mandatory !</span>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <div class="col-md-2">
                                                    <label>Resident Type Master<span style="color: red;">*</span>:</label>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txtconsumertype" runat="server" CssClass="form-control" TabIndex="1" MaxLength="40"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvconsumer" runat="server" ControlToValidate="txtconsumertype"
                                                        ErrorMessage="Residents Type Name Required." ValidationGroup="consumer"></asp:RequiredFieldValidator>
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="ftbeType" runat="server" TargetControlID="txtconsumertype" FilterType="Custom, LowercaseLetters, UppercaseLetters"
                                                        ValidChars=" ">
                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                    <asp:ValidationSummary ID="valsumEng" runat="server"
                                                        DisplayMode="List" ValidationGroup="consumer" ShowMessageBox="true" ShowSummary="false" />
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <div class="col-md-2">
                                                </div>
                                                <div class="col-md-10">
                                                    <asp:Button ID="btnsubmit" runat="server" Text="Submit" OnClick="btnsubmit_Click"
                                                        ValidationGroup="consumer" CssClass="btn btn-primary" TabIndex="2" />
                                                    <asp:Button ID="btnreset" runat="server" Text="Reset" OnClick="btnreset_Click"
                                                        CssClass="btn btn-warning" TabIndex="3" />
                                                    <%-- <asp:Button ID="btnreport"  runat="server" Text="Report"  
                                                            Width="70px" onclick="btnreport_Click"  />
                                                    --%>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <div class="col-md-12">
                                                    <asp:Panel runat="server" ID="fldconsumertype">
                                                        <asp:Repeater ID="Repeater_consumertype" runat="server" OnItemCommand="Repeater_consumertype_ItemCommand">
                                                            <HeaderTemplate>
                                                                <table class="table table-bordered table-hover">
                                                                    <thead>
                                                                        <tr class="bg-light-blue">
                                                                            <th>EDIT
                                                                            </th>
                                                                            <th>CONSUMER TYPE
                                                                            </th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                            </HeaderTemplate>

                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td>
                                                                        <asp:ImageButton ID="ImageButton_Edit" runat="server" CommandArgument='<%#Eval("IDNO")%>' CommandName="edit" ImageUrl="../../IMAGES/edit.gif" ToolTip="Update this Material Type" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblconsumertype" runat="server" Text=' <%#Eval("CONSUMERTYPE_NAME")%>' Style="text-align: left;"></asp:Label>
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

