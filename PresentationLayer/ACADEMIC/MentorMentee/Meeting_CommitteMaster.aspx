<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Meeting_CommitteMaster.aspx.cs" Inherits="ACADEMIC_MentorMentee_Meeting_CommitteMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%-- <script src="../../JAVASCRIPTS/JScriptAdmin_Module.js" type="text/javascript"></script>--%>

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updActivity"
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
    <asp:UpdatePanel ID="updActivity" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                      <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">COMMITTEE MASTER</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <asp:Panel ID="pnlDesig" runat="server">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="trCollegeName" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Institute Name </label>
                                            </div>
                                            <asp:DropDownList ID="ddlCollege" runat="server" AppendDataBoundItems="true" TabIndex="2"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true" ToolTip="Select Institute">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvCollege" runat="server" ControlToValidate="ddlCollege"
                                                Display="None" ErrorMessage="Please Select College" ValidationGroup="Submit" SetFocusOnError="True" InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div id="Div2" class="form-group col-lg-3 col-md-6 col-12" runat="server">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Department</label>
                                            </div>
                                            <asp:DropDownList ID="ddlDepartment" runat="server" AppendDataBoundItems="true" TabIndex="2"
                                                CssClass="form-control" data-select2-enable="true" ToolTip="Select Institute">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlDepartment"
                                                Display="None" ErrorMessage="Please Select Department" ValidationGroup="Submit" SetFocusOnError="True" InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Committee</label>
                                            </div>
                                            <asp:TextBox ID="txtname" runat="server" onkeydown="return((event.keyCode >= 64 && event.keyCode <= 91) || (event.keyCode==32)|| (event.keyCode==8)|| (event.keyCode==9));"
                                                ValidationGroup="Submit" MaxLength="40" TabIndex="3" CssClass="form-control" ToolTip="Select Committee"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvcode" runat="server" ControlToValidate="txtname"
                                                Display="None" ErrorMessage="Please Select Committee " ValidationGroup="Submit"
                                                SetFocusOnError="true"></asp:RequiredFieldValidator>
                                             <ajaxToolKit:FilteredTextBoxExtender ID="txtname1" runat="server"
                                                        FilterMode="InvalidChars" FilterType="Custom" InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" TargetControlID="txtname" />
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Committee Code</label>
                                            </div>
                                            <asp:TextBox ID="txtcode" runat="server" ValidationGroup="Submit" MaxLength="10" onkeydown="return validateAlphaNumeric(txt)"
                                                TabIndex="4" CssClass="form-control" ToolTip="Enter Committee Code"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvname" runat="server" ControlToValidate="txtcode"
                                                Display="None" ErrorMessage="Please Enter Committee Code" ValidationGroup="Submit"
                                                SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftbeCCode" runat="server" FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters"
                                                TargetControlID="txtcode" ValidChars="/\- ">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                            <div class=" col-12 btn-footer">
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="Submit"
                                OnClick="btnSubmit_Click" CausesValidation="true" TabIndex="5" CssClass="btn btn-primary" ToolTip="Click here to Submit" />
                            <asp:Button ID="btnPrint" runat="server" Text="Report" CssClass="btn btn-info" ToolTip="Click to get Report" CausesValidation="false" TabIndex="7" OnClick="btnPrint_Click" Visible="false" />
                              <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CausesValidation="false" TabIndex="6" CssClass="btn btn-warning" ToolTip="Click here to Cancel" />
                         
                                 <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                ShowMessageBox="true" ShowSummary="false" ValidationGroup="Submit" HeaderText="Following Field(s) are mandatory" />
                         </div>
                           <div class="col-12">
                                <asp:Panel ID="pnlList" runat="server" ScrollBars="Auto">
                                    <asp:ListView ID="lvComit" runat="server" Visible="false">
                                        <LayoutTemplate>
                                            <div id="lgv1">
                                                <div class="sub-heading"><h5>LIST OF COMMITTEES CREATED</h5> </div>
                                                  <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                            <th>Action </th>
                                                           <%-- <th>Delete </th>--%>
                                                           <%-- <th>Edit </th>--%>
                                                            <th>Committee Name </th>
                                                            <th>Committee Code </th>
                                                            <th>Department
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
                                                    <asp:ImageButton ID="btnDelete" runat="server" AlternateText="Delete Record" CommandArgument='<%# Eval("ID")%>' ImageUrl="~/images/delete.png" OnClick="btnDelete_Click" OnClientClick="showConfirmDel(this); return false;" ToolTip="Delete Record" />
                                               <%-- </td>
                                                <td>--%>
                                                    <asp:ImageButton ID="btnEdit" runat="server" AlternateText="Edit Record" CausesValidation="false" CommandArgument='<%# Eval("ID") %>' ImageUrl="~/images/edit.png" OnClick="btnEdit_Click" ToolTip="Edit Record" />
                                                </td>
                                                <td><%# Eval("NAME")%></td>
                                                <td><%# Eval("CODE")%></td>
                                                <td>
                                                    <%# Eval("SUBDEPT")%>
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
                    <asp:Image ID="imgWarning" runat="server" ImageUrl="~/images/warning.png" />
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

    <%--END MODAL POPUP EXTENDER FOR DELETE CONFIRMATION --%>
    
    <script type="text/javascript" language="javascript">


        function toggleExpansion(imageCtl, divId) {
            if (document.getElementById(divId).style.display == "block") {
                document.getElementById(divId).style.display = "none";
                imageCtl.src = "../../IMAGES/expand_blue.jpg";
            } //../images/action_up.gif
            else if (document.getElementById(divId).style.display == "none") {
                document.getElementById(divId).style.display = "block";
                imageCtl.src = "../../IMAGES/collapse_blue.jpg";
            }
        }


        function validateAlphaNumeric(txt) {
            var expAN = /[$\\@\\\#%\^\&\*\(\)\[\]\+\_popup\{\}|`\~\=\|]/;
            var strPass = txt.value;
            var strLength = strPass.length;
            var lchar = txt.value.charAt((strLength) - 1);

            if (lchar.search(expAN) != -1) {
                txt.value(txt.value.substring(0, (strLength) - 1));
                txt.focus();
                alert('Only Alpha-Numeric Characters Allowed!');
            }
            return true;
        }




    </script>


</asp:Content>
