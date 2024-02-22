<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddressDetails.aspx.cs" MasterPageFile="~/SiteMasterPage.master" Inherits="ACADEMIC_AddressDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updAddressDetails"
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
        #sidebar {
            display: none;
        }

        .page-wrapper.toggled .page-content {
            padding-left: 15px;
        }

        .panel-info > .panel-heading b {
            padding: 8px;
            font-size: 12px;
        }

        .sidebar-menu {
            padding: 0;
            list-style: none;
        }

            .sidebar-menu .treeview {
                padding: 0px 0px;
                color: #255282;
            }

        .treeview i {
            padding-left: 10px;
        }

        .treeview span a {
            color: #255282 !important;
            font-weight: 600;
            padding-left: 3px;
        }

            .treeview span a:hover {
                color: #0d70fd !important;
            }

        .treeview.active i, .treeview.active span a {
            color: #0d70fd !important;
        }

        hr {
            margin: 12px 0px;
            border-top: 1px solid #ccc;
        }

        #ctl00_ContentPlaceHolder1_divtabs {
            box-shadow: rgb(0 0 0 / 20%) 0px 5px 10px;
            padding: 15px 5px;
            margin: 5px 0px 15px 0px;
        }

        .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>
    <asp:UpdatePanel ID="updAddressDetails" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">STUDENT INFORMATION</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="col-lg-2 col-md-4 col-12" id="divtabs" runat="server">
                                        <aside class="sidebar">

                                            <!-- sidebar: style can be found in sidebar.less -->
                                            <section class="sidebar" style="background-color: #ffffff">
                                                <ul class="sidebar-menu">
                                                    <!-- Optionally, you can add icons to the links -->
                                                    <li class="treeview" id="divhome" runat="server">
                                                        <i class="fa fa-search"></i>
                                                        <span>
                                                            <asp:LinkButton runat="server" ID="lnkGoHome"
                                                                ToolTip="Please Click Here To Go To Home" OnClick="lnkGoHome_Click" Text="Search New Student"> 
                                                            </asp:LinkButton>
                                                        </span>
                                                        <hr />
                                                    </li>
                                                    <li class="treeview">
                                                        <i class="fa fa-user"></i>
                                                        <span>
                                                            <asp:LinkButton runat="server" ID="lnkPersonalDetail"
                                                                ToolTip="Please select Personal Details" OnClick="lnkPersonalDetail_Click" Text="Personal Details"> 
                                                            </asp:LinkButton>
                                                        </span>
                                                        <hr />
                                                    </li>

                                                    <li class="treeview active">
                                                        <i class="fa fa-map-marker"></i>
                                                        <span>
                                                            <asp:LinkButton runat="server" ID="lnkAddressDetail"
                                                                ToolTip="Please select Address Details" OnClick="lnkAddressDetail_Click" Text="Address Details"> 
                                                            </asp:LinkButton>
                                                        </span>
                                                        <hr />
                                                    </li>

                                                    <li class="treeview" id="divadmissiondetails" runat="server">
                                                        <i class="fa fa-university"></i>
                                                        <span>
                                                            <asp:LinkButton runat="server" ID="lnkAdmissionDetail"
                                                                ToolTip="Please select Admission Details" OnClick="lnkAdmissionDetail_Click" Text="Admission Details"> 
                                                            </asp:LinkButton>
                                                        </span>
                                                        <hr />
                                                    </li>

                                                    <li class="treeview" style="display: none">
                                                        <i class="fa fa-info-circle"></i>
                                                        <span>
                                                            <asp:LinkButton runat="server" ID="lnkDasaStudentInfo"
                                                                ToolTip="Please select DASA Student Information" Text="Information"> 
                                                            </asp:LinkButton>
                                                        </span>
                                                        <hr />
                                                    </li>

                                                    <li class="treeview">
                                                        <i class="fa fa-file"></i>
                                                        <span>
                                                            <asp:LinkButton runat="server" ID="lnkUploadDocument"
                                                                ToolTip="Please Upload Documents" OnClick="lnkUploadDocument_Click" Text="Document Upload"> 
                                                            </asp:LinkButton>
                                                        </span>
                                                        <hr />
                                                    </li>
                                                    <li class="treeview">
                                                        <i class="fa fa-graduation-cap"></i>
                                                        <span>
                                                            <asp:LinkButton runat="server" ID="lnkQualificationDetail"
                                                                ToolTip="Please select Qualification Details" OnClick="lnkQualificationDetail_Click" Text="Qualification Details"> 
                                                            </asp:LinkButton>
                                                        </span>
                                                        <hr />
                                                    </li>

                                                    <li class="treeview">
                                                        <i class="fa fa-stethoscope"></i>
                                                        <span>
                                                            <asp:LinkButton runat="server" ID="lnkCovid" Visible="true"
                                                                ToolTip="Covid Vaccination Details" OnClick="lnkCovid_Click" Text="Covid Information"> 
                                                            </asp:LinkButton>
                                                        </span>
                                                        <hr />
                                                    </li>

                                                    <li class="treeview">
                                                        <i class="fa fa-link"></i>
                                                        <span>
                                                            <asp:LinkButton runat="server" ID="lnkotherinfo"
                                                                ToolTip="Please select Other Information." OnClick="lnkotherinfo_Click" Text="Other Information"> 
                                                            </asp:LinkButton>
                                                        </span>
                                                        <hr />
                                                    </li>

                                                    <li class="treeview" id="divAdmissionApprove" runat="server">
                                                        <i class="fa fa-check-circle"></i>
                                                        <span>
                                                            <asp:LinkButton runat="server" ID="lnkApproveAdm"
                                                                ToolTip="Verify Information" OnClick="lnkApproveAdm_Click" Text="Verify Information"> 
                                                            </asp:LinkButton>
                                                        </span>
                                                        <hr />
                                                    </li>

                                                    <li class="treeview" id="divPrintReport" runat="server" visible="false">
                                                        <i class="fas fa-print"></i>
                                                        <span>
                                                            <asp:LinkButton runat="server" ID="lnkprintapp" OnClick="lnkprintapp_Click" Text="Print"></asp:LinkButton>
                                                        </span>
                                                    </li>
                                                </ul>
                                            </section>
                                        </aside>
                                    </div>

                                    <div class="col-lg-10 col-md-8 col-12" id="divAddressAndContactDetails" style="display: block;">
                                        <asp:UpdatePanel ID="upAddressDetails" runat="server">
                                            <ContentTemplate>
                                                <div class="col-12 pl-0 pr-0 pl-lg-2 pr-lg-2">
                                                    <div class="row">

                                                        <div class="col-md-12">
                                                            <div class="sub-heading">
                                                                <h5>Permanent Address</h5>
                                                            </div>
                                                        </div>
                                                        <%--Added IDs to the div(s) and sup(s) by Shrikant W. on 25-12-2023--%>
                                                        <div class="form-group col-lg-6 col-md-6 col-12" id="divAddDetails" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup id="supAddDetails" runat="server">* </sup>
                                                                <label>Address Details </label>
                                                            </div>
                                                            <asp:ImageButton ID="imgbToCopyLocalAddress" Visible="false" runat="server" ImageUrl="~/images/copy.png"
                                                                OnClientClick="copyLocalAddr(this)" ToolTip="Copy Local Address" TabIndex="11" />
                                                            <asp:TextBox ID="txtPermAddress" CssClass="form-control" runat="server" TextMode="MultiLine"
                                                                Rows="1" MaxLength="200" ToolTip="Please Enter Permanent Address"
                                                                TabIndex="12" />
                                                            <%--<asp:RequiredFieldValidator ID="rfvPermAddress" runat="server" ControlToValidate="txtPermAddress"
                                                                Display="None" ErrorMessage="Please Enter Permanent Address" SetFocusOnError="True"
                                                                ValidationGroup="Academic"></asp:RequiredFieldValidator>--%>

                                                            <asp:TextBox ID="txtPdistrict" CssClass="form-control" runat="server" Visible="False"></asp:TextBox>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server"
                                                                FilterMode="InvalidChars" FilterType="Custom" InvalidChars="1234567890" TargetControlID="txtPdistrict" />
                                                        </div>


                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divPCountry" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup id="supPCountry" runat="server">* </sup>
                                                                <label>Country </label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlPermCountry" CssClass="form-control" runat="server" AppendDataBoundItems="True" TabIndex="15"
                                                                ToolTip="Please Select Country" AutoPostBack="True" OnSelectedIndexChanged="ddlPermCountry_SelectedIndexChanged" data-select2-enable="true" />
                                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlPermCountry"
                                                                Display="None" ErrorMessage="Please Select Permanent Country" SetFocusOnError="True"
                                                                ValidationGroup="Academic" InitialValue="0"></asp:RequiredFieldValidator>--%>
                                                        </div>




                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divPState" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup id="supPState" runat="server">* </sup>
                                                                <label>State </label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlPermState" CssClass="form-control" runat="server" AppendDataBoundItems="True" TabIndex="15"
                                                                ToolTip="Please Select State" AutoPostBack="True" OnSelectedIndexChanged="ddlPermState_SelectedIndexChanged" data-select2-enable="true" />
                                                            <%--<asp:RequiredFieldValidator ID="rfvPermState" runat="server" ControlToValidate="ddlPermState"
                                                                Display="None" ErrorMessage="Please Select Permanent State" SetFocusOnError="True"
                                                                ValidationGroup="Academic" InitialValue="0"></asp:RequiredFieldValidator>--%>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divPDistrict" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup id="supPDistrict" runat="server">* </sup>
                                                                <label>District</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlPdistrict" CssClass="form-control" runat="server" AppendDataBoundItems="True" AutoPostBack="true"
                                                                ToolTip="Please select district" TabIndex="14" data-select2-enable="true">
                                                            </asp:DropDownList>
                                                            <asp:HiddenField ID="hdn_Pdistrict" runat="server" />
                                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="ddlPdistrict"
                                                                Display="None" ErrorMessage="Please Select Permanent District" SetFocusOnError="True"
                                                                ValidationGroup="Academic" InitialValue="0"></asp:RequiredFieldValidator>--%>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divPTaluka" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup id="supPTaluka" runat="server">* </sup>
                                                                <label>Taluka </label>
                                                            </div>
                                                            <%-- <asp:DropDownList ID="ddlptaluka" CssClass="form-control" runat="server" AppendDataBoundItems="True" AutoPostBack="true"
                                                                ToolTip="Please select Taluka" TabIndex="13" data-select2-enable="true">
                                                            </asp:DropDownList>--%>

                                                            <asp:TextBox ID="txtPTaluka" CssClass="form-control" runat="server" ToolTip="Please Enter Taluka"
                                                                ValidationGroup="Academic" TabIndex="13" MaxLength="30" placeholder="Enter Taluka Name" onkeydown="return((event.keyCode >= 64 && event.keyCode <= 91) || (event.keyCode==32)|| (event.keyCode==8)|| (event.keyCode==9));" />



                                                            <asp:HiddenField ID="HiddenField1" runat="server" />
                                                            <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="ddlPdistrict"
                                                            Display="None" ErrorMessage="Please Select Permanent District" SetFocusOnError="True"
                                                            ValidationGroup="Academic" InitialValue="0"></asp:RequiredFieldValidator>--%>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divPCity" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup id="supPCity" runat="server">* </sup>
                                                                <label>City/Village </label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlPermCity" CssClass="form-control" runat="server" TabIndex="12" AppendDataBoundItems="True"
                                                                ToolTip="Please Select City" data-select2-enable="true">
                                                            </asp:DropDownList>
                                                            <%--<asp:RequiredFieldValidator ID="rfvPermCity" runat="server" ControlToValidate="ddlPermCity"
                                                                Display="None" ErrorMessage="Please select Permanent City/Village" SetFocusOnError="True"
                                                                ValidationGroup="Academic" InitialValue="0"></asp:RequiredFieldValidator>--%>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divPPincode" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup id="supPPincode" runat="server">* </sup>
                                                                <label>Pin Code </label>
                                                            </div>
                                                            <asp:TextBox ID="txtPermPIN" CssClass="form-control" runat="server" TabIndex="16" ToolTip="Please Enter PIN"
                                                                MaxLength="6" onkeyup="validateNumeric(this);" placeholder="Enter ZIP/PIN" />
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftrPermPIN" runat="server" FilterType="Numbers"
                                                                TargetControlID="txtPermPIN">
                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtPermPIN"
                                                                Display="None" ErrorMessage="Please Enter Permanent Pin Code" SetFocusOnError="True"
                                                                ValidationGroup="Academic"></asp:RequiredFieldValidator>--%>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divPLandLineNo" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup id="supPLandLineNo" runat="server">* </sup>
                                                                <label>Landline No.</label>
                                                            </div>
                                                            <asp:TextBox ID="txtLocalNo" CssClass="form-control" runat="server" ToolTip="Please Enter Number"
                                                                ValidationGroup="Academic" TabIndex="17" MaxLength="12" placeholder="Enter Landline No." />
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="fteLoaclno" runat="server" FilterType="Numbers"
                                                                TargetControlID="txtLocalNo">
                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divlocalmobileaddress" runat="server" visible="false">
                                                            <div class="label-dynamic">
                                                                <label>Mobile No.</label>
                                                            </div>
                                                            <asp:TextBox ID="txtMobileNo" CssClass="form-control" runat="server" ToolTip="Please Enter Mobile No." TabIndex="18"
                                                                MaxLength="12" placeholder="Enter Mobile No." />
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="fteMobilenum" runat="server" FilterType="Numbers"
                                                                TargetControlID="txtMobileNo">
                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                            <asp:CompareValidator ID="rfvMobileNo" runat="server" ControlToValidate="txtMobileNo"
                                                                ErrorMessage="Please Numeric Number" Operator="DataTypeCheck" SetFocusOnError="True"
                                                                Type="Integer" ValidationGroup="Academic" Display="None" Visible="False"></asp:CompareValidator>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                                            <div class="label-dynamic">
                                                                <label>Parent Email Id</label>
                                                            </div>
                                                            <asp:TextBox ID="txtPermEmailId" CssClass="form-control" runat="server" ToolTip="Please Enter E-Mail Address" TabIndex="59"
                                                                ValidationGroup="Academic" />
                                                            <asp:RegularExpressionValidator ID="revPermEmailId" runat="server" ControlToValidate="txtPermEmailId"
                                                                Display="Dynamic" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                                ErrorMessage="Please Enter Valid EmailID" ValidationGroup="Academic"></asp:RegularExpressionValidator>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divbirthtehsil" runat="server" visible="false">
                                                            <div class="label-dynamic">
                                                                <label>Sub Division/Tehsil </label>
                                                            </div>
                                                            <asp:TextBox ID="txtTehsil" CssClass="form-control" runat="server" MaxLength="150" TabIndex="17" ToolTip="Please Enter Tehsil" onkeydown="return((event.keyCode >= 64 && event.keyCode <= 91) || (event.keyCode==32)|| (event.keyCode==8)|| (event.keyCode==9));" />
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender30" runat="server"
                                                                TargetControlID="txtTehsil" FilterType="Custom" FilterMode="InvalidChars"
                                                                InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" />
                                                            <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtTehsil"
                                                        Display="None" ErrorMessage="Please Enter Sub Division/Tehsil" SetFocusOnError="True" ToolTip="Please enter Sub Division/Tehsil"
                                                        ValidationGroup="Academic"></asp:RequiredFieldValidator>--%>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-3 col-12" id="divPAreaPostOffice" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup id="supAreaPostOffice" runat="server">* </sup>
                                                                <label>Area Post Office </label>
                                                            </div>
                                                            <asp:TextBox ID="txtpermpostOff" CssClass="form-control" runat="server" MaxLength="150" TabIndex="19" ToolTip="Please Enter Post Office" onkeypress="return alphaOnly(event);" placeholder="Enter Area Post Office" />
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender56" runat="server"
                                                                TargetControlID="txtpermpostOff" FilterType="Custom" FilterMode="InvalidChars"
                                                                InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" />
                                                            <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtpermpostOff"
                                                        Display="None" ErrorMessage="Please Enter Permanent Area Post Office" SetFocusOnError="True"
                                                        ValidationGroup="Academic"></asp:RequiredFieldValidator>--%>
                                                        </div>

                                                        <div class="form-group col-lg-6 col-md-6 col-12" id="divAreaPoliceStation" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup id="supAreaPoliceStation" runat="server">* </sup>
                                                                <label>Area Police Station </label>
                                                            </div>
                                                            <asp:TextBox ID="txtPermPoliceStation" CssClass="form-control" runat="server" MaxLength="150" TabIndex="20" onkeypress="return alphaOnly(event);" placeholder="Enter Area Police Station"
                                                                ToolTip="Please Enter Police Station" />
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender31" runat="server"
                                                                TargetControlID="txtPermPoliceStation" FilterType="Custom" FilterMode="InvalidChars"
                                                                InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" />
                                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtPermPoliceStation"
                                                        Display="None" ErrorMessage="Please Enter Permanent Area Police Station" SetFocusOnError="True" ToolTip="Please Enter Area Police Station"
                                                        ValidationGroup="Academic"></asp:RequiredFieldValidator>--%>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                                            <div class="label-dynamic">
                                                                <label>Sub Division/Tehsil</label>
                                                            </div>
                                                            <asp:TextBox ID="txtPermTehsil" CssClass="form-control" runat="server" MaxLength="150" ToolTip="Please Enter Tehsil" TabIndex="20" />
                                                        </div>

                                                    </div>

                                                    <div class="row">
                                                        <div class="col-md-12 mt-3">
                                                            <div class="sub-heading">
                                                                <%--<h5>Local Address  (Copy Permanent)</h5>--%>
                                                                <h5>Local Address  </h5>
                                                            </div>
                                                        </div>
                                                        <div class="form-group col-lg-12 col-md-12 col-12">
                                                            <asp:CheckBox ID="chkcopypermanentadress" onclick="copyPermanentlAddr(this)" runat="server" TabIndex="20" Text="&nbsp;&nbsp;Same as Permanent Address" />
                                                        </div>

                                                        <div class="form-group col-lg-6 col-md-6 col-12" id="divLAddDetails" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup id="supLAddDetails" runat="server">* </sup>
                                                                <label>Address Details </label>
                                                            </div>
                                                            <asp:TextBox ID="txtLocalAddress" CssClass="form-control" runat="server" TextMode="MultiLine"
                                                                Rows="1" MaxLength="200" ToolTip="Please Enter Local ddress" TabIndex="21" />
                                                            <asp:TextBox ID="txtLdistrict" runat="server" Visible="False"></asp:TextBox>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server"
                                                                FilterMode="InvalidChars" FilterType="Custom" InvalidChars="1234567890" TargetControlID="txtLdistrict" />

                                                            <%--   <asp:RequiredFieldValidator ID="rfvtxtLocalAddress" runat="server" ControlToValidate="txtLocalAddress"
                                                            Display="None" ErrorMessage="Please Enter Local Address" SetFocusOnError="True"
                                                            ValidationGroup="Academic"></asp:RequiredFieldValidator>--%>
                                                        </div>


                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divLCountry" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup id="supLCountry" runat="server">* </sup>
                                                                <label>Country </label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlLocalCountry" CssClass="form-control" runat="server" AppendDataBoundItems="True" TabIndex="15"
                                                                ToolTip="Please Select Country" AutoPostBack="True" OnSelectedIndexChanged="ddlLocalCountry_SelectedIndexChanged" data-select2-enable="true" />
                                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlLocalCountry"
                                                                Display="None" ErrorMessage="Please Select Local Country" SetFocusOnError="True"
                                                                ValidationGroup="Academic" InitialValue="0"></asp:RequiredFieldValidator>--%>
                                                        </div>



                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divLState" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup id="supLState" runat="server">* </sup>
                                                                <label>State </label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlLocalState" CssClass="form-control" runat="server" AppendDataBoundItems="True" data-select2-enable="true"
                                                                ToolTip="Please Select State" TabIndex="25" AutoPostBack="True" OnSelectedIndexChanged="ddlLocalState_SelectedIndexChanged" />
                                                            <%--      <asp:RequiredFieldValidator ID="rfvLocalState" runat="server" ControlToValidate="ddlLocalState"
                                                            Display="None" ErrorMessage="Please Select Local State" SetFocusOnError="True"
                                                            ValidationGroup="Academic" InitialValue="0"></asp:RequiredFieldValidator>--%>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divLDistrict" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup id="supLDistrict" runat="server">* </sup>
                                                                <label>District </label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlLdistrict" CssClass="form-control" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                                                TabIndex="24" ToolTip="Please Select District" data-select2-enable="true">
                                                            </asp:DropDownList>
                                                            <asp:HiddenField ID="hdnldistrict" runat="server" />
                                                            <asp:CompareValidator ID="rfvLocalPIN" runat="server" ControlToValidate="txtLocalPIN"
                                                                Display="None" ErrorMessage="Please Enter Numeric Value" Operator="DataTypeCheck"
                                                                SetFocusOnError="True" Type="Integer" ValidationGroup="Academic" Visible="False"></asp:CompareValidator>
                                                            <%--     <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="ddlLdistrict"
                                                            Display="None" ErrorMessage="Please Select Local District" SetFocusOnError="True"
                                                            ValidationGroup="Academic" InitialValue="0"></asp:RequiredFieldValidator>--%>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divLTaluka" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup id="supLTaluka" runat="server">* </sup>
                                                                <label>Taluka </label>
                                                            </div>
                                                            <%-- <asp:DropDownList ID="ddlLtaluka" CssClass="form-control" runat="server" AppendDataBoundItems="True" AutoPostBack="true"
                                                                ToolTip="Please select Taluka" TabIndex="23" data-select2-enable="true">
                                                            </asp:DropDownList>--%>

                                                            <asp:TextBox ID="txtLTaluka" CssClass="form-control" runat="server" ToolTip="Please Enter Taluka"
                                                                ValidationGroup="Academic" TabIndex="13" MaxLength="30" placeholder="Enter Taluka Name" onkeydown="return((event.keyCode >= 64 && event.keyCode <= 91) || (event.keyCode==32)|| (event.keyCode==8)|| (event.keyCode==9));" />


                                                            <asp:HiddenField ID="HiddenField2" runat="server" />
                                                            <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="ddlPdistrict"
                                                            Display="None" ErrorMessage="Please Select Permanent District" SetFocusOnError="True"
                                                            ValidationGroup="Academic" InitialValue="0"></asp:RequiredFieldValidator>--%>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divLCity" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup id="supLCity" runat="server">* </sup>
                                                                <label>City/Village </label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlLocalCity" CssClass="form-control" runat="server" TabIndex="22" AppendDataBoundItems="true"
                                                                ToolTip="Please Select City" data-select2-enable="true">
                                                            </asp:DropDownList>
                                                            <%--     <asp:RequiredFieldValidator ID="rfvddlLocalCity" runat="server" ControlToValidate="ddlLocalCity"
                                                            Display="None" ErrorMessage="Please Select Local City" SetFocusOnError="True"
                                                            TabIndex="23" ValidationGroup="Academic" InitialValue="0"></asp:RequiredFieldValidator>--%>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divLPin" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup id="supLPin" runat="server">* </sup>
                                                                <label>ZIP/PIN </label>
                                                            </div>
                                                            <asp:TextBox ID="txtLocalPIN" CssClass="form-control" runat="server" TabIndex="26" MaxLength="6" placeholder="Enter ZIP/PIN" ToolTip="Please Enter PIN" />
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftbPINLoal" runat="server" FilterType="Numbers"
                                                                TargetControlID="txtLocalPIN">
                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                            <%--   <asp:RequiredFieldValidator ID="rfvtxtLocalPIN" runat="server" ControlToValidate="txtLocalPIN"
                                                            Display="None" ErrorMessage="Please Enter Local Pin Code" SetFocusOnError="True"
                                                            ValidationGroup="Academic"></asp:RequiredFieldValidator>--%>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divLLandLineNo" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup id="supLLandLineNo" runat="server">* </sup>
                                                                <label>Landline No.</label>
                                                            </div>

                                                            <asp:TextBox ID="txtLocalLandlineNo" CssClass="form-control" runat="server" ToolTip="Please Enter Landline Number"
                                                                TabIndex="27" MaxLength="12" placeholder="Enter Landline No." />
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="fteLocalLandline" runat="server" FilterType="Numbers"
                                                                TargetControlID="txtLocalLandlineNo">
                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divpermenantaddress" runat="server" visible="false">
                                                            <div class="label-dynamic">
                                                                <label>Mobile No.</label>
                                                            </div>

                                                            <asp:TextBox ID="txtLocalMobileNo" CssClass="form-control" runat="server" ToolTip="Please Enter Mobile No."
                                                                TabIndex="7" MaxLength="12" placeholder="Enter Mobile No." />
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="fteLocalMobile" runat="server" FilterType="Numbers"
                                                                TargetControlID="txtLocalMobileNo">
                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                            <asp:CompareValidator ID="rfvLocalMobileNo" runat="server" ControlToValidate="txtLocalMobileNo"
                                                                ErrorMessage="Please Numeric Number" Operator="DataTypeCheck" SetFocusOnError="True"
                                                                Type="Integer" ValidationGroup="Academic" Display="None"></asp:CompareValidator>
                                                            <asp:RegularExpressionValidator runat="server" ErrorMessage="Mobile No. is Invalid" ID="RegularExpressionValidator4" ControlToValidate="txtLocalMobileNo" ValidationExpression=".{10}.*"
                                                                Display="None" ValidationGroup="Academic"></asp:RegularExpressionValidator>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                                            <div class="label-dynamic">
                                                                <label>Parent Email ID</label>
                                                            </div>
                                                            <asp:TextBox ID="txtLocalEmail" CssClass="form-control" runat="server" ToolTip="Please Enter E-Mail Address" placeholder="Enter Parent Email ID" />
                                                            <asp:RegularExpressionValidator ID="rfvLocalEmail" runat="server" ControlToValidate="txtLocalEmail"
                                                                Display="Dynamic" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                                ErrorMessage="Please Enter Valid Local EmailId" ValidationGroup="Academic"></asp:RegularExpressionValidator>
                                                            <%-- <asp:RequiredFieldValidator ID="rfvLocalCity" runat="server" SetFocusOnError="True"
                                                        ControlToValidate="ddlLocalCity" Display="None" ErrorMessage="Please select Local City"
                                                        ValidationGroup="Academic" Visible="False"></asp:RequiredFieldValidator>--%>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-3 col-12" id="divLAreaPostOffice" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup id="supLAreaPostOffice" runat="server">* </sup>
                                                                <label>Area Post Office </label>
                                                            </div>
                                                            <asp:TextBox ID="txtpostoff" CssClass="form-control" runat="server" MaxLength="150" TabIndex="28" ToolTip="Please Enter Post Office" onkeypress="return alphaOnly(event);" placeholder="Enter Area Post Office" />
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender55" runat="server"
                                                                TargetControlID="txtpostoff" FilterType="Custom" FilterMode="InvalidChars"
                                                                InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" />
                                                            <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtpostoff"
                                                        Display="None" ErrorMessage="Please Enter Local Post Office" SetFocusOnError="True"
                                                        ValidationGroup="Academic"></asp:RequiredFieldValidator>--%>
                                                        </div>

                                                        <div class="form-group col-lg-6 col-md-6 col-12" id="divLAreaPoliceStation" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup id="supLAreaPoliceStation" runat="server">* </sup>
                                                                <label>Area Police Station </label>
                                                            </div>
                                                            <asp:TextBox ID="txtpolicestation" CssClass="form-control" runat="server" MaxLength="150" TabIndex="29" ToolTip="Please Enter Police Station" onkeypress="return alphaOnly(event);" placeholder="Enter Area Police Station" />
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender29" runat="server"
                                                                TargetControlID="txtpolicestation" FilterType="Custom" FilterMode="InvalidChars"
                                                                InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" />
                                                            <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtpolicestation"
                                                        Display="None" ErrorMessage="Please Enter Local Area Police Station" SetFocusOnError="True"
                                                        ValidationGroup="Academic"></asp:RequiredFieldValidator>--%>
                                                        </div>

                                                        <asp:Panel ID="pnlvisible" Visible="false" runat="server">
                                                            <div class="form-group col-md-12">
                                                                <label>Correspondance Address</label>
                                                                <asp:TextBox ID="txtCorresAddress" CssClass="form-control" runat="server" MaxLength="150" ToolTip="Please Enter Correspondace Address" TextMode="MultiLine" TabIndex="48" />
                                                            </div>
                                                        </asp:Panel>

                                                        <asp:Panel ID="pnlcorrenspondence" runat="server" Visible="false">
                                                            <div class="form-group col-md-3">
                                                                <label>Correspondace Pincode</label>
                                                                <asp:TextBox ID="txtCorresPin" CssClass="form-control" runat="server" MaxLength="6" TabIndex="49" ToolTip="Please Enter Correspondace Pin" />
                                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender24" runat="server" FilterType="Numbers"
                                                                    TargetControlID="txtCorresPin">
                                                                </ajaxToolKit:FilteredTextBoxExtender>
                                                            </div>
                                                            <div class="form-group col-md-3">
                                                                <label>Correspondance Mob No.</label>
                                                                <asp:TextBox ID="txtCorresMob" CssClass="form-control" runat="server" MaxLength="10" TabIndex="50" ToolTip="Please Enter Correspondace Mob No." />
                                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender25" runat="server" FilterType="Numbers"
                                                                    TargetControlID="txtCorresMob">
                                                                </ajaxToolKit:FilteredTextBoxExtender>
                                                            </div>
                                                        </asp:Panel>

                                                    </div>

                                                    <div class="row">
                                                        <div class="col-md-12 mt-3">
                                                            <div class="sub-heading">
                                                                <h5>Local Guardian's Address</h5>
                                                            </div>
                                                        </div>
                                                        <div class="form-group col-lg-12 col-md-12 col-12">
                                                            <asp:CheckBox ID="chkcopyperaddress" onclick="copyPermAddr(this)" TabIndex="30" runat="server" Text="&nbsp;&nbsp;Same as Permanent Address" />
                                                        </div>

                                                        <div class="form-group col-lg-6 col-md-6 col-12" id="divGAddDetails" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup id="supGAddDetails" runat="server">* </sup>
                                                                <label>Address Details(Copy Permanent) </label>
                                                            </div>
                                                            <asp:ImageButton ID="imgCopyPermAddress" Visible="false" runat="server" ImageUrl="~/images/copy.png"
                                                                OnClientClick="copyPermAddr(this)" ToolTip="Copy Permanent Address" TabIndex="31" />
                                                            <asp:TextBox ID="txtGuardianAddress" CssClass="form-control" runat="server" TextMode="MultiLine"
                                                                Rows="1" MaxLength="200" ToolTip="Please Enter Guardian Address" TabIndex="31"></asp:TextBox>
                                                            <%--<asp:RequiredFieldValidator ID="rfvGuardianAddress" runat="server" ControlToValidate="txtGuardianAddress"
                                                        Display="None" ErrorMessage="Please Enter Guardian Address" SetFocusOnError="True"
                                                        ValidationGroup="Academic" Visible="False"></asp:RequiredFieldValidator>--%>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divGuardianName" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup id="supGuardianName" runat="server">* </sup>
                                                                <label>Guardian Name </label>
                                                            </div>
                                                            <asp:TextBox ID="txtGuardianName" CssClass="form-control" runat="server" ToolTip="Please Enter Guardian Name" onkeypress="return alphaOnly(event);" placeholder="Enter Guardian Name" TabIndex="32"></asp:TextBox>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server"
                                                                TargetControlID="txtGuardianName" FilterType="Custom" FilterMode="InvalidChars"
                                                                InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" />
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divGPhoneNo" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup id="supGPhoneNo" runat="server">* </sup>
                                                                <label>Guardian Ph. No.</label>
                                                            </div>

                                                            <asp:TextBox ID="txtGuardianLandline" CssClass="form-control" runat="server" TabIndex="33" MaxLength="15" ToolTip="Please Enter Guardian Ph. No." placeholder="Enter Guardian Ph. No."></asp:TextBox>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="fteGuardianLandline" runat="server" FilterType="Numbers"
                                                                TargetControlID="txtGuardianLandline">
                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                            <asp:RegularExpressionValidator runat="server" ErrorMessage="Guardian Ph. No. is Invalid" ID="RegularExpressionValidator5" ControlToValidate="txtGuardianLandline" ValidationExpression=".{10}.*"
                                                                Display="None" ValidationGroup="Academic"></asp:RegularExpressionValidator>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                                            <div class="label-dynamic">
                                                                <label>Annual Income</label>
                                                            </div>

                                                            <asp:TextBox ID="txtAnnualIncome" CssClass="form-control" runat="server" TabIndex="25" MaxLength="12" ToolTip="Please Enter Annual Income"></asp:TextBox>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="fteAnnualIncome" runat="server" FilterType="Numbers"
                                                                TargetControlID="txtAnnualIncome">
                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divGRelation" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup id="supGRelation" runat="server">* </sup>
                                                                <label>Guardian's Relation </label>
                                                            </div>
                                                            <asp:TextBox ID="txtRelationWithGuardian" CssClass="form-control" runat="server" ToolTip="Please Enter Relation" onkeypress="return alphaOnly(event);" placeholder="Enter Guardian's Relation" TabIndex="34"></asp:TextBox>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server"
                                                                TargetControlID="txtRelationWithGuardian" FilterType="Custom" FilterMode="InvalidChars"
                                                                InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" />
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divOccupation" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup id="supOccupation" runat="server">* </sup>
                                                                <label>Occupation</label>
                                                            </div>
                                                            <asp:TextBox ID="txtGoccupationName" CssClass="form-control" runat="server" ToolTip="Please Enter GOccupation"
                                                                ValidationGroup="Academic" TabIndex="35" onkeypress="return alphaOnly(event);" placeholder="Enter Guardian Occupation"></asp:TextBox>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender32" runat="server"
                                                                TargetControlID="txtGoccupationName" FilterType="Custom" FilterMode="InvalidChars"
                                                                InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" />
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                                            <div class="label-dynamic">
                                                                <label>Guardian Email Address</label>
                                                            </div>
                                                            <asp:TextBox ID="txtguardianEmail" CssClass="form-control" runat="server" TabIndex="70" ToolTip="Please Enter E-Mail"
                                                                ValidationGroup="Academic" />
                                                            <asp:RegularExpressionValidator ID="revGuardianEmail" runat="server" ControlToValidate="txtguardianEmail"
                                                                Display="Dynamic" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                                ErrorMessage="Please Enter Valid EmailID" ValidationGroup="Academic"></asp:RegularExpressionValidator>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divGQual" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup id="supGQual" runat="server">* </sup>
                                                                <label>Guardian Qualification</label>
                                                            </div>
                                                            <asp:TextBox ID="txtGDesignation" CssClass="form-control" runat="server" ToolTip="Please Enter Guardian Qualification"
                                                                ValidationGroup="Academic" TabIndex="36" onkeypress="return alphaOnly(event);" placeholder="Enter Guardian Qualification"></asp:TextBox>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="fteGDesignation" runat="server" TargetControlID="txtGDesignation"
                                                                FilterType="Custom" FilterMode="InvalidChars" InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" />
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divOtherInfo" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup id="supOtherInfo" runat="server">* </sup>
                                                                <label>Other Information</label>
                                                            </div>
                                                            <asp:TextBox ID="txtOtherInfo" CssClass="form-control" runat="server" TabIndex="1" TextMode="MultiLine"></asp:TextBox>
                                                            
                                                        </div>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnSubmit" runat="server" TabIndex="37" Text="Save & Continue >>" ToolTip="Click to Report"
                                        CssClass="btn btn-primary" OnClick="btnSubmit_Click" ValidationGroup="Academic" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="True"
                                        ShowSummary="False" ValidationGroup="Academic" />

                                    <%--    <asp:Button ID="btnGohome" runat="server" Visible="false" TabIndex="23" Text="Go Back Home" ToolTip="Click to Go Back Home"
                                                class="btn btn-warning btnGohome" UseSubmitBehavior="false" OnClick="btnGohome_Click" />--%>
                                    <button runat="server" id="btnGohome" visible="false" tabindex="30" onserverclick="btnGohome_Click" class="btn btn-warning btnGohome" tooltip="Click to Go Back Home">
                                        Go Back Home
                                    </button>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSubmit" />

        </Triggers>
    </asp:UpdatePanel>

    <script type="text/javascript">
        function alphaOnly(e) {
            var code;
            if (!e) var e = window.event;

            if (e.keyCode) code = e.keyCode;
            else if (e.which) code = e.which;

            if ((code >= 48) && (code <= 57)) { return false; }
            return true;



        }
    </script>
    <script type="text/javascript" language="javascript">
        function toggleExpansion(imageCtl, divId) {
            if (document.getElementById(divId).style.display == "block") {
                document.getElementById(divId).style.display = "none";
                imageCtl.src = "../images/expand_blue.jpg";
            }
            else if (document.getElementById(divId).style.display == "none") {
                document.getElementById(divId).style.display = "block";
                imageCtl.src = "../images/collapse_blue.jpg";
            }
        }

        function copyLocalAddr(chk) {
            debugger;
            if (chk.checked) {

                document.getElementById('<%= ddlPermState.ClientID %>').selectedIndex = document.getElementById('<%=ddlLocalState.ClientID %>').selectedIndex;

                document.getElementById('<%= txtPermAddress.ClientID %>').value = document.getElementById('<%= txtLocalAddress.ClientID %>').value;
                document.getElementById('<%= ddlPermCountry.ClientID %>').selectedIndex = document.getElementById('<%= ddlLocalCountry.ClientID %>').selectedIndex;
                document.getElementById('<%= ddlPermCity.ClientID %>').selectedIndex = document.getElementById('<%=ddlLocalCity.ClientID %>').selectedIndex;

                document.getElementById('<%= hdn_Pdistrict.ClientID %>').value = document.getElementById('<%= ddlLdistrict.ClientID %>').selectedIndex;
                document.getElementById('<%= ddlPdistrict.ClientID %>').selectedIndex = document.getElementById('<%= ddlLdistrict.ClientID %>').selectedIndex;
                document.getElementById('<%= txtPTaluka.ClientID %>').selectedIndex = document.getElementById('<%= txtLTaluka.ClientID %>').value;             //

                document.getElementById('<%= txtPermPIN.ClientID %>').value = document.getElementById('<%= txtLocalPIN.ClientID %>').value;

                document.getElementById('<%= txtLocalNo.ClientID %>').value = document.getElementById('<%= txtLocalLandlineNo.ClientID %>').value;
                document.getElementById('<%= txtpermpostOff.ClientID %>').value = document.getElementById('<%= txtpostoff.ClientID %>').value;
                document.getElementById('<%= txtPermPoliceStation.ClientID %>').value = document.getElementById('<%= txtpolicestation.ClientID %>').value;

                //No Need For Indus University
                //  document.getElementById('<%= txtPermEmailId.ClientID %>').value = document.getElementById('<%= txtLocalEmail.ClientID %>').value;
                //  document.getElementById('<%= txtMobileNo.ClientID %>').value = document.getElementById('<%= txtLocalMobileNo.ClientID %>').value;
                //  document.getElementById('<%= txtPermTehsil.ClientID %>').value = document.getElementById('<%= txtTehsil.ClientID %>').value;

            }
            else {


                document.getElementById('<%= txtPermAddress.ClientID %>').value = '';
                document.getElementById('<%= ddlPermCountry.ClientID %>').selectedIndex = 0;

                document.getElementById('<%= ddlPermCity.ClientID %>').selectedIndex = 0;
                //document.getElementById('<%= ddlPdistrict.ClientID %>').selectedIndex=0;
                //document.getElementById('<%= txtPdistrict.ClientID %>').value = '';
                document.getElementById('<%= ddlPermState.ClientID %>').selectedIndex = 0;
                document.getElementById('<%= txtPermPIN.ClientID %>').value = '';

                document.getElementById('<%= ddlPermState.ClientID %>').selectedIndex = 0;

                document.getElementById('<%= ddlPdistrict.ClientID %>').selectedIndex = 0;
                document.getElementById('<%= txtPTaluka.ClientID %>').value = '';
                document.getElementById('<%= txtLocalNo.ClientID %>').value = '';
                document.getElementById('<%= txtpermpostOff.ClientID %>').value = '';
                document.getElementById('<%= txtPermPoliceStation.ClientID %>').value = '';
            }
        }


        function copyPermanentlAddr(chk) {
            debugger;
            if (chk.checked) {


                // document.getElementById('<%= ddlLocalState.ClientID %>').value = document.getElementById('<%= ddlPermState.ClientID %>').value;

                document.getElementById('<%= txtLocalAddress.ClientID %>').value = document.getElementById('<%= txtPermAddress.ClientID %>').value;
                document.getElementById('<%= txtLocalPIN.ClientID %>').value = document.getElementById('<%=txtPermPIN.ClientID %>').value;

                $("#ctl00_ContentPlaceHolder1_ddlLocalCountry").val(document.getElementById('<%=ddlPermCountry.ClientID %>').value).change(); //= document.getElementById('<%=ddlPermCountry.ClientID %>').selectedIndex;
                $("#ctl00_ContentPlaceHolder1_ddlLocalState").val(document.getElementById('<%=ddlPermState.ClientID %>').value).change();
                $("#ctl00_ContentPlaceHolder1_ddlLdistrict").val(document.getElementById('<%=ddlPdistrict.ClientID %>').value).change(); //= document.getElementById('<%=ddlPdistrict.ClientID %>').selectedIndex;
                $("#ctl00_ContentPlaceHolder1_ddlLtaluka").val(document.getElementById('<%=txtPTaluka.ClientID %>').value).change(); //= document.getElementById('<%=txtPTaluka.ClientID %>').selectedIndex;

                // document.getElementById('<%= txtLTaluka.ClientID %>').value = document.getElementById('<%= txtPTaluka.ClientID %>').value;

                //$("#ctl00_ContentPlaceHolder1_ddlLocalCity").text() = $("#ctl00_ContentPlaceHolder1_ddlPermCity").text();//document.getElementById('<%=ddlPermCity.ClientID %>').innerText;
                $("#ctl00_ContentPlaceHolder1_ddlLocalCity").val(document.getElementById('<%=ddlPermCity.ClientID %>').value).change(); //= document.getElementById('<%=ddlPermCity.ClientID %>').selectedIndex;
                // document.getElementById('<%= ddlLocalCity.ClientID %>').selectedIndex = document.getElementById('<%=ddlPermCity.ClientID %>').selectedIndex;
                document.getElementById('<%= ddlLocalCountry.ClientID %>').selectedIndex = document.getElementById('<%=ddlPermCountry.ClientID %>').selectedIndex;
                document.getElementById('<%= ddlLocalState.ClientID %>').selectedIndex = document.getElementById('<%=ddlPermState.ClientID %>').selectedIndex;

                document.getElementById('<%= ddlLdistrict.ClientID %>').selectedIndex = document.getElementById('<%= ddlPdistrict.ClientID %>').selectedIndex;
                // document.getElementById('<%= ddlLdistrict.ClientID %>').value = document.getElementById('ctl00_ContentPlaceHolder1_ddlPdistrict').value;  //commented by deepali
                // document.getElementById('<%= hdnldistrict.ClientID %>').value = document.getElementById('ctl00_ContentPlaceHolder1_ddlPdistrict').selectedIndex;
                document.getElementById('<%= txtLTaluka.ClientID %>').value = document.getElementById('<%= txtPTaluka.ClientID %>').value;
                // alert(document.getElementById('<%= ddlLdistrict.ClientID %>').selectedIndex);
                //  alert(document.getElementById('<%= ddlLdistrict.ClientID %>').value);
                // alert(document.getElementById('<%= ddlPdistrict.ClientID %>').selectedIndex);
                // alert(document.getElementById('<%= ddlPdistrict.ClientID %>').value);wait


                document.getElementById('<%= txtpostoff.ClientID %>').value = document.getElementById('<%=txtpermpostOff.ClientID %>').value;
                document.getElementById('<%= txtpolicestation.ClientID %>').value = document.getElementById('<%=txtPermPoliceStation.ClientID %>').value;
                document.getElementById('<%= txtLocalLandlineNo.ClientID %>').value = document.getElementById('<%=txtLocalNo.ClientID %>').value;
                document.getElementById('<%= txtTehsil.ClientID %>').value = document.getElementById('<%= txtPermTehsil.ClientID %>').value;
                document.getElementById('<%= txtLocalEmail.ClientID %>').value = document.getElementById('<%=txtPermEmailId.ClientID %>').value;
                document.getElementById('<%= txtLocalMobileNo.ClientID %>').value = document.getElementById('<%=txtMobileNo.ClientID %>').value;


                //document.getElementById('<%= txtPermAddress.ClientID %>').value = document.getElementById('<%= txtLocalAddress.ClientID %>').value;
                // document.getElementById('<%= hdn_Pdistrict.ClientID %>').value = document.getElementById('<%= ddlLdistrict.ClientID %>').selectedIndex;
                // document.getElementById('<%= ddlPdistrict.ClientID %>').selectedIndex = document.getElementById('<%= ddlLdistrict.ClientID %>').selectedIndex;


                // document.getElementById('<%= txtPermPIN.ClientID %>').value = document.getElementById('<%= txtLocalPIN.ClientID %>').value;
                // document.getElementById('<%= ddlPermCity.ClientID %>').selectedIndex = document.getElementById('<%= ddlLocalCity.ClientID %>').selectedIndex;
                // document.getElementById('<%= ddlPermState.ClientID %>').selectedIndex = document.getElementById('<%= ddlLocalState.ClientID %>').selectedIndex;
                // document.getElementById('<%= txtpermpostOff.ClientID %>').value = document.getElementById('<%= txtpostoff.ClientID %>').value;
                // document.getElementById('<%= txtPermPoliceStation.ClientID %>').value = document.getElementById('<%= txtpolicestation.ClientID %>').value;
                // document.getElementById('<%= txtPermTehsil.ClientID %>').value = document.getElementById('<%= txtTehsil.ClientID %>').value;
                // document.getElementById('<%= txtPermEmailId.ClientID %>').value = document.getElementById('<%= txtLocalEmail.ClientID %>').value;
                //  document.getElementById('<%= txtMobileNo.ClientID %>').value = document.getElementById('<%= txtLocalMobileNo.ClientID %>').value;
                //  document.getElementById('<%= txtLocalNo.ClientID %>').value = document.getElementById('<%= txtLocalLandlineNo.ClientID %>').value;

                document.getElementById('<%= txtPTaluka.ClientID %>').value = '';

            }

            else {

                document.getElementById('<%= txtLocalAddress.ClientID %>').value = '';

                $("#ctl00_ContentPlaceHolder1_ddlLocalCity").val('0').change();
                document.getElementById('<%= ddlLocalCountry.ClientID %>').selectedIndex = 0;
                $("#ctl00_ContentPlaceHolder1_ddlLdistrict").val('0').change();

                $("#ctl00_ContentPlaceHolder1_ddlLocalState").val('0').change();

                $("#ctl00_ContentPlaceHolder1_ddlLtaluka").val('0').change();

                document.getElementById('<%= txtLTaluka.ClientID %>').value = '';                          //Added By Ruchika Dhakate on 16.09.2022

                document.getElementById('<%= ddlLdistrict.ClientID %>').selectedIndex = 0;

                document.getElementById('<%= ddlLocalState.ClientID %>').selectedIndex = 0;

                document.getElementById('<%= txtLocalPIN.ClientID %>').value = '';

                document.getElementById('<%= txtLocalLandlineNo.ClientID %>').value = '';

                document.getElementById('<%= txtpostoff.ClientID %>').value = '';

                document.getElementById('<%= txtpolicestation.ClientID %>').value = '';

                //$("#ctl00_ContentPlaceHolder1_ddlLocalCountry").val('0').change();

                // document.getElementById('<%= ddlLocalCountry.ClientID %>').selectedIndex = 0;

                // document.getElementById('<%= txtPTaluka.ClientID %>').value = '';                          //Added By Ruchika Dhakate on 16.09.2022




                //    document.getElementById('<%= ddlPermState.ClientID %>').selectedIndex = 0;
                // document.getElementById('<%= txtPermPIN.ClientID %>').value = '';

                // document.getElementById('<%= ddlPermState.ClientID %>').selectedIndex = 0;


                //  document.getElementById('<%= txtPermAddress.ClientID %>').value = '';

                // document.getElementById('<%= ddlPermState.ClientID %>').selectedIndex = 0;

                //  document.getElementById('<%= ddlPermCity.ClientID %>').selectedIndex = 0;
                //  document.getElementById('<%= ddlPermState.ClientID %>').selectedIndex = 0;


            }
        }

        function copyPermAddr(chk) {
            if (chk.checked) {
                document.getElementById('<%= txtGuardianAddress.ClientID %>').value = document.getElementById('<%= txtPermAddress.ClientID %>').value;
                if (document.getElementById('<%= ddlPermCity.ClientID %>').selectedIndex > 0) {
                    document.getElementById('<%= txtGuardianAddress.ClientID %>').value =
                document.getElementById('<%= txtGuardianAddress.ClientID %>').value + ', ' + '\n' +
                document.getElementById('<%= ddlPermCity.ClientID %>').options[document.getElementById('<%= ddlPermCity.ClientID %>').selectedIndex].text;
                }
                if (document.getElementById('<%= ddlPermState.ClientID %>').selectedIndex > 0) {
                    document.getElementById('<%= txtGuardianAddress.ClientID %>').value =
                document.getElementById('<%= txtGuardianAddress.ClientID %>').value + ', ' + '\n' +
               document.getElementById('<%= ddlPermState.ClientID %>').options[document.getElementById('<%= ddlPermState.ClientID %>').selectedIndex].text;
                    document.getElementById('<%= ddlPermCountry.ClientID %>').options[document.getElementById('<%= ddlPermCountry.ClientID %>').selectedIndex].text;

                }
            }
            else {
                document.getElementById('<%= txtGuardianAddress.ClientID %>').value = '';
            }
        }




        function validateAlphabet(txt) {
            var expAlphabet = /^[A-Za-z]+$/;
            if (txt.value.search(expAlphabet) == -1) {
                txt.value = txt.value.substring(0, (txt.value.length) - 1);
                txt.focus();
                alert('Only Alphabets Allowed');
                return false;
            }
            else
                return true;

        }
        function validateNumeric(txt) {
            if (isNaN(txt.value)) {
                txt.value = '';
                alert('Only Numeric Characters Allowed!');
                txt.focus();
                return;
            }
        }

        function validateAlphaNumeric(txt) {
            var expAN = /[$\\@\\\#%\^\&\*\(\)\[\]\+\_popup\{\}|`\~\=\|]/;
            var strPass = txt.value;
            var strLength = strPass.length;
            var lchar = txt.value.charAt((strLength) - 1);

            if (lchar.search(expAN) != -1) {
                txt.value(txt.value.substring(0, (strLength) - 1));
                txt.focus();
                alert('Only Alpha-Numeric Characters Allowed!');
            }
            return true;
        }

        function LoadImage() {
            document.getElementById("ctl00_ContentPlaceHolder1_imgPhoto").src = document.getElementById("ctl00_ContentPlaceHolder1_fuPhotoUpload").value;
            document.getElementById("ctl00_ContentPlaceHolder1_imgPhoto").height = '96px';
            document.getElementById("ctl00_ContentPlaceHolder1_imgPhoto").width = '96px';
        }

        function conver_uppercase(text) {
            text.value = text.value.toUpperCase();
        }

    </script>

    <script type="text/javascript">
        function alertmessage(commaSeperatedString) {
            var parts = commaSeperatedString.split(',');
            var errorMessage = parts.join('\n');
            alert(errorMessage);
        }
    </script>

    <div id="divMsg" runat="server"></div>
</asp:Content>
