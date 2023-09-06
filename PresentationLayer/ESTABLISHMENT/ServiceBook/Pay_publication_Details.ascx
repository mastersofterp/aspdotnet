<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Pay_publication_Details.ascx.cs"
    Inherits="PAYROLL_TRANSACTIONS_Pay_publication_Details" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<link href="../Css/master.css" rel="stylesheet" type="text/css" />
<link href="../Css/Theme1.css" rel="stylesheet" type="text/css" />

<br />
<div class="row">
    <div class="col-md-12">
        <form role="form">
            <div class="box-body">
                <div class="col-md-12">
                    <div class="col-md-6">
                        <asp:Panel ID="pnlAdd" runat="server">
                            <div class="panel panel-info">
                                <div class="panel panel-heading">Publication Details</div>
                                <div class="panel panel-body">
                                    <div class="form-group col-md-6">
                                        <label>Publication Category :</label>
                                        <asp:DropDownList ID="ddlPublication" runat="server" CssClass="form-control"
                                            TabIndex="4" ToolTip="Select Publication Category">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <asp:ListItem Value="Conference">Conference</asp:ListItem>
                                            <asp:ListItem Value="Journal">Journal</asp:ListItem>
                                            <asp:ListItem Value="Book">Book Chapter</asp:ListItem>
                                            <%--<asp:ListItem Value="Paper">Paper</asp:ListItem>--%>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlPublication"
                                            Display="None" ErrorMessage="Please Enter Publication Category" ValidationGroup="ServiceBook" InitialValue="0"
                                            SetFocusOnError="True"> </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label>Publication Type :</label>
                                        <asp:DropDownList ID="ddlPublicationType" runat="server" CssClass="form-control"
                                            ToolTip="Select Publication Type" TabIndex="5">
                                            <asp:ListItem Value="National">National</asp:ListItem>
                                            <asp:ListItem Value="InterNational">Inter National</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-md-12">
                                        <label>Name Of The Authors :</label>
                                        <asp:TextBox ID="txtAuthor" runat="server" CssClass="form-control" Enabled="false"
                                            ToolTip="Name Of The Authors 1" TabIndex="6"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-12">
                                        <asp:TextBox ID="txtAuthor2" runat="server" CssClass="form-control" TabIndex="7" ToolTip="Enter Name Of The Authors 2"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-12">
                                        <asp:TextBox ID="txtAuthor3" runat="server" CssClass="form-control" TabIndex="8" ToolTip="Enter Name Of The Authors 3"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-12">
                                        <asp:TextBox ID="txtAuthor4" runat="server" CssClass="form-control" TabIndex="9" ToolTip="Enter Name Of The Authors 3"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-12">
                                        <label>Title of Paper :</label>
                                        <asp:TextBox ID="txttitle" runat="server" CssClass="form-control" TabIndex="9" ToolTip="Enter Title"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtitle" runat="server" ControlToValidate="txttitle"
                                            Display="None" ErrorMessage="Please Enter Title" ValidationGroup="ServiceBook"
                                            SetFocusOnError="True"> </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-md-12" id="trnamejou" runat="server" visible="false">
                                        <label>Name of Journal/Conference :</label>
                                        <asp:TextBox ID="txtName" runat="server" CssClass="form-control" ToolTip="Enter Name of Journal/Conference" TabIndex="10"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-12" id="trOrg" runat="server" visible="false">
                                        <label>Organised By :</label>
                                        <asp:TextBox ID="txtOrg" runat="server" CssClass="form-control" ToolTip="Enter Organised By" TabIndex="11"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-12">
                                        <label>Title of Journal :</label>
                                        <asp:TextBox ID="txtSubject" runat="server" CssClass="form-control" ToolTip="Enter Subject" TabIndex="12"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvSubject" runat="server" ControlToValidate="txtSubject"
                                            Display="None" ErrorMessage="Please Enter  Subject" ValidationGroup="ServiceBook"
                                            SetFocusOnError="True"> </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-md-12" id="trPageno" runat="server" visible="false">
                                        <label>Page NO :</label>
                                        <asp:TextBox ID="txtPage" runat="server" CssClass="form-control" ToolTip="Enter Page Number" TabIndex="13"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-12" id="Div1" >
                                        <label>ISBN :</label>
                                        <asp:TextBox ID="txtIsbn" runat="server" CssClass="form-control" ToolTip="Enter Page Number" TabIndex="13"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-12">
                                        <label>Publication Date :</label>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <asp:Image ID="Image1" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                            </div>
                                            <asp:TextBox ID="txtPublicationDate" runat="server" CssClass="form-control" ToolTip="Enter Publication Date"
                                                TabIndex="14" Style="z-index: 0;"></asp:TextBox>
                                            <ajaxToolKit:CalendarExtender ID="ceToDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtPublicationDate"
                                                PopupButtonID="Image1" Enabled="true" EnableViewState="true" PopupPosition="BottomLeft">
                                            </ajaxToolKit:CalendarExtender>
                                            <%--<asp:RequiredFieldValidator ID="rfvPublicationDate" runat="server" ControlToValidate="txtPublicationDate"
                                                        Display="None" ErrorMessage="Please Select Publication Date in (dd/MM/yyyy Format)"
                                                        ValidationGroup="ServiceBook" SetFocusOnError="True"> </asp:RequiredFieldValidator>--%>
                                            <ajaxToolKit:MaskedEditExtender ID="meToDate" runat="server" TargetControlID="txtPublicationDate"
                                                Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                AcceptNegative="Left" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate">
                                            </ajaxToolKit:MaskedEditExtender>
                                            <ajaxToolKit:MaskedEditValidator ID="mevToDate" runat="server" ControlExtender="meToDate"
                                                ControlToValidate="txtPublicationDate" EmptyValueMessage="Please Enter Publication Date"
                                                InvalidValueMessage="Publication Date is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                                TooltipMessage="Please Enter Publication Date" EmptyValueBlurredText="Empty"
                                                InvalidValueBlurredMessage="Invalid Date" ValidationGroup="ServiceBook" SetFocusOnError="True" />
                                        </div>
                                    </div>
                                    <div class="form-group col-md-12">
                                        <label>Abstract :</label>
                                        <asp:TextBox ID="txtDetails" runat="server" CssClass="form-control" ToolTip="Enter Details"
                                            TabIndex="15" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-12">
                                        <label>Attachments :</label>
                                        <asp:FileUpload ID="flPUB" runat="server" ToolTip="Click here to Upload Attachments" TabIndex="16"/>
                                    </div>
                                    <div class="form-group col-md-12">
                                        <p class="text-center">
                                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="ServiceBook" TabIndex="17"
                                                    OnClick="btnSubmit_Click" CssClass="btn btn-success" ToolTip="Click here to Submit"/>&nbsp;
                                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" TabIndex="18"
                                                    OnClick="btnCancel_Click" CssClass="btn btn-danger" ToolTip="Click here to Reset"/>
                                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="ServiceBook"
                                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                           </p>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                    <div class="col-md-6">
                        <asp:Panel ID="pnlList" runat="server" ScrollBars="Auto">
                            <asp:ListView ID="lvPublicationDetails" runat="server">
                                <EmptyDataTemplate>
                                    <br />
                                    <p class="text-center text-bold">
                                        <asp:Label ID="lblErrMsg" runat="server" SkinID="Errorlbl" Text="No Rows In Publication Details"></asp:Label>
                                    </p>
                                </EmptyDataTemplate>
                                <LayoutTemplate>
                                    <div id="lgv1">
                                        <h4 class="box-title">Publication Details
                                        </h4>
                                        <table class="table table-bordered table-hover">
                                            <thead>
                                                <tr class="bg-light-blue">
                                                    <th>Action
                                                    </th>
                                                    <th>Publication Type
                                                    </th>
                                                    <th>Title
                                                    </th>
                                                    <th>Subject
                                                    </th>
                                                    <th>Publication Date
                                                    </th>
                                                    <th>Uploaded File
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr id="itemPlaceholder" runat="server" />
                                            </tbody>
                                        </table>
                                    </div>
                                    </div>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("PUBTRXNO")%>'
                                                AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />&nbsp;
                                        <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.gif" CommandArgument='<%# Eval("PUBTRXNO") %>'
                                            AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                            OnClientClick="showConfirmDel(this); return false;" />
                                        </td>
                                        <td>
                                            <%# Eval("PUBLICATION_TYPE")%>
                                        </td>
                                        <td>
                                            <%# Eval("TITLE")%>
                                        </td>
                                        <td>
                                            <%# Eval("SUBJECT")%>
                                        </td>
                                        <td>
                                            <%# Eval("PUBLICATIONDATE", "{0:dd/MM/yyyy}")%>
                                        </td>
                                        <td>
                                            <asp:HyperLink ID="lnkDownload" runat="server" Target="_blank" NavigateUrl='<%# GetFileNamePath(Eval("ATTACHMENT"),Eval("PUBTRXNO"),Eval("IDNO"))%>'><%# Eval("ATTACHMENT")%></asp:HyperLink>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </form>
    </div>
