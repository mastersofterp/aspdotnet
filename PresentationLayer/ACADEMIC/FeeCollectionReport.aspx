<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="FeeCollectionReport.aspx.cs" Inherits="ACADEMIC_FeeCollectionReport" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--===== Data Table Script added by gaurav =====--%>
    <script>
        $(document).ready(function () {
            var table = $('#tblStudLedger').DataTable({
                responsive: true,
                lengthChange: true,
                scrollY: 320,
                scrollX: true,
                scrollCollapse: true,
                paging: false, // Added by Gaurav for Hide pagination

                dom: 'lBfrtip',
                buttons: [
                    {
                        extend: 'colvis',
                        text: 'Column Visibility',
                        columns: function (idx, data, node) {
                            var arr = [0];
                            if (arr.indexOf(idx) !== -1) {
                                return false;
                            } else {
                                return $('#tblStudLedger').DataTable().column(idx).visible();
                            }
                        }
                    },
                    {
                        extend: 'collection',
                        text: '<i class="glyphicon glyphicon-export icon-share"></i> Export',
                        buttons: [
                                {
                                    extend: 'copyHtml5',
                                    exportOptions: {

                                    }
                                },
                                {
                                    extend: 'excelHtml5',
                                    exportOptions: {

                                    }
                                },
                                {
                                    extend: 'pdfHtml5',
                                    exportOptions: {

                                    }
                                },
                        ]
                    }
                ],
                "bDestroy": true,
            });
        });
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                var table = $('#tblStudLedger').DataTable({
                    responsive: true,
                    lengthChange: true,
                    scrollY: 320,
                    scrollX: true,
                    scrollCollapse: true,
                    paging: false, // Added by Gaurav for Hide pagination

                    dom: 'lBfrtip',
                    buttons: [
                        {
                            extend: 'colvis',
                            text: 'Column Visibility',
                            columns: function (idx, data, node) {
                                var arr = [0];
                                if (arr.indexOf(idx) !== -1) {
                                    return false;
                                } else {
                                    return $('#tblStudLedger').DataTable().column(idx).visible();
                                }
                            }
                        },
                        {
                            extend: 'collection',
                            text: '<i class="glyphicon glyphicon-export icon-share"></i> Export',
                            buttons: [
                                    {
                                        extend: 'copyHtml5',
                                        exportOptions: {

                                        }
                                    },
                                    {
                                        extend: 'excelHtml5',
                                        exportOptions: {

                                        }
                                    },
                                    {
                                        extend: 'pdfHtml5',
                                        exportOptions: {

                                        }
                                    },
                            ]
                        }
                    ],
                    "bDestroy": true,
                });
            });
        });

    </script>
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="pnlFeeTable"
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

    <asp:UpdatePanel ID="pnlFeeTable" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <%--<h3 class="box-title">Overall Fee Collection Report</h3>--%>
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Selection Criteria</label>
                                        </div>
                                        <asp:RadioButtonList ID="rblFeeCollection" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" RepeatColumns="2" CssClass="col-md-12" OnSelectedIndexChanged="rblFeeCollection_SelectedIndexChanged" AppendDataBoundItems="true">
                                            <asp:ListItem Value="1" Selected="True">Student Ledger  </asp:ListItem>
                                            <asp:ListItem Value="2">University level Collection/Outstanding </asp:ListItem>
                                            <asp:ListItem Value="3">Degree/Branch level Collection/Outstanding  </asp:ListItem>
                                            <asp:ListItem Value="4">Admission Batch Wise Collection/Outstanding</asp:ListItem>
                                            <asp:ListItem Value="5">Financial Year Wise Collection/Outstanding</asp:ListItem>
                                        </asp:RadioButtonList>
                                        <asp:RequiredFieldValidator ID="rfvscs" runat="server" ValidationGroup="report" ErrorMessage="Please select atleast one Option."
                                            ControlToValidate="rblFeeCollection" Display="None"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divRecieptType" visible="true">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Receipt Type</label>
                                        </div>
                                        <asp:DropDownList ID="ddlReceiptType" runat="server" AppendDataBoundItems="true" AutoPostBack="false" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlReceiptType_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divAdmissionBatch" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--<label>Admission Batch</label>--%>
                                            <asp:Label ID="lblDYddlAdmBatch" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlAdmbatch" runat="server" AppendDataBoundItems="true" AutoPostBack="false" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlAdmbatch_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divDegree" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--<label>Degree</label>--%>
                                            <asp:Label ID="lblDYddlDegree" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" AutoPostBack="true" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divBranch" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%-- <label>Branch</label>--%>
                                            <asp:Label ID="lblDYddlBranch" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" AutoPostBack="true" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnshow" runat="server" Text="Show" CssClass="btn btn-info" OnClick="btnshow_Click" />

                                <asp:Button ID="btnReport" runat="server" Text="Report" OnClick="btnReport_Click" CssClass="btn btn-info" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" OnClick="btnCancel_Click" />
                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List"
                                    ShowMessageBox="True" ShowSummary="False" ValidationGroup="report" />
                            </div>

                            <asp:UpdatePanel ID="updPopUP" runat="server">
                                <ContentTemplate>
                                    <div class="col-12">
                                        <div id="divListView" runat="server" visible="false">
                                            <asp:Panel ID="pnlStudentLedger" runat="server">
                                                <asp:ListView ID="lvStudList" runat="server">
                                                    <LayoutTemplate>
                                                        <div class="sub-heading">
                                                            <h5>Student Ledger Report</h5>
                                                        </div>
                                                        <table class="table table-striped table-bordered nowrap" style="width: 100%" id="tblStudLedger">
                                                            <thead class="bg-light-blue">
                                                                <tr class="">
                                                                    <th>
                                                                        <asp:Label ID="lblDYRRNo" runat="server" Font-Bold="true"></asp:Label>
                                                                    </th>
                                                                    <th>Student Name
                                                                    </th>
                                                                    <th>
                                                                        <asp:Label ID="lblDYtxtBranch" runat="server" Font-Bold="true"></asp:Label>
                                                                    </th>
                                                                    <th>Receipt Paid Count
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
                                                                    CausesValidation="false" OnClientClick="return show(); ">
                                                                    <asp:Label ID="lblReceipt" runat="server"></asp:Label>
                                                                </asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </asp:Panel>
                                        </div>
                                    </div>

                                    <div class="col-12">
                                        <asp:Panel ID="pnlAmount" runat="server" Visible="false">
                                            <asp:ListView ID="lvAmount" runat="server" OnItemDataBound="lvAmount_ItemDataBound" Visible="false">
                                                <LayoutTemplate>
                                                    <div class="sub-heading">
                                                        <h5>University Level Collection Report</h5>
                                                    </div>
                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th>Show
                                                                </th>
                                                                <th>College Name
                                                                </th>
                                                                <th>Overall Total Amount
                                                                </th>
                                                                <th></th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </tbody>
                                                    </table>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                        <tr id="MainTableRow" runat="server" class="item">
                                                            <td>
                                                                <asp:Panel ID="pnlDetails" runat="server" Style="cursor: pointer; vertical-align: top; float: left">
                                                                    <asp:Image ID="imgExp" runat="server" ImageUrl="~/Images/action_down.png" />
                                                                </asp:Panel>
                                                                <%--&nbsp;&nbsp;<asp:Label ID="lbIoNo" runat="server"
                                                                                        Visible="false"></asp:Label>--%>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblCollName" runat="server" Text='<%# Eval("COLLEGE_NAME") %>' ToolTip='<%# Eval("COLLEGE_ID") %>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblOverAllAmount" runat="server" Text='<%# Eval("TOTAL_AMT") %>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <ajaxToolKit:CollapsiblePanelExtender ID="cpeCourt2" runat="server" CollapseControlID="pnlDetails"
                                                                    Collapsed="true" CollapsedImage="~/Images/action_down.png" ExpandControlID="pnlDetails"
                                                                    ExpandedImage="~/images/action_up.png" ImageControlID="imgExp" TargetControlID="pnlShowCDetails">
                                                                </ajaxToolKit:CollapsiblePanelExtender>
                                                            </td>
                                                        </tr>
                                                       
                                                   <tr>
                                                                                    <asp:Panel ID="pnlShowCDetails" runat="server" CssClass="collapsePanel">
                                                                                                    <asp:ListView ID="lvDetails" runat="server">
                                                                                                        <LayoutTemplate>
                                                                                                            <div id="demo-grid" class="vista-grid" style="width: 99%; height: 60%">
                                                                                                                <table class="dataTable table table-bordered table-striped table-hover">
                                                                                                                    <thead>
                                                                                                                        <tr class="header bg-light-blue">
                                                                                                                            <%--<th>Sr.no
                                                                                                                                </th>--%>
                                                                                                                            <th>Degree
                                                                                                                            </th>
                                                                                                                            <th>Branch 
                                                                                                                            </th>
                                                                                                                            <th>Applied Fee
                                                                                                                            </th>
                                                                                                                            <th>Paid Fees
                                                                                                                            </th>
                                                                                                                            <th>Outstanding Fees
                                                                                                                            </th>
                                                                                                                            <th>Excess Amount
                                                                                                                            </th>
                                                                                                                            <%--<th style="width: 10%">Total Amount</th>--%>
                                                                                                                        </tr>
                                                                                                                    </thead>
                                                                                                                    <tbody>
                                                                                                                        <tr id="itemPlaceholder" runat="server">
                                                                                                                        </tr>
                                                                                                                    </tbody>
                                                                                                                </table>
                                                                                                            </div>
                                                                                                        </LayoutTemplate>
                                                                                                        <EmptyDataTemplate>
                                                                                                            <div style="text-align: center; font-family: Arial; font-size: medium">
                                                                                                                No Record Found
                                                                                                            </div>
                                                                                                        </EmptyDataTemplate>
                                                                                                        <ItemTemplate>
                                                                                                            <tr class="item">
                                                                                                                <td>
                                                                                                                    <asp:Label ID="lblDegree" runat="server" Text='<%# Eval("DEGREE") %>'></asp:Label>
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:Label ID="lblBranch" runat="server" Text='<%# Eval("BRANCH") %>'></asp:Label>
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:Label ID="lblAppliedFee" runat="server" Text='<%# Eval("APPLIED_FEE") %>'></asp:Label>
                                                                                                                    <asp:HiddenField ID="hdnLevel" runat="server" />
                                                                                                                    <asp:HiddenField ID="hdnIntakeFrom" runat="server" />
                                                                                                                    <asp:HiddenField ID="hdnIntakeTo" runat="server" />
                                                                                                                    <%--<asp:HiddenField ID="hdnSubCategory" runat="server" Value='<%# Eval("FEE_SUB_CATEGORY") %>' /> --%>
                                                                                                                    <asp:HiddenField ID="hdnDuration" runat="server" />
                                                                                                                    <asp:HiddenField ID="hdnIntake" runat="server" />
                                                                                                                    <asp:HiddenField ID="hdnAdmittedCount" runat="server" />
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:Label ID="lblPaidFee" runat="server" Text='<%# Eval("PAID_FEE") %>'></asp:Label>
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:Label ID="lblOutstanding" runat="server" Text='<%# Eval("OUTSTANDING_FEE") %>'></asp:Label>
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:Label ID="lblExcess" runat="server" Text='<%# Eval("EXCESS_AMOUNT") %>'></asp:Label>
                                                                                                                </td>
                                                                                                                <%--<td style="width: 5%">
                                                                                                                        <asp:Label ID="lblStandardFee" runat="server" ></asp:Label>
                                                                                                                    </td>
                                                                                                                    <td style="width: 10%">
                                                                                                                        <asp:Label ID="lblTotAmountLv" runat="server" ></asp:Label>
                                                                                                                    </td>--%>
                                                                                                            </tr>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:ListView>
                                                                                    </asp:Panel>
                                                                            </tr>
                                                                        
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </asp:Panel>
                                    </div>

                                    <div class="col-12">
                                        <asp:Panel ID="pnlAdmissionBatch" runat="server" Visible="false">
                                            <asp:ListView ID="lvAdmissionBatch" runat="server" OnItemDataBound="lvAdmissionBatch_ItemDataBound" Visible="false">
                                                <LayoutTemplate>
                                                    <div class="sub-heading">
                                                        <h5>Admission Batch Wise Collection</h5>
                                                    </div>
                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th>Show
                                                                </th>
                                                                <th>College Name
                                                                </th>
                                                                <th>Overall Total Amount
                                                                </th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </tbody>
                                                    </table>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                        <tr id="MainTableRow" runat="server" class="item">
                                                            <td>
                                                                <asp:Panel ID="pnlDetails" runat="server" Style="cursor: pointer; vertical-align: top; float: left">
                                                                    <asp:Image ID="imgExp" runat="server" ImageUrl="~/images/action_down.png" />
                                                                </asp:Panel>
                                                                <%--&nbsp;&nbsp;<asp:Label ID="lbIoNo" runat="server"
                                                                                        Visible="false"></asp:Label>--%>
                                                            </td>
                                                             <td>
                                                                <asp:Label ID="lblCollName" runat="server" Text='<%# Eval("COLLEGE_NAME") %>' ToolTip='<%# Eval("COLLEGE_ID") %>'></asp:Label>
                                                            </td>
                                                             <td>
                                                                <asp:Label ID="lblOverAllAmount" runat="server" Text='<%# Eval("TOTAL_AMT") %>'></asp:Label>
                                                            </td>
                                                        </tr>

                                                    <tr>
                                                       
                                                                                    <asp:Panel ID="pnlShowCDetails" runat="server" CssClass="collapsePanel">
                                                                                       
                                                                                                    <asp:ListView ID="lvDetails" runat="server">
                                                                                                        <LayoutTemplate>
                                                                                                            <div id="demo-grid" class="vista-grid">
                                                                                                                <table class="dataTable table table-bordered table-striped table-hover">
                                                                                                                    <thead>
                                                                                                                        <tr class="header bg-light-blue">
                                                                                                                            <%--<th>Sr.no
                                                                                                                                </th>--%>
                                                                                                                            <th>Degree
                                                                                                                            </th>
                                                                                                                            <th>Branch 
                                                                                                                            </th>
                                                                                                                            <th>Applied Fee
                                                                                                                            </th>
                                                                                                                            <th>Paid Fees
                                                                                                                            </th>
                                                                                                                            <th>Outstanding Fees
                                                                                                                            </th>
                                                                                                                            <th>Excess Amount
                                                                                                                            </th>
                                                                                                                            <%--<th style="width: 10%">Total Amount</th>--%>
                                                                                                                        </tr>
                                                                                                                    </thead>
                                                                                                                    <tbody>
                                                                                                                        <tr id="itemPlaceholder" runat="server">
                                                                                                                        </tr>
                                                                                                                    </tbody>
                                                                                                                </table>
                                                                                                            </div>
                                                                                                        </LayoutTemplate>
                                                                                                        <EmptyDataTemplate>
                                                                                                            <div style="text-align: center; font-family: Arial; font-size: medium">
                                                                                                                No Record Found
                                                                                                            </div>
                                                                                                        </EmptyDataTemplate>
                                                                                                        <ItemTemplate>
                                                                                                            <tr class="item">
                                                                                                                <td>
                                                                                                                    <asp:Label ID="lblDegree" runat="server" Text='<%# Eval("DEGREE") %>'></asp:Label>
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:Label ID="lblBranch" runat="server" Text='<%# Eval("BRANCH") %>'></asp:Label>
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:Label ID="lblAppliedFee" runat="server" Text='<%# Eval("APPLIED_FEE") %>'></asp:Label>
                                                                                                                    <asp:HiddenField ID="hdnLevel" runat="server" />
                                                                                                                    <asp:HiddenField ID="hdnIntakeFrom" runat="server" />
                                                                                                                    <asp:HiddenField ID="hdnIntakeTo" runat="server" />
                                                                                                                    <%--<asp:HiddenField ID="hdnSubCategory" runat="server" Value='<%# Eval("FEE_SUB_CATEGORY") %>' /> --%>
                                                                                                                    <asp:HiddenField ID="hdnDuration" runat="server" />
                                                                                                                    <asp:HiddenField ID="hdnIntake" runat="server" />
                                                                                                                    <asp:HiddenField ID="hdnAdmittedCount" runat="server" />
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:Label ID="lblPaidFee" runat="server" Text='<%# Eval("PAID_FEE") %>'></asp:Label>
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:Label ID="lblOutstanding" runat="server" Text='<%# Eval("OUTSTANDING_FEE") %>'></asp:Label>
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:Label ID="lblExcess" runat="server" Text='<%# Eval("EXCESS_AMOUNT") %>'></asp:Label>
                                                                                                                </td>
                                                                                                                <%--<td style="width: 5%">
                                                                                                                        <asp:Label ID="lblStandardFee" runat="server" ></asp:Label>
                                                                                                                    </td>
                                                                                                                    <td style="width: 10%">
                                                                                                                        <asp:Label ID="lblTotAmountLv" runat="server" ></asp:Label>
                                                                                                                    </td>--%>
                                                                                                            </tr>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:ListView>
                                                                                              
                                                                                    </asp:Panel>
                                                                               
                                                                            </tr>
                                                                       

                                                </ItemTemplate>
                                            </asp:ListView>
                                            <%--</td>--%>
                                            <%-- </tr>
                                            </table>--%>
                                        </asp:Panel>
                                    </div>

                                    <div class="col-12">
                                        <asp:Panel ID="pnlDegreeBranch" runat="server" CssClass="collapsePanel" Visible="false">
                                            <asp:ListView ID="lvDetails" runat="server">
                                                <LayoutTemplate>
                                                    <div class="sub-heading">
                                                        <h5>Degree/Branch Level Collection</h5>
                                                    </div>
                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <%--<th>Sr.no
                                                                </th>--%>
                                                                <th>Degree
                                                                </th>
                                                                <th>Branch 
                                                                </th>
                                                                <th>Applied Fee
                                                                </th>
                                                                <th>Paid Fees
                                                                </th>
                                                                <th>Outstanding Fees
                                                                </th>
                                                                <th>Excess Amount
                                                                </th>
                                                                <%--<th style="width: 10%">Total Amount</th>--%>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server">
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </LayoutTemplate>
                                                <EmptyDataTemplate>
                                                    <div style="text-align: center; font-family: Arial; font-size: medium">
                                                        No Record Found
                                                    </div>
                                                </EmptyDataTemplate>
                                                <ItemTemplate>
                                                    <tr class="item">
                                                        <td>
                                                            <asp:Label ID="lblDegree" runat="server" Text='<%# Eval("DEGREE") %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblBranch" runat="server" Text='<%# Eval("BRANCH") %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblAppliedFee" runat="server" Text='<%# Eval("APPLIED_FEE") %>'></asp:Label>
                                                            <asp:HiddenField ID="hdnLevel" runat="server" />
                                                            <asp:HiddenField ID="hdnIntakeFrom" runat="server" />
                                                            <asp:HiddenField ID="hdnIntakeTo" runat="server" />
                                                            <%--<asp:HiddenField ID="hdnSubCategory" runat="server" Value='<%# Eval("FEE_SUB_CATEGORY") %>' /> --%>
                                                            <asp:HiddenField ID="hdnDuration" runat="server" />
                                                            <asp:HiddenField ID="hdnIntake" runat="server" />
                                                            <asp:HiddenField ID="hdnAdmittedCount" runat="server" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblPaidFee" runat="server" Text='<%# Eval("PAID_FEE") %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblOutstanding" runat="server" Text='<%# Eval("OUTSTANDING_FEE") %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblExcess" runat="server" Text='<%# Eval("EXCESS_AMOUNT") %>'></asp:Label>
                                                        </td>
                                                        <%--<td style="width: 5%">
                                                            <asp:Label ID="lblStandardFee" runat="server" ></asp:Label>
                                                        </td>
                                                        <td style="width: 10%">
                                                            <asp:Label ID="lblTotAmountLv" runat="server" ></asp:Label>
                                                        </td>--%>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </asp:Panel>
                                    </div>

                                    <div class="col-12">
                                        <asp:Panel ID="pnlFinancial" runat="server" Visible="false">
                                            <asp:ListView ID="lvFinancialYear" runat="server" OnItemDataBound="lvFinancialYear_ItemDataBound" Visible="false">
                                                <LayoutTemplate>
                                                    <div class="sub-heading">
                                                        <h5>Financial Year Wise Collection</h5>
                                                    </div>
                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th>Show
                                                                </th>
                                                                <th>College Name
                                                                </th>
                                                                <th>Overall Total Amount
                                                                </th>
                                                            </tr> 
                                                        </thead>
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </tbody>
                                                    </table>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr id="MainTableRow" runat="server">
                                                        <td>
                                                            <asp:Panel ID="pnlDetails" runat="server" Style="cursor: pointer; vertical-align: top; float: left">
                                                                <asp:Image ID="imgExp" runat="server" ImageUrl="~/images/action_down.png" />
                                                            </asp:Panel>
                                                            <%--&nbsp;&nbsp;<asp:Label ID="lbIoNo" runat="server"
                                                                                        Visible="false"></asp:Label>--%>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblCollName" runat="server" Text='<%# Eval("COLLEGE_NAME") %>' ToolTip='<%# Eval("COLLEGE_ID") %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblOverAllAmount" runat="server" Text='<%# Eval("TOTAL_AMT") %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <ajaxToolKit:CollapsiblePanelExtender ID="cpeCourt2" runat="server" CollapseControlID="pnlDetails"
                                                                Collapsed="true" CollapsedImage="~/images/action_down.png" ExpandControlID="pnlDetails"
                                                                ExpandedImage="~/images/action_up.png" ImageControlID="imgExp" TargetControlID="pnlShowCDetails">
                                                            </ajaxToolKit:CollapsiblePanelExtender>
                                                        </td>
                                                    </tr>
                                                    <%--</td>--%>
                                                    <%--</tr>--%>
                                                    <tr>
                                                      
                                                            <asp:Panel ID="pnlShowCDetails" runat="server" CssClass="collapsePanel">

                                                                <asp:ListView ID="lvDetails" runat="server">
                                                                    <LayoutTemplate>
                                                                        <div id="demo-grid" class="vista-grid" style="width: 99%; height: 60%">
                                                                            <table class="dataTable table table-bordered table-striped table-hover" width="99%">
                                                                                <thead>
                                                                                    <tr class="header bg-light-blue">
                                                                                        <%--<th>Sr.no
                                                                                                                                </th>--%>
                                                                                        <th>Degree
                                                                                        </th>
                                                                                        <th>Branch 
                                                                                        </th>
                                                                                        <th>Applied Fee
                                                                                        </th>
                                                                                        <th>Paid Fees
                                                                                        </th>
                                                                                        <th>Outstanding Fees
                                                                                        </th>
                                                                                        <th>Excess Amount
                                                                                        </th>
                                                                                        <%--<th style="width: 10%">Total Amount</th>--%>
                                                                                    </tr>
                                                                                </thead>
                                                                                <tbody>
                                                                                    <tr id="itemPlaceholder" runat="server">
                                                                                    </tr>
                                                                                </tbody>
                                                                            </table>
                                                                        </div>
                                                                    </LayoutTemplate>
                                                                    <EmptyDataTemplate>
                                                                        <div style="text-align: center; font-family: Arial; font-size: medium">
                                                                            No Record Found
                                                                        </div>
                                                                    </EmptyDataTemplate>
                                                                    <ItemTemplate>
                                                                        <tr class="item">
                                                                            <td style="width: 20%">
                                                                                <asp:Label ID="lblDegree" runat="server" Text='<%# Eval("DEGREE") %>'></asp:Label>
                                                                            </td>
                                                                            <td style="width: 20%">
                                                                                <asp:Label ID="lblBranch" runat="server" Text='<%# Eval("BRANCH") %>'></asp:Label>
                                                                            </td>
                                                                            <td style="width: 5%">
                                                                                <asp:Label ID="lblAppliedFee" runat="server" Text='<%# Eval("APPLIED_FEE") %>'></asp:Label>
                                                                                <asp:HiddenField ID="hdnLevel" runat="server" />
                                                                                <asp:HiddenField ID="hdnIntakeFrom" runat="server" />
                                                                                <asp:HiddenField ID="hdnIntakeTo" runat="server" />
                                                                                <%--<asp:HiddenField ID="hdnSubCategory" runat="server" Value='<%# Eval("FEE_SUB_CATEGORY") %>' /> --%>
                                                                                <asp:HiddenField ID="hdnDuration" runat="server" />
                                                                                <asp:HiddenField ID="hdnIntake" runat="server" />
                                                                                <asp:HiddenField ID="hdnAdmittedCount" runat="server" />
                                                                            </td>
                                                                            <td style="width: 5%">
                                                                                <asp:Label ID="lblPaidFee" runat="server" Text='<%# Eval("PAID_FEE") %>'></asp:Label>
                                                                            </td>
                                                                            <td style="width: 5%">
                                                                                <asp:Label ID="lblOutstanding" runat="server" Text='<%# Eval("OUTSTANDING_FEE") %>'></asp:Label>
                                                                            </td>
                                                                            <td style="width: 5%">
                                                                                <asp:Label ID="lblExcess" runat="server" Text='<%# Eval("EXCESS_AMOUNT") %>'></asp:Label>
                                                                            </td>
                                                                            <%--<td style="width: 5%">
                                                                                                                        <asp:Label ID="lblStandardFee" runat="server" ></asp:Label>
                                                                                                                    </td>
                                                                                                                    <td style="width: 10%">
                                                                                                                        <asp:Label ID="lblTotAmountLv" runat="server" ></asp:Label>
                                                                                                                    </td>--%>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:ListView>

                                                            </asp:Panel>
                                                      
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </asp:Panel>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="lvStudList" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnReport" />
        </Triggers>
    </asp:UpdatePanel>



    <div class="modal" id="myModalFee">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">

                <!-- Modal Header -->
                <%--  <div class="modal-header">
                    <h4 class="modal-title">Receipt Details</h4>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>--%>

                <!-- Modal body -->
                <div class="modal-body">
                    <%-- <div class="col-md-12">
                        <asp:TextBox ID="txt" runat="server"></asp:TextBox>
                    </div>--%>
                    <div class="row">
                        <div class="col-12">
                            <div class="sub-heading">
                                <h5>Receipt Details</h5>
                            </div>
                            <%--<div class="row">--%>
                            <asp:ListView ID="lvReceipt" runat="server" align="Center" Visible="false">
                                <LayoutTemplate>
                                    <div id="demo-grid" class="vista-grid">
                                        <%--style="width: 710PX; margin-left: 10px; margin-right: 10px"--%>
                                        <table id="tblHead" class="table table-bordered table-hover table-fixed">
                                            <thead>
                                                <tr class="bg-light-blue">
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
                            <%--</div>--%>
                        </div>
                    </div>

                    <%-- <div id="divPopup" runat="server" visible="false">--%>
                    <%-- <asp:LinkButton ID="lnkFake" runat="server"></asp:LinkButton>
                        <ajaxToolKit:ModalPopupExtender ID="mpePnlStudent" BehaviorID="mpe" runat="server" PopupControlID="pnlPopup1"
                            TargetControlID="lnkFake" OnOkScript="ResetSession()" BackgroundCssClass="modalBackground" CancelControlID="btnClose">
                        </ajaxToolKit:ModalPopupExtender>--%>


                    <%-- <div class="body">
                                <asp:Panel ID="pnlPopUp" runat="server" Height="300px" Width="718px" Style="overflow-x: hidden;" ScrollBars="Vertical">--%>

                    <%--</asp:Panel>--%>
                    <%--</div>--%>
                    <%--  <div class="footer" align="right">
                                <asp:Button ID="btnClose" runat="server" ForeColor="Red" Font-Bold="true" Font-Size="Larger" Text="Close" CssClass="no" />
                            </div>--%>
                    <%--  </div>--%>
                </div>

                <!-- Modal footer -->
                <div class="modal-footer">
                    <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                </div>

            </div>
        </div>
    </div>


    <div class="modal fade" id="myModal1" role="dialog">
        <div class="modal-dialog" style="width: 54%">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">
                        &times;</button>
                    <h4 class="modal-title">Receipt Info <i class="fa fa-info-circle"></i>
                    </h4>
                </div>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="box-body modal-warning">
                            <%--<div style="z-index: 1; position: absolute; top: 10px; left: 600px;">
                                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updPopUP"
                                            DynamicLayout="true" DisplayAfter="0">
                                            <ProgressTemplate>
                                                <div style="width: 120px; padding-left: 5px">
                                                    <i class="fa fa-refresh fa-spin" style="font-size:50px"></i>
                                                    <p class="text-success"><b>Loading..</b></p>
                                                </div>
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>

                                    </div>--%>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <div id="divMsg" runat="server">
    </div>

    <%--<script>
        $.noConflict();
    </script>--%>
    <script type="text/javascript">
        function showModal() {
            //alert('hii');
            //$.noConflict();
            $("#myModalFee").modal('show');
        }
    </script>

</asp:Content>
