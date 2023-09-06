<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="quarter_occupation.aspx.cs"
    Inherits="ESTATE_Transaction_quarter_occupation" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="updOccupantMaterial" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">QUARTER ASSETS ALLOTMENT </h3>
                        </div>
                        <div class="box-body">
                            <div class="col-md-12">
                                <asp:Panel ID="pnlAdd" runat="server">
                                    <div class="panel panel-info">
                                        <div class="panel-heading">
                                            Add/Edit Quarter Assets Allotment
                                        </div>
                                        <div class="panel-body">
                                            <div class="form-group row">
                                                <div class="col-md-12">
                                                    Note <b>:</b> <span style="color: #FF0000">* Marked Is Mandatory !</span>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <div class="col-md-4">
                                                    <label>Resident Type<span style="color: red;">*</span> :</label>
                                                    <asp:DropDownList ID="ddlEmptype" runat="server" AppendDataBoundItems="true" AutoPostBack="true"
                                                        CssClass="form-control" TabIndex="1"
                                                        OnSelectedIndexChanged="ddlEmptype_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvEmployee" runat="server" ControlToValidate="ddlEmptype"
                                                        ErrorMessage="Please Select Resident Type." InitialValue="0"  ValidationGroup="occupant"
                                                        Display="None" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                </div>

                                                <div class="col-md-4">
                                                    <label>Occupant Name<span style="color: red;">*</span> :</label>
                                                    <asp:DropDownList ID="ddlEmpName" runat="server" AppendDataBoundItems="true" CssClass="form-control" TabIndex="2"
                                                        OnSelectedIndexChanged="ddlEmpName_SelectedIndexChanged" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvEmployeeName" runat="server" ControlToValidate="ddlEmpName"
                                                        ErrorMessage="Please Select Name of occupant." InitialValue="0" Display="None" ValidationGroup="occupant"
                                                        SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                    <asp:ValidationSummary ID="valsEmployee" runat="server"  ValidationGroup="occupant" DisplayMode="List"
                                                        ShowSummary="false" ShowMessageBox="true" />
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="panel panel-info">
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <div class="col-md-4">
                                                    <label>Office Order Date :</label>
                                                    <asp:TextBox ID="txtoffceOrderDt" runat="server" CssClass="form-control" TabIndex="3" ReadOnly="true"></asp:TextBox>
                                                </div>

                                                <div class="col-md-4">
                                                    <label>Quarter Type<span style="color: red;">*</span> :</label>
                                                    <asp:DropDownList ID="ddlquarterType" runat="server" CssClass="form-control" TabIndex="4">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlquarterType"
                                                        ErrorMessage="Please Select Quarter Type." Display="None" ValidationGroup="material"
                                                        SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                </div>

                                                <div class="col-md-4">
                                                    <label>Allotment Date :</label>
                                                    <asp:TextBox ID="txtAllotDT" runat="server" CssClass="form-control" TabIndex="5" ReadOnly="true"></asp:TextBox>
                                                </div>

                                                <div class="col-md-4">
                                                    <label>Quarter No :</label>
                                                    <asp:DropDownList ID="ddlquarterNo" runat="server" CssClass="form-control" TabIndex="6">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="panel panel-info">
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <div class="col-md-4">
                                                    <label>Assets<span style="color: red;">*</span> :</label>
                                                    <asp:DropDownList ID="ddlMaterial" runat="server" AppendDataBoundItems="true" CssClass="form-control" TabIndex="7">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvMaterial" runat="server" ControlToValidate="ddlMaterial"
                                                        ErrorMessage="Please Select Assets." Display="None" InitialValue="0" ValidationGroup="material"
                                                        SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                </div>

                                                <div class="col-md-4">
                                                    <label>Quantity<span style="color: red;">*</span> :</label>
                                                    <asp:ValidationSummary ID="vsummaterial" runat="server" ShowMessageBox="true" ShowSummary="false"
                                                        ValidationGroup="material" DisplayMode="List" />
                                                    <asp:TextBox ID="txtQuantity" runat="server" CssClass="form-control" TabIndex="8" MaxLength="3"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtQuantity"
                                                        ErrorMessage="Please Enter Quantity." Display="None" ValidationGroup="material"
                                                        SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="ftbeQuantity" runat="server" TargetControlID="txtQuantity"
                                                        FilterType="Numbers">
                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                </div>

                                                <div class="col-md-4">
                                                    <br />
                                                    <asp:Button ID="btnSaveMaterial" runat="server" OnClick="btnSaveMaterial_Click" Text="Add Asset"
                                                        CssClass="btn btn-primary" ValidationGroup="material" TabIndex="9" />
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="panel panel-info">
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="table-responsive">
                                                        <asp:Repeater ID="Repeater_meterialtype" runat="server" OnItemCommand="Repeater_meterialtype_ItemCommand">
                                                            <HeaderTemplate>
                                                                <table class="table table-bordered table-hover">
                                                                    <thead>
                                                                        <tr class="bg-light-blue">
                                                                            <th>Delete
                                                                            </th>
                                                                            <th>Asset Name
                                                                            </th>
                                                                            <th>Quantity
                                                                            </th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td>
                                                                        <asp:ImageButton ID="ImageButton_Edit" runat="server" CommandName="delete" ImageUrl="../../IMAGES/Delete.gif"
                                                                            ToolTip="Update this Material Type" CommandArgument='<%#Eval("IDNO")%>' />
                                                                    </td>

                                                                    <td>
                                                                        <asp:Label ID="lblmaterialtype" runat="server" Text=' <%#Eval("MNAME")%>'
                                                                            ToolTip='<%#Eval("MNO")%>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblqty" runat="server" Text=' <%#Eval("QNT")%>'></asp:Label>
                                                                    </td>

                                                                </tr>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                </tbody>
                                                            </table>
                                                            </FooterTemplate>
                                                        </asp:Repeater>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="text-center">
                                                        <asp:Button ID="btnOccupantSave" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnOccupantSave_Click"
                                                            ValidationGroup="occupant" TabIndex="10" />
                                                        <asp:ValidationSummary ID="vssubmit" runat="server"  ValidationGroup="occupant" ShowMessageBox="true" ShowSummary="true" DisplayMode="List" />
                                                        <asp:Button ID="btnOccupantCancel" runat="server" Text="Reset" CssClass="btn btn-warning" OnClick="btnOccupantCancel_Click"
                                                            TabIndex="11" />
                                                        <asp:Button ID="btnReport" runat="server" Text="Report" CssClass="btn btn-info" OnClick="btnReport_Click"
                                                            TabIndex="12" />
                                                    </div>
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



