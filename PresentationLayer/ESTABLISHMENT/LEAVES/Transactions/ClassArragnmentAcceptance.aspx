<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ClassArragnmentAcceptance.aspx.cs"
    Inherits="ESTABLISHMENT_LEAVES_Transactions_ClassArragnmentAcceptance" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">Class Arrangement Acceptance</h3>
                </div>
                <div class="container-fluid">
                    <div id="pnlAdd" runat="server" visible="false">
                        <div class="form-group col-md-12">
                            <div class="panel panel-info">
                                <div class="sub-heading">
                                    <h5>Class Arrangement Acceptance</h5>
                                </div>
                                <div class="panel-body">
                                    <div class="col-12">
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="form-group">
                                                    <label>Applicant Name :</label>
                                                    <asp:TextBox ID="lblEmpName" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12" id="divSubject" runat="server" visible="false">
                                                <div class="form-group">
                                                    <label>SUBJECT :</label>
                                                    <asp:TextBox ID="lblsubject" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="form-group">
                                                    <label>Date :</label>
                                                    <asp:TextBox ID="lblDate" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="form-group">
                                                    <label>TIME :</label>
                                                    <asp:TextBox ID="lblPeriod" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12" id="divClass" runat="server" visible="false">
                                                <div class="form-group">
                                                    <label>CLASS :</label>
                                                    <asp:TextBox ID="lblClass" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12" id="divCourse" runat="server" visible="false">
                                                <div class="form-group">
                                                    <label>COURSE NAME :</label>
                                                    <asp:TextBox ID="lblCourse" runat="server" CssClass="form-control" TextMode="MultiLine" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>

                                            <%--<div class="row">--%>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="form-group">
                                                    <sup>* </sup>
                                                    <label>Select :</label>
                                                    <asp:DropDownList ID="ddlSelect" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        <asp:ListItem Value="A">Accept</asp:ListItem>
                                                        <asp:ListItem Value="R">Reject</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvSelect" runat="server" InitialValue="0"
                                                        ControlToValidate="ddlSelect" Display="None" ErrorMessage="Please Select Accept/Reject"
                                                        SetFocusOnError="true" ValidationGroup="Leaveapp"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="form-group">
                                                    <label>Remarks :</label>
                                                    <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" CssClass="form-control" />
                                                </div>
                                            </div>
                                        </div>
                                        <%-- </div>--%>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <p class="text-center">
                                                        <asp:Button ID="btnSave" runat="server" Text="Submit" ValidationGroup="Leaveapp"
                                                            CssClass="btn btn-primary" ToolTip="Click to Save" OnClick="btnSave_Click" />
                                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                                                            CssClass="btn btn-warning" ToolTip="Click to Cancel" OnClick="btnCancel_Click" />
                                                        <asp:Button ID="btnBack" runat="server" Text="Back" CausesValidation="false" CssClass="btn btn-primary"
                                                            ToolTip="Click to Go Back" OnClick="btnBack_Click" />
                                                        <asp:ValidationSummary ID="validationsummaryAdd" runat="server" ValidationGroup="Leaveapp"
                                                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                                    </p>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>


                                    <asp:Panel ID="pnllistPending" runat="server">
                                        <div class="col-12">
                                            <asp:ListView ID="lvPendingList" runat="server">
                                                <EmptyDataTemplate>
                                                    <asp:Label ID="lblErr" runat="server" Text="" CssClass="d-block text-center mt-3">
                                                    </asp:Label>
                                                </EmptyDataTemplate>
                                                <LayoutTemplate>
                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th>Sr.No.
                                                                </th>
                                                                <th>TIME
                                                                </th>
                                                                <%--<th>SUBJECT
                                                            </th>--%>
                                                                <th>Date
                                                                </th>
                                                                <th>Faculty Name
                                                                </th>
                                                                <th>Applicant Name
                                                                </th>
                                                                <th>Status
                                                                </th>
                                                                <th>Accept/Reject
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
                                                            <%# Eval("SEQNO")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("TIME")%>                                                
                                                        </td>
                                                        <%--<td>
                                                        <%# Eval("SUBJECT")%>
                                                    </td>--%>
                                                        <td>
                                                            <%# Eval("DATE", "{0:dd/MM/yyyy}")%>
                                                        </td>
                                                        <td>
                                                            <%--<%# Eval("FACULTY_NAME")%>--%>
                                                            <%# Eval("CHARGENAME")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("APPLICANTNAME") %>
                                                        </td>
                                                        <td>
                                                            <%# Eval("STATUS") %>
                                                        </td>
                                                        <td>
                                                            <asp:Button ID="btnApproval" runat="server" Text="Approve/Reject" CommandArgument='<%# Eval("SRNO")%>'
                                                                ToolTip="Click to Approve"  CssClass="btn btn-primary" OnClick="btnApproval_Click" />
                                                            <%--CommandArgument='<%# Eval("CHTRNO")%>'--%>
                                                        </td>
                                                        <%-- <td>
                                                <asp:Button ID="btnReject" runat="server" Text="Reject" CommandArgument='<%# Eval("CHTRNO")%>'
                                                    ToolTip="Click to Reject" OnClick="btnReject_Click" />
                                            </td>--%>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                            <div id="DivNotePendingList" runat="server" visible="false">
                                                <div class="form-group col-sm-12">
                                                    <div class="text-center">
                                                        <p style="color: Red; font-weight: bold">
                                                            Pending List of Charge Acceptance..!!                                                                
                                                        </p>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                    <%-- <div id="DivStatusButton" runat="server">
                        <div class="form-group col-md-12">
                            <asp:LinkButton ID="lnkViewStatus" runat="server" Text="Hide Accepted Status" OnClick="lnkViewStatus_Click"></asp:LinkButton>
                        </div>
                    </div>--%>
                                    <div id="pnlListAccepted" runat="server">
                                        <div class="form-group col-md-12">
                                            <div class="panel panel-info">
                                                <div class="sub-heading">
                                                    <h5>Class Arrangement Accepted/Rejected List</h5>
                                                </div>
                                                <div class="panel-body" style="padding: 0">
                                                    <asp:Repeater ID="lvAcceptedList" runat="server">
                                                        <HeaderTemplate>
                                                            <div class="table-overflow">
                                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                                    <thead class="bg-light-blue">
                                                                        <tr>
                                                                            <th>Sr.No.
                                                                            </th>
                                                                            <th>TIME
                                                                            </th>
                                                                            <%--<th>SUBJECT
                                                            </th>--%>
                                                                            <th>Date
                                                                            </th>
                                                                            <th>Faculty Name
                                                                            </th>
                                                                            <th>Applicant Name
                                                                            </th>
                                                                            <th>Status
                                                                            </th>
                                                                        </tr>
                                                                        <%--<tr id="Tr2" runat="server" />--%>
                                                                    <thead>
                                                                    <tbody>
                                                            </div>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <tr class="item">
                                                                <td>
                                                                    <%# Eval("SEQNO")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("TIME")%>                                                
                                                                </td>
                                                                <%--<td>
                                                    <%# Eval("SUBJECT")%> 
                                                </td>--%>
                                                                <td>
                                                                    <%# Eval("DATE", "{0:dd/MM/yyyy}")%>
                                                                </td>
                                                                <td>
                                                                    <%--<%# Eval("FACULTY_NAME")%>--%>
                                                                    <%# Eval("CHARGENAME") %>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("APPLICANTNAME") %>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("STATUS") %>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            </tbody></table>
                                                        </FooterTemplate>
                                                    </asp:Repeater>

                                                    <div id="DivStatusNote" runat="server" visible="false">
                                                        <div class="form-group col-sm-12">
                                                            <div class="text-center">
                                                                <p style="color: Red; font-weight: bold">
                                                                    Charge Acceptance Status                                                               
                                                                </p>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <%-- <div id="DivLeaveApprovalButton">
                            <div class="form-group col-sm-12">
                                <asp:Button ID="lnkbut" runat="server" Text="Leave Approval Status" ToolTip="Leave Approval Status"
                                    OnClick="lnkbut_Click" TabIndex="1" class="btn btn-primary" />
                            </div>
                        </div>--%>
                </div>
            </div>
        </div>
    </div>


    <%--<div class="row">
        <div class="col-md-12">
            <div class="box-header with-border">
                 <h3 class="box-title">Class Arrangement Acceptance</h3>
                </div>
            <div>
                <div class="box-body">
                    <div class="col-md-12">
                        <asp:Panel ID="pnllist" runat="server">
                            <div class="panel panel-info">
                                <div class="panel panel-heading">Pending List of Class Arrangement Acceptance</div>
                                <asp:ListView ID="lvLclass" runat="server">
                                    <LayoutTemplate>
                                        <table class="table table-bordered table-hover">
                                            <thead>
                                                <tr class="bg-light-blue">
                                                    <th>
                                                        PERIOD NO
                                                    </th>
                                                    <th>
                                                        SUBJECT
                                                    </th>
                                                    <th>
                                                        Date
                                                    </th>
                                                    <th>
                                                        Faculty Name
                                                    </th>
                                                    <th>
                                                        Approval
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
                                                <%# Eval("PERIODNO") %>
                                            </td>
                                            <td>
                                                <%# Eval("SUBJECT") %>
                                            </td>
                                            <td>
                                                <%# Eval("DATE") %>
                                            </td>
                                            <td>
                                                <%# Eval("APPLICANTNAME") %>
                                            </td>
                                            <td>
                                                <asp:Button ID="btnApproval" runat="server" CssClass="btn btn-primary" OnClick="btnApproval_Click" />
                                            </td>
                                        </tr>
                                        </ItemTemplate>   
                                </asp:ListView>
                                </div>
                            </asp:Panel>
                        <div class="form-group col-md-12">
                            <asp:Panel ID="pnladd" runat="server" Visible="false">
                                <div class="form-group col-md-12">
                                    <div class="form-group col-md-12">
                                     <div class="form-group col-md-4">                                    
                                    <label>Applicant Name</label>                                    
                                    </div>
                                        <div class="form-group col-md-8" style="text-align:left">
                                             <asp:Label ID="lblEmpName" runat="server" TabIndex="1" ToolTip="Applicant Name"></asp:Label>
                                            </div>
                                    </div>
                                    <div class="form-group col-md-12">
                                        <div class="form-group col-md-4">
                                            <label>SUBJECT</label>
                                            </div>
                                        <div class="form-group col-md-8" style="text-align:left">
                                            <asp:Label ID="lblsubject" runat="server" TabIndex="2" ToolTip="Subject Name"></asp:Label>
                                        </div>
                                        </div>
                                    <div class="form-group col-md-12">
                                        <div class="form-group col-md-4">
                                            <label>Date</label>
                                            </div>
                                        <div class="form-group col-md-8" style="text-align:left">
                                             <asp:Label ID="lblDate" runat="server" TabIndex="3" ToolTip="Date"></asp:Label>
                                            </div>
                                        </div>
                                    <div class="form-group col-md-12">
                                        <div class="form-group col-md-4">
                                            <label>PERIOD NUMBER</label>
                                            </div>
                                        <div class="form-group col-md-8" style="text-align:left">
                                            <asp:Label ID="Periodnumber" runat="server" TabIndex="4" ToolTip="Period number"></asp:Label>
                                            </div>
                                        </div>
                                    <asp:Panel ID="pnlButton" runat="server" Visible="false">
                                     <div class="form-group col-md-12">
                                        <p class="text-center">
                                            <asp:Button ID="btnSave" runat="server" Text="Submit" ValidationGroup="Leaveapp" TabIndex="30"
                                                CssClass="btn btn-primary" ToolTip="Click here To Submit" OnClick="btnSave_Click" />
                                            &nbsp;
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" TabIndex="31"
                                            CssClass="btn btn-warning" ToolTip="Click here to Reset" OnClick="btnCancel_Click" />
                                            <asp:Button ID="btnBack" runat="server" Text="Back" CausesValidation="false" CssClass="btn btn-primary"
                                                OnClick="btnBack_Click" ToolTip="Click here to Go Back" TabIndex="32" />
                                        </p>
                                    </div>
                                </asp:Panel>


                                  </div>
                            </asp:Panel>
                            </div>
                        </div>
                    </div>
            </div>
            </div>
        </div>--%>
                                    <%-- <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <%--<td class="vista_page_title_bar" valign="top" style="height: 30px">Leave Approval &nbsp;               
                <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                    AlternateText="Page Help" ToolTip="Page Help" />
            </td>--%>

                                    <tr>
                                        <td>
                                            <!-- "Wire frame" div used to transition from the button to the info panel -->
                                            <div id="flyout" style="display: none; overflow: hidden; z-index: 2; background-color: #FFFFFF; border: solid 1px #D0D0D0;">
                                            </div>
                                            <!-- Info panel to be displayed as a flyout when the button is clicked -->
                                            <div id="info" style="display: none; width: 250px; z-index: 2; opacity: 0; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=0); font-size: 12px; border: solid 1px #CCCCCC; background-color: #FFFFFF; padding: 5px;">
                                                <div id="btnCloseParent" style="float: right; opacity: 0; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=0);">
                                                    <asp:LinkButton ID="btnClose" runat="server" OnClientClick="return false;" Text="X"
                                                        ToolTip="Close" Style="background-color: #666666; color: #FFFFFF; text-align: center; font-weight: bold; text-decoration: none; border: outset thin #FFFFFF; padding: 5px;" />
                                                </div>
                                                <div>
                                                    <p class="page_help_head">
                                                        <span style="font-weight: bold; text-decoration: underline;">Page Help</span><br />
                                                        <asp:Image ID="imgSReason" runat="server" ImageUrl="~/Images/action_down.png" AlternateText="Show Reason" />
                                                        Show Reason
                            <asp:Image ID="imgHReason" runat="server" ImageUrl="~/Images/action_up.png" AlternateText="Hide Reason" />
                                                        Hide Reason
                                                    </p>
                                                    <p class="page_help_text">
                                                        <asp:Label ID="lblHelp" runat="server" Font-Names="Trebuchet MS" />
                                                    </p>
                                                </div>
                                            </div>

                                            <script type="text/javascript" language="javascript">
                                                // Move an element directly on top of another element (and optionally
                                                // make it the same size)
                                                function Cover(bottom, top, ignoreSize) {
                                                    var location = Sys.UI.DomElement.getLocation(bottom);
                                                    top.style.position = 'absolute';
                                                    top.style.top = location.y + 'px';
                                                    top.style.left = location.x + 'px';
                                                    if (!ignoreSize) {
                                                        top.style.height = bottom.offsetHeight + 'px';
                                                        top.style.width = bottom.offsetWidth + 'px';
                                                    }
                                                }
                                            </script>
                                        </td>
                                    </tr>
</asp:Content>





