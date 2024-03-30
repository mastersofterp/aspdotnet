<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Job_Announcement.aspx.cs" Inherits="Job_Announcement" ValidateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="<%=Page.ResolveClientUrl("~/plugins/multi-select/bootstrap-multiselect.css")%>" rel="stylesheet" />
    <script src="<%=Page.ResolveClientUrl("~/plugins/multi-select/bootstrap-multiselect.js")%>"></script>

    <script src='<%=Page.ResolveUrl("~/plugins/TinyMce/jquery.tinymce.min.js") %>'></script>

    <style>
        #DataTables_Table_3_wrapper .dataTables_scrollHeadInner  {
           width: max-content !important;
        }
           .dataTables_scrollHeadInner {
            width: max-content !important;
        }
        .card-header .title {
            font-size: 15px;
            color: #000;
        }

        .card-header .accicon {
            float: right;
            font-size: 20px;
            width: 1.2em;
        }

        .card-header {
            cursor: pointer;
            border-bottom: none;
            padding: .3rem 0.7rem;
        }

        .card {
            border: 1px solid #ddd;
        }

        .card-body {
            border-top: 1px solid #ddd;
            padding: 1.25rem 0rem;
        }

        .card-header:not(.collapsed) .rotate-icon {
            transform: rotate(180deg);
        }

        input[type=checkbox], input[type=radio] {
            margin: 0px 5px 0 0;
        }

        .fa-plus {
            color: #17a2b8;
            border: 1px solid #17a2b8;
            border-radius: 50%;
            padding: 6px 8px;
        }

        .company-logo img {
            width: 26px;
        }

        .MyLRMar {
            margin-left: 5px;
            margin-right: 5px;
        }
    </style>

    <style>
        li.select2-selection__choice {
            max-width: 100%;
            overflow: hidden;
            text-overflow: ellipsis;
        }
        /*ul.select2-selection__rendered {
    padding-right: 12px !important; 
    }*/
    </style>


    <asp:UpdatePanel ID="upnlMain" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:HiddenField ID="hfdTemplate" runat="server" />
            <asp:HiddenField ID="hfdEligibility" runat="server" />
            <asp:HiddenField ID="hfDescription" runat="server" />
            <asp:HiddenField ID="hfdOperation" runat="server" Value="1" />
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title"><span>Job Announcement</span></h3>
                        </div>

                        <div class="box-body">
                            <div class="accordion" id="accordionExample">

                                <asp:UpdatePanel ID="upnlJobDetails" runat="server">
                                    <ContentTemplate>
                                        <div class="card">
                                            <div class="card-header" data-toggle="collapse" data-target="#collapseOne" aria-expanded="true">
                                                <span class="title">Job Details </span>
                                                <span class="accicon"><i class="fa fa-angle-down rotate-icon"></i></span>
                                            </div>


                                            <div id="collapseOne" class="collapse show">
                                                <div class="card-body">
                                                    <div class="col-12">
                                                        <div class="row">
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Company Name</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlCompanyName" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" TabIndex="1">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvCollegeScheme" runat="server" ControlToValidate="ddlCompanyName" Display="None"
                                                                    ErrorMessage="Please Select Company Name." InitialValue="0" ValidationGroup="jobannouncemennt">
                                                                </asp:RequiredFieldValidator>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Job Type</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlJobType" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" TabIndex="2">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlJobType" Display="None"
                                                                    ErrorMessage="Please Select Job Type." InitialValue="0" ValidationGroup="jobannouncemennt">
                                                                </asp:RequiredFieldValidator>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Job Role</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlJobRole" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" TabIndex="3">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlJobRole" Display="None"
                                                                    ErrorMessage="Please Select Job Role." InitialValue="0" ValidationGroup="jobannouncemennt">
                                                                </asp:RequiredFieldValidator>

                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Placement Mode</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlPlacement" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" TabIndex="4">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                   <%-- <asp:ListItem Value="1">Online</asp:ListItem>
                                                                    <asp:ListItem Value="2">Offline</asp:ListItem>
                                                                    <asp:ListItem Value="3">Hybrid</asp:ListItem>--%>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlPlacement" Display="None"
                                                                    ErrorMessage="Please Select Placement Mode." InitialValue="0" ValidationGroup="jobannouncemennt">
                                                                </asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>

                                                        <div class="row">
                                                            <div class="col-12">
                                                                <div class="sub-heading">
                                                                    <h5>Location</h5>
                                                                </div>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>City</label>
                                                                </div>
                                                                <asp:ListBox ID="lstbxCity" runat="server" CssClass="form-control multi-select-demo" SelectionMode="Multiple" AppendDataBoundItems="true" TabIndex="5"></asp:ListBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="lstbxCity" Display="None"
                                                                    ErrorMessage="Please Select City." InitialValue="" ValidationGroup="jobannouncemennt">
                                                                </asp:RequiredFieldValidator>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <label></label>
                                                                </div>
                                                                <asp:CheckBox ID="chkAnywhere" runat="server" Text="Any where in India" OnCheckedChanged="chkAnywhere_CheckedChanged" AutoPostBack="true" TabIndex="6" Visible="false" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                    <%--<Triggers>
                                        <asp:PostBackTrigger ControlID="chkAnywhere" />
                                    </Triggers>--%>
                                </asp:UpdatePanel>


                                <%--<asp:UpdatePanel ID="pnlschedule" runat="server">
                             <ContentTemplate>--%>

                                <div class="card">
                                    <div class="card-header collapsed" data-toggle="collapse" data-target="#collapseTwo" aria-expanded="true" aria-controls="collapseTwo">
                                        <span class="title">Schedule Details</span>
                                        <span class="accicon"><i class="fa fa-angle-down rotate-icon"></i></span>
                                    </div>
                                    <div id="collapseTwo" class="collapse collapse show">
                                        <div class="card-body">
                                            <div class="col-12">
                                                <div class="row">
                                                  <%--  <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Schedule Date (From-To)</label>
                                                        </div>
                                                        <asp:HiddenField ID="hdnDate" runat="server" />
                                                        <div id="picker" class="form-control" tabindex="7">

                                                            <i class="fa fa-calendar"></i>&nbsp;
                                                        <span id="date"></span>
                                                            <i class="fa fa-angle-down" aria-hidden="true" style="float: right; padding-top: 4px; font-weight: bold;"></i>
                                                        </div>
                                                    </div>
                                                       --%>


                                                       <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Schedule From Date</label>
                                                            </div>
                                                             <asp:TextBox ID="txtSchFromDate" runat="server" CssClass="form-control" type="date" TabIndex="3"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtSchFromDate" Display="None"
                                                            ErrorMessage="Please Select Schedule From Date." ValidationGroup="jobannouncemennt">
                                                        </asp:RequiredFieldValidator>
                                                        </div>
                                                       

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Schedule To Date</label>
                                                            </div>
                                                            <asp:TextBox ID="txtSchToDate" runat="server" CssClass="form-control" type="date" TabIndex="3"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtSchToDate" Display="None"
                                                            ErrorMessage="Please Select Schedule To Date." ValidationGroup="jobannouncemennt">
                                                        </asp:RequiredFieldValidator>
                                                        </div>

                                                   

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Venue</label>
                                                        </div>
                                                        <asp:TextBox ID="txtVenue" runat="server" CssClass="form-control" TabIndex="8" ValidationGroup="jobannouncemennt" MaxLength="80" onkeypress="return alphaOnly(event);" />
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtVenue" Display="None"
                                                            ErrorMessage="Please Enter Venue." ValidationGroup="jobannouncemennt">
                                                        </asp:RequiredFieldValidator>
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                                                            TargetControlID="txtVenue" FilterType="Custom" FilterMode="InvalidChars"
                                                            InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" />
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Last Date to Apply</label>
                                                        </div>
                                                        <%-- <asp:TextBox ID="txtLastDate" runat="server" CssClass="form-control" type="date" TabIndex="9" ValidationGroup="jobannouncemennt" onchange="pastdate()" />
                                                        <ajaxtoolkit:CalendarExtender ID="Calendar1" OnClientDateSelectionChanged="SelectDate" PopupButtonID="imgPopup" runat="server" TargetControlID="txtLastDate"
                                                          Format="dd/MM/yyyy">
                                                          </ajaxtoolkit:CalendarExtender>--%>
                                                        <asp:TextBox ID="txtLastDate" runat="server" CssClass="form-control" type="date" TabIndex="3"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtLastDate" Display="None"
                                                            ErrorMessage="Please Select Last Date." ValidationGroup="jobannouncemennt">
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <%-- </ContentTemplate>
                             </asp:UpdatePanel>--%>


                                <%-- <asp:UpdatePanel ID="upnlJobDiscription" runat="server">
                                <ContentTemplate>--%>
                                <div class="card">
                                    <div class="card-header collapsed" data-toggle="collapse" data-target="#collapseThree" aria-expanded="true">
                                        <span class="title">Job Description</span>
                                        <span class="accicon"><i class="fa fa-angle-down rotate-icon"></i></span>
                                    </div>
                                    <div id="collapseThree" class="collapse collapse show">
                                        <div class="card-body">
                                            <div class="col-12">
                                                <div class="row">
                                                    <div class="form-group col-lg-6 col-md-12 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Job Description</label>
                                                        </div>
                                                        <asp:TextBox ID="templateEditor" runat="server" Visible="true" TextMode="MultiLine" ClientIDMode="Static" CssClass="form-control TextBox1" TabIndex="10" MaxLength="300"></asp:TextBox>
                                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="templateEditor" Display="None"
                                                            ErrorMessage="Please Enter Job Description." ValidationGroup="jobannouncemennt">
                                                        </asp:RequiredFieldValidator>
                                                    </div>

                                                    <div class="form-group col-lg-6 col-md-12 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Eligibility</label>
                                                        </div>
                                                        <asp:TextBox ID="txtEligibility" runat="server" Visible="true" TextMode="MultiLine" ClientIDMode="Static" CssClass="form-control TextBox1" TabIndex="11" MaxLength="300"></asp:TextBox>
                                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtEligibility" Display="None"
                                                            ErrorMessage="Please Enter Eligibility." ValidationGroup="jobannouncemennt">
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <%--</ContentTemplate>
                            </asp:UpdatePanel>--%>

                                <%-- <asp:UpdatePanel ID="upnlEligibilityCriteria" runat="server">
                                <ContentTemplate>--%>
                                <div class="card">
                                    <div class="card-header collapsed" data-toggle="collapse" data-target="#collapseTwo" aria-expanded="true" aria-controls="collapseTwo">
                                        <span class="title">Eligibility Criteria [%]</span>
                                        <span class="accicon"><i class="fa fa-angle-down rotate-icon"></i></span>
                                    </div>
                                    <div id="Div1" class="collapse collapse show">
                                        <div class="card-body">
                                            <div class="col-12">
                                                <div class="row">                  
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>SSC</label>
                                                        </div>
                                                        <asp:TextBox ID="txtSSC" runat="server" Visible="true" ClientIDMode="Static" CssClass="form-control TextBox1" TabIndex="12" onkeyup="validateNumeric(this);"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvSSC" runat="server" ControlToValidate="txtSSC" Display="None"
                                                            ErrorMessage="Please Enter SSC Percentage" ValidationGroup="jobannouncemennt">
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>HSC</label>
                                                        </div>
                                                        <asp:TextBox ID="txtHSC" runat="server" Visible="true" ClientIDMode="Static" CssClass="form-control TextBox1" TabIndex="13" onkeyup="validateNumeric(this);"></asp:TextBox>
                                                        
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Diploma</label>
                                                        </div>
                                                       <asp:TextBox ID="txtDiploma" runat="server" Visible="true" ClientIDMode="Static" CssClass="form-control TextBox1" TabIndex="14" onkeyup="validateNumeric(this);"></asp:TextBox>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>UG</label>
                                                        </div>
                                                       <asp:TextBox ID="txtUG" runat="server" Visible="true" ClientIDMode="Static" CssClass="form-control TextBox1" TabIndex="15" onkeyup="validateNumeric(this);"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvUG" runat="server" ControlToValidate="txtUG" Display="None"
                                                            ErrorMessage="Please Enter UG Percentage" ValidationGroup="jobannouncemennt">
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>PG</label>
                                                        </div>
                                                       <asp:TextBox ID="txtPG" runat="server" Visible="true" ClientIDMode="Static" CssClass="form-control TextBox1" TabIndex="16" onkeyup="validateNumeric(this);"></asp:TextBox>
                                                    </div>
                                                    
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <%--</ContentTemplate>
                            </asp:UpdatePanel>--%>

                                <asp:UpdatePanel ID="upnlsalary" runat="server">
                                    <ContentTemplate>
                                        <div class="card">
                                            <div class="card-header collapsed" data-toggle="collapse" data-target="#collapseFour" aria-expanded="true">
                                                <span class="title">Salary/Stipend </span>
                                                <span class="accicon"><i class="fa fa-angle-down rotate-icon"></i></span>
                                            </div>
                                            <div id="collapseFour" class="collapse collapse show">
                                                <div class="card-body">
                                                    <div class="col-12">
                                                        <div class="row">
                                                            <div class="form-group col-lg-12 col-md-12 col-12">
                                                                <asp:RadioButton ID="chksalary" runat="server" Text="Salary/Stipend" GroupName="salary" OnCheckedChanged="chksalary_CheckedChanged" CausesValidation="false" AutoPostBack="true" TabIndex="14" Checked="true" /><br />
                                                                <asp:RadioButton ID="chkNoSalary" runat="server" Text="This job profile does not have any Salary/Stipend" GroupName="salary" OnCheckedChanged="chkNoSalary_CheckedChanged" CausesValidation="false" AutoPostBack="true" TabIndex="12" /><br />
                                                                <asp:RadioButton ID="chkSalaryAnnLater" runat="server" Text="Salary/Stipend to be Announced later" GroupName="salary" OnCheckedChanged="chkSalaryAnnLater_CheckedChanged" CausesValidation="false" AutoPostBack="true" TabIndex="13" />


                                                            </div>
                                                            <div class="col-md-8 col-12">
                                                                <div class="row">

                                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                                        <asp:Panel ID="pnlamount" runat="server">
                                                                            <div class="label-dynamic">
                                                                                <%--<sup>* </sup>--%>
                                                                                <b>
                                                                                    <asp:Label ID="lblAmount" runat="server" Text="Amount" /></b>
                                                                            </div>
                                                                            <asp:TextBox ID="txtAmount" runat="server" CssClass="form-control" TabIndex="14" MaxLength="10" onkeyup="validateNumeric(this);" />
                                                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtAmount" Display="None" 
                                    ErrorMessage="Please Enter Amount."  ValidationGroup="jobannouncemennt">
                                                            </asp:RequiredFieldValidator>--%>
                                                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftrPermPIN" runat="server" ValidChars="0123456789."
                                                                                TargetControlID="txtAmount">
                                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                                            <%--FilterType="Numbers"--%>
                                                                        </asp:Panel>
                                                                    </div>

                                                                    <div class="form-group col-lg-4 col-md-6 col-12 text-center">
                                                                        <div class="label-dynamic">
                                                                            <label></label>
                                                                        </div>
                                                                        <asp:LinkButton ID="btnSpecifyRange" runat="server" CssClass="btn btn-outline-info" OnClick="btnSpecifyRange_Click" TabIndex="15">Specify Range</asp:LinkButton>
                                                                    </div>
                                                                    <div class="col-lg-4 col-md-6 col-12">
                                                                        <div class="row">

                                                                            <div class="form-group col-lg-6 col-md-6 col-6">
                                                                                <asp:Panel ID="pnlmin" runat="server">
                                                                                    <div class="label-dynamic">
                                                                                        <sup>* </sup>
                                                                                        <b>
                                                                                            <asp:Label ID="lblMinAmount" runat="server" Text="MinAmount"></asp:Label></b>
                                                                                    </div>
                                                                                    <asp:TextBox ID="txtMinAmount" runat="server" CssClass="form-control" TabIndex="16" MaxLength="10" onkeyup="validateNumeric(this);" />
                                                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtMinAmount" Display="None" 
                                    ErrorMessage="Please Enter Min Amount."  ValidationGroup="jobannouncemennt">
                                                                                         </asp:RequiredFieldValidator>
                                                                                      <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers"
                                                                TargetControlID="txtMinAmount">
                                                            </ajaxToolKit:FilteredTextBoxExtender>--%>
                                                                                </asp:Panel>
                                                                            </div>
                                                                            <div class="form-group col-lg-6 col-md-6 col-6">
                                                                                <asp:Panel ID="pnlmax" runat="server">
                                                                                    <div class="label-dynamic">
                                                                                        <sup>* </sup>
                                                                                        <b>
                                                                                            <asp:Label ID="lblMaxAmount" runat="server" Text="MaxAmount"></asp:Label></b>


                                                                                    </div>
                                                                                    <asp:TextBox ID="txtMaxAmount" runat="server" CssClass="form-control" TabIndex="17" MaxLength="10" onkeyup="validateNumeric(this);" />
                                                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtMaxAmount" Display="None" 
                                    ErrorMessage="Please Enter Max Amount."  ValidationGroup="jobannouncemennt">
                                                                                         </asp:RequiredFieldValidator>
                                                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers"
                                                                TargetControlID="txtMaxAmount">
                                                                                        </ajaxToolKit:FilteredTextBoxExtender>--%>
                                                                                </asp:Panel>
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <%-- <sup>* </sup>--%>
                                                                            <label>Currency</label>
                                                                        </div>
                                                                        <asp:DropDownList ID="ddlCurrency" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" TabIndex="18">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddlCurrency" Display="None"
                                                                            ErrorMessage="Please Select Currency." InitialValue="0" ValidationGroup="jobannouncemennt">
                                                                        </asp:RequiredFieldValidator>--%>
                                                                    </div>

                                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <%--<sup>* </sup>--%>
                                                                            <label>Interval</label>
                                                                        </div>
                                                                        <asp:DropDownList ID="ddlInterval" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" TabIndex="19">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="ddlInterval" Display="None"
                                                                            ErrorMessage="Please Select Interval." InitialValue="0" ValidationGroup="jobannouncemennt">
                                                                        </asp:RequiredFieldValidator>--%>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-4 col-12">
                                                                <div class="col-12">
                                                                    <div class="row">
                                                                        <div class="form-group col-lg-12 col-md-12 col-12">
                                                                            <div class="label-dynamic">
                                                                                <label>Salary/Stipend Break-up/Additional Details</label>
                                                                            </div>
                                                                            <asp:TextBox ID="txtAddDetails" runat="server" Visible="true" TextMode="MultiLine" CssClass="form-control" TabIndex="20" MaxLength="280"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                    <%--<Triggers>
                                        <asp:PostBackTrigger ControlID="chkNoSalary" />
                                        <asp:PostBackTrigger ControlID="chkSalaryAnnLater" />
                                        <asp:PostBackTrigger ControlID="btnSpecifyRange" />
                                    </Triggers>--%>
                                </asp:UpdatePanel>


                                <%-- <asp:UpdatePanel ID="upnlSelectionCriteria" runat="server">
                                    <ContentTemplate>--%>
                                <%--  <div class="card">--%>
                                <div class="card-header collapsed" data-toggle="collapse" data-target="#collapseFive" aria-expanded="true">
                                    <span class="title">Selection Criteria </span>
                                    <span class="accicon"><i class="fa fa-angle-down rotate-icon"></i></span>
                                </div>
                                <div id="collapseFive" class="collapse collapse show">
                                    <div class="card-body">
                                        <div class="col-12">
                                            <div class="row">
                                                <div class="col-12 col-md-12">
                                                    <div class="row">
                                                        <div class="col-12">
                                                            <div class="sub-heading">
                                                                <h5>Rounds</h5>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <%--<div class="row">
                                                                    <asp:PlaceHolder ID="placeH" runat="server" Visible="true"></asp:PlaceHolder>
                                                                 </div>--%>
                                                    <div class="row">


                                                        <div class="col-lg-3 col-md-6 col-12">
                                                            <label><span style="color: red">*</span>Round's</label>

                                                            <asp:DropDownList ID="ddlRound" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" TabIndex="21">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                           <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlRound"
                                                                Display="None" ErrorMessage="Please Select Round." SetFocusOnError="true"
                                                                ValidationGroup="Round" />--%>
                                                             <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlRound" Display="None"
                                                                    ErrorMessage="Please Select Round." InitialValue="0" ValidationGroup="Round">
                                                                </asp:RequiredFieldValidator>
                                                        </div>

                                                        <div class="col-lg-3 col-md-6 col-12">
                                                            <label><span style="color: red">*</span>Round Sr.No.</label>

                                                            <asp:TextBox ID="txtRoundSrNo" runat="server" MaxLength="2000" class="form-control">
                                                            </asp:TextBox>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftbtxtSrNo" runat="server" ValidChars="0123456789"
                                                                FilterType="Custom" FilterMode="ValidChars" TargetControlID="txtRoundSrNo">
                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                            <asp:RequiredFieldValidator ID="rfvProjDetails" runat="server" ControlToValidate="txtRoundSrNo"
                                                                Display="None" ErrorMessage="Please Enter Round Serial Number." SetFocusOnError="true"
                                                                ValidationGroup="Round" />
                                                        </div>


                                                        <div class="col-lg-3 col-md-6 col-12">
                                                            <asp:Button ID="btnRoundAdd" runat="server" OnClick="btnRoundAdd_Click" Text="Add" CssClass="btn btn-primary"
                                                                ValidationGroup="Round" Width="80px" />


                                                            <asp:ValidationSummary ID="ValidationSummary4" runat="server"
                                                                DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Round" />


                                                        </div>

                                                        <div class="col-lg-3 col-md-6 col-12">
                                                            <div class="row">
                                                                <div class="form-group col-lg-12 col-md-12 col-12">
                                                                    <div class="label-dynamic">
                                                                        <label>Description</label>
                                                                    </div>
                                                                    <asp:TextBox ID="txtDescription" runat="server" Visible="true" TextMode="MultiLine" ClientIDMode="Static" CssClass="form-control TextBox1" MaxLength="280"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-12 col-md-12 col-12">


                                                            <div class="row">
                                                                <asp:Panel ID="pnlRoundDetail" runat="server" ScrollBars="Auto">

                                                                  <%--  <asp:ListView ID="lvRoundDetail" runat="server">
                                                                        <EmptyDataTemplate>
                                                                            <br />
                                                                            <center>
                                                                    <asp:Label ID="lblerr" runat="server" SkinID="Errorlbl" Text="No Records Found" ></asp:Label></center>
                                                                        </EmptyDataTemplate>
                                                                        <LayoutTemplate>
                                                                            <div id="demo-grid" >
                                                                                <div class="titlebar">
                                                                                    <strong>Round Details</strong>
                                                                                </div>--%>
                                                                    <asp:ListView ID="lvRoundDetail" runat="server">
                                                                          <LayoutTemplate>
                                                                            <div id="lgv1">
                                                                                <table class="table table-bordered table-hover">
                                                                                    <thead>
                                                                                        <tr class="bg-light-blue">
                                                                                            <th>Delete
                                                                                            </th>
                                                                                            <th>Rounds
                                                                                            </th>
                                                                                            <th>Rounds Sr.No.
                                                                                            </th>
                                                                                        </tr>
                                                                                    </thead>
                                                                                    <tbody>
                                                                                        <tr id="itemPlaceholder" runat="server">
                                                                                        </tr>
                                                                                    </tbody>
                                                                                </table>
                                                                            </div>
                                                                        </LayoutTemplate>
                                                                        <ItemTemplate>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:ImageButton ID="btnDelRoundDetail" runat="server" AlternateText="Delete Record"
                                                                                        CommandArgument='<%# Eval("SEQNO") %>' ImageUrl="~/images/delete.gif" OnClick="btnDelRoundDetail_Click" OnClientClick="return confirm('Are you sure you want to Delete Record ?');"
                                                                                        ToolTip="Delete Record" />
                                                                                </td>

                                                                                <td>
                                                                                    <asp:Label runat="server" ID="lblRoundDetail" Text='<%# Eval("ROUNDS") %>'></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label runat="server" ID="Label1" Text='<%# Eval("ROUNDS_SRNO") %>'></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                        </ItemTemplate>
                                                                    </asp:ListView>
                                                                </asp:Panel>

                                                            </div>



                                                            <%--<div class="form-group col-lg-1 col-md-1 col-1">
                                                                        <div class="label-dynamic mt-2">
                                                                            <label>1</label>
                                                                        </div>
                                                                    </div>--%>
                                                            <%-- <div class="form-group col-lg-6 col-md-8 col-9">
                                                                        <asp:DropDownList ID="ddlRound1" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" TabIndex="21">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>--%>

                                                            <%-- <div class="form-group col-lg-2 col-md-2 col-2">--%>
                                                            <%-- <i class="fa fa-plus" aria-hidden="true"></i>--%>
                                                            <%-- <asp:LinkButton ID="btnr1" runat="server" CssClass="fa fa-plus" aria-hidden="true" OnClick="btnr1_Click" TabIndex="22"></asp:LinkButton>--%>
                                                            <%--<asp:Button ID="btnr1" runat="server" Text="+" CssClass="fa fa-plus" aria-hidden="true" OnClick="btnr1_Click" />--%>
                                                            <%--</div>
                                                                </div>--%>

                                                            <%--<asp:Panel ID="pnlr2" runat="server" Visible="false">
                                                                    <div class="row">
                                                                        <div class="form-group col-lg-1 col-md-1 col-1">
                                                                            <div class="label-dynamic mt-2">
                                                                                <label>2</label>
                                                                            </div>
                                                                        </div>
                                                                        <div class="form-group col-lg-6 col-md-8 col-9">
                                                                            <asp:DropDownList ID="ddlRound2" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" TabIndex="23">
                                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                            <asp:Panel ID="pnlround1" runat="server">
                                                                            </asp:Panel>
                                                                        </div>--%>
                                                            <%-- <div class="form-group col-lg-2 col-md-2 col-2">
                                                                            <asp:LinkButton ID="btnr2" runat="server" CssClass="fa fa-plus" aria-hidden="true" OnClick="btnr2_Click" TabIndex="24"></asp:LinkButton>--%>
                                                            <%-- <asp:Button ID="btnr2" runat="server" Text="+" CssClass="fa fa-plus" aria-hidden="true" OnClick="btnr2_Click"/>--%>
                                                            <%--<i class="fa fa-plus" aria-hidden="true"></i>--%>
                                                            <%--  </div>
                                                                    </div>
                                                                </asp:Panel>--%>
                                                            <%-- <asp:Panel ID="pnlr3" runat="server" Visible="false">
                                                                    <div class="row">

                                                                        <div class="form-group col-lg-1 col-md-1 col-1">
                                                                            <div class="label-dynamic mt-2">
                                                                                <label>3</label>
                                                                            </div>
                                                                        </div>
                                                                        <div class="form-group col-lg-6 col-md-8 col-9">
                                                                            <asp:DropDownList ID="ddlRound3" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" TabIndex="25">
                                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                            <asp:Panel ID="pnlRound2" runat="server">
                                                                            </asp:Panel>
                                                                        </div>--%>

                                                            <%--  <div class="form-group col-lg-2 col-md-2 col-2">--%>
                                                            <%--<asp:Button ID="btnr3" runat="server" Text="+" CssClass="fa fa-plus" aria-hidden="true"/>--%>
                                                            <%-- <asp:LinkButton ID="btnr3" runat="server" CssClass="fa fa-plus" aria-hidden="true" OnClick="btnr3_Click" TabIndex="26"></asp:LinkButton>--%>
                                                            <%--<i class="fa fa-plus" aria-hidden="true"></i>--%>
                                                            <%-- </div>

                                                                    </div>
                                                                </asp:Panel>--%>

                                                            <%--   <asp:Panel ID="pnlr4" runat="server" Visible="false">
                                                                    <div class="row">

                                                                        <div class="form-group col-lg-1 col-md-1 col-1">
                                                                            <div class="label-dynamic mt-2">
                                                                                <label>4</label>
                                                                            </div>
                                                                        </div>
                                                                        <div class="form-group col-lg-6 col-md-8 col-9">
                                                                            <asp:DropDownList ID="ddlRound4" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" TabIndex="27">
                                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                        <div class="form-group col-lg-2 col-md-2 col-2">--%>
                                                            <%--<asp:Button ID="btnr3" runat="server" Text="+" CssClass="fa fa-plus" aria-hidden="true"/>--%>
                                                            <%-- <asp:LinkButton ID="btnr4" runat="server" CssClass="fa fa-plus" aria-hidden="true" OnClick="btnr4_Click"></asp:LinkButton>--%>
                                                            <%--<i class="fa fa-plus" aria-hidden="true"></i>--%>
                                                            <%--   </div>

                                                                    </div>
                                                                </asp:Panel>--%>

                                                            <%--    <asp:Panel ID="pnlr5" runat="server" Visible="false">
                                                                    <div class="row">

                                                                        <div class="form-group col-lg-1 col-md-1 col-1">
                                                                            <div class="label-dynamic mt-2">
                                                                                <label>5</label>
                                                                            </div>
                                                                        </div>
                                                                        <div class="form-group col-lg-6 col-md-8 col-9">
                                                                            <asp:DropDownList ID="ddlr5" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" TabIndex="27">
                                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                        <div class="form-group col-lg-2 col-md-2 col-2">--%>
                                                            <%--<asp:Button ID="btnr3" runat="server" Text="+" CssClass="fa fa-plus" aria-hidden="true"/>--%>
                                                            <%--    <asp:LinkButton ID="btnr5" runat="server" CssClass="fa fa-plus" aria-hidden="true" AppendDataBoundItems="true" OnClick="btnr5_Click"></asp:LinkButton>--%>
                                                            <%--<i class="fa fa-plus" aria-hidden="true"></i>--%>
                                                            <%--                                                                        </div>

                                                                    </div>
                                                                </asp:Panel>--%>
                                                            <%-- <asp:Panel ID="pnlr6" runat="server" Visible="false">
                                                                    <div class="row">

                                                                        <div class="form-group col-lg-1 col-md-1 col-1">
                                                                            <div class="label-dynamic mt-2">
                                                                                <label>5</label>
                                                                            </div>
                                                                        </div>
                                                                        <div class="form-group col-lg-6 col-md-8 col-9">
                                                                            <asp:DropDownList ID="ddlr6" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" TabIndex="27">
                                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                        <div class="form-group col-lg-2 col-md-2 col-2">--%>
                                                            <%--<asp:Button ID="btnr3" runat="server" Text="+" CssClass="fa fa-plus" aria-hidden="true"/>--%>
                                                            <%--  <asp:LinkButton ID="btnr6" runat="server" CssClass="fa fa-plus" aria-hidden="true" AppendDataBoundItems="true"></asp:LinkButton>--%>
                                                            <%--<i class="fa fa-plus" aria-hidden="true"></i>--%>
                                                            <%--  </div>

                                                                    </div>
                                                                </asp:Panel>--%>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <%--</ContentTemplate>--%>
                                    <%--  <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnr1" />
                                    </Triggers>--%>
                                    <%--  </asp:UpdatePanel>--%>

                                    <asp:UpdatePanel ID="upnlAnnouncedFor" runat="server">
                                        <ContentTemplate>
                                            <div class="card">
                                                <div class="card-header collapsed" data-toggle="collapse" data-target="#collapseSix" aria-expanded="true">
                                                    <span class="title">Announce for </span>
                                                    <span class="accicon"><i class="fa fa-angle-down rotate-icon"></i></span>
                                                </div>
                                                <div id="collapseSix" class="collapse collapse show">
                                                    <div class="card-body">
                                                        <div class="col-12">
                                                            <div class="row">
                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <label>College / Institute</label>
                                                                    </div>
                                                                    <asp:DropDownList ID="ddlFaculty" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" TabIndex="28" OnSelectedIndexChanged="ddlFaculty_SelectedIndexChanged" AutoPostBack="true">
                                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="ddlFaculty" Display="None"
                                                                        ErrorMessage="Please Select Faculty." InitialValue="0" ValidationGroup="announcement">
                                                                    </asp:RequiredFieldValidator>
                                                                </div>
                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <label>Study Level</label>
                                                                    </div>
                                                                    <asp:DropDownList ID="ddlStudyLevel" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" TabIndex="29" OnSelectedIndexChanged="ddlStudyLevel_SelectedIndexChanged" AutoPostBack="true">
                                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        <%--asp:ListItem Value="1">UG</asp:ListItem>
                                                            <asp:ListItem Value="2">PG</asp:ListItem>--%>
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="ddlStudyLevel" Display="None"
                                                                        ErrorMessage="Please Select Study Level." InitialValue="0" ValidationGroup="announcement">
                                                                    </asp:RequiredFieldValidator>
                                                                </div>

                                                                <div class="form-group col-lg-3 col-md-6 col-12" id="divProgname" runat="server">
                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <label>Program Name</label>
                                                                    </div>
                                                                    <%-- <div id="lstQueriesDiv" style="overflow-y: auto !important; overflow-x: auto !important;
                                                                 Width: 300px; height:167px; " > --%>
                                                                    <asp:ListBox ID="lstbxProgramName" runat="server" CssClass="form-control multi-select-demo" SelectionMode="Multiple" TabIndex="30" Style="max-width: 100%; overflow: hidden; text-overflow: ellipsis;"></asp:ListBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="lstbxProgramName" Display="None"
                                                                        ErrorMessage="Please Select Program Name." InitialValue="" ValidationGroup="announcement">
                                                                    </asp:RequiredFieldValidator>
                                                                    <%-- </div>--%>
                                                                </div>

                                                                <div class="form-group col-lg-2 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <label>Semester</label>
                                                                    </div>
                                                                    <asp:ListBox ID="lstbxSemester" runat="server" CssClass="form-control multi-select-demo" Style="overflow: hidden; text-overflow: ellipsis; display: block;" SelectionMode="Multiple" AppendDataBoundItems="true" TabIndex="31"></asp:ListBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="lstbxSemester" Display="None"
                                                                        ErrorMessage="Please Select Semester." InitialValue="" ValidationGroup="announcement">
                                                                    </asp:RequiredFieldValidator>
                                                                </div>
                                                                <div class="form-group col-lg-1 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <label></label>
                                                                    </div>
                                                                    <%-- <asp:LinkButton ID="btnAdd" runat="server" CssClass="btn btn-outline-info" OnClick="btnAdd_Click" ValidationGroup="announcement" TabIndex="32" UseSubmitBehavior="false" OnClientClick="disableButton()">ADD</asp:LinkButton>--%>
                                                                    <asp:Button ID="btnAdd" runat="server" Text="ADD" CssClass="btn btn-outline-info" ValidationGroup="announcement"
                                                                        OnClick="btnAdd_Click" ToolTip="Click here to Add Announce" TabIndex="7" UseSubmitBehavior="false" OnClientClick="handleButtonClick()" />
                                                                    <%--Shaikh Juned 17-10-2023 disable button on single click--%>

                                                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="announcement" ShowMessageBox="true" ShowSummary="false"
                                                                        DisplayMode="List" />
                                                                </div>
                                                                <div class="form-group col-lg-3 col-md-6 col-12" style="color: Red; font-weight: bold;">
                                                                    Note : File Upload Control Enabled After Adding Annouce Details.
                                                                      (Upload .pdf only-Max size 500 Kb.)
                                                                </div><br />

                                                                <%--  //Added by Parag//29-02-2024--%>

                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <label>Document Upload</label>
                                                                    </div>

                                                                    <asp:FileUpload ID="FileUploadCompany" ToolTip="Select file to upload" runat="server" />
                                                                  
                                                                </div>
                                                                <%-- //Added by Parag//29-02-2024--%>
                                                            </div>

                                                            <div class="col-md-12">
                                                                <%--<asp:UpdatePanel ID="updJobAnn" runat="server">
                                                        <ContentTemplate>--%>

                                                                <asp:ListView ID="lvannouncefor" runat="server">
                                                                    <LayoutTemplate>
                                                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                                            <thead class="bg-light-blue">
                                                                                <tr>
                                                                                    <th>Delete</th>
                                                                                    <%--<th>Edit</th>--%>
                                                                                    <th>FACULTY NAME</th>
                                                                                    <th>STUDY LEVEL</th>
                                                                                    <th>PROGRAM NAME</th>
                                                                                    <th>SEMESTER</th>
                                                                                </tr>
                                                                            </thead>
                                                                            <tbody>
                                                                                <tr id="itemPlaceholder" runat="server" />
                                                                            </tbody>
                                                                        </table>

                                                                    </LayoutTemplate>
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <%--<td>
                                                                    <%# Container.DataItemIndex + 1%>
                                                                </td>--%>
                                                                            <td>
                                                                                <asp:ImageButton ID="btnDelProjDetail" runat="server" AlternateText="Delete Record"
                                                                                    CommandArgument='<%# Eval("SCHEDULENO") %>' ImageUrl="~/images/delete.gif" OnClick="btnDelPDetail_Click" OnClientClick="return confirm('Are you sure you want to Delete Record ?');"
                                                                                    ToolTip="Delete Record" />
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("Faculty Name")%>
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("Study Level")%>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label runat="server" ID="lblpname" Text=' <%# Eval("ProgramFullName")%>'></asp:Label>

                                                                            </td>
                                                                            <td>
                                                                                <asp:Label runat="server" ID="lblsno" Text=' <%# Eval("Semester")%>'></asp:Label>

                                                                            </td>

                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:ListView>
                                                                <%-- </ContentTemplate>
                                                        <Triggers>--%>
                                                                <%-- <asp:PostBackTrigger ControlID="btnAdd" />--%>
                                                                <%--<asp:AsyncPostBackTrigger ControlID="btnAdd"  />
                                                        </Triggers>
                                                    </asp:UpdatePanel>--%>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnAdd" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>

                                <div class="col-12 btn-footer mt-3" runat="server" id="divbtn">
                                    <%-- <asp:LinkButton ID="btnSubmit" runat="server" CssClass="btn btn-outline-info" OnClick="btnSubmit_Click">Submit</asp:LinkButton>--%>
                                    <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-outline-info" OnClick="btnSubmit_Click" Text="Submit" ValidationGroup="jobannouncemennt" />
                                    <asp:LinkButton ID="btnCancel" runat="server" CssClass="btn btn-outline-danger" OnClick="btnCancel_Click">Cancel</asp:LinkButton>
                                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="jobannouncemennt" ShowMessageBox="true" ShowSummary="false"
                                        DisplayMode="List" />
                                </div>

                                <div class="col-12 mt-3">
                                    <%-- <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                    <thead class="bg-light-blue">
                                        <tr>
                                            <th>Edit</th>
                                            <th>Logo</th>
                                            <th>Company Name</th>
                                            <th>Location</th>
                                            <th>Job Type</th>
                                            <th>Job Role</th>
                                            <th>Schedule Date (From-To)</th>
                                            <th>Venue</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>
                                                <asp:LinkButton ID="btnEdit" runat="server" CssClass="fa fa-pencil-square-o"></asp:LinkButton>
                                            </td>
                                            <td class="company-logo">
                                                <img src="IMAGES/SLIIT_logo.png" alt="logo" />
                                            </td>
                                            <td>MastersoftErp Solution Pvt. Ltd.</td>
                                            <td>Nagpur</td>
                                            <td>Job Type</td>
                                            <td>Job Role</td>
                                            <td>18 Jan,2022 - 28 Jan,2022</td>
                                            <td>Srilanka</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:LinkButton ID="LinkButton1" runat="server" CssClass="fa fa-pencil-square-o"></asp:LinkButton>
                                            </td>
                                            <td class="company-logo">
                                                <img src="IMAGES/SLIIT_logo.png" alt="logo" />
                                            </td>
                                            <td>MastersoftErp Solution Pvt. Ltd.</td>
                                            <td>Nagpur</td>
                                            <td>Job Type</td>
                                            <td>Job Role</td>
                                            <td>18 Jan,2022 - 28 Jan,2022</td>
                                            <td>Srilanka</td>
                                        </tr>
                                    </tbody>
                                </table>--%>
                                     <asp:UpdatePanel ID="updJobAnn" runat="server">
                                                        <ContentTemplate>

                                            <div class="col-12">
                                                <asp:ListView ID="lvJobAnnouncement" runat="server">
                                                    <LayoutTemplate>
                                                        <div class="sub-heading">
                                                            <h5>Job Announcement Details</h5>
                                                        </div>
                                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                            <thead class="bg-light-blue">
                                                                <tr>
                                                                    <th>Edit</th>
                                                                    <th>Logo</th>
                                                                    <th>Company Name</th>
                                                                    <th>Location</th>
                                                                    <th>Job Type</th>
                                                                    <th>Job Role</th>
                                                                    <th>Schedule Date (From-To)</th>
                                                                    <th>Venue</th>
                                                                    <th>Download</th>
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
                                                                <asp:ImageButton ID="btnJobAnnouncement" class="btnEditX" runat="server" CausesValidation="false" CssClass="fa fa-pencil-square-o" ImageUrl="~/Images/edit1.png"
                                                                    CommandArgument='<%# Eval("ACOMSCHNO") %>' AlternateText="Edit Record" OnClick="btnEditJobAnnouncement_Click" /><%-- ToolTip='<%# Eval("COMPID") %>'--%>
                                                            </td>
                                                            <td>
                                                                <%--<%# Eval("LOGO")%>--%>
                                                                <%--<asp:Image runat="server" AlternateText="img" ImageUrl='<%# "data:image/jpg;base64," + Convert.ToBase64String((byte[])Eval("LOGO")) %>'  Height="30" Width="30" />--%>
                                                                <%-- <asp:Image ID="Image1" runat="server" ImageUrl='<%# Eval("LOGO") %>' height="20" width="20" />--%>

                                                                <asp:Image ID="imgLogo" runat="server" ImageUrl='<%# String.Format("data:image/jpeg;base64,{0}", Convert.ToBase64String((byte[])Eval("LOGO")) ) %>' Height="30" Width="30" />
                                                            </td>
                                                            <td>
                                                                <%# Eval("COMPNAME")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("CITY")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("JOBTYPE")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("JOBROLETYPE")%>
                                                            </td>
                                                            <td>
                                                                <%# String.Format("{0}, {1}", Eval("INTERVIEWFROM","{0: dd/MM/yyyy}"), Eval("INTERVIEWTO","{0: dd/MM/yyyy}")) %> 
                                                            </td>
                                                            <td>
                                                                <%# Eval("VENUE")%>
                                                            </td>
                                                            <td>
                                                                <%--asp:LinkButton ID="lnkDownloadOffer" runat="server" CssClass="fa fa-download" ></asp:LinkButton>--%>
                                                                <asp:ImageButton ID="imgdownloadComDetails" runat="server" Text="Download" ImageUrl="~/Images/action_down.png" ToolTip='<%# Eval("FileName") %>'
                                                                    CommandArgument='<%# Eval("FileName") %>' OnClick="imgdownloadComDetails_Click"></asp:ImageButton>

                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <AlternatingItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <asp:ImageButton ID="btnEditAddCompany" runat="server" CausesValidation="false" CssClass="fa fa-pencil-square-o" ImageUrl="~/Images/edit1.png"
                                                                    CommandArgument='<%# Eval("ACOMSCHNO") %>' AlternateText="Edit Record" ToolTip='<%# Eval("COMPID") %>' OnClick="btnEditJobAnnouncement_Click" />
                                                            </td>
                                                            <td>
                                                                <%-- <%# Eval("LOGO")%>--%>
                                                                <asp:Image ID="imgLogo" runat="server" ImageUrl='<%# String.Format("data:image/jpeg;base64,{0}", Convert.ToBase64String((byte[])Eval("LOGO")) ) %>' Height="30" Width="30" />
                                                            </td>
                                                            <td>
                                                                <%# Eval("COMPNAME")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("CITY")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("JOBTYPE")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("JOBROLETYPE")%>
                                                            </td>
                                                            <td>
                                                                <%# String.Format("{0}, {1}", Eval("INTERVIEWFROM","{0: dd/MM/yyyy}"), Eval("INTERVIEWTO","{0: dd/MM/yyyy}")) %>
                                                            </td>
                                                            <td>
                                                                <%# Eval("VENUE")%>
                                                            </td>
                                                            <td>
                                                                <%--<asp:LinkButton ID="lnkDownloadOffer" runat="server" CssClass="fa fa-download" ></asp:LinkButton>--%>
                                                                <asp:ImageButton ID="imgdownloadComDetails" runat="server" Text="Download" ImageUrl="~/Images/action_down.png" ToolTip='<%# Eval("FileName") %>'
                                                                    CommandArgument='<%# Eval("FileName") %>' OnClick="imgdownloadComDetails_Click"></asp:ImageButton>

                                                            </td>

                                                        <asp:Image ID="imgLogo" runat="server" ImageUrl='<%# String.Format("data:image/jpeg;base64,{0}", Convert.ToBase64String((byte[])Eval("LOGO")) ) %>' Height="30" Width="30" />
                                                    </td>
                                                    <td>
                                                        <%# Eval("COMPNAME")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("CITY")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("JOBTYPE")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("JOBROLETYPE")%>
                                                    </td>
                                                    <td>
                                                        <%# String.Format("{0}, {1}", Eval("INTERVIEWFROM","{0: dd/MM/yyyy}"), Eval("INTERVIEWTO","{0: dd/MM/yyyy}")) %> 
                                                    </td>
                                                    <td>
                                                        <%# Eval("VENUE")%>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <AlternatingItemTemplate>
                                                <tr>
                                                    <td>
                                                        <asp:ImageButton ID="btnEditAddCompany" runat="server" CausesValidation="false" CssClass="fa fa-pencil-square-o" ImageUrl="~/Images/edit1.png"
                                                            CommandArgument='<%# Eval("ACOMSCHNO") %>' AlternateText="Edit Record" ToolTip='<%# Eval("COMPID") %>' OnClick="btnEditJobAnnouncement_Click" />
                                                    </td>
                                                    <td>
                                                        <%-- <%# Eval("LOGO")%>--%>
                                                        <asp:Image ID="imgLogo" runat="server" ImageUrl='<%# String.Format("data:image/jpeg;base64,{0}", Convert.ToBase64String((byte[])Eval("LOGO")) ) %>' Height="30" Width="30" />
                                                    </td>
                                                    <td>
                                                        <%# Eval("COMPNAME")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("CITY")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("JOBTYPE")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("JOBROLETYPE")%>
                                                    </td>
                                                    <td>
                                                        <%# String.Format("{0}, {1}", Eval("INTERVIEWFROM","{0: dd/MM/yyyy}"), Eval("INTERVIEWTO","{0: dd/MM/yyyy}")) %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("VENUE")%>
                                                    </td>

                                                </tr>
                                            </AlternatingItemTemplate>
                                        </asp:ListView>
                                    </div>



                                            <asp:Label ID="lblBlobConnectiontring" runat="server" Text="" Visible="false"></asp:Label>
                                            <asp:HiddenField ID="hdnBlobCon" runat="server" />
                                            <asp:Label ID="lblBlobContainer" runat="server" Text="" Visible="false"></asp:Label>
                                            <asp:HiddenField ID="hdnBlobContainer" runat="server" />

                                        </ContentTemplate>

                                    </asp:UpdatePanel>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
                <script src='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.js") %>'></script>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSubmit" />
            <asp:PostBackTrigger ControlID="lvJobAnnouncement" />
            <asp:PostBackTrigger ControlID="btnCancel" />
            <asp:PostBackTrigger ControlID="btnRoundAdd" />


        </Triggers>
    </asp:UpdatePanel>


    <!-- Start End Date Script -->
    <script type="text/javascript">
   //     $(document).ready(function () {
   //         debugger;
   //         $('#picker').daterangepicker({
   //             startDate: moment().subtract(00, 'days'),
   //             endDate: moment(),
   //             locale: {
   //                 format: 'DD MMM, YYYY'
   //             },
   //             ranges: {
   //             },
   //         },
   //     function (start, end) {
   //         debugger;
   //         //alert("in");
   //         $('#date').html(start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'));
   //         document.getElementById('<%=txtSchFromDate.ClientID%>').value = (start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'))
   //         var today = new Date();
   //         var yyyy = today.getFullYear();
   //         var mm = today.getMonth() + 1; // Months start at 0!
   //         var dd = today.getDate();
   //
   //         if (dd < 10) dd = '0' + dd;
   //         if (mm < 10) mm = '0' + mm;
   //
   //         var today = dd + '/' + mm + '/' + yyyy;
   //         var startDate = start.format('DD/MM/YYYY');
   //         //alert(today);
   //         //alert(startDate);
   //         var d1 = today.split("/");
   //         var d2 = startDate.split("/");
   //         //alert('today : ' + d1);
   //         //alert('startdate : ' + d2);
   //         var fromCurrent = new Date(d1[2], parseInt(d1[1]) - 1, d1[0]);  // -1 because months are from 0 to 11                
   //         var toStart = new Date(d2[2], parseInt(d2[1]) - 1, d2[0]);
   //         //alert(fromCurrent);
   //         // alert(toStart);
   //         if (toStart < fromCurrent) {
   //             alert("Backdate are not allowed.");
   //             // $('#date').html(start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'));
   //             document.getElementById('<%=txtSchFromDate.ClientID%>').value = startDate = start.format('DD/MM/YYYY');
   //         }
   //     });
   //
   //         $('#date').html(moment().subtract(00, 'days').format('DD MMM, YYYY') + ' - ' + moment().format('DD MMM, YYYY'));
   //         document.getElementById('<%=txtSchFromDate.ClientID%>').value = (moment().subtract(00, 'days').format('DD MMM, YYYY') + ' - ' + moment().format('DD MMM, YYYY'))
   //
   //
   //     });

    </script>
    <%--<script>
        var todayDate = new Date().toISOString().slice(0, 10);
        $('.picker').dateRangePicker({
            autoClose: true,
            format: 'YYYY-MM-DD',
            startDate: todayDate,
        });

    </script>--%>
    <!-- TinyMce Script -->
    <script>
        //Sys.Application.add_load(LoadTinyMCE);
        //var prm = Sys.WebForms.PageRequestManager.getInstance();
        $(document).ready(function () {
            LoadTinyMCE();
        });

    </script>
    <script>
        function LoadTinyMCE() {
            $('.TextBox1').tinymce({
                script_url: ResolveUrl("~/plugins/TinyMce/tinymce.min.js"),
                placeholder: 'Enter the course content here ...',
                height: 300,
                menubar: 'file edit view insert format tools table tc help',
                plugins: [
                  'advlist autolink lists link image charmap print preview anchor',
                  'searchreplace visualblocks code fullscreen',
                  'insertdatetime media table paste code help wordcount'
                ],
                toolbar: 'undo redo | formatselect | ' +
                'bold italic backcolor | alignleft aligncenter ' +
                'alignright alignjustify | bullist numlist outdent indent | ' +
                'removeformat | help',
                content_style: 'body { font-family:Helvetica,Arial,sans-serif; font-size:14px }',
                //encoding: 'xml'
                //init_instance_callback: function (editor) {
                //    editor.on('mouseup', function (e) {
                //        alert('okoko');
                //    });
                //}
            });
        }

        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            function LoadTinyMCE() {
                $('.TextBox1').tinymce({
                    script_url: ResolveUrl("~/plugins/TinyMce/tinymce.min.js"),
                    placeholder: 'Enter the course content here ...',
                    height: 300,
                    menubar: 'file edit view insert format tools table tc help',
                    plugins: [
                      'advlist autolink lists link image charmap print preview anchor',
                      'searchreplace visualblocks code fullscreen',
                      'insertdatetime media table paste code help wordcount'
                    ],
                    toolbar: 'undo redo | formatselect | ' +
                    'bold italic backcolor | alignleft aligncenter ' +
                    'alignright alignjustify | bullist numlist outdent indent | ' +
                    'removeformat | help',
                    content_style: 'body { font-family:Helvetica,Arial,sans-serif; font-size:14px }',
                    //encoding: 'xml'
                    //init_instance_callback: function (editor) {
                    //    editor.on('mouseup', function (e) {
                    //        alert('okoko');
                    //    });
                    //}
                });
            }
        });

    </script>
    <script>
    //   function Setdate(date) {
    //       var prm = Sys.WebForms.PageRequestManager.getInstance();
    //       prm.add_endRequest(function () {
    //           $(document).ready(function () {
    //               debugger;
    //               var startDate = moment(date.split('-')[0], "MMM DD, YYYY");
    //               var endtDate = moment(date.split('-')[1], "MMM DD, YYYY");
    //               //$('#date').html(date);
    //               $('#date').html(startDate.format("MMM DD, YYYY") + ' - ' + endtDate.format("MMM DD, YYYY"));
    //               document.getElementById('<%=txtSchFromDate.ClientID%>').value = date;
    //               //$('#picker').daterangepicker({ startDate: startDate, endDate: endtDate});
    //               $('#picker').daterangepicker({
    //                   startDate: startDate.format("MM/DD/YYYY"),
    //                   endDate: endtDate.format("MM/DD/YYYY"),
    //                   ranges: {
    //                   },
    //               },
    //       function (start, end) {
    //           debugger
    //           $('#date').html(start.format('MMM DD, YYYY') + ' - ' + end.format('MMM DD, YYYY'))
    //           document.getElementById('<%=txtSchFromDate.ClientID%>').value = (start.format('MMM DD, YYYY') + ' - ' + end.format('MMM DD, YYYY'))
    //       });
    //
    //           });
    //       });
    //

    var dateToday = new Date();
    $(function () {
        $("#date").datepicker({
            minDate: dateToday
        });
    });




            //$('.picker').daterangepicker({
            //    autoUpdateInput: false,
            //    locale: {
            //        format: 'DD/MM/YYYY',
            //        cancelLabel: 'Clear'
            //    }
            //});


            //$('.picker').on('apply.daterangepicker', function (ev, picker) {
            //    $(this).val(picker.startDate.format('MM/DD/YYYY') + ' - ' + picker.endDate.format('MM/DD/YYYY'));
            //});

};
    </script>
    <script>
        $('#<%=btnSubmit.ClientID%>').click(function () {
            // alert('a');
            $('#<%=hfdTemplate.ClientID%>').val($('#templateEditor').val().replace('MyLRMar', '').replace('badge', ''));
        });

        $.ajax({
            type: "POST",
            url: "CreateTemplate.aspx/CategoryType",
            data: '{val:"' + val + '"}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (d) {
                debugger
                var data = JSON.parse(d.d);
                var iHtml = "<center>";
                $.each(data, function (a, b) {
                    debugger;
                    iHtml += '<b class="MyLRMar badge" draggable="true" style="font-size:14px;background-color:#abd2e8;padding:7px; color:black;margin-bottom:5px;">[' + b.NAME + ']</b>';
                });


            },
            failure: function (response) {
                alert("Err1");
            },
            error: function (response) {
                alert(response.responseText);
            }
        });


        $('#<%=templateEditor.ClientID%>').change(function () {

            var val = $(this).val().split('^')[1].toString();
            $("#spnSP_NAME").html(val);

            $.ajax({
                type: "POST",
                url: "CreateTemplate.aspx/CategoryType",
                data: '{val:"' + val + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (d) {
                    debugger
                    var data = JSON.parse(d.d);
                    var iHtml = "<center>";
                    $.each(data, function (a, b) {
                        debugger;
                        iHtml += '<b class="MyLRMar badge" draggable="true" style="font-size:14px;background-color:#abd2e8;padding:7px; color:black">[' + b.NAME + ']</b>';
                    });
                },
                failure: function (response) {
                    alert("Err1");
                },
                error: function (response) {
                    alert(response.responseText);
                }
            });
        });

        $('#<%=templateEditor.ClientID%>').change(function () {
            debugger
            ShowLoader();
            tinyMCE.triggerSave();
            $.ajax({
                type: "POST",
                url: "CreateTemplate.aspx/DataList",
                data: '{val:' + parseInt($(this).val()) + '}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (d) {
                    var data = JSON.parse(d.d);
                    var options = "";
                    options += "<option value='0'>Please Select</option>";
                    $.each(data, function (a, b) {
                        options += "<option value=" + b.ID + "^" + b.SP_NAME + ">" + b.NAME + "</option>";
                    });
                    $("#spnSP_NAME").html('');
                    HideLoader();
                },
                failure: function (response) {
                    HideLoader();
                    alert("Err1");
                },
                error: function (response) {
                    HideLoader();
                    alert(response.responseText);
                }
            });
        });

        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $('#<%=templateEditor.ClientID%>').change(function () {
                debugger
                ShowLoader();
                tinyMCE.triggerSave();
                $.ajax({
                    type: "POST",
                    url: "CreateTemplate.aspx/DataList",
                    data: '{val:' + parseInt($(this).val()) + '}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (d) {
                        var data = JSON.parse(d.d);
                        var options = "";
                        options += "<option value='0'>Please Select</option>";
                        $.each(data, function (a, b) {
                            options += "<option value=" + b.ID + "^" + b.SP_NAME + ">" + b.NAME + "</option>";
                        });
                        $("#spnSP_NAME").html('');
                        HideLoader();
                    },
                    failure: function (response) {
                        HideLoader();
                        alert("Err1");
                    },
                    error: function (response) {
                        HideLoader();
                        alert(response.responseText);
                    }
                });
            });
        });

    </script>

    <script>
        $('#<%=btnSubmit.ClientID%>').click(function () {
            // alert('a');
            $('#<%=hfdEligibility.ClientID%>').val($('#txtEligibility').val().replace('MyLRMar', '').replace('badge', ''));
        });

        $.ajax({
            type: "POST",
            url: "CreateTemplate.aspx/CategoryType",
            data: '{val:"' + val + '"}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (d) {
                debugger
                var data = JSON.parse(d.d);
                var iHtml = "<center>";
                $.each(data, function (a, b) {
                    debugger;
                    iHtml += '<b class="MyLRMar badge" draggable="true" style="font-size:14px;background-color:#abd2e8;padding:7px; color:black;margin-bottom:5px;">[' + b.NAME + ']</b>';
                });


            },
            failure: function (response) {
                alert("Err1");
            },
            error: function (response) {
                alert(response.responseText);
            }
        });




        $('#<%=txtEligibility.ClientID%>').change(function () {

            var val = $(this).val().split('^')[1].toString();
            $("#spnSP_NAME").html(val);

            $.ajax({
                type: "POST",
                url: "CreateTemplate.aspx/CategoryType",
                data: '{val:"' + val + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (d) {
                    debugger
                    var data = JSON.parse(d.d);
                    var iHtml = "<center>";
                    $.each(data, function (a, b) {
                        debugger;
                        iHtml += '<b class="MyLRMar badge" draggable="true" style="font-size:14px;background-color:#abd2e8;padding:7px; color:black">[' + b.NAME + ']</b>';
                    });
                },
                failure: function (response) {
                    alert("Err1");
                },
                error: function (response) {
                    alert(response.responseText);
                }
            });
        });

        $('#<%=txtEligibility.ClientID%>').change(function () {
            debugger
            ShowLoader();
            tinyMCE.triggerSave();
            $.ajax({
                type: "POST",
                url: "CreateTemplate.aspx/DataList",
                data: '{val:' + parseInt($(this).val()) + '}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (d) {
                    var data = JSON.parse(d.d);
                    var options = "";
                    options += "<option value='0'>Please Select</option>";
                    $.each(data, function (a, b) {
                        options += "<option value=" + b.ID + "^" + b.SP_NAME + ">" + b.NAME + "</option>";
                    });
                    $("#spnSP_NAME").html('');
                    HideLoader();
                },
                failure: function (response) {
                    HideLoader();
                    alert("Err1");
                },
                error: function (response) {
                    HideLoader();
                    alert(response.responseText);
                }
            });
        });
    </script>

    <script>
        $('#<%=btnSubmit.ClientID%>').click(function () {
            // alert('a');
            $('#<%=hfDescription.ClientID%>').val($('#txtDescription').val().replace('MyLRMar', '').replace('badge', ''));
        });

        $.ajax({
            type: "POST",
            url: "CreateTemplate.aspx/CategoryType",
            data: '{val:"' + val + '"}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (d) {
                debugger
                var data = JSON.parse(d.d);
                var iHtml = "<center>";
                $.each(data, function (a, b) {
                    debugger;
                    iHtml += '<b class="MyLRMar badge" draggable="true" style="font-size:14px;background-color:#abd2e8;padding:7px; color:black;margin-bottom:5px;">[' + b.NAME + ']</b>';
                });


            },
            failure: function (response) {
                alert("Err1");
            },
            error: function (response) {
                alert(response.responseText);
            }
        });




        $('#<%=txtDescription.ClientID%>').change(function () {

            var val = $(this).val().split('^')[1].toString();
            $("#spnSP_NAME").html(val);

            $.ajax({
                type: "POST",
                url: "CreateTemplate.aspx/CategoryType",
                data: '{val:"' + val + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (d) {
                    debugger
                    var data = JSON.parse(d.d);
                    var iHtml = "<center>";
                    $.each(data, function (a, b) {
                        debugger;
                        iHtml += '<b class="MyLRMar badge" draggable="true" style="font-size:14px;background-color:#abd2e8;padding:7px; color:black">[' + b.NAME + ']</b>';
                    });
                },
                failure: function (response) {
                    alert("Err1");
                },
                error: function (response) {
                    alert(response.responseText);
                }
            });
        });

        $('#<%=txtDescription.ClientID%>').change(function () {
            debugger
            ShowLoader();
            tinyMCE.triggerSave();
            $.ajax({
                type: "POST",
                url: "CreateTemplate.aspx/DataList",
                data: '{val:' + parseInt($(this).val()) + '}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (d) {
                    var data = JSON.parse(d.d);
                    var options = "";
                    options += "<option value='0'>Please Select</option>";
                    $.each(data, function (a, b) {
                        options += "<option value=" + b.ID + "^" + b.SP_NAME + ">" + b.NAME + "</option>";
                    });
                    $("#spnSP_NAME").html('');
                    HideLoader();
                },
                failure: function (response) {
                    HideLoader();
                    alert("Err1");
                },
                error: function (response) {
                    HideLoader();
                    alert(response.responseText);
                }
            });
        });
    </script>

    <script>
        $('.btnEditX').click(function () {
            debugger
            $('#templateEditor').val($(this).attr('name').split('$')[6]);
            $('#<%=hfdOperation.ClientID%>').val(3);
            $('#ddlMailType').val($(this).attr('name').split('$')[8]).trigger('change');
            $('#ddlDataType').val($(this).attr('name').split('$')[7]).trigger('change');
            HideLoader();
        });


        $('.btnEditX').click(function () {
            ShowLoader();
            $('#txtDescription').focus();
            $('#txtDescription').val($(this).attr('name').split('$')[0]);
            $('#txtSubject').val($(this).attr('name').split('$')[1]);

            var arr_UserType = new Array();
            for (var i = 0; i < $(this).attr('name').split('$')[2].split(',').length; i++) {
                arr_UserType[i] = $(this).attr('name').split('$')[2].split(',')[i];
            }
            $('.select2UserType').val(arr_UserType).trigger('change');

            var arr_Page = new Array();
            for (var i = 0; i < $(this).attr('name').split('$')[3].split(',').length; i++) {
                arr_Page[i] = $(this).attr('name').split('$')[3].split(',')[i];
            }
            $('.select2Page').val(arr_Page).trigger('change');

            $('#ddlCategoty').val($(this).attr('name').split('$')[4]).trigger('change');

            $('#templateEditor').val($(this).attr('name').split('$')[6]);
            $('#<%=hfdOperation.ClientID%>').val(3);
            $('#txtDescription').val($(this).attr('name').split('$')[8]).trigger('change');
            $('#txtDescription').val($(this).attr('name').split('$')[7]).trigger('change');
            HideLoader();
        });
    </script>
    <script>
        function validateRoundDetails() {

            var ddljobtype = $("[id$=ddlCompanyName]").attr("id");
            var ss = document.getElementById('<%=ddlCompanyName.ClientID%>').value;

            if (ss == '0') {

                alert('Please Select Company Name.', 'Warning!');
                $(ddljobtype).focus();
                return false;
            }

            var round = $("[id$=ddlJobType]").attr("id");
            var rr = document.getElementById('<%=ddlJobType.ClientID%>').value;

            if (rr == '0') {

                alert('Please Select Job Type.', 'Warning!');
                $(round).focus();
                return false;
            }

            var jobrole = $("[id$=ddlJobRole]").attr("id");
            var jb = document.getElementById('<%=ddlJobRole.ClientID%>').value;

            if (jb == '0') {

                alert('Please Select Job Role.', 'Warning!');
                $(jobrole).focus();
                return false;
            }

            var place = $("[id$=ddlPlacement]").attr("id");
            var pll = document.getElementById('<%=ddlPlacement.ClientID%>').value;

            if (pll == '0') {
                alert('Please Select Placement Mode.', 'Warning!');
                $(place).focus();
                return false;
            }

            var ListBox = document.getElementById('<%=lstbxCity.ClientID %>');
            var length = ListBox.length;
            var i = 0;
            var SelectedItemCount = 0;

            for (i = 0; i < length; i++) {
                if (ListBox.options[i].selected) {
                    SelectedItemCount = SelectedItemCount + 1;
                }

                if (SelectedItemCount == 0) {
                    alert('Please Select City.');
                    return false;
                }
            }


            var ven = $("[id$=txtVenue]").attr("id");
            var venue = document.getElementById('<%=txtVenue.ClientID%>').value;

            if (venue == '') {

                alert('Please Enter Venue.', 'Warning!');
                $(venue).focus();
                return false;
            }


            var last = $("[id$=txtLastDate]").attr("id");
            var date = document.getElementById('<%=txtLastDate.ClientID%>').value;

             if (date == '') {

                alert('Please Select Last Date.', 'Warning!');
                $(date).focus();
                return false;
            }

            var dis = document.getElementById('<%=hfdTemplate.ClientID%>').value
            if (dis == '') {
                alert('Please Enter Description.', 'Warning!');
                return false;
            }

            var eligibility = document.getElementById('<%=hfdEligibility.ClientID%>').value
            if (eligibility == '') {
                alert('Please Enter Eligibility.', 'Warning!');
                return false;
            }

            var amt = $("[id$=txtAmount]").attr("id");
            var a = document.getElementById('<%=txtAmount.ClientID%>').value;

            if (a == '') {

                alert('Please Enter Amount.', 'Warning!');
                $(amt).focus();
                return false;
            }


            var minamt = $("[id$=txtMinAmount]").attr("id");
            var mina = document.getElementById('<%=txtMinAmount.ClientID%>').value;

            if (mina == '') {

                alert('Please Enter Min Amount.', 'Warning!');
                $(minamt).focus();
                return false;
            }

            var maxamt = $("[id$=txtMaxAmount]").attr("id");
            var maxa = document.getElementById('<%=txtMaxAmount.ClientID%>').value;

             if (maxa == '') {

                 alert('Please Enter Max Amount.', 'Warning!');
                 $(maxamt).focus();
                 return false;
             }

             var currency = $("[id$=ddlCurrency]").attr("id");
             var rrr = document.getElementById('<%=ddlCurrency.ClientID%>').value;

            if (rrr == '0') {

                alert('Please Select Currency.', 'Warning!');
                $(currency).focus();
                return false;
            }


            var int = $("[id$=ddlInterval]").attr("id");
            var i = document.getElementById('<%=ddlInterval.ClientID%>').value;

            if (i == '0') {

                alert('Please Select Interval.', 'Warning!');
                $(int).focus();
                return false;
            }
        }


        function validateannouncementfor() {


            var ddlfac = $("[id$=ddlFaculty]").attr("id");
            var fac = document.getElementById('<%=ddlFaculty.ClientID%>').value;

             if (fac == '0') {

                alert('Please Select Faculty.', 'Warning!');
                $(fac).focus();
                return false;
            }


            var stuOGevel = $("[id$=ddlStudyLevel]").attr("id");
            var studl = document.getElementById('<%=ddlStudyLevel.ClientID%>').value;

            if (studl == '0') {

                alert('Please Select Study Level.', 'Warning!');
                $(stulevel).focus();
                return false;
            }

            var ListBox = document.getElementById('<%=lstbxProgramName.ClientID %>');
            var length = ListBox.length;
            var i = 0;
            var SelectedItemCount = 0;

             var stuOGevel = $("[id$=ddlStudyLevel]").attr("id");
             var studl = document.getElementById('<%=ddlStudyLevel.ClientID%>').value;

            if (studl == '0') {

                alert('Please Select Study Level.', 'Warning!');
                $(stulevel).focus();
                return false;
            }

            var ListBox = document.getElementById('<%=lstbxSemester.ClientID %>');
            var length = ListBox.length;
            var i = 0;
            var SelectedItemCount = 0;

            for (i = 0; i < length; i++) {
                if (ListBox.options[i].selected) {
                    SelectedItemCount = SelectedItemCount + 1;
                }

                if (SelectedItemCount == 0) {
                    alert('Please Select Semester Name.');
                    return false;
                }
            }

        }


    </script>
    <script>
        function validateNumeric(txt) {
            if (isNaN(txt.value)) {
                txt.value = '';
                alert('Only Numeric Characters Allowed!');
                txt.focus();
                return;
            }
        }
    </script>
    <script>
        function alphaOnly(e) {
            var code;
            if (!e) var e = window.event;

            if (e.keyCode) code = e.keyCode;
            else if (e.which) code = e.which;

            if ((code >= 48) && (code <= 57)) { return false; }
            return true;
        }
    </script>

    <script type="text/javascript">
        function SelectDate(e) {
            //  alert("a");
            var chkdate = document.getElementById("txtLastDate").value;
            var edate = chkdate.split("/");
            var spdate = new Date();
            var sdd = spdate.getDate();
            var smm = spdate.getMonth();
            var syyyy = spdate.getFullYear();
            var today = new Date(syyyy, smm, sdd).getTime();
            var e_date = new Date(edate[2], edate[1] - 1, edate[0]).getTime();
            if (e_date < today) {
                alert("You Cannot Select Past Date");
                document.getElementById('txtLastDate').value = " ";
                return false;
            }

        }
    </script>
    <script>

        //$(function pastdate() {

        function pastdate() {
            // $(function () {
            var today = new Date();
            var month = ('0' + (today.getMonth() + 1)).slice(-2);
            var day = ('0' + today.getDate()).slice(-2);
            var year = today.getFullYear();
            var date = year + '-' + month + '-' + day;
            $('[id*=txtLastDate]').attr('min', date);
            // });
        }

        //$(function () {
        //    var today = new Date();
        //    var month = ('0' + (today.getMonth() + 1)).slice(-2);
        //    var day = ('0' + today.getDate()).slice(-2);
        //    var year = today.getFullYear();
        //    var date = year + '-' + month + '-' + day;
        //    $('[id*=picker]').attr('min', date);
        //});
        //});
        //            $(function () {
        //                alert("A");
        //    var dtToday = new Date();

        //    var month = dtToday.getMonth() + 1;
        //    var day = dtToday.getDate();
        //    var year = dtToday.getFullYear();

        //    if(month < 10)
        //        month = '0' + month.toString();
        //    if(day < 10)
        //        day = '0' + day.toString();

        //    var maxDate = year + '-' + month + '-' + day;    
        //    $('#txtLastDate').attr('max', maxDate);
        //});
    </script>
    <script>

        var start = new Date();
        var end = new Date(start.getFullYear(), start.getMonth(), start.getDate() + 10);
        $("#picker").kendoDateRangePicker({

            range: [start, end],
            disableDates: ["we", "th"],
        });


        $(function () {
            $('input[name="picker"]').daterangepicker({

                timePicker: true,
                startDate: moment().startOf('hour'),
                endDate: moment().startOf('hour').add(32, 'hour'),
                locale: {
                    format: 'M/DD hh:mm A'
                }
            });
        });

    </script>

    <script>
        function handleButtonClick() {
            var button = document.getElementById('<%= btnAdd.ClientID %>');

            // Disable the button and update text
            button.disabled = true;
            button.value = "Wait...";

            // Enable the button after 10 seconds
            setTimeout(function () {
                button.disabled = false;
                button.value = "Add";
            }, 5000); // 10000 milliseconds = 10 seconds
        }
    </script>

    <script type="text/javascript">
        $(document).ready(function () {
            $('.multi-select-demo').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200,
                enableFiltering: true,
                filterPlaceholder: 'Search',
                enableCaseInsensitiveFiltering: true,
            });
        });
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                $('.multi-select-demo').multiselect({
                    includeSelectAllOption: true,
                    maxHeight: 200,
                    enableFiltering: true,
                    filterPlaceholder: 'Search',
                    enableCaseInsensitiveFiltering: true,
                });
            });
        });
    </script>

</asp:Content>

