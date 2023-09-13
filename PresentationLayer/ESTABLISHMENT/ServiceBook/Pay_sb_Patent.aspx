<%@ Page Language="C#" MasterPageFile="~/ServiceBookMaster.master" AutoEventWireup="true" CodeFile="Pay_sb_Patent.aspx.cs"
    Inherits="ESTABLISHMENT_ServiceBook_Pay_sb_Patent" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="sbhead" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="sbctp" runat="Server">
    <asp:UpdatePanel ID="updImage" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <form role="form">

                        <div class="col-md-12">
                            <div class="col-md-12">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="sub-heading">
                                                <h5>Patent</h5>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <asp:Panel ID="pnlFm" runat="server">


                                    <div class="panel panel-info">
                                        <%--<div class="panel panel-heading">Patent</div>--%>
                                        <div class="panel panel-body">
                                            <%-- Modified by Saahil Trivedi 22/01/2022--%>
                                            <div class="col-md-12">
                                                <div class="row">
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">

                                                            <label><span style="color: #FF0000">*</span> Title of the Patent : </label>
                                                        </div>
                                                        <asp:TextBox ID="txtPatent" runat="server" TabIndex="1" MaxLength="200" CssClass="form-control"
                                                            ToolTip="Enter Title Of Patent" onkeypress="return CheckAlphabet(event,this);"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvPatent" runat="server" ControlToValidate="txtPatent"
                                                            Display="None" ErrorMessage="Please Enter Title Of Patent" SetFocusOnError="True" ValidationGroup="ServiceBook"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label><span style="color: #FF0000">*</span> Applicant /Assignee Name : </label>
                                                        </div>
                                                        <asp:TextBox ID="txtApplicantname" runat="server" TabIndex="2" MaxLength="200" CssClass="form-control"
                                                            ToolTip="Enter Name Of Applicant /Assignee Name" onkeypress="return CheckAlphabet(event,this);"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvOragName" runat="server" ControlToValidate="txtApplicantname"
                                                            Display="None" ErrorMessage="Please Enter Name Of Applicant /Assignee Name" SetFocusOnError="true" ValidationGroup="ServiceBook"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label><span style="color: #FF0000">*</span>  Role : </label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlrole" runat="server" AppendDataBoundItems="true" data-select2-enable="true"
                                                            CssClass="form-control" ToolTip="Select Role" TabIndex="3">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            <asp:ListItem Value="1">Inventor</asp:ListItem>
                                                            <asp:ListItem Value="2">Co-Inventor</asp:ListItem>
                                                            <asp:ListItem Value="3">other</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfRole" runat="server" ControlToValidate="ddlrole"
                                                            Display="None" ErrorMessage="Please Select Role" ValidationGroup="ServiceBook"
                                                            InitialValue="0"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Other Role : </label>
                                                        </div>
                                                        <asp:TextBox ID="txtotherrole" runat="server" TabIndex="4" MaxLength="150" CssClass="form-control"
                                                            ToolTip="Enter Other Role"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-12">
                                                <div class="row">
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label><span style="color: #FF0000">*</span> Category : </label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlCategory" runat="server" AppendDataBoundItems="true" data-select2-enable="true"
                                                            CssClass="form-control" ToolTip="Select Category" TabIndex="5">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            <asp:ListItem Value="1">National</asp:ListItem>
                                                            <asp:ListItem Value="2">International</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvcategory" runat="server" ControlToValidate="ddlCategory"
                                                            Display="None" ErrorMessage="Please Select Category" ValidationGroup="ServiceBook"
                                                            InitialValue="0"></asp:RequiredFieldValidator>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">

                                                            <label>Application No. : </label>
                                                        </div>
                                                        <asp:TextBox ID="txtAppNo" runat="server" TabIndex="6" MaxLength="30" CssClass="form-control"
                                                            ToolTip="Enter Application No"></asp:TextBox>
                                                        <%--onkeyup="return validateNumeric(this);"--%>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label><span style="color: #FF0000">*</span>Status :  </label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlstatus" runat="server" AppendDataBoundItems="true" data-select2-enable="true"
                                                            CssClass="form-control" ToolTip="Select Status" TabIndex="7">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            <asp:ListItem Value="1">Filed</asp:ListItem>
                                                            <asp:ListItem Value="2">Published</asp:ListItem>
                                                            <asp:ListItem Value="3">Granted</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvstatus" runat="server" ControlToValidate="ddlstatus"
                                                            Display="None" ErrorMessage="Please Select Staus" ValidationGroup="ServiceBook"
                                                            InitialValue="0"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label><span style="color: #FF0000">*</span>Status Date :  </label>
                                                        </div>
                                                        <div class="input-group date">
                                                            <div class="input-group-addon">
                                                                <asp:Image ID="Image2" runat="server" class="fa fa-calendar text-blue" />
                                                            </div>
                                                            <asp:TextBox ID="txtStatusDate" runat="server" CssClass="form-control" ToolTip="Enter Status Date"
                                                                TabIndex="8" Style="z-index: 0;"></asp:TextBox>
                                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtStatusDate"
                                                                PopupButtonID="Image2" Enabled="true" EnableViewState="true" PopupPosition="BottomLeft">
                                                            </ajaxToolKit:CalendarExtender>
                                                            <ajaxToolKit:MaskedEditExtender ID="meStatusDate" runat="server" TargetControlID="txtStatusDate"
                                                                Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                                AcceptNegative="Left" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate">
                                                            </ajaxToolKit:MaskedEditExtender>
                                                            <ajaxToolKit:MaskedEditValidator ID="mevStatusDate" runat="server" ControlExtender="meStatusDate"
                                                                ControlToValidate="txtStatusDate"
                                                                InvalidValueMessage="Status Date is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                                                TooltipMessage="Please Enter Status Date " EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                                                ValidationGroup="ServiceBook" SetFocusOnError="True" />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtStatusDate"
                                                                Display="None" ErrorMessage="Please Enter Status Date"
                                                                ValidationGroup="ServiceBook" SetFocusOnError="True">
                                                            </asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-12">
                                                <div class="row">
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>File No./Patent No :  </label>
                                                        </div>
                                                        <asp:TextBox ID="txtFileNo" runat="server" TabIndex="9" MaxLength="21" CssClass="form-control"
                                                            ToolTip="Enter File No"></asp:TextBox>
                                                        <%-- onkeyup="return validateNumeric(this);"--%>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Withdrawn :  </label>
                                                            <%--<span style="color: #FF0000">*</span>--%>
                                                        </div>
                                                        <asp:DropDownList ID="ddlwithdrawn" runat="server" AppendDataBoundItems="true" data-select2-enable="true"
                                                            CssClass="form-control" ToolTip="Select Withdrawn" TabIndex="10">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            <asp:ListItem Value="1">Yes</asp:ListItem>
                                                            <asp:ListItem Value="2">No</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <%--<asp:RequiredFieldValidator ID="rfvwithdrawn" runat="server" ControlToValidate="ddlwithdrawn"
                                                        Display="None" ErrorMessage="Please Select Withdrawn" ValidationGroup="ServiceBook"
                                                        InitialValue="0"></asp:RequiredFieldValidator>--%>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Date : </label>
                                                            <%--<span style="color: #FF0000">*</span>--%>
                                                        </div>
                                                        <div class="input-group date">
                                                            <div class="input-group-addon">
                                                                <asp:Image ID="Image1" runat="server" class="fa fa-calendar text-blue" />
                                                            </div>
                                                            <asp:TextBox ID="txtDate" runat="server" CssClass="form-control" ToolTip="Enter Date"
                                                                TabIndex="11" Style="z-index: 0;"></asp:TextBox>
                                                            <ajaxToolKit:CalendarExtender ID="ceToDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDate"
                                                                PopupButtonID="Image1" Enabled="true" EnableViewState="true" PopupPosition="BottomLeft">
                                                            </ajaxToolKit:CalendarExtender>
                                                            <ajaxToolKit:MaskedEditExtender ID="meToDate" runat="server" TargetControlID="txtDate"
                                                                Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                                AcceptNegative="Left" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate">
                                                            </ajaxToolKit:MaskedEditExtender>
                                                            <ajaxToolKit:MaskedEditValidator ID="mevToDate" runat="server" ControlExtender="meToDate"
                                                                ControlToValidate="txtDate" EmptyValueMessage="Please Enter Date of talk"
                                                                InvalidValueMessage="Date is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                                                TooltipMessage="Please Enter Date" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                                                ValidationGroup="ServiceBook" SetFocusOnError="True" />
                                                            <%-- <asp:RequiredFieldValidator ID="rfvPublicationDate" runat="server" ControlToValidate="txtDate"
                                                            Display="None" ErrorMessage="Please Enter Date"
                                                            ValidationGroup="ServiceBook" SetFocusOnError="True">
                                                        </asp:RequiredFieldValidator>--%>
                                                        </div>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Number of members involved : </label>
                                                        </div>
                                                        <%--<span style="color: #FF0000">*</span>--%>
                                                        <asp:TextBox ID="txtNumber" runat="server" CssClass="form-control" ToolTip="Enter Number of members involved"
                                                            TabIndex="12" onkeyup="return validateNumeric(this);" MaxLength="6"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group col-md-4">
                                                <label>Subject of The Patent: </label>
                                                <%--<span style="color: #FF0000">*</span>--%>
                                                <asp:TextBox ID="txtsubject" runat="server" CssClass="form-control" ToolTip="Subject Of The Patent"
                                                    MaxLength="300" TextMode="MultiLine" TabIndex="13"
                                                    onkeyDown="checkTextAreaMaxLength(this,event,'400');" onkeyup="textCounter(this, this.form.remLen, 400);"></asp:TextBox>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12" id="divBlob" runat="server" visible="false">
                                                <asp:Label ID="lblBlobConnectiontring" runat="server" Text=""></asp:Label>
                                                <asp:HiddenField ID="hdnBlobCon" runat="server" />
                                                <asp:Label ID="lblBlobContainer" runat="server" Text=""></asp:Label>
                                                <asp:HiddenField ID="hdnBlobContainer" runat="server" />
                                            </div>
                                        </div>
                                    </div>

                                    <%--<div class="col-md-12" id="Div2" runat="server">--%>
                                    <asp:UpdatePanel ID="UpdatePanelMedical" runat="server">
                                        <ContentTemplate>

                                            <div class="col-12">
                                                <div class="row">
                                                    <div class="col-12">
                                                        <div class="sub-heading">
                                                            <h5>Details of members</h5>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            </div>
                                            <asp:Panel ID="Panel1" runat="server">
                                                <div class="panel panel-info">

                                                    <div class="panel-body">
                                                        <div class="form-group row" id="divMedicineDetails" style="display: block;">
                                                            <div class="col-md-12">
                                                                <div class="row">
                                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <label><span style="color: red;">*</span> Name :</label>
                                                                        </div>
                                                                        <asp:TextBox ID="txtName" runat="server" TabIndex="14" ToolTip="Please Enter Name " class="form-control"
                                                                            autocomplete="off" MaxLength="200"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtName" Display="None"
                                                                            ErrorMessage="Please Enter  Name" SetFocusOnError="true" ValidationGroup="Info">
                                                                        </asp:RequiredFieldValidator>



                                                                    </div>
                                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <label><span style="color: red;">*</span> Address:</label>
                                                                        </div>
                                                                        <asp:TextBox ID="txtAddress" runat="server"
                                                                            ToolTip="Please Enter Address " class="form-control" TabIndex="15" autocomplete="off" MaxLength="200"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtAddress" Display="None"
                                                                            ErrorMessage="Please Enter Address" SetFocusOnError="true" ValidationGroup="Info">
                                                                        </asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <label>Role <span style="color: red;">*</span> :</label>
                                                                        </div>
                                                                        <asp:DropDownList ID="ddlrolenew" runat="server" AppendDataBoundItems="true" data-select2-enable="true"
                                                                            CssClass="form-control" ToolTip="Select Role" TabIndex="16">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                            <asp:ListItem Value="Inventor">Inventor</asp:ListItem>
                                                                            <asp:ListItem Value="Co-investor">Co-investor</asp:ListItem>
                                                                            <asp:ListItem Value="other">other</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="ddlrolenew" Display="None"
                                                                            ErrorMessage="Please Enter Role" SetFocusOnError="true" ValidationGroup="Info" InitialValue="0">
                                                                        </asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <div class="col-md-3">
                                                                        <br />
                                                                        <asp:Button ID="btnAdd" runat="server" TabIndex="17" Text="Add Details" class="btn btn-primary" OnClick="btnAdd_Click"
                                                                            ValidationGroup="Info" />
                                                                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="Info"
                                                                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-12">
                                                                <asp:Panel ID="pnlEnclosure" runat="server" Visible="false">
                                                                    <asp:ListView ID="lvEnclosures" runat="server">
                                                                        <LayoutTemplate>
                                                                            <div class="sub-heading">
                                                                                <h5>Details of Members</h5>
                                                                            </div>
                                                                            <table class="table table-bordered table-hover">
                                                                                <thead>
                                                                                    <tr class="bg-light-blue">
                                                                                        <th>Remove</th>
                                                                                        <th>Name</th>
                                                                                        <th>Address</th>
                                                                                        <th>Role</th>
                                                                                    </tr>
                                                                                </thead>
                                                                                <tbody>
                                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                                </tbody>
                                                                            </table>
                                                                            <%--</div>--%>
                                                                        </LayoutTemplate>
                                                                        <ItemTemplate>
                                                                            <tr>
                                                                                <%-- Modified by Saahil Trivedi 22/01/2022--%>
                                                                                <td>
                                                                                    <asp:ImageButton ID="btnDeleteEN" runat="server" CausesValidation="false"
                                                                                        CommandArgument='<%# Eval("SEQNO")%>' ImageUrl="~/Images/delete.png" ToolTip="Delete Record"
                                                                                        OnClick="btnDeleteEN_Click" EnableViewState="true" OnClientClick="javascript:return confirm('Are you sure you want to delete ?')" /></td>
                                                                                <%--CommandArgument='<%# Eval("SEQNO")%>'--%>
                                                                                <td><%# Eval("Name")%></td>
                                                                                <td><%# Eval("Address")%></td>
                                                                                <td><%# Eval("Role")%></td>
                                                                                <%--<asp:HiddenField ID="hdnmedcost" runat="server" Value='<%# Eval("MED_PRICE")%>' />--%>
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
                                    </asp:UpdatePanel>


                                    <%-- <div class="panel panel-info">
                                            <div class="panel panel-heading">Upload Documents</div>
                                            <div class="panel panel-body">
                                                <div class="col-md-12">
                                                   <label>Upload Document :</label>
                                                    <asp:FileUpload ID="flupld" runat="server" ToolTip="Click here to Upload Document" TabIndex="19" />

                                                </div>
                                            </div>
                                        </div>--%>
                                    <%--</div>--%>

                                    <asp:UpdatePanel ID="pnlnew" runat="server">
                                        <ContentTemplate>
                                            <div class="col-12">
                                                <div class="row">
                                                    <div class="col-12">
                                                        <div class="sub-heading">
                                                            <h5>Upload files</h5>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            </div>
                                                <asp:Panel ID="Panel2" runat="server">
                                                    <div class="panel panel-info">

                                                        <div class="panel-body">
                                                            <div class="form-group row" id="divUploadFiles" style="display: block;">
                                                                <div class="col-md-6">
                                                                    <label>Multiple Files Can Be Attached :</label>
                                                                    <asp:FileUpload ID="FileUpload1" runat="server" TabIndex="18" ToolTip="Upload Multiple Files Here" /><br />
                                                                    <asp:Label ID="Label2" runat="server" Text=" Please Select valid Document file(e.g. .pdf,.jpg,.doc) upto 10MB" ForeColor="Red"></asp:Label>
                                                                </div>
                                                                <div class="col-md-3">
                                                                    <br />
                                                                    <asp:Button ID="btnUploadDoc" runat="server" Text="Add" TabIndex="19" class="btn btn-primary"
                                                                        OnClick="btnUploadDoc_Click" ToolTip="Click here to uplaod multiple files" />
                                                                </div>
                                                                <div id="divAttch" runat="server" style="display: none">
                                                                    <div class="col-md-12">
                                                                        <asp:Panel ID="pnlfiles" runat="server">
                                                                            <asp:ListView ID="LVFiles" runat="server">
                                                                                <LayoutTemplate>
                                                                                    <div class="sub-heading">
                                                                                        <h5>Attached Files</h5>
                                                                                    </div>
                                                                                    <table class="table table-bordered table-hover">
                                                                                        <thead>
                                                                                            <tr class="bg-light-blue">
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
                                                                                    <%--</div>--%>
                                                                                </LayoutTemplate>
                                                                                <ItemTemplate>
                                                                                    <tr>
                                                                                        <%--<td>
                                                                                            <asp:ImageButton ID="btnDelFile" runat="server" ImageUrl="~/Images/delete.png"
                                                                                                CommandArgument=' <%#Eval("GETFILE") %>' AlternateText=' <%#Eval("APPID") %>' ToolTip="Delete Record"
                                                                                                OnClientClick="javascript:return confirm('Are you sure you want to delete this file?')" OnClick="btnDelFile_Click" />
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:HyperLink ID="lnkDownload" runat="server" Target="_blank" NavigateUrl='<%# GetFileNamePath(Eval("GETFILE"),Eval("FUID"),Eval("IDNO"),Eval("FOLDER"),Eval("APPID"))%>'><%# Eval("DisplayFileName")%></asp:HyperLink>
                                                                                        </td>--%>
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
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="btnUploadDoc" />
                                        </Triggers>
                                    </asp:UpdatePanel>


                                    <div class="form-group col-md-12">
                                        <p class="text-center">
                                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="ServiceBook" TabIndex="20"
                                                OnClick="btnSubmit_Click" CssClass="btn btn-primary" ToolTip="Click here to Submit" />&nbsp;
                                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" TabIndex="21"
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
                                                <asp:Label ID="lblErrMsg" runat="server" SkinID="Errorlbl" Text="No Rows In Patent Details"></asp:Label>
                                            </p>
                                        </EmptyDataTemplate>
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Patent Details</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">

                                                <%--   <table class="table table-bordered table-hover">--%>
                                                <thead>
                                                    <tr class="bg-light-blue">
                                                        <th>Action
                                                        </th>
                                                        <th>Title of the Patent
                                                        </th>
                                                        <th>Applicant /Assignee Name
                                                        </th>
                                                        <th>Role
                                                        </th>
                                                        <th>Category
                                                        </th>
                                                        <th>Status
                                                        </th>
                                                        <th>Withdrawn
                                                        </th>
                                                        <th>Date
                                                        </th>
                                                        <%-- <th>ATTACHMENT
                                                                    </th>--%>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                            <%--</div>--%>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <%-- Modified by Saahil Trivedi 22/01/2022--%>
                                                <td>
                                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("PCNO")%>'
                                                        AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />&nbsp;
                                        <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Images/delete.png" CommandArgument='<%# Eval("PCNO") %>'
                                            AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                            OnClientClick="showConfirmDel(this); return false;" />
                                                </td>
                                                <td>
                                                    <%# Eval("Title_Patent")%>
                                                </td>
                                                <td>
                                                    <%# Eval("Applicant_Name")%>
                                                </td>
                                                <td>
                                                    <%# Eval("Role")%>
                                                </td>
                                                <td>
                                                    <%# Eval("Category")%>
                                                </td>
                                                <td>
                                                    <%# Eval("Status")%>
                                                </td>
                                                <td>
                                                    <%# Eval("Withdrawn")%> 
                                                </td>
                                                <td>
                                                    <%# Eval("DATE", "{0:dd/MM/yyyy}")%>
                                                </td>
                                                <%--<td>
                                                        <%# Eval("Description")%>
                                                    </td>--%>
                                                <%--<td>
                                                        <asp:HyperLink ID="lnkDownload" runat="server" Target="_blank" NavigateUrl='<%# GetFileNamePath(Eval("ATTACHMENT"),Eval("PCNO"),Eval("IDNO"))%>'><%# Eval("ATTACHMENT")%></asp:HyperLink>
                                                    </td>--%>
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
        </ContentTemplate>
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

        //function validateAlphabet(txt) {
        //    var expAlphabet = /^[A-Za-z .]+$/;
        //    if (txt.value.search(expAlphabet) == -1) {
        //        txt.value = txt.value.substring(0, (txt.value.length) - 1);
        //        txt.value = '';
        //        txt.focus = true;
        //        alert("Only Alphabets allowed!");
        //        return false;
        //    }
        //    else
        //        return true;
        //}


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
    </script>

    <script type="text/javascript">
        function checkTextAreaMaxLength(textBox, e, length) {

            var mLen = textBox["MaxLength"];
            if (null == mLen)
                mLen = length;

            var maxLength = parseInt(mLen);
            if (!checkSpecialKeys(e)) {
                if (textBox.value.length > maxLength - 1) {
                    if (window.event) {//IE
                        e.returnValue = false;
                    }
                    else {//Firefox
                        e.preventDefault();
                    }
                }
            }
        }

        function textCounter(field, countfield, maxlimit) {
            if (field.value.length > maxlimit)
                field.value = field.value.substring(0, maxlimit);
            else
                countfield.value = maxlimit - field.value.length;
        }

    </script>

</asp:Content>



