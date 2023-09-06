<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="GuestRoomAllotment.aspx.cs" Inherits="HOSTEL_GuestRoomAllotment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updRoomAllot"
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

    <asp:UpdatePanel ID="updRoomAllot" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">GUEST ROOM ALLOTMENT</h3>
                        </div>

                        <div class="box-body">
                            <div id="divGuestInfo" runat="server" visible="false">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="sub-heading">
                                                <h5>Guest Information</h5>
                                            </div>
                                        </div>

                                        <div class="col-lg-6 col-md-6 col-12">
                                            <ul class="list-group list-group-unbordered">
                                                <li class="list-group-item"><b>Name :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblGuestName" Font-Bold="true" runat="server" />
                                                    </a>
                                                </li>
                                                <li class="list-group-item"><b>Address :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblGuestAdd" Font-Bold="true" runat="server" />
                                                    </a>
                                                </li>
                                            </ul>
                                        </div>

                                        <div class="col-lg-6 col-md-6 col-12">
                                            <ul class="list-group list-group-unbordered">
                                                <li class="list-group-item"><b>Contact No. :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblGuestContact" Font-Bold="true" runat="server" />
                                                    </a>
                                                </li>
                                                <li class="list-group-item"><b>Purpose :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblGuestPurpose" Font-Bold="true" runat="server" />
                                                    </a>
                                                </li>
                                            </ul>
                                        </div>

                                    </div>
                                </div>
                            </div>

                            <div id="divGuestSearch" runat="server" class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divGuestDdl" runat="server">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Select Guest</label>
                                        </div>
                                        <asp:DropDownList ID="ddlGuestList" runat="server" AppendDataBoundItems="True" AutoPostBack="True" TabIndex="1" OnSelectedIndexChanged="ddlGuestList_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true">
                                        </asp:DropDownList>

                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divStudentSelect" runat="server">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label id="lblStuSelect" runat="server">Select Student</label>
                                        </div>

                                        <%-- <asp:RadioButtonList ID="rdoSerachStudent" runat="server">
                                            <asp:ListItem>Search Student</asp:ListItem>
                                        </asp:RadioButtonList>--%>
                                        <asp:CheckBox runat="server" ID="chkSearchStudent" OnCheckedChanged="chkSearchStudent_CheckedChanged" AutoPostBack="true" />
                                    </div>

                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="btnShowGuest" runat="server" Text="Show Information" OnClick="btnShowGuest_Click" TabIndex="2" ValidationGroup="search" Visible="False" CssClass="btn btn-primary" />
                                    </div>
                                </div>
                            </div>


                            <div id="divStudSearch" runat="server" class="col-12" visible="false">
                                <div class="row">

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divSearchCriteria" runat="server">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Search Criteria</label>
                                        </div>

                                        <%--onchange=" return ddlSearch_change();"--%>
                                        <asp:DropDownList runat="server" class="form-control" ID="ddlSearch" AutoPostBack="true" AppendDataBoundItems="true" data-select2-enable="true" OnSelectedIndexChanged="ddlSearch_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>

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
                                <div class="col-12 btn-footer" id="divBtnSearchRegion" runat="server">
                                    <%-- OnClientClick="return submitPopup(this.name);"--%>
                                    <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" CssClass="btn btn-primary" OnClientClick="return submitPopup(this.name);" />

                                    <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="btn btn-warning" OnClick="btnClose_Click" OnClientClick="return CloseSearchBox(this.name)" data-dismiss="modal" />
                                </div>
                                <div class="col-12 btn-footer">
                                    <asp:Label ID="lblNoRecords" runat="server" SkinID="lblmsg" />
                                </div>

                                <div class="col-12">
                                    <asp:Panel ID="Panel3" runat="server">
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
                                                    </asp:Panel>
                                                </div>

                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <asp:LinkButton ID="lnkId" runat="server" Text='<%# Eval("Name") %>' CommandArgument='<%# Eval("IDNo") %>' OnClick="lnkId_Click"></asp:LinkButton>
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

                              <%-- STUDENT SECTION BY SONALI ON 20/01/2023  START--%>

                                <div class="col-12" id="div_Studentdetail" runat="server" visible="false">

                                    <div class="row mb-3" id="divStudInfo" runat="server">
                                        <div class="col-lg-6 col-md-6 col-12">
                                            <ul class="list-group list-group-unbordered">
                                                <li class="list-group-item"><b>Student Name :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblStudName" runat="server" Font-Bold="True"></asp:Label>
                                                    </a>
                                                </li>
                                                <li class="list-group-item"><b>
                                                    <asp:Label ID="lblDYRRNo" runat="server" Font-Bold="true"></asp:Label>
                                                    :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblRegno" runat="server" Font-Bold="True"></asp:Label>
                                                    </a>
                                                </li>
                                                <li class="list-group-item d-none"><b>Roll No :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblStudRollNo" runat="server" Font-Bold="True"></asp:Label></a>
                                                </li>
                                                <li class="list-group-item"><b>
                                                    <asp:Label ID="lblDYtxtSchoolname" runat="server" Font-Bold="true"></asp:Label>
                                                    :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblStudClg" runat="server" Font-Bold="True"></asp:Label></a>
                                                </li>
                                            </ul>
                                        </div>
                                        <div class="col-lg-6 col-md-6 col-12">
                                            <ul class="list-group list-group-unbordered">
                                                <li class="list-group-item"><b>
                                                    <asp:Label ID="lblDYtxtDegree" runat="server" Font-Bold="true"></asp:Label>
                                                    :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblStudDegree" runat="server" Font-Bold="True"></asp:Label>
                                                    </a>
                                                </li>
                                                <li class="list-group-item"><b>
                                                    <asp:Label ID="lblDYtxtBranch" runat="server" Font-Bold="true"></asp:Label>
                                                    :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblStudBranch" runat="server" Font-Bold="True"></asp:Label></a>
                                                </li>
                                                <li class="list-group-item"><b>Mobile No. :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblMobileNo" runat="server" Font-Bold="True"></asp:Label></a>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>

                                    <div class="row mb-3" id="divStudRoomAllotRegion" runat="server">
                                        <div class="col-12">
                                            <div class="sub-heading">
                                                <h5>Student Room Allotment</h5>
                                            </div>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Resident Type </label>
                                            </div>
                                            <asp:DropDownList ID="ddlStudResidentType" runat="server"
                                                AppendDataBoundItems="True" Enabled="true" CssClass="form-control" data-select2-enable="true" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlStudResidentType"
                                                Display="None" ErrorMessage="Please select resident type." ValidationGroup="studsubmit" SetFocusOnError="True" InitialValue="0" />
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Hostel Session </label>
                                            </div>
                                            <asp:DropDownList ID="ddlStuSession" runat="server" Enabled="true" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlStuSession" Display="None"
                                                ErrorMessage="Please select hostel session." ValidationGroup="studsubmit" SetFocusOnError="True" InitialValue="0" />
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Date of Allotment </label>
                                            </div>
                                            <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="meeAllotmentDate"
                                                ControlToValidate="txtStudDateAllot" IsValidEmpty="False" EmptyValueMessage="Enter Date of Allotment"
                                                InvalidValueMessage="Allotment date is invalid" InvalidValueBlurredMessage="*" Display="none" ValidationGroup="studsubmit" />
                                            </label>
                                            <div class="input-group">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar"></i>
                                                </div>
                                                <asp:TextBox ID="txtStudDateAllot" runat="server" TabIndex="2" ToolTip="Enter Date of Allotment" AutoPostBack="true" CssClass="form-control" />
                                         
                                                <%--<asp:Image ID="imgCalAllotmentDate" runat="server" src="../images/calendar.png" Style="cursor: hand" />--%>
                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                                    TargetControlID="txtStudDateAllot" PopupButtonID="imgCalAllotmentDate" />
                                                <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtStudDateAllot"
                                                    Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" ErrorTooltipEnabled="false"
                                                    OnInvalidCssClass="errordate" />

                                            </div>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>From Date </label>
                                            </div>
                                            <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator3" runat="server" ControlExtender="meeAllotmentDate"
                                                ControlToValidate="txtStudFromDate" IsValidEmpty="False" EmptyValueMessage="Enter From Date"
                                                InvalidValueMessage="From date is invalid" InvalidValueBlurredMessage="*" Display="none" ValidationGroup="studsubmit" />
                                            </label>
                                            <div class="input-group">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar"></i>
                                                </div>
                                                <asp:TextBox ID="txtStudFromDate" runat="server" TabIndex="2" ToolTip="Enter From Date" AutoPostBack="true"
                                                     CssClass="form-control" OnTextChanged="txtStudFromDate_TextChanged" />
                                            
                                                <%--<asp:Image ID="imgCalAllotmentDate" runat="server" src="../images/calendar.png" Style="cursor: hand" />--%>
                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy"
                                                    TargetControlID="txtStudFromDate" PopupButtonID="imgCalAllotmentDate" />
                                                <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txtStudFromDate"
                                                    Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" ErrorTooltipEnabled="false"
                                                    OnInvalidCssClass="errordate" />

                                            </div>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>To Date </label>
                                            </div>
                                            <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator4" runat="server" ControlExtender="meeAllotmentDate"
                                                ControlToValidate="txtStudToDate" IsValidEmpty="False" EmptyValueMessage="Enter To Date"
                                                InvalidValueMessage="To date is invalid" InvalidValueBlurredMessage="*" Display="none" ValidationGroup="studsubmit" />
                                            </label>
                                            <div class="input-group">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar"></i>
                                                </div>
                                                <asp:TextBox ID="txtStudToDate" runat="server" TabIndex="2" ToolTip="Enter To Date" AutoPostBack="true"
                                                     CssClass="form-control" OnTextChanged="txtStudToDate_TextChanged" />        
                                                <%--<asp:Image ID="imgCalAllotmentDate" runat="server" src="../images/calendar.png" Style="cursor: hand" />--%>
                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy"
                                                    TargetControlID="txtStudToDate" PopupButtonID="imgCalAllotmentDate" />
                                                <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" TargetControlID="txtStudToDate"
                                                    Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" ErrorTooltipEnabled="false"
                                                    OnInvalidCssClass="errordate" />
                                                
                                            </div>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Hostel </label>
                                            </div>
                                            <asp:DropDownList ID="ddlStudHostel" runat="server" TabIndex="3" AppendDataBoundItems="true" CssClass="form-control"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlStudHostel_SelectedIndexChanged" data-select2-enable="true" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlStudHostel"
                                                Display="None" ErrorMessage="Please select Hostel." ValidationGroup="studsubmit"
                                                SetFocusOnError="True" InitialValue="0" />
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Room Type </label>
                                            </div>
                                            <asp:DropDownList ID="ddlStudRoomType" runat="server" TabIndex="3" AppendDataBoundItems="true" CssClass="form-control"
                                                AutoPostBack="true" data-select2-enable="true" OnSelectedIndexChanged="ddlStudRoomType_SelectedIndexChanged" >
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlStudRoomType"
                                                Display="None" ErrorMessage="Please select Room Type." ValidationGroup="studsubmit"
                                                SetFocusOnError="True" InitialValue="0" />
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Block </label>
                                            </div>
                                            <asp:DropDownList ID="ddlStudBlock" runat="server" TabIndex="4" AppendDataBoundItems="true" CssClass="form-control"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlStudBlock_SelectedIndexChanged" Enabled="false" data-select2-enable="true" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlStudBlock"
                                                Display="None" ErrorMessage="Please select Block." ValidationGroup="studsubmit"
                                                SetFocusOnError="True" InitialValue="0" />
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Floor </label>
                                            </div>
                                            <asp:DropDownList ID="ddlStudFloor" runat="server" TabIndex="5" AppendDataBoundItems="True" CssClass="form-control"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlStudFloor_SelectedIndexChanged" Enabled="false" data-select2-enable="true" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlStudFloor"
                                                Display="None" ErrorMessage="Please select Floor." ValidationGroup="studsubmit"
                                                SetFocusOnError="True" InitialValue="0" />
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Available Room </label>
                                            </div>
                                            <asp:DropDownList ID="ddlStudAvailRoom" runat="server" TabIndex="6" AppendDataBoundItems="True" Enabled="false" AutoPostBack="true"
                                                 CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlStudAvailRoom_SelectedIndexChanged" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlStudAvailRoom" 
                                                Display="None" ErrorMessage="Please select room to allot." ValidationGroup="studsubmit"
                                                SetFocusOnError="True" InitialValue="0" />
                                        </div>

                                        <div class="form-group col-lg-2 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Stay No. of Days</label>
                                        </div>
                                        <asp:TextBox ID="txtStudStayDays" runat="server" TabIndex="4" MaxLength="8" CssClass="form-control" Enabled="false" Text="0"
                                             />
                                              <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" runat="server" FilterType="Numbers, Custom"
                                               ValidChars=".," TargetControlID="txtStudStayDays" />
                                     <%--   <asp:RequiredFieldValidator ID="rfvtxtCharge" runat="server" ControlToValidate="txtStudStayDays"
                                            Display="None" ErrorMessage="Please Enter Charges" ValidationGroup="Submit"
                                            SetFocusOnError="True" />--%>
                                    </div>
                                        
                                        <div class="form-group col-lg-2 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Amount</label>
                                        </div>
                                        <asp:TextBox ID="txtTotalAmount" runat="server" TabIndex="6" MaxLength="8" CssClass="form-control" Enabled="false" Text="0" />
                                              <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" runat="server" FilterType="Numbers, Custom"
                                               ValidChars=".," TargetControlID="txtTotalAmount" />
                                            <asp:RequiredFieldValidator ID="RFVTotalAmount" runat="server" ControlToValidate="ddlStudRoomType"
                                                Display="None" ErrorMessage="Please Enter Total Amount." ValidationGroup="studsubmit"
                                                SetFocusOnError="True" InitialValue="0" />
                                       </div>
                                        <div class="form-group col-lg-2 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label></label>
                                        </div>
                                        <asp:CheckBox ID="chkMonthlyAmount" runat="server" TabIndex="5"  CssClass="" Text="&nbsp;&nbsp;Monthly" OnCheckedChanged="chkMonthlyAmount_CheckedChanged" AutoPostBack="true" />
                                       </div>
                                     </div>

                                    <div class="col-12 btn-footer mt-3" id="divStudRegionButton" runat="server">
                                    <asp:Button ID="btnStudAllotRoom" runat="server" Text="Allot Room" ValidationGroup="studsubmit"
                                       TabIndex="7" CssClass="btn btn-primary" Visible="true" OnClick="btnStudAllotRoom_Click" />
                                    <asp:Button ID="btnStudReport" runat="server" Enabled="false" Text="Allot Report" TabIndex="8"
                                         CssClass="btn btn-info" OnClick="btnStudReport_Click"/>

                                   <asp:Button ID="btnStudBack" runat="server" Text="Back" CausesValidation="false"
                                       TabIndex="9" CssClass="btn btn-info" OnClick="btnStudBack_Click" Visible="false" />

                                    <asp:Button ID="BtnStudCancel" runat="server" Text="Cancel" CausesValidation="false"
                                       TabIndex="9" CssClass="btn btn-warning" OnClick="BtnStudCancel_Click" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="true"
                                        ShowSummary="false" ValidationGroup="studsubmit" />

                                   </div>

                                    <div class="col-12">
                                    <asp:Panel ID="pnlStudRoomAllot" runat="server">
                                        <asp:ListView ID="lvStudRoomAllot" runat="server" Visible="false">
                                            <LayoutTemplate>
                                                <div id="demo-grid">
                                                    <div class="sub-heading">
                                                        <h5>Room Allotment Details  </h5>
                                                    </div>
                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th>Edit</th>
                                                                <th>Student Name
                                                                </th>
                                                                <th>Reg. No.</th>
                                                                <th>Hostel
                                                                </th>
                                                                <th>Block
                                                                </th>
                                                                <th>Floor
                                                                </th>
                                                                <th>Room
                                                                </th>
                                                                <th>Allotment Date
                                                                </th>
                                                                <th>Stay Days</th>
                                                            <th>Charges Applied</th>
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
                                        <asp:ImageButton ID="btnStudEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("RESIDENT_NO") %>'
                                            AlternateText="Edit Record" ToolTip="Edit Record" TabIndex="7" OnClick="btnStudEdit_Click"/>&nbsp;
                                    
                                    </td>
                                                    <td>
                                                        <%# Eval("STUDNAME")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("REGNO")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("HOSTEL_NAME")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("BLOCK_NAME")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("FLOOR_NAME")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("ROOM_NAME")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("ALLOTMENT_DATE", "{0:d}")%>
                                                    </td>
                                                       <td>
                                                        <%# Eval("STAY_DAYS")%>
                                                    </td>
                                                      <td>
                                                        <%# Eval("DEMAND_CHARGE")%>
                                                    </td>
                                                    
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </asp:Panel>
                                </div>

                                </div>

                                <%--END--%>
                            </div>


                            <div id="divRoomAllotment" runat="server" visible="false">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="sub-heading">
                                                <h5>Room Allotment</h5>
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Resident Type </label>
                                            </div>
                                            <asp:DropDownList ID="ddlResidentType" runat="server"
                                                AppendDataBoundItems="True" Enabled="true" CssClass="form-control" data-select2-enable="true" />
                                            <asp:RequiredFieldValidator ID="valResidentType" runat="server" ControlToValidate="ddlResidentType"
                                                Display="None" ErrorMessage="Please select resident type." ValidationGroup="submit" SetFocusOnError="True" InitialValue="0" />
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Hostel Session </label>
                                            </div>
                                            <asp:DropDownList ID="ddlSession" runat="server" Enabled="true" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true" />
                                            <asp:RequiredFieldValidator ID="valSession" runat="server" ControlToValidate="ddlSession" Display="None"
                                                ErrorMessage="Please select hostel session." ValidationGroup="submit" SetFocusOnError="True" InitialValue="0" />
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Date of Allotment </label>
                                            </div>
                                            <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="meeAllotmentDate"
                                                ControlToValidate="txtAllotmentDate" IsValidEmpty="False" EmptyValueMessage="Enter Date of Allotment"
                                                InvalidValueMessage="Allotment date is invalid" InvalidValueBlurredMessage="*" Display="none" ValidationGroup="submit" />
                                            </label>
                                            <div class="input-group">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar"></i>
                                                </div>
                                                <asp:TextBox ID="txtAllotmentDate" runat="server" TabIndex="2" ToolTip="Enter Date of Allotment" AutoPostBack="true" CssClass="form-control" />
                                                <%-- OnTextChanged="txtAllotmentDate_TextChanged"--%>
                                                <%--<asp:Image ID="imgCalAllotmentDate" runat="server" src="../images/calendar.png" Style="cursor: hand" />--%>
                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                                    TargetControlID="txtAllotmentDate" PopupButtonID="imgCalAllotmentDate" />
                                                <ajaxToolKit:MaskedEditExtender ID="meeAllotmentDate" runat="server" TargetControlID="txtAllotmentDate"
                                                    Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" ErrorTooltipEnabled="false"
                                                    OnInvalidCssClass="errordate" />

                                            </div>
                                        </div>


                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Hostel </label>
                                            </div>
                                            <asp:DropDownList ID="ddlHostel" runat="server" TabIndex="3" AppendDataBoundItems="true" CssClass="form-control"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlHostel_SelectedIndexChanged" data-select2-enable="true" />
                                            <asp:RequiredFieldValidator ID="rfvHostel" runat="server" ControlToValidate="ddlHostel"
                                                Display="None" ErrorMessage="Please select Hostel." ValidationGroup="submit"
                                                SetFocusOnError="True" InitialValue="0" />
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Block </label>
                                            </div>
                                            <asp:DropDownList ID="ddlBlock" runat="server" TabIndex="4" AppendDataBoundItems="true" CssClass="form-control"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlBlock_SelectedIndexChanged" Enabled="false" data-select2-enable="true" />
                                            <asp:RequiredFieldValidator ID="rfvBlock" runat="server" ControlToValidate="ddlBlock"
                                                Display="None" ErrorMessage="Please select Block." ValidationGroup="submit"
                                                SetFocusOnError="True" InitialValue="0" />
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Floor </label>
                                            </div>
                                            <asp:DropDownList ID="ddlFloor" runat="server" TabIndex="5" AppendDataBoundItems="True" CssClass="form-control"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlFloor_SelectedIndexChanged" Enabled="false" data-select2-enable="true" />
                                            <asp:RequiredFieldValidator ID="rfvFloor" runat="server" ControlToValidate="ddlFloor"
                                                Display="None" ErrorMessage="Please select Floor." ValidationGroup="submit"
                                                SetFocusOnError="True" InitialValue="0" />
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Available Room </label>
                                            </div>
                                            <asp:DropDownList ID="ddlRoom" runat="server" TabIndex="6" AppendDataBoundItems="True" Enabled="false" CssClass="form-control" data-select2-enable="true" />
                                            <asp:RequiredFieldValidator ID="valRoom" runat="server" ControlToValidate="ddlRoom"
                                                Display="None" ErrorMessage="Please select room to allot." ValidationGroup="submit"
                                                SetFocusOnError="True" InitialValue="0" />
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div id="divButtonEvent" runat="server" visible="false">

                                <div class="col-12 btn-footer mt-3">
                                    <asp:Button ID="btnAllotRoom" runat="server" Text="Allot Room" ValidationGroup="submit"
                                        OnClick="btnAllotRoom_Click" TabIndex="7" CssClass="btn btn-primary" Visible="true" />
                                    <asp:Button ID="btnReport" runat="server" Enabled="false" Text="Allot Report" TabIndex="8" CssClass="btn btn-info" OnClick="btnReport_Click" />
                                    <%--OnClick="btnReport_Click"--%>
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                                        OnClick="btnCancel_Click" TabIndex="9" CssClass="btn btn-warning" />
                                    <asp:ValidationSummary ID="valSummary" runat="server" DisplayMode="List" ShowMessageBox="true"
                                        ShowSummary="false" ValidationGroup="submit" />

                                </div>

                                <div class="col-12">
                                    <asp:Panel ID="Panel1" runat="server">
                                        <asp:ListView ID="lvAllotmentDetails" runat="server" Visible="false">
                                            <LayoutTemplate>
                                                <div id="demo-grid">
                                                    <div class="sub-heading">
                                                        <h5>Room Allotment Details  </h5>
                                                    </div>
                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th>Name
                                                                </th>
                                                                <th>Hostel
                                                                </th>
                                                                <th>Block
                                                                </th>
                                                                <th>Floor
                                                                </th>
                                                                <th>Room
                                                                </th>
                                                                <th>Allotment Date
                                                                </th>
                                                                <th>ContactNo
                                                                </th>
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
                                                        <%# Eval("GUEST_NAME")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("HOSTEL_NAME")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("BLOCK_NAME")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("FLOOR_NAME")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("ROOM_NAME")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("ALLOTMENT_DATE", "{0:d}")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("CONTACT_NO")%>
                                                    </td>
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

            <div id="divMsg" runat="server">
            </div>

        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnReport" />
             <asp:PostBackTrigger ControlID="btnStudReport" />

        </Triggers>
    </asp:UpdatePanel>

    <%--Search Box Script Start--%>
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
    <%--Search Box Script End--%>
</asp:Content>
