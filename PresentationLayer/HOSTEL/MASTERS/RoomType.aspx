<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="RoomType.aspx.cs" Inherits="HOSTEL_MASTERS_RoomType" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<%--<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%-- ADDED FOR DATATABLE USE IN REPEATER FOR BIND 
    ADDED BY SHUBHAM BARKE ON 17/03/22--%>

    <%-- <script type="text/javascript">
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
                    <h3 class="box-title">ROOM TYPE MASTER</h3>
                </div>

                <div class="box-body">
                    <div class="col-12">
                        <div class="row">
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Hostel Name </label>
                                </div>
                                <asp:DropDownList ID="ddlHostel" AppendDataBoundItems="true" runat="server" TabIndex="1" CssClass="form-control" data-select2-enable="true"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvHostel" runat="server" ControlToValidate="ddlHostel"
                                    Display="None" ErrorMessage="Please Select Hostel."
                                    ValidationGroup="submit" SetFocusOnError="True" InitialValue="0" />
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Room Type Name </label>
                                </div>
                                <asp:TextBox ID="txtRoomtypeName" runat="server" MaxLength="25" TabIndex="2" CssClass="form-control" />
                                <asp:RequiredFieldValidator ID="rfvBlockCode" runat="server" ControlToValidate="txtRoomtypeName"
                                    Display="None" ErrorMessage="Please Enter Room Type Name." ValidationGroup="submit" SetFocusOnError="True" />
                            </div>
                        </div>
                    </div>

                    <div class="col-12 btn-footer">
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="submit" TabIndex="3"
                            CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" TabIndex="4"
                            CssClass="btn btn-warning" OnClick="btnCancel_Click" />
                        <asp:ValidationSummary ID="valSummary" runat="server" DisplayMode="List" ShowMessageBox="true"
                            ShowSummary="false" ValidationGroup="submit" />
                    </div>

                    <div class="col-12">
                        <asp:Repeater ID="lvBlock" runat="server">
                            <HeaderTemplate>
                                <div class="sub-heading">
                                    <h5>List of Hostel Room Type</h5>
                                </div>
                                <table id="table2" class="table table-striped table-bordered nowrap display" style="width: 100%">
                                    <thead class="bg-light-blue">
                                        <tr>
                                            <th>Edit
                                            </th>
                                            <th>Hostel Name
                                            </th>
                                            <th>Room Type 
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("TYPE_NO") %>'
                                            AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" TabIndex="5" />&nbsp;
                                            
                                    </td>
                                    <td>
                                        <%# Eval("HOSTEL_NAME") %>
                                    </td>
                                    <td>
                                        <%# Eval("ROOMTYPE_NAME") %>
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

