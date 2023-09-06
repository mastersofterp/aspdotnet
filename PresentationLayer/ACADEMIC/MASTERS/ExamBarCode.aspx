<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ExamBarCode.aspx.cs" Inherits="ACADEMIC_MASTERS_ExamBarCode" MasterPageFile="~/SiteMasterPage.master" %>

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
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">Seating Arrangement Report</h3>
                </div>
                <div class="box-body">
                    <asp:UpdatePanel ID="updBarcode" runat="server">
                        <ContentTemplate>

                            <div class="col-12">
                                <div class="row">
                                    <div class="col-lg-7 col-md-12 col-12">
                                        <div class="row">
                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>School/College name</label>
                                                </div>
                                                <asp:DropDownList ID="ddlCollege" runat="server" AppendDataBoundItems="true" TabIndex="2" data-select2-enable="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvddlCollege" runat="server" ControlToValidate="ddlCollege"
                                                    Display="None" ErrorMessage="Please Select College/School" InitialValue="0" SetFocusOnError="True"
                                                    ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator ID="rfvcollege" runat="server" ControlToValidate="ddlCollege"
                                                    Display="None" ErrorMessage="Please Select College/School" InitialValue="0" SetFocusOnError="True"
                                                    ValidationGroup="Seating"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Exam Date</label>
                                                </div>
                                                <div class="input-group">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <asp:TextBox ID="txtExamDate" runat="server" TabIndex="3" ValidationGroup="submit" OnTextChanged="txtExamDate_TextChanged" AutoPostBack="true" />
                                                    <%-- <asp:Image ID="imgExamDate" runat="server" ImageUrl="~/images/calendar.png" />--%>
                                                    <ajaxToolKit:CalendarExtender ID="ceExamDate" runat="server" Format="dd/MM/yyyy"
                                                        TargetControlID="txtExamDate" PopupButtonID="imgExamDate" />
                                                    <ajaxToolKit:MaskedEditExtender ID="meExamDate" runat="server" TargetControlID="txtExamDate"
                                                        Mask="99/99/9999" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                                        MaskType="Date" />
                                                    <ajaxToolKit:MaskedEditValidator ID="mvExamDate" runat="server" EmptyValueMessage="Please Enter Exam Date"
                                                        ControlExtender="meExamDate" ControlToValidate="txtExamDate" IsValidEmpty="false"
                                                        InvalidValueMessage="Exam Date is invalid" Display="None" ErrorMessage="Please Enter Exam Date"
                                                        InvalidValueBlurredMessage="*" ValidationGroup="Submit" SetFocusOnError="true" />
                                                    <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" EmptyValueMessage="Please Enter Exam Date"
                                                        ControlExtender="meExamDate" ControlToValidate="txtExamDate" IsValidEmpty="false"
                                                        InvalidValueMessage="Exam Date is invalid" Display="None" ErrorMessage="Please Enter Exam Date"
                                                        InvalidValueBlurredMessage="*" ValidationGroup="Seating" SetFocusOnError="true" />
                                                    <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" EmptyValueMessage="Please Enter Exam Date"
                                                        ControlExtender="meExamDate" ControlToValidate="txtExamDate" IsValidEmpty="false"
                                                        InvalidValueMessage="Exam Date is invalid" Display="None" ErrorMessage="Please Enter Exam Date"
                                                        InvalidValueBlurredMessage="*" ValidationGroup="DisplayReport" SetFocusOnError="true" />
                                                </div>
                                            </div>
                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Exam  Slot</label>
                                                </div>
                                                <asp:DropDownList ID="ddlslot" runat="server" TabIndex="1" AppendDataBoundItems="True"
                                                    OnSelectedIndexChanged="ddlslot_SelectedIndexChanged" AutoPostBack="true" data-select2-enable="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvSlot1" runat="server" ControlToValidate="ddlslot"
                                                    Display="None" ErrorMessage="Please Select Exam Slot" InitialValue="0" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator ID="rfvSlot2" runat="server" ControlToValidate="ddlslot"
                                                    Display="None" ErrorMessage="Please Select Exam Slot" InitialValue="0" ValidationGroup="Seating"></asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator ID="rfSlot3" runat="server" ControlToValidate="ddlslot"
                                                    Display="None" ErrorMessage="Please Select Exam Slot" InitialValue="0" ValidationGroup="DisplayReport"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Exam Name</label>
                                                </div>
                                                <asp:DropDownList ID="ddlExamName" runat="server" AppendDataBoundItems="true"
                                                    TabIndex="7" data-select2-enable="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    <asp:ListItem Value="1">Mid Semester</asp:ListItem>
                                                    <asp:ListItem Value="2">End Semester</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvExam" runat="server" ControlToValidate="ddlExamName"
                                                    ValidationGroup="Seating" Display="None" ErrorMessage="Please Select Exam Name"
                                                    SetFocusOnError="true" InitialValue="0" />
                                                <asp:RequiredFieldValidator ID="rfvExa" runat="server" ControlToValidate="ddlExamName"
                                                    ValidationGroup="Submit" Display="None" ErrorMessage="Please Select Exam Name"
                                                    SetFocusOnError="true" InitialValue="0" />
                                                <asp:RequiredFieldValidator ID="rfvExa1" runat="server" ControlToValidate="ddlExamName"
                                                    ValidationGroup="DisplayReport" Display="None" ErrorMessage="Please Select Exam Name"
                                                    SetFocusOnError="true" InitialValue="0" />
                                            </div>
                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Room</label>
                                                </div>
                                                <asp:DropDownList ID="ddlRoom" runat="server" TabIndex="1" AppendDataBoundItems="True" data-select2-enable="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>

                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlRoom"
                                                    Display="None" ErrorMessage="Please Select Room" InitialValue="0" ValidationGroup="Seating"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-lg-4 col-md-6 col-12 d-none">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Room</label>
                                                </div>
                                                <asp:DropDownList ID="DropDownList1" runat="server" TabIndex="1" AppendDataBoundItems="True" data-select2-enable="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-12 btn-footer mt-4">
                                                <asp:Button ID="btnExamTimeTable" runat="server" Text="Exam Bar Code Report" ValidationGroup="Submit" Enabled="false"
                                                    CssClass="btn btn-info" OnClick="btnExamTimeTable_Click" Visible="false" />
                                                <asp:Button ID="txtmasterseatplan" runat="server" Text="Door Seating Plan" ValidationGroup="Seating"
                                                    CssClass="btn btn-info" OnClick="txtmasterseatplan_Click" />
                                                <asp:Button ID="txtblockarrrangement" runat="server" Text="Block Arrangement" ValidationGroup="Submit"
                                                    CssClass="btn btn-info" OnClick="txtblockarrrangement_Click" />
                                                <asp:Button ID="txtstudeattendence" runat="server" Text="Student Attendence" ValidationGroup="Submit"
                                                    CssClass="btn btn-info" OnClick="txtstudeattendence_Click" Visible="true" />
                                                <asp:Button ID="btnQPReportView" runat="server" Text="Question Paper Count Report Format 1" ValidationGroup="Submit"
                                                    CssClass="btn btn-info" OnClick="btnQPReportView_Click" />

                                                <asp:Button ID="btnQPF_two" runat="server" Text="Question Paper Count Report Format 2" ValidationGroup="Seating"
                                                    CssClass="btn btn-info" OnClick="btnQPF_two_Click" />
                                                <asp:Button ID="btnenvelop" runat="server" Text="Envelop Sticker Manuscript" CssClass="btn btn-info"
                                                    OnClick="btnenvelop_Click" ValidationGroup="Submit" Visible="false" />
                                                <asp:Button ID="btnSeatPlan" runat="server" Text="Seating Plan - Format 1" CssClass="btn btn-info"
                                                    OnClick="btnSeatPlan_Click" ValidationGroup="Submit" Visible="false" />
                                                <asp:Button ID="btnDisplaySitting" runat="server" Text="Display Sitting" ValidationGroup="DisplayReport"
                                                    CssClass="btn btn-info" OnClick="btnDisplaySitting_Click" />
                                                <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                    ShowSummary="false" ValidationGroup="Submit" />
                                                <asp:ValidationSummary ID="vsSeatingPlan" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                    ShowSummary="false" ValidationGroup="Seating" />
                                                <asp:ValidationSummary ID="vsDisplayReport" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                    ShowSummary="false" ValidationGroup="DisplayReport" />
                                            </div>
                                        </div>
                                    </div>


                                    <div class="form-group col-lg-5 col-md-12 col-12">
                                        <div class=" note-div">
                                            <h5 class="heading">Note (Please Select)</h5>
                                            <p>
                                                <i class="fa fa-star" aria-hidden="true"></i><span>Door Seating Plan<br />
                                                <span style="color: green; font-weight: bold;margin-left: 30px;">College name->Exam Date->Exam Slot->Exam Name->Room</span></span>
                                            </p>

                                            <p>
                                                <i class="fa fa-star" aria-hidden="true"></i><span>Block Arrangement<br />
                                                <span style="color: green; font-weight: bold;margin-left: 30px;">College name->Exam Date->Exam Slot->Exam Name</span></span>
                                            </p>

                                            <p>
                                                <i class="fa fa-star" aria-hidden="true"></i><span>Student Attendance <br />
                                                <span style="color: green; font-weight: bold;margin-left: 30px;">College name->Exam Date->Exam Slot->Exam Name</span></span>
                                            </p>

                                            <p>
                                                <i class="fa fa-star" aria-hidden="true"></i><span>Exam Registered <br />
                                                <span style="color: green; font-weight: bold;margin-left: 30px;">Session->College->Degree->Regulation->Semester->Subject</span></span>
                                            </p>

                                            <p>
                                                <i class="fa fa-star" aria-hidden="true"></i><span>Question Paper Count Format 1<br />
                                                <span style="color: green; font-weight: bold;margin-left: 30px;">College name->Exam Date->Exam Slot->Exam Name</span></span>
                                            </p>

                                            <p>
                                                <i class="fa fa-star" aria-hidden="true"></i><span>Question Paper Count Format 2<br />
                                                <span style="color: green; font-weight: bold;margin-left: 30px;">College name->Exam Date->Exam Slot->Exam Name->Room</span></span>
                                            </p>

                                            <p>
                                                <i class="fa fa-star" aria-hidden="true"></i><span>Display Sitting <br />
                                                <span style="color: green; font-weight: bold;margin-left: 30px;">Exam Date->Exam Slot->Exam Name</span></span>
                                            </p>

                                        </div>
                                    </div>

                                </div>
                            </div>

                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
    <div id="divMsg" runat="server">
    </div>
</asp:Content>
