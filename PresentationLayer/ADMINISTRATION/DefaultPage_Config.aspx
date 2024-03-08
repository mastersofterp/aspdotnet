<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="DefaultPage_Config.aspx.cs" Inherits="ADMINISTRATION_DefaultPage_Config" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row">
        <div class="col-md-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title"><b>Default Page Configuration</b></h3>
                </div>

                <div class="col-md-12">
                    <asp:ListView ID="lvConfig" runat="server">
                        <LayoutTemplate>
                            <div id="demo-grid" style="overflow-y: auto; max-height: 500px;">
                                <%--<h4 style="top: 0; background-color: #f8f9fa;">Default Page Configuration</h4>--%>
                                <table class="table table-hover table-bordered">
                                    <thead>
                                        <tr class="bg-light-blue">
                                            <th>Action</th>
                                            <th style="width: 100px">Sr No.</th>
                                            <th>Param Name</th>
                                            <th style="width: 240px">Param Value</th>
                                            <th>Param Description</th>
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
                                    <asp:CheckBox ID="chkparam" runat="server" OnCheckedChanged="chkparam_CheckedChanged" AutoPostBack="true" />
                                </td>
                                <td>
                                    <%#Container.DataItemIndex+1 %>
                                    <asp:HiddenField ID="hdnconfigid" runat="server" Value='<%# Eval("CONFIGID")%>' />
                                </td>
                                <td>
                                    <asp:Label ID="lblparamname" runat="server" Text='<%# Eval("PARAM_NAME")%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtparamvalue" runat="server" Text='<%# Eval("PARAM_VALUE")%>' Enabled="false" Width="240px" MaxLength="400"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="lblparamdescription" runat="server" Text='<%# Eval("PARAM_DESCRIPTION")%>'></asp:Label>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:ListView>
                    <div class="Row" style="text-align: center;">
                        <asp:Button ID="btnsubmit" runat="server" Text="Submit" CssClass="btn btn-outline-success" OnClick="btnsubmit_Click" />
                        <asp:Button ID="btnclear" runat="server" Text="Clear" CssClass="btn btn-outline-danger " OnClick="btnclear_Click" />
                    </div>
                </div>

            </div>
        </div>
    </div>
    <div id="popup" runat="server">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="modal" id="myModalPopUp" data-backdrop="static">
                    <div class="modal-dialog modal-md">
                        <div class="modal-content">
                            <div class="modal-body pl-0 pr-0 pl-lg-2 pr-lg-2">
                                <div class="col-12 mt-3">
                                    <h5 class="heading">Please enter password to access this page.</h5>
                                    <div class="row">
                                        <div class="form-group col-lg-12 col-md-12 col-12">
                                            <asp:Label ID="lblPass" runat="server" Text="ybc@123" Visible="false"></asp:Label>
                                            <asp:TextBox ID="txtPass" TextMode="Password" runat="server" TabIndex="1" ToolTip="Please Enter Password" AutoComplete="new-password"
                                                MaxLength="50" CssClass="form-control" />
                                            <asp:RequiredFieldValidator ID="req_password" runat="server" ErrorMessage="Password Required !" ControlToValidate="txtPass"
                                                Display="None" ValidationGroup="password"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-12 col-md-12 col-12">
                                        </div>
                                        <div class="btn form-group col-lg-12 col-md-12 col-12">
                                            <asp:Button ID="btnConnect" data-dismiss="myModalPopUp" data-keyboard="false" TabIndex="1" CssClass="btn btn-outline-primary"
                                                runat="server" Text="Submit" OnClick="btnConnect_Click" ValidationGroup="password" />
                                            <asp:Button ID="btnCancel1" data-dismiss="myModalPopUp" data-keyboard="false" TabIndex="2" CssClass="btn btn-danger"
                                                runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnConnect" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <asp:ValidationSummary ID="vsLogin" runat="server" ShowMessageBox="true" ShowSummary="false" DisplayMode="List" ValidationGroup="password" />
    <script type="text/javascript">
        $(window).on('load', function () {
            $('#myModalPopUp').modal('show');
        });
    </script>
</asp:Content>

