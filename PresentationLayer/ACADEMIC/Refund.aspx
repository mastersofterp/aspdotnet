<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    Title="" CodeFile="Refund.aspx.cs" Inherits="Academic_Refund" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        #ctl00_ContentPlaceHolder1_updExistingFees .dataTables_scrollHeadInner,
        #ctl00_ContentPlaceHolder1_updExistingFees .dataTables_scrollHeadInner,
        #ctl00_ContentPlaceHolder1_lvStudent_Panel2 .dataTables_scrollHeadInner,
        #ctl00_ContentPlaceHolder1_divAllCollections .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updRefund"
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
    <asp:UpdatePanel ID="updRefund" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <%-- <h3 class="box-title">REFUND</h3>--%>
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>

                        <div class="box-body">
                            <%--Search Pannel Added by Amit Bhumbur on date 13/01/22 --%>
                            <asp:UpdatePanel ID="updEdit" runat="server">
                                <ContentTemplate>
                                    <div class="col-12">
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
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
                                                            <sup>*</sup>
                                                            <label>Search String</label>
                                                        </div>
                                                        <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" onkeypress="return Validate()"></asp:TextBox>
                                                    </div>
                                                </asp:Panel>
                                                <asp:Panel ID="pnlDropdown" runat="server">
                                                    <div id="divDropDown" runat="server" style="display: block">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
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
                                            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" OnClientClick="return submitPopup(this.name);" CssClass="btn btn-primary" />
                                            <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="btn btn-warning" OnClick="btnClose_Click" data-dismiss="modal" />
                                        </div>
                                        <div class="col-12 btn-footer">
                                            <asp:Label ID="lblNoRecords" runat="server" SkinID="lblmsg" />
                                        </div>

                                        <div>
                                            <div class="col-12">
                                                <asp:Panel ID="pnlLV" runat="server">
                                                    <asp:ListView ID="lvStudent" runat="server">
                                                        <LayoutTemplate>
                                                            <div id="listViewGrid" class="vista-grid">
                                                                <div class="sub-heading">
                                                                    <h5>Student List</h5>
                                                                </div>
                                                                <asp:Panel ID="Panel2" runat="server">
                                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="tblStud">
                                                                        <thead class="bg-light-blue">
                                                                            <tr>
                                                                                <th>Sr.No.
                                                                                </th>
                                                                                <th>Name
                                                                                </th>
                                                                                <th style="display: none">IdNo
                                                                                </th>
                                                                                <th>
                                                                                    <asp:Label ID="lblDYRRNo" runat="server" Font-Bold="true"></asp:Label>
                                                                                </th>
                                                                                <th>Adm. Status
                                                                                </th>
                                                                                <th>
                                                                                    <asp:Label ID="lblDYtxtBranch" runat="server" Font-Bold="true"></asp:Label>
                                                                                </th>
                                                                                <th>
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
                                                                <td><%#Container.DataItemIndex+1 %></td>
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
                                                                    <asp:Label ID="lblAdmStatus" runat="server" Text='<%# Eval("ADMSTATUS")%>'></asp:Label>
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
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>

                            <div class="col-lg-12 col-md-12 col-12">
                                <a href="#" title="Search Student for Refund" onclick="ShowModalbox(); return false;"
                                    style="background-color: White"></a>
                                <b>
                                    <asp:Label ID="lblStudentCancel" runat="server" Font-Italic="True" Style="color: Red; text-decoration: blink; font-size: 100%; text-decoration: underline;">
                                    </asp:Label>
                                </b>
                            </div>

                            <div class="col-12 btn-footer">

                                <asp:ValidationSummary ID="valSummary3" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="studSearch" />
                            </div>

                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <div class="col-md-12 mb-3" id="divStudInfo" runat="server" style="display: none;">
                                        <div class="sub-heading">
                                            <h5>Student Information</h5>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-4 col-md-6 col-12">
                                                <ul class="list-group list-group-unbordered">
                                                    <li class="list-group-item"><b>Student's Name :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblStudName" CssClass="form-control" runat="server" />
                                                        </a>
                                                    </li>
                                                    <li class="list-group-item"><b>Univ. Reg. No. :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblRegNo" CssClass="form-control" runat="server" /></a>
                                                    </li>
                                                    <li class="list-group-item"><b>Degree :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblDegree" CssClass="form-control" runat="server" /></a>
                                                    </li>
                                                </ul>
                                            </div>

                                            <div class="col-lg-4 col-md-6 col-12">
                                                <ul class="list-group list-group-unbordered">
                                                    <li class="list-group-item"><b>Programme/Branch :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblBranch" CssClass="form-control" runat="server" />
                                                        </a>
                                                    </li>
                                                    <li class="list-group-item"><b>Year :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblYear" CssClass="form-control" runat="server" /></a>
                                                    </li>
                                                    <li class="list-group-item"><b>Semester :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblSemester" CssClass="form-control" runat="server" /></a>
                                                    </li>
                                                </ul>
                                            </div>

                                            <div class="col-lg-4 col-md-6 col-12">
                                                <ul class="list-group list-group-unbordered">
                                                    <li class="list-group-item"><b>Date of Admission :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblDateOfAdm" CssClass="form-control" runat="server" />
                                                        </a>
                                                    </li>
                                                    <li class="list-group-item"><b>Payment Type :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblPaymentType" CssClass="form-control" runat="server" /></a>
                                                    </li>
                                                    <li class="list-group-item"><b>Sex :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblSex" CssClass="form-control" runat="server" /></a>
                                                    </li>
                                                    <li class="list-group-item d-none"><b>Admission Batch :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblBatch" CssClass="form-control" runat="server" /></a>
                                                    </li>
                                                </ul>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>

                            <asp:UpdatePanel ID="updExistingFees" runat="server">
                                <ContentTemplate>
                                    <div id="divAllCollections" runat="server" visible="false" class="col-12">
                                        <asp:ListView ID="lvAllCollections" runat="server" OnItemDataBound="lvAllCollections_ItemDataBound" >
                                            <LayoutTemplate>
                                                <div id="listViewGrid">
                                                    <div class="sub-heading">
                                                        <h5>Existing Fee Collections</h5>
                                                    </div>
                                                    <table id="tblSearchResults" class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th>Action</th>
                                                                 <th>Print</th>
                                                                <th>Name
                                                                </th>
                                                                <th>
                                                                    <asp:Label ID="lblDYtxtRegNo" runat="server" Font-Bold="true"></asp:Label>
                                                                </th>
                                                                <th>
                                                                    <asp:Label ID="lblDYddlBranch" runat="server" Font-Bold="true"></asp:Label>
                                                                </th>
                                                                <th>
                                                                    <asp:Label ID="lblDYddlSemester_Tab" runat="server" Font-Bold="true"></asp:Label>
                                                                </th>
                                                                <th>Payment Cat.
                                                                </th>
                                                                <th>Rec. Type
                                                                </th>
                                                                <th>Rec. No.
                                                                </th>
                                                                <th>Rec. Amt.
                                                                </th>
                                                                <th>Refunded Amt.
                                                                </th>
                                                                <th>Refundable Amt.
                                                                </th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </LayoutTemplate>
                                            <EmptyDataTemplate>
                                            </EmptyDataTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <asp:ImageButton ID="btnRefund" runat="server" AlternateText="Refund" ImageUrl="~/Images/Refund.jpg"
                                                            OnClick="btnRefund_Click" CommandArgument='<%# Eval("DCR_NO") %>' CommandName='<%# Eval("IDNO") %>'
                                                            ToolTip='<%# Eval("REC_NO")%>' />
                                                        <asp:HiddenField ID="hdnidno" runat="server" Value='<%# Eval("IDNO") %>' />
                                                        <asp:HiddenField ID="hdndcr" runat="server" Value='<%# Eval("DCR_NO") %>' />
                                                        <%-- onmouseover="this.src='../images/Refund1.jpg'"
                                            onmouseout="this.src='../images/Refund.jpg'"--%>
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="btnprintrefund" runat="server" AlternateText="Print" ImageUrl="../Images/print.png"
                                    OnClick="btnprintrefund_Click" CommandArgument='<%# Eval("DCR_NO") %>' CommandName='<%# Eval("IDNO") %>'
                                                            ToolTip='<%# Eval("REC_NO")%>' />

                                                    </td>
                                                    <td>
                                                        <%# Eval("NAME")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("ENROLLNMENTNO")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("BRANCHSNAME")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("SEMESTER")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("PTYPENAME")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("RECIEPT_TITLE")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("REC_NO")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("DCR_AMT")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("REFUND_AMT")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("REFUNDABLE_AMT")%>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>

                            <asp:UpdatePanel ID="updVoucher" runat="server">
                                <ContentTemplate>
                                    <div id="divVoucherInfo" runat="server" style="display: none" class="col-md-12">
                                        <%--<div class="col-12">--%>
                                            <div class="row">
                                                <div class="col-12">
                                                    <div class="sub-heading">
                                                        <h5>Voucher Information</h5>
                                                    </div>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Voucher No.</label>
                                                    </div>
                                                    <asp:TextBox ID="txtVoucherNo" onkeydown="javascript:return false;" runat="server"
                                                        CssClass="form-control" />
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Voucher Date</label>
                                                    </div>
                                                    <div class="input-group">
                                                        <div class="input-group-addon" id="imgCalVoucherDate">
                                                            <i class="fa fa-calendar"></i>
                                                        </div>
                                                        <asp:TextBox ID="txtVoucherDate" CssClass="form-control" runat="server" TabIndex="4" />
                                                        <%--<asp:Image ID="imgCalVoucherDate" runat="server" src="../images/calendar.png" Style="cursor: hand" />--%>
                                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                                            TargetControlID="txtVoucherDate" PopupButtonID="imgCalVoucherDate" Enabled="true"
                                                            EnableViewState="true">
                                                        </ajaxToolKit:CalendarExtender>
                                                        <ajaxToolKit:MaskedEditExtender ID="meeVoucherDate" runat="server" TargetControlID="txtVoucherDate"
                                                            Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                            AcceptNegative="Left" ErrorTooltipEnabled="true" />
                                                        <asp:RequiredFieldValidator ID="valVoucherDate" ControlToValidate="txtVoucherDate"
                                                            runat="server" Display="None" ErrorMessage="Please enter voucher date." ValidationGroup="submit" />
                                                    </div>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic"> 
                                                        <label>Pay Type (C/D/O)</label>
                                                    </div>
                                                    <asp:TextBox ID="txtPayType" onchange="ValidatePayType(this);" runat="server" MaxLength="1"
                                                        TabIndex="4" ToolTip="Enter C for cash payment, D for payment by demand draft or Q for payment by cheque."
                                                        CssClass="form-control" />
                                                    <asp:RequiredFieldValidator ID="valPayType" runat="server" ControlToValidate="txtPayType"
                                                        Display="None" ErrorMessage="Please enter type of payment whether cash(C), demand draft(D) or Online(O)."
                                                        SetFocusOnError="true" ValidationGroup="submit" />
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Refundable Amount</label>
                                                    </div>
                                                    <asp:TextBox ID="txtReceiptAmount" onkeydown="javascript:return false;" runat="server"
                                                        CssClass="form-control" />
                                                </div>
                                            </div>
                                       <%-- </div>--%>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>

                            <asp:UpdatePanel ID="chqDD" runat="server">
                                <ContentTemplate>
                                    <div id="divChqDDInfo" runat="server" style="display: none" class="col-12">
                                        <div class="row">
                                            <div class="col-12">
                                                <div class="sub-heading">
                                                    <h5>Cheque/Demand Draft Details</h5>
                                                </div>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>No.</label>
                                                </div>
                                                <asp:TextBox ID="txtChqDDNo" runat="server" TabIndex="5" CssClass="form-control" />
                                                <asp:RequiredFieldValidator ID="valDDNo" ControlToValidate="txtChqDDNo" runat="server"
                                                    Display="None" ErrorMessage="Please enter cheque/demand draft no." ValidationGroup="dd_info" />
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Amount</label>
                                                </div>
                                                <asp:TextBox ID="txtChqDDAmount" onkeyup="IsNumeric(this);" runat="server" TabIndex="6"
                                                    CssClass="form-control" />
                                                <asp:RequiredFieldValidator ID="valDdAmount" ControlToValidate="txtChqDDAmount" runat="server"
                                                    Display="None" ErrorMessage="Please enter amount of cheque/demand draft." ValidationGroup="dd_info" />
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Date</label>
                                                </div>
                                                <div class="input-group">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <asp:TextBox ID="txtChqDDDate" runat="server" TabIndex="7" CssClass="form-control" />
                                                    <%--<asp:Image ID="imgCalDDDate" runat="server" src="../images/calendar.png" Style="cursor: hand" />--%>
                                                    <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                                        TargetControlID="txtChqDDDate" PopupButtonID="imgCalDDDate" Enabled="true" EnableViewState="true">
                                                    </ajaxToolKit:CalendarExtender>
                                                    <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtChqDDDate"
                                                        Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                        AcceptNegative="Left" ErrorTooltipEnabled="true" />
                                                    <asp:RequiredFieldValidator ID="valDdDate" ControlToValidate="txtChqDDDate" runat="server"
                                                        Display="None" ErrorMessage="Please enter cheque/demand draft date." ValidationGroup="dd_info" />
                                                </div>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    
                                                    <label>City</label>
                                                </div>
                                                <asp:TextBox ID="txtChqDDCity" runat="server" TabIndex="8" CssClass="form-control" />
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Bank</label>
                                                </div>
                                                <asp:DropDownList ID="ddlBank" AppendDataBoundItems="true" TabIndex="9" runat="server"
                                                    CssClass="form-control" data-select2-enable="true" />
                                                <asp:RequiredFieldValidator ID="valBankName" runat="server" ControlToValidate="ddlBank"
                                                    Display="None" ErrorMessage="Please select bank name." ValidationGroup="dd_info"
                                                    InitialValue="0" SetFocusOnError="true" />
                                            </div>
                                        </div>
                                        <div class="col-12 btn-footer">
                                            <asp:Button ID="btnSaveChqDD_Info" runat="server" Text="Save Demand Draft" OnClick="btnSaveChqDD_Info_Click"
                                                ValidationGroup="dd_info" TabIndex="10" CssClass="btn btn-primary" />
                                            <asp:ValidationSummary ID="valSummery2" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                ShowSummary="false" ValidationGroup="dd_info" />
                                        </div>


                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>

                            <asp:UpdatePanel ID="UpdOnline" runat="server">
                                <ContentTemplate>
                                    <div id="divOnline" runat="server" style="display: none" class="col-12">
                                        <div class="row">
                                            <div class="col-12">
                                                <div class="sub-heading">
                                                    <h5>Online/Other Fees Details</h5>
                                                </div>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Mode</label>
                                                </div>
                                                <asp:RadioButtonList ID="rdlOnlineMode" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="1" Text="NEFT &nbsp;&nbsp;&nbsp;&nbsp;"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="IMPS &nbsp;&nbsp;&nbsp;&nbsp;"></asp:ListItem>
                                                    <asp:ListItem Value="3" Text="UPI "></asp:ListItem>
                                                </asp:RadioButtonList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="rdlOnlineMode" runat="server"
                                                    Display="None" ErrorMessage="Please Select Mode." SetFocusOnError="true" 
                                                    ValidationGroup="online_info" />
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>UTR/Transaction No. </label>
                                                </div>
                                                <asp:TextBox ID="txttransactionno" runat="server" TabIndex="5" CssClass="form-control" MaxLength="40" />   
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txttransactionno" runat="server"
                                                    Display="None" ErrorMessage="Please Enter UTR/Transaction No." SetFocusOnError="true"
                                                    ValidationGroup="online_info" />
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Amount</label>
                                                </div>
                                                <asp:TextBox ID="txtOnlineAmount" onkeyup="IsNumeric(this);" runat="server" TabIndex="6"
                                                    CssClass="form-control" MaxLength="12" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtOnlineAmount" runat="server"
                                                    Display="None" ErrorMessage="Please Enter Amount." ValidationGroup="online_info" />
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" runat="server" TargetControlID="txtOnlineAmount" FilterType="Custom" FilterMode="ValidChars" ValidChars="0,1,2,3,4,5,6,7,8,9" />
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Date</label>
                                                </div>
                                                <div class="input-group">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <asp:TextBox ID="txtOnlineDate" runat="server" TabIndex="7" CssClass="form-control" />
                                                    <%--<asp:Image ID="imgCalDDDate" runat="server" src="../images/calendar.png" Style="cursor: hand" />--%>
                                                    <ajaxToolKit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy"
                                                        TargetControlID="txtOnlineDate" PopupButtonID="imgCalDDDate" Enabled="true" EnableViewState="true">
                                                    </ajaxToolKit:CalendarExtender>
                                                    <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txtOnlineDate"
                                                        Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                        AcceptNegative="Left" ErrorTooltipEnabled="true" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtOnlineDate" runat="server"
                                                        Display="None" ErrorMessage="Please Enter Date." ValidationGroup="online_info" />
                                                </div>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>City</label>
                                                </div>
                                                <asp:TextBox ID="txtcityOnline" runat="server" TabIndex="8" CssClass="form-control" MaxLength="50" />
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                                    TargetControlID="txtcityOnline" FilterType="Custom" FilterMode="InvalidChars"
                                                    InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'1,2,3,4,5,6,7,8,9,0" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="txtcityOnline" runat="server"
                                                    Display="None" ErrorMessage="Please Enter City." ValidationGroup="online_info" />
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Bank</label>
                                                </div>
                                                <asp:DropDownList ID="ddlbankonline" AppendDataBoundItems="true" TabIndex="9" runat="server"
                                                    CssClass="form-control" data-select2-enable="true" />
                                               <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlbankonline"
                                                                Display="None" ErrorMessage="Please Select Bank."
                                                                SetFocusOnError="true" ValidationGroup="online_info" InitialValue="0" />
                                            </div>
                                        </div>
                                        <div class="col-12 btn-footer">
                                            <asp:Button ID="btnsaveonline" runat="server" Text="Save Online Payment Details" OnClick="btnsaveonline_Click"
                                                TabIndex="10" CssClass="btn btn-primary" ValidationGroup="online_info" />
                                            <asp:ValidationSummary ID="valSummery1122" DisplayMode="List" runat="server" ShowMessageBox="true"
                                                ShowSummary="false" ValidationGroup="online_info" />
                                            
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>

                            <asp:UpdatePanel ID="updchqddDetail" runat="server">
                                <ContentTemplate>
                                    <div class="col-12">
                                    <asp:ListView ID="lvChqDdDetails" runat="server">
                                        <LayoutTemplate>
                                            <div id="divlvDemandDraftDetails1">
                                                <div class="sub-heading">
                                                    <h5>Cheque/Demand Draft Details</h5>
                                                </div>
                                                <table id="tblChqDD_Details" class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>Edit
                                                            </th>
                                                            <th>Delete
                                                            </th>
                                                            <th>No.
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
                                                    <asp:ImageButton ID="btnEditChqDDInfo" runat="server" OnClick="btnEditChqDDInfo_Click"
                                                        CommandArgument='<%# Eval("ChqDd_No") %>' ImageUrl="../Images/edit11.png" ToolTip="Edit Record" />
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="btnDeleteChqDDInfo" runat="server" OnClick="btnDeleteChqDDInfo_Click"
                                                        CommandArgument='<%# Eval("ChqDd_No") %>' ImageUrl="~/images/delete.gif" ToolTip="Delete Record" />
                                                </td>
                                                <td>
                                                    <%# Eval("ChqDd_No") %>
                                                </td>
                                                <td>
                                                    <%# (Eval("ChqDd_Date").ToString() != string.Empty) ? ((DateTime)Eval("ChqDd_Date")).ToShortDateString() : Eval("ChqDd_Date") %>
                                                </td>
                                                <td>
                                                    <%# Eval("ChqDd_City")%>
                                                </td>
                                                <td>
                                                    <%# Eval("ChqDd_BankName")%>
                                                </td>
                                                <td>
                                                    <%# Eval("ChqDd_Amount") %>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                        </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>


                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <div class="col-12">
                                    <asp:ListView ID="LvOnlinePayDetils" runat="server">
                                        <LayoutTemplate>
                                            <div id="divlvDemandDraftDetails1">
                                                <div class="sub-heading">
                                                    <h5>Online/Other payment Details</h5>
                                                </div>
                                                <table id="tblonline_details" class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>Edit
                                                            </th>
                                                            <th>Delete
                                                            </th>
                                                            <th>Transaction No.
                                                            </th>
                                                            <th>Mode
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
                                                    <asp:ImageButton ID="btnEditOnlineInfo" runat="server" OnClick="btnEditOnlineInfo_Click"
                                                        CommandArgument='<%# Eval("TrsancationID") %>' ImageUrl="../Images/edit11.png" ToolTip="Edit Record" />
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="btnDeleteOnlineInfo" runat="server" OnClick="btnDeleteOnlineInfo_Click"
                                                        CommandArgument='<%# Eval("TrsancationID") %>' ImageUrl="~/images/delete.gif" ToolTip="Delete Record" />
                                                </td>
                                                <td>
                                                    <%# Eval("TrsancationID") %>
                                                </td>
                                                <td>
                                                    <%# Eval("Mode") %>
                                                </td>

                                                <td>
                                                    <%# (Eval("Online_Date").ToString() != string.Empty) ? ((DateTime)Eval("Online_Date")).ToShortDateString() : Eval("Online_Date") %>
                                                </td>
                                                <td>
                                                    <%# Eval("Online_City")%>
                                                </td>
                                                <td>
                                                    <%# Eval("Online_BankName")%>
                                                </td>
                                                <td>
                                                    <%# Eval("Online_Amount") %>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                        </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>


                            <asp:UpdatePanel ID="FeeItem" runat="server">
                                <ContentTemplate>
                                    <div id="divFeeItems" style="display: block" visible="false" runat="server" class="col-12">

                                        <asp:ListView ID="lvFeeItems" runat="server">
                                            <LayoutTemplate>
                                                <div id="divlvFeeItems" class="vista-grid">
                                                    <div class="sub-heading">
                                                        <h5>Fee Items</h5>
                                                    </div>
                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="tblFeeItems">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th>Sr. No.</th>
                                                                <th>Fee Heads</th>
                                                                <th>Receipt Amount</th>
                                                                <th>Refund Amount</th>

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
                                                        <%#Container.DataItemIndex+1 %>
                                                        <asp:Label ID="lblFeeHeadSrNo" Visible="false" runat="server" Text='<%# Eval("SRNO") %>' /></td>

                                                    <td>
                                                        <%# Eval("FEE_LONGNAME")%>

                                                    </td>
                                                    <td>
                                                        <%# Eval("AMOUNT")%>

                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtFeeItemAmount" onblur="UpdateTotalRefundAmt(); ValidateRefundAmt(this);" TabIndex="11" onkeyup="IsNumeric(this);"
                                                            Style="text-align: right" runat="server" CssClass="form-control" />
                                                        <%-- ValidateRefundAmt(this);--%>
                                                        <asp:HiddenField ID="hidFeeItemAmount" runat="server" Value='<%# Eval("AMOUNT") %>' />
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="fetext1" runat="server" TargetControlID="txtFeeItemAmount" ValidChars="0123456789." FilterMode="ValidChars">
                                                        </ajaxToolKit:FilteredTextBoxExtender>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>

                                        <div class="row mt-4">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>Total Refund Amount</label>
                                                </div>
                                                <asp:TextBox ID="txtTotalRefundAmt" Text="" CssClass="form-control" runat="server"
                                                    onkeydown="javascript:return false;" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12" id="divCancellationCharges" runat="server">
                                                <div class="label-dynamic">
                                                    <label><span style="color: red">*</span> Admission Cancellation Charges</label>
                                                </div>
                                                <asp:TextBox ID="txtAdmCancelCharge" runat="server" MaxLength="5" autocomplete="off"
                                                    TabIndex="12" CssClass="form-control" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtAdmCancelCharge" runat="server"
                                                    Display="None" ErrorMessage="Please enter Admission Cancellation Charges." SetFocusOnError="true"
                                                    ValidationGroup="submit" />
                                                <ajaxToolKit:FilteredTextBoxExtender ID="fetext" runat="server" TargetControlID="txtAdmCancelCharge" ValidChars="0123456789." FilterMode="ValidChars">
                                                </ajaxToolKit:FilteredTextBoxExtender>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label><span style="color: red">*</span>Remark</label>
                                                </div>
                                                <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" Rows="4" MaxLength="400"
                                                    TabIndex="12" CssClass="form-control" />
                                                <asp:RequiredFieldValidator ID="valRemark" ControlToValidate="txtRemark" runat="server"
                                                    Display="None" ErrorMessage="Please enter reason for refund." SetFocusOnError="true"
                                                    ValidationGroup="submit" />
                                            </div>
                                        </div>
                                        <div class="col-12 btn-footer">
                                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click"
                                                TabIndex="13" Enabled="false" ValidationGroup="submit" CssClass="btn btn-primary" />
                                            <asp:Button ID="btnReport" runat="server" Text="Print Refund Receipt" CausesValidation="false"
                                                OnClick="btnReport_Click" TabIndex="15" Enabled="false" CssClass="btn btn-info" />
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                                                OnClick="btnCancel_Click" TabIndex="14" CssClass="btn btn-warning" />
                                            <asp:ValidationSummary ID="valSummery1" DisplayMode="List" runat="server" ShowMessageBox="true"
                                                ShowSummary="false" ValidationGroup="submit" />
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>


                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <!-- Modal -->





    <div id="divMsg" runat="server">
    </div>

    <script type="text/javascript" language="javascript">

        function ShowModalbox() {
            try {
                Modalbox.show($('divModalboxContent'), { title: 'Search Student for Refund', width: 600, overlayClose: false, slideDownDuration: 0.1, slideUpDuration: 0.1, afterLoad: SetFocus });
            }
            catch (e) {
                alert("Error: " + e.description);
            }
            return;
        }

        function SetFocus() {
            try {
                document.getElementById('<%= txtSearch.ClientID %>').focus();
            }
            catch (e) {
                alert("Error: " + e.description);
            }
        }
        //////////added by Amit B. on date 12/01/22


        $(document).ready(function () {
            debugger
            //$("#<%= pnltextbox.ClientID %>").hide();

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

            if ((char >= 65 && char <= 90) || (char >= 97 && char <= 122) || (char = 49)) {
                return true;
            }
            else {
                return false;
            }

        }
    }

    ///////////

    function ValidatePayType(txtPayType) {
        try {
            debugger;
            
            //alert("HI")
          // alert(txtPayType.value)
            //if (txtPayType != null && txtPayType.value != '') {
                if (txtPayType.value.toUpperCase() == 'D') {
                    txtPayType.value = txtPayType.value.toUpperCase();
                    if (document.getElementById('ctl00_ContentPlaceHolder1_divChqDDInfo') != null) {
                        document.getElementById('ctl00_ContentPlaceHolder1_divChqDDInfo').style.display = "block";
                        document.getElementById('ctl00_ContentPlaceHolder1_divOnline').style.display = "none";
                        document.getElementById('ctl00_ContentPlaceHolder1_txtChqDDNo').focus();
                    }
                }
                else if (txtPayType.value.toUpperCase() == 'C') {
                        txtPayType.value = "C";
                        if (document.getElementById('ctl00_ContentPlaceHolder1_divChqDDInfo') != null && document.getElementById('ctl00_ContentPlaceHolder1_divOnline') != null) {
                            document.getElementById('ctl00_ContentPlaceHolder1_divChqDDInfo').style.display = "none";
                            document.getElementById('ctl00_ContentPlaceHolder1_divOnline').style.display = "none";
                        }

                    }

                else if (txtPayType.value.toUpperCase() == 'O') {
                        txtPayType.value = "O";
                        if (document.getElementById('ctl00_ContentPlaceHolder1_divOnline') != null) {
                            document.getElementById('ctl00_ContentPlaceHolder1_divOnline').style.display = "block";
                            document.getElementById('ctl00_ContentPlaceHolder1_divChqDDInfo').style.display = "none";
                        }
                    }
                
                
            //}
            else {
                alert("Please enter C for cash payment, D for payment by demand draft or O for Online Payment");
                if (document.getElementById('ctl00_ContentPlaceHolder1_divOnline') != null)
                    document.getElementById('ctl00_ContentPlaceHolder1_divOnline').style.display = "none";
                document.getElementById('ctl00_ContentPlaceHolder1_divChqDDInfo').style.display = "none";


                txtPayType.value = "";
                txtPayType.focus();
            }
        }
        catch (e) {
            alert("Error: " + e.description);
        }
    }

    function UpdateTotalRefundAmt() {
        try {
            var totalAmt = 0.00;
            var dataRows = null;

            if (document.getElementById('tblFeeItems') != null)
                dataRows = document.getElementById('tblFeeItems').getElementsByTagName('tr');

            if (dataRows != null) {
                for (i = 1; i < dataRows.length; i++) {
                    var dataCellCollection = dataRows.item(i).getElementsByTagName('td');
                    var dataCell = dataCellCollection.item(3);
                    var controls = dataCell.getElementsByTagName('input');
                    var txtAmt = controls.item(0).value;
                    if (txtAmt.trim() != '')
                        totalAmt += parseFloat(txtAmt);

                }
                //if (document.getElementById('ctl00_ContentPlaceHolder1_txtTotalRefundAmt') != null)
                document.getElementById('ctl00_ContentPlaceHolder1_txtTotalRefundAmt').value = totalAmt;
                // var totalAmt = 0.00;
                //// var tbl = document.getElementById('tblFeeItems');
                // var tbl = document.getElementById('tblFeeItems');
                // if(tbl != null && tbl.rows && tbl.rows.length > 1)
                // {
                //     for(i = 1; i < tbl.rows.length; i++)
                //     {
                //         var dataRow = tbl.rows[i];
                //         var dataCell = dataRow.lastChild;
                //         var txtAmt = dataCell.childNodes[1].value;
                //         var amt = parseFloat((txtAmt.value.trim() != '')? txtAmt.value.trim(): 0);
                //         totalAmt += amt;
                //     }
                //     document.getElementById('ctl00_ContentPlaceHolder1_txtTotalRefundAmt').value = totalAmt;
            }
        }
        catch (e) {
            alert("Error: " + e.description);
        }
    }

    function ValidateRefundAmt(txtRefundAmt) {
        try {
            var refundAmt = parseFloat((txtRefundAmt.value.trim() != '') ? txtRefundAmt.value.trim() : 0);
            var hidOrigAmt = txtRefundAmt.nextSibling;
            var origAmt = parseFloat((hidOrigAmt.value != '') ? hidOrigAmt.value : 0);
            if (refundAmt > origAmt) {
                alert('Refund amount can not be greater than receipt amount.');
                txtRefundAmt.value = "";
                txtRefundAmt.focus();
            }
        }
        catch (e) {
            alert("Error: " + e.description);
        }
    }
    </script>
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

</asp:Content>
