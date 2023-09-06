<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="CreateBusIncharge.aspx.cs" Inherits="VEHICLE_MAINTENANCE_Transaction_CreateBusIncharge" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--  <script src="../../JAVASCRIPTS/JScriptAdmin_Module.js" type="text/javascript"></script>--%>
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div2" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">CREATE BUS INCHARGE</h3>
                </div>
                <div class="box-body">
                    <div class="col-12">
                        <asp:Panel ID="pnlInsurance" runat="server">
                            <div class="row">
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label></label>
                                    </div>
                                    <asp:RadioButtonList ID="rdblistVehicleType" runat="server" RepeatDirection="Horizontal" TabIndex="1"
                                        OnSelectedIndexChanged="rdblistVehicleType_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Selected="True" Value="1">College Vehicles&nbsp;&nbsp;</asp:ListItem>
                                        <asp:ListItem Value="2">Contract Vehicles</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Vehicle</label>
                                    </div>
                                    <asp:DropDownList ID="ddlVehicle" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" TabIndex="2"
                                        ToolTip="Select Vehicle">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="None" runat="server"
                                        ControlToValidate="ddlVehicle" ErrorMessage="Please Select Vehicle" SetFocusOnError="true"
                                        ValidationGroup="submit" InitialValue="0"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Employee</label>
                                    </div>
                                    <asp:DropDownList ID="ddlEmployee" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" TabIndex="2"
                                        ToolTip="Select Employee">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Display="None" runat="server"
                                        ControlToValidate="ddlEmployee" ErrorMessage="Please Select Employee" SetFocusOnError="true"
                                        ValidationGroup="submit" InitialValue="0"></asp:RequiredFieldValidator>
                                </div>

                            </div>
                        </asp:Panel>
                    </div>

                    <div class="col-12 btn-footer">
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" ValidationGroup="submit"
                            OnClick="btnSubmit_Click" ToolTip="Click here to Submit" TabIndex="15" />
                        <asp:Button ID="btnReport" runat="server" Text="Report" CssClass="btn btn-info" TabIndex="17"
                            OnClick="btnReport_Click" ToolTip="Click here to Show Report" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" TabIndex="16"
                            OnClick="btnCancel_Click" ToolTip="Click here to reset" />
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                            ShowSummary="false" DisplayMode="List" ValidationGroup="submit" />
                    </div>
                    <div class="col-12 mt-3">
                        <asp:Panel ID="pnlList" runat="server">
                            <asp:ListView ID="lvBusIncharge" runat="server">
                                <LayoutTemplate>
                                    <div id="lgv1">
                                        <div class="sub-heading">
                                            <h5>Bus Incharge Entry List</h5>
                                        </div>
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>EDIT
                                                    </th>
                                                    <th>VEHICLE NAME
                                                    </th>
                                                    <th>EMPLOYEE NAME
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
                                            <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("BUS_INC_ID") %>'
                                                ToolTip="Edit Record" AlternateText="Edit Record" OnClick="btnEdit_Click" />&nbsp;
                                           <%-- <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.gif"
                                                CommandArgument='<%# Eval("BUS_INC_ID") %>' ToolTip="Delete Record" OnClick="btnDelete_Click"
                                                OnClientClick="showConfirmDel(this); return false;" />--%>
                                        </td>
                                        <td>
                                            <%# Eval("VEHICLENAME")%>
                                        </td>
                                        <td>
                                            <%# Eval("EMPNAME")%>
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


    <script type="text/javascript">

        function validateNumeric(txt) {
            if (isNaN(txt.value)) {
                txt.value = txt.value.substring(0, (txt.value.length) - 1);
                txt.value = '';
                txt.focus = true;
                alert("Only Numeric Characters allowed !");
                return false;
            }
            else
                return true;
        }
    </script>
</asp:Content>
