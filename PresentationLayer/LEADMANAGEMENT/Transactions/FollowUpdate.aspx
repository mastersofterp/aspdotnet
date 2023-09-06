<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FollowUpdate.aspx.cs" Inherits="LEADMANAGEMENT_Transactions_FollowUpdate" MasterPageFile="~/SiteMasterPage.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        #ctl00_ContentPlaceHolder1_pnlToday.dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>
    <style>
        #ctl00_ContentPlaceHolder1_pnlUpcoming.dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>

    <style>
        .dynamic-nav-tabs li.active a {
            color: #255282;
            background-color: #fff;
            border-top: 3px solid #255282 !important;
            border-color: #255282 #255282 #fff !important;
        }

        .chiller-theme .nav-tabs.dynamic-nav-tabs > li.active {
            border-radius: 8px;
            background-color: transparent !important;
        }
        /*#showright-sidebar8 {
         display:none;
        }*/
    </style>
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updUpdate"
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
    <asp:UpdatePanel ID="updUpdate" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>
                        <div class="col-md-12 col-sm-12 col-12">
                            <div class="box box-primary">
                                <ul class="nav nav-tabs dynamic-nav-tabs" role="tablist">
                                    <li class="nav-item active" id="liToday" runat="server">
                                        <asp:LinkButton ID="lnkFollowDate" runat="server" OnClick="lnkFollowDate_Click" CssClass="nav-link" TabIndex="1">Today's Folllow-up</asp:LinkButton>
                                        <%--<a class="nav-link active" data-toggle="tab" tabindex="1" href="#tab1">Today's Folllow-up</a>--%>
                                    </li>
                                    <li class="nav-item" id="liUpcoming" runat="server">
                                        <asp:LinkButton ID="lnkUpcoming" runat="server" OnClick="lnkUpcoming_Click" CssClass="nav-link" TabIndex="1">Upcoming</asp:LinkButton>

                                        <%--<a class="nav-link" data-toggle="tab" tabindex="1" href="#tab2">Upcoming</a>--%>
                                    </li>
                                    <li class="nav-item" id="liOverdue" runat="server">
                                        <asp:LinkButton ID="lnkOverdue" runat="server" OnClick="lnkOverdue_Click" CssClass="nav-link" TabIndex="1">Overdue</asp:LinkButton>

                                        <%--<a class="nav-link" data-toggle="tab" tabindex="1" href="#tab3">Overdue</a>--%>
                                    </li>
                                    <li class="nav-item" id="liComplete" runat="server">
                                        <asp:LinkButton ID="lnkComplete" runat="server" OnClick="lnkComplete_Click" CssClass="nav-link" TabIndex="1">Completed</asp:LinkButton>

                                        <%--<a class="nav-link" data-toggle="tab" tabindex="1" href="#tab4">Completed</a>--%>
                                    </li>
                                    <li class="nav-item" id="liAll" runat="server">
                                        <asp:LinkButton ID="lnkAll" runat="server" OnClick="lnkAll_Click" CssClass="nav-link" TabIndex="1">All</asp:LinkButton>

                                        <%--<a class="nav-link" data-toggle="tab" tabindex="1" href="#tab5">All</a>--%>
                                    </li>
                                </ul>
                                <div class="tab-content">
                                    <div class="tab-pane fade show active" id="todaydate" role="tabpanel" runat="server" aria-labelledby="Today'sFolllow-up-tab-tab">
                                        <div>
                                            <asp:UpdateProgress ID="UpdFirst" runat="server" AssociatedUpdatePanelID="UpdToday"
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
                                        <asp:UpdatePanel ID="UpdToday" runat="server">
                                            <ContentTemplate>
                                                <div class="col-12">
                                                    <asp:Panel ID="pnlToday" runat="server">
                                                        <asp:ListView ID="lvTodayFollow" runat="server" Visible="false">
                                                            <LayoutTemplate>
                                                                <div class="sub-heading">
                                                                    <h5>Today's Follow Up List</h5>
                                                                </div>
                                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="divToday">
                                                                    <thead class="bg-light-blue">
                                                                        <tr>
                                                                            <th>Select
                                                                            </th>
                                                                            <th>SrNo
                                                                            </th>
                                                                            <th>Name
                                                                            </th>
                                                                            <th>Email Id
                                                                            </th>
                                                                            <th>Mobile
                                                                            </th>
                                                                            <th>Registration Date
                                                                            </th>
                                                                            <th>Follow Up Date
                                                                            </th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                        <tr id="itemPlaceholder" runat="server" />
                                                                    </tbody>
                                                                </table>
                                                            </LayoutTemplate>
                                                            <ItemTemplate>
                                                                <tr class="item">
                                                                    <td>
                                                                        <asp:CheckBox ID="chkCheck" runat="server" />
                                                                    </td>
                                                                    <td>
                                                                        <%#Container.DataItemIndex+1 %>
                                                                    </td>
                                                                    <td>
                                                                      <asp:Label ID="lblName_Today" runat="server" Text=' <%#Eval("NAME") %>'></asp:Label>
                                                                        <asp:HiddenField ID="hdnUserNo" runat="server" Value='<%#Eval("USERNO")%>' />
                                                                    </td>
                                                                    <td>
                                                                      <asp:Label ID="lblEmail_Today" runat="server" Text=' <%#Eval("EMAILID") %>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                      <asp:Label ID="lblMobile_Today" runat="server" Text=' <%#Eval("MOBILENO") %>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                     <asp:Label ID="lblRegDate_Today" runat="server" Text='<%#Eval("REGDATE") %>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                     <asp:Label ID="lblNextDate_Today" runat="server" Text='<%#Eval("NEXTDATE") %>'></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                            <EmptyDataTemplate>
                                                                <div align="center">
                                                                 Record Not Found
                                                                </div>
                                                            </EmptyDataTemplate>
                                                        </asp:ListView>
                                                    </asp:Panel>
                                                </div>
                                                <div class="col-12 btn-footer">
                                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="btnSubmit_Click" TabIndex="1" ToolTip="Click To Submit."  OnClientClick="return validation();"/>
                                                    <asp:Button ID="btnSendEmail" runat="server" Text="Send Email" CssClass="btn btn-primary" OnClick="btnSendEmail_Click" TabIndex="1" ToolTip="Click To Send Email." Visible="false"/>
                                                </div>
                                            </ContentTemplate>                                            
                                        </asp:UpdatePanel>
                                        <script>
                                            function validation() {
                                                var count = 0;
                                                var numberOfChecked = $('[id*=divToday] td input:checkbox:checked').length;
                                                if (numberOfChecked == 0) {
                                                    alert("Please select atleast one student.");
                                                    return false;
                                                }
                                                else
                                                    return true;
                                            }

                                            </script>
                                    </div>
                                    <div id="upcomindate" role="tabpanel" runat="server" aria-labelledby="Upcoming-tab" visible="false">
                                        <div>
                                            <asp:UpdateProgress ID="UpdSecond" runat="server" AssociatedUpdatePanelID="UpdUpcoming"
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
                                        <asp:UpdatePanel ID="UpdUpcoming" runat="server">
                                            <ContentTemplate>
                                                <div class="col-12">
                                                    <asp:Panel ID="pnlUpcoming" runat="server">
                                                        <asp:ListView ID="lvUpcoming" runat="server">
                                                            <LayoutTemplate>
                                                                <div class="sub-heading">
                                                                    <h5>Upcoming Follow Up List</h5>
                                                                </div>
                                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="divUpcoming">
                                                                    <thead class="bg-light-blue">
                                                                        <tr>
                                                                            <th>Select
                                                                            </th>
                                                                            <th>SrNo
                                                                            </th>
                                                                            <th>Name
                                                                            </th>
                                                                            <th>Email Id
                                                                            </th>
                                                                            <th>Mobile
                                                                            </th>
                                                                            <th>Registration Date
                                                                            </th>
                                                                            <th>Follow Up Date
                                                                            </th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                        <tr id="itemPlaceholder" runat="server" />
                                                                    </tbody>
                                                                </table>
                                                            </LayoutTemplate>
                                                            <ItemTemplate>
                                                                <tr class="item">
                                                                    <td>
                                                                        <asp:CheckBox ID="chkCheck_Upcoming" runat="server" />
                                                                    </td>
                                                                    <td>
                                                                        <%#Container.DataItemIndex+1 %>
                                                                    </td>
                                                                    <td>
                                                                      <asp:Label ID="lblName_Upcoming" runat="server" Text=' <%#Eval("NAME") %>'></asp:Label>
                                                                        <asp:HiddenField ID="hdnUserNo_Upcoming" runat="server" Value='<%#Eval("USERNO")%>' />
                                                                    </td>
                                                                    <td>
                                                                      <asp:Label ID="lblEmail_Upcoming" runat="server" Text='<%#Eval("EMAILID") %>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                      <asp:Label ID="lblMobile_Upcoming" runat="server" Text='<%#Eval("MOBILENO") %>'></asp:Label>

                                                                       <%-- <%#Eval("MOBILENO") %>--%>
                                                                    </td>
                                                                    <td>
                                                                      <asp:Label ID="lblRegDate_Upcoming" runat="server" Text='<%#Eval("REGDATE") %>'></asp:Label>

                                                                        <%--<%#Eval("REGDATE") %>--%>
                                                                    </td>
                                                                    <td>
                                                                      <asp:Label ID="lblNextDate_Upcoming" runat="server" Text='<%#Eval("NEXTDATE") %>'></asp:Label>

                                                                        <%--<%#Eval("NEXTDATE") %>--%>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                            <EmptyDataTemplate>
                                                                <div align="center">
                                                                    Record Not Found
                                                                </div>
                                                            </EmptyDataTemplate>
                                                        </asp:ListView>
                                                    </asp:Panel>
                                                </div>
                                                <div class="col-12 btn-footer">
                                                    <asp:Button ID="btnSubmit_Upcoming" runat="server" Text="Submit" CssClass="btn btn-primary" TabIndex="1" ToolTip="Click To Submit." OnClick="btnSubmit_Upcoming_Click" />
                                                    <asp:Button ID="btnSendEmail_Upcoming" runat="server" Text="Send Email" CssClass="btn btn-primary" TabIndex="1" ToolTip="Click To Send Email." OnClick="btnSendEmail_Upcoming_Click" Visible="false"/>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    <div id="overduedate" role="tabpanel" runat="server" aria-labelledby="overduedate-tab" visible="false">
                                        <div>
                                            <asp:UpdateProgress ID="UpdThird" runat="server" AssociatedUpdatePanelID="UpdOverdue"
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
                                        <asp:UpdatePanel ID="UpdOverdue" runat="server">
                                            <ContentTemplate>
                                                <div class="col-12">
                                                    <asp:Panel ID="pnlOverdue" runat="server">
                                                        <asp:ListView ID="lvOverdue" runat="server" Visible="false">
                                                            <LayoutTemplate>
                                                                <div class="sub-heading">
                                                                    <h5>Overdue Follow Up List</h5>
                                                                </div>
                                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="divOverdue">
                                                                    <thead class="bg-light-blue">
                                                                        <tr>
                                                                            <th>Select
                                                                            </th>
                                                                            <th>SrNo
                                                                            </th>
                                                                            <th>Name
                                                                            </th>
                                                                            <th>Email Id
                                                                            </th>
                                                                            <th>Mobile
                                                                            </th>
                                                                            <th>Registration Date
                                                                            </th>
                                                                            <th>Follow Up Date
                                                                            </th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                        <tr id="itemPlaceholder" runat="server" />
                                                                    </tbody>
                                                                </table>
                                                            </LayoutTemplate>
                                                            <ItemTemplate>
                                                                <tr class="item">
                                                                    <td>
                                                                        <asp:CheckBox ID="chkCheck_Overdue" runat="server" />
                                                                    </td>
                                                                    <td>
                                                                        <%#Container.DataItemIndex+1 %>
                                                                    </td>
                                                                    <td>
                                                                     <asp:Label ID="lblName_Overdue" runat="server" Text='<%#Eval("NAME") %>'></asp:Label>
                                                                        <asp:HiddenField ID="hdnUserNo_Overdue" runat="server" Value='<%#Eval("USERNO") %>' />
                                                                    </td>
                                                                    <td>
                                                                     <asp:Label ID="lblEmail_Overdue" runat="server" Text='<%#Eval("EMAILID") %>'></asp:Label>

                                                                        <%--<%#Eval("EMAILID") %>--%>
                                                                    </td>
                                                                    <td>
                                                                     <asp:Label ID="lblMobile_Overdue" runat="server" Text='<%#Eval("MOBILENO") %>'></asp:Label>

                                                                        <%--<%#Eval("MOBILENO") %>--%>
                                                                    </td>
                                                                    <td>
                                                                     <asp:Label ID="lblRegDate_Overdue" runat="server" Text='<%#Eval("REGDATE") %>'></asp:Label>

                                                                        <%--<%#Eval("REGDATE") %>--%>
                                                                    </td>
                                                                    <td>
                                                                     <asp:Label ID="lblNextDate_Overdue" runat="server" Text='<%#Eval("NEXTDATE") %>'></asp:Label>

                                                                        <%--<%#Eval("NEXTDATE") %>--%>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                            <EmptyDataTemplate>
                                                                <div align="center">
                                                                    Record Not Found
                                                                </div>
                                                            </EmptyDataTemplate>
                                                        </asp:ListView>
                                                    </asp:Panel>
                                                </div>
                                                <div class="col-12 btn-footer">
                                                    <asp:Button ID="btnSubmit_Overdue" runat="server" Text="Submit" CssClass="btn btn-primary" TabIndex="1" ToolTip="Click To Submit." OnClick="btnSubmit_Overdue_Click" />
                                                    <asp:Button ID="btnSendEmail_Overdue" runat="server" Text="Send Email" CssClass="btn btn-primary" TabIndex="1" ToolTip="Click To Send Email." OnClick="btnSendEmail_Overdue_Click" Visible="false"/>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    <div id="completedate" role="tabpanel" runat="server" aria-labelledby="completedate-tab" visible="false">
                                        <div>
                                            <asp:UpdateProgress ID="UpdFourth" runat="server" AssociatedUpdatePanelID="UpdComplete"
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
                                        <asp:UpdatePanel ID="UpdComplete" runat="server">
                                            <ContentTemplate>
                                                <div class="col-12">
                                                    <asp:Panel ID="pnlComplete" runat="server">
                                                        <asp:ListView ID="lvComplete" runat="server" Visible="false">
                                                            <LayoutTemplate>
                                                                <div class="sub-heading">
                                                                    <h5>Complete Follow Up List</h5>
                                                                </div>
                                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="divComplete">
                                                                    <thead class="bg-light-blue">
                                                                        <tr>
                                                                            <th>Select
                                                                            </th>
                                                                            <th>SrNo
                                                                            </th>
                                                                            <th>Name
                                                                            </th>
                                                                            <th>Email Id
                                                                            </th>
                                                                            <th>Mobile
                                                                            </th>
                                                                            <th>Registration Date
                                                                            </th>
                                                                            <th>Follow Up Date
                                                                            </th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                        <tr id="itemPlaceholder" runat="server" />
                                                                    </tbody>
                                                                </table>
                                                            </LayoutTemplate>
                                                            <ItemTemplate>
                                                                <tr class="item">
                                                                    <td>
                                                                        <asp:CheckBox ID="chkCheck_Complete" runat="server" />
                                                                    </td>
                                                                    <td>
                                                                        <%#Container.DataItemIndex+1 %>
                                                                    </td>
                                                                    <td>
                                                                      <asp:Label ID="lblName_Comp" runat="server" Text=' <%#Eval("NAME") %>'></asp:Label>
                                                                        <asp:HiddenField ID="hdnUserNo_Comp" runat="server" Value='<%#Eval("USERNO") %>' />
                                                                    </td>
                                                                    <td>
                                                                      <asp:Label ID="lblEmail_Comp" runat="server" Text=' <%#Eval("EMAILID") %>'></asp:Label>

                                                                        <%--<%#Eval("EMAILID") %>--%>
                                                                    </td>
                                                                    <td>
                                                                      <asp:Label ID="lblMobile_Comp" runat="server" Text=' <%#Eval("MOBILENO") %>'></asp:Label>
                                                                        <%--<%#Eval("MOBILENO") %>--%>
                                                                    </td>
                                                                    <td>
                                                                      <asp:Label ID="lblRegDate_Comp" runat="server" Text=' <%#Eval("REGDATE") %>'></asp:Label>

                                                                        <%--<%#Eval("REGDATE") %>--%>
                                                                    </td>
                                                                    <td>
                                                                      <asp:Label ID="lblNextDate_Comp" runat="server" Text=' <%#Eval("NEXTDATE") %>'></asp:Label>

                                                                        <%--<%#Eval("NEXTDATE") %>--%>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                            <EmptyDataTemplate>
                                                                <div align="center">
                                                                    Record Not Found
                                                                </div>
                                                            </EmptyDataTemplate>
                                                        </asp:ListView>
                                                    </asp:Panel>
                                                </div>
                                                <div class="col-12 btn-footer">
                                                    <asp:Button ID="btnSubmit_Complete" runat="server" Text="Submit" CssClass="btn btn-primary" TabIndex="1" ToolTip="Click To Submit." OnClick="btnSubmit_Complete_Click" />
                                                    <asp:Button ID="btnSendEmail_Complete" runat="server" Text="Send Email" CssClass="btn btn-primary" TabIndex="1" ToolTip="Click To Send Email." OnClick="btnSendEmail_Complete_Click" Visible="false" />
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    <div id="alldate" runat="server" visible="false" role="tabpanel" aria-labelledby="all-tab">
                                        <div>
                                            <asp:UpdateProgress ID="UpdFifth" runat="server" AssociatedUpdatePanelID="UpdAll"
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
                                        <asp:UpdatePanel ID="UpdAll" runat="server">
                                            <ContentTemplate>
                                                <div class="col-12">
                                                    <asp:Panel ID="pnlAllDates" runat="server">
                                                        <asp:ListView ID="lvAll" runat="server" Visible="false">
                                                            <LayoutTemplate>
                                                                <div class="sub-heading">
                                                                    <h5>All Follow Up List</h5>
                                                                </div>
                                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="divAll">
                                                                    <thead class="bg-light-blue">
                                                                        <tr>
                                                                            
                                                                            <th>SrNo
                                                                            </th>
                                                                            <th>Name
                                                                            </th>
                                                                            <th>Email Id
                                                                            </th>
                                                                            <th>Mobile
                                                                            </th>
                                                                            <th>Registration Date
                                                                            </th>
                                                                            <th>Follow Up Date
                                                                            </th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                        <tr id="itemPlaceholder" runat="server" />
                                                                    </tbody>
                                                                </table>
                                                            </LayoutTemplate>
                                                            <ItemTemplate>
                                                                <tr class="item">
                                                                    
                                                                    <td>
                                                                        <%#Container.DataItemIndex+1 %>
                                                                    </td>
                                                                    <td>
                                                                      <asp:Label ID="lblName_All" runat="server" Text=' <%#Eval("NAME") %>'></asp:Label>
                                                                        <asp:HiddenField ID="hdnUser_All" runat="server" Value='<%#Eval("USERNO") %>' />
                                                                    </td>
                                                                    <td>
                                                                      <asp:Label ID="lblEmail_All" runat="server" Text=' <%#Eval("EMAILID") %>'></asp:Label>

                                                                        <%--<%#Eval("EMAILID") %>--%>
                                                                    </td>
                                                                    <td>
                                                                      <asp:Label ID="lblMobile_All" runat="server" Text=' <%#Eval("MOBILENO") %>'></asp:Label>

                                                                        <%--<%#Eval("MOBILENO") %>--%>
                                                                    </td>
                                                                    <td>
                                                                      <asp:Label ID="lblRegDate_All" runat="server" Text=' <%#Eval("REGDATE") %>'></asp:Label>

                                                                        <%--<%#Eval("REGDATE") %>--%>
                                                                    </td>
                                                                    <td>
                                                                      <asp:Label ID="lblNextDate_All" runat="server" Text=' <%#Eval("NEXTDATE") %>'></asp:Label>

                                                                        <%--<%#Eval("NEXTDATE") %>--%>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                            <EmptyDataTemplate>
                                                                <div align="center">
                                                                    Record Not Found
                                                                </div>
                                                            </EmptyDataTemplate>
                                                        </asp:ListView>
                                                    </asp:Panel>
                                                </div>
                                                <div class="col-12 btn-footer">
                                                    <%--<asp:Button ID="btnSubmit_All" runat="server" Text="Submit" CssClass="btn btn-primary" TabIndex="1" ToolTip="Click To Submit." />--%>
                                                    <asp:Button ID="btnSendEmail_All" runat="server" Text="Send Email" CssClass="btn btn-primary" TabIndex="1" ToolTip="Click To Send Email." OnClick="btnSendEmail_All_Click" Visible="false" />
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="lnkUpcoming" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
