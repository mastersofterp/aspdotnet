<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="StudentScholarshipReport.aspx.cs" Inherits="ACADEMIC_REPORTS_StudentScholarshipReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="pnlFeeTable"
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

    <asp:UpdatePanel ID="pnlFeeTable" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">Student Scholarship Allotment/Adjustment Report</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-12">
                                        <%-- <asp:RadioButtonList ID="rblSelection" runat="server"
                                            RepeatDirection="Horizontal" AutoPostBack="True"
                                            OnSelectedIndexChanged="rblSelection_SelectedIndexChanged">
                                            <asp:ListItem Value="1" Selected="True">&nbsp;&nbsp;&nbsp;Degree & Branch Wise &nbsp;&nbsp;&nbsp;&nbsp;
                                              &nbsp;&nbsp;&nbsp;</asp:ListItem>
                                                        <asp:ListItem Value="2">&nbsp;&nbsp;&nbsp;Collection Wise  &nbsp;&nbsp;&nbsp;&nbsp;                               
                                              &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                                        <asp:ListItem Value="3">&nbsp;&nbsp;&nbsp;Session and Receipt Wise  &nbsp;&nbsp;&nbsp;&nbsp;                               
                                              &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                                        <asp:ListItem Value="4">&nbsp;&nbsp;&nbsp;Demand Session & Collection Session and Receipt Wise</asp:ListItem>
                                        </asp:RadioButtonList>--%>

                                        <asp:RadioButtonList ID="rblSelection" runat="server" Style="display: none"
                                            RepeatDirection="Horizontal" AutoPostBack="True">
                                            <asp:ListItem Value="4" Selected="True">&nbsp;&nbsp;&nbsp;Receipt Wise Demand Session And Collection Session&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                            <asp:ListItem Value="5">&nbsp;&nbsp;&nbsp;Excess Payment Report</asp:ListItem>
                                        </asp:RadioButtonList>

                                        <asp:Label ID="lblStudents" runat="server" Font-Bold="true" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="trAdmbatch" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Admission Batch</label>
                                        </div>
                                        <asp:DropDownList ID="ddlAdmBatch" runat="server" AppendDataBoundItems="True"
                                            ValidationGroup="Show" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvDept" runat="server"
                                            ControlToValidate="ddlAdmBatch" Display="None"
                                            ErrorMessage="Please Select Admission Batch" InitialValue="0" SetFocusOnError="true"
                                            ValidationGroup="Show"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Academic Year</label>
                                        </div>
                                        <asp:DropDownList ID="ddlAcdYear" runat="server" AutoPostBack="true" AppendDataBoundItems="true" TabIndex="6" ValidationGroup="show" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvAcademicYear" runat="server" ControlToValidate="ddlAcdYear"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Academic Year" ValidationGroup="Show" >
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12 ">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <%--lblDYddlYear--%>
                                            <asp:Label ID="lblDYddlYear" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlYear" runat="server" AppendDataBoundItems="True" ToolTip="Please Select Year" CssClass="form-control" data-select2-enable="true" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                       <%-- <asp:RequiredFieldValidator ID="rfvYear" runat="server" ControlToValidate="ddlYear"
                                            Display="None" ErrorMessage="Please Select Year" InitialValue="0" ValidationGroup="Show"></asp:RequiredFieldValidator>--%>
                                    </div>


                                    


                                    <div class="form-group col-lg-3 col-md-6 col-12" id="trSchool" runat="server">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>School/College</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSchClg" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                            TabIndex="2" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="trDegree" runat="server">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Degree</label>
                                        </div>
                                        <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                            CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="trBranch" runat="server">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Branch</label>
                                        </div>
                                        <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                            CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="trSemester" runat="server">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Semester</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                            ValidationGroup="Show" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <%-- <asp:RequiredFieldValidator ID="rfvSemester" runat="server"
                                        ControlToValidate="ddlSemester" Display="None"
                                        ErrorMessage="Please Select Semester" InitialValue="0" SetFocusOnError="true"
                                        ValidationGroup="Show"></asp:RequiredFieldValidator>--%>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none" id="trRec" runat="server">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Receipt type</label>
                                        </div>
                                        <asp:DropDownList ID="ddlRecType" runat="server" AppendDataBoundItems="True"
                                            ValidationGroup="Show" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <%--<asp:RequiredFieldValidator ID="rfvReceiptType" runat="server"
                                            ControlToValidate="ddlRecType" Display="None"
                                            ErrorMessage="Please Select Receipt Type" InitialValue="0" SetFocusOnError="true"
                                            ValidationGroup="Show"></asp:RequiredFieldValidator>--%>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divfromdate" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>From Date</label>
                                        </div>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i id="dvcal1" runat="server" class="fa fa-calendar text-green"></i>
                                            </div>
                                            <asp:TextBox ID="txtFromDate" runat="server" ValidationGroup="Show" onpaste="return false;"
                                                TabIndex="3" ToolTip="Please Enter Session Start Date" CssClass="form-control" Style="z-index: 0;" />
                                            <%-- <asp:Image ID="imgStartDate" runat="server" ImageUrl="~/images/calendar.png" ToolTip="Select Date"
                                                AlternateText="Select Date" Style="cursor: pointer" />--%>
                                            <ajaxToolKit:CalendarExtender ID="ceFromDate" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtFromDate" PopupButtonID="dvcal1" />
                                            <asp:RequiredFieldValidator ID="rfvFromDate" runat="server" ControlToValidate="txtFromDate"
                                                Display="None" ErrorMessage="Please Enter From   Date" SetFocusOnError="True"
                                                ValidationGroup="Show" />
                                            <ajaxToolKit:MaskedEditExtender ID="meeFromDate" runat="server" OnInvalidCssClass="errordate"
                                                TargetControlID="txtFromDate" Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date"
                                                DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True" />
                                            <ajaxToolKit:MaskedEditValidator ID="mevFromDate" runat="server" ControlExtender="meeFromDate"
                                                ControlToValidate="txtFromDate" EmptyValueMessage="Please Enter From Date"
                                                InvalidValueMessage="From Date is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                                TooltipMessage="Please Enter Start Date" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                                ValidationGroup="Show" SetFocusOnError="True" />
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divtodate" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>To Date</label>
                                        </div>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i id="dvcal2" runat="server" class="fa fa-calendar text-green"></i>
                                            </div>
                                            <asp:TextBox ID="txtToDate" runat="server" ValidationGroup="Show" TabIndex="4"
                                                ToolTip="Please Enter To Date" CssClass="form-control" Style="z-index: 0;" />
                                            <%-- <asp:Image ID="imgEDate" runat="server" ImageUrl="~/images/calendar.png" ToolTip="Select Date"
                                                AlternateText="Select Date" Style="cursor: pointer" />--%>
                                            <ajaxToolKit:CalendarExtender ID="ceToDate" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtToDate" PopupButtonID="dvcal2" />
                                            <asp:RequiredFieldValidator ID="rfvToDate" runat="server" SetFocusOnError="True"
                                                ErrorMessage="Please Enter To Date" ControlToValidate="txtToDate" Display="None"
                                                ValidationGroup="Show" />
                                            <ajaxToolKit:MaskedEditExtender ID="meeToDate" runat="server" OnInvalidCssClass="errordate"
                                                TargetControlID="txtToDate" Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date"
                                                DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True" />
                                            <ajaxToolKit:MaskedEditValidator ID="mevToDate" runat="server" ControlExtender="meeToDate"
                                                ControlToValidate="txtToDate" EmptyValueMessage="Please Enter To Date"
                                                InvalidValueMessage="To Date is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                                TooltipMessage="Please Enter To Date" EmptyValueBlurredText="Empty"
                                                InvalidValueBlurredMessage="Invalid Date" ValidationGroup="Show" SetFocusOnError="True" />
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="trReport" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <label>Report</label>
                                        </div>
                                        <asp:RadioButtonList ID="rdbReport" runat="server" RepeatDirection="Horizontal"
                                            AutoPostBack="true" RepeatColumns="2">
                                            <asp:ListItem Selected="True" Value="1">&nbsp;&nbsp;DD Student List &nbsp;&nbsp;&nbsp;</asp:ListItem>
                                            <%--<asp:ListItem Value="2">SBI Collect Student List</asp:ListItem>--%>
                                            <asp:ListItem Value="2">&nbsp;&nbsp;Cash Student List</asp:ListItem>

                                        </asp:RadioButtonList>
                                    </div>

                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <%-- <asp:Button ID="btnShow" runat="Server" Text="Show Data" ValidationGroup="Show" OnClick="btnShow_Click" />--%>
                                <%--<asp:Button ID="btnReport" runat="Server" Text="Report" ValidationGroup="Show" OnClick="btnReport_Click"
                                    CssClass="btn btn-info" />--%>
                                <asp:Button ID="btnExcel" runat="Server" Text="Excel Report" ValidationGroup="Show"
                                    OnClick="btnExcel_Click" CssClass="btn btn-info" />
                                <asp:Button ID="btnDCRExcelReport" runat="server" Text="Scholarship DCR Excel Report" ValidationGroup="Show" OnClick="btnDCRExcelReport_Click" CssClass="btn btn-info" />
                                <asp:Button ID="btnSummaryReport" runat="Server" Text="Summary Report" ValidationGroup="Show"
                                    OnClick="btnSummaryReport_Click" CssClass="btn btn-info" Visible="false" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" OnClick="btnCancel_Click" />
                                <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="Show" />
                                <asp:Label ID="lblStatus" runat="server" SkinID="Errorlbl" />

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <%--<asp:PostBackTrigger ControlID="btnReport" />--%>
            <asp:PostBackTrigger ControlID="btnExcel" />
            <asp:PostBackTrigger ControlID="btnCancel" />
            <asp:PostBackTrigger ControlID="btnSummaryReport" />
            <asp:PostBackTrigger ControlID="btnDCRExcelReport" />
            
        </Triggers>
    </asp:UpdatePanel>

    <div id="divMsg" runat="server">
    </div>

    <script language="javascript" type="text/javascript">
        function onreport() {

            var a = document.getElementById("ctl00_ContentPlaceHolder1_rdbReport_4");
            if (a.checked) {
                document.getElementById("ctl00_ContentPlaceHolder1_rfvDegree").enabled = false;
                document.getElementById("ctl00_ContentPlaceHolder1_rfvBranch").enabled = false;
                document.getElementById("ctl00_ContentPlaceHolder1_rfvProgram").enabled = false;
                document.getElementById("ctl00_ContentPlaceHolder1_rfvSemester").enabled = false;
            }
        }
    </script>
</asp:Content>
