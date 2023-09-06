<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Pay_Monthly_Regular_deducation.aspx.cs" Inherits="PayRoll_Pay_Monthly_Regular_deducation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div2" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">REGULAR DEDUCTIONS</h3>
                </div>
                <div class="box-body">
                        <div class="col-12">
	                            <div class="row">
		                            <div class="col-12">
		                            <div class="sub-heading">
				                            <h5>Select Criteria</h5>
			                            </div>
		                            </div>
	                            </div>
                            </div>
                    <asp:Panel ID="pnlSelection" runat="server">
                        <div class="col-12">
                            <div class="row">
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <label>Order By</label>
                                    </div>
                                    <asp:DropDownList ID="ddlorderby" AppendDataBoundItems="true" runat="server" CssClass="form-control" TabIndex="1" data-select2-enable="true"
                                        ToolTip="Select Order By"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlorderby_SelectedIndexChanged">
                                        <asp:ListItem Value="3" Selected="True">Employee Code</asp:ListItem>
                                        <asp:ListItem Value="2">Name</asp:ListItem>
                                        <asp:ListItem Value="1">IdNo</asp:ListItem>

                                        <asp:ListItem Value="4">Sequence Number</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>College Name</label>
                                    </div>
                                    <asp:DropDownList ID="ddlCollege" AppendDataBoundItems="true" runat="server" CssClass="form-control" TabIndex="2" data-select2-enable="true"
                                        ToolTip="Select College Name"
                                        AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvddlCollege" runat="server" ControlToValidate="ddlCollege"
                                        Display="None" SetFocusOnError="true" ErrorMessage="Please Select College Name"
                                        ValidationGroup="ADD" InitialValue="0"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Employee Name</label>
                                    </div>
                                    <asp:DropDownList ID="ddlEmployee" AppendDataBoundItems="true" runat="server" CssClass="form-control" TabIndex="3" 
                                        ToolTip="Select Employee Name"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvddlEmployee" runat="server" ControlToValidate="ddlEmployee"
                                        Display="None" SetFocusOnError="true" ErrorMessage="Please Select Employee Name"
                                        ValidationGroup="ADD" InitialValue="0"></asp:RequiredFieldValidator>
                                    
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <label></label>
                                    </div>
                                    <asp:CheckBox ID="chkStopInstall" runat="server" AutoPostBack="true" OnCheckedChanged="chkStopInstall_CheckedChanged"
                                        ValidationGroup="ADD" Text="Show stop regular deduction" />
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="pnlEmpDetails" runat="server">
                        <div class="col-12">
                            <div class="row">
                                <div class="col-12">
                                    <div class="sub-heading">
                                        <h5>Employee Details</h5>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-12">
                            <div class="row">
                                <div class="form-group col-lg-3 col-md-6 col-12">
								    <div class="label-dynamic">
									    <label>ID No & Name</label>
								    </div>
                                    <asp:Label ID="lblIdnoName" runat="server" Font-Bold="true" Text="101-Sanjay"></asp:Label>
							    </div>
                                
                                <div class="form-group col-lg-3 col-md-6 col-12">
								    <div class="label-dynamic">
									    <label>Designation</label>
								    </div>
                                    <asp:Label ID="lblDesignation" runat="server" Font-Bold="true" Text="SoftWare Developer"></asp:Label>
							    </div>
                                
                                <div class="form-group col-lg-3 col-md-6 col-12">
								    <div class="label-dynamic">
									    <label>Department</label>
								    </div>
                                    <asp:Label ID="lblDepartment" runat="server" Font-Bold="true" Text=".NET"></asp:Label>
							    </div>
                            </div>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="pnldeducationentry" runat="server">
                        <div class="col-12">
	                        <div class="row">
		                        <div class="col-12">
		                        <div class="sub-heading">
				                        <h5>Select Pay Head For Deduction</h5>
			                        </div>
		                        </div>
	                        </div>
                        </div>

                        <div class="col-12">
                            <div class="row">
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Pay Head</label>
                                    </div>
                                    <asp:DropDownList ID="ddlPayhead" AppendDataBoundItems="true" runat="server" CssClass="form-control" TabIndex="4" ToolTip="Select Pay Head" data-select2-enable="true"
                                        OnSelectedIndexChanged="ddlPayhead_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Value="0">Please Select Payhead</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvddlPayhead" runat="server" ControlToValidate="ddlPayhead"
                                        Display="None" SetFocusOnError="true" ErrorMessage="Please Select Payhead" ValidationGroup="payroll"
                                        InitialValue="0"></asp:RequiredFieldValidator>
                                    <asp:CheckBox ID="chkStop" CssClass="legendPay" runat="server" Text="Stop" />
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <label>Installment For Sub Payhead</label>
                                    </div>
                                    <asp:DropDownList ID="ddlSubpayhead" AppendDataBoundItems="true" runat="server" CssClass="form-control" TabIndex="5" ToolTip="Select Installment For Sub Payhead" data-select2-enable="true">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Monthly Deducted Amt</label>
                                    </div>
                                    <asp:TextBox ID="txtMonthlyDedAmt" runat="server" MaxLength="9" CssClass="form-control" TabIndex="6" ToolTip="Enter Monthly Deducted Amt" onkeyup="return ValidateNumeric(this);"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rvfMonthlyDedAmt" runat="server" ControlToValidate="txtMonthlyDedAmt"
                                        Display="None" SetFocusOnError="true" ErrorMessage="Please Enter Monthly Deducted Amt"
                                        ValidationGroup="payroll"></asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Bank</label>
                                    </div>
                                    <asp:DropDownList ID="ddlBank" AppendDataBoundItems="true" runat="server" CssClass="form-control" TabIndex="7" ToolTip="Select Bank" data-select2-enable="true">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvddlBank" runat="server" ControlToValidate="ddlBank"
                                        Display="None" SetFocusOnError="true" ErrorMessage="Please Select Bank" ValidationGroup="payroll"
                                        InitialValue="0"></asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Bank Place</label>
                                    </div>
                                    <asp:DropDownList ID="ddlBankPlace" AppendDataBoundItems="true" runat="server" ToolTip="Select Bank Place" CssClass="form-control" TabIndex="8" data-select2-enable="true">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvBankPlace" runat="server" ControlToValidate="ddlBankPlace"
                                        Display="None" SetFocusOnError="true" ErrorMessage="Please Select Bank Place"
                                        ValidationGroup="payroll" InitialValue="0"></asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12" id="tdPolicyno" visible="false" runat="server">
                                    <div class="label-dynamic">
                                        <label>Policy.No</label>
                                    </div>
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12" id="tdRdNo" visible="false" runat="server">
                                    <div class="label-dynamic">
                                        <label>Rd.No</label>
                                    </div>
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12" id="tdAccountNO" runat="server">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Account No/Policy.No</label>
                                    </div>
                                    <asp:TextBox ID="txtAccNo" runat="server" MaxLength="20" CssClass="form-control" TabIndex="9" ToolTip="Enter Account No"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rvfAccNo" runat="server" ControlToValidate="txtAccNo"
                                        Display="None" SetFocusOnError="true" ErrorMessage="Please Enter Account No "
                                        ValidationGroup="payroll"></asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <label>Remarks</label>
                                    </div>
                                    <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control" TabIndex="10" ToolTip="Enter Remarks" TextMode="MultiLine" />
                                </div>
                            </div>
                        </div>
                                        
                        <div class="col-md-12 btn-footer">
                            <asp:Button ID="butSubmit" runat="server" OnClick="btnSave_Click" Text="Submit" ValidationGroup="payroll"
                                TabIndex="11" ToolTip="Click To Save" CssClass="btn btn-primary" />
                            <asp:Button ID="butCancel" runat="server" OnClick="btnCancel_Click" Text="Cancel"
                                CssClass="btn btn-warning" TabIndex="12" ToolTip="Click To Reset" />
                            <asp:ValidationSummary ID="vsSelection" runat="server" DisplayMode="List" ShowMessageBox="true"
                                ShowSummary="false" ValidationGroup="payroll" />
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="pnlList" runat="server">
                        <div class="col-12 btn-footer">
                            <asp:LinkButton ID="btnAdd" runat="server" SkinID="LinkAddNew" TabIndex="13" ToolTip="Click Add New To Enter Installment Details"
                                ValidationGroup="ADD" OnClick="btnAdd_Click" CssClass="btn btn-primary" Text="Add New"></asp:LinkButton>
                            <asp:ValidationSummary ID="Vadd" runat="server" DisplayMode="List" ShowMessageBox="true"
                                ShowSummary="false" ValidationGroup="ADD" />
                        </div>
                        <div class="col-12">
                            <asp:ListView ID="lvMonthlyIstallment" runat="server">
                                <EmptyDataTemplate>
                                    <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="" />
                                </EmptyDataTemplate>
                                <LayoutTemplate>
                                        <div class="sub-heading">
	                                        <h5>Monthly Installment Amount Entry</h5>
                                        </div>
                                        <table class="table table-striped table-bordered nowrap display" style="width:100%">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>Action
                                                    </th>
                                                    <th>Account/Policy Number
                                                    </th>
                                                    <th>PayHead
                                                    </th>
                                                    <th>Stop
                                                    </th>
                                                    <th>Monthly Amt
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
                                        <td class="text-center">
                                            <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("INO") %>'
                                                AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />&nbsp;
                                                    <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.gif" CommandArgument='<%# Eval("INO") %>'
                                                        AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                                        OnClientClick="showConfirmDel(this); return false;" Visible="false" />
                                        </td>
                                        <td>
                                            <%# Eval("ACCNO")%>
                                        </td>
                                        <td>
                                            <%# Eval("PAYSHORT")%>
                                        </td>

                                        <td>
                                            <asp:CheckBox ID="chkSelect" runat="server" ToolTip='<%#Eval("INO")%>' Checked='<%#(Convert.ToInt32(Eval("STOP"))==1 ? true : false)%>' />
                                        </td>
                                        <td>
                                            <%-- <%# Eval("MONAMT")%>--%>

                                            <asp:TextBox ID="txtMonAmt" runat="server" Text='<%# Eval("MONAMT")%>' Width="220px" MaxLength="10"></asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftb1" runat="server"
                                                TargetControlID="txtMonAmt"
                                                FilterType="Custom"
                                                FilterMode="ValidChars"
                                                ValidChars="1234567890.">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                        </td>
                                    </tr>
                                </ItemTemplate>

                            </asp:ListView>

                            <div class="col-12" style="text-align: right" id="div_footer" runat="server" visible="false">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Monthly Amount</label>
                                        </div>
                                        <asp:Label ID="lblSumMonAmount" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            
                            <div class="vista-grid_datapager text-center d-none" id="div_Pager" runat="server" visible="true">
                                <asp:DataPager ID="dpPager" runat="server" PagedControlID="lvMonthlyIstallment" PageSize="10"
                                    OnPreRender="dpPager_PreRender">
                                    <Fields>
                                        <asp:NextPreviousPagerField FirstPageText="<<" PreviousPageText="<" ButtonType="Link"
                                            RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="true" ShowPreviousPageButton="true"
                                            ShowLastPageButton="false" ShowNextPageButton="false" />
                                        <asp:NumericPagerField ButtonType="Link" ButtonCount="7" CurrentPageLabelCssClass="current" />
                                        <asp:NextPreviousPagerField LastPageText=">>" NextPageText=">" ButtonType="Link"
                                            RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="false" ShowPreviousPageButton="false"
                                            ShowLastPageButton="true" ShowNextPageButton="true" />
                                    </Fields>
                                </asp:DataPager>
                            </div>
                        </div>
                        <div class="col-12 btn-footer">
                            <asp:Button ID="btnBulkSave" runat="server" TabIndex="14" OnClick="btnBulkSave_Click"
                                CssClass="btn btn-primary" Text="Save"></asp:Button>
                            <asp:Button ID="btnCancle" runat="server" TabIndex="15" OnClick="btnCancle_Click"
                                CssClass="btn btn-warning" Text="cancel"></asp:Button>
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="true"
                                ShowSummary="false" ValidationGroup="ADD" />
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>
   

    <script type="text/javascript">

        //Calculating the total amount
        function totalamount(val) {
            if (ValidateNumeric(val)) {
                var txtMonthlyDedAmt = document.getElementById("ctl00_ContentPlaceHolder1_txtMonthlyDedAmt");
                var txtTotalAmount = document.getElementById("ctl00_ContentPlaceHolder1_txtTotalAmount");
                var txtOutStandingAmt = document.getElementById("ctl00_ContentPlaceHolder1_txtOutStandingAmt");
                var txtNoofInstPaid = document.getElementById("ctl00_ContentPlaceHolder1_txtNoofInstPaid");
                txtTotalAmount.value = Number(val.value) * Number(txtMonthlyDedAmt.value);
                txtNoofInstPaid.value = 0;
                txtOutStandingAmt.value = txtTotalAmount.value;
            }
        }


        function ValidateNumeric(txt) {
            if (isNaN(txt.value)) {
                txt.value = txt.value.substring(0, (txt.value.length) - 1);
                txt.value = "";
                txt.focus();
                alert("Only Numeric Characters alloewd");
                return false;
            }
            else {
                return true;
            }

        }
    </script>
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
