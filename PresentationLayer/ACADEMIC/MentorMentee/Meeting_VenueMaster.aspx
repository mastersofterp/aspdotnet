<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Meeting_VenueMaster.aspx.cs" Inherits="ACADEMIC_MentorMentee_Meeting_VenueMaster_aspx" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../../JAVASCRIPTS/JScriptAdmin_Module.js" type="text/javascript"></script>
    <asp:UpdatePanel ID="updActivity" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">VENUE MASTER</h3>
                        </div>
                        <div>
                            <form role="form">
                                <div class="box-body">
                                    <div class="col-12">

                                        <asp:Panel ID="pnlDesig" runat="server">
                                            <div class="panel panel-info">
                                                <%--<div class="panel panel-heading">Add/Edit Venue</div>--%>
                                                <div class="sub-heading">
                                                    <h5>Add/Edit Venue</h5>
                                                </div>
                                                <div class="panel panel-body">
                                                    <div class="row">
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <label><span style="color: #FF0000">*</span>Venue Name :</label>
                                                            <asp:TextBox ID="txtVenue" runat="server" ValidationGroup="Submit" MaxLength="100" TabIndex="1" CssClass="form-control" ToolTip="Enter Venue Name" onkeypress="allowAlphaNumericSpace(event)"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvcode" runat="server" ControlToValidate="txtVenue"
                                                                Display="None" ErrorMessage="Please Enter Venue Name" ValidationGroup="Submit"
                                                                SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FTBE" runat="server" TargetControlID="txtVenue" FilterType="Custom,LowercaseLetters,UppercaseLetters,Numbers" ValidChars="@,.#$%&*-/() "></ajaxToolKit:FilteredTextBoxExtender>

                                                            <%-- onkeypress="return CheckAlphabet(event,this);"--%>
                                                        </div>

                                                        <asp:HiddenField ID="hfdActive" runat="server" ClientIDMode="Static" />
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup></sup>
                                                                <label>Status</label>
                                                            </div>
                                                            <div class="switch form-inline">
                                                                <input type="checkbox" id="chkStatus" name="switch" checked />
                                                                <label data-on="Active" data-off="InActive" for="chkStatus"></label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </form>
                        </div>
                        <div class="box-footer">
                            <p class="text-center">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="Submit" CssClass="btn btn-primary" ToolTip="Click here to Submit" OnClientClick="return validate_Active();" OnClick="btnSubmit_Click" CausesValidation="true" TabIndex="2" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" ToolTip="Click here to Cancel" OnClick="btnCancel_Click" CausesValidation="false" TabIndex="3" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Submit" />
                                <div class="col-md-12">
                                    <asp:Panel ID="pnlList" runat="server" ScrollBars="Auto">
                                        <asp:ListView ID="lvDesig" runat="server" Visible="false">
                                            <LayoutTemplate>
                                                <div id="lgv1">
                                                    <%--<h4 class="box-title">VENUE LIST </h4>--%>
                                                    <div class="sub-heading">
                                                        <h5>VENUE ENTRY LIST</h5>
                                                    </div>
                                                    <table class="table table-striped table-bordered nowrap display">
                                                        <thead>
                                                            <tr class="bg-light-blue">
                                                                <th>EDIT </th>
                                                                <th>VENUE NAME </th>
                                                                <th>STATUS</th>
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
                                                    <td style="width: 5%;">
                                                        <asp:ImageButton ID="btnEdit" runat="server" AlternateText="Edit Record" CausesValidation="false" CommandArgument='<%# Eval("PK_VENUEID") %>' ImageUrl="~/images/edit.PNG" OnClick="btnEdit_Click" ToolTip="Edit Record" />
                                                    </td>
                                                    <td>
                                                        <%# Eval("VENUE")%>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblStatus" runat="server" CssClass="badge" Text='<%# Eval("STATUS") %>'></asp:Label>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </asp:Panel>
                                </div>
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
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

    </script>

    <script>
        function allowAlphaNumericSpace(e) {
            var code = ('charCode' in e) ? e.charCode : e.keyCode;
            if (!(code == 32) && // space
              !(code == 45) && // Dash
              !(code > 47 && code < 58) && // numeric (0-9)
              !(code > 64 && code < 91) && // upper alpha (A-Z)
              !(code > 96 && code < 123)) { // lower alpha (a-z)
                e.preventDefault();
            }
        }
    </script>


    <script>
        function SetActive(val) {
            $('#chkStatus').prop('checked', val);

        }
        function validate_Active() {
            $('#hfdActive').val($('#chkStatus').prop('checked'));

        }

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnSubmit').click(function () {
                    validate_Active();
                });
            });
        });
    </script>
</asp:Content>
