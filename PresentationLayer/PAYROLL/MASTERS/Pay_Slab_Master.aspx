<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Pay_Slab_Master.aspx.cs" Inherits="PAYROLL_MASTERS_Pay_Slab_Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:UpdatePanel ID="updpanel" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">SLAB CALCULATION MASTER</h3>
                            <p class="text-center">
                                <asp:Label ID="lblStatus" runat="server" SkinID="Errorlbl" />
                            </p>
                        </div>
                        <div>
                            <form role="form">
                                <div class="box-body">
                                     <div class="col-12">
                                        <asp:Panel ID="pnlSlabMaster" runat="server">
                                            <div class="panel panel-info">
                                                <div class="panel panel-heading">Add/Edit Slab Master</div>
                                                <div class="panel panel-body">
                                                     <div class="col-12">
                                                         <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <label>Slab Name <span style="color: #FF0000">*</span>:</label>
                                                            <asp:TextBox ID="txtSlabName" runat="server" Text="" CssClass="form-control" IsRequired="True" IsValidate="True"
                                                                TabIndex="1" ToolTip="Please Enter Slab Name" MaxLength="20">
                                                            </asp:TextBox>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftbSlabName" runat="server" TargetControlID="txtSlabName"
                                                                FilterType="Numbers,Custom" ValidChars="-">
                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                            <asp:RequiredFieldValidator ID="rfvSlabName" runat="server" ControlToValidate="txtSlabName"
                                                                Display="None" ErrorMessage="Please Enter Slab Name" ValidationGroup="payroll"
                                                                SetFocusOnError="True">
                                                            </asp:RequiredFieldValidator>
                                                        </div>

                                                          <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <label>From Slab <span style="color: #FF0000">*</span>: </label>
                                                            <asp:TextBox ID="txtFromSlab" runat="server" Text="" CssClass="form-control" IsRequired="True" IsValidate="True"
                                                                TabIndex="2" ToolTip="Please Enter From Slab" MaxLength="12">
                                                            </asp:TextBox>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftbeFromSlab" runat="server" TargetControlID="txtFromSlab"
                                                                FilterType="Numbers,Custom" ValidChars=".">
                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                            <asp:RangeValidator ID="rvFromSlab" runat="server" ControlToValidate="txtFromSlab" SetFocusOnError="True"
                                                                Display="None" ErrorMessage="Please Enter From Slab between 0 to 99999999.99" ValidationGroup="payroll"
                                                                MinimumValue="0" MaximumValue="99999999.99" Type="Double">
                                                            </asp:RangeValidator>
                                                            <asp:RequiredFieldValidator ID="rfvFromSlab" runat="server" ControlToValidate="txtFromSlab"
                                                                Display="None" ErrorMessage="Please Enter From Slab" ValidationGroup="payroll"
                                                                SetFocusOnError="True"> </asp:RequiredFieldValidator>
                                                        </div>

                                                         <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <label>To Slab <span style="color: #FF0000">*</span>:</label>
                                                            <asp:TextBox ID="txtToSlab" runat="server" Text="" CssClass="form-control" IsRequired="True" IsValidate="True"
                                                                TabIndex="3" ToolTip="Please Enter To Slab" MaxLength="12"> 
                                                            </asp:TextBox>
                                                            <asp:RangeValidator ID="rvToSlab" runat="server" ControlToValidate="txtToSlab" SetFocusOnError="True"
                                                                Display="None" ErrorMessage="Please Enter To Slab between 0 to 99999999.99" ValidationGroup="payroll"
                                                                MinimumValue="0" MaximumValue="99999999.99" Type="Double">
                                                            </asp:RangeValidator>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftbeToSlab" runat="server" TargetControlID="txtToSlab"
                                                                FilterType="Numbers,Custom" ValidChars=".">
                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                            <asp:RequiredFieldValidator ID="rfvToSlab" runat="server" ControlToValidate="txtToSlab"
                                                                Display="None" ErrorMessage="Please Enter To Slab" ValidationGroup="payroll"
                                                                SetFocusOnError="True"> </asp:RequiredFieldValidator>
                                                            <%--onpaste="return false"--%>
                                                        </div>

                                                          <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <label>Amount <span style="color: #FF0000">*</span>:</label>
                                                            <asp:TextBox ID="txtAmount" runat="server" Text="" CssClass="form-control" IsRequired="True" IsValidate="True"
                                                                TabIndex="3" ToolTip="Please Enter Amount" MaxLength="12" AutoComplete="off"> 
                                                            </asp:TextBox>
                                                            <asp:RangeValidator ID="rvAmount" runat="server" ControlToValidate="txtAmount" SetFocusOnError="True"
                                                                Display="None" ErrorMessage="Please Enter Amount between 0 to 99999999.99" ValidationGroup="payroll"
                                                                MinimumValue="0" MaximumValue="99999999.99" Type="Double">
                                                            </asp:RangeValidator>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftbeAmount" runat="server" TargetControlID="txtAmount"
                                                                FilterType="Numbers,Custom" ValidChars=".">
                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                            <asp:RequiredFieldValidator ID="rfvAmount" runat="server" ControlToValidate="txtAmount"
                                                                Display="None" ErrorMessage="Please Enter Amount" ValidationGroup="payroll"
                                                                SetFocusOnError="True"> </asp:RequiredFieldValidator>
                                                            <%--onpaste="return false"--%>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </form>
                        </div>

                        <div class="box-footer">
                            <p class="text-center">
                                <asp:Button ID="btnsubmit" runat="server" TabIndex="5" Text="Submit" OnClick="btnSubmit_Click" ValidationGroup="payroll"
                                    ToolTip="Submit" CssClass="btn btn-primary" />
                                <asp:Button ID="btncancel" runat="server" TabIndex="6" Text="Cancel" OnClick="btncancel_Click" CausesValidation="False"
                                    ToolTip="Cancel" CssClass="btn btn-warning" />
                                <asp:Button ID="btnPrint" runat="server" TabIndex="7" Text="Report" OnClick="btnPrint_Click" ValidationGroup="payroll"
                                    ToolTip="Report" CssClass="btn btn-info" Visible="false" />
                                <asp:ValidationSummary ID="ValidationSummary1" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="payroll" runat="server" />
                            </p>

                            <div class="col-md-12">
                                <asp:Panel ID="pnlList" runat="server" ScrollBars="Auto">
                                    <asp:ListView ID="lvSlab" runat="server">
                                        <LayoutTemplate>
                                            <div id="lgv1">
                                                <h4 class="box-title">Slab List</h4>
                                                <table class="table table-bordered table-hover">
                                                    <thead>
                                                        <tr class="bg-light-blue">
                                                            <th>Action
                                                            </th>
                                                            <%--<th>ID
                                                            </th>--%>
                                                            <th>Slab Name</th>
                                                            <th>From Slab
                                                            </th>
                                                            <th>To Slab
                                                            </th>
                                                            <th>Amount
                                                            </th>
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
                                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.png" CommandArgument='<%# Eval("PTSLABID") %>'
                                                        AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />
                                                </td>
                                                <%--<td>
                                                    <asp:Label ID="lblDeptno" runat="server" Text='<%# Eval("SUBDEPTNO") %>' />
                                                </td>--%>
                                                <td>
                                                    <asp:Label ID="lblSlabName" runat="server" Text='<%# Eval("SLAB_NAME") %>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblFromSlab" runat="server" Text='<%# Eval("FROM_SLAB") %>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblToSlab" runat="server" Text='<%# Eval("TO_SLAB") %>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("AMOUNT") %>' />
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>

                            <div class="vista-grid_datapager">
                                <div class="text-center">
                                    <asp:DataPager ID="dpPager" runat="server" OnPreRender="dpPager_PreRender" PagedControlID="lvSlab"
                                        PageSize="10">
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
            <div id="divMsg" runat="server"></div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

