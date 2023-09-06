<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="tallyFeeHeads.aspx.cs" Inherits="Tally_tallyFeeHeads" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">



    <script>
        $(document).ready(function () {

            bindDataTable();
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(bindDataTable);
        });

        function bindDataTable() {
            var myDT = $('#tblSearchResults').DataTable({

            });

        }

    </script>
    <div>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="upDetails"
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
                            <h3 class="box-title">Pay Head Master</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>College</label>
                                        </div>
                                        <asp:DropDownList ID="ddlCollegeName" runat="server" AppendDataBoundItems="True" ValidationGroup="Submit"
                                            ToolTip="Please Select College" TabIndex="1" CssClass="form-control" data-select2-enable="true"> 
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlCollegeName" runat="server" ControlToValidate="ddlCollegeName"
                                            Display="None" ErrorMessage="Please Select College" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Staff Type </label>
                                        </div>
                                        <asp:DropDownList ID="ddlStaff" runat="server" AppendDataBoundItems="True" ValidationGroup="Submit" CssClass="form-control" data-select2-enable="true"
                                            ToolTip="Please Select Staff type" TabIndex="1" AutoPostBack="True" OnSelectedIndexChanged="ddlStaff_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvCourseCode" runat="server" ControlToValidate="ddlStaff"
                                            Display="None" ErrorMessage="Please Select Staff Type" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSave" runat="server" Text="Save" UseSubmitBehavior="false" ValidationGroup="Submit" OnClick="btnSave_Click" CssClass="btn btn-primary progress-button" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-warning" />
                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Submit" />

                            </div>

                            <div class="col-sm-12 mt-3 mb-4 " id="DivFeeHeads" runat="server" visible="false">
                                <asp:ListView ID="repFeeHeads" runat="server" OnItemDataBound="repFeeHeads_ItemDataBound">
                                    <LayoutTemplate>
                                        <div id="listViewGrid" class="vista-grid">
                                            <div class="sub-heading">
                                                <h5>Pay Heads</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="tblSearchResults">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>PAY HEAD</th>
                                                        <th>PAY HEAD NAME</th>
                                                        <th>LEDGER NAME<span style="color: #FF0000; font-weight: bold">*</span></th>
                                                        <%--<th style="width: 10%; text-align: left;">COST CATEGORY</th>
                                                                                                        <th style="width: 10%; text-align: left;">COST CENTER</th>--%>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                        </div>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr class="item">

                                            <td>
                                                <strong><%# Eval("PAYHEAD")%>
                                                    <asp:HiddenField ID="hdnPayHeadId" runat="server" Value='<%# Eval("PAYHEAD") %>' /></td>
                                            <td>
                                                <strong><%# Eval("FULLNAME")%></strong>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlCashLedgerName" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
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
        <Triggers>
        </Triggers>
    </asp:UpdatePanel>
    <div id="divMsg" runat="server" />
</asp:Content>

