<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/SiteMasterPage.master" CodeFile="LeaveCombinationAllow.aspx.cs" Inherits="ESTABLISHMENT_LEAVES_Master_LeaveCombinationAllow" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box box-primary">
                            <div class="box-header with-border">
                                <h3 class="box-title">Leave Combination Allow Or Not</h3>
                            </div>
                            <div class="box-body">
                                <asp:Panel ID="pnl" runat="server">
                                    <div class="col-12">
                                        <div class="row">
                                            <div class="col-12">
                                                <div class="sub-heading">
                                                    <h5>Add/Edit Leave Combination</h5>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-12">
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Select Leave Name</label>
                                                </div>
                                                <asp:DropDownList ID="ddlleaveshrtname" runat="server" AutoPostBack="true" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" ToolTip="Select Leave Name"
                                                    OnSelectedIndexChanged="ddlleaveshrtname_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlleaveshrtname"
                                                    Display="None" ErrorMessage="Please Select Leave Name" ValidationGroup="show"
                                                    SetFocusOnError="True" InitialValue="0">
                                                </asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnsubmit" runat="server" Text="Submit" ValidationGroup="show" OnClick="btnsubmit_Click" CssClass="btn btn-primary" ToolTip="Click here to Save or Update" />
                                    <asp:Button ID="btncancel" runat="server" Text="Cancel" CausesValidation="false" OnClick="btncancel_Click" CssClass="btn btn-warning" ToolTip="Click here to Reset" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                        ShowMessageBox="True" ShowSummary="False" ValidationGroup="show" />
                                </div>

                                <asp:Panel ID="pnllist" runat="server" Visible="false">
                                    <div class="col-12">
                                        <div class="sub-heading">
                                            <h5>Leave Combination Allow Or Not List</h5>
                                        </div>
                                        <table>
                                            <tr style="width: 50%">
                                                <td colspan="2" class="form_left_text" style="padding: 5px; vertical-align: top">
                                                    <asp:Panel ID="pnlListMain" runat="server" Visible="false">
                                                        <asp:ListView ID="lvlist" runat="server">
                                                            <%--<EmptyDataTemplate>
                                                <p class="text-center">
                                                    <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Records Found" />
                                                </p>
                                            </EmptyDataTemplate>--%>
                                                            <LayoutTemplate>
                                                                <div class="sub-heading">
                                                                    <h5>Leave Prefix Allowed</h5>
                                                                </div>
                                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                                    <thead class="bg-light-blue">
                                                                        <tr>
                                                                            <th>
                                                                                <asp:CheckBoxList ID="chklist" RepeatDirection="Horizontal" runat="server" Width="100px"
                                                                                    AppendDataBoundItems="true" ToolTip="Please Select" />
                                                                            </th>
                                                                            <th>Leave Name
                                                                            </th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                        <tr id="itemPlaceholder" runat="server" />
                                                                    </tbody>
                                                                </table>
                                                            </LayoutTemplate>
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td>
                                                                         <asp:CheckBox ID="chkAccept" runat="server" Checked="false" ToolTip='<%# Eval("LVNO")%>' />
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("Leave_Name") %>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                    </asp:Panel>
                                                </td>
                                                <td colspan="2" class="form_left_text" style="padding: 5px; vertical-align: top">
                                                    <asp:Panel ID="pnllistprefix" runat="server" Visible="false">
                                                        <asp:ListView ID="lvlleavepre" runat="server">
                                                            <LayoutTemplate>
                                                                <div class="sub-heading">
                                                                    <h5>Leave Suffix Allowed</h5>
                                                                </div>
                                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                                    <thead class="bg-light-blue">
                                                                        <tr>
                                                                            <th>
                                                                                <asp:CheckBoxList ID="chklistsuffix" RepeatDirection="Horizontal" runat="server"
                                                                                    AppendDataBoundItems="true" Width="100px" ToolTip="Please Select">
                                                                                </asp:CheckBoxList>
                                                                            </th>
                                                                            <th>Leave Name
                                                                            </th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                        <tr id="itemPlaceholder" runat="server" />
                                                                    </tbody>
                                                                </table>
                                                            </LayoutTemplate>
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td>
                                                                        <asp:CheckBox ID="chkacceptsuffix" runat="server" Checked="false" ToolTip='<%# Eval("LVNO")%>' />
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("Leave_Name") %>
                                                                    </td>
                                                                </tr>

                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        

</asp:Content>
