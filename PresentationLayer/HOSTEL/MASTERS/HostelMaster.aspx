<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="HostelMaster.aspx.cs" Inherits="HOSTEL_HostelMaster" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--<script type="text/javascript">
                //On Page Load
                $(document).ready(function () {
                    $('#table2').DataTable();
                });
    </script>--%>

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

    <style>
        .checkbox-list-box {
            height: 75px;
        }
    </style>

    <%--  Shrink the info panel out of view --%> <%--  Reset the sample so it can be played again --%>
    <ajaxToolKit:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mdlPopupDel" runat="server"
        TargetControlID="div" PopupControlID="div" 
        OkControlID="btnOkDel" OnOkScript="okDelClick();"
        CancelControlID="btnNoDel" OnCancelScript="cancelDelClick();" BackgroundCssClass="modalBackground" />
     <asp:Panel ID="div" runat="server" Style="display: none" CssClass="modalPopup">
        <div style="text-align: center">
            <table>
                <tr>
                    <td align="center">
                        <asp:Image ID="imgWarning" runat="server" ImageUrl="~/images/warning.png"  AlternateText="Warning" /></td>
                    <td>&nbsp;&nbsp; <h6> Are you sure you want to Delete this Record ? </h6>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:Button ID="btnOkDel" runat="server" Text="Yes" CssClass="btn btn-primary" />
                        <asp:Button ID="btnNoDel" runat="server" Text="No" CssClass="btn btn-warning" />
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">DEFINE HOSTEL</h3>
                </div>

                <div class="box-body">
                     <div class="row">
                            <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divhostel" visible="false">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Degree Name</label>
                                </div>
                                <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" TabIndex="1" CssClass="form-control" data-select2-enable="true" AutoPostBack="true">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <%--<asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                    Display="None" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup="submit">
                                </asp:RequiredFieldValidator>--%>
                                <%--comment for the Kota and Hamirpur not added in both.--%>
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Hostel Name</label>
                                </div>
                                <asp:TextBox ID="txtHostelName" runat="server" TabIndex="2" MaxLength="50" />
                                <asp:RequiredFieldValidator ID="rfvSessionName" runat="server" ControlToValidate="txtHostelName"
                                    Display="None" ErrorMessage="Please enter Hostel Name." ValidationGroup="submit"
                                    SetFocusOnError="True" />
                                <ajaxToolKit:FilteredTextBoxExtender ID="fteHostelName" runat="server" TargetControlID="txtHostelName"
                                    FilterType="Custom" FilterMode="InvalidChars" InvalidChars="!@#$%^&*()_=+~`?/><">
                                </ajaxToolKit:FilteredTextBoxExtender>
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Hostel Address</label>
                                </div>
                                <asp:TextBox ID="txtAddress" runat="server" TabIndex="3" MaxLength="100" CssClass="form-control" />
                                <asp:RequiredFieldValidator ID="rfvHostelAdd" runat="server" ControlToValidate="txtAddress"
                                    Display="None" ErrorMessage="Please enter Hostel Address." ValidationGroup="submit"
                                    SetFocusOnError="True" />
                                <ajaxToolKit:FilteredTextBoxExtender ID="fteAddress" runat="server" TargetControlID="txtAddress"
                                    FilterType="Custom" FilterMode="InvalidChars" InvalidChars="!@#`~$%^&*()_=+?/:;<>">
                                </ajaxToolKit:FilteredTextBoxExtender>
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Hostel Type</label>
                                </div>
                                <asp:RadioButtonList ID="rdoGender" runat="server" RepeatDirection="Horizontal" TabIndex="4">
                                    <asp:ListItem Value="1"> Male</asp:ListItem>
                                    <asp:ListItem Value="2"> Female</asp:ListItem>
                                </asp:RadioButtonList>
                                <asp:RequiredFieldValidator ID="rfvHostelType" runat="server" ControlToValidate="rdoGender"
                                    Display="None" ErrorMessage="Please select Hostel Type." ValidationGroup="submit"
                                    SetFocusOnError="True" />
                            </div>

                            </div>

                        <div class="row">

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup></sup>
                                    <label>Year</label>
                                </div>
                                <div class="form-group col-md-12 checkbox-list-box">
                                    <asp:CheckBoxList ID="ChkBlstYear" runat="server" RepeatColumns="2"
                                        RepeatDirection="Horizontal" Width="100%" CssClass="checkbox-list-style" AppendDataBoundItems="true" TabIndex="5">
                                    </asp:CheckBoxList>
                                </div>
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup></sup>
                                    <label>Category</label>
                                </div>
                                <div class="form-group col-md-12 checkbox-list-box">
                                    <asp:CheckBoxList ID="ChkblstCategory" runat="server" RepeatColumns="3"
                                        RepeatDirection="Horizontal" Width="100%" CssClass="checkbox-list-style" AppendDataBoundItems="true" TabIndex="6">
                                    </asp:CheckBoxList>
                                </div>
                            </div>
                        </div>

                    <div class="col-12 btn-footer mt-3">
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" ValidationGroup="submit"
                            OnClick="btnSubmit_Click" TabIndex="7" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" CausesValidation="false"
                            OnClick="btnCancel_Click" TabIndex="8" />
                        <asp:ValidationSummary ID="valSummary" runat="server" DisplayMode="List" ShowMessageBox="true"
                            ShowSummary="false" ValidationGroup="submit" />
                    </div>

                    <asp:Panel ID="pnlList" runat="server">
                        <div class="col-12">
                            <asp:Repeater ID="lvSession" runat="server">
                                <HeaderTemplate>
                                    <div class="sub-heading">
                                        <h5>List of Hostels</h5>
                                    </div>
                                    <table id="table2" class="table table-striped table-bordered nowrap display" style="width: 100%">
                                        <thead class="bg-light-blue">
                                            <tr>
                                                <th>Edit
                                                </th>
                                                <th>Delete
                                                </th>
                                                <%--<th>Degree 
                                                </th>--%>
                                                <%--comment for the Kota and Hamirpur not added in both.--%>
                                                <th>Hostel Name
                                                </th>
                                                <th>Hostel Address
                                                </th>
                                                <th>Hostel Type
                                                </th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>

                                        <td>
                                            <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("HOSTEL_NO") %>'
                                                AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" TabIndex="15" />&nbsp;
                                                
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Images/delete.png" CommandArgument='<%# Eval("HOSTEL_NO") %>'
                                                AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                                OnClientClick="showConfirmDel(this); return false;" />
                                        </td>
                                        <%--<td>
                                            <%# Eval("DEGREE")%>
                                        </td>--%>
                                        <%--comment for the Kota and Hamirpur not added in both.--%>
                                        <td>
                                            <%# Eval("HOSTEL_NAME")%>
                                        </td>
                                        <td>
                                            <%# Eval("HOSTEL_ADDRESS")%>
                                        </td>
                                        <td>
                                            <%# Eval("HOSTEL_TYPE")%>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </tbody></table>
                                </FooterTemplate>
                            </asp:Repeater>
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>

    <div id="divMsg" runat="server">
    </div>
</asp:Content>
