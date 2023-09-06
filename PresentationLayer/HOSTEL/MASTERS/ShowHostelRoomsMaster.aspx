<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ShowHostelRoomsMaster.aspx.cs" Inherits="Hostel_ShowHostelRooms_Master" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style>
        .table-bordered > thead > tr > th {
            border-top: 1px solid #e5e5e5 !important;
        }
    </style>
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">SHOW HOSTEL ROOM MASTER</h3>
                </div>

                <div class="box-body">
                    <div class="col-12">
                        <div class="row">
                            <div class="col-12 col-md-12 col-lg-6">
                                <div class="row">
                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Session </label>
                                        </div>
                                        <asp:DropDownList ID="ddlSession" runat="server" CssClass="form-control" AppendDataBoundItems="True" data-select2-enable="true" TabIndex="1">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" ValidationGroup="submit"
                                            SetFocusOnError="True" InitialValue="0" />
                                    </div>

                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Hostel </label>
                                        </div>
                                        <asp:DropDownList ID="ddlHostelNo" runat="server" AutoPostBack="true" CssClass="form-control" AppendDataBoundItems="True"
                                            OnSelectedIndexChanged="ddlHostelNo_SelectedIndexChanged" data-select2-enable="true" TabIndex="2">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvHostel" runat="server" ControlToValidate="ddlHostelNo"
                                            Display="None" ErrorMessage="Please Select Hostel" ValidationGroup="submit"
                                            SetFocusOnError="True" InitialValue="0" />
                                    </div>

                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Degree </label>
                                        </div>
                                        <asp:Panel ID="pnlDegree" runat="server" TabIndex="3">
                                            <div class="form-group col-md-12 checkbox-list-box">

                                                <asp:CheckBox ID="chkDegree" runat="server" OnCheckedChanged="chkDegree_CheckedChanged" AutoPostBack="true" />
                                                Select All 
                                                <br />

                                                <asp:CheckBoxList ID="cblstDegree" runat="server" RepeatColumns="1"
                                                    RepeatDirection="Horizontal" Width="100%" CssClass="checkbox-list-style" ToolTip="Click to Select Degree">
                                                </asp:CheckBoxList>
                                            </div>
                                        </asp:Panel>

                                    </div>

                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Semester </label>
                                        </div>
                                        <asp:Panel ID="Panel2" runat="server" TabIndex="4">
                                            <div class="form-group col-md-12 checkbox-list-box">
                                                <asp:CheckBox ID="chkSem" runat="server" OnCheckedChanged="chkSem_CheckedChanged" AutoPostBack="true" />
                                                Select All
                                            <br />

                                                <asp:CheckBoxList ID="cblstSemester" RepeatDirection="Horizontal" Width="100%" CssClass="checkbox-list-style" RepeatColumns="5" runat="server" ToolTip="Click to Select Semester">
                                                </asp:CheckBoxList>
                                            </div>
                                        </asp:Panel>

                                    </div>

                                    <%-- Comment by shubham as per Discussion with Pankaj sir--%>

                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Gender </label>
                                        </div>
                                        <asp:RadioButtonList ID="cblgenderd" runat="server" AppendDataBoundItems="True" RepeatDirection="Horizontal" TabIndex="6">
                                            <asp:ListItem Text="Male"  Value="M"></asp:ListItem>
                                            <asp:ListItem Text="Female" Value="F"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>

                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                        <label> Block </label>
                                        <asp:DropDownList ID="ddlBlock" runat="server" CssClass="form-control" AppendDataBoundItems="True"
                                              data-select2-enable="true" TabIndex="7">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
