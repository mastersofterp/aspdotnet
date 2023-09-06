<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="PhdAnnexure.aspx.cs" Inherits="Academic_PhdAnnexure" UICulture="auto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

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


    <div id="myModal2" role="dialog" runat="server">
        <asp:UpdateProgress ID="UpdateProgress1" runat="server"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>


                <div>
                    <asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="updEdit"
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

            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>

    <asp:Panel ID="pnDisplay" runat="server">
        <div class="row">
            <div class="col-md-12 col-sm-12 col-12">
                <div class="box box-primary">
                    <div class="box-header with-border">
                        <h3 class="box-title">
                            <asp:Label ID="lblDynamicPageTitle" runat="server" Font-Bold="true"></asp:Label>
                        </h3>
                        <asp:Label ID="lblHelp" runat="server" Font-Names="Trebuchet MS" />
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
                                                    <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control"></asp:TextBox>
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
                                        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" CssClass="btn btn-primary" OnClientClick="return submitPopup(this.name);" />

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

                        <div id="divmain" runat="server" visible="false">
                            <div class="accordion" id="accordionExample">

                                <div class="card" runat="server" id="DivSutLog">
                                    <div class="card-header" data-toggle="collapse" data-target="#collapseOne" aria-expanded="true">
                                        <span class="title">General Information</span>
                                        <span class="accicon"><i class="fa fa-angle-down rotate-icon"></i></span>
                                    </div>


                                    <div id="collapseOne" class="collapse show">
                                        <div class="card-body">
                                            <div id="divGeneralInfo" style="display: block;" runat="server">
                                                <asp:Panel ID="pnlId" runat="server" Visible="false">
                                                    <div class="form-group col-md-4 col-md-offset-4" id="dividno" runat="server" visible="false">
                                                        <%-- <div class="row">--%>
                                                        <label>ID No.</label>
                                                        <div class="input-group date">
                                                            <asp:TextBox ID="txtIDNo" runat="server" class="form-control" TabIndex="1" Enabled="False" />
                                                            <a href="#" title="Search Student for Modification" onclick="Modalbox.show($('divdemo2'), {title: this.title, width: 600,overlayClose:false});return false;"></a>
                                                            <div class="input-group-addon">
                                                                <asp:Image ID="imgSearch" runat="server" ImageUrl="~/IMAGES/search.png" TabIndex="1"
                                                                    AlternateText="Search" ToolTip="Search Student by IDNo, Name, Reg. No, Branch, Semester" />


                                                                <asp:LinkButton ID="lnkSearch" runat="server" Text="Search Here" OnClick="lnkSearch_Click"></asp:LinkButton>

                                                            </div>
                                                            <%--  </div>--%>
                                                        </div>
                                                    </div>
                                                </asp:Panel>

                                                <div class="col-12" id="DivGenInfo" runat="server" visible="true">
                                                    <div class="row">
                                                        <div class="col-lg-6 col-md-6 col-12">
                                                            <ul class="list-group list-group-unbordered">
                                                                <li class="list-group-item"><b>ID No. :</b>
                                                                    <a class="sub-label">
                                                                        <asp:Label ID="lblidno" runat="server" Font-Bold="True"></asp:Label>
                                                                    </a>
                                                                </li>
                                                                <li class="list-group-item"><b>Enrollment No. :</b>
                                                                    <a class="sub-label">
                                                                        <asp:Label ID="lblenrollmentnos" runat="server" Font-Bold="True"></asp:Label></a>
                                                                </li>
                                                                <li class="list-group-item"><b>Date of Joining :</b>
                                                                    <a class="sub-label">
                                                                        <asp:Label ID="lbljoiningdate" runat="server" Font-Bold="True"></asp:Label>
                                                                        <asp:HiddenField ID="hfdegreenos" runat="server" />
                                                                    </a>
                                                                </li>
                                                                <li class="list-group-item"><b>Status :</b>
                                                                    <a class="sub-label">
                                                                        <asp:Label ID="lblstatussup" runat="server" Font-Bold="True"></asp:Label></a>
                                                                </li>
                                                            </ul>
                                                        </div>
                                                        <div class="col-lg-6 col-md-6 col-12">
                                                            <ul class="list-group list-group-unbordered">
                                                                <li class="list-group-item"><b>Student Name :</b>
                                                                    <a class="sub-label">
                                                                        <asp:Label ID="lblnames" runat="server" Font-Bold="True"></asp:Label>
                                                                    </a>
                                                                </li>
                                                                <li class="list-group-item"><b>Father Name :</b>
                                                                    <a class="sub-label">
                                                                        <asp:Label ID="lblfathername" runat="server" Font-Bold="True"></asp:Label>
                                                                    </a>
                                                                </li>
                                                                <li class="list-group-item"><b>Department :</b>
                                                                    <a class="sub-label">
                                                                        <asp:Label ID="lblDepartment" runat="server" Font-Bold="true"></asp:Label>
                                                                        <asp:HiddenField ID="hfDepartment" runat="server" />
                                                                    </a>
                                                                </li>
                                                                <li class="list-group-item"><b>Admission Batch :</b>
                                                                    <a class="sub-label">
                                                                        <asp:Label ID="lbladmbatch" runat="server" Font-Bold="True"></asp:Label>
                                                                        <asp:HiddenField ID="hfadmbatch" runat="server" />
                                                                    </a>
                                                                </li>
                                                            </ul>
                                                        </div>
                                                    </div>

                                                    <div class="row">
                                                        <div class="form-group col-sm-4" runat="server" visible="false">
                                                            <label>ID. No. </label>
                                                            <asp:TextBox ID="txtRegNo" runat="server" class="form-control" TabIndex="2" ToolTip="Please Enter Roll No."
                                                                Enabled="false" />
                                                        </div>

                                                        <div class="form-group col-sm-4" runat="server" visible="false">
                                                            <label>Enrollment No.</label>
                                                            <asp:TextBox ID="txtEnrollno" runat="server" class="form-control" TabIndex="3" ToolTip="Please Enter Enrollment No."
                                                                Enabled="false" />
                                                        </div>

                                                        <div class="form-group col-sm-4" runat="server" visible="false">
                                                            <sup>*</sup>
                                                            <label>Student Name</label>
                                                            <asp:TextBox ID="txtStudentName" runat="server" MaxLength="150" TabIndex="4" ToolTip="Please Enter Student name"
                                                                Enabled="false" class="form-control" />
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                                                FilterMode="InvalidChars" FilterType="Custom" InvalidChars="1234567890" TargetControlID="txtStudentName" />
                                                            <asp:RequiredFieldValidator ID="rfvStudentName" runat="server" ControlToValidate="txtStudentName"
                                                                Display="None" ErrorMessage="Please Enter Name" SetFocusOnError="True" TabIndex="8"
                                                                ValidationGroup="Academic"></asp:RequiredFieldValidator>
                                                        </div>

                                                        <div class="form-group col-sm-4" runat="server" visible="false">
                                                            <sup>*</sup>
                                                            <label>Father&#39;s Name</label>
                                                            <asp:TextBox ID="txtFatherName" runat="server" TabIndex="5" ToolTip="Please Enter Father's Name"
                                                                Enabled="false" class="form-control" />
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                                FilterMode="InvalidChars" FilterType="Custom" InvalidChars="1234567890" TargetControlID="txtFatherName" />
                                                            <asp:RequiredFieldValidator ID="rfvtxtFatherName" runat="server" ControlToValidate="txtFatherName"
                                                                Display="None" ErrorMessage="Please Enter Father Name" SetFocusOnError="True"
                                                                ValidationGroup="Academic" Visible="False"></asp:RequiredFieldValidator>
                                                        </div>

                                                        <div class="form-group col-sm-4" runat="server" visible="false">
                                                            <sup>*</sup>
                                                            <label>Date of Joining </label>
                                                            <div class="input-group date">
                                                                <div class="input-group-addon">
                                                                    <i class="fa fa-calendar" id="txtDateOfJoining1" runat="server"></i>
                                                                </div>
                                                                <asp:TextBox ID="txtDateOfJoining" runat="server" TabIndex="6" ToolTip="Please Enter Date Of Joining"
                                                                    Enabled="false" class="form-control" />
                                                                <ajaxToolKit:CalendarExtender ID="ceDateOfJoining" runat="server" Enabled="True"
                                                                    Format="dd/MM/yyyy" PopupButtonID="txtDateOfJoining1" TargetControlID="txtDateOfJoining">
                                                                </ajaxToolKit:CalendarExtender>
                                                                <ajaxToolKit:MaskedEditExtender ID="meeDateOfJoining" runat="server" AcceptAMPM="True"
                                                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                    CultureTimePlaceholder="" Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999"
                                                                    MaskType="Date" TargetControlID="txtDateOfJoining" />
                                                                <asp:RequiredFieldValidator ID="rfvDateOfJoining" runat="server" ControlToValidate="txtDateOfJoining"
                                                                    Display="None" ErrorMessage="Please Enter Date Of Joining" SetFocusOnError="True"
                                                                    ValidationGroup="Academic"></asp:RequiredFieldValidator>
                                                                <ajaxToolKit:MaskedEditValidator ID="mevDateOfJoining" runat="server" ControlExtender="meeDateOfJoining"
                                                                    ControlToValidate="txtDateOfJoining" Display="None" EmptyValueBlurredText="*"
                                                                    EmptyValueMessage="Please Enter Date Of Joining" ErrorMessage="Please Select Date"
                                                                    InvalidValueBlurredMessage="*" InvalidValueMessage="Date is invalid" IsValidEmpty="False"
                                                                    SetFocusOnError="True" TooltipMessage="Input a date" ValidationGroup="Academic" />
                                                            </div>
                                                        </div>

                                                        <div class="form-group col-sm-4" runat="server" visible="false">
                                                            <label for="city"><span style="color: red;">*</span> Department</label>
                                                            <asp:DropDownList ID="ddlDepatment" runat="server" AppendDataBoundItems="True" TabIndex="7"
                                                                Enabled="false" ToolTip="Please Select Department" class="form-control" />
                                                        </div>

                                                        <div class="form-group col-sm-4" runat="server" visible="false">
                                                            <label for="city"><span style="color: red;">* </span>Status</label>
                                                            <asp:DropDownList ID="ddlStatus" runat="server" class="form-control" AutoPostBack="True"
                                                                OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                <asp:ListItem Value="1">Full Time</asp:ListItem>
                                                                <asp:ListItem Value="2">Part Time</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>

                                                    <div class="row mt-3" id="DivDrops" runat="server" visible="false">
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Status Category</label>
                                                            </div>

                                                            <asp:DropDownList ID="ddlStatusCat" runat="server" AppendDataBoundItems="True" TabIndex="8"
                                                                ToolTip="Please Select Status Category" CssClass="form-control" data-select2-enable="true">
                                                                <asp:ListItem Value="1" Text="Course Work Completed"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvSession" runat="server" ErrorMessage="Please Select Status Category"
                                                                ControlToValidate="ddlStatusCat" Display="None" SetFocusOnError="True" InitialValue="0"
                                                                ValidationGroup="Academic"></asp:RequiredFieldValidator>

                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Total No.of credits</label>
                                                            </div>

                                                            <asp:TextBox ID="txtTotCredits" runat="server" TabIndex="9" ToolTip="Please Enter Total No. of Credits."
                                                                class="form-control" onkeyup="validateNumeric(this)" MaxLength="2" />
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" TargetControlID="txtTotCredits"
                                                                ValidChars="1234567890" FilterMode="ValidChars" />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTotCredits"
                                                                Display="None" ErrorMessage="Please Enter Total No. of Credits." SetFocusOnError="True"
                                                                ValidationGroup="Academic"></asp:RequiredFieldValidator>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Supervisor</label>
                                                            </div>

                                                            <asp:DropDownList ID="ddlSupervisor" runat="server" AppendDataBoundItems="True" TabIndex="13"
                                                                ToolTip="Please Select Supervisor" CssClass="form-control" data-select2-enable="true" AutoPostBack="True" OnSelectedIndexChanged="ddlSupervisor_SelectedIndexChanged" />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlSupervisor"
                                                                Display="None" ErrorMessage="Please Select Supervisor" InitialValue="0"
                                                                ValidationGroup="Academic" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Supervisor Role</label>
                                                            </div>

                                                            <asp:DropDownList ID="ddlSupervisorrole" runat="server" AutoPostBack="true" AppendDataBoundItems="True"
                                                                TabIndex="15" ToolTip="Please Select Supervisor role" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlSupervisorrole_SelectedIndexChanged">
                                                                <asp:ListItem Selected="True" Value="0">Please Select</asp:ListItem>
                                                                <asp:ListItem Value="S">Singly</asp:ListItem>
                                                                <asp:ListItem Value="J">Jointly</asp:ListItem>
                                                                <asp:ListItem Value="T">Multiple</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlSupervisorrole"
                                                                Display="None" ErrorMessage="Please Select Supervisor Role" InitialValue="0"
                                                                ValidationGroup="Academic" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Area of Research </label>
                                                            </div>

                                                            <asp:TextBox ID="txtResearch" runat="server" CssClass="unwatermarked" Rows="1" class="form-control" TextMode="MultiLine" TabIndex="11"> </asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtResearch"
                                                                Display="None" ErrorMessage="Please Enter Area of Research" SetFocusOnError="True"
                                                                ValidationGroup="Academic"></asp:RequiredFieldValidator>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="nodgc" runat="server">
                                                            <div class="label-dynamic">
                                                                <label>No.of DGC Member</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlNdgc" runat="server" TabIndex="12"
                                                                ToolTip="Please Select No.Of DGC" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlNdgc_SelectedIndexChanged"
                                                                AutoPostBack="True">
                                                                <asp:ListItem Selected="True" Value="4">4</asp:ListItem>
                                                                <asp:ListItem Value="3">3</asp:ListItem>
                                                                <asp:ListItem Value="5">5</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false">
                                                            <div class="label-dynamic">
                                                                <label>Admission Batch </label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlAdmBatch" runat="server" AppendDataBoundItems="True" TabIndex="10"
                                                                Enabled="false" ToolTip="Please Select Admission Batch" CssClass="form-control" data-select2-enable="true" />
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="Jointid" runat="server" visible="false">
                                                            <div class="label-dynamic">
                                                                <label>Joint Supervisor</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlCoSupervisor" runat="server" AppendDataBoundItems="True"
                                                                Visible="false" TabIndex="14" ToolTip="Please Select Co-Supervisor" CssClass="form-control" data-select2-enable="true" />
                                                            <asp:Label ID="lblJointSupevisor" runat="server"></asp:Label>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="tdremark" runat="server">
                                                            <div class="label-dynamic">
                                                                <label>Remark for Cancellation </label>
                                                            </div>
                                                            <asp:TextBox ID="txtRemark" runat="server" Enabled="false"> </asp:TextBox>
                                                        </div>

                                                        <div id="divremark" runat="server" style="color: Red; font-size: medium;" visible="false" class="form-group col-md-4">
                                                            <b>DGC Approval has been Cancelled</b>
                                                        </div>

                                                        <div id="divConfirm" runat="server" style="color: Green; font-size: medium;" visible="false" class="form-group col-md-4">
                                                            <b>Student is Eligible For PhD Annexure A</b>
                                                        </div>

                                                        <div id="divhod" runat="server" style="color: Red; font-size: medium;" visible="false" class="form-group col-md-4">
                                                            <b>Above Student is not from your Department</b>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="card">
                                    <div class="card-header collapsed" data-toggle="collapse" data-target="#collapseTwo" aria-expanded="true" aria-controls="collapseTwo">
                                        <span class="title" id="trDGC" runat="server">Doctoral guidance committee (DGC)</span>
                                        <span class="accicon"><i class="fa fa-angle-down rotate-icon"></i></span>
                                    </div>
                                    <div id="collapseTwo" class="collapse collapse show">
                                        <div class="card-body">
                                            <div class="divdoctrate" runat="server">
                                                <asp:Panel ID="pnlDoc" runat="server" Enabled="true">
                                                    <div class="col-12">
                                                        <div class="row">
                                                            <div class="form-group col-lg-6 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <label>Supervisor </label>
                                                                </div>

                                                                <div class="row">
                                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                                        <asp:DropDownList ID="ddlDGCSupervisor" runat="server" TabIndex="15" ToolTip="Please Select Supervisor"
                                                                            CssClass="form-control" data-select2-enable="true">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                                        <asp:DropDownList ID="ddlMember" runat="server" TabIndex="16" ToolTip="Please Select Member"
                                                                            CssClass="form-control" data-select2-enable="true">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                            <asp:ListItem Value="1">Convener</asp:ListItem>
                                                                            <%--   <asp:ListItem Value="2">Member(s)</asp:ListItem>
                                                            <asp:ListItem Value="3">Member</asp:ListItem>--%>
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="rfvDGCSupervisor" runat="server" ControlToValidate="ddlMember"
                                                                            Display="None" ErrorMessage="Please Select Member" InitialValue="0" ValidationGroup="Academic"
                                                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                </div>
                                                                <asp:Label ID="lblStatus" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
                                                            </div>

                                                            <div class="form-group col-lg-6 col-md-6 col-12" id="expertrow" runat="server">
                                                                <div class="label-dynamic">
                                                                    <label>Joint-Supervisor *(s)(if any) </label>
                                                                </div>

                                                                <div class="row">
                                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                                        <asp:DropDownList ID='ddlJointSupervisor' runat="server" AppendDataBoundItems="True"
                                                                            TabIndex="75" ToolTip="Please Select Joint Supervisor" CssClass="form-control" data-select2-enable="true"
                                                                            AutoPostBack="true" OnSelectedIndexChanged="ddlJointSupervisor_SelectedIndexChanged" />
                                                                    </div>
                                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                                        <asp:DropDownList ID='ddlMember1' runat="server" AutoPostBack="true" TabIndex="18" CssClass="form-control" data-select2-enable="true"
                                                                            ToolTip="Please Select Member" OnSelectedIndexChanged="ddlMember1_SelectedIndexChanged">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                            <%-- <asp:ListItem Value="1">Convener</asp:ListItem>--%>
                                                                            <asp:ListItem Value="2">Member(s)</asp:ListItem>
                                                                            <asp:ListItem Value="3">Member</asp:ListItem>
                                                                            <asp:ListItem Value="4">Outside Member</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>

                                                                    <asp:Label ID="lblStatus1" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
                                                                    <div class="col-sm-12 group-form">
                                                                        <asp:TextBox ID="txtOutside1" runat="server" Visible="false" class="form-control"> </asp:TextBox>
                                                                        <asp:DropDownList ID="txtOutside" runat="server" AppendDataBoundItems="True"
                                                                            TabIndex="19" ToolTip="Please Select Outside Joint Supervisor" Visible="false" class="form-control"
                                                                            AutoPostBack="true" OnSelectedIndexChanged="txtOutside_SelectedIndexChanged" data-select2-enable="true">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <%--ADDED FOR EXTRA SUPERVISOR--%>

                                                            <div class="form-group col-lg-6 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <label>One Institute faculty expert </label>
                                                                </div>

                                                                <div class="row">
                                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                                        <asp:DropDownList ID='ddlInstFac' runat="server" AppendDataBoundItems="True" TabIndex="20"
                                                                            ToolTip="Please Select One Institute faculty expert" CssClass="form-control" data-select2-enable="true" />
                                                                    </div>
                                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                                        <asp:DropDownList ID='ddlMember2' runat="server" TabIndex="15" ToolTip="Please Select Member"
                                                                            cOnSelectedIndexChanged="ddlMember2_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                            <%-- <asp:ListItem Value="1">Convener</asp:ListItem>--%>
                                                                            <asp:ListItem Value="2">Member(s)</asp:ListItem>
                                                                            <asp:ListItem Value="3">Member</asp:ListItem>
                                                                            <%-- <asp:ListItem Value="4">Outside Member</asp:ListItem>--%>
                                                                        </asp:DropDownList>
                                                                    </div>

                                                                    <asp:Label ID="lblStatus2" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
                                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                                        <asp:DropDownList ID='txtOutsideInsti' runat="server" AppendDataBoundItems="True" TabIndex="21"
                                                                            ToolTip="Please Select One Institute faculty expert" CssClass="form-control" data-select2-enable="true" />
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="form-group col-lg-6 col-md-6 col-12" id="secondsupervisor" runat="server">
                                                                <div class="label-dynamic">
                                                                    <label>Joint-Supervisor *(s)(if any)</label>
                                                                </div>

                                                                <div class="row">
                                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                                        <asp:DropDownList ID='ddlJointSupervisorSecond' runat="server" AppendDataBoundItems="True"
                                                                            OnSelectedIndexChanged="ddlJointSupervisorSecond_SelectedIndexChanged"
                                                                            AutoPostBack="true" TabIndex="15" ToolTip="Please Select Second Joint Supervisor" class="form-control" data-select2-enable="true" />
                                                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlJointSupervisorSecond"
                                                            Display="None" ErrorMessage="Please Select Joint Supervisor" InitialValue="0"
                                                            ValidationGroup="Academic" SetFocusOnError="True"></asp:RequiredFieldValidator>--%>
                                                                    </div>
                                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                                        <asp:DropDownList ID='ddlMember5' runat="server" AutoPostBack="true" TabIndex="15"
                                                                            ToolTip="Please Select Member" class="form-control" OnSelectedIndexChanged="ddlMember5_SelectedIndexChanged" data-select2-enable="true">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                            <%--   <asp:ListItem Value="1">Convener</asp:ListItem>--%>
                                                                            <asp:ListItem Value="2">Member(s)</asp:ListItem>
                                                                            <asp:ListItem Value="3">Member</asp:ListItem>
                                                                            <asp:ListItem Value="4">Outside Member</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                    <asp:Label ID="lblStatus5" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
                                                                    <div class="col-sm-12 group-form">
                                                                        <asp:TextBox ID="txtSecondSupervisor1" runat="server" Visible="false" class="form-control"> </asp:TextBox>
                                                                        <%-- <asp:DropDownList ID='' runat="server" AppendDataBoundItems="True" Visible="false"
                                                        TabIndex="15" ToolTip="Please Select Second Outside Joint Supervisor" class="form-control" />--%>

                                                                        <asp:DropDownList ID="txtSecondSupervisorOutside" runat="server" AppendDataBoundItems="True"
                                                                            TabIndex="19" ToolTip="Please Select Second Outside Joint Supervisor" Visible="false"
                                                                            AutoPostBack="true" OnSelectedIndexChanged="txtSecondSupervisorOutside_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="form-group col-lg-6 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <label>A DRC nominee </label>
                                                                </div>

                                                                <div class="row">
                                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                                        <asp:DropDownList ID='ddlDRC' runat="server" AppendDataBoundItems="True" TabIndex="22"
                                                                            ToolTip="Please Select A DRC nominee" class="form-control" data-select2-enable="true" />
                                                                    </div>
                                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                                        <asp:DropDownList ID='ddlMember3' runat="server" TabIndex="15" ToolTip="Please Select Member"
                                                                            OnSelectedIndexChanged="ddlMember3_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                            <%--                                                            <asp:ListItem Value="1">Convener</asp:ListItem>--%>
                                                                            <asp:ListItem Value="2">Member(s)</asp:ListItem>
                                                                            <asp:ListItem Value="3">Member</asp:ListItem>
                                                                            <%-- <asp:ListItem Value="4">Outside Member</asp:ListItem>--%>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                    <%-- </div>--%>
                                                                    <asp:Label ID="lblStatus3" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
                                                                    <%--  <asp:TextBox ID="txtOutsideDRC" runat="server" Visible="false" class="form-control"> </asp:TextBox>--%>
                                                                    <asp:DropDownList ID='txtOutsideDRC' runat="server" AppendDataBoundItems="True" TabIndex="23"
                                                                        Visible="false" ToolTip="Please Select A DRC nominee" CssClass="form-control" data-select2-enable="true" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                </asp:Panel>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="card">
                                    <div class="card-header collapsed" data-toggle="collapse" data-target="#collapseThree" aria-expanded="true">
                                        <span class="title" id="trdrc" runat="server">Recommendation of the Departmental Research Committee(DRC)</span>
                                        <span class="accicon"><i class="fa fa-angle-down rotate-icon"></i></span>
                                    </div>
                                    <div id="collapseThree" class="collapse collapse show">
                                        <div class="card-body">
                                            <div id="divdrc" runat="server" class="col-12">
                                                The DRC recommends the registration of Mr./Mrs.<asp:Label ID="lblname" runat="server"
                                                    Text="name" Font-Bold="true"></asp:Label>&nbsp;<asp:Label ID="partfull" runat="server"></asp:Label>student with effect from
                                                        <asp:Label ID="lbldate" runat="server" Font-Bold="true"></asp:Label>
                                                and also recommends the appointment of supervisor (s) as he / she / they satisfy
                                                        rule R.7 of PhD ordinance (supervisors' Bio-data with list of publications and experience
                                                        be enclosed) and formation of DGC as indicated above.
                                            </div>

                                            <div id="trdrc1" runat="server" class="col-12 mt-3">
                                                <div class="row">
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>A DRC Chairman  </label>
                                                        </div>

                                                        <span class="input-group-addon">
                                                            <asp:DropDownList ID='ddlDRCChairman' runat="server" TabIndex="24" ToolTip="Please Select A DRC nominee"
                                                                CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                            </asp:DropDownList>

                                                            <asp:DropDownList ID='ddlMember4' runat="server" Visible="false" TabIndex="25" ToolTip="Please Select Member"
                                                                CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlMember4_SelectedIndexChanged">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                <asp:ListItem Value="1">Convener</asp:ListItem>
                                                                <asp:ListItem Value="2">Member(s)</asp:ListItem>
                                                                <asp:ListItem Value="3">Member</asp:ListItem>
                                                                <asp:ListItem Value="4">Outside Member</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </span>
                                                        <asp:Label ID="lblStatus4" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
                                                        <asp:TextBox ID="txtOutsideDrcCh" runat="server" Visible="false" class="form-control"> </asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-12 btn-footer">
                                    <asp:ValidationSummary ID="rfvValidationSummary" runat="server" ValidationGroup="Academic"
                                        DisplayMode="List" ShowMessageBox="True" ShowSummary="False" />

                                    <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary" OnClick="btnSubmit_Click" TabIndex="26" Text="Submit" ValidationGroup="Academic" />
                                    <asp:Button ID="btnApply" runat="server" CssClass="btn btn-primary" OnClick="btnApply_Click" TabIndex="30" Text="Apply Student List" />

                                    <asp:Button ID="btnReport" runat="server" CssClass="btn btn-info" OnClick="btnReport_Click" TabIndex="28" Text="Annexure-A Report" Visible="false" />
                                    <asp:Button ID="btnJoint" runat="server" CssClass="btn btn-info" OnClick="btnJoint_Click" TabIndex="31" Text="No.of Scholar per Supervisor List" Visible="false" />
                                    <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-warning" OnClick="btnCancel_Click" TabIndex="27" Text="Cancel" />

                                    <asp:Button ID="btnReject" runat="server" CssClass="btn btn-warning" OnClick="btnReject_Click" TabIndex="29" Text="Reject" Visible="false" />


                                    <asp:HyperLink ID="hlink" runat="server" NavigateUrl="~/ACADEMIC/PhDAnnexureEdit.aspx?pageno=1185" Text="Edit Phd Annexure A" Visible="false"></asp:HyperLink>

                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="Qual" />
                                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="EntranceExam" />

                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>

    <div id="divMsg" runat="server">
    </div>

    <script type="text/javascript">
        jQuery(function ($) {
            $(document).ready(function () {
                bindDataTable();
                Sys.WebForms.PageRequestManager.getInstance().add_endRequest(bindDataTable);
            });
            function bindDataTable() {
                var myDT = $('#id1').DataTable({
                });
            }

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
