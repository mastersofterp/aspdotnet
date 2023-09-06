<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="OutstandingFeesReport.aspx.cs" Inherits="Academic_OutstandingFeesReport"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div2" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">Outstanding Fee Reports</h3>
                </div>

                <div class="box-body">
                    <div class="col-12">
                        <div class="row">
                            <div class="col-12">
                                <div class="sub-heading">
                                    <h5>Report Types</h5>
                                </div>
                            </div>
                            <div class="form-group col-lg-12 col-md-12 col-12">

                                <asp:RadioButton ID="rdoShowAllStudent" Text="Show All Students" GroupName="ReportType"
                                    Checked="true" TabIndex="1" runat="server" />
                                &nbsp;&nbsp;&nbsp;

                                <asp:RadioButton ID="rdoDetailedReport" Text="Detailed Report" CssClass="data_label"
                                    Checked="true" GroupName="RptSubType" TabIndex="2" runat="server" />&nbsp;&nbsp;&nbsp;
                                <asp:RadioButton ID="rdoSummeryReport" Text="Summary Report" CssClass="data_label"
                                    GroupName="RptSubType" TabIndex="2" runat="server" />&nbsp;&nbsp;&nbsp;
                                <asp:RadioButton ID="rdoShowStudentsWithBalance" Text="Show Students having Balance."
                                    GroupName="ReportType" TabIndex="1" runat="server" />
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup></sup>
                                    <label>Receipt Type</label>
                                </div>
                                <asp:DropDownList ID="ddlReceiptType" runat="server"
                                    AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                    <asp:ListItem Selected="True" Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-12">
                                <div class="sub-heading">
                                    <h5>Data Filters</h5>
                                </div>
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup></sup>
                                    <label>Degree </label>
                                </div>
                                <asp:DropDownList ID="ddlDegree" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true"
                                    TabIndex="8" />
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup></sup>
                                    <label>Year </label>
                                </div>
                                <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true"
                                    TabIndex="10" />
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup></sup>
                                    <label>Branch </label>
                                </div>
                                <asp:DropDownList ID="ddlBranch" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true"
                                    TabIndex="9" />
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup></sup>
                                    <label>Semester </label>
                                </div>
                                <asp:DropDownList ID="ddlSemester" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true"
                                    TabIndex="11" />
                            </div>
                        </div>
                    </div>

                    <div class="col-12 btn-footer">
                        <asp:Button ID="btnReport" runat="server" Text="Report" OnClick="btnReport_Click"
                            TabIndex="12" ValidationGroup="report" CssClass="btn btn-info"/>
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                            OnClick="btnCancel_Click" TabIndex="13" CssClass="btn btn-warning"/>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div id="divMsg" runat="server">
    </div>
</asp:Content>
