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
                    <div class="col-12">

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
                    <br /><br />
                    <div class="col-12 btn-footer">
                        <asp:Button ID="btnApprove" runat="server" Text="Approve" TabIndex="2" ValidationGroup="submit"
                            CssClass="btn btn-primary" OnClick="btnApprove_Click"/>
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" TabIndex="3"
                             CssClass="btn btn-warning" OnClick="btnCancel_Click" />

                        <asp:ValidationSummary ID="valSummary" runat="server" DisplayMode="List" ShowMessageBox="true"
                            ShowSummary="false" ValidationGroup="submit" />
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
                                                <%--<asp:CheckBox ID="chkAll" runat="server" onclick="return CheckAll(this);" />--%> Check 
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
                                        <%# Eval("OUTDATE ") %>
                                    </td>
                                    <td>
                                        <%# Eval("INDATE ") %>
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

        <script type="text/javascript" language="javascript">

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



