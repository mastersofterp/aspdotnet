<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="CreateEmailGroup.aspx.cs" Inherits="Itle_CreateEmailGroup" Title="" %>

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
    <asp:UpdatePanel ID="updpnl" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">SEND MAIL IN GROUP</h3>
                        </div>
                        <div class="box-body">
                            <asp:Panel ID="pnlEmailGroup" runat="server">
                                <div class="col-12">
                                    <asp:Panel ID="pnlCourse" runat="server">
                                        <div class="row">
                                           <%-- <div class="col-12">
                                                <div class="sub-heading">
                                                    <h5>Send Mail In Group Details</h5>
                                                </div>
                                            </div>--%>
                                            <%-- <div class="sub-heading">
                                               <h5>Send Mail In Group</h5>
                                              </div>--%>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Session</label>
                                                </div>
                                                <asp:DropDownList ID="ddlSession" CssClass="form-control" data-select2-enable="true" TabIndex="1"
                                                    runat="server" AppendDataBoundItems="true" ToolTip="Select Session"
                                                    AutoPostBack="true" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvddlSession" runat="server" ControlToValidate="ddlSession"
                                                    Display="None" InitialValue="0" ErrorMessage="Please Select Session" ValidationGroup="saveGroup">
                                                </asp:RequiredFieldValidator>
                                                <%--  <asp:DropDownList ID="ddlSession" Width="80%" runat="server" AppendDataBoundItems="true"
                                                                                     OnSelectedIndexChanged="ddlSession_SelectedIndexChanged" AutoPostBack="true">
                                                                                </asp:DropDownList>--%>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Select Course</label>
                                                </div>
                                                <asp:DropDownList ID="ddlCourse"  runat="server" AppendDataBoundItems="true"
                                                    OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged" ToolTip="Select Course"
                                                    AutoPostBack="true" CssClass="form-control" data-select2-enable="true" TabIndex="2">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvddlCourse" runat="server" ControlToValidate="ddlCourse"
                                                    Display="None" InitialValue="0" ErrorMessage="Please Select Course" ValidationGroup="saveGroup">
                                                </asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Group Name</label>
                                                </div>
                                                <asp:TextBox ID="txtGroupName" runat="server" CssClass="form-control" TabIndex="3"
                                                    ToolTip="Enter Group Name"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvGroupName" runat="server" ControlToValidate="txtGroupName"
                                                    Display="None" InitialValue="" ErrorMessage="Please Enter Group Name" ValidationGroup="saveGroup">
                                                </asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-12 btn-footer">
                                            <asp:Button ID="btnCreateGroup" runat="server" Text="Save" ValidationGroup="saveGroup" TabIndex="4"
                                                OnClick="btnCreateGroup_Click" CssClass="btn btn-primary" ToolTip="Clcik here to Save" />
                                              <input id="Button1" type="button" value="Close Window" class="btn btn-primary" tabindex="6"
                                                onclick="javascript: window.close();" />
                                             <asp:Button ID="btnreset" runat="server" Text="Reset" OnClick="btnreset_Click" TabIndex="5"
                                                CssClass="btn btn-warning" ToolTip="Click here to Reset" />
                                         
                                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="saveGroup"
                                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                        </div>
                                    </asp:Panel>
                                </div>
                                <div class="col-12">
                                    <asp:Panel ID="pnlGroupList" runat="server" ScrollBars="Auto">
                                        <asp:ListView ID="lvGroupList" runat="server">
                                            <LayoutTemplate>
                                                <div id="demo-grid">
                                                    <div class="sub-heading">
                                                        <h5>Group List</h5>
                                                    </div>
                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th>Action
                                                                </th>
                                                                <th>Group Name
                                                                </th>
                                                                <th>Course Name
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
                                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit1.gif"
                                                            CommandArgument='<%# Eval("EGROUPNO") %>'
                                                            ToolTip="Edit Record" AlternateText='<%# Eval("COURSENO")%>' OnClick="btnEdit_Click" />&nbsp;
                                                                    <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Images/delete.gif"
                                                                        CommandArgument='<%# Eval("EGROUPNO") %>'
                                                                        ToolTip="Delete Record" OnClick="btnDelete_Click" OnClientClick="showConfirmDel(this); return false;" />
                                                    </td>
                                                    <td>
                                                        <%# Eval("GROUPNAME")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("COURSE_NAME")%>
                        
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </asp:Panel>
                                </div>
                                <div class="col-12">
                                    <asp:Panel ID="pnlStudentList" runat="server">
                                        <fieldset id="fldtemplate" runat="server">
                                            <asp:ListView ID="lvStudents" runat="server" Visible="false" Style="height: 300px; overflow: auto">
                                                <LayoutTemplate>
                                                    <div class="sub-heading">
                                                        <h5>Student List</h5>
                                                    </div>
                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th>
                                                                    <input id="chkSelectAll" onclick="SelectAll(this);" type="checkbox" />&nbsp;&nbsp;&nbsp;Enrl.
                                                                                        No.
                                                                </th>
                                                                <th>Student Name
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
                                                            <asp:CheckBox ID="chkSelectMail" runat="server" ToolTip='<%# Eval("UA_NO")%>' />
                                                            <asp:HiddenField ID="hidUserName" runat="server" Value='<%# Eval("studname")%>' />
                                                            <asp:HiddenField ID="hidUserId" runat="server" Value='<%# Eval("UA_NO")%>' />
                                                            <%# Eval("REGNO")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("studname")%>
                                                        </td>

                                                    </tr>
                                                </ItemTemplate>
                                                <EmptyDataTemplate>
                                                    <p class="text-center text-bold">
                                                        No user contact found.<br />
                                                        <br />
                                                    </p>
                                                </EmptyDataTemplate>
                                            </asp:ListView>

                                        </fieldset>
                                    </asp:Panel>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
          
        </ContentTemplate>
    </asp:UpdatePanel>
   

    <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mdlPopupDel"
        runat="server" TargetControlID="div" PopupControlID="div" OkControlID="btnOkDel"
        OnOkScript="okDelClick();" CancelControlID="btnNoDel" OnCancelScript="cancelDelClick();"
        BackgroundCssClass="modalBackground" />
    <asp:Panel ID="div" runat="server" Style="display: none" CssClass="modalPopup">
        <div style="text-align: center">
            <table>
                <tr>
                    <td align="center">
                        <asp:Image ID="imgWarning" runat="server" ImageUrl="~/Images/warning.gif" AlternateText="Warning" />
                    </td>
                    <td>&nbsp;&nbsp;Are you sure you want to delete this record?
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
    <script language="javascript" type="text/javascript">
        function SelectAll(headchk) {
            var hdfTot = document.getElementById('<%= pnlStudentList.ClientID %>');

            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.type == 'checkbox') {
                    if (e.name.endsWith('chkSelectMail')) {
                        if (headchk.checked == true) {
                            e.checked = true;
                            hdfTot.value = Number(hdfTot.value) + 1;
                        }
                        else
                            e.checked = false;

                    }
                }
            }

            if (headchk.checked == false) hdfTot.value = "0";
        }

    </script>
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
</asp:Content>
