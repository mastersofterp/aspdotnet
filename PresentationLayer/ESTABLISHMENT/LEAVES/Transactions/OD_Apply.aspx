<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="OD_Apply.aspx.cs" Inherits="ESTABLISHMENT_LEAVES_Transactions_OD_Apply" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">


        function parseDate(str) {
            var date = str.split('/');
            return new Date(date[2], date[1], date[0] - 1);
        }

        function GetDaysBetweenDates(date1, date2) {
            return (date2 - date1) / (1000 * 60 * 60 * 24)
        }


        function caldiff() {

            if ((document.getElementById('ctl00_ContentPlaceHolder1_txtFromdt').value != null) && (document.getElementById('ctl00_ContentPlaceHolder1_txtTodt').value != null)) {

                var d = GetDaysBetweenDates(parseDate(document.getElementById('ctl00_ContentPlaceHolder1_txtFromdt').value), parseDate(document.getElementById('ctl00_ContentPlaceHolder1_txtTodt').value));
                {
                    document.getElementById('ctl00_ContentPlaceHolder1_txtNodays').value = (parseInt(d) + 1);
                    if (document.getElementById('ctl00_ContentPlaceHolder1_txtNodays').value == "NaN") {
                        document.getElementById('ctl00_ContentPlaceHolder1_txtNodays').value = "";
                    }
                }

            }
            if (document.getElementById('ctl00_ContentPlaceHolder1_txtNodays').value <= 0) {
                alert("No. of Days can not be 0 or less than 0 ");
                document.getElementById('ctl00_ContentPlaceHolder1_txtNodays').value = "";
                document.getElementById('ctl00_ContentPlaceHolder1_txtTodt').focus();
            }
            if (parseInt(document.getElementById('ctl00_ContentPlaceHolder1_txtNodays').value) > parseInt(document.getElementById('ctl00_ContentPlaceHolder1_txtLeavebal').value)) {

                alert("No. of Days not more than Balance Days");
                document.getElementById('ctl00_ContentPlaceHolder1_txtNodays').value = "";
                document.getElementById('ctl00_ContentPlaceHolder1_txtTodt').focus();
            }
            return false;
        }

    </script>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">OD DUTY SLIP/APPLICATION ENTRY</h3>
                </div>
                <div class="box-body">
                    <div class="col-12">
                        <div class="row">
                            <div class="col-12">
                                <div class="sub-heading">
                                    <h5>OD Duty Slip/Application Entry</h5>
                                </div>
                            </div>
                        </div>
                    </div>

                    <%--<asp:Panel ID="pnllist" runat="server">--%>
                    <div class="col-12 btn-footer">
                        <asp:LinkButton ID="lnkNew" runat="server" OnClick="lnkNew_Click" Text="New OD Application" CssClass="btn btn-primary"
                            ToolTip="Click here for New OD Application" TabIndex="1"></asp:LinkButton>
                        <asp:LinkButton ID="lnkbut" runat="server" OnClick="lnkbut_Click" Visible="true" Text="OD Application Status"
                            CssClass="btn btn-primary" TabIndex="3"
                            ToolTip="Click here for OD Application Status"></asp:LinkButton>

                    </div>
                    <%-- </asp:Panel>--%>
                    <div class="col-12">
                        <%--<asp:Panel ID="pnlODInfo" runat="server" Visible="false">--%>
                        <asp:Panel ID="pnllist" runat="server" Visible="false">
                            <asp:ListView ID="lvODinfo" runat="server">
                                <EmptyDataTemplate>
                                    <%-- <asp:Label ID="lblErr" runat="server" Text="No On Duty Slip found">
                                                                </asp:Label>--%>
                                </EmptyDataTemplate>
                                <LayoutTemplate>
                                    <div class="sub-heading">
                                        <h5>Employee OD Record</h5>
                                    </div>
                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                        <thead class="bg-light-blue">
                                            <tr>
                                                <th>Action</th>
                                                <th>From Date
                                                </th>
                                                <th>To Date
                                                </th>
                                                <%--<th>Place
                                                    </th>--%>
                                                <th>Purpose
                                                </th>
                                                <%-- <th>Instructed By
                                                </th>--%>
                                                <th>Out Time
                                                </th>
                                                <th>IN Time
                                                </th>
                                                <th>Status
                                                </th>
                                                <%-- <th>Report
                                                </th>--%>
                                                <%--<th>OD Leave Type
                                                    </th>--%>
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
                                            <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png"
                                                CommandArgument='<%# Eval("ODTRNO") %>' ToolTip="Edit Record"
                                                AlternateText="Edit Record" OnClick="btnEdit_Click" Enabled="true" />&nbsp;
                                                                <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Images/delete.png"
                                                                    CommandArgument='<%# Eval("ODTRNO") %>'
                                                                    AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                                                    OnClientClick="showConfirmDel(this); return false;" Enabled="true" />

                                        </td>
                                        <td>
                                            <%# Eval("FROM_DATE", "{0:dd-MM-yyyy}")%>
                                        </td>
                                        <td>
                                            <%# Eval("TO_DATE", "{0:dd-MM-yyyy}")%>
                                        </td>
                                        <%-- <td>
                                                <%# Eval("Place") %>
                                            </td>--%>
                                        <td>
                                            <%-- <%# Eval("OD_PURPOSE")%>--%>
                                            <%# Eval("Purposenew")%>
                                        </td>
                                        <%--<td>
                                            <%# Eval("INSTRUCTED_BY")%>
                                        </td>--%>
                                        <td>
                                            <%# Eval("OUT_TIME") %>
                                        </td>
                                        <td>
                                            <%# Eval("IN_TIME") %>
                                        </td>
                                        <td>
                                            <%# Eval("Statusnew")%>
                                        </td>
                                        <%--<td>
                                            <asp:Button ID="btnReport" runat="server" Text="Report" CommandArgument='<%# Eval("ODTRNO")%>'
                                                ToolTip='<%# Eval("ODTRNO")%>' OnClick="btnReport_click" CssClass="btn btn-info"
                                                TabIndex="2" />
                                        </td>--%>
                                        <%--<td>
                                                <%# Eval("ODTYPE")%>
                                            </td>--%>
                                        <%--<td></td>--%>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </asp:Panel>
                      <%--  <div class="vista-grid_datapager d-none">
                            <div class="text-center">
                                <asp:DataPager ID="dpODinfo" runat="server" PagedControlID="lvODinfo" PageSize="10"
                                    OnPreRender="dpODinfo_PreRender">
                                    <Fields>
                                        <asp:NextPreviousPagerField FirstPageText="<<" PreviousPageText="<" ButtonType="Link"
                                            RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="true" ShowPreviousPageButton="true"
                                            ShowLastPageButton="false" ShowNextPageButton="false" />
                                        <asp:NumericPagerField ButtonType="Link" ButtonCount="7" CurrentPageLabelCssClass="Current" />
                                        <asp:NextPreviousPagerField LastPageText=">>" NextPageText=">" ButtonType="Link"
                                            RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="false" ShowPreviousPageButton="false"
                                            ShowLastPageButton="true" ShowNextPageButton="true" />
                                    </Fields>
                                </asp:DataPager>
                            </div>
                        </div>--%>
                    </div>



                    <%--  <asp:Panel ID="pnlODStatus" runat="server">
                        <div class="form-group col-12">
                            <asp:Panel ID="pnlODStatusList" runat="server">
                                <asp:ListView ID="lvStatus" runat="server">
                                    <EmptyDataTemplate>
                                        <asp:Label ID="ibler" runat="server" Text="No more Leave aaplication" CssClass="d-block text-center mt-3"></asp:Label>
                                    </EmptyDataTemplate>
                                    <LayoutTemplate>
                                        <div class="sub-heading">
                                            <h5>OD Application / Slip Status</h5>
                                        </div>
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>From Date
                                                    </th>
                                                    <th>To Date
                                                    </th>
                                                    <th>Place Of Visit
                                                    </th>
                                                    <th>Out Time
                                                    </th>
                                                    <th>IN Time
                                                    </th>
                                                    <th>Status
                                                    </th>
                                                    <th>OD Type
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
                                                <%# Eval("FROM_DATE", "{0:dd-MM-yyyy}")%>
                                            </td>
                                            <td>
                                                <%# Eval("TO_DATE", "{0:dd-MM-yyyy}")%>
                                            </td>
                                            <td>
                                                <%# Eval("Place")%>
                                            </td>
                                            <td>
                                                <%# Eval("OUT_TIME")%>
                                            </td>
                                            <td>
                                                <%# Eval("IN_TIME")%>
                                            </td>
                                            <td>
                                                <%# Eval("Status") %>
                                            </td>
                                            <td>
                                                <%# Eval("ODTYPE") %>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </asp:Panel>
                            <div class="vista-grid_datapager d-none">
                                <div class="text-center">
                                    <asp:DataPager ID="dpPager" runat="server" PagedControlID="lvStatus" PageSize="10"
                                        OnPreRender="dpPager_PreRender">
                                        <Fields>
                                            <asp:NextPreviousPagerField FirstPageText="<<" PreviousPageText="<" ButtonType="Link"
                                                RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="true" ShowPreviousPageButton="true"
                                                ShowLastPageButton="false" ShowNextPageButton="false" />
                                            <asp:NumericPagerField ButtonType="Link" ButtonCount="7" CurrentPageLabelCssClass="Current" />
                                            <asp:NextPreviousPagerField LastPageText=">>" NextPageText=">" ButtonType="Link"
                                                RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="false" ShowPreviousPageButton="false"
                                                ShowLastPageButton="true" ShowNextPageButton="true" />
                                        </Fields>
                                    </asp:DataPager>
                                </div>
                            </div>
                        </div>
                        <div class="col-12 btn-footer">
                            <asp:Button ID="btnHidePanel" runat="server" Text="Back" OnClick="btnHidePanel_Click" CssClass="btn btn-primary"
                                ToolTip="Click here to Go Back" TabIndex="4" />
                        </div>
                    </asp:Panel>--%>

                    <asp:Panel ID="pnlAdd" runat="server">
                        <%--<div class="panel panel-info">
                                                <div class="panel panel-heading">OD Slip/Application Entry</div>
                                                <div class="panel panel-body">--%>
                        <%--<legend class="legendPay">OD Slip/Application Entry</legend>--%>
                        <div class="col-12">
                            <div class="row">
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <label>OD Criteria</label>
                                    </div>
                                    <asp:RadioButtonList ID="rblODType" runat="server" AutoPostBack="true" TabIndex="5"
                                        RepeatDirection="Horizontal" ToolTip="Select OD Slip/OD Application"
                                        OnSelectedIndexChanged="rblODType_SelectedIndexChanged">
                                        <asp:ListItem Enabled="true" Selected="True" Text="OD Slip" Value="0"></asp:ListItem>
                                        <asp:ListItem Enabled="true" Text="OD Application" Value="1"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12" id="trEventRange" runat="server" visible="false">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Event Range From Date</label>
                                    </div>
                                    <div class="input-group date">
                                        <div class="input-group-addon">
                                            <i id="Image1" runat="server" class="fa fa-calendar text-blue"></i>
                                        </div>
                                        <asp:TextBox ID="txtEventFrm" runat="server" MaxLength="10" CssClass="form-control"
                                            ToolTip="Enter Event From Date" Style="z-index: 0;" TabIndex="6"
                                            AutoPostBack="true" OnTextChanged="txtEventFrm_TextChanged"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvNodays" runat="server"
                                            ControlToValidate="txtEventFrm" Display="None" SetFocusOnError="true"
                                            ErrorMessage="Please Enter Event From Date" ValidationGroup="Leaveapp">
                                        </asp:RequiredFieldValidator>
                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="true"
                                            EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="Image1"
                                            TargetControlID="txtEventFrm">
                                        </ajaxToolKit:CalendarExtender>
                                        <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender6" runat="server"
                                            AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="true"
                                            Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true"
                                            TargetControlID="txtEventFrm" />
                                    </div>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Event Range To Date</label>
                                    </div>
                                    <div class="input-group date">
                                        <div class="input-group-addon">
                                            <i id="Image2" runat="server" class="fa fa-calendar text-blue"></i>
                                        </div>
                                        <asp:TextBox ID="txtEventTo" runat="server" AutoPostBack="true" MaxLength="10" CssClass="form-control"
                                            ToolTip="Enter Event To Date" Style="z-index: 0;" TabIndex="7"
                                            OnTextChanged="txtEventTo_TextChanged" />
                                        <%--  <asp:RequiredFieldValidator ID="rfvToEve" runat="server"
                                            ControlToValidate="txtEventTo" Display="None" SetFocusOnError="true"
                                            ErrorMessage="Please Enter Event To Date" ValidationGroup="Leaveapp">
                                        </asp:RequiredFieldValidator>
                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="true"
                                            EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="Image2"
                                            TargetControlID="txtEventTo">
                                        </ajaxToolKit:CalendarExtender>
                                        <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender7" runat="server"
                                            AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="true"
                                            Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true"
                                            TargetControlID="txtEventTo" />--%>
                                    </div>
                                </div>
                                <div class="form-group col-lg-6 col-md-6 col-12" id="trfrmto" runat="server">
                                    <div class="row">
                                        <div class="form-group col-lg-6 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>From Date</label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i id="imgCalFromdt" runat="server" class="fa fa-calendar text-blue"></i>
                                                </div>

                                                <asp:TextBox ID="txtFromdt" runat="server" MaxLength="10" CssClass="form-control"
                                                    AutoPostBack="true" OnTextChanged="txtFromdt_TextChanged" TabIndex="8"
                                                    ToolTip="Enter From Date" Style="z-index: 0;"></asp:TextBox>
                                                <ajaxToolKit:CalendarExtender ID="cefromfate" runat="server" Enabled="true"
                                                    EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="imgCalFromdt"
                                                    TargetControlID="txtFromdt" PopupPosition="BottomLeft">
                                                </ajaxToolKit:CalendarExtender>
                                                <ajaxToolKit:MaskedEditExtender ID="mefromdate" runat="server"
                                                    AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="true"
                                                    Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true"
                                                    TargetControlID="txtFromdt" OnInvalidCssClass="errotdate" />
                                                <ajaxToolKit:MaskedEditValidator ID="mevfromdate" runat="server" ControlExtender="mefromdate" ControlToValidate="txtFromdt"
                                                    EmptyValueMessage="Please Enter From Date" InvalidValueMessage="From Date is Invalid (Enter dd/MM/yyyy Format)"
                                                    Display="None" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date" SetFocusOnError="True"
                                                    ValidationGroup="Leaveapp" IsValidEmpty="false"> </ajaxToolKit:MaskedEditValidator>
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-6 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>To Date</label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i id="imgCalTodt" runat="server" class="fa fa-calendar text-blue"></i>
                                                </div>

                                                <asp:TextBox ID="txtTodt" runat="server" AutoPostBack="true" MaxLength="10"
                                                    OnTextChanged="txtTodt_TextChanged" CssClass="form-control" TabIndex="9"
                                                    ToolTip="Enter To Date" Style="z-index: 0;"></asp:TextBox>
                                                <ajaxToolKit:CalendarExtender ID="CeTodt" runat="server" Enabled="true"
                                                    EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="imgCalTodt"
                                                    TargetControlID="txtTodt">
                                                </ajaxToolKit:CalendarExtender>
                                                <ajaxToolKit:MaskedEditExtender ID="meeTodt" runat="server"
                                                    AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="true"
                                                    Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true"
                                                    TargetControlID="txtTodt" />
                                                <ajaxToolKit:MaskedEditValidator ID="mevtodate" runat="server" ControlExtender="meeTodt" ControlToValidate="txtTodt"
                                                    EmptyValueMessage="Please Enter To Date" InvalidValueMessage="To Date is Invalid (Enter dd/MM/yyyy Format)"
                                                    Display="None" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date" SetFocusOnError="True"
                                                    ValidationGroup="Leaveapp" IsValidEmpty="false"> </ajaxToolKit:MaskedEditValidator>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <label>No. Of Days</label>
                                    </div>
                                    <asp:TextBox ID="txtNodays" runat="server" AutoPostBack="true" MaxLength="5" TabIndex="10"
                                        CssClass="form-control" ToolTip="Enter Number Of Days" Enabled="false" />
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>
                                            <asp:Label ID="lblJoinindt" runat="server" Text=""></asp:Label>
                                            <%--<asp:Label ID="lblJoinindt" runat="server" Text="" Font-Size="Large"> </asp:Label>--%></label>
                                    </div>
                                    <div class="input-group date">
                                        <div class="input-group-addon">
                                            <i id="imgCalJoindt" runat="server" class="fa fa-calendar text-blue"></i>
                                        </div>

                                        <asp:TextBox ID="txtJoindt" runat="server" MaxLength="10" CssClass="form-control" AutoPostBack="true"
                                            OnTextChanged="txtJoindt_TextChanged" ToolTip="Enter Event Date" TabIndex="11"
                                            Style="z-index: 0;" />
                                        <ajaxToolKit:CalendarExtender ID="CeJoindt" runat="server" Enabled="true"
                                            EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="imgCalJoindt"
                                            TargetControlID="txtJoindt">
                                        </ajaxToolKit:CalendarExtender>
                                        <ajaxToolKit:MaskedEditExtender ID="meeJoindt" runat="server"
                                            AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="true"
                                            Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true"
                                            TargetControlID="txtJoindt" />
                                        <asp:RequiredFieldValidator ID="rfvHolyType" runat="server" ControlToValidate="txtJoindt"
                                            Display="None" ErrorMessage="Please Enter joining Date" ValidationGroup="Leaveapp"
                                            SetFocusOnError="True">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group col-lg-6 col-md-6 col-12" id="trpurpose" runat="server">
                                    <div class="row">
                                        <div class="form-group col-lg-6 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Purpose</label>
                                            </div>
                                            <asp:DropDownList ID="ddlPurpose" AppendDataBoundItems="true" CssClass="form-control" ToolTip="Select Purpose"
                                                runat="server" TabIndex="12" data-select2-enable="true">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvPurpose" runat="server"
                                                ControlToValidate="ddlPurpose" Display="None" InitialValue="0"
                                                ErrorMessage="Please Select Purpose" ValidationGroup="Leaveapp">
                                            </asp:RequiredFieldValidator>
                                            <%-- <asp:TextBox ID="txtodpurpose" runat="server" ToolTip="Enter OD Purpose"
                                                CssClass="form-control" MaxLength="50" onkeypress="return CheckAlphabet(event,this);"></asp:TextBox>--%>
                                            <%-- <asp:RequiredFieldValidator ID="rfvodpurpose" runat="server" ControlToValidate="txtodpurpose"
                                                Display="None" ErrorMessage="Please Enter OD purpose" ValidationGroup="Leaveapp">
                                            </asp:RequiredFieldValidator>--%>
                                        </div>
                                        <div class="form-group col-lg-6 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Event Type</label>
                                            </div>
                                            <asp:DropDownList ID="ddlEventType" AppendDataBoundItems="true" CssClass="form-control"
                                                ToolTip="Select Event Type" runat="server" TabIndex="13" data-select2-enable="true">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvEvent" runat="server"
                                                ControlToValidate="ddlEventType" Display="None" InitialValue="0"
                                                ErrorMessage="Please Select Event Type" ValidationGroup="Leaveapp">
                                            </asp:RequiredFieldValidator>
                                            <%-- <asp:TextBox ID="txteventtype" runat="server" ToolTip="Enter Event Type"
                                                CssClass="form-control" MaxLength="50" onkeypress="return CheckAlphabet(event,this);"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvevent" runat="server" ControlToValidate="ddlEventType"
                                                Display="None" ErrorMessage="Please Enter Event type" ValidationGroup="Leaveapp">
                                            </asp:RequiredFieldValidator>--%>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group col-lg-6 col-md-6 col-12" id="trOrg" runat="server" visible="false">
                                    <div class="row">
                                        <div class="form-group col-lg-6 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Organised By</label>
                                            </div>
                                            <asp:TextBox ID="txtOrganised" runat="server" CssClass="form-control" MaxLength="1000" TabIndex="14"
                                                onkeypress="return CheckAlphabet(event,this);" ToolTip="Enter Organized By"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-lg-6 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Instructed By</label>
                                            </div>
                                            <asp:TextBox ID="txtInstruct" runat="server" CssClass="form-control" MaxLength="1000" TabIndex="15"
                                                onkeypress="return CheckAlphabet(event,this);" ToolTip="Enter Instructed By"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Place Of Visit</label>
                                    </div>
                                    <asp:TextBox ID="txtPlace" runat="server" CssClass="form-control" TextMode="MultiLine"
                                        ToolTip="Enter Place Of Visit" TabIndex="16" onkeypress="return CheckAlphabet(event,this);"></asp:TextBox>
                                  <asp:RequiredFieldValidator ID="rfvPlaceofVisit" runat="server"
                                     ControlToValidate="txtPlace" Display="None" 
                                     ErrorMessage="Please Enter Place of Visit" ValidationGroup="Leaveapp">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <label>Topic</label>
                                    </div>
                                    <asp:TextBox ID="txtTopic" runat="server" CssClass="form-control" MaxLength="1000" TextMode="MultiLine"
                                        onkeypress="return CheckAlphabet(event,this);" ToolTip="Enter Topic" TabIndex="17"></asp:TextBox>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12" id="trAmt" runat="server" visible="false">
                                    <div class="row">
                                        <div class="form-group col-lg-6 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Registration Amount</label>
                                            </div>
                                            <asp:TextBox ID="txtRegAmt" runat="server" CssClass="form-control" MaxLength="6"
                                                onkeypress="return CheckNumeric(event,this);" TabIndex="18"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-lg-6 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>TA & DA Amt Status</label>
                                            </div>
                                            <%--<asp:TextBox ID="txtTADA" runat="server" Width="300px" MaxLength="1000" ></asp:TextBox>--%>
                                            <asp:RadioButtonList ID="rblTA" runat="server" RepeatDirection="Horizontal" TabIndex="19"
                                                ToolTip="Select Yes or No">
                                                <asp:ListItem Enabled="true" Selected="True" Text="No" Value="0"></asp:ListItem>
                                                <asp:ListItem Enabled="true" Text="Yes" Value="1"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12" id="trcomment" runat="server" align="center" visible="false">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Press A or P to switch between AM and PM</label>
                                    </div>
                                </div>
                                <div class="form-group col-lg-6 col-md-6 col-12" id="trinout" runat="server">
                                    <div class="row">
                                        <div class="form-group col-lg-6 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Out Time</label>
                                            </div>
                                            <asp:TextBox ID="txtOutTime" CssClass="form-control" runat="server" TabIndex="20"
                                                ToolTip="Press A or P to switch between AM and PM "></asp:TextBox>
                                            <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" TargetControlID="txtOutTime"
                                                Mask="99:99" MaskType="Time" AcceptAMPM="True" ErrorTooltipEnabled="True"
                                                CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                CultureTimePlaceholder="" Enabled="True" />
                                            <%-- <asp:RequiredFieldValidator ID="rfvOut" runat="server" ControlToValidate="txtOutTime"
                                                            Display="None" ErrorMessage="Please Enter Out Time" ValidationGroup="Leaveapp"
                                                            SetFocusOnError="True">
                                                        </asp:RequiredFieldValidator>--%>
                                            <ajaxToolKit:MaskedEditValidator ID="mevEnterTimeTo" runat="server" EmptyValueMessage="Please Enter Out Time" ControlExtender="MaskedEditExtender3"
                                                ControlToValidate="txtOutTime" IsValidEmpty="false" ErrorMessage="Please  Enter Out Time" SetFocusOnError="True"
                                                InvalidValueMessage="Outtime is Invalid(Enter 12 Hours Format)" Display="None" TooltipMessage="Input a time" InvalidValueBlurredMessage="*"
                                                ValidationGroup="Leaveapp" />
                                        </div>
                                        <div class="form-group col-lg-6 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>In Time</label>
                                            </div>
                                            <asp:TextBox ID="txtInTime" CssClass="form-control" runat="server" TabIndex="21"
                                                ToolTip="Press A or P to switch between AM and PM "></asp:TextBox>
                                            <ajaxToolKit:MaskedEditExtender ID="meintime" runat="server" TargetControlID="txtInTime"
                                                Mask="99:99" MaskType="Time" AcceptAMPM="True" ErrorTooltipEnabled="True"
                                                CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                CultureTimePlaceholder="" Enabled="True" />
                                            <%--<asp:RequiredFieldValidator ID="rfvIn" runat="server" ControlToValidate="txtInTime"
                                                            Display="None" ErrorMessage="Please Enter In Time" ValidationGroup="Leaveapp"
                                                            SetFocusOnError="True">
                                                        </asp:RequiredFieldValidator>--%>
                                            <ajaxToolKit:MaskedEditValidator ID="mevintime" runat="server" EmptyValueMessage="Please Enter In Time" ControlExtender="meintime"
                                                ControlToValidate="txtInTime" IsValidEmpty="false" ErrorMessage="Please Enter In Time" SetFocusOnError="True"
                                                InvalidValueMessage="Intime is Invalid(Enter 12 Hours Format)" Display="None" TooltipMessage="Input a time" InvalidValueBlurredMessage="*"
                                                ValidationGroup="Leaveapp" />
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12" id="trout" runat="server" visible="false">
                                    <div class="row">
                                        <div class="form-group col-lg-6 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>In Time</label>
                                            </div>
                                            <asp:TextBox ID="txtIn" CssClass="form-control" runat="server" TabIndex="22"
                                                ToolTip="Press A or P to switch between AM and PM "></asp:TextBox>
                                            <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txtIn"
                                                Mask="99:99" MaskType="Time" AcceptAMPM="True" ErrorTooltipEnabled="True"
                                                CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                CultureTimePlaceholder="" Enabled="True" />
                                            <asp:RequiredFieldValidator ID="rfvo2" runat="server" ControlToValidate="txtIn"
                                                Display="None" ErrorMessage="Please Enter In Time" ValidationGroup="Leaveapp"
                                                SetFocusOnError="True">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-6 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Out Time</label>
                                            </div>
                                            <asp:TextBox ID="txtOut" runat="server" CssClass="form-control" TabIndex="23"
                                                ToolTip="Press A or P to switch between AM and PM "></asp:TextBox>
                                            <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender4" runat="server" TargetControlID="txtOut"
                                                Mask="99:99" MaskType="Time" AcceptAMPM="True" ErrorTooltipEnabled="True"
                                                CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                CultureTimePlaceholder="" Enabled="True" />
                                            <asp:RequiredFieldValidator ID="rfvi2" runat="server" ControlToValidate="txtOut"
                                                Display="None" ErrorMessage="Please Enter Out Time" ValidationGroup="Leaveapp"
                                                SetFocusOnError="True">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <label>Path</label>
                                    </div>
                                    <asp:TextBox ID="txtPath" runat="server" CssClass="form-control" ToolTip="Path"
                                        Enabled="false" TabIndex="24"></asp:TextBox>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <label>File Upload</label>
                                    </div>
                                    <asp:FileUpload ID="fuUploadImage" runat="server" />
                                </div>
                            </div>
                        </div>
                        <%--<div class="col-12 btn-footer">
                            <asp:Button ID="btnSave" runat="server" Text="Submit" ValidationGroup="Leaveapp" TabIndex="25"
                                CssClass="btn btn-primary" ToolTip="Click here to Submit" OnClick="btnSave_Click" />
                            <asp:Button ID="btnBack" runat="server" Text="Back" CausesValidation="false" TabIndex="26"
                                OnClick="btnBack_Click" CssClass="btn btn-primary" ToolTip="Click here to Go Back" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" TabIndex="27"
                                CssClass="btn btn-warning" ToolTip="Click here to Reset" OnClick="btnCancel_Click" />
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Leaveapp"
                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                        </div>--%>
                        <%--</div>
                                            </div>--%>
                    </asp:Panel>

                    <%--<asp:Panel ID="div" runat="server" Style="display: none" CssClass="modalPopup">
                        <div class="text-center">
                            <div class="modal-content">
                                <div class="modal-body">
                                    <asp:Image ID="imgWarning" runat="server" ImageUrl="~/images/warning.gif" />
                                    <td>&nbsp;&nbsp;Are you sure you want to delete this record..?</td>
                                    <div class="text-center">
                                        <asp:Button ID="btnOkDel" runat="server" Text="Yes" CssClass="btn-primary" />
                                        <asp:Button ID="btnNoDel" runat="server" Text="No" CssClass="btn-primary" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>--%>



                    <asp:Panel ID="pnlbtn" runat="server">
                        <div class="col-12 btn-footer mt-4">
                            <asp:Button ID="btnSave" runat="server" Text="Submit" ValidationGroup="Leaveapp" TabIndex="25"
                                CssClass="btn btn-primary" ToolTip="Click here to Submit" OnClick="btnSave_Click" />
                            <asp:Button ID="btnBack" runat="server" Text="Back" CausesValidation="false" TabIndex="26"
                                OnClick="btnBack_Click" CssClass="btn btn-primary" ToolTip="Click here to Go Back" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" TabIndex="27"
                                CssClass="btn btn-warning" ToolTip="Click here to Reset" OnClick="btnCancel_Click" />
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Leaveapp"
                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                        </div>
                    </asp:Panel>
                </div>
            </div>

        </div>
    </div>



    <script>


        function CheckNumeric(event, obj) {
            var k = (window.event) ? event.keyCode : event.which;
            //alert(k);
            if (k == 8 || k == 9 || k == 43 || k == 95 || k == 0) {
                obj.style.backgroundColor = "White";
                return true;
            }
            if (k > 45 && k < 58) {
                obj.style.backgroundColor = "White";
                return true;

            }
            else {
                alert('Please Enter numeric Value');
                obj.focus();
            }
            return false;
        }


        function CheckAlphabet(event, obj) {

            var k = (window.event) ? event.keyCode : event.which;
            if (k == 8 || k == 9 || k == 43 || k == 95 || k == 0 || k == 32 || k == 46 || k == 13) {
                obj.style.backgroundColor = "White";
                return true;

            }
            if (k >= 65 && k <= 90 || k >= 97 && k <= 122) {
                obj.style.backgroundColor = "White";
                return true;

            }
            else {
                alert('Please Enter Alphabets Only!');
                obj.focus();
            }
            return false;
        }
    </script>
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
                    <td>&nbsp;&nbsp;Are you sure you want to delete?
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:Button ID="btnOkDel" runat="server" Text="Yes" Width="50px" />
                        <asp:Button ID="btnNoDel" runat="server" Text="No" Width="50px" />
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
    <script type="text/javascript">
        //  keeps track of the delete button for the row
        //  that is going to be removed
        var _source;
        // keep track of the popup div
        var _popup;

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
    <div id="divMsg" runat="server">
    </div>
</asp:Content>
