<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="HostelInchargeEntry.aspx.cs"
    Inherits="HOSTEL_MASTERS_HostelInchargeEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%-- <script type="text/javascript">
        //On Page Load
        $(document).ready(function () {
            $('#table2').DataTable();
        });
    </script>--%><script type="text/javascript">
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
    <asp:UpdatePanel ID="UpdAtten" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">Attendance Incharge Entry</h3>
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
                                            ValidationGroup="Submit" ToolTip="Hostel Name" AutoPostBack="True" OnSelectedIndexChanged="ddlHostelName_SelectedIndexChanged" />
                                        <asp:RequiredFieldValidator ID="rfvHostelName" runat="server" ControlToValidate="ddlHostelName"
                                            Display="None" ErrorMessage="Please Select Hostel" ValidationGroup="Submit" SetFocusOnError="True"
                                            InitialValue="0" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlHostelName"
                                            Display="None" ErrorMessage="Please Select Hostel" ValidationGroup="Report" SetFocusOnError="True"
                                            InitialValue="0" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Block Name </label>
                                        </div>
                                        <asp:Panel ID="pnlblock" runat="server" TabIndex="3">
                                            <div class="form-group col-md-12 checkbox-list-box">

                                                <asp:CheckBox ID="chkBlock" runat="server" AutoPostBack="true" OnCheckedChanged="chkBlock_CheckedChanged" />
                                                Select All 
                                                <br />

                                                <asp:CheckBoxList ID="cblstBlock" runat="server" RepeatColumns="1" OnSelectedIndexChanged="cblstBlock_SelectedIndexChanged"
                                                    RepeatDirection="Horizontal" Width="100%" CssClass="checkbox-list-style" ToolTip="Click to Select Block Name" AutoPostBack="true">
                                                </asp:CheckBoxList>

                                            </div>
                                        </asp:Panel>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Block Name</label>
                                        </div>
                                        <asp:DropDownList ID="ddlBlock" runat="server" TabIndex="1" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true"
                                            ValidationGroup="Submit" ToolTip="Block Name" AutoPostBack="True" OnSelectedIndexChanged="ddlBlock_SelectedIndexChanged" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlBlock"
                                            Display="None" ErrorMessage="Please Select Block" ValidationGroup="Submit" SetFocusOnError="True"
                                            InitialValue="0" />
                                    </div>
                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Floor </label>
                                        </div>
                                        <asp:Panel ID="Panel2" runat="server" TabIndex="4">
                                            <div class="form-group col-md-12 checkbox-list-box">
                                                <asp:CheckBox ID="chkFloor" runat="server" AutoPostBack="true" OnCheckedChanged="chkFloor_CheckedChanged" />
                                                Select All
                                            <br />

                                                <asp:CheckBoxList ID="cblstFloor" RepeatDirection="Horizontal" Width="100%" CssClass="checkbox-list-style" RepeatColumns="5"
                                                    runat="server" ToolTip="Click to Select Floor">
                                                </asp:CheckBoxList>

                                            </div>
                                        </asp:Panel>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Incharge Name</label>
                                        </div>
                                        <asp:DropDownList ID="ddlIncharge" runat="server" TabIndex="4" AppendDataBoundItems="True" CssClass="form-control"
                                            data-select2-enable="true"
                                            ValidationGroup="Submit" ToolTip="Incharge Name" AutoPostBack="True" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlIncharge"
                                            Display="None" ErrorMessage="Please Select Incharge Name" ValidationGroup="Submit" SetFocusOnError="True"
                                            InitialValue="0" />
                                    </div>

                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSave" runat="server" Text="Submit" ToolTip="Submit" ValidationGroup="Submit" OnClick="btnSave_Click"
                                    CssClass="btn btn-primary" TabIndex="5" />
                                <asp:Button ID="btnReport" runat="server" Text="Report" ToolTip="Get Report" ValidationGroup="Report" OnClick="btnReport_Click"
                                    CssClass="btn btn-primary" TabIndex="6" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false"
                                    CssClass="btn btn-warning" TabIndex="6" OnClick="btnCancel_Click" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Submit"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="Report"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                            </div>

                            <div class="col-12">
                                <asp:Repeater ID="lvIncharge" runat="server">
                                    <HeaderTemplate>
                                        <div class="sub-heading">
                                            <h5>List of Attendance Incharge</h5>
                                        </div>
                                        <table id="table2" class="table table-striped table-bordered nowrap display" style="width: 100%">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>Action
                                                    </th>
                                                    <th>INcharge Name
                                                    </th>
                                                    <th>Hostel Name
                                                    </th>
                                                    <th>Block Name
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
                                                <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" OnClick="btnEdit_Click"
                                                    AlternateText="Edit Record" ToolTip="Edit Record" TabIndex="7" CommandArgument='<%# Eval("INCHARGE_ID") %>' />&nbsp; 
                                    
                                        <%--   <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.gif" CommandArgument='<%# Eval("HNO") %>'
                                                    AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                                    OnClientClick="showConfirmDel(this); return false;" />--%>
                                            </td>
                                            <td>
                                                <%# Eval("UA_NAME")%>
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
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnReport" />

        </Triggers>
    </asp:UpdatePanel>
    <div id="divMsg" runat="server">
    </div>

</asp:Content>

