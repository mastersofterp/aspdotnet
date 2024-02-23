<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="FacilityApplication.aspx.cs" Inherits="FacilityApplication" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="plugins/datatables/jquery.dataTables.css" rel="stylesheet" />
    <link href="plugins/datatables/jquery.dataTables.min.css" rel="stylesheet" />
    <link href="plugins/datatables/jquery.dataTables_themeroller.css" rel="stylesheet" />
    <link href="plugins/datatables/dataTables.bootstrap.css" rel="stylesheet" />
    <script type="text/javascript">
        RunThisAfterEachAsyncPostback();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
    </script>
    <asp:UpdatePanel ID="updPanel" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">APPLY CENTRALIZE FACILITY</h3>
                        </div>
                        <form role="form">
                            <div class="box-body">
                                <div class="col-md-12">

                                    <asp:Panel ID="pnlAdd" runat="server">
                                        <div class="panel panel-info">
                                            <div class="panel-heading" style="font-weight: 600; font-size: 13px">Add/Edit Centralize Facility Application</div>
                                            <div class="panel-body">
                                                <div class="form-group col-lg-8 col-md-12 col-12">
                                                    <div class=" note-div">
                                                        <h5 class="heading">Note</h5>
                                                        <p><i class="fa fa-star" aria-hidden="true"></i><span>* Marked Is Mandatory !</span></p>
                                                    </div>
                                                </div>
                                                <br />
                                                <div class="col-md-12">

                                                    <asp:Panel ID="pnlEmpInfo" runat="server">
                                                        <div class="panel panel-info">
                                                            <div class="panel-heading">
                                                                <div class="sub-heading">
                                                                    <h5>User Details</h5>
                                                                </div>
                                                            </div>
                                                            <div class="panel-body">
                                                                <div class="row">
                                                                    <div class="form-group col-md-3">
                                                                        <label>Employee Name :</label>
                                                                        <asp:TextBox ID="txtName" runat="server" ReadOnly="true" CssClass="form-control" />

                                                                    </div>
                                                                    <div class="form-group col-md-3">
                                                                        <label>Department  :</label>
                                                                        <asp:TextBox ID="txtDept" runat="server" ReadOnly="true" CssClass="form-control" />

                                                                    </div>
                                                                    <div class="form-group col-md-3">
                                                                        <label>Employee Code :</label>
                                                                        <asp:TextBox ID="txtCode" runat="server" ReadOnly="true" CssClass="form-control" />

                                                                    </div>
                                                                    <div class="form-group col-md-3">
                                                                        <label>Designation  :</label>
                                                                        <asp:TextBox ID="txtDesignation" runat="server" ReadOnly="true" CssClass="form-control" />

                                                                    </div>
                                                                </div>


                                                            </div>
                                                        </div>
                                                    </asp:Panel>
                                                </div>
                                                <br />
                                                <div class="col-md-12">

                                                    <asp:Panel ID="Panel1" runat="server">
                                                        <div class="panel panel-info">
                                                            <div class="panel-heading">
                                                                <div class="sub-heading">
                                                                    <h5>Apply Centralize Facility Details</h5>
                                                                </div>
                                                            </div>
                                                            <div class="panel-body">
                                                                <div class="row">
                                                                    <div class="form-group col-md-3">
                                                                        <label>Centralize Facility Name <span style="color: #FF0000">*</span>:</label>
                                                                        <asp:DropDownList ID="ddlFacilityName" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlFacilityName_SelectedIndexChanged">
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="rfvFacilitytype" runat="server" ControlToValidate="ddlFacilityName"
                                                                            Display="None" ErrorMessage="Please Select Centralize Facility Name" ValidationGroup="Facility"
                                                                            SetFocusOnError="True" InitialValue="0">
                                                                        </asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <div class="form-group col-md-3">
                                                                        <label>Centralize Facility Details :</label>

                                                                        <asp:TextBox ID="txtDetail" runat="server" ReadOnly="true" TabIndex="4"
                                                                            TextMode="MultiLine" CssClass="textbox form-control" ToolTip="Centralize Facility Details" />
                                                                    </div>
                                                                    <div class="form-group col-md-3">
                                                                        <label>Remark :</label>
                                                                        <asp:TextBox ID="txtRemark" runat="server" ReadOnly="true" MaxLength="500" TabIndex="4"
                                                                            TextMode="MultiLine" CssClass="textbox form-control" ToolTip="Enter Facility Remark" />

                                                                    </div>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <asp:Panel ID="Panel2" runat="server" ScrollBars="Vertical">
                                                                        <asp:Repeater ID="rptMinorFacilityList" runat="server" Visible="true">
                                                                            <HeaderTemplate>
                                                                                <div class="sub-heading">
                                                                                    <h5>Minor Facility List </h5>
                                                                                </div>
                                                                                <table class="table table-hover table-bordered" id="tbluser">
                                                                                    <thead>
                                                                                        <tr class="bg-light-blue">
                                                                                            <th>Select
                                                                                            </th>
                                                                                            <th>Minor Facility Name
                                                                                            </th>
                                                                                        </tr>
                                                                                    </thead>
                                                                                    <tbody>
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                                    <ContentTemplate>
                                                                                        <tr>
                                                                                            <td>

                                                                                                <asp:CheckBox ID="chkSelect" runat="server" ToolTip='<%# Eval("MinFacilityNo")%>' Checked="true"></asp:CheckBox>
                                                                                            </td>
                                                                                            <td>
                                                                                                <%# Eval("MinFacilityName")%>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </ContentTemplate>
                                                                                </asp:UpdatePanel>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                </tbody>  </table>
                                                                            </FooterTemplate>
                                                                        </asp:Repeater>
                                                                    </asp:Panel>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <asp:Panel ID="Panel4" runat="server" ScrollBars="Vertical">
                                                                        <asp:Repeater ID="rptExtraMinorList" runat="server" Visible="true">
                                                                            <HeaderTemplate>
                                                                                <div class="sub-heading">
                                                                                    <h5>Extra Minor Facility List </h5>
                                                                                </div>
                                                                                <table class="table table-hover table-bordered" id="tbluser">
                                                                                    <thead>
                                                                                        <tr class="bg-light-blue">
                                                                                            <th>Select
                                                                                            </th>
                                                                                            <th>Minor Facility Name
                                                                                            </th>
                                                                                        </tr>
                                                                                    </thead>
                                                                                    <tbody>
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                                    <ContentTemplate>
                                                                                        <tr>
                                                                                            <td>

                                                                                                <asp:CheckBox ID="chkSelectExtra" runat="server" ToolTip='<%# Eval("MinFacilityNo")%>'></asp:CheckBox>
                                                                                            </td>
                                                                                            <td>
                                                                                                <%# Eval("MinFacilityName")%>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </ContentTemplate>
                                                                                </asp:UpdatePanel>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                </tbody>  </table>
                                                                            </FooterTemplate>
                                                                        </asp:Repeater>
                                                                    </asp:Panel>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </asp:Panel>
                                                </div>

                                                <br />
                                                <div class="col-md-12">

                                                    <asp:Panel ID="Panel3" runat="server">
                                                        <asp:UpdatePanel ID="upAppEntry" runat="server">
                                                            <ContentTemplate>
                                                                <div class="panel panel-info">
                                                                    <div class="panel-heading">
                                                                        <div class="sub-heading">
                                                                            <h5>Application Details</h5>
                                                                        </div>
                                                                    </div>
                                                                    <div class="panel-body">
                                                                        <div class="row">
                                                                            <div class="form-group col-md-3">
                                                                                <label>Application Date <span style="color: #FF0000">*</span>:</label>
                                                                                <div class="input-group">
                                                                                    <div class="input-group-addon">
                                                                                        <%-- <asp:Image ID="imgCalJoindt" runat="server" ImageUrl="~/images/calendar.png"
                                                                         Style="cursor: pointer" />--%>
                                                                                        <i id="imgCalJoindt" runat="server" class="fa fa-calendar text-blue" style="cursor: pointer"></i>
                                                                                    </div>
                                                                                    <asp:TextBox ID="txtApplicationDate" runat="server" MaxLength="10" CssClass="form-control" />


                                                                                    <ajaxToolKit:CalendarExtender ID="CeJoindt" runat="server" Enabled="true"
                                                                                        EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="imgCalJoindt"
                                                                                        TargetControlID="txtApplicationDate">
                                                                                    </ajaxToolKit:CalendarExtender>
                                                                                    <ajaxToolKit:MaskedEditExtender ID="meeJoindt" runat="server"
                                                                                        AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="true"
                                                                                        Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true"
                                                                                        TargetControlID="txtApplicationDate" />
                                                                                    <ajaxToolKit:MaskedEditValidator ID="mevJoindt" runat="server"
                                                                                        ControlExtender="meeJoindt" ControlToValidate="txtApplicationDate" Display="None"
                                                                                        EmptyValueBlurredText="Empty" EmptyValueMessage="Please Enter Joining Date"
                                                                                        InvalidValueBlurredMessage="Invalid Date"
                                                                                        InvalidValueMessage="Joining Date is Invalid (Enter dd/MM/yyyy Format)"
                                                                                        SetFocusOnError="true" TooltipMessage="Please Enter Joining Date"
                                                                                        ValidationGroup="Leaveapp">
                              
                                                                                    </ajaxToolKit:MaskedEditValidator>
                                                                                </div>
                                                                            </div>

                                                                            <%--<div class="form-group col-md-12">--%>

                                                                            <div class="col-md-3">


                                                                                <label>From Date :<span style="color: #FF0000">*</span></label>
                                                                                <div class="input-group">
                                                                                    <div class="input-group-addon">
                                                                                        <i id="imgCalFacilityDt" runat="server" class="fa fa-calendar text-blue" style="cursor: pointer"></i>
                                                                                        <%--<asp:Image ID="imgCalFacilityDt" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />--%>
                                                                                    </div>
                                                                                    <asp:TextBox ID="txtFromDt" runat="server" MaxLength="10" onchange="Cleardate()" class="form-control" Style="z-index: 0;" />
                                                                                    <ajaxToolKit:CalendarExtender ID="ceFacilityDt" runat="server" Format="dd/MM/yyyy"
                                                                                        TargetControlID="txtFromDt" PopupButtonID="imgCalFacilityDt" Enabled="true"
                                                                                        EnableViewState="true">
                                                                                    </ajaxToolKit:CalendarExtender>
                                                                                    <%--<asp:RequiredFieldValidator ID="rfvFacilityDt" runat="server" ControlToValidate="txtFromDt"
                                                                                        Display="None" ErrorMessage="Please Enter From Date" ValidationGroup="Facility"
                                                                                        SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                                                                    <%--<asp:CompareValidator ID="cvdate" runat="server" controltovalidate="txtToDt" controltocompare="txtFromDt" operator="GreaterThanEqual" type="Date" ErrorMessage="The From Date should be smaller than To Date !" ></asp:CompareValidator>--%>
                                                                                    <ajaxToolKit:MaskedEditExtender ID="meeFacilityDt" runat="server" TargetControlID="txtFromDt"
                                                                                        Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                                                        AcceptNegative="Left" ErrorTooltipEnabled="true" />
                                                                                    <ajaxToolKit:MaskedEditValidator ID="mevFacilityDt" runat="server" ControlExtender="meeFacilityDt"
                                                                                        ControlToValidate="txtFromDt" EmptyValueMessage="Please Enter From Date" IsValidEmpty="false"
                                                                                        InvalidValueMessage=" From Date is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                                                                        TooltipMessage="Please Enter From Date" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                                                                        ValidationGroup="Facility" SetFocusOnError="true"></ajaxToolKit:MaskedEditValidator>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-md-3">
                                                                                <label>From Time :<span style="color: #FF0000">*</span></label>
                                                                                <asp:TextBox ID="txtFromTime" runat="server" CssClass="form-control "></asp:TextBox>
                                                                                <%--<asp:RequiredFieldValidator ID="rfvFacilityTime1" runat="server" ControlToValidate="txtFromTime"
                                                                                    Display="None" ErrorMessage="Please Enter From Time" ValidationGroup="Facility"
                                                                                    SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                                                                <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txtFromTime"
                                                                                    Mask="99:99" MaskType="Time"
                                                                                    AcceptAMPM="true" ErrorTooltipEnabled="true" MessageValidatorTip="true" AcceptNegative="Left"
                                                                                    DisplayMoney="Left" OnInvalidCssClass="errordate" />
                                                                                <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="MaskedEditExtender2"
                                                                                    ControlToValidate="txtFromTime" Display="None" EmptyValueBlurredText="Empty" EmptyValueMessage="Please Enter From Time"
                                                                                    InvalidValueMessage="From Time is Invalid (Enter 12 Hour Format)"
                                                                                    SetFocusOnError="true" TooltipMessage="Please Enter From Time"
                                                                                    IsValidEmpty="false" ValidationGroup="Facility"/>
                                                                            </div>
                                                                            <%--</div>--%>

                                                                            <div class="form-group col-md-3">
                                                                                <label>To Date <span style="color: #FF0000">*</span>:</label>
                                                                                <asp:UpdatePanel ID="updToDt" runat="server">
                                                                                    <ContentTemplate>
                                                                                        <div class="input-group date">
                                                                                            <div class="input-group-addon">
                                                                                                <%--<asp:Image ID="imgCalToDt" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />--%>
                                                                                                <i id="imgCalToDt" runat="server" class="fa fa-calendar text-blue" style="cursor: pointer"></i>
                                                                                            </div>
                                                                                            <asp:TextBox ID="txtToDt" runat="server" MaxLength="10" class="form-control" Style="z-index: 0;" />
                                                                                            <%-- <asp:TextBox ID="txtToDt" runat="server" MaxLength="10" class="form-control" Style="z-index: 0;" OnTextChanged="txtToDt_TextChanged" AutoPostBack="false" />--%>

                                                                                            <%-- <asp:TextBox ID="txtToDt" runat="server" MaxLength="10" class="form-control" Style="z-index: 0;" />--%>
                                                                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtToDt"
                                                                                                Display="None" ErrorMessage="Please Enter  To Date" ValidationGroup="Facility"
                                                                                                SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                                                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                                                                                TargetControlID="txtToDt" PopupButtonID="imgCalToDt" Enabled="true"
                                                                                                EnableViewState="true">
                                                                                            </ajaxToolKit:CalendarExtender>
                                                                                            <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtToDt"
                                                                                                Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                                                                AcceptNegative="Left" ErrorTooltipEnabled="true" />
                                                                                            <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender1"
                                                                                                ControlToValidate="txtToDt" EmptyValueMessage="Please Enter To Date"
                                                                                                InvalidValueMessage=" To Date is Invalid (Enter dd/MM/yyyy Format)" Display="None" 
                                                                                                TooltipMessage="Please Enter To Date" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                                                                                ValidationGroup="Facility" SetFocusOnError="true"></ajaxToolKit:MaskedEditValidator>
                                                                                        </div>


                                                                                    </ContentTemplate>
                                                                                    <%-- <Triggers>
                                                                 <asp:AsyncPostBackTrigger ControlID="txtToDt" EventName="TextChanged"/>
                                                             </Triggers>--%>
                                                                                </asp:UpdatePanel>

                                                                            </div>

                                                                            <div class="form-group col-md-3">
                                                                                <label>To Time <span style="color: #FF0000">*</span>:</label>

                                                                                <asp:TextBox ID="txtToTime" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                <%--<asp:RequiredFieldValidator ID="rfvFacilityTime2" runat="server" ControlToValidate="txtToTime"
                                                                                    Display="None" ErrorMessage="Please Enter To Time" ValidationGroup="Facility"
                                                                                    SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                                                                <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" TargetControlID="txtToTime"
                                                                                    Mask="99:99" MaskType="Time"
                                                                                    AcceptAMPM="true" ErrorTooltipEnabled="true" MessageValidatorTip="true" AcceptNegative="Left"
                                                                                    DisplayMoney="Left" OnInvalidCssClass="errordate" />
                                                                                <ajaxToolKit:MaskedEditValidator ID="mevEnterTimeTo" runat="server" ControlExtender="MaskedEditExtender3"
                                                                                    ControlToValidate="txtToTime" Display="None" EmptyValueBlurredText="Empty" EmptyValueMessage="Please Enter To Time"
                                                                                    InvalidValueMessage="To Time is Invalid (Enter 12 Hour Format)"
                                                                                    SetFocusOnError="true" TooltipMessage="Please Enter To Time"
                                                                                    IsValidEmpty="false" ValidationGroup="Facility" />


                                                                            </div>

                                                                            <div class="form-group col-md-3">
                                                                                <label>Priority Level <span style="color: #FF0000">*</span>:</label>
                                                                                <asp:DropDownList ID="ddlLevel" runat="server" data-select2-enable="true" CssClass="form-control">
                                                                                    <asp:ListItem Selected="True" Text="Please Select" Value="0"></asp:ListItem>
                                                                                    <asp:ListItem Text="High" Value="H"></asp:ListItem>
                                                                                    <asp:ListItem Text="Low" Value="L"></asp:ListItem>
                                                                                    <asp:ListItem Text="Medium" Value="M"></asp:ListItem>
                                                                                </asp:DropDownList>

                                                                            </div>
                                                                            <div class="form-group col-md-3">
                                                                                <label>Remark :</label>
                                                                                <asp:TextBox ID="txtAppRemark" runat="server" MaxLength="250" CssClass="form-control" />

                                                                            </div>

                                                                        </div>

                                                                        <div class="panel panel-info" id="divCancel1" runat="server" visible="false">
                                                                            <div class="panel-heading">
                                                                                <div class="sub-heading">
                                                                                    <h5>Cancel Status</h5>
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="form-group col-2">
                                                                                    <asp:CheckBox ID="chkIsCancel" runat="server" Text="Cancel" Checked="false" CssClass="" />
                                                                                </div>
                                                                                <div class="form-group col-6">
                                                                                    <label>Cancel Reason/Remark :</label>
                                                                                    <asp:TextBox ID="txtReason" runat="server" MaxLength="100" CssClass="form-control" TextMode="MultiLine" Width="300px" />
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:PostBackTrigger ControlID="txtToDt" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>
                                                    </asp:Panel>
                                                </div>



                                            </div>
                                        </div>
                                    </asp:Panel>
                                    <div class="col-md-12 text-center">
                                        <asp:Panel ID="pnlList" runat="server">
                                            <asp:LinkButton ID="btnAdd" runat="server" SkinID="LinkAddNew" OnClick="btnAdd_Click" ToolTip="Click Add New To Enter Centralize Facility" Text="Add New" CssClass="btn btn-primary"></asp:LinkButton>
                                            <%-- <asp:Button ID="btnShowReport"  Visible="false" runat="server" Text=" Report" CausesValidation="false" OnClick="btnShowReport_Click"
                                         CssClass="btn btn-info" ToolTip="Click here to show the report" />--%>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </div>
                            <div class="box-footer">
                                <asp:Panel ID="pnlbutton" runat="server">
                                    <p class="text-center">

                                        <asp:Button ID="btnSave" runat="server" TabIndex="4" Text="Submit" ValidationGroup="Facility"
                                            CssClass="btn btn-primary" OnClick="btnSave_Click" ToolTip="Click here to Submit" />
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" Visible="false"
                                            OnClick="btnCancel_Click" CssClass="btn btn-warning" TabIndex="5" ToolTip="Click here to Reset" />&nbsp;
                                 <asp:Button ID="btnBack" runat="server" Text="Back" CausesValidation="false" ToolTip="Click here to go back to previous" OnClick="btnBack_Click"
                                     CssClass="btn btn-info" />
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Facility"
                                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />

                                    </p>
                                </asp:Panel>
                                <div class="col-md-12 table-responsive">
                                    <asp:Repeater ID="lvApplication" runat="server">
                                        <HeaderTemplate>
                                            <div class="sub-heading">
                                                <h5>Centralize Facility List </h5>
                                            </div>
                                            <table class="table table-hover table-bordered" id="tbluser">
                                                <thead>
                                                    <tr class="bg-light-blue">
                                                        <th>Action
                                                        </th>
                                                        <th>Centralize Facility Name
                                                        </th>
                                                        <th>From Date
                                                        </th>
                                                        <th>To Date
                                                        </th>
                                                        <th>Status
                                                        </th>
                                                        <th>Priority Level
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <tr>
                                                        <td>
                                                            <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.png" CommandArgument='<%# Eval("ApplicationNo") %>'
                                                                AlternateText='<%# Eval("Status") %>' ToolTip="Edit Record" OnClick="btnEdit_Click" TabIndex="6" />
                                                        </td>
                                                        <td>
                                                            <%# Eval("CenFacilityName")%>
                                                        </td>
                                                        <td>
                                                            <%#Eval("FromDate")%>
                                                        </td>
                                                        <td>
                                                            <%#Eval("ToDate")%>
                                                        </td>
                                                        <td>
                                                            <%#Eval("Status")%>
                                                        </td>
                                                        <td>
                                                            <%#Eval("PriorityLevel_Status") %>
                                                        </td>
                                                    </tr>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="btnEdit" />
                                                </Triggers>

                                            </asp:UpdatePanel>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </tbody>  </table>
                                        </FooterTemplate>
                                    </asp:Repeater>

                                </div>
                            </div>
                    </div>
                    </fieldset>
                                        
                </div>
            </div>
            </form>
            </div>

            </div>
            </div>
            </div>

            <table width="100%" cellpadding="1" cellspacing="1" class="table-formdata">
                <div id="divMsg" runat="server"></div>
                <tr>
                    <%--<td class="vista_page_title_bar" style="border-bottom: solid 1px #2E72BD; padding-left: 10px; height: 30px;" colspan="6">LEAVE TYPE ENTRY--%>
                    <%-- <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                            AlternateText="Page Help" ToolTip="Page Help" />--%>
                    <div id="Div1" style="display: none; overflow: hidden; z-index: 2; background-color: #FFFFFF; border: solid 1px #D0D0D0;">
                    </div>
                    </td>
                </tr>

                <tr>
                    <td>
                        <!-- "Wire frame" div used to transition from the button to the info panel -->
                        <div id="flyout" style="display: none; overflow: hidden; z-index: 2; background-color: #FFFFFF; border: solid 1px #D0D0D0;">
                        </div>
                        <!-- Info panel to be displayed as a flyout when the button is clicked -->
                        <div id="info" style="display: none; width: 250px; z-index: 2; opacity: 0; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=0); font-size: 12px; border: solid 1px #CCCCCC; background-color: #FFFFFF; padding: 5px;">
                            <div id="btnCloseParent" style="float: right; opacity: 0; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=0);">
                                <asp:LinkButton ID="btnClose" runat="server" OnClientClick="return false;" Text="X"
                                    ToolTip="Close" Style="background-color: #666666; color: #FFFFFF; text-align: center; font-weight: bold; text-decoration: none; border: outset thin #FFFFFF; padding: 5px;" />
                            </div>
                            <div>
                                <%--  <p class="page_help_head">
                                    <span style="font-weight: bold; text-decoration: underline;">Page Help</span><br />
                                    <asp:Image ID="imgEdit" runat="server" ImageUrl="~/images/edit.gif" AlternateText="Edit Record" />
                                    Edit Record
                            <asp:Image ID="imgDelete" runat="server" ImageUrl="~/images/delete.gif" AlternateText="Delete Record" />
                                    Delete Record
                                </p>--%>
                                <p class="page_help_text">
                                    <asp:Label ID="lblHelp" runat="server" Font-Names="Trebuchet MS" />
                                </p>
                            </div>
                        </div>

                        <script type="text/javascript" language="javascript">
                            // Move an element directly on top of another element (and optionally
                            // make it the same size)
                            function Cover(bottom, top, ignoreSize) {
                                var location = Sys.UI.DomElement.getLocation(bottom);
                                top.style.position = 'absolute';
                                top.style.top = location.y + 'px';
                                top.style.left = location.x + 'px';
                                if (!ignoreSize) {
                                    top.style.height = bottom.offsetHeight + 'px';
                                    top.style.width = bottom.offsetWidth + 'px';
                                }
                            }
                        </script>

                        <%--<ajaxToolKit:AnimationExtender ID="AnimationExtender1" runat="server" TargetControlID="btnHelp">
                            <Animations>
                                <OnClick>
                                    <Sequence>
                                        
                                        <EnableAction Enabled="false" />
                                        
                                        
                                        <ScriptAction Script="Cover($get('ctl00$ContentPlaceHolder1$btnHelp'), $get('flyout'));" />
                                        <StyleAction AnimationTarget="flyout" Attribute="display" Value="block"/>
                                        
                                        
                                        <ScriptAction Script="Cover($get('flyout'), $get('info'), true);" />
                                        <StyleAction AnimationTarget="info" Attribute="display" Value="block"/>
                                        <FadeIn AnimationTarget="info" Duration=".2"/>
                                        <StyleAction AnimationTarget="flyout" Attribute="display" Value="none"/>
                                        
                                      
                                        <Parallel AnimationTarget="info" Duration=".5">
                                            <Color PropertyKey="color" StartValue="#666666" EndValue="#FF0000" />
                                            <Color PropertyKey="borderColor" StartValue="#666666" EndValue="#FF0000" />
                                        </Parallel>
                                        <Parallel AnimationTarget="info" Duration=".5">
                                            <Color PropertyKey="color" StartValue="#FF0000" EndValue="#666666" />
                                            <Color PropertyKey="borderColor" StartValue="#FF0000" EndValue="#666666" />
                                            <FadeIn AnimationTarget="btnCloseParent" MaximumOpacity=".9" />
                                        </Parallel>
                                    </Sequence>
                                </OnClick>
                            </Animations>
                        </ajaxToolKit:AnimationExtender>--%>
                        
                    </td>
                </tr>
                <tr>
                    <td style="width: 5%"></td>
                    <td style="width: 1%"></td>
                    <%--<td colspan="4">Note <b>:</b> <span style="color: #FF0000">* Marked Is Mandatory !</span><br />--%>
                    </td>
                </tr>
            </table>
            <center>
      
                <%--<asp:Panel ID="pnlAdd" runat="server" Width="90%">--%>
                 
                        <fieldset >
                           <%-- <legend class="legend">Leave Type Entry</legend>   --%>                         
                              <table width="100%" cellpadding="1" cellspacing="1" border="0">
                                <%--<tr align="center" >
                                   <td style="width: 15%; padding-left:20%" align="left" >
                                     Leave Name <span style="color: #FF0000">*</span>
                                   </td>
                                   <td style ="width:1%"><b>:</b></td>
                                    <td style="width: 15%" align="left">
                                         <asp:TextBox ID="txtFacilityName" runat="server" MaxLength="50" Width="300px" onkeypress="return CheckAlphabet(event,this);" CssClass="textbox"/>
                                         <asp:RequiredFieldValidator ID="rfvHolyType" runat="server" ControlToValidate="txtFacilityName"
                                                        Display="None" ErrorMessage="Please Enter Leave Name" ValidationGroup="Facility"
                                                        SetFocusOnError="True">
                                         </asp:RequiredFieldValidator>
                                    </td>
                                </tr>--%>
                                <%-- <tr align="center" >
                                   <td style="width: 15%; padding-left:20%" align="left">
                                      Short Name <span style="color: #FF0000">*</span>
                                   </td>
                                   <td style ="width:1%"><b>:</b></td>
                                   <td style="width: 15%" align="left">
                                         <asp:TextBox ID="txtshortname" runat="server" MaxLength="50" Width="300px" onkeypress="return CheckAlphabet(event,this);" CssClass="textbox"/>
                                                    
                                                    <asp:RequiredFieldValidator ID="rfvshrtname" runat="server" ControlToValidate="txtshortname" 
                                                        Display="None" ErrorMessage="Please Enter Short Name" ValidationGroup="Facility"
                                                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>--%>
                                 <%--<tr align="center" >
                                       <td style="width: 15%; padding-left:20%" align="left" >
                                    Max Days<span style="color: #FF0000">*</span>
                                   </td>
                                   <td style ="width:1%"><b>:</b></td>
                                  <td style="width: 15%" align="left">
                                      <asp:TextBox ID="txtmaxday" runat="server" MaxLength="5" Width="150px" onkeypress="return CheckNumeric(event,this);" CssClass="textbox"/>
                                          <asp:RequiredFieldValidator ID="rfvmaxdays" runat="server" ControlToValidate="txtmaxday"
                                                        Display="None" ErrorMessage="Please Enter Max Days" ValidationGroup="Facility"
                                                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    
                                                                            
                                    </td>
                                </tr>--%>
                                  
                                 <%--<tr align="center" >
                                      <td style="width: 15%; padding-left:20%" align="left">
                                  Period<span style="color: #FF0000">*</span>
                                   </td>
                                   <td style ="width:1%"><b>:</b></td>
                                   <td style="width: 15%" align="left">
                                     <asp:RadioButtonList ID="rdbYearly" runat="server" RepeatDirection="Horizontal" Autopostback="true">
                                            <asp:ListItem Enabled="true" Text="Yearly" Value="Max_Days"></asp:ListItem>
                                            <asp:ListItem Enabled="true" Text="Half Yearly" Value="Yearly"></asp:ListItem>
                                                 </asp:RadioButtonList>
                                                 
                                             <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="rdbYearly"
                                                        Display="None" ErrorMessage="Please Select Period" ValidationGroup="Facility"
                                                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>--%>
                               
                                <tr>
                                    <td align="center" style="width: 499px">
                                        &nbsp
                                    </td>
                                </tr>
                                </table>
                        </fieldset>                  
                </asp:Panel>

                <center>
                    <%--<asp:Panel ID="pnlbutton" runat="server" Width="90%">
                                        <asp:Button ID="btnSave" runat="server" Text="Submit" ValidationGroup="Facility"
                                            Width="80px" onclick="btnSave_Click" />
                                        &nbsp;
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                                            OnClick="btnCancel_Click" Width="80px" />&nbsp;
                                        <asp:Button ID="btnBack" runat="server" Text="Back" CausesValidation="false" OnClick="btnBack_Click"
                                            Width="80px" />
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Facility"
                                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                            </asp:Panel>--%>
                </center>
                <%-- <tr>
                                    <td align="center" colspan="2" style="width: 599px">
                                        <asp:Label ID="lblerror" SkinID="Errorlbl" runat="server"></asp:Label>
                                        <asp:Label ID="lblmsg" runat="server" SkinID="lblmsg"></asp:Label>
                                    </td>
                                </tr>--%><%-- <asp:Panel ID="pnlList" runat="server">--%>
                <table cellpadding="0" cellspacing="0" style="width: 90%; text-align: center">
                    <%--<tr>
                            <td style="text-align: left; padding-left: 10px; padding-top: 10px;">
                              
                               <asp:LinkButton ID="btnAdd" runat="server" SkinID="LinkAddNew" OnClick="btnAdd_Click">Add New</asp:LinkButton>
                                
                                  <asp:Button ID="btnShowReport" runat="server" Text="Report" CausesValidation="false" OnClick="btnShowReport_Click"
                                            Width="80px" />
                               
                                
                              
                                              
                            </td>
                        </tr>--%>
                    <tr>
                        <td>&nbsp; </td>
                    </tr>
                    <tr>
                        <td align="center"><%--<asp:ListView ID="lvApplication" runat="server">--%>
                            <EmptyDataTemplate>
                                <br />
                                <%--<asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="Click Add New To Enter Leave Name" />--%>
                            </EmptyDataTemplate>
                            <%-- <LayoutTemplate>
                                        <div id="demp_grid" class="vista-grid">
                                            <div class="titlebar">
                                               Leave Types</div>
                                            <table class="datatable" cellpadding="0" cellspacing="0" width="100%">
                                                <tr class="header">
                                                    <th>
                                                        Action
                                                    </th>
                                                    <th>
                                                        Leave Name
                                                    </th>
                                                    <th>
                                                       Leave Short Name
                                                    </th>
                                                    <th>
                                                      Max Days
                                                    </th>
                                                    <th>
                                                     Yearly/Half
                                                    </th>
                                                    
                                                    
                                                </tr>
                                                <tr id="itemPlaceholder" runat="server" />
                                            </table>
                                        </div>
                                    </LayoutTemplate>--%>
                            <%-- <ItemTemplate>
                                        <tr class="item" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                            <td>
                                                <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("LVNO") %>'
                                                    AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />&nbsp;
                                                <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.gif" CommandArgument='<%# Eval("LVNO") %>'
                                                    AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                                    OnClientClick="showConfirmDel(this); return false;" />
                                            </td>
                                            <td>
                                                <%# Eval("Leave_Name")%>
                                            </td>
                                            <td>
                                              <%#Eval("Leave_ShortName")%>
                                            </td>
                                            <td>
                                              <%#Eval("Max_Days")%>
                                            </td>
                                            <td>
                                             <%#Eval("Yearly")%>
                                            </td>
                                           
                                            
                                        </tr>
                                    </ItemTemplate>
                                    <AlternatingItemTemplate>
                                        <tr class="altitem" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFD2'">
                                            <td>
                                                <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("LVNO") %>'
                                                    AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />&nbsp;
                                                <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.gif" CommandArgument='<%# Eval("LVNO") %>'
                                                    AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                                    OnClientClick="showConfirmDel(this); return false;" />
                                            </td>
                                            <td>
                                                <%# Eval("Leave_Name")%>
                                            </td>
                                            <td>
                                              <%#Eval("Leave_ShortName")%>
                                            </td>
                                            <td>
                                              <%#Eval("Max_Days")%>
                                            </td>
                                            <td>
                                             <%#Eval("Yearly")%>
                                                                                
                                        </tr>
                                    </AlternatingItemTemplate>
                                </asp:ListView>--%>
                            <%-- <div class="vista-grid_datapager">
                                <asp:DataPager ID="dpPager" runat="server" PagedControlID="lvApplication" PageSize="10"
                                OnPreRender="dpPager_PreRender">
                                <Fields>
                                <asp:NextPreviousPagerField FirstPageText="<<" PreviousPageText="<"  ButtonType="Link"
                                RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="true" ShowPreviousPageButton="true"
                                ShowLastPageButton="false" ShowNextPageButton="false" />
                                <asp:NumericPagerField ButtonType="Link" ButtonCount="7" CurrentPageLabelCssClass="Current" />
                                <asp:NextPreviousPagerField LastPageText=">>" NextPageText= ">"  ButtonType="Link"
                                RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="false" ShowPreviousPageButton="false"
                                ShowLastPageButton="true" ShowNextPageButton="true" />
                                
                                </Fields>
                                </asp:DataPager>
                                </div>--%></td>
                    </tr>
                </table>
                </asp:panel>
     
                              
   
      </center>
        </ContentTemplate>
        <%--<Triggers>
            <asp:PostBackTrigger ControlID="btnShowReport" />
        </Triggers>--%>
    </asp:UpdatePanel>
    <%--  Enable the button so it can be played again --%>           <%-- <%# Eval("Leave_Name")%>--%>
    <ajaxToolKit:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mdlPopupDel"
        runat="server" TargetControlID="div" PopupControlID="div" OkControlID="btnOkDel"
        OnOkScript="okDelClick();" CancelControlID="btnNoDel" OnCancelScript="cancelDelClick();"
        BackgroundCssClass="modalBackground" />

    <asp:Panel ID="div" runat="server" Style="display: none" CssClass="modalPopup">
        <div style="text-align: center">
            <table>
                <tr>
                    <td align="center">
                        <asp:Image ID="imgWarning" runat="server" ImageUrl="~/images/warning.png" />
                    </td>
                    <td>&nbsp;&nbsp;Are you sure you want to delete this record?
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
        ; debugger
        function ViewReason() {
            ; debugger
            alert("called");
            var isCancel = document.getElementById('ctl00_ContentPlaceHolder1_chkIsCancel').checked;
            alert(isCancel);
            if (isCancel = true) {
                //document.getElementById('ctl00_ContentPlaceHolder1_divCancel2').style.visibility = 'visible';
                document.getElementById('ctl00_ContentPlaceHolder1_txtReason').style.visibility = 'visible';
            }
            else {
                //document.getElementById('ctl00_ContentPlaceHolder1_divCancel2').sty.visibility = 'hidden';//ctl00_ContentPlaceHolder1_txtReason
                document.getElementById('ctl00_ContentPlaceHolder1_txtReason').sty.visibility = 'hidden';//ctl00_ContentPlaceHolder1_txtReason
            }

        }
        function Cleardate() {


            document.getElementById('ctl00_ContentPlaceHolder1_txtToDt').value = "";
            document.getElementById('ctl00_ContentPlaceHolder1_txtToDt').focus = "";

        }
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

</asp:Content>

