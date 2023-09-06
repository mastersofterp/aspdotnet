<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="complaint_item.aspx.cs"
    Inherits="Estate_complaint_item" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<script src="../../JAVASCRIPTS/JScriptAdmin_Module.js" type="text/javascript"></script>--%>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">ITEM MASTER</h3>
                        </div>
                        <div class="box-body">

                            <asp:Panel ID="pnlAdd" runat="server">
                                <div class="box-body">
                                    <div class="col-12">
                                        <div class="sub-heading">
                                            <h5>Add/Edit Item</h5>
                                        </div>
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Service Department </label>
                                                </div>
                                                <asp:DropDownList ID="ddlDepartmentName" Enabled="true" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" TabIndex="1"
                                                    OnSelectedIndexChanged="ddlDepartmentName_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem Value="-1">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                    ControlToValidate="ddlDepartmentName" Display="None" ErrorMessage="please Enter Department Name."
                                                    ValidationGroup="CItem" InitialValue="-1"></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Service Request Type </label>
                                                </div>
                                                <asp:DropDownList ID="ddlItemType" runat="server" CssClass="form-control" data-select2-enable="true"
                                                    AppendDataBoundItems="true" TabIndex="1">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                                                        ControlToValidate="ddlItemType" Display="None" ErrorMessage="Please Enter Item Type."
                                                        ValidationGroup="CItem" InitialValue="-1"></asp:RequiredFieldValidator>--%>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Item Code </label>
                                                </div>
                                                <asp:TextBox ID="txtItemCode" runat="server" CssClass="form-control" MaxLength="25" TabIndex="1" />
                                                <asp:RequiredFieldValidator ID="rfvItemCode" runat="server" ControlToValidate="txtItemCode" ErrorMessage="Please Enter Item Code."
                                                    ValidationGroup="CItem" Display="None"></asp:RequiredFieldValidator>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="ftbeType" runat="server" TargetControlID="txtItemCode" FilterType="Custom,LowercaseLetters,UppercaseLetters,Numbers"
                                                    ValidChars=" ">
                                                </ajaxToolKit:FilteredTextBoxExtender>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Item Name </label>
                                                </div>
                                                <asp:TextBox ID="txtItemName" runat="server" CssClass="form-control" MaxLength="80" TabIndex="1" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtItemName" Display="None" ErrorMessage="Please Enter Item Name." ValidationGroup="CItem"></asp:RequiredFieldValidator>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtItemName" FilterType="Custom, LowercaseLetters,UppercaseLetters,Numbers"
                                                    ValidChars=",.&- ">
                                                </ajaxToolKit:FilteredTextBoxExtender>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Item Unit </label>
                                                </div>
                                                <asp:DropDownList ID="ddlItemUnit" runat="server" CssClass="form-control"
                                                    AppendDataBoundItems="true" TabIndex="1" data-select2-enable="true">
                                                    <asp:ListItem Value="-1">Please Select</asp:ListItem>
                                                    <asp:ListItem Value="Nos">Nos</asp:ListItem>
                                                    <asp:ListItem Value="Kg">Kg</asp:ListItem>
                                                    <asp:ListItem Value="Ltr">Ltr</asp:ListItem>
                                                    <asp:ListItem Value="Meter">Meter</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                                    ControlToValidate="ddlItemUnit" ErrorMessage="Please Enter Item Unit."
                                                    ValidationGroup="CItem" Display="None" InitialValue="-1"></asp:RequiredFieldValidator>
                                            </div>


                                            <div class="form-group col-lg-3 col-md-6 col-12" id="maxstock" runat="server" visible="false">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Max Stock </label>
                                                </div>
                                                <asp:TextBox ID="txtMaxStock" runat="server" Visible="false" CssClass="form-control" TabIndex="1" MaxLength="10" />
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtMaxStock" FilterType="Numbers">
                                                </ajaxToolKit:FilteredTextBoxExtender>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12" id="minstock" runat="server" visible="false">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Min Stock </label>
                                                </div>
                                                <asp:TextBox ID="txtMinStock" runat="server" Visible="false" CssClass="form-control" TabIndex="1" MaxLength="10" />
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtMinStock" FilterType="Numbers">
                                                </ajaxToolKit:FilteredTextBoxExtender>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12" id="currentstock" runat="server" visible="false">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Current Stock </label>
                                                </div>
                                                <asp:TextBox ID="txtCurrentStock" runat="server" Visible="false" CssClass="form-control" TabIndex="1" MaxLength="10" />
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtCurrentStock" FilterType="Numbers"></ajaxToolKit:FilteredTextBoxExtender>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" TabIndex="1" ValidationGroup="CItem" OnClick="btnSubmit_Click" CssClass="btn btn-primary" />
                                        <asp:Button ID="btnReport" runat="server" Text="Report" TabIndex="1" CausesValidation="false" OnClick="btnReport_Click" CssClass="btn btn-info" />
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="1" CausesValidation="False" OnClick="btnCancel_Click" CssClass="btn btn-warning" />
                                        <asp:Button ID="btnBack" runat="server" Text="Back" TabIndex="1" CausesValidation="false" OnClick="btnBack_Click" CssClass="btn btn-warning" Visible="false" />
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="CItem" ShowMessageBox="true" ShowSummary="false"
                                            DisplayMode="List" />
                                    </div>
                                    <div class="col-12">
                                        <asp:Panel ID="pnlList" runat="server" Visible="true">
                                            <%--  <tr>
                                                                  <td style="text-align:left;padding-left:40px;padding-top:10px" colspan="2">
                                                                      <asp:LinkButton ID="btnAdd" runat="server" SkinID="LinkAddNew" OnClick="btnAdd_Click">Add New</asp:LinkButton>
                                                                 
                                                                  </td>
                                                              </tr>--%>
                                            <asp:ListView ID="lvComplaintItem" runat="server">
                                                <LayoutTemplate>
                                                    <div id="lgv1">

                                                        <div class="sub-heading">
                                                            <h5>ITEM LIST</h5>
                                                        </div>
                                                        <table class="table table-hover table-bordered table-striped display" style="width: 100%">
                                                            <thead>
                                                                <tr class="bg-light-blue">
                                                                    <th>ACTION</th>
                                                                    <th>SERVICE DEPARTMENT</th>
                                                                    <th>ITEM NAME</th>
                                                                    <%-- <th>MIN STOCK</th>
                                                                                <th>MAX STOCK</th>
                                                                                <th>CURRENT STOCK</th>--%>
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
                                                            <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png"
                                                                CommandArgument='<%# Eval("ITEMID") %>' AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />
                                                        </td>
                                                        <td><%# Eval("DEPTNAME")%></td>
                                                        <td><%# Eval("ITEMNAME")%></td>
                                                        <%-- <td><%# Eval("MINSTOCK")%></td>
                                                                    <td><%# Eval("MAXSTOCK")%></td>
                                                                    <td><%# Eval("CURRSTOCK")%></td>--%>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </asp:Panel>
                                    </div>

                                </div>

                            </asp:Panel>

                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="divMsg" runat="server"></div>
</asp:Content>

