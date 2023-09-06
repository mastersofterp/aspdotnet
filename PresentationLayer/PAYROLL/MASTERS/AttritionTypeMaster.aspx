<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="AttritionTypeMaster.aspx.cs" Inherits="PAYROLL_MASTERS_AttritionTypeMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <asp:UpdatePanel ID="updpanel" runat="server">
       <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">ATTRITION TYPE MASTER </h3>
                        </div>
                        <div class="box-body">
                            <asp:Label ID="lblStatus" runat="server" SkinID="Errorlbl" />
                            <asp:Panel ID="pnlPfMaster" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-12">
                                           <%-- <div class="sub-heading">
                                                <h5>Add/Edit Department Management</h5>
                                            </div>--%>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12">
                                    <div class="row">
                                  <%-- <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>College Name</label>
                                        </div>
                                   <asp:DropDownList ID="ddlCollege" runat="server" ToolTip="Select College" CssClass="form-control" AppendDataBoundItems="true" data-select2-enable="true"
                                        TabIndex="2">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlCollege"
                                        ValidationGroup="submit" ErrorMessage="Please select College" SetFocusOnError="true"
                                        InitialValue="0" Display="None"></asp:RequiredFieldValidator>
                                    </div>--%>
                                         <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Attrition Name</label>
                                            </div>
                                            <asp:TextBox ID="txtAttritionName" runat="server" Text="" CssClass="form-control" IsRequired="True" IsValidate="True"
                                                TabIndex="1" ToolTip="Please Enter Attrition Name" onkeypress="return lettersOnly(event)">
                                            </asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtAttritionName" runat="server" ErrorMessage="Enter Attrition Name" 
                                                ValidationGroup="submit" Display="None" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        </div>
                                           <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>IsActive</label>
                                            </div>
                                           <asp:CheckBox  ID="chkisActive" runat="server" Checked="true"/>
                                        </div>
                                   </div>
                                </div>
                            </asp:Panel>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnsubmit" runat="server" TabIndex="3" Text="Submit" ValidationGroup="submit"  OnClick="btnsubmit_Click"
                                    ToolTip="Submit" CssClass="btn btn-primary" />
                                 <asp:Button ID="btnPrint" runat="server" TabIndex="4" Text="Report"  ValidationGroup="submit"  OnClick="btnPrint_Click"
                                    ToolTip="Report" CssClass="btn btn-info" Visible="false" />
                                <asp:Button ID="btncancel" runat="server" TabIndex="5" Text="Cancel"  CausesValidation="False"  OnClick="btncancel_Click"
                                    ToolTip="Cancel" CssClass="btn btn-warning" />  
                                <asp:ValidationSummary ID="ValidationSummary1" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="submit" runat="server" />
                            </div>
                            <div class="col-12">
                                <asp:Panel ID="pnlList" runat="server">
                                    <asp:ListView ID="lvAttritionType" runat="server">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Attrition Type Master List</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Action
                                                        </th>
                                                        <th>
                                                            Attrition Name
                                                        </th>
                                                        <th>
                                                            IsActive
                                                        </th>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td class="text-left">
                                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("AttritionTypeNo") %>'
                                                        AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click"/>
                                                </td>
                                                <td>
                                                <asp:Label ID="lblAttritionName" runat="server" Text='<%# Eval("AttritionName") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblisactive" runat="server" Text='<%# Eval("IsActive") %>'></asp:Label>
                                                </td>
                                                <%--<td>
                                                    <asp:Label ID="lblDeptKannada" runat="server" Text='<%# Eval("SUBDEPT_KANNADA") %>' />
                                                </td>--%>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                                <div class="vista-grid_datapager d-none">
                                    <div class="text-center">
                                        <asp:DataPager ID="DataPager1" runat="server"   OnPreRender="DataPager1_PreRender"  PagedControlID="lvAttritionType"
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
            <div id="divMsg" runat="server"></div>
        </ContentTemplate>
  </asp:UpdatePanel>
</asp:Content>
