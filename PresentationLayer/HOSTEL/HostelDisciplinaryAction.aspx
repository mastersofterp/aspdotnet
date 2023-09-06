<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="HostelDisciplinaryAction.aspx.cs" Inherits="HOSTEL_Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        function showConfirmDel(source) {
            this._source = source;
            this._popup = $find('mdlPopupDel');

            //  find the confirm ModalPopup and show it    
            this._popup.show();
        }

        function okDelClick() {
            //  find the confirm ModalPopup and hide it    
            this._popup.hide();
            //  use the cached button as the postback source
            __doPostBack(this._source.name, '');
        }

        function cancelDelClick() {
            //  find the confirm ModalPopup and hide it 
            this._popup.hide();
            //  clear the event source
            this._source = null;
            this._popup = null;
        }

    </script>
    <%--  Shrink the info panel out of view --%> <%--  Reset the sample so it can be played again --%>
    <ajaxToolKit:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mdlPopupDel" runat="server"
        TargetControlID="div" PopupControlID="div"
        OkControlID="btnOkDel" OnOkScript="okDelClick();"
        CancelControlID="btnNoDel" OnCancelScript="cancelDelClick();" BackgroundCssClass="modalBackground" />
    <asp:Panel ID="div" runat="server" Style="display: none" CssClass="modalPopup">
        <div style="text-align: center">
            <table>
                <tr>
                    <td align="center">
                        <asp:Image ID="imgWarning" runat="server" ImageUrl="~/images/warning.png" AlternateText="Warning" /></td>
                    <td>&nbsp;&nbsp;
                        <h6>Are you sure you want to Delete this Record ? </h6>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:Button ID="btnOkDel" runat="server" Text="Yes" CssClass="btn btn-primary" />
                        <asp:Button ID="btnNoDel" runat="server" Text="No" CssClass="btn btn-warning" />
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
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
                            <h3 class="box-title">Hostel Disciplinary Action </h3>
                        </div>
                        <div class="box-body">
                            <div id="divStudSearch" runat="server" class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divSearchCriteria" runat="server">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Search Criteria</label>
                                        </div>
                                        <%--onchange=" return ddlSearch_change();"--%>
                                        <asp:DropDownList runat="server" class="form-control" ID="ddlSearch" AutoPostBack="true" AppendDataBoundItems="true"
                                            data-select2-enable="true" OnSelectedIndexChanged="ddlSearch_SelectedIndexChanged">
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
                                                <asp:DropDownList runat="server" class="form-control" ID="ddlDropdown" AppendDataBoundItems="true" data-select2-enable="true" OnSelectedIndexChanged="ddlDropdown_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </asp:Panel>
                                    </div>
                                </div>
                                <div class="col-12 btn-footer" id="divBtnSearchRegion" runat="server">
                                    <%-- OnClientClick="return submitPopup(this.name);"--%>
                                    <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" CssClass="btn btn-primary" OnClientClick="return submitPopup(this.name);" />
                                    <asp:Button ID="btnReport" runat="server" Text="Report" OnClick="btnReport_Click" CssClass="btn btn-primary" />
                                    <asp:Button ID="btnClose" runat="server" Text="CANCEL" CssClass="btn btn-warning" OnClick="btnClose_Click" OnClientClick="return CloseSearchBox(this.name)" data-dismiss="modal" />
                                </div>
                                <div class="col-12 btn-footer">
                                    <asp:Label ID="lblNoRecords" runat="server" SkinID="lblmsg" />
                                </div>
                                <%-- LIST OF DISCIPLINRY ACTIONS STUDENT  START--%>
                                <asp:Panel ID="pnLDisciStudList" runat="server" Visible="false">
                                    <div class="col-12">
                                        <asp:Repeater ID="lvDisciplinary" runat="server">
                                            <HeaderTemplate>
                                                <div class="sub-heading">
                                                    <h5>List of Disciplinary Action Student </h5>
                                                </div>
                                                <table id="table2" class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <%-- <th>Edit
                                                </th>--%>
                                                            <th>Delete
                                                            </th>
                                                            <th>Student Name 
                                                            </th>
                                                            <th>Student Roll No.
                                                            </th>
                                                            <th>Hostel Name 
                                                            </th>
                                                            <th>Room Name</th>
                                                            <th>From Date </th>
                                                            <th>To Date</th>
                                                            <th>Remark</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>


                                                    <%-- <td>
                                            <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("DISCIPLINARY_ID") %>'
                                                AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" TabIndex="15" />&nbsp;
                                                
                                        </td>--%>
                                                    <td>
                                                        <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Images/delete.png" CommandArgument='<%# Eval("DISCIPLINARY_ID") %>'
                                                            AlternateText="Delete Record" ToolTip="Delete Record" OnClientClick="showConfirmDel(this); return false;" OnClick="btnDelete_Click" />
                                                    </td>
                                               
                                                    <td>
                                                       <%--<asp:LinkButton ID="lnkStuId" runat="server" Text='<%# Eval("STUDNAME") %>' CommandArgument='<%# Eval("IDNO") %>' OnClick="lnkId_Click"></asp:LinkButton>--%>
                                                        <%# Eval("STUDNAME")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("ROLLNO")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("HOSTEL_NAME")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("ROOM_NAME")%>
                                                    </td>
                                                    <td>
                                                         <%# DataBinder.Eval(Container.DataItem, "FROMDATE", "{0:d}")%>
                                                        <%--<%# Eval("FROMDATE")%>--%>
                                                    </td>
                                                    <td>
                                                         <%# DataBinder.Eval(Container.DataItem, "TODATE", "{0:d}")%>
                                                       <%-- <%# Eval("TODATE")%>--%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("REMARK")%>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </tbody></table>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </div>
                                </asp:Panel>
                                 <div class="fcol-12 btn-footer mt-3" id="div2" runat="server" a>
                                </div>
                                <%--END--%>
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
                                                        <%# Eval("SEMESTERNAME")%>
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
                                <%-- STUDENT SECTION START--%>
                                <div class="col-12" id="div_Studentdetail" runat="server" visible="false">
                                    <div class="row mb-3" id="divStudInfo" runat="server">
                                        <div class="col-12">
                                            <div class="sub-heading">
                                                <h5>Student Information</h5>
                                            </div>
                                        </div>
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
                                                <li class="list-group-item"><b>Hostel Name :</b>

                                                    <a class="sub-label">
                                                        <asp:Label ID="lblHostelName" runat="server" Font-Bold="True"></asp:Label></a>
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
                                                <li class="list-group-item"><b>Room Name :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblRoomName" runat="server" Font-Bold="True"></asp:Label></a>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12">
                                    <asp:Panel ID="pnlStudDisciSingle" runat="server" Visible="false">
                                        <asp:ListView ID="lvStudDisciAct" runat="server" >
                                            <LayoutTemplate>
                                                <div id="demo-grid">
                                                    <div class="sub-heading">
                                                        <h5>Disciplinary Action Details  </h5>
                                                    </div>
                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th>Edit</th>
                                                                <th>Student Name 
                                                                </th>
                                                                <th>Student Roll No.
                                                                </th>
                                                                <th>Hostel Name 
                                                                </th>
                                                                <th>Room Name</th>
                                                                <th>From Date </th>
                                                                <th>To Date</th>
                                                                <th>Remark</th>
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
                                                        <asp:ImageButton ID="btnStudDisciEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("DISCIPLINARY_ID") %>'
                                                            AlternateText="Edit Record" ToolTip="Edit Record" TabIndex="7" OnClick="btnStudDisciEdit_Click" />&nbsp;                            
                                                    </td>
                                                   <td>
                                                        <%# Eval("STUDNAME")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("ROLLNO")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("HOSTEL_NAME")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("ROOM_NAME")%>
                                                    </td>
                                                    <td>
                                                         <%# DataBinder.Eval(Container.DataItem, "FROMDATE", "{0:d}")%>
                                                        <%--<%# Eval("FROMDATE")%>--%>
                                                    </td>
                                                    <td>
                                                         <%# DataBinder.Eval(Container.DataItem, "TODATE", "{0:d}")%>
                                                        <%--<%# Eval("TODATE")%>--%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("REMARK")%>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </asp:Panel>
                                </div>
                            </div>
                            <%--END--%>
                        </div>
                        <div class="col-12 " runat="server" id="divDisciEntry" visible="false">
                            <%--<div class="col-12">--%>
                            <div class="row mb-3">
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Session No</label>
                                    </div>
                                    <asp:DropDownList ID="ddlSession" runat="server" TabIndex="1"
                                        AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true" />
                                    <asp:RequiredFieldValidator ID="valBlock" runat="server" ControlToValidate="ddlSession"
                                        Display="None" ErrorMessage="Please select Session " ValidationGroup="submit" SetFocusOnError="True"
                                        InitialValue="0" />
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Registration No.</label>
                                    </div>
                                    <asp:TextBox ID="txtRegistrationNo" runat="server" TabIndex="2" MaxLength="100" CssClass="form-control" onkeypress="return AlphabetsOnly(event,this);" />
                                    <asp:RequiredFieldValidator ID="rfvRegistrationname" runat="server" ControlToValidate="txtRegistrationNo"
                                        Display="None" ErrorMessage="Please enter Registration No." ValidationGroup="submit"
                                        SetFocusOnError="True" />
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Disciplinary From Date </label>
                                    </div>
                                    <div class="input-group">
                                        <div class="input-group-addon">
                                            <i class="fa fa-calendar"></i>
                                        </div>
                                        <asp:TextBox ID="txtFromDate" runat="server" TabIndex="3" CssClass="form-control"
                                            ToolTip="Enter From Date" AutoPostBack="true" OnTextChanged="txtFromDate_TextChanged" />
                                        <%--<asp:Image ID="imgFromDate" runat="server" ImageUrl="~/images/calendar.png" Width="16px" />--%>
                                        <ajaxToolKit:CalendarExtender ID="ceFromDate" runat="server" Format="dd/MM/yyyy"
                                            TargetControlID="txtFromDate" PopupButtonID="imgFromDate" Enabled="true" />
                                        <ajaxToolKit:MaskedEditExtender ID="meFromDate" runat="server" TargetControlID="txtFromDate"
                                            Mask="99/99/9999" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                            MaskType="Date" ErrorTooltipEnabled="false" />
                                        <ajaxToolKit:MaskedEditValidator ID="mvFromDate" runat="server" EmptyValueMessage="Please enter from date."
                                            ControlExtender="meFromDate" ControlToValidate="txtFromDate" IsValidEmpty="false"
                                            InvalidValueMessage="From Date is invalid" Display="None" TooltipMessage="Input a date"
                                            ErrorMessage="Please Select Date" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                            ValidationGroup="submit" SetFocusOnError="true" />
                                    </div>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Disciplinary To Date </label>
                                    </div>
                                    <div class="input-group">
                                        <div class="input-group-addon">
                                            <i class="fa fa-calendar"></i>
                                        </div>
                                        <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control" TabIndex="4"
                                            ToolTip="Enter To Date" AutoPostBack="true" OnTextChanged="txtToDate_TextChanged" />

                                        <%--<asp:Image ID="imgFromDate" runat="server" ImageUrl="~/images/calendar.png" Width="16px" />--%>
                                        <ajaxToolKit:CalendarExtender ID="ceToDate" runat="server" Format="dd/MM/yyyy"
                                            TargetControlID="txtToDate" PopupButtonID="imgFromDate" Enabled="true" />
                                        <ajaxToolKit:MaskedEditExtender ID="meToDate" runat="server" TargetControlID="txtToDate"
                                            Mask="99/99/9999" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                            MaskType="Date" ErrorTooltipEnabled="false" />
                                        <ajaxToolKit:MaskedEditValidator ID="mvToDate" runat="server" EmptyValueMessage="Please enter To date."
                                            ControlExtender="meToDate" ControlToValidate="txtToDate" IsValidEmpty="false"
                                            InvalidValueMessage="From Date is invalid" Display="None" TooltipMessage="Input a date"
                                            ErrorMessage="Please Select Date" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                            ValidationGroup="submit" SetFocusOnError="true" />
                                    </div>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Remark</label>
                                    </div>
                                    <asp:TextBox ID="txtRemark" runat="server" TabIndex="5" Rows="1" CssClass="form-control" Height="50px"
                                        MaxLength="150" TextMode="MultiLine" onkeypress="return AlphabetsOnly(event,this);" />
                                    <asp:RequiredFieldValidator ID="rfvRemark" runat="server" ControlToValidate="txtRemark"
                                        Display="None" ErrorMessage="Please enter Remark." ValidationGroup="submit"
                                        SetFocusOnError="True" />
                                </div>
                            </div>
                            <%--</div>--%>
                        </div>
                        <div class="col-12 btn-footer mt-3" runat="server" id="divBtnDisciEntry" visible="false">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="submit"
                                    TabIndex="11" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                                    TabIndex="12" CssClass="btn btn-warning" OnClick="btnCancel_Click" />
                                <asp:ValidationSummary ID="valSummary" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="submit" />
                            </div>
                    </div>
                </div>
            </div>
            <div id="divMsg" runat="server">
            </div>

        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnReport" />
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
