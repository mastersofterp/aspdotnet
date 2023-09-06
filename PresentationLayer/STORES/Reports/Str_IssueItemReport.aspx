<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Str_IssueItemReport.aspx.cs" Inherits="STORES_Reports_Str_IssueItemReport"
    Title="" %>

<%@ Register Assembly="RControl" Namespace="RControl" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript">
        function update(obj) {

            try {
                var mvar = obj.split('¤');
                document.getElementById(mvar[1]).value = mvar[0];
                document.getElementById('ctl00_ctp_hdnId').value = mvar[0] + "  ";
                setTimeout('__doPostBack(\'' + mvar[1] + '\',\'\')', 0);
                //document.forms.submit;
            }
            catch (e) {
                alert(e);
            }
        }
    </script>
    <%--<script type="text/javascript">
        function chk(sender, args) {
            var T_date = sender.get_selectedDate();
            var date = $find("_fromdate");
            var F_date = date.get_selectedDate();
            if (T_date < F_date) {
                alert("End Date Should be greater than or equal to  From Date");
                document.getElementById('<%=txtToDate.ClientID%>').value = "";
            }

        }
    </script>--%>

    <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="updpanel">
        <ProgressTemplate>
            <div>
                <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updpanel"
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
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="updpanel" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">ISSUED ITEM REPORT</h3>

                        </div>
                        <div class="box-body">
                            <asp:Panel ID="pnlDSRDetails" runat="server" Visible="True">
                                <div class="col-12">
                                    <div class="row">
                                        <%--<div class="sub-heading">
                                            <h5>ASSET ISSUE REPORT</h5>
                                        </div>--%>

                                        <asp:Panel ID="Panel_Error" runat="server" CssClass="Panel_Error" EnableViewState="false"
                                            Visible="false">
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <td>
                                                            <img src="../../../Images/error.png" align="absmiddle" alt="Error" />
                                                        </td>
                                                        <td>
                                                            <font style="font-family: Verdana; font-family: 11px; font-weight: bold; color: #CD0A0A">
                                                               </font>
                                                            <asp:Label ID="Label_ErrorMessage" runat="server" Style="font-family: Verdana; font-size: 11px; color: #CD0A0A"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </thead>
                                            </table>
                                        </asp:Panel>
                                        <asp:Panel ID="Panel_Confirm" runat="server" CssClass="Panel_Confirm" EnableViewState="false"
                                            Visible="false">
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="Table1">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <td style="vertical-align: top">
                                                            <img src="../../../Images/confirm.png" align="absmiddle" alt="confirm" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label_ConfirmMessage" runat="server" Style="font-family: Verdana; font-size: 11px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </thead>
                                            </table>
                                        </asp:Panel>
                                        <div class="form-group col-12">
                                            <div class="form-group col-md-6 col-12">

                                                <asp:RadioButton ID="rdbRequisition" runat="server" TabIndex="4" AutoPostBack="True" GroupName="rdbIssue"
                                                    OnCheckedChanged="rdbRequisition_CheckedChanged" Text="Requisition Wise" Checked="true" />
                                                <asp:RadioButton ID="rdbDirectIssue" TabIndex="5" runat="server" AutoPostBack="True" GroupName="rdbIssue"
                                                    OnCheckedChanged="rdbDirectIssue_CheckedChanged" Text="Issue Slip No. Wise" />
                                                <asp:RadioButton ID="rdbIndentSlip" TabIndex="5" runat="server" AutoPostBack="True" GroupName="rdbIssue"
                                                    OnCheckedChanged="rdbIndentSlip_CheckedChanged" Text="Indent Slip Report" />
                                                <%--<asp:HiddenField ID="hdnRowCount" runat="server"></asp:HiddenField>--%>
                                            </div>
                                        </div>
                                        <div class="form-group col-12">
                                            <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divIssue" visible="false">
                                                <label><span style="color: red">*</span>Issue Slip No. :</label>
                                                <asp:DropDownList ID="ddlIssueNo" runat="server" AppendDataBoundItems="true" ToolTip="Select Issue No" CssClass="form-control" TabIndex="3">
                                                    <asp:ListItem Selected="True" Value="0" Text="--Please Select--"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divReq" visible="true">

                                                <label><span style="color: red">*</span>Requisition No. :</label>

                                                <asp:DropDownList ID="ddlRequisitionNo" runat="server" AppendDataBoundItems="true" ToolTip="Select Requisition No." CssClass="form-control" TabIndex="3">
                                                    <asp:ListItem Selected="True" Value="0" Text="--Please Select--"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-12 btn-footer">
                                            <asp:Button ID="btnRpt" runat="server" Text="Show Report" OnClick="btnRpt_Click"
                                                ToolTip="Click To Show Report" CssClass="btn btn-info" TabIndex="4" ValidationGroup="Store" />
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                                                ToolTip="Click To Reset" CssClass="btn btn-warning" TabIndex="5" />
                                            <asp:ValidationSummary ID="vsstore" runat="server" DisplayMode="List" ShowSummary="false" ShowMessageBox="true" ValidationGroup="Store" />
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>

                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
