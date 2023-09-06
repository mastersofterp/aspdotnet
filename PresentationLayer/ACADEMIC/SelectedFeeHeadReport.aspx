<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="SelectedFeeHeadReport.aspx.cs" Inherits="Academic_SelectedFeeHeadReport"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">Selected Fee Head Report</h3>
                </div>

                <div class="box-body">
                    <div class="col-12">
                        <div class="row">
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>From Date</label>
                                </div>
                                <div class="input-group date">
                                    <div class="input-group-addon" id="imgCalFromDate">
                                        <i class="fa fa-calendar text-blue"></i>
                                    </div>
                                    <asp:TextBox ID="txtFromDate" runat="server" TabIndex="1" CssClass="form-control" />
                                    <%-- <asp:Image ID="imgCalFromDate" runat="server" src="../images/calendar.png"
                                        Style="cursor: hand" TabIndex="0" />--%>
                                    <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                        TargetControlID="txtFromDate" PopupButtonID="imgCalFromDate" Enabled="true" EnableViewState="true">
                                    </ajaxToolKit:CalendarExtender>
                                    <asp:RequiredFieldValidator ID="valFromDate" runat="server" ControlToValidate="txtFromDate"
                                        Display="None" ErrorMessage="Please enter initial date for report." SetFocusOnError="true"
                                        ValidationGroup="report" />
                                    <ajaxToolKit:MaskedEditExtender ID="meeFromDate" runat="server" TargetControlID="txtFromDate"
                                        Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                        AcceptNegative="Left" ErrorTooltipEnabled="true" />
                                </div>
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>To Date</label>
                                </div>
                                <div class="input-group date">
                                    <div class="input-group-addon" id="imgCalToDate">
                                        <i class="fa fa-calendar text-blue"></i>
                                    </div>
                                    <asp:TextBox ID="txtToDate" runat="server" TabIndex="2" CssClass="form-control" />
                                    <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                        TargetControlID="txtToDate" PopupButtonID="imgCalToDate" Enabled="true" EnableViewState="true">
                                    </ajaxToolKit:CalendarExtender>
                                    <ajaxToolKit:MaskedEditExtender ID="meeToDate" runat="server" TargetControlID="txtToDate"
                                        Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                        AcceptNegative="Left" ErrorTooltipEnabled="true" />
                                    <%--<asp:Image ID="imgCalToDate" runat="server" src="../images/calendar.png"
                                        Style="cursor: hand" TabIndex="0" />--%>
                                    <asp:RequiredFieldValidator ID="valToDate" runat="server" ControlToValidate="txtToDate"
                                        Display="None" ErrorMessage="Please enter last date for report." SetFocusOnError="true"
                                        ValidationGroup="report" />
                                </div>
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <label>Receipt Type</label>
                                </div>
                                <asp:DropDownList ID="ddlReceiptType" runat="server" AppendDataBoundItems="true"
                                    TabIndex="3" AutoPostBack="True" CssClass="form-control" data-select2-enable="true"
                                    OnSelectedIndexChanged="ddlReceiptType_SelectedIndexChanged" />
                            </div>
                        </div>
                    </div>

                    <asp:UpdatePanel ID="updReceiptType" runat="server">
                        <ContentTemplate>
                            <div class="col-md-12">
                                <asp:Panel ID="Panel1" runat="server">
                                    <asp:ListView ID="lvSelectedFeeItems" runat="server" Style="display: none">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Available Fee Item</h5>
                                                <span style="color:red"><b>* Please select any 5 fee Head</b></span>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="divsessionlist">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Select
                                                        </th>
                                                        <th>Short Name
                                                        </th>
                                                        <th>Long Name
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
                                                    <asp:CheckBox
                                                        ID="chkReport" runat="server" onclick="ValidateSelection(this)" />
                                                    <asp:HiddenField ID="hidFeeHead" runat="server" Value='<%# Eval("FEE_HEAD") %>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblFeeShortName" runat="server" Text='<%# Eval("FEE_SHORTNAME")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <%# Eval("FEE_LONGNAME") %>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <EmptyDataTemplate>
                                            <div align="center" class="data_label">
                                                -- No Fee Item Found --
                                            </div>
                                        </EmptyDataTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                    <div class="col-12">
                        <div class="row">
                            <div class="col-12">
                                <div class="sub-heading">
                                    <h5>Data Filters</h5>
                                </div>
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <label>Degree</label>
                                </div>
                                <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                    TabIndex="4" />
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <label>Year</label>
                                </div>
                                <asp:DropDownList ID="ddlYear" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                    TabIndex="7" />
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <label>Branch</label>
                                </div>
                                <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                    TabIndex="5" />
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <label>Semester</label>
                                </div>
                                <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                    TabIndex="9" />
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false">
                                <div class="label-dynamic">
                                    <label>Option For Caution Money Report</label>
                                </div>
                                <asp:CheckBox ID="chkWithoutZero" runat="server" Text="WithoutZero" />
                            </div>
                        </div>
                    </div>

                    <div class="col-12 btn-footer">
                        <asp:Button ID="btnReport" runat="server" Text="Selected Fee Head Report" OnClick="btnReport_Click"
                            TabIndex="8" ValidationGroup="report" CssClass="btn btn-info" />
                        <asp:Button ID="btnCaution" runat="server" Text="Caution Money Report" TabIndex="9" Visible="false"
                            CausesValidation="False" OnClick="btnCaution_Click" CssClass="btn btn-info" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                            OnClick="btnCancel_Click" TabIndex="10" CssClass="btn btn-danger" />

                        <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                            ShowSummary="false" ValidationGroup="report" />
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div id="divMsg" runat="server">
    </div>

    <script type="text/javascript">

        function ValidateSelection(chkbox) {
            if (!ValidateMultipleSelection(chkbox)) {
                document.getElementById(chkbox.id).checked = false;
                alert('Maximum five fee item selection is allowed.');
            }
        }

        function ValidateMultipleSelection(chkbox) {
            var isValid = true;
            var counter = 1;
            try {
                var tbl = document.getElementById('tblSelectedFeeItems');
                if (tbl != null && tbl.rows && tbl.rows.length > 0) {
                    for (i = 1; i < tbl.rows.length; i++) {
                        var dataRow = tbl.rows[i];
                        var dataCell = dataRow.firstChild;
                        var check = dataCell.firstChild;
                        if (check.checked) {
                            if (counter < 5)
                                counter++;
                            else
                                isValid = false;
                        }
                    }
                }
            }
            catch (e) {
                alert("Error: " + e.description);
            }
            return isValid;
        }

        function ValidateSingleSelection() {
            var isValid = false;
            try {
                var tbl = document.getElementById('tblSelectedFeeItems');
                if (tbl != null && tbl.rows && tbl.rows.length > 0) {
                    for (i = 1; i < tbl.rows.length; i++) {
                        var dataRow = tbl.rows[i];
                        var dataCell = dataRow.firstChild;
                        var check = dataCell.firstChild;
                        if (check.checked) {
                            isValid = true;
                        }
                    }
                }
            }
            catch (e) {
                alert("Error: " + e.description);
            }
            return isValid;
        }
    </script>

</asp:Content>
