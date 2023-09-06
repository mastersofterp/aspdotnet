<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="SubGrievance.aspx.cs" Inherits="GrievanceRedressal_Master_SubGrievance" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<script src="../../JAVASCRIPTS/JScriptAdmin_Module.js" type="text/javascript"></script>--%>

    <asp:UpdatePanel ID="updActivity" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">GRIEVANCE TYPE</h3>
                        </div>
                        <div class="box-body">
                            <asp:Panel ID="pnlGriv" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="sub-heading">
                                                <h5>Add/Edit Grievance Type</h5>
                                            </div>
                                        </div>
                                         <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Grievance Type </label>
                                            </div>
                                             <asp:DropDownList ID="ddlGriv" AppendDataBoundItems="true" runat="server" CssClass="form-control" data-select2-enable="true"
                                                 AutoPostBack="true" TabIndex="1">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>                                            
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSubGriv"
                                                Display="None" ErrorMessage="Please Enter Grievance Type" ValidationGroup="Submit"
                                                SetFocusOnError="true"></asp:RequiredFieldValidator>                                          
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Sub Grievance Type </label>
                                            </div>
                                            <asp:TextBox ID="txtSubGriv" runat="server" ValidationGroup="Submit" MaxLength="100" TabIndex="1"
                                                 CssClass="form-control" ToolTip="Enter Grievance Type"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvGrievanceType" runat="server" ControlToValidate="txtSubGriv"
                                                Display="None" ErrorMessage="Please Enter Grievance Type" ValidationGroup="Submit"
                                                SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredtxtGT" runat="server"
                                                FilterType="Custom,UppercaseLetters, LowercaseLetters,Numbers" TargetControlID="txtSubGriv" ValidChars=" ">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                        </div>
                                        <%--<div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Grievance Type Code</label>
                                            </div>
                                            <asp:TextBox ID="txtGRCode" runat="server"
                                                ValidationGroup="Submit" MaxLength="6" TabIndex="1" CssClass="form-control" ToolTip="Enter Grievance Type Code"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvgrCode" runat="server" ControlToValidate="txtGRCode"
                                                Display="None" ErrorMessage="Please Enter Grievance Type Code" ValidationGroup="Submit"
                                                SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                FilterType="Custom,UppercaseLetters, LowercaseLetters,Numbers" TargetControlID="txtGRCode" ValidChars=" ">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                        </div>--%>

                                    </div>
                                </div>
                            </asp:Panel>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="Submit" CssClass="btn btn-primary" ToolTip="Click here to Submit" OnClick="btnSubmit_Click" CausesValidation="true" TabIndex="1" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" ToolTip="Click here to Cancel" OnClick="btnCancel_Click" CausesValidation="false" TabIndex="1" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Submit" />
                            </div>
                            <div class="col-12">
                                <asp:Panel ID="pnlGrievance" runat="server">
                                    <asp:ListView ID="lvSubGrivType" runat="server">
                                        <LayoutTemplate>
                                            <div id="lgv1">
                                                <div class="sub-heading">
                                                    <h5>GRIEVANCE TYPE LIST </h5>
                                                </div>

                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>EDIT
                                                            </th>
                                                            <th>SUB GRIEVANCE TYPE 
                                                            </th>
                                                            <th>GRIEVANCE TYPE
                                                            </th>
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
                                                    <asp:ImageButton ID="btnEdit" runat="server" AlternateText="Edit Record" CausesValidation="false"
                                                        CommandArgument='<%# Eval("SUB_ID") %>' ImageUrl="~/Images/edit.png"
                                                        ToolTip="Edit Record" OnClick="btnEdit_Click" TabIndex="1" />
                                                </td>
                                                <td>
                                                    <%# Eval("SUBGTNAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("GT_NAME")%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>



