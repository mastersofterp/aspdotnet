<%--<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="qualificationMas.aspx.cs" Inherits="Masters_qualificationMas" Title="" %>
<%@ Register Src="~/PayRoll/Masters/qualificationMas.ascx" TagName="qualification" TagPrefix="uc1" %>
--%>

<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="qualificationMas.aspx.cs" Inherits="Masters_qualificationMas" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">QUALIFICATION MANAGEMENT</h3>
                        </div>

                        <div class="box-body">
                            <asp:Label ID="lblStatus" runat="server" SkinID="Errorlbl" />
                            <asp:Panel ID="pnlQualification" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="sub-heading">
                                                <h5>Add/Edit Qualification</h5>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Qualification Type</label>
                                            </div>
                                            <asp:DropDownList ID="ddlQType" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                                ToolTip="Select Qualification Type" TabIndex="1">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvQType" runat="server" InitialValue="0" ControlToValidate="ddlQType"
                                                Display="None" ErrorMessage="Please Select Qualification Type" ValidationGroup="qual"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Qualification</label>
                                            </div>
                                            <asp:TextBox ID="txtQualification" runat="server" MaxLength="20" TabIndex="2" CssClass="form-control"
                                                ToolTip="Enter Qualification(only alphabets)" />
                                            <asp:RequiredFieldValidator ID="rfvQualification" runat="server" ControlToValidate="txtQualification"
                                                Display="None" ErrorMessage="Please Enter Qualification" ValidationGroup="qual"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click"
                                    ValidationGroup="qual" ToolTip="Click here to Submit" TabIndex="3" CssClass="btn btn-primary" />
                                <asp:Button ID="btnShowReport" runat="server" Text="Report" CausesValidation="False"
                                    ToolTip="Click here to Show Report" TabIndex="4" CssClass="btn btn-info" OnClick="btnShowReport_Click" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                                    CausesValidation="False" ToolTip="Click here to Cancel" TabIndex="5" CssClass="btn btn-warning" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="qual"
                                    DisplayMode="List" ShowSummary="false" ShowMessageBox="true" />
                            </div>
                            <div class="col-12">
                                <asp:Panel ID="pnlList" runat="server">
                                    <asp:ListView ID="lvQualification" runat="server">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
	                                                <h5>Qualification List</h5>
                                                </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width:100%">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>Action</th>
                                                            <th>Qualification</th>
                                                            <th>Qualification Type</th>
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
                                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("qualino") %>'
                                                        AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />
                                                    <asp:Label ID="lblQLevelNo" runat="server" Text='<%# Eval("qualilevelno") %>' Visible="false" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblQuali" runat="server" Text='<%# Eval("quali") %>' />
                                                </td>
                                                <td>
                                                    <%# Eval("qualilevelname") %>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>
                            <div class="vista-grid_datapager d-none">
                                <div class="text-center">
                                    <asp:DataPager ID="dpPager" runat="server" OnPreRender="dpPager_PreRender" PagedControlID="lvQualification"
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
