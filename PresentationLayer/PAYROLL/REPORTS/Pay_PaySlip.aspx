<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Pay_PaySlip.aspx.cs" Inherits="PayRoll_Pay_PaySlip" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 <link href="../plugins/multiselect/bootstrap-multiselect.css" rel="stylesheet" />
 <link href='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.css") %>' rel="stylesheet" />
  <script src='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.js") %>'></script>
     <style>
        .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>

    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">EMPLOYEE PAY SLIP</h3>
                        </div>
                        <form role="form">
                            <div class="box-body">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="sub-heading">
                                                <h5>Employee PaySlip Information Report</h5>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <asp:Panel ID="pnl" runat="server">
                                    <div class="col-12">
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Month / Year</label>
                                                </div>
                                                <asp:DropDownList ID="ddlMonthYear" runat="server" CssClass="form-control" AppendDataBoundItems="True" data-select2-enable="true"
                                                    TabIndex="1" ToolTip="Select Month/Year" OnSelectedIndexChanged="ddlMonthYear_SelectedIndexChanged" AutoPostBack="true">
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
                                                <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control" AppendDataBoundItems="true" data-select2-enable="true"
                                                    AutoPostBack="true" TabIndex="2" ToolTip="Select College"
                                                    OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>

                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RFVCollege" runat="server" SetFocusOnError="true" ControlToValidate="ddlCollege" Display="None"  ValidationGroup="Payroll"
                                                    ErrorMessage="Please Select College" InitialValue="0"
                                                    ></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false">
                                                <div class="label-dynamic">
                                                    <%--<sup>* </sup>--%>
                                                    <label>Staff</label>
                                                </div>
                                                <asp:DropDownList ID="ddlStaffNo" runat="server" ToolTip="Select Staff" CssClass="form-control" AppendDataBoundItems="True" data-select2-enable="true"
                                                    TabIndex="3" AutoPostBack="true" OnSelectedIndexChanged="ddlStaffNo_SelectedIndexChanged">
                                                </asp:DropDownList>
                                               <%-- <asp:RequiredFieldValidator ID="rfvStaffNo" runat="server" SetFocusOnError="true"
                                                    ControlToValidate="ddlStaffNo" Display="None" ErrorMessage="Please Select Scheme"
                                                    ValidationGroup="Payroll" InitialValue="0"></asp:RequiredFieldValidator>--%>
                                            </div>

                                      <div class="form-group col-lg-3 col-md-6 col-12" id="divmaxnooffile" runat="server" visible="true">
                                        <div class="label-dynamic">
                                            <label>Scheme/Staff</label>
                                        </div>
                                           <asp:ListBox ID="ddlStaffNo1" runat="server" AppendDataBoundItems="true"  TabIndex="11"  CssClass="form-control multi-select-demo" SelectionMode="Multiple" OnSelectedIndexChanged="ddlStaffNo1_SelectedIndexChanged" AutoPostBack="true"></asp:ListBox>  
                                    </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12" style="display:none;">
                                                <div class="label-dynamic">
                                                    <label>Staff</label>
                                                </div>
                                                <asp:DropDownList ID="ddlEmployeeType" runat="server" CssClass="form-control" data-select2-enable="true"
                                                    AppendDataBoundItems="True" TabIndex="4" ToolTip="Select Staff">
                                                </asp:DropDownList>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12" id="trrbl" runat="server">
                                                <div class="label-dynamic">
                                                    <label></label>
                                                </div>
                                                <asp:RadioButton ID="rdoSelectEmployee" runat="server" Checked="true" Text="Select Employee"
                                                    GroupName="A" TabIndex="5" onclick="DisableDropDownList(false);" />
                                                <asp:RadioButton ID="rdoAllEmployee" runat="server" Text="All Employee" GroupName="A"
                                                    onclick="DisableDropDownList(true);" TabIndex="6" />
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>Employee</label>
                                                </div>
                                                <asp:DropDownList ID="ddlEmployeeNo" runat="server" AppendDataBoundItems="True" TabIndex="7" data-select2-enable="true"
                                                    CssClass="form-control" ToolTip="Select Employee">
                                                </asp:DropDownList>

                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>Salary Certificate Issued to</label>
                                                </div>
                                                <asp:TextBox ID="txtBankName" runat="server" CssClass="form-control" ToolTip="Enter Salary Certificate Issued to " TabIndex="8"></asp:TextBox>
                                            </div>
                                             <div class="form-group col-lg-3 col-md-6 col-12" style="visibility:hidden;">
                                                 <div class="label-dynamic">
                                                    <label>From Date :</label>
                                                </div>
                                                  <div class="input-group date">
                                                     <div class="input-group-addon">
                                                      <asp:Image ID="imgCalFromDate" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                       </div>
                                                     <asp:TextBox ID="txtFromDate" runat="server" TabIndex="1" CssClass="form-control" ToolTip="Enter From Date" />
                                                     <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                                     TargetControlID="txtFromDate" PopupButtonID="imgCalFromDate" Enabled="true" EnableViewState="true">
                                                     </ajaxToolKit:CalendarExtender>
                                                     <asp:RequiredFieldValidator ID="valFromDate" runat="server" ControlToValidate="txtFromDate"
                                                      Display="None" ErrorMessage="Please enter From Date." SetFocusOnError="true"
                                                      ValidationGroup="Payroll1" />
                                                      <ajaxToolKit:MaskedEditExtender ID="meeFromDate" runat="server" TargetControlID="txtFromDate"
                                                     Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                     AcceptNegative="Left" ErrorTooltipEnabled="true" />
                                                   </div>
                                                 </div>
                                              <div class="form-group col-lg-3 col-md-6 col-12"  style="visibility:hidden;">
                                                 <div class="label-dynamic">
                                                    <label>To Date:</label>
                                                </div>
                                                   <div class="input-group date">
                                                     <div class="input-group-addon">
                                                     <asp:Image ID="imgCalToDate" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                      </div>
                                                      <asp:TextBox ID="txtToDate" runat="server" TabIndex="2" CssClass="form-control" ToolTip="Enter To Date" />
                                                       <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                                       TargetControlID="txtToDate" PopupButtonID="imgCalToDate" Enabled="true" EnableViewState="true">
                                                      </ajaxToolKit:CalendarExtender>
                                                      <ajaxToolKit:MaskedEditExtender ID="meeToDate" runat="server" TargetControlID="txtToDate"
                                                      Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                       AcceptNegative="Left" ErrorTooltipEnabled="true" />
                                                       <asp:RequiredFieldValidator ID="valToDate" runat="server" ControlToValidate="txtToDate"
                                                        Display="None" ErrorMessage="Please enter To Date." SetFocusOnError="true"
                                                        ValidationGroup="Payroll1" />
                                                     </div>
                                                  </div>
                                             </div>
                                        </div>
                                    
                                </asp:Panel>
                                <div class="col-12 btn-footer">
                                    <asp:ValidationSummary ID="rfvValidationSummary" runat="server" ValidationGroup="Payroll"
                                        DisplayMode="List" ShowMessageBox="true" ShowSummary="False" />
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Payroll1"
                                        DisplayMode="List" ShowMessageBox="true" ShowSummary="False" />
                                    <asp:Button ID="btnShow" runat="server" Text="Show" ToolTip="Click to Show the Report" OnClick="btnShow_Click" CssClass="btn btn-primary"
                                        TabIndex="8" ValidationGroup="Payroll" />
                                    <asp:Button ID="btnSalaryCertificate" runat="server" ToolTip="Click to Show the Salary Certificate Report" Text="Salary Certificate" OnClick="btnSalaryCertificate_Click"
                                        TabIndex="9" ValidationGroup="Payroll" CssClass="btn btn-primary" />
                                    <asp:Button ID="btnAnnualPaySlip" runat="server" Text="Annual Pay Slip" ToolTip="Click to Show the Report" OnClick="btnAnnualPaySlip_Click" CssClass="btn btn-primary"  Visible="false"
                                        TabIndex="8"  ValidationGroup="Payroll1"/>
                                    <asp:Button ID="btnThreeMonthPaySlip" runat="server" Text="Three Month Salary Statement" ToolTip="Click to Show the Report" OnClick="btnThreeMonthPaySlip_Click" CssClass="btn btn-primary"
                                        TabIndex="8" ValidationGroup="Payroll" />
                                    <asp:Button ID="btnSixmonthsPaySlip" runat="server" Text="Six Month Salary Statement" ToolTip="Click to Show the Report" OnClick="btnSixmonthsPaySlip_Click" CssClass="btn btn-primary"
                                        TabIndex="8" ValidationGroup="Payroll" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" ToolTip="Click To Reset" OnClick="btnCancel_Click"
                                        TabIndex="10" />
                                </div>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
            <div id="divMsg" runat="server">
            </div>
              <script type="text/javascript">
                  $(document).ready(function () {
                      $('.multi-select-demo').multiselect({
                          includeSelectAllOption: true,
                          maxHeight: 200
                      });
                  });
                  var parameter = Sys.WebForms.PageRequestManager.getInstance();
                  parameter.add_endRequest(function () {
                      $('.multi-select-demo').multiselect({
                          includeSelectAllOption: true,
                          maxHeight: 200
                      });
                  });
          </script>
            <script>
                function checkout() {
                    var checkBoxes = document.getElementsByClassName('ddlStaffNo1');
                    var nbChecked = 0;
                    for (var i = 0; i < checkBoxes.length; i++) {
                        if (checkBoxes[i].checked) {
                            nbChecked++;
                        };
                    };
                    if (nbChecked == 0) {
                        alert('Please, select at least one Staff/Scheme!');
                        return false;
                    } else {
                        //Do what you need for form submission, if needed...
                    }
                }
    </script>
            <script type="text/javascript">
                function DisableDropDownList(disable) {
                    document.getElementById('ctl00_ContentPlaceHolder1_ddlEmployeeNo').selectedIndex = 0;
                    document.getElementById('ctl00_ContentPlaceHolder1_ddlEmployeeNo').disabled = disable;
                }
            </script>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnShow" />
            <asp:PostBackTrigger ControlID="btnSalaryCertificate" />
            <asp:PostBackTrigger ControlID="btnAnnualPaySlip" />
            <asp:PostBackTrigger ControlID="btnThreeMonthPaySlip" />
            <asp:PostBackTrigger ControlID="btnSixmonthsPaySlip" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
