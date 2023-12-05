<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="ordergenerate.aspx.cs" Inherits="ESTABLISHMENT_LEAVES_Transactions_ordergenerate"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
    <script language="javascript" type="text/javascript">


        function parseDate(str) {
            var date = str.split('/');
            return new Date(date[2], date[1], date[0] - 1);
        }

        function GetDaysBetweenDates(date1, date2) {
            return (date2 - date1) / (1000 * 60 * 60 * 24)
        }


        //function caldiff()
        //    {  
        //    
        //       
        //        if((document.getElementById('ctl00_ContentPlaceHolder1_txtFromdt').value !="") && (document.getElementById('ctl00_ContentPlaceHolder1_txtTodt').value != "")) 
        //        {
        //            
        //            var d= GetDaysBetweenDates(parseDate(document.getElementById('ctl00_ContentPlaceHolder1_txtFromdt').value), parseDate(document.getElementById('ctl00_ContentPlaceHolder1_txtTodt').value)) ;
        //            {  
        //                document.getElementById('ctl00_ContentPlaceHolder1_txtNodays').value=(parseInt(d)+1);
        //                if (document.getElementById('ctl00_ContentPlaceHolder1_txtNodays').value == "NaN")
        //                {
        //                    document.getElementById('ctl00_ContentPlaceHolder1_txtNodays').value ="";
        //                } 
        //                
        //            }
        //            
        //        }  
        ////        if (document.getElementById('ctl00_ContentPlaceHolder1_txtNodays').value <= 0 )
        //        {
        //            alert("No. of Days can not be 0 or less than 0 "); 
        //            document.getElementById('ctl00_ContentPlaceHolder1_txtNodays').value="";
        //            document.getElementById('ctl00_ContentPlaceHolder1_txtTodt').focus();
        //        }
        //        if (parseInt(document.getElementById('ctl00_ContentPlaceHolder1_txtNodays').value) > parseInt(document.getElementById('ctl00_ContentPlaceHolder1_txtLeavebal').value))
        //        {
        //           
        //            alert("No. of Days not more than Balance Days"); 
        //            document.getElementById('ctl00_ContentPlaceHolder1_txtNodays').value="";
        //            document.getElementById('ctl00_ContentPlaceHolder1_txtTodt').focus();
        //        }
        //          return false; 
        //    }

    </script>


    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">ORDER GENERATION</h3>
                </div>

                <div class="box-body">
                    <asp:Panel ID="pnlSelection" runat="server">
                        <div class="col-12">
                            <div class="row">
                                <div class="col-12">
                                    <div class="sub-heading">
                                        <h5>Criteria for Order Generation</h5>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-12">
                            <div class="row">
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>College Name</label>
                                    </div>
                                    <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control" TabIndex="1" AppendDataBoundItems="true" data-select2-enable="true"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged" ToolTip="Select College Name">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlCollege" InitialValue="0"
                                        Display="None" ErrorMessage="Please Select College Name " ValidationGroup="LeaveOrd"
                                        SetFocusOnError="true">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Staff Type</label>
                                    </div>
                                    <asp:DropDownList ID="ddlStaffType" runat="server" AppendDataBoundItems="true" data-select2-enable="true"
                                        OnSelectedIndexChanged="ddlStaffType_SelectedIndexChanged" ToolTip="Select Staff Type"
                                        AutoPostBack="true" TabIndex="2" CssClass="form-control">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvStaffType" runat="server" ControlToValidate="ddlStaffType" InitialValue="0"
                                        Display="None" ErrorMessage="Please Select Staff Type " ValidationGroup="LeaveOrd"
                                        SetFocusOnError="true">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <label>Order Type</label>
                                    </div>
                                    <asp:RadioButtonList ID="rblLeaveOD" runat="server" AutoPostBack="true" TabIndex="3"
                                        OnSelectedIndexChanged="rblLeaveOD_SelectedIndexChanged" ToolTip="Select order Type" RepeatDirection="Horizontal">
                                        <asp:ListItem Enabled="true" Selected="True" Text="Leave Order" Value="0">
                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                        <asp:ListItem Enabled="true" Text="OD Order" Value="1"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>

                    <asp:Panel ID="pnllist" runat="server">
                        <div class="col-12">
                            <div class="row">
                                <div class="col-12">
                                    <div class="sub-heading">
                                        <h5>List of Approved Leaves</h5>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-12">
                            <div class="row">
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <label>Order No.</label>
                                    </div>
                                    <asp:TextBox ID="txtOrderNo" runat="server" Enabled="false" MaxLength="30"
                                        CssClass="form-control" ToolTip="Order Number"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvOrdNo" runat="server"
                                        ControlToValidate="txtOrderNo" Display="None"
                                        ErrorMessage="Please Enter order No." SetFocusOnError="true"
                                        ValidationGroup="LeaveOrd">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12" id="trOType" runat="server" align="left">
                                    <div class="label-dynamic">
                                        <label>Order Type</label>
                                    </div>
                                    <asp:DropDownList ID="ddltype" runat="server" CssClass="form-control" ToolTip="Order Type" Enabled="false" data-select2-enable="true">
                                        <asp:ListItem Enabled="true" Text="Sanction" Value="1"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Order Date</label>
                                    </div>
                                    <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i id="imgCalOrddt" runat="server" class="fa fa-calendar text-blue"></i>
                                            </div>
                                        <asp:TextBox ID="txtOrderDt" runat="server" MaxLength="10" CssClass="form-control" TabIndex="4"
                                            OnTextChanged="txtOrderDt_TextChanged" ToolTip="Order Date" Style="z-index: 0;"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvOrdDt" runat="server" ControlToValidate="txtOrderDt"
                                            ValidationGroup="LeaveOrd" Display="None" ErrorMessage="Please Enter Order Date"
                                            SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        <ajaxToolKit:CalendarExtender ID="ceOrddt" runat="server" Enabled="true"
                                            EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="imgCalOrddt"
                                            TargetControlID="txtOrderDt">
                                        </ajaxToolKit:CalendarExtender>
                                        <ajaxToolKit:MaskedEditExtender ID="meeOrddt" runat="server"
                                            AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="true"
                                            Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true"
                                            TargetControlID="txtOrderdt" />
                                        <ajaxToolKit:MaskedEditValidator ID="mevOrddt" runat="server"
                                            ControlExtender="meeOrddt" ControlToValidate="txtOrderdt" Display="None"
                                            EmptyValueBlurredText="Empty" EmptyValueMessage="Please Enter Order Date"
                                            InvalidValueBlurredMessage="Invalid Date"
                                            InvalidValueMessage="Order Date is Invalid (Enter dd/MM/yyyy Format)"
                                            SetFocusOnError="true" TooltipMessage="Please Enter Order Date"
                                            ValidationGroup="LeaveOrd">
                                                &nbsp;&nbsp;&nbsp;
                                        </ajaxToolKit:MaskedEditValidator>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-12 btn-footer">
                            <asp:Button ID="btnOrder" runat="server" OnClick="btnOrder_Click" TabIndex="5"
                                Text="Save Order" ValidationGroup="LeaveOrd" CssClass="btn btn-primary" ToolTip="Click here to Save Order" />
                            <asp:Button ID="btnAdd" runat="server" CausesValidation="false" TabIndex="6"
                                OnClick="btnAdd_Click" Text="Add" Visible="false" CssClass="btn btn-primary" ToolTip="Click here to Save" />
                            <asp:Button ID="btnTransfer" runat="server" CausesValidation="false" TabIndex="7"
                                OnClick="btnTransfer_Click" Text="Transfer to Service Book" Visible="false"
                                CssClass="btn btn-primary" ToolTip="Click here to Transfer to Service Book" />
                            <asp:Button ID="btnReport" runat="server" CausesValidation="false" TabIndex="8"
                                OnClick="btnReport_Click" Text="Report" CssClass="btn btn-info" ToolTip="Click here to Show Report" />                            
                            <asp:ValidationSummary ID="ValidationSummary2" runat="server"
                                DisplayMode="List" ShowMessageBox="true" ShowSummary="false"
                                ValidationGroup="LeaveOrd" />
                        </div>
                        <div class="col-12">
                            <asp:ListView ID="lvPendingList" runat="server">
                                <EmptyDataTemplate>
                                    <br />
                                    <p class="text-center text-bold">
                                        <asp:Label ID="lblErr" runat="server" Text=" No More List of Approved Leaves "></asp:Label>
                                    </p>
                                </EmptyDataTemplate>
                                <LayoutTemplate>
                                        <div class="sub-heading">
	                                        <h5>List of Leaves Approved</h5>
                                        </div>
                                        <table class="table table-striped table-bordered nowrap display" style="width:100%">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <%--<th>Action
                                                                        </th>--%>
                                                    <th>Action
                                                    </th>
                                                    <th>Sr.No.
                                                    </th>
                                                    <th>Employee
                                                    </th>
                                                    <th>Department
                                                    </th>
                                                    <th>Leave
                                                    </th>
                                                    <th>From Date
                                                    </th>
                                                    <th>To Date
                                                    </th>
                                                    <th>Days
                                                    </th>
                                                    <th>Join Date
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
                                        <%--<td>
                                                                <asp:ImageButton ID="btnEdit" runat="server" AlternateText="Edit Record"
                                                                    CommandArgument='<%# Eval("LETRNO") %>' ImageUrl="~/images/edit.gif"
                                                                    OnClick="btnEdit_Click" ToolTip="Edit Record" TabIndex="9" />
                                                                &nbsp;
                                                    <asp:ImageButton ID="btnDelete" runat="server" AlternateText="Delete Record"
                                                        CommandArgument='<%# Eval("LETRNO") %>' ImageUrl="~/images/delete.gif"
                                                        OnClick="btnDelete_Click" OnClientClick="showConfirmDel(this); return false;"
                                                        ToolTip="Delete Record" />
                                                            </td>--%>
                                        <td>
                                            <asp:CheckBox ID="chkSelect" runat="server" ToolTip='<%# Eval("LETRNO") %>' />
                                        </td>
                                        <td>
                                            <%# Eval("sno") %>
                                        </td>
                                        <td>
                                            <%# Eval("EmpName")%>
                                        </td>
                                        <td>
                                            <%# Eval("SUBDEPT")%>
                                        </td>
                                        <td>
                                            <%# Eval("LName")%>
                                        </td>
                                        <td>
                                            <%# Eval("From_date")%>
                                        </td>
                                        <td>
                                            <%# Eval("TO_DATE") %>
                                        </td>
                                        <td>
                                            <%# Eval("NO_OF_DAYS") %>
                                        </td>
                                        <td>
                                            <%# Eval("JOINDT") %>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                       <%--     <div class="vista-grid_datapager d-none">
                                <div class="text-center">
                                    <asp:DataPager ID="dpPager" runat="server" OnPreRender="dpPager_PreRender"
                                        PagedControlID="lvPendingList" PageSize="10">
                                        <Fields>
                                            <asp:NextPreviousPagerField ButtonType="Link" FirstPageText="&lt;&lt;"
                                                PreviousPageText="&lt;" RenderDisabledButtonsAsLabels="true"
                                                ShowFirstPageButton="true" ShowLastPageButton="false"
                                                ShowNextPageButton="false" ShowPreviousPageButton="true" />
                                            <asp:NumericPagerField ButtonCount="7" ButtonType="Link"
                                                CurrentPageLabelCssClass="Current" />
                                            <asp:NextPreviousPagerField ButtonType="Link" LastPageText="&gt;&gt;"
                                                NextPageText="&gt;" RenderDisabledButtonsAsLabels="true"
                                                ShowFirstPageButton="false" ShowLastPageButton="true" ShowNextPageButton="true"
                                                ShowPreviousPageButton="false" />
                                        </Fields>
                                    </asp:DataPager>
                                </div>
                            </div>--%>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="pnlAdd" runat="server">
                        <div class="col-12">
                            <div class="row">
                                <div class="col-12">
                                    <div class="sub-heading">
                                        <h5>Add Criteria for Order Generation</h5>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-12">
                            <div class="row">
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <label>Leave Type</label>
                                    </div>
                                    <asp:RadioButtonList ID="rblleavetype" runat="server" AutoPostBack="true" RepeatDirection="Horizontal">
                                        <asp:ListItem Enabled="true" Selected="True" Text="Full Day" Value="0">&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                        <asp:ListItem Enabled="true" Text="Half Day" Value="1"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Employee Name</label>
                                    </div>
                                    <asp:DropDownList ID="ddlEmp" runat="server" data-select2-enable="true" CssClass="form-control" AppendDataBoundItems="true"
                                        AutoPostBack="True" OnSelectedIndexChanged="ddlEmp_SelectedIndexChanged" ToolTip="Select Employee Name">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvEmp" runat="server" ControlToValidate="ddlEmp"
                                        Display="None" ErrorMessage="Please Select Employee Name" ValidationGroup="Leaveapp"
                                        SetFocusOnError="true" InitialValue="0" />
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Leave Name</label>
                                    </div>
                                    <asp:DropDownList ID="ddlLName" runat="server" data-select2-enable="true" CssClass="form-control" ToolTip="Select Leave Name" AppendDataBoundItems="true"
                                        AutoPostBack="True" OnSelectedIndexChanged="ddlLName_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvLName" runat="server" ControlToValidate="ddlLName"
                                        Display="None" ErrorMessage="Please Select Leave " ValidationGroup="Leaveapp"
                                        SetFocusOnError="true" InitialValue="0" />
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <label>Balance Leave</label>
                                    </div>
                                    <asp:TextBox ID="txtLeavebal" runat="server" CssClass="form-control" Enabled="false" ToolTip="Balance Leave" ReadOnly="true" />
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
                                        <asp:TextBox ID="txtFromdt" runat="server" CssClass="form-control" MaxLength="10"
                                            ToolTip="Enter From Date" Style="z-index: 0;"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvFromdt" runat="server" ControlToValidate="txtFromdt"
                                            Display="None" ErrorMessage="Please Enter From Date" ValidationGroup="Leaveapp"
                                            SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        <ajaxToolKit:CalendarExtender ID="ceFromdt" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFromdt"
                                            PopupButtonID="imgCalFromdt" Enabled="true" EnableViewState="true">
                                        </ajaxToolKit:CalendarExtender>
                                        <ajaxToolKit:MaskedEditExtender ID="meeFromdt" runat="server" TargetControlID="txtFromdt"
                                            Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                            AcceptNegative="Left" ErrorTooltipEnabled="true" />
                                        <ajaxToolKit:MaskedEditValidator ID="mevFromdt" runat="server" ControlExtender="meeFromdt"
                                            ControlToValidate="txtFromdt" EmptyValueMessage="Please Enter From Date" InvalidValueMessage="From Date is Invalid (Enter dd/MM/yyyy Format)"
                                            Display="None" TooltipMessage="Please Enter From Date" EmptyValueBlurredText="Empty"
                                            InvalidValueBlurredMessage="Invalid Date" ValidationGroup="Leaveapp" SetFocusOnError="true"></ajaxToolKit:MaskedEditValidator>
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
                                        <asp:TextBox ID="txtTodt" runat="server" CssClass="form-control" MaxLength="10" OnTextChanged="txtTodt_TextChanged"
                                            AutoPostBack="true" ToolTip="Enter To Date" Style="z-index: 0;" />
                                        <asp:RequiredFieldValidator ID="rfvTodt" runat="server" ControlToValidate="txtTodt"
                                            Display="None" ErrorMessage="Please Enter To Date" ValidationGroup="Leaveapp"
                                            SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        <ajaxToolKit:CalendarExtender ID="CeTodt" runat="server" Format="dd/MM/yyyy" TargetControlID="txtTodt"
                                            PopupButtonID="imgCalTodt" Enabled="true" EnableViewState="true">
                                        </ajaxToolKit:CalendarExtender>
                                        <ajaxToolKit:MaskedEditExtender ID="meeTodt" runat="server" TargetControlID="txtTodt"
                                            Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                            AcceptNegative="Left" ErrorTooltipEnabled="true" />
                                        <ajaxToolKit:MaskedEditValidator ID="mevTodt" runat="server" ControlExtender="meeTodt"
                                            ControlToValidate="txtTodt" EmptyValueMessage="Please Enter To Date" InvalidValueMessage="To Date is Invalid (Enter dd/MM/yyyy Format)"
                                            Display="None" TooltipMessage="Please Enter To Date" EmptyValueBlurredText="Empty"
                                            InvalidValueBlurredMessage="Invalid Date" ValidationGroup="Leaveapp" SetFocusOnError="true"></ajaxToolKit:MaskedEditValidator>
                                    </div>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>No. of Days</label>
                                    </div>
                                    <asp:TextBox ID="txtNodays" runat="server" MaxLength="5" CssClass="form-control" OnTextChanged="txtNodays_TextChanged"
                                        AutoPostBack="true" ToolTip="Enter Number of Days" />
                                    <asp:RequiredFieldValidator ID="rfvNodays" runat="server" ControlToValidate="txtNodays"
                                        Display="None" ErrorMessage="Please Enter No. of Days" ValidationGroup="Leaveapp"></asp:RequiredFieldValidator>
                                    <asp:RangeValidator ID="rngNodays" runat="server" ControlToValidate="txtNodays" Display="None"
                                        ErrorMessage="Please Enter No of Days Between 1 to 999" ValidationGroup="Leaveapp"
                                        SetFocusOnError="true" MinimumValue="1" MaximumValue="999.99" Type="Double"></asp:RangeValidator>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <label>Joining Date</label>
                                    </div>
                                    <div class="input-group date">
                                        <div class="input-group-addon">
                                            <i id="imgCalJoindt" runat="server" class="fa fa-calendar text-blue"></i>
                                        </div>
                                        <asp:TextBox ID="txtJoindt" runat="server" CssClass="form-control" MaxLength="10"
                                            ToolTip="Enter Joining Date" Style="z-index: 0;" />
                                        <ajaxToolKit:CalendarExtender ID="CeJoindt" runat="server" Format="dd/MM/yyyy" TargetControlID="txtJoindt"
                                            PopupButtonID="imgCalJoindt" Enabled="true" EnableViewState="true">
                                        </ajaxToolKit:CalendarExtender>
                                        <ajaxToolKit:MaskedEditExtender ID="meeJoindt" runat="server" TargetControlID="txtJoindt"
                                            Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                            AcceptNegative="Left" ErrorTooltipEnabled="true" />
                                        <ajaxToolKit:MaskedEditValidator ID="mevJoindt" runat="server" ControlExtender="meeJoindt"
                                            ControlToValidate="txtJoindt" EmptyValueMessage="Please Enter Joining Date" InvalidValueMessage="Joining Date is Invalid (Enter dd/MM/yyyy Format)"
                                            Display="None" TooltipMessage="Please Enter Joining Date" EmptyValueBlurredText="Empty"
                                            InvalidValueBlurredMessage="Invalid Date" ValidationGroup="Leaveapp" SetFocusOnError="true">
                                        </ajaxToolKit:MaskedEditValidator>
                                    </div>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <label>FN/AN</label>
                                    </div>
                                    <asp:DropDownList ID="ddlNoon" runat="server" CssClass="form-control" ToolTip="Select FN/AN" AppendDataBoundItems="true" data-select2-enable="true">
                                        <asp:ListItem Value="0">FN</asp:ListItem>
                                        <asp:ListItem Value="1">AN</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <label>Certificates</label>
                                    </div>
                                    <asp:CheckBox ID="chkUFit" Text="Unfit Certificate" runat="Server" />
                                    &nbsp;
                                                <asp:CheckBox ID="ChkFit" Text="Fit Certificate" runat="Server" />
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Reason</label>
                                    </div>
                                    <asp:TextBox ID="txtReason" runat="server" TextMode="MultiLine" ToolTip="Enter Reason" CssClass="form-control" />
                                    <asp:RequiredFieldValidator ID="rfvReason" runat="server" ControlToValidate="txtReason"
                                        Display="None" ErrorMessage="Please Enter Reason" ValidationGroup="Leaveapp"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="col-12 btn-footer">
                            <asp:Button ID="btnSave" runat="server" Text="Submit" ValidationGroup="Leaveapp"
                                CssClass="btn btn-primary" ToolTip="Click here to Submit" OnClick="btnSave_Click" />
                            <asp:Button ID="btnBack" runat="server" CausesValidation="false" OnClick="btnBack_Click"
                                Text="Back" CssClass="btn btn-primary" ToolTip="Click here to Go Back" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                                CssClass="btn btn-warning" ToolTip="Click here to Reset" OnClick="btnCancel_Click" />                            
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Leaveapp"
                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                        </div>
                    </asp:Panel>

                    <asp:Panel ID="pnlOrder" runat="server" Visible="false">
                        <div class="col-12">
                            <div class="row">

                                 <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup> *</sup>
                                        <label>College Name</label>
                                    </div>
                                    <asp:DropDownList ID="ddlcollege2" runat="server" CssClass="form-control" TabIndex="1" AppendDataBoundItems="true" data-select2-enable="true"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlcollege2_SelectedIndexChanged" ToolTip="Select College Name">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="reqcollege" runat="server" ControlToValidate="ddlcollege2" InitialValue="0"
                                        Display="None" ErrorMessage="Please Select College Name " ValidationGroup="Report"
                                        SetFocusOnError="true">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup> *</sup>
                                        <label>Staff Type</label>
                                    </div>
                                    <asp:DropDownList ID="ddlstafftype2" runat="server" AppendDataBoundItems="true" data-select2-enable="true"
                                        OnSelectedIndexChanged="ddlstafftype2_SelectedIndexChanged" ToolTip="Select Staff Type"
                                        AutoPostBack="true" TabIndex="2" CssClass="form-control">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="reqstafftype" runat="server" ControlToValidate="ddlstafftype2" InitialValue="0"
                                        Display="None" ErrorMessage="Please Select Staff Type " ValidationGroup="Report"
                                        SetFocusOnError="true">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Employee Name</label>
                                    </div>
                                    <asp:DropDownList ID="ddlEname" runat="server" AutoPostBack="true" CssClass="form-control" data-select2-enable="true"
                                        AppendDataBoundItems="true" ToolTip="Select Employee Name"
                                        OnSelectedIndexChanged="ddlEname_SelectedIndexChanged">
                                        <asp:ListItem Enabled="true" Selected="True" Text="Please Select" Value="0">                                            
                                        </asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvddlEname" runat="server" ControlToValidate="ddlEname"
                                        Display="None" ErrorMessage="Please Select Employee Name" ValidationGroup="Report"
                                        InitialValue="0"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Order No.</label>
                                    </div>
                                    <asp:DropDownList ID="ddlOrder" runat="server" AppendDataBoundItems="true" data-select2-enable="true"
                                        CssClass="form-control" ToolTip="Select Order Number" OnSelectedIndexChanged="ddlOrder_SelectedIndexChanged">
                                        <asp:ListItem Enabled="true" Selected="True" Text="Please Select" Value="0">
                                        </asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlOrder"
                                        Display="None" ErrorMessage="Please Select Order No." ValidationGroup="Report"
                                        InitialValue="0"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12" id="trtext" runat="server">
                                    <div class="label-dynamic">
                                        <label>Text</label>
                                    </div>
                                    <asp:TextBox ID="txtText" runat="server" ToolTip="Enter Text" CssClass="form-control"></asp:TextBox>
                                </div>

                            </div>
                        </div>
                        <div class="col-12 btn-footer">
                            <asp:Button ID="btnbac" runat="server" CausesValidation="false" Text="Back" CssClass="btn btn-primary"
                                OnClick="btnbac_Click" ToolTip="Click here to Go Back" />
                            <asp:Button ID="btnRpt" runat="server" Text=" Bulk Report " CssClass="btn btn-info" OnClick="btnRpt_Click"
                                ValidationGroup="Report" Visible="false" ToolTip="click here for Bulk Report" />
                            <asp:Button ID="btnInd" runat="server" Text="Report" CssClass="btn btn-info" OnClick="btnInd_Click"
                                ValidationGroup="Report" ToolTip="Click here to Show Report" />                            
                            <asp:ValidationSummary ID="ValidationSummary3" DisplayMode="List" ShowMessageBox="true"
                                ShowSummary="false" ValidationGroup="Report" runat="server" />
                        </div>
                    </asp:Panel>
                    <%--<div class="col-md-12">
                            <asp:Panel ID="div" runat="server" Style="display: none" CssClass="modalPopup">
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
                            </asp:Panel>
                        </div>--%>
                </div>
            </div>
        </div>
    </div>


    <div id="divMsg" runat="server">
    </div>
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
