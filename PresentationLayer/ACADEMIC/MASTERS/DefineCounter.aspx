<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="DefineCounter.aspx.cs" Inherits="DefineCounter" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .checkbox-list-style input[type=checkbox]
        {
            vertical-align: middle;
            margin-right: 5px;
        }
    </style>
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updcounter"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div id="preloader">
                    <div id="loader-img">
                        <div id="loader">
                        </div>
                        <p class="saving">Loading<span>.</span><span>.</span><span>.</span></p>
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <asp:UpdatePanel ID="updcounter" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <%--<h3 class="box-title">DEFINE COUNTER a</h3>--%>
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="sub-heading">
                                    <h5>Add/Edit Counter</h5>
                                </div>
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Counter Name:</label>
                                        </div>
                                        <asp:TextBox ID="txtCounterName" runat="server" />
                                        <asp:RequiredFieldValidator ID="valCounterName" runat="server" ControlToValidate="txtCounterName" TabIndex="1"
                                            ValidationGroup="Submit" Display="None" ErrorMessage="Please enter counter name." />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Short Print Name:</label>
                                        </div>
                                        <asp:TextBox ID="txtPrintName" runat="server" TabIndex="1" />
                                        <asp:RequiredFieldValidator ID="valPrintName" runat="server" ControlToValidate="txtPrintName"
                                            ValidationGroup="Submit" Display="None" ErrorMessage="Please enter print name." />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Counter User:</label>
                                        </div>
                                        <asp:DropDownList ID="ddlCounterUser" runat="server" AppendDataBoundItems="true" TabIndex="1" data-select2-enable="true" />
                                        <asp:RequiredFieldValidator ID="valCounterUser" runat="server" ControlToValidate="ddlCounterUser"
                                            ValidationGroup="Submit" Display="None" ErrorMessage="Please enter counter user."
                                            InitialValue="0" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Permission for Receipts:</label>
                                        </div>

                                        <div class="form-group col-md-12 checkbox-list-box">
                                            <asp:CheckBoxList ID="chkListReceiptTypes" CssClass="checkbox-list-style" runat="server" TabIndex="1" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" CssClass="btn btn-primary" TabIndex="1" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-warning" TabIndex="1" />
                                <asp:ValidationSummary ID="valSummary" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="Submit" />
                            </div>
                            <div class="col-12">
                                <div class="sub-heading">
                                    <h5>List of Counters</h5>
                                </div>
                                <asp:ListView ID="lvCounters" runat="server">
                                    <LayoutTemplate>
                                        <table id="tblHead" class="table table-bordered table-hover display" style="width: 100%;">
                                            <thead class="bg-light-blue">
                                                <tr id="trRow">
                                                    <th>Edit
                                                    </th>
                                                    <th>Counter Name
                                                    </th>
                                                    <th>Print Name
                                                    </th>
                                                    <th>Counter User
                                                    </th>
                                                    <th>Permission for Receipts
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr id="itemPlaceholder" runat="server" />
                                            </tbody>
                                        </table>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr class="item">
                                            <td>
                                                <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" ImageUrl="~/Images/edit.png"
                                                    CommandArgument='<%# Eval("COUNTERNO") %>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                    OnClick="btnEdit_Click" TabIndex="10" />
                                            </td>
                                            <td>
                                                <%# ((Eval("COUNTERNAME").ToString() != string.Empty)? Eval("COUNTERNAME") : "--") %>
                                            </td>
                                            <td>
                                                <%# ((Eval("PRINTNAME").ToString() != string.Empty) ? Eval("PRINTNAME") : "--") %>
                                            </td>
                                            <td>
                                                <%# ((Eval("UA_FULLNAME").ToString() != string.Empty) ? Eval("UA_FULLNAME") : "--") %>
                                            </td>
                                            <td>
                                                <%# ((Eval("RECEIPT_PERMISSION").ToString() != string.Empty) ? Eval("RECEIPT_PERMISSION") : "--")%>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="divMsg" runat="server">
    </div>
</asp:Content>
