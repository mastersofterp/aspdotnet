<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Pay_AstractSalary_ForMultipleStaff.aspx.cs" Inherits="PAYROLL_REPORTS_Pay_AstractSalary_ForMultipleStaff" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">ABSTRACT SALARY REPORT FOR MULTIPLE STAFF</h3>
                </div>

                <div class="box-body">
                    <div class="col-12">
                        <div class="row">
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Month</label>
                                </div>
                                <asp:DropDownList ID="ddlMonthYear" runat="server" ToolTip="Select Month" CssClass="form-control" AppendDataBoundItems="True" TabIndex="1" data-select2-enable="true">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvMonthYear" runat="server" SetFocusOnError="true"
                                    ControlToValidate="ddlMonthYear" Display="None" ErrorMessage="Please Select Month/Year"
                                    ValidationGroup="Payroll" InitialValue="0"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>College</label>
                                </div>
                                <asp:DropDownList ID="ddlCollege" runat="server" ToolTip="Select College" CssClass="form-control" AppendDataBoundItems="true" data-select2-enable="true"
                                    TabIndex="2">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rqfCollege" runat="server" ControlToValidate="ddlCollege"
                                    ValidationGroup="Payroll" ErrorMessage="Please select College" SetFocusOnError="true"
                                    InitialValue="0" Display="None"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic" id="trstaff" runat="server">
                                    <sup>* </sup>
                                    <label>Scheme</label>
                                </div>
                                <asp:DropDownList ID="ddlStaffNo" runat="server" CssClass="form-control" ToolTip="Select Scheme" AppendDataBoundItems="True" TabIndex="3" data-select2-enable="true">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" SetFocusOnError="true"
                                    ControlToValidate="ddlStaffNo" Display="None" ErrorMessage="Please Select Scheme"
                                    ValidationGroup="Payroll" InitialValue="0"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic" id="Div2" runat="server">
                                    <sup>* </sup>
                                    <label>Bill No.</label>
                                </div>
                                <asp:TextBox ID="txtBillNo" runat="server" CssClass="textbox" TabIndex="4"></asp:TextBox>
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic" id="Div3" runat="server">
                                    <label>Report Heading</label>
                                </div>
                                <asp:TextBox ID="txtReportHeadingKannada" runat="server" TabIndex="5"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 btn-footer">
                        <asp:ValidationSummary ID="rfvValidationSummary" runat="server" ValidationGroup="Payroll"
                            DisplayMode="List" ShowMessageBox="true" ShowSummary="False" />
                        <asp:Button ID="btnRegisterWithAbstract" runat="server" TabIndex="6" Text="Salary Register With Abstract" CssClass="btn btn-info" ValidationGroup="Payroll" OnClick="btnRegisterWithAbstract_Click" />
                        <asp:Button ID="btnKannadaReport" runat="server" TabIndex="7" Text="Salary Register Kannada" CssClass="btn btn-info" Visible="false" ValidationGroup="Payroll" OnClick="btnKannadaReport_Click" />
                        <asp:Button ID="btnExcel" runat="server" TabIndex="8" Text="Export Into Excel File" CssClass="btn btn-info" ValidationGroup="Payroll" OnClick="btnExcel_Click" Visible="false" />
                        <asp:Button ID="btnNonPlan" runat="server" TabIndex="9" Text="Non Plan Report" CssClass="btn btn-info" ValidationGroup="Payroll" OnClick="btnNonPlan_Click" />
                        <asp:Button ID="btn85Per" runat="server" TabIndex="10" Text="85% Gross" CssClass="btn btn-info" ValidationGroup="Payroll" OnClick="btn85Per_Click" />
                        <asp:Button ID="btn15Per" runat="server" TabIndex="11" Text="15% Gross" CssClass="btn btn-info" ValidationGroup="Payroll" OnClick="btn15Per_Click" />
                        <asp:Button ID="btn15PerExcel" runat="server" TabIndex="12" Text="Export To Excel File 15% " CssClass="btn btn-info" ValidationGroup="Payroll" OnClick="btn15PerExcel_Click" />
                        <asp:Button ID="btnCancel" runat="server" TabIndex="13" CssClass="btn btn-warning" Text="Cancel" OnClick="btnCancel_Click" />
                    </div>
                    <div class="col-12" id="div_ExportToExcel" runat="server" visible="false">
                        <asp:Panel ID="pnlList" runat="server">
                            <div style="background-color: lightskyblue; border: 1px solid black">
                                <asp:Label ID="lblCaption" runat="server" Style="font-weight: bold; color: #000;"></asp:Label>
                            </div>
                            <asp:ListView ID="grdSelectFieldReport" runat="server">
                                <EmptyDataTemplate>
                                    <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="Click Add New To Enter Earnings And Deducations" />
                                </EmptyDataTemplate>
                                <LayoutTemplate>
                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                        <thead class="bg-light-blue">
                                            <tr>

                                                <th>ಕ್ರಮಸಂ
                                                </th>
                                                <th>ಸಿ ಸಂ
                                                </th>
                                                <th>ಹೆಸರು     
                                                </th>
                                                <th>ಹುದ್ದೆ
                                                </th>
                                                <th>ವಿಭಾಗ
                                                </th>


                                                <th>ವೇತನ ಶ್ರೇಣಿ
                                                </th>
                                                <th>ಮೂಲ ವೇತನ
                                                </th>

                                                <th>ಎಜಿಪಿ
                                                </th>
                                                <%--
                                                                <th>ತುಟ್ಟಿ ಭತ್ಯೆ
                                                                </th>
                                                                <th>ಮನೆ ಬಾಡಿಗೆ ಭತ್ಯೆ
                                                                </th>
                                                                <th>ಸಿಸಿಎ
                                                                </th>
                                                                <th>ವಿ. ಭತ್ಯೆ
                                                                </th>
                                                                <th>ವೈ. ಭತ್ಯೆ
                                                                </th>

                                                                <th>ಒಟ್ಟು ವೇತನ
                                                                </th>
                                                                <th>ಒಟ್ಟು ವೇತನದ ಶೇ.85 ರಷ್ಟು
                                                                </th>
                                                                <th>ವರ-ಮಾನ ತೆರಿಗೆ
                                                                </th>


                                                                 <th>ವೃತ್ತಿ  ತೆರಿಗೆ
                                                                </th>
                                                                <th>ಕುಟುಂಬ ಕಲ್ಯಾಣ ನಿಧಿ ವಂತಿಗೆ
                                                                </th>
                                                                <th>ಗುಂಪು ಜೀವ ವಿಮೆ
                                                                </th>
                                                                <th>ಅಂಚೆ ಕಛೇರಿ ಉ ಖಾತೆ
                                                                </th>
                                                                <th>ಜೆಸಿಇ ಕಲ್ಯಾಣ ನಿಧಿ
                                                                </th>
                                                                <th>ಭವಿಷ್ಯ ನಿಧಿ
                                                                </th>
                                                                <th>ಭವಿಷ್ಯ ನಿಧಿ
                                                                </th>
                                                                 <th>ಮನೆ ಸಾಲ 
                                                                </th>
                                                                <th> ನಿವೃತ್ತ ಸಿಬ್ಬಂದಿ  ಕೊಡುಗೆ
                                                                </th>
                                                                <th>ಕೊಡಗು ಸಂತ್ರಸ್ತರ ಕೊಡುಗೆ
                                                                </th>
                                                                <th>ವೈದ್ಯಕೀಯ ವಿಮೆ
                                                                </th>
                                                                <th>ಸಿಬ್ಬಂದಿಯವರಿಗೆ ವಿತರಿಸಬೇಕಾದ ಸರ್ಕಾರದ ಪಾಲಿನ ಶೇ.85 ರಷ್ಟು ವೇತನ
                                                                </th>
                                                                <th>ಸಿಬ್ಬಂದಿ ಬ್ಯಾಂಕ್ ಖಾತೆ ಸಂಖ್ಯೆ & ಶಾಖೆ -ಎಸ್.ಬಿ.ಐ
                                                                </th>--%>


                                                <th>
                                                    <asp:Label ID="lblEH1" runat="server"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="lblEH2" runat="server"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="lblEH3" runat="server"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="lblEH4" runat="server"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="lblEH5" runat="server"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="lblEH6" runat="server"></asp:Label>
                                                </th>
                                                <%-- <th>
                                                                                    <asp:Label ID="lblEH7" runat="server"></asp:Label>
                                                                                </th>--%>
                                                <th>
                                                    <asp:Label ID="lblEH8" runat="server"></asp:Label>
                                                </th>
                                                <%--<th>
                                                                                    <asp:Label ID="lblEH9" runat="server"></asp:Label>
                                                                                </th>
                                                                                <th>
                                                                                    <asp:Label ID="lblEH10" runat="server"></asp:Label>
                                                                                </th>
                                                                                <th>
                                                                                    <asp:Label ID="lblEH11" runat="server"></asp:Label>
                                                                                </th>--%>
                                                <%-- <th>
                                                                                    <asp:Label ID="lblEH12" runat="server"></asp:Label>
                                                                                </th>
                                                                                <th>
                                                                                    <asp:Label ID="lblEH13" runat="server"></asp:Label>
                                                                                </th>
                                                                                <th>
                                                                                    <asp:Label ID="lblEH14" runat="server"></asp:Label>
                                                                                </th>
                                                                                <th>
                                                                                    <asp:Label ID="lblEH15" runat="server"></asp:Label>
                                                                                </th>--%>
                                                <th>ಒಟ್ಟು ವೇತನ
                                                                   <%-- Gross Total--%>
                                                </th>
                                                <th>
                                                    <%--85% GROSS--%>
                                                                    ಒಟ್ಟು ವೇತನದ ಶೇ.85 ರಷ್ಟು
                                                </th>
                                                <th>ಒಟ್ಟು ವೇತನದ ಶೇ.15 ರಷ್ಟು
                                                </th>
                                                <th>
                                                    <asp:Label ID="lblDH1" runat="server"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="lblDH2" runat="server"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="lblDH3" runat="server"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="lblDH4" runat="server"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="lblDH5" runat="server"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="lblDH6" runat="server"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="lblDH7" runat="server"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="lblDH8" runat="server"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="lblDH9" runat="server"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="lblDH10" runat="server"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="lblDH11" runat="server"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="lblDH12" runat="server"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="lblDH13" runat="server"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="lblDH14" runat="server"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="lblDH15" runat="server"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="lblDH16" runat="server"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="lblDH17" runat="server"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="lblDH18" runat="server"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="lblDH19" runat="server"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="lblDH20" runat="server"></asp:Label>
                                                </th>
                                                <%--     <th>
                                                                                    <asp:Label ID="lblDH21" runat="server"></asp:Label>
                                                                                </th>
                                                                                <th>
                                                                                    <asp:Label ID="lblDH22" runat="server"></asp:Label>
                                                                                </th>
                                                                                <th>
                                                                                    <asp:Label ID="lblDH23" runat="server"></asp:Label>
                                                                                </th>
                                                                                <th>
                                                                                    <asp:Label ID="lblDH24" runat="server"></asp:Label>
                                                                                </th>
                                                                                <th>
                                                                                    <asp:Label ID="lblDH25" runat="server"></asp:Label>
                                                                                </th>
                                                                                <th>
                                                                                    <asp:Label ID="lblDH26" runat="server"></asp:Label>
                                                                                </th>
                                                                                <th>
                                                                                    <asp:Label ID="lblDH27" runat="server"></asp:Label>
                                                                                </th>
                                                                                <th>
                                                                                    <asp:Label ID="lblDH28" runat="server"></asp:Label>
                                                                                </th>
                                                                                <th>
                                                                                    <asp:Label ID="lblDH29" runat="server"></asp:Label>
                                                                                </th>
                                                                                <th>
                                                                                    <asp:Label ID="lblDH30" runat="server"></asp:Label>
                                                                                </th>--%>
                                                <th>ಒಟ್ಟು ಕಡಿತಗಳು
                                                                    <%--Total Deduction--%>
                                                </th>
                                                <th>ಸಿಬ್ಬಂದಿಯವರಿಗೆ ವಿತರಿಸಬೇಕಾದ ಸರ್ಕಾರದ ಪಾಲಿನ ಶೇ.85 ರಷ್ಟು ವೇತನ
                                                                    <%--Net Salary--%>
                                                </th>
                                                <th>ಸಿಬ್ಬಂದಿ ಬ್ಯಾಂಕ್ ಖಾತೆ ಸಂಖ್ಯೆ & ಶಾಖೆ -ಎಸ್.ಬಿ.ಐ
                                                                    <%--Account Number--%>
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
                                            <%# Container.DataItemIndex + 1%>
                                        </td>
                                        <td>
                                            <%# Eval("PFILENO")%>
                                        </td>
                                        <td>
                                            <%# Eval("NAME")%>
                                        </td>
                                        <td>
                                            <%# Eval("SUBDESIG")%>
                                        </td>

                                        <td>
                                            <%# Eval("SUBDEPT")%>
                                        </td>
                                        <td>
                                            <%# Eval("SCALE")%>
                                        </td>

                                        <td>
                                            <%# Eval("BASIC")%>
                                        </td>

                                        <td>
                                            <%# Eval("GRADEPAY")%>
                                        </td>



                                        <td>
                                            <%# Eval("I1")%>
                                        </td>
                                        <td>
                                            <%# Eval("I2")%>
                                        </td>
                                        <td>
                                            <%# Eval("I3")%>
                                        </td>
                                        <td>
                                            <%# Eval("I4")%>
                                        </td>
                                        <td>
                                            <%# Eval("I5")%>
                                        </td>
                                        <td>
                                            <%# Eval("I6")%>
                                        </td>
                                        <%--   <td>
                                                                        <%# Eval("I7")%>
                                                                    </td>--%>
                                        <td>
                                            <%# Eval("I8")%>
                                        </td>
                                        <%--<td>
                                                                        <%# Eval("I9")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("I10")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("I11")%>
                                                                    </td>--%>
                                        <%--<td>
                                                                        <%# Eval("I12")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("I13")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("I14")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("I15")%>
                                                                    </td>--%>
                                        <td>
                                            <%# Eval("GS")%>
                                        </td>
                                        <td>
                                            <%# Eval("85%Gross")%>
                                        </td>
                                        <td>
                                            <%# Eval("15%Gross")%>
                                        </td>
                                        <td>
                                            <%# Eval("D1")%>
                                        </td>
                                        <td>
                                            <%# Eval("D2")%>
                                        </td>
                                        <td>
                                            <%# Eval("D3")%>
                                        </td>
                                        <td>
                                            <%# Eval("D4")%>
                                        </td>
                                        <td>
                                            <%# Eval("D5")%>
                                        </td>
                                        <td>
                                            <%# Eval("D6")%>
                                        </td>
                                        <td>
                                            <%# Eval("D7")%>
                                        </td>
                                        <td>
                                            <%# Eval("D8")%>
                                        </td>
                                        <td>
                                            <%# Eval("D9")%>
                                        </td>
                                        <td>
                                            <%# Eval("D10")%>
                                        </td>
                                        <td>
                                            <%# Eval("D11")%>
                                        </td>
                                        <td>
                                            <%# Eval("D12")%>
                                        </td>
                                        <td>
                                            <%# Eval("D13")%>
                                        </td>
                                        <td>
                                            <%# Eval("D14")%>
                                        </td>
                                        <td>
                                            <%# Eval("D15")%>
                                        </td>
                                        <td>
                                            <%# Eval("D16")%>
                                        </td>
                                        <td>
                                            <%# Eval("D17")%>
                                        </td>
                                        <td>
                                            <%# Eval("D18")%>
                                        </td>
                                        <td>
                                            <%# Eval("D19")%>
                                        </td>
                                        <td>
                                            <%# Eval("D20")%>
                                        </td>
                                        <%--   <td>
                                                                        <%# Eval("D21")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("D22")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("D23")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("D24")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("D25")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("D26")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("D27")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("D28")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("D29")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("D30")%>
                                                                    </td>--%>
                                        <td>
                                            <%# Eval("TOT_DED")%>
                                        </td>
                                        <td>
                                            <%# Eval("NET_PAY")%>
                                        </td>
                                        <td><%# Eval("BANKACC_NO")%></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </asp:Panel>
                    </div>
                <div class="col-12" id="div_ExportToExcel15" runat="server" visible="false">
                    <asp:Panel ID="Panel1" runat="server" Visible="false">
                        <div style="background-color: lightskyblue; border: 1px solid black">
                            <asp:Label ID="Label1" runat="server" Text="15% Gross" Style="font-weight: bold; color: #000;"></asp:Label>
                        </div>
                        <asp:ListView ID="grdSelectFieldReport15" runat="server">
                            <EmptyDataTemplate>
                                <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="Click Add New To Enter Earnings And Deducations" />
                            </EmptyDataTemplate>
                            <LayoutTemplate>
                                <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                    <thead class="bg-light-blue">
                                        <tr>
                                            <th>Sr. No.
                                            </th>
                                            <th>Account No
                                            </th>
                                            <th>15%
                                            </th>
                                            <th>Code No
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
                                        <%# Container.DataItemIndex + 1%>
                                    </td>
                                    <td>
                                        <%# Eval("NAME")%>
                                    </td>

                                    <td>
                                        <%# Eval("BANKACC_NO")%>
                                    </td>
                                    <td>
                                        <%# Eval("PerSalary15")%>
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

    <div id="divMsg" runat="server">
    </div>

    <script type="text/javascript" src="https://www.google.com/jsapi">
    </script>
    <script type="text/javascript">
        // Load the Google Transliterate API
        google.load("elements", "1", {
            packages: "transliteration"
        });

        function onLoad() {
            var options = {
                sourceLanguage:
                google.elements.transliteration.LanguageCode.ENGLISH,
                destinationLanguage:
                [google.elements.transliteration.LanguageCode.KANNADA],
                shortcutKey: 'ctrl+e',
                transliterationEnabled: true
            };

            // Create an instance on TransliterationControl with the required
            // options.
            var control =
            new google.elements.transliteration.TransliterationControl(options);

            // Enable transliteration in the textbox with id
            // 'transliterateTextarea'.
            control.makeTransliteratable(['ctl00_ContentPlaceHolder1_txtReportHeadingKannada']);
        }
        google.setOnLoadCallback(onLoad);
    </script>
</asp:Content>

