<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="OnlinePayment.aspx.cs" Inherits="ACADEMIC_OnlinePayment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        #ctl00_ContentPlaceHolder1_Panel3 .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>
    <div>
        <%--   <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updFee"
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
        </asp:UpdateProgress>--%>
    </div>

    <asp:UpdatePanel runat="server" ID="updFee">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title" id="spanPageHeader" runat="server"><%--ONLINE PAYMENT--%>
                                <asp:Label ID="lblDynamicPageTitle" runat="server" Font-Bold="true"></asp:Label></h3>
                        </div>

                        <div class="box-body">
                            <%--Search Pannel Start by Swapnil --%>
                            <div id="myModal2" role="dialog" runat="server">
                                <div>
                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updEdit"
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

                                                <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="btn btn-warning" OnClick="btnClose_Click" OnClientClick="return CloseSearchBox(this.name)" data-dismiss="modal" />
                                            </div>
                                            <div class="col-12 btn-footer">
                                                <asp:Label ID="lblNoRecords" runat="server" SkinID="lblmsg" />
                                            </div>
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

                                </asp:UpdatePanel>
                            </div>
                            <%--Search Pannel End--%>

                            <div class="col-12" id="divStudentSearch" runat="server">
                                <div class="row">
                                    <%-- <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divEnrollment">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Registration No.</label>
                                        </div>
                                        <asp:TextBox ID="txtEnrollmentNo" runat="server" placeholder="Enter Enrollment No." TabIndex="1" ValidationGroup="submit" CssClass="form-control" />
                                        <asp:RequiredFieldValidator ID="rfvtxtEnrollmentNo" runat="server" ControlToValidate="txtEnrollmentNo"
                                            Display="None" ErrorMessage="Please Enter Enrollment No." InitialValue="" SetFocusOnError="true"
                                            ValidationGroup="Payment" />
                                    </div>--%>
                                </div>
                            </div>

                            <div id="divDirectPayment" runat="server">
                                <div class="col-12" id="div_Studentdetail" runat="server" visible="false">
                                    <div class="row mb-3">
                                        <div class="col-lg-8 col-md-6 col-12">
                                            <div class="row">
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
                                                        <li class="list-group-item"><b>E-Mail ID :</b>
                                                            <a class="sub-label">
                                                                <asp:Label ID="lblMailId" runat="server" Font-Bold="True"></asp:Label></a>
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
                                                        <li class="list-group-item"><b>Total Amount :</b>
                                                            <a class="sub-label">
                                                                <asp:Label ID="lblAmount" runat="server" Font-Bold="True" Style="color: green"></asp:Label>
                                                                <asp:HiddenField ID="hdfCity" runat="server" />
                                                                <asp:HiddenField ID="hdfState" runat="server" />
                                                                <asp:HiddenField ID="hdfZipCode" runat="server" />
                                                                <asp:HiddenField ID="hdfIdno" runat="server" />
                                                                <asp:HiddenField ID="hdfAmount" runat="server" />
                                                                <asp:HiddenField ID="hdfName" runat="server" />
                                                                <asp:HiddenField ID="hdfEmailId" runat="server" />
                                                                <asp:HiddenField ID="hdfMobileNo" runat="server" />
                                                                <asp:HiddenField ID="hdfSessioNo" runat="server" />
                                                            </a>
                                                        </li>

                                                    </ul>
                                                </div>

                                            </div>
                                            <div class="row mb-3 mt-4">
                                                <div class="form-group col-lg-6 col-md-6 col-12" runat="server" id="divReceiptType">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Receipt Type</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlReceiptType" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="2" OnSelectedIndexChanged="ddlReceiptType_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvddlReceiptType" runat="server" ControlToValidate="ddlReceiptType"
                                                        Display="None" ErrorMessage="Please Select Receipt Type" InitialValue="0" SetFocusOnError="true"
                                                        ValidationGroup="Payment" />
                                                </div>

                                                <div class="form-group col-lg-6 col-md-6 col-12" runat="server" id="divStudSemester">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <%--<label>Semester</label>--%>
                                                        <asp:Label ID="lblDYddlSemester_Tab" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlSemester" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="3" OnSelectedIndexChanged="ddlSemester_OnSelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvddlSemester" runat="server" ErrorMessage="Please Select Semester" SetFocusOnError="true"
                                                        Display="None" ControlToValidate="ddlSemester" InitialValue="0" ValidationGroup="Payment"></asp:RequiredFieldValidator>
                                                </div>

                                                <div class="form-group col-lg-6 col-md-6 col-12" runat="server" id="divHostelTransport" visible="false" style="margin-top: 30px; font-size: 14px;">
                                                    <asp:LinkButton ID="btnHostel" Text="Click Here to Select Hostel" runat="server" ForeColor="Green" OnClick="btnHostel_Click"></asp:LinkButton>
                                                    <div style="font-size: 12px; color: red; margin-top: -20px; margin-left: 180px;">
                                                        <asp:Label ID="Label1" Text="(Optional)" runat="server"></asp:Label>
                                                    </div>
                                                    <br />
                                                    <div style="font-size: 12px; color: red; margin-top: -15px;">
                                                        <asp:Label ID="lblhostel" Text="(Selecting Hostel Will Add up to Your Payable Fees)" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                                <%--<div  class="form-group col-lg-1 col-md-6 col-12">
                                         <asp:Label ID="Label2" Text="(Optional)" runat="server" ></asp:Label>
                                         </div>--%>
                                                <div class="form-group col-lg-6 col-md-6 col-12" runat="server" id="divhottrsansport" visible="false" style="display: none">
                                                    <asp:RadioButtonList ID="rblhottransport" runat="server" AppendDataBoundItems="true" class="radiobuttonlist" AutoPostBack="true"
                                                        RepeatDirection="Horizontal" Width="100%" OnSelectedIndexChanged="rblhottransport_SelectedIndexChanged">
                                                        <asp:ListItem Value="1"><span style="font-size: 13px;font-weight:bold"> Choose Hostel </span></asp:ListItem>
                                                        <%--<asp:ListItem Value="2"><span style="font-size: 13px;font-weight:bold"> Choose Transport</span></asp:ListItem>--%>
                                                    </asp:RadioButtonList>
                                                    <%-- <asp:RadioButton ID="rdbhostel" runat="server" Text="Hostel" GroupName="hottransp" OnCheckedChanged="rdbhostel_CheckedChanged"/>
                                            <asp:RadioButton ID="rdbtransport" runat="server" Text="Transport" GroupName="hottransp" />--%>
                                                </div>
                                                <div class="form-group col-lg-6 col-md-6 col-12" runat="server" id="divhosteltype" visible="false">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Hostel Type</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlhosteltype" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="2" OnSelectedIndexChanged="ddlhosteltype_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlhosteltype"
                                                        Display="None" ErrorMessage="Please Select Hostel Type" InitialValue="0" SetFocusOnError="true"
                                                        ValidationGroup="Payment" />

                                                </div>


                                            </div>
                                        </div>

                                        <div class="col-lg-4 col-md-6 col-12">
                                            <asp:ListView ID="lvfeehead" runat="server">
                                                <LayoutTemplate>
                                                    <div id="divlvFeeItems">
                                                        <div class="sub-heading">
                                                            <h5>Applicable Fees</h5>
                                                        </div>
                                                        <%--Available Fee Items--%>
                                                        <div class="table-responsive" style="height: 200px; overflow: auto; border-top: 1px solid #e5e5e5;">
                                                            <table id="tblFeeItems" runat="server" class="table table-striped table-bordered nowrap " style="width: 100%">
                                                                <thead class="bg-light-blue" style="position: sticky; z-index: 1; top: 0; background: #fff !important;">
                                                                    <tr>
                                                                        <th>Sr. No. </th>
                                                                        <th>Fee Heads </th>
                                                                        <th>Applicable Amount </th>
                                                                        <th>Paid Amount </th>
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
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblFeeHeadSrNo" runat="server" Text='<%# Eval("SRNO") %>' Visible="false" />
                                                            <%# Container.DataItemIndex + 1%></td>
                                                        <td><%# Eval("FEE_LONGNAME")%>
                                                            <asp:HiddenField ID="hdnfld_FEE_LONGNAME" runat="server" Value='<%# Eval("FEE_LONGNAME")%>' />
                                                            <asp:HiddenField ID="hfFee_hd" runat="server" Value='<%# Eval("FEE_HEAD")%>' />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblTotaldemandamt" runat="server" Text='<%# Eval("total_demand") %>' />
                                                            <%-- <%# Eval("total_demand")%>--%></td>
                                                        <td>
                                                            <asp:Label ID="lblTotaldcramt" runat="server" Text='<%# Eval("total_dcr") %>' />
                                                            <%-- <%# Eval("total_dcr")%>--%></td>

                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </div>

                                    </div>


                                    <%--     <div class="col-md-12">
                                        <div class="row">
                                            <asp:Panel ID="pnlfeedetails" runat="server">
                                                <div class="form-group col-lg-12 col-md-12 col-12 pl-lg-0" id="feedetails" runat="server">
                                                    <asp:ListView ID="lvfeehead" runat="server">
                                                        <LayoutTemplate>
                                                            <div class="sub-heading">
                                                                <h5>Fees Details</h5>
                                                            </div>
                                                            <table class="table table-striped table-bordered nowrap mb-0" style="width: 100%" id="tblHead">
                                                                <thead class="bg-light-blue">
                                                                    <tr class="header">
                                                                        <th>Sr No.</th>
                                                                        <th>Fees Head
                                                                        </th>
                                                                        <th style="text-align: right">Amount
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
                                                                    <asp:Label ID="lblsrnumb" runat="server" Text='<%# Container.DataItemIndex+1%>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("FEE_LONGNAME") %> 
                                                                       
                                                    
                                                                </td>
                                                                <td style="text-align: right">
                                                                    <%# Eval("SEMESTER") %>
                                                                            
                                                                </td>

                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                    <div id="divamount" runat="server">
                                                        <table id="tblamt" class="table table-striped table-bordered nowrap" style="width: 70%">
                                                            <tr style="background-color: #eee !important;">
                                                                <th style="text-align: center">
                                                                    <asp:Label ID="lbltotals" runat="server" Style="font-weight: bold;" Text="Total :"></asp:Label>
                                                                </th>
                                                                <td style="text-align: right">
                                                                    <asp:Label ID="lbltotalfeehead" runat="server" Enabled="false" Style="font-weight: bold; text-align: right;"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                            </asp:Panel>

                                        </div>
                                    </div>--%>

                                    <div class="col-12">
                                        <%--<div class="sub-heading"><h5>Fee Items</h5></div>--%>
                                    </div>

                                    <div class="col-12 mb-4" id="divFeeItems" runat="server" visible="false">
                                        <asp:ListView ID="lvFeeItems" OnItemDataBound="lvFeeItems_ItemDataBound" runat="server">
                                            <LayoutTemplate>
                                                <div id="divlvFeeItems">
                                                    <div class="sub-heading">
                                                        <h5>Available Fees</h5>
                                                    </div>
                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="tblFeeItems">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th hidden>Select
                                                                </th>
                                                                <th>Fee Heads
                                                                </th>
                                                                <th>Currency
                                                                </th>
                                                                <th>Amount
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
                                                    <td hidden>
                                                        <asp:CheckBox ID="chkFee" runat="server" ToolTip='<%# Eval("SRNO") %>' onclick="UpdateTotalRefundAmt();" />
                                                    </td>
                                                    <td>
                                                        <%# Eval("FEE_LONGNAME")%>
                                                        <asp:HiddenField ID="hdnfld_FEE_LONGNAME" runat="server" Value='<%# Eval("FEE_LONGNAME")%>' />
                                                    </td>
                                                    <td>
                                                        <%# Eval("CURRENCY")%>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtFeeItemAmount" onkeyup="IsNumeric(this);" Enabled="false"
                                                            Text=' <%#(Convert.ToDouble(Eval("AMOUNT"))<0 ? 0.00 : Eval("AMOUNT"))%>' Style="text-align: right" ReadOnly="true" runat="server" CssClass="form-control"
                                                            TabIndex="15" onchange="checkHack(this);" />
                                                        <asp:HiddenField ID="hidFeeItemAmount" runat="server" Visible="false" Value='<%#(Convert.ToDouble(Eval("AMOUNT"))<0 ? 0.00 : Eval("AMOUNT"))%>' />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                        <div class="label-dynamic">
                                            <label>Total Amount</label>
                                        </div>
                                        <asp:TextBox ID="txtTotalCashAmt" Text="0" CssClass="data_label form-control" runat="server" onkeydown="javascript:return false;" />
                                        <asp:HiddenField ID="hdnTotalCashAmt" runat="server" />
                                    </div>

                                    <div id="dvFeeHeadAmount" class="col-12" style="display: none">
                                        <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto">
                                            <div class="col-12">
                                                <asp:GridView ID="gvFeeAmount" runat="server" AutoGenerateColumns="false" ShowFooter="true" Width="100%">
                                                    <HeaderStyle CssClass="bg-light-blue" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sl.No" ItemStyle-HorizontalAlign="Center"
                                                            ItemStyle-Width="3%">
                                                            <ItemTemplate>
                                                                <%#Container.DataItemIndex + 1%>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="10%" />
                                                            <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="FEE_LONGNAME" HeaderStyle-HorizontalAlign="Center" HeaderText="Fees Head" ItemStyle-Width="60%" />
                                                        <asp:BoundField DataField="AMOUNTS" HeaderText="Amount" ItemStyle-Width="30" DataFormatString="{0:N2}"
                                                            ItemStyle-HorizontalAlign="Right" />
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </asp:Panel>
                                    </div>

                                    <div class="col-md-12">
                                        <asp:ListView ID="lvFeeCollectionModes" runat="server" Visible="false">
                                            <LayoutTemplate>
                                                <div id="divlvFeeCollectionModes">
                                                    <div class="sub-heading">
                                                        <h5>Available Modes of Fee Collection</h5>
                                                    </div>
                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th>Modes
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
                                                        <asp:HyperLink ID="hlkFeeCollection" runat="server" Text='<%# Eval("LINK_CAPTION") %>'
                                                            NavigateUrl='<%# Eval("LINK_URL") + "&RecTitle=" + ddlReceiptType.SelectedItem.Text + "&RecType=" + ddlReceiptType.SelectedValue %>' />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </div>
                                </div>

                                <div class="col-12 btn-footer" runat="server" id="divMSG" visible="false">
                                    <asp:Label ID="lblmsg" runat="server" Font-Bold="True" Style="color: green"></asp:Label>
                                </div>

                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnPayment" runat="server" Visible="false" Text="Payment" TabIndex="5" OnClick="btnPayment_Click" ValidationGroup="Payment" CssClass="btn btn-primary" />
                                    <asp:Button ID="btnReciept" runat="server" Visible="false" Text="Reciept" TabIndex="5" OnClick="btnReciept_Click" ValidationGroup="Payment" CssClass="btn btn-info" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" TabIndex="6" CssClass="btn btn-warning" />
                                    <asp:ValidationSummary ID="vsbtn" runat="server" ShowSummary="false" DisplayMode="List" ShowMessageBox="true" ValidationGroup="Payment" />
                                    <asp:HiddenField ID="hdnFeeItemCount" runat="server" />
                                </div>



                                <div class="col-12">


                                    <div id="divpaymentdetails" visible="false" runat="server">
                                        <div id="div3" runat="server">
                                            <asp:Panel ID="pnlpaymentdetails" runat="server">
                                                <div class="table-responsive">
                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="Table1">
                                                        <asp:Repeater ID="lvPaymentDetails" runat="server">
                                                            <HeaderTemplate>
                                                                <div class="sub-heading">
                                                                    <h5>Payment Details</h5>
                                                                </div>
                                                                <thead class="bg-light-blue">
                                                                    <tr>
                                                                        <th>Receipt Type
                                                                        </th>
                                                                        <th>Semester 
                                                                        </th>
                                                                        <th>Payable Fee / Applicable Fee
                                                                        </th>
                                                                        <th>Paid
                                                                        </th>
                                                                        <th>Balance
                                                                        </th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr id="itemPlaceholder" runat="server" />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <tr>

                                                                    <td>
                                                                        <%# Eval("RECIEPT_TYPE") %>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("SEMESTERNO") %>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("ADMFEE") %>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("PADMFEE") %>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("BADMFEE") %>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                </tbody>
                                                            </FooterTemplate>
                                                        </asp:Repeater>
                                                    </table>
                                                </div>
                                            </asp:Panel>
                                        </div>
                                    </div>



                                </div>

                                <br />

                                <div class="col-12">
                                    <div id="divPreviousReceipts" visible="false" runat="server">
                                        <div id="divHidPreviousReceipts" runat="server">
                                            <%# Eval("DD_BANK")%>
                                            <asp:Panel ID="Panel2" runat="server">
                                                <div class="table-responsive">
                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                        <asp:Repeater ID="lvPaidReceipts" runat="server">
                                                            <HeaderTemplate>
                                                                <div class="sub-heading">
                                                                    <h5>Previous Receipts Information</h5>
                                                                </div>
                                                                <thead class="bg-light-blue">
                                                                    <tr>
                                                                        <th>Print
                                                                        </th>
                                                                        <th>Receipt Type
                                                                        </th>
                                                                        <th>Receipt No
                                                                        </th>
                                                                        <th>Date
                                                                        </th>
                                                                        <th>Semester
                                                                        </th>
                                                                        <th>Pay Type
                                                                        </th>
                                                                        <th>Amount
                                                                        </th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr id="itemPlaceholder" runat="server" />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <%--  <tr class="item">--%>
                                                                    <td>
                                                                        <asp:ImageButton ID="btnPrintReceipt" runat="server" OnClick="btnPrintReceipt_Click"
                                                                            CommandArgument='<%# Eval("DCR_NO") %>' ImageUrl="~/Images/print.png" ToolTip='<%# Eval("RECIEPT_CODE")%>' CausesValidation="False" />
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("RECIEPT_TITLE") %>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("REC_NO") %>
                                                                    </td>
                                                                    <td>
                                                                        <%# (Eval("REC_DT").ToString() != string.Empty) ? ((DateTime)Eval("REC_DT")).ToShortDateString() : Eval("REC_DT") %>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("SEMESTERNAME") %>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("PAY_TYPE") %>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("TOTAL_AMT") %>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                </tbody>
                                                            </FooterTemplate>
                                                        </asp:Repeater>
                                                    </table>
                                                </div>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div id="divInstallmentPayment" class="box-footer" runat="server" visible="false">
                                <div class="row mb-3">
                                    <div class="col-lg-4 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Student Name :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblName" runat="server" Font-Bold="True"></asp:Label>
                                                </a>
                                            </li>
                                            <li class="list-group-item"><b>Roll No :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblRollNo" runat="server" Font-Bold="True"></asp:Label></a>
                                            </li>
                                            <li class="list-group-item"><b>School/Institute Name :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblCollegeName" runat="server" Font-Bold="True"></asp:Label></a>
                                            </li>
                                        </ul>
                                    </div>
                                    <div class="col-lg-4 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Degree Name :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblDegreeName" runat="server" Font-Bold="True"></asp:Label>
                                                </a>
                                            </li>
                                            <li class="list-group-item"><b>Programme/Branch Name :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblBranchName" runat="server" Font-Bold="True"></asp:Label></a>
                                            </li>
                                            <li class="list-group-item"><b>Mobile No. :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblMobile" runat="server" Font-Bold="True"></asp:Label></a>
                                            </li>
                                        </ul>
                                    </div>
                                    <div class="col-lg-4 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Total Amount :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblTotalAmount" runat="server" Font-Bold="True"></asp:Label>

                                                </a>
                                            </li>
                                            <li class="list-group-item"><b>E-Mail ID :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblEmailID" runat="server" Font-Bold="True"></asp:Label></a>
                                            </li>
                                            <li class="list-group-item"><b>Total Installment :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblTotalInstallment" runat="server" Font-Bold="True"></asp:Label></a>
                                            </li>
                                        </ul>
                                    </div>
                                </div>

                                <div class="col-12">
                                    <asp:Panel ID="pnlInstallment" runat="server" Visible="true">
                                        <asp:ListView ID="lvInstallment" runat="server" Visible="true">
                                            <LayoutTemplate>
                                                <div class="sub-heading">
                                                    <h5>Installment Fees Details</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="tblFees">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>Sr.No.</th>
                                                            <th>Installment No. 
                                                            </th>

                                                            <th>Due Date
                                                            </th>
                                                            <th>Installment Amount
                                                            </th>
                                                            <th>Status
                                                            </th>
                                                            <th style="text-align: center">Pay</th>
                                                            <th>Print Receipt</th>
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
                                                        <%# Container.DataItemIndex +1 %>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblInstallmentNo" runat="server" Text='<%# Eval("INSTALMENT_NO") %>'></asp:Label>
                                                        <asp:HiddenField ID="hdfIdno" Value='<%#Eval("IDNO")%>' runat="server" />
                                                    </td>
                                                    <%--<td style="text-align: center">
                                                        <%# Eval("COLLEGE_CODE") %>
                                                    </td>--%>
                                                    <td>
                                                        <asp:Label ID="lblDate" runat="server" Text='<%# Eval("DUE_DATE") %>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("INSTALL_AMOUNT") %>'></asp:Label>
                                                    </td>
                                                    <td style="color: red">
                                                        <asp:Label ID="lblStatus" ForeColor='<%# (Convert.ToInt32(Eval("RECON") )== 0 ?System.Drawing.Color.Red:System.Drawing.Color.Green)%>'
                                                            Text='<%# (Convert.ToInt32(Eval("RECON") )== 1 ?  "Paid" : "Not Paid" )%>' runat="server"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <%--<asp:Button ID="btnPay" runat="server" Enabled='<%# (Convert.ToInt32(Eval("RECON") ) == 1 ?  false : true )%>' Visible='<%# (Convert.ToInt32(Eval("RECON") ) == 1 ?  false : true )%>' ToolTip='<%# Eval("DEMAND_NO") %>'
                                                            CommandArgument='<%# Eval("INSTALL_NO") %>' CommandName='<%# Eval("INSTALL_AMOUNT") %>' Text="Pay Now" CssClass="btn btn-success" OnClick="btnPay_Click" />--%>

                                                        <asp:Button ID="btnPay" runat="server" Enabled='<%# (Convert.ToInt32(Eval("RECON") ) == 1 ? false : true )%>' Visible='<%# (Convert.ToInt32(Eval("REWNO") ) > 1 ? false : true )%>' ToolTip='<%# Eval("DEMAND_NO") %>'
                                                            CommandArgument='<%# Eval("INSTALL_NO") %>' CommandName='<%# Eval("INSTALL_AMOUNT") %>' Text="Pay Now" CssClass="btn btn-success" OnClick="btnPay_Click" />
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="btnPrintReceipt" runat="server" OnClick="btnPrintReceipt_Click1" Enabled='<%# (Convert.ToInt32(Eval("RECON") )== 0 ?  false : true )%>'
                                                            CommandArgument='<%# Eval("DCR_NO") %>' ImageUrl="~/Images/print.png" CausesValidation="False" />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </asp:Panel>
                                </div>

                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnClear" runat="server" Text="Cancel" OnClick="btnClear_Click" TabIndex="6" CssClass="btn btn-warning" />
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnPayment" />
            <asp:PostBackTrigger ControlID="btnReciept" />
            <asp:PostBackTrigger ControlID="lvInstallment" />
            <%--<asp:AsyncPostBackTrigger ControlID="ddlReceiptType" />--%>
        </Triggers>
    </asp:UpdatePanel>

    <script type="text/javascript">
        function validateAmount(PaidAmt, TotalAmt) {
            if (PaidAmt.value != '' & (Number(PaidAmt.value) > Number(TotalAmt))) {
                PaidAmt.focus();
                alert("Paid Amount is more than Total Amount.");
                PaidAmt.value = '';
            }
        }

        function UpdateTotalRefundAmt() {
            debugger;
            try {
                var totalFeeAmt = 0.00;
                var dataRows = null;

                var tbl = document.getElementById('ctl00_ContentPlaceHolder1_lvFeeItems_tblFeeItems');
                dataRows = document.getElementById('ctl00_ContentPlaceHolder1_lvFeeItems_tblFeeItems').getElementsByTagName('tr');

                for (i = 0; i < tbl.rows.length - 1; i++) {

                    var chkRow = document.getElementById('ctl00_ContentPlaceHolder1_lvFeeItems_ctrl' + i + '_chkFee');
                    var amount = document.getElementById('ctl00_ContentPlaceHolder1_lvFeeItems_ctrl' + i + '_txtFeeItemAmount')

                    if (chkRow.checked) {

                        //var dataCell = dataRows.item(i).getElementsByTagName('td');

                        //var dataCell1 = dataCell.item(3);
                        //alert(dataCell1)
                        //var controls = dataCell1.getElementsByTagName('input');

                        //var txtAmt = controls.item(0).value;
                        var txtAmt = amount.value;
                        if (txtAmt.trim() != '')
                            totalFeeAmt += parseFloat(txtAmt);

                        // alert(totalFeeAmt)
                    }
                    document.getElementById('ctl00_ContentPlaceHolder1_txtTotalCashAmt').value = totalFeeAmt;
                    document.getElementById('<%=hdnTotalCashAmt.ClientID%>').value = totalFeeAmt;
                }

            }

            catch (e) {
                alert("Error: " + e.description);
            }
        }

        function checkHack() {
            alert('t');

        }


        function checkFeeItems() {
            var chk = false;
            var value = document.getElementById('<%= hdnFeeItemCount.ClientID%>').value;
            for (var i = 0 ; i < value; i++) {
                var check = document.getElementById('ctl00_ContentPlaceHolder1_lvFeeItems_ctrl' + i + '_chkFee');
                if (check.type == 'checkbox') {
                    if (check.checked == true) {
                        chk = true;
                    }
                }

            }
            if (chk == false) {
                alert('Please Select Atleast one FeeHead');
                return false;
            }
            else {
                return true;
            }
        }


    </script>

    <%--Search Box Script Start--%>
    <script type="text/javascript" lang="javascript">

        $(document).ready(function () {
            debugger
            // $("#<%= pnltextbox.ClientID %>").hide();

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
    <script type="text/javascript">
        $(function () {
            $(':text').bind('keydown', function (e) {
                //on keydown for all textboxes prevent from postback
                if (e.target.className != "searchtextbox") {
                    if (e.keyCode == 13) { //if this is enter key
                        document.getElementById('<%=btnSearch.ClientID%>').click();
                        e.preventDefault();
                        return true;
                    }
                    else
                        return true;
                }
                else
                    return true;
            });
        });

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $(':text').bind('keydown', function (e) {
                    //on keydown for all textboxes
                    if (e.target.className != "searchtextbox") {
                        if (e.keyCode == 13) { //if this is enter key
                            document.getElementById('<%=btnSearch.ClientID%>').click();
                            e.preventDefault();
                            return true;
                        }
                        else
                            return true;
                    }
                    else
                        return true;
                });
            });

        });
    </script>

    <script>
        function confirmmsg() {
            if (confirm("If You Select For Hostel Then Your Payable Fees Will be Modified. If You Wish to Continue for Hostel Click 'Ok' Else Click 'Cancel'")) {


                $('#divhosteltype').css("display", "block");
                //window.location.href = "https://jecrc.mastersofterp.in/";
            }
            else {

                //alert("HI");s
                // window.location.reload(true);
                $("#ctl00_ContentPlaceHolder1_divhottrsansport").hide();
                $("#ctl00_ContentPlaceHolder1_divhosteltype").hide();
                // $('#divhottrsansport').hide();
                //$('#divSearchPanel').style.display='block'

                //$('#divtxt').style.display = "block";
            }
        }

    </script>

    <script>
        function confirmhostel() {
            if (confirm("Are you sure to choose for Hostel ? If You Select For this Hostel Then Your Payable Fees Will be Modified. If You Wish to Continue for Hostel Click 'Ok' Else Click 'Cancel'")) {

                $('#divhosteltype').css("display", "block");
                __doPostBack("ModifyDemand");

                //  $('#divhottrsansport').css("display", "block");
                //window.location.href = "https://jecrc.mastersofterp.in/";
            }
            else {

                $("#ctl00_ContentPlaceHolder1_divhosteltype").hide();
                $("#ctl00_ContentPlaceHolder1_divhottrsansport").hide();

            }
        }





    </script>

</asp:Content>

