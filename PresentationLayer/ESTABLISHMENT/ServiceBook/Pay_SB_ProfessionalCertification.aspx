<%@ Page Language="C#" MasterPageFile="~/ServiceBookMaster.master" AutoEventWireup="true" CodeFile="Pay_SB_ProfessionalCertification.aspx.cs"
    Inherits="ESTABLISHMENT_ServiceBook_Pay_SB_ProfessionalCertification" %>

<asp:Content ID="Content1" ContentPlaceHolderID="sbhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="sbctp" runat="Server">

    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
    <link href="../Css/master.css" rel="stylesheet" type="text/css" />
    <link href="../Css/Theme1.css" rel="stylesheet" type="text/css" />

    <%-- <asp:UpdatePanel ID="updImage" runat="server">
        <ContentTemplate>--%>
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">

                <div class="box-body">
                    <asp:Panel ID="pnlProf" runat="server">
                        <div class="col-12">
                            <div class="row">
                                <div class="col-12">
                                    <div class="sub-heading">
                                        <h5>Training/Short Term Training Program/Conference Attended/Workshop</h5>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <asp:UpdatePanel ID="updImage" runat="server">
                            <ContentTemplate>
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <label>Program Level : <span style="color: #FF0000">*</span> </label>
                                            <asp:DropDownList ID="ddlProgramLevel" runat="server" AppendDataBoundItems="true"
                                                CssClass="form-control" ToolTip="Select Program Level" TabIndex="1" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                <asp:ListItem Value="1">National</asp:ListItem>
                                                <asp:ListItem Value="2">International</asp:ListItem>
                                                <asp:ListItem Value="3">Online</asp:ListItem>
                                                <asp:ListItem Value="4">Others</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvProgramLevel" runat="server" ControlToValidate="ddlProgramLevel"
                                                Display="None" ErrorMessage="Please Select Program Level" ValidationGroup="ServiceBook"
                                                SetFocusOnError="True" InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <label>Program Type : <span style="color: #FF0000">*</span> </label>
                                            <asp:DropDownList ID="ddlProgramType" runat="server" AppendDataBoundItems="true"
                                                CssClass="form-control" ToolTip="Select Program Type" TabIndex="2" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                <asp:ListItem Value="1">Conference</asp:ListItem>
                                                <asp:ListItem Value="2">Workshop</asp:ListItem>
                                                <asp:ListItem Value="3">FDP</asp:ListItem>
                                                <asp:ListItem Value="4">Industrial Training</asp:ListItem>
                                                <asp:ListItem Value="5">Short Term Program</asp:ListItem>
                                                <asp:ListItem Value="6">NPTEL</asp:ListItem>
                                                <asp:ListItem Value="7">Orientation Program</asp:ListItem>
                                                <asp:ListItem Value="8">Refresher Course</asp:ListItem>
                                                <asp:ListItem Value="9">Certification Course</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvProgramtype" runat="server" ControlToValidate="ddlProgramType"
                                                Display="None" ErrorMessage="Please Select Program Type" ValidationGroup="ServiceBook"
                                                SetFocusOnError="True" InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <label>Participation Type :</label>
                                            <asp:DropDownList ID="ddlPartitionType" runat="server" data-select2-enable="true" AppendDataBoundItems="true"
                                                CssClass="form-control" ToolTip="Select Partition Type" TabIndex="3">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                <asp:ListItem Value="1">Attended</asp:ListItem>
                                                <asp:ListItem Value="2">Presented</asp:ListItem>
                                                <asp:ListItem Value="3">Chair Person</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Role :</label>
                                            </div>
                                            <asp:TextBox ID="txtRole" runat="server" CssClass="form-control" MaxLength="100"
                                                ToolTip="Enter Role Details" TabIndex="3" onkeypress="return CheckAlphabet(event,this);"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <label>Academic Year : </label>
                                            <asp:TextBox ID="txtAcadYear" runat="server" CssClass="form-control" MaxLength="50" TabIndex="20"
                                                ToolTip="Enter Academic Year" autocomplete="off"></asp:TextBox>
                                            <%--<ajaxToolKit:FilteredTextBoxExtender ID="ftbAcadYear" runat="server" TargetControlID="txtAcadYear"
                                                ValidChars="" Enabled="True">
                                            </ajaxToolKit:FilteredTextBoxExtender>--%>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <label>Mode :</label>
                                            <asp:RadioButtonList ID="rdbMode" runat="server" AutoPostBack="true" TabIndex="4" CssClass="form-control"
                                                RepeatDirection="Horizontal" ToolTip="Select Mode Type">
                                                <asp:ListItem Enabled="true" Selected="True" Text="Online" Value="0"></asp:ListItem>
                                                <asp:ListItem Enabled="true" Text="Offline" Value="1"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <label>Presentation Details : <span style="color: #FF0000"></span></label>
                                            <asp:TextBox ID="txtPresentation" runat="server" CssClass="form-control" MaxLength="300"
                                                ToolTip="Enter Presentation Details" TabIndex="5" onkeypress="return CheckAlphabet(event,this);"></asp:TextBox>
                                            <%-- <asp:RequiredFieldValidator ID="rfvPresentation" runat="server" ControlToValidate="txtPresentation"
                                                        Display="None" ErrorMessage="Please Enter Presentation Details" ValidationGroup="ServiceBook"
                                                        SetFocusOnError="True">
                                                    </asp:RequiredFieldValidator>--%>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Name of the Program/ Training :</label>
                                            </div>
                                            <asp:TextBox ID="txtCourse" runat="server" CssClass="form-control"
                                                ToolTip="Enter Name of the Course" TabIndex="6" MaxLength="100" onBlur="CalDuration();" onChange="CalDuration();" onkeypress="return CheckAlphabet(event,this);"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvCourse" runat="server" ControlToValidate="txtCourse"
                                                Display="None" ErrorMessage="Please Enter  Name of the Course" ValidationGroup="ServiceBook"
                                                SetFocusOnError="True">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Name of the Institute</label>
                                            </div>
                                            <asp:TextBox ID="txtInstitute" runat="server" CssClass="form-control"
                                                ToolTip="Enter Name of the Institute" TabIndex="7" MaxLength="100" onkeypress="return CheckAlphabet(event,this);"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvInstitute" runat="server" ControlToValidate="txtInstitute"
                                                Display="None" ErrorMessage="Please Enter  Name of the Institute" ValidationGroup="ServiceBook"
                                                SetFocusOnError="True">
                                            </asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <label>Venue :<span style="color: #FF0000">*</span> </label>
                                            <asp:TextBox ID="txtVenue" runat="server" CssClass="form-control" MaxLength="100"
                                                ToolTip="Enter Venue" TabIndex="8"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvInstituteadd" runat="server" ControlToValidate="txtVenue"
                                                Display="None" ErrorMessage="Please Enter Venue" ValidationGroup="ServiceBook"
                                                SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        </div>


                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>From Date</label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i id="imgCal" runat="server" class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control"
                                                    ToolTip="Enter From Date" TabIndex="9" Style="z-index: 0;" onBlur="CalDuration();" onChange="CalDuration();"></asp:TextBox>
                                                <ajaxToolKit:CalendarExtender ID="ceFromDate" runat="server" Format="dd/MM/yyyy"
                                                    TargetControlID="txtFromDate" PopupButtonID="imgCal" Enabled="true" EnableViewState="true"
                                                    PopupPosition="BottomLeft">
                                                </ajaxToolKit:CalendarExtender>
                                                <asp:RequiredFieldValidator ID="rfvFromDate" runat="server" ControlToValidate="txtFromDate"
                                                    Display="None" ErrorMessage="Please Select From Date"
                                                    ValidationGroup="ServiceBook" SetFocusOnError="True">
                                                </asp:RequiredFieldValidator>
                                                <ajaxToolKit:MaskedEditExtender ID="meFromDate" runat="server" TargetControlID="txtFromDate"
                                                    Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                    AcceptNegative="Left" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate">
                                                </ajaxToolKit:MaskedEditExtender>
                                                <ajaxToolKit:MaskedEditValidator ID="mevFromDate" runat="server" ControlExtender="meFromDate"
                                                    ControlToValidate="txtFromDate" InvalidValueMessage="From Date is Invalid (Enter -dd/MM/yyyy Format)"
                                                    Display="None" TooltipMessage="Please Enter From Date" EmptyValueBlurredText="Empty"
                                                    InvalidValueBlurredMessage="Invalid Date" ValidationGroup="ServiceBook" SetFocusOnError="True" IsValidEmpty="false" InitialValue="__/__/____" />
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>To Date</label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i id="Image1" runat="server" class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control"
                                                    ToolTip="Enter TO Date" TabIndex="10" Style="z-index: 0;" onBlur="CalDuration();" onChange="CalDuration();"></asp:TextBox>
                                                <ajaxToolKit:CalendarExtender ID="ceToDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtToDate"
                                                    PopupButtonID="Image1" Enabled="true" EnableViewState="true" PopupPosition="BottomLeft">
                                                </ajaxToolKit:CalendarExtender>
                                                <asp:RequiredFieldValidator ID="rfvToDate" runat="server" ControlToValidate="txtToDate"
                                                    Display="None" ErrorMessage="Please Select To Date" ValidationGroup="ServiceBook"
                                                    SetFocusOnError="True">
                                                </asp:RequiredFieldValidator>
                                                <ajaxToolKit:MaskedEditExtender ID="meToDate" runat="server" TargetControlID="txtToDate"
                                                    Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                    AcceptNegative="Left" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate">
                                                </ajaxToolKit:MaskedEditExtender>
                                                <ajaxToolKit:MaskedEditValidator ID="mevToDate" runat="server" ControlExtender="meToDate"
                                                    ControlToValidate="txtToDate" InvalidValueMessage="To Date is Invalid (Enter -dd/MM/yyyy Format)"
                                                    Display="None" TooltipMessage="Please Enter To Date" EmptyValueBlurredText="Empty"
                                                    InvalidValueBlurredMessage="Invalid Date" ValidationGroup="ServiceBook" SetFocusOnError="True" IsValidEmpty="false" InitialValue="__/__/____" /><%--EmptyValueMessage="Please Enter To Date"--%>
                                                <%-- <asp:CompareValidator ID="cvToDate" runat="server" Display="None" ErrorMessage="Traning To Date  Should be Greater than  or equal to From Date"
                                                            ValidationGroup="ServiceBook" SetFocusOnError="True" ControlToCompare="txtFromDate"
                                                            ControlToValidate="txtToDate" Operator="GreaterThanEqual" Type="Date"></asp:CompareValidator>--%>
                                            </div>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <label>Duration : </label>
                                            <asp:TextBox ID="txtDuration" runat="server" CssClass="form-control" ToolTip="Enter Duration" AutoCompleteType="None" AutoComplete="off" MaxLength="150"
                                                onkeydown="return EditControl(event,this);" onkeypress="return EditControl(event,this);"
                                                onclick="return EditControl(event,this);" TabIndex="11"></asp:TextBox>
                                        </div>



                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <label>Sponsored Amount : </label>
                                            <asp:TextBox ID="txtSponsoredAmt" runat="server" CssClass="form-control" MaxLength="10" TabIndex="12"
                                                ToolTip="Enter Sponsored Amount" autocomplete="off"
                                                onkeypress="return CheckNumeric(event,this);"></asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftbeAmount" runat="server" TargetControlID="txtsponsoredamt"
                                                ValidChars="0123456789." Enabled="True">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <label>Cost involved : </label>
                                            <asp:TextBox ID="txtCostInvolved" runat="server" CssClass="form-control" ToolTip="Enter Cost involved" TabIndex="13"
                                                MaxLength="10" onkeypress="return CheckNumeric(event,this);"></asp:TextBox>

                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtCostInvolved"
                                                ValidChars="0123456789." Enabled="True">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <label>Sponsored by : </label>
                                            <asp:TextBox ID="txtSponsoredBy" runat="server" CssClass="form-control" ToolTip="Enter Sponsored by"
                                                TabIndex="14" MaxLength="50" onkeypress="return CheckAlphabet(event,this);"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <label>Internal Faculty :</label>

                                            <asp:TextBox ID="txtInfac" runat="server" CssClass="form-control" ToolTip="Enter Internal Faculty Name"
                                                TabIndex="15" MaxLength="100" onkeypress="return CheckAlphabet(event,this);"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <label>External Faculty :</label>

                                            <asp:TextBox ID="txtExtFac" runat="server" CssClass="form-control" ToolTip="Enter External Faculty Name"
                                                TabIndex="16" MaxLength="100" onkeypress="return CheckAlphabet(event,this);"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <label>Internal Student : </label>

                                            <asp:TextBox ID="txtInStud" runat="server" CssClass="form-control" ToolTip="Enter Internal Student Name"
                                                TabIndex="17" MaxLength="100" onkeypress="return CheckAlphabet(event,this);"></asp:TextBox>

                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <label>External Student :</label>
                                            <asp:TextBox ID="txtExStud" runat="server" CssClass="form-control" ToolTip="Enter External Student Name"
                                                TabIndex="18" MaxLength="100" onkeypress="return CheckAlphabet(event,this);"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <label>Professional Body :</label>
                                            <asp:TextBox ID="txtProf" runat="server" CssClass="form-control" ToolTip="Enter Professional Body"
                                                TabIndex="19" MaxLength="100" onkeypress="return CheckAlphabet(event,this);"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Remarks If Any</label>
                                            </div>
                                            <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control" ToolTip="Enter Remarks If Any"
                                                TabIndex="20" TextMode="MultiLine" MaxLength="120" onkeypress="return CheckAlphabet(event,this);"></asp:TextBox>
                                            <%--<asp:RequiredFieldValidator ID="rfvReMarks" runat="server" ControlToValidate="txtReMarks"
                                                        Display="None" ErrorMessage="Please Enter ReMarks If Any" ValidationGroup="ServiceBook"
                                                        SetFocusOnError="True">
                                                    </asp:RequiredFieldValidator>--%>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <label>Theme Of Training Attended : </label>
                                            <asp:TextBox ID="txtThemeOfTrainingAttended" runat="server" CssClass="form-control" TabIndex="25"
                                                ToolTip="Enter Theme Of Training Attended"></asp:TextBox>
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
                                                <asp:FileUpload ID="FileUpload1" runat="server" TabIndex="21" ToolTip="Upload Multiple Files Here" />
                                                <asp:Label ID="Label2" runat="server" Text=" Please Select valid Document file(e.g. .pdf,.jpg) upto 5MB" ForeColor="Red"></asp:Label>
                                                <asp:Button ID="btnAdd" runat="server" Text="Add" TabIndex="22" class="btn btn-primary"
                                                    ToolTip="Click here to uplaod multiple files" OnClick="btnAdd_Click" />

                                            </div>
                                        </div>
                                    </div>
                                    <%-- </div>
                        
                    </div>--%>


                                    <div class="col-md-12">
                                        <asp:UpdatePanel ID="pnlnew" runat="server">
                                            <ContentTemplate>
                                                <asp:Panel ID="Panel2" runat="server">
                                                    <div class="panel panel-info">
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

                                                                            <%--                                                                        <td>
                                                                            <asp:ImageButton ID="imgbtndelFile" runat="server" ImageUrl="~/Images/delete.png"
                                                                                CommandArgument=' <%#Eval("GETFILE") %>' AlternateText=' <%#Eval("APPID") %>' ToolTip="Delete Record"
                                                                                OnClientClick="javascript:return confirm('Are you sure you want to delete this file?')" OnClick="imgbtndelFile_Click" />

                                                                        </td>

                                                                        <td>

                                                                            <asp:HyperLink ID="lnkDownload" runat="server" Target="_blank" NavigateUrl='<%# GetFileNamePath(Eval("GETFILE"),Eval("FUID"),Eval("IDNO"),Eval("FOLDER"),Eval("APPID"))%>'><%# Eval("DisplayFileName")%></asp:HyperLink>
                                                                        </td>--%>
                                                                            <td>
                                                                                <asp:ImageButton ID="imgbtndelFile" runat="server" ImageUrl="~/Images/delete.png"
                                                                                    CommandArgument=' <%#Eval("GETFILE") %>' AlternateText=' <%#Eval("APPID") %>' ToolTip="Delete Record"
                                                                                    OnClientClick="javascript:return confirm('Are you sure you want to delete this file?')" OnClick="imgbtndelFile_Click" />
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
                                                </asp:Panel>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="btnAdd" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="ServiceBook" TabIndex="23"
                                        CssClass="btn btn-primary" ToolTip="Click here to Submit" OnClick="btnSubmit_Click" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" TabIndex="24"
                                        CssClass="btn btn-warning" ToolTip="Click here to Reset" OnClick="btnCancel_Click" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="ServiceBook"
                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <%--<asp:PostBackTrigger ControlID="btnUpload" />--%>
                                <asp:PostBackTrigger ControlID="btnCancel" />
                                <asp:PostBackTrigger ControlID="btnSubmit" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </asp:Panel>
                    <div class="col-12">
                        <asp:Panel ID="pnlList" runat="server">
                            <asp:ListView ID="lvProfessional" runat="server">
                                <EmptyDataTemplate>
                                    <br />
                                    <p class="text-center text-bold">
                                        <asp:Label ID="lblErrMsg" runat="server" SkinID="Errorlbl" Text="No Rows In Training/Short Term Training Program/Conference Attended/Workshop"></asp:Label>

                                    </p>
                                </EmptyDataTemplate>
                                <LayoutTemplate>
                                    <div class="sub-heading">
                                        <h5>Training/STTP/conference Attended/Workshop</h5>
                                    </div>
                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                        <thead class="bg-light-blue">
                                            <tr>
                                                <th>Action
                                                </th>
                                                <th>Course
                                                </th>
                                                <th>Institute
                                                </th>
                                                <th>From Date
                                                </th>
                                                <th>To Date
                                                </th>
                                                <%-- <th>Attachment
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
                                            <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("pno")%>'
                                                AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />
                                            <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Images/delete.png" CommandArgument='<%# Eval("pno") %>'
                                                AlternateText="Delete Record" ToolTip="Delete Record"
                                                OnClientClick="showConfirmDel(this); return false;" OnClick="btnDelete_Click" />
                                        </td>
                                        <td>
                                            <%# Eval("course")%>
                                        </td>
                                        <td>
                                            <%# Eval("inst")%>
                                        </td>
                                        <td>
                                            <%# Eval("fdt","{0:dd/MM/yyyy}")%>
                                        </td>
                                        <td>
                                            <%# Eval("tdt","{0:dd/MM/yyyy}")%>
                                        </td>
                                        <%-- <td>
                                                <asp:HyperLink ID="lnkDownload" runat="server" Target="_blank" NavigateUrl='<%# GetFileNamePath(Eval("ATTACHMENT"),Eval("TNO"),Eval("IDNO"))%>'><%# Eval("ATTACHMENT")%></asp:HyperLink>
                                            </td>--%>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
    </div>


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
        onkeypress = "return CheckAlphabet(event,this);"
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


        function EditControl(event, obj) {
            obj.style.backgroundColor = "LightGray";

            document.getElementById('<%=txtDuration.ClientID%>').contentEditable.replace = false;
            document.getElementById('<%=txtDuration.ClientID%>').contentEditable = false;
            alert("Not Editable");
            return false;
        }

        function CalDuration() {

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




    </script>


</asp:Content>



