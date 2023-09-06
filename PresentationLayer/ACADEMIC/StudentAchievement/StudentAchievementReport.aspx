<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="StudentAchievementReport.aspx.cs" Inherits="ACADEMIC_StudentAchievement_StudentAchievementReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title"><span>Student Achievement Report</span></h3>
                </div>

                <div class="box-body">
                    <div class="col-12">
                        <div class="row">
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Academic Year</label>
                                </div>
                                <asp:DropDownList ID="ddlAcademicYear" runat="server" AppendDataBoundItems="true" CssClass="form-control">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvddlAcademicYear" runat="server" ControlToValidate="ddlAcademicYear"
                                    Display="None" ErrorMessage="Please Select Academic Year" SetFocusOnError="false"
                                    ValidationGroup="Academic" InitialValue="0"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="col-12  btn-footer">
                        <asp:LinkButton ID="btnReport" runat="server" class="text-center" CssClass="btn btn-info" OnClick="btnReport_Click" Text="Event Participation Report" ValidationGroup="Academic"></asp:LinkButton>
                        <asp:LinkButton ID="btnReportMoocs" runat="server" CssClass="btn btn-info" OnClick="btnReportMoocs_Click" Text="Moocs Certification Report" ValidationGroup="Academic"></asp:LinkButton>
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="Academic" />

                    </div>

                </div>
            </div>
        </div>
    </div>

</asp:Content>


<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
    <style type="text/css">
        .btn-info {
            margin-left: 0px;
        }
    </style>
</asp:Content>



