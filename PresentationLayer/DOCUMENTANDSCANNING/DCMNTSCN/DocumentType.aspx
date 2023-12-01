<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="DocumentType.aspx.cs" Inherits="DOCUMENTANDSCANNING_DCMNTSCN_DocumentType" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <script src="../../JAVASCRIPTS/JScriptAdmin_Module.js" type="text/javascript"></script>
 <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <%--<h3 class="box-title">SESSION CREATION</h3>--%>
                    <h3 class="box-title">DOCUMENT TYPE MASTER </h3>
                </div>

                <div class="box-body">
                    <div class="col-12">
                        <asp:Panel ID="pnlAdd" runat="server">
                            <div class="row"> 
                                <div class="form-group col-lg-3 col-md-6 col-12" id="t11" runat="server">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Document type </label>
                                    </div>
                                    <asp:TextBox ID="txtdoctype" runat="server" CssClass="form-control" ToolTip="Enter Document Type" TabIndex="1"
                                        onkeypress="return CheckAlphabet(event,this);"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvdoctype" runat="server"
                                        ControlToValidate="txtdoctype" Display="None"
                                        ErrorMessage="Please enter Document type" SetFocusOnError="true"
                                        ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                       <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtdoctype"
                                       FilterMode="ValidChars" ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890/ ">
                                        </ajaxToolKit:FilteredTextBoxExtender>
                                </div>

                            </div>
                        </asp:Panel>
                    </div>
                    <div class="col-12 btn-footer">

                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="Submit" TabIndex="3"
                              CssClass="btn btn-primary" ToolTip="Click here to Submit" OnClick="btnSubmit_Click" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel"  CssClass="btn btn-warning"
                            ToolTip="Click here to Reset" TabIndex="4" OnClick="btnCancel_Click" />
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Submit"
                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                    </div>
                    <div class="col-12">
                        <asp:Panel ID="pnlList" runat="server" ScrollBars="Auto">
                            <asp:ListView ID="lvDoc" runat="server">
                                <LayoutTemplate>
                                    <div id="lgv1">
                                        <div class="sub-heading">
                                            <h5>DOCUMENT TYPE</h5>
                                        </div>

                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>Action
                                                    </th>
                                                    <th>Document Type
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
                                             <td style="width: 10%">
                                                        <asp:ImageButton ID="btnEdit" runat="server" CommandArgument='<%# Eval("DOC_ID")%>'
                                                            ImageUrl="~/Images/edit.png" ToolTip="Edit record" OnClick="btnEdit_Click"  />
                                        <td>
                                            <%#Eval("DOCUMENT_TYPE") %>
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
</asp:Content>

