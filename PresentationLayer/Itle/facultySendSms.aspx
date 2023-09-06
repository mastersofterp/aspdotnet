<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="facultySendSms.aspx.cs" Inherits="facultySendSms" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        function SelectAll(chkAll) {
            try {
                var tbl;
                var lvName = '';

                tbl = document.getElementById('tblStudentList');
                lvName = 'lvStudents';

                if (tbl != null && tbl.rows && tbl.rows.length > 0) {
                    for (i = 1; i < tbl.rows.length; i++) {

                        var chk = document.getElementById('ctl00_ContentPlaceHolder1_lvStudents_ctrl' + (i - 1).toString() + '_chkSelectMail');
                        if (chk != null) {
                            chk.checked = chkAll.checked;
                        }
                    }
                }
            }
            catch (ex) {
            }
        }
    </script>
    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updpnl"
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

    <asp:UpdatePanel ID="updpnl" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">SEND SMS</h3>
                        </div>
                        <div class="box-body">
                            <asp:Panel ID="pnlFacultySms" runat="server">
                                <div class="col-12">
                                    <asp:Panel ID="pnlCourse" runat="server">
                                        <div class="row">

                                            <div class="form-group col-lg-3 col-md-6 col-12" id="trSession" runat="server" visible="false">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label></label>
                                                </div>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>Session</label>
                                                </div>
                                                <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="true" AutoPostBack="true"
                                                    OnSelectedIndexChanged="ddlSession_SelectedIndexChanged" ToolTip="Select Session"
                                                    CssClass="form-control" data-select2-enable="true">
                                                </asp:DropDownList>
                                                <%--  <asp:DropDownList ID="ddlSession" Width="80%" runat="server" AppendDataBoundItems="true"
                                                                    OnSelectedIndexChanged="ddlSession_SelectedIndexChanged" AutoPostBack="true">
                                                                </asp:DropDownList>--%>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Select Course</label>
                                                </div>
                                                <asp:DropDownList ID="ddlCourse" runat="server" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged"
                                                    AutoPostBack="true" CssClass="form-control" data-select2-enable="true" ToolTip="Select Course">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-group col-lg-6 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Write Template to Send</label>
                                                </div>
                                                <asp:TextBox ID="txtDesiTemp" runat="server" TextMode="MultiLine" MaxLength="132" CssClass="form-control"
                                                    ToolTip="Enter Write Template to Send">
                                                </asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-12 btn-footer">
                                            <asp:Button ID="btnSendSms" runat="server" Text="Send SMS" CssClass="btn btn-primary"
                                                OnClick="btnSendSms_Click" ToolTip="Click here to Send SMS" />
                                            <asp:Button ID="btnreset" runat="server" Text="Reset" CssClass="btn btn-warning"
                                                OnClick="btnreset_Click" ToolTip="Click here to Reset" />
                                        </div>

                                    </asp:Panel>
                                </div>
                                <div class="col-12">
                                    <asp:Panel ID="fldtemplate" runat="server">
                                        <asp:ListView ID="lvStudents" runat="server" Visible="false" OnItemDataBound="lvStudents_ItemDataBound">
                                            <LayoutTemplate>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="tblStudentList">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>
                                                                <input id="chkSelectAll" onclick="SelectAll(this);" type="checkbox" />&nbsp;&nbsp;&nbsp;Enrl. 
                                                                    No
                                                            </th>
                                                            <th>Student Name
                                                            </th>
                                                            <th>Mobile Number
                                                            </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </tbody>
                                                </table>

                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <asp:CheckBox ID="chkSelectMail" runat="server" />
                                                        <asp:HiddenField ID="hidUserName" runat="server" Value='<%# Eval("studname")%>' />
                                                        <asp:HiddenField ID="hidUserId" runat="server" Value='<%# Eval("IDNO")%>' />
                                                        <%# Eval("REGNO")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("studname")%>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblstudmobile" runat="server" Text='<%# Eval("studentmobile")%>'> </asp:Label>

                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <EmptyDataTemplate>
                                                <p class="text-bold text-center">
                                                    No user contact found.<br />
                                                    <br />
                                                </p>
                                            </EmptyDataTemplate>
                                        </asp:ListView>
                                    </asp:Panel>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>

