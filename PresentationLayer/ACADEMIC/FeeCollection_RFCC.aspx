<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    Title="" CodeFile="FeeCollection_RFCC.aspx.cs" Inherits="Academic_FeeCollection" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--<script type="text/javascript">
            function RunThisAfterEachAsyncPostback()
            {
                RepeaterDiv();
            }
    
    
            function RepeaterDiv()
            {
                $(document).ready(function() {

                    $(".display").dataTable({
                        "bJQueryUI": true,
                        "sPaginationType": "full_numbers"
                    });

                });
 
            }
        </script>--%>

    <%--<script type="text/javascript" charset="utf-8">
            $(document).ready(function() {

                $(".display").dataTable({
                    "bJQueryUI": true,
                    "sPaginationType": "full_numbers"
                });

            });
        </script>

        <script type="text/javascript">
            RunThisAfterEachAsyncPostback();
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
        </script>--%>

    <style>
        #DataTables_Table_0_wrapper .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>
    <div class="col-xs-12">

        <div class="row">
            <div class="col-md-12 col-sm-12 col-12">
                <div class="box box-primary">
                    <div id="div2" runat="server"></div>
                    <div class="box-header with-border">
                        <h3 class="box-title">
                            <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                    </div>

                    <div class="box-body">
                        <asp:Panel ID="divSearchPanel" runat="server">
                            <asp:UpdatePanel ID="updPanel" runat="server">
                                <ContentTemplate>
                                    <div id="div3" runat="server" class="col-md-12">
                                        <div class="row">
                                            <div class="form-group col-12">
                                                <%--Search Pannel Start by Swapnil --%>
                                                <div id="myModal2" role="dialog" runat="server">
                                                    <%--<div>
                                                <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="updEdit"
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
                                            </div>--%>
                                                    <div>
                                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                            <ContentTemplate>
                                                                <div class="col-12">
                                                                    <div class="row">
                                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                                            <div class="label-dynamic">
                                                                                <sup>* </sup>
                                                                                <label>Search Criteria</label>
                                                                            </div>

                                                                            <%--onchange=" return ddlSearch_change();"--%>
                                                                            <asp:DropDownList runat="server" class="form-control" ID="ddlSearchPanel" AutoPostBack="true" OnSelectedIndexChanged="ddlSearchPanel_SelectedIndexChanged"
                                                                                AppendDataBoundItems="true" data-select2-enable="true">
                                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>

                                                                        <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divpanel">
                                                                            <asp:Panel ID="pnltextbox" runat="server">
                                                                                <div class="label-dynamic">
                                                                                    <sup>* </sup>
                                                                                    <label>Search String</label>
                                                                                </div>
                                                                                <div id="divtxt" runat="server" style="display: none">
                                                                                    <asp:TextBox ID="txtSearchPanel" runat="server" CssClass="form-control" onkeypress="return Validate()"></asp:TextBox>
                                                                                </div>
                                                                            </asp:Panel>

                                                                            <asp:Panel ID="pnlDropdown" runat="server">
                                                                                <div id="divDropDown" runat="server" style="display: none">
                                                                                    <div class="label-dynamic">
                                                                                        <sup>* </sup>
                                                                                        <%-- <label id="lblDropdown"></label>--%>
                                                                                        <asp:Label ID="lblDropdown" Style="font-weight: bold" runat="server"></asp:Label>
                                                                                    </div>
                                                                                    <asp:DropDownList runat="server" class="form-control" ID="ddlDropdown" AppendDataBoundItems="true" data-select2-enable="true">
                                                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>

                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                            </asp:Panel>
                                                                        </div>
                                                                        <%--   <div class="col-12 btn-footer">--%>
                                                                        <div class="form-group col-lg-3 col-12">
                                                                            <div class="label-dynamic">
                                                                                <label></label>
                                                                            </div>
                                                                            <asp:Button ID="btnSearchPanel" runat="server" Text="Search" CausesValidation="false" CssClass="btn btn-primary" OnClick="btnSearchPanel_Click" OnClientClick="return submitPopup(this.name);" />
                                                                            <%--                                       <asp:Button ID="btnClose" runat="server" Text="Clear Search" CssClass="btn btn-warning" OnClick="btnClose_Click" OnClientClick="return CloseSearchBox(this.name)" data-dismiss="modal" />--%>
                                                                            <asp:Button ID="btnClosePanel" runat="server" Text="Clear" CausesValidation="false" OnClick="btnClosePanel_Click"
                                                                                CssClass="btn btn-warning" TabIndex="4" />

                                                                            <asp:ValidationSummary ID="ValidationSummary1" DisplayMode="List" runat="server" ShowMessageBox="true"
                                                                                ShowSummary="false" ValidationGroup="submit" />
                                                                        </div>
                                                                    </div>
                                                                    <div id="divfooter" runat="server" visible="false">
                                                                        <div class="row">
                                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                                <div class="label-dynamic">
                                                                                    <sup>* </sup>
                                                                                    <label>Payment for Semester</label>
                                                                                </div>
                                                                                <asp:DropDownList ID="DropDownList1" AppendDataBoundItems="true" runat="server" AutoPostBack="true"
                                                                                    CssClass="form-control" Enabled="true" TabIndex="2">
                                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                                <%# Eval("DD_NO") %>
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlSemester"
                                                                                    Display="None" InitialValue="0" ErrorMessage="Please select semester" SetFocusOnError="true"
                                                                                    ValidationGroup="studSearch" />
                                                                            </div>
                                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                                <div class="label-dynamic">
                                                                                    <sup>* </sup>
                                                                                    <label>Select Receipt Type</label>
                                                                                </div>
                                                                                <%--  OnSelectedIndexChanged="ddlReceiptType_SelectedIndexChanged" AutoPostBack="true"--%>
                                                                                <asp:DropDownList ID="ddlReceiptType" runat="server" CssClass="form-control" TabIndex="3" AppendDataBoundItems="true" AutoPostBack="true" />
                                                                            </div>
                                                                        </div>
                                                                    </div>


                                                                </div>

                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>

                                        <div class="col-12 btn-footer">
                                            <asp:Label ID="lblNoRecords" runat="server" SkinID="lblmsg" />
                                        </div>

                                        <div class="col-12">
                                            <asp:Panel ID="pnlLV" runat="server">
                                                <asp:ListView ID="lvStudentPanel" runat="server">
                                                    <LayoutTemplate>
                                                        <div id="listViewGrid" class="vista-grid">
                                                            <div class="sub-heading">
                                                                <h5>Student List</h5>
                                                            </div>
                                                            <asp:Panel ID="Panel2" runat="server">
                                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                                    <thead class="bg-light-blue">
                                                                        <tr>
                                                                            <th>Name
                                                                            </th>
                                                                            <th>Admission Status     
                                                                                <!-- Added By Rohit on 16_12_2022-->
                                                                            </th>
                                                                            <th>Category</th>
                                                                            <th>Year</th>
                                                                            <th>
                                                                                <asp:Label ID="lblDYRRNo" runat="server" Font-Bold="true"></asp:Label>
                                                                            </th>
                                                                            <th>
                                                                                <asp:Label ID="lblDYtxtBranch" runat="server" Font-Bold="true"></asp:Label>
                                                                            </th>
                                                                            <th>
                                                                                <asp:Label ID="lblDYtxtSession" runat="server" Font-Bold="true"></asp:Label>
                                                                            </th>
                                                                            <th>
                                                                                <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>
                                                                            </th>
                                                                            <th>Receipt Type
                                                                            </th>
                                                                            <th>Balance
                                                                            </th>
                                                                            <th>Father Name
                                                                            </th>
                                                                            <%--<th>Mother Name
                                                                            </th>--%>
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
                                                                <asp:LinkButton ID="lnkIdPanel" CausesValidation="false" runat="server" Text='<%# Eval("Name") %>' CommandArgument='<%# Eval("IDNo") %>'
                                                                    OnClick="lnkIdPanel_Click"></asp:LinkButton>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("ADMSTATUS") %>'
                                                                    ForeColor='<%# (Convert.ToString(Eval("ADMSTATUS") )== "ADMITTED" ?System.Drawing.Color.Green:System.Drawing.Color.Red)%>'></asp:Label>
                                                                <!-- Added By Rohit on 16_12_2022-->
                                                            </td>
                                                            <td>
                                                                <%# Eval("CATEGORY")%>
                                                            </td>
                                                            <%--<td>
                                                                        <%# Eval("idno")%>
                                                                    </td>--%>
                                                            <td>
                                                                <%# Eval("YEARNAME")%>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblstuenrollno" runat="server" Text='<%# Eval("EnrollmentNo")%>' ToolTip='<%# Eval("IDNO")%>'></asp:Label>

                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblstudentfullname" runat="server" Text='<%# Eval("longname")%>' ToolTip='<%# Eval("IDNO")%>'></asp:Label>

                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblsession" runat="server" Text='<%# Eval("SESSIONNAME")%>' ToolTip='<%# Eval("SESSIONNO")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblSemester" runat="server" Text='<%# Eval("SEMESTERNAME")%>' ToolTip='<%# Eval("SEMESTERNO")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblReceipttype" runat="server" Text='<%# Eval("RECIEPT_TITLE")%>' ToolTip='<%# Eval("RECIEPT_CODE")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <%# Eval("BALANCE")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("FATHERNAME") %>
                                                            </td>
                                                            <%--<td>
                                                                <%# Eval("MOTHERNAME") %>
                                                            </td>--%>
                                                            <td>
                                                                <%#Eval("STUDENTMOBILE") %>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </asp:Panel>
                                            <%--<layouttemplate>
                                    <itemtemplate>
                                                <tr>
                                                    <td>
                                                        <asp:LinkButton ID="lnkIdPanel" CausesValidation="false" runat="server" Text='<%# Eval("Name") %>' CommandArgument='<%# Eval("IDNo") %>'
                                                            OnClick="lnkIdPanel_Click"></asp:LinkButton>
                                                    </td>
                                                    <td>
                                                        <%# Eval("idno")%>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblstuenrollno" runat="server" Text='<%# Eval("EnrollmentNo")%>' ToolTip='<%# Eval("IDNO")%>'></asp:Label>

                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblstudentfullname" runat="server" Text='<%# Eval("longname")%>' ToolTip='<%# Eval("IDNO")%>'></asp:Label>

                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblstudsemester" runat="server" Text='<%# Eval("SEMESTERNAME")%>' ToolTip='<%# Eval("IDNO")%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <%# Eval("RECIEPT_TITLE")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("BALANCE")%>
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
                                            </itemtemplate>
                                         </layouttemplate>--%>
                                            <%--</asp:ListView>
                        </asp:Panel>--%>
                                        </div>
                                        <%--Search Pannel End--%>
                                        <%--<div class="col-12">

                                        <div class="box-footer">
                                            <p class="text-center">
                                            </p>
                                        </div>
                                    </div>--%>
                                        <asp:HiddenField ID="hdnID" runat="server" />
                                        <asp:HiddenField ID="hdnapppath" runat="server" />
                                        <div id="myModal5" class="modal fade" role="dialog">
                                            <div class="modal-dialog modal-lg">
                                                <!-- Modal content-->
                                                <div class="modal-content">
                                                    <div class="modal-header">
                                                        <h4 class="modal-title">Fee Collection Modes</h4>
                                                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                                                    </div>
                                                    <div class="modal-body">
                                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                            <ContentTemplate>
                                                                <div class="col-12">
                                                                    <div class="row">
                                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                                            <div class="label-dynamic">
                                                                                <label>Mode</label>
                                                                            </div>
                                                                            <asp:DropDownList ID="ddlMode" AppendDataBoundItems="true" runat="server" AutoPostBack="true"
                                                                                CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlMode_SelectedIndexChanged" />
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:PostBackTrigger ControlID="ddlMode" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                    <%-- 
                        </div>
                    </div>
                </div>
                </div>--%>
                                </ContentTemplate>

                            </asp:UpdatePanel>
                        </asp:Panel>
                        <!-- Modal -->

                        <div class="col-xs-12">
                            <asp:UpdatePanel runat="server" ID="updFee">
                                <ContentTemplate>
                                    <div class="row" id="divStudInfo" runat="server" style="display: none">
                                        <div class="col-md-12 col-sm-12 col-12">
                                            <div id="divStudentSearch" runat="server" class="col-md-12" style="display: none;">
                                                <div class="row">
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Enter Enrollment No./Application ID</label>
                                                        </div>
                                                        <div class="input-group date">
                                                            <asp:TextBox ID="txtEnrollNo" runat="server" MaxLength="15" TabIndex="1" CssClass="form-control" />
                                                            <span class="input-group-addon">
                                                                <a href="#" title="Search Student for Fee Payment" data-toggle="modal" data-target="#myModal4"
                                                                    style="background-color: White">
                                                                    <asp:Image ID="imgSearch" runat="server" ImageUrl="~/Images/search-svg.png" TabIndex="4" />
                                                                </a>
                                                            </span>
                                                            <asp:RequiredFieldValidator ID="valEnrollNo" runat="server" ControlToValidate="txtEnrollNo"
                                                                Display="None" ErrorMessage="Please enter Enrollment No./Application ID" SetFocusOnError="true"
                                                                ValidationGroup="studSearch" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Payment for Semester</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlSemester" AppendDataBoundItems="true" runat="server"
                                                            CssClass="form-control" data-select2-enable="true" Enabled="true" TabIndex="2">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <%# Eval("DD_NO") %>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSemester"
                                                            Display="None" InitialValue="0" ErrorMessage="Please select semester" SetFocusOnError="true"
                                                            ValidationGroup="studSearch" />
                                                    </div>
                                                </div>
                                                <div class="col-12 btn-footer">
                                                    <%# (Eval("DD_DT").ToString() != string.Empty) ? ((DateTime)Eval("DD_DT")).ToShortDateString() : Eval("DD_DT") %>
                                                    <asp:Button ID="btnShowInfo" runat="server" Text="Show Information" OnClick="btnShowInfo_Click"
                                                        TabIndex="3" ValidationGroup="studSearch" CssClass="btn btn-primary" />
                                                    <asp:DropDownList ID="ddlExamType" AppendDataBoundItems="true" runat="server"
                                                        CssClass="form-control" data-select2-enable="true" Enabled="true" Visible="False">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        <asp:ListItem Value="1">Regular</asp:ListItem>
                                                        <asp:ListItem Value="2">Repeater</asp:ListItem>
                                                        <asp:ListItem Value="3">Revaluation</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:ValidationSummary ID="valSummary3" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                        ShowSummary="false" ValidationGroup="studSearch" />
                                                </div>
                                            </div>

                                            <div class="col-12" runat="server">
                                                <div class="row">
                                                    <div class="col-lg-6 col-md-6 col-12">
                                                        <ul class="list-group list-group-unbordered">
                                                            <li class="list-group-item"><b>Enrollment No. :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblRegNo" Font-Bold="true" runat="server" /></a>
                                                            </li>
                                                            <li class="list-group-item"><b>Student's Name :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblStudName" Font-Bold="true" runat="server" /></a>
                                                            </li>
                                                            <li class="list-group-item"><b>Programme/Branch :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblBranch" Font-Bold="true" runat="server" /></a>
                                                            </li>

                                                            <li class="list-group-item"><b>Payment Type :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblPaymentType" Font-Bold="true" runat="server" /></a>
                                                            </li>
                                                            <li class="list-group-item"><b>Year :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblYear" Font-Bold="true" runat="server" /></a>
                                                            </li>
                                                            <li class="list-group-item"><b>Admission Status :</b>
                                                                <!-- Added By Rohit on 16_12_2022-->
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblAdmissionstatus" Font-Bold="true" runat="server" /></a>
                                                            </li>
                                                        </ul>
                                                    </div>

                                                    <div class="col-lg-6 col-md-6 col-12">
                                                        <ul class="list-group list-group-unbordered">
                                                            <li class="list-group-item"><b>Degree :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblDegree" Font-Bold="true" runat="server" /></a>
                                                            </li>
                                                            <li class="list-group-item"><b>Gender :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblSex" Font-Bold="true" runat="server" /></a>
                                                            </li>
                                                            <li class="list-group-item"><b>Date of Admission :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblDateOfAdm" Font-Bold="true" runat="server" /></a>
                                                            </li>

                                                            <li class="list-group-item"><b>Batch :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblBatch" Font-Bold="true" runat="server" /></a>
                                                            </li>
                                                            <li class="list-group-item"><b>Current Semester :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblSemester" Font-Bold="true" runat="server" /></a>
                                                            </li>
                                                            <li class="list-group-item"><b>Selected Semester :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblseletedsem" Font-Bold="true" runat="server" /></a>
                                                            </li>

                                                        </ul>
                                                    </div>
                                                </div>
                                            </div>

                                            <div id="divCurrentReceiptInfo" runat="server" visible="false">
                                                <div class="col-12 mt-3">
                                                    <div class="row">
                                                        <div class="col-12">
                                                            <div class="sub-heading">
                                                                <h5>Receipt Information</h5>
                                                            </div>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                                            <div class="label-dynamic">
                                                                <label>Receipt No.</label>
                                                            </div>
                                                            <asp:TextBox ID="txtReceiptNo" runat="server" CssClass="form-control"
                                                                onkeydown="javascript:return false;" TabIndex="3"></asp:TextBox>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>Receipt Date</label>
                                                            </div>
                                                            <%--<asp:TextBox ID="txtReceiptDate" CssClass="data_label" runat="server" ReadOnly="true"
                                                    TabIndex="4" />--%>
                                                            <div class="input-group date">
                                                                <div class="input-group-addon">
                                                                    <i class="fa fa-calendar text-blue"></i>
                                                                </div>
                                                                <asp:TextBox ID="txtReceiptDate" runat="server" TabIndex="4" CssClass="form-control" placeholder="dd/MM/yyyy" OnTextChanged="txtReceiptDate_TextChanged" AutoPostBack="true" />
                                                                <%--<asp:Image ID="imgCalDateOfAdmission" runat="server" src="../images/calendar.png" />--%>
                                                                <%-- <asp:RequiredFieldValidator ID="rfvDateOfReporting" runat="server" ControlToValidate="txtDateOfReporting"
                                                        Display="none" ErrorMessage="Please Enter DOB" SetFocusOnError="true"
                                                        ValidationGroup="academic"></asp:RequiredFieldValidator>--%>
                                                                <ajaxToolKit:CalendarExtender ID="ceDateOfReporting" runat="server" Format="dd/MM/yyyy"
                                                                    TargetControlID="txtReceiptDate" PopupButtonID="imgCalDateOfAdmission" Enabled="true"
                                                                    EnableViewState="true">
                                                                </ajaxToolKit:CalendarExtender>

                                                            </div>

                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <%--  <label>Pay Type (C - For Cash/D/T)</label>--%>  <%--onblur="ValidatePayType(this); UpdateCash_DD_Amount();" onchange="ValidatePayType(); UpdateCash_DD_Amount();"--%>
                                                                <label>Pay Type</label>
                                                            </div>

                                                            <asp:TextBox ID="txtPayType" Text="D" Visible="false"
                                                                runat="server" MaxLength="1" TabIndex="5" ToolTip="Enter C for cash payment OR D for payment by demand draft OR T for transfer student."
                                                                CssClass="form-control" />

                                                            <asp:DropDownList runat="server" ID="ddlPaytype" AppendDataBoundItems="true" onchange="ValidatePayType(this); UpdateCash_DD_Amount();">
                                                                <asp:ListItem Value="0">Please select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="valPayType" runat="server" ControlToValidate="ddlPaytype"
                                                                Display="None" ErrorMessage="Please enter type of payment whether cash(C) or demand draft(D)."
                                                                SetFocusOnError="true" ValidationGroup="submit" />
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divInstallment" runat="server" visible="false">
                                                            <div class="label-dynamic">
                                                                <label>Select Installment</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlInstallment" AutoPostBack="true" runat="server" AppendDataBoundItems="true"
                                                                CssClass="form-control" data-select2-enable="true" Enabled="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>Total Amount</label>
                                                            </div>

                                                            <asp:TextBox ID="txtTotalAmountShow" CssClass="form-control" runat="server" ReadOnly="true" />
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Amount to be Paid </label>
                                                            </div>
                                                            <%-- onblur="DevideTotalAmount(),passToDD(this),AdjustKeyPress(this.value);"--%>
                                                            <asp:TextBox ID="txtTotalAmount" onblur="DevideTotalAmount()" onkeyup="AdjustKeyPress(this.value),passToDD(this);"
                                                                runat="server" ToolTip="Enter total fee amount to be paid." CssClass="form-control" AutoComplete="off"
                                                                TabIndex="12" MaxLength="15" />
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterMode="ValidChars" FilterType="Custom" TargetControlID="txtTotalAmount" ValidChars="1234567890." />
                                                            <%--<asp:TextBox ID="txtTotalAmount" onkeyup="passToDD(this),AdjustKeyPress(this.value);"
                                                    runat="server" ToolTip="Enter total fee amount to be paid." CssClass="data_label"
                                                    TabIndex="12" />--%>
                                                            <asp:HiddenField ID="HdnTotalAmount" runat="server" />
                                                            <asp:RequiredFieldValidator ID="valTotalAmt" runat="server" ControlToValidate="txtTotalAmount"
                                                                Display="None" ErrorMessage="Please Enter Amount to be Paid." SetFocusOnError="true"
                                                                ValidationGroup="submit" />
                                                            <asp:TextBox ID="lblamtpaid"
                                                                runat="server" ToolTip="Enter total fee amount to be paid." Style="display: none" CssClass="data_label"
                                                                TabIndex="12" />
                                                            <%-- <asp:Label ID="lblamtpaid" runat="server"></asp:Label>--%>
                                                            <%--ctl00_ContentPlaceHolder1_lblamtpaid--%>
                                                            <%--<asp:HiddenField ID="hdnamtbepaid" runat="server" />--%>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>Currency</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlCurrency" AutoPostBack="true" runat="server"
                                                                CssClass="form-control" data-select2-enable="true" Enabled="true"
                                                                OnSelectedIndexChanged="ddlCurrency_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12" runat="server" style="display: none;" id="divCredit">
                                                            <div class="label-dynamic">
                                                                <label>Credit/Debit Card No.</label>
                                                            </div>

                                                            <asp:TextBox ID="txtCreditDebit" CssClass="form-control" runat="server" />
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12" runat="server" style="display: none;" id="divTrans">
                                                            <div class="label-dynamic">
                                                                <label>Transaction Id/Reff. No.</label>
                                                            </div>

                                                            <asp:TextBox ID="txtTransReff" CssClass="form-control" runat="server" />
                                                        </div>


                                                        <div class="form-group col-lg-3 col-md-6 col-12" runat="server" style="display: block;" id="divtransdate">
                                                            <div class="label-dynamic">
                                                                <label>Transaction Date.</label>
                                                            </div>

                                                            <div class="input-group">
                                                                <div class="input-group-addon">
                                                                    <i class="fa fa-calendar"></i>
                                                                </div>
                                                                <asp:TextBox ID="transdate" runat="server" TabIndex="14" CssClass="form-control" />
                                                                <%-- <asp:Image ID="imgCalDDDate" runat="server" src="../images/calendar.png" Style="cursor: hand" />--%>
                                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="transdate"
                                                                    PopupButtonID="imgCalDDDate" />
                                                                <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="transdate"
                                                                    Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" ErrorTooltipEnabled="true"
                                                                    OnInvalidCssClass="errordate" />
                                                                <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="meeDDDate"
                                                                    ControlToValidate="txtDDDate" IsValidEmpty="False" EmptyValueMessage="Select Transaction Date."
                                                                    InvalidValueMessage="Demand draft date is invalid" EmptyValueBlurredText="*"
                                                                    InvalidValueBlurredMessage="*" Display="Dynamic" ValidationGroup="submit" />
                                                            </div>
                                                        </div>


                                                        <div class="form-group col-lg-3 col-md-6 col-12" runat="server" style="display: none;" id="divBank">
                                                            <div class="label-dynamic">
                                                                <label>Bank</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlBankT" AppendDataBoundItems="true" runat="server" CssClass="form-control" data-select2-enable="true" />
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="trExcessAmt" runat="server">
                                                            <div class="label-dynamic">
                                                                <label>Excess/Deposit Amount</label>
                                                            </div>
                                                            <asp:TextBox ID="txtExcessAmount" runat="server" CssClass="form-control" onkeydown="javascript:return false;" Enabled="false"></asp:TextBox>
                                                            <asp:HiddenField runat="server" ID="hdAdjustExcess" />
                                                        </div>
                                                        <%-- below control added by Shailendra K on dated 18.05.2023 as per ticket No: 43569 discussed with Dr.Manoj sir --%>
                                                        <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="div1">
                                                            <div class="label-dynamic">
                                                            </div>
                                                            <asp:Button ID="lblCalculateLateFine" ForeColor="Green" Font-Size="Medium" Font-Bold="true" OnClick="lblCalculateLateFine_Click" runat="server" Text="Click To Calculate Late Fine"></asp:Button>
                                                        </div>
                                                        <%-- Above control added by Shailendra K on dated 18.05.2023 as per ticket No: 43569 discussed with Dr.Manoj sir --%>
                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="trExcesschk" runat="server" visible="true">
                                                            <div class="label-dynamic">
                                                                <label></label>
                                                            </div>
                                                            <asp:CheckBox ID="chkAllowExcessFee" runat="server" Text="Allow Deposits"
                                                                AutoPostBack="True" OnCheckedChanged="chkAllowExcessFee_CheckedChanged" />
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false">
                                                            <div class="label-dynamic">
                                                                <label></label>
                                                            </div>
                                                            <asp:CheckBox ID="chkPaytm" runat="server" Text="Pay Through Paytm" />
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Amount Paid in Bank</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlAmtPaidBank" AppendDataBoundItems="true" TabIndex="16" runat="server" CssClass="form-control" data-select2-enable="true" />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlAmtPaidBank"
                                                                Display="None" ErrorMessage="Please select Amount Paid in Bank" ValidationGroup="submit"
                                                                InitialValue="0" SetFocusOnError="true" />
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="dvScholar" visible="false" runat="server">
                                                            <div class="label-dynamic" id="lblScholarship" runat="server">
                                                                <label>Scholarship Adjusted Amount</label>
                                                            </div>
                                                            <asp:Label ID="lblSchAdjAmt" Style="font: bold" runat="server"></asp:Label>
                                                        </div>

                                                        <div class="form-group col-12" id="trNote" runat="server" visible="true">
                                                            <asp:Label runat="server" Text="Note : Select Allow Deposits Checkbox" ForeColor="Red" ID="lblNote"></asp:Label>
                                                        </div>
                                                        <div class="form-group col-12" id="divInstallmentNote" runat="server" visible="false">
                                                            <asp:Label runat="server" Text="Note : Installment Found For This Student" Font-Bold="true" ForeColor="Red" ID="Label1"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-12" id="divDDDetails" runat="server" style="display: none;">
                                                    <div class="row">
                                                        <div class="col-12">
                                                            <div class="sub-heading">
                                                                <h5>Demand Draft/Transfer Reference Details</h5>
                                                            </div>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>DD/Cheque/Trans-Ref No.</label>
                                                            </div>

                                                            <asp:TextBox ID="txtDDNo" runat="server" onkeyup="IsNumeric(this);" TabIndex="13" CssClass="form-control" MaxLength="6" />
                                                            <asp:RequiredFieldValidator ID="valDDNo" ControlToValidate="txtDDNo" runat="server"
                                                                Display="None" ErrorMessage="Please enter demand draft number." ValidationGroup="dd_info" />
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>Amount</label>
                                                            </div>

                                                            <asp:TextBox ID="txtDDAmount" onkeyup="IsNumeric(this);" runat="server" TabIndex="7"
                                                                CssClass="form-control" Enabled="false" />
                                                            <asp:RequiredFieldValidator ID="valDdAmount" ControlToValidate="txtDDAmount" runat="server"
                                                                Display="None" ErrorMessage="Please enter amount of demand draft." ValidationGroup="dd_info" />
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>Date</label>
                                                            </div>

                                                            <div class="input-group">
                                                                <div class="input-group-addon">
                                                                    <i class="fa fa-calendar"></i>
                                                                </div>
                                                                <asp:TextBox ID="txtDDDate" runat="server" TabIndex="14" CssClass="form-control" />
                                                                <%-- <asp:Image ID="imgCalDDDate" runat="server" src="../images/calendar.png" Style="cursor: hand" />--%>
                                                                <ajaxToolKit:CalendarExtender ID="ceDDDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDDDate"
                                                                    PopupButtonID="imgCalDDDate" />
                                                                <ajaxToolKit:MaskedEditExtender ID="meeDDDate" runat="server" TargetControlID="txtDDDate"
                                                                    Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" ErrorTooltipEnabled="true"
                                                                    OnInvalidCssClass="errordate" />
                                                                <ajaxToolKit:MaskedEditValidator ID="mevDDDate" runat="server" ControlExtender="meeDDDate"
                                                                    ControlToValidate="txtDDDate" IsValidEmpty="False" EmptyValueMessage="Demand draft date is required"
                                                                    InvalidValueMessage="Demand draft date is invalid" EmptyValueBlurredText="*"
                                                                    InvalidValueBlurredMessage="*" Display="Dynamic" ValidationGroup="dd_info" />
                                                            </div>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>City</label>
                                                            </div>

                                                            <asp:TextBox ID="txtDDCity" runat="server" TabIndex="15" CssClass="form-control" />
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>Bank</label>
                                                            </div>

                                                            <asp:DropDownList ID="ddlBank" AppendDataBoundItems="true" TabIndex="16" runat="server" CssClass="form-control" data-select2-enable="true" />
                                                            <asp:RequiredFieldValidator ID="valBankName" runat="server" ControlToValidate="ddlBank"
                                                                Display="None" ErrorMessage="Please select bank name." ValidationGroup="dd_info"
                                                                InitialValue="0" SetFocusOnError="true" />
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label></label>
                                                            </div>
                                                            <asp:Button ID="btnSaveDD_Info" runat="server" Text="Save Demand Draft" OnClick="btnSaveDD_Info_Click"
                                                                ValidationGroup="dd_info" TabIndex="17" CssClass="btn btn-primary" />
                                                            <asp:ValidationSummary ID="valSummery2" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                                ShowSummary="false" ValidationGroup="dd_info" />
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-12">
                                                    <asp:Panel ID="Panel1" runat="server">
                                                        <asp:ListView ID="lvDemandDraftDetails" runat="server">
                                                            <LayoutTemplate>
                                                                <div id="divlvDemandDraftDetails1">
                                                                    <div class="sub-heading">
                                                                        <h5>Demand Draft Details</h5>
                                                                    </div>

                                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="tblDD_Details">
                                                                        <thead class="bg-light-blue">
                                                                            <tr>
                                                                                <th>Edit
                                                                                </th>
                                                                                <th>Delete
                                                                                </th>
                                                                                <th>Demand Draft No.
                                                                                </th>
                                                                                <th>Date
                                                                                </th>
                                                                                <th>City
                                                                                </th>
                                                                                <th>Bank
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
                                                                    <td>
                                                                        <asp:ImageButton ID="btnEditDDInfo" runat="server" OnClick="btnEditDDInfo_Click"
                                                                            CommandArgument='<%# Eval("DD_NO") %>' ImageUrl="~/Images/edit.png" ToolTip="Edit Record" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:ImageButton ID="btnDeleteDDInfo" runat="server" OnClick="btnDeleteDDInfo_Click"
                                                                            CommandArgument='<%# Eval("DD_NO") %>' ImageUrl="~/Images/delete.png" ToolTip="Delete Record" CausesValidation="false" />
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("DD_NO") %>
                                                                    </td>
                                                                    <td>
                                                                        <%# (Eval("DD_DT").ToString() != string.Empty) ? ((DateTime)Eval("DD_DT")).ToShortDateString() : Eval("DD_DT") %>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("DD_CITY")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("DD_BANK")%>
                                                                        <asp:HiddenField ID="hidBankNo" runat="server" Value='<%# Eval("DD_BANK_NO") %>' />
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("DD_AMOUNT")%>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                    </asp:Panel>
                                                </div>

                                            </div>

                                            <div class="col-md-12 mt-3">
                                                <div id="divFeesDetails" runat="server" visible="false">
                                                </div>
                                                <asp:ListView ID="lvFeesDetails" runat="server">
                                                    <LayoutTemplate>
                                                        <div id="divlvFeesDetails">
                                                            <div class="sub-heading mt-3">
                                                                <h5>Previous Fees Details</h5>
                                                            </div>

                                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                                <thead class="bg-light-blue">
                                                                    <tr>
                                                                        <th>Semester</th>
                                                                        <th>Applied
                                                                        </th>
                                                                        <th>Paid
                                                                        </th>
                                                                        <th>Balance
                                                                        </th>
                                                                        <th>Refund    
                                                                            <!-- Added By Rohit on 16_12_2022-->
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
                                                                <%# Eval("SEMESTERNAME") %>
                                                            </td>
                                                            <td>
                                                                <%# Eval("ADMFEE")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("PADMFEE")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("BADMFEE")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("REFUND_AMOUNT")%>
                                                                <!-- Added By Rohit on 16_12_2022-->
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>

                                            </div>

                                            <div id="divFeeItems" runat="server" visible="false">
                                                <div class="col-12">
                                                    <%--<div class="sub-heading"><h5>Fee Items</h5></div>--%>
                                                    <asp:ListView ID="lvFeeItems" runat="server">
                                                        <LayoutTemplate>
                                                            <div id="divlvFeeItems">
                                                                <div class="sub-heading">
                                                                    <h5>Applicable Fees</h5>
                                                                </div>
                                                                <%--Available Fee Items--%>
                                                                <table id="tblFeeItems" runat="server" class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                                    <thead class="bg-light-blue">
                                                                        <tr>
                                                                            <th>Sr. No. </th>
                                                                            <th>Fee Heads </th>
                                                                            <th>Currency </th>
                                                                            <th>Demand Amount </th>
                                                                            <th>DCR Amount </th>
                                                                            <th style="text-align: right">Amount </th>
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
                                                                    <asp:Label ID="lblFeeHeadSrNo" runat="server" Text='<%# Eval("SRNO") %>' Visible="false" />
                                                                    <%# Container.DataItemIndex + 1%></td>
                                                                <td><%# Eval("FEE_LONGNAME")%>
                                                                    <asp:HiddenField ID="hdnfld_FEE_LONGNAME" runat="server" Value='<%# Eval("FEE_LONGNAME")%>' />
                                                                    <asp:HiddenField ID="hfFee_hd" runat="server" Value='<%# Eval("FEE_HEAD")%>' />
                                                                </td>
                                                                <td><%# Eval("CURRENCY")%></td>
                                                                <td>
                                                                    <asp:Label ID="lblTotaldemandamt" runat="server" Text='<%# Eval("total_demand") %>' />
                                                                    <%-- <%# Eval("total_demand")%>--%></td>
                                                                <td>
                                                                    <asp:Label ID="lblTotaldcramt" runat="server" Text='<%# Eval("total_dcr") %>' />
                                                                    <%-- <%# Eval("total_dcr")%>--%></td>
                                                                <td>
                                                                    <asp:TextBox ID="txtFeeItemAmount" runat="server" CssClass="form-control" MaxLength="9" onblur="UpdateTotalAndBalance();" onkeyup="IsNumeric(this);" Style="text-align: right" Text=' <%#(Convert.ToDouble(Eval("AMOUNT1"))<0 ? 0.00 : Eval("AMOUNT1"))%>' />
                                                                    <%--<asp:RegularExpressionValidator runat="server" ErrorMessage="Amount is Invalid" ID="RegularExpressionValidator1" ControlToValidate="txtFeeItemAmount"  ValidationExpression=".-{9}.*"
                                                                    Display="None" ValidationGroup="Academic"></asp:RegularExpressionValidator>--%>
                                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterMode="ValidChars" FilterType="Custom" TargetControlID="txtFeeItemAmount" ValidChars="1234567890." />
                                                                    <asp:HiddenField ID="hidFeeItemAmount" runat="server" Value='<%# Eval("AMOUNT1") %>' />
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </div>
                                                <div class="col-12 mt-3">
                                                    <div class="row">
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>
                                                                    Total Cash Amount</label>
                                                            </div>
                                                            <asp:TextBox ID="txtTotalCashAmt" runat="server" CssClass="form-control" onkeydown="javascript:return false;" Text="0" />
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>
                                                                    Total D.D. Amount</label>
                                                            </div>
                                                            <asp:TextBox ID="txtTotalDDAmount" runat="server" CssClass="form-control" onkeydown="javascript:return false;" Text="0" />
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>
                                                                    Total Fee Amount</label>
                                                            </div>
                                                            <asp:TextBox ID="txtTotalFeeAmount" runat="server" CssClass="form-control" onkeydown="javascript:return false;" Text="0" />
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>
                                                                    Excess/Deposit Amount</label>
                                                            </div>
                                                            <asp:TextBox ID="txtFeeBalance" runat="server" CssClass="form-control" onkeydown="javascript:return false;" Text="0" />
                                                            <%--<asp:CompareValidator ID="valFeeBalanceOrDiff" runat="server" ControlToValidate="txtFeeBalance"
                                                ErrorMessage="Total fee amount is not equals to total paid amount. Please check and adjust the fee item amounts."
                                                Operator="Equal" ValueToCompare="0" Display="None" />--%><%--commented by mahesh--%>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>
                                                                    Remark</label>
                                                            </div>
                                                            <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control" MaxLength="400" Rows="4" TabIndex="133" TextMode="MultiLine" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div id="divFeeCriteria" runat="server" visible="false">
                                                <div class="col-12 mt-3">
                                                    <div class="col-12">
                                                        <div class="sub-heading">
                                                            <h5>Fee Criteria
                                                                <img alt="Show/Hide" src="../images/action_down.gif" onclick="ShowHideDiv('ctl00_ContentPlaceHolder1_divHidFeeCriteria', this);" style="display: none;" />
                                                            </h5>
                                                        </div>
                                                    </div>
                                                    <div id="divHidFeeCriteria" runat="server" class="col-12" style="display: none;">
                                                        <div class="row">
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <label>
                                                                        Payment Type</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlPaymentType" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" />
                                                                <asp:RequiredFieldValidator ID="valddlPaymentType" runat="server" ControlToValidate="ddlPaymentType" Display="None" ErrorMessage="Please select payment type." InitialValue="0" SetFocusOnError="true" ValidationGroup="updateCritria" />
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <label>
                                                                        Scholarship Type</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlScholarship" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" />
                                                                <asp:RequiredFieldValidator ID="valddlScholarship" runat="server" ControlToValidate="ddlScholarship" Display="None" ErrorMessage="Please select scholarship type." InitialValue="0" SetFocusOnError="true" ValidationGroup="updateCritria" />
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <label>
                                                                    </label>
                                                                </div>
                                                                <asp:Button ID="btnUpdateFeeCriteria" runat="server" CssClass="btn btn-primary" OnClick="btnUpdateFeeCriteria_Click" Text="Update Fee Criteria" ValidationGroup="updateCritria" />
                                                                <asp:ValidationSummary ID="valSummary4" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="updateCritria" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-12 btn-footer">
                                                <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary" Enabled="false" OnClick="btnSubmit_Click" Text="Submit" ValidationGroup="submit" />
                                                <%-- OnClientClick="getdata();"--%>
                                                <asp:Button ID="btnNewFee" runat="server" CausesValidation="false" CssClass="btn btn-primary" OnClick="btnNewFee_Click" Text="New Receipt" Visible="false" />
                                                <asp:Button ID="btnBack" runat="server" CausesValidation="false" CssClass="btn btn-primary" OnClick="btnBack_Click" Text="Back" />
                                                <asp:Button ID="btnReport" runat="server" CausesValidation="false" CssClass="btn btn-info" Enabled="false" OnClick="btnReport_Click" Text="Receipt Report" />
                                                <asp:Button ID="btnCancel" runat="server" CausesValidation="false" CssClass="btn btn-danger" OnClick="btnCancel_Click" Text="Cancel" />
                                                <asp:ValidationSummary ID="valSummery1" runat="server" DisplayMode="List" ShowSummary="false" ValidationGroup="submit" />
                                            </div>

                                            <div id="divPreviousReceipts" runat="server" visible="false">
                                                <div class="col-12">
                                                    <%--<div class="sub-heading">
                                                        <h5>Previous Receipts
                                                <img alt="Show/Hide" src="../images/action_down.gif" onclick="ShowHideDiv('ctl00_ContentPlaceHolder1_divHidPreviousReceipts', this);" style="display: none;" />
                                                        </h5>
                                                    </div>--%>
                                                    <div id="divHidPreviousReceipts" runat="server">
                                                        <%# Eval("DD_BANK")%>
                                                        <asp:Panel ID="Panel2" runat="server">
                                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                                <asp:Repeater ID="lvPaidReceipts" runat="server">
                                                                    <HeaderTemplate>
                                                                        <div class="sub-heading">
                                                                            <h5>Previous Receipts Information</h5>
                                                                        </div>
                                                                        <thead class="bg-light-blue">
                                                                            <tr>
                                                                                <th>Print </th>
                                                                                <th>Receipt Type </th>
                                                                                <th>Receipt No </th>
                                                                                <th>Date </th>
                                                                                <th>Semester </th>
                                                                                <th>Pay Type </th>
                                                                                <th>Amount </th>
                                                                                <th>Receipt Status </th>
                                                                            </tr>
                                                                        </thead>
                                                                        <tbody>
                                                                            <%--<tr id="itemPlaceholder" runat="server" />--%>
                                                                        </tbody>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <%--  <tr class="item">--%>
                                                                            <td>
                                                                                <asp:ImageButton ID="btnPrintReceipt" runat="server" CausesValidation="False" CommandArgument='<%# Eval("DCR_NO") %>' ImageUrl="~/Images/print.png" OnClick="btnPrintReceipt_Click" ToolTip='<%# Eval("CAN")%>' />
                                                                                <asp:HiddenField ID="hdnReconStatus" runat="server" Value='<%# Eval("REFUND")%>' />
                                                                            </td>
                                                                            <td><%# Eval("RECIEPT_TITLE") %></td>
                                                                            <td><%# Eval("REC_NO") %></td>
                                                                            <td><%# (Eval("REC_DT").ToString() != string.Empty) ? ((DateTime)Eval("REC_DT")).ToShortDateString() : Eval("REC_DT") %></td>
                                                                            <td><%# Eval("SEMESTERNAME") %></td>
                                                                            <td><%# Eval("PAY_TYPE") %></td>
                                                                            <td><%# Eval("TOTAL_AMT") %></td>
                                                                            <td><%-- <%# Eval("TOTAL_AMT") %>--%><%-- <asp:Label ID="lblreconstatus" Text='<%# Convert.ToInt32(Eval("RECON"))==1 ?"Yes":"NO" %>' runat="server" Font-Bold="true" ForeColor="Green"></asp:Label>--%>
                                                                                <asp:Label ID="lblreconstatus" runat="server" Text='<%# Eval("RECON_STATUS")%>'></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        </tbody>
                                                                    </FooterTemplate>
                                                                </asp:Repeater>
                                                            </table>
                                                        </asp:Panel>
                                                    </div>
                                                </div>
                                            </div>
                                            </br>
                                            </br>


                                        </div>
                                    </div>

                                </ContentTemplate>
                                <Triggers>
                                    <%--<asp:AsyncPostBackTrigger ControlID="btnShowInfo" />--%>
                                    <asp:PostBackTrigger ControlID="btnShowInfo" />
                                    <asp:PostBackTrigger ControlID="btnsubmit" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <div id="divModalboxContent" style="display: none; height: 540px">
    </div>

    <div id="divMsg" runat="server">
    </div>

    <script type="text/javascript">

        function ShowModalbox() {
            try {
                Modalbox.show($('divModalboxContent'), { title: 'Search Student for Fee Payment', width: 600, overlayClose: false, slideDownDuration: 0.1, slideUpDuration: 0.1, afterLoad: SetFocus });
            }
            catch (e) {
                alert("Error: " + e.description);
            }
            return;
        }

        function SetFocus() {
            try {
                document.getElementById('<%= txtSearchPanel.ClientID %>').focus();
            }
            catch (e) {
                alert("Error: " + e.description);
            }
        }

        <%--function SubmitSearch(btnsearch) {
            try {
                var searchParams = "";
                if (document.getElementById('<%= rdoName.ClientID %>').checked) {
                    searchParams = "Name=" + document.getElementById('<%= txtSearch.ClientID %>').value;
                    searchParams += ",EnrollNo=";
                }
                else if (document.getElementById('<%= rdoEnrollmentNo.ClientID %>').checked) {
                    searchParams = "EnrollNo=" + document.getElementById('<%= txtSearch.ClientID %>').value;
                    searchParams += ",IdNo=";
                }
                else if (document.getElementById('<%=rdoIdNo.ClientID%>').checked) {
                    searchParams = "IdNo=" + document.getElementById('<%= txtSearch.ClientID %>').value;
                    searchParams += ",Name=";
                }
        searchParams += ",DegreeNo=" + document.getElementById('<%= ddlDegree.ClientID %>').options[document.getElementById('<%= ddlDegree.ClientID %>').selectedIndex].value;
                searchParams += ",BranchNo=" + document.getElementById('<%= ddlBranch.ClientID %>').options[document.getElementById('<%= ddlBranch.ClientID %>').selectedIndex].value;
                searchParams += ",YearNo=" + document.getElementById('<%= ddlYear.ClientID %>').options[document.getElementById('<%= ddlYear.ClientID %>').selectedIndex].value;
                searchParams += ",SemNo=" + document.getElementById('<%= ddlSem.ClientID %>').options[document.getElementById('<%= ddlSem.ClientID %>').selectedIndex].value;

                __doPostBack(btnsearch, searchParams);
            }
            catch (e) {
                alert("Error: " + e.description);
            }
            return;
        }--%>
        function ShowHideDiv(divId, img) {
            try {
                if (document.getElementById(divId) != null &&
                    document.getElementById(divId).style.display == 'none') {
                    document.getElementById(divId).style.display = 'block';
                    img.src = '../images/action_up.gif';
                }
                else {
                    document.getElementById(divId).style.display = 'none';
                    img.src = '../images/action_down.gif';
                }
            }
            catch (e) {
                alert("Error: " + e.description);
            }
        }
        function IsNumeric(textbox) {
            if (textbox != null && textbox.value != "") {
                if (isNaN(textbox.value)) {
                    document.getElementById(textbox.id).value = '';
                }
            }
        }
        function DevideTotalAmount() {
            try {

                var totalAmt = 0;
                if (document.getElementById('ctl00_ContentPlaceHolder1_txtTotalAmount') != null &&
                document.getElementById('ctl00_ContentPlaceHolder1_txtTotalAmount').value.trim() != '')
                    totalAmt = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_txtTotalAmount').value.trim());
                //alert(totalAmt); 
                //if (totalAmt == 0) {
                //    alert("Please Enter Valid Amount");
                //    totalAmt = null;
                //    return;
                //}
                var dataRows = null;
                if (document.getElementById('ctl00_ContentPlaceHolder1_lvFeeItems_tblFeeItems') != null)
                    dataRows = document.getElementById('ctl00_ContentPlaceHolder1_lvFeeItems_tblFeeItems').getElementsByTagName('tr');

                if (dataRows != null) {
                    for (i = 1; i < dataRows.length; i++) {
                        var dataCellCollection = dataRows.item(i).getElementsByTagName('td');
                        var dataCell = dataCellCollection.item(5);
                        var controls = dataCell.getElementsByTagName('input');

                        var originalAmt = controls.item(1).value;
                        // alert(originalAmt);
                        if (originalAmt.trim() != '')
                            originalAmt = parseFloat(originalAmt);

                        if ((totalAmt - originalAmt) >= originalAmt) {
                            document.getElementById('ctl00_ContentPlaceHolder1_lvFeeItems_ctrl' + (i - 1) + '_txtFeeItemAmount').value = originalAmt;
                            totalAmt = (totalAmt - originalAmt);
                        }

                        else {
                            if ((totalAmt - originalAmt) >= 0) {
                                document.getElementById('ctl00_ContentPlaceHolder1_lvFeeItems_ctrl' + (i - 1) + '_txtFeeItemAmount').value = originalAmt;
                                totalAmt = (totalAmt - originalAmt);
                            }
                            else {
                                document.getElementById('ctl00_ContentPlaceHolder1_lvFeeItems_ctrl' + (i - 1) + '_txtFeeItemAmount').value = totalAmt;
                                totalAmt = 0;
                            }
                        }
                    }
                }
                UpdateTotalAndBalance();
            }
            catch (e) {
                alert("Error: " + e.description);
            }
        }

        function DevideTotalAmountFromcodebehind() {
            try {

                var totalAmt = 0;
                if (document.getElementById('ctl00_ContentPlaceHolder1_txtTotalAmount') != null &&
                document.getElementById('ctl00_ContentPlaceHolder1_txtTotalAmount').value.trim() != '')
                    totalAmt = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_txtTotalAmount').value.trim());

                //if (totalAmt == 0) {
                //    alert("Please Enter Valid Amount");
                //    totalAmt = null;
                //    return;
                //}
                var dataRows = null;
                if (document.getElementById('ctl00_ContentPlaceHolder1_lvFeeItems_tblFeeItems') != null)
                    dataRows = document.getElementById('ctl00_ContentPlaceHolder1_lvFeeItems_tblFeeItems').getElementsByTagName('tr');
                //alert('count');
                //alert(dataRows.length);
                if (dataRows != null) {
                    for (i = 1; i < dataRows.length; i++) {
                        var dataCellCollection = dataRows.item(i).getElementsByTagName('td');
                        var dataCell = dataCellCollection.item(5);
                        var controls = dataCell.getElementsByTagName('input');

                        var originalAmt = controls.item(1).value;
                        var demandamt = null;
                        var dcramt = null;

                        //alert('totalAmt');
                        //alert(totalAmt);
                        demandamt = document.getElementById('ctl00_ContentPlaceHolder1_lvFeeItems_ctrl' + (i - 1) + '_lblTotaldemandamt').textContent;
                        dcramt = document.getElementById('ctl00_ContentPlaceHolder1_lvFeeItems_ctrl' + (i - 1) + '_lblTotaldcramt').textContent;
                        //alert('demand');
                        //alert(demandamt);
                        //alert(dcramt);
                        if ((demandamt - dcramt) >= totalAmt) {
                            document.getElementById('ctl00_ContentPlaceHolder1_lvFeeItems_ctrl' + (i - 1) + '_txtFeeItemAmount').value = totalAmt;
                            totalAmt = 0;
                        }
                        else {
                            document.getElementById('ctl00_ContentPlaceHolder1_lvFeeItems_ctrl' + (i - 1) + '_txtFeeItemAmount').value = (demandamt - dcramt);
                            totalAmt = totalAmt - (demandamt - dcramt);
                        }



                        //else {

                        //}
                    }
                }
                UpdateTotalAndBalancecodebehind();
                if (document.getElementById('ctl00_ContentPlaceHolder1_txtPayType').value.trim() == "D") {
                    //alert('in a');
                    totAmount = document.getElementById('ctl00_ContentPlaceHolder1_txtTotalAmount').value.trim();
                    document.getElementById('ctl00_ContentPlaceHolder1_txtDDAmount').value = parseFloat(totAmount);
                    document.getElementById('ctl00_ContentPlaceHolder1_HdnTotalAmount').value = parseFloat(totAmount);
                }

            }
            catch (e) {
                //alert("Error: " + e.description);
            }
        }

        function UpdateTotalAndBalancecodebehind() {
            try {

                var totalFeeAmt = 0.00;
                var dataRows = null;

                if (document.getElementById('ctl00_ContentPlaceHolder1_lvFeeItems_tblFeeItems') != null)
                    dataRows = document.getElementById('ctl00_ContentPlaceHolder1_lvFeeItems_tblFeeItems').getElementsByTagName('tr');

                if (dataRows != null) {
                    for (i = 1; i < dataRows.length; i++) {
                        var dataCellCollection = dataRows.item(i).getElementsByTagName('td');
                        var dataCell = dataCellCollection.item(5);
                        var controls = dataCell.getElementsByTagName('input');
                        var txtAmt = controls.item(0).value;
                        if (txtAmt.trim() != '')
                            totalFeeAmt += parseFloat(txtAmt);
                    }
                    if (document.getElementById('ctl00_ContentPlaceHolder1_txtTotalFeeAmount') != null)
                        document.getElementById('ctl00_ContentPlaceHolder1_txtTotalFeeAmount').value = totalFeeAmt;

                    var totalPaidAmt = 0;
                    if (document.getElementById('ctl00_ContentPlaceHolder1_txtTotalAmount') != null && document.getElementById('ctl00_ContentPlaceHolder1_txtTotalAmount').value.trim() != '')
                        totalPaidAmt = document.getElementById('ctl00_ContentPlaceHolder1_txtTotalAmount').value.trim();

                    if (document.getElementById('ctl00_ContentPlaceHolder1_txtFeeBalance') != null)
                        document.getElementById('ctl00_ContentPlaceHolder1_txtFeeBalance').value = (parseFloat(totalPaidAmt) - parseFloat(totalFeeAmt));
                }
                UpdateCash_DD_Amountcodebehind();
            }
            catch (e) {
                // alert("Error: " + e.description);
            }
            function UpdateCash_DD_Amountcodebehind() {
                try {

                    var txtPayType = document.getElementById('ctl00_ContentPlaceHolder1_ddlPaytype');
                    var txtPaidAmt = document.getElementById('ctl00_ContentPlaceHolder1_txtTotalAmount');
                    if (txtPaidAmt != null) {
                        document.getElementById('ctl00_ContentPlaceHolder1_txtDDAmount').value = txtPaidAmt.value.trim();
                    }
                    if (txtPayType != null && txtPaidAmt != null) {
                        if (txtPayType.value.trim() == "C" && document.getElementById('ctl00_ContentPlaceHolder1_txtTotalCashAmt') != null) {
                            document.getElementById('ctl00_ContentPlaceHolder1_txtTotalCashAmt').value = txtPaidAmt.value.trim();
                            document.getElementById('ctl00_ContentPlaceHolder1_txtTotalDDAmount').value = '0';
                        }
                        else if (txtPayType.value.trim() == "T") {
                            var totalDDAmt = 0.00;
                            if (txtPayType.value.trim() == "T" && document.getElementById('ctl00_ContentPlaceHolder1_txtTotalCashAmt') != null) {
                                document.getElementById('ctl00_ContentPlaceHolder1_txtTotalCashAmt').value = txtPaidAmt.value.trim();
                                document.getElementById('ctl00_ContentPlaceHolder1_txtTotalCashAmt').value = parseFloat(txtPaidAmt.value.trim()) - parseFloat(totalDDAmt);
                                document.getElementById('ctl00_ContentPlaceHolder1_txtTotalDDAmount').value = '0';
                            }
                        }
                        else if (txtPayType.value.trim() == "D" && document.getElementById('tblDD_Details') != null) {

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
                    // alert("Error: " + e.description);
                }
            }
        }

        function UpdateTotalAndBalance() {
            try {
                var totalFeeAmt = 0.00;
                var dataRows = null;

                if (document.getElementById('ctl00_ContentPlaceHolder1_lvFeeItems_tblFeeItems') != null)
                    dataRows = document.getElementById('ctl00_ContentPlaceHolder1_lvFeeItems_tblFeeItems').getElementsByTagName('tr');

                if (dataRows != null) {
                    for (i = 1; i < dataRows.length; i++) {
                        var dataCellCollection = dataRows.item(i).getElementsByTagName('td');
                        var dataCell = dataCellCollection.item(5);
                        var controls = dataCell.getElementsByTagName('input');
                        var txtAmt = controls.item(0).value;
                        if (txtAmt.trim() != '')
                            totalFeeAmt += parseFloat(txtAmt);
                    }
                    if (document.getElementById('ctl00_ContentPlaceHolder1_txtTotalFeeAmount') != null)
                        document.getElementById('ctl00_ContentPlaceHolder1_txtTotalFeeAmount').value = totalFeeAmt;

                    var totalPaidAmt = 0;
                    if (document.getElementById('ctl00_ContentPlaceHolder1_txtTotalAmount') != null && document.getElementById('ctl00_ContentPlaceHolder1_txtTotalAmount').value.trim() != '')
                        totalPaidAmt = document.getElementById('ctl00_ContentPlaceHolder1_txtTotalAmount').value.trim();

                    if (document.getElementById('ctl00_ContentPlaceHolder1_txtFeeBalance') != null)
                        document.getElementById('ctl00_ContentPlaceHolder1_txtFeeBalance').value = (parseFloat(totalPaidAmt) - parseFloat(totalFeeAmt));
                }
                UpdateCash_DD_Amount();
            }
            catch (e) {
                alert("Error: " + e.description);
            }
            function UpdateCash_DD_Amount() {
                try {
                    var txtPayType = document.getElementById('ctl00_ContentPlaceHolder1_ddlPaytype');
                    var txtPaidAmt = document.getElementById('ctl00_ContentPlaceHolder1_txtTotalAmount');
                    if (txtPaidAmt != null) {
                        document.getElementById('ctl00_ContentPlaceHolder1_txtDDAmount').value = txtPaidAmt.value.trim();
                    }
                    if (txtPayType != null && txtPaidAmt != null) {
                        if (txtPayType.value.trim() == "C" && document.getElementById('ctl00_ContentPlaceHolder1_txtTotalCashAmt') != null) {
                            document.getElementById('ctl00_ContentPlaceHolder1_txtTotalCashAmt').value = txtPaidAmt.value.trim();
                            document.getElementById('ctl00_ContentPlaceHolder1_txtTotalDDAmount').value = '0';
                        }
                        else if (txtPayType.value.trim() == "T") {
                            var totalDDAmt = 0.00;
                            if (txtPayType.value.trim() == "T" && document.getElementById('ctl00_ContentPlaceHolder1_txtTotalCashAmt') != null) {
                                document.getElementById('ctl00_ContentPlaceHolder1_txtTotalCashAmt').value = txtPaidAmt.value.trim();
                                document.getElementById('ctl00_ContentPlaceHolder1_txtTotalCashAmt').value = parseFloat(txtPaidAmt.value.trim()) - parseFloat(totalDDAmt);
                                document.getElementById('ctl00_ContentPlaceHolder1_txtTotalDDAmount').value = '0';
                            }
                        }
                        else if (txtPayType.value.trim() == "D" && document.getElementById('tblDD_Details') != null) {

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
        }

        function ValidatePayType(txtPayType) {
            //function ValidatePayType() {ddlPaytype
            //var txtPayType = document.getElementById('ctl00_ContentPlaceHolder1_txtPayType');
            //alert(txtPayType.value);
            try {
                //debugger;
                if (txtPayType != null && txtPayType.value != '') {
                    if (txtPayType.value.toUpperCase() == 'D') {
                        txtPayType.value = "D";
                        document.getElementById('ctl00_ContentPlaceHolder1_divCredit').style.display = "none";
                        document.getElementById('ctl00_ContentPlaceHolder1_divTrans').style.display = "none";
                        //document.getElementById('ctl00_ContentPlaceHolder1_divtransdate').style.display = "none";
                        document.getElementById('ctl00_ContentPlaceHolder1_divBank').style.display = "none";
                        if (document.getElementById('ctl00_ContentPlaceHolder1_divDDDetails') != null) {
                            document.getElementById('ctl00_ContentPlaceHolder1_divDDDetails').style.display = "block";
                            // document.getElementById('ctl00_ContentPlaceHolder1_txtDDNo').focus();
                        }
                    }
                    else {
                        if (txtPayType.value.toUpperCase() == 'C') {

                            txtPayType.value = "C";
                            document.getElementById('ctl00_ContentPlaceHolder1_divCredit').style.display = "none";
                            document.getElementById('ctl00_ContentPlaceHolder1_divTrans').style.display = "none";
                            // document.getElementById('ctl00_ContentPlaceHolder1_divtransdate').style.display = "none";
                            document.getElementById('ctl00_ContentPlaceHolder1_divBank').style.display = "none";
                            if (document.getElementById('ctl00_ContentPlaceHolder1_divDDDetails') != null &&
                                document.getElementById('ctl00_ContentPlaceHolder1_divFeeItems') != null) {
                                document.getElementById('ctl00_ContentPlaceHolder1_divDDDetails').style.display = "none";
                                document.getElementById('ctl00_ContentPlaceHolder1_divFeeItems').style.display = "block";

                            }
                        }
                        else {
                            if (txtPayType.value.toUpperCase() == 'T') {
                                txtPayType.value = "T";
                                document.getElementById('ctl00_ContentPlaceHolder1_divCredit').style.display = "none";
                                document.getElementById('ctl00_ContentPlaceHolder1_divTrans').style.display = "block";
                                //document.getElementById('ctl00_ContentPlaceHolder1_divtransdate').style.display = "block";
                                document.getElementById('ctl00_ContentPlaceHolder1_divBank').style.display = "block";
                                document.getElementById('ctl00_ContentPlaceHolder1_divDDDetails').style.display = "none";
                            }
                            else {
                                //alert("Please enter only 'C' for Cash payment OR 'D' for payment through Demand Drafts OR 'T' for Transfer Payment To Online Transfer.");
                                if (document.getElementById('ctl00_ContentPlaceHolder1_divDDDetails') != null)
                                    document.getElementById('ctl00_ContentPlaceHolder1_divDDDetails').style.display = "none";
                                document.getElementById('ctl00_ContentPlaceHolder1_divCredit').style.display = "none";
                                document.getElementById('ctl00_ContentPlaceHolder1_divTrans').style.display = "block";
                                // document.getElementById('ctl00_ContentPlaceHolder1_divtransdate').style.display = "block";


                                //txtPayType.value = "";
                                //txtPayType.focus();
                            }
                        }
                    }
                }
            }
            catch (e) {
                alert("Error: " + e.description);
            }
        }




        function AdjustKeyPress(value) {

            var keyAdjust = 'ADJUSTMENT';
            var hdnExcess = document.getElementById('<%=hdAdjustExcess.ClientID %>');
            if (hdnExcess != null) {
                // alert('A');
                if (hdnExcess.value.match(keyAdjust)) {
                    var v = parseFloat(value);
                    var txtExcess = document.getElementById('<%=txtExcessAmount.ClientID %>');
                    var totDemand = document.getElementById('ctl00_ContentPlaceHolder1_txtTotalAmountShow');
                    if (txtExcess != null) {
                        var maxValue = parseFloat(document.getElementById('<%=txtExcessAmount.ClientID %>').value);
                        if (v > maxValue) {
                            document.getElementById('<%=txtTotalAmount.ClientID %>').value = '';
                            document.getElementById('ctl00_ContentPlaceHolder1_txtFeeBalance').value = 0;
                            document.getElementById('ctl00_ContentPlaceHolder1_txtTotalCashAmt').value = 0;
                            alert("Entered Amount Should Be Less Than or Equal To Excess Amount.");
                            //alert("Entered Amount Should Be Less Than or Equal To Excess Amount and Total Demand Amount.");
                        }
                        else {

                        }
                    }
                }
            }
            else {
                alert("Error: " + e.description);
            }
            passToDD(value);
        }


        function passToDD(value) {
            debugger;
            //  var testNumber = isNaN(value.toString());
            var testNumber = IsNumeric(value);
            //alert(parseInt(testNumber));
            // alert(testNumber);
            if (testNumber == "NaN") {
                var x = '';
                document.getElementById('ctl00_ContentPlaceHolder1_txtDDAmount').value = parseFloat(x);
                return false;
            } else {
                //alert("process next step");
                var totAmount = 0;
                if (document.getElementById('ctl00_ContentPlaceHolder1_ddlPaytype').value.trim() == "D") {
                    //document.getElementById('ctl00_ContentPlaceHolder1_txtTotalAmount').value = 0;

                    totAmount = document.getElementById('ctl00_ContentPlaceHolder1_txtTotalAmount').value.trim();
                    document.getElementById('ctl00_ContentPlaceHolder1_txtDDAmount').value = parseFloat(totAmount);
                    document.getElementById('ctl00_ContentPlaceHolder1_HdnTotalAmount').value = parseFloat(totAmount);
                    // alert(totAmount);
                    // alert(document.getElementById('ctl00_ContentPlaceHolder1_txtDDAmount').value);
                }
                else {
                    //document.getElementById('ctl00_ContentPlaceHolder1_txtTotalAmount').value = 0;
                    totAmount = document.getElementById('ctl00_ContentPlaceHolder1_txtTotalAmount').value.trim();
                    // document.getElementById('ctl00_ContentPlaceHolder1_txtTotalAmount').value = totAmount;
                    // To show the amount is sub heads
                    document.getElementById('ctl00_ContentPlaceHolder1_txtTotalAmount').focus();
                    //To Come back on Enter Amount Textbox
                    document.getElementById('ctl00_ContentPlaceHolder1_txtTotalAmount').focus();
                    document.getElementById('ctl00_ContentPlaceHolder1_hfldTotalAmt').value = totAmount;
                    document.getElementById('ctl00_ContentPlaceHolder1_HdnTotalAmount').value = parseFloat(totAmount);

                }

            }
        }
    </script>

    <script type="text/javascript">
        function ClearSearchBox(btnclear) {
            document.getElementById('<%=txtSearchPanel.ClientID %>').value = '';
            __doPostBack(btnclear, '');
            return true;
        }
    </script>
    <%--Search Box Script Start--%>
    <script type="text/javascript" lang="javascript">

       <%-- $(document).ready(function () {
            debugger
            $("#<%= divpanel.ClientID %>").css("display", "none");
            $("#<%= pnltextbox.ClientID %>").css("display", "none");
            $("#<%= pnlDropdown.ClientID %>").css("display", "none");
        });--%>
        function submitPopup(btnsearch) {

            debugger
            var rbText;
            var searchtxt;

            var e = document.getElementById("<%=ddlSearchPanel.ClientID%>");
            var rbText = e.options[e.selectedIndex].text;
            var ddlname = e.options[e.selectedIndex].text;
            if (rbText == "Please Select") {
                alert('Please select Criteria as you want search...')
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
                    searchtxt = document.getElementById('<%=txtSearchPanel.ClientID %>').value;
                    if (searchtxt == "" || searchtxt == null) {
                        alert('Please Enter Data you want to search..');
                        document.getElementById('<%=txtSearchPanel.ClientID %>').focus();
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
        document.getElementById('<%=txtSearchPanel.ClientID %>').value = '';
        __doPostBack(btncancelmodal, '');
        return true;
    }
    function CloseSearchBox(btnClose) {
        document.getElementById('<%=txtSearchPanel.ClientID %>').value = '';
        __doPostBack(btnClose, '');
        return true;
    }




    function Validate() {

        debugger

        var rbText;

        var e = document.getElementById("<%=ddlSearchPanel.ClientID%>");
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

            if ((char >= 65 && char <= 90) || (char >= 97 && char <= 122) || (char = 49)) {
                return true;
            }
            else {
                return false;
            }

        }
    }


    </script>
    <%--Search Box Script End--%>
    <script>
        function confirmmsg() {
            if (confirm("Fee collection done successfully, Do want to generate receipt ?")) {
                document.getElementById('<%=btnReport.ClientID%>').click();
            }
            else {
                document.getElementById('<%=btnBack.ClientID%>').click();
                // window.location.reload(true);
                //$('#divStudInfo').css("display", "none");
                //$('#divSearchPanel').style.display='block'
                //$('#divtxt').style.display = "block";
            }
        }
    </script>
    <%--<script type="text/javascript">
        $(document).keypress(function (e) {
            var keyCode = (window.event) ? e.which : e.keyCode;
            if (keyCode && keyCode == 13) {
                document.getElementById('<%=btnSearchPanel.ClientID%>').click();
                return false;
            }
        });
    </script>--%>

    <script type="text/javascript">
        $(function () {
            $(':text').bind('keydown', function (e) {
                //on keydown for all textboxes prevent from postback
                if (e.target.className != "searchtextbox") {
                    if (e.keyCode == 13) { //if this is enter key
                        document.getElementById('<%=btnSearchPanel.ClientID%>').click();
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
                            document.getElementById('<%=btnSearchPanel.ClientID%>').click();
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




    <script type="text/javascript">

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $(':text').bind('keydown', function (e) {
                    //on keydown for all textboxes
                    if (e.target.className != "searchtextbox") {
                        if (e.keyCode == 13) { //if this is enter key
                            document.getElementById('<%=btnSearchPanel.ClientID%>').click();
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
</asp:Content>
