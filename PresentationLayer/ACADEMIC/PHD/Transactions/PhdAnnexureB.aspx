<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="PhdAnnexureB.aspx.cs" Inherits="Academic_StudentInfoEntry" UICulture="auto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server"
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

    <style>
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
            border-radius: 0rem;
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
        #ctl00_ContentPlaceHolder1_lvStudent_Panel2 .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>

    <!--Create New User-->
    <asp:Panel ID="pnDisplay" runat="server">
        <div class="row">
            <div class="col-md-12 col-sm-12 col-12">
                <div class="box box-primary">
                    <div class="box-header with-border">
                        <h3 class="box-title">PHD ANNEXURE-B 
                        </h3>
                    </div>

                    <div class="box-body">
                        <asp:UpdatePanel ID="updEdit" runat="server">
                            <ContentTemplate>
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Search Criteria</label>
                                            </div>
                                            <asp:DropDownList runat="server" class="form-control" ID="ddlSearch" AutoPostBack="true" AppendDataBoundItems="true" data-select2-enable="true" OnSelectedIndexChanged="ddlSearch_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                <%-- <asp:ListItem>Please Select</asp:ListItem>
                                                        <asp:ListItem>BRANCH</asp:ListItem>
                                                        <asp:ListItem>ENROLLMENT NUMBER</asp:ListItem>
                                                        <asp:ListItem>REGISTRATION NUMBER</asp:ListItem>
                                                        <asp:ListItem>FatherName</asp:ListItem>
                                                        <asp:ListItem>IDNO</asp:ListItem>
                                                        <asp:ListItem>MOBILE NUMBER</asp:ListItem>
                                                        <asp:ListItem>MotherName</asp:ListItem>
                                                        <asp:ListItem>NAME</asp:ListItem>
                                                        <asp:ListItem>ROLLNO</asp:ListItem>
                                                        <asp:ListItem>SEMESTER</asp:ListItem>--%>
                                            </asp:DropDownList>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divpanel">

                                            <asp:Panel ID="pnltextbox" runat="server">
                                                <div id="divtxt" runat="server" style="display: block">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Search String</label>
                                                    </div>
                                                    <%--onkeypress="return Validate()"--%>
                                                    <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                                </div>
                                            </asp:Panel>

                                            <asp:Panel ID="pnlDropdown" runat="server">
                                                <div id="divDropDown" runat="server" style="display: block">
                                                    <div class="label-dynamic">
                                                        <%-- <label id="lblDropdown"></label>--%>
                                                        <asp:Label ID="lblDropdown" Style="font-weight: bold" runat="server"></asp:Label>
                                                    </div>
                                                    <asp:DropDownList runat="server" class="form-control" ID="ddlDropdown" AppendDataBoundItems="true" data-select2-enable="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>

                                                    </asp:DropDownList>

                                                </div>
                                            </asp:Panel>

                                        </div>
                                    </div>
                                    <div class="col-12 btn-footer">
                                        <%-- OnClientClick="return submitPopup(this.name);"--%>
                                        <asp:Button ID="Button1" runat="server" Text="Search" OnClick="btnSearch_Click" CssClass="btn btn-primary" OnClientClick="return submitPopup(this.name);" />
                                        <asp:Button ID="btnClose" runat="server" Text="Cancel" CssClass="btn btn-warning" OnClick="btnClose_Click" OnClientClick="return CloseSearchBox(this.name)" data-dismiss="modal" />
                                    </div>
                                    <div class="col-12 btn-footer">
                                        <asp:Label ID="lblNoRecords" runat="server" SkinID="lblmsg" />
                                    </div>
                                </div>

                                <div class="col-12">
                                    <asp:Panel ID="Panellistview" runat="server">
                                        <asp:ListView ID="lvStudent" runat="server">
                                            <LayoutTemplate>
                                                <div>
                                                    <div class="sub-heading">
                                                        <h5>Student List</h5>
                                                    </div>
                                                    <asp:Panel ID="Panel2" runat="server">
                                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                            <thead class="bg-light-blue">
                                                                <tr>
                                                                    <th>Name
                                                                    </th>
                                                                    <th style="display: none">IdNo
                                                                    </th>
                                                                    <th>
                                                                        <asp:Label ID="lblDYRRNo" runat="server" Font-Bold="true"></asp:Label>
                                                                    </th>
                                                                    <th><%--Branch--%>
                                                                        <asp:Label ID="lblDYtxtBranch" runat="server" Font-Bold="true"></asp:Label>
                                                                    </th>
                                                                    <th><%--Semester--%>
                                                                        <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>
                                                                    </th>
                                                                    <th>Father Name
                                                                    </th>
                                                                    <th>Mother Name
                                                                    </th>
                                                                    <th>Mobile No.
                                                                    </th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <tr id="itemPlaceholder" runat="server" />
                                                            </tbody>
                                                        </table>
                                                    </asp:Panel>
                                                </div>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <asp:LinkButton ID="lnkId" runat="server" Text='<%# Eval("Name") %>' CommandArgument='<%# Eval("IDNo") %>'
                                                            OnClick="lnkId_Click"></asp:LinkButton>
                                                    </td>
                                                    <td style="display: none">
                                                        <%# Eval("idno")%>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblstuenrollno" runat="server" Text='<%# Eval("EnrollmentNo")%>' ToolTip='<%# Eval("IDNO")%>'></asp:Label>

                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblstudentfullname" runat="server" Text='<%# Eval("longname")%>' ToolTip='<%# Eval("IDNO")%>'></asp:Label>

                                                    </td>
                                                    <td>
                                                        <%# Eval("SEMESTERNO")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("FATHERNAME") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("MOTHERNAME") %>
                                                    </td>
                                                    <td>
                                                        <%#Eval("STUDENTMOBILE") %>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </asp:Panel>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="lvStudent" />
                            </Triggers>
                        </asp:UpdatePanel>


                        <asp:Panel ID="pnlmain" runat="server">
                            <div id="divmain" runat="server">
                                <div class="accordion" id="accordionExample">
                                    <div class="card">
                                        <div class="card-header" data-toggle="collapse" data-target="#collapseOne" aria-expanded="true">
                                            <span class="title">General Information</span>
                                            <span class="accicon"><i class="fa fa-angle-down rotate-icon"></i></span>
                                        </div>


                                        <div id="collapseOne" class="collapse show">
                                            <div class="card-body">
                                                <div id="divGeneralInfo" style="display: block;">
                                                    <div style="display: none">
                                                        <asp:Panel ID="pnlId" runat="server" Visible="false">
                                                            <div class="form-group col-sm-4 col-sm-offset-4">
                                                                <label>ID No.</label>
                                                                <div class="input-group date">
                                                                    <asp:TextBox ID="txtIDNo" runat="server" class="form-control" TabIndex="1" Enabled="False" />
                                                                    <%--  Enable the button so it can be played again --%>
                                                                    <div class="input-group-addon">
                                                                        <asp:Image ID="imgSearch" runat="server" ImageUrl="~/IMAGES/search.png" TabIndex="1" data-toggle="modal" data-target="#myModal"
                                                                            AlternateText="Search" ToolTip="Search Student by IDNo, Name, Reg. No, Branch, Semester" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </asp:Panel>
                                                    </div>

                                                    <asp:Panel ID="pnlstudinfo" runat="server">
                                                        <div class="col-12" id="divdetails" runat="server">
                                                            <div class="row">
                                                                <div class="col-lg-6 col-md-6 col-12">
                                                                    <ul class="list-group list-group-unbordered">
                                                                        <li class="list-group-item"><b>ID No. :</b>
                                                                            <a class="sub-label">
                                                                                <asp:Label ID="lblRegNo" runat="server" Font-Bold="True"></asp:Label>
                                                                            </a>
                                                                        </li>
                                                                        <li class="list-group-item"><b>Enrollment No. :</b>
                                                                            <a class="sub-label">
                                                                                <asp:Label ID="lblEnrollNo" runat="server" Font-Bold="True"></asp:Label></a>
                                                                        </li>
                                                                        <li class="list-group-item"><b>Date of Joining :</b>
                                                                            <a class="sub-label">
                                                                                <asp:Label ID="lblDateOfJoining" runat="server" Font-Bold="True"></asp:Label></a>
                                                                        </li>
                                                                        <li class="list-group-item"><b>Status :</b>
                                                                            <a class="sub-label">
                                                                                <asp:Label ID="lblStatus" runat="server" Font-Bold="True"></asp:Label></a>
                                                                        </li>
                                                                    </ul>
                                                                </div>
                                                                <div class="col-lg-6 col-md-6 col-12">
                                                                    <ul class="list-group list-group-unbordered">
                                                                        <li class="list-group-item"><b>Student Name :</b>
                                                                            <a class="sub-label">
                                                                                <asp:Label ID="lblStudName" runat="server" Font-Bold="True"></asp:Label>
                                                                            </a>
                                                                        </li>
                                                                        <li class="list-group-item"><b>Father Name :</b>
                                                                            <a class="sub-label">
                                                                                <asp:Label ID="lblFatherName" runat="server" Font-Bold="True"></asp:Label>
                                                                            </a>
                                                                        </li>
                                                                        <li class="list-group-item"><b>Department :</b>
                                                                            <a class="sub-label">
                                                                                <asp:Label ID="lblBranch" runat="server" Font-Bold="true"></asp:Label>
                                                                            </a>
                                                                        </li>
                                                                        <li class="list-group-item"><b>Supervisor :</b>
                                                                            <a class="sub-label">
                                                                                <asp:Label ID="lblSupervisor" runat="server" Font-Bold="True"></asp:Label></a>
                                                                        </li>
                                                                    </ul>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </asp:Panel>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <asp:Panel ID="pnlinfo" runat="server">
                                        <div class="card">
                                            <div class="card-header collapsed" data-toggle="collapse" data-target="#collapseTwo" aria-expanded="true" aria-controls="collapseTwo">
                                                <span class="title">Date of Written Test</span>
                                                <span class="accicon"><i class="fa fa-angle-down rotate-icon"></i></span>
                                            </div>
                                            <div id="collapseTwo" class="collapse collapse show">
                                                <div class="card-body">
                                                    <div class="col-12">
                                                        <div class="row">
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <label>Attempt 1 </label>
                                                                </div>
                                                                <div class="input-group date">
                                                                    <div class="input-group-addon">
                                                                        <i class="fa fa-calendar" id="txtAttemptDate"></i>
                                                                    </div>
                                                                    <asp:TextBox ID="txtAttempt1Date" runat="server" TabIndex="2" ValidationGroup="academic" class="form-control" />

                                                                    <ajaxToolKit:CalendarExtender ID="ceAttempt1date" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                                        PopupButtonID="txtAttemptDate" TargetControlID="txtAttempt1Date" />
                                                                    <ajaxToolKit:MaskedEditExtender ID="meeAttempt1Date" runat="server" TargetControlID="txtAttempt1Date"
                                                                        Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                                        AcceptNegative="Left" ErrorTooltipEnabled="True" />
                                                                </div>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <label>Attempt 2 (if applicable) </label>
                                                                </div>
                                                                <div class="input-group date">
                                                                    <div class="input-group-addon">
                                                                        <i class="fa fa-calendar" id="txtAttemptDate2"></i>
                                                                    </div>
                                                                    <asp:TextBox ID="txtAttempt2Date" runat="server"
                                                                        ToolTip="Please Select Attempt2 Date" class="form-control" TabIndex="3"></asp:TextBox>
                                                                    <ajaxToolKit:CalendarExtender ID="ceAttempt2date" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                                        PopupButtonID="txtAttemptDate2" TargetControlID="txtAttempt2Date" />
                                                                    <ajaxToolKit:MaskedEditExtender ID="meeAttempt2Date" runat="server" TargetControlID="txtAttempt2Date"
                                                                        Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                                        AcceptNegative="Left" ErrorTooltipEnabled="True" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="card" id="dvRemark" runat="server">
                                            <div class="card-header collapsed" data-toggle="collapse" data-target="#collapseThree" aria-expanded="true">
                                                <span class="title" id="tr1" runat="server">Date of Oral Test</span>
                                                <span class="accicon"><i class="fa fa-angle-down rotate-icon"></i></span>
                                            </div>
                                            <div id="collapseThree" class="collapse collapse show">
                                                <div class="card-body">
                                                    <div class="col-12">
                                                        <div class="row">
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Attempt 1 </label>
                                                                </div>
                                                                <div class="input-group date">
                                                                    <div class="input-group-addon">
                                                                        <i class="fa fa-calendar" id="txtOralAttemptDate"></i>
                                                                    </div>
                                                                    <asp:TextBox ID="txtOralAttempt1Date" runat="server" class="form-control" ToolTip="Please Select Oral Attempt1 Date" TabIndex="4"></asp:TextBox>
                                                                    <ajaxToolKit:CalendarExtender ID="ceOralAttempt1date" runat="server" Enabled="True"
                                                                        Format="dd/MM/yyyy" PopupButtonID="txtOralAttemptDate" TargetControlID="txtOralAttempt1Date" />
                                                                    <ajaxToolKit:MaskedEditExtender ID="meeOralAttempt1Date" runat="server" AcceptNegative="Left"
                                                                        DisplayMoney="Left" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date"
                                                                        MessageValidatorTip="true" TargetControlID="txtOralAttempt1Date" />
                                                                    <asp:RequiredFieldValidator ID="rfvOralAttempt1Date" runat="server" ControlToValidate="txtOralAttempt1Date"
                                                                        Display="None" ErrorMessage="Please Enter Oral Attempt1 Date" SetFocusOnError="True"
                                                                        ValidationGroup="Academic"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <label>Attempt 2 (if applicable) </label>
                                                                </div>

                                                                <div class="input-group date">
                                                                    <div class="input-group-addon">
                                                                        <i class="fa fa-calendar" id="txtOralAttemptDate2"></i>
                                                                    </div>
                                                                    <asp:TextBox ID="txtOralAttempt2Date"
                                                                        runat="server" ToolTip="Please Select Oral Attempt2 Date" class="form-control" TabIndex="5"></asp:TextBox><ajaxToolKit:CalendarExtender ID="ceOralAttempt2date" runat="server" Enabled="True"
                                                                            Format="dd/MM/yyyy" PopupButtonID="txtOralAttemptDate2" TargetControlID="txtOralAttempt2Date" />
                                                                    <ajaxToolKit:MaskedEditExtender ID="meeOralAttempt2Date" runat="server" AcceptNegative="Left"
                                                                        DisplayMoney="Left" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date"
                                                                        MessageValidatorTip="true" TargetControlID="txtOralAttempt2Date" />
                                                                </div>
                                                            </div>

                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="card">
                                            <div class="card-header collapsed" data-toggle="collapse" data-target="#collapseFour" aria-expanded="true">
                                                <span class="title" id="Div1" runat="server">The student submitted and presented a research plan entitled. </span>
                                                <span class="accicon"><i class="fa fa-angle-down rotate-icon"></i></span>
                                            </div>
                                            <div id="collapseFour" class="collapse collapse show">
                                                <div class="card-body">

                                                    <div class="col-12">
                                                        <asp:TextBox ID="txtReseachPlan" runat="server" TextMode="MultiLine" class="form-control" TabIndex="6"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvReseachPlan" runat="server" ControlToValidate="txtReseachPlan"
                                                            Display="None" ErrorMessage="Please Enter Research Plan" SetFocusOnError="True"
                                                            ValidationGroup="Academic"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="card" id="trApproved" runat="server">
                                            <asp:UpdatePanel ID="uppnl" runat="server">
                                                <ContentTemplate>
                                                    <div id="dvApproved" runat="server">
                                                        <div class="card-header collapsed" data-toggle="collapse" data-target="#collapseFive" aria-expanded="true">
                                                            <span class="title" id="tr3" runat="server">The research plan is approved and that the Date of approval of Research Plan be </span>
                                                            <span class="accicon"><i class="fa fa-angle-down rotate-icon"></i></span>
                                                        </div>
                                                        <div id="collapseFive" class="collapse collapse show">
                                                            <div class="card-body">
                                                                <div class="col-12">
                                                                    <div class="row">
                                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                                            <div class="label-dynamic">
                                                                                <label>Result</label>
                                                                            </div>
                                                                            <asp:DropDownList ID="ddlPHDresult" runat="server" AutoPostBack="true" CssClass="form-control" data-select2-enable="true"
                                                                                AppendDataBoundItems="true" TabIndex="7" OnSelectedIndexChanged="ddlPHDresult_SelectedIndexChanged">
                                                                                <asp:ListItem Value="0"> Please Select </asp:ListItem>
                                                                                <asp:ListItem Value="1"> Pass </asp:ListItem>
                                                                                <asp:ListItem Value="2"> Fail </asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                                            <div class="label-dynamic">
                                                                                <sup>* </sup>
                                                                                <label>Approved Date</label>
                                                                            </div>
                                                                            <div class="input-group date">
                                                                                <div class="input-group-addon">
                                                                                    <i class="fa fa-calendar" id="txtApprovedDate1"></i>
                                                                                </div>
                                                                                <asp:TextBox ID="txtApprovedDate" runat="server"
                                                                                    class="form-control" ToolTip="Please Select Approved Date" TabIndex="8"></asp:TextBox>
                                                                                <ajaxToolKit:CalendarExtender ID="ceApproveddate" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                                                    PopupButtonID="txtApprovedDate1" TargetControlID="txtApprovedDate" />
                                                                                <ajaxToolKit:MaskedEditExtender ID="meeApprovedDate" runat="server" AcceptNegative="Left"
                                                                                    DisplayMoney="Left" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date"
                                                                                    MessageValidatorTip="true" TargetControlID="txtApprovedDate" />
                                                                                <asp:RequiredFieldValidator ID="reqfv_txtApprovedDate" runat="server" ControlToValidate="txtApprovedDate"
                                                                                    Display="None" ErrorMessage="Please Select Approved Date" SetFocusOnError="True"
                                                                                    ValidationGroup="Academic"></asp:RequiredFieldValidator>
                                                                            </div>
                                                                        </div>
                                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                                            <div class="label-dynamic">
                                                                                <sup>* </sup>
                                                                                <label>Research Plan</label>
                                                                            </div>
                                                                            <asp:DropDownList ID="ddlPHDResearchplan" runat="server" AutoPostBack="true" AppendDataBoundItems="true" Enabled="false" CssClass="form-control" data-select2-enable="true" TabIndex="9">
                                                                                <asp:ListItem Value="0"> Please Select </asp:ListItem>
                                                                                <asp:ListItem Value="1"> Approved </asp:ListItem>
                                                                                <asp:ListItem Value="2"> Not Approved  </asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                                            <div class="label-dynamic">
                                                                                <label>Course Work Completed</label>
                                                                            </div>
                                                                            <asp:DropDownList ID="ddlComplete" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" TabIndex="10">
                                                                                <asp:ListItem Selected="True" Value="1">Yes</asp:ListItem>
                                                                                <asp:ListItem Value="0">No</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                            <asp:RequiredFieldValidator ID="reqfv_ddlComplete" runat="server" ControlToValidate="ddlComplete"
                                                                                Display="None" ErrorMessage="Please Select Approved Date" SetFocusOnError="True"
                                                                                ValidationGroup="Academic"></asp:RequiredFieldValidator>
                                                                        </div>

                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="ddlPHDresult" EventName="SelectedIndexChanged" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>

                                        <div class="col-12 btn-footer">
                                            <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary" OnClick="btnSubmit_Click" TabIndex="11" Text="Submit" ValidationGroup="Academic" />
                                            <asp:Button ID="btnReport" runat="server" CssClass="btn btn-info" OnClick="btnReport_Click" TabIndex="13" Text="Annexure-B Report" Visible="false" />
                                            <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-warning" OnClick="btnCancel_Click" TabIndex="12" Text="Cancel" />

                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="Qual" />
                                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="EntranceExam" />
                                            <asp:ValidationSummary ID="rfvValidationSummary" runat="server" ValidationGroup="Academic"
                                                DisplayMode="List" ShowMessageBox="True" ShowSummary="False" />
                                        </div>
                                    </asp:Panel>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>

    </asp:Panel>

    <div id="divMsg" runat="server">
    </div>
    <script type="text/javascript">

        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $('#id1').dataTable({
                paging: false,
                searching: true,
                bDestroy: true
            });
        });
    </script>
    <script type="text/javascript" lang="javascript">

        $(document).ready(function () {
            debugger
            $("#<%= pnltextbox.ClientID %>").hide();

            $("#<%= pnlDropdown.ClientID %>").hide();
        });
        function submitPopup(btnsearch) {

            debugger
            var rbText;
            var searchtxt;

            var e = document.getElementById("<%=ddlSearch.ClientID%>");
            var rbText = e.options[e.selectedIndex].text;
            var ddlname = e.options[e.selectedIndex].text;
            if (rbText == "Please Select") {
                alert('Please Select Search Criteria.')
                $(e).focus();
                return false;
            }

            else {


                if (rbText == "ddl") {
                    var skillsSelect = document.getElementById("<%=ddlDropdown.ClientID%>").value;

                    var searchtxt = skillsSelect;
                    if (searchtxt == "0") {
                        alert('Please Select ' + ddlname + '..!');
                    }
                    else {
                        __doPostBack(btnsearch, rbText + ',' + searchtxt);
                        return true;
                        //$("#<%= divpanel.ClientID %>").hide();
                        $("#<%= pnltextbox.ClientID %>").hide();

                    }
                }
                else if (rbText == "BRANCH") {

                    if (searchtxt == "Please Select") {
                        alert('Please Select Branch..!');

                    }
                    else {
                        __doPostBack(btnsearch, rbText + ',' + searchtxt);

                        return true;
                    }

                }
                else {
                    searchtxt = document.getElementById('<%=txtSearch.ClientID %>').value;
                    if (searchtxt == "" || searchtxt == null) {
                        alert('Please Enter Data to Search.');
                        //$(searchtxt).focus();
                        document.getElementById('<%=txtSearch.ClientID %>').focus();
                        return false;
                    }
                    else {
                        __doPostBack(btnsearch, rbText + ',' + searchtxt);
                        //$("#<%= divpanel.ClientID %>").hide();
                        //$("#<%= pnltextbox.ClientID %>").show();

                        return true;
                    }
                }
        }
    }

    function ClearSearchBox(btncancelmodal) {
        document.getElementById('<%=txtSearch.ClientID %>').value = '';
        __doPostBack(btncancelmodal, '');
        return true;
    }
    function CloseSearchBox(btnClose) {
        document.getElementById('<%=txtSearch.ClientID %>').value = '';
        __doPostBack(btnClose, '');
        return true;
    }




    function Validate() {

        debugger

        var rbText;

        var e = document.getElementById("<%=ddlSearch.ClientID%>");
        var rbText = e.options[e.selectedIndex].text;

        if (rbText == "IDNO" || rbText == "Mobile") {

            var char = (event.which) ? event.which : event.keyCode;
            if (char >= 48 && char <= 57) {
                return true;
            }
            else {
                return false;
            }
        }
        else if (rbText == "NAME") {

            var char = (event.which) ? event.which : event.keyCode;

            if ((char >= 65 && char <= 90) || (char >= 97 && char <= 122)) {
                return true;
            }
            else {
                return false;
            }

        }
    }
    </script>

</asp:Content>
