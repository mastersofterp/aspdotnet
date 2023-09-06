<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="PF_loantype.aspx.cs" Inherits="PAYROLL_MASTERS_PF_loantype" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="updMain" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">PF LOAN TYPE MASTER</h3>
                        </div>

                        <div class="box-body">
                            <asp:Label ID="lblStatus" runat="server" SkinID="Errorlbl" />
                                <%-- <legend class="legendPay">Add/Edit PF Laon Type</legend>--%>
                                <%-- <h4 class="box-title">Add/Edit PF Laon Type</h4>--%>
                                <asp:Panel ID="pnlPfMaster" runat="server">
                                    <div class="col-12">
	                                    <div class="row">
		                                    <div class="col-12">
		                                    <div class="sub-heading">
				                                    <h5>Add/Edit PF Laon Type</h5>
			                                    </div>
		                                    </div>
	                                    </div>
                                    </div>
                                    <div class="col-12">
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
								                <div class="label-dynamic">
									                <sup>* </sup>
									                <label>Provident Fund</label>
								                </div>
                                                 <asp:DropDownList ID="ddlPF" runat="server" AppendDataBoundItems="true" CssClass="form-control" ToolTip="Select Provident Fund" data-select2-enable="true"
                                                    TabIndex="1">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvddlPF" runat="server" InitialValue="0" ControlToValidate="ddlPF"
                                                    Display="None" ErrorMessage="Please Select PF" ValidationGroup="payroll"></asp:RequiredFieldValidator>
							                </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
								                <div class="label-dynamic">
									                <sup>* </sup>
									                <label>Loan Full Name</label>
								                </div>
                                                 <asp:TextBox ID="txtFullName" ToolTip="Enter Loan Full Name" runat="server" MaxLength="50" TabIndex="2" CssClass="form-control" onkeypress="return lettersOnly(event);" />
                                                <asp:RequiredFieldValidator ID="rfvtxtFullName" runat="server" ControlToValidate="txtFullName"
                                                    Display="None" ErrorMessage="Please Enter Full Name" ValidationGroup="payroll"></asp:RequiredFieldValidator>
							                </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
								                <div class="label-dynamic">
									                <sup>* </sup>
									                <label>Loan Short Name</label>
								                </div>
                                                  <asp:TextBox ID="txtShortName" ToolTip="Enter Loan Short Name" runat="server" MaxLength="20" CssClass="form-control" TabIndex="3"  onkeypress="return lettersOnly(event);" />
                                                <asp:RequiredFieldValidator ID="rfvtxtShortName" runat="server" ControlToValidate="txtShortName"
                                                    Display="None" ErrorMessage="Please Enter Short Name" ValidationGroup="payroll"></asp:RequiredFieldValidator>
							                </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
								                <div class="label-dynamic">
									                <sup>* </sup>
									                <label>Percentage</label>
								                </div>
                                                  <asp:TextBox ID="txtamt" runat="server" ToolTip="Enter Percentage" CssClass="form-control" TabIndex="4" onkeyup="return ValidateNumeric(this);" onchange="return Increment2();"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvtxtamt" runat="server" ControlToValidate="txtamt"
                                                    Display="None" ErrorMessage="Please Enter Percentage" ValidationGroup="payroll"></asp:RequiredFieldValidator>
							                </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
								                <div class="label-dynamic">
									                <label>Deducted</label>
								                </div>
                                                  <asp:CheckBox ID="chkDeducted" runat="server" ToolTip="Check if Loan is Deducted" CssClass="form-control" BorderStyle="None" TabIndex="5" />
							                </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSubmit" runat="server" TabIndex="6" Text="Submit" OnClick="btnSubmit_Click" ValidationGroup="payroll" CssClass="btn btn-primary" ToolTip="Click here to Submit" />
                                <asp:Button ID="btnCancel" runat="server" TabIndex="7" Text="Cancel" OnClick="btnCancel_Click" CausesValidation="False" CssClass="btn btn-warning" ToolTip="Click here to Cancel" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="payroll" DisplayMode="List" ShowSummary="false" ShowMessageBox="true" />
                            </div>
                                <asp:Panel ID="pnlList" runat="server">
                                    <div class="col-12">
                                        <asp:ListView ID="lvPFLoanType" runat="server">
                                        <LayoutTemplate>
                                            <%--div id="demo-grid" class="vista-grid">--%>
                                                <div class="sub-heading">
	                                                <h5>PF LOAN TYPE</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width:100%">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>Action
                                                            </th>
                                                            <th>Provident Fund
                                                            </th>
                                                            <th>Loan Short Name
                                                            </th>
                                                            <th>Loan Full Name
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
                                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("PFLOANTYPENO") %>'
                                                        AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />
                                                </td>
                                                <td>
                                                    <%# Eval("PFSHORTNAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("LOANSHORTNAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("NAME")%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>

                                    </asp:ListView>
                                    </div>                                  
                                </asp:Panel>
                            <div class="vista-grid_datapager d-none">
                            <div class="text-center">
                                <asp:DataPager ID="dpPager" runat="server" OnPreRender="dpPager_PreRender" PagedControlID="lvPFLoanType"
                                    PageSize="100">
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
                        </div>
                    </div>
                </div>
            </div>

              <script type="text/javascript">
                function ValidateNumeric(txt) {
                    if (isNaN(txt.value)) {
                        txt.value = txt.value.substring(0, (txt.value.length) - 1);
                        txt.value = "";
                        txt.focus();
                        alert("Only Numeric Characters allowed");
                        return false;
                    }
                    else {
                        return true;
                    }
                }
            </script>
              <script type="text/javascript" >
                  function Increment2() {
                      debugger
                      //validateNumeric(txt);


                      var SANCTIONAMOUNT = document.getElementById('<%=txtamt.ClientID%>').value;

                     if (SANCTIONAMOUNT > 100) {
                         alert("Percentage should be less than 100%");
                         document.getElementById("<%=txtamt.ClientID %>").value = "";
                         return false;
                     }
                     return true;
                 }
          </script>
             <script type="text/javascript">
                 function lettersOnly() {
                     var charCode = event.keyCode;

                     if ((charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123) || (charCode == 32) || (charCode == 8))

                         return true;
                     else
                         return false;
                     alert("Only Alphabets allowed");
                 }
            </script>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
