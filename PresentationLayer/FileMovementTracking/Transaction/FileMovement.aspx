<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="FileMovement.aspx.cs" Inherits="FileMovementTracking_Transaction_FileMovement" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--   <script src="../../JAVASCRIPTS/JScriptAdmin_Module.js" type="text/javascript"></script>--%>
    <style>
        .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>
    <script type="text/javascript">
        debugger
        function clientShowing(source, args) {
            source._popupBehavior._element.style.zIndex = 100000;
        }
    </script>
    
   <%-- <style>
        .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>--%>
    <%-- <asp:UpdatePanel ID="updActivity" runat="server">
        <ContentTemplate>--%>
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">FILE MOVEMENT TRACKING</h3>
                </div>
                <div>
                    <div class="box-body">
                        <div class="col-12">
                            <asp:Panel ID="pnlFile" runat="server" Visible="false">
                                <div class="sub-heading">
                                    <h5>File Movement</h5>
                                </div>
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Section</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSection" CssClass="form-control" runat="server" TabIndex="1"
                                            AppendDataBoundItems="true" ToolTip="Select Section" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlSec" runat="server" ErrorMessage="Please Select Section."
                                            ControlToValidate="ddlSection" InitialValue="0"
                                            Display="None" ValidationGroup="Add"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-1 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label></label>
                                        </div>
                                        <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" TabIndex="1" ValidationGroup="Add"
                                            CssClass="btn btn-primary" ToolTip="Click here to Add Section" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>File Path </label>
                                        </div>
                                        <asp:Label ID="lblPath" runat="server" CssClass="form-control" ToolTip="File Path" Enabled="false"></asp:Label>
                                    </div>
                                    <div class="form-group col-lg-5 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Note </label>
                                        </div>
                                        <asp:TextBox ID="txtRemark" runat="server" TabIndex="1" CssClass="form-control" ToolTip="Enter Note"
                                            TextMode="MultiLine" />
                                        <asp:RequiredFieldValidator ID="rfvRemark" runat="server" ControlToValidate="txtRemark"
                                            Display="None" ErrorMessage="Please enter note." ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                    </div>
                                </div>

                            </asp:Panel>
                        </div>
                        <div class="form-group col-12">
                            <asp:Panel ID="lvPanel" runat="server" Visible="false">
                                <asp:ListView ID="lvMovRoute" runat="server">
                                    <LayoutTemplate>
                                        <div id="lgv1">
                                            <div class="sub-heading">
                                                <h5>Section List For Path</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                <thead>
                                                    <tr class="bg-light-blue">
                                                        <th>DELETE</th>
                                                        <th>SECTION NAME</th>
                                                        <th></th>
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
                                                <asp:ImageButton ID="btnDelete" runat="server" CommandArgument='<%# Eval("SRNO") %>' ImageUrl="~/Images/delete.png" OnClick="btnDelete_Click" ToolTip="Delete Record" />
                                            </td>
                                            <td><%# Eval("SECTIONNAME") %></td>
                                            <td>
                                                <asp:Label ID="lblSction" runat="server" Visible="false" Text='<%# Eval("SECTIONID")%>'></asp:Label>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </asp:Panel>
                        </div>
                        <div class="col-12">
                            <asp:Panel ID="pnlView" runat="server" Visible="false">
                                <div class="sub-heading">
                                    <h5>Movement Details</h5>
                                </div>
                                <asp:UpdatePanel ID="updActivity" runat="server">
                                    <ContentTemplate>
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label></label>
                                                </div>
                                                <asp:RadioButton ID="rbtForward" runat="server" Checked="false" Text="Forward" AutoPostBack="true"
                                                    OnCheckedChanged="rbtForward_CheckedChanged" TabIndex="1" ToolTip="Forward file for further approval process" />s&nbsp;&nbsp;&nbsp;
                                                    <asp:RadioButton ID="rbtComplete" runat="server" Checked="false" Text="Complete" AutoPostBack="true"
                                                        OnCheckedChanged="rbtComplete_CheckedChanged" TabIndex="1" ToolTip="Complete will close the file & stop approval process" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Section User Name </label>
                                                </div>
                                                <asp:TextBox ID="txtSectionUserName" runat="server" CssClass="form-control" TabIndex="1"
                                                    placeholder="Enter Character to Search" ToolTip="Enter Section User Name" AutoPostBack="true" OnTextChanged="txtSectionUserName_TextChanged" onblur="CheckValidUser(this)"></asp:TextBox>
                                                <asp:HiddenField ID="hfSectionId" runat="server" />
                                                 <asp:HiddenField ID="HiddenGetSuName" value="0" runat="server" />

                                                <ajaxToolKit:AutoCompleteExtender ID="autAgainstAc" runat="server" TargetControlID="txtSectionUserName"
                                                    MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="1000"
                                                    ServiceMethod="GetSectionName" OnClientShowing="clientShowing" OnClientItemSelected="GetSUName">
                                                </ajaxToolKit:AutoCompleteExtender>

                                                <asp:RequiredFieldValidator ID="rfvSUName" runat="server" ControlToValidate="txtSectionUserName"
                                                    Display="None" ErrorMessage="Enter Section User Name." SetFocusOnError="True"
                                                    ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12" id="divSendToUserRole" runat="server" visible="false">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Send To User Role </label>
                                                </div>

                                                <asp:DropDownList ID="ddlReceiverRole" CssClass="form-control" runat="server" TabIndex="1"
                                                    AppendDataBoundItems="true" ToolTip="Select Send To User Role">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvRRole" runat="server" ErrorMessage="Please Select Send To User Role."
                                                    ControlToValidate="ddlReceiverRole" InitialValue="0" Display="None" ValidationGroup="Submit" />
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Note </label>
                                                </div>
                                                <asp:TextBox ID="txtNote" runat="server" TabIndex="1" CssClass="form-control" ToolTip="Enter Note" TextMode="MultiLine" />
                                                <asp:RequiredFieldValidator ID="rfvNote" runat="server" ControlToValidate="txtNote"
                                                    Display="None" ErrorMessage="Please Enter Note." ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </asp:Panel>
                        </div>
                        <div class="col-12">
                            <asp:Panel ID="pnlUploadFiles" runat="server" Visible="false">
                                <div class="sub-heading">
                                    <h5>Upload Files</h5>
                                </div>
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>Note: Your File Name Should Not Contain Any "_,-"</sup>
                                            <label>Upload Files </label>
                                        </div>
                                        <asp:FileUpload ID="FileUpload1" runat="server" class="multi" TabIndex="1" ToolTip="Click here to Upload Document" />
                                    </div>
                                    <div class="form-group col-lg-2 col-md-6 col-12">
                                        <asp:Button ID="btnUploadFile" runat="server" Text="Add Documents" OnClick="btnUploadFile_Click" CssClass="btn btn-primary"
                                            ToolTip="Click here to upload new documents" TabIndex="1" />
                                    </div>
                                    <div class="col-12">
                                        <asp:Label ID="lblFilesDestinationPath" runat="server" Text="" Visible="false"></asp:Label>
                                        <asp:Label ID="lblFName" runat="server" Text="" Visible="false"></asp:Label>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                        <div id="divButtons" runat="server" class="col-12 btn-footer">
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" TabIndex="1" ValidationGroup="Submit" OnClick="btnSubmit_Click"
                                CssClass="btn btn-primary" ToolTip="Click here to Submit" CausesValidation="true" Visible="false" />
                            <asp:Button ID="btnReport" runat="server" Text="Report" TabIndex="1" OnClick="btnReport_Click"
                                CssClass="btn btn-info" CausesValidation="false" ToolTip="Click here to Show Report" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="1" OnClick="btnCancel_Click"
                                CssClass="btn btn-warning" CausesValidation="false" ToolTip="Click here to Reset" Visible="false" />
                            <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Submit" />
                            <asp:ValidationSummary ID="VS2" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Add" />
                        </div>
                        <div class="col-12 mt-3">
                            <div class="row">
                                <asp:Panel ID="pnlRadioButton" runat="server">
                                    <div class="form-group col-lg-12 col-md-12 col-12">
                                        <asp:RadioButtonList ID="rdbList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rdbList_SelectedIndexChanged" RepeatDirection="Horizontal">
                                            <asp:ListItem Selected="True" Value="0">Incomplete Files&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                            <asp:ListItem Value="1">Complete Files</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                        <div class="col-12">
                            <asp:Panel ID="pnlList" runat="server">
                                <asp:ListView ID="lvFile" runat="server">
                                    <LayoutTemplate>
                                        <div id="lgv1">
                                            <div class="sub-heading">
                                                <h5>List Of Moving File</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                <thead>
                                                    <tr class="bg-light-blue">
                                                        <th>SELECT
                                                        </th>
                                                        <th>FILE NO.
                                                        </th>
                                                        <th>FILE NAME
                                                        </th>
                                                        <th>FILE PATH
                                                        </th>
                                                        <th>CREATION DATE
                                                        </th>
                                                        <th>DOCUMENT TYPE
                                                        </th>
                                                        <th>STATUS
                                                        </th>
                                                        <th>MOVEMENT STATUS
                                                        </th>
                                                        <th>RECEIVE FILE
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
                                                <asp:RadioButton ID="rdoFile" runat="server" AutoPostBack="true" ToolTip='<%# Eval("FILE_ID") %>'
                                                    OnClick="javascript:SelectSingleRadiobutton(this.id)" OnCheckedChanged="rdoFile_CheckedChanged" />
                                            </td>
                                            <td>
                                                <%# Eval("FILE_CODE")%>
                                            </td>
                                            <td>
                                                <%# Eval("FILE_NAME")%>
                                                <asp:HiddenField ID="hdnFileId" runat="server" Value='<%# Eval("FILE_ID") %>' />
                                            </td>
                                            <td>
                                                <%# Eval("FILEPATH") %>
                                                <asp:Label ID="lblFilePath" runat="server" Text='<%# Eval("FILEPATH") %> ' Visible="false"></asp:Label>
                                                <asp:HiddenField ID="hdnSectionIDS" runat="server" Value='<%# Eval("SECTIONIDS") %>' />

                                            </td>

                                            <td>
                                                <%# Eval("CREATION_DATE", "{0:dd-MMM-yyyy}")%>
                                            </td>

                                            <td>
                                                <%# Eval("DOCUMENT_TYPE")%>
                                            </td>
                                            <td>
                                                <%# Eval("STATUS").ToString() == "0" ? "ACTIVE": "INACTIVE" %>
                                            </td>
                                            <td>
                                                <%# Eval("MOVEMENT_STATUS")%>
                                            </td>
                                            <td>
                                                <asp:Button ID="btnReceive" runat="server" Text="View" CommandName='<%# Eval("FILE_ID") %>' CommandArgument='<%# Eval("FILE_MOVID") %>'
                                                    Visible='<%#Eval("MOVEMENT_STATUS").ToString() == "MOVE"  ? true : false %>' OnClick="btnReceive_Click" CssClass="btn btn-primary"
                                                    ToolTip='<%# Eval("LINK_STATUS") %>' />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </asp:Panel>
                            <div class="col-12">
                                <asp:Panel ID="pnlInActive" runat="server" Visible="false">
                                    <asp:ListView ID="lvInactiveFile" runat="server">
                                        <LayoutTemplate>
                                            <div id="lgv1">
                                                <div class="sub-heading">
                                                    <h5>List Of Completed Files</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                    <thead>
                                                        <tr class="bg-light-blue">
                                                            <th>SELECT
                                                            </th>
                                                            <th>FILE NO.
                                                            </th>
                                                            <th>FILE NAME
                                                            </th>
                                                            <th>FILE PATH
                                                            </th>
                                                            <th>CREATION DATE
                                                            </th>
                                                            <th>DOCUMENT TYPE
                                                            </th>
                                                            <th>STATUS
                                                            </th>
                                                            <th>MOVEMENT STATUS
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
                                                    <asp:RadioButton ID="rdoFileInactive" runat="server" AutoPostBack="true" ToolTip='<%# Eval("FILE_ID") %>'
                                                        OnClick="javascript:SelectSingleRadiobutton(this.id)" OnCheckedChanged="rdoFileInactive_CheckedChanged" />
                                                </td>
                                                <td>
                                                    <%# Eval("FILE_CODE")%>
                                                </td>
                                                <td>
                                                    <%# Eval("FILE_NAME")%>
                                                    <asp:HiddenField ID="hdnFileId" runat="server" Value='<%# Eval("FILE_ID") %>' />
                                                </td>
                                                <td>
                                                    <%# Eval("FILEPATH") %>
                                                    <asp:Label ID="lblFilePath" runat="server" Text='<%# Eval("FILEPATH") %> ' Visible="false"></asp:Label>
                                                    <asp:HiddenField ID="hdnSectionIDS" runat="server" Value='<%# Eval("SECTIONIDS") %>' />

                                                </td>

                                                <td>
                                                    <%# Eval("CREATION_DATE", "{0:dd-MMM-yyyy}")%>
                                                </td>

                                                <td>
                                                    <%# Eval("DOCUMENT_TYPE")%>
                                                </td>
                                                <td>
                                                    <%# Eval("STATUS").ToString() == "0" ? "ACTIVE": "INACTIVE" %>
                                                </td>
                                                <td>
                                                    <%# Eval("MOVEMENT_STATUS")%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>
                            <div class="col-12 mt-3">
                                <asp:Panel ID="pnlMovemnt" runat="server">
                                    <asp:ListView ID="lvFileMovement" runat="server">
                                        <LayoutTemplate>
                                            <div id="lgv1">
                                                <div class="sub-heading">
                                                    <h5>Movement Tracking Of Files </h5>
                                                </div>

                                              <%--  <table class="table table-striped table-bordered" style="width: 100%">--%>
                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                    <thead>
                                                        <tr class="bg-light-blue">
                                                            <th>FILE NO.
                                                            </th>
                                                            <th>FILE NAME
                                                            </th>
                                                            <th>MOVEMENT DATE
                                                            </th>
                                                            <th>RECEIVER DETAILS
                                                            </th>
                                                            <th>MOVEMENT STATUS
                                                            </th>
                                                            <th>RECEIVE NOTE
                                                            </th>
                                                            <th>FORWARD/ RETURN NOTE
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
                                                    <%# Eval("FILE_CODE")%>
                                                </td>
                                                <td>
                                                    <%# Eval("FILE_NAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("MOVEMENT_DATE", "{0:dd-MMM-yyyy}")%>
                                                </td>
                                                <td>
                                                    <%# Eval("FNAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("FILE_STATUS") %>
                                                </td>
                                                <td>
                                                    <%# Eval("REMARK_RECEIVE") %>
                                                </td>
                                                <td>
                                                    <%# Eval("REMARK_FOR_RET") %>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>
                            <div class="col-12">
                                <asp:Panel ID="pnlInActiveMovement" runat="server" Visible="false">
                                    <asp:ListView ID="lvMovementInActive" runat="server">
                                        <LayoutTemplate>
                                            <div id="lgv1">
                                                <div class="sub-heading">
                                                    <h5>Movement Tracking Of Completed Files</h5>
                                                </div>

                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                    <thead>
                                                        <tr class="bg-light-blue">
                                                            <th>FILE NO.
                                                            </th>
                                                            <th>FILE NAME
                                                            </th>
                                                            <th>MOVEMENT DATE
                                                            </th>
                                                            <th>RECEIVER DETAILS
                                                            </th>
                                                            <th>MOVEMENT STATUS
                                                            </th>
                                                            <th>RECEIVE NOTE
                                                            </th>
                                                            <th>FORWARD/ RETURN NOTE
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
                                                    <%# Eval("FILE_CODE")%>
                                                </td>
                                                <td>
                                                    <%# Eval("FILE_NAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("MOVEMENT_DATE", "{0:dd-MMM-yyyy}")%>
                                                </td>
                                                <td>
                                                    <%# Eval("FNAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("FILE_STATUS") %>
                                                </td>
                                                <td>
                                                    <%# Eval("REMARK_RECEIVE") %>
                                                </td>
                                                <td>
                                                    <%# Eval("REMARK_FOR_RET") %>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>
                            <div class="col-12">
                                <asp:Panel ID="pnlNewFiles" runat="server" Visible="false" ScrollBars="Auto">
                                    <asp:ListView ID="lvNewFiles" runat="server">
                                        <LayoutTemplate>
                                            <div id="lgv1">
                                                <div class="sub-heading"><h5>List Of Documents </h5>
                                           <%--     <table class="table table-bordered table-hover">--%>
                                                  <table class=" table table-striped table-bordered">   
                                                   
                                                    <thead>
                                                        <tr class="bg-light-blue">
                                                            <%-- <th>Delete
                                                         </th>--%>
                                                            <th>File Name
                                                            </th>
                                                            <th>Download
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
                                                <%-- <td>
                                            <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.gif" CommandArgument='<%# Eval("FILENAME")%> '
                                                AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click" CommandName='<%# Eval("IDNO")%>'
                                                OnClientClick="javascript:return confirm('Are you sure you want to delete this file?')" />
                                        </td>--%>
                                                <td>
                                                    <%#GetFileName(DataBinder.Eval(Container, "DataItem")) %>
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="imgFile" runat="Server" ImageUrl="~/images/action_down.png"
                                                        AlternateText='<%#DataBinder.Eval(Container, "DataItem") %>' ToolTip='<%#DataBinder.Eval(Container, "DataItem")%>' OnClick="imgdownloadNew_Click" />
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>
                            <div class="col-12">
                                <asp:Panel ID="pnlFinalList" runat="server" Visible="false">
                                    <asp:ListView ID="lvFinalList" runat="server">
                                        <LayoutTemplate>
                                            <%--<div id="lgv1">
                                                <div class="sub-heading">
                                                    <h5>List of documents  </h5>
                                                </div>--%>
                                            <div id="lgv1">
                                                <div class="sub-heading"><h5>List Of Documents </h5>

                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                    <thead>
                                                        <tr class="bg-light-blue">
                                                            <%-- <th>Delete
                                                    </th>--%>
                                                            <th>File Name
                                                            </th>
                                                            <th>Download
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
                                                <%-- <td>
                                            <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.gif" CommandArgument='<%# Eval("FILENAME")%> '
                                                AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click" CommandName='<%# Eval("IDNO")%>'
                                                OnClientClick="javascript:return confirm('Are you sure you want to delete this file?')" />
                                        </td>--%>
                                                <td>
                                                    <asp:HiddenField ID="hdnFinalIDNO" runat="server" Value='<%# Eval("IDNO")%>' />
                                                    <asp:HiddenField ID="hdnFinalUPLNO" runat="server" Value='<%# Eval("UPLNO")%>' />
                                                    <asp:Label ID="lblFinalFileName" runat="server" Text='<%# Eval("FILENAME")%> '></asp:Label>

                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="imgFile" runat="Server" ImageUrl="~/images/action_down.gif" CommandArgument='<%# Eval("FILEPATH")%>'
                                                        AlternateText='<%# Eval("FILENAME")%>' OnClick="imgdownload_Click" CommandName='<%# Eval("UA_NO")%>' />
                                                    <asp:HiddenField ID="hdnSourcePath" runat="server" Value='<%# Eval("SOURCEPATH")%>' />
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
        </div>
    </div>
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
    <script language="javascript" type="text/javascript">
        function SelectSingleRadiobutton(rdbtnid) {
            var rdBtn = document.getElementById(rdbtnid);
            var rdBtnList = document.getElementsByTagName("input");
            for (i = 0; i < rdBtnList.length; i++) {
                if (rdBtnList[i].type == "radio" && rdBtnList[i].id != rdBtn.id) {
                    rdBtnList[i].checked = false;
                }
            }
        }
    </script>
    <script type="text/javascript">
        function GetSUName(source, eventArgs) {
            var idno = eventArgs.get_value().split("*");
            var Name = idno[0].split("-");
            document.getElementById('ctl00_ContentPlaceHolder1_txtSectionUserName').value = idno[1];
            document.getElementById('ctl00_ContentPlaceHolder1_hfSectionId').value = Name[0];

            document.getElementById('ctl00_ContentPlaceHolder1_HiddenGetSuName').value = 1;
        }

    </script>
    <script type="text/javascript">
        function CheckValidUser(ctrl) {
            var name = ctrl.value;
            var len = name.split('*').length;
            var GetSuName = document.getElementById('ctl00_ContentPlaceHolder1_HiddenGetSuName').value  //GetSuName=1 means user selected from list and GetSuName=0 means user not select from list
            var txtusername = document.getElementById('ctl00_ContentPlaceHolder1_txtSectionUserName').value;

            if (len == 1) {
                // alert(len);
                if (GetSuName == 0 && txtusername != "") {
                    // alert(GetSuName);
                    alert("Please Select Valid User from List.");
                    ctrl.value = "";
                    // ctrl.focus();
                }
            }
        }
        </script>

</asp:Content>