</div>
<table cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td>&nbsp
        </td>
    </tr>
    <tr>
        <td valign="top" width="50%">
            <%--<asp:Panel ID="pnlAdd" runat="server" Style="text-align: left; width: 95%; padding-left: 15px;">
                <fieldset class="fieldsetPay">
                    <legend class="legendPay">Publication Details</legend>
                    <br />
                    <table cellpadding="0" cellspacing="0" style="width: 100%;">
                        <tr>
                            <td class="form_left_label">Publication Category :
                            </td>
                            <td class="form_left_text">
                                <asp:DropDownList ID="ddlPublication" runat="server" Width="200px">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    <asp:ListItem Value="Conference">Conference</asp:ListItem>
                                    <asp:ListItem Value="Journal">Journal</asp:ListItem>
                                    <asp:ListItem Value="Novels">Novels</asp:ListItem>
                                    <asp:ListItem Value="Paper">Paper</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlPublication"
                                    Display="None" ErrorMessage="Please Enter Publication Category" ValidationGroup="ServiceBook" InitialValue="0"
                                    SetFocusOnError="True"> </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="form_left_label">Publication Type :
                            </td>
                            <td class="form_left_text">
                                <asp:DropDownList ID="ddlPublicationType" runat="server" Width="200px">
                                    <asp:ListItem Value="National">National</asp:ListItem>
                                    <asp:ListItem Value="InterNational">InterNational</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="form_left_label">Name Of The Authors :
                            </td>
                            <td class="form_left_text">
                                <asp:TextBox ID="txtAuthor" runat="server" Width="200px"></asp:TextBox>

                            </td>
                        </tr>
                        <tr>
                            <td class="form_left_label"></td>
                            <td class="form_left_text">
                                <asp:TextBox ID="txtAuthor2" runat="server" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="form_left_label"></td>
                            <td class="form_left_text">
                                <asp:TextBox ID="txtAuthor3" runat="server" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="form_left_label">Title :
                            </td>
                            <td class="form_left_text">
                                <asp:TextBox ID="txttitle" runat="server" Width="200px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvtitle" runat="server" ControlToValidate="txttitle"
                                    Display="None" ErrorMessage="Please Enter Title" ValidationGroup="ServiceBook"
                                    SetFocusOnError="True"> </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr id="trnamejou" runat="server" visible="false">
                            <td class="form_left_label">Name of Journal/Conference :
                            </td>
                            <td class="form_left_text">
                                <asp:TextBox ID="txtName" runat="server" Width="200px"></asp:TextBox>

                            </td>
                        </tr>
                        <tr id="trOrg" runat="server" visible="false">
                            <td class="form_left_label">Organised By :
                            </td>
                            <td class="form_left_text">
                                <asp:TextBox ID="txtOrg" runat="server" Width="200px"></asp:TextBox>

                            </td>
                        </tr>
                        <tr>
                            <td class="form_left_label">Subject :
                            </td>
                            <td class="form_left_text">
                                <asp:TextBox ID="txtSubject" runat="server" Width="200px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvSubject" runat="server" ControlToValidate="txtSubject"
                                    Display="None" ErrorMessage="Please Enter  Subject" ValidationGroup="ServiceBook"
                                    SetFocusOnError="True"> </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr id="trPageno" runat="server" visible="false">
                            <td class="form_left_label">Page NO :
                            </td>
                            <td class="form_left_text">
                                <asp:TextBox ID="txtPage" runat="server" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="form_left_label">Publication Date :
                            </td>
                            <td class="form_left_text">
                                <asp:TextBox ID="txtPublicationDate" runat="server" Width="80px"></asp:TextBox>
                                &nbsp;<asp:Image ID="Image1" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                <ajaxToolKit:CalendarExtender ID="ceToDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtPublicationDate"
                                    PopupButtonID="Image1" Enabled="true" EnableViewState="true" PopupPosition="BottomLeft">
                                </ajaxToolKit:CalendarExtender>--%>
            <%--Already Committed<asp:RequiredFieldValidator ID="rfvPublicationDate" runat="server" ControlToValidate="txtPublicationDate"
                                    Display="None" ErrorMessage="Please Select Publication Date in (dd/MM/yyyy Format)"
                                    ValidationGroup="ServiceBook" SetFocusOnError="True"> </asp:RequiredFieldValidator>--%>
            <%-- <ajaxToolKit:MaskedEditExtender ID="meToDate" runat="server" TargetControlID="txtPublicationDate"
                                    Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                    AcceptNegative="Left" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate">
                                </ajaxToolKit:MaskedEditExtender>
                                <ajaxToolKit:MaskedEditValidator ID="mevToDate" runat="server" ControlExtender="meToDate"
                                    ControlToValidate="txtPublicationDate" EmptyValueMessage="Please Enter Publication Date"
                                    InvalidValueMessage="Publication Date is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                    TooltipMessage="Please Enter Publication Date" EmptyValueBlurredText="Empty"
                                    InvalidValueBlurredMessage="Invalid Date" ValidationGroup="ServiceBook" SetFocusOnError="True" />
                            </td>
                        </tr>
                        <tr>
                            <td class="form_left_label">Details :
                            </td>
                            <td class="form_left_text">
                                <asp:TextBox ID="txtDetails" runat="server" Width="200px" TextMode="MultiLine"></asp:TextBox>

                            </td>
                        </tr>
                        <tr>
                            <td class="form_left_label">Attachments :
                            </td>
                            <td class="form_left_text">
                                <asp:FileUpload ID="flPUB" runat="server" Width="300px" />

                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="ServiceBook"
                                    OnClick="btnSubmit_Click" Width="80px" />&nbsp;
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                                    OnClick="btnCancel_Click" Width="80px" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="ServiceBook"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </asp:Panel>--%>
        </td>
        <td colspan="2" align="center" valign="top">
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td align="center">
                        <%-- <asp:ListView ID="lvPublicationDetails" runat="server">
                            <EmptyDataTemplate>
                                <br />
                                <asp:Label ID="lblErrMsg" runat="server" SkinID="Errorlbl" Text="No Rows In Publication Details"></asp:Label>
                            </EmptyDataTemplate>
                            <LayoutTemplate>
                                <div class="vista-gridServiceBook">
                                    <div class="titlebar-ServiceBook">
                                        Publication Details
                                    </div>
                                    <table class="datatable-ServiceBook" cellpadding="0" cellspacing="0" style="width: 100%;">
                                        <thead>
                                            <tr class="header-ServiceBook">
                                                <th width="10%">Action
                                                </th>
                                                <th width="10%">Publication Type
                                                </th>
                                                <th width="10%">Title
                                                </th>
                                                <th width="10%">Subject
                                                </th>
                                                <th width="10%">Publication Date
                                                </th>
                                                <th width="10%">Uploaded File
                                                </th>
                                            </tr>
                                            <thead>
                                    </table>
                                </div>
                                <div class="listview-container-servicebook">
                                    <div id="Div1" class="vista-gridServiceBook">
                                        <table class="datatable-ServiceBook" cellpadding="0" cellspacing="0" style="width: 100%;">
                                            <tbody>
                                                <tr id="itemPlaceholder" runat="server" />
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                                </div>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr class="altitem-ServiceBook" onmouseover="this.style.backgroundColor='#FFFFAA'"
                                    onmouseout="this.style.backgroundColor='#FFFFD2'">
                                    <td width="10%" align="left">
                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("PUBTRXNO")%>'
                                            AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />&nbsp;
                                        <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.gif" CommandArgument='<%# Eval("PUBTRXNO") %>'
                                            AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                            OnClientClick="showConfirmDel(this); return false;" />
                                    </td>
                                    <td width="10%">
                                        <%# Eval("PUBLICATION_TYPE")%>
                                    </td>
                                    <td width="10%">
                                        <%# Eval("TITLE")%>
                                    </td>
                                    <td width="10%">
                                        <%# Eval("SUBJECT")%>
                                    </td>
                                    <td width="10%">
                                        <%# Eval("PUBLICATIONDATE", "{0:dd/MM/yyyy}")%>
                                    </td>
                                    <td width="10%">
                                        <asp:HyperLink ID="lnkDownload" runat="server" Target="_blank" NavigateUrl='<%# GetFileNamePath(Eval("ATTACHMENT"),Eval("PUBTRXNO"),Eval("IDNO"))%>'><%# Eval("ATTACHMENT")%></asp:HyperLink>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <tr class="item-ServiceBook" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                    <td width="10%" align="left">
                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("PUBTRXNO")%>'
                                            AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />&nbsp;
                                        <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.gif" CommandArgument='<%# Eval("PUBTRXNO") %>'
                                            AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                            OnClientClick="showConfirmDel(this); return false;" />
                                    </td>
                                    <td width="10%">
                                        <%# Eval("PUBLICATION_TYPE")%>
                                    </td>
                                    <td width="10%">
                                        <%# Eval("TITLE")%>
                                    </td>
                                    <td width="10%">
                                        <%# Eval("SUBJECT")%>
                                    </td>
                                    <td width="10%">
                                        <%# Eval("PUBLICATIONDATE", "{0:dd/MM/yyyy}")%>
                                    </td>
                                    <td width="10%">
                                        <asp:HyperLink ID="lnkDownload" runat="server" Target="_blank" NavigateUrl='<%# GetFileNamePath(Eval("ATTACHMENT"),Eval("PUBTRXNO"),Eval("IDNO"))%>'><%# Eval("ATTACHMENT")%></asp:HyperLink>
                                    </td>
                                </tr>
                            </AlternatingItemTemplate>
                        </asp:ListView>--%>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>&nbsp
        </td>
    </tr>
</table>
<%-- <th width="10%">
                                                    Remarks
                                                </th>--%> <%# Eval("PUBLICATION_TYPE")%>
<ajaxToolKit:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mdlPopupDel"
    runat="server" TargetControlID="div" PopupControlID="div" OkControlID="btnOkDel"
    OnOkScript="okDelClick();" CancelControlID="btnNoDel" OnCancelScript="cancelDelClick();"
    BackgroundCssClass="modalBackground" />
<div class="col-md-12">
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
</script>

