<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="complaint_itemorder.aspx.cs"
    Inherits="Estate_complaint_itemorder" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../../JAVASCRIPTS/JScriptAdmin_Module.js" type="text/javascript"></script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">ITEM ORDER</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-md-12">
                                <asp:Panel ID="pnl" runat="server">
                                    <div class="panel panel-info">
                                        <div class="panel-heading">
                                            Add/Edit Item Order
                                        </div>
                                        <div class="panel-body">
                                            <div class="form-group row">
                                                <div class="col-md-12">
                                                    Note <b>:</b> <span style="color: #FF0000">* Marked Is Mandatory !</span>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <div class="col-md-2">
                                                    <label>Department Name:</label>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:DropDownList ID="ddlDepartmentName" Enabled="false" runat="server" CssClass="form-control" AppendDataBoundItems="true" TabIndex="1">
                                                        <asp:ListItem Value="-1">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                                        ControlToValidate="ddlDepartmentName" Display="None" ErrorMessage="Department Name Required"
                                                        ValidationGroup="Complaint" InitialValue="-1"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <div class="col-md-2">
                                                    <label>Item Name <span style="color: red;">*</span>:</label>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:DropDownList ID="ddlItemName" runat="server" CssClass="form-control" AppendDataBoundItems="true" TabIndex="1">
                                                        <asp:ListItem Value="-1">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlItemName" Display="None" ErrorMessage="Please Select Item Name."
                                                        ValidationGroup="CItem" InitialValue="-1"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <div class="col-md-2">
                                                    <label>Quantity Order <span style="color: red;">*</span>:</label>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txtQtyOrder" runat="server" CssClass="form-control" TabIndex="8" MaxLength="10" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtQtyOrder" Display="None" ErrorMessage="Please Enter Quantity of Item." ValidationGroup="CItem"></asp:RequiredFieldValidator>
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtQtyOrder" FilterType="Numbers">
                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <div class="col-md-2">
                                                    <label>Order Date<span style="color: red;">*</span>:</label>
                                                </div>
                                                <div class="col-md-2">
                                                    <div class="input-group date">
                                                        <div class="input-group-addon">
                                                            <asp:Image ID="Image1" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                        </div>
                                                        <asp:TextBox ID="txtOrderDate" runat="server" CssClass="form-control" TabIndex="9" />
                                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtOrderDate" PopupButtonID="Image1"></ajaxToolKit:CalendarExtender>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtOrderDate" Display="None" ErrorMessage="Please Select Order Date." ValidationGroup="CItem"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <div class="col-md-2">
                                                </div>
                                                <div class="col-md-10">
                                                    <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary" Text="Submit" TabIndex="10" ValidationGroup="CItem" OnClick="btnSubmit_Click" />
                                                    <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-warning" Text="Cancel" TabIndex="11" CausesValidation="False" OnClick="btnCancel_Click" />
                                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="CItem" ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <div class="col-md-12">
                                                    <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <div class="col-md-12">
                                                    <asp:Panel ID="pnlList" runat="server" Visible="true">
                                                        <%-- <tr>
                                                          <td style="text-align:left;padding-left:40px;padding-top:10px" colspan="2">
                                                              <asp:LinkButton ID="btnAdd" runat="server" SkinID="LinkAddNew" OnClick="btnAdd_Click">Add New</asp:LinkButton>
                                                          </td>
                                                      </tr>--%>
                                                        <asp:ListView ID="lvItemOrder" runat="server">
                                                            <EmptyDataTemplate>
                                                                No Record Found.
                                                            </EmptyDataTemplate>
                                                            <LayoutTemplate>
                                                                <div id="lgv1">
                                                                    <h4 class="box-title">ITEM ORDER ENTRY LIST</h4>
                                                                    <table class="table table-bordered table-hover">
                                                                        <thead>
                                                                            <tr class="bg-light-blue">
                                                                                <%-- <th>Action</th>--%>
                                                                                <th>ITEM NAME</th>
                                                                                <th>QUANTITY ORDERED</th>
                                                                                <th>ORDER DATE</th>
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
                                                                    <%--<td>
                                                                                  <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif" 
                                                                                      CommandArgument='<%# Eval("ORDERID") %>' AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />
                                                                              </td>--%>
                                                                    <td><%# Eval("ITEMNAME")%></td>
                                                                    <td><%# Eval("ORDERQTY")%></td>
                                                                    <td><%# Eval("ORDERDATE", "{0:dd-MMM-yyyy}")%></td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                        <%--<div class="vista-grid_datapager">
                                                                         <asp:DataPager ID="dpComplaintType" runat="server" PagedControlID="lvItemOrder" PageSize="18" OnPreRender="dpComplaintType_PreRender">
                                                                             <Fields>
                                                                                 <asp:NumericPagerField ButtonCount="5" ButtonType="Link" />
                                                                             </Fields>
                                                                         </asp:DataPager>
                                                                     </div>--%>
                                                            </td>
                                                        </tr>
                                                    </asp:Panel>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

