<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Movement.aspx.cs" Inherits="Dispatch_Transactions_Movement" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
  <%--  <script type="text/javascript">
        function RunThisAfterEachAsyncPostback() {
            RepeaterDiv();

        }
        function RepeaterDiv() {
            $(document).ready(function () {

                $(".display").dataTable({
                    "bJQueryUI": true,
                    "sPaginationType": "full_numbers"
                });
            });
        }
    </script>--%>

    <script src="../../Content/jquery.js" type="text/javascript"></script>

    <script src="../../Content/jquery.dataTables.js" language="javascript" type="text/javascript"></script>

    <script type="text/javascript">
        RunThisAfterEachAsyncPostback();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
    </script>


    <%--<asp:UpdatePanel ID="updActivity" runat="server">
        <ContentTemplate>--%>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">MOVEMENT ENTRY</h3>
                        </div>
                        <div>
                            <form role="form">
                                <div class="box-body">
                                    <div class="col-md-12" id="divPanel" runat="server">
                                        Note <b>:</b> <span style="color: #FF0000">* Marked Is Mandatory !</span>
                                        <asp:Panel ID="Panel1" runat="server">
                                            <div class="panel panel-info">
                                                <div class="panel panel-heading">General Details</div>
                                                <div class="panel panel-body">
                                                    <div class="form-group col-sm-12">
                                                        <div class="form-group col-sm-4">
                                                            <label><span style="color: red;"></span>Date :</label>
                                                            <div class="input-group date">
                                                                <div class="input-group-addon">
                                                                    <asp:Image ID="imgReceivedDT" runat="server" ImageUrl="~/images/calendar.png" CausesValidation="False" Style="cursor: pointer" />
                                                                </div>
                                                                <asp:TextBox ID="txtMovenmentDT" runat="server" MaxLength="10" CssClass="form-control" ToolTip="Select Date"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvReceivedDT" runat="server" ControlToValidate="txtMovenmentDT"
                                                                    Display="None" ErrorMessage="Please enter valid Received Date." SetFocusOnError="true"
                                                                    ValidationGroup="Submit" />
                                                                <ajaxToolKit:CalendarExtender ID="CeReceivedDT" runat="server" Enabled="true" EnableViewState="true"
                                                                    Format="dd/MM/yyyy" PopupButtonID="imgReceivedDT" TargetControlID="txtMovenmentDT">
                                                                </ajaxToolKit:CalendarExtender>
                                                            </div>
                                                        </div>
                                                        <div class="form-group col-sm-4">
                                                            <label><span style="color: red;">*</span>Department :</label>
                                                            <asp:DropDownList ID="ddlDepartment" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChange"
                                                                AppendDataBoundItems="true" CssClass="form-control" ToolTip="Select Department" />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlDepartment"
                                                                Display="None" InitialValue="0" ErrorMessage="Please Select Department" SetFocusOnError="true"
                                                                ValidationGroup="Submit" />
                                                        </div>
                                                        <div class="form-group col-sm-4">
                                                            <label><span style="color: red;"></span>Peon Name :</label>
                                                            <asp:TextBox ID="txtPeon" runat="server" CssClass="form-control" ToolTip="Enter Peon Name" onkeypress="return CheckAlphabet(event,this);"></asp:TextBox>
                                                        </div>
                                                        <div class="form-group col-sm-4">
                                                            <label><span style="color: red;"></span>Remark :</label>
                                                            <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control" ToolTip="Enter Remark" onkeypress="return CheckAlphabet(event,this);"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                        </asp:Panel>
                                    </div>


                                    <div class="col-md-12">
                                        <asp:Panel ID="Panel4" runat="server" ScrollBars="Auto" Height="300px">
                                            <asp:Repeater ID="reUser" runat="server">
                                                <HeaderTemplate>
                                                    <h4>User Details </h4>
                                                    <table class="table table-hover table-bordered">
                                                        <thead>
                                                            <tr class="bg-light-blue">
                                                                <th>
                                                                    <asp:CheckBox ID="cbHead" runat="server" />
                                                                </th>
                                                                <th>Name
                                                                </th>
                                                                <th>Branch Name
                                                                </th>
                                                            </tr>
                                                            <thead>
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </tbody>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td style="width: 10%; text-align: left">
                                                            <asp:CheckBox ID="chkId" runat="server" ToolTip='<%# Eval("UA_IDNO")%>' />
                                                            <asp:HiddenField ID="hfApplied" runat="server" Value='<%# Eval("STATUS") %>' />
                                                        </td>
                                                        <td align="center">
                                                            <%# Eval("UA_FULLNAME")%>
                                                        </td>
                                                        <td align="center">
                                                            <%# Eval("DEPTNAME")%>
                                                        </td>

                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    </tbody></table>
                                                </FooterTemplate>
                                            </asp:Repeater>
                                        </asp:Panel>
                                    </div>

                                    <div class="col-md-12">
                                        <asp:Panel ID="Panel2" runat="server" ScrollBars="Auto" Height="300px">
                                            <asp:Repeater ID="lvlinks" runat="server">
                                                <HeaderTemplate>
                                                    <h4>Letter Details</h4>
                                                    <table class="table table-hover table-bordered">
                                                        <thead>
                                                            <tr class="bg-light-blue">
                                                                <th>Action
                                                                </th>
                                                                <th>Disptach Reference No
                                                                </th>
                                                                <th>From User
                                                                </th>
                                                                <th>To User
                                                                </th>
                                                                <th>Subject
                                                                </th>
                                                                <th>City
                                                                </th>
                                                                <th>Received Date
                                                                </th>
                                                                <th>Status
                                                                </th>
                                                            </tr>
                                                            <thead>
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </tbody>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <asp:CheckBox ID="chkLNo" runat="server" ToolTip='<%# Eval("IOTRANNO")%>' />
                                                        </td>
                                                        <td>
                                                            <asp:LinkButton ID="lbtnCentralRefNo" Text='<%# Eval("CENTRALREFERENCENO") %>' OnClick="lbtnCentralRefNo_Click"
                                                                SkinID="LinkAddNew" runat="server" CommandName='<% Eval("CENTRALREFERENCENO") %>'
                                                                CommandArgument='<%# Eval("CENTRALREFERENCENO")%>' Enabled="false"></asp:LinkButton>
                                                        <td>
                                                            <%# Eval("IOFROM") %>
                                                        </td>
                                                        <td>
                                                            <%# Eval("TOUSER") %>
                                                        </td>
                                                        <td>
                                                            <%# Eval("SUBJECT")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("CITY") %>
                                                        </td>
                                                        <td>
                                                            <%# Eval("CENTRALRECSENTDT","{0:dd-MMM-yyyy}") %>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("STATUS") %>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    </tbody></table>
                                                </FooterTemplate>
                                            </asp:Repeater>
                                        </asp:Panel>
                                    </div>

                                    <div class="box-footer" id="divAddTo" runat="server">
                                        <p class="text-center">
                                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="Submit" OnClick="btnSubmit_Click" CssClass="btn btn-primary" ToolTip="Click here to Submit" />
                                            <asp:Button ID="btnViewAll" runat="server" Text="View All Letter" ValidationGroup="Submit" OnClick="btnViewAll_Click" CssClass="btn btn-info" ToolTip="Click here to View" />
                                            &nbsp;<asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" ToolTip="Click here to Cancel" OnClick="btnCancel_Click" />
                                            <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Submit" />
                                        </p>
                                    </div>
                                </div>
                            </form>
                        </div>
                        </div>
                </div>
            </div>
    <%--    </ContentTemplate>
    </asp:UpdatePanel>--%>

    <script type="text/javascript" language="javascript">
        function totAllIDs(headchk) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.type == 'checkbox') {
                    if (headchk.checked == true)
                        e.checked = true;
                    else
                        e.checked = false;
                }
            }

        }
        function validateNumeric(txt) {
            if (isNaN(txt.value)) {
                txt.value = '';
                alert('Only Numeric Characters Allowed!');
                txt.focus();
                return;
            }
        }
        function toggleExpansion(imageCtl, divId) {
            if (document.getElementById(divId).style.display == "block") {
                document.getElementById(divId).style.display = "none";
                imageCtl.src = "../../IMAGES/expand_blue.jpg";
            } //../images/action_up.gif
            else if (document.getElementById(divId).style.display == "none") {
                document.getElementById(divId).style.display = "block";
                imageCtl.src = "../../IMAGES/collapse_blue.jpg";
            }
        }

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
</asp:Content>

