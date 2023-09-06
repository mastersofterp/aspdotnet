<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="SeatingArrangementReportFixed.aspx.cs" Inherits="ACADEMIC_MASTERS_SeatingArrangementReportFixed" %>

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
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">Fixed Seating Arrangement Report</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="col-lg-8 col-md-12 col-sm-12">
                                        <div class="row">
                                            <div class="col-lg-4 col-md-6 col-12 form-group">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Session </label>
                                                </div>
                                                <asp:DropDownList ID="ddlSession" runat="server" TabIndex="1" AppendDataBoundItems="True" data-select2-enable="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                                    Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="Report"></asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator ID="rfv1Session" runat="server" ControlToValidate="ddlSession"
                                                    Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlSession"
                                                    Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="Door"></asp:RequiredFieldValidator>
                                            </div>


                                            <div class="col-lg-4 col-md-6 col-12 form-group">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Exam Date</label>
                                                </div>
                                                <div class="input-group">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <asp:TextBox ID="txtExamDate" runat="server" TabIndex="3" OnTextChanged="txtExamDate_TextChanged" AutoPostBack="true" />
                                                    <%-- <asp:Image ID="imgExamDate" runat="server" ImageUrl="~/images/calendar.png" />--%>
                                                    <ajaxToolKit:CalendarExtender ID="ceExamDate" runat="server" Format="dd/MM/yyyy"
                                                        TargetControlID="txtExamDate" PopupButtonID="imgExamDate" />
                                                    <ajaxToolKit:MaskedEditExtender ID="meExamDate" runat="server" TargetControlID="txtExamDate"
                                                        Mask="99/99/9999" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                                        MaskType="Date" />
                                                    <%-- <ajaxToolKit:MaskedEditValidator ID="mvExamDate" runat="server" EmptyValueMessage="Please Enter Exam Date"
                                        ControlExtender="meExamDate" ControlToValidate="txtExamDate" IsValidEmpty="false"
                                        InvalidValueMessage="Exam Date is invalid" Display="None" ErrorMessage="Please Enter Exam Date"
                                        InvalidValueBlurredMessage="*" ValidationGroup="Submit" SetFocusOnError="true" />--%>
                                                </div>
                                            </div>


                                            <div class="col-lg-4 col-md-6 col-12 form-group">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Exam  Slot </label>
                                                </div>
                                                <asp:DropDownList ID="ddlslot" runat="server" TabIndex="1" AppendDataBoundItems="True" data-select2-enable="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlslot"
                                                    Display="None" ErrorMessage="Please Select Exam Slot" InitialValue="0" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlslot"
                                                    Display="None" ErrorMessage="Please Select Exam  Slot" InitialValue="0" ValidationGroup="Door"></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="col-lg-4 col-md-6 col-12 form-group">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Exam  Name</label>
                                                </div>
                                                <asp:DropDownList ID="ddlExamName" runat="server" TabIndex="1" AppendDataBoundItems="True" AutoPostBack="true" data-select2-enable="true" OnSelectedIndexChanged="ddlExamName_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    <asp:ListItem Value="1">MID SEM</asp:ListItem>
                                                    <asp:ListItem Value="2">END SEM</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvExamName" runat="server" ControlToValidate="ddlExamName"
                                                    Display="None" ErrorMessage="Please Select Exam Name" InitialValue="0" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlExamName"
                                                    Display="None" ErrorMessage="Please Select Exam  Name" InitialValue="0" ValidationGroup="Door"></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="col-lg-4 col-md-6 col-12 form-group">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Room</label>
                                                </div>
                                                <asp:DropDownList ID="ddlRoom" runat="server" TabIndex="1" data-select2-enable="true" AppendDataBoundItems="True">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvRoom" runat="server" ControlToValidate="ddlRoom"
                                                    Display="None" ErrorMessage="Please Select Room" InitialValue="0" ValidationGroup="Door"></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="col-lg-4 col-md-6 col-12 form-group d-none">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>School / College Name</label>
                                                </div>
                                                <asp:DropDownList ID="ddlCollege" runat="server" AppendDataBoundItems="true" data-select2-enable="true"
                                                    TabIndex="2">
                                                </asp:DropDownList>
                                                <%--  <asp:RequiredFieldValidator ID="rfvddlCollege" runat="server" ControlToValidate="ddlCollege"
                                    Display="None" ErrorMessage="Please Select Institute" InitialValue="0" SetFocusOnError="True"
                                    ValidationGroup="Submit"></asp:RequiredFieldValidator>--%>
                                                <%--      <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCollege"
                                    Display="None" ErrorMessage="Please Select College/School" InitialValue="0" SetFocusOnError="True"
                                    ValidationGroup="Report"></asp:RequiredFieldValidator>--%>
                                            </div>


                                            <div class="col-12 btn-footer mt-4">

                                                <asp:Button ID="btnExamTimeTable" runat="server" Text="Exam Bar Code Report" ValidationGroup="Submit" Visible="false"
                                                    CssClass="btn btn-info" OnClick="btnExamTimeTable_Click" />

                                                <asp:Button ID="txtmasterseatplan" runat="server" Text="Door Seating Plan" ValidationGroup="Door"
                                                    CssClass="btn btn-info" OnClick="txtmasterseatplan_Click" />

                                                <asp:Button ID="txtblockarrrangement" runat="server" Text="Block Arrangement" ValidationGroup="Submit"
                                                    CssClass="btn btn-info" OnClick="txtblockarrrangement_Click" />

                                                <asp:Button ID="btnblockarrangementExcel" runat="server" Text="Block Arrangement Excel" ValidationGroup="Submit"
                                                    CssClass="btn btn-info" OnClick="btnblockarrangementExcel_Click" Visible="false" />

                                                <asp:Button ID="txtstudeattendence" runat="server" Text="Student Attendance" ValidationGroup="Submit"
                                                    CssClass="btn btn-info" OnClick="txtstudeattendence_Click" />

                                                <asp:Button ID="btbStudAttFormat2" runat="server" Visible="false" Text="Student Attendence Format(2)" OnClick="btnStudAttFormat2_Click" ValidationGroup="Submit"
                                                    CssClass="btn btn-info" />


                                                <asp:Button ID="btnDualSeatingPlan" runat="server" Text="Student Attendence Format(3)" Visible="false" CssClass="btn btn-info"
                                                    ValidationGroup="Submit" OnClick="btnDualSeatingPlan_Click" /><%--<br />--%>
                                                <%-- <br />--%>

                                                <asp:Button ID="btnQPReportView" runat="server" Text="Question Paper Count Format 1" ValidationGroup="Submit"
                                                    CssClass="btn btn-info" OnClick="btnQPReportView_Click" />


                                                <asp:Button ID="btnQPF2" runat="server" Text="Question Paper Count Format 2" ValidationGroup="Submit"
                                                    CssClass="btn btn-info" OnClick="btnQPF2_Click" />

                                                <asp:Button ID="btnDisplaySitting" runat="server" Text="Display Sitting" ValidationGroup="Submit"
                                                    CssClass="btn btn-info" OnClick="btnDisplaySitting_Click" />

                                                <asp:Button ID="btnQPReportExcel" runat="server" Text="Question Paper Report Excel" ValidationGroup="Submit"
                                                    CssClass="btn btn-info" OnClick="btnQPReportExcel_Click" Visible="false" />

                                                <asp:Button ID="btnenvelop" runat="server" Text="Envelop Sticker Manuscript" CssClass="btn btn-info" Visible="false"
                                                    OnClick="btnenvelop_Click" ValidationGroup="Submit" />

                                                <asp:Button ID="btnConsolidatedAbsStud" runat="server" Text="Absent Student List" CssClass="btn btn-info" Visible="false"
                                                    ValidationGroup="Submit" OnClick="btnConsolidatedAbsStud_Click" />

                                                <asp:Button ID="btnreport" runat="server" Text="Consolidated Absent Student List" CssClass="btn btn-info" Visible="false"
                                                    ValidationGroup="Submit" OnClick="btnreport_Click" />

                                                <asp:Button ID="Button1" runat="server" Text="Supervisor Description Report" CssClass="btn btn-info" Visible="false"
                                                    ValidationGroup="Submit" OnClick="Button1_Click" /><%--<br />--%>
                                                <%--<br />--%>


                                                <asp:Button ID="btnprctlreprt" runat="server" Text="Practical Attendance Report" CssClass="btn btn-priminfoary" Visible="false"
                                                    ValidationGroup="Submit" OnClick="btnprctlreprt_Click" />

                                                <asp:Button ID="btnblankmarksheet" runat="server" Text="Practical Blank Mark Sheet" CssClass="btn btn-info" Visible="false"
                                                    ValidationGroup="Submit" OnClick="btnblankmarksheet_Click" />


                                                <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                    ShowSummary="false" ValidationGroup="Submit" />

                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-4 col-md-12 col-12">
                                        <div class=" note-div">
                                            <h5 class="heading">Note (Please Select)</h5>
                                            <p>
                                                <i class="fa fa-star" aria-hidden="true"></i><span>Door Seating Plan<br />
                                                    <span style="color: green; font-weight: bold; margin-left: 30px;">Session->Exam Slot->Exam Name->Room</span></span>
                                            </p>

                                            <p>
                                                <i class="fa fa-star" aria-hidden="true"></i><span>Block Arrangement
                                                    <br />
                                                    <span style="color: green; font-weight: bold; margin-left: 30px;">Session->Exam Slot->Exam Name</span></span>
                                            </p>

                                            <p>
                                                <i class="fa fa-star" aria-hidden="true"></i><span>Student Attendance 
                                                    <br />
                                                    <span style="color: green; font-weight: bold; margin-left: 30px;">Session->Exam Slot->Exam Name</span></span>
                                            </p>

                                            <p>
                                                <i class="fa fa-star" aria-hidden="true"></i><span>Question Paper Count Format 1 
                                                    <br />
                                                    <span style="color: green; font-weight: bold; margin-left: 30px;">Session->Exam Slot->Exam Name</span></span>
                                            </p>

                                            <p>
                                                <i class="fa fa-star" aria-hidden="true"></i><span>Question Paper Count Format 2<br />
                                                    <span style="color: green; font-weight: bold; margin-left: 30px;">Session->Exam Slot->Exam Name</span></span>
                                            </p>

                                            <p>
                                                <i class="fa fa-star" aria-hidden="true"></i><span>Display Sitting<br />
                                                    <span style="color: green; font-weight: bold; margin-left: 30px;">Session->Exam Slot->Exam Name</span></span>
                                            </p>


                                        </div>
                                    </div>

                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>

            </div>
                
                    <asp:ValidationSummary ID="ValidationSummary" runat="server" ValidationGroup="Door" ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />

        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnQPReportExcel" />
            <asp:PostBackTrigger ControlID="btnblockarrangementExcel" />

        </Triggers>
    </asp:UpdatePanel>
    <div id="divMsg" runat="server">
    </div>
</asp:Content>

