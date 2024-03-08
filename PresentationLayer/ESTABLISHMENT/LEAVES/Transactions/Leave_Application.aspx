<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Leave_Application.aspx.cs" Inherits="ESTABLISHMENT_LEAVES_Transactions_Leave_Application" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<link href="../../../CSS/master.css" rel="stylesheet" type="text/css" />--%>
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
    <%-- <asp:UpdatePanel ID="UpdatePanLeave" runat="server">
        <ContentTemplate>--%>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">EMPLOYEE LEAVE CARD</h3>
                </div>

                <div class="box-body">
                    <asp:Panel ID="pnllist" runat="server">
                        <div class="col-12">
                            <div class="row">
                                <div class="col-12">
                                    <div class="sub-heading">
                                        <h5>Employee Leave Card List</h5>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <asp:Panel ID="pnlLeaveCard" runat="server">
                            <div class="col-12">
                                <asp:ListView ID="lvLeaveinfo" runat="server">
                                    <EmptyDataTemplate>
                                        <asp:Label ID="lblErr" runat="server" Text="" CssClass="d-block text-center mt-3">
                                        </asp:Label>
                                    </EmptyDataTemplate>
                                    <LayoutTemplate>
                                        <%--<h4 class="box-title">
                                                            Employee Leave Card
                                                        </h4>--%>
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>Leave Name
                                                    </th>
                                                    <th>Period
                                                    </th>
                                                    <th>Op. Bal.
                                                    </th>
                                                    <th>Credit
                                                    </th>
                                                    <th>Total
                                                    </th>
                                                    <th>Taken
                                                    </th>
                                                    <th>Bal.
                                                    </th>
                                                    <th>Apply
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
                                                <%# Eval("LEAVENAME") %>
                                            </td>
                                            <td>
                                                <%# Eval("PERIOD") %>
                                            </td>
                                            <td>
                                                <%# Eval("OB") %>
                                            </td>
                                            <td>
                                                <%# Eval("CR") %>
                                            </td>
                                            <td>
                                                <%# Eval("Total")%>
                                                <asp:HiddenField ID="hidTotal" Value='<%# Eval("Total") %>' runat="server" />
                                            </td>
                                            <td>
                                                <%# Eval("DR") %>
                                            </td>
                                            <td>
                                                <%# Eval("bal") %>
                                                <asp:HiddenField ID="hfbal" runat="server" Value='<%# Eval("bal") %>' />

                                            </td>
                                            <td>
                                                <asp:Button ID="btnApply" runat="server" Text="Apply" CommandName='<%# Eval("date") %>'
                                                    CommandArgument='<%# Eval("LNO_NEW") %>' CssClass="btn btn-primary" TabIndex="1"
                                                    ToolTip=' <%# Eval("YEAR") %>' OnClick="btnApply_Click" />
                                                <asp:HiddenField ID="hidlno" runat="server" Value='<%# Eval("LNO")%>' />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>
                        </asp:Panel>
                    </asp:Panel>
                    <div class="col-12 btn-footer">
                        <asp:LinkButton ID="lnkbut" runat="server" OnClick="lnkbut_Click" Text="Leave Application Status" CssClass="btn btn-primary"
                            ToolTip="Click here for Leave Application Status" TabIndex="2"></asp:LinkButton>
                        <asp:LinkButton ID="lnkRestrictedLeaves" runat="server" OnClick="lnkRestrictedLeaves_Click" Text="Restricted Holidays List" CssClass="btn btn-primary"
                            ToolTip="Click here for Leave Application Status" TabIndex="3" Style="display: none"></asp:LinkButton>
                    </div>
                    <asp:Panel ID="pnlLeaveStatus" runat="server">
                        <div class="col-12">
                            <asp:ListView ID="lvStatus" runat="server">
                                <EmptyDataTemplate>
                                    <asp:Label ID="ibler" runat="server" Text="No more Leave aaplication" CssClass="d-block text-center mt-3"></asp:Label>
                                </EmptyDataTemplate>
                                <LayoutTemplate>
                                    <div class="sub-heading">
                                        <h5>Leave Application Status</h5>
                                    </div>
                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                        <thead class="bg-light-blue">
                                            <tr>
                                                <th>Action</th>
                                                <th>Leave Name
                                                </th>
                                                <th>From Date
                                                </th>
                                                <th>Todate
                                                </th>
                                                <th>No of days
                                                </th>
                                                <th>Status
                                                </th>
                                                <th style="display: none">Application Report
                                                </th>
                                                <%-- <th>
                                                        Joining Report
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
                                            <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("LETRNO") %>'
                                                AlternateText="Edit Record" ToolTip='<%# Eval("LNO")%>' OnClick="btnEdit_Click" Enabled="true" />
                                            <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Images/delete.png" CommandArgument='<%# Eval("LETRNO") %>'
                                                AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                                OnClientClick="showConfirmDel(this); return false;" Enabled="true" />

                                        </td>
                                        <td>
                                            <%# Eval("LEAVENAME")%>
                                        </td>
                                        <td>
                                            <%# Eval("From_date")%>
                                        </td>
                                        <td>
                                            <%# Eval("To_date") %>
                                        </td>
                                        <td>
                                            <%# Eval("No_of_days")%>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                            <%--<%# Eval("Status") %>--%>
                                        </td>
                                        <td style="display: none">
                                            <asp:Button ID="btnRPT" runat="server" Text="Application Report" CommandArgument='<%# Eval("LNO")%>'
                                                ToolTip='<%# Eval("LETRNO")%>' OnClick="btnRPT_Click" CssClass="btn btn-info" TabIndex="4" />

                                        </td>
                                        <%-- <td>
                                                <asp:Button ID="btnLeaveRPT" runat="server" Text="Joining Report" CommandArgument='<%# Eval("LNO")%>'
                                                ToolTip='<%# Eval("LETRNO")%>' OnClick="btnLeaveRPT_Click"  />
                                                
                                            </td>--%>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                            <%-- <div class="vista-grid_datapager d-none">
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
                            </div>--%>
                        </div>
                        <div class="col-12 btn-footer">
                            <asp:Button ID="btnHidePanel" runat="server" Text="Back" CssClass="btn btn-primary" TabIndex="5" ToolTip="Click here to Go Back"
                                OnClick="btnHidePanel_Click" />
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="pnlLeaveRestrictHolidays" runat="server" Visible="false">
                        <asp:ListView ID="lvRestrictHolidays" runat="server">
                            <EmptyDataTemplate>
                                <asp:Label ID="ibler" runat="server" Text="No more Leave aaplication" CssClass="d-block text-center mt-3"></asp:Label>
                            </EmptyDataTemplate>
                            <LayoutTemplate>
                                <div class="sub-heading">
                                    <h5>Restricted Holidays</h5>
                                </div>
                                <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                    <thead class="bg-light-blue">
                                        <tr>
                                            <th></th>
                                            <th>Holiday Name
                                            </th>
                                            <th>DATE
                                            </th>
                                            <%-- <th>
                                                        Joining Report
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
                                        <%--<asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("LETRNO") %>'
                                                        AlternateText="Edit Record" ToolTip='<%# Eval("LNO")%>' OnClick="btnEdit_Click" Enabled="true" />&nbsp;
                                                    <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.gif" CommandArgument='<%# Eval("LETRNO") %>'
                                                        AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                                        OnClientClick="showConfirmDel(this); return false;" Enabled="true" />--%>

                                    </td>
                                    <td>
                                        <%# Eval("HOLIDAYNAME")%>
                                    </td>
                                    <td>
                                        <%# Eval("date")%>
                                    </td>

                                </tr>
                            </ItemTemplate>
                        </asp:ListView>
                        <%-- <div class="vista-grid_datapager d-none">
                            <div class="text-center">
                                <asp:DataPager ID="DataPager1" runat="server" PagedControlID="lvStatus" PageSize="10"
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
                        </div>--%>
                        <div class="col-12 btn-footer">
                            <asp:Button ID="Button1" runat="server" Text="Back" CssClass="btn btn-primary" ToolTip="Click here to Go Back" TabIndex="6"
                                OnClick="btnHidePanel_Click" />
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="pnlAdd" runat="server">
                        <div class="col-12">
                            <div class="row">
                                <div class="col-12">
                                    <div class="sub-heading">
                                        <h5>Add/Edit Employee Leave</h5>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-12">
                            <asp:CheckBox ID="chklvapply" runat="server" ToolTip="Check To Apply Leave"
                                Text="CHECK IF YOU STILL WANTS TO APPLY A LEAVE. IT WILL BE CONSIDERED AS LWP"
                                ForeColor="#FF3300" Font-Bold="True" TabIndex="7" />
                        </div>
                        <div class="col-12">
                            <div class="row">
                                <div class="form-group col-lg-6 col-md-12 col-12" id="trotherlvs" runat="server">
                                    <div class="row">
                                        <div class="form-group col-lg-6 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Leave Type</label>
                                            </div>
                                            <asp:RadioButtonList ID="rblleavetype" runat="server" AutoPostBack="true"
                                                RepeatDirection="Horizontal" TabIndex="8" ToolTip="Select Full Day or Half Day"
                                                OnSelectedIndexChanged="rblleavetype_SelectedIndexChanged">
                                                <asp:ListItem Enabled="true" Selected="True" Text="Full Day" Value="0"></asp:ListItem>
                                                <asp:ListItem Enabled="true" Text="Half Day" Value="1"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                        <div class="form-group col-lg-6 col-md-6 col-12" id="trhalfDay" runat="server">
                                            <div class="label-dynamic">
                                                <label>Half Day Criteria</label>
                                            </div>
                                            <asp:DropDownList ID="ddlLeaveFNAN" runat="server" AppendDataBoundItems="true" AutoPostBack="True" TabIndex="9"
                                                OnSelectedIndexChanged="ddlLeaveFNAN_SelectedIndexChanged" Enabled="false" CssClass="form-control" ToolTip="Select AN/Fn">
                                                <asp:ListItem Value="0">Second Half</asp:ListItem>
                                                <%--AN--%>
                                                <asp:ListItem Value="1">First Half</asp:ListItem>
                                                <%--FN--%>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12" id="trml" runat="server" visible="false">
                                    <div class="label-dynamic">
                                        <label>Medical Leave</label>
                                    </div>
                                    <asp:RadioButtonList ID="rdbml" runat="server" AutoPostBack="true"
                                        RepeatDirection="Horizontal" TabIndex="10" ToolTip="Select Full Pay or Half Pay"
                                        OnSelectedIndexChanged="rdbml_SelectedIndexChanged">
                                        <asp:ListItem Enabled="true" Selected="True" Text="Full Pay" Value="0"></asp:ListItem>
                                        <asp:ListItem Enabled="true" Text="Half Pay" Value="1"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <label>Leave Name</label>
                                    </div>
                                    <%--<label>Leave Name :</label>--%>
                                    <asp:TextBox ID="txtLeavename" runat="server" CssClass="form-control" ToolTip="Leave Name"
                                        ReadOnly="true" TabIndex="11">
                                    </asp:TextBox>
                                    <%-- <div class="form-group col-md-12">
                                            <asp:Label ID="txtLeavename"  runat="server" CssClass="form-control" ToolTip="Leave Name"></asp:Label>
                                                  </div>--%>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <label>Bal.Leave</label>
                                    </div>
                                    <asp:TextBox ID="txtLeavebal" runat="server" CssClass="form-control" ToolTip="Balance Leave"
                                        ReadOnly="true" TabIndex="12" />
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>From Date</label>
                                    </div>
                                    <div class="input-group date">
                                        <div class="input-group-addon">
                                            <i id="imgCalFromdt" runat="server" class="fa fa-calendar text-blue"></i>
                                        </div>
                                        <asp:TextBox ID="txtFromdt" runat="server" MaxLength="10" CssClass="form-control" ToolTip="Enter Leave From Date"
                                            Style="z-index: 0;" TabIndex="13" AutoPostBack="true" OnTextChanged="txtFromdt_TextChanged" />
                                        <%--onchange="Cleardate()"--%>
                                        <asp:RequiredFieldValidator ID="rfvFromdt" runat="server"
                                            ControlToValidate="txtFromdt" Display="None"
                                            ErrorMessage="Please Enter From Date" SetFocusOnError="true"
                                            ValidationGroup="Leaveapp"></asp:RequiredFieldValidator>
                                        <ajaxToolKit:CalendarExtender ID="ceFromdt" runat="server" Enabled="true"
                                            EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="imgCalFromdt"
                                            TargetControlID="txtFromdt">
                                        </ajaxToolKit:CalendarExtender>
                                        <ajaxToolKit:MaskedEditExtender ID="meeFromdt" runat="server"
                                            AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="true"
                                            Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true"
                                            TargetControlID="txtFromdt" />
                                        <ajaxToolKit:MaskedEditValidator ID="mevFromdt" runat="server"
                                            ControlExtender="meeFromdt" ControlToValidate="txtFromdt" Display="None"
                                            EmptyValueBlurredText="Empty" EmptyValueMessage="Please Enter From Date"
                                            InvalidValueBlurredMessage="Invalid Date"
                                            InvalidValueMessage="From Date is Invalid (Enter dd/MM/yyyy Format)"
                                            SetFocusOnError="true" TooltipMessage="Please Enter From Date"
                                            ValidationGroup="Leaveapp">
                                                &#160;&#160;
                                        </ajaxToolKit:MaskedEditValidator>
                                    </div>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>To Date</label>
                                    </div>
                                    <div class="input-group date">
                                        <div class="input-group-addon">
                                            <i id="imgCalTodt" runat="server" class="fa fa-calendar text-blue"></i>
                                        </div>
                                        <asp:TextBox ID="txtTodt" runat="server" AutoPostBack="true" MaxLength="10" Style="z-index: 0;" TabIndex="14"
                                            OnTextChanged="txtTodt_TextChanged" CssClass="form-control" ToolTip="Enter Leave To Date" />
                                        <asp:RequiredFieldValidator ID="rfvTodt" runat="server"
                                            ControlToValidate="txtTodt" Display="None" ErrorMessage="Please Enter To Date"
                                            SetFocusOnError="true" ValidationGroup="Leaveapp"></asp:RequiredFieldValidator>
                                        <ajaxToolKit:CalendarExtender ID="CeTodt" runat="server" Enabled="true" EnableViewState="true"
                                            Format="dd/MM/yyyy" PopupButtonID="imgCalTodt" TargetControlID="txtTodt">
                                        </ajaxToolKit:CalendarExtender>
                                        <ajaxToolKit:MaskedEditExtender ID="meeTodt" runat="server" AcceptNegative="Left" DisplayMoney="Left"
                                            ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true"
                                            TargetControlID="txtTodt" />
                                        <ajaxToolKit:MaskedEditValidator ID="mevTodt" runat="server" ControlExtender="meeTodt" ControlToValidate="txtTodt"
                                            Display="None" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                            InvalidValueMessage="To Date is Invalid (Enter dd/MM/yyyy Format)" SetFocusOnError="true"
                                            TooltipMessage="Please Enter To Date" ValidationGroup="Leaveapp">                                                    
                                                    &#160;&#160;
                                        </ajaxToolKit:MaskedEditValidator>
                                    </div>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <label>No. of Days</label>
                                    </div>
                                    <asp:TextBox ID="txtNodays" runat="server" MaxLength="5" CssClass="form-control" ToolTip="Enter Number Of Days"
                                        AutoPostBack="true" OnTextChanged="txtNodays_TextChanged" ReadOnly="true" TabIndex="15" />
                                    <%--<asp:CheckBox ID="chklvapply" runat="server" 
                                                        Text = "Check If You Still Wants To Apply a Leave" ForeColor="#FF3300" />--%>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <label>Joining Date</label>
                                    </div>
                                    <asp:TextBox ID="txtJoindt" runat="server" MaxLength="10" CssClass="form-control"
                                        ToolTip="Enter Joining Date" Style="z-index: 0;" TabIndex="16" ReadOnly="true" />
                                    <%--<ajaxToolKit:CalendarExtender ID="CeJoindt" runat="server" Enabled="true"
                                                        EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="imgCalJoindt"
                                                        TargetControlID="txtJoindt">
                                                    </ajaxToolKit:CalendarExtender>
                                                    <ajaxToolKit:MaskedEditExtender ID="meeJoindt" runat="server"
                                                        AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="true"
                                                        Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true"
                                                        TargetControlID="txtJoindt" />
                                                    <ajaxToolKit:MaskedEditValidator ID="mevJoindt" runat="server"
                                                        ControlExtender="meeJoindt" ControlToValidate="txtJoindt" Display="None"
                                                        EmptyValueBlurredText="Empty" EmptyValueMessage="Please Enter Joining Date"
                                                        InvalidValueBlurredMessage="Invalid Date"
                                                        InvalidValueMessage="Joining Date is Invalid (Enter dd/MM/yyyy Format)"
                                                        SetFocusOnError="true" TooltipMessage="Please Enter Joining Date"
                                                        ValidationGroup="Leaveapp">
                                                    &#160;&#160;
                                                    </ajaxToolKit:MaskedEditValidator>
                                                </div>--%>
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="trhalfjoinDay">
                                    <div class="label-dynamic">
                                        <label>Joining Criteria</label>
                                    </div>
                                    <asp:DropDownList ID="ddlNoon" runat="server" AppendDataBoundItems="true" Enabled="false"
                                        CssClass="form-control" ToolTip="Select AN/FN" TabIndex="17">
                                        <asp:ListItem Value="0">First Half</asp:ListItem>
                                        <%--FN--%>
                                        <asp:ListItem Value="1">Second Half</asp:ListItem>
                                        <%--AN--%>  <%--data-select2-enable="true"--%>
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                    <div class="label-dynamic">
                                        <label>Type</label>
                                    </div>
                                    <asp:Label ID="txtLeaveType" runat="server" Text="" CssClass="form-control" ToolTip="Leave Type" TabIndex="18"></asp:Label>
                                    <%--<asp:TextBox ID="txtLeaveType" runat="server" CssClass="form-control"
                                        ToolTip="Leave Type" TabIndex="18" Enabled="false"/>--%>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Reason</label>
                                    </div>
                                    <asp:TextBox ID="txtReason" runat="server" CssClass="form-control" TextMode="MultiLine"
                                        ToolTip="Enter Reason" TabIndex="19"  MaxLength="199" onkeyDown="checkTextAreaMaxLength(this,event,'199');" onkeyup="textCounter(this, this.form.remLen, 199);"/>
                                    <asp:RequiredFieldValidator ID="rfvReason" runat="server"
                                        ControlToValidate="txtReason" Display="None" ErrorMessage="Please Enter Reason"
                                        ValidationGroup="Leaveapp"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <label>Address During Leave</label>
                                    </div>
                                    <asp:TextBox ID="txtadd" runat="server" CssClass="form-control" TextMode="MultiLine"
                                        ToolTip="Enter Address During Leave" TabIndex="20" MaxLength="170" 
                                        onkeyDown="checkTextAreaMaxLength(this,event,'170');" onkeyup="textCounter(this, this.form.remLen, 170);"/>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <label>Mobile No.</label>
                                    </div>
                                    <asp:Label ID="lblmobile" runat="server" CssClass="form-control"
                                        ToolTip="Mobile Number" TabIndex="21" />
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <label>Email Id</label>
                                    </div>
                                    <asp:Label ID="lblemail" runat="server"
                                        ToolTip="Email Id" TabIndex="22" />
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Alternate Arrangement</label>
                                    </div>
                                    <asp:TextBox ID="txtcharge" runat="server" CssClass="form-control" ToolTip="Enter Charge Handed Over To"
                                        TextMode="MultiLine" TabIndex="23" MaxLength="90"
                                        onkeyDown="checkTextAreaMaxLength(this,event,'90');" onkeyup="textCounter(this, this.form.remLen, 90);"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvcharge" runat="server" ControlToValidate="txtcharge"
                                        Display="None" ErrorMessage="Please Enter Alternate Arrangement" ValidationGroup="Leaveapp">
                                    </asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <label>Path</label>
                                    </div>
                                    <asp:DropDownList ID="ddlPath" runat="server" Enabled="false" Visible="false" ToolTip="Select Path" data-select2-enable="true"
                                        CssClass="form-control" TabIndex="24">
                                    </asp:DropDownList>
                                    <asp:TextBox ID="txtPath" runat="server" Enabled="false" CssClass="form-control"
                                        ToolTip="Path" TabIndex="25" TextMode="MultiLine"></asp:TextBox>
                                </div>
                                <div class="form-group col-md-12">
                                    <asp:Label ID="lblvalid" runat="server" Text=""></asp:Label>
                                </div>
                            </div>
                        </div>


                        <div class="col-md-12">
                            <asp:Panel ID="pnlEngaged" runat="server" Visible="false">
                                <%--<div class="col-12">
                                <div class="row">
                                    <div class="col-12">--%>
                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="sub-heading">
                                                <h5>Class Arrangement Information</h5>
                                            </div>
                                        </div>
                                    </div>
                                </div>


                                <div class="panel panel-body">
                                    <%-- </div>--%>
                                    <div class="col-12">
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Date : <span style="color: #FF0000"></span></label>
                                                </div>
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <asp:Image ID="imgCalLectdt" runat="server" ImageUrl="~/images/calendar.png"
                                                            Style="cursor: pointer" />
                                                    </div>
                                                    <asp:TextBox ID="txtEngagedDate" runat="server" MaxLength="10" Style="z-index: 0;" TabIndex="26"
                                                        CssClass="form-control" ToolTip="Enter Class Arrangement Date" />
                                                    <%-- <asp:RequiredFieldValidator ID="rfvEngdate" runat="server"
                                                        ControlToValidate="txtEngagedDate" Display="None" ErrorMessage="Please Enter To Date"
                                                        SetFocusOnError="true" ValidationGroup="Leaveadd"></asp:RequiredFieldValidator>--%>

                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                        ControlToValidate="txtEngagedDate" Display="None"
                                                        ErrorMessage="Please Enter Class Arrangement Date" SetFocusOnError="true"
                                                        ValidationGroup="Leaveadd"></asp:RequiredFieldValidator>


                                                    <ajaxToolKit:CalendarExtender ID="CeLectdt" runat="server" Enabled="true" EnableViewState="true"
                                                        Format="dd/MM/yyyy" PopupButtonID="imgCalLectdt" TargetControlID="txtEngagedDate">
                                                    </ajaxToolKit:CalendarExtender>
                                                    <ajaxToolKit:MaskedEditExtender ID="meeLectdt" runat="server" AcceptNegative="Left" DisplayMoney="Left"
                                                        ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true"
                                                        TargetControlID="txtEngagedDate" />
                                                    <ajaxToolKit:MaskedEditValidator ID="mevLectdt1" runat="server" ControlExtender="meeLectdt" ControlToValidate="txtEngagedDate"
                                                        Display="None" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                                        InvalidValueMessage="To Date is Invalid (Enter dd/MM/yyyy Format)" SetFocusOnError="true"
                                                        ErrorMessage="Please Enter Class arrangement Date"
                                                        TooltipMessage="Please Enter Class arrangement  Date">          <%--ValidationGroup="Leaveapp" --%>                                         
                                                                        &#160;&#160;
                                                    </ajaxToolKit:MaskedEditValidator>
                                                </div>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Time :</label>
                                                </div>
                                                <asp:TextBox ID="txtTime" runat="server" TabIndex="27" MaxLength="3" onkeypress="return CheckNumeric(event,this);"
                                                    CssClass="form-control" ToolTip="Enter Time" autocomplete="off" />
                                                <asp:RequiredFieldValidator ID="rfvTime" runat="server" ControlToValidate="txtTime"
                                                    Display="None" ErrorMessage="Please Enter Time. " ValidationGroup="Leaveadd"></asp:RequiredFieldValidator>
                                                <ajaxToolKit:MaskedEditExtender ID="meeTime" runat="server" TargetControlID="txtTime"
                                                    ClearMaskOnLostFocus="false" MaskType="Time" Mask="99:99" DisplayMoney="None" />
                                                <ajaxToolKit:MaskedEditValidator ID="mevtime" ControlToValidate="txtTime" ControlExtender="meeTime"
                                                    SetFocusOnError="true" MaximumValue="23:59:59" runat="server"
                                                    ErrorMessage="Please Enter Valid Time" MinimumValue="00:00" InvalidValueBlurredMessage="Please Enter Valid Time" ValidationGroup="Leaveadd" />
                                            </div>

                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="shift"
                                                                ControlToValidate="txtSunIn" ErrorMessage="Please Enter the Time" Display="None" />
                                                            <ajaxToolKit:MaskedEditExtender ID="meeSunIn" runat="server" TargetControlID="txtSunIn"
                                                                ClearMaskOnLostFocus="false" MaskType="Time" Mask="99:99:99" />
                                                            <ajaxToolKit:MaskedEditValidator ID="mevSunIn" ControlToValidate="txtSunIn" ControlExtender="meeSunIn"
                                                                SetFocusOnError="true" ValidationGroup="shift" MaximumValue="23:59:59" runat="server"
                                                                ErrorMessage="Invalid Time" MinimumValue="00:00:00" InvalidValueBlurredMessage="Invalid Time" />--%>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Class/Department :</label>
                                                </div>
                                                <asp:TextBox ID="txtyear" runat="server" TabIndex="28" MaxLength="30" CssClass="form-control" ToolTip="Enter Class/Department"
                                                    autocomplete="off" />
                                                <asp:RequiredFieldValidator ID="rfvyear" runat="server" ControlToValidate="txtyear"
                                                    Display="None" ErrorMessage="Please Enter Class/Department. " ValidationGroup="Leaveadd"
                                                    SetFocusOnError="True"> 
                                                </asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Subjects/Work :</label>
                                                </div>
                                                <asp:TextBox ID="txtsubject" runat="server" TabIndex="29" MaxLength="50" CssClass="form-control" ToolTip="Enter Subject/Work"
                                                    autocomplete="off" />
                                                <asp:RequiredFieldValidator ID="rfvminEncashdays" runat="server" ControlToValidate="txtsubject"
                                                    Display="None" ErrorMessage="Please Enter Subjects/Work. "
                                                    SetFocusOnError="True" ValidationGroup="Leaveadd">
                                                </asp:RequiredFieldValidator>
                                            </div>


                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Faculty Name </label>
                                                </div>
                                                <asp:DropDownList ID="ddlChargeHanded" AppendDataBoundItems="true" TabIndex="30" data-select2-enable="true" runat="server" CssClass="form-control">
                                                    <asp:ListItem Value="0" Text="Please Select"></asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvFaculty" runat="server" ControlToValidate="ddlChargeHanded" Display="None"
                                                    ErrorMessage="Please Select Faculty Name. " ValidationGroup="Leaveadd" SetFocusOnError="true" InitialValue="0"></asp:RequiredFieldValidator>
                                            </div>

                                            <%--<div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Faculty Designation: <span style="color: #FF0000"></span></label>
                                        </div>
                                        <asp:TextBox ID="txtdesig" runat="server" TabIndex="7" MaxLength="25"
                                            CssClass="form-control" ToolTip="Enter Designation" autocomplete="off"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                            ControlToValidate="txtdesig" Display="None"
                                            ErrorMessage="Please Enter Designation." SetFocusOnError="true"
                                            ValidationGroup="Leaveadd"></asp:RequiredFieldValidator>

                                    </div>--%>
                                            <div class="form-group col-md-12">
                                                <p class="text-center">
                                                    <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-primary" TabIndex="31"
                                                        Text="ADD" ValidationGroup="Leaveadd" Visible="true" ToolTip="Click here to add" OnClick="btnAdd_Click"
                                                        Width="80px" />

                                                    <asp:ValidationSummary ID="validationsummaryAdd" runat="server" ValidationGroup="Leaveadd"
                                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                                </p>

                                            </div>

                                            <div class="col-12">
                                                <asp:Panel ID="PnlAddEngaged" runat="server" ScrollBars="Auto">
                                                    <asp:ListView ID="lvEngagedInfo" runat="server">
                                                        <EmptyDataTemplate>
                                                            <asp:Label ID="lblErr" runat="server" Text="" CssClass="d-block text-center mt-3">
                                                            </asp:Label>
                                                        </EmptyDataTemplate>
                                                        <LayoutTemplate>
                                                            <div class="sub-heading">
                                                                <h5>Class Arrangement Details</h5>
                                                            </div>

                                                            <table class="table table-striped table-bordered nowrap" style="width: 100%">
                                                                <thead class="bg-light-blue">
                                                                    <tr>
                                                                        <th style="width: 10%">Action
                                                                        </th>
                                                                        <th>Date
                                                                        </th>
                                                                        <th>Time.
                                                                        </th>
                                                                        <th>Class/Department
                                                                        </th>
                                                                        <th>Subject/Work
                                                                        </th>
                                                                        <%-- <th>T/P
                                                                        </th>--%>
                                                                        <th>Engaged By
                                                                        </th>
                                                                        <%--<th>Designation
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
                                                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit1.png"
                                                                        CommandArgument='<%#Eval("DATE", "{0:yyyy/MM/dd}")+ ";" +Eval("TIME")+ ";" +Eval("YEAR_SEM")+ ";" +Eval("SUBJECT")+";"+Eval("FACULTY_NAME")%>'
                                                                        AlternateText="Edit Record" ToolTip='<%# Eval("SEQNO")%>' Enabled="true" OnClick="btnEditEng_Click" />&nbsp;
                                                                    <%--OnClick="btnEditEng_Click"--%>
                                                                    <asp:ImageButton ID="btnDeleteLecture" runat="server" AlternateText="Delete Record" CommandArgument='<%# Eval("SEQNO") %>'
                                                                        ImageUrl="~/images/delete.png" ToolTip='<%# Eval("SEQNO") %>' OnClick="btnDeleteLecture_Click" />
                                                                    <%--OnClick="btnDeleteLecture_Click"--%> 
                                                                </td>
                                                                <td>
                                                                    <%--Text='<%# Bind("DateOfBirth", "{0:MMM dd, yyyy}") %>'--%>
                                                                    <%--<asp:TextBox ID="txtdt" runat="server" CssClass="form-control" Text='<%# Eval("DATE", "{0:dd/MM/yyyy}")%>' ReadOnly="true" />--%>
                                                                    <%# Eval("DATE", "{0:dd/MM/yyyy}")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("TIME")%>
                                                                    <%--<asp:TextBox ID="txtperiodno" runat="server" CssClass="form-control" Text='<%# Eval("TIME")%>' ReadOnly="true" />--%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("YEAR_SEM")%>
                                                                    <%--<asp:TextBox ID="txtyearsem" runat="server" CssClass="form-control" Text='<%# Eval("YEAR_SEM")%>' ReadOnly="true" />--%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("SUBJECT")%>
                                                                    <%--<asp:TextBox ID="txtsub" runat="server" CssClass="form-control" Text='<%# Eval("SUBJECT")%>' ReadOnly="true" />--%>
                                                                </td>
                                                                <%-- <td>
                                                                    <asp:TextBox ID="txttheoryprac" runat="server" CssClass="form-control" Text='<%# Eval("THEORY_PRACTICAL")%>' ReadOnly="true" />
                                                                </td>--%>
                                                                <td>
                                                                    <%-- <asp:TextBox ID="txtfaculty" runat="server" CssClass="form-control" Text='<%# Eval("FACULTY_NAME")%>' ReadOnly="true" />--%>
                                                                    <%# Eval("FACULTY_NAME")%>
                                                                </td>
                                                                <%-- <td>
                                                            <asp:TextBox ID="txtDesignation" runat="server" CssClass="form-control" Text='<%# Eval("DESIGNATION")%>' ReadOnly="true" />
                                                        </td>--%>
                                                            </tr>

                                                        </ItemTemplate>

                                                    </asp:ListView>
                                                    <div id="DivEng" visible="false" runat="server">
                                                        <div class="form-group col-sm-12">
                                                            <div class="text-center">
                                                                <p style="color: Red; font-weight: bold">
                                                                    No Record Found..!!                                                                
                                                                </p>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>

                        <div class="col-md-12" runat="server" id="CertificateUpload">
                            <asp:Panel ID="PanelCertificate" runat="server" Visible="false">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="sub-heading">
                                                <h5>Upload File</h5>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="panel panel-body">
                                    <%--<div class="form-group col-md-6">--%>
                                    <div class="12">
                                        <%--<div class="form-group col-md-4">
                                                                            <label>Certificates Required:</label>
                                                                        </div>--%>
                                        <div class="form-group col-lg-6 col-md-12 col-12" id="divIsCertificate" runat="server">
                                            <div class="row">
                                                <div class="form-group col-lg-6 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Certificates Required</label>
                                                    </div>
                                                    <asp:RadioButtonList ID="rblIsCertificate" runat="server" AutoPostBack="true"
                                                        RepeatDirection="Horizontal" TabIndex="32" ToolTip="Select Certificate" OnSelectedIndexChanged="rblIsCertificate_SelectedIndexChanged">
                                                        <asp:ListItem Enabled="true" Selected="True" Text="With Certificate" Value="0"></asp:ListItem>
                                                        <%--<asp:ListItem Enabled="true" Text="Without Certificate" Value="1"></asp:ListItem>--%>
                                                    </asp:RadioButtonList>
                                                </div>
                                                <div class="form-group col-lg-6 col-md-6 col-12" id="divCer" runat="server">
                                                    <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                                        <div class="label-dynamic">
                                                            <label>Certificates</label>
                                                        </div>
                                                        <asp:CheckBox ID="chkUFit" runat="Server" Text="Unfit Certificate" TabIndex="33" ToolTip="Check If Unfit Certificate" />
                                                        &nbsp;
                                                                                         <asp:CheckBox ID="ChkFit" runat="Server" Text="Fit Certificate" TabIndex="34" ToolTip="Check If Fit Certificate" />
                                                    </div>


                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>File Upload</label>
                                                        </div>
                                                        <asp:FileUpload ID="fuUploadImage" runat="server" />
                                                        <%--<asp:RequiredFieldValidator ID="rfvFileupload" ValidationGroup="Leaveapp" runat="server"
                                                    ControlToValidate="fuUploadImage" ErrorMessage="Please Upload File"></asp:RequiredFieldValidator>--%>
                                                    </div>

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <%--</div>--%>
                                </div>

                            </asp:Panel>
                        </div>

                        <%--</div>
                                    </div>--%>
                        <%--</div>--%>


                        <%--</asp:Panel>--%>
                        <%--</div>--%>
                        <%-- </asp:Panel>--%>

                        <div class="col-12" style="margin-top: 10px;">
                            <%--<asp:UpdatePanel ID="updDocument" runat="server">
                                <ContentTemplate>--%>
                                    <asp:Panel ID="pnlFacLoad" runat="server">
                                        <div class="col-12">
                                            <asp:ListView ID="lvFacLoad" runat="server" OnItemDataBound="lvFacLoad_ItemDataBound">
                                                <EmptyDataTemplate>
                                                    <asp:Label ID="ibler" runat="server" Text="No More Employee Load List" CssClass="d-block text-center mt-3"></asp:Label>
                                                </EmptyDataTemplate>
                                                <LayoutTemplate>
                                                    <div class="sub-heading">
                                                        <h5>Employee Load List</h5>
                                                    </div>
                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th>Date
                                                                </th>
                                                                <th>Class
                                                                </th>
                                                                <th>Course code - Course Name
                                                                </th>
                                                                <th>Timing Slot
                                                                </th>
                                                                <th>Faculty
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
                                                            <asp:Label ID="lbldate" runat="server" Text='<%# Eval("TIMETABLE_DATE", "{0:dd/MM/yyyy}")%>'
                                                                TabIndex="17"></asp:Label>
                                                            <%--<%# Eval("TIMETABLE_DATE")%>--%>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblClass" runat="server" Text='<%# Eval("CLASS")%>'
                                                                TabIndex="17"></asp:Label>
                                                            <%--<%# Eval("CLASS")%>--%>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSE_NAME")%>'
                                                                TabIndex="17"></asp:Label>
                                                            <%--<%# Eval("COURSE_NAME") %>--%>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblSlotName" runat="server" Text='<%# Eval("SLOTNAME")%>'
                                                                TabIndex="17"></asp:Label>
                                                            <%--<%# Eval("SLOTNAME")%>--%>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlFac" runat="server" AppendDataBoundItems="true"
                                                                 ToolTip="Select Employee"  CssClass="form-control" data-select2-enable="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                           <%-- <asp:HiddenField ID="hdnSrno" Value='<%# Eval("SrNo")%>' runat="server" />--%>
                                                            <asp:HiddenField ID="hdnidno" runat="server" Value='<%#Eval("FACULTY")%>' />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </div>
                                    </asp:Panel>
                                <%--</ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="lvFacLoad" />
                                </Triggers>

                            </asp:UpdatePanel>--%>
                        </div>

                        <div class="col-12 btn-footer">
                            <asp:Button ID="btnSave" runat="server" Text="Submit" ValidationGroup="Leaveapp" TabIndex="35"
                                CssClass="btn btn-primary" ToolTip="Click here to Submit" OnClick="btnSave_Click" />
                            <asp:Button ID="btnBack" runat="server" Text="Back" CausesValidation="false" TabIndex="36"
                                CssClass="btn btn-primary" ToolTip="Click here to Go Back" OnClick="btnBack_Click" />
                            <asp:Button ID="btnreport" runat="server" Text="Report" CausesValidation="false" TabIndex="37"
                                CssClass="btn btn-info" ToolTip="Click here to Show Report" OnClick="btnreport_Click" Visible="False" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" TabIndex="38"
                                CssClass="btn btn-warning" ToolTip="Click here to Reset" OnClick="btnCancel_Click" />
                            <%--<asp:Button ID="btnReport2" runat="server" Text="Report2" CausesValidation="false" 
                                        Width="80px" onclick="btnreport2_Click" />   --%>
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Leaveapp"
                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                        </div>
                    </asp:Panel>
                </div>
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
            </div>
        </div>
    </div>
    <%--</div>--%>
    <div id="divMsg" runat="server"></div>
    <%-- </ContentTemplate>

    </asp:UpdatePanel>--%>
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
</asp:Content>
