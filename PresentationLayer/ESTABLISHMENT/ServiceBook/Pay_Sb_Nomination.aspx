<%@ Page Title="" Language="C#" MasterPageFile="~/ServiceBookMaster.master" AutoEventWireup="true" CodeFile="Pay_Sb_Nomination.aspx.cs" Inherits="ESTABLISHMENT_ServiceBook_Pay_Sb_Nomination" %>

<asp:Content ID="Content1" ContentPlaceHolderID="sbhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="sbctp" runat="Server">

    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
    <%-- <link href="../Css/master.css" rel="stylesheet" type="text/css" />
    <link href="../Css/Theme1.css" rel="stylesheet" type="text/css" />--%>

    <asp:UpdatePanel ID="updImage" runat="server">
        <ContentTemplate>

            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">

                        <div class="box-body">
                            <asp:Panel ID="pnlAdd" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="sub-heading">
                                                <h5>Nomination</h5>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Nomination For</label>
                                            </div>
                                            <asp:DropDownList ID="ddlNominationFor" AppendDataBoundItems="true" runat="server" data-select2-enable="true"
                                                CssClass="form-control" ToolTip="Select Nomination For" TabIndex="1">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfNominationFor" runat="server" ControlToValidate="ddlNominationFor"
                                                Display="None" ErrorMessage="Please Select Nomination For" ValidationGroup="ServiceBook"
                                                InitialValue="0"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Name Of Nominee</label>
                                            </div>
                                            <asp:TextBox ID="txtNameOfNominee" runat="server" CssClass="form-control" TabIndex="2"
                                                ToolTip="Enter Name Of Nominee" onkeyup="validateAlphabet(this);" MaxLength="60"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvNameOfNominee" runat="server" ControlToValidate="txtNameOfNominee"
                                                Display="None" ErrorMessage="Please Enter Name Of Nominee" ValidationGroup="ServiceBook"
                                                SetFocusOnError="True">
                                            </asp:RequiredFieldValidator>
                                            <asp:Label ID="lblname" runat="server" Text="Please Enter Name Of Nominee as Lastname Firstname Middlename"></asp:Label>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Relation</label>
                                            </div>
                                            <%-- <asp:DropDownList ID="ddlrelationship" AppendDataBoundItems="true" runat="server" AutoPostBack="true" TabIndex="1" data-select2-enable="true"
                                                CssClass="form-control" ToolTip="Select Relation">
                                                <asp:ListItem Value="0" Selected="True">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvRelationShip" runat="server" ControlToValidate="ddlrelationship" InitialValue="0"
                                                Display="None" ErrorMessage="Please Select Relation" ValidationGroup="ServiceBook"
                                                SetFocusOnError="True">
                                            </asp:RequiredFieldValidator>--%>
                                            <asp:TextBox ID="txtRelation" runat="server" CssClass="form-control" ToolTip="Enter Relation"
                                                onkeyup="validateAlphabet(this);" TabIndex="3" MaxLength="25"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rvfRelation" runat="server" ControlToValidate="txtRelation"
                                                Display="None" ErrorMessage="Please Enter Relation " ValidationGroup="ServiceBook"
                                                SetFocusOnError="True">
                                            </asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <%--<sup>* </sup>--%>
                                                <label>Percentage</label>
                                            </div>
                                            <asp:TextBox ID="txtPercentage" runat="server" CssClass="form-control" ToolTip="Enter Percentage" AutoPostBack="true"
                                                MaxLength="5" onkeyup="validateNumeric(this);" TabIndex="4" OnTextChanged="txtPercentage_TextChanged"></asp:TextBox>
                                            <%--<ajaxToolKit:MaskedEditExtender ID="mePercent" runat="server" TargetControlID="txtPercentage"
                                                Mask="99.99" MaskType="Number" InputDirection="LeftToRight" MessageValidatorTip="true" ErrorTooltipEnabled="true">
                                            </ajaxToolKit:MaskedEditExtender>--%>
                                            <%-- <asp:RequiredFieldValidator ID="rfvPercentage" runat="server" ControlToValidate="txtPercentage"
                                                Display="None" ErrorMessage="Please Enter Percentage" ValidationGroup="ServiceBook"
                                                SetFocusOnError="True">
                                            </asp:RequiredFieldValidator>--%>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Date of Birth(Nominee)</label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i id="imgDateOfBirth" runat="server" class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtDateOfBirth" runat="server" CssClass="form-control" ToolTip="Enter Date of Birth"
                                                    OnTextChanged="txtDateOfBirth_TextChanged" AutoPostBack="true" TabIndex="5" Style="z-index: 0;"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvAge" runat="server" ControlToValidate="txtDateOfBirth"
                                                    Display="None" ErrorMessage="Please Select Date of Birth " ValidationGroup="ServiceBook"
                                                    SetFocusOnError="True">
                                                </asp:RequiredFieldValidator>
                                                <ajaxToolKit:CalendarExtender ID="ceDateOfBirth" runat="server" Format="dd/MM/yyyy"
                                                    TargetControlID="txtDateOfBirth" PopupButtonID="imgDateOfBirth" Enabled="true"
                                                    EnableViewState="true" PopupPosition="BottomLeft">
                                                </ajaxToolKit:CalendarExtender>
                                                <ajaxToolKit:MaskedEditExtender ID="meDateOfBirth" runat="server" TargetControlID="txtDateOfBirth"
                                                    Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                    AcceptNegative="Left" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate">
                                                </ajaxToolKit:MaskedEditExtender>

                                                <ajaxToolKit:MaskedEditValidator ID="mevdobDate" runat="server" ControlExtender="meDateOfBirth"
                                                    ControlToValidate="txtDateOfBirth" InvalidValueMessage="Date of Birth is Invalid (Enter -dd/mm/yyyy Format)"
                                                    Display="None" TooltipMessage="Please Enter From Date" EmptyValueBlurredText="Empty"
                                                    InvalidValueBlurredMessage="Invalid Date" ValidationGroup="ServiceBook" SetFocusOnError="True" IsValidEmpty="false" InitialValue="__/__/____" />


                                                <%--<ajaxToolKit:MaskedEditValidator ID="mevDateOfBirth" runat="server" ControlExtender="meDateOfBirth"
                                                            ControlToValidate="txtDateOfBirth" EmptyValueMessage="Please Enter DateOfBirth"
                                                            InvalidValueMessage="Date Of Birth is Invalid (Enter -dd/mm/yyyy Format)" Display="None"
                                                            TooltipMessage="Please Enter DateOfBirth" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                                            ValidationGroup="ServiceBook" SetFocusOnError="True" />--%>
                                            </div>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Age</label>
                                            </div>
                                            <asp:TextBox ID="txtAge" runat="server" CssClass="form-control" ToolTip="Total Age" MaxLength="3"
                                                onkeyup="validateNumeric(this);" TabIndex="6" Enabled="false"></asp:TextBox>
                                            <%--  <asp:RequiredFieldValidator ID="rfvAge" runat="server" ControlToValidate="txtAge"
                                                        Display="None" ErrorMessage="Please Enter Age " ValidationGroup="ServiceBook"
                                                        SetFocusOnError="True">
                                                    </asp:RequiredFieldValidator>--%>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Contingencies Of The Happening</label>
                                            </div>
                                            <asp:TextBox ID="txtContingencies" runat="server" CssClass="form-control" TabIndex="7"
                                                ToolTip="Enter Contingencies Of The Happening" MaxLength="70"></asp:TextBox>
                                        </div>


                                        <%--    <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Upload Document</label>
                                            </div>
                                            <asp:FileUpload ID="flupld" runat="server" TabIndex="8" ToolTip="Upload Document" />
                                            <span style="color: red">Upload Document Maximum Size 10 Mb</span>
                                        </div>--%>

                                        <div class="form-group col-lg-3 ">
                                            <label>Select Address :</label>
                                            <br />
                                            <div class="form-group col-lg-12">
                                                <%-- <asp:RadioButton ID="rdbNewAddress" runat="server"  Text="New" Checked="false" TabIndex="11" AutoPostBack="true" OnCheckedChanged="rdbNewAddress_CheckedChanged"/>&nbsp;
                                                    <asp:RadioButton ID="rdbAsPermanentAdd" runat="server"  Text="Same As Permanent" Checked="false" TabIndex="11" AutoPostBack="true" OnCheckedChanged="rdbAsPermanentAdd_CheckedChanged"/>&nbsp;
                                                    <asp:RadioButton ID="rdbAsLocalAdd" runat="server"  Text="Same As Local" Checked="false" TabIndex="11" AutoPostBack="true" OnCheckedChanged="rdbAsLocalAdd_CheckedChanged"/>&nbsp;               
                                                --%>
                                                <asp:RadioButton ID="rdbNewAddress" runat="server" Text="New" Checked="false" AutoPostBack="true" OnCheckedChanged="rdbNewAddress_CheckedChanged" />
                                                <asp:RadioButton ID="rdbAsPermanentAdd" runat="server" Text="Same As Permanent" Checked="false" AutoPostBack="true" OnCheckedChanged="rdbAsPermanentAdd_CheckedChanged" />
                                                <asp:RadioButton ID="rdbAsLocalAdd" runat="server" Text="Same As Local" Checked="false" AutoPostBack="true" OnCheckedChanged="rdbAsLocalAdd_CheckedChanged" />

                                            </div>
                                        </div>


                                        <%-- <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Address</label>
                                            </div>
                                            <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" TabIndex="12"
                                                ToolTip="Enter Address" TextMode="MultiLine" MaxLength="130"></asp:TextBox>
                                        </div>--%>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Country :</label>
                                                <asp:TextBox ID="txtCountry" runat="server" CssClass="form-control" TabIndex="9" MaxLength="80"
                                                    ToolTip="Enter Country Name" onkeyup="validateAlphabet(this);"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>State :</label>
                                                <asp:TextBox ID="txtState" runat="server" CssClass="form-control" TabIndex="10" MaxLength="80"
                                                    ToolTip="Enter State Name" onkeyup="validateAlphabet(this);"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>City/Town :</label>
                                                <asp:TextBox ID="txtCity" runat="server" CssClass="form-control" TabIndex="11" MaxLength="100"
                                                    ToolTip="Enter City Name" onkeyup="validateAlphabet(this);"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div id="taluka" class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Taluka :</label>
                                                <asp:TextBox ID="txtTaluka" runat="server" CssClass="form-control" TabIndex="12" MaxLength="80"
                                                    ToolTip="Enter Taluka Name" onkeyup="validateAlphabet(this);"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>District :</label>
                                                <asp:TextBox ID="txtDistrict" runat="server" CssClass="form-control" TabIndex="13" MaxLength="80"
                                                    ToolTip="Enter District Name" onkeyup="validateAlphabet(this);"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Address :</label>
                                            </div>
                                            <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" TabIndex="14" MaxLength="200"
                                                ToolTip="Enter Address" TextMode="MultiLine"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Pincode :</label>
                                                <asp:TextBox ID="txtPincode" runat="server" CssClass="form-control" TabIndex="15" MaxLength="6"
                                                    ToolTip="Enter Pincode" onkeyup="validateNumeric(this);"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Remarks</label>
                                            </div>
                                            <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control" TabIndex="16"
                                                ToolTip="Enter Remarks" TextMode="MultiLine" MaxLength="120"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="trchk" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <label>Last Nominee</label>
                                            </div>
                                            <asp:CheckBox ID="chkLastNominee" runat="server" TabIndex="17" ToolTip="Check if Last Nominee" />
                                        </div>

                                        <%-- <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Upload Document</label>
                                            </div>
                                            <asp:FileUpload ID="flupld" runat="server" TabIndex="18" ToolTip="Upload Document" />
                                            <span style="color: red">Upload Document Maximum Size 10 Mb (eg.Upload Aadhar Card,Pan Card,Bank Passbook,Passport size photo)</span>
                                        </div>--%>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divBlob" runat="server" visible="false">
                                            <asp:Label ID="lblBlobConnectiontring" runat="server" Text=""></asp:Label>
                                            <asp:HiddenField ID="hdnBlobCon" runat="server" />
                                            <asp:Label ID="lblBlobContainer" runat="server" Text=""></asp:Label>
                                            <asp:HiddenField ID="hdnBlobContainer" runat="server" />
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Upload Files</label>
                                            <label>(eg.Aadhar Card,Pan Card,Bank Passbook,Passport size photo) :</label>
                                        </div>
                                        <asp:FileUpload ID="flupld" runat="server" TabIndex="21" ToolTip="Upload Multiple Files Here" />
                                        <asp:Label ID="Label2" runat="server" Text=" Please Select valid Document file(e.g. .pdf,.jpg) upto 5MB" ForeColor="Red"></asp:Label>
                                        <asp:Button ID="btnAdd" runat="server" Text="Add" TabIndex="22" class="btn btn-primary" OnClick="btnAdd_Click"
                                            ToolTip="Click here to uplaod multiple files" />
                                    </div>
                                </div>
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
                                                             <asp:HyperLink ID="lnkDownload" runat="server" Target="_blank" NavigateUrl='<%# GetFileNamePathForMultiple(Eval("GETFILE"),Eval("FUID"),Eval("IDNO"),Eval("FOLDER"),Eval("APPID"))%>'><%# Eval("DisplayFileName")%></asp:HyperLink>
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
                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="ServiceBook" TabIndex="18"
                                        OnClick="btnSubmit_Click" CssClass="btn btn-primary" ToolTip="Click here to Submit" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" TabIndex="19"
                                        OnClick="btnCancel_Click" CssClass="btn btn-warning" ToolTip="Click here to Reset" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="ServiceBook"
                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                </div>
                            </asp:Panel>
                            <div class="col-12">
                                <asp:Panel ID="pnlNomination" runat="server">
                                    <asp:ListView ID="lvNomination" runat="server">
                                        <EmptyDataTemplate>
                                            <br />
                                            <p class="text-center text-bold">
                                                <asp:Label ID="lblErrMsg" runat="server" SkinID="Errorlbl" Text="No Rows In Nomination of Employee"></asp:Label>
                                            </p>
                                        </EmptyDataTemplate>
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Nomination Details</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Action
                                                        </th>
                                                        <th>Nomin.For
                                                        </th>
                                                        <th>Nominee
                                                        </th>
                                                        <th>Relation
                                                        </th>
                                                        <th>Percentage
                                                        </th>
                                                        <th>DOB
                                                        </th>
                                                        <th>Age
                                                        </th>
                                                        <%-- <th id="divFolder" runat="server">Attachment
                                                        </th>
                                                        <th id="divBlob" runat="server">Attachment
                                                        </th>--%>
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
                                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("nfno")%>'
                                                        AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />&nbsp;
                                        <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Images/delete.png" CommandArgument='<%# Eval("nfno") %>'
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
                                                <%--<td id="tdFolder" runat="server">
                                                    <asp:HyperLink ID="lnkDownload" runat="server" Target="_blank" NavigateUrl='<%# GetFileNamePath(Eval("ATTACHMENT"),Eval("NFNO"),Eval("IDNO"))%>'><%# Eval("ATTACHMENT")%></asp:HyperLink>
                                                </td>--%>
                                                <%--<td style="text-align: center" id="tdBlob" runat="server" visible="false">
                                                    <asp:UpdatePanel ID="updPreview" runat="server">
                                                        <ContentTemplate>
                                                            <asp:ImageButton ID="imgbtnPreview" runat="server" OnClick="imgbtnPreview_Click" Text="Preview" ImageUrl="~/Images/action_down.png" ToolTip='<%# Eval("ATTACHMENT") %>'
                                                                data-toggle="modal" data-target="#preview" CommandArgument='<%# Eval("ATTACHMENT") %>' Visible='<%# Convert.ToString(Eval("ATTACHMENT"))==string.Empty?false:true %>'></asp:ImageButton>

                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="imgbtnPreview" EventName="Click" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
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


            </script>

        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSubmit" />
            <asp:PostBackTrigger ControlID="btnAdd" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

