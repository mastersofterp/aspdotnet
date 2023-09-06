<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Pay_Sb_publication_Details.aspx.cs" Inherits="PAYROLL_SERVICEBOOK_Pay_Sb_publication_Details" MasterPageFile="~/ServiceBookMaster.master" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="sbhead" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="sbctp" runat="Server">
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
                                                <h5>Publication Details</h5>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Publication Category</label>
                                            </div>
                                            <%--<asp:DropDownList ID="ddlPublication" runat="server" CssClass="form-control"
                                                            TabIndex="4" ToolTip="Select Publication Category">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            <asp:ListItem Value="Conference">Conference</asp:ListItem>
                                                            <asp:ListItem Value="Journal">Journal</asp:ListItem>
                                                            <asp:ListItem Value="Book">Book Chapter</asp:ListItem>
                                                            <%--<asp:ListItem Value="Paper">Paper</asp:ListItem>
                                                        </asp:DropDownList>--%>
                                            <asp:RadioButtonList ID="rblConferenceJournal" runat="server" ToolTip="Select Publication Category" RepeatDirection="Horizontal"
                                                OnSelectedIndexChanged="rblConferenceJournal_SelectedIndexChanged" AutoPostBack="true">
                                                <%--CssClass="form-control" --%>
                                                <asp:ListItem Value="0" Selected="True">Journal</asp:ListItem>
                                                <asp:ListItem Value="1">Conference</asp:ListItem>
                                                <asp:ListItem Value="2">Book Chapter</asp:ListItem>
                                                <asp:ListItem Value="3">Book </asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>

                                        <%--Academic Litrature Collections--%>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divCollection1" runat="server" hidden>
                                            <div class="label-dynamic">
                                                <label>Indexing</label>
                                            </div>
                                            <asp:DropDownList ID="ddlCollection" runat="server" CssClass="form-control" data-select2-enable="true"
                                                ToolTip="Academic Litrature Collections" TabIndex="1">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                <asp:ListItem Value="1">Scopus</asp:ListItem>
                                                <asp:ListItem Value="2">Non Scopus</asp:ListItem>
                                                <asp:ListItem Value="3">Web of Science</asp:ListItem>
                                                <%-- <asp:ListItem Value="InterNational">Inter National</asp:ListItem>--%>
                                            </asp:DropDownList>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Publication Type</label>
                                            </div>
                                            <asp:DropDownList ID="ddlPublicationType" runat="server" CssClass="form-control" data-select2-enable="true"
                                                ToolTip="Select Publication Type" TabIndex="2">
                                                <asp:ListItem Value="National">National</asp:ListItem>
                                                <asp:ListItem Value="InterNational">International</asp:ListItem>
                                                <%-- <asp:ListItem Value="InterNational">Inter National</asp:ListItem>--%>
                                            </asp:DropDownList>
                                        </div>

                                        <div class="form-group col-lg-6 col-md-6 col-12" id="divCollection" runat="server">
                                            <div class="label-dynamic">
                                                <label>Indexing</label>
                                            </div>
                                            <asp:CheckBoxList ID="cblIndexing" runat="server" RepeatDirection="Horizontal" ToolTip="Select Indexing" TabIndex="3" RepeatColumns="4">
                                                <asp:ListItem Value="1">&nbsp;Web of science &nbsp;&nbsp;</asp:ListItem>
                                                <asp:ListItem Value="2">&nbsp;SCI &nbsp;&nbsp;</asp:ListItem>
                                                <asp:ListItem Value="3">&nbsp;Indian citation &nbsp;&nbsp;</asp:ListItem>
                                                <asp:ListItem Value="4">&nbsp;Scopus &nbsp;&nbsp;</asp:ListItem>
                                                <asp:ListItem Value="5">&nbsp;UGC &nbsp;&nbsp;</asp:ListItem>
                                                <asp:ListItem Value="6">&nbsp;PUBMED &nbsp;&nbsp;</asp:ListItem>
                                                <asp:ListItem Value="7">&nbsp;Others &nbsp;&nbsp;</asp:ListItem>
                                            </asp:CheckBoxList>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divPublicationStatus" runat="server">
                                            <div class="label-dynamic">
                                                <label>Publication Status</label>
                                            </div>
                                            <asp:DropDownList ID="ddlPublicationStatus" runat="server" CssClass="form-control" data-select2-enable="true"
                                                ToolTip="Select Publication Status" TabIndex="4">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                <asp:ListItem Value="Published">Published</asp:ListItem>
                                                <asp:ListItem Value="Submitted">Submitted</asp:ListItem>
                                                <asp:ListItem Value="InPress">In Press</asp:ListItem>
                                                <asp:ListItem Value="Accepted">Accepted</asp:ListItem>
                                                <asp:ListItem Value="Under Reviewed">Under Reviewed</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divTitlePaper" runat="server">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label id="lblTitlePaper" runat="server">Title of Paper</label>
                                            </div>
                                            <asp:TextBox ID="txttitle" runat="server" CssClass="form-control" TabIndex="5" ToolTip="Enter Title of Paper" MaxLength="400"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvtitle" runat="server" ControlToValidate="txttitle"
                                                Display="None" ErrorMessage="Please Enter Title of Paper" ValidationGroup="ServiceBook"
                                                SetFocusOnError="True"> </asp:RequiredFieldValidator>

                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divName" runat="server">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label id="lblName" runat="server">Name of Journal</label>
                                            </div>
                                            <asp:TextBox ID="txtName" runat="server" CssClass="form-control" ToolTip="Enter Name of Journal/Conference" TabIndex="6" MaxLength="200"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvtxtName" runat="server" ControlToValidate="txtName"
                                                Display="None" ErrorMessage="Please Enter Name of Journal" ValidationGroup="ServiceBook" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="trOrg" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <label>Organised By</label>
                                            </div>
                                            <asp:TextBox ID="txtOrg" runat="server" CssClass="form-control" ToolTip="Enter Organised By" TabIndex="7" MaxLength="50"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divSub" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <label id="lblSub" runat="server">Title of Journal</label>
                                            </div>
                                            <asp:TextBox ID="txtSubject" runat="server" CssClass="form-control" ToolTip="Enter Title" TabIndex="8" MaxLength="50"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Publication Date</label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i id="Image1" runat="server" class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtPublicationDate" runat="server" CssClass="form-control" ToolTip="Enter Publication Date"
                                                    TabIndex="9" Style="z-index: 0;"></asp:TextBox>
                                                <ajaxToolKit:CalendarExtender ID="ceToDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtPublicationDate"
                                                    PopupButtonID="Image1" Enabled="true" EnableViewState="true" PopupPosition="BottomLeft">
                                                </ajaxToolKit:CalendarExtender>

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

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="trPageno" runat="server">
                                            <div class="label-dynamic">
                                                <label>Page No</label>
                                            </div>
                                            <asp:TextBox ID="txtPage" runat="server" CssClass="form-control" ToolTip="Enter Page Number" TabIndex="10" MaxLength="30"></asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftbepageno" runat="server" TargetControlID="txtPage" FilterType="Numbers,Custom" ValidChars="-">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divYear" runat="server">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Year</label>
                                            </div>
                                            <asp:TextBox ID="txtYear" runat="server" CssClass="form-control" ToolTip="Enter Year" TabIndex="11" MaxLength="4"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvtxtyear" runat="server" ControlToValidate="txtYear"
                                                Display="None" ErrorMessage="Please Enter Year" ValidationGroup="ServiceBook" SetFocusOnError="true"></asp:RequiredFieldValidator>

                                            <asp:RegularExpressionValidator ID="revyear" runat="server" ControlToValidate="txtYear" ValidationExpression="^[0-9]{4}$"
                                                ErrorMessage="Please Enter Valid Year" ValidationGroup="ServiceBook" SetFocusOnError="true" ForeColor="Red"></asp:RegularExpressionValidator>


                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtYear" FilterType="Numbers">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divMonth" runat="server">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Month</label>
                                            </div>
                                            <asp:TextBox ID="txtMonth" runat="server" CssClass="form-control" ToolTip="Enter Month" MaxLength="50" TabIndex="12"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvtxtmonth" runat="server" ControlToValidate="txtMonth"
                                                Display="None" ErrorMessage="Please Enter Month" ValidationGroup="ServiceBook" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtMonth" FilterType="LowercaseLetters,UppercaseLetters" ValidChars="-">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divVolumeNo" runat="server">
                                            <div class="label-dynamic">
                                                <label id="lblVolumeNo" runat="server">Volume No</label>
                                            </div>
                                            <asp:TextBox ID="txtVolumeNo" runat="server" CssClass="form-control" ToolTip="Enter Volume Number" TabIndex="13" MaxLength="50"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divIssueNo" runat="server">
                                            <div class="label-dynamic">
                                                <label id="lblIssueNo" runat="server">Issue No</label>
                                            </div>
                                            <asp:TextBox ID="txtIssueNo" runat="server" CssClass="form-control" ToolTip="Enter Issue Number" TabIndex="14" MaxLength="10"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divISBN" runat="server">
                                            <div class="label-dynamic">
                                                <label id="lblISBN" runat="server">Print ISSN/ ISBN :</label>
                                            </div>
                                            <%--ISBN/DOI--%>
                                            <asp:TextBox ID="txtIsbn" runat="server" CssClass="form-control" ToolTip="Enter ISBN Number" TabIndex="15" MaxLength="50"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divEISSN" runat="server">
                                            <div class="label-dynamic">
                                                <label id="lblEISSN" runat="server">E-ISSN/ ISBN :</label>
                                            </div>
                                            <asp:TextBox ID="txtEISSN" runat="server" CssClass="form-control" ToolTip="Enter E-ISSN/ ISBN Number" TabIndex="16" MaxLength="100"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divsponsore" runat="server">
                                            <div class="label-dynamic">
                                                <label id="lblspons" runat="server">Amount sponsored by College</label>
                                            </div>
                                            <asp:TextBox ID="txtspons" runat="server" CssClass="form-control" ToolTip="Enter Amount sponsored by College"
                                                TabIndex="17" MaxLength="10"></asp:TextBox>
                                            <%--onkeypress="return CheckNumeric(event,this);"--%>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtspons" FilterType="Numbers,Custom" ValidChars=".">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divLocation" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <label id="lblLocation" runat="server">Location :</label>
                                            </div>
                                            <asp:TextBox ID="txtLoctaion" runat="server" CssClass="form-control" ToolTip="Enter Location" TabIndex="18" MaxLength="150"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divPublisher" runat="server">
                                            <div class="label-dynamic">
                                                <%--visible="false"--%>
                                                <label id="lblPublisher" runat="server">Name of the Publisher :</label>
                                            </div>
                                            <%--Publisher--%>
                                            <asp:TextBox ID="txtPublisher" runat="server" CssClass="form-control" ToolTip="Enter Name of the Publisher" TabIndex="19" MaxLength="150"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divnameofpublisher">
                                            <div class="label-dynamic">
                                                <%--visible="false"--%>
                                                <label id="lblpublisheradd" runat="server">Publisher Address :</label>
                                            </div>
                                            <asp:TextBox ID="txtpublisheradd" runat="server" CssClass="form-control" ToolTip="Enter Publisher Address" TabIndex="20"
                                                MaxLength="250"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divimpact" runat="server">
                                            <div class="label-dynamic">
                                                <label>Impact Factor</label>
                                            </div>
                                            <asp:TextBox ID="txtimpactfactors" runat="server" CssClass="form-control" MaxLength="50"
                                                ToolTip="Enter Impact Factors" TabIndex="21"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divsitension" runat="server">
                                            <div class="label-dynamic">
                                                <label>Citation Index</label>
                                            </div>
                                            <asp:TextBox ID="txtcitation" runat="server" CssClass="form-control"
                                                ToolTip="Enter citation Index" TabIndex="22" MaxLength="50"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="div1" runat="server">
                                            <div class="label-dynamic">
                                                <label>DOI</label>
                                            </div>
                                            <asp:TextBox ID="txtDOI" runat="server" CssClass="form-control" MaxLength="100"
                                                ToolTip="Enter DOI Number" TabIndex="23"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Indexing Factors</label>
                                            </div>
                                            <asp:DropDownList ID="ddlIndexFac" runat="server" AppendDataBoundItems="true" TabIndex="24" ToolTip="Select Indexing Factors" data-select2-enable="true"
                                                CssClass="form-control">
                                                <asp:ListItem Text="Please Select" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="H-Index" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="i10-index" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="Citations" Value="3"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Indexing Factor Value</label>
                                            </div>
                                            <asp:TextBox ID="txtindexVal" runat="server" CssClass="form-control" TabIndex="25" ToolTip="Enter Indexing Factor Value"
                                                MaxLength="50" />
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Indexing Date</label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i id="imgFac" runat="server" class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtIndexDt" runat="server" CssClass="form-control" TabIndex="26" />
                                                <ajaxToolKit:CalendarExtender ID="CeIndexFac" runat="server" Format="dd/MM/yyyy"
                                                    TargetControlID="txtIndexDt" PopupButtonID="imgFac" Enabled="true" EnableViewState="true"
                                                    PopupPosition="BottomLeft">
                                                </ajaxToolKit:CalendarExtender>
                                                <ajaxToolKit:MaskedEditExtender ID="meeIndexDt" runat="server" TargetControlID="txtIndexDt"
                                                    Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                    AcceptNegative="Left" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate">
                                                </ajaxToolKit:MaskedEditExtender>
                                                <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="meeIndexDt"
                                                    ControlToValidate="txtIndexDt" InvalidValueMessage="Date is Invalid (Enter dd/mm/yyyy Format)"
                                                    Display="None" TooltipMessage="Please Enter Indexing Date" EmptyValueBlurredText="Empty"
                                                    InvalidValueBlurredMessage="Invalid Indexing Date" ValidationGroup="MisceDetail" SetFocusOnError="True" IsValidEmpty="false"
                                                    InitialValue="__/__/____" /><%--EmptyValueMessage="Please Enter From Date"--%>
                                            </div>

                                            <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtindexVal"
                                                        Display="None" ErrorMessage="Please Enter Indexing Date" SetFocusOnError="true"
                                                        ValidationGroup="MisceDetail"></asp:RequiredFieldValidator>--%>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divweblink" runat="server">
                                            <div class="label-dynamic">
                                                <label>Web Link</label>
                                            </div>
                                            <asp:TextBox ID="txtweblink" runat="server" CssClass="form-control" ToolTip="Enter Web Link" MaxLength="250"
                                                TextMode="MultiLine" TabIndex="27" />
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Abstract</label>
                                            </div>
                                            <asp:TextBox ID="txtDetails" runat="server" CssClass="form-control" ToolTip="Enter Details" MaxLength="1000"
                                                TabIndex="28" TextMode="MultiLine"
                                                onkeyDown="checkTextAreaMaxLength(this,event,'1000');" onkeyup="textCounter(this, this.form.remLen, 1000);"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="sub-heading">
                                                <h5>Add Authors (Note - Click On Add Button To Add Author.)</h5>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Author's Name</label>
                                            </div>
                                            <asp:TextBox ID="txtAuthor" runat="server" CssClass="form-control" MaxLength="50"
                                                ToolTip="Name Of The Author" TabIndex="29"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtAuthor"
                                                Display="None" ErrorMessage="Please Enter Author Name "
                                                ValidationGroup="add" SetFocusOnError="True"> </asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Author Role</label>
                                            </div>
                                            <asp:DropDownList ID="ddlAuthorRole" runat="server" CssClass="form-control" ToolTip="Enter Author Role" data-select2-enable="true" TabIndex="30">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                <asp:ListItem Value="FirstAuthor">First Author</asp:ListItem>
                                                <asp:ListItem Value="Co-Author">Co-Author</asp:ListItem>
                                                <asp:ListItem Value="Corresponding">Corresponding</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvAuthor" runat="server" ControlToValidate="ddlAuthorRole"
                                                Display="None" ErrorMessage="Please Select Author Role "
                                                ValidationGroup="add" SetFocusOnError="True" InitialValue="0"> </asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Affiliation</label>
                                            </div>
                                            <%--<span style="color: #FF0000">*</span>--%>
                                            <asp:TextBox ID="txtAffiliation" runat="server" CssClass="form-control" MaxLength="450"
                                                ToolTip="Enter Affiliation" TabIndex="31"></asp:TextBox>

                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtAffiliation"
                                                            Display="None" ErrorMessage="Please Enter Affiliation "
                                                            ValidationGroup="add" SetFocusOnError="True"> </asp:RequiredFieldValidator>--%>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnAdd" runat="server" Text="Add" ValidationGroup="add" CssClass="btn btn-primary"
                                        ToolTip="Click here to Add" OnClick="btnAdd_Click" />
                                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="add"
                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                </div>

                                <%--     Code to display author list--%>
                                <asp:Panel ID="pnlAuthorList" runat="server">
                                    <div class="col-12">
                                        <asp:ListView ID="lvAuthorList" runat="server">
                                            <EmptyDataTemplate>
                                                <br />
                                                <p class="text-center text-bold">
                                                    <asp:Label ID="lblErrMsg" runat="server" SkinID="Errorlbl" Text="No Author Found"></asp:Label>
                                                </p>
                                            </EmptyDataTemplate>
                                            <LayoutTemplate>
                                                <div class="sub-heading">
                                                    <h5>Author List</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>Action</th>
                                                            <th>Author Name</th>
                                                            <th>Author Role</th>
                                                            <th>Affiliation</th>
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
                                                        <asp:ImageButton ID="btnDeleteAuthor" runat="server" ImageUrl="~/Images/delete.png" CommandArgument='<%# Eval("SRNO") %>'
                                                            AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDeleteAuthor_Click"
                                                            OnClientClick="showConfirmDel(this); return false;" />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblAuthName" runat="server" Text='<%#Eval("Name")%>'></asp:Label>
                                                        <asp:HiddenField ID="hdnsrno" runat="server" Value='<%# Eval("SRNO") %>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblAuthorrole" runat="server" Text='<%#Eval("Author_Role")%>'></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="lblAuthoraffi" runat="server" Text='<%#Eval("Affiliation")%>'></asp:Label></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </div>
                                </asp:Panel>
                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="sub-heading">
                                                <h5>Upload Documents</h5>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12">
                                    <label>Attachments :</label>
                                    <asp:FileUpload ID="flPUB" runat="server" ToolTip="Click here to Upload Attachments" TabIndex="32" />
                                    <asp:Label ID="Label2" runat="server" Text=" Please Select valid Document file(e.g. .pdf,.doc) upto 5MB" ForeColor="Red"></asp:Label>

                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12" id="divBlob" runat="server" visible="false">
                                            <asp:Label ID="lblBlobConnectiontring" runat="server" Text=""></asp:Label>
                                            <asp:HiddenField ID="hdnBlobCon" runat="server" />
                                            <asp:Label ID="lblBlobContainer" runat="server" Text=""></asp:Label>
                                            <asp:HiddenField ID="hdnBlobContainer" runat="server" />
                                        </div>
                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="ServiceBook" TabIndex="33"
                                        OnClick="btnSubmit_Click" CssClass="btn btn-primary" ToolTip="Click here to Submit" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" TabIndex="34"
                                        OnClick="btnCancel_Click" CssClass="btn btn-warning" ToolTip="Click here to Reset" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="ServiceBook"
                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                </div>
                            </asp:Panel>
                            <div class="col-12">
                                <asp:Panel ID="pnlList" runat="server">
                                    <asp:ListView ID="lvPublicationDetails" runat="server">
                                        <EmptyDataTemplate>
                                            <br />
                                            <p class="text-center text-bold">
                                                <asp:Label ID="lblErrMsg" runat="server" SkinID="Errorlbl" Text="No Rows In Publication of Employee"></asp:Label>
                                            </p>
                                        </EmptyDataTemplate>
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Publication Details</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Action
                                                        </th>
                                                        <th>Publication Type
                                                        </th>
                                                        <th>Title
                                                        </th>
                                                        <%--<th>Subject
                                                                </th>--%>
                                                        <th>Year
                                                        </th>
                                                        <th>Month
                                                        </th>
                                                        <th id="divFolder" runat="server">Attachment
                                                        </th>
                                                        <th id="divBlob" runat="server">Attachment
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                    <td>
                                                        <%# Eval("TITLE")%>
                                                    </td>
                                                </tbody>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("PUBTRXNO")%>'
                                                        AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />
                                                    <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Images/delete.png" CommandArgument='<%# Eval("PUBTRXNO") %>'
                                                        AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                                        OnClientClick="showConfirmDel(this); return false;" />
                                                </td>
                                                <td>
                                                    <%# Eval("PUBLICATION_TYPE")%>
                                                </td>
                                                <td>
                                                    <%# Eval("TITLE")%>
                                                </td>
                                                <%-- <td>
                                                        <%# Eval("SUBJECT")%>
                                                    </td>--%>
                                                <%-- <td>
                                                    <%# Eval("PUBLICATIONDATE", "{0:dd/MM/yyyy}")%>
                                                </td>--%>
                                                <td>
                                                    <%# Eval("Year")%>
                                                </td>
                                                <td>
                                                    <%# Eval("MONTH")%>
                                                </td>
                                                <td id="tdFolder" runat="server">
                                                    <asp:HyperLink ID="lnkDownload" runat="server" Target="_blank" NavigateUrl='<%# GetFileNamePath(Eval("ATTACHMENT"),Eval("PUBTRXNO"),Eval("IDNO"))%>'><%# Eval("ATTACHMENT")%></asp:HyperLink>
                                                </td>
                                                <td style="text-align: center" id="tdBlob" runat="server" visible="false">
                                                    <asp:UpdatePanel ID="updPreview" runat="server">
                                                        <ContentTemplate>
                                                            <asp:ImageButton ID="imgbtnPreview" runat="server" OnClick="imgbtnPreview_Click" Text="Preview" ImageUrl="~/Images/action_down.png" ToolTip='<%# Eval("ATTACHMENT") %>'
                                                                data-toggle="modal" data-target="#preview" CommandArgument='<%# Eval("ATTACHMENT") %>' Visible='<%# Convert.ToString(Eval("ATTACHMENT"))==string.Empty?false:true %>'></asp:ImageButton>
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
                                <%-- <asp:Panel ID="pnlList" runat="server" ScrollBars="Auto">
                            <asp:ListView ID="lvPublicationDetails" runat="server" ItemPlaceholderID="PlaceHolder1">
                                <EmptyDataTemplate>
                                    <br />
                                    <p class="text-center text-bold">
                                        <asp:Label ID="Label1" runat="server" SkinID="Errorlbl" Text="No Rows In Publication Details"></asp:Label>
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
                                                <tr id="Tr1" runat="server" />
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
                        </asp:Panel>--%>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <%--<asp:PostBackTrigger ControlID="btnUpload" />--%>

            <asp:PostBackTrigger ControlID="btnSubmit" />
        </Triggers>
    </asp:UpdatePanel>
    <%-- <th width="10%">
                                                    Remarks
                                                </th>--%> <%--<%# Eval("PUBLICATION_TYPE")%>--%>
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
