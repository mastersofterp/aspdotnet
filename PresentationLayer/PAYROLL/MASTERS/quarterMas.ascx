<%@ Control Language="C#" AutoEventWireup="true" CodeFile="quarterMas.ascx.cs" Inherits="Masters_quarterMas" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<div class="row">
    <div class="col-md-12 col-sm-12 col-12">
        <div class="box box-primary">
            <div id="div2" runat="server"></div>
            <div class="box-header with-border">
                <h3 class="box-title">QUARTER MANAGEMENT</h3>
                <asp:Label ID="lblStatus" runat="server" SkinID="Errorlbl" />
            </div>
            <div class="box-body">
                <asp:Panel ID="pnldeducationentry" runat="server">
                    <div class="col-12">
                        <div class="row">
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>*</sup>
                                    <label>Quarter Type</label>
                                </div>
                                <asp:DropDownList ID="ddlQrtType" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                    ToolTip="Select Quarter Type" TabIndex="1">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvQrtType" runat="server" InitialValue="0" ControlToValidate="ddlQrtType"
                                    Display="None" ErrorMessage="Select Quarter Type" ValidationGroup="quarter"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>*</sup>
                                    <label>Quarter Name </label>
                                </div>
                                <asp:TextBox ID="txtQuarter" runat="server" MaxLength="20" TabIndex="2" CssClass="form-control"
                                    ToolTip="Enter Quarter Name(only alphabets)" />
                                <asp:RequiredFieldValidator ID="rfvQuarter" runat="server" ControlToValidate="txtQuarter"
                                    Display="None" ErrorMessage="Please Enter Quarter Name" ValidationGroup="quarter"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                </asp:Panel>

                <div class="col-12 btn-footer">
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click"
                        ValidationGroup="quarter" ToolTip="Click here to Submit" TabIndex="3" CssClass="btn btn-primary" />
                    <asp:Button ID="btnShowReport" runat="server" Text="Report" CausesValidation="False"
                        ToolTip="Click here to Show Report" TabIndex="4" CssClass="btn btn-info" Visible="false" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                        CausesValidation="False" ToolTip="Click here to Reset" TabIndex="4" CssClass="btn btn-warning" />
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="quarter"
                        DisplayMode="List" ShowSummary="false" ShowMessageBox="true" />
                </div>

                <div class="col-12 mt-3">
                    <asp:Panel ID="pnlList" runat="server" ScrollBars="Auto">
                        <asp:ListView ID="lvQuarter" runat="server">
                            <LayoutTemplate>
                                <div id="lgv1">
                                    <div class="sub-heading">
                                        <h5>Quarter List</h5>
                                    </div>
                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                        <thead class="bg-light-blue">
                                            <tr>
                                                <th>Action</th>
                                                <th>Quarter Name</th>
                                                <th>Quarter Type</th>
                                                <th>Quarter Area</th>
                                                <th>Quarter Rent</th>
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
                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("qtrno") %>'
                                            AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />
                                        <asp:Label ID="lblQtrTypeNo" runat="server" Text='<%# Eval("qrttypeno") %>' Visible="false" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblQtrName" runat="server" Text='<%# Eval("qtrname") %>' />
                                    </td>
                                    <td>
                                        <%# Eval("qrttype") %>
                                    </td>
                                    <td>
                                        <%# Eval("qarea") %>
                                    </td>
                                    <td>
                                        <%# Eval("qrent") %>
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