<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ITLE_Faculty_Achivements.aspx.cs" Inherits="Itle_ITLE_Faculty_Achivements" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<%@ Register Assembly="FreeTextBox" Namespace="FreeTextBoxControls" TagPrefix="FTB" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="ckeditor/ckeditor.js" type="text/javascript"></script>
    <script src="ckeditor/ckeditor_basic.js" type="text/javascript"></script>

    <div class="row">
        <div class="col-md-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">FACULTY ACHIEVEMENTS</h3>
                </div>
                <div class="box-body">
                    <div class="col-md-12">
                        Note <b>:</b> <span style="color: #FF0000">* Marked Is Mandatory !</span>
                        <asp:Panel ID="pnlFaculty" runat="server">
                            <div class="panel panel-info">
                                <div class="panel panel-heading">Achievement Creation By Faculty</div>
                                <div class="panel panel-body">
                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <div class="col-md-2">
                                                <label>Session&nbsp;&nbsp;:</label>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:Label ID="lblSession" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <div class="col-md-2">
                                                <label>Course Name&nbsp;&nbsp;:</label>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:Label ID="lblCorseName" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <div class="col-md-2">
                                                <label>Current Date&nbsp;&nbsp;:</label>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:Label ID="lblCurrdate" runat="server" ForeColor="Green"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <div class="col-md-2">
                                                <label><span style="color: Red">*</span>Subject&nbsp;&nbsp;:</label>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:DropDownList ID="ddlAchivType" runat="server" CssClass="form-control" ToolTip="Select Subject"
                                                    AppendDataBoundItems="true">
                                                </asp:DropDownList>
                                                <%--<ajaxToolkit:CascadingDropDown ID="cddDept" runat="server" TargetControlID="ddlBranch"
                                                    Category="City" PromptText="Please Select" LoadingText="[Loading...]" ServicePath="~/WebService.asmx"
                                                ServiceMethod="GetBranch" ParentControlID="ddlDept" />--%>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" InitialValue="0" ControlToValidate="ddlAchivType"
                                                    ErrorMessage="Please Select Subject" SetFocusOnError="True" ValidationGroup="submit">*</asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <div class="col-md-2">
                                                <label><span style="color: Red">*</span>Description&nbsp;&nbsp;:</label>
                                            </div>
                                            <div class="col-md-10">
                                                <%-- <ajaxToolkit:CascadingDropDown ID="cddBranch" runat="server" TargetControlID="ddlSemester"
                                                    Category="City" PromptText="Please Select" LoadingText="[Loading...]" ServicePath="~/WebService.asmx"
                                                    ServiceMethod="GetSemester" ParentControlID="ddlBranch" />--%>
                                                <CKEditor:CKEditorControl ID="ftbDescription" runat="server" Height="200" BasePath="~/ckeditor">		                        
                                                </CKEditor:CKEditorControl>
                                                <%--<FTB:FreeTextBox ID="ftbDescription" runat="server" Height="250px" Width="100%" Text="&nbsp;">
                                                </FTB:FreeTextBox>--%>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ftbDescription"
                                                    ErrorMessage="Enter Description" SetFocusOnError="True" ValidationGroup="submit">*</asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="form-group col-md-12">
                                            <div class="col-md-2">
                                                <label><span style="color: Red">*</span>Achivement Date&nbsp;&nbsp;:</label>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <asp:Image ID="imgSubmitDate" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                    </div>
                                                    <asp:TextBox ID="txtAchivDate" runat="server" CssClass="form-control" ToolTip="Enter Achivement Date"
                                                        Style="z-index: 0;"></asp:TextBox>
                                                    <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server"
                                                        TargetControlID="txtAchivDate" PopupButtonID="imgSubmitDate" Enabled="true" EnableViewState="true" Format="dd/MM/yyyy">
                                                    </ajaxToolKit:CalendarExtender>
                                                    <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtAchivDate"
                                                        Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                        AcceptNegative="Left" ErrorTooltipEnabled="true" />
                                                    <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender1"
                                                        ControlToValidate="txtAchivDate" EmptyValueMessage="Please Enter Submission Date"
                                                        InvalidValueMessage="Submission Date is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                                        TooltipMessage="Please Enter Achivement Date" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                                        ValidationGroup="submit" SetFocusOnError="true"></ajaxToolKit:MaskedEditValidator>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <asp:RequiredFieldValidator ID="reqDateField" runat="server" ControlToValidate="txtAchivDate"
                                                    ErrorMessage="Please enter Achievement Date" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="form-group col-md-12">
                                            <div class="col-md-2">
                                                <label>Attachment Files&nbsp;&nbsp;:</label>
                                            </div>
                                            <div class="col-md-3">
                                                <asp:FileUpload ID="fuAnnounce" runat="server" />&nbsp;&nbsp;(Max.Size
                                                    <asp:Label ID="lblFileSize" runat="server" Font-Bold="true"></asp:Label>)
                                            </div>
                                            <div class="col-md-3">
                                                <asp:HiddenField ID="hdnFile" runat="server" />
                                                <asp:Label ID="lblPreAttach" runat="server" Height="21px" Text="Label" Visible="False"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="form-group col-md-12">
                                            <div class="col-md-2">
                                                <label></label>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                    <ContentTemplate>
                                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" ValidationGroup="submit"
                                                            OnClick="btnSubmit_Click" ToolTip="Click here to Submit"/>&nbsp;&nbsp;
                                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" 
                                                            OnClick="btnCancel_Click" ToolTip="Click here to Reset"/>
                                                        &nbsp;&nbsp;
                                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                                                            ShowSummary="false" DisplayMode="List" ValidationGroup="submit" />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <div class="col-md-2">
                                                <label></label>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:Label ID="lblStatus" runat="server" SkinID="Msglbl"></asp:Label>
                                            </div>
                                        </div>
                                    </div>                                   
                                </div>
                            </div>
                        </asp:Panel>

                        <asp:Panel ID="pnlAchievemntsList" runat="server" ScrollBars="Auto">
                            <asp:ListView ID="lvAchievements" runat="server">
                                <LayoutTemplate>
                                    <div id="demo-grid">
                                        <h4 class="box-title">Achievements List
                                        </h4>
                                        <table class="table table-bordered table-hover">
                                            <thead>
                                                <tr class="bg-light-blue">
                                                    <th>Action
                                                    </th>
                                                    <th>Achievement Type
                                                    </th>
                                                    <th>Achievement Date
                                                    </th>
                                                    <th>Attachment
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
                                            <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit1.gif" CommandArgument='<%# Eval("FANO") %>'
                                                ToolTip="Edit Record" AlternateText="Edit Record" OnClick="btnEdit_Click" />&nbsp;
                                            <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.gif" CommandArgument='<%# Eval("FANO") %>'
                                                ToolTip="Delete Record" OnClick="btnDelete_Click" OnClientClick="showConfirmDel(this); return false;" />
                                        </td>
                                        <td>
                                            <%# Eval("AWDTYPE")%>
                                        </td>
                                        <td>
                                            <%# Eval("ACHIV_DATE", "{0:dd/MM/yyyy}")%>
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="lnkDownload" runat="server" Text='<%# Eval("ATTACHMENT") %>'
                                                ToolTip='<%# Eval("ATTACHMENT")%>' OnClick="lnkDownload_Click" CommandArgument='<%#Eval("FANO")%>'>
                                            </asp:LinkButton>
                                            <%-- <asp:HyperLink ID="lnkDownload" runat="server" Text='<%# Eval("ATTACHMENT") %>' NavigateUrl='<%# GetFileName(Eval("ATTACHMENT"), Eval("FANO"))%>'
                                            Target="_blank">
                                            </asp:HyperLink>--%>
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
   
    <%# Eval("STARTDATE", "{0:dd-MMM-yyyy}")%>
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



</asp:Content>

