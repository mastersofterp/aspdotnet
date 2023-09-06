<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Passing_Authority.aspx.cs" Inherits="ESTABLISHMENT_LEAVES_Master_Passing_Authority" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="https://cdn.datatables.net/1.10.4/js/jquery.dataTables.min.js"></script>

    <%--<script type="text/javascript">
        //On Page Load
        $(document).ready(function () {
            $('#table2').DataTable();
        });
    </script>--%>

   <%-- <script type="text/javascript">
        //On UpdatePanel Refresh
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    $('#table2').dataTable();
                }
            });
        };
    </script>--%>

    <%--<script type="text/javascript">
        RunThisAfterEachAsyncPostback();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
    </script>--%>

    <script type="text/javascript">
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


    <asp:UpdatePanel ID="updAll" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">PASSING AUTHORITY</h3>
                        </div>
                        <div class="box-body">
                            <asp:Panel ID="pnlAdd" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="sub-heading">
                                                <h5>Add/Edit Passing Authority</h5>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <asp:UpdatePanel ID="updAdd" runat="server">
                                    <ContentTemplate>
                                        <div class="col-12">
                                            <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>College Name</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control" ToolTip="Select College Name" data-select2-enable="true"
                                                        AppendDataBoundItems="true" TabIndex="1" AutoPostBack="true"
                                                        OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCollege"
                                                        Display="None" ErrorMessage="Please Select College Name" ValidationGroup="PAuthority" SetFocusOnError="true"
                                                        InitialValue="0"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Department</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control" ToolTip="Select Department Name" data-select2-enable="true"
                                                        AppendDataBoundItems="true" TabIndex="2" AutoPostBack="true"
                                                        OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvdepartment" runat="server" ControlToValidate="ddlDepartment"
                                                        Display="None" ErrorMessage="Please Select Department" ValidationGroup="PAuthority" SetFocusOnError="true"
                                                        InitialValue="0"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Passing Authority</label>
                                                    </div>
                                                    <asp:TextBox ID="txtPAuthority" runat="server" MaxLength="50" CssClass="form-control"
                                                        ToolTip="Enter Passing Name" TabIndex="3" onkeypress="return CheckAlphabet(event,this);" />
                                                    <asp:RequiredFieldValidator ID="rfvPAuthority" runat="server" ControlToValidate="txtPAuthority"
                                                        Display="None" ErrorMessage="Please Enter Passing Authority" ValidationGroup="PAuthority"
                                                        SetFocusOnError="True">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>User</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlUser" runat="server" CssClass="form-control" ToolTip="Select User" data-select2-enable="true"
                                                        AppendDataBoundItems="true" TabIndex="4">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvUser" runat="server" ControlToValidate="ddlUser"
                                                        Display="None" ErrorMessage="Please Select User" ValidationGroup="PAuthority" SetFocusOnError="true"
                                                        InitialValue="0"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 btn-footer">
                                                <asp:Label ID="lblerror" SkinID="Errorlbl" runat="server"></asp:Label>
                                                <asp:Label ID="lblmsg" runat="server" SkinID="lblmsg"></asp:Label>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </asp:Panel>
                            <div class="col-12 btn-footer">
                                <asp:LinkButton ID="btnAdd" runat="server" SkinID="LinkAddNew" OnClick="btnAdd_Click" Text="Add New"
                                    CssClass="btn btn-primary" ToolTip="Click here to Add New Passing Authority" TabIndex="5"></asp:LinkButton>
                                <asp:Button ID="btnSave" runat="server" Text="Submit" ValidationGroup="PAuthority" OnClick="btnSave_Click"
                                CssClass="btn btn-primary" ToolTip="Click here to Submit" TabIndex="6" />
                                <asp:Button ID="btnBack" runat="server" Text="Back" CausesValidation="false" OnClick="btnBack_Click"
                                    CssClass="btn btn-primary" ToolTip="Click here to Return to Previous Menu" TabIndex="7" />
                                <asp:Button ID="btnShowReport" runat="server" Text="Show Report" CssClass="btn btn-info" ToolTip="Click here to Show Report"
                                    OnClick="btnShowReport_Click" TabIndex="8" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" ToolTip="Click here to Reset"
                                    OnClick="btnCancel_Click" CssClass="btn btn-warning" TabIndex="9" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="PAuthority"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />

                            </div>
                            <div class="col-12">
                                <asp:Panel ID="pnlList" runat="server">
                                    <asp:Repeater ID="lvPAuthority" runat="server">
                                        <HeaderTemplate>
                                            <div class="sub-heading">
                                                <h5>Passing Authority List</h5>
                                            </div>
                                            <table id="table2" class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Action
                                                        </th>
                                                        <th>College Name
                                                        </th>
                                                        <th>Passing Authority 
                                                        </th>
                                                        <th>User
                                                        </th>

                                                    </tr>
                                                </thead>
                                                <tbody>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("PANO") %>'
                                                        AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" TabIndex="9" />
                                               <%-- <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.gif" CommandArgument='<%# Eval("PANO") %>'
                                                    AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                                    OnClientClick="showConfirmDel(this); return false;" />--%>
                                                </td>
                                                <td>
                                                    <%# Eval("COLLEGE_NAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("PANAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("UA_FULLNAME")%>
                                                </td>

                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </tbody></table>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div id="divMsg" runat="server">
            </div>
        </ContentTemplate>

        <Triggers>
            <asp:PostBackTrigger ControlID="btnShowReport" />
            <asp:PostBackTrigger ControlID="btnSave" />
            <asp:PostBackTrigger ControlID="btnCancel" />
            <asp:PostBackTrigger ControlID="btnBack" />
            <asp:PostBackTrigger ControlID="ddlCollege" />
            <asp:PostBackTrigger ControlID="ddlDepartment" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
