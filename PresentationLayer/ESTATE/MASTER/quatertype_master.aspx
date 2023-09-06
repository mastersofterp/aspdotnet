<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="quatertype_master.aspx.cs" Inherits="ESTATE_Master_quatertype_master" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="updqtrtypemaster" runat="server">
        <ContentTemplate>
            
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">QUARTER TYPE MASTER</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-md-12">
                                <asp:Panel ID="pnlAdd" runat="server">
                                    <div class="panel panel-info">
                                        <div class="panel-heading">
                                            Add/Edit Quarter Type Master
                                        </div>
                                        <div class="panel-body">
                                            <div class="form-group row">
                                                <div class="col-md-12">
                                                    Note <b>:</b> <span style="color: #FF0000">* Marked Is Mandatory !</span>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <div class="col-md-4">
                                                    <label>Quarter Type <span style="color: red;">*</span>:</label>
                                                    <asp:TextBox ID="txtquartertype" runat="server" CssClass="form-control" MaxLength="25" TabIndex="1"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvQuaterType" runat="server" ControlToValidate="txtquartertype"
                                                        ErrorMessage="Please Enter Quater Type." Display="None" ValidationGroup="quatertypemaster"></asp:RequiredFieldValidator>
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="ftbeType" runat="server" TargetControlID="txtquartertype" FilterType="Custom, LowercaseLetters,  Numbers, UppercaseLetters"
                                                        ValidChars="- ">
                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                </div>
                                                <div class="col-md-4">
                                                    <label>Entitled Staff <span style="color: red;">*</span>:</label>
                                                    <asp:DropDownList ID="ddlEStaff" runat="server" ValidationGroup="quatertypemaster" TabIndex="2" CssClass AppendDataBoundItems="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvStaff" runat="server" ControlToValidate="ddlEStaff" ErrorMessage="Please Select Entitled Staff." Display="None"
                                                        InitialValue="0" ValidationGroup="quatertypemaster"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="col-md-4">
                                                    <label>License Fee:</label>
                                                    <asp:TextBox ID="txtRent" runat="server" CssClass="col-md-4" MaxLength="5" TabIndex="3"></asp:TextBox>
                                                    <%--<asp:RequiredFieldValidator ID="rfvQrent" runat="server" ControlToValidate="txtRent" ValidationGroup="quatertypemaster"
                                                           ErrorMessage="Quater Rent Required" Display="None" ></asp:RequiredFieldValidator>--%>
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="ftbeRent" runat="server" TargetControlID="txtRent" ValidChars="0123456789."></ajaxToolKit:FilteredTextBoxExtender>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <div class="col-md-4">
                                                    <label>Quarter Area:</label>
                                                    <div class="input-group">
                                                        <asp:TextBox ID="txtqArea" runat="server" CssClass="form-control" MaxLength="5" TabIndex="4"></asp:TextBox>
                                                        <div class="input-group-addon">
                                                            <label>Sqft</label>
                                                        </div>
                                                    </div>
                                                    <%--<asp:RequiredFieldValidator ID="rfvquarterArea" runat="server" ControlToValidate="txtqArea" ValidationGroup="quatertypemaster"
                                                         ErrorMessage="Quater Area  Required" Display="None" ></asp:RequiredFieldValidator>--%>
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="ftbeArea" runat="server" TargetControlID="txtqArea" ValidChars="0123456789."></ajaxToolKit:FilteredTextBoxExtender>
                                                </div>
                                                <div class="col-md-4">
                                                    <label>Fixed Charge:</label>
                                                    <asp:TextBox ID="txtfixedcharge" runat="server" CssClass="form-control" TabIndex="5" MaxLength="10"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvfixedCharge" runat="server" ControlToValidate="txtfixedcharge"
                                                        ErrorMessage="Please Enter Fixed Charge." SetFocusOnError="true" ValidationGroup="quatertypemaster" Display="None"></asp:RequiredFieldValidator>
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="ftbefenergyFixedCharge" runat="server" TargetControlID="txtfixedcharge"
                                                        ValidChars="0123456789." Enabled="True">
                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <div class="col-md-12 text-center">
                                                    <asp:ValidationSummary ID="vsumQuarter" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="quatertypemaster" />
                                                    <asp:Button ID="btnsubmit" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="btnsubmit_Click" TabIndex="6" ValidationGroup="quatertypemaster" />
                                                    <asp:Button ID="btnreset" runat="server" Text="Reset" CssClass="btn btn-warning" OnClick="btnreset_Click" TabIndex="7" />
                                                    <asp:Button ID="btnreport" runat="server" Text="Report" CssClass="btn btn-info" OnClick="btnreport_Click" TabIndex="8" />
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <div class="col-md-12">
                                                    <asp:Panel runat="server" ID="fldmetertype">
                                                        <div class="table-responsive">
                                                            <asp:Repeater ID="Repeater_metertype" runat="server"
                                                                OnItemCommand="Repeater_metertype_ItemCommand">
                                                                <HeaderTemplate>
                                                                    <table class="table table-bordered table-hover">
                                                                        <thead>
                                                                            <tr class="bg-light-blue">
                                                                                <th>EDIT
                                                                                </th>
                                                                                <th>QUARTER TYPE
                                                                                </th>
                                                                                <th>STAFF TYPE
                                                                                </th>
                                                                                <th>RENT
                                                                                </th>
                                                                                <th>QUARTER AREA
                                                                                </th>
                                                                                <th>FIXED CHARGE
                                                                                </th>
                                                                            </tr>
                                                                        </thead>
                                                                        <tbody>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:ImageButton ID="ImageButton_Edit" runat="server" CommandArgument=' <%#Eval("QNO")%>' CommandName="edit" ImageUrl="../../IMAGES/edit.gif" ToolTip="Update this  Investigation" />
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblquartertype" runat="server" Text=' <%#Eval("QUARTER_TYPE")%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblStafftype" runat="server" Text=' <%#Eval("STAFF_TYPE")%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblrent" runat="server" Text=' <%#Eval("RENT")%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="Label1" runat="server" Text=' <%#Eval("QAREA")%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblFixedCharge" runat="server" Text=' <%#Eval("FIXED_CHARGE")%>'></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    </tbody></table>
                                                                </FooterTemplate>
                                                            </asp:Repeater>
                                                        </div>
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
            <div id="divMsg" runat="server">
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

