<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="AttendanceReport.aspx.cs"
    Inherits="Academic_AttendanceReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updtime"
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

    <asp:UpdatePanel ID="updtime" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <%--<h3 class="box-title">Student Attendance Report</h3>--%>
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--  <label>College & Scheme</label>--%>
                                            <asp:Label ID="lblDYddlColgScheme" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlClgname" runat="server" AppendDataBoundItems="true" AutoPostBack="True" CssClass="form-control" TabIndex="1"
                                            ValidationGroup="offered" OnSelectedIndexChanged="ddlClgname_SelectedIndexChanged" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvCname" runat="server" ControlToValidate="ddlClgname"
                                            Display="None" ErrorMessage="Please Select College & Regulation" InitialValue="0" ValidationGroup="submit">
                                        </asp:RequiredFieldValidator>
                                    </div>


                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <%--<label>Session</label>--%>
                                            <asp:Label ID="lblDYddlSession" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlTerm" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlTerm_SelectedIndexChanged" data-select2-enable="true" TabIndex="1">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Degree</label>
                                        </div>
                                        <asp:DropDownList ID="ddlDegree" runat="server" ValidationGroup="Submit" CssClass="form-control" data-select2-enable="true"
                                            TabIndex="2" AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                            ErrorMessage="Please Select Degree" InitialValue="0"
                                            Display="None"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Scheme</label>
                                        </div>
                                        <asp:DropDownList ID="ddlDept" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged" TabIndex="3">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlDept"
                                            Display="None" ErrorMessage="Please Select Scheme" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Scheme</label>
                                        </div>
                                        <asp:DropDownList ID="ddlDegScheme" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlDegScheme_SelectedIndexChanged" TabIndex="3">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlDegScheme"
                                            Display="None" ErrorMessage="Please Select Scheme" InitialValue="0" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Semester</label>
                                        </div>
                                        <asp:DropDownList ID="ddlsemester" runat="server" ValidationGroup="Submit" CssClass="form-control" data-select2-enable="true"
                                            AppendDataBoundItems="true" TabIndex="4" OnSelectedIndexChanged="ddlsemester_SelectedIndexChanged"
                                            AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSem" runat="server" ControlToValidate="ddlsemester"
                                            ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="Submit"
                                            Display="None"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Subject Type</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSubjectType" runat="server" AutoPostBack="true" CssClass="form-control" data-select2-enable="true"
                                            OnSelectedIndexChanged="ddlSubjectType_SelectedIndexChanged"
                                            ValidationGroup="Submit" TabIndex="5">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <asp:ListItem Value="1">Theory</asp:ListItem>
                                            <asp:ListItem Value="2">Practical</asp:ListItem>
                                            <asp:ListItem Value="4">Project</asp:ListItem>
                                            <asp:ListItem Value="5">Seminar</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSubjectType" runat="server" ControlToValidate="ddlSubjectType"
                                            ErrorMessage="Please Select Subject Type" InitialValue="0" ValidationGroup="Submit"
                                            Display="None"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Course</label>
                                        </div>
                                        <asp:DropDownList ID="ddlCourse" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged" TabIndex="6">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlCourse"
                                            Display="None" ErrorMessage="Please Select Course" InitialValue="0" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Division</label>
                                        </div>
                                        <asp:DropDownList ID="ddlDivision" runat="server" ValidationGroup="Submit" CssClass="form-control" data-select2-enable="true"
                                            TabIndex="220" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" AppendDataBoundItems="True"
                                            AutoPostBack="True">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvDivision" runat="server" ControlToValidate="ddlDivision"
                                            ErrorMessage="Please Select Division" InitialValue="0" ValidationGroup="Show"
                                            Display="None"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Batch</label>
                                        </div>
                                        <asp:DropDownList ID="ddlBatch" runat="server" AppendDataBoundItems="true" ValidationGroup="Submit" CssClass="form-control" data-select2-enable="true"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlBatch_SelectedIndexChanged"
                                            TabIndex="221">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Faculty</label>
                                        </div>
                                        <asp:DropDownList ID="ddlFaculty" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true"
                                            TabIndex="7" OnSelectedIndexChanged="ddlFaculty_SelectedIndexChanged" AutoPostBack="True">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvFaculty" runat="server" ControlToValidate="ddlFaculty"
                                            Display="None" ErrorMessage="Please Select Faculty" InitialValue="0" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>From Date</label>
                                        </div>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar" id="imgCalResultDate"></i>
                                            </div>
                                            <asp:TextBox ID="txtAttDate" runat="server" TabIndex="8" ValidationGroup="Submit" />
                                            <ajaxToolKit:CalendarExtender ID="ceResultDate" runat="server" Format="dd/MM/yyyy"
                                                PopupButtonID="imgCalResultDate" TargetControlID="txtAttDate" />
                                            <ajaxToolKit:MaskedEditExtender ID="meeResultDate" runat="server" ErrorTooltipEnabled="true"
                                                Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true" OnInvalidCssClass="errordate"
                                                TargetControlID="txtAttDate" />
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>To Date</label>
                                        </div>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar" id="Image1"></i>
                                            </div>
                                            <asp:TextBox ID="txtRecDate" runat="server" TabIndex="9" ToolTip="Please Enter To Date" />
                                            <ajaxToolKit:CalendarExtender ID="cerecdate" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                PopupButtonID="Image1" TargetControlID="txtRecDate">
                                            </ajaxToolKit:CalendarExtender>
                                            <ajaxToolKit:MaskedEditExtender ID="meeFromDate" runat="server" TargetControlID="txtRecDate"
                                                Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                AcceptNegative="Left" ErrorTooltipEnabled="True" />
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="trRowMonth" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <label>Month & Year</label>
                                        </div>
                                        <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="66">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <asp:ListItem Value="01">JAN</asp:ListItem>
                                            <asp:ListItem Value="02">FEB</asp:ListItem>
                                            <asp:ListItem Value="03">MAR</asp:ListItem>
                                            <asp:ListItem Value="04">APR</asp:ListItem>
                                            <asp:ListItem Value="05">MAY</asp:ListItem>
                                            <asp:ListItem Value="06">JUNE</asp:ListItem>
                                            <asp:ListItem Value="07">JULY</asp:ListItem>
                                            <asp:ListItem Value="08">AUG</asp:ListItem>
                                            <asp:ListItem Value="09">SEPT</asp:ListItem>
                                            <asp:ListItem Value="10">OCT</asp:ListItem>
                                            <asp:ListItem Value="11">NOV</asp:ListItem>
                                            <asp:ListItem Value="12">DEC</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Year</label>
                                        </div>
                                        <asp:DropDownList ID="ddlYear" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                            TabIndex="77">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvMonth" runat="server" ControlToValidate="ddlMonth"
                                            Display="None" ErrorMessage="Please Select Month" InitialValue="0" ValidationGroup="Submit" />
                                        <asp:RequiredFieldValidator ID="rfvYear" runat="server" ControlToValidate="ddlYear"
                                            Display="None" ErrorMessage="Please Select Year" InitialValue="0" ValidationGroup="Submit" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="trStudent" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <label>Select Student</label>
                                        </div>
                                        <asp:DropDownList ID="ddlStudentConsolidate" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true"
                                            TabIndex="88">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvStudentConslidate" runat="server" ControlToValidate="ddlStudentConsolidate"
                                            Display="None" ErrorMessage="Please Select Student" InitialValue="0" ValidationGroup="Student"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnDayWise" runat="server" Text="Day-Wise Report" ValidationGroup="Submit"
                                    TabIndex="999" OnClick="btnDayWise_Click" Visible="False" />
                                <asp:Button ID="btnUpToDate" runat="server" Text="UpTo Date Report" CssClass="btn btn-info"
                                    ValidationGroup="Submit" TabIndex="10" OnClick="btnUpToDate_Click" />
                                <asp:Button ID="btnEndTerm" runat="server" Text="Term End Report*" Visible="false"
                                    ValidationGroup="Student" TabIndex="111" OnClick="btnEndTerm_Click" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                                    TabIndex="11" OnClick="btnCancel_Click1" CssClass="btn btn-warning" />
                                <asp:Button ID="btnFilledAtt" runat="server" Text="Filled Attendance" Width="150px"
                                    TabIndex="133" OnClick="btnFilledAtt_Click" />
                                <asp:Button ID="btnConsolidate" runat="server" Text="Consolidated Report"
                                    ValidationGroup="Submit" OnClick="btnConsolidate_Click" TabIndex="144" />
                                <asp:Button ID="btnStudentConsolidate" runat="server" Text="Student Consolidated Report"
                                    ValidationGroup="Student" OnClick="btnStudentConsolidate_Click"
                                    TabIndex="155" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                    ShowMessageBox="True" ShowSummary="False" ValidationGroup="Submit" Style="text-align: center" />
                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List"
                                    ShowMessageBox="True" ShowSummary="False" ValidationGroup="Student" Style="text-align: center" />
                            </div>

                            <div class="col-12 table table-responsive">
                                <asp:GridView ID="lvDayWise" runat="server" BackColor="White" BorderColor="#3366CC"
                                    BorderStyle="None" BorderWidth="1px" CellPadding="4">
                                    <RowStyle BackColor="White" ForeColor="#003399" />
                                    <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                                    <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                                    <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                    <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
                                </asp:GridView>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Label ID="lblCurSession" runat="server" Visible="false"></asp:Label>
                                <asp:Label ID="lblUserNo" runat="server" Visible="false"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div id="divMsg" runat="server">
            </div>
        </ContentTemplate>
        <Triggers>   
            <asp:PostBackTrigger ControlID="btnCancel" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
