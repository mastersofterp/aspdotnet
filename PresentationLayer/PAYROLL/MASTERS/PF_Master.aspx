<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="PF_Master.aspx.cs" Inherits="PAYROLL_MASTERS_PF_Master" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="updMain" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">PF MASTER</h3>
                        </div>

                        <div class="box-body">
                            <asp:Label ID="lblStatus" runat="server" SkinID="Errorlbl" />

                            <div class="col-12">
                                <%--<legend class="legendPay">Add/Edit PF Master</legend>--%>
                                <asp:Panel ID="pnlPfMaster" runat="server">
                                    <div class="sub-heading">
                                        <h5>Add/Edit PF Master</h5>
                                    </div>
                                    <div class="col-12">
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Full Name</label>
                                                </div>
                                                <asp:TextBox ID="txtFullName" runat="server" MaxLength="100" ToolTip="Enter Full Name" CssClass="form-control" TabIndex="1" />
                                                <asp:RequiredFieldValidator ID="rfvtxtFullName" runat="server" ControlToValidate="txtFullName"
                                                    Display="None" ErrorMessage="Please Enter Full Name" ValidationGroup="payroll"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Short Name</label>
                                                </div>
                                                <asp:TextBox ID="txtShortName" runat="server" ToolTip="Enter Short Name" CssClass="form-control" MaxLength="20" TabIndex="2" />
                                                <asp:RequiredFieldValidator ID="rfvtxtShortName" runat="server" ControlToValidate="txtShortName"
                                                    Display="None" ErrorMessage="Please Enter Short Name" ValidationGroup="payroll"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Subscription Head</label>
                                                </div>
                                                <asp:DropDownList ID="ddlSubscriptionHead" ToolTip="Select Subscription Head" runat="server" AutoPostBack="true" CssClass="form-control" AppendDataBoundItems="true"
                                                    TabIndex="3" OnSelectedIndexChanged="ddlSubscriptionHead_SelectedIndexChanged" data-select2-enable="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvddlSubscriptionHead" runat="server" InitialValue="0"
                                                    ControlToValidate="ddlSubscriptionHead" Display="None" ErrorMessage="Please Select Subscription Head"
                                                    ValidationGroup="payroll"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Add Subscription Head</label>
                                                </div>
                                                <asp:DropDownList ID="ddlAddSubscriptionHead" runat="server" AutoPostBack="true" CssClass="form-control" AppendDataBoundItems="true" data-select2-enable="true"
                                                    ToolTip="Select Add Subscription Head" TabIndex="4"
                                                    OnSelectedIndexChanged="ddlAddSubscriptionHead_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvAddSubscriptionHead" runat="server" InitialValue="0"
                                                    ControlToValidate="ddlAddSubscriptionHead" Display="None" ErrorMessage="Please Select Add Subscription Head"
                                                    ValidationGroup="payroll"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Repayment Head</label>
                                                </div>
                                                <asp:DropDownList ID="ddlRepaymentHead" runat="server" CssClass="form-control" AppendDataBoundItems="true" data-select2-enable="true"
                                                    ToolTip="Select Repayment Head " TabIndex="5">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvRepaymentHead" runat="server" InitialValue="0"
                                                    ControlToValidate="ddlRepaymentHead" Display="None" ErrorMessage="Please Select Repayment Head "
                                                    ValidationGroup="payroll"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSubmit" runat="server" TabIndex="6" Text="Submit" OnClick="btnSubmit_Click" ValidationGroup="payroll" ToolTip="Submit" CssClass="btn btn-primary" />
                                <asp:Button ID="btnCancel" runat="server" TabIndex="7" Text="Cancel" OnClick="btnCancel_Click" CausesValidation="False" ToolTip="Cancel" CssClass="btn btn-warning" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="payroll" DisplayMode="List" ShowSummary="false" ShowMessageBox="true" />
                            </div>
                            <div class="col-12">
                            <asp:Panel ID="pnlList" runat="server">                                
                                <asp:ListView ID="lvPFmaster" runat="server">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>PF MASTER</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Action</th>
                                                        <th>Full Name</th>
                                                        <th>Short Name</th>
                                                        <th>Subscription Head</th>
                                                        <th>Add Subscription Head </th>
                                                        <th>Repayment Head</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td class="text-center">
                                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("PFNO") %>'
                                                        AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />
                                                </td>
                                                <td>
                                                    <%# Eval("FULLNAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("SHORTNAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("H1")%>
                                                </td>
                                                <td>
                                                    <%# Eval("H2")%>
                                                </td>
                                                <td>
                                                    <%# Eval("H3")%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>                               
                            </asp:Panel>
                                </div>
                            <div class="vista-grid_datapager d-none">
                                <div class="text-center">
                                    <asp:DataPager ID="dpPager" runat="server" OnPreRender="dpPager_PreRender" PagedControlID="lvPFmaster"
                                        PageSize="100">
                                        <Fields>
                                            <asp:NextPreviousPagerField FirstPageText="<<" PreviousPageText="<" ButtonType="Link"
                                                RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="true" ShowPreviousPageButton="true"
                                                ShowLastPageButton="false" ShowNextPageButton="false" />
                                            <asp:NumericPagerField ButtonType="Link" ButtonCount="7" CurrentPageLabelCssClass="current" />
                                            <asp:NextPreviousPagerField LastPageText=">>" NextPageText=">" ButtonType="Link"
                                                RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="false" ShowPreviousPageButton="false"
                                                ShowLastPageButton="true" ShowNextPageButton="true" />
                                        </Fields>
                                    </asp:DataPager>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
