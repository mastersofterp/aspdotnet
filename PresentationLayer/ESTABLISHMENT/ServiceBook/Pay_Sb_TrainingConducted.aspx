<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Pay_Sb_TrainingConducted.aspx.cs" MasterPageFile="~/ServiceBookMaster.master" Inherits="PAYROLL_SERVICEBOOK_Pay_Sb_TrainingConducted" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="cc2" %>
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
                                                    <h5>Program/Training/STTP/Conference/Workshop Conducted</h5>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <asp:Panel ID="pnlAdd" runat="server">

                                        <div class="panel panel-info">
                                            <%--<div class="panel panel-heading">Program/Training/Short Term Course/Conference Conducted</div>--%>
                                            <div class="panel panel-body">

                                                <div class="col-12">
                                                    <div class="row">
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>Program Level : <span style="color: #FF0000">*</span> </label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlProgramLevel" runat="server" AppendDataBoundItems="true" data-select2-enable="true"
                                                                CssClass="form-control" ToolTip="Select Program Level" TabIndex="1">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                <asp:ListItem Value="1">National</asp:ListItem>
                                                                <asp:ListItem Value="2">International</asp:ListItem>
                                                                <asp:ListItem Value="3">Others</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvProgramLevel" runat="server" ControlToValidate="ddlProgramLevel"
                                                                Display="None" ErrorMessage="Please Select Program Level" ValidationGroup="ServiceBook"
                                                                SetFocusOnError="True" InitialValue="0">
                                                            </asp:RequiredFieldValidator>
                                                        </div>


                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>Program Type : <span style="color: #FF0000">*</span> </label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlProgramType" runat="server" AppendDataBoundItems="true" data-select2-enable="true"
                                                                CssClass="form-control" ToolTip="Select Program Type" TabIndex="2">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                <asp:ListItem Value="1">Conference</asp:ListItem>
                                                                <asp:ListItem Value="2">Workshop</asp:ListItem>
                                                                <asp:ListItem Value="3">FDP</asp:ListItem>
                                                                <asp:ListItem Value="4">Industrial Training</asp:ListItem>
                                                                <asp:ListItem Value="5"> Short Term Program</asp:ListItem>
                                                                <asp:ListItem Value="6">Seminar</asp:ListItem>
                                                                <asp:ListItem Value="7">Soft Skill Training</asp:ListItem>
                                                                <asp:ListItem Value="8">Guest Lecture</asp:ListItem>
                                                                <asp:ListItem Value="9">Internal</asp:ListItem>
                                                                <asp:ListItem Value="10">Hackathon</asp:ListItem>
                                                                <asp:ListItem Value="11">Professional Development Program</asp:ListItem>
                                                                <asp:ListItem Value="12">Administrative Training Program</asp:ListItem>
                                                                <asp:ListItem Value="13">MDP</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvProgramtype" runat="server" ControlToValidate="ddlProgramType"
                                                                Display="None" ErrorMessage="Please Select Program Type" ValidationGroup="ServiceBook"
                                                                SetFocusOnError="True" InitialValue="0">
                                                            </asp:RequiredFieldValidator>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <label>Mode :</label>
                                                            <asp:RadioButtonList ID="rdbMode" runat="server" AutoPostBack="true" TabIndex="3" CssClass="form-control"
                                                                RepeatDirection="Horizontal" ToolTip="Select Mode Type">
                                                                <asp:ListItem Enabled="true" Selected="True" Text="Online" Value="0"></asp:ListItem>
                                                                <asp:ListItem Enabled="true" Text="Offline" Value="1"></asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>Role :  </label>
                                                            </div>
                                                            <asp:TextBox ID="txtRole" runat="server" CssClass="form-control" MaxLength="100"
                                                                ToolTip="Enter Role" TabIndex="4"></asp:TextBox>
                                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtRole"
                                                        Display="None" ErrorMessage="Please Enter Presentation Details" ValidationGroup="ServiceBook"
                                                        SetFocusOnError="True">
                                                    </asp:RequiredFieldValidator>--%>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-12">
                                                    <div class="row">
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>Presentation Details : <span style="color: #FF0000"></span></label>
                                                            </div>
                                                            <asp:TextBox ID="txtPresentation" runat="server" CssClass="form-control" MaxLength="400"
                                                                ToolTip="Enter Presentation Details" TabIndex="5"></asp:TextBox>
                                                            <%-- <asp:RequiredFieldValidator ID="rfvPresentation" runat="server" ControlToValidate="txtPresentation"
                                                        Display="None" ErrorMessage="Please Enter Presentation Details" ValidationGroup="ServiceBook"
                                                        SetFocusOnError="True">
                                                    </asp:RequiredFieldValidator>--%>
                                                        </div>
                                                        <%-- </div>
                                                   </div>--%>
                                                        <%-- <div class="col-12">
                                            <div class="row">--%>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>Name of the Program/ Training : <span style="color: #FF0000">*</span> </label>
                                                            </div>
                                                            <asp:TextBox ID="txtCourse" runat="server" CssClass="form-control" ToolTip="Enter Name of the Program/ Training"
                                                                TabIndex="6" MaxLength="90"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvCourse" runat="server" ControlToValidate="txtCourse"
                                                                Display="None" ErrorMessage="Please Enter Name of the Program/ Training" ValidationGroup="ServiceBook"
                                                                SetFocusOnError="True">
                                                            </asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>Name of the Institute : <span style="color: #FF0000">*</span> </label>
                                                            </div>
                                                            <asp:TextBox ID="txtInstitute" runat="server" CssClass="form-control" ToolTip="Enter Name of the Institute"
                                                                TabIndex="7" MaxLength="90"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvInstitute" runat="server" ControlToValidate="txtInstitute"
                                                                Display="None" ErrorMessage="Please Enter  Name of the Institute" ValidationGroup="ServiceBook"
                                                                SetFocusOnError="True">
                                                            </asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>Venue :</label>
                                                            </div>
                                                            <asp:TextBox ID="txtlocation" runat="server" CssClass="form-control" ToolTip="Enter Venue" TabIndex="8" MaxLength="90"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-12">
                                                    <div class="row">
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>From Date :<span style="color: #FF0000">*</span> </label>
                                                            </div>
                                                            <div class="input-group date">
                                                                <div class="input-group-addon">
                                                                    <i id="i1" runat="server" class="fa fa-calendar text-blue"></i>
                                                                </div>
                                                                <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control" ToolTip="Enter From Date"
                                                                    TabIndex="9" Style="z-index: 0;" onBlur="CalDuration();" onChange="CalDuration();"></asp:TextBox>
                                                                <ajaxToolKit:CalendarExtender ID="ceFromDate" runat="server" Format="dd/MM/yyyy"
                                                                    TargetControlID="txtFromDate" PopupButtonID="i1" Enabled="true" EnableViewState="true"
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
                                                                    InvalidValueBlurredMessage="Invalid Date" ValidationGroup="ServiceBook" SetFocusOnError="True" />
                                                            </div>
                                                        </div>
                                                        <%--</div>
                                                     </div>
                                                  <div class="col-12">
                                            <div class="row">--%>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>To Date : <span style="color: #FF0000">*</span> </label>
                                                            </div>
                                                            <div class="input-group date">
                                                                <div class="input-group-addon">
                                                                    <i id="i2" runat="server" class="fa fa-calendar text-blue"></i>
                                                                </div>
                                                                <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control" ToolTip="Enter To Date"
                                                                    TabIndex="10" Style="z-index: 0;" onBlur="CalDuration();" onChange="CalDuration();"></asp:TextBox>
                                                                <ajaxToolKit:CalendarExtender ID="ceToDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtToDate"
                                                                    PopupButtonID="i2" Enabled="true" EnableViewState="true" PopupPosition="BottomLeft">
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
                                                                    InvalidValueBlurredMessage="Invalid Date" ValidationGroup="ServiceBook" SetFocusOnError="True" />
                                                                <%--<asp:CompareValidator ID="cvToDate" runat="server" Display="None" ErrorMessage="To Date Should be Greater than or equal to From Date"
                                                            ValidationGroup="ServiceBook" SetFocusOnError="True" ControlToCompare="txtFromDate"
                                                            ControlToValidate="txtToDate" Operator="GreaterThanEqual" Type="Date"></asp:CompareValidator>--%>
                                                            </div>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>Duration : </label>
                                                            </div>
                                                            <asp:TextBox ID="txtDuration" AutoCompleteType="None" runat="server" AutoComplete="off" CssClass="form-control"
                                                                ToolTip="Enter Duration" MaxLength="50" TabIndex="11"
                                                                onkeydown="return EditControl(event,this);" onkeypress="return EditControl(event,this);"
                                                                onclick="return EditControl(event,this);"></asp:TextBox>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>Sponsored Amount :</label>
                                                            </div>
                                                            <asp:TextBox ID="txtspoamount" runat="server" CssClass="form-control" onkeypress="return CheckNumeric(event,this);"
                                                                ToolTip="Enter Sponsored Amount" TabIndex="12" MaxLength="10"></asp:TextBox>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftbeamount" runat="server" TargetControlID="txtspoamount" FilterType="Numbers,Custom"
                                                                ValidChars=".">
                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-12">
                                                    <div class="row">
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>Cost involved : </label>
                                                            </div>
                                                            <asp:TextBox ID="txtCostInvolved" runat="server" CssClass="form-control" ToolTip="Enter Cost involved" MaxLength="10"
                                                                onkeypress="return CheckNumeric(event,this);" TabIndex="13"></asp:TextBox>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtCostInvolved" FilterType="Numbers,Custom"
                                                                ValidChars=".">
                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                        </div>
                                                        <%--  </div>
                                                      </div>
                                                 <div class="col-12">
                                            <div class="row">--%>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>Sponsored by :</label>
                                                            </div>
                                                            <asp:TextBox ID="txtsponsoredby" runat="server" CssClass="form-control" ToolTip="Enter Sponsored by"
                                                                AutoComplete="off" MaxLength="150" TabIndex="14"></asp:TextBox>
                                                        </div>


                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>Internal Faculty :</label>
                                                            </div>
                                                            <asp:TextBox ID="txtIntFaculty" runat="server" CssClass="form-control" TabIndex="15" MaxLength="100"
                                                                ToolTip="Please Enter Internal Faculty"></asp:TextBox>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>External Faculty :</label>
                                                            </div>
                                                            <asp:TextBox ID="txtExtFaculty" runat="server" CssClass="form-control" TabIndex="16" MaxLength="100"
                                                                ToolTip="Please Enter External Faculty"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-12">
                                                    <div class="row">
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>Internal Student :</label>
                                                            </div>
                                                            <asp:TextBox ID="txtIntStud" runat="server" CssClass="form-control" TabIndex="17" MaxLength="100"
                                                                ToolTip="Please Enter Internal Student"></asp:TextBox>
                                                        </div>
                                                        <%-- </div>
                                                </div>
                                            </div>
                                             <div class="col-12">
                                            <div class="row">--%>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>External Student :</label>
                                                            </div>
                                                            <asp:TextBox ID="txtExtStud" runat="server" CssClass="form-control" TabIndex="18" MaxLength="100"
                                                                ToolTip="Please Enter External Student"></asp:TextBox>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>Professional Body :</label>
                                                            </div>
                                                            <asp:TextBox ID="txtProfessionalbody" runat="server" CssClass="form-control" MaxLength="100"
                                                                ToolTip="Please Enter Professional Body" TabIndex="19"></asp:TextBox>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>Remarks If Any :</label>
                                                            </div>
                                                            <%--<span style="color: #FF0000">*</span>--%>
                                                            <asp:TextBox ID="txtReMarks" runat="server" CssClass="form-control" ToolTip="Enter Remarks If Any" TabIndex="20" MaxLength="125"
                                                                onkeyDown="checkTextAreaMaxLength(this,event,'125');" onkeyup="textCounter(this, this.form.remLen, 125);"
                                                                TextMode="MultiLine"></asp:TextBox>
                                                            <%-- <asp:RequiredFieldValidator ID="rfvReMarks" runat="server" ControlToValidate="txtReMarks"
                                                        Display="None" ErrorMessage="Please Enter Remarks If Any" ValidationGroup="ServiceBook"
                                                        SetFocusOnError="True">
                                                    </asp:RequiredFieldValidator>--%>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>Upload Files</label>
                                                    <label>Multiple Files Can Be Attached :</label>
                                                </div>
                                                <asp:FileUpload ID="FileUpload1" runat="server" TabIndex="21" ToolTip="Upload Multiple Files Here" />
                                                <asp:Label ID="Label2" runat="server" Text=" Please Select valid Document file(e.g. .pdf,.jpg) upto 5MB" ForeColor="Red"></asp:Label>
                                                <asp:Button ID="btnAdd" runat="server" Text="Add" TabIndex="22" class="btn btn-primary" OnClick="btnAdd_Click"
                                                    ToolTip="Click here to uplaod multiple files" />
                                            </div>
                                        </div>


                                        <div class="hidden">
                                            <%-- <label>Participant Type :</label>--%>
                                        </div>
                                        <asp:DropDownList ID="ddlnoofparti" runat="server" CssClass="form-control" AppendDataBoundItems="true" TabIndex="23"
                                            OnChange="ChangePanels2()" ToolTip="Select Participant Type" Visible="false">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <asp:ListItem Value="1">Internal</asp:ListItem>
                                            <asp:ListItem Value="2">External</asp:ListItem>
                                        </asp:DropDownList>

                                        <%--</div>--%>

                                        <div class="form-group col-md-4" hidden>
                                            <%--   <label>No.Of Participant :</label>--%>
                                            <asp:TextBox ID="txtnoofparti" runat="server" Enabled="false" CssClass="form-control" MaxLength="6"
                                                onkeypress="return CheckNumeric(event,this);" ToolTip="Enter No. Of Participant" TabIndex="24"></asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtnoofparti" FilterType="Numbers">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                        </div>

                                        <%--<div class="form-group col-md-4">
                                                    <label>Upload Document :</label>
                                                    <asp:FileUpload ID="flupld" runat="server" ToolTip="Click here to Upload Document" TabIndex="9" />
                                                </div>--%>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divBlob" runat="server" visible="false">
                                            <asp:Label ID="lblBlobConnectiontring" runat="server" Text=""></asp:Label>
                                            <asp:HiddenField ID="hdnBlobCon" runat="server" />
                                            <asp:Label ID="lblBlobContainer" runat="server" Text=""></asp:Label>
                                            <asp:HiddenField ID="hdnBlobContainer" runat="server" />
                                        </div>

                                        <%-- </asp:Panel>--%>
                                        <%--<asp:UpdatePanel ID="pnlnew" runat="server">
                    <ContentTemplate>
                        <div class="col-12">
                            <div class="row">
                                <p class="text-center">
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
                                                            <th>File Name</th>
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

                                                    <td>
                                                        <asp:HyperLink ID="lnkDownload" runat="server" Target="_blank" NavigateUrl='<%# GetFileNamePath(Eval("GETFILE"),Eval("FUID"),Eval("IDNO"),Eval("FOLDER"),Eval("APPID"))%>'><%# Eval("DisplayFileName")%></asp:HyperLink>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </asp:Panel>
                                </p>
                            </div>
                        </div>                        
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
                                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="ServiceBook" TabIndex="25"
                                                    OnClick="btnSubmit_Click" CssClass="btn btn-primary" ToolTip="Click here to Submit" />&nbsp;
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" TabIndex="26"
                                                OnClick="btnCancel_Click" CssClass="btn btn-warning" ToolTip="Click here to Reset" />
                                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="ServiceBook"
                                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                            </p>
                                        </div>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                </div>
                <div class="col-md-12">
                    <asp:Panel ID="pnlList" runat="server" ScrollBars="Auto">
                        <asp:ListView ID="lvTraning" runat="server">
                            <EmptyDataTemplate>
                                <br />
                                <p class="text-center text-bold">
                                    <asp:Label ID="lblErrMsg" runat="server" SkinID="Errorlbl" Text="No Rows In Training/Short Term Course/Conference Conducted"></asp:Label>
                                </p>
                            </EmptyDataTemplate>
                            <LayoutTemplate>
                                <div class="sub-heading">
                                    <h5>Program/Training/Short Term Course/Conference Conducted</h5>
                                </div>

                                <%--  <table class="table table-bordered table-hover">--%>
                                <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                    <thead>
                                        <tr class="bg-light-blue">
                                            <th>Action
                                            </th>
                                            <th>Program/Training
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
                                <%-- </div>--%>
                                                </div>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("tno")%>'
                                            AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />&nbsp;
                                        <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Images/delete.png" CommandArgument='<%# Eval("tno") %>'
                                            AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                            OnClientClick="showConfirmDel(this); return false;" />
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
                    <legend class="legendPay">Training/Short Term Course/Conference Conducted</legend>
                    <br />
                    <table cellpadding="0" cellspacing="0" style="width: 100%;">
                        <tr>
                            <td class="form_left_label">Name of the Course :
                            </td>
                            <td class="form_left_text">
                                <asp:TextBox ID="txtCourse" runat="server" Width="200px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvCourse" runat="server" ControlToValidate="txtCourse"
                                    Display="None" ErrorMessage="Please Enter  Name of the Course" ValidationGroup="ServiceBook"
                                    SetFocusOnError="True">
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="form_left_label">Name of the Institute :
                            </td>
                            <td class="form_left_text">
                                <asp:TextBox ID="txtInstitute" runat="server" Width="200px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvInstitute" runat="server" ControlToValidate="txtInstitute"
                                    Display="None" ErrorMessage="Please Enter  Name of the Institute" ValidationGroup="ServiceBook"
                                    SetFocusOnError="True">
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="form_left_label">From Date :
                            </td>
                            <td class="form_left_text">
                                <asp:TextBox ID="txtFromDate" runat="server" Width="80px"></asp:TextBox>
                                &nbsp;<asp:Image ID="imgCal" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
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
                                    ControlToValidate="txtFromDate" EmptyValueMessage="Please Enter From Date" InvalidValueMessage="From Date is Invalid (Enter mm/dd/yyyy Format)"
                                    Display="None" TooltipMessage="Please Enter From Date" EmptyValueBlurredText="Empty"
                                    InvalidValueBlurredMessage="Invalid Date" ValidationGroup="ServiceBook" SetFocusOnError="True" />
                            </td>
                        </tr>
                        <tr>
                            <td class="form_left_label">To Date :
                            </td>
                            <td class="form_left_text">
                                <asp:TextBox ID="txtToDate" runat="server" Width="80px"></asp:TextBox>
                                &nbsp;<asp:Image ID="Image1" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
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
                                    InvalidValueBlurredMessage="Invalid Date" ValidationGroup="ServiceBook" SetFocusOnError="True" />
                                <asp:CompareValidator ID="cvToDate" runat="server" Display="None" ErrorMessage="Traning To Date  Should be Greater than  or equal to From Date"
                                    ValidationGroup="ServiceBook" SetFocusOnError="True" ControlToCompare="txtFromDate"
                                    ControlToValidate="txtToDate" Operator="GreaterThanEqual" Type="Date"></asp:CompareValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="form_left_label">Remarks If Any :
                            </td>
                            <td class="form_left_text">
                                <asp:TextBox ID="txtReMarks" runat="server" Width="200px" TextMode="MultiLine"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvReMarks" runat="server" ControlToValidate="txtReMarks"
                                    Display="None" ErrorMessage="Please Enter ReMarks If Any" ValidationGroup="ServiceBook"
                                    SetFocusOnError="True">
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="form_left_label">Upload Document :
                            </td>
                            <td class="form_left_text">
                                <asp:FileUpload ID="flupld" runat="server" />
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
                                    <%--<asp:ListView ID="lvTraning" runat="server">
                            <EmptyDataTemplate>
                                <br />
                                <asp:Label ID="lblErrMsg" runat="server" SkinID="Errorlbl" Text="No Rows In Training/Short Term Course/Conference Conducted"></asp:Label>
                            </EmptyDataTemplate>
                            <LayoutTemplate>
                                <div class="vista-gridServiceBook">
                                    <div class="titlebar-ServiceBook">
                                        Training/Short Term Course/Conference Conducted
                                    </div>
                                    <table class="datatable-ServiceBook" cellpadding="0" cellspacing="0" style="width: 100%;">
                                        <thead>
                                            <tr class="header-ServiceBook">
                                                <th width="10%">Action
                                                </th>
                                                <th width="10%">Course
                                                </th>
                                                <th width="10%">Institute
                                                </th>
                                                <th width="10%">From Date
                                                </th>
                                                <th width="10%">To Date
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
                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("tno")%>'
                                            AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />&nbsp;
                                        <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.gif" CommandArgument='<%# Eval("tno") %>'
                                            AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                            OnClientClick="showConfirmDel(this); return false;" />
                                    </td>
                                    <td width="10%">
                                        <%# Eval("course")%>
                                    </td>
                                    <td width="10%">
                                        <%# Eval("inst")%>
                                    </td>
                                    <td width="10%">
                                        <%# Eval("fdt","{0:dd/MM/yyyy}")%>
                                    </td>
                                    <td width="10%">
                                        <%# Eval("tdt","{0:dd/MM/yyyy}")%>
                                    </td>
                                    <td width="15%" align="left">
                                        <asp:HyperLink ID="lnkDownload" runat="server" Target="_blank" NavigateUrl='<%# GetFileNamePath(Eval("ATTACHMENT"),Eval("TNO"),Eval("IDNO"))%>'><%# Eval("ATTACHMENT")%></asp:HyperLink>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <tr class="altitem-ServiceBook" onmouseover="this.style.backgroundColor='#FFFFAA'"
                                    onmouseout="this.style.backgroundColor='#FFFFD2'">
                                    <td width="10%">
                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("tno")%>'
                                            AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />&nbsp;
                                        <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.gif" CommandArgument='<%# Eval("tno") %>'
                                            AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                            OnClientClick="showConfirmDel(this); return false;" />
                                    </td>
                                    <td width="10%">
                                        <%# Eval("course")%>
                                    </td>
                                    <td width="10%">
                                        <%# Eval("inst")%>
                                    </td>
                                    <td width="10%">
                                        <%# Eval("fdt","{0:dd/MM/yyyy}")%>
                                    </td>
                                    <td width="10%">
                                        <%# Eval("tdt","{0:dd/MM/yyyy}")%>
                                    </td>
                                    <td width="15%" align="left">
                                        <asp:HyperLink ID="lnkDownload" runat="server" Target="_blank" NavigateUrl='<%# GetFileNamePath(Eval("ATTACHMENT"),Eval("TNO"),Eval("IDNO"))%>'><%# Eval("ATTACHMENT")%></asp:HyperLink>
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
        </ContentTemplate>
        <Triggers>
            <%--<asp:PostBackTrigger ControlID="btnUpload" />--%>

            <asp:PostBackTrigger ControlID="btnSubmit" />
            <asp:PostBackTrigger ControlID="btnAdd" />
        </Triggers>
    </asp:UpdatePanel>

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
    </script>
    <script type="text/javascript">

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



    <script>

        function Check(event, obj) {

            var k = (window.event) ? event.keyCode : event.which;

            if (k > 32 && k < 128) {
                // obj.style.backgroundColor = "LightGray";
                document.getElementById('<%=txtDuration.ClientID%>').disabled = true;
                return false;

            }


        }
        function EditControl(event, obj) {
            obj.style.backgroundColor = "LightGray";

            document.getElementById('<%=txtDuration.ClientID%>').contentEditable.replace = false;
            document.getElementById('<%=txtDuration.ClientID%>').contentEditable = false;
            alert("Not Editable");
            return false;
        }

    </script>

    <script type="text/javascript">
        function ChangePanels2() {
            //debugger;
            var spl = document.getElementById('<%=ddlnoofparti.ClientID %>');
            var valspl = spl.options[spl.selectedIndex].text;
            if (valspl != "Internal" && valspl != "External") {
                document.getElementById('<%=txtnoofparti.ClientID %>').disabled = true;
                //document.getElementById('<%=txtnoofparti.ClientID %>').style.visibility = 'hidden';
                document.getElementById('<%=txtnoofparti.ClientID %>').value = "";
            }
            else {


                document.getElementById('<%=txtnoofparti.ClientID %>').disabled = false;
                //  document.getElementById('<%=txtnoofparti.ClientID %>').disabled = false;


            }
        }
        Loadnew()
        {
            var opt = document.createElement("option");

            opt.text = "New Value";
            opt.value = "100";
            document.getElementById('<%=ddlnoofparti.ClientID%>').options.add(opt);

        }
        function onfocusnext() {

            document.getElementById('<%=ddlnoofparti.ClientID %>').focus();

        }</script>


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
