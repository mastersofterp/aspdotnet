<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/SiteMasterPage.master" CodeFile="Academic_SummeryReport.aspx.cs"
    Inherits="ACADEMIC_Academic_SummeryReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
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

    <style>
        .checkbox-list-box {
            height: 20px;
        }

        @media (max-width: 767px) {
            .checkbox-list-box {
                min-height: 25px;
                height: auto;
            }
        }
    </style>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <%--<h3 class="box-title">STUDENT STRENGTH SUMMARY REPORT</h3>--%>
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--<label>Session</label>--%>
                                            <asp:Label ID="lblDYddlSession" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="true" Font-Bold="true" CssClass="form-control" data-select2-enable="true"
                                            AutoPostBack="true" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlSession" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="submit"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--<label>College</label>--%>
                                            <asp:Label ID="lblDYddlSchool" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlCollege" runat="server" AppendDataBoundItems="True" ToolTip="Please Select School/Institute." AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true" ValidationGroup="submit" TabIndex="1">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlCollege"
                                            Display="None" ErrorMessage="Please Select School/Institute." InitialValue="0" SetFocusOnError="true"
                                            ValidationGroup="submit">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Academic Year</label>
                                        </div>
                                        <asp:DropDownList ID="ddlAcdYear" runat="server" AutoPostBack="true" AppendDataBoundItems="true" TabIndex="2" ValidationGroup="show" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>

                                        <%--<asp:RequiredFieldValidator ID="rfvAcademicYear" runat="server" ControlToValidate="ddlAcdYear"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select  Academic Year" ValidationGroup="submit">
                                        </asp:RequiredFieldValidator>--%>
                                    </div>

                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Summary By</label>
                                        </div>
                                        <div class="form-group col-md-12 checkbox-list-box">
                                            <asp:CheckBoxList ID="chkReportType" runat="server" CellPadding="1" CellSpacing="8" RepeatColumns="2" AppendDataBoundItems="true" RepeatDirection="Horizontal" CssClass="checkbox-list-style">
                                                <asp:ListItem Value="0"> DEGREE &nbsp;&nbsp;</asp:ListItem>
                                                <asp:ListItem Value="1"> BATCH &nbsp;&nbsp;</asp:ListItem>
                                                <asp:ListItem Value="2"> BRANCH &nbsp;&nbsp;</asp:ListItem>
                                                <asp:ListItem Value="3"> CATEGORY &nbsp;&nbsp;</asp:ListItem>
                                            </asp:CheckBoxList>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 mt-4 btn-footer">
                                <asp:Button ID="btnView" runat="server" Text="View"
                                    ValidationGroup="submit" CssClass="btn btn-primary" OnClick="btnView_Click" />
                                <asp:Button ID="btnShowReports" runat="server" Text="Export In Excel" Visible="false"
                                    ValidationGroup="submit" TabIndex="9" CssClass="btn btn-info" OnClick="btnShowReports_Click" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel"
                                    TabIndex="10" CssClass="btn btn-warning" OnClick="btnCancel_Click" />
                                <asp:ValidationSummary ID="valSummery" DisplayMode="List" runat="server" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="submit" />
                            </div>

                            <div class="col-12" id="dvGrid" runat="server" visible="false">
                                <div style="height: 400px; overflow: auto;">
                                    <%-- <asp:Panel ID="panell" runat="server" style="overflow-x:scroll;"  Visible="true">--%>
                                    <asp:GridView ID="lstDetails" runat="server" CellPadding="6" EmptyDataText="No Records Found" CssClass="Freezing" EnableModelValidation="True" ForeColor="#333333" GridLines="Horizontal" Height="105px">
                                        <AlternatingRowStyle BackColor="White" />
                                        <EditRowStyle BackColor="#2461BF" />
                                        <FooterStyle BackColor="#1b7a87" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#1B7A87" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#EFF3FB" />
                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    </asp:GridView>
                                    <%--</asp:Panel>--%>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnView" />
            <asp:PostBackTrigger ControlID="btnShowReports" />
        </Triggers>
    </asp:UpdatePanel>

    <div id="divMsg" runat="server">
    </div>
</asp:Content>
