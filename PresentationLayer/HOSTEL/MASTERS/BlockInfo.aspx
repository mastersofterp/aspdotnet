<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="BlockInfo.aspx.cs" Inherits="Hostel_Masters_BlockInfo" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%-- <script type="text/javascript">
        //On Page Load
        $(document).ready(function () {
            $('#table2').DataTable();
        });
    </script>--%>

    <script type="text/javascript">
        //On UpdatePanel Refresh
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    $('#table2').dataTable();
                }
            });
        };

        //Below function added by Saurabh L on 30/09/2022
        //Purpose: for validation 
        function CheckInteger(event, element) {
            var charCode = (event.which) ? event.which : event.keyCode
            if (charCode == 8) return true;
            if ((charCode < 48 || charCode > 57))
                return false;
            return true;
        };
        //--------- End by Saurabh L on 30/09/2022

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
                    <h3 class="box-title">BLOCK MAPPING</h3>
                </div>

                <div class="box-body">
                    <div class="col-12">
                        <div class="row">
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Hostel Name</label>
                                </div>
                                <asp:DropDownList ID="ddlHostelName" runat="server" TabIndex="1" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true"
                                    ValidationGroup="Submit" ToolTip="Hostel Name" AutoPostBack="True"
                                    OnSelectedIndexChanged="ddlHostelName_SelectedIndexChanged" />
                                <asp:RequiredFieldValidator ID="rfvHostelName" runat="server" ControlToValidate="ddlHostelName"
                                    Display="None" ErrorMessage="Please Select Hostel" ValidationGroup="Submit" SetFocusOnError="True"
                                    InitialValue="0" />
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Block Name </label>
                                </div>
                                <asp:DropDownList ID="ddlBlockName" runat="server" TabIndex="2" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true"
                                    ValidationGroup="Submit" ToolTip="Block Name">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvBlockName" runat="server" ControlToValidate="ddlBlockName"
                                    Display="None" ErrorMessage="Please Select Block Name" ValidationGroup="Submit"
                                    SetFocusOnError="True" InitialValue="0" />
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Contains Floors till </label>
                                </div>
                                <asp:DropDownList ID="ddlFloor" runat="server" TabIndex="3" CssClass="form-control" data-select2-enable="true"
                                    ValidationGroup="Submit" ToolTip="No. of Floors" AppendDataBoundItems="true" />
                                <asp:RequiredFieldValidator ID="valFloors" runat="server" ControlToValidate="ddlFloor"
                                    Display="None" ErrorMessage="Please Select Floors"
                                    ValidationGroup="Submit" SetFocusOnError="True"
                                    InitialValue="0" />
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Total No. of Rooms </label>
                                </div>
                                <asp:TextBox ID="txtBlockCapacity" runat="server" TabIndex="4" MaxLength="3" onkeypress="return CheckInteger(event,this);" CssClass="form-control"
                                    ToolTip="Block Capacity" />
                                <asp:RequiredFieldValidator ID="rfvBlockCapacity" runat="server" ControlToValidate="txtBlockCapacity"
                                    Display="None" ErrorMessage="Please Enter Block Capacity" ValidationGroup="Submit"
                                    SetFocusOnError="True" />
                            </div>
                        </div>
                    </div>

                    <div class="col-12 btn-footer">
                        <asp:Button ID="btnSave" runat="server" Text="Submit" ToolTip="Submit" ValidationGroup="Submit"
                            OnClick="btnSave_Click" CssClass="btn btn-primary" TabIndex="5" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false"
                            OnClick="btnCancel_Click" CssClass="btn btn-warning" TabIndex="6" />
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Submit"
                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                    </div>

                    <div class="col-12">
                        <asp:Repeater ID="lvBlock" runat="server">
                            <HeaderTemplate>
                                <div class="sub-heading">
                                    <h5>List of Hostel Blocks</h5>
                                </div>
                                <table id="table2" class="table table-striped table-bordered nowrap display" style="width: 100%">
                                    <thead class="bg-light-blue">
                                        <tr>
                                            <th>Action
                                            </th>
                                            <th>Hostel Name
                                            </th>
                                            <th>Block Name
                                            </th>
                                            <th>Room Capacity
                                            </th>
                                            <th>Floor Name
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("BLOCK_NO") %>'
                                            AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" TabIndex="7" />&nbsp;
                                             <%--   <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.gif" CommandArgument='<%# Eval("HNO") %>'
                                                    AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                                    OnClientClick="showConfirmDel(this); return false;" />--%>
                                    </td>
                                    <td>
                                        <%# Eval("HOSTEL_NAME")%>
                                    </td>
                                    <td>
                                        <%# Eval("BLOCKNAME")%>
                                    </td>
                                    <td>
                                        <%# Eval("ROOM_CAPACITY")%>
                                    </td>
                                    <td>
                                        <%# Eval("FLOOR_NAME")%>
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
    <div id="divMsg" runat="server">
    </div>
</asp:Content>
