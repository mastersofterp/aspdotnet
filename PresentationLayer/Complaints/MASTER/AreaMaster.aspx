<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="AreaMaster.aspx.cs" Inherits="Complaints_MASTER_AreaMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<script src="../../JAVASCRIPTS/JScriptAdmin_Module.js" type="text/javascript"></script>--%>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">AREA MASTER</h3>
                        </div>

                        <asp:Panel ID="pnlAdd" runat="server">

                            <div class="box-body">
                                <div class="col-12">
                                    <div class="sub-heading">
                                        <h5>Add/Edit Area Master</h5>
                                    </div>
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Area Name  </label>
                                            </div>
                                            <asp:TextBox ID="txtAreaName" runat="server" CssClass="form-control" MaxLength="60" TabIndex="1" ValidationGroup="Area"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvTye" runat="server" ErrorMessage="Please Enter Area Name."
                                                ControlToValidate="txtAreaName" Display="None" ValidationGroup="Area"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="Area" OnClick="btnSubmit_Click" CssClass="btn btn-primary" TabIndex="1" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="False" OnClick="btnCancel_Click" CssClass="btn btn-warning" TabIndex="1" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Area" ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                </div>

                                <div class="col-12">
                                    <asp:Panel ID="pnlList" runat="server">
                                        <asp:ListView ID="lvArea" runat="server">
                                            <LayoutTemplate>
                                                <div id="lgv1">
                                                   
                                                    <div class="sub-heading"><h5>AREA NAME ENTRY LIST</h5></div>
                                                    <table class="table table-hover table-bordered table-striped display" style="width: 100%">
                                                        <thead>
                                                            <tr class="bg-light-blue">
                                                                <th>Action</th>
                                                                <th>Area Name</th>
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
                                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" TabIndex="1"
                                                            CommandArgument='<%# Eval("AREAID") %>' AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />
                                                    </td>
                                                    <td><%# Eval("AREANAME")%></td>
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

