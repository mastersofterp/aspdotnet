<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="PaySBResponsibilities.aspx.cs" Inherits="PAYROLL_MASTERS_PaySBResponsibilities" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%-- <asp:UpdatePanel ID="updPanel" runat="server">
        <ContentTemplate>--%>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">Responsibilities</h3>
                </div>
                <div class="box-body">
                    <asp:Panel ID="pnlAdd" runat="server">
                        <div class="col-12">
                            <div class="row">
                                <div class="col-12">
                                    <div class="sub-heading">
                                        <h5>RESPONSIBILITIES</h5>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-12">
                            <div class="row">
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Responsibilities</label>
                                    </div>
                                    <asp:TextBox ID="txtResponsibilities" runat="server" MaxLength="400" CssClass="form-control"
                                         ToolTip="Enter Responsibilities" TabIndex="1" />
                                    <asp:RequiredFieldValidator ID="rfvResponsibilties" runat="server" ControlToValidate="txtResponsibilities"
                                        Display="None" ErrorMessage="Please Enter Responsibilities" ValidationGroup="Responsibilities"
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
                        <asp:Button ID="btnSave" runat="server" Text="Submit" ValidationGroup="Responsibilities" OnClick="btnSave_Click"
                            CssClass="btn btn-primary" ToolTip="Click here to Submit" TabIndex="3" />                   
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-warning"
                            TabIndex="6" ToolTip="Click here to Reset" />
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Responsibilities"
                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                    </div>
                    <div class="col-12">
                        <asp:Panel ID="pnlList" runat="server">
                            <asp:ListView ID="lvResponsibilites" runat="server">
                                <%--<EmptyDataTemplate>
                                    <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="Click Add New To Enter Responsibilities" CssClass="d-block text-center mt-3" />
                                </EmptyDataTemplate>--%>
                                <LayoutTemplate>
                                    <div class="sub-heading">
                                        <h5>Responsibilities List</h5>
                                    </div>
                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                        <thead class="bg-light-blue">
                                            <tr>
                                                <th>Action
                                                </th>
                                                <th>Responsibilities
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
                                            <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("RNO") %>'
                                                AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />
                                        </td>
                                        <td>
                                            <%# Eval("RESPONSIBILTIY")%>
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


