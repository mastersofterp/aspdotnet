<%@ Page Language="C#" AutoEventWireup="true" CodeFile="examseatingreports_RCPIT.aspx.cs"
    MasterPageFile="~/SiteMasterPage.master" Inherits="examseatingreports" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updBarcode"
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

    <asp:UpdatePanel ID="updBarcode" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">

                            <h3 class="box-title">
                                <%--<asp:Label ID="lblDYtxtSeatingReport" runat="server"></asp:Label>--%>
                                <asp:Label ID="lblRoom" runat="server">SEATING ARRANGEMENT REPORT</asp:Label>
                            </h3>

                            <div class="box-tools pull-right">
                                <div style="color: Red; font-weight: bold">
                                    &nbsp;&nbsp;&nbsp;Note : * Marked fields are mandatory
                                </div>
                            </div>

                        </div>
                        <br />
                        <br />

                        <div class="box-body" style="margin-top: -20px;">
                            <%--<div class="box-tools pull-right">--%>
                            <%-- <div style="color: Red; font-weight: bold; margin-top:20px;">
                             &nbsp;&nbsp;&nbsp;Exam Slot is optional for Building Chart Report</div><br />
                                 </div>--%>


                            <div class="form-group col-md-12 mr-top-auto">
                                <%--  <div class="form-group col-md-4">   style="margin-top:-40px;"--%>
                                <div class="row">

                                    <div class="form-group col-lg-3 col-md-6 col-12">

                                        <%--  <div class="col-md-3">--%>
                                        <%--    <label><span style="color: red;">*</span> College </label>--%>
                                        <sup>* </sup>
                                        <asp:Label ID="lblDYtxtSchoolname" runat="server" Font-Bold="true"></asp:Label>

                                        <asp:DropDownList ID="ddlCollege" runat="server" TabIndex="1" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged" AutoPostBack="true" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <%-- <asp:RequiredFieldValidator ID="rfvCollege" runat="server" ControlToValidate="ddlCollege"
                                            Display="None" ErrorMessage="Please Select School/Institute Name" InitialValue="0" ValidationGroup="configure"></asp:RequiredFieldValidator>--%>

                                        <asp:RequiredFieldValidator ID="rfvColleges" runat="server" ControlToValidate="ddlslot"
                                            Display="None" ErrorMessage="Please Select School/Institute Name" InitialValue="0" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <%--                                    <div class="col-md-3">--%>
                                        <%--  <label><span style="color: red;">*</span> Session  </label>--%>
                                        <sup>* </sup>
                                        <asp:Label ID="lblDYtxtSession" runat="server" Font-Bold="true"></asp:Label>

                                        <asp:DropDownList ID="ddlSession" runat="server" TabIndex="2" AppendDataBoundItems="True" data-select2-enable="true" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="Submit"></asp:RequiredFieldValidator>

                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">

                                        <%-- <label><span style="color: red;">*</span>Exam Date</label>--%>
                                        <sup>* </sup>
                                        <asp:Label ID="lblDYtxtExamDate" runat="server" Font-Bold="true"></asp:Label>

                                        <%--   <asp:DropDownList ID="ddlExamdate" runat="server" TabIndex="1" AppendDataBoundItems="True" data-select2-enable="true"  OnSelectedIndexChanged="ddlExamdate_SelectedIndexChanged" AutoPostBack="true" >
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>--%>

                                        <asp:DropDownList ID="ddlExamdate" runat="server" TabIndex="3" AppendDataBoundItems="True" AutoPostBack="true" data-select2-enable="true" OnSelectedIndexChanged="ddlExamdate_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>

                                        <%--<asp:RequiredFieldValidator ID="rfvexamdate" runat="server" ControlToValidate="ddlExamdate"
                                       Display="None" ErrorMessage="Please Select Exam Date"  ValidationGroup="Submit"></asp:RequiredFieldValidator>--%>

                                        <asp:RequiredFieldValidator ID="rfvexamdate" runat="server" ControlToValidate="ddlExamdate" TabIndex="4"
                                            Display="None" ErrorMessage="Please Select Exam Date" InitialValue="0" ValidationGroup="Submit"></asp:RequiredFieldValidator>


                                        <%--  <div class="input-group">
                                        <div class="input-group-addon">
                                            <i class="fa fa-calendar text-blue"></i>
                                        </div>
                                        <asp:TextBox ID="txtExamDate" runat="server" TabIndex="3" ValidationGroup="submit" OnTextChanged="txtExamDate_TextChanged" AutoPostBack="true"  />

                                        <%-- <asp:Image ID="imgExamDate" runat="server" ImageUrl="~/images/calendar.png" />--%>
                                        <%-- <ajaxToolKit:CalendarExtender ID="ceExamDate" runat="server" Format="dd/MM/yyyy"
                                            TargetControlID="txtExamDate" PopupButtonID="imgExamDate" />
                                        <ajaxToolKit:MaskedEditExtender ID="meExamDate" runat="server" TargetControlID="txtExamDate"
                                            Mask="99/99/9999" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                            MaskType="Date" />
                                        <ajaxToolKit:MaskedEditValidator ID="mvExamDate" runat="server" EmptyValueMessage="Please Enter Exam Date"
                                            ControlExtender="meExamDate" ControlToValidate="txtExamDate" IsValidEmpty="false"
                                            InvalidValueMessage="Exam Date is invalid" Display="None" ErrorMessage="Please Enter Exam Date"
                                            InvalidValueBlurredMessage="*" ValidationGroup="Submit" SetFocusOnError="true" />
                                         <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" EmptyValueMessage="Please Enter Exam Date"
                                            ControlExtender="meExamDate" ControlToValidate="txtExamDate" IsValidEmpty="false"
                                            InvalidValueMessage="Exam Date is invalid" Display="None" ErrorMessage="Please Enter Exam Date"
                                            InvalidValueBlurredMessage="*" ValidationGroup="chart" SetFocusOnError="true" />
                                    </div>--%>
                                    </div>


                                    <%--  <div class="form-group col-md-4">--%>
                                    <div class="form-group col-lg-3 col-md-6 col-12">

                                        <%-- <label><span style="color: red;">*</span>Exam  Slot :</label>--%>
                                        <sup>* </sup>
                                        <asp:Label ID="lblDYlvExamSlot" runat="server" Font-Bold="true"></asp:Label>
                                        <asp:DropDownList ID="ddlslot" runat="server" TabIndex="5" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlslot"
                                            Display="None" ErrorMessage="Please Select Exam Slot" InitialValue="0" ValidationGroup="Submit"></asp:RequiredFieldValidator>

                                        <%--   <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlslot"
                                    Display="None" ErrorMessage="Please Select Exam Slot" InitialValue="0" ValidationGroup="chart"></asp:RequiredFieldValidator>--%>
                                    </div>


                                </div>
                            </div>
                            <%-- <div class="form-group col-md-4">--%>
                            <div class="form-group col-lg-3 col-md-6 col-12">

                                <%--                                        <label><span style="color: red;">*</span>Regular/Repeater</label>--%>
                                <asp:DropDownList ID="ddlExamType" runat="server" AppendDataBoundItems="true"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlExamType_SelectedIndexChanged" Visible="false">
                                    <asp:ListItem Value="-1">Please Select</asp:ListItem>
                                    <asp:ListItem Value="0">Regular</asp:ListItem>
                                    <asp:ListItem Value="1">Repeater</asp:ListItem>
                                    <asp:ListItem Value="2">Regular/Repeater</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvexamtype" runat="server" ControlToValidate="ddlExamType"
                                    Display="None" ErrorMessage="Please Select Regular/Repeater" InitialValue="-1" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlExamType"
                                    Display="None" ErrorMessage="Please Select Regular/Repeater" InitialValue="-1" ValidationGroup="chart"></asp:RequiredFieldValidator>

                            </div>

                            <%-- <div class="form-group col-md-4"></div>--%>

                            <br />
                            <br />
                            <br />



                            <div class="box-footer" style="margin-top: -40px;">
                                <p class="text-center">

                                    <%-- <asp:Button ID="btnExamTimeTable" runat="server" Text="Exam Bar Code Report" ValidationGroup="Submit"
                                    CssClass="btn btn-primary" OnClick="btnExamTimeTable_Click" />--%>


                                    <asp:Button ID="txtmasterseatplan" runat="server" Text="Door Seating Plan" ValidationGroup="Submit" TabIndex="6"
                                        CssClass="btn btn-primary" OnClick="txtmasterseatplan_Click" />
                                    <asp:Button ID="txtblockarrrangement" runat="server" Text="Block Arrangement" ValidationGroup="Submit" TabIndex="7"
                                        CssClass="btn btn-primary" OnClick="txtblockarrrangement_Click" />
                                    <asp:Button ID="btnblockarrangementExcel" runat="server" Text="Block Arrangement Excel" ValidationGroup="Submit" TabIndex="8"
                                        CssClass="btn btn-primary" OnClick="btnblockarrangementExcel_Click" />
                                    <asp:Button ID="txtstudeattendence" runat="server" Text="Student Attendence" ValidationGroup="Submit" TabIndex="9"
                                        CssClass="btn btn-primary" OnClick="txtstudeattendence_Click" />


                                    <asp:Button   ID="btnbuildingchart"  runat="server" OnClick="btnbuildingchart_Click" Text="Building Chart" ToolTip="Click Here To Generate Buliding Chart" TabIndex="10"
                                        CssClass="btn btn-primary" ValidationGroup="Submit"  hidden/>
                                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List" ShowMessageBox="true"
                                        ShowSummary="false" ValidationGroup="Submit" />

                                    <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                                        ShowSummary="false" ValidationGroup="Submit" />




                                    <%--   <asp:Button ID="btnQPReportView" runat="server" Text="Question Paper Report" ValidationGroup="Submit"
                                    CssClass="btn btn-primary" OnClick="btnQPReportView_Click" />--%>

                                    <%--<asp:Button ID="btnQPReportExcel" runat="server" Text="Question Paper Report Excel" ValidationGroup="Submit"
                                    CssClass="btn btn-primary" OnClick="btnQPReportExcel_Click" />--%>


                                    <%--  <asp:Button ID="btnenvelop" runat="server" Text="Envelop Sticker Manuscript" CssClass="btn btn-primary"
                                     OnClick="btnenvelop_Click" ValidationGroup="Submit"/>
                                 <asp:Button ID="btnConsolidatedAbsStud" runat="server" Text="Absent Student List" CssClass="btn btn-primary"
                                     ValidationGroup="Submit" OnClick="btnConsolidatedAbsStud_Click" />
                                   <asp:Button ID="btnreport" runat="server" Text="Consolidated Absent Student List" CssClass="btn btn-primary"
                                      ValidationGroup="Submit" OnClick="btnreport_Click"/>
                                <asp:Button ID="Button1" runat="server" Text="Supervisor Description Report" CssClass="btn btn-primary"
                                     ValidationGroup="Submit" OnClick="Button1_Click"/><br /><br />
                                  <asp:Button ID="btnprctlreprt" runat="server" Text="Practical Attendance Report" CssClass="btn btn-primary"
                                     ValidationGroup="Submit" OnClick="btnprctlreprt_Click"/>
                      
                                  <asp:Button ID="btnblankmarksheet" runat="server" Text="Practical Blank Mark Sheet" CssClass="btn btn-primary"
                                      ValidationGroup="Submit" OnClick="btnblankmarksheet_Click"/>--%>
                                </p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <%--   <asp:PostBackTrigger ControlID="btnQPReportExcel" />--%>
            <asp:PostBackTrigger ControlID="btnblockarrangementExcel" />
            <asp:PostBackTrigger ControlID="btnbuildingchart" />

        </Triggers>
    </asp:UpdatePanel>
    <div id="divMsg" runat="server">
    </div>
</asp:Content>
