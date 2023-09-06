<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Receipt_Details.aspx.cs" Inherits="ACADEMIC_Receipt_Details" MasterPageFile="~/SiteMasterPage.master" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updLists"
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
    
    <asp:UpdatePanel ID="updLists" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Select Receipt Type </label>
                                        </div>
                                        <asp:DropDownList ID="ddlReceipt" runat="server" AppendDataBoundItems="True" ToolTip="Please Select Receipt Type." CssClass="form-control" data-select2-enable="true" OnTextChanged="ddlReceipt_TextChanged" AutoPostBack="true">
                                            <asp:ListItem Text="Please Select" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                        <%--<asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session." InitialValue="0" SetFocusOnError="true"
                                             ValidationGroup="submit">
                                        </asp:RequiredFieldValidator>--%>
                                    </div>
                                </div>
                            </div>

                            <div id="divListView" runat="server" class="col-12" visible="true">
                                <asp:ListView ID="lvStudList" runat="server">
                                    <LayoutTemplate>
                                        <div class="sub-heading">
                                            <h5>Student List</h5>
                                        </div>
                                        <div id="listViewGrid">
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                <thead>
                                                    <tr>
                                                        <th>Sr. No.
                                                        </th>
                                                        <th>Registration No.
                                                        </th>
                                                        <th>Student Name
                                                        </th>
                                                        <th>Programme/Branch
                                                        </th>
                                                        <th>Receipt Paid Count
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
                                                <%#: Container.DataItemIndex + 1 %>
                                            </td>
                                            <td>
                                                <%#Eval("ENROLLNMENTNO") %>
                                            </td>
                                            <td>
                                                <%#Eval("NAME") %>
                                            </td>
                                            <td>
                                                <%#Eval("BRANCH") %>
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="btnReceiptInfo" runat="server" OnClick="btnReceiptInfo_Click" CommandArgument='<%#Eval("IDNO") %>' ToolTip='<%#Eval("IDNO")%>' Text='<%#Eval("RECEIPT_PAID_COUNT") %>'
                                                    CausesValidation="false">
                                                    <asp:Label ID="lblReceipt" runat="server"></asp:Label>
                                                </asp:LinkButton>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>



    <!-- The Modal -->
    <div class="modal fade" id="myModal1">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">

                <!-- Modal Header -->
                <div class="modal-header">
                    <h4 class="modal-title">Receipt Info</h4>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>

                <!-- Modal body -->
                <div class="modal-body">
                    <div>
                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updPopUP"
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
                    <asp:UpdatePanel ID="updPopUP" runat="server">
                        <ContentTemplate>
                            <div class="col-12">
                                <asp:Panel ID="pnlPopUp" runat="server">
                                    <asp:ListView ID="lvReceipt" runat="server" align="Center">
                                        <LayoutTemplate>
                                            <div class="table-responsive">
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="tblHead">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>Sr.No.</th>
                                                            <th>Semester
                                                            </th>
                                                            <th>Receipt No.
                                                            </th>
                                                            <th>Receipt Date
                                                            </th>
                                                            <th>Applied Amount
                                                            </th>
                                                            <th>Paid Amount
                                                            </th>
                                                            <th>Balance Amount
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
                                                <td><%# Container.DataItemIndex +1 %></td>
                                                <td>
                                                    <%#Eval("Semster") %>
                                                </td>
                                                <td>
                                                    <%#Eval("REC_NO") %>
                                                </td>
                                                <td>
                                                    <%#Eval("REC_DATE") %><%--<asp:Label ID="lblReceiptDate" runat="server" Text='<%# (Eval("REC_DT").ToString() != string.Empty) ? ((DateTime)Eval("REC_DT")).ToShortDateString() : Eval("REC_DT") %>'></asp:Label>--%>
                                                    
                                                </td>
                                                <td>
                                                    <%#Eval("APPLIED_AMT") %>
                                                    <%--<asp:Label ID="lblAppliedAmount" runat="server" Text='<%#Eval("APPLIED_AMOUNT") %>'></asp:Label>--%>
                                                </td>
                                                <td>
                                                    <%#Eval("PAID_AMT") %>
                                                </td>
                                                <td>
                                                    <%#Eval("BAL_AMT") %>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>


    <script type="text/javascript">
        function showModal() {
            $("#myModal1").modal('show');
        }
    </script>
    <%--<script>
    $(document).ready(function () {
        bindDataTable();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(bindDataTable);
    });

    function bindDataTable() {
        var myDT = $('#tblstudDetails').DataTable({
            'pageLength': 50,
            'lengthMenu':[50,100,200,500]
        });
    }
    </script>--%>
</asp:Content>
