<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Phd_Thesis_Tracking_Status.aspx.cs" Inherits="ACADEMIC_PHD_Phd_Thesis_Tracking_Status" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="<%=Page.ResolveClientUrl("~/plugins/multi-select/bootstrap-multiselect.css") %>" rel="stylesheet" />
    <script src="<%=Page.ResolveClientUrl("~/plugins/multi-select/bootstrap-multiselect.js")%>"></script>


    <style>
        #ctl00_ContentPlaceHolder1_pnlSession .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>
    <style>
        .Tab:focus {
            outline: none;
            box-shadow: 0px 0px 5px 2px #61C5FA !important;
        }

        .Background {
            background-color: black;
            opacity: 0.9;
        }
    </style>
    <style>
        .dataTables_scrollHeadInner {
            width: max-content!important;
        }
    </style>
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updSession"
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

    <asp:UpdatePanel ID="updSession" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <%--<h3 class="box-title">SESSION CREATION</h3>--%>
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server">Ph.D. Thesis Tracking Status</asp:Label></h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divAdmBatch" runat="server" visible="false">
                                        <span style="color: red;">*</span><label>Admission Batch</label>
                                        <asp:DropDownList ID="ddlAdmBatch" runat="server" TabIndex="2" CssClass="form-control" data-select2-enable="true"
                                            AppendDataBoundItems="True" ToolTip="Please Select Admission Batch" AutoPostBack="true" OnSelectedIndexChanged="ddlAdmBatch_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12" id="divStudentlist" runat="server" visible="false">
                                <div class="sub-heading">
                                    <h5>Student List</h5>
                                </div>
                                <asp:Panel ID="pnlStudentlist" runat="server">
                                    <asp:ListView ID="lvStudentlist" runat="server">
                                        <LayoutTemplate>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Sr. No
                                                        </th>
                                                        <th style="text-align: center;">History
                                                        </th>
                                                        <th>RRN
                                                        </th>
                                                        <th>Student Name
                                                        </th>
                                                        <th>Program /Branch
                                                        </th>
                                                        <th>Thesis Title
                                                        </th>
                                                        <th>Submission Date
                                                        </th>
                                                        <th>Current Status
                                                        </th>
                                                        <th>Remark
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <%# Container.DataItemIndex + 1%>
                                                </td>
                                                <td style="text-align: center;">
                                                    <asp:ImageButton ID="btnView" class="newAddNew Tab" runat="server" CausesValidation="false" ImageUrl="~/Images/view.png"
                                                        CommandArgument='<%# Eval("IDNO")%>' AlternateText="View History" ToolTip="View History"
                                                        OnClick="btnView_Click" TabIndex="6" />
                                                </td>
                                                <td>
                                                    <%# Eval("REGNO") %>
                                                </td>
                                                <td>
                                                    <%# Eval("STUDNAME") %>
                                                </td>
                                                <td>
                                                    <%# Eval("LONGNAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("THESIS_TITLE")%>
                                                </td>
                                                <td>
                                                    <%# Eval("THESIS_SUBMIT_DATE")%>
                                                </td>
                                                <td>
                                                    <%# Eval("FLAG_NAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("FLAG_REMARK")%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                    <asp:LinkButton ID="lnkPopup" runat="server"></asp:LinkButton>
                                </asp:Panel>
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnExcel" runat="server" Text="Export to Excel" TabIndex="5" CssClass="btn btn-info" OnClick="btnExcel_Click" Visible="false" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <asp:Panel ID="pnlHistory" runat="server">
                <asp:UpdatePanel ID="updMaindiv" runat="server">
                    <ContentTemplate>
                        <div class="modal-dialog  modal-lg">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h4 class="modal-title" style="font-weight: bold">Status History</h4>
                                    <button type="button" id="btnClose" class="close" data-dismiss="modal">&times;</button>
                                </div>

                                <div class="modal-body" style="padding: 2rem">
                                    <div class="col-12">

                                        <asp:ListView ID="lvstustslist" runat="server" RepeatDirection="Vertical">
                                            <LayoutTemplate>
                                                <table class="table table-striped table-bordered nowrap table-responsive">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th style="padding: 10px">Status</th>
                                                            <th style="padding: 10px">Status Date</th>
                                                            <th style="padding: 10px">Remarks</th>
                                                        </tr>
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </tbody>
                                                </table>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td style="padding: 10px">
                                                        <asp:Label ID="lblStatusname" runat="server" Text='<%# Eval("STATUSNAME")%>'></asp:Label>
                                                    </td>
                                                    <td style="padding: 10px">
                                                        <asp:Label ID="lblSubmitdate" runat="server" Text='<%# Eval("CREATE_DATE")%>'></asp:Label>
                                                    </td>
                                                    <td style="padding: 10px">
                                                        <asp:Label ID="lblRemarks" runat="server" Text='<%# Eval("REMARK")%>'></asp:Label>
                                                    </td>
                                                </tr>

                                            </ItemTemplate>
                                        </asp:ListView>
                                        <%--<tr>
                                                    <td style="padding: 10px">Thesis sent to Examiner
                                                    </td>
                                                    <td style="padding: 10px">
                                                        <asp:Label ID="lblDispatch" runat="server" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 10px">Indian Examiner Report Received
                                                    </td>
                                                    <td style="padding: 10px">
                                                        <asp:Label ID="lblReviewer" runat="server" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 10px">Public Viva Voce Scheduled 
                                                    </td>
                                                    <td style="padding: 10px">
                                                        <asp:Label ID="lblDefence" runat="server" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 10px">Awarded 
                                                    </td>
                                                    <td style="padding: 10px">
                                                        <asp:Label ID="lblAwarded" runat="server" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                                  <tr>
                                                    <td style="padding: 10px">Foreign Examiner Report Received 
                                                    </td>
                                                    <td style="padding: 10px">
                                                        <asp:Label ID="lblForeign" runat="server" Text=""></asp:Label>
                                                    </td>
                                                </tr>--%>
                                    </div>
                                </div>
                            </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:Panel>

            <ajaxToolKit:ModalPopupExtender ID="MpHistory" runat="server" TargetControlID="lnkPopup" PopupControlID="pnlHistory" BackgroundCssClass="Background" CancelControlID="btnClose"></ajaxToolKit:ModalPopupExtender>


        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExcel" />
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>

