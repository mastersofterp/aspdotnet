<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Login_details_Time_Interval.aspx.cs" Inherits="Login_details_Time_Interval"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   <%-- <script src="https://cdn.datatables.net/1.10.4/js/jquery.dataTables.min.js"></script>--%>

    <script type="text/javascript">
        //On Page Load
        $(document).ready(function () {
            $('#table2').DataTable();
        });
    </script>

    <script type="text/javascript">
        //On UpdatePanel Refresh
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    $('#table2').dataTable();
                }
            });
        };
    </script>

  <%--  <script type="text/javascript">
        RunThisAfterEachAsyncPostback();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
    </script>--%>
    <%--<fieldset class="fieldset">--%>
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">BIOMETRIC DETAILS REPORT</h3>
                </div>
                <div class="box-body">
                    <div id="div1" runat="server"></div>
                    <div class="col-md-12">
                        <%--<div class="form-group col-md-11">--%>

                        <%--<h4 class="box-title">LOGIN/LOGOUT REPORT</h4>--%>
                        <%-- <div class="sub-heading">
                            <h5>LOGIN/LOGOUT REPORT</h5>
                        </div>

                    </div>--%>
                        <%--<div class="col-12">
                        <div class="row">
                            <div class="col-12">
                                <div class="sub-heading">
                                    <h5>Select Criteria for Report</h5>
                                </div>
                            </div>
                        </div>
                    </div>--%>

                        <div class="form-group col-md-1" style="text-align: left">

                            <asp:ImageButton ID="imgBtnBack" runat="server" ImageUrl="~/IMAGES/btnBack.jpg" Width="60px" Height="30px" PostBackUrl="~/PAYROLL/TRANSACTIONS/Pay_UniversalSearch_EmployeeDetail.aspx" Visible="false" />

                        </div>
                        <%--  <hr />--%>


                        <%--<form role="form">
                        <div class="box-body">--%>
                        <div class="col-12">
                            <div class="row">
                                <%--Note <b>:</b> <span style="color: #FF0000">* Marked Is Mandatory !</span>--%>
                                <%-- <div class="panel panel-info">--%>
                                <%-- <div class="panel-heading">LOGIN/LOGOUT REPORT</div>--%>
                                <%-- <div class="panel-body">--%>
                                <%--<div class="form-group col-md-12">--%>
                                <div class="col-md-12">
                                    <asp:Label ID="lblerror" SkinID="Errorlbl" runat="server"></asp:Label>
                                    <asp:Label ID="lblmsg" runat="server" SkinID="lblmsg"></asp:Label>
                                </div>
                                <div class="col-md-12">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <asp:Panel ID="pnlInfo" runat="server">
                                                <div class="col-12">
                                                    <div class="row">
                                                        <%--Note <b>:</b> <span style="color: #FF0000">* Marked Is Mandatory !</span><br />--%>
                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="trStaffType" runat="server" visible="false">
                                                            <sup>* </sup>
                                                            <label>Staff Type</label>
                                                            <asp:DropDownList ID="ddlStaffType" AppendDataBoundItems="true" runat="server"
                                                                CssClass="form-control" data-select2-enable="true">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvDept" runat="server" ControlToValidate="ddlStaffType"
                                                                Display="None" ErrorMessage="Please Select Staff Type" ValidationGroup="Holiday"
                                                                SetFocusOnError="True" InitialValue="0">
                                                            </asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="trcollege" runat="server">
                                                            <sup>* </sup>
                                                            <label>College</label>
                                                            <asp:DropDownList ID="ddlCollege" AppendDataBoundItems="true" runat="server" data-select2-enable="true"
                                                                CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlCollege"
                                                                Display="None" ErrorMessage="Please Select College" ValidationGroup="Holiday"
                                                                SetFocusOnError="True" InitialValue="0">
                                                            </asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="tr1" runat="server" visible="false">
                                                            <label>Staff Type </label>
                                                            <asp:DropDownList ID="ddlStaff" AppendDataBoundItems="true" runat="server" data-select2-enable="true"
                                                                CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlStaff_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                            <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlStaff"
                                                                Display="None" ErrorMessage="Please Select Staff Type" ValidationGroup="Holiday"
                                                                SetFocusOnError="True" InitialValue="0">
                                                            </asp:RequiredFieldValidator>--%>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="trdept" runat="server">
                                                            <label>Department</label>
                                                            <asp:DropDownList ID="ddldept" AppendDataBoundItems="true" AutoPostBack="true" runat="server"
                                                                CssClass="form-control" OnSelectedIndexChanged="ddldept_SelectedIndexChanged" data-select2-enable="true">
                                                            </asp:DropDownList>

                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="trShiftCategory" runat="server">
                                                            <label>Employee Type </label>
                                                            <asp:RadioButtonList ID="rblEmpType" runat="server" RepeatDirection="Horizontal" AutoPostBack="true"
                                                                OnSelectedIndexChanged="rblEmpType_SelectedIndexChanged">
                                                                <%----%>
                                                                <asp:ListItem Value="0" Text="General Employee" Selected="True"></asp:ListItem>
                                                                <asp:ListItem Value="1" Text="Shift Module Employee"></asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="trsearchtype" runat="server" style="padding-top: 10px">
                                                            <label>Search Type</label>
                                                            <asp:RadioButtonList ID="rblSelect" runat="server" RepeatDirection="Horizontal" AutoPostBack="True"
                                                                OnSelectedIndexChanged="rblSelect_SelectedIndexChanged">
                                                                <asp:ListItem Value="0" Text="All Employee" Selected="True"></asp:ListItem>
                                                                <asp:ListItem Value="1" Text="Particular"></asp:ListItem>
                                                            </asp:RadioButtonList>

                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="trEmp" visible="false" runat="server">
                                                            <label>Employee <span style="color: #FF0000">*</span></label>
                                                            <asp:DropDownList ID="ddlEmployee" runat="server" AppendDataBoundItems="true" data-select2-enable="true"
                                                                AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvSelectEmployee" runat="server" ControlToValidate="ddlEmployee"
                                                                Display="None" ErrorMessage="Please Select Employee"  ValidationGroup="Holiday"
                                                                SetFocusOnError="True">
                                                            </asp:RequiredFieldValidator>

                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                             <sup>* </sup>
                                                            <label>From Date</label>
                                                            <div class="input-group">
                                                                <asp:TextBox ID="txtFdate" runat="server" CssClass="form-control"
                                                                    AutoPostBack="true"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvholidayDt" runat="server" ControlToValidate="txtFdate"
                                                                    Display="None" ErrorMessage="Please Enter From Date" ValidationGroup="Holiday"
                                                                    SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                                <div class="input-group-addon">
                                                                    <asp:Image ID="imgToDate" runat="server" ImageUrl="~/IMAGES/calendar.png" Style="cursor: pointer" />
                                                                </div>
                                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                                    PopupButtonID="imgToDate" TargetControlID="txtFdate">
                                                                </ajaxToolKit:CalendarExtender>
                                                                <ajaxToolKit:MaskedEditExtender ID="meFDate" runat="server" CultureAMPMPlaceholder=""
                                                                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                    CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                                    OnInvalidCssClass="errordate" Enabled="True" Mask="99/99/9999" MaskType="Date"
                                                                    TargetControlID="txtFdate">
                                                                </ajaxToolKit:MaskedEditExtender>
                                                                <ajaxToolKit:MaskedEditValidator ID="mvFdate" runat="server" ControlExtender="meFDate"
                                                                    ControlToValidate="txtFdate" Display="None" EmptyValueMessage="Please Enter From Date."
                                                                    ErrorMessage="Please Select From Date" InvalidValueBlurredMessage="*" InvalidValueMessage="From Date is invalid"
                                                                    IsValidEmpty="False" SetFocusOnError="True" ValidationGroup="show"></ajaxToolKit:MaskedEditValidator>
                                                            </div>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <sup>* </sup>
                                                            <label>To Date </label>
                                                            <div class="input-group">
                                                                <asp:TextBox ID="txtDate" runat="server" CssClass="form-control"
                                                                    OnTextChanged="txtDate_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDate"
                                                                    Display="None" ErrorMessage="Please Enter To Date" ValidationGroup="Holiday"
                                                                    SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                                <div class="input-group-addon">
                                                                    <asp:Image ID="imgDate" runat="server" ImageUrl="~/IMAGES/calendar.png" Style="cursor: pointer" />
                                                                </div>
                                                                <ajaxToolKit:CalendarExtender ID="calDate" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                                    PopupButtonID="imgDate" TargetControlID="txtDate">
                                                                </ajaxToolKit:CalendarExtender>
                                                                <ajaxToolKit:MaskedEditExtender ID="meDate" runat="server" CultureAMPMPlaceholder=""
                                                                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                    CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                                    OnInvalidCssClass="errordate" Enabled="True" Mask="99/99/9999" MaskType="Date"
                                                                    TargetControlID="txtDate">
                                                                </ajaxToolKit:MaskedEditExtender>
                                                                <ajaxToolKit:MaskedEditValidator ID="mvDate" runat="server" ControlExtender="meDate"
                                                                    ControlToValidate="txtDate" Display="None" EmptyValueMessage="Please Enter Date."
                                                                    ErrorMessage="Please Select Date" InvalidValueBlurredMessage="*" InvalidValueMessage="Date is invalid"
                                                                    IsValidEmpty="False" SetFocusOnError="True" ValidationGroup="show"></ajaxToolKit:MaskedEditValidator>
                                                                <asp:CompareValidator ID="CampCalExtDate" runat="server" ControlToValidate="txtDate"
                                                                    CultureInvariantValues="true" Display="None" ErrorMessage="To Date Must Be Equal To Or Greater Than From Date." Operator="GreaterThanEqual" SetFocusOnError="True" Type="Date"
                                                                    ValidationGroup="Holiday" ControlToCompare="txtFdate" />
                                                            </div>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="trTimeFormat" runat="server" visible="false" style="padding-top: 10px">
                                                            <label>Time Format</label>

                                                            <asp:RadioButtonList ID="rblTime" runat="server" RepeatDirection="Horizontal"
                                                                AutoPostBack="True" OnSelectedIndexChanged="rblTime_SelectedIndexChanged">
                                                                <asp:ListItem Value="0" Text="In Time"></asp:ListItem>
                                                                <asp:ListItem Value="1" Text="Out Time"></asp:ListItem>
                                                            </asp:RadioButtonList>

                                                        </div>
                                                        <div id="trIn" runat="server" visible="false" class="form-group col-md-4">
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <label>From InTime</label>
                                                                <asp:TextBox ID="txtInTimeFrom" runat="server" AutoPostBack="true" ToolTip="Please Enter In Time Start Range"
                                                                    OnTextChanged="txtInTimeFrom_TextChanged" CssClass="form-control"></asp:TextBox>
                                                                <ajaxToolKit:MaskedEditExtender
                                                                    ID="MaskedEditExtender1" runat="server" CultureAMPMPlaceholder=""
                                                                    CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                    CultureDatePlaceholder="" CultureDecimalPlaceholder=""
                                                                    CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True"
                                                                    ErrorTooltipEnabled="True" Mask="99:99" MaskType="Time"
                                                                    TargetControlID="txtInTimeFrom" />

                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12" id="Div2" runat="server" visible="false">
                                                                <label>To Intime </label>
                                                                <asp:TextBox ID="txtInTimeTo" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtInTimeTo_TextChanged" ToolTip="Please Enter In Time End Range"></asp:TextBox>
                                                                <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender4" runat="server" TargetControlID="txtInTimeTo"
                                                                    Mask="99:99" MaskType="Time" ErrorTooltipEnabled="True"
                                                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                    CultureTimePlaceholder="" Enabled="True" />
                                                            </div>
                                                        </div>
                                                        <div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12" id="trOut" runat="server" visible="false">

                                                                <label>From OutTime </label>

                                                                <asp:TextBox ID="txtOutTimeFrom" runat="server" AutoPostBack="true"
                                                                    CssClass="form-control" OnTextChanged="txtOutTimeFrom_TextChanged" ToolTip="Please Enter Out Time Start Range"></asp:TextBox>
                                                                <ajaxToolKit:MaskedEditExtender
                                                                    ID="MaskedEditExtender2" runat="server" CultureAMPMPlaceholder=""
                                                                    CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                    CultureDatePlaceholder="" CultureDecimalPlaceholder=""
                                                                    CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True"
                                                                    ErrorTooltipEnabled="True" Mask="99:99" MaskType="Time"
                                                                    TargetControlID="txtOutTimeFrom" />



                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12" id="trOuttype" runat="server" visible="false">
                                                                <label>To OutTime </label>
                                                                <asp:TextBox ID="txtOutTimeTo" runat="server" CssClass="form-control" AutoPostBack="true" ToolTip="Please Enter Out Time End Range"
                                                                    OnTextChanged="txtOutTimeTo_TextChanged"></asp:TextBox>
                                                                <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" TargetControlID="txtOutTimeTo"
                                                                    Mask="99:99" MaskType="Time" ErrorTooltipEnabled="True"
                                                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                    CultureTimePlaceholder="" Enabled="True" />
                                                            </div>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="trReportType" runat="server">
                                                            <label>Report Type</label>

                                                            <asp:CheckBox ID="chkGraph" runat="server" Text="Graph" Checked="false"
                                                                OnCheckedChanged="chkGraph_CheckedChanged" AutoPostBack="true" />


                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="trFormat" runat="server" visible="false">

                                                            <label>Select Format</label>


                                                            <asp:RadioButtonList ID="rblGraph" runat="server" RepeatDirection="Horizontal">
                                                                <asp:ListItem Value="0" Text="Graph Format1"></asp:ListItem>
                                                                <asp:ListItem Value="1" Text="Graph Format2"></asp:ListItem>
                                                            </asp:RadioButtonList>

                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                            <div class="col-md-12 form-group text-center">
                                                <asp:Button ID="btnShow" runat="server" Text="Show" CssClass-="btn btn-primary" ValidationGroup="Holiday" OnClick="btnShow_Click" />

                                                <asp:Button ID="btnReport" runat="server"
                                                    Text="Report" OnClick="btnReport_Click" ValidationGroup="Holiday"  CssClass-="btn btn-info" />

                                                &nbsp;<asp:Button ID="btnExport" runat="server" 
                                                    OnClick="btnExport_Click" Text="Export" CssClass-="btn btn-info" />
                                                <asp:Button runat="server" Text="Graph Report" ID="btnHisto" ValidationGroup="Holiday"
                                                    Visible="false" CssClass-="btn btn-info" />
                                                <asp:Button ID="btnCancel" runat="server" CausesValidation="False"
                                                    Text="Cancel" OnClick="btnCancel_Click" CssClass-="btn btn-warning" />
                                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Holiday"
                                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                            </div>
                                            <div class="text-center">
                                                <asp:Label ID="lblHead" runat="server" Visible="False" Style="text-align: center"></asp:Label>
                                            </div>
                                            <div class="col-md-12 table-responsive">
                                                <%--  <asp:Panel ID="pnlGridview" runat="server" ScrollBars="Auto" Visible="false">
                                                            <label>LOGIN/LOGOUT REPORT</label>
                                                            <asp:GridView ID="gvLoginDetails" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                                ForeColor="#333333" GridLines="None" Width="100%">
                                                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                                <RowStyle BackColor="#EFF3FB" />
                                                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                                <EditRowStyle BackColor="#2461BF" />
                                                                <AlternatingRowStyle BackColor="White" />
                                                                <Columns>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblID" runat="server" Text='<%# Eval("IDNO") %>' Visible="false"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <HeaderTemplate>
                                                                            RFIDNO
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblID" runat="server" Text='<%# Eval("USERID") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <HeaderTemplate>
                                                                            Name
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblName" runat="server" Text='<%# Eval("USERNAME") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <%--<asp:TemplateField>
                                                                        <HeaderTemplate>
                                                                            Staff
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblStaffName" runat="server" Text='<%# Eval("STAFF_NAME") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>--%>
                                                <%--<asp:TemplateField>
                                                                        <HeaderTemplate>
                                                                            Date
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblIntime" runat="server" Text='<%# Eval("ENTDATE") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                   <%-- <asp:TemplateField>
                                                                        <HeaderTemplate>
                                                                            Shift In-Time
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblShifttime" runat="server" Text='<%# Eval("SHIFTINTIME") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>--%>
                                                <%-- <asp:TemplateField>
                                                                        <HeaderTemplate>
                                                                            In-Time
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblOuttime" runat="server" Text='<%# Eval("INTIME") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>--%>
                                                <%--<asp:TemplateField>
                                                                        <HeaderTemplate>
                                                                            Shift Out-Time
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblShiftOut" runat="server" Text='<%# Eval("SHIFTOUTTIME") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>--%>
                                                <%-- <asp:TemplateField>
                                                                        <HeaderTemplate>
                                                                            Out-Time
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblHours" runat="server" Text='<%# Eval("OUTTIME") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>--%>

                                                <%-- <asp:TemplateField>
                                                                        <HeaderTemplate>
                                                                            LEAVETYPE
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblHours" runat="server" Text='<%# Eval("LEAVETYPE") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </asp:Panel>--%>
                                                <asp:Panel ID="pnlGridview" runat="server" ScrollBars="Auto" Visible="false">
                                                    <div>
                                                        <asp:Repeater ID="Rep_Info" runat="server">
                                                            <HeaderTemplate>
                                                                <%--<h4 class="box-title">Login  Details</h4>--%>
                                                                <div class="sub-heading">
	                                                            <h5>Login Details</h5>
                                                                </div>

                                                                <table id="table2" class="table table-striped dt-responsive nowrap">
                                                                    <thead>
                                                                        <tr class="bg-light-blue">
                                                                            <th>RFIDNO</th>
                                                                            <th>Employee No</th>
                                                                            <th>Name</th>
                                                                            <th>Date</th>
                                                                            <%-- <th> Shift In-Time</th>--%>
                                                                            <th>In-Time</th>
                                                                            <th>Out-Time</th>
                                                                            <th>Hours Worked</th>
                                                                            <th>Leave Type</th>

                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <tr class="item">
                                                                    <td style="width: 10%; padding-left: 14px;">
                                                                        <%# Eval("USERID")%>
                                                                    </td>
                                                                    <td style="width: 10%; padding-left: 14px;">
                                                                        <%# Eval("PFILENO")%>
                                                                    </td>
                                                                    <td style="width: 10%; text-align: left">
                                                                        <%# Eval("USERNAME")%>
                                                                    </td>
                                                                    <td style="width: 10%; text-align: left">
                                                                        <%# Eval("DATE")%>
                                                                    </td>
                                                                    <%--<td style="width: 10%; text-align: left">
                                                    <%# Eval("SHIFTINTIME")%>
                                                </td>  --%>
                                                                    <td style="width: 10%; text-align: left">
                                                                        <%# Eval("INTIME")%>
                                                                    </td>
                                                                    <td style="width: 10%; text-align: left">
                                                                        <%# Eval("OUTTIME")%>
                                                                    </td>
                                                                    <td style="width: 10%; text-align: left">
                                                                        <%# Eval("HOURS")%>
                                                                    </td>
                                                                    <td style="width: 10%; text-align: left">
                                                                        <%# Eval("LEAVETYPE")%>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                </tbody></table>
                                                            </FooterTemplate>
                                                        </asp:Repeater>
                                                    </div>
                                                </asp:Panel>
                                                <div class="col-md-12">
                                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                                                        <ProgressTemplate>
                                                            <asp:Image ID="imgmoney" runat="server" ImageUrl="~/images/ajax-loader.gif" />
                                                            Processing Please Wait.........................................
                                                        </ProgressTemplate>
                                                    </asp:UpdateProgress>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="btnReport" />
                                            <asp:PostBackTrigger ControlID="btnExport" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                                <%--</div>--%>
                                <%--</div>--%>
                                <%--</div>--%>
                            </div>
                        </div>
                        <%--</div>
                    </form>--%>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <table style="width: 100%">
        <div id="divMsg" runat="server">
        </div>
        <%-- <tr>
            <td class="vista_page_title_bar" valign="top" style="height: 30px">
               
                        &nbsp;
                        <!-- Button used to launch the help (animation) -->
                <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                    AlternateText="Page Help" ToolTip="Page Help" />
            </td>
        </tr>--%>
        <%--PAGE HELP--%>
        <%--JUST CHANGE THE IMAGE AS PER THE PAGE. NOTHING ELSE--%>
        <%-- <tr>
            <td style="width: 726px">
               
                <div id="flyout" style="display: none; overflow: hidden; z-index: 2; background-color: #FFFFFF; border: solid 1px #D0D0D0;">
                </div>
               
                <div id="info" style="display: none; width: 250px; z-index: 2; opacity: 0; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=0); font-size: 12px; border: solid 1px #CCCCCC; background-color: #FFFFFF; padding: 5px;">
                    <div id="btnCloseParent" style="float: right; opacity: 0; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=0);">
                        <asp:LinkButton ID="btnClose" runat="server" OnClientClick="return false;" Text="X"
                            ToolTip="Close" Style="background-color: #666666; color: #FFFFFF; text-align: center; font-weight: bold; text-decoration: none; border: outset thin #FFFFFF; padding: 5px;" />
                    </div>
                    <div>
                        <p class="page_help_head">
                            <span style="font-weight: bold; text-decoration: underline;">Page Help</span><br />
                            <asp:Image ID="imgEdit" runat="server" ImageUrl="~/images/edit.gif" AlternateText="Edit Record" />
                            Edit Record
                        </p>
                        <p class="page_help_text">
                            <asp:Label ID="lblHelp" runat="server" Font-Names="Trebuchet MS" />
                        </p>
                    </div>
                </div>--%>
    </table>
</asp:Content>
