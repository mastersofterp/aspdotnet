<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="BulkUpdOpeningBalance.aspx.cs" Inherits="ACADEMIC_BulkUpdPermanentRegNo"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

<%--    <script src="https://cdn.datatables.net/1.10.4/js/jquery.dataTables.min.js"></script>--%>

    <script type="text/javascript">
        //On Page Load
        $(document).ready(function () {
            $('#table2').DataTable();
        });
    </script>

    <script type="text/javascript">
        //On UpdatePanel Refresh
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    $('#table2').dataTable();
                }
            });
        };
    </script>

 <%--   <script type="text/javascript">
        RunThisAfterEachAsyncPostback();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
    </script>--%>
    <%-- <asp:UpdatePanel ID="updAll" runat="server">
        <ContentTemplate>
    --%>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">BULK UPDATE OPENING BALANCE</h3>
                </div>

                <div class="box-body">
                    <asp:Panel ID="pnl" runat="server">
                        <div class="col-12">
                            <div class="row">
                                <div class="col-12">
                                    <div class="sub-heading">
                                        <h5>Opening Balance Entry</h5>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-12">
                            <div class="row">
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <label>Transaction Type</label>
                                    </div>
                                    <asp:DropDownList ID="ddlState" runat="server" CssClass="form-control" TabIndex="1" AutoPostBack="true" data-select2-enable="true"
                                        OnSelectedIndexChanged="ddlState_SelectedIndexChanged" ToolTip="Select Transaction Type">
                                        <asp:ListItem Selected="True" Value="1" Text="New Allotment"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Modification In Old Allotment"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>College Name</label>
                                    </div>
                                    <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control" TabIndex="2" AppendDataBoundItems="true" data-select2-enable="true"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged" ToolTip="Select College Name">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlCollege" InitialValue="0"
                                        Display="None" ErrorMessage="Please Select College Name " ValidationGroup="submit"
                                        SetFocusOnError="true">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Staff Type</label>
                                    </div>
                                    <asp:DropDownList ID="ddlStaffType" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                        AutoPostBack="True" TabIndex="3" ToolTip="Select Staff Type"
                                        OnSelectedIndexChanged="ddlStaffType_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvddlAdmBatch" runat="server" ControlToValidate="ddlStaffType"
                                        Display="None" ErrorMessage="Please Select Staff Type" InitialValue="0" SetFocusOnError="True"
                                        ValidationGroup="submit"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12" id="trDept" runat="server" visible="false">
                                    <div class="label-dynamic">
                                        <label>Select Department</label>
                                    </div>
                                    <asp:DropDownList ID="ddldept" runat="server" AppendDataBoundItems="true" CssClass="form-control" TabIndex="4" data-select2-enable="true"
                                        AutoPostBack="True" OnSelectedIndexChanged="ddldept_SelectedIndexChanged" ToolTip="Select Department">
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Period</label>
                                    </div>
                                    <asp:DropDownList ID="ddlPeriod" runat="server" CssClass="form-control" TabIndex="5" data-select2-enable="true"
                                        AppendDataBoundItems="true" AutoPostBack="true" ToolTip="Select Period"
                                        OnSelectedIndexChanged="ddlPeriod_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>

                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlPeriod"
                                        Display="None" ErrorMessage="Please Select Period " InitialValue="0" SetFocusOnError="True"
                                        ValidationGroup="submit"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Year</label>
                                    </div>
                                    <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control" AutoPostBack="true" TabIndex="6" data-select2-enable="true"
                                        OnSelectedIndexChanged="ddlYear_SelectedIndexChanged" ToolTip="Select Year">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlYear"
                                        Display="None" ErrorMessage="Please Select Year " InitialValue="0" SetFocusOnError="True"
                                        ValidationGroup="submit"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>From Date</label>
                                    </div>
                                    <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i id="imgCalholidayDt" runat="server" class="fa fa-calendar text-blue"></i>
                                            </div>
                                        <asp:TextBox ID="txtFromDt" runat="server" MaxLength="10" CssClass="form-control" TabIndex="7" Enabled="false"
                                            ToolTip="Enter From Date" Style="z-index: 0;" />
                                        <asp:RequiredFieldValidator ID="rfvholidayDt" runat="server" ControlToValidate="txtFromDt"
                                            Display="None" ErrorMessage="Please Enter From Date" ValidationGroup="submit"
                                            SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        <ajaxToolKit:CalendarExtender ID="ceholidayDt" runat="server" Format="dd/MM/yyyy"
                                            TargetControlID="txtFromDt" PopupButtonID="imgCalholidayDt" Enabled="true"
                                            EnableViewState="true">
                                        </ajaxToolKit:CalendarExtender>
                                        <ajaxToolKit:MaskedEditExtender ID="meeholidayDt" runat="server" TargetControlID="txtFromDt"
                                            Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                            AcceptNegative="Left" ErrorTooltipEnabled="true" />
                                        <ajaxToolKit:MaskedEditValidator ID="mevholidayDt" runat="server" ControlExtender="meeholidayDt"
                                            ControlToValidate="txtFromDt" EmptyValueMessage="Please Enter  From Date"
                                            InvalidValueMessage=" From Date is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                            TooltipMessage="Please Enter From Date" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                            ValidationGroup="Leave" SetFocusOnError="true"></ajaxToolKit:MaskedEditValidator>
                                    </div>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>To Date</label>
                                    </div>
                                    <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i id="imgCalToDt" runat="server" class="fa fa-calendar text-blue"></i>
                                            </div>                                        
                                        <asp:TextBox ID="txtToDt" runat="server" MaxLength="10" CssClass="form-control" TabIndex="8" Enabled="false"
                                            ToolTip="Enter To Date" Style="z-index: 0;" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtToDt"
                                            Display="None" ErrorMessage="Please Enter  To Date" ValidationGroup="submit"
                                            SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                            TargetControlID="txtToDt" PopupButtonID="imgCalToDt" Enabled="true"
                                            EnableViewState="true">
                                        </ajaxToolKit:CalendarExtender>
                                        <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtToDt"
                                            Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                            AcceptNegative="Left" ErrorTooltipEnabled="true" />
                                        <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="meeholidayDt"
                                            ControlToValidate="txtToDt" EmptyValueMessage="Please Enter  To Date"
                                            InvalidValueMessage=" To Date is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                            TooltipMessage="Please Enter  To Date" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                            ValidationGroup="Leave" SetFocusOnError="true"></ajaxToolKit:MaskedEditValidator>
                                        <%--  <asp:CompareValidator ID="CampCalExtDate" runat="server" ControlToValidate="txtToDt"
                                                        CultureInvariantValues="true" Display="Static" ErrorMessage="To Date Must Be Equal To Or Greater Than From Date." Operator="GreaterThanEqual" SetFocusOnError="True" Type="Date"
                                                        ValidationGroup="submit" ControlToCompare="txtFromDt" />--%>
                                    </div>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Leave Name</label>
                                    </div>
                                    <asp:DropDownList ID="ddlLeave" runat="server" AppendDataBoundItems="true" CssClass="form-control" TabIndex="9" data-select2-enable="true"
                                        AutoPostBack="True" OnSelectedIndexChanged="ddlLeave_SelectedIndexChanged" ToolTip="Select Leave Name">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlLeave"
                                        Display="None" ErrorMessage="Please Select Leave " InitialValue="0" SetFocusOnError="True"
                                        ValidationGroup="submit"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                    <div class="col-12 btn-footer">
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click"
                            ValidationGroup="submit" CssClass="btn btn-primary" ToolTip="Click here to Submit" TabIndex="10" />
                        <asp:Button ID="btnCancle" runat="server" Text="Cancel" OnClick="btnCancle_Click" CssClass="btn btn-warning" ToolTip="Click here to Cancle" TabIndex="11" />
                        <asp:ValidationSummary ID="ValidationSummury" runat="server" DisplayMode="List"
                            ShowMessageBox="True" ShowSummary="False" ValidationGroup="submit" />
                    </div>
                    <div class="col-12">
                        <%--<asp:Panel ID="pnlStudent" runat="server" Style="overflow-x: hidden; overflow-y: scroll; height: 300px;" Width="100%" Visible="false">--%>
                        <asp:Panel ID="pnlEmployee" runat="server" Visible="false">
                            <%--   <asp:Repeater ID="lvLeave" runat="server">
                                <HeaderTemplate>
                                    <h4 class="box-title">Employee List</h4>
                                    <table id="table2" class="table table-striped dt-responsive nowrap">
                                        <thead>
                                            <tr class="bg-light-blue">
                                                <th>Name
                                                </th>
                                                <th>Opening Balance
                                                </th>
                                               
                                            </tr>
                                        </thead>
                                        <tbody>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <%# Eval("NAME")%>
                                        </td>
                                        <td>
                                             <asp:TextBox ID="txtOpBal" runat="server" TabIndex="4" Text='<%# Eval("OB")%>'  />
                                            <asp:HiddenField ID="hdfidno" runat="server" Value='<%# Eval("idno")%>' />
                                        </td>
                                        
                                       
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </tbody></table>
                                </FooterTemplate>
                            </asp:Repeater>        --%>
                            <asp:ListView ID="lvLeave" runat="server">
                                <LayoutTemplate>
                                    <div class="sub-heading">
                                        <h5>Employee List</h5>
                                    </div>
                                   <%-- <table class="table table-striped table-bordered nowrap display" style="width: 100%">--%>
                                         <table style="width: 100%" >
                                        <thead class="bg-light-blue">
                                            <tr>
                                                <th>Name
                                                </th>
                                                <th>Opening Balance
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
                                            <%# Eval("NAME")%>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtOpBal" runat="server" TabIndex="4" Text='<%# Eval("OB")%>' />
                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftbe" runat="server" TargetControlID="txtOpBal" FilterType="Numbers,Custom" ValidChars=".-"></ajaxToolKit:FilteredTextBoxExtender>
                                            <asp:HiddenField ID="hdfidno" runat="server" Value='<%# Eval("idno")%>' />
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
