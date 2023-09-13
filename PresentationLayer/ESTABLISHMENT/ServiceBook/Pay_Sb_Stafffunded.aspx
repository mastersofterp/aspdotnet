<%@ Page Language="C#" MasterPageFile="~/ServiceBookMaster.master" AutoEventWireup="true" CodeFile="Pay_Sb_Stafffunded.aspx.cs"
    Inherits="ESTABLISHMENT_ServiceBook_Pay_Sb_Stafffunded" %>

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
                                                    <h5>Funded Project</h5>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <asp:Panel ID="pnlFm" runat="server">

                                        <div class="panel panel-info">
                                            <%--<div class="panel panel-heading">Funded Project</div>--%>
                                            <div class="panel panel-body">
                                                <%-- Modified by Saahil Trivedi 25/01/2022--%>
                                                <div class="col-12">
                                                    <div class="row">
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label><span style="color: #FF0000">*</span>Project Title :</label>
                                                            </div>
                                                            <asp:TextBox ID="txtProjname" runat="server" TabIndex="1" MaxLength="200" CssClass="form-control"
                                                                ToolTip="Enter Project Title" onkeypress="return CheckAlphabet(event,this);" autocomplete="off"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvAwardName" runat="server" ControlToValidate="txtProjname"
                                                                Display="None" ErrorMessage="Please Enter Project Title " SetFocusOnError="True" ValidationGroup="ServiceBook"></asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label><span style="color: #FF0000">*</span>Funding Agency Name :</label>
                                                            </div>
                                                            <asp:TextBox ID="txtFunName" runat="server" TabIndex="2" MaxLength="250" CssClass="form-control" autocomplete="off"
                                                                ToolTip="Enter Funding Agency Name" onkeypress="return CheckAlphabet(event,this);"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvOragName" runat="server" ControlToValidate="txtFunName"
                                                                Display="None" ErrorMessage="Please Enter Funding Agency Name" SetFocusOnError="true" ValidationGroup="ServiceBook"></asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>Address :</label>
                                                            </div>
                                                            <%--<span style="color: #FF0000">*</span>--%>
                                                            <asp:TextBox ID="txtadd" runat="server" TabIndex="3" MaxLength="200" CssClass="form-control" autocomplete="off"
                                                                ToolTip="Enter the Address"></asp:TextBox>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label><span style="color: #FF0000">*</span>Agency Category: </label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlagency" runat="server" AppendDataBoundItems="true" data-select2-enable="true"
                                                                CssClass="form-control" ToolTip="Select Agency Category" TabIndex="4">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                <asp:ListItem Value="1">Govt</asp:ListItem>
                                                                <asp:ListItem Value="2">Semi Govt</asp:ListItem>
                                                                <asp:ListItem Value="3">Private</asp:ListItem>
                                                                <asp:ListItem Value="4">NGO</asp:ListItem>
                                                                <asp:ListItem Value="5">Industry</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfAgency" runat="server" ControlToValidate="ddlagency"
                                                                Display="None" ErrorMessage="Please Select Agency Category" ValidationGroup="ServiceBook"
                                                                InitialValue="0"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-12">
                                                    <div class="row">
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label><span style="color: #FF0000">*</span>Role :</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlrole" runat="server" AppendDataBoundItems="true" data-select2-enable="true"
                                                                CssClass="form-control" ToolTip="Select Role" TabIndex="5">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                <asp:ListItem Value="1">Principal Investigator</asp:ListItem>
                                                                <asp:ListItem Value="2">Co-Principal Investigator</asp:ListItem>

                                                                <%--<asp:ListItem Value="2">Private Investigator</asp:ListItem>
                                                        <asp:ListItem Value="3">Co-investor</asp:ListItem>
                                                        <asp:ListItem Value="4">Others</asp:ListItem>--%>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfrole" runat="server" ControlToValidate="ddlrole"
                                                                Display="None" ErrorMessage="Please Select Role" ValidationGroup="ServiceBook"
                                                                InitialValue="0"></asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>Grant Sanctioned : </label>
                                                            </div>
                                                            <asp:TextBox ID="txtfund" runat="server" CssClass="form-control" MaxLength="10" TabIndex="6"
                                                                ToolTip="Please Enter Grant Sanctioned" autocomplete="off"
                                                                onkeypress="return numericdotOnly(event, this);"></asp:TextBox>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftbeAmount" runat="server" TargetControlID="txtfund"
                                                                ValidChars="0123456789." Enabled="True">
                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>Project Status : </label>
                                                            </div>
                                                            <%--<asp:TextBox ID="txtproject" runat="server" CssClass="form-control" ToolTip="Please Enter Project Status" TabIndex="7"
                                                        MaxLength="150" onkeypress="return CheckAlphabet(event,this);"></asp:TextBox>--%>
                                                            <asp:DropDownList ID="ddlProjectStatus" runat="server" AppendDataBoundItems="true" data-select2-enable="true"
                                                                CssClass="form-control" ToolTip="Select Project Level" TabIndex="7">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                <asp:ListItem Value="1">Ongoing</asp:ListItem>
                                                                <asp:ListItem Value="2">Completed</asp:ListItem>
                                                                <asp:ListItem Value="3">Active</asp:ListItem>
                                                                <asp:ListItem Value="4">Active and ongoing</asp:ListItem>
                                                                <asp:ListItem Value="5">Live</asp:ListItem>
                                                                <asp:ListItem Value="6">Submited But Not Selected</asp:ListItem>
                                                                <asp:ListItem Value="7">Submited Waiting For Reply</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>Nature Of Project : </label>
                                                            </div>
                                                            <asp:TextBox ID="txtnature" runat="server" CssClass="form-control" ToolTip="Please Enter Nature Of Project" TabIndex="8"
                                                                MaxLength="300" onkeypress="return CheckAlphabet(event,this);"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-12">
                                                    <div class="row">
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>Project Level:</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlProject" runat="server" AppendDataBoundItems="true" data-select2-enable="true"
                                                                CssClass="form-control" ToolTip="Select Project Level" TabIndex="9">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                <asp:ListItem Value="1">National</asp:ListItem>
                                                                <asp:ListItem Value="2">International</asp:ListItem>

                                                            </asp:DropDownList>
                                                            <%--  <asp:RequiredFieldValidator ID="rfvaward" runat="server" ControlToValidate="ddlAward"
                                                        Display="None" ErrorMessage="Please SelectAward Level" ValidationGroup="ServiceBook"
                                                        SetFocusOnError="True" InitialValue="0">
                                                    </asp:RequiredFieldValidator>--%>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>Scheme Name : </label>
                                                            </div>
                                                            <asp:TextBox ID="Txtsname" runat="server" CssClass="form-control" ToolTip="Please Enter Scheme Name" TabIndex="10"
                                                                MaxLength="250"></asp:TextBox>
                                                        </div>



                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label><span style="color: #FF0000">*</span>From Date :  </label>
                                                            </div>
                                                            <div class="input-group date">
                                                                <div class="input-group-addon">
                                                                    <asp:Image ID="imgCal" runat="server" class="fa fa-calendar text-blue" />
                                                                </div>
                                                                <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control" ToolTip="Enter From Date" TabIndex="11" Style="z-index: 0;" onBlur="Exp();" onChange="Exp();">
                                                                </asp:TextBox>
                                                                <ajaxToolKit:CalendarExtender ID="ceFromDate" runat="server" Format="dd/MM/yyyy"
                                                                    TargetControlID="txtFromDate" PopupButtonID="imgCal" Enabled="true" EnableViewState="true"
                                                                    PopupPosition="BottomLeft">
                                                                </ajaxToolKit:CalendarExtender>
                                                                <asp:RequiredFieldValidator ID="rfvFromDate" runat="server" ControlToValidate="txtFromDate"
                                                                    Display="None" ErrorMessage="Please Select From Date in (dd/MM/yyyy Format)"
                                                                    ValidationGroup="ServiceBook" SetFocusOnError="True">
                                                                </asp:RequiredFieldValidator>
                                                                <ajaxToolKit:MaskedEditExtender ID="meFromDate" runat="server" TargetControlID="txtFromDate"
                                                                    Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                                    AcceptNegative="Left" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate">
                                                                </ajaxToolKit:MaskedEditExtender>
                                                                <ajaxToolKit:MaskedEditValidator ID="mevFromDate" runat="server" ControlExtender="meFromDate"
                                                                    ControlToValidate="txtFromDate" EmptyValueMessage="Please Enter From Date"
                                                                    InvalidValueMessage="From Date is Invalid (Enter dd/MM/yyyy Format)"
                                                                    Display="None" TooltipMessage="Please Enter From Date" EmptyValueBlurredText="Empty"
                                                                    InvalidValueBlurredMessage="Invalid From Date" ValidationGroup="ServiceBook" SetFocusOnError="True" />
                                                            </div>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label><span style="color: #FF0000">*</span>To Date :  </label>
                                                            </div>
                                                            <div class="input-group date">
                                                                <div class="input-group-addon">
                                                                    <asp:Image ID="Image1" runat="server" class="fa fa-calendar text-blue" />
                                                                </div>
                                                                <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control"
                                                                    ToolTip="Enter To Date" TabIndex="12" Style="z-index: 0;" onBlur="Exp();" onChange="Exp();"></asp:TextBox>
                                                                <ajaxToolKit:CalendarExtender ID="ceToDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtToDate"
                                                                    PopupButtonID="Image1" Enabled="true" EnableViewState="true" PopupPosition="BottomLeft">
                                                                </ajaxToolKit:CalendarExtender>
                                                                <asp:RequiredFieldValidator ID="rfvToDate" runat="server" ControlToValidate="txtToDate"
                                                                    Display="None" ErrorMessage="Please Select To Date in (dd/MM/yyyy Format)" ValidationGroup="ServiceBook"
                                                                    SetFocusOnError="True">
                                                                </asp:RequiredFieldValidator>
                                                                <ajaxToolKit:MaskedEditExtender ID="meToDate" runat="server" TargetControlID="txtToDate"
                                                                    Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                                    AcceptNegative="Left" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate">
                                                                </ajaxToolKit:MaskedEditExtender>
                                                                <ajaxToolKit:MaskedEditValidator ID="mevToDate" runat="server" ControlExtender="meToDate"
                                                                    ControlToValidate="txtToDate" EmptyValueMessage="Please Enter To Date" InvalidValueMessage="To Date is Invalid (Enter dd/MM/yyyy Format)"
                                                                    Display="None" TooltipMessage="Please Enter To Date" EmptyValueBlurredText="Empty"
                                                                    InvalidValueBlurredMessage="Invalid To Date" ValidationGroup="ServiceBook" SetFocusOnError="True" />
                                                                <%--<asp:CompareValidator ID="cvToDate" runat="server" Display="None" ErrorMessage="To Date  Should be Greater than  or equal to From Date"
                                                            ValidationGroup="ServiceBook" SetFocusOnError="True" ControlToCompare="txtFromDate"
                                                            ControlToValidate="txtToDate" Operator="GreaterThanEqual" Type="Date"></asp:CompareValidator>--%>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-12">
                                                    <div class="row">
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>Duration : </label>
                                                            </div>
                                                            <asp:TextBox ID="txtDuration" runat="server" CssClass="form-control" ToolTip="Enter Duration" AutoComplete="off"
                                                                MaxLength="50" onkeydown="return EditControl(event,this);" onkeypress="return EditControl(event,this);"
                                                                onclick="return EditControl(event,this);" TabIndex="13"></asp:TextBox>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>Name of Principal Investigator:</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlInvestigator" runat="server" AppendDataBoundItems="true" data-select2-enable="true"
                                                                CssClass="form-control" ToolTip="Select Employee" TabIndex="14">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>

                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>Name of Co–Principal Investigator:</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlCoInvestigator" runat="server" AppendDataBoundItems="true" data-select2-enable="true"
                                                                CssClass="form-control" ToolTip="Select Employee" TabIndex="15">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>

                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>Web Link :</label>
                                                            </div>
                                                            <asp:TextBox ID="txtweblink" runat="server" CssClass="form-control" ToolTip="Enter Web Link" MaxLength="250"
                                                                TextMode="MultiLine" TabIndex="16" />
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divBlob" runat="server" visible="false">
                                                            <asp:Label ID="lblBlobConnectiontring" runat="server" Text=""></asp:Label>
                                                            <asp:HiddenField ID="hdnBlobCon" runat="server" />
                                                            <asp:Label ID="lblBlobContainer" runat="server" Text=""></asp:Label>
                                                            <asp:HiddenField ID="hdnBlobContainer" runat="server" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <%--  <div class="col-md-12">--%>
                                        <asp:UpdatePanel ID="pnlnew" runat="server">
                                            <ContentTemplate>
                                                <asp:Panel ID="Panel2" runat="server">
                                                    <div class="panel panel-info">
                                                        <div class="sub-heading">
                                                            <h5>Upload Files</h5>
                                                        </div>
                                                        <div class="panel-body">
                                                            <div class="form-group row" id="divUploadFiles" style="display: block;">
                                                                <div class="col-md-6">
                                                                    <label>Multiple Files Can Be Attached :</label>
                                                                    <asp:FileUpload ID="FileUpload1" runat="server" TabIndex="17" ToolTip="Upload Multiple Files Here" /><br />
                                                                    <asp:Label ID="Label2" runat="server" Text=" Please Select valid Document file(e.g. .pdf,.jpg,.doc) upto 10MB" ForeColor="Red"></asp:Label>
                                                                    <asp:Button ID="btnAdd" runat="server" Text="Add" TabIndex="18" class="btn btn-primary" OnClick="btnAdd_Click"
                                                                        ToolTip="Click here to uplaod multiple files" />
                                                                </div>



                                                                <%----%>
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
                                                                                                <%-- <th>Delete</th>                                                                                                  
                                                                                                    <th>File Name</th>--%>
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
                                                                                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/delete.png"
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
                                                <asp:PostBackTrigger ControlID="btnAdd" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                        <%--  </div>--%>
                                        <div class="form-group col-md-12">
                                            <p class="text-center">
                                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="ServiceBook" TabIndex="19"
                                                    OnClick="btnSubmit_Click" CssClass="btn btn-primary" ToolTip="Click here to Submit" />&nbsp;
                                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" TabIndex="20"
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
                                                    <asp:Label ID="lblErrMsg" runat="server" SkinID="Errorlbl" Text="No Rows In Staff Funded Project Details"></asp:Label>
                                                </p>
                                            </EmptyDataTemplate>
                                            <LayoutTemplate>
                                                <div class="sub-heading">
                                                    <h5>Funded Project
                                                    </h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%">

                                                    <%--<table class="table table-bordered table-hover">--%>
                                                    <thead>
                                                        <tr class="bg-light-blue">
                                                            <th>Action
                                                            </th>
                                                            <th>Project Title
                                                            </th>
                                                            <th>Funding Agency Name
                                                            </th>
                                                            <th>Agency Category
                                                            </th>
                                                            <th>Role
                                                            </th>
                                                            <th>Grant Sanctioned
                                                            </th>
                                                            <th>Project Status
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
                                                    <%-- Modified by Saahil Trivedi 25/01/2022--%>
                                                    <td>
                                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("SFNO")%>'
                                                            AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />&nbsp;
                                        <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Images/delete.png" CommandArgument='<%# Eval("SFNO") %>'
                                            AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                            OnClientClick="showConfirmDel(this); return false;" />
                                                    </td>
                                                    <td>
                                                        <%# Eval("Project_Title")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("Funding_Name")%>
                                                    </td>
                                                    <%--<td>
                                                        <%# Eval("Address")%>
                                                    </td>--%>
                                                    <td>
                                                        <%# Eval("Agency_Category")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("Role")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("Fund_Received")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("Project_Status")%>
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
    </script>
    <script type="text/javascript">
        debugger;
        function Exp() {

            var datejoing = document.getElementById('<%=txtFromDate.ClientID%>').value;
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
                        document.getElementById('<%=txtDuration.ClientID%>').value = '';
                    } else {
                        // date is valid
                    }
                } else {
                    // not a date
                }

                if (Object.prototype.toString.call(date1) === "[object Date]") {
                    // it is a date
                    if (isNaN(date1.getTime())) {  // d.valueOf() could also work
                        document.getElementById('<%=txtDuration.ClientID%>').value = '';
                        return;
                    } else {
                        // date is valid
                    }
                } else {
                    // not a date
                }

                if (date1 > date2) {
                    alert("To date should be greater than equal to from date.");
                    document.getElementById('<%=txtDuration.ClientID%>').value = '';
                    document.getElementById('<%=txtToDate.ClientID%>').value = '';
                    return;
                }

                dateDiff(date1, date2);
                //var timeDiff = Math.abs(parseInt(date1.getTime()) - parseInt(date2.getTime()));
                //var diffDays = Math.round(timeDiff / (1000 * 60 * 60 * 24));

                //var totalYears = Math.trunc(diffDays / 365);
                //var totalMonths = Math.trunc((diffDays % 365) / 30);
                //var totalDays = Math.trunc((diffDays % 365) % 30)
                //document.getElementById('<%=txtDuration.ClientID%>').value = totalYears + ' ' + 'Years' + ' ' + totalMonths + ' ' + 'Months' + ' ' + totalDays + ' ' + 'Days';
                // document.getElementById('<%=txtDuration.ClientID%>').value = totalYears + ' ' + 'Years' + ' ' + totalMonths + ' ' + 'Months' ;
            }
            else
                document.getElementById('<%=txtDuration.ClientID%>').value = '';
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
            document.getElementById('<%=txtDuration.ClientID%>').value = yearDiff + ' ' + 'Years' + ' ' + monthDiff + ' ' + 'Months' + ' ' + dayDiff + ' ' + 'Days';
            return yearDiff + 'Y ' + monthDiff + 'M ' + dayDiff + 'D';
        }

        function EditControl(event, obj) {
            obj.style.backgroundColor = "LightGray";

            // document.getElementById('<%=txtDuration.ClientID%>').contentEditable.replace = false;
            document.getElementById('<%=txtDuration.ClientID%>').contentEditable = false;
            // alert("Not Editable");
            return false;
        }

    </script>
</asp:Content>

