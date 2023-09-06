<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    MaintainScrollPositionOnPostback="true" CodeFile="StudentPrecouncelling.aspx.cs"
    Inherits="ACADEMIC_StudentPrecouncelling" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updLists"
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

    <asp:UpdatePanel ID="updLists" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">PRE - COUNCELING</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-12">
                                        <asp:RadioButtonList ID="rdbSelection" runat="server" RepeatDirection="Horizontal" ClientIDMode="Static" OnSelectedIndexChanged="rdbSelection_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0" style="padding-right: 50px" Text="Pre Counceling Student Entry" Selected="True"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="Pre Counceling Student Report"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="rApplicationID" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Application ID</label>
                                        </div>
                                        <asp:TextBox ID="txtAppID" runat="server" ToolTip="Please Enter Application ID" ValidationGroup="submit" MaxLength="16"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="None" ErrorMessage="Please Enter Application ID"
                                            ValidationGroup="show" ControlToValidate="txtAppID"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="rAdmbatch" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Admission batch</label>
                                        </div>
                                        <asp:DropDownList ID="ddlAdmbatch" runat="server" CssClass="form-control" AppendDataBoundItems="true" TabIndex="1">
                                            <asp:ListItem>Please Select </asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlAdmbatch"
                                            Display="None" ValidationGroup="report" InitialValue="0"
                                            ErrorMessage="Please Select Admission batch."></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                    ShowMessageBox="True" ValidationGroup="show" ShowSummary="False"/>
                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List"
                                    ShowMessageBox="True" ValidationGroup="submit" ShowSummary="false" />
                                <asp:ValidationSummary ID="ValidationSummary3" runat="server" DisplayMode="List"
                                    ShowMessageBox="True" ValidationGroup="report" ShowSummary="false" />
                                    
                                <asp:Button ID="btnShow" runat="server" Text="Show Details" ValidationGroup="show" OnClick="btnShow_Click" CssClass="btn btn-primary" Visible="false" />
                                <asp:Button ID="btnPdfreport" runat="server" Text="Report" ValidationGroup="report"  OnClick="btnPdfreport_Click" CssClass="btn btn-info" Visible="false" />
                                <asp:Button ID="btncancel" runat="server" Text="Cancel" OnClick="btncancel_Click" CssClass="btn btn-warning" />
                            </div>

                            <div class="col-12" id="divdetails" runat="server" visible="false">
                                <asp:Panel ID="fldstudent" runat="server" Visible="false">
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="sub-heading">
                                                <h5>Student Information</h5>
                                            </div>
                                        </div>

                                        <div class="col-lg-6 col-md-6 col-12 pl-md-0 pl-3">
                                            <ul class="list-group list-group-unbordered ipad-view">
                                                <li class="list-group-item"><b>Application ID :</b>
                                                    <a class="sub-label"><asp:Label ID="lblAppID" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                <li class="list-group-item"><b>Name of Candidate :</b>
                                                    <a class="sub-label"><asp:Label ID="lblName" runat="server" Text="" Font-Bold="true"></asp:Label></a>
                                                </li>
                                                <li class="list-group-item"><b>Mobile No :</b>
                                                    <a class="sub-label"><asp:Label ID="lblMobileNo" runat="server" Text="" Font-Bold="true"></asp:Label></a>
                                                </li>
                                                <li class="list-group-item"><b>Email ID :</b>
                                                    <a class="sub-label"><asp:Label ID="lblEmail_id" runat="server" Text="" Font-Bold="true"></asp:Label></a>
                                                </li>        
                                            </ul>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Entrance Exam</label>
                                            </div>
                                            <asp:DropDownList ID="ddlentranceExam" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" TabIndex="1">
                                                <asp:ListItem>Please Select </asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddlentranceExam" runat="server" ControlToValidate="ddlentranceExam"
                                                Display="None" ValidationGroup="submit" InitialValue="0"
                                                ErrorMessage="Please Select Entrance Exam."></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Scholarship</label>
                                            </div>
                                            <asp:RadioButtonList ID="rdbScholarship" runat="server" RepeatDirection="Horizontal" ClientIDMode="Static">
                                                <asp:ListItem Value="0" style="padding-right: 30px">NO</asp:ListItem>
                                                <asp:ListItem Value="1">YES</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>

                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" ValidationGroup="submit" OnClick="btnSubmit_Click" />
                                        <asp:Button ID="btnSingleReport" runat="server" Text="Report" CssClass="btn btn-warning" OnClick="btnSingleReport_Click" Visible="false" />
                                    </div>

                                    <div class="col-12 btn-footer">
                                        <asp:Label ID="lblNote1" runat="server" Text=""></asp:Label></b>
                                        <div id="divMsg" runat="server">
                                        </div>
                                    </div>

                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSubmit" />
            <asp:PostBackTrigger ControlID="btnSingleReport" />
            <asp:PostBackTrigger ControlID="btnShow" />
            <asp:PostBackTrigger ControlID="btncancel" />
            <%--<asp:PostBackTrigger ControlID="btnPdfreport" />--%>
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>
