<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ApplicationProcessNewGridview.aspx.cs" Inherits="ACADEMIC_POSTADMISSION_ApplicationProcessNewGridview" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="https://cdnjs.cloudflare.com/ajax/libs/FileSaver.js/2014-11-29/FileSaver.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/xlsx/0.12.13/xlsx.full.min.js"></script>
    <style>
        #tblData1_info {
            display: none;
        }

        .input-group .input-group-addon {
            padding: 0px 0px;
        }

        .table-responsive > .table-bordered {
            border: 1px solid #e5e5e5;
        }

        .dataTables_scrollHeadInner {
            width: max-content !important;
        }

        .appli-proc {
            font-weight: 600;
            font-size: 14px;
        }
    </style>

    <style>
        .FixedHeader {
            position: sticky;
            top: -2px;
            z-index: 1;
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
    </style>
    <script type="text/javascript" src="https://cdn.datatables.net/1.10.20/js/jquery.dataTables.min.js"></script>
    <link href="https://cdn.datatables.net/1.10.20/css/jquery.dataTables.css" rel="stylesheet" type="text/css" />

    <div>
        <asp:UpdateProgress ID="UpdateProgress4" runat="server"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div id="preloader">
                    <div id="loader-img">
                        <div id="loader">
                        </div>
                        <p class="saving">Loading<span>.</span><span>.</span><span>.</span></p>
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <asp:UpdatePanel ID="updApplicationProcess" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <%--<h3 class="box-title">APPLICATION PROCESS NEW</h3>--%>
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label>
                            </h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Admission Batch</label>
                                        </div>
                                        <asp:DropDownList ID="ddlAdmBatch" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="1"
                                            AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlAdmBatch_SelectedIndexChanged">
                                            <%-- onchange="onchangeAdmbatch()--%>
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="frvAdmBatch" runat="server" ControlToValidate="ddlAdmBatch" ValidationGroup="show"
                                            ErrorMessage="Please Select Admission Batch" Display="None" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Applicant Type</label>
                                        </div>
                                        <asp:DropDownList ID="ddlApplicantType" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="2"
                                            AppendDataBoundItems="true" OnSelectedIndexChanged="ddlApplicantType_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <asp:ListItem Value="1">UG</asp:ListItem>
                                            <asp:ListItem Value="2">PG</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvApplType" runat="server" ControlToValidate="ddlApplicantType" ValidationGroup="show"
                                            ErrorMessage="Please Select Applicant Type" Display="None" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Degree - Program</label>
                                        </div>
                                        <asp:DropDownList ID="ddlDegreeProgram" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="3"
                                            AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlDegreeProgram_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvDegProgram" runat="server" ControlToValidate="ddlDegreeProgram" ValidationGroup="show"
                                            ErrorMessage="Please Select Degree - Program" Display="None" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divCurrentAppStage" style="display: none">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Current Application Stage</label>
                                        </div>
                                        <asp:DropDownList ID="ddlCurrentAppStage" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="4"
                                            AppendDataBoundItems="true" OnSelectedIndexChanged="ddlCurrentAppStage_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <%--    <asp:RequiredFieldValidator ID="rfvAppStage" runat="server" ControlToValidate="ddlCurrrentAppStage" ValidationGroup="show"
                                    ErrorMessage="Please Select Current Application Stage" Display="None" InitialValue="0"></asp:RequiredFieldValidator>--%>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divToAppStage" style="display: none">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>To Application Stage</label>
                                        </div>
                                        <asp:DropDownList ID="ddlToAppStage" runat="server" CssClass="form-control" data-select2-enable="true"
                                            TabIndex="5" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlToAppStage_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divScheduleSlot" style="display: none" runat="server">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Scheduled Slot</label>
                                        </div>
                                        <asp:DropDownList ID="ddlScheduleSlot" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="7" AppendDataBoundItems="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divFaculty" style="display: none" runat="server">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Verified By</label>
                                        </div>
                                        <asp:DropDownList ID="ddlFaculty" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="7" AppendDataBoundItems="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divCommunication" runat="server" style="display: none">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <asp:CheckBox ID="chkCommunication" runat="server" onchange="showDivEmailSms();" />
                                            <label>Communication</label>
                                        </div>
                                    </div>

                                    <div class="col-lg-9 col-md-9 col-12">
                                        <%-- id="divEmailTemplate" runat="server" style="display: none"--%>
                                        <div class="row">
                                            <div class="form-group col-lg-4 col-md-6 col-12" id="divEmailSMS" runat="server">

                                                <asp:CheckBox ID="chkEmail" runat="server" Text="Email" onchange="showDivTemplate();" />
                                                &nbsp;&nbsp;
                                             <asp:CheckBox ID="chkSMS" runat="server" Text="SMS" />
                                                &nbsp;&nbsp;
                                                <asp:CheckBox ID="chkWhatsApp" runat="server" Text="Whats App" onchange="showDivTemplate();" />
                                            </div>
                                            <div class="form-group col-lg-4 col-md-6 col-12" id="divTemplateType" runat="server" style="display: none">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Template Type</label>
                                                </div>
                                                <asp:DropDownList ID="ddlTemplateType" runat="server" AppendDataBoundItems="true" TabIndex="1" CssClass="form-control"
                                                    data-select2-enable="true" OnSelectedIndexChanged="ddlTemplateType_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    <asp:ListItem></asp:ListItem>
                                                </asp:DropDownList>

                                            </div>
                                            <div class="form-group col-lg-4 col-md-6 col-12" id="divTemplateName" runat="server" style="display: none">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Template Name</label>
                                                </div>
                                                <asp:DropDownList ID="ddlTemplateName" runat="server" AppendDataBoundItems="true" TabIndex="1" CssClass="form-control" data-select2-enable="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    <asp:ListItem></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnSendEmail" runat="server" class="btn btn-primary" Text="Communicate" OnClick="btnSendEmail_Click" Style="display: none" />
                                    <%-- Style="display: none" --%>
                                    <asp:Button ID="btnSubmit" runat="server" class="btn btn-primary" Text="Submit" OnClick="btnSubmit_Click" ValidationGroup="show" />
                                    <asp:Button ID="btnCancel" runat="server" class="btn btn-warning" Text="Cancel" OnClick="btnCancel_Click" />
                                    <asp:ValidationSummary ID="vsshow" runat="server" ValidationGroup="show" ShowMessageBox="true" DisplayMode="List" ShowSummary="false" />

                                </div>
                              
                                <div class="col-12" id="divgvApplicationProcess" runat="server" style="display: none;">
                                    <div class="offset-lg-9 col-md-3">
                                        <div class="input-group sea-rch mb-2">
                                            <input type="text" id="FilterData" class="form-control" placeholder="Search" />
                                            <div class="input-group-addon d-none">
                                                <i class="fa fasss-search"></i>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="table-responsive" style="height: 400px;">
                                        <asp:GridView ID="gvApplicationProcess" runat="server" AutoGenerateColumns="false" Style="border: 1px solid #E5E5E5; width: 100%;"
                                            HeaderStyle-CssClass="table table-bordered table-hover FixedHeader" ForeColor="#000">
                                            <AlternatingRowStyle CssClass="table-striped" BackColor="#e8effa" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Select">
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="chkAll" runat="server" onchange="CheckAll1(this)" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkStud" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Sr No">
                                                    <ItemTemplate>
                                                        <%#Eval("SRNO") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Verification">
                                                    <ItemTemplate>
                                                        <div style="text-align: center;">
                                                            <asp:UpdatePanel ID="updgvverify" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:Button ID="btnVerify1" runat="server" ControlStyle-CssClass="btn btn-outline-primary" Text="Verify" OnClick="btnVerify_Click" CommandArgument='<%#Eval("Userno") %>' />
                                                                </ContentTemplate>
                                                                <Triggers>
                                                                    <asp:PostBackTrigger ControlID="btnVerify1" />
                                                                </Triggers>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Application Id" ControlStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblApplicationId" runat="server" Text='<%#Eval("APPLICATION_ID") %>'></asp:Label>
                                                        <asp:HiddenField ID="hdnuserno" runat="server" Value='<%#Eval("Userno") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Candidate Name" ControlStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <a href='<%# Eval("LOGIN_URL")%>?Userno=<%# Eval("Userno")%>&UA_TYPE=<%# Eval("UA_TYPE")%>&UA_NO=<%# Eval("UA_NO")%>' target="_blank"><%#Eval("FULLNAME") %></a>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Candidate Name" ControlStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFullName" runat="server" Text='<%#Eval("FULLNAME") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Mobile No" ControlStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMobileNo" runat="server" Text='<%#Eval("MOBILENO") %>'></asp:Label></td>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Email Id" ControlStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEmail" runat="server" Text='<%#Eval("EMAILID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:BoundField DataField="APPLICATION_CATEGORY" HeaderText="Application Category" />
                                                <asp:BoundField DataField="STAGENAME" HeaderText="Stage Name" />
                                                <asp:TemplateField HeaderText="Preview" ControlStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <div style="text-align: center;">
                                                            <asp:UpdatePanel ID="updgvpreview" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:ImageButton ID="btnPreview2" runat="server" CssClass="text-center" OnClick="btnPreviewForm_Click" CommandArgument='<%# Eval("Userno") %>'
                                                                        ImageUrl="~/Images/view2.png" />
                                                                </ContentTemplate>
                                                                <Triggers>
                                                                    <asp:PostBackTrigger ControlID="btnPreview2" />
                                                                </Triggers>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Download">
                                                    <ItemTemplate>
                                                        <div style="text-align: center;">
                                                            <asp:UpdatePanel ID="updgvdownload" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:ImageButton ID="btnDownload" runat="server" CssClass="text-center" OnClick="btnDownload_Click" CommandArgument='<%# Eval("Userno")+","+Eval("APPLICATION_ID") %>'
                                                                        ImageUrl="~/Images/action_down.png" />
                                                                </ContentTemplate>
                                                                <Triggers>
                                                                    <asp:PostBackTrigger ControlID="btnDownload" />
                                                                </Triggers>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="ExamSchedule" HeaderText="Scheduled Slot" Visible="false" />
                                                <asp:BoundField DataField="VERIFY_STATUS" HeaderText="Verify Status" />
                                            </Columns>
                                             <FooterStyle BackColor="#990000" Font-Bold="True"
                                                ForeColor="#000" />
                                            <HeaderStyle BackColor="white"
                                                ForeColor="#5b5b5b" />
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>

    </asp:UpdatePanel>
    <!-- The Modal- Preview Form Button In ListView -->
    <div class="modal" id="modalDetails" data-backdrop="false" style="background-color: rgba(0,0,0,0.6);">
        <div class="modal-dialog modal-xl">
            <div class="modal-content">

                <!-- Modal Header -->
                <div class="modal-header">
                    <h4 class="modal-title">Applicant Details</h4>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>

                <!-- Modal body -->
                <div class="modal-body pl-0 pr-0 pl-lg-2 pr-lg-2" style="overflow: scroll; height: 450px;">
                    <div class="col-12">
                        <div class="row">
                            <div class="col-12">
                                <div class="float-right">
                                    <asp:Label ID="app" runat="server" Text="Applicant ID:" Font-Bold="true"></asp:Label>
                                    <asp:Label ID="lblAppId" runat="server" Font-Bold="true"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Panel ID="pnlPanel1" runat="server"></asp:Panel>
                            <p id="PPreviewVerify" runat="server" style="overflow: scroll; height: 500px;"></p>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <!-- Modal footer -->
                <div class="modal-footer">
                    <button type="button" class="btn btn-outline-danger" data-dismiss="modal">Close</button>
                </div>

            </div>
        </div>
    </div>
    <!-- The Modal- for Document verification -->
    <div class="modal" id="ModelVerification" data-backdrop="false" style="background-color: rgba(0,0,0,0.6);">
        <div class="modal-dialog modal-xl">
            <div class="modal-content">
                <asp:UpdatePanel ID="updBindModel" runat="server">
                    <ContentTemplate>
                        <!-- Modal Header -->
                        <div class="modal-header">
                            <h4 class="modal-title">Document Verification</h4>
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                        </div>

                        <!-- Modal body -->
                        <div class="modal-body pl-0 pr-0 pl-lg-2 pr-lg-2" style="overflow: scroll; height: 450px;">
                            <div class="col-12">
                                <div class="row">
                                    <div class="col-12">
                                        <div class="float-right">
                                            <asp:Label ID="Label1" runat="server" Text="Candidate Name :" Font-Bold="true"></asp:Label>
                                            <asp:Label ID="lblCandidateName" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-5 col-md-6 col-12" style="overflow: scroll; height: 520px;">
                                        <div>
                                            <div class="accordion" id="StudentDetails">
                                                <%-- Applied Program Section start --%>
                                                <asp:Panel ID="pnlAppliedProgram" runat="server">
                                                    <div class="card">
                                                        <div class="card-header collapsed" data-toggle="collapse" data-target="#collapseOne" aria-expanded="true" aria-controls="collapseOne">
                                                            <span class="title">Applied Programme </span>
                                                            <span class="accicon"><i class="fa fa-angle-down rotate-icon"></i></span>
                                                        </div>
                                                        <div id="collapseOne" class="collapse" data-parent="#StudentDetails">
                                                            <div class="card-body">
                                                                <div class="col-12">
                                                                    <asp:ListView ID="lvProgramme" runat="server" Visible="false">
                                                                        <LayoutTemplate>
                                                                            <div class="vista-grid" style="width: 100%;">
                                                                                <div class="table table-responsive">
                                                                                    <table class="table table-striped table-bordered nowrap" id="myTable">
                                                                                        <thead class="bg-light-blue">
                                                                                            <tr>
                                                                                                <th>Programme Branch</th>
                                                                                                <th>Application ID</th>
                                                                                                <th>Programme Preference</th>
                                                                                                <th>Application Category</th>
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
                                                                            <tr class="item">
                                                                                <td><%#Eval ("PROGRAMNAME") %></td>
                                                                                <td><%# Eval("APPLICATION_ID")%></td>
                                                                                <td><%# Eval("BRPREF")%></td>
                                                                                <td>
                                                                                    <asp:Label runat="server" ID="lblcategory" Text='<%# Eval("IS_NRI")%>'></asp:Label></td>
                                                                            </tr>
                                                                        </ItemTemplate>
                                                                    </asp:ListView>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                                <%-- Applied Program Section end --%>
                                                <%--Personal Details Section start --%>
                                                <asp:Panel ID="pnlPersonalDetails" runat="server">
                                                    <div class="card">
                                                        <div class="card-header collapsed" data-toggle="collapse" data-target="#collapseTwo" aria-expanded="false" aria-controls="collapseTwo">
                                                            <span class="title" id="headingT">Personal Details</span>
                                                            <span class="accicon"><i class="fa fa-angle-down rotate-icon"></i></span>
                                                        </div>
                                                        <div id="collapseTwo" class="collapse" data-parent="#StudentDetails">
                                                            <div class="card-body">
                                                                <div class="col-12">
                                                                    <div class="row mt-3">
                                                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                                                            <label>Candidate's Full Name</label>
                                                                            <asp:TextBox ID="txtFullName" runat="server" TabIndex="1" ToolTip="Please Enter Candidate's Full Name" AutoComplete="off"
                                                                                MaxLength="50" CssClass="form-control" onkeypress="return alphaOnly(event);" onkeyup="ToUpper(this)" Enabled="false" />
                                                                            <%--(As per 10th Certificate)--%>
                                                                        </div>

                                                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                                                            <label>First Name</label>
                                                                            <asp:TextBox ID="txtFirstName" runat="server" TabIndex="1" ToolTip="Please Enter First Name" AutoComplete="off"
                                                                                MaxLength="50" CssClass="form-control" onkeypress="return alphaOnly(event);" onkeyup="ToUpper(this)" Enabled="false" />
                                                                            <%--(As per 10th Certificate)--%>
                                                                        </div>

                                                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                                                            <label>Last Name</label>
                                                                            <asp:TextBox ID="txtLastName" runat="server" TabIndex="1" ToolTip="Please Enter Last Name" AutoComplete="off"
                                                                                MaxLength="50" CssClass="form-control" onkeypress="return alphaOnly(event);" onkeyup="ToUpper(this)" Enabled="false" />
                                                                            <%--(As per 10th Certificate)--%>
                                                                        </div>
                                                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                                                            <label>DOB<span style="font-size: xx-small">(dd/mm/yyyy) </span></label>
                                                                            <asp:TextBox ID="txtDateOfBirth" runat="server" TabIndex="1" AutoComplete="off" CssClass="form-control" ValidationGroup="regsubmit" ToolTip="Please Enter Date Of Birth" placeholder="DD/MM/YYYY"></asp:TextBox>
                                                                            <ajaxToolkit:MaskedEditExtender ID="meeAdmDt" runat="server" TargetControlID="txtDateOfBirth"
                                                                                Mask="99/99/9999" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True"
                                                                                CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                                CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                                CultureTimePlaceholder="" Enabled="True" />
                                                                        </div>
                                                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                                                            <label>Gender</label><br />
                                                                            <asp:RadioButton ID="rbMale" runat="server" Text="Male" TabIndex="1" GroupName="check" />&nbsp;&nbsp;
                                            <asp:RadioButton ID="rbFemale" runat="server" Text="Female" TabIndex="1" GroupName="check" />
                                                                            <asp:RadioButton ID="rbTransGender" runat="server" Text="TransGender" TabIndex="1" GroupName="check" />
                                                                        </div>
                                                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                                                            <label>Mobile</label><br />
                                                                            <div class="input-group">
                                                                                <div class="input-group-addon" style="width: 20%">
                                                                                    <asp:TextBox ID="txtmobilecode" runat="server" CssClass="form-control"
                                                                                        MaxLength="3" AutoComplete="off" Enabled="false" />
                                                                                    <asp:RequiredFieldValidator ID="rfvmobilecode" runat="server" ErrorMessage="Country Code Required"
                                                                                        ControlToValidate="txtmobilecode" Display="None" ValidationGroup="regsubmit"></asp:RequiredFieldValidator>

                                                                                </div>
                                                                                <asp:TextBox ID="txtMobile" runat="server" onkeyup="validateNumeric(this)"
                                                                                    MaxLength="10" ToolTip="Please Enter Mobile No." CssClass="form-control" />
                                                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtMobile"
                                                                                    ValidChars="1234567890" FilterMode="ValidChars" />
                                                                            </div>
                                                                        </div>
                                                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                                                            <label>Email</label>
                                                                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TabIndex="1" ToolTip="Please Enter Email" AutoComplete="off" />

                                                                        </div>
                                                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                                                            <label>Alternate Email ID</label>
                                                                            <asp:TextBox ID="txtAlternateEmailId" runat="server" ToolTip="Please Enter Alternate Email ID"
                                                                                TabIndex="1" CssClass="form-control" Placeholder="Please Enter Email ID" AutoComplete="off" />

                                                                        </div>
                                                                        <div id="Div3" class="form-group col-lg-4 col-md-6 col-12" runat="server" visible="false">
                                                                            <label>Religion</label>
                                                                            <asp:DropDownList runat="server" ID="ddlReligion" AppendDataBoundItems="true"
                                                                                TabIndex="1" ToolTip="Please Select Religion" CssClass="form-control">
                                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                            <asp:TextBox ID="txtReligion" runat="server" TabIndex="1" onkeypress="return alphaOnly(event);"
                                                                                ToolTip="Please Enter Religion" Visible="false" MaxLength="25" CssClass="form-control" AutoComplete="off" />
                                                                        </div>
                                                                        <div id="Div6" class="form-group col-lg-4 col-md-6 col-12" runat="server">
                                                                            <label>Category</label>
                                                                            <asp:DropDownList runat="server" ID="ddlSocCategory" AppendDataBoundItems="true"
                                                                                TabIndex="1" ToolTip="Please Select Social Category" CssClass="form-control" Enabled="false">
                                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                                                            <label>Nationality</label>
                                                                            <asp:DropDownList runat="server" ID="ddlNationality" AppendDataBoundItems="true"
                                                                                ToolTip="Please Select Nationality" CssClass="form-control" TabIndex="1">
                                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                                                            <label>State of Domicile</label>
                                                                            <asp:DropDownList runat="server" ID="ddlStateDomicile" AppendDataBoundItems="true"
                                                                                TabIndex="1" ToolTip="Please Select State of Domicile" CssClass="form-control">
                                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                                                            <label>Place Of Residency</label>
                                                                            <asp:DropDownList runat="server" ID="ddlresidency" AppendDataBoundItems="true"
                                                                                TabIndex="1" ToolTip="Please Select State of Domicile" CssClass="form-control" Enabled="false">
                                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                                <asp:ListItem Value="C">City</asp:ListItem>
                                                                                <asp:ListItem Value="T">Town</asp:ListItem>
                                                                                <asp:ListItem Value="V">Village</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                        <div id="Div7" class="form-group col-lg-4 col-md-6 col-12" runat="server" visible="false">
                                                                            <label>Blood Group</label>
                                                                            <asp:DropDownList runat="server" ID="ddlBloodGroup" AppendDataBoundItems="true"
                                                                                TabIndex="1" ToolTip="Please Select Blood Group" CssClass="form-control">
                                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                                                            <label>Identification Mark</label>
                                                                            <asp:TextBox ID="txtIdentificationMark" runat="server" TabIndex="1" AutoComplete="off"
                                                                                CssClass="form-control" ToolTip="Please Enter Identification Mark" placeholder="Please Enter Identification Mark" MaxLength="50" />
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender15" runat="server" TargetControlID="txtIdentificationMark"
                                                                                InvalidChars="1234567890~`!@#$%^&*_-+={[}]|\:;'<,>.?" FilterMode="InvalidChars" />
                                                                        </div>
                                                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                                                            <label>Aadhar No</label>
                                                                            <asp:TextBox ID="txtAdhaarNo" runat="server" TabIndex="1" CssClass="form-control"
                                                                                ToolTip="Please Enter Aadhar No." MaxLength="12" placeholder="Please Enter Aadhar No." AutoComplete="off" />
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="ftAdharNo" runat="server" TargetControlID="txtAdhaarNo"
                                                                                ValidChars="1234567890" FilterMode="ValidChars" />
                                                                        </div>
                                                                        <div id="Div11" class="form-group col-lg-4 col-md-6 col-12" runat="server" visible="false">
                                                                            <label>Marital Status</label>
                                                                            <asp:DropDownList runat="server" ID="ddlMarital" AppendDataBoundItems="true"
                                                                                TabIndex="1" ToolTip="Please Select Marital Status" CssClass="form-control">
                                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                                <asp:ListItem Value="1">Married</asp:ListItem>
                                                                                <asp:ListItem Value="2">Unmarried</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                        <div id="Div12" class="form-group col-lg-4 col-md-6 col-12" runat="server" visible="false">
                                                                            <label>Are You Differently Abled?</label>
                                                                            <asp:DropDownList runat="server" ID="ddlDiffAbility" AppendDataBoundItems="true"
                                                                                TabIndex="1" ToolTip="Please Select Are You Differently Abled" CssClass="form-control">
                                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                                                                <asp:ListItem Value="2">No</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                        <div class="form-group col-lg-8 col-md-12 col-12" id="Abilility" runat="server" visible="false">
                                                                            <div class="row">
                                                                                <div class="form-group col-lg-6 col-md-6 col-12">
                                                                                    <label>Nature of Disability</label>
                                                                                    <asp:TextBox ID="txtNatureOfDisability" runat="server" TabIndex="1" CssClass="form-control"
                                                                                        ToolTip="Please Nature of Disability" AutoComplete="off" />
                                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="ajaxDisable" runat="server" TargetControlID="txtNatureOfDisability" FilterMode="InvalidChars"
                                                                                        InvalidChars="`~!@#$%^&*-_+={[}]|\:;'<,>.?1234567890">
                                                                                    </ajaxToolkit:FilteredTextBoxExtender>
                                                                                </div>
                                                                                <div class="form-group col-lg-6 col-md-6 col-12">
                                                                                    <label>Percentage(%) of Disability</label>
                                                                                    <asp:TextBox ID="txtPercentageOfDisability" runat="server" TabIndex="1" CssClass="form-control"
                                                                                        ToolTip="Please Enter Percentage of Disability" MaxLength="3" AutoComplete="off" />
                                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server" TargetControlID="txtPercentageOfDisability"
                                                                                        ValidChars="1234567890" FilterMode="ValidChars" />
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                        <div id="Div13" class="form-group col-lg-4 col-md-6 col-12" runat="server" visible="false">
                                                                            <label>Sports Person ?</label>
                                                                            <asp:DropDownList runat="server" ID="ddlSports" AppendDataBoundItems="true"
                                                                                TabIndex="1" ToolTip="Please Select Sports Person" CssClass="form-control">
                                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                                                                <asp:ListItem Value="2">No</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                        <div class="form-group col-lg-4 col-md-6 col-12" id="LevelOfSports" runat="server" visible="false">
                                                                            <label>Level of Sports Represented</label>
                                                                            <asp:DropDownList runat="server" ID="ddlLevelOfSports" AppendDataBoundItems="true"
                                                                                TabIndex="1" ToolTip="Please Select Level of Sports Represented" CssClass="form-control">
                                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                                <asp:ListItem Value="1"> State Level</asp:ListItem>
                                                                                <asp:ListItem Value="2"> National Level</asp:ListItem>
                                                                            </asp:DropDownList>

                                                                        </div>
                                                                        <div class="form-group col-lg-4 col-md-6 col-12" id="sportsName" runat="server" visible="false">
                                                                            <label>Sports Name</label>
                                                                            <asp:TextBox ID="txtSportsName" runat="server" ToolTip="Please Enter Sports Name" MaxLength="64" CssClass="form-control" AutoComplete="off" TabIndex="1"></asp:TextBox>
                                                                            <%--  <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" InvalidChars="~`!@#$%^&*()_-+=|\}]{[:;'<,>.?/0123456789ss"
                                    FilterMode="ValidChars" TargetControlID="txtSportsName">
                                </ajaxToolKit:FilteredTextBoxExtender>--%>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="ftbe1" runat="server" InvalidChars="~`!@#$%^&*()_-+=|\}]{[:;'<,>.?/0123456789" FilterMode="InvalidChars" TargetControlID="txtSportsName"></ajaxToolkit:FilteredTextBoxExtender>
                                                                        </div>
                                                                        <div class="form-group col-lg-4 col-md-6 col-12" id="sportsDoc" runat="server" visible="false">
                                                                            <label>Document Upload </label>
                                                                            <asp:FileUpload ID="sportUpload" runat="server" TabIndex="1" ToolTip="Please Upload Document" Visible="false" />
                                                                            <asp:Label ID="lblFileName" runat="server" Visible="false"></asp:Label>
                                                                        </div>
                                                                        <div class="form-group col-lg-4 col-md-6 col-12" runat="server" id="forRCAInternaCandi" visible="false">
                                                                            <label>Internal Candidate Information?</label>
                                                                            <asp:DropDownList runat="server" ID="ddlCondidateInfo" AppendDataBoundItems="true"
                                                                                TabIndex="1" ToolTip="Please Select Internal Candidate Information" CssClass="form-control">
                                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                                                                <asp:ListItem Value="2">No</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                                                            <label>Height (in cms.)</label>
                                                                            <asp:TextBox ID="txtHeight" runat="server" TabIndex="12" CssClass="form-control"
                                                                                ToolTip="Please Enter Height" MaxLength="3" placeholder="Please Enter Height" AutoComplete="off" Enabled="false" />
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender22" runat="server" TargetControlID="txtHeight"
                                                                                ValidChars="1234567890" FilterMode="ValidChars">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </div>

                                                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                                                            <label>Weight (in Kgs.)</label>
                                                                            <asp:TextBox ID="txtWeight" runat="server" TabIndex="13" CssClass="form-control"
                                                                                ToolTip="Please Enter Weight" MaxLength="3" placeholder="Please Enter Weight" AutoComplete="off" Enabled="false" />
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender23" runat="server" TargetControlID="txtWeight"
                                                                                ValidChars="1234567890" FilterMode="ValidChars">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-12">
                                                                    <div class="sub-heading">
                                                                        <h5>Passport Details</h5>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div id="Div14" class="form-group col-lg-4 col-md-6 col-12" runat="server">
                                                                            <label>Passport No.</label>
                                                                            <asp:TextBox ID="txtPassportNo" runat="server" TabIndex="1" CssClass="form-control" Enabled="false"
                                                                                ToolTip="Please Enter Passport No." MaxLength="10" placeholder="Please Enter Passport No" onkeypress="allowAlphaNumericSpace(event)" AutoComplete="off" />

                                                                        </div>
                                                                        <div class="form-group col-lg-4 col-md-6 col-12" runat="server" id="divdateofissue">
                                                                            <label>Date Of Issue</label>
                                                                            <asp:TextBox ID="txtDateOfIssuePass" runat="server" onchange="return myfunction(this),cal()" type="date" Enabled="false"
                                                                                CssClass="form-control" TabIndex="1"></asp:TextBox>
                                                                        </div>
                                                                        <div class="form-group col-lg-4 col-md-6 col-12" runat="server" id="divdateofexpiry">
                                                                            <label>Date Of Expiry</label>
                                                                            <asp:TextBox ID="txtDateOfExpiryPass" runat="server" type="date" onchange="return myfunction(this),cal()" Enabled="false"
                                                                                CssClass="form-control" TabIndex="1"></asp:TextBox>
                                                                        </div>
                                                                        <div class="form-group col-lg-4 col-md-6 col-12" runat="server" id="divcountryofissue">
                                                                            <label>Country Of Issue</label>
                                                                            <asp:DropDownList ID="ddlCountryOfIssuePass" runat="server" ToolTip="Please Select Country Of Issue" Enabled="false"
                                                                                CssClass="form-control" AppendDataBoundItems="true" data-select2-enable="true">
                                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                        <div class="form-group col-lg-4 col-md-6 col-12" runat="server" id="divplaceofissue">
                                                                            <label>Place Of Issue</label>
                                                                            <asp:TextBox ID="txtPlaceOfIssuePass" MaxLength="50" onkeypress="return alphaOnly(event);" runat="server" Enabled="false"
                                                                                CssClass="form-control text-uppercase" TabIndex="1"></asp:TextBox>
                                                                        </div>
                                                                        <div class="form-group col-lg-4 col-md-6 col-12" runat="server" id="divissuingproof" visible="false">
                                                                            <label>Issuing Authority of Proof of NRI Status</label>
                                                                            <asp:TextBox ID="txtProofNRIStatus" MaxLength="100" onkeypress="return alphaOnly(event);" runat="server" CssClass="form-control text-uppercase" TabIndex="1"></asp:TextBox>
                                                                        </div>



                                                                    </div>
                                                                </div>

                                                                <div class="col-12 mt-3">
                                                                    <div class="row">
                                                                        <div class="col-12">
                                                                            <div class="sub-heading">
                                                                                <h5>Father's Details</h5>
                                                                            </div>
                                                                        </div>
                                                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                                                            <label>Father's Full Name</label>
                                                                            <asp:TextBox ID="txtFatherName" runat="server" TabIndex="1" ToolTip="Please Enter Father's Name" AutoComplete="off"
                                                                                onkeypress="return alphaOnly(event);" onkeyup="ToUpper(this)" MaxLength="50" CssClass="form-control" Placeholder="Please Enter Father's Name" />
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtFatherName"
                                                                                InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" FilterMode="InvalidChars" />
                                                                        </div>
                                                                        <div id="Div15" class="form-group col-lg-4 col-md-6 col-12" runat="server" visible="false">
                                                                            <label>Tel. Number (with STD code)</label>
                                                                            <asp:TextBox ID="txtFTelNo" runat="server" TabIndex="1" ToolTip="Please Enter Tel. Number" AutoComplete="off"
                                                                                MaxLength="12" CssClass="form-control" Placeholder="Please Enter Tel. Number" onkeyup="validateNumeric(this)" />
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" TargetControlID="txtFTelNo"
                                                                                ValidChars="1234567890" FilterMode="ValidChars" />
                                                                        </div>
                                                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                                                            <label>Mobile Number</label>
                                                                            <asp:TextBox ID="txtFMobile" runat="server" TabIndex="1" ToolTip="Please Enter Mobile Number" CssClass="form-control" AutoComplete="off"
                                                                                MaxLength="10" Placeholder="Please Enter 10 Digit Mobile No." onkeyup="validateNumeric(this)" />
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" TargetControlID="txtFMobile"
                                                                                ValidChars="1234567890" FilterMode="ValidChars" />
                                                                        </div>
                                                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                                                            <label>Occupation</label>
                                                                            <asp:DropDownList runat="server" ID="ddlFOccupation" AppendDataBoundItems="true" AutoPostBack="true"
                                                                                TabIndex="1" ToolTip="Please Select Occupation" CssClass="form-control">
                                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                                                            <label>Designation</label>
                                                                            <asp:TextBox ID="txtFDesignation" runat="server" TabIndex="1" ToolTip="Please Enter Designation"
                                                                                MaxLength="50" CssClass="form-control" Placeholder="Please Enter Designation" AutoComplete="off" />
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender19" runat="server" TargetControlID="txtFDesignation"
                                                                                InvalidChars="0123456789~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" FilterMode="InvalidChars" />
                                                                        </div>
                                                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                                                            <label>Email</label>
                                                                            <asp:TextBox ID="txtFEmail" runat="server" TabIndex="1" CssClass="form-control" AutoComplete="off" ToolTip="Please Enter Valid Email ID" />
                                                                            <br />
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender16" runat="server" TargetControlID="txtFEmail"
                                                                                InvalidChars="0123456789~`!#$%^*()_+=,/:;<>?'{}[]\|-&&quot;'" FilterMode="InvalidChars" />
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="col-12 mt-3">
                                                                    <div class="row">
                                                                        <div class="col-12">
                                                                            <div class="sub-heading">
                                                                                <h5>Mother's Details</h5>
                                                                            </div>
                                                                        </div>
                                                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                                                            <label>Mother's Full Name</label>
                                                                            <asp:TextBox ID="txtMothersName" runat="server" TabIndex="1" ToolTip="Please Enter Mother's Name" AutoComplete="off"
                                                                                onkeypress="return alphaOnly(event);" onkeyup="ToUpper(this)" MaxLength="50" CssClass="form-control" Placeholder="Please Enter Mother's Name" />
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtMothersName"
                                                                                InvalidChars="0123456789~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" FilterMode="InvalidChars" />
                                                                        </div>
                                                                        <div id="Div16" class="form-group col-lg-4 col-md-6 col-12" runat="server" visible="false">
                                                                            <label>Tel. Number (with STD code)</label>
                                                                            <asp:TextBox ID="txtMTelNo" runat="server" TabIndex="1" ToolTip="Please Enter Tel. Number" AutoComplete="off"
                                                                                MaxLength="12" CssClass="form-control" Placeholder="Please Enter Tel. Number" onkeyup="validateNumeric(this)" />
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" TargetControlID="txtMTelNo"
                                                                                ValidChars="1234567890" FilterMode="ValidChars" />
                                                                        </div>
                                                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                                                            <label>Mobile Number</label>
                                                                            <asp:TextBox ID="txtMMobile" runat="server" TabIndex="1" ToolTip="Please Enter Mobile Number" CssClass="form-control" AutoComplete="off"
                                                                                MaxLength="10" Placeholder="Please Enter 10 Digit Mobile No." onkeyup="validateNumeric(this)" />
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" TargetControlID="txtMTelNo"
                                                                                ValidChars="1234567890" FilterMode="ValidChars" />
                                                                        </div>
                                                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                                                            <label>Occupation</label>
                                                                            <asp:DropDownList runat="server" ID="ddlMOccupation" AppendDataBoundItems="true" AutoPostBack="true"
                                                                                TabIndex="1" ToolTip="Please Select Occupation" CssClass="form-control">
                                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                                                            <label>Designation</label>
                                                                            <asp:TextBox ID="txtMDesignation" runat="server" TabIndex="1" ToolTip="Please Enter Designation"
                                                                                MaxLength="50" CssClass="form-control" Placeholder="Please Enter Designation" AutoComplete="off" />
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender17" runat="server" TargetControlID="txtMDesignation"
                                                                                InvalidChars="0123456789~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" FilterMode="InvalidChars" />
                                                                        </div>
                                                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                                                            <label>Email</label>
                                                                            <asp:TextBox ID="txtMEmail" runat="server" TabIndex="1" CssClass="form-control" AutoComplete="off" ToolTip="Please Enter Valid Email ID" />
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender18" runat="server" TargetControlID="txtMEmail"
                                                                                InvalidChars="0123456789~`!#$%^*()_+=,/:;<>?'{}[]\|-&&quot;'" FilterMode="InvalidChars" />
                                                                        </div>
                                                                        <div id="Div17" class="form-group col-lg-4 col-md-6 col-12" runat="server" visible="false">
                                                                            <label>Parent's Annual Income</label>
                                                                            <asp:DropDownList runat="server" ID="ddlParentsIncome" AppendDataBoundItems="true"
                                                                                TabIndex="1" ToolTip="Please Select Parent's Annual Income" CssClass="form-control">
                                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                                <asp:ListItem Value="1">BPL</asp:ListItem>
                                                                                <asp:ListItem Value="2">Less than 2 Lac</asp:ListItem>
                                                                                <asp:ListItem Value="3">2 Lac-4 Lac</asp:ListItem>
                                                                                <asp:ListItem Value="4">4 Lac-6 Lac</asp:ListItem>
                                                                                <asp:ListItem Value="5">Above 6 Lac</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                                <%--Personal Details Section end --%>
                                                <%--Address Details Section start --%>
                                                <asp:Panel ID="pnlAddressDetails" runat="server">
                                                    <div class="card">
                                                        <div class="card-header collapsed" data-toggle="collapse" data-target="#collapseThree" aria-expanded="false" aria-controls="collapseThree">
                                                            <span class="title">Address Details</span>
                                                            <span class="accicon"><i class="fa fa-angle-down rotate-icon"></i></span>
                                                        </div>
                                                        <div id="collapseThree" class="collapse" data-parent="#StudentDetails">
                                                            <div class="card-body">
                                                                <div class="col-12">
                                                                    <div class="row">
                                                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                                                            <label>Correspondence Address</label>

                                                                            <asp:TextBox ID="txtCorresAddress" TextMode="MultiLine" runat="server" Rows="1"
                                                                                MaxLength="100" ToolTip="Please Enter Local Address" TabIndex="1" CssClass="form-control" placeholder="Please Enter Local Address" AutoComplete="off" />
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" InvalidChars="<>?|\~`!@#$%^&*_+={[}]:;&quot;'"
                                                                                TargetControlID="txtCorresAddress" FilterMode="InvalidChars" />
                                                                        </div>
                                                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                                                            <label>Country </label>

                                                                            <asp:DropDownList ID="ddlLCon" runat="server" TabIndex="1" CssClass="form-control" AppendDataBoundItems="True" ToolTip="Please Select Country.">
                                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                            <asp:TextBox runat="server" ID="txtLcountry" Visible="false" PlaceHolder="Please Enter Country" TabIndex="0" CssClass="form-control" Width="55%" ToolTip="Please Enter Country" AutoComplete="off"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" FilterType="LowercaseLetters, UppercaseLetters"
                                                                                TargetControlID="txtLcountry" />

                                                                        </div>
                                                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                                                            <label>State </label>
                                                                            <asp:DropDownList ID="ddlLSta" runat="server" CssClass="form-control" AppendDataBoundItems="True" TabIndex="1" ToolTip="Please Select State.">
                                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                            <asp:TextBox runat="server" ID="txtLState" Visible="false" PlaceHolder="Please Enter State" TabIndex="0" CssClass="form-control" Width="55%" ToolTip="Please Enter State" AutoComplete="off"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" FilterType="LowercaseLetters, UppercaseLetters"
                                                                                TargetControlID="txtLState" />

                                                                        </div>
                                                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                                                            <label>District</label>
                                                                            <div class="d-flex">
                                                                                <asp:DropDownList ID="ddlCorrDistrict" runat="server" CssClass="form-control" AppendDataBoundItems="True" TabIndex="1"
                                                                                    Enabled="false" ToolTip="Please Select City.">
                                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                                </asp:DropDownList>


                                                                            </div>
                                                                        </div>
                                                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                                                            <label>City</label>
                                                                            <asp:TextBox runat="server" ID="txtLCity" Enabled="false" PlaceHolder="Please Enter City" TabIndex="0" CssClass="form-control" AutoComplete="off" ToolTip="Please Enter City."></asp:TextBox>
                                                                        </div>
                                                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                                                            <label>Taluka / Town / Village</label>
                                                                            <asp:TextBox ID="txtLTaluka" runat="server" TabIndex="1" CssClass="form-control" Enabled="false"
                                                                                ToolTip="Please Enter Taluka / Town / Village" MaxLength="16" placeholder="Please Enter Taluka / Town / Village" onkeydown="return((event.keyCode >= 64 && event.keyCode <= 91) || (event.keyCode==32)|| (event.keyCode==8)|| (event.keyCode==9));" />
                                                                        </div>
                                                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                                                            <label>PIN Code/ZIP </label>
                                                                            <asp:TextBox ID="txtLocalPIN" runat="server" TabIndex="1" MaxLength="6"
                                                                                ToolTip="Please Enter PIN Code/ZIP" CssClass="form-control" placeholder="Please Enter PIN Code/ZIP" AutoComplete="off" />
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="ftbPINLoal" runat="server" FilterType="Numbers"
                                                                                InvalidChars="~!@#$%^&*()_+|?></?" TargetControlID="txtLocalPIN">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                                                            <label>Permanent Address</label>

                                                                            <asp:TextBox ID="txtPermAddress" runat="server" TextMode="MultiLine" Rows="1"
                                                                                MaxLength="100" ToolTip="Please Enter Permenant Address" TabIndex="1" CssClass="form-control" placeholder="Please Enter Permenant Address" AutoComplete="off" />
                                                                            <asp:TextBox ID="txtPdistrict" runat="server" TabIndex="0" Visible="False" ToolTip="Please Enter District" AutoComplete="off"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" runat="server"
                                                                                FilterMode="InvalidChars" FilterType="Custom" InvalidChars="1234567890" TargetControlID="txtPdistrict" />
                                                                        </div>
                                                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                                                            <label>Country </label>
                                                                            <%--<td class="style3">--%>
                                                                            <asp:DropDownList ID="ddlPCon" runat="server" CssClass="form-control" TabIndex="1" AppendDataBoundItems="True" ToolTip="Please Select Country.">
                                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>

                                                                            </asp:DropDownList>
                                                                            <asp:TextBox runat="server" ID="txtCCountry" TabIndex="0" Visible="false" PlaceHolder="Please Enter Country" CssClass="form-control" Width="250px" ToolTip="Please Enter Country" AutoComplete="off"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender14" runat="server" FilterType="LowercaseLetters, UppercaseLetters"
                                                                                TargetControlID="txtCCountry" />
                                                                        </div>
                                                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                                                            <label>State </label>
                                                                            <asp:DropDownList ID="ddlPermanentState" runat="server" CssClass="form-control" AppendDataBoundItems="True" TabIndex="1" ToolTip="Please Select State.">
                                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                            <asp:TextBox runat="server" ID="txtCState" TabIndex="0" Visible="false" PlaceHolder="Please Enter State" CssClass="form-control" Width="55%"></asp:TextBox>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender20" runat="server" FilterType="LowercaseLetters, UppercaseLetters"
                                                                                TargetControlID="txtCState" />
                                                                        </div>

                                                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                                                            <label>District  </label>
                                                                            <div class="d-flex">
                                                                                <asp:DropDownList ID="ddlPermanentDistrict" runat="server" CssClass="form-control" AppendDataBoundItems="True" TabIndex="1"
                                                                                    Enabled="false" ToolTip="Please Select City.">
                                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                                </asp:DropDownList>

                                                                            </div>
                                                                        </div>
                                                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                                                            <label>City</label>
                                                                            <asp:TextBox runat="server" ID="txtPermanentCity" Enabled="false" PlaceHolder="Please Enter City" TabIndex="0" CssClass="form-control" AutoComplete="off" ToolTip="Please Enter City."></asp:TextBox>

                                                                        </div>
                                                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                                                            <label>Taluka / Town / Village</label>
                                                                            <asp:TextBox ID="txtPerTaluka" runat="server" TabIndex="1" CssClass="form-control"
                                                                                Enabled="false" ToolTip="Please Enter Taluka / Town / Village" MaxLength="16" placeholder="Please Enter Taluka / Town / Village"
                                                                                onkeydown="return((event.keyCode >= 64 && event.keyCode <= 91) || (event.keyCode==32)|| (event.keyCode==8)|| (event.keyCode==9));" />
                                                                        </div>
                                                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                                                            <label>PIN Code/ZIP</label>
                                                                            <asp:TextBox ID="txtPermPIN" runat="server" TabIndex="1" ToolTip="Please Enter PIN Code/ZIP." AutoComplete="off"
                                                                                MaxLength="6" onkeyup="validateNumeric(this);" CssClass="form-control" placeholder="Please Enter PIN Code/ZIP" />
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="ftrPermPIN" runat="server" FilterType="Numbers"
                                                                                InvalidChars="~!@#$%^&*()_+|?></?" TargetControlID="txtPermPIN">
                                                                            </ajaxToolkit:FilteredTextBoxExtender>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                                <%--Address Details Section end --%>
                                                <%--Photo & Signature Section start --%>
                                                <asp:Panel ID="pnlPhotoSign" runat="server">
                                                    <div class="card">
                                                        <div class="card-header collapsed" data-toggle="collapse" data-target="#collapseFour" aria-expanded="false" aria-controls="collapseFour">
                                                            <span class="title">Photo & Signature Details </span>
                                                            <span class="accicon"><i class="fa fa-angle-down rotate-icon"></i></span>
                                                        </div>
                                                        <div id="collapseFour" class="collapse" data-parent="#StudentDetails">
                                                            <div class="card-body">
                                                                <div class="col-12 mt-3">
                                                                    <div class="row">
                                                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                                                            <asp:Image ID="imgPhoto" runat="server" Width="100px" ImageUrl="~/IMAGES/nophoto.jpg" />
                                                                        </div>
                                                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                                                            <asp:Image ID="ImgSign" runat="server" Width="125px" ImageUrl="~/IMAGES/sign11.jpg" Height="40px" Style="margin-top: 25px;" />
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                                <%--Photo & Signature Section end --%>
                                                <%-- Qualification Section start --%>
                                                <asp:Panel ID="pnlQualificationDetails" runat="server">
                                                    <div class="card">
                                                        <div class="card-header collapsed" data-toggle="collapse" data-target="#collapseFive" aria-expanded="false" aria-controls="collapseFive">
                                                            <span class="title">Qualification Details </span>
                                                            <span class="accicon"><i class="fa fa-angle-down rotate-icon"></i></span>
                                                        </div>
                                                        <div id="collapseFive" class="collapse" data-parent="#StudentDetails">
                                                            <div class="card-body">
                                                                <div class="col-12 mt-3">
                                                                    <div class="row" id="divQualiX" runat="server" visible="false">
                                                                        <div class="col-12">
                                                                            <div class="sub-heading">
                                                                                <h5>X-Class Details</h5>
                                                                            </div>
                                                                        </div>
                                                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                                                            <label>Enrollment/Seat No</label>
                                                                            <asp:TextBox ID="txtRegisterNumber" runat="server" Enabled="false" CssClass="form-control" TabIndex="1" ToolTip="Register Number"></asp:TextBox>
                                                                        </div>

                                                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                                                            <label>Institution Name/School</label>
                                                                            <asp:TextBox ID="txtInstitution" runat="server" Enabled="false" CssClass="form-control" TabIndex="2" ToolTip="Institution Name/School"></asp:TextBox>
                                                                        </div>

                                                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                                                            <label>Country</label>
                                                                            <asp:TextBox ID="ddlCountry" runat="server" Enabled="false" CssClass="form-control" TabIndex="2"></asp:TextBox>
                                                                        </div>

                                                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                                                            <label>State</label>
                                                                            <asp:TextBox ID="ddlState" runat="server" Enabled="false" CssClass="form-control" TabIndex="2"></asp:TextBox>

                                                                        </div>

                                                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                                                            <label>Board of Examination</label>
                                                                            <asp:TextBox ID="ddlBoardExamination" runat="server" Enabled="false" CssClass="form-control" TabIndex="2"></asp:TextBox>
                                                                        </div>

                                                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                                                            <label>Year of Passing</label>
                                                                            <asp:TextBox ID="ddlYearPassing" runat="server" Enabled="false" CssClass="form-control" TabIndex="2"></asp:TextBox>
                                                                        </div>

                                                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                                                            <label>Month of Passing</label>
                                                                            <asp:TextBox ID="ddlMonthPassing" runat="server" Enabled="false" CssClass="form-control" TabIndex="2"></asp:TextBox>
                                                                        </div>

                                                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                                                            <label>Number of Attempts</label>
                                                                            <asp:TextBox ID="txtNumberAttempts" runat="server" Enabled="false" CssClass="form-control" TabIndex="8"></asp:TextBox>
                                                                        </div>

                                                                        <div id="Div8" class="form-group col-lg-4 col-md-6 col-12" runat="server" visible="false">
                                                                            <label>Evaluation Type</label>
                                                                            <asp:TextBox ID="ddlEvaluationType" runat="server" Enabled="false" CssClass="form-control" TabIndex="2" ToolTip="Evaluation"></asp:TextBox>
                                                                        </div>

                                                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                                                            <label>Total Maximum Marks </label>
                                                                            <asp:TextBox ID="txtXMaxMarks" runat="server" Enabled="false" CssClass="form-control" placeholder="0"
                                                                                MaxLength="3"></asp:TextBox>
                                                                        </div>
                                                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                                                            <label>Total Marks Obtained </label>
                                                                            <asp:TextBox ID="txtXMarksObtained" runat="server" Enabled="false" CssClass="form-control" placeholder="0"
                                                                                MaxLength="3"></asp:TextBox>

                                                                        </div>
                                                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                                                            <label>% of Marks</label>
                                                                            <asp:TextBox ID="txtXPerOfMarks" runat="server" Enabled="false" CssClass="form-control" placeholder="0" MaxLength="5"></asp:TextBox>
                                                                        </div>


                                                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                                                            <label>Certificate is Uploaded?</label><br />
                                                                            <asp:TextBox ID="txtCertificateStatus" runat="server" Enabled="false" CssClass="form-control" TabIndex="2" ToolTip="CERTIFICATE"></asp:TextBox>
                                                                        </div>

                                                                        <div class="col-12">
                                                                            <asp:ListView ID="lvQualiSSC_grade" runat="server" Visible="false">
                                                                                <LayoutTemplate>
                                                                                    <div class="vista-grid" style="width: 100%;">
                                                                                        <%--<div class="sub-heading" id="dem">
                                                        <h6>i. X-Class Details:</h6>
                                                    </div>--%>
                                                                                        <div class="table table-responsive">
                                                                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="myTable1">
                                                                                                <thead class="bg-light-blue">
                                                                                                    <tr>
                                                                                                        <%--  <th>Subject Type</th>--%>
                                                                                                        <th>Subject</th>
                                                                                                        <th>Grade</th>
                                                                                                        <th>Maximum Grade Point</th>
                                                                                                        <th>Grade Point Obtained</th>
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
                                                                                    <tr class="item">
                                                                                        <%--  <td><%# Eval("SUBJECTTYPE")%><%--Subject Type</td>--%>
                                                                                        <td><%# Eval("SUBJECTNAME")%><%--Subject--%></td>
                                                                                        <td><%# Eval("GRADENAME")%><%--Grade--%></td>
                                                                                        <td><%# Eval("MAXGRADEPOINT")%><%--Maximum Grade Point--%></td>
                                                                                        <td><%# Eval("CGPA")%><%--Grade Point Obtained--%></td>
                                                                                    </tr>
                                                                                </ItemTemplate>

                                                                            </asp:ListView>
                                                                        </div>
                                                                    </div>

                                                                    <%-- Qualification : 2.HSC --%>
                                                                    <div class="row" id="divQualiPlusTwo" runat="server" visible="false">
                                                                        <div class="col-12">
                                                                            <div class="sub-heading">
                                                                                <%--Educational--%>
                                                                                <h5>Plus Two Level Details</h5>

                                                                            </div>
                                                                        </div>
                                                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                                                            <label>Register Number</label>
                                                                            <asp:TextBox ID="txtPlusTwoRegNo" runat="server" Enabled="false" CssClass="form-control" TabIndex="1"></asp:TextBox>
                                                                        </div>

                                                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                                                            <label>Institution Name/School</label>
                                                                            <asp:TextBox ID="txtPluseTwoInstitutionName" runat="server" Enabled="false" CssClass="form-control" TabIndex="1"></asp:TextBox>
                                                                        </div>
                                                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                                                            <label>Country of Institution</label>
                                                                            <asp:DropDownList runat="server" ID="ddlPTCountryOfInstitution" Enabled="false" CssClass="form-control" data-select2-enable="true" TabIndex="1" AppendDataBoundItems="true">
                                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                                                            <label>Country</label>
                                                                            <asp:TextBox ID="ddlPlusTwoCountry" runat="server" Enabled="false" CssClass="form-control" TabIndex="1"></asp:TextBox>
                                                                        </div>

                                                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                                                            <label>State</label>
                                                                            <asp:TextBox ID="ddlPlusTwoState" runat="server" Enabled="false" CssClass="form-control" TabIndex="1"></asp:TextBox>
                                                                        </div>

                                                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                                                            <label>Board of Examination</label>
                                                                            <asp:TextBox ID="ddlPlusTwoBOE" runat="server" Enabled="false" CssClass="form-control" TabIndex="1"></asp:TextBox>
                                                                        </div>

                                                                        <%--<div class="form-group col-lg-4 col-md-6 col-12">
                                            <label>Group</label>
                                            <asp:TextBox ID="ddlPlusTwoGroup" runat="server" Enabled="false" CssClass="form-control" TabIndex="1"></asp:TextBox>
                                        </div>--%>

                                                                        <div id="Div9" class="form-group col-lg-4 col-md-6 col-12" runat="server" visible="false">
                                                                            <label>Year of Study - From</label>
                                                                            <asp:TextBox ID="ddlPlusTwoYearFrom" runat="server" Enabled="false" CssClass="form-control" TabIndex="1"></asp:TextBox>
                                                                        </div>

                                                                        <div id="Div10" class="form-group col-lg-4 col-md-6 col-12" runat="server" visible="false">
                                                                            <label>Year of Study - To</label>
                                                                            <asp:TextBox ID="ddlPlusTwoYearTo" runat="server" Enabled="false" CssClass="form-control" TabIndex="1"></asp:TextBox>
                                                                        </div>

                                                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                                                            <label>Year of Passing</label>
                                                                            <asp:TextBox ID="ddlPlusTwoYPassing" runat="server" Enabled="false" CssClass="form-control" TabIndex="1"></asp:TextBox>
                                                                        </div>

                                                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                                                            <label>Month of Passing</label>
                                                                            <asp:TextBox ID="ddlPlusTwoMPassing" runat="server" Enabled="false" CssClass="form-control" TabIndex="1"></asp:TextBox>
                                                                        </div>

                                                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                                                            <label>Number of Attempts</label>
                                                                            <asp:TextBox ID="txtPlusTwoNoOfAttempts" runat="server" Enabled="false" CssClass="form-control" TabIndex="1"></asp:TextBox>

                                                                        </div>

                                                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                                                            <label>Total Maximum Marks </label>
                                                                            <asp:TextBox ID="txtPTMaxMarks" runat="server" Enabled="false" CssClass="form-control" placeholder="0" MaxLength="3"></asp:TextBox>
                                                                        </div>
                                                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                                                            <label>Total Marks Obtained </label>
                                                                            <asp:TextBox ID="txtPTMarksObtained" runat="server" Enabled="false" CssClass="form-control" placeholder="0" MaxLength="3"></asp:TextBox>

                                                                        </div>
                                                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                                                            <label>% of Marks</label>
                                                                            <asp:TextBox ID="txtPTPerOfMarks" runat="server" Enabled="false" CssClass="form-control" placeholder="0" MaxLength="5"></asp:TextBox>

                                                                        </div>
                                                                        <div class="col-12">
                                                                            <span style="color: black; font-weight: bold; font-size: 13px">Note :  Aggregate marks of  Mathematics, Physics and any one of Chemistry/Bio-technology/Computer Science/Biology</span>
                                                                        </div>
                                                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                                                            <label>Aggregate Max Marks</label>
                                                                            <asp:TextBox ID="txtMaxMarksPhyMaths" runat="server" Enabled="false" CssClass="form-control" placeholder="0" MaxLength="4" TabIndex="1"></asp:TextBox>
                                                                        </div>
                                                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                                                            <label>Aggregate obtained Marks</label>
                                                                            <asp:TextBox ID="txtObtainedPhyMaths" runat="server" Enabled="false" CssClass="form-control" placeholder="0" MaxLength="5" TabIndex="1"></asp:TextBox>
                                                                        </div>

                                                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                                                            <label>Aggregate percentage of Marks in Physics and Mathematics</label>
                                                                            <asp:TextBox ID="txtPTAggPhyMaths" runat="server" Enabled="false" CssClass="form-control" placeholder="0" MaxLength="5" TabIndex="1"></asp:TextBox>
                                                                        </div>

                                                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                                                            <asp:CheckBox ID="chkResult" runat="server" Text="Result Not Announced" TabIndex="1" Enabled="false" />
                                                                        </div>
                                                                        <div class="col-12" id="divXIAggPhyMaths" runat="server">
                                                                            <span style="color: black; font-weight: bold; font-size: 13px">Note :  Aggregate marks of  Mathematics, Physics and any one of Chemistry/Bio-technology/Computer Science/Biology</span>
                                                                        </div>
                                                                        <div class="form-group col-lg-4 col-md-6 col-12" runat="server" id="divxiaggmm">
                                                                            <label>XI Aggregate Max Marks </label>
                                                                            <asp:TextBox ID="txtXiMaxMarksPhyMaths" runat="server" Enabled="false" CssClass="form-control" placeholder="0" MaxLength="4" TabIndex="1"></asp:TextBox>
                                                                        </div>
                                                                        <div class="form-group col-lg-4 col-md-6 col-12" runat="server" id="divxiaggcgpa">
                                                                            <label>XI Aggregate obtained Marks</label>
                                                                            <asp:TextBox ID="txtXiObtainedPhyMaths" runat="server" Enabled="false" CssClass="form-control" placeholder="0" MaxLength="5" TabIndex="1"></asp:TextBox>
                                                                        </div>
                                                                        <div class="form-group col-lg-4 col-md-6 col-12" runat="server" id="divagg">
                                                                            <label>XI Aggregate % / CGPA out of 10</label>
                                                                            <asp:TextBox ID="txtXiAggCgpa" runat="server" Enabled="false" CssClass="form-control" placeholder="0" MaxLength="5" TabIndex="1"></asp:TextBox>
                                                                        </div>
                                                                        <div class="form-group col-lg-4 col-md-6 col-12" runat="server" id="divaggpm">
                                                                            <label>XI Average of Physics and Maths</label>
                                                                            <asp:TextBox ID="txtXiAggPhyMaths" runat="server" Enabled="false" CssClass="form-control" placeholder="0" MaxLength="5" TabIndex="1"></asp:TextBox>
                                                                        </div>
                                                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                                                            <label>Certificate is Uploaded?</label><br />
                                                                            <asp:TextBox ID="txtPlusTwoCertificateStatus" runat="server" Enabled="false" CssClass="form-control" TabIndex="1"></asp:TextBox>
                                                                        </div>


                                                                    </div>

                                                                    <%-- Qualification : 3.Graduation --%>

                                                                    <div id="divQualiUG" runat="server" visible="false">
                                                                        <div class="row">
                                                                            <div class="col-12">
                                                                                <div class="sub-heading">
                                                                                    <%--Educational--%>
                                                                                    <h5>Graduation Details</h5>
                                                                                </div>
                                                                            </div>

                                                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                                                <label>Institution Name/School</label>
                                                                                <asp:TextBox ID="txtUGInstitutionName" runat="server" Enabled="false" CssClass="form-control" TabIndex="1"></asp:TextBox>
                                                                            </div>

                                                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                                                <label>Country</label>
                                                                                <asp:TextBox ID="ddlUGCountry" runat="server" Enabled="false" CssClass="form-control" TabIndex="1"></asp:TextBox>
                                                                            </div>

                                                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                                                <label>State</label>
                                                                                <asp:TextBox ID="ddlUGState" runat="server" Enabled="false" CssClass="form-control" TabIndex="1"></asp:TextBox>
                                                                            </div>

                                                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                                                <label>University/Board </label>
                                                                                <asp:TextBox ID="ddlUGUniversity" runat="server" Enabled="false" CssClass="form-control" TabIndex="1"></asp:TextBox>
                                                                            </div>

                                                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                                                <label>Qualify Degree</label>
                                                                                <asp:DropDownList runat="server" ID="ddlUGQualifyDegree" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" TabIndex="33" AutoPostBack="true">
                                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                            <div class="form-group col-lg-4 col-md-6 col-12" id="divQualifyDegree" runat="server" visible="false">
                                                                                <label>Qualify Degree</label>
                                                                                <asp:TextBox ID="txtUGQualifyDegree" runat="server" Enabled="false" CssClass="form-control" TabIndex="34" MaxLength="40"></asp:TextBox>
                                                                            </div>
                                                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                                                <label>Qualify Program</label>
                                                                                <asp:DropDownList runat="server" ID="ddlUGProgram" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" TabIndex="33" AutoPostBack="true">
                                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                            <div class="form-group col-lg-4 col-md-6 col-12" id="divQualifyProgram" runat="server" visible="false">
                                                                                <label>Qualify Program</label>
                                                                                <asp:TextBox ID="txtUGQualifyProgram" runat="server" Enabled="false" CssClass="form-control" TabIndex="34" MaxLength="40"></asp:TextBox>
                                                                            </div>

                                                                            <%--<div class="form-group col-lg-4 col-md-6 col-12">
                                                <label>Qualify Program</label>
                                                <asp:TextBox ID="ddlUGProgram" runat="server" Enabled="false" CssClass="form-control" TabIndex="1"></asp:TextBox>
                                            </div>--%>

                                                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                                                <label>Specialization</label>
                                                                                <asp:TextBox ID="txtUGSpecialization" runat="server" Enabled="false" CssClass="form-control" TabIndex="1"></asp:TextBox>
                                                                            </div>

                                                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                                                <label>No. of Semesters</label>
                                                                                <asp:TextBox ID="ddlUGSemester" runat="server" Enabled="false" CssClass="form-control" TabIndex="1"></asp:TextBox>
                                                                            </div>

                                                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                                                <label>Course Duration From</label>
                                                                                <asp:TextBox ID="ddlUGCourseDurationFrom" runat="server" Enabled="false" CssClass="form-control" TabIndex="1"></asp:TextBox>
                                                                            </div>

                                                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                                                <label>Course Duration To</label>
                                                                                <asp:TextBox ID="ddlUGCourseDurationTo" runat="server" Enabled="false" CssClass="form-control" TabIndex="1"></asp:TextBox>
                                                                            </div>

                                                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                                                <label>Evaluation Type</label>
                                                                                <asp:TextBox ID="ddlUGEvaluationType" runat="server" Enabled="false" CssClass="form-control" TabIndex="1"></asp:TextBox>
                                                                            </div>

                                                                            <%--<div class="form-group col-lg-4 col-md-6 col-12">
                                            <label>Certificate is Uploaded?</label>
                                            <asp:TextBox ID="txtUGCertificateStatus" runat="server" Enabled="false" CssClass="form-control" TabIndex="1"></asp:TextBox>
                                        </div>--%>

                                                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                                                <label>CGPA </label>
                                                                                <asp:TextBox ID="txtUGCGPA" runat="server" Enabled="false" CssClass="form-control" TabIndex="1"></asp:TextBox>
                                                                            </div>

                                                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                                                <label>Maximum CGPA </label>
                                                                                <asp:TextBox ID="txtUGMaxCGPA" runat="server" Enabled="false" CssClass="form-control" TabIndex="1"></asp:TextBox>
                                                                            </div>
                                                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                                                <label>
                                                                                    Current % of Marks
                                                                                </label>
                                                                                <asp:TextBox ID="txtUGPercentOfMarks" runat="server" CssClass="form-control" MaxLength="5" TabIndex="1"
                                                                                    onkeypress="return (event.charCode >= 48 && event.charCode <= 57)||event.charCode == 46" placeholder="0" onkeyup="return ValidatedPercentage(this);"></asp:TextBox>
                                                                            </div>
                                                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                                                <label>% from CGPA </label>
                                                                                <asp:TextBox ID="txtUGPerCGPA" runat="server" Enabled="false" CssClass="form-control" TabIndex="1"></asp:TextBox>
                                                                            </div>

                                                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                                                <label>Year of Passing</label>
                                                                                <asp:TextBox ID="ddlUGYearOfPassing" runat="server" Enabled="false" CssClass="form-control" TabIndex="1"></asp:TextBox>
                                                                            </div>

                                                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                                                <label>Certificate is Uploaded?</label>
                                                                                <asp:TextBox ID="txtUGCertificateStatus" runat="server" Enabled="false" CssClass="form-control" TabIndex="1"></asp:TextBox>
                                                                            </div>

                                                                            <div class="col-12">
                                                                                <%--  <asp:ListView ID="lvQualiUG" runat="server" Visible="false">
                                                    <LayoutTemplate>--%>
                                                                                <%--margin-left: 13px--%>
                                                                                <%-- <div class="vista-grid" style="width: 100%;">
                                                            <div class="sub-heading" id="dem">--%>
                                                                                <%-- <h6>ii. Plus Two Level Details:</h6>--%>
                                                                                <%-- </div>
                                                            <div class="table table-responsive">
                                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="myTable1">
                                                                    <thead class="bg-light-blue">
                                                                        <tr>
                                                                            <th>Semester</th>
                                                                            <th>Maximum Marks</th>
                                                                            <th>Marks Obtained</th>
                                                                            <th>Maximum SGPA</th>
                                                                            <th>SGPA</th>
                                                                            <th>Grade</th>
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
                                                        <tr class="item">
                                                            <td><%# Eval("SEMESTERNO")%></td>
                                                            <td><%# Eval("OUT_OF_MARKS")%></td>
                                                            <td><%# Eval("OBTAINED_MARKS")%></td>
                                                            <%--UGPG_SGPA	UGPG_MAXSGPA	UGPG_GRADE	UGPG_CGPA	UGPG_MAXCGPA--%>
                                                                                <%--<td><%# Eval("UGPG_MAXSGPA")%></td>
                                                            <td><%# Eval("UGPG_SGPA")%></td>
                                                            <td><%# Eval("UGPG_GRADE")%></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>--%>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <%-- Qualification : 4.Post Graduation --%>
                                                                    <div id="divQualiPG" runat="server" visible="false">
                                                                        <div class="row">
                                                                            <div class="col-12">
                                                                                <div class="sub-heading">
                                                                                    <%--Educational--%>
                                                                                    <h5>Post Graduation Details</h5>
                                                                                </div>
                                                                            </div>

                                                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                                                <label>Institution Name/School</label>
                                                                                <asp:TextBox ID="txtPGInstitutionName" runat="server" Enabled="false" CssClass="form-control" TabIndex="1"></asp:TextBox>
                                                                            </div>

                                                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                                                <label>Country</label>
                                                                                <asp:TextBox ID="ddlPGCountry" runat="server" Enabled="false" CssClass="form-control" TabIndex="1"></asp:TextBox>
                                                                            </div>

                                                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                                                <label>State</label>
                                                                                <asp:TextBox ID="ddlPGState" runat="server" Enabled="false" CssClass="form-control" TabIndex="1"></asp:TextBox>
                                                                            </div>

                                                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                                                <label>University/Board </label>
                                                                                <asp:TextBox ID="ddlPGUniversity" runat="server" Enabled="false" CssClass="form-control" TabIndex="1"></asp:TextBox>
                                                                            </div>

                                                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                                                <label>Program</label>
                                                                                <asp:TextBox ID="ddlPGProgram" runat="server" Enabled="false" CssClass="form-control" TabIndex="1"></asp:TextBox>
                                                                            </div>

                                                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                                                <label>Specialization</label>
                                                                                <asp:TextBox ID="txtPGSpecialization" runat="server" Enabled="false" CssClass="form-control" TabIndex="1"></asp:TextBox>
                                                                            </div>

                                                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                                                <label>No. of Semesters</label>
                                                                                <asp:TextBox ID="ddlPGSemester" runat="server" Enabled="false" CssClass="form-control" TabIndex="1"></asp:TextBox>
                                                                            </div>

                                                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                                                <label>Course Duration From</label>
                                                                                <asp:TextBox ID="ddlPGCourseDurationFrom" runat="server" Enabled="false" CssClass="form-control" TabIndex="1"></asp:TextBox>
                                                                            </div>

                                                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                                                <label>Course Duration To</label>
                                                                                <asp:TextBox ID="ddlPGCourseDurationTo" runat="server" Enabled="false" CssClass="form-control" TabIndex="1"></asp:TextBox>
                                                                            </div>

                                                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                                                <label>Evaluation Type</label>
                                                                                <asp:TextBox ID="ddlPGEvaluationType" runat="server" Enabled="false" CssClass="form-control" TabIndex="1"></asp:TextBox>
                                                                            </div>

                                                                            <%--<div class="form-group col-lg-4 col-md-6 col-12">
                                            <label>Certificate is Uploaded?</label>
                                            <asp:TextBox ID="txtPGCertificateStatus" runat="server" Enabled="false" CssClass="form-control" TabIndex="1"></asp:TextBox>
                                        </div>--%>

                                                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                                                <label>All Subject Total Marks Obtained </label>
                                                                                <asp:TextBox ID="txtAllSubjectMarksObtained" runat="server" Enabled="false" CssClass="form-control" TabIndex="1"></asp:TextBox>
                                                                            </div>

                                                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                                                <label>All Subject Total Maximum Mark </label>
                                                                                <asp:TextBox ID="txtAllSubjectMaxMark" runat="server" Enabled="false" CssClass="form-control" TabIndex="1"></asp:TextBox>
                                                                            </div>

                                                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                                                <label>% of Marks </label>
                                                                                <asp:TextBox ID="txtPGPercentOfMarks" runat="server" Enabled="false" CssClass="form-control" TabIndex="1"></asp:TextBox>
                                                                            </div>

                                                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                                                <label>Year of Passing</label>
                                                                                <asp:TextBox ID="ddlPGYearOfPassing" runat="server" Enabled="false" CssClass="form-control" TabIndex="1"></asp:TextBox>
                                                                            </div>

                                                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                                                <label>Certificate is Uploaded?</label>
                                                                                <asp:TextBox ID="txtPGCertificateStatus" runat="server" Enabled="false" CssClass="form-control" TabIndex="1"></asp:TextBox>
                                                                            </div>

                                                                            <div class="col-12">
                                                                                <asp:ListView ID="lvQualiPG" runat="server" Visible="false">
                                                                                    <LayoutTemplate>
                                                                                        <%--margin-left: 13px--%>
                                                                                        <div class="vista-grid" style="width: 100%;">
                                                                                            <div class="sub-heading" id="dem">
                                                                                                <%--<h6>ii. Plus Two Level Details:</h6>--%>
                                                                                            </div>
                                                                                            <div id="demo-grid">
                                                                                                <table class="table table-striped table-bordered nowrap" style="width: 100%" id="tblData_pg_1">
                                                                                                    <%--<div class="table table-responsive">
                                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="myTable1">--%>
                                                                                                    <thead class="bg-light-blue">
                                                                                                        <tr>
                                                                                                            <th>Semester</th>
                                                                                                            <th>Maximum Marks</th>
                                                                                                            <th>Marks Obtained</th>
                                                                                                            <th>Maximum SGPA</th>
                                                                                                            <th>SGPA</th>
                                                                                                            <th>Grade</th>
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

                                                                                        <tr class="item">
                                                                                            <td><%# Eval("SEMESTERNO")%></td>
                                                                                            <td><%# Eval("OUT_OF_MARKS")%></td>
                                                                                            <td><%# Eval("OBTAINED_MARKS")%></td>
                                                                                            <%--UGPG_SGPA,	UGPG_MAXSGPA,	UGPG_GRADE,	UGPG_CGPA,	UGPG_MAXCGPA, ALL_SUBJECT_TOTAL_MARKS_OBTAINED, ALL_SUBJECT_TOTAL_MAX_MARK--%>
                                                                                            <td><%# Eval("UGPG_MAXSGPA")%></td>
                                                                                            <td><%# Eval("UGPG_SGPA")%></td>
                                                                                            <td><%# Eval("UGPG_GRADE")%></td>
                                                                                        </tr>
                                                                                    </ItemTemplate>

                                                                                </asp:ListView>
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                    <div class="row" id="divQualiOther" runat="server" visible="false">
                                                                        <div class="col-12">
                                                                            <div class="sub-heading">
                                                                                <%--Educational--%>
                                                                                <h5>Other Details</h5>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-12">
                                                                            <asp:ListView ID="lvQualiOther" runat="server" Visible="false">
                                                                                <LayoutTemplate>
                                                                                    <%--margin-left: 13px--%>
                                                                                    <div class="vista-grid" style="width: 100%;">
                                                                                        <div id="demo-grid">
                                                                                            <table class="table table-striped table-bordered nowrap" style="width: 100%" id="tblData_other_1">
                                                                                                <%--<div class="table table-responsive">
                                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="myTable1">--%>
                                                                                                <thead class="bg-light-blue">
                                                                                                    <tr>
                                                                                                        <th>Institution Name/School</th>
                                                                                                        <th>Country</th>
                                                                                                        <th>State</th>
                                                                                                        <th>University/Board</th>
                                                                                                        <th>Program</th>
                                                                                                        <th>Course Duration From</th>
                                                                                                        <th>Course Duration To</th>
                                                                                                        <th>Year of Passing</th>
                                                                                                        <th>Total Marks Obtained</th>
                                                                                                        <th>Maximum Mark</th>
                                                                                                        <th>% of Marks</th>
                                                                                                        <th>Other Details</th>
                                                                                                        <th>is File Uploaded?</th>

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

                                                                                    <tr class="item">
                                                                                        <td><%# Eval("INSTITUTION_NAME")%></td>
                                                                                        <td><%# Eval("COUNTRYNAME")%></td>
                                                                                        <td><%# Eval("STATENAME")%></td>
                                                                                        <td><%# Eval("BOARDNAME")%></td>
                                                                                        <td><%# Eval("PROG_NAME")%></td>
                                                                                        <td><%# Eval("YEAR_OF_STUDY_FROM")%></td>
                                                                                        <td><%# Eval("YEAR_OF_STUDY_TO")%></td>
                                                                                        <td><%# Eval("YEARNAME")%></td>
                                                                                        <td><%# Eval("OBTAINED_MARKS")%></td>
                                                                                        <td><%# Eval("OUT_OF_MARKS")%></td>
                                                                                        <td><%# Eval("PERCENTAGE")%></td>
                                                                                        <td><%# Eval("OTHER_DETAILS")%></td>
                                                                                        <td><%# Eval("IS_UPLOAD_DOCS")%></td>
                                                                                    </tr>
                                                                                </ItemTemplate>

                                                                            </asp:ListView>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <%-- Qualification Section end --%>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                                <%-- Qualification Section end --%>
                                                <%-- Test Score Section start --%>
                                                <asp:Panel ID="pnlTestScore" runat="server">
                                                    <div class="card">
                                                        <div class="card-header collapsed" data-toggle="collapse" data-target="#collapseSix" aria-expanded="false" aria-controls="collapseSix">
                                                            <span class="title">Test Score Details </span>
                                                            <span class="accicon"><i class="fa fa-angle-down rotate-icon"></i></span>
                                                        </div>
                                                        <div id="collapseSix" class="collapse" data-parent="#StudentDetails">
                                                            <div class="card-body">
                                                                <div class="col-12 mt-3">
                                                                    <div class="row" id="divTestScoreList" runat="server" visible="false">
                                                                        <%--<div class="col-12">
                                            <div class="sub-heading">
                                                <h5>Test Score</h5>
                                            </div>
                                        </div>--%>
                                                                        <div class="col-12">
                                                                            <asp:ListView ID="lvTestScore" runat="server" Visible="true">
                                                                                <LayoutTemplate>
                                                                                    <div class="vista-grid" style="width: 100%;">
                                                                                        <div class="table table-responsive">
                                                                                            <table class="table table-striped table-bordered nowrap" style="width: 100%" id="myTable1">
                                                                                                <thead class="bg-light-blue">
                                                                                                    <tr>
                                                                                                        <th>Test</th>
                                                                                                        <th>Year</th>
                                                                                                        <th runat="server" visible="true" id="thsubcode">Subject Code</th>
                                                                                                        <th>Roll/Registration No.</th>
                                                                                                        <th>Score Card</th>
                                                                                                        <th runat="server" visible="true" id="thtotalscore">Total Score Out Of</th>
                                                                                                        <th runat="server" visible="true" id="thobtscore">Score Obtained</th>
                                                                                                        <th runat="server" visible="true" id="thrank">All India Rank</th>
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
                                                                                    <tr class="item">
                                                                                        <td><%# Eval("TESTNAME")%></td>
                                                                                        <td><%# Eval("QUALIFY_YEAR")%></td>
                                                                                        <td runat="server" visible="true" id="tdsubcode"><%# Eval("GATE_SUB_CODE")%></td>
                                                                                        <td><%# Eval("REGNO")%></td>
                                                                                        <td><%# Eval("DOCUMENT")%></td>
                                                                                        <td runat="server" visible="true" id="tdtscore"><%# Eval("SCORE_OUT_OF") %></td>
                                                                                        <td runat="server" visible="true" id="tdobtscore"><%# Eval("SCORE_OBTAINED")%></td>
                                                                                        <td runat="server" visible="true" id="tdrank"><%# Eval("INDIA_RANK")%></td>

                                                                                    </tr>
                                                                                </ItemTemplate>

                                                                            </asp:ListView>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                                <%-- Test Score Section end --%>

                                                <%-- Work Experience Section start --%>
                                                <asp:Panel ID="pnlWorkExperience" runat="server">
                                                    <div class="card" id="divWorkExpEmployed" runat="server" visible="false">
                                                        <div class="card-header collapsed" data-toggle="collapse" data-target="#collapseSeven" aria-expanded="false" aria-controls="collapseSeven">
                                                            <span class="title">Work Experience Details </span>
                                                            <span class="accicon"><i class="fa fa-angle-down rotate-icon"></i></span>
                                                        </div>
                                                        <div id="collapseSeven" class="collapse" data-parent="#StudentDetails">
                                                            <div class="card-body">
                                                                <div class="col-12 mt-3">
                                                                    <div class="row">
                                                                        <div class="col-12">
                                                                            <div class="sub-heading">
                                                                                <h5>Employed Details</h5>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-12">
                                                                            <asp:ListView ID="lvWorkExpEmp" runat="server" Visible="true">
                                                                                <LayoutTemplate>
                                                                                    <div class="vista-grid" style="width: 100%;">
                                                                                        <div id="demo-grid">
                                                                                            <table class="table table-striped table-bordered nowrap" style="width: 100%" id="tblData_workwxpemp_1">
                                                                                                <%--<div class="table table-responsive">
                                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="myTable1">--%>
                                                                                                <thead class="bg-light-blue">
                                                                                                    <tr>
                                                                                                        <th>Work Experience</th>
                                                                                                        <th>Organization </th>
                                                                                                        <th>Designation  </th>
                                                                                                        <th>Duration - From  </th>
                                                                                                        <th>Duration - To </th>
                                                                                                        <th>Nature of Work </th>
                                                                                                        <th>Nature of Business</th>
                                                                                                        <th>Monthly Remuneration </th>
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
                                                                                    <tr class="item">
                                                                                        <td><%# Eval("WORK_EXPERIENCE")%></td>
                                                                                        <td><%# Eval("ORGANIZATION")%></td>
                                                                                        <td><%# Eval("DESIGNATION")%></td>
                                                                                        <td><%#Eval("START_DURATION","{0:dd-MM-yyyy}") %></td>
                                                                                        <td><%# Eval("END_DURATION", "{0:dd-MM-yyyy}")%></td>
                                                                                        <td><%# Eval("NATURE_WORK")%></td>
                                                                                        <td><%# Eval("NATURE_BUSINESS")%></td>
                                                                                        <td><%# Eval("MONTHLY_REMUNERATION")%></td>
                                                                                    </tr>
                                                                                </ItemTemplate>

                                                                            </asp:ListView>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row" id="divWorkExpEntrepre" runat="server" visible="false">
                                                                        <div class="col-12">
                                                                            <div class="sub-heading">
                                                                                <h5>Entrepreneur Details</h5>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-12">
                                                                            <asp:ListView ID="lvWorkExpEnt" runat="server" Visible="true">
                                                                                <LayoutTemplate>
                                                                                    <div class="vista-grid" style="width: 100%;">

                                                                                        <div id="demo-grid">
                                                                                            <table class="table table-striped table-bordered nowrap" style="width: 100%" id="tblData_workexpent_1">
                                                                                                <%--<div class="table table-responsive">
                                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="myTable1">--%>
                                                                                                <thead class="bg-light-blue">
                                                                                                    <tr>
                                                                                                        <th>Work Experience</th>
                                                                                                        <th>Organization </th>
                                                                                                        <th>Designation  </th>
                                                                                                        <th>Duration - From  </th>
                                                                                                        <th>Duration - To </th>
                                                                                                        <th>Nature of Work </th>
                                                                                                        <th>Nature of Business</th>
                                                                                                        <th>Monthly Remuneration </th>
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
                                                                                    <tr class="item">
                                                                                        <td><%# Eval("WORK_EXPERIENCE")%></td>
                                                                                        <td><%# Eval("ORGANIZATION")%></td>
                                                                                        <td><%# Eval("DESIGNATION")%></td>
                                                                                        <td><%#Eval("START_DURATION","{0:dd-MM-yyyy}") %></td>
                                                                                        <td><%# Eval("END_DURATION", "{0:dd-MM-yyyy}")%></td>
                                                                                        <td><%# Eval("NATURE_WORK")%></td>
                                                                                        <td><%# Eval("NATURE_BUSINESS")%></td>
                                                                                        <td><%# Eval("MONTHLY_REMUNERATION")%></td>
                                                                                    </tr>
                                                                                </ItemTemplate>

                                                                            </asp:ListView>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row" id="divWorkExpRefer" runat="server" visible="false">
                                                                        <div class="col-12">
                                                                            <div class="sub-heading">
                                                                                <h5>Reference Details</h5>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-12">
                                                                            <asp:ListView ID="lvWorkExpRefer" runat="server" Visible="true">
                                                                                <LayoutTemplate>
                                                                                    <div class="vista-grid" style="width: 100%;">

                                                                                        <div id="demo-grid">
                                                                                            <table class="table table-striped table-bordered nowrap" style="width: 100%" id="tblData_workexpref_1">
                                                                                                <%--<div class="table table-responsive">
                                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="myTable1">--%>
                                                                                                <thead class="bg-light-blue">
                                                                                                    <tr>
                                                                                                        <th>Reference Name</th>
                                                                                                        <th>Organization </th>
                                                                                                        <th>Designation  </th>
                                                                                                        <th>Email</th>
                                                                                                        <th>Mobile No </th>
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
                                                                                    <tr class="item">
                                                                                        <td><%# Eval("REFERENCE_NAME")%></td>
                                                                                        <td><%# Eval("ORGANIZATION")%></td>
                                                                                        <td><%# Eval("DESIGNATION")%></td>
                                                                                        <td><%# Eval("EMAIL_ID")%></td>
                                                                                        <td><%# Eval("MOBILE_NO")%></td>
                                                                                    </tr>
                                                                                </ItemTemplate>

                                                                            </asp:ListView>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                                <%-- Work Experience Section end --%>
                                                <%-- Upload Documents Section start --%>
                                                <asp:Panel ID="pnlUploadDocuments" runat="server">
                                                    <div class="card" id="divDocuments" runat="server" visible="false">
                                                        <div class="card-header collapsed" data-toggle="collapse" data-target="#collapseEight" aria-expanded="false" aria-controls="collapseEight">
                                                            <span class="title">Upload Documents Details </span>
                                                            <span class="accicon"><i class="fa fa-angle-down rotate-icon"></i></span>
                                                        </div>
                                                        <div id="collapseEight" class="collapse" data-parent="#StudentDetails">
                                                            <div class="card-body">
                                                                <div class="col-12 mt-3">
                                                                    <div class="row">
                                                                        <div class="col-12">
                                                                            <asp:ListView ID="lvDocuments" runat="server" Visible="true">
                                                                                <LayoutTemplate>
                                                                                    <div class="vista-grid" style="width: 100%;">
                                                                                        <div class="table table-responsive">
                                                                                            <table class="table table-striped table-bordered nowrap" style="width: 100%" id="myTable1">
                                                                                                <thead class="bg-light-blue">
                                                                                                    <tr>
                                                                                                        <th>Document Name</th>
                                                                                                        <th>is Uploaded?</th>
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
                                                                                    <tr class="item">
                                                                                        <td><%# Eval("DOCUMENTNAME")%></td>
                                                                                        <td><%# Eval("IS_UPLOAD_DOCS")%></td>
                                                                                    </tr>
                                                                                </ItemTemplate>

                                                                            </asp:ListView>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                                <%-- Upload Documents Section end --%>
                                                <%-- Reservation Details Section start --%>
                                                <div class="card" id="divParentOfReservation" runat="server" visible="false">
                                                    <div class="card-header collapsed" data-toggle="collapse" data-target="#collapseNine" aria-expanded="false" aria-controls="collapseNine">
                                                        <span class="title">Reservation Details </span>
                                                        <span class="accicon"><i class="fa fa-angle-down rotate-icon"></i></span>
                                                    </div>
                                                    <div id="collapseNine" class="collapse" data-parent="#StudentDetails">
                                                        <div class="card-body">
                                                            <div class="col-12 mt-3">
                                                                <div class="row">

                                                                    <%-- Reservation-1 --%>
                                                                    <div class="col-lg-6 col-md-12 col-12 form-group" id="divReservation1" runat="server" visible="true">
                                                                        <div class="all-ques">
                                                                            <div class="questions">
                                                                                <label><span class="ques-num">1.</span> Sponsored by Union Territory of Lakshadweep</label>
                                                                                <asp:RadioButtonList ID="rdoUnionTerr" Enabled="false" runat="server" RepeatDirection="Horizontal" CssClass="rdo-btn">
                                                                                    <asp:ListItem Value="0"> YES &nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                                                                    <asp:ListItem Value="1" Selected="True"> NO</asp:ListItem>
                                                                                </asp:RadioButtonList>
                                                                            </div>
                                                                            <div class="upload-doc pl-3" id="divUnionTerr" runat="server" visible="false">
                                                                                <label>is Document Uploaded? </label>
                                                                                <div class="form-inline">
                                                                                    <asp:Label ID="lblfuUnionTerrFileName" runat="server" Text="Yes" />
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                    <%-- Reservation-2 --%>
                                                                    <div class="col-lg-6 col-md-12 col-12 form-group" id="divReservation2" runat="server" visible="true">
                                                                        <div class="all-ques">
                                                                            <div class="questions">
                                                                                <label><span class="ques-num">2.</span> Sponsored by Union Territory of Andaman</label>
                                                                                <asp:RadioButtonList ID="rdoUniAndman" Enabled="false" runat="server" RepeatDirection="Horizontal" CssClass="rdo-btn">
                                                                                    <asp:ListItem Value="0"> YES &nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                                                                    <asp:ListItem Value="1" Selected="True"> NO</asp:ListItem>
                                                                                </asp:RadioButtonList>
                                                                            </div>
                                                                            <div class="upload-doc pl-3" id="divUniAndaman" runat="server" visible="false">
                                                                                <label>is Document Uploaded? </label>
                                                                                <div class="form-inline">
                                                                                    <asp:Label ID="lblfuUniAndamanFileName" runat="server" Text="Yes" />
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                    <%-- Reservation-3 --%>
                                                                    <div class="col-lg-6 col-md-12 col-12 form-group" id="divReservation3" runat="server" visible="false">
                                                                        <div class="all-ques">
                                                                            <div class="questions">
                                                                                <label><span class="ques-num">3.</span> Are you eligible for NCC Grace mark?</label>
                                                                                <asp:RadioButtonList ID="rdoNCCGrade" Enabled="false" runat="server" RepeatDirection="Horizontal" CssClass="rdo-btn">
                                                                                    <asp:ListItem Value="0"> YES &nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                                                                    <asp:ListItem Value="1" Selected="True"> NO</asp:ListItem>
                                                                                </asp:RadioButtonList>
                                                                            </div>
                                                                            <div class="col-lg-6 col-md-6 col-12 form-group sub-ques" id="divNCCGradeDDL" runat="server" visible="false">
                                                                                <label>NCC Grade </label>
                                                                                <asp:DropDownList ID="ddlNCCGrade" Enabled="false" runat="server" CssClass="form-control" data-select2-enable="true">
                                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                                    <asp:ListItem Value="1">A</asp:ListItem>
                                                                                    <asp:ListItem Value="2">B</asp:ListItem>
                                                                                    <asp:ListItem Value="3">C</asp:ListItem>
                                                                                    <asp:ListItem Value="4">Participation</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                            <div class="upload-doc pl-3" id="divNCCGradeFile" runat="server" visible="false">
                                                                                <label>is Document Uploaded? </label>
                                                                                <div class="form-inline">
                                                                                    <asp:Label ID="lblfuNCCGradeFileName" runat="server" Text="Yes" />
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                    <%-- Reservation-4 --%>
                                                                    <div class="col-lg-6 col-md-12 col-12 form-group" id="divReservation4" runat="server" visible="false">
                                                                        <div class="all-ques">
                                                                            <div class="questions">
                                                                                <label><span class="ques-num">4.</span> Are you eligible for NSS Grace mark?</label>
                                                                                <asp:RadioButtonList ID="rdoNSSGrade" Enabled="false" runat="server" RepeatDirection="Horizontal" CssClass="rdo-btn">
                                                                                    <asp:ListItem Value="0"> YES &nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                                                                    <asp:ListItem Value="1" Selected="True"> NO</asp:ListItem>
                                                                                </asp:RadioButtonList>
                                                                            </div>
                                                                            <div class="col-lg-6 col-md-6 col-12 form-group sub-ques" id="divNSSGradeDDL" runat="server" visible="false">
                                                                                <label>NSS Grade </label>
                                                                                <asp:DropDownList ID="ddlNSSGrade" runat="server" Enabled="false" CssClass="form-control" data-select2-enable="true">
                                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                                    <asp:ListItem Value="1">Participation</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                            <div class="upload-doc pl-3" id="divNSSGradeFile" runat="server" visible="false">
                                                                                <label>is Document Uploaded? </label>
                                                                                <div class="form-inline">
                                                                                    <asp:Label ID="lblfuNSSGradeFileName" runat="server" Text="Yes" />
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                    <%-- Reservation-5 --%>
                                                                    <div class="col-lg-6 col-md-12 col-12 form-group" id="divReservation5" runat="server" visible="false">
                                                                        <div class="all-ques">
                                                                            <div class="questions">
                                                                                <label><span class="ques-num">5.</span> Others</label>
                                                                                <asp:RadioButtonList ID="rdoOthers" Enabled="false" runat="server" RepeatDirection="Horizontal" CssClass="rdo-btn">
                                                                                    <asp:ListItem Value="1"> Widow/Child Of Jawan &nbsp;&nbsp;</asp:ListItem>
                                                                                    <asp:ListItem Value="2"> Widow/Child Of Ex-Servicemen &nbsp;&nbsp;</asp:ListItem>
                                                                                    <asp:ListItem Value="3"> Ex-Servicemen</asp:ListItem>
                                                                                    <asp:ListItem Value="0" Selected="True"> None &nbsp;&nbsp;</asp:ListItem>
                                                                                </asp:RadioButtonList>
                                                                            </div>
                                                                            <div class="upload-doc pl-3" id="divOtherFile" runat="server" visible="false">
                                                                                <label>is Document Uploaded? </label>
                                                                                <div class="form-inline">
                                                                                    <asp:Label ID="lblfuOtherFileName" runat="server" Text="Yes" />
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                    <%-- Reservation-6 --%>
                                                                    <div class="col-lg-6 col-md-12 col-12 form-group" id="divReservation6" runat="server" visible="false">
                                                                        <div class="all-ques">
                                                                            <div class="questions">
                                                                                <label><span class="ques-num">6.</span> Are you Eligible for Sports/Cultural Quota?</label>
                                                                                <div class="form-inline">
                                                                                    <asp:RadioButtonList ID="rdoSportsCultural" Enabled="false" runat="server" RepeatDirection="Horizontal" CssClass="rdo-btn">
                                                                                        <asp:ListItem Value="0"> YES &nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                                                                        <asp:ListItem Value="1" Selected="True"> NO</asp:ListItem>
                                                                                    </asp:RadioButtonList>
                                                                                    <asp:CheckBoxList ID="chkBtnSportsCultural" Enabled="false" runat="server" Visible="false" RepeatDirection="Horizontal" CssClass="chk-btn">
                                                                                        <asp:ListItem Value="0" Selected="True">Sports &nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                                                                        <asp:ListItem Value="1">Cultural </asp:ListItem>
                                                                                    </asp:CheckBoxList>
                                                                                </div>
                                                                            </div>

                                                                            <%-- Sport Listview --%>
                                                                            <div class="sport" id="divSportsEvent" runat="server" visible="false">
                                                                                <div class="col-12">
                                                                                    <div class="row">
                                                                                        <div class="col-12">
                                                                                            <h5 class="sub-heading">Sports Events</h5>
                                                                                        </div>
                                                                                    </div>

                                                                                    <div class="col-12">
                                                                                        <asp:Panel ID="pnlSportsEvent" runat="server" Visible="true">
                                                                                            <asp:ListView ID="lvSportsEvent" runat="server">
                                                                                                <LayoutTemplate>
                                                                                                    <table class="table table-striped table-bordered nowrap" style="width: 100%" id="divAddSportsEvent">
                                                                                                        <thead class="bg-light-blue">
                                                                                                            <tr>
                                                                                                                <th>Event Name</th>
                                                                                                                <th>Level Reached</th>
                                                                                                            </tr>
                                                                                                        </thead>
                                                                                                        <tbody>
                                                                                                            <tr id="itemPlaceholder" runat="server" />
                                                                                                        </tbody>
                                                                                                    </table>
                                                                                                </LayoutTemplate>
                                                                                                <ItemTemplate>
                                                                                                    <tr class="item">
                                                                                                        <td><%# Eval("EVENT_NAME")%></td>
                                                                                                        <td><%# Eval("LEVEL_NAME")%></td>
                                                                                                    </tr>
                                                                                                </ItemTemplate>
                                                                                            </asp:ListView>
                                                                                        </asp:Panel>
                                                                                    </div>


                                                                                </div>
                                                                                <div class="upload-doc pl-3">
                                                                                    <label>is Document Uploaded? </label>
                                                                                    <div class="form-inline">
                                                                                        <asp:Label ID="lblfuSportsEventFileName" runat="server" Text="Yes" />
                                                                                    </div>
                                                                                </div>
                                                                            </div>

                                                                            <%-- Cultural Listview --%>
                                                                            <%--class="cultural mt-3 d-none"--%>
                                                                            <div class="cultural mt-3" id="divCulturalEvent" runat="server" visible="false">
                                                                                <div class="col-12">
                                                                                    <div class="row">
                                                                                        <div class="col-12">
                                                                                            <h5 class="sub-heading">Cultural Events</h5>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="col-12">
                                                                                        <asp:Panel ID="pnlCulturalEvent" runat="server" Visible="true">
                                                                                            <asp:ListView ID="lvCulturalEvent" runat="server">
                                                                                                <LayoutTemplate>
                                                                                                    <table class="table table-striped table-bordered nowrap" style="width: 100%" id="divAddCulturalEvent">
                                                                                                        <thead class="bg-light-blue">
                                                                                                            <tr>
                                                                                                                <th>Event Name</th>
                                                                                                                <th>Level </th>
                                                                                                                <th>Grade </th>
                                                                                                            </tr>
                                                                                                        </thead>
                                                                                                        <tbody>
                                                                                                            <tr id="itemPlaceholder" runat="server" />
                                                                                                        </tbody>
                                                                                                    </table>
                                                                                                </LayoutTemplate>
                                                                                                <ItemTemplate>
                                                                                                    <tr class="item">
                                                                                                        <td><%# Eval("EVENT_NAME")%></td>
                                                                                                        <td><%# Eval("LEVEL_NAME")%></td>
                                                                                                        <td><%# Eval("GRADE_NAME")%></td>
                                                                                                    </tr>
                                                                                                </ItemTemplate>
                                                                                            </asp:ListView>
                                                                                        </asp:Panel>
                                                                                    </div>

                                                                                </div>
                                                                                <div class="upload-doc pl-3">
                                                                                    <label>is Document Uploaded? </label>
                                                                                    <div class="form-inline">
                                                                                        <asp:Label ID="lblfuCulturalEventFileName" runat="server" Text="Yes" />
                                                                                    </div>
                                                                                </div>
                                                                            </div>

                                                                        </div>
                                                                    </div>

                                                                    <%-- Reservation-7 --%>
                                                                    <div class="col-lg-6 col-md-12 col-12 form-group" id="divReservation7" runat="server" visible="false">
                                                                        <div class="all-ques">
                                                                            <div class="questions">
                                                                                <label><span class="ques-num">7.</span> Are you eligible for reservation under Kerala State Government?</label>
                                                                                <asp:RadioButtonList ID="rdoReservStateQuota" Enabled="false" runat="server" RepeatDirection="Horizontal" CssClass="rdo-btn">
                                                                                    <asp:ListItem Value="0"> YES &nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                                                                    <asp:ListItem Value="1" Selected="True"> NO</asp:ListItem>
                                                                                </asp:RadioButtonList>
                                                                            </div>
                                                                            <div class="col-lg-6 col-md-6 col-12 form-group sub-ques" id="divReservStateQuotaDDL" runat="server" visible="false">
                                                                                <label>Reservation Quota </label>
                                                                                <asp:DropDownList ID="ddlReservStateQuota" runat="server" Enabled="false" CssClass="form-control" data-select2-enable="true">
                                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                                    <asp:ListItem Value="1">SCHEDULED TRIBE</asp:ListItem>
                                                                                    <asp:ListItem Value="2">SCHEDULED CASTE</asp:ListItem>
                                                                                    <asp:ListItem Value="3">COMMUNITY QUOTA(RCSC)</asp:ListItem>
                                                                                    <asp:ListItem Value="4">EZHAVA, THIYYA AND BILLAVA (ETB)</asp:ListItem>
                                                                                    <asp:ListItem Value="5">MUSLIMS</asp:ListItem>
                                                                                    <asp:ListItem Value="6">LATIN CATHOLICS OTHER THAN ANGLO INDIANS</asp:ListItem>
                                                                                    <asp:ListItem Value="7">OTHER BACKWARD CHRISTIANS</asp:ListItem>
                                                                                    <asp:ListItem Value="8">OTHER BACKWARD HINDUS</asp:ListItem>
                                                                                    <asp:ListItem Value="9">COMMUNITY QUOTA(CHRISTIANS)</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                            <div class="upload-doc pl-3" id="divReservStateQuotaFile" runat="server" visible="false">
                                                                                <label>is Document Uploaded? </label>
                                                                                <div class="form-inline">
                                                                                    <asp:Label ID="lblfuReservStateQuotaFileName" runat="server" Text="Yes" />
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                    <%-- Reservation-8 --%>
                                                                    <div class="col-lg-6 col-md-12 col-12 form-group" id="divReservation8" runat="server" visible="false">
                                                                        <div class="all-ques">
                                                                            <div class="questions">
                                                                                <label><span class="ques-num">8.</span> Are you eligible for reservation under person with disability?(PwD)</label>
                                                                                <asp:RadioButtonList ID="rdoReservDisability" Enabled="false" runat="server" RepeatDirection="Horizontal" CssClass="rdo-btn">
                                                                                    <asp:ListItem Value="0"> YES &nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                                                                    <asp:ListItem Value="1" Selected="True"> NO</asp:ListItem>
                                                                                </asp:RadioButtonList>
                                                                            </div>
                                                                            <div class="col-12" id="divReservDisabilityAll" runat="server" visible="false">
                                                                                <div class="row">
                                                                                    <div class="col-lg-6 col-md-6 col-12 form-group sub-ques">
                                                                                        <label>Disability </label>
                                                                                        <asp:DropDownList ID="ddlReservDisability" runat="server" Enabled="false" CssClass="form-control" data-select2-enable="true">
                                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                                            <asp:ListItem Value="1">DEAF </asp:ListItem>
                                                                                            <asp:ListItem Value="2">ORTHOPAEDICALLY CHALLENGED </asp:ListItem>
                                                                                            <asp:ListItem Value="3">PARALYSED </asp:ListItem>
                                                                                            <asp:ListItem Value="4">VISUALLY IMPAIRED </asp:ListItem>
                                                                                            <asp:ListItem Value="5">HYDROCEPHALUS PERITONEAL SHUNT,LEFT MONOPLEGIA </asp:ListItem>
                                                                                        </asp:DropDownList>
                                                                                    </div>

                                                                                    <div class="col-lg-6 col-md-6 col-12 form-group sub-ques">
                                                                                        <label>Disability Percentage </label>
                                                                                        <asp:TextBox ID="txtDisabilityPercent" Enabled="false" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                    </div>

                                                                                    <div class="col-lg-6 col-md-6 col-12 form-group sub-ques" style="margin-bottom: 0.37rem;">
                                                                                        <label>UDID Number </label>
                                                                                        <asp:TextBox ID="txtDisabilityUdidNumber" Enabled="false" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="row">
                                                                                    <div class="upload-doc pl-3">
                                                                                        <label>is Document Uploaded? </label>
                                                                                        <div class="form-inline">
                                                                                            <asp:Label ID="lblfuReservDisabilityFileName" runat="server" Text="Yes" />
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                    <%-- Reservation-9 --%>
                                                                    <div class="col-lg-6 col-md-12 col-12 form-group" id="divReservation9" runat="server" visible="false">
                                                                        <div class="all-ques">
                                                                            <div class="questions">
                                                                                <label><span class="ques-num">9.</span> Any Other Disability?</label>
                                                                                <div class="col-lg-6 col-md-6 col-12 form-group sub-ques mt-1">
                                                                                    <asp:TextBox ID="txtOtherDisability" Enabled="false" runat="server" CssClass="form-control" placeholder="Enter Other Disability Here"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                            <div class="upload-doc pl-3">
                                                                                <label>is Document Uploaded? </label>
                                                                                <div class="form-inline">
                                                                                    <asp:Label ID="lblfuOtherDisabilityFileName" runat="server" Text="Yes" />
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                    <%-- Reservation-10 --%>
                                                                    <div class="col-lg-6 col-md-12 col-12 form-group" id="divReservation10" runat="server" visible="false">
                                                                        <div class="all-ques">
                                                                            <div class="questions">
                                                                                <label><span class="ques-num">10.</span> Have you received Nanmamudra or Rajyapuraskar in Higher Secondary Level as Scout/Guide/Ranger/Rover?</label>
                                                                                <asp:RadioButtonList ID="rdoNanmamudra" Enabled="false" runat="server" RepeatDirection="Horizontal" CssClass="rdo-btn">
                                                                                    <asp:ListItem Value="0"> YES &nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                                                                    <asp:ListItem Value="1" Selected="True"> NO</asp:ListItem>
                                                                                </asp:RadioButtonList>
                                                                            </div>
                                                                            <div class="upload-doc pl-3" id="divNanmamudraFile" runat="server" visible="false">
                                                                                <label>is Document Uploaded? </label>
                                                                                <div class="form-inline">
                                                                                    <asp:Label ID="lblfuNanmamudraFileName" runat="server" Text="Yes" />
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                    <%-- Reservation-11 --%>
                                                                    <div class="col-lg-6 col-md-12 col-12 form-group" id="divReservation11" runat="server" visible="false">
                                                                        <div class="all-ques">
                                                                            <div class="questions">
                                                                                <label><span class="ques-num">11.</span> Student Police Cadets</label>
                                                                                <asp:RadioButtonList ID="rdoPoliceCadets" Enabled="false" runat="server" RepeatDirection="Horizontal" CssClass="rdo-btn">
                                                                                    <asp:ListItem Value="0"> YES &nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                                                                    <asp:ListItem Value="1" Selected="True"> NO</asp:ListItem>
                                                                                </asp:RadioButtonList>
                                                                            </div>
                                                                            <div class="upload-doc pl-3" id="divPoliceCadetsFile" runat="server" visible="false">
                                                                                <label>is Document Uploaded? </label>
                                                                                <div class="form-inline">
                                                                                    <asp:Label ID="lblfuPoliceCadetsFileName" runat="server" Text="Yes" />
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <%-- Reservation Details Section end --%>

                                                <%-- Final Submission Section start --%>
                                                <asp:Panel ID="pnlFinalSubmission" runat="server">
                                                    <div class="card" id="divFinalSubmission" runat="server" visible="false">
                                                        <div class="card-header collapsed" data-toggle="collapse" data-target="#collapseTen" aria-expanded="false" aria-controls="collapseTen">
                                                            <span class="title">Final Submission Details </span>
                                                            <span class="accicon"><i class="fa fa-angle-down rotate-icon"></i></span>
                                                        </div>
                                                        <div id="collapseTen" class="collapse" data-parent="#StudentDetails">
                                                            <div class="card-body">
                                                                <div class="col-12 mt-3">
                                                                    <div class="row">
                                                                        <div class="form-group col-lg-6 col-md-12 col-12">
                                                                            <label>How do you know about DAIICT</label>
                                                                            <div class="chk-box">
                                                                                <asp:CheckBoxList ID="chkSourcesFrom" runat="server" Enabled="false" RepeatColumns="2">
                                                                                    <asp:ListItem Value="0"> Newspaper Advertisement &nbsp;&nbsp;&nbsp;</asp:ListItem>
                                                                                    <asp:ListItem Value="1"> SMS/Email &nbsp;&nbsp;&nbsp;</asp:ListItem>
                                                                                    <asp:ListItem Value="2"> Friends/Relatives &nbsp;&nbsp;&nbsp;</asp:ListItem>
                                                                                    <asp:ListItem Value="3"> Google Search &nbsp;&nbsp;&nbsp;</asp:ListItem>
                                                                                    <asp:ListItem Value="4"> Social Media &nbsp;&nbsp;&nbsp;</asp:ListItem>
                                                                                    <asp:ListItem Value="5"> Coaching Centre </asp:ListItem>
                                                                                    <asp:ListItem Value="6"> Other</asp:ListItem>
                                                                                </asp:CheckBoxList>
                                                                            </div>
                                                                        </div>

                                                                        <div class="form-group col-lg-6 col-md-12 col-12" id="divOtherSources" runat="server" visible="false">
                                                                            <label>Other Sources</label>
                                                                            <asp:TextBox ID="txtOtherSources" runat="server" CssClass="form-control" Enabled="false" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                                                        </div>

                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                                <%-- Final Submission Section end --%>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-7 col-md-6 col-12" id="divDocumentList" runat="server" style="display: none">
                                        <div class="sub-heading">
                                            <h5>Document List</h5>
                                        </div>
                                        <asp:UpdatePanel ID="UpdatePanel00" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:ListView ID="lvVerifyDocumentList" runat="server">
                                                    <LayoutTemplate>
                                                        <div class="vista-grid" style="width: 100%;">
                                                            <div class="table table-responsive">
                                                                <table class="table table-striped table-bordered nowrap" id="tblData2">
                                                                    <thead class="bg-light-blue">
                                                                        <tr>
                                                                            <th>
                                                                                <asp:CheckBox ID="chkAllVerify" runat="server" onchange="CheckAll(this,2)" /></th>
                                                                            <%-- onchange="CheckAll(this,2)"--%>
                                                                            <th>Sr No</th>
                                                                            <th>Document Name</th>
                                                                            <th>Preview</th>
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
                                                        <tr class="item">
                                                            <td>
                                                                <asp:CheckBox ID="chkDocument" runat="server" Checked='<%# (Convert.ToInt32(Eval("ISVERIFY") )== 1 ? true:false)%>' Enabled='<%# (Convert.ToInt32(Eval("RESULT_NOT_ANNOUNCED") )== 0 ? true:false)%>' />
                                                            </td>
                                                            <td><%# Container.DataItemIndex + 1 %></td>
                                                            <td>
                                                                <asp:Label ID="lblDocName" runat="server" Text='<%#Eval ("DOCUMENTNAME") %>'></asp:Label>
                                                                <asp:HiddenField ID="hdnDocumentNo" runat="server" Value='<%#Eval ("DOCUMENTNO") %>' />
                                                                <asp:HiddenField ID="hdnFilename" runat="server" Value='<%#Eval ("FILENAME") %>' />
                                                                <asp:HiddenField ID="hdnDocumentFrom" runat="server" Value='<%#Eval ("DOCUMENTFROM") %>' />
                                                                <asp:HiddenField ID="hdnUserNo" runat="server" Value='<%#Eval ("USERNO") %>' />
                                                            </td>
                                                            <td style="text-align: center">

                                                                <asp:UpdatePanel ID="UpdatePanel10" runat="server" UpdateMode="Conditional">
                                                                    <ContentTemplate>
                                                                        <asp:ImageButton ID="btnDocVerifyPreview" runat="server" OnClick="btnDocVerifyPreview_Click" CommandArgument='<%# Eval("FILENAME")%>'
                                                                            ImageUrl="~/Images/view2.png" CommandName='<%# Eval("DOCUMENTFROM")+","+Eval("USERNO")+","+Eval("QUALIFYNO")%>' Enabled='<%# (Convert.ToInt32(Eval("RESULT_NOT_ANNOUNCED") )== 0 ? true:false)%>' />
                                                                    </ContentTemplate>
                                                                    <Triggers>
                                                                        <asp:AsyncPostBackTrigger ControlID="btnDocVerifyPreview" />
                                                                    </Triggers>
                                                                </asp:UpdatePanel>

                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>

                                        <div class="form-group col-lg-12 col-md-12 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Remark</label>
                                            </div>
                                            <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="2"></asp:TextBox>
                                        </div>
                                    </div>
                                    <%--  <div class="form-group col-lg-6 col-md-6 col-12">
                                    </div>--%>
                                    <div class="form-group col-lg-7 col-md-6 col-12">
                                        <asp:Panel ID="pnlPopup" runat="server" Visible="false">
                                            <%--CssClass="modalPopup"--%>
                                            <div class="header">
                                                <button type="button" class="close" id="clzbtn">&times;</button>
                                            </div>
                                            <div class="body">
                                                <iframe runat="server" width="630px" height="500px" id="iframeView"></iframe>
                                            </div>
                                            <%-- <div class="footer" align="right">
                                                <asp:Button ID="btnClose" runat="server" ForeColor="Red" Font-Bold="true" Font-Size="Larger" Text="Close" CssClass="no" />
                                            </div>--%>
                                        </asp:Panel>
                                    </div>
                                    <div class="form-group col-lg-5 col-md-6 col-12" id="divMarksheetList" runat="server" style="display: none">
                                        <div style="width: 100%;">
                                            <div class="header">
                                                <button type="button" class="close" id="btncloseMarksheet">&times;</button>
                                            </div>
                                            <%--  <asp:UpdatePanel ID="updsubject" runat="server">
                                        <ContentTemplate>--%>
                                            <asp:Panel ID="pnlSubject" runat="server" Style="display: none">
                                                <asp:ListView ID="lvSubject" runat="server">
                                                    <LayoutTemplate>
                                                        <div id="demo-grid">
                                                            <div class="sub-heading" id="dem">
                                                                <h5>Subject Details List</h5>
                                                            </div>
                                                            <table class="table table-striped table-bordered nowrap" style="width: 100%" id="myTable1">
                                                                <thead class="bg-light-blue">
                                                                    <tr>
                                                                        <td>Sr No.</td>
                                                                        <th>Subject Type</th>
                                                                        <th>Subject</th>
                                                                        <th>Maximum Marks</th>
                                                                        <th>Marks Obtained</th>
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
                                                            <td style="text-align: center"><%# Container.DataItemIndex + 1 %></td>
                                                            <td><%#Eval ("SUBJECTTYPE") %></td>
                                                            <td><%#Eval ("SUBJECTNAME") %></td>
                                                            <td><%#Eval ("OUT_OF_MARKS") %></td>
                                                            <td><%#Eval ("OBTAINED_MARKS") %></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>

                                            </asp:Panel>
                                            <%-- </ContentTemplate>
                                        <Triggers>
                                         
                                        </Triggers>
                                    </asp:UpdatePanel>--%>
                                            <asp:Panel ID="pnlGrade" runat="server" Style="display: none">
                                                <asp:ListView ID="lvGrade" runat="server">
                                                    <LayoutTemplate>
                                                        <div id="demo-grid">
                                                            <div class="sub-heading" id="dem">
                                                                <h5>Grade Details List</h5>
                                                            </div>
                                                            <table class="table table-striped table-bordered nowrap" style="width: 100%" id="myTable1">
                                                                <thead class="bg-light-blue">
                                                                    <tr>
                                                                        <td>Sr No.</td>
                                                                        <th>Subject Type</th>
                                                                        <th>Subject</th>
                                                                        <th>Grade</th>
                                                                        <th>Max Grade Point</th>
                                                                        <th>Grade Point Obtained</th>
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
                                                            <td style="text-align: center"><%# Container.DataItemIndex + 1 %></td>
                                                            <td><%#Eval ("SUBJECTTYPE") %></td>
                                                            <td><%#Eval ("SUBJECTNAME") %></td>
                                                            <td><%#Eval ("GRADENAME") %></td>
                                                            <td><%#Eval ("MAXGRADEPOINT") %></td>
                                                            <td><%#Eval ("CGPA") %></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <!-- Modal footer -->
                            <div class="modal-footer">
                                <div class="divbtnsub" runat="server" id="divbtnsub">
                                    <asp:Button ID="btnSubmitDocVerify" runat="server" class="btn btn-primary" Text="Submit" OnClick="btnSubmitDocVerify_Click" />
                                </div>
                                <button type="button" class="btn btn-outline-danger" data-dismiss="modal">Close</button>
                            </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        $('#ctl00_ContentPlaceHolder1_pnlPopup').show()
        jQuery('#clzbtn').on('click', function () {
            //$("[id*=btnSubmitDocVerify]").show();
            jQuery('#ctl00_ContentPlaceHolder1_pnlPopup').toggle();
            $("[id*=divDocumentList]").show();
            $("[id*=divMarksheetList]").hide();
            $("[id*=btnSubmitDocVerify]").removeClass("d-none");
            $("[id*=btnSubmitDocVerify]").addClass("btn btn-primary");
        })
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            jQuery('#clzbtn').on('click', function () {
                //$("[id*=btnSubmitDocVerify]").show();
                jQuery('#ctl00_ContentPlaceHolder1_pnlPopup').toggle();
                $("[id*=divDocumentList]").show();
                $("[id*=divMarksheetList]").hide();
                $("[id*=btnSubmitDocVerify]").removeClass("d-none");
                $("[id*=btnSubmitDocVerify]").addClass("btn btn-primary");
            })
        });

        jQuery('#btncloseMarksheet').on('click', function () {
            $("[id*=divDocumentList]").show();
            $("[id*=divMarksheetList]").hide();
            $("[id*=pnlPopup]").hide();
            $("[id*=btnSubmitDocVerify]").removeClass("d-none");
            $("[id*=btnSubmitDocVerify]").addClass("btn btn-primary");

            //$("[id*=btnSubmitDocVerify]").show();
        })
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            jQuery('#btncloseMarksheet').on('click', function () {
                $("[id*=divDocumentList]").show();
                $("[id*=divMarksheetList]").hide();
                $("[id*=pnlPopup]").hide();
                $("[id*=btnSubmitDocVerify]").removeClass("d-none");
                $("[id*=btnSubmitDocVerify]").addClass("btn btn-primary");
                //$("[id*=btnSubmitDocVerify]").show();
            })
        });

        $(document).ready(function () {
            DivEmailTemplate();   
        });
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {

            DivEmailTemplate();
        });

        function OpenPreview() {
            var htmltext = $('#StudentDetails').html();
            $('#ctl00_ContentPlaceHolder1_PPreviewVerify').prop('innerHTML', htmltext);
            $('#modalDetails').modal('show');

        }
        function ModelPopupDocumentVerify() {
            // var htmltext = $('#StudentDetails').html();
            // $('#ctl00_ContentPlaceHolder1_updBindModel #StudentDetails').prop('innerHTML', htmltext);
            $('#ctl00_ContentPlaceHolder1_updBindModel #StudentDetails .col-lg-4').removeClass('col-lg-4');
            $('#ctl00_ContentPlaceHolder1_updBindModel #StudentDetails .col-md-6').removeClass('col-md-6');
            $('#ctl00_ContentPlaceHolder1_updBindModel #StudentDetails .col-lg-5').removeClass('col-lg-5');
            $('#ctl00_ContentPlaceHolder1_updBindModel #StudentDetails .col-md-12').removeClass('col-md-12');
            $('#ctl00_ContentPlaceHolder1_updBindModel #StudentDetails .col-lg-6').removeClass('col-lg-6');

            $('#ModelVerification').modal('show');
        }



        function CheckAll(checkid, tblId) {
            var updateButtons = $('#tblData' + tblId + ' input[type=checkbox]');
            if ($(checkid).children().is(':checked')) {
                updateButtons.each(function () {

                    if (($(this).attr("id") != $(checkid).children().attr("id"))) {
                        //&& ($(checkid).children().attr("id") != $(checkid).children().attr('disabled'))
                        $(this).prop("checked", true);
                    }
                });
            }
            else {
                updateButtons.each(function () {
                    if ($(this).attr("id") != $(checkid).children().attr("id")) {
                        $(this).prop("checked", false);
                    }
                });
            }
        }
        function DivEmailTemplate() {
            if ($("[id*=chkCommunication]").prop('checked') == true) {
                $("[id*=divEmailTemplate]").show();
                $("[id*=divEmailSMS]").show();
                $("[id*=divTemplateType]").hide();
                $("[id*=divTemplateName]").hide();
                $("[id*=btnSendEmail]").show();
                //  $("[id*=btnSubmit]").hide();
                // $("[id*=btnSubmitDocVerify]").show();
            }
            else {
                //    $("[id*=divEmailTemplate]").hide();
                $("[id*=divEmailSMS]").hide();
                $("[id*=divTemplateType]").hide();
                $("[id*=divTemplateName]").hide();
                $("[id*=btnSendEmail]").hide();
                //  $("[id*=btnSubmit]").show();
                $("[id*=chkEmail]").prop("checked", false);
                $("[id*=chkSMS]").prop("checked", false);
                $("[id*=chkWhatsApp]").prop("checked", false);

                //$("[id*=btnSubmitDocVerify]").show(); 07012023
            }

            if ($("[id*=chkEmail]").prop('checked') == true || $("[id*=chkWhatsApp]").prop('checked') == true) {
                $("[id*=divTemplateType]").show();
                $("[id*=divTemplateName]").show();
                // $("[id*=btnSendEmail]").show();
            }
            else {
                $("[id*=divTemplateType]").hide();
                $("[id*=divTemplateName]").hide();
                // $("[id*=btnSendEmail]").show();
            }

        }


        function CheckAll1(checkid) {
           
            var updateButtons = $('#ctl00_ContentPlaceHolder1_gvApplicationProcess input[type=checkbox]');
            
            if ($(checkid).children().is(':checked')) {
                updateButtons.each(function () {

                    if (($(this).attr("id") != $(checkid).children().attr("id"))) {
                        //&& ($(checkid).children().attr("id") != $(checkid).children().attr('disabled'))
                        $(this).prop("checked", true);
                    }
                });
            }
            else {
                updateButtons.each(function () {
                    if ($(this).attr("id") != $(checkid).children().attr("id")) {
                        $(this).prop("checked", false);
                    }
                });
            }
        }

        function showDivEmailSms() {
            var valcommunication = $("[id*=chkCommunication]").prop('checked');
            if (valcommunication == true) {
                $("[id*=btnSendEmail]").show();
                $("[id*=btnSubmit]").hide();
                //    $("[id*=divEmailTemplate]").show();
                $("[id*=divEmailSMS]").show();
                $("[id*=divTemplateType]").hide();
                $("[id*=divTemplateName]").hide();
            }
            else {
                $("[id*=btnSendEmail]").hide();
                $("[id*=btnSubmit]").show();
                //  $("[id*=btnSubmit]").attr("disabled", true);
                //   $("[id*=divEmailTemplate]").hide();
                $("[id*=divEmailSMS]").hide();
                $("[id*=divTemplateType]").hide();
                $("[id*=divTemplateName]").hide();
                //  $("[id*=btnSubmit]").removeAttr("disabled");
            }
            $("[id*=chkEmail]").prop("checked", false);
            $("[id*=chkSMS]").prop("checked", false);
            $("[id*=chkWhatsApp]").prop("checked", false);
        }
        function showDivTemplate() {
            // $("[id*=ddlTemplateType]").val("0").trigger("change");
            // $("[id*=ddlTemplateName]").val("0").trigger("change");
            var valemail = $("[id*=chkEmail]").prop('checked');
            var valwhatsapp = $("[id*=chkWhatsApp]").prop('checked');
            if (valemail == true || valwhatsapp == true) {
                $("[id*=divTemplateType]").show();
                $("[id*=divTemplateName]").show();
                //   $("[id*=btnSubmit]").hide();
            }
            else {
                $("[id*=divTemplateType]").hide();
                $("[id*=divTemplateName]").hide();
            }

        }


        $(".scroll").click(function (event) {
            $('html,body').animate({ scrollTop: $(this.hash).offset().top }, 500);
        });
    </script>
    <script>
        function toggleSearch(searchBar, table) {
            var tableBody = table.querySelector('tbody');
            var allRows = tableBody.querySelectorAll('tr');
            var val = searchBar.value.toLowerCase();
            allRows.forEach((row, index) => {
                var insideSearch = row.innerHTML.trim().toLowerCase();
            //console.log('data',insideSearch.includes(val),'searchhere',insideSearch);
            if (insideSearch.includes(val)) {
                row.style.display = 'table-row';
            }
            else {
                row.style.display = 'none';
            }

        });
        }

        function test5() {
            var searchBar5 = document.querySelector('#FilterData');
            var table5 = document.querySelector('#ctl00_ContentPlaceHolder1_gvApplicationProcess');
            //console.log(allRows);
            //searchBar5.addEventListener('focusout', () => {
            searchBar5.addEventListener('keyup', () => {
                toggleSearch(searchBar5, table5);
        });

        $(".saveAsExcel").click(function () {
            let UserCall = prompt("Please Enter Password:");
            var ExcelDetails = '<%=Session["ExcelDetails"] %>';
            if (UserCall == null || UserCall == "") {
                return false;
            } else {
                if(UserCall == ExcelDetails)
                {

                }
                else {
                    alert('Password is not matched !!!')
                    return false;
                }
            }
            //if (confirm('Do You Want To Apply for New Program?') == true) {
            //    return false;
            //}
            var workbook = XLSX.utils.book_new();
            var allDataArray = [];
            allDataArray = makeTableArray(table5, allDataArray);
            var worksheet = XLSX.utils.aoa_to_sheet(allDataArray);
            workbook.SheetNames.push("Test");
            workbook.Sheets["Test"] = worksheet;
            XLSX.writeFile(workbook, "LeadData.xlsx");
        });
        }

        function makeTableArray(table, array) {
            var allTableRows = table.querySelectorAll('tr');
            allTableRows.forEach(row => {
                var rowArray = [];
            if (row.querySelector('td')) {
                var allTds = row.querySelectorAll('td');
                allTds.forEach(td => {
                    if (td.querySelector('span')) {
                        rowArray.push(td.querySelector('span').textContent);
            }
            else if (td.querySelector('input')) {
                rowArray.push(td.querySelector('input').value);
            }
            else if (td.querySelector('select')) {
                rowArray.push(td.querySelector('select').value);
            }
            else if (td.innerText) {
                rowArray.push(td.innerText);
            }
            else{
                rowArray.push('');
            }
        });
        }
        if (row.querySelector('th')) {
            var allThs = row.querySelectorAll('th');
            allThs.forEach(th => {
                if (th.textContent) {
                    rowArray.push(th.textContent);
        }
        else {
            rowArray.push('');
        }
        });
        }
        // console.log(allTds);

        array.push(rowArray);
        });
        return array;
        }

    </script>
</asp:Content>