<%--                                    <div class="form-group col-md-3" style="display: none;">
                                        <label><span style="color: red;">*</span> Block No.:</label>

                                    </div>--%>

                                    <div class="form-group col-md-3" style="display: none;">
                                        <label><span style="color: red;">*</span> Floor:</label>
                                        <asp:DropDownList ID="ddlFloor" runat="server" Width="30%" AppendDataBoundItems="true" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group col-md-3" style="display: none;">
                                        <label><span style="color: red;">*</span> Resident Type No.:</label>
                                        <asp:DropDownList ID="ddlResidentTypeNo" runat="server" Width="30%" AppendDataBoundItems="True" data-select2-enable="true">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-md-3" style="display: none;">
                                        <label><span style="color: red;">*</span> Branch:</label>
                                        <asp:DropDownList ID="ddlBranch" runat="server" Width="30%" AppendDataBoundItems="True" data-select2-enable="true">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-md-3" style="display: none;">
                                        <label><span style="color: red;">*</span> Start:</label>

                                        <asp:CheckBox ID="chkstart" Text="Yes" runat="server" />

                                    </div>
                                </div>

                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnShow" runat="server" Text="Show" OnClick="btnShow_Click"
                                        ValidationGroup="submit" CssClass="btn btn-primary" TabIndex="7" />

                                    <asp:Button ID="btnsubmit" runat="server" Text="Submit" Visible="false"
                                        ValidationGroup="submit" OnClick="btnsubmit_Click" CssClass="btn btn-primary" TabIndex="8" />

                                    <asp:Button ID="btnReport" runat="server" Text="Report" CssClass="btn btn-info" ValidationGroup="submit" OnClick="btnReport_Click" TabIndex="9" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" OnClick="btnCancel_Click"
                                        CausesValidation="false" TabIndex="10" />
                                    <asp:ValidationSummary ID="valSummary" runat="server" DisplayMode="List" ShowMessageBox="true"
                                        ShowSummary="false" ValidationGroup="submit" />

                                </div>

                                <asp:Panel ID="pnlStudent" runat="server">
                                    <asp:ListView ID="lvStudent" runat="server">
                                        <LayoutTemplate>
                                            <div id="listViewGrid">
                                                <div class="sub-heading">
                                                    <h5>Select Room</h5>
                                                </div>
                                                <div class="table-responsive mb-0" style="height: 200px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                                    <table class="table table-striped table-bordered nowrap" style="width: 100%" id="myTable">
                                                        <thead class="bg-light-blue" style="position: sticky; top: 0; background: #fff !important; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                            <tr>

                                                                <th width="10%">
                                                                    <asp:CheckBox ID="chkheader" runat="server" onclick="return totAll(this);" />
                                                                </th>
                                                                <th width="15%">Room Name
                                                                </th>
                                                                <th width="75%">Block Name
                                                                </th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </div>
                                        </LayoutTemplate>
                                        <EmptyDataTemplate>
                                        </EmptyDataTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:CheckBox ID="chkSelect" Checked='<%# (Eval("ISSELECT").ToString() == "1" ? true : false) %>' runat="server" onclick="totSubjects(this)" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblroomname" runat="server" Text='<%# Eval("ROOM_NAME")%>' ToolTip='<%# Eval("ROOM_NO")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblblockname" runat="server" Text='<%# Eval("BLOCK_NAME")%>'></asp:Label>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>

                                </asp:Panel>

                            </div>

                            <div class="col-12 col-md-12 col-lg-6">
                                <div class="sub-heading">
                                    <h5>Booking Start/Stop</h5>
                                </div>

                                <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto" Height="645px">
                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="btnsub" runat="server" CssClass="btn btn-primary" Text="Submit" OnClick="btnsub_Click" />
                                    </div>
                                    <asp:ListView ID="lvdetail" runat="server">
                                        <LayoutTemplate>
                                            <div id="listViewGrid" class="table-responsive">
                                                <table class="table table-striped table-bordered" style="width: 100%">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>Detail
                                                            </th>
                                                            <th>
                                                                <asp:CheckBox ID="chkStart" runat="server" onclick="return totAllStart(this);" /> Started
                                                            </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </tbody>
                                                </table>
                                            </div>
                                        </LayoutTemplate>
                                        <EmptyDataTemplate>
                                        </EmptyDataTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lbldetail" runat="server" Text='<%# Eval("DETAIL")%>' ToolTip='<%# Eval("DETAIL")%>'></asp:Label>
                                                    <asp:HiddenField ID="hidrecno" Value='<%# Eval("RECORD_NO")%>' runat="server" />
                                                    <asp:HiddenField ID="hidfamiler" Value='<%# Eval("FAMILER")%>' runat="server" />
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="chkstarted" Checked='<%# (Eval("STARTED").ToString() == "1" ? true : false) %>' runat="server" onclick="totSubjects(this)" />
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>

                                </asp:Panel>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div id="divMsg" runat="server">
    </div>
    <script language="javascript" type="text/javascript">
        function totAll(headchk) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];

                if (e.type == 'checkbox') {
                    ostr = e.name
                    var myChkName = ostr.split("$");
                    if (myChkName[4] == 'chkSelect' && (e.type == 'checkbox') && (!e.disabled)) {
                        if (headchk.checked == true) {
                            e.checked = true;
                        }
                        else {
                            e.checked = false;
                        }
                    }
                }
            }
        }

        function totAllStart(headchk) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];

                if (e.type == 'checkbox') {
                    ostr = e.name
                    var myChkName = ostr.split("$");
                    if (myChkName[4] == 'chkstarted' && (e.type == 'checkbox') && (!e.disabled)) {
                        if (headchk.checked == true) {
                            e.checked = true;
                        }
                        else {
                            e.checked = false;
                        }
                    }
                }
            }
        }
    </script>
</asp:Content>
