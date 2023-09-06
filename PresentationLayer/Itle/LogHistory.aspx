<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="LogHistory.aspx.cs" Inherits="Itle_LogHistory" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style>
    .list-group .list-group-item .sub-label {
            float: initial;
        }
    </style>
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div2" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">STUDENT LOG HISTORY</h3>
                </div>
                <div class="box-body">
                    <div class="col-12">
                        <asp:Panel ID="pnlLogHistory" runat="server">
                            <div class="row mb-3">
                                <div class="col-12 btn-footer">
                                    <asp:Label ID="lblMsg" runat="server" SkinID="Msglbl"></asp:Label>
                                </div>
                                <div class="col-lg-5 col-md-6 col-12">
                                    <ul class="list-group list-group-unbordered">
                                        <li class="list-group-item"><b>Session  :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblSession" runat="server" Text="lblSession" Font-Bold="True"></asp:Label>
                                            </a>
                                        </li>
                                    </ul>
                                </div>
                            </div>
                            <asp:UpdatePanel ID="updLogHistory" runat="server">
                                <ContentTemplate>
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Degree</label>
                                            </div>
                                            <asp:DropDownList ID="ddldegree" runat="server" AppendDataBoundItems="true"
                                                CssClass="form-control" data-select2-enable="true" ValidationGroup="Select Schemeno"
                                                ToolTip="Select Degree" TabIndex="1"
                                                OnSelectedIndexChanged="ddldegree_SelectedIndexChanged"
                                                AutoPostBack="True">
                                            </asp:DropDownList>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Branch/Basic Course</label>
                                            </div>
                                            <asp:DropDownList ID="ddlbranch" runat="server" AppendDataBoundItems="true"
                                                CssClass="form-control" data-select2-enable="true" ValidationGroup="Select Schemeno"
                                                ToolTip="Select Branch" TabIndex="2"
                                                OnSelectedIndexChanged="ddlbranch_SelectedIndexChanged"
                                                AutoPostBack="True">
                                            </asp:DropDownList>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Scheme/Course</label>
                                            </div>
                                            <asp:DropDownList ID="ddlSchemeNo" runat="server" AppendDataBoundItems="true"
                                                CssClass="form-control" data-select2-enable="true" ValidationGroup="Select Schemeno"
                                                ToolTip="Select Scheme" TabIndex="3"
                                                OnSelectedIndexChanged="ddlSchemeNo_SelectedIndexChanged"
                                                AutoPostBack="True">
                                            </asp:DropDownList>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Semester</label>
                                            </div>
                                            <asp:DropDownList ID="ddlsemester" runat="server" AppendDataBoundItems="true"
                                                CssClass="form-control" data-select2-enable="true" ValidationGroup="Select Schemeno"
                                                ToolTip="Select Semester" TabIndex="4"
                                                OnSelectedIndexChanged="ddlsemester_SelectedIndexChanged"
                                                AutoPostBack="True">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Section</label>
                                            </div>
                                            <asp:DropDownList ID="ddlsection" runat="server" AppendDataBoundItems="true"
                                                CssClass="form-control" data-select2-enable="true" ValidationGroup="Select Schemeno"
                                                ToolTip="Select Section" TabIndex="5"
                                                OnSelectedIndexChanged="ddlsection_SelectedIndexChanged"
                                                AutoPostBack="True">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Student</label>
                                            </div>
                                            <asp:DropDownList ID="ddlStudent" runat="server" AppendDataBoundItems="true"
                                                CssClass="form-control" data-select2-enable="true" ValidationGroup="Select Schemeno"
                                                ToolTip="Select Student" TabIndex="6" AutoPostBack="True">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <div class="row">
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <label>Date</label>
                                    </div>
                                    <div class="input-group date">
                                        <div class="input-group-addon">
                                            <i class="fa fa-calendar text-blue"></i>
                                        </div>
                                        <asp:TextBox ID="txtLogDate" runat="server" CssClass="form-control" TabIndex="6"
                                            ValidationGroup="process" ToolTip="Enter  Date" Style="z-index: 0;" />
                                        <ajaxToolKit:CalendarExtender ID="ceResultDate" runat="server" Format="dd/MM/yyyy"
                                            PopupButtonID="imgCalResultDate" TargetControlID="txtLogDate" />
                                        <ajaxToolKit:MaskedEditExtender ID="meeResultDate" runat="server" ErrorTooltipEnabled="true"
                                            Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true" OnInvalidCssClass="errordate"
                                            TargetControlID="txtLogDate" />
                                        <ajaxToolKit:MaskedEditValidator ID="mevResultDate" runat="server" ControlExtender="meeResultDate"
                                            ControlToValidate="txtLogDate" Display="Dynamic" EmptyValueBlurredText="*"
                                            EmptyValueMessage=" Date Required" InvalidValueBlurredMessage=""
                                            InvalidValueMessage=" Date is Invalid!!"
                                            IsValidEmpty="False" />
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnReport" runat="server" Text="Log History Report"
                                    OnClick="btnReport_Click" CssClass="btn btn-primary" TabIndex="8"
                                    ValidationGroup="Select Roll List Report Button"
                                    ToolTip="Click here to Show Log History Selected Criteria" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel"
                                    OnClick="btnCancel_Click" ValidationGroup="Cancel Button"
                                    ToolTip="Click here to Reset" CssClass="btn btn-warning"
                                    TabIndex="9" />
                            </div>

                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div id="divMsg" runat="server"></div>
</asp:Content>

