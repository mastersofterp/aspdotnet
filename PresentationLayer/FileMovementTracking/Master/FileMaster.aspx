<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="FileMaster.aspx.cs"
    Inherits="FileMovementTracking_Master_FileMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--  <script src="../../JAVASCRIPTS/JScriptAdmin_Module.js" type="text/javascript"></script>--%>

    <script type="text/javascript">
        debugger
        function clientShowing(source, args) {
            source._popupBehavior._element.style.zIndex = 100000;
        }
    </script>
    
    <style>
        #ctl00_ContentPlaceHolder1_pnlList .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">FILE CREATION</h3>
                </div>

                <div class="box-body">

                    <asp:Panel ID="pnlDetails" runat="server">
                        <div class="col-12">
                            <div class="sub-heading">
                                <h5>File Details</h5>
                            </div>
                            <div class="row">

                                <div class="form-group col-md-12" visible="false" id="divDocSaccanning" runat="server">
                                    <asp:CheckBox ID="chkFile" runat="server" AutoPostBack="true" OnCheckedChanged="chkFile_CheckedChanged" Text="Document And Scanning" />
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>File No  </label>
                                    </div>
                                    <asp:TextBox ID="txtFileCode" runat="server" MaxLength="30" TabIndex="1" CssClass="form-control"
                                        ToolTip="Enter File Number" Enabled="false" />
                                    <asp:RequiredFieldValidator ID="rfvFCode" runat="server" ControlToValidate="txtFileCode"
                                        Display="None" ErrorMessage="Please Enter File Code." ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                    <ajaxToolKit:FilteredTextBoxExtender ID="ftbeFileCode" runat="server"
                                        FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters" TargetControlID="txtFileCode" ValidChars="/\- ">
                                    </ajaxToolKit:FilteredTextBoxExtender>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>File Name </label>
                                    </div>
                                    <asp:TextBox ID="txtFileName" runat="server" MaxLength="60" TabIndex="1" CssClass="form-control" ToolTip="Enter File Name" />
                                    <asp:DropDownList ID="ddlFileCategory" CssClass="form-control" runat="server" TabIndex="2" AppendDataBoundItems="true" AutoPostBack="true" ToolTip="Select Department"
                                        OnSelectedIndexChanged="ddlFileCategory_SelectedIndexChanged" Visible="false">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvFName" runat="server" ControlToValidate="txtFileName"
                                        Display="None" ErrorMessage="Please Enter File Name." ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                    <ajaxToolKit:FilteredTextBoxExtender ID="ftbeFName" runat="server"
                                        FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters" TargetControlID="txtFileName" ValidChars="-_ ">
                                    </ajaxToolKit:FilteredTextBoxExtender>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Owner Name </label>
                                    </div>
                                    <asp:TextBox ID="txtOwnerName" runat="server" MaxLength="60" TabIndex="1" Enabled="false"
                                        CssClass="form-control" ToolTip="Enter Owner Name" />
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12" id="divLbL" runat="server" visible="false">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label>Files will be uploaded in this category/directory </label>
                                    </div>

                                    <asp:Label ID="lblFilePath" runat="server" Text=""></asp:Label>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Department </label>
                                    </div>
                                    <asp:DropDownList ID="ddlDepartment" CssClass="form-control" data-select2-enable="true" TabIndex="1" runat="server" Enabled="false" AppendDataBoundItems="true" ToolTip="Select Department">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvddlDept" runat="server" ErrorMessage="Please Select Department."
                                        ControlToValidate="ddlDepartment" InitialValue="0"
                                        Display="None" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                </div>
                                </div>

                                <div id="divFileDesc" runat="server" visible="true">

                                    <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>File Description</label>
                                        </div>
                                        <asp:TextBox ID="txtDesc" runat="server" MaxLength="200" TabIndex="1" CssClass="form-control"
                                            TextMode="SingleLine" ToolTip="Enter File Description" />
                                        <asp:RequiredFieldValidator ID="rfvDesc" runat="server" ControlToValidate="txtDesc"
                                            Display="None" ErrorMessage="Please Enter File Description." ValidationGroup="Submit">
                                        </asp:RequiredFieldValidator>
                                    </div>

                               
                                        <div class="form-group col-lg-3 col-md-6 col-12"  id="dt" runat="server">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Creation Date </label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i id="Image1" runat="server" class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtCreationDate" runat="server" CssClass="form-control" ToolTip="Enter Creation Date"
                                                    TabIndex="1" Enabled="false"></asp:TextBox>
                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="false"
                                                    EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="Image1" TargetControlID="txtCreationDate">
                                                </ajaxToolKit:CalendarExtender>
                                                <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" AcceptNegative="Left"
                                                    DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true"
                                                    OnInvalidCssClass="errordate" TargetControlID="txtCreationDate" ClearMaskOnLostFocus="true">
                                                </ajaxToolKit:MaskedEditExtender>
                                                <%-- <ajaxToolKit:MaskedEditValidator ID="mevDate" runat="server" ControlExtender="MaskedEditExtender2"
                                                        ControlToValidate="txtCreationDate" ErrorMessage="Please Enter Valid Date In [dd/MM/yyyy] format"
                                                        IsValidEmpty="false" Display="None" Text="*" ValidationGroup="Submit"
                                                        InvalidValueMessage="Please Enter Valid Date In [dd/MM/yyyy] format"></ajaxToolKit:MaskedEditValidator> --%>
                                            </div>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Document Type </label>
                                            </div>
                                            <asp:DropDownList ID="ddlDocType" CssClass="form-control" runat="server" TabIndex="1"
                                                AppendDataBoundItems="true" ToolTip="Select Document Type" data-select2-enable="true">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddlDoc" runat="server" ErrorMessage="Please Select Document Type."
                                                ControlToValidate="ddlDocType" InitialValue="0" Display="None" ValidationGroup="Submit" />
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divStatus" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>File Status </label>
                                            </div>

                                            <asp:RadioButtonList ID="rdoStatus" runat="server" RepeatDirection="Horizontal"
                                                TabIndex="1" ToolTip="Select File Status">
                                                <asp:ListItem Selected="True" Value="0">Active</asp:ListItem>
                                                <asp:ListItem Value="1">InActive</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Keywords </label>
                                            </div>
                                            <asp:TextBox ID="txtKeywords" runat="server" MaxLength="250" TabIndex="1" CssClass="form-control" ToolTip="Enter Keywords" />
                                            <asp:RequiredFieldValidator ID="rfvKeywords" runat="server" ControlToValidate="txtKeywords"
                                                Display="None" ErrorMessage="Please Enter Keywords." ValidationGroup="Submit">
                                            </asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>File Creator's Role</label>
                                            </div>
                                            <asp:DropDownList ID="ddlCreatorRole" CssClass="form-control" runat="server" TabIndex="1"
                                                AppendDataBoundItems="true" ToolTip="Select File Creator's Role" data-select2-enable="true">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvFCRole" runat="server" ErrorMessage="Please Select File Creator's Role."
                                                ControlToValidate="ddlCreatorRole" InitialValue="0" Display="None" ValidationGroup="Submit" />
                                        </div>

                                    </div>

                                    </div>
                               </div>
                          
                    
                    </asp:Panel>
                    <div class="col-12">
                        <div class="row">
                            <div class="col-lg-6 col-md-12 col-12">
                                <asp:Panel ID="pnlReceiverDetails" runat="server">

                                    <div class="sub-heading">
                                        <h5>Receiver Details</h5>
                                    </div>

                                    <asp:UpdatePanel ID="updActivity" runat="server">
                                        <ContentTemplate>
                                            <div class="row">
                                                <div class="form-group col-lg-6 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>Send To User </label>
                                                    </div>
                                                    <asp:TextBox ID="txtSectionUserName" runat="server" CssClass="form-control" TabIndex="1"
                                                        placeholder="Enter Character to Search" ToolTip="Please Enter Send To User " AutoPostBack="true" OnTextChanged="txtSectionUserName_TextChanged" onblur="CheckValidUser(this)"></asp:TextBox>
                                                    <asp:HiddenField ID="hfSectionId" runat="server" />
                                                     <asp:HiddenField ID="HiddenGetSuName" value="0" runat="server" />

                                                    <ajaxToolKit:AutoCompleteExtender ID="autAgainstAc" runat="server" TargetControlID="txtSectionUserName"
                                                        MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="1000"
                                                        ServiceMethod="GetSectionName" OnClientShowing="clientShowing" OnClientItemSelected="GetSUName">
                                                    </ajaxToolKit:AutoCompleteExtender>
                                                    <asp:RequiredFieldValidator ID="rfvSUName" runat="server" ControlToValidate="txtSectionUserName"
                                                        Display="None" ErrorMessage="Please Enter Send To User." SetFocusOnError="True"
                                                        ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                </div>

                                                <div class="form-group col-lg-6 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>Send To User Role </label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlReceiverRole" CssClass="form-control" runat="server" TabIndex="1"
                                                        AppendDataBoundItems="true" ToolTip="Select Send To User Role" data-select2-enable="true">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvRRole" runat="server" ErrorMessage="Please Select Send To User Role."
                                                        ControlToValidate="ddlReceiverRole" InitialValue="0" Display="None" ValidationGroup="Submit" />
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>



                                </asp:Panel>
                            </div>
                            <div class="col-lg-6 col-md-12 col-12">
                                <asp:Panel ID="divMovement" runat="server" Visible="false">

                                    <div class="sub-heading">
                                        <h5>Add Note</h5>
                                    </div>
                                    <div class="row">
                                        <div class="form-group col-lg-12 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Note </label>
                                            </div>
                                            <asp:TextBox ID="txtRemark" runat="server" TabIndex="1" CssClass="form-control" ToolTip="Enter Note" TextMode="MultiLine" />
                                            <asp:RequiredFieldValidator ID="rfvRemark" runat="server" ControlToValidate="txtRemark"
                                                Display="None" ErrorMessage="Please Enter Note." ValidationGroup="Submit"></asp:RequiredFieldValidator>

                                        </div>
                                    </div>

                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                    <asp:Panel ID="pnlCategoryFileUpload" runat="server" Visible="false">
                        <div class="col-12">
                            <div class="sub-heading">
                                <h5>Select Documents From Category</h5>
                            </div>
                            <div class="row">


                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label>Category Files </label>
                                    </div>
                                    <asp:DropDownList ID="ddlCategory" CssClass="form-control" runat="server" TabIndex="1" data-select2-enable="true" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged"
                                        AppendDataBoundItems="true" AutoPostBack="true" ToolTip="Select Category">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>

                                </div>
                                <div class="form-group col-lg-4 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label></label>
                                    </div>
                                    <asp:Label ID="lblPathOfSelectedCategory" runat="server" Text="" Visible="false"></asp:Label>
                                </div>
                            </div>
                            <div class="col-12">
                                <asp:Panel ID="pnlFile" runat="server" Visible="false">
                                    <asp:ListView ID="lvfileUpload" runat="server">
                                        <LayoutTemplate>
                                            <div id="lgv1">
                                                <div class="sub-heading">
                                                    <h5>Download files</h5>
                                                </div>

                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                    <thead>
                                                        <tr class="bg-light-blue">
                                                            <th>Select
                                                            </th>
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
                                                    <asp:CheckBox ID="chkSelect" runat="server" ToolTip='<%# Eval("IDNO")%>' />
                                                </td>
                                                <td>
                                                    <asp:HiddenField ID="hdnUPLNO" runat="server" Value='<%# Eval("UPLNO")%>' />
                                                    <asp:Label ID="lblFileName" runat="server" Text='<%# Eval("FILENAME")%> '></asp:Label>
                                                    <asp:HiddenField ID="hdnSourcePath" runat="server" Value='<%# Eval("SOURCEPATH")%>' />
                                                    <asp:HiddenField ID="hdnUANO" runat="server" Value='<%# Eval("UA_NO")%>' />
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="imgFile" runat="Server" ImageUrl="~/images/action_down.gif" CommandArgument='<%# Eval("FILEPATH")%>'
                                                        AlternateText='<%# Eval("FILENAME")%>' OnClick="imgdownloadFirst_Click" />
                                                </td>

                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>
                            <div class="col-12 btn-footer">

                                <asp:Button ID="btnAddCategoryFiles" runat="server" Text="Add Documents" OnClick="btnAddCategoryFiles_Click" CssClass="btn btn-primary"
                                    ToolTip="Click here to add selected documents" TabIndex="1" Visible="false" />

                            </div>


                        </div>
                    </asp:Panel>

                    <asp:Panel ID="pnlUploadNewDocuments" runat="server">

                        <div class="col-12">
                            <div class="sub-heading">
                                <h5>Upload New Documents</h5>
                            </div>

                            <div class="row">
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                  <label>    <b>Note: Your Upload File Name Should Not Contain Any  " _  , - " </b> </label> 
                                      <%--  <label>Upload Files </label>--%>
                                    </div>

                                    <asp:FileUpload ID="FileUpload1" runat="server" class="multi" TabIndex="1" ToolTip="Click here to Upload Document" />&nbsp;        
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12 mt-2">
                                    <asp:Button ID="btnAdd" runat="server" Text="Add Documents" OnClick="btnAdd_Click" CssClass="btn btn-primary"
                                        ToolTip="Click here to upload new documents" TabIndex="1" />
                                </div>
                            </div>
                        </div>
                    </asp:Panel>

                    <div class="col-12">
                        <asp:Panel ID="pnlFinalList" runat="server" Visible="false">
                            <asp:ListView ID="lvFinalList" runat="server" OnItemDataBound="lvFinalList_ItemDataBound">
                                <LayoutTemplate>
                                    <div id="lgv1">
                                        <div class="sub-heading">
                                            <h5>List of documents</h5>
                                        </div>
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                            <thead>
                                                <tr class="bg-light-blue">
                                                    <th>Delete
                                                    </th>
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
                                            <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Images/delete.png" CommandArgument='<%# Eval("FILENAME")%> '
                                                AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click" CommandName='<%# Eval("IDNO")%>'
                                                OnClientClick="javascript:return confirm('Are you sure you want to delete this file?')" />
                                        </td>
                                        <td>
                                            <asp:HiddenField ID="hdnFinalIDNO" runat="server" Value='<%# Eval("IDNO")%>' />
                                            <asp:HiddenField ID="hdnFinalUPLNO" runat="server" Value='<%# Eval("UPLNO")%>' />
                                            <asp:Label ID="lblFinalFileName" runat="server" Text='<%# Eval("FILENAME")%> '></asp:Label>

                                        </td>
                                        <td>
                                            <asp:ImageButton ID="imgFile" runat="Server"  ImageUrl="~/Images/action_down.png" CommandArgument='<%# Eval("FILEPATH")%>'
                                                AlternateText='<%# Eval("FILENAME")%>' OnClick="imgdownload_Click" CommandName='<%# Eval("UA_NO")%>' />
                                            <asp:HiddenField ID="hdnSourcePath" runat="server" Value='<%# Eval("SOURCEPATH")%>' />
                                        </td>

                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </asp:Panel>
                    </div>

                    <div class="col-12">
                        <asp:Panel ID="pnlNewFiles" runat="server" Visible="false">
                            <asp:ListView ID="lvNewFiles" runat="server">
                                <LayoutTemplate>
                                    <div id="lgv1">
                                        <div class="sub-heading">
                                            <h5>List of documents</h5>
                                        </div>
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                            <thead>
                                                <tr class="bg-light-blue">
                                                    <th>Delete
                                                    </th>
                                                    <th>File Name
                                                    </th>
                                                    <th runat="server" visible="false">User Name  <%----24/01/2022--%>
                                                                </th>
                                                    <th>Download
                                                    </th>
                                                   <%-- <th>Preview
                                                    </th>--%>
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
                                            <asp:ImageButton ID="btnDeleteNew" runat="server" ImageUrl="~/Images/delete.png" CommandArgument='<%#DataBinder.Eval(Container, "DataItem") %>'
                                                AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDeleteNew_Click"
                                                OnClientClick="javascript:return confirm('Are you sure you want to delete this file?')" />
                                        </td>
                                        <td>
                                            <%#GetFileName(DataBinder.Eval(Container, "DataItem")) %>
                                        </td>

                                        <td runat="server" visible="false">
                                                        <%#GetUserName(DataBinder.Eval(Container, "DataItem")) %>     <%----03/12/2021--%>
                                                    </td>

                                        <td>
                                            <asp:ImageButton ID="imgFile" runat="Server" ImageUrl="~/images/action_down.png"
                                                AlternateText='<%#DataBinder.Eval(Container, "DataItem") %>' ToolTip='<%#DataBinder.Eval(Container, "DataItem")%>' OnClick="imgdownload_Click" />
                                        </td>
                                       <%-- <td>
                                            <asp:ImageButton ID="imgPreview" runat="Server" ImageUrl="~/images/action_down.png"
                                                AlternateText='<%#DataBinder.Eval(Container, "DataItem") %>' OnClick="imgPreview_Click" />
                                            <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Eval("SOURCEPATH")%>' />
                                        </td>--%>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </asp:Panel>
                    </div>

                    <div id="divButtons" runat="server" class="col-12 btn-footer">
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" TabIndex="1" ValidationGroup="Submit" OnClick="btnSubmit_Click"
                            CssClass="btn btn-primary" ToolTip="Click here to Submit" CausesValidation="true" OnClientClick="return ValidateFields()"/>
                        <asp:Button ID="btnReport" runat="server" Text="Report" TabIndex="1" OnClick="btnReport_Click"
                            CssClass="btn btn-info" CausesValidation="false" ToolTip="Click here to Show Report" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="1" OnClick="btnCancel_Click"
                            CssClass="btn btn-warning" CausesValidation="false" ToolTip="Click here to Reset" />
                        <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false"
                            ValidationGroup="Submit" />
                    </div>
                    <div class="col-12">
                        <asp:Panel ID="pnlList" runat="server">
                            <asp:ListView ID="lvFile" runat="server" OnItemDataBound="lvFile_ItemDataBound">
                                <LayoutTemplate>
                                    <div id="lgv1">
                                        <div class="sub-heading">
                                            <h5>File Entry List</h5>
                                        </div>
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                            <thead>
                                                <tr class="bg-light-blue">
                                                    <th>EDIT
                                                    </th>
                                                    <th>FILE NO.
                                                    </th>
                                                    <th>FILE NAME
                                                    </th>
                                                    <th>OWNER NAME
                                                    </th>
                                                    <th>CREATION DATE
                                                    </th>
                                                    <%-- <th>DESCRIPTION
                                                    </th>--%>
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
                                            <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit1.png" CommandArgument='<%# Eval("FILE_ID")%>'
                                                AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />
                                        <td>
                                            <%# Eval("FILE_CODE")%>
                                        </td>
                                        <td>
                                            <%# Eval("FILE_NAME")%>
                                        </td>
                                        <td>
                                            <%# Eval("UA_FULLNAME")%>
                                        </td>
                                        <td>
                                            <%# Eval("CREATION_DATE", "{0:dd-MMM-yyyy}")%>
                                        </td>
                                        <%--  <td>
                                            <%# Eval("DESCRIPTION")%>
                                        </td>--%>
                                        <td>
                                            <%# Eval("DOCUMENT_TYPE")%>
                                        </td>
                                        <td>
                                            <%# Eval("STATUS")%>
                                        </td>
                                        <td>
                                            <%# Eval("MOVEMENT_STATUS")%>
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

    <asp:LinkButton ID="lnkDummy" runat="server"></asp:LinkButton>
    <ajaxToolKit:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mpe" runat="server"
        PopupControlID="pnlPopup" TargetControlID="lnkDummy" BackgroundCssClass="modalBackground">
    </ajaxToolKit:ModalPopupExtender>
    <asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup" Style="display: none">
        <div class="header">
            ITLE Video
        </div>
        <div class="body">
            <iframe id="video" runat="server" width="600" height="415" frameborder="0" allowfullscreen></iframe>
            <br />
            <br />
            <asp:Button ID="btnClose" runat="server" CssClass="btn btn-warning" Text="Close" />
        </div>
    </asp:Panel>

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

        function ValidateFields() {
            document.getElementById("ctl00_ContentPlaceHolder1_btnSubmit").onclick = function () {
                //disable
                this.disabled = true;
            }
        }

        function CheckValidUser(ctrl) {
            var name = ctrl.value;
            var len = name.split('*').length;
            var GetSuName = document.getElementById('ctl00_ContentPlaceHolder1_HiddenGetSuName').value
            var txtusername = document.getElementById('ctl00_ContentPlaceHolder1_txtSectionUserName').value;
           // alert(txtusername);
            
            if (len == 1) {
                //alert(len);
                if (GetSuName == 0 && txtusername!="") {
                   // alert(GetSuName);
                    alert("Please Select Valid User from List.");
                    ctrl.value = "";
                    // ctrl.focus();
                }
            }
        }
        </script>
</asp:Content>


