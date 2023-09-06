<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="PassingAuthority.aspx.cs" Inherits="Sports_Masters_PassingAuthority" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:UpdatePanel ID="updAdd" runat="server">
        <ContentTemplate>
            <div class="col-md-12">
                <div class="box box-primary">
                    <div id="div1" runat="server"></div>
                    <div class="box-header with-border">
                        <h3 class="box-title">APPROVAL AUTHORITY</h3>
                    </div>
                    <div>
                        <form role="form">
                            <div class="box-body">
                                Note <b>:</b> <span style="color: #FF0000">* Marked Is Mandatory !</span>

                                <div class="col-md-12" id="divButtons" runat="server">
                                    <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" Text="Add New" CssClass="btn btn-primary" ToolTip="Click to Add New" />
                                    <asp:Button ID="btnShowReport" runat="server" Text="Report" CssClass="btn btn-info" ToolTip="Click to get Report" OnClick="btnShowReport_Click" />
                                </div>
                                
                                <div class="col-md-12">
                                    <br />                              
                                    <asp:Panel ID="pnlList" runat="server" ScrollBars="Auto">
                                        <asp:ListView ID="lvPAuthority" runat="server">
                                            <LayoutTemplate>
                                                <div id="lgv1">
                                                    <h4 class="box-title">Approval Authority List
                                                    </h4>
                                                    <table class="table table-bordered table-hover">
                                                        <thead>
                                                            <tr class="bg-light-blue">
                                                                <th>Action
                                                                </th>
                                                                <th>Institute
                                                                </th>
                                                                <th>Approval Authority 
                                                                </th>
                                                                <th>User
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
                                                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("PANO") %>'
                                                            AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />&nbsp;
                                                                                           <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/images/delete.gif" CommandArgument='<%# Eval("PANO") %>'
                                                                                               AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                                                                               OnClientClick="showConfirmDel(this); return false;" />
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
                                        </asp:ListView>
                                    </asp:Panel>
                                </div>
                                <div class="col-md-12">
                                    <asp:Panel ID="pnlAdd" runat="server">
                                        <div class="panel panel-info">
                                            <div class="panel panel-heading">Add/Edit Approval Authority</div>
                                            <div class="panel panel-body">
                                                <div class="form-group col-md-4">
                                                    <label><span style="color: #FF0000">*</span>Institute :</label>
                                                    <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control" ToolTip="Select Institute" AppendDataBoundItems="true" TabIndex="1" AutoPostBack="true"
                                                        OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged"><asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCollege"
                                                        Display="None" ErrorMessage="Please Select Institute" ValidationGroup="PAuthority" SetFocusOnError="true"
                                                        InitialValue="0"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-md-4">
                                                    <label><span style="color: #FF0000">*</span>Approval Authority :</label>
                                                    <asp:TextBox ID="txtPAuthority" runat="server" MaxLength="50" CssClass="form-control" ToolTip="Enter Approval Authority" TabIndex="2" />
                                                    <asp:RequiredFieldValidator ID="rfvPAuthority" runat="server" ControlToValidate="txtPAuthority"
                                                        Display="None" ErrorMessage="Please Enter Approval Authority" ValidationGroup="PAuthority"
                                                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-md-4">
                                                    <label><span style="color: #FF0000">*</span>User :</label>
                                                    <asp:DropDownList ID="ddlUser" runat="server" CssClass="form-control" ToolTip="Select User" AppendDataBoundItems="true" TabIndex="3">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvUser" runat="server" ControlToValidate="ddlUser"
                                                        Display="None" ErrorMessage="Please Select User" ValidationGroup="PAuthority" SetFocusOnError="true" InitialValue="0"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </div>
                            </div>
                        </form>
                    </div>
                    <div class="box-footer" id="divButtonFooter" runat="server" visible="false">
                        <p class="text-center">
                            <asp:Button ID="btnSave" runat="server" Text="Submit" ValidationGroup="PAuthority" OnClick="btnSave_Click" TabIndex="4" CssClass="btn btn-primary" ToolTip="Click here to Submit" />&nbsp;                                               
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" TabIndex="5" OnClick="btnCancel_Click" CssClass="btn btn-warning" ToolTip="Click here to Cancel" />&nbsp;
                            <asp:Button ID="btnBack" runat="server" Text="Back" CausesValidation="false" OnClick="btnBack_Click" TabIndex="6" CssClass="btn btn-primary" ToolTip="Click to Back" />
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="PAuthority" ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                        </p>                       
                    </div>
                </div>
            </div>
            </div> 
        </ContentTemplate>
    </asp:UpdatePanel>


    <%--BELOW CODE IS TO SHOW THE MODAL POPUP EXTENDER FOR DELETE CONFIRMATION--%>
    <%--DONT CHANGE THE CODE BELOW. USE AS IT IS--%>
    <ajaxToolKit:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mdlPopupDel"
        runat="server" TargetControlID="div" PopupControlID="div" OkControlID="btnOkDel"
        OnOkScript="okDelClick();" CancelControlID="btnNoDel" OnCancelScript="cancelDelClick();"
        BackgroundCssClass="modalBackground" />
    <asp:Panel ID="div" runat="server" Style="display: none" CssClass="modalPopup">
                            <div class="text-center">
                                <div class="modal-content">
                                    <div class="modal-body">
                                        <asp:Image ID="imgWarning" runat="server" ImageUrl="~/images/warning.gif" />
                                        <td>&nbsp;&nbsp;Are you sure you want to delete this record..?</td>
                                        <div class="text-center">
                                            <asp:Button ID="btnOkDel" runat="server" Text="Yes" CssClass="btn-primary" />
                                            <asp:Button ID="btnNoDel" runat="server" Text="No" CssClass="btn-primary" />
                                        </div>
                                    </div>
                                </div>
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
    <div id="divMsg" runat="server">
    </div>
</asp:Content>

