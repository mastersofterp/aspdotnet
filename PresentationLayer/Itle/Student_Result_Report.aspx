<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Student_Result_Report.aspx.cs" Inherits="Itle_Student_Result_Report" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<link href="../CSS/master.css" rel="stylesheet" />--%>

    <script type="text/javascript">
        function clientShowing(source, args) {
            source._popupBehavior._element.style.zIndex = 100000;
        }
    </script>
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
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
    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">STUDENT RESULT REPORT</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <asp:Panel ID="panelStudent" runat="server">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Search By</label>
                                                <asp:RadioButtonList ID="rbtnSearch" runat="server"
                                                    RepeatDirection="Horizontal" AutoPostBack="true"
                                                    OnSelectedIndexChanged="rbtnSearch_SelectedIndexChanged">
                                                    <asp:ListItem Text="RRN NO." Value="R" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="NAME" Value="N"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="Tr1" runat="server" visible="true">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Student Roll No</label>
                                            </div>
                                            <asp:TextBox ID="txtStudent_Id" runat="server" CssClass="form-control" AutoPostBack="true"
                                                TabIndex="1" ToolTip="Enter Student Roll Number"></asp:TextBox>
                                            <asp:HiddenField ID="hdn1" runat="server" />
                                            <ajaxToolKit:AutoCompleteExtender ID="txtStudent_Id_AutoCompleteExtender" runat="server"
                                                Enabled="True" ServicePath="~/Autocomplete.asmx" TargetControlID="txtStudent_Id" OnClientShowing="clientShowing"
                                                CompletionSetCount="6" ServiceMethod="GetStudentID" MinimumPrefixLength="1" CompletionInterval="0"
                                                CompletionListCssClass="autocomplete_completionListElement" OnClientItemSelected="GetnName"
                                                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem">
                                            </ajaxToolKit:AutoCompleteExtender>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Student Name</label>
                                            </div>
                                           <%-- <asp:TextBox ID="txtStudent_Name" runat="server" CssClass="form-control" TabIndex="1"
                                                AutoPostBack="true" Enabled="false" ToolTip="Enter Student Name"></asp:TextBox>--%>

                                             <asp:DropDownList ID="ddlname" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                         ToolTip="Select Name" Enabled="false" TabIndex="1">
                                    </asp:DropDownList>

                                  

                                            <asp:Label ID="lblName_Required" runat="server" Text="" ForeColor="Red" Font-Bold="true"></asp:Label>
                                            <asp:HiddenField ID="hdn2" runat="server" />
                                           <%-- <ajaxToolKit:AutoCompleteExtender ID="txtStudent_Name_AutoCompleteExtender" runat="server" OnClientShowing="clientShowing"
                                                Enabled="True" ServicePath="~/Autocomplete.asmx" TargetControlID="ddlname"
                                                CompletionSetCount="6" ServiceMethod="GetStudentName" MinimumPrefixLength="1" CompletionInterval="0"
                                                CompletionListCssClass="autocomplete_completionListElement" OnClientItemSelected="GetName"
                                                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                                CompletionListItemCssClass="autocomplete_listItem">
                                            </ajaxToolKit:AutoCompleteExtender>--%>
                                        </div>

                                    </div>
                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary" Text="Show Result" TabIndex="2"
                                            OnClick="btnSubmit_Click" ToolTip="Clcik here to Show Result" />
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-warning"
                                            TabIndex="3" ToolTip="Click here to Reset" />
                                        <%--<asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                            ShowSummary="false" ValidationGroup="Submit" />--%>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>

                    </div>
                </div>
            </div>


            <ajaxToolKit:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mdlPopupDel" runat="server"
                TargetControlID="div" PopupControlID="div"
                OkControlID="btnOkDel" OnOkScript="okDelClick();"
                CancelControlID="btnNoDel" OnCancelScript="cancelDelClick();" BackgroundCssClass="modalBackground" />
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

                function GetnName(source, eventArgs) {
                    var idno = eventArgs.get_value().split("*");
                    var Name = idno[0].split("-");
                    document.getElementById('ctl00_ContentPlaceHolder1_txtStudent_Name').value = idno[1];
                    document.getElementById('ctl00_ContentPlaceHolder1_txtStudent_Id').value = Name[0];
                    document.getElementById('ctl00_ContentPlaceHolder1_hdn1').value = idno[2];
                }

                function GetName(source, eventArgs) {
                    var idno = eventArgs.get_value().split("*");
                    var Name = idno[0].split("-");
                    document.getElementById('ctl00_ContentPlaceHolder1_txtStudent_Name').value = Name[0];
                    document.getElementById('ctl00_ContentPlaceHolder1_txtStudent_Id').value = idno[1];
                    document.getElementById('ctl00_ContentPlaceHolder1_hdn2').value = idno[2];
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
    <div id="divMsg" runat="server"></div>
        <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>

