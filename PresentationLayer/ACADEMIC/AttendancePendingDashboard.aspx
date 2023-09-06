<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AttendancePendingDashboard.aspx.cs" Inherits="ACADEMIC_AttendancePendingDashboard" MasterPageFile="~/SiteMasterPage.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%-- <style>
        td, th {
            border: 1px solid #e5e5e5 !important;
            padding: 0.4rem !important;
        }
    </style>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updMarksEntryDetailReport"
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
    <%-- <form role="form">--%>

    <asp:UpdatePanel runat="server" ID="updMarksEntryDetailReport">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label></label>
                                        </div>
                                        <asp:RadioButtonList ID="rdblBulkEmail" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rdblBulkEmail_SelectedIndexChanged"
                                            AppendDataBoundItems="true" AutoPostBack="true">
                                            <asp:ListItem Value="1" Selected="true"> Single Email Sending&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                            <asp:ListItem Value="2">Bulk Email Sending</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>

                                </div>

                            </div>
                            <div runat="server" id="divSingle">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <asp:Label ID="lblDYddlSession" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlSession" AppendDataBoundItems="true" CssClass="form-control" placeholder="Please Select Session" runat="server" data-select2-enable="true">
                                                <%--OnSelectedIndexChanged="ddlSession_OnSelectedIndexChanged" AutoPostBack="true"--%>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                                Display="None" ErrorMessage="Please Select Session" InitialValue="0" SetFocusOnError="true"
                                                ValidationGroup="Show">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>From Date</label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar" id="cal1" style="cursor: pointer"></i>
                                                </div>
                                                <asp:TextBox ID="txtFromDate" runat="server" ValidationGroup="submit" onpaste="return false;"
                                                    TabIndex="4" ToolTip="Please Enter From Date" CssClass="form-control" />
                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                                    TargetControlID="txtFromDate" PopupButtonID="cal1" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtFromDate"
                                                    Display="None" ErrorMessage="Please Enter From Date" SetFocusOnError="True"
                                                    ValidationGroup="Show" />
                                                <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" OnInvalidCssClass="errordate"
                                                    TargetControlID="txtFromDate" Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date"
                                                    DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True" />
                                                <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender1"
                                                    ControlToValidate="txtFromDate" EmptyValueMessage="Please Enter Start Date"
                                                    InvalidValueMessage="From Date is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                                    TooltipMessage="Please Enter Start Date" InvalidValueBlurredMessage="Invalid Date"
                                                    ValidationGroup="submit" SetFocusOnError="True" />
                                            </div>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>To Date</label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar" id="cal2" style="cursor: pointer"></i>
                                                </div>
                                                <asp:TextBox ID="txtToDate" runat="server" ValidationGroup="submit" onpaste="return false;"
                                                    TabIndex="5" ToolTip="Please Enter Session Start Date" CssClass="form-control" />
                                                <ajaxToolKit:CalendarExtender ID="ceStartDate" runat="server" Format="dd/MM/yyyy"
                                                    TargetControlID="txtToDate" PopupButtonID="cal2" />
                                                <asp:RequiredFieldValidator ID="rfvStartDate" runat="server" ControlToValidate="txtToDate"
                                                    Display="None" ErrorMessage="Please Enter To Date" SetFocusOnError="True"
                                                    ValidationGroup="Show" />
                                                <ajaxToolKit:MaskedEditExtender ID="meeStartDate" runat="server" OnInvalidCssClass="errordate"
                                                    TargetControlID="txtToDate" Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date"
                                                    DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True" />
                                                <ajaxToolKit:MaskedEditValidator ID="mevStartDate" runat="server" ControlExtender="meeStartDate"
                                                    ControlToValidate="txtToDate" EmptyValueMessage="Please Enter To Date"
                                                    InvalidValueMessage="To Date is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                                    TooltipMessage="Please Enter To Date" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                                    ValidationGroup="submit" SetFocusOnError="True" />
                                            </div>
                                        </div>
                                    </div>
                                </div>


                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnShow" runat="server" Text="Show" CssClass="btn btn-primary" ValidationGroup="Show" OnClick="btnShow_OnClick" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Show"
                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                </div>

                                <div class="col-12">
                                    <div class="table-responsive">
                                        <asp:GridView ID="gvParent" runat="server" Width="100%"
                                            CssClass="table table-striped table-bordered nowrap" AutoGenerateColumns="false" OnRowDataBound="gvParent_RowDataBound"
                                            GridLines="Horizontal"
                                            ShowFooter="false" Visible="true" EmptyDataText="There are no data records to display.">

                                            <HeaderStyle CssClass="bg-light-blue" />
                                            <Columns>
                                                <asp:BoundField DataField="UA_FULLNAME" HeaderText="Faculty Name" HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="SHORTNAME" HeaderText="Program" HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="DEPARTMENT" HeaderText="Department" HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="SCHOOL" HeaderText="School" HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="UA_EMAIL" HeaderText="Email Id" HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="UA_MOBILE" HeaderText="Mobile No." HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Center" />
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Subjects"
                                                    HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <div id="divcR" runat="server">
                                                            <a href="JavaScript:divexpandcollapse('div1<%# Eval("COURSENO") %><%# Eval("UA_NO") %>');">
                                                                <%--  <img alt='<%# Eval("COURSE_MARKS_PENDING") %>' id='CLOSE<%# Eval("SRNO") %>' border="0" title='<%# Eval("COURSE_MARKS_PENDING") %>' />--%>
                                                                <asp:Label runat="server" ID="lbl" Text='<%# Eval("CCODE") %>'></asp:Label>
                                                                <asp:HiddenField ID="hdfTempExam" runat="server" Value='<%# Eval("COURSENO") %>' />
                                                                <asp:HiddenField ID="hdfUano" runat="server" Value='<%# Eval("UA_NO") %>' />
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Send E-Mail" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnSendDeptMail" OnClick="btnSendDeptMail_Click" CssClass="btn btn-primary btn-sm" ValidationGroup='<%# Eval("UA_EMAIL") %>'
                                                            Text="Send Mail" runat="server" />
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle Width="15%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td colspan="100%">
                                                                <div id='div1<%# Eval("COURSENO") %><%# Eval("UA_NO") %>' style="display: none; position: relative; left: 30px; overflow: auto">
                                                                    <asp:GridView ID="gvChild" runat="server" DataKeyNames="UA_NO" AutoGenerateColumns="false"
                                                                        CssClass="table table-striped table-bordered nowrap" OnRowDataBound="gvChild_RowDataBound"
                                                                        Width="95%" ShowFooter="false" ShowHeaderWhenEmpty="true" EmptyDataText="No data Found">
                                                                        <HeaderStyle CssClass="bg-light-blue" />
                                                                        <FooterStyle Font-Bold="true" ForeColor="White" />
                                                                        <RowStyle />
                                                                        <AlternatingRowStyle BackColor="White" />
                                                                        <HeaderStyle CssClass="bg-light-blue" />
                                                                        <Columns>
                                                                            <asp:BoundField DataField="SCHEMENAME" ItemStyle-Width="30%" HeaderText="Scheme" HeaderStyle-HorizontalAlign="Center"
                                                                                ItemStyle-HorizontalAlign="Center" />
                                                                            <asp:BoundField DataField="SECTION" ItemStyle-Width="40%" HeaderText="SECTION" HeaderStyle-HorizontalAlign="Center"
                                                                                ItemStyle-HorizontalAlign="Center" />
                                                                            <asp:BoundField DataField="SEMESTER" ItemStyle-Width="40%" HeaderText="SEMESTER" HeaderStyle-HorizontalAlign="Center"
                                                                                ItemStyle-HorizontalAlign="Center" />
                                                                            <asp:TemplateField ItemStyle-Width="30%" ItemStyle-HorizontalAlign="Center" HeaderText="Attendance Pending Count"
                                                                                HeaderStyle-HorizontalAlign="Center">
                                                                                <ItemTemplate>
                                                                                    <div id="divccR" runat="server">
                                                                                        <a href="JavaScript:divexpandcollapse('div2<%# Eval("UA_NO") %>');">
                                                                                            <%--  <img alt='<%# Eval("COURSE_MARKS_PENDING") %>' id='CLOSE<%# Eval("SRNO") %>' border="0" title='<%# Eval("COURSE_MARKS_PENDING") %>' />--%>
                                                                                            <asp:Label runat="server" ID="lbl" Text='<%# Eval("PENDING_ATT_COUNT") %>'></asp:Label>
                                                                                            <asp:HiddenField ID="hdfCourseno" runat="server" Value='<%# Eval("COURSENO") %>' />
                                                                                            <asp:HiddenField ID="hdfSecno" runat="server" Value='<%# Eval("SECTIONNO") %>' />
                                                                                            <asp:HiddenField ID="hdfSemno" runat="server" Value='<%# Eval("SEMESTERNO") %>' />
                                                                                            <asp:HiddenField ID="hdfSchemeno" runat="server" Value='<%# Eval("SCHEMENO") %>' />
                                                                                            <asp:HiddenField ID="hdnUaNo" runat="server" Value='<%# Eval("UA_NO") %>' />
                                                                                    </div>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <tr>
                                                                                        <td colspan="100%">
                                                                                            <div id='div2<%# Eval("UA_NO") %>' style="display: none; position: relative; left: 30px; overflow: auto">
                                                                                                <asp:GridView ID="gvChildAttDates" runat="server" AutoGenerateColumns="false"
                                                                                                    CssClass="table table-striped table-bordered nowrap"
                                                                                                    Width="95%" ShowFooter="false" ShowHeaderWhenEmpty="true" EmptyDataText="No data Found">
                                                                                                    <HeaderStyle CssClass="bg-light-blue" />
                                                                                                    <FooterStyle Font-Bold="true" ForeColor="White" />
                                                                                                    <RowStyle />
                                                                                                    <AlternatingRowStyle BackColor="White" />
                                                                                                    <HeaderStyle CssClass="bg-light-blue" />
                                                                                                    <Columns>
                                                                                                        <asp:BoundField DataField="ATT_DATE" ItemStyle-Width="30%" HeaderText="DATE" HeaderStyle-HorizontalAlign="Center"
                                                                                                            ItemStyle-HorizontalAlign="Center" />
                                                                                                        <asp:BoundField DataField="SLOT" ItemStyle-Width="40%" HeaderText="SLOT" HeaderStyle-HorizontalAlign="Center"
                                                                                                            ItemStyle-HorizontalAlign="Center" />
                                                                                                        <asp:BoundField DataField="TOPIC_COVERED" ItemStyle-Width="40%" HeaderText="TOPIC COVERED" HeaderStyle-HorizontalAlign="Center"
                                                                                                            ItemStyle-HorizontalAlign="Center" />

                                                                                                    </Columns>
                                                                                                </asp:GridView>
                                                                                            </div>
                                                                                        </td>
                                                                                    </tr>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>


                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>

                            <div runat="server" id="divBulk" visible="false">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <asp:Label ID="lblDYddlSession_tab" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlSessionBulk" AppendDataBoundItems="true" CssClass="form-control" placeholder="Please Select Session" runat="server" data-select2-enable="true">
                                                <%--OnSelectedIndexChanged="ddlSession_OnSelectedIndexChanged" AutoPostBack="true"--%>
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSessionBulk"
                                                Display="None" ErrorMessage="Please Select Session" InitialValue="0" SetFocusOnError="true"
                                                ValidationGroup="BulkShow">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>From Date</label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar" id="cal3" style="cursor: pointer"></i>
                                                </div>
                                                <asp:TextBox ID="txtbulkFDate" runat="server" ValidationGroup="submit" onpaste="return false;"
                                                     ToolTip="Please Enter From Date" CssClass="form-control" />
                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                                    TargetControlID="txtbulkFDate" PopupButtonID="cal3" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtbulkFDate"
                                                    Display="None" ErrorMessage="Please Enter From Date" SetFocusOnError="True"
                                                    ValidationGroup="BulkShow" />
                                                <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" OnInvalidCssClass="errordate"
                                                    TargetControlID="txtbulkFDate" Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date"
                                                    DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True" />
                                                <%--<ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="MaskedEditExtender2"
                                                    ControlToValidate="txtbulkFDate" EmptyValueMessage="Please Enter Start Date"
                                                    InvalidValueMessage="From Date is Invalid (Enter dd/MM/yyyy Format)1" Display="None"
                                                    TooltipMessage="Please Enter Start Date" InvalidValueBlurredMessage="Invalid Date"
                                                    ValidationGroup="BulkShow" SetFocusOnError="True" />--%>
                                            </div>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>To Date</label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar" id="cal4" style="cursor: pointer"></i>
                                                </div>
                                                <asp:TextBox ID="txtbulkTDate" runat="server" ValidationGroup="submit" onpaste="return false;"
                                                    ToolTip="Please Enter Session Start Date" CssClass="form-control" />
                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy"
                                                    TargetControlID="txtbulkTDate" PopupButtonID="cal4" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtbulkTDate"
                                                    Display="None" ErrorMessage="Please Enter To Date" SetFocusOnError="True"
                                                    ValidationGroup="BulkShow" />
                                                <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" OnInvalidCssClass="errordate"
                                                    TargetControlID="txtbulkTDate" Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date"
                                                    DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True" />
                                                <%--<ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator3" runat="server" ControlExtender="MaskedEditExtender3"
                                                    ControlToValidate="txtbulkTDate" EmptyValueMessage="Please Enter To Date"
                                                    InvalidValueMessage="To Date is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                                    TooltipMessage="Please Enter To Date" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                                    ValidationGroup="BulkShow" SetFocusOnError="True" />--%>
                                            </div>
                                        </div>
                                    </div>
                                </div>


                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnbulkShow" runat="server" Text="Show" CssClass="btn btn-primary" ValidationGroup="BulkShow" OnClick="btnbulkShow_Click" />
                                    <asp:Button ID="btnSendEmail" runat="server" Text="Send Email" CssClass="btn btn-primary" 
                                        ValidationGroup="BulkShow" OnClick="btnSendEmail_Click" Enabled="false" />
                                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="BulkShow"
                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />

                                </div>


                                <div class="col-12">
                                    <div class="table-responsive">
                                        <asp:GridView ID="gvBulkEmail" runat="server" Width="100%"
                                            CssClass="table table-striped table-bordered nowrap" AutoGenerateColumns="false" OnRowDataBound="gvBulkEmail_RowDataBound"
                                            GridLines="Horizontal"
                                            ShowFooter="false" Visible="true" EmptyDataText="There are no data records to display.">

                                            <HeaderStyle CssClass="bg-light-blue" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Select" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkSelect" runat="server" />
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle Width="5%" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="UA_FULLNAME" HeaderText="Faculty Name" HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="SHORTNAME" HeaderText="Program" HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="DEPARTMENT" HeaderText="Department" HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="SCHOOL" HeaderText="School" HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="UA_EMAIL" HeaderText="Email Id" HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="UA_MOBILE" HeaderText="Mobile No." HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Center" />
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Subjects"
                                                    HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <div id="divcR" runat="server">
                                                            <a href="JavaScript:divexpandcollapse('div1<%# Eval("COURSENO") %><%# Eval("UA_NO") %>');">
                                                                <%--  <img alt='<%# Eval("COURSE_MARKS_PENDING") %>' id='CLOSE<%# Eval("SRNO") %>' border="0" title='<%# Eval("COURSE_MARKS_PENDING") %>' />--%>
                                                                <asp:Label runat="server" ID="lbl" Text='<%# Eval("CCODE") %>'></asp:Label>
                                                                <asp:HiddenField ID="hdfTempExam" runat="server" Value='<%# Eval("COURSENO") %>' />
                                                                <asp:HiddenField ID="hdfUano" runat="server" Value='<%# Eval("UA_NO") %>' />
                                                                <asp:HiddenField ID="hdnEmail" runat="server" Value='<%# Eval("UA_EMAIL") %>' />
                                                                <asp:HiddenField ID="hdnUaname" runat="server" Value='<%# Eval("UA_FULLNAME") %>' />
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Email Subject" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Center" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtSubjectBulk" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle Width="30%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Email Message" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Center" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtEmailMsg" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle Width="30%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td colspan="100%">
                                                                <div id='div1<%# Eval("COURSENO") %><%# Eval("UA_NO") %>' style="display: none; position: relative; left: 30px; overflow: auto">
                                                                    <asp:GridView ID="gvChild" runat="server" DataKeyNames="UA_NO" AutoGenerateColumns="false"
                                                                        CssClass="table table-striped table-bordered nowrap" OnRowDataBound="gvChild_RowDataBound"
                                                                        Width="95%" ShowFooter="false" ShowHeaderWhenEmpty="true" EmptyDataText="No data Found">
                                                                        <HeaderStyle CssClass="bg-light-blue" />
                                                                        <FooterStyle Font-Bold="true" ForeColor="White" />
                                                                        <RowStyle />
                                                                        <AlternatingRowStyle BackColor="White" />
                                                                        <HeaderStyle CssClass="bg-light-blue" />
                                                                        <Columns>
                                                                            <asp:BoundField DataField="SCHEMENAME" ItemStyle-Width="30%" HeaderText="Scheme" HeaderStyle-HorizontalAlign="Center"
                                                                                ItemStyle-HorizontalAlign="Center" />
                                                                            <asp:BoundField DataField="SECTION" ItemStyle-Width="40%" HeaderText="SECTION" HeaderStyle-HorizontalAlign="Center"
                                                                                ItemStyle-HorizontalAlign="Center" />
                                                                            <asp:BoundField DataField="SEMESTER" ItemStyle-Width="40%" HeaderText="SEMESTER" HeaderStyle-HorizontalAlign="Center"
                                                                                ItemStyle-HorizontalAlign="Center" />
                                                                            <asp:TemplateField ItemStyle-Width="30%" ItemStyle-HorizontalAlign="Center" HeaderText="Attendance Pending Count"
                                                                                HeaderStyle-HorizontalAlign="Center">
                                                                                <ItemTemplate>
                                                                                    <div id="divccR" runat="server">
                                                                                        <a href="JavaScript:divexpandcollapse('div2<%# Eval("UA_NO") %>');">
                                                                                            <%--  <img alt='<%# Eval("COURSE_MARKS_PENDING") %>' id='CLOSE<%# Eval("SRNO") %>' border="0" title='<%# Eval("COURSE_MARKS_PENDING") %>' />--%>
                                                                                            <asp:Label runat="server" ID="lbl" Text='<%# Eval("PENDING_ATT_COUNT") %>'></asp:Label>
                                                                                            <asp:HiddenField ID="hdfCourseno" runat="server" Value='<%# Eval("COURSENO") %>' />
                                                                                            <asp:HiddenField ID="hdfSecno" runat="server" Value='<%# Eval("SECTIONNO") %>' />
                                                                                            <asp:HiddenField ID="hdfSemno" runat="server" Value='<%# Eval("SEMESTERNO") %>' />
                                                                                            <asp:HiddenField ID="hdfSchemeno" runat="server" Value='<%# Eval("SCHEMENO") %>' />
                                                                                            <asp:HiddenField ID="hdnUaNo" runat="server" Value='<%# Eval("UA_NO") %>' />
                                                                                    </div>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <tr>
                                                                                        <td colspan="100%">
                                                                                            <div id='div2<%# Eval("UA_NO") %>' style="display: none; position: relative; left: 30px; overflow: auto">
                                                                                                <asp:GridView ID="gvChildAttDates" runat="server" AutoGenerateColumns="false"
                                                                                                    CssClass="table table-striped table-bordered nowrap"
                                                                                                    Width="95%" ShowFooter="false" ShowHeaderWhenEmpty="true" EmptyDataText="No data Found">
                                                                                                    <HeaderStyle CssClass="bg-light-blue" />
                                                                                                    <FooterStyle Font-Bold="true" ForeColor="White" />
                                                                                                    <RowStyle />
                                                                                                    <AlternatingRowStyle BackColor="White" />
                                                                                                    <HeaderStyle CssClass="bg-light-blue" />
                                                                                                    <Columns>
                                                                                                        <asp:BoundField DataField="ATT_DATE" ItemStyle-Width="30%" HeaderText="DATE" HeaderStyle-HorizontalAlign="Center"
                                                                                                            ItemStyle-HorizontalAlign="Center" />
                                                                                                        <asp:BoundField DataField="SLOT" ItemStyle-Width="40%" HeaderText="SLOT" HeaderStyle-HorizontalAlign="Center"
                                                                                                            ItemStyle-HorizontalAlign="Center" />
                                                                                                        <asp:BoundField DataField="TOPIC_COVERED" ItemStyle-Width="40%" HeaderText="TOPIC COVERED" HeaderStyle-HorizontalAlign="Center"
                                                                                                            ItemStyle-HorizontalAlign="Center" />

                                                                                                    </Columns>
                                                                                                </asp:GridView>
                                                                                            </div>
                                                                                        </td>
                                                                                    </tr>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>

                                            <%--<asp:TemplateField ItemStyle-Width="30%" ItemStyle-HorizontalAlign="Center" HeaderText="Mark Entry Pending Courses View Details"
                                        HeaderStyle-HorizontalAlign="Center">
                                        <itemtemplate>
                                                <div id="divcR" runat="server">
                                                     <asp:Label runat="server" id="lbl" Text='<%# Eval("COURSENO") %>'></asp:Label>
                                                       <asp:HiddenField ID="hdfTempExam" runat="server" Value='<%# Eval("COURSENO") %>' />
                                                </div>
                                              </itemtemplate>
                                    </asp:TemplateField>--%>
                                        </asp:GridView>

                                    </div>
                                </div>

                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <div class="col-12">
        <div class="row">
            <div class="modal fade" style="padding-top: 5%;" data-backdrop="static" data-keyboard="false" aria-modal="true" id="Model_Message" role="dialog">
                <div class="modal-dialog">

                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header text-center" style="background-color: #00C6D7 !important">
                            <h4 class="modal-title" style="font-style: normal; font-weight: bold; color: white">Send Mail</h4>
                        </div>
                        <br />
                        <br />
                        <div class="modal-body">
                            <div class="container-fluid">
                                <div class="row">
                                    <div class="col-md-12 col-sm-12 col-xs-12">
                                        <div class="row">
                                            <div class="col-sm-12 form-group">
                                                <div class="row">

                                                    <div class="col-sm-12 form-group">
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <label>To<sup>*</sup></label>
                                                            </div>
                                                            <div class="col-sm-12">
                                                                <asp:TextBox ID="txt_emailid" Enabled="true" placeholder="Email ID" runat="server" TabIndex="2" CssClass="form-control"></asp:TextBox>
                                                                <asp:HiddenField ID="hdfEmail" runat="server" />
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please Enter To Email "
                                                                    ControlToValidate="txt_emailid" Display="None" ValidationGroup="EmailSend"></asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txt_emailid"
                                                                    Display="None" ErrorMessage="Please Enter Valid To Email " ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                                    ValidationGroup="login1"></asp:RegularExpressionValidator>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-12 form-group">
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <label>Cc</label>
                                                            </div>
                                                            <div class="col-sm-12">
                                                                <asp:TextBox ID="txtCc" placeholder="Cc Email ID" runat="server" TabIndex="2" CssClass="form-control"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="revCc" runat="server" ControlToValidate="txtCc"
                                                                    Display="None" ErrorMessage="Please Enter Valid Cc Email " ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                                    ValidationGroup="login1"></asp:RegularExpressionValidator>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-12 form-group">
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <label>Subject<sup>*</sup></label>
                                                            </div>
                                                            <div class="col-sm-12">
                                                                <asp:TextBox ID="txtSubject" runat="server" TabIndex="3" MaxLength="128" placeholder="Enter Subject" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtSubject"
                                                                    ErrorMessage="Please Enter Subject" Display="None" ValidationGroup="EmailSend"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-12 form-group">
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <label>Message Body<sup>*</sup></label>
                                                            </div>
                                                            <div class="col-sm-12">
                                                                <asp:TextBox ID="txtBody" runat="server" TabIndex="4" MaxLength="8000" TextMode="MultiLine" placeholder="Enter Message" CssClass="form-control"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtBody"
                                                                    ErrorMessage="Please Enter Message  " Display="None" ValidationGroup="EmailSend"></asp:RequiredFieldValidator>

                                                            </div>
                                                        </div>
                                                    </div>


                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                </div>

                            </div>

                            <div class="modal-footer text-center">
                                <asp:Button ID="btnSent" runat="server" Text="Send" CssClass="btn btn-primary" UseSubmitBehavior="false"
                                    OnClientClick="if (!Page_ClientValidate('EmailSend')){ return false; } this.disabled = true; this.value ='  Please Wait...';"
                                    TabIndex="4" ValidationGroup="EmailSend" OnClick="btnSent_Click" />
                                <asp:Button ID="btnCancel" runat="server" Text="Close" data-dismiss="modal" OnClientClick="ClearMessageText();" CssClass="btn btn-danger" TabIndex="5"></asp:Button>
                                <asp:HiddenField ID="hdfReceiver_id" runat="server" />
                                <asp:ValidationSummary ID="vsMessage" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="EmailSend" DisplayMode="List" />
                            </div>

                        </div>
                    </div>

                </div>

            </div>
        </div>
    </div>
    <script language="javascript" type="text/javascript">
        function divexpandcollapse(divname) {
            var div = document.getElementById(divname);
            //var img = document.getElementById('img' + divname);
            if (div.style.display == "none") {
                div.style.display = "inline";
                //img.src = "../IMAGES/minus.png";
            }
            else {
                div.style.display = "none";
                //img.src = "../IMAGES/plus.gif";
            }
        }
        function View(txtvalue, txtCC) {
            $("#Model_Message").modal();
            document.getElementById('ctl00_ContentPlaceHolder1_txt_emailid').value = txtvalue;
            document.getElementById('ctl00_ContentPlaceHolder1_txtCc').value = txtCC;
            document.getElementById('ctl00_ContentPlaceHolder1_txtSubject').focus();
        }
        function validation() {
            var count = $('#ctl00_ContentPlaceHolder1_gvBulkEmail [type="checkbox"]:checked').length;
            console.log(count);
            if (count == 0) {
                alert('Please Select Atleast one faculty.');
                return false;
            }
            else {

                return CheckEmailValidation();
            }
        }

        function CheckEmailValidation() {
            var ret = false;
            $('#ctl00_ContentPlaceHolder1_gvBulkEmail [type="checkbox"]:checked').each(function () {

                var chkids = '' + this.id + '';
                var myarray = Array();
                myarray = chkids.split('_');
                var txtSubject = document.getElementById('ctl00_ContentPlaceHolder1_gvBulkEmail_' + myarray[3] + '_txtSubjectBulk').value;
                var txtEmail = document.getElementById('ctl00_ContentPlaceHolder1_gvBulkEmail_' + myarray[3] + '_txtEmailMsg').value;
                var hdnUaname = document.getElementById('ctl00_ContentPlaceHolder1_gvBulkEmail_' + myarray[3] + '_hdnUaname').value;
                if (txtSubject == "" || txtEmail == "") {
                    alert("Please enter Email Subject/Message details for faculkty " + hdnUaname);
                    ret = false;
                    return;
                }
                else {
                    ret = true;
                }
            });
            if (!ret) {
                return false;
            }
            else { return true; }
        }

        //Display selected Row data in Alert Box.

    </script>
</asp:Content>
