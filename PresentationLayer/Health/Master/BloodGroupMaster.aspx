<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="BloodGroupMaster.aspx.cs" Inherits="Health_Master_BloodGroupMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--  <script src="../../JAVASCRIPTS/JScriptAdmin_Module.js" type="text/javascript"></script>--%>
    <%--<div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updActivity"
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
    </div>--%>
    <asp:UpdatePanel ID="updActivity" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">BLOOD GROUP MASTER</h3>
                        </div>
                        <div class="box-body">
                            <asp:Panel ID="pnlBlood" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Blood Group </label>
                                            </div>
                                            <asp:TextBox ID="txtBGroup" TabIndex="1" runat="server" ToolTip="Enter Blood Group"
                                                CssClass="form-control"></asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftbeBGroup" runat="server" ValidChars="+- "
                                                FilterType="Custom,UppercaseLetters, LowercaseLetters" TargetControlID="txtBGroup">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                            <asp:RequiredFieldValidator ID="rfvBgroup" runat="server" ControlToValidate="txtBGroup"
                                                Display="None" ErrorMessage="Please Enter Blood Group" ValidationGroup="submit">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSubmit" TabIndex="2" runat="server" Text="Submit" CssClass="btn btn-outline-primary"
                                    ValidationGroup="submit" OnClick="btnSubmit_OnClick" ToolTip="Click here to Submit" />
                                <asp:Button ID="btnRport" runat="server" Text="Report" TabIndex="4" CssClass="btn btn-outline-info"
                                    OnClick="btnRport_Click" ToolTip="Click here to Show Report" />
                                <asp:Button ID="btnCancel" TabIndex="3" runat="server" Text="Cancel" CssClass="btn btn-outline-danger"
                                    ToolTip="Click here to Reset" OnClick="btnCancel_OnClick" />
                                <asp:ValidationSummary ID="submit" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="submit" />
                            </div>
                            <div class="col-12 mt-3">
                                <asp:Panel ID="pnlBloodList" runat="server">
                                    <asp:ListView ID="lvBGroup" runat="server">
                                        <LayoutTemplate>
                                            <div id="lgv1">
                                                <div class="sub-heading">
                                                    <h5>Blood Group List</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>Edit
                                                            </th>
                                                            <th>Blood Group
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
                                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("BLOODGRNO")%>'
                                                        AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />

                                                <td>
                                                    <%# Eval("BLOODGR")%>
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
     <p class="page_help_text">
                            <asp:Label ID="lblHelp" runat="server" Font-Names="Trebuchet MS" />
                        </p>

</asp:Content>

