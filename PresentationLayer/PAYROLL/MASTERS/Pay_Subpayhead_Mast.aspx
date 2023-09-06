<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Pay_Subpayhead_Mast.aspx.cs" Inherits="Masters_Pay_Subpayhead_Mast"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="updpnlMain" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">SUB PAY HEAD</h3>
                            <%-- <div class="box-tools pull-right">
                                <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                                    AlternateText="Page Help" ToolTip="Page Help" />
                            </div>--%>
                        </div>

                        <div class="box-body">
                            <asp:Label ID="lblStatus" runat="server" SkinID="Errorlbl" />
                            <div class="col-12">
                                <div class="row">
                                    <div class="col-12">
                                        <div class="sub-heading">
                                            <h5>Add/Edit Sub Pay Head</h5>
                                        </div>
                                    </div>

                                </div>
                            </div>
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Payhead</label>
                                        </div>
                                        <asp:DropDownList ID="ddlMainPayhead" runat="server" AppendDataBoundItems="true" CssClass="form-control" ToolTip="Select Quarter Type" TabIndex="1" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvMainPayhead" runat="server" InitialValue="0" ControlToValidate="ddlMainPayhead"
                                            Display="None" ErrorMessage="Please Select Main PayHead" ValidationGroup="payroll"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Sub pay head Full Name</label>
                                        </div>
                                        <asp:TextBox ID="txtFullName" runat="server" MaxLength="20" TabIndex="2" CssClass="form-control" ToolTip="Enter Quarter Name(only alphabets)" />
                                        <asp:RequiredFieldValidator ID="rvftxtFullName" runat="server" ControlToValidate="txtFullName"
                                            Display="None" ErrorMessage="Please Enter Sub pay head Full Name" ValidationGroup="payroll"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Sub Pay head Short Name</label>
                                        </div>
                                        <asp:TextBox ID="txtshortpayhead" runat="server" MaxLength="10" TabIndex="3" CssClass="form-control" ToolTip="Enter Quarter Name(only alphabets)" />
                                        <asp:RequiredFieldValidator ID="rfvshortpayhead" runat="server" ControlToValidate="txtshortpayhead"
                                            Display="None" ErrorMessage="Please Enter Short Name of Sub PayHead" ValidationGroup="payroll"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="Div3" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <label>Book Entry</label>
                                        </div>
                                        <div class="checkbox">
                                            <asp:CheckBox ID="chkBookAbj" runat="server" CssClass="legendPay" Text="If Checked 'Yes'" />
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="Div4" runat="server">
                                        <div class="label-dynamic">
                                            <label></label>
                                        </div>
                                        <asp:CheckBox ID="chkMainHead" runat="server" CssClass="legendPay" Text="Consider it as main head" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" CssClass="btn btn-primary" ValidationGroup="payroll" ToolTip="Submit" TabIndex="4" />
                                <asp:Button ID="btnShowReport" runat="server" Text="Report" CausesValidation="False" CssClass="btn btn-info" ToolTip="Show Report" TabIndex="5"  Visible="true" OnClick="btnShowReport_Click"/>
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-warning" CausesValidation="False" ToolTip="Cancel" TabIndex="6" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="payroll" DisplayMode="List" ShowSummary="false" ShowMessageBox="true" />
                            </div>
                            <div class="col-12">
                                <asp:Panel ID="pnlList" runat="server">
                                    <asp:ListView ID="lvSubPayhead" runat="server">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Sub Pay Head Details</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Action </th>
                                                        <th>Pay head </th>
                                                        <th>SPH Short Name </th>
                                                        <th>SPH Full Name </th>
                                                        <th>Include in main head </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr class="item">
                                                <td class="text-center">
                                                    <asp:ImageButton ID="btnEdit" runat="server" AlternateText="Edit Record" CommandArgument='<%# Eval("SUBHEADNO") %>' ImageUrl="~/Images/edit.png" OnClick="btnEdit_Click" ToolTip="Edit Record" />
                                                    <asp:Label ID="lblPayhead" runat="server" Text='<%# Eval("PAYCODE") %>' Visible="false" />
                                                    <asp:Label ID="lblSname" runat="server" Text='<%# Eval("SHORTNAME") %>' Visible="false" />
                                                    <asp:Label ID="lblFname" runat="server" Text='<%# Eval("FULLNAME") %>' Visible="false" />
                                                    <asp:Label ID="lblMainHead" runat="server" Text='<%# Eval("MAINHEADSTATUS") %>' Visible="false" />
                                                </td>
                                                <td><%# Eval("PAYHEAD")%></td>
                                                <td><%# Eval("SHORTNAME")%></td>
                                                <td><%# Eval("FULLNAME")%></td>
                                                <td><%# Eval("MAINHEADSTATUS")%></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                                <div class="vista-grid_datapager d-none">
                                    <div class="text-center">
                                        <asp:DataPager ID="dpPager" runat="server" OnPreRender="dpPager_PreRender" PagedControlID="lvSubPayhead" PageSize="100">
                                            <Fields>
                                                <asp:NextPreviousPagerField ButtonType="Link" FirstPageText="&lt;&lt;" PreviousPageText="&lt;" RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="true" ShowLastPageButton="false" ShowNextPageButton="false" ShowPreviousPageButton="true" />
                                                <asp:NumericPagerField ButtonCount="7" ButtonType="Link" CurrentPageLabelCssClass="current" />
                                                <asp:NextPreviousPagerField ButtonType="Link" LastPageText="&gt;&gt;" NextPageText="&gt;" RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="false" ShowLastPageButton="true" ShowNextPageButton="true" ShowPreviousPageButton="false" />
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
    <div id="divMsg" runat="server"></div>
</asp:Content>
