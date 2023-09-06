<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="DosageMaster.aspx.cs" Inherits="Health_Master_DosageMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--  <script src="../../JAVASCRIPTS/JScriptAdmin_Module.js" type="text/javascript"></script>--%>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div2" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">DOSAGE MASTER</h3>
                </div>
                <div class="box-body">
                    <asp:Panel ID="pnlBlood" runat="server">
                        <div class="col-12">
                            <div class="row">
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Dosage Name </label>
                                    </div>
                                    <asp:TextBox ID="txtdname" TabIndex="1" runat="server" ToolTip="Enter Dosage Name"
                                        CssClass="form-control" MaxLength="100"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtdname"
                                        Display="None" ErrorMessage="Please Enter Dosage Name" ValidationGroup="submit">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Dosage Quantity </label>
                                    </div>
                                    <asp:TextBox ID="txtquantity" TabIndex="2" runat="server" ToolTip="Enter Dosage Quantity"
                                        CssClass="form-control" MaxLength="100"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvQuant" runat="server" ControlToValidate="txtquantity"
                                        Display="None" ErrorMessage="Please Enter Dosage Quantity" ValidationGroup="submit">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12" id="divCheck" runat="server" visible="false">
                                    <div class="label-dynamic">
                                        <label>Is Active</label>
                                    </div>
                                    <asp:CheckBox ID="chkActive" runat="server" AutoPostBack="true" TabIndex="3"
                                        ToolTip="Check For Active" Checked="true" />
                                </div>
                            </div>
                        </div>
                    </asp:Panel>

                    <div class="col-12 btn-footer">
                        <asp:Button ID="btnSubmit" TabIndex="2" runat="server" Text="Submit" CssClass="btn btn-outline-primary"
                            ValidationGroup="submit" ToolTip="Click here to Submit" OnClick="btnSubmit_Click" />
                        <asp:Button ID="btnCancel" TabIndex="3" runat="server" Text="Cancel" CssClass="btn btn-outline-danger"
                            ToolTip="Click here to Reset" OnClick="btnCancel_Click" />
                        <asp:ValidationSummary ID="submit" runat="server" DisplayMode="List" ShowMessageBox="true"
                            ShowSummary="false" ValidationGroup="submit" />
                    </div>
                    <div class="col-12 mt-3">
                        <asp:Panel ID="pnlDosage" runat="server">
                            <asp:ListView ID="lvDosage" runat="server">
                                <LayoutTemplate>
                                    <div id="lgv1">
                                        <div class="sub-heading">
                                            <h5>Dosage</h5>
                                        </div>
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>Edit
                                                    </th>
                                                    <th>Dosage Name
                                                    </th>
                                                    <th>Dosage Quantity
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
                                            <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("DNO")%>'
                                                AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />

                                        <td>
                                            <%# Eval("DNAME")%>
                                        </td>
                                        <td>
                                            <%# Eval("DQTY")%>
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


    <p class="page_help_text">
        <asp:Label ID="lblHelp" runat="server" Font-Names="Trebuchet MS" />
    </p>

</asp:Content>



