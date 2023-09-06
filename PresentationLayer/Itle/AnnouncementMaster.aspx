<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="AnnouncementMaster.aspx.cs" Inherits="Itle_AnnouncementMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<%@ Register Assembly="FreeTextBox" Namespace="FreeTextBoxControls" TagPrefix="FTB" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../plugins/ckeditor/ckeditor.js"></script>
    <script src="../plugins/ckeditor/ckeditor_basic.js"></script>
    <%--    <script src="ckeditor/ckeditor.js" type="text/javascript"></script>
    <script src="ckeditor/ckeditor_basic.js" type="text/javascript"></script>--%>
    <script type="text/javascript">

        window.onload = function () {

            document.getElementById('<%=txtSubject.ClientID%>').focus();
        }
    </script>

    <style>
        .list-group .list-group-item .sub-label {
            float: initial;
        }
    </style>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">ANNOUNCEMENT CREATION</h3>
                </div>

                <div class="box-body">
                    <asp:Panel ID="pnlAnnouncement" runat="server">
                        <asp:Panel ID="pnlAnnounce" runat="server">
                            <div class="col-12">
                                <div class="row">
                                    <%--<div class="form-group col-lg-12 col-md-12 col-12">
                                        <div class="sub-heading">
                                            <h5>Announcement Creation By Faculty</h5>
                                        </div>
                                    </div>--%>
                                    <div class="col-lg-5 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Session :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblSession" runat="server" Font-Bold="True" ForeColor="#006600"></asp:Label>

                                                </a>
                                            </li>
                                        </ul>
                                    </div>
                                    <div class="col-lg-3 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Current Date :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblCurrdate" runat="server" Font-Bold="true"></asp:Label>

                                                </a>
                                            </li>
                                        </ul>
                                    </div>
                                    <div class="col-lg-12 col-md-12 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Course Name :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblCorseName" runat="server" Font-Bold="True" ForeColor="#006600"></asp:Label>

                                                </a>
                                            </li>
                                        </ul>
                                    </div>
                                    
                                </div>
                            </div>
                            <div class="col-12 mt-3">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Announcement Title</label>
                                        </div>
                                        <asp:TextBox ID="txtSubject" runat="server" CssClass="form-control" ToolTip="Enter Subject"
                                            TabIndex="1"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSubject"
                                            ErrorMessage="Enter Subject" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>

                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Expiry Date</label>
                                        </div>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i id="imgSubmitDate" runat="server" class="fa fa-calendar text-blue"></i>
                                            </div>
                                            <asp:TextBox ID="txtExpDate" runat="server" CssClass="form-control" ToolTip="Enter Expiry Date"
                                                ReadOnly="True" TabIndex="2"></asp:TextBox>
                                            <ajaxToolKit:CalendarExtender ID="ceSubmitDate" runat="server" Format="dd-MMM-yyyy"
                                                TargetControlID="txtExpDate" PopupButtonID="imgSubmitDate">
                                            </ajaxToolKit:CalendarExtender>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtExpDate"
                                                ErrorMessage="Enter Expiy Date!" SetFocusOnError="True"
                                                ValidationGroup="submit" Text="Enter Expiry Date" Display="None"></asp:RequiredFieldValidator>

                                        </div>
                                    </div>
                                    <div class="form-group col-lg-12 col-md-12 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Description</label>
                                        </div>
                                        <div class="table table-responsive">
                                            <CKEditor:CKEditorControl ID="ftbDescription" runat="server" Height="150" TabIndex="3"
                                                ToolTip="Enter Description" BasePath="~/plugins/ckeditor">
		                                                             <%--&lt;p&gt;This is some &lt;strong&gt;sample text&lt;/strong&gt;. 
                                                                         You are using &lt;a href="http://ckeditor.com/"&gt;CKEditor&lt;/a&gt;.&lt;/p&gt;--%>
                                            </CKEditor:CKEditorControl>
                                            <%--<FTB:FreeTextBox ID="ftbDescription" runat="server" Height="250px" Width="100%"
                                                                         Focus ="true"  Text="&nbsp;"   >
                                                                </FTB:FreeTextBox>--%>
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="Tr1" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Attachment&nbsp;Files</label>
                                        </div>
                                        <asp:FileUpload ID="fuAnnounce" runat="server" TabIndex="3" ToolTip="Click here to Select File" />
                                        (Max.Size<asp:Label ID="lblFileSize" runat="server" Font-Bold="true"></asp:Label>)                                
                                                                <asp:HiddenField ID="hdnFile" runat="server" />
                                        <asp:Label ID="lblPreAttach" runat="server" Text="Label" Visible="False"
                                            CssClass="form-control"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-12 btn-footer">
                                    <asp:UpdatePanel ID="UpdAnnounce" runat="server">
                                        <ContentTemplate>
                                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary"
                                                ValidationGroup="submit" ToolTip="Click here to Submit" TabIndex="5"
                                                OnClick="btnSubmit_Click" />
                                            <asp:Button ID="btnViewAnnouncement" runat="server" Text="Announcement Report"
                                                CssClass="btn btn-info" TabIndex="7"
                                                OnClick="btnViewAnnouncement_Click" ToolTip="Click here for Announcemnet Report" />
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning"
                                                OnClick="btnCancel_Click" ToolTip="Click here to Reset" TabIndex="6" />

                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                                                ShowSummary="false" DisplayMode="List" ValidationGroup="submit" />
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="btnCancel" />
                                            <asp:PostBackTrigger ControlID="btnSubmit" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-12">
                                    <asp:Label ID="lblStatus" runat="server" SkinID="Msglbl"></asp:Label>
                                </div>
                            </div>
                        </asp:Panel>

                        <div class="col-12">
                            <asp:Panel ID="pnlAnnouncementList" runat="server">
                                <div class="sub-heading">
                                    <h5>Announcement List</h5>
                                </div>
                                <asp:ListView ID="lvAnnounce" runat="server" OnSelectedIndexChanged="lvAnnounce_SelectedIndexChanged">

                                    <LayoutTemplate>

                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>Action</th>
                                                    <th>Created Date</th>
                                                    <th>Subject</th>
                                                    <th>Expiry Date</th>
                                                    <th>Status</th>
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
                                                <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit1.png" CommandArgument='<%# Eval("AN_NO") %>'
                                                    ToolTip="Edit Record" AlternateText="Edit Record" OnClick="btnEdit_Click" TabIndex="7" />&nbsp;
                                                                    <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.png" CommandArgument='<%# Eval("AN_NO") %>'
                                                                        ToolTip="Delete Record" OnClick="btnDelete_Click" OnClientClick="showConfirmDel(this); return false;" TabIndex="8" />

                                            </td>
                                            <td>
                                                <%# Eval("STARTDATE", "{0:dd-MMM-yyyy}")%>
                                            </td>
                                            <td>
                                                <%# Eval("SUBJECT")%>
                                            </td>
                                            <td>
                                                <%# Eval("EXPDATE", "{0:dd-MMM-yyyy}")%>                                                                   
                                            </td>
                                            <td>
                                                <%# GetStatus(Eval("EXPDATE"))%>                                                        
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <EmptyDataTemplate>
                                        <p class="text-center text-bold">

                                            <asp:Label ID="lblEmptyMsg" runat="server" Text="No Records Found"></asp:Label>
                                        </p>
                                    </EmptyDataTemplate>
                                </asp:ListView>
                            </asp:Panel>

                        </div>
                    </asp:Panel>

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
