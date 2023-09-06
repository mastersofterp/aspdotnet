<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Room.aspx.cs" Inherits="Hostel_Masters_Room" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--<script type="text/javascript">
        //On Page Load
        $(document).ready(function () {
            $('#table2').DataTable();
        });
    </script>--%>

    <script type="text/javascript">
        //On UpdatePanel Refresh
        //var prm = Sys.WebForms.PageRequestManager.getInstance();
        //if (prm != null) {
        //    prm.add_endRequest(function (sender, e) {
        //        if (sender._postBackSettings.panelsToUpdate != null) {
        //            $('#table2').dataTable();
        //        }
        //    });
        //};

        function CheckNumeric(event, obj) {
            var k = (window.event) ? event.keyCode : event.which;
            //alert(k);
            if (k == 8 || k == 9 || k == 43 || k == 95 || k == 0) {
                obj.style.backgroundColor = "White";
                return true;
            }
            if (k > 45 && k < 58) {
                obj.style.backgroundColor = "White";
                return true;

            }
            else {
                alert('Please Enter numeric Value');
                obj.focus();
            }
            return false;
        }
        onkeypress = "return CheckAlphabet(event,this);"
        function CheckAlphabet(event, obj) {

            var k = (window.event) ? event.keyCode : event.which;
            if (k == 8 || k == 9 || k == 43 || k == 95 || k == 0 || k == 32 || k == 46 || k == 13) {
                obj.style.backgroundColor = "White";
                return true;

            }
            if (k >= 65 && k <= 90 || k >= 97 && k <= 122) {
                obj.style.backgroundColor = "White";
                return true;

            }
            else {
                alert('Please Enter Alphabets Only!');
                obj.focus();
            }
            return false;
        }
    </script>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">CREATE ROOMS</h3>
                </div>

                <div class="box-body">
                    <div class="col-12">
                        <div class="row">
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Hostel </label>
                                </div>
                                <asp:DropDownList ID="ddlHostel" runat="server" CssClass="form-control" TabIndex="1" AppendDataBoundItems="True" data-select2-enable="true"
                                    AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlHostel_SelectedIndexChanged" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlHostel"
                                    Display="None" ErrorMessage="Please select hostel." ValidationGroup="submit"
                                    SetFocusOnError="True" InitialValue="0" />

                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Block </label>
                                </div>
                                <asp:DropDownList ID="ddlBlock" runat="server" CssClass="form-control" TabIndex="2" AppendDataBoundItems="True" data-select2-enable="true"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlBlock_SelectedIndexChanged">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="valBlock" runat="server" ControlToValidate="ddlBlock"
                                    Display="None" ErrorMessage="Please select block." ValidationGroup="submit" SetFocusOnError="True"
                                    InitialValue="0" />

                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Floor Name </label>
                                </div>
                                <asp:DropDownList ID="ddlFloor" runat="server" TabIndex="3"
                                    AppendDataBoundItems="true" AutoPostBack="True" data-select2-enable="true"
                                    OnSelectedIndexChanged="ddlFloor_SelectedIndexChanged">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="valFloor" runat="server" ControlToValidate="ddlFloor"
                                    Display="None" ErrorMessage="Please select floor." ValidationGroup="submit" SetFocusOnError="True"
                                    InitialValue="0" />
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Room Name </label>
                                </div>
                                <asp:TextBox ID="txtRoomName" CssClass="form-control" runat="server" TabIndex="4" MaxLength="20" />
                                <asp:RequiredFieldValidator ID="valRoom" runat="server" ControlToValidate="txtRoomName"
                                    Display="None" ErrorMessage="Please enter room name." ValidationGroup="submit"
                                    SetFocusOnError="True" />
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Person Capacity </label>
                                </div>
                                <asp:DropDownList ID="ddlCapacity" runat="server" CssClass="form-control" TabIndex="5" data-select2-enable="true"
                                    AutoPostBack="True">
                                    <asp:ListItem Value="0" Selected="True">Please Select</asp:ListItem>
                                    <asp:ListItem Value="1">1</asp:ListItem>
                                    <asp:ListItem Value="2">2</asp:ListItem>
                                    <asp:ListItem Value="3">3</asp:ListItem>
                                    <asp:ListItem Value="4">4</asp:ListItem>
                                    <asp:ListItem Value="5">5</asp:ListItem>
                                    <asp:ListItem Value="6">6</asp:ListItem>
                                    <asp:ListItem Value="7">7</asp:ListItem>
                                    <asp:ListItem Value="8">8</asp:ListItem>
                                    <asp:ListItem Value="9">9</asp:ListItem>
                                    <asp:ListItem Value="10">10</asp:ListItem>
                                    <asp:ListItem Value="11">11</asp:ListItem>
                                    <asp:ListItem Value="12">12</asp:ListItem>
                                    <asp:ListItem Value="13">13</asp:ListItem>
                                    <asp:ListItem Value="14">14</asp:ListItem>
                                    <asp:ListItem Value="15">15</asp:ListItem>
                                    <asp:ListItem Value="16">16</asp:ListItem>
                                    <asp:ListItem Value="17">17</asp:ListItem>
                                    <asp:ListItem Value="18">18</asp:ListItem>
                                    <asp:ListItem Value="19">19</asp:ListItem>
                                    <asp:ListItem Value="20">20</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="valCapacity" runat="server" ControlToValidate="ddlCapacity"
                                    Display="None" ErrorMessage="Please select capacity." ValidationGroup="submit"
                                    SetFocusOnError="True" InitialValue="0" />

                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Room Type Name </label>
                                </div>
                                <asp:DropDownList ID="ddlRoomtypename" runat="server" TabIndex="6"
                                    AppendDataBoundItems="true" AutoPostBack="True" data-select2-enable="true">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvRoomType" runat="server" ControlToValidate="ddlRoomtypename"
                                    Display="None" ErrorMessage="Please select Room Type." ValidationGroup="submit" SetFocusOnError="True"
                                    InitialValue="0" />
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Room Resident </label>
                                </div>
                                <asp:DropDownList ID="ResidentTypeNo" runat="server" TabIndex="6" CssClass="form-control"
                                    AppendDataBoundItems="true" AutoPostBack="True" data-select2-enable="true">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvRoomrsd" runat="server" ControlToValidate="ResidentTypeNo"
                                    Display="None" ErrorMessage="Please select Room Resident." ValidationGroup="submit" SetFocusOnError="True"
                                    InitialValue="0" />
                            </div>
                        </div>
                    </div>
                    <div class="col-12 btn-footer">
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" ValidationGroup="submit"
                            OnClick="btnSubmit_Click" TabIndex="7" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" CausesValidation="false"
                            OnClick="btnCancel_Click" TabIndex="8" />
                        <asp:ValidationSummary ID="valSummary" runat="server" DisplayMode="List" ShowMessageBox="true"
                            ShowSummary="false" ValidationGroup="submit" />
                    </div>

                    <div class="col-12">
                        <asp:Repeater ID="lvRooms" runat="server">
                            <HeaderTemplate>
                                <div class="sub-heading">
                                    <h5>List of Rooms</h5>
                                </div>
                                <table id="table2" class="table table-striped table-bordered nowrap display" style="width: 100%">
                                    <thead class="bg-light-blue">
                                        <tr>
                                            <th>Edit
                                            </th>
                                            <th>Hostel
                                            </th>
                                            <th>Block
                                            </th>
                                            <th>Floor Name
                                            </th>
                                            <th>Room
                                            </th>
                                            <th>Capacity
                                            </th>
                                            <th>Room Type
                                            </th>
                                            <th>Room Resident
                                            </th>
                                            <%--<th>
                                        Semester
                                    </th>--%>
                                        </tr>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("ROOM_NO") %>'
                                            AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" TabIndex="9" />&nbsp;
                                             <%--   <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.gif" CommandArgument='<%# Eval("HNO") %>'
                                                    AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                                    OnClientClick="showConfirmDel(this); return false;" />--%>
                                    </td>
                                    <td>
                                        <%# Eval("HOSTEL_NAME")%>
                                    </td>
                                    <td>
                                        <%# Eval("BLOCK_NAME")%>
                                    </td>
                                    <td>
                                        <%# Eval("FLOOR_NAME")%>
                                    </td>
                                    <td>
                                        <%# Eval("ROOM_NAME")%>
                                    </td>
                                    <td>
                                        <%# Eval("CAPACITY")%>
                                    </td>
                                    <td>
                                        <%# Eval("ROOMTYPE_NAME")%>
                                    </td>
                                    <td>
                                        <%# Eval("RESIDENT_TYPE_NAME")%>
                                    </td>
                                    <%--<td>
                                    <%# Eval("SEMESTERNAME")%>
                                </td>--%>
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

    <div id="divMsg" runat="server">
    </div>
</asp:Content>
