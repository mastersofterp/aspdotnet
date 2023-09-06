<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="StatusReport.aspx.cs" Inherits="EMP_APPRAISAL_TRANSACTION_StatusReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updActivity"
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
        .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>

    <asp:UpdatePanel ID="updActivity" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div3" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">APPRAISAL STATUS REPORT</h3>
                        </div>
                        <div class="box-body">
                            <asp:Panel ID="pnlStatus" runat="server">
                                <div class="col-12">
                                    <div class="sub-heading">
                                        <h5>Select Session</h5>
                                    </div>
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Session</label>
                                            </div>
                                            <asp:DropDownList ID="ddlSession" runat="server" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" TabIndex="1"
                                                OnSelectedIndexChanged="ddlSession_SelectedIndexChanged" ToolTip="Select Session">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="div2" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Department</label>
                                            </div>
                                            <asp:DropDownList ID="ddlDeptStatusRepo" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" TabIndex="2"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlDeptStatusRepo_SelectedIndexChanged" ToolTip="Select Department">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>

                            <div class="col-12 btn-footer" visible="false">
                                <%--<asp:Button ID="btnReport" runat="server" Text="Report" CssClass="btn btn-outline-primary" ToolTip="Click here to Show Report"
                                    OnClick="btnRport_Click" ValidationGroup="Report" TabIndex="3"  Visible="false"/>
                                <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Report" />--%>
                            </div>
                            <div class="col-12 mt-3 mb-3">
                                <asp:Panel ID="pnlEmpStatus" runat="server">

                                    <asp:Panel ID="Panel1" runat="server">
                                        <asp:ListView ID="lvEmpStatus" runat="server">
                                            <LayoutTemplate>
                                                <div id="lgv1">
                                                    <div class="sub-heading">
                                                        <h5>Employee List Who Complete/Incomplete Appraisal Form </h5>
                                                    </div>
                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th>Emp Code</th>
                                                                <th>Name</th>
                                                                <th>Department</th>
                                                                <th>Designation</th>
                                                                <th>User Status</th>
                                                                <th>Reporting Officer Status</th>
                                                                <th>Print</th>
                                                                <th>Review</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <%# Eval("PFILENO") %>                                                                   
                                                    </td>
                                                    <td>
                                                        <%# Eval("NAME") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("SUBDEPT") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("SUBDESIG") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("LOCK") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("REPORT_LOCK") %>
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnPrint" runat="server" Text="Print" CommandName='<%# Eval("IDNO") %>' OnClick="btnPrint_Click"
                                                            CssClass="btn btn-outline-primary" Enabled='<%#Eval("LOCK").ToString() == "INCOMPLETE" ? false : true %>'
                                                            CommandArgument='<%# Eval("SRNO") %>' />
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnReview" runat="server" Text="Review" CommandName='<%# Eval("EMPLOYEE_TYPE") %>' OnClick="btnReview_Click"
                                                            CssClass="btn btn-outline-primary" CommandArgument='<%# Eval("SRNO") %>'
                                                            Enabled='<%#Eval("LOCK").ToString() == "COMPLETE" ? true : false %>' ToolTip='<%# Eval("IDNO") %>' />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </asp:Panel>
                                   <%-- <div class="vista-grid_datapager d-none">
                                        <div class="text-center">
                                            <asp:DataPager ID="dpPager" runat="server" OnPreRender="dpPager_PreRender" PagedControlID="lvEmpStatus" PageSize="10">
                                                <Fields>
                                                    <asp:NextPreviousPagerField ButtonType="Link" FirstPageText="&lt;&lt;" PreviousPageText="&lt;" RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="true" ShowLastPageButton="false" ShowNextPageButton="false" ShowPreviousPageButton="true" />
                                                    <asp:NumericPagerField ButtonCount="10" ButtonType="Link" CurrentPageLabelCssClass="Current" />
                                                    <asp:NextPreviousPagerField ButtonType="Link" LastPageText="&gt;&gt;" NextPageText="&gt;" RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="false" ShowLastPageButton="true" ShowNextPageButton="true" ShowPreviousPageButton="false" />
                                                </Fields>
                                            </asp:DataPager>
                                        </div>
                                    </div>--%>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>



