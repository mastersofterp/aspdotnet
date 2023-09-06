<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Pay_IncrementArrears.aspx.cs" Inherits="PAYROLL_TRANSACTIONS_Pay_IncrementArrears" %>

<%@ Register Assembly="RControl" Namespace="RControl" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updpanel"
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
    <asp:UpdatePanel ID="updpanel" runat="server">
        <ContentTemplate>

            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">ARREARS</h3>
                        </div>

                        <div class="box-body">
                            <asp:Panel ID="pnl" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="sub-heading">
                                                <h5>Arrears Calculation</h5>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>College</label>
                                            </div>
                                            <asp:DropDownList ID="ddlCollege" runat="server" ToolTip="Select College" TabIndex="1" AutoPostBack="true" data-select2-enable="true"
                                                AppendDataBoundItems="true" CssClass="form-control" />
                                            <asp:RequiredFieldValidator ID="rfvCollege" runat="server" ControlToValidate="ddlCollege"
                                                InitialValue="0" Display="None" ErrorMessage="Please Select College "
                                                SetFocusOnError="True" ValidationGroup="emp"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Staff Type</label>
                                            </div>
                                            <asp:RequiredFieldValidator ID="rfvStaff" runat="server" ControlToValidate="ddlCollegeType"
                                                InitialValue="0" Display="None" ErrorMessage="Select Staff Type "
                                                SetFocusOnError="True" ValidationGroup="emp"></asp:RequiredFieldValidator>

                                            <asp:DropDownList ID="ddlCollegeType" runat="server" TabIndex="2" AutoPostBack="true" data-select2-enable="true"
                                                AppendDataBoundItems="true" CssClass="form-control"
                                                OnSelectedIndexChanged="ddlCollegeType_SelectedIndexChanged" />
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Arrears From Date</label>
                                            </div>
                                            <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i id="imgCPTCalFromDate" runat="server" class="fa fa-calendar text-blue"></i>
                                            </div>
                                                <asp:TextBox ID="txtCPTFromDate" runat="server" CssClass="form-control" TabIndex="3" ToolTip="Enter Arrears From Date" />                                  
                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                                    TargetControlID="txtCPTFromDate" PopupButtonID="imgCPTCalFromDate" Enabled="true"
                                                    EnableViewState="true" />
                                                <ajaxToolKit:MaskedEditExtender ID="meeFromDate" runat="server" TargetControlID="txtCPTFromDate"
                                                    MaskType="Date" Mask="99/99/9999">
                                                </ajaxToolKit:MaskedEditExtender>
                                                <asp:RequiredFieldValidator ID="rfvFromDate" runat="server" ControlToValidate="txtCPTFromDate"
                                                    Display="None" ErrorMessage="Please Select From Date" SetFocusOnError="True"
                                                    ValidationGroup="emp"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>

                                        <%--<div class="form-group col-md-10">
                                                            <label>Current Rate:<span style="color: #FF0000">*</span></label>

                                                            <asp:RequiredFieldValidator ID="rfvCurRate" runat="server" ControlToValidate="txtCurRate"
                                                                Display="None" ErrorMessage="Please Enter Current rate" SetFocusOnError="True" ValidationGroup="emp"></asp:RequiredFieldValidator>
                                                            <asp:TextBox ID="txtCurRate" runat="server" CssClass="form-control" TabIndex="6" ToolTip="Enter Current Rate"
                                                                OnTextChanged="txtCurRate_TextChanged" AutoPostBack="false"></asp:TextBox>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FTBCurrentRate" runat="server"
                                                                TargetControlID="txtCurRate"
                                                                FilterType="Custom,Numbers"
                                                                FilterMode="ValidChars"
                                                                ValidChars=".-_ ">
                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                        </div>--%>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Gov. Order no.</label>
                                            </div>
                                            <asp:RequiredFieldValidator ID="rfvCovOrdNo" runat="server" ControlToValidate="txtGovOrdNo"
                                                Display="None" ErrorMessage="Please Enter Gov. Order No."
                                                SetFocusOnError="True" ValidationGroup="emp"></asp:RequiredFieldValidator>
                                            <asp:TextBox ID="txtGovOrdNo" runat="server" CssClass="form-control" MaxLength="25" TabIndex="4" ToolTip="Enter Gov. Order no"></asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftbeGovOrdNumber" runat="server"
                                                TargetControlID="txtGovOrdNo"
                                                FilterType="Custom,Numbers,LowerCaseLetters,UpperCaseLetters"
                                                FilterMode="ValidChars"
                                                ValidChars=".-_ /">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Office Order No.</label>
                                            </div>
                                            <asp:RequiredFieldValidator ID="rfvOffOrdno" runat="server" ControlToValidate="txtOffOrdNo"
                                                Display="None" ErrorMessage="Please Enter Office Order No." SetFocusOnError="True" ValidationGroup="emp"></asp:RequiredFieldValidator>

                                            <asp:TextBox ID="txtOffOrdNo" runat="server" CssClass="form-control" TabIndex="5" ToolTip="Enter Office Order No"></asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FtbeOffOrdNo" runat="server"
                                                TargetControlID="txtOffOrdNo"
                                                FilterType="Custom,Numbers,LowerCaseLetters,UpperCaseLetters"
                                                FilterMode="ValidChars"
                                                ValidChars=".-_ /">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="tremp" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <label>Select Employee</label>
                                            </div>
                                            <asp:DropDownList ID="ddlEmployee" runat="server" TabIndex="6" AutoPostBack="true" ToolTip="Select Employee" data-select2-enable="true"
                                                AppendDataBoundItems="true" CssClass="form-control" />
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>To Date:</label>
                                            </div>
                                            <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i id="imgCPTCalToDate" runat="server" class="fa fa-calendar text-blue"></i>
                                            </div>
                                                <asp:TextBox ID="txtCPTToDate" runat="server" CssClass="form-control" TabIndex="7" ToolTip="Enter To Date" />                                      
                                                <ajaxToolKit:CalendarExtender ID="ceCPTToDate" runat="server" Format="dd/MM/yyyy"
                                                    TargetControlID="txtCPTToDate" PopupButtonID="imgCPTCalToDate" Enabled="true"
                                                    EnableViewState="true" />
                                                <ajaxToolKit:MaskedEditExtender ID="meeToDate" runat="server" TargetControlID="txtCPTToDate"
                                                    MaskType="Date" Mask="99/99/9999">
                                                </ajaxToolKit:MaskedEditExtender>
                                                <asp:RequiredFieldValidator ID="rfvToDate" runat="server" ControlToValidate="txtCPTToDate"
                                                    Display="None" ErrorMessage="Please Select To Date" SetFocusOnError="True" ValidationGroup="emp"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>

                                        <%--<div class="form-group col-md-10">
                                                            <label>Rule:<span style="color: #FF0000">*</span></label>

                                                            <asp:RequiredFieldValidator ID="rfvRule" runat="server" ControlToValidate="ddlRule"
                                                                Display="None" ErrorMessage="Please Select Rule" SetFocusOnError="True" InitialValue="0" ValidationGroup="emp"></asp:RequiredFieldValidator>
                                                            <asp:DropDownList ID="ddlRule" AppendDataBoundItems="true" ToolTip="Select Rule" runat="server" CssClass="form-control" TabIndex="7">
                                                            </asp:DropDownList>
                                                        </div>--%>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Gov. Order Date</label>
                                            </div>
                                            <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i id="imgGovdt" runat="server" class="fa fa-calendar text-blue"></i>
                                            </div>
                                                <asp:TextBox ID="txtGovDt" runat="server" AutoPostBack="true" ToolTip="Enter Gov. Order Date" CssClass="form-control" TabIndex="8" />
                                                
                                                <ajaxToolKit:CalendarExtender ID="ceeGovDt" runat="server" Format="dd/MM/yyyy"
                                                    TargetControlID="txtGovDt" PopupButtonID="imgGovdt" Enabled="true"
                                                    EnableViewState="true" />
                                                <ajaxToolKit:MaskedEditExtender ID="MeeGovDt" runat="server" TargetControlID="txtGovDt"
                                                    MaskType="Date" Mask="99/99/9999">
                                                </ajaxToolKit:MaskedEditExtender>
                                            </div>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Office Order Date</label>
                                            </div>
                                            <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i id="imgoffDt" runat="server" class="fa fa-calendar text-blue"></i>
                                            </div>
                                                <asp:TextBox ID="txtOffDt" runat="server" AutoPostBack="true" ToolTip="Enter Office Order Date" CssClass="form-control" TabIndex="9" />                                               
                                                <ajaxToolKit:CalendarExtender ID="ceeoffDt" runat="server" Format="dd/MM/yyyy"
                                                    TargetControlID="txtOffDt" PopupButtonID="imgoffDt" Enabled="true"
                                                    EnableViewState="true" />
                                                <ajaxToolKit:MaskedEditExtender ID="meeOffDt" runat="server" TargetControlID="txtOffDt"
                                                    MaskType="Date" Mask="99/99/9999">
                                                </ajaxToolKit:MaskedEditExtender>
                                            </div>
                                        </div>

                                        <div class="form-group col-lg-6 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Remark</label>
                                            </div>
                                            <asp:TextBox ID="txtRemark" TextMode="MultiLine" runat="server" ToolTip="Enter Remark" CssClass="form-control" TabIndex="10"></asp:TextBox>
                                        </div>

                                    </div>
                                </div>
                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnShowEmployee" runat="server" OnClick="btnShowEmployee_Click" ToolTip="Click to Show Employees"
                                        TabIndex="11" Text="Show Employees" ValidationGroup="emp" CssClass="btn btn-primary" />
                                    <asp:Button ID="btnCalculate" runat="server" OnClick="btnCalculate_Click" ToolTip="Click to Calculate Arrears"
                                        TabIndex="12" Text="Calculate" ValidationGroup="emp" CssClass="btn btn-primary" Visible="false" />
                                    <asp:Button ID="btnPrint" runat="server" TabIndex="13" Text="Report" ToolTip="Click to Print"
                                        ValidationGroup="emp" CssClass="btn btn-info" Visible="false" />
                                    <asp:Button ID="btnCancel" runat="server" CausesValidation="false" OnClick="btnCancel_Click" ToolTip="Click to Reset"
                                        TabIndex="14" Text="Cancel" CssClass="btn btn-warning" />                                    
                                    <asp:ValidationSummary ID="ValidationSummary1" DisplayMode="List" ShowMessageBox="true"
                                        ShowSummary="false" ValidationGroup="emp" runat="server" />
                                </div>
                                <div class="col-12">
                                    <asp:ListView ID="lvEmployees" runat="server" Visible="false">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Employee List</h5>
                                            </div>
                                            <table id="tblStudents" class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>
                                                            <asp:CheckBox ID="cbHead" runat="server" onclick="totAllSubjects(this)" Visible="true" />Select
                                                        </th>
                                                        <th>Emp. Code
                                                        </th>
                                                        <th>Employee Name
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
                                                    <asp:CheckBox ID="chkRow" runat="server" Font-Bold="true" ForeColor="Green" />
                                                    <asp:HiddenField ID="hidStudentId" runat="server"
                                                        Value='<%# Eval("IDNO")%>' />
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="lblreg" Text='<%# Eval("PFILENO")%>' ToolTip='<%# Eval("PFILENO")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="lblstud" Text='<%# Eval("NAME")%>'></asp:Label>
                                                </td>

                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>


        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript" language="javascript">
        function totAllSubjects(headchk) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.type == 'checkbox') {
                    if (headchk.checked == true)
                        e.checked = true;
                    else
                        e.checked = false;
                }
            }
        }
    </script>
</asp:Content>
