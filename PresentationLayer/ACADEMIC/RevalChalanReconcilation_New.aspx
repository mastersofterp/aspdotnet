<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="RevalChalanReconcilation_New.aspx.cs" Inherits="Academic_RevalChalanReconcilation"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" charset="utf-8">
        $(document).ready(function () {
            $(".display").dataTable({
                "bJQueryUI": true,
                "sPaginationType": "full_numbers"
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
                            <h3 class="box-title">CHALLAN RECONCILATION FOR EXAM</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">


                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Institute</label>
                                        </div>
                                        <asp:DropDownList ID="ddlCollege" runat="server" AppendDataBoundItems="True" AutoPostBack="true" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlClgScheme_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvClgScheme" runat="server" ControlToValidate="ddlCollege"
                                            Display="None" ErrorMessage="Please Select Institute." SetFocusOnError="true" InitialValue="0"
                                            ValidationGroup="search" />
                                    </div>



                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Session</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session." SetFocusOnError="true" InitialValue="0"
                                            ValidationGroup="search" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Reval Type </label>
                                        </div>
                                        <asp:DropDownList ID="ddlReceiptType" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlReceiptType"
                                            Display="None" ErrorMessage="Please Select Reval Type." SetFocusOnError="true" InitialValue="0"
                                            ValidationGroup="search" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Univ. Reg. No. / Adm. No.</label>
                                        </div>
                                        <asp:TextBox ID="txtSearchText" runat="server" CssClass="form-control" ToolTip="Enter text to search." />
                                        <asp:RequiredFieldValidator ID="valSearchText" runat="server" ControlToValidate="txtSearchText"
                                            Display="None" ErrorMessage="Please Enter Univ. Reg. No. / Adm. No." SetFocusOnError="true"
                                            ValidationGroup="search" />
                                        <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                                            ShowSummary="false" ValidationGroup="search" />
                                    </div>


                                </div>
                            </div>
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-12 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label></label>
                                        </div>
                                        <p class="text-center">
                                            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click"
                                                ValidationGroup="search" CssClass="btn btn-outline-primary" />
                                        </p>
                                    </div>

                                </div>
                            </div>
                            <div class="col-12" id="divinfo" runat="server" visible="false">
                                <div class="row">
                                    <div class="col-lg-5 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Student Name  :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblStudName" Font-Bold="true" runat="server" />
                                                </a>
                                            </li>
                                            <li class="list-group-item"><b>Degree :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblDegree" Font-Bold="true" runat="server" /></a>
                                            </li>

                                        </ul>
                                    </div>
                                    <div class="col-lg-5 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Branch :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblBranch" CssClass="data_label" runat="server" /></a>
                                            </li>
                                            <li class="list-group-item"><b>Current Semester :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblSemester" CssClass="data_label" runat="server" /></a>
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12 mt-3">
                                <asp:ListView ID="lvSearchResults" runat="server">
                                    <LayoutTemplate>
                                        <div id="listViewGrid">
                                            <div class="sub-heading">
                                                <h5>>Chalan Search Results</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="tblSearchResults">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Select
                                                        </th>
                                                        <%-- <th>Challan No.
                                                    </th>--%>
                                                        <th>Challan Date
                                                        </th>
                                                        <th>Receipt Type
                                                        </th>
                                                        <%-- <th id="Sem">Semester
                                                    </th>--%>
                                                        <th>Pay Type
                                                        </th>
                                                        <th>Amount
                                                        </th>
                                                        <th>PAY Status
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                        </div>
                                    </LayoutTemplate>
                                    <%-- DO NOT FORMAT FOLLOWING 5-6 LINES. JAVASCRIPT IS DEPENDENT ON ELEMENT HIERARCHY --%>
                                    <EmptyDataTemplate>
                                    </EmptyDataTemplate>
                                    <ItemTemplate>

                                        <tr>
                                            <td>
                                                <label id="lblreco" runat="server">
                                                    <input id="rdoSelectRecord" value='<%# Eval("IDNO") %>' name="Receipts" type="radio"
                                                        onclick="ShowRemark(this);" /></label>
                                                <asp:HiddenField ID="hidRemark" runat="server" Value='<%# Eval("REMARK") %>' />
                                                <asp:HiddenField ID="hidSessionNo" runat="server" Value='<%# Eval("SESSIONNO") %>' />
                                                <%-- <asp:HiddenField ID="hidDcrSemesterNo" runat="server" Value='<%# Eval("SEMESTERNO") %>' />--%>
                                                <asp:HiddenField ID="hidIdNo" runat="server" Value='<%# Eval("IDNO") %>' />
                                                <asp:HiddenField ID="hidRcypt" runat="server" Value='<%# Eval("RECIEPT_CODE") %>' />
                                                <asp:HiddenField ID="hidrecon" runat="server" Value='<%# Eval("RECON") %>' />
                                            </td>
                                            <%-- <td>
                                            <%# Eval("REC_NO") %>
                                        </td>--%>
                                            <td>
                                                <%# Eval("REC_DT") %>
                                            </td>
                                            <td>
                                                <%# Eval("RECIEPT_TITLE") %>
                                            </td>
                                            <%-- <td>
                                            <%# Eval("SEMESTERNAME")%>
                                        </td>--%>
                                            <td>
                                                <%# Eval("PAY_MODE_CODE")%>
                                            </td>
                                            <td>
                                                <%# Eval("TOTAL_AMT")%>
                                            </td>
                                            <td>
                                                <%# Eval("STATUS")%>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>

                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12" id="divRemark" runat="server" visible="false">
                                <div class="label-dynamic">
                                    <sup>*</sup>
                                    <label>Reason For Cancelling Challan </label>
                                </div>
                                <asp:TextBox ID="txtRemark" Rows="4" TextMode="MultiLine" Width="450px" MaxLength="400"
                                    runat="server" />
                            </div>
                            <div class="col-12 btn-footer" id="divChallanButtonDetails" runat="server" visible="false">
                                <input id="btnReconcile"  runat="server" type="button" onclick="ReconcileChalan();"
                                    value="Reconcile Challan" disabled="disabled" class="btn btn-outline-primary" />   <%--onclick="ReconcileChalan();"--%>

                                <input id="btnDelete" onclick="DeleteChalan();" runat="server" type="button" value="Cancel Challan"
                                    disabled="disabled" class="btn btn-outline-danger" />

                                <asp:Button ID="btncancel" runat="server" Text="Cancel"
                                    OnClick="btncancel_Click" CssClass="btn btn-outline-danger" />

                            </div>

                            <div id="divMsg" runat="server">
                            </div>
                        </div>
                    </div>
                </div>
            </div>


        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSearch" />
            <asp:PostBackTrigger ControlID="btnReconcile" />
            <asp:PostBackTrigger ControlID="btnDelete" />
        </Triggers>
    </asp:UpdatePanel>

    <script type="text/javascript" language="javascript">

        function ReconcileChalan() {
            try {
                if (ValidateRecordSelection()) {
                    __doPostBack("ReconcileChalan", "");
                }
                else {
                    alert("Please select a challan to reconcile.");
                }
            }
            catch (e) {
                alert("Error: " + e.description);
            }
        }

        function DeleteChalan() {
            try {
                if (ValidateRecordSelection()) {
                    if (document.getElementById('<%= txtRemark.ClientID %>').value.trim() != "") {
                        if (confirm("If you cancel this challan, it will not be appear in the system.\n\nAre you sure you want to cancel this challan?")) {
                            __doPostBack("DeleteChalan", "");
                        }
                    }
                    else
                        alert('Please enter reason of cancelling challan.');
                }
                else {
                    alert("Please select a challan to cancel.");
                }
            }
            catch (e) {
                alert("Error: " + e.description);
            }
        }

        function ValidateRecordSelection() {
            var check = false;
            var gridView = document.getElementById("tblSearchResults");
            var checkBoxes = gridView.getElementsByTagName("input");
            for (var i = 0; i < checkBoxes.length; i++) {
                if (checkBoxes[i].type == "radio") {
                    if (checkBoxes[i].checked) {
                        check = true;
                    }

                }
            }
            return check;
        }
        function ShowRemark(rdoSelect) {
            try {
                if (rdoSelect != null && rdoSelect.nextSibling != null) {
                    var hidRemark = rdoSelect.nextSibling;
                    if (hidRemark != null)
                        document.getElementById('<%= txtRemark.ClientID %>').value = hidRemark.value;
                }
            }
            catch (e) {
                alert("Error: " + e.description);
            }
        }




    </script>

</asp:Content>
