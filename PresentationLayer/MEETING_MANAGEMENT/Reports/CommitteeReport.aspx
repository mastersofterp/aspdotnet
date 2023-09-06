<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="CommitteeReport.aspx.cs" Inherits="MEETING_MANAGEMENT_Reports_CommitteeReport"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../../JAVASCRIPTS/JScriptAdmin_Module.js" type="text/javascript"></script>
    <asp:UpdatePanel ID="updActivity" runat="server">
        <ContentTemplate>
            <div class="col-md-12">
                <div class="box box-primary">
                    <div id="div1" runat="server"></div>
                    <div class="box-header with-border">
                        <h3 class="box-title">COMMITTEE REPORTS</h3>
                    </div>
                    <div>
                        <form role="form">
                            <div class="box-body">
                                <div class="col-md-12">
                                    Note <b>:</b> <span style="color: #FF0000">* Marked Is Mandatory !</span>
                                    <asp:Panel ID="pnlDesig" runat="server">
                                        <div class="panel panel-info">
                                           <%-- <div class="panel panel-heading">Meeting Reports</div>--%>
                                            <div class="sub-heading">
                                                        <h5>Meeting Reports</h5>
                                                    </div>
                                            <div class="panel panel-body">
                                                <div class="col-md-6">
                                                    <div class="form-horizontal">
                                                        <div class="form-group">
                                                            <div class="col-sm-4">
                                                                <asp:Label ID="lblSelectCommittee" CssClass="control-label" runat="server" Text="Select Committee :"> </asp:Label>
                                                                <span style="color: #FF0000; font-weight: bold">*</span>
                                                            </div>
                                                            <div class="col-sm-8">
                                                                <asp:DropDownList ID="ddlCommittee" runat="server" CssClass="form-control" AppendDataBoundItems="true" TabIndex="1" Width="350px" AutoPostBack="true" OnSelectedIndexChanged="ddlCommittee_SelectedIndexChanged">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                             <%--      //Modified by Saahil Trivedi 15-01-2022--%>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please Select Commitee Details" ValidationGroup="Submit" InitialValue="0" ControlToValidate="ddlCommittee" Display="None"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                        <div class="form-group" id="trMeeting" runat="server" visible="False">
                                                            <div class="col-sm-4">
                                                                <asp:Label ID="lblSelectMeeting" CssClass="control-label" runat="server" Text="Select Meeting :"></asp:Label>

                                                            </div>
                                                            <div class="col-sm-8">
                                                                <asp:DropDownList ID="ddlMeeting" runat="server" AppendDataBoundItems="true"  TabIndex="2" CssClass="form-control"
                                                                    Width="350px">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Select Meeting Details" ControlToValidate="ddlMeeting" InitialValue="0"  ValidationGroup="Submit" Display="None"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                   </asp:Panel>
                                </div>                                
                            </div>                   
                    </form>
                </div>

                <div class="box-footer">
                    <div class="col-md-12">
                        <p class="text-center">
                            <asp:Button ID="btnCLReport" runat="server" CssClass="btn btn-info" TabIndex="3" Text="Committee List" OnClick="btnCLReport_Click" Visible="false" />
                            &nbsp;&nbsp;<asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Submit" />
                            <p class="text-center">
                                &nbsp;<asp:Button ID="btnCMLReport0" runat="server" CssClass="btn btn-info" OnClick="btnCMLReport_Click" TabIndex="4" Text="Committee Member List" ValidationGroup="Submit" />
                                <asp:Button ID="btnMeetingReport" runat="server" CssClass="btn btn-info" OnClick="btnMeetingReport_Click" TabIndex="5" Text="Meeting Report" Visible="false" />
                                <asp:Button ID="btnAllMember" runat="server" CssClass="btn btn-info" OnClick="btnAllMember_Click" TabIndex="6" Text="All Members List" Visible="false" />
                                &nbsp;&nbsp;<asp:Button ID="btnMeetingDetails" runat="server" CssClass="btn btn-info" OnClick="btnMeetingDetails_Click" TabIndex="7" Text="Minutes Of Meeting" ValidationGroup="Submit" />
                                &nbsp;&nbsp;<asp:Button ID="btnCancel" runat="server" CssClass="btn btn-warning" OnClick="btnCancel_Click" TabIndex="8" Text="Cancel" Width="80px" />
                            </p>
                            <p>
                            </p>
                        </p>
                    </div>
                </div>
                 </div>
        </ContentTemplate>
    </asp:UpdatePanel>


    <div id="divMsg" runat="server">
    </div>
</asp:Content>
