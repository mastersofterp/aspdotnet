<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="StudentAdmitCardReportForStudent.aspx.cs" Inherits="ACADEMIC_StudentAdmitCardReportForStudent" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<%--<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>--%>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updAdmit"
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


    <div class="box-body" id="divExamHalTckt" runat="server">
        <asp:UpdatePanel ID="updAdmit" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-md-12 col-sm-12 col-12">
                        <div class="box box-primary">
                            <div id="div1" runat="server"></div>
                            <div class="box-header with-border">
                                <h3 class="box-title">Exam Hall Ticket</h3>
                            </div>

                            <div class="box-body">
                                <div class="col-12">
                                    <asp:Panel ID="pnHallTicKet" runat="server">
                                        <asp:ListView ID="lvHallTicketDetails" runat="server">
                                            <LayoutTemplate>
                                                <div class="sub-heading">
                                                    <h5></h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap" style="width: 100%">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>Session</th>
                                                            <th>Semester</th>
                                                            <th>Exam Name</th>
                                                            <th>Section</th>
                                                            <th>Download Hall Ticket</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </tbody>
                                                </table>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td><%# Eval("SESSION_NAME")%>
                                                          <asp:HiddenField ID="hidIdNo" runat="server" Value='<%# Eval("IDNO") %>' Visible="false" />
                                                           <asp:HiddenField ID="hdfdegreeno" runat="server" Value='<%# Eval("DEGREENO") %>' Visible="false" />
                                                           <asp:HiddenField ID="hdfbranchno" runat="server" Value='<%# Eval("BRANCHNO") %>' Visible="false" />
                                                           <asp:HiddenField ID="hdSessionno" runat="server" Value='<%# Eval("SESSIONNO") %>' Visible="false" />
                                                    </td>
                                                    <td><%# Eval("SEMESTERNAME")%>
                                                        <asp:HiddenField ID="hdfsem" runat="server" Value='<%# Eval("SEMESTERNO") %>' Visible="false" />
                                                    </td>
                                                    <td><%# Eval("EXAMNAME")%>
                                                     <asp:HiddenField ID="hdfprev_status" runat="server" Value='<%# Eval("PREV_STATUS") %>' Visible="false" />
                                                     <asp:HiddenField ID="hdexamno" runat="server" Value='<%# Eval("EXAMNO") %>' Visible="false"/>
                                                     <asp:HiddenField ID="hdExamName" runat="server" Value='<%# Eval("EXAMNAME") %>' Visible="false"/>
                                                     <asp:HiddenField ID="hdSection" runat="server" Value='<%# Eval("SECTIONNO") %>' Visible="false" />
                                                     <asp:HiddenField ID="hdSchemeno" runat="server" Value='<%# Eval("SCHEMENO") %>' Visible="false" />
                                                    </td>
                                                    <th><%# Eval("SECTIONNAME")%></th>
                                                    <td>
                                                        <asp:LinkButton ID="btnHallTicket" runat="server" Text="Hall Ticket" OnClick="btnHallTicket_Click" CausesValidation="false" CommandName='<%# Eval("EXAMNO") %>' CommandArgument='<%# Eval("IDNO") %>' ToolTip=<%# Container.DataItemIndex + 1 %>/>
                                                        <%--<asp ID="btnEdit" runat="server" AlternateText="Edit Record" CausesValidation="false" CommandArgument='<%# Eval("SESSION_ACTIVITY_NO") %>' ImageUrl="~/Images/edit.png" OnClick="btnEdit_Click" TabIndex="10" ToolTip="Edit Record" />--%>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </asp:Panel>
                                </div>


                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="lvHallTicketDetails" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <div id="divMsg" runat="server">
    </div>
</asp:Content>
