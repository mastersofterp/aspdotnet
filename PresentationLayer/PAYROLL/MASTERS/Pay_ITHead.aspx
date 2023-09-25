<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Pay_ITHead.aspx.cs" Inherits="Pay_ITHead" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updpanel"
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

    <asp:UpdatePanel ID="updpanel" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">IT HEADS</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="col-12">
                                        <div class="sub-heading">
                                            <h5>Income Tax Heads</h5>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <asp:Panel ID="Panel_Error" runat="server" CssClass="Panel_Error" EnableViewState="false"
                                Visible="false">
                                <table class="table table-bordered table-hover">
                                    <tr>
                                        <td style="width: 3%; vertical-align: top">
                                            <img src="../../../images/error.gif" align="absmiddle" alt="Error" />
                                        </td>
                                        <td style="width: 97%">
                                            <font style="font-family: Verdana; font-family: 11px; font-weight: bold; color: #CD0A0A">
                                                             </font>
                                            <asp:Label ID="Label_ErrorMessage" runat="server" Style="font-family: Verdana; font-size: 11px; color: #CD0A0A"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <asp:Panel ID="Panel_Confirm" runat="server" CssClass="Panel_Confirm" EnableViewState="false"
                                Visible="false">
                                <table class="table table-bordered table-hover">
                                    <tr>
                                        <td style="width: 3%; vertical-align: top">
                                            <img src="../../../images/confirm.gif" align="absmiddle" alt="confirm" />
                                        </td>
                                        <td style="width: 97%">
                                            <asp:Label ID="Label_ConfirmMessage" runat="server" Style="font-family: Verdana; font-size: 11px"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <asp:Panel ID="pnl" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                   <sup>* </sup>
                                                <label>Head Full Name</label>
                                            </div>
                                            <asp:TextBox ID="txtheadfullname" runat="server" Text="" CssClass="form-control" IsRequired="True" IsValidate="True" TabIndex="1" ToolTip="Please Enter Head Full Name">
                                            </asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvheadfullname" runat="server" ErrorMessage="Enter Head Full Name" ControlToValidate="txtheadfullname" Display="None" ValidationGroup="submit" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                         </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                   <sup>* </sup>
                                                <label>Head Name</label>
                                            </div>
                                            <asp:TextBox ID="txtHead" runat="server" Text="" CssClass="form-control" IsRequired="True" IsValidate="True" TabIndex="2" ToolTip="Please Enter Head Name">
                                            </asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvheadname" runat="server" ErrorMessage="Enter Head Name" ControlToValidate="txtHead" Display="None"  ValidationGroup="submit" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                   <sup>* </sup>
                                                <label>Section</label>
                                            </div>
                                            <asp:TextBox ID="txtSection" runat="server" Text="" CssClass="form-control" IsRequired="True" IsValidate="True" TabIndex="3" ToolTip="Please Enter Section">
                                            </asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvsectionname" runat="server" ErrorMessage="Enter Section Name" ControlToValidate="txtSection" Display="None" ValidationGroup="submit" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                 <%--  <sup>* </sup>--%>
                                                <label>PayHead</label>
                                            </div>
                                            <asp:TextBox ID="txtPayHead" runat="server" Text="" CssClass="form-control" IsRequired="True" IsValidate="True" TabIndex="4" ToolTip="Please Enter Section">
                                            </asp:TextBox>
                                           <%-- <asp:RequiredFieldValidator ID="rfvpayheadname" runat="server" ErrorMessage="Enter Pay Head Name" 
                                                ControlToValidate="txtPayHead" Display="None" ValidationGroup="submit"  SetFocusOnError="true"></asp:RequiredFieldValidator>
                                     --%>   </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                   <sup>* </sup>
                                                <label>Limit Amount/Perncentage</label>
                                            </div>
                                            <asp:TextBox ID="txtLimitAmt" runat="server" Text="" CssClass="form-control" IsRequired="True" IsValidate="True" TabIndex="5" ToolTip="Please Enter Limit">
                                            </asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvlimitamt" runat="server" ErrorMessage="Enter Limit /Amount Percentage" ControlToValidate="txtLimitAmt" 
                                                Display="None" ValidationGroup="submit" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Is Percentage</label>
                                            </div>
                                            <asp:CheckBox ID="chkIsPercentage" runat="server" />
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                            <div class="col-12 btn-footer">
                                <asp:ValidationSummary ID="ValidationSummary1" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="submit" runat="server" />
                                <asp:Button ID="btnsubmit" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnSubmit_Click"
                                    ValidationGroup="submit" TabIndex="6" ToolTip="Click To Save" />
                                <asp:Button ID="btnPrint" runat="server" Text="Print" TabIndex="7" ToolTip="Click To Print"
                                    OnClick="btnPrint_Click" CssClass="btn btn-info" />
                                <asp:Button ID="btncancel" runat="server" Text="Cancel"
                                    TabIndex="8" ToolTip="Click To Reset" CssClass="btn btn-warning" OnClick="btncancel_Click" />
                            </div>
                            <div class="col-12">
                                <asp:ListView ID="lvITHead" runat="server">
                                    <LayoutTemplate>
                                        <div class="sub-heading">
                                            <h5>IT HEADS</h5>
                                        </div>
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>Action
                                                    </th>
                                                    <th>HEAD FULL NAME
                                                    </th>
                                                    <th>HEAD NAME
                                                    </th>
                                                    <th>SECTION
                                                    </th>
                                                    <th>PAY HEAD
                                                    </th>
                                                    <th>LIMIT
                                                    </th>
                                                    <th>PERCENTAGE
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
                                            <td class="text-center">
                                                <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("CNO") %>'
                                                    AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />
                                                <asp:Label ID="lblCNO" runat="server" Text='<%# Eval("CNO") %>' Visible="false" />

                                            </td>
                                            <td>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("HEADNAMEFULL") %>' />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblHead" runat="server" Text='<%# Eval("HEADNAME") %>' />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblSection" runat="server" Text='<%# Eval("SEC") %>' />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblPayHead" runat="server" Text='<%# Eval("PAYHEAD") %>' />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblLimit" runat="server" Text='<%# Eval("LIMIT") %>' />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblIsPercentage" runat="server" Text='<%# Eval("ISPERCENTAGE") %>' />
                                            </td>
                                        </tr>
                                    </ItemTemplate>

                                </asp:ListView>

                                <div class="vista-grid_datapager d-none">
                                    <asp:DataPager ID="dpPager" runat="server" PagedControlID="lvITHead" OnPreRender="dpPager_PreRender"
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
                            <div id="divMsg" runat="server"></div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

