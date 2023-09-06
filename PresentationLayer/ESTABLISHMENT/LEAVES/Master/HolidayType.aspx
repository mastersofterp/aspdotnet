<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="HolidayType.aspx.cs" Inherits="ESTABLISHMENT_LEAVES_Master_HolidayType" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--<script src="https://cdn.datatables.net/1.10.4/js/jquery.dataTables.min.js"></script>

    <script type="text/javascript">
        //On Page Load
        $(document).ready(function () {
            $('#table2').DataTable();
        });
    </script>--%>

    <%--<script type="text/javascript">
        //On UpdatePanel Refresh
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    $('#table2').dataTable();
                }
            });
        };
    </script>

    <script type="text/javascript">
        RunThisAfterEachAsyncPostback();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
    </script>--%>

     <script type="text/javascript">
         //On Page Load
         $(document).ready(function () {
             $('#table2').DataTable();
         });
    </script>

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

    <style>
        .dataTables_scrollHeadInner {
            width:100% !important;
        }
        .table {
            width:100% !important;
        }
    </style>

    <asp:UpdatePanel ID="updAll" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">HOLIDAY TYPE</h3>
                        </div>

                        <div class="box-body">
                            <asp:Panel ID="pnlAdd" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="sub-heading">
                                                <h5>Add/Edit Holiday Type</h5>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Holiday Type</label>
                                            </div>
                                            <asp:TextBox ID="txtHolyType" runat="server" MaxLength="50" TabIndex="1" CssClass="form-control" ToolTip="Enter Holiday Type"  onkeypress = "return CheckAlphabet(event,this);" />
                                            <asp:RequiredFieldValidator ID="rfvHolyType" runat="server" ControlToValidate="txtHolyType"
                                                Display="None" ErrorMessage="Please Enter Holiday Type" ValidationGroup="HolyType"
                                                SetFocusOnError="True">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12 btn-footer">
                                    <%--<td align="center" colspan="2" style="width: 599px">--%>
                                    <asp:Label ID="lblerror" SkinID="Errorlbl" runat="server"></asp:Label>
                                    <asp:Label ID="lblmsg" runat="server" SkinID="lblmsg"></asp:Label>
                                </div>
                            </asp:Panel>
                            <div class="col-12 btn-footer">
                                <asp:LinkButton ID="btnAdd" runat="server" SkinID="LinkAddNew" OnClick="btnAdd_Click" Text="Add New" CssClass="btn btn-primary"
                                    ToolTip="Click here to Add New Holiday Type" TabIndex="2"></asp:LinkButton>
                                <asp:Button ID="btnSave" runat="server" Text="Submit" ValidationGroup="HolyType" OnClick="btnSave_Click"
                                    CssClass="btn btn-primary" ToolTip="Click here to Save" TabIndex="3" />
                                <asp:Button ID="btnBack" runat="server" Text="Back" CausesValidation="false" OnClick="btnBack_Click"
                                    CssClass="btn btn-primary" ToolTip="Click here to Return to Previous Menu" TabIndex="4" />
                                <asp:Button ID="btnShowReport" runat="server" Text="Show Report" CssClass="btn btn-info" ToolTip="Click here to Show Report"
                                    Visible="false" OnClick="btnShowReport_Click" TabIndex="5" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="HolyType"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" TabIndex="6"
                                    OnClick="btnCancel_Click" CssClass="btn btn-warning" ToolTip="Click here to Reset" />

                            </div>

                            <div class="col-12">
                                <asp:Panel ID="pnlList" runat="server">
                                    <table id="table2" class="table table-striped table-bordered nowrap display" style="width: 100%">
                                        <asp:Repeater ID="lvHolyType" runat="server">
                                            <HeaderTemplate>
                                                <div class="sub-heading">
                                                    <h5>Holiday Types List</h5>
                                                </div>
                                            
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Action
                                                        </th>
                                                        <th>Holiday Type
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("HTNO") %>'
                                                            AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" TabIndex="7" />&nbsp;
                                                   <%-- <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.gif" CommandArgument='<%# Eval("PANO") %>'
                                                        AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                                        OnClientClick="showConfirmDel(this); return false;" />--%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("HOLIDAYTYPE")%>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </tbody>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </table>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>

        <Triggers>
            <asp:PostBackTrigger ControlID="btnShowReport" />
            <asp:PostBackTrigger ControlID="btnBack" />
            <asp:PostBackTrigger ControlID="btnSave" />
            <asp:PostBackTrigger ControlID="btnCancel" />
        </Triggers>
    </asp:UpdatePanel>

    <div id="divMsg" runat="server">
    </div>

<%--    <script>
        setTimeout(function () {
            $($.fn.dataTable.tables(true)).DataTable()
                   .columns.adjust();
        }, 200);
    </script>--%>

   <%-- <script>

        $('.nav-tabs a').on('shown.bs.tab', function () {
            $($.fn.dataTable.tables(true)).DataTable()
                   .columns.adjust();
        });
    </script>--%>

    <%--<script>
        setTimeout(function () {
            $($.fn.dataTable.tables(true)).DataTable()
                   .columns.adjust();
        }, 200);
    </script>--%>
</asp:Content>

