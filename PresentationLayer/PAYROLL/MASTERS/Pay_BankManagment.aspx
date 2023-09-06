<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Pay_BankManagment.aspx.cs" Inherits="PAYROLL_MASTERS_Pay_BankManagment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">BANK MANAGEMENT</h3>
                        </div>

                        <div class="box-body">
                            <asp:Label ID="lblStatus" runat="server" SkinID="Errorlbl" />
                            <asp:Label ID="lblHelp" runat="server" Font-Names="Trebuchet MS" />
                            <asp:Panel ID="pnlQualification" runat="server">
                                <div class="col-12">
	                                    <div class="row">
		                                    <div class="col-12">
		                                    <div class="sub-heading">
				                                    <h5>Add/Edit Bank Detail</h5>
			                                    </div>
		                                    </div>
	                                    </div>
                                    </div>
                                <div class="col-12">
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
								                <div class="label-dynamic">
									                <label><span style="color: red">*</span>BANK CODE </label>
								                </div>
                                                <asp:TextBox ID="txtbcode" runat="server" MaxLength="20" TabIndex="1" CssClass="form-control"
                                                    ToolTip="Enter Qualification(only alphabets)" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtbcode"
                                                    Display="None" ErrorMessage="Please Enter Bank Code" ValidationGroup="qual"></asp:RequiredFieldValidator>
							                </div>                                         
                                            <div class="form-group col-lg-3 col-md-6 col-12">
								                <div class="label-dynamic">
									                <label><span style="color: red">*</span>BANK NAME </label>
								                </div>
                                                <asp:TextBox ID="txtbname" runat="server" MaxLength="40" TabIndex="2" CssClass="form-control"
                                                    ToolTip="Enter Qualification(only alphabets)" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtbname"
                                                    Display="None" ErrorMessage="Please Enter Bank Name" ValidationGroup="qual"></asp:RequiredFieldValidator>
							                </div>                                              
                                            <div class="form-group col-lg-3 col-md-6 col-12">
								                <div class="label-dynamic">
									                <label> <span style="color: red">*</span>BRANCH NAME</label>
								                </div>
                                                <asp:TextBox ID="txtbranch" runat="server" MaxLength="20" TabIndex="3" CssClass="form-control"
                                                    ToolTip="Enter Qualification(only alphabets)" />
                                                <asp:RequiredFieldValidator ID="rfvQualification" runat="server" ControlToValidate="txtbranch"
                                                    Display="None" ErrorMessage="Please Enter Branch Name" ValidationGroup="qual"></asp:RequiredFieldValidator>
							                </div>                                              
                                            <div class="form-group col-lg-3 col-md-6 col-12">
								                <div class="label-dynamic">
									                <label><span style="color: red">*</span>BANK ADDRESS </label>
								                </div>
                                                <asp:TextBox ID="txtaddress" runat="server" MaxLength="20" TabIndex="4" CssClass="form-control"
                                                    ToolTip="Enter Qualification(only alphabets)" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtaddress"
                                                    Display="None" ErrorMessage="Please Enter Bank Address" ValidationGroup="qual"></asp:RequiredFieldValidator>
							                </div>
                                        </div>
                                    </div>
                            </asp:Panel>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit"
                                    ValidationGroup="qual" ToolTip="Click here to Submit" TabIndex="5" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
                                <asp:Button ID="btnShowReport" runat="server" Text="Report" CausesValidation="False"
                                    ToolTip="Click here to Show Report" TabIndex="6" CssClass="btn btn-info"  OnClick="btnShowReport_Click"/>
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel"
                                    CausesValidation="False" ToolTip="Click here to Cancel" TabIndex="7" CssClass="btn btn-warning" OnClick="btnCancel_Click" />                                
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="qual"
                                    DisplayMode="List" ShowSummary="false" ShowMessageBox="true" />           
                            </div>
                            <div class="col-12">
                                    <asp:Panel ID="pnlList" runat="server">
                                        <asp:ListView ID="lvQualification" runat="server">
                                            <LayoutTemplate>
                                                <div class="sub-heading">
	                                                <h5>BANK LIST</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width:100%">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>Action</th>
                                                            <th>ID</th>
                                                            <th>BANK CODE</th>
                                                            <th>BANK NAME</th>
                                                            <th>BANK ADDRESS</th>
                                                            <th>BRANCH </th>
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
                                                        <asp:ImageButton ID="btnEdit" runat="server" AlternateText="Edit Record" CommandArgument='<%# Eval("BANKNO") %>' ImageUrl="~/Images/edit.png" ToolTip="Edit Record" OnClick="btnEdit_Click" />

                                                    </td>
                                                    <td>

                                                        <asp:Label ID="Lblbnakno" runat="server" Text='<%# Eval("BANKNO") %>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Lblbankcode" runat="server" Text='<%# Eval("BANKCODE") %>'></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="Lblbankname" runat="server" Text='<%# Eval("BANKNAME") %>'></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="Lblbankadd" runat="server" Text='<%# Eval("BANKADDR") %>'></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="Lblbranchname" runat="server" Text='<%# Eval("BRANCHNAME") %>'></asp:Label></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </asp:Panel>
                                </div>
                                <div class="vista-grid_datapager d-none">
                                    <div class="text-center">
                                        <%--<asp:DataPager ID="dpPager" runat="server" OnPreRender="dpPager_PreRender" PagedControlID="lvQualification"
                                        PageSize="10">
                                        <Fields>
                                            <asp:NextPreviousPagerField FirstPageText="<<" PreviousPageText="<" ButtonType="Link"
                                                RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="true" ShowPreviousPageButton="true"
                                                ShowLastPageButton="false" ShowNextPageButton="false" />
                                            <asp:NumericPagerField ButtonType="Link" ButtonCount="7" CurrentPageLabelCssClass="current" />
                                            <asp:NextPreviousPagerField LastPageText=">>" NextPageText=">" ButtonType="Link"
                                                RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="false" ShowPreviousPageButton="false"
                                                ShowLastPageButton="true" ShowNextPageButton="true" />
                                        </Fields>
                                    </asp:DataPager>--%>
                                    </div>
                                </div>
                        </div>
                    </div>
                </div>
            </div>
             <div id="divMsg" runat="server">
            </div>
        </ContentTemplate>
         <Triggers>
            <%--<asp:PostBackTrigger ControlID="btnShowReport" />--%>
            <asp:AsyncPostBackTrigger ControlID="btnShowReport" />
         </Triggers>
    </asp:UpdatePanel>
</asp:Content>

