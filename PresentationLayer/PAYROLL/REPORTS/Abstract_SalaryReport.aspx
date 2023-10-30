<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/SiteMasterPage.master" CodeFile="Abstract_SalaryReport.aspx.cs" Inherits="PAYROLL_TRANSACTIONS_Abstract_SalaryReport" %>

<%@ Register Assembly="AutoSuggestBox" Namespace="ASB" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>



<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div style="z-index: 1; position: fixed; left: 600px;">
        <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UPDLedger"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <asp:UpdatePanel runat="server" ID="UPDLedger">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">ABSTRACT SALARY (STAFF WISE EXCEL EXPORT)</h3>
                        </div>
                        <div class="box-body">
                            <asp:Panel ID="pnl" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="sub-heading">
                                                <h5>View Salary as Staff Wise</h5>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <label>Month<span style="color: Red">*</span></label>

                                            <asp:DropDownList ID="ddlMonthYear" runat="server" ToolTip="Select Month" CssClass="form-control" AppendDataBoundItems="True" TabIndex="1">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvMonthYear" runat="server" SetFocusOnError="true"
                                                ControlToValidate="ddlMonthYear" Display="None" ErrorMessage="Please Select Month"
                                                ValidationGroup="Payroll" InitialValue="0"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">

                                            <label>College <span style="color: Red">*</span></label>

                                            <asp:DropDownList ID="ddlCollege" runat="server" ToolTip="Select College" CssClass="form-control" AppendDataBoundItems="true"
                                                TabIndex="2">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rqfCollege" runat="server" ControlToValidate="ddlCollege"
                                                ValidationGroup="Payroll" ErrorMessage="Please Select College" SetFocusOnError="true"
                                                InitialValue="0" Display="None"></asp:RequiredFieldValidator>

                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="Div2" runat="server">
                                            <label>Scheme :</label>

                                            <asp:DropDownList ID="ddlStaffNo" runat="server" CssClass="form-control" ToolTip="Select Scheme" AppendDataBoundItems="True" TabIndex="3">
                                            </asp:DropDownList>
                                            <%--  <asp:RequiredFieldValidator ID="rfvStaffNo" runat="server" SetFocusOnError="true"
                                                            ControlToValidate="ddlStaffNo" Display="None" ErrorMessage="Please Select Scheme"
                                                            ValidationGroup="Payroll" InitialValue="0"></asp:RequiredFieldValidator>--%>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <label>Staff</label>

                                            <asp:DropDownList ID="ddlEmployeeType" runat="server" CssClass="form-control"
                                                AppendDataBoundItems="True" TabIndex="4" ToolTip="Select Staff">
                                            </asp:DropDownList>

                                        </div>



                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divgy" runat="server" visible="false">
                                            <label>Select Two Staffs:<span style="color: Red">*</span></label>

                                            <asp:ListBox ID="lstStaffFill" runat="server" Height="60px" CssClass="form-control" SelectionMode="Multiple" TabIndex="6" ToolTip="Select two Staffs"
                                                AppendDataBoundItems="True"></asp:ListBox>
                                            <asp:RequiredFieldValidator ID="rfvlstSfaffFill" runat="server" SetFocusOnError="true"
                                                ControlToValidate="lstStaffFill" Display="None" ErrorMessage="Please Select Staff from the list"
                                                ValidationGroup="Payroll" InitialValue="0"></asp:RequiredFieldValidator>

                                        </div>

                                    </div>
                                </div>
                            </asp:Panel>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnShowAbstractInExcelDeptGroup" runat="server" Text="Show To Export Into Excel File"
                                   CssClass="btn btn-info" ValidationGroup="Payroll" OnClick="btnShowAbstractExcelwithGrpTotal_Click" />

                                <asp:ValidationSummary ID="rfvValidationSummary" runat="server" ValidationGroup="Payroll" DisplayMode="List" ShowMessageBox="true" ShowSummary="False" />

                                <center style="display: none">

                                         <asp:ImageButton ID="imgbutExporttoexcelGrpTotal" runat="server" ToolTip="Export to excel"
                                         ImageUrl="~/IMAGES/excel.jpeg" Height="50px" Width="50px" OnClick="imgbutExporttoexcelGrpTotal_Click"  />
            
                                </center>
                            </div>
                            <div class="col-12" id="divpnlGroupTotal" runat="server" visible="false">
                                <asp:Panel ID="pnlGroupTotal" runat="server">
                                    <asp:GridView ID="grdSalarywithGrpTotal" runat="server" AutoGenerateColumns="false"
                                        OnDataBound="OnDataBound" OnRowCreated="OnRowCreated" OnRowDataBound="grdSalarywithGrpTotal_RowDataBound">
                                        <Columns>
                                            <asp:BoundField DataField="SRNOS" HeaderText="UN.NO." />
                                            <asp:BoundField DataField="SUBDEPTNO" HeaderText="DEPTID" />
                                            <asp:BoundField DataField="EMPLOYEEID" HeaderText="EMPLOYEE ID" />
                                            <asp:BoundField DataField="NAME" HeaderText="EMPLOYEE NAME" />
                                            <asp:BoundField DataField="MON" HeaderText="MONTH" />
                                            <%-- <asp:BoundField DataField = "STAFF" HeaderText = "STAFF"  /> --%>
                                            <asp:BoundField DataField="SUBDESIG" HeaderText="DESIGNATION" />
                                            <asp:BoundField DataField="SUBDEPT" HeaderText="STAFF" />
                                            <asp:BoundField DataField="MONTH_DAYS" HeaderText="PAIDDAYS" />
                                            <asp:BoundField DataField="BASIC" HeaderText="BASIC" />
                                            <%--<asp:BoundField DataField = "DAAMT" HeaderText = "DAAMT"  />--%>
                                            <asp:BoundField DataField="GRADEPAY" HeaderText="GRADE PAY" />
                                            <asp:BoundField DataField="PAY" HeaderText="PAY" />
                                            <asp:BoundField DataField="I1" HeaderText="I1" />
                                            <asp:BoundField DataField="I2" HeaderText="I2" />
                                            <asp:BoundField DataField="I3" HeaderText="I3" />
                                            <asp:BoundField DataField="I4" HeaderText="I4" />
                                            <asp:BoundField DataField="I5" HeaderText="I5" />
                                            <asp:BoundField DataField="I6" HeaderText="I6" />
                                            <asp:BoundField DataField="I7" HeaderText="I7" />
                                            <asp:BoundField DataField="I8" HeaderText="I8" />
                                            <asp:BoundField DataField="I9" HeaderText="I9" />
                                            <asp:BoundField DataField="I10" HeaderText="I10" />
                                            <asp:BoundField DataField="I11" HeaderText="I11" />
                                            <asp:BoundField DataField="I12" HeaderText="I12" />
                                            <asp:BoundField DataField="I13" HeaderText="I13" />
                                            <asp:BoundField DataField="I14" HeaderText="I14" />
                                            <asp:BoundField DataField="I15" HeaderText="I15" />
                                            <asp:BoundField DataField="GS" HeaderText="GS" />
                                            <asp:BoundField DataField="D1" HeaderText="D1" />
                                            <asp:BoundField DataField="D2" HeaderText="D2" />
                                            <asp:BoundField DataField="D3" HeaderText="D3" />
                                            <asp:BoundField DataField="D4" HeaderText="D4" />
                                            <asp:BoundField DataField="D5" HeaderText="D5" />
                                            <asp:BoundField DataField="D6" HeaderText="D6" />
                                            <asp:BoundField DataField="D7" HeaderText="D7" />
                                            <asp:BoundField DataField="D8" HeaderText="D8" />
                                            <asp:BoundField DataField="D9" HeaderText="D9" />
                                            <asp:BoundField DataField="D10" HeaderText="D10" />
                                            <asp:BoundField DataField="D11" HeaderText="D11" />
                                            <asp:BoundField DataField="D12" HeaderText="D12" />
                                            <asp:BoundField DataField="D13" HeaderText="D13" />
                                            <asp:BoundField DataField="D14" HeaderText="D14" />
                                            <asp:BoundField DataField="D15" HeaderText="D15" />
                                            <asp:BoundField DataField="D16" HeaderText="D16" />
                                            <asp:BoundField DataField="D17" HeaderText="D17" />
                                            <asp:BoundField DataField="D18" HeaderText="D18" />
                                            <asp:BoundField DataField="D19" HeaderText="D19" />
                                            <asp:BoundField DataField="D20" HeaderText="D20" />
                                            <asp:BoundField DataField="D21" HeaderText="D21" />
                                            <asp:BoundField DataField="D22" HeaderText="D22" />
                                            <asp:BoundField DataField="D23" HeaderText="D23" />
                                            <asp:BoundField DataField="D24" HeaderText="D24" />
                                            <asp:BoundField DataField="D25" HeaderText="D25" />
                                            <asp:BoundField DataField="D26" HeaderText="D26" />
                                            <asp:BoundField DataField="D27" HeaderText="D27" />
                                            <asp:BoundField DataField="D28" HeaderText="D28" />
                                            <asp:BoundField DataField="D29" HeaderText="D29" />
                                            <asp:BoundField DataField="D30" HeaderText="D30" />
                                            <asp:BoundField DataField="TOT_DED" HeaderText="Total Deduction" />
                                            <asp:BoundField DataField="NET_PAY" HeaderText="Net Pay" />
                                            <asp:TemplateField HeaderText="Sl.no">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--<asp:BoundField DataField = "Price" HeaderText = "Price"  DataFormatString = "{0:N2}" ItemStyle-HorizontalAlign = "Right" />--%>
                                        </Columns>
                                    </asp:GridView>
                                </asp:Panel>
                                <%--<asp:ImageButton ID="imgbutExporttoexcel" runat="server" ToolTip="Export to excel"
                            ImageUrl="~/IMAGES/excel.jpeg" Height="50px" Width="50px" OnClick="imgbutExporttoexcel_Click" />--%>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnShowAbstractInExcelDeptGroup" />
        </Triggers>
    </asp:UpdatePanel>
    <div id="divMsg" runat="server">
    </div>
</asp:Content>
