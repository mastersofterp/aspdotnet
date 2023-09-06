<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Phd_Thesis_Approval_Entry.aspx.cs" Inherits="ACADEMIC_PHD_Phd_Thesis_Approval_Entry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="<%=Page.ResolveClientUrl("~/plugins/multi-select/bootstrap-multiselect.css") %>" rel="stylesheet" />
    <script src="<%=Page.ResolveClientUrl("~/plugins/multi-select/bootstrap-multiselect.js")%>"></script>


    <style>
        #ctl00_ContentPlaceHolder1_pnlSession .dataTables_scrollHeadInner {
            width: max-content !important;
        }

        #ctl00_ContentPlaceHolder1_divStudentlist .dataTables_scrollHeadInner {
            width: max-content!important;
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
                                <asp:Label ID="lblDynamicPageTitle" runat="server">Ph.D. Thesis Approval Entry</asp:Label></h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12" id="divEntry" runat="server" visible="false">
                                <div class="row">
                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Thesis Submission Date</label>
                                        </div>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i id="dvcal1" runat="server" class="fa fa-calendar text-blue"></i>
                                            </div>
                                            <asp:TextBox ID="txtThesisdate" runat="server" ValidationGroup="submit" onpaste="return false; __/__/____"
                                                ToolTip="Please Enter Thesis Submission Date" CssClass="form-control" Style="z-index: 0;" Enabled="false" />
                                   
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Title Thesis</label>
                                        </div>
                                        <asp:TextBox ID="txtTitleThesis" runat="server" AutoComplete="off" CssClass="form-control" MaxLength="300"
                                            ToolTip="Please Enter Thesis Title" placeholder="Enter Thesis Title" Enabled="false" />
                                    
                                    </div>
                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Synopsis Submission Date</label>
                                        </div>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i id="dvcal2" runat="server" class="fa fa-calendar text-blue"></i>
                                            </div>
                                            <asp:TextBox ID="txtSynopsisDate" runat="server" ValidationGroup="submit" onpaste="return false; __/__/____"
                                                ToolTip="Please Enter Synopsis Submission Date" CssClass="form-control" Style="z-index: 0;" Enabled="false" />
                                           
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Examiner Type</label>
                                        </div>
                                        <asp:DropDownList ID="ddlExaminerType" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" ToolTip="Select Examiner Type" TabIndex="1">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <asp:ListItem Value="1">Indian</asp:ListItem>
                                            <asp:ListItem Value="2">Foreign</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="submit2" ControlToValidate="ddlExaminerType" Display="None"
                                            ErrorMessage="Please Select Examiner Type." InitialValue="0" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Status</label>
                                        </div>
                                        <asp:DropDownList ID="ddlStatus" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" ToolTip="Select Status" TabIndex="2" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <%--<asp:ListItem Value="1">Submission </asp:ListItem>
                                            <asp:ListItem Value="2">Dispatched to Reviewer</asp:ListItem>
                                            <asp:ListItem Value="3">Reviewer Report Received</asp:ListItem>
                                            <asp:ListItem Value="4">Open Defence Viva Scheduled</asp:ListItem>
                                            <asp:ListItem Value="5">Awarded</asp:ListItem>--%>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSessionCollege" runat="server" ValidationGroup="submit2" ControlToValidate="ddlStatus" Display="None"
                                            ErrorMessage="Please Select Session." InitialValue="0" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Remark</label>
                                        </div>
                                        <asp:TextBox ID="txtRemark" runat="server" AutoComplete="off" CssClass="form-control" MaxLength="256"
                                            ToolTip="Please Enter Remark" placeholder="Enter Remark" />
                                        <asp:RequiredFieldValidator ID="rfvRemark" runat="server" SetFocusOnError="True"
                                            ErrorMessage="Please Enter Remark" ControlToValidate="txtRemark"
                                            Display="None" ValidationGroup="submit" />
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender36" runat="server" TargetControlID="txtRemark" FilterMode="InvalidChars"
                                            InvalidChars="~`!@#$%^&*+<>?">
                                        </ajaxToolKit:FilteredTextBoxExtender>
                                    </div>
                                </div>
                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="submit"
                                        TabIndex="4" CssClass="btn btn-primary" OnClick="btnSubmit_Click" OnClientClick="return validaterev();" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="5" CssClass="btn btn-warning" OnClick="btnCancel_Click" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                                        ShowSummary="false" DisplayMode="List" ValidationGroup="submit" />
                                </div>
                            </div>

                            <div class="col-12" id="divStudentlist" runat="server">
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
                                                        <th style="text-align: center;">Edit
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
                                                    <asp:ImageButton ID="btnEdit" class="newAddNew Tab" runat="server" CausesValidation="false" ImageUrl="~/Images/edit.png"
                                                        CommandArgument='<%# Eval("IDNO")%>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                        OnClick="btnEdit_Click" TabIndex="6" />
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
                                <asp:Button ID="btnExcel" runat="server" Text="Export to Excel" TabIndex="5" CssClass="btn btn-info" OnClick="btnExcel_Click" />
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
                                                <table class="table table-striped table-bordered nowrap" style="width: 100%">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th style="padding: 10px">Status</th>
                                                            <th style="padding: 10px">Status Date</th>
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


    <script language="javascript" type="text/javascript">
        function validaterev() {
            if (document.getElementById("<%=ddlExaminerType.ClientID%>").value == '0') {
                alert("Please Select Examiner Type");
                document.getElementById("<%=ddlExaminerType.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=ddlStatus.ClientID%>").value == '0') {
                alert("Please Select Status");
                document.getElementById("<%=ddlStatus.ClientID%>").focus();
                return false;
            }

            var idtxtweb = $("[id$=txtRemark]").attr("id");
            var txtweb = document.getElementById(idtxtweb);
            if (txtweb.value.length == 0) {
                alert('Please Enter Remark', 'Warning!');
                //$(txtweb).css('border-color', 'red');
                $(txtweb).focus();
                return false;
            }
        }
    </script>

</asp:Content>

