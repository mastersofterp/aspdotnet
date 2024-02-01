<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/SiteMasterPage.master" CodeFile="LeaveBalanceReport.aspx.cs" Inherits="ESTABLISHMENT_LEAVES_Reports_LeaveBalanceReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
  <%--  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
            <script src="https://cdn.datatables.net/1.10.4/js/jquery.dataTables.min.js"></script>

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

            <script type="text/javascript">
                RunThisAfterEachAsyncPostback();
                Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
            </script>
            <%--<fieldset class="fieldset">--%>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">EMPLOYEE LEAVE BALANCE REPORT</h3>
                        </div>

                        <div class="form-group col-md-1" style="text-align: left">
                            <asp:ImageButton ID="imgBtnBack" runat="server" ImageUrl="~/IMAGES/btnBack.jpg" Width="60px" Height="30px" PostBackUrl="~/PAYROLL/TRANSACTIONS/Pay_UniversalSearch_EmployeeDetail.aspx" Visible="false" />
                        </div>
                        <div class="col-12" runat="server" visible="false">
                            <div class="row">
                                <div class="col-12">
                                    <div id="divnote" runat="server">
                                        Note <b>:</b> <span style="color: #FF0000">Please Select The College Name To Get The Employee List.</span>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="box-body">
                            <div class="panel-body">
                                <div class="box-body">
                                    <div class="col-12">
                                        <%--<div class="row">--%>
                                            <asp:Panel ID="pnlInfo" runat="server">
                                                <%-- Note <b>:</b> <span style="color: #FF0000">* Marked Is Mandatory !</span><br />--%>

                                                <div class="col-12">
                                                    <div class="row">
                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="trStaffType" runat="server" visible="false">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Staff Type</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlStaffType" AppendDataBoundItems="true" runat="server"
                                                                CssClass="form-control" data-select2-enable="true">
                                                            <asp:ListItem Selected="True" Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvDept" runat="server" ControlToValidate="ddlStaffType"
                                                                Display="None" ErrorMessage="Please Select Staff Type" ValidationGroup="Holiday"
                                                                SetFocusOnError="True" InitialValue="0">
                                                            </asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="trcollege" runat="server">
                                                            <div class="label-dynamic">
                                                                <%--<sup>* </sup>--%>
                                                                <label>College</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlCollege" AppendDataBoundItems="true" runat="server" data-select2-enable="true"
                                                                CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlCollege"
                                                                Display="None" ErrorMessage="Please Select College" ValidationGroup="Holiday"
                                                                SetFocusOnError="True" InitialValue="0">
                                                            </asp:RequiredFieldValidator>--%>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="tr1" runat="server" visible="false">
                                                            <div class="label-dynamic">
                                                                <sup></sup>
                                                                <label>Staff Type</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlStaff" AppendDataBoundItems="true" runat="server" data-select2-enable="true"
                                                                CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlStaff_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                            <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlStaff"
                                                                Display="None" ErrorMessage="Please Select Staff Type" ValidationGroup="Holiday"
                                                                SetFocusOnError="True" InitialValue="0">
                                                            </asp:RequiredFieldValidator>--%>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="trdept" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup></sup>
                                                                <label>Department</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddldept" AppendDataBoundItems="true" AutoPostBack="true" runat="server"
                                                                CssClass="form-control" OnSelectedIndexChanged="ddldept_SelectedIndexChanged" data-select2-enable="true">
                                                            </asp:DropDownList>

                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="trsearchtype" runat="server" style="padding-top: 10px">
                                                            <label>Search Type</label>
                                                            <asp:RadioButtonList ID="rblSelect" runat="server" RepeatDirection="Horizontal" AutoPostBack="True"
                                                                OnSelectedIndexChanged="rblSelect_SelectedIndexChanged">
                                                                <asp:ListItem Value="0" Text="All Employee" Selected="True"></asp:ListItem>
                                                                <asp:ListItem Value="1" Text="Particular Employee"></asp:ListItem>
                                                            </asp:RadioButtonList>

                                                        </div>

                                                         <div class="form-group col-lg-3 col-md-6 col-12" id="trEmp" runat="server" visible="false" >
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Select Employee</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlEmployee" runat="server" AppendDataBoundItems="true" CssClass="form-control" TabIndex="5" data-select2-enable="true"
                                                                ToolTip="Select Employee">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvEmp" runat="server" ControlToValidate="ddlEmployee" InitialValue="0"
                                                                Display="None" ErrorMessage="Please Select Employee" SetFocusOnError="true" ValidationGroup="Holiday" >
                                                            </asp:RequiredFieldValidator>
                                                         </div>

                                                      <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                          <sup>* </sup>
                                                          <label>Period</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlPeriod" AppendDataBoundItems="true" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="4">
                                                           <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvperiod" runat="server" ControlToValidate="ddlPeriod"
                                                            Display="None" ErrorMessage="Please Select Period" ValidationGroup="Holiday" InitialValue="0"></asp:RequiredFieldValidator>
                                                      </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Year</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control" TabIndex="5" AutoPostBack="true" data-select2-enable="true"
                                                                OnSelectedIndexChanged="ddlYear_SelectedIndexChanged" ToolTip="Select Year">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlYear"
                                                                Display="None" ErrorMessage="Please Select Year" ValidationGroup="Holiday" InitialValue="0"></asp:RequiredFieldValidator>
                                                        </div>
                                                        <%-- </div>--%>
                                                           
                                                        <%-- <div class="col-md-12">--%>
                                                        <div id="Div1" class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>From Date</label>
                                                            </div>
                                                            <div class="input-group">
                                                                <asp:TextBox ID="txtFdate" runat="server" CssClass="form-control"
                                                                    AutoPostBack="true"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvholidayDt" runat="server" ControlToValidate="txtFdate"
                                                                    Display="None" ErrorMessage="Please Enter From Date" ValidationGroup="Holiday"
                                                                    SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                                <div class="input-group-addon">
                                                                    <asp:Image ID="imgToDate" runat="server" ImageUrl="~/IMAGES/calendar.png" Style="cursor: pointer" />
                                                                </div>
                                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                                    PopupButtonID="imgToDate" TargetControlID="txtFdate">
                                                                </ajaxToolKit:CalendarExtender>
                                                                <ajaxToolKit:MaskedEditExtender ID="meFromDate" runat="server" TargetControlID="txtFdate"
                                                                    Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                                    AcceptNegative="Left" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate">
                                                                </ajaxToolKit:MaskedEditExtender>
                                                                <ajaxToolKit:MaskedEditValidator ID="mevFromDate" runat="server" ControlExtender="meFromDate"
                                                                    ControlToValidate="txtFdate" InvalidValueMessage="From Date is Invalid (Enter -dd/mm/yyyy Format)"
                                                                    Display="None" TooltipMessage="Please Enter From Date" EmptyValueBlurredText="Empty"
                                                                    InvalidValueBlurredMessage="Invalid Date" ValidationGroup="Holiday" SetFocusOnError="True" IsValidEmpty="false"
                                                                    InitialValue="__/__/____" />
                                                            </div>
                                                        </div>
                                                        <div id="Div2" class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>To Date</label>
                                                            </div>
                                                            <div class="input-group">
                                                                <asp:TextBox ID="txtDate" runat="server" CssClass="form-control"
                                                                     AutoPostBack="true"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDate"
                                                                    Display="None" ErrorMessage="Please Enter To Date" ValidationGroup="Holiday"
                                                                    SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                                <div class="input-group-addon">
                                                                    <asp:Image ID="imgDate" runat="server" ImageUrl="~/IMAGES/calendar.png" Style="cursor: pointer" />
                                                                </div>
                                                                <ajaxToolKit:CalendarExtender ID="calDate" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                                    PopupButtonID="imgDate" TargetControlID="txtDate">
                                                                </ajaxToolKit:CalendarExtender>
                                                                <ajaxToolKit:MaskedEditExtender ID="meeTodt" runat="server" AcceptNegative="Left"
                                                                    DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                                                    MessageValidatorTip="true" TargetControlID="txtDate" />
                                                                <ajaxToolKit:MaskedEditValidator ID="mevToDate" runat="server" ControlExtender="meeTodt"
                                                                    ControlToValidate="txtDate" InvalidValueMessage="To Date is Invalid (Enter -dd/MM/yyyy Format)"
                                                                    Display="None" TooltipMessage="Please Enter To Date" EmptyValueBlurredText="Empty"
                                                                    InvalidValueBlurredMessage="Invalid Date" ValidationGroup="Holiday" SetFocusOnError="True" IsValidEmpty="false" InitialValue="__/__/____" />
                                                                <asp:CompareValidator ID="CampCalExtDate" runat="server" ControlToValidate="txtDate"
                                                                    CultureInvariantValues="true" Display="None" ErrorMessage="To Date Must Be Equal To Or Greater Than From Date." Operator="GreaterThanEqual" SetFocusOnError="True" Type="Date"
                                                                    ValidationGroup="Holiday" ControlToCompare="txtFdate" />
                                                            </div>
                                                        </div>        
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                            <div class="col-md-12 form-group text-center">
                                                <asp:Button ID="btnReport" runat="server"
                                                    Text="Report" OnClick="btnReport_Click" ValidationGroup="Holiday" CssClass-="btn btn-info" />
                                                <asp:Button ID="btnExport" runat="server" CausesValidation="False"
                                                    OnClick="btnExport_Click" Text="Export" CssClass-="btn btn-info" />
                                                <asp:Button ID="btnCancel" runat="server" CausesValidation="False"
                                                    Text="Cancel" OnClick="btnCancel_Click" CssClass-="btn btn-warning" />
                                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Holiday"
                                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                            </div>
                                            <div class="col-md-12 form-group text-center">
                                                <asp:Label ID="lblHead" runat="server" Visible="False" Style="text-align: center"></asp:Label>
                                            </div>
                                            
                                        <%--</div>--%>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
      <%--  </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnReport" />
            <asp:PostBackTrigger ControlID="btnExport" />
         </Triggers>
    </asp:UpdatePanel>--%>
    <div id="divMsg" runat="server">
    </div>
</asp:Content>



