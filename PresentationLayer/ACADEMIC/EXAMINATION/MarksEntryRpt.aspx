<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="MarksEntryRpt.aspx.cs" Inherits="ACADEMIC_EXAMINATION_MarksEntryRpt" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="updpnl" runat="server">
        <ContentTemplate>
            <div>
                <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updpnl"
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
                        <div class="box-header with-border">
                            <h3 class="box-title"><b>MARK ENTRY REPORTS</b></h3>
                        </div>
                        <div class="box-body">
                            <asp:Panel ID="pnlMarkEntry" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-lg-3 col-md-6 col-12 form-group">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <asp:Label ID="lblDYddlColgScheme" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlcollege" TabIndex="2" runat="server" CssClass="form-control" AppendDataBoundItems="true" ToolTip="Please Select Institute" data-select2-enable="true"
                                                AutoPostBack="True" OnSelectedIndexChanged="ddlcollege_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvcollege" runat="server" ControlToValidate="ddlcollege"
                                                Display="None" ErrorMessage="Please Select College & Scheme." InitialValue="0" SetFocusOnError="True"
                                                ValidationGroup="show"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-lg-3 col-md-6 col-12 form-group">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <asp:Label ID="lblDYddlSession" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlSession" TabIndex="1" runat="server" CssClass="form-control" AppendDataBoundItems="true"
                                                AutoPostBack="True" data-select2-enable="true" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                                Display="None" ErrorMessage="Please Select Session." InitialValue="0" SetFocusOnError="True"
                                                ValidationGroup="show"></asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSession"
                                                Display="None" ErrorMessage="Please Select Session." InitialValue="0" SetFocusOnError="True"
                                                ValidationGroup="Excel"></asp:RequiredFieldValidator>

                                        </div>
                                        <div class="col-lg-3 col-md-6 col-12 form-group">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlsemester" TabIndex="6" runat="server" CssClass="form-control" AppendDataBoundItems="true"
                                                AutoPostBack="True" data-select2-enable="true" OnSelectedIndexChanged="ddlsemester_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvsemester" runat="server" ControlToValidate="ddlsemester"
                                                Display="None" ErrorMessage="Please Select Semester." InitialValue="0" SetFocusOnError="True"
                                                ValidationGroup="show"></asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlsemester"
                                                Display="None" ErrorMessage="Please Select Semester." InitialValue="0" SetFocusOnError="True"
                                                ValidationGroup="Excel"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-lg-3 col-md-6 col-12 form-group">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <asp:Label ID="lblDYddlSubtype" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlSubjectType" runat="server" AppendDataBoundItems="True"
                                                AutoPostBack="True" data-select2-enable="true"
                                                TabIndex="7" CssClass="form-control" OnSelectedIndexChanged="ddlSubjectType_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvSubjectType" runat="server" ControlToValidate="ddlSubjectType"
                                                Display="None" ErrorMessage="Please Select Subject Type." InitialValue="0" SetFocusOnError="true"
                                                ValidationGroup="show">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-lg-3 col-md-6 col-12 form-group">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <asp:Label ID="lblDYddlCourse" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlCourse" runat="server" AppendDataBoundItems="True" data-select2-enable="true"
                                                CssClass="form-control" AutoPostBack="True" TabIndex="8">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvCourse" runat="server" ControlToValidate="ddlCourse"
                                                Display="None" ErrorMessage="Please Select Course Name." InitialValue="0" SetFocusOnError="True"
                                                ValidationGroup="show"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-lg-3 col-md-6 col-12 form-group d-none">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <asp:Label ID="lblDYddlExam" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlExam" runat="server" AppendDataBoundItems="True" data-select2-enable="true"
                                                CssClass="form-control" TabIndex="9" AutoPostBack="True">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <%--<asp:RequiredFieldValidator ID="rfvExam" runat="server" ControlToValidate="ddlExam"
                                                Display="None" ErrorMessage="Please Select Exam Name." InitialValue="0" SetFocusOnError="True"
                                                ValidationGroup="show"></asp:RequiredFieldValidator>--%>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12 btn-footer">
                                    <div class="col-12 btn-footer mt-4">
                                        <asp:Button ID="btnInMrkPDF" TabIndex="1" runat="server" ValidationGroup="show"
                                            Text=" Internal Mark In PDF(Course Wise)" CssClass="btn btn-info" OnClick="btnInMrkPDF_Click" Visible="true" />

                                        <asp:Button ID="btnWeightarpt" TabIndex="1" runat="server" ValidationGroup="show"
                                            Text="Internal Weightagewise Report" CssClass="btn btn-info" OnClick="btnWeightarpt_Click" Visible="true" />

                                        <asp:Button ID="BtnExcelCW" TabIndex="1" runat="server" ValidationGroup="show"
                                            Text=" Internal Mark In Excel(Course Wise)" CssClass="btn btn-info" OnClick="BtnExcelCW_Click" Visible="true" />

                                    </div>
                                    <div class="col-12 btn-footer mt-4">
                                        <asp:Button ID="btnIntExcel" TabIndex="1" runat="server" ValidationGroup="show"
                                            Text="Internal Mark Course Wise (Excel)" CssClass="btn btn-info" Visible="true" OnClick="btnIntExcel_Click" />

                                        <asp:Button ID="BtnExcelReport" TabIndex="1" runat="server" Text="Internal Mark Details"
                                            CssClass="btn btn-info" OnClick="BtnExcelReport_Click" ValidationGroup="Excel" />

                                        <asp:Button ID="btnCancel2" runat="server" TabIndex="1" OnClick="btnCancel2_Click"
                                            Text="Cancel" CssClass="btn btn-warning" />
                                    </div>
                                    <asp:ValidationSummary runat="server" ID="ValidationSummary1" ValidationGroup="show" DisplayMode="List"
                                        ShowSummary="false" ShowMessageBox="true" />
                                    <asp:ValidationSummary runat="server" ID="ValidationSummary2" ValidationGroup="Excel" DisplayMode="List"
                                        ShowSummary="false" ShowMessageBox="true" />
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
            <div id="divMsg" runat="server">
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnIntExcel" />
            <asp:PostBackTrigger ControlID="BtnExcelReport" />
            <asp:PostBackTrigger ControlID="btnIntExcel" />
            <asp:PostBackTrigger ControlID="BtnExcelCW" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
