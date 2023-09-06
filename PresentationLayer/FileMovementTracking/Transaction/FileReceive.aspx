<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="FileReceive.aspx.cs" Inherits="FileMovementTracking_Transaction_FileReceive" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<script src="../../JAVASCRIPTS/JScriptAdmin_Module.js" type="text/javascript"></script>--%>
    <script type="text/javascript">
        debugger
        function clientShowing(source, args) {
            source._popupBehavior._element.style.zIndex = 100000;
        }
    </script>
     <style>
        .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>
    <%--  <asp:UpdatePanel ID="updActivity" runat="server">
        <ContentTemplate>--%>
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">RECEIVE FILE</h3>
                </div>

                <div class="box-body">
                    <asp:Panel ID="pnlDetails" runat="server" Visible="false">
                        <div class="col-12">
                            <div class="row">
                                <div class="col-lg-4 col-md-6 col-12">
                                    <ul class="list-group list-group-unbordered">
                                        <li class="list-group-item"><b>File No. :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblFCode" runat="server" Text="" Font-Bold="true"></asp:Label>
                                            </a>
                                        </li>
                                        <li class="list-group-item"><b>File Movement Note :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblMovRemark" runat="server" Text="" Font-Bold="true"></asp:Label>
                                            </a>
                                        </li>
                                    </ul>
                                </div>

                                <div class="col-lg-4 col-md-6 col-12">
                                    <ul class="list-group list-group-unbordered">
                                        <li class="list-group-item"><b>File Name :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblFName" runat="server" Font-Bold="true"></asp:Label>
                                                <asp:HiddenField ID="hdnLinkStatus" runat="server" Value="" />
                                            </a>
                                        </li>
                                        <li class="list-group-item"><b>File Movement Path :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblFilePath" runat="server" Font-Bold="true"></asp:Label>
                                                <asp:HiddenField ID="hdnSectionIDS" runat="server" />
                                            </a>
                                        </li>
                                    </ul>
                                </div>

                                <div class="col-lg-4 col-md-6 col-12">
                                    <ul class="list-group list-group-unbordered">
                                        <li class="list-group-item"><b>File Creation Date :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblDate" runat="server" Text="" Font-Bold="true"></asp:Label>
                                            </a>
                                        </li>

                                       <%-- <li class="list-group-item"><b>File Creation Date : </b>
                                            <a class="sub-label">
                                                <asp:Label ID="Label1" runat="server" Text="" Font-Bold="true"></asp:Label>
                                            </a>
                                        </li>--%>

                                    </ul>
                                </div>
                            </div>
                        </div>
                        <div class="col-12 mt-2">
                            <div class="row">
                                <div class="form-group col-lg-3 col-md-6 col-12" id="divFDesc" runat="server" visible="false">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label>File Description </label>
                                    </div>

                                    <asp:TextBox ID="txtDesc" runat="server" MaxLength="200" TabIndex="1" CssClass="form-control"
                                        TextMode="MultiLine" ToolTip="Enter File Description" />
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Note </label>
                                    </div>
                                    <asp:TextBox ID="txtRemark" runat="server" MaxLength="200" TabIndex="1" CssClass="form-control" TextMode="MultiLine"
                                        ToolTip="Enter Note" /> 

                                    <asp:RequiredFieldValidator ID="rfvRemark" runat="server" ControlToValidate="txtRemark"
                                        Display="None" ErrorMessage="Please Enter Note." ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label></label>
                                    </div>

                                    <asp:RadioButton ID="rdbApproved" runat="server" Text="Approved" OnCheckedChanged="rdbApproved_CheckedChanged"
                                        ToolTip="Approved for further movement or close the file" AutoPostBack="true"  TabIndex="1"/>

                                  

                                    &nbsp;
                                                     <asp:RadioButton ID="rdbNotApproved" runat="server" Text="Not Approved" AutoPostBack="true"
                                                         OnCheckedChanged="rdbNotApproved_CheckedChanged" ToolTip="Not Approved to Return the file " />
                                </div>  


                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label></label>
                                    </div>
                                    <asp:RadioButton ID="rbtReceive" runat="server" Checked="true" Text="Receive" TabIndex="1" Visible="false" />
                                    <asp:RadioButton ID="rbtForward" runat="server" Checked="false" Text="Forward" AutoPostBack="true"
                                        OnCheckedChanged="rbtForward_CheckedChanged" TabIndex="1" ToolTip="Forward file for further approval process" />
                                    &nbsp;
                                                    <asp:RadioButton ID="rbtReturn" runat="server" Checked="false" Text="Return" AutoPostBack="true"
                                                        OnCheckedChanged="rbtReturn_CheckedChanged" TabIndex="1" ToolTip="Return to members only present in movement path" />

                                    &nbsp;
                                                    <asp:RadioButton ID="rbtComplete" runat="server" Checked="false" Text="Complete" AutoPostBack="true"
                                                        OnCheckedChanged="rbtComplete_CheckedChanged" TabIndex="1" ToolTip="Complete will forward file to owner for further action." />
                                </div>
                            </div>
                        </div>
                        <div class="col-12" id="trDept" runat="server" visible="false">
                            <asp:UpdatePanel ID="updActivity" runat="server">
                                <ContentTemplate>
                                    <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label><span style="color: #FF0000">*</span>Section User Name </label>
                                        </div>
                                        <asp:TextBox ID="txtSectionUserName" runat="server" CssClass="form-control" TabIndex="1" AutoPostBack="true" OnTextChanged="txtSectionUserName_TextChanged" onblur="CheckValidUser(this)"
                                            placeholder="Enter Character to Search" ToolTip="Enter Section User Name"></asp:TextBox>
                                        <asp:HiddenField ID="hfSectionId" runat="server" />
                                         <asp:HiddenField ID="HiddenGetSuName" value="0" runat="server" />

                                        <asp:RequiredFieldValidator ID="rfvSUName" runat="server" ControlToValidate="txtSectionUserName"
                                            Display="None" ErrorMessage="Please Enter Section User Name." SetFocusOnError="True"
                                            ValidationGroup="Submit"></asp:RequiredFieldValidator>

                                        <ajaxToolKit:AutoCompleteExtender ID="autAgainstAc" runat="server" TargetControlID="txtSectionUserName"
                                            MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="1000"
                                            ServiceMethod="GetSectionName" OnClientShowing="clientShowing" OnClientItemSelected="GetSUName">
                                        </ajaxToolKit:AutoCompleteExtender>
                                    </div>
                                       
                                    <div id="divSendToUserRole" runat="server" visible="false" class="form-group col-lg-3 col-md-6 col-12">
                                        <label><span style="color: #FF0000">*</span>Send To User Role :</label>
                                        <asp:DropDownList ID="ddlReceiverRole" CssClass="form-control" runat="server" TabIndex="1"
                                            AppendDataBoundItems="true" ToolTip="Select Send To User Role">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvRRole" runat="server" ErrorMessage="Please Select Send To User Role."
                                            ControlToValidate="ddlReceiverRole" InitialValue="0" Display="None" ValidationGroup="Submit" />
                                    </div> </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="pnlUploadFiles" runat="server" Visible="false">
                        <div class="col-12">
                             <div class="sub-heading">
                                <h5>Upload Files</h5>
                            </div>
                        
                            <div class="row">
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>Note: Your File Name Should Not Contain Any "_,-"</sup>
                                                <label>Upload Files </label>
                                            </div>
                                    <asp:FileUpload ID="FileUpload1" runat="server" class="multi" TabIndex="11" ToolTip="Click here to Upload Document" />       
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label></label>
                                            </div>
                                    <asp:Button ID="btnAdd" runat="server" Text="Add Documents" OnClick="btnAdd_Click" CssClass="btn btn-primary"
                                        ToolTip="Click here to upload new documents" TabIndex="12" />
                                </div>
                                <div class="col-12">
                                    <asp:Label ID="lblFilesDestinationPath" runat="server" Text="" Visible="false"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                    <div id="divButtons" runat="server" visible="false" class="col-12 btn-footer">
                        <asp:Button ID="btnReceive" runat="server" Text="Submit" TabIndex="1" ValidationGroup="Submit" OnClick="btnReceive_Click"
                            CssClass="btn btn-primary" ToolTip="Click here to Submit" CausesValidation="true" OnClientClick="return DisableIt()"/>
                        <asp:Button ID="btnReport" runat="server" Text="Report" Visible="false" TabIndex="1" OnClick="btnReport_Click" CssClass="btn btn-info"
                            CausesValidation="false" ToolTip="Click here to Show Report" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="1" OnClick="btnCancel_Click" CssClass="btn btn-warning"
                            CausesValidation="false" ToolTip="Click here to Reset" />
                        <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Submit" />
                    </div>
                    <div class="col-12">
                        <div class="row">
                        <div class="form-group col-lg-3 col-md-6 col-12">
                            <div class="label-dynamic">
                                <sup></sup>
                                <label></label>
                            </div>
                            <asp:Panel ID="pnlRadioButton" runat="server">
                                <asp:RadioButtonList ID="rdbList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rdbList_SelectedIndexChanged" RepeatDirection="Horizontal">
                                    <asp:ListItem Selected="True" Value="0">Incomplete Files&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                    <asp:ListItem Value="1">Complete Files</asp:ListItem>
                                </asp:RadioButtonList>
                            </asp:Panel>
                        </div>
                            </div>
                    </div>
                    <div class="col-12">
                        <asp:Panel ID="pnlDropDownUserRole" runat="server">
                            <div class="row">
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup></sup>
                                    <label>Select Role </label>
                                </div>
                                <asp:DropDownList ID="ddlUserRole" CssClass="from-control" runat="server" AppendDataBoundItems="true" ToolTip="Select Role" data-select2-enable="true"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlUserRole_SelectedIndexChanged">
                                    <asp:ListItem Value="0">All</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                                </div>
                        </asp:Panel>
                    </div>
                    <div class="col-12">
                        <asp:Panel ID="pnlList" runat="server" Visible="false">
                            <asp:ListView ID="lvFileMovement" runat="server">
                                <LayoutTemplate>
                                    <div id="lgv1"  class="table-responsive">

                                        <div class="sub-heading">
                                            <h5>List Of Files </h5>
                                        </div>
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                            <thead>
                                                <tr class="bg-light-blue">
                                                    <th>SELECT
                                                    </th>
                                                    <th>FILE NO.-
                                                      FILE NAME  </th>
                                                    <%--<th>FILE NAME
                                                    </th>--%>
                                                    <th>CREATOR NAME
                                                    </th>
                                                    <%--  <th>DEPTT.
                                                    </th>--%>
                                                    <th>MOVEMENT DATE
                                                    </th>
                                                    <th>RECEIVED DATE 
                                                    </th>
                                                    <th>SEND BY
                                                    </th>
                                                    <th>SEND TO
                                                    </th>
                                                    <th>STATUS
                                                    </th>
                                                    <%--<th>RECEIVE NOTE
                                                    </th>
                                                    <th>NOTE (FORWARD/RETURN)
                                                    </th>--%>
                                                    <th>DETAILS
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
                                            <%-- <%# Eval("FILE_CODE")%>--%>
                                            <%# Eval("F_NAME")%>
                                        </td>
                                        <%--<td>
                                            <%# Eval("FILE_NAME")%>
                                        </td> --%>
                                        <td>
                                            <%# Eval("UAFULLNAME")%>
                                        </td>

                                        <%--<td>
                                            <%# Eval("SUBDEPT")%>
                                        </td>--%>

                                        <td>
                                            <%# Eval("MOVEMENT_DATE", "{0:dd-MM-yyyy}")%>
                                        </td>
                                        <td>
                                            <%# Eval("RECEIVED_DATE", "{0:dd-MM-yyyy}")%>
                                        </td>
                                        <td>
                                            <%# Eval("SENDBYS")%>  
                                        </td>
                                        <td>
                                            <%# Eval("RECEIVED_AS")%>  
                                        </td>
                                        <td>
                                            <%# Eval("FILE_STATUS")%>                                                           
                                        </td>
                                        <%-- <td>
                                            <%# Eval("REMARK")%>                                                           
                                        </td>
                                        <td>
                                            <%# Eval("REMARK_FOR_RET")%>   
                                        </td>--%>

                                        <td>
                                            <asp:Button ID="btnDetail" runat="server" ToolTip='<%# Eval("FILE_MOVID") %>' CommandName='<%# Eval("FILE_ID") %>'
                                                CommandArgument='<%# Eval("SECTION_ID") %>' Enabled='<%#Eval("STAT").ToString() == "F"|| Eval("STAT").ToString() == "R" || Eval("STAT").ToString() == "C" ? false : true %>'
                                                Text='<%#Eval("STAT").ToString() == "P" ? "Receive" : "Details"  %>'
                                                OnClick="btnDetail_Click" TabIndex="10" CssClass="btn btn-primary" />
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </asp:Panel>
                    </div>
                    <div class="col-12">
                        <asp:Panel ID="pnlInActive" runat="server" Visible="false">
                            <asp:ListView ID="lvInactiveFile" runat="server">
                                <LayoutTemplate>
                                    <div id="lgv1">
                                        <div class="sub-heading">
                                            <h5>List Of Files </h5>
                                        </div>
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                            <thead>
                                                <tr class="bg-light-blue">
                                                    <th>SELECT
                                                    </th>
                                                    <th>FILE NO. / 
                                                        FILE NAME
                                                    </th>
                                                    <th>CREATOR NAME
                                                    </th>
                                                    <%--<th>DEPTT.
                                                    </th>--%>
                                                    <th>MOVEMENT DATE
                                                    </th>
                                                    <th>RECEIVED DATE 
                                                    </th>
                                                    <th>SEND BY
                                                    </th>
                                                    <th>STATUS
                                                    </th>
                                                    <th>DETAILS
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
                                            <%# Eval("F_NAME")%>
                                        </td>
                                        <td>
                                            <%# Eval("UAFULLNAME")%>
                                        </td>

                                        <%--<td>
                                            <%# Eval("SUBDEPT")%>
                                        </td>--%>

                                        <td>
                                            <%# Eval("MOVEMENT_DATE", "{0:dd-MM-yyyy}")%>
                                        </td>
                                        <td>
                                            <%# Eval("RECEIVED_DATE", "{0:dd-MM-yyyy}")%>
                                        </td>
                                        <td>
                                            <%# Eval("SENDBYS")%>  
                                        </td>
                                        <td>
                                            <%# Eval("FILE_STATUS")%>                                                           
                                        </td>


                                        <td>
                                            <asp:Button ID="btnDetail" runat="server" ToolTip='<%# Eval("FILE_MOVID") %>' CommandName='<%# Eval("FILE_ID") %>'
                                                CommandArgument='<%# Eval("SECTION_ID") %>' Enabled='<%#Eval("STAT").ToString() == "F"|| Eval("STAT").ToString() == "R" || Eval("STAT").ToString() == "C" ? false : true %>'
                                                Text='<%#Eval("STAT").ToString() == "P" ? "Receive" : "Details"  %>'
                                                OnClick="btnDetail_Click" TabIndex="10"
                                                CssClass="btn btn-info" />
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </asp:Panel>
                    </div>
                    <div class="col-12">
                        <asp:Panel ID="pnlNewFiles" runat="server" Visible="false">
                            <asp:ListView ID="lvNewFiles" runat="server" OnItemDataBound="lvNewFiles_ItemDataBound">
                                <LayoutTemplate>
                                    <div class="sub-heading">
                                        <h5>List of documents </h5>
                                    </div>
                                    <div id="lgv1">

                                   <%--     <table class="table table-striped table-bordered nowrap display" style="width: 100%">--%>
                                        <table class="table table-striped table-bordered" style="width: 100%">
                                            <thead>
                                                <tr class="bg-light-blue">
                                                    <th>Delete
                                                    </th>
                                                    <th>File Name
                                                    </th>
                                                     <th runat  ="server" visible="false">User Name   <%----24/01/2022--%>
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
                                        <td>
                                            <%--<asp:ImageButton ID="btnDeleteNew" runat="server"  ImageUrl="~/Images/delete.png" CommandArgument='<%#DataBinder.Eval(Container, "DataItem") %>'
                                                AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDeleteNew_Click"
                                                OnClientClick="javascript:return confirm('Are you sure you want to delete this file?')" />--%>
                                            <asp:ImageButton ID="btnDeleteNew" runat="server" ImageUrl="~/images/delete.png" CommandArgument='<%#DataBinder.Eval(Container, "DataItem") %>'
                                                AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDeleteNew_Click" 
                                                OnClientClick="javascript:return confirm('Are you sure you want to delete this file?')" />
                                             <asp:HiddenField ID="hdnUserNo" runat="server" Value=' <%#GetUserNo(DataBinder.Eval(Container, "DataItem")) %>' />  <%--24/01/2022--%>
                                        </td>
                                       
                                        <td>
                                            <%#GetFileName(DataBinder.Eval(Container, "DataItem")) %>
                                        </td>
                                        <td runat="server" visible="false">
                                                <%#GetUserName(DataBinder.Eval(Container, "DataItem")) %>    <%--24/01/2022--%>
                                            </td>

                                        <td>
                                            <asp:ImageButton ID="imgFile" runat="Server"   ImageUrl="~/Images/action_down.png"
                                                AlternateText='<%#DataBinder.Eval(Container, "DataItem") %>' ToolTip='<%#DataBinder.Eval(Container, "DataItem")%>' OnClick="imgdownloadNew_Click" />
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </asp:Panel>
                    </div>
                    <div class="col-12">
                        <asp:ListView ID="lvFinalList" runat="server" Visible="false">
                            <LayoutTemplate>
                                <div id="lgv1">
                                    <div class="sub-heading">
                                        <h5>List of documents</h5>
                                    </div>

                                    <%--<table class="table table-striped table-bordered nowrap display" style="width: 100%">--%>
                                    <table class="table table-striped table-bordered" style="width: 100%">
                                        <thead>
                                            <tr class="bg-light-blue">
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
                                    <td>
                                        <asp:HiddenField ID="hdnFinalIDNO" runat="server" Value='<%# Eval("IDNO")%>' />
                                        <asp:HiddenField ID="hdnFinalUPLNO" runat="server" Value='<%# Eval("UPLNO")%>' />
                                        <asp:Label ID="lblFinalFileName" runat="server" Text='<%# Eval("FILENAME")%> '></asp:Label>

                                    </td>
                                    <td>
                                        <asp:ImageButton ID="imgFile" runat="Server" ImageUrl="~/images/action_down.gif" CommandArgument='<%# Eval("FILEPATH")%>'
                                            AlternateText='<%# Eval("FILENAME")%>' OnClick="imgdownload_Click" />
                                        <asp:HiddenField ID="hdnSourcePath" runat="server" Value='<%# Eval("SOURCEPATH")%>' />
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:ListView>
                    </div>
                </div>

                <%--      <div class="col-md-12">
                                <asp:ListView ID="lvDocumentList" runat="server" >
                                    <LayoutTemplate>
                                        <div id="lgv1">
                                            <h4 class="box-title">List of documents yyyy
                                            </h4>
                                            <table class="table table-bordered table-hover">
                                                <thead>
                                                    <tr class="bg-light-blue">
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
                                            <td>
                                                <asp:HiddenField ID="hdnFinalIDNO" runat="server" Value='<%# Eval("IDNO")%>' />
                                                <asp:HiddenField ID="hdnFinalUPLNO" runat="server" Value='<%# Eval("UPLNO")%>' />
                                                <asp:Label ID="lblFinalFileName" runat="server" Text='<%# Eval("FILENAME")%> '></asp:Label>

                                            </td>
                                            <td>
                                                <asp:ImageButton ID="imgFile" runat="Server" ImageUrl="~/images/action_down.gif" CommandArgument='<%# Eval("FILEPATH")%>'
                                                    AlternateText='<%# Eval("FILENAME")%>' OnClick="imgdownload_Click" />
                                                <asp:HiddenField ID="hdnSourcePath" runat="server" Value='<%# Eval("SOURCEPATH")%>' />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>



                           
                           <%--    <asp:Panel ID="pnlFile" runat="server" Visible="false" ScrollBars="Auto">
                                        <asp:ListView ID="lvfileUpload" runat="server">
                                            <LayoutTemplate>
                                                <div id="lgv1">
                                                    <h4 class="box-title">Download files
                                                    </h4>
                                                    <table class="table table-bordered table-hover">
                                                        <thead>
                                                            <tr class="bg-light-blue">                                                           
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
                                                    <td>                                                      
                                                        <%#GetFileName(DataBinder.Eval(Container, "DataItem")) %>
                                                    </td>
                                                    <td>                                                     

                                                         <asp:ImageButton ID="imgFile" runat="Server" ImageUrl="~/images/action_down.gif"
                                                            AlternateText='<%#DataBinder.Eval(Container, "DataItem") %>' ToolTip='<%#DataBinder.Eval(Container, "DataItem")%>' 
                                                             OnClick="imgdownload_Click" />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </asp:Panel>                                
                     
                --%>
            </div>
        </div>
    </div>

    <%--    </ContentTemplate>     
    </asp:UpdatePanel>--%>



    <script language="javascript" type="text/javascript">

        function DisableIt() {
            document.getElementById("ctl00_ContentPlaceHolder1_btnReceive").onclick = function () {
                //disable
                this.disabled = true;
            }
        }

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
            var GetSuName = document.getElementById('ctl00_ContentPlaceHolder1_HiddenGetSuName').value
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
