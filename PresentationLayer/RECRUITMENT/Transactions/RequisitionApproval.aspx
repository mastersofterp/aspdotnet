﻿<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/SiteMasterPage.master" CodeFile="RequisitionApproval.aspx.cs" Inherits="RECRUITMENT_Transactions_RequisitionApproval" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style>
        .dataTables_scrollHeadInner
        {
            width: max-content!important;
        }
    </style>

    <script type="text/javascript" language="javascript">
        // Move an element directly on top of another element (and optionally
        // make it the same size)


        function totAllSubjects(headchk) {
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

    </script>


    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updPanel"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div id="preloader">
                    <div id="loader-img">
                        <div id="loader">
                        </div>
                        <p class="saving">Loading<span>.</span><span>.</span><span>.</span></p>
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>

    <asp:UpdatePanel ID="updPanel" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">Requisition Approval</h3>
                        </div>

                        <div class="box-body">
                            <asp:Panel ID="pnlAdd" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <%--<div class="col-12">
                                            <div class="sub-heading">
                                                <h5>Requisition Request</h5>
                                            </div>
                                        </div>--%>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Department</label>
                                            </div>
                                            <asp:DropDownList ID="ddlDepartment" runat="server" AppendDataBoundItems="true" CssClass="form-control" ToolTip="Select Department" data-select2-enable="true" AutoPostBack="true"
                                                OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" TabIndex="6">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvDepartment" runat="server" InitialValue="0" ControlToValidate="ddlDepartment"
                                                Display="None" ErrorMessage="Please Select Department" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <%--<sup>* </sup>--%>
                                                <label>Post Category</label>
                                            </div>
                                            <asp:DropDownList ID="ddlpostType" runat="server" AppendDataBoundItems="true" AutoPostBack="true" CssClass="form-control" ToolTip="Select Post Category" data-select2-enable="true"
                                                TabIndex="3">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                <asp:ListItem Value="1">Teaching</asp:ListItem>
                                                <asp:ListItem Value="2">Non Teaching</asp:ListItem>
                                            </asp:DropDownList>
                                            <%--<asp:RequiredFieldValidator ID="rfvPostType" runat="server" InitialValue="0" ControlToValidate="ddlpostType"
                                                Display="None" ErrorMessage="Please Select Post Type" ValidationGroup="submit"></asp:RequiredFieldValidator>--%>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <%--<sup>* </sup>--%>
                                                <label>Post</label>
                                            </div>
                                            <asp:DropDownList ID="ddlPost" runat="server" AppendDataBoundItems="true" AutoPostBack="true" CssClass="form-control" ToolTip="Select Post" data-select2-enable="true"
                                                TabIndex="3">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <%-- <asp:RequiredFieldValidator ID="rfvPost" runat="server" InitialValue="0" ControlToValidate="ddlPost"
                                                Display="None" ErrorMessage="Please Select Post" ValidationGroup="submit"></asp:RequiredFieldValidator>--%>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Filter</label>
                                            </div>
                                            <asp:DropDownList ID="ddlFilter" runat="server" AppendDataBoundItems="true" AutoPostBack="true" CssClass="form-control" ToolTip="Select Status of Filter" data-select2-enable="true"
                                                TabIndex="3">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                <asp:ListItem Value="1">Pending</asp:ListItem>
                                                <asp:ListItem Value="2">Approved</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvFilter" runat="server" InitialValue="0" ControlToValidate="ddlFilter"
                                                Display="None" ErrorMessage="Please Select Status for Filter List" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        </div>
                            </asp:Panel>

                            <div class="col-12 btn-footer">
                                <asp:Panel ID="pnlbtn" runat="server">
                                    <asp:Button ID="btnshow" runat="server" Text="Show" TabIndex="12" ToolTip="Click here to show Requisition Requests list" ValidationGroup="submit"
                                        OnClick="btnshow_Click" CssClass="btn btn-primary" />
                                    <asp:Button ID="btnApprove" runat="server" TabIndex="14" Text="Approve" ToolTip="Click here to Approve Pending Requisition Requests" ValidationGroup="submit"
                                        OnClick="btnApprove_Click" CssClass="btn btn-primary" />
                                    <asp:Button ID="btnCancel" runat="server" TabIndex="13" Text="Cancel" CausesValidation="false" ToolTip="Click here to Reset"
                                        OnClick="btnCancel_Click" CssClass="btn btn-warning" />

                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="submit"
                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                </asp:Panel>
                            </div>

                            <div class="col-12">
                                <asp:Panel ID="pnlPndRequiAppList" runat="server" Visible="false">
                                    <div class="sub-heading">
                                        <h5>Pending Requisition for Approval List</h5>
                                    </div>
                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="table2">
                                        <asp:Repeater ID="lvPendRequi" runat="server">
                                            <HeaderTemplate>
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>
                                                            <asp:CheckBox ID="chkAllEmployees" Checked="false" Text="Select All" Enabled="true"
                                                                runat="server" onclick="checkAllEmployees(this)" TabIndex="22" ToolTip="Select All Requisition Requests" /></th>
                                                        <th>Action
                                                        </th>
                                                        <th>Requisition No.
                                                        </th>
                                                        <th>Department
                                                        </th>
                                                        <th>Post Category
                                                        </th>
                                                        <th>Post
                                                        </th>
                                                        <th>Request Date
                                                        </th>
                                                        <th>No. of Position
                                                        </th>
                                                        <th>Remarks
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <asp:CheckBox ID="chkID" runat="server" Checked="false" Tag='lvItem' Text='<%#Eval("UA_FULLNAME")%>'
                                                            ToolTip='<%#Eval("REQ_ID")%>' TabIndex="23" />
                                                    </td>
                                                    <td>
                                                        <asp:Panel ID="pnlShowQA" runat="server" Style="cursor: pointer; vertical-align: top; float: left">
                                                            <asp:Image ID="imgExp" runat="server" ImageUrl="~/Images/action_down.png" TabIndex="1" />
                                                        </asp:Panel>
                                                    </td>
                                                    <td>
                                                        <%# Eval("REQUISITION_NO")%>                                                                 
                                                    </td>
                                                    <td>
                                                        <%# Eval("DEPT_NO")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("POSTTYPE")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("POST")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("REQUEST_DATE")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("NO_OF_POSITION")%>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtRemarks" runat="server" Tag='lvItem' ToolTip='Enter Remarks' CssClass="form-control" TabIndex="23" MaxLength="100" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="9" style="text-align: left; padding-left: 10px; padding-right: 10px">
                                                        <asp:Panel ID="pnlQues" runat="server" CssClass="collapsePanel">
                                                            <table class="table table-striped table-bordered nowrap">
                                                                <tr class="bg-light-blue">
                                                                    <th>Description</th>
                                                                </tr>
                                                                <tr>
                                                                    <td style="max-width: 500px; overflow: hidden; text-overflow: ellipsis; white-space: nowrap;">
                                                                        <p id='description<%# Eval("REQ_ID") %>'><%# Eval("DESCRIPTION") %></p>
                                                                        <button type="button" class="badge badge-primary" onclick="toggleDescription('<%# Eval("REQ_ID") %>')" data-item-id='<%# Eval("REQ_ID") %>'>
                                                                            Show More
                                                                        </button>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                                <ajaxToolKit:CollapsiblePanelExtender ID="cpeQA" runat="server" ExpandDirection="Vertical"
                                                    TargetControlID="pnlQues" ExpandControlID="pnlShowQA" CollapseControlID="pnlShowQA"
                                                    ExpandedText="Hide Description" CollapsedText="Show Description" CollapsedImage="~/Images/action_down.png"
                                                    ExpandedImage="~/images/action_up.png" ImageControlID="imgExp" Collapsed="true">
                                                </ajaxToolKit:CollapsiblePanelExtender>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </tbody>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </table>
                                </asp:Panel>
                            </div>

                            <div class="col-12">
                                <asp:Panel ID="pnlApprlist" runat="server" Visible="false">
                                    <div class="sub-heading">
                                        <h5>Approved Requisition Requests List</h5>
                                    </div>
                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="table1">
                                        <asp:Repeater ID="lvApprList" runat="server">
                                            <HeaderTemplate>
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <%--<th><asp:CheckBox ID="chkAllEmployees" Checked="false" Text="Select All" Enabled="true"
                                                            runat="server" onclick="checkAllEmployees(this)" TabIndex="22" ToolTip="Select All Requisition Requests" /></th>--%>
                                                        <th>Action
                                                        </th>
                                                        <th>Requisition No.
                                                        </th>
                                                        <th>Department
                                                        </th>
                                                        <th>Post Category
                                                        </th>
                                                        <th>Post
                                                        </th>
                                                        <th>Request Date
                                                        </th>
                                                        <th>No. of Position
                                                        </th>
                                                        <th>Status
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <%--<td>
                                                       <asp:CheckBox ID="chkID" runat="server" Checked="false" Tag='lvItem' Text='<%#Eval("UA_FULLNAME")%>'
                                                        ToolTip='<%#Eval("REQ_ID")%>' TabIndex="23" />
                                                    </td>--%>
                                                    <td>
                                                        <asp:Panel ID="pnlShowQA" runat="server" Style="cursor: pointer; vertical-align: top; float: left">
                                                            <asp:Image ID="imgExp" runat="server" ImageUrl="~/Images/action_down.png" TabIndex="1" />
                                                        </asp:Panel>
                                                    </td>
                                                    <td>
                                                        <%# Eval("REQUISITION_NO")%>                                                                 
                                                    </td>
                                                    <td>
                                                        <%# Eval("DEPT_NO")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("POSTTYPE")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("POST")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("REQUEST_DATE")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("NO_OF_POSITION")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("STATUS")%>
                                                        <%--<asp:TextBox ID="txtRemarks" runat="server" Tag='lvItem' ToolTip='Enter Remarks' CssClass="form-control" TabIndex="23" MaxLength="100" />--%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="9" style="text-align: left; padding-left: 10px; padding-right: 10px">
                                                        <asp:Panel ID="pnlQues" runat="server" CssClass="collapsePanel">
                                                            <table class="table table-striped table-bordered nowrap">
                                                                <tr class="bg-light-blue">
                                                                    <th>Description</th>
                                                                </tr>
                                                                <tr>
                                                                    <td style="max-width: 500px; overflow: hidden; text-overflow: ellipsis; white-space: nowrap;">
                                                                        <p id='description<%# Eval("REQ_ID") %>'><%# Eval("DESCRIPTION") %></p>
                                                                        <button type="button" class="badge badge-primary" onclick="toggleDescription('<%# Eval("REQ_ID") %>')" data-item-id='<%# Eval("REQ_ID") %>'>
                                                                            Show More
                                                                        </button>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                                <ajaxToolKit:CollapsiblePanelExtender ID="cpeQA" runat="server" ExpandDirection="Vertical"
                                                    TargetControlID="pnlQues" ExpandControlID="pnlShowQA" CollapseControlID="pnlShowQA"
                                                    ExpandedText="Hide Description" CollapsedText="Show Description" CollapsedImage="~/Images/action_down.png"
                                                    ExpandedImage="~/images/action_up.png" ImageControlID="imgExp" Collapsed="true">
                                                </ajaxToolKit:CollapsiblePanelExtender>
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
            </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <%-- <asp:PostBackTrigger ControlID="btnShowReport" />--%>
        </Triggers>
    </asp:UpdatePanel>

    <%--BELOW CODE IS TO SHOW THE MODAL POPUP EXTENDER FOR DELETE CONFIRMATION--%>
    <%--DONT CHANGE THE CODE BELOW. USE AS IT IS--%>

    <div id="divMsg" runat="server">
    </div>

    <asp:Panel ID="div" runat="server" Style="display: none" CssClass="modalPopup">
        <div style="text-align: center">
            <table>
                <tr>
                    <td align="center">
                        <asp:Image ID="imgWarning" runat="server" ImageUrl="~/images/warning.png" AlternateText="Warning" /></td>
                    <td>&nbsp;&nbsp;Are you sure you want to delete?
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:Button ID="btnOkDel" runat="server" Text="Yes" Width="50px" />
                        <asp:Button ID="btnNoDel" runat="server" Text="No" Width="50px" />
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
    <script type="text/javascript">
        //  keeps track of the delete button for the row
        //  that is going to be removed
        var _source;
        // keep track of the popup div
        var _popup;

        function showConfirmDel(source) {
            this._source = source;
            this._popup = $find('mdlPopupDel');

            //  find the confirm ModalPopup and show it    
            this._popup.show();
        }

        function okDelClick() {
            //  find the confirm ModalPopup and hide it    
            this._popup.hide();
            //  use the cached button as the postback source
            __doPostBack(this._source.name, '');
        }

        function cancelDelClick() {
            //  find the confirm ModalPopup and hide it 
            this._popup.hide();
            //  clear the event source
            this._source = null;
            this._popup = null;
        }

    </script>
    <script type="text/javascript" language="javascript">
        function validateNumeric(txt) {
            if (isNaN(txt.value)) {
                txt.value = txt.value.substring(0, (txt.value.length) - 1);
                txt.value = '';
                txt.focus = true;
                alert("Only Numeric Characters allowed !");
                return false;
            }
            else
                return true;
        }
    </script>
    <script type="text/javascript">
        function lettersOnly() {
            var charCode = event.keyCode;

            if ((charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123) || (charCode == 32) || (charCode == 8))

                return true;
            else
                return false;
            alert("Only Alphabets allowed");
        }

        function checkAllEmployees(chkcomplaint) {
            var frm = document.forms[0];
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.type == 'checkbox') {
                    if (chkcomplaint.checked == true)
                        e.checked = true;
                    else
                        e.checked = false;
                }
            }
        }
    </script>
    <script type="text/javascript">
        function toggleDescription(itemId) {
            var description = document.getElementById("description" + itemId);
            var button = document.querySelector("[data-item-id='" + itemId + "']");

            if (description && button) {
                if (description.style.whiteSpace === "nowrap" || window.getComputedStyle(description).whiteSpace === "nowrap") {
                    description.style.whiteSpace = "normal";
                    button.innerHTML = "Show Less";
                } else {
                    description.style.whiteSpace = "nowrap";
                    button.innerHTML = "Show More";
                }
            }
        }
    </script>
</asp:Content>