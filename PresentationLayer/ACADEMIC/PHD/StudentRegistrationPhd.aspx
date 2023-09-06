<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    MaintainScrollPositionOnPostback="true" CodeFile="StudentRegistrationPhd.aspx.cs"
    Inherits="Academic_Default" %>

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
        #ctl00_ContentPlaceHolder1_pnlSession .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>
    <%--<style>
        .Tab:focus {
            outline: none;
            box-shadow: 0px 0px 5px 2px #61C5FA !important;
        }
    </style>
    <script type="text/javascript">
        RunThisAfterEachAsyncPostback();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
    </script>--%>

    <div>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updpnlUser"
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
        #ctl00_ContentPlaceHolder1_lvStudent_Panel2 .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>

    <!--Create New User-->
    <asp:UpdatePanel ID="updpnlUser" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server" Font-Bold="true"></asp:Label>
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

                                                <%--onchange=" return ddlSearch_change();"--%>
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
                                    </div>
                                    <div class="col-12 btn-footer">
                                        <%-- OnClientClick="return submitPopup(this.name);"--%>
                                        <asp:Button ID="Button1" runat="server" Text="Search" OnClick="btnSearch_Click" CssClass="btn btn-primary" OnClientClick="return submitPopup(this.name);" />

                                        <asp:Button ID="btnClose" runat="server" Text="Cancel" CssClass="btn btn-warning" OnClick="btnClose_Click" OnClientClick="return CloseSearchBox(this.name)" data-dismiss="modal" />
                                    </div>
                                    <div class="col-12 btn-footer">
                                        <asp:Label ID="lblNoRecords" runat="server" SkinID="lblmsg" />
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
                                <asp:UpdatePanel ID="upnldivmain" runat="server">
                                    <ContentTemplate>
                                        <div class="col-12">
                                            <div class="sub-heading">
                                                <h5>General Information</h5>
                                            </div>
                                            <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false">
                                                    <div class="label-dynamic">
                                                        <label for="city">Temp. Reg. No. </label>
                                                    </div>
                                                    <div class="input-group date">
                                                        <asp:TextBox ID="txtREGNo" runat="server" class="form-control" TabIndex="1" PlaceHolder="Enter Registration NO." />
                                                        <ajaxToolKit:TextBoxWatermarkExtender ID="watREGNo" TargetControlID="txtREGNo" runat="server">
                                                        </ajaxToolKit:TextBoxWatermarkExtender>
                                                        <div class="input-group-addon">
                                                            <asp:ImageButton ID="btnSearch" runat="server" ImageUrl="~/IMAGES/search.png"
                                                                ToolTip="Search by Registration No." OnClick="btnSearch_Click" Visible="true" Height="18px" />
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <asp:Label ID="lblDYRRNo" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                    <asp:TextBox ID="txtRRNo" runat="server" class="form-control" TabIndex="2" PlaceHolder="Type RRN No"
                                                        ToolTip="Please Enter RRN No." ValidationGroup="academic" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtRRNo"
                                                        ErrorMessage="Please Enter RRN No." Display="None" ValidationGroup="academic"
                                                        SetFocusOnError="true">
                                                    </asp:RequiredFieldValidator>
                                                    <ajaxToolKit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" TargetControlID="txtRRNo"
                                                        WatermarkText="Type RRN No" runat="server">
                                                    </ajaxToolKit:TextBoxWatermarkExtender>
                                                    <%--<ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server"
                                                    TargetControlID="txtStudentName" FilterType="Custom" FilterMode="InvalidChars"
                                                    InvalidChars="1234567890" />--%>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Student Name</label>
                                                    </div>
                                                    <asp:TextBox ID="txtStudentName" runat="server" class="form-control" TabIndex="2" PlaceHolder="Type Full Name Here"
                                                        ToolTip="Please Enter Student Name" ValidationGroup="academic" />
                                                    <asp:RequiredFieldValidator ID="rfvStudentName" runat="server" ControlToValidate="txtStudentName"
                                                        ErrorMessage="Please Enter Student Name" Display="None" ValidationGroup="academic"
                                                        SetFocusOnError="true">
                                                    </asp:RequiredFieldValidator>
                                                    <ajaxToolKit:TextBoxWatermarkExtender ID="tbWaterMarkStudName" TargetControlID="txtStudentName"
                                                        WatermarkText="Type Full Name Here" runat="server">
                                                    </ajaxToolKit:TextBoxWatermarkExtender>
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server"
                                                        TargetControlID="txtStudentName" FilterType="Custom" FilterMode="InvalidChars"
                                                        InvalidChars="1234567890" />
                                                </div>


                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Father's Name</label>
                                                    </div>
                                                    <asp:TextBox ID="txtFatherName" runat="server" class="form-control" TabIndex="3" ToolTip="Please Enter Father's Name" />
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                                        TargetControlID="txtFatherName" FilterType="Custom" FilterMode="InvalidChars"
                                                        InvalidChars="1234567890" />
                                                    <asp:RequiredFieldValidator ID="rfvFatherName" runat="server" ControlToValidate="txtFatherName"
                                                        Display="None" ErrorMessage="Please Enter Father Name" ValidationGroup="academic"
                                                        SetFocusOnError="true">
                                                    </asp:RequiredFieldValidator>
                                                </div>


                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Mother's Name</label>
                                                    </div>
                                                    <asp:TextBox ID="txtMotherName" runat="server" class="form-control" TabIndex="4" ToolTip="Please Enter Mother's name" />
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                                                        TargetControlID="txtMotherName" FilterType="Custom" FilterMode="InvalidChars"
                                                        InvalidChars="1234567890" />
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Date of Birth</label>
                                                    </div>
                                                    <div class="input-group date">
                                                        <div class="input-group-addon">
                                                            <i class="fa fa-calendar" id="imgStartDate" runat="server"></i>
                                                        </div>
                                                        <asp:TextBox ID="txtDateOfBirth" runat="server" TabIndex="5" ValidationGroup="academic" class="form-control" />

                                                        <ajaxToolKit:CalendarExtender ID="ceDateOfBirth" runat="server" Format="dd/MM/yyyy"
                                                            TargetControlID="txtDateOfBirth" PopupButtonID="imgStartDate" Enabled="true"
                                                            EnableViewState="true">
                                                        </ajaxToolKit:CalendarExtender>
                                                        <ajaxToolKit:MaskedEditExtender ID="meeDateOfBirth" runat="server" TargetControlID="txtDateOfBirth"
                                                            Mask="99/99/9999" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                                            MaskType="Date" AcceptAMPM="True" ErrorTooltipEnabled="True" />
                                                        <ajaxToolKit:MaskedEditValidator ID="mevDateOfBirth" runat="server" EmptyValueMessage="Please Enter DateOfBirth"
                                                            ControlExtender="meeDateOfBirth" ControlToValidate="txtDateOfBirth" IsValidEmpty="true"
                                                            InvalidValueMessage="Date is invalid" Display="None" TooltipMessage="Input a date"
                                                            ErrorMessage="Please Select Date" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                                            ValidationGroup="academic" SetFocusOnError="true" />
                                                    </div>
                                                </div>

                                                <%-- client requirement--%>
                                                <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Aadhar No.</label>
                                                    </div>
                                                    <asp:TextBox ID="txtAddharno" runat="server" class="form-control" TabIndex="6" ToolTip="Please Enter aadharno"
                                                        MaxLength="12" />
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server"
                                                        TargetControlID="txtAddharno" FilterType="Custom" FilterMode="InvalidChars" InvalidChars="abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ~!@#$%^&*()_+|}{:|][',.;\/{}-" />

                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Gender</label>
                                                    </div>
                                                    <asp:RadioButton ID="rdoMale" runat="server" Text="Male" GroupName="Sex" Checked="true"
                                                        TabIndex="6" />
                                                    <asp:RadioButton ID="rdoFemale" runat="server" Text="Female" GroupName="Sex" TabIndex="8" />
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label>Marital Status</label>
                                                    </div>
                                                    <asp:RadioButton ID="rdoMarriedNo" runat="server" Text="Unmarried" GroupName="Married"
                                                        Checked="true" TabIndex="9" />
                                                    <asp:RadioButton ID="rdoMarriedYes" runat="server" Text="Married" GroupName="Married"
                                                        TabIndex="7" />
                                                </div>


                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label>Religion</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlReligion" runat="server" class="form-control"
                                                        ToolTip="Please Select Religion" TabIndex="8" AppendDataBoundItems="True" data-select2-enable="true">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlReligion"
                                                        Display="None" ErrorMessage="Please Select Religion." ValidationGroup="academic" InitialValue="0"
                                                        SetFocusOnError="true">
                                                    </asp:RequiredFieldValidator>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label>Nationality</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlNationality" runat="server" Class="form-control"
                                                        ToolTip="Please Select Nationality" TabIndex="9" AppendDataBoundItems="True" data-select2-enable="true">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlNationality"
                                                        Display="None" ErrorMessage="Please Select Nationality." ValidationGroup="academic" InitialValue="0"
                                                        SetFocusOnError="true">
                                                    </asp:RequiredFieldValidator>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label>Category</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlCategory" runat="server" Class="form-control"
                                                        ToolTip="Please Select Nationality" TabIndex="10" AppendDataBoundItems="True" data-select2-enable="true">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlCategory"
                                                        Display="None" ErrorMessage="Please Select Category." ValidationGroup="academic" InitialValue="0"
                                                        SetFocusOnError="true">
                                                    </asp:RequiredFieldValidator>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label>Admission Category</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlAdmCategory" runat="server" CssClass="form-control"
                                                        ToolTip="Please Select Admission Category" TabIndex="11" AppendDataBoundItems="True" data-select2-enable="true">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlAdmCategory"
                                                        Display="None" ErrorMessage="Please Select Admission Category." ValidationGroup="academic" InitialValue="0"
                                                        SetFocusOnError="true">
                                                    </asp:RequiredFieldValidator>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Student Official Email</label>
                                                    </div>
                                                    <asp:TextBox ID="txtStudEmail" runat="server" CssClass="form-control" ToolTip="Please Enter Email"
                                                        TabIndex="12" />
                                                    <asp:RegularExpressionValidator ID="rfvStudEmail" runat="server" ControlToValidate="txtStudEmail"
                                                        Display="None" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                        ErrorMessage="Please Enter Valid EmailID" ValidationGroup="academic"></asp:RegularExpressionValidator>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Student Personal Email</label>
                                                    </div>
                                                    <asp:TextBox ID="txtPersonalemail" runat="server" CssClass="form-control" ToolTip="Please Enter Email"
                                                        TabIndex="13" />
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtPersonalemail"
                                                        Display="None" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                        ErrorMessage="Please Enter Valid EmailID" ValidationGroup="academic"></asp:RegularExpressionValidator>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtPersonalemail"
                                                        ErrorMessage="Please Enter Personal Email ID." Display="None" ValidationGroup="academic"
                                                        SetFocusOnError="true">
                                                    </asp:RequiredFieldValidator>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label>Blood Group</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlBloodGrp" runat="server" class="form-control"
                                                        ToolTip="Please Select Blood Group" TabIndex="14" AppendDataBoundItems="True" data-select2-enable="true">
                                                    </asp:DropDownList>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Physical Handicap</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlPhyHandicap" runat="server" AppendDataBoundItems="True"
                                                        class="form-control" TabIndex="15" data-select2-enable="true">
                                                        <asp:ListItem Value="-1">Please Select</asp:ListItem>
                                                        <asp:ListItem Value="1">Yes</asp:ListItem>
                                                        <asp:ListItem Value="0">No</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfv_ddlPhyHandicap" runat="server" ControlToValidate="ddlPhyHandicap"
                                                        ValidationGroup="academic" Display="None" SetFocusOnError="true" InitialValue="-1"
                                                        ErrorMessage="Please Select Physical Handicap Status!"></asp:RequiredFieldValidator>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12" id="ph" runat="server">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Type of Handicap</label>
                                                    </div>
                                                    <span id="ph1" runat="server">
                                                        <asp:DropDownList ID="ddlTypeofPhy" runat="server" AutoPostBack="True" AppendDataBoundItems="True"
                                                            TabIndex="16" data-select2-enable="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfv_ddlTypeofPhy" runat="server" ControlToValidate="ddlTypeofPhy"
                                                            ValidationGroup="academic" Display="None" SetFocusOnError="true" InitialValue="0"
                                                            ErrorMessage="Please Select Type of Physical Handicap!"></asp:RequiredFieldValidator>
                                                    </span>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12" id="dis" runat="server">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Type of Disability</label>
                                                    </div>
                                                    <span id="dis1" runat="server">
                                                        <asp:TextBox ID="txtdisablity" runat="server" TextMode="MultiLine" MaxLength="150"></asp:TextBox>
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="ftvdisbale" runat="server" TargetControlID="txtdisablity"
                                                            FilterType="Custom" FilterMode="InvalidChars" InvalidChars="1234567890" />
                                                    </span>
                                                </div>

                                            </div>
                                        </div>

                                        <div id="divAddressAndContactDetails" class="col-12 mt-3">
                                            <div class="sub-heading">
                                                <h5>Address and Contact Details</h5>
                                            </div>
                                            <div class="row">
                                                <div class="form-group col-lg-6 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Permanent Address</label>
                                                    </div>
                                                    <asp:TextBox ID="txtPermanentAddress" runat="server" TabIndex="17" Rows="1"
                                                        TextMode="MultiLine" ToolTip="Please Enter Permanent Address" class="form-control"></asp:TextBox>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>City </label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlcity" runat="server" class="form-control"
                                                        ToolTip="Please Select City." TabIndex="18" AppendDataBoundItems="True" data-select2-enable="true">
                                                    </asp:DropDownList>
                                                    <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlcity"
                                                        Display="None" ErrorMessage="Please Select City." ValidationGroup="academic" InitialValue="0"
                                                        SetFocusOnError="true">
                                                    </asp:RequiredFieldValidator> --%>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>State </label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlstate" runat="server" CssClass="form-control"
                                                        ToolTip="Please Select State" TabIndex="19" AppendDataBoundItems="True" data-select2-enable="true">
                                                    </asp:DropDownList>
                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlstate"
                                                        Display="None" ErrorMessage="Please Select State." ValidationGroup="academic" InitialValue="0"
                                                        SetFocusOnError="true">
                                                    </asp:RequiredFieldValidator> --%>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>PIN </label>
                                                    </div>
                                                    <asp:TextBox ID="txtPIN" runat="server" TabIndex="20" MaxLength="6" ToolTip="Please Enter PIN"
                                                        CssClass="form-control" />
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="fteTxtPin" runat="server" FilterType="Numbers"
                                                        TargetControlID="txtPIN">
                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Student Contact No. </label>
                                                    </div>
                                                    <asp:TextBox ID="txtContactNumber" runat="server" TabIndex="21" ValidationGroup="academic"
                                                        MaxLength="10" ToolTip="Please Enter Contact Number" Class="form-control" />
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="fteTxtContactNum" runat="server" FilterType="Numbers"
                                                        TargetControlID="txtContactNumber">
                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                </div>
                                            </div>
                                        </div>



                                        <asp:Panel ID="upDegree" runat="server">
                                            <div id="divGuardianAddress" class="col-12 mt-3">
                                                <div class="sub-heading">
                                                    <h5>Guardian Address and Contact Details</h5>
                                                </div>
                                                <div class="row">
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Copy Permanent Address</label>
                                                        </div>
                                                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/copy.png" OnClientClick="copyPermAddr(this)"
                                                            ToolTip="Copy Permanent Address" TabIndex="27" />
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Mother Mobile No. </label>
                                                        </div>
                                                        <asp:TextBox ID="txtGuardianPhone" runat="server" CssClass="form-control" ToolTip="Please Enter Mother Mobile No."
                                                            MaxLength="10" TabIndex="24" />
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers"
                                                            TargetControlID="txtGuardianPhone">
                                                        </ajaxToolKit:FilteredTextBoxExtender>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Father Mobile No. </label>
                                                        </div>
                                                        <asp:TextBox ID="txtGuardianMobile" runat="server" CssClass="form-control" ToolTip="Please Enter Guardian Mobile No."
                                                            MaxLength="10" TabIndex="25" />
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server"
                                                            FilterType="Numbers" TargetControlID="txtGuardianMobile">
                                                        </ajaxToolKit:FilteredTextBoxExtender>
                                                        <asp:CompareValidator ID="rfvGuardianMobileNo" runat="server" ControlToValidate="txtGuardianMobile"
                                                            ErrorMessage="Please Numeric Number" Operator="DataTypeCheck" SetFocusOnError="True"
                                                            Type="Integer" ValidationGroup="academic" Display="None"></asp:CompareValidator>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Parents Email </label>
                                                        </div>
                                                        <asp:TextBox ID="txtGuardianEmail" runat="server" TabIndex="26" ToolTip="Please Enter Email"
                                                            CssClass="form-control" />
                                                        <asp:RegularExpressionValidator ID="rfvGuardianEmail" runat="server" ControlToValidate="txtGuardianEmail"
                                                            Display="None" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                            ErrorMessage="Please Enter Valid EmailID" ValidationGroup="academic"></asp:RegularExpressionValidator>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>City </label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlLocalCity" runat="server" class="form-control" data-select2-enable="true"
                                                            ToolTip="Please Select Local City" TabIndex="22" AppendDataBoundItems="True">
                                                        </asp:DropDownList>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>State </label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlLocalState" runat="server" class="form-control"
                                                            ToolTip="Please Select Local State" TabIndex="23" AppendDataBoundItems="True" data-select2-enable="true">
                                                        </asp:DropDownList>
                                                    </div>

                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Postal Address</label>
                                                        </div>
                                                        <asp:TextBox ID="txtPostalAddress" runat="server" TabIndex="28" TextMode="MultiLine" Rows="1"
                                                            ToolTip="Please Enter Postal Address" CssClass="form-control"></asp:TextBox>
                                                    </div>

                                                </div>
                                            </div>


                                            <div id="divAdmissionDetails" style="display: block;" class="col-12 mt-3">
                                                <div class="sub-heading">
                                                    <h5>Admission Details</h5>
                                                </div>
                                                <div class="row">
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Date of Admission </label>
                                                        </div>
                                                        <div class="input-group date">
                                                            <div class="input-group-addon">
                                                                <i class="fa fa-calendar" id="imgStartDate2" runat="server"></i>
                                                            </div>
                                                            <asp:TextBox ID="txtDateOfAdmission" runat="server" TabIndex="29" class="form-control" />
                                                            <asp:RequiredFieldValidator ID="rfvDateOfAdmission" runat="server" ControlToValidate="txtDateOfAdmission"
                                                                Display="none" ErrorMessage="Please Enter Date Of Admission" SetFocusOnError="true"
                                                                ValidationGroup="academic"></asp:RequiredFieldValidator>
                                                            <ajaxToolKit:CalendarExtender ID="ceDateOfAdmission" runat="server" Format="dd/MM/yyyy"
                                                                TargetControlID="txtDateOfAdmission" PopupButtonID="imgStartDate2" Enabled="true"
                                                                EnableViewState="true">
                                                            </ajaxToolKit:CalendarExtender>
                                                        </div>
                                                    </div>


                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <asp:Label ID="lblDYddlSession_Tab" runat="server" Font-Bold="true"></asp:Label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlsession" runat="server" class="form-control" ValidationGroup="academic"
                                                            ToolTip="Please Select Session" TabIndex="30" AppendDataBoundItems="True" data-select2-enable="true">
                                                            <%--<asp:ListItem Value="0">Please Select</asp:ListItem>--%>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlsession"
                                                            Display="None" SetFocusOnError="true" InitialValue="0" ErrorMessage="Please Select Session"
                                                            ValidationGroup="academic"></asp:RequiredFieldValidator>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Degree </label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlDegree" runat="server" class="form-control" ValidationGroup="academic"
                                                            ToolTip="Please Select Degree" TabIndex="31" AppendDataBoundItems="True"
                                                            OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" data-select2-enable="true" AutoPostBack="true">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                                            Display="None" SetFocusOnError="true" InitialValue="0" ErrorMessage="Please Select Degree"
                                                            ValidationGroup="academic"></asp:RequiredFieldValidator>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Branch </label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlBranch" runat="server" class="form-control" TabIndex="32" AppendDataBoundItems="True"
                                                            ValidationGroup="academic" ToolTip="Please Select Branch"
                                                            data-select2-enable="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvddlBranch" runat="server" ControlToValidate="ddlBranch"
                                                            ErrorMessage="Please Select Branch" Display="None" ValidationGroup="academic"
                                                            SetFocusOnError="true" InitialValue="0">
                                                        </asp:RequiredFieldValidator>
                                                        <asp:RequiredFieldValidator ID="rfvDD_Branch" runat="server" ControlToValidate="ddlBranch"
                                                            ErrorMessage="Please Select Branch" Display="None" ValidationGroup="ddinfo" SetFocusOnError="true"
                                                            InitialValue="0">
                                                        </asp:RequiredFieldValidator>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <asp:Label ID="lblDYtxtBatch" runat="server" Font-Bold="true"></asp:Label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlBatch" runat="server" class="form-control" TabIndex="33" AppendDataBoundItems="true"
                                                            ToolTip="Please Select Batch" Enabled="true" data-select2-enable="true" />
                                                        <asp:RequiredFieldValidator ID="rfvBatch" runat="server" ControlToValidate="ddlBatch"
                                                            Display="None" SetFocusOnError="true" InitialValue="0" ErrorMessage="Please Select Batch"
                                                            ValidationGroup="academic"></asp:RequiredFieldValidator>
                                                        <asp:RequiredFieldValidator ID="rfvddino_batch" runat="server" ControlToValidate="ddlBatch"
                                                            Display="None" SetFocusOnError="true" InitialValue="0" ErrorMessage="Please Select Batch"
                                                            ValidationGroup="ddinfo"></asp:RequiredFieldValidator>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <asp:Label ID="lblDYtxtYear" runat="server" Font-Bold="true"></asp:Label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlYear" runat="server" class="form-control" TabIndex="34" AppendDataBoundItems="true"
                                                            ToolTip="Please Select Year" data-select2-enable="true" />
                                                        <asp:RequiredFieldValidator ID="rfvYear" runat="server" ControlToValidate="ddlYear"
                                                            Display="None" SetFocusOnError="true" InitialValue="0" ErrorMessage="Please Select Year"
                                                            ValidationGroup="academic"></asp:RequiredFieldValidator>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Payment Type </label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlPaymentType" runat="server" class="form-control" TabIndex="35" AppendDataBoundItems="true"
                                                            ToolTip="Please Select Payment Type" AutoPostBack="True" data-select2-enable="true" />
                                                        <asp:RequiredFieldValidator ID="rfvPaymentType" runat="server" ControlToValidate="ddlPaymentType"
                                                            Display="None" SetFocusOnError="true" InitialValue="0" ErrorMessage="Please Select Payment Type"
                                                            ValidationGroup="academic"></asp:RequiredFieldValidator>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>
                                                        </div>

                                                        <asp:DropDownList ID="ddlSemester" runat="server" class="form-control" TabIndex="36" AppendDataBoundItems="true"
                                                            ToolTip="Please Select Semester" Enabled="true" data-select2-enable="true" />
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                                        <div class="label-dynamic">
                                                            <label>State of Eligibility </label>
                                                        </div>
                                                        <div class="input-group date">
                                                            <asp:TextBox ID="txtStateOfEligibility" runat="server" class="form-control"
                                                                ToolTip="Please Enter State Of Eligibility" TabIndex="0" />
                                                            <span class="input-group-addon" style="color: red; font-weight: bolder">+</span>
                                                        </div>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Admission Type </label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlAdmType" runat="server" class="form-control" AppendDataBoundItems="true"
                                                            ToolTip="Please Select Admission Type" TabIndex="37" data-select2-enable="true">
                                                        </asp:DropDownList>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12" id="trPhdStatus" runat="server" visible="true">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Mode </label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlStatus" runat="server" class="form-control" TabIndex="38" data-select2-enable="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            <asp:ListItem Value="1">Full Time</asp:ListItem>
                                                            <asp:ListItem Value="2">Part Time</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvStatus" runat="server" ControlToValidate="ddlStatus"
                                                            Display="None" SetFocusOnError="true" InitialValue="0" ErrorMessage="Please Select Mode."
                                                            ValidationGroup="academic"></asp:RequiredFieldValidator>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Supervisor </label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlSupervisor" runat="server" CssClass="form-control" AppendDataBoundItems="True"
                                                            data-select2-enable="true" TabIndex="39">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlSupervisor"
                                                            Display="None" SetFocusOnError="true" InitialValue="0" ErrorMessage="Please Select Supervisor."
                                                            ValidationGroup="academic"></asp:RequiredFieldValidator>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                                        <div class="label-dynamic">
                                                            <label>Employee ID of the Guide</label>
                                                        </div>
                                                        <asp:TextBox ID="txtEMployeeID" runat="server" CssClass="form-control" placeholder="Enter Employee ID of the Guide"></asp:TextBox>
                                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtEMployeeID"
                                                    ErrorMessage="Please Enter Employee ID of the Guide." Display="None" ValidationGroup="academic"
                                                    SetFocusOnError="true">
                                                </asp:RequiredFieldValidator>--%>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Status </label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlStatusCat" runat="server" CssClass="form-control" AppendDataBoundItems="True"
                                                            data-select2-enable="true" TabIndex="40">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            <asp:ListItem Value="1">No Confirmation</asp:ListItem>
                                                            <asp:ListItem Value="2">Awaiting for Viva</asp:ListItem>
                                                            <asp:ListItem Value="3">Course Work Completed</asp:ListItem>
                                                            <asp:ListItem Value="4">Confirmation Completed</asp:ListItem>
                                                            <asp:ListItem Value="5">First DC Meeting Completed</asp:ListItem>
                                                            <asp:ListItem Value="6">Break of Study</asp:ListItem>
                                                            <asp:ListItem Value="7">Course work pending</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddlStatusCat"
                                                            Display="None" SetFocusOnError="true" InitialValue="0" ErrorMessage="Please Select Status."
                                                            ValidationGroup="academic"></asp:RequiredFieldValidator>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divnoc" runat="server" visible="false">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>NOC Submit </label>
                                                        </div>
                                                        <asp:RadioButton ID="rdbnocyes" runat="server" GroupName="PhdNoc" Text="Yes"
                                                            TabIndex="42" />
                                                        <asp:RadioButton ID="rdbnocno" runat="server" GroupName="PhdNoc" Text="No"
                                                            Checked="true" TabIndex="43" />
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label></label>
                                                        </div>
                                                        <asp:RadioButton ID="rdoHostelerYes" runat="server" GroupName="Hosteler" Text="Yes"
                                                            TabIndex="44" Visible="False" />
                                                        <asp:RadioButton ID="rdoHostelerNo" runat="server" GroupName="Hosteler" Text="No"
                                                            Checked="true" TabIndex="45" Visible="False" />
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divsuptype" runat="server" visible="false">
                                                        <div class="label-dynamic">
                                                            <label>Supervisor Type</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlSupervisorType" runat="server" CssClass="form-control" AppendDataBoundItems="True">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            <asp:ListItem Value="1">Internal</asp:ListItem>
                                                            <asp:ListItem Value="2">External</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <%-- </div>--%>



                                                    <div class="form-group col-lg-12 col-md-12 col-12" id="Tr2" runat="server" visible="false">
                                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                                            <label>Co-Supervisor1 Name </label>
                                                            <asp:DropDownList ID="ddlCoSupevisor1" runat="server" Class="form-control" AppendDataBoundItems="True"
                                                                AutoPostBack="True" OnSelectedIndexChanged="ddlCoSupevisor1_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </div>

                                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                                            <label>Co-Supervisor1 Type </label>
                                                            <asp:DropDownList ID="ddlCoSupervisorType1" runat="server" Class="form-control" AppendDataBoundItems="True">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                <asp:ListItem Value="1">Internal</asp:ListItem>
                                                                <asp:ListItem Value="2">External</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>

                                                    <div class="form-group col-lg-12 col-md-12 col-12" id="Tr3" runat="server" visible="false">
                                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                                            <label for="city">Co-Supervisor2 Name</label>
                                                            <asp:DropDownList ID="ddlCoSupevisor2" runat="server" Class="form-control" AppendDataBoundItems="True"
                                                                AutoPostBack="True" OnSelectedIndexChanged="ddlCoSupevisor2_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </div>

                                                        <div id="Div1" class="form-group col-lg-4 col-md-6 col-12" runat="server" visible="false">
                                                            <label for="city">Co-Supervisor2 Type</label>
                                                            <asp:DropDownList ID="ddlCoSupervisorType2" runat="server" Class="form-control" AppendDataBoundItems="True">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                <asp:ListItem Value="1">Internal</asp:ListItem>
                                                                <asp:ListItem Value="2">External</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>

                                                    <div class="form-group col-lg-4 col-md-6 col-12" style="display: none">
                                                        <label for="city">Admission Round</label>
                                                        <asp:DropDownList ID="ddlAdmRound" runat="server" class="form-control" TabIndex="340" AppendDataBoundItems="true" />
                                                    </div>

                                                </div>

                                            </div>


                                            <div id="divEntranceExamScores" class="col-12">
                                                <div class="sub-heading">
                                                    <h5>Qualified Exam</h5>
                                                </div>
                                                <div class="row">
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Exam Name</label>
                                                        </div>

                                                        <asp:DropDownList ID="ddlExamNo" runat="server" AppendDataBoundItems="True" class="form-control"
                                                            TabIndex="40" data-select2-enable="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Institute/College</label>
                                                        </div>

                                                        <asp:TextBox ID="txtinstitute" runat="server" class="form-control" TabIndex="41"></asp:TextBox>
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server"
                                                            TargetControlID="txtinstitute" FilterType="Custom" FilterMode="InvalidChars"
                                                            InvalidChars="0123456789@!#$%^*?{}:';,~" />
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Year Of Exam </label>
                                                        </div>

                                                        <asp:TextBox ID="txtYearOfExam" runat="server" class="form-control" TabIndex="41" onkeyup="validateNumeric(this);"
                                                            MaxLength="4"></asp:TextBox>
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server"
                                                            TargetControlID="txtYearOfExam" FilterType="Custom" FilterMode="ValidChars"
                                                            ValidChars="0123456789" />
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Percentage </label>
                                                        </div>
                                                        <asp:TextBox ID="txtPer" runat="server" class="form-control" ToolTip="Please Enter Percentage"
                                                            onkeyup="validateNumeric(this);" TabIndex="42"></asp:TextBox>
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server"
                                                            TargetControlID="txtPer" FilterType="Custom" FilterMode="ValidChars"
                                                            ValidChars="0123456789." />
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12" id="trNet" runat="server" visible="false">
                                                        <div class="label-dynamic">
                                                            <label>Net Exam </label>
                                                        </div>
                                                        <asp:RadioButtonList ID="rblNet" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" TabIndex="43">
                                                            <asp:ListItem Value="0">Yes</asp:ListItem>
                                                            <asp:ListItem Value="1" Selected="True">No</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>


                                                    <div class="form-group col-lg-4 col-md-6 col-12" id="trInd" runat="server" visible="false">
                                                        <div class="form-group col-sm-4">
                                                            <label for="city">All India Rank</label>
                                                            <asp:TextBox ID="txtAllIndiaRank" runat="server" class="form-control" ToolTip="Please Enter All India Rank"
                                                                TabIndex="44"></asp:TextBox>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="fteAllIndiaRank" runat="server" FilterType="Numbers"
                                                                TargetControlID="txtAllIndiaRank">
                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                        </div>

                                                        <div class="form-group col-sm-4" style="display: none">
                                                            <label for="city">State Rank</label>
                                                            <asp:TextBox ID="txtStateRank" runat="server" class="form-control" ToolTip="Please Enter State Rank"
                                                                TabIndex="45"></asp:TextBox>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="fteStateRank" runat="server" FilterType="Numbers"
                                                                TargetControlID="txtStateRank">
                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                        </div>

                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12" style="display: none">
                                                        <label for="city">Exam Roll No</label>
                                                        <asp:TextBox ID="txtQExamRollNo" runat="server" class="form-control" ToolTip="Please Enter Qualifying Exam Roll No"
                                                            TabIndex="31"></asp:TextBox>
                                                        <%--<ajaxToolKit:FilteredTextBoxExtender ID="ftrQExamRollNo" runat="server" FilterType="Numbers"
                                    TargetControlID="txtQExamRollNo">
                                </ajaxToolKit:FilteredTextBoxExtender>--%>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Percentile </label>
                                                        </div>
                                                        <asp:TextBox ID="txtPercentile" runat="server" CssClass="form-control" onkeyup="validateNumeric(this);"
                                                            TabIndex="46"></asp:TextBox>
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server"
                                                            TargetControlID="txtPercentile" FilterType="Custom" FilterMode="ValidChars"
                                                            ValidChars="0123456789." />
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                                        <label for="city">Quota </label>
                                                        <asp:DropDownList ID="ddlQuota" class="form-control" data-select2-enable="true" runat="server" AppendDataBoundItems="True"
                                                            AutoPostBack="True" TabIndex="47">
                                                        </asp:DropDownList>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="trSpotOption" visible="false">
                                                        <div class="form-group col-sm-4">
                                                            <label for="city">Spot Option  </label>

                                                            <asp:DropDownList ID="ddlSpotOption" runat="server" Class="form-control">
                                                                <asp:ListItem Selected="True" Value="0">--Please Select--</asp:ListItem>
                                                                <asp:ListItem Value="1">GATE with Scholarship</asp:ListItem>
                                                                <asp:ListItem Value="2">Non GATE without Scholarship</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>

                                                        <div class="form-group col-sm-4">
                                                            <label for="city">Amount Paid To DD   </label>
                                                            <asp:TextBox ID="txtDDAmountPaid" runat="server" CssClass="form-control" TabIndex="48"></asp:TextBox>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server"
                                                                TargetControlID="txtDDAmountPaid" FilterType="Numbers" />
                                                            <asp:RequiredFieldValidator ID="DDpaidAmt" runat="server" ControlToValidate="txtDDAmountPaid"
                                                                Display="None" ErrorMessage="Please enter DD through paid amount." SetFocusOnError="true"
                                                                ValidationGroup="academic" />
                                                        </div>

                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="trReconcile" visible="false">
                                                        <label for="city">Reconcile Process  </label>
                                                        <asp:RadioButtonList ID="rblReconcile" runat="server" RepeatDirection="Horizontal">
                                                            <asp:ListItem Selected="True" Value="0">Yes</asp:ListItem>
                                                            <asp:ListItem Value="1">No</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="trPaytype" visible="false">
                                                        <label for="city">Pay Type </label>
                                                        <div id="divPaytype" runat="server" class="col-sm-12">
                                                            <asp:Panel ID="UpdpayType" runat="server">

                                                                <asp:TextBox ID="txtPayType" runat="server" TabIndex="49" CssClass="form-control" onblur="ValidatePayType(this); UpdateCash_DD_Amount();"
                                                                    MaxLength="1" ToolTip="Enter D for payment by demand draft."></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="valPayType" runat="server" ControlToValidate="txtPayType"
                                                                    Display="None" ErrorMessage="Please enter type of payment whether demand draft(D)."
                                                                    SetFocusOnError="true" ValidationGroup="academic" />



                                                            </asp:Panel>
                                                        </div>
                                                    </div>

                                                    <div class="form-group col-lg-4 col-md-6 col-12" id="trmsg" runat="server" visible="false">
                                                        <label>
                                                            <span style="color: red;">Note : Please enter type of pay Type whether  demand draft(D) or SBI-Collect
                                                    (I) </span>
                                                        </label>
                                                    </div>

                                                    <div id="divDDDetails" runat="server" style="display: none" class="form-group col-md-12">
                                                        <h3>Demand Draft Details</h3>
                                                        <div class="form-group col-sm-3">
                                                            <label>DD/Check No.</label>
                                                            <asp:TextBox ID="txtDDNo" runat="server" TabIndex="50" CssClass="data_label" Class="form-control" />
                                                            <asp:RequiredFieldValidator ID="valDDNo" ControlToValidate="txtDDNo" runat="server"
                                                                Display="None" ErrorMessage="Please enter demand draft number." ValidationGroup="dd_info" />
                                                        </div>

                                                        <div class="form-group col-sm-4">
                                                            <label>Amount </label>
                                                            <asp:TextBox ID="txtDDAmount" onkeyup="IsNumeric(this);" runat="server" TabIndex="51 "
                                                                Class="form-control" CssClass="data_label" />
                                                            <asp:RequiredFieldValidator ID="valDdAmount" ControlToValidate="txtDDAmount" runat="server"
                                                                Display="None" ErrorMessage="Please enter amount of demand draft." ValidationGroup="dd_info" />
                                                        </div>

                                                        <div class="form-group col-sm-4">
                                                            <label>Date </label>
                                                            <div class="input-group date">
                                                                <div class="input-group-addon">
                                                                    <i class="fa fa-calendar"></i>
                                                                </div>
                                                                <asp:TextBox ID="txtDDDate" runat="server" TabIndex="52" CssClass="data_label" Class="form-control" />
                                                                <ajaxToolKit:CalendarExtender ID="ceDDDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDDDate"
                                                                    PopupButtonID="txtDDDate" />
                                                                <ajaxToolKit:MaskedEditExtender ID="meeDDDate" runat="server" TargetControlID="txtDDDate"
                                                                    Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" ErrorTooltipEnabled="true"
                                                                    OnInvalidCssClass="errordate" />
                                                                <ajaxToolKit:MaskedEditValidator ID="mevDDDate" runat="server" ControlExtender="meeDDDate"
                                                                    ControlToValidate="txtDDDate" IsValidEmpty="False" EmptyValueMessage="Demand draft date is required"
                                                                    InvalidValueMessage="Demand draft date is invalid" EmptyValueBlurredText="*"
                                                                    InvalidValueBlurredMessage="*" Display="Dynamic" ValidationGroup="dd_info" />
                                                            </div>
                                                        </div>

                                                        <div class="form-group col-sm-4">
                                                            <label for="city">City</label>
                                                            <asp:TextBox ID="txtDDCity" runat="server" TabIndex="53" CssClass="data_label" Class="form-control" />
                                                        </div>
                                                        <div class="form-group col-sm-4">
                                                            <label for="city">Bank</label>
                                                            <asp:DropDownList ID="ddlBank" AppendDataBoundItems="true" TabIndex="54" runat="server"
                                                                Class="form-control" />
                                                            <asp:RequiredFieldValidator ID="valBankName" runat="server" ControlToValidate="ddlBank"
                                                                Display="None" ErrorMessage="Please select bank name." ValidationGroup="dd_info"
                                                                InitialValue="0" SetFocusOnError="true" />
                                                            <p>
                                                                &nbsp;<asp:ValidationSummary ID="valSummery2" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                                    ShowSummary="false" ValidationGroup="dd_info" />
                                                            </p>
                                                        </div>

                                                    </div>

                                                    <div id="divCashDate" runat="server" style="display: none" class="form-group col-md-4">
                                                        <label>Date</label>
                                                        <div class="input-group date">
                                                            <div class="input-group-addon">
                                                                <i class="fa fa-calendar"></i>
                                                            </div>
                                                            <asp:TextBox ID="txtCashDate" runat="server" TabIndex="55" CssClass="data_label" Class="form-control" />

                                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                                                TargetControlID="txtCashDate" PopupButtonID="txtCashDate" />
                                                            <ajaxToolKit:MaskedEditExtender ID="meeDate" runat="server" TargetControlID="txtCashDate"
                                                                Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" ErrorTooltipEnabled="true"
                                                                OnInvalidCssClass="errordate" />
                                                            <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="meeDate"
                                                                ControlToValidate="txtDDDate" IsValidEmpty="False" EmptyValueMessage="date is required"
                                                                InvalidValueMessage="date is invalid" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                                                Display="Dynamic" ValidationGroup="dd_info" />
                                                        </div>

                                                    </div>

                                                    <div id="divUnder" runat="server" style="display: none" class="form-group col-md-12">
                                                        <div class="form-group col-sm-4">
                                                            <label>Date of UnderTaking</label>
                                                            <div class="input-group date">
                                                                <div class="input-group-addon">
                                                                    <i class="fa fa-calendar" id="I1"></i>
                                                                </div>

                                                                <asp:TextBox ID="txtUnder" runat="server" TabIndex="8" CssClass="data_label" Class="form-control" />

                                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                                                    TargetControlID="txtUnder" PopupButtonID="I1" />
                                                                <ajaxToolKit:MaskedEditExtender ID="meunder" runat="server" TargetControlID="txtUnder"
                                                                    Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" ErrorTooltipEnabled="true"
                                                                    OnInvalidCssClass="errordate" />
                                                                <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="meunder"
                                                                    ControlToValidate="txtUnder" IsValidEmpty="False" EmptyValueMessage="date is required"
                                                                    InvalidValueMessage="date is invalid" EmptyValueBlurredText="" InvalidValueBlurredMessage="*"
                                                                    Display="None" ValidationGroup="dd_info" />
                                                            </div>
                                                        </div>
                                                        <div class="form-group col-sm-4">
                                                            <label>Remark </label>
                                                            <asp:TextBox ID="txtRemarkunder" runat="server" TabIndex="56" CssClass="data_label" Class="form-control" />
                                                        </div>
                                                    </div>


                                                    <div class="form-group col-lg-12 col-md-12 col-12 text-center">
                                                        <asp:RequiredFieldValidator ID="rfvExamName" runat="server" ControlToValidate="ddlExamNo"
                                                            ValidationGroup="EntranceExam" Display="None" SetFocusOnError="true" InitialValue="0"
                                                            ErrorMessage="Please Select Exam Name"></asp:RequiredFieldValidator>
                                                        <asp:RequiredFieldValidator ID="rfvYearOfEntranceExam" runat="server" ControlToValidate="txtYearOfExam"
                                                            Display="None" SetFocusOnError="true" ValidationGroup="EntranceExam" ErrorMessage="Please Enter Year of Exam"></asp:RequiredFieldValidator>
                                                    </div>

                                                </div>
                                            </div>

                                            <div id="divUpload" class="col-12">
                                                <div class="sub-heading">
                                                    <h5>Photo And Sign Upload </h5>
                                                </div>
                                                <div class="row">
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Photo Upload</label>
                                                        </div>
                                                        <div class="image">
                                                            <asp:Image ID="imgPhoto" runat="server" Width="120px" Height="128px" src="../../Images/nophoto.jpg" Style="margin-bottom: 10px;" />
                                                            <%-- <asp:Image ID="imgapplicantsign" runat="server" Height="30" Width="100" style="margin-right: 30px;"/>--%>
                                                        </div>
                                                        <asp:FileUpload ID="fuPhotoUpload" runat="server" TabIndex="57" onChange="LoadImage()" accept=".jpg" />
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationExpression="([a-zA-Z0-9\s_\\.\-:])+(.JPEG|.jpg|.JPG)$"
                                                            ControlToValidate="fuPhotoUpload" runat="server" ForeColor="Red" ErrorMessage="Please select a JPG Format ." ValidationGroup="submit"
                                                            Display="Dynamic" />

                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Sign Upload</label>
                                                        </div>
                                                   
                                                        <div class="image">
                                                            <asp:Image ID="ImgSign" runat="server" src="../../Images/sign11.jpg" Width="100%" Height="60px" Style="margin-bottom: 10px;" />
                                                        </div>
                                                        <asp:FileUpload ID="fuSignUpload" runat="server" onChange="LoadImageSign()" TabIndex="58" accept=".jpg" />
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ValidationExpression="([a-zA-Z0-9\s_\\.\-:])+(.JPEG|.jpg|.JPG)$"
                                                            ControlToValidate="fuSignUpload" runat="server" ForeColor="Red" ErrorMessage="Please select a JPG Format." ValidationGroup="submit"
                                                            Display="Dynamic" />
                                                    </div>


                                                </div>
                                            </div>

                                        </asp:Panel>

                                        <div class="col-12 btn-footer">
                                            <asp:Button ID="btnSave" runat="server" Text="Submit" ToolTip="Submit Student Information"
                                                ValidationGroup="academic" CssClass="btn btn-primary" OnClick="btnSave_Click" TabIndex="59"
                                                Enabled="true" />

                                            <asp:Button ID="btnReport" runat="server" Text="Admission Slip" ToolTip="Admission Slip For Student "
                                                CausesValidation="false" CssClass="btn btn-info" TabIndex="60" ValidationGroup="PayType"
                                                Font-Bold="True" OnClick="btnReport_Click" Visible="false" />
                                            <asp:Button ID="btnChallan" runat="server" Text="Challan" ToolTip="Print Challan" TabIndex="61"
                                                CausesValidation="false" CssClass="btn btn-info" Font-Bold="True" OnClick="btnChallan_Click"
                                                Visible="false" />
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" ToolTip="Cancel Student Information"
                                                CausesValidation="false" CssClass="btn btn-warning" TabIndex="62" ValidationGroup="academic"
                                                OnClick="btnCancel_Click" Font-Bold="True" />

                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="academic"
                                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="ddinfo" />

                                            <asp:Label ID="lblHelp" runat="server" Font-Names="Trebuchet MS" />
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btnSave" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="ImageButton1" />
            <asp:PostBackTrigger ControlID="ddlPhyHandicap" />
            <asp:PostBackTrigger ControlID="ddlPaymentType" />
            <asp:PostBackTrigger ControlID="ddlExamNo" />
            <asp:PostBackTrigger ControlID="ddlStatus" />
            <asp:AsyncPostBackTrigger ControlID="ddlDegree" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlBranch" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlSpotOption" EventName="SelectedIndexChanged" />
            <asp:PostBackTrigger ControlID="btnSearch" />
            <asp:PostBackTrigger ControlID="btnSave" />
            <asp:PostBackTrigger ControlID="btnReport" />
            <asp:PostBackTrigger ControlID="btnChallan" />
            <asp:PostBackTrigger ControlID="btnCancel" />
        </Triggers>
    </asp:UpdatePanel>



    <script type="text/javascript">
        function ValidateBranch() {
            if (document.getElementById('<%= ddlBranch.ClientID %>').selectedIndex == 0) {
                alert("Please Select Branch");
                return false;
            }
            else
                return true;
        }

        function copyPermAddr(chk) {
            if (chk.click) {
                var city = document.getElementById('<%= ddlcity.ClientID %>').value;
                var state = document.getElementById('<%= ddlstate.ClientID %>').value;
                document.getElementById('<%= txtPostalAddress.ClientID %>').value = document.getElementById('<%= txtPermanentAddress.ClientID %>').value
                document.getElementById('<%= ddlLocalCity.ClientID %>').value = city;
                document.getElementById('<%= ddlLocalState.ClientID %>').value = state;
            }
            else {
                document.getElementById('<%= txtPostalAddress.ClientID %>').value = '';
            }
        }

        function ValidatePayType(txtPayType) {
            try {
                if (txtPayType != null && txtPayType.value != '') {
                    if (txtPayType.value.toUpperCase() == 'D') {


                        txtPayType.value = "D";
                        if (document.getElementById('<%= divDDDetails.ClientID %>') != null) {
                            document.getElementById('<%= divDDDetails.ClientID %>').style.display = "block";
                            document.getElementById('<%= txtDDNo.ClientID%>').focus();
                        }
                        if (document.getElementById('<%= divCashDate.ClientID %>') != null) {
                            document.getElementById('<%= divCashDate.ClientID %>').style.display = "none";
                        }
                    }
                    else {
                        if (txtPayType.value.toUpperCase() == 'I') {
                            txtPayType.value = "I";
                            if (document.getElementById('<%= divDDDetails.ClientID %>') != null) {
                                document.getElementById('<%= divDDDetails.ClientID %>').style.display = "block";
                                document.getElementById('<%= txtDDNo.ClientID%>').focus();
                            }
                            if (document.getElementById('<%= divCashDate.ClientID %>') != null) {
                                document.getElementById('<%= divCashDate.ClientID %>').style.display = "none";
                            }
                        }
                        else {
                            if (txtPayType.value.toUpperCase() == 'C') {
                                txtPayType.value = "C";
                                if (document.getElementById('<%= divDDDetails.ClientID %>') != null) {
                                    document.getElementById('<%= divDDDetails.ClientID %>').style.display = "none";
                                }
                                if (document.getElementById('<%= divCashDate.ClientID %>') != null) {
                                    document.getElementById('<%= divCashDate.ClientID %>').style.display = "block";

                                    //document.getElementById('ctl00_ContentPlaceHolder1_divFeeItems').style.display = "block";
                                }
                            }
                            else
                                if (txtPayType.value.toUpperCase() == 'U') {
                                    txtPayType.value = "U";
                                    if (document.getElementById('<%= divCashDate.ClientID %>') != null) {
                                        document.getElementById('<%= divCashDate.ClientID %>').style.display = "none";
                                    }
                                    if (document.getElementById('<%= divUnder.ClientID %>') != null) {
                                        document.getElementById('<%= divUnder.ClientID %>').style.display = "block";

                                    }
                                }
                                else {
                                    //OR 'U' for SC/ST undertaking  'C' for Cash payment OR
                                    alert("Please enter only 'D' for payment through Demand Drafts OR 'I' for payment through SBI-Collect .");
                                    if (document.getElementById('<%= divDDDetails.ClientID %>') != null || document.getElementById('<%= divCashDate.ClientID %>') != null)
                                        document.getElementById('<%= divDDDetails.ClientID %>').style.display = "none";
                                    document.getElementById('<%= divCashDate.ClientID %>').style.display = "none";

                                    txtPayType.value = "";
                                    txtPayType.focus();
                                }
                        }
                    }
                }
            }
            catch (e) {
                alert("Error: " + e.description);
            }
        }

        function UpdateCash_DD_Amount() {
            try {
                var txtPayType = document.getElementById('ctl00_ContentPlaceHolder1_txtPayType');
                var txtPaidAmt = document.getElementById('ctl00_ContentPlaceHolder1_txtTotalAmount');

                if (txtPayType != null && txtPaidAmt != null) {
                    //if (txtPayType.value.trim() == "C" && document.getElementById('ctl00_ContentPlaceHolder1_txtTotalCashAmt') != null) {
                    //    document.getElementById('ctl00_ContentPlaceHolder1_txtTotalCashAmt').value = txtPaidAmt.value.trim();
                    //} else
                    if (txtPayType.value.trim() == "D" && document.getElementById('tblDD_Details') != null) {
                        var totalDDAmt = 0.00;
                        var dataRows = document.getElementById('tblDD_Details').getElementsByTagName('tr');
                        if (dataRows != null) {
                            for (i = 1; i < dataRows.length; i++) {
                                var dataCellCollection = dataRows.item(i).getElementsByTagName('td');
                                var dataCell = dataCellCollection.item(6);
                                if (dataCell != null) {
                                    var txtAmt = dataCell.innerHTML.trim();
                                    totalDDAmt += parseFloat(txtAmt);
                                }
                            }
                            if (document.getElementById('ctl00_ContentPlaceHolder1_txtTotalDDAmount') != null &&
                                document.getElementById('ctl00_ContentPlaceHolder1_txtTotalCashAmt') != null) {
                                document.getElementById('ctl00_ContentPlaceHolder1_txtTotalDDAmount').value = totalDDAmt;
                                document.getElementById('ctl00_ContentPlaceHolder1_txtTotalCashAmt').value = parseFloat(txtPaidAmt.value.trim()) - parseFloat(totalDDAmt);
                            }
                        }
                    }
                }
            }
            catch (e) {
                alert("Error: " + e.description);
            }
        }
    </script>

    <div id="divMsg" runat="server">
    </div>

    <div style="width: 400px; margin: 0 auto;">
        <ejs-toast id="element">
        <e-toast-position X="Right" Y="Bottom"></e-toast-position>
    </ejs-toast>
        <div class="row">
            <ejs-button id="showToast" content="Show Types" cssclass="e-btn"></ejs-button>
        </div>
    </div>


    <script type="text/javascript">
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            function RunThisAfterEachAsyncPostback() {
                $(function () {
                    $("#<%=txtDateOfBirth.ClientID%>").datepicker({
                        changeMonth: true,
                        changeYear: true,
                        dateFormat: 'dd/mm/yy',
                        yearRange: '1975:' + getCurrentYear()
                    });
                });
            }

            $(function () {
                $("#<%=txtDateOfAdmission.ClientID%>").datepicker({
                    changeMonth: true,
                    changeYear: true,
                    dateFormat: 'dd/mm/yy',
                    yearRange: '1975:' + getCurrentYear()
                });
            });

            function getCurrentYear() {
                var cDate = new Date();
                return cDate.getFullYear();
            }
        });
    </script>
    <script type="text/javascript">
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                $('[id*=txtStateOfEligibility]').autocomplete({
                    source: function (request, response) {
                        $.ajax({
                            url: "../WebService.asmx/GetData",
                            data: "{ 'data': '" + request.term + "' ,'tablename': 'ACD_STATE','col1': 'STATENO','col2': 'STATENAME','col3': '' }",
                            dataType: "json",
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            dataFilter: function (data) { return data; },
                            success: function (data) {
                                response($.map(data.d, function (item) {
                                    return {
                                        value: item
                                    }
                                }))
                            },
                            error: function (XMLHttpRequest, textStatus, errorThrown) {
                                alert(errorThrown);
                            }
                        });
                    },
                    minLength: 1

                });
            });
        });
    </script>


    <%-- <script type="text/javascript">
          RunThisAfterEachAsyncPostback();
          Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
    </script>--%>
    <script type="text/javascript">
        function makeUppercase() {
            document.form_name.name.value = document.form_name.name.value.toUpperCase();
        }
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
