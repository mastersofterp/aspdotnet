<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="tallyFeeHeads.aspx.cs" Inherits="Tally_tallyFeeHeads" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="upDetails"
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

    <asp:UpdatePanel ID="upDetails" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">FeeHeads Configuration</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>
                                                Receipt Type
                                            </label>
                                        </div>
                                        <asp:DropDownList ID="ddlCashBook" runat="server" AppendDataBoundItems="True" ValidationGroup="Submit" CssClass="form-control" data-select2-enable="true"
                                            ToolTip="Please Select Cash Book" TabIndex="1" AutoPostBack="True" OnSelectedIndexChanged="ddlCashBook_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvCourseCode" runat="server" ControlToValidate="ddlCashBook"
                                            Display="None" ErrorMessage="Please Select Reciept Type" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="Submit"></asp:RequiredFieldValidator>

                                    </div>

                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="btnSave" runat="server" Text="Save" TabIndex="2" UseSubmitBehavior="false" ValidationGroup="Submit" OnClick="btnSave_Click" CssClass="btn btn-primary" />
                                        <asp:Button ID="btnCancel" runat="server" TabIndex="3" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-warning" />

                                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Submit" />
                                    </div>
                                </div>
                            </div>

                            <div class="col-12">
                                <asp:Panel ID="DivFeeHeads" runat="server" Visible="false">
                                    <asp:ListView ID="repFeeHeads" runat="server" OnItemDataBound="repFeeHeads_ItemDataBound">
                                        <LayoutTemplate>
                                            <div id="listViewGrid">
                                                <div class="sub-heading">
                                                    <h5>Fee Heads</h5>
                                                </div>

                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="tblSearchResults">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>SR.NO.
                                                            </th>
                                                            <th>HEAD NAME
                                                            </th>

                                                            <th>SHORT NAME
                                                            </th>

                                                            <th>CASH LEDGER 
                                                            </th>

                                                            <th>BANK LEDGER
                                                            </th>
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
                                                    <%# Container.DataItemIndex + 1 %>
                                                    <asp:HiddenField ID="hdnFeeHeadId" runat="server" Value='<%# Eval("FEE_HEAD") %>' />
                                                </td>
                                                <td>
                                                    <strong><%# Eval("FEE_LONGNAME")%></strong>
                                                </td>

                                                <td>
                                                    <strong><%# Eval("FEE_SHORTNAME")%></strong>

                                                </td>

                                                <td>
                                                    <asp:DropDownList ID="ddlCashLedgerName" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                    </asp:DropDownList>

                                                </td>

                                                <td>
                                                    <asp:DropDownList ID="ddlBankLedgerName" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                    </asp:DropDownList>

                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
        <Triggers>
        </Triggers>
    </asp:UpdatePanel>

    <div id="divMsg" runat="server" />

</asp:Content>

