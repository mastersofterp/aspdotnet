<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Leave_Name_Short.aspx.cs" Inherits="ESTABLISHMENT_LEAVES_Master_Leave_Name_Short" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="https://cdn.datatables.net/1.10.4/js/jquery.dataTables.min.js"></script>

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

    <%--  <script type="text/javascript">
        RunThisAfterEachAsyncPostback();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
    </script>--%>

    <asp:UpdatePanel ID="updAll" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">LEAVE NAME ENTRY</h3>
                        </div>
                        <div class="box-body">
                            <asp:Panel ID="pnlAdd" runat="server">
                                <div class="panel panel-info">
                                    <div class="col-12">
                                        <div class="row">
                                            <div class="col-12">
                                                <div class="sub-heading">
                                                    <h5>Add/Edit Leave Type</h5>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-12">
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Leave Name</label>
                                                </div>
                                                <asp:TextBox ID="txtleavename" runat="server" MaxLength="50" CssClass="form-control"
                                                    ToolTip="Enter Leave Name" TabIndex="1" onkeypress="return CheckAlphabet(event,this);" />
                                                <asp:RequiredFieldValidator ID="rfvHolyType" runat="server" ControlToValidate="txtleavename"
                                                    Display="None" ErrorMessage="Please Enter Leave Name" ValidationGroup="LeaveName"
                                                    SetFocusOnError="True">
                                                </asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Short Name</label>
                                                </div>
                                                <asp:TextBox ID="txtshortname" runat="server" MaxLength="10" ToolTip="Enter Short Name"
                                                    CssClass="form-control" TabIndex="2" onkeypress="return CheckAlphabet(event,this);" />
                                                <asp:RequiredFieldValidator ID="rfvshrtname" runat="server" ControlToValidate="txtshortname"
                                                    Display="None" ErrorMessage="Please Enter Short Name" ValidationGroup="LeaveName"
                                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Max Days</label>
                                                </div>
                                                <asp:TextBox ID="txtmaxday" runat="server" MaxLength="5" onkeypress="return CheckNumeric(event,this);" CssClass="form-control" ToolTip="Enter Maximum Days"
                                                    TabIndex="3" />
                                                <asp:RequiredFieldValidator ID="rfvmaxdays" runat="server" ControlToValidate="txtmaxday"
                                                    Display="None" ErrorMessage="Please Enter Max Days" ValidationGroup="LeaveName"
                                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="ftbefenergyFixedCharge" runat="server" TargetControlID="txtmaxday"
                                                    ValidChars="0123456789." Enabled="True" FilterType="Numbers">
                                                </ajaxToolKit:FilteredTextBoxExtender>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Period</label>
                                                </div>
                                                <asp:RadioButtonList ID="rdbYearly" runat="server" RepeatDirection="Horizontal"
                                                    AutoPostBack="true" ToolTip="Select Leave Period" TabIndex="4">
                                                    <asp:ListItem Enabled="true" Text="&nbsp;&nbsp;Yearly&nbsp;&nbsp;" Value="Max_Days">&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                                    <asp:ListItem Enabled="true" Text="&nbsp;&nbsp;Half Yearly&nbsp;&nbsp;" Value="Yearly"></asp:ListItem>
                                                    <asp:ListItem Enabled="true" Text="&nbsp;&nbsp;Service Base&nbsp;&nbsp;" Value="ServiceBase"></asp:ListItem>
                                                </asp:RadioButtonList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="rdbYearly"
                                                    Display="None" ErrorMessage="Please Select Period" ValidationGroup="LeaveName"
                                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>Is Pay Leave</label>
                                                </div>
                                                <asp:CheckBox ID="chkIsPayLeave" runat="server" Checked="false" TabIndex="5" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>Is Certificate</label>
                                                </div>
                                                <asp:CheckBox ID="chkIsCertificate" runat="server" Checked="false" TabIndex="6" AutoPostBack="true" OnCheckedChanged="chkIsCertificate_CheckedChanged" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12" id="divNoofdaycertifi" runat="server" visible="false">
                                                <div class="label-dynamic">
                                                    <label>No. Of Days for Certificate</label>
                                                </div>

                                                <asp:TextBox ID="txtCertificatedays" runat="server" MaxLength="2" onkeypress="return CheckNumeric(event,this);" CssClass="form-control" ToolTip="Enter No.of Days for Certificated"
                                                    TabIndex="7" />
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>Include Prefix/Suffix/Holiday</label>
                                                </div>
                                                <asp:CheckBox ID="chkPrefix" runat="server" Checked="false" TabIndex="8" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>Is Comp-Off</label>
                                                </div>
                                                <asp:CheckBox ID="chkComp" runat="server" Checked="false" TabIndex="9" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-12">
                                        <asp:Label ID="lblerror" SkinID="Errorlbl" runat="server"></asp:Label>
                                        <asp:Label ID="lblmsg" runat="server" SkinID="lblmsg"></asp:Label>
                                    </div>
                                </div>
                            </asp:Panel>
                            <div class="col-12 btn-footer">
                                <asp:LinkButton ID="btnAdd" runat="server" SkinID="LinkAddNew" OnClick="btnAdd_Click" TabIndex="7"
                                    CssClass="btn btn-primary" ToolTip="Click here to Add New Leave Type" Text="Add New"></asp:LinkButton>
                                <asp:Button ID="btnSave" runat="server" Text="Submit" ValidationGroup="LeaveName" TabIndex="10"
                                    CssClass="btn btn-primary" ToolTip="Click here to Submit" OnClick="btnSave_Click" />
                                <asp:Button ID="btnBack" runat="server" Text="Back" CausesValidation="false" OnClick="btnBack_Click"
                                    CssClass="btn btn-primary" ToolTip="Click here to Return to Previous Menu" TabIndex="11" />
                                <asp:Button ID="btnShowReport" runat="server" Text="Show Report" CssClass="btn btn-info" ToolTip="Click here to Show Report"
                                    Visible="false" OnClick="btnShowReport_Click" TabIndex="12" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" ToolTip="Click here to Reset"
                                    OnClick="btnCancel_Click" CssClass="btn btn-warning" TabIndex="13" />

                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="LeaveName"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                            </div>
                            <asp:Panel ID="pnlList" runat="server">
                                <div class="col-12">
                                    <asp:Repeater ID="lvLeaveName" runat="server">
                                        <HeaderTemplate>
                                            <div class="sub-heading">
                                                <h5>Leave Type Details</h5>
                                            </div>
                                            <table id="table2" class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Action
                                                        </th>
                                                        <th>Leave Name
                                                        </th>
                                                        <th>Leave Short Name
                                                        </th>
                                                        <th>Max Days
                                                        </th>
                                                        <th>Yearly/Half
                                                        </th>

                                                    </tr>
                                                </thead>
                                                <tbody>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("LVNO") %>'
                                                        AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" TabIndex="12" />
                                                    <%-- <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.gif" CommandArgument='<%# Eval("PANO") %>'
                                                    AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                                    OnClientClick="showConfirmDel(this); return false;" />--%>
                                                </td>
                                                <td>
                                                    <%# Eval("Leave_Name")%>
                                                </td>
                                                <td>
                                                    <%#Eval("Leave_ShortName")%>
                                                </td>
                                                <td>
                                                    <%#Eval("Max_Days")%>
                                                </td>
                                                <td>
                                                    <%#Eval("Yearly")%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </tbody></table>
                                        </FooterTemplate>
                                    </asp:Repeater>

                                    <%--    <asp:ListView ID="lvLeaveName" runat="server">
                                        <EmptyDataTemplate>
                                            <br />
                                            <p class="text-center text-bold">
                                                <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="Click Add New To Enter Leave Type" />
                                            </p>
                                        </EmptyDataTemplate>
                                        <LayoutTemplate>
                                            <div id="lgv1">
                                                <h4 class="box-title">Leave Type Details
                                                </h4>
                                                <table class="table table-bordered table-hover">
                                                    <thead>
                                                        <tr class="bg-light-blue">
                                                            <th>Action
                                                            </th>
                                                            <th>Leave Name
                                                            </th>
                                                            <th>Leave Short Name
                                                            </th>
                                                            <th>Max Days
                                                            </th>
                                                            <th>Yearly/Half
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
                                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("LVNO") %>'
                                                        AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />&nbsp;
                                              
                                                </td>
                                                <td>
                                                    <%# Eval("Leave_Name")%>
                                                </td>
                                                <td>
                                                    <%#Eval("Leave_ShortName")%>
                                                </td>
                                                <td>
                                                    <%#Eval("Max_Days")%>
                                                </td>
                                                <td>
                                                    <%#Eval("Yearly")%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>--%>

                                    <%-- <div class="vista-grid_datapager">
                                        <div class="text-center">
                                            <asp:DataPager ID="dpPager" runat="server" PagedControlID="lvLeaveName" PageSize="10"
                                                OnPreRender="dpPager_PreRender">
                                                <Fields>
                                                    <asp:NextPreviousPagerField FirstPageText="<<" PreviousPageText="<" ButtonType="Link"
                                                        RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="true" ShowPreviousPageButton="true"
                                                        ShowLastPageButton="false" ShowNextPageButton="false" />
                                                    <asp:NumericPagerField ButtonType="Link" ButtonCount="7" CurrentPageLabelCssClass="Current" />
                                                    <asp:NextPreviousPagerField LastPageText=">>" NextPageText=">" ButtonType="Link"
                                                        RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="false" ShowPreviousPageButton="false"
                                                        ShowLastPageButton="true" ShowNextPageButton="true" />

                                                </Fields>
                                            </asp:DataPager>
                                        </div>
                                    </div>--%>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnBack" />
            <asp:PostBackTrigger ControlID="btnSave" />
            <asp:PostBackTrigger ControlID="btnCancel" />
        </Triggers>
    </asp:UpdatePanel>

    <%--  Enable the button so it can be played again --%>


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
    </script>



</asp:Content>


