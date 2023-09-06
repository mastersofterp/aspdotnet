<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Assignment_Result_Report.aspx.cs" Inherits="Itle_Assignment_Result_Report" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<%@ Register Assembly="FreeTextBox" Namespace="FreeTextBoxControls" TagPrefix="FTB" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">ASSIGNMENT RESULT REPORT</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <asp:Panel ID="pnlAssignmentReport" runat="server">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="tr2" runat="server">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Select Report Type</label>
                                            </div>
                                            <asp:RadioButtonList ID="rbtnReportType" runat="server" AutoPostBack="true"
                                                OnSelectedIndexChanged="rbtnReportType_SelectedIndexChanged" Font-Bold="true">
                                                <asp:ListItem Value="0" Selected="True" Text="Course wise Assignment Report"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="Single Assignment Report"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="Single Student Assignment Report"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                        <%-- <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                              <sup></sup>
                                            <label>Select Report Type</label>
                                        </div>
                                     </div>--%>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="tr1" runat="server">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Select Course</label>
                                            </div>
                                            <asp:DropDownList ID="ddCourse" CssClass="form-control" data-select2-enable="true" runat="server" TabIndex="1"
                                                AppendDataBoundItems="true" AutoPostBack="true" ToolTip="Select Course"
                                                OnSelectedIndexChanged="ddCourse_SelectedIndexChanged">
                                            </asp:DropDownList>

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddCourse" InitialValue="0"
                                                ErrorMessage="Select course." ValidationGroup="submit"></asp:RequiredFieldValidator>

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="trAssignment" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Select Assignment</label>
                                            </div>
                                            <asp:DropDownList ID="ddlAssignment" CssClass="form-control" data-select2-enable="true" runat="server" TabIndex="2"
                                                AppendDataBoundItems="true" AutoPostBack="true" ToolTip="Select Assignment">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlAssignment" InitialValue="0"
                                                ErrorMessage="Select assignment." ValidationGroup="submit"></asp:RequiredFieldValidator>

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="trStudent" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Select Student</label>
                                            </div>

                                            <asp:DropDownList ID="ddlStudent" CssClass="form-control" data-select2-enable="true" runat="server" AppendDataBoundItems="true"
                                                TabIndex="3" ToolTip="Select Student">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlStudent" InitialValue="0"
                                                ErrorMessage="Select student." ValidationGroup="submit"></asp:RequiredFieldValidator>

                                        </div>
                                    </div>
                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="btnShowReport" runat="server" Text="Show Report" ValidationGroup="submit" TabIndex="4"
                                            OnClick="btnShowReport_Click" CssClass="btn btn-primary" ToolTip="Click here to Show Report" />

                                        <asp:Button ID="btnreset" runat="server" Text="Reset" TabIndex="5" ToolTip="Click here to Reset"
                                            OnClick="btnreset_Click" CssClass="btn btn-warning" />
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                                            ShowSummary="false" DisplayMode="List" ValidationGroup="submit" />
                                    </div>

                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

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

</asp:Content>
