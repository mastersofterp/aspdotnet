<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="HostelRoomCharge.aspx.cs" Inherits="HOSTEL_MASTERS_HostelRoomCharge" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdCharge" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">HOSTEL ROOM CHARGE</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Hostel Session</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSession" runat="server" TabIndex="1" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true"
                                            ValidationGroup="Submit" AutoPostBack="True" />
                                        <asp:RequiredFieldValidator ID="rfvHostelSession" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Hostel Session" ValidationGroup="Submit" SetFocusOnError="True"
                                            InitialValue="0" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Hostel Name</label>
                                        </div>
                                        <asp:DropDownList ID="ddlHostel" runat="server" TabIndex="3" CssClass="form-control" data-select2-enable="true"
                                            ValidationGroup="Submit" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlHostel_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvHostel" runat="server" ControlToValidate="ddlHostel"
                                            Display="None" ErrorMessage="Please Select Hostel Name"
                                            ValidationGroup="Submit" SetFocusOnError="True"
                                            InitialValue="0" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Room Type</label>
                                        </div>
                                        <asp:DropDownList ID="ddlRoomType" runat="server" TabIndex="3" CssClass="form-control" data-select2-enable="true"
                                            ValidationGroup="Submit" AppendDataBoundItems="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="valFloors" runat="server" ControlToValidate="ddlRoomType"
                                            Display="None" ErrorMessage="Please Select Room Type"
                                            ValidationGroup="Submit" SetFocusOnError="True"
                                            InitialValue="0" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Guest Type</label>
                                        </div>
                                        <asp:DropDownList ID="ddlResidentType" runat="server" TabIndex="2" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true"
                                            ValidationGroup="Submit">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvResidentType" runat="server" ControlToValidate="ddlResidentType"
                                            Display="None" ErrorMessage="Please Select Guest Type" ValidationGroup="Submit"
                                            SetFocusOnError="True" InitialValue="0" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Charges(In Rs.)</label>
                                        </div>
                                        <asp:TextBox ID="txtCharge" runat="server" TabIndex="4" MaxLength="8" CssClass="form-control"
                                            ToolTip="Block Capacity" />
                                              <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" runat="server" FilterType="Numbers, Custom"
                                               ValidChars=".," TargetControlID="txtCharge" />
                                        <asp:RequiredFieldValidator ID="rfvtxtCharge" runat="server" ControlToValidate="txtCharge"
                                            Display="None" ErrorMessage="Please Enter Charges" ValidationGroup="Submit"
                                            SetFocusOnError="True" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSave" runat="server" Text="Submit" ToolTip="Submit" ValidationGroup="Submit"
                                    CssClass="btn btn-primary" TabIndex="5" OnClick="btnSave_Click"/>
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false"
                                    CssClass="btn btn-warning" TabIndex="6" OnClick="btnCancel_Click" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Submit"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                            </div>
                            <div class="col-12">
                                <asp:Repeater ID="lvCharge" runat="server">
                                    <HeaderTemplate>
                                        <div class="sub-heading">
                                            <h5>List of Hostel Blocks</h5>
                                        </div>
                                        <table id="table2" class="table table-striped table-bordered nowrap display" style="width: 100%">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>Action
                                                    </th>
                                                    <th>Hostel Session
                                                    </th>
                                                    <th>Hostel Name 
                                                    </th>
                                                    <th>Room Type
                                                    </th>
                                                    <th>Guest Type 
                                                    </th>
                                                    <th>Charges</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("CHARGE_NO") %>'
                                                    AlternateText="Edit Record" ToolTip="Edit Record" TabIndex="7" OnClick="btnEdit_Click"/>&nbsp;
                                             <%--   <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.gif" CommandArgument='<%# Eval("HNO") %>'
                                                    AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                                    OnClientClick="showConfirmDel(this); return false;" />--%>
                                            </td>
                                            <td>
                                                <%# Eval("SESSION_NAME")%>
                                            </td>
                                            <td>
                                                <%# Eval("HOSTEL_NAME")%>
                                            </td>
                                            <td>
                                                <%# Eval("ROOMTYPE_NAME")%>
                                            </td>
                                            <td>
                                                <%# Eval("RESIDENT_TYPE_NAME")%>
                                            </td>
                                              <td>
                                                <%# Eval("CHARGES")%>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </tbody></table>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
        </Triggers>
    </asp:UpdatePanel>
    <div id="divMsg" runat="server">
    </div>
</asp:Content>

