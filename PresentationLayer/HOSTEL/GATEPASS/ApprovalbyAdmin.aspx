<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ApprovalbyAdmin.aspx.cs" Inherits="HOSTEL_GATEPASS_ApprovalbyAdmin" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <style>
            ul.ui-autocomplete
            {
                max-height: 180px !important;
                overflow: auto !important;
            }
        </style>

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

        function formatNumber(input) {
            var value = input.value;
            if (value < 10) {
                input.value = '0' + value;
            }
        }
        </script>

    <meta charset="UTF-8">
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">Final Approval by Admin</h3>
                </div>
                <br />
                <div class="box-body">
                    <div class="col-12" id="finalAproval" runat="server">
                        <div class="row">
                        <div class="form-group col-lg-8 col-md-4 col-12">
                             <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Remark</label>
                                </div>
                                <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control" TextMode="MultiLine" TabIndex="1" Rows="1" Height="74px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvtxtRemark" runat="server" ErrorMessage="Please Enter Remark"
                                    Display="None" ControlToValidate="txtRemark" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                         </div>
                        </div>
                    </div>

                    <div class="col-12" id="changeApproval" runat="server" visible="false">
                        <div class="row">
                        <div class="form-group col-lg-6 col-md-4 col-12">
                             <div class="label-dynamic">
                                    <label>Student Name</label>
                                </div>
                                <asp:TextBox ID="txtName" runat="server" CssClass="form-control" TabIndex="2" Enabled="False"></asp:TextBox>
                         </div>
                        </div>

                        <div class="row">

                        <div class="form-group col-lg-4 col-md-4 col-12">
                             <div class="label-dynamic">
                                 <sup>* </sup>
                                    <label>First Approval</label>
                                </div>
                                <asp:DropDownList ID="ddlAA1" AppendDataBoundItems="true" runat="server" TabIndex="3" CssClass="form-control" data-select2-enable="true" AutoPostBack="True" OnSelectedIndexChanged="ddlAA1_SelectedIndexChanged" >
                                <asp:ListItem Value="0" Selected="True">Please Select</asp:ListItem>
                                </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvAA1" runat="server" ErrorMessage="Please Select First Approval"
                                    Display="None" ControlToValidate="ddlAA1" SetFocusOnError="True" ValidationGroup="AA"></asp:RequiredFieldValidator>
                         </div>

                        <div class="form-group col-lg-4 col-md-4 col-12">
                             <div class="label-dynamic">
                                 <sup>* </sup>
                                    <label>Second Approval</label>
                                </div>
                                <asp:DropDownList ID="ddlAA2" AppendDataBoundItems="true" runat="server" TabIndex="3" CssClass="form-control" data-select2-enable="true" AutoPostBack="True" OnSelectedIndexChanged="ddlAA2_SelectedIndexChanged" >
                                <asp:ListItem Value="0" Selected="True">Please Select</asp:ListItem>
                                </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvAA2" runat="server" ErrorMessage="Please Select Second Approval"
                                    Display="None" ControlToValidate="ddlAA2" SetFocusOnError="True" ValidationGroup="AA"></asp:RequiredFieldValidator>
                         </div>

                        <div class="form-group col-lg-4 col-md-4 col-12">
                             <div class="label-dynamic">
                                 <sup>* </sup>
                                    <label>Third Approval</label>
                                </div>
                                <asp:DropDownList ID="ddlAA3" AppendDataBoundItems="true" runat="server" TabIndex="3" CssClass="form-control" data-select2-enable="true" AutoPostBack="True" OnSelectedIndexChanged="ddlAA3_SelectedIndexChanged" >
                                <asp:ListItem Value="0" Selected="True">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvAA3" runat="server" ErrorMessage="Please Select Third Approval"
                                    Display="None" ControlToValidate="ddlAA3" SetFocusOnError="True" ValidationGroup="AA"></asp:RequiredFieldValidator>
                         </div>

                        </div>

                        <div class="row">
                        <div class="form-group col-lg-4 col-md-4 col-12">
                             <div class="label-dynamic">
                                 <%--<sup>* </sup>--%>
                                    <label>Fourth Approval</label>
                                </div>
                                <asp:DropDownList ID="ddlAA4" AppendDataBoundItems="true" runat="server" TabIndex="3" CssClass="form-control" data-select2-enable="true" AutoPostBack="True" OnSelectedIndexChanged="ddlAA4_SelectedIndexChanged" >
                                <asp:ListItem Value="0" Selected="True">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <%--<asp:RequiredFieldValidator ID="rfvAA4" runat="server" ErrorMessage="Please Select Fourth Approval"
                                    Display="None" ControlToValidate="ddlAA4" SetFocusOnError="True" ValidationGroup="AA"></asp:RequiredFieldValidator>--%>
                         </div>

                        <div class="form-group col-lg-4 col-md-4 col-12">
                            <label>Path</label>
                        <asp:TextBox ID="txtAAPath" runat="server" ReadOnly="true" TextMode="MultiLine"
                                                Rows="1" TabIndex="7" ToolTip="Path" Height="67px"></asp:TextBox>
                        </div>
                        </div>
                    </div>

                    <br /><br />
                    <div class="col-12 btn-footer">
                        <asp:Button ID="btnApprove" runat="server" Text="Approve" TabIndex="2" ValidationGroup="submit"
                            CssClass="btn btn-primary" OnClick="btnApprove_Click"/>
                        <asp:Button ID="btnChangeApproval" runat="server" Text="Change Approval" ValidationGroup="AA" 
                            CssClass="btn btn-outline-success" OnClick="btnChangeApproval_Click" />
                        <asp:Button ID="btnUpdatePath" runat="server" Text="Update Approval" ValidationGroup="AA" Visible="false"  
                            CssClass="btn btn-outline-success" OnClick="btnUpdatePath_Click" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" TabIndex="3"
                             CssClass="btn btn-warning" OnClick="btnCancel_Click" />

                        <asp:ValidationSummary ID="valSummary" runat="server" DisplayMode="List" ShowMessageBox="true"
                            ShowSummary="false" ValidationGroup="submit" />
                        <asp:ValidationSummary ID="valAA" runat="server" DisplayMode="List" ShowMessageBox="true"
                            ShowSummary="false" ValidationGroup="AA" />
                    </div>

                    <div class="col-12">
                        <asp:ListView  ID="lvGatePass" runat="server" OnItemDataBound="lvGatePass_ItemDataBound">
                            <LayoutTemplate>
                                <div class="sub-heading">
                                    <h5>List of Hostel Purposes</h5>
                                </div>
                                <table id="table2" class="table table-striped table-bordered nowrap display" style="width: 100%">
                                    <thead class="bg-light-blue">
                                        <tr>
                                            <th>
                                                <asp:CheckBox ID="chkAll" runat="server" onclick="return CheckAll(this);" /> Check All
                                            </th>
                                            <th>Student Name
                                            </th>
                                            <th>Out Date
                                            </th>
                                            <th>In Date
                                            </th>
                                            <th>Purpose
                                            </th>
                                            <th>Remarks
                                            </th>
                                            <th>
                                                Status
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody id="tblSearchResults">
                                            <tr id="itemPlaceholder" runat="server" />
                                        </tbody>
                                        <tbody>
                                            <tr id="Tr1" runat="server" />
                                        </tbody>
                                    </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="chkApprove" runat="server" onclick="totSubjects(this)" />
                                        <asp:HiddenField ID="hidrecid" runat="server" Value='<%# Eval("HGP_ID") %>' />
                                    </td>
                                    <td>
                                         <%# Eval("STUDNAME") %>
                                        
                                    </td>
                                    <td>
                                        <%# Eval("OUTDATE","{0:dd/MM/yyyy hh:mm tt}") %>
                                    </td>
                                    <td>
                                        <%# Eval("INDATE","{0:dd/MM/yyyy hh:mm tt}") %>
                                    </td>
                                    <td>
                                        <%# Eval("PURPOSE_NAME") %>
                                    </td>
                                    <td>
                                        <%# Eval("REMARKS") %>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("STATUS") %>' ForeColor='<%# Eval("STATUS").Equals("APPROVED")?System.Drawing.Color.Green:System.Drawing.Color.Red %>'></asp:Label>

                                    </td>
                                </tr>
                            </ItemTemplate>
                            <EmptyItemTemplate>
                                    -- Record Not Found --
                            </EmptyItemTemplate>
                        </asp:ListView>
                    </div>

                 

                </div>
            </div>
        </div>
    </div>
    <div id="divMsg" runat="server">
    </div>

        <script type="text/javascript">

            function CheckAll(headchk) {

                var frm = document.forms[0]
                for (i = 0; i < document.forms[0].elements.length; i++) {
                    var e = frm.elements[i];
                    if (e.type == 'checkbox') {
                        if (headchk.checked == true) {
                            e.checked = true;
                            headchk.checked == true;
                            document.getElementById('ctl00_ContentPlaceHolder1_lvGatePass_chkAll').checked = true;
                        }
                        else {
                            e.checked = false;
                            headchk.checked == false;
                            document.getElementById('ctl00_ContentPlaceHolder1_lvGatePass_chkAll').checked = false;
                        }
                    }
                }
            }
    </script>

</asp:Content>

<asp:Content ID="Content2" runat="server" contentplaceholderid="head">
    <style type="text/css">
        .form-control {}
    </style>
</asp:Content>



