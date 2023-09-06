<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Pay_Nomination.ascx.cs"
    Inherits="PayRoll_Pay_Nomination" %>
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
                                <div class="panel panel-heading">Nomination</div>
                                <div class="panel panel-body">
                                    <div class="form-group col-md-6">
                                        <label>Nomination For :</label>
                                        <asp:DropDownList ID="ddlNominationFor" AppendDataBoundItems="true" runat="server"
                                            CssClass="form-control" ToolTip="Select Nomination For" TabIndex="4">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfNominationFor" runat="server" ControlToValidate="ddlNominationFor"
                                            Display="None" ErrorMessage="Please Select Nomination For" ValidationGroup="ServiceBook"
                                            InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label>Name Of Nominee :</label>
                                        <asp:TextBox ID="txtNameOfNominee" runat="server" CssClass="form-control" TabIndex="5"
                                            ToolTip="Enter Name Of Nominee" onkeyup="return validateAlphabet(this);"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvNameOfNominee" runat="server" ControlToValidate="txtNameOfNominee"
                                            Display="None" ErrorMessage="Please Enter Name Of Nominee " ValidationGroup="ServiceBook"
                                            SetFocusOnError="True">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label>Relation :</label>
                                        <asp:TextBox ID="txtRelation" runat="server" CssClass="form-control" ToolTip="Enter Relation"
                                            onkeyup="return validateAlphabet(this);" TabIndex="6"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rvfRelation" runat="server" ControlToValidate="txtRelation"
                                            Display="None" ErrorMessage="Please Enter Relation " ValidationGroup="ServiceBook"
                                            SetFocusOnError="True">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label>Percentage :</label>
                                        <asp:TextBox ID="txtPercentage" runat="server" CssClass="form-control" ToolTip="Enter Percentage"
                                            MaxLength="3" onkeyup="return validateNumeric(this);" TabIndex="7"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvPercentage" runat="server" ControlToValidate="txtPercentage"
                                            Display="None" ErrorMessage="Please Enter Percentage" ValidationGroup="ServiceBook"
                                            SetFocusOnError="True">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label>Date of Birth :</label>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <asp:Image ID="imgDateOfBirth" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                            </div>
                                            <asp:TextBox ID="txtDateOfBirth" runat="server" CssClass="form-control" ToolTip="Enter Date of Birth"
                                                OnTextChanged="txtDateOfBirth_TextChanged" AutoPostBack="true" TabIndex="8" Style="z-index: 0;"></asp:TextBox>
                                            <ajaxToolKit:CalendarExtender ID="ceDateOfBirth" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtDateOfBirth" PopupButtonID="imgDateOfBirth" Enabled="true"
                                                EnableViewState="true" PopupPosition="BottomLeft">
                                            </ajaxToolKit:CalendarExtender>
                                            <ajaxToolKit:MaskedEditExtender ID="meDateOfBirth" runat="server" TargetControlID="txtDateOfBirth"
                                                Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                AcceptNegative="Left" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate">
                                            </ajaxToolKit:MaskedEditExtender>
                                            <ajaxToolKit:MaskedEditValidator ID="mevDateOfBirth" runat="server" ControlExtender="meDateOfBirth"
                                                ControlToValidate="txtDateOfBirth" EmptyValueMessage="Please Enter DateOfBirth"
                                                InvalidValueMessage="Date Of Birth is Invalid (Enter mm/dd/yyyy Format)" Display="None"
                                                TooltipMessage="Please Enter DateOfBirth" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                                ValidationGroup="ServiceBook" SetFocusOnError="True" />
                                        </div>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label>Age :</label>
                                        <asp:TextBox ID="txtAge" runat="server" CssClass="form-control" ToolTip="Total Age" MaxLength="3"
                                            onkeyup="return validateNumeric(this);" TabIndex="9" Enabled="false"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvAge" runat="server" ControlToValidate="txtAge"
                                            Display="None" ErrorMessage="Please Enter Age " ValidationGroup="ServiceBook"
                                            SetFocusOnError="True">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label>Contingencies Of The Happening :</label>
                                        <asp:TextBox ID="txtContingencies" runat="server" CssClass="form-control" TabIndex="10"
                                            ToolTip="Enter Contingencies Of The Happening"></asp:TextBox>
                                    </div>
                                      <div class="form-group col-md-6">
                                        <label>Upload Document :</label>
                                          <br />
                                           <br />    <br /> 
                                        <asp:FileUpload ID="flupld" runat="server" TabIndex="11" ToolTip="Upload Document" />
                                    </div>                                                                     
                                    <div class="form-group col-md-6">
                                        <label>Address :</label>
                                        <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" TabIndex="12"
                                            ToolTip="Enter Address" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label>Remarks :</label>
                                        <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control" TabIndex="13"
                                            ToolTip="Enter Remarks" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-6" id="trchk" runat="server" visible="false">
                                        <label>Last Nominee :</label>
                                        <asp:CheckBox ID="chkLastNominee" runat="server" TabIndex="14" ToolTip="Check if Last Nominee" />
                                    </div>
                                  
                                    <div class="form-group col-md-12">
                                        <br />
                                        <p class="text-center">
                                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="ServiceBook" TabIndex="15"
                                                OnClick="btnSubmit_Click" CssClass="btn btn-primary" ToolTip="Click here to Submit" />&nbsp;
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" TabIndex="16"
                                                OnClick="btnCancel_Click" CssClass="btn btn-warning" ToolTip="Click here to Reset" />
                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="ServiceBook"
                                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                        </p>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                    <div class="col-md-6">
                        <asp:Panel ID="pnlNomination" runat="server" ScrollBars="Auto">
                            <asp:ListView ID="lvNomination" runat="server">
                                <EmptyDataTemplate>
                                    <br />
                                    <p class="text-center text-bold">
                                        <asp:Label ID="lblErrMsg" runat="server" SkinID="Errorlbl" Text="No Rows In Nomination of Employee"></asp:Label>
                                    </p>
                                </EmptyDataTemplate>
                                <LayoutTemplate>
                                    <div id="lgv1">
                                        <h4 class="box-title">Nomination Details
                                        </h4>
                                        <table class="table table-bordered table-hover">
                                            <thead>
                                                <tr class="bg-light-blue">
                                                    <th>Action
                                                    </th>
                                                    <th>Nomin.For
                                                    </th>
                                                    <th>Nominee
                                                    </th>
                                                    <th>Relation
                                                    </th>
                                                    <th>Per.
                                                    </th>
                                                    <th>DOB
                                                    </th>
                                                    <th>Age
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
                                            <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("nfno")%>'
                                                AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />&nbsp;
                                        <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.gif" CommandArgument='<%# Eval("nfno") %>'
                                            AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                            OnClientClick="showConfirmDel(this); return false;" />
                                        </td>
                                        <td>
                                            <%# Eval("NOMINITYPE")%>
                                        </td>
                                        <td>
                                            <%# Eval("name")%>
                                        </td>
                                        <td>
                                            <%# Eval("relation")%>
                                        </td>
                                        <td>
                                            <%# Eval("per")%>
                                        </td>
                                        <td>
                                            <%# Eval("dob", "{0:dd/MM/yyyy}")%>
                                        </td>
                                        <td>
                                            <%# Eval("Age")%>
                                        </td>
                                        <td>
                                            <asp:HyperLink ID="lnkDownload" runat="server" Target="_blank" NavigateUrl='<%# GetFileNamePath(Eval("ATTACHMENT"),Eval("NFNO"),Eval("IDNO"))%>'><%# Eval("ATTACHMENT")%></asp:HyperLink>
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
            <%--<asp:Panel ID="pnlAdd" runat="server" Style="text-align: left; width: 95%; padding-left: 5px;">
                <fieldset class="fieldsetPay">
                    <legend class="legendPay">Nomination</legend>
                    <br />
                    <table cellpadding="0" cellspacing="0" style="width: 100%;">
                        <tr>
                            <td class="form_left_label" width="30%">
                                Nomination For :
                            </td>
                            <td class="form_left_text">
                                <asp:DropDownList ID="ddlNominationFor" AppendDataBoundItems="true" runat="server"
                                    Width="200px">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfNominationFor" runat="server" ControlToValidate="ddlNominationFor"
                                    Display="None" ErrorMessage="Please Select Nomination For" ValidationGroup="ServiceBook"
                                    InitialValue="0"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="form_left_label">
                                Name Of Nominee :
                            </td>
                            <td class="form_left_text">
                                <asp:TextBox ID="txtNameOfNominee" runat="server" Width="200px" onkeyup="return validateAlphabet(this);"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvNameOfNominee" runat="server" ControlToValidate="txtNameOfNominee"
                                    Display="None" ErrorMessage="Please Enter Name Of Nominee " ValidationGroup="ServiceBook"
                                    SetFocusOnError="True">
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="form_left_label">
                                Relation :
                            </td>
                            <td class="form_left_text">
                                <asp:TextBox ID="txtRelation" runat="server" Width="200px" onkeyup="return validateAlphabet(this);"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rvfRelation" runat="server" ControlToValidate="txtRelation"
                                    Display="None" ErrorMessage="Please Enter Relation " ValidationGroup="ServiceBook"
                                    SetFocusOnError="True">
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="form_left_label">
                                Percentage :
                            </td>
                            <td class="form_left_text">
                                <asp:TextBox ID="txtPercentage" runat="server" Width="50px" MaxLength="3" onkeyup="return validateNumeric(this);"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvPercentage" runat="server" ControlToValidate="txtPercentage"
                                    Display="None" ErrorMessage="Please Enter Percentage" ValidationGroup="ServiceBook"
                                    SetFocusOnError="True">
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="form_left_label">
                                DOB :
                            </td>
                            <td class="form_left_text">
                               <asp:TextBox ID="txtDateOfBirth" runat="server" Width="80px" OnTextChanged="txtDateOfBirth_TextChanged" AutoPostBack="true"></asp:TextBox>
                                &nbsp;<asp:Image ID="imgDateOfBirth" runat="server" ImageUrl="~/images/calendar.png"
                                    Style="cursor: pointer" />
                                <ajaxToolKit:CalendarExtender ID="ceDateOfBirth" runat="server" Format="dd/MM/yyyy"
                                    TargetControlID="txtDateOfBirth" PopupButtonID="imgDateOfBirth" Enabled="true"
                                    EnableViewState="true" PopupPosition="BottomLeft">
                                </ajaxToolKit:CalendarExtender>--%>
            <%--Already Committed<asp:RequiredFieldValidator ID="rfvDateOfBirth" runat="server" ControlToValidate="txtDateOfBirth"
                                    Display="None" ErrorMessage="Please Select Date Of Birth in (dd/MM/yyyy Format)"
                                    ValidationGroup="ServiceBook" SetFocusOnError="True">
                                </asp:RequiredFieldValidator>--%>
            <%--<ajaxToolKit:MaskedEditExtender ID="meDateOfBirth" runat="server" TargetControlID="txtDateOfBirth"
                                    Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                    AcceptNegative="Left" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate">
                                </ajaxToolKit:MaskedEditExtender>
                                <ajaxToolKit:MaskedEditValidator ID="mevDateOfBirth" runat="server" ControlExtender="meDateOfBirth"
                                    ControlToValidate="txtDateOfBirth" EmptyValueMessage="Please Enter DateOfBirth"
                                    InvalidValueMessage="Date Of Birth is Invalid (Enter mm/dd/yyyy Format)" Display="None"
                                    TooltipMessage="Please Enter DateOfBirth" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                    ValidationGroup="ServiceBook" SetFocusOnError="True" />
                            </td>
                        </tr>
                        <tr>
                            <td class="form_left_label">
                                Age :
                            </td>
                            <td class="form_left_text">
                                <asp:TextBox ID="txtAge" runat="server" Width="30px" MaxLength="3" onkeyup="return validateNumeric(this);"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvAge" runat="server" ControlToValidate="txtAge"
                                    Display="None" ErrorMessage="Please Enter Age " ValidationGroup="ServiceBook"
                                    SetFocusOnError="True">
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="form_left_label">
                                Contingencies Of The Happening :
                            </td>
                            <td class="form_left_text">
                                <asp:TextBox ID="txtContingencies" runat="server" Width="200px"></asp:TextBox>
                                
                            </td>
                        </tr>
                        <tr>
                            <td class="form_left_label">
                                Address :
                            </td>
                            <td class="form_left_text">
                                <asp:TextBox ID="txtAddress" runat="server" Width="200px" TextMode="MultiLine"></asp:TextBox>
                                
                            </td>
                        </tr>
                        <tr>
                            <td class="form_left_label">
                                Remarks :
                            </td>
                            <td class="form_left_text">
                                <asp:TextBox ID="txtRemarks" runat="server" Width="200px" TextMode="MultiLine"></asp:TextBox>
                            
                            </td>
                        </tr>
                        <tr id="trchk" runat="server" visible="false">
                            <td class="form_left_label">
                                Last Nominee :
                            </td>
                            <td class="form_left_text">
                                <asp:CheckBox ID="chkLastNominee" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="form_left_label">
                                Upload Document :
                            </td>
                            <td class="form_left_text">
                                <asp:FileUpload ID="flupld" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp
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
                            <td>
                                &nbsp
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
                        <%--<asp:ListView ID="lvNomination" runat="server">
                            <EmptyDataTemplate>
                                <br />
                                <asp:Label ID="lblErrMsg" runat="server" SkinID="Errorlbl" Text="No Rows In Nomination of Employee"></asp:Label>
                            </EmptyDataTemplate>
                            <LayoutTemplate>
                                <div class="vista-gridServiceBook">
                                    <div class="titlebar-ServiceBook">
                                        Nomination
                                    </div>
                                    <table class="datatable-ServiceBook" cellpadding="0" cellspacing="0" style="width: 100%;">
                                        <thead>
                                            <tr class="header-ServiceBook">
                                                <th width="10%">Action
                                                </th>
                                                <th width="10%">Nomin.For
                                                </th>
                                                <th width="10%">Nominee
                                                </th>
                                                <th width="10%">Relation
                                                </th>
                                                <th width="10%">Per.
                                                </th>
                                                <th width="10%">DOB
                                                </th>
                                                <th width="10%">Age
                                                </th>
                                                <th width="15%" align="left">Attachment
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
                                <tr class="item-ServiceBook" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                    <td width="10%" align="left">
                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("nfno")%>'
                                            AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />&nbsp;
                                        <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.gif" CommandArgument='<%# Eval("nfno") %>'
                                            AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                            OnClientClick="showConfirmDel(this); return false;" />
                                    </td>
                                    <td width="10%">
                                        <%# Eval("NOMINITYPE")%>
                                    </td>
                                    <td width="10%">
                                        <%# Eval("name")%>
                                    </td>
                                    <td width="10%">
                                        <%# Eval("relation")%>
                                    </td>
                                    <td width="10%">
                                        <%# Eval("per")%>
                                    </td>
                                    <td width="10%">
                                        <%# Eval("dob", "{0:dd/MM/yyyy}")%>
                                    </td>
                                    <td width="10%">
                                        <%# Eval("Age")%>
                                    </td>
                                    <td width="15%" align="left">
                                        <asp:HyperLink ID="lnkDownload" runat="server" Target="_blank" NavigateUrl='<%# GetFileNamePath(Eval("ATTACHMENT"),Eval("NFNO"),Eval("IDNO"))%>'><%# Eval("ATTACHMENT")%></asp:HyperLink>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <tr class="altitem-ServiceBook" onmouseover="this.style.backgroundColor='#FFFFAA'"
                                    onmouseout="this.style.backgroundColor='#FFFFD2'">
                                    <td width="10%">
                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("nfno")%>'
                                            AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />&nbsp;
                                        <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.gif" CommandArgument='<%# Eval("nfno") %>'
                                            AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                            OnClientClick="showConfirmDel(this); return false;" />
                                    </td>
                                    <td width="10%">
                                        <%# Eval("NOMINITYPE")%>
                                    </td>
                                    <td width="10%">
                                        <%# Eval("name")%>
                                    </td>
                                    <td width="10%">
                                        <%# Eval("relation")%>
                                    </td>
                                    <td width="10%">
                                        <%# Eval("per")%>
                                    </td>
                                    <td width="10%">
                                        <%# Eval("dob", "{0:dd/MM/yyyy}")%>
                                    </td>
                                    <td width="10%">
                                        <%# Eval("Age")%>
                                    </td>
                                    <td width="15%" align="left">
                                        <asp:HyperLink ID="lnkDownload" runat="server" Target="_blank" NavigateUrl='<%# GetFileNamePath(Eval("ATTACHMENT"),Eval("NFNO"),Eval("IDNO"))%>'><%# Eval("ATTACHMENT")%></asp:HyperLink>
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
<%--BELOW CODE IS TO SHOW THE MODAL POPUP EXTENDER FOR DELETE CONFIRMATION--%>
<%--DONT CHANGE THE CODE BELOW. USE AS IT IS--%>
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

    function validateNumeric(txt) {
        if (isNaN(txt.value)) {
            txt.value = txt.value.substring(0, (txt.value.length) - 1);
            txt.value = '';
            txt.focus = true;
            alert("Only Numeric Characters allowed !");
            return false;
        }
        else
            return true;
    }

    function validateAlphabet(txt) {
        var expAlphabet = /^[A-Za-z .]+$/;
        if (txt.value.search(expAlphabet) == -1) {
            txt.value = txt.value.substring(0, (txt.value.length) - 1);
            txt.value = '';
            txt.focus = true;
            alert("Only Alphabets allowed!");
            return false;
        }
        else
            return true;
    }

</script>

