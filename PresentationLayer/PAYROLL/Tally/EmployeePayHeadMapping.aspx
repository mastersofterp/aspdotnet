<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="EmployeePayHeadMapping.aspx.cs" Inherits="PAYROLL_Tally_EmployeePayHeadMapping" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script>
        $(document).ready(function () {

            bindDataTable();
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(bindDataTable);
        });

        function bindDataTable() {
            var myDT = $('#example').DataTable({

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
                            <h3 class="box-title">Employee Mapping</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Staff Type</label>
                                        </div>
                                        <asp:DropDownList ID="ddlStaff" runat="server" AppendDataBoundItems="True" ValidationGroup="Submit"
                                            ToolTip="Please Select Staff type" TabIndex="1" AutoPostBack="True" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlStaff_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvCourseCode" runat="server" ControlToValidate="ddlStaff"
                                            Display="None" ErrorMessage="Please Select Staff Type" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Submit" />
                                <asp:Button ID="btnSave" runat="server" Text="Save" UseSubmitBehavior="false" ValidationGroup="Submit" OnClick="btnSave_Click" CssClass="btn btn-primary progress-button" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-warning" />

                            </div>

                            <div class="col-12" id="DivFeeHeads" runat="server" visible="false">
                                <asp:ListView ID="repEmployeeHeads" runat="server" OnItemDataBound="repEmployeeHeads_ItemDataBound">
                                    <%----%>
                                    <LayoutTemplate>
                                        <div id="listViewGrid" class="vista-grid">
                                            <div class="sub-heading">
                                                <h5>Employee Mapping</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="example">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>SR. NO.</th>
                                                        <th>EMPLOYEE NAME</th>
                                                        <th>LEDGER NAME<span style="color: #FF0000; font-weight: bold">*</span></th>
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
                                                <%# Container.DataItemIndex + 1 %>
                                                <asp:HiddenField ID="hdnEmplyeeId" runat="server" Value='<%# Eval("IDNO") %>' />
                                            </td>
                                            <td>
                                                <strong><%# Eval("EMPNAME")%>
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


