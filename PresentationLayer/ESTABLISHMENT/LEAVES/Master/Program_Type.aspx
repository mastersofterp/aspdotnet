<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Program_Type.aspx.cs" Inherits="ESTABLISHMENT_LEAVES_Master_Program_Type" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%-- <asp:UpdatePanel ID="updPanel" runat="server">
        <ContentTemplate>--%>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">EVENT TYPE</h3>
                </div>
                <div class="box-body">
                    <asp:Panel ID="pnlAdd" runat="server" Visible="false">
                        <div class="col-12">
                            <div class="row">
                                <div class="col-12">
                                    <div class="sub-heading">
                                        <h5>Event Type Entry</h5>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-12">
                            <div class="row">
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Event Type</label>
                                    </div>
                                    <asp:TextBox ID="txtProgramType" runat="server" MaxLength="50" CssClass="form-control"
                                        onkeypress="return CheckAlphabet(event,this);" ToolTip="Enter Event Type" TabIndex="1" />
                                    <asp:RequiredFieldValidator ID="ReqProgramType" runat="server" ControlToValidate="txtProgramType"
                                        Display="None" ErrorMessage="Please Enter Event Type" ValidationGroup="ProgramType"
                                        SetFocusOnError="True">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="col-12">
                            <asp:Label ID="lblerror" SkinID="Errorlbl" runat="server"></asp:Label>
                            <asp:Label ID="lblmsg" runat="server" SkinID="lblmsg"></asp:Label>
                        </div>
                    </asp:Panel>
                    <div class="col-12 btn-footer">
                        <asp:LinkButton ID="LnkAdd" runat="server" OnClick="btnAdd_Click" Text="Add New"
                            ToolTip="Click here to Add New Program Type" CssClass="btn btn-primary" TabIndex="2"></asp:LinkButton>
                        <asp:Button ID="btnSave" runat="server" Text="Submit" ValidationGroup="ProgramType" OnClick="btnSave_Click"
                            CssClass="btn btn-primary" ToolTip="Click here to Submit" TabIndex="3" />
                        <asp:Button ID="btnBack" runat="server" Text="Back" ToolTip="Click here to Go Back" CssClass="btn btn-primary" TabIndex="4"
                            OnClick="btnBack_Click" />
                        <asp:Button ID="btnReport" runat="server" Text="Show Report" OnClick="btnShowReport_Click"
                            TabIndex="5" ToolTip="Click here to Show Report" CssClass="btn btn-info" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-warning"
                            TabIndex="6" ToolTip="Click here to Reset" />
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="ProgramType"
                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                    </div>
                    <div class="col-12">
                        <asp:Panel ID="pnlList" runat="server">
                            <asp:ListView ID="lvProgramType" runat="server">
                                <EmptyDataTemplate>
                                    <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="Click Add New To Enter Event/Program Type" CssClass="d-block text-center mt-3" />
                                </EmptyDataTemplate>
                                <LayoutTemplate>
                                    <div class="sub-heading">
                                        <h5>Event Type List</h5>
                                    </div>
                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                        <thead class="bg-light-blue">
                                            <tr>
                                                <th>Action
                                                </th>
                                                <th>Event Type
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
                                            <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("PROGRAM_NO") %>'
                                                AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />
                                                <%--<asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.gif" CommandArgument='<%# Eval("PROGRAM_NO") %>'
                                                    AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                                    OnClientClick="showConfirmDel(this); return false;" />--%>
                                        </td>
                                        <td>
                                            <%# Eval("PROGRAM_TYPE")%>
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
     <div id="divMsg" runat="server"></div>
    <script type="text/javascript" language="javascript">
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


