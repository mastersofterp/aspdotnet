<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="StaffInformation.aspx.cs" Inherits="HOSTEL_StaffInformation" %>

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

        function CheckInteger(event, element) {
            var charCode = (event.which) ? event.which : event.keyCode
            if (charCode == 8) return true;
            if ((charCode < 48 || charCode > 57))
                return false;
            return true;
        };

        ///Below function added by Saurabh L on 27/09/2022 Purpose: validation for accept only Alphabet 
        function AlphabetsOnly(e, element) {
            var regex = new RegExp(/^[a-zA-Z\s]+$/);
            var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
            if (regex.test(str)) {
                return true;
            }
            else {
                e.preventDefault();
                return false;
            }
        };

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
                    <h3 class="box-title">Hostel Staff Information</h3>
                </div>

                <div class="box-body">
                    <div class="col-12">
                        <div class="row">
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Employee Name </label>
                                </div>
                                <asp:TextBox ID="txtName" runat="server" CssClass="form-control" TabIndex="1" onkeypress="return AlphabetsOnly(event,this);"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvEmpName" runat="server" ErrorMessage="Please Enter Employee Name"
                                    Display="None" ControlToValidate="txtName" SetFocusOnError="True" ValidationGroup="Save"></asp:RequiredFieldValidator>
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Designation </label>
                                </div>
                                <asp:TextBox ID="txtDesignation" runat="server" CssClass="form-control" TabIndex="2" onkeypress="return AlphabetsOnly(event,this);"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvtxtDesignation" runat="server" ErrorMessage="Please Enter Designation"
                                    Display="None" ControlToValidate="txtDesignation" SetFocusOnError="True" ValidationGroup="Save"></asp:RequiredFieldValidator>
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Contact No. </label>
                                </div>

                                <asp:TextBox ID="txtContact" runat="server" CssClass="form-control" TabIndex="3" MaxLength="10"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvtxtContact" runat="server" ErrorMessage="Please Enter Contact No."
                                    Display="None" ControlToValidate="txtContact" SetFocusOnError="True" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="revMobNo" runat="server" ErrorMessage="Invalid Mobile Number."
                                    ValidationExpression="^([0-9]{10})$" ControlToValidate="txtContact" ValidationGroup="Save"></asp:RegularExpressionValidator>
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup></sup>
                                    <label>Email Id </label>
                                </div>
                                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TabIndex="4"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revEmail" runat="server" ErrorMessage="Invalid Email Id"
                                    Display="Dynamic" SetFocusOnError="True" ControlToValidate="txtEmail" ValidationGroup="Save"
                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                            </div>

                            <div class="form-group col-lg-6 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Address </label>
                                </div>
                                <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" TextMode="MultiLine" TabIndex="5" Rows="1"></asp:TextBox>
                                <%--Below RequiredFieldValidator uncommented by Saurabh L on 30/09/2022 for validation--%>
                                <asp:RequiredFieldValidator ID="rfvtxtAddress" runat="server" ErrorMessage="Please Enter Address"
                                    Display="None" ControlToValidate="txtAddress" SetFocusOnError="True" ValidationGroup="Save"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>

                    <div class="col-12">
                        <div class="row">
                            <div class="col-12">
                                <div class="sub-heading">
                                    <h5>Hostel Detail</h5>
                                </div>
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Hostel Session </label>
                                </div>
                                <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="True" TabIndex="2" AutoPostBack="True" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged"
                                    CssClass="form-control" data-select2-enable="true">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvSession" runat="server" ErrorMessage="Please Select Hostel Session"
                                    Display="None" ControlToValidate="ddlSession" SetFocusOnError="True" ValidationGroup="Save"
                                    InitialValue="0"></asp:RequiredFieldValidator>
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Hostel </label>
                                </div>
                                <asp:DropDownList ID="ddlHostel" runat="server" AppendDataBoundItems="True" TabIndex="4" AutoPostBack="True" OnSelectedIndexChanged="ddlHostel_SelectedIndexChanged"
                                    CssClass="form-control" data-select2-enable="true">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvHostel" runat="server" ErrorMessage="Please Select Hostel"
                                    Display="None" ControlToValidate="ddlHostel" SetFocusOnError="True" ValidationGroup="Save"
                                    InitialValue="0"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>

                    <div class="col-12 btn-footer">
                        <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="Save" TabIndex="8" OnClick="btnSave_Click" CssClass="btn btn-primary" />
                        <asp:Button ID="btnReport" runat="server" Text="Report" TabIndex="9" OnClick="btnReport_Click" CssClass="btn btn-info" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="10" OnClick="btnCancel_Click" CssClass="btn btn-warning" />
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                            ShowMessageBox="True" ShowSummary="False" ValidationGroup="Save" />
                    </div>

                    <div class="col-12">
                        <asp:Repeater ID="lvStaff" runat="server">
                            <HeaderTemplate>
                                <div class="sub-heading">
                                    <h5>List of Staff Details</h5>
                                </div>
                                <table id="table2" class="table table-striped table-bordered nowrap display" style="width: 100%">
                                    <thead class="bg-light-blue">
                                        <tr>
                                            <th>Edit
                                            </th>
                                            <th>Employee Name
                                            </th>
                                            <th>Designation
                                            </th>
                                            <th>Contact No
                                            </th>
                                            <th>Email Id.
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("STNO") %>'
                                            AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" TabIndex="7" />&nbsp;
                                             <%--   <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.gif" CommandArgument='<%# Eval("HNO") %>'
                                                    AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                                    OnClientClick="showConfirmDel(this); return false;" />--%>
                                    </td>
                                    <td>
                                        <%# Eval("EMP_NAME")%>
                                    </td>
                                    <td>
                                        <%# Eval("DESIGNATION")%>
                                    </td>
                                    <td>
                                        <%# Eval("CONTACTNO")%>
                                    </td>
                                    <td>
                                        <%# Eval("EMAILID")%>
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
    <div id="divMsg" runat="server"></div>
</asp:Content>
