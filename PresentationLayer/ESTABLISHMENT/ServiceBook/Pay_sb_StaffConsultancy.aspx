<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/ServiceBookMaster.master" CodeFile="Pay_sb_StaffConsultancy.aspx.cs"
    Inherits="ESTABLISHMENT_ServiceBook_Pay_sb_StaffConsultancy" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="sbhead" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="sbctp" runat="Server">

    <asp:UpdatePanel ID="updImage" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <form role="form">
                        <div class="box-body">
                            <div class="col-md-12">
                                <div class="col-md-12">

                                    <div class="col-12">
                                        <div class="row">
                                            <div class="col-12">
                                                <div class="sub-heading">
                                                    <h5>Consultancy</h5>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <asp:Panel ID="pnlFm" runat="server">

                                        <div class="panel panel-info">
                                            <%--<div class="panel panel-heading">Consultancy</div>--%>

                                            <div class="panel panel-body">

                                                <div class="col-12">
                                                    <div class="row">
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label><span style="color: #FF0000">*</span>Title :</label>
                                                            </div>
                                                            <asp:TextBox ID="txtTitle" runat="server" TabIndex="1" MaxLength="100" CssClass="form-control" autocomplete="off"
                                                                ToolTip="Enter Title" onkeypress="return CheckAlphabet(event,this);"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvTitle" runat="server" ControlToValidate="txtTitle"
                                                                Display="None" ErrorMessage="Please Enter Title" SetFocusOnError="True" ValidationGroup="ServiceBook"></asp:RequiredFieldValidator>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label><span style="color: #FF0000">*</span>Name of the Organization :</label>
                                                            </div>
                                                            <asp:TextBox ID="txtname" runat="server" TabIndex="2" MaxLength="200" CssClass="form-control" autocomplete="off"
                                                                ToolTip="Enter Name Of Organization" onkeypress="return CheckAlphabet(event,this);"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvAwardName" runat="server" ControlToValidate="txtname"
                                                                Display="None" ErrorMessage="Please Enter Name Of Organization" SetFocusOnError="True" ValidationGroup="ServiceBook"></asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label><span style="color: #FF0000">*</span>Address : </label>
                                                            </div>
                                                            <asp:TextBox ID="txtadd" runat="server" TabIndex="3" MaxLength="300" CssClass="form-control"
                                                                ToolTip="Enter Address" autocomplete="off"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvOragName" runat="server" ControlToValidate="txtadd"
                                                                Display="None" ErrorMessage="Please Enter Address" SetFocusOnError="true" ValidationGroup="ServiceBook"></asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label><span style="color: #FF0000">*</span>From Date : </label>
                                                            </div>
                                                            <div class="input-group date">
                                                                <div class="input-group-addon">
                                                                    <asp:Image ID="Image1" runat="server" class="fa fa-calendar text-blue" />
                                                                </div>
                                                                <asp:TextBox ID="txtDateOftalk" runat="server" CssClass="form-control" ToolTip="Enter From Date"
                                                                    TabIndex="4" Style="z-index: 0;" onchange="GetDateDiff();" onblur="GetDateDiff();"></asp:TextBox>
                                                                <ajaxToolKit:CalendarExtender ID="ceToDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDateOftalk"
                                                                    PopupButtonID="Image1" Enabled="true" EnableViewState="true" PopupPosition="BottomLeft">
                                                                </ajaxToolKit:CalendarExtender>
                                                                <asp:RequiredFieldValidator ID="rfvPublicationDate" runat="server" ControlToValidate="txtDateOftalk"
                                                                    Display="None" ErrorMessage="Please Enter From Date"
                                                                    ValidationGroup="ServiceBook" SetFocusOnError="True">
                                                                </asp:RequiredFieldValidator>
                                                                <ajaxToolKit:MaskedEditExtender ID="meToDate" runat="server" TargetControlID="txtDateOftalk"
                                                                    Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                                    AcceptNegative="Left" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate">
                                                                </ajaxToolKit:MaskedEditExtender>
                                                                <ajaxToolKit:MaskedEditValidator ID="mevToDate" runat="server" ControlExtender="meToDate"
                                                                    ControlToValidate="txtDateOftalk" EmptyValueMessage="Please Enter From Date"
                                                                    InvalidValueMessage="From Date is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                                                    TooltipMessage="Please Enter From Date" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid From Date"
                                                                    ValidationGroup="ServiceBook" SetFocusOnError="True" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-12">
                                                <div class="row">
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label><span style="color: #FF0000">*</span>To Date : </label>
                                                        </div>
                                                        <div class="input-group date">
                                                            <div class="input-group-addon">
                                                                <asp:Image ID="Image2" runat="server" class="fa fa-calendar text-blue" />
                                                            </div>
                                                            <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control" ToolTip="Enter To Date"
                                                                TabIndex="5" Style="z-index: 0;" onchange="GetDateDiff();" onblur="GetDateDiff();"></asp:TextBox>
                                                            <ajaxToolKit:CalendarExtender ID="ceTonewDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtToDate"
                                                                PopupButtonID="Image2" Enabled="true" EnableViewState="true" PopupPosition="BottomLeft">
                                                            </ajaxToolKit:CalendarExtender>
                                                            <asp:RequiredFieldValidator ID="rfvtodate" runat="server" ControlToValidate="txtToDate"
                                                                Display="None" ErrorMessage="Please Enter To Date"
                                                                ValidationGroup="ServiceBook" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                            <ajaxToolKit:MaskedEditExtender ID="meTonewDate" runat="server" TargetControlID="txtToDate"
                                                                Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                                AcceptNegative="Left" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate">
                                                            </ajaxToolKit:MaskedEditExtender>
                                                            <ajaxToolKit:MaskedEditValidator ID="mevnewToDate" runat="server" ControlExtender="meTonewDate"
                                                                ControlToValidate="txtToDate" EmptyValueMessage="Please Enter To Date"
                                                                InvalidValueMessage="To Date is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                                                TooltipMessage="Please Enter To Date" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                                                ValidationGroup="ServiceBook" SetFocusOnError="True" />
                                                        </div>
                                                    </div>
                                                    <%--  <div class="form-group col-md-4">
                                                    <label>Consultancy Period :<span style="color: #FF0000">*</span></label>
                                                    <div class="input-group date">
                                                        <div class="input-group-addon">
                                                            <asp:Image ID="Image3" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                        </div>
                                                        <asp:TextBox ID="txtConsultancy" runat="server" TabIndex="5" CssClass="form-control"
                                                            ToolTip="Enter Consultancy Period" onkeypress="return CheckNumeric(event,this);"></asp:TextBox>
                                                        <ajaxToolKit:CalendarExtender ID="Cecondate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtConsultancy"
                                                            PopupButtonID="Image3" Enabled="true" EnableViewState="true" PopupPosition="BottomLeft">
                                                        </ajaxToolKit:CalendarExtender>
                                                        <asp:RequiredFieldValidator ID="rfvcondate" runat="server" ControlToValidate="txtConsultancy"
                                                            Display="None" ErrorMessage="Please Enter Consultancy Period" ValidationGroup="ServiceBook"
                                                            SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                        <ajaxToolKit:MaskedEditExtender ID="mecondate" runat="server" TargetControlID="txtConsultancy"
                                                            Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                            AcceptNegative="Left" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate">
                                                        </ajaxToolKit:MaskedEditExtender>
                                                        <ajaxToolKit:MaskedEditValidator ID="mevcondate" runat="server" ControlExtender="mecondate"
                                                            ControlToValidate="txtConsultancy" EmptyValueMessage="Please Enter Date of talk"
                                                            InvalidValueMessage="Consultancy Period is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                                            TooltipMessage="Please Enter Consultancy Period" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Consultancy Period"
                                                            ValidationGroup="ServiceBook" SetFocusOnError="True" />
                                                    </div>
                                                </div>--%>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Duration :</label>
                                                        </div>
                                                        <asp:TextBox ID="txtConsultancy" runat="server" MaxLength="50" CssClass="form-control" TabIndex="6"
                                                            ToolTip="Enter Duration" autocomplete="off" onkeydown="return EditControl(event,this);" onkeypress="return EditControl(event,this);" onclick="return EditControl(event,this);"></asp:TextBox>

                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Project Cost  : </label>
                                                        </div>
                                                        <asp:TextBox ID="txtAmount" runat="server" MaxLength="10" TabIndex="7" CssClass="form-control"
                                                            ToolTip="Enter Project Cost" autocomplete="off" onkeypress="return numericdotOnly(event, this);"></asp:TextBox>
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="ftbeamount" runat="server" TargetControlID="txtAmount"
                                                            ValidChars="0123456789." Enabled="True">
                                                        </ajaxToolKit:FilteredTextBoxExtender>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Nature of work : </label>
                                                        </div>
                                                        <asp:TextBox ID="txtnaturework" runat="server" MaxLength="200" TabIndex="8" CssClass="form-control"
                                                            ToolTip="Enter Nature Of Work" autocomplete="off"></asp:TextBox>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Description : </label>
                                                        </div>
                                                        <asp:TextBox ID="txtDescription" runat="server" MaxLength="250" TabIndex="9" CssClass="form-control"
                                                            ToolTip="Enter Description" autocomplete="off"></asp:TextBox>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divBlob" runat="server" visible="false">
                                                        <asp:Label ID="lblBlobConnectiontring" runat="server" Text=""></asp:Label>
                                                        <asp:HiddenField ID="hdnBlobCon" runat="server" />
                                                        <asp:Label ID="lblBlobContainer" runat="server" Text=""></asp:Label>
                                                        <asp:HiddenField ID="hdnBlobContainer" runat="server" />
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Upload Files</label>
                                                            <label>Multiple Files Can Be Attached :</label>
                                                            <asp:FileUpload ID="FileUpload1" runat="server" TabIndex="10" />
                                                            <asp:Label ID="Label2" runat="server" Text=" Please Select valid Document file(e.g. .pdf,.jpg) upto 10MB" ForeColor="Red"></asp:Label>
                                                            <asp:Button ID="btnAdd" runat="server" Text="Add" TabIndex="11" class="btn btn-primary" OnClick="btnAdd_Click" ToolTip="Click on add to add file" />
                                                        </div>
                                                    </div>
                                                </div>


                                                <%--<asp:UpdatePanel ID="pnlnew" runat="server">
                                                    <ContentTemplate>
                                                        <asp:Panel ID="Panel2" runat="server">
                                                            <div class="panel panel-info">

                                                                <div class="panel-body">
                                                                    <div class="form-group row" id="divUploadFiles" style="display: block;">
                                                                        <div class="col-md-3">
                                                                        </div>
                                                                        <div class="col-md-3">
                                                                            <br />

                                                                        </div>
                                                                        <div class="col-md-12">
                                                                            <asp:Panel ID="pnlfiles" runat="server">
                                                                                <asp:ListView ID="LVFiles" runat="server">
                                                                                    <LayoutTemplate>
                                                                                        <div id="lgv1">
                                                                                            <h4>Attached Files</h4>
                                                                                            <table class="table table-bordered table-hover">
                                                                                                <thead>
                                                                                                    <tr class="bg-light-blue">
                                                                                                        <th>Delete</th>
                                                                                                        <th>File Name</th>
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
                                                                                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/delete.png"
                                                                                                    CommandArgument=' <%#Eval("GETFILE") %>' AlternateText=' <%#Eval("APPID") %>' ToolTip="Delete Record"
                                                                                                    OnClientClick="javascript:return confirm('Are you sure you want to delete this file?')" OnClick="btnDelFile_Click" />

                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:HyperLink ID="lnkDownload" runat="server" Target="_blank" NavigateUrl='<%# GetFileNamePath(Eval("GETFILE"),Eval("FUID"),Eval("IDNO"),Eval("FOLDER"),Eval("APPID"))%>'><%# Eval("DisplayFileName")%></asp:HyperLink>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </ItemTemplate>
                                                                                </asp:ListView>
                                                                            </asp:Panel>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </asp:Panel>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:PostBackTrigger ControlID="btnAdd" />
                                                    </Triggers>
                                                </asp:UpdatePanel>--%>

                                                <div id="divAttch" runat="server" style="display: none">
                                                    <div class="form-group">
                                                        <div class="col-md-12">
                                                            <asp:Panel ID="pnlAttachmentList" runat="server" ScrollBars="Auto">
                                                                <asp:ListView ID="lvCompAttach" runat="server">
                                                                    <LayoutTemplate>
                                                                        <table class="table table-striped table-bordered nowrap" style="width: 100%">
                                                                            <thead>
                                                                                <tr>
                                                                                    <th>Delete</th>
                                                                                    <th id="divattach" runat="server">Attachments  
                                                                                    </th>
                                                                                    <th id="divattachblob" runat="server" visible="false">Attachments
                                                                                    </th>
                                                                                    <th id="divDownload" runat="server" visible="false">Download
                                                                                    </th>
                                                                                    <th id="divBlobDownload" runat="server" visible="false">Download
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
                                                                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/delete.png"
                                                                                    CommandArgument=' <%#Eval("GETFILE") %>' AlternateText=' <%#Eval("APPID") %>' ToolTip="Delete Record"
                                                                                    OnClientClick="javascript:return confirm('Are you sure you want to delete this file?')" OnClick="btnDelFile_Click" />
                                                                            </td>
                                                                            <td id="attachfile" runat="server">
                                                                                <a target="_blank" class="mail_pg" href="DownloadAttachment.aspx?file=<%#Eval("DisplayFileName") %>&filename=<%# Eval("DisplayFileName")%>">
                                                                                    <%# Eval("DisplayFileName")%></a>
                                                                            </td>
                                                                            <td id="attachblob" runat="server" visible="false">
                                                                                <%# Eval("DisplayFileName")%></a>
                                                                            </td>

                                                                            <td id="tdDownloadLink" runat="server" visible="false">
                                                                                <img alt="Attachment" src="../IMAGES/attachment.png" />
                                                                                <%-- <a target="_blank" class="mail_pg" href="DownloadAttachment.aspx?file=<%#Eval("FILE_PATH") %>&filename=<%# Eval("FILE_NAME")%>">
                                                                                --%>      <%# Eval("DisplayFileName")%></a>&nbsp;&nbsp;
                                                            
                                                                            </td>
                                                                            <td style="text-align: center" id="tdBlob" runat="server" visible="false">
                                                                                <asp:UpdatePanel ID="updPreview" runat="server">
                                                                                    <ContentTemplate>
                                                                                        <asp:ImageButton ID="imgbtnPreview" runat="server" OnClick="imgbtnPreview_Click" Text="Preview" ImageUrl="~/Images/action_down.png" ToolTip='<%# Eval("FILENAME") %>'
                                                                                            data-toggle="modal" data-target="#preview" CommandArgument='<%# Eval("FILENAME") %>' Visible='<%# Convert.ToString(Eval("FILENAME"))==string.Empty?false:true %>'></asp:ImageButton>

                                                                                    </ContentTemplate>
                                                                                    <Triggers>
                                                                                        <asp:AsyncPostBackTrigger ControlID="imgbtnPreview" EventName="Click" />
                                                                                    </Triggers>
                                                                                </asp:UpdatePanel>

                                                                            </td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:ListView>
                                                            </asp:Panel>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="form-group col-md-12">
                                                    <p class="text-center">
                                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="ServiceBook" TabIndex="12"
                                                            OnClick="btnSubmit_Click" CssClass="btn btn-primary" ToolTip="Click here to Submit" />&nbsp;
                                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" TabIndex="13"
                                                                OnClick="btnCancel_Click" CssClass="btn btn-warning" ToolTip="Click here to Reset" />
                                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="ServiceBook"
                                                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                                    </p>
                                                </div>
                                    </asp:Panel>
                                </div>
                                <div class="col-md-12">
                                    <asp:Panel ID="pnlList" runat="server" ScrollBars="Auto">
                                        <asp:ListView ID="lvAchiveInfo" runat="server">
                                            <EmptyDataTemplate>
                                                <br />
                                                <p class="text-center text-bold">
                                                    <asp:Label ID="lblErrMsg" runat="server" SkinID="Errorlbl" Text="No Rows In Consultancy Details"></asp:Label>
                                                </p>
                                            </EmptyDataTemplate>
                                            <LayoutTemplate>
                                                <div class="sub-heading">
                                                    <h5>Consultancy Details
                                                    </h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                    <%--  <table class="table table-bordered table-hover">--%>
                                                    <thead>
                                                        <tr class="bg-light-blue">
                                                            <th>Action
                                                            </th>
                                                            <th>Title
                                                            </th>
                                                            <th>Name of Organization
                                                            </th>
                                                            <%--<th>Address
                                                                </th>--%>
                                                            <th>From Date
                                                            </th>
                                                            <th>To Date
                                                            </th>
                                                            <th>Duration
                                                            </th>
                                                            <th>Amount
                                                            </th>
                                                            <th>Nature of work
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
                                                    <%-- //Modified by Saahil Trivedi 20/07/2022--%>
                                                    <td>
                                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("SCNO")%>'
                                                            AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />&nbsp;
                                        <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Images/delete.png" CommandArgument='<%# Eval("SCNO") %>'
                                            AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                            OnClientClick="showConfirmDel(this); return false;" />
                                                    </td>
                                                    <td>
                                                        <%# Eval("TITLE")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("Name_Of_Org")%>
                                                    </td>
                                                    <%--<td>
                                                        <%# Eval("ORG_ADDRESS")%>
                                                    </td>--%>
                                                    <td>
                                                        <%# Eval("From_Date", "{0:dd/MM/yyyy}")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("To_Date", "{0:dd/MM/yyyy}")%>
                                                    </td>
                                                    <td>
                                                        <%--<%# Eval("Consultancy_Period", "{0:dd/MM/yyyy}")%>--%>
                                                        <%# Eval("Duration") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("Amount_Earned")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("Nature_of_work")%>
                                                    </td>
                                                    <%--<td>
                                                        <%# Eval("Description")%>
                                                    </td>--%>
                                                    <td>
                                                        <%--<asp:HyperLink ID="lnkDownload" runat="server" Target="_blank" NavigateUrl='<%# GetFileNamePath(Eval("ATTACHMENT"),Eval("ACNO"),Eval("IDNO"))%>'><%# Eval("ATTACHMENT")%></asp:HyperLink>--%>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                    </from>                       
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSubmit" />
            <asp:PostBackTrigger ControlID="btnAdd" />
        </Triggers>
    </asp:UpdatePanel>
    <ajaxToolKit:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mdlPopupDel"
        runat="server" TargetControlID="div" PopupControlID="div" OkControlID="btnOkDel"
        OnOkScript="okDelClick();" CancelControlID="btnNoDel" OnCancelScript="cancelDelClick();"
        BackgroundCssClass="modalBackground" />
    <div class="col-md-12">
        <asp:Panel ID="div" runat="server" Style="display: none" CssClass="modalPopup">
            <div class="text-center">
                <div class="modal-content">
                    <div class="modal-body">
                        <asp:Image ID="imgWarning" runat="server" ImageUrl="~/Images/warning.png" />
                        <td>&nbsp;&nbsp;Are you sure you want to delete this record?</td>
                        <div class="text-center">
                            <asp:Button ID="btnOkDel" runat="server" Text="Yes" CssClass="btn-primary" />
                            <asp:Button ID="btnNoDel" runat="server" Text="No" CssClass="btn-primary" />
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>
    </div>


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

        function toggleExpansion(imageCtl, divId) {
            if (document.getElementById(divId).style.display == "block") {
                document.getElementById(divId).style.display = "none";
                imageCtl.src = "../../images/expand_blue.jpg";
            }
            else if (document.getElementById(divId).style.display == "none") {
                document.getElementById(divId).style.display = "block";
                imageCtl.src = "../../images/collapse_blue.jpg";
            }
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


        function numericdotOnly(eventRef, elementRef) {
            var keyCodeEntered = (eventRef) ? eventRef.keyCode : (event.which) ? event.which : (window.event.keyCode) ? window.event.keyCode : -1;

            if (keyCodeEntered == 46) {
                // Allow only 1 decimal point ('.')...
                if ((elementRef.value) && (elementRef.value.indexOf('.') >= 0))
                    return false;
                else
                    return true;
            }
        }


        //function GetDateDiff() {
        //debugger;
        //var fDate = document.getElementById('<%= txtDateOftalk.ClientID %>');
        //var tDate = document.getElementById('<%= txtToDate.ClientID %>');

        //var FDates = fDate.value.split("/");
        //var Fd = new Date(FDates[2], FDates[1] - 1, FDates[0]);

        //var TDates = tDate.value.split("/");
        //var Td = new Date(TDates[2], TDates[1] - 1, TDates[0]);

        //if (Fd > Td) {
        //alert("To date should be greater than equal to From date");
        // endDate.value = "";
        //tDate.value = "";
        //}
        //};

    </script>
    <script type="text/javascript">
        debugger;
        function GetDateDiff() {

            var datejoing = document.getElementById('<%=txtDateOftalk.ClientID%>').value;
            var dateleaving = document.getElementById('<%=txtToDate.ClientID%>').value;
            if (datejoing != '' && dateleaving != '') {

                var dateElements = datejoing.split("/");
                var outputDateString = dateElements[2] + "/" + dateElements[1] + "/" + dateElements[0];
                var dateElementsnew = dateleaving.split("/");
                var outputDateStringnew = dateElementsnew[2] + "/" + dateElementsnew[1] + "/" + dateElementsnew[0];

                var date1 = new Date(outputDateString);
                var date2 = new Date(outputDateStringnew);

                if (Object.prototype.toString.call(date2) === "[object Date]") {
                    // it is a date
                    if (isNaN(date2.getTime())) {  // d.valueOf() could also work
                        document.getElementById('<%=txtConsultancy.ClientID%>').value = '';
                        return;
                    } else {
                        // date is valid
                    }
                } else {
                    // not a date
                }

                if (Object.prototype.toString.call(date1) === "[object Date]") {
                    // it is a date
                    if (isNaN(date1.getTime())) {  // d.valueOf() could also work
                        document.getElementById('<%=txtConsultancy.ClientID%>').value = '';
                        return;
                    } else {
                        // date is valid
                    }
                } else {
                    // not a date
                }

                if (date1 > date2) {
                    alert("To date should be greater than equal to from date.");
                    document.getElementById('<%=txtConsultancy.ClientID%>').value = '';
                    document.getElementById('<%=txtToDate.ClientID%>').value = '';
                    return;
                }

                dateDiff(date1, date2);
                //var timeDiff = Math.abs(parseInt(date1.getTime()) - parseInt(date2.getTime()));
                //var diffDays = Math.round(timeDiff / (1000 * 60 * 60 * 24));

                //var totalYears = Math.trunc(diffDays / 365);
                //var totalMonths = Math.trunc((diffDays % 365) / 30);
                //var totalDays = Math.trunc((diffDays % 365) % 30)
                //document.getElementById('<%=txtConsultancy.ClientID%>').value = totalYears + ' ' + 'Years' + ' ' + totalMonths + ' ' + 'Months' + ' ' + totalDays + ' ' + 'Days';
                //document.getElementById('<%=txtConsultancy.ClientID%>').value = totalYears + ' ' + 'Years' + ' ' + totalMonths + ' ' + 'Months';
            }
            else
                document.getElementById('<%=txtConsultancy.ClientID%>').value = '';
        }

        function dateDiff(startingDate, endingDate) {
            var startDate = new Date(new Date(startingDate).toISOString().substr(0, 10));
            if (!endingDate) {
                endingDate = new Date().toISOString().substr(0, 10);    // need date in YYYY-MM-DD format
            }
            var endDate = new Date(endingDate);
            if (startDate > endDate) {
                var swap = startDate;
                startDate = endDate;
                endDate = swap;
            }
            var startYear = startDate.getFullYear();
            var february = (startYear % 4 === 0 && startYear % 100 !== 0) || startYear % 400 === 0 ? 29 : 28;
            var daysInMonth = [31, february, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];

            var yearDiff = endDate.getFullYear() - startYear;
            var monthDiff = endDate.getMonth() - startDate.getMonth();
            if (monthDiff < 0) {
                yearDiff--;
                monthDiff += 12;
            }
            var dayDiff = endDate.getDate() - startDate.getDate();
            if (dayDiff < 0) {
                if (monthDiff > 0) {
                    monthDiff--;
                } else {
                    yearDiff--;
                    monthDiff = 11;
                }
                dayDiff += daysInMonth[startDate.getMonth()];
            }
            document.getElementById('<%=txtConsultancy.ClientID%>').value = yearDiff + ' ' + 'Years' + ' ' + monthDiff + ' ' + 'Months' + ' ' + dayDiff + ' ' + 'Days';
            return yearDiff + 'Y ' + monthDiff + 'M ' + dayDiff + 'D';
        }

        function EditControl(event, obj) {
            obj.style.backgroundColor = "LightGray";

            // document.getElementById('<%=txtConsultancy.ClientID%>').contentEditable.replace = false;
            document.getElementById('<%=txtConsultancy.ClientID%>').contentEditable = false;
            // alert("Not Editable");
            return false;
        }

    </script>

</asp:Content>

