<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="LeaveApplyByStudent.aspx.cs" Inherits="ACADEMIC_TIMETABLE_LeaveApplyByStudent" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--<style>
        #ctl00_ContentPlaceHolder1_btnShow {
            z-index: 0;
        }
    </style>--%>
    <div>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updHoliday"
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

    <asp:UpdatePanel ID="updHoliday" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <%--<h3 class="box-title">OD APPLY</h3>--%>
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12" id="divCourses" runat="server">
                                <asp:Panel ID="tblInfo" runat="server" Visible="true">
                                    <div class="row">
                                        <div class="col-lg-6 col-md-6 col-12">
                                            <ul class="list-group list-group-unbordered">
                                                <li class="list-group-item"><b>Student Name :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblName" runat="server" Font-Bold="True" />
                                                    </a>
                                                </li>
                                                <li class="list-group-item"><b>Father Name :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblFatherName" runat="server" Font-Bold="True" /></a>
                                                </li>
                                                <li class="list-group-item"><b>Mother Name :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblMotherName" runat="server" Font-Bold="True" /></a>
                                                </li>
                                                <li class="list-group-item"><b>
                                                    <asp:Label ID="lblDYRRNo" runat="server" Font-Bold="true"></asp:Label>
                                                    :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblEnrollNo" runat="server" Font-Bold="True"></asp:Label></a>
                                                </li>
                                                <li class="list-group-item"><b>
                                                    <asp:Label ID="lblDYddlAdmBatch" runat="server" Font-Bold="true"></asp:Label>
                                                    :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblAdmBatch" runat="server" Font-Bold="True"></asp:Label></a>
                                                </li>
                                                <li class="list-group-item"><b>
                                                    <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>
                                                    :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblSemester" runat="server" Font-Bold="True"></asp:Label></a>
                                                </li>
                                                <li class="list-group-item"><b>
                                                    <asp:Label ID="lblDYddlDegree" runat="server" Font-Bold="true"></asp:Label>
                                                    /
                                                    <asp:Label ID="lblDYtxtBranch" runat="server" Font-Bold="true"></asp:Label>
                                                    :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblBranch" runat="server" Font-Bold="True"></asp:Label>
                                                    </a>
                                                </li>
                                            </ul>
                                        </div>
                                        <div class="col-lg-6 col-md-6 col-12">
                                            <ul class="list-group list-group-unbordered">
                                                <li class="list-group-item"><b>PH :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblPH" runat="server" Font-Bold="true"></asp:Label></a>
                                                </li>
                                                <li class="list-group-item"><b>
                                                    <asp:Label ID="lblDYtxtScheme" runat="server" Font-Bold="True"></asp:Label>
                                                    :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblScheme" runat="server" Font-Bold="True"></asp:Label></a>
                                                </li>
                                                <li class="list-group-item"><b></b>
                                                    <a class="sub-label">
                                                        <asp:Image ID="imgPhoto" runat="server" Width="128px" Height="128px"
                                                            BorderColor="Black" BorderWidth="2px" />
                                                    </a>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>

                            <div class="col-12 mt-3">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--<label>Session</label>--%>
                                            <asp:Label ID="lblDYddlSession" runat="server" Font-Bold="True"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlSession_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                            ValidationGroup="Submit" Display="None" ErrorMessage="Please Select Session"
                                            InitialValue="0" SetFocusOnError="true" />
                                        <asp:HiddenField ID="hiddenfieldfromDt" runat="server" Visible="False" />
                                        <asp:HiddenField ID="hiddenFieldToDt" runat="server" Visible="False" />
                                        <asp:HiddenField ID="hiddenfieldfromDtBulk" runat="server" Visible="False" />
                                        <asp:HiddenField ID="hiddenFieldToDtBulk" runat="server" Visible="False" />
                                        <asp:RequiredFieldValidator ID="rfvSessionrpt" runat="server"
                                            ControlToValidate="ddlSession" Display="None"
                                            ErrorMessage="Please Select Session" InitialValue="0" SetFocusOnError="true"
                                            ValidationGroup="Report" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>OD Type</label>
                                        </div>
                                        <asp:DropDownList ID="ddlOdType" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlOdType_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlOdType" runat="server" ControlToValidate="ddlOdType"
                                            ValidationGroup="Submit" Display="None" ErrorMessage="Please Select OD Type"
                                            InitialValue="0" SetFocusOnError="true" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>OD Date</label>
                                        </div>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i id="txtFromDate1" runat="server" class="fa fa-calendar text-blue"></i>
                                            </div>
                                            <asp:TextBox ID="txtFromDate" runat="server" TabIndex="3" ValidationGroup="Submit"
                                                CssClass="form-control" AutoPostBack="true" OnTextChanged="txtFromDate_TextChanged" />
                                            <ajaxToolKit:CalendarExtender ID="ceFromDate" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtFromDate" PopupButtonID="txtFromDate1" OnClientDateSelectionChanged="selectfromdate" />

                                            <ajaxToolKit:MaskedEditExtender ID="meFromDate" runat="server" TargetControlID="txtFromDate"
                                                Mask="99/99/9999" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                                MaskType="Date" />
                                            <ajaxToolKit:MaskedEditValidator ID="mvFromDate" runat="server" EmptyValueMessage="Please Enter OD Date"
                                                ControlExtender="meFromDate" ControlToValidate="txtFromDate" IsValidEmpty="false"
                                                InvalidValueMessage=" Date is invalid" Display="None" ErrorMessage="Please Enter OD Date"
                                                InvalidValueBlurredMessage="*" ValidationGroup="Submit" SetFocusOnError="true" />
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="tdToDate" runat="server" visible="true">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>To Date</label>
                                        </div>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i id="txtToDate2" runat="server" class="fa fa-calendar text-blue"></i>
                                            </div>
                                            <asp:TextBox ID="txtToDate" runat="server" TabIndex="3" ValidationGroup="Submit"
                                                CssClass="form-control" onchange="checkDateJoining(this);" />
                                            <ajaxToolKit:CalendarExtender ID="ceToDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtToDate"
                                                PopupButtonID="txtToDate2" />
                                            <ajaxToolKit:MaskedEditExtender ID="meToDate" runat="server" TargetControlID="txtToDate"
                                                Mask="99/99/9999" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                                MaskType="Date" />
                                            <ajaxToolKit:MaskedEditValidator ID="mevToDate" runat="server" EmptyValueMessage="Please Enter To Date"
                                                ControlExtender="meToDate" ControlToValidate="txtToDate" IsValidEmpty="false"
                                                InvalidValueMessage="Date is invalid" Display="None" ErrorMessage="Please Enter To Date"
                                                InvalidValueBlurredMessage="*" ValidationGroup="Submit" SetFocusOnError="true" />
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>OD Leave Type</label>
                                        </div>
                                        <asp:DropDownList ID="ddlLeaveName" runat="server" AppendDataBoundItems="True" ValidationGroup="Submit"
                                            CssClass="form-control" data-select2-enable="true">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RFVddlLeaveName" runat="server" ControlToValidate="ddlLeaveName"
                                            ValidationGroup="Submit" Display="None" ErrorMessage="Please Select Leave Type"
                                            InitialValue="0" SetFocusOnError="true" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Reason</label>
                                        </div>
                                        <asp:TextBox ID="txtEventDetail" runat="server" CssClass="form-control" Style="resize: none" TabIndex="5" TextMode="MultiLine">
                                        </asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtEventDetail" runat="server"
                                            ControlToValidate="txtEventDetail" Display="None" ErrorMessage="Please Enter Reason" SetFocusOnError="true"
                                            ValidationGroup="Submit" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <%--  <sup>* </sup>--%>
                                            <label>Document Upload</label>
                                        </div>
                                        <asp:FileUpload ID="fuDoc" runat="server" Width="220px" />
                                        <asp:Label ID="lblFileName" runat="server" Visible="false"></asp:Label>
                                    </div>


                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>
                                                Slots (Select All
                                                    <asp:CheckBox ID="chkCheckAll" OnCheckedChanged="chkCheckAll_CheckedChanged" AutoPostBack="true" Visible="false" runat="server" />)</label>
                                            <asp:CheckBoxList ID="chkSlots" CssClass="col-lg-12" RepeatDirection="Horizontal" RepeatColumns="4" runat="server">
                                            </asp:CheckBoxList>
                                            </label>
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Day Name</label>
                                        </div>
                                        <asp:DropDownList ID="ddlDay" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                            TabIndex="10">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvDay" runat="server" ControlToValidate="ddlDay"
                                            ValidationGroup="Submit" Display="None" ErrorMessage="Please Select Day Name"
                                            SetFocusOnError="true" InitialValue="0" />
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:ValidationSummary ID="valSummery0" runat="server" DisplayMode="List"
                                    ShowMessageBox="true" ShowSummary="false" ValidationGroup="Submit" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                    ShowMessageBox="true" ShowSummary="false" ValidationGroup="Show" />
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="Submit"
                                    CssClass="btn btn-primary" OnClick="btnSubmit_Click" />

                                <asp:Button ID="btnReport" runat="server" Text="Report" CssClass="btn btn-info" Visible="false"
                                    OnClick="btnReport_Click" ValidationGroup="Report" />
                                <asp:Button ID="btnDeptwise" runat="server" Text="Branchwise Report" CssClass="btn btn-info" Visible="false"
                                    OnClick="btnDeptwise_Click" ValidationGroup="Report" />
                                <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="Report" />

                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" OnClick="btnCancel_Click" />
                            </div>

                            <div class="col-12">
                                <asp:ListView ID="lvExamday" runat="server" OnItemDataBound="lvExamday_ItemDataBound">
                                    <LayoutTemplate>
                                        <div class="sub-heading">
                                            <h5>APPLIED OD LIST </h5>
                                        </div>
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="divsessionlist">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>Edit
                                                    </th>
                                                    <th style="display: none;">Delete
                                                    </th>
                                                    <th>
                                                        <asp:Label ID="lblDYtxtRegNo" runat="server" Font-Bold="true"></asp:Label>
                                                    </th>
                                                    <th>Name
                                                    </th>
                                                    <th>OD Type
                                                    </th>
                                                    <th>OD Name
                                                    </th>
                                                    <th>OD Start Date
                                                    </th>
                                                    <th>OD End Date
                                                    </th>
                                                    <th>Download Document 
                                                    </th>
                                                    <th>Status
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
                                                <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" ImageUrl="../../Images/edit.png"
                                                    CommandArgument='<%# Eval("HOLIDAY_NO") %>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                    OnClick="btnEdit_Click" />
                                            </td>
                                            <td style="display: none">
                                                <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.gif" CommandArgument='<%# Eval("HOLIDAY_NO") %>'
                                                    ToolTip='<%# Eval("HOLIDAY_NO") %>' OnClientClick="return ConfirmSubmit();" OnClick="btnDelete_Click" />
                                            </td>
                                            <td>
                                                <%# Eval("REGNO")%>
                                            </td>
                                            <td>
                                                <%# Eval("STUDNAME")%>
                                            </td>
                                            <td>
                                                <%# Eval("OD_NAME")%>
                                                <asp:HiddenField ID="hdnODTYPE" runat="server" Value='<%# Eval("ODTYPE")%>' />
                                            </td>
                                            <td>
                                                <%# Eval("ACADEMIC_LEAVE_NAME")%>
                                            </td>
                                            <td>
                                                <%# Eval("ACADEMIC_HOLIDAY_STDATE")%>
                                            </td>
                                            <td>
                                                <%# Eval("ACADEMIC_HOLIDAY_ENDDATE")%>
                                            </td>
                                            <td>
                                                <asp:UpdatePanel ID="updnpfPreview" runat="server">
                                                    <ContentTemplate>
                                                        <asp:ImageButton ID="imgbtnpfPrevDoc" runat="server" CommandArgument='<%# Eval("FILENAME") %>' ImageUrl="~/images/downarrow.jpg" Text="Preview" ToolTip='<%# Eval("FILENAME") %>' Width="20px" Visible='<%# Convert.ToString(Eval("FILENAME"))==string.Empty?false:true %>' OnClick="imgbtnpfPrevDoc_Click"/>
                                                        <asp:Label ID="lblnpfPreview" Text='<%# Convert.ToString(Eval("FILENAME"))==string.Empty ?"Preview not available":"" %>' runat="server" Font-Bold="true" ForeColor="Red"></asp:Label>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                       <%-- <asp:AsyncPostBackTrigger ControlID="imgbtnpfPrevDoc" EventName="Click" />--%>
                                                        <asp:PostBackTrigger ControlID="imgbtnpfPrevDoc" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblAStatus" runat="server" Text='<%# Eval("APPROVAL_STATUS")%>'></asp:Label>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <AlternatingItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" ImageUrl="../../Images/edit.png"
                                                    CommandArgument='<%# Eval("HOLIDAY_NO") %>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                    OnClick="btnEdit_Click" />
                                            </td>
                                            <td style="display: none">
                                                <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.gif" CommandArgument='<%# Eval("HOLIDAY_NO") %>'
                                                    ToolTip='<%# Eval("HOLIDAY_NO") %>' OnClientClick="return ConfirmSubmit();" OnClick="btnDelete_Click" />
                                            </td>

                                            <td>
                                                <%# Eval("REGNO")%>
                                            </td>
                                            <td>
                                                <%# Eval("STUDNAME")%>
                                            </td>
                                            <td>
                                                <%# Eval("OD_NAME")%>
                                            </td>
                                            <td>
                                                <%# Eval("ACADEMIC_LEAVE_NAME")%>
                                            </td>
                                            <td>
                                                <%# Eval("ACADEMIC_HOLIDAY_STDATE")%>
                                            </td>
                                            <td>
                                                <%# Eval("ACADEMIC_HOLIDAY_ENDDATE")%>
                                            </td>
                                            <td>
                                                <asp:UpdatePanel ID="updnpfPreview1" runat="server">
                                                    <ContentTemplate>
                                                        <asp:ImageButton ID="imgbtnpfPrevDoc1" runat="server" CommandArgument='<%# Eval("FILENAME") %>'  ImageUrl="~/images/downarrow.jpg" Text="Preview" ToolTip='<%# Eval("FILENAME") %>' Width="20px" Visible='<%# Convert.ToString(Eval("FILENAME"))==string.Empty?false:true %>' OnClick="imgbtnpfPrevDoc_Click"/>
                                                        <asp:Label ID="lblnpfPreview1" Text='<%# Convert.ToString(Eval("FILENAME"))==string.Empty ?"Preview not available":"" %>' runat="server" Font-Bold="true" ForeColor="Red"></asp:Label>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                      <%--  <asp:AsyncPostBackTrigger ControlID="imgbtnpfPrevDoc1" EventName="Click" />--%>
                                                         <asp:PostBackTrigger ControlID="imgbtnpfPrevDoc1" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblAStatus" runat="server" Text='<%# Eval("APPROVAL_STATUS")%>'></asp:Label>
                                            </td>
                                        </tr>
                                    </AlternatingItemTemplate>
                                </asp:ListView>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
            <div id="divMsg" runat="Server">
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSubmit" />
            <asp:PostBackTrigger ControlID="lvExamday" />
        </Triggers>
    </asp:UpdatePanel>
    
    <ajaxToolKit:ModalPopupExtender ID="mpeViewDocument" BehaviorID="mpeViewDocument" runat="server" PopupControlID="pnlPopup"
        TargetControlID="lnkPreview" CancelControlID="btnClose" BackgroundCssClass="modalBackground"
        OnOkScript="ResetSession()">
    </ajaxToolKit:ModalPopupExtender>
        <asp:LinkButton ID="lnkPreview" runat="server" CommandArgument='<%# Eval("FILENAME") %>'></asp:LinkButton>

    <!-- The Modal -->
    <div class="modal fade" id="PassModel">
        <div class="modal-dialog">
            <div class="modal-content">

                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup">
                            <div class="modal-header">
                                <h4 class="modal-title">Document</h4>
                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                            </div>

                            <!-- Modal body -->
                            <div class="modal-body">
                                <div class="col-12">
                                    <iframe runat="server" width="420px" height="500px" id="iframeView"></iframe>
                                </div>
                            </div>

                            <!-- Modal footer -->
                            <div class="modal-footer d-none">
                                <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="btn btn-danger no" />
                            </div>
                        </asp:Panel>

                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>


    <script type="text/javascript">
        function selectfromdate(sender, args) {
        }

        function ConfirmSubmit() {
            var ret = confirm('Are you sure to delete this Leave entry?');
            if (ret == true)
                return true;
            else
                return false;
        }
    </script>

</asp:Content>
