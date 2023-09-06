<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Approval_PO.aspx.cs" Inherits="STORES_Transactions_Quotation_Approval_PO" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div2" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">PO APPROVAL</h3>
                </div>
                <div class="box-body">
                    <div class="col-12 mb-4">
                        <div id="pnlQuotation" runat="server">
                            <div class="sub-heading">
                                <h5>Purchase Detail</h5>
                            </div>
                            <asp:Panel ID="pnlQuotationDiv" runat="server">
                                <table class="table table-striped table-bordered nowrap" style="width: 100%" id="">
                                    <asp:Repeater ID="rptPODetail" runat="server">
                                        <HeaderTemplate>

                                            <thead class="bg-light-blue">
                                                <tr>

                                                    <th>
                                                        <asp:CheckBox ID="chkQuotation" onClick="selectAll(this)" runat="server" />
                                                    </th>
                                                    <th style="display: none">Send Date </th>
                                                    <th>Purchase No. </th>
                                                    <th>Purchase Amount </th>
                                                    <th>Approval Status </th>
                                                    <th>Passing Path </th>
                                                    <th id="det" runat="server" visible="false">Detail </th>
                                                    <th>Remark </th>
                                                </tr>

                                            </thead>
                                            <tbody>
                                        </HeaderTemplate>

                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:CheckBox ID="chkQuotation" runat="server" />
                                                </td>
                                                <td style="display: none"><%# Eval("SDATE", "{0:dd-MMM-yyyy}")%></td>
                                                <td><%# Eval("REFNO")%>
                                                    <asp:HiddenField ID="hdPNO" runat="server" Value='<%# Eval("PNO")%>' />
                                                </td>
                                                <td><%# Eval("TotAmount")%></td>
                                                <td>
                                                    <asp:Label ID="lblAppStatus" runat="server" Text='<%# Eval("APP_Status")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <%-- <asp:ImageButton ID="btnPPath" runat="server" CssClass="btn btn-primary" CommandArgument='<%# Eval("PNO")%>' AlternateText="Status Detail" 
                                                                OnClick="btnPPath_OnClick" />--%>
                                                    <asp:LinkButton ID="btnPPath" runat="server" CommandArgument='<%# Eval("PNO")%>' Text="Status Detail" Font-Bold="true"
                                                        Font-Underline="true" OnClick="btnPPath_Click" />
                                                </td>

                                                <td id="tddet" runat="server" visible="false">
                                                    <%-- <asp:ImageButton ID="btnDetail" runat="server" CssClass="btn btn-primary"  CommandArgument='<%# Eval("PNO")%>' AlternateText="Detail"
                                                                OnClick="btnDetail_OnClick" />--%>
                                                    <asp:LinkButton ID="btnDetail" runat="server" CommandArgument='<%# Eval("PNO")%>' Text="Detail" Font-Underline="true" Font-Bold="true"
                                                        OnClick="btnDetail_Click" />
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtRemarks" runat="server" Text='<%# Eval("Remark") %>' CssClass="form-control"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </tbody>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </table>

                            </asp:Panel>
                        </div>
                    </div>

                    <div id="pnlPOReport" class="col-12" runat="server" visible="false">
                        <div class="sub-heading">
                            <h5>Quotation Detail Report</h5>
                        </div>

                        <asp:Panel ID="pnlPOReportDiv" runat="server">
                            <div class="col-12">
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Purchase Order</label>
                                    </div>
                                    <asp:DropDownList ID="ddlAppPO" runat="server" CssClass="form-control" AppendDataBoundItems="True">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnReport" runat="server" Text="Report" CssClass="btn btn-info" OnClick="btnReport_Click" />
                                    <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btn btn-primary" OnClick="btnBack_Click" />
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                    <div id="pnlItemDetail" runat="server" visible="false" class="col-md-12">
                        <%--  <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                    <ContentTemplate>--%>
                        <ajaxToolKit:ModalPopupExtender ID="MdlApproval" runat="server" PopupControlID="pnlApproval" TargetControlID="lblApproval"
                            BackgroundCssClass="modalBackground" BehaviorID="mdlPopupDel" CancelControlID="ImgApproval">
                        </ajaxToolKit:ModalPopupExtender>
                        <asp:Label ID="lblApproval" runat="server"></asp:Label>
                        <%--</ContentTemplate>
                                </asp:UpdatePanel>--%>

                        <asp:Panel ID="pnlApproval" runat="server" CssClass="PopupReg" Style="display: none; height: auto; width: 40%; background: #fff; z-index: 101!important; box-shadow: rgba(0, 0, 0, 0.16) 0px 10px 36px 0px, rgba(0, 0, 0, 0.06) 0px 0px 0px 1px;">
                            <div class="col-12 mt-4 mb-5">
                                <div class="sub-heading">
                                    <h5>Passing Path Details</h5>
                                    <div class="box-tools pull-right">
                                        <asp:ImageButton ID="ImgApproval" runat="server" ImageUrl="~/IMAGES/delete.png" ToolTip="Close" />
                                    </div>
                                </div>

                                <asp:ListView ID="lvitemPurchase" runat="server">
                                    <LayoutTemplate>
                                        <div id="lgv1">
                                            <h4 class="box-title"><%--Passing Path Details--%>
                                            </h4>
                                            <table class="table table-striped table-bordered nowrap" style="width: 100%" id="">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>PO No.
                                                        </th>
                                                        <th>Approval Authority//User Name
                                                        </th>
                                                        <th>Status
                                                        </th>
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
                                                <%# Eval("REFNO")%>
                                            </td>
                                            <td>
                                                <%# Eval("PANAMe")%>
                                            </td>
                                            <td>
                                                <%# Eval("APP_Status")%>
                                            </td>

                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>

                            </div>
                        </asp:Panel>

                    </div>

                    <div id="pnlAdd" runat="server" class="col-12">
                        <div class="sub-heading">
                            <h5>Quotation Detail Report</h5>

                        </div>

                        <asp:Panel ID="DivpnlAdd" runat="server">
                            <%-- <asp:UpdatePanel ID="updatepanel" runat="server">
                                            <ContentTemplate>--%>
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Select </label>
                                        </div>
                                        <asp:DropDownList ID="ddlSelect" runat="server" data-select2-enable="true" CssClass="form-control" Width="200px"
                                            AppendDataBoundItems="true">
                                            <asp:ListItem Value="A">Approve</asp:ListItem>
                                            <%-- <asp:ListItem Value="F">Approved and Forward</asp:ListItem>--%>
                                            <asp:ListItem Value="R">Reject</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">

                                <asp:Button ID="btnSave" runat="server" Text="Submit" ValidationGroup="Leaveapp" CssClass="btn btn-primary" OnClick="btnSave_Click" />

                                <asp:Button ID="btnReportPanel" runat="server" Visible="false" Text="Report" CssClass="btn btn-info" OnClick="btnReportPanel_Click" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" CssClass="btn btn-warning" OnClick="btnCancel_Click" />

                            </div>
                            <%--  </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="btnSave" />
                                                <asp:PostBackTrigger ControlID="btnReportPanel" />                                              
                                            </Triggers>
                                        </asp:UpdatePanel>--%>
                        </asp:Panel>

                    </div>
                </div>
            </div>

        </div>
    </div>


    <script type="text/javascript">
        function selectAll(invoker) {
            var inputElements = document.getElementsByTagName('input');
            for (var i = 0; i < inputElements.length; i++) {
                var myElement = inputElements[i];
                if (myElement.type === "checkbox") {
                    myElement.checked = invoker.checked;
                }
            }
        }
    </script>



    <%--   <script type="text/javascript">
        RunThisAfterEachAsyncPostback();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
    </script>

    <script src="../../../JAVASCRIPTS/jquery.min_1.js" language="javascript" type="text/javascript"></script>
    <script src="../../../JAVASCRIPTS/jquery-ui.min_1.js" language="javascript" type="text/javascript"></script>
    <script src="../../../JAVASCRIPTS/AutoComplete.js" language="javascript" type="text/javascript"></script>
    <script src="../../../jquery/jquery-1.10.2.js" type="text/javascript"></script>--%>

    <script type="text/javascript">

        function showpopup() {

            var r = confirm("Are you sure to delete this item");
            if (r == true) {

                return true;
            } else {

                return false;
            }

        }
    </script>
    <div id="divMsg" runat="server">
    </div>
</asp:Content>

